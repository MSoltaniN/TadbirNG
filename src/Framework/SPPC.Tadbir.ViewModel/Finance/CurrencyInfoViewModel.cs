using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.ViewModel.Finance
{
    /// <summary>
    /// اطلاعات کلی یکی از ارزهای تعریف شده را نگهداری می کند
    /// </summary>
    public class CurrencyInfoViewModel
    {
        /// <summary>
        /// شناسه دیتابیسی ارز
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// نام محلی شده ارز
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// آخرین نرخ وارد شده نسبت به ارز پیش فرض برای ارز
        /// </summary>
        public double LastRate { get; set; }
    }
}
