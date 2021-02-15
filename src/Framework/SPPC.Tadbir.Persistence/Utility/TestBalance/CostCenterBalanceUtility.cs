using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Model;
using SPPC.Tadbir.Model.Finance;
using SPPC.Tadbir.ViewModel.Reporting;

namespace SPPC.Tadbir.Persistence.Utility
{
    internal class CostCenterBalanceUtility : CostCenterUtility, ITestBalanceUtility
    {
        public CostCenterBalanceUtility(IRepositoryContext context, IConfigRepository config,
            ISecureRepository repository, ITestBalanceHelper helper)
            : base(context, config, repository)
        {
            _helper = helper;
        }

        public async Task<IEnumerable<TestBalanceModeInfo>> GetLevelBalanceTypesAsync()
        {
            return await _helper.GetLevelBalanceTypesAsync(ViewId.CostCenter);
        }

        public async Task<IEnumerable<TestBalanceModeInfo>> GetChildBalanceTypesAsync()
        {
            return await _helper.GetChildBalanceTypesAsync(ViewId.CostCenter);
        }

        public IQueryable<VoucherLine> IncludeVoucherLineReference(IQueryable<VoucherLine> query)
        {
            return query.Include(line => line.CostCenter);
        }

        public Func<TestBalanceItemViewModel, bool> GetCurrentlevelFilter(int level)
        {
            return line => line.CostCenterId > 0 && line.CostCenterLevel == level;
        }

        public Func<TestBalanceItemViewModel, bool> GetUpperlevelFilter(int level)
        {
            return line => line.CostCenterId > 0 && line.CostCenterLevel >= level;
        }

        public async Task<TestBalanceItemViewModel> GetItemFromVoucherLineAsync(TestBalanceItemViewModel line, string fullCode)
        {
            var repository = UnitOfWork.GetAsyncRepository<CostCenter>();
            var costCenter = await repository.GetSingleByCriteriaAsync(cc => cc.FullCode == fullCode);
            return new TestBalanceItemViewModel()
            {
                BranchId = line.BranchId,
                BranchName = line.BranchName,
                CostCenterId = costCenter.Id,
                CostCenterName = costCenter.Name,
                CostCenterFullCode = costCenter.FullCode,
                CostCenterLevel = costCenter.Level
            };
        }

        public async Task<decimal> GetInitialBalanceAsync(int itemId, TestBalanceParameters parameters)
        {
            decimal balance = parameters.FromDate.HasValue
                ? await GetBalanceAsync(itemId, parameters.FromDate.Value)
                : await GetBalanceAsync(itemId, parameters.FromNo.Value);
            if ((parameters.Options & TestBalanceOptions.OpeningVoucherAsInitBalance) > 0)
            {
                balance += await GetBalanceAsync(itemId, VoucherOriginId.OpeningVoucher);
            }

            return balance;
        }

        public async Task<IEnumerable<TestBalanceItemViewModel>> GetZeroBalanceItemsAsync(
            IEnumerable<TestBalanceItemViewModel> items, TestBalanceMode mode)
        {
            var zeroItems = new List<TestBalanceItemViewModel>();
            var repository = UnitOfWork.GetAsyncRepository<CostCenter>();
            var usedIds = items
                .Where(item => item.CostCenterLevel == (short)mode)
                .Select(item => item.CostCenterId);
            var notUsed = await repository
                .GetEntityQuery(cc => cc.Branch)
                .Where(cc => !usedIds.Contains(cc.Id) && cc.Level == (short)mode)
                .Select(cc => new { cc.Id, cc.Name, cc.FullCode, cc.BranchId, BranchName = cc.Branch.Name })
                .ToListAsync();
            foreach (var notUsedItem in notUsed)
            {
                zeroItems.Add(new TestBalanceItemViewModel()
                {
                    CostCenterFullCode = notUsedItem.FullCode,
                    CostCenterId = notUsedItem.Id,
                    CostCenterName = notUsedItem.Name,
                    BranchId = notUsedItem.BranchId,
                    BranchName = notUsedItem.BranchName
                });
            }

            return zeroItems;
        }

        public IEnumerable<TestBalanceItemViewModel> FilterBalanceLines(
            IEnumerable<TestBalanceItemViewModel> lines, TreeEntity accountItem)
        {
            return lines.Where(line => line.CostCenterFullCode.StartsWith(accountItem.FullCode));
        }

        public int GetItemId(TestBalanceItemViewModel item)
        {
            return item.CostCenterId;
        }

        public IEnumerable<TestBalanceItemViewModel> GetSortedItems(IEnumerable<TestBalanceItemViewModel> items)
        {
            return items
                .OrderBy(item => item.CostCenterFullCode)
                .ToArray();
        }

        public int GetSourceList(TestBalanceFormat format)
        {
            return _helper.GetSourceList(format, "CostCenter");
        }

        protected override Func<TModel, string> GetGroupSelector<TModel>(int groupLevel)
        {
            int codeLength = GetLevelCodeLength(ViewId.CostCenter, groupLevel);
            return item => item.CostCenterFullCode.Substring(0, codeLength);
        }

        private readonly ITestBalanceHelper _helper;
    }
}
