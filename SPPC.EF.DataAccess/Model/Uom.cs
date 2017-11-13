using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.DataAccess
{
    public partial class Uom
    {
        public Uom()
        {
            InvoiceLine = new HashSet<InvoiceLine>();
            IssueReceiptVoucherLine = new HashSet<IssueReceiptVoucherLine>();
            RequisitionVoucherLine = new HashSet<RequisitionVoucherLine>();
        }

        public int UomId { get; set; }
        public string Name { get; set; }
        public Guid Rowguid { get; set; }
        public DateTime ModifiedDate { get; set; }

        public ICollection<InvoiceLine> InvoiceLine { get; set; }
        public ICollection<IssueReceiptVoucherLine> IssueReceiptVoucherLine { get; set; }
        public ICollection<RequisitionVoucherLine> RequisitionVoucherLine { get; set; }
    }
}
