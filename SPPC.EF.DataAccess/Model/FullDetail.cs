using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.DataAccess
{
    public partial class FullDetail
    {
        public FullDetail()
        {
            InvoiceFullDetail = new HashSet<Invoice>();
            InvoiceLine = new HashSet<InvoiceLine>();
            InvoicePartnerFullDetail = new HashSet<Invoice>();
            IssueReceiptVoucher = new HashSet<IssueReceiptVoucher>();
            IssueReceiptVoucherLine = new HashSet<IssueReceiptVoucherLine>();
            RequisitionVoucher = new HashSet<RequisitionVoucher>();
            RequisitionVoucherLine = new HashSet<RequisitionVoucherLine>();
        }

        public int FullDetailId { get; set; }
        public int? TypeId { get; set; }
        public int Detail2Id { get; set; }
        public int? Detail3Id { get; set; }
        public int? Detail4Id { get; set; }
        public int? Detail5Id { get; set; }
        public int? Detail6Id { get; set; }
        public int? Detail7Id { get; set; }
        public int? Detail8Id { get; set; }
        public int? Detail9Id { get; set; }
        public Guid Rowguid { get; set; }
        public DateTime ModifiedDate { get; set; }

        public DetailAccount Detail2 { get; set; }
        public DetailAccount Detail3 { get; set; }
        public DetailAccount Detail4 { get; set; }
        public DetailAccount Detail5 { get; set; }
        public DetailAccount Detail6 { get; set; }
        public DetailAccount Detail7 { get; set; }
        public DetailAccount Detail8 { get; set; }
        public DetailAccount Detail9 { get; set; }
        public FullDetailType Type { get; set; }
        public ICollection<Invoice> InvoiceFullDetail { get; set; }
        public ICollection<InvoiceLine> InvoiceLine { get; set; }
        public ICollection<Invoice> InvoicePartnerFullDetail { get; set; }
        public ICollection<IssueReceiptVoucher> IssueReceiptVoucher { get; set; }
        public ICollection<IssueReceiptVoucherLine> IssueReceiptVoucherLine { get; set; }
        public ICollection<RequisitionVoucher> RequisitionVoucher { get; set; }
        public ICollection<RequisitionVoucherLine> RequisitionVoucherLine { get; set; }
    }
}
