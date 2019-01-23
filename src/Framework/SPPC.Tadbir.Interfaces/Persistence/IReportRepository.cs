using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.ViewModel.Auth;
using SPPC.Tadbir.ViewModel.Reporting;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای تهیه و محاسبه اطلاعات گزارش های برنامه را تعریف می کند
    /// </summary>
    public interface IReportRepository
    {
        /// <summary>
        /// اطلاعات محیطی و امنیتی کاربر جاری برنامه را برای کنترل قواعد کاری برنامه تنظیم می کند
        /// </summary>
        /// <param name="userContext">اطلاعات محیطی و امنیتی کاربر جاری برنامه</param>
        void SetCurrentContext(UserContextViewModel userContext);

        /// <summary>
        /// ساختار درختی گزارش های تعریف شده را به زبان جاری برنامه خوانده و برمی گرداند
        /// </summary>
        /// <param name="localeCode">کد استاندارد  دو حرفی زبان جاری برنامه</param>
        /// <returns>ساختار درختی گزارش ها به زبان جاری برنامه</returns>
        Task<IList<TreeItemViewModel>> GetReportTreeAsync(string localeCode);

        /// <summary>
        /// اطلاعات مورد نیاز برای پیش نمایش یا چاپ گزارش را خوانده و برمی گرداند
        /// </summary>
        /// <param name="reportId">شناسه دیتابیسی گزارش مورد نظر</param>
        /// <param name="localeCode">کد استاندارد  دو حرفی زبان جاری برنامه</param>
        /// <returns>اطلاعات مورد نیاز برای پیش نمایش یا چاپ گزارش</returns>
        Task<PrintInfoViewModel> GetReportAsync(int reportId, string localeCode);

        /// <summary>
        /// اطلاعات مورد نیاز برای طراحی گزارش را خوانده و برمی گرداند
        /// </summary>
        /// <param name="reportId">شناسه دیتابیسی گزارش مورد نظر</param>
        /// <param name="localeCode">کد استاندارد  دو حرفی زبان جاری برنامه</param>
        /// <returns>اطلاعات مورد نیاز برای طراحی گزارش</returns>
        Task<PrintInfoViewModel> GetReportDesignAsync(int reportId, string localeCode);

        /// <summary>
        /// اطلاعات خلاصه گزارش را برای عملیات اعتبارسنجی خوانده و برمی گرداند
        /// </summary>
        /// <param name="reportId">شناسه دیتابیسی گزارش مورد نظر</param>
        /// <returns>اطلاعات خلاصه گزارش موزد نظر</returns>
        Task<ReportSummaryViewModel> GetReportSummaryAsync(int reportId);

        /// <summary>
        /// اطلاعات یک گزارش ذخیره شده کاربری را ذخیره یا بروزرسانی می کند
        /// </summary>
        /// <param name="report">اطلاعات محلی شده گزارش کاربری</param>
        Task SaveUserReportAsync(LocalReportViewModel report);

        /// <summary>
        /// عنوان یک گزارش ذخیره شده کاربری را بروزرسانی می کند
        /// </summary>
        /// <param name="report">اطلاعات محلی شده گزارش کاربری شامل عنوان جدید مورد نظر</param>
        Task SetUserReportCaptionAsync(LocalReportViewModel report);

        /// <summary>
        /// یک گزارش ذخیر شده کاربری را به همراه کلیه اطلاعات مرتبط حذف می کند
        /// </summary>
        /// <param name="reportId">شناسه دیتابیسی گزارش مورد نظر</param>
        Task DeleteUserReportAsync(int reportId);

        /// <summary>
        /// به روش آسنکرون، اطلاعات مورد نیاز در گزارش خلاصه اسناد حسابداری را خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های برنامه برای فیلتر، مرتب سازی و صفحه بندی اطلاعات</param>
        /// <returns>اطلاعات گزارش خلاصه اسناد حسابداری</returns>
        Task<IList<VoucherSummaryViewModel>> GetVoucherSummaryByDateReportAsync(
            GridOptions gridOptions);

        /// <summary>
        /// به روش آسنکرون، نعداد سطرهای اطلاعاتی در گزارش خلاصه اسناد حسابداری را خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های برنامه برای فیلتر، مرتب سازی و صفحه بندی اطلاعات</param>
        /// <returns>تعداد سطرهای گزارش خلاصه اسناد حسابداری</returns>
        Task<int> GetVoucherSummaryByDateCountAsync(GridOptions gridOptions);

        /// <summary>
        /// به روش آسنکرون، اطلاعات مورد نیاز در گزارش فرم مرسوم سند را خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های برنامه برای فیلتر، مرتب سازی و صفحه بندی اطلاعات</param>
        /// <param name="withDetail">مشخص می کند که آیا جزییات سطوح شناور نیز مورد نیاز است یا نه</param>
        /// <returns>اطلاعات گزارش فرم مرسوم سند</returns>
        Task<StandardVoucherViewModel> GetStandardVoucherFormAsync(
            GridOptions gridOptions, bool withDetail = false);
    }
}
