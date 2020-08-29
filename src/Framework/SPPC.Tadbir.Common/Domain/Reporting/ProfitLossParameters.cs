using System;
using SPPC.Framework.Presentation;

namespace SPPC.Tadbir.Values
{
    /// <summary>
    /// پارامترهای مورد نیاز برای محاسبه گزارش سود و زیان را نگهداری می کند
    /// </summary>
    public class ProfitLossParameters
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        public ProfitLossParameters()
        {
        }

        /// <summary>
        /// تاریخ شروع دوره گزارشگیری
        /// </summary>
        public DateTime FromDate { get; set; }

        /// <summary>
        /// تاریخ پایان دوره گزارشگیری
        /// </summary>
        public DateTime ToDate { get; set; }

        /// <summary>
        /// مبلغ مالیات تعیین شده
        /// </summary>
        public decimal TaxAmount { get; set; }

        /// <summary>
        /// مشخص می کند که سند بستن حسابهای موقت باید در نظر گرفته شود یا نه
        /// </summary>
        public bool UseClosingTempVoucher { get; set; }

        /// <summary>
        /// شناسه دیتابیسی مرکز هزینه مورد نظر برای فیلتر اطلاعات - اختیاری
        /// </summary>
        public int? CostCenterId { get; set; }

        /// <summary>
        /// شناسه دیتابیسی پروژه مورد نظر برای فیلتر اطلاعات - اختیاری
        /// </summary>
        public int? ProjectId { get; set; }

        /// <summary>
        /// گزینه های برنامه برای فیلتر، مرتب سازی و صفحه بندی اطلاعات
        /// </summary>
        public GridOptions GridOptions { get; set; }
    }
}
