using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Values
{
    /// <summary>
    /// داده شمارشی برای نماهای مختلف گزارش دفتر روزنامه
    /// </summary>
    public enum JournalMode
    {
        /// <summary>
        /// مطابق ردیف های سند
        /// </summary>
        ByRows = 0,

        /// <summary>
        /// مطابق ردیف های سند - با سطوح شناور
        /// </summary>
        ByRowsWithDetail,

        /// <summary>
        /// در سطح کل
        /// </summary>
        ByLedger,

        /// <summary>
        /// در سطح معین
        /// </summary>
        BySubsidiary,

        /// <summary>
        /// سند خلاصه
        /// </summary>
        LedgerSummary,

        /// <summary>
        /// سند خلاصه به تفکیک تاریخ
        /// </summary>
        LedgerSummaryByDate,

        /// <summary>
        /// سند خلاصه ماهیانه
        /// </summary>
        MonthlyLedgerSummary
    }
}
