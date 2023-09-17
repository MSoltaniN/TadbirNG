﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPPC.Tadbir.Configuration.Models
{
    /// <summary>
    /// تنظیمات فرآیند و ثبت مالی فرم دریافت را نگهداری می‌کند
    /// </summary>
    public class ReceiptConfig
    {
        /// <summary>
        /// تنظیمات فرآیند ثبت مالی فرم دریافت
        /// </summary>
        public RegisterFlowConfig RegisterFlowConfig { get; set; }

        /// <summary>
        /// سیاست‌های انتخاب یا ایجاد سند برای ثبت مالی فرم دریافت
        /// </summary>
        public RegisterConfig RegisterConfig { get; set; }
    }
}
