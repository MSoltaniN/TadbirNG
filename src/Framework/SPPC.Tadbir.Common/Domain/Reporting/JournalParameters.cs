using System;
using SPPC.Framework.Presentation;

namespace SPPC.Tadbir.Domain
{
    /// <summary>
    /// اطلاعات پارامترهای مورد نیاز در گزارش دفتر روزنامه را نگهداری می کند
    /// </summary>
    public class JournalParameters
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        public JournalParameters()
        {
            Mode = JournalMode.ByRows;
        }

        /// <summary>
        /// نوع نمایش مورد نظر برای گزارش دفتر روزنامه
        /// </summary>
        public JournalMode Mode { get; set; }

        /// <summary>
        /// تاریخ شروع دوره گزارش گیری در حالت گزارش بر اساس تاریخ
        /// </summary>
        public DateTime FromDate { get; set; }

        /// <summary>
        /// تاریخ پایان دوره گزارش گیری در حالت گزارش بر اساس تاریخ
        /// </summary>
        public DateTime ToDate { get; set; }

        /// <summary>
        /// اولین سند در حالت گزارش بر اساس سند
        /// </summary>
        public int FromNo { get; set; }

        /// <summary>
        /// آخرین سند در حالت گزارش بر اساس سند
        /// </summary>
        public int ToNo { get; set; }

        /// <summary>
        /// گزینه های مورد استفاده برای مرتب سازی، فیلتر و صفحه بندی اطلاعات گزارش
        /// </summary>
        public GridOptions GridOptions { get; set; }
    }
}
