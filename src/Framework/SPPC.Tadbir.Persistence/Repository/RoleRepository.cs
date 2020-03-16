using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPPC.Framework.Common;
using SPPC.Framework.Extensions;
using SPPC.Framework.Persistence;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Configuration;
using SPPC.Tadbir.Helpers;
using SPPC.Tadbir.Model.Auth;
using SPPC.Tadbir.Model.Corporate;
using SPPC.Tadbir.Model.Finance;
using SPPC.Tadbir.Model.Metadata;
using SPPC.Tadbir.ViewModel;
using SPPC.Tadbir.ViewModel.Auth;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای مدیریت اطلاعات نقش ها را پیاده سازی می کند
    /// </summary>
    public class RoleRepository : SystemLoggingRepository<Role, RoleFullViewModel>, IRoleRepository
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="context">امکانات مشترک مورد نیاز را برای عملیات دیتابیسی فراهم می کند</param>
        /// <param name="log">امکان ایجاد لاگ های عملیاتی را در دیتابیس سیستمی برنامه فراهم می کند</param>
        public RoleRepository(IRepositoryContext context, IOperationLogRepository log)
            : base(context, log)
        {
            UnitOfWork.UseSystemContext();
        }

        /// <summary>
        /// به روش آسنکرون، لیست نقش های تعریف شده را از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>لیست نقش های تعریف شده</returns>
        public async Task<PagedList<RoleViewModel>> GetRolesAsync(GridOptions gridOptions = null)
        {
            var repository = UnitOfWork.GetAsyncRepository<Role>();
            var query = repository
                .GetEntityQuery()
                .Include(r => r.RolePermissions)
                    .ThenInclude(rp => rp.Permission);

            var roles = await query
                .Select(r => Mapper.Map<RoleViewModel>(r))
                .ToListAsync();
            await ReadAsync(gridOptions);
            return new PagedList<RoleViewModel>(roles, gridOptions);
        }

        /// <summary>
        /// Asynchronously initializes and returns a new role object that contains all available security permissions.
        /// </summary>
        /// <returns>A blank <see cref="RoleFullViewModel"/> object that contains full permission list from repository
        /// </returns>
        public async Task<RoleFullViewModel> GetNewRoleAsync()
        {
            var repository = UnitOfWork.GetAsyncRepository<Permission>();
            var all = await repository
                .GetAllAsync(perm => perm.Group);
            var allView = all
                .Select(perm => Mapper.Map<PermissionViewModel>(perm))
                .Where(perm => IsPublicPermission(perm))
                .ToArray();
            var role = new RoleFullViewModel();
            Array.ForEach(allView, perm =>
            {
                perm.IsEnabled = false;
                role.Permissions.Add(perm);
            });
            return role;
        }

        /// <summary>
        /// Asynchronously retrieves a single role with permissions (specified by role identifier) from repository.
        /// </summary>
        /// <param name="roleId">Unique identifier of the role to search for</param>
        /// <returns>A <see cref="RoleFullViewModel"/> instance that corresponds to the specified identifier, if there is
        /// such a role defined; otherwise, returns null.</returns>
        public async Task<RoleFullViewModel> GetRoleAsync(int roleId)
        {
            RoleFullViewModel role = null;
            var repository = UnitOfWork.GetAsyncRepository<Role>();
            var existing = await repository
                .GetEntityQuery()
                .Include(r => r.RolePermissions)
                    .ThenInclude(rp => rp.Permission)
                        .ThenInclude(p => p.Group)
                .Where(r => r.Id == roleId)
                .SingleOrDefaultAsync();
            if (existing != null)
            {
                var enabledPermissions = existing.RolePermissions
                    .Select(rp => rp.Permission)
                    .Select(perm => Mapper.Map<PermissionViewModel>(perm));
                var permissionRepository = UnitOfWork.GetAsyncRepository<Permission>();
                var disabledPermissions = await permissionRepository
                    .GetAllAsync(perm => perm.Group);
                var disabledView = disabledPermissions
                    .Select(perm => Mapper.Map<PermissionViewModel>(perm))
                    .Where(perm => IsPublicPermission(perm))
                    .Except(enabledPermissions, new EntityEqualityComparer<PermissionViewModel>())
                    .ToArray();
                Array.ForEach(disabledView, perm => perm.IsEnabled = false);

                role = new RoleFullViewModel()
                {
                    Id = roleId,
                    Role = Mapper.Map<RoleViewModel>(existing)
                };
                Array.ForEach(enabledPermissions
                    .Concat(disabledView)
                    .OrderBy(perm => perm.GroupId)
                    .ThenBy(perm => perm.Flag)
                    .ToArray(), perm => role.Permissions.Add(perm));
            }

            return role;
        }

        /// <summary>
        /// Asynchronously retrieves a single role with full details (specified by role identifier) from repository.
        /// </summary>
        /// <param name="roleId">Unique identifier of the role to search for</param>
        /// <returns>A <see cref="RoleDetailsViewModel"/> instance that corresponds to the specified identifier, if there is
        /// such a role defined; otherwise, returns null.</returns>
        public async Task<RoleDetailsViewModel> GetRoleDetailsAsync(int roleId)
        {
            RoleDetailsViewModel role = null;
            var repository = UnitOfWork.GetAsyncRepository<Role>();
            var existing = await repository
                .GetEntityQuery()
                .Include(r => r.RolePermissions)
                    .ThenInclude(rp => rp.Permission)
                        .ThenInclude(p => p.Group)
                .Include(r => r.UserRoles)
                    .ThenInclude(ur => ur.User)
                        .ThenInclude(usr => usr.Person)
                .Where(r => r.Id == roleId)
                .SingleOrDefaultAsync();
            if (existing != null)
            {
                role = new RoleDetailsViewModel()
                {
                    Role = Mapper.Map<RoleViewModel>(existing)
                };
                Array.ForEach(
                    existing.RolePermissions.Select(rp => rp.Permission).ToArray(),
                    perm => role.Permissions.Add(Mapper.Map<PermissionViewModel>(perm)));
                Array.ForEach(
                    existing.UserRoles.Select(ur => ur.User).ToArray(),
                    usr => role.Users.Add(Mapper.Map<UserBriefViewModel>(usr)));
            }

            return role;
        }

        /// <summary>
        /// Asynchronously retrieves brief information for a single role specified by unique identifier from repository.
        /// </summary>
        /// <param name="roleId">Unique identifier of the role to search for</param>
        /// <returns>A <see cref="RoleViewModel"/> instance that corresponds to the specified role identifier,
        /// if there is such a role defined; otherwise, returns null.</returns>
        public async Task<RoleViewModel> GetRoleBriefAsync(int roleId)
        {
            RoleViewModel roleBrief = null;
            var repository = UnitOfWork.GetAsyncRepository<Role>();
            var role = await repository.GetByIDAsync(roleId);
            if (role != null)
            {
                roleBrief = Mapper.Map<RoleViewModel>(role);
            }

            return roleBrief;
        }

        /// <summary>
        /// Asynchronously inserts or updates a single security role, including all permissions in it, in repository
        /// </summary>
        /// <param name="roleView">Role to insert or update</param>
        public async Task<RoleViewModel> SaveRoleAsync(RoleFullViewModel roleView)
        {
            Verify.ArgumentNotNull(roleView, "roleView");
            Verify.ArgumentNotNull(roleView.Role, "roleView.Role");
            Role role = null;
            var repository = UnitOfWork.GetAsyncRepository<Role>();
            if (roleView.Role.Id == 0)
            {
                role = Mapper.Map<Role>(roleView.Role);
                AddRolePermissions(role, roleView);
                await InsertAsync(repository, role);
            }
            else
            {
                role = await repository.GetByIDWithTrackingAsync(roleView.Role.Id, r => r.RolePermissions);
                if (role != null)
                {
                    if (ArePermissionsModified(role, roleView))
                    {
                        if (role.RolePermissions.Count > 0)
                        {
                            RemoveDisabledPermissions(role, roleView);
                        }

                        AddNewPermissions(role, roleView);
                    }

                    await UpdateAsync(repository, role, roleView);
                }
            }

            return Mapper.Map<RoleViewModel>(role);
        }

        /// <summary>
        /// Asynchronously deletes a role specified by unique identifier from repository.
        /// </summary>
        /// <param name="roleId">Unique identifier of the role to delete</param>
        /// <remarks>If no role with specified identifier could be found, no exception would be thrown.</remarks>
        public async Task DeleteRoleAsync(int roleId)
        {
            var repository = UnitOfWork.GetAsyncRepository<Role>();
            var role = await repository.GetByIDWithTrackingAsync(roleId, r => r.RolePermissions);
            if (role != null)
            {
                role.RolePermissions.Clear();
                await DeleteAsync(repository, role);
            }
        }

        /// <summary>
        /// به روش آسنکرون، مشخص می کند که آیا نقش مورد نظر به کاربری تخصیص داده شده یا نه
        /// </summary>
        /// <param name="roleId">شناسه دیتابیسی نقش مورد نظر</param>
        /// <returns>اگر نقش مورد نظر به کاربری تخصیص داده شده مقدار "درست" و
        /// در غیر این صورت مقدار "نادرست" را برمی گرداند</returns>
        public async Task<bool> IsAssignedRoleAsync(int roleId)
        {
            bool isAssigned = false;
            var repository = UnitOfWork.GetAsyncRepository<Role>();
            var role = await repository
                .GetEntityQuery(r => r.UserRoles)
                .Where(r => r.Id == roleId)
                .SingleOrDefaultAsync();
            if (role != null)
            {
                isAssigned = (role.UserRoles.Count > 0);
            }

            return isAssigned;
        }

        /// <summary>
        /// به روش آسنکرون، مشخص می کند که آیا نقش مورد نظر با یک یا چند شعبه مرتبط شده یا نه
        /// </summary>
        /// <param name="roleId">شناسه دیتابیسی نقش مورد نظر</param>
        /// <returns>اگر نقش مورد نظر با یک یا چند شعبه مرتبط شده مقدار "درست" و
        /// در غیر این صورت مقدار "نادرست" را برمی گرداند</returns>
        public async Task<bool> IsRoleRelatedToBranchAsync(int roleId)
        {
            UnitOfWork.UseCompanyContext();
            var repository = UnitOfWork.GetAsyncRepository<RoleBranch>();
            int count = await repository
                .GetCountByCriteriaAsync(rb => rb.RoleId == roleId);
            UnitOfWork.UseSystemContext();
            return count > 0;
        }

        /// <summary>
        /// به روش آسنکرون، مشخص می کند که آیا نقش مورد نظر با یک یا چند دوره مالی مرتبط شده یا نه
        /// </summary>
        /// <param name="roleId">شناسه دیتابیسی نقش مورد نظر</param>
        /// <returns>اگر نقش مورد نظر با یک یا چند دوره مالی مرتبط شده مقدار "درست" و
        /// در غیر این صورت مقدار "نادرست" را برمی گرداند</returns>
        public async Task<bool> IsRoleRelatedToFiscalPeriodAsync(int roleId)
        {
            UnitOfWork.UseCompanyContext();
            var repository = UnitOfWork.GetAsyncRepository<RoleFiscalPeriod>();
            int count = await repository
                .GetCountByCriteriaAsync(rfp => rfp.RoleId == roleId);
            UnitOfWork.UseSystemContext();
            return count > 0;
        }

        /// <summary>
        /// به روش آسنکرون، شعبه های قابل دسترسی توسط یک نقش را خوانده و برمی گرداند
        /// </summary>
        /// <param name="roleId">شناسه یکی از نقش های موجود</param>
        /// <returns>اطلاعات نمایشی شعبه های قابل دسترسی</returns>
        public async Task<RelatedItemsViewModel> GetRoleBranchesAsync(int roleId)
        {
            RelatedItemsViewModel roleBranches = null;
            UnitOfWork.UseCompanyContext();
            var relatedRepository = UnitOfWork.GetAsyncRepository<RoleBranch>();
            var roleBranchesModel = relatedRepository.GetByCriteria(
                rb => rb.RoleId == roleId, rb => rb.Branch);
            var enabledBranches = roleBranchesModel
                .Select(rb => rb.Branch)
                .Select(br => Mapper.Map<RelatedItemViewModel>(br))
                .ToArray();
            var branchRepository = UnitOfWork.GetAsyncRepository<Branch>();
            var allBranches = await branchRepository
                .GetAllAsync();
            var disabledBranches = allBranches
                .Select(br => Mapper.Map<RelatedItemViewModel>(br))
                .Except(enabledBranches, new EntityEqualityComparer<RelatedItemViewModel>())
                .ToArray();
            Array.ForEach(enabledBranches, item => item.IsSelected = true);

            roleBranches = new RelatedItemsViewModel() { Id = roleId };
            Array.ForEach(enabledBranches
                .Concat(disabledBranches)
                .OrderBy(item => item.Id)
                .ToArray(), item => roleBranches.RelatedItems.Add(item));
            UnitOfWork.UseSystemContext();

            return roleBranches;
        }

        /// <summary>
        /// به روش آسنکرون، آخرین وضعیت شعبه های قابل دسترسی توسط یک نقش را ذخیره می کند
        /// </summary>
        /// <param name="roleBranches">اطلاعات نمایشی شعبه های قابل دسترسی</param>
        public async Task SaveRoleBranchesAsync(RelatedItemsViewModel roleBranches)
        {
            Verify.ArgumentNotNull(roleBranches, "roleBranches");
            UnitOfWork.UseCompanyContext();
            var repository = UnitOfWork.GetAsyncRepository<RoleBranch>();
            var existing = await repository.GetByCriteriaAsync(rb => rb.RoleId == roleBranches.Id, rb => rb.Branch);
            if (AreBranchesModified(existing, roleBranches))
            {
                if (existing.Count > 0)
                {
                    RemoveInaccessibleBranches(repository, existing, roleBranches);
                }

                AddNewBranches(repository, existing, roleBranches);
                await UnitOfWork.CommitAsync();
                OnEntityAction(OperationId.BranchAccess);
                Log.Description = await GetRoleBranchDescriptionAsync(roleBranches.Id);
                await TrySaveLogAsync();
            }

            UnitOfWork.UseSystemContext();
        }

        /// <summary>
        /// به روش آسنکرون، کاربران یک نقش را خوانده و برمی گرداند
        /// </summary>
        /// <param name="roleId">شناسه یکی از نقش های موجود</param>
        /// <returns>اطلاعات نمایشی کاربران</returns>
        public async Task<RelatedItemsViewModel> GetRoleUsersAsync(int roleId)
        {
            RelatedItemsViewModel roleUsers = null;
            var repository = UnitOfWork.GetAsyncRepository<Role>();
            var existing = await repository
                .GetEntityQuery()
                .Include(r => r.UserRoles)
                    .ThenInclude(ur => ur.User)
                        .ThenInclude(usr => usr.Person)
                .Where(r => r.Id == roleId)
                .SingleOrDefaultAsync();
            if (existing != null)
            {
                var enabledUsers = existing.UserRoles
                    .Select(ur => ur.User)
                    .Select(usr => Mapper.Map<RelatedItemViewModel>(usr))
                    .ToArray();
                var userRepository = UnitOfWork.GetAsyncRepository<User>();
                var allUsers = await userRepository
                    .GetAllAsync(usr => usr.Person);
                var disabledUsers = allUsers
                    .Select(usr => Mapper.Map<RelatedItemViewModel>(usr))
                    .Except(enabledUsers, new EntityEqualityComparer<RelatedItemViewModel>())
                    .ToArray();
                Array.ForEach(enabledUsers, item => item.IsSelected = true);

                roleUsers = Mapper.Map<RelatedItemsViewModel>(existing);
                Array.ForEach(enabledUsers
                    .Concat(disabledUsers)
                    .OrderBy(item => item.Id)
                    .ToArray(), item => roleUsers.RelatedItems.Add(item));
            }

            return roleUsers;
        }

        /// <summary>
        /// به روش آسنکرون، آخرین وضعیت کاربران یک نقش را ذخیره می کند
        /// </summary>
        /// <param name="roleUsers">اطلاعات نمایشی کاربران</param>
        public async Task SaveRoleUsersAsync(RelatedItemsViewModel roleUsers)
        {
            Verify.ArgumentNotNull(roleUsers, "roleUsers");
            var repository = UnitOfWork.GetAsyncRepository<Role>();
            var existing = await repository.GetByIDWithTrackingAsync(roleUsers.Id, r => r.UserRoles);
            if (existing != null && AreUsersModified(existing, roleUsers))
            {
                if (existing.UserRoles.Count > 0)
                {
                    RemoveUnassignedUsers(existing, roleUsers);
                }

                AddNewUsers(existing, roleUsers);
                repository.Update(existing);
                await UnitOfWork.CommitAsync();
                OnEntityAction(OperationId.AssignUser);
                Log.Description = await GetRoleUserDescriptionAsync(roleUsers.Id);
                await TrySaveLogAsync();
            }
        }

        /// <summary>
        /// به روش آسنکرون، دوره های مالی قابل دسترسی توسط یک نقش را خوانده و برمی گرداند
        /// </summary>
        /// <param name="roleId">شناسه یکی از نقش های موجود</param>
        /// <returns>اطلاعات نمایشی دوره های مالی قابل دسترسی</returns>
        public async Task<RelatedItemsViewModel> GetRoleFiscalPeriodsAsync(int roleId)
        {
            RelatedItemsViewModel roleFiscalPeriods = null;
            UnitOfWork.UseCompanyContext();
            var relatedRepository = UnitOfWork.GetAsyncRepository<RoleFiscalPeriod>();
            var rolePeriodsModel = relatedRepository.GetByCriteria(
                rfp => rfp.RoleId == roleId, rb => rb.FiscalPeriod);
            var enabledPeriods = rolePeriodsModel
                .Select(rfp => rfp.FiscalPeriod)
                .Select(fp => Mapper.Map<RelatedItemViewModel>(fp))
                .ToArray();
            var repository = UnitOfWork.GetAsyncRepository<FiscalPeriod>();
            var allPeriods = await repository
                .GetAllAsync();
            var disabledPeriods = allPeriods
                .Select(fp => Mapper.Map<RelatedItemViewModel>(fp))
                .Except(enabledPeriods, new EntityEqualityComparer<RelatedItemViewModel>())
                .ToArray();
            Array.ForEach(enabledPeriods, item => item.IsSelected = true);

            roleFiscalPeriods = new RelatedItemsViewModel() { Id = roleId };
            Array.ForEach(enabledPeriods
                .Concat(disabledPeriods)
                .OrderBy(item => item.Id)
                .ToArray(), item => roleFiscalPeriods.RelatedItems.Add(item));
            UnitOfWork.UseSystemContext();

            return roleFiscalPeriods;
        }

        /// <summary>
        /// به روش آسنکرون، آخرین وضعیت دوره های مالی قابل دسترسی توسط یک نقش را ذخیره می کند
        /// </summary>
        /// <param name="rolePeriods">اطلاعات نمایشی دوره های مالی قابل دسترسی</param>
        public async Task SaveRoleFiscalPeriodsAsync(RelatedItemsViewModel rolePeriods)
        {
            Verify.ArgumentNotNull(rolePeriods, "rolePeriods");
            UnitOfWork.UseCompanyContext();
            var repository = UnitOfWork.GetAsyncRepository<RoleFiscalPeriod>();
            var existing = await repository.GetByCriteriaAsync(
                rfp => rfp.RoleId == rolePeriods.Id, rfp => rfp.FiscalPeriod);
            if (AreFiscalPeriodsModified(existing, rolePeriods))
            {
                if (existing.Count > 0)
                {
                    RemoveInaccessibleFiscalPeriods(repository, existing, rolePeriods);
                }

                AddNewFiscalPeriods(repository, existing, rolePeriods);
                await UnitOfWork.CommitAsync();
                OnEntityAction(OperationId.FiscalPeriodAccess);
                Log.Description = await GetRoleFiscalPeriodDescriptionAsync(rolePeriods.Id);
                await TrySaveLogAsync();
            }

            UnitOfWork.UseSystemContext();
        }

        /// <summary>
        /// به روش آسنکرون، آخرین وضعیت نقش های یک کاربر را ذخیره می کند
        /// </summary>
        /// <param name="userRoles">اطلاعات نمایشی نقش ها</param>
        public async Task SaveUserRolesAsync(RelatedItemsViewModel userRoles)
        {
            Verify.ArgumentNotNull(userRoles, "userRoles");
            var repository = UnitOfWork.GetAsyncRepository<User>();
            var existing = await repository.GetByIDWithTrackingAsync(userRoles.Id, r => r.UserRoles);
            if (existing != null && AreRolesModified(existing, userRoles))
            {
                if (existing.UserRoles.Count > 0)
                {
                    RemoveUnassignedRoles(existing, userRoles);
                }

                AddNewRoles(existing, userRoles);
                repository.Update(existing);
                await UnitOfWork.CommitAsync();
            }
        }

        /// <summary>
        /// به روش آسنکرون، تنظیمات دسترسی به سطرهای اطلاعاتی را برای نقش مشخص شده خوانده و برمی گرداند
        /// </summary>
        /// <param name="roleId">شناسه دیتابیسی یکی از نقش های امنیتی موجود</param>
        /// <returns>تنظیمات دسترسی به سطرهای اطلاعاتی</returns>
        public async Task<RowPermissionsForRoleViewModel> GetRowAccessSettingsAsync(int roleId)
        {
            var rowSettings = new RowPermissionsForRoleViewModel() { Id = roleId };
            var repository = UnitOfWork.GetAsyncRepository<ViewRowPermission>();
            var settings = await repository
                .GetByCriteriaAsync(perm => perm.Role.Id == roleId, perm => perm.Role, perm => perm.View);
            Array.ForEach(
                settings
                    .Select(perm => Mapper.Map<ViewRowPermissionViewModel>(perm))
                    .ToArray(),
                perm => rowSettings.RowPermissions.Add(perm));

            var viewRepository = UnitOfWork.GetAsyncRepository<View>();
            var views = await viewRepository
                .GetEntityQuery()
                .Where(view => !String.IsNullOrEmpty(view.FetchUrl))
                .ToArrayAsync();
            Array.ForEach(
                views
                    .Where(view => !settings
                        .Select(item => item.View.Id)
                        .Contains(view.Id))
                    .ToArray(),
                view =>
                {
                    var permission = new ViewRowPermissionViewModel()
                    {
                        RoleId = roleId,
                        ViewId = view.Id,
                        ViewName = view.Name
                    };
                    rowSettings.RowPermissions.Add(permission);
                });

            return rowSettings;
        }

        /// <summary>
        /// به روش آسنکرون، آخرین وضعیت تنظیمات دسترسی به سطرهای اطلاعاتی برای یک نقش را ذخیره می کند
        /// </summary>
        /// <param name="permissions">تنظیمات دسترسی به سطرهای اطلاعاتی برای یک نقش</param>
        public async Task SaveRowAccessSettingsAsync(RowPermissionsForRoleViewModel permissions)
        {
            Verify.ArgumentNotNull(permissions, "permissions");
            var repository = UnitOfWork.GetAsyncRepository<Role>();
            var role = await repository.GetByIDAsync(permissions.Id);
            if (role != null)
            {
                foreach (var permission in permissions.RowPermissions)
                {
                    await SaveViewRolePermissionAsync(permission);
                }

                await UnitOfWork.CommitAsync();
            }
        }

        /// <summary>
        /// مشخص می کند که آیا دسترسی داده شده قابل تخصیص به نقش های عمومی (غیر مدیر سیستم) هست یا نه؟
        /// </summary>
        /// <param name="permission">دسترسی مورد نظر برای بررسی</param>
        /// <returns>در صورت عمومی بودن دسترسی، مقدار بولی "درست" و در غیر این صورت
        /// مقدار بولی "نادرست" را برمی گرداند</returns>
        public bool IsPublicPermission(PermissionViewModel permission)
        {
            return permission.GroupId != 29;
        }

        /// <inheritdoc/>
        public override async Task InsertAsync(IRepository<Role> repository, Role entity)
        {
            OnEntityAction(OperationId.Create);
            Log.Description = GetState(entity);
            repository.Insert(entity, role => role.RolePermissions);
            await FinalizeActionAsync(entity);
        }

        /// <inheritdoc/>
        public override async Task UpdateAsync(IRepository<Role> repository, Role entity, RoleFullViewModel entityView)
        {
            var clone = new Role() { Id = entity.Id, Name = entity.Name, Description = entity.Description };
            OnEntityAction(OperationId.Edit);
            UpdateExisting(entityView, entity);
            Log.Description = String.Format("(Old) => {1}{0}(New) => {2}",
                Environment.NewLine, GetState(clone), GetState(entity));
            repository.UpdateWithTracking(entity);
            await FinalizeActionAsync(entity);
        }

        /// <inheritdoc/>
        public override async Task DeleteAsync(IRepository<Role> repository, Role entity)
        {
            OnEntityAction(OperationId.Delete);
            Log.Description = GetState(entity);
            repository.Delete(entity);
            await FinalizeActionAsync(entity);
        }

        internal override int? EntityType
        {
            get { return (int)SysEntityTypeId.Role; }
        }

        /// <summary>
        /// آخرین تغییرات موجودیت را از مدل نمایشی به سطر اطلاعاتی موجود کپی می کند
        /// </summary>
        /// <param name="roleView">مدل نمایشی شامل آخرین تغییرات</param>
        /// <param name="role">سطر اطلاعاتی موجود</param>
        protected override void UpdateExisting(RoleFullViewModel roleView, Role role)
        {
            role.Name = roleView.Role.Name;
            role.Description = roleView.Role.Description;
        }

        /// <summary>
        /// اطلاعات خلاصه سطر اطلاعاتی داده شده را به صورت یک رشته متنی برمی گرداند
        /// </summary>
        /// <param name="entity">یکی از سطرهای اطلاعاتی موجود</param>
        /// <returns>اطلاعات خلاصه سطر اطلاعاتی داده شده به صورت رشته متنی</returns>
        protected override string GetState(Role entity)
        {
            return (entity != null)
                ? String.Format(
                    "Name : {1}{0}Description : {2}",
                    Environment.NewLine, entity.Name, entity.Description)
                : null;
        }

        private static void RemoveDisabledPermissions(Role existing, RoleFullViewModel role)
        {
            var currentItems = role.Permissions
                .Where(perm => perm.IsEnabled)
                .Select(perm => perm.Id);
            var removedItems = existing.RolePermissions
                .Select(rp => rp.PermissionId)
                .Where(id => !currentItems.Contains(id))
                .ToArray();

            foreach (int id in removedItems)
            {
                existing.RolePermissions.Remove(existing.RolePermissions
                    .Where(rp => rp.PermissionId == id)
                    .Single());
            }
        }

        private static bool ArePermissionsModified(Role existing, RoleFullViewModel role)
        {
            var existingItems = existing.RolePermissions
                .Select(rp => rp.PermissionId)
                .ToArray();
            var enabledItems = role.Permissions
                .Where(perm => perm.IsEnabled)
                .Select(perm => perm.Id)
                .ToArray();
            return (!AreEqual(existingItems, enabledItems));
        }

        private static bool AreBranchesModified(IList<RoleBranch> existing, RelatedItemsViewModel roleItems)
        {
            var existingItems = existing
                .Select(rb => rb.BranchId)
                .ToArray();
            var enabledItems = roleItems.RelatedItems
                .Where(item => item.IsSelected)
                .Select(item => item.Id)
                .ToArray();
            return (!AreEqual(existingItems, enabledItems));
        }

        private static bool AreUsersModified(Role existing, RelatedItemsViewModel roleItems)
        {
            var existingItems = existing.UserRoles
                .Select(ur => ur.UserId)
                .ToArray();
            var enabledItems = roleItems.RelatedItems
                .Where(item => item.IsSelected)
                .Select(item => item.Id)
                .ToArray();
            return (!AreEqual(existingItems, enabledItems));
        }

        private static bool AreFiscalPeriodsModified(
            IList<RoleFiscalPeriod> existing, RelatedItemsViewModel roleItems)
        {
            var existingItems = existing
                .Select(rfp => rfp.FiscalPeriodId)
                .ToArray();
            var enabledItems = roleItems.RelatedItems
                .Where(item => item.IsSelected)
                .Select(item => item.Id)
                .ToArray();
            return (!AreEqual(existingItems, enabledItems));
        }

        private static bool AreRolesModified(User existing, RelatedItemsViewModel roleItems)
        {
            var existingItems = existing.UserRoles
                .Select(ur => ur.RoleId)
                .ToArray();
            var enabledItems = roleItems.RelatedItems
                .Where(item => item.IsSelected)
                .Select(item => item.Id)
                .ToArray();
            return (!AreEqual(existingItems, enabledItems));
        }

        private static void RemoveInaccessibleBranches(
            IRepository<RoleBranch> repository, IList<RoleBranch> existing, RelatedItemsViewModel roleItems)
        {
            var currentItems = roleItems.RelatedItems
                .Where(item => item.IsSelected)
                .Select(item => item.Id);
            var removedItems = existing
                .Select(rb => rb.BranchId)
                .Where(id => !currentItems.Contains(id))
                .ToArray();
            foreach (int id in removedItems)
            {
                var removed = existing
                    .Where(rb => rb.BranchId == id)
                    .Single();
                repository.Delete(removed);
            }
        }

        private static void RemoveUnassignedUsers(Role existing, RelatedItemsViewModel role)
        {
            var currentItems = role.RelatedItems
                .Where(item => item.IsSelected)
                .Select(item => item.Id);
            var removedItems = existing.UserRoles
                .Select(ur => ur.UserId)
                .Where(id => !currentItems.Contains(id))
                .ToArray();
            foreach (int id in removedItems)
            {
                existing.UserRoles.Remove(existing.UserRoles
                    .Where(ur => ur.UserId == id)
                    .Single());
            }
        }

        private static void RemoveInaccessibleFiscalPeriods(
            IRepository<RoleFiscalPeriod> repository, IList<RoleFiscalPeriod> existing, RelatedItemsViewModel roleItems)
        {
            var currentItems = roleItems.RelatedItems
                .Where(item => item.IsSelected)
                .Select(item => item.Id);
            var removedItems = existing
                .Select(rfp => rfp.FiscalPeriodId)
                .Where(id => !currentItems.Contains(id))
                .ToArray();
            foreach (int id in removedItems)
            {
                var removed = existing
                    .Where(rfp => rfp.FiscalPeriodId == id)
                    .Single();
                repository.Delete(removed);
            }
        }

        private static void RemoveUnassignedRoles(User existing, RelatedItemsViewModel role)
        {
            var currentItems = role.RelatedItems
                .Where(item => item.IsSelected)
                .Select(item => item.Id);
            var removedItems = existing.UserRoles
                .Select(ur => ur.RoleId)
                .Where(id => !currentItems.Contains(id))
                .ToArray();
            foreach (int id in removedItems)
            {
                existing.UserRoles.Remove(existing.UserRoles
                    .Where(ur => ur.RoleId == id)
                    .Single());
            }
        }

        private static bool AreEqual(IEnumerable<int> left, IEnumerable<int> right)
        {
            return left.Count() == right.Count()
                && left.All(value => right.Contains(value));
        }

        private static void UpdateExistingRowPermission(ViewRowPermission existing, ViewRowPermissionViewModel permission)
        {
            existing.AccessMode = permission.AccessMode;
            existing.Value = permission.Value;
            existing.Value2 = permission.Value2;
            existing.TextValue = permission.TextValue;
            existing.Items = permission.Items.Count > 0
                ? String.Join(",", permission.Items)
                : null;
        }

        private void AddNewPermissions(Role existing, RoleFullViewModel role)
        {
            var repository = UnitOfWork.GetRepository<Permission>();
            var currentItems = existing.RolePermissions.Select(rp => rp.PermissionId);
            var newItems = role.Permissions
                .Where(perm => perm.IsEnabled
                    && !currentItems.Contains(perm.Id));
            foreach (var item in newItems)
            {
                var permission = repository.GetByIDWithTracking(item.Id, perm => perm.Group);
                var rolePermission = new RolePermission()
                {
                    Permission = permission,
                    PermissionId = permission.Id,
                    Role = existing,
                    RoleId = existing.Id
                };
                existing.RolePermissions.Add(rolePermission);
            }
        }

        private void AddNewBranches(
            IRepository<RoleBranch> repository, IList<RoleBranch> existing, RelatedItemsViewModel roleItems)
        {
            var branchRepository = UnitOfWork.GetRepository<Branch>();
            var currentItems = existing.Select(rb => rb.BranchId);
            var newItems = roleItems.RelatedItems
                .Where(item => item.IsSelected
                    && !currentItems.Contains(item.Id));
            foreach (var item in newItems)
            {
                var branch = branchRepository.GetByID(item.Id);
                var roleBranch = new RoleBranch()
                {
                    Branch = branch,
                    BranchId = branch.Id,
                    RoleId = roleItems.Id
                };
                repository.Insert(roleBranch);
            }
        }

        private void AddNewUsers(Role existing, RelatedItemsViewModel roleItems)
        {
            var repository = UnitOfWork.GetRepository<User>();
            var currentItems = existing.UserRoles.Select(ur => ur.UserId);
            var newItems = roleItems.RelatedItems
                .Where(item => item.IsSelected
                    && !currentItems.Contains(item.Id));
            foreach (var item in newItems)
            {
                var user = repository.GetByIDWithTracking(item.Id);
                var userRole = new UserRole()
                {
                    User = user,
                    UserId = user.Id,
                    Role = existing,
                    RoleId = existing.Id
                };
                existing.UserRoles.Add(userRole);
            }
        }

        private void AddNewFiscalPeriods(
            IRepository<RoleFiscalPeriod> repository, IList<RoleFiscalPeriod> existing, RelatedItemsViewModel roleItems)
        {
            var periodRepository = UnitOfWork.GetRepository<FiscalPeriod>();
            var currentItems = existing.Select(rfp => rfp.FiscalPeriodId);
            var newItems = roleItems.RelatedItems
                .Where(item => item.IsSelected
                    && !currentItems.Contains(item.Id));
            foreach (var item in newItems)
            {
                var fiscalPeriod = periodRepository.GetByID(item.Id);
                var roleFiscalPeriod = new RoleFiscalPeriod()
                {
                    FiscalPeriod = fiscalPeriod,
                    FiscalPeriodId = fiscalPeriod.Id,
                    RoleId = roleItems.Id
                };
                repository.Insert(roleFiscalPeriod);
            }
        }

        private void AddNewRoles(User existing, RelatedItemsViewModel roleItems)
        {
            var repository = UnitOfWork.GetRepository<Role>();
            var currentItems = existing.UserRoles.Select(ur => ur.RoleId);
            var newItems = roleItems.RelatedItems
                .Where(item => item.IsSelected
                    && !currentItems.Contains(item.Id));
            foreach (var item in newItems)
            {
                var role = repository.GetByIDWithTracking(item.Id);
                var userRole = new UserRole()
                {
                    User = existing,
                    UserId = existing.Id,
                    Role = role,
                    RoleId = role.Id
                };
                existing.UserRoles.Add(userRole);
            }
        }

        private void AddRolePermissions(Role role, RoleFullViewModel roleViewModel)
        {
            Array.ForEach(
                roleViewModel.Permissions
                    .Where(perm => perm.IsEnabled)
                    .ToArray(),
                perm => role.RolePermissions.Add(GetNewRolePermission(perm, role)));
        }

        private RolePermission GetNewRolePermission(PermissionViewModel perm, Role role)
        {
            var permission = Mapper.Map<Permission>(perm);
            return new RolePermission()
            {
                Role = role,
                RoleId = role.Id,
                Permission = permission,
                PermissionId = permission.Id
            };
        }

        private async Task SaveViewRolePermissionAsync(ViewRowPermissionViewModel rowPermission)
        {
            var repository = UnitOfWork.GetAsyncRepository<ViewRowPermission>();
            if (rowPermission.Id == 0)
            {
                if (rowPermission.AccessMode != RowAccessOptions.Default)
                {
                    var newRowPermission = Mapper.Map<ViewRowPermission>(rowPermission);
                    repository.Insert(newRowPermission);
                }
            }
            else
            {
                var existing = await repository.GetByIDAsync(rowPermission.Id);
                if (existing != null)
                {
                    if (rowPermission.AccessMode == RowAccessOptions.Default)
                    {
                        repository.Delete(existing);
                    }
                    else
                    {
                        UpdateExistingRowPermission(existing, rowPermission);
                        repository.Update(existing);
                    }
                }
            }
        }

        private async Task<string> GetRoleBranchDescriptionAsync(int roleId)
        {
            var builder = new StringBuilder();
            UnitOfWork.UseSystemContext();
            var roleRepository = UnitOfWork.GetAsyncRepository<Role>();
            var role = await roleRepository.GetByIDAsync(roleId);
            UnitOfWork.UseCompanyContext();
            if (role != null)
            {
                var repository = UnitOfWork.GetAsyncRepository<RoleBranch>();
                var existing = await repository.GetByCriteriaAsync(
                    rb => rb.RoleId == roleId, rb => rb.Branch);
                builder.AppendFormat("Role : {0} , Accessible branches : ", role.Name);
                if (existing.Count > 0)
                {
                    builder.Append(String.Join(",", existing.Select(rb => rb.Branch.Name)));
                }
                else
                {
                    builder.Append("(None)");
                }
            }

            return builder.ToString();
        }

        private async Task<string> GetRoleFiscalPeriodDescriptionAsync(int roleId)
        {
            var builder = new StringBuilder();
            UnitOfWork.UseSystemContext();
            var roleRepository = UnitOfWork.GetAsyncRepository<Role>();
            var role = await roleRepository.GetByIDAsync(roleId);
            UnitOfWork.UseCompanyContext();
            if (role != null)
            {
                var repository = UnitOfWork.GetAsyncRepository<RoleFiscalPeriod>();
                var existing = await repository.GetByCriteriaAsync(
                    rfp => rfp.RoleId == roleId, rfp => rfp.FiscalPeriod);
                builder.AppendFormat("Role : {0} , Accessible fiscal periods : ", role.Name);
                if (existing.Count > 0)
                {
                    builder.Append(String.Join(",", existing.Select(rfp => rfp.FiscalPeriod.Name)));
                }
                else
                {
                    builder.Append("(None)");
                }
            }

            return builder.ToString();
        }

        private async Task<string> GetRoleUserDescriptionAsync(int roleId)
        {
            var builder = new StringBuilder();
            var roleRepository = UnitOfWork.GetAsyncRepository<Role>();
            var role = await roleRepository.GetByIDAsync(roleId);
            if (role != null)
            {
                var repository = UnitOfWork.GetAsyncRepository<UserRole>();
                var existing = await repository.GetByCriteriaAsync(
                    ur => ur.RoleId == roleId, ur => ur.User);
                builder.AppendFormat("Role : {0} , Assigned users : ", role.Name);
                if (existing.Count > 0)
                {
                    builder.Append(String.Join(",", existing.Select(ur => ur.User.UserName)));
                }
                else
                {
                    builder.Append("(None)");
                }
            }

            return builder.ToString();
        }
    }
}
