using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPPC.Tadbir.Configuration.Models
{
    /// <summary>
    /// سیاست‌های انتخاب یا ایجاد سند برای ثبت مالی در فرم‌های عملیاتی را نگهداری می‌کند
    /// </summary>
    public class RegisterConfig
    {
        /// <summary>
        /// ارسال ثبت‌های مالی به آخرین سند باز معتبر
        /// </summary>
        public bool RegisterOnLastValidVoucher { get; set; }

        /// <summary>
        /// ایجاد یک سند حسابداری جدید برای هر فرم عملیاتی
        /// </summary>
        public bool RegisterOnCreatedVoucher { get; set; }
        
        /// <summary>
        /// ثبت خودکار سند حسابداری ایجاد شده
        /// </summary>
        public bool CheckedVoucher { get; set; }
    }
}
