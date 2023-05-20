using System.Threading.Tasks;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.ViewModel.Reporting;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای مدیریت و محاسبه گزارش ترازنامه را تعریف می کند
    /// </summary>
    public interface IBalanceSheetRepository
    {
        /// <summary>
        /// اطلاعات گزارش ترازنامه را با توجه به پارامترهای داده شده محاسبه کرده و برمی گرداند
        /// </summary>
        /// <param name="parameters">پارامترهای مورد نظر برای محاسبه گزارش</param>
        /// <returns>اطلاعات محاسبه شده برای گزارش ترازنامه</returns>
        Task<BalanceSheetViewModel> GetBalanceSheetAsync(BalanceSheetParameters parameters);
    }
}
