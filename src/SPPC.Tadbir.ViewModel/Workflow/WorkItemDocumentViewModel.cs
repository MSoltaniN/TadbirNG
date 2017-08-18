using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.ViewModel.Workflow
{
    public partial class WorkItemDocumentViewModel
    {
        /// <summary>
        /// شناسه دیتابیسی آخرین کاری که در جریان اجرای گردش کار روی این موجودیت انجام شده است
        /// </summary>
        public int WorkItemId { get; set; }

        /// <summary>
        /// شناسه دیتابیسی موجودیت مرتبط (ضمیمه) این کار
        /// </summary>
        public int EntityId { get; set; }
    }
}
