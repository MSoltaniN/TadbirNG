using System;
using System.Activities;
using SPPC.Framework.Common;
using SPPC.Tadbir.Persistence;
using SPPC.Workflow.Unity;

namespace SPPC.Tadbir.Workflow
{
    /// <summary>
    /// این فعالیت با تغییر شناسه دیتابیسی نقش گیرنده یک کار در کارتابل،
    /// عملا آن کار را به کارتابل نقش جدید منتقل می کند.
    /// </summary>
    public sealed class MoveCartableItemActivity : CodeActivity
    {
        /// <summary>
        /// شناسه دیتابیسی مستند مرتبط با یک کار موجود
        /// </summary>
        [RequiredArgument]
        public InArgument<int> DocumentId { get; set; }

        /// <summary>
        /// نوع مستند مرتبط با یک کار موجود
        /// </summary>
        [RequiredArgument]
        public InArgument<string> DocumentType { get; set; }

        /// <summary>
        /// شناسه دیتابیسی نقش سازمانی که باید گیرنده جدید برای کار باشد
        /// </summary>
        [RequiredArgument]
        public InArgument<int> NewTargetId { get; set; }

        /// <summary>
        /// فعالیت را با استفاده از اطلاعات جاری محیطی اجرا می کند.
        /// </summary>
        /// <param name="context">اطلاعات محیط اجرایی فعالیت در زمان اجرای آن</param>
        protected override void Execute(CodeActivityContext context)
        {
            Verify.ArgumentNotNull(context, "context");
            InitializeDependencies(context);
            int documentId = context.GetValue(DocumentId);
            string documentType = context.GetValue(DocumentType);
            int newTargetId = context.GetValue(NewTargetId);
            _repository.UpdateWorkItemTarget(documentId, documentType, newTargetId);
        }

        private void InitializeDependencies(CodeActivityContext context)
        {
            _repository = context.GetDependency<IWorkItemRepository>("WF");
        }

        private IWorkItemRepository _repository;
    }
}
