using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPPC.Framework.Common;
using SPPC.Framework.Persistence;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Model.Corporate;
using SPPC.Tadbir.Model.Finance;
using SPPC.Tadbir.Persistence.Utility;
using SPPC.Tadbir.Resources;
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
        /// <param name="utility">امکانات تکمیلی برای کار با مجموعه های حساب را پیاده سازی می کند</param>
        public AccountCollectionRepository(IRepositoryContext context, ISystemRepository system,
            IAccountCollectionUtility utility)
            : base(context, system?.Logger)
        {
            _utility = utility;
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات نمایشی طبقه بندی های مجموعه حساب را خوانده و برمی گرداند
        /// </summary>
        /// <returns>مجموعه ای از اطلاعات نمایشی طبقه بندی های مجموعه حساب</returns>
        public async Task<IList<AccountCollectionCategoryViewModel>> GetCollectionCategoriesAsync()
        {
            var repository = UnitOfWork.GetAsyncRepository<AccountCollectionCategory>();
            var categories = await repository
                .GetEntityQuery(cat => cat.AccountCollections)
                .Select(cat => Mapper.Map<AccountCollectionCategoryViewModel>(cat))
                .ToListAsync();
            return categories;
        }

        /// <summary>
        /// به روش آسنکرون، حساب های انتخاب شده برای یک مجموعه حساب را خوانده و برمی گرداند
        /// </summary>
        /// <param name="collectionId">شناسه یکتای مجموعه حساب</param>
        /// <returns>مجموعه ای از حساب های انتخاب شده در یک مجموعه حساب</returns>
        public async Task<IList<AccountViewModel>> GetCollectionAccountsAsync(int collectionId)
        {
            Verify.EnumValueIsDefined(typeof(AccountCollectionId), nameof(collectionId), collectionId);
            var accounts = await _utility.GetInheritedAccountsAsync(
                (AccountCollectionId)collectionId, UserContext.BranchId);
            await LogCollectionOperationAsync(OperationId.View, collectionId);
            return accounts
                .Select(acc => Mapper.Map<AccountViewModel>(acc))
                .ToList();
        }

        /// <summary>
        /// به روش آسنکرون، آخرین وضعیت حساب های یک مجموعه حساب را ذخیره میکند
        /// </summary>
        /// <param name="accounts">اطلاعات حساب های یک مجموعه حساب</param>
        /// <param name="collectionId">شناسه یکتای مجموعه حساب انتخاب شده</param>
        /// <returns></returns>
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

        internal override int? EntityType
        {
            get { return (int)EntityTypeId.AccountCollectionAccount; }
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

        private readonly IAccountCollectionUtility _utility;
    }
}
