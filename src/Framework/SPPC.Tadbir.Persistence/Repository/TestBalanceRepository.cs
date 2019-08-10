using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SPPC.Tadbir.Values;
using SPPC.Tadbir.ViewModel.Reporting;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای خواندن اطلاعات گزارش تراز آزمایشی را پیاده سازی می کند
    /// </summary>
    public class TestBalanceRepository : ITestBalanceRepository
    {
        /// <summary>
        /// به روش آسنکرون، اطلاعات گزارش تراز آزمایشی در سطح کل را خوانده و برمی گرداند
        /// </summary>
        /// <param name="parameters">پارامترهای مورد نیاز برای گزارش</param>
        /// <returns>اطلاعات گزارش با توجه به پارامترهای داده شده</returns>
        public Task<TestBalanceViewModel> GetLedgerBalanceAsync(TestBalanceParameters parameters)
        {
            return Task.FromResult(new TestBalanceViewModel());
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات گزارش تراز آزمایشی در سطح معین را خوانده و برمی گرداند
        /// </summary>
        /// <param name="parameters">پارامترهای مورد نیاز برای گزارش</param>
        /// <returns>اطلاعات گزارش با توجه به پارامترهای داده شده</returns>
        public Task<TestBalanceViewModel> GetSubsidiaryBalanceAsync(TestBalanceParameters parameters)
        {
            return Task.FromResult(new TestBalanceViewModel());
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات گزارش تراز آزمایشی در سطح تفصیلی را خوانده و برمی گرداند
        /// </summary>
        /// <param name="parameters">پارامترهای مورد نیاز برای گزارش</param>
        /// <returns>اطلاعات گزارش با توجه به پارامترهای داده شده</returns>
        public Task<TestBalanceViewModel> GetDetailBalanceAsync(TestBalanceParameters parameters)
        {
            return Task.FromResult(new TestBalanceViewModel());
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات گزارش تراز آزمایشی معین های یک حساب کل را خوانده و برمی گرداند
        /// </summary>
        /// <param name="accountId">شناسه دیتابیسی یکی از حساب های کل</param>
        /// <param name="parameters">پارامترهای مورد نیاز برای گزارش</param>
        /// <returns>اطلاعات گزارش با توجه به پارامترهای داده شده</returns>
        public Task<TestBalanceViewModel> GetLedgerItemsBalanceAsync(int accountId, TestBalanceParameters parameters)
        {
            return Task.FromResult(new TestBalanceViewModel());
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات گزارش تراز آزمایشی تفصیلی های یک حساب معین را خوانده و برمی گرداند
        /// </summary>
        /// <param name="accountId">شناسه دیتابیسی یکی از حساب های معین</param>
        /// <param name="parameters">پارامترهای مورد نیاز برای گزارش</param>
        /// <returns>اطلاعات گزارش با توجه به پارامترهای داده شده</returns>
        public Task<TestBalanceViewModel> GetSubsidiaryItemsBalanceAsync(int accountId, TestBalanceParameters parameters)
        {
            return Task.FromResult(new TestBalanceViewModel());
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات گزارش تراز آزمایشی برای یکی از سطوح تفصیلی شناور را خوانده و برمی گرداند
        /// </summary>
        /// <param name="level">شماره سطح مورد نظر از تفصیلی های شناور برای گزارش گیری</param>
        /// <param name="parameters">پارامترهای مورد نیاز برای گزارش</param>
        /// <returns>اطلاعات گزارش با توجه به پارامترهای داده شده</returns>
        public Task<TestBalanceViewModel> GetDetailAccountLevelBalanceAsync(int level, TestBalanceParameters parameters)
        {
            return Task.FromResult(new TestBalanceViewModel());
        }
    }
}
