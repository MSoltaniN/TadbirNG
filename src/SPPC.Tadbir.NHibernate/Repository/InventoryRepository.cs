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

        public void SaveProductInventory(ProductInventoryViewModel inventory)
        {
            Verify.ArgumentNotNull(inventory, "inventory");
            var repository = _unitOfWork.GetRepository<ProductInventory>();
            if (inventory.Id == 0)
            {
                var newInventory = _mapper.Map<ProductInventory>(inventory);
                repository.Insert(newInventory);
                _unitOfWork.Commit();
            }
        }

        private IUnitOfWork _unitOfWork;
        private IDomainMapper _mapper;
    }
}
