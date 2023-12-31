﻿using System.Collections.Generic;
using System.Threading.Tasks;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Utility;
using SPPC.Tadbir.ViewModel.CashFlow;
using SPPC.Tadbir.ViewModel.Core;
using SPPC.Tadbir.ViewModel.Finance;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای مدیریت اطلاعات اسناد مالی و آرتیکل های آنها را تعریف می کند.
    /// </summary>
    public partial interface IVoucherRepository
    {
        /// <summary>
        /// به روش آسنکرون، کلیه اسناد مالی از نوع مفهومی سند حسابداری را که در دوره مالی و شعبه جاری
        /// تعریف شده اند، از دیتابیس خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه ای از اسناد مالی تعریف شده در دوره مالی و شعبه جاری</returns>
        Task<PagedList<VoucherViewModel>> GetVouchersAsync(GridOptions gridOptions);

        /// <summary>
        /// به روش آسنکرون، سند مالی با شناسه عددی مشخص شده را خوانده و برمی گرداند
        /// </summary>
        /// <param name="voucherId">شناسه عددی یکی از اسناد مالی موجود</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>سند مالی مشخص شده با شناسه عددی</returns>
        Task<VoucherViewModel> GetVoucherAsync(int voucherId, GridOptions gridOptions = null);

        /// <summary>
        /// به روش آسنکرون، سند مالی جدیدی را با مقادیر پیشنهادی ایجاد کرده و برمی گرداند
        /// </summary>
        /// <param name="subject">نوع مفهومی مورد نظر برای سند جدید که پیش فرض آن سند عادی است</param>
        /// <returns>سند مالی جدید با مقادیر پیشنهادی</returns>
        Task<VoucherViewModel> GetNewVoucherAsync(SubjectType subject = SubjectType.Normal);

        /// <summary>
        /// به روش آسنکرون، سند مالی با شماره مشخص شده را خوانده و برمی گرداند
        /// </summary>
        /// <param name="voucherNo">شماره یکی از اسناد مالی موجود</param>
        /// <param name="subject">نوع مفهومی مورد نظر برای سند که پیش فرض آن سند عادی است</param>
        /// <returns>سند مالی مشخص شده با شماره</returns>
        Task<VoucherViewModel> GetVoucherByNoAsync(int voucherNo, SubjectType subject = SubjectType.Normal);

        /// <summary>
        /// به روش آسنکرون، اولین سند مالی را خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>اولین سند مالی</returns>
        Task<VoucherViewModel> GetFirstVoucherAsync(GridOptions gridOptions);

        /// <summary>
        /// به روش آسنکرون، اولین سند مالی از نوع مشخص شده را خوانده و برمی گرداند
        /// </summary>
        /// <param name="subject">نوع مفهومی سند مورد نظر که به صورت پیش فرض سند عادی است</param>
        /// <returns>اولین سند مالی از نوع مشخص شده</returns>
        Task<VoucherViewModel> GetFirstVoucherAsync(SubjectType subject = SubjectType.Normal);

        /// <summary>
        /// به روش آسنکرون، اطلاعات سند مالی قبلی را خوانده و برمی گرداند
        /// </summary>
        /// <param name="currentNo">شماره سند مالی جاری در برنامه</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>سند مالی قبلی</returns>
        Task<VoucherViewModel> GetPreviousVoucherAsync(int currentNo, GridOptions gridOptions);

        /// <summary>
        /// به روش آسنکرون، اطلاعات سند مالی قبلی از نوع مشخص شده را خوانده و برمی گرداند
        /// </summary>
        /// <param name="currentNo">شماره سند مالی جاری در برنامه</param>
        /// <param name="subject">نوع مفهومی سند مورد نظر که به صورت پیش فرض سند عادی است</param>
        /// <returns>سند مالی قبلی از نوع مشخص شده</returns>
        Task<VoucherViewModel> GetPreviousVoucherAsync(int currentNo, SubjectType subject = SubjectType.Normal);

        /// <summary>
        /// به روش آسنکرون، اطلاعات سند مالی بعدی را خوانده و برمی گرداند
        /// </summary>
        /// <param name="currentNo">شماره سند مالی جاری در برنامه</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>سند مالی بعدی</returns>
        Task<VoucherViewModel> GetNextVoucherAsync(int currentNo, GridOptions gridOptions);

        /// <summary>
        /// به روش آسنکرون، اطلاعات سند مالی بعدی از نوع مشخص شده را خوانده و برمی گرداند
        /// </summary>
        /// <param name="currentNo">شماره سند مالی جاری در برنامه</param>
        /// <param name="subject">نوع مفهومی سند مورد نظر که به صورت پیش فرض سند عادی است</param>
        /// <returns>سند مالی بعدی از نوع مشخص شده</returns>
        Task<VoucherViewModel> GetNextVoucherAsync(int currentNo, SubjectType subject = SubjectType.Normal);

        /// <summary>
        /// به روش آسنکرون، آخرین سند مالی را خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>آخرین سند مالی</returns>
        Task<VoucherViewModel> GetLastVoucherAsync(GridOptions gridOptions);

        /// <summary>
        /// به روش آسنکرون، آخرین سند مالی از نوع مشخص شده را خوانده و برمی گرداند
        /// </summary>
        /// <param name="subject">نوع مفهومی سند مورد نظر که به صورت پیش فرض سند عادی است</param>
        /// <returns>آخرین سند مالی از نوع مشخص شده</returns>
        Task<VoucherViewModel> GetLastVoucherAsync(SubjectType subject = SubjectType.Normal);

        /// <summary>
        /// به روش آسنکرون، مدل نمایشی دوره مالی مورد استفاده در یک سند مالی را خوانده و برمی گرداند
        /// </summary>
        /// <param name="voucher">مدل نمایشی سند مالی مورد نظر</param>
        /// <returns>مدل نمایشی دوره مالی به کار رفته در سند مالی</returns>
        Task<FiscalPeriodViewModel> GetVoucherFiscalPeriodAsync(VoucherViewModel voucher);

        /// <summary>
        /// به روش آسنکرون، تعداد اسناد مالی تعریف شده در دوره مالی و شعبه جاری را
        /// خوانده و برمی گرداند
        /// </summary>
        /// <typeparam name="TViewModel">نوع مدل نمایشی که برای نمایش اطلاعات موجودیت استفاده می شود</typeparam>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>تعداد اسناد مالی تعریف شده در دوره مالی و شعبه جاری</returns>
        Task<int> GetCountAsync<TViewModel>(GridOptions gridOptions = null)
            where TViewModel : class, new();

        /// <summary>
        /// به روش آسنکرون، تعداد سندهای دوره مالی جاری با وضعیت ثبتی داده شده را خوانده و برمی گرداند
        /// </summary>
        /// <param name="status">وضعیت ثبتی مورد نظر برای سند</param>
        /// <param name="subject">نوع مفهومی اسناد مورد نظر که به طور پیش فرض سند عادی است</param>
        /// <returns>تعداد سندهای دوره مالی جاری با وضعیت ثبتی مورد نظر</returns>
        Task<int> GetCountByStatusAsync(DocumentStatusId status, SubjectType subject = SubjectType.Normal);

        /// <summary>
        /// به روش آسنکرون، اطلاعات محدوده سندهای قابل دسترسی توسط کاربر جاری برنامه را خوانده و برمی گرداند
        /// </summary>
        /// <returns>محدوده سندهای قابل دسترسی توسط کاربر جاری</returns>
        Task<NumberedItemRangeViewModel> GetVoucherRangeInfoAsync();

        /// <summary>
        /// به روش آسنکرون، شماره روزانه سند را با توجه به سندهای موجود در یک تاریخ تنظیم می کند
        /// </summary>
        /// <param name="voucher">اطلاعات نمایشی سند جدید</param>
        Task SetVoucherDailyNoAsync(VoucherViewModel voucher);

        /// <summary>
        /// به روش آسنکرون، اطلاعات یک سند مالی را در محل ذخیره ایجاد یا اصلاح می کند
        /// </summary>
        /// <param name="voucher">سند مالی برای ایجاد یا اصلاح</param>
        /// <returns>مدل نمایشی سند ایجاد یا اصلاح شده</returns>
        Task<VoucherViewModel> SaveVoucherAsync(VoucherViewModel voucher);

        /// <summary>
        /// به روش آسنکرون، سند مالی مشخص شده با شناسه عددی را حذف می کند
        /// </summary>
        /// <param name="voucherId">شناسه عددی سند مالی برای حذف</param>
        Task DeleteVoucherAsync(int voucherId);

        /// <summary>
        /// به روش آسنکرون، اسناد مالی مشخص شده با شناسه عددی را حذف می کند
        /// </summary>
        /// <param name="items">مجموعه شناسه های دیتابیسی سطرهای مورد نظر برای حذف</param>
        Task DeleteVouchersAsync(IEnumerable<int> items);

        /// <summary>
        /// به روش آسنکرون، مشخص می کند که آیا شماره سند مورد نظر تکراری است یا نه
        /// </summary>
        /// <param name="voucher">سند مالی که تکراری بودن شماره آن باید بررسی شود</param>
        /// <returns>مقدار بولی درست در صورت تکراری بودن شماره، در غیر این صورت مقدار بولی نادرست</returns>
        Task<bool> IsDuplicateVoucherNoAsync(VoucherViewModel voucher);

        /// <summary>
        /// به روش آسنکرون، مشخص می کند که آیا شماره روزانه سند مورد نظر، با توجه به تاریخ سند، تکراری است یا نه
        /// </summary>
        /// <param name="voucher">سند مالی که تکراری بودن شماره روزانه آن باید بررسی شود</param>
        /// <returns>مقدار بولی درست در صورت تکراری بودن شماره، در غیر این صورت مقدار بولی نادرست</returns>
        Task<bool> IsDuplicateVoucherDailyNoAsync(VoucherViewModel voucher);

        /// <summary>
        /// مشخص می کند که سند حسابداری داده شده قابل تبدیل به سند پیش نویس هست یا نه؟
        /// </summary>
        /// <param name="voucher">اطلاعات نمایشی سند حسابداری مورد نظر</param>
        /// <returns>اگر سند داده شده قابل تبدیل به سند پیش نویس باشد مقدار بولی درست و در غیر این صورت
        /// مقدار بولی نادرست را برمی گرداند</returns>
        bool CanSaveAsDraftVoucher(VoucherViewModel voucher);

        /// <summary>
        /// وضعیت ثبتی سند مالی را به وضعیت داده شده تغییر می دهد
        /// </summary>
        /// <param name="voucherId">شناسه دیتابیسی یکی از اسناد مالی موجود</param>
        /// <param name="status">وضعیت جدید مورد نظر برای سند مالی</param>
        /// <returns>سند را همراه با تغییرات وضعیت سند برمی گرداند</returns>
        Task<VoucherViewModel> SetVoucherStatusAsync(int voucherId, DocumentStatusId status);

        /// <summary>
        /// به روش آسنکرون، وضعیت تایید سند مشخص شده را تغییر می دهد
        /// </summary>
        /// <param name="voucherId">شناسه دیتابیسی سند مورد نظر</param>
        /// <param name="isConfirmed">مشخص می کند که سند مورد نظر تایید شده است یا نه؟ مقدار بولی درست
        /// یعنی سند تایید شده و مقدار بولی نادرست یعنی سند برگشت از تایید شده است.</param>
        Task SetVoucherConfirmationAsync(int voucherId, bool isConfirmed);

        /// <summary>
        /// به روش آسنکرون، وضعیت تصویب سند مشخص شده را تغییر می دهد
        /// </summary>
        /// <param name="voucherId">شناسه دیتابیسی سند مورد نظر</param>
        /// <param name="isApproved">مشخص می کند که سند مورد نظر تصویب شده است یا نه؟ مقدار بولی درست
        /// یعنی سند تصویب شده و مقدار بولی نادرست یعنی سند برگشت از تصویب شده است.</param>
        Task SetVoucherApprovalAsync(int voucherId, bool isApproved);

        /// /// <summary>
        /// به روش آسنکرون، وضعیت ثبتی اسناد مالی مشخص شده با شناسه عددی راتغییر می دهد
        /// </summary>
        /// <param name="items">مجموعه شناسه های دیتابیسی سطرهای مورد نظر برای تغییر وضعیت</param>
        /// <param name="status">وضعیت جدید مورد نظر برای سند مالی</param>
        Task SetVouchersStatusAsync(IEnumerable<int> items, DocumentStatusId status);

        /// <summary>
        /// به روش آسنکرون، وضعیت تأیید یا تصویب اسناد مالی مشخص شده را تغییر می دهد
        /// </summary>
        /// <param name="items">مجموعه شناسه های دیتابیسی سطرهای مورد نظر برای تغییر وضعیت</param>
        /// <param name="isConfirmed">مشخص می کند که تغییر مورد نظر تأیید/تصویب است یا رفع تأیید/تصویب</param>
        Task SetVouchersConfirmApproveStatusAsync(IEnumerable<int> items, bool isConfirmed);

        /// <summary>
        /// عمل داده شده را روی سند با شناسه دیتابیسی مشخص شده بررسی و اعتبارسنجی می کند
        /// </summary>
        /// <param name="voucherId">شناسه دیتابیسی سند مورد نظر</param>
        /// <param name="action">عمل مورد نظر</param>
        /// <returns>در صورت مجاز بودن عمل، مقدار خالی و در غیر این صورت
        /// پیغام خطای عملیاتی را برمی گرداند</returns>
        Task<GroupActionResultViewModel> ValidateVoucherActionAsync(int voucherId, string action);

        /// <summary>
        /// به روش آسنکرون، اطلاعات خلاصه سند مالی را خوانده و برمی گرداند
        /// </summary>
        /// <param name="voucherId">شناسه دیتابیسی سند مالی مورد نظر</param>
        /// <returns>اطلاعات خلاصه سند مالی با شناسه دیتابیسی داده شده</returns>
        Task<VoucherInfoViewModel> GetVoucherInfoAsync(int voucherId);

        /// <summary>
        /// به روش آسنکرون، بررسی می‌کند که سند دارای آرتیکل سیستمی هست یا خیر
        /// </summary>
        /// <param name="voucherId">شناسه دیتابیسی سند مالی مورد نظر</param>
        /// <returns>اگر سند دارای آرتیکل سیستمی باشد مقدار درست و
        /// در غیر این صورت نادرست برمی گرداند.</returns>
        Task<bool> HasSystemicArticleAsync(int voucherId);
    }
}
