using System;
using System.Collections.Generic;
using SPPC.Framework.Service;
using SPPC.Tadbir.ViewModel.Finance;

namespace SPPC.Tadbir.Service
{
    /// <summary>
    /// Defines operations required for working with financial transactions in the application.
    /// </summary>
    public interface ITransactionService
    {
        /// <summary>
        /// Retrieves all transaction items that are currently defined in the specified fiscal period.
        /// </summary>
        /// <param name="fpId">Identifier of an existing fiscal period</param>
        /// <returns>Collection of all transactions in the specified fiscal period</returns>
        IEnumerable<TransactionViewModel> GetTransactions(int fpId);

        ServiceResponse SaveTransaction(TransactionViewModel transaction);
    }
}
