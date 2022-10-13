using System;
using System.Collections.Generic;
using System.Linq;
using SPPC.Framework.Extensions;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Persistence.Utility;
using SPPC.Tadbir.Resources;
using SPPC.Tadbir.ViewModel.Finance;
using SPPC.Tadbir.ViewModel.Reporting;

namespace SPPC.Tadbir.Persistence
{
    public partial class DashboardRepository
    {
        private WidgetFunctionEvaluator GetFunctionEvaluator(string functionName)
        {
            var evaluator = default(WidgetFunctionEvaluator);
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
                case AppStrings.FunctionXT_GrossSales:
                    evaluator = CalculateGrossSales;
                    break;
                case AppStrings.FunctionXT_NetSales:
                    evaluator = CalculateNetSales;
                    break;
                case AppStrings.FunctionXB_LiquidRatio:
                    evaluator = CalculateLiquidRatio;
                    break;
                case AppStrings.FunctionXB_BankBalance:
                    evaluator = CalculateBankBalance;
                    break;
                case AppStrings.FunctionXB_CashBalance:
                    evaluator = CalculateCashBalance;
                    break;
            }

            return evaluator;
        }

        private ChartSeriesViewModel GetBasicFunctionData(string functionName,
            IEnumerable<WidgetFunctionValues> values, IEnumerable<FullAccountViewModel> fullAccounts)
        {
            var dataSeries = new ChartSeriesViewModel();
            dataSeries.Labels.AddRange(values.Select(value => value.XLabel));
            var evaluator = GetFunctionEvaluator(functionName);
            foreach (var fullAccount in fullAccounts)
            {
                var serie = new ChartSerieViewModel()
                {
                    Label = GetFullAccountLabel(fullAccount)
                };
                serie.Data.AddRange(values
                    .OrderBy(value => value.FromDate)
                    .Select(value => evaluator(value.FromDate, value.ToDate, fullAccount)));
                dataSeries.Datasets.Add(serie);
            }

            return dataSeries;
        }

        private ChartSeriesViewModel GetSpecialFunctionData(
            string functionName, IEnumerable<WidgetFunctionValues> values)
        {
            var dataSeries = new ChartSeriesViewModel();
            dataSeries.Labels.AddRange(values.Select(value => value.XLabel));
            var evaluator = GetFunctionEvaluator(functionName);
            var serie = new ChartSerieViewModel()
            {
                Label = functionName
            };
            serie.Data.AddRange(values
                .OrderBy(value => value.FromDate)
                .Select(value => evaluator(value.FromDate, value.ToDate)));
            dataSeries.Datasets.Add(serie);
            return dataSeries;
        }

        private decimal CalculateDebitTurnover(DateTime from, DateTime to, FullAccountViewModel fullAccount)
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

        private decimal CalculateCreditTurnover(DateTime from, DateTime to, FullAccountViewModel fullAccount)
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

        private decimal CalculateNetTurnover(DateTime from, DateTime to, FullAccountViewModel fullAccount)
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

        private decimal CalculateBalance(DateTime from, DateTime to, FullAccountViewModel fullAccount)
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

        private decimal CalculateGrossSales(DateTime from, DateTime to, FullAccountViewModel fullAccount = null)
        {
            var balance = GetCollectionBalance(AccountCollectionId.Sales, from, to);
            return Math.Abs(balance);
        }

        private decimal CalculateNetSales(DateTime from, DateTime to, FullAccountViewModel fullAccount = null)
        {
            var grossSales = CalculateGrossSales(from, to);
            var deficitAccounts = new List<AccountItemBriefViewModel>();
            deficitAccounts.AddRange(_report.GetUsableAccounts(AccountCollectionId.SalesRefund));
            deficitAccounts.AddRange(_report.GetUsableAccounts(AccountCollectionId.SalesDiscount));
            var refundDiscount = GetCollectionBalance(deficitAccounts.Select(acc => acc.Id), from, to);
            return Math.Abs(grossSales - refundDiscount);
        }

        private decimal CalculateLiquidRatio(DateTime from, DateTime to, FullAccountViewModel fullAccount = null)
        {
            decimal liquidAssets = GetCollectionBalance(AccountCollectionId.LiquidAssets, from, to);
            decimal liquidLiabilities = Math.Max(1.0M, Math.Abs(
                GetCollectionBalance(AccountCollectionId.LiquidLiabilities, from, to)));
            decimal liquidRatio = Math.Round(liquidAssets / liquidLiabilities, 2);
            return liquidRatio;
        }

        private decimal CalculateBankBalance(DateTime from, DateTime to, FullAccountViewModel fullAccount = null)
        {
            return GetCollectionBalance(AccountCollectionId.Bank, from, to);
        }

        private decimal CalculateCashBalance(DateTime from, DateTime to, FullAccountViewModel fullAccount = null)
        {
            return GetCollectionBalance(AccountCollectionId.Cashier, from, to);
        }

        private decimal GetCollectionBalance(AccountCollectionId collectionId, DateTime from, DateTime to)
        {
            var accounts = _report.GetUsableAccounts(collectionId);
            var accountIds = accounts.Select(acc => acc.Id);
            return GetCollectionBalance(accountIds, from, to);
        }

        private decimal GetCollectionBalance(
            IEnumerable<int> accountIds, DateTime from, DateTime to)
        {
            decimal balance = 0.0M;
            if (accountIds.Any())
            {
                DbConsole.ConnectionString = UnitOfWork.CompanyConnection;
                var query = String.Format(
                    DashboardQuery.CollectionBalance, from.ToShortDateString(false),
                    to.ToShortDateString(false), UserContext.FiscalPeriodId,
                    String.Join(",", accountIds));
                var result = DbConsole.ExecuteQuery(query);
                if (result.Rows.Count > 0)
                {
                    balance = _report.ValueOrDefault<decimal>(result.Rows[0], "Balance");
                }
            }

            return balance;
        }

        private readonly IReportDirectUtility _report;
        private delegate decimal WidgetFunctionEvaluator(
            DateTime from, DateTime to, FullAccountViewModel fullAccount = null);
    }
}
