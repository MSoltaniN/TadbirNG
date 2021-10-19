using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Model.Auth
{
    public partial class ViewRowPermission
    {
        /// <summary>
        /// شناسه دیتابیسی نقش امنیتی که محدودیت دسترسی به سطرهای اطلاعاتی برای آن تعریف می شود
        /// </summary>
        public int RoleId { get; set; }

        /// <summary>
        /// شناسه دیتابیسی موجودیتی که محدودیت دسترسی به سطرهای اطلاعاتی برای آن تعریف می شود
        /// </summary>
        public int ViewID { get; set; }
    }
}
