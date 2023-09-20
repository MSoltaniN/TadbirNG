using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPPC.Tadbir.Configuration.Models
{
    /// <summary>
    /// تنظیمات فرآیند و ثبت مالی فرم‌های عملیاتی را نگهداری می‌کند
    /// </summary>
    public class OperationalFormsConfig
    {
        /// <summary>
        /// تنظیمات فرآیند ثبت مالی فرم‌های عملیاتی
        /// </summary>
        public RegisterFlowConfig RegisterFlowConfig { get; set; }

        /// <summary>
        /// سیاست‌های انتخاب یا ایجاد سند برای ثبت مالی فرم‌های عملیاتی
        /// </summary>
        public RegisterConfig RegisterConfig { get; set; }
    }
}
