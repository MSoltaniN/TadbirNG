using System;
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

        private enum ExpressionUsage
        {
            Select = 0,
            Where = 1,
            GroupBy = 2
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
            var query = String.Format(DashboardQuery.CurrentDashboard, UserContext.Id);
            DbConsole.ConnectionString = UnitOfWork.CompanyConnection;
            var dashboardResult = DbConsole.ExecuteQuery(query);
            return GetDashboard(dashboardResult);
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
            Config.GetCurrentFiscalDateRange(out DateTime start, out DateTime end);
            start = fromDate ?? start;
            end = toDate ?? end;
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
                var values = await GetFunctionValuesAsync(start, end, dateUnit);
                var evaluator = GetFunctionEvaluator(widget.Function.Name);
                dataSeries = new ChartSeriesViewModel();
                dataSeries.Labels.AddRange(values.Select(value => value.XLabel));
                foreach (var fullAccount in fullAccounts)
                {
                    var serie = new ChartSerieViewModel()
                    {
                        Label = GetFullAccountLabel(fullAccount)
                    };
                    serie.Data.AddRange(values
                        .OrderBy(value => value.FromDate)
                        .Select(value => evaluator(fullAccount, value.FromDate, value.ToDate)));
                    dataSeries.Datasets.Add(serie);
                }
            }

            return dataSeries;
        }

        /// <summary>
        /// به روش آسنکرون، یکی از ویجت های قابل دسترسی توسط کاربر جاری را در برگه تعیین شده اضافه یا اصلاح می کند
        /// </summary>
        /// <param name="tabWidget">اطلاعات ویجت مورد نظر برای ایجاد یا اصلاح به برگه داشبورد</param>
        /// <returns>آخرین اطلاعات ویجت اضافه یا اصلاح شده در برگه داشبورد</returns>
        public async Task<TabWidgetViewModel> SaveTabWidgetAsync(TabWidgetViewModel tabWidget)
        {
            var repository = UnitOfWork.GetAsyncRepository<TabWidget>();
            if (tabWidget.Id == 0)
            {
                var saved = Mapper.Map<TabWidget>(tabWidget);
                repository.Insert(saved);
                await UnitOfWork.CommitAsync();
                var widgetRepository = UnitOfWork.GetAsyncRepository<Widget>();
                var widgetInfo = await widgetRepository
                    .GetEntityQuery()
                    .Where(wgt => wgt.Id == tabWidget.WidgetId)
                    .Select(wgt => new
                    {
                        wgt.Title,
                        wgt.TypeId
                    })
                    .SingleOrDefaultAsync();
                var mapped = Mapper.Map<TabWidgetViewModel>(saved);
                mapped.WidgetTitle = widgetInfo.Title;
                mapped.WidgetTypeId = widgetInfo.TypeId;
                return mapped;
            }
            else
            {
                var existing = await repository.GetByIDAsync(tabWidget.Id);
                if (existing != null)
                {
                    // TODO: Update tab-widget values here... 
                }
            }

            return null;
        }

        /// <summary>
        /// به روش آسنکرون، ویجت داده شده را از برگه مورد نظر در داشبورد کاربر جاری حذف می کند
        /// </summary>
        /// <param name="tabId">شناسه دیتابیسی برگه ای که ویجت از آن حذف می شود</param>
        /// <param name="widgetId">شناسه دیتابیسی ویجتی که از برگه مورد نظر حذف می شود</param>
        public async Task DeleteTabWidgetAsync(int tabId, int widgetId)
        {
            var repository = UnitOfWork.GetAsyncRepository<TabWidget>();
            var existing = await repository.GetFirstByCriteriaAsync(
                tw => tw.TabId == tabId && tw.WidgetId == widgetId);
            if (existing != null)
            {
                repository.Delete(existing);
                await UnitOfWork.CommitAsync();
            }
        }

        private DashboardViewModel GetDashboard(DataTable dashboardResult)
        {
            var dashboard = DashboardFromResult(dashboardResult);
            if (dashboard != null)
            {
                var query = String.Format(DashboardQuery.CurrentDashboardWidgets, dashboard.Id);
                var widgetResult = DbConsole.ExecuteQuery(query);
                var tabWidgets = widgetResult.Rows
                    .Cast<DataRow>()
                    .Select(row => new TabWidgetViewModel()
                    {
                        TabId = _report.ValueOrDefault<int>(row, "TabID"),
                        WidgetId = _report.ValueOrDefault<int>(row, "WidgetID"),
                        WidgetTitle = _report.ValueOrDefault(row, "Title"),
                        WidgetDescription = _report.ValueOrDefault(row, "Description"),
                        Settings = _report.ValueOrDefault(row, "Settings"),
                        DefaultSettings = _report.ValueOrDefault(row, "DefaultSettings")
                    })
                    .ToList();

                LoadWidgetDetails(tabWidgets);
                var widgetIds = tabWidgets
                    .Select(tw => tw.WidgetId)
                    .Distinct();
                var accounts = GetWidgetAccounts(widgetIds);
                var parameters = GetWidgetParameters(widgetIds);
                foreach (var tab in dashboard.Tabs)
                {
                    tab.Widgets.AddRange(tabWidgets.Where(tw => tw.TabId == tab.Id));
                    foreach (var widget in tab.Widgets)
                    {
                        widget.WidgetAccounts.AddRange(accounts
                            .Where(wacc => wacc.WidgetId == widget.WidgetId)
                            .Select(wacc => wacc.FullAccount));
                        widget.WidgetParmeters.AddRange(parameters
                            .Where(wpara => wpara.WidgetId == widget.WidgetId)
                            .Select(wpara => wpara.Parameter));
                    }
                }
            }

            return dashboard;
        }

        private DashboardViewModel DashboardFromResult(DataTable dashboardResult)
        {
            var dashboard = default(DashboardViewModel);
            if (dashboardResult.Rows.Count > 0)
            {
                int dashboardId = _report.ValueOrDefault<int>(dashboardResult.Rows[0], "DashboardID");
                dashboard = new DashboardViewModel()
                {
                    Id = dashboardId,
                    UserId = UserContext.Id
                };
                dashboard.Tabs.AddRange(dashboardResult.Rows
                    .Cast<DataRow>()
                    .Select(row => new DashboardTabViewModel()
                    {
                        Id = _report.ValueOrDefault<int>(row, "TabID"),
                        Index = _report.ValueOrDefault<int>(row, "TabIndex"),
                        Title = _report.ValueOrDefault(row, "TabTitle")
                    }));
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
                var detailsResult = DbConsole.ExecuteQuery(query);
                var details = detailsResult.Rows
                    .Cast<DataRow>()
                    .Select(row => new TabWidgetViewModel()
                    {
                        WidgetId = _report.ValueOrDefault<int>(row, "WidgetID"),
                        WidgetFunctionId = _report.ValueOrDefault<int>(row, "FunctionID"),
                        WidgetTypeId = _report.ValueOrDefault<int>(row, "TypeID"),
                        WidgetFunctionName = _report.ValueOrDefault(row, "FunctionName"),
                        WidgetTypeName = _report.ValueOrDefault(row, "TypeName")
                    });
                foreach (var group in details.GroupBy(w => w.WidgetId))
                {
                    var widget = group.First();
                    Array.ForEach(tabWidgets
                        .Where(item => item.WidgetId == group.Key)
                        .ToArray(), tw =>
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
            var accounts = new List<AccountByWidget>();
            if (widgetIds.Any())
            {
                var query = String.Format(DashboardQuery.WidgetsAccounts, String.Join(',', widgetIds));
                var accountsResult = DbConsole.ExecuteQuery(query);
                accounts.AddRange(accountsResult.Rows
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

            return accounts;
        }

        private IEnumerable<ParameterByWidget> GetWidgetParameters(IEnumerable<int> widgetIds)
        {
            var parameters = new List<ParameterByWidget>();
            if (widgetIds.Any())
            {

                var query = String.Format(DashboardQuery.WidgetsParameters, String.Join(',', widgetIds));
                var parametersResult = DbConsole.ExecuteQuery(query);
                parameters.AddRange(parametersResult.Rows
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

            return parameters;
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

        private FunctionEvaluator GetFunctionEvaluator(string functionName)
        {
            var evaluator = default(FunctionEvaluator);
            switch (functionName)
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

        private static string GetFullAccountLabel(FullAccountViewModel fullAccount)
        {
            return $"{fullAccount.Account.FullCode}-{fullAccount.DetailAccount.FullCode}-" +
                $"{fullAccount.CostCenter.FullCode}-{fullAccount.Project.FullCode}";
        }

        private decimal CalculateDebitTurnover(FullAccountViewModel fullAccount, DateTime from, DateTime to)
        {
            decimal turnover = 0.0M;
            var query = String.Format(
                DashboardQuery.DebitTurnover, GetFullAccountExpressions(fullAccount, ExpressionUsage.Select),
                from.ToShortDateString(false), to.ToShortDateString(false), UserContext.FiscalPeriodId,
                GetFullAccountExpressions(fullAccount, ExpressionUsage.Where),
                GetFullAccountExpressions(fullAccount, ExpressionUsage.GroupBy));
            var result = DbConsole.ExecuteQuery(query);
            if (result.Rows.Count > 0)
            {
                turnover = _report.ValueOrDefault<decimal>(result.Rows[0], "Debit");
            }

            return turnover;
        }

        private decimal CalculateCreditTurnover(FullAccountViewModel fullAccount, DateTime from, DateTime to)
        {
            decimal turnover = 0.0M;
            var query = String.Format(
                DashboardQuery.CreditTurnover, GetFullAccountExpressions(fullAccount, ExpressionUsage.Select),
                from.ToShortDateString(false), to.ToShortDateString(false), UserContext.FiscalPeriodId,
                GetFullAccountExpressions(fullAccount, ExpressionUsage.Where),
                GetFullAccountExpressions(fullAccount, ExpressionUsage.GroupBy));
            var result = DbConsole.ExecuteQuery(query);
            if (result.Rows.Count > 0)
            {
                turnover = _report.ValueOrDefault<decimal>(result.Rows[0], "Credit");
            }

            return turnover;
        }

        private decimal CalculateNetTurnover(FullAccountViewModel fullAccount, DateTime from, DateTime to)
        {
            decimal turnover = 0.0M;
            var query = String.Format(
                DashboardQuery.NetTurnover, GetFullAccountExpressions(fullAccount, ExpressionUsage.Select),
                from.ToShortDateString(false), to.ToShortDateString(false), UserContext.FiscalPeriodId,
                GetFullAccountExpressions(fullAccount, ExpressionUsage.Where),
                GetFullAccountExpressions(fullAccount, ExpressionUsage.GroupBy));
            var result = DbConsole.ExecuteQuery(query);
            if (result.Rows.Count > 0)
            {
                turnover = _report.ValueOrDefault<decimal>(result.Rows[0], "Net");
            }

            return turnover;
        }

        private decimal CalculateBalance(FullAccountViewModel fullAccount, DateTime from, DateTime to)
        {
            decimal balance = 0.0M;
            var query = String.Format(
                DashboardQuery.Balance, GetFullAccountExpressions(fullAccount, ExpressionUsage.Select),
                to.ToShortDateString(false), UserContext.FiscalPeriodId,
                GetFullAccountExpressions(fullAccount, ExpressionUsage.Where),
                GetFullAccountExpressions(fullAccount, ExpressionUsage.GroupBy));
            var result = DbConsole.ExecuteQuery(query);
            if (result.Rows.Count > 0)
            {
                balance = _report.ValueOrDefault<decimal>(result.Rows[0], "Balance");
            }

            return balance;
        }

        private string GetFullAccountExpressions(FullAccountViewModel fullAccount, ExpressionUsage usage)
        {
            var selectList = new List<string>();
            if (fullAccount.Account.Id > 0)
            {
                var codeLength = Config.GetLevelCodeLength(ViewId.Account, fullAccount.Account.Level);
                var selectItem = usage == ExpressionUsage.Select
                    ? $"SUBSTRING([acc].[FullCode], 1, {codeLength}) AS [Account]"
                    : (usage == ExpressionUsage.GroupBy
                        ? $"SUBSTRING([acc].[FullCode], 1, {codeLength})"
                        : $"SUBSTRING([acc].[FullCode], 1, {codeLength}) = '{fullAccount.Account.FullCode}'");
                selectList.Add(selectItem);
            }

            if (fullAccount.DetailAccount.Id > 0)
            {
                var codeLength = Config.GetLevelCodeLength(ViewId.DetailAccount, fullAccount.DetailAccount.Level);
                var selectItem = usage == ExpressionUsage.Select
                    ? $"SUBSTRING([facc].[FullCode], 1, {codeLength}) AS [DetailAccount]"
                    : (usage == ExpressionUsage.GroupBy
                        ? $"SUBSTRING([facc].[FullCode], 1, {codeLength})"
                        : $"SUBSTRING([facc].[FullCode], 1, {codeLength}) = '{fullAccount.DetailAccount.FullCode}'");
                selectList.Add(selectItem);
            }

            if (fullAccount.CostCenter.Id > 0)
            {
                var codeLength = Config.GetLevelCodeLength(ViewId.CostCenter, fullAccount.CostCenter.Level);
                var selectItem = usage == ExpressionUsage.Select
                    ? $"SUBSTRING([cc].[FullCode], 1, {codeLength}) AS [CostCenter]"
                    : (usage == ExpressionUsage.GroupBy
                        ? $"SUBSTRING([cc].[FullCode], 1, {codeLength})"
                        : $"SUBSTRING([cc].[FullCode], 1, {codeLength}) = '{fullAccount.CostCenter.FullCode}'");
                selectList.Add(selectItem);
            }

            if (fullAccount.Project.Id > 0)
            {
                var codeLength = Config.GetLevelCodeLength(ViewId.Project, fullAccount.Project.Level);
                var selectItem = usage == ExpressionUsage.Select
                    ? $"SUBSTRING([prj].[FullCode], 1, {codeLength}) AS [Project]"
                    : (usage == ExpressionUsage.GroupBy
                        ? $"SUBSTRING([prj].[FullCode], 1, {codeLength})"
                        : $"SUBSTRING([prj].[FullCode], 1, {codeLength}) = '{fullAccount.Project.FullCode}'");
                selectList.Add(selectItem);
            }

            return usage == ExpressionUsage.Where
                ? String.Join(" AND ", selectList)
                : String.Join(",", selectList);
        }

        private delegate decimal FunctionEvaluator(FullAccountViewModel fullAccount, DateTime from, DateTime to);
    }
}
