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

        /// <summary>
        /// Retrieves a single financial transaction with detail information from repository.
        /// </summary>
        /// <param name="transactionId">Unique identifier of an existing transaction</param>
        /// <returns>The transaction retrieved from repository as a <see cref="TransactionFullViewModel"/> object</returns>
        TransactionFullViewModel GetTransactionDetail(int transactionId);

        /// <summary>
        /// Inserts or updates a single transaction in repository.
        /// </summary>
        /// <param name="transaction">Item to insert or update</param>
        void SaveTransaction(TransactionViewModel transaction);

        /// <summary>
        /// Deletes an existing financial transaction from repository.
        /// </summary>
        /// <param name="transactionId">Identifier of the transaction to delete</param>
        bool DeleteTransaction(int transactionId);

        /// <summary>
        /// Retrieves a single financial article from repository.
        /// </summary>
        /// <param name="articleId">Unique identifier of an existing article</param>
        /// <returns>The article retrieved from repository as a <see cref="TransactionLineViewModel"/> object</returns>
        TransactionLineViewModel GetArticle(int articleId);

        /// <summary>
        /// Retrieves a single financial transaction line (article) with detail information from repository.
        /// </summary>
        /// <param name="articleId">Unique identifier of an existing transaction line (article)</param>
        /// <returns>The transaction line (article) retrieved from repository as a
        /// <see cref="TransactionLineFullViewModel"/> object</returns>
        TransactionLineFullViewModel GetArticleDetails(int articleId);

        /// <summary>
        /// Inserts or updates a single transaction line (article) in repository.
        /// </summary>
        /// <param name="article">Article to insert or update</param>
        void SaveArticle(TransactionLineViewModel article);

        /// <summary>
        /// Validates the specified transaction to make sure it fulfills all business rules.
        /// </summary>
        /// <param name="transaction">Transaction that needs to be validated</param>
        /// <returns>True if the transaction fulfills all business rules; otherwise, returns false.</returns>
        bool IsValidTransaction(TransactionViewModel transaction);

        /// <summary>
        /// Deletes an existing financial transaction line (article) from repository.
        /// </summary>
        /// <param name="articleId">Identifier of the article to delete</param>
        void DeleteArticle(int articleId);
    }
}
