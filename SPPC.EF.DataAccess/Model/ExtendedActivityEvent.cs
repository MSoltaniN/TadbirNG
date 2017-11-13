using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.DataAccess
{
    public partial class ExtendedActivityEvent
    {
        public int EventId { get; set; }
        public Guid WorkflowInstanceId { get; set; }
        public long? RecordNumber { get; set; }
        public byte? TraceLevelId { get; set; }
        public string ActivityRecordType { get; set; }
        public string ActivityName { get; set; }
        public string ActivityId { get; set; }
        public string ActivityInstanceId { get; set; }
        public string ActivityType { get; set; }
        public string ChildActivityName { get; set; }
        public string ChildActivityId { get; set; }
        public string ChildActivityInstanceId { get; set; }
        public string ChildActivityType { get; set; }
        public string FaultDetails { get; set; }
        public string FaultHandlerActivityName { get; set; }
        public string FaultHandlerActivityId { get; set; }
        public string FaultHandlerActivityInstanceId { get; set; }
        public string FaultHandlerActivityType { get; set; }
        public string SerializedAnnotations { get; set; }
        public DateTime TimeCreated { get; set; }
        public Guid Rowguid { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
