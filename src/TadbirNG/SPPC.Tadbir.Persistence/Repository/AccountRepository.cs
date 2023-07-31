using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPPC.Framework.Common;
using SPPC.Framework.Persistence;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Model.Finance;
using SPPC.Tadbir.Persistence.Utility;
using SPPC.Tadbir.Resources;
using SPPC.Tadbir.Utility;
using SPPC.Tadbir.ViewModel.Finance;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای مدیریت اطلاعات سرفصل های حسابداری را تعریف می کند.
    /// </summary>
    public class AccountRepository : ActiveStateRepository<Account, AccountViewModel>, IAccountRepository
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="context">امکانات مشترک مورد نیاز را برای عملیات دیتابیسی فراهم می کند</param>
        /// <param name="system">امکانات مورد نیاز در دیتابیس های سیستمی را فراهم می کند</param>
        /// <param name="customerTaxInfo">امکانات مورد نیاز برای اطلاعات مالی طرف حساب ها را فراهم میکند</param>
        /// <param name="accountOwner"></param>
        public AccountRepository(IRepositoryContext context, ISystemRepository system,
            ICustomerTaxInfoRepository customerTaxInfo, IAccountOwnerRepository accountOwner)
            : base(context, system?.Logger)
        {
            _system = system;
            _customerTaxInfo = customerTaxInfo;
            _accountOwner = accountOwner;
            var fullConfig = _system.Config.GetViewTreeConfigByViewAsync(ViewId.Account).Result;
            _treeUtility = new TreeEntityUtility<Account, AccountViewModel>(context, fullConfig.Current);
        }

        /// <inheritdoc/>
        public async Task<PagedList<AccountViewModel>> GetAccountsAsync(GridOptions gridOptions)
        {
            Verify.ArgumentNotNull(gridOptions, nameof(gridOptions));
            var accounts = new List<AccountViewModel>();
            if (gridOptions.Operation != (int)OperationId.Print)
            {
                accounts = await Repository
                    .GetAllQuery<Account>(ViewId.Account, acc => acc.Children)
                    .OrderBy(item => item.FullCode)
                    .Select(item => Mapper.Map<AccountViewModel>(item))
                    .ToListAsync();
                await UpdateInactiveItemsAsync(accounts);
                Array.ForEach(accounts.ToArray(), acc => Localize(acc));
            }

            await ReadAsync(gridOptions);
            return new PagedList<AccountViewModel>(accounts, gridOptions);
        }

        /// <inheritdoc/>
        public async Task<PagedList<AccountViewModel>> GetAccountsLookupAsync(GridOptions gridOptions)
        {
            var accounts = await Repository
                .GetAllQuery<Account>(ViewId.Account, acc => acc.Children)
                .Select(item => Mapper.Map<AccountViewModel>(item))
                .ToListAsync();
            return new PagedList<AccountViewModel>(accounts, gridOptions);
        }

        /// <inheritdoc/>
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
                bool isDeactivated = await IsDeactivatedAsync(item.Id);
                item.State = isDeactivated
                    ? Context.Localize(AppStrings.Inactive)
                    : Context.Localize(AppStrings.Active);
            }

            return item;
        }

        /// <inheritdoc/>
        public async Task<AccountFullDataViewModel> GetAccountFullDataAsync(int accountId)
        {
            AccountFullDataViewModel item = null;
            var repository = UnitOfWork.GetAsyncRepository<Account>();

            var account = await repository.GetByIDWithTrackingAsync(
                accountId,
                acc => acc.Children,
                acc => acc.CustomerTaxInfo,
                acc => acc.AccountOwner.AccountHolders);

            if (account != null)
            {
                item = Mapper.Map<AccountFullDataViewModel>(account);
                item.Account.GroupId = GetAccountGroupId(repository, account);
                item.Account.CurrencyId = await GetAccountCurrencyIdAsync(account.Id);
                bool isDeactivated = await IsDeactivatedAsync(item.Account.Id);
                item.Account.State = isDeactivated
                    ? Context.Localize(AppStrings.Inactive)
                    : Context.Localize(AppStrings.Active);
            }

            if (!await IsCommercialDebtorAndCreditorAsync(accountId))
            {
                item.CustomerTaxInfo = null;
            }

            if (!await IsBankSubSetAsync(accountId))
            {
                item.AccountOwner = null;
            }

            return item;
        }

        /// <inheritdoc/>
        public async Task<IList<AccountItemBriefViewModel>> GetLedgerAccountsAsync()
        {
            var accounts = await Repository
                .GetAllQuery<Account>(ViewId.Account, acc => acc.Children)
                .Where(acc => acc.ParentId == null)
                .Select(acc => Mapper.Map<AccountItemBriefViewModel>(acc))
                .ToListAsync();
            return accounts;
        }

        /// <inheritdoc/>
        public async Task<IList<AccountItemBriefViewModel>> GetLedgerAccountsByGroupIdAsync(int groupId)
        {
            var accounts = await Repository
                .GetAllQuery<Account>(ViewId.Account, acc => acc.Children)
                .Where(acc => acc.ParentId == null && acc.GroupId == groupId)
                .Select(acc => Mapper.Map<AccountItemBriefViewModel>(acc))
                .ToListAsync();
            return accounts;
        }

        /// <inheritdoc/>
        public async Task<IList<AccountItemBriefViewModel>> GetAccountChildrenAsync(int accountId)
        {
            var children = await Repository
                .GetAllQuery<Account>(ViewId.Account, acc => acc.Children)
                .Where(acc => acc.ParentId == accountId)
                .Select(acc => Mapper.Map<AccountItemBriefViewModel>(acc))
                .ToListAsync();
            return children;
        }

        /// <inheritdoc/>
        public async Task<AccountFullDataViewModel> SaveAccountAsync(AccountFullDataViewModel accountFullView)
        {
            Verify.ArgumentNotNull(accountFullView, "accountFullView");
            Account account;
            var customerTax = default(CustomerTaxInfoViewModel);
            var accountOwner = default(AccountOwnerViewModel);

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

                    if (accountFullView.AccountOwner != null)
                    {
                        accountOwner = await _accountOwner.SaveAccountOwnerAsync(accountFullView.AccountOwner);
                    }
                }
            }

            return new AccountFullDataViewModel()
            {
                Account = Mapper.Map<AccountViewModel>(account),
                CustomerTaxInfo = customerTax,
                AccountOwner = accountOwner
            };
        }

        /// <inheritdoc/>
        public async Task DeleteAccountAsync(int accountId)
        {
            var repository = UnitOfWork.GetAsyncRepository<Account>();
            var account = await repository.GetByIDWithTrackingAsync(
                accountId,
                acc => acc.AccountCurrencies,
                acc => acc.CustomerTaxInfo,
                acc => acc.AccountOwner);

            if (account != null)
            {
                if (account.CustomerTaxInfo != null && account.CustomerTaxInfo.Id > 0)
                {
                    await _customerTaxInfo.DeleteCustomerTaxInfoAsync(account.CustomerTaxInfo.Id);
                }

                if (account.AccountOwner != null && account.AccountOwner.Id > 0)
                {
                    await _accountOwner.DeleteAccountOwnerAsync(account.AccountOwner.Id);
                }

                account.AccountCurrencies.Clear();
                await OnDeleteItemAsync(account.Id);
                await DeleteAsync(repository, account);
                await UpdateLevelUsageAsync(account.Level);
            }
        }

        /// <inheritdoc/>
        public async Task DeleteAccountsAsync(IList<int> accountIds)
        {
            var repository = UnitOfWork.GetAsyncRepository<Account>();
            int level = 0;
            foreach (int accountId in accountIds)
            {
                var account = await repository.GetByIDWithTrackingAsync(
                    accountId,
                    acc => acc.AccountCurrencies,
                    acc => acc.CustomerTaxInfo,
                    acc => acc.AccountOwner);

                if (account != null)
                {
                    if (account.CustomerTaxInfo != null)
                    {
                        await _customerTaxInfo.DeleteCustomerTaxInfoAsync(account.CustomerTaxInfo.Id);
                    }

                    if (account.AccountOwner != null)
                    {
                        await _accountOwner.DeleteAccountOwnerAsync(account.AccountOwner.Id);
                    }

                    level = Math.Max(level, account.Level);
                    account.AccountCurrencies.Clear();
                    await OnDeleteItemAsync(account.Id);
                    await DeleteNoLogAsync(repository, account);
                }
            }

            await UpdateLevelUsageAsync(level);
            await OnEntityGroupDeleted(accountIds);
        }

        /// <inheritdoc/>
        public async Task<bool> IsAssociationChildAccountAsync(AccountViewModel account)
        {
            bool isInvalid = false;
            if (account.Level > 0)
            {
                var repository = UnitOfWork.GetAsyncRepository<Account>();
                var parent = await repository.GetByIDWithTrackingAsync((int)account.ParentId);
                var groupId = GetAccountGroupId(repository, parent);

                var groupRepository = UnitOfWork.GetAsyncRepository<AccountGroup>();
                var accountGroup = await groupRepository.GetByIDAsync(groupId);
                isInvalid = (accountGroup.Category == AppStrings.CategoryAssociation);
            }

            return isInvalid;
        }

        /// <inheritdoc/>
        public async Task<bool> CanHaveChildrenAsync(int accountId)
        {
            var repository = UnitOfWork.GetAsyncRepository<AccountCollectionAccount>();
            var collectionAccounts = await repository
                .GetByCriteriaAsync(aca => aca.AccountId == accountId, aca => aca.Collection);

            if (collectionAccounts.Count == 0)
            {
                return true;
            }
            else
            {
                var collections = collectionAccounts
                    .Select(aca => aca.Collection)
                    .Distinct();
                return !collections
                    .Where(coll => coll.TypeLevel == (int)TypeLevel.LeafAccounts)
                    .Any();
            }
        }

        /// <inheritdoc/>
        public async Task<bool> IsUsedAccountAsync(int accountId)
        {
            var repository = UnitOfWork.GetAsyncRepository<VoucherLine>();
            var articleCount = await repository
                .GetCountByCriteriaAsync(art => art.AccountId == accountId);
            return (articleCount > 0);
        }

        /// <inheritdoc/>
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

            return relatedDetails > 0 || relatedCenters > 0 || relatedProjects > 0;
        }

        /// <inheritdoc/>
        public async Task<bool> IsUsedInAccountCollectionAsync(int accountId)
        {
            var repository = UnitOfWork.GetAsyncRepository<AccountCollectionAccount>();
            var accountCount = await repository
                .GetCountByCriteriaAsync(aca => aca.AccountId == accountId);
            return (accountCount > 0);
        }

        /// <inheritdoc/>
        public async Task<int> GetAllAccountsCountAsync()
        {
            var repository = UnitOfWork.GetAsyncRepository<Account>();
            var query = repository.GetEntityQuery();
            return await query.CountAsync();
        }

        #region Common TreeEntity Operations

        /// <inheritdoc/>
        public async Task<AccountFullDataViewModel> GetNewChildAccountAsync(int? parentId)
        {
            var child = await _treeUtility.GetNewChildItemAsync(parentId);
            return child != null
                ? new AccountFullDataViewModel() { Account = child }
                : null;
        }

        /// <inheritdoc/>
        public async Task<string> GetAccountFullCodeAsync(int accountId)
        {
            return await _treeUtility.GetItemFullCodeAsync(accountId);
        }

        /// <inheritdoc/>
        public async Task<bool> IsDuplicateFullCodeAsync(AccountViewModel account)
        {
            return await _treeUtility.IsDuplicateFullCodeAsync(account);
        }

        /// <inheritdoc/>
        public async Task<bool> IsDuplicateNameAsync(AccountViewModel account)
        {
            return await _treeUtility.IsDuplicateNameAsync(account);
        }

        /// <inheritdoc/>
        public async Task<bool?> HasChildrenAsync(int accountId)
        {
            return await _treeUtility.HasChildrenAsync(accountId);
        }

        #endregion

        internal override int? EntityType
        {
            get { return (int)EntityTypeId.Account; }
        }

        /// <inheritdoc/>
        protected override void UpdateExisting(AccountViewModel accountViewModel, Account account)
        {
            account.GroupId = accountViewModel.GroupId;
            account.Code = accountViewModel.Code;
            account.FullCode = accountViewModel.FullCode;
            account.Name = accountViewModel.Name;
            account.Level = accountViewModel.Level;
            account.Description = accountViewModel.Description;
            account.IsCurrencyAdjustable = accountViewModel.IsCurrencyAdjustable;
            account.TurnoverMode = (short)Enum.Parse(typeof(TurnoverMode), accountViewModel.TurnoverMode);
        }

        /// <inheritdoc/>
        protected override string GetState(Account entity)
        {
            return (entity != null)
                ? String.Format(
                    "{0} : {1} , {2} : {3} , {4} : {5} , {6} : {7}",
                    AppStrings.Name, entity.Name, AppStrings.Code, entity.Code,
                    AppStrings.FullCode, entity.FullCode, AppStrings.Description, entity.Description)
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

        private async Task<bool> IsCommercialDebtorAndCreditorAsync(int accountId)
        {
            var repository = UnitOfWork.GetAsyncRepository<AccountCollectionAccount>();
            int count = await repository.GetCountByCriteriaAsync(
                col => (col.CollectionId == (int)AccountCollectionId.BusinessDebtors
                    || col.CollectionId == (int)AccountCollectionId.BusinessCreditors)
                    && col.AccountId == accountId);
            return count > 0;
        }

        private async Task<bool> IsBankSubSetAsync(int accountId)
        {
            var repository = UnitOfWork.GetAsyncRepository<AccountCollectionAccount>();
            int count = await repository.GetCountByCriteriaAsync(
                col => col.CollectionId == (int)AccountCollectionId.Bank && col.AccountId == accountId);
            return count > 0;
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
            await Config.SaveTreeLevelUsageAsync(ViewId.Account, level, count);
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
            var accountCurrency = await repository.GetSingleByCriteriaAsync(
                accCurr => accCurr.AccountId == account.Id && accCurr.BranchId == UserContext.BranchId);

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
            var repository = UnitOfWork.GetAsyncRepository<AccountCurrency>();
            var accCurrency = await repository.GetSingleByCriteriaAsync(
                accCurr => accCurr.AccountId == accountId && accCurr.BranchId == UserContext.BranchId);

            int? currencyId = null;
            if (accCurrency != null)
            {
                currencyId = accCurrency.CurrencyId;
            }

            return currencyId;
        }

        private void Localize(AccountViewModel account)
        {
            account.TurnoverMode = Context.Localize(account.TurnoverMode);
            account.State = Context.Localize(account.State);
        }

        private readonly ISystemRepository _system;
        private readonly ICustomerTaxInfoRepository _customerTaxInfo;
        private readonly IAccountOwnerRepository _accountOwner;
        private readonly TreeEntityUtility<Account, AccountViewModel> _treeUtility;
    }
}
