using System.Collections.Generic;

namespace SPPC.Tadbir.Utility.Model
{
    /// <summary>
    /// اطلاعات نسخه نصب شده برنامه را نگهداری می کند
    /// </summary>
    public class VersionInfo
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        public VersionInfo()
        {
            Services = new List<ServiceInfo>();
        }

        /// <summary>
        /// نسخه برنامه نصب شده
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// ویرایش برنامه نصب شده
        /// </summary>
        public string Edition { get; set; }

        /// <summary>
        /// اطلاعات سرویس های داکری موجود در برنامه
        /// </summary>
        public List<ServiceInfo> Services { get; }
    }
}
