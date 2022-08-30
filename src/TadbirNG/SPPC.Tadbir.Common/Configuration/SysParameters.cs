using System;

namespace SPPC.Tadbir.Configuration
{
    /// <summary>
    /// اطلاعات پارامترهای سیستمی برنامه را نگهداری می کند
    /// </summary>
    public class SysParameters
    {
        /// <summary>
        /// پارامترهای سیستمی مرتبط با دیتابیس های برنامه
        /// </summary>
        public DbParameters Db { get; set; }

        /// <summary>
        /// پارامترهای سیستمی مرتبط با سرویس های داکری برنامه
        /// </summary>
        public DockerParameters Docker { get; set; }

        /// <summary>
        /// پارامترهای سیستمی مرتبط با سرویس شناسه سخت افزاری
        /// </summary>
        public ServiceParameters Service { get; set; }

        /// <summary>
        /// پارامترهای سیستمی مرتبط با سرورهای سازمانی مورد استفاده برنامه
        /// </summary>
        public ServerParameters Servers { get; set; }
    }
}
