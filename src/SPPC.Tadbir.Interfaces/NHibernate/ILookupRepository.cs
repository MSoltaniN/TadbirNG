using System;
using System.Collections.Generic;
using SPPC.Framework.Helpers;

namespace SPPC.Tadbir.NHibernate
{
    /// <summary>
    /// Defines repository operations for getting different types of key/value collections (lookups).
    /// </summary>
    public interface ILookupRepository
    {
        /// <summary>
        /// Retrieves all financial account items in the specified fiscal period and branch as a collection of
        /// <see cref="KeyValue"/> objects. The key for each entry is the unique identifier of corresponding
        /// account in data store.
        /// </summary>
        /// <param name="fpId">Unique identifier of an existing fiscal period</param>
        /// <param name="branchId">Unique identifier of the branch to look for accounts</param>
        /// <returns>Collection of all account items in the specified fiscal period.</returns>
        IEnumerable<KeyValue> GetAccounts(int fpId, int branchId);

        /// <summary>
        /// Retrieves all currency objects as a collection of <see cref="KeyValue"/> objects. The key for each
        /// entry is the unique identifier of corresponding currency in data store.
        /// </summary>
        /// <returns>Collection of all currency items.</returns>
        IEnumerable<KeyValue> GetCurrencies();
    }
}
