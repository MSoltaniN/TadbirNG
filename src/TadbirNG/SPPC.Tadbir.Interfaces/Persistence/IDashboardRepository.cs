using System.Collections.Generic;
using System.Threading.Tasks;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Configuration.Models;
using SPPC.Tadbir.Utility;
using SPPC.Tadbir.ViewModel;
using SPPC.Tadbir.ViewModel.Finance;
using SPPC.Tadbir.ViewModel.Reporting;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای تهیه اطلاعات خلاصه در داشبورد را تعریف می کند
    /// </summary>
    public interface IDashboardRepository
    {
        #region Dashboard Management

        /// <summary>
        /// به روش آسنکرون، اطلاعات کامل داشبورد ایجاد شده توسط کاربر جاری برنامه را خوانده و برمی گرداند
        /// </summary>
        /// <returns>اطلاعات کامل برای داشبورد کاربر جاری برنامه یا رفرنس بدون مقدار
        /// در صورتی که داشبوردی برای کاربر جاری وجود نداشته باشد</returns>
        DashboardViewModel GetCurrentUserDashboard();

        /// <summary>
        /// به روش آسنکرون، داشبورد جدیدی برای کاربر جاری ایجاد کرده و اولین ویجت را به آن اضافه می کند
        /// </summary>
        /// <param name="tabWidget">اولین ویجت در داشبورد کاربر جاری که به برگه پیش فرض اضافه می شود</param>
        /// <returns>اطلاعات داشبورد ایجاد شده برای کاربر جاری</returns>
        Task<DashboardViewModel> CreateCurrentUserDashboardAsync(TabWidgetViewModel tabWidget);

        /// <summary>
        /// به روش آسنکرون، مشخص می کند که داشبورد برای کاربر جاری ایجاد شده یا نه
        /// </summary>
        /// <returns>اگر برای کاربر جاری داشبورد ایجاد شده باشد، مقدار بولی "درست" و
        /// در غیر این صورت مقدار بولی "نادرست" را برمی گرداند</returns>
        Task<bool> IsCurrentDashboardCreatedAsync();

        /// <summary>
        /// به روش آسنکرون، اطلاعات نمایشی یکی از برگه های موجود در داشبورد کاربر جاری را خوانده و برمی گرداند
        /// </summary>
        /// <param name="tabId">شناسه دیتابیسی برگه مورد نظر</param>
        /// <returns>اطلاعات نمایشی برگه مورد نظر یا رفرنس بدون مقدار در صورتی که برگه مورد نظر
        /// در داشبورد کاربر جاری نباشد</returns>
        Task<DashboardTabViewModel> GetDashboardTabAsync(int tabId);

        /// <summary>
        /// به روش آسنکرون، اطلاعات نمایشی یک برگه داشبورد را در دیتابیس ایجاد یا اصلاح می کند
        /// </summary>
        /// <param name="tab">اطلاعات برگه مورد نظر برای ایجاد یا اصلاح</param>
        /// <returns>اطلاعات برگه ایجاد یا اصلاح شده در دیتابیس</returns>
        Task<DashboardTabViewModel> SaveDashboardTabAsync(DashboardTabViewModel tab);

        /// <summary>
        /// به روش آسنکرون، اطلاعات نمایشی چند برگه داشبورد را در دیتابیس ایجاد یا اصلاح می کند
        /// </summary>
        /// <param name="tabs">اطلاعات برگه های مورد نظر برای ایجاد یا اصلاح</param>
        Task SaveDashboardTabsAsync(IList<DashboardTabViewModel> tabs);

        /// <summary>
        /// به روش آسنکرون، برگه مشخص شده را در دیتابیس حذف می کند
        /// </summary>
        /// <param name="tabId">شناسه دستابیسی برگه مورد نظر برای حذف</param>
        Task DeleteDashboardTabAsync(int tabId);

        /// <summary>
        /// به روش آسنکرون، تعداد ویجت های اضافه شده به برگه مشخص شده را خوانده و برمی گرداند
        /// </summary>
        /// <param name="tabId">شناسه دیتابیسی یکی از برگه های موجود</param>
        /// <returns>تعداد ویجت های اضافه شده به برگه</returns>
        Task<int> GetTabWidgetCountAsync(int tabId);

        /// <summary>
        /// به روش آسنکرون، مشخص می کند که برگه داده شده تنها برگه داشبورد است یا نه
        /// </summary>
        /// <param name="tabId">شناسه دیتابیسی برگه مورد نظر</param>
        /// <returns>در صورتی که برگه مشخص شده تنها برگه داشبورد باشد، مقدار بولی "درست" و در غیر این صورت
        /// مقدار بولی "نادرست" را برمی گرداند</returns>
        Task<bool> IsSoleDashboardTab(int tabId);

        /// <summary>
        /// به روش آسنکرون، یکی از ویجت های قابل دسترسی توسط کاربر جاری را در برگه تعیین شده اضافه می کند
        /// </summary>
        /// <param name="tabWidget">اطلاعات ویجت مورد نظر برای اضافه کردن به برگه داشبورد</param>
        /// <returns>آخرین اطلاعات ویجت اضافه شده در برگه داشبورد</returns>
        Task<TabWidgetViewModel> SaveTabWidgetAsync(TabWidgetViewModel tabWidget);

        /// <summary>
        /// به روش آسنکرون، اطلاعات ویجت های اضافه شده به برگه داشبورد را اصلاح می کند.
        /// </summary>
        /// <param name="tabWidgets">مجموعه ویجت های اضافه شده به داشبورد کاربر جاری</param>
        Task SaveTabWidgetsAsync(IList<TabWidgetViewModel> tabWidgets);

        /// <summary>
        /// به روش آسنکرون، ویجت داده شده را از برگه مورد نظر در داشبورد کاربر جاری حذف می کند
        /// </summary>
        /// <param name="tabId">شناسه دیتابیسی برگه ای که ویجت از آن حذف می شود</param>
        /// <param name="widgetId">شناسه دیتابیسی ویجتی که از برگه مورد نظر حذف می شود</param>
        Task DeleteTabWidgetAsync(int tabId, int widgetId);

        #endregion

        #region Widget Management

        /// <summary>
        /// به روش آسنکرون، فهرست ویجت های ایجادشده توسط کاربر جاری برنامه را خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>ویجت های ایجادشده توسط کاربر جاری</returns>
        Task<PagedList<WidgetViewModel>> GetCurrentUserWidgetsAsync(GridOptions gridOptions = null);

        /// <summary>
        /// به روش آسنکرون، فهرست ویجت های قابل دسترسی توسط کاربر جاری برنامه را خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>ویجت های قابل دسترسی توسط کاربر جاری</returns>
        Task<PagedList<WidgetViewModel>> GetAccessibleWidgetsAsync(GridOptions gridOptions = null);

        /// <summary>
        /// به روش آسنکرون، تعداد موارد استفاده از ویجت داده شده را خوانده و برمی گرداند
        /// </summary>
        /// <param name="widgetId">شناسه دیتابیسی ویجت مورد نظر</param>
        /// <returns>تعداد موارد استفاده از ویجت</returns>
        Task<int> GetWidgetUsageCountAsync(int widgetId);

        /// <summary>
        /// به روش آسنکرون، اطلاعات ویجت مشخص شده را با توجه به پارامترهای داده شده محاسبه کرده و برمی گرداند
        /// </summary>
        /// <param name="widgetId">شناسه دیتابیسی ویجت مورد نظر</param>
        /// <param name="parameters">پارامترهای مورد نیاز برای تابع محاسباتی ویجت</param>
        /// <returns>اطلاعات مورد نیاز برای نمایش در ویجت</returns>
        Task<object> GetWidgetDataAsync(int widgetId, IList<ParameterSummary> parameters);

        /// <summary>
        /// به روش آسنکرون اطلاعات نمایشی ویجت مورد نظر را خوانده و برمی گرداند
        /// </summary>
        /// <param name="widgetId">شناسه دیتابیسی ویجت مورد نظر</param>
        /// <returns>اطلاعات نمایشی ویجت مورد نظر</returns>
        Task<WidgetViewModel> GetWidgetAsync(int widgetId);

        /// <summary>
        /// به روش آسنکرون، اطلاعات ویجت داده شده را در دیتابیس ایجاد یا اصلاح می کند
        /// </summary>
        /// <param name="widget">ویجت مورد نظر برای ایجاد یا اصلاح</param>
        /// <returns>اطلاعات ویجت ایجاد یا اصلاح شده در دیتابیس</returns>
        Task<WidgetViewModel> SaveWidgetAsync(WidgetViewModel widget);

        /// <summary>
        /// به روش آسنکرون، اطلاعات ویجت داده شده را از دیتابیس حذف می کند
        /// </summary>
        /// <param name="widgetId">شناسه دیتابیسی ویجت مورد نظر برای حذف</param>
        Task DeleteWidgetAsync(int widgetId);

        /// <summary>
        /// به روش آسنکرون، پارامترهای مورد نیاز تابع محاسباتی داده شده را برمی گرداند
        /// </summary>
        /// <param name="functionId">شناسه دیتابیسی تابع محاسباتی مورد نظر</param>
        /// <returns>پارامترهای مورد نیاز تابع محاسباتی</returns>
        Task<IList<ParameterSummary>> GetFunctionParametersAsync(int functionId);

        /// <summary>
        /// به روش آسنکرون، دسترسی نقش های مرتبط با ویجت مورد نظر را برمی گرداند 
        /// </summary>
        /// <param name="widgetId">شناسه دیتابیسی ویجت مورد نظر</param>
        /// <returns></returns>
        Task<RelatedItemsViewModel> GetWidgetRolesAsync(int widgetId);
        
        /// <summary>
        /// به روش آسنکرون ، آخرین وضعیت نقش های مرتبط با ویجت مورد نظر را برمی گرداند
        /// </summary>
        /// <param name="widgetRoles">اطلاعات نمایشی نقش های دارای دسترسی</param>
        /// <returns></returns>
        Task SaveWidgetRolesAsync(RelatedItemsViewModel widgetRoles);
        #endregion

        #region Data Lookup

        /// <summary>
        /// به روش آسنکرون، اطلاعات توابع محاسباتی قابل استفاده در ویجت ها را خوانده و برمی گرداند
        /// </summary>
        /// <returns>اطلاعات توابع محاسباتی</returns>
        Task<List<WidgetFunctionViewModel>> GetWidgetFunctionsLookupAsync();

        /// <summary>
        /// به روش آسنکرون، اطلاعات انواع نمودارهای قابل استفاده در ویجت ها را خوانده و برمی گرداند
        /// </summary>
        /// <returns>اطلاعات انواع نمودارها</returns>
        Task<List<WidgetTypeViewModel>> GetWidgetTypesLookupAsync();

        /// <summary>
        /// به روش آسنکرون، اطلاعات ویجت های قابل دسترسی توسط کاربر جاری را خوانده و برمی گرداند
        /// </summary>
        /// <returns>اطلاعات ویجت های قابل دسترسی</returns>
        Task<List<WidgetViewModel>> GetWidgetsLookupAsync();

        #endregion

    }
}
