﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPPC.Framework.Common;
using SPPC.Framework.Mapper;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Model.Auth;
using SPPC.Tadbir.Model.Corporate;
using SPPC.Tadbir.ViewModel;
using SPPC.Tadbir.ViewModel.Auth;
using SPPC.Tadbir.ViewModel.Corporate;
using SPPC.Tadbir.ViewModel.Metadata;

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
        /// <param name="unitOfWork">پیاده سازی اینترفیس واحد کاری برای انجام عملیات دیتابیسی </param>
        /// <param name="mapper">نگاشت مورد استفاده برای تبدیل کلاس های مدل اطلاعاتی</param>
        /// <param name="metadata">امکان خواندن متادیتا برای یک موجودیت را فراهم می کند</param>
        /// <param name="log">امکان ایجاد لاگ های عملیاتی را در دیتابیس سیستمی برنامه فراهم می کند</param>
        public BranchRepository(
            IAppUnitOfWork unitOfWork, IDomainMapper mapper, IMetadataRepository metadata,
            IOperationLogRepository log)
            : base(unitOfWork, mapper, metadata, log)
        {
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
            var repository = UnitOfWork.GetAsyncRepository<Branch>();
            var branches = await repository
                .GetByCriteriaAsync(
                    br => br.CompanyId == companyId,
                    gridOptions, br => br.Parent, br => br.Children);
            return branches
                .Select(item => Mapper.Map<BranchViewModel>(item))
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
            var repository = UnitOfWork.GetAsyncRepository<Branch>();
            var count = await repository
                .GetCountByCriteriaAsync(
                    br => br.CompanyId == companyId,
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
            var repository = UnitOfWork.GetAsyncRepository<Branch>();
            var branch = await repository.GetByIDAsync(
                branchId, b => b.Children);
            if (branch != null)
            {
                item = Mapper.Map<BranchViewModel>(branch);
            }

            return item;
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات فراداده ای تعریف شده برای شعبه سازمانی را از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <returns>اطلاعات فراداده ای تعریف شده برای شعبه سازمانی</returns>
        public async Task<ViewViewModel> GetBranchMetadataAsync()
        {
            return await Metadata.GetViewMetadataAsync<Branch>();
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
        /// به روش آسنکرون، مشخص می کند که آیا شعبه سازمانی انتخاب شده دارای زیرمجموعه هست یا نه
        /// </summary>
        /// <param name="branchId">شناسه یکتای یکی از شعب سازمانی موجود</param>
        /// <returns>در حالتی که شعبه سازمانی مشخص شده دارای زیرمجموعه باشد مقدار "درست" و در غیر این صورت
        /// مقدار "نادرست" را برمی گرداند</returns>
        public async Task<bool?> HasChildrenAsync(int branchId)
        {
            bool? hasChildren = null;
            var repository = UnitOfWork.GetAsyncRepository<Branch>();
            var branch = await repository.GetByIDAsync(branchId, b => b.Children);
            if (branch != null)
            {
                hasChildren = branch.Children.Count > 0;
            }

            return hasChildren;
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
                    "Name : {1}{0}Description : {2}",
                    Environment.NewLine, entity.Name, entity.Description)
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
                    Branch = existing,
                    BranchId = existing.Id,
                    RoleId = item.Id
                };
                existing.RoleBranches.Add(roleBranch);
            }
        }
    }
}
