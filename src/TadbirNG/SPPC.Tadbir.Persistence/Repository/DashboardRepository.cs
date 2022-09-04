using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPPC.Framework.Extensions;
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
    public class DashboardRepository : RepositoryBase, IDashboardRepository
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="context">امکانات مشترک مورد نیاز را برای عملیات دیتابیسی فراهم می کند</param>
        /// <param name="report"></param>
        public DashboardRepository(IRepositoryContext context, IReportDirectUtility report)
            : base(context)
        {
            _report = report;
        }

        private class AccountByWidget
        {
            public int WidgetId { get; set; }

            public FullAccountViewModel FullAccount { get; set; }
        }

        private class ParameterByWidget
        {
            public int WidgetId { get; set; }

            public WidgetParameterViewModel Parameter { get; set; }
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
                LiquidRatio = CalculateLiquidRatio(),
                UnbalancedVoucherCount = await GetUnbalancedVoucherCountAsync(),
                BankBalance = GetCollectionBalance(AccountCollectionId.Bank),
                CashierBalance = GetCollectionBalance(AccountCollectionId.Cashier),
                NetSales = GetMonthlyNetSales(months),
                GrossSales = GetMonthlyGrossSales(months)
            };
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات کامل داشبورد ایجاد شده توسط کاربر جاری برنامه را خوانده و برمی گرداند
        /// </summary>
        /// <returns>اطلاعات کامل برای داشبورد کاربر جاری برنامه یا رفرنس بدون مقدار
        /// در صورتی که داشبوردی برای کاربر جاری وجود نداشته باشد</returns>
        public DashboardViewModel GetCurrentUserDashboard()
        {
            var query = String.Format(DashboardQuery.CurrentDashboardWidgets, UserContext.Id);
            DbConsole.ConnectionString = UnitOfWork.CompanyConnection;
            var widgets = DbConsole.ExecuteQuery(query);
            return GetDashboard(widgets);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات توابع محاسباتی قابل استفاده در ویجت ها را خوانده و برمی گرداند
        /// </summary>
        /// <returns>اطلاعات توابع محاسباتی</returns>
        public async Task<List<WidgetFunctionViewModel>> GetWidgetFunctionsLookupAsync()
        {
            var repository = UnitOfWork.GetAsyncRepository<WidgetFunction>();
            return await repository
                .GetEntityQuery()
                .Select(func => Mapper.Map<WidgetFunctionViewModel>(func))
                .ToListAsync();
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات انواع نمودارهای قابل استفاده در ویجت ها را خوانده و برمی گرداند
        /// </summary>
        /// <returns>اطلاعات انواع نمودارها</returns>
        public async Task<List<WidgetTypeViewModel>> GetWidgetTypesLookupAsync()
        {
            var repository = UnitOfWork.GetAsyncRepository<WidgetType>();
            return await repository
                .GetEntityQuery()
                .Select(type => Mapper.Map<WidgetTypeViewModel>(type))
                .ToListAsync();
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات ویجت های قابل دسترسی توسط کاربر جاری را خوانده و برمی گرداند
        /// </summary>
        /// <returns>اطلاعات ویجت های قابل دسترسی</returns>
        public async Task<List<WidgetViewModel>> GetWidgetsLookupAsync()
        {
            var repository = UnitOfWork.GetAsyncRepository<Widget>();
            return await repository
                .GetEntityQuery(widget => widget.Function, widget => widget.Type)
                .Select(type => Mapper.Map<WidgetViewModel>(type))
                .ToListAsync();
        }

        private DashboardViewModel GetDashboard(DataTable widgets)
        {
            var dashboard = DashboardFromWidgets(widgets);
            if (dashboard != null)
            {
                var tabWidgets = widgets.Rows
                    .Cast<DataRow>()
                    .Select(row => new TabWidgetViewModel()
                    {
                        TabId = _report.ValueOrDefault<int>(row, "TabID"),
                        WidgetId = _report.ValueOrDefault<int>(row, "WidgetID"),
                        Settings = _report.ValueOrDefault(row, "Settings"),
                        DefaultSettings = _report.ValueOrDefault(row, "DefaultSettings"),
                        WidgetTitle = _report.ValueOrDefault(row, "Title"),
                        WidgetDescription = _report.ValueOrDefault(row, "Description")
                    })
                    .ToList();

                LoadWidgetDetails(tabWidgets);
                var widgetIds = tabWidgets
                    .Select(tw => tw.WidgetId)
                    .Distinct();
                var widgetAccounts = GetWidgetAccounts(widgetIds);
                var widgetParameters = GetWidgetParameters(widgetIds);
                foreach (var tab in dashboard.Tabs)
                {
                    tab.Widgets.AddRange(tabWidgets.Where(tw => tw.TabId == tab.Id));
                    foreach (var widget in tab.Widgets)
                    {
                        widget.WidgetAccounts.AddRange(widgetAccounts
                            .Where(wacc => wacc.WidgetId == widget.WidgetId)
                            .Select(wacc => wacc.FullAccount));
                        widget.WidgetParmeters.AddRange(widgetParameters
                            .Where(wpara => wpara.WidgetId == widget.WidgetId)
                            .Select(wpara => wpara.Parameter));
                    }
                }
            }

            return dashboard;
        }

        private DashboardViewModel DashboardFromWidgets(DataTable widgets)
        {
            var dashboard = default(DashboardViewModel);
            if (widgets.Rows.Count > 0)
            {
                int dashboardId = _report.ValueOrDefault<int>(widgets.Rows[0], "DashboardID");
                dashboard = new DashboardViewModel()
                {
                    Id = dashboardId,
                    UserId = UserContext.Id
                };
                var tabs = widgets.Rows
                    .Cast<DataRow>()
                    .Select(row => new DashboardTabViewModel()
                    {
                        Id = _report.ValueOrDefault<int>(row, "TabID"),
                        Index = _report.ValueOrDefault<int>(row, "Index"),
                        Title = _report.ValueOrDefault(row, "TabTitle")
                    });
                foreach (var group in tabs.GroupBy(tab => tab.Id))
                {
                    dashboard.Tabs.Add(group.First());
                }
            }

            return dashboard;
        }

        private void LoadWidgetDetails(List<TabWidgetViewModel> tabWidgets)
        {
            var widgetIds = tabWidgets
                .Select(tw => tw.WidgetId)
                .Distinct();
            if (widgetIds.Any())
            {
                var query = String.Format(DashboardQuery.WidgetDetails, String.Join(',', widgetIds));
                var details = DbConsole.ExecuteQuery(query);
                var allWidgets = details.Rows
                    .Cast<DataRow>()
                    .Select(row => new TabWidgetViewModel()
                    {
                        WidgetId = _report.ValueOrDefault<int>(row, "WidgetID"),
                        WidgetFunctionId = _report.ValueOrDefault<int>(row, "FunctionID"),
                        WidgetTypeId = _report.ValueOrDefault<int>(row, "TypeID"),
                        WidgetFunctionName = _report.ValueOrDefault(row, "FunctionName"),
                        WidgetTypeName = _report.ValueOrDefault(row, "TypeName")
                    });
                foreach (var group in allWidgets.GroupBy(w => w.WidgetId))
                {
                    var widget = group.First();
                    Array.ForEach(tabWidgets.Where(item => item.WidgetId == group.Key).ToArray(), tw =>
                    {
                        tw.WidgetFunctionId = widget.WidgetFunctionId;
                        tw.WidgetFunctionName = widget.WidgetFunctionName;
                        tw.WidgetTypeId = widget.WidgetTypeId;
                        tw.WidgetTypeName = widget.WidgetTypeName;
                    });
                }
            }
        }

        private IEnumerable<AccountByWidget> GetWidgetAccounts(IEnumerable<int> widgetIds)
        {
            var widgetAccounts = new List<AccountByWidget>();
            if (widgetIds.Any())
            {
                var query = String.Format(DashboardQuery.WidgetsAccounts, String.Join(',', widgetIds));
                var accounts = DbConsole.ExecuteQuery(query);
                widgetAccounts.AddRange(accounts.Rows
                    .Cast<DataRow>()
                    .Select(row => new AccountByWidget
                    {
                        WidgetId = _report.ValueOrDefault<int>(row, "WidgetID"),
                        FullAccount = new FullAccountViewModel()
                        {
                            Account = new AccountItemBriefViewModel()
                            {
                                Id = _report.ValueOrDefault<int>(row, "AccountID"),
                                Name = _report.ValueOrDefault(row, "AccountName"),
                                FullCode = _report.ValueOrDefault(row, "AccountFullCode")
                            },
                            DetailAccount = new AccountItemBriefViewModel()
                            {
                                Id = _report.ValueOrDefault<int>(row, "DetailAccountID"),
                                Name = _report.ValueOrDefault(row, "DetailAccountName"),
                                FullCode = _report.ValueOrDefault(row, "DetailAccountFullCode")
                            },
                            CostCenter = new AccountItemBriefViewModel()
                            {
                                Id = _report.ValueOrDefault<int>(row, "CostCenterID"),
                                Name = _report.ValueOrDefault(row, "CostCenterName"),
                                FullCode = _report.ValueOrDefault(row, "CostCenterFullCode")
                            },
                            Project = new AccountItemBriefViewModel()
                            {
                                Id = _report.ValueOrDefault<int>(row, "ProjectID"),
                                Name = _report.ValueOrDefault(row, "ProjectName"),
                                FullCode = _report.ValueOrDefault(row, "ProjectFullCode")
                            }
                        }
                    }));
            }

            return widgetAccounts;
        }

        private IEnumerable<ParameterByWidget> GetWidgetParameters(IEnumerable<int> widgetIds)
        {
            var widgetParameters = new List<ParameterByWidget>();
            if (widgetIds.Any())
            {

                var query = String.Format(DashboardQuery.WidgetsParameters, String.Join(',', widgetIds));
                var parameters = DbConsole.ExecuteQuery(query);
                widgetParameters.AddRange(parameters.Rows
                    .Cast<DataRow>()
                    .Select(row => new ParameterByWidget
                    {
                        WidgetId = _report.ValueOrDefault<int>(row, "WidgetID"),
                        Parameter = new WidgetParameterViewModel()
                        {
                            Name = _report.ValueOrDefault(row, "Name"),
                            Alias = _report.ValueOrDefault(row, "Alias"),
                            Type = _report.ValueOrDefault(row, "Type"),
                            DefaultValue = _report.ValueOrDefault(row, "DefaultValue"),
                            Description = _report.ValueOrDefault(row, "Description")
                        }
                    }));
            }

            return widgetParameters;
        }

        private decimal CalculateLiquidRatio()
        {
            decimal liquidAssets = GetCollectionBalance(AccountCollectionId.LiquidAssets);
            decimal liquidLiabilities = Math.Max(1.0M, Math.Abs(
                GetCollectionBalance(AccountCollectionId.LiquidLiabilities)));
            decimal liquidRatio = Math.Round(liquidAssets / liquidLiabilities, 2);
            return liquidRatio;
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

        private decimal GetCollectionBalance(AccountCollectionId collectionId)
        {
            var accounts = _report.GetUsableAccounts(collectionId);
            return GetCollectionBalance(accounts);
        }

        private decimal GetCollectionBalance(IEnumerable<AccountItemBriefViewModel> accounts,
            DateTime? from = null, DateTime? to = null)
        {
            decimal balance = 0.0M;
            var reportQuery = default(ReportQuery);
            if (accounts.Any())
            {
                DbConsole.ConnectionString = UnitOfWork.CompanyConnection;
                var filterBuilder = new StringBuilder(_report.GetEnvironmentFilters());
                filterBuilder.AppendFormat(" AND vl.AccountID IN({0})",
                    String.Join(",", accounts.Select(acc => acc.Id)));
                filterBuilder.Replace("BranchID", "vl.BranchID");
                if (from == null || to == null)
                {
                    reportQuery = new ReportQuery(String.Format(
                        DashboardQuery.CollectionBalance, filterBuilder.ToString()));
                }
                else
                {
                    var fromDate = from.Value.ToShortDateString(false);
                    var toDate = to.Value.ToShortDateString(false);
                    reportQuery = new ReportQuery(String.Format(
                        DashboardQuery.CollectionBalanceByDate, fromDate, toDate, filterBuilder.ToString()));
                }

                var result = DbConsole.ExecuteQuery(reportQuery.Query);
                if (result.Rows.Count > 0)
                {
                    balance = _report.ValueOrDefault<decimal>(result.Rows[0], "Balance");
                }
            }

            return balance;
        }

        private readonly IReportDirectUtility _report;
    }
}
