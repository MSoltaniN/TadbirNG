using System;
using System.Collections.Generic;
using System.Text;

namespace SPPC.Tadbir.Model.Auth
{
    public partial class Permission
    {
        /// <summary>
        /// Gets a collection of existing associations between roles and permissions
        /// </summary>
        public virtual IList<RolePermission> RolePermissions { get; protected set; }
    }
}
