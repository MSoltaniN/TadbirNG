using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Model;
using SPPC.Tadbir.Model.Finance;
using SPPC.Tadbir.ViewModel.Finance;
using SPPC.Tadbir.ViewModel.Reporting;

namespace SPPC.Tadbir.Persistence.Utility
{
    internal class ProjectBalanceUtility : ProjectUtility, ITestBalanceUtility
    {
        public ProjectBalanceUtility(IRepositoryContext context, IConfigRepository config,
            ISecureRepository repository, ICacheUtility<VoucherLineDetailViewModel> cache,
            ITestBalanceHelper helper)
            : base(context, config, repository, cache)
        {
            _helper = helper;
        }

        public async Task<IEnumerable<TestBalanceModeInfo>> GetLevelBalanceTypesAsync()
        {
            return await _helper.GetLevelBalanceTypesAsync(ViewId.Project);
        }

        public async Task<IEnumerable<TestBalanceModeInfo>> GetChildBalanceTypesAsync()
        {
            return await _helper.GetChildBalanceTypesAsync(ViewId.Project);
        }

        public IQueryable<VoucherLine> IncludeVoucherLineReference(IQueryable<VoucherLine> query)
        {
            return query.Include(line => line.Project);
        }

        public Func<TestBalanceItemViewModel, bool> GetCurrentlevelFilter(int level)
        {
            return line => line.ProjectId > 0 && line.ProjectLevel == level;
        }

        public Func<TestBalanceItemViewModel, bool> GetUpperlevelFilter(int level)
        {
            return line => line.ProjectId > 0 && line.ProjectLevel >= level;
        }

        public async Task<TestBalanceItemViewModel> GetItemFromVoucherLineAsync(TestBalanceItemViewModel line, string fullCode)
        {
            var repository = UnitOfWork.GetAsyncRepository<Project>();
            var project = await repository.GetSingleByCriteriaAsync(prj => prj.FullCode == fullCode);
            return new TestBalanceItemViewModel()
            {
                BranchId = line.BranchId,
                BranchName = line.BranchName,
                ProjectId = project.Id,
                ProjectName = project.Name,
                ProjectFullCode = project.FullCode,
                ProjectLevel = project.Level
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
            var repository = UnitOfWork.GetAsyncRepository<Project>();
            var usedIds = items
                .Where(item => item.ProjectLevel == (short)mode)
                .Select(item => item.ProjectId);
            var notUsed = await repository
                .GetEntityQuery(prj => prj.Branch)
                .Where(prj => !usedIds.Contains(prj.Id) && prj.Level == (short)mode)
                .Select(prj => new { prj.Id, prj.Name, prj.FullCode, prj.BranchId, BranchName = prj.Branch.Name })
                .ToListAsync();
            foreach (var notUsedItem in notUsed)
            {
                zeroItems.Add(new TestBalanceItemViewModel()
                {
                    ProjectFullCode = notUsedItem.FullCode,
                    ProjectId = notUsedItem.Id,
                    ProjectName = notUsedItem.Name,
                    BranchId = notUsedItem.BranchId,
                    BranchName = notUsedItem.BranchName
                });
            }

            return zeroItems;
        }

        public IEnumerable<TestBalanceItemViewModel> FilterBalanceLines(
            IEnumerable<TestBalanceItemViewModel> lines, TreeEntity accountItem)
        {
            return lines.Where(line => line.ProjectFullCode.StartsWith(accountItem.FullCode));
        }

        public int GetItemId(TestBalanceItemViewModel item)
        {
            return item.ProjectId;
        }

        public IEnumerable<TestBalanceItemViewModel> GetSortedItems(IEnumerable<TestBalanceItemViewModel> items)
        {
            return items
                .OrderBy(item => item.ProjectFullCode)
                .ToArray();
        }

        public int GetSourceList(TestBalanceFormat format)
        {
            return _helper.GetSourceList(format, "Project");
        }

        protected override Func<TModel, string> GetGroupSelector<TModel>(int groupLevel)
        {
            int codeLength = GetLevelCodeLength(ViewId.Project, groupLevel);
            return item => item.ProjectFullCode.Substring(0, codeLength);
        }

        private readonly ITestBalanceHelper _helper;
    }
}
