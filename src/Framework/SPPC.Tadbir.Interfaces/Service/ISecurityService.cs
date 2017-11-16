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
        /// Removes an authenticated user from current application context.
        /// </summary>
        void Logout();

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
        /// Retrieves context information for a single user specified by identifier.
        /// </summary>
        /// <param name="userId">Unique identifier of an existing user</param>
        /// <returns>A <see cref="UserContextViewModel"/> object containing user context information,
        /// if user can be found; otherwise, returns null.</returns>
        UserContextViewModel GetUserContext(int userId);

        /// <summary>
        /// Inserts or updates an application user.
        /// </summary>
        /// <param name="user">User to insert or update</param>
        /// <returns>A <see cref="ServiceResponse"/> object that contains details about the result of operation</returns>
        ServiceResponse SaveUser(UserViewModel user);

        /// <summary>
        /// Updates the password in a user's profile.
        /// </summary>
        /// <param name="profile">An object containing user profile information</param>
        /// <returns>A <see cref="ServiceResponse"/> object that contains details about the result of operation</returns>
        ServiceResponse ChangePassword(UserProfileViewModel profile);

        /// <summary>
        /// Retrieves all application-defined roles currently registered in security system.
        /// </summary>
        /// <returns>Collection of all roles in security system</returns>
        IEnumerable<RoleViewModel> GetRoles();

        /// <summary>
        /// Retrieves all existing permissions and includes them in a blank (uninitializes) role instance.
        /// </summary>
        /// <returns>A <see cref="RoleFullViewModel"/> object that contains all available permissions</returns>
        RoleFullViewModel GetNewRole();

        /// <summary>
        /// Retrieves a single role specified by unique identifier.
        /// </summary>
        /// <param name="roleId">Unique identifier of an existing role</param>
        /// <returns>A <see cref="RoleFullViewModel"/> object containing role information, if role can be found;
        /// otherwise, returns null.</returns>
        RoleFullViewModel GetRole(int roleId);

        /// <summary>
        /// Retrieves a single role including full details (specified by role identifier).
        /// </summary>
        /// <param name="roleId">Unique identifier of an existing role</param>
        /// <returns>A <see cref="RoleDetailsViewModel"/> object containing role information, if role can be found;
        /// otherwise, returns null.</returns>
        RoleDetailsViewModel GetRoleDetails(int roleId);

        /// <summary>
        /// Inserts or updates a security role.
        /// </summary>
        /// <param name="role">Role to insert or update</param>
        /// <returns>A <see cref="ServiceResponse"/> object that contains details about the result of operation</returns>
        ServiceResponse SaveRole(RoleFullViewModel role);

        /// <summary>
        /// Deletes a security role.
        /// </summary>
        /// <param name="roleId">Unique identifier of an existing role</param>
        /// <returns>A <see cref="ServiceResponse"/> object that contains details about the result of operation</returns>
        ServiceResponse DeleteRole(int roleId);

        /// <summary>
        /// Retrieves information about all branches accessible by a role specified by unique identifier.
        /// </summary>
        /// <param name="roleId">Unique identifier of an existing role</param>
        /// <returns>A <see cref="RoleBranchesViewModel"/> object containing accessible branches, if the role can be found;
        /// otherwise, returns null.</returns>
        RoleBranchesViewModel GetRoleBranches(int roleId);

        /// <summary>
        /// Updates accessible branches for a role specified by unique identifier.
        /// </summary>
        /// <param name="branches">A <see cref="RoleBranchesViewModel"/> object containing accessible branches for the role
        /// </param>
        /// <returns>A <see cref="ServiceResponse"/> object that contains details about the result of operation</returns>
        ServiceResponse SaveRoleBranches(RoleBranchesViewModel branches);

        /// <summary>
        /// Retrieves information about all users that have a role specified by unique identifier.
        /// </summary>
        /// <param name="roleId">Unique identifier of an existing role</param>
        /// <returns>A <see cref="RoleUsersViewModel"/> object containing assigned users, if the role can be found;
        /// otherwise, returns null.</returns>
        RoleUsersViewModel GetRoleUsers(int roleId);

        /// <summary>
        /// Updates assigned users for a role specified by unique identifier.
        /// </summary>
        /// <param name="users">A <see cref="RoleUsersViewModel"/> object containing users assigned to the role
        /// </param>
        /// <returns>A <see cref="ServiceResponse"/> object that contains details about the result of operation</returns>
        ServiceResponse SaveRoleUsers(RoleUsersViewModel users);
    }
}
