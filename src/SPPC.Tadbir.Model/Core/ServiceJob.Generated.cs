// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.0.0.0
//     Template Version: 1.0
//     Generation Date: 2017-08-02 7:42:27 PM
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using BabakSoft.Platform.Domain;
using SPPC.Tadbir.Model.Procurement;

namespace SPPC.Tadbir.Model.Core
{
    /// <summary>
    /// TODO: Add description...
    /// </summary>
    public partial class ServiceJob : IEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceJob"/> class.
        /// </summary>
        public ServiceJob()
        {
            this.ModifiedDate = DateTime.Now;
            InitReferences();
        }

        /// <summary>
        /// Gets or sets the unique identifier for this entity. This property is auto-generated.
        /// </summary>
        public virtual int Id { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier for the database row for this entity. This property is auto-generated.
        /// </summary>
        public virtual Guid RowGuid { get; set; }

        /// <summary>
        /// Gets or sets the date when database row for this entity was last modified. This property is auto-generated.
        /// </summary>
        public virtual DateTime ModifiedDate { get; set; }

        /// <summary>
        /// Gets or sets the todo: add description...
        /// </summary>
        public virtual IList<RequisitionVoucher> RequisitionVouchers { get; protected set; }

        private void InitReferences()
        {
            this.RequisitionVouchers = new List<RequisitionVoucher>();
        }
    }
}
