using System.Collections.Generic;

namespace SPPC.Tadbir.Model.Reporting
{
    public partial class SystemIssue
    {
        /// <summary>
        /// شناسه دیتابیسی اشکال والد در ساختار درختی
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

        /// <summary>
        /// مجموعه ای از اشکالات زیرشاخه (فرزند) در ساختار درختی
        /// </summary>
        public IList<SystemIssue> Children { get; protected set; }
    }
}
