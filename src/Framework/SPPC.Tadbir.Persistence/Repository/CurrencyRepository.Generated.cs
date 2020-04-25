using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPPC.Framework.Common;
using SPPC.Framework.Helpers;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Helpers;
using SPPC.Tadbir.Model.Finance;
using SPPC.Tadbir.Resources;
using SPPC.Tadbir.ViewModel;
using SPPC.Tadbir.ViewModel.Finance;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای مدیریت ارزها را پیاده سازی میکند
    /// </summary>
    public class CurrencyRepository : LoggingRepository<Currency, CurrencyViewModel>, ICurrencyRepository
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="context">امکانات مشترک مورد نیاز را برای عملیات دیتابیسی فراهم می کند</param>
        /// <param name="log">امکان ایجاد لاگ های عملیاتی را در دیتابیس سیستمی برنامه فراهم می کند</param>
        /// <param name="access">امکان کار با دیتابیس های برنامه اکسس را فراهم می کند</param>
        public CurrencyRepository(IRepositoryContext context, IOperationLogRepository log,
            IAccessRepository access)
            : base(context, log)
        {
            _access = access;
        }

        /// <summary>
        /// به روش آسنکرون، کلیه ارزها را خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه ای از ارزها تعریف شده</returns>
        public async Task<PagedList<CurrencyViewModel>> GetCurrenciesAsync(GridOptions gridOptions = null)
        {
            var repository = UnitOfWork.GetAsyncRepository<Currency>();
            var currencies = await repository
                .GetEntityQuery(curr => curr.Branch)
                .Select(item => Mapper.Map<CurrencyViewModel>(item))
                .ToListAsync();
            await ReadAsync(gridOptions);
            return new PagedList<CurrencyViewModel>(currencies, gridOptions);
        }

        /// <summary>
        /// به روش آسنکرون، ارز با شناسه عددی مشخص شده را خوانده و برمی گرداند
        /// </summary>
        /// <param name="currencyId">شناسه عددی یکی از ارزها موجود</param>
        /// <returns>ارز مشخص شده با شناسه عددی</returns>
        public async Task<CurrencyViewModel> GetCurrencyAsync(int currencyId)
        {
            CurrencyViewModel item = null;
            var repository = UnitOfWork.GetAsyncRepository<Currency>();
            var currency = await repository.GetByIDAsync(currencyId, curr => curr.Branch);
            if (currency != null)
            {
                item = Mapper.Map<CurrencyViewModel>(currency);
            }

            return item;
        }

        /// <summary>
        /// اطلاعات استاندارد یک ارز با نام مشخص شده را خوانده و برمی گرداند
        /// </summary>
        /// <param name="localDbPath">مسیر فیزیکی فایل استاتیک بانک اطلاعاتی ارزهای استاندارد</param>
        /// <param name="nameKey">کلید متن چندزبانه برای نام ارز مورد نظر</param>
        /// <returns>اطلاعات استاندارد ارز</returns>
        public CurrencyViewModel GetCurrencyByName(string localDbPath, string nameKey)
        {
            var currencies = GetLocalCurrencyDatabase(localDbPath);
            var currency = currencies
                .Where(curr => curr.Currency.NameKey == nameKey)
                .Select(curr => Mapper.Map<CurrencyViewModel>(curr))
                .FirstOrDefault();
            return currency;
        }

        /// <summary>
        /// مجموعه ای از همه ارزهای معتبر شناخته شده را به صورت زوج های کلید-مقدار خوانده و برمی گرداند
        /// </summary>
        /// <param name="localDbPath">مسیر فیزیکی فایل استاتیک بانک اطلاعاتی ارزهای استاندارد</param>
        /// <returns>مجموعه ای از همه ارزهای معتبر شناخته شده</returns>
        public IList<KeyValue> GetCurrencyNamesLookup(string localDbPath)
        {
            var currencies = GetLocalCurrencyDatabase(localDbPath);
            var names = currencies
                .Select(info => info.Currency.NameKey)
                .Distinct()
                .Select(name => new KeyValue(name, name))
                .ToList();
            return names;
        }

        /// <summary>
        /// به روش آسنکرون، ارز پیش فرض برای یک بردار حساب با حساب و شناور مشخص شده را خوانده و برمی گرداند
        /// </summary>
        /// <param name="accountId">شناسه دیتابیسی مولفه حساب در بردار حساب مورد نظر</param>
        /// <param name="faccountId">شناسه دیتابیسی مولفه تفصیلی شناور در بردار حساب مورد نظر</param>
        /// <returns>اطلاعات ارز پیش فرض برای بردار حساب مشخص شده</returns>
        public async Task<CurrencyInfoViewModel> GetDefaultCurrencyAsync(int accountId, int faccountId)
        {
            var currency = default(CurrencyInfoViewModel);
            var detailRepository = UnitOfWork.GetAsyncRepository<DetailAccount>();
            var detailAccount = await detailRepository.GetByIDAsync(faccountId, facc => facc.Currency);
            if (detailAccount != null && detailAccount.CurrencyId.HasValue)
            {
                currency = Mapper.Map<CurrencyInfoViewModel>(detailAccount.Currency);
            }
            else
            {
                var accountCurrencyRepository = UnitOfWork.GetAsyncRepository<AccountCurrency>();
                var accCurrency = await accountCurrencyRepository.GetSingleByCriteriaAsync(
                    accCurr => accCurr.AccountId == accountId && accCurr.BranchId == UserContext.BranchId,
                    accCurr => accCurr.Currency);
                if (accCurrency != null)
                {
                    currency = Mapper.Map<CurrencyInfoViewModel>(accCurrency.Currency);
                }
            }

            return currency;
        }

        /// <summary>
        /// به روش آسنکرون، مجموعه ارزهای مالیاتی تعریف شده را خوانده و برمی گرداند
        /// </summary>
        /// <returns>ارزهای مالیاتی تعریف شده در دیتابیس شرکت جاری</returns>
        public async Task<IList<TaxCurrencyViewModel>> GetTaxCurrenciesAsync()
        {
            var repository = UnitOfWork.GetAsyncRepository<TaxCurrency>();
            return await repository
                .GetEntityQuery()
                .OrderBy(curr => curr.Name)
                .Select(curr => Mapper.Map<TaxCurrencyViewModel>(curr))
                .ToListAsync();
        }

        /// <summary>
        /// به روش آسنکرون، ارزهای مالیاتی تعریف شده در شرکت جاری را به روزرسانی می کند
        /// </summary>
        /// <param name="mdbPath">مسیر فایل بانک اطلاعاتی اکسس مرتبط با ارزهای مالیاتی</param>
        public async Task UpdateTaxCurrenciesAsync(string mdbPath)
        {
            var repository = UnitOfWork.GetAsyncRepository<TaxCurrency>();
            repository.ExecuteCommand("TRUNCATE TABLE [Finance].[TaxCurrency]");
            var taxItems = await _access.GetAllAsync<TaxCurrencyViewModel>(mdbPath, "Arz");
            foreach (var taxItem in taxItems)
            {
                repository.Insert(Mapper.Map<TaxCurrency>(taxItem));
            }

            await UnitOfWork.CommitAsync();
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات یک ارز را ایجاد یا اصلاح می کند
        /// </summary>
        /// <param name="currency">ارز مورد نظر برای ایجاد یا اصلاح</param>
        /// <returns>اطلاعات نمایشی ارز ایجاد یا اصلاح شده</returns>
        public async Task<CurrencyViewModel> SaveCurrencyAsync(CurrencyViewModel currency)
        {
            Verify.ArgumentNotNull(currency, nameof(currency));
            Currency currencyModel = default(Currency);
            var repository = UnitOfWork.GetAsyncRepository<Currency>();
            if (currency.Id == 0)
            {
                currencyModel = Mapper.Map<Currency>(currency);
                await InsertAsync(repository, currencyModel);
            }
            else
            {
                currencyModel = await repository.GetByIDAsync(currency.Id);
                if (currencyModel != null)
                {
                    await UpdateAsync(repository, currencyModel, currency);
                }
            }

            return Mapper.Map<CurrencyViewModel>(currencyModel);
        }

        /// <summary>
        /// به روش آسنکرون، ارز مشخص شده با شناسه عددی را حذف می کند
        /// </summary>
        /// <param name="currencyId">شناسه عددی ارز مورد نظر برای حذف</param>
        public async Task DeleteCurrencyAsync(int currencyId)
        {
            var repository = UnitOfWork.GetAsyncRepository<Currency>();
            var currency = await repository.GetByIDWithTrackingAsync(currencyId, curr => curr.Rates);
            if (currency != null)
            {
                currency.Rates.Clear();
                await DeleteAsync(repository, currency);
            }
        }

        /// <summary>
        /// به روش آسنکرون، ارزهای مشخص شده با شناسه دیتابیسی را حذف می کند
        /// </summary>
        /// <param name="items">مجموعه شناسه های دیتابیسی سطرهای مورد نظر برای حذف</param>
        public async Task DeleteCurrenciesAsync(IEnumerable<int> items)
        {
            var repository = UnitOfWork.GetAsyncRepository<Currency>();
            foreach (int item in items)
            {
                var currency = await repository.GetByIDWithTrackingAsync(item, curr => curr.Rates);
                if (currency != null)
                {
                    currency.Rates.Clear();
                    await DeleteNoLogAsync(repository, currency);
                }
            }

            await OnEntityGroupDeleted(items);
        }

        /// <summary>
        /// به روش آسنکرون، مشخص می کند که آیا ارز مشخص شده با شناسه دیتابیسی قابل حذف هست یا نه؟
        /// </summary>
        /// <param name="currencyId">شناسه دیتابیسی ارز مورد نظر برای حذف</param>
        /// <returns>در صورتی که ارز مورد نظر قابل حذف باشد مقدار بولی "درست" و
        /// در غیر این صورت مقدار بولی "نادرست" را برمی گرداند.</returns>
        public async Task<bool> CanDeleteCurrencyAsync(int currencyId)
        {
            bool isUsed = await IsUsedInAccounts(currencyId);
            if (!isUsed)
            {
                isUsed = await IsUsedInVoucherLines(currencyId);
            }

            return !isUsed;
        }

        /// <summary>
        /// به روش آسنکرون، تکراری بودن ارز مشخص شده توسط نمایه بین المللی را بررسی می کند
        /// </summary>
        /// <param name="code">نمایه بین المللی ارز مورد نظر</param>
        /// <param name="currencyId">شناسه دیتابیسی ارز مورد نظر برای ایجاد یا اصلاح</param>
        /// <returns>اگر ارز مشخص شده قبلاً تعریف شده باشد مقدار بولی "درست" و در غیر این صورت
        /// مقدار بولی "نادرست" را برمی گرداند</returns>
        public async Task<bool> IsDuplicateCurrencyAsync(string code, int currencyId = 0)
        {
            var repository = UnitOfWork.GetAsyncRepository<Currency>();
            var existing = await repository.GetFirstByCriteriaAsync(
                curr => curr.Code == code && curr.Id != currencyId);
            return existing != null;
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات یک ارز پایه را ایجاد می کند
        /// </summary>
        /// <param name="currency">ارز مورد نظر برای ایجاد</param>
        /// <returns>اطلاعات نمایشی ارز ایجاد شده</returns>
        public async Task<CurrencyViewModel> InsertDefaultCurrencyAsync(CurrencyViewModel currency)
        {
            var repository = UnitOfWork.GetAsyncRepository<Currency>();
            CurrencyViewModel currencyViewModel = default(CurrencyViewModel);
            var existingDefaultCurrency = await repository.GetFirstByCriteriaAsync(
                curr => curr.IsDefaultCurrency);

            if (existingDefaultCurrency != null)
            {
                if (existingDefaultCurrency.Code != currency.Code)
                {
                    currencyViewModel = Mapper.Map<CurrencyViewModel>(existingDefaultCurrency);
                    currencyViewModel.IsDefaultCurrency = false;

                    await UpdateAsync(repository, existingDefaultCurrency, currencyViewModel);

                    currencyViewModel = await SaveDefaultCurrencyAsync(currency);
                }
                else
                {
                    currencyViewModel = Mapper.Map<CurrencyViewModel>(existingDefaultCurrency);
                }
            }
            else
            {
                currencyViewModel = await SaveDefaultCurrencyAsync(currency);
            }

            return currencyViewModel;
        }

        internal override int? EntityType
        {
            get { return (int)EntityTypeId.Currency; }
        }

        /// <summary>
        /// آخرین تغییرات موجودیت را از مدل نمایشی به سطر اطلاعاتی موجود کپی می کند
        /// </summary>
        /// <param name="currencyViewModel">مدل نمایشی شامل آخرین تغییرات</param>
        /// <param name="currency">سطر اطلاعاتی موجود</param>
        protected override void UpdateExisting(CurrencyViewModel currencyViewModel, Currency currency)
        {
            currency.BranchScope = currencyViewModel.BranchScope;
            currency.Name = currencyViewModel.Name;
            currency.Country = currencyViewModel.Country;
            currency.Code = currencyViewModel.Code;
            currency.TaxCode = currencyViewModel.TaxCode;
            currency.MinorUnit = currencyViewModel.MinorUnit;
            currency.Multiplier = currencyViewModel.Multiplier;
            currency.DecimalCount = currencyViewModel.DecimalCount;
            currency.IsActive = currencyViewModel.IsActive;
            currency.Description = currencyViewModel.Description;
            currency.IsDefaultCurrency = currencyViewModel.IsDefaultCurrency;
        }

        /// <summary>
        /// اطلاعات خلاصه سطر اطلاعاتی داده شده را به صورت یک رشته متنی برمی گرداند
        /// </summary>
        /// <param name="entity">یکی از سطرهای اطلاعاتی موجود</param>
        /// <returns>اطلاعات خلاصه سطر اطلاعاتی داده شده به صورت رشته متنی</returns>
        protected override string GetState(Currency entity)
        {
            return (entity != null)
                ? String.Format(
                    AppStrings.Name, entity.Name, AppStrings.Code, entity.Code,
                    AppStrings.MinorUnit, entity.MinorUnit, AppStrings.Multiplier, entity.Multiplier,
                    AppStrings.DecimalCount, entity.DecimalCount, AppStrings.Description, entity.Description)
                : null;
        }

        private List<CurrencyInfo> GetLocalCurrencyDatabase(string localDbPath)
        {
            return JsonHelper.To<List<CurrencyInfo>>(File.ReadAllText(localDbPath));
        }

        private async Task<bool> IsUsedInAccounts(int currencyId)
        {
            var accountCurrencyRepository = UnitOfWork.GetAsyncRepository<AccountCurrency>();
            int usageCount = await accountCurrencyRepository
                .GetEntityQuery()
                .Where(accCurr => accCurr.CurrencyId == currencyId)
                .CountAsync();
            return (usageCount != 0);
        }

        private async Task<bool> IsUsedInVoucherLines(int currencyId)
        {
            var lineRepository = UnitOfWork.GetAsyncRepository<VoucherLine>();
            int usageCount = await lineRepository
                .GetEntityQuery()
                .Where(line => line.CurrencyId == currencyId)
                .CountAsync();
            return (usageCount != 0);
        }

        private async Task<CurrencyViewModel> SaveDefaultCurrencyAsync(CurrencyViewModel currency)
        {
            var repository = UnitOfWork.GetAsyncRepository<Currency>();
            Currency currencyModel = default(Currency);
            var existingCurrency = await repository.GetFirstByCriteriaAsync(
                          curr => curr.Code == currency.Code);

            if (existingCurrency != null)
            {
                await UpdateAsync(repository, existingCurrency, currency);
            }
            else
            {
                currencyModel = Mapper.Map<Currency>(currency);
                await InsertAsync(repository, currencyModel);
            }

            return Mapper.Map<CurrencyViewModel>(currencyModel);
        }

        private readonly IAccessRepository _access;
    }
}
