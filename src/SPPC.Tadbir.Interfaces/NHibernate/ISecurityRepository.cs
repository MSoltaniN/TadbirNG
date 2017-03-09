using System;
using System.Collections.Generic;
using SPPC.Tadbir.ViewModel.Auth;

namespace SPPC.Tadbir.NHibernate
{
    /// <summary>
    /// Defines repository operations related to security administration.
    /// </summary>
    public interface ISecurityRepository
    {
        /// <summary>
        /// Retrieves all application users from repository.
        /// </summary>
        /// <returns>A collection of <see cref="UserViewModel"/> objects retrieved from repository</returns>
        IList<UserViewModel> GetUsers();

        /// <summary>
        /// Retrieves a single user specified by user name from repository.
        /// </summary>
        /// <param name="userName">User name to search for</param>
        /// <returns>A <see cref="UserViewModel"/> instance that corresponds to the specified user name, if there is
        /// such a user defined; otherwise, returns null.</returns>
        UserViewModel GetUser(string userName);

        /// <summary>
        /// Retrieves a single user specified by unique identifier from repository.
        /// </summary>
        /// <param name="userId">Unique identifier of the user to search for</param>
        /// <returns>A <see cref="UserViewModel"/> instance that corresponds to the specified identifier, if there is
        /// such a user defined; otherwise, returns null.</returns>
        UserViewModel GetUser(int userId);

        /// <summary>
        /// Inserts or updates a single user in repository.
        /// </summary>
        /// <param name="user">Item to insert or update</param>
        void SaveUser(UserViewModel user);

        /// <summary>
        /// Sets LastLoginDate field of the specified user to current system date/time.
        /// </summary>
        /// <param name="userId">Unique identifier of an existing user</param>
        void UpdateUserLastLogin(int userId);

        /// <summary>
        /// Determines if the specified <see cref="UserViewModel"/> instance has a user name that is already used
        /// by a different user.
        /// </summary>
        /// <param name="user">User item to check for duplicate user name</param>
        /// <returns>True if the user name is already used; otherwise returns false.</returns>
        bool IsDuplicateUser(UserViewModel user);

        /// <summary>
        /// Retrieves all application roles from repository.
        /// </summary>
        /// <returns>A collection of <see cref="RoleViewModel"/> objects retrieved from repository</returns>
        IList<RoleViewModel> GetRoles();
    }
}
