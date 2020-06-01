using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SPPC.Framework.Common;
using SPPC.Framework.Persistence;
using SPPC.Tadbir.Model.Finance;
using SPPC.Tadbir.ViewModel.Finance;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای مدیریت اطلاعات بانکی حساب را تعریف میکند
    /// </summary>
    public class AccountOwnerRepository : LoggingRepository<AccountOwner, AccountOwnerViewModel>, IAccountOwnerRepository
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="context">امکانات مشترک مورد نیاز را برای عملیات دیتابیسی فراهم می کند</param>
        /// <param name="system">امکانات مورد نیاز در دیتابیس های سیستمی را فراهم می کند</param>
        public AccountOwnerRepository(IRepositoryContext context, ISystemRepository system)
            : base(context, system?.Logger)
        {
            _system = system;
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات حساب را در محل ذخیره ایجاد یا اصلاح می کند
        /// </summary>
        /// <param name="accountOwner">اطلاعات حساب بانکی</param>
        /// <returns>اطلاعات حساب بانکی ایجاد یا اصلاح شده</returns>
        public async Task<AccountOwnerViewModel> SaveAccountOwnerAsync(AccountOwnerViewModel accountOwner)
        {
            Verify.ArgumentNotNull(accountOwner, "accountOwner");
            AccountOwner accOwner = default(AccountOwner);
            var repository = UnitOfWork.GetAsyncRepository<AccountOwner>();
            if (accountOwner.Id == 0)
            {
                accOwner = Mapper.Map<AccountOwner>(accountOwner);
                await InsertAsync(repository, accOwner);

                var rep = UnitOfWork.GetAsyncRepository<AccountHolder>();

                foreach (var item in accOwner.AccountHolders)
                {
                    item.AccountOwnerId = accOwner.Id;
                    rep.Insert(item);
                }

                await UnitOfWork.CommitAsync();
            }
            else
            {
                accOwner = await repository.GetByIDAsync(accountOwner.Id, owner => owner.AccountHolders);
                if (accOwner != null)
                {
                    await UpdateAsync(repository, accOwner, accountOwner);

                    await UpdateAccountHoldersAsync(accOwner.AccountHolders, accountOwner.AccountHolders);
                }
            }

            return Mapper.Map<AccountOwnerViewModel>(accOwner);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات حساب بانکی مشخص شده با شناسه عددی را از محل ذخیره حذف می کند
        /// </summary>
        /// <param name="ownerId">شناسه عددی اطلاعات حساب بانکی مورد نظر برای حذف</param>
        public async Task DeleteAccountOwnerAsync(int ownerId)
        {
            var repository = UnitOfWork.GetAsyncRepository<AccountOwner>();
            var accountOwner = await repository.GetByIDWithTrackingAsync(
                ownerId,
                owner => owner.AccountHolders);
            if (accountOwner != null)
            {
                accountOwner.AccountHolders.Clear();
                await DeleteAsync(repository, accountOwner);
            }
        }

        /// <summary>
        /// آخرین تغییرات موجودیت را از مدل نمایشی به سطر اطلاعاتی موجود کپی می کند
        /// </summary>
        /// <param name="accountOwnerViewModel">مدل نمایشی شامل آخرین تغییرات</param>
        /// <param name="accountOwner">سطر اطلاعاتی موجود</param>
        protected override void UpdateExisting(AccountOwnerViewModel accountOwnerViewModel, AccountOwner accountOwner)
        {
            accountOwner.BankName = accountOwnerViewModel.BankName;
            accountOwner.AccountType = accountOwnerViewModel.AccountType;
            accountOwner.BankBranchName = accountOwnerViewModel.BankBranchName;
            accountOwner.BranchIndex = accountOwnerViewModel.BranchIndex;
            accountOwner.AccountNumber = accountOwnerViewModel.AccountNumber;
            accountOwner.CardNumber = accountOwnerViewModel.CardNumber;
            accountOwner.ShabaNumber = accountOwnerViewModel.ShabaNumber;
            accountOwner.Description = accountOwnerViewModel.Description;
        }

        private static async Task UpdateAccountHoldersAsync(
            IAsyncRepository<AccountHolder> repository,
            IList<AccountHolderViewModel> updatedItems)
        {
            foreach (var item in updatedItems)
            {
                var entity = await repository.GetByIDAsync(item.Id);
                if (entity != null)
                {
                    entity.FirstName = item.FirstName;
                    entity.LastName = item.LastName;
                    entity.HasSignature = item.HasSignature;

                    repository.Update(entity);
                }
            }
        }

        private static void RemoveAccountHolders(
            IAsyncRepository<AccountHolder> repository,
            IList<AccountHolder> removedItems)
        {
            foreach (var item in removedItems)
            {
                repository.Delete(item);
            }
        }

        private async Task UpdateAccountHoldersAsync(
            IList<AccountHolder> existing,
            IList<AccountHolderViewModel> accHolders)
        {
            var repository = UnitOfWork.GetAsyncRepository<AccountHolder>();

            if (existing.Count > 0)
            {
                var accHolderItems = accHolders
                    .Select(item => item.Id);
                var removedItems = existing
                .Where(item => !accHolderItems.Contains(item.Id))
                .ToList();

                RemoveAccountHolders(repository, removedItems);

                var newItems = accHolders.Where(f => f.Id == 0).ToList();
                InsertAccountHolder(repository, newItems);

                var updatedItems = accHolders
                    .Where(item => item.Id > 0 &&
                    !removedItems.Select(rmvItem => rmvItem.Id).Contains(item.Id))
                    .ToList();

                await UpdateAccountHoldersAsync(repository, updatedItems);
            }
            else
            {
                InsertAccountHolder(repository, accHolders);
            }

            await UnitOfWork.CommitAsync();
        }

        private void InsertAccountHolder(
            IAsyncRepository<AccountHolder> repository,
            IList<AccountHolderViewModel> newItems)
        {
            foreach (var item in newItems)
            {
                var entity = Mapper.Map<AccountHolder>(item);
                repository.Insert(entity);
            }
        }

        private readonly ISystemRepository _system;
    }
}
