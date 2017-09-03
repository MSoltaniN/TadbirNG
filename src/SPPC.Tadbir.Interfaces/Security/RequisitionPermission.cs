using System;
using System.Collections.Generic;
using SPPC.Tadbir.ViewModel.Auth;

namespace SPPC.Tadbir.Security
{
    /// <summary>
    /// Exposes all permissions currently applicable to managing a requisition voucher. This is a singleton class.
    /// </summary>
    public sealed class RequisitionPermission
    {
        private RequisitionPermission()
        {
            View = new PermissionBriefViewModel()
            {
                EntityName = SecureEntity.Requisition,
                Flags = (int)RequisitionPermissions.View
            };
            Create = new PermissionBriefViewModel()
            {
                EntityName = SecureEntity.Requisition,
                Flags = (int)RequisitionPermissions.Create
            };
            Edit = new PermissionBriefViewModel()
            {
                EntityName = SecureEntity.Requisition,
                Flags = (int)RequisitionPermissions.Edit
            };
            Delete = new PermissionBriefViewModel()
            {
                EntityName = SecureEntity.Requisition,
                Flags = (int)RequisitionPermissions.Delete
            };
            Prepare = new PermissionBriefViewModel()
            {
                EntityName = SecureEntity.Requisition,
                Flags = (int)RequisitionPermissions.Prepare
            };
            Confirm = new PermissionBriefViewModel()
            {
                EntityName = SecureEntity.Requisition,
                Flags = (int)RequisitionPermissions.Confirm
            };
            Approve = new PermissionBriefViewModel()
            {
                EntityName = SecureEntity.Requisition,
                Flags = (int)RequisitionPermissions.Approve
            };
            All = new PermissionBriefViewModel()
            {
                EntityName = SecureEntity.Requisition,
                Flags = (int)RequisitionPermissions.All
            };
        }

        /// <summary>
        /// Gets the one and only instance of the <see cref="RequisitionPermission"/> class.
        /// </summary>
        public static RequisitionPermission Instance
        {
            get { return _instance; }
        }

        /// <summary>
        /// Gets an object that indicates permission to view requisition list or details of a requisition.
        /// </summary>
        public PermissionBriefViewModel View { get; private set; }

        /// <summary>
        /// Gets an object that indicates permission to create a new requisition.
        /// </summary>
        public PermissionBriefViewModel Create { get; private set; }

        /// <summary>
        /// Gets an object that indicates permission to edit an existing requisition.
        /// </summary>
        public PermissionBriefViewModel Edit { get; private set; }

        /// <summary>
        /// Gets an object that indicates permission to delete an existing requisition.
        /// </summary>
        public PermissionBriefViewModel Delete { get; private set; }

        /// <summary>
        /// Gets an object that indicates permission to prepare an existing requisition.
        /// </summary>
        public PermissionBriefViewModel Prepare { get; private set; }

        /// <summary>
        /// Gets an object that indicates permission to confirm an existing requisition.
        /// </summary>
        public PermissionBriefViewModel Confirm { get; private set; }

        /// <summary>
        /// Gets an object that indicates permission to approve an existing requisition.
        /// </summary>
        public PermissionBriefViewModel Approve { get; private set; }

        /// <summary>
        /// Gets an object that indicates all permissions for managing a requisition.
        /// </summary>
        public PermissionBriefViewModel All { get; private set; }

        private static RequisitionPermission _instance = new RequisitionPermission();
    }
}
