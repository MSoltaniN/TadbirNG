using System;
using SPPC.Tadbir.ViewModel.Auth;

namespace SPPC.Tadbir.Security
{
    /// <summary>
    /// Exposes all permissions currently applicable to managing a detail account. This is a singleton class.
    /// </summary>
    public sealed class DetailAccountPermission
    {
        private DetailAccountPermission()
        {
            View = new PermissionBriefViewModel()
            {
                EntityName = SecureEntity.DetailAccount,
                Flags = (int)DetailAccountPermissions.View
            };
            Create = new PermissionBriefViewModel()
            {
                EntityName = SecureEntity.DetailAccount,
                Flags = (int)DetailAccountPermissions.Create
            };
            Edit = new PermissionBriefViewModel()
            {
                EntityName = SecureEntity.DetailAccount,
                Flags = (int)DetailAccountPermissions.Edit
            };
            Delete = new PermissionBriefViewModel()
            {
                EntityName = SecureEntity.DetailAccount,
                Flags = (int)DetailAccountPermissions.Delete
            };
            All = new PermissionBriefViewModel()
            {
                EntityName = SecureEntity.DetailAccount,
                Flags = (int)DetailAccountPermissions.All
            };
        }

        /// <summary>
        /// Gets the one and only instance of the <see cref="DetailAccountPermission"/> class.
        /// </summary>
        public static DetailAccountPermission Instance
        {
            get { return _instance; }
        }

        /// <summary>
        /// Gets an object that indicates permission to view detail account list or details of a detail account.
        /// </summary>
        public PermissionBriefViewModel View { get; private set; }

        /// <summary>
        /// Gets an object that indicates permission to create a new detail account.
        /// </summary>
        public PermissionBriefViewModel Create { get; private set; }

        /// <summary>
        /// Gets an object that indicates permission to edit an existing detail account.
        /// </summary>
        public PermissionBriefViewModel Edit { get; private set; }

        /// <summary>
        /// Gets an object that indicates permission to delete an existing detail account.
        /// </summary>
        public PermissionBriefViewModel Delete { get; private set; }

        /// <summary>
        /// Gets an object that indicates all permissions for managing an detail account.
        /// </summary>
        public PermissionBriefViewModel All { get; private set; }

        private static DetailAccountPermission _instance = new DetailAccountPermission();
    }
}
