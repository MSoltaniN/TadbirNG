using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Api
{
    public sealed class InventoryApi
    {
        private InventoryApi()
        {
        }

        public const string ProductInventories = "inventories/fp/{0}/branch/{1}";
        public const string ProductInventoriesUrl = "inventories/fp/{fpId:int}/branch/{branchId:int}";
    }
}
