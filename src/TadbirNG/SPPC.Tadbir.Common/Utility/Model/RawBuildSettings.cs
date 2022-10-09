using SPPC.Framework.Helpers;

namespace SPPC.Tadbir.Utility.Model
{
    /// <summary>
    /// اطلاعات تنظیمات دیتابیس و سرویس های برنامه را نگهداری می کند
    /// </summary>
    public class RawBuildSettings : IBuildSettings
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        public RawBuildSettings()
        {
            Tcp = new RemoteConnection();
        }

        /// <summary>
        /// آدرس وب سرور سازمانی موجود برای کنترل مجوزهای برنامه
        /// </summary>
        public string OnlineServerRoot { get; set; }

        /// <summary>
        /// آدرس وب سرور محلی برنامه برای مجوزدهی
        /// </summary>
        public string LocalServerRoot { get; set; }

        /// <summary>
        /// آدرس خاص سرور محلی مجوزدهی برای دسترسی از داخل سرویس های داکر
        /// </summary>
        public string LocalServerUrl { get; set; }

        /// <summary>
        /// آدرس وب سرویس اصلی برنامه
        /// </summary>
        public string WebApiUrl { get; set; }

        /// <summary>
        /// اطلاعات اتصال از راه دور به سرویس شناسه سخت افزاری
        /// </summary>
        public RemoteConnection Tcp { get; }

        /// <summary>
        /// نام سرور دیتابیس اصلی برنامه
        /// </summary>
        public string DbServerName { get; set; }

        /// <summary>
        /// نام لاگین دیتابیسی پیش فرض برای اتصال به دیتابیس های برنامه
        /// </summary>
        public string DbUserName { get; set; }

        /// <summary>
        /// رمز عبور لاگین پیش فرض پیش فرض برای اتصال به دیتابیس های برنامه
        /// </summary>
        public string DbPassword { get; set; }

        /// <summary>
        /// رمز عبور لاگین راهبر سیستم - مورد استفاده برای اتصال به سرور دیتابیسی فیزیکی کاربر
        /// </summary>
        public string SaPassword { get; set; }

        /// <summary>
        /// کلید شناسایی این نسخه از برنامه
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// نسخه فعلی برنامه در حال نصب یا به روزرسانی
        /// </summary>
        public string Version { get; set; }
    }
}
