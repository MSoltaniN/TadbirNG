using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SPPC.Framework.Helpers;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.ViewModel.Auth;
using SPPC.Tadbir.ViewModel.Metadata;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// Defines repository operations related to security administration.
    /// </summary>
    public interface ISecurityRepository
    {
        #region User Management operations

        #region Asynchronous Methods

        /// <summary>
        /// Asynchronously retrieves all application users from repository.
        /// </summary>
        /// <returns>A collection of <see cref="UserViewModel"/> objects retrieved from repository</returns>
        Task<IList<UserViewModel>> GetUsersAsync(GridOptions gridOptions = null);

        /// <summary>
        /// Asynchronously retrieves a single user specified by user name from repository.
        /// </summary>
        /// <param name="userName">User name to search for</param>
        /// <returns>A <see cref="UserViewModel"/> instance that corresponds to the specified user name, if there is
        /// such a user defined; otherwise, returns null.</returns>
        Task<UserViewModel> GetUserAsync(string userName);

        /// <summary>
        /// Asynchronously retrieves a single user specified by unique identifier from repository.
        /// </summary>
        /// <param name="userId">Unique identifier of the user to search for</param>
        /// <returns>A <see cref="UserViewModel"/> instance that corresponds to the specified identifier, if there is
        /// such a user defined; otherwise, returns null.</returns>
        Task<UserViewModel> GetUserAsync(int userId);

        /// <summary>
        /// به روش آسنکرون، اطلاعات فراداده ای تعریف شده برای کاربر را از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <returns>اطلاعات فراداده ای تعریف شده برای کاربر</returns>
        Task<EntityItemViewModel<UserViewModel>> GetUserMetadataAsync();

        /// <summary>
        /// Asynchronously retrieves context information for a user specified by unique identifier from repository.
        /// </summary>
        /// <param name="userId">Unique identifier of the user to search for</param>
        /// <returns>A <see cref="UserContextViewModel"/> instance containing context information, if there is
        /// such a user defined; otherwise, returns null.</returns>
        Task<UserContextViewModel> GetUserContextAsync(int userId);

        /// <summary>
        /// به روش آسنکرون، تعداد کاربران تعریف شده را از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>تعداد کاربران تعریف شده</returns>
        Task<int> GetUserCountAsync(GridOptions gridOptions = null);

        /// <summary>
        /// Asynchronously inserts or updates a single user in repository.
        /// </summary>
        /// <param name="user">Item to insert or update</param>
        Task<UserViewModel> SaveUserAsync(UserViewModel user);

        /// <summary>
        /// Asynchronously sets LastLoginDate field of the specified user to current system date/time.
        /// </summary>
        /// <param name="userId">Unique identifier of an existing user</param>
        Task UpdateUserLastLoginAsync(int userId);

        /// <summary>
        /// Asynchronously updates a user profile in repository.
        /// </summary>
        /// <param name="profile">User profile to update</param>
        Task UpdateUserPasswordAsync(UserProfileViewModel profile);

        /// <summary>
        /// Asynchronously determines if the specified <see cref="UserViewModel"/> instance has a user name that is already used
        /// by a different user.
        /// </summary>
        /// <param name="user">User item to check for duplicate user name</param>
        /// <returns>True if the user name is already used; otherwise returns false.</returns>
        Task<bool> IsDuplicateUserAsync(UserViewModel user);

        #endregion

        #region Synchronous Methods (May be removed in the future)

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
        /// Retrieves context information for a user specified by unique identifier from repository.
        /// </summary>
        /// <param name="userId">Unique identifier of the user to search for</param>
        /// <returns>A <see cref="UserContextViewModel"/> instance containing context information, if there is
        /// such a user defined; otherwise, returns null.</returns>
        UserContextViewModel GetUserContext(int userId);

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
        /// Updates a user profile in repository.
        /// </summary>
        /// <param name="profile">User profile to update</param>
        void UpdateUserPassword(UserProfileViewModel profile);

        /// <summary>
        /// Determines if the specified <see cref="UserViewModel"/> instance has a user name that is already used
        /// by a different user.
        /// </summary>
        /// <param name="user">User item to check for duplicate user name</param>
        /// <returns>True if the user name is already used; otherwise returns false.</returns>
        bool IsDuplicateUser(UserViewModel user);

        #endregion

        #endregion

        #region Role Management operations

        #region Asynchronous Methods

        /// <summary>
        /// Asynchronously retrieves all application roles from repository.
        /// </summary>
        /// <returns>A collection of <see cref="RoleViewModel"/> objects retrieved from repository</returns>
        Task<IList<RoleViewModel>> GetRolesAsync();

        /// <summary>
        /// Asynchronously initializes and returns a new role object that contains all available security permissions.
        /// </summary>
        /// <returns>A blank <see cref="RoleFullViewModel"/> object that contains full permission list from repository
        /// </returns>
        Task<RoleFullViewModel> GetNewRoleAsync();

        /// <summary>
        /// Asynchronously retrieves a single role with permissions (specified by role identifier) from repository.
        /// </summary>
        /// <param name="roleId">Unique identifier of the role to search for</param>
        /// <returns>A <see cref="RoleFullViewModel"/> instance that corresponds to the specified identifier, if there is
        /// such a role defined; otherwise, returns null.</returns>
        Task<RoleFullViewModel> GetRoleAsync(int roleId);

        /// <summary>
        /// Asynchronously retrieves a single role with full details (specified by role identifier) from repository.
        /// </summary>
        /// <param name="roleId">Unique identifier of the role to search for</param>
        /// <returns>A <see cref="RoleDetailsViewModel"/> instance that corresponds to the specified identifier, if there is
        /// such a role defined; otherwise, returns null.</returns>
        Task<RoleDetailsViewModel> GetRoleDetailsAsync(int roleId);

        /// <summary>
        /// Asynchronously retrieves brief information for a single role specified by unique identifier from repository.
        /// </summary>
        /// <param name="roleId">Unique identifier of the role to search for</param>
        /// <returns>A <see cref="RoleViewModel"/> instance that corresponds to the specified role identifier,
        /// if there is such a role defined; otherwise, returns null.</returns>
        Task<RoleViewModel> GetRoleBriefAsync(int roleId);

        /// <summary>
        /// Asynchronously inserts or updates a single security role, including all permissions in it, in repository
        /// </summary>
        /// <param name="role">Role to insert or update</param>
        Task SaveRoleAsync(RoleFullViewModel role);

        /// <summary>
        /// Asynchronously deletes a role specified by unique identifier from repository.
        /// </summary>
        /// <param name="roleId">Unique identifier of the role to delete</param>
        /// <remarks>If no role with specified identifier could be found, no exception should be thrown.</remarks>
        Task DeleteRoleAsync(int roleId);

        /// <summary>
        /// Asynchronously determines if an existing role specified by unique identifier is assigned to users.
        /// </summary>
        /// <param name="roleId">Unique identifier of the role to search for</param>
        /// <returns>true if specified role is assigned; otherwise false.</returns>
        Task<bool> IsAssignedRoleAsync(int roleId);

        /// <summary>
        /// Asynchronously retrieves branch associations for a role specified by identifier.
        /// </summary>
        /// <param name="roleId">Unique identifier of an existing role</param>
        /// <returns>An object that contains information about all branches accessible by specified role</returns>
        Task<RoleBranchesViewModel> GetRoleBranchesAsync(int roleId);

        /// <summary>
        /// Asynchronously updates branch associations for a role specified by identifier.
        /// </summary>
        /// <param name="role">A <see cref="RoleBranchesViewModel"/> object that contains information about all branch
        /// associations to the specified role</param>
        Task SaveRoleBranchesAsync(RoleBranchesViewModel role);

        /// <summary>
        /// Asynchronously retrieves user associations for a role specified by identifier.
        /// </summary>
        /// <param name="roleId">Unique identifier of an existing role</param>
        /// <returns>An object that contains information about all users assigned to specified role</returns>
        Task<RoleUsersViewModel> GetRoleUsersAsync(int roleId);

        /// <summary>
        /// Asynchronously updates user associations for a role specified by identifier.
        /// </summary>
        /// <param name="role">A <see cref="RoleUsersViewModel"/> object that contains information about all user
        /// associations to the specified role</param>
        Task SaveRoleUsersAsync(RoleUsersViewModel role);

        #endregion

        #region Synchronous Methods (May be removed in the future)

        /// <summary>
        /// Retrieves all application roles from repository.
        /// </summary>
        /// <returns>A collection of <see cref="RoleViewModel"/> objects retrieved from repository</returns>
        IList<RoleViewModel> GetRoles();

        /// <summary>
        /// Initializes and returns a new role object that contains all available security permissions.
        /// </summary>
        /// <returns>A blank <see cref="RoleFullViewModel"/> object that contains full permission list from repository
        /// </returns>
        RoleFullViewModel GetNewRole();

        /// <summary>
        /// Retrieves a single role with permissions (specified by role identifier) from repository.
        /// </summary>
        /// <param name="roleId">Unique identifier of the role to search for</param>
        /// <returns>A <see cref="RoleFullViewModel"/> instance that corresponds to the specified identifier, if there is
        /// such a role defined; otherwise, returns null.</returns>
        RoleFullViewModel GetRole(int roleId);

        /// <summary>
        /// Retrieves a single role with full details (specified by role identifier) from repository.
        /// </summary>
        /// <param name="roleId">Unique identifier of the role to search for</param>
        /// <returns>A <see cref="RoleDetailsViewModel"/> instance that corresponds to the specified identifier, if there is
        /// such a role defined; otherwise, returns null.</returns>
        RoleDetailsViewModel GetRoleDetails(int roleId);

        /// <summary>
        /// Retrieves brief information for a single role specified by unique identifier from repository.
        /// </summary>
        /// <param name="roleId">Unique identifier of the role to search for</param>
        /// <returns>A <see cref="RoleViewModel"/> instance that corresponds to the specified role identifier,
        /// if there is such a role defined; otherwise, returns null.</returns>
        RoleViewModel GetRoleBrief(int roleId);

        /// <summary>
        /// Inserts or updates a single security role, including all permissions in it, in repository
        /// </summary>
        /// <param name="role">Role to insert or update</param>
        void SaveRole(RoleFullViewModel role);

        /// <summary>
        /// Deletes a role specified by unique identifier from repository.
        /// </summary>
        /// <param name="roleId">Unique identifier of the role to delete</param>
        /// <remarks>If no role with specified identifier could be found, no exception should be thrown.</remarks>
        void DeleteRole(int roleId);

        /// <summary>
        /// Determines if an existing role specified by unique identifier is assigned to users.
        /// </summary>
        /// <param name="roleId">Unique identifier of the role to search for</param>
        /// <returns>true if specified role is assigned; otherwise false.</returns>
        bool IsAssignedRole(int roleId);

        /// <summary>
        /// Retrieves branch associations for a role specified by identifier.
        /// </summary>
        /// <param name="roleId">Unique identifier of an existing role</param>
        /// <returns>An object that contains information about all branches accessible by specified role</returns>
        RoleBranchesViewModel GetRoleBranches(int roleId);

        /// <summary>
        /// Updates branch associations for a role specified by identifier.
        /// </summary>
        /// <param name="role">A <see cref="RoleBranchesViewModel"/> object that contains information about all branch
        /// associations to the specified role</param>
        void SaveRoleBranches(RoleBranchesViewModel role);

        /// <summary>
        /// Retrieves user associations for a role specified by identifier.
        /// </summary>
        /// <param name="roleId">Unique identifier of an existing role</param>
        /// <returns>An object that contains information about all users assigned to specified role</returns>
        RoleUsersViewModel GetRoleUsers(int roleId);

        /// <summary>
        /// Updates user associations for a role specified by identifier.
        /// </summary>
        /// <param name="role">A <see cref="RoleUsersViewModel"/> object that contains information about all user
        /// associations to the specified role</param>
        void SaveRoleUsers(RoleUsersViewModel role);

        #endregion

        #endregion
    }
}