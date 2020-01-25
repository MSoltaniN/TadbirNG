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
                return await GetReportByAllAccountAsync(parameters);
            }
        }

        private async Task<BalanceByAccountViewModel> GetReportBySelectedAccountAsync(BalanceByAccountParameters parameters)
        {
            var balanceByAccount = new BalanceByAccountViewModel();
            var accountItem = await GetAccountAsync(parameters.AccountId.Value);
            if (accountItem != null)
            {
                var lines = await GetVoucherLinesAsync(parameters);

                lines = lines.Where(line => line.AccountFullCode.StartsWith(accountItem.FullCode)).ToList();

                //Func<BalanceByAccountItemViewModel, bool> filter;


                //int groupLevel = parameters.AccountLevel.Value + 1;
                //filter = line => line.AccountLevel >= groupLevel;
                //foreach (var lineGroup in _report.GetTurnoverGroups(lines, groupLevel, filter))
                //{
                //    await AddBalanceByAccountItemsAsync(balanceByAccount, lineGroup, parameters, lineGroup.Key);
                //}


                Func<BalanceByAccountItemViewModel, bool> filter;
                int index = 0;
                while (index < parameters.AccountLevel)
                {
                    filter = line => line.AccountLevel == index;
                    foreach (var lineGroup in _report.GetTurnoverGroups(lines, index, filter))
                    {
                        await AddBalanceByAccountItemsAsync(balanceByAccount, lineGroup, parameters, lineGroup.Key);
                    }

                    index++;
                }

                filter = line => line.AccountLevel >= index;
                foreach (var lineGroup in _report.GetTurnoverGroups(lines, index, filter))
                {
                    await AddBalanceByAccountItemsAsync(balanceByAccount, lineGroup, parameters, lineGroup.Key);
                }





                //await AddBalanceByAccountItemsAsync(balanceByAccount, lines, parameters, accountItem.FullCode);

                //lines = await GetFilteredLinesAsync(lines, parameters);

                //foreach (var item in SeparationLinesAsync(lines, parameters))
                //{
                //    await AddBalanceByAccountItemsAsync(balanceByAccount, item, parameters, accountItem.FullCode);
                //}



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

        private async Task<BalanceByAccountViewModel> GetReportByAllAccountAsync(BalanceByAccountParameters parameters)
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
                    await AddBalanceByAccountItemsAsync(balanceByAccount, lineGroup, parameters, lineGroup.Key);
                }

                index++;
            }

            filter = line => line.AccountLevel >= index;
            foreach (var lineGroup in _report.GetTurnoverGroups(lines, index, filter))
            {
                await AddBalanceByAccountItemsAsync(balanceByAccount, lineGroup, parameters, lineGroup.Key);
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
                line => line.Branch);

            query = IncludeLineReference(query, parameters);

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

        private IQueryable<VoucherLine> IncludeLineReference(IQueryable<VoucherLine> query, BalanceByAccountParameters parameters)
        {
            if (parameters.ViewId == ViewName.Account || parameters.AccountLevel.HasValue)
            {
                query = query.Include(line => line.Account);
            }

            if (parameters.ViewId == ViewName.DetailAccount || parameters.DetailAccountLevel.HasValue)
            {
                query = query.Include(line => line.DetailAccount);
            }

            if (parameters.ViewId == ViewName.CostCenter || parameters.CostCenterLevel.HasValue)
            {
                query = query.Include(line => line.CostCenter);
            }

            if (parameters.ViewId == ViewName.Project || parameters.ProjectLevel.HasValue)
            {
                query = query.Include(line => line.Project);
            }

            return query;
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
            string fullCode)
        {
            if (parameters.IsByBranch)
            {
                foreach (var branchGroup in lines.GroupBy(item => item.BranchId))
                {
                    balanceByAccount.Items.Add(await GetBalanceByAccountItemAsync(branchGroup, parameters, fullCode));
                }
            }
            else
            {
                balanceByAccount.Items.Add(await GetBalanceByAccountItemAsync(lines, parameters, fullCode));
            }
        }

        private async Task<BalanceByAccountItemViewModel> GetBalanceByAccountItemAsync(
            IEnumerable<BalanceByAccountItemViewModel> lines, BalanceByAccountParameters parameters, string fullCode)
        {
            var balanceByAccountItem = await GetAccountFromVoucherLineAsync(lines.First(), fullCode);

            balanceByAccountItem.StartBalance = await GetInitialBalanceAsync(balanceByAccountItem.AccountId, parameters);
            balanceByAccountItem.Debit = lines.Sum(line => line.Debit);
            balanceByAccountItem.Credit = lines.Sum(line => line.Credit);
            balanceByAccountItem.EndBalance = balanceByAccountItem.StartBalance + balanceByAccountItem.Debit - balanceByAccountItem.Credit;
            return balanceByAccountItem;
        }

        private async Task<IList<BalanceByAccountItemViewModel>> GetFilteredLinesAsync(
            IEnumerable<BalanceByAccountItemViewModel> lines,
            BalanceByAccountParameters parameters)
        {
            if (parameters.ViewId != ViewName.Account)
            {
                if (parameters.AccountId.HasValue)
                {
                    var accountItem = await GetAccountAsync(parameters.AccountId.Value);
                    lines = lines.Where(line => line.AccountFullCode.StartsWith(accountItem.FullCode));
                }

                if (!parameters.AccountId.HasValue && parameters.AccountLevel.HasValue)
                {
                    lines = lines.Where(line => line.AccountLevel == parameters.AccountLevel.Value);
                }
            }

            //if (parameters.ViewId != ViewName.DetailAccount)
            //{
            //    if (parameters.DetailAccountId.HasValue)
            //    {
            //        var detailAccountItem = await GetDetailAccountAsync(parameters.DetailAccountId.Value);
            //        lines = lines.Where(line => line.DetailAccountFullCode.StartsWith(detailAccountItem.FullCode));
            //    }

            //    if (!parameters.DetailAccountId.HasValue && parameters.DetailAccountLevel.HasValue)
            //    {
            //        lines = lines.Where(line => line.DetailAccountLevel == parameters.DetailAccountLevel.Value);
            //    }
            //}

            //if (parameters.ViewId != ViewName.CostCenter)
            //{
            //    if (parameters.CostCenterId.HasValue)
            //    {
            //        var costcenterItem = await GetCostCenterAsync(parameters.CostCenterId.Value);
            //        lines = lines.Where(line => line.CostCenterFullCode.StartsWith(costcenterItem.FullCode));
            //    }

            //    if (!parameters.CostCenterId.HasValue && parameters.CostCenterLevel.HasValue)
            //    {
            //        lines = lines.Where(line => line.CostCenterLevel == parameters.CostCenterLevel.Value);
            //    }
            //}

            //if (parameters.ViewId != ViewName.Project)
            //{
            //    if (parameters.ProjectId.HasValue)
            //    {
            //        var projectItem = await GetProjectAsync(parameters.ProjectId.Value);
            //        lines = lines.Where(line => line.ProjectFullCode.StartsWith(projectItem.FullCode));
            //    }

            //    if (!parameters.ProjectId.HasValue && parameters.ProjectLevel.HasValue)
            //    {
            //        lines = lines.Where(line => line.ProjectLevel == parameters.ProjectLevel.Value);
            //    }
            //}

            return lines.ToList();
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

        private async Task<DetailAccount> GetDetailAccountAsync(int dAccountId)
        {
            var repository = UnitOfWork.GetAsyncRepository<DetailAccount>();
            return await repository.GetByIDAsync(dAccountId);
        }

        private async Task<CostCenter> GetCostCenterAsync(int cCenterId)
        {
            var repository = UnitOfWork.GetAsyncRepository<CostCenter>();
            return await repository.GetByIDAsync(cCenterId);
        }

        private async Task<Project> GetProjectAsync(int projectId)
        {
            var repository = UnitOfWork.GetAsyncRepository<Project>();
            return await repository.GetByIDAsync(projectId);
        }

        private async Task<BalanceByAccountItemViewModel> GetAccountFromVoucherLineAsync(BalanceByAccountItemViewModel line, string fullCode)
        {
            var repository = UnitOfWork.GetAsyncRepository<Account>();
            var account = await repository.GetSingleByCriteriaAsync(acc => acc.FullCode == fullCode);
            return new BalanceByAccountItemViewModel()
            {
                BranchId = line.BranchId,
                BranchName = line.BranchName,
                AccountId = account.Id,
                AccountName = account.Name,
                AccountFullCode = account.FullCode,
                AccountLevel = account.Level
            };
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
