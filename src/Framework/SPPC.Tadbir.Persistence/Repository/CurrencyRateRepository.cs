using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPPC.Framework.Common;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Model.Finance;
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
        /// <param name="log">امکان ایجاد لاگ های عملیاتی را در دیتابیس سیستمی برنامه فراهم می کند</param>
        public CurrencyRateRepository(IRepositoryContext context, IOperationLogRepository log)
            : base(context, log)
        {
        }

        /// <summary>
        /// به روش آسنکرون، کلیه نرخ های ثبت شده برای ارز مشخص شده را خوانده و برمی گرداند
        /// </summary>
        /// <param name="currencyId">شناسه دیتابیسی ارز مورد نظر</param>
        /// <returns>مجموعه نرخ های ثبت شده برای ارز مورد نظر</returns>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        public async Task<IList<CurrencyRateViewModel>> GetCurrencyRatesAsync(int currencyId, GridOptions gridOptions)
        {
            var repository = UnitOfWork.GetAsyncRepository<CurrencyRate>();
            var parentRepository = UnitOfWork.GetAsyncRepository<Currency>();
            var currency = await parentRepository.GetByIDAsync(currencyId);
            if (currency != null)
            {
                var all = await repository
                    .GetEntityQuery(rate => rate.Branch)
                    .Where(rate => rate.CurrencyId == currencyId)
                    .OrderByDescending(rate => rate.Date)
                    .ThenByDescending(rate => rate.Time)
                    .Select(rate => Mapper.Map<CurrencyRateViewModel>(rate))
                    .ToListAsync();
                OnEntityAction(OperationId.View);
                Log.Description = String.Format("Currency : {0}", currency.Name);
                await ReadAsync(gridOptions);
                return all;
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
                await InsertAsync(repository, rateModel);
            }
            else
            {
                rateModel = await repository.GetByIDAsync(currencyRate.Id);
                if (rateModel != null)
                {
                    await UpdateAsync(repository, rateModel, currencyRate);
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
                await DeleteAsync(repository, currencyRate);
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

            await OnEntityGroupDeleted(items);
        }

        /// <summary>
        /// به روش آسنکرون، مشخص میکند که آیا برای ارز مشخص شده نرخ تعریف شده است یا خیر؟
        /// </summary>
        /// <param name="currencyId">شناسه دیتابیسی ارز مورد نظر</param>
        /// <returns>اگر برای ارز مشخص شده نرخ تعریف شده باشد مقدار بولی "درست" و در غیر این صورت
        /// مقدار بولی "نادرست" را برمی گرداند</returns>
        public async Task<bool> CurrencyHasRatesAsync(int currencyId)
        {
            var repository = UnitOfWork.GetAsyncRepository<CurrencyRate>();
            var currencyRates = await repository
                .GetCountByCriteriaAsync(rate => rate.CurrencyId == currencyId);
            return currencyRates > 0 ? true : false;
        }

        internal override int? EntityType
        {
            get { return (int)EntityTypeId.CurrencyRate; }
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
            return (entity != null)
                ? String.Format("Date : {1}{0}Time : {2}{0}Multiplier : {3}",
                Environment.NewLine, entity.Date, entity.Time, entity.Multiplier)
                : null;
        }
    }
}
