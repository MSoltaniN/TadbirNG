using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Model.Metadata
{
    public partial class ShortcutCommand
    {
        /// <summary>
        /// شناسه دیتابیسی دسترسی امنیتی مورد نیاز برای اجرای عملیات مرتبط با این کلید میانبر
        /// </summary>
        public virtual int? PermissionId { get; set; }
    }
}
