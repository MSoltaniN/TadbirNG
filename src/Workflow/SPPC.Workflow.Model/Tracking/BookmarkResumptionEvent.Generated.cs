// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.0.0.0
//     Template Version: 1.0
//     Generation Date: 2017-07-08 4:28:04 PM
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------

using System;
using SPPC.Framework.Domain;

namespace SPPC.Workflow.Model.Tracking
{
    /// <summary>
    /// 
    /// </summary>
    public partial class BookmarkResumptionEvent : IEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BookmarkResumptionEvent"/> class.
        /// </summary>
        public BookmarkResumptionEvent()
        {
            this.BookmarkName = String.Empty;
            this.OwnerActivityName = String.Empty;
            this.OwnerActivityId = String.Empty;
            this.OwnerActivityInstanceId = String.Empty;
            this.OwnerActivityType = String.Empty;
            this.SerializedAnnotations = String.Empty;
            this.ModifiedDate = DateTime.Now;
        }

        /// <summary>
        /// Gets or sets the unique identifier for this entity. This property is auto-generated.
        /// </summary>
        public virtual int Id { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the generating workflow instance
        /// </summary>
        public virtual Guid WorkflowInstanceId { get; set; }

        /// <summary>
        /// Gets or sets the a sequence that defines the order in which tracking records are generated
        /// </summary>
        public virtual long RecordNumber { get; set; }

        /// <summary>
        /// Gets or sets the tracelevel of the event
        /// </summary>
        public virtual byte TraceLevelId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string BookmarkName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual Guid BookmarkScope { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string OwnerActivityName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string OwnerActivityId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string OwnerActivityInstanceId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string OwnerActivityType { get; set; }

        /// <summary>
        /// Gets or sets the a collection of name/value pairs that are added to this tracking record
        /// </summary>
        public virtual string SerializedAnnotations { get; set; }

        /// <summary>
        /// Gets or sets the date and time the tracking record occurred
        /// </summary>
        public virtual DateTime TimeCreated { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier for the database row for this entity. This property is auto-generated.
        /// </summary>
        public virtual Guid RowGuid { get; set; }

        /// <summary>
        /// Gets or sets the date when database row for this entity was last modified. This property is auto-generated.
        /// </summary>
        public virtual DateTime ModifiedDate { get; set; }
    }
}
