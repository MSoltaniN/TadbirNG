using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.DataAccess
{
    public partial class WorkflowInstanceEvent
    {
        public int EventId { get; set; }
        public Guid WorkflowInstanceId { get; set; }
        public string ActivityDefinition { get; set; }
        public long RecordNumber { get; set; }
        public string State { get; set; }
        public byte? TraceLevelId { get; set; }
        public string Reason { get; set; }
        public string ExceptionDetails { get; set; }
        public string SerializedAnnotations { get; set; }
        public DateTime TimeCreated { get; set; }
        public Guid Rowguid { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
