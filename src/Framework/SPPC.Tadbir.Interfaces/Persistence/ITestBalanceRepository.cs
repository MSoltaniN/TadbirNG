using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Values;
using SPPC.Tadbir.ViewModel.Reporting;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای خواندن اطلاعات گزارش تراز آزمایشی را تعریف می کند
    /// </summary>
    public interface ITestBalanceRepository
    {
        /// <summary>
        /// به روش آسنکرون، اطلاعات گزارش تراز آزمایشی برای یکی از سطوح حساب را خوانده و برمی گرداند
        /// </summary>
        /// <param name="level">شماره یکی از سطوح حساب</param>
        /// <param name="parameters">پارامترهای مورد نیاز برای گزارش</param>
        /// <returns>اطلاعات گزارش با توجه به پارامترهای داده شده</returns>
        Task<TestBalanceViewModel> GetLevelBalanceAsync(int level, TestBalanceParameters parameters);

        /// <summary>
        /// به روش آسنکرون، اطلاعات گزارش تراز آزمایشی زیرمجموعه های یک حساب را خوانده و برمی گرداند
        /// </summary>
        /// <param name="accountId">شناسه دیتابیسی یکی از حساب های دارای زیرمجموعه</param>
        /// <param name="parameters">پارامترهای مورد نیاز برای گزارش</param>
        /// <returns>اطلاعات گزارش با توجه به پارامترهای داده شده</returns>
        Task<TestBalanceViewModel> GetChildrenBalanceAsync(int accountId, TestBalanceParameters parameters);

        /// <summary>
        /// به روش آسنکرون، انواع مختلف تراز آزمایشی را با توجه به ساختار درختی سرفصل های حسابداری خوانده و برمی گرداند
        /// </summary>
        /// <returns>انواع مختلف تراز آزمایشی</returns>
        Task<IEnumerable<TestBalanceModeInfo>> GetBalanceTypesLookupAsync(int viewId);

        /// <summary>
        /// کلاس پارامتر مورد نیاز برای گزارش های گردش و مانده حساب و سطوح شناور را
        /// با استفاده از مقادیر داده شده ساخته و برمی گرداند
        /// </summary>
        /// <param name="viewId">شناسه دیتابیسی مولفه حساب مورد نظر</param>
        /// <param name="from">تاریخ یا شماره سند ابتدا در محدوده گزارشگیری</param>
        /// <param name="to">تاریخ یا شماره سند انتها در محدوده گزارشگیری</param>
        /// <param name="mode">سطح مورد نظر برای گزارشگیری</param>
        /// <param name="format">قالب مورد نظر</param>
        /// <param name="gridOptions">گزینه های صفحه بندی، فیلتر و مرتب سازی برای نمای لیستی</param>
        /// <param name="byBranch">مشخص می کند که گزارش به تفکیک شعبه است یا نه</param>
        /// <param name="options">سایر گزینه های مورد نظر برای گزارشگیری</param>
        /// <returns>کلاس پارامتر ساخته شده</returns>
        TestBalanceParameters BuildParameters(int viewId, string from, string to,
            TestBalanceMode mode, TestBalanceFormat format,
            GridOptions gridOptions, bool? byBranch, int? options);
    }
}
