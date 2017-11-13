using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.DataAccess
{
    public partial class Permission
    {
        public Permission()
        {
            RolePermission = new HashSet<RolePermission>();
        }

        public int PermissionId { get; set; }
        public int GroupId { get; set; }
        public string Name { get; set; }
        public int Flag { get; set; }
        public string Description { get; set; }
        public Guid Rowguid { get; set; }
        public DateTime ModifiedDate { get; set; }

        public PermissionGroup Group { get; set; }
        public ICollection<RolePermission> RolePermission { get; set; }
    }
}
