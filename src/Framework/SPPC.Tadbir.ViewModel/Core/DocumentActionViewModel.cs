using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.ViewModel.Core
{
    public partial class DocumentActionViewModel
    {
        /// <summary>
        /// تاریخ آخرین تغییر روی مستند
        /// </summary>
        public DateTime ModifiedDate { get; set; }

        /// <summary>
        /// شناسه دیتابیسی کاربری که مستند را ایجاد کرده است
        /// </summary>
        public int CreatedById { get; set; }

        /// <summary>
        /// شناسه دیتابیسی کاربری که آخرین تغییر را روی مستند انجام داده است
        /// </summary>
        public int ModifiedById { get; set; }

        /// <summary>
        /// شناسه دیتابیسی کاربری که آخرین تایید مستند را انجام داده است
        /// </summary>
        public int ConfirmedById { get; set; }

        /// <summary>
        /// شناسه دیتابیسی کاربری که آخرین تصویب مستند را انجام داده است
        /// </summary>
        public int ApprovedById { get; set; }

        /// <summary>
        /// شناسه دیتابیسی ردیف (آرتیکل) سند اداری مرتبط با مستند
        /// </summary>
        public int LineId { get; set; }
    }
}
