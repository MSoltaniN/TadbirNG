using System;
using System.Collections.Generic;
using SPPC.Tadbir.ViewModel.Finance;

namespace SPPC.Tadbir.NHibernate
{
    public partial interface IAccountRepository
    {
        /// <summary>
        /// Determines if the specified <see cref="AccountViewModel"/> instance uses a code that is already used
        /// in a different account item.
        /// </summary>
        /// <param name="accountViewModel">Account item to check for duplicate code</param>
        /// <returns>True if the Code of specified account item is already used in a different account;
        /// otherwise, returns false.</returns>
        /// <remarks>If the account code is used in the same account (i.e. the account is being edited
        /// without changing its code value), this method will return false.</remarks>
        bool IsDuplicateAccount(AccountViewModel accountViewModel);
    }
}
