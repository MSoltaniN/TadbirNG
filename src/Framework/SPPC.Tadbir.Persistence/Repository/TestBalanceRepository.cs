using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPPC.Framework.Extensions;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Model.Finance;
using SPPC.Tadbir.Values;
using SPPC.Tadbir.ViewModel.Reporting;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای خواندن اطلاعات گزارش تراز آزمایشی را پیاده سازی می کند
    /// </summary>
    public class TestBalanceRepository : RepositoryBase, ITestBalanceRepository
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="context">امکانات مشترک مورد نیاز را برای عملیات دیتابیسی فراهم می کند</param>
        /// <param name="system">امکانات مورد نیاز در دیتابیس های سیستمی را فراهم می کند</param>
        /// <param name="report">امکان انجام محاسبات مشترک در گزارشات برنامه را فراهم می کند</param>
        public TestBalanceRepository(IRepositoryContext context, ISystemRepository system, IReportRepository report)
            : base(context)
        {
            _system = system;
            _report = report;
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات گزارش تراز آزمایشی در سطح کل را خوانده و برمی گرداند
        /// </summary>
        /// <param name="parameters">پارامترهای مورد نیاز برای گزارش</param>
        /// <returns>اطلاعات گزارش با توجه به پارامترهای داده شده</returns>
        public async Task<TestBalanceViewModel> GetLedgerBalanceAsync(TestBalanceParameters parameters)
        {
            Func<TestBalanceItemViewModel, bool> ledgerFilter = line => true;
            return await GetGeneralBalanceAsync(TestBalanceMode.Ledger, parameters, ledgerFilter);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات گزارش تراز آزمایشی در سطح معین را خوانده و برمی گرداند
        /// </summary>
        /// <param name="parameters">پارامترهای مورد نیاز برای گزارش</param>
        /// <returns>اطلاعات گزارش با توجه به پارامترهای داده شده</returns>
        public async Task<TestBalanceViewModel> GetSubsidiaryBalanceAsync(TestBalanceParameters parameters)
        {
            Func<TestBalanceItemViewModel, bool> ledgerFilter = line => line.AccountLevel == 0;
            Func<TestBalanceItemViewModel, bool> subsidiaryFilter = line => line.AccountLevel >= 1;
            return await GetGeneralBalanceAsync(TestBalanceMode.Subsidiary, parameters,
                ledgerFilter, subsidiaryFilter);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات گزارش تراز آزمایشی در سطح تفصیلی را خوانده و برمی گرداند
        /// </summary>
        /// <param name="parameters">پارامترهای مورد نیاز برای گزارش</param>
        /// <returns>اطلاعات گزارش با توجه به پارامترهای داده شده</returns>
        public async Task<TestBalanceViewModel> GetDetailBalanceAsync(TestBalanceParameters parameters)
        {
            Func<TestBalanceItemViewModel, bool> ledgerFilter = line => line.AccountLevel == 0;
            Func<TestBalanceItemViewModel, bool> subsidiaryFilter = line => line.AccountLevel == 1;
            Func<TestBalanceItemViewModel, bool> detailFilter = line => line.AccountLevel >= 2;
            return await GetGeneralBalanceAsync(TestBalanceMode.Detail, parameters,
                ledgerFilter, subsidiaryFilter, detailFilter);
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
            var testBalance = new TestBalanceViewModel();
            var repository = UnitOfWork.GetAsyncRepository<Account>();
            var account = await repository.GetByIDAsync(accountId);
            if (account != null)
            {
                int groupLevel = account.Level + 1;
                Func<TestBalanceItemViewModel, bool> lineFilter = line => line.AccountLevel >= groupLevel;
                IEnumerable<TestBalanceItemViewModel> lines = await GetRawBalanceLinesAsync(parameters);
                lines = lines.Where(line => line.AccountFullCode.StartsWith(account.FullCode));
                foreach (var lineGroup in GetTurnoverGroups(lines, groupLevel, lineFilter))
                {
                    await AddTwoAndFourColumnBalanceItemAsync(
                        testBalance, lineGroup, lineGroup.Key, parameters.Format, parameters.IsByBranch);
                }

                await AddSixAndEightColumnBalanceItemsAsync(testBalance, parameters);
                await ApplyZeroBalanceOptionAsync(testBalance, parameters);
                SetSummaryItems(testBalance);
            }

            return testBalance;
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
                decimal balance = (item.StartBalanceDebit + item.TurnoverDebit)
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

        private async Task<TestBalanceViewModel> GetGeneralBalanceAsync(
            TestBalanceMode mode, TestBalanceParameters parameters,
            Func<TestBalanceItemViewModel, bool> ledgerFilter,
            Func<TestBalanceItemViewModel, bool> subsidiaryFilter = null,
            Func<TestBalanceItemViewModel, bool> detailFilter = null)
        {
            var testBalance = new TestBalanceViewModel();
            var lines = await GetRawBalanceLinesAsync(parameters);
            if (mode == TestBalanceMode.Detail)
            {
                foreach (var lineGroup in GetTurnoverGroups(lines, 2, detailFilter))
                {
                    await AddTwoAndFourColumnBalanceItemAsync(
                        testBalance, lineGroup, lineGroup.Key, parameters.Format, parameters.IsByBranch);
                }
            }

            if (mode == TestBalanceMode.Detail || mode == TestBalanceMode.Subsidiary)
            {
                foreach (var lineGroup in GetTurnoverGroups(lines, 1, subsidiaryFilter))
                {
                    await AddTwoAndFourColumnBalanceItemAsync(
                        testBalance, lineGroup, lineGroup.Key, parameters.Format, parameters.IsByBranch);
                }
            }

            if (mode <= TestBalanceMode.Detail)
            {
                foreach (var lineGroup in GetTurnoverGroups(lines, 0, ledgerFilter))
                {
                    await AddTwoAndFourColumnBalanceItemAsync(
                        testBalance, lineGroup, lineGroup.Key, parameters.Format, parameters.IsByBranch);
                }
            }

            await AddSixAndEightColumnBalanceItemsAsync(testBalance, parameters);
            await ApplyZeroBalanceOptionAsync(testBalance, parameters);
            SetSummaryItems(testBalance);
            return testBalance;
        }

        private async Task<IList<TestBalanceItemViewModel>> GetRawBalanceLinesAsync(
            TestBalanceParameters parameters)
        {
            IList<TestBalanceItemViewModel> lines = null;
            if (parameters.FromDate != null && parameters.ToDate != null)
            {
                Expression<Func<VoucherLine, bool>> mainFilter = art => art.Voucher.Date.IsBetween(
                    parameters.FromDate.Value, parameters.ToDate.Value);
                lines = await GetRawBalanceLinesAsync(mainFilter, parameters);
            }
            else if (parameters.FromNo != null && parameters.ToNo != null)
            {
                Expression<Func<VoucherLine, bool>> mainFilter = art => art.FiscalPeriodId == UserContext.FiscalPeriodId
                    && art.Voucher.No >= parameters.FromNo.Value
                    && art.Voucher.No <= parameters.ToNo.Value;
                lines = await GetRawBalanceLinesAsync(mainFilter, parameters);
            }

            return lines;
        }

        private async Task<IList<TestBalanceItemViewModel>> GetRawBalanceLinesAsync(
            Expression<Func<VoucherLine, bool>> mainFilter, TestBalanceParameters parameters)
        {
            var query = Repository
                .GetAllOperationQuery<VoucherLine>(ViewName.VoucherLine,
                    art => art.Voucher, art => art.Account, art => art.Branch);
            var options = parameters.Options;
            if ((options & TestBalanceOptions.UseClosingVoucher) == 0)
            {
                query = query.Where(art => art.Voucher.Type != (short)VoucherType.ClosingVoucher);
            }

            if ((options & TestBalanceOptions.UseClosingTempVoucher) == 0)
            {
                query = query.Where(art => art.Voucher.Type != (short)VoucherType.ClosingTempAccounts);
            }

            if ((options & TestBalanceOptions.OpeningVoucherAsInitBalance) > 0)
            {
                query = query.Where(art => art.Voucher.Type != (short)VoucherType.OpeningVoucher);
            }

            var lines = await query
                .Where(mainFilter)
                .Select(art => Mapper.Map<TestBalanceItemViewModel>(art))
                .ToListAsync();
            return lines.Apply(parameters.GridOptions, false).ToList();
        }

        private IEnumerable<IGrouping<string, TestBalanceItemViewModel>> GetTurnoverGroups(
            IEnumerable<TestBalanceItemViewModel> lines, int groupLevel,
            Func<TestBalanceItemViewModel, bool> lineFilter)
        {
            int codeLength = GetLevelCodeLength(ViewName.Account, groupLevel);
            return GetGroupByThenByItems(lines.Where(lineFilter),
                    item => item.AccountFullCode.Substring(0, codeLength));
        }

        private async Task<TestBalanceItemViewModel> GetTwoAndFourColumnBalanceItemAsync(
            IEnumerable<TestBalanceItemViewModel> lines, TestBalanceFormat format, string fullCode)
        {
            var first = lines.First();
            var repository = UnitOfWork.GetAsyncRepository<Account>();
            var account = await repository.GetSingleByCriteriaAsync(acc => acc.FullCode == fullCode);
            var balanceItem = new TestBalanceItemViewModel()
            {
                BranchId = first.BranchId,
                BranchName = first.BranchName,
                AccountId = account.Id,
                AccountName = account.Name,
                AccountFullCode = account.FullCode
            };
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
                foreach (var branchGroup in GetGroupByThenByItems(lines, line => line.BranchId))
                {
                    testBalance.Items.Add(await GetTwoAndFourColumnBalanceItemAsync(lines, format, fullCode));
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
                decimal balance = parameters.FromDate.HasValue
                    ? await _report.GetAccountBalanceAsync(item.AccountId, parameters.FromDate.Value)
                    : await _report.GetAccountBalanceAsync(item.AccountId, parameters.FromNo.Value);
                if ((parameters.Options & TestBalanceOptions.OpeningVoucherAsInitBalance) > 0)
                {
                    balance += await _report.GetSpecialVoucherBalanceAsync(
                        VoucherType.OpeningVoucher, item.AccountId);
                }

                item.StartBalanceDebit = Math.Max(0, balance);
                item.StartBalanceCredit = Math.Abs(Math.Min(0, balance));
            }
        }

        private int GetLevelCodeLength(int viewId, int level)
        {
            var fullConfig = Config
                .GetViewTreeConfigByViewAsync(viewId)
                .Result;
            var treeConfig = fullConfig.Current;
            int codeLength = treeConfig.Levels
                .Where(cfg => cfg.No <= level + 1)
                .Sum(cfg => cfg.CodeLength);
            return codeLength;
        }

        private async Task ApplyZeroBalanceOptionAsync(
            TestBalanceViewModel testBalance, TestBalanceParameters parameters)
        {
            if ((parameters.Options & TestBalanceOptions.ShowZeroBalanceItems) > 0)
            {
                // Add items for accounts not used in articles...
                var usedIds = testBalance.Items
                    .Where(item => item.AccountLevel == (short)parameters.Mode)
                    .Select(item => item.AccountId);
                var notUsed = await Repository
                    .GetAllQuery<Account>(ViewName.Account, acc => acc.Branch)
                    .Where(acc => !usedIds.Contains(acc.Id) && acc.Level == (short)parameters.Mode)
                    .Select(acc => new { acc.Id, acc.Name, acc.FullCode, acc.BranchId, BranchName = acc.Branch.Name })
                    .ToListAsync();
                foreach (var notUsedItem in notUsed)
                {
                    testBalance.Items.Add(new TestBalanceItemViewModel()
                    {
                        AccountFullCode = notUsedItem.FullCode,
                        AccountId = notUsedItem.Id,
                        AccountName = notUsedItem.Name,
                        BranchId = notUsedItem.BranchId,
                        BranchName = notUsedItem.BranchName
                    });
                }
            }
            else
            {
                // Remove items with zero end balance...
                var nonZeroItems = testBalance.Items
                    .Where(item => !IsZeroBalanceItem(item))
                    .ToArray();
                testBalance.SetBalanceItems(nonZeroItems);
            }

            var sortedItems = testBalance.Items.OrderBy(item => item.AccountFullCode).ToArray();
            testBalance.SetBalanceItems(sortedItems);
        }

        private IEnumerable<IGrouping<TKey1, TestBalanceItemViewModel>> GetGroupByThenByItems<TKey1>(
            IEnumerable<TestBalanceItemViewModel> lines, Func<TestBalanceItemViewModel, TKey1> firstSelector)
        {
            foreach (var byFirst in lines
                .OrderBy(firstSelector)
                .GroupBy(firstSelector))
            {
                yield return byFirst;
            }
        }

        private readonly ISystemRepository _system;
        private readonly IReportRepository _report;
    }
}
