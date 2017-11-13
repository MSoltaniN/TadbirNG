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

        /// <summary>
        /// Determines if the account specified by identifier is referenced by other records. 
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        bool IsUsedAccount(int accountId);

        /// <summary>
        /// Retrieves a single financial account with detail information from repository
        /// </summary>
        /// <param name="accountId">Unique identifier of an existing account</param>
        /// <returns>The account retrieved from repository as a <see cref="AccountFullViewModel"/> object</returns>
        AccountFullViewModel GetAccountDetail(int accountId);

        /// <summary>
        /// Retrieves all transaction lines (articles) that use the financial account specified by given unique identifier.
        /// </summary>
        /// <param name="accountId">Unique identifier of an existing financial account</param>
        /// <returns>Collection of all transaction lines (articles) for specified account</returns>
        IList<TransactionLineViewModel> GetAccountArticles(int accountId); 

        /// <summary>
        /// Deletes an existing financial account from repository.
        /// </summary>
        /// <param name="accountId">Identifier of the account to delete</param>
        void DeleteAccount(int accountId);

        /// <summary>
        /// Retrieves the count of all account items in a specified fiscal period and branch
        /// </summary>
        /// <param name="fpId">Identifier of an existing fiscal period</param>
        /// <param name="branchId">Identifier of an existing corporate branch</param>
        /// <returns>Count of all account items</returns>
        int GetCount(int fpId, int branchId);
    }
}
