using System;

namespace SPPC.Tadbir.Configuration
{
    /// <summary>
    /// پارامترهای اصلی مورد نیاز در یک سرویس داکری را نگهداری می کند
    /// </summary>
    public class DockerServiceParameters
    {
        /// <summary>
        /// نام نمایشی سرویس
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// نام انتخاب شده برای ایمیج سرویس
        /// </summary>
        public string ImageName { get; set; }

        /// <summary>
        /// برچسب مورد استفاده برای ایمیج سرویس
        /// </summary>
        public string Tag { get; set; }
    }
}
