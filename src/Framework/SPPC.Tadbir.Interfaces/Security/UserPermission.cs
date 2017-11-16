using System;
using System.Collections.Generic;
using SPPC.Tadbir.ViewModel.Auth;

namespace SPPC.Tadbir.Security
{
    /// <summary>
    /// Exposes all permissions currently applicable to managing an application user. This is a singleton class.
    /// </summary>
    public sealed class UserPermission
    {
        private UserPermission()
        {
            View = new PermissionBriefViewModel()
            {
                EntityName = SecureEntity.User,
                Flags = (int)UserPermissions.View
            };
            Create = new PermissionBriefViewModel()
            {
                EntityName = SecureEntity.User,
                Flags = (int)UserPermissions.Create
            };
            Edit = new PermissionBriefViewModel()
            {
                EntityName = SecureEntity.User,
                Flags = (int)UserPermissions.Edit
            };
            All = new PermissionBriefViewModel()
            {
                EntityName = SecureEntity.User,
                Flags = (int)UserPermissions.All
            };
        }

        /// <summary>
        /// Gets the one and only instance of the <see cref="UserPermission"/> class.
        /// </summary>
        public static UserPermission Instance
        {
            get { return _instance; }
        }

        /// <summary>
        /// Gets an object that indicates permission to view user list or details of a user.
        /// </summary>
        public PermissionBriefViewModel View { get; private set; }

        /// <summary>
        /// Gets an object that indicates permission to create a new user.
        /// </summary>
        public PermissionBriefViewModel Create { get; private set; }

        /// <summary>
        /// Gets an object that indicates permission to edit an existing user.
        /// </summary>
        public PermissionBriefViewModel Edit { get; private set; }

        /// <summary>
        /// Gets an object that indicates all permissions for managing a user.
        /// </summary>
        public PermissionBriefViewModel All { get; private set; }

        private static UserPermission _instance = new UserPermission();
    }
}
