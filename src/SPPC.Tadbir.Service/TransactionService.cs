using System;
using System.Collections.Generic;
using System.Linq;
using SPPC.Framework.Service;
using SPPC.Tadbir.ViewModel.Finance;

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

        private IApiClient _apiClient;
    }
}
