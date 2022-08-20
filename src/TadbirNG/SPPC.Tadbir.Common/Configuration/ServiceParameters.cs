using System;

namespace SPPC.Tadbir.Configuration
{
    /// <summary>
    /// پارامترهای مرتبط با سرویس شناسه سخت افزاری را نگهداری می کند
    /// </summary>
    public class ServiceParameters
    {
        /// <summary>
        /// نام اصلی سرویس به شکلی که در برنامه کنترل سرویس های ویندوزی نمایش داده می شود
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// عنوان سرویس به شکلی که در برنامه کنترل سرویس های ویندوزی نمایش داده می شود
        /// </summary>
        public string DisplayName { get; set; }
    }
}
