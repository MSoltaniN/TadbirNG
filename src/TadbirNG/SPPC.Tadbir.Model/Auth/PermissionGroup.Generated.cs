// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.2.1493
//     Template Version: 1.0
//     Generation Date: 2023-03-19 12:24:27 PM
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using SPPC.Tadbir.Model.Metadata;

namespace SPPC.Tadbir.Model.Auth
{
    /// <summary>
    /// Represents a category used for organizing related permissions.
    /// </summary>
    public partial class PermissionGroup : CoreEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PermissionGroup"/> class.
        /// </summary>
        public PermissionGroup()
        {
            Name = String.Empty;
            EntityName = String.Empty;
            Description = String.Empty;
            ModifiedDate = DateTime.Now;
        }

        /// <summary>
        /// Gets or sets the name of this permission group
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// Gets or sets the name of an application-defined entity that permissions in this group protect
        /// </summary>
        public virtual string EntityName { get; set; }

        /// <summary>
        /// Gets or sets the detail information related to this permission group
        /// </summary>
        public virtual string Description { get; set; }

        /// <summary>
        /// Gets the collection of all permissions defined in this group
        /// </summary>
        public virtual IList<Permission> Permissions { get; protected set; }

        /// <summary>
        /// Gets or sets the subsystem that defines the general scope for this permission group
        /// </summary>
        public virtual Subsystem Subsystem { get; set; }

        /// <summary>
        /// Gets or sets the general category for the entity or form that this permission group protects
        /// </summary>
        public virtual OperationSourceType SourceType { get; set; }
    }
}
