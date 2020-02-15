using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SPPC.Tadbir.Values;

namespace SPPC.Tadbir.Persistence.Utility
{
    /// <summary>
    /// امکانات مشترک مرتبط با محاسبه مانده مولفه های حساب را تعریف می کند
    /// </summary>
    public interface ITestBalanceHelper
    {
        /// <summary>
        /// کد داخلی نمای لیستی گزارش تراز آزمایشی را با توجه به قالب و نوع مولفه حساب به دست می آورد
        /// </summary>
        /// <param name="format">قالب نمایشی داده شده برای گزارش</param>
        /// <param name="itemTypeName">نوع مولفه حساب</param>
        /// <returns>کد نمای لیستی به دست آمده</returns>
        int GetSourceList(TestBalanceFormat format, string itemTypeName);

        /// <summary>
        /// به روش آسنکرون، فهرست سطوح قابل استفاده برای گزارشگیری را
        /// برای مولفه حساب داده شده خوانده و برمی گرداند
        /// </summary>
        /// <param name="viewId">شناسه مولفه حساب</param>
        /// <returns>فهرست سطوح قابل استفاده</returns>
        Task<IEnumerable<TestBalanceModeInfo>> GetLevelBalanceTypesAsync(int viewId);

        /// <summary>
        /// به روش آسنکرون، فهرست سطوح زیرمجموعه قابل انتخاب برای گزارشگیری را خوانده و برمی گرداند
        /// </summary>
        /// <param name="viewId">شناسه مولفه حساب</param>
        /// <returns>فهرست سطوح زیرمجموعه قابل انتخاب</returns>
        Task<IEnumerable<TestBalanceModeInfo>> GetChildBalanceTypesAsync(int viewId);
    }
}
