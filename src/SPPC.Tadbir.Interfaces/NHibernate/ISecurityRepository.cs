﻿using System;
using System.Collections.Generic;
using SPPC.Tadbir.ViewModel.Auth;

namespace SPPC.Tadbir.NHibernate
{
    /// <summary>
    /// Defines repository operations related to security administration.
    /// </summary>
    public interface ISecurityRepository
    {
        #region User Management operations

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

        #endregion

        #region Role Management operations

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
        /// Retrieves a single role specified by unique identifier from repository.
        /// </summary>
        /// <param name="roleId">Unique identifier of the role to search for</param>
        /// <returns>A <see cref="RoleFullViewModel"/> instance that corresponds to the specified identifier, if there is
        /// such a role defined; otherwise, returns null.</returns>
        RoleFullViewModel GetRole(int roleId);

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

        #endregion
    }
}
