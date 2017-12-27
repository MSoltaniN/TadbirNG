using System;
using System.Activities.Tracking;
using System.Collections.Generic;
using System.Diagnostics;
using SPPC.Framework.Mapper;
using SPPC.Workflow.Model.Tracking;
using SPPC.Framework.Persistence;

namespace SPPC.Framework.Workflow.Tracking
{
    /// <summary>
    /// Represents a workflow extension that interacts with the workflow tracking infrastructure and accesses
    /// tracking records in a SQL Server database
    /// </summary>
    public class SqlTrackingParticipant : TrackingParticipant
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlTrackingParticipant"/> class
        /// </summary>
        /// <param name="repository">Repository object to use for managing workflow tracking records</param>
        /// <param name="mapper">Mapper instance to use for conversion between model and view model objects</param>
        public SqlTrackingParticipant(ITrackingRepository repository, IDomainMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <summary>
        /// Synchronously processes the tracking record
        /// </summary>
        /// <param name="record">The generated tracking record</param>
        /// <param name="timeout">The time period after which the provider aborts the attempt</param>
        protected override void Track(TrackingRecord record, TimeSpan timeout)
        {
            var instanceRecord = record as WorkflowInstanceRecord;
            if (instanceRecord != null)
            {
                var workflowEvent = CreateWorkflowInstanceEvent(instanceRecord);
                _repository.SaveWorkflowEvent(workflowEvent);
            }

            var stateRecord = record as ActivityStateRecord;
            if (stateRecord != null)
            {
                var activityEvent = _mapper.Map<ActivityInstanceEvent>(stateRecord);
                _repository.SaveActivityEvent(activityEvent);
            }

            var scheduledRecord = record as ActivityScheduledRecord;
            if (scheduledRecord != null)
            {
                var activityEvent = _mapper.Map<ExtendedActivityEvent>(scheduledRecord);
                _repository.SaveExtendedActivityEvent(activityEvent);
            }

            var cancelRecord = record as CancelRequestedRecord;
            if (cancelRecord != null)
            {
                var activityEvent = _mapper.Map<ExtendedActivityEvent>(cancelRecord);
                _repository.SaveExtendedActivityEvent(activityEvent);
            }

            var faultRecord = record as FaultPropagationRecord;
            if (faultRecord != null)
            {
                var activityEvent = _mapper.Map<ExtendedActivityEvent>(faultRecord);
                _repository.SaveExtendedActivityEvent(activityEvent);
            }

            var bookmarkRecord = record as BookmarkResumptionRecord;
            if (bookmarkRecord != null)
            {
                var bookmarkEvent = _mapper.Map<BookmarkResumptionEvent>(bookmarkRecord);
                _repository.SaveBookmarkEvent(bookmarkEvent);
            }

            var customRecord = record as CustomTrackingRecord;
            if (customRecord != null)
            {
                string log = String.Format("[INFO] ActivityName = '{0}' (Tracker Id : {1:X})", customRecord.Activity.Name, this.GetHashCode());
                Debug.WriteLine(log);
                var customEvent = _mapper.Map<CustomTrackingEvent>(customRecord);
                _repository.SaveCustomEvent(customEvent);
            }
        }

        private WorkflowInstanceEvent CreateWorkflowInstanceEvent(WorkflowInstanceRecord record)
        {
            WorkflowInstanceEvent workflowEvent = _mapper.Map<WorkflowInstanceEvent>(record);
            var updatedRecord = record as WorkflowInstanceUpdatedRecord;
            if (updatedRecord != null)
            {
                return _mapper.Map<WorkflowInstanceEvent>(updatedRecord);
            }

            var faultedRecord = record as WorkflowInstanceUnhandledExceptionRecord;
            if (faultedRecord != null)
            {
                return _mapper.Map<WorkflowInstanceEvent>(faultedRecord);
            }

            var terminatedRecord = record as WorkflowInstanceTerminatedRecord;
            if (terminatedRecord != null)
            {
                return _mapper.Map<WorkflowInstanceEvent>(terminatedRecord);
            }

            var abortedRecord = record as WorkflowInstanceAbortedRecord;
            if (abortedRecord != null)
            {
                return _mapper.Map<WorkflowInstanceEvent>(abortedRecord);
            }

            var suspendedRecord = record as WorkflowInstanceSuspendedRecord;
            if (suspendedRecord != null)
            {
                return _mapper.Map<WorkflowInstanceEvent>(suspendedRecord);
            }

            return workflowEvent;
        }

        private ITrackingRepository _repository;
        private IDomainMapper _mapper;
    }
}
