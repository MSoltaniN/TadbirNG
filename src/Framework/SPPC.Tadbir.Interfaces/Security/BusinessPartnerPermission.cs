using System;
using SPPC.Tadbir.ViewModel.Auth;

namespace SPPC.Tadbir.Security
{
    /// <summary>
    /// Exposes all permissions currently applicable to managing a business partner. This is a singleton class.
    /// </summary>
    public sealed class BusinessPartnerPermission
    {
        private BusinessPartnerPermission()
        {
            View = new PermissionBriefViewModel()
            {
                EntityName = SecureEntity.BusinessPartner,
                Flags = (int)BusinessPartnerPermissions.View
            };
            Create = new PermissionBriefViewModel()
            {
                EntityName = SecureEntity.BusinessPartner,
                Flags = (int)BusinessPartnerPermissions.Create
            };
            Edit = new PermissionBriefViewModel()
            {
                EntityName = SecureEntity.BusinessPartner,
                Flags = (int)BusinessPartnerPermissions.Edit
            };
            Delete = new PermissionBriefViewModel()
            {
                EntityName = SecureEntity.BusinessPartner,
                Flags = (int)BusinessPartnerPermissions.Delete
            };
            All = new PermissionBriefViewModel()
            {
                EntityName = SecureEntity.BusinessPartner,
                Flags = (int)BusinessPartnerPermissions.All
            };
        }

        /// <summary>
        /// Gets the one and only instance of the <see cref="BusinessPartnerPermission"/> class.
        /// </summary>
        public static BusinessPartnerPermission Instance
        {
            get { return _instance; }
        }

        /// <summary>
        /// Gets an object that indicates permission to view business partner list or details of a business partner.
        /// </summary>
        public PermissionBriefViewModel View { get; private set; }

        /// <summary>
        /// Gets an object that indicates permission to create a new business partner.
        /// </summary>
        public PermissionBriefViewModel Create { get; private set; }

        /// <summary>
        /// Gets an object that indicates permission to edit an existing business partner.
        /// </summary>
        public PermissionBriefViewModel Edit { get; private set; }

        /// <summary>
        /// Gets an object that indicates permission to delete an existing business partner.
        /// </summary>
        public PermissionBriefViewModel Delete { get; private set; }

        /// <summary>
        /// Gets an object that indicates all permissions for managing business partners.
        /// </summary>
        public PermissionBriefViewModel All { get; private set; }

        private static BusinessPartnerPermission _instance = new BusinessPartnerPermission();
    }
}
