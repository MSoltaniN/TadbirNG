using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPPC.Framework.Extensions;
using SPPC.Framework.Mapper;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Model.Finance;
using SPPC.Tadbir.Values;
using SPPC.Tadbir.ViewModel.Auth;
using SPPC.Tadbir.ViewModel.Reporting;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای خواندن اطلاعات گزارش تراز آزمایشی را پیاده سازی می کند
    /// </summary>
    public class TestBalanceRepository : ITestBalanceRepository
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="unitOfWork">پیاده سازی اینترفیس واحد کاری برای انجام عملیات دیتابیسی</param>
        /// <param name="mapper">نگاشت مورد استفاده برای تبدیل کلاس های مدل اطلاعاتی</param>
        /// <param name="repository">عملیات مورد نیاز برای اعمال دسترسی امنیتی در سطح سطرهای اطلاعاتی را تعریف می کند</param>
        /// <param name="reportRepository">امکانات عمومی و مشترک در گزارش های مالی را تعریف می کند</param>
        /// <param name="configRepository">امکان خواندن تنظیمات برنامه را فراهم می کند</param>
        public TestBalanceRepository(
            IAppUnitOfWork unitOfWork, IDomainMapper mapper, ISecureRepository repository,
            IReportRepository reportRepository, IConfigRepository configRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _repository = repository;
            _reportRepository = reportRepository;
            _configRepository = configRepository;
        }

        /// <summary>
        /// اطلاعات محیطی و امنیتی کاربر جاری برنامه را برای کنترل قواعد کاری برنامه تنظیم می کند
        /// </summary>
        /// <param name="userContext">اطلاعات محیطی و امنیتی کاربر جاری برنامه</param>
        public void SetCurrentContext(UserContextViewModel userContext)
        {
            _currentContext = userContext;
            _repository.SetCurrentContext(userContext);
            _reportRepository.SetCurrentContext(userContext);
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
            var repository = _unitOfWork.GetAsyncRepository<Account>();
            var account = await repository.GetByIDAsync(accountId, acc => acc.Children);
            if (account != null)
            {
                int groupLevel = account.Level + 1;
                Func<TestBalanceItemViewModel, bool> lineFilter = line => line.AccountLevel >= groupLevel;
                IEnumerable<TestBalanceItemViewModel> lines = await GetRawBalanceLinesAsync(parameters);
                lines = lines.Where(line => line.AccountFullCode.StartsWith(account.FullCode));
                foreach (var lineGroup in GetTurnoverGroups(lines, groupLevel, lineFilter))
                {
                    testBalance.Items.Add(await GetTwoAndFourColumnBalanceItemAsync(
                        lineGroup, lineGroup.Key));
                }

                if (parameters.Format >= TestBalanceFormat.SixColumn)
                {
                    await AddInitialBalancesAsync(testBalance, parameters);
                    UpdateEndBalances(testBalance);
                }

                if (parameters.Format >= TestBalanceFormat.EightColumn)
                {
                    AddOperationSums(testBalance);
                }

                SetSummaryItems(testBalance);
            }

            return testBalance;
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
            testBalance.Total.StartBalanceCredit = testBalance.Items.Sum(item => item.StartBalanceDebit);
            testBalance.Total.TurnoverDebit = testBalance.Items.Sum(item => item.TurnoverDebit);
            testBalance.Total.TurnoverCredit = testBalance.Items.Sum(item => item.TurnoverCredit);
            testBalance.Total.OperationSumDebit = testBalance.Items.Sum(item => item.OperationSumDebit);
            testBalance.Total.OperationSumCredit = testBalance.Items.Sum(item => item.OperationSumCredit);
            testBalance.Total.CorrectionsDebit = testBalance.Items.Sum(item => item.CorrectionsDebit);
            testBalance.Total.CorrectionsCredit = testBalance.Items.Sum(item => item.CorrectionsCredit);
            testBalance.Total.EndBalanceDebit = testBalance.Items.Sum(item => item.EndBalanceDebit);
            testBalance.Total.EndBalanceCredit = testBalance.Items.Sum(item => item.EndBalanceCredit);
        }

        private async Task<TestBalanceViewModel> GetGeneralBalanceAsync(
            TestBalanceMode mode, TestBalanceParameters parameters, Func<TestBalanceItemViewModel, bool> ledgerFilter,
            Func<TestBalanceItemViewModel, bool> subsidiaryFilter = null, Func<TestBalanceItemViewModel, bool> detailFilter = null)
        {
            var testBalance = new TestBalanceViewModel();
            var lines = await GetRawBalanceLinesAsync(parameters);
            if (mode == TestBalanceMode.Detail)
            {
                foreach (var lineGroup in GetTurnoverGroups(lines, 2, detailFilter))
                {
                    testBalance.Items.Add(await GetTwoAndFourColumnBalanceItemAsync(
                        lineGroup, lineGroup.Key));
                }
            }

            if (mode == TestBalanceMode.Detail || mode == TestBalanceMode.Subsidiary)
            {
                foreach (var lineGroup in GetTurnoverGroups(lines, 1, subsidiaryFilter))
                {
                    testBalance.Items.Add(await GetTwoAndFourColumnBalanceItemAsync(
                        lineGroup, lineGroup.Key));
                }
            }

            if (mode <= TestBalanceMode.Detail)
            {
                foreach (var lineGroup in GetTurnoverGroups(lines, 0, ledgerFilter))
                {
                    testBalance.Items.Add(await GetTwoAndFourColumnBalanceItemAsync(
                        lineGroup, lineGroup.Key));
                }
            }

            var sortedItems = testBalance.Items.OrderBy(item => item.AccountFullCode).ToArray();
            testBalance.SetBalanceItems(sortedItems);
            if (parameters.Format >= TestBalanceFormat.SixColumn)
            {
                await AddInitialBalancesAsync(testBalance, parameters);
                UpdateEndBalances(testBalance);
            }

            if (parameters.Format >= TestBalanceFormat.EightColumn)
            {
                AddOperationSums(testBalance);
            }

            SetSummaryItems(testBalance);
            return testBalance;
        }

        private async Task<IList<TestBalanceItemViewModel>> GetRawBalanceLinesAsync(TestBalanceParameters parameters)
        {
            IList<TestBalanceItemViewModel> lines = null;
            if (parameters.FromDate != null && parameters.ToDate != null)
            {
                lines = await GetRawBalanceByDateLinesAsync(
                    parameters.FromDate.Value, parameters.ToDate.Value, parameters.GridOptions);
            }
            else if (parameters.FromNo != null && parameters.ToNo != null)
            {
                lines = await GetRawBalanceByNoLinesAsync(
                    parameters.FromNo.Value, parameters.ToNo.Value, parameters.GridOptions);
            }

            return lines;
        }

        private async Task<IList<TestBalanceItemViewModel>> GetRawBalanceByDateLinesAsync(
            DateTime from, DateTime to, GridOptions gridOptions)
        {
            var lines = await _repository
                .GetAllOperationQuery<VoucherLine>(ViewName.VoucherLine,
                    art => art.Voucher, art => art.Account, art => art.Branch)
                .Where(art => art.Voucher.Date.IsBetween(from, to))
                .Select(art => _mapper.Map<TestBalanceItemViewModel>(art))
                .ToListAsync();
            return lines.Apply(gridOptions, false).ToList();
        }

        private async Task<IList<TestBalanceItemViewModel>> GetRawBalanceByNoLinesAsync(
            int from, int to, GridOptions gridOptions)
        {
            var lines = await _repository
                .GetAllOperationQuery<VoucherLine>(ViewName.VoucherLine,
                    art => art.Voucher, art => art.Account, art => art.Branch)
                .Where(art => art.FiscalPeriodId == _currentContext.FiscalPeriodId
                    && art.Voucher.No >= from
                    && art.Voucher.No <= to)
                .Select(art => _mapper.Map<TestBalanceItemViewModel>(art))
                .ToListAsync();
            return lines.Apply(gridOptions, false).ToList();
        }

        private IEnumerable<IGrouping<string, TestBalanceItemViewModel>> GetTurnoverGroups(
            IEnumerable<TestBalanceItemViewModel> lines, int groupLevel,
            Func<TestBalanceItemViewModel, bool> lineFilter)
        {
            int codeLength = GetLevelCodeLength(ViewName.Account, groupLevel);
            var turnoverGroups = lines
                .Where(lineFilter)
                .OrderBy(art => art.AccountFullCode)
                .GroupBy(art => art.AccountFullCode.Substring(0, codeLength));
            return turnoverGroups;
        }

        private async Task<TestBalanceItemViewModel> GetTwoAndFourColumnBalanceItemAsync(
            IEnumerable<TestBalanceItemViewModel> lines, string fullCode)
        {
            var line = lines.First();
            var repository = _unitOfWork.GetAsyncRepository<Account>();
            var account = await repository.GetSingleByCriteriaAsync(acc => acc.FullCode == fullCode);
            var balanceItem = new TestBalanceItemViewModel()
            {
                BranchId = line.BranchId,
                BranchName = line.BranchName,
                AccountId = account.Id,
                AccountName = account.Name,
                AccountFullCode = fullCode,
                TurnoverDebit = lines.Sum(item => item.TurnoverDebit),
                TurnoverCredit = lines.Sum(item => item.TurnoverCredit)
            };
            decimal balance = balanceItem.TurnoverDebit - balanceItem.TurnoverCredit;
            balanceItem.EndBalanceDebit = Math.Max(0, balance);
            balanceItem.EndBalanceCredit = Math.Abs(Math.Min(0, balance));
            return balanceItem;
        }

        private async Task AddInitialBalancesAsync(TestBalanceViewModel testBalance, TestBalanceParameters parameters)
        {
            foreach (var item in testBalance.Items)
            {
                decimal balance = parameters.FromDate.HasValue
                    ? await _reportRepository.GetAccountBalanceAsync(item.AccountId, parameters.FromDate.Value)
                    : await _reportRepository.GetAccountBalanceAsync(item.AccountId, parameters.FromNo.Value);
                item.StartBalanceDebit = Math.Max(0, balance);
                item.StartBalanceCredit = Math.Abs(Math.Min(0, balance));
            }
        }

        private int GetLevelCodeLength(int viewId, int level)
        {
            var fullConfig = _configRepository
                .GetViewTreeConfigByViewAsync(viewId)
                .Result;
            var treeConfig = fullConfig.Current;
            int codeLength = treeConfig.Levels
                .Where(cfg => cfg.No <= level + 1)
                .Sum(cfg => cfg.CodeLength);
            return codeLength;
        }

        private readonly IAppUnitOfWork _unitOfWork;
        private readonly IDomainMapper _mapper;
        private readonly ISecureRepository _repository;
        private readonly IConfigRepository _configRepository;
        private IReportRepository _reportRepository;
        private UserContextViewModel _currentContext;
    }
}
