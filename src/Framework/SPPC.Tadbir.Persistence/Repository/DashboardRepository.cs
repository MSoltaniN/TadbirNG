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
using SPPC.Tadbir.Helpers;
using SPPC.Tadbir.Model.Finance;
using SPPC.Tadbir.ViewModel.Auth;
using SPPC.Tadbir.ViewModel.Finance;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای تهیه اطلاعات خلاصه در داشبورد را پیاده سازی می کند
    /// </summary>
    public class DashboardRepository : RepositoryBase, IDashboardRepository
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="unitOfWork">پیاده سازی اینترفیس واحد کاری برای انجام عملیات دیتابیسی </param>
        /// <param name="mapper">نگاشت مورد استفاده برای تبدیل کلاس های مدل اطلاعاتی</param>
        /// <param name="metadata">امکان خواندن متادیتا برای یک موجودیت را فراهم می کند</param>
        /// <param name="repository">عملیات مورد نیاز برای اعمال دسترسی امنیتی در سطح سطرهای اطلاعاتی و شعب را تعریف می کند</param>
        /// <param name="setRepository">امکان کار با مجموعه حساب ها را فراهم می کند</param>
        public DashboardRepository(
            IAppUnitOfWork unitOfWork, IDomainMapper mapper, IMetadataRepository metadata,
            ISecureRepository repository, IAccountSetRepository setRepository)
            : base(unitOfWork, mapper, metadata)
        {
            _repository = repository;
            _setRepository = setRepository;
        }

        /// <summary>
        /// به روش آسنکرون، مقادیر خلاصه محاسبه شده برای نمایش در داشبورد را خوانده و برمی گرداند
        /// </summary>
        /// <param name="calendar">تقویم مورد استفاده برای نمودارهای ماهیانه</param>
        /// <returns>اطلاعات مالی محاسبه شده</returns>
        public async Task<DashboardSummariesViewModel> GetSummariesAsync(Calendar calendar)
        {
            var repository = UnitOfWork.GetAsyncRepository<FiscalPeriod>();
            var currentPeriod = await repository.GetByIDAsync(_currentContext.FiscalPeriodId);
            var monthEnum = new MonthEnumerator(currentPeriod.StartDate, currentPeriod.EndDate, calendar);
            var months = monthEnum.GetMonths();
            return new DashboardSummariesViewModel()
            {
                LiquidRatio = await CalculateLiquidRatioAsync(),
                UnbalancedVoucherCount = await GetUnbalancedVoucherCountAsync(),
                BankBalance = await CalculateBankBalanceAsync(),
                CashierBalance = await CalculateCashierBalanceAsync(),
                NetSales = await GetMonthlyNetSalesAsync(months),
                GrossSales = await GetMonthlyGrossSalesAsync(months)
            };
        }

        /// <summary>
        /// اطلاعات محیطی کاربر جاری برنامه را برای فیلترهای سطری و شعب تنظیم می کند
        /// </summary>
        /// <param name="userContext">اطلاعات دسترسی کاربر به منابع محدود شده مانند نقش ها، دوره های مالی و شعبه ها</param>
        public override void SetCurrentContext(UserContextViewModel userContext)
        {
            base.SetCurrentContext(userContext);
            _repository.SetCurrentContext(userContext);
        }

        private static decimal CalculateBalance(IEnumerable<VoucherLineAmountsViewModel> amounts)
        {
            decimal debitSum = amounts.Sum(am => am.Debit);
            decimal creditSum = amounts.Sum(am => am.Credit);
            return debitSum - creditSum;
        }

        private async Task<decimal> CalculateBankBalanceAsync()
        {
            var amounts = await GetAccountSetAmountsAsync(AccountCollectionId.Bank);
            return CalculateBalance(amounts);
        }

        private async Task<decimal> CalculateCashierBalanceAsync()
        {
            var amounts = await GetAccountSetAmountsAsync(AccountCollectionId.Cashier);
            return CalculateBalance(amounts);
        }

        private async Task<decimal> CalculateLiquidRatioAsync()
        {
            var assetAmounts = await GetAccountSetAmountsAsync(AccountCollectionId.LiquidAssets);
            var liabilityAmounts = await GetAccountSetAmountsAsync(AccountCollectionId.LiquidLiabilities);
            decimal liquidAssets = CalculateBalance(assetAmounts);
            decimal liquidLiabilities = Math.Max(1.0M, Math.Abs(CalculateBalance(liabilityAmounts)));
            decimal liquidRatio = Math.Round(liquidAssets / liquidLiabilities, 2);
            return liquidRatio;
        }

        private async Task<int> GetUnbalancedVoucherCountAsync()
        {
            return await _repository
                .GetAllOperationQuery<Voucher>(ViewName.Voucher)
                .Where(v => !v.IsBalanced)
                .CountAsync();
        }

        private async Task<DashboardChartSeriesViewModel> GetMonthlyNetSalesAsync(
            IEnumerable<MonthInfo> months)
        {
            var series = new DashboardChartSeriesViewModel()
            {
                Title = "NetMonthlySalesChartTitle",
                Legend = "NetSalesChartLegend"
            };
            foreach (var month in months)
            {
                decimal netSales = await GetNetSalesAsync(month.Start, month.End);
                series.Points.Add(new DashboardChartPointViewModel()
                {
                    XValue = month.Name,
                    YValue = netSales
                });
            }

            return series;
        }

        private async Task<DashboardChartSeriesViewModel> GetMonthlyGrossSalesAsync(
            IEnumerable<MonthInfo> months)
        {
            var series = new DashboardChartSeriesViewModel()
            {
                Title = "GrossMonthlySalesChartTitle",
                Legend = "GrossSalesChartLegend"
            };
            foreach (var month in months)
            {
                decimal grossSales = await GetGrossSalesAsync(month.Start, month.End);
                series.Points.Add(new DashboardChartPointViewModel()
                {
                    XValue = month.Name,
                    YValue = grossSales
                });
            }

            return series;
        }

        private async Task<decimal> GetNetSalesAsync(DateTime fromDate, DateTime toDate)
        {
            decimal grossSales = await GetGrossSalesAsync(fromDate, toDate);
            var deficitAccounts = await _setRepository.GetSalesDeficitAccountsAsync();
            var amounts = await _repository
                .GetAllOperationQuery<VoucherLine>(ViewName.VoucherLine)
                .Where(line => deficitAccounts.Any(item => line.Account.FullCode.StartsWith(item.FullCode))
                    && line.Voucher.Date.IsBetween(fromDate, toDate))
                .Select(line => Mapper.Map<VoucherLineAmountsViewModel>(line))
                .ToListAsync();
            decimal deficit = amounts.Sum(am => am.Debit);
            return grossSales - deficit;
        }

        private async Task<decimal> GetGrossSalesAsync(DateTime fromDate, DateTime toDate)
        {
            var salesAccounts = await _setRepository.GetAccountSetItemsAsync(AccountCollectionId.Sales);
            var amounts = await _repository
                .GetAllOperationQuery<VoucherLine>(ViewName.VoucherLine)
                .Where(line => salesAccounts.Any(item => line.Account.FullCode.StartsWith(item.FullCode))
                    && line.Voucher.Date.IsBetween(fromDate, toDate))
                .Select(line => Mapper.Map<VoucherLineAmountsViewModel>(line))
                .ToListAsync();
            return amounts.Sum(am => am.Credit);
        }

        private async Task<IEnumerable<VoucherLineAmountsViewModel>> GetAccountSetAmountsAsync(
            AccountCollectionId collectionId)
        {
            var accounts = await _setRepository.GetAccountSetItemsAsync(collectionId);
            return await _repository
                .GetAllOperationQuery<VoucherLine>(ViewName.VoucherLine)
                .Where(line => accounts.Any(item => line.Account.FullCode.StartsWith(item.FullCode)))
                .Select(line => Mapper.Map<VoucherLineAmountsViewModel>(line))
                .ToListAsync();
        }

        private readonly ISecureRepository _repository;
        private readonly IAccountSetRepository _setRepository;
    }
}
