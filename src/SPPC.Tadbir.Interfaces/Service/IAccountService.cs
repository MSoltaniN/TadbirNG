using System;
using System.Collections.Generic;
using SPPC.Tadbir.ViewModel.Finance;

namespace SPPC.Tadbir.Service
{
    /// <summary>
    /// Defines operations required for working with financial accounts in the application.
    /// </summary>
    public interface IAccountService
    {
        /// <summary>
        /// Retrieves all account items that are currently defined in the specified fiscal period.
        /// </summary>
        /// <param name="fpId">Identifier of an existing fiscal period</param>
        /// <returns>Collection of all account items in the specified fiscal period</returns>
        IEnumerable<AccountViewModel> GetAccounts(int fpId);
    }
}
