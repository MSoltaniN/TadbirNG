using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPPC.Framework.Common;
using SPPC.Framework.Extensions;
using SPPC.Framework.Helpers;
using SPPC.Framework.Mapper;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Model.Finance;
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
        /// <param name="unitOfWork">پیاده سازی اینترفیس واحد کاری برای انجام عملیات دیتابیسی </param>
        /// <param name="mapper">نگاشت مورد استفاده برای تبدیل کلاس های مدل اطلاعاتی</param>
        /// <param name="metadata">امکان خواندن متادیتا برای یک موجودیت را فراهم می کند</param>
        /// <param name="log">امکان ایجاد لاگ های عملیاتی را در دیتابیس سیستمی برنامه فراهم می کند</param>
        public CurrencyRepository(IAppUnitOfWork unitOfWork, IDomainMapper mapper,
            IMetadataRepository metadata, IOperationLogRepository log)
            : base(unitOfWork, mapper, metadata, log)
        {
        }

        /// <summary>
        /// به روش آسنکرون، کلیه ارزها را خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه ای از ارزها تعریف شده</returns>
        public async Task<IList<CurrencyViewModel>> GetCurrenciesAsync(GridOptions gridOptions = null)
        {
            var repository = UnitOfWork.GetAsyncRepository<Currency>();
            var currencies = await repository
                .GetEntityQuery(curr => curr.Branch)
                .Select(item => Mapper.Map<CurrencyViewModel>(item))
                .ToListAsync();
            return currencies
                .Apply(gridOptions)
                .ToList();
        }

        /// <summary>
        /// به روش آسنکرون، تعداد ارزها را خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>تعداد ارزها</returns>
        public async Task<int> GetCountAsync(GridOptions gridOptions = null)
        {
            var repository = UnitOfWork.GetAsyncRepository<Currency>();
            var items = await repository
                .GetEntityQuery()
                .Select(item => Mapper.Map<CurrencyViewModel>(item))
                .ToListAsync();
            return items
                .Apply(gridOptions, false)
                .Count();
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
                var accountRepository = UnitOfWork.GetAsyncRepository<Account>();
                var account = await accountRepository.GetFirstByCriteriaAsync(
                    acc => acc.Id == accountId, acc => acc.Currency);
                if (account != null && account.CurrencyId.HasValue)
                {
                    currency = Mapper.Map<CurrencyInfoViewModel>(account.Currency);
                }
            }

            return currency;
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
            foreach (int item in items)
            {
                await DeleteCurrencyAsync(item);
            }
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
        }

        /// <summary>
        /// اطلاعات خلاصه سطر اطلاعاتی داده شده را به صورت یک رشته متنی برمی گرداند
        /// </summary>
        /// <param name="entity">یکی از سطرهای اطلاعاتی موجود</param>
        /// <returns>اطلاعات خلاصه سطر اطلاعاتی داده شده به صورت رشته متنی</returns>
        protected override string GetState(Currency entity)
        {
            return (entity != null)
                ? String.Format(@"Name : {1}{0}Country : {2}{0}Code : {3}{0}MinorUnit : {4}{0}
Multiplier : {5}{0}DecimalCount : {6}{0}Description : {7}{0}", Environment.NewLine,
                entity.Name, entity.Country, entity.Code, entity.MinorUnit, entity.Multiplier,
                entity.DecimalCount, entity.Description)
                : null;
        }

        private List<CurrencyInfo> GetLocalCurrencyDatabase(string localDbPath)
        {
            return JsonHelper.To<List<CurrencyInfo>>(File.ReadAllText(localDbPath));
        }

        private async Task<bool> IsUsedInAccounts(int currencyId)
        {
            var accountRepository = UnitOfWork.GetAsyncRepository<Account>();
            int usageCount = await accountRepository
                .GetEntityQuery()
                .Where(acc => acc.CurrencyId == currencyId)
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
    }
}
