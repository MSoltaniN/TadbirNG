using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPPC.Framework.Common;
using SPPC.Framework.Extensions;
using SPPC.Tadbir.Configuration.Models;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Model.Finance;
using SPPC.Tadbir.Persistence.Utility;
using SPPC.Tadbir.Resources;
using SPPC.Tadbir.ViewModel.Finance;
using SPPC.Tadbir.ViewModel.Reporting;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    ///
    /// </summary>
    public class ProfitLossRepositoryDirect : LoggingRepositoryBase, IProfitLossRepository
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="context">امکانات مشترک مورد نیاز را برای عملیات دیتابیسی فراهم می کند</param>
        /// <param name="system">امکانات مورد نیاز در دیتابیس های سیستمی را فراهم می کند</param>
        /// <param name="utility">امکان استفاده از مجموعه حسابها را فراهم می کند</param>
        public ProfitLossRepositoryDirect(IRepositoryContext context, ISystemRepository system,
            IReportDirectUtility utility)
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
            var profitLoss = new ProfitLossViewModel();
            if (parameters.GridOptions.Operation != (int)OperationId.Print)
            {
                profitLoss = await CalculateProfitLossAsync(parameters, balanceItems);
            }

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
            if (parameters.GridOptions.Operation != (int)OperationId.Print)
            {
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
            }

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
            if (parameters.GridOptions.Operation != (int)OperationId.Print)
            {
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
            }

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
            if (parameters.GridOptions.Operation != (int)OperationId.Print)
            {
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
            }

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
            if (parameters.GridOptions.Operation != (int)OperationId.Print)
            {
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
            }

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

        private static void ApplyPeriodStartOption(
            ProfitLossParameters parameters, StringBuilder queryBuilder)
        {
            if (parameters.StartTurnoverAsInitBalance)
            {
                string predicate = String.Format(
                    "{0} >= '{1}'", DateExp, parameters.FromDate.ToShortDateString(false));
                string newPredicate = String.Format(
                    "{0} > '{1}'", DateExp, parameters.FromDate.ToShortDateString(false));
                queryBuilder.Replace(predicate, newPredicate);

                predicate = String.Format(
                    "{0} < '{1}'", DateExp, parameters.FromDate.ToShortDateString(false));
                newPredicate = String.Format(
                    "{0} <= '{1}'", DateExp, parameters.FromDate.ToShortDateString(false));
                queryBuilder.Replace(predicate, newPredicate);
            }
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

        private async Task<ProfitLossViewModel> CalculateProfitLossAsync(
            ProfitLossParameters parameters, IEnumerable<StartEndBalanceViewModel> balanceItems)
        {
            var option = await Config.GetConfigByTypeAsync<FinanceReportConfig>();
            parameters.StartTurnoverAsInitBalance = option.StartTurnoverAsInitBalance;

            DbConsole.ConnectionString = UnitOfWork.CompanyConnection;
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
                new ProfitLossItemViewModel() { Group = AppStrings.GrossProfitCalculation, StartBalance = null }
            };

            var netRevenue = await GetNetRevenueItemAsync(parameters);
            var productCost = (UserContext.InventoryMode == (int)InventoryMode.Perpetual)
                ? await GetProductCostItemAsync(parameters)
                : await GetPeriodicProductCostItemAsync(parameters, balanceItems);

            items.Add(netRevenue);
            items.Add(productCost);
            var grossProfit = netRevenue - productCost;
            grossProfit.Group = AppStrings.GrossProfit;
            items.Add(grossProfit);

            return items;
        }

        private async Task<ProfitLossItemViewModel> GetNetRevenueItemAsync(
            ProfitLossParameters parameters)
        {
            var accounts = new List<AccountItemBriefViewModel>();
            accounts.AddRange(_utility.GetUsableAccounts(
                AccountCollectionId.FinalSales, false, parameters.BranchId));
            accounts.AddRange(_utility.GetUsableAccounts(
                AccountCollectionId.SalesRefundDiscount, false, parameters.BranchId));

            var items = await GetReportItemsAsync(
                accounts, parameters, ProfitLossQuery.BalanceTotalSelect,
                ProfitLossQuery.BalanceTotalEnd, ProfitLossQuery.InitBalanceTotalEnd,
                CreditDebit, AppStrings.NetRevenue);
            var netRevenue = items.Count() > 0
                ? items.First()
                : new ProfitLossItemViewModel()
                {
                    Account = AppStrings.NetRevenue,
                    StartBalance = 0.0M,
                    PeriodTurnover = 0.0M,
                    EndBalance = 0.0M,
                    Balance = 0.0M
                };
            return netRevenue;
        }

        private async Task<ProfitLossItemViewModel> GetProductCostItemAsync(
            ProfitLossParameters parameters)
        {
            var accounts = new List<AccountItemBriefViewModel>();
            accounts.AddRange(_utility.GetUsableAccounts(
                AccountCollectionId.SoldProductCost, false, parameters.BranchId));

            var items = await GetReportItemsAsync(
                accounts, parameters, ProfitLossQuery.BalanceTotalSelect,
                ProfitLossQuery.BalanceTotalEnd, ProfitLossQuery.InitBalanceTotalEnd,
                DebitCredit, AppStrings.SoldProductCost);
            var productCost = items.Count() > 0
                ? items.First()
                : new ProfitLossItemViewModel()
                {
                    Account = AppStrings.SoldProductCost,
                    StartBalance = 0.0M,
                    PeriodTurnover = 0.0M,
                    EndBalance = 0.0M,
                    Balance = 0.0M
                };
            return productCost;
        }

        private async Task<ProfitLossItemViewModel> GetPeriodicProductCostItemAsync(
            ProfitLossParameters parameters, IEnumerable<StartEndBalanceViewModel> balanceItems)
        {
            var accounts = new List<AccountItemBriefViewModel>();
            accounts.AddRange(_utility.GetUsableAccounts(
                AccountCollectionId.ProductInventory, false, parameters.BranchId));
            int fiscalPeriodId = parameters.FiscalPeriodId ?? UserContext.FiscalPeriodId;
            var startDate = await _utility.GetFiscalPeriodStartAsync(fiscalPeriodId);
            var paramCopy = parameters.GetCopy();
            paramCopy.FromDate = startDate;
            paramCopy.ToDate = startDate;

            var inventoryItems = await GetReportItemsAsync(
                accounts, paramCopy, ProfitLossQuery.BalanceTotalSelect,
                ProfitLossQuery.BalanceTotalEnd, ProfitLossQuery.InitBalanceTotalEnd,
                DebitCredit, AppStrings.NetRevenue);

            var inventoryItem = inventoryItems.FirstOrDefault();
            decimal openingInventory = inventoryItem != null ? inventoryItem.PeriodTurnover.Value : 0.0M;
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
            var accounts = new List<AccountItemBriefViewModel>();
            accounts.AddRange(_utility.GetUsableAccounts(
                AccountCollectionId.FinalPurchase, false, parameters.BranchId));
            accounts.AddRange(_utility.GetUsableAccounts(
                AccountCollectionId.PurchaseRefundDiscount, false, parameters.BranchId));

            int fiscalPeriodId = parameters.FiscalPeriodId ?? UserContext.FiscalPeriodId;
            var startDate = await _utility.GetFiscalPeriodStartAsync(fiscalPeriodId);
            var paramCopy = parameters.GetCopy();
            paramCopy.FromDate = startDate;
            paramCopy.ToDate = date;

            var purchaseItems = await GetReportItemsAsync(
                accounts, paramCopy, ProfitLossQuery.BalanceTotalSelect,
                ProfitLossQuery.BalanceTotalEnd, ProfitLossQuery.InitBalanceTotalEnd,
                DebitCredit, AppStrings.NetRevenue);

            var purchaseItem = purchaseItems.FirstOrDefault();
            decimal netPurchase = purchaseItem != null ? purchaseItem.PeriodTurnover.Value : 0.0M;
            decimal inventory = inventoryLines.Sum(item => item.Debit - item.Credit);
            return openingInventory + netPurchase - inventory;
        }

        private async Task<IEnumerable<ProfitLossItemViewModel>> GetOperationalCostItemsAsync(
            ProfitLossItemViewModel grossProfit, ProfitLossParameters parameters)
        {
            var items = new List<ProfitLossItemViewModel>
            {
                new ProfitLossItemViewModel() { Group = AppStrings.OperationalCost, StartBalance = null }
            };

            var accounts = _utility.GetUsableAccounts(
                AccountCollectionId.OperationalCosts, false, parameters.BranchId);

            var costItems = await GetReportItemsAsync(
                accounts, parameters, ProfitLossQuery.BalanceByAccountSelect,
                ProfitLossQuery.BalanceByAccountEnd, ProfitLossQuery.InitBalanceByAccountEnd,
                DebitCredit);

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
                new ProfitLossItemViewModel() { Group = AppStrings.OtherCostAndRevenue, StartBalance = null }
            };

            var accounts = _utility.GetUsableAccounts(
                AccountCollectionId.OtherCostRevenue, false, parameters.BranchId);

            var costItems = await GetReportItemsAsync(
                accounts, parameters, ProfitLossQuery.BalanceByAccountSelect,
                ProfitLossQuery.BalanceByAccountEnd, ProfitLossQuery.InitBalanceByAccountEnd,
                DebitCredit);

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

        private async Task<IEnumerable<ProfitLossItemViewModel>> GetReportItemsAsync(
            IEnumerable<AccountItemBriefViewModel> accounts, ProfitLossParameters parameters,
            string select, string end, string initEnd, string balanceFunc, string account = null)
        {
            string accountFilter = String.Join(" OR ",
                accounts.Select(acc => String.Format("acc.FullCode LIKE '{0}%'", acc.FullCode)));
            if (String.IsNullOrEmpty(accountFilter))
            {
                return new List<ProfitLossItemViewModel>();
            }

            var filterParams = new List<string>()
                {
                    parameters.FromDate.ToShortDateString(false),
                    parameters.ToDate.ToShortDateString(false),
                    accountFilter
                };
            var query = await GetProfitLossQueryAsync(
                select, end, balanceFunc, filterParams.ToArray(), parameters);
            var turnover = DbConsole.ExecuteQuery(query.Query);

            filterParams.RemoveAt(1);       // Init balance doesn't need ToDate parameter
            query = await GetProfitLossQueryAsync(
                select, initEnd, balanceFunc, filterParams.ToArray(), parameters);
            var initBalance = DbConsole.ExecuteQuery(query.Query);

            return GetReportItems(initBalance, turnover, account);
        }

        private IEnumerable<ProfitLossItemViewModel> GetReportItems(
            DataTable initBalance, DataTable turnover, string account = null)
        {
            var items = new List<ProfitLossItemViewModel>();
            foreach (var row in initBalance.Rows.Cast<DataRow>())
            {
                string accountName = _utility.ValueOrDefault(row, "Name");
                items.Add(new ProfitLossItemViewModel()
                {
                    Account = accountName ?? account,
                    StartBalance = _utility.ValueOrDefault<decimal>(row, "Balance")
                });
            }

            foreach (var row in turnover.Rows.Cast<DataRow>())
            {
                var balance = _utility.ValueOrDefault<decimal>(row, "Balance");
                string accountName = _utility.ValueOrDefault(row, "Name") ?? account;
                var item = items
                    .Where(pli => pli.Account == accountName)
                    .FirstOrDefault();
                if (item != null)
                {
                    item.PeriodTurnover = balance;
                    item.EndBalance = item.StartBalance + item.PeriodTurnover;
                    item.Balance = item.StartBalance + item.PeriodTurnover;
                }
                else
                {
                    items.Add(new ProfitLossItemViewModel()
                    {
                        Account = accountName,
                        PeriodTurnover = balance,
                        EndBalance = balance,
                        Balance = balance
                    });
                }
            }

            return items;
        }

        private async Task<ReportQuery> GetProfitLossQueryAsync(
            string select, string end, string balanceFunc, string[] filterParams,
            ProfitLossParameters parameters)
        {
            int fiscalPeriodId = parameters.FiscalPeriodId ?? UserContext.FiscalPeriodId;
            var filterBuilder = new StringBuilder(
                _utility.GetEnvironmentFilters(parameters.GridOptions, fiscalPeriodId));
            var queryBuilder = new StringBuilder();
            queryBuilder.AppendFormat(select, balanceFunc);
            queryBuilder.AppendLine();

            if (!parameters.UseClosingTempVoucher)
            {
                filterBuilder.AppendFormat(
                    " AND v.OriginID <> {0}", (int)VoucherOriginId.ClosingTempAccounts);
            }

            if (parameters.CostCenterId != null)
            {
                var costRepository = UnitOfWork.GetAsyncRepository<CostCenter>();
                var fullCode = await costRepository
                    .GetEntityQuery()
                    .Where(cc => cc.Id == parameters.CostCenterId)
                    .Select(cc => cc.FullCode)
                    .SingleOrDefaultAsync();
                queryBuilder.AppendLine(
                    "    INNER JOIN [Finance].[CostCenter] cc ON vl.CostCenterID = cc.CostCenterID");
                filterBuilder.AppendFormat(" AND cc.FullCode LIKE '{0}%'", fullCode);
            }

            if (parameters.ProjectId != null)
            {
                var projectRepository = UnitOfWork.GetAsyncRepository<Project>();
                var fullCode = await projectRepository
                    .GetEntityQuery()
                    .Where(prj => prj.Id == parameters.ProjectId)
                    .Select(prj => prj.FullCode)
                    .SingleOrDefaultAsync();
                queryBuilder.AppendLine(
                    "    INNER JOIN [Finance].[Project] prj ON vl.ProjectID = prj.ProjectID");
                filterBuilder.AppendFormat(" AND prj.FullCode LIKE '{0}%'", fullCode);
            }

            queryBuilder.AppendFormat(end, filterParams);
            ApplyPeriodStartOption(parameters, queryBuilder);
            var reportQuery = new ReportQuery(queryBuilder.ToString());
            reportQuery.SetFilter(filterBuilder.ToString());
            return reportQuery;
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

        private async Task<ProfitLossParameters> GetAdjustedParametersAsync(
            ProfitLossParameters parameters, int fiscalPeriodId)
        {
            var adjusted = parameters.GetCopy();
            var repository = UnitOfWork.GetAsyncRepository<FiscalPeriod>();
            var fiscalPeriod = await repository.GetByIDAsync(fiscalPeriodId);
            adjusted.FiscalPeriodId = fiscalPeriodId;

            var calendarType = await Config.GetCurrentCalendarAsync();
            if (calendarType == CalendarType.Jalali)
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

        private const string DateExp = "CAST(v.Date AS date)";
        private const string CreditDebit = "vl.Credit - vl.Debit";
        private const string DebitCredit = "vl.Debit - vl.Credit";
        private readonly ISystemRepository _system;
        private readonly IReportDirectUtility _utility;
    }
}
