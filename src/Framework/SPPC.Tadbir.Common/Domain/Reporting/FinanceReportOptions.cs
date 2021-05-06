using System;

namespace SPPC.Tadbir.Domain
{
    /// <summary>
    /// داده نمایشی برای گزینه های عملیاتی در تراز آزمایشی
    /// </summary>
    [Flags]
    public enum FinanceReportOptions
    {
        /// <summary>
        /// عدم انتخاب همه گزینه ها
        /// </summary>
        None = 0x0,

        /// <summary>
        /// گزینه نمایش سند اختتامیه
        /// </summary>
        UseClosingVoucher = 0x1,

        /// <summary>
        /// گزینه نمایش سند بستن حساب ها
        /// </summary>
        UseClosingTempVoucher = 0x2,

        /// <summary>
        /// گزینه انعکاس افتتاحیه در ستون مانده ابتدا
        /// </summary>
        OpeningVoucherAsInitBalance = 0x4,

        /// <summary>
        /// گزینه نمایش سرفصل های با مانده صفر
        /// </summary>
        ShowZeroBalanceItems = 0x8
    }
}
