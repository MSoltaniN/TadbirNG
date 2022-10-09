using System;
using System.Collections.Generic;
using System.Linq;
using SPPC.Framework.Extensions;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Persistence.Utility;
using SPPC.Tadbir.Resources;
using SPPC.Tadbir.ViewModel.Finance;

namespace SPPC.Tadbir.Persistence
{
    public partial class DashboardRepository
    {
        private ChartFunctionEvaluator GetChartFunctionEvaluator(string functionName)
        {
            var evaluator = default(ChartFunctionEvaluator);
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

        private GaugeFunctionEvaluator GetGaugeFunctionEvaluator(string functionName)
        {
            var evaluator = default(GaugeFunctionEvaluator);
            switch (functionName)
            {
                case AppStrings.Function_LiquidRatio:
                    evaluator = CalculateLiquidRatio;
                    break;
            }

            return evaluator;
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

        private decimal CalculateLiquidRatio(DateTime from, DateTime to)
        {
            decimal liquidAssets = GetCollectionBalance(AccountCollectionId.LiquidAssets, from, to);
            decimal liquidLiabilities = Math.Max(1.0M, Math.Abs(
                GetCollectionBalance(AccountCollectionId.LiquidLiabilities, from, to)));
            decimal liquidRatio = Math.Round(liquidAssets / liquidLiabilities, 2);
            return liquidRatio;
        }

        private decimal GetCollectionBalance(AccountCollectionId collectionId, DateTime from, DateTime to)
        {
            var accounts = _report.GetUsableAccounts(collectionId);
            return GetCollectionBalance(accounts, from, to);
        }

        private decimal GetCollectionBalance(
            IEnumerable<AccountItemBriefViewModel> accounts, DateTime from, DateTime to)
        {
            decimal balance = 0.0M;
            if (accounts.Any())
            {
                DbConsole.ConnectionString = UnitOfWork.CompanyConnection;
                var query = String.Format(
                    DashboardQuery.CollectionBalance, from.ToShortDateString(false),
                    to.ToShortDateString(false), UserContext.FiscalPeriodId,
                    String.Join(",", accounts.Select(acc => acc.Id)));
                var result = DbConsole.ExecuteQuery(query);
                if (result.Rows.Count > 0)
                {
                    balance = _report.ValueOrDefault<decimal>(result.Rows[0], "Balance");
                }
            }

            return balance;
        }

        private readonly IReportDirectUtility _report;
        private delegate decimal ChartFunctionEvaluator(FullAccountViewModel fullAccount, DateTime from, DateTime to);
        private delegate decimal GaugeFunctionEvaluator(DateTime from, DateTime to);
    }
}
