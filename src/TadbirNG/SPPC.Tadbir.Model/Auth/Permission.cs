using System.Collections.Generic;

namespace SPPC.Tadbir.Model.Auth
{
    public partial class Permission
    {
        /// <summary>
        /// Gets a collection of existing associations between roles and permissions
        /// </summary>
        public virtual IList<RolePermission> RolePermissions { get; protected set; }

        /// <summary>
        /// Gets or sets the identifier of the group that categorizes this permission
        /// </summary>
        public virtual int GroupId { get; set; }
    }
}
