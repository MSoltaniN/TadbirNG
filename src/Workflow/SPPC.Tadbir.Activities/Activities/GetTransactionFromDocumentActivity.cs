using System;
using System.Activities;
using SPPC.Framework.Common;
using SPPC.Tadbir.Persistence;
using SPPC.Tadbir.ViewModel.Finance;
using SPPC.Workflow.Unity;

namespace SPPC.Tadbir.Workflow.Activities
{
    /// <summary>
    /// این فعالیت اطلاعات خلاصه مربوط به یک سند مالی را از دیتابیس می خواند.
    /// </summary>
    public sealed class GetTransactionFromDocumentActivity : CodeActivity<TransactionSummaryViewModel>
    {
        /// <summary>
        /// آرگومان اجباری برای نگهداری شناسه دیتابیسی مستند مرتبط با سند مالی که اطلاعات خلاصه آن مورد نیاز است
        /// </summary>
        [RequiredArgument]
        public InArgument<int> DocumentId { get; set; }

        protected override TransactionSummaryViewModel Execute(CodeActivityContext context)
        {
            Verify.ArgumentNotNull(context, "context");
            InitializeDependencies(context);
            int documentId = context.GetValue(DocumentId);
            var summary = _repository.GetTransactionSummaryFromDocument(documentId);
            return summary;
        }

        private void InitializeDependencies(CodeActivityContext context)
        {
            _repository = context.GetDependency<ITransactionRepository>("WF");
        }

        private ITransactionRepository _repository;
    }
}
