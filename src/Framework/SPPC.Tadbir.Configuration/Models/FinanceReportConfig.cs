namespace SPPC.Tadbir.Configuration.Models
{
    /// <summary>
    /// تنظیمات گزارش تراز آزمایشی را نگهداری می کند
    /// </summary>
    public class FinanceReportConfig
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        public FinanceReportConfig()
        {
            AddOpeningVoucherToInitBalance = false;
        }

        /// <summary>
        /// گزینه انعکاس افتتاحیه در ستون مانده ابتدا
        /// </summary>
        public bool AddOpeningVoucherToInitBalance { get; set; }
    }
}
