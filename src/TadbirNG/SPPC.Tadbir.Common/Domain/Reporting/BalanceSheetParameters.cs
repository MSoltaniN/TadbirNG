using System;
using SPPC.Framework.Presentation;

namespace SPPC.Tadbir.Domain
{
    /// <summary>
    /// پارامترهای مورد نیاز برای محاسبه گزارش ترازنامه را نگهداری می کند
    /// </summary>
    public class BalanceSheetParameters
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        public BalanceSheetParameters()
        {
        }

        /// <summary>
        /// تاریخ مورد نظر برای گزارشگیری
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// شناسه دیتابیسی دوره مالی قبل - اختیاری
        /// </summary>
        public int? FiscalPeriodId { get; set; }

        /// <summary>
        /// شناسه دیتابیسی مرکز هزینه مورد نظر برای فیلتر اطلاعات - اختیاری
        /// </summary>
        public int? CostCenterId { get; set; }

        /// <summary>
        /// شناسه دیتابیسی پروژه مورد نظر برای فیلتر اطلاعات - اختیاری
        /// </summary>
        public int? ProjectId { get; set; }

        /// <summary>
        /// مشخص می کند که سند اختتامیه باید در نظر گرفته شود یا نه
        /// </summary>
        public bool UseClosingVoucher { get; set; }

        /// <summary>
        /// گزینه های برنامه برای فیلتر، مرتب سازی و صفحه بندی اطلاعات
        /// </summary>
        public GridOptions GridOptions { get; set; }
    }
}
