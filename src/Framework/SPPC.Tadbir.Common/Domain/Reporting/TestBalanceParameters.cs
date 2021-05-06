namespace SPPC.Tadbir.Domain
{
    /// <summary>
    /// اطلاعات پارامترهای مورد نیاز در گزارش تراز آزمایشی را نگهداری می کند
    /// </summary>
    public class TestBalanceParameters : ReportParameters
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        public TestBalanceParameters()
        {
            ViewId = Domain.ViewId.Account;
            Mode = TestBalanceMode.Level;
            Format = TestBalanceFormat.SixColumn;
            IsByBranch = false;
            Options = FinanceReportOptions.None;
        }

        /// <summary>
        /// شناسه مولفه حساب مورد نظر برای محاسبه گردش و مانده
        /// </summary>
        public int ViewId { get; set; }

        /// <summary>
        /// نوع مورد نظر برای گزارش تراز آزمایشی
        /// </summary>
        public TestBalanceMode Mode { get; set; }

        /// <summary>
        /// قالب مورد نیاز برای گزارش تراز آزمایشی
        /// </summary>
        public TestBalanceFormat Format { get; set; }

        /// <summary>
        /// گزینه های عملیاتی مورد نیاز در گزارش
        /// </summary>
        public FinanceReportOptions Options { get; set; }
    }
}
