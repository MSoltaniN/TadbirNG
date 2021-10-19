using System;
using System.Collections.Generic;
using SPPC.Framework.Presentation;

namespace SPPC.Tadbir.Domain
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
            CompareItems = new List<int>();
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
        /// مشخص می کند که گردش ابتدای دوره باید در مانده ابتدا در نظر گرفته شود یا نه
        /// </summary>
        public bool StartTurnoverAsInitBalance { get; set; }

        /// <summary>
        /// شناسه دیتابیسی مرکز هزینه مورد نظر برای فیلتر اطلاعات - اختیاری
        /// </summary>
        public int? CostCenterId { get; set; }

        /// <summary>
        /// شناسه دیتابیسی پروژه مورد نظر برای فیلتر اطلاعات - اختیاری
        /// </summary>
        public int? ProjectId { get; set; }

        /// <summary>
        /// شناسه دیتابیسی شعبه مورد نظر برای فیلتر اطلاعات
        /// </summary>
        public int? BranchId { get; set; }

        /// <summary>
        /// شناسه دیتابیسی دوره مالی مورد نظر برای فیلتر اطلاعات
        /// </summary>
        public int? FiscalPeriodId { get; set; }

        /// <summary>
        /// گزینه های برنامه برای فیلتر، مرتب سازی و صفحه بندی اطلاعات
        /// </summary>
        public GridOptions GridOptions { get; set; }

        /// <summary>
        /// مجموعه ای از شناسه های دیتابیسی اقلام انتخاب شده برای گزارش مقایسه ای
        /// </summary>
        public List<int> CompareItems { get; }

        /// <summary>
        /// کپی جدیدی از این کلاس با مقادیر موجود در نمونه جاری ساخته و برمی گرداند
        /// </summary>
        /// <returns>کپی جدید با اطلاعات نمونه جاری</returns>
        public ProfitLossParameters GetCopy()
        {
            return (ProfitLossParameters)MemberwiseClone();
        }
    }
}
