using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
        /// به روش آسنکرون، اطلاعات گزارش تراز آزمایشی در سطح کل را خوانده و برمی گرداند
        /// </summary>
        /// <param name="parameters">پارامترهای مورد نیاز برای گزارش</param>
        /// <returns>اطلاعات گزارش با توجه به پارامترهای داده شده</returns>
        Task<TestBalanceViewModel> GetLedgerBalanceAsync(TestBalanceParameters parameters);

        /// <summary>
        /// به روش آسنکرون، اطلاعات گزارش تراز آزمایشی در سطح معین را خوانده و برمی گرداند
        /// </summary>
        /// <param name="parameters">پارامترهای مورد نیاز برای گزارش</param>
        /// <returns>اطلاعات گزارش با توجه به پارامترهای داده شده</returns>
        Task<TestBalanceViewModel> GetSubsidiaryBalanceAsync(TestBalanceParameters parameters);

        /// <summary>
        /// به روش آسنکرون، اطلاعات گزارش تراز آزمایشی در سطح تفصیلی را خوانده و برمی گرداند
        /// </summary>
        /// <param name="parameters">پارامترهای مورد نیاز برای گزارش</param>
        /// <returns>اطلاعات گزارش با توجه به پارامترهای داده شده</returns>
        Task<TestBalanceViewModel> GetDetailBalanceAsync(TestBalanceParameters parameters);

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
        Task<IEnumerable<TestBalanceModeInfo>> GetBalanceTypesLookupAsync();
    }
}
