using System;
using System.Collections.Generic;
using System.Text;

namespace SPPC.Tadbir.Model.Auth
{
    public partial class Role
    {
        /// <summary>
        /// Gets a collection of existing associations between users and roles
        /// </summary>
        public virtual IList<UserRole> UserRoles { get; protected set; }

        /// <summary>
        /// Gets a collection of existing associations between roles and branches
        /// </summary>
        public virtual IList<RoleBranch> RoleBranches { get; protected set; }

        /// <summary>
        /// Gets a collection of existing associations between roles and fiscal periods
        /// </summary>
        public virtual IList<RoleFiscalPeriod> RoleFiscalPeriods { get; protected set; }

        /// <summary>
        /// Gets a collection of existing associations between roles and permissions
        /// </summary>
        public virtual IList<RolePermission> RolePermissions { get; protected set; }
    }
}
