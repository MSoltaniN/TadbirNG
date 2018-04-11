using System;
using System.Activities;
using SPPC.Framework.Common;
using SPPC.Tadbir.Metadata.Workflow;
using SPPC.Tadbir.Persistence;
using SPPC.Workflow.Unity;

namespace SPPC.Tadbir.Workflow.Activities
{
    /// <summary>
    /// این فعالیت اطلاعات فراداده ای گردش کار تغییر وضعیت را برای یک نوع مستند اداری می خواند.
    /// </summary>
    public sealed class GetWorkflowMetadataActivity : CodeActivity<StateWorkflow>
    {
        /// <summary>
        /// آرگومان اجباری برای نگهداری نوع مستند اداری که اطلاعات فراداده ای آن مورد نیاز است
        /// </summary>
        public InArgument<string> DocumentType { get; set; }

        /// <summary>
        /// فعالیت را با استفاده از اطلاعات جاری محیطی اجرا می کند.
        /// </summary>
        /// <param name="context">اطلاعات محیط اجرایی فعالیت در زمان اجرای آن</param>
        /// <returns>اطلاعات فراداده ای گردش کار. اگر نوع مستند اداری داده شده وجود نداشته باشد
        /// مقدار null برمیگرداند.</returns>
        protected override StateWorkflow Execute(CodeActivityContext context)
        {
            Verify.ArgumentNotNull(context, "context");
            InitializeDependencies(context);
            string documentType = context.GetValue(DocumentType);
            ////var stateWorkflow = _repository.GetStateWorkflow(documentType);
            ////return stateWorkflow;
            throw ExceptionBuilder.NewNotImplementedException("Workflow functionality is currently disabled.");
        }

        private void InitializeDependencies(CodeActivityContext context)
        {
            _repository = context.GetDependency<IMetadataRepository>();
        }

        private IMetadataRepository _repository;
    }
}
