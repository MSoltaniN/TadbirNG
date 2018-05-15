using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Model.Metadata
{
    public partial class Command
    {
        /// <summary>
        /// مجموعه ای از دستورات زیرشاخه (فرزند) در ساختار درختی
        /// </summary>
        public IList<Command> Children { get; protected set; }

        /// <summary>
        /// شناسه دیتابیسی دستور والد در ساختار درختی
        /// </summary>
        public int? ParentId { get; set; }

        /// <summary>
        /// شناسه دیتابیسی دسترسی مورد نیاز برای فعال کردن دستور در برنامه
        /// </summary>
        public int? PermissionId { get; set; }
    }
}
