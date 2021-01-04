using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.ViewModel.Reporting;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای مدیریت و محاسبه گزارش ترازنامه را پیاده سازی می کند
    /// </summary>
    public class BalanceSheetRepository : RepositoryBase, IBalanceSheetRepository
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="context">امکانات مشترک مورد نیاز برای عملیات دیتابیسی را فراهم می کند</param>
        public BalanceSheetRepository(IRepositoryContext context)
            : base(context)
        {
        }

        /// <summary>
        /// اطلاعات گزارش ترازنامه را با توجه به پارامترهای داده شده محاسبه کرده و برمی گرداند
        /// </summary>
        /// <param name="parameters">پارامترهای مورد نظر برای محاسبه گزارش</param>
        /// <returns>اطلاعات محاسبه شده برای گزارش ترازنامه</returns>
        public async Task<BalanceSheetViewModel> GetBalanceSheetAsync(BalanceSheetParameters parameters)
        {
            return new BalanceSheetViewModel();
        }
    }
}
