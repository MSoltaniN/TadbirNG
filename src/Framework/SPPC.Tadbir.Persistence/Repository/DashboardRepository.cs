using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPPC.Framework.Common;
using SPPC.Framework.Extensions;
using SPPC.Framework.Mapper;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Model.Finance;
using SPPC.Tadbir.ViewModel.Auth;
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
        /// <param name="repository">عملیات مورد نیاز برای اعمال دسترسی امنیتی در سطح سطرهای اطلاعاتی و شعب را تعریف می کند</param>
        /// <param name="setRepository">امکان کار با مجموعه حساب ها را فراهم می کند</param>
        public DashboardRepository(
            IAppUnitOfWork unitOfWork, IDomainMapper mapper, ISecureRepository repository,
            IAccountSetRepository setRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _repository = repository;
            _setRepository = setRepository;
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
                UnbalancedVoucherCount = await GetUnbalancedVoucherCountAsync(),
                NetSales = await GetNetSalesSeriesAsync(),
                GrossSales = await GetGrossSalesSeriesAsync()
            };
        }

        /// <summary>
        /// اطلاعات محیطی کاربر جاری برنامه را برای فیلترهای سطری و شعب تنظیم می کند
        /// </summary>
        /// <param name="userContext">اطلاعات دسترسی کاربر به منابع محدود شده مانند نقش ها، دوره های مالی و شعبه ها</param>
        public void SetCurrentContext(UserContextViewModel userContext)
        {
            _repository.SetCurrentContext(userContext);
        }

        private async Task<decimal> CalculateBankBalanceAsync()
        {
            var bankAccount = await _setRepository.GetBankAccountAsync();
            var amounts = await _repository
                .GetAllOperationQuery<VoucherLine>(ViewName.VoucherLine, line => line.Account)
                .Where(line => line.Account.FullCode.StartsWith(bankAccount.FullCode))
                .Select(line => _mapper.Map<VoucherLineAmountsViewModel>(line))
                .ToListAsync();
            return CalculateBalance(amounts);
        }

        private async Task<decimal> CalculateCashierBalanceAsync()
        {
            var cashierAccount = await _setRepository.GetCashierAccountAsync();
            var amounts = await _repository
                .GetAllOperationQuery<VoucherLine>(ViewName.VoucherLine, line => line.Account)
                .Where(line => line.Account.FullCode.StartsWith(cashierAccount.FullCode))
                .Select(line => _mapper.Map<VoucherLineAmountsViewModel>(line))
                .ToListAsync();
            return CalculateBalance(amounts);
        }

        private async Task<decimal> CalculateLiquidRatioAsync()
        {
            var assets = await _setRepository.GetLiquidAssetAccountsAsync();
            var liabilities = await _setRepository.GetLiquidLiabilityAccountsAsync();
            var assetAmounts = await _repository
                .GetAllOperationQuery<VoucherLine>(ViewName.VoucherLine, line => line.Account)
                .Where(line => assets.Any(item => line.Account.FullCode.StartsWith(item.FullCode)))
                .Select(line => _mapper.Map<VoucherLineAmountsViewModel>(line))
                .ToListAsync();
            var liabilityAmounts = await _repository
                .GetAllOperationQuery<VoucherLine>(ViewName.VoucherLine, line => line.Account)
                .Where(line => liabilities.Any(item => line.Account.FullCode.StartsWith(item.FullCode)))
                .Select(line => _mapper.Map<VoucherLineAmountsViewModel>(line))
                .ToListAsync();
            decimal liquidRatio = CalculateBalance(assetAmounts) / 1.0M; // CalculateBalance(liabilityAmounts);
            return liquidRatio;
        }

        private async Task<int> GetUnbalancedVoucherCountAsync()
        {
            int unbalancedCount = await _repository
                .GetAllOperationQuery<Voucher>(ViewName.Voucher, v => v.Lines)
                .Select(v => _mapper.Map<VoucherViewModel>(v))
                .Where(v => Math.Abs(v.DebitSum - v.CreditSum) >= 1.0M)
                .CountAsync();
            return unbalancedCount;
        }

        private async Task<DashboardChartSeriesViewModel> GetNetSalesSeriesAsync()
        {
            var calendar = new PersianCalendar();
            var series = new DashboardChartSeriesViewModel()
            {
                Title = "NetSalesChartTitle",
                Legend = "NetSalesChartLegend"
            };
            foreach (int month in Enumerable.Range(1, 12))
            {
                DateTime startOfMonth = calendar.GetStartOfMonth(month);
                DateTime endOfMonth = calendar.GetEndOfMonth(month);
                decimal netSales = await GetNetSalesAsync(startOfMonth, endOfMonth);
                series.Points.Add(new DashboardChartPointViewModel()
                {
                    XValue = JalaliDateTime.FromDateTime(startOfMonth).MonthName,
                    YValue = netSales
                });
            }

            return series;
        }

        private async Task<DashboardChartSeriesViewModel> GetGrossSalesSeriesAsync()
        {
            var calendar = new PersianCalendar();
            var series = new DashboardChartSeriesViewModel()
            {
                Title = "GrossSalesChartTitle",
                Legend = "GrossSalesChartLegend"
            };
            foreach (int month in Enumerable.Range(1, 12))
            {
                DateTime startOfMonth = calendar.GetStartOfMonth(month);
                DateTime endOfMonth = calendar.GetEndOfMonth(month);
                decimal grossSales = await GetGrossSalesAsync(startOfMonth, endOfMonth);
                series.Points.Add(new DashboardChartPointViewModel()
                {
                    XValue = JalaliDateTime.FromDateTime(startOfMonth).MonthName,
                    YValue = grossSales
                });
            }

            return series;
        }

        private async Task<decimal> GetNetSalesAsync(DateTime fromDate, DateTime toDate)
        {
            decimal grossSales = await GetGrossSalesAsync(fromDate, toDate);
            var deficitAccount = await _setRepository.GetSalesDeficitAccountAsync();
            var amounts = await _repository
                .GetAllOperationQuery<VoucherLine>(ViewName.VoucherLine, line => line.Account)
                .Where(line => line.Account.FullCode.StartsWith(deficitAccount.FullCode))
                .Select(line => _mapper.Map<VoucherLineAmountsViewModel>(line))
                .ToListAsync();
            decimal deficit = amounts
                .Select(am => am.Debit)
                .Sum();
            return grossSales - deficit;
        }

        private async Task<decimal> GetGrossSalesAsync(DateTime fromDate, DateTime toDate)
        {
            var salesAccount = await _setRepository.GetSalesAccountAsync();
            var amounts = await _repository
                .GetAllOperationQuery<VoucherLine>(ViewName.VoucherLine, line => line.Account)
                .Where(line => line.Account.FullCode.StartsWith(salesAccount.FullCode))
                .Select(line => _mapper.Map<VoucherLineAmountsViewModel>(line))
                .ToListAsync();
            return amounts
                .Select(am => am.Credit)
                .Sum();
        }

        private decimal CalculateBalance(IEnumerable<VoucherLineAmountsViewModel> amounts)
        {
            decimal debitSum = amounts
                .Select(am => am.Debit)
                .Sum();
            decimal creditSum = amounts
                .Select(am => am.Credit)
                .Sum();
            return debitSum - creditSum;
        }

        private readonly IAppUnitOfWork _unitOfWork;
        private readonly IDomainMapper _mapper;
        private readonly ISecureRepository _repository;
        private readonly IAccountSetRepository _setRepository;
    }
}
