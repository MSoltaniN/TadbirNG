using System;
using System.Collections.Generic;
using System.Text;

namespace SPPC.Tadbir.Model.Auth
{
    public partial class Permission
    {
        public virtual IList<RolePermission> RolePermissions { get; protected set; }
    }
}
