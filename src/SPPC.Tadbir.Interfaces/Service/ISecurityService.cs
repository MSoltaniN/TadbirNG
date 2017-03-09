using System;
using System.Collections.Generic;
using SPPC.Framework.Service;
using SPPC.Tadbir.ViewModel.Auth;

namespace SPPC.Tadbir.Service
{
    /// <summary>
    /// Defines common security operation used for authentication and authorization.
    /// </summary>
    public interface ISecurityService
    {
        /// <summary>
        /// Authenticates a user from given credentials.
        /// </summary>
        /// <param name="login">Credentials to use for authentication</param>
        /// <returns>If the user is authenticated, returns a <see cref="UserViewModel"/> instance corresponding
        /// to user information inside database; otherwise, returns null.</returns>
        UserViewModel Authenticate(LoginViewModel login);

        /// <summary>
        /// Registers an authenticated user in current application context.
        /// </summary>
        /// <param name="user">User to set context for</param>
        void Login(UserViewModel user);

        /// <summary>
        /// Retrieves all application users currently registered in security system.
        /// </summary>
        /// <returns>Collection of all users in security system</returns>
        IEnumerable<UserViewModel> GetUsers();

        /// <summary>
        /// Retrieves a single user specified by unique identifier.
        /// </summary>
        /// <param name="userId">Unique identifier of an existing user</param>
        /// <returns>A <see cref="UserViewModel"/> object containing user information, if user can be found;
        /// otherwise, returns null.</returns>
        UserViewModel GetUser(int userId);

        /// <summary>
        /// Inserts or updates an application user.
        /// </summary>
        /// <param name="user">User to insert or update</param>
        /// <returns>A <see cref="ServiceResponse"/> object that contains details about the result of operation</returns>
        ServiceResponse SaveUser(UserViewModel user);

        /// <summary>
        /// Retrieves all application-defined roles currently registered in security system.
        /// </summary>
        /// <returns>Collection of all roles in security system</returns>
        IEnumerable<RoleViewModel> GetRoles();
    }
}
