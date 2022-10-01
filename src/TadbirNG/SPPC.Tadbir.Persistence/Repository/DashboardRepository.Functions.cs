using System;
using SPPC.Framework.Extensions;
using SPPC.Tadbir.Resources;
using SPPC.Tadbir.ViewModel.Finance;

namespace SPPC.Tadbir.Persistence
{
    public partial class DashboardRepository
    {
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

        private delegate decimal FunctionEvaluator(FullAccountViewModel fullAccount, DateTime from, DateTime to);
    }
}
