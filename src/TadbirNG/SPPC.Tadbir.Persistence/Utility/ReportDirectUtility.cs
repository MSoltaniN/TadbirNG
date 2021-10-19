using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Model;
using SPPC.Tadbir.Model.Finance;
using SPPC.Tadbir.ViewModel.Finance;

namespace SPPC.Tadbir.Persistence.Utility
{
    /// <summary>
    ///
    /// </summary>
    public class ReportDirectUtility : RepositoryBase, IReportDirectUtility
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="context"></param>
        public ReportDirectUtility(IRepositoryContext context)
            : base(context)
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="viewId"></param>
        /// <returns></returns>
        public string GetItemName(int viewId)
        {
            string itemName = String.Empty;
            switch (viewId)
            {
                case ViewId.Account:
                    itemName = typeof(Account).Name;
                    break;
                case ViewId.DetailAccount:
                    itemName = typeof(DetailAccount).Name;
                    break;
                case ViewId.CostCenter:
                    itemName = typeof(CostCenter).Name;
                    break;
                case ViewId.Project:
                    itemName = typeof(Project).Name;
                    break;
            }

            return itemName;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="viewId"></param>
        /// <returns></returns>
        public string GetFieldName(int viewId)
        {
            string fieldName;
            if (viewId == ViewId.DetailAccount)
            {
                fieldName = "Detail";
            }
            else
            {
                fieldName = GetItemName(viewId);
            }

            return fieldName;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="gridOptions"></param>
        /// <param name="fiscalPeriodId"></param>
        /// <param name="branchId"></param>
        /// <param name="noDraft"></param>
        /// <returns></returns>
        public string GetEnvironmentFilters(GridOptions gridOptions = null, int? fiscalPeriodId = null,
            int? branchId = null, bool noDraft = true)
        {
            var predicates = new List<string>();
            int fpId = fiscalPeriodId ?? UserContext.FiscalPeriodId;
            var quickFilter = gridOptions?.QuickFilter?.ToString();
            if (branchId != null)
            {
                predicates.Add(String.Format("BranchID = {0}", branchId));
            }
            else if (quickFilter == null || quickFilter.IndexOf("BranchId") == -1)
            {
                var branchIds = GetChildTree(UserContext.BranchId);
                string branchList = String.Join(",", branchIds.Select(id => id.ToString()));
                if (!String.IsNullOrEmpty(branchList))
                {
                    predicates.Add(String.Format(
                        "(BranchID = {0} OR BranchID IN({1}))", UserContext.BranchId, branchList));
                }
                else
                {
                    predicates.Add(String.Format("BranchID = {0}", UserContext.BranchId));
                }
            }

            predicates.Add(String.Format("v.FiscalPeriodID = {0}", fpId));
            if (noDraft)
            {
                predicates.Add(String.Format("SubjectType <> {0}", (int)SubjectType.Draft));
            }

            if (!String.IsNullOrEmpty(quickFilter))
            {
                predicates.Add(quickFilter);
            }

            return String.Join(" AND ", predicates);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="gridOptions"></param>
        /// <returns></returns>
        public string GetColumnFilters(GridOptions gridOptions)
        {
            string columnFilters = gridOptions.Filter?.ToString();
            if (!String.IsNullOrEmpty(columnFilters))
            {
                columnFilters = TranslateFieldNames(columnFilters);
                columnFilters = TranslateOperators(columnFilters);
            }

            return columnFilters;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="gridOptions"></param>
        /// <returns></returns>
        public string GetColumnSorting(GridOptions gridOptions)
        {
            string columnSorting = String.Empty;
            if (gridOptions.SortColumns.Count > 0)
            {
                columnSorting = String.Join(", ", gridOptions.SortColumns.Select(item => item.ToString()));
                columnSorting = TranslateFieldNames(columnSorting);
            }

            return columnSorting;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="row"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        public string ValueOrDefault(DataRow row, string field)
        {
            string value = null;
            if (row.Table.Columns.Contains(field))
            {
                value = row[field].ToString();
            }

            return value;
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="row"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        public T ValueOrDefault<T>(DataRow row, string field)
        {
            var value = default(T);
            if (row.Table.Columns.Contains(field) && row[field] != DBNull.Value)
            {
                value = (T)Convert.ChangeType(row[field], typeof(T));
            }

            return value;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="viewId"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public ReportQuery GetItemLookupQuery(int viewId, int length)
        {
            var query = default(ReportQuery);
            string componentName = String.Empty;
            switch (viewId)
            {
                case ViewId.Account:
                    componentName = typeof(Account).Name;
                    break;
                case ViewId.DetailAccount:
                    componentName = typeof(DetailAccount).Name;
                    break;
                case ViewId.CostCenter:
                    componentName = typeof(CostCenter).Name;
                    break;
                case ViewId.Project:
                    componentName = typeof(Project).Name;
                    break;
                default:
                    break;
            }

            if (!String.IsNullOrEmpty(componentName))
            {
                string command = String.Format(AccountItemQuery.ItemLookup,
                    length, componentName, UserContext.FiscalPeriodId);
                query = new ReportQuery(command);
            }

            return query;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="viewId"></param>
        /// <param name="itemId"></param>
        /// <returns></returns>
        public async Task<TreeEntity> GetItemAsync(int viewId, int itemId)
        {
            var accountItem = default(TreeEntity);
            switch (viewId)
            {
                case ViewId.Account:
                    accountItem = await GetItemAsync<Account>(itemId);
                    break;
                case ViewId.DetailAccount:
                    accountItem = await GetItemAsync<DetailAccount>(itemId);
                    break;
                case ViewId.CostCenter:
                    accountItem = await GetItemAsync<CostCenter>(itemId);
                    break;
                case ViewId.Project:
                    accountItem = await GetItemAsync<Project>(itemId);
                    break;
            }

            return accountItem;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="fpId"></param>
        /// <returns></returns>
        public async Task<DateTime> GetFiscalPeriodStartAsync(int fpId)
        {
            var repository = UnitOfWork.GetAsyncRepository<FiscalPeriod>();
            return await repository
                .GetEntityQuery()
                .Where(fp => fp.Id == fpId)
                .Select(fp => fp.StartDate)
                .SingleOrDefaultAsync();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="fpId"></param>
        /// <returns></returns>
        public async Task<DateTime> GetFiscalPeriodEndAsync(int fpId)
        {
            var repository = UnitOfWork.GetAsyncRepository<FiscalPeriod>();
            return await repository
                .GetEntityQuery()
                .Where(fp => fp.Id == fpId)
                .Select(fp => fp.EndDate)
                .SingleOrDefaultAsync();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="fpId"></param>
        /// <returns></returns>
        public async Task<int> GetFirstVoucherNoAsync(int fpId)
        {
            var repository = UnitOfWork.GetAsyncRepository<Voucher>();
            return await repository
                .GetEntityQuery()
                .Where(v => v.FiscalPeriodId == fpId)
                .OrderBy(v => v.No)
                .Select(v => v.No)
                .FirstOrDefaultAsync();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="fiscalPeriodId"></param>
        /// <param name="originId"></param>
        /// <returns></returns>
        public async Task<bool> HasSpecialVoucherAsync(int fiscalPeriodId, VoucherOriginId originId)
        {
            var repository = UnitOfWork.GetAsyncRepository<Voucher>();
            int specialVoucherId = await repository
                .GetEntityQuery()
                .Where(v => v.FiscalPeriodId == fiscalPeriodId
                    && v.OriginId == (int)originId)
                .Select(v => v.Id)
                .FirstOrDefaultAsync();
            return specialVoucherId > 0;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="collectionId"></param>
        /// <param name="withRelations"></param>
        /// <param name="branchId"></param>
        /// <returns></returns>
        public IEnumerable<AccountItemBriefViewModel> GetUsableAccounts(
            AccountCollectionId collectionId, bool withRelations = false, int? branchId = null)
        {
            DbConsole.ConnectionString = UnitOfWork.CompanyConnection;
            int inBranchId = branchId ?? UserContext.BranchId;
            var accounts = GetInheritedAccounts(collectionId, inBranchId);
            if (accounts.Count() == 0)
            {
                return new List<AccountItemBriefViewModel>();
            }

            var tree = GetChildTree(inBranchId)
                .ToList();
            tree.Insert(0, UserContext.BranchId);
            string branchList = String.Join(",", tree);
            var cmdBuilder = new StringBuilder(String.Format(
                AccountItemQuery.EnvironmentAccounts, UserContext.FiscalPeriodId,
                branchList, UserContext.BranchId));
            cmdBuilder.AppendFormat(" AND {0}", AccountItemQuery.LeafAccountFilter);
            string filter = String.Join(" OR ", accounts
                .Select(acc => String.Format("acc.FullCode LIKE '{0}%'", acc.FullCode)));
            if (!String.IsNullOrEmpty(filter))
            {
                cmdBuilder.AppendFormat(" AND ({0})", filter);
            }

            var result = DbConsole.ExecuteQuery(cmdBuilder.ToString());
            return result.Rows
                .Cast<DataRow>()
                .Select(row => GetAccountInfo(row));
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="collectionId"></param>
        /// <param name="branchId"></param>
        /// <returns></returns>
        public IEnumerable<AccountItemBriefViewModel> GetInheritedAccounts(
            AccountCollectionId collectionId, int branchId)
        {
            DbConsole.ConnectionString = UnitOfWork.CompanyConnection;
            var accounts = new List<AccountItemBriefViewModel>();
            var parentTree = GetParentTree(branchId);
            foreach (int parentId in parentTree)
            {
                var query = new ReportQuery(String.Format(
                    AccountItemQuery.CollectionAccounts, UserContext.FiscalPeriodId,
                    parentId, (int)collectionId));
                var result = DbConsole.ExecuteQuery(query.Query);
                if (result.Rows.Count > 0)
                {
                    accounts.AddRange(result.Rows
                        .Cast<DataRow>()
                        .Select(row => GetAccountInfo(row)));
                }
            }

            return accounts;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public async Task<Voucher> GetOpeningVoucherAsync()
        {
            var repository = UnitOfWork.GetAsyncRepository<Voucher>();
            var openingVoucher = await repository
                .GetFirstByCriteriaAsync(v => v.OriginId == (int)VoucherOriginId.OpeningVoucher
                    && v.FiscalPeriodId == UserContext.FiscalPeriodId);
            return openingVoucher;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="options"></param>
        /// <param name="openingVoucher"></param>
        /// <returns></returns>
        public bool MustApplyOpeningOption(FinanceReportOptions options, Voucher openingVoucher)
        {
            bool mustApply = openingVoucher != null
                && (options & FinanceReportOptions.OpeningAsFirstVoucher) > 0;
            return mustApply;
        }

        private static string TranslateFieldNames(string query)
        {
            var builder = new StringBuilder(query);
            return builder
                .Replace("Voucher", "v.")
                .Replace("DetailAccount", "facc.")
                .Replace("Account", "acc.")
                .Replace("CostCenter", "cc.")
                .Replace("Project", "prj.")
                .Replace("Debit", "vl.Debit")
                .Replace("Credit", "vl.Credit")
                .Replace("Mark", "vl.Mark")
                .Replace("BranchName", "br.Name")
                .ToString();
        }

        private static string TranslateOperators(string query)
        {
            var builder = new StringBuilder(query)
                .Replace("== null", " IS NULL")
                .Replace("!= null", " IS NOT NULL")
                .Replace("\"", "'")
                .Replace("&&", "AND")
                .Replace("||", "OR")
                .Replace("==", "=")
                .Replace("!=", "<>");

            string result = builder.ToString();
            var regEx = new Regex(StartsWithRegex);
            foreach (Match match in regEx.Matches(result))
            {
                builder.Replace(match.ToString(), String.Format(" LIKE N'{0}%'", match.Groups[1]));
            }

            regEx = new Regex(EndsWithRegex);
            foreach (Match match in regEx.Matches(result))
            {
                builder.Replace(match.ToString(), String.Format(" LIKE N'%{0}'", match.Groups[1]));
            }

            regEx = new Regex(ContainsRegex);
            foreach (Match match in regEx.Matches(result))
            {
                builder.Replace(match.ToString(), String.Format(" LIKE N'%{0}%'", match.Groups[1]));
            }

            regEx = new Regex(NotContainsRegex);
            foreach (Match match in regEx.Matches(result))
            {
                builder.Replace(match.ToString(), String.Format(" NOT LIKE N'%{0}%'", match.Groups[1]));
            }

            return builder.ToString();
        }

        private AccountItemBriefViewModel GetAccountInfo(DataRow row)
        {
            return new AccountItemBriefViewModel()
            {
                Id = ValueOrDefault<int>(row, "Id"),
                Name = ValueOrDefault(row, "Name"),
                FullCode = ValueOrDefault(row, "FullCode")
            };
        }

        private async Task<T> GetItemAsync<T>(int itemId)
            where T : TreeEntity
        {
            var repository = UnitOfWork.GetAsyncRepository<T>();
            return await repository.GetByIDAsync(itemId);
        }

        private const string StartsWithRegex = @"\.StartsWith\('(\w{1,})'\)";
        private const string EndsWithRegex = @"\.EndsWith\('(\w{1,})'\)";
        private const string ContainsRegex = @"\.Contains\('(\w{1,})'\)";
        private const string NotContainsRegex = @"\.IndexOf\('(\w{1,})'\) = -1";
    }
}
