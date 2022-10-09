﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Model.Finance;
using SPPC.Tadbir.Model.Reporting;
using SPPC.Tadbir.Persistence.Utility;
using SPPC.Tadbir.Utility;
using SPPC.Tadbir.ViewModel.Finance;
using SPPC.Tadbir.ViewModel.Reporting;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای تهیه اطلاعات خلاصه در داشبورد را پیاده سازی می کند
    /// </summary>
    public partial class DashboardRepository : EntityLoggingRepository<Widget, WidgetViewModel>, IDashboardRepository
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="context">امکانات مشترک مورد نیاز را برای عملیات دیتابیسی فراهم می کند</param>
        /// <param name="system"></param>
        /// <param name="report"></param>
        public DashboardRepository(IRepositoryContext context, ISystemRepository system,
            IReportDirectUtility report)
            : base(context, system?.Logger)
        {
            _report = report;
            Config = system.Config;
        }

        /// <summary>
        /// به روش آسنکرون، مقادیر خلاصه محاسبه شده برای نمایش در داشبورد را خوانده و برمی گرداند
        /// </summary>
        /// <param name="calendar">تقویم مورد استفاده برای نمودارهای ماهیانه</param>
        /// <returns>اطلاعات مالی محاسبه شده</returns>
        public async Task<DashboardSummariesViewModel> GetSummariesAsync(Calendar calendar)
        {
            var repository = UnitOfWork.GetAsyncRepository<FiscalPeriod>();
            var currentPeriod = await repository.GetByIDAsync(UserContext.FiscalPeriodId);
            if (currentPeriod == null)
            {
                return new DashboardSummariesViewModel();
            }

            var monthEnum = new MonthEnumerator(currentPeriod.StartDate, currentPeriod.EndDate, calendar);
            var months = monthEnum.GetMonths();
            return new DashboardSummariesViewModel()
            {
                //LiquidRatio = CalculateLiquidRatio(currentPeriod.EndDate),
                UnbalancedVoucherCount = await GetUnbalancedVoucherCountAsync(),
                //BankBalance = GetCollectionBalance(AccountCollectionId.Bank, currentPeriod.EndDate),
                //CashierBalance = GetCollectionBalance(AccountCollectionId.Cashier, currentPeriod.EndDate),
                NetSales = GetMonthlyNetSales(months),
                GrossSales = GetMonthlyGrossSales(months)
            };
        }

        private async Task<int> GetUnbalancedVoucherCountAsync()
        {
            var repository = UnitOfWork.GetAsyncRepository<Voucher>();
            return await repository.GetCountByCriteriaAsync(
                v => v.FiscalPeriodId == UserContext.FiscalPeriodId && !v.IsBalanced);
        }

        private DashboardChartSeriesViewModel GetMonthlyNetSales(
            IEnumerable<MonthInfo> months)
        {
            var series = new DashboardChartSeriesViewModel()
            {
                Title = "NetMonthlySalesChartTitle",
                Legend = "NetSalesChartLegend"
            };
            foreach (var month in months)
            {
                decimal netSales = GetNetSales(month.Start, month.End);
                series.Points.Add(new DashboardChartPointViewModel()
                {
                    XValue = month.Name,
                    YValue = netSales
                });
            }

            return series;
        }

        private DashboardChartSeriesViewModel GetMonthlyGrossSales(
            IEnumerable<MonthInfo> months)
        {
            var accounts = _report.GetUsableAccounts(AccountCollectionId.Sales);
            var series = new DashboardChartSeriesViewModel()
            {
                Title = "GrossMonthlySalesChartTitle",
                Legend = "GrossSalesChartLegend"
            };
            foreach (var month in months)
            {
                decimal grossSales = GetCollectionBalance(accounts, month.Start, month.End);
                series.Points.Add(new DashboardChartPointViewModel()
                {
                    XValue = month.Name,
                    YValue = Math.Abs(Math.Min(0, grossSales))
                });
            }

            return series;
        }

        private decimal GetNetSales(DateTime fromDate, DateTime toDate)
        {
            var salesAccounts = _report.GetUsableAccounts(AccountCollectionId.Sales);
            decimal grossSales = GetCollectionBalance(salesAccounts, fromDate, toDate);
            grossSales = Math.Abs(Math.Min(0, grossSales));
            var reducerAccounts = GetSalesReducerAccounts();
            decimal refundDiscount = GetCollectionBalance(reducerAccounts, fromDate, toDate);
            return grossSales - refundDiscount;
        }

        private IEnumerable<AccountItemBriefViewModel> GetSalesReducerAccounts()
        {
            var deficitAccounts = new List<AccountItemBriefViewModel>();
            deficitAccounts.AddRange(_report.GetUsableAccounts(AccountCollectionId.SalesRefund));
            deficitAccounts.AddRange(_report.GetUsableAccounts(AccountCollectionId.SalesDiscount));
            return deficitAccounts;
        }
    }
}
