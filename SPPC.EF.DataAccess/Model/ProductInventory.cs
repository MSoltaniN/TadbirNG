using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.DataAccess
{
    public partial class ProductInventory
    {
        public int ProductInventoryId { get; set; }
        public int ProductId { get; set; }
        public int UomId { get; set; }
        public int WarehouseId { get; set; }
        public int FiscalPeriodId { get; set; }
        public int BranchId { get; set; }
        public double Quantity { get; set; }
        public Guid Rowguid { get; set; }
        public DateTime ModifiedDate { get; set; }

        public Branch Branch { get; set; }
        public FiscalPeriod FiscalPeriod { get; set; }
        public Product Product { get; set; }
        public Warehouse Warehouse { get; set; }
    }
}
