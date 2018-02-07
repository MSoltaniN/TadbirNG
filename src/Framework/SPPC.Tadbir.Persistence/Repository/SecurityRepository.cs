using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPPC.Framework.Common;
using SPPC.Framework.Helpers;
using SPPC.Framework.Mapper;
using SPPC.Framework.Persistence;
using SPPC.Tadbir.Model;
using SPPC.Tadbir.Model.Auth;
using SPPC.Tadbir.Model.Contact;
using SPPC.Tadbir.Model.Corporate;
using SPPC.Tadbir.ViewModel;
using SPPC.Tadbir.ViewModel.Auth;
using SPPC.Tadbir.ViewModel.Corporate;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// Provides repository operations related to security administration.
    /// </summary>
    public class SecurityRepository : ISecurityRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SecurityRepository"/> class.
        /// </summary>
        /// <param name="unitOfWork">The <see cref="IUnitOfWork"/> implementation to use for all database operations
        /// in this repository.</param>
        /// <param name="mapper">Domain mapper to use for mapping between entitiy and view model classes</param>
        public SecurityRepository(IUnitOfWork unitOfWork, IDomainMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        #region User Management operations

        #region Asynchronous Methods

        /// <summary>
        /// Asynchronously retrieves all application users from repository.
        /// </summary>
        /// <returns>A collection of <see cref="UserViewModel"/> objects retrieved from repository</returns>
        public async Task<IList<UserViewModel>> GetUsersAsync()
        {
            var repository = _unitOfWork.GetAsyncRepository<User>();
            var users = await repository
                .GetAllAsync(u => u.Person);
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
        /// Asynchrnously retrieves all companies accessible by the specified user, as a list of key/value pairs
        /// </summary>
        /// <param name="userId">Database identifier of an existing user</param>
        /// <returns>Collection of all companies accessible by the user</returns>
        public async Task<IList<KeyValue>> GetUserCompaniesAsync(int userId)
        {
            var repository = _unitOfWork.GetAsyncRepository<User>();
            var query = repository
                .GetEntityQuery()
                .Where(usr => usr.Id == userId)
                .Include(usr => usr.UserRoles)
                    .ThenInclude(ur => ur.Role)
                        .ThenInclude(r => r.RoleBranches)
                            .ThenInclude(rb => rb.Branch)
                                .ThenInclude(br => br.Company);
            var user = await query.SingleOrDefaultAsync();
            var companies = new List<KeyValue>();
            if (user != null)
            {
                Array.ForEach(
                    user.UserRoles
                        .Select(ur => ur.Role)
                        .ToArray(),
                    role => companies.AddRange(
                        role.RoleBranches
                            .Select(rb => rb.Branch)
                            .Select(br => br.Company)
                            .Distinct(new EntityEqualityComparer())
                            .Select(c => _mapper.Map<KeyValue>(c))));
            }

            return companies;
        }

        /// <summary>
        /// Asynchronously inserts or updates a single user in repository.
        /// </summary>
        /// <param name="user">Item to insert or update</param>
        public async Task SaveUserAsync(UserViewModel user)
        {
            Verify.ArgumentNotNull(user, "user");
            var repository = _unitOfWork.GetAsyncRepository<User>();
            if (user.Id == 0)
            {
                var newUser = GetNewUser(user);
                repository.Insert(newUser, usr => usr.Person);
            }
            else
            {
                var existing = await repository.GetByIDAsync(user.Id, u => u.Person);
                if (existing != null)
                {
                    UpdateExistingUser(existing, user);
                    repository.Update(existing, usr => usr.Person);
                }
            }

            await _unitOfWork.CommitAsync();
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

        #region Synchronous Methods (May be removed in the future)

        /// <summary>
        /// Retrieves all application users from repository.
        /// </summary>
        /// <returns>A collection of <see cref="UserViewModel"/> objects retrieved from repository</returns>
        public IList<UserViewModel> GetUsers()
        {
            var repository = _unitOfWork.GetRepository<User>();
            var users = repository
                .GetAll(u => u.Person)
                .Select(user => _mapper.Map<UserViewModel>(user))
                .ToList();
            return users;
        }

        /// <summary>
        /// Retrieves a single user specified by user name from repository.
        /// </summary>
        /// <param name="userName">User name to search for</param>
        /// <returns>A <see cref="UserViewModel"/> instance that corresponds to the specified user name, if there is
        /// such a user defined; otherwise, returns null.</returns>
        public UserViewModel GetUser(string userName)
        {
            UserViewModel userViewModel = null;
            var repository = _unitOfWork.GetRepository<User>();
            var user = repository
                .GetByCriteria(usr => usr.UserName == userName, usr => usr.Person)
                .FirstOrDefault();
            if (user != null)
            {
                userViewModel = _mapper.Map<UserViewModel>(user);
            }

            return userViewModel;
        }

        /// <summary>
        /// Retrieves context information for a user specified by unique identifier from repository.
        /// </summary>
        /// <param name="userId">Unique identifier of the user to search for</param>
        /// <returns>A <see cref="UserContextViewModel"/> instance containing context information, if there is
        /// such a user defined; otherwise, returns null.</returns>
        public UserContextViewModel GetUserContext(int userId)
        {
            UserContextViewModel userContext = null;
            var repository = _unitOfWork.GetRepository<User>();
            var user = repository.GetByID(userId, usr => usr.Person, usr => usr.UserRoles);
            if (user != null)
            {
                var permissions = new List<PermissionBriefViewModel>();
                var branches = new List<int>();
                var roleRepository = _unitOfWork.GetRepository<Role>();
                foreach (var roleId in user.UserRoles.Select(ur => ur.RoleId))
                {
                    var role = roleRepository.GetByID(roleId, r => r.RoleBranches, r => r.RolePermissions);
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
        /// Retrieves a single user specified by unique identifier from repository.
        /// </summary>
        /// <param name="userId">Unique identifier of the user to search for</param>
        /// <returns>A <see cref="UserViewModel"/> instance that corresponds to the specified identifier, if there is
        /// such a user defined; otherwise, returns null.</returns>
        public UserViewModel GetUser(int userId)
        {
            UserViewModel userViewModel = null;
            var repository = _unitOfWork.GetRepository<User>();
            var user = repository.GetByID(userId, usr => usr.Person);
            if (user != null)
            {
                userViewModel = _mapper.Map<UserViewModel>(user);
            }

            return userViewModel;
        }

        /// <summary>
        /// Inserts or updates a single user in repository.
        /// </summary>
        /// <param name="user">Item to insert or update</param>
        public void SaveUser(UserViewModel user)
        {
            Verify.ArgumentNotNull(user, "user");
            var repository = _unitOfWork.GetRepository<User>();
            if (user.Id == 0)
            {
                var newUser = GetNewUser(user);
                repository.Insert(newUser, usr => usr.Person);
            }
            else
            {
                var existing = repository.GetByID(user.Id, u => u.Person);
                if (existing != null)
                {
                    UpdateExistingUser(existing, user);
                    repository.Update(existing, usr => usr.Person);
                }
            }

            _unitOfWork.Commit();
        }

        /// <summary>
        /// Sets LastLoginDate field of the specified user to current system date/time.
        /// </summary>
        /// <param name="userId">Unique identifier of an existing user</param>
        public void UpdateUserLastLogin(int userId)
        {
            var repository = _unitOfWork.GetRepository<User>();
            var user = repository.GetByID(userId);
            if (user != null)
            {
                user.LastLoginDate = DateTime.Now;
                repository.Update(user);
                _unitOfWork.Commit();
            }
        }

        /// <summary>
        /// Updates a user profile in repository.
        /// </summary>
        /// <param name="profile">User profile to update</param>
        public void UpdateUserPassword(UserProfileViewModel profile)
        {
            Verify.ArgumentNotNull(profile, "profile");
            var repository = _unitOfWork.GetRepository<User>();
            var user = repository
                .GetByCriteria(usr => usr.UserName == profile.UserName)
                .FirstOrDefault();
            if (user != null)
            {
                user.PasswordHash = profile.NewPassword;
                repository.Update(user);
                _unitOfWork.Commit();
            }
        }

        /// <summary>
        /// Determines if the specified <see cref="UserViewModel"/> instance has a user name that is already used
        /// by a different user.
        /// </summary>
        /// <param name="user">User item to check for duplicate user name</param>
        /// <returns>True if the user name is already used; otherwise returns false.</returns>
        public bool IsDuplicateUser(UserViewModel user)
        {
            Verify.ArgumentNotNull(user, "user");
            var repository = _unitOfWork.GetRepository<User>();
            var existing = repository
                .GetByCriteria(usr => usr.Id != user.Id
                    && usr.UserName == user.UserName)
                .FirstOrDefault();
            return (existing != null);
        }

        #endregion

        #endregion

        #region Role Management operations

        #region Asynchronous Methods

        /// <summary>
        /// Asynchronously retrieves all application roles from repository.
        /// </summary>
        /// <returns>A collection of <see cref="RoleViewModel"/> objects retrieved from repository</returns>
        public async Task<IList<RoleViewModel>> GetRolesAsync()
        {
            var repository = _unitOfWork.GetAsyncRepository<Role>();
            var roles = await repository
                .GetEntityQuery()
                .Include(r => r.RolePermissions)
                    .ThenInclude(rp => rp.Permission)
                .ToListAsync();
            return roles
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
        /// Asynchronously inserts or updates a single security role, including all permissions in it, in repository
        /// </summary>
        /// <param name="role">Role to insert or update</param>
        public async Task SaveRoleAsync(RoleFullViewModel role)
        {
            Verify.ArgumentNotNull(role, "role");
            Verify.ArgumentNotNull(role.Role, "role.Role");
            var repository = _unitOfWork.GetAsyncRepository<Role>();
            if (role.Role.Id == 0)
            {
                var newRole = _mapper.Map<Role>(role.Role);
                AddRolePermissions(newRole, role);
                repository.Insert(newRole, r => r.RolePermissions);
            }
            else
            {
                var existing = await repository.GetByIDWithTrackingAsync(role.Role.Id, r => r.RolePermissions);
                if (existing != null)
                {
                    if (ArePermissionsModified(existing, role))
                    {
                        if (existing.RolePermissions.Count > 0)
                        {
                            RemoveDisabledPermissions(existing, role);
                        }

                        AddNewPermissions(existing, role);
                    }

                    UpdateExistingRole(existing, role);
                    repository.UpdateWithTracking(existing);
                }
            }

            await _unitOfWork.CommitAsync();
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

        #endregion

        #region Synchronous Methods (May be removed in the future)

        /// <summary>
        /// Retrieves all application roles from repository.
        /// </summary>
        /// <returns>A collection of <see cref="RoleViewModel"/> objects retrieved from repository</returns>
        public IList<RoleViewModel> GetRoles()
        {
            var repository = _unitOfWork.GetRepository<Role>();
            var roles = repository
                .GetEntityQuery()
                .Include(r => r.RolePermissions)
                    .ThenInclude(rp => rp.Permission)
                .ToList();
            return roles
                .Select(r => _mapper.Map<RoleViewModel>(r))
                .ToList();
        }

        /// <summary>
        /// Initializes and returns a new role object that contains all available security permissions.
        /// </summary>
        /// <returns>A blank <see cref="RoleFullViewModel"/> object that contains full permission list from repository
        /// </returns>
        public RoleFullViewModel GetNewRole()
        {
            var repository = _unitOfWork.GetRepository<Permission>();
            var all = repository
                .GetAll(perm => perm.Group)
                .Select(perm => _mapper.Map<PermissionViewModel>(perm))
                .ToArray();
            var role = new RoleFullViewModel();
            Array.ForEach(all, perm =>
            {
                perm.IsEnabled = false;
                role.Permissions.Add(perm);
            });
            return role;
        }

        /// <summary>
        /// Retrieves a single role with permissions (specified by role identifier) from repository.
        /// </summary>
        /// <param name="roleId">Unique identifier of the role to search for</param>
        /// <returns>A <see cref="RoleFullViewModel"/> instance that corresponds to the specified identifier, if there is
        /// such a role defined; otherwise, returns null.</returns>
        public RoleFullViewModel GetRole(int roleId)
        {
            RoleFullViewModel role = null;
            var repository = _unitOfWork.GetRepository<Role>();
            var existing = repository
                .GetEntityQuery()
                .Include(r => r.RolePermissions)
                    .ThenInclude(rp => rp.Permission)
                        .ThenInclude(p => p.Group)
                .Where(r => r.Id == roleId)
                .SingleOrDefault();
            if (existing != null)
            {
                var enabledPermissions = existing.RolePermissions
                    .Select(rp => rp.Permission)
                    .Select(perm => _mapper.Map<PermissionViewModel>(perm));
                var permissionRepository = _unitOfWork.GetRepository<Permission>();
                var disabledPermissions = permissionRepository
                    .GetAll(perm => perm.Group)
                    .Select(perm => _mapper.Map<PermissionViewModel>(perm))
                    .Except(enabledPermissions, new EntityEqualityComparer<PermissionViewModel>())
                    .ToArray();
                Array.ForEach(disabledPermissions, perm => perm.IsEnabled = false);

                role = new RoleFullViewModel()
                {
                    Role = _mapper.Map<RoleViewModel>(existing)
                };
                Array.ForEach(enabledPermissions
                    .Concat(disabledPermissions)
                    .OrderBy(perm => perm.Id)
                    .ToArray(), perm => role.Permissions.Add(perm));
            }

            return role;
        }

        /// <summary>
        /// Retrieves a single role with full details (specified by role identifier) from repository.
        /// </summary>
        /// <param name="roleId">Unique identifier of the role to search for</param>
        /// <returns>A <see cref="RoleDetailsViewModel"/> instance that corresponds to the specified identifier, if there is
        /// such a role defined; otherwise, returns null.</returns>
        public RoleDetailsViewModel GetRoleDetails(int roleId)
        {
            RoleDetailsViewModel role = null;
            var repository = _unitOfWork.GetRepository<Role>();
            var existing = repository
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
                .SingleOrDefault();
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
        /// Retrieves brief information for a single role specified by unique identifier from repository.
        /// </summary>
        /// <param name="roleId">Unique identifier of the role to search for</param>
        /// <returns>A <see cref="RoleViewModel"/> instance that corresponds to the specified role identifier,
        /// if there is such a role defined; otherwise, returns null.</returns>
        public RoleViewModel GetRoleBrief(int roleId)
        {
            RoleViewModel roleBrief = null;
            var repository = _unitOfWork.GetRepository<Role>();
            var role = repository.GetByID(roleId);
            if (role != null)
            {
                roleBrief = _mapper.Map<RoleViewModel>(role);
            }

            return roleBrief;
        }

        /// <summary>
        /// Inserts or updates a single security role, including all permissions in it, in repository
        /// </summary>
        /// <param name="role">Role to insert or update</param>
        public void SaveRole(RoleFullViewModel role)
        {
            Verify.ArgumentNotNull(role, "role");
            Verify.ArgumentNotNull(role.Role, "role.Role");
            var repository = _unitOfWork.GetRepository<Role>();
            if (role.Role.Id == 0)
            {
                var newRole = _mapper.Map<Role>(role.Role);
                AddRolePermissions(newRole, role);
                repository.Insert(newRole, r => r.RolePermissions);
            }
            else
            {
                var existing = repository.GetByIDWithTracking(role.Role.Id, r => r.RolePermissions);
                if (existing != null)
                {
                    if (ArePermissionsModified(existing, role))
                    {
                        if (existing.RolePermissions.Count > 0)
                        {
                            RemoveDisabledPermissions(existing, role);
                        }

                        AddNewPermissions(existing, role);
                    }

                    UpdateExistingRole(existing, role);
                    repository.UpdateWithTracking(existing);
                }
            }

            _unitOfWork.Commit();
        }

        /// <summary>
        /// Deletes a role specified by unique identifier from repository.
        /// </summary>
        /// <param name="roleId">Unique identifier of the role to delete</param>
        /// <remarks>If no role with specified identifier could be found, no exception would be thrown.</remarks>
        public void DeleteRole(int roleId)
        {
            var repository = _unitOfWork.GetRepository<Role>();
            var role = repository.GetByIDWithTracking(roleId, r => r.RolePermissions, r => r.RoleBranches);
            if (role != null)
            {
                role.RolePermissions.Clear();
                role.RoleBranches.Clear();
                repository.Delete(role);
                _unitOfWork.Commit();
            }
        }

        /// <summary>
        /// Determines if an existing role specified by unique identifier is assigned to users.
        /// </summary>
        /// <param name="roleId">Unique identifier of the role to search for</param>
        /// <returns>true if specified role is assigned; otherwise false. If no role with specified identifier
        /// could be found, returns false.</returns>
        public bool IsAssignedRole(int roleId)
        {
            bool isAssigned = false;
            var repository = _unitOfWork.GetRepository<Role>();
            var role = repository
                .GetEntityQuery()
                .Include(r => r.UserRoles)
                .Where(r => r.Id == roleId)
                .SingleOrDefault();
            if (role != null)
            {
                isAssigned = (role.UserRoles.Count > 0);
            }

            return isAssigned;
        }

        /// <summary>
        /// Retrieves branch associations for a role specified by identifier.
        /// </summary>
        /// <param name="roleId">Unique identifier of an existing role</param>
        /// <returns>An object that contains information about all branches accessible by specified role</returns>
        public RoleBranchesViewModel GetRoleBranches(int roleId)
        {
            RoleBranchesViewModel role = null;
            var repository = _unitOfWork.GetRepository<Role>();
            var existing = repository
                .GetEntityQuery()
                .Include(r => r.RoleBranches)
                    .ThenInclude(rb => rb.Branch)
                        .ThenInclude(br => br.Company)
                .Where(r => r.Id == roleId)
                .SingleOrDefault();
            if (existing != null)
            {
                var enabledBranches = existing.RoleBranches
                    .Select(rb => rb.Branch)
                    .Select(br => _mapper.Map<BranchViewModel>(br));
                var branchRepository = _unitOfWork.GetRepository<Branch>();
                var allBranches = branchRepository
                    .GetEntityQuery()
                    .Include(br => br.Company)
                    .ToList();
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
        /// Updates branch associations for a role specified by identifier.
        /// </summary>
        /// <param name="role">A <see cref="RoleBranchesViewModel"/> object that contains information about all branch
        /// associations to the specified role</param>
        public void SaveRoleBranches(RoleBranchesViewModel role)
        {
            Verify.ArgumentNotNull(role, "role");
            var repository = _unitOfWork.GetRepository<Role>();
            var existing = repository.GetByIDWithTracking(role.Id, r => r.RoleBranches);
            if (existing != null && AreBranchesModified(existing, role))
            {
                if (existing.RoleBranches.Count > 0)
                {
                    RemoveInaccessibleBranches(existing, role);
                }

                AddNewBranches(existing, role);
                repository.Update(existing);
                _unitOfWork.Commit();
            }
        }

        /// <summary>
        /// Retrieves user associations for a role specified by identifier.
        /// </summary>
        /// <param name="roleId">Unique identifier of an existing role</param>
        /// <returns>An object that contains information about all users assigned to specified role</returns>
        public RoleUsersViewModel GetRoleUsers(int roleId)
        {
            RoleUsersViewModel role = null;
            var repository = _unitOfWork.GetRepository<Role>();
            var existing = repository
                .GetEntityQuery()
                .Include(r => r.UserRoles)
                    .ThenInclude(ur => ur.User)
                        .ThenInclude(usr => usr.Person)
                .Where(r => r.Id == roleId)
                .SingleOrDefault();
            if (existing != null)
            {
                var enabledUsers = existing.UserRoles
                    .Select(ur => ur.User)
                    .Select(usr => _mapper.Map<UserBriefViewModel>(usr));
                var userRepository = _unitOfWork.GetRepository<User>();
                var allUsers = userRepository
                    .GetEntityQuery()
                    .Include(usr => usr.Person)
                    .ToList();
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
        /// Updates user associations for a role specified by identifier.
        /// </summary>
        /// <param name="role">A <see cref="RoleUsersViewModel"/> object that contains information about all user
        /// associations to the specified role</param>
        public void SaveRoleUsers(RoleUsersViewModel role)
        {
            Verify.ArgumentNotNull(role, "role");
            var repository = _unitOfWork.GetRepository<Role>();
            var existing = repository.GetByIDWithTracking(role.Id, r => r.UserRoles);
            if (existing != null && AreUsersModified(existing, role))
            {
                if (existing.UserRoles.Count > 0)
                {
                    RemoveUnassignedUsers(existing, role);
                }

                AddNewUsers(existing, role);
                repository.Update(existing);
                _unitOfWork.Commit();
            }
        }

        #endregion

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

        private IUnitOfWork _unitOfWork;
        private IDomainMapper _mapper;
    }
}
