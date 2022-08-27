using SPPC.Framework.Helpers;

namespace SPPC.Tadbir.Utility
{
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

        string DbUserName { get; set; }

        string DbPassword { get; set; }

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
