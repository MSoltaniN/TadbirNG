using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Configuration.Models
{
    /// <summary>
    /// تنظیمات مورد استفاده برای سفارشی سازی عناوین یک فرم گزارشی را نگهداری می کند
    /// </summary>
    public class FormLabelConfig
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        public FormLabelConfig()
        {
            LabelMap = new Dictionary<string, string>();
        }

        /// <summary>
        /// شناسه دیتابیسی فرم گزارشی مرتبط با تنظیمات
        /// </summary>
        public int FormId { get; set; }

        /// <summary>
        /// شناسه دیتابیسی زبان استفاده شده در متن عناوین
        /// </summary>
        public int LocaleId { get; set; }

        /// <summary>
        /// مجموعه عناوین قابل سفارشی سازی در فرم
        /// </summary>
        public IDictionary<string, string> LabelMap { get; }
    }
}
