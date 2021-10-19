using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Model.Reporting
{
    public partial class Report
    {
        /// <summary>
        /// شناسه دیتابیسی کاربر ایجاد کننده گزارش
        /// </summary>
        public virtual int CreatedById { get; set; }

        /// <summary>
        /// شناسه دیتابیسی شاخه والد این گزارش در ساختار درختی گزارشات
        /// </summary>
        public virtual int? ParentId { get; set; }

        /// <summary>
        /// شناسه دیتابیسی فرمی که به عنوان منبع داده اصلی این گزارش در برنامه شناخته می شود
        /// </summary>
        public virtual int? ViewId { get; set; }
    }
}
