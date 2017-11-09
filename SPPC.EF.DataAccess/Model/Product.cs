using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.DataAccess
{
    public partial class Product
    {
        public Product()
        {
            InvoiceLine = new HashSet<InvoiceLine>();
            IssueReceiptVoucherLine = new HashSet<IssueReceiptVoucherLine>();
            ProductInventory = new HashSet<ProductInventory>();
            RequisitionVoucherLine = new HashSet<RequisitionVoucherLine>();
        }

        public int ProductId { get; set; }
        public int CategoryId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public Guid Rowguid { get; set; }
        public DateTime ModifiedDate { get; set; }

        public ProductCategory Category { get; set; }
        public ICollection<InvoiceLine> InvoiceLine { get; set; }
        public ICollection<IssueReceiptVoucherLine> IssueReceiptVoucherLine { get; set; }
        public ICollection<ProductInventory> ProductInventory { get; set; }
        public ICollection<RequisitionVoucherLine> RequisitionVoucherLine { get; set; }
    }
}
