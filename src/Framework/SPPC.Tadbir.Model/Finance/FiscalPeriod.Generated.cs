// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.0.0.0
//     Template Version: 1.0
//     Generation Date: 2017-04-17 10:52:12 AM
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using SPPC.Tadbir.Model.Corporate;
using SPPC.Framework.Domain;
using SPPC.Tadbir.Model.Auth;

namespace SPPC.Tadbir.Model.Finance
{
    /// <summary>
    /// Represents a date period used for partitioning financial events of a business unit
    /// </summary>
    public partial class FiscalPeriod : IEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FiscalPeriod"/> class.
        /// </summary>
        public FiscalPeriod()
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
        /// Gets or sets the name of this fiscal period
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// Gets or sets the date when business activities of this period starts
        /// </summary>
        public virtual DateTime StartDate { get; set; }

        /// <summary>
        /// Gets or sets the date when business activities of this period ends
        /// </summary>
        public virtual DateTime EndDate { get; set; }

        /// <summary>
        /// Gets or sets the detail information related to this fiscal period
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
        /// Gets or sets the business unit that defines this fiscal period
        /// </summary>
        public virtual Company Company { get; set; }

        private void InitReferences()
        {
            RoleFiscalPeriods = new List<RoleFiscalPeriod>();
        }
    }
}
