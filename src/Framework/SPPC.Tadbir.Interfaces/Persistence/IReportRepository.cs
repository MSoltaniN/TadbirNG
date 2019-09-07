using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Values;
using SPPC.Tadbir.ViewModel.Auth;
using SPPC.Tadbir.ViewModel.Metadata;
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
        /// به روش آسنکرون، مانده حساب مشخص شده را محاسبه کرده و برمی گرداند
        /// </summary>
        /// <param name="accountId">شناسه دیتابیسی حساب مورد نظر</param>
        /// <param name="date">تاریخ مورد نظر برای محاسبه مانده</param>
        /// <returns>مانده حساب مشخص شده به صورت علامتدار : عدد مثبت نمایانگر مانده بدهکار
        /// و عدد منفی نمایانگر مانده بستانکار است</returns>
        Task<decimal> GetAccountBalanceAsync(int accountId, DateTime date);

        /// <summary>
        /// به روش آسنکرون، مانده حساب مشخص شده را محاسبه کرده و برمی گرداند
        /// </summary>
        /// <param name="accountId">شناسه دیتابیسی حساب مورد نظر</param>
        /// <param name="number">شماره سندی که مانده با توجه به کلیه سندهای پیش از آن در دوره مالی جاری محاسبه می شود</param>
        /// <returns>مانده حساب مشخص شده به صورت علامتدار : عدد مثبت نمایانگر مانده بدهکار
        /// و عدد منفی نمایانگر مانده بستانکار است</returns>
        Task<decimal> GetAccountBalanceAsync(int accountId, int number);

        /// <summary>
        /// به روش آسنکرون، مانده تفصیلی شناور مشخص شده را محاسبه کرده و برمی گرداند
        /// </summary>
        /// <param name="faccountId">شناسه دیتابیسی تفصیلی شناور مورد نظر</param>
        /// <param name="date">تاریخ مورد نظر برای محاسبه مانده</param>
        /// <returns>مانده حساب مشخص شده به صورت علامتدار : عدد مثبت نمایانگر مانده بدهکار
        /// و عدد منفی نمایانگر مانده بستانکار است</returns>
        Task<decimal> GetDetailAccountBalanceAsync(int faccountId, DateTime date);

        /// <summary>
        /// به روش آسنکرون، مانده تفصیلی شناور مشخص شده را محاسبه کرده و برمی گرداند
        /// </summary>
        /// <param name="faccountId">شناسه دیتابیسی تفصیلی شناور مورد نظر</param>
        /// <param name="number">شماره سندی که مانده با توجه به کلیه سندهای پیش از آن در دوره مالی جاری محاسبه می شود</param>
        /// <returns>مانده حساب مشخص شده به صورت علامتدار : عدد مثبت نمایانگر مانده بدهکار
        /// و عدد منفی نمایانگر مانده بستانکار است</returns>
        Task<decimal> GetDetailAccountBalanceAsync(int faccountId, int number);

        /// <summary>
        /// به روش آسنکرون، مانده مرکز هزینه مشخص شده را محاسبه کرده و برمی گرداند
        /// </summary>
        /// <param name="ccenterId">شناسه دیتابیسی مرکز هزینه مورد نظر</param>
        /// <param name="date">تاریخ مورد نظر برای محاسبه مانده</param>
        /// <returns>مانده حساب مشخص شده به صورت علامتدار : عدد مثبت نمایانگر مانده بدهکار
        /// و عدد منفی نمایانگر مانده بستانکار است</returns>
        Task<decimal> GetCostCenterBalanceAsync(int ccenterId, DateTime date);

        /// <summary>
        /// به روش آسنکرون، مانده مرکز هزینه مشخص شده را محاسبه کرده و برمی گرداند
        /// </summary>
        /// <param name="ccenterId">شناسه دیتابیسی مرکز هزینه مورد نظر</param>
        /// <param name="number">شماره سندی که مانده با توجه به کلیه سندهای پیش از آن در دوره مالی جاری محاسبه می شود</param>
        /// <returns>مانده حساب مشخص شده به صورت علامتدار : عدد مثبت نمایانگر مانده بدهکار
        /// و عدد منفی نمایانگر مانده بستانکار است</returns>
        Task<decimal> GetCostCenterBalanceAsync(int ccenterId, int number);

        /// <summary>
        /// به روش آسنکرون، مانده پروژه مشخص شده را محاسبه کرده و برمی گرداند
        /// </summary>
        /// <param name="projectId">شناسه دیتابیسی پروژه مورد نظر</param>
        /// <param name="date">تاریخ مورد نظر برای محاسبه مانده</param>
        /// <returns>مانده حساب مشخص شده به صورت علامتدار : عدد مثبت نمایانگر مانده بدهکار
        /// و عدد منفی نمایانگر مانده بستانکار است</returns>
        Task<decimal> GetProjectBalanceAsync(int projectId, DateTime date);

        /// <summary>
        /// به روش آسنکرون، مانده پروژه مشخص شده را محاسبه کرده و برمی گرداند
        /// </summary>
        /// <param name="projectId">شناسه دیتابیسی پروژه مورد نظر</param>
        /// <param name="number">شماره سندی که مانده با توجه به کلیه سندهای پیش از آن در دوره مالی جاری محاسبه می شود</param>
        /// <returns>مانده حساب مشخص شده به صورت علامتدار : عدد مثبت نمایانگر مانده بدهکار
        /// و عدد منفی نمایانگر مانده بستانکار است</returns>
        Task<decimal> GetProjectBalanceAsync(int projectId, int number);

        /// <summary>
        /// به روش آسنکرون، تاریخ سند سیستمی با نوع داده شده را خوانده و برمی گرداند
        /// </summary>
        /// <param name="type">یکی از انواع تعریف شده برای سندهای سیستمی</param>
        /// <returns>تاریخ سند مورد نظر یا اگر سند مورد نظر پیدا نشود، بدون مقدار</returns>
        Task<DateTime?> GetSpecialVoucherDateAsync(VoucherType type);

        /// <summary>
        /// مانده سرفصل حسابداری مشخص شده را در سنتد مالی از نوع داده شده محاسبه کرده و برمی گرداند
        /// </summary>
        /// <param name="type">نوع سیستمی مورد نظر برای محاسبه مانده</param>
        /// <param name="accountId">شناسه دیتابیسی سرفصل حسابداری مورد نظر</param>
        /// <returns>مانده محاسبه شده برای سرفصل حسابداری</returns>
        Task<decimal> GetSpecialVoucherBalanceAsync(VoucherType type, int accountId);

        /// <summary>
        /// اطلاعات فراداده ای یکی از نماهای اطلاعاتی گزارشی را خوانده و برمی گرداند
        /// </summary>
        /// <param name="viewId">شناسه دیتابیسی نمای اطلاعاتی مورد نظر</param>
        /// <returns>اطلاعات فراداده ای نمای گزارشی</returns>
        Task<ViewViewModel> GetReportMetadataByViewAsync(int viewId);

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
