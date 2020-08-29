using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using SPPC.Framework.Extensions;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Model.Finance;
using SPPC.Tadbir.Persistence.Utility;
using SPPC.Tadbir.Values;
using SPPC.Tadbir.ViewModel.Reporting;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای خواندن اطلاعات گزارش تراز آزمایشی را پیاده سازی می کند
    /// </summary>
    public class TestBalanceRepository : LoggingRepository<Account, object>, ITestBalanceRepository
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="context">امکانات مشترک مورد نیاز را برای عملیات دیتابیسی فراهم می کند</param>
        /// <param name="system">امکانات مورد نیاز در دیتابیس های سیستمی را فراهم می کند</param>
        /// <param name="factory">امکان ساختن کلاس های کمکی محاسبات تراز را برای مولفه های مختلف حساب فراهم می کند</param>
        public TestBalanceRepository(IRepositoryContext context, ISystemRepository system,
            ITestBalanceUtilityFactory factory)
            : base(context, system.Logger)
        {
            _system = system;
            _factory = factory;
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
            _utility = _factory.Create(parameters.ViewId);
            var testBalance = new TestBalanceViewModel();
            var lines = await GetRawBalanceLinesAsync(parameters);
            Func<TestBalanceItemViewModel, bool> filter;
            int index = 0;
            while (index < level)
            {
                filter = _utility.GetCurrentlevelFilter(index);
                foreach (var lineGroup in _utility.GetTurnoverGroups(lines, index, filter))
                {
                    await AddTwoAndFourColumnBalanceItemAsync(
                        testBalance, lineGroup, lineGroup.Key, parameters.Format, parameters.IsByBranch);
                }

                index++;
            }

            filter = _utility.GetUpperlevelFilter(index);
            foreach (var lineGroup in _utility.GetTurnoverGroups(lines, index, filter))
            {
                await AddTwoAndFourColumnBalanceItemAsync(
                    testBalance, lineGroup, lineGroup.Key, parameters.Format, parameters.IsByBranch);
            }

            await AddSixAndEightColumnBalanceItemsAsync(testBalance, parameters);
            await ApplyZeroBalanceOptionAsync(testBalance, parameters);
            testBalance.SetBalanceItems(testBalance.Items
                .Apply(parameters.GridOptions, false)
                .ToArray());
            SetSummaryItems(testBalance);

            var source = (parameters.ViewId == ViewId.Account)
                ? OperationSourceId.TestBalance
                : OperationSourceId.ItemBalance;
            await OnSourceActionAsync(parameters.GridOptions, source,
                (SourceListId)_utility.GetSourceList(parameters.Format));
            return testBalance;
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
            _utility = _factory.Create(parameters.ViewId);
            var testBalance = new TestBalanceViewModel();
            var accountItem = await _utility.GetItemAsync(accountId);
            if (accountItem != null)
            {
                int groupLevel = accountItem.Level + 1;
                var lineFilter = _utility.GetUpperlevelFilter(groupLevel);
                IEnumerable<TestBalanceItemViewModel> lines = await GetRawBalanceLinesAsync(parameters);
                lines = _utility.FilterBalanceLines(lines, accountItem);
                foreach (var lineGroup in _utility.GetTurnoverGroups(lines, groupLevel, lineFilter))
                {
                    await AddTwoAndFourColumnBalanceItemAsync(
                        testBalance, lineGroup, lineGroup.Key, parameters.Format, parameters.IsByBranch);
                }

                await AddSixAndEightColumnBalanceItemsAsync(testBalance, parameters);
                await ApplyZeroBalanceOptionAsync(testBalance, parameters);
                testBalance.SetBalanceItems(testBalance.Items
                    .Apply(parameters.GridOptions, false)
                    .ToArray());
                SetSummaryItems(testBalance);
            }

            var source = (parameters.ViewId == ViewId.Account)
                ? OperationSourceId.TestBalance
                : OperationSourceId.ItemBalance;
            await OnSourceActionAsync(parameters.GridOptions, source,
                (SourceListId)_utility.GetSourceList(parameters.Format));
            return testBalance;
        }

        /// <summary>
        /// به روش آسنکرون، انواع مختلف تراز آزمایشی را با توجه به ساختار درختی سرفصل های حسابداری خوانده و برمی گرداند
        /// </summary>
        /// <returns>انواع مختلف تراز آزمایشی</returns>
        public async Task<IEnumerable<TestBalanceModeInfo>> GetBalanceTypesLookupAsync(int viewId)
        {
            _utility = _factory.Create(viewId);
            var lookup = new List<TestBalanceModeInfo>();
            lookup.AddRange(await _utility.GetLevelBalanceTypesAsync());
            lookup.AddRange(await _utility.GetChildBalanceTypesAsync());
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
        public TestBalanceParameters BuildParameters(int viewId, string from, string to,
            TestBalanceMode mode, TestBalanceFormat format,
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

        private ISecureRepository Repository
        {
            get { return _system.Repository; }
        }

        private IConfigRepository Config
        {
            get { return _system.Config; }
        }

        private static void UpdateEndBalances(TestBalanceViewModel testBalance)
        {
            foreach (var item in testBalance.Items)
            {
                decimal balance = item.StartBalanceDebit + item.TurnoverDebit
                    - (item.StartBalanceCredit + item.TurnoverCredit);
                item.EndBalanceDebit = Math.Max(0, balance);
                item.EndBalanceCredit = Math.Abs(Math.Min(0, balance));
            }
        }

        private static void AddOperationSums(TestBalanceViewModel testBalance)
        {
            foreach (var item in testBalance.Items)
            {
                item.OperationSumDebit = item.StartBalanceDebit + item.TurnoverDebit;
                item.OperationSumCredit = item.StartBalanceCredit + item.TurnoverCredit;
            }
        }

        private static void SetSummaryItems(TestBalanceViewModel testBalance)
        {
            testBalance.Total.StartBalanceDebit = testBalance.Items.Sum(item => item.StartBalanceDebit);
            testBalance.Total.StartBalanceCredit = testBalance.Items.Sum(item => item.StartBalanceCredit);
            testBalance.Total.TurnoverDebit = testBalance.Items.Sum(item => item.TurnoverDebit);
            testBalance.Total.TurnoverCredit = testBalance.Items.Sum(item => item.TurnoverCredit);
            testBalance.Total.OperationSumDebit = testBalance.Items.Sum(item => item.OperationSumDebit);
            testBalance.Total.OperationSumCredit = testBalance.Items.Sum(item => item.OperationSumCredit);
            testBalance.Total.CorrectionsDebit = testBalance.Items.Sum(item => item.CorrectionsDebit);
            testBalance.Total.CorrectionsCredit = testBalance.Items.Sum(item => item.CorrectionsCredit);
            testBalance.Total.EndBalanceDebit = testBalance.Items.Sum(item => item.EndBalanceDebit);
            testBalance.Total.EndBalanceCredit = testBalance.Items.Sum(item => item.EndBalanceCredit);
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

        private async Task<IList<TestBalanceItemViewModel>> GetRawBalanceLinesAsync(
            TestBalanceParameters parameters)
        {
            var query = Repository
                .GetAllOperationQuery<VoucherLine>(ViewId.VoucherLine,
                    art => art.Voucher, art => art.Branch);
            query = _utility.IncludeVoucherLineReference(query);

            var options = parameters.Options;
            if ((options & TestBalanceOptions.UseClosingVoucher) == 0)
            {
                query = query.Where(
                    art => art.Voucher.VoucherOriginId != (int)VoucherOriginId.ClosingVoucher);
            }

            if ((options & TestBalanceOptions.UseClosingTempVoucher) == 0)
            {
                query = query.Where(
                    art => art.Voucher.VoucherOriginId != (int)VoucherOriginId.ClosingTempAccounts);
            }

            if ((options & TestBalanceOptions.OpeningVoucherAsInitBalance) > 0)
            {
                query = query.Where(
                    art => art.Voucher.VoucherOriginId != (int)VoucherOriginId.OpeningVoucher);
            }

            IList<TestBalanceItemViewModel> lines = null;
            if (parameters.FromDate != null && parameters.ToDate != null)
            {
                lines = await _utility.GetRawReportByDateLinesAsync<TestBalanceItemViewModel>(
                    query, parameters.FromDate.Value, parameters.ToDate.Value, parameters.GridOptions);
            }
            else if (parameters.FromNo != null && parameters.ToNo != null)
            {
                lines = await _utility.GetRawReportByNumberLinesAsync<TestBalanceItemViewModel>(
                    query, parameters.FromNo.Value, parameters.ToNo.Value, parameters.GridOptions);
            }

            return lines;
        }

        private async Task<TestBalanceItemViewModel> GetTwoAndFourColumnBalanceItemAsync(
            IEnumerable<TestBalanceItemViewModel> lines, TestBalanceFormat format, string fullCode)
        {
            var first = lines.First();
            var balanceItem = await _utility.GetItemFromVoucherLineAsync(lines.First(), fullCode);
            decimal turnoverDebit = lines.Sum(item => item.TurnoverDebit);
            decimal turnoverCredit = lines.Sum(item => item.TurnoverCredit);
            decimal balance = turnoverDebit - turnoverCredit;
            balanceItem.EndBalanceDebit = Math.Max(0, balance);
            balanceItem.EndBalanceCredit = Math.Abs(Math.Min(0, balance));
            if (format >= TestBalanceFormat.FourColumn)
            {
                balanceItem.TurnoverDebit = turnoverDebit;
                balanceItem.TurnoverCredit = turnoverCredit;
            }

            return balanceItem;
        }

        private async Task AddTwoAndFourColumnBalanceItemAsync(TestBalanceViewModel testBalance,
            IEnumerable<TestBalanceItemViewModel> lines, string fullCode, TestBalanceFormat format, bool byBranch)
        {
            if (byBranch)
            {
                foreach (var branchGroup in _utility.GetGroupByItems(lines, line => line.BranchId))
                {
                    testBalance.Items.Add(await GetTwoAndFourColumnBalanceItemAsync(branchGroup, format, fullCode));
                }
            }
            else
            {
                testBalance.Items.Add(await GetTwoAndFourColumnBalanceItemAsync(lines, format, fullCode));
            }
        }

        private async Task AddSixAndEightColumnBalanceItemsAsync(
            TestBalanceViewModel testBalance, TestBalanceParameters parameters)
        {
            if (parameters.Format >= TestBalanceFormat.SixColumn)
            {
                await AddInitialBalancesAsync(testBalance, parameters);
                UpdateEndBalances(testBalance);
            }

            if (parameters.Format >= TestBalanceFormat.EightColumn)
            {
                AddOperationSums(testBalance);
            }
        }

        private async Task AddInitialBalancesAsync(TestBalanceViewModel testBalance, TestBalanceParameters parameters)
        {
            foreach (var item in testBalance.Items)
            {
                int itemId = _utility.GetItemId(item);
                decimal balance = await _utility.GetInitialBalanceAsync(itemId, parameters);
                item.StartBalanceDebit = Math.Max(0, balance);
                item.StartBalanceCredit = Math.Abs(Math.Min(0, balance));
            }
        }

        private async Task ApplyZeroBalanceOptionAsync(
            TestBalanceViewModel testBalance, TestBalanceParameters parameters)
        {
            if ((parameters.Options & TestBalanceOptions.ShowZeroBalanceItems) > 0)
            {
                testBalance.Items.AddRange(
                    await _utility.GetZeroBalanceItemsAsync(testBalance.Items, parameters.Mode));
            }
            else
            {
                // Remove items with zero end balance...
                var nonZeroItems = testBalance.Items
                    .Where(item => !IsZeroBalanceItem(item))
                    .ToArray();
                testBalance.SetBalanceItems(nonZeroItems);
            }

            testBalance.SetBalanceItems(_utility.GetSortedItems(testBalance.Items));
        }

        private readonly ISystemRepository _system;
        private readonly ITestBalanceUtilityFactory _factory;
        private ITestBalanceUtility _utility;
    }
}
