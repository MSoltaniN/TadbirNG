using System;
using System.Collections.Generic;
using SPPC.Tadbir.Model.Finance;
using SPPC.Tadbir.ViewModel.Finance;

namespace SPPC.Tadbir.NHibernate
{
    public partial class AccountRepository
    {
        static partial void UpdateExistingAccount(AccountViewModel accountViewModel, Account account)
        {
            account.Code = accountViewModel.Code;
            account.Name = accountViewModel.Name;
            account.Description = accountViewModel.Description;
        }
    }
}
