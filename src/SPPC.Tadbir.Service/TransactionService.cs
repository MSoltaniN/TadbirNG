using System;
using System.Collections.Generic;
using System.Linq;
using SPPC.Framework.Service;
using SPPC.Tadbir.ViewModel.Finance;
using SwForAll.Platform.Common;

namespace SPPC.Tadbir.Service
{
    /// <summary>
    /// Defines operations required for working with financial transactions in the application.
    /// </summary>
    public class TransactionService : ITransactionService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionService"/> class
        /// </summary>
        /// <param name="apiClient">Object that wraps common operations for calling a Web API service</param>
        public TransactionService(IApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        /// <summary>
        /// Retrieves all transaction items that are currently defined in the specified fiscal period.
        /// </summary>
        /// <param name="fpId">Identifier of an existing fiscal period</param>
        /// <returns>Collection of all transactions in the specified fiscal period</returns>
        public IEnumerable<TransactionViewModel> GetTransactions(int fpId)
        {
            var transactions = _apiClient.Get<IEnumerable<TransactionViewModel>>(
                "transactions/fp/{0}", fpId);
            return transactions;
        }

        /// <summary>
        /// Inserts or updates a financial transaction.
        /// </summary>
        /// <param name="transaction">Financial transaction to insert or update</param>
        /// <returns>A <see cref="ServiceResponse"/> object that contains details about the result of service operation</returns>
        public ServiceResponse SaveTransaction(TransactionViewModel transaction)
        {
            Verify.ArgumentNotNull(transaction, "transaction");
            ServiceResponse response = null;
            if (transaction.Id == 0)
            {
                response = _apiClient.Insert(transaction, "transactions");
            }
            else
            {
                response = _apiClient.Update(transaction, "transactions/{0}", transaction.Id);
            }

            return response;
        }

        /// <summary>
        /// Inserts or updates a financial transaction article.
        /// </summary>
        /// <param name="article">Article to insert or update</param>
        /// <returns>A <see cref="ServiceResponse"/> object that contains details about the result of service operation</returns>
        public ServiceResponse SaveArticle(TransactionLineViewModel article)
        {
            Verify.ArgumentNotNull(article, "article");
            var response = new ServiceResponse();
            if (article.Id == 0)
            {
                response = _apiClient.Insert(article, "transactions/{0}/articles", article.TransactionId);
            }
            else
            {
                response = _apiClient.Update(article, "transactions/articles/{0}", article.Id);
            }

            return response;
        }

        /// <summary>
        /// Retrieves detail information of a single transaction item specified by unique identifier.
        /// </summary>
        /// <param name="transactionId">Unique identifier of the transaction to retrieve</param>
        /// <returns>Transaction item with detail information as a <see cref="TransactionFullViewModel"/> instance</returns>
        public TransactionFullViewModel GetDetailTransactionInfo(int transactionId)
        {
            var transaction = _apiClient.Get<TransactionFullViewModel>("transactions/{0}/detail", transactionId);
            return transaction;
        }

        /// <summary>
        /// Retrieves a single transaction article specified by unique identifier.
        /// </summary>
        /// <param name="articleId">Unique identifier of the transaction article to retrieve</param>
        /// <returns>Transaction article as a <see cref="TransactionLineViewModel"/> object</returns>
        public TransactionLineViewModel GetArticle(int articleId)
        {
            var article = _apiClient.Get<TransactionLineViewModel>(
                "transactions/articles/{0}", articleId);
            return article;
        }

        /// <summary>
        /// Retrieves detail information of a single transaction line (article) specified by unique identifier.
        /// </summary>
        /// <param name="articleId">Unique identifier of the transaction line (article) to retrieve</param>
        /// <returns>Transaction line (article) with detail information as a <see cref="TransactionLineFullViewModel"/>
        /// instance</returns>
        public TransactionLineFullViewModel GetDetailArticleInfo(int articleId)
        {
            var articleDetail = _apiClient.Get<TransactionLineFullViewModel>(
                "transactions/articles/{0}/details", articleId);
            return articleDetail;
        }

        private IApiClient _apiClient;
    }
}
