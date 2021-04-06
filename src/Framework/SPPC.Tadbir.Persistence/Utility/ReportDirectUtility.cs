using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPPC.Framework.Common;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Model;
using SPPC.Tadbir.Model.Corporate;
using SPPC.Tadbir.Model.Finance;
using SPPC.Tadbir.ViewModel.Auth;
using SPPC.Tadbir.ViewModel.Finance;
using SPPC.Tadbir.ViewModel.Reporting;

namespace SPPC.Tadbir.Persistence.Utility
{
    /// <summary>
    ///
    /// </summary>
    public class ReportDirectUtility : IReportDirectUtility
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="context"></param>
        /// <param name="system"></param>
        public ReportDirectUtility(IRepositoryContext context, ISystemRepository system)
        {
            _context = context;
            _system = system;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="branchId"></param>
        /// <returns></returns>
        public IEnumerable<int> GetChildTree(int branchId)
        {
            var tree = new List<int>();
            var repository = UnitOfWork.GetRepository<Branch>();
            var branch = repository.GetByID(branchId, br => br.Children);
            AddChildren(branch, tree);
            return tree;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="branchId"></param>
        /// <returns></returns>
        public IEnumerable<int> GetParentTree(int branchId)
        {
            var tree = new List<int>();
            var repository = UnitOfWork.GetRepository<Branch>();
            var branch = repository.GetByIDWithTracking(branchId);
            var currentBranch = branch;
            while (currentBranch != null)
            {
                tree.Add(currentBranch.Id);
                repository.LoadReference(currentBranch, br => br.Parent);
                currentBranch = currentBranch.Parent;
            }

            return tree;
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
            string fieldName = String.Empty;
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
        /// <param name="level"></param>
        /// <returns></returns>
        public int GetLevelCodeLength(int level)
        {
            return GetLevelCodeLength(ViewId.Account, level);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="viewId"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        public int GetLevelCodeLength(int viewId, int level)
        {
            var fullConfig = Config
                .GetViewTreeConfigByViewAsync(viewId)
                .Result;
            var treeConfig = fullConfig.Current;
            int codeLength = treeConfig.Levels
                .Where(cfg => cfg.No <= level + 1)
                .Select(cfg => (int)cfg.CodeLength)
                .Sum();
            return codeLength;
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
                predicates.Add(String.Format("BranchId = {0}", branchId));
            }
            else if (quickFilter == null || quickFilter.IndexOf("BranchId") == -1)
            {
                var branchIds = GetChildTree(UserContext.BranchId);
                string branchList = String.Join(",", branchIds.Select(id => id.ToString()));
                if (!String.IsNullOrEmpty(branchList))
                {
                    predicates.Add(String.Format(
                        "(BranchId = {0} OR BranchId IN({1}))", UserContext.BranchId, branchList));
                }
                else
                {
                    predicates.Add(String.Format("BranchId = {0}", UserContext.BranchId));
                }
            }

            predicates.Add(String.Format("v.FiscalPeriodId = {0}", fpId));
            if (noDraft)
            {
                predicates.Add(String.Format("v.SubjectType <> {0}", (int)SubjectType.Draft));
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
        /// به روش آسنکرون، فهرست سطوح قابل استفاده برای گزارشگیری را
        /// برای مولفه حساب داده شده خوانده و برمی گرداند
        /// </summary>
        /// <param name="viewId">شناسه مولفه حساب</param>
        /// <returns>فهرست سطوح قابل استفاده</returns>
        public async Task<IEnumerable<TestBalanceModeInfo>> GetLevelBalanceTypesAsync(int viewId)
        {
            var lookup = new List<TestBalanceModeInfo>();
            var fullConfig = await Config.GetViewTreeConfigByViewAsync(viewId);
            var usedLevels = fullConfig.Current
                .Levels
                .Where(level => level.IsEnabled && level.IsUsed)
                .ToList();
            int typeId = 0;
            for (int index = 0; index < usedLevels.Count; index++)
            {
                lookup.Add(new TestBalanceModeInfo()
                {
                    Id = typeId++,
                    Name = usedLevels[index].Name,
                    Level = usedLevels[index].No,
                    IsDetail = false
                });
            }

            return lookup;
        }

        /// <summary>
        /// به روش آسنکرون، فهرست سطوح زیرمجموعه قابل انتخاب برای گزارشگیری را خوانده و برمی گرداند
        /// </summary>
        /// <returns>فهرست سطوح زیرمجموعه قابل انتخاب</returns>
        public async Task<IEnumerable<TestBalanceModeInfo>> GetChildBalanceTypesAsync()
        {
            var lookup = new List<TestBalanceModeInfo>();
            var fullConfig = await Config.GetViewTreeConfigByViewAsync(ViewId.Account);
            var usedLevels = fullConfig.Current
                .Levels
                .Where(level => level.IsEnabled && level.IsUsed)
                .ToList();
            int typeId = 0;
            lookup.Add(new TestBalanceModeInfo()
            {
                Id = typeId++,
                Name = "SubsidiariesOfLedger",
                Level = 2,
                IsDetail = true
            });
            lookup.Add(new TestBalanceModeInfo()
            {
                Id = typeId++,
                Name = "DetailsOfSubsidiary",
                Level = 3,
                IsDetail = true
            });
            for (int index = 2; index < usedLevels.Count - 1; index++)
            {
                lookup.Add(new TestBalanceModeInfo()
                {
                    Id = typeId++,
                    Name = usedLevels[index].Name,
                    Level = usedLevels[index].No + 1,
                    IsDetail = true
                });
            }

            return lookup;
        }

        /// <summary>
        /// به روش آسنکرون، فهرست سطوح زیرمجموعه قابل انتخاب برای گزارشگیری را خوانده و برمی گرداند
        /// </summary>
        /// <param name="viewId">شناسه مولفه حساب</param>
        /// <returns>فهرست سطوح زیرمجموعه قابل انتخاب</returns>
        public async Task<IEnumerable<TestBalanceModeInfo>> GetChildBalanceTypesAsync(int viewId)
        {
            if (viewId == ViewId.Account)
            {
                return await GetChildBalanceTypesAsync();
            }

            var lookup = new List<TestBalanceModeInfo>();
            var fullConfig = await Config.GetViewTreeConfigByViewAsync(viewId);
            var usedLevels = fullConfig.Current
                .Levels
                .Where(level => level.IsEnabled && level.IsUsed)
                .ToList();
            int typeId = usedLevels.Count;
            for (int index = 0; index < usedLevels.Count - 1; index++)
            {
                lookup.Add(new TestBalanceModeInfo()
                {
                    Id = typeId++,
                    Name = usedLevels[index].Name,
                    Level = usedLevels[index].No + 1,
                    IsDetail = true
                });
            }

            return lookup;
        }

        /// <summary>
        /// سطرهای بدون مانده و گردش را با توجه به سطرهای داده شده برای گزارش خوانده و برمی گرداند
        /// </summary>
        /// <param name="viewId">شناسه مولفه حساب مورد نظر</param>
        /// <param name="items">سطرهای داده شده برای گزارش</param>
        /// <param name="level">سطح گزارشگیری مورد نظر</param>
        /// <returns>سطرهای بدون مانده و گردش</returns>
        public async Task<IEnumerable<TestBalanceItemViewModel>> GetZeroBalanceItemsAsync(
            int viewId, IEnumerable<TestBalanceItemViewModel> items, int level)
        {
            var zeroItems = new List<TestBalanceItemViewModel>();
            var notUsed = await GetNotUsedItemsAsync(viewId, items, level);
            foreach (var notUsedItem in notUsed)
            {
                zeroItems.Add(new TestBalanceItemViewModel()
                {
                    AccountFullCode = notUsedItem.FullCode,
                    DetailAccountFullCode = notUsedItem.FullCode,
                    CostCenterFullCode = notUsedItem.FullCode,
                    ProjectFullCode = notUsedItem.FullCode,
                    BranchName = notUsedItem.Branch.Name
                });
            }

            return zeroItems;
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
                    length, componentName, _context.UserContext.FiscalPeriodId);
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
            _context.DbConsole.ConnectionString = UnitOfWork.CompanyConnection;
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

            var result = _context.DbConsole.ExecuteQuery(cmdBuilder.ToString());
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
            _context.DbConsole.ConnectionString = UnitOfWork.CompanyConnection;
            var accounts = new List<AccountItemBriefViewModel>();
            var parentTree = GetParentTree(branchId);
            foreach (int parentId in parentTree)
            {
                var query = new ReportQuery(String.Format(
                    AccountItemQuery.CollectionAccounts, _context.UserContext.FiscalPeriodId,
                    parentId, (int)collectionId));
                var result = _context.DbConsole.ExecuteQuery(query.Query);
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
        /// <param name="query"></param>
        /// <returns></returns>
        public string TranslateQuery(string query)
        {
            Verify.ArgumentNotNull(query, nameof(query));
            return query
                .Replace("Voucher", "v.")
                .Replace("Date", "v.Date")
                .Replace("== null", " IS NULL")
                .Replace("!= null", " IS NOT NULL")
                .Replace("\"", "'")
                .Replace("&&", "AND")
                .Replace("||", "OR")
                .Replace("==", "=")
                .Replace("!=", "<>")
                .Replace("BranchId", "v.BranchID");
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
                .Replace("Description", "vl.Description")
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

        private IAppUnitOfWork UnitOfWork
        {
            get { return _context.UnitOfWork; }
        }

        private IConfigRepository Config
        {
            get { return _system.Config; }
        }

        private UserContextViewModel UserContext
        {
            get { return _context.UserContext; }
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

        private void AddChildren(Branch branch, IList<int> children)
        {
            var repository = UnitOfWork.GetRepository<Branch>();
            foreach (var child in branch.Children)
            {
                children.Add(child.Id);
                var item = repository.GetByID(child.Id, br => br.Children);
                AddChildren(item, children);
            }
        }

        private async Task<IEnumerable<TreeEntity>> GetNotUsedItemsAsync(
            int viewId, IEnumerable<TestBalanceItemViewModel> items, int level)
        {
            IEnumerable<TreeEntity> notUsed = null;
            switch (viewId)
            {
                case ViewId.Account:
                    notUsed = await GetNotUsedItemsAsync<Account>(viewId, items, level);
                    break;
                case ViewId.DetailAccount:
                    notUsed = await GetNotUsedItemsAsync<DetailAccount>(viewId, items, level);
                    break;
                case ViewId.CostCenter:
                    notUsed = await GetNotUsedItemsAsync<CostCenter>(viewId, items, level);
                    break;
                case ViewId.Project:
                    notUsed = await GetNotUsedItemsAsync<Project>(viewId, items, level);
                    break;
            }

            return notUsed;
        }

        private async Task<IEnumerable<T>> GetNotUsedItemsAsync<T>(
            int viewId, IEnumerable<TestBalanceItemViewModel> items, int level)
            where T : TreeEntity
        {
            var repository = _system.Repository;
            var usedCodes = items
                .Select(item => item.AccountFullCode);
            return await repository
                .GetAllQuery<T>(viewId, tree => tree.Branch)
                .Where(tree => !usedCodes.Contains(tree.FullCode) && tree.Level == level)
                .ToListAsync();
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
        private readonly IRepositoryContext _context;
        private readonly ISystemRepository _system;
    }
}
