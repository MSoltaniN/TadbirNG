using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Helpers;
using SPPC.Tadbir.ViewModel.Finance;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای مدیریت اطلاعات آرتیکل های مالی را تعریف می کند.
    /// </summary>
    public interface IVoucherLineRepository
    {
        /// <summary>
        /// به روش آسنکرون، آرتیکل های یک سند مشخص شده با شناسه عددی را از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <param name="voucherId">شناسه یکی از اسناد مالی موجود</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>آرتیکل های سندمشخص شده با شناسه عددی</returns>
        Task<PagedList<VoucherLineViewModel>> GetArticlesAsync(int voucherId, GridOptions gridOptions = null);

        /// <summary>
        /// به روش آسنکرون، اطلاعات سطر سند مالی (آرتیکل) مشخص شده با شناسه عددی را از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <param name="articleId">شناسه عددی آرتیکل موجود</param>
        /// <returns>اطلاعات آرتیکل مشخص شده با شناسه عددی</returns>
        Task<VoucherLineViewModel> GetArticleAsync(int articleId);

        /// <summary>
        /// به روش آسنکرون، تعداد آرتیکل های یک سند مالی را بعد از اعمال فیلتر (در صورت وجود)
        /// از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <typeparam name="TViewModel">نوع مدل نمایشی که برای نمایش اطلاعات موجودیت استفاده می شود</typeparam>
        /// <param name="voucherId">شناسه یکی از اسناد مالی موجود</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>تعداد آرتیکل های سند مالی بعد از اعمال فیلتر</returns>
        Task<int> GetArticleCountAsync<TViewModel>(int voucherId, GridOptions gridOptions = null)
            where TViewModel : class, new();

        /// <summary>
        /// به روش آسنکرون، مدل نمایشی سرفصل حسابداری مشخص شده
        /// را از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <param name="accountId">شناسه دیتابیسی یکی از حساب های موجود</param>
        /// <returns>مدل نمایشی سرفصل حسابداری مورد استفاده در آرتیکل</returns>
        Task<AccountViewModel> GetArticleAccountAsync(int accountId);

        /// <summary>
        /// به روش آسنکرون، مدل نمایشی تفصیلی شناور مشخص شده
        /// را از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <param name="faccountId">شناسه دیتابیسی یکی از تفصیلی های شناور موجود</param>
        /// <returns>مدل نمایشی تفصیلی شناور مورد استفاده در آرتیکل</returns>
        Task<DetailAccountViewModel> GetArticleDetailAccountAsync(int faccountId);

        /// <summary>
        /// به روش آسنکرون، مدل نمایشی مرکز هزینه مشخص شده
        /// را از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <param name="costCenterId">شناسه دیتابیسی یکی از مراکز هزینه موجود</param>
        /// <returns>مدل نمایشی مرکز هزینه مورد استفاده در آرتیکل</returns>
        Task<CostCenterViewModel> GetArticleCostCenterAsync(int costCenterId);

        /// <summary>
        /// به روش آسنکرون، مدل نمایشی پروژه مشخص شده
        /// را از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <param name="projectId">شناسه دیتابیسی یکی از پروژه های موجود</param>
        /// <returns>مدل نمایشی پروژه مورد استفاده در آرتیکل</returns>
        Task<ProjectViewModel> GetArticleProjectAsync(int projectId);

        /// <summary>
        /// به روش آسنکرون، اطلاعات یک سطر سند مالی (آرتیکل) را در محل ذخیره ایجاد یا اصلاح می کند
        /// </summary>
        /// <param name="article">آرتیکل برای ایجاد یا اصلاح</param>
        Task<VoucherLineViewModel> SaveArticleAsync(VoucherLineViewModel article);

        /// <summary>
        /// به روش آسنکرون، علامتگذاری مشخص شده را روی آرتیکل سند اعمال می کند
        /// </summary>
        /// <param name="mark">اطلاعات علامتکذاری آرتیکل</param>
        Task SaveArticleMarkAsync(VoucherLineMarkViewModel mark);

        /// <summary>
        /// به روش آسنکرون، سطر سند مالی (آرتیکل) مشخص شده با شناسه عددی را از محل ذخیره حذف می کند
        /// </summary>
        /// <param name="articleId">شناسه عددی آرتیکل برای حذف</param>
        Task DeleteArticleAsync(int articleId);

        /// <summary>
        /// به روش آسنکرون، آرتیکل های مشخص شده با شناسه عددی را حذف می کند
        /// </summary>
        /// <param name="items">مجموعه شناسه های دیتابیسی سطرهای مورد نظر برای حذف</param>
        Task DeleteArticlesAsync(IEnumerable<int> items);

        /// <summary>
        /// به روش آسنکرون، تعداد کل آرتیکل های ثبت شده را برمیگرداند
        /// </summary>
        /// <returns>تعداد کل آرتیکل ها</returns>
        Task<int> GetAllArticlesCountAsync();

        /// <summary>
        /// به روش آسنکرون، لیست و تعداد آرتیکل ها را بر اساس نوع کنترل سیستم برمیگرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <param name="issueType">نوع کنترل سیستم</param>
        /// <param name="from">تاریخ شروع گزارش</param>
        /// <param name="to">تاریخ پایان گزارش</param>
        /// <returns>لیست و تعداد آرتیکل ها</returns>
        Task<ValueTuple<IList<VoucherLineDetailViewModel>, int>> GetSystemIssueArticlesAsync(
            GridOptions gridOptions, string issueType, DateTime from, DateTime to);
    }
}
