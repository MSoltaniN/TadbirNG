using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPPC.Framework.Common;
using SPPC.Framework.Helpers;
using SPPC.Framework.Persistence;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Configuration.Models;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Helpers;
using SPPC.Tadbir.Model.Finance;
using SPPC.Tadbir.Values;
using SPPC.Tadbir.ViewModel.Finance;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای مدیریت اطلاعات سرفصل های حسابداری را تعریف می کند.
    /// </summary>
    public class AccountRepository : LoggingRepository<Account, AccountViewModel>, IAccountRepository
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="context">امکانات مشترک مورد نیاز را برای عملیات دیتابیسی فراهم می کند</param>
        /// <param name="system">امکانات مورد نیاز در دیتابیس های سیستمی را فراهم می کند</param>
        /// <param name="customerTaxInfo">امکانات مورد نیاز برای اطلاعات مالی طرف حساب ها را فراهم میکند</param>
        public AccountRepository(IRepositoryContext context, ISystemRepository system, ICustomerTaxInfoRepository customerTaxInfo)
            : base(context, system?.Logger)
        {
            _system = system;
            _customerTaxInfo = customerTaxInfo;
        }

        /// <summary>
        /// به روش آسنکرون، کلیه حساب هایی را که در دوره مالی و شعبه مشخص شده تعریف شده اند،
        /// از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه ای از حساب های تعریف شده در دوره مالی و شعبه مشخص شده</returns>
        public async Task<PagedList<AccountViewModel>> GetAccountsAsync(GridOptions gridOptions = null)
        {
            var accounts = await Repository
                .GetAllQuery<Account>(ViewName.Account, acc => acc.Children)
                .Select(item => Mapper.Map<AccountViewModel>(item))
                .ToListAsync();
            await ReadAsync(gridOptions);
            return new PagedList<AccountViewModel>(accounts, gridOptions);
        }

        /// <summary>
        /// به روش آسنکرون، کلیه حساب هایی را که در دوره مالی و شعبه مشخص شده تعریف شده اند،
        /// به صورت مجموعه ای از کد و نام خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه ای از حساب های تعریف شده در دوره مالی و شعبه مشخص شده</returns>
        public async Task<IList<KeyValue>> GetAccountsLookupAsync(GridOptions gridOptions = null)
        {
            return await Repository.GetAllLookupAsync<Account>(ViewName.Account, gridOptions);
        }

        /// <summary>
        /// به روش آسنکرون، حساب با شناسه عددی مشخص شده را از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <param name="accountId">شناسه عددی یکی از حساب های موجود</param>
        /// <returns>حساب مشخص شده با شناسه عددی</returns>
        public async Task<AccountViewModel> GetAccountAsync(int accountId)
        {
            AccountViewModel item = null;
            var repository = UnitOfWork.GetAsyncRepository<Account>();
            var account = await repository.GetByIDWithTrackingAsync(accountId);
            if (account != null)
            {
                item = Mapper.Map<AccountViewModel>(account);
                item.GroupId = GetAccountGroupId(repository, account);
                item.CurrencyId = await GetAccountCurrencyIdAsync(account.Id);
            }

            return item;
        }

        /// <summary>
        /// به روش آسنکرون، برای حساب والد مشخص شده حساب زیرمجموعه جدیدی پیشنهاد داده و برمی گرداند
        /// </summary>
        /// <param name="parentId">شناسه دیتابیسی حساب والد - اگر مقدار نداشته باشد حساب جدید
        /// در سطح کل پیشنهاد می شود</param>
        /// <returns>مدل نمایشی کلی حساب </returns>
        public async Task<AccountFullDataViewModel> GetNewChildAccountAsync(int? parentId)
        {
            var repository = UnitOfWork.GetAsyncRepository<Account>();
            var parent = await repository.GetByIDAsync(parentId ?? 0);
            if (parentId > 0 && parent == null)
            {
                return null;
            }

            var fullConfig = await Config.GetViewTreeConfigByViewAsync(ViewName.Account);
            var treeConfig = fullConfig.Current;
            if (parent != null && parent.Level + 1 == treeConfig.MaxDepth)
            {
                return new AccountFullDataViewModel()
                {
                    Account = new AccountViewModel() { Level = -1 },
                    CustomerTaxInfo = null
                };
            }

            var childrenCodes = await GetChildrenCodesAsync(parentId);
            string newCode = GetNewAccountCode(parent, childrenCodes, treeConfig);
            return new AccountFullDataViewModel()
            {
                Account = GetNewChildAccount(parent, newCode, treeConfig),
                CustomerTaxInfo = null
            };
        }

        /// <summary>
        /// به روش آسنکرون، مجموعه ای از سرفصل های حسابداری در سطح کل را خوانده و برمی گرداند
        /// </summary>
        /// <returns>مجموعه ای از مدل نمایشی خلاصه سرفصل های حسابداری در سطح کل</returns>
        public async Task<IList<AccountItemBriefViewModel>> GetLedgerAccountsAsync()
        {
            var accounts = await Repository
                .GetAllQuery<Account>(ViewName.Account, acc => acc.Children)
                .Where(acc => acc.ParentId == null)
                .Select(acc => Mapper.Map<AccountItemBriefViewModel>(acc))
                .ToListAsync();
            return accounts;
        }

        /// <summary>
        /// به روش آسنکرون، مجموعه ای از سرفصل های حسابداری یک گروه حساب را خوانده و برمی گرداند
        /// </summary>
        /// <param name="groupId">شناسه یکتای دیتابیسی گروه حساب</param>
        /// <returns>مجموعه ای از مدل نمایشی خلاصه سرفصل های حسابداری یک گروه حساب</returns>
        public async Task<IList<AccountItemBriefViewModel>> GetLedgerAccountsByGroupIdAsync(int groupId)
        {
            var accounts = await Repository
                .GetAllQuery<Account>(ViewName.Account, acc => acc.Children)
                .Where(acc => acc.ParentId == null && acc.GroupId == groupId)
                .Select(acc => Mapper.Map<AccountItemBriefViewModel>(acc))
                .ToListAsync();
            return accounts;
        }

        /// <summary>
        /// به روش آسنکرون، مجموعه ای از سرفصل های حسابداری زیرمجموعه یک سرفصل حسابداری مشخص را خوانده و برمی گرداند
        /// </summary>
        /// <param name="accountId">شناسه یکی از سرفصل های حسابداری موجود</param>
        /// <returns>مجموعه ای از سرفصل های حسابداری زیرمجموعه</returns>
        public async Task<IList<AccountItemBriefViewModel>> GetAccountChildrenAsync(int accountId)
        {
            var children = await Repository
                .GetAllQuery<Account>(ViewName.Account, acc => acc.Children)
                .Where(acc => acc.ParentId == accountId)
                .Select(acc => Mapper.Map<AccountItemBriefViewModel>(acc))
                .ToListAsync();
            return children;
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات یک حساب را در محل ذخیره ایجاد یا اصلاح می کند
        /// </summary>
        /// <param name="accountFullView">اطلاعات مالیاتی طرف حساب مورد نظر برای ایجاد یا اصلاح</param>
        /// <returns>اطلاعات نمایشی حساب ایجاد یا اصلاح شده</returns>
        public async Task<AccountFullDataViewModel> SaveAccountAsync(AccountFullDataViewModel accountFullView)
        {
            Verify.ArgumentNotNull(accountFullView, "accountFullView");
            Account account = default(Account);
            CustomerTaxInfoViewModel customerTax = default(CustomerTaxInfoViewModel);
            var repository = UnitOfWork.GetAsyncRepository<Account>();
            var accountView = accountFullView.Account;
            if (accountView.Id == 0)
            {
                account = Mapper.Map<Account>(accountView);
                await InsertAsync(repository, account);
                await UpdateLevelUsageAsync(account.Level);
                await InsertAccountCurrencyAsync(accountView, account);
            }
            else
            {
                account = await repository.GetByIDAsync(accountView.Id);
                if (account != null)
                {
                    bool needsCascade = (account.Code != accountView.Code);
                    await UpdateAsync(repository, account, accountView);
                    if (needsCascade)
                    {
                        await CascadeUpdateFullCodeAsync(account.Id);
                    }

                    await UpdateAccountCurrencyAsync(accountView, account);

                    if (accountFullView.CustomerTaxInfo != null)
                    {
                        customerTax = await _customerTaxInfo.SaveCustomerTaxInfoAsync(accountFullView.CustomerTaxInfo);
                    }
                }
            }

            return new AccountFullDataViewModel()
            {
                Account = Mapper.Map<AccountViewModel>(account),
                CustomerTaxInfo = customerTax
            };
        }

        /// <summary>
        /// به روش آسنکرون، حساب مشخص شده با شناسه عددی را از محل ذخیره حذف می کند
        /// </summary>
        /// <param name="accountId">شناسه عددی حساب مورد نظر برای حذف</param>
        public async Task DeleteAccountAsync(int accountId)
        {
            var repository = UnitOfWork.GetAsyncRepository<Account>();
            var account = await repository.GetByIDWithTrackingAsync(accountId, acc => acc.AccountCurrencies, acc => acc.CustomerTaxInfo);
            if (account != null)
            {
                await _customerTaxInfo.DeleteCustomerTaxInfoAsync(account.CustomerTaxInfo.Id);

                account.AccountCurrencies.Clear();
                await DeleteAsync(repository, account);
                await UpdateLevelUsageAsync(account.Level);
            }
        }

        /// <summary>
        /// به روش آسنکرون، حساب های مشخص شده با شناسه عددی را از محل ذخیره حذف می کند
        /// </summary>
        /// <param name="accountIds">مجموعه ای از شناسه های عددی حساب های مورد نظر برای حذف</param>
        public async Task DeleteAccountsAsync(IList<int> accountIds)
        {
            var repository = UnitOfWork.GetAsyncRepository<Account>();
            int level = 0;
            foreach (int accountId in accountIds)
            {
                var account = await repository.GetByIDWithTrackingAsync(accountId, acc => acc.AccountCurrencies, acc => acc.CustomerTaxInfo);
                if (account != null)
                {
                    await _customerTaxInfo.DeleteCustomerTaxInfoAsync(account.CustomerTaxInfo.Id);

                    level = Math.Max(level, account.Level);
                    account.AccountCurrencies.Clear();
                    await DeleteNoLogAsync(repository, account);
                }
            }

            await UpdateLevelUsageAsync(level);
            await OnEntityGroupDeleted(accountIds);
        }

        /// <summary>
        /// به روش آسنکرون، مشخص می کند که آیا کد حساب مورد نظر تکراری است یا نه
        /// </summary>
        /// <param name="accountViewModel">مدل نمایشی حساب مورد نظر</param>
        /// <returns>اگر کد حساب تکراری باشد مقدار "درست" و در غیر این صورت مقدار "نادرست" برمی گرداند</returns>
        /// <remarks>اگر کد حساب در حسابی با شناسه یکتای همین حساب به کار رفته باشد (مثلاً در حالتی که
        /// یک حساب در حالت ویرایش است) در این صورت مقدار "نادرست" را برمی گرداند</remarks>
        public async Task<bool> IsDuplicateAccountAsync(AccountViewModel accountViewModel)
        {
            Verify.ArgumentNotNull(accountViewModel, "accountViewModel");
            var repository = UnitOfWork.GetAsyncRepository<Account>();
            int count = await repository
                .GetCountByCriteriaAsync(acc => acc.Id != accountViewModel.Id
                    && acc.FullCode == accountViewModel.FullCode);
            return count > 0;
        }

        /// <summary>
        /// به روش آسنکرون، با توجه به مجموعه حساب پدر حساب، مشخص میکند که حساب قابلیت اضافه شدن دارد یا نه
        /// </summary>
        /// <param name="accountViewModel">مدل نمایشی حساب مورد نظر</param>
        /// <returns>اگر حساب شرایط اضافه شدن با توجه به مجموعه حساب والد را داشته باشد مقدار"درست"
        /// و در غیر این صورت مقدار "نادرست" را برمیگرداند</returns>
        public async Task<bool> IsAccountCollectionValidAsync(AccountViewModel accountViewModel)
        {
            Verify.ArgumentNotNull(accountViewModel, "accountViewModel");
            var accCollectionRepository = UnitOfWork.GetAsyncRepository<AccountCollectionAccount>();
            var accCollectionAccounts = await accCollectionRepository
                .GetByCriteriaAsync(col => col.AccountId == accountViewModel.ParentId, ac => ac.Collection);

            if (accCollectionAccounts.Count() == 0)
            {
                return false;
            }
            else
            {
                var accCollections = accCollectionAccounts.Select(ac => ac.Collection).Distinct();
                return accCollections.Where(f => f.TypeLevel == (int)TypeLevel.LeafAccounts).Count() > 0;
            }
        }

        /// <summary>
        /// به روش آسنکرون، مشخص می کند که آیا حساب انتخاب شده توسط رکوردهای اطلاعاتی دیگر
        /// در حال استفاده است یا نه
        /// </summary>
        /// <param name="accountId">شناسه یکتای یکی از حساب های موجود</param>
        /// <returns>در حالتی که حساب مشخص شده در حال استفاده باشد مقدار "درست" و در غیر این صورت
        /// مقدار "نادرست" را برمی گرداند</returns>
        public async Task<bool> IsUsedAccountAsync(int accountId)
        {
            var repository = UnitOfWork.GetAsyncRepository<VoucherLine>();
            var articleCount = await repository
                .GetCountByCriteriaAsync(art => art.Account.Id == accountId);
            return (articleCount > 0);
        }

        /// <summary>
        /// به روش آسنکرون، مشخص می کند که آیا حساب انتخاب شده توسط ارتباطات موجود برای بردار حساب
        /// در حال استفاده است یا نه
        /// </summary>
        /// <param name="accountId">شناسه یکتای یکی از حساب های موجود</param>
        /// <returns>در حالتی که حساب مشخص شده در حال استفاده باشد مقدار "درست" و در غیر این صورت
        /// مقدار "نادرست" را برمی گرداند</returns>
        public async Task<bool> IsRelatedAccountAsync(int accountId)
        {
            var accDetailRepository = UnitOfWork.GetAsyncRepository<AccountDetailAccount>();
            int relatedDetails = await accDetailRepository.GetCountByCriteriaAsync(
                ada => ada.AccountId == accountId);
            var accCenterRepository = UnitOfWork.GetAsyncRepository<AccountCostCenter>();
            int relatedCenters = await accCenterRepository.GetCountByCriteriaAsync(
                ac => ac.AccountId == accountId);
            var accProjectRepository = UnitOfWork.GetAsyncRepository<AccountProject>();
            int relatedProjects = await accProjectRepository.GetCountByCriteriaAsync(
                ap => ap.AccountId == accountId);

            return (relatedDetails > 0 || relatedCenters > 0 || relatedProjects > 0);
        }

        /// <summary>
        /// به روش آسنکرون، مشخص می کند که آیا حساب انتخاب شده دارای حساب زیرمجموعه هست یا نه
        /// </summary>
        /// <param name="accountId">شناسه یکتای یکی از حساب های موجود</param>
        /// <returns>در حالتی که حساب مشخص شده دارای حساب زیرمجموعه باشد مقدار "درست" و در غیر این صورت
        /// مقدار "نادرست" را برمی گرداند</returns>
        public async Task<bool?> HasChildrenAsync(int accountId)
        {
            bool? hasChildren = null;
            var repository = UnitOfWork.GetAsyncRepository<Account>();
            var account = await repository.GetByIDAsync(accountId, acc => acc.Children);
            if (account != null)
            {
                hasChildren = account.Children.Count > 0;
            }

            return hasChildren;
        }

        /// <summary>
        /// به روش آسنکرون، مشخص میکند که آیا حساب انتخاب شده در مجموعه حسابی وجود دارد یا نه
        /// </summary>
        /// <param name="accountId">شناسه یکتای یکی از حساب های موجود</param>
        /// <returns>در حالتی که حساب مشخص شده در کجکوعه حسابی باشد مقدار "درست" و در غیر این صورت
        /// مقدار "نادرست" را برمی گرداند</returns>
        public async Task<bool> IsUsedInAccountCollectionAsync(int accountId)
        {
            var repository = UnitOfWork.GetAsyncRepository<AccountCollectionAccount>();
            var accountCount = await repository
                .GetCountByCriteriaAsync(ac => ac.Account.Id == accountId);
            return (accountCount > 0);
        }

        /// <summary>
        /// به روش آسنکرون، مقدار فیلد FullCode والد هر حساب را برمیگرداند
        /// </summary>
        /// <param name="parentId">شناسه والد هر حساب</param>
        /// <returns>اگر حساب والد نداشته باشد مقدار خالی و اگر والد داشته باشد مقدار FullCode والد را برمیگرداند</returns>
        public async Task<string> GetAccountFullCodeAsync(int parentId)
        {
            var repository = UnitOfWork.GetAsyncRepository<Account>();
            var account = await repository.GetByIDAsync(parentId);
            if (account == null)
            {
                return string.Empty;
            }

            return account.FullCode;
        }

        /// <summary>
        /// به روش آسنکرون، تعداد کل حساب های ثبت شده را برمیگرداند
        /// </summary>
        /// <returns>تعداد کل حساب ها</returns>
        public async Task<int> GetAllAccountsCountAsync()
        {
            var repository = UnitOfWork.GetAsyncRepository<Account>();
            var query = repository.GetEntityQuery();
            return await query.CountAsync();
        }

        /// <summary>
        /// به روش آسنکرون، حساب با سایر مشخصات حساب را از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <param name="accountId">شناسه یکتای یکی از حساب های موجود</param>
        /// <returns></returns>
        public async Task<AccountFullDataViewModel> GetAccountFullDataAsync(int accountId)
        {
            AccountFullDataViewModel item = null;
            var repository = UnitOfWork.GetAsyncRepository<Account>();

            var account = await repository.GetByIDWithTrackingAsync(
                accountId,
                acc => acc.CustomerTaxInfo);

            if (account != null)
            {
                item = Mapper.Map<AccountFullDataViewModel>(account);
                item.Account.GroupId = GetAccountGroupId(repository, account);
                item.Account.CurrencyId = await GetAccountCurrencyIdAsync(account.Id);
            }

            if (!await IsCommercialDebtorAndCreditorAsync(accountId))
            {
                item.CustomerTaxInfo = null;
            }

            return item;
        }

        private async Task<bool> IsCommercialDebtorAndCreditorAsync(int accountId)
        {
            var repository = UnitOfWork.GetAsyncRepository<AccountCollectionAccount>();
            // 31: بدهکاران تجاری
            // 32: بستانکاران تجاری
            var collection = await repository.GetByCriteriaAsync(col => (col.CollectionId == 31 || col.CollectionId == 32) && col.AccountId == accountId);
            return collection.Count() > 0 ? true : false;
        }

        internal override int? EntityType
        {
            get { return (int)EntityTypeId.Account; }
        }

        /// <summary>
        /// آخرین تغییرات موجودیت را از مدل نمایشی به سطر اطلاعاتی موجود کپی می کند
        /// </summary>
        /// <param name="accountViewModel">مدل نمایشی شامل آخرین تغییرات</param>
        /// <param name="account">سطر اطلاعاتی موجود</param>
        protected override void UpdateExisting(AccountViewModel accountViewModel, Account account)
        {
            account.GroupId = accountViewModel.GroupId;
            account.Code = accountViewModel.Code;
            account.FullCode = accountViewModel.FullCode;
            account.Name = accountViewModel.Name;
            account.Level = accountViewModel.Level;
            account.Description = accountViewModel.Description;
            account.IsActive = accountViewModel.IsActive;
            account.IsCurrencyAdjustable = accountViewModel.IsCurrencyAdjustable;
            account.TurnoverMode = (short)Enum.Parse(typeof(TurnoverMode), accountViewModel.TurnoverMode);
        }

        /// <summary>
        /// اطلاعات خلاصه سطر اطلاعاتی داده شده را به صورت یک رشته متنی برمی گرداند
        /// </summary>
        /// <param name="entity">یکی از سطرهای اطلاعاتی موجود</param>
        /// <returns>اطلاعات خلاصه سطر اطلاعاتی داده شده به صورت رشته متنی</returns>
        protected override string GetState(Account entity)
        {
            return (entity != null)
                ? String.Format(
                    "Name : {1}{0}Code : {2}{0}FullCode : {3}{0}Description : {4}",
                    Environment.NewLine, entity.Name, entity.Code, entity.FullCode, entity.Description)
                : null;
        }

        private ISecureRepository Repository
        {
            get { return _system.Repository; }
        }

        private IConfigRepository Config
        {
            get { return _system.Config; }
        }

        private static int GetAccountGroupId(IRepository<Account> repository, Account account)
        {
            repository.LoadReference(account, acc => acc.Parent);
            var parent = account;
            while (parent.ParentId != null)
            {
                repository.LoadReference(parent, acc => acc.Parent);
                parent = parent.Parent;
            }

            return parent.GroupId ?? 0;
        }

        private static string GetNewAccountCode(
            Account parent, IEnumerable<string> existingCodes, ViewTreeConfig treeConfig)
        {
            int childLevel = (parent != null) ? parent.Level + 1 : 0;
            int codeLength = treeConfig.Levels[childLevel].CodeLength;
            string format = String.Format("D{0}", codeLength);
            var maxCode = (long)Math.Pow(10, codeLength) - 1;
            var lastCode = (existingCodes.Count() > 0) ? Int64.Parse(existingCodes.Max()) : 0;
            var newCode = Math.Min(lastCode + 1, maxCode);
            return newCode.ToString(format);
        }

        /// <summary>
        /// به روش آسنکرون، وضعیت استفاده از یکی از سطوح درختی حساب را در دیتابیس بروزرسانی می کند
        /// </summary>
        /// <param name="level">شماره سطح مورد نظر</param>
        /// <remarks>قابل توجه است که در این متد هیچگونه فیلتری روی دوره مالی، شعبه یا سطرهای قابل دسترسی صورت نمی گیرد.
        /// این به این معنی است که اطلاعات سطح مورد نظر در هر شعبه یا دوره مالی ممکن است ایجاد شده باشد. </remarks>
        private async Task UpdateLevelUsageAsync(int level)
        {
            var repository = UnitOfWork.GetAsyncRepository<Account>();
            int count = await repository.GetCountByCriteriaAsync(acc => acc.Level == level);
            await Config.SaveTreeLevelUsageAsync(ViewName.Account, level, count);
        }

        private async Task CascadeUpdateFullCodeAsync(int accountId)
        {
            var repository = UnitOfWork.GetAsyncRepository<Account>();
            var account = await repository.GetByIDAsync(accountId, acc => acc.Children);
            if (account != null)
            {
                foreach (var child in account.Children)
                {
                    child.FullCode = account.FullCode + child.Code;
                    repository.Update(child);
                    await UnitOfWork.CommitAsync();
                    await CascadeUpdateFullCodeAsync(child.Id);
                }
            }
        }

        private async Task<IList<string>> GetChildrenCodesAsync(int? parentId)
        {
            var repository = UnitOfWork.GetAsyncRepository<Account>();
            return await repository
                .GetEntityQuery()
                .Where(acc => acc.ParentId == parentId)
                .Select(acc => acc.Code)
                .ToListAsync();
        }

        private AccountViewModel GetNewChildAccount(
            Account parent, string newCode, ViewTreeConfig treeConfig)
        {
            var childAccount = new AccountViewModel()
            {
                Code = newCode,
                ParentId = parent?.Id,
                FiscalPeriodId = UserContext.FiscalPeriodId,
                BranchId = UserContext.BranchId
            };
            childAccount.FullCode = (parent != null)
                ? parent.FullCode + childAccount.Code
                : childAccount.Code;
            if (parent != null)
            {
                childAccount.Level = (short)((parent.Level + 1 < treeConfig.MaxDepth)
                    ? parent.Level + 1
                    : -1);
            }

            return childAccount;
        }

        private async Task InsertAccountCurrencyAsync(AccountViewModel accountViewModel, Account account)
        {
            if (accountViewModel.CurrencyId.HasValue)
            {
                var repository = UnitOfWork.GetAsyncRepository<AccountCurrency>();
                var accountCurrency = new AccountCurrency()
                {
                    Id = 0,
                    AccountId = account.Id,
                    CurrencyId = accountViewModel.CurrencyId.Value,
                    BranchId = UserContext.BranchId,
                };

                repository.Insert(accountCurrency);
                await UnitOfWork.CommitAsync();
            }
        }

        private async Task UpdateAccountCurrencyAsync(AccountViewModel accountViewModel, Account account)
        {
            var repository = UnitOfWork.GetAsyncRepository<AccountCurrency>();
            var accountCurrency = await repository.GetSingleByCriteriaAsync(accCurr => accCurr.AccountId == account.Id && accCurr.BranchId == UserContext.BranchId);

            if (accountViewModel.CurrencyId.HasValue)
            {
                if (accountCurrency != null)
                {
                    accountCurrency.CurrencyId = accountViewModel.CurrencyId.Value;
                    repository.Update(accountCurrency);
                }
                else
                {
                    var accCurrency = new AccountCurrency()
                    {
                        Id = 0,
                        AccountId = account.Id,
                        CurrencyId = accountViewModel.CurrencyId.Value,
                        BranchId = UserContext.BranchId,
                    };

                    repository.Insert(accCurrency);
                }
            }
            else
            {
                if (accountCurrency != null)
                {
                    repository.Delete(accountCurrency);
                }
            }

            await UnitOfWork.CommitAsync();
        }

        private async Task<int?> GetAccountCurrencyIdAsync(int accountId)
        {
            var accountCurrencyRepository = UnitOfWork.GetAsyncRepository<AccountCurrency>();
            var accCurrency = await accountCurrencyRepository.GetSingleByCriteriaAsync(
                accCurr => accCurr.AccountId == accountId && accCurr.BranchId == UserContext.BranchId);

            if (accCurrency != null)
            {
                return accCurrency.CurrencyId;
            }

            return null;
        }

        private readonly ISystemRepository _system;
        private readonly ICustomerTaxInfoRepository _customerTaxInfo;
    }
}
