using System;
using System.Collections.Generic;
using SPPC.Tadbir.ViewModel.Core;

namespace SPPC.Tadbir.ViewModel.Finance
{
    public partial class TransactionViewModel
    {
        /// <summary>
        /// شناسه دیتابیسی دوره مالی که این سرفصل حسابداری در آن تعریف شده است
        /// </summary>
        public int FiscalPeriodId { get; set; }

        /// <summary>
        /// شناسه دیتابیسی شعبه سازمانی که این سرفصل حسابداری در آن تعریف شده است
        /// </summary>
        public int BranchId { get; set; }

        /// <summary>
        /// شناسه دیتابیسی کار مرتبط با این سند مالی در کارتابل، در صورت وجود
        /// </summary>
        public int WorkItemId { get; set; }

        /// <summary>
        /// شناسه دیتابیسی نقش گیرنده کار مرتبط با این سند مالی در کارتابل، در صورت وجود
        /// </summary>
        public int WorkItemTargetId { get; set; }

        /// <summary>
        /// آخرین اقدام صورت گرفته روی کار مرتبط با این سند مالی در کارتابل، در صورت وجود
        /// </summary>
        public string WorkItemAction { get; set; }

        /// <summary>
        /// جمع مقادیر بدهکار در آرتیکل های سند
        /// </summary>
        public decimal DebitSum { get; set; }

        /// <summary>
        /// جمع مقادیر بستانکار در آرتیکل های سند
        /// </summary>
        public decimal CreditSum { get; set; }

        /// <summary>
        /// مدل نمایشی مستند اداری مرتبط با این سند مالی
        /// </summary>
        public DocumentViewModel Document { get; set; }
    }
}
