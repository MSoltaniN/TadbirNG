using System;
using System.Collections.Generic;
using SPPC.Tadbir.ViewModel.Auth;

namespace SPPC.Tadbir.Security
{
    /// <summary>
    /// Exposes all permissions currently applicable to managing an application role. This is a singleton class.
    /// </summary>
    public sealed class RolePermission
    {
        private RolePermission()
        {
            View = new PermissionBriefViewModel()
            {
                EntityName = SecureEntity.Role,
                Flags = (int)RolePermissions.View
            };

            Create = new PermissionBriefViewModel()
            {
                EntityName = SecureEntity.Role,
                Flags = (int)RolePermissions.Create
            };

            Edit = new PermissionBriefViewModel()
            {
                EntityName = SecureEntity.Role,
                Flags = (int)RolePermissions.Edit
            };

            Delete = new PermissionBriefViewModel()
            {
                EntityName = SecureEntity.Role,
                Flags = (int)RolePermissions.Delete
            };

            AssignUsers = new PermissionBriefViewModel()
            {
                EntityName = SecureEntity.Role,
                Flags = (int)RolePermissions.AssignUsers
            };

            AssignBranches = new PermissionBriefViewModel()
            {
                EntityName = SecureEntity.Role,
                Flags = (int)RolePermissions.AssignBranches
            };

            All = new PermissionBriefViewModel()
            {
                EntityName = SecureEntity.Role,
                Flags = (int)RolePermissions.All
            };
        }

        /// <summary>
        /// Gets the one and only instance of the <see cref="RolePermission"/> class.
        /// </summary>
        public static RolePermission Instance
        {
            get { return _instance; }
        }

        /// <summary>
        /// Gets an object that indicates permission to view role list or details of a role.
        /// </summary>
        public PermissionBriefViewModel View { get; private set; }

        /// <summary>
        /// Gets an object that indicates permission to create a new role.
        /// </summary>
        public PermissionBriefViewModel Create { get; private set; }

        /// <summary>
        /// Gets an object that indicates permission to edit an existing role.
        /// </summary>
        public PermissionBriefViewModel Edit { get; private set; }

        /// <summary>
        /// Gets an object that indicates permission to delete an existing role.
        /// </summary>
        public PermissionBriefViewModel Delete { get; private set; }

        /// <summary>
        /// Gets an object that indicates permission to add/remove one or more users to/from a role.
        /// </summary>
        public PermissionBriefViewModel AssignUsers { get; private set; }

        /// <summary>
        /// Gets an object that indicates permission to add/remove one or more branches accessible to a role.
        /// </summary>
        public PermissionBriefViewModel AssignBranches { get; private set; }

        /// <summary>
        /// Gets an object that indicates all permissions for managing a role.
        /// </summary>
        public PermissionBriefViewModel All { get; private set; }

        private static RolePermission _instance = new RolePermission();
    }
}
