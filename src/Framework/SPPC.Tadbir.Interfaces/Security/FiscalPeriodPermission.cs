using System;
using SPPC.Tadbir.ViewModel.Auth;

namespace SPPC.Tadbir.Security
{
    /// <summary>
    /// Exposes all permissions currently applicable to managing a fiscal period. This is a singleton class.
    /// </summary>
    public sealed class FiscalPeriodPermission
    {
        private FiscalPeriodPermission()
        {
            View = new PermissionBriefViewModel()
            {
                EntityName = SecureEntity.FiscalPeriod,
                Flags = (int)FiscalPeriodPermissions.View
            };
            Create = new PermissionBriefViewModel()
            {
                EntityName = SecureEntity.FiscalPeriod,
                Flags = (int)FiscalPeriodPermissions.Create
            };
            Edit = new PermissionBriefViewModel()
            {
                EntityName = SecureEntity.FiscalPeriod,
                Flags = (int)FiscalPeriodPermissions.Edit
            };
            Delete = new PermissionBriefViewModel()
            {
                EntityName = SecureEntity.FiscalPeriod,
                Flags = (int)FiscalPeriodPermissions.Delete
            };
            All = new PermissionBriefViewModel()
            {
                EntityName = SecureEntity.FiscalPeriod,
                Flags = (int)FiscalPeriodPermissions.All
            };
        }

        /// <summary>
        /// Gets the one and only instance of the <see cref="FiscalPeriodPermission"/> class.
        /// </summary>
        public static FiscalPeriodPermission Instance
        {
            get { return _instance; }
        }

        /// <summary>
        /// Gets an object that indicates permission to view fiscal period list or details of a fiscal period.
        /// </summary>
        public PermissionBriefViewModel View { get; private set; }

        /// <summary>
        /// Gets an object that indicates permission to create a new fiscal period.
        /// </summary>
        public PermissionBriefViewModel Create { get; private set; }

        /// <summary>
        /// Gets an object that indicates permission to edit an existing fiscal period.
        /// </summary>
        public PermissionBriefViewModel Edit { get; private set; }

        /// <summary>
        /// Gets an object that indicates permission to delete an existing fiscal period.
        /// </summary>
        public PermissionBriefViewModel Delete { get; private set; }

        /// <summary>
        /// Gets an object that indicates all permissions for managing fiscal periods.
        /// </summary>
        public PermissionBriefViewModel All { get; private set; }

        private static FiscalPeriodPermission _instance = new FiscalPeriodPermission();
    }
}
