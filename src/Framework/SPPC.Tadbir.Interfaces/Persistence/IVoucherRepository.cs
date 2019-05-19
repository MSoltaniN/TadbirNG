using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.ViewModel.Auth;
using SPPC.Tadbir.ViewModel.Finance;
using SPPC.Tadbir.ViewModel.Metadata;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای مدیریت اطلاعات اسناد مالی و آرتیکل های آنها را تعریف می کند.
    /// </summary>
    public interface IVoucherRepository
    {
        /// <summary>
        /// به روش آسنکرون، کلیه اسناد مالی از نوع مفهومی سند حسابداری را که در دوره مالی و شعبه جاری
        /// تعریف شده اند، از دیتابیس خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه ای از اسناد مالی تعریف شده در دوره مالی و شعبه جاری</returns>
        Task<IList<VoucherViewModel>> GetVouchersAsync(GridOptions gridOptions = null);

        /// <summary>
        /// به روش آسنکرون، سند مالی با شناسه عددی مشخص شده را از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <param name="voucherId">شناسه عددی یکی از اسناد مالی موجود</param>
        /// <returns>سند مالی مشخص شده با شناسه عددی</returns>
        Task<VoucherViewModel> GetVoucherAsync(int voucherId);

        /// <summary>
        /// به روش آسنکرون، سند مالی جدیدی را با مقادیر پیشنهادی ایجاد کرده و برمی گرداند
        /// </summary>
        /// <returns>سند مالی جدید با مقادیر پیشنهادی</returns>
        Task<VoucherViewModel> GetNewVoucherAsync();

        /// <summary>
        /// به روش آسنکرون، سند مالی با شماره مشخص شده را خوانده و برمی گرداند
        /// </summary>
        /// <param name="voucherNo">شماره یکی از اسناد مالی موجود</param>
        /// <returns>سند مالی مشخص شده با شماره</returns>
        Task<VoucherViewModel> GetVoucherByNoAsync(int voucherNo);

        /// <summary>
        /// به روش آسنکرون، اولین سند مالی را خوانده و برمی گرداند
        /// </summary>
        /// <returns>اولین سند مالی</returns>
        Task<VoucherViewModel> GetFirstVoucherAsync();

        /// <summary>
        /// به روش آسنکرون، اطلاعات سند مالی قبلی را خوانده و برمی گرداند
        /// </summary>
        /// <param name="currentNo">شماره سند مالی جاری در برنامه</param>
        /// <returns>سند مالی قبلی</returns>
        Task<VoucherViewModel> GetPreviousVoucherAsync(int currentNo);

        /// <summary>
        /// به روش آسنکرون، اطلاعات سند مالی بعدی را خوانده و برمی گرداند
        /// </summary>
        /// <param name="currentNo">شماره سند مالی جاری در برنامه</param>
        /// <returns>سند مالی بعدی</returns>
        Task<VoucherViewModel> GetNextVoucherAsync(int currentNo);

        /// <summary>
        /// به روش آسنکرون، آخرین سند مالی را خوانده و برمی گرداند
        /// </summary>
        /// <returns>آخرین سند مالی</returns>
        Task<VoucherViewModel> GetLastVoucherAsync();

        /// <summary>
        /// به روش آسنکرون، اطلاعات فراداده ای تعریف شده برای سند مالی را از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <returns>اطلاعات فراداده ای تعریف شده برای سند مالی</returns>
        Task<ViewViewModel> GetVoucherMetadataAsync();

        /// <summary>
        /// به روش آسنکرون، مدل نمایشی دوره مالی مورد استفاده در یک سند مالی را از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <param name="voucher">مدل نمایشی سند مالی مورد نظر</param>
        /// <returns>مدل نمایشی دوره مالی به کار رفته در سند مالی</returns>
        Task<FiscalPeriodViewModel> GetVoucherFiscalPeriodAsync(VoucherViewModel voucher);

        /// <summary>
        /// به روش آسنکرون، تعداد اسناد مالی تعریف شده در دوره مالی و شعبه جاری را
        /// از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <typeparam name="TViewModel">نوع مدل نمایشی که برای نمایش اطلاعات موجودیت استفاده می شود</typeparam>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>تعداد اسناد مالی تعریف شده در دوره مالی و شعبه جاری</returns>
        Task<int> GetCountAsync<TViewModel>(GridOptions gridOptions = null)
            where TViewModel : class, new();

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

        /// <summary>
        /// اطلاعات محیطی کاربر جاری برنامه را برای ایجاد لاگ های عملیاتی تنظیم می کند
        /// </summary>
        /// <param name="userContext">اطلاعات دسترسی کاربر به منابع محدود شده مانند نقش ها، دوره های مالی و شعبه ها</param>
        void SetCurrentContext(UserContextViewModel userContext);

        /// <summary>
        /// وضعیت ثبتی سند مالی را به وضعیت داده شده تغییر می دهد
        /// </summary>
        /// <param name="voucherId">شناسه دیتابیسی یکی از اسناد مالی موجود</param>
        /// <param name="status">وضعیت جدید مورد نظر برای سند مالی</param>
        Task SetVoucherStatusAsync(int voucherId, DocumentStatusValue status);
    }
}
