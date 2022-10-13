using System.Collections.Generic;
using System.Threading.Tasks;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Configuration.Models;
using SPPC.Tadbir.Utility;
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
        /// به روش آسنکرون، اطلاعات نمایشی یکی از برگه های موجود را خوانده و برمی گرداند
        /// </summary>
        /// <param name="tabId">شناسه دیتابیسی برگه مورد نظر</param>
        /// <returns>اطلاعات نمایشی برگه مورد نظر</returns>
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
        /// به روش آسنکرون، یکی از ویجت های قابل دسترسی توسط کاربر جاری را در برگه تعیین شده اضافه یا اصلاح می کند
        /// </summary>
        /// <param name="tabWidget">اطلاعات ویجت مورد نظر برای ایجاد یا اصلاح به برگه داشبورد</param>
        /// <returns>آخرین اطلاعات ویجت اضافه یا اصلاح شده در برگه داشبورد</returns>
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
