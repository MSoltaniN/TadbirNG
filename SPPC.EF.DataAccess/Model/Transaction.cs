using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.DataAccess
{
    public partial class Transaction
    {
        public Transaction()
        {
            TransactionLine = new HashSet<TransactionLine>();
        }

        public int TransactionId { get; set; }
        public int FiscalPeriodId { get; set; }
        public int BranchId { get; set; }
        public int DocumentId { get; set; }
        public string No { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public Guid Rowguid { get; set; }
        public DateTime ModifiedDate { get; set; }

        public Branch Branch { get; set; }
        public Document Document { get; set; }
        public FiscalPeriod FiscalPeriod { get; set; }
        public ICollection<TransactionLine> TransactionLine { get; set; }
    }
}
