using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Configuration.Models
{
    /// <summary>
    /// تنظیمات گزارش فوری را برای کاربر جاری نگهداری می کند
    /// </summary>
    public class QuickReportConfig
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        public QuickReportConfig()
        {
        }

        /// <summary>
        /// شناسه دیتابیسی نمای اطلاعاتی گزارش فوری
        /// </summary>
        public int ViewId { get; set; }

        /// <summary>
        /// عنوان گزارش فوری به صورت متن محلی شده
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// لیست ستون های گرید
        /// </summary>
        public IList<QuickReportColumnConfig> Columns { get; set; }

        /// <summary>
        /// لیست پارامتر های فرم برای گزارش فوری
        /// </summary>
        public IList<QuickReportParameterConfig> Parameters { get; set; }
    }
}
