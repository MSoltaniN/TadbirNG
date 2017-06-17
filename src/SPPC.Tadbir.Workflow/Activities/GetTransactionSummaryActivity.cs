using System;
using System.Activities;
using SPPC.Framework.Unity.WF;
using SPPC.Tadbir.NHibernate;
using SPPC.Tadbir.ViewModel.Finance;
using SwForAll.Platform.Common;

namespace SPPC.Tadbir.Workflow
{
    /// <summary>
    /// این فعالیت اطلاعات خلاصه مربوط به یک سند مالی را از دیتابیس می خواند.
    /// </summary>
    public sealed class GetTransactionSummaryActivity : CodeActivity<TransactionSummaryViewModel>
    {
        /// <summary>
        /// آرگومان اجباری برای نگهداری شناسه دیتابیسی سند مالی که اطلاعات خلاصه آن مورد نیاز است
        /// </summary>
        [RequiredArgument]
        public InArgument<int> TransactionId { get; set; }

        /// <summary>
        /// فعالیت را با استفاده از اطلاعات جاری محیطی اجرا می کند.
        /// </summary>
        /// <param name="context">اطلاعات محیط اجرایی فعالیت در زمان اجرای آن</param>
        /// <returns>اطلاعات خلاصه یک سند مالی موجود. اگر سند مالی با شناسه دیتابیسی داده شده وجود نداشته باشد
        /// مقدار null برمیگرداند.</returns>
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
