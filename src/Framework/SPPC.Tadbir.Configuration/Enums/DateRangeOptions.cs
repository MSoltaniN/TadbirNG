using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Configuration
{
    /// <summary>
    /// گزینه های موجود برای تنظیمات محدوده های تاریخی را تعریف می کند
    /// </summary>
    public sealed class DateRangeOptions
    {
        private DateRangeOptions()
        {
        }

        /// <summary>
        /// محدوده تاریخی از ابتدا تا انتهای دوره مالی جاری
        /// </summary>
        public const string FiscalStartToFiscalEnd = "FiscalStartToFiscalEnd";

        /// <summary>
        /// محدوده تاریخی از ابتدای دوره مالی جاری تا روز جاری
        /// </summary>
        public const string FiscalStartToCurrent = "FiscalStartToCurrent";

        /// <summary>
        /// محدوده تاریخی از روز جاری تا روز جاری
        /// </summary>
        public const string CurrentToCurrent = "CurrentToCurrent";
    }
}
