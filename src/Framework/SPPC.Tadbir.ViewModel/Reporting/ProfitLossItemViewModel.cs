using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.ViewModel.Reporting
{
    /// <summary>
    /// اطلاعات یکی از سطرهای گزارش سود و زیان را نگهداری می کند
    /// </summary>
    public class ProfitLossItemViewModel
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        public ProfitLossItemViewModel()
        {
        }

        /// <summary>
        /// گروه محاسباتی گزارش - مانند سود ناخالص، هزینه های عملیاتی و غیره
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// شناسه دیتابیسی حساب ویژه مورد استفاده
        /// </summary>
        public int AccountId { get; set; }

        /// <summary>
        /// حساب ویژه مورد استفاده
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// مانده حساب ویژه در تاریخ شروع دوره گزارشگیری
        /// </summary>
        public decimal? StartBalance { get; set; }

        /// <summary>
        /// گردش محاسبه شده برای حساب ویژه در دوره گزارشگیری
        /// </summary>
        public decimal? PeriodTurnover { get; set; }

        /// <summary>
        /// مانده حساب ویژه در تاریخ پایان دوره گزارشگیری
        /// </summary>
        public decimal? EndBalance { get; set; }

        /// <summary>
        /// مانده حساب ویژه برای گزارشگیری در یک تاریخ مشخص
        /// </summary>
        public decimal? Balance { get; set; }

        /// <summary>
        /// نام شعبه ایجادکننده آرتیکل سند برای حالت تفکیک شعبه
        /// </summary>
        public string BranchName { get; set; }
    }
}
