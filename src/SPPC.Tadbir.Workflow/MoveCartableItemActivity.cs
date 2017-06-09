using System;
using System.Activities;
using SPPC.Framework.Unity.WF;
using SPPC.Tadbir.NHibernate;

namespace SPPC.Tadbir.Workflow
{
    public sealed class MoveCartableItemActivity : CodeActivity
    {
        [RequiredArgument]
        public InArgument<int> DocumentId { get; set; }

        [RequiredArgument]
        public InArgument<string> DocumentType { get; set; }

        [RequiredArgument]
        public InArgument<int> NewTargetId { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
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
