using System.Collections.Generic;

namespace SPPC.Tadbir.ViewModel.Finance
{
    /// <summary>
    /// حساب های قابل انتخاب و انتخاب شده برای یک مجموعه حساب
    /// </summary>
    public class AccountCollectionItemsViewModel
    {
        /// <summary>
        /// تمام حساب های قابل انتخاب در مجموعه حساب ها
        /// </summary>
        public List<AccountViewModel> AllAccounts { get; set; }

        /// <summary>
        /// حساب های انتخاب شده برای یک مجموعه حساب
        /// </summary>
        public List<AccountViewModel> SelectedAccounts { get; set; }
    }
}
