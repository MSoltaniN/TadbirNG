using System;
using System.Collections.Generic;
using SPPC.Framework.Common;
using SPPC.Framework.Service;
using SPPC.Tadbir.Api;
using SPPC.Tadbir.Values;
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
        /// Retrieves all account items that are currently defined in the specified fiscal period and branch.
        /// </summary>
        /// <param name="fpId">Identifier of an existing fiscal period</param>
        /// <param name="branchId">Identifier of an existing corporate branch</param>
        /// <returns>Collection of all account items in the specified fiscal period</returns>
        public IEnumerable<AccountViewModel> GetAccounts(int fpId, int branchId)
        {
            var accounts = _apiClient.Get<IEnumerable<AccountViewModel>>(
                AccountApi.EnvironmentAccounts, fpId, branchId);
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
        /// Inserts or updates a financial account.
        /// </summary>
        /// <param name="account">Financial account to insert or update</param>
        public ServiceResponse SaveAccount(AccountViewModel account)
        {
            Verify.ArgumentNotNull(account, "account");
            ServiceResponse response = null;
            if (account.Id == 0)
            {
                response = _apiClient.Insert(account, AccountApi.EnvironmentAccounts);
            }
            else
            {
                response = _apiClient.Update(account, AccountApi.Account, account.Id);
            }

            return response;
        }

        /// <summary>
        /// Deletes a financial account specified by unique identifier.
        /// </summary>
        /// <param name="accountId">Unique identifier of the account to delete</param>
        /// <returns>A <see cref="ServiceResponse"/> object that contains details about the result of delete operation</returns>
        public ServiceResponse DeleteAccount(int accountId)
        {
            var response = _apiClient.Delete(AccountApi.Account, accountId);
            if (!response.Succeeded)
            {
                response.Result = ServiceResult.DeleteFailed;
            }

            return response;
        }

        private IApiClient _apiClient;
    }
}
