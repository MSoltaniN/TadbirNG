using System;
using System.Activities;
using System.Activities.Tracking;
using SPPC.Tadbir.Values;
using BabakSoft.Platform.Common;

namespace SPPC.Tadbir.Workflow.Activities
{
    /// <summary>
    /// این فعالیت یک رکورد ردگیری برای اعلام تغییر وضعیت گردش کار ایجاد می کند.
    /// </summary>
    public class TrackWorkflowStateActivity : CodeActivity
    {
        /// <summary>
        /// اطلاعات مورد نیاز برای انجام یک اقدام روی یک مستند
        /// </summary>
        [RequiredArgument]
        public InArgument<StateOperation> Operation { get; set; }

        /// <summary>
        /// وضعیتی که توسط این فعالیت برای یک مستند درون گردش کاری اعلام می شود
        /// </summary>
        [RequiredArgument]
        public InArgument<string> State { get; set; }

        /// <summary>
        /// عنوان گردش کاری که این فعالیت در آن قرار گرفته است
        /// </summary>
        [RequiredArgument]
        public InArgument<string> WorkflowName { get; set; }

        /// <summary>
        /// ویرایش مورد استفاده از گردش کاری که این فعالیت در آن قرار گرفته است 
        /// </summary>
        [RequiredArgument]
        public InArgument<string> EditionName { get; set; }

        /// <summary>
        /// فعالیت را با استفاده از اطلاعات جاری محیطی اجرا می کند.
        /// </summary>
        /// <param name="context">اطلاعات محیط اجرایی فعالیت در زمان اجرای آن</param>
        protected override void Execute(CodeActivityContext context)
        {
            Verify.ArgumentNotNull(context, "context");
            var record = new CustomTrackingRecord(WorkflowEvent.StateChanged);
            record.Data.Add("InstanceId", context.WorkflowInstanceId.ToString("B"));
            record.Data.Add("DocumentId", context.GetValue(Operation).DocumentId);
            record.Data.Add("DocumentType", context.GetValue(Operation).DocumentType);
            record.Data.Add("WorkflowName", context.GetValue(WorkflowName));
            record.Data.Add("EditionName", context.GetValue(EditionName));
            record.Data.Add("State", context.GetValue(State));
            record.Data.Add("LastActor", context.GetValue(Operation).CreatedById.ToString());
            record.Data.Add("LastActionDate", DateTime.Now);
            context.Track(record);
        }
    }
}
