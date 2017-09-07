using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Api
{
    public sealed class InventoryApi
    {
        private InventoryApi()
        {
        }

        public const string FiscalPeriodBranchInventories = "inventories/fp/{0}/branch/{1}";
        public const string FiscalPeriodBranchInventoriesUrl = "inventories/fp/{fpId:int}/branch/{branchId:int}";
        public const string Inventories = "inventories";
        public const string InventoriesUrl = "inventories";
        public const string Inventory = "inventories/{0}";
        public const string InventoryUrl = "inventories/{inventoryId:int}";
    }
}
