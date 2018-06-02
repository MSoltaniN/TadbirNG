using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;
using Microsoft.AspNetCore.Http;
using SPPC.Framework.Common;
using SPPC.Framework.Service;
using SPPC.Framework.Service.Security;
using SPPC.Tadbir.Api;
using SPPC.Tadbir.Values;
using SPPC.Tadbir.ViewModel;
using SPPC.Tadbir.ViewModel.Auth;

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
        /// <param name="httpContextAccessor">Provides current Web application context to use for security operations</param>
        public SecurityService(IApiClient apiClient, ICryptoService crypto, IHttpContextAccessor httpContextAccessor)
        {
            _apiClient = apiClient;
            _crypto = crypto;
            _httpContextAccessor = httpContextAccessor;
            _httpContext = httpContextAccessor.HttpContext;
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
            UserViewModel user = _apiClient.Get<UserViewModel>(UserApi.UserByName, login.UserName);
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
            throw ExceptionBuilder.NewInvalidOperationException("ERROR: Operation is currently disabled.");
        }

        /// <summary>
        /// Removes an authenticated user from current application context.
        /// </summary>
        public void Logout()
        {
            throw ExceptionBuilder.NewInvalidOperationException("ERROR: Operation is currently disabled.");
        }

        /// <summary>
        /// Retrieves all application users currently registered in security system.
        /// </summary>
        /// <returns>Collection of all users in security system</returns>
        public IEnumerable<UserViewModel> GetUsers()
        {
            var users = _apiClient.Get<IEnumerable<UserViewModel>>(UserApi.Users);
            return users;
        }

        /// <summary>
        /// Retrieves a single user specified by unique identifier.
        /// </summary>
        /// <param name="userId">Unique identifier of an existing user</param>
        /// <returns>A <see cref="UserViewModel"/> object containing user information, if user can be found;
        /// otherwise, returns null.</returns>
        public UserViewModel GetUser(int userId)
        {
            var user = _apiClient.Get<UserViewModel>(UserApi.User, userId);
            return user;
        }

        /// <summary>
        /// Retrieves context information for a single user specified by identifier.
        /// </summary>
        /// <param name="userId">Unique identifier of an existing user</param>
        /// <returns>A <see cref="UserContextViewModel"/> object containing user context information,
        /// if user can be found; otherwise, returns null.</returns>
        public UserContextViewModel GetUserContext(int userId)
        {
            var userContext = _apiClient.Get<UserContextViewModel>(UserApi.UserContext, userId);
            return userContext;
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
                response = _apiClient.Insert(user, UserApi.Users);
            }
            else
            {
                response = _apiClient.Update(user, UserApi.User, user.Id);
            }

            return response;
        }

        /// <summary>
        /// Updates the password in a user's profile.
        /// </summary>
        /// <param name="profile">An object containing user profile information</param>
        /// <returns>A <see cref="ServiceResponse"/> object that contains details about the result of operation</returns>
        public ServiceResponse ChangePassword(UserProfileViewModel profile)
        {
            ProcessProfile(profile);
            var response = _apiClient.Update(profile, UserApi.UserPassword, profile.UserName);
            return response;
        }

        /// <summary>
        /// Retrieves all application-defined roles currently registered in security system.
        /// </summary>
        /// <returns>Collection of all roles in security system</returns>
        public IEnumerable<RoleViewModel> GetRoles()
        {
            var roles = _apiClient.Get<IEnumerable<RoleViewModel>>(RoleApi.Roles);
            return roles;
        }

        /// <summary>
        /// Retrieves all existing permissions and includes them in a blank (uninitializes) role instance.
        /// </summary>
        /// <returns>A <see cref="RoleFullViewModel"/> object that contains all available permissions</returns>
        public RoleFullViewModel GetNewRole()
        {
            var newRole = _apiClient.Get<RoleFullViewModel>(RoleApi.NewRole);
            return newRole;
        }

        /// <summary>
        /// Retrieves a single role specified by unique identifier.
        /// </summary>
        /// <param name="roleId">Unique identifier of an existing role</param>
        /// <returns>A <see cref="RoleFullViewModel"/> object containing role information, if role can be found;
        /// otherwise, returns null.</returns>
        public RoleFullViewModel GetRole(int roleId)
        {
            var role = _apiClient.Get<RoleFullViewModel>(RoleApi.Role, roleId);
            return role;
        }

        /// <summary>
        /// Retrieves a single role including full details (specified by role identifier).
        /// </summary>
        /// <param name="roleId">Unique identifier of an existing role</param>
        /// <returns>A <see cref="RoleDetailsViewModel"/> object containing role information, if role can be found;
        /// otherwise, returns null.</returns>
        public RoleDetailsViewModel GetRoleDetails(int roleId)
        {
            var role = _apiClient.Get<RoleDetailsViewModel>(RoleApi.RoleDetails, roleId);
            return role;
        }

        /// <summary>
        /// Inserts or updates a security role.
        /// </summary>
        /// <param name="role">Role to insert or update</param>
        /// <returns>A <see cref="ServiceResponse"/> object that contains details about the result of operation</returns>
        public ServiceResponse SaveRole(RoleFullViewModel role)
        {
            Verify.ArgumentNotNull(role, "role");
            Verify.ArgumentNotNull(role.Role, "role.Role");
            ServiceResponse response = null;
            if (role.Role.Id == 0)
            {
                response = _apiClient.Insert(role, RoleApi.Roles);
            }
            else
            {
                response = _apiClient.Update(role, RoleApi.Role, role.Role.Id);
            }

            return response;
        }

        /// <summary>
        /// Deletes a security role. If specified role cannot be deleted, returns a response whose result
        /// is set to ServiceResult.DeleteFailed.
        /// </summary>
        /// <param name="roleId">Unique identifier of an existing role</param>
        /// <returns>A <see cref="ServiceResponse"/> object that contains details about the result of operation</returns>
        public ServiceResponse DeleteRole(int roleId)
        {
            var response = _apiClient.Delete(RoleApi.Role, roleId);
            if (!response.Succeeded)
            {
                response.Result = ServiceResult.DeleteFailed;
                response.Hint = Strings.DeleteRoleHint;
            }

            return response;
        }

        /// <summary>
        /// Retrieves information about all branches accessible by a role specified by unique identifier.
        /// </summary>
        /// <param name="roleId">Unique identifier of an existing role</param>
        /// <returns>A <see cref="RelatedItemsViewModel"/> object containing accessible branches, if the role can be found;
        /// otherwise, returns null.</returns>
        public RelatedItemsViewModel GetRoleBranches(int roleId)
        {
            var branches = _apiClient.Get<RelatedItemsViewModel>(RoleApi.RoleBranches, roleId);
            return branches;
        }

        /// <summary>
        /// Updates accessible branches for a role specified by unique identifier.
        /// </summary>
        /// <param name="branches">A <see cref="RelatedItemsViewModel"/> object containing accessible branches for the role
        /// </param>
        /// <returns>A <see cref="ServiceResponse"/> object that contains details about the result of operation</returns>
        public ServiceResponse SaveRoleBranches(RelatedItemsViewModel branches)
        {
            Verify.ArgumentNotNull(branches, "branches");
            ServiceResponse response = _apiClient.Update(branches, RoleApi.RoleBranches, branches.Id);
            return response;
        }

        /// <summary>
        /// Retrieves information about all users that have a role specified by unique identifier.
        /// </summary>
        /// <param name="roleId">Unique identifier of an existing role</param>
        /// <returns>A <see cref="RelatedItemsViewModel"/> object containing assigned users, if the role can be found;
        /// otherwise, returns null.</returns>
        public RelatedItemsViewModel GetRoleUsers(int roleId)
        {
            var users = _apiClient.Get<RelatedItemsViewModel>(RoleApi.RoleUsers, roleId);
            return users;
        }

        /// <summary>
        /// Updates assigned users for a role specified by unique identifier.
        /// </summary>
        /// <param name="users">A <see cref="RelatedItemsViewModel"/> object containing users assigned to the role
        /// </param>
        /// <returns>A <see cref="ServiceResponse"/> object that contains details about the result of operation</returns>
        public ServiceResponse SaveRoleUsers(RelatedItemsViewModel users)
        {
            Verify.ArgumentNotNull(users, "users");
            ServiceResponse response = _apiClient.Update(users, RoleApi.RoleUsers, users.Id);
            return response;
        }

        /// <summary>
        /// Associates the specified principal object with current Web context.
        /// </summary>
        /// <param name="principal">Principal instance corresponding to the current user</param>
        private void LoginPrincipal(IPrincipal principal)
        {
            throw ExceptionBuilder.NewInvalidOperationException("ERROR: Operation is currently disabled.");
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
            _apiClient.Update(new { }, UserApi.UserLastLogin, userId);
        }

        private void ProcessProfile(UserProfileViewModel profile)
        {
            Verify.ArgumentNotNull(profile, "profile");
            var oldPasswordHash = _crypto.CreateHash(profile.OldPassword);
            var newPasswordHash = _crypto.CreateHash(profile.NewPassword);
            profile.OldPassword = oldPasswordHash;
            profile.NewPassword = newPasswordHash;
            profile.RepeatPassword = newPasswordHash;
        }

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly HttpContext _httpContext;
        private IApiClient _apiClient;
        private ICryptoService _crypto;
    }
}
