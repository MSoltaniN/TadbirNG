using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Configuration.Models
{
    /// <summary>
    /// اطلاعات تنظیمات مورد استفاده در نمایش مقادیر عددی و پولی را نگهداری می کند
    /// </summary>
    public class NumberDisplayConfig
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        public NumberDisplayConfig()
        {
            DecimalPrecision = 0;
            MaxPrecision = 8;
            UseSeparator = true;
            SeparatorMode = SeparatorModeOptions.UseCustom;
            SeparatorSymbol = ",";
        }

        /// <summary>
        /// تعداد ارقام اعشار مورد نیاز برای نمایش اعداد
        /// </summary>
        public short DecimalPrecision { get; set; }

        /// <summary>
        /// حداکثر ارقام اعشار قابل تنظیم برای نمایش اعداد
        /// </summary>
        public short MaxPrecision { get; set; }

        /// <summary>
        /// مشخص می کند که آیا برای نمایش مقادیر عددی از جداکننده هزاری استفاده بشود یا نه
        /// </summary>
        public bool UseSeparator { get; set; }

        /// <summary>
        /// نحوه خواندن و اعمال تنظیمات جداکننده، که می تواند از تنظیمات موجود در سیستم عامل
        /// یا از تنظیمات سفارشی برنامه استفاده کند
        /// </summary>
        public string SeparatorMode { get; set; }

        /// <summary>
        /// علامت مورد استفاده برای جداکننده هزاری
        /// </summary>
        public string SeparatorSymbol { get; set; }
    }
}
