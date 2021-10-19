using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Configuration.Models
{
    /// <summary>
    /// تنظیمات جاری و پیش فرض سفارشی سازی عناوین یک فرم گزارشی را نگهداری می کند
    /// </summary>
    public class FormLabelFullConfig
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        public FormLabelFullConfig()
        {
            Current = new FormLabelConfig();
            Default = new FormLabelConfig();
        }

        /// <summary>
        /// تنظیمات جاری برای سفارشی سازی عناوین
        /// </summary>
        public FormLabelConfig Current { get; set; }

        /// <summary>
        /// تنظیمات پیش فرض برای سفارشی سازی عناوین
        /// </summary>
        public FormLabelConfig Default { get; set; }
    }
}
