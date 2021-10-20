// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.0.0.0
//     Template Version: 1.0
//     Generation Date: 2017-04-01 3:24:07 PM
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Model.Auth
{
    /// <summary>
    /// Represents a logical authority used in a role-based security subsystem.
    /// </summary>
    public partial class Role : CoreEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Role"/> class.
        /// </summary>
        public Role()
        {
            this.Name = String.Empty;
            this.Description = String.Empty;
            this.ModifiedDate = DateTime.Now;
            InitReferences();
        }

        /// <summary>
        /// Gets or sets the name of this application role
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// Gets or sets the detail information related to this role
        /// </summary>
        public virtual string Description { get; set; }

        // Temporarily disabled, due to EF Core's lack of support for direct many-to-many relationships.
        ///// <summary>
        ///// Gets or sets the collection of all application users that this role is assigned to them
        ///// </summary>
        ////public virtual IList<User> Users { get; protected set; }

        // Temporarily disabled, due to EF Core's lack of support for direct many-to-many relationships.
        ///// <summary>
        ///// Gets or sets the collection of all operational permissions that are enabled for this role
        ///// </summary>
        ////public virtual IList<Permission> Permissions { get; protected set; }

        // Temporarily disabled, due to EF Core's lack of support for direct many-to-many relationships.
        ///// <summary>
        ///// Gets or sets the collection of all branches that this role can access
        ///// </summary>
        ////public virtual IList<Branch> Branches { get; protected set; }

        private void InitReferences()
        {
            UserRoles = new List<UserRole>();
            RolePermissions = new List<RolePermission>();
        }
    }
}
