using System;
using SPPC.Tadbir.ViewModel.Auth;

namespace SPPC.Tadbir.Security
{
    /// <summary>
    /// Exposes all permissions currently applicable to managing a currency. This is a singleton class.
    /// </summary>
    public sealed class CurrencyPermission
    {
        private CurrencyPermission()
        {
            View = new PermissionBriefViewModel()
            {
                EntityName = SecureEntity.Currency,
                Flags = (int)CurrencyPermissions.View
            };
            Create = new PermissionBriefViewModel()
            {
                EntityName = SecureEntity.Currency,
                Flags = (int)CurrencyPermissions.Create
            };
            Edit = new PermissionBriefViewModel()
            {
                EntityName = SecureEntity.Currency,
                Flags = (int)CurrencyPermissions.Edit
            };
            Delete = new PermissionBriefViewModel()
            {
                EntityName = SecureEntity.Currency,
                Flags = (int)CurrencyPermissions.Delete
            };
            All = new PermissionBriefViewModel()
            {
                EntityName = SecureEntity.Currency,
                Flags = (int)CurrencyPermissions.All
            };
        }

        /// <summary>
        /// Gets the one and only instance of the <see cref="CurrencyPermission"/> class.
        /// </summary>
        public static CurrencyPermission Instance
        {
            get { return _instance; }
        }

        /// <summary>
        /// Gets an object that indicates permission to view currency list or details of a currency.
        /// </summary>
        public PermissionBriefViewModel View { get; private set; }

        /// <summary>
        /// Gets an object that indicates permission to create a new currency.
        /// </summary>
        public PermissionBriefViewModel Create { get; private set; }

        /// <summary>
        /// Gets an object that indicates permission to edit an existing currency.
        /// </summary>
        public PermissionBriefViewModel Edit { get; private set; }

        /// <summary>
        /// Gets an object that indicates permission to delete an existing currency.
        /// </summary>
        public PermissionBriefViewModel Delete { get; private set; }

        /// <summary>
        /// Gets an object that indicates all permissions for managing currencies.
        /// </summary>
        public PermissionBriefViewModel All { get; private set; }

        private static CurrencyPermission _instance = new CurrencyPermission();
    }
}
