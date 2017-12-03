using System;
using System.Collections.Generic;
using System.Linq;
using SPPC.Framework.Common;
using SPPC.Framework.Persistence;
using SPPC.Framework.Mapper;
using SPPC.Tadbir.Model.Inventory;
using SPPC.Tadbir.ViewModel.Inventory;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات دیتابیسی مربوط به مدیریت موجودی های کالا در انبار را پیاده سازی می کند
    /// </summary>
    public class InventoryRepository : IInventoryRepository
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="unitOfWork">پیاده سازی اینترفیس واحد کاری برای انجام عملیات دیتابیسی </param>
        /// <param name="mapper">نگاشت مورد استفاده برای تبدیل کلاس های مدل اطلاعاتی</param>
        public InventoryRepository(IUnitOfWork unitOfWork, IDomainMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <summary>
        /// اطلاعات موجودی های کالا در انبار را در یک دوره مالی و یک شعبه خاص از دیتابیس خوانده و بر می گرداند
        /// </summary>
        /// <param name="fpId">شناسه دیتابیسی یکی از دوره های مالی موجود</param>
        /// <param name="branchId">شناسه دیتابیسی یکی از شعبه های موجود</param>
        /// <returns>مجموعه ای از اطلاعات نمایشی موجودی های کالا</returns>
        public IList<ProductInventoryViewModel> GetProductInventories(int fpId, int branchId)
        {
            var repository = _unitOfWork.GetRepository<ProductInventory>();
            var inventories = repository
                .GetByCriteria(
                    inv => inv.FiscalPeriod.Id == fpId && inv.Branch.Id == branchId,
                    inv => inv.Product, inv => inv.Uom, inv => inv.Warehouse, inv => inv.FiscalPeriod, inv => inv.Branch)
                .Select(inv => _mapper.Map<ProductInventoryViewModel>(inv))
                .ToList();
            return inventories;
        }

        /// <summary>
        /// اطلاعات موجودی یک کالا در یک انبار را از دیتابیس خوانده و بر می گرداند
        /// </summary>
        /// <param name="inventoryId">شناسه دیتابیسی موجودی کالا در انبار</param>
        /// <returns>اطلاعات نمایشی موجودی کالا در انبار</returns>
        public ProductInventoryViewModel GetProductInventory(int inventoryId)
        {
            ProductInventoryViewModel inventory = default(ProductInventoryViewModel);
            var repository = _unitOfWork.GetRepository<ProductInventory>();
            var existing = repository.GetByID(
                inventoryId,
                inv => inv.Product, inv => inv.Uom, inv => inv.Warehouse, inv => inv.FiscalPeriod, inv => inv.Branch);
            if (existing != null)
            {
                inventory = _mapper.Map<ProductInventoryViewModel>(existing);
            }

            return inventory;
        }

        /// <summary>
        /// اطلاعات موجودی یک کالا در یک انبار را درون دیتابیس ایجاد یا اصلاح می کند
        /// </summary>
        /// <param name="inventory">اطلاعات نمایشی موجودی یک کالا در یک انبار</param>
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
                var existing = repository.GetByID(
                    inventory.Id,
                    inv => inv.Product, inv => inv.Uom, inv => inv.Warehouse, inv => inv.FiscalPeriod, inv => inv.Branch);
                if (existing != null)
                {
                    UpdateExistingInventory(existing, inventory);
                    repository.Update(existing);
                }
            }

            _unitOfWork.Commit();
        }

        /// <summary>
        /// اطلاعات موجودی یک کالا در یک انبار را از دیتابیس حذف می کند
        /// </summary>
        /// <param name="inventoryId">شناسه دیتابیسی موجودی کالا در انبار</param>
        public void DeleteProductInventory(int inventoryId)
        {
            var repository = _unitOfWork.GetRepository<ProductInventory>();
            var existing = repository.GetByID(inventoryId);
            if (existing != null)
            {
                repository.Delete(existing);
            }

            _unitOfWork.Commit();
        }

        private void UpdateExistingInventory(ProductInventory existing, ProductInventoryViewModel inventory)
        {
            existing.Quantity = inventory.Quantity;
            existing.Product = _unitOfWork.GetRepository<Product>().GetByID(inventory.ProductId);
            existing.Uom = _unitOfWork.GetRepository<UnitOfMeasurement>().GetByID(inventory.UomId);
            existing.Warehouse = _unitOfWork.GetRepository<Warehouse>().GetByID(inventory.WarehouseId);
        }

        private IUnitOfWork _unitOfWork;
        private IDomainMapper _mapper;
    }
}
