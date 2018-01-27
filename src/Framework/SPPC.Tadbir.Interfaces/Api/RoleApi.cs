﻿using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Api
{
    /// <summary>
    /// Defines API server routes and API client operation URLs for working with application roles.
    /// </summary>
    public sealed class RoleApi
    {
        private RoleApi()
        {
        }

        /// <summary>
        /// API client URL for all roles
        /// </summary>
        public const string Roles = "roles";

        /// <summary>
        /// API server route URL for all roles
        /// </summary>
        public const string RolesUrl = "roles";

        /// <summary>
        /// API server route URL for all roles
        /// </summary>
        public const string RolesSyncUrl = "roles/sync";

        /// <summary>
        /// API client URL for a role specified by unique identifier.
        /// </summary>
        public const string Role = "roles/{0}";

        /// <summary>
        /// API server route URL for a role specified by unique identifier.
        /// </summary>
        public const string RoleUrl = "roles/{roleId:min(1)}";

        /// <summary>
        /// API server route URL for a role specified by unique identifier.
        /// </summary>
        public const string RoleSyncUrl = "roles/{roleId:min(1)}/sync";

        /// <summary>
        /// API client URL for full details of a role specified by unique identifier.
        /// </summary>
        public const string RoleDetails = "roles/{0}/details";

        /// <summary>
        /// API server route URL for full details of a role specified by unique identifier.
        /// </summary>
        public const string RoleDetailsUrl = "roles/{roleId:min(1)}/details";

        /// <summary>
        /// API server route URL for full details of a role specified by unique identifier.
        /// </summary>
        public const string RoleDetailsSyncUrl = "roles/{roleId:min(1)}/details/sync";

        /// <summary>
        /// API client URL for branches accessible to a role specified by unique identifier.
        /// </summary>
        public const string RoleBranches = "roles/{0}/branches";

        /// <summary>
        /// API server route URL for branches accessible to a role specified by unique identifier.
        /// </summary>
        public const string RoleBranchesUrl = "roles/{roleId:min(1)}/branches";

        /// <summary>
        /// API server route URL for branches accessible to a role specified by unique identifier.
        /// </summary>
        public const string RoleBranchesSyncUrl = "roles/{roleId:min(1)}/branches/sync";

        /// <summary>
        /// API client URL for users assigned to a role specified by unique identifier.
        /// </summary>
        public const string RoleUsers = "roles/{0}/users";

        /// <summary>
        /// API server route URL for users assigned to a role specified by unique identifier.
        /// </summary>
        public const string RoleUsersUrl = "roles/{roleId:min(1)}/users";

        /// <summary>
        /// API server route URL for users assigned to a role specified by unique identifier.
        /// </summary>
        public const string RoleUsersSyncUrl = "roles/{roleId:min(1)}/users/sync";

        /// <summary>
        /// API client URL for a new role that contains all available permissions.
        /// </summary>
        public const string NewRole = "roles/new";

        /// <summary>
        /// API server route URL for a new role that contains all available permissions.
        /// </summary>
        public const string NewRoleUrl = "roles/new";

        /// <summary>
        /// API server route URL for a new role that contains all available permissions.
        /// </summary>
        public const string NewRoleSyncUrl = "roles/new/sync";
    }
}