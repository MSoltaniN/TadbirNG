using System;
using SPPC.Tadbir.ViewModel.Auth;

namespace SPPC.Tadbir.Security
{
    /// <summary>
    /// Exposes all permissions currently applicable to managing a business unit. This is a singleton class.
    /// </summary>
    public sealed class BusinessUnitPermission
    {
        private BusinessUnitPermission()
        {
            View = new PermissionBriefViewModel()
            {
                EntityName = SecureEntity.BusinessUnit,
                Flags = (int)BusinessUnitPermissions.View
            };
            Create = new PermissionBriefViewModel()
            {
                EntityName = SecureEntity.BusinessUnit,
                Flags = (int)BusinessUnitPermissions.Create
            };
            Edit = new PermissionBriefViewModel()
            {
                EntityName = SecureEntity.BusinessUnit,
                Flags = (int)BusinessUnitPermissions.Edit
            };
            Delete = new PermissionBriefViewModel()
            {
                EntityName = SecureEntity.BusinessUnit,
                Flags = (int)BusinessUnitPermissions.Delete
            };
            All = new PermissionBriefViewModel()
            {
                EntityName = SecureEntity.BusinessUnit,
                Flags = (int)BusinessUnitPermissions.All
            };
        }

        /// <summary>
        /// Gets the one and only instance of the <see cref="BusinessUnitPermission"/> class.
        /// </summary>
        public static BusinessUnitPermission Instance
        {
            get { return _instance; }
        }

        /// <summary>
        /// Gets an object that indicates permission to view business unit list or details of a business unit.
        /// </summary>
        public PermissionBriefViewModel View { get; private set; }

        /// <summary>
        /// Gets an object that indicates permission to create a new business unit.
        /// </summary>
        public PermissionBriefViewModel Create { get; private set; }

        /// <summary>
        /// Gets an object that indicates permission to edit an existing business unit.
        /// </summary>
        public PermissionBriefViewModel Edit { get; private set; }

        /// <summary>
        /// Gets an object that indicates permission to delete an existing business unit.
        /// </summary>
        public PermissionBriefViewModel Delete { get; private set; }

        /// <summary>
        /// Gets an object that indicates all permissions for managing business units.
        /// </summary>
        public PermissionBriefViewModel All { get; private set; }

        private static BusinessUnitPermission _instance = new BusinessUnitPermission();
    }
}
