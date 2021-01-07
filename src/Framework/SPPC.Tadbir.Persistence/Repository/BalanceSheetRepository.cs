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

            var assets = await GetLiquidAssetItemsAsync(parameters);
            var liabilities = await GetLiquidLiabilityItemsAsync(parameters);
            balanceSheet.Items.AddRange(GetMergedLiquidItems(assets, liabilities));

            assets = await GetNonLiquidAssetItemsAsync(parameters);
            liabilities = await GetNonLiquidLiabilityItemsAsync(parameters);
            balanceSheet.Items.AddRange(GetMergedNonLiquidItems(assets, liabilities));

            balanceSheet.Items.AddRange(await GetOwnerEquityItemsAsync(parameters));

            return balanceSheet;
        }

        private static IList<BalanceSheetItemViewModel> GetMergedLiquidItems(
            IList<BalanceSheetItemViewModel> assets, IList<BalanceSheetItemViewModel> liabilities)
        {
            throw new NotImplementedException();
        }

        private static IList<BalanceSheetItemViewModel> GetMergedNonLiquidItems(
            IList<BalanceSheetItemViewModel> assets, IList<BalanceSheetItemViewModel> liabilities)
        {
            throw new NotImplementedException();
        }

        private static string GetReportLabel(string label)
        {
            return String.Format("{0}:", label);
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
                    .Sum(line => line.Debit = line.Credit);
                var previousBalance = previousLines
                    .Where(line => line.AccountId == account.Key)
                    .Sum(line => line.Debit = line.Credit);
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
            var liquidAssets = new List<BalanceSheetItemViewModel>();
            var lines = await GetCollectionLinesAsync(
                AccountCollectionId.LiquidAssets, parameters.Date, parameters, UserContext.FiscalPeriodId);
            var previousLines = _previousFiscalPeriodId.HasValue
                ? await GetCollectionLinesAsync(
                    AccountCollectionId.LiquidAssets, parameters.Date, parameters, _previousFiscalPeriodId.Value)
                : new List<VoucherLineDetailViewModel>();
            liquidAssets.AddRange(GetReportItems(lines, previousLines, true));

            return liquidAssets;
        }

        private async Task<IList<BalanceSheetItemViewModel>> GetLiquidLiabilityItemsAsync(
            BalanceSheetParameters parameters)
        {
            var liquidLiabilities = new List<BalanceSheetItemViewModel>();
            var lines = await GetCollectionLinesAsync(
                AccountCollectionId.LiquidLiabilities, parameters.Date, parameters, UserContext.FiscalPeriodId);
            var previousLines = _previousFiscalPeriodId.HasValue
                ? await GetCollectionLinesAsync(
                    AccountCollectionId.LiquidLiabilities, parameters.Date, parameters, _previousFiscalPeriodId.Value)
                : new List<VoucherLineDetailViewModel>();
            liquidLiabilities.AddRange(GetReportItems(lines, previousLines, false));

            return liquidLiabilities;
        }

        private async Task<IList<BalanceSheetItemViewModel>> GetNonLiquidAssetItemsAsync(
            BalanceSheetParameters parameters)
        {
            throw new NotImplementedException();
        }

        private async Task<IList<BalanceSheetItemViewModel>> GetNonLiquidLiabilityItemsAsync(
            BalanceSheetParameters parameters)
        {
            throw new NotImplementedException();
        }

        private async Task<IList<BalanceSheetItemViewModel>> GetOwnerEquityItemsAsync(
            BalanceSheetParameters parameters)
        {
            throw new NotImplementedException();
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
