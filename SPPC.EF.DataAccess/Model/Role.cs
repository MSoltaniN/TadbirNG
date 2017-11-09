using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.DataAccess
{
    public partial class Role
    {
        public Role()
        {
            RoleBranch = new HashSet<RoleBranch>();
            RolePermission = new HashSet<RolePermission>();
            UserRole = new HashSet<UserRole>();
            WorkItem = new HashSet<WorkItem>();
            WorkItemHistory = new HashSet<WorkItemHistory>();
        }

        public int RoleId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid Rowguid { get; set; }
        public DateTime ModifiedDate { get; set; }

        public ICollection<RoleBranch> RoleBranch { get; set; }
        public ICollection<RolePermission> RolePermission { get; set; }
        public ICollection<UserRole> UserRole { get; set; }
        public ICollection<WorkItem> WorkItem { get; set; }
        public ICollection<WorkItemHistory> WorkItemHistory { get; set; }
    }
}
