using System;
using System.Collections.Generic;
using System.Linq;
using SPPC.Framework.Mapper;
using SPPC.Tadbir.Model.Auth;
using SPPC.Tadbir.Model.Contact;
using SPPC.Tadbir.ViewModel.Auth;
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
            var existing = repository.GetByID(user.Id);
            if (existing == null)
            {
                var newUser = GetNewUser(user);
                repository.Insert(newUser);
            }
            else
            {
                UpdateExistingUser(existing, user);
                repository.Update(existing);
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
            Array.ForEach(all, perm => perm.IsEnabled = false);
            var role = new RoleFullViewModel() { Permissions = new List<PermissionViewModel>(all) };
            return role;
        }

        /// <summary>
        /// Inserts or updates a single security role, including all permissions in it, in repository
        /// </summary>
        /// <param name="role">Role to insert or update</param>
        public void SaveRole(RoleFullViewModel role)
        {
            var repository = _unitOfWork.GetRepository<Role>();
            var existing = repository.GetByID(role.Role.Id);
            if (existing == null)
            {
                var newRole = _mapper.Map<Role>(role.Role);
                Array.ForEach(role.Permissions
                        .Where(perm => perm.IsEnabled)
                        .ToArray(), perm => newRole.Permissions.Add(_mapper.Map<Permission>(perm)));
                repository.Insert(newRole);
                _unitOfWork.Commit();
            }
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

        private IUnitOfWork _unitOfWork;
        private IDomainMapper _mapper;
    }
}
