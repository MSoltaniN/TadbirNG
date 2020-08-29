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
using SPPC.Tadbir.ViewModel.Reporting;

namespace SPPC.Tadbir.Persistence.Repository
{
    /// <summary>
    /// عملیات مورد نیاز برای خواندن اطلاعات گزارش مانده به تفکیک حساب را پیاده سازی می کند
    /// </summary>
    public class BalanceByAccountRepository : LoggingRepository<Account, object>, IBalanceByAccountRepository
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="context">امکانات مشترک مورد نیاز را برای عملیات دیتابیسی فراهم می کند</param>
        /// <param name="system">امکانات مورد نیاز در دیتابیس های سیستمی را فراهم می کند</param>
        /// <param name="report">امکانات عمومی مورد نیاز برای گزارشگیری را فراهم می کند</param>
        public BalanceByAccountRepository(IRepositoryContext context, ISystemRepository system,
            IReportUtility report)
            : base(context, system.Logger)
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
                case ViewId.Account:
                    {
                        result = await ReportByAccountAsync(parameters);
                        break;
                    }

                case ViewId.DetailAccount:
                    {
                        result = await ReportByDetailAccountAsync(parameters);
                        break;
                    }

                case ViewId.CostCenter:
                    {
                        result = await ReportByCostCenterAsync(parameters);
                        break;
                    }

                case ViewId.Project:
                    {
                        result = await ReportByProjectAsync(parameters);
                        break;
                    }

