using System;
using SPPC.Framework.Presentation;

namespace SPPC.Tadbir.Domain
{
    /// <summary>
    ///
    /// </summary>
    public class ReportParameters
    {
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
    }
}
