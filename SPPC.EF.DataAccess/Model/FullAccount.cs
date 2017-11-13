using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.DataAccess
{
    public partial class FullAccount
    {
        public FullAccount()
        {
            InvoiceFullAccount = new HashSet<Invoice>();
            InvoiceLine = new HashSet<InvoiceLine>();
            InvoicePartnerFullAccount = new HashSet<Invoice>();
            IssueReceiptVoucher = new HashSet<IssueReceiptVoucher>();
            IssueReceiptVoucherLine = new HashSet<IssueReceiptVoucherLine>();
            RequisitionVoucher = new HashSet<RequisitionVoucher>();
            RequisitionVoucherLine = new HashSet<RequisitionVoucherLine>();
        }

        public int FullAccountId { get; set; }
        public int AccountId { get; set; }
        public int? DetailId { get; set; }
        public int? CostCenterId { get; set; }
        public int? ProjectId { get; set; }
        public Guid Rowguid { get; set; }
        public DateTime ModifiedDate { get; set; }

        public Account Account { get; set; }
        public CostCenter CostCenter { get; set; }
        public DetailAccount Detail { get; set; }
        public Project Project { get; set; }
        public ICollection<Invoice> InvoiceFullAccount { get; set; }
        public ICollection<InvoiceLine> InvoiceLine { get; set; }
        public ICollection<Invoice> InvoicePartnerFullAccount { get; set; }
        public ICollection<IssueReceiptVoucher> IssueReceiptVoucher { get; set; }
        public ICollection<IssueReceiptVoucherLine> IssueReceiptVoucherLine { get; set; }
        public ICollection<RequisitionVoucher> RequisitionVoucher { get; set; }
        public ICollection<RequisitionVoucherLine> RequisitionVoucherLine { get; set; }
    }
}
