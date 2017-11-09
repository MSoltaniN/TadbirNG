﻿using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.DataAccess
{
    public partial class CustomTrackingEvent
    {
        public int EventId { get; set; }
        public Guid WorkflowInstanceId { get; set; }
        public long? RecordNumber { get; set; }
        public byte? TraceLevelId { get; set; }
        public string CustomRecordName { get; set; }
        public string ActivityName { get; set; }
        public string ActivityId { get; set; }
        public string ActivityInstanceId { get; set; }
        public string ActivityType { get; set; }
        public string SerializedData { get; set; }
        public string SerializedAnnotations { get; set; }
        public DateTime TimeCreated { get; set; }
        public Guid Rowguid { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
