using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SPPC.Tadbir.Values;
using SPPC.Tadbir.ViewModel.Reporting;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات دیتابیسی مورد نیاز برای تهیه گزارش سود و زیان را تعریف می کند
    /// </summary>
    public interface IProfitLossRepository
    {
        /// <summary>
        /// اطلاعات گزارش سود و زیان غیرمقایسه ای را محاسبه کرده و برمی گرداند
        /// </summary>
        /// <param name="parameters">پارامترهای مورد نیاز برای تهیه گزارش</param>
        /// <returns>اطلاعات گزارش سود و زیان غیرمقایسه ای</returns>
        Task<ProfitLossViewModel> GetProfitLossAsync(ProfitLossParameters parameters);
    }
}
