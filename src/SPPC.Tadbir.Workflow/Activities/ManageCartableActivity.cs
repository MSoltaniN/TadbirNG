using System;
using System.Activities;
using BabakSoft.Platform.Common;
using SPPC.Framework.Unity.WF;
using SPPC.Tadbir.Metadata.Workflow;
using SPPC.Tadbir.NHibernate;
using SPPC.Tadbir.Values;
using SPPC.Tadbir.ViewModel.Workflow;

namespace SPPC.Tadbir.Workflow
{
    /// <summary>
    /// این فعالیت از طریق ایجاد کار و رکوردهای مرتبط با آن، از گردش اطلاعات موجودیت ها
    /// در جریان اجرای فرآیندهای کسب و کار پشتیبانی می کند.
    /// </summary>
    public class ManageCartableActivity : CodeActivity
    {
        /// <summary>
        /// آرگومان اجباری برای نگهداری اطلاعات مورد نیاز برای انجام یک اقدام روی یک مستند
        /// </summary>
        [RequiredArgument]
        public InArgument<StateOperation> Operation { get; set; }

        /// <summary>
        /// آرگومان اجباری برای نگهداری اطلاعات فراداده ای اقدام جاری روی مستند
        /// </summary>
        [RequiredArgument]
        public InArgument<StateAction> Metadata { get; set; }

        /// <summary>
        /// فعالیت را با استفاده از اطلاعات جاری محیطی اجرا می کند.
        /// </summary>
        /// <param name="context">اطلاعات محیط اجرایی فعالیت در زمان اجرای آن</param>
        protected override void Execute(CodeActivityContext context)
        {
            Verify.ArgumentNotNull(context, "context");
            InitializeDependencies(context);
            var operation = context.GetValue(Operation);
            var metadata = context.GetValue(Metadata);
            var workItem = GetNewWorkItem(operation, metadata);
            var createDelegate = GetWorkItemDelegate(metadata.ToDocumentStatus, metadata.FromDocumentStatus);
            createDelegate(workItem);
        }

        private static string GenerateNumber()
        {
            return Guid.NewGuid()
                .ToString()
                .Replace("{", String.Empty)
                .Replace("}", String.Empty)
                .Replace("-", String.Empty)
                .Substring(0, 8);
        }

        private static WorkItemViewModel GetNewWorkItem(StateOperation operation, StateAction metadata)
        {
            DateTime current = DateTime.Now;
            var workItem = new WorkItemViewModel()
            {
                CreatedById = operation.CreatedById,
                EntityId = operation.EntityId,
                DocumentType = operation.DocumentType,
                DocumentId = operation.DocumentId,
                TargetId = metadata.TargetId,
                Number = GenerateNumber(),
                Date = current.Date,
                Time = current.TimeOfDay,
                Title = metadata.WorkTitle,
                StatusId = metadata.ToStatus,
                OperationalStatus = metadata.ToDocumentStatus,
                Action = metadata.NextAction,
                PreviousAction = metadata.Name,
                Remarks = operation.Remarks
            };

            return workItem;
        }

        private void InitializeDependencies(CodeActivityContext context)
        {
            _repository = context.GetDependency<IWorkItemRepository>("WF");
        }

        private CreateWorkItemDelegate GetWorkItemDelegate(string status, string fromStatus)
        {
            CreateWorkItemDelegate method = null;
            switch (status)
            {
                case DocumentStatusName.Created:
                    break;
                case DocumentStatusName.Prepared:
                    method = (fromStatus == DocumentStatusName.Created)
                        ? new CreateWorkItemDelegate(_repository.CreateInitialWorkItem)
                        : new CreateWorkItemDelegate(_repository.CreateWorkItem);
                    break;
                case DocumentStatusName.Approved:
                    method = new CreateWorkItemDelegate(_repository.CreateFinalWorkItem);
                    break;
                default:
                    method = new CreateWorkItemDelegate(_repository.CreateWorkItem);
                    break;
            }

            return method;
        }

        private IWorkItemRepository _repository;
    }

    internal delegate void CreateWorkItemDelegate(WorkItemViewModel workItem);
}
