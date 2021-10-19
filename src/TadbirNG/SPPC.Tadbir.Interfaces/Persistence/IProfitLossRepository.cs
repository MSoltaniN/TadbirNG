using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.ViewModel.Finance;
using SPPC.Tadbir.ViewModel.Reporting;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات دیتابیسی مورد نیاز برای تهیه گزارش سود و زیان را تعریف می کند
    /// </summary>
    public interface IProfitLossRepository : IRepositoryBase
    {
        /// <summary>
        /// اطلاعات گزارش سود و زیان غیرمقایسه ای را محاسبه کرده و برمی گرداند
        /// </summary>
        /// <param name="parameters">پارامترهای مورد نیاز برای تهیه گزارش</param>
        /// <param name="balanceItems">مجموعه اطلاعات مانده ابتدا و انتهای دوره گزارشگیری
        /// برای حساب های موجودی کالا - سیستم ادواری</param>
        /// <returns>اطلاعات گزارش سود و زیان غیرمقایسه ای</returns>
        Task<ProfitLossViewModel> GetProfitLossAsync(
            ProfitLossParameters parameters, IEnumerable<StartEndBalanceViewModel> balanceItems);

        /// <summary>
        /// اطلاعات گزارش سود و زیان مقایسه ای را برای مراکز هزینه انتخابی محاسبه کرده و برمی گرداند
        /// </summary>
        /// <param name="parameters">پارامترهای مورد نیاز برای تهیه گزارش</param>
        /// <param name="balanceItems">مجموعه اطلاعات مانده ابتدا و انتهای دوره گزارشگیری
        /// برای حساب های موجودی کالا - سیستم ادواری</param>
        /// <returns>اطلاعات گزارش سود و زیان مقایسه ای برای چند مرکز هزینه</returns>
        Task<ProfitLossViewModel> GetProfitLossByCostCentersAsync(
            ProfitLossParameters parameters, IEnumerable<StartEndBalanceViewModel> balanceItems);

        /// <summary>
        /// اطلاعات گزارش سود و زیان مقایسه ای را برای پروژه های انتخابی محاسبه کرده و برمی گرداند
        /// </summary>
        /// <param name="parameters">پارامترهای مورد نیاز برای تهیه گزارش</param>
        /// <param name="balanceItems">مجموعه اطلاعات مانده ابتدا و انتهای دوره گزارشگیری
        /// برای حساب های موجودی کالا - سیستم ادواری</param>
        /// <returns>اطلاعات گزارش سود و زیان مقایسه ای برای چند پروژه</returns>
        Task<ProfitLossViewModel> GetProfitLossByProjectsAsync(
            ProfitLossParameters parameters, IEnumerable<StartEndBalanceViewModel> balanceItems);

        /// <summary>
        /// اطلاعات گزارش سود و زیان مقایسه ای را برای شعبه های انتخابی محاسبه کرده و برمی گرداند
        /// </summary>
        /// <param name="parameters">پارامترهای مورد نیاز برای تهیه گزارش</param>
        /// <param name="balanceItems">مجموعه اطلاعات مانده ابتدا و انتهای دوره گزارشگیری
        /// برای حساب های موجودی کالا - سیستم ادواری</param>
        /// <returns>اطلاعات گزارش سود و زیان مقایسه ای برای چند شعبه</returns>
        Task<ProfitLossViewModel> GetProfitLossByBranchesAsync(
            ProfitLossParameters parameters, IEnumerable<StartEndBalanceViewModel> balanceItems);

        /// <summary>
        /// اطلاعات گزارش سود و زیان مقایسه ای را برای دوره های مالی انتخابی محاسبه کرده و برمی گرداند
        /// </summary>
        /// <param name="parameters">پارامترهای مورد نیاز برای تهیه گزارش</param>
        /// <param name="balanceItems">مجموعه اطلاعات مانده ابتدا و انتهای دوره گزارشگیری
        /// برای حساب های موجودی کالا - سیستم ادواری</param>
        /// <returns>اطلاعات گزارش سود و زیان مقایسه ای برای چند دوره مالی</returns>
        Task<ProfitLossViewModel> GetProfitLossByFiscalPeriodsAsync(
            ProfitLossParameters parameters, IEnumerable<StartEndBalanceViewModel> balanceItems);
    }
}
