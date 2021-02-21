using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Model;
using SPPC.Tadbir.Model.Corporate;
using SPPC.Tadbir.Model.Finance;
using SPPC.Tadbir.ViewModel.Reporting;

namespace SPPC.Tadbir.Persistence.Utility
{
    public class ReportDirectUtility : IReportDirectUtility
    {
        public ReportDirectUtility(IRepositoryContext context, ISystemRepository system)
        {
            _context = context;
            _system = system;
        }

        public IEnumerable<int> GetChildTree(int branchId)
        {
            var tree = new List<int>();
            var repository = UnitOfWork.GetRepository<Branch>();
            var branch = repository.GetByID(branchId, br => br.Children);
            AddChildren(branch, tree);
            return tree;
        }

        public int GetLevelCodeLength(int viewId, int level)
        {
            var fullConfig = Config
                .GetViewTreeConfigByViewAsync(viewId)
                .Result;
            var treeConfig = fullConfig.Current;
            int codeLength = treeConfig.Levels
                .Where(cfg => cfg.No <= level + 1)
                .Select(cfg => (int)cfg.CodeLength)
                .Sum();
            return codeLength;
        }

        public T ValueOrDefault<T>(DataRow row, string field)
        {
            var value = default(T);
            if (row.Table.Columns.Contains(field) && row[field] != DBNull.Value)
            {
                value = (T)Convert.ChangeType(row[field], typeof(T));
            }

            return value;
        }

        public string ValueOrDefault(DataRow row, string field)
        {
            string value = null;
            if (row.Table.Columns.Contains(field))
            {
                value = row[field].ToString();
            }

            return value;
        }

        /// <summary>
        /// به روش آسنکرون، فهرست سطوح قابل استفاده برای گزارشگیری را
        /// برای مولفه حساب داده شده خوانده و برمی گرداند
        /// </summary>
        /// <param name="viewId">شناسه مولفه حساب</param>
        /// <returns>فهرست سطوح قابل استفاده</returns>
        public async Task<IEnumerable<TestBalanceModeInfo>> GetLevelBalanceTypesAsync(int viewId)
        {
            var lookup = new List<TestBalanceModeInfo>();
            var fullConfig = await Config.GetViewTreeConfigByViewAsync(viewId);
            var usedLevels = fullConfig.Current
                .Levels
                .Where(level => level.IsEnabled && level.IsUsed)
                .ToList();
            int typeId = 0;
            for (int index = 0; index < usedLevels.Count; index++)
            {
                lookup.Add(new TestBalanceModeInfo()
                {
                    Id = typeId++,
                    Name = usedLevels[index].Name,
                    Level = usedLevels[index].No,
                    IsDetail = false
                });
            }

            return lookup;
        }

        /// <summary>
        /// به روش آسنکرون، فهرست سطوح زیرمجموعه قابل انتخاب برای گزارشگیری را خوانده و برمی گرداند
        /// </summary>
        /// <returns>فهرست سطوح زیرمجموعه قابل انتخاب</returns>
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

