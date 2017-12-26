// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.0.0.0
//     Template Version: 1.0
//     Generation Date: 2017-07-08 4:27:59 PM
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
    public partial class ExtendedActivityEvent : IEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExtendedActivityEvent"/> class.
        /// </summary>
        public ExtendedActivityEvent()
        {
            this.ActivityRecordType = String.Empty;
            this.ActivityName = String.Empty;
            this.ActivityId = String.Empty;
            this.ActivityInstanceId = String.Empty;
            this.ActivityType = String.Empty;
            this.ChildActivityName = String.Empty;
            this.ChildActivityId = String.Empty;
            this.ChildActivityInstanceId = String.Empty;
            this.ChildActivityType = String.Empty;
            this.FaultDetails = String.Empty;
            this.FaultHandlerActivityName = String.Empty;
            this.FaultHandlerActivityId = String.Empty;
            this.FaultHandlerActivityInstanceId = String.Empty;
            this.FaultHandlerActivityType = String.Empty;
            this.SerializedAnnotations = String.Empty;
            this.ModifiedDate = DateTime.Now;
            InitReferences();
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
        public virtual string ActivityRecordType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string ActivityName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string ActivityId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string ActivityInstanceId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string ActivityType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string ChildActivityName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string ChildActivityId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string ChildActivityInstanceId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string ChildActivityType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string FaultDetails { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string FaultHandlerActivityName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string FaultHandlerActivityId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string FaultHandlerActivityInstanceId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string FaultHandlerActivityType { get; set; }

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

        private void InitReferences()
        {

            //// IMPORTANT NOTE: DO NOT add initialization statements for one-to-one and many-to-one relationships.
            //// 1. Initializing one-to-one associations causes StackOverflowException (A initializes B and B initializes A)
            //// 2. Initializing many-to-one associations causes most mapping tests to fail, because they will trigger many
            //// unnecessary operations (INSERT and UPDATE) by in-memory SQLite database.
        }
    }
}
