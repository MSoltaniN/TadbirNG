using System;

namespace SPPC.Tadbir.Configuration
{
    /// <summary>
    /// مدل اطلاعاتی برای سطوح مختلف ایجاد لاگ در زیرساخت سرویس وب
    /// </summary>
    public class LogLevelModel
    {
        /// <summary>
        /// سطح پیش فرض برای ایجاد لاگ
        /// </summary>
        public string Default { get; set; }

        /// <summary>
        /// سطح ایجاد لاگ مورد استفاده در زیرساخت های نرم افزاری مایکروسافت
        /// </summary>
        public string Microsoft { get; set; }
    }
}
