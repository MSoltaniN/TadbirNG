using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPPC.Framework.Common;
using SPPC.Framework.Extensions;
using SPPC.Framework.Mapper;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Configuration;
using SPPC.Tadbir.Model.Auth;
using SPPC.Tadbir.Model.Corporate;
using SPPC.Tadbir.Model.Finance;
using SPPC.Tadbir.Model.Metadata;
using SPPC.Tadbir.ViewModel;
using SPPC.Tadbir.ViewModel.Auth;
using SPPC.Tadbir.ViewModel.Corporate;
using SPPC.Tadbir.ViewModel.Metadata;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای مدیریت اطلاعات نقش ها را پیاده سازی می کند
    /// </summary>
    public class RoleRepository : RepositoryBase, IRoleRepository
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="unitOfWork">پیاده سازی اینترفیس واحد کاری برای انجام عملیات دیتابیسی </param>
        /// <param name="mapper">نگاشت مورد استفاده برای تبدیل کلاس های مدل اطلاعاتی</param>
        /// <param name="metadata">امکان خواندن متادیتا برای یک موجودیت را فراهم می کند</param>
        public RoleRepository(IAppUnitOfWork unitOfWork, IDomainMapper mapper, IMetadataRepository metadata)
            : base(unitOfWork, mapper, metadata)
        {
        }

        /// <summary>
        /// به روش آسنکرون، لیست نقش های تعریف شده را از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>لیست نقش های تعریف شده</returns>
        public async Task<IList<RoleViewModel>> GetRolesAsync(GridOptions gridOptions = null)
        {
            var repository = UnitOfWork.GetAsyncRepository<Role>();
            var query = repository
                .GetEntityQuery()
                .Include(r => r.RolePermissions)
                    .ThenInclude(rp => rp.Permission);

            return await query
                .Apply(gridOptions)
                .Select(r => Mapper.Map<RoleViewModel>(r))
                .ToListAsync();
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
                    .OrderBy(perm => perm.Id)
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
                .Include(r => r.RoleBranches)
                    .ThenInclude(rb => rb.Branch)
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
                    existing.RoleBranches.Select(rb => rb.Branch).ToArray(),
                    br => role.Branches.Add(Mapper.Map<BranchViewModel>(br)));
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
        /// به روش آسنکرون، اطلاعات فراداده ای تعریف شده برای نقش را از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <returns>اطلاعات فراداده ای تعریف شده برای نقش</returns>
        public async Task<EntityViewModel> GetRoleMetadataAsync()
        {
            return await Metadata.GetEntityMetadataAsync<Role>();
        }

        /// <summary>
        /// به روش آسنکرون، تعداد نقش های تعریف شده را از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>تعداد نقش های تعریف شده</returns>
        public async Task<int> GetRoleCountAsync(GridOptions gridOptions = null)
        {
            var repository = UnitOfWork.GetAsyncRepository<Role>();
            var count = await repository.GetCountByCriteriaAsync(null, gridOptions);
            return count;
        }

        /// <summary>
        /// Asynchronously inserts or updates a single security role, including all permissions in it, in repository
        /// </summary>
        /// <param name="role">Role to insert or update</param>
        public async Task<RoleViewModel> SaveRoleAsync(RoleFullViewModel role)
        {
            Verify.ArgumentNotNull(role, "role");
            Verify.ArgumentNotNull(role.Role, "role.Role");
            Role outputRole = null;
            var repository = UnitOfWork.GetAsyncRepository<Role>();
            if (role.Role.Id == 0)
            {
                outputRole = Mapper.Map<Role>(role.Role);
                AddRolePermissions(outputRole, role);
                repository.Insert(outputRole, r => r.RolePermissions);
            }
            else
            {
                outputRole = await repository.GetByIDWithTrackingAsync(role.Role.Id, r => r.RolePermissions);
                if (outputRole != null)
                {
                    if (ArePermissionsModified(outputRole, role))
                    {
                        if (outputRole.RolePermissions.Count > 0)
                        {
                            RemoveDisabledPermissions(outputRole, role);
                        }

                        AddNewPermissions(outputRole, role);
                    }

                    UpdateExistingRole(outputRole, role);
                    repository.UpdateWithTracking(outputRole);
                }
            }

            await UnitOfWork.CommitAsync();
            return Mapper.Map<RoleViewModel>(outputRole);
        }

        /// <summary>
        /// Asynchronously deletes a role specified by unique identifier from repository.
        /// </summary>
        /// <param name="roleId">Unique identifier of the role to delete</param>
        /// <remarks>If no role with specified identifier could be found, no exception would be thrown.</remarks>
        public async Task DeleteRoleAsync(int roleId)
        {
            var repository = UnitOfWork.GetAsyncRepository<Role>();
            var role = await repository.GetByIDWithTrackingAsync(roleId, r => r.RolePermissions, r => r.RoleBranches);
            if (role != null)
            {
                role.RolePermissions.Clear();
                role.RoleBranches.Clear();
                repository.Delete(role);
                await UnitOfWork.CommitAsync();
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
            bool isAssigned = false;
            var repository = UnitOfWork.GetAsyncRepository<Role>();
            var role = await repository
                .GetEntityQuery(r => r.RoleBranches)
                .Where(r => r.Id == roleId)
                .SingleOrDefaultAsync();
            if (role != null)
            {
                isAssigned = (role.RoleBranches.Count > 0);
            }

            return isAssigned;
        }

        /// <summary>
        /// به روش آسنکرون، مشخص می کند که آیا نقش مورد نظر با یک یا چند دوره مالی مرتبط شده یا نه
        /// </summary>
        /// <param name="roleId">شناسه دیتابیسی نقش مورد نظر</param>
        /// <returns>اگر نقش مورد نظر با یک یا چند دوره مالی مرتبط شده مقدار "درست" و
        /// در غیر این صورت مقدار "نادرست" را برمی گرداند</returns>
        public async Task<bool> IsRoleRelatedToFiscalPeriodAsync(int roleId)
        {
            bool isAssigned = false;
            var repository = UnitOfWork.GetAsyncRepository<Role>();
            var role = await repository
                .GetEntityQuery(r => r.RoleFiscalPeriods)
                .Where(r => r.Id == roleId)
                .SingleOrDefaultAsync();
            if (role != null)
            {
                isAssigned = (role.RoleFiscalPeriods.Count > 0);
            }

            return isAssigned;
        }

        /// <summary>
        /// به روش آسنکرون، شعبه های قابل دسترسی توسط یک نقش را خوانده و برمی گرداند
        /// </summary>
        /// <param name="roleId">شناسه یکی از نقش های موجود</param>
        /// <returns>اطلاعات نمایشی شعبه های قابل دسترسی</returns>
        public async Task<RelatedItemsViewModel> GetRoleBranchesAsync(int roleId)
        {
            RelatedItemsViewModel roleBranches = null;
            var repository = UnitOfWork.GetAsyncRepository<Role>();
            var existing = await repository
                .GetEntityQuery()
                .Include(r => r.RoleBranches)
                    .ThenInclude(rb => rb.Branch)
                .Where(r => r.Id == roleId)
                .SingleOrDefaultAsync();
            if (existing != null)
            {
                var enabledBranches = existing.RoleBranches
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

                roleBranches = Mapper.Map<RelatedItemsViewModel>(existing);
                Array.ForEach(enabledBranches
                    .Concat(disabledBranches)
                    .OrderBy(item => item.Id)
                    .ToArray(), item => roleBranches.RelatedItems.Add(item));
            }

            return roleBranches;
        }

        /// <summary>
        /// به روش آسنکرون، آخرین وضعیت شعبه های قابل دسترسی توسط یک نقش را ذخیره می کند
        /// </summary>
        /// <param name="roleBranches">اطلاعات نمایشی شعبه های قابل دسترسی</param>
        public async Task SaveRoleBranchesAsync(RelatedItemsViewModel roleBranches)
        {
            Verify.ArgumentNotNull(roleBranches, "role");
            var repository = UnitOfWork.GetAsyncRepository<Role>();
            var existing = await repository.GetByIDWithTrackingAsync(roleBranches.Id, r => r.RoleBranches);
            if (existing != null && AreBranchesModified(existing, roleBranches))
            {
                if (existing.RoleBranches.Count > 0)
                {
                    RemoveInaccessibleBranches(existing, roleBranches);
                }

                AddNewBranches(existing, roleBranches);
                repository.Update(existing);
                await UnitOfWork.CommitAsync();
            }
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
            }
        }

        /// <summary>
        /// به روش آسنکرون، دوره های مالی قابل دسترسی توسط یک نقش را خوانده و برمی گرداند
        /// </summary>
        /// <param name="roleId">شناسه یکی از نقش های موجود</param>
        /// <returns>اطلاعات نمایشی دوره های مالی قابل دسترسی</returns>
        public async Task<RelatedItemsViewModel> GetRoleFiscalPeriodsAsync(int roleId)
        {
            RelatedItemsViewModel rolePeriods = null;
            var repository = UnitOfWork.GetAsyncRepository<Role>();
            var existing = await repository
                .GetEntityQuery()
                .Include(r => r.RoleFiscalPeriods)
                    .ThenInclude(rfp => rfp.FiscalPeriod)
                .Where(r => r.Id == roleId)
                .SingleOrDefaultAsync();
            if (existing != null)
            {
                var enabledPeriods = existing.RoleFiscalPeriods
                    .Select(rfp => rfp.FiscalPeriod)
                    .Select(fp => Mapper.Map<RelatedItemViewModel>(fp))
                    .ToArray();
                var periodRepository = UnitOfWork.GetAsyncRepository<FiscalPeriod>();
                var allPeriods = await periodRepository
                    .GetAllAsync();
                var disabledPeriods = allPeriods
                    .Select(fp => Mapper.Map<RelatedItemViewModel>(fp))
                    .Except(enabledPeriods, new EntityEqualityComparer<RelatedItemViewModel>())
                    .ToArray();
                Array.ForEach(enabledPeriods, item => item.IsSelected = true);

                rolePeriods = Mapper.Map<RelatedItemsViewModel>(existing);
                Array.ForEach(enabledPeriods
                    .Concat(disabledPeriods)
                    .OrderBy(item => item.Id)
                    .ToArray(), item => rolePeriods.RelatedItems.Add(item));
            }

            return rolePeriods;
        }

        /// <summary>
        /// به روش آسنکرون، آخرین وضعیت دوره های مالی قابل دسترسی توسط یک نقش را ذخیره می کند
        /// </summary>
        /// <param name="rolePeriods">اطلاعات نمایشی دوره های مالی قابل دسترسی</param>
        public async Task SaveRoleFiscalPeriodsAsync(RelatedItemsViewModel rolePeriods)
        {
            Verify.ArgumentNotNull(rolePeriods, "rolePeriods");
            var repository = UnitOfWork.GetAsyncRepository<Role>();
            var existing = await repository.GetByIDWithTrackingAsync(rolePeriods.Id, r => r.RoleFiscalPeriods);
            if (existing != null && AreFiscalPeriodsModified(existing, rolePeriods))
            {
                if (existing.RoleFiscalPeriods.Count > 0)
                {
                    RemoveInaccessibleFiscalPeriods(existing, rolePeriods);
                }

                AddNewFiscalPeriods(existing, rolePeriods);
                repository.Update(existing);
                await UnitOfWork.CommitAsync();
            }
        }

        /// <summary>
        /// به روش آسنکرون، نقش های یک کاربر را خوانده و برمی گرداند
        /// </summary>
        /// <param name="userId">شناسه یکی از کاربران موجود</param>
        /// <returns>اطلاعات نمایشی نقش ها</returns>
        public async Task<RelatedItemsViewModel> GetUserRolesAsync(int userId)
        {
            RelatedItemsViewModel userRoles = null;
            var repository = UnitOfWork.GetAsyncRepository<User>();
            var existing = await repository
                .GetEntityQuery()
                .Include(u => u.UserRoles)
                    .ThenInclude(ur => ur.Role)
                .Where(u => u.Id == userId)
                .SingleOrDefaultAsync();
            if (existing != null)
            {
                var enabledRoles = existing.UserRoles
                    .Select(ur => ur.Role)
                    .Select(r => Mapper.Map<RelatedItemViewModel>(r))
                    .ToArray();
                var roleRepository = UnitOfWork.GetAsyncRepository<Role>();
                var allRoles = await roleRepository
                    .GetAllAsync();
                var disabledRoles = allRoles
                    .Select(r => Mapper.Map<RelatedItemViewModel>(r))
                    .Except(enabledRoles, new EntityEqualityComparer<RelatedItemViewModel>())
                    .ToArray();
                Array.ForEach(enabledRoles, item => item.IsSelected = true);

                userRoles = Mapper.Map<RelatedItemsViewModel>(existing);
                Array.ForEach(enabledRoles
                    .Concat(disabledRoles)
                    .OrderBy(item => item.Id)
                    .ToArray(), item => userRoles.RelatedItems.Add(item));
            }

            return userRoles;
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

            var viewRepository = UnitOfWork.GetAsyncRepository<Entity>();
            var viewIds = await viewRepository
                .GetEntityQuery()
                .Select(view => view.Id)
                .ToArrayAsync();
            Array.ForEach(
                viewIds
                    .Except(settings.Select(perm => perm.View.Id))
                    .ToArray(),
                id =>
                {
                    var permission = new ViewRowPermissionViewModel() { RoleId = roleId, ViewId = id };
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

        private static void UpdateExistingRole(Role existing, RoleFullViewModel role)
        {
            existing.Name = role.Role.Name;
            existing.Description = role.Role.Description;
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

        private static bool AreBranchesModified(Role existing, RelatedItemsViewModel roleItems)
        {
            var existingItems = existing.RoleBranches
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

        private static bool AreFiscalPeriodsModified(Role existing, RelatedItemsViewModel roleItems)
        {
            var existingItems = existing.RoleFiscalPeriods
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

        private static void RemoveInaccessibleBranches(Role existing, RelatedItemsViewModel roleItems)
        {
            var currentItems = roleItems.RelatedItems
                .Where(item => item.IsSelected)
                .Select(item => item.Id);
            var removedItems = existing.RoleBranches
                .Select(rb => rb.BranchId)
                .Where(id => !currentItems.Contains(id))
                .ToArray();
            foreach (int id in removedItems)
            {
                existing.RoleBranches.Remove(existing.RoleBranches
                    .Where(rb => rb.BranchId == id)
                    .Single());
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

        private static void RemoveInaccessibleFiscalPeriods(Role existing, RelatedItemsViewModel roleItems)
        {
            var currentItems = roleItems.RelatedItems
                .Where(item => item.IsSelected)
                .Select(item => item.Id);
            var removedItems = existing.RoleFiscalPeriods
                .Select(rfp => rfp.FiscalPeriodId)
                .Where(id => !currentItems.Contains(id))
                .ToArray();
            foreach (int id in removedItems)
            {
                existing.RoleFiscalPeriods.Remove(existing.RoleFiscalPeriods
                    .Where(rfp => rfp.FiscalPeriodId == id)
                    .Single());
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

        private void AddNewBranches(Role existing, RelatedItemsViewModel roleItems)
        {
            var repository = UnitOfWork.GetRepository<Branch>();
            var currentItems = existing.RoleBranches.Select(rb => rb.BranchId);
            var newItems = roleItems.RelatedItems
                .Where(item => item.IsSelected
                    && !currentItems.Contains(item.Id));
            foreach (var item in newItems)
            {
                var branch = repository.GetByIDWithTracking(item.Id);
                var roleBranch = new RoleBranch()
                {
                    Branch = branch,
                    BranchId = branch.Id,
                    Role = existing,
                    RoleId = existing.Id
                };
                existing.RoleBranches.Add(roleBranch);
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

        private void AddNewFiscalPeriods(Role existing, RelatedItemsViewModel roleItems)
        {
            var repository = UnitOfWork.GetRepository<FiscalPeriod>();
            var currentItems = existing.RoleFiscalPeriods.Select(rfp => rfp.FiscalPeriodId);
            var newItems = roleItems.RelatedItems
                .Where(item => item.IsSelected
                    && !currentItems.Contains(item.Id));
            foreach (var item in newItems)
            {
                var fiscalPeriod = repository.GetByIDWithTracking(item.Id);
                var roleFiscalPeriod = new RoleFiscalPeriod()
                {
                    FiscalPeriod = fiscalPeriod,
                    FiscalPeriodId = fiscalPeriod.Id,
                    Role = existing,
                    RoleId = existing.Id
                };
                existing.RoleFiscalPeriods.Add(roleFiscalPeriod);
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
    }
}
