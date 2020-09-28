using System.Collections.Generic;
using System.Threading.Tasks;
using SPPC.Tadbir.ViewModel.Finance;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای مدیریت مجموعه حساب را تعریف می کند.
    /// </summary>
    public interface IAccountCollectionRepository
    {
        /// <summary>
        /// به روش آسنکرون، اطلاعات نمایشی طبقه بندی های مجموعه حساب را خوانده و برمی گرداند
        /// </summary>
        /// <returns>مجموعه ای از اطلاعات نمایشی طبقه بندی های مجموعه حساب</returns>
        Task<IList<AccountCollectionCategoryViewModel>> GetCollectionCategoriesAsync();

        /// <summary>
        /// به روش آسنکرون، حساب های انتخاب شده برای یک مجموعه حساب را خوانده و برمی گرداند
        /// </summary>
        /// <param name="collectionId">شناسه یکتای مجموعه حساب</param>
        /// <returns>مجموعه ای از حساب های انتخاب شده در یک مجموعه حساب</returns>
        Task<IList<AccountViewModel>> GetCollectionAccountsAsync(int collectionId);

        /// <summary>
        /// به روش آسنکرون، حساب های یک مجموعه حساب را اضافه میکند
        /// </summary>
        /// <param name="accCollectionsList">اطلاعات حساب های یک مجموعه حساب</param>
        /// <param name="collectionId">شناسه یکتای مجموعه حساب انتخاب شده</param>
        /// <returns></returns>
        Task AddCollectionAccountsAsync(int collectionId, IList<AccountCollectionAccountViewModel> accCollectionsList);
    }
}
