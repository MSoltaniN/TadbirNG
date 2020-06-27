using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPPC.Framework.Extensions;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Model.Corporate;
using SPPC.Tadbir.Model.Finance;
using SPPC.Tadbir.Persistence.Utility;
using SPPC.Tadbir.Resources;
using SPPC.Tadbir.Values;
using SPPC.Tadbir.ViewModel.Reporting;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات دیتابیسی مورد نیاز برای تهیه گزارش سود و زیان را پیاده سازی می کند
    /// </summary>
    public class ProfitLossRepository : RepositoryBase, IProfitLossRepository
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="context">امکانات مشترک مورد نیاز را برای عملیات دیتابیسی فراهم می کند</param>
        /// <param name="utility">امکان استفاده از مجموعه حسابها را فراهم می کند</param>
        public ProfitLossRepository(IRepositoryContext context, IAccountCollectionUtility utility)
            : base(context)
        {
            _utility = utility;
        }

        /// <summary>
        /// اطلاعات گزارش سود و زیان غیرمقایسه ای را برای سیستم ثبت دائمی محاسبه کرده و برمی گرداند
        /// </summary>
        /// <param name="parameters">پارامترهای مورد نیاز برای تهیه گزارش</param>
        /// <returns>اطلاعات گزارش سود و زیان غیرمقایسه ای</returns>
        public async Task<ProfitLossViewModel> GetProfitLossAsync(ProfitLossParameters parameters)
        {
            var profitLoss = new ProfitLossViewModel();
            var grossProfit = await GetGrossProfitItemsAsync(parameters);
            profitLoss.Items.AddRange(grossProfit);
            var operationProfit = await GetOperationalCostItemsAsync(grossProfit.Last(), parameters);
            profitLoss.Items.AddRange(operationProfit);
            var beforeTax = await GetOtherCostRevenueItemsAsync(operationProfit.Last(), parameters);
            profitLoss.Items.AddRange(beforeTax);
            profitLoss.Items.AddRange(GetNetProfitItemsAsync(beforeTax.Last(), parameters));
            return profitLoss;
        }

        private async Task<IEnumerable<ProfitLossItemViewModel>> GetGrossProfitItemsAsync(
            ProfitLossParameters parameters)
        {
            var items = new List<ProfitLossItemViewModel>
            {
                new ProfitLossItemViewModel() { Category = AppStrings.GrossProfitCalculation }
            };

            var lines = new List<ProfitLossLineViewModel>();
            lines.AddRange(await GetCollectionLinesAsync(
                (int)AccountCollectionId.FinalSales, parameters.ToDate, parameters));
            lines.AddRange(await GetCollectionLinesAsync(
                (int)AccountCollectionId.SalesRefundDiscount, parameters.ToDate, parameters));
            var netRevenue = GetReportItem(
                lines, line => line.Credit - line.Debit, AppStrings.NetRevenue,
                parameters.FromDate, parameters.ToDate);

            lines.Clear();
            lines.AddRange(await GetCollectionLinesAsync(
                (int)AccountCollectionId.SoldProductCost, parameters.ToDate, parameters));
            var productCost = GetReportItem(
                lines, line => line.Debit - line.Credit, AppStrings.SoldProductCost,
                parameters.FromDate, parameters.ToDate);

            items.Add(netRevenue);
            items.Add(productCost);
            var grossProfit = netRevenue - productCost;
            grossProfit.Category = AppStrings.GrossProfit;
            items.Add(grossProfit);

            return items;
        }

        private async Task<IEnumerable<ProfitLossItemViewModel>> GetOperationalCostItemsAsync(
            ProfitLossItemViewModel grossProfit, ProfitLossParameters parameters)
        {
            var items = new List<ProfitLossItemViewModel>
            {
                new ProfitLossItemViewModel() { Category = AppStrings.OperationalCost }
            };

            var costItems = new List<ProfitLossItemViewModel>();
            var lines = new List<ProfitLossLineViewModel>();
            lines.AddRange(await GetCollectionLinesAsync(
                (int)AccountCollectionId.OperationalCosts, parameters.ToDate, parameters));
            foreach (var group in lines
                .OrderBy(line => line.AccountId)
                .GroupBy(line => line.AccountId))
            {
                var costItem = GetReportItem(group, line => line.Debit - line.Credit,
                    group.First().AccountName, parameters.FromDate, parameters.ToDate);
                costItems.Add(costItem);
            }

            items.AddRange(costItems);
            var totalCost = new ProfitLossItemViewModel()
            {
                Category = AppStrings.OperationalCostTotal,
                StartBalance = costItems.Sum(item => item.StartBalance),
                PeriodTurnover = costItems.Sum(item => item.PeriodTurnover),
                EndBalance = costItems.Sum(item => item.EndBalance),
                Balance = costItems.Sum(item => item.EndBalance)
            };
            items.Add(totalCost);
            var operationProfit = grossProfit - totalCost;
            operationProfit.Category = AppStrings.OperationalProfit;
            items.Add(operationProfit);

            return items;
        }

        private async Task<IEnumerable<ProfitLossItemViewModel>> GetOtherCostRevenueItemsAsync(
            ProfitLossItemViewModel operationProfit, ProfitLossParameters parameters)
        {
            var items = new List<ProfitLossItemViewModel>
            {
                new ProfitLossItemViewModel() { Category = AppStrings.OtherCostAndRevenue }
            };

            var costItems = new List<ProfitLossItemViewModel>();
            var lines = new List<ProfitLossLineViewModel>();
            lines.AddRange(await GetCollectionLinesAsync(
                (int)AccountCollectionId.OtherCostRevenue, parameters.ToDate, parameters));
            foreach (var group in lines
                .OrderBy(line => line.AccountId)
                .GroupBy(line => line.AccountId))
            {
                var costItem = GetReportItem(group, line => line.Debit - line.Credit,
                    group.First().AccountName, parameters.FromDate, parameters.ToDate);
                costItems.Add(costItem);
            }

            items.AddRange(costItems);
            var netCost = new ProfitLossItemViewModel()
            {
                Category = AppStrings.OtherCostAndRevenueNet,
                StartBalance = costItems.Sum(item => item.StartBalance),
                PeriodTurnover = costItems.Sum(item => item.PeriodTurnover),
                EndBalance = costItems.Sum(item => item.EndBalance),
                Balance = costItems.Sum(item => item.EndBalance)
            };
            items.Add(netCost);
            var beforeTax = operationProfit - netCost;
            beforeTax.Category = AppStrings.ProfitBeforeTax;
            items.Add(beforeTax);

            return items;
        }

        private IEnumerable<ProfitLossItemViewModel> GetNetProfitItemsAsync(
            ProfitLossItemViewModel beforeTax, ProfitLossParameters parameters)
        {
            var items = new List<ProfitLossItemViewModel>
            {
                new ProfitLossItemViewModel()
                {
                    Account = AppStrings.Tax,
                    StartBalance = parameters.TaxAmount,
                    EndBalance = parameters.TaxAmount,
                    Balance = parameters.TaxAmount
                },
                new ProfitLossItemViewModel()
                {
                    Category = AppStrings.NetProfit,
                    PeriodTurnover = beforeTax.PeriodTurnover - parameters.TaxAmount,
                    EndBalance = beforeTax.EndBalance - parameters.TaxAmount
                }
            };

            return items;
        }

        private async Task<IEnumerable<ProfitLossLineViewModel>> GetCollectionLinesAsync(
            int collectionId, DateTime until, ProfitLossParameters parameters)
        {
            var lines = new List<ProfitLossLineViewModel>();
            var repository = UnitOfWork.GetAsyncRepository<FiscalPeriod>();
            DateTime startDate = await repository
                .GetEntityQuery()
                .Where(fp => fp.Id == UserContext.FiscalPeriodId)
                .Select(fp => fp.StartDate)
                .SingleOrDefaultAsync();
            if (startDate != DateTime.MinValue)
            {
                lines.AddRange(
                    await GetCollectionLinesAsync(collectionId, startDate, until, parameters));
            }

            return lines;
        }

        private async Task<IEnumerable<ProfitLossLineViewModel>> GetCollectionLinesAsync(
            int collectionId, DateTime from, DateTime to, ProfitLossParameters parameters)
        {
            var accounts = await _utility.GetUsableCollectionAccountsAsync(collectionId);
            var accountIds = accounts.Select(acc => acc.Id);
            var branchIds = GetChildTree(UserContext.BranchId);
            var repository = UnitOfWork.GetAsyncRepository<VoucherLine>();
            var linesQuery = repository
                .GetEntityQuery(line => line.Voucher, line => line.Account)
                .Where(line => line.Voucher.Date.IsBetween(from, to)
                    && line.FiscalPeriodId == UserContext.FiscalPeriodId
                    && accountIds.Contains(line.AccountId)
                    && branchIds.Contains(line.BranchId));
            if (!parameters.UseClosingTempVoucher)
            {
                linesQuery = linesQuery
                    .Where(line => line.Voucher.Type != (short)VoucherType.ClosingTempAccounts);
            }

            if (parameters.CostCenterId != null)
            {
                var costRepository = UnitOfWork.GetAsyncRepository<CostCenter>();
                var fullCode = await costRepository
                    .GetEntityQuery()
                    .Where(cc => cc.Id == parameters.CostCenterId)
                    .Select(cc => cc.FullCode)
                    .SingleOrDefaultAsync();
                linesQuery = linesQuery.Where(line => line.CostCenter.FullCode.StartsWith(fullCode));
            }

            if (parameters.ProjectId != null)
            {
                var projectRepository = UnitOfWork.GetAsyncRepository<CostCenter>();
                var fullCode = await projectRepository
                    .GetEntityQuery()
                    .Where(prj => prj.Id == parameters.ProjectId)
                    .Select(prj => prj.FullCode)
                    .SingleOrDefaultAsync();
                linesQuery = linesQuery.Where(line => line.Project.FullCode.StartsWith(fullCode));
            }

            var filteredLines = await linesQuery.ToListAsync();
            return filteredLines
                .Select(line => Mapper.Map<ProfitLossLineViewModel>(line))
                .Apply(parameters.GridOptions, false);
        }

        private ProfitLossItemViewModel GetReportItem(
            IEnumerable<ProfitLossLineViewModel> lines,
            Func<ProfitLossLineViewModel, decimal> balanceFunc,
            string account, DateTime from, DateTime to)
        {
            decimal initBalance = lines
                .Where(line => line.VoucherDate.Date < from)
                .Sum(balanceFunc);
            decimal turnover = lines
                .Where(line => line.VoucherDate.IsBetween(from, to))
                .Sum(balanceFunc);
            return new ProfitLossItemViewModel()
            {
                Account = account,
                StartBalance = initBalance,
                PeriodTurnover = turnover,
                EndBalance = initBalance + turnover,
                Balance = initBalance + turnover
            };
        }

        private IEnumerable<int> GetChildTree(int branchId)
        {
            var tree = new List<int>();
            var repository = UnitOfWork.GetAsyncRepository<Branch>();
            var branch = repository.GetByID(branchId, br => br.Children);
            tree.Add(branchId);
            AddChildren(branch, tree);
            return tree;
        }

        private void AddChildren(Branch branch, IList<int> children)
        {
            var repository = UnitOfWork.GetAsyncRepository<Branch>();
            foreach (var child in branch.Children)
            {
                children.Add(child.Id);
                var item = repository.GetByID(child.Id, br => br.Children);
                AddChildren(item, children);
            }
        }

        private readonly IAccountCollectionUtility _utility;
    }
}
