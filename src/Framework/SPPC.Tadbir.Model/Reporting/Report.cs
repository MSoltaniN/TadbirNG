using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Model.Reporting
{
    public partial class Report
    {
        /// <summary>
        /// شناسه دیتابیسی کاربر ایجاد کننده گزارش
        /// </summary>
        public int CreatedById { get; set; }

        /// <summary>
        /// شناسه دیتابیسی شاخه والد این گزارش در ساختار درختی گزارشات
        /// </summary>
        public int? ParentId { get; set; }
    }
}
