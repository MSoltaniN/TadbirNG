using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.DataAccess
{
    public partial class RequisitionVoucher
    {
        public RequisitionVoucher()
        {
            InvoiceLine = new HashSet<InvoiceLine>();
            IssueReceiptVoucherLine = new HashSet<IssueReceiptVoucherLine>();
            RequisitionVoucherLine = new HashSet<RequisitionVoucherLine>();
        }

        public int VoucherId { get; set; }
        public int VoucherTypeId { get; set; }
        public int FiscalPeriodId { get; set; }
        public int BranchId { get; set; }
        public int RequesterId { get; set; }
        public int ReceiverId { get; set; }
        public int RequesterUnitId { get; set; }
        public int ReceiverUnitId { get; set; }
        public int WarehouseId { get; set; }
        public int? ServiceJobId { get; set; }
        public int FullAccountId { get; set; }
        public int? FullDetailId { get; set; }
        public int DocumentId { get; set; }
        public string No { get; set; }
        public string Reference { get; set; }
        public DateTime OrderedDate { get; set; }
        public DateTime? RequiredDate { get; set; }
        public DateTime? PromisedDate { get; set; }
        public string Reason { get; set; }
        public string WarehouseComment { get; set; }
        public bool? IsActive { get; set; }
        public string Description { get; set; }
        public byte[] Timestamp { get; set; }
        public DateTime ModifiedDate { get; set; }
        public Guid Rowguid { get; set; }

        public Branch Branch { get; set; }
        public Document Document { get; set; }
        public FiscalPeriod FiscalPeriod { get; set; }
        public FullAccount FullAccount { get; set; }
        public FullDetail FullDetail { get; set; }
        public BusinessPartner Receiver { get; set; }
        public BusinessUnit ReceiverUnit { get; set; }
        public BusinessPartner Requester { get; set; }
        public BusinessUnit RequesterUnit { get; set; }
        public ServiceJob ServiceJob { get; set; }
        public RequisitionVoucherType VoucherType { get; set; }
        public Warehouse Warehouse { get; set; }
        public ICollection<InvoiceLine> InvoiceLine { get; set; }
        public ICollection<IssueReceiptVoucherLine> IssueReceiptVoucherLine { get; set; }
        public ICollection<RequisitionVoucherLine> RequisitionVoucherLine { get; set; }
    }
}
