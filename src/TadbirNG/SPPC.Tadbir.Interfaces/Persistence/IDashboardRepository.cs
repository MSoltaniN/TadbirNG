using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.ViewModel.Finance;
using SPPC.Tadbir.ViewModel.Reporting;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای تهیه اطلاعات خلاصه در داشبورد را تعریف می کند
    /// </summary>
    public interface IDashboardRepository
    {
        /// <summary>
        /// به روش آسنکرون، مقادیر خلاصه محاسبه شده برای نمایش در داشبورد را خوانده و برمی گرداند
        /// </summary>
        /// <param name="calendar">تقویم مورد استفاده برای نمودارهای ماهیانه</param>
        /// <returns>اطلاعات مالی محاسبه شده</returns>
        Task<DashboardSummariesViewModel> GetSummariesAsync(Calendar calendar);

        /// <summary>
        /// به روش آسنکرون، اطلاعات کامل داشبورد ایجاد شده توسط کاربر جاری برنامه را خوانده و برمی گرداند
        /// </summary>
        /// <returns>اطلاعات کامل برای داشبورد کاربر جاری برنامه یا رفرنس بدون مقدار
        /// در صورتی که داشبوردی برای کاربر جاری وجود نداشته باشد</returns>
        DashboardViewModel GetCurrentUserDashboard();

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

        /// <summary>
        /// اطلاعات ویجت مشخص شده را با توجه به پارامترهای داده شده محاسبه کرده و برمی گرداند
        /// </summary>
        /// <param name="widgetId">شناسه دیتابیسی ویجت مورد نظر</param>
        /// <param name="fromDate">تاریخ ابتدا برای محاسبه اطلاعات</param>
        /// <param name="toDate">تاریخ انتها برای محاسبه اطلاعات</param>
        /// <param name="unit">واحد زمانی مورد نظر برای نمایش ریز اطلاعات در نمودار</param>
        /// <returns>اطلاعات مورد نیاز برای نمایش در نمودار</returns>
        Task<ChartSeriesViewModel> GetWidgetDataAsync(
            int widgetId, DateTime? fromDate, DateTime? toDate, WidgetDateUnit? unit);
    }
}
