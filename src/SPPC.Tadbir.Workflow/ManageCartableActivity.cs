using System;
using System.Activities;
using SPPC.Framework.Unity.WF;
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
        [RequiredArgument]
        public InArgument<int> CreatedById { get; set; }

        [RequiredArgument]
        public InArgument<string> Target { get; set; }

        [RequiredArgument]
        public InArgument<int> DocumentId { get; set; }

        [RequiredArgument]
        public InArgument<string> Title { get; set; }

        public InArgument<string> Remarks { get; set; }

        public InArgument<string> FromStatus { get; set; }

        [RequiredArgument]
        public InArgument<string> Status { get; set; }

        [RequiredArgument]
        public InArgument<string> OperationalStatus { get; set; }

        /// <summary>
        /// فعالیت را با استفاده از اطلاعات جاری محیطی اجرا می کند.
        /// </summary>
        /// <param name="context">اطلاعات محیط اجرایی فعالیت در زمان اجرای آن</param>
        protected override void Execute(CodeActivityContext context)
        {
            InitializeDependencies(context);
            var workItem = GetNewWorkItem(context);
            string previousStatus = context.GetValue(FromStatus);
            string status = context.GetValue(OperationalStatus);
            var createDelegate = GetWorkItemDelegate(status, previousStatus);
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

        private void InitializeDependencies(CodeActivityContext context)
        {
            _repository = context.GetDependency<IWorkItemRepository>("WF");
        }

        private CreateWorkItemDelegate GetWorkItemDelegate(string status, string fromStatus)
        {
            CreateWorkItemDelegate method = null;
            switch (status)
            {
                case DocumentStatus.Created:
                    break;
                case DocumentStatus.Prepared:
                    method = String.IsNullOrEmpty(fromStatus)
                        ? new CreateWorkItemDelegate(_repository.CreateInitialWorkItem)
                        : new CreateWorkItemDelegate(_repository.CreateWorkItem);
                    break;
                case DocumentStatus.Approved:
                    method = new CreateWorkItemDelegate(_repository.CreateFinalWorkItem);
                    break;
                default:
                    method = new CreateWorkItemDelegate(_repository.CreateWorkItem);
                    break;
            }

            return method;
        }

        private WorkItemViewModel GetNewWorkItem(CodeActivityContext context)
        {
            DateTime current = DateTime.Now;
            var workItem = new WorkItemViewModel()
            {
                CreatedById = context.GetValue(CreatedById),
                TargetId = Convert.ToInt32(context.GetValue(Target)),
                Number = GenerateNumber(),
                Date = current.Date,
                Time = current.TimeOfDay,
                Title = context.GetValue(Title),
                DocumentType = "Transaction",
                DocumentId = context.GetValue(DocumentId),
                Remarks = context.GetValue(Remarks),
                Status = context.GetValue(Status),
                OperationalStatus = context.GetValue(OperationalStatus)
            };

            return workItem;
        }

        private IWorkItemRepository _repository;
    }

    internal delegate void CreateWorkItemDelegate(WorkItemViewModel workItem);
}
