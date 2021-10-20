using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Domain
{
    /// <summary>
    /// داده شمارشی برای نماهای مختلف گزارش دفتر حساب
    /// </summary>
    public enum AccountBookMode
    {
        /// <summary>
        /// مطابق ردیف های سند
        /// </summary>
        ByRows = 0,

        /// <summary>
        /// جمع مبالغ هر  سند
        /// </summary>
        VoucherSum = 1,

        /// <summary>
        /// جمع مبالغ اسناد هر  روز
        /// </summary>
        DailySum = 2,

        /// <summary>
        /// جمع مبالغ اسناد هر  ماه
        /// </summary>
        MonthlySum = 3
    }
}
