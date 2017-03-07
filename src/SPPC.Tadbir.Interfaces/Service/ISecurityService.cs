using System;
using System.Collections.Generic;
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
    }
}
