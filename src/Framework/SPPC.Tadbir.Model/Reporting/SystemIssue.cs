using System;
using System.Collections.Generic;
using System.Text;

namespace SPPC.Tadbir.Model.Reporting
{
    public partial class SystemIssue
    {
        /// <summary>
        /// شناسه دیتابیسی دستور والد در ساختار درختی
        /// </summary>
        public int? ParentId { get; set; }

        /// <summary>
        /// شناسه دیتابیسی دسترسی مورد نیاز
        /// </summary>
        public int? PermissionID { get; set; }

        /// <summary>
        /// شناسه دیتابیسی موجودیت
        /// </summary>
        public int? ViewId { get; set; }
    }
}
