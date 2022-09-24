using System;

namespace SPPC.Tadbir.Utility.Model
{
    /// <summary>
    /// مقادیر پیش فرض برای تنظیمات داخلی سرویس های داکری برنامه را نگهداری می کند
    /// </summary>
    public static class BuildSettingValues
    {
        /// <summary>
        /// نام پیش فرض برای آدرس وب ریشه سرویس ها در حالت نصب محلی
        /// </summary>
        public const string LocalHostUrl = "localhost";

        /// <summary>
        /// آدرس وب ویژه برای دسترسی سرویس های داکر به سیستم فیزیکی میزبان
        /// </summary>
        public const string DockerHostInternalUrl = "host.docker.internal";

        /// <summary>
        /// شماره پورت پیش فرض برای دسترسی به سرویس برنامه وب از طریق مرورگر
        /// </summary>
        public const int DefaultAppPort = 9090;

        /// <summary>
        /// شماره پورت پیش فرض برای دسترسی به سرویس محلی مجوزدهی
        /// </summary>
        public const int DefaultLicenseApiPort = 9093;

        /// <summary>
        /// شماره پورت پیش فرض برای دسترسی به سرویس آنلاین مجوزدهی
        /// </summary>
        public const int DefaultWebLicenseApiPort = 9094;

        /// <summary>
        /// شماره پورت پیش فرض برای دسترسی به سرویس اصلی برنامه
        /// </summary>
        public const int DefaultApiPort = 9095;

        /// <summary>
        /// شناسه کلید شناسایی پیش فرض برنامه که با کلید شناسایی مشتری جایگزین می شود
        /// </summary>
        public const string DummyInstanceKey = "AppInstanceKey";
    }
}