        /// <summary>
        /// به روش آسنکرون، فهرست سطوح زیرمجموعه قابل انتخاب برای گزارشگیری را خوانده و برمی گرداند
        /// </summary>
        /// <param name="viewId">شناسه مولفه حساب</param>
        /// <returns>فهرست سطوح زیرمجموعه قابل انتخاب</returns>
        public async Task<IEnumerable<TestBalanceModeInfo>> GetChildBalanceTypesAsync(int viewId)
        {
            if (viewId == ViewId.Account)
            {
                return await GetChildBalanceTypesAsync();
            }

            var lookup = new List<TestBalanceModeInfo>();
            var fullConfig = await Config.GetViewTreeConfigByViewAsync(viewId);
            var usedLevels = fullConfig.Current
                .Levels
                .Where(level => level.IsEnabled && level.IsUsed)
                .ToList();
            int typeId = usedLevels.Count;
            for (int index = 0; index < usedLevels.Count - 1; index++)
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

        /// <summary>
        /// سطرهای بدون مانده و گردش را با توجه به سطرهای داده شده برای گزارش خوانده و برمی گرداند
        /// </summary>
        /// <param name="viewId">شناسه مولفه حساب مورد نظر</param>
        /// <param name="items">سطرهای داده شده برای گزارش</param>
        /// <param name="level">سطح گزارشگیری مورد نظر</param>
        /// <returns>سطرهای بدون مانده و گردش</returns>
        public async Task<IEnumerable<TestBalanceItemViewModel>> GetZeroBalanceItemsAsync(
            int viewId, IEnumerable<TestBalanceItemViewModel> items, int level)
        {
            var zeroItems = new List<TestBalanceItemViewModel>();
            var notUsed = await GetNotUsedItemsAsync(viewId, items, level);
            foreach (var notUsedItem in notUsed)
            {
                zeroItems.Add(new TestBalanceItemViewModel()
                {
                    AccountFullCode = notUsedItem.FullCode,
                    DetailAccountFullCode = notUsedItem.FullCode,
                    CostCenterFullCode = notUsedItem.FullCode,
                    ProjectFullCode = notUsedItem.FullCode,
                    BranchName = notUsedItem.Branch.Name
                });
            }

            return zeroItems;
        }

        public async Task SetItemNamesAsync(int viewId, IEnumerable<TestBalanceItemViewModel> items)
        {
            switch (viewId)
            {
                case ViewId.Account:
                    await SetItemNamesAsync<Account>(items);
                    break;
                case ViewId.DetailAccount:
                    await SetItemNamesAsync<DetailAccount>(items);
                    break;
                case ViewId.CostCenter:
                    await SetItemNamesAsync<CostCenter>(items);
                    break;
                case ViewId.Project:
                    await SetItemNamesAsync<Project>(items);
                    break;
            }
        }

        public async Task<TreeEntity> GetItemAsync(int viewId, int itemId)
        {
            var accountItem = default(TreeEntity);
            switch (viewId)
            {
                case ViewId.Account:
                    accountItem = await GetItemAsync<Account>(itemId);
                    break;
                case ViewId.DetailAccount:
                    accountItem = await GetItemAsync<DetailAccount>(itemId);
                    break;
                case ViewId.CostCenter:
                    accountItem = await GetItemAsync<CostCenter>(itemId);
                    break;
                case ViewId.Project:
                    accountItem = await GetItemAsync<Project>(itemId);
                    break;
            }

            return accountItem;
        }

        private IAppUnitOfWork UnitOfWork
        {
            get { return _context.UnitOfWork; }
        }

        private IConfigRepository Config
        {
            get { return _system.Config; }
        }

        private void AddChildren(Branch branch, IList<int> children)
        {
            var repository = UnitOfWork.GetRepository<Branch>();
            foreach (var child in branch.Children)
            {
                children.Add(child.Id);
                var item = repository.GetByID(child.Id, br => br.Children);
                AddChildren(item, children);
            }
        }

        private async Task<IEnumerable<TreeEntity>> GetNotUsedItemsAsync(
            int viewId, IEnumerable<TestBalanceItemViewModel> items, int level)
        {
            IEnumerable<TreeEntity> notUsed = null;
            switch (viewId)
            {
                case ViewId.Account:
                    notUsed = await GetNotUsedItemsAsync<Account>(viewId, items, level);
                    break;
                case ViewId.DetailAccount:
                    notUsed = await GetNotUsedItemsAsync<DetailAccount>(viewId, items, level);
                    break;
                case ViewId.CostCenter:
                    notUsed = await GetNotUsedItemsAsync<CostCenter>(viewId, items, level);
                    break;
                case ViewId.Project:
                    notUsed = await GetNotUsedItemsAsync<Project>(viewId, items, level);
                    break;
            }

            return notUsed;
        }

        private async Task<IEnumerable<T>> GetNotUsedItemsAsync<T>(
            int viewId, IEnumerable<TestBalanceItemViewModel> items, int level)
            where T : TreeEntity
        {
            var repository = _system.Repository;
            var usedCodes = items
                .Select(item => item.AccountFullCode);
            return await repository
                .GetAllQuery<T>(viewId, tree => tree.Branch)
                .Where(tree => !usedCodes.Contains(tree.FullCode) && tree.Level == level)
                .ToListAsync();
        }

        private async Task SetItemNamesAsync<T>(IEnumerable<TestBalanceItemViewModel> items)
            where T : TreeEntity
        {
            var repository = UnitOfWork.GetRepository<T>();
            var fullCodes = items
                .Select(item => item.AccountFullCode);  // ALL codes are set to be identical
            var treeItems = await repository
                .GetEntityQuery()
                .Where(tree => fullCodes.Contains(tree.FullCode))
                .Select(tree => new KeyValuePair<string, string>(tree.FullCode, tree.Name))
                .ToListAsync();
            var treeItemMap = new Dictionary<string, string>(treeItems.Count);
            foreach (var keyValue in treeItems)
            {
                treeItemMap.Add(keyValue.Key, keyValue.Value);
            }

            foreach (var item in items)
            {
                // NOTE: All codes are set to be identical in each balance item
                item.AccountName = treeItemMap[item.AccountFullCode];
                item.DetailAccountName = treeItemMap[item.AccountFullCode];
                item.CostCenterName = treeItemMap[item.AccountFullCode];
                item.ProjectName = treeItemMap[item.AccountFullCode];
            }
        }

        private async Task<T> GetItemAsync<T>(int itemId)
            where T : TreeEntity
        {
            var repository = UnitOfWork.GetAsyncRepository<T>();
            return await repository.GetByIDAsync(itemId);
        }

        private readonly IRepositoryContext _context;
        private readonly ISystemRepository _system;
    }
}
