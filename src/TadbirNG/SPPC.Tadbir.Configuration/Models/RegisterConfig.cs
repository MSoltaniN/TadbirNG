namespace SPPC.Tadbir.Configuration.Models
{
    /// <summary>
    /// سیاست‌های انتخاب یا ایجاد سند برای ثبت مالی در فرم‌های عملیاتی را نگهداری می‌کند
    /// </summary>
    public class RegisterConfig
    {
        /// <summary>
        /// ارسال ثبت‌های مالی به آخرین سند باز معتبر
        /// </summary>
        public bool RegisterWithLastValidVoucher { get; set; }

        /// <summary>
        /// ایجاد یک سند حسابداری جدید برای ثبت مالی
        /// </summary>
        public bool RegisterWithNewCreatedVoucher { get; set; }
        
        /// <summary>
        /// ثبت خودکار سند حسابداری ایجاد شده
        /// </summary>
        public bool CheckedVoucher { get; set; }
    }
}
