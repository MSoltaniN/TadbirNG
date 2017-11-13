using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.DataAccess
{
    public partial class ServiceJob
    {
        public ServiceJob()
        {
            RequisitionVoucher = new HashSet<RequisitionVoucher>();
        }

        public int JobId { get; set; }
        public Guid Rowguid { get; set; }
        public DateTime ModifiedDate { get; set; }

        public ICollection<RequisitionVoucher> RequisitionVoucher { get; set; }
    }
}
