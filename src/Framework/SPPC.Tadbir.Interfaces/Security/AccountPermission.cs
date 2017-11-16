using System;
using System.Collections.Generic;
using SPPC.Tadbir.ViewModel.Auth;

namespace SPPC.Tadbir.Security
{
    /// <summary>
    /// Exposes all permissions currently applicable to managing a financial account. This is a singleton class.
    /// </summary>
    public sealed class AccountPermission
    {
        private AccountPermission()
        {
            View = new PermissionBriefViewModel()
            {
                EntityName = SecureEntity.Account,
                Flags = (int)AccountPermissions.View
            };
            Create = new PermissionBriefViewModel()
            {
                EntityName = SecureEntity.Account,
                Flags = (int)AccountPermissions.Create
            };
            Edit = new PermissionBriefViewModel()
            {
                EntityName = SecureEntity.Account,
                Flags = (int)AccountPermissions.Edit
            };
            Delete = new PermissionBriefViewModel()
            {
                EntityName = SecureEntity.Account,
                Flags = (int)AccountPermissions.Delete
            };
            All = new PermissionBriefViewModel()
            {
                EntityName = SecureEntity.Account,
                Flags = (int)AccountPermissions.All
            };
        }

        /// <summary>
        /// Gets the one and only instance of the <see cref="AccountPermission"/> class.
        /// </summary>
        public static AccountPermission Instance
        {
            get { return _instance; }
        }

        /// <summary>
        /// Gets an object that indicates permission to view account list or details of a account.
        /// </summary>
        public PermissionBriefViewModel View { get; private set; }

        /// <summary>
        /// Gets an object that indicates permission to create a new account.
        /// </summary>
        public PermissionBriefViewModel Create { get; private set; }

        /// <summary>
        /// Gets an object that indicates permission to edit an existing account.
        /// </summary>
        public PermissionBriefViewModel Edit { get; private set; }

        /// <summary>
        /// Gets an object that indicates permission to delete an existing account.
        /// </summary>
        public PermissionBriefViewModel Delete { get; private set; }

        /// <summary>
        /// Gets an object that indicates all permissions for managing an account.
        /// </summary>
        public PermissionBriefViewModel All { get; private set; }

        private static AccountPermission _instance = new AccountPermission();
    }
}
