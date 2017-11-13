using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.DataAccess
{
    public partial class RequisitionVoucherLine
    {
        public int LineId { get; set; }
        public int VoucherId { get; set; }
        public int WarehouseId { get; set; }
        public int ProductId { get; set; }
        public int UomId { get; set; }
        public int BranchId { get; set; }
        public int FiscalPeriodId { get; set; }
        public int FullAccountId { get; set; }
        public int? FullDetailId { get; set; }
        public int ActionId { get; set; }
        public int No { get; set; }
        public double OrderedQuantity { get; set; }
        public double? DeliveredQuantity { get; set; }
        public double? ReservedQuantity { get; set; }
        public double? LastOrderedQuantity { get; set; }
        public DateTime RequiredDate { get; set; }
        public DateTime? PromisedDate { get; set; }
        public DateTime? DeliveredDate { get; set; }
        public DateTime? LastOrderedDate { get; set; }
        public bool? IsActive { get; set; }
        public string Description { get; set; }
        public byte[] Timestamp { get; set; }
        public DateTime ModifiedDate { get; set; }
        public Guid Rowguid { get; set; }

        public DocumentAction Action { get; set; }
        public Branch Branch { get; set; }
        public FiscalPeriod FiscalPeriod { get; set; }
        public FullAccount FullAccount { get; set; }
        public FullDetail FullDetail { get; set; }
        public Product Product { get; set; }
        public Uom Uom { get; set; }
        public RequisitionVoucher Voucher { get; set; }
        public Warehouse Warehouse { get; set; }
    }
}
