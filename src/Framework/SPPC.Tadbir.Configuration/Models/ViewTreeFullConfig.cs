using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Configuration.Models
{
    /// <summary>
    /// تنظیمات جاری و پیش فرض یک ساختار اطلاعاتی درختی (سلسله مراتبی) را نگهداری می کند
    /// </summary>
    public class ViewTreeFullConfig
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        public ViewTreeFullConfig()
        {
            Current = new ViewTreeConfig();
            Default = new ViewTreeConfig();
        }

        /// <summary>
        /// تنظیمات جاری برای این ساختار درختی
        /// </summary>
        public ViewTreeConfig Current { get; set; }

        /// <summary>
        /// تنظیمات پیش فرض برای این ساختار درختی
        /// </summary>
        public ViewTreeConfig Default { get; set; }
    }
}
