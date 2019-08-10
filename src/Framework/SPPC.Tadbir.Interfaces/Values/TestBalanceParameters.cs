using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Values
{
    /// <summary>
    /// اطلاعات پارامترهای مورد نیاز در گزارش تراز آزمایشی را نگهداری می کند
    /// </summary>
    public class TestBalanceParameters
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        public TestBalanceParameters()
        {
            Format = TestBalanceFormat.SixColumn;
            IsByBranch = false;
        }

        /// <summary>
        /// قالب مورد نیاز برای گزارش تراز آزمایشی
        /// </summary>
        public TestBalanceFormat Format { get; set; }

        /// <summary>
        /// تاریخ شروع دوره گزارش گیری در حالت گزارش بر اساس تاریخ
        /// </summary>
        public DateTime? FromDate { get; set; }

        /// <summary>
        /// تاریخ پایان دوره گزارش گیری در حالت گزارش بر اساس تاریخ
        /// </summary>
        public DateTime? ToDate { get; set; }

        /// <summary>
        /// اولین سند در حالت گزارش بر اساس سند
        /// </summary>
        public int? FromNo { get; set; }

        /// <summary>
        /// آخرین سند در حالت گزارش بر اساس سند
        /// </summary>
        public int? ToNo { get; set; }

        /// <summary>
        /// مشخص می کند که آیا گزارش گیری باید به تفکیک شعبه انجام شود یا نه؟
        /// </summary>
        public bool IsByBranch { get; set; }
    }
}
