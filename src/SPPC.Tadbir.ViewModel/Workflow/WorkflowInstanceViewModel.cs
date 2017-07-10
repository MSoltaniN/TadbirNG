using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.ViewModel.Workflow
{
    /// <summary>
    /// این مدل نمایشی اطلاعات مربوط به یک گردش کار در حال اجرا را نمایش می دهد.
    /// </summary>
    public class WorkflowInstanceViewModel
    {
        public WorkflowInstanceViewModel()
        {
            InstanceId = String.Empty;
            DocumentType = String.Empty;
            WorkflowName = String.Empty;
            EditionName = String.Empty;
            State = String.Empty;
            LastActionDate = String.Empty;
        }

        public string InstanceId { get; set; }

        /// <summary>
        /// شناسه دیتابیسی مستند مرتبط (ضمیمه) این کار
        /// </summary>
        public int DocumentId { get; set; }

        /// <summary>
        /// نوع موجودیت مرتبط با کار
        /// </summary>
        public string DocumentType { get; set; }

        /// <summary>
        /// نوع گردش کار مورد استفاده در این نمونه گردش کار
        /// </summary>
        public string WorkflowName { get; set; }

        /// <summary>
        /// ویرایش مورد استفاده در این نمونه گردش کار
        /// </summary>
        public string EditionName { get; set; }

        /// <summary>
        /// آخرین وضعیت گردش کار (کاربرد در گردش های کاری از نوع ماشین حالت)
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// تاریخ شمسی و زمان آخرین اقدام انجام شده در این نمونه گردش کار
        /// </summary>
        public string LastActionDate { get; set; }
    }
}
