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
using SwForAll.Platform.Domain;

namespace SPPC.Tadbir.Model.Auth
{
    /// <summary>
    /// Represents a potential access grant for a unit of functionality in the application
    /// </summary>
    public partial class Permission : IEntity
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
        /// Gets or sets the unique identifier for this entity. This property is auto-generated.
        /// </summary>
        public virtual int Id { get; set; }

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
        /// Gets or sets the unique identifier for the database row for this entity. This property is auto-generated.
        /// </summary>
        public virtual Guid RowGuid { get; set; }

        /// <summary>
        /// Gets or sets the date when database row for this entity was last modified. This property is auto-generated.
        /// </summary>
        public virtual DateTime ModifiedDate { get; set; }

        /// <summary>
        /// Gets or sets the main permission group that contains this permission
        /// </summary>
        public virtual PermissionGroup Group { get; set; }

        /// <summary>
        /// Gets or sets the collection of all roles that this permission is enabled in them.
        /// </summary>
        public virtual IList<Role> Roles { get; protected set; }

        private void InitReferences()
        {
            this.Roles = new List<Role>();

            //// IMPORTANT NOTE: DO NOT add initialization statements for one-to-one and many-to-one relationships.
            //// 1. Initializing one-to-one associations causes StackOverflowException (A initializes B and B initializes A)
            //// 2. Initializing many-to-one associations causes most mapping tests to fail, because they will trigger many
            //// unnecessary operations (INSERT and UPDATE) by in-memory SQLite database.
        }
    }
}
