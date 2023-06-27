using System.Collections.Generic;
using System.Threading.Tasks;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Utility;
using SPPC.Tadbir.ViewModel.CashFlow;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای مدیریت حساب‌های نقدیی را تعریف می کند
    /// </summary>
    public interface IPayReceiveCashAccountRepository
    {
        /// <summary>
        /// به روش آسنکرون، اطلاعات کلیه حساب‌های نقدی را خوانده و برمی گرداند
        /// </summary>
        /// <param name="payReceiveId">شناسه یکی از فرم های دریافت/پرداخت موجود</param> 
        /// <param name="type">نوع فرم مورد نظر برای درخواست جاری - دریافت یا پرداخت</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه‌ای از حساب‌های نقدی تعریف شده</returns>
        Task<PagedList<PayReceiveCashAccountViewModel>> GetCashAccountArticlesAsync(
           int payReceiveId, int type, GridOptions gridOptions);

        /// <summary>
        /// به روش آسنکرون، حساب نقدی با شناسه عددی مشخص شده را خوانده و برمی گرداند
        /// </summary>
        /// <param name="cashAccountArticleId">شناسه عددی یکی از حساب‌های نقدی موجود</param>
        /// <returns>حساب نقدی مشخص شده با شناسه عددی</returns>
        Task<PayReceiveCashAccountViewModel> GetCashAccountArticleAsync(int cashAccountArticleId);

        /// <summary>
        /// به روش آسنکرون، اطلاعات خلاصه حساب نقدی با شناسه عددی مشخص شده را خوانده و برمی گرداند
        /// </summary>
        /// <param name="cashAccountArticleId">شناسه عددی یکی از حساب‌های نقدی موجود</param>
        /// <returns>حساب نقدی مشخص شده با شناسه عددی</returns>
        Task<PayReceiveCashAccountSummaryViewModel> GetCashAccountArticleSummaryAsync(int cashAccountArticleId);

        /// <summary>
        /// به روش آسنکرون، اطلاعات یک حساب نقدی را ایجاد یا اصلاح می کند
        /// </summary>
        /// <param name="cashAccountArticle">حساب نقدی مورد نظر برای ایجاد یا اصلاح</param>
        /// <param name="type">نوع فرم مورد نظر برای درخواست جاری - دریافت یا پرداخت</param> 
        /// <returns>اطلاعات نمایشی حساب نقدی ایجاد یا اصلاح شده</returns>
        Task<PayReceiveCashAccountViewModel> SaveCashAccountArticleAsync(
            PayReceiveCashAccountViewModel cashAccountArticle, int type);

        /// <summary>
        /// به روش آسنکرون، حساب نقدی مشخص شده با شناسه عددی را حذف می کند
        /// </summary>
        /// <param name="cashAccountArticleId">شناسه عددی حساب نقدی مورد نظر برای حذف</param>
        /// <param name="type">نوع فرم مورد نظر برای درخواست جاری - دریافت یا پرداخت</param> 
        Task DeleteCashAccountArticleAsync(int cashAccountArticleId, int type);

        /// <summary>
        /// به روش آسنکرون، حساب‌های نقدی مشخص شده با شناسه عددی را حذف می کند
        /// </summary>
        /// <param name="cashAccountArticleIds">مجموعه ای از شناسه های عددی حساب‌های نقدی مورد نظر برای حذف</param>
        /// <param name="type">نوع فرم مورد نظر برای درخواست جاری - دریافت یا پرداخت</param> 
        Task DeleteCashAccountArticlesAsync(IList<int> cashAccountArticleIds, int type);

        /// <summary>
        /// به روش آسنکرون، فرم دریافت/پرداخت مربوط به شناسه های آرتیکل حساب نقدی ورودی را برمی گرداند
        /// </summary>
        /// <param name="cashAccountArticleIds">لیست شناسه های آرتیکل حساب نقدی</param>
        /// <returns>فرم دریافت/پرداخت مشخص شده با شناسه عددی</returns>
        Task<PayReceiveViewModel> GetPayReceiveAsync(IList<int> cashAccountArticleIds);
    }
}
