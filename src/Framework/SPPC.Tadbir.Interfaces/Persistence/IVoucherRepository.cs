using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.ViewModel.Finance;
using SPPC.Tadbir.ViewModel.Metadata;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای مدیریت اطلاعات اسناد مالی و آرتیکل های آنها را تعریف می کند.
    /// </summary>
    public interface IVoucherRepository
    {
        #region Voucher Operations

        #region Asynchronous Methods

        /// <summary>
        /// به روش آسنکرون، کلیه اسناد مالی را که در دوره مالی و شعبه مشخص شده تعریف شده اند، از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <param name="fpId">شناسه عددی یکی از دوره های مالی موجود</param>
        /// <param name="branchId">شناسه عددی یکی از شعب موجود</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه ای از اسناد مالی تعریف شده در دوره مالی و شعبه مشخص شده</returns>
        Task<EntityListViewModel<VoucherViewModel>> GetVouchersAsync(
            int fpId, int branchId, GridOptions gridOptions = null);

        /// <summary>
        /// به روش آسنکرون، سند مالی با شناسه عددی مشخص شده را از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <param name="voucherId">شناسه عددی یکی از اسناد مالی موجود</param>
        /// <returns>سند مالی مشخص شده با شناسه عددی</returns>
        Task<EntityItemViewModel<VoucherViewModel>> GetVoucherAsync(int voucherId);

        /// <summary>
        /// به روش آسنکرون، اطلاعات فراداده ای تعریف شده برای سند مالی را از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <returns>اطلاعات فراداده ای تعریف شده برای سند مالی</returns>
        Task<EntityItemViewModel<VoucherViewModel>> GetVoucherMetadataAsync();

        /// <summary>
        /// به روش آسنکرون، مدل نمایشی دوره مالی مورد استفاده در یک سند مالی را از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <param name="voucher">مدل نمایشی سند مالی مورد نظر</param>
        /// <returns>مدل نمایشی دوره مالی به کار رفته در سند مالی</returns>
        Task<FiscalPeriodViewModel> GetVoucherFiscalPeriodAsync(VoucherViewModel voucher);

        /// <summary>
        /// به روش آسنکرون، تعداد اسناد مالی تعریف شده در دوره مالی و شعبه مشخص شده را
        /// از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <param name="fpId">شناسه عددی یکی از دوره های مالی موجود</param>
        /// <param name="branchId">شناسه عددی یکی از شعب موجود</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>تعداد اسناد مالی تعریف شده در دوره مالی و شعبه مشخص شده</returns>
        Task<int> GetCountAsync(int fpId, int branchId, GridOptions gridOptions = null);

        /// <summary>
        /// به روش آسنکرون، اطلاعات یک سند مالی را در محل ذخیره ایجاد یا اصلاح می کند
        /// </summary>
        /// <param name="voucher">سند مالی برای ایجاد یا اصلاح</param>
        /// <returns>مدل نمایشی سند ایجاد یا اصلاح شده</returns>
        Task<VoucherViewModel> SaveVoucherAsync(VoucherViewModel voucher);

        /// <summary>
        /// به روش آسنکرون، سند مالی مشخص شده با شناسه عددی را از محل ذخیره حذف می کند
        /// </summary>
        /// <param name="voucherId">شناسه عددی سند مالی برای حذف</param>
        Task DeleteVoucherAsync(int voucherId);

        /// <summary>
        /// به روش آسنکرون، مشخص می کند که آیا شماره سند مورد نظر تکراری است یا نه
        /// </summary>
        /// <param name="voucher">سند مالی که تکراری بودن شماره آن باید بررسی شود</param>
        /// <returns>مقدار بولی درست در صورت تکراری بودن شماره، در غیر این صورت مقدار بولی نادرست</returns>
        Task<bool> IsDuplicateVoucherNoAsync(VoucherViewModel voucher);

        #endregion

        #region Synchronous Methods (May be removed in the future)

        /// <summary>
        /// کلیه اسناد مالی را که در دوره مالی و شعبه مشخص شده تعریف شده اند، از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <param name="fpId">شناسه عددی یکی از دوره های مالی موجود</param>
        /// <param name="branchId">شناسه عددی یکی از شعب موجود</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه ای از اسناد مالی تعریف شده در دوره مالی و شعبه مشخص شده</returns>
        IList<VoucherViewModel> GetVouchers(int fpId, int branchId, GridOptions gridOptions = null);

        /// <summary>
        /// سند مالی با شناسه عددی مشخص شده را از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <param name="voucherId">شناسه عددی یکی از اسناد مالی موجود</param>
        /// <returns>سند مالی مشخص شده با شناسه عددی</returns>
        VoucherViewModel GetVoucher(int voucherId);

        /// <summary>
        /// تعداد اسناد مالی تعریف شده در دوره مالی و شعبه مشخص شده را از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <param name="fpId">شناسه عددی یکی از دوره های مالی موجود</param>
        /// <param name="branchId">شناسه عددی یکی از شعب موجود</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>تعداد اسناد مالی تعریف شده در دوره مالی و شعبه مشخص شده</returns>
        int GetCount(int fpId, int branchId, GridOptions gridOptions = null);

        /// <summary>
        /// اطلاعات یک سند مالی را در محل ذخیره ایجاد یا اصلاح می کند
        /// </summary>
        /// <param name="voucher">سند مالی برای ایجاد یا اصلاح</param>
        void SaveVoucher(VoucherViewModel voucher);

        /// <summary>
        /// سند مالی مشخص شده با شناسه عددی را از محل ذخیره حذف می کند
        /// </summary>
        /// <param name="voucherId">شناسه عددی سند مالی برای حذف</param>
        void DeleteVoucher(int voucherId);

        /// <summary>
        /// اطلاعات سند مالی داده شده را برای مطابقت با کلیه قواعد کاری برنامه اعتبارسنجی می کند
        /// </summary>
        /// <param name="voucher">سند مالی که باید اعتبارسنجی شود</param>
        /// <returns>مقدار بولی درست در صورت مطابقت کامل با قواعد کاری، در غیر این صورت مقدار بولی نادرست</returns>
        bool IsValidVoucher(VoucherViewModel voucher);

        #endregion

        #endregion

        #region Voucher Line Operations

        #region Asynchronous Methods

        /// <summary>
        /// به روش آسنکرون، آرتیکل های یک سند مشخص شده با شناسه عددی را از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <param name="voucherId">شناسه یکی از اسناد مالی موجود</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>آرتیکل های سندمشخص شده با شناسه عددی</returns>
        Task<EntityListViewModel<VoucherLineViewModel>> GetArticlesAsync(
            int voucherId, GridOptions gridOptions = null);

        /// <summary>
        /// به روش آسنکرون، اطلاعات سطر سند مالی (آرتیکل) مشخص شده با شناسه عددی را از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <param name="articleId">شناسه عددی آرتیکل موجود</param>
        /// <returns>اطلاعات آرتیکل مشخص شده با شناسه عددی</returns>
        Task<EntityItemViewModel<VoucherLineViewModel>> GetArticleAsync(int articleId);

        /// <summary>
        /// به روش آسنکرون، اطلاعات فراداده ای تعریف شده برای آرتیکل سند مالی را از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <returns>اطلاعات فراداده ای تعریف شده برای آرتیکل سند مالی</returns>
        Task<EntityItemViewModel<VoucherLineViewModel>> GetVoucherLineMetadataAsync();

        /// <summary>
        /// به روش آسنکرون، تعداد آرتیکل های یک سند مالی را بعد از اعمال فیلتر (در صورت وجود)
        /// از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <param name="voucherId">شناسه یکی از اسناد مالی موجود</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>تعداد آرتیکل های سند مالی بعد از اعمال فیلتر</returns>
        Task<int> GetArticleCountAsync(int voucherId, GridOptions gridOptions = null);

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
        /// به روش آسنکرون، سطر سند مالی (آرتیکل) مشخص شده با شناسه عددی را از محل ذخیره حذف می کند
        /// </summary>
        /// <param name="articleId">شناسه عددی آرتیکل برای حذف</param>
        Task DeleteArticleAsync(int articleId);

        #endregion

        #region Synchronous Methods (May be removed in the future)

        /// <summary>
        /// آرتیکل های یک سند مشخص شده با شناسه عددی را از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <param name="voucherId">شناسه یکی از اسناد مالی موجود</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>آرتیکل های سندمشخص شده با شناسه عددی</returns>
        IList<VoucherLineViewModel> GetArticles(int voucherId, GridOptions gridOptions = null);

        /// <summary>
        /// اطلاعات سطر سند مالی (آرتیکل) مشخص شده با شناسه عددی را از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <param name="articleId">شناسه عددی آرتیکل موجود</param>
        /// <returns>اطلاعات آرتیکل مشخص شده با شناسه عددی</returns>
        VoucherLineViewModel GetArticle(int articleId);

        /// <summary>
        /// تعداد آرتیکل های یک سند مالی را بعد از اعمال فیلتر (در صورت وجود)
        /// از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <param name="voucherId">شناسه یکی از اسناد مالی موجود</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>تعداد آرتیکل های سند مالی بعد از اعمال فیلتر</returns>
        int GetArticleCount(int voucherId, GridOptions gridOptions = null);

        /// <summary>
        /// اطلاعات یک سطر سند مالی (آرتیکل) را در محل ذخیره ایجاد یا اصلاح می کند
        /// </summary>
        /// <param name="article">آرتیکل برای ایجاد یا اصلاح</param>
        void SaveArticle(VoucherLineViewModel article);

        /// <summary>
        /// سطر سند مالی (آرتیکل) مشخص شده با شناسه عددی را از محل ذخیره حذف می کند
        /// </summary>
        /// <param name="articleId">شناسه عددی آرتیکل برای حذف</param>
        void DeleteArticle(int articleId);

        #endregion

        #endregion
    }
}
