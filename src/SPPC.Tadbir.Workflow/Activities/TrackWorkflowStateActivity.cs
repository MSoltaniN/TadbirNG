using System;
using System.Activities;
using System.Activities.Tracking;
using System.Collections.Generic;
using SPPC.Tadbir.Values;

namespace SPPC.Tadbir.Workflow.Activities
{
    public class TrackWorkflowStateActivity : CodeActivity
    {
        [RequiredArgument]
        public InArgument<StateOperation> Operation { get; set; }

        [RequiredArgument]
        public InArgument<string> State { get; set; }

        [RequiredArgument]
        public InArgument<string> WorkflowName { get; set; }

        [RequiredArgument]
        public InArgument<string> EditionName { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            var record = new CustomTrackingRecord(WorkflowEvent.StateChanged);
            record.Data.Add("InstanceId", context.WorkflowInstanceId.ToString("B"));
            record.Data.Add("DocumentId", context.GetValue(Operation).DocumentId);
            record.Data.Add("DocumentType", context.GetValue(Operation).DocumentType);
            record.Data.Add("WorkflowName", context.GetValue(WorkflowName));
            record.Data.Add("EditionName", context.GetValue(EditionName));
            record.Data.Add("State", context.GetValue(State));
            record.Data.Add("LastActionDate", DateTime.Now);
            context.Track(record);
        }
    }
}
