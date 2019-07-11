using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Configuration.Models
{
    /// <summary>
    /// تنظیمات یکی از پارامترهای گزارش فوری را نگهداری می کند
    /// </summary>
    public class QuickReportParameterConfig
    {
        /// <summary>
        /// شناسه متنی پارامتر که برای یک گزارش باید غیرتکراری باشد
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// کلید متن چندزبانه برای عنوان پارامتر در فرم پارامترها
        /// </summary>
        public string CaptionKey { get; set; }

        /// <summary>
        /// کلید متن چندزبانه برای شرح معنی و عملکرد پارامتر در فرم پارامترها
        /// </summary>
        public string DescriptionKey { get; set; }

        /// <summary>
        /// مقدار پیش فرض برای پارامتر جهت پیشنهاد اولیه به کاربر
        /// </summary>
        public string DefaultValue { get; set; }
    }
}
