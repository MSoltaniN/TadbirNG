﻿using System;
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
                .GetByCriteria(usr => usr.UserName == user.UserName)
                .FirstOrDefault();
            return (existing != null);
        }

        private void UpdateExistingUser(User existing, UserViewModel user)
        {
            var modifiedUser = _mapper.Map<User>(user);
            existing.UserName = user.UserName;
            existing.PasswordHash = modifiedUser.PasswordHash;
            existing.IsEnabled = user.IsEnabled;
            existing.LastLoginDate = user.LastLoginDate;
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
