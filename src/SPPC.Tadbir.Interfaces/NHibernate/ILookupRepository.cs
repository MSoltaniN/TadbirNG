using System;
using System.Collections.Generic;
using SPPC.Framework.Helpers;
using SPPC.Tadbir.ViewModel.Procurement;

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
        /// Retrieves all detail account objects as a collection of <see cref="KeyValue"/> objects. The key for each
        /// entry is the unique identifier of corresponding detail account in data store.
        /// </summary>
        /// <returns>Collection of all detail account items.</returns>
        IEnumerable<KeyValue> GetDetailAccounts();

        /// <summary>
        /// Retrieves all cost center objects as a collection of <see cref="KeyValue"/> objects. The key for each
        /// entry is the unique identifier of corresponding cost center in data store.
        /// </summary>
        /// <returns>Collection of all cost center items.</returns>
        IEnumerable<KeyValue> GetCostCenters();

        /// <summary>
        /// Retrieves all project objects as a collection of <see cref="KeyValue"/> objects. The key for each
        /// entry is the unique identifier of corresponding project in data store.
        /// </summary>
        /// <returns>Collection of all project items.</returns>
        IEnumerable<KeyValue> GetProjects();

        /// <summary>
        /// Retrieves all currency objects as a collection of <see cref="KeyValue"/> objects. The key for each
        /// entry is the unique identifier of corresponding currency in data store.
        /// </summary>
        /// <returns>Collection of all currency items.</returns>
        IEnumerable<KeyValue> GetCurrencies();

        /// <summary>
        /// Retrieves all business partner objects as a collection of <see cref="KeyValue"/> objects. The key for each
        /// entry is the unique identifier of corresponding business partner in data store.
        /// </summary>
        /// <returns>Collection of all business partner items.</returns>
        IEnumerable<KeyValue> GetPartners();

        /// <summary>
        /// Retrieves all business unit objects as a collection of <see cref="KeyValue"/> objects. The key for each
        /// entry is the unique identifier of corresponding business unit in data store.
        /// </summary>
        /// <returns>Collection of all business unit items.</returns>
        IEnumerable<KeyValue> GetBusinessUnits();

        /// <summary>
        /// Retrieves all warehouse objects as a collection of <see cref="KeyValue"/> objects. The key for each
        /// entry is the unique identifier of corresponding warehouse in data store.
        /// </summary>
        /// <returns>Collection of all warehouse items.</returns>
        IEnumerable<KeyValue> GetWarehouses();

        /// <summary>
        /// Retrieves all product objects as a collection of <see cref="KeyValue"/> objects. The key for each
        /// entry is the unique identifier of corresponding product in data store.
        /// </summary>
        /// <returns>Collection of all product items.</returns>
        IEnumerable<KeyValue> GetProducts();

        /// <summary>
        /// Retrieves all unit of measurement (UOM) objects as a collection of <see cref="KeyValue"/> objects.
        /// The key for each entry is the unique identifier of corresponding unit of measurement (UOM) in data store.
        /// </summary>
        /// <returns>Collection of all unit of measurement (UOM) items.</returns>
        IEnumerable<KeyValue> GetUnitsOfMeasurement();

        /// <summary>
        /// Retrieves all requisition voucher type objects as a collection of <see cref="KeyValue"/> objects.
        /// The key for each entry is the unique identifier of corresponding requisition voucher type in database.
        /// </summary>
        /// <returns>Collection of all requisition voucher type items.</returns>
        IEnumerable<KeyValue> GetRequisitionVoucherTypes();

        VoucherDependsViewModel GetRequisitionDepends();

        VoucherLineDependsViewModel GetRequisitionLineDepends();
    }
}
