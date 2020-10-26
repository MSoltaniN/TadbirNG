using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPPC.Framework.Common;
using SPPC.Framework.Extensions;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Model.Corporate;
using SPPC.Tadbir.Model.Finance;
using SPPC.Tadbir.Persistence.Utility;
using SPPC.Tadbir.Resources;
using SPPC.Tadbir.ViewModel.Finance;
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
        /// <param name="metadata">امکان خواندن اطلاعات فراداده ای را فراهم می کند</param>
        /// <param name="utility">امکان استفاده از مجموعه حسابها را فراهم می کند</param>
        public ProfitLossRepository(IRepositoryContext context, IMetadataRepository metadata,
            IAccountCollectionUtility utility)
            : base(context)
        {
            _metadata = metadata;
            _utility = utility;
        }

        /// <summary>
        /// اطلاعات گزارش سود و زیان غیرمقایسه ای را محاسبه کرده و برمی گرداند
        /// </summary>
        /// <param name="parameters">پارامترهای مورد نیاز برای تهیه گزارش</param>
        /// <param name="balanceItems">مجموعه اطلاعات مانده ابتدا و انتهای دوره گزارشگیری
        /// برای حساب های موجودی کالا - سیستم ادواری</param>
        /// <returns>اطلاعات گزارش سود و زیان غیرمقایسه ای</returns>
        public async Task<ProfitLossViewModel> GetProfitLossAsync(
            ProfitLossParameters parameters, IEnumerable<StartEndBalanceViewModel> balanceItems)
        {
            var profitLoss = new ProfitLossViewModel();
            var grossProfit = await GetGrossProfitItemsAsync(parameters, balanceItems);
            profitLoss.Items.AddRange(grossProfit);
            var operationProfit = await GetOperationalCostItemsAsync(grossProfit.Last(), parameters);
            profitLoss.Items.AddRange(operationProfit);
            var beforeTax = await GetOtherCostRevenueItemsAsync(operationProfit.Last(), parameters);
            profitLoss.Items.AddRange(beforeTax);
            var netProfit = GetNetProfitItemsAsync(beforeTax.Last(), parameters);
            profitLoss.Items.AddRange(netProfit);
            return profitLoss;
        }

        /// <summary>
        /// اطلاعات گزارش سود و زیان مقایسه ای را برای مراکز هزینه انتخابی محاسبه کرده و برمی گرداند
        /// </summary>
        /// <param name="parameters">پارامترهای مورد نیاز برای تهیه گزارش</param>
        /// <param name="balanceItems">مجموعه اطلاعات مانده ابتدا و انتهای دوره گزارشگیری
        /// برای حساب های موجودی کالا - سیستم ادواری</param>
        /// <returns>اطلاعات گزارش سود و زیان مقایسه ای برای چند مرکز هزینه</returns>
        public async Task<ProfitLossViewModel> GetProfitLossByCostCentersAsync(
            ProfitLossParameters parameters, IEnumerable<StartEndBalanceViewModel> balanceItems)
        {
            var profitLoss = new ProfitLossViewModel();
            if (parameters.CompareItems.Count > 0)
            {
                int firstCostCenterId = parameters.CompareItems[0];
                parameters.CostCenterId = firstCostCenterId;
                profitLoss = await GetProfitLossAsync(parameters, balanceItems);
                foreach (var item in profitLoss.Items)
                {
                    profitLoss.ItemsByCostCenters.Add(Mapper.Map<ProfitLossByCostCentersViewModel>(item));
                }

                int itemIndex = 1;
                while (itemIndex < parameters.CompareItems.Count)
                {
                    parameters.CostCenterId = parameters.CompareItems[itemIndex];
                    var itemProfitLoss = await GetProfitLossAsync(parameters, balanceItems);
                    if (itemProfitLoss.Items.Count == profitLoss.ItemsByCostCenters.Count)
                    {
                        for (int lineIndex = 0; lineIndex < profitLoss.ItemsByCostCenters.Count; lineIndex++)
                        {
                            CopyCostCenterValues(itemIndex,
                                itemProfitLoss.Items[lineIndex], profitLoss.ItemsByCostCenters[lineIndex]);
                        }
                    }

                    itemIndex++;
                }

                profitLoss.ViewMetadata = await _metadata.GetCompoundViewMetadataAsync(
                    ViewId.ProfitLossByCostCenters, ViewId.CostCenter, parameters.CompareItems);
            }

            return profitLoss;
        }

        /// <summary>
        /// اطلاعات گزارش سود و زیان مقایسه ای را برای پروژه های انتخابی محاسبه کرده و برمی گرداند
        /// </summary>
        /// <param name="parameters">پارامترهای مورد نیاز برای تهیه گزارش</param>
        /// <param name="balanceItems">مجموعه اطلاعات مانده ابتدا و انتهای دوره گزارشگیری
        /// برای حساب های موجودی کالا - سیستم ادواری</param>
        /// <returns>اطلاعات گزارش سود و زیان مقایسه ای برای چند پروژه</returns>
        public async Task<ProfitLossViewModel> GetProfitLossByProjectsAsync(
            ProfitLossParameters parameters, IEnumerable<StartEndBalanceViewModel> balanceItems)
        {
            var profitLoss = new ProfitLossViewModel();
            if (parameters.CompareItems.Count > 0)
            {
                int firstProjectId = parameters.CompareItems[0];
                parameters.ProjectId = firstProjectId;
                profitLoss = await GetProfitLossAsync(parameters, balanceItems);
                foreach (var item in profitLoss.Items)
                {
                    profitLoss.ItemsByProjects.Add(Mapper.Map<ProfitLossByProjectsViewModel>(item));
                }

                int itemIndex = 1;
                while (itemIndex < parameters.CompareItems.Count)
                {
                    parameters.ProjectId = parameters.CompareItems[itemIndex];
                    var itemProfitLoss = await GetProfitLossAsync(parameters, balanceItems);
                    if (itemProfitLoss.Items.Count == profitLoss.ItemsByProjects.Count)
                    {
                        for (int lineIndex = 0; lineIndex < profitLoss.ItemsByProjects.Count; lineIndex++)
                        {
                            CopyProjectValues(itemIndex,
                                itemProfitLoss.Items[lineIndex], profitLoss.ItemsByProjects[lineIndex]);
                        }
                    }

                    itemIndex++;
                }

                profitLoss.ViewMetadata = await _metadata.GetCompoundViewMetadataAsync(
                    ViewId.ProfitLossByProjects, ViewId.Project, parameters.CompareItems);
            }

            return profitLoss;
        }

        /// <summary>
        /// اطلاعات گزارش سود و زیان مقایسه ای را برای شعبه های انتخابی محاسبه کرده و برمی گرداند
        /// </summary>
        /// <param name="parameters">پارامترهای مورد نیاز برای تهیه گزارش</param>
        /// <param name="balanceItems">مجموعه اطلاعات مانده ابتدا و انتهای دوره گزارشگیری
        /// برای حساب های موجودی کالا - سیستم ادواری</param>
        /// <returns>اطلاعات گزارش سود و زیان مقایسه ای برای چند شعبه</returns>
        public async Task<ProfitLossViewModel> GetProfitLossByBranchesAsync(
            ProfitLossParameters parameters, IEnumerable<StartEndBalanceViewModel> balanceItems)
        {
            var profitLoss = new ProfitLossViewModel();
            if (parameters.CompareItems.Count > 0)
            {
                int firstBranchId = parameters.CompareItems[0];
                parameters.BranchId = firstBranchId;
                profitLoss = await GetProfitLossAsync(parameters, balanceItems);
                foreach (var item in profitLoss.Items)
                {
                    profitLoss.ItemsByBranches.Add(Mapper.Map<ProfitLossByBranchesViewModel>(item));
                }

                int itemIndex = 1;
                while (itemIndex < parameters.CompareItems.Count)
                {
                    parameters.BranchId = parameters.CompareItems[itemIndex];
                    var itemProfitLoss = await GetProfitLossAsync(parameters, balanceItems);
                    if (itemProfitLoss.Items.Count == profitLoss.ItemsByBranches.Count)
                    {
                        for (int lineIndex = 0; lineIndex < profitLoss.ItemsByBranches.Count; lineIndex++)
                        {
                            CopyBranchValues(itemIndex,
                                itemProfitLoss.Items[lineIndex], profitLoss.ItemsByBranches[lineIndex]);
                        }
                    }

                    itemIndex++;
                }

                profitLoss.ViewMetadata = await _metadata.GetCompoundViewMetadataAsync(
                    ViewId.ProfitLossByBranches, ViewId.Branch, parameters.CompareItems);
            }

            return profitLoss;
        }

        /// <summary>
        /// اطلاعات گزارش سود و زیان مقایسه ای را برای دوره های مالی انتخابی محاسبه کرده و برمی گرداند
        /// </summary>
        /// <param name="parameters">پارامترهای مورد نیاز برای تهیه گزارش</param>
        /// <param name="balanceItems">مجموعه اطلاعات مانده ابتدا و انتهای دوره گزارشگیری
        /// برای حساب های موجودی کالا - سیستم ادواری</param>
        /// <returns>اطلاعات گزارش سود و زیان مقایسه ای برای چند دوره مالی</returns>
        public async Task<ProfitLossViewModel> GetProfitLossByFiscalPeriodsAsync(
            ProfitLossParameters parameters, IEnumerable<StartEndBalanceViewModel> balanceItems)
        {
            var profitLoss = new ProfitLossViewModel();
            if (parameters.CompareItems.Count > 0)
            {
                int firstPeriodId = parameters.CompareItems[0];
                var adjusted = await GetAdjustedParameters(parameters, firstPeriodId);
                profitLoss = await GetProfitLossAsync(adjusted, balanceItems);
                foreach (var item in profitLoss.Items)
                {
                    profitLoss.ItemsByFiscalPeriods.Add(Mapper.Map<ProfitLossByFiscalPeriodsViewModel>(item));
                }

                int itemIndex = 1;
                while (itemIndex < parameters.CompareItems.Count)
                {
                    adjusted = await GetAdjustedParameters(parameters, parameters.CompareItems[itemIndex]);
                    var itemProfitLoss = await GetProfitLossAsync(adjusted, balanceItems);
                    if (itemProfitLoss.Items.Count == profitLoss.ItemsByFiscalPeriods.Count)
                    {
                        for (int lineIndex = 0; lineIndex < profitLoss.ItemsByFiscalPeriods.Count; lineIndex++)
                        {
                            CopyFiscalPeriodValues(itemIndex,
                                itemProfitLoss.Items[lineIndex], profitLoss.ItemsByFiscalPeriods[lineIndex]);
                        }
                    }

                    itemIndex++;
                }

                profitLoss.ViewMetadata = await _metadata.GetCompoundViewMetadataAsync(
                    ViewId.ProfitLossByFiscalPeriods, ViewId.FiscalPeriod, parameters.CompareItems);
            }

            return profitLoss;
        }

        private static void CopyCostCenterValues(int index,
            ProfitLossItemViewModel source, ProfitLossByCostCentersViewModel item)
        {
            string fieldName = String.Format("StartBalanceCostCenter{0}", index + 1);
            Reflector.CopyProperty(source, "StartBalance", item, fieldName);
            fieldName = String.Format("PeriodTurnoverCostCenter{0}", index + 1);
            Reflector.CopyProperty(source, "PeriodTurnover", item, fieldName);
            fieldName = String.Format("EndBalanceCostCenter{0}", index + 1);
            Reflector.CopyProperty(source, "EndBalance", item, fieldName);
            fieldName = String.Format("BalanceCostCenter{0}", index + 1);
            Reflector.CopyProperty(source, "Balance", item, fieldName);
        }

        private static void CopyProjectValues(int index,
            ProfitLossItemViewModel source, ProfitLossByProjectsViewModel item)
        {
            string fieldName = String.Format("StartBalanceProject{0}", index + 1);
            Reflector.CopyProperty(source, "StartBalance", item, fieldName);
            fieldName = String.Format("PeriodTurnoverProject{0}", index + 1);
            Reflector.CopyProperty(source, "PeriodTurnover", item, fieldName);
            fieldName = String.Format("EndBalanceProject{0}", index + 1);
            Reflector.CopyProperty(source, "EndBalance", item, fieldName);
            fieldName = String.Format("BalanceProject{0}", index + 1);
            Reflector.CopyProperty(source, "Balance", item, fieldName);
        }

        private static void CopyBranchValues(int index,
            ProfitLossItemViewModel source, ProfitLossByBranchesViewModel item)
        {
            string fieldName = String.Format("StartBalanceBranch{0}", index + 1);
            Reflector.CopyProperty(source, "StartBalance", item, fieldName);
            fieldName = String.Format("PeriodTurnoverBranch{0}", index + 1);
            Reflector.CopyProperty(source, "PeriodTurnover", item, fieldName);
            fieldName = String.Format("EndBalanceBranch{0}", index + 1);
            Reflector.CopyProperty(source, "EndBalance", item, fieldName);
            fieldName = String.Format("BalanceBranch{0}", index + 1);
            Reflector.CopyProperty(source, "Balance", item, fieldName);
        }

        private static void CopyFiscalPeriodValues(int index,
            ProfitLossItemViewModel source, ProfitLossByFiscalPeriodsViewModel item)
        {
            string fieldName = String.Format("StartBalanceFiscalPeriod{0}", index + 1);
            Reflector.CopyProperty(source, "StartBalance", item, fieldName);
            fieldName = String.Format("PeriodTurnoverFiscalPeriod{0}", index + 1);
            Reflector.CopyProperty(source, "PeriodTurnover", item, fieldName);
            fieldName = String.Format("EndBalanceFiscalPeriod{0}", index + 1);
            Reflector.CopyProperty(source, "EndBalance", item, fieldName);
            fieldName = String.Format("BalanceFiscalPeriod{0}", index + 1);
            Reflector.CopyProperty(source, "Balance", item, fieldName);
        }

        private async Task<IEnumerable<ProfitLossItemViewModel>> GetGrossProfitItemsAsync(
            ProfitLossParameters parameters, IEnumerable<StartEndBalanceViewModel> balanceItems)
        {
            var items = new List<ProfitLossItemViewModel>
            {
                new ProfitLossItemViewModel() { Group = AppStrings.GrossProfitCalculation }
            };

            var lines = new List<ProfitLossLineViewModel>();
            lines.AddRange(await GetCollectionLinesAsync(
                AccountCollectionId.FinalSales, parameters.ToDate, parameters));
            lines.AddRange(await GetCollectionLinesAsync(
                AccountCollectionId.SalesRefundDiscount, parameters.ToDate, parameters));
            var netRevenue = GetReportItem(
                lines, line => line.Credit - line.Debit, AppStrings.NetRevenue,
                parameters.FromDate, parameters.ToDate);

            ProfitLossItemViewModel productCost = (UserContext.InventoryMode == (int)InventoryMode.Perpetual)
                ? await GetProductCostItemAsync(parameters)
                : await GetPeriodicProductCostItemAsync(parameters, balanceItems);

            items.Add(netRevenue);
            items.Add(productCost);
            var grossProfit = netRevenue - productCost;
            grossProfit.Group = AppStrings.GrossProfit;
            items.Add(grossProfit);

            return items;
        }

        private async Task<ProfitLossItemViewModel> GetProductCostItemAsync(
            ProfitLossParameters parameters)
        {
            var lines = new List<ProfitLossLineViewModel>();
            lines.AddRange(await GetCollectionLinesAsync(
                AccountCollectionId.SoldProductCost, parameters.ToDate, parameters));
            var productCost = GetReportItem(
                lines, line => line.Debit - line.Credit, AppStrings.SoldProductCost,
                parameters.FromDate, parameters.ToDate);
            return productCost;
        }

        private async Task<ProfitLossItemViewModel> GetPeriodicProductCostItemAsync(
            ProfitLossParameters parameters, IEnumerable<StartEndBalanceViewModel> balanceItems)
        {
            var lines = new List<ProfitLossLineViewModel>();
            lines.AddRange(await GetStartCollectionLinesAsync(
                AccountCollectionId.ProductInventory, parameters));
            decimal openingInventory = lines.Sum(line => line.Debit - line.Credit);
            var inventoryLines = balanceItems
                .Select(item => new VoucherLineAmountsViewModel()
                {
                    Debit = item.StartBalanceDebit,
                    Credit = item.StartBalanceCredit
                });
            decimal startCost = await GetPeriodicProductCostBalanceAsync(
                parameters.FromDate, openingInventory, inventoryLines, parameters);
            var costItem = new ProfitLossItemViewModel()
            {
                Account = AppStrings.SoldProductCost,
                StartBalance = startCost,
                Balance = startCost
            };

            if (parameters.ToDate > parameters.FromDate)
            {
                inventoryLines = balanceItems
                    .Select(item => new VoucherLineAmountsViewModel()
                    {
                        Debit = item.EndBalanceDebit,
                        Credit = item.EndBalanceCredit
                    });
                decimal endCost = await GetPeriodicProductCostBalanceAsync(
                    parameters.ToDate, openingInventory, inventoryLines, parameters);
                costItem.EndBalance = endCost;
                costItem.Balance = 0.0M;
                costItem.PeriodTurnover = endCost - startCost;
            }

            return costItem;
        }

        private async Task<decimal> GetPeriodicProductCostBalanceAsync(
            DateTime date, decimal openingInventory, IEnumerable<VoucherLineAmountsViewModel> inventoryLines,
            ProfitLossParameters parameters)
        {
            // Product Cost = Opening Inventory + Net Purchase - Inventory
            var lines = new List<ProfitLossLineViewModel>();
            lines.AddRange(await GetCollectionLinesAsync(
                AccountCollectionId.FinalPurchase, date, parameters));
            lines.AddRange(await GetCollectionLinesAsync(
                AccountCollectionId.PurchaseRefundDiscount, date, parameters));
            decimal netPurchase = lines.Sum(line => line.Debit - line.Credit);
            decimal inventory = inventoryLines.Sum(item => item.Debit - item.Credit);
            return openingInventory + netPurchase - inventory;
        }

        private async Task<IEnumerable<ProfitLossItemViewModel>> GetOperationalCostItemsAsync(
            ProfitLossItemViewModel grossProfit, ProfitLossParameters parameters)
        {
            var items = new List<ProfitLossItemViewModel>
            {
                new ProfitLossItemViewModel() { Group = AppStrings.OperationalCost }
            };

            var costItems = new List<ProfitLossItemViewModel>();
            var lines = new List<ProfitLossLineViewModel>();
            lines.AddRange(await GetCollectionLinesAsync(
                AccountCollectionId.OperationalCosts, parameters.ToDate, parameters));
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
                Group = AppStrings.OperationalCostTotal,
                StartBalance = costItems.Sum(item => item.StartBalance),
                PeriodTurnover = costItems.Sum(item => item.PeriodTurnover),
                EndBalance = costItems.Sum(item => item.EndBalance),
                Balance = costItems.Sum(item => item.EndBalance)
            };
            items.Add(totalCost);
            var operationProfit = grossProfit - totalCost;
            operationProfit.Group = AppStrings.OperationalProfit;
            items.Add(operationProfit);

            return items;
        }

        private async Task<IEnumerable<ProfitLossItemViewModel>> GetOtherCostRevenueItemsAsync(
            ProfitLossItemViewModel operationProfit, ProfitLossParameters parameters)
        {
            var items = new List<ProfitLossItemViewModel>
            {
                new ProfitLossItemViewModel() { Group = AppStrings.OtherCostAndRevenue }
            };

            var costItems = new List<ProfitLossItemViewModel>();
            var lines = new List<ProfitLossLineViewModel>();
            lines.AddRange(await GetCollectionLinesAsync(
                AccountCollectionId.OtherCostRevenue, parameters.ToDate, parameters));
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
                Group = AppStrings.OtherCostAndRevenueNet,
                StartBalance = costItems.Sum(item => item.StartBalance),
                PeriodTurnover = costItems.Sum(item => item.PeriodTurnover),
                EndBalance = costItems.Sum(item => item.EndBalance),
                Balance = costItems.Sum(item => item.EndBalance)
            };
            items.Add(netCost);
            var beforeTax = operationProfit - netCost;
            beforeTax.Group = AppStrings.ProfitBeforeTax;
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
                    Group = AppStrings.NetProfit,
                    PeriodTurnover = beforeTax.PeriodTurnover - parameters.TaxAmount,
                    EndBalance = beforeTax.EndBalance - parameters.TaxAmount,
                    Balance = beforeTax.EndBalance - parameters.TaxAmount
                }
            };

            return items;
        }

        private async Task<IEnumerable<ProfitLossLineViewModel>> GetStartCollectionLinesAsync(
            AccountCollectionId collectionId, ProfitLossParameters parameters)
        {
            var lines = new List<ProfitLossLineViewModel>();
            int fiscalPeriodId = parameters.FiscalPeriodId ?? UserContext.FiscalPeriodId;
            DateTime startDate = await GetFiscalStartDateAsync(fiscalPeriodId);
            if (startDate != DateTime.MinValue)
            {
                lines.AddRange(
                    await GetCollectionLinesAsync(collectionId, startDate, startDate, parameters));
            }

            return lines;
        }

        private async Task<IEnumerable<ProfitLossLineViewModel>> GetCollectionLinesAsync(
            AccountCollectionId collectionId, DateTime until, ProfitLossParameters parameters)
        {
            var lines = new List<ProfitLossLineViewModel>();
            int fiscalPeriodId = parameters.FiscalPeriodId ?? UserContext.FiscalPeriodId;
            DateTime startDate = await GetFiscalStartDateAsync(fiscalPeriodId);
            if (startDate != DateTime.MinValue)
            {
                lines.AddRange(
                    await GetCollectionLinesAsync(collectionId, startDate, until, parameters));
            }

            return lines;
        }

        private async Task<IEnumerable<ProfitLossLineViewModel>> GetCollectionLinesAsync(
            AccountCollectionId collectionId, DateTime from, DateTime to, ProfitLossParameters parameters)
        {
            var accounts = await _utility.GetUsableAccountsAsync(collectionId);
            var accountIds = accounts.Select(acc => acc.Id);
            var repository = UnitOfWork.GetAsyncRepository<VoucherLine>();
            var linesQuery = repository
                .GetEntityQuery(line => line.Voucher, line => line.Account)
                .Where(line => line.Voucher.Date.IsBetween(from, to)
                    && accountIds.Contains(line.AccountId));
            int fiscalPeriodId = parameters.FiscalPeriodId ?? UserContext.FiscalPeriodId;
            linesQuery = linesQuery
                .Where(line => line.FiscalPeriodId == fiscalPeriodId);
            if (parameters.BranchId != null)
            {
                linesQuery = linesQuery
                    .Where(line => line.BranchId == parameters.BranchId);
            }
            else
            {
                var branchIds = GetChildTree(UserContext.BranchId);
                linesQuery = linesQuery
                    .Where(line => branchIds.Contains(line.BranchId));
            }

            if (!parameters.UseClosingTempVoucher)
            {
                linesQuery = linesQuery
                    .Where(line => line.Voucher.VoucherOriginId != (int)VoucherOriginId.ClosingTempAccounts);
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
                var projectRepository = UnitOfWork.GetAsyncRepository<Project>();
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

        private async Task<DateTime> GetFiscalStartDateAsync(int fiscalPeriodId)
        {
            var repository = UnitOfWork.GetAsyncRepository<FiscalPeriod>();
            return await repository
                .GetEntityQuery()
                .Where(fp => fp.Id == fiscalPeriodId)
                .Select(fp => fp.StartDate)
                .SingleOrDefaultAsync();
        }

        private async Task<ProfitLossParameters> GetAdjustedParameters(
            ProfitLossParameters parameters, int fiscalPeriodId)
        {
            var adjusted = parameters.GetCopy();
            var repository = UnitOfWork.GetAsyncRepository<FiscalPeriod>();
            var fiscalPeriod = await repository.GetByIDAsync(fiscalPeriodId);
            adjusted.FromDate = new DateTime(
                fiscalPeriod.StartDate.Year, parameters.FromDate.Month, parameters.FromDate.Day);
            adjusted.ToDate = new DateTime(
                fiscalPeriod.StartDate.Year, parameters.ToDate.Month, parameters.ToDate.Day, 11, 59, 59);
            adjusted.FiscalPeriodId = fiscalPeriodId;
            return adjusted;
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

        private readonly IMetadataRepository _metadata;
        private readonly IAccountCollectionUtility _utility;
    }
}
