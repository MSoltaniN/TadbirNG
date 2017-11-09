using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.DataAccess
{
    public partial class InvoiceLine
    {
        public int LineId { get; set; }
        public int InvoiceId { get; set; }
        public int WarehouseId { get; set; }
        public int ProductId { get; set; }
        public int UomId { get; set; }
        public int CurrencyId { get; set; }
        public int RequisitionVoucherId { get; set; }
        public int BranchId { get; set; }
        public int FiscalPeriodId { get; set; }
        public int FullAccountId { get; set; }
        public int? FullDetailId { get; set; }
        public int No { get; set; }
        public double Quantity { get; set; }
        public double UnitPrice { get; set; }
        public double? CurrencyUnitPrice { get; set; }
        public double? Discount { get; set; }
        public double? UnitCost { get; set; }
        public bool? IsActive { get; set; }
        public string Description { get; set; }
        public byte[] Timestamp { get; set; }
        public DateTime ModifiedDate { get; set; }
        public Guid Rowguid { get; set; }

        public Branch Branch { get; set; }
        public Currency Currency { get; set; }
        public FiscalPeriod FiscalPeriod { get; set; }
        public FullAccount FullAccount { get; set; }
        public FullDetail FullDetail { get; set; }
        public Invoice Invoice { get; set; }
        public Product Product { get; set; }
        public RequisitionVoucher RequisitionVoucher { get; set; }
        public Uom Uom { get; set; }
        public Warehouse Warehouse { get; set; }
    }
}
