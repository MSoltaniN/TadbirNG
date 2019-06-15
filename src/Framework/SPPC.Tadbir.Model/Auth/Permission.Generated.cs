// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.0.0.0
//     Template Version: 1.0
//     Generation Date: 2017-03-08 6:00:01 PM
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
    /// Represents a potential access grant for a unit of functionality in the application
    /// </summary>
    public partial class Permission : CoreEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Permission"/> class.
        /// </summary>
        public Permission()
        {
            this.Name = String.Empty;
            this.Description = String.Empty;
            this.ModifiedDate = DateTime.Now;
            InitReferences();
        }

        /// <summary>
        /// Gets or sets the name of this permission
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// Gets or sets the numerical code that identifies a secure operation that this permission represents
        /// </summary>
        public virtual int Flag { get; set; }

        /// <summary>
        /// Gets or sets the detail information related to this permission
        /// </summary>
        public virtual string Description { get; set; }

        /// <summary>
        /// Gets or sets the main permission group that contains this permission
        /// </summary>
        public virtual PermissionGroup Group { get; set; }

        // Temporarily disabled, due to EF Core's lack of support for direct many-to-many relationships.
        ///// <summary>
        ///// Gets or sets the collection of all roles that this permission is enabled in them.
        ///// </summary>
        ////public virtual IList<Role> Roles { get; protected set; }

        private void InitReferences()
        {
            Group = new PermissionGroup();
            RolePermissions = new List<RolePermission>();
            ////Roles = new List<Role>();
        }
    }
}
