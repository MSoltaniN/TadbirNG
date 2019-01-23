using System;
using System.Collections.Generic;
using System.Text;

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
        public List<AccountIdentityViewModel> AllAccounts { get; set; }

        /// <summary>
        /// حساب های انتخاب شده برای یک مجموعه حساب
        /// </summary>
        public List<AccountIdentityViewModel> SelectedAccounts { get; set; }
    }
}
