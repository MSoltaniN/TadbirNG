using SPPC.Framework.Helpers;

namespace SPPC.Tadbir.Utility
{
    /// <summary>
    /// تنظیمات مورد نیاز برای سرویس های داکر و سرور دیتابیس برنامه را تعریف می کند
    /// </summary>
    public interface IBuildSettings
    {
        /// <summary>
        /// آدرس سرویس  آنلاین لایسنس در نمونه نصب شده
        /// </summary>
        string OnlineServerRoot { get; set; }

        /// <summary>
        /// آدرس سرویس  آفلاین لایسنس در نمونه نصب شده
        /// </summary>
        string LocalServerRoot { get; set; }

        /// <summary>
        /// آدرس سرویس  آفلاین لایسنس در نمونه نصب شده
        /// </summary>
        string LocalServerUrl { get; set; }

        /// <summary>
        /// آدرس سرویس وب در نمونه نصب شده
        /// </summary>
        string WebApiUrl { get; set; }

        /// <summary>
        /// مشخصات اتصال از راه دور به سیستم عامل مقصد
        /// </summary>
        RemoteConnection Tcp { get; }

        /// <summary>
        /// نام سرور دیتابیسی در برنامه نصب شده
        /// </summary>
        string DbServerName { get; set; }

        /// <summary>
        /// نام لاگین پیش فرض برای اتصال به دیتابیس های برنامه
        /// </summary>
        string DbUserName { get; set; }

        /// <summary>
        /// رمز عبور لاگین پیش فرض برای اتصال به دیتابیس های برنامه
        /// </summary>
        string DbPassword { get; set; }

        /// <summary>
        /// رمز عبور لاگین راهبر سیستم برای اتصال به سرور دیتابیس فیزیکی کاربر
        /// </summary>
        string SaPassword { get; set; }

        /// <summary>
        /// شناسه برنامه نصب شده
        /// </summary>
        string Key { get; set; }

        /// <summary>
        /// نسخه برنامه نصب شده
        /// </summary>
        string Version { get; set; }
    }
}
