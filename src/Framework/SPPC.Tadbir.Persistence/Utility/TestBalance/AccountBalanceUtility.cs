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
    internal class AccountBalanceUtility : AccountUtility, ITestBalanceUtility
    {
        public AccountBalanceUtility(IRepositoryContext context, IConfigRepository config,
            ISecureRepository repository, ITestBalanceHelper helper)
            : base(context, config, repository)
        {
            _helper = helper;
        }

        public async Task<IEnumerable<TestBalanceModeInfo>> GetLevelBalanceTypesAsync()
        {
            return await _helper.GetLevelBalanceTypesAsync(ViewId.Account);
        }

        public async Task<IEnumerable<TestBalanceModeInfo>> GetChildBalanceTypesAsync()
        {
            var lookup = new List<TestBalanceModeInfo>();
            var fullConfig = await Config.GetViewTreeConfigByViewAsync(ViewId.Account);
            var usedLevels = fullConfig.Current
                .Levels
                .Where(level => level.IsEnabled && level.IsUsed)
                .ToList();
            int typeId = 0;
            lookup.Add(new TestBalanceModeInfo()
            {
                Id = typeId++,
                Name = "SubsidiariesOfLedger",
                Level = 2,
                IsDetail = true
            });
            lookup.Add(new TestBalanceModeInfo()
            {
                Id = typeId++,
                Name = "DetailsOfSubsidiary",
                Level = 3,
                IsDetail = true
            });
            for (int index = 2; index < usedLevels.Count - 1; index++)
            {
                lookup.Add(new TestBalanceModeInfo()
                {
                    Id = typeId++,
                    Name = usedLevels[index].Name,
                    Level = usedLevels[index].No + 1,
                    IsDetail = true
                });
            }

            return lookup;
        }

        public IQueryable<VoucherLine> IncludeVoucherLineReference(IQueryable<VoucherLine> query)
        {
            return query.Include(line => line.Account);
        }

        public Func<TestBalanceItemViewModel, bool> GetCurrentlevelFilter(int level)
        {
            return line => line.AccountLevel == level;
        }

        public Func<TestBalanceItemViewModel, bool> GetUpperlevelFilter(int level)
        {
            return line => line.AccountLevel >= level;
        }

        public async Task<TestBalanceItemViewModel> GetItemFromVoucherLineAsync(
            TestBalanceItemViewModel line, string fullCode)
        {
            var repository = UnitOfWork.GetAsyncRepository<Account>();
            var account = await repository.GetSingleByCriteriaAsync(acc => acc.FullCode == fullCode);
            return new TestBalanceItemViewModel()
            {
                BranchId = line.BranchId,
                BranchName = line.BranchName,
                AccountId = account.Id,
                AccountName = account.Name,
                AccountFullCode = account.FullCode,
                AccountLevel = account.Level
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
            var repository = UnitOfWork.GetAsyncRepository<Account>();
            var usedIds = items
                .Where(item => item.AccountLevel == (short)mode)
                .Select(item => item.AccountId);
            var notUsed = await repository
                .GetEntityQuery(acc => acc.Branch)
                .Where(acc => !usedIds.Contains(acc.Id) && acc.Level == (short)mode)
                .Select(acc => new { acc.Id, acc.Name, acc.FullCode, acc.BranchId, BranchName = acc.Branch.Name })
                .ToListAsync();
            foreach (var notUsedItem in notUsed)
            {
                zeroItems.Add(new TestBalanceItemViewModel()
                {
                    AccountFullCode = notUsedItem.FullCode,
                    AccountId = notUsedItem.Id,
                    AccountName = notUsedItem.Name,
                    BranchId = notUsedItem.BranchId,
                    BranchName = notUsedItem.BranchName
                });
            }

            return zeroItems;
        }

        public IEnumerable<TestBalanceItemViewModel> FilterBalanceLines(
            IEnumerable<TestBalanceItemViewModel> lines, TreeEntity accountItem)
        {
            return lines.Where(line => line.AccountFullCode.StartsWith(accountItem.FullCode));
        }

        public int GetItemId(TestBalanceItemViewModel item)
        {
            return item.AccountId;
        }

        public IEnumerable<TestBalanceItemViewModel> GetSortedItems(IEnumerable<TestBalanceItemViewModel> items)
        {
            return items
                .OrderBy(item => item.AccountFullCode)
                .ToArray();
        }

        public int GetSourceList(TestBalanceFormat format)
        {
            return _helper.GetSourceList(format, "Account");
        }

        private readonly ITestBalanceHelper _helper;
    }
}
