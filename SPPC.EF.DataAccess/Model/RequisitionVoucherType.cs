using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.DataAccess
{
    public partial class RequisitionVoucherType
    {
        public RequisitionVoucherType()
        {
            RequisitionVoucher = new HashSet<RequisitionVoucher>();
        }

        public int VoucherTypeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid Rowguid { get; set; }
        public DateTime ModifiedDate { get; set; }

        public ICollection<RequisitionVoucher> RequisitionVoucher { get; set; }
    }
}
