using System;
using System.Collections.Generic;
using SPPC.Framework.Helpers;

namespace SPPC.Tadbir.Service
{
    /// <summary>
    /// Provides operations for retrieving existing items as key/value collections (lookups).
    /// </summary>
    public interface ILookupService
    {
        /// <summary>
        /// Retrieves existing accounts in the specified fiscal period as a lookup collection.
        /// </summary>
        /// <param name="fpId">Unique identifier of the fiscal period to look for accounts</param>
        /// <returns>Lookup collection of existing accounts in the fiscal period</returns>
        IEnumerable<KeyValue> LookupAccounts(int fpId);

        /// <summary>
        /// Retrieves existing currencies as a lookup collection
        /// </summary>
        /// <returns>Lookup collection of existing currencies</returns>
        IEnumerable<KeyValue> LookupCurrencies();
    }
}
