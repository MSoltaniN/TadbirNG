using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using SPPC.Framework.Extensions;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Persistence.Utility;
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
            int length = 0;
            string filter = String.Empty;
            var query = default(ReportQuery);
            int index = 0;

            while (index < level)
            {
                length = _utility.GetLevelCodeLength(parameters.ViewId, index);
                filter = String.Format("Level == {0}", index);
                query = GetLevelQuery(length, parameters, filter);
                items.AddRange(GetQueryResult(query));
                index++;
            }

            length = _utility.GetLevelCodeLength(parameters.ViewId, level);
            filter = String.Format("Level >= {0}", level);
            query = GetLevelQuery(length, parameters, filter);
            items.AddRange(GetQueryResult(query));

            if (parameters.Format >= TestBalanceFormat.SixColumn)
            {
                AddInitialBalances(length, items, parameters);
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
                var filter = String.Format("Level >= {0} AND acc.FullCode LIKE '{1}%'", level, accountItem.FullCode);
                int length = _utility.GetLevelCodeLength(parameters.ViewId, level);
                var query = GetLevelQuery(length, parameters, filter);
                items.AddRange(GetQueryResult(query));

                if (parameters.Format >= TestBalanceFormat.SixColumn)
                {
                    AddInitialBalances(length, items, parameters);
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
                parameters.Options = (TestBalanceOptions)options.Value;
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
            string enumValue = null;
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

        private async Task<List<TestBalanceItemViewModel>> ApplyZeroBalanceOptionAsync(
            IEnumerable<TestBalanceItemViewModel> items, TestBalanceParameters parameters, int level)
        {
            IEnumerable<TestBalanceItemViewModel> newItems = null;
            if ((parameters.Options & TestBalanceOptions.ShowZeroBalanceItems) > 0)
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
            SetSummaryItems(balance, items);
            balance.Items.AddRange(items.ApplyPaging(parameters.GridOptions));
            SetItemNames(parameters.ViewId, length, balance.Items);
        }

        private void AddInitialBalances(
            int length, IEnumerable<TestBalanceItemViewModel> items, TestBalanceParameters parameters)
        {
            DbConsole.ConnectionString = UnitOfWork.CompanyConnection;
            if (parameters.IsByBranch)
            {
                AddInitialBalanceByBranch(length, items, parameters);
                return;
            }

            var initMap = new Dictionary<string, decimal>();
            var openingMap = new Dictionary<string, decimal>();
            var query = GetInitBalanceQuery(length, parameters);
            var result = DbConsole.ExecuteQuery(query.Query);
            foreach (DataRow row in result.Rows)
            {
                initMap.Add(_utility.ValueOrDefault(row, "FullCode"),
                    _utility.ValueOrDefault<decimal>(row, "Balance"));
            }

            bool openingAsInitBalance =
                (parameters.Options & TestBalanceOptions.OpeningVoucherAsInitBalance) > 0;
            if (openingAsInitBalance)
            {
                query = GetInitBalanceQuery(length, parameters, true);
                result = DbConsole.ExecuteQuery(query.Query);
                foreach (DataRow row in result.Rows)
                {
                    openingMap.Add(_utility.ValueOrDefault(row, "FullCode"),
                        _utility.ValueOrDefault<decimal>(row, "Balance"));
                }
            }

            foreach (var item in items)
            {
                decimal balance = 0.0M;
                if (initMap.ContainsKey(item.AccountFullCode))
                {
                    balance = initMap[item.AccountFullCode];
                }

                if (openingAsInitBalance && openingMap.ContainsKey(item.AccountFullCode))
                {
                    balance += openingMap[item.AccountFullCode];
                }

                item.StartBalanceDebit = Math.Max(0, balance);
                item.StartBalanceCredit = Math.Abs(Math.Min(0, balance));

                balance = item.StartBalanceDebit + item.TurnoverDebit
                    - (item.StartBalanceCredit + item.TurnoverCredit);
                item.EndBalanceDebit = Math.Max(0, balance);
                item.EndBalanceCredit = Math.Abs(Math.Min(0, balance));
            }
        }

        private void AddInitialBalanceByBranch(
            int length, IEnumerable<TestBalanceItemViewModel> items, TestBalanceParameters parameters)
        {
            var initMap = new Dictionary<FullCodeBranch, decimal>();
            var openingMap = new Dictionary<FullCodeBranch, decimal>();
            var query = GetInitBalanceQuery(length, parameters);
            var result = DbConsole.ExecuteQuery(query.Query);
            foreach (DataRow row in result.Rows)
            {
                var codeBranch = new FullCodeBranch()
                {
                    FullCode = _utility.ValueOrDefault(row, "FullCode"),
                    BranchName = _utility.ValueOrDefault(row, "BranchName")
                };
                initMap.Add(codeBranch, _utility.ValueOrDefault<decimal>(row, "Balance"));
            }

            bool openingAsInitBalance =
                (parameters.Options & TestBalanceOptions.OpeningVoucherAsInitBalance) > 0;
            if (openingAsInitBalance)
            {
                query = GetInitBalanceQuery(length, parameters, true);
                result = DbConsole.ExecuteQuery(query.Query);
                foreach (DataRow row in result.Rows)
                {
                    var codeBranch = new FullCodeBranch()
                    {
                        FullCode = _utility.ValueOrDefault(row, "FullCode"),
                        BranchName = _utility.ValueOrDefault(row, "BranchName")
                    };
                    openingMap.Add(codeBranch, _utility.ValueOrDefault<decimal>(row, "Balance"));
                }
            }

            foreach (var item in items)
            {
                decimal balance = 0.0M;
                var codeBranch = new FullCodeBranch()
                {
                    FullCode = item.AccountFullCode,
                    BranchName = item.BranchName
                };
                if (initMap.ContainsKey(codeBranch))
                {
                    balance = initMap[codeBranch];
                }

                if (openingAsInitBalance && openingMap.ContainsKey(codeBranch))
                {
                    balance += openingMap[codeBranch];
                }

                item.StartBalanceDebit = Math.Max(0, balance);
                item.StartBalanceCredit = Math.Abs(Math.Min(0, balance));

                balance = item.StartBalanceDebit + item.TurnoverDebit
                    - (item.StartBalanceCredit + item.TurnoverCredit);
                item.EndBalanceDebit = Math.Max(0, balance);
                item.EndBalanceCredit = Math.Abs(Math.Min(0, balance));
            }
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

        private ReportQuery GetLevelQuery(
            int length, TestBalanceParameters parameters, string otherFilter = null)
        {
            var query = default(ReportQuery);
            string componentName = GetComponentName(parameters.ViewId);
            string fieldName = parameters.ViewId == ViewId.DetailAccount
                ? "Detail"
                : componentName;

            if (parameters.FromDate.HasValue && parameters.ToDate.HasValue)
            {
                var fromDate = parameters.FromDate.Value;
                var toDate = parameters.ToDate.Value;
                if (parameters.Format == TestBalanceFormat.TwoColumn)
                {
                    query = parameters.IsByBranch
                        ? new ReportQuery(String.Format(BalanceQuery.TwoColumnByDateByBranch, length, componentName,
                            fieldName, fromDate.ToShortDateString(false), toDate.ToShortDateString(false)))
                        : new ReportQuery(String.Format(BalanceQuery.TwoColumnByDate, length, componentName,
                            fieldName, fromDate.ToShortDateString(false), toDate.ToShortDateString(false)));
                }
                else
                {
                    query = parameters.IsByBranch
                        ? new ReportQuery(String.Format(BalanceQuery.FourColumnByDateByBranch, length, componentName,
                            fieldName, fromDate.ToShortDateString(false), toDate.ToShortDateString(false)))
                        : new ReportQuery(String.Format(BalanceQuery.FourColumnByDate, length, componentName,
                            fieldName, fromDate.ToShortDateString(false), toDate.ToShortDateString(false)));
                }
            }
            else
            {
                var fromNo = parameters.FromNo.Value;
                var toNo = parameters.ToNo.Value;
                if (parameters.Format == TestBalanceFormat.TwoColumn)
                {
                    query = parameters.IsByBranch
                        ? new ReportQuery(String.Format(BalanceQuery.TwoColumnByNoByBranch, length, componentName,
                            fieldName, fromNo, toNo))
                        : new ReportQuery(String.Format(BalanceQuery.TwoColumnByNo, length, componentName,
                            fieldName, fromNo, toNo));
                }
                else
                {
                    query = parameters.IsByBranch
                        ? new ReportQuery(String.Format(BalanceQuery.FourColumnByNoByBranch, length, componentName,
                            fieldName, fromNo, toNo))
                        : new ReportQuery(String.Format(BalanceQuery.FourColumnByNo, length, componentName,
                            fieldName, fromNo, toNo));
                }
            }

            query.SetFilter(GetEnvironmentFilters(parameters, otherFilter));
            return query;
        }

        private ReportQuery GetInitBalanceQuery(
            int length, TestBalanceParameters parameters, bool isOpening = false)
        {
            var query = default(ReportQuery);
            string componentName = GetComponentName(parameters.ViewId);
            string fieldName = parameters.ViewId == ViewId.DetailAccount
                ? "Detail"
                : componentName;

            if (parameters.FromDate.HasValue && parameters.ToDate.HasValue)
            {
                var fromDate = parameters.FromDate.Value;
                if (isOpening)
                {
                    query = parameters.IsByBranch
                        ? new ReportQuery(String.Format(BalanceQuery.OpeningBalanceByDateByBranch, length, componentName,
                            fieldName, fromDate.ToShortDateString(false)))
                        : new ReportQuery(String.Format(BalanceQuery.OpeningBalanceByDate, length, componentName,
                            fieldName, fromDate.ToShortDateString(false)));
                }
                else
                {
                    query = parameters.IsByBranch
                        ? new ReportQuery(String.Format(BalanceQuery.InitBalanceByDateByBranch, length, componentName,
                            fieldName, fromDate.ToShortDateString(false)))
                        : new ReportQuery(String.Format(BalanceQuery.InitBalanceByDate, length, componentName,
                            fieldName, fromDate.ToShortDateString(false)));
                }
            }
            else
            {
                var fromNo = parameters.FromNo.Value;
                if (isOpening)
                {
                    query = parameters.IsByBranch
                        ? new ReportQuery(String.Format(BalanceQuery.OpeningBalanceByNoByBranch, length, componentName,
                            fieldName, fromNo))
                        : new ReportQuery(String.Format(BalanceQuery.OpeningBalanceByNo, length, componentName,
                            fieldName, fromNo));
                }
                else
                {
                    query = parameters.IsByBranch
                        ? new ReportQuery(String.Format(BalanceQuery.InitBalanceByNoByBranch, length, componentName,
                            fieldName, fromNo))
                        : new ReportQuery(String.Format(BalanceQuery.InitBalanceByNo, length, componentName,
                            fieldName, fromNo));
                }
            }

            query.SetFilter(GetEnvironmentFilters(parameters));
            return query;
        }

        private string GetEnvironmentFilters(TestBalanceParameters parameters, string otherFilters = null)
        {
            var predicates = new List<string>
            {
                _utility.GetEnvironmentFilters(parameters.GridOptions, UserContext.FiscalPeriodId)
            };

            var options = parameters.Options;
            if ((options & TestBalanceOptions.UseClosingVoucher) == 0)
            {
                predicates.Add(String.Format("VoucherOriginID != 4"));
            }

            if ((options & TestBalanceOptions.UseClosingTempVoucher) == 0)
            {
                predicates.Add(String.Format("VoucherOriginID != 3"));
            }

            if ((options & TestBalanceOptions.OpeningVoucherAsInitBalance) > 0)
            {
                predicates.Add(String.Format("VoucherOriginID != 2"));
            }

            if (!String.IsNullOrEmpty(otherFilters))
            {
                predicates.Add(otherFilters);
            }

            return String.Join(" AND ", predicates);
        }

        private readonly IReportDirectUtility _utility;
    }
}
