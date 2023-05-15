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
        /// به روش آسنکرون، دریافت و پرداخت با شناسه عددی مشخص شده را خوانده و برمی گرداند
        /// </summary>
        /// <param name="payReceiveId">شناسه عددی یکی از دریافت ها و پرداخت ها موجود</param>
        /// <returns>دریافت و پرداخت مشخص شده با شناسه عددی</returns>
        Task<PayReceiveViewModel> GetPayReceiveAsync(int payReceiveId);

        /// <summary>
        /// به روش آسنکرون، اطلاعات یک دریافت و پرداخت را ایجاد یا اصلاح می کند
        /// </summary>
        /// <param name="payReceive">دریافت و پرداخت مورد نظر برای ایجاد یا اصلاح</param>
        /// <returns>اطلاعات نمایشی دریافت و پرداخت ایجاد یا اصلاح شده</returns>
        Task<PayReceiveViewModel> SavePayReceiveAsync(PayReceiveViewModel payReceive);

        /// <summary>
        /// به روش آسنکرون، دریافت و پرداخت مشخص شده با شناسه عددی را حذف می کند
        /// </summary>
        /// <param name="payReceiveId">شناسه عددی دریافت و پرداخت مورد نظر برای حذف</param>
        /// <param name="type">نوع فرم دریافت/پرداخت</param>
        Task DeletePayReceiveAsync(int payReceiveId, int type);

        /// <summary>
        /// به روش آسنکرون، مشخص می کند که آیا شماره فرم دریافت/پرداخت مورد نظر تکراری است یا نه
        /// </summary>
        /// <param name="payReceive">اطلاعات نمایشی فرم دریافت/پرداخت مورد نظر</param>
        /// <returns>در صورت تکراری بودن شماره فرم دریافت/پرداخت مقدار درست و
        /// در غیر اینصورت نادرست برمی گرداند</returns>
        Task<bool> IsDuplicatePayReceiveNo(PayReceiveViewModel payReceive);

        /// <summary>
        /// به روش آسنکرون، وضعیت تایید فرم دریافت/پرداخت مشخص شده را تغییر می دهد
        /// </summary>
        /// <param name="payReceiveId">شناسه دیتابیسی فرم دریافت/پرداخت مورد نظر</param>
        /// <param name="isConfirmed"> در صورت تایید فرم دریافت/پرداخت با مقدار درست 
        /// و در غیر این صورت با مقدار نادرست پر می شود.</param>
        Task SetPayReceiveConfirmationAsync(int payReceiveId, bool isConfirmed);
    }
}
