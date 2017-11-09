using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.DataAccess
{
    public partial class PermissionGroup
    {
        public PermissionGroup()
        {
            Permission = new HashSet<Permission>();
        }

        public int PermissionGroupId { get; set; }
        public string Name { get; set; }
        public string EntityName { get; set; }
        public string Description { get; set; }
        public Guid Rowguid { get; set; }
        public DateTime ModifiedDate { get; set; }

        public ICollection<Permission> Permission { get; set; }
    }
}
