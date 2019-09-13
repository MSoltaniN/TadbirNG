using System.Collections.Generic;
using System.Threading.Tasks;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.ViewModel.Auth;
using SPPC.Tadbir.ViewModel.Finance;
using SPPC.Tadbir.ViewModel.Metadata;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای مدیریت مجموعه حساب را تعریف می کند.
    /// </summary>
    public interface IAccountCollectionRepository
    {
        /// <summary>
        /// به روش آسنکرون، اطلاعات نمایشی کلیه مجموعه های حساب را خوانده و برمی گرداند
        /// </summary>
        /// <returns>مجموعه ای از اطلاعات نمایشی مجموعه های حساب</returns>
        Task<IList<AccountCollectionCategoryViewModel>> GetAccountCollectionCategoriesAsync();

        /// <summary>
        /// به روش آسنکرون، حساب های انتخاب شده برای یک مجموعه حساب را خوانده و برمی گرداند
        /// </summary>
        /// <param name="collectionId">شناسه یکتای مجموعه حساب</param>
        /// <returns>مجموعه ای از حساب های انتخاب شده در یک مجموعه حساب</returns>
        Task<IList<AccountViewModel>> GetCollectionAccountsAsync(int collectionId);

        /// <summary>
        /// به روش آسنکرون، تعداد حساب های تعریف شده در دوره مالی و شعبه جاری برنامه را
        /// از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>تعداد حساب های تعریف شده در دوره مالی و شعبه جاری برنامه</returns>
        Task<int> GetCountAsync(GridOptions gridOptions = null);

        /// <summary>
        /// به روش آسنکرون، حساب های یک مجموعه حساب را اضافه میکند
        /// </summary>
        /// <param name="accCollectionsList">اطلاعات حساب های یک مجموعه حساب</param>
        /// <param name="collectionId">شناسه یکتای مجموعه حساب انتخاب شده</param>
        /// <returns></returns>
        Task AddCollectionAccountsAsync(int collectionId, IList<AccountCollectionAccountViewModel> accCollectionsList);
    }
}
