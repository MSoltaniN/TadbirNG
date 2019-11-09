using System;
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
        /// API client URL for a role specified by unique identifier.
        /// </summary>
        public const string Role = "roles/{0}";

        /// <summary>
        /// API server route URL for a role specified by unique identifier.
        /// </summary>
        public const string RoleUrl = "roles/{roleId:min(1)}";

        /// <summary>
        /// API client URL for full details of a role specified by unique identifier.
        /// </summary>
        public const string RoleDetails = "roles/{0}/details";

        /// <summary>
        /// API server route URL for full details of a role specified by unique identifier.
        /// </summary>
        public const string RoleDetailsUrl = "roles/{roleId:min(1)}/details";

        /// <summary>
        /// API client URL for branches accessible to a role specified by unique identifier.
        /// </summary>
        public const string RoleBranches = "roles/{0}/branches";

        /// <summary>
        /// API server route URL for branches accessible to a role specified by unique identifier.
        /// </summary>
        public const string RoleBranchesUrl = "roles/{roleId:min(1)}/branches";

        /// <summary>
        /// API client URL for users assigned to a role specified by unique identifier.
        /// </summary>
        public const string RoleUsers = "roles/{0}/users";

        /// <summary>
        /// API server route URL for users assigned to a role specified by unique identifier.
        /// </summary>
        public const string RoleUsersUrl = "roles/{roleId:min(1)}/users";

        /// <summary>
        /// API client URL for fiscal periods accessible to a role specified by unique identifier.
        /// </summary>
        public const string RoleFiscalPeriods = "roles/{0}/fperiods";

        /// <summary>
        /// API server route URL for fiscal periods accessible to a role specified by unique identifier.
        /// </summary>
        public const string RoleFiscalPeriodsUrl = "roles/{roleId:min(1)}/fperiods";

        /// <summary>
        /// API client URL for a new role that contains all available permissions.
        /// </summary>
        public const string NewRole = "roles/new";

        /// <summary>
        /// API server route URL for a new role that contains all available permissions.
        /// </summary>
        public const string NewRoleUrl = "roles/new";

        /// <summary>
        /// API client URL for row access settings configured for a role specified by unique identifier
        /// </summary>
        public const string RowAccessSettings = "roles/{0}/rowaccess";

        /// <summary>
        /// API server route URL for row access settings configured for a role specified by unique identifier
        /// </summary>
        public const string RowAccessSettingsUrl = "roles/{roleId:min(2)}/rowaccess";
    }
}
