using System;

namespace SPPC.Tadbir.Configuration
{
    /// <summary>
    /// مدل اطلاعاتی برای تنظیمات رشته های اتصال در فایل پیکربندی سرویس وب
    /// </summary>
    public class ConnectionStringsModel
    {
        /// <summary>
        /// نام رشته اتصال برای کار با دیتابیس سیستمی تدبیر
        /// </summary>
        public string TadbirSysApi { get; set; }
    }
}
