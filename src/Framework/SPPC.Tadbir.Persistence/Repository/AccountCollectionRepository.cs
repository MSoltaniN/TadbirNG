using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPPC.Framework.Persistence;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Model.Corporate;
using SPPC.Tadbir.Model.Finance;
using SPPC.Tadbir.Resources;
using SPPC.Tadbir.Values;
using SPPC.Tadbir.ViewModel.Finance;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای مدیریت مجموعه حساب را تعریف می کند.
    /// </summary>
    public class AccountCollectionRepository
        : LoggingRepository<AccountCollectionAccount, AccountCollectionAccountViewModel>, IAccountCollectionRepository
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="context">امکانات مشترک مورد نیاز را برای عملیات دیتابیسی فراهم می کند</param>
        /// <param name="system">امکانات مورد نیاز در دیتابیس های سیستمی را فراهم می کند</param>
        public AccountCollectionRepository(IRepositoryContext context, ISystemRepository system)
            : base(context, system?.Logger)
        {
            _system = system;
            UnitOfWork.UseCompanyContext();
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات نمایشی کلیه مجموعه های حساب را خوانده و برمی گرداند
        /// </summary>
        /// <returns>مجموعه ای از اطلاعات نمایشی مجموعه های حساب</returns>
        public async Task<IList<AccountCollectionCategoryViewModel>> GetAccountCollectionCategoriesAsync()
        {
            var repository = UnitOfWork.GetAsyncRepository<AccountCollectionCategory>();
            var accCollectionCat = await repository
                .GetAllAsync(f => f.AccountCollections);

            return accCollectionCat
                .Select(a => Mapper.Map<AccountCollectionCategoryViewModel>(a))
                .ToList();
        }

        /// <summary>
        /// به روش آسنکرون، حساب های انتخاب شده برای یک مجموعه حساب را خوانده و برمی گرداند
        /// </summary>
        /// <param name="collectionId">شناسه یکتای مجموعه حساب</param>
        /// <returns>مجموعه ای از حساب های انتخاب شده در یک مجموعه حساب</returns>
        public async Task<IList<AccountViewModel>> GetCollectionAccountsAsync(int collectionId)
        {
            var accCollection = await Repository
                .GetAllOperationQuery<AccountCollectionAccount>(
                    ViewId.AccountCollectionAccount, col => col.Account, col => col.Account.Children)
                .Where(col => col.CollectionId == collectionId &&
                    col.BranchId == UserContext.BranchId &&
                    col.FiscalPeriodId == UserContext.FiscalPeriodId)
                .Select(col => Mapper.Map<AccountViewModel>(col))
                .ToListAsync();

            await LogCollectionOperationAsync(OperationId.View, collectionId);
            return accCollection;
        }

        /// <summary>
        /// به روش آسنکرون، حساب های یک مجموعه حساب را اضافه یا کم میکند
        /// </summary>
        /// <param name="accCollectionsList">اطلاعات حساب های یک مجموعه حساب</param>
        /// <param name="collectionId">شناسه یکتای مجموعه حساب انتخاب شده</param>
        /// <returns></returns>
        public async Task AddCollectionAccountsAsync(
            int collectionId, IList<AccountCollectionAccountViewModel> accCollectionsList)
        {
            var repository = UnitOfWork.GetAsyncRepository<AccountCollectionAccount>();
            var existing = await repository.GetByCriteriaAsync(
                f => f.CollectionId == collectionId &&
                f.FiscalPeriodId == UserContext.FiscalPeriodId);

            if (existing.Count > 0)
            {
                await RemoveInaccessibleAccountCollections(repository, existing, accCollectionsList);
            }

            await AddNewAccountCollections(repository, existing, accCollectionsList);
            await UnitOfWork.CommitAsync();
            await LogCollectionOperationAsync(OperationId.Save, collectionId);
        }

        internal override int? EntityType
        {
            get { return (int)EntityTypeId.AccountCollectionAccount; }
        }

        /// <summary>
        /// اطلاعات خلاصه سطر اطلاعاتی داده شده را به صورت یک رشته متنی برمی گرداند
        /// </summary>
        /// <param name="entity">یکی از سطرهای اطلاعاتی موجود</param>
        /// <returns>اطلاعات خلاصه سطر اطلاعاتی داده شده به صورت رشته متنی</returns>
        protected override string GetState(AccountCollectionAccount entity)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// آخرین تغییرات موجودیت را از مدل نمایشی به سطر اطلاعاتی موجود کپی می کند
        /// </summary>
        /// <param name="accountViewModel">مدل نمایشی شامل آخرین تغییرات</param>
        /// <param name="account">سطر اطلاعاتی موجود</param>
        protected override void UpdateExisting(
            AccountCollectionAccountViewModel accountViewModel, AccountCollectionAccount account)
        {
            throw new NotImplementedException();
        }

        private ISecureRepository Repository
        {
            get { return _system.Repository; }
        }

        private async Task AddNewAccountCollections(
            IAsyncRepository<AccountCollectionAccount> repository,
            IList<AccountCollectionAccount> existing, IList<AccountCollectionAccountViewModel> accCollectionsList)
        {
            var accountRepository = UnitOfWork.GetAsyncRepository<Account>();
            var branchRepository = UnitOfWork.GetAsyncRepository<Branch>();
            var branchId = UserContext.BranchId;
            var accountItems = existing
                .Where(item => item.BranchId == branchId)
                .Select(item => item.AccountId);
            var newItems = accCollectionsList
                .Where(item => !accountItems.Contains(item.AccountId));
            foreach (var item in newItems)
            {
                var account = await accountRepository.GetByIDAsync(item.AccountId);
                var accountCollectionAccount = Mapper.Map<AccountCollectionAccount>(item);
                repository.Insert(accountCollectionAccount);
                if (account != null && account.BranchScope != (short)BranchScope.CurrentBranch)
                {
                    await CascadeNewAccountCollection(repository, branchRepository, item);
                }
            }
        }

        private async Task CascadeNewAccountCollection(IAsyncRepository<AccountCollectionAccount> repository,
            IAsyncRepository<Branch> branchRepository,
            AccountCollectionAccountViewModel accCollection)
        {
            var branches = await branchRepository.GetByCriteriaAsync(br => br.ParentId == accCollection.BranchId);
            foreach (var item in branches)
            {
                var accountCollectionAccount = Mapper.Map<AccountCollectionAccount>(accCollection);
                accountCollectionAccount.Id = 0;
                accountCollectionAccount.BranchId = item.Id;
                repository.Insert(accountCollectionAccount);
                accCollection.BranchId = item.Id;
                await CascadeNewAccountCollection(repository, branchRepository, accCollection);
            }
        }

        private async Task RemoveInaccessibleAccountCollections(
            IAsyncRepository<AccountCollectionAccount> repository,
            IList<AccountCollectionAccount> existing,
            IList<AccountCollectionAccountViewModel> accCollectionsList)
        {
            var branchRepository = UnitOfWork.GetAsyncRepository<Branch>();

            var accountItems = accCollectionsList
                .Select(item => item.AccountId);
            var branchID = UserContext.BranchId;
            var removedItems = existing
                .Where(item => item.BranchId == branchID && !accountItems.Contains(item.AccountId))
                .ToArray();
            foreach (var item in removedItems)
            {
                await CascadeNewAccountCollection(repository, branchRepository, existing, item);
                repository.Delete(item);
            }
        }

        private async Task CascadeNewAccountCollection(
            IAsyncRepository<AccountCollectionAccount> repository,
            IAsyncRepository<Branch> branchRepository,
            IList<AccountCollectionAccount> existing,
            AccountCollectionAccount removedItem)
        {
            var childBranches = await branchRepository.GetByCriteriaAsync(
                br => br.ParentId == removedItem.BranchId);
            foreach (var child in childBranches)
            {
                var item = existing.SingleOrDefault(col => col.BranchId == child.Id
                    && col.CollectionId == removedItem.CollectionId
                    && col.AccountId == removedItem.AccountId);
                if (item != null)
                {
                    await CascadeNewAccountCollection(repository, branchRepository, existing, item);
                    repository.Delete(item);
                }
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

        private readonly ISystemRepository _system;
    }
}
