using System;

namespace SPPC.Tadbir.Configuration
{
    /// <summary>
    /// تنظیمات سرورهای سازمانی مورد نیاز برنامه تدبیر را نگهداری می کند
    /// </summary>
    public class ServerParameters
    {
        /// <summary>
        /// آدرس آی پی استاتیک سرور آنلاین ویندوز
        /// </summary>
        public string WinIpAddress { get; set; }

        /// <summary>
        /// آدرس آی پی استاتیک سرور آنلاین لینوکس
        /// </summary>
        public string LinuxIpAddress { get; set; }

        /// <summary>
        /// آدرس وب سرور آنلاین مورد استفاده برای مجوزدهی برنامه
        /// </summary>
        public string License { get; set; }

        /// <summary>
        /// آدرس وب سرور آنلاین مورد استفاده برای به روزرسانی برنامه
        /// </summary>
        public string Update { get; set; }
    }
}
