using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.DataAccess
{
    public partial class Currency
    {
        public Currency()
        {
            InvoiceLine = new HashSet<InvoiceLine>();
            IssueReceiptVoucherLine = new HashSet<IssueReceiptVoucherLine>();
            TransactionLine = new HashSet<TransactionLine>();
        }

        public int CurrencyId { get; set; }
        public string Name { get; set; }
        public Guid Rowguid { get; set; }
        public DateTime ModifiedDate { get; set; }

        public ICollection<InvoiceLine> InvoiceLine { get; set; }
        public ICollection<IssueReceiptVoucherLine> IssueReceiptVoucherLine { get; set; }
        public ICollection<TransactionLine> TransactionLine { get; set; }
    }
}
