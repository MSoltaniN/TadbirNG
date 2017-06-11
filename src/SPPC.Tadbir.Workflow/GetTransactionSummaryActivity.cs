using System;
using System.Activities;
using SPPC.Framework.Unity.WF;
using SPPC.Tadbir.NHibernate;
using SPPC.Tadbir.ViewModel.Finance;
using SwForAll.Platform.Common;

namespace SPPC.Tadbir.Workflow
{
    public sealed class GetTransactionSummaryActivity : CodeActivity<TransactionSummaryViewModel>
    {
        [RequiredArgument]
        public InArgument<int> TransactionId { get; set; }

        protected override TransactionSummaryViewModel Execute(CodeActivityContext context)
        {
            Verify.ArgumentNotNull(context, "context");
            InitializeDependencies(context);
            int transactionId = context.GetValue(TransactionId);
            var summary = _repository.GetTransactionSummary(transactionId);
            return summary;
        }

        private void InitializeDependencies(CodeActivityContext context)
        {
            _repository = context.GetDependency<ITransactionRepository>("WF");
        }

        private ITransactionRepository _repository;
    }
}
