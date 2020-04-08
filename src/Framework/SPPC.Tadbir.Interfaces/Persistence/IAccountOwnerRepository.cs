using System.Threading.Tasks;
using SPPC.Tadbir.ViewModel.Finance;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای مدیریت اطلاعات بانکی حساب را تعریف میکند
    /// </summary>
    public interface IAccountOwnerRepository
    {
        /// <summary>
        /// به روش آسنکرون، اطلاعات حساب را در محل ذخیره ایجاد یا اصلاح می کند
        /// </summary>
        /// <param name="accountOwner">اطلاعات حساب بانکی</param>
        /// <returns>اطلاعات حساب بانکی ایجاد یا اصلاح شده</returns>
        Task<AccountOwnerViewModel> SaveAccountOwnerAsync(AccountOwnerViewModel accountOwner);

        /// <summary>
        /// به روش آسنکرون، اطلاعات حساب بانکی مشخص شده با شناسه عددی را از محل ذخیره حذف می کند
        /// </summary>
        /// <param name="ownerId">شناسه عددی اطلاعات حساب بانکی مورد نظر برای حذف</param>
        Task DeleteAccountOwnerAsync(int ownerId);
    }
}
