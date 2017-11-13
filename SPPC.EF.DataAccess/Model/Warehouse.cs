using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.DataAccess
{
    public partial class Warehouse
    {
        public Warehouse()
        {
            InvoiceLine = new HashSet<InvoiceLine>();
            IssueReceiptVoucher = new HashSet<IssueReceiptVoucher>();
            IssueReceiptVoucherLine = new HashSet<IssueReceiptVoucherLine>();
            ProductInventory = new HashSet<ProductInventory>();
            RequisitionVoucher = new HashSet<RequisitionVoucher>();
            RequisitionVoucherLine = new HashSet<RequisitionVoucherLine>();
        }

        public int WarehouseId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public Guid Rowguid { get; set; }
        public DateTime ModifiedDate { get; set; }

        public ICollection<InvoiceLine> InvoiceLine { get; set; }
        public ICollection<IssueReceiptVoucher> IssueReceiptVoucher { get; set; }
        public ICollection<IssueReceiptVoucherLine> IssueReceiptVoucherLine { get; set; }
        public ICollection<ProductInventory> ProductInventory { get; set; }
        public ICollection<RequisitionVoucher> RequisitionVoucher { get; set; }
        public ICollection<RequisitionVoucherLine> RequisitionVoucherLine { get; set; }
    }
}
