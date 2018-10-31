using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.ViewModel.Finance
{
    /// <summary>
    /// اطلاعات مالی خلاصه مورد نیاز برای نمایش در داشبورد را نگهداری می کند
    /// </summary>
    public class DashboardSummariesViewModel
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        public DashboardSummariesViewModel()
        {
            NetSales = new DashboardChartSeriesViewModel();
            GrossSales = new DashboardChartSeriesViewModel();
        }

        /// <summary>
        /// مقدار محاسبه شده برای موجودی (مانده) حساب های بانک
        /// </summary>
        public decimal BankBalance { get; set; }

        /// <summary>
        /// مقدار محاسبه شده برای موجودی (مانده) حساب های صندوق
        /// </summary>
        public decimal CashierBalance { get; set; }

        /// <summary>
        /// مقدار محاسبه شده برای نسبت جاری
        /// </summary>
        public decimal LiquidRatio { get; set; }

        /// <summary>
        /// تعداد سندهای مالی ناتراز
        /// </summary>
        public int UnbalancedVoucherCount { get; set; }

        /// <summary>
        /// مقادیر فروش خالص به تفکیک ماه
        /// </summary>
        public DashboardChartSeriesViewModel NetSales { get; set; }

        /// <summary>
        /// مقادیر فروش ناخالص به تفکیک ماه
        /// </summary>
        public DashboardChartSeriesViewModel GrossSales { get; set; }
    }
}
