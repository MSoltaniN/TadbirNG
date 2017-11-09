using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.DataAccess
{
    public partial class CostCenter
    {
        public CostCenter()
        {
            FullAccount = new HashSet<FullAccount>();
            InverseParent = new HashSet<CostCenter>();
        }

        public int CostCenterId { get; set; }
        public int? ParentId { get; set; }
        public string Code { get; set; }
        public string FullCode { get; set; }
        public string Name { get; set; }
        public short Level { get; set; }
        public Guid Rowguid { get; set; }
        public DateTime ModifiedDate { get; set; }

        public CostCenter Parent { get; set; }
        public ICollection<FullAccount> FullAccount { get; set; }
        public ICollection<CostCenter> InverseParent { get; set; }
    }
}
