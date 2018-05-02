using System;
using System.Collections.Generic;
using SPPC.Tadbir.ViewModel.Auth;

namespace SPPC.Tadbir.Security
{
    /// <summary>
    /// Exposes all permissions currently applicable to managing a financial voucher. This is a singleton class.
    /// </summary>
    public sealed class VoucherPermission
    {
        private VoucherPermission()
        {
            View = new PermissionBriefViewModel()
            {
                EntityName = SecureEntity.Voucher,
                Flags = (int)VoucherPermissions.View
            };

            Create = new PermissionBriefViewModel()
            {
                EntityName = SecureEntity.Voucher,
                Flags = (int)VoucherPermissions.Create
            };

            Edit = new PermissionBriefViewModel()
            {
                EntityName = SecureEntity.Voucher,
                Flags = (int)VoucherPermissions.Edit
            };

            Delete = new PermissionBriefViewModel()
            {
                EntityName = SecureEntity.Voucher,
                Flags = (int)VoucherPermissions.Delete
            };

            Prepare = new PermissionBriefViewModel()
            {
                EntityName = SecureEntity.Voucher,
                Flags = (int)VoucherPermissions.Prepare
            };

            Review = new PermissionBriefViewModel()
            {
                EntityName = SecureEntity.Voucher,
                Flags = (int)VoucherPermissions.Review
            };

            Confirm = new PermissionBriefViewModel()
            {
                EntityName = SecureEntity.Voucher,
                Flags = (int)VoucherPermissions.Confirm
            };

            Approve = new PermissionBriefViewModel()
            {
                EntityName = SecureEntity.Voucher,
                Flags = (int)VoucherPermissions.Approve
            };

            All = new PermissionBriefViewModel()
            {
                EntityName = SecureEntity.Voucher,
                Flags = (int)VoucherPermissions.All
            };
        }

        /// <summary>
        /// Gets the one and only instance of the <see cref="VoucherPermission"/> class.
        /// </summary>
        public static VoucherPermission Instance
        {
            get { return _instance; }
        }

        /// <summary>
        /// Gets an object that indicates permission to view voucher list or details of a voucher.
        /// </summary>
        public PermissionBriefViewModel View { get; private set; }

        /// <summary>
        /// Gets an object that indicates permission to create a new voucher.
        /// </summary>
        public PermissionBriefViewModel Create { get; private set; }

        /// <summary>
        /// Gets an object that indicates permission to edit an existing voucher.
        /// </summary>
        public PermissionBriefViewModel Edit { get; private set; }

        /// <summary>
        /// Gets an object that indicates permission to delete an existing voucher.
        /// </summary>
        public PermissionBriefViewModel Delete { get; private set; }

        /// <summary>
        /// Gets an object that indicates permission to prepare an existing voucher.
        /// </summary>
        public PermissionBriefViewModel Prepare { get; private set; }

        /// <summary>
        /// Gets an object that indicates permission to review an existing voucher.
        /// </summary>
        public PermissionBriefViewModel Review { get; private set; }

        /// <summary>
        /// Gets an object that indicates permission to confirm an existing voucher.
        /// </summary>
        public PermissionBriefViewModel Confirm { get; private set; }

        /// <summary>
        /// Gets an object that indicates permission to approve an existing voucher.
        /// </summary>
        public PermissionBriefViewModel Approve { get; private set; }

        /// <summary>
        /// Gets an object that indicates all permissions for managing a voucher.
        /// </summary>
        public PermissionBriefViewModel All { get; private set; }

        private static VoucherPermission _instance = new VoucherPermission();
    }
}
