using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPPC.Framework.Extensions;
using SPPC.Framework.Mapper;
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
            var testBalance = new TestBalanceViewModel();
            Expression<Func<VoucherLine, bool>> lineFilter = line => true;
            if (parameters.Format == TestBalanceFormat.TenColumn)
            {
                lineFilter = line => line.TypeId != (short)VoucherLineType.Revised;
            }

            var lines = await GetRawBalanceLinesAsync(parameters, lineFilter);
            foreach (var lineGroup in GetTurnoverGroups(lines, ViewName.Account, 0))
            {
                testBalance.Items.Add(GetTwoAndFourColumnBalanceItem(lineGroup, ViewName.Account));
            }

            if (parameters.Format >= TestBalanceFormat.SixColumn)
            {
                await AddInitialBalancesAsync(testBalance, parameters.FromDate.Value);
            }

            if (parameters.Format >= TestBalanceFormat.EightColumn)
            {
                AddOperationSums(testBalance);
            }

            if (parameters.Format == TestBalanceFormat.TenColumn)
            {
                lineFilter = line => line.TypeId == (short)VoucherLineType.Revised;
                lines = await GetRawBalanceLinesAsync(parameters, lineFilter);
                foreach (var lineGroup in GetTurnoverGroups(lines, ViewName.Account, 0))
                {
                    var balanceItem = testBalance.Items
                        .Where(item => item.AccountFullCode == lineGroup.Key)
                        .FirstOrDefault();
                    if (balanceItem != null)
                    {
                        balanceItem.CorrectionsDebit = lineGroup.Sum(line => line.Debit);
                        balanceItem.CorrectionsCredit = lineGroup.Sum(line => line.Credit);
                    }
                }
            }

            SetItemsSummary(testBalance);
            return testBalance;
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات گزارش تراز آزمایشی در سطح معین را خوانده و برمی گرداند
        /// </summary>
        /// <param name="parameters">پارامترهای مورد نیاز برای گزارش</param>
        /// <returns>اطلاعات گزارش با توجه به پارامترهای داده شده</returns>
        public async Task<TestBalanceViewModel> GetSubsidiaryBalanceAsync(TestBalanceParameters parameters)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات گزارش تراز آزمایشی در سطح تفصیلی را خوانده و برمی گرداند
        /// </summary>
        /// <param name="parameters">پارامترهای مورد نیاز برای گزارش</param>
        /// <returns>اطلاعات گزارش با توجه به پارامترهای داده شده</returns>
        public async Task<TestBalanceViewModel> GetDetailBalanceAsync(TestBalanceParameters parameters)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات گزارش تراز آزمایشی معین های یک حساب کل را خوانده و برمی گرداند
        /// </summary>
        /// <param name="accountId">شناسه دیتابیسی یکی از حساب های کل</param>
        /// <param name="parameters">پارامترهای مورد نیاز برای گزارش</param>
        /// <returns>اطلاعات گزارش با توجه به پارامترهای داده شده</returns>
        public async Task<TestBalanceViewModel> GetLedgerItemsBalanceAsync(int accountId, TestBalanceParameters parameters)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات گزارش تراز آزمایشی تفصیلی های یک حساب معین را خوانده و برمی گرداند
        /// </summary>
        /// <param name="accountId">شناسه دیتابیسی یکی از حساب های معین</param>
        /// <param name="parameters">پارامترهای مورد نیاز برای گزارش</param>
        /// <returns>اطلاعات گزارش با توجه به پارامترهای داده شده</returns>
        public async Task<TestBalanceViewModel> GetSubsidiaryItemsBalanceAsync(int accountId, TestBalanceParameters parameters)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات گزارش تراز آزمایشی برای یکی از سطوح تفصیلی شناور را خوانده و برمی گرداند
        /// </summary>
        /// <param name="level">شماره سطح مورد نظر از تفصیلی های شناور برای گزارش گیری</param>
        /// <param name="parameters">پارامترهای مورد نیاز برای گزارش</param>
        /// <returns>اطلاعات گزارش با توجه به پارامترهای داده شده</returns>
        public async Task<TestBalanceViewModel> GetDetailAccountLevelBalanceAsync(int level, TestBalanceParameters parameters)
        {
            throw new NotImplementedException();
        }

        private static void AddOperationSums(TestBalanceViewModel testBalance)
        {
            foreach (var item in testBalance.Items)
            {
                item.OperationSumDebit = item.StartBalanceDebit + item.TurnoverDebit;
                item.OperationSumCredit = item.StartBalanceCredit + item.TurnoverCredit;
            }
        }

        private static void SetItemsSummary(TestBalanceViewModel testBalance)
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

        private async Task<IList<VoucherLine>> GetRawBalanceLinesAsync(
            TestBalanceParameters parameters, Expression<Func<VoucherLine, bool>> lineFilter)
        {
            IList<VoucherLine> lines = null;
            if (parameters.FromDate != null && parameters.ToDate != null)
            {
                lines = await GetRawBalanceByDateLinesAsync(
                    parameters.FromDate.Value, parameters.ToDate.Value, lineFilter);
            }
            else if (parameters.FromNo != null && parameters.ToNo != null)
            {
                lines = await GetRawBalanceByNoLinesAsync(
                    parameters.FromNo.Value, parameters.ToNo.Value, lineFilter);
            }

            return lines;
        }

        private async Task<IList<VoucherLine>> GetRawBalanceByDateLinesAsync(
            DateTime from, DateTime to, Expression<Func<VoucherLine, bool>> lineFilter)
        {
            return await _repository
                .GetAllOperationQuery<VoucherLine>(ViewName.VoucherLine,
                    art => art.Voucher, art => art.Account, art => art.Branch)
                .Where(art => art.Voucher.Date.IsBetween(from, to))
                .Where(lineFilter)
                .ToListAsync();
        }

        private async Task<IList<VoucherLine>> GetRawBalanceByNoLinesAsync(
            int from, int to, Expression<Func<VoucherLine, bool>> lineFilter)
        {
            return await _repository
                .GetAllOperationQuery<VoucherLine>(ViewName.VoucherLine,
                    art => art.Voucher, art => art.Account, art => art.Branch)
                .Where(art => art.FiscalPeriodId == _currentContext.FiscalPeriodId
                    && art.Voucher.No >= from
                    && art.Voucher.No <= to)
                .Where(lineFilter)
                .ToListAsync();
        }

        private IEnumerable<IGrouping<string, VoucherLine>> GetTurnoverGroups(
            IEnumerable<VoucherLine> lines, int viewId, int groupLevel)
        {
            int codeLength = GetLevelCodeLength(viewId, groupLevel);
            var turnoverGroups = lines
                .OrderBy(art => art.Account.FullCode)
                .GroupBy(art => art.Account.FullCode.Substring(0, codeLength));
            return turnoverGroups;
        }

        private TestBalanceItemViewModel GetTwoAndFourColumnBalanceItem(
            IEnumerable<VoucherLine> lines, int viewId)
        {
            var line = lines.First();
            var balanceItem = new TestBalanceItemViewModel()
            {
                AccountId = (viewId == ViewName.Account)
                    ? line.AccountId
                    : line.DetailId.Value,
                AccountName = (viewId == ViewName.Account)
                    ? line.Account.Name
                    : line.DetailAccount.Name,
                AccountFullCode = (viewId == ViewName.Account)
                    ? line.Account.FullCode
                    : line.DetailAccount.FullCode,
                TurnoverDebit = lines.Sum(item => item.Debit),
                TurnoverCredit = lines.Sum(item => item.Credit)
            };
            decimal balance = balanceItem.TurnoverDebit - balanceItem.TurnoverCredit;
            balanceItem.EndBalanceDebit = Math.Max(0, balance);
            balanceItem.EndBalanceCredit = Math.Abs(Math.Min(0, balance));
            return balanceItem;
        }

        private async Task AddInitialBalancesAsync(TestBalanceViewModel testBalance, DateTime date)
        {
            foreach (var item in testBalance.Items)
            {
                decimal balance = await _reportRepository.GetAccountBalanceAsync(item.AccountId, date);
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
