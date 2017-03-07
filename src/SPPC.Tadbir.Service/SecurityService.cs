using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Security;
using SPPC.Framework.Service;
using SPPC.Framework.Service.Security;
using SPPC.Tadbir.Api;
using SPPC.Tadbir.ViewModel.Auth;
using SwForAll.Platform.Common;

namespace SPPC.Tadbir.Service
{
    /// <summary>
    /// Provides common security operation that can be used in an ASP.NET Web appication that uses Forms authentication.
    /// </summary>
    public class SecurityService : ISecurityService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SecurityService"/> class.
        /// </summary>
        /// <param name="apiClient">Object that wraps common operations for calling a Web API service</param>
        /// <param name="crypto">An <see cref="ICryptoService"/> implementation to use for cryptographic operations</param>
        /// <param name="httpContext">Current Web application context to use for security operations</param>
        public SecurityService(IApiClient apiClient, ICryptoService crypto, HttpContextBase httpContext)
        {
            _apiClient = apiClient;
            _crypto = crypto;
            _httpContext = httpContext;
        }

        /// <summary>
        /// Authenticates a user from given credentials.
        /// </summary>
        /// <param name="login">Credentials to use for authentication</param>
        /// <returns>If the user is authenticated, returns a <see cref="UserViewModel"/> instance corresponding
        /// to user information inside database; otherwise, returns null.</returns>
        public UserViewModel Authenticate(LoginViewModel login)
        {
            Verify.ArgumentNotNull(login, "login");
            UserViewModel user = _apiClient.Get<UserViewModel>(SecurityApi.UserByName, login.UserName);
            if (IsAuthenticated(user, login.Password))
            {
                UpdateUserLogin(user.Id);
            }
            else
            {
                user = null;
            }

            return user;
        }

        /// <summary>
        /// Registers an authenticated user in current application context.
        /// </summary>
        /// <param name="user">User to set context for</param>
        public void Login(UserViewModel user)
        {
            Verify.ArgumentNotNull(user, "user");
            var principal = GetPrincipal(user);
            LoginPrincipal(principal);
        }

        /// <summary>
        /// Retrieves all application users currently registered in security system.
        /// </summary>
        /// <returns>Collection of all users in security system</returns>
        public IEnumerable<UserViewModel> GetUsers()
        {
            var users = _apiClient.Get<IEnumerable<UserViewModel>>(SecurityApi.Users);
            return users;
        }

        /// <summary>
        /// Inserts or updates an application user.
        /// </summary>
        /// <param name="user">User to insert or update</param>
        /// <returns>A <see cref="ServiceResponse"/> object that contains details about the result of operation</returns>
        public ServiceResponse SaveUser(UserViewModel user)
        {
            Verify.ArgumentNotNull(user, "user");
            ServiceResponse response = null;
            if (user.Id == 0)
            {
                response = _apiClient.Insert(user, SecurityApi.Users);
            }
            else
            {
                response = _apiClient.Update(user, SecurityApi.User, user.Id);
            }

            return response;
        }

        /// <summary>
        /// Creates and returns a concrete <see cref="IPrincipal"/> instance using specified member information,
        /// its assigned roles and a value indicating if user must be kept logged in.
        /// </summary>
        /// <param name="user">A <see cref="UserViewModel"/> instance for which a principal must be created</param>
        /// <returns>A principal created for the specified user</returns>
        private static IPrincipal GetPrincipal(UserViewModel user)
        {
            Verify.ArgumentNotNull(user, "user");
            var authCookie = FormsAuthentication.GetAuthCookie(user.UserName, false);
            var ticket = FormsAuthentication.Decrypt(authCookie.Value);
            FormsAuthentication.SetAuthCookie(user.UserName, false);
            var identity = new FormsIdentity(ticket);
            return new GenericPrincipal(identity, new string[0]);
        }

        /// <summary>
        /// Associates the specified principal object with current Web context.
        /// </summary>
        /// <param name="principal">Principal instance corresponding to the current user</param>
        private void LoginPrincipal(IPrincipal principal)
        {
            Verify.ArgumentNotNull(principal, "principal");
            _httpContext.User = principal;
            Thread.CurrentPrincipal = principal;
        }

        private bool IsAuthenticated(UserViewModel user, string password)
        {
            bool isAuthenticated = false;
            if (user != null)
            {
                byte[] passwordHash = Transform.FromHexString(user.Password);
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                isAuthenticated = user.IsEnabled && _crypto.ValidateHash(passwordBytes, passwordHash);
            }

            return isAuthenticated;
        }

        private void UpdateUserLogin(int userId)
        {
            _apiClient.Update(new { }, SecurityApi.UserLastLogin, userId);
        }

        private IApiClient _apiClient;
        private ICryptoService _crypto;
        private HttpContextBase _httpContext;
    }
}
