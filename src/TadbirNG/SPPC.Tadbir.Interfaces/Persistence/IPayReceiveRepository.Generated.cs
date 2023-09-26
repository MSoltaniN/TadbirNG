using System;
using System.Threading.Tasks;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.ViewModel.CashFlow;
using SPPC.Tadbir.ViewModel.Finance;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای مدیریت دریافت ها و پرداخت ها را تعریف می کند
    /// </summary>
    public interface IPayReceiveRepository
    {
        /// <summary>
        /// به روش آسنکرون، فرم دریافت/پرداخت با شناسه عددی مشخص شده را خوانده و برمی گرداند
        /// </summary>
        /// <param name="payReceiveId">شناسه عددی یکی از فرم های دریافت یا پرداخت موجود</param>
        /// <param name="type">نوع فرم مورد نظر برای درخواست جاری - دریافت یا پرداخت</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای دریافت اطلاعات لازم از سمت وب</param>
        /// <returns>فرم دریافت/پرداخت مشخص شده با شناسه عددی</returns>
        Task<PayReceiveViewModel> GetPayReceiveAsync(int payReceiveId, int type = 0,
            GridOptions gridOptions = null);

        /// <summary>
        /// به روش آسنکرون، اطلاعات یک فرم دریافت/پرداخت را ایجاد یا اصلاح می کند
        /// </summary>
        /// <param name="payReceive">فرم دریافت/پرداخت مورد نظر برای ایجاد یا اصلاح</param>
        /// <param name="type">نوع فرم مورد نظر برای درخواست جاری - دریافت یا پرداخت</param>
        /// <returns>اطلاعات نمایشی فرم دریافت/پرداخت ایجاد یا اصلاح شده</returns>
        Task<PayReceiveViewModel> SavePayReceiveAsync(PayReceiveViewModel payReceive, int type);

        /// <summary>
        /// به روش آسنکرون، فرم دریافت/پرداخت مشخص شده با شناسه عددی را حذف می کند
        /// </summary>
        /// <param name="payReceiveId">شناسه عددی فرم دریافت/پرداخت مورد نظر برای حذف</param>
        /// <param name="type">نوع فرم مورد نظر برای درخواست جاری - دریافت یا پرداخت</param>
        Task DeletePayReceiveAsync(int payReceiveId, int type);

        /// <summary>
        /// به روش آسنکرون، مشخص می کند که آیا شماره فرم دریافت/پرداخت مورد نظر تکراری است یا نه
        /// </summary>
        /// <param name="payReceive">اطلاعات نمایشی فرم دریافت/پرداخت مورد نظر</param>
        /// <param name="type">نوع فرم مورد نظر برای درخواست جاری - دریافت یا پرداخت</param>
        /// <returns>در صورت تکراری بودن شماره فرم دریافت/پرداخت مقدار درست و
        /// در غیر اینصورت نادرست برمی گرداند</returns>
        Task<bool> IsDuplicateTextNoAsync(PayReceiveViewModel payReceive, int type);

        /// <summary>
        /// به روش آسنکرون، وضعیت تایید فرم دریافت/پرداخت مشخص شده را تغییر می دهد
        /// </summary>
        /// <param name="payReceiveId">شناسه دیتابیسی فرم دریافت/پرداخت مورد نظر</param>
        /// <param name="isConfirmed"> در صورت تایید فرم دریافت/پرداخت با مقدار درست 
        /// و در غیر این صورت با مقدار نادرست پر می شود</param>
        /// <returns>فرم دریافت/پرداخت را همراه با تغییرات تایید برمی گرداند</returns>
        Task<PayReceiveViewModel> SetPayReceiveConfirmationAsync(int payReceiveId, bool isConfirmed);

        /// <summary>
        /// به روش آسنکرون، وضعیت تصویب فرم دریافت/پرداخت مشخص شده را تغییر می دهد
        /// </summary>
        /// <param name="payReceiveId">شناسه دیتابیسی فرم دریافت/پرداخت مورد نظر</param>
        /// <param name="isApproved"> در صورت تصویب فرم دریافت/پرداخت با مقدار درست 
        /// و در غیر این صورت با مقدار نادرست پر می شود</param>
        /// <returns>فرم دریافت/پرداخت را همراه با تغییرات تصویب برمی گرداند</returns>
        Task<PayReceiveViewModel> SetPayReceiveApprovalAsync(int payReceiveId, bool isApproved);

        /// <summary>
        /// به روش آسنکرون، فرم دریافت/پرداخت با شماره مشخص شده را خوانده و برمی گرداند
        /// </summary>
        /// <param name="textNo">شماره فرم دریافت/پرداخت مورد نظر</param>
        /// <param name="type">مشخص می کند که درخواست از نوع پرداختی یا دریافتی می باشد</param>
        /// <returns>فرم دریافت/پرداخت مشخص شده با شماره</returns>
        Task<PayReceiveViewModel> GetPayReceiveByNoAsync(string textNo, int type);

        /// <summary>
        /// به روش آسنکرون، اطلاعات اولین فرم دریافت/پرداخت را از نوع مشخص شده خوانده و برمی گرداند
        /// </summary>
        /// <param name="type">نوع فرم مورد نظر برای درخواست جاری - دریافت یا پرداخت</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>اولین فرم دریافت/پرداخت</returns>
        Task<PayReceiveViewModel> GetFirstPayReceiveAsync(int type, GridOptions gridOptions = null);

        /// <summary>
        /// به روش آسنکرون، اطلاعات آخرین فرم دریافت/پرداخت را از نوع مشخص شده خوانده و برمی گرداند
        /// </summary>
        /// <param name="type">نوع فرم مورد نظر برای درخواست جاری - دریافت یا پرداخت</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>آخرین فرم دریافت/پرداخت</returns>
        Task<PayReceiveViewModel> GetLastPayReceiveAsync(int type, GridOptions gridOptions = null);

        /// <summary>
        /// به روش آسنکرون، اطلاعات فرم دریافت/پرداخت بعدی را از نوع مشخص شده خوانده و برمی گرداند
        /// </summary>
        /// <param name="type">نوع فرم مورد نظر برای درخواست جاری - دریافت یا پرداخت</param>
        /// <param name="currentNo">شماره فرم دریافت/پرداخت جاری در برنامه</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>فرم دریافت/پرداخت بعدی</returns>
        Task<PayReceiveViewModel> GetNextPayReceiveAsync(string currentNo, int type,
            GridOptions gridOptions = null);

        /// <summary>
        /// به روش آسنکرون، اطلاعات فرم دریافت/پرداخت قبلی را از نوع مشخص شده خوانده و برمی گرداند
        /// </summary>
        /// <param name="type">نوع فرم مورد نظر برای درخواست جاری - دریافت یا پرداخت</param>
        /// <param name="currentNo">شماره فرم دریافت/پرداخت جاری در برنامه</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>فرم دریافت/پرداخت قبلی</returns>
        Task<PayReceiveViewModel> GetPreviousPayReceiveAsync(string currentNo, int type,
            GridOptions gridOptions = null);

        /// <summary>
        /// به روش آسنکرون، نمونه ای جدید از فرم دریافت/پرداخت می سازد
        /// </summary>
        /// <param name="type">نوع فرم مورد نظر برای درخواست جاری - دریافت یا پرداخت</param>
        /// <returns>فرم دریافت/پرداخت جدید</returns>
        Task<PayReceiveViewModel> GetNewPayReceiveAsync(int type);

        /// <summary>
        /// به روش آسنکرون، بررسی می کند که آیا برای فرم دریافت/پرداخت داده شده
        /// طرف حساب تعریف شده است یا خیر        
        /// </summary>
        /// <param name="payReceiveId">شناسه فرم دریافت/پرداخت مورد نظر</param>
        /// <returns>در صورت وجود آرتیکل حساب مقدار درست و
        /// در غیر این صورت مقدار نادرست برمی گرداند</returns>
        Task<bool> HasAccountArticleAsync(int payReceiveId);

        /// <summary>
        /// به روش آسنکرون، بررسی می کند که آیا برای فرم دریافت/پرداخت داده شده
        /// حساب نقدی تعریف شده است یا خیر        
        /// </summary>
        /// <param name="payReceiveId">شناسه فرم دریافت/پرداخت مورد نظر</param>
        /// <returns>در صورت وجود آرتیکل حساب نقدی مقدار درست و
        /// در غیر این صورت مقدار نادرست برمی گرداند</returns>
        Task<bool> HasCashAccountArticleAsync(int payReceiveId);

        /// <summary>
        /// به روش آسنکرون، ثبت مالی فرم دریافت/پرداخت را انجام می‌دهد     
        /// </summary>
        /// <param name="payReceiveId">شناسه فرم دریافت/پرداخت مورد نظر</param>
        /// <param name="voucherId">شناسه سندی که ثبت مالی روی آن انجام می‌گیرد‌</param>
        /// <returns>اطلاعات نمایشی سند مالی به همراه آرتیکل‌های ایجاد شده</returns>
        Task<VoucherViewModel> RegisterAsync(int payReceiveId, int voucherId = 0);

        /// <summary>
        /// به روش آسنکرون، برگشت از ثبت مالی فرم دریافت/پرداخت را انجام می‌دهد     
        /// </summary>
        /// <param name="payReceiveId">شناسه فرم دریافت/پرداخت مورد نظر</param>
        /// <param name="type">نوع فرم مورد نظر برای درخواست جاری - دریافت یا پرداخت</param>
        Task UndoRegisterAsync(int payReceiveId, int type);

        /// <summary>
        /// به روش آسنکرون، شناسه سند داده شده را برای ثبت مالی اعتبار سنجی می کند 
        /// </summary>
        /// <param name="voucherId">شناسه سند مورد نظر</param>
        /// <param name="operationalDate">تاریخ فرم عملیاتی مورد نظر برای ثبت مالی</param>
        /// <returns>اگر سند دارای شرایط ثبت مالی باشد مقدار درست
        /// و در غیر اینصورت مقدار نادرست برمی گرداند </returns>
        Task<bool> IsValidVoucherForRegisterAsync(int voucherId, DateTime operationalDate);

        /// <summary>
        /// به روش آسنکرون، سندی که فرم دریافت/پرداخت ورودی روی آن ثبت مالی شده را برمی گرداند
        /// </summary>
        /// <param name="PayReceiveId">شناسه فرم دریافت/پرداخت مورد نظر</param>
        /// <returns>سندی مرتبط با فرم دریافت/پرداخت داده شده</returns>
        Task<VoucherViewModel> GetVoucherOfRegisterAsync(int PayReceiveId);

        /// <summary>
        /// به روش آسنکرون، شناسه آخرین سند باز معتبر برای ثبت مالی را برمی گرداند
        /// </summary>
        /// <param name="operationalDate">تاریخ عملیاتی سند مورد نظر</param>
        /// <returns>شناسه آخرین سند معتبر برای ثبت مالی</returns>
        Task<int> GetLastVoucherforRegisterAsync(DateTime operationalDate);
    }
}
