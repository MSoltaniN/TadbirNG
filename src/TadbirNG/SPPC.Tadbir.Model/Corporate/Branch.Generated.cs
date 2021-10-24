// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.0.0.0
//     Template Version: 1.0
//     Generation Date: 2017-04-17 10:52:07 AM
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using SPPC.Tadbir.Model.Auth;

namespace SPPC.Tadbir.Model.Corporate
{
    /// <summary>
    /// Represents a physical division of a business unit
    /// </summary>
    public partial class Branch : CoreEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Branch"/> class.
        /// </summary>
        public Branch()
        {
            this.Name = String.Empty;
            this.Description = String.Empty;
            this.ModifiedDate = DateTime.Now;
            InitReferences();
        }

        /// <summary>
        /// Gets or sets the name of this branch
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// Gets or sets the detail information related to this branch
        /// </summary>
        public virtual string Description { get; set; }

        /// <summary>
        /// Gets or sets the level of this branch in the main branch hierarchy
        /// </summary>
        public virtual int Level { get; set; }

        // Temporarily disabled, due to EF Core's lack of support for direct many-to-many relationships.
        ///// <summary>
        ///// Gets or sets the collection of all roles that can access this branch
        ///// </summary>
        ////public virtual IList<Role> Roles { get; protected set; }

        /// <summary>
        /// Gets or sets the parent of this branch in the main branch hierarchy
        /// </summary>
        public virtual Branch Parent { get; set; }

        private void InitReferences()
        {
            RoleBranches = new List<RoleBranch>();
            Children = new List<Branch>();
        }
    }
}