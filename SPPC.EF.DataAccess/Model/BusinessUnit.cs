using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.DataAccess
{
    public partial class BusinessUnit
    {
        public BusinessUnit()
        {
            RequisitionVoucherReceiverUnit = new HashSet<RequisitionVoucher>();
            RequisitionVoucherRequesterUnit = new HashSet<RequisitionVoucher>();
        }

        public int UnitId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid Rowguid { get; set; }
        public DateTime ModifiedDate { get; set; }

        public ICollection<RequisitionVoucher> RequisitionVoucherReceiverUnit { get; set; }
        public ICollection<RequisitionVoucher> RequisitionVoucherRequesterUnit { get; set; }
    }
}
