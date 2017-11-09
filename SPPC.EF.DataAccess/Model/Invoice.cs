using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.DataAccess
{
    public partial class Invoice
    {
        public Invoice()
        {
            InverseReferenceInvoice = new HashSet<Invoice>();
            InvoiceLine = new HashSet<InvoiceLine>();
        }

        public int InvoiceId { get; set; }
        public int PartnerId { get; set; }
        public int CustomerId { get; set; }
        public int ReferenceInvoiceId { get; set; }
        public int IssueReceiptVoucherId { get; set; }
        public int FiscalPeriodId { get; set; }
        public int BranchId { get; set; }
        public int FullAccountId { get; set; }
        public int? FullDetailId { get; set; }
        public int PartnerFullAccountId { get; set; }
        public int? PartnerFullDetailId { get; set; }
        public int DocumentId { get; set; }
        public string No { get; set; }
        public bool? IsActive { get; set; }
        public bool IsCancelled { get; set; }
        public short Type { get; set; }
        public string Reference { get; set; }
        public DateTime Date { get; set; }
        public double Discount { get; set; }
        public double Expense { get; set; }
        public string ContractNo { get; set; }
        public string ShipmentNo { get; set; }
        public string Description { get; set; }
        public byte[] Timestamp { get; set; }
        public DateTime ModifiedDate { get; set; }
        public Guid Rowguid { get; set; }

        public Branch Branch { get; set; }
        public Customer Customer { get; set; }
        public Document Document { get; set; }
        public FiscalPeriod FiscalPeriod { get; set; }
        public FullAccount FullAccount { get; set; }
        public FullDetail FullDetail { get; set; }
        public IssueReceiptVoucher IssueReceiptVoucher { get; set; }
        public BusinessPartner Partner { get; set; }
        public FullAccount PartnerFullAccount { get; set; }
        public FullDetail PartnerFullDetail { get; set; }
        public Invoice ReferenceInvoice { get; set; }
        public ICollection<Invoice> InverseReferenceInvoice { get; set; }
        public ICollection<InvoiceLine> InvoiceLine { get; set; }
    }
}
