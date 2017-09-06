using System;
using System.Collections.Generic;
using SPPC.Tadbir.ViewModel.Auth;

namespace SPPC.Tadbir.Security
{
    /// <summary>
    /// Exposes all permissions currently applicable to managing a product inventory. This is a singleton class.
    /// </summary>
    public sealed class ProductInventoryPermission
    {
        private ProductInventoryPermission()
        {
            View = new PermissionBriefViewModel()
            {
                EntityName = SecureEntity.ProductInventory,
                Flags = (int)ProductInventoryPermissions.View
            };
            Create = new PermissionBriefViewModel()
            {
                EntityName = SecureEntity.ProductInventory,
                Flags = (int)ProductInventoryPermissions.Create
            };
            Edit = new PermissionBriefViewModel()
            {
                EntityName = SecureEntity.ProductInventory,
                Flags = (int)ProductInventoryPermissions.Edit
            };
            Delete = new PermissionBriefViewModel()
            {
                EntityName = SecureEntity.ProductInventory,
                Flags = (int)ProductInventoryPermissions.Delete
            };
            All = new PermissionBriefViewModel()
            {
                EntityName = SecureEntity.ProductInventory,
                Flags = (int)ProductInventoryPermissions.All
            };
        }

        /// <summary>
        /// Gets the one and only instance of the <see cref="ProductInventoryPermission"/> class.
        /// </summary>
        public static ProductInventoryPermission Instance
        {
            get { return _instance; }
        }

        /// <summary>
        /// Gets an object that indicates permission to view product inventory list or details of a product inventory.
        /// </summary>
        public PermissionBriefViewModel View { get; private set; }

        /// <summary>
        /// Gets an object that indicates permission to create a new product inventory.
        /// </summary>
        public PermissionBriefViewModel Create { get; private set; }

        /// <summary>
        /// Gets an object that indicates permission to edit an existing product inventory.
        /// </summary>
        public PermissionBriefViewModel Edit { get; private set; }

        /// <summary>
        /// Gets an object that indicates permission to delete an existing product inventory.
        /// </summary>
        public PermissionBriefViewModel Delete { get; private set; }

        /// <summary>
        /// Gets an object that indicates all permissions for managing a product inventory.
        /// </summary>
        public PermissionBriefViewModel All { get; private set; }

        private static ProductInventoryPermission _instance = new ProductInventoryPermission();
    }
}
