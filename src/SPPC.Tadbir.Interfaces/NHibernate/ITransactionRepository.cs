using System;
using System.Collections.Generic;
using SPPC.Tadbir.ViewModel.Finance;

namespace SPPC.Tadbir.NHibernate
{
    /// <summary>
    /// Defines repository operations for managing a transaction and its child items.
    /// </summary>
    public interface ITransactionRepository
    {
        /// <summary>
        /// Retrieves all transactions in specified fiscal period from repository.
        /// </summary>
        /// <param name="fpId">Identifier of an existing fiscal period</param>
        /// <returns>A collection of <see cref="TransactionViewModel"/> objects retrieved from repository</returns>
        IList<TransactionViewModel> GetTransactions(int fpId);

        void SaveTransaction(TransactionViewModel transaction);

        bool IsValidTransaction(TransactionViewModel transaction);
    }
}
