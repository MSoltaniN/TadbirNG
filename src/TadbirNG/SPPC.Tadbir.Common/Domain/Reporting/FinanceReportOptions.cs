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
        /// گزینه سند افتتاحیه به عنوان اولین سند
        /// </summary>
        OpeningAsFirstVoucher = 0x4,

        /// <summary>
        /// گزینه نمایش سرفصل های با مانده صفر
        /// </summary>
        ShowZeroBalanceItems = 0x8,

        /// <summary>
        /// گزینه انعکاس گردش ابتدای دوره در مانده ابتدا
        /// </summary>
        StartTurnoverAsInitBalance = 0x10,

        /// <summary>
        /// انتخاب همه گزینه ها
        /// </summary>
        All = 0x1f
    }
}
