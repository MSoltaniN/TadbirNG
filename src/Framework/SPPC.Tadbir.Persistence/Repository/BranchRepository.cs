using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPPC.Framework.Common;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Model;
using SPPC.Tadbir.Model.Auth;
using SPPC.Tadbir.Model.Corporate;
using SPPC.Tadbir.Resources;
using SPPC.Tadbir.Utility;
using SPPC.Tadbir.ViewModel;
using SPPC.Tadbir.ViewModel.Corporate;
using SPPC.Tadbir.ViewModel.Finance;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای مدیریت شعب را پیاده سازی میکند.
    /// </summary>
    public class BranchRepository : LoggingRepository<Branch, BranchViewModel>, IBranchRepository
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="context">امکانات مشترک مورد نیاز را برای عملیات دیتابیسی فراهم می کند</param>
        /// <param name="log">امکان ایجاد لاگ های عملیاتی را در دیتابیس سیستمی برنامه فراهم می کند</param>
        public BranchRepository(IRepositoryContext context, IOperationLogRepository log)
            : base(context, log)
        {
        }

        /// <summary>
        /// به روش آسنکرون، کلیه شعب سازمانی را که در شرکت جاری تعریف شده اند
        /// خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه ای از شعب سازمانی تعریف شده در شرکت جاری</returns>
        public async Task<PagedList<BranchViewModel>> GetBranchesAsync(GridOptions gridOptions = null)
        {
            var repository = UnitOfWork.GetAsyncRepository<Branch>();
            var branches = await repository.GetByCriteriaAsync(
                    await GetSecurityFilterAsync(), br => br.Parent, br => br.Children);
            await ReadAsync(gridOptions);
            return new PagedList<BranchViewModel>(
                branches.Select(br => Mapper.Map<BranchViewModel>(br)), gridOptions);
        }

        /// <summary>
        /// به روش آسنکرون،شعبه سازمانی با شناسه عددی مشخص شده را از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <param name="branchId">شناسه عددی یکی از شعب سازمانی موجود</param>
        /// <returns>شعبه سازمانی مشخص شده با شناسه عددی</returns>
        public async Task<BranchViewModel> GetBranchAsync(int branchId)
        {
            BranchViewModel item = null;
            var repository = UnitOfWork.GetAsyncRepository<Branch>();
            var branch = await repository.GetByIDAsync(branchId);
            if (branch != null)
            {
                item = Mapper.Map<BranchViewModel>(branch);
            }

            return item;
        }

        /// <summary>
        /// به روش آسنکرون، کلیه شعب سازمانی زیرمجموعه یک شعبه را خوانده و برمی گرداند
        /// </summary>
        /// <param name="parentId">شناسه دیتابیسی شعبه والد مورد نظر</param>
        /// <returns>مجموعه ای از شعب سازمانی زیرمجموعه</returns>
        public async Task<IList<AccountItemBriefViewModel>> GetBranchChildrenAsync(int? parentId)
        {
            var repository = UnitOfWork.GetAsyncRepository<Branch>();
            var children = await repository
                .GetEntityQuery(br => br.Children)
                .Where(await GetSecurityFilterAsync())
                .Where(br => br.ParentId == parentId)
                .Select(br => Mapper.Map<AccountItemBriefViewModel>(br))
                .ToListAsync();
            return children;
        }

        /// <summary>
        /// به روش آسنکرون، نقش های دارای دسترسی به یک شعبه را خوانده و برمی گرداند
        /// </summary>
        /// <param name="branchId">شناسه یکی از شعبه های موجود</param>
        /// <returns>اطلاعات نمایشی نقش های دارای دسترسی</returns>
        public async Task<RelatedItemsViewModel> GetBranchRolesAsync(int branchId)
        {
            RelatedItemsViewModel branchRoles = null;
            var repository = UnitOfWork.GetAsyncRepository<Branch>();
            var existing = await repository.GetByIDAsync(branchId, br => br.RoleBranches);
            if (existing != null)
            {
                UnitOfWork.UseSystemContext();
                var roleRepository = UnitOfWork.GetAsyncRepository<Role>();
                var enabledRoleIds = existing.RoleBranches.Select(rb => rb.RoleId);
                var enabledRoles = await roleRepository
                    .GetEntityQuery()
                    .Where(r => enabledRoleIds.Contains(r.Id))
                    .Select(r => Mapper.Map<RelatedItemViewModel>(r))
                    .ToArrayAsync();
                var disabledRoles = await roleRepository
                    .GetEntityQuery()
                    .Where(r => !enabledRoleIds.Contains(r.Id))
                    .Select(r => Mapper.Map<RelatedItemViewModel>(r))
                    .ToArrayAsync();
                Array.ForEach(enabledRoles, item => item.IsSelected = true);
                UnitOfWork.UseCompanyContext();

                branchRoles = new RelatedItemsViewModel() { Id = branchId };
                Array.ForEach(enabledRoles
                    .Concat(disabledRoles)
                    .OrderBy(item => item.Id)
                    .ToArray(), item => branchRoles.RelatedItems.Add(item));
            }

            return branchRoles;
        }

        /// <summary>
        /// به روش آسنکرون، آخرین وضعیت نقش های دارای دسترسی به یک شعبه را ذخیره می کند
        /// </summary>
        /// <param name="branchRoles">اطلاعات نمایشی نقش های دارای دسترسی</param>
        public async Task SaveBranchRolesAsync(RelatedItemsViewModel branchRoles)
        {
            Verify.ArgumentNotNull(branchRoles, "branchRoles");
            var repository = UnitOfWork.GetAsyncRepository<Branch>();
            var existing = await repository.GetByIDWithTrackingAsync(branchRoles.Id, br => br.RoleBranches);
            if (existing != null && AreRolesModified(existing, branchRoles))
            {
                if (existing.RoleBranches.Count > 0)
                {
                    RemoveInaccessibleRoles(existing, branchRoles);
                }

                AddNewRoles(existing, branchRoles);
                repository.Update(existing);
                await UnitOfWork.CommitAsync();
                OnEntityAction(OperationId.RoleAccess);
                Log.Description = Context.Localize(await GetBranchRoleDescriptionAsync(existing));
                await TrySaveLogAsync();
            }
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات یک شعبه سازمانی را در محل ذخیره ایجاد یا اصلاح می کند
        /// </summary>
        /// <param name="branchView">شعبه سازمانی مورد نظر برای ایجاد یا اصلاح</param>
        /// <returns>اطلاعات نمایشی شعبه سازمانی ایجاد یا اصلاح شده</returns>
        public async Task<BranchViewModel> SaveBranchAsync(BranchViewModel branchView)
        {
            Verify.ArgumentNotNull(branchView, "branchView");
            Branch branch = default(Branch);
            var repository = UnitOfWork.GetAsyncRepository<Branch>();
            if (branchView.Id == 0)
            {
                branch = Mapper.Map<Branch>(branchView);
                await InsertAsync(repository, branch);
            }
            else
            {
                branch = await repository.GetByIDAsync(branchView.Id);
                if (branch != null)
                {
                    await UpdateAsync(repository, branch, branchView);
                }
            }

            return Mapper.Map<BranchViewModel>(branch);
        }

        /// <summary>
        /// به روش آسنکرون، شعبه سازمانی مشخص شده با شناسه عددی را از محل ذخیره حذف می کند
        /// </summary>
        /// <param name="branchId">شناسه عددی شعبه سازمانی مورد نظر برای حذف</param>
        public async Task DeleteBranchAsync(int branchId)
        {
            var repository = UnitOfWork.GetAsyncRepository<Branch>();
            var branch = await repository.GetByIDAsync(branchId);
            if (branch != null)
            {
                await DeleteAsync(repository, branch);
            }
        }

        /// <summary>
        /// به روش آسنکرون، شعبه سازمانی مشخص شده با شناسه عددی را به همراه کلیه اطلاعات وابسته به آن حذف می کند
        /// </summary>
        /// <param name="branchId">شناسه عددی شعبه سازمانی مورد نظر برای حذف</param>
        public async Task DeleteBranchWithDataAsync(int branchId)
        {
            await DeleteWithCascadeAsync(branchId);
        }

        /// <summary>
        /// به روش آسنکرون، شعبه های مشخص شده با شناسه دیتابیسی را حذف می کند
        /// </summary>
        /// <param name="items">مجموعه شناسه های دیتابیسی سطرهای مورد نظر برای حذف</param>
        public async Task DeleteBranchesAsync(IEnumerable<int> items)
        {
            var repository = UnitOfWork.GetAsyncRepository<Branch>();
            foreach (int item in items)
            {
                var branch = await repository.GetByIDAsync(item);
                if (branch != null)
                {
                    await DeleteNoLogAsync(repository, branch);
                }
            }

            await OnEntityGroupDeleted(items);
        }

        /// <summary>
        /// به روش آسنکرون، مشخص می کند که آیا شعبه سازمانی انتخاب شده دارای زیرمجموعه هست یا نه
        /// </summary>
        /// <param name="branchId">شناسه یکتای یکی از شعب سازمانی موجود</param>
        /// <returns>در حالتی که شعبه سازمانی مشخص شده دارای زیرمجموعه باشد مقدار "درست" و در غیر این صورت
        /// مقدار "نادرست" را برمی گرداند</returns>
        public async Task<bool> HasChildrenAsync(int branchId)
        {
            var repository = UnitOfWork.GetAsyncRepository<Branch>();
            int count = await repository.GetCountByCriteriaAsync(br => br.ParentId == branchId);
            return count > 0;
        }

        /// <summary>
        /// به روش آسنکرون، مشخص می کند که آیا شعبه مشخص شده قابل حذف است یا نه؟
        /// </summary>
        /// <param name="branchId">شناسه دیتابیسی شعبه مورد نظر</param>
        /// <returns>اگر شعبه مورد نظر در برنامه به طور مستقیم استفاده شده باشد
        /// مقدار "نادرست" و در غیر این صورت مقدار "درست" را برمی گرداند</returns>
        public async Task<bool> CanDeleteBranchAsync(int branchId)
        {
            bool canDelete = true;
            var dependentTypes = ModelCatalogue.GetAllDependentsOfType(typeof(Branch));
            foreach (var type in dependentTypes)
            {
                if (HasBranchReference(type, branchId))
                {
                    canDelete = false;
                    break;
                }
            }

            if (canDelete)
            {
                var repository = UnitOfWork.GetAsyncRepository<RoleBranch>();
                int roleCount = await repository.GetCountByCriteriaAsync(rb => rb.BranchId == branchId);
                canDelete = (roleCount == 0);
            }

            return canDelete;
        }

        /// <summary>
        /// به روش آسنکرون، قواعد کاری تعریف شده را برای شعبه داده شده بررسی می کند
        /// </summary>
        /// <param name="branch">مدل نمایشی شعبه مورد بررسی</param>
        /// <returns>در صورت نبود اشکال، مقدار بولی "درست" و در غیر این صورت مقدار بولی "نادرست" را برمی گرداند</returns>
        public async Task<bool> IsValidBranchAsync(BranchViewModel branch)
        {
            bool isValid = branch.Level > 0;
            if (!isValid)
            {
                var repository = UnitOfWork.GetAsyncRepository<Branch>();
                int count = await repository.GetCountByCriteriaAsync(
                    br => br.Level == 0);
                isValid = count == 0;
            }

            return isValid;
        }

        internal override int? EntityType
        {
            get { return (int)EntityTypeId.Branch; }
        }

        /// <summary>
        /// آخرین تغییرات موجودیت را از مدل نمایشی به سطر اطلاعاتی موجود کپی می کند
        /// </summary>
        /// <param name="branchViewModel">مدل نمایشی شامل آخرین تغییرات</param>
        /// <param name="branch">سطر اطلاعاتی موجود</param>
        protected override void UpdateExisting(BranchViewModel branchViewModel, Branch branch)
        {
            branch.Name = branchViewModel.Name;
            branch.Level = branchViewModel.Level;
            branch.Description = branchViewModel.Description;
        }

        /// <summary>
        /// اطلاعات خلاصه سطر اطلاعاتی داده شده را به صورت یک رشته متنی برمی گرداند
        /// </summary>
        /// <param name="entity">یکی از سطرهای اطلاعاتی موجود</param>
        /// <returns>اطلاعات خلاصه سطر اطلاعاتی داده شده به صورت رشته متنی</returns>
        protected override string GetState(Branch entity)
        {
            return (entity != null)
                ? String.Format(
                    "{0} : {1} , {2} : {3}",
                    AppStrings.Name, entity.Name, AppStrings.Description, entity.Description)
                : null;
        }

        private static bool AreEqual(IEnumerable<int> left, IEnumerable<int> right)
        {
            return left.Count() == right.Count()
                && left.All(value => right.Contains(value));
        }

        private static bool AreRolesModified(Branch existing, RelatedItemsViewModel roleItems)
        {
            var existingItems = existing.RoleBranches
                .Select(rb => rb.RoleId)
                .ToArray();
            var enabledItems = roleItems.RelatedItems
                .Where(item => item.IsSelected)
                .Select(item => item.Id)
                .ToArray();
            return (!AreEqual(existingItems, enabledItems));
        }

        private static void RemoveInaccessibleRoles(Branch existing, RelatedItemsViewModel roleItems)
        {
            var currentItems = roleItems.RelatedItems
                .Where(item => item.IsSelected)
                .Select(item => item.Id);
            var removedItems = existing.RoleBranches
                .Select(rb => rb.RoleId)
                .Where(id => !currentItems.Contains(id))
                .ToArray();
            foreach (int id in removedItems)
            {
                existing.RoleBranches.Remove(existing.RoleBranches
                    .Where(rb => rb.RoleId == id)
                    .Single());
            }
        }

        private void AddNewRoles(Branch existing, RelatedItemsViewModel roleItems)
        {
            var currentItems = existing.RoleBranches.Select(rb => rb.RoleId);
            var newItems = roleItems.RelatedItems
                .Where(item => item.IsSelected
                    && !currentItems.Contains(item.Id));
            foreach (var item in newItems)
            {
                var roleBranch = new RoleBranch()
                {
                    BranchId = existing.Id,
                    RoleId = item.Id
                };
                existing.RoleBranches.Add(roleBranch);
            }
        }

        private async Task<string> GetBranchRoleDescriptionAsync(Branch branch)
        {
            var builder = new StringBuilder();
            builder.AppendFormat("{0} : {1} , {2} : ",
                AppStrings.Branch, branch.Name, AppStrings.RolesWithAccess);
            if (branch.RoleBranches.Count > 0)
            {
                UnitOfWork.UseSystemContext();
                var repository = UnitOfWork.GetAsyncRepository<Role>();
                var roles = await repository.GetByCriteriaAsync(r => branch.RoleBranches
                    .Select(rb => rb.RoleId)
                    .Contains(r.Id));
                builder.Append(String.Join(" , ", roles.Select(r => r.Name)));
                UnitOfWork.UseCompanyContext();
            }
            else
            {
                builder.Append(AppStrings.None);
            }

            return builder.ToString();
        }

        private async Task<Expression<Func<Branch, bool>>> GetSecurityFilterAsync()
        {
            if (!UserContext.Roles.Contains(AppConstants.AdminRoleId))
            {
                var repository = UnitOfWork.GetAsyncRepository<RoleBranch>();
                var branchIds = await repository
                    .GetEntityQuery()
                    .Where(rb => UserContext.Roles.Contains(rb.RoleId))
                    .Select(rb => rb.BranchId)
                    .Distinct()
                    .ToListAsync();
                return branch => branchIds.Contains(branch.Id);
            }
            else
            {
                return branch => true;
            }
        }
    }
}
