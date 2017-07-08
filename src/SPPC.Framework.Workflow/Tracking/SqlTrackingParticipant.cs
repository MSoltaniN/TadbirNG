using System;
using System.Activities.Tracking;

namespace SPPC.Framework.Workflow.Tracking
{
    public class SqlTrackingParticipant : TrackingParticipant
    {
        public SqlTrackingParticipant()
        {
        }

        // The track method is called when a tracking record is emitted by the workflow runtime
        protected override void Track(TrackingRecord record, TimeSpan timeout)
        {
            // TODO: Implement tracking here...
        }
    }
}
