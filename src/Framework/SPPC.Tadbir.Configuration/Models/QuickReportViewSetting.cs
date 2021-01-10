using System;
using System.Collections.Generic;
using System.Text;

namespace SPPC.Tadbir.Configuration.Models
{
    /// <summary>
    /// تنظیمات ظاهری گزارش فوری را نگهداری می کند
    /// </summary>
    public class QuickReportViewSetting
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        public QuickReportViewSetting()
        {
        }

        /// <summary>
        /// برای پنهان کردن لاین های افقی در گزارش فوری
        /// </summary>
        public bool HideHorizontalLine;

        /// <summary>
        /// برای پنهان کردن لاین های عمودی در گزارش فوری
        /// </summary>
        public bool HideVerticalLine;
    }
}
