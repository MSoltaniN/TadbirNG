using System;
using System.Collections.Generic;
using BabakSoft.Platform.Common;
using SPPC.Framework.Service;
using SPPC.Tadbir.Api;
using SPPC.Tadbir.ViewModel.Inventory;

namespace SPPC.Tadbir.Service
{
    public class InventoryService : IInventoryService
    {
        public InventoryService(IApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public IEnumerable<ProductInventoryViewModel> GetProductInventories(int fpId, int branchId)
        {
            var inventories = _apiClient.Get<IEnumerable<ProductInventoryViewModel>>(
                InventoryApi.FiscalPeriodBranchInventories, fpId, branchId);
            return inventories;
        }

        public void SaveProductInventory(ProductInventoryViewModel inventory)
        {
            Verify.ArgumentNotNull(inventory, "inventory");
            if (inventory.Id == 0)
            {
                _apiClient.Insert(inventory, InventoryApi.Inventories);
            }
        }

        private IApiClient _apiClient;
    }
}
