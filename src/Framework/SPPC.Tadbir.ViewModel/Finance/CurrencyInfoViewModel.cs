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
        /// نمایه استاندارد بین المللی ارز، که معمولاً سه حرفی است
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// آخرین نرخ وارد شده نسبت به ارز پیش فرض برای ارز
        /// </summary>
        public double LastRate { get; set; }

        /// <summary>
        /// تعداد ارقام اعشار مورد نیاز برای ارز
        /// </summary>
        public short DecimalCount { get; set; }
    }
}
