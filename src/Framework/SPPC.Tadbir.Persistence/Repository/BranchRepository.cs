using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPPC.Framework.Common;
using SPPC.Framework.Mapper;
using SPPC.Framework.Persistence;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Model.Auth;
using SPPC.Tadbir.Model.Corporate;
using SPPC.Tadbir.ViewModel;
using SPPC.Tadbir.ViewModel.Corporate;
using SPPC.Tadbir.ViewModel.Metadata;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای مدیریت شعب را پیاده سازی میکند.
    /// </summary>
    public class BranchRepository : IBranchRepository
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="unitOfWork">پیاده سازی اینترفیس واحد کاری برای انجام عملیات دیتابیسی </param>
        /// <param name="mapper">نگاشت مورد استفاده برای تبدیل کلاس های مدل اطلاعاتی</param>
        /// <param name="decorator">امکان ضمیمه کردن متادیتا به اطلاعات خوانده شده را فراهم می کند</param>
        public BranchRepository(IUnitOfWork unitOfWork, IDomainMapper mapper, IMetadataDecorator decorator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _decorator = decorator;
        }

        /// <summary>
        /// به روش آسنکرون، کلیه شعب سازمانی را که در شرکت مشخص شده تعریف شده اند،
        /// از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <param name="companyId"> شناسه عددی یکی از شرکت های موجود</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه ای از شعب سازمانی تعریف شده در شرکت مشخص شده</returns>
        public async Task<IList<BranchViewModel>> GetBranchesAsync(int companyId, GridOptions gridOptions = null)
        {
            var repository = _unitOfWork.GetAsyncRepository<Branch>();
            var branches = await repository
                .GetByCriteriaAsync(
                    b => b.Company.Id == companyId,
                    gridOptions, b => b.Company, b => b.Parent, b => b.Children);
            return branches
                .Select(item => _mapper.Map<BranchViewModel>(item))
                .ToList();
        }

        /// <summary>
        /// به روش آسنکرون، تعداد شعب سازمانی تعریف شده در شرکت مشخص شده را
        /// از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <param name="companyId"> شناسه عددی یکی از شرکت های موجود</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>تعداد شعب  سازمانی تعریف شده در شرکت مشخص شده</returns>
        public async Task<int> GetCountAsync(int companyId, GridOptions gridOptions = null)
        {
            var repository = _unitOfWork.GetAsyncRepository<Branch>();
            var count = await repository
                .GetCountByCriteriaAsync(
                    fp => fp.Company.Id == companyId,
                    gridOptions);
            return count;
        }

        /// <summary>
        /// به روش آسنکرون،شعبه سازمانی با شناسه عددی مشخص شده را از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <param name="branchId">شناسه عددی یکی از شعب سازمانی موجود</param>
        /// <returns>شعبه سازمانی مشخص شده با شناسه عددی</returns>
        public async Task<BranchViewModel> GetBranchAsync(int branchId)
        {
            BranchViewModel item = null;
            var repository = _unitOfWork.GetAsyncRepository<Branch>();
            var branch = await repository.GetByIDAsync(
                branchId, b => b.Company, b => b.Children);
            if (branch != null)
            {
                item = _mapper.Map<BranchViewModel>(branch);
            }

            return item;
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات فراداده ای تعریف شده برای شعبه سازمانی را از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <returns>اطلاعات فراداده ای تعریف شده برای شعبه سازمانی</returns>
        public async Task<EntityItemViewModel<BranchViewModel>> GetBranchMetadataAsync()
        {
            return await _decorator.GetDecoratedItemAsync<Branch, BranchViewModel>(null);
        }

        /// <summary>
        /// به روش آسنکرون، نقش های دارای دسترسی به یک شعبه را خوانده و برمی گرداند
        /// </summary>
        /// <param name="branchId">شناسه یکی از شعبه های موجود</param>
        /// <returns>اطلاعات نمایشی نقش های دارای دسترسی</returns>
        public async Task<RelatedItemsViewModel> GetBranchRolesAsync(int branchId)
        {
            RelatedItemsViewModel branchRoles = null;
            var repository = _unitOfWork.GetAsyncRepository<Branch>();
            var existing = await repository
                .GetEntityQuery()
                .Include(br => br.RoleBranches)
                    .ThenInclude(rb => rb.Role)
                .Where(br => br.Id == branchId)
                .SingleOrDefaultAsync();
            if (existing != null)
            {
                var enabledRoles = existing.RoleBranches
                    .Select(rb => rb.Role)
                    .Select(r => _mapper.Map<RelatedItemViewModel>(r))
                    .ToArray();
                var roleRepository = _unitOfWork.GetAsyncRepository<Role>();
                var allRoles = await roleRepository
                    .GetAllAsync();
                var disabledRoles = allRoles
                    .Select(r => _mapper.Map<RelatedItemViewModel>(r))
                    .Except(enabledRoles, new EntityEqualityComparer<RelatedItemViewModel>())
                    .ToArray();
                Array.ForEach(enabledRoles, item => item.IsSelected = true);

                branchRoles = _mapper.Map<RelatedItemsViewModel>(existing);
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
            var repository = _unitOfWork.GetAsyncRepository<Branch>();
            var existing = await repository.GetByIDWithTrackingAsync(branchRoles.Id, br => br.RoleBranches);
            if (existing != null && AreRolesModified(existing, branchRoles))
            {
                if (existing.RoleBranches.Count > 0)
                {
                    RemoveInaccessibleRoles(existing, branchRoles);
                }

                AddNewRoles(existing, branchRoles);
                repository.Update(existing);
                await _unitOfWork.CommitAsync();
            }
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات یک شعبه سازمانی را در محل ذخیره ایجاد یا اصلاح می کند
        /// </summary>
        /// <param name="branch">شعبه سازمانی مورد نظر برای ایجاد یا اصلاح</param>
        /// <returns>اطلاعات نمایشی شعبه سازمانی ایجاد یا اصلاح شده</returns>
        public async Task<BranchViewModel> SaveBranchAsync(BranchViewModel branch)
        {
            Verify.ArgumentNotNull(branch, "branch");
            Branch branchModel = default(Branch);
            var repository = _unitOfWork.GetAsyncRepository<Branch>();
            if (branch.Id == 0)
            {
                branchModel = _mapper.Map<Branch>(branch);
                repository.Insert(branchModel);
            }
            else
            {
                branchModel = await repository.GetByIDAsync(
                    branch.Id, b => b.Company);
                if (branchModel != null)
                {
                    UpdateExistingBranch(branch, branchModel);
                    repository.Update(branchModel);
                }
            }

            await _unitOfWork.CommitAsync();
            return _mapper.Map<BranchViewModel>(branchModel);
        }

        /// <summary>
        /// به روش آسنکرون، شعبه سازمانی مشخص شده با شناسه عددی را از محل ذخیره حذف می کند
        /// </summary>
        /// <param name="branchId">شناسه عددی شعبه سازمانی مورد نظر برای حذف</param>
        public async Task DeleteBranchAsync(int branchId)
        {
            var repository = _unitOfWork.GetAsyncRepository<Branch>();
            var branch = await repository.GetByIDAsync(branchId);
            if (branch != null)
            {
                branch.Company = null;
                branch.Parent = null;
                repository.Delete(branch);
                await _unitOfWork.CommitAsync();
            }
        }

        /// <summary>
        /// به روش آسنکرون، مشخص می کند که آیا شعبه سازمانی انتخاب شده دارای زیرمجموعه هست یا نه
        /// </summary>
        /// <param name="branchId">شناسه یکتای یکی از شعب سازمانی موجود</param>
        /// <returns>در حالتی که شعبه سازمانی مشخص شده دارای زیرمجموعه باشد مقدار "درست" و در غیر این صورت
        /// مقدار "نادرست" را برمی گرداند</returns>
        public async Task<bool?> HasChildrenAsync(int branchId)
        {
            bool? hasChildren = null;
            var repository = _unitOfWork.GetAsyncRepository<Branch>();
            var branch = await repository.GetByIDAsync(branchId, b => b.Children);
            if (branch != null)
            {
                hasChildren = branch.Children.Count > 0;
            }

            return hasChildren;
        }

        private static void UpdateExistingBranch(BranchViewModel branchViewModel, Branch branch)
        {
            branch.Name = branchViewModel.Name;
            branch.Level = branchViewModel.Level;
            branch.Description = branchViewModel.Description;
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
            var repository = _unitOfWork.GetRepository<Role>();
            var currentItems = existing.RoleBranches.Select(rb => rb.RoleId);
            var newItems = roleItems.RelatedItems
                .Where(item => item.IsSelected
                    && !currentItems.Contains(item.Id));
            foreach (var item in newItems)
            {
                var role = repository.GetByIDWithTracking(item.Id);
                var roleBranch = new RoleBranch()
                {
                    Branch = existing,
                    BranchId = existing.Id,
                    Role = role,
                    RoleId = role.Id
                };
                existing.RoleBranches.Add(roleBranch);
            }
        }

        private IUnitOfWork _unitOfWork;
        private IDomainMapper _mapper;
        private IMetadataDecorator _decorator;
    }
}
