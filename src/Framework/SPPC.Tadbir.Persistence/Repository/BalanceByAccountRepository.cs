using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPPC.Framework.Extensions;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Model.Finance;
using SPPC.Tadbir.Persistence.Utility;
using SPPC.Tadbir.Values;
using SPPC.Tadbir.ViewModel.Reporting;

namespace SPPC.Tadbir.Persistence.Repository
{
    /// <summary>
    /// عملیات مورد نیاز برای خواندن اطلاعات گزارش مانده به تفکیک حساب را پیاده سازی می کند
    /// </summary>
    public class BalanceByAccountRepository : RepositoryBase, IBalanceByAccountRepository
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="context">امکانات مشترک مورد نیاز را برای عملیات دیتابیسی فراهم می کند</param>
        /// <param name="system">امکانات مورد نیاز در دیتابیس های سیستمی را فراهم می کند</param>
        /// <param name="report">امکانات عمومی مورد نیاز برای گزارشگیری را فراهم می کند</param>
        public BalanceByAccountRepository(IRepositoryContext context, ISystemRepository system, IReportUtility report)
            : base(context)
        {
            _system = system;
            _report = report;
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات گزارش مانده به تفکیک حساب را خوانده و برمیگرداند
        /// </summary>
        /// <param name="parameters">پارامترهای مورد نیاز برای گزارش</param>
        /// <returns></returns>
        public async Task<BalanceByAccountViewModel> GetBalanceByAccountAsync(BalanceByAccountParameters parameters)
        {
            var result = new BalanceByAccountViewModel();
            switch (parameters.ViewId)
            {
                case ViewName.Account:
                    {
                        result = await ReportByAccount(parameters);
                        break;
                    }

                case ViewName.DetailAccount:
                    {
                        break;
                    }

                case ViewName.CostCenter:
                    {
                        break;
                    }

                case ViewName.Project:
                    {
                        break;
                    }

                default:
                    break;
            }

            return result;
        }

        private ISecureRepository Repository
        {
            get { return _system.Repository; }
        }

        private IConfigRepository Config
        {
            get { return _system.Config; }
        }

        private async Task<BalanceByAccountViewModel> ReportByAccount(BalanceByAccountParameters parameters)
        {
            if (parameters.AccountId.HasValue)
            {
                return await GetReportBySelectedAccountAsync(parameters);
            }
            else
            {
                if (!parameters.AccountId.HasValue && parameters.AccountLevel.HasValue)
                {
                    return await GetReportBySelectedAccountlevelAsync(parameters);
                }
                else
                {
                    if (!parameters.AccountId.HasValue && !parameters.AccountLevel.HasValue)
                    {
                        return await GetReportBySelectedViewAsync(parameters);
                    }
                }
            }

            return null;
        }

        private async Task<BalanceByAccountViewModel> GetReportBySelectedAccountAsync(BalanceByAccountParameters parameters)
        {
            var balanceByAccount = new BalanceByAccountViewModel();
            var accountItem = await GetAccountAsync(parameters.AccountId.Value);
            if (accountItem != null)
            {
                var lines = await GetVoucherLinesAsync(parameters);
                lines = lines.Where(line => line.AccountFullCode.StartsWith(accountItem.FullCode)).ToList();

                await AddBalanceByAccountItemsAsync(balanceByAccount, lines, parameters, accountItem.Id);

                //foreach (var lineGroup in lines.GroupBy(line => line.AccountFullCode))
                //{
                //    balanceByAccount.Items.Add(await GetBalanceByAccountItemAsync(lineGroup, lineGroup.Key, parameters));
                //}
            }

            var sortedList = balanceByAccount.Items.OrderBy(item => item.AccountFullCode);
            balanceByAccount.SetItems(sortedList
                .Apply(parameters.GridOptions, false)
                .ToArray());

            SetSummaryItems(balanceByAccount);

            return balanceByAccount;
        }

        private async Task<BalanceByAccountViewModel> GetReportBySelectedAccountlevelAsync(BalanceByAccountParameters parameters)
        {
            var balanceByAccount = new BalanceByAccountViewModel();
            var lines = await GetVoucherLinesAsync(parameters);

            Func<BalanceByAccountItemViewModel, bool> filter;
            int index = 0;
            while (index < parameters.AccountLevel)
            {
                filter = line => line.AccountLevel == index;
                foreach (var lineGroup in _report.GetTurnoverGroups(lines, index, filter))
                {
                    await AddBalanceByAccountItemsAsync(balanceByAccount, lineGroup, parameters);
                }

                index++;
            }

            filter = line => line.AccountLevel >= index;
            foreach (var lineGroup in _report.GetTurnoverGroups(lines, index, filter))
            {
                await AddBalanceByAccountItemsAsync(balanceByAccount, lineGroup, parameters);
            }

            var sortedList = balanceByAccount.Items.OrderBy(item => item.AccountFullCode);
            balanceByAccount.SetItems(sortedList
                .Apply(parameters.GridOptions, false)
                .ToArray());

            SetSummaryItems(balanceByAccount);

            return balanceByAccount;
        }

        private async Task<BalanceByAccountViewModel> GetReportBySelectedViewAsync(BalanceByAccountParameters parameters)
        {
            var balanceByAccount = new BalanceByAccountViewModel();
            var lines = await GetVoucherLinesAsync(parameters);

            Func<BalanceByAccountItemViewModel, bool> filter;
            int index = 0;
            while (index < GetMaxDepth(parameters.ViewId))
            {
                filter = line => line.AccountLevel == index;
                foreach (var lineGroup in _report.GetTurnoverGroups(lines, index, filter))
                {
                    await AddBalanceByAccountItemsAsync(balanceByAccount, lineGroup, parameters);
                }

                index++;
            }

            var sortedList = balanceByAccount.Items.OrderBy(item => item.AccountFullCode);
            balanceByAccount.SetItems(sortedList
                .Apply(parameters.GridOptions, false)
                .ToArray());

            SetSummaryItems(balanceByAccount);

            return balanceByAccount;
        }

        private void SetSummaryItems(BalanceByAccountViewModel balanceByAccount)
        {
            balanceByAccount.Total.StartBalance = balanceByAccount.Items.Sum(item => item.StartBalance);
            balanceByAccount.Total.Credit = balanceByAccount.Items.Sum(item => item.Credit);
            balanceByAccount.Total.Debit = balanceByAccount.Items.Sum(item => item.Debit);
            balanceByAccount.Total.EndBalance = balanceByAccount.Items.Sum(item => item.EndBalance);
        }

        private async Task<IList<BalanceByAccountItemViewModel>> GetVoucherLinesAsync(BalanceByAccountParameters parameters)
        {
            var query = Repository
                .GetAllOperationQuery<VoucherLine>(ViewName.VoucherLine,
                line => line.Voucher,
                line => line.Branch,
                line => line.Account);

            var options = (TestBalanceOptions)parameters.Options;
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

            IList<BalanceByAccountItemViewModel> lines = null;
            if (parameters.IsByDate)
            {
                lines = await GetRawReportByDateLinesAsync(
                    query, parameters.FromDate.Value, parameters.ToDate.Value, parameters.GridOptions);
            }
            else
            {
                lines = await GetRawReportByNumberLinesAsync(
                     query, parameters.FromNo.Value, parameters.ToNo.Value, parameters.GridOptions);
            }

            return lines;
        }

        private async Task<List<BalanceByAccountItemViewModel>> GetRawReportByDateLinesAsync(IQueryable<VoucherLine> query,
            DateTime from, DateTime to, GridOptions gridOptions)
        {
            var lines = await query
                .Where(art => art.Voucher.Date.IsBetween(from, to))
                .OrderBy(art => art.Voucher.Date)
                    .ThenBy(art => art.Voucher.No)
                .Select(art => Mapper.Map<BalanceByAccountItemViewModel>(art))
                .ToListAsync();
            return lines
                .ApplyQuickFilter(gridOptions)
                .ToList();
        }

        private async Task<List<BalanceByAccountItemViewModel>> GetRawReportByNumberLinesAsync(IQueryable<VoucherLine> query,
           int from, int to, GridOptions gridOptions)
        {
            var lines = await query
                .Where(art => art.Voucher.No >= from
                    && art.Voucher.No <= to)
                .OrderBy(art => art.Voucher.No)
                .Select(art => Mapper.Map<BalanceByAccountItemViewModel>(art))
                .ToListAsync();
            return lines
                .ApplyQuickFilter(gridOptions)
                .ToList();
        }

        private async Task AddBalanceByAccountItemsAsync(
            BalanceByAccountViewModel balanceByAccount,
            IEnumerable<BalanceByAccountItemViewModel> lines,
            BalanceByAccountParameters parameters,
            int? accId = null)
        {
            if (parameters.IsByBranch)
            {
                foreach (var branchGroup in lines.GroupBy(item => item.BranchId))
                {
                    var accountId = accId.HasValue ? accId.Value : branchGroup.First().AccountId;
                    balanceByAccount.Items.Add(await GetBalanceByAccountItemAsync(branchGroup, accountId, parameters));
                }
            }
            else
            {
                var accountId = accId.HasValue ? accId.Value : lines.First().AccountId;
                balanceByAccount.Items.Add(await GetBalanceByAccountItemAsync(lines, accountId, parameters));
            }
        }

        private async Task<BalanceByAccountItemViewModel> GetBalanceByAccountItemAsync(
            IEnumerable<BalanceByAccountItemViewModel> lines, int accountId, BalanceByAccountParameters parameters)
        {
            var first = lines.First();
            first.StartBalance = await GetInitialBalanceAsync(accountId, parameters);
            first.Debit = lines.Sum(line => line.Debit);
            first.Credit = lines.Sum(line => line.Credit);
            first.EndBalance = first.StartBalance + first.Debit - first.Credit;
            return first;
        }

        private async Task<decimal> GetInitialBalanceAsync(int accountId, BalanceByAccountParameters parameters)
        {
            decimal balance = parameters.IsByDate
                ? await GetBalanceAsync(accountId, parameters.FromDate.Value)
                : await GetBalanceAsync(accountId, parameters.FromNo.Value);

            return balance;
        }

        private async Task<decimal> GetBalanceAsync(int accountId, DateTime date)
        {
            decimal balance = 0.0M;
            var account = await GetAccountAsync(accountId);
            if (account != null)
            {
                balance = await GetItemBalanceAsync(
                    date, line => line.Account.FullCode.StartsWith(account.FullCode));
            }

            return balance;
        }

        private async Task<decimal> GetBalanceAsync(int accountId, int number)
        {
            decimal balance = 0.0M;
            var account = await GetAccountAsync(accountId);
            if (account != null)
            {
                balance = await GetItemBalanceAsync(
                    number, line => line.Account.FullCode.StartsWith(account.FullCode));
            }

            return balance;
        }

        private async Task<Account> GetAccountAsync(int accountId)
        {
            var repository = UnitOfWork.GetAsyncRepository<Account>();
            return await repository.GetByIDAsync(accountId);
        }

        private async Task<decimal> GetItemBalanceAsync(
           DateTime date, Expression<Func<VoucherLine, bool>> itemCriteria)
        {
            return await GetBalanceAsync(
                line => line.Voucher.Date.CompareWith(date) < 0, itemCriteria);
        }

        private async Task<decimal> GetItemBalanceAsync(
           int number, Expression<Func<VoucherLine, bool>> itemCriteria)
        {
            return await GetBalanceAsync(
                line => line.Voucher.No < number, itemCriteria);
        }

        private async Task<decimal> GetBalanceAsync(
            Expression<Func<VoucherLine, bool>> lineCriteria,
            Expression<Func<VoucherLine, bool>> itemCriteria)
        {
            return await Repository
                .GetAllOperationQuery<VoucherLine>(ViewName.VoucherLine)
                .Where(line => line.FiscalPeriodId == UserContext.FiscalPeriodId)
                .Where(lineCriteria)
                .Where(itemCriteria)
                .Select(line => line.Debit - line.Credit)
                .SumAsync();
        }

        private int GetMaxDepth(int viewId)
        {
            var fullConfig = Config
               .GetViewTreeConfigByViewAsync(viewId)
               .Result;

            return fullConfig.Current.Levels
                .Where(f => f.IsUsed)
                .Count();
        }

        private readonly ISystemRepository _system;
        private readonly IReportUtility _report;
    }
}
