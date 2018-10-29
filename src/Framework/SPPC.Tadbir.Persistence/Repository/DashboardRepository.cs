using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SPPC.Framework.Mapper;
using SPPC.Tadbir.ViewModel.Finance;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای تهیه اطلاعات خلاصه در داشبورد را پیاده سازی می کند
    /// </summary>
    public class DashboardRepository : IDashboardRepository
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="unitOfWork">پیاده سازی اینترفیس واحد کاری برای انجام عملیات دیتابیسی </param>
        /// <param name="mapper">نگاشت مورد استفاده برای تبدیل کلاس های مدل اطلاعاتی</param>
        public DashboardRepository(IAppUnitOfWork unitOfWork, IDomainMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <summary>
        /// مقادیر خلاصه محاسبه شده برای نمایش در داشبورد را خوانده و برمی گرداند
        /// </summary>
        /// <returns>اطلاعات مالی محاسبه شده</returns>
        public async Task<DashboardSummariesViewModel> GetSummariesAsync()
        {
            return new DashboardSummariesViewModel()
            {
                BankBalance = await CalculateBankBalanceAsync(),
                CashierBalance = await CalculateCashierBalanceAsync(),
                LiquidRatio = await CalculateLiquidRatioAsync(),
                UnbalancedVoucherCount = await GetUnbalancedVoucherCountAsync()
            };
        }

        private async Task<decimal> CalculateBankBalanceAsync()
        {
            return 0.0M;
        }

        private async Task<decimal> CalculateCashierBalanceAsync()
        {
            return 0.0M;
        }

        private async Task<decimal> CalculateLiquidRatioAsync()
        {
            return 0.0M;
        }

        private async Task<int> GetUnbalancedVoucherCountAsync()
        {
            return 0;
        }

        private async Task<decimal> GetNetSales(DateTime fromDate, DateTime toDate)
        {
            return 0.0M;
        }

        private async Task<decimal> GetGrossSales(DateTime fromDate, DateTime toDate)
        {
            return 0.0M;
        }

        private readonly IAppUnitOfWork _unitOfWork;
        private readonly IDomainMapper _mapper;
    }
}
