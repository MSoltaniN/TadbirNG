using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.DataAccess
{
    public partial class DetailAccount
    {
        public DetailAccount()
        {
            FullAccount = new HashSet<FullAccount>();
            FullDetailDetail2 = new HashSet<FullDetail>();
            FullDetailDetail3 = new HashSet<FullDetail>();
            FullDetailDetail4 = new HashSet<FullDetail>();
            FullDetailDetail5 = new HashSet<FullDetail>();
            FullDetailDetail6 = new HashSet<FullDetail>();
            FullDetailDetail7 = new HashSet<FullDetail>();
            FullDetailDetail8 = new HashSet<FullDetail>();
            FullDetailDetail9 = new HashSet<FullDetail>();
            InverseParent = new HashSet<DetailAccount>();
        }

        public int DetailId { get; set; }
        public int? ParentId { get; set; }
        public string Code { get; set; }
        public string FullCode { get; set; }
        public string Name { get; set; }
        public short Level { get; set; }
        public Guid Rowguid { get; set; }
        public DateTime ModifiedDate { get; set; }

        public DetailAccount Parent { get; set; }
        public ICollection<FullAccount> FullAccount { get; set; }
        public ICollection<FullDetail> FullDetailDetail2 { get; set; }
        public ICollection<FullDetail> FullDetailDetail3 { get; set; }
        public ICollection<FullDetail> FullDetailDetail4 { get; set; }
        public ICollection<FullDetail> FullDetailDetail5 { get; set; }
        public ICollection<FullDetail> FullDetailDetail6 { get; set; }
        public ICollection<FullDetail> FullDetailDetail7 { get; set; }
        public ICollection<FullDetail> FullDetailDetail8 { get; set; }
        public ICollection<FullDetail> FullDetailDetail9 { get; set; }
        public ICollection<DetailAccount> InverseParent { get; set; }
    }
}
