using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.DataAccess
{
    public partial class Project
    {
        public Project()
        {
            FullAccount = new HashSet<FullAccount>();
            InverseParent = new HashSet<Project>();
        }

        public int ProjectId { get; set; }
        public int? ParentId { get; set; }
        public string Code { get; set; }
        public string FullCode { get; set; }
        public string Name { get; set; }
        public short Level { get; set; }
        public Guid Rowguid { get; set; }
        public DateTime ModifiedDate { get; set; }

        public Project Parent { get; set; }
        public ICollection<FullAccount> FullAccount { get; set; }
        public ICollection<Project> InverseParent { get; set; }
    }
}
