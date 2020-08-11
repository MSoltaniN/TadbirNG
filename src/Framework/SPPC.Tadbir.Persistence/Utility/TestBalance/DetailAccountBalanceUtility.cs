using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Model;
using SPPC.Tadbir.Model.Finance;
using SPPC.Tadbir.Values;
using SPPC.Tadbir.ViewModel.Reporting;

namespace SPPC.Tadbir.Persistence.Utility
{
    internal class DetailAccountBalanceUtility : DetailAccountUtility, ITestBalanceUtility
    {
        public DetailAccountBalanceUtility(IRepositoryContext context, IConfigRepository config,
            ITestBalanceHelper helper)
            : base(context, config)
        {
            _helper = helper;
        }

        public async Task<IEnumerable<TestBalanceModeInfo>> GetLevelBalanceTypesAsync()
        {
            return await _helper.GetLevelBalanceTypesAsync(ViewName.DetailAccount);
        }

        public async Task<IEnumerable<TestBalanceModeInfo>> GetChildBalanceTypesAsync()
        {
            return await _helper.GetChildBalanceTypesAsync(ViewName.DetailAccount);
        }

        public IQueryable<VoucherLine> IncludeVoucherLineReference(IQueryable<VoucherLine> query)
        {
            return query.Include(line => line.DetailAccount);
        }

        public Func<TestBalanceItemViewModel, bool> GetCurrentlevelFilter(int level)
        {
            return line => line.DetailAccountId > 0 && line.DetailAccountLevel == level;
        }

        public Func<TestBalanceItemViewModel, bool> GetUpperlevelFilter(int level)
        {
            return line => line.DetailAccountId > 0 && line.DetailAccountLevel >= level;
        }

        public async Task<TestBalanceItemViewModel> GetItemFromVoucherLineAsync(TestBalanceItemViewModel line, string fullCode)
        {
            var repository = UnitOfWork.GetAsyncRepository<DetailAccount>();
            var detailAccount = await repository.GetSingleByCriteriaAsync(facc => facc.FullCode == fullCode);
            return new TestBalanceItemViewModel()
            {
                BranchId = line.BranchId,
                BranchName = line.BranchName,
                DetailAccountId = detailAccount.Id,
                DetailAccountName = detailAccount.Name,
                DetailAccountFullCode = detailAccount.FullCode,
                DetailAccountLevel = detailAccount.Level
            };
        }

        public async Task<decimal> GetInitialBalanceAsync(int itemId, TestBalanceParameters parameters)
        {
            decimal balance = parameters.FromDate.HasValue
                ? await GetBalanceAsync(itemId, parameters.FromDate.Value)
                : await GetBalanceAsync(itemId, parameters.FromNo.Value);
            if ((parameters.Options & TestBalanceOptions.OpeningVoucherAsInitBalance) > 0)
            {
                balance += await GetBalanceAsync(itemId, VoucherOriginValue.OpeningVoucher);
            }

            return balance;
        }

        public async Task<IEnumerable<TestBalanceItemViewModel>> GetZeroBalanceItemsAsync(
            IEnumerable<TestBalanceItemViewModel> items, TestBalanceMode mode)
        {
            var zeroItems = new List<TestBalanceItemViewModel>();
            var repository = UnitOfWork.GetAsyncRepository<DetailAccount>();
            var usedIds = items
                .Where(item => item.DetailAccountLevel == (short)mode)
                .Select(item => item.DetailAccountId);
            var notUsed = await repository
                .GetEntityQuery(facc => facc.Branch)
                .Where(facc => !usedIds.Contains(facc.Id) && facc.Level == (short)mode)
                .Select(facc => new { facc.Id, facc.Name, facc.FullCode, facc.BranchId, BranchName = facc.Branch.Name })
                .ToListAsync();
            foreach (var notUsedItem in notUsed)
            {
                zeroItems.Add(new TestBalanceItemViewModel()
                {
                    DetailAccountFullCode = notUsedItem.FullCode,
                    DetailAccountId = notUsedItem.Id,
                    DetailAccountName = notUsedItem.Name,
                    BranchId = notUsedItem.BranchId,
                    BranchName = notUsedItem.BranchName
                });
            }

            return zeroItems;
        }

        public IEnumerable<TestBalanceItemViewModel> FilterBalanceLines(
            IEnumerable<TestBalanceItemViewModel> lines, TreeEntity accountItem)
        {
            return lines.Where(line => line.DetailAccountFullCode.StartsWith(accountItem.FullCode));
        }

        public int GetItemId(TestBalanceItemViewModel item)
        {
            return item.DetailAccountId;
        }

        public IEnumerable<TestBalanceItemViewModel> GetSortedItems(IEnumerable<TestBalanceItemViewModel> items)
        {
            return items
                .OrderBy(item => item.DetailAccountFullCode)
                .ToArray();
        }

        public int GetSourceList(TestBalanceFormat format)
        {
            return _helper.GetSourceList(format, "DetailAccount");
        }

        protected override Func<TModel, string> GetGroupSelector<TModel>(int groupLevel)
        {
            int codeLength = GetLevelCodeLength(ViewName.DetailAccount, groupLevel);
            return item => item.DetailAccountFullCode.Substring(0, codeLength);
        }

        private readonly ITestBalanceHelper _helper;
    }
}
