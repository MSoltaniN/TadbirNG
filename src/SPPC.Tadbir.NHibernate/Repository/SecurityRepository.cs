using System;
using System.Collections.Generic;
using System.Linq;
using SPPC.Framework.Mapper;
using SPPC.Tadbir.Model.Auth;
using SPPC.Tadbir.Model.Contact;
using SPPC.Tadbir.Model.Corporate;
using SPPC.Tadbir.ViewModel;
using SPPC.Tadbir.ViewModel.Auth;
using SPPC.Tadbir.ViewModel.Corporate;
using SwForAll.Platform.Common;
using SwForAll.Platform.Persistence;

namespace SPPC.Tadbir.NHibernate
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

        /// <summary>
        /// Retrieves all application users from repository.
        /// </summary>
        /// <returns>A collection of <see cref="UserViewModel"/> objects retrieved from repository</returns>
        public IList<UserViewModel> GetUsers()
        {
            var repository = _unitOfWork.GetRepository<User>();
            var users = repository
                .GetAll()
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
                .GetByCriteria(usr => usr.UserName == userName)
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
            var user = repository.GetByID(userId);
            if (user != null)
            {
                userContext = _mapper.Map<UserContextViewModel>(user);
                var branches = new List<int>();
                Array.ForEach(user.Roles.ToArray(), role => branches.AddRange(role.Branches.Select(br => br.Id)));
                Array.ForEach(branches.Distinct().ToArray(), br => userContext.Branches.Add(br));
                Array.ForEach(user.Roles.Select(role => role.Id).ToArray(), roleId => userContext.Roles.Add(roleId));

                var permissions = new List<PermissionBriefViewModel>();
                Array.ForEach(
                    user.Roles.ToArray(),
                    role => permissions.AddRange(
                        role.Permissions.Select(perm => _mapper.Map<PermissionBriefViewModel>(perm))));
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
            var user = repository.GetByID(userId);
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
                repository.Insert(newUser);
            }
            else
            {
                var existing = repository.GetByID(user.Id);
                if (existing != null)
                {
                    UpdateExistingUser(existing, user);
                    repository.Update(existing);
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

        #region Role Management operations

        /// <summary>
        /// Retrieves all application roles from repository.
        /// </summary>
        /// <returns>A collection of <see cref="RoleViewModel"/> objects retrieved from repository</returns>
        public IList<RoleViewModel> GetRoles()
        {
            var repository = _unitOfWork.GetRepository<Role>();
            var roles = repository
                .GetAll()
                .Select(user => _mapper.Map<RoleViewModel>(user))
                .ToList();
            return roles;
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
                .GetAll()
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
            var existing = repository.GetByID(roleId);
            if (existing != null)
            {
                var enabledPermissions = existing.Permissions
                    .Select(perm => _mapper.Map<PermissionViewModel>(perm));
                var permissionRepository = _unitOfWork.GetRepository<Permission>();
                var disabledPermissions = permissionRepository
                    .GetAll()
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
            var existing = repository.GetByID(roleId);
            if (existing != null)
            {
                role = new RoleDetailsViewModel()
                {
                    Role = _mapper.Map<RoleViewModel>(existing)
                };
                Array.ForEach(
                    existing.Permissions.ToArray(),
                    perm => role.Permissions.Add(_mapper.Map<PermissionViewModel>(perm)));
                Array.ForEach(
                    existing.Branches.ToArray(),
                    br => role.Branches.Add(_mapper.Map<BranchViewModel>(br)));
                Array.ForEach(
                    existing.Users.ToArray(),
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
                repository.Insert(newRole);
            }
            else
            {
                var existing = repository.GetByID(role.Role.Id);
                if (existing != null)
                {
                    if (ArePermissionsModified(existing, role))
                    {
                        if (existing.Permissions.Count > 0)
                        {
                            RemoveDisabledPermissions(existing, role);
                        }

                        AddNewPermissions(existing, role);
                    }

                    UpdateExistingRole(existing, role);
                    repository.Update(existing);
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
            var role = repository.GetByID(roleId);
            if (role != null)
            {
                role.Permissions.Clear();
                repository.Update(role);
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
            var role = repository.GetByID(roleId);
            if (role != null)
            {
                isAssigned = (role.Users.Count > 0);
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
            var existing = repository.GetByID(roleId);
            if (existing != null)
            {
                var enabledBranches = existing.Branches
                    .Select(br => _mapper.Map<BranchViewModel>(br));
                var branchRepository = _unitOfWork.GetRepository<Branch>();
                var disabledBranches = branchRepository
                    .GetAll()
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
            var existing = repository.GetByID(role.Id);
            if (existing != null && AreBranchesModified(existing, role))
            {
                if (existing.Branches.Count > 0)
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
            var existing = repository.GetByID(roleId);
            if (existing != null)
            {
                var enabledUsers = existing.Users
                    .Select(usr => _mapper.Map<UserBriefViewModel>(usr));
                var userRepository = _unitOfWork.GetRepository<User>();
                var disabledUsers = userRepository
                    .GetAll()
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
            var existing = repository.GetByID(role.Id);
            if (existing != null && AreUsersModified(existing, role))
            {
                if (existing.Users.Count > 0)
                {
                    RemoveUnassignedUsers(existing, role);
                }

                AddNewUsers(existing, role);
                repository.Update(existing);
                _unitOfWork.Commit();
            }
        }

        #endregion

        private static void RemoveDisabledPermissions(Role existing, RoleFullViewModel role)
        {
            var currentItems = role.Permissions
                .Where(perm => perm.IsEnabled)
                .Select(perm => perm.Id);
            var removedItems = existing.Permissions
                .Select(perm => perm.Id)
                .Where(id => !currentItems.Contains(id))
                .ToArray();
            foreach (int id in removedItems)
            {
                existing.Permissions.Remove(existing.Permissions
                    .Where(perm => perm.Id == id)
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
            var existingItems = existing.Permissions
                .Select(perm => perm.Id)
                .ToArray();
            var enabledItems = role.Permissions
                .Where(perm => perm.IsEnabled)
                .Select(perm => perm.Id)
                .ToArray();
            return (!AreEqual(existingItems, enabledItems));
        }

        private static bool AreBranchesModified(Role existing, RoleBranchesViewModel role)
        {
            var existingItems = existing.Branches
                .Select(br => br.Id)
                .ToArray();
            var enabledItems = role.Branches
                .Where(br => br.IsAccessible)
                .Select(br => br.Id)
                .ToArray();
            return (!AreEqual(existingItems, enabledItems));
        }

        private static bool AreUsersModified(Role existing, RoleUsersViewModel role)
        {
            var existingItems = existing.Users
                .Select(usr => usr.Id)
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
            var removedItems = existing.Branches
                .Select(br => br.Id)
                .Where(id => !currentItems.Contains(id))
                .ToArray();
            foreach (int id in removedItems)
            {
                existing.Branches.Remove(existing.Branches
                    .Where(br => br.Id == id)
                    .Single());
            }
        }

        private static void RemoveUnassignedUsers(Role existing, RoleUsersViewModel role)
        {
            var currentItems = role.Users
                .Where(usr => usr.HasRole)
                .Select(usr => usr.Id);
            var removedItems = existing.Users
                .Select(usr => usr.Id)
                .Where(id => !currentItems.Contains(id))
                .ToArray();
            foreach (int id in removedItems)
            {
                existing.Users.Remove(existing.Users
                    .Where(usr => usr.Id == id)
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
            var currentItems = existing.Permissions.Select(perm => perm.Id);
            var newItems = role.Permissions
                .Where(perm => perm.IsEnabled
                    && !currentItems.Contains(perm.Id));
            foreach (var item in newItems)
            {
                existing.Permissions.Add(_mapper.Map<Permission>(item));
            }
        }

        private void AddNewBranches(Role existing, RoleBranchesViewModel role)
        {
            var currentItems = existing.Branches.Select(br => br.Id);
            var newItems = role.Branches
                .Where(br => br.IsAccessible
                    && !currentItems.Contains(br.Id));
            foreach (var item in newItems)
            {
                existing.Branches.Add(_mapper.Map<Branch>(item));
            }
        }

        private void AddNewUsers(Role existing, RoleUsersViewModel role)
        {
            var currentItems = existing.Users.Select(usr => usr.Id);
            var newItems = role.Users
                .Where(usr => usr.HasRole
                    && !currentItems.Contains(usr.Id));
            foreach (var item in newItems)
            {
                existing.Users.Add(_mapper.Map<User>(item));
            }
        }

        private void AddRolePermissions(Role role, RoleFullViewModel roleViewModel)
        {
            Array.ForEach(roleViewModel.Permissions
                .Where(perm => perm.IsEnabled)
                .ToArray(), perm => role.Permissions.Add(_mapper.Map<Permission>(perm)));
        }

        private IUnitOfWork _unitOfWork;
        private IDomainMapper _mapper;
    }
}
