using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPPC.Framework.Extensions;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Persistence.Utility;
using SPPC.Tadbir.ViewModel;
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
            _utility = report;
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
            var turnover = DbConsole.ExecuteQuery(query.Query);

            query = await GetReportQueryAsync(parameters, true);
            var initBalance = DbConsole.ExecuteQuery(query.Query);
            balanaceByItem.Items.AddRange(GetMergedItems(initBalance, turnover));
            balanaceByItem.Total = new BalanceByAccountItemViewModel()
            {
                StartBalance = balanaceByItem.Items.Sum(item => item.StartBalance),
                Debit = balanaceByItem.Items.Sum(item => item.Debit),
                Credit = balanaceByItem.Items.Sum(item => item.Credit),
                EndBalance = balanaceByItem.Items.Sum(item => item.EndBalance)
            };

            SetItemNames(parameters, balanaceByItem);
            return balanaceByItem;
        }

        private static bool IsZeroItem(BalanceByAccountItemViewModel item)
        {
            return item.StartBalance == 0.0M
                && item.Debit == 0.0M
                && item.Credit == 0.0M
                && item.EndBalance == 0.0M;
        }

        private static string GetFromClause(BalanceByAccountParameters parameters)
        {
            var fromBuilder = new StringBuilder(@"FROM [Finance].[Voucher] v
    INNER JOIN [Finance].[VoucherLine] vl ON v.VoucherID = vl.VoucherID");
            if (parameters.IsSelectedAccount)
            {
                fromBuilder.AppendLine();
                fromBuilder.Append(
                    "    INNER JOIN [Finance].[Account] acc ON acc.AccountID = vl.AccountID");
            }

            if (parameters.IsSelectedDetailAccount)
            {
                string joinType = parameters.ViewId == ViewId.DetailAccount
                    ? "INNER JOIN"
                    : "LEFT OUTER JOIN";
                fromBuilder.AppendLine();
                fromBuilder.AppendFormat(
                    "    {0} [Finance].[DetailAccount] facc ON facc.DetailAccountID = vl.DetailID", joinType);
            }

            if (parameters.IsSelectedCostCenter)
            {
                string joinType = parameters.ViewId == ViewId.CostCenter
                    ? "INNER JOIN"
                    : "LEFT OUTER JOIN";
                fromBuilder.AppendLine();
                fromBuilder.AppendFormat(
                    "    {0} [Finance].[CostCenter] cc ON cc.CostCenterID = vl.CostCenterID", joinType);
            }

            if (parameters.IsSelectedProject)
            {
                string joinType = parameters.ViewId == ViewId.Project
                    ? "INNER JOIN"
                    : "LEFT OUTER JOIN";
                fromBuilder.AppendLine();
                fromBuilder.AppendFormat(
                    "    {0} [Finance].[Project] prj ON prj.ProjectID = vl.ProjectID", joinType);
            }

            if (parameters.IsByBranch)
            {
                fromBuilder.AppendLine();
                fromBuilder.Append(
                    "    INNER JOIN [Corporate].[Branch] br ON br.BranchID = vl.BranchID");
            }

            return fromBuilder.ToString();
        }

        private IEnumerable<BalanceByAccountItemViewModel> GetMergedItems(
            DataTable initBalance, DataTable turnover)
        {
            var items = new List<BalanceByAccountItemViewModel>();
            var initMap = new Dictionary<FullAccountCodeBranch, BalanceByAccountItemViewModel>();
            var turnoverMap = new Dictionary<FullAccountCodeBranch, BalanceByAccountItemViewModel>();
            foreach (var row in initBalance.Rows.Cast<DataRow>())
            {
                var item = GetBalanceByAccountItem(row);
                var key = Mapper.Map<FullAccountCodeBranch>(item);
                item.StartBalance = item.Debit - item.Credit;
                initMap.Add(key, item);
            }

            foreach (var row in turnover.Rows.Cast<DataRow>())
            {
                var item = GetBalanceByAccountItem(row);
                var key = Mapper.Map<FullAccountCodeBranch>(item);
                turnoverMap.Add(key, item);
            }

            var balanceItem = default(BalanceByAccountItemViewModel);
            var allKeys = initMap.Keys.Cast<FullAccountCodeBranch>()
                .Concat(turnoverMap.Keys.Cast<FullAccountCodeBranch>())
                .Distinct();
            foreach (var key in allKeys)
            {
                if (initMap.ContainsKey(key) && turnoverMap.ContainsKey(key))
                {
                    balanceItem = initMap[key];
                    var turnoverItem = turnoverMap[key];
                    balanceItem.Debit = turnoverItem.Debit;
                    balanceItem.Credit = turnoverItem.Credit;
                    balanceItem.EndBalance =
                        balanceItem.StartBalance + balanceItem.Debit - balanceItem.Credit;
                }
                else if (initMap.ContainsKey(key) && !turnoverMap.ContainsKey(key))
                {
                    balanceItem = initMap[key];
                    balanceItem.Debit = 0.0M;
                    balanceItem.Credit = 0.0M;
                    balanceItem.EndBalance = balanceItem.StartBalance;
                }
                else
                {
                    // Item has turnover but doesn't have initial balance
                    balanceItem = turnoverMap[key];
                    balanceItem.EndBalance = balanceItem.Debit - balanceItem.Credit;
                }

                items.Add(balanceItem);
            }

            return items.Where(item => !IsZeroItem(item));
        }

        private BalanceByAccountItemViewModel GetBalanceByAccountItem(DataRow row)
        {
            return new BalanceByAccountItemViewModel()
            {
                AccountFullCode = _utility.ValueOrDefault(row, "AccountFullCode"),
                DetailAccountFullCode = _utility.ValueOrDefault(row, "DetailAccountFullCode"),
                CostCenterFullCode = _utility.ValueOrDefault(row, "CostCenterFullCode"),
                ProjectFullCode = _utility.ValueOrDefault(row, "ProjectFullCode"),
                Debit = _utility.ValueOrDefault<decimal>(row, "DebitSum"),
                Credit = _utility.ValueOrDefault<decimal>(row, "CreditSum"),
                BranchName = _utility.ValueOrDefault(row, "BranchName")
            };
        }

        private async Task<ReportQuery> GetReportQueryAsync(
            BalanceByAccountParameters parameters, bool isInit = false)
        {
            var queryBuilder = new StringBuilder();
            queryBuilder.AppendLine(GetSelectClause(parameters));
            queryBuilder.AppendLine(GetFromClause(parameters));
            if (isInit)
            {
                queryBuilder.AppendLine(await GetInitWhereClauseAsync(parameters));
            }
            else
            {
                queryBuilder.AppendLine(await GetWhereClauseAsync(parameters));
            }

            queryBuilder.AppendLine(GetGroupByClause(parameters));
            return new ReportQuery(queryBuilder.ToString());
        }

        private void SetItemNames(BalanceByAccountParameters parameters,
            BalanceByAccountViewModel balanceByAccount)
        {
            var accountLookup = new Dictionary<string, string>();
            var detailAccountLookup = new Dictionary<string, string>();
            var costCenterLookup = new Dictionary<string, string>();
            var projectLookup = new Dictionary<string, string>();
            if (parameters.IsSelectedAccount)
            {
                int length = _utility.GetLevelCodeLength(
                    ViewId.Account, parameters.AccountLevel.Value);
                accountLookup = GetItemLookup(
                    length, _utility.GetItemName(ViewId.Account),
                    balanceByAccount.Items.Select(item => item.AccountFullCode));
            }

            if (parameters.IsSelectedDetailAccount)
            {
                int length = _utility.GetLevelCodeLength(
                    ViewId.DetailAccount, parameters.DetailAccountLevel.Value);
                detailAccountLookup = GetItemLookup(
                    length, _utility.GetItemName(ViewId.DetailAccount),
                    balanceByAccount.Items.Select(item => item.DetailAccountFullCode));
            }

            if (parameters.IsSelectedCostCenter)
            {
                int length = _utility.GetLevelCodeLength(
                    ViewId.CostCenter, parameters.CostCenterLevel.Value);
                costCenterLookup = GetItemLookup(
                    length, _utility.GetItemName(ViewId.CostCenter),
                    balanceByAccount.Items.Select(item => item.CostCenterFullCode));
            }

            if (parameters.IsSelectedProject)
            {
                int length = _utility.GetLevelCodeLength(
                    ViewId.Project, parameters.ProjectLevel.Value);
                projectLookup = GetItemLookup(
                    length, _utility.GetItemName(ViewId.Project),
                    balanceByAccount.Items.Select(item => item.ProjectFullCode));
            }

            foreach (var item in balanceByAccount.Items)
            {
                if (!String.IsNullOrEmpty(item.AccountFullCode))
                {
                    item.AccountName = accountLookup[item.AccountFullCode];
                }

                if (!String.IsNullOrEmpty(item.DetailAccountFullCode))
                {
                    item.DetailAccountName = detailAccountLookup[item.DetailAccountFullCode];
                }

                if (!String.IsNullOrEmpty(item.CostCenterFullCode))
                {
                    item.CostCenterName = costCenterLookup[item.CostCenterFullCode];
                }

                if (!String.IsNullOrEmpty(item.ProjectFullCode))
                {
                    item.ProjectName = projectLookup[item.ProjectFullCode];
                }
            }
        }

        private Dictionary<string, string> GetItemLookup(
            int length, string componentName, IEnumerable<string> fullCodes)
        {
            if (fullCodes.Count() == 0)
            {
                return new Dictionary<string, string>();
            }

            string fullCodeList = String.Join(",", fullCodes
                .Where(item => item != null)
                .Distinct()
                .Select(item => String.Format("'{0}'", item)));
            var query = new ReportQuery(String.Format(AccountItemQuery.ItemLookupExact,
                length, componentName, UserContext.FiscalPeriodId, fullCodeList));
            var result = DbConsole.ExecuteQuery(query.Query);

            var itemLookup = new Dictionary<string, string>(result.Rows.Count);
            foreach (var row in result.Rows.Cast<DataRow>())
            {
                itemLookup.Add(_utility.ValueOrDefault(row, "FullCode"),
                    _utility.ValueOrDefault(row, "Name"));
            }

            return itemLookup;
        }

        private string GetSelectClause(BalanceByAccountParameters parameters)
        {
            var selectBuilder = new StringBuilder(
                "SELECT SUM(vl.Debit) AS DebitSum, SUM(vl.Credit) AS CreditSum");
            if (parameters.IsSelectedAccount)
            {
                int length = _utility.GetLevelCodeLength(ViewId.Account, parameters.AccountLevel.Value);
                selectBuilder.AppendFormat(", SUBSTRING(acc.FullCode, 1, {0}) AS AccountFullCode", length);
            }

            if (parameters.IsSelectedDetailAccount)
            {
                int length = _utility.GetLevelCodeLength(ViewId.DetailAccount, parameters.DetailAccountLevel.Value);
                selectBuilder.AppendFormat(", SUBSTRING(facc.FullCode, 1, {0}) AS DetailAccountFullCode", length);
            }

            if (parameters.IsSelectedCostCenter)
            {
                int length = _utility.GetLevelCodeLength(ViewId.CostCenter, parameters.CostCenterLevel.Value);
                selectBuilder.AppendFormat(", SUBSTRING(cc.FullCode, 1, {0}) AS CostCenterFullCode", length);
            }

            if (parameters.IsSelectedProject)
            {
                int length = _utility.GetLevelCodeLength(ViewId.Project, parameters.ProjectLevel.Value);
                selectBuilder.AppendFormat(", SUBSTRING(prj.FullCode, 1, {0}) AS ProjectFullCode", length);
            }

            if (parameters.IsByBranch)
            {
                selectBuilder.Append(", br.Name AS BranchName");
            }

            return selectBuilder.ToString();
        }

        private async Task<string> GetWhereClauseAsync(BalanceByAccountParameters parameters)
        {
            var whereBuilder = new StringBuilder(await GetCommonWhereClauseAsync(parameters));
            var options = (TestBalanceOptions)parameters.Options;
            if ((options & TestBalanceOptions.OpeningVoucherAsInitBalance) > 0)
            {
                whereBuilder.AppendFormat(
                    " AND v.OriginID <> {0}", (int)VoucherOriginId.OpeningVoucher);
            }

            return whereBuilder.ToString();
        }

        private async Task<string> GetInitWhereClauseAsync(BalanceByAccountParameters parameters)
        {
            var options = (TestBalanceOptions)parameters.Options;
            string newPredicate = String.Empty;
            string whereClause = await GetCommonWhereClauseAsync(parameters);
            if (parameters.FromDate.HasValue && parameters.ToDate.HasValue)
            {
                string predicate = String.Format("v.Date >= '{0}' AND v.Date <= '{1}'",
                    parameters.FromDate.Value.ToShortDateString(false),
                    parameters.ToDate.Value.ToShortDateString(false));
                if ((options & TestBalanceOptions.OpeningVoucherAsInitBalance) == 0)
                {
                    newPredicate = String.Format(
                        "(v.Date < '{0}' AND v.OriginID <> 2)",
                        parameters.FromDate.Value.ToShortDateString(false));
                }
                else
                {
                    newPredicate = String.Format(
                        "(v.Date < '{0}' OR (v.Date >= '{0}' AND v.OriginID = 2))",
                        parameters.FromDate.Value.ToShortDateString(false));
                }

                whereClause = whereClause.Replace(predicate, newPredicate);
            }
            else
            {
                string predicate = String.Format("v.No >= {0} AND v.No <= {1}",
                    parameters.FromNo, parameters.ToNo);
                if ((options & TestBalanceOptions.OpeningVoucherAsInitBalance) == 0)
                {
                    newPredicate = String.Format(
                        "(v.No < {0} AND v.OriginID <> 2)", parameters.FromNo.Value);
                }
                else
                {
                    newPredicate = String.Format(
                        "(v.No < {0} OR (v.No >= {0} AND v.OriginID = 2))",
                        parameters.FromNo.Value);
                }

                whereClause = whereClause.Replace(predicate, newPredicate);
            }

            return whereClause;
        }

        private async Task<string> GetCommonWhereClauseAsync(BalanceByAccountParameters parameters)
        {
            var whereBuilder = new StringBuilder("WHERE ");
            whereBuilder.Append(ReportQuery.TranslateQuery(
                _utility.GetEnvironmentFilters(parameters.GridOptions)));

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

            if (parameters.AccountId.HasValue)
            {
                var account = await _utility.GetItemAsync(ViewId.Account, parameters.AccountId.Value);
                whereBuilder.AppendFormat(" AND acc.FullCode LIKE '{0}%'", account.FullCode);
            }

            if (parameters.DetailAccountId.HasValue)
            {
                var detailAccount = await _utility.GetItemAsync(
                    ViewId.DetailAccount, parameters.DetailAccountId.Value);
                whereBuilder.AppendFormat(" AND facc.FullCode LIKE '{0}%'", detailAccount.FullCode);
            }

            if (parameters.CostCenterId.HasValue)
            {
                var costCenter = await _utility.GetItemAsync(
                    ViewId.CostCenter, parameters.CostCenterId.Value);
                whereBuilder.AppendFormat(" AND cc.FullCode LIKE '{0}%'", costCenter.FullCode);
            }

            if (parameters.ProjectId.HasValue)
            {
                var project = await _utility.GetItemAsync(
                    ViewId.Project, parameters.ProjectId.Value);
                whereBuilder.AppendFormat(" AND prj.FullCode LIKE '{0}%'", project.FullCode);
            }

            return whereBuilder.ToString();
        }

        private string GetGroupByClause(BalanceByAccountParameters parameters)
        {
            var groupByBuilder = new StringBuilder("GROUP BY ");
            var groupClauses = new List<string>();
            var orderClauses = new List<string>();
            if (parameters.IsSelectedAccount)
            {
                int length = _utility.GetLevelCodeLength(ViewId.Account, parameters.AccountLevel.Value);
                string clause = String.Format("SUBSTRING(acc.FullCode, 1, {0})", length);
                if (parameters.ViewId == ViewId.Account)
                {
                    groupClauses.Insert(0, clause);
                    orderClauses.Insert(0, clause);
                }
                else
                {
                    groupClauses.Add(clause);
                    orderClauses.Add(clause);
                }
            }

            if (parameters.IsSelectedDetailAccount)
            {
                int length = _utility.GetLevelCodeLength(ViewId.DetailAccount, parameters.DetailAccountLevel.Value);
                string clause = String.Format("SUBSTRING(facc.FullCode, 1, {0})", length);
                if (parameters.ViewId == ViewId.DetailAccount)
                {
                    groupClauses.Insert(0, clause);
                    orderClauses.Insert(0, clause);
                }
                else
                {
                    groupClauses.Add(clause);
                    orderClauses.Add(clause);
                }
            }

            if (parameters.IsSelectedCostCenter)
            {
                int length = _utility.GetLevelCodeLength(ViewId.CostCenter, parameters.CostCenterLevel.Value);
                string clause = String.Format("SUBSTRING(cc.FullCode, 1, {0})", length);
                if (parameters.ViewId == ViewId.CostCenter)
                {
                    groupClauses.Insert(0, clause);
                    orderClauses.Insert(0, clause);
                }
                else
                {
                    groupClauses.Add(clause);
                    orderClauses.Add(clause);
                }
            }

            if (parameters.IsSelectedProject)
            {
                int length = _utility.GetLevelCodeLength(ViewId.Project, parameters.ProjectLevel.Value);
                string clause = String.Format("SUBSTRING(prj.FullCode, 1, {0})", length);
                if (parameters.ViewId == ViewId.Project)
                {
                    groupClauses.Insert(0, clause);
                    orderClauses.Insert(0, clause);
                }
                else
                {
                    groupClauses.Add(clause);
                    orderClauses.Add(clause);
                }
            }

            if (parameters.IsByBranch)
            {
                groupClauses.Add("br.BranchID, br.Name");
                orderClauses.Add("br.BranchID");
            }

            groupByBuilder.AppendLine(String.Join(", ", groupClauses));
            groupByBuilder.Append("ORDER BY ");
            groupByBuilder.Append(String.Join(", ", orderClauses));
            return groupByBuilder.ToString();
        }

        private readonly ISystemRepository _system;
        private readonly IReportDirectUtility _utility;
    }
}
