using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SPPC.Tadbir.ViewModel.Auth;
using SPPC.Tadbir.ViewModel.Reporting;

namespace SPPC.Tadbir.Persistence
{
    public interface IReportSystemRepository
    {
        /// <summary>
        /// اطلاعات محیطی و امنیتی کاربر جاری برنامه را برای کنترل قواعد کاری برنامه تنظیم می کند
        /// </summary>
        /// <param name="userContext">اطلاعات محیطی و امنیتی کاربر جاری برنامه</param>
        void SetCurrentContext(UserContextViewModel userContext);

        /// <summary>
        /// به روش آسنکرون، ساختار درختی گزارش های تعریف شده را به زبان جاری برنامه خوانده و برمی گرداند
        /// </summary>
        /// <param name="localeCode">کد استاندارد  دو حرفی زبان جاری برنامه</param>
        /// <returns>ساختار درختی گزارش ها به زبان جاری برنامه</returns>
        Task<IList<TreeItemViewModel>> GetReportTreeAsync(string localeCode);

        /// <summary>
        /// به روش آسنکرون، اطلاعات مورد نیاز برای پیش نمایش یا چاپ گزارش را خوانده و برمی گرداند
        /// </summary>
        /// <param name="reportId">شناسه دیتابیسی گزارش مورد نظر</param>
        /// <param name="localeCode">کد استاندارد  دو حرفی زبان جاری برنامه</param>
        /// <returns>اطلاعات مورد نیاز برای پیش نمایش یا چاپ گزارش</returns>
        Task<PrintInfoViewModel> GetReportAsync(int reportId, string localeCode);

        /// <summary>
        /// به روش آسنکرون، اطلاعات مورد نیاز برای طراحی گزارش را خوانده و برمی گرداند
        /// </summary>
        /// <param name="reportId">شناسه دیتابیسی گزارش مورد نظر</param>
        /// <param name="localeCode">کد استاندارد  دو حرفی زبان جاری برنامه</param>
        /// <returns>اطلاعات مورد نیاز برای طراحی گزارش</returns>
        Task<PrintInfoViewModel> GetReportDesignAsync(int reportId, string localeCode);

        /// <summary>
        /// به روش آسنکرون، اطلاعات خلاصه گزارش را برای عملیات اعتبارسنجی خوانده و برمی گرداند
        /// </summary>
        /// <param name="reportId">شناسه دیتابیسی گزارش مورد نظر</param>
        /// <returns>اطلاعات خلاصه گزارش موزد نظر</returns>
        Task<ReportSummaryViewModel> GetReportSummaryAsync(int reportId);

        /// <summary>
        /// به روش آسنکرون، اطلاعات خلاصه گزارش پیش فرض برای یک فرم را خوانده و برمی گرداند
        /// </summary>
        /// <param name="viewId">شناسه دیتابیسی یکی از فرم های قابل چاپ</param>
        /// <returns>اطلاعات خلاصه گزارش پیش فرض</returns>
        Task<ReportSummaryViewModel> GetDefaultReportByViewAsync(int viewId);

        Task<int> GetLocaleIdAsync(string localeCode);

        /// <summary>
        /// به روش آسنکرون، اطلاعات یک گزارش ذخیره شده کاربری را ذخیره یا بروزرسانی می کند
        /// </summary>
        /// <param name="report">اطلاعات محلی شده گزارش کاربری</param>
        Task SaveUserReportAsync(LocalReportViewModel report);

        /// <summary>
        /// به روش آسنکرون، عنوان یک گزارش ذخیره شده کاربری را بروزرسانی می کند
        /// </summary>
        /// <param name="report">اطلاعات محلی شده گزارش کاربری شامل عنوان جدید مورد نظر</param>
        Task SetUserReportCaptionAsync(LocalReportViewModel report);

        /// <summary>
        /// به روش آسنکرون، یک گزارش ذخیر شده کاربری را به همراه کلیه اطلاعات مرتبط حذف می کند
        /// </summary>
        /// <param name="reportId">شناسه دیتابیسی گزارش مورد نظر</param>
        Task DeleteUserReportAsync(int reportId);
    }
}
