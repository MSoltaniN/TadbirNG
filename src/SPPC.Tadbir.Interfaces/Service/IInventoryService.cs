using System;
using System.Collections.Generic;
using SPPC.Tadbir.ViewModel.Inventory;

namespace SPPC.Tadbir.Service
{
    public interface IInventoryService
    {
        IEnumerable<ProductInventoryViewModel> GetProductInventories(int fpId, int branchId);

        ProductInventoryViewModel GetProductInventory(int id);

        void SaveProductInventory(ProductInventoryViewModel inventory);

        void DeleteProductInventory(int id);
    }
}
