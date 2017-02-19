// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.0.0.0
//     Template Version: 1.0
//     Generation Date: 2017-02-15 2:57:55 PM
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using SPPC.Tadbir.Model.Finance;
using SwForAll.Platform.Domain;

namespace SPPC.Tadbir.Model.Corporate
{
    /// <summary>
    /// Represents a physical division of a business unit
    /// </summary>
    public partial class Branch : IEntity
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
        /// Gets or sets the unique identifier for this entity. This property is auto-generated.
        /// </summary>
        public virtual int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of this branch
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// Gets or sets the detail information related to this branch
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
        /// Gets or sets the main company that controls the activities of this branch
        /// </summary>
        public virtual Company Company { get; set; }

        /// <summary>
        /// Gets or sets the collection of all fiscal periods defined for this branch
        /// </summary>
        public virtual IList<FiscalPeriod> FiscalPeriods { get; protected set; }

        private void InitReferences()
        {
            this.FiscalPeriods = new List<FiscalPeriod>();

            //// IMPORTANT NOTE: DO NOT add initialization statements for one-to-one and many-to-one relationships.
            //// 1. Initializing one-to-one associations causes StackOverflowException (A initializes B and B initializes A)
            //// 2. Initializing many-to-one associations causes most mapping tests to fail, because they will trigger many
            //// unnecessary operations (INSERT and UPDATE) by in-memory SQLite database.
        }
    }
}
