using System;
using System.Collections.Generic;
using System.Linq;
using SPPC.Framework.Helpers;
using SPPC.Framework.Service;

namespace SPPC.Tadbir.Service
{
    /// <summary>
    /// Provides operations for retrieving existing items as key/value collections (lookups).
    /// </summary>
    public class LookupService : ILookupService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LookupService"/> class.
        /// </summary>
        /// <param name="apiClient"></param>
        public LookupService(IApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        /// <summary>
        /// Retrieves existing accounts in the specified fiscal period as a lookup collection.
        /// </summary>
        /// <param name="fpId">Unique identifier of the fiscal period to look for accounts</param>
        /// <returns>Lookup collection of existing accounts in the fiscal period</returns>
        public IEnumerable<KeyValue> LookupAccounts(int fpId)
        {
            var accountLookup = _apiClient.Get<IEnumerable<KeyValue>>("lookup/accounts/{0}", fpId);
            return accountLookup;
        }

        /// <summary>
        /// Retrieves existing currencies as a lookup collection
        /// </summary>
        /// <returns>Lookup collection of existing currencies</returns>
        public IEnumerable<KeyValue> LookupCurrencies()
        {
            var currencyLookup = _apiClient.Get<IEnumerable<KeyValue>>("lookup/currencies");
            return currencyLookup;
        }

        private IApiClient _apiClient;
    }
}
