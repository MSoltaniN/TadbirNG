using System;
using System.Collections.Generic;
using System.Linq;
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
                InventoryApi.ProductInventories, fpId, branchId);
            return inventories;
        }

        private IApiClient _apiClient;
    }
}
