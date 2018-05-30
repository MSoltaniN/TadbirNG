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
using SPPC.Tadbir.Model.Contact;
using SPPC.Tadbir.Model.Corporate;
using SPPC.Tadbir.Model.Finance;
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
        /// <param name="decorator">امکان ضمیمه کردن متادیتا به اطلاعات خوانده شده را فراهم می کند</param>
        public SecurityRepository(IUnitOfWork unitOfWork, IDomainMapper mapper, IMetadataDecorator decorator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _decorator = decorator;
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
        public async Task<EntityItemViewModel<UserViewModel>> GetUserMetadataAsync()
        {
            return await _decorator.GetDecoratedItemAsync<User, UserViewModel>(null);
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
            var topCommands = await _decorator.Repository.GetTopLevelCommandsAsync();
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
                .GetEntityQuery(gridOptions)
                .Include(r => r.RolePermissions)
                    .ThenInclude(rp => rp.Permission)
                .ToAsyncEnumerable();
            if (gridOptions != null)
            {
                query = query
                    .Skip((gridOptions.Paging.PageIndex - 1) * gridOptions.Paging.PageSize)
                    .Take(gridOptions.Paging.PageSize);
            }

            return await query
                .Select(r => _mapper.Map<RoleViewModel>(r))
                .ToList();
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
        public async Task<EntityItemViewModel<RoleViewModel>> GetRoleMetadataAsync()
        {
            return await _decorator.GetDecoratedItemAsync<Role, RoleViewModel>(null);
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
        /// Asynchronously retrieves branch associations for a role specified by identifier.
        /// </summary>
        /// <param name="roleId">Unique identifier of an existing role</param>
        /// <returns>An object that contains information about all branches accessible by specified role</returns>
        public async Task<RoleBranchesViewModel> GetRoleBranchesAsync(int roleId)
        {
            RoleBranchesViewModel role = null;
            var repository = _unitOfWork.GetAsyncRepository<Role>();
            var existing = await repository
                .GetEntityQuery()
                .Include(r => r.RoleBranches)
                    .ThenInclude(rb => rb.Branch)
                        .ThenInclude(br => br.Company)
                .Where(r => r.Id == roleId)
                .SingleOrDefaultAsync();
            if (existing != null)
            {
                var enabledBranches = existing.RoleBranches
                    .Select(rb => rb.Branch)
                    .Select(br => _mapper.Map<BranchViewModel>(br));
                var branchRepository = _unitOfWork.GetAsyncRepository<Branch>();
                var allBranches = await branchRepository
                    .GetEntityQuery()
                    .Include(br => br.Company)
                    .ToListAsync();
                var disabledBranches = allBranches
                    .Select(br => _mapper.Map<BranchViewModel>(br))
                    .Except(enabledBranches, new EntityEqualityComparer<BranchViewModel>())
                    .ToArray();
                Array.ForEach(disabledBranches, br => br.IsAccessible = false);

                role = _mapper.Map<RoleBranchesViewModel>(existing);
                Array.ForEach(enabledBranches
                    .Concat(disabledBranches)
                    .OrderBy(br => br.Id)
                    .ToArray(), br => role.Branches.Add(br));
            }

            return role;
        }

        /// <summary>
        /// Asynchronously updates branch associations for a role specified by identifier.
        /// </summary>
        /// <param name="role">A <see cref="RoleBranchesViewModel"/> object that contains information about all branch
        /// associations to the specified role</param>
        public async Task SaveRoleBranchesAsync(RoleBranchesViewModel role)
        {
            Verify.ArgumentNotNull(role, "role");
            var repository = _unitOfWork.GetAsyncRepository<Role>();
            var existing = await repository.GetByIDWithTrackingAsync(role.Id, r => r.RoleBranches);
            if (existing != null && AreBranchesModified(existing, role))
            {
                if (existing.RoleBranches.Count > 0)
                {
                    RemoveInaccessibleBranches(existing, role);
                }

                AddNewBranches(existing, role);
                repository.Update(existing);
                await _unitOfWork.CommitAsync();
            }
        }

        /// <summary>
        /// Asynchronously retrieves user associations for a role specified by identifier.
        /// </summary>
        /// <param name="roleId">Unique identifier of an existing role</param>
        /// <returns>An object that contains information about all users assigned to specified role</returns>
        public async Task<RoleUsersViewModel> GetRoleUsersAsync(int roleId)
        {
            RoleUsersViewModel role = null;
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
                    .Select(usr => _mapper.Map<UserBriefViewModel>(usr));
                var userRepository = _unitOfWork.GetRepository<User>();
                var allUsers = await userRepository
                    .GetEntityQuery()
                    .Include(usr => usr.Person)
                    .ToListAsync();
                var disabledUsers = allUsers
                    .Select(usr => _mapper.Map<UserBriefViewModel>(usr))
                    .Except(enabledUsers, new EntityEqualityComparer<UserBriefViewModel>())
                    .ToArray();
                Array.ForEach(disabledUsers, usr => usr.HasRole = false);

                role = _mapper.Map<RoleUsersViewModel>(existing);
                Array.ForEach(enabledUsers
                    .Concat(disabledUsers)
                    .OrderBy(usr => usr.Id)
                    .ToArray(), usr => role.Users.Add(usr));
            }

            return role;
        }

        /// <summary>
        /// Asynchronously updates user associations for a role specified by identifier.
        /// </summary>
        /// <param name="role">A <see cref="RoleUsersViewModel"/> object that contains information about all user
        /// associations to the specified role</param>
        public async Task SaveRoleUsersAsync(RoleUsersViewModel role)
        {
            Verify.ArgumentNotNull(role, "role");
            var repository = _unitOfWork.GetAsyncRepository<Role>();
            var existing = await repository.GetByIDWithTrackingAsync(role.Id, r => r.UserRoles);
            if (existing != null && AreUsersModified(existing, role))
            {
                if (existing.UserRoles.Count > 0)
                {
                    RemoveUnassignedUsers(existing, role);
                }

                AddNewUsers(existing, role);
                repository.Update(existing);
                await _unitOfWork.CommitAsync();
            }
        }

        /// <summary>
        /// به روش آسنکرون، دوره های مالی قابل دسترسی توسط یک نقش را خوانده و برمی گرداند
        /// </summary>
        /// <param name="roleId">شناسه یکی از نقش های موجود</param>
        /// <returns>اطلاعات نمایشی دوره های مالی قابل دسترسی</returns>
        public async Task<RoleItemsViewModel> GetRoleFiscalPeriodsAsync(int roleId)
        {
            RoleItemsViewModel role = null;
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

                role = _mapper.Map<RoleItemsViewModel>(existing);
                Array.ForEach(enabledPeriods
                    .Concat(disabledPeriods)
                    .OrderBy(item => item.Id)
                    .ToArray(), item => role.RelatedItems.Add(item));
            }

            return role;
        }

        /// <summary>
        /// به روش آسنکرون، آخرین وضعیت دوره های مالی قابل دسترسی توسط یک نقش را ذخیره می کند
        /// </summary>
        /// <param name="role">اطلاعات نمایشی دوره های مالی قابل دسترسی</param>
        public async Task SaveRoleFiscalPeriodsAsync(RoleItemsViewModel role)
        {
            Verify.ArgumentNotNull(role, "role");
            var repository = _unitOfWork.GetAsyncRepository<Role>();
            var existing = await repository.GetByIDWithTrackingAsync(role.Id, r => r.RoleFiscalPeriods);
            if (existing != null && AreFiscalPeriodsModified(existing, role))
            {
                if (existing.RoleFiscalPeriods.Count > 0)
                {
                    RemoveInaccessibleFiscalPeriods(existing, role);
                }

                AddNewFiscalPeriods(existing, role);
                repository.Update(existing);
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

        private static bool AreBranchesModified(Role existing, RoleBranchesViewModel role)
        {
            var existingItems = existing.RoleBranches
                .Select(rb => rb.BranchId)
                .ToArray();
            var enabledItems = role.Branches
                .Where(br => br.IsAccessible)
                .Select(br => br.Id)
                .ToArray();
            return (!AreEqual(existingItems, enabledItems));
        }

        private static bool AreUsersModified(Role existing, RoleUsersViewModel role)
        {
            var existingItems = existing.UserRoles
                .Select(ur => ur.UserId)
                .ToArray();
            var enabledItems = role.Users
                .Where(usr => usr.HasRole)
                .Select(usr => usr.Id)
                .ToArray();
            return (!AreEqual(existingItems, enabledItems));
        }

        private static bool AreFiscalPeriodsModified(Role existing, RoleItemsViewModel roleItems)
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

        private static void RemoveInaccessibleBranches(Role existing, RoleBranchesViewModel role)
        {
            var currentItems = role.Branches
                .Where(br => br.IsAccessible)
                .Select(br => br.Id);
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

        private static void RemoveUnassignedUsers(Role existing, RoleUsersViewModel role)
        {
            var currentItems = role.Users
                .Where(usr => usr.HasRole)
                .Select(usr => usr.Id);
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

        private static void RemoveInaccessibleFiscalPeriods(Role existing, RoleItemsViewModel roleItems)
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

        private static bool AreEqual(IEnumerable<int> left, IEnumerable<int> right)
        {
            return left.Count() == right.Count()
                && left.All(value => right.Contains(value));
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

        private void AddNewBranches(Role existing, RoleBranchesViewModel role)
        {
            var repository = _unitOfWork.GetRepository<Branch>();
            var currentItems = existing.RoleBranches.Select(rb => rb.BranchId);
            var newItems = role.Branches
                .Where(br => br.IsAccessible
                    && !currentItems.Contains(br.Id));
            foreach (var item in newItems)
            {
                var branch = repository.GetByIDWithTracking(item.Id, br => br.Company);
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

        private void AddNewUsers(Role existing, RoleUsersViewModel role)
        {
            var repository = _unitOfWork.GetRepository<User>();
            var currentItems = existing.UserRoles.Select(ur => ur.UserId);
            var newItems = role.Users
                .Where(usr => usr.HasRole
                    && !currentItems.Contains(usr.Id));
            foreach (var item in newItems)
            {
                var user = repository.GetByIDWithTracking(item.Id, usr => usr.Person);
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

        private void AddNewFiscalPeriods(Role existing, RoleItemsViewModel roleItems)
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

        private IUnitOfWork _unitOfWork;
        private IDomainMapper _mapper;
        private IMetadataDecorator _decorator;
    }
}
