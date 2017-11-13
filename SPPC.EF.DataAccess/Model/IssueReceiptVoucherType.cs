using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.DataAccess
{
    public partial class IssueReceiptVoucherType
    {
        public int VoucherTypeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid Rowguid { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
