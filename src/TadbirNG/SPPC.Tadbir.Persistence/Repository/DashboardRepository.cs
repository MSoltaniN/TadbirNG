﻿using System;
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
            var result = DbConsole.ExecuteQuery(query);
            return GetDashboard(result);
        }

        private DashboardViewModel GetDashboard(DataTable result)
        {
            var fullDashboard = default(DashboardViewModel);
            if (result.Rows.Count > 0)
            {
                int dashboardId = _report.ValueOrDefault<int>(result.Rows[0], "DashboardID");
                fullDashboard = new DashboardViewModel()
                {
                    Id = dashboardId,
                    UserId = UserContext.Id
                };
                var tabs = result.Rows
                    .Cast<DataRow>()
                    .Select(row => new DashboardTabViewModel()
                    {
                        Id = _report.ValueOrDefault<int>(row, "TabID"),
                        Index = _report.ValueOrDefault<int>(row, "Index"),
                        Title = _report.ValueOrDefault(row, "TabTitle")
                    });
                foreach (var tab in tabs.GroupBy(t => t.Id))
                {
                    fullDashboard.Tabs.Add(tab.First());
                }

                var tabWidgets = result.Rows
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
                    .ToArray();

                var widgetIds = tabWidgets
                    .Select(tw => tw.WidgetId)
                    .Distinct();
                if (widgetIds.Any())
                {
                    var query = String.Format(DashboardQuery.WidgetDetails, String.Join(',', widgetIds));
                    var details = DbConsole.ExecuteQuery(query);
                    var widgets = details.Rows
                        .Cast<DataRow>()
                        .Select(row => new TabWidgetViewModel()
                        {
                            WidgetId = _report.ValueOrDefault<int>(row, "WidgetID"),
                            WidgetFunctionId = _report.ValueOrDefault<int>(row, "FunctionID"),
                            WidgetTypeId = _report.ValueOrDefault<int>(row, "TypeID"),
                            WidgetFunctionName = _report.ValueOrDefault(row, "FunctionName"),
                            WidgetTypeName = _report.ValueOrDefault(row, "TypeName")
                        });
                    foreach (var group in widgets.GroupBy(w => w.WidgetId))
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

                    query = String.Format(DashboardQuery.WidgetsAccounts, String.Join(',', widgetIds));
                    var accounts = DbConsole.ExecuteQuery(query);
                    var widgetAccounts = accounts.Rows
                        .Cast<DataRow>()
                        .Select(row => new
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
                        });

                    query = String.Format(DashboardQuery.WidgetsParameters, String.Join(',', widgetIds));
                    var parameters = DbConsole.ExecuteQuery(query);
                    var widgetParameters = parameters.Rows
                        .Cast<DataRow>()
                        .Select(row => new
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
                        });
                    foreach (var tab in fullDashboard.Tabs)
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
            }

            return fullDashboard;
        }

        private async Task LoadDashboardWidgetsAsync(Dashboard dashboard)
        {
            var allTabWidgets = new List<TabWidget>();
            var tabRepository = UnitOfWork.GetAsyncRepository<DashboardTab>();
            var tabs = await tabRepository
                .GetEntityWithTrackingQuery(tab => tab.Widgets)
                .Where(tab => tab.DashboardId == dashboard.Id)
                .ToListAsync();
            dashboard.Tabs.AddRange(tabs);
            Array.ForEach(tabs.ToArray(), tab => allTabWidgets.AddRange(tab.Widgets));
            var tabWidgetRepository = UnitOfWork.GetAsyncRepository<TabWidget>();
            Array.ForEach(allTabWidgets.ToArray(),
                tabWidget => tabWidgetRepository.LoadReference(tabWidget, tw => tw.Widget));
            LoadWidgetsReferences(allTabWidgets.Select(tw => tw.Widget));
        }

        private void LoadWidgetsReferences(IEnumerable<Widget> widgets)
        {
            var widgetRepository = UnitOfWork.GetAsyncRepository<Widget>();
            Array.ForEach(widgets.ToArray(), widget =>
            {
                widgetRepository.LoadCollection(widget, w => w.Accounts);
                widgetRepository.LoadCollection(widget, w => w.Parameters);
            });

            var accountRepository = UnitOfWork.GetAsyncRepository<WidgetAccount>();
            var paramRepository = UnitOfWork.GetAsyncRepository<UsedWidgetParameter>();
            foreach (var widget in widgets)
            {
                Array.ForEach(widget.Accounts.ToArray(), account =>
                {
                    accountRepository.LoadReference(account, acc => acc.Account);
                    accountRepository.LoadReference(account, acc => acc.DetailAccount);
                    accountRepository.LoadReference(account, acc => acc.CostCenter);
                    accountRepository.LoadReference(account, acc => acc.Project);
                });
                Array.ForEach(widget.Parameters.ToArray(),
                    param => paramRepository.LoadReference(param, uwp => uwp.Parameter));
            }
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
