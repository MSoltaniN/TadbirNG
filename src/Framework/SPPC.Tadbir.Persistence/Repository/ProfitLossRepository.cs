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
    public class ProfitLossRepository : LoggingRepositoryBase, IProfitLossRepository
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="context">امکانات مشترک مورد نیاز را برای عملیات دیتابیسی فراهم می کند</param>
        /// <param name="system">امکانات مورد نیاز در دیتابیس های سیستمی را فراهم می کند</param>
        /// <param name="utility">امکان استفاده از مجموعه حسابها را فراهم می کند</param>
        public ProfitLossRepository(IRepositoryContext context, ISystemRepository system,
            IAccountCollectionUtility utility)
            : base(context, system.Logger)
        {
            _system = system;
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
            var profitLoss = await CalculateProfitLossAsync(parameters, balanceItems);
            await OnSourceActionAsync(parameters.GridOptions, SourceListId.ProfitLoss);
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
            var profitLossItems = new List<ProfitLossViewModel>();
            int viewId = parameters.FromDate.CompareWith(parameters.ToDate) == 0
                ? ViewId.ComparativeProfitLossSimple
                : ViewId.ComparativeProfitLoss;
            foreach (int itemId in parameters.CompareItems)
            {
                parameters.CostCenterId = itemId;
                profitLossItems.Add(await CalculateProfitLossAsync(parameters, balanceItems));
            }

            profitLoss.ComparativeItems.AddRange(MergeItems(profitLossItems));
            profitLoss.ViewMetadata = await Metadata.GetCompoundViewMetadataAsync(
                viewId, ViewId.CostCenter, parameters.CompareItems);

            await OnSourceActionAsync(parameters.GridOptions, SourceListId.ProfitLossByCostCenter);
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
            var profitLossItems = new List<ProfitLossViewModel>();
            int viewId = parameters.FromDate.CompareWith(parameters.ToDate) == 0
                ? ViewId.ComparativeProfitLossSimple
                : ViewId.ComparativeProfitLoss;
            foreach (int itemId in parameters.CompareItems)
            {
                parameters.ProjectId = itemId;
                profitLossItems.Add(await CalculateProfitLossAsync(parameters, balanceItems));
            }

            profitLoss.ComparativeItems.AddRange(MergeItems(profitLossItems));
            profitLoss.ViewMetadata = await Metadata.GetCompoundViewMetadataAsync(
                viewId, ViewId.Project, parameters.CompareItems);

            await OnSourceActionAsync(parameters.GridOptions, SourceListId.ProfitLossByProject);
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
            var profitLossItems = new List<ProfitLossViewModel>();
            int viewId = parameters.FromDate.CompareWith(parameters.ToDate) == 0
                ? ViewId.ComparativeProfitLossSimple
                : ViewId.ComparativeProfitLoss;
            foreach (int itemId in parameters.CompareItems)
            {
                parameters.BranchId = itemId;
                profitLossItems.Add(await CalculateProfitLossAsync(parameters, balanceItems));
            }

            profitLoss.ComparativeItems.AddRange(MergeItems(profitLossItems));
            profitLoss.ViewMetadata = await Metadata.GetCompoundViewMetadataAsync(
                viewId, ViewId.Branch, parameters.CompareItems);

            await OnSourceActionAsync(parameters.GridOptions, SourceListId.ProfitLossByBranch);
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
            var profitLossItems = new List<ProfitLossViewModel>();
            int viewId = parameters.FromDate.CompareWith(parameters.ToDate) == 0
                ? ViewId.ComparativeProfitLossSimple
                : ViewId.ComparativeProfitLoss;
            foreach (int itemId in parameters.CompareItems)
            {
                var adjusted = await GetAdjustedParametersAsync(parameters, itemId);
                profitLossItems.Add(await CalculateProfitLossAsync(adjusted, balanceItems));
            }

            profitLoss.ComparativeItems.AddRange(MergeItems(profitLossItems));
            profitLoss.ViewMetadata = await Metadata.GetCompoundViewMetadataAsync(
                viewId, ViewId.FiscalPeriod, parameters.CompareItems);

            await OnSourceActionAsync(parameters.GridOptions, SourceListId.ProfitLossByFiscalPeriod);
            return profitLoss;
        }

        internal override OperationSourceId OperationSource
        {
            get { return OperationSourceId.ProfitLoss; }
        }

        private IMetadataRepository Metadata
        {
            get { return _system.Metadata; }
        }

        private IConfigRepository Config
        {
            get { return _system.Config; }
        }

        private static void CopyItemValues(
            int index, ProfitLossItemViewModel source, ProfitLossByItemsViewModel target)
        {
            target.Group = source.Group;
            target.Account = source.Account;

            string fieldName = String.Format("StartBalanceItem{0}", index + 1);
            Reflector.CopyProperty(source, "StartBalance", target, fieldName);
            fieldName = String.Format("PeriodTurnoverItem{0}", index + 1);
            Reflector.CopyProperty(source, "PeriodTurnover", target, fieldName);
            fieldName = String.Format("EndBalanceItem{0}", index + 1);
            Reflector.CopyProperty(source, "EndBalance", target, fieldName);
            fieldName = String.Format("BalanceItem{0}", index + 1);
            Reflector.CopyProperty(source, "Balance", target, fieldName);
        }

        private static DateTime MapDateToYear(DateTime source, int year)
        {
            var projected = source.Date;
            if (source.IsLeapDay())
            {
                projected = new DateTime(year, source.Month, source.Day - 1);
            }
            else
            {
                projected = new DateTime(year, source.Month, source.Day);
            }

            return projected;
        }

        private static DateTime MapJalaliDateToYear(DateTime source, int year)
        {
            var projected = source.Date;
            var jalali = JalaliDateTime.FromDateTime(source);
            if (jalali.IsLeapDay())
            {
                projected = new JalaliDateTime(year, jalali.Month, jalali.Day - 1)
                    .ToGregorian();
            }
            else
            {
                projected = new JalaliDateTime(year, jalali.Month, jalali.Day)
                    .ToGregorian();
            }

            return projected;
        }

        private static IEnumerable<string> GetAccounts(IList<ProfitLossItemViewModel> items, string from, string to)
        {
            var accounts = new List<string>();
            int fromIndex = items.IndexOf(items.Where(item => item.Group == from).Single());
            int toIndex = items.IndexOf(items.Where(item => item.Group == to).Single());
            for (int index = fromIndex + 1; index < toIndex; index++)
            {
                accounts.Add(items[index].Account);
            }

            return accounts;
        }

        private async Task<ProfitLossViewModel> CalculateProfitLossAsync(
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
                .Where(line => line.Voucher.SubjectType != (short)SubjectType.Draft
                    && line.Voucher.Date.IsBetween(from, to)
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
                    .Where(line => line.Voucher.OriginId != (int)VoucherOriginId.ClosingTempAccounts);
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
                .Apply(parameters.GridOptions, false)
                .ApplyQuickFilter(parameters.GridOptions);
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

        private async Task<ProfitLossParameters> GetAdjustedParametersAsync(
            ProfitLossParameters parameters, int fiscalPeriodId)
        {
            var adjusted = parameters.GetCopy();
            var repository = UnitOfWork.GetAsyncRepository<FiscalPeriod>();
            var fiscalPeriod = await repository.GetByIDAsync(fiscalPeriodId);
            adjusted.FiscalPeriodId = fiscalPeriodId;

            int calendarType = await Config.GetCurrentCalendarAsync();
            if (calendarType == (int)CalendarType.Jalali)
            {
                var periodStart = JalaliDateTime.FromDateTime(fiscalPeriod.StartDate);
                adjusted.FromDate = MapJalaliDateToYear(parameters.FromDate, periodStart.Year);
                adjusted.ToDate = MapJalaliDateToYear(parameters.ToDate, periodStart.Year);
            }
            else
            {
                adjusted.FromDate = MapDateToYear(parameters.FromDate, fiscalPeriod.StartDate.Year);
                adjusted.ToDate = MapDateToYear(parameters.ToDate, fiscalPeriod.StartDate.Year);
            }

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

        private IEnumerable<ProfitLossByItemsViewModel> MergeItems(IList<ProfitLossViewModel> items)
        {
            var mergedItems = new List<ProfitLossByItemsViewModel>();
            var operationalCostAccounts = new List<string>();
            var otherCostAccounts = new List<string>();
            foreach (var item in items)
            {
                operationalCostAccounts.AddRange(
                    GetAccounts(item.Items, AppStrings.OperationalCost, AppStrings.OperationalCostTotal));
                otherCostAccounts.AddRange(
                    GetAccounts(item.Items, AppStrings.OtherCostAndRevenue, AppStrings.OtherCostAndRevenueNet));
            }

            operationalCostAccounts = operationalCostAccounts
                .Distinct()
                .OrderBy(acc => acc)
                .ToList();
            otherCostAccounts = otherCostAccounts
                .Distinct()
                .OrderBy(acc => acc)
                .ToList();
            int mergedItemCount = operationalCostAccounts.Count + otherCostAccounts.Count + 12;
            for (int i = 0; i < mergedItemCount; i++)
            {
                mergedItems.Add(new ProfitLossByItemsViewModel());
            }

            var operationalCostItems = new List<ProfitLossItemViewModel>();
            var otherCostItems = new List<ProfitLossItemViewModel>();
            int itemIndex = 0;
            foreach (var item in items)
            {
                int index = item.Items.IndexOf(
                    item.Items.Where(vm => vm.Group == AppStrings.OperationalCost).Single());
                foreach (string account in operationalCostAccounts)
                {
                    var accountItem = item.Items
                        .Where(vm => vm.Account == account)
                        .SingleOrDefault();
                    if (accountItem == null)
                    {
                        var newItem = new ProfitLossItemViewModel()
                        {
                            Account = account,
                            StartBalance = 0.0M,
                            PeriodTurnover = 0.0M,
                            EndBalance = 0.0M,
                            Balance = 0.0M
                        };
                        operationalCostItems.Add(newItem);
                    }
                    else
                    {
                        operationalCostItems.Add(accountItem);
                        item.Items.Remove(accountItem);
                    }
                }

                item.Items.InsertRange(index + 1, operationalCostItems);

                index = item.Items.IndexOf(
                    item.Items.Where(vm => vm.Group == AppStrings.OtherCostAndRevenue).Single());
                foreach (string account in otherCostAccounts)
                {
                    var accountItem = item.Items
                        .Where(vm => vm.Account == account)
                        .SingleOrDefault();
                    if (accountItem == null)
                    {
                        var newItem = new ProfitLossItemViewModel()
                        {
                            Account = account,
                            StartBalance = 0.0M,
                            PeriodTurnover = 0.0M,
                            EndBalance = 0.0M,
                            Balance = 0.0M
                        };
                        otherCostItems.Add(newItem);
                    }
                    else
                    {
                        otherCostItems.Add(accountItem);
                        item.Items.Remove(accountItem);
                    }
                }

                item.Items.InsertRange(index + 1, otherCostItems);
                for (int lineIndex = 0; lineIndex < item.Items.Count; lineIndex++)
                {
                    CopyItemValues(itemIndex, item.Items[lineIndex], mergedItems[lineIndex]);
                }

                operationalCostItems.Clear();
                otherCostItems.Clear();
                itemIndex++;
            }

            return mergedItems;
        }

        private readonly ISystemRepository _system;
        private readonly IAccountCollectionUtility _utility;
    }
}
