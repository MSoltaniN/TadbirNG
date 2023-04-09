using System;

namespace SPPC.Tadbir.Common
{
    /// <summary>
    /// مقادیر ثابت مورد نیاز برای کار با اسکریپت های دیتابیسی برنامه را فراهم می کند
    /// </summary>
    public static class ScriptConstants
    {
        /// <summary>
        /// نام فایل اسکریپت مورد استفاده برای ایجاد ساختار دیتابیس سیستمی
        /// </summary>
        public const string SysDbCreateScript = "TadbirSys_CreateDbObjects.sql";

        /// <summary>
        /// نام فایل اسکریپت مورد استفاده برای ایجاد ساختار دیتابیس شرکتی
        /// </summary>
        public const string DbCreateScript = "Tadbir_CreateDbObjects.sql";

        /// <summary>
        /// نام فایل اسکریپت مورد استفاده برای ارتقاء ساختار دیتابیس سیستمی
        /// </summary>
        public const string SysDbUpdateScript = "TadbirSys_UpdateDbObjects.sql";

        /// <summary>
        /// نام فایل اسکریپت مورد استفاده برای ارتقاء ساختار دیتابیس شرکتی
        /// </summary>
        public const string DbUpdateScript = "Tadbir_UpdateDbObjects.sql";

        /// <summary>
        /// عبارت باقاعده مورد استفاده برای پیدا کردن بلوک های دستوری درون اسکریپت های ارتقاء
        /// </summary>
        public const string ScriptBlockRegex = @"-- (\d{1,}).(\d{1,}).(\d{1,})";
    }
}
