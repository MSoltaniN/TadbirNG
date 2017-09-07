using System;
using System.Collections.Generic;
using System.Linq;
using BabakSoft.Platform.Common;
using BabakSoft.Platform.Persistence;
using SPPC.Framework.Mapper;
using SPPC.Tadbir.Model.Inventory;
using SPPC.Tadbir.ViewModel.Inventory;

namespace SPPC.Tadbir.NHibernate
{
    public class InventoryRepository : IInventoryRepository
    {
        public InventoryRepository(IUnitOfWork unitOfWork, IDomainMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public IList<ProductInventoryViewModel> GetProductInventories(int fpId, int branchId)
        {
            var repository = _unitOfWork.GetRepository<ProductInventory>();
            var inventories = repository
                .GetByCriteria(inv => inv.FiscalPeriod.Id == fpId && inv.Branch.Id == branchId)
                .Select(inv => _mapper.Map<ProductInventoryViewModel>(inv))
                .ToList();
            return inventories;
        }

        public ProductInventoryViewModel GetProductInventory(int inventoryId)
        {
            ProductInventoryViewModel inventory = default(ProductInventoryViewModel);
            var repository = _unitOfWork.GetRepository<ProductInventory>();
            var existing = repository.GetByID(inventoryId);
            if (existing != null)
            {
                inventory = _mapper.Map<ProductInventoryViewModel>(existing);
            }

            return inventory;
        }

        public void SaveProductInventory(ProductInventoryViewModel inventory)
        {
            Verify.ArgumentNotNull(inventory, "inventory");
            var repository = _unitOfWork.GetRepository<ProductInventory>();
            if (inventory.Id == 0)
            {
                var newInventory = _mapper.Map<ProductInventory>(inventory);
                repository.Insert(newInventory);
            }
            else
            {
                var existing = repository.GetByID(inventory.Id);
                if (existing != null)
                {
                    UpdateExistingInventory(existing, inventory);
                    repository.Update(existing);
                }
            }

            _unitOfWork.Commit();
        }

        private static void UpdateExistingInventory(ProductInventory existing, ProductInventoryViewModel inventory)
        {
            existing.Quantity = inventory.Quantity;
            existing.Product = new Product() { Id = inventory.ProductId };
            existing.Uom = new UnitOfMeasurement() { Id = inventory.UomId };
            existing.Warehouse = new Warehouse() { Id = inventory.WarehouseId };
        }

        private IUnitOfWork _unitOfWork;
        private IDomainMapper _mapper;
    }
}
