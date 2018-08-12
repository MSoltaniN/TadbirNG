using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPPC.Framework.Common;
using SPPC.Framework.Extensions;
using SPPC.Framework.Mapper;
using SPPC.Framework.Persistence;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Configuration;
using SPPC.Tadbir.Model.Auth;
using SPPC.Tadbir.Model.Contact;
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
    /// Provides repository operations related to security administration.
    /// </summary>
    public class SecurityRepository : ISecurityRepository
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="unitOfWork">پیاده سازی اینترفیس واحد کاری برای انجام عملیات دیتابیسی </param>
        /// <param name="mapper">نگاشت مورد استفاده برای تبدیل کلاس های مدل اطلاعاتی</param>
        /// <param name="metadataRepository">امکان خواندن متادیتا برای یک موجودیت را فراهم می کند</param>
        public SecurityRepository(IAppUnitOfWork unitOfWork, IDomainMapper mapper, IMetadataRepository metadataRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _metadataRepository = metadataRepository;
        }

        #region User Management operations

        /// <summary>
        /// به روش آسنکرون، لیست کاربران برنامه را از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>لیست کاربران برنامه</returns>
        public async Task<IList<UserViewModel>> GetUsersAsync(GridOptions gridOptions = null)
        {
            var repository = _unitOfWork.GetAsyncRepository<User>();
            var users = await repository
                .GetAllAsync(gridOptions, u => u.Person);
            return users
                .Select(user => _mapper.Map<UserViewModel>(user))
                .ToList();
        }

        /// <summary>
        /// Asynchronously retrieves a single user specified by user name from repository.
        /// </summary>
        /// <param name="userName">User name to search for</param>
        /// <returns>A <see cref="UserViewModel"/> instance that corresponds to the specified user name, if there is
        /// such a user defined; otherwise, returns null.</returns>
        public async Task<UserViewModel> GetUserAsync(string userName)
        {
            UserViewModel userViewModel = null;
            var repository = _unitOfWork.GetAsyncRepository<User>();
            var users = await repository
                .GetByCriteriaAsync(usr => usr.UserName == userName, usr => usr.Person);
            var user = users.SingleOrDefault();
            if (user != null)
            {
                userViewModel = _mapper.Map<UserViewModel>(user);
            }

            return userViewModel;
        }

        /// <summary>
        /// Asynchronously retrieves a single user specified by unique identifier from repository.
        /// </summary>
        /// <param name="userId">Unique identifier of the user to search for</param>
        /// <returns>A <see cref="UserViewModel"/> instance that corresponds to the specified identifier, if there is
        /// such a user defined; otherwise, returns null.</returns>
        public async Task<UserViewModel> GetUserAsync(int userId)
        {
            UserViewModel userViewModel = null;
            var repository = _unitOfWork.GetAsyncRepository<User>();
            var user = await repository.GetByIDAsync(userId, usr => usr.Person);
            if (user != null)
            {
                userViewModel = _mapper.Map<UserViewModel>(user);
            }

            return userViewModel;
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات فراداده ای تعریف شده برای کاربر را از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <returns>اطلاعات فراداده ای تعریف شده برای کاربر</returns>
        public async Task<EntityViewModel> GetUserMetadataAsync()
        {
            return await _metadataRepository.GetEntityMetadataAsync<User>();
        }

        /// <summary>
        /// Asynchronously retrieves context information for a user specified by unique identifier from repository.
        /// </summary>
        /// <param name="userId">Unique identifier of the user to search for</param>
        /// <returns>A <see cref="UserContextViewModel"/> instance containing context information, if there is
        /// such a user defined; otherwise, returns null.</returns>
        public async Task<UserContextViewModel> GetUserContextAsync(int userId)
        {
            UserContextViewModel userContext = null;
            var repository = _unitOfWork.GetAsyncRepository<User>();
            var user = await repository.GetByIDAsync(userId, usr => usr.Person, usr => usr.UserRoles);
            if (user != null)
            {
                var permissions = new List<PermissionBriefViewModel>();
                var branches = new List<int>();
                var roleRepository = _unitOfWork.GetAsyncRepository<Role>();
                foreach (var roleId in user.UserRoles.Select(ur => ur.RoleId))
                {
                    var role = await roleRepository.GetByIDAsync(roleId, r => r.RoleBranches, r => r.RolePermissions);
                    userContext = _mapper.Map<UserContextViewModel>(user);
                    userContext.Roles.Add(roleId);
                    branches.AddRange(role.RoleBranches.Select(rb => rb.BranchId));
                    Array.ForEach(
                        role.RolePermissions.ToArray(),
                        rp => permissions.Add(_mapper.Map<PermissionBriefViewModel>(
                            _unitOfWork.GetRepository<Permission>().GetByID(rp.PermissionId, perm => perm.Group))));
                }

                Array.ForEach(branches.Distinct().ToArray(), br => userContext.Branches.Add(br));
                var groups = permissions
                    .Distinct(new PermissionEqualityComparer())
                    .GroupBy(perm => perm.EntityName);
                foreach (var group in groups)
                {
                    var permission = new PermissionBriefViewModel()
                    {
                        EntityName = group.Key,
                        Flags = group.Sum(perm => perm.Flags)
                    };
                    userContext.Permissions.Add(permission);
                }
            }

            return userContext;
        }

        /// <summary>
        /// دسترسی های امنیتی داده شده به یک کاربر را به صورت مجموعه ای از شناسه های دیتابیسی
        /// از دیتابیس خوانده و بر می گرداند
        /// </summary>
        /// <param name="userId">شناسه دیتابیسی یکی از کاربران موجود</param>
        /// <returns>مجموعه شناسه های دسترسی های داده شده به کاربر</returns>
        public async Task<IList<int>> GetUserPermissionIdsAsync(int userId)
        {
            var permissionIds = new List<int>();
            var query = GetUserPermissionsQuery(userId);
            var user = await query.SingleOrDefaultAsync();
            if (user != null)
            {
                Array.ForEach(user.UserRoles.Select(ur => ur.Role).ToArray(),
                    role => permissionIds.AddRange(role.RolePermissions.Select(rp => rp.PermissionId)));
            }

            return permissionIds
                .Distinct()
                .ToList();
        }

        /// <summary>
        /// اطلاعات نمایشی تمام دستورات قابل دسترسی توسط کاربر مشخص شده را از دیتابیس خوانده و برمی گرداند
        /// </summary>
        /// <param name="userId">شناسه دیتابیسی یکی از کاربران موجود</param>
        /// <returns>مجموعه ای از دستورات قابل دسترسی توسط کاربر</returns>
        public async Task<IList<CommandViewModel>> GetUserCommandsAsync(int userId)
        {
            var topCommands = await _metadataRepository.GetTopLevelCommandsAsync();
            var userCommands = new List<CommandViewModel>(topCommands.Count);
            var userPermissions = await GetUserPermissionIdsAsync(userId);
            foreach (var command in topCommands)
            {
                var topCommand = new CommandViewModel() { Id = command.Id, Title = command.Title };
                foreach (var child in command.Children)
                {
                    if (child.PermissionId == null || userPermissions.Contains(child.PermissionId.Value))
                    {
                        topCommand.Children.Add(child);
                    }
                }

                if (topCommand.Children.Count > 0)
                {
                    userCommands.Add(topCommand);
                }
            }

            return userCommands;
        }

        /// <summary>
        /// به روش آسنکرون، تعداد کاربران تعریف شده را از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>تعداد کاربران تعریف شده</returns>
        public async Task<int> GetUserCountAsync(GridOptions gridOptions = null)
        {
            var repository = _unitOfWork.GetAsyncRepository<User>();
            var count = await repository.GetCountByCriteriaAsync(null, gridOptions);
            return count;
        }

        /// <summary>
        /// Asynchronously inserts or updates a single user in repository.
        /// </summary>
        /// <param name="user">Item to insert or update</param>
        public async Task<UserViewModel> SaveUserAsync(UserViewModel user)
        {
            Verify.ArgumentNotNull(user, "user");
            User userModel = default(User);
            var repository = _unitOfWork.GetAsyncRepository<User>();
            if (user.Id == 0)
            {
                userModel = GetNewUser(user);
                repository.Insert(userModel, usr => usr.Person);
            }
            else
            {
                userModel = await repository.GetByIDAsync(user.Id, u => u.Person);
                if (userModel != null)
                {
                    UpdateExistingUser(userModel, user);
                    repository.Update(userModel, usr => usr.Person);
                }
            }

            await _unitOfWork.CommitAsync();
            return _mapper.Map<UserViewModel>(userModel);
        }

        /// <summary>
        /// Asynchronously sets LastLoginDate field of the specified user to current system date/time.
        /// </summary>
        /// <param name="userId">Unique identifier of an existing user</param>
        public async Task UpdateUserLastLoginAsync(int userId)
        {
            var repository = _unitOfWork.GetAsyncRepository<User>();
            var user = await repository.GetByIDAsync(userId);
            if (user != null)
            {
                user.LastLoginDate = DateTime.Now;
                repository.Update(user);
                await _unitOfWork.CommitAsync();
            }
        }

        /// <summary>
        /// Asynchronously updates a user profile in repository.
        /// </summary>
        /// <param name="profile">User profile to update</param>
        public async Task UpdateUserPasswordAsync(UserProfileViewModel profile)
        {
            Verify.ArgumentNotNull(profile, "profile");
            var repository = _unitOfWork.GetAsyncRepository<User>();
            var users = await repository
                .GetByCriteriaAsync(usr => usr.UserName == profile.UserName);
            var user = users.SingleOrDefault();
            if (user != null)
            {
                user.PasswordHash = profile.NewPassword;
                repository.Update(user);
                await _unitOfWork.CommitAsync();
            }
        }

        /// <summary>
        /// Asynchronously determines if the specified <see cref="UserViewModel"/> instance has a user name that is already used
        /// by a different user.
        /// </summary>
        /// <param name="user">User item to check for duplicate user name</param>
        /// <returns>True if the user name is already used; otherwise returns false.</returns>
        public async Task<bool> IsDuplicateUserAsync(UserViewModel user)
        {
            Verify.ArgumentNotNull(user, "user");
            var repository = _unitOfWork.GetAsyncRepository<User>();
            var items = await repository
                .GetByCriteriaAsync(usr => usr.Id != user.Id
                    && usr.UserName == user.UserName);
            var existing = items.SingleOrDefault();
            return (existing != null);
        }

        #endregion

        #region Role Management operations

        /// <summary>
        /// به روش آسنکرون، لیست نقش های تعریف شده را از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>لیست نقش های تعریف شده</returns>
        public async Task<IList<RoleViewModel>> GetRolesAsync(GridOptions gridOptions = null)
        {
            var repository = _unitOfWork.GetAsyncRepository<Role>();
            var query = repository
                .GetEntityQuery()
                .Include(r => r.RolePermissions)
                    .ThenInclude(rp => rp.Permission);

            return await query
                .Apply(gridOptions)
                .Select(r => _mapper.Map<RoleViewModel>(r))
                .ToListAsync();
        }

        /// <summary>
        /// Asynchronously initializes and returns a new role object that contains all available security permissions.
        /// </summary>
        /// <returns>A blank <see cref="RoleFullViewModel"/> object that contains full permission list from repository
        /// </returns>
        public async Task<RoleFullViewModel> GetNewRoleAsync()
        {
            var repository = _unitOfWork.GetAsyncRepository<Permission>();
            var all = await repository
                .GetAllAsync(perm => perm.Group);
            var allView = all
                .Select(perm => _mapper.Map<PermissionViewModel>(perm))
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
            var repository = _unitOfWork.GetAsyncRepository<Role>();
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
                    .Select(perm => _mapper.Map<PermissionViewModel>(perm));
                var permissionRepository = _unitOfWork.GetAsyncRepository<Permission>();
                var disabledPermissions = await permissionRepository
                    .GetAllAsync(perm => perm.Group);
                var disabledView = disabledPermissions
                    .Select(perm => _mapper.Map<PermissionViewModel>(perm))
                    .Except(enabledPermissions, new EntityEqualityComparer<PermissionViewModel>())
                    .ToArray();
                Array.ForEach(disabledView, perm => perm.IsEnabled = false);

                role = new RoleFullViewModel()
                {
                    Id = roleId,
                    Role = _mapper.Map<RoleViewModel>(existing)
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
            var repository = _unitOfWork.GetAsyncRepository<Role>();
            var existing = await repository
                .GetEntityQuery()
                .Include(r => r.RolePermissions)
                    .ThenInclude(rp => rp.Permission)
                        .ThenInclude(p => p.Group)
                .Include(r => r.RoleBranches)
                    .ThenInclude(rb => rb.Branch)
                        .ThenInclude(br => br.Company)
                .Include(r => r.UserRoles)
                    .ThenInclude(ur => ur.User)
                        .ThenInclude(usr => usr.Person)
                .Where(r => r.Id == roleId)
                .SingleOrDefaultAsync();
            if (existing != null)
            {
                role = new RoleDetailsViewModel()
                {
                    Role = _mapper.Map<RoleViewModel>(existing)
                };
                Array.ForEach(
                    existing.RolePermissions.Select(rp => rp.Permission).ToArray(),
                    perm => role.Permissions.Add(_mapper.Map<PermissionViewModel>(perm)));
                Array.ForEach(
                    existing.RoleBranches.Select(rb => rb.Branch).ToArray(),
                    br => role.Branches.Add(_mapper.Map<BranchViewModel>(br)));
                Array.ForEach(
                    existing.UserRoles.Select(ur => ur.User).ToArray(),
                    usr => role.Users.Add(_mapper.Map<UserBriefViewModel>(usr)));
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
            var repository = _unitOfWork.GetAsyncRepository<Role>();
            var role = await repository.GetByIDAsync(roleId);
            if (role != null)
            {
                roleBrief = _mapper.Map<RoleViewModel>(role);
            }

            return roleBrief;
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات فراداده ای تعریف شده برای نقش را از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <returns>اطلاعات فراداده ای تعریف شده برای نقش</returns>
        public async Task<EntityViewModel> GetRoleMetadataAsync()
        {
            return await _metadataRepository.GetEntityMetadataAsync<Role>();
        }

        /// <summary>
        /// به روش آسنکرون، تعداد نقش های تعریف شده را از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>تعداد نقش های تعریف شده</returns>
        public async Task<int> GetRoleCountAsync(GridOptions gridOptions = null)
        {
            var repository = _unitOfWork.GetAsyncRepository<Role>();
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
            var repository = _unitOfWork.GetAsyncRepository<Role>();
            if (role.Role.Id == 0)
            {
                outputRole = _mapper.Map<Role>(role.Role);
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

            await _unitOfWork.CommitAsync();
            return _mapper.Map<RoleViewModel>(outputRole);
        }

        /// <summary>
        /// Asynchronously deletes a role specified by unique identifier from repository.
        /// </summary>
        /// <param name="roleId">Unique identifier of the role to delete</param>
        /// <remarks>If no role with specified identifier could be found, no exception would be thrown.</remarks>
        public async Task DeleteRoleAsync(int roleId)
        {
            var repository = _unitOfWork.GetAsyncRepository<Role>();
            var role = await repository.GetByIDWithTrackingAsync(roleId, r => r.RolePermissions, r => r.RoleBranches);
            if (role != null)
            {
                role.RolePermissions.Clear();
                role.RoleBranches.Clear();
                repository.Delete(role);
                await _unitOfWork.CommitAsync();
            }
        }

        /// <summary>
        /// Asynchronously determines if an existing role specified by unique identifier is assigned to users.
        /// </summary>
        /// <param name="roleId">Unique identifier of the role to search for</param>
        /// <returns>true if specified role is assigned; otherwise false. If no role with specified identifier
        /// could be found, returns false.</returns>
        public async Task<bool> IsAssignedRoleAsync(int roleId)
        {
            bool isAssigned = false;
            var repository = _unitOfWork.GetAsyncRepository<Role>();
            var role = await repository
                .GetEntityQuery()
                .Include(r => r.UserRoles)
                .Where(r => r.Id == roleId)
                .SingleOrDefaultAsync();
            if (role != null)
            {
                isAssigned = (role.UserRoles.Count > 0);
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
            var repository = _unitOfWork.GetAsyncRepository<Role>();
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
                    .Select(br => _mapper.Map<RelatedItemViewModel>(br))
                    .ToArray();
                var branchRepository = _unitOfWork.GetAsyncRepository<Branch>();
                var allBranches = await branchRepository
                    .GetAllAsync();
                var disabledBranches = allBranches
                    .Select(br => _mapper.Map<RelatedItemViewModel>(br))
                    .Except(enabledBranches, new EntityEqualityComparer<RelatedItemViewModel>())
                    .ToArray();
                Array.ForEach(enabledBranches, item => item.IsSelected = true);

                roleBranches = _mapper.Map<RelatedItemsViewModel>(existing);
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
            var repository = _unitOfWork.GetAsyncRepository<Role>();
            var existing = await repository.GetByIDWithTrackingAsync(roleBranches.Id, r => r.RoleBranches);
            if (existing != null && AreBranchesModified(existing, roleBranches))
            {
                if (existing.RoleBranches.Count > 0)
                {
                    RemoveInaccessibleBranches(existing, roleBranches);
                }

                AddNewBranches(existing, roleBranches);
                repository.Update(existing);
                await _unitOfWork.CommitAsync();
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
            var repository = _unitOfWork.GetAsyncRepository<Role>();
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
                    .Select(usr => _mapper.Map<RelatedItemViewModel>(usr))
                    .ToArray();
                var userRepository = _unitOfWork.GetAsyncRepository<User>();
                var allUsers = await userRepository
                    .GetAllAsync(usr => usr.Person);
                var disabledUsers = allUsers
                    .Select(usr => _mapper.Map<RelatedItemViewModel>(usr))
                    .Except(enabledUsers, new EntityEqualityComparer<RelatedItemViewModel>())
                    .ToArray();
                Array.ForEach(enabledUsers, item => item.IsSelected = true);

                roleUsers = _mapper.Map<RelatedItemsViewModel>(existing);
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
            var repository = _unitOfWork.GetAsyncRepository<Role>();
            var existing = await repository.GetByIDWithTrackingAsync(roleUsers.Id, r => r.UserRoles);
            if (existing != null && AreUsersModified(existing, roleUsers))
            {
                if (existing.UserRoles.Count > 0)
                {
                    RemoveUnassignedUsers(existing, roleUsers);
                }

                AddNewUsers(existing, roleUsers);
                repository.Update(existing);
                await _unitOfWork.CommitAsync();
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
            var repository = _unitOfWork.GetAsyncRepository<Role>();
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
                    .Select(fp => _mapper.Map<RelatedItemViewModel>(fp))
                    .ToArray();
                var periodRepository = _unitOfWork.GetAsyncRepository<FiscalPeriod>();
                var allPeriods = await periodRepository
                    .GetAllAsync();
                var disabledPeriods = allPeriods
                    .Select(fp => _mapper.Map<RelatedItemViewModel>(fp))
                    .Except(enabledPeriods, new EntityEqualityComparer<RelatedItemViewModel>())
                    .ToArray();
                Array.ForEach(enabledPeriods, item => item.IsSelected = true);

                rolePeriods = _mapper.Map<RelatedItemsViewModel>(existing);
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
            var repository = _unitOfWork.GetAsyncRepository<Role>();
            var existing = await repository.GetByIDWithTrackingAsync(rolePeriods.Id, r => r.RoleFiscalPeriods);
            if (existing != null && AreFiscalPeriodsModified(existing, rolePeriods))
            {
                if (existing.RoleFiscalPeriods.Count > 0)
                {
                    RemoveInaccessibleFiscalPeriods(existing, rolePeriods);
                }

                AddNewFiscalPeriods(existing, rolePeriods);
                repository.Update(existing);
                await _unitOfWork.CommitAsync();
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
            var repository = _unitOfWork.GetAsyncRepository<User>();
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

                userRoles = _mapper.Map<RelatedItemsViewModel>(existing);
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
            var repository = _unitOfWork.GetAsyncRepository<User>();
            var existing = await repository.GetByIDWithTrackingAsync(userRoles.Id, r => r.UserRoles);
            if (existing != null && AreRolesModified(existing, userRoles))
            {
                if (existing.UserRoles.Count > 0)
                {
                    RemoveUnassignedRoles(existing, userRoles);
                }

                AddNewRoles(existing, userRoles);
                repository.Update(existing);
                await _unitOfWork.CommitAsync();
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
            var repository = _unitOfWork.GetAsyncRepository<ViewRowPermission>();
            var settings = await repository
                .GetByCriteriaAsync(perm => perm.Role.Id == roleId, perm => perm.Role, perm => perm.View);
            Array.ForEach(
                settings
                    .Select(perm => _mapper.Map<ViewRowPermissionViewModel>(perm))
                    .ToArray(),
                perm => rowSettings.RowPermissions.Add(perm));

            var viewRepository = _unitOfWork.GetAsyncRepository<Entity>();
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
            var repository = _unitOfWork.GetAsyncRepository<Role>();
            var role = await repository.GetByIDAsync(permissions.Id);
            if (role != null)
            {
                foreach (var permission in permissions.RowPermissions)
                {
                    await SaveViewRolePermissionAsync(permission);
                }

                await _unitOfWork.CommitAsync();
            }
        }

        #endregion

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

        private void UpdateExistingUser(User existing, UserViewModel user)
        {
            var modifiedUser = _mapper.Map<User>(user);
            existing.UserName = user.UserName;
            existing.IsEnabled = user.IsEnabled;
            existing.Person.FirstName = user.PersonFirstName;
            existing.Person.LastName = user.PersonLastName;
            if (!String.IsNullOrEmpty(modifiedUser.PasswordHash))
            {
                existing.PasswordHash = modifiedUser.PasswordHash;
            }
        }

        private User GetNewUser(UserViewModel userViewModel)
        {
            var user = _mapper.Map<User>(userViewModel);
            var person = new Person()
            {
                FirstName = userViewModel.PersonFirstName,
                LastName = userViewModel.PersonLastName
            };

            user.Person = person;
            person.User = user;
            return user;
        }

        private void AddNewPermissions(Role existing, RoleFullViewModel role)
        {
            var repository = _unitOfWork.GetRepository<Permission>();
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
            var repository = _unitOfWork.GetRepository<Branch>();
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
            var repository = _unitOfWork.GetRepository<User>();
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
            var repository = _unitOfWork.GetRepository<FiscalPeriod>();
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
            var repository = _unitOfWork.GetRepository<Role>();
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
            var permission = _mapper.Map<Permission>(perm);
            return new RolePermission()
            {
                Role = role,
                RoleId = role.Id,
                Permission = permission,
                PermissionId = permission.Id
            };
        }

        private IQueryable<User> GetUserPermissionsQuery(int userId)
        {
            var repository = _unitOfWork.GetRepository<User>();
            var query = repository.GetEntityQuery()
                .Where(usr => usr.Id == userId)
                .Include(usr => usr.UserRoles)
                    .ThenInclude(ur => ur.Role)
                        .ThenInclude(r => r.RolePermissions);
            return query;
        }

        private async Task SaveViewRolePermissionAsync(ViewRowPermissionViewModel rowPermission)
        {
            var repository = _unitOfWork.GetAsyncRepository<ViewRowPermission>();
            if (rowPermission.Id == 0)
            {
                if (rowPermission.AccessMode != RowAccessOptions.Default)
                {
                    var newRowPermission = _mapper.Map<ViewRowPermission>(rowPermission);
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

        private IAppUnitOfWork _unitOfWork;
        private IDomainMapper _mapper;
        private IMetadataRepository _metadataRepository;
    }
}
