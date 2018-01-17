using System;
using SPPC.Tadbir.ViewModel.Auth;

namespace SPPC.Tadbir.Security
{
    /// <summary>
    /// Exposes all permissions currently applicable to managing a cost center. This is a singleton class.
    /// </summary>
    public sealed class CostCenterPermission
    {
        private CostCenterPermission()
        {
            View = new PermissionBriefViewModel()
            {
                EntityName = SecureEntity.CostCenter,
                Flags = (int)CostCenterPermissions.View
            };
            Create = new PermissionBriefViewModel()
            {
                EntityName = SecureEntity.CostCenter,
                Flags = (int)CostCenterPermissions.Create
            };
            Edit = new PermissionBriefViewModel()
            {
                EntityName = SecureEntity.CostCenter,
                Flags = (int)CostCenterPermissions.Edit
            };
            Delete = new PermissionBriefViewModel()
            {
                EntityName = SecureEntity.CostCenter,
                Flags = (int)CostCenterPermissions.Delete
            };
            All = new PermissionBriefViewModel()
            {
                EntityName = SecureEntity.CostCenter,
                Flags = (int)CostCenterPermissions.All
            };
        }

        /// <summary>
        /// Gets the one and only instance of the <see cref="CostCenterPermission"/> class.
        /// </summary>
        public static CostCenterPermission Instance
        {
            get { return _instance; }
        }

        /// <summary>
        /// Gets an object that indicates permission to view cost center list or details of a cost center.
        /// </summary>
        public PermissionBriefViewModel View { get; private set; }

        /// <summary>
        /// Gets an object that indicates permission to create a new cost center.
        /// </summary>
        public PermissionBriefViewModel Create { get; private set; }

        /// <summary>
        /// Gets an object that indicates permission to edit an existing cost center.
        /// </summary>
        public PermissionBriefViewModel Edit { get; private set; }

        /// <summary>
        /// Gets an object that indicates permission to delete an existing cost center.
        /// </summary>
        public PermissionBriefViewModel Delete { get; private set; }

        /// <summary>
        /// Gets an object that indicates all permissions for managing a cost center.
        /// </summary>
        public PermissionBriefViewModel All { get; private set; }

        private static CostCenterPermission _instance = new CostCenterPermission();
    }
}
