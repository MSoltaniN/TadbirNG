using System.Collections.Generic;
using System.Threading.Tasks;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.ViewModel.Auth;
using SPPC.Tadbir.ViewModel.Metadata;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای خواندن اطلاعات فراداده ای از محل ذخیره را تعریف می کند
    /// </summary>
    public interface IMetadataRepository
    {
        /// <summary>
        /// به روش آسنکرون، اطلاعات فراداده ای تعریف شده برای موجودیت با نام مشخص شده را از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <param name="viewName">نام (شناسه متنی) موجودیت مورد نظر</param>
        /// <returns>اطلاعات فراداده ای تعریف شده برای موجودیت</returns>
        Task<ViewViewModel> GetViewMetadataAsync(string viewName);

        /// <summary>
        /// به روش آسنکرون، اطلاعات فراداده ای تعریف شده برای موجودیت با نام مشخص شده را از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <param name="viewId">شناسه عددی موجودیت مورد نظر</param>
        /// <returns>اطلاعات فراداده ای تعریف شده برای موجودیت</returns>
        Task<ViewViewModel> GetViewMetadataByIdAsync(int viewId);

        /// <summary>
        /// به روش آسنکرون، اطلاعات نمایشی تمام کلیدهای میانبر را خوانده و برمی گرداند
        /// </summary>
        /// <returns>مجموعه ای از کلیدهای میانبر</returns>
        Task<IList<ShortcutCommandViewModel>> GetShortcutCommandsAsync();

        /// <summary>
        /// به روش آسنکرون، اطلاعات نمایشی تمام دستوراتی که در بالاترین سطح ساختار درختی قرار دارند را
        /// از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <returns>مجموعه ای از دستورات در بالاترین سطح</returns>
        Task<IList<CommandViewModel>> GetTopLevelCommandsAsync();

        /// <summary>
        /// به روش آسنکرون، اطلاعات نمایشی دستورات پیش فرض کاربران را خوانده و برمی گرداند
        /// </summary>
        /// <returns>مجموعه دستورات در منوی پیش فرض کاربران</returns>
        Task<IList<CommandViewModel>> GetDefaultCommandsAsync();

        /// <summary>
        /// به روش آسنکرون، اطلاعات فراداده ای تعریف شده برای همه موجودیت ها را از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <returns> اطلاعات فراداده ای تعریف شده برای همه موجودیت ها</returns>
        Task<IList<ViewViewModel>> GetViewsMetadataAsync();

        /// <summary>
        /// به روش آسنکرون، اطلاعات فراداده ای مرکب برای نمای لیستی مقایسه ای با اقلام داده شده را ساخته و برمی گرداند
        /// </summary>
        /// <param name="viewId">شناسه دیتابیسی نمای لیستی مورد نظر</param>
        /// <param name="itemViewId">شناسه دیتابیسی که نوع موجودیت اقلام داده شده را تعیین می کند</param>
        /// <param name="items">مجموعه اقلام داده شده برای مقایسه</param>
        /// <returns>اطلاعات فراداده ای مرکب برای نمای لیستی مقایسه ای</returns>
        Task<ViewViewModel> GetCompoundViewMetadataAsync(int viewId, int itemViewId, IEnumerable<int> items);

        /// <summary>
        /// به روش آسنکرون، اطلاعات فراداده ای پویا را برای گزارش مانده به تفکیک حساب ایجاد کرده و برمی گرداند
        /// </summary>
        /// <param name="parameters">پارامترهای تنظیم شده برای گزارش</param>
        /// <returns>اطلاعات فراداده ای پویا برای کزارش</returns>
        Task<ViewViewModel> GetBalanceByAccountMetadataAsync(BalanceByAccountParameters parameters);

        #region System Designer

        /// <summary>
        /// به روش آسنکرون، اطلاعات فراداده ای مجوزهای امنیتی موجود را خوانده و برمی گرداند
        /// </summary>
        /// <returns>مجموعه ای از اطلاعات فراداده ای مجوزهای امنیتی موجود</returns>
        Task<IList<PermissionGroupViewModel>> GetPermissionGroupsAsync();

        #endregion
    }
}
