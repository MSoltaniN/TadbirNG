using System;
using System.Threading.Tasks;
using SPPC.Tadbir.Domain;

namespace SPPC.Tadbir.Persistence.Utility
{
    /// <summary>
    /// امکانات مشترک مرتبط با محاسبات گزارشی را تعریف می کند
    /// </summary>
    public interface IReportUtility
    {
        /// <summary>
        /// به روش آسنکرون، تاریخ سند سیستمی با مأخذ داده شده را خوانده و برمی گرداند
        /// </summary>
        /// <param name="origin">یکی از مأخذهای تعریف شده برای سندهای سیستمی</param>
        /// <returns>تاریخ سند مورد نظر یا اگر سند مورد نظر پیدا نشود، بدون مقدار</returns>
        Task<DateTime?> GetSpecialVoucherDateAsync(VoucherOriginId origin);

        /// <summary>
        /// طول کد یکی از مولفه های حساب را در سطح داده شده خوانده و برمی گرداند
        /// </summary>
        /// <param name="viewId">شناسه دیتابیسی مولفه حساب مورد نظر</param>
        /// <param name="level">شماره سطح مورد نظر در ساختار درختی</param>
        /// <returns>طول کد مولفه حساب در سطح مورد نظر</returns>
        int GetLevelCodeLength(int viewId, int level);
    }
}
