using System;

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
            StartBalance = 0.0M;
        }

        /// <summary>
        /// گروه محاسباتی گزارش - مانند سود ناخالص، هزینه های عملیاتی و غیره
        /// </summary>
        public string Group { get; set; }

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

        /// <summary>
        /// تابع کمکی که عمل تفریق یک سطر از این آبجکت را فراهم می کند
        /// </summary>
        /// <param name="right">عبارت سمت راست عملگر تفریق</param>
        /// <returns>همین آبجکت را پس از انجام عمل تفریق برمی گرداند</returns>
        public ProfitLossItemViewModel Subtract(ProfitLossItemViewModel right)
        {
            StartBalance -= right.StartBalance;
            PeriodTurnover -= right.PeriodTurnover;
            EndBalance -= right.EndBalance;
            Balance = EndBalance;
            return this;
        }

        /// <summary>
        /// عملگر تفریق را برای سطر گزارش سود و زیان پیاده سازی می کند
        /// </summary>
        /// <param name="left">عبارت سمت چپ عملگر تفریق</param>
        /// <param name="right">عبارت سمت راست عملگر تفریق</param>
        /// <returns>آبجکت جدیدی با مقادیر تفریق شده</returns>
        public static ProfitLossItemViewModel operator - (
            ProfitLossItemViewModel left, ProfitLossItemViewModel right)
        {
            return new ProfitLossItemViewModel()
            {
                StartBalance = left.StartBalance - right.StartBalance,
                PeriodTurnover = left.PeriodTurnover - right.PeriodTurnover,
                EndBalance = left.EndBalance - right.EndBalance,
                Balance = left.EndBalance - right.EndBalance,
                BranchName = left.BranchName
            };
        }
    }
}
