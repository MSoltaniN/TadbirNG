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
        /// نام موجودیت تفصیلی شناور
        /// </summary>
        public const string DetailAccount = "DetailAccount";

        /// <summary>
        /// نام موجودیت مرکز هزینه
        /// </summary>
        public const string CostCenter = "CostCenter";

        /// <summary>
        /// نام موجودیت پروژه
        /// </summary>
        public const string Project = "Project";

        /// <summary>
        /// نام موجودیت مجازی ارتباطات بردار حساب
        /// </summary>
        public const string AccountRelations = "AccountRelations";

        /// <summary>
        /// نام موجودیت ارز
        /// </summary>
        public const string Currency = "Currency";

        /// <summary>
        /// نام موجودیت دوره مالی
        /// </summary>
        public const string FiscalPeriod = "FiscalPeriod";

        /// <summary>
        /// نام موجودیت شعبه سازمانی
        /// </summary>
        public const string Branch = "Branch";

        /// <summary>
        /// نام موجودیت واحد سازمانی
        /// </summary>
        public const string BusinessUnit = "BusinessUnit";

        /// <summary>
        /// نام موجودیت شریک تجاری
        /// </summary>
        public const string BusinessPartner = "BusinessPartner";

        /// <summary>
        /// نام موجودیت سند مالی
        /// </summary>
        public const string Voucher = "Voucher";

        /// <summary>
        /// نام موجودیت کاربر برنامه
        /// </summary>
        public const string User = "User";

        /// <summary>
        /// نام موجودیت نقش سازمانی
        /// </summary>
        public const string Role = "Role";

        /// <summary>
        /// نام موجودیت موجودی کالا
        /// </summary>
        public const string ProductInventory = "ProductInventory";

        /// <summary>
        /// نام موجودیت درخواست کالا
        /// </summary>
        public const string Requisition = "RequisitionVoucher";
    }
}
