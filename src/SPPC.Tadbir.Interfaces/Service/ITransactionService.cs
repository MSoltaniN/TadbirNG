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
        #region Transaction CRUD Operations

        /// <summary>
        /// Retrieves all transaction items that are currently defined in the specified fiscal period and branch.
        /// </summary>
        /// <param name="fpId">Identifier of an existing fiscal period</param>
        /// <param name="branchId">Identifier of an existing corporate branch</param>
        /// <returns>Collection of all transactions in the specified fiscal period</returns>
        IEnumerable<TransactionViewModel> GetTransactions(int fpId, int branchId);

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
        /// Deletes a financial transaction specified by unique identifier.
        /// </summary>
        /// <param name="transactionId">Unique identifier of the transaction to delete</param>
        void DeleteTransaction(int transactionId);

        #endregion

        #region Article CRUD Operations

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

        /// <summary>
        /// Retrieves detail information of a single transaction line (article) specified by unique identifier.
        /// </summary>
        /// <param name="articleId">Unique identifier of the transaction line (article) to retrieve</param>
        /// <returns>Transaction line (article) with detail information as a <see cref="TransactionLineFullViewModel"/>
        /// instance</returns>
        TransactionLineFullViewModel GetDetailArticleInfo(int articleId);

        /// <summary>
        /// Deletes a financial transaction line (article) specified by unique identifier.
        /// </summary>
        /// <param name="articleId">Unique identifier of the article to delete</param>
        void DeleteArticle(int articleId);

        #endregion

        #region Transaction Workflow Operations

        /// <summary>
        /// Updates operational status of a financial transaction to Prepared.
        /// </summary>
        /// <param name="transactionId">Unique identifier of the transaction to prepare</param>
        /// <param name="paraph">Optional remarks that user can enter before completing the action</param>
        ServiceResponse PrepareTransaction(int transactionId, string paraph = null);

        /// <summary>
        /// Updates operational status of a financial transaction to Reviewed.
        /// </summary>
        /// <param name="transactionId">Unique identifier of the transaction to review</param>
        /// <param name="paraph">Optional remarks that user can enter before completing the action</param>
        ServiceResponse ReviewTransaction(int transactionId, string paraph = null);

        /// <summary>
        /// Updates operational status of a reviewed financial transaction to Prepred,
        /// meaning it needs to be reviewed again.
        /// </summary>
        /// <param name="transactionId">Unique identifier of the transaction to reject</param>
        /// <param name="paraph">Optional remarks that user can enter before completing the action</param>
        ServiceResponse RejectTransaction(int transactionId, string paraph = null);

        /// <summary>
        /// Updates operational status of a financial transaction to Confirmed.
        /// </summary>
        /// <param name="transactionId">Unique identifier of the transaction to confirm</param>
        /// <param name="paraph">Optional remarks that user can enter before completing the action</param>
        ServiceResponse ConfirmTransaction(int transactionId, string paraph = null);

        /// <summary>
        /// Updates operational status of a financial transaction to Approved.
        /// </summary>
        /// <param name="transactionId">Unique identifier of the transaction to approve</param>
        /// <param name="paraph">Optional remarks that user can enter before completing the action</param>
        ServiceResponse ApproveTransaction(int transactionId, string paraph = null);

        /// <summary>
        /// Updates operational status of multiple financial transactions to Prepared.
        /// </summary>
        /// <param name="transactions">Unique identifiers of transactions to prepare</param>
        /// <param name="paraph">Optional remarks that user can enter before completing the action</param>
        ServiceResponse PrepareTransactions(IEnumerable<int> transactions, string paraph = null);

        #endregion
    }
}
