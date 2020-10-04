using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPPC.Framework.Common;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Model.Finance;
using SPPC.Tadbir.Resources;
using SPPC.Tadbir.Utility;
using SPPC.Tadbir.ViewModel.Finance;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای مدیریت اطلاعات نرخ های ارز را پیاده سازی می کند
    /// </summary>
    public class CurrencyRateRepository : LoggingRepository<CurrencyRate, CurrencyRateViewModel>, ICurrencyRateRepository
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="context">امکانات مشترک مورد نیاز را برای عملیات دیتابیسی فراهم می کند</param>
        /// <param name="system">امکانات مورد نیاز در دیتابیس های سیستمی را فراهم می کند</param>
        public CurrencyRateRepository(IRepositoryContext context, ISystemRepository system)
            : base(context, system.Logger)
        {
            _system = system;
        }

        /// <summary>
        /// به روش آسنکرون، کلیه نرخ های ثبت شده برای ارز مشخص شده را خوانده و برمی گرداند
        /// </summary>
        /// <param name="currencyId">شناسه دیتابیسی ارز مورد نظر</param>
        /// <returns>مجموعه نرخ های ثبت شده برای ارز مورد نظر</returns>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        public async Task<PagedList<CurrencyRateViewModel>> GetCurrencyRatesAsync(int currencyId, GridOptions gridOptions)
        {
            var currency = await Repository
                .GetAllQuery<Currency>(ViewId.Currency)
                .Where(curr => curr.Id == currencyId)
                .FirstOrDefaultAsync();
            if (currency != null)
            {
                var all = await Repository
                    .GetAllQuery<CurrencyRate>(ViewId.CurrencyRate, rate => rate.Branch)
                    .Where(rate => rate.CurrencyId == currencyId)
                    .OrderByDescending(rate => rate.Date)
                    .ThenByDescending(rate => rate.Time)
                    .Select(rate => Mapper.Map<CurrencyRateViewModel>(rate))
                    .ToListAsync();
                var description = GetRatesOperationDescription(currency.Name);
                var options = gridOptions ?? new GridOptions();
                options.Operation = (int)OperationId.ViewRates;
                await ReadAsync(options, description);
                return new PagedList<CurrencyRateViewModel>(all, gridOptions);
            }

            return null;
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات نرخ ارز با شناسه عددی مشخص شده را خوانده و برمی گرداند
        /// </summary>
        /// <param name="rateId">شناسه عددی یکی از نرخ های موجود</param>
        /// <returns>اطلاعات نرخ ارز مورد نظر</returns>
        public async Task<CurrencyRateViewModel> GetCurrencyRateAsync(int rateId)
        {
            CurrencyRateViewModel item = null;
            var repository = UnitOfWork.GetAsyncRepository<CurrencyRate>();
            var currencyRate = await repository.GetByIDAsync(rateId, rate => rate.Branch);
            if (currencyRate != null)
            {
                item = Mapper.Map<CurrencyRateViewModel>(currencyRate);
            }

            return item;
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات یک نرخ ارز را ایجاد یا اصلاح می کند
        /// </summary>
        /// <param name="currencyRate">نرخ ارز مورد نظر برای ایجاد یا اصلاح</param>
        /// <returns>اطلاعات نمایشی نرخ ارز ایجاد یا اصلاح شده</returns>
        public async Task<CurrencyRateViewModel> SaveCurrencyRateAsync(CurrencyRateViewModel currencyRate)
        {
            Verify.ArgumentNotNull(currencyRate, nameof(currencyRate));
            CurrencyRate rateModel = default(CurrencyRate);
            var repository = UnitOfWork.GetAsyncRepository<CurrencyRate>();
            if (currencyRate.Id == 0)
            {
                rateModel = Mapper.Map<CurrencyRate>(currencyRate);
                await InsertAsync(repository, rateModel, OperationId.CreateRate);
            }
            else
            {
                rateModel = await repository.GetByIDAsync(currencyRate.Id);
                if (rateModel != null)
                {
                    await UpdateAsync(repository, rateModel, currencyRate, OperationId.EditRate);
                }
            }

            return Mapper.Map<CurrencyRateViewModel>(rateModel);
        }

        /// <summary>
        /// به روش آسنکرون، نرخ ارز مشخص شده با شناسه عددی را حذف می کند
        /// </summary>
        /// <param name="rateId">شناسه عددی نرخ ارز مورد نظر برای حذف</param>
        public async Task DeleteCurrencyRateAsync(int rateId)
        {
            var repository = UnitOfWork.GetAsyncRepository<CurrencyRate>();
            var currencyRate = await repository.GetByIDAsync(rateId);
            if (currencyRate != null)
            {
                await DeleteAsync(repository, currencyRate, OperationId.DeleteRate);
            }
        }

        /// <summary>
        /// به روش آسنکرون، نرخ ارزهای مشخص شده با شناسه دیتابیسی را حذف می کند
        /// </summary>
        /// <param name="items">مجموعه شناسه های دیتابیسی سطرهای مورد نظر برای حذف</param>
        public async Task DeleteCurrencyRatesAsync(IEnumerable<int> items)
        {
            var repository = UnitOfWork.GetAsyncRepository<CurrencyRate>();
            foreach (int item in items)
            {
                var currencyRate = await repository.GetByIDAsync(item);
                if (currencyRate != null)
                {
                    await DeleteNoLogAsync(repository, currencyRate);
                }
            }

            await OnEntityGroupDeleted(items, OperationId.GroupDeleteRates);
        }

        /// <summary>
        /// به روش آسنکرون، مشخص میکند که آیا برای ارز مشخص شده نرخ تعریف شده است یا خیر؟
        /// </summary>
        /// <param name="currencyId">شناسه دیتابیسی ارز مورد نظر</param>
        /// <returns>اگر برای ارز مشخص شده نرخ تعریف شده باشد مقدار بولی "درست" و در غیر این صورت
        /// مقدار بولی "نادرست" را برمی گرداند</returns>
        public async Task<bool> CurrencyHasRatesAsync(int currencyId)
        {
            int rateCount = await Repository
                .GetAllQuery<CurrencyRate>(ViewId.CurrencyRate)
                .Where(rate => rate.CurrencyId == currencyId)
                .CountAsync();
            return rateCount > 0;
        }

        internal override int? EntityType
        {
            get { return (int)EntityTypeId.Currency; }
        }

        /// <summary>
        /// آخرین تغییرات موجودیت را از مدل نمایشی به سطر اطلاعاتی موجود کپی می کند
        /// </summary>
        /// <param name="rateViewModel">مدل نمایشی شامل آخرین تغییرات</param>
        /// <param name="rate">سطر اطلاعاتی موجود</param>
        protected override void UpdateExisting(CurrencyRateViewModel rateViewModel, CurrencyRate rate)
        {
            rate.Date = rateViewModel.Date;
            rate.Time = rateViewModel.Time;
            rate.Multiplier = rateViewModel.Multiplier;
            rate.Description = rateViewModel.Description;
            rate.BranchScope = rateViewModel.BranchScope;
        }

        /// <summary>
        /// اطلاعات خلاصه سطر اطلاعاتی داده شده را به صورت یک رشته متنی برمی گرداند
        /// </summary>
        /// <param name="entity">یکی از سطرهای اطلاعاتی موجود</param>
        /// <returns>اطلاعات خلاصه سطر اطلاعاتی داده شده به صورت رشته متنی</returns>
        protected override string GetState(CurrencyRate entity)
        {
            string dateValue = Config.GetDateDisplayAsync(entity.Date).Result;
            return (entity != null)
                ? String.Format(
                    "{0} : {1} , {2} : {3} , {4} : {5}",
                    AppStrings.Date, dateValue, AppStrings.Time, entity.Time,
                    AppStrings.Multiplier, entity.Multiplier)
                : null;
        }

        /// <inheritdoc/>
        protected override async Task FinalizeActionAsync(CurrencyRate entity)
        {
            var repository = UnitOfWork.GetAsyncRepository<Currency>();
            var currency = await repository.GetByIDAsync(entity.CurrencyId);
            if (currency != null)
            {
                await UnitOfWork.CommitAsync();
                Log.EntityId = entity.Id;
                CopyEntityDataToLog(currency);
                Log.EntityName = Context.Localize(Log.EntityName);
                await TrySaveLogAsync();
            }
        }

        private ISecureRepository Repository
        {
            get { return _system.Repository; }
        }

        private IConfigRepository Config
        {
            get { return _system.Config; }
        }

        private string GetRatesOperationDescription(string currencyName)
        {
            string template = Context.Localize(AppStrings.CurrencyRatesList);
            string description = String.Format(template, currencyName);
            return Context.Localize(description);
        }

        private readonly ISystemRepository _system;
    }
}
