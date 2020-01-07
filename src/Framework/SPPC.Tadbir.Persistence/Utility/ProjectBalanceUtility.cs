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
    internal class ProjectBalanceUtility : BalanceUtilityBase, ITestBalanceUtility
    {
        public ProjectBalanceUtility(IRepositoryContext context, ISecureRepository repository, IConfigRepository config)
            : base(context, repository, config)
        {
        }

        /// <summary>
        /// به روش آسنکرون، مانده مولفه حساب مشخص شده را محاسبه کرده و برمی گرداند
        /// </summary>
        /// <param name="itemId">شناسه دیتابیسی مولفه حساب مورد نظر</param>
        /// <param name="date">تاریخ مورد نظر برای محاسبه مانده</param>
        /// <returns>مانده حساب مشخص شده به صورت علامتدار : عدد مثبت نمایانگر مانده بدهکار
        /// و عدد منفی نمایانگر مانده بستانکار است</returns>
        public async Task<decimal> GetBalanceAsync(int itemId, DateTime date)
        {
            decimal balance = 0.0M;
            var project = await GetAccountItemAsync<Project>(itemId);
            if (project != null)
            {
                balance = await GetItemBalanceAsync(
                    date, line => line.Project.FullCode.StartsWith(project.FullCode));
            }

            return balance;
        }

        /// <summary>
        /// به روش آسنکرون، مانده مولفه حساب مشخص شده را محاسبه کرده و برمی گرداند
        /// </summary>
        /// <param name="itemId">شناسه دیتابیسی مولفه حساب مورد نظر</param>
        /// <param name="number">شماره سندی که مانده با توجه به کلیه سندهای پیش از آن در دوره مالی جاری محاسبه می شود</param>
        /// <returns>مانده حساب مشخص شده به صورت علامتدار : عدد مثبت نمایانگر مانده بدهکار
        /// و عدد منفی نمایانگر مانده بستانکار است</returns>
        public async Task<decimal> GetBalanceAsync(int itemId, int number)
        {
            decimal balance = 0.0M;
            var project = await GetAccountItemAsync<Project>(itemId);
            if (project != null)
            {
                balance = await GetItemBalanceAsync(
                    number, line => line.Project.FullCode.StartsWith(project.FullCode));
            }

            return balance;
        }

        /// <summary>
        /// به روش آسنکرون، مانده مولفه حساب مشخص شده را در سند مالی از نوع داده شده
        /// محاسبه کرده و برمی گرداند
        /// </summary>
        /// <param name="itemId">شناسه دیتابیسی مولفه حساب مورد نظر</param>
        /// <param name="type">نوع سیستمی مورد نظر برای محاسبه مانده</param>
        /// <returns>مانده محاسبه شده برای سرفصل حسابداری</returns>
        public async Task<decimal> GetSpecialVoucherBalanceAsync(int itemId, VoucherType type)
        {
            decimal balance = 0.0M;
            var project = await GetAccountItemAsync<Project>(itemId);
            if (project != null)
            {
                balance = await GetSpecialVoucherBalanceAsync(
                    type, line => line.Project.FullCode.StartsWith(project.FullCode));
            }

            return balance;
        }

        public async Task<TreeEntity> GetAccountItemAsync(int itemId)
        {
            var repository = UnitOfWork.GetAsyncRepository<Project>();
            return await repository.GetByIDAsync(itemId);
        }

        public async Task<IEnumerable<TestBalanceModeInfo>> GetLevelBalanceTypesAsync()
        {
            return await GetLevelBalanceTypesAsync(ViewName.Project);
        }

        public async Task<IEnumerable<TestBalanceModeInfo>> GetChildBalanceTypesAsync()
        {
            return await GetChildBalanceTypesAsync(ViewName.Project);
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
                balance += await GetSpecialVoucherBalanceAsync(itemId, VoucherType.OpeningVoucher);
            }

            return balance;
        }

        public async Task<IEnumerable<TestBalanceItemViewModel>> GetZeroBalanceItemsAsync(
            IEnumerable<TestBalanceItemViewModel> items, TestBalanceMode mode)
        {
            var zeroItems = new List<TestBalanceItemViewModel>();
            var usedIds = items
                .Where(item => item.ProjectLevel == (short)mode)
                .Select(item => item.ProjectId);
            var notUsed = await Repository
                .GetAllQuery<Project>(ViewName.Project, prj => prj.Branch)
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
            return GetSourceList(format, "Project");
        }

        protected override Func<TModel, string> GetGroupSelector<TModel>(int groupLevel)
        {
            int codeLength = GetLevelCodeLength(ViewName.Project, groupLevel);
            return item => item.ProjectFullCode.Substring(0, codeLength);
        }
    }
}