                default:
                    break;
            }

            return result;
        }

        internal override OperationSourceId OperationSource
        {
            get { return OperationSourceId.BalanceByAccount; }
        }

        private ISecureRepository Repository
        {
            get { return _system.Repository; }
        }

        private IConfigRepository Config
        {
            get { return _system.Config; }
        }

        private static void SetSummaryItems(BalanceByAccountViewModel balanceByAccount)
        {
            balanceByAccount.Total.StartBalance = balanceByAccount.Items.Sum(item => item.StartBalance);
            balanceByAccount.Total.Credit = balanceByAccount.Items.Sum(item => item.Credit);
            balanceByAccount.Total.Debit = balanceByAccount.Items.Sum(item => item.Debit);
            balanceByAccount.Total.EndBalance = balanceByAccount.Items.Sum(item => item.EndBalance);
        }

        private static void SortAndSetItems(BalanceByAccountViewModel balanceByAccount, GridOptions gridOptions)
        {
            var sortedList = balanceByAccount.Items
                .OrderBy(item => item.AccountFullCode)
                .ThenBy(item => item.DetailAccountFullCode)
                .ThenBy(item => item.CostCenterFullCode)
                .ThenBy(item => item.ProjectFullCode);

            balanceByAccount.SetItems(sortedList
                .Apply(gridOptions, false)
                .ToArray());
        }

        private static IQueryable<VoucherLine> IncludeLineReference(IQueryable<VoucherLine> query, BalanceByAccountParameters parameters)
        {
            if (parameters.IsSelectedAccount)
            {
                query = query.Include(line => line.Account);
            }

            if (parameters.IsSelectedDetailAccount)
            {
                if (parameters.ViewId == ViewId.DetailAccount)
                {
                    query = query.Where(line => line.DetailId.HasValue);
                }

                query = query.Include(line => line.DetailAccount);
            }

            if (parameters.IsSelectedCostCenter)
            {
                if (parameters.ViewId == ViewId.CostCenter)
                {
                    query = query.Where(line => line.CostCenterId.HasValue);
                }

                query = query.Include(line => line.CostCenter);
            }

            if (parameters.IsSelectedProject)
            {
                if (parameters.ViewId == ViewId.Project)
                {
                    query = query.Where(line => line.ProjectId.HasValue);
                }

                query = query.Include(line => line.Project);
            }

            return query;
        }

        private static IEnumerable<IGrouping<string, BalanceByAccountItemViewModel>> GetGroupByItems(
            IEnumerable<BalanceByAccountItemViewModel> items, Func<BalanceByAccountItemViewModel, string> selector1)
        {
            foreach (var byFirst in items
                .OrderBy(selector1)
                .GroupBy(selector1))
            {
                yield return byFirst;
            }
        }

        #region report by Account

        private async Task<BalanceByAccountViewModel> ReportByAccountAsync(BalanceByAccountParameters parameters)
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

                filter = line => line.AccountLevel >= parameters.AccountLevel;
                foreach (var lineGroup in _report.GetTurnoverGroups(lines, parameters.AccountLevel.Value, filter))
                {
                    await AddBalanceByAccountItemsAsync(balanceByAccount, lineGroup, parameters, lineGroup.Key);
                }
            }

            SortAndSetItems(balanceByAccount, parameters.GridOptions);
            SetSummaryItems(balanceByAccount);

            await OnSourceActionAsync(parameters.GridOptions, SourceListId.BalanceByOneAccount);
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

            SortAndSetItems(balanceByAccount, parameters.GridOptions);
            SetSummaryItems(balanceByAccount);

            await OnSourceActionAsync(parameters.GridOptions, SourceListId.BalanceByAllAccounts);
            return balanceByAccount;
        }

        #endregion

        #region report by DetailAccount

        private async Task<BalanceByAccountViewModel> ReportByDetailAccountAsync(BalanceByAccountParameters parameters)
        {
            if (parameters.DetailAccountId.HasValue)
            {
                return await GetReportBySelectedDetailAccountAsync(parameters);
            }
            else
            {
                return await GetReportByAllDetailAccountAsync(parameters);
            }
        }

        private async Task<BalanceByAccountViewModel> GetReportBySelectedDetailAccountAsync(BalanceByAccountParameters parameters)
        {
            var balanceByAccount = new BalanceByAccountViewModel();
            var detailAccItem = await GetDetailAccountAsync(parameters.DetailAccountId.Value);
            if (detailAccItem != null)
            {
                var lines = await GetVoucherLinesAsync(parameters);

                lines = lines.Where(line => line.DetailAccountFullCode.StartsWith(detailAccItem.FullCode)).ToList();

                Func<BalanceByAccountItemViewModel, bool> filter;
                int index = 0;
                while (index < parameters.DetailAccountLevel)
                {
                    filter = line => line.DetailAccountLevel == index;
                    foreach (var lineGroup in GetDetailAccountGroups(lines, index, filter))
                    {
                        await AddBalanceByAccountItemsAsync(balanceByAccount, lineGroup, parameters, lineGroup.Key);
                    }

                    index++;
                }

                filter = line => line.DetailAccountLevel >= parameters.DetailAccountLevel;
                foreach (var lineGroup in GetDetailAccountGroups(lines, parameters.DetailAccountLevel.Value, filter))
                {
                    await AddBalanceByAccountItemsAsync(balanceByAccount, lineGroup, parameters, lineGroup.Key);
                }
            }

            SortAndSetItems(balanceByAccount, parameters.GridOptions);
            SetSummaryItems(balanceByAccount);

            await OnSourceActionAsync(parameters.GridOptions, SourceListId.BalanceByOneDetailAccount);
            return balanceByAccount;
        }

        private async Task<BalanceByAccountViewModel> GetReportByAllDetailAccountAsync(BalanceByAccountParameters parameters)
        {
            var balanceByAccount = new BalanceByAccountViewModel();
            var lines = await GetVoucherLinesAsync(parameters);

            Func<BalanceByAccountItemViewModel, bool> filter;
            int index = 0;
            while (index < parameters.DetailAccountLevel)
            {
                filter = line => line.DetailAccountLevel == index;
                foreach (var lineGroup in GetDetailAccountGroups(lines, index, filter))
                {
                    await AddBalanceByAccountItemsAsync(balanceByAccount, lineGroup, parameters, lineGroup.Key);
                }

                index++;
            }

            filter = line => line.DetailAccountLevel >= index;

            foreach (var lineGroup in GetDetailAccountGroups(lines, index, filter))
            {
                await AddBalanceByAccountItemsAsync(balanceByAccount, lineGroup, parameters, lineGroup.Key);
            }

            SortAndSetItems(balanceByAccount, parameters.GridOptions);
            SetSummaryItems(balanceByAccount);

            await OnSourceActionAsync(parameters.GridOptions, SourceListId.BalanceByAllDetailAccounts);
            return balanceByAccount;
        }

        #endregion

        #region report by CostCenter

        private async Task<BalanceByAccountViewModel> ReportByCostCenterAsync(BalanceByAccountParameters parameters)
        {
            if (parameters.CostCenterId.HasValue)
            {
                return await GetReportBySelectedCostCenterAsync(parameters);
            }
            else
            {
                return await GetReportByAllCostCenterAsync(parameters);
            }
        }

        private async Task<BalanceByAccountViewModel> GetReportBySelectedCostCenterAsync(BalanceByAccountParameters parameters)
        {
            var balanceByAccount = new BalanceByAccountViewModel();
            var costCenterItem = await GetCostCenterAsync(parameters.CostCenterId.Value);
            if (costCenterItem != null)
            {
                var lines = await GetVoucherLinesAsync(parameters);

                lines = lines.Where(line => line.CostCenterFullCode.StartsWith(costCenterItem.FullCode)).ToList();

                Func<BalanceByAccountItemViewModel, bool> filter;
                int index = 0;
                while (index < parameters.CostCenterLevel)
                {
                    filter = line => line.CostCenterLevel == index;
                    foreach (var lineGroup in GetCostCenterGroups(lines, index, filter))
                    {
                        await AddBalanceByAccountItemsAsync(balanceByAccount, lineGroup, parameters, lineGroup.Key);
                    }

                    index++;
                }

                filter = line => line.CostCenterLevel >= parameters.CostCenterLevel;
                foreach (var lineGroup in GetCostCenterGroups(lines, parameters.CostCenterLevel.Value, filter))
                {
                    await AddBalanceByAccountItemsAsync(balanceByAccount, lineGroup, parameters, lineGroup.Key);
                }
            }

            SortAndSetItems(balanceByAccount, parameters.GridOptions);
            SetSummaryItems(balanceByAccount);

            await OnSourceActionAsync(parameters.GridOptions, SourceListId.BalanceByOneCostCenter);
            return balanceByAccount;
        }

        private async Task<BalanceByAccountViewModel> GetReportByAllCostCenterAsync(BalanceByAccountParameters parameters)
        {
            var balanceByAccount = new BalanceByAccountViewModel();
            var lines = await GetVoucherLinesAsync(parameters);

            Func<BalanceByAccountItemViewModel, bool> filter;
            int index = 0;
            while (index < parameters.CostCenterLevel)
            {
                filter = line => line.CostCenterLevel == index;
                foreach (var lineGroup in GetCostCenterGroups(lines, index, filter))
                {
                    await AddBalanceByAccountItemsAsync(balanceByAccount, lineGroup, parameters, lineGroup.Key);
                }

                index++;
            }

            filter = line => line.CostCenterLevel >= index;

            foreach (var lineGroup in GetCostCenterGroups(lines, index, filter))
            {
                await AddBalanceByAccountItemsAsync(balanceByAccount, lineGroup, parameters, lineGroup.Key);
            }

            SortAndSetItems(balanceByAccount, parameters.GridOptions);
            SetSummaryItems(balanceByAccount);

            await OnSourceActionAsync(parameters.GridOptions, SourceListId.BalanceByAllCostCenters);
            return balanceByAccount;
        }

        #endregion

        #region report by Project

        private async Task<BalanceByAccountViewModel> ReportByProjectAsync(BalanceByAccountParameters parameters)
        {
            if (parameters.ProjectId.HasValue)
            {
                return await GetReportBySelectedProjectAsync(parameters);
            }
            else
            {
                return await GetReportByAllProjectAsync(parameters);
            }
        }

        private async Task<BalanceByAccountViewModel> GetReportBySelectedProjectAsync(BalanceByAccountParameters parameters)
        {
            var balanceByAccount = new BalanceByAccountViewModel();
            var projectItem = await GetProjectAsync(parameters.ProjectId.Value);
            if (projectItem != null)
            {
                var lines = await GetVoucherLinesAsync(parameters);

                lines = lines.Where(line => line.ProjectFullCode.StartsWith(projectItem.FullCode)).ToList();

                Func<BalanceByAccountItemViewModel, bool> filter;
                int index = 0;
                while (index < parameters.ProjectLevel)
                {
                    filter = line => line.ProjectLevel == index;
                    foreach (var lineGroup in GetProjectGroups(lines, index, filter))
                    {
                        await AddBalanceByAccountItemsAsync(balanceByAccount, lineGroup, parameters, lineGroup.Key);
                    }

                    index++;
                }

                filter = line => line.ProjectLevel >= parameters.ProjectLevel;
                foreach (var lineGroup in GetProjectGroups(lines, parameters.ProjectLevel.Value, filter))
                {
                    await AddBalanceByAccountItemsAsync(balanceByAccount, lineGroup, parameters, lineGroup.Key);
                }
            }

            SortAndSetItems(balanceByAccount, parameters.GridOptions);
            SetSummaryItems(balanceByAccount);

            await OnSourceActionAsync(parameters.GridOptions, SourceListId.BalanceByOneProject);
            return balanceByAccount;
        }

        private async Task<BalanceByAccountViewModel> GetReportByAllProjectAsync(BalanceByAccountParameters parameters)
        {
            var balanceByAccount = new BalanceByAccountViewModel();
            var lines = await GetVoucherLinesAsync(parameters);

            Func<BalanceByAccountItemViewModel, bool> filter;
            int index = 0;
            while (index < parameters.ProjectLevel)
            {
                filter = line => line.ProjectLevel == index;
                foreach (var lineGroup in GetProjectGroups(lines, index, filter))
                {
                    await AddBalanceByAccountItemsAsync(balanceByAccount, lineGroup, parameters, lineGroup.Key);
                }

                index++;
            }

            filter = line => line.ProjectLevel >= index;

            foreach (var lineGroup in GetProjectGroups(lines, index, filter))
            {
                await AddBalanceByAccountItemsAsync(balanceByAccount, lineGroup, parameters, lineGroup.Key);
            }

            SortAndSetItems(balanceByAccount, parameters.GridOptions);
            SetSummaryItems(balanceByAccount);

            await OnSourceActionAsync(parameters.GridOptions, SourceListId.BalanceByAllProjects);
            return balanceByAccount;
        }

        #endregion

        private async Task<IList<BalanceByAccountItemViewModel>> GetVoucherLinesAsync(BalanceByAccountParameters parameters)
        {
            var query = Repository
                .GetAllOperationQuery<VoucherLine>(ViewId.VoucherLine,
                line => line.Voucher,
                line => line.Branch);

            query = IncludeLineReference(query, parameters);

            var options = (TestBalanceOptions)parameters.Options;
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
            string fullCode)
        {
            if (parameters.IsByBranch)
            {
                foreach (var branchGroup in lines.GroupBy(item => item.BranchId))
                {
                    foreach (var lineGroup in await FilterLineByItemsAsync(branchGroup, parameters))
                    {
                        balanceByAccount.Items.Add(await GetBalanceByAccountItemAsync(lineGroup, parameters, fullCode));
                    }
                }
            }
            else
            {
                foreach (var lineGroup in await FilterLineByItemsAsync(lines, parameters))
                {
                    balanceByAccount.Items.Add(await GetBalanceByAccountItemAsync(lineGroup, parameters, fullCode));
                }
            }
        }

        private async Task<IEnumerable<IGrouping<string, BalanceByAccountItemViewModel>>> FilterLineByItemsAsync(
            IEnumerable<BalanceByAccountItemViewModel> lines,
            BalanceByAccountParameters parameters)
        {
            var resultGroups = new List<IGrouping<string, BalanceByAccountItemViewModel>>();

            var groups = lines.GroupBy(line => string.Empty).ToList();

            if (parameters.IsSelectedAccount)
            {
                resultGroups = new List<IGrouping<string, BalanceByAccountItemViewModel>>();
                foreach (var lineGroup in groups)
                {
                    var test = await FilterLinesByAccountAsync(lineGroup, parameters);
                    resultGroups.AddRange(test);
                }

                groups = resultGroups;
            }

            if (parameters.IsSelectedDetailAccount)
            {
                resultGroups = new List<IGrouping<string, BalanceByAccountItemViewModel>>();
                foreach (var lineGroup in groups)
                {
                    var test = await FilterLinesByDetailAccountAsync(lineGroup, parameters);
                    resultGroups.AddRange(test);
                }

                groups = resultGroups;
            }

            if (parameters.IsSelectedCostCenter)
            {
                resultGroups = new List<IGrouping<string, BalanceByAccountItemViewModel>>();
                foreach (var lineGroup in groups)
                {
                    var test = await FilterLinesByCostCenterAsync(lineGroup, parameters);
                    resultGroups.AddRange(test);
                }

                groups = resultGroups;
            }

            if (parameters.IsSelectedProject)
            {
                resultGroups = new List<IGrouping<string, BalanceByAccountItemViewModel>>();
                foreach (var lineGroup in groups)
                {
                    var test = await FilterLinesByProjectAsync(lineGroup, parameters);
                    resultGroups.AddRange(test);
                }

                groups = resultGroups;
            }

            return groups;
        }

        private async Task<IEnumerable<IGrouping<string, BalanceByAccountItemViewModel>>> FilterLinesByAccountAsync(
           IEnumerable<BalanceByAccountItemViewModel> lines,
           BalanceByAccountParameters parameters)
        {
            var groups = new List<IGrouping<string, BalanceByAccountItemViewModel>>();

            if (parameters.IsSelectedAccount && parameters.ViewId != ViewId.Account)
            {
                if (parameters.AccountId.HasValue)
                {
                    var accountItem = await GetAccountAsync(parameters.AccountId.Value);
                    if (accountItem != null)
                    {
                        lines = lines.Where(line => line.AccountFullCode.StartsWith(accountItem.FullCode)).ToList();

                        Func<BalanceByAccountItemViewModel, bool> filter;
                        int index = 0;
                        while (index < parameters.AccountLevel)
                        {
                            filter = line => line.AccountLevel == index;
                            foreach (var lineGroup in GetAccountGroups(lines, index, filter))
                            {
                                groups.Add(lineGroup);
                            }

                            index++;
                        }

                        filter = line => line.AccountLevel >= parameters.AccountLevel;
                        foreach (var lineGroup in GetAccountGroups(lines, parameters.AccountLevel.Value, filter))
                        {
                            groups.Add(lineGroup);
                        }
                    }
                }
                else
                {
                    Func<BalanceByAccountItemViewModel, bool> filter;
                    var filterdLines = new List<BalanceByAccountItemViewModel>();
                    int index = 0;
                    while (index < parameters.AccountLevel)
                    {
                        filterdLines = lines.Where(line => line.AccountLevel == index && string.IsNullOrEmpty(line.AccountFullCode)).ToList();
                        foreach (var lineGroup in filterdLines.GroupBy(line => line.AccountFullCode))
                        {
                            groups.Add(lineGroup);
                        }

                        filter = line => line.AccountLevel == index && !string.IsNullOrEmpty(line.AccountFullCode);
                        foreach (var lineGroup in GetAccountGroups(lines, index, filter))
                        {
                            groups.Add(lineGroup);
                        }

                        index++;
                    }

                    filterdLines = lines.Where(line => line.AccountLevel >= index && string.IsNullOrEmpty(line.AccountFullCode)).ToList();

                    foreach (var lineGroup in filterdLines.GroupBy(line => line.AccountFullCode))
                    {
                        groups.Add(lineGroup);
                    }

                    filter = line => line.AccountLevel >= index && !string.IsNullOrEmpty(line.AccountFullCode);

                    foreach (var lineGroup in GetAccountGroups(lines, index, filter))
                    {
                        groups.Add(lineGroup);
                    }
                }
            }
            else
            {
                groups = lines.GroupBy(line => string.Empty).ToList();
            }

            return groups;
        }

        private async Task<IEnumerable<IGrouping<string, BalanceByAccountItemViewModel>>> FilterLinesByDetailAccountAsync(
            IEnumerable<BalanceByAccountItemViewModel> lines,
            BalanceByAccountParameters parameters)
        {
            var groups = new List<IGrouping<string, BalanceByAccountItemViewModel>>();

            if (parameters.IsSelectedDetailAccount && parameters.ViewId != ViewId.DetailAccount)
            {
                if (parameters.DetailAccountId.HasValue)
                {
                    var detailAccountItem = await GetDetailAccountAsync(parameters.DetailAccountId.Value);
                    if (detailAccountItem != null)
                    {
                        lines = lines.Where(line => line.DetailAccountFullCode.StartsWith(detailAccountItem.FullCode)).ToList();

                        Func<BalanceByAccountItemViewModel, bool> filter;
                        int index = 0;
                        while (index < parameters.DetailAccountLevel)
                        {
                            filter = line => line.DetailAccountLevel == index;
                            foreach (var lineGroup in GetDetailAccountGroups(lines, index, filter))
                            {
                                groups.Add(lineGroup);
                            }

                            index++;
                        }

                        filter = line => line.DetailAccountLevel >= parameters.DetailAccountLevel;
                        foreach (var lineGroup in GetDetailAccountGroups(lines, parameters.DetailAccountLevel.Value, filter))
                        {
                            groups.Add(lineGroup);
                        }
                    }
                }
                else
                {
                    Func<BalanceByAccountItemViewModel, bool> filter;
                    var filterdLines = new List<BalanceByAccountItemViewModel>();
                    int index = 0;
                    while (index < parameters.DetailAccountLevel)
                    {
                        filterdLines = lines.Where(line => line.DetailAccountLevel == index && string.IsNullOrEmpty(line.DetailAccountFullCode)).ToList();
                        foreach (var lineGroup in filterdLines.GroupBy(line => line.DetailAccountFullCode))
                        {
                            groups.Add(lineGroup);
                        }

                        filter = line => line.DetailAccountLevel == index && !string.IsNullOrEmpty(line.DetailAccountFullCode);
                        foreach (var lineGroup in GetDetailAccountGroups(lines, index, filter))
                        {
                            groups.Add(lineGroup);
                        }

                        index++;
                    }

                    filterdLines = lines.Where(line => line.DetailAccountLevel >= index && string.IsNullOrEmpty(line.DetailAccountFullCode)).ToList();

                    foreach (var lineGroup in filterdLines.GroupBy(line => line.DetailAccountFullCode))
                    {
                        groups.Add(lineGroup);
                    }

                    filter = line => line.DetailAccountLevel >= index && !string.IsNullOrEmpty(line.DetailAccountFullCode);

                    foreach (var lineGroup in GetDetailAccountGroups(lines, index, filter))
                    {
                        groups.Add(lineGroup);
                    }
                }
            }
            else
            {
                groups = lines.GroupBy(line => string.Empty).ToList();
            }

            return groups;
        }

        private async Task<IEnumerable<IGrouping<string, BalanceByAccountItemViewModel>>> FilterLinesByCostCenterAsync(
            IEnumerable<BalanceByAccountItemViewModel> lines,
            BalanceByAccountParameters parameters)
        {
            var groups = new List<IGrouping<string, BalanceByAccountItemViewModel>>();

            if (parameters.IsSelectedCostCenter && parameters.ViewId != ViewId.CostCenter)
            {
                if (parameters.CostCenterId.HasValue)
                {
                    var costCenterItem = await GetCostCenterAsync(parameters.CostCenterId.Value);
                    if (costCenterItem != null)
                    {
                        lines = lines.Where(line => line.CostCenterFullCode.StartsWith(costCenterItem.FullCode)).ToList();

                        Func<BalanceByAccountItemViewModel, bool> filter;
                        int index = 0;
                        while (index < parameters.CostCenterLevel)
                        {
                            filter = line => line.CostCenterLevel == index;
                            foreach (var lineGroup in GetCostCenterGroups(lines, index, filter))
                            {
                                groups.Add(lineGroup);
                            }

                            index++;
                        }

                        filter = line => line.CostCenterLevel >= parameters.CostCenterLevel;
                        foreach (var lineGroup in GetCostCenterGroups(lines, parameters.CostCenterLevel.Value, filter))
                        {
                            groups.Add(lineGroup);
                        }
                    }
                }
                else
                {
                    Func<BalanceByAccountItemViewModel, bool> filter;
                    var filterdLines = new List<BalanceByAccountItemViewModel>();
                    int index = 0;
                    while (index < parameters.CostCenterLevel)
                    {
                        filterdLines = lines.Where(line => line.CostCenterLevel == index && string.IsNullOrEmpty(line.CostCenterFullCode)).ToList();
                        foreach (var lineGroup in filterdLines.GroupBy(line => line.CostCenterFullCode))
                        {
                            groups.Add(lineGroup);
                        }

                        filter = line => line.CostCenterLevel == index && !string.IsNullOrEmpty(line.CostCenterFullCode);
                        foreach (var lineGroup in GetCostCenterGroups(lines, index, filter))
                        {
                            groups.Add(lineGroup);
                        }

                        index++;
                    }

                    filterdLines = lines.Where(line => line.CostCenterLevel >= index && string.IsNullOrEmpty(line.CostCenterFullCode)).ToList();

                    foreach (var lineGroup in filterdLines.GroupBy(line => line.CostCenterFullCode))
                    {
                        groups.Add(lineGroup);
                    }

                    filter = line => line.CostCenterLevel >= index && !string.IsNullOrEmpty(line.CostCenterFullCode);

                    foreach (var lineGroup in GetCostCenterGroups(lines, index, filter))
                    {
                        groups.Add(lineGroup);
                    }
                }
            }
            else
            {
                groups = lines.GroupBy(line => string.Empty).ToList();
            }

            return groups;
        }

        private async Task<IEnumerable<IGrouping<string, BalanceByAccountItemViewModel>>> FilterLinesByProjectAsync(
            IEnumerable<BalanceByAccountItemViewModel> lines,
            BalanceByAccountParameters parameters)
        {
            var groups = new List<IGrouping<string, BalanceByAccountItemViewModel>>();

            if (parameters.IsSelectedProject && parameters.ViewId != ViewId.Project)
            {
                if (parameters.ProjectId.HasValue)
                {
                    var projectItem = await GetProjectAsync(parameters.ProjectId.Value);
                    if (projectItem != null)
                    {
                        lines = lines.Where(line => line.ProjectFullCode.StartsWith(projectItem.FullCode)).ToList();

                        Func<BalanceByAccountItemViewModel, bool> filter;
                        int index = 0;
                        while (index < parameters.ProjectLevel)
                        {
                            filter = line => line.ProjectLevel == index;
                            foreach (var lineGroup in GetProjectGroups(lines, index, filter))
                            {
                                groups.Add(lineGroup);
                            }

                            index++;
                        }

                        filter = line => line.ProjectLevel >= parameters.ProjectLevel;
                        foreach (var lineGroup in GetProjectGroups(lines, parameters.ProjectLevel.Value, filter))
                        {
                            groups.Add(lineGroup);
                        }
                    }
                }
                else
                {
                    Func<BalanceByAccountItemViewModel, bool> filter;
                    var filterdLines = new List<BalanceByAccountItemViewModel>();
                    int index = 0;
                    while (index < parameters.ProjectLevel)
                    {
                        filterdLines = lines.Where(line => line.ProjectLevel == index && string.IsNullOrEmpty(line.ProjectFullCode)).ToList();
                        foreach (var lineGroup in filterdLines.GroupBy(line => line.ProjectFullCode))
                        {
                            groups.Add(lineGroup);
                        }

                        filter = line => line.ProjectLevel == index && !string.IsNullOrEmpty(line.ProjectFullCode);
                        foreach (var lineGroup in GetProjectGroups(lines, index, filter))
                        {
                            groups.Add(lineGroup);
                        }

                        index++;
                    }

                    filterdLines = lines.Where(line => line.ProjectLevel >= index && string.IsNullOrEmpty(line.ProjectFullCode)).ToList();

                    foreach (var lineGroup in filterdLines.GroupBy(line => line.ProjectFullCode))
                    {
                        groups.Add(lineGroup);
                    }

                    filter = line => line.ProjectLevel >= index && !string.IsNullOrEmpty(line.ProjectFullCode);

                    foreach (var lineGroup in GetProjectGroups(lines, index, filter))
                    {
                        groups.Add(lineGroup);
                    }
                }
            }
            else
            {
                groups = lines.GroupBy(line => string.Empty).ToList();
            }

            return groups;
        }

        private IEnumerable<IGrouping<string, BalanceByAccountItemViewModel>> GetAccountGroups(
            IEnumerable<BalanceByAccountItemViewModel> lines, int groupLevel, Func<BalanceByAccountItemViewModel, bool> lineFilter)
        {
            var selector = GetAccountGroupSelector(groupLevel);
            return GetGroupByItems(lines.Where(lineFilter), selector);
        }

        private IEnumerable<IGrouping<string, BalanceByAccountItemViewModel>> GetDetailAccountGroups(
            IEnumerable<BalanceByAccountItemViewModel> lines, int groupLevel, Func<BalanceByAccountItemViewModel, bool> lineFilter)
        {
            var selector = GetDetailAccountGroupSelector(groupLevel);
            return GetGroupByItems(lines.Where(lineFilter), selector);
        }

        private IEnumerable<IGrouping<string, BalanceByAccountItemViewModel>> GetCostCenterGroups(
            IEnumerable<BalanceByAccountItemViewModel> lines, int groupLevel, Func<BalanceByAccountItemViewModel, bool> lineFilter)
        {
            var selector = GetCostCenterGroupSelector(groupLevel);
            return GetGroupByItems(lines.Where(lineFilter), selector);
        }

        private IEnumerable<IGrouping<string, BalanceByAccountItemViewModel>> GetProjectGroups(
            IEnumerable<BalanceByAccountItemViewModel> lines, int groupLevel, Func<BalanceByAccountItemViewModel, bool> lineFilter)
        {
            var selector = GetProjectGroupSelector(groupLevel);
            return GetGroupByItems(lines.Where(lineFilter), selector);
        }

        private Func<BalanceByAccountItemViewModel, string> GetAccountGroupSelector(int groupLevel)
        {
            int codeLength = _report.GetLevelCodeLength(ViewId.Account, groupLevel);
            return item => item.AccountFullCode.Substring(0, codeLength);
        }

        private Func<BalanceByAccountItemViewModel, string> GetDetailAccountGroupSelector(int groupLevel)
        {
            int codeLength = _report.GetLevelCodeLength(ViewId.DetailAccount, groupLevel);
            return item => item.DetailAccountFullCode.Substring(0, codeLength);
        }

        private Func<BalanceByAccountItemViewModel, string> GetCostCenterGroupSelector(int groupLevel)
        {
            int codeLength = _report.GetLevelCodeLength(ViewId.CostCenter, groupLevel);
            return item => item.CostCenterFullCode.Substring(0, codeLength);
        }

        private Func<BalanceByAccountItemViewModel, string> GetProjectGroupSelector(int groupLevel)
        {
            int codeLength = _report.GetLevelCodeLength(ViewId.Project, groupLevel);
            return item => item.ProjectFullCode.Substring(0, codeLength);
        }

        private async Task<BalanceByAccountItemViewModel> GetBalanceByAccountItemAsync(
            IEnumerable<BalanceByAccountItemViewModel> lines, BalanceByAccountParameters parameters, string fullCode)
        {
            var balanceItem = lines.First();

            await GetAccountFromVoucherLineAsync(balanceItem, fullCode, parameters);
            await GetDetailAccountFromVoucherLineAsync(balanceItem, fullCode, parameters);
            await GetCostCenterFromVoucherLineAsync(balanceItem, fullCode, parameters);
            await GetProjectFromVoucherLineAsync(balanceItem, fullCode, parameters);

            balanceItem.StartBalance = await GetInitialBalanceAsync(balanceItem.AccountId, parameters);
            balanceItem.Debit = lines.Sum(line => line.Debit);
            balanceItem.Credit = lines.Sum(line => line.Credit);
            balanceItem.EndBalance = balanceItem.StartBalance + balanceItem.Debit - balanceItem.Credit;
            return balanceItem;
        }

        private async Task<IList<BalanceByAccountItemViewModel>> GetFilteredLinesAsync(
            IEnumerable<BalanceByAccountItemViewModel> lines,
            BalanceByAccountParameters parameters)
        {
            if (parameters.ViewId != ViewId.Account)
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

        private async Task GetAccountFromVoucherLineAsync(
            BalanceByAccountItemViewModel line,
            string fullCode,
            BalanceByAccountParameters parameters)
        {
            if (parameters.ViewId == ViewId.Account ||
                (parameters.IsSelectedAccount && !parameters.AccountId.HasValue && !string.IsNullOrEmpty(line.AccountFullCode)))
            {
                string accountFullCode = string.Empty;

                if (parameters.ViewId == ViewId.Account)
                {
                    accountFullCode = fullCode;
                }
                else
                {
                    if (parameters.AccountLevel.Value < line.AccountLevel)
                    {
                        int codeLength = _report.GetLevelCodeLength(ViewId.Account, parameters.AccountLevel.Value);
                        accountFullCode = line.AccountFullCode.Substring(0, codeLength);
                    }
                }

                if (!string.IsNullOrEmpty(accountFullCode))
                {
                    var repository = UnitOfWork.GetAsyncRepository<Account>();
                    var account = await repository.GetSingleByCriteriaAsync(acc => acc.FullCode == accountFullCode);

                    line.AccountId = account.Id;
                    line.AccountName = account.Name;
                    line.AccountFullCode = account.FullCode;
                    line.AccountLevel = account.Level;
                }
            }
        }

        private async Task GetDetailAccountFromVoucherLineAsync(
            BalanceByAccountItemViewModel line,
            string fullCode,
            BalanceByAccountParameters parameters)
        {
            if (parameters.ViewId == ViewId.DetailAccount ||
                (parameters.IsSelectedDetailAccount && !parameters.DetailAccountId.HasValue && !string.IsNullOrEmpty(line.DetailAccountFullCode)))
            {
                string detailAccFullCode = string.Empty;

                if (parameters.ViewId == ViewId.DetailAccount)
                {
                    detailAccFullCode = fullCode;
                }
                else
                {
                    if (parameters.DetailAccountLevel.Value < line.DetailAccountLevel)
                    {
                        int codeLength = _report.GetLevelCodeLength(ViewId.DetailAccount, parameters.DetailAccountLevel.Value);
                        detailAccFullCode = line.DetailAccountFullCode.Substring(0, codeLength);
                    }
                }

                if (!string.IsNullOrEmpty(detailAccFullCode))
                {
                    var repository = UnitOfWork.GetAsyncRepository<DetailAccount>();
                    var detailAccount = await repository.GetSingleByCriteriaAsync(detail => detail.FullCode == detailAccFullCode);

                    line.DetailAccountId = detailAccount.Id;
                    line.DetailAccountName = detailAccount.Name;
                    line.DetailAccountFullCode = detailAccount.FullCode;
                    line.DetailAccountLevel = detailAccount.Level;
                }
            }
        }

        private async Task GetCostCenterFromVoucherLineAsync(
            BalanceByAccountItemViewModel line,
            string fullCode,
            BalanceByAccountParameters parameters)
        {
            if (parameters.ViewId == ViewId.CostCenter ||
                (parameters.IsSelectedCostCenter && !parameters.CostCenterId.HasValue && !string.IsNullOrEmpty(line.CostCenterFullCode)))
            {
                string cCenterFullCode = string.Empty;

                if (parameters.ViewId == ViewId.CostCenter)
                {
                    cCenterFullCode = fullCode;
                }
                else
                {
                    if (parameters.CostCenterLevel.Value < line.CostCenterLevel)
                    {
                        int codeLength = _report.GetLevelCodeLength(ViewId.CostCenter, parameters.CostCenterLevel.Value);
                        cCenterFullCode = line.CostCenterFullCode.Substring(0, codeLength);
                    }
                }

                if (!string.IsNullOrEmpty(cCenterFullCode))
                {
                    var repository = UnitOfWork.GetAsyncRepository<CostCenter>();
                    var costCenter = await repository.GetSingleByCriteriaAsync(cCenter => cCenter.FullCode == cCenterFullCode);

                    line.CostCenterId = costCenter.Id;
                    line.CostCenterName = costCenter.Name;
                    line.CostCenterFullCode = costCenter.FullCode;
                    line.CostCenterLevel = costCenter.Level;
                }
            }
        }

        private async Task GetProjectFromVoucherLineAsync(
            BalanceByAccountItemViewModel line,
            string fullCode,
            BalanceByAccountParameters parameters)
        {
            if (parameters.ViewId == ViewId.Project ||
                (parameters.IsSelectedProject && !parameters.ProjectId.HasValue && !string.IsNullOrEmpty(line.ProjectFullCode)))
            {
                string projectFullCode = string.Empty;

                if (parameters.ViewId == ViewId.Project)
                {
                    projectFullCode = fullCode;
                }
                else
                {
                    if (parameters.ProjectLevel.Value < line.ProjectLevel)
                    {
                        int codeLength = _report.GetLevelCodeLength(ViewId.Project, parameters.ProjectLevel.Value);
                        projectFullCode = line.ProjectFullCode.Substring(0, codeLength);
                    }
                }

                if (!string.IsNullOrEmpty(projectFullCode))
                {
                    var repository = UnitOfWork.GetAsyncRepository<Project>();
                    var project = await repository.GetSingleByCriteriaAsync(prj => prj.FullCode == projectFullCode);

                    line.ProjectId = project.Id;
                    line.ProjectName = project.Name;
                    line.ProjectFullCode = project.FullCode;
                    line.ProjectLevel = project.Level;
                }
            }
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
                .GetAllOperationQuery<VoucherLine>(ViewId.VoucherLine)
                .Where(line => line.FiscalPeriodId == UserContext.FiscalPeriodId)
                .Where(lineCriteria)
                .Where(itemCriteria)
                .Select(line => line.Debit - line.Credit)
                .SumAsync();
        }

        private readonly ISystemRepository _system;
        private readonly IReportUtility _report;
    }
}
