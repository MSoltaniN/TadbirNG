using System;
using System.Collections.Generic;
using System.Linq;
using SPPC.Framework.Service;
using SPPC.Tadbir.Api;
using SPPC.Tadbir.ViewModel.Finance;
using SwForAll.Platform.Common;

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
            var accounts = _apiClient.Get<IEnumerable<AccountViewModel>>(AccountApi.FiscalPeriodAccounts, fpId);
            return accounts;
        }

        /// <summary>
        /// Retrieves a single account item specified by unique identifier.
        /// </summary>
        /// <param name="accountId">Unique identifier of the account to retrieve</param>
        /// <returns>Account item having the specified identifier as an <see cref="AccountViewModel"/> instance</returns>
        public AccountViewModel GetAccount(int accountId)
        {
            var account = _apiClient.Get<AccountViewModel>(AccountApi.Account, accountId);
            return account;
        }

        /// <summary>
        /// Retrieves detail information of a single account item specified by unique identifier.
        /// </summary>
        /// <param name="accountId">Unique identifier of the account to retrieve</param>
        /// <returns>Account item with detail information as an <see cref="AccountFullViewModel"/> instance</returns>
        public AccountFullViewModel GetDetailAccountInfo(int accountId)
        {
            var account = _apiClient.Get<AccountFullViewModel>(AccountApi.AccountDetails, accountId);
            return account;
        }

        /// <summary>
        /// Inserts or updates a financial account.
        /// </summary>
        /// <param name="account">Financial account to insert or update</param>
        public ServiceResponse SaveAccount(AccountViewModel account)
        {
            Verify.ArgumentNotNull(account, "account");
            ServiceResponse response = null;
            if (account.Id == 0)
            {
                response = _apiClient.Insert(account, AccountApi.Accounts);
            }
            else
            {
                response = _apiClient.Update(account, AccountApi.Account, account.Id);
            }

            return response;
        }

        private IApiClient _apiClient;
    }
}
