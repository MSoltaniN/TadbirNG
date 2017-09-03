using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Security
{
    /// <summary>
    /// نام موجودیت هایی را که یک یا چند دسترسی امنیتی برای آنها تعریف شده مشخص می کند
    /// </summary>
    public sealed class SecureEntity
    {
        private SecureEntity()
        {
        }

        /// <summary>
        /// نام موجودیت سرفصل حسابداری
        /// </summary>
        public const string Account = "Account";

        /// <summary>
        /// نام موجودیت سند مالی
        /// </summary>
        public const string Transaction = "Transaction";

        /// <summary>
        /// نام موجودیت کاربر برنامه
        /// </summary>
        public const string User = "User";

        /// <summary>
        /// نام موجودیت نقش سازمانی
        /// </summary>
        public const string Role = "Role";

        /// <summary>
        /// نام موجودیت درخواست کالا
        /// </summary>
        public const string Requisition = "RequisitionVoucher";
    }
}
