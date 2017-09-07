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

        public ProductInventoryViewModel GetProductInventory(int id)
        {
            var inventory = _apiClient.Get<ProductInventoryViewModel>(InventoryApi.Inventory, id);
            return inventory;
        }

        public void SaveProductInventory(ProductInventoryViewModel inventory)
        {
            Verify.ArgumentNotNull(inventory, "inventory");
            if (inventory.Id == 0)
            {
                _apiClient.Insert(inventory, InventoryApi.Inventories);
            }
            else
            {
                _apiClient.Update(inventory, InventoryApi.Inventory, inventory.Id);
            }
        }

        private IApiClient _apiClient;
    }
}
