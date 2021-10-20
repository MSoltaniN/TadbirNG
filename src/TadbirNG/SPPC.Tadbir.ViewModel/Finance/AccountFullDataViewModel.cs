namespace SPPC.Tadbir.ViewModel.Finance
{
    /// <summary>
    /// مدل نمایشی کامل حساب همراه با سایر مشخصات حساب
    /// </summary>
    public class AccountFullDataViewModel
    {
        /// <summary>
        /// مدل نمایشی حساب
        /// </summary>
        public AccountViewModel Account { get; set; }

        /// <summary>
        /// مدل نمایشی اطلاعات مالیاتی طرف حساب
        /// </summary>
        public CustomerTaxInfoViewModel CustomerTaxInfo { get; set; }

        /// <summary>
        /// مدل نمایشی اطلاعات حساب بانکی
        /// </summary>
        public AccountOwnerViewModel AccountOwner { get; set; }
    }
}
