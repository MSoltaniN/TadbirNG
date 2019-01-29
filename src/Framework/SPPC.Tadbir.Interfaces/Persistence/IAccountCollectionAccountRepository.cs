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
    public interface IAccountCollectionAccountRepository
    {
        /// <summary>
        /// به روش آسنکرون، حساب های یک مجموعه حساب و حساب های قابل انتخاب را خوانده و برمی گرداند
        /// </summary>
        /// <param name="collectionId">شناسه یکتای مجموعه حساب</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه ای از حساب های یک سطح و حساب های انتخاب شده در یک مجموعه حساب</returns>
        Task<AccountCollectionItemsViewModel> GetAccountCollectionAccountAsync(int collectionId, GridOptions gridOptions = null);

        /// <summary>
        /// اطلاعات محیطی کاربر جاری برنامه را برای ایجاد لاگ های عملیاتی تنظیم می کند
        /// </summary>
        /// <param name="userContext">اطلاعات دسترسی کاربر به منابع محدود شده مانند نقش ها، دوره های مالی و شعبه ها</param>
        void SetCurrentContext(UserContextViewModel userContext);

        /// <summary>
        /// به روش آسنکرون، تعداد حساب های تعریف شده در دوره مالی و شعبه جاری برنامه را
        /// از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>تعداد حساب های تعریف شده در دوره مالی و شعبه جاری برنامه</returns>
        Task<int> GetCountAsync(GridOptions gridOptions = null);

        /// <summary>
        /// به روش آسنکرون، اطلاعات فراداده ای تعریف شده برای مجموعه حساب را از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <returns>اطلاعات فراداده ای تعریف شده برای مجموعه حساب</returns>
        Task<ViewViewModel> GetAccountCollectionMetadataAsync();

        /// <summary>
        /// به روش آسنکرون، حساب های یک مجموعه حساب را اضافه میکند
        /// </summary>
        /// <param name="accCollectionsList">اطلاعات حساب های یک مجموعه حساب</param>
        /// <param name="collectionId">شناسه یکتای مجموعه حساب انتخاب شده</param>
        /// <returns></returns>
        Task AddAccountCollectionAccountAsync(int collectionId, IList<AccountCollectionAccountViewModel> accCollectionsList);
    }
}
