// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.0.0.0
//     Template Version: 1.0
//     Generation Date: 2017-08-03 6:50:34 PM
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using BabakSoft.Platform.Domain;
using SPPC.Tadbir.Model.Contact;
using SPPC.Tadbir.Model.Core;
using SPPC.Tadbir.Model.Corporate;
using SPPC.Tadbir.Model.Finance;
using SPPC.Tadbir.Model.Inventory;

namespace SPPC.Tadbir.Model.Procurement
{

    /// <summary>
    /// TODO: Add description...
    /// </summary>
    public partial class RequisitionVoucher : IEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RequisitionVoucher"/> class.
        /// </summary>
        public RequisitionVoucher()
        {
            this.No = String.Empty;
            this.Reference = String.Empty;
            this.Reason = String.Empty;
            this.WarehouseComment = String.Empty;
            this.Description = String.Empty;
            this.ModifiedDate = DateTime.Now;
            InitReferences();
        }

        /// <summary>
        /// Gets or sets the unique identifier for this entity. This property is auto-generated.
        /// </summary>
        public virtual int Id { get; set; }

        /// <summary>
        /// TODO: Add description...
        /// </summary>
        public virtual string No { get; set; }

        /// <summary>
        /// TODO: Add description...
        /// </summary>
        public virtual string Reference { get; set; }

        /// <summary>
        /// TODO: Add description...
        /// </summary>
        public virtual DateTime? OrderedDate { get; set; }

        /// <summary>
        /// TODO: Add description...
        /// </summary>
        public virtual DateTime? RequiredDate { get; set; }

        /// <summary>
        /// TODO: Add description...
        /// </summary>
        public virtual DateTime? PromisedDate { get; set; }

        /// <summary>
        /// TODO: Add description...
        /// </summary>
        public virtual string Reason { get; set; }

        /// <summary>
        /// TODO: Add description...
        /// </summary>
        public virtual string WarehouseComment { get; set; }

        /// <summary>
        /// TODO: Add description...
        /// </summary>
        public virtual bool IsActive { get; set; }

        /// <summary>
        /// TODO: Add description...
        /// </summary>
        public virtual string Description { get; set; }

        /// <summary>
        /// TODO: Add description...
        /// </summary>
        public virtual long Timestamp { get; set; }

        /// <summary>
        /// Gets or sets the date when database row for this entity was last modified. This property is auto-generated.
        /// </summary>
        public virtual DateTime ModifiedDate { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier for the database row for this entity. This property is auto-generated.
        /// </summary>
        public virtual Guid RowGuid { get; set; }

        /// <summary>
        /// Gets or sets the todo: add description...
        /// </summary>
        public virtual IList<RequisitionVoucherLine> Lines { get; protected set; }

        /// <summary>
        /// Gets or sets the todo: add description...
        /// </summary>
        public virtual FiscalPeriod FiscalPeriod { get; set; }

        /// <summary>
        /// Gets or sets the todo: add description...
        /// </summary>
        public virtual Branch Branch { get; set; }

        /// <summary>
        /// Gets or sets the todo: add description...
        /// </summary>
        public virtual BusinessPartner Requester { get; set; }

        /// <summary>
        /// Gets or sets the todo: add description...
        /// </summary>
        public virtual BusinessPartner Receiver { get; set; }

        /// <summary>
        /// Gets or sets the todo: add description...
        /// </summary>
        public virtual BusinessUnit RequesterUnit { get; set; }

        /// <summary>
        /// Gets or sets the todo: add description...
        /// </summary>
        public virtual BusinessUnit ReceiverUnit { get; set; }

        /// <summary>
        /// Gets or sets the todo: add description...
        /// </summary>
        public virtual Warehouse Warehouse { get; set; }

        /// <summary>
        /// Gets or sets the todo: add description...
        /// </summary>
        public virtual ServiceJob ServiceJob { get; set; }

        /// <summary>
        /// Gets or sets the todo: add description...
        /// </summary>
        public virtual FullAccount FullAccount { get; set; }

        /// <summary>
        /// Gets or sets the todo: add description...
        /// </summary>
        public virtual FullDetail FullDetail { get; set; }

        /// <summary>
        /// Gets or sets the todo: add description...
        /// </summary>
        public virtual Document Document { get; set; }

        private void InitReferences()
        {
        }
    }
}
