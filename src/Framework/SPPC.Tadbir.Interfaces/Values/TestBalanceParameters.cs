using System;
using System.Collections.Generic;
using SPPC.Framework.Presentation;

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
            Mode = TestBalanceMode.Ledger;
            Format = TestBalanceFormat.SixColumn;
            IsByBranch = false;
            Options = TestBalanceOptions.UseClosingVoucher
                | TestBalanceOptions.UseClosingTempVoucher
                | TestBalanceOptions.OpeningVoucherAsInitBalance;
        }

        /// <summary>
        /// نوع مورد نظر برای گزارش تراز آزمایشی
        /// </summary>
        public TestBalanceMode Mode { get; set; }

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

        /// <summary>
        /// گزینه های مورد استفاده برای مرتب سازی، فیلتر و صفحه بندی اطلاعات گزارش
        /// </summary>
        public GridOptions GridOptions { get; set; }

        /// <summary>
        /// گزینه های عملیاتی مورد نیاز در گزارش
        /// </summary>
        public TestBalanceOptions Options { get; set; }
    }
}
