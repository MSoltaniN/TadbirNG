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
using SPPC.Tadbir.Model.Corporate;
using SPPC.Tadbir.Model.Finance;
using SPPC.Tadbir.Model.Inventory;
using SPPC.Tadbir.Model.Procurement;

namespace SPPC.Tadbir.Model.Warehousing
{
    public partial class IssueReceiptVoucherLine : IEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IssueReceiptVoucherLine"/> class.
        /// </summary>
        public IssueReceiptVoucherLine()
        {
            this.Description = String.Empty;
            this.ModifiedDate = DateTime.Now;
            InitReferences();
        }

        /// <summary>
        /// Gets or sets the unique identifier for this entity. This property is auto-generated.
        /// </summary>
        public virtual int Id { get; set; }
        public virtual int No { get; set; }
        public virtual double Quantity { get; set; }
        public virtual double UnitPrice { get; set; }
        public virtual double CurrencyUnitPrice { get; set; }
        public virtual double Remainder { get; set; }
        public virtual bool IsActive { get; set; }
        public virtual string Description { get; set; }
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
        public virtual IssueReceiptVoucher Voucher { get; set; }

        /// <summary>
        /// Gets or sets the todo: add description...
        /// </summary>
        public virtual Warehouse Warehouse { get; set; }

        /// <summary>
        /// Gets or sets the todo: add description...
        /// </summary>
        public virtual Product Product { get; set; }

        /// <summary>
        /// Gets or sets the todo: add description...
        /// </summary>
        public virtual UnitOfMeasurement Uom { get; set; }

        /// <summary>
        /// Gets or sets the todo: add description...
        /// </summary>
        public virtual Currency Currency { get; set; }

        /// <summary>
        /// Gets or sets the todo: add description...
        /// </summary>
        public virtual RequisitionVoucher RequisitionVoucher { get; set; }

        /// <summary>
        /// Gets or sets the todo: add description...
        /// </summary>
        public virtual Branch Branch { get; set; }

        /// <summary>
        /// Gets or sets the todo: add description...
        /// </summary>
        public virtual FiscalPeriod FiscalPeriod { get; set; }

        /// <summary>
        /// Gets or sets the todo: add description...
        /// </summary>
        public virtual FullAccount FullAccount { get; set; }

        /// <summary>
        /// Gets or sets the todo: add description...
        /// </summary>
        public virtual FullDetail FullDetail { get; set; }

        private void InitReferences()
        {
        }
    }
}
