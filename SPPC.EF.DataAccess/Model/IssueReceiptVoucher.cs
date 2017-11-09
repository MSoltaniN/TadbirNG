using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.DataAccess
{
    public partial class IssueReceiptVoucher
    {
        public IssueReceiptVoucher()
        {
            InversePricedVoucher = new HashSet<IssueReceiptVoucher>();
            Invoice = new HashSet<Invoice>();
            IssueReceiptVoucherLine = new HashSet<IssueReceiptVoucherLine>();
        }

        public int VoucherId { get; set; }
        public int FiscalPeriodId { get; set; }
        public int BranchId { get; set; }
        public int ActingPartnerId { get; set; }
        public int WarehouseId { get; set; }
        public int PricedVoucherId { get; set; }
        public int PartnerFullAccountId { get; set; }
        public int? PartnerFullDetailId { get; set; }
        public int DocumentId { get; set; }
        public string No { get; set; }
        public bool? IsActive { get; set; }
        public string Reference { get; set; }
        public short Type { get; set; }
        public string Description { get; set; }
        public byte[] Timestamp { get; set; }
        public DateTime ModifiedDate { get; set; }
        public Guid Rowguid { get; set; }

        public BusinessPartner ActingPartner { get; set; }
        public Branch Branch { get; set; }
        public Document Document { get; set; }
        public FiscalPeriod FiscalPeriod { get; set; }
        public FullAccount PartnerFullAccount { get; set; }
        public FullDetail PartnerFullDetail { get; set; }
        public IssueReceiptVoucher PricedVoucher { get; set; }
        public Warehouse Warehouse { get; set; }
        public ICollection<IssueReceiptVoucher> InversePricedVoucher { get; set; }
        public ICollection<Invoice> Invoice { get; set; }
        public ICollection<IssueReceiptVoucherLine> IssueReceiptVoucherLine { get; set; }
    }
}
