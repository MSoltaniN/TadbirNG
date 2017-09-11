using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Api
{
    /// <summary>
    /// Defines API server routes and API client operation URLs for working with product inventory in warehouses.
    /// </summary>
    public sealed class InventoryApi
    {
        private InventoryApi()
        {
        }

        /// <summary>
        /// API client URL for all product inventories defined in a fiscal period and a corporate branch
        /// </summary>
        public const string FiscalPeriodBranchInventories = "inventories/fp/{0}/branch/{1}";

        /// <summary>
        /// API server route URL for all product inventories defined in a fiscal period and a corporate branch
        /// </summary>
        public const string FiscalPeriodBranchInventoriesUrl = "inventories/fp/{fpId:int}/branch/{branchId:int}";

        /// <summary>
        /// API client URL for all product inventories
        /// </summary>
        public const string Inventories = "inventories";

        /// <summary>
        /// API server route URL for all product inventories
        /// </summary>
        public const string InventoriesUrl = "inventories";

        /// <summary>
        /// API client URL for a single product inventory specified by identifier
        /// </summary>
        public const string Inventory = "inventories/{0}";

        /// <summary>
        /// API server route URL for a single product inventory specified by identifier
        /// </summary>
        public const string InventoryUrl = "inventories/{inventoryId:int}";
    }
}
