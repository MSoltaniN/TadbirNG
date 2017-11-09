using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.DataAccess
{
    public partial class RolePermission
    {
        public int RolePermissionId { get; set; }
        public int RoleId { get; set; }
        public int PermissionId { get; set; }
        public Guid Rowguid { get; set; }
        public DateTime ModifiedDate { get; set; }

        public Permission Permission { get; set; }
        public Role Role { get; set; }
    }
}
