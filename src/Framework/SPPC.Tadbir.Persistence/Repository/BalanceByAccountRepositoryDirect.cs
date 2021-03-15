using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPPC.Framework.Extensions;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Persistence.Utility;
using SPPC.Tadbir.ViewModel.Reporting;

namespace SPPC.Tadbir.Persistence.Repository
{
    /// <summary>
    ///
    /// </summary>
    public class BalanceByAccountRepositoryDirect : LoggingRepositoryBase, IBalanceByAccountRepository
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="context">امکانات مشترک مورد نیاز را برای عملیات دیتابیسی فراهم می کند</param>
        /// <param name="system">امکانات مورد نیاز در دیتابیس های سیستمی را فراهم می کند</param>
        /// <param name="report">امکانات عمومی مورد نیاز برای گزارشگیری را فراهم می کند</param>
        public BalanceByAccountRepositoryDirect(IRepositoryContext context, ISystemRepository system,
            IReportDirectUtility report)
            : base(context, system.Logger)
        {
            _system = system;
            _report = report;
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات گزارش مانده به تفکیک حساب را خوانده و برمیگرداند
        /// </summary>
        /// <param name="parameters">پارامترهای مورد نیاز برای گزارش</param>
        /// <returns></returns>
        public async Task<BalanceByAccountViewModel> GetBalanceByAccountAsync(
            BalanceByAccountParameters parameters)
        {
            var balanaceByItem = new BalanceByAccountViewModel();
            DbConsole.ConnectionString = UnitOfWork.CompanyConnection;
            var query = await GetReportQueryAsync(parameters);
            var result = DbConsole.ExecuteQuery(query.Query);
            balanaceByItem.Items.AddRange(result.Rows
                .Cast<DataRow>()
                .Select(row => GetBalanceByAccountItem(row)));
            balanaceByItem.Total = new BalanceByAccountItemViewModel()
            {
                StartBalance = balanaceByItem.Items.Sum(item => item.StartBalance),
                Debit = balanaceByItem.Items.Sum(item => item.Debit),
                Credit = balanaceByItem.Items.Sum(item => item.Credit),
                EndBalance = balanaceByItem.Items.Sum(item => item.EndBalance)
            };

            return balanaceByItem;
        }

        private BalanceByAccountItemViewModel GetBalanceByAccountItem(DataRow row)
        {
            var item = new BalanceByAccountItemViewModel()
            {
                AccountFullCode = _report.ValueOrDefault(row, "AccountFullCode"),
                DetailAccountFullCode = _report.ValueOrDefault(row, "DetailAccountFullCode"),
                CostCenterFullCode = _report.ValueOrDefault(row, "CostCenterFullCode"),
                ProjectFullCode = _report.ValueOrDefault(row, "ProjectFullCode"),
                Debit = _report.ValueOrDefault<decimal>(row, "DebitSum"),
                Credit = _report.ValueOrDefault<decimal>(row, "CreditSum")
            };

            item.EndBalance = item.Debit - item.Credit;
            return item;
        }

        private async Task<ReportQuery> GetReportQueryAsync(BalanceByAccountParameters parameters)
        {
            var queryBuilder = new StringBuilder();
            queryBuilder.AppendLine(GetSelectClause(parameters));
            queryBuilder.AppendLine(GetFromClause(parameters));
            queryBuilder.AppendLine(await GetWhereClauseAsync(parameters));
            queryBuilder.AppendLine(GetGroupByClause(parameters));
            return new ReportQuery(queryBuilder.ToString());
        }

        private string GetSelectClause(BalanceByAccountParameters parameters)
        {
            var selectBuilder = new StringBuilder(
                "SELECT SUM(vl.Debit) AS DebitSum, SUM(vl.Credit) AS CreditSum");
            if (parameters.IsSelectedAccount)
            {
                int length = _report.GetLevelCodeLength(ViewId.Account, parameters.AccountLevel.Value);
                selectBuilder.AppendFormat(", SUBSTRING(acc.FullCode, 1, {0}) AS AccountFullCode", length);
            }

            if (parameters.IsSelectedDetailAccount)
            {
                int length = _report.GetLevelCodeLength(ViewId.DetailAccount, parameters.DetailAccountLevel.Value);
                selectBuilder.AppendFormat(", SUBSTRING(facc.FullCode, 1, {0}) AS DetailAccountFullCode", length);
            }

            if (parameters.IsSelectedCostCenter)
            {
                int length = _report.GetLevelCodeLength(ViewId.CostCenter, parameters.CostCenterLevel.Value);
                selectBuilder.AppendFormat(", SUBSTRING(cc.FullCode, 1, {0}) AS CostCenterFullCode", length);
            }

            if (parameters.IsSelectedProject)
            {
                int length = _report.GetLevelCodeLength(ViewId.Project, parameters.ProjectLevel.Value);
                selectBuilder.AppendFormat(", SUBSTRING(prj.FullCode, 1, {0}) AS ProjectFullCode", length);
            }

            return selectBuilder.ToString();
        }

        private string GetFromClause(BalanceByAccountParameters parameters)
        {
            var fromBuilder = new StringBuilder(@"FROM [Finance].[Voucher] v
    INNER JOIN [Finance].[VoucherLine] vl ON v.VoucherID = vl.VoucherID");
            if (parameters.IsSelectedAccount)
            {
                fromBuilder.AppendLine();
                fromBuilder.Append(
                    "	INNER JOIN [Finance].[Account] acc ON acc.AccountID = vl.AccountID");
            }

            if (parameters.IsSelectedDetailAccount)
            {
                fromBuilder.AppendLine();
                fromBuilder.Append(
                    "	INNER JOIN [Finance].[DetailAccount] facc ON facc.DetailAccountID = vl.DetailID");
            }

            if (parameters.IsSelectedCostCenter)
            {
                fromBuilder.AppendLine();
                fromBuilder.Append(
                    "	INNER JOIN [Finance].[CostCenter] cc ON cc.CostCenterID = vl.CostCenterID");
            }

            if (parameters.IsSelectedProject)
            {
                fromBuilder.AppendLine();
                fromBuilder.Append(
                    "	INNER JOIN [Finance].[Project] prj ON prj.ProjectID = vl.ProjectID");
            }

            return fromBuilder.ToString();
        }

        private async Task<string> GetWhereClauseAsync(BalanceByAccountParameters parameters)
        {
            var whereBuilder = new StringBuilder("WHERE ");
            whereBuilder.Append(ReportQuery.TranslateQuery(
                _report.GetEnvironmentFilters(parameters.GridOptions, UserContext.FiscalPeriodId)));

            if (parameters.FromDate.HasValue && parameters.ToDate.HasValue)
            {
                whereBuilder.AppendFormat(" AND v.Date >= '{0}' AND v.Date <= '{1}'",
                    parameters.FromDate.Value.ToShortDateString(false),
                    parameters.ToDate.Value.ToShortDateString(false));
            }
            else
            {
                whereBuilder.AppendFormat(" AND v.No >= {0} AND v.No <= {1}",
                    parameters.FromNo, parameters.ToNo);
            }

            var options = (TestBalanceOptions)parameters.Options;
            if ((options & TestBalanceOptions.UseClosingVoucher) == 0)
            {
                whereBuilder.AppendFormat(
                    " AND v.OriginID <> {0}", (int)VoucherOriginId.ClosingVoucher);
            }

            if ((options & TestBalanceOptions.UseClosingTempVoucher) == 0)
            {
                whereBuilder.AppendFormat(
                    " AND v.OriginID <> {0}", (int)VoucherOriginId.ClosingTempAccounts);
            }

            if ((options & TestBalanceOptions.OpeningVoucherAsInitBalance) > 0)
            {
                whereBuilder.AppendFormat(
                    " AND v.OriginID <> {0}", (int)VoucherOriginId.OpeningVoucher);
            }

            if (parameters.AccountId.HasValue)
            {
                var account = await _report.GetItemAsync(ViewId.Account, parameters.AccountId.Value);
                whereBuilder.AppendFormat(" AND acc.FullCode LIKE '{0}%'", account.FullCode);
            }

            if (parameters.DetailAccountId.HasValue)
            {
                var detailAccount = await _report.GetItemAsync(
                    ViewId.DetailAccount, parameters.DetailAccountId.Value);
                whereBuilder.AppendFormat(" AND facc.FullCode LIKE '{0}%'", detailAccount.FullCode);
            }

            if (parameters.CostCenterId.HasValue)
            {
                var costCenter = await _report.GetItemAsync(
                    ViewId.CostCenter, parameters.CostCenterId.Value);
                whereBuilder.AppendFormat(" AND cc.FullCode LIKE '{0}%'", costCenter.FullCode);
            }

            if (parameters.ProjectId.HasValue)
            {
                var project = await _report.GetItemAsync(
                    ViewId.Project, parameters.ProjectId.Value);
                whereBuilder.AppendFormat(" AND prj.FullCode LIKE '{0}%'", project.FullCode);
            }

            return whereBuilder.ToString();
        }

        private string GetGroupByClause(BalanceByAccountParameters parameters)
        {
            var groupByBuilder = new StringBuilder("GROUP BY ");
            var clauses = new List<string>();
            if (parameters.IsSelectedAccount)
            {
                int length = _report.GetLevelCodeLength(ViewId.Account, parameters.AccountLevel.Value);
                string clause = String.Format("SUBSTRING(acc.FullCode, 1, {0})", length);
                if (parameters.ViewId == ViewId.Account)
                {
                    clauses.Insert(0, clause);
                }
                else
                {
                    clauses.Add(clause);
                }
            }

            if (parameters.IsSelectedDetailAccount)
            {
                int length = _report.GetLevelCodeLength(ViewId.DetailAccount, parameters.DetailAccountLevel.Value);
                string clause = String.Format("SUBSTRING(facc.FullCode, 1, {0})", length);
                if (parameters.ViewId == ViewId.DetailAccount)
                {
                    clauses.Insert(0, clause);
                }
                else
                {
                    clauses.Add(clause);
                }
            }

            if (parameters.IsSelectedCostCenter)
            {
                int length = _report.GetLevelCodeLength(ViewId.CostCenter, parameters.CostCenterLevel.Value);
                string clause = String.Format("SUBSTRING(cc.FullCode, 1, {0})", length);
                if (parameters.ViewId == ViewId.CostCenter)
                {
                    clauses.Insert(0, clause);
                }
                else
                {
                    clauses.Add(clause);
                }
            }

            if (parameters.IsSelectedProject)
            {
                int length = _report.GetLevelCodeLength(ViewId.Project, parameters.ProjectLevel.Value);
                string clause = String.Format("SUBSTRING(prj.FullCode, 1, {0})", length);
                if (parameters.ViewId == ViewId.Project)
                {
                    clauses.Insert(0, clause);
                }
                else
                {
                    clauses.Add(clause);
                }
            }

            groupByBuilder.Append(String.Join(", ", clauses));
            return groupByBuilder.ToString();
        }

        private readonly ISystemRepository _system;
        private readonly IReportDirectUtility _report;
    }
}
