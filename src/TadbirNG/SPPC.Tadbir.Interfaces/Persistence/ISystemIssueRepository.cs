using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Utility;
using SPPC.Tadbir.ViewModel.Finance;
using SPPC.Tadbir.ViewModel.Reporting;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای مدیریت کنترل سیستم را تعریف می کند.
    /// </summary>
    public interface ISystemIssueRepository
    {
        /// <summary>
        /// به روش آسنکرون، اطلاعات نمایشی موضوعات سیستم قابل دسترسی توسط کاربر مشخص شده را خوانده و برمی گرداند
        /// </summary>
        /// <param name="userId">شناسه یکتای یکی از کاربران موجود</param>
        /// <returns>مجموعه ای از موضوعات سیستم قابل دسترسی توسط کاربر</returns>
        Task<IList<SystemIssueViewModel>> GetUserSystemIssuesAsync(int userId);

        /// <summary>
        /// اطلاعات خلاصه ای از اشکالات موجود در سیستم را خوانده و برمی گرداند
        /// </summary>
        /// <param name="issueId">شناسه دیتابیسی یکی از اشکالات تعریف شده که به همراه اشکالات زیرمجموعه در ساختار درختی
        /// برای گزارشگیری استفاده می شود</param>
        /// <param name="gridOptions"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns>فهرست اطلاعات خلاصه برای اشکالات موجود</returns>
        Task<IList<SystemIssueSummaryViewModel>> GetSystemIssueSummaries(
            int issueId, GridOptions gridOptions, DateTime from, DateTime to);

        /// <summary>
        /// به روش آسنکرون، لیست و تعداد اسناد دارای نا تراز را برمیگرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <param name="from">تاریخ شروع گزارش</param>
        /// <param name="to">تاریخ پایان گزارش</param>
        /// <returns>لیست و تعداد اسناد نا تراز</returns>
        Task<PagedList<VoucherViewModel>> GetUnbalancedVouchersAsync(
            GridOptions gridOptions, DateTime from, DateTime to);

        /// <summary>
        /// به روش آسنکرون، لیست و تعداد اسناد فاقد آرتیکل را برمیگرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <param name="from">تاریخ شروع گزارش</param>
        /// <param name="to">تاریخ پایان گزارش</param>
        /// <returns>لیست و تعداد اسناد فاقد آرتیکل</returns>
        Task<PagedList<VoucherViewModel>> GetVouchersWithNoArticleAsync(
            GridOptions gridOptions, DateTime from, DateTime to);

        /// <summary>
        /// به روش آسنکرون، لیست و تعداد شماره اسناد جا افتاده را برمیگرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <param name="from">تاریخ شروع گزارش</param>
        /// <param name="to">تاریخ پایان گزارش</param>
        /// <returns>لیست و تعداد شماره اسناد جا افتاده</returns>
        Task<PagedList<NumberListViewModel>> GetMissingVoucherNumbersAsync(
            GridOptions gridOptions, DateTime from, DateTime to);

        /// <summary>
        /// به روش آسنکرون، لیست و تعداد آرتیکل ها را بر اساس نوع کنترل سیستم برمیگرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <param name="issueType">نوع کنترل سیستم</param>
        /// <param name="from">تاریخ شروع گزارش</param>
        /// <param name="to">تاریخ پایان گزارش</param>
        /// <returns>لیست و تعداد آرتیکل ها</returns>
        Task<PagedList<VoucherLineDetailViewModel>> GetSystemIssueArticlesAsync(
            GridOptions gridOptions, string issueType, DateTime from, DateTime to);
    }
}
