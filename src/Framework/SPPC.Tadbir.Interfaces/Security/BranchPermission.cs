using System;
using SPPC.Tadbir.ViewModel.Auth;

namespace SPPC.Tadbir.Security
{
    /// <summary>
    /// Exposes all permissions currently applicable to managing a branch. This is a singleton class.
    /// </summary>
    public sealed class BranchPermission
    {
        private BranchPermission()
        {
            View = new PermissionBriefViewModel()
            {
                EntityName = SecureEntity.Branch,
                Flags = (int)BranchPermissions.View
            };
            Create = new PermissionBriefViewModel()
            {
                EntityName = SecureEntity.Branch,
                Flags = (int)BranchPermissions.Create
            };
            Edit = new PermissionBriefViewModel()
            {
                EntityName = SecureEntity.Branch,
                Flags = (int)BranchPermissions.Edit
            };
            Delete = new PermissionBriefViewModel()
            {
                EntityName = SecureEntity.Branch,
                Flags = (int)BranchPermissions.Delete
            };
            All = new PermissionBriefViewModel()
            {
                EntityName = SecureEntity.Branch,
                Flags = (int)BranchPermissions.All
            };
        }

        /// <summary>
        /// Gets the one and only instance of the <see cref="BranchPermission"/> class.
        /// </summary>
        public static BranchPermission Instance
        {
            get { return _instance; }
        }

        /// <summary>
        /// Gets an object that indicates permission to view branch list or details of a branch.
        /// </summary>
        public PermissionBriefViewModel View { get; private set; }

        /// <summary>
        /// Gets an object that indicates permission to create a new branch.
        /// </summary>
        public PermissionBriefViewModel Create { get; private set; }

        /// <summary>
        /// Gets an object that indicates permission to edit an existing branch.
        /// </summary>
        public PermissionBriefViewModel Edit { get; private set; }

        /// <summary>
        /// Gets an object that indicates permission to delete an existing branch.
        /// </summary>
        public PermissionBriefViewModel Delete { get; private set; }

        /// <summary>
        /// Gets an object that indicates all permissions for managing branches.
        /// </summary>
        public PermissionBriefViewModel All { get; private set; }

        private static BranchPermission _instance = new BranchPermission();
    }
}
