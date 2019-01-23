using System.Collections.Generic;
using System.Threading.Tasks;
using SPPC.Tadbir.ViewModel.Finance;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای مدیریت گروه مجموعه حساب را تعریف می کند.
    /// </summary>
    public interface IAccountCollectionCategoryRepository
    {
        /// <summary>
        /// به روش آسنکرون، اطلاعات نمایشی کلیه مجموعه های حساب را خوانده و برمی گرداند
        /// </summary>
        /// <returns>مجموعه ای از اطلاعات نمایشی مجموعه های حساب</returns>
        Task<IList<AccountCollectionCategoryViewModel>> GetAccountCollectionCategoriesAsync();
    }
}
