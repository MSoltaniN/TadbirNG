using System;
using System.Collections.Generic;
using System.Text;

namespace SPPC.Tadbir.Model.Auth
{
    public partial class Role
    {
        public IList<UserRole> UserRoles { get; protected set; }

        public IList<RoleBranch> RoleBranches { get; protected set; }

        public IList<RolePermission> RolePermissions { get; protected set; }
    }
}
