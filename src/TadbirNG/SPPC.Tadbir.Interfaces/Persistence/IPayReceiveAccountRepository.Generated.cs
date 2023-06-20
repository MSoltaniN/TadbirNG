using System.Collections.Generic;
using System.Threading.Tasks;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Utility;
using SPPC.Tadbir.ViewModel.CashFlow;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای مدیریت طرف‌های حساب را تعریف می کند
    /// </summary>
    public interface IPayReceiveAccountRepository
    {
        /// <summary>
        /// به روش آسنکرون، اطلاعات کلیه طرف‌های حساب را خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <param name="payReceiveId">شناسه یکی از فرم های دریافت/پرداخت موجود</param> 
        /// <returns>مجموعه ای از طرف‌های حساب تعریف شده</returns>
        Task<PagedList<PayReceiveAccountViewModel>> GetAccountArticlesAsync(
           int payReceiveId, GridOptions gridOptions);

        /// <summary>
        /// به روش آسنکرون، طرف حساب با شناسه عددی مشخص شده را خوانده و برمی گرداند
        /// </summary>
        /// <param name="accountArticleId">شناسه عددی یکی از طرف‌های حساب موجود</param>
        /// <returns>طرف حساب مشخص شده با شناسه عددی</returns>
        Task<PayReceiveAccountViewModel> GetAccountArticleAsync(int accountArticleId);

        /// <summary>
        /// به روش آسنکرون، اطلاعات خلاصه طرف حساب با شناسه عددی مشخص شده را خوانده و برمی گرداند
        /// </summary>
        /// <param name="accountArticleId">شناسه عددی یکی از طرف‌های حساب موجود</param>
        /// <returns>طرف حساب مشخص شده با شناسه عددی</returns>
        Task<PayReceiveAccountSummaryViewModel> GetAccountArticleSummaryAsync(int accountArticleId);

        /// <summary>
        /// به روش آسنکرون، اطلاعات یک طرف حساب را ایجاد یا اصلاح می کند
        /// </summary>
        /// <param name="accountArticle">طرف حساب مورد نظر برای ایجاد یا اصلاح</param>
        /// <param name="type">نوع فرم مورد نظر برای درخواست جاری - دریافت یا پرداخت</param> 
        /// <returns>اطلاعات نمایشی طرف حساب ایجاد یا اصلاح شده</returns>
        Task<PayReceiveAccountViewModel> SaveAccountArticleAsync(
            PayReceiveAccountViewModel accountArticle, int type);

        /// <summary>
        /// به روش آسنکرون، طرف حساب مشخص شده با شناسه عددی را حذف می کند
        /// </summary>
        /// <param name="accountArticleId">شناسه عددی طرف حساب مورد نظر برای حذف</param>
        /// <param name="type">نوع فرم مورد نظر برای درخواست جاری - دریافت یا پرداخت</param> 
        Task DeleteAccountArticleAsync(int accountArticleId, int type);

        /// <summary>
        /// به روش آسنکرون، طرف‌های حساب مشخص شده با شناسه عددی را حذف می کند
        /// </summary>
        /// <param name="accountArticleIds">مجموعه ای از شناسه های عددی طرف‌های حساب مورد نظر برای حذف</param>
        /// <param name="type">نوع فرم مورد نظر برای درخواست جاری - دریافت یا پرداخت</param> 
        Task DeleteAccountArticlesAsync(IList<int> accountArticleIds, int type);

        /// <summary>
        /// به روش آسنکرون، فرم دریافت/پرداخت مربوط به شناسه های آرتیکل حساب ورودی را برمی گرداند
        /// </summary>
        /// <param name="accountArticleIds">لیست شناسه های آرتیکل حساب</param>
        /// <returns>فرم دریافت/پرداخت مشخص شده با شناسه عددی</returns>
        Task<PayReceiveViewModel> GetPayReceiveAsync(IList<int> accountArticleIds);

        /// <summary>
        /// به روش آسنکرون، ردیف های نامعتبر طرف حساب در فرم دریافت/پرداخت داده شده را حذف می کند
        /// </summary>
        /// <param name="payReceiveId">شناسه فرم دریافت/پرداخت مورد نظر</param>
        /// <param name="type">نوع فرم مورد نظر برای درخواست جاری - دریافت یا پرداخت</param>
        Task DeleteInvalidRowsAccountArticleAsync(int payReceiveId, int type);

        /// <summary>
        /// به روش آسنکرون، وجود ردیف های نامعتبر طرف حساب در فرم دریافت/پرداخت داده شده را بررسی می کند
        /// </summary>
        /// <param name="payReceiveId">شناسه فرم دریافت/پرداخت مورد نظر</param>
        /// <returns>در صورت وجود ردیف مقدار درست و در غیر این صورت نادرست برمی گرداند</returns>
        Task<bool> HasAccountArticleInvalidRowsAsync(int payReceiveId);

        /// <summary>
        /// به روش آسنکرون، ردیف های طرف حساب با بردار حساب مشترک را 
        /// در فرم دریافت/پرداخت داده شده تجمیع می کند
        /// </summary>
        /// <param name="payReceiveId">شناسه فرم دریافت/پرداخت مورد نظر</param>
        /// <param name="type">نوع فرم مورد نظر برای درخواست جاری - دریافت یا پرداخت</param>
        Task AggregateAccountArticleRowsAsync(int payReceiveId, int type);

        /// <summary>
        /// به روش آسنکرون، وجود ردیف برای تجمیع طرف حساب در فرم دریافت/پرداخت داده شده را بررسی می کند
        /// </summary>
        /// <param name="payReceiveId">شناسه فرم دریافت/پرداخت مورد نظر</param>
        /// <returns>در صورت وجود ردیف مقدار درست و در غیر این صورت نادرست برمی گرداند</returns>
        Task<bool> HasAccountArticlestoAggregateAsync(int payReceiveId);
    }
}
