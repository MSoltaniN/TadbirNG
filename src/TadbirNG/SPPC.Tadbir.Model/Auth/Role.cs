using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Model.Auth
{
    public partial class Role
    {
        /// <summary>
        /// Gets a collection of existing associations between users and roles
        /// </summary>
        public virtual IList<UserRole> UserRoles { get; protected set; }

        /// <summary>
        /// Gets a collection of existing associations between roles and permissions
        /// </summary>
        public virtual IList<RolePermission> RolePermissions { get; protected set; }

        /// <summary>
        /// Gets a collection of existing associations between roles and companies
        /// </summary>
        public virtual IList<RoleCompany> RoleCompanies { get; protected set; }
    }
}
