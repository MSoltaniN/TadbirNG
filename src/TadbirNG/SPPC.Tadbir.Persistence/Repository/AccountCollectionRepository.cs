using Microsoft.EntityFrameworkCore;
using SPPC.Framework.Common;
using SPPC.Framework.Persistence;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Model.Corporate;
using SPPC.Tadbir.Model.Finance;
using SPPC.Tadbir.Resources;
using SPPC.Tadbir.ViewModel.Finance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای مدیریت مجموعه حساب را تعریف می کند.
    /// </summary>
    public class AccountCollectionRepository
        : EntityLoggingRepository<AccountCollectionAccount, AccountCollectionAccountViewModel>, IAccountCollectionRepository
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="context">امکانات مشترک مورد نیاز را برای عملیات دیتابیسی فراهم می کند</param>
        /// <param name="system">امکانات مورد نیاز در دیتابیس های سیستمی را فراهم می کند</param>
        public AccountCollectionRepository(IRepositoryContext context, ISystemRepository system)
            : base(context, system?.Logger)
        {
        }

        /// <inheritdoc/>
        public async Task<IList<AccountCollectionCategoryViewModel>> GetCollectionCategoriesAsync()
        {
            var repository = UnitOfWork.GetAsyncRepository<AccountCollectionCategory>();
            var categories = await repository
                .GetEntityQuery(cat => cat.AccountCollections)
                .Select(cat => Mapper.Map<AccountCollectionCategoryViewModel>(cat))
                .ToListAsync();
            return categories;
        }

        /// <inheritdoc/>
        public async Task<IList<AccountCollectionAccountViewModel>> GetCollectionAccountsAsync(
            int collectionId, GridOptions gridOptions)
        {
            Verify.EnumValueIsDefined(typeof(AccountCollectionId), nameof(collectionId), collectionId);
            var accounts = await GetInheritedAccountsAsync(collectionId, UserContext.BranchId);
            if (gridOptions.ListChanged)
            {
                await LogCollectionOperationAsync(OperationId.View, collectionId);
            }

            return accounts;
        }

        /// <inheritdoc/>
        public async Task AddCollectionAccountsAsync(
            int collectionId, IList<AccountCollectionAccountViewModel> accounts)
        {
            var branchAccounts = accounts
                .Where(aca => aca.BranchId == UserContext.BranchId)
                .ToList();
            var repository = UnitOfWork.GetAsyncRepository<AccountCollectionAccount>();
            var existing = await repository.GetByCriteriaAsync(aca =>
                aca.CollectionId == collectionId &&
                aca.BranchId == UserContext.BranchId &&
                aca.FiscalPeriodId <= UserContext.FiscalPeriodId);

            if (existing.Count > 0)
            {
                RemoveInaccessibleAccountCollections(repository, existing, branchAccounts);
            }

            AddNewAccountCollections(repository, existing, branchAccounts);
            await UnitOfWork.CommitAsync();
            await LogCollectionOperationAsync(OperationId.Save, collectionId);
        }

        /// <inheritdoc/>
        public async Task<bool> CanBranchManageCollectionAsync(int branchId, int collectionId)
        {
            bool canManage = true;
            var repository = UnitOfWork.GetAsyncRepository<AccountCollection>();
            var collection = await repository.GetByIDAsync(collectionId);
            if (collection != null && !collection.MultiSelect)
            {
                var branchRepository = UnitOfWork.GetAsyncRepository<Branch>();
                var top = await branchRepository.GetSingleByCriteriaAsync(br => br.ParentId == null);
                canManage = top.Id == branchId;
            }

            return canManage;
        }

        /// <inheritdoc/>
        public async Task<IList<AccountItemBriefViewModel>> GetCashAndBankAccountsAsync()
        {
            var repository = UnitOfWork.GetAsyncRepository<AccountCollectionAccount>();
            var cashBankCollectionIds = new int[] { (int)AccountCollectionId.Bank, (int)AccountCollectionId.CashFund };
            var userBranchId = UserContext.BranchId;
            var userFiscalPeriodId = UserContext.FiscalPeriodId;
            var cashBankAccounts = await repository.GetEntityQuery()
                .Include(aca => aca.Account)
                .Where(aca =>
                    cashBankCollectionIds.Contains(aca.CollectionId) &&
                    aca.BranchId == userBranchId &&
                    aca.FiscalPeriodId == userFiscalPeriodId)
                .Select(aca => Mapper.Map<AccountItemBriefViewModel>(aca))
                .ToListAsync();
            return cashBankAccounts;
        }


        internal override int? EntityType
        {
            get { return (int)EntityTypeId.AccountCollectionAccount; }
        }

        private async Task<IList<AccountCollectionAccountViewModel>> GetInheritedAccountsAsync(
            int collectionId, int branchId)
        {
            var accounts = new List<AccountCollectionAccountViewModel>();
            var branchRepository = UnitOfWork.GetAsyncRepository<Branch>();
            var branch = await branchRepository.GetByIDWithTrackingAsync(branchId);
            var currentBranch = branch;
            var repository = UnitOfWork.GetAsyncRepository<AccountCollectionAccount>();
            while (currentBranch != null)
            {
                var collectionAccounts = await repository
                    .GetEntityQuery()
                    .Include(aca => aca.Account)
                    .Where(aca => aca.FiscalPeriodId <= UserContext.FiscalPeriodId &&
                        aca.BranchId == currentBranch.Id &&
                        aca.CollectionId == collectionId)
                    .Select(aca => Mapper.Map<AccountCollectionAccountViewModel>(aca))
                    .ToListAsync();
                if (collectionAccounts.Count > 0)
                {
                    accounts.AddRange(collectionAccounts);
                }

                branchRepository.LoadReference(currentBranch, br => br.Parent);
                currentBranch = currentBranch.Parent;
            }

            return accounts;
        }

        private void RemoveInaccessibleAccountCollections(
            IAsyncRepository<AccountCollectionAccount> repository,
            IList<AccountCollectionAccount> existing,
            IList<AccountCollectionAccountViewModel> accounts)
        {
            var branchRepository = UnitOfWork.GetAsyncRepository<Branch>();
            var accountItems = accounts
                .Select(item => item.AccountId);
            var removedItems = existing
                .Where(item => !accountItems.Contains(item.AccountId));
            foreach (var item in removedItems)
            {
                repository.Delete(item);
            }
        }

        private void AddNewAccountCollections(
            IAsyncRepository<AccountCollectionAccount> repository,
            IList<AccountCollectionAccount> existing,
            IList<AccountCollectionAccountViewModel> accounts)
        {
            var existingIds = existing
                .Select(item => item.AccountId);
            var newItems = accounts
                .Where(item => !existingIds.Contains(item.AccountId));
            foreach (var item in newItems)
            {
                repository.Insert(new AccountCollectionAccount()
                {
                    AccountId = item.AccountId,
                    CollectionId = item.CollectionId,
                    BranchId = UserContext.BranchId,
                    FiscalPeriodId = UserContext.FiscalPeriodId
                });
            }
        }

        private async Task LogCollectionOperationAsync(OperationId operation, int collectionId)
        {
            var repository = UnitOfWork.GetAsyncRepository<AccountCollection>();
            string collectionName = await repository
                .GetEntityQuery()
                .Where(coll => coll.Id == collectionId)
                .Select(coll => coll.Name)
                .FirstOrDefaultAsync();
            if (!String.IsNullOrEmpty(collectionName))
            {
                string template = Context.Localize(AppStrings.AccountCollectionItems);
                OnEntityAction(operation);
                Log.EntityName = collectionName;
                Log.Description = String.Format(template, collectionName);
                await TrySaveLogAsync();
            }
        }
    }
}
