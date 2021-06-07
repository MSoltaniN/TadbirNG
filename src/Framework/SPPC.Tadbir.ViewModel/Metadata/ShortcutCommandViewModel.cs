using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.ViewModel.Metadata
{
    public partial class ShortcutCommandViewModel
    {
        /// <summary>
        /// شناسه دیتابیسی دسترسی امنیتی مورد نیاز برای اجرای عملیات مرتبط با این کلید میانبر
        /// </summary>
        public int? PermissionId { get; set; }
    }
}
