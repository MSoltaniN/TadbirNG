using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Values
{
    /// <summary>
    /// شناسه های دیتابیسی مربوط به نقش های سیستمی را در یک کلاس مرکزی نعریف می کند.
    /// </summary>
    public sealed class SystemRoles
    {
        private SystemRoles()
        {
        }

        /// <summary>
        /// شناسه دیتابیسی نقش راهبر سیستم
        /// </summary>
        public const int AdminRoleId = 1;

        /// <summary>
        /// شناسه دیتابیسی نقش کارشناس حسابداری
        /// </summary>
        public const int AccountantRoleId = 2;

        /// <summary>
        /// شناسه دیتابیسی نقش رییس حسابداری
        /// </summary>
        public const int LeadAccountant = 3;

        /// <summary>
        /// شناسه دیتابیسی نقش معاون مالی
        /// </summary>
        public const int CFOAssistant = 4;

        /// <summary>
        /// شناسه دیتابیسی نقش مدیر مالی
        /// </summary>
        public const int CFO = 5;
    }
}
