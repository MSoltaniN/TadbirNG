using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Configuration.Models
{
    /// <summary>
    /// تنظیمات گزارش تراز آزمایشی را نگهداری می کند
    /// </summary>
    public class TestBalanceConfig
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        public TestBalanceConfig()
        {
            AddOpeningVoucherToInitBalance = false;
        }

        /// <summary>
        /// گزینه انعکاس افتتاحیه در ستون مانده ابتدا
        /// </summary>
        public bool AddOpeningVoucherToInitBalance { get; set; }
    }
}
