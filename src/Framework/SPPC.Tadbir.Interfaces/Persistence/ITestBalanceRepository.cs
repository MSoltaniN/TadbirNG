using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SPPC.Tadbir.Values;
using SPPC.Tadbir.ViewModel.Auth;
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
        /// به روش آسنکرون، اطلاعات گزارش تراز آزمایشی معین های یک حساب کل را خوانده و برمی گرداند
        /// </summary>
        /// <param name="accountId">شناسه دیتابیسی یکی از حساب های کل</param>
        /// <param name="parameters">پارامترهای مورد نیاز برای گزارش</param>
        /// <returns>اطلاعات گزارش با توجه به پارامترهای داده شده</returns>
        Task<TestBalanceViewModel> GetLedgerItemsBalanceAsync(int accountId, TestBalanceParameters parameters);

        /// <summary>
        /// به روش آسنکرون، اطلاعات گزارش تراز آزمایشی تفصیلی های یک حساب معین را خوانده و برمی گرداند
        /// </summary>
        /// <param name="accountId">شناسه دیتابیسی یکی از حساب های معین</param>
        /// <param name="parameters">پارامترهای مورد نیاز برای گزارش</param>
        /// <returns>اطلاعات گزارش با توجه به پارامترهای داده شده</returns>
        Task<TestBalanceViewModel> GetSubsidiaryItemsBalanceAsync(int accountId, TestBalanceParameters parameters);

        /// <summary>
        /// اطلاعات محیطی و امنیتی کاربر جاری برنامه را برای کنترل قواعد کاری برنامه تنظیم می کند
        /// </summary>
        /// <param name="userContext">اطلاعات محیطی و امنیتی کاربر جاری برنامه</param>
        void SetCurrentContext(UserContextViewModel userContext);
    }
}
