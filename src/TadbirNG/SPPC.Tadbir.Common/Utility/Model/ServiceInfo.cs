using System;

namespace SPPC.Tadbir.Utility.Model
{
    /// <summary>
    /// اطلاعات سیستمی یکی از سرویس های داکری برنامه را نگهداری می کند
    /// </summary>
    public class ServiceInfo
    {
        /// <summary>
        /// نام سرویس داکری
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// آدرس وب برای دریافت آخرین نسخه سرویس
        /// </summary>
        public string SourceUrl { get; set; }

        /// <summary>
        /// شناسه یکتای به دست آمده از درهم سازی فایل فشرده سرویس
        /// </summary>
        public string Sha256 { get; set; }

        /// <summary>
        /// اندازه فایل فشرده سرویس بر حسب بایت
        /// </summary>
        public int Size { get; set; }
    }
}
