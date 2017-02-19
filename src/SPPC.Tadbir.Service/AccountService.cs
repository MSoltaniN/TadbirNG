using System;
using System.Collections.Generic;
using System.Linq;
using SPPC.Framework.Service;
using SPPC.Tadbir.ViewModel.Finance;

namespace SPPC.Tadbir.Service
{
    /// <summary>
    /// Provides operations required for working with financial accounts in the application.
    /// </summary>
    public class AccountService : IAccountService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AccountService"/> class
        /// </summary>
        /// <param name="apiClient">Object that wraps common operations for calling a Web API service</param>
        public AccountService(IApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        /// <summary>
        /// Retrieves all account items that are currently defined in the specified fiscal period.
        /// </summary>
        /// <param name="fpId">Identifier of an existing fiscal period</param>
        /// <returns>Collection of all account items in the specified fiscal period</returns>
        public IEnumerable<AccountViewModel> GetAccounts(int fpId)
        {
            var accounts = _apiClient.Get<IEnumerable<AccountViewModel>>(
                "accounts/fp/{0}", fpId);
            return accounts;
        }

        private IApiClient _apiClient;
    }
}
