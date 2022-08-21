using System;

namespace SPPC.Tadbir.Configuration
{
    /// <summary>
    /// مقادیر پارامترهای مرتبط با داکر را نگهداری می کند
    /// </summary>
    public class DockerParameters
    {
        /// <summary>
        /// پیشوند مورد استفاده برای ایمیج های داکر که معمولا نام حساب کاربری سرویس داکرهاب است
        /// </summary>
        public string HubHandle { get; set; }

        /// <summary>
        /// پارامترهای مورد استفاده در سرویس داکری مجوزدهی
        /// </summary>
        public DockerServiceParameters License { get; set; }

        /// <summary>
        /// پارامترهای مورد استفاده در سرویس وب اصلی برنامه
        /// </summary>
        public DockerServiceParameters Api { get; set; }

        /// <summary>
        /// پارامترهای مورد استفاده در سرویس داکری سرور دیتابیس
        /// </summary>
        public DockerServiceParameters Db { get; set; }

        /// <summary>
        /// پارامترهای مورد استفاده در سرویس داکری برنامه وب تدبیر
        /// </summary>
        public DockerServiceParameters App { get; set; }
    }
}
