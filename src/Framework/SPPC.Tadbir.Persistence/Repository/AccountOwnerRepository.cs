using System.Threading.Tasks;
using SPPC.Framework.Common;
using SPPC.Tadbir.Model.Finance;
using SPPC.Tadbir.ViewModel.Finance;

namespace SPPC.Tadbir.Persistence.Repository
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
            }
            else
            {
                accOwner = await repository.GetByIDAsync(accountOwner.Id);
                if (accOwner != null)
                {
                    await UpdateAsync(repository, accOwner, accountOwner);
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
            var accountOwner = await repository.GetByIDWithTrackingAsync(ownerId);
            if (accountOwner != null)
            {
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

        private readonly ISystemRepository _system;
    }
}
