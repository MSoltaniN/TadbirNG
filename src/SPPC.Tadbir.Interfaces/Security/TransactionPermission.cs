using System;
using System.Collections.Generic;
using SPPC.Tadbir.ViewModel.Auth;

namespace SPPC.Tadbir.Security
{
    /// <summary>
    /// Exposes all permissions currently applicable to managing a financial transaction. This is a singleton class.
    /// </summary>
    public sealed class TransactionPermission
    {
        private TransactionPermission()
        {
            View = new PermissionBriefViewModel()
            {
                EntityName = SecureEntity.Transaction,
                Flags = (int)TransactionPermissions.View
            };

            Create = new PermissionBriefViewModel()
            {
                EntityName = SecureEntity.Transaction,
                Flags = (int)TransactionPermissions.Create
            };

            Edit = new PermissionBriefViewModel()
            {
                EntityName = SecureEntity.Transaction,
                Flags = (int)TransactionPermissions.Edit
            };

            Delete = new PermissionBriefViewModel()
            {
                EntityName = SecureEntity.Transaction,
                Flags = (int)TransactionPermissions.Delete
            };

            All = new PermissionBriefViewModel()
            {
                EntityName = SecureEntity.Transaction,
                Flags = (int)TransactionPermissions.All
            };
        }

        /// <summary>
        /// Gets the one and only instance of the <see cref="TransactionPermission"/> class.
        /// </summary>
        public static TransactionPermission Instance
        {
            get { return _instance; }
        }

        /// <summary>
        /// Gets an object that indicates permission to view transaction list or details of a transaction.
        /// </summary>
        public PermissionBriefViewModel View { get; private set; }

        /// <summary>
        /// Gets an object that indicates permission to create a new transaction.
        /// </summary>
        public PermissionBriefViewModel Create { get; private set; }

        /// <summary>
        /// Gets an object that indicates permission to edit an existing transaction.
        /// </summary>
        public PermissionBriefViewModel Edit { get; private set; }

        /// <summary>
        /// Gets an object that indicates permission to delete an existing transaction.
        /// </summary>
        public PermissionBriefViewModel Delete { get; private set; }

        /// <summary>
        /// Gets an object that indicates all permissions for managing a transaction.
        /// </summary>
        public PermissionBriefViewModel All { get; private set; }

        private static TransactionPermission _instance = new TransactionPermission();
    }
}
