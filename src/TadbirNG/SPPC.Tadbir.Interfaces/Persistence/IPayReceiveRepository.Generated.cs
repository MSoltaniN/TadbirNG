using System.Collections.Generic;
using System.Threading.Tasks;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Utility;
using SPPC.Tadbir.ViewModel.CashFlow;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای مدیریت دریافت ها و پرداخت ها را تعریف می کند
    /// </summary>
    public interface IPayReceiveRepository
    {
        /// <summary>
        /// به روش آسنکرون، اطلاعات کلیه دریافت ها و پرداخت ها را خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه ای از دریافت ها و پرداخت ها تعریف شده</returns>
        Task<PagedList<PayReceiveViewModel>> GetPayReceivesAsync(GridOptions gridOptions = null);

        /// <summary>
        /// به روش آسنکرون، دریافت و پرداخت با شناسه عددی مشخص شده را خوانده و برمی گرداند
        /// </summary>
        /// <param name="payreceiveId">شناسه عددی یکی از دریافت ها و پرداخت ها موجود</param>
        /// <returns>دریافت و پرداخت مشخص شده با شناسه عددی</returns>
        Task<PayReceiveViewModel> GetPayReceiveAsync(int payreceiveId);

        /// <summary>
        /// به روش آسنکرون، اطلاعات یک دریافت و پرداخت را ایجاد یا اصلاح می کند
        /// </summary>
        /// <param name="payreceive">دریافت و پرداخت مورد نظر برای ایجاد یا اصلاح</param>
        /// <returns>اطلاعات نمایشی دریافت و پرداخت ایجاد یا اصلاح شده</returns>
        Task<PayReceiveViewModel> SavePayReceiveAsync(PayReceiveViewModel payreceive);

        /// <summary>
        /// به روش آسنکرون، دریافت و پرداخت مشخص شده با شناسه عددی را حذف می کند
        /// </summary>
        /// <param name="payreceiveId">شناسه عددی دریافت و پرداخت مورد نظر برای حذف</param>
        Task DeletePayReceiveAsync(int payreceiveId);

        /// <summary>
        /// به روش آسنکرون، دریافت ها و پرداخت ها مشخص شده با شناسه عددی را حذف می کند
        /// </summary>
        /// <param name="payReceiveIds">مجموعه ای از شناسه های عددی دریافت ها و پرداخت ها مورد نظر برای حذف</param>
        Task DeletePayReceivesAsync(IList<int> payReceiveIds);
    }
}
