using System;
using System.Collections.Generic;
using System.Text;

namespace SPPC.Tadbir.Model.Auth
{
    public partial class Role
    {
        public virtual IList<UserRole> UserRoles { get; protected set; }

        public virtual IList<RoleBranch> RoleBranches { get; protected set; }

        public virtual IList<RolePermission> RolePermissions { get; protected set; }
    }
}
