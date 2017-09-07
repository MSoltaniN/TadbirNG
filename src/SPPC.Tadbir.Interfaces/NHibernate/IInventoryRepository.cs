using System;
using System.Collections.Generic;
using SPPC.Tadbir.ViewModel.Inventory;

namespace SPPC.Tadbir.NHibernate
{
    public interface IInventoryRepository
    {
        IList<ProductInventoryViewModel> GetProductInventories(int fpId, int branchId);

        ProductInventoryViewModel GetProductInventory(int inventoryId);

        void SaveProductInventory(ProductInventoryViewModel inventory);
    }
}
