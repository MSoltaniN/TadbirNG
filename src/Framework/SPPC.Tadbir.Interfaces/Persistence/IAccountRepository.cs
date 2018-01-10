using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.ViewModel.Finance;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// Defines repository operations for managing a financial account.
    /// </summary>
    public interface IAccountRepository
    {
        /// <summary>
        /// Retrieves all accounts in specified fiscal period and branch from repository.
        /// </summary>
        /// <param name="fpId">Identifier of an existing fiscal period</param>
        /// <param name="branchId">Identifier of an existing corporate branch</param>
        /// <param name="options">Options used for displaying data in a tabular grid view</param>
        /// <returns>A collection of <see cref="AccountViewModel"/> objects retrieved from repository</returns>
        IList<AccountViewModel> GetAccounts(int fpId, int branchId, GridOptions options = null);

        /// <summary>
        /// Asynchronously retrieves all accounts in specified fiscal period and branch from repository.
        /// </summary>
        /// <param name="fpId">Identifier of an existing fiscal period</param>
        /// <param name="branchId">Identifier of an existing corporate branch</param>
        /// <param name="options">Options used for displaying data in a tabular grid view</param>
        /// <returns>A collection of <see cref="AccountViewModel"/> objects retrieved from repository</returns>
        Task<IList<AccountViewModel>> GetAccountsAsync(int fpId, int branchId, GridOptions options = null);

        /// <summary>
        /// Retrieves a single account specified by Id from repository.
        /// </summary>
        /// <param name="accountId">Identifier of an existing account</param>
        /// <returns>The account retrieved from repository as a <see cref="AccountViewModel"/> object</returns>
        AccountViewModel GetAccount(int accountId);

        /// <summary>
        /// Asynchronously retrieves a single account specified by Id from repository.
        /// </summary>
        /// <param name="accountId">Identifier of an existing account</param>
        /// <returns>The account retrieved from repository as a <see cref="AccountViewModel"/> object</returns>
        Task<AccountViewModel> GetAccountAsync(int accountId);

        /// <summary>
        /// Inserts or updates a single account in repository.
        /// </summary>
        /// <param name="account">Item to insert or update</param>
        void SaveAccount(AccountViewModel account);

        /// <summary>
        /// Asynchronously inserts or updates a single account in repository.
        /// </summary>
        /// <param name="account">Item to insert or update</param>
        Task SaveAccountAsync(AccountViewModel account);

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

        /// <summary>
        /// Asynchronously determines if the specified <see cref="AccountViewModel"/> instance uses a code
        /// that is already used in a different account item.
        /// </summary>
        /// <param name="accountViewModel">Account item to check for duplicate code</param>
        /// <returns>True if the Code of specified account item is already used in a different account;
        /// otherwise, returns false.</returns>
        /// <remarks>If the account code is used in the same account (i.e. the account is being edited
        /// without changing its code value), this method will return false.</remarks>
        Task<bool> IsDuplicateAccountAsync(AccountViewModel accountViewModel);

        /// <summary>
        /// Determines if the account specified by identifier is referenced by other records.
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        bool IsUsedAccount(int accountId);

        /// <summary>
        /// Asynchronously determines if the account specified by identifier is referenced by other records.
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        Task<bool> IsUsedAccountAsync(int accountId);

        /// <summary>
        /// Retrieves a single financial account with detail information from repository
        /// </summary>
        /// <param name="accountId">Unique identifier of an existing account</param>
        /// <returns>The account retrieved from repository as a <see cref="AccountFullViewModel"/> object</returns>
        AccountFullViewModel GetAccountDetail(int accountId);

        /// <summary>
        /// Asynchronously retrieves a single financial account with detail information from repository
        /// </summary>
        /// <param name="accountId">Unique identifier of an existing account</param>
        /// <returns>The account retrieved from repository as a <see cref="AccountFullViewModel"/> object</returns>
        Task<AccountFullViewModel> GetAccountDetailAsync(int accountId);

        /// <summary>
        /// Retrieves all transaction lines (articles) that use the financial account specified by given unique identifier.
        /// </summary>
        /// <param name="accountId">Unique identifier of an existing financial account</param>
        /// <returns>Collection of all transaction lines (articles) for specified account</returns>
        IList<TransactionLineViewModel> GetAccountArticles(int accountId);

        /// <summary>
        /// Asynchronously retrieves all transaction lines (articles) that use the financial account specified by
        /// given unique identifier.
        /// </summary>
        /// <param name="accountId">Unique identifier of an existing financial account</param>
        /// <returns>Collection of all transaction lines (articles) for specified account</returns>
        Task<IList<TransactionLineViewModel>> GetAccountArticlesAsync(int accountId);

        /// <summary>
        /// Deletes an existing financial account from repository.
        /// </summary>
        /// <param name="accountId">Identifier of the account to delete</param>
        void DeleteAccount(int accountId);

        /// <summary>
        /// Asynchronously deletes an existing financial account from repository.
        /// </summary>
        /// <param name="accountId">Identifier of the account to delete</param>
        Task DeleteAccountAsync(int accountId);

        /// <summary>
        /// Retrieves the count of all account items in a specified fiscal period and branch
        /// </summary>
        /// <param name="fpId">Identifier of an existing fiscal period</param>
        /// <param name="branchId">Identifier of an existing corporate branch</param>
        /// <returns>Count of all account items</returns>
        int GetCount(int fpId, int branchId);
    }
}
