using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Configuration.Models
{
    /// <summary>
    /// اطلاعات تنظیمات محدوده های تاریخی مورد استفاده در برنامه را نگهداری می کند
    /// </summary>
    public class DateRangeConfig
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        public DateRangeConfig()
        {
            DefaultDateRange = DateRangeOptions.FiscalStartToFiscalEnd;
        }

        /// <summary>
        /// محدوده تاریخی پیش فرض مورد استفاده برای فیلتر فرم های لیستی
        /// </summary>
        public string DefaultDateRange { get; set; }
    }
}
