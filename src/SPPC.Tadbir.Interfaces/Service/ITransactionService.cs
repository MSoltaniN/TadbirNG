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

        /// <summary>
        /// Inserts or updates a financial transaction.
        /// </summary>
        /// <param name="transaction">Financial transaction to insert or update</param>
        /// <returns>A <see cref="ServiceResponse"/> object that contains details about the result of service operation</returns>
        ServiceResponse SaveTransaction(TransactionViewModel transaction);

        /// <summary>
        /// Retrieves detail information of a single transaction item specified by unique identifier.
        /// </summary>
        /// <param name="transactionId">Unique identifier of the transaction to retrieve</param>
        /// <returns>Transaction item with detail information as a <see cref="TransactionFullViewModel"/> instance</returns>
        TransactionFullViewModel GetDetailTransactionInfo(int transactionId);

        /// <summary>
        /// Inserts or updates a financial transaction article.
        /// </summary>
        /// <param name="article">Article to insert or update</param>
        /// <returns>A <see cref="ServiceResponse"/> object that contains details about the result of service operation</returns>
        ServiceResponse SaveArticle(TransactionLineViewModel article);

        /// <summary>
        /// Retrieves a single transaction article specified by unique identifier.
        /// </summary>
        /// <param name="articleId">Unique identifier of the transaction article to retrieve</param>
        /// <returns>Transaction article as a <see cref="TransactionLineViewModel"/> object</returns>
        TransactionLineViewModel GetArticle(int articleId);
    }
}
