using System;
using System.Collections.Generic;
using SPPC.Framework.Helpers;
using SPPC.Framework.Service;
using SPPC.Tadbir.Api;
using SPPC.Tadbir.ViewModel.Procurement;

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
        /// Retrieves existing accounts in the specified fiscal period and branch as a lookup collection.
        /// </summary>
        /// <param name="fpId">Unique identifier of the fiscal period to look for accounts</param>
        /// <param name="branchId">Unique identifier of the branch to look for accounts</param>
        /// <returns>Lookup collection of existing accounts in the fiscal period</returns>
        public IEnumerable<KeyValue> LookupAccounts(int fpId, int branchId)
        {
            var accountLookup = _apiClient.Get<IEnumerable<KeyValue>>(
                LookupApi.FiscalPeriodBranchAccounts, fpId, branchId);
            return accountLookup;
        }

        /// <summary>
        /// Retrieves existing detail accounts as a lookup collection
        /// </summary>
        /// <returns>Lookup collection of existing detail accounts</returns>
        public IEnumerable<KeyValue> LookupDetailAccounts()
        {
            var lookup = _apiClient.Get<IEnumerable<KeyValue>>(LookupApi.DetailAccounts);
            return lookup;
        }

        /// <summary>
        /// Retrieves existing cost centers as a lookup collection
        /// </summary>
        /// <returns>Lookup collection of existing cost centers</returns>
        public IEnumerable<KeyValue> LookupCostCenters()
        {
            var lookup = _apiClient.Get<IEnumerable<KeyValue>>(LookupApi.CostCenters);
            return lookup;
        }

        /// <summary>
        /// Retrieves existing projects as a lookup collection
        /// </summary>
        /// <returns>Lookup collection of existing projects</returns>
        public IEnumerable<KeyValue> LookupProjects()
        {
            var lookup = _apiClient.Get<IEnumerable<KeyValue>>(LookupApi.Projects);
            return lookup;
        }

        /// <summary>
        /// Retrieves existing currencies as a lookup collection
        /// </summary>
        /// <returns>Lookup collection of existing currencies</returns>
        public IEnumerable<KeyValue> LookupCurrencies()
        {
            var currencyLookup = _apiClient.Get<IEnumerable<KeyValue>>(LookupApi.CostCenters);
            return currencyLookup;
        }

        /// <summary>
        /// Retrieves existing business partners as a lookup collection
        /// </summary>
        /// <returns>Lookup collection of existing business partners</returns>
        public IEnumerable<KeyValue> LookupPartners()
        {
            var lookup = _apiClient.Get<IEnumerable<KeyValue>>(LookupApi.Partners);
            return lookup;
        }

        /// <summary>
        /// Retrieves existing business units as a lookup collection
        /// </summary>
        /// <returns>Lookup collection of existing business units</returns>
        public IEnumerable<KeyValue> LookupBusinessUnits()
        {
            var lookup = _apiClient.Get<IEnumerable<KeyValue>>(LookupApi.Units);
            return lookup;
        }

        /// <summary>
        /// Retrieves existing warehouses as a lookup collection
        /// </summary>
        /// <returns>Lookup collection of existing warehouses</returns>
        public IEnumerable<KeyValue> LookupWarehouses()
        {
            var lookup = _apiClient.Get<IEnumerable<KeyValue>>(LookupApi.Warehouses);
            return lookup;
        }

        public RequisitionVoucherDependsViewModel LookupRequisitionVoucherDepends()
        {
            var voucherDepends = _apiClient.Get<RequisitionVoucherDependsViewModel>(LookupApi.RequisitionVoucherDepends);
            return voucherDepends;
        }

        private IApiClient _apiClient;
    }
}
