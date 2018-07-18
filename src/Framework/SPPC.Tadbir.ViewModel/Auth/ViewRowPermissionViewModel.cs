using System;
using System.Collections.Generic;
using System.Text;

namespace SPPC.Tadbir.ViewModel.Auth
{
    public partial class ViewRowPermissionViewModel
    {
        /// <summary>
        /// شناسه دیتابیسی نقش امنیتی که محدودیت دسترسی به سطرهای اطلاعاتی برای آن تعریف می شود
        /// </summary>
        public int RoleId { get; set; }

        /// <summary>
        /// شناسه دیتابیسی موجودیتی که محدودیت دسترسی به سطرهای اطلاعاتی برای آن تعریف می شود
        /// </summary>
        public int ViewId { get; set; }
    }
}
