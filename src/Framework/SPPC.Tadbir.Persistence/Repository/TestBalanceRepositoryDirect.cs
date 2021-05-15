using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPPC.Framework.Extensions;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Model.Finance;
using SPPC.Tadbir.Persistence.Utility;
using SPPC.Tadbir.ViewModel.Finance;
using SPPC.Tadbir.ViewModel.Reporting;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    ///
    /// </summary>
    public class TestBalanceRepositoryDirect : LoggingRepositoryBase, ITestBalanceRepository
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="context">امکانات مشترک مورد نیاز را برای عملیات دیتابیسی فراهم می کند</param>
        /// <param name="system">امکانات مورد نیاز در دیتابیس های سیستمی را فراهم می کند</param>
        /// <param name="utility"></param>
        public TestBalanceRepositoryDirect(IRepositoryContext context, ISystemRepository system,
            IReportDirectUtility utility)
            : base(context, system.Logger)
        {
            _utility = utility;
        }

        internal override OperationSourceId OperationSource
        {
            get { return OperationSourceId.TestBalance; }
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات گزارش تراز آزمایشی برای یکی از سطوح حساب را خوانده و برمی گرداند
        /// </summary>
        /// <param name="level">شماره یکی از سطوح حساب</param>
        /// <param name="parameters">پارامترهای مورد نیاز برای گزارش</param>
        /// <returns>اطلاعات گزارش با توجه به پارامترهای داده شده</returns>
        public async Task<TestBalanceViewModel> GetLevelBalanceAsync(
            int level, TestBalanceParameters parameters)
        {
            var balance = new TestBalanceViewModel();
            var items = new List<TestBalanceItemViewModel>();
            int length;
            string filter;
            int index = 0;
            ReportQuery query;
            while (index < level)
            {
                length = _utility.GetLevelCodeLength(parameters.ViewId, index);
                filter = String.Format("acc.Level == {0}", index);
                query = await GetEndBalanceQueryAync(length, parameters, filter);
                items.AddRange(GetQueryResult(query));
                index++;
            }

            length = _utility.GetLevelCodeLength(parameters.ViewId, level);
            filter = String.Format("acc.Level >= {0}", level);
            query = await GetEndBalanceQueryAync(length, parameters, filter);
            items.AddRange(GetQueryResult(query));

            if (parameters.Format >= TestBalanceFormat.FourColumn)
            {
                await AddTurnoversAsync(length, items, parameters);
            }

            if (parameters.Format >= TestBalanceFormat.SixColumn)
            {
                await AddInitialBalancesAsync(length, items, parameters);
            }

            if (parameters.Format >= TestBalanceFormat.EightColumn)
            {
                AddOperationSums(items);
            }

            items = await ApplyZeroBalanceOptionAsync(items, parameters, level);
            PrepareBalance(balance, items, parameters, length);
            var source = (parameters.ViewId == ViewId.Account)
                ? OperationSourceId.TestBalance
                : OperationSourceId.ItemBalance;
            await OnSourceActionAsync(parameters.GridOptions, source,
                (SourceListId)GetSourceList(parameters.Format, GetComponentName(parameters.ViewId)));
            return balance;
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات گزارش تراز آزمایشی زیرمجموعه های یک حساب را خوانده و برمی گرداند
        /// </summary>
        /// <param name="accountId">شناسه دیتابیسی یکی از حساب های دارای زیرمجموعه</param>
        /// <param name="parameters">پارامترهای مورد نیاز برای گزارش</param>
        /// <returns>اطلاعات گزارش با توجه به پارامترهای داده شده</returns>
        public async Task<TestBalanceViewModel> GetChildrenBalanceAsync(
            int accountId, TestBalanceParameters parameters)
        {
            var balance = new TestBalanceViewModel();
            var accountItem = await _utility.GetItemAsync(parameters.ViewId, accountId);
            if (accountItem != null)
            {
                var items = new List<TestBalanceItemViewModel>();
                int level = accountItem.Level + 1;
                var filter = String.Format("acc.Level >= {0} AND acc.FullCode LIKE '{1}%'", level, accountItem.FullCode);
                int length = _utility.GetLevelCodeLength(parameters.ViewId, level);
                var query = await GetEndBalanceQueryAync(length, parameters, filter);
                items.AddRange(GetQueryResult(query));

                if (parameters.Format >= TestBalanceFormat.FourColumn)
                {
                    await AddTurnoversAsync(length, items, parameters, filter);
                }

                if (parameters.Format >= TestBalanceFormat.SixColumn)
                {
                    await AddInitialBalancesAsync(length, items, parameters, filter);
                }

                if (parameters.Format >= TestBalanceFormat.EightColumn)
                {
                    AddOperationSums(items);
                }

                items = await ApplyZeroBalanceOptionAsync(items, parameters, level);
                PrepareBalance(balance, items, parameters, length);
            }

            var source = (parameters.ViewId == ViewId.Account)
                ? OperationSourceId.TestBalance
                : OperationSourceId.ItemBalance;
            await OnSourceActionAsync(parameters.GridOptions, source,
                (SourceListId)GetSourceList(parameters.Format, GetComponentName(parameters.ViewId)));
            return balance;
        }

        /// <summary>
        /// به روش آسنکرون، انواع مختلف تراز آزمایشی را با توجه به ساختار درختی سرفصل های حسابداری خوانده و برمی گرداند
        /// </summary>
        /// <returns>انواع مختلف تراز آزمایشی</returns>
        public async Task<IEnumerable<TestBalanceModeInfo>> GetBalanceTypesLookupAsync(int viewId)
        {
            var lookup = new List<TestBalanceModeInfo>();
            lookup.AddRange(await _utility.GetLevelBalanceTypesAsync(viewId));
            lookup.AddRange(await _utility.GetChildBalanceTypesAsync(viewId));
            return lookup;
        }

        /// <summary>
        /// کلاس پارامتر مورد نیاز برای گزارش های گردش و مانده حساب و سطوح شناور را
        /// با استفاده از مقادیر داده شده ساخته و برمی گرداند
        /// </summary>
        /// <param name="viewId">شناسه دیتابیسی مولفه حساب مورد نظر</param>
        /// <param name="from">تاریخ یا شماره سند ابتدا در محدوده گزارشگیری</param>
        /// <param name="to">تاریخ یا شماره سند انتها در محدوده گزارشگیری</param>
        /// <param name="mode">سطح مورد نظر برای گزارشگیری</param>
        /// <param name="format">قالب مورد نظر</param>
        /// <param name="gridOptions">گزینه های صفحه بندی، فیلتر و مرتب سازی برای نمای لیستی</param>
        /// <param name="byBranch">مشخص می کند که گزارش به تفکیک شعبه است یا نه</param>
        /// <param name="options">سایر گزینه های مورد نظر برای گزارشگیری</param>
        /// <returns>کلاس پارامتر ساخته شده</returns>
        public TestBalanceParameters BuildParameters(
            int viewId, string from, string to, TestBalanceMode mode, TestBalanceFormat format,
            GridOptions gridOptions, bool? byBranch, int? options)
        {
            var parameters = new TestBalanceParameters()
            {
                ViewId = viewId,
                Mode = mode,
                Format = format,
                GridOptions = gridOptions
            };
            var culture = new CultureInfo("en");
            if (DateTime.TryParse(from, culture, DateTimeStyles.None, out DateTime fromDate))
            {
                parameters.FromDate = fromDate;
            }

            if (DateTime.TryParse(to, culture, DateTimeStyles.None, out DateTime toDate))
            {
                parameters.ToDate = toDate;
            }

            if (Int32.TryParse(from, out int fromNo))
            {
                parameters.FromNo = fromNo;
            }

            if (Int32.TryParse(to, out int toNo))
            {
                parameters.ToNo = toNo;
            }

            if (byBranch.HasValue)
            {
                parameters.IsByBranch = byBranch.Value;
            }

            if (options.HasValue)
            {
                parameters.Options = (FinanceReportOptions)options.Value;
            }

            return parameters;
        }

        private static int GetSourceList(TestBalanceFormat format, string itemTypeName)
        {
            string enumStringTemplate = "{0}Balance{1}Column";
            string itemType = (itemTypeName == "Account")
                ? "Test"
                : itemTypeName;
            string formatString = format.ToString();
            string enumValue;
            if (formatString.StartsWith("Two"))
            {
                enumValue = String.Format(enumStringTemplate, itemType, 2);
            }
            else if (formatString.StartsWith("Four"))
            {
                enumValue = String.Format(enumStringTemplate, itemType, 4);
            }
            else if (formatString.StartsWith("Six"))
            {
                enumValue = String.Format(enumStringTemplate, itemType, 6);
            }
            else if (formatString.StartsWith("Eight"))
            {
                enumValue = String.Format(enumStringTemplate, itemType, 8);
            }
            else
            {
                enumValue = String.Format(enumStringTemplate, itemType, 10);
            }

            return (int)Enum.Parse(typeof(SourceListId), enumValue);
        }

        private static void SetSummaryItems(
            TestBalanceViewModel balance, IEnumerable<TestBalanceItemViewModel> items)
        {
            balance.TotalCount = items.Count();
            balance.Total.StartBalanceDebit = items.Sum(item => item.StartBalanceDebit);
            balance.Total.StartBalanceCredit = items.Sum(item => item.StartBalanceCredit);
            balance.Total.TurnoverDebit = items.Sum(item => item.TurnoverDebit);
            balance.Total.TurnoverCredit = items.Sum(item => item.TurnoverCredit);
            balance.Total.OperationSumDebit = items.Sum(item => item.OperationSumDebit);
            balance.Total.OperationSumCredit = items.Sum(item => item.OperationSumCredit);
            balance.Total.CorrectionsDebit = items.Sum(item => item.CorrectionsDebit);
            balance.Total.CorrectionsCredit = items.Sum(item => item.CorrectionsCredit);
            balance.Total.EndBalanceDebit = items.Sum(item => item.EndBalanceDebit);
            balance.Total.EndBalanceCredit = items.Sum(item => item.EndBalanceCredit);
        }

        private static void AddOperationSums(IEnumerable<TestBalanceItemViewModel> items)
        {
            foreach (var item in items)
            {
                item.OperationSumDebit = item.StartBalanceDebit + item.TurnoverDebit;
                item.OperationSumCredit = item.StartBalanceCredit + item.TurnoverCredit;
            }
        }

        private static bool IsZeroBalanceItem(TestBalanceItemViewModel item)
        {
            return item.StartBalanceDebit == 0.0M
                && item.StartBalanceCredit == 0.0M
                && item.TurnoverDebit == 0.0M
                && item.TurnoverCredit == 0.0M
                && item.OperationSumDebit == 0.0M
                && item.OperationSumCredit == 0.0M
                && item.EndBalanceDebit == 0.0M
                && item.EndBalanceCredit == 0.0M;
        }

        private static string GetComponentName(int viewId)
        {
            string componentName = String.Empty;
            switch (viewId)
            {
                case ViewId.Account:
                    componentName = "Account";
                    break;
                case ViewId.DetailAccount:
                    componentName = "DetailAccount";
                    break;
                case ViewId.CostCenter:
                    componentName = "CostCenter";
                    break;
                case ViewId.Project:
                    componentName = "Project";
                    break;
                default:
                    break;
            }

            return componentName;
        }

        private static bool HasColumnFilterOrSort(GridOptions gridOptions)
        {
            return gridOptions.Filter != null
                || gridOptions.SortColumns.Count > 0;
        }

        private static void MergeByCode(
            List<TestBalanceItemViewModel> items, Dictionary<string, VoucherLineAmountsViewModel> initMap,
            MergeByCodeFunction mergeFunction)
        {
            var allCodes = initMap.Keys
                .Cast<string>()
                .Concat(items.Select(item => item.AccountFullCode))
                .Distinct();

            foreach (var code in allCodes)
            {
                var item = items.Where(it => it.AccountFullCode == code).FirstOrDefault();
                var updatedItem = mergeFunction(item, initMap, code);
                if (!Object.ReferenceEquals(item, updatedItem))
                {
                    items.Add(updatedItem);
                }
            }
        }

        private static void MergeByCodeBranch(
            List<TestBalanceItemViewModel> items, Dictionary<FullCodeBranch, VoucherLineAmountsViewModel> initMap,
            MergeByCodeBranchFunction mergeFunction)
        {
            var allCodeBranches = initMap.Keys
                .Cast<FullCodeBranch>()
                .Concat(items.Select(item =>
                    new FullCodeBranch()
                    {
                        FullCode = item.AccountFullCode,
                        BranchName = item.BranchName
                    }))
                .Distinct();

            foreach (var codeBranch in allCodeBranches)
            {
                var item = items
                    .Where(it => it.AccountFullCode == codeBranch.FullCode
                        && it.BranchName == codeBranch.BranchName).FirstOrDefault();
                var updatedItem = mergeFunction(item, initMap, codeBranch);
                if (!Object.ReferenceEquals(item, updatedItem))
                {
                    items.Add(updatedItem);
                }
            }
        }

        private async Task<List<TestBalanceItemViewModel>> ApplyZeroBalanceOptionAsync(
            IEnumerable<TestBalanceItemViewModel> items, TestBalanceParameters parameters, int level)
        {
            IEnumerable<TestBalanceItemViewModel> newItems = null;
            if ((parameters.Options & FinanceReportOptions.ShowZeroBalanceItems) > 0)
            {
                newItems = items.Concat(
                    await _utility.GetZeroBalanceItemsAsync(parameters.ViewId, items, level));
            }
            else
            {
                // Remove items with zero end balance...
                newItems = items
                    .Where(item => !IsZeroBalanceItem(item));
            }

            return newItems
                .OrderBy(item => item.AccountFullCode)
                .ToList();
        }

        private void PrepareBalance(
            TestBalanceViewModel balance, IEnumerable<TestBalanceItemViewModel> items,
            TestBalanceParameters parameters, int length)
        {
            if (HasColumnFilterOrSort(parameters.GridOptions))
            {
                SetItemNames(parameters.ViewId, length, items);
                var filtered = items.Apply(parameters.GridOptions, false);
                SetSummaryItems(balance, filtered);
                balance.Items.AddRange(filtered.ApplyPaging(parameters.GridOptions));
            }
            else
            {
                SetSummaryItems(balance, items);
                balance.Items.AddRange(items.ApplyPaging(parameters.GridOptions));
                SetItemNames(parameters.ViewId, length, balance.Items);
            }
        }

        private async Task AddTurnoversAsync(
            int length, List<TestBalanceItemViewModel> items, TestBalanceParameters parameters,
            string filter = null)
        {
            DbConsole.ConnectionString = UnitOfWork.CompanyConnection;
            if (parameters.IsByBranch)
            {
                await AddTurnoversByBranchAsync(length, items, parameters);
                return;
            }

            var initMap = new Dictionary<string, VoucherLineAmountsViewModel>();
            var query = await GetTurnoverQueryAsync(length, parameters, filter);
            var result = DbConsole.ExecuteQuery(query.Query);
            foreach (DataRow row in result.Rows)
            {
                var amounts = new VoucherLineAmountsViewModel()
                {
                    Debit = _utility.ValueOrDefault<decimal>(row, "DebitSum"),
                    Credit = _utility.ValueOrDefault<decimal>(row, "CreditSum")
                };
                initMap.Add(_utility.ValueOrDefault(row, "FullCode"), amounts);
            }

            MergeByCode(items, initMap, UpdateTurnover);
        }

        private async Task AddInitialBalancesAsync(
            int length, List<TestBalanceItemViewModel> items, TestBalanceParameters parameters,
            string filter = null)
        {
            DbConsole.ConnectionString = UnitOfWork.CompanyConnection;
            if (parameters.IsByBranch)
            {
                await AddInitialBalanceByBranchAsync(length, items, parameters);
                return;
            }

            var initMap = new Dictionary<string, VoucherLineAmountsViewModel>();
            var query = await GetInitBalanceQueryAsync(length, parameters, filter);
            var result = DbConsole.ExecuteQuery(query.Query);
            foreach (DataRow row in result.Rows)
            {
                decimal balance = _utility.ValueOrDefault<decimal>(row, "Balance");
                var amounts = new VoucherLineAmountsViewModel()
                {
                    Debit = Math.Max(0, balance),
                    Credit = Math.Abs(Math.Min(0, balance))
                };
                initMap.Add(_utility.ValueOrDefault(row, "FullCode"), amounts);
            }

            MergeByCode(items, initMap, UpdateInitBalance);
        }

        private async Task AddTurnoversByBranchAsync(
            int length, List<TestBalanceItemViewModel> items, TestBalanceParameters parameters)
        {
            var initMap = new Dictionary<FullCodeBranch, VoucherLineAmountsViewModel>();
            var query = await GetTurnoverQueryAsync(length, parameters);
            var result = DbConsole.ExecuteQuery(query.Query);
            foreach (DataRow row in result.Rows)
            {
                var amounts = new VoucherLineAmountsViewModel()
                {
                    Debit = _utility.ValueOrDefault<decimal>(row, "DebitSum"),
                    Credit = _utility.ValueOrDefault<decimal>(row, "CreditSum")
                };
                var codeBranch = new FullCodeBranch()
                {
                    FullCode = _utility.ValueOrDefault(row, "FullCode"),
                    BranchName = _utility.ValueOrDefault(row, "BranchName")
                };
                initMap.Add(codeBranch, amounts);
            }

            MergeByCodeBranch(items, initMap, UpdateTurnoverByBranch);
        }

        private async Task AddInitialBalanceByBranchAsync(
            int length, List<TestBalanceItemViewModel> items, TestBalanceParameters parameters)
        {
            var initMap = new Dictionary<FullCodeBranch, VoucherLineAmountsViewModel>();
            var query = await GetInitBalanceQueryAsync(length, parameters);
            var result = DbConsole.ExecuteQuery(query.Query);
            foreach (DataRow row in result.Rows)
            {
                decimal balance = _utility.ValueOrDefault<decimal>(row, "Balance");
                var amounts = new VoucherLineAmountsViewModel()
                {
                    Debit = Math.Max(0, balance),
                    Credit = Math.Abs(Math.Min(0, balance))
                };
                var codeBranch = new FullCodeBranch()
                {
                    FullCode = _utility.ValueOrDefault(row, "FullCode"),
                    BranchName = _utility.ValueOrDefault(row, "BranchName")
                };
                initMap.Add(codeBranch, amounts);
            }

            MergeByCodeBranch(items, initMap, UpdateInitBalanceByBranch);
        }

        private TestBalanceItemViewModel UpdateInitBalance(
            TestBalanceItemViewModel item, Dictionary<string, VoucherLineAmountsViewModel> itemMap, string fullCode)
        {
            if (item != null && itemMap.ContainsKey(fullCode))
            {
                var amounts = itemMap[fullCode];
                item.StartBalanceDebit = amounts.Debit;
                item.StartBalanceCredit = amounts.Credit;
            }
            else if (item == null && itemMap.ContainsKey(fullCode))
            {
                var amounts = itemMap[fullCode];
                item = new TestBalanceItemViewModel()
                {
                    AccountFullCode = fullCode,
                    DetailAccountFullCode = fullCode,
                    CostCenterFullCode = fullCode,
                    ProjectFullCode = fullCode,
                    StartBalanceDebit = amounts.Debit,
                    StartBalanceCredit = amounts.Credit,
                };
            }

            return item;
        }

        private TestBalanceItemViewModel UpdateInitBalanceByBranch(
            TestBalanceItemViewModel item, Dictionary<FullCodeBranch, VoucherLineAmountsViewModel> itemMap,
            FullCodeBranch codeBranch)
        {
            if (item != null && itemMap.ContainsKey(codeBranch))
            {
                var amounts = itemMap[codeBranch];
                item.StartBalanceDebit = amounts.Debit;
                item.StartBalanceCredit = amounts.Credit;
            }
            else if (item == null && itemMap.ContainsKey(codeBranch))
            {
                var amounts = itemMap[codeBranch];
                item = new TestBalanceItemViewModel()
                {
                    AccountFullCode = codeBranch.FullCode,
                    DetailAccountFullCode = codeBranch.FullCode,
                    CostCenterFullCode = codeBranch.FullCode,
                    ProjectFullCode = codeBranch.FullCode,
                    BranchName = codeBranch.BranchName,
                    StartBalanceDebit = amounts.Debit,
                    StartBalanceCredit = amounts.Credit,
                };
            }

            return item;
        }

        private TestBalanceItemViewModel UpdateTurnover(
            TestBalanceItemViewModel item, Dictionary<string, VoucherLineAmountsViewModel> itemMap, string fullCode)
        {
            if (item != null && itemMap.ContainsKey(fullCode))
            {
                var amounts = itemMap[fullCode];
                item.TurnoverDebit = amounts.Debit;
                item.TurnoverCredit = amounts.Credit;
            }
            else if (item == null && itemMap.ContainsKey(fullCode))
            {
                var amounts = itemMap[fullCode];
                item = new TestBalanceItemViewModel()
                {
                    AccountFullCode = fullCode,
                    DetailAccountFullCode = fullCode,
                    CostCenterFullCode = fullCode,
                    ProjectFullCode = fullCode,
                    TurnoverDebit = amounts.Debit,
                    TurnoverCredit = amounts.Credit,
                };
            }

            return item;
        }

        private TestBalanceItemViewModel UpdateTurnoverByBranch(
            TestBalanceItemViewModel item, Dictionary<FullCodeBranch, VoucherLineAmountsViewModel> itemMap,
            FullCodeBranch codeBranch)
        {
            if (item != null && itemMap.ContainsKey(codeBranch))
            {
                var amounts = itemMap[codeBranch];
                item.TurnoverDebit = amounts.Debit;
                item.TurnoverCredit = amounts.Credit;
            }
            else if (item == null && itemMap.ContainsKey(codeBranch))
            {
                var amounts = itemMap[codeBranch];
                item = new TestBalanceItemViewModel()
                {
                    AccountFullCode = codeBranch.FullCode,
                    DetailAccountFullCode = codeBranch.FullCode,
                    CostCenterFullCode = codeBranch.FullCode,
                    ProjectFullCode = codeBranch.FullCode,
                    BranchName = codeBranch.BranchName,
                    TurnoverDebit = amounts.Debit,
                    TurnoverCredit = amounts.Credit,
                };
            }

            return item;
        }

        private void SetItemNames(int viewId, int length, IEnumerable<TestBalanceItemViewModel> items)
        {
            DbConsole.ConnectionString = UnitOfWork.CompanyConnection;
            var query = _utility.GetItemLookupQuery(viewId, length);
            var result = DbConsole.ExecuteQuery(query.Query);

            var itemMap = new Dictionary<string, string>(result.Rows.Count);
            foreach (DataRow row in result.Rows)
            {
                string key = _utility.ValueOrDefault(row, "FullCode");
                string value = String.Join(",",
                    _utility.ValueOrDefault<int>(row, "Id"), _utility.ValueOrDefault(row, "Name"));
                itemMap.Add(key, value);
            }

            foreach (var item in items)
            {
                // NOTE: All codes are set to be identical in each balance item
                if (itemMap.ContainsKey(item.AccountFullCode))
                {
                    var idName = itemMap[item.AccountFullCode].Split(',');
                    item.AccountId =
                        item.DetailAccountId =
                        item.CostCenterId =
                        item.ProjectId = Int32.Parse(idName[0]);
                    item.AccountName =
                        item.DetailAccountName =
                        item.CostCenterName =
                        item.ProjectName = idName[1];
                }
            }
        }

        private IEnumerable<TestBalanceItemViewModel> GetQueryResult(ReportQuery query)
        {
            DbConsole.ConnectionString = UnitOfWork.CompanyConnection;
            var result = DbConsole.ExecuteQuery(query.Query);
            return result.Rows
                .Cast<DataRow>()
                .Select(row => GetBalanceItem(row));
        }

        private TestBalanceItemViewModel GetBalanceItem(DataRow row)
        {
            var balance = _utility.ValueOrDefault<decimal>(row, "Balance");
            var fullCode = _utility.ValueOrDefault(row, "FullCode");
            return new TestBalanceItemViewModel()
            {
                AccountFullCode = fullCode,
                DetailAccountFullCode = fullCode,
                CostCenterFullCode = fullCode,
                ProjectFullCode = fullCode,
                TurnoverDebit = _utility.ValueOrDefault<decimal>(row, "DebitSum"),
                TurnoverCredit = _utility.ValueOrDefault<decimal>(row, "CreditSum"),
                EndBalanceDebit = Math.Max(0, balance),
                EndBalanceCredit = Math.Abs(Math.Min(0, balance)),
                BranchName = _utility.ValueOrDefault(row, "BranchName")
            };
        }

        private async Task<ReportQuery> GetEndBalanceQueryAync(
            int length, TestBalanceParameters parameters, string otherFilter = null)
        {
            ReportQuery query;
            string componentName = GetComponentName(parameters.ViewId);
            string fieldName = parameters.ViewId == ViewId.DetailAccount
                ? "Detail"
                : componentName;

            if (parameters.FromDate.HasValue && parameters.ToDate.HasValue)
            {
                var fpStart = await _utility.GetFiscalPeriodStartAsync(UserContext.FiscalPeriodId);
                var toDate = parameters.ToDate.Value;
                query = parameters.IsByBranch
                    ? new ReportQuery(String.Format(BalanceQuery.EndBalanceByDateByBranch, length, componentName,
                        fieldName, fpStart.ToShortDateString(false), toDate.ToShortDateString(false)))
                    : new ReportQuery(String.Format(BalanceQuery.EndBalanceByDate, length, componentName,
                        fieldName, fpStart.ToShortDateString(false), toDate.ToShortDateString(false)));
            }
            else
            {
                var toNo = parameters.ToNo.Value;
                query = parameters.IsByBranch
                    ? new ReportQuery(String.Format(BalanceQuery.EndBalanceByNoByBranch, length, componentName,
                        fieldName, 1, toNo))
                    : new ReportQuery(String.Format(BalanceQuery.EndBalanceByNo, length, componentName,
                        fieldName, 1, toNo));
            }

            var openingVoucher = await _utility.GetOpeningVoucherAsync();
            query.SetFilter(GetEnvironmentFilters(parameters, otherFilter, openingVoucher, false));
            return query;
        }

        private async Task<ReportQuery> GetTurnoverQueryAsync(
            int length, TestBalanceParameters parameters, string otherFilter = null)
        {
            ReportQuery query;
            string componentName = GetComponentName(parameters.ViewId);
            string fieldName = parameters.ViewId == ViewId.DetailAccount
                ? "Detail"
                : componentName;

            if (parameters.FromDate.HasValue && parameters.ToDate.HasValue)
            {
                var fromDate = parameters.FromDate.Value;
                var toDate = parameters.ToDate.Value;
                query = parameters.IsByBranch
                    ? new ReportQuery(String.Format(BalanceQuery.TurnoverByDateByBranch, length, componentName,
                        fieldName, fromDate.ToShortDateString(false), toDate.ToShortDateString(false)))
                    : new ReportQuery(String.Format(BalanceQuery.TurnoverByDate, length, componentName,
                        fieldName, fromDate.ToShortDateString(false), toDate.ToShortDateString(false)));
            }
            else
            {
                var fromNo = parameters.FromNo.Value;
                var toNo = parameters.ToNo.Value;
                query = parameters.IsByBranch
                    ? new ReportQuery(String.Format(BalanceQuery.TurnoverByNoByBranch, length, componentName,
                        fieldName, fromNo, toNo))
                    : new ReportQuery(String.Format(BalanceQuery.TurnoverByNo, length, componentName,
                        fieldName, fromNo, toNo));
            }

            var openingVoucher = await _utility.GetOpeningVoucherAsync();
            query.SetFilter(GetEnvironmentFilters(parameters, otherFilter, openingVoucher));
            return query;
        }

        private async Task<ReportQuery> GetInitBalanceQueryAsync(
            int length, TestBalanceParameters parameters, string filter = null)
        {
            ReportQuery query;
            string componentName = GetComponentName(parameters.ViewId);
            string fieldName = parameters.ViewId == ViewId.DetailAccount
                ? "Detail"
                : componentName;
            if (parameters.FromDate.HasValue && parameters.ToDate.HasValue)
            {
                query = parameters.IsByBranch
                    ? new ReportQuery(String.Format(BalanceQuery.InitBalanceByDateByBranch,
                        length, componentName, fieldName))
                    : new ReportQuery(String.Format(BalanceQuery.InitBalanceByDate,
                        length, componentName, fieldName));
            }
            else
            {
                query = parameters.IsByBranch
                    ? new ReportQuery(String.Format(BalanceQuery.InitBalanceByNoByBranch,
                        length, componentName, fieldName))
                    : new ReportQuery(String.Format(BalanceQuery.InitBalanceByNo,
                        length, componentName, fieldName));
            }

            var openingVoucher = await _utility.GetOpeningVoucherAsync();
            query.SetFilter(GetEnvironmentFilters(parameters, openingVoucher, filter));
            return query;
        }

        private string GetEnvironmentFilters(
            TestBalanceParameters parameters, string otherFilters, Voucher openingVoucher,
            bool isTurnover = true)
        {
            var predicates = GetEnvironmentFilters(parameters.GridOptions, parameters.Options);
            if (!String.IsNullOrEmpty(otherFilters))
            {
                predicates.Add(otherFilters);
            }

            bool mustApply = _utility.MustApplyOpeningOption(parameters.Options, openingVoucher);
            if (mustApply && isTurnover)
            {
                predicates.Add(String.Format("v.OriginID <> {0}", (int)VoucherOriginId.OpeningVoucher));
            }

            return String.Join(" AND ", predicates);
        }

        private string GetEnvironmentFilters(
            TestBalanceParameters parameters, Voucher openingVoucher, string filter = null)
        {
            var predicates = GetEnvironmentFilters(parameters.GridOptions, parameters.Options);
            bool mustApply = _utility.MustApplyOpeningOption(parameters.Options, openingVoucher);
            bool isByDate = parameters.FromDate.HasValue && parameters.ToDate.HasValue;

            string datePredicate = isByDate
                ? String.Format("v.Date < '{0}'", parameters.FromDate.Value.ToShortDateString(false))
                : String.Format("v.No < {0}", parameters.FromNo.Value);
            if (mustApply)
            {
                datePredicate = isByDate
                    ? String.Format(
                        "(v.Date < '{0}' OR (v.Date >= '{0}' AND v.OriginID = {1}))",
                        parameters.FromDate.Value.ToShortDateString(false),
                        (int)VoucherOriginId.OpeningVoucher)
                    : String.Format(
                        "(v.No < {0} OR (v.No >= {0} AND v.OriginID = {1}))",
                        parameters.FromNo.Value, (int)VoucherOriginId.OpeningVoucher);
            }

            predicates.Add(datePredicate);
            if (filter != null)
            {
                predicates.Add(filter);
            }

            return String.Join(" AND ", predicates);
        }

        private List<string> GetEnvironmentFilters(GridOptions gridOptions, FinanceReportOptions options)
        {
            var predicates = new List<string>
            {
                _utility.GetEnvironmentFilters(gridOptions)
            };

            if ((options & FinanceReportOptions.UseClosingVoucher) == 0)
            {
                predicates.Add(String.Format("v.OriginID <> {0}", (int)VoucherOriginId.ClosingVoucher));
            }

            if ((options & FinanceReportOptions.UseClosingTempVoucher) == 0)
            {
                predicates.Add(String.Format("v.OriginID <> {0}", (int)VoucherOriginId.ClosingTempAccounts));
            }

            return predicates;
        }

        private readonly IReportDirectUtility _utility;
        private delegate TestBalanceItemViewModel MergeByCodeFunction(
            TestBalanceItemViewModel item, Dictionary<string, VoucherLineAmountsViewModel> itemMap,
            string fullCode);
        private delegate TestBalanceItemViewModel MergeByCodeBranchFunction(
            TestBalanceItemViewModel item, Dictionary<FullCodeBranch, VoucherLineAmountsViewModel> itemMap,
            FullCodeBranch codeBranch);
    }
}
