
namespace SPPC.Tadbir.Configuration.Models
{
    /// <summary>
    /// تنظیمات فرآیند ثبت مالی را برای فرم‌های عملیاتی نگهداری می‌کند
    /// </summary>
    public class RegisterFlowConfig
    {
        /// <summary>
        /// تایید خودکار فرم پس از ذخیره
        /// </summary>
        public bool ConfirmAfterSave { get; set; }

        /// <summary>
        /// تصویب خودکار فرم پس از تایید
        /// </summary>
        public bool ApproveAfterConfirm { get; set; }

        /// <summary>
        /// ثبت مالی خودکار فرم پس از تصویب
        /// </summary>
        public bool RegisterAfterApprove { get; set; }
    }
}
