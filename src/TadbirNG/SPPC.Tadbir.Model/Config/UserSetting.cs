using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Model.Config
{
    public partial class UserSetting
    {
        /// <summary>
        /// شناسه دیتابیسی کاربر متناظر با تنظیمات کاربری
        /// </summary>
        public int? UserId { get; set; }

        /// <summary>
        /// شناسه دیتابیسی نقش متناظر با تنظیمات برای تنظیماتی که در سطح نقش تعریف می شوند
        /// </summary>
        public int? RoleId { get; set; }
    }
}
