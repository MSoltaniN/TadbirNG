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
            OpeningAsFirstVoucher = false;
        }

        /// <summary>
        /// گزینه سند افتتاحیه به عنوان اولین سند
        /// </summary>
        public bool OpeningAsFirstVoucher { get; set; }
    }
}
