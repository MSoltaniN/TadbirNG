using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.ViewModel.Metadata
{
    public partial class CommandViewModel
    {
        /// <summary>
        /// مشخص می کند که آیا کاربر جاری به این دستور اجرایی دسترسی دارد یا نه؟
        /// </summary>
        public bool HasPermission { get; set; }

        /// <summary>
        /// شناسه دیتابیسی دسترسی مورد نیاز برای مشاهده و اجرای این دستور
        /// </summary>
        public int? PermissionId { get; set; }

        /// <summary>
        /// مجموعه ای از دستورات زیرشاخه (فرزند) در ساختار درختی
        /// </summary>
        public IList<CommandViewModel> Children { get; protected set; }
    }
}
