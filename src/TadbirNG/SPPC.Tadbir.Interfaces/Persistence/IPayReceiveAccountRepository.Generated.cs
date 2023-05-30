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
        /// <param name="payreceiveaccountId">شناسه عددی یکی از طرف‌های حساب موجود</param>
        /// <returns>طرف حساب مشخص شده با شناسه عددی</returns>
        Task<PayReceiveAccountViewModel> GetPayReceiveAccountAsync(int payreceiveaccountId);

        /// <summary>
        /// به روش آسنکرون، اطلاعات یک طرف حساب را ایجاد یا اصلاح می کند
        /// </summary>
        /// <param name="payreceiveaccount">طرف حساب مورد نظر برای ایجاد یا اصلاح</param>
        /// <returns>اطلاعات نمایشی طرف حساب ایجاد یا اصلاح شده</returns>
        Task<PayReceiveAccountViewModel> SavePayReceiveAccountAsync(PayReceiveAccountViewModel payreceiveaccount);

        /// <summary>
        /// به روش آسنکرون، طرف حساب مشخص شده با شناسه عددی را حذف می کند
        /// </summary>
        /// <param name="payreceiveaccountId">شناسه عددی طرف حساب مورد نظر برای حذف</param>
        Task DeletePayReceiveAccountAsync(int payreceiveaccountId);

        /// <summary>
        /// به روش آسنکرون، طرف‌های حساب مشخص شده با شناسه عددی را حذف می کند
        /// </summary>
        /// <param name="payReceiveAccountIds">مجموعه ای از شناسه های عددی طرف‌های حساب مورد نظر برای حذف</param>
        Task DeletePayReceiveAccountsAsync(IList<int> payReceiveAccountIds);
    }
}
