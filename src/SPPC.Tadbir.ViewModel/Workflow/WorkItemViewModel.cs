using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.ViewModel.Workflow
{
    public partial class WorkItemViewModel
    {
        /// <summary>
        /// شناسه دیتابیسی کاربری که این کار توسط او ایجاد شده است
        /// </summary>
        public int CreatedById { get; set; }

        /// <summary>
        /// نام نقش گیرنده این کار
        /// </summary>
        public string TargetRole { get; set; }

        /// <summary>
        /// شناسه دیتابیسی مستند مرتبط (ضمیمه) این کار
        /// </summary>
        public int DocumentId { get; set; }
    }
}
