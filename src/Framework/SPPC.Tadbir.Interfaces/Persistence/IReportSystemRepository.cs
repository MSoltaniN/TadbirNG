using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SPPC.Tadbir.ViewModel.Reporting;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز در زیرساخت مدیریت گزارشات را تعریف می کند
    /// </summary>
    public interface IReportSystemRepository
    {
        /// <summary>
        /// به روش آسنکرون، اطلاعات قالب گزارش فوری را به زبان مورد نیاز خوانده و برمی گرداند
        /// </summary>
        /// <param name="localeId">شناسه دیتابیسی زبان مورد نظر</param>
        /// <returns>اطلاعات قالب گزارش فوری</returns>
        Task<LocalReportViewModel> GetQuickReportTemplateAsync(int localeId);

        /// <summary>
        /// به روش آسنکرون، ساختار درختی گزارش های تعریف شده را به زبان جاری برنامه خوانده و برمی گرداند
        /// </summary>
        /// <param name="localeId">شناسه دیتابیسی زبان جاری برنامه</param>
        /// <returns>ساختار درختی گزارش ها به زبان جاری برنامه</returns>
        Task<IList<TreeItemViewModel>> GetReportTreeAsync(int localeId);

        /// <summary>
        /// به روش آسنکرون، ساختار درختی گزارش های تعریف شده برای یک فرم را به زبان جاری برنامه خوانده و برمی گرداند
        /// </summary>
        /// <param name="localeId">شناسه دیتابیسی زبان جاری برنامه</param>
        /// <param name="viewId">شناسه دیتابیسی فرم مورد نظر</param>
        /// <returns>ساختار درختی گزارش ها به زبان جاری برنامه</returns>
        Task<IList<TreeItemViewModel>> GetReportTreeByViewAsync(int localeId, int viewId);

        /// <summary>
        /// به روش آسنکرون، ساختار درختی گزارش های تعریف شده در یک زیرسیستم را به زبان جاری برنامه خوانده و برمی گرداند
        /// </summary>
        /// <param name="localeId">شناسه دیتابیسی زبان جاری برنامه</param>
        /// <param name="subsystemId">شناسه دیتابیسی زیرسیستم مورد نظر</param>
        /// <returns>ساختار درختی گزارش ها به زبان جاری برنامه</returns>
        Task<IList<TreeItemViewModel>> GetReportTreeBySubsystemAsync(int localeId, int subsystemId);

        /// <summary>
        /// به روش آسنکرون، اطلاعات مورد نیاز برای پیش نمایش یا چاپ گزارش را خوانده و برمی گرداند
        /// </summary>
        /// <param name="reportId">شناسه دیتابیسی گزارش مورد نظر</param>
        /// <param name="localeId">شناسه دیتابیسی زبان جاری برنامه</param>
        /// <returns>اطلاعات مورد نیاز برای پیش نمایش یا چاپ گزارش</returns>
        Task<PrintInfoViewModel> GetReportAsync(int reportId, int localeId);

        /// <summary>
        /// به روش آسنکرون، اطلاعات مورد نیاز برای طراحی گزارش را خوانده و برمی گرداند
        /// </summary>
        /// <param name="reportId">شناسه دیتابیسی گزارش مورد نظر</param>
        /// <param name="localeId">شناسه دیتابیسی زبان جاری برنامه</param>
        /// <returns>اطلاعات مورد نیاز برای طراحی گزارش</returns>
        Task<PrintInfoViewModel> GetReportDesignAsync(int reportId, int localeId);

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

        /// <summary>
        /// به روش آسنکرون، اطلاعات خلاصه گزارش فوری برای یک فرم را خوانده و برمی گرداند
        /// </summary>
        /// <param name="viewId">شناسه دیتابیسی یکی از فرم های قابل چاپ</param>
        /// <returns>اطلاعات خلاصه گزارش فوری</returns>
        Task<ReportSummaryViewModel> GetQuickReportByViewAsync(int viewId);

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

        /// <summary>
        /// گزارش مشخص شده را به عنوان پیش فرض همه کاربران برای فرم مرتبط تنظیم می کند
        /// </summary>
        /// <param name="reportId">شناسه دیتابیسی گزارش مورد نظر</param>
        Task SetReportAsDefaultAsync(int reportId);

        /// <summary>
        /// به روش آسنکرون، مشخص می کند که آیا عنوان گزارش محلی داده شده تکراری است یا نه؟
        /// </summary>
        /// <param name="localeId">شناسه دیتابیسی زبان جاری برنامه</param>
        /// <param name="report">گزارش محلی مورد نظر</param>
        /// <returns>در صورت تکراری بودن مقدار بولی "درست" و در غیر این صورت
        /// مقدار بولی "نادرست" را برمی گرداند</returns>
        Task<bool> IsDuplicateReportCaptionAsync(int localeId, LocalReportViewModel report);
    }
}
