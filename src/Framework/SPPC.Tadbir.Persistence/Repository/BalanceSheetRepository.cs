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
using SPPC.Tadbir.Utility;
using SPPC.Tadbir.ViewModel.Finance;
using SPPC.Tadbir.ViewModel.Reporting;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای مدیریت و محاسبه گزارش ترازنامه را پیاده سازی می کند
    /// </summary>
    public class BalanceSheetRepository : RepositoryBase, IBalanceSheetRepository
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="context">امکانات مشترک مورد نیاز برای عملیات دیتابیسی را فراهم می کند</param>
        /// <param name="utility">امکان استفاده از مجموعه حسابها را فراهم می کند</param>
        public BalanceSheetRepository(IRepositoryContext context, IAccountCollectionUtility utility)
            : base(context)
        {
            _utility = utility;
        }

        /// <summary>
        /// اطلاعات گزارش ترازنامه را با توجه به پارامترهای داده شده محاسبه کرده و برمی گرداند
        /// </summary>
        /// <param name="parameters">پارامترهای مورد نظر برای محاسبه گزارش</param>
        /// <returns>اطلاعات محاسبه شده برای گزارش ترازنامه</returns>
        public async Task<BalanceSheetViewModel> GetBalanceSheetAsync(BalanceSheetParameters parameters)
        {
            var balanceSheet = new BalanceSheetViewModel();
            _previousFiscalPeriodId = await GetPreviousFiscalPeriodIdAsync();

            // Calculate and add liquid asset/liability items...
            balanceSheet.Items.Add(
                GetReportHeaderItem(AppStrings.LiquidAssets, AppStrings.LiquidLiabilities));
            var assets = await GetLiquidAssetItemsAsync(parameters);
            var liabilities = await GetLiquidLiabilityItemsAsync(parameters);
            var merged = GetMergedReportItems(assets, liabilities);
            var liquidSummary = GetReportSummaryItems(
                merged, AppStrings.LiquidAssetsSum, AppStrings.LiquidLiabilitiesSum);
            balanceSheet.Items.AddRange(merged);
            balanceSheet.Items.AddRange(liquidSummary);

            // Calculate and add non-liquid asset/liability items...
            balanceSheet.Items.Add(
                GetReportHeaderItem(AppStrings.NonLiquidAssets, AppStrings.NonLiquidLiabilities));
            assets = await GetNonLiquidAssetItemsAsync(parameters);
            liabilities = await GetNonLiquidLiabilityItemsAsync(parameters);
            merged = GetMergedReportItems(assets, liabilities);
            var nonLiquidSummary = GetReportSummaryItems(
                merged, AppStrings.NonLiquidAssetsSum, AppStrings.NonLiquidLiabilitiesSum);
            balanceSheet.Items.AddRange(merged);
            balanceSheet.Items.AddRange(nonLiquidSummary);

            // Calculate and add owner equity items...
            balanceSheet.Items.Add(
                GetReportHeaderItem(null, AppStrings.OwnerEquities));
            var equity = await GetOwnerEquityItemsAsync(parameters);
            var equitySummary = GetReportSummaryItems(equity, null, AppStrings.OwnerEquitiesSum);
            balanceSheet.Items.AddRange(equity);
            balanceSheet.Items.AddRange(equitySummary);

            // Calculate and add total item...
            var total = liquidSummary[1] + nonLiquidSummary[1] + equitySummary[1];
            total.Assets = AppStrings.AssetsSum;
            total.Liabilities = AppStrings.LiabilitiesOwnerEquitiesSum;
            balanceSheet.Items.Add(total);

            return balanceSheet;
        }

        private static IList<BalanceSheetItemViewModel> GetMergedReportItems(
            IList<BalanceSheetItemViewModel> assets, IList<BalanceSheetItemViewModel> liabilities)
        {
            var merged = new List<BalanceSheetItemViewModel>();
            int minSize = Math.Min(assets.Count, liabilities.Count);
            int maxSize = Math.Max(assets.Count, liabilities.Count);
            for (int i = 0; i < minSize; i++)
            {
                var asset = assets[i];
                var liability = liabilities[i];
                merged.Add(new BalanceSheetItemViewModel()
                {
                    Assets = asset.Assets,
                    AssetsBalance = asset.AssetsBalance,
                    AssetsPreviousBalance = asset.AssetsPreviousBalance,
                    Liabilities = liability.Liabilities,
                    LiabilitiesBalance = liability.LiabilitiesBalance,
                    LiabilitiesPreviousBalance = liability.LiabilitiesPreviousBalance
                });
            }

            for (int i = minSize; i < maxSize; i++)
            {
                if (i < assets.Count)
                {
                    merged.Add(assets[i].GetCopy());
                }
                else
                {
                    merged.Add(liabilities[i].GetCopy());
                }
            }

            return merged;
        }

        private static BalanceSheetItemViewModel GetReportHeaderItem(string asset, string liability)
        {
            return new BalanceSheetItemViewModel()
            {
                Assets = asset,
                Liabilities = liability
            };
        }

        private static BalanceSheetItemViewModel[] GetReportSummaryItems(
            IEnumerable<BalanceSheetItemViewModel> items, string asset, string liability)
        {
            return new BalanceSheetItemViewModel[]
            {
                new BalanceSheetItemViewModel(),
                new BalanceSheetItemViewModel()
                {
                    Assets = asset,
                    AssetsBalance = items.Sum(item => item.AssetsBalance),
                    AssetsPreviousBalance = items.Sum(item => item.AssetsPreviousBalance),
                    Liabilities = liability,
                    LiabilitiesBalance = items.Sum(item => item.LiabilitiesBalance),
                    LiabilitiesPreviousBalance = items.Sum(item => item.LiabilitiesPreviousBalance)
                },
                new BalanceSheetItemViewModel()
            };
        }

        private static IEnumerable<BalanceSheetItemViewModel> GetReportItems(
            IEnumerable<VoucherLineDetailViewModel> lines,
            IEnumerable<VoucherLineDetailViewModel> previousLines,
            bool isAsset)
        {
            var items = new List<BalanceSheetItemViewModel>();
            var accounts = lines.Select(line => new KeyValuePair<int, string>(line.AccountId, line.AccountName))
                .Concat(previousLines.Select(line => new KeyValuePair<int, string>(line.AccountId, line.AccountName)))
                .Distinct(new IdNameComparer());
            foreach (var account in accounts)
            {
                var item = new BalanceSheetItemViewModel();
                var balance = lines
                    .Where(line => line.AccountId == account.Key)
                    .Sum(line => line.Debit - line.Credit);
                var previousBalance = previousLines
                    .Where(line => line.AccountId == account.Key)
                    .Sum(line => line.Debit - line.Credit);
                if (isAsset)
                {
                    item.Assets = account.Value;
                    item.AssetsBalance = balance;
                    item.AssetsPreviousBalance = previousBalance;
                }
                else
                {
                    item.Liabilities = account.Value;
                    item.LiabilitiesBalance = balance;
                    item.LiabilitiesPreviousBalance = previousBalance;
                }

                items.Add(item);
            }

            return items;
        }

        private async Task<int?> GetPreviousFiscalPeriodIdAsync()
        {
            var repository = UnitOfWork.GetAsyncRepository<FiscalPeriod>();
            int previous = await repository.GetEntityQuery()
                .Where(fp => fp.Id < UserContext.FiscalPeriodId)
                .OrderByDescending(fp => fp.Id)
                .Select(fp => fp.Id)
                .FirstOrDefaultAsync();
            return previous > 0 ? (int?)previous : null;
        }

        private async Task<IList<BalanceSheetItemViewModel>> GetLiquidAssetItemsAsync(
            BalanceSheetParameters parameters)
        {
            return await GetReportItemsAsync(parameters, AccountCollectionId.LiquidAssets, true);
        }

        private async Task<IList<BalanceSheetItemViewModel>> GetLiquidLiabilityItemsAsync(
            BalanceSheetParameters parameters)
        {
            return await GetReportItemsAsync(parameters, AccountCollectionId.LiquidLiabilities, false);
        }

        private async Task<IList<BalanceSheetItemViewModel>> GetNonLiquidAssetItemsAsync(
            BalanceSheetParameters parameters)
        {
            return await GetReportItemsAsync(parameters, AccountCollectionId.NonLiquidAssets, true);
        }

        private async Task<IList<BalanceSheetItemViewModel>> GetNonLiquidLiabilityItemsAsync(
            BalanceSheetParameters parameters)
        {
            return await GetReportItemsAsync(parameters, AccountCollectionId.NonLiquidLiabilities, false);
        }

        private async Task<IList<BalanceSheetItemViewModel>> GetOwnerEquityItemsAsync(
            BalanceSheetParameters parameters)
        {
            return await GetReportItemsAsync(parameters, AccountCollectionId.OwnerEquities, false);
        }

        private async Task<IList<BalanceSheetItemViewModel>> GetReportItemsAsync(
            BalanceSheetParameters parameters, AccountCollectionId collectionId, bool isAsset)
        {
            var reportItems = new List<BalanceSheetItemViewModel>();
            var lines = await GetCollectionLinesAsync(
                collectionId, parameters.Date, parameters, UserContext.FiscalPeriodId);
            var previousLines = _previousFiscalPeriodId.HasValue
                ? await GetCollectionLinesAsync(
                    collectionId, parameters.Date, parameters, _previousFiscalPeriodId.Value)
                : new List<VoucherLineDetailViewModel>();
            reportItems.AddRange(GetReportItems(lines, previousLines, isAsset));

            return reportItems;
        }

        private async Task<IEnumerable<VoucherLineDetailViewModel>> GetCollectionLinesAsync(
            AccountCollectionId collectionId, DateTime until, BalanceSheetParameters parameters,
            int fiscalPeriodId)
        {
            var lines = new List<VoucherLineDetailViewModel>();
            DateTime startDate = await GetFiscalStartDateAsync(fiscalPeriodId);
            if (startDate != DateTime.MinValue)
            {
                lines.AddRange(
                    await GetCollectionLinesAsync(collectionId, startDate, until, parameters, fiscalPeriodId));
            }

            return lines;
        }

        private async Task<IEnumerable<VoucherLineDetailViewModel>> GetCollectionLinesAsync(
            AccountCollectionId collectionId, DateTime from, DateTime to, BalanceSheetParameters parameters,
            int fiscalPeriodId)
        {
            var accounts = await _utility.GetUsableAccountsAsync(collectionId);
            var accountIds = accounts.Select(acc => acc.Id);
            var branchIds = GetChildTree(UserContext.BranchId);
            var repository = UnitOfWork.GetAsyncRepository<VoucherLine>();
            var linesQuery = repository
                .GetEntityQuery(line => line.Voucher, line => line.Account)
                .Where(line => line.Voucher.SubjectType != (short)SubjectType.Draft
                    && line.Voucher.Date.IsBetween(from, to)
                    && accountIds.Contains(line.AccountId)
                    && branchIds.Contains(line.BranchId));
            linesQuery = linesQuery
                .Where(line => line.FiscalPeriodId == fiscalPeriodId);

            if (!parameters.UseClosingVoucher)
            {
                linesQuery = linesQuery
                    .Where(line => line.Voucher.VoucherOriginId != (int)VoucherOriginId.ClosingVoucher);
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
                .Select(line => Mapper.Map<VoucherLineDetailViewModel>(line))
                .Apply(parameters.GridOptions, false)
                .ApplyQuickFilter(parameters.GridOptions);
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
        private int? _previousFiscalPeriodId;
    }
}
