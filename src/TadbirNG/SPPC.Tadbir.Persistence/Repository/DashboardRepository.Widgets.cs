﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPPC.Framework.Extensions;
using SPPC.Framework.Persistence;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Model.Reporting;
using SPPC.Tadbir.Resources;
using SPPC.Tadbir.Utility;
using SPPC.Tadbir.ViewModel.Finance;
using SPPC.Tadbir.ViewModel.Reporting;

namespace SPPC.Tadbir.Persistence
{
    public partial class DashboardRepository
    {
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

        private class WidgetFunctionValues
        {
            public string XLabel { get; set; }

            public DateTime FromDate { get; set; }

            public DateTime ToDate { get; set; }
        }

        /// <summary>
        /// امکان دسترسی به تنظیمات جاری برنامه را فراهم می کند
        /// </summary>
        public IConfigRepository Config { get; }

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

        /// <summary>
        /// اطلاعات ویجت مشخص شده را با توجه به پارامترهای داده شده محاسبه کرده و برمی گرداند
        /// </summary>
        /// <param name="widgetId">شناسه دیتابیسی ویجت مورد نظر</param>
        /// <param name="fromDate">تاریخ ابتدا برای محاسبه اطلاعات</param>
        /// <param name="toDate">تاریخ انتها برای محاسبه اطلاعات</param>
        /// <param name="unit">واحد زمانی مورد نظر برای نمایش ریز اطلاعات در نمودار</param>
        /// <returns>اطلاعات مورد نیاز برای نمایش در نمودار</returns>
        public async Task<ChartSeriesViewModel> GetWidgetDataAsync(
            int widgetId, DateTime? fromDate, DateTime? toDate, WidgetDateUnit? unit)
        {
            var dataSeries = default(ChartSeriesViewModel);
            var dateUnit = unit ?? WidgetDateUnit.Monthly;
            Config.GetCurrentFiscalDateRange(out DateTime from, out DateTime to);
            from = fromDate ?? from;
            to = toDate ?? to;
            var repository = UnitOfWork.GetAsyncRepository<Widget>();
            var widget = await repository
                .GetEntityWithTrackingQuery(wgt => wgt.Function, wgt => wgt.Accounts)
                .Where(wgt => wgt.Id == widgetId)
                .FirstOrDefaultAsync();
            if (widget != null)
            {
                DbConsole.ConnectionString = UnitOfWork.CompanyConnection;
                var accountRepository = UnitOfWork.GetAsyncRepository<WidgetAccount>();
                var fullAccounts = GetFullAccounts(widget.Accounts, accountRepository);
                var values = await GetFunctionValuesAsync(from, to, dateUnit);
                var evaluator = GetFunctionEvaluator(widget.Function);
                dataSeries = new ChartSeriesViewModel();
                dataSeries.Labels.AddRange(values.Select(value => value.XLabel));
                foreach (var fullAccount in fullAccounts)
                {
                    var serie = new ChartSerieViewModel()
                    {
                        Label = $"{fullAccount.Account.FullCode}-{fullAccount.DetailAccount.FullCode}-{fullAccount.CostCenter.FullCode}-{fullAccount.Project.FullCode}"
                    };
                    serie.Data.AddRange(values
                        .OrderBy(value => value.FromDate)
                        .Select(value => evaluator(fullAccount, value.FromDate, value.ToDate)));
                    dataSeries.Datasets.Add(serie);
                }
            }

            return dataSeries;
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

        private IList<FullAccountViewModel> GetFullAccounts(
            IList<WidgetAccount> accounts, IRepository<WidgetAccount> repository)
        {
            var fullAccounts = new List<FullAccountViewModel>();
            foreach (var account in accounts)
            {
                var fullAccount = new FullAccountViewModel();
                if (account.AccountId.HasValue)
                {
                    repository.LoadReference(account, acc => acc.Account);
                    fullAccount.Account = Mapper.Map<AccountItemBriefViewModel>(account.Account);
                }

                if (account.DetailAccountId.HasValue)
                {
                    repository.LoadReference(account, acc => acc.DetailAccount);
                    fullAccount.DetailAccount = Mapper.Map<AccountItemBriefViewModel>(account.DetailAccount);
                }

                if (account.CostCenterId.HasValue)
                {
                    repository.LoadReference(account, acc => acc.CostCenter);
                    fullAccount.CostCenter = Mapper.Map<AccountItemBriefViewModel>(account.CostCenter);
                }

                if (account.ProjectId.HasValue)
                {
                    repository.LoadReference(account, acc => acc.Project);
                    fullAccount.Project = Mapper.Map<AccountItemBriefViewModel>(account.Project);
                }

                fullAccounts.Add(fullAccount);
            }

            return fullAccounts;
        }

        private async Task<IList<WidgetFunctionValues>> GetFunctionValuesAsync(
            DateTime from, DateTime to, WidgetDateUnit unit)
        {
            var values = new List<WidgetFunctionValues>();
            var calendar = await Config.GetCurrentCalendarAsync();
            if (unit == WidgetDateUnit.Monthly)
            {
                var enumerator = new MonthEnumerator(from, to, calendar);
                values.AddRange(enumerator
                    .GetMonths()
                    .Select(month => new WidgetFunctionValues()
                    {
                        XLabel = Context.Localize(month.Name),
                        FromDate = month.Start,
                        ToDate = month.End
                    }));
            }

            return values;
        }

        private FunctionEvaluator GetFunctionEvaluator(WidgetFunction function)
        {
            var evaluator = default(FunctionEvaluator);
            switch (function.Name)
            {
                case AppStrings.Function_DebitTurnover:
                    evaluator = CalculateDebitTurnover;
                    break;
                case AppStrings.Function_CreditTurnover:
                    evaluator = CalculateCreditTurnover;
                    break;
                case AppStrings.Function_NetTurnover:
                    evaluator = CalculateNetTurnover;
                    break;
                case AppStrings.Function_Balance:
                    evaluator = CalculateBalance;
                    break;
            }

            return evaluator;
        }

        private decimal CalculateDebitTurnover(FullAccountViewModel fullAccount, DateTime from, DateTime to)
        {
            decimal turnover = 0.0M;
            var query = String.Format(
                DashboardQuery.DebitTurnover, GetFullAccountSelectList(fullAccount, true),
                from.ToShortDateString(false), to.ToShortDateString(false), UserContext.FiscalPeriodId,
                GetFullAccountSelectList(fullAccount, false));
            var result = DbConsole.ExecuteQuery(query);
            if (result.Rows.Count > 0)
            {
                turnover = GetCalculatedValue(result, fullAccount, "Debit");
            }

            return turnover;
        }

        private decimal CalculateCreditTurnover(FullAccountViewModel fullAccount, DateTime from, DateTime to)
        {
            decimal turnover = 0.0M;
            var query = String.Format(
                DashboardQuery.CreditTurnover, GetFullAccountSelectList(fullAccount, true),
                from.ToShortDateString(false), to.ToShortDateString(false), UserContext.FiscalPeriodId,
                GetFullAccountSelectList(fullAccount, false));
            var result = DbConsole.ExecuteQuery(query);
            if (result.Rows.Count > 0)
            {
                turnover = GetCalculatedValue(result, fullAccount, "Credit");
            }

            return turnover;
        }

        private decimal CalculateNetTurnover(FullAccountViewModel fullAccount, DateTime from, DateTime to)
        {
            decimal turnover = 0.0M;
            var query = String.Format(
                DashboardQuery.NetTurnover, GetFullAccountSelectList(fullAccount, true),
                from.ToShortDateString(false), to.ToShortDateString(false), UserContext.FiscalPeriodId,
                GetFullAccountSelectList(fullAccount, false));
            var result = DbConsole.ExecuteQuery(query);
            if (result.Rows.Count > 0)
            {
                turnover = GetCalculatedValue(result, fullAccount, "Net");
            }

            return turnover;
        }

        private decimal CalculateBalance(FullAccountViewModel fullAccount, DateTime from, DateTime to)
        {
            decimal balance = 0.0M;
            var query = String.Format(
                DashboardQuery.Balance, GetFullAccountSelectList(fullAccount, true),
                to.ToShortDateString(false), UserContext.FiscalPeriodId,
                GetFullAccountSelectList(fullAccount, false));
            var result = DbConsole.ExecuteQuery(query);
            if (result.Rows.Count > 0)
            {
                balance = GetCalculatedValue(result, fullAccount, "Balance");
            }

            return balance;
        }

        private string GetFullAccountSelectList(FullAccountViewModel fullAccount, bool withAlias)
        {
            var selectList = new List<string>();
            if (fullAccount.Account.Id > 0)
            {
                var codeLength = Config.GetLevelCodeLength(ViewId.Account, fullAccount.Account.Level);
                var selectItem = withAlias
                    ? $"SUBSTRING([acc].[FullCode], 1, {codeLength}) AS [Account]"
                    : $"SUBSTRING([acc].[FullCode], 1, {codeLength})";
                selectList.Add(selectItem);
            }

            if (fullAccount.DetailAccount.Id > 0)
            {
                var codeLength = Config.GetLevelCodeLength(ViewId.DetailAccount, fullAccount.DetailAccount.Level);
                var selectItem = withAlias
                    ? $"SUBSTRING([facc].[FullCode], 1, {codeLength}) AS [DetailAccount]"
                    : $"SUBSTRING([facc].[FullCode], 1, {codeLength})";
                selectList.Add(selectItem);
            }

            if (fullAccount.CostCenter.Id > 0)
            {
                var codeLength = Config.GetLevelCodeLength(ViewId.CostCenter, fullAccount.CostCenter.Level);
                var selectItem = withAlias
                    ? $"SUBSTRING([cc].[FullCode], 1, {codeLength}) AS [CostCenter]"
                    : $"SUBSTRING([cc].[FullCode], 1, {codeLength})";
                selectList.Add(selectItem);
            }

            if (fullAccount.Project.Id > 0)
            {
                var codeLength = Config.GetLevelCodeLength(ViewId.Project, fullAccount.Project.Level);
                var selectItem = withAlias
                    ? $"SUBSTRING([prj].[FullCode], 1, {codeLength}) AS [Project]"
                    : $"SUBSTRING([prj].[FullCode], 1, {codeLength})";
                selectList.Add(selectItem);
            }

            return String.Join(",", selectList);
        }

        private decimal GetCalculatedValue(DataTable result, FullAccountViewModel fullAccount, string field)
        {
            var rows = result.Rows
                .Cast<DataRow>()
                .Select(row => new
                {
                    Account = _report.ValueOrDefault(row, "Account"),
                    DetailAccount = _report.ValueOrDefault(row, "DetailAccount"),
                    CostCenter = _report.ValueOrDefault(row, "CostCenter"),
                    Project = _report.ValueOrDefault(row, "Project"),
                    Value = _report.ValueOrDefault<decimal>(row, field)
                });
            if (fullAccount.Account.Id > 0)
            {
                rows = rows
                    .Where(row => row.Account == fullAccount.Account.FullCode);
            }
            if (fullAccount.DetailAccount.Id > 0)
            {
                rows = rows
                    .Where(row => row.DetailAccount == fullAccount.DetailAccount.FullCode);
            }
            if (fullAccount.CostCenter.Id > 0)
            {
                rows = rows
                    .Where(row => row.CostCenter == fullAccount.CostCenter.FullCode);
            }
            if (fullAccount.Project.Id > 0)
            {
                rows = rows
                    .Where(row => row.Project == fullAccount.Project.FullCode);
            }

            return rows.Any()
                ? rows.First().Value
                : 0.0M;
        }

        private delegate decimal FunctionEvaluator(FullAccountViewModel fullAccount, DateTime from, DateTime to);
    }
}
