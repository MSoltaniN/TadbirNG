// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.0.0.0
//     Template Version: 1.0
//     Generation Date: 2017-02-18 12:59:04 PM
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using SPPC.Tadbir.ViewModel.Finance;
using SPPC.Tadbir.ViewModel.UI;

namespace SPPC.Tadbir.NHibernate
{
    /// <summary>
    /// Defines repository operations for managing an account and its child items.
    /// </summary>
    public partial interface IAccountRepository
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
        /// Retrieves a single account specified by Id from repository.
        /// </summary>
        /// <param name="accountId">Identifier of an existing account</param>
        /// <returns>The account retrieved from repository as a <see cref="AccountViewModel"/> object</returns>
        AccountViewModel GetAccount(int accountId);

        /// <summary>
        /// Inserts or updates a single account in repository.
        /// </summary>
        /// <param name="account">Item to insert or update</param>
        void SaveAccount(AccountViewModel account);
    }
}
