using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.DataAccess
{
    public partial class TransactionLine
    {
        public int LineId { get; set; }
        public int TransactionId { get; set; }
        public int FiscalPeriodId { get; set; }
        public int BranchId { get; set; }
        public int AccountId { get; set; }
        public int CurrencyId { get; set; }
        public string Description { get; set; }
        public decimal Debit { get; set; }
        public decimal Credit { get; set; }
        public Guid Rowguid { get; set; }
        public DateTime ModifiedDate { get; set; }

        public Account Account { get; set; }
        public Branch Branch { get; set; }
        public Currency Currency { get; set; }
        public FiscalPeriod FiscalPeriod { get; set; }
        public Transaction Transaction { get; set; }
    }
}
