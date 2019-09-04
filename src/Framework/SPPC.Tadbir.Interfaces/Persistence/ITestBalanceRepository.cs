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
        /// به روش آسنکرون، اطلاعات گزارش تراز آزمایشی زیرمجموعه های یک حساب را خوانده و برمی گرداند
        /// </summary>
        /// <param name="accountId">شناسه دیتابیسی یکی از حساب های دارای زیرمجموعه</param>
        /// <param name="parameters">پارامترهای مورد نیاز برای گزارش</param>
        /// <returns>اطلاعات گزارش با توجه به پارامترهای داده شده</returns>
        Task<TestBalanceViewModel> GetChildrenBalanceAsync(int accountId, TestBalanceParameters parameters);

        /// <summary>
        /// اطلاعات محیطی و امنیتی کاربر جاری برنامه را برای کنترل قواعد کاری برنامه تنظیم می کند
        /// </summary>
        /// <param name="userContext">اطلاعات محیطی و امنیتی کاربر جاری برنامه</param>
        void SetCurrentContext(UserContextViewModel userContext);
    }
}
