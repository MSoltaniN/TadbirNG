using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPPC.Framework.Extensions;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Model.Finance;
using SPPC.Tadbir.Persistence.Utility;
using SPPC.Tadbir.Resources;
using SPPC.Tadbir.ViewModel.Reporting;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    ///
    /// </summary>
    public class BalanceSheetRepositoryDirect : LoggingRepositoryBase, IBalanceSheetRepository
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="context">امکانات مشترک مورد نیاز را برای عملیات دیتابیسی فراهم می کند</param>
        /// <param name="system">امکانات مورد نیاز در دیتابیس های سیستمی را فراهم می کند</param>
        /// <param name="utility"></param>
        public BalanceSheetRepositoryDirect(IRepositoryContext context, ISystemRepository system,
            IReportDirectUtility utility)
            : base(context, system.Logger)
        {
            _context = context;
            _system = system;
            _utility = utility;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public async Task<BalanceSheetViewModel> GetBalanceSheetAsync(BalanceSheetParameters parameters)
        {
            var balanceSheet = new BalanceSheetViewModel();
            _previousFiscalPeriodId = await GetPreviousFiscalPeriodIdAsync();
            int length = _utility.GetLevelCodeLength(0);

            // Calculate and add liquid asset/liability items...
            balanceSheet.Items.Add(
                GetReportHeaderItem(AppStrings.LiquidAssets, AppStrings.LiquidLiabilities));
            var assets = await GetLiquidAssetItemsAsync(parameters, length);
            var liabilities = await GetLiquidLiabilityItemsAsync(parameters, length);
            var merged = GetMergedReportItems(assets.ToList(), liabilities.ToList());
            var liquidSummary = GetReportSummaryItems(
                merged, AppStrings.LiquidAssetsSum, AppStrings.LiquidLiabilitiesSum);
            balanceSheet.Items.AddRange(merged);
            balanceSheet.Items.AddRange(liquidSummary);

            // Calculate and add non-liquid asset/liability items...
            balanceSheet.Items.Add(
                GetReportHeaderItem(AppStrings.NonLiquidAssets, AppStrings.NonLiquidLiabilities));
            assets = await GetNonLiquidAssetItemsAsync(parameters, length);
            liabilities = await GetNonLiquidLiabilityItemsAsync(parameters, length);
            merged = GetMergedReportItems(assets.ToList(), liabilities.ToList());
            var nonLiquidSummary = GetReportSummaryItems(
                merged, AppStrings.NonLiquidAssetsSum, AppStrings.NonLiquidLiabilitiesSum);
            balanceSheet.Items.AddRange(merged);
            balanceSheet.Items.AddRange(nonLiquidSummary);

            // Calculate and add owner equity items...
            balanceSheet.Items.Add(
                GetReportHeaderItem(null, AppStrings.OwnerEquities));
            var equity = await GetOwnerEquityItemsAsync(parameters, length);
            var equitySummary = GetReportSummaryItems(equity, null, AppStrings.OwnerEquitiesSum);
            balanceSheet.Items.AddRange(equity);
            balanceSheet.Items.AddRange(equitySummary);

            // Calculate and add total item...
            var total = liquidSummary[1] + nonLiquidSummary[1] + equitySummary[1];
            total.Assets = AppStrings.AssetsSum;
            total.Liabilities = AppStrings.LiabilitiesOwnerEquitiesSum;
            balanceSheet.Items.Add(total);

            await OnSourceActionAsync(parameters.GridOptions, SourceListId.BalanceSheet);
            return balanceSheet;
        }

        internal override OperationSourceId OperationSource
        {
            get { return OperationSourceId.BalanceSheet; }
        }

        private static BalanceSheetItemViewModel GetReportHeaderItem(string asset, string liability)
        {
            return new BalanceSheetItemViewModel()
            {
                Assets = asset,
                Liabilities = liability
            };
        }

        private static IEnumerable<BalanceSheetItemViewModel> GetMergedReportItems(
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

        private async Task<DateTime> GetFiscalStartDateAsync(int fiscalPeriodId)
        {
            var repository = UnitOfWork.GetAsyncRepository<FiscalPeriod>();
            return await repository
                .GetEntityQuery()
                .Where(fp => fp.Id == fiscalPeriodId)
                .Select(fp => fp.StartDate)
                .SingleOrDefaultAsync();
        }

        private async Task<IEnumerable<BalanceSheetItemViewModel>> GetLiquidAssetItemsAsync(
            BalanceSheetParameters parameters, int length)
        {
            return await GetReportItemsAsync(
                length, parameters, AccountCollectionId.LiquidAssets, true);
        }

        private async Task<IEnumerable<BalanceSheetItemViewModel>> GetLiquidLiabilityItemsAsync(
            BalanceSheetParameters parameters, int length)
        {
            return await GetReportItemsAsync(
                length, parameters, AccountCollectionId.LiquidLiabilities, false);
        }

        private async Task<IEnumerable<BalanceSheetItemViewModel>> GetNonLiquidAssetItemsAsync(
            BalanceSheetParameters parameters, int length)
        {
            return await GetReportItemsAsync(
                length, parameters, AccountCollectionId.NonLiquidAssets, true);
        }

        private async Task<IEnumerable<BalanceSheetItemViewModel>> GetNonLiquidLiabilityItemsAsync(
            BalanceSheetParameters parameters, int length)
        {
            return await GetReportItemsAsync(
                length, parameters, AccountCollectionId.NonLiquidLiabilities, false);
        }

        private async Task<IEnumerable<BalanceSheetItemViewModel>> GetOwnerEquityItemsAsync(
            BalanceSheetParameters parameters, int length)
        {
            return await GetReportItemsAsync(
                length, parameters, AccountCollectionId.OwnerEquities, false);
        }

        private async Task<IEnumerable<BalanceSheetItemViewModel>> GetReportItemsAsync(
            int length, BalanceSheetParameters parameters, AccountCollectionId collectionId, bool isAsset)
        {
            DbConsole.ConnectionString = UnitOfWork.CompanyConnection;
            var items = new List<BalanceSheetItemViewModel>();
            DateTime startDate = await GetFiscalStartDateAsync(UserContext.FiscalPeriodId);
            if (startDate != DateTime.MinValue)
            {
                var accounts = _utility.GetUsableAccountsAsync(collectionId);
                if (accounts.Count() == 0)
                {
                    return items;
                }

                string accountList = String.Join(",", accounts.Select(acc => acc.Id));
                var query = await GetSheetQueryAsync(startDate, parameters, accountList, length);
                var result = DbConsole.ExecuteQuery(query.Query);
                items.AddRange(result.Rows
                    .Cast<DataRow>()
                    .Select(row => GetSheetItem(row, isAsset)));
                await SetPreviousBalancesAsync(length, parameters.GridOptions, items, isAsset);
                await SetItemNamesAsync(items, isAsset);
            }

            return items;
        }

        private async Task<ReportQuery> GetSheetQueryAsync(
            DateTime from, BalanceSheetParameters parameters, string accountList, int length)
        {
            var to = parameters.Date;
            var selectBuilder = new StringBuilder();
            selectBuilder.AppendFormat(BalanceSheetQuery.CollectionBalanceSelect, length);
            var filterBuilder = new StringBuilder();
            filterBuilder.AppendFormat(BalanceSheetQuery.CollectionBalanceWhere,
                from.ToShortDateString(false), to.ToShortDateString(false), accountList);

            if (parameters.CostCenterId != null)
            {
                var costRepository = UnitOfWork.GetAsyncRepository<CostCenter>();
                var fullCode = await costRepository
                    .GetEntityQuery()
                    .Where(cc => cc.Id == parameters.CostCenterId)
                    .Select(cc => cc.FullCode)
                    .SingleOrDefaultAsync();

                selectBuilder.AppendLine();
                selectBuilder.AppendLine(
                    "    INNER JOIN [Finance].[CostCenter] cc ON vl.CostCenterID = cc.CostCenterID");
                filterBuilder.AppendFormat(" AND cc.FullCode LIKE '{0}%'", fullCode);
            }

            if (parameters.ProjectId != null)
            {
                var projectRepository = UnitOfWork.GetAsyncRepository<Project>();
                var fullCode = await projectRepository
                    .GetEntityQuery()
                    .Where(prj => prj.Id == parameters.CostCenterId)
                    .Select(prj => prj.FullCode)
                    .SingleOrDefaultAsync();

                selectBuilder.AppendLine();
                selectBuilder.AppendLine(
                    "    INNER JOIN [Finance].[Project] prj ON vl.ProjectID = prj.ProjectID");
                filterBuilder.AppendFormat(" AND prj.FullCode LIKE '{0}%'", fullCode);
            }

            var queryBuilder = new StringBuilder(selectBuilder.ToString());
            queryBuilder.Append(filterBuilder.ToString());
            queryBuilder.AppendFormat(BalanceSheetQuery.CollectionBalanceEnd, length);
            var query = new ReportQuery(queryBuilder.ToString());
            query.SetFilter(GetEnvironmentFilters(parameters));
            return query;
        }

        private BalanceSheetItemViewModel GetSheetItem(DataRow row, bool isAsset)
        {
            string name = _utility.ValueOrDefault(row, "FullCode");
            decimal balance = _utility.ValueOrDefault<decimal>(row, "Balance");
            return new BalanceSheetItemViewModel()
            {
                Assets = isAsset ? name : null,
                AssetsBalance = isAsset ? balance : (decimal?)null,
                Liabilities = isAsset ? null : name,
                LiabilitiesBalance = isAsset ? (decimal?)null : balance
            };
        }

        private async Task SetPreviousBalancesAsync(int length, GridOptions gridOptions,
            IEnumerable<BalanceSheetItemViewModel> items, bool isAsset)
        {
            int fpId = UserContext.FiscalPeriodId;
            var fullCodes = isAsset
                ? items.Where(item => !String.IsNullOrEmpty(item.Assets))
                       .Select(item => item.Assets)
                : items.Where(item => !String.IsNullOrEmpty(item.Liabilities))
                       .Select(item => item.Liabilities);
            string filter = String.Join(
                " OR ", fullCodes.Select(code => String.Format("acc.FullCode LIKE '{0}%'", code)));

            int previousFpId = _previousFiscalPeriodId ?? 0;
            var result = new Dictionary<string, decimal>();
            if (await _utility.HasSpecialVoucherAsync(fpId, VoucherOriginId.OpeningVoucher))
            {
                result = GetBalanceQueryResult(
                    AccountItemQuery.SpecialVoucherBalanceByCode, length,
                    (int)VoucherOriginId.OpeningVoucher, filter, gridOptions, fpId);
            }
            else if (previousFpId > 0
                && await _utility.HasSpecialVoucherAsync(previousFpId, VoucherOriginId.ClosingVoucher))
            {
                result = GetBalanceQueryResult(
                    AccountItemQuery.SpecialVoucherBalanceByCode, length,
                    (int)VoucherOriginId.ClosingVoucher, filter, gridOptions, previousFpId);
            }
            else if (previousFpId > 0)
            {
                var endDate = _utility.GetFiscalPeriodEndAsync(previousFpId);
                result = GetBalanceQueryResult(
                    AccountItemQuery.PreviousBalanceByCode, length, endDate, filter, gridOptions, previousFpId);
            }
            else
            {
                foreach (string fullCode in fullCodes)
                {
                    result.Add(fullCode, 0.0M);
                }
            }

            foreach (var item in items)
            {
                if ((!String.IsNullOrEmpty(item.Assets) && result.ContainsKey(item.Assets))
                    || (!String.IsNullOrEmpty(item.Liabilities) && result.ContainsKey(item.Liabilities)))
                {
                    item.AssetsPreviousBalance = isAsset ? result[item.Assets] : (decimal?)null;
                    item.LiabilitiesPreviousBalance = isAsset ? (decimal?)null : result[item.Liabilities];
                }
                else
                {
                    item.AssetsPreviousBalance = isAsset ? 0.0M : (decimal?)null;
                    item.LiabilitiesPreviousBalance = isAsset ? (decimal?)null : 0.0M;
                }
            }
        }

        private async Task SetItemNamesAsync(IList<BalanceSheetItemViewModel> items, bool isAsset)
        {
            var fullCodes = isAsset
                ? items.Where(item => !String.IsNullOrEmpty(item.Assets))
                       .Select(item => item.Assets)
                : items.Where(item => !String.IsNullOrEmpty(item.Liabilities))
                       .Select(item => item.Liabilities);
            var repository = UnitOfWork.GetAsyncRepository<Account>();
            var accounts = await repository
                .GetEntityQuery()
                .Where(acc => fullCodes.Contains(acc.FullCode))
                .Select(acc => new KeyValuePair<string, string>(acc.FullCode, acc.Name))
                .ToListAsync();
            var accountMap = new Dictionary<string, string>();
            foreach (var account in accounts)
            {
                accountMap.Add(account.Key, account.Value);
            }

            foreach (var item in items)
            {
                if (isAsset)
                {
                    item.Assets = accountMap[item.Assets];
                }
                else
                {
                    item.Liabilities = accountMap[item.Liabilities];
                }
            }
        }

        private Dictionary<string, decimal> GetBalanceQueryResult(
            string query, int length, object value, string filter, GridOptions gridOptions, int fpId)
        {
            DbConsole.ConnectionString = UnitOfWork.CompanyConnection;
            string command = String.Format(query, length, "Account", "Account", value, filter);
            command = command.Replace(" AND ()", String.Empty);
            var balanceQuery = new ReportQuery(command);
            balanceQuery.SetFilter(_utility.GetEnvironmentFilters(gridOptions, fpId));
            var result = DbConsole.ExecuteQuery(balanceQuery.Query);

            var balanceMap = new Dictionary<string, decimal>(result.Rows.Count);
            foreach (DataRow row in result.Rows)
            {
                balanceMap.Add(_utility.ValueOrDefault(row, "FullCode"),
                    _utility.ValueOrDefault<decimal>(row, "Balance"));
            }

            return balanceMap;
        }

        private string GetEnvironmentFilters(BalanceSheetParameters parameters)
        {
            var builder = new StringBuilder(
                _utility.GetEnvironmentFilters(parameters.GridOptions, UserContext.FiscalPeriodId));
            if (!parameters.UseClosingVoucher)
            {
                builder.Append("AND VoucherOriginID != 4");
            }

            return builder.ToString();
        }

        private readonly IRepositoryContext _context;
        private readonly ISystemRepository _system;
        private readonly IReportDirectUtility _utility;
        private int? _previousFiscalPeriodId;
    }
}
