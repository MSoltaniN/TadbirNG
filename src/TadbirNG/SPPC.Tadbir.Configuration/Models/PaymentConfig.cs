using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPPC.Tadbir.Configuration.Models
{
    /// <summary>
    /// تنظیمات فرآیند و ثبت مالی فرم پرداخت را نگهداری می‌کند
    /// </summary>
    public class PaymentConfig
    {
        /// <summary>
        /// تنظیمات فرآیند ثبت مالی فرم پرداخت
        /// </summary>
        public RegisterFlowConfig RegisterFlowConfig { get; set; }

        /// <summary>
        /// سیاست‌های انتخاب یا ایجاد سند برای ثبت مالی فرم پرداخت
        /// </summary>
        public RegisterConfig RegisterConfig { get; set; }
    }
}
