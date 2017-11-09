using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.DataAccess
{
    public partial class BusinessPartner
    {
        public BusinessPartner()
        {
            Invoice = new HashSet<Invoice>();
            IssueReceiptVoucher = new HashSet<IssueReceiptVoucher>();
            RequisitionVoucherReceiver = new HashSet<RequisitionVoucher>();
            RequisitionVoucherRequester = new HashSet<RequisitionVoucher>();
        }

        public int PartnerId { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string CommerceCode { get; set; }
        public string Address { get; set; }
        public Guid Rowguid { get; set; }
        public DateTime ModifiedDate { get; set; }

        public ICollection<Invoice> Invoice { get; set; }
        public ICollection<IssueReceiptVoucher> IssueReceiptVoucher { get; set; }
        public ICollection<RequisitionVoucher> RequisitionVoucherReceiver { get; set; }
        public ICollection<RequisitionVoucher> RequisitionVoucherRequester { get; set; }
    }
}
