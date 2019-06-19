using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Api
{
    /// <summary>
    /// Defines API server routes and API client operation URLs for working with cost centers.
    /// </summary>
    public sealed class CostCenterApi
    {
        private CostCenterApi()
        {
        }

        /// <summary>
        /// API client URL for cost centers defined in current environment
        /// </summary>
        public const string EnvironmentCostCenters = "ccenters";

        /// <summary>
        /// API server route URL for cost centers defined in current environment
        /// </summary>
        public const string EnvironmentCostCentersUrl = "ccenters";

        /// <summary>
        /// API client URL for cost center lookups defined in current environment
        /// </summary>
        public const string EnvironmentCostCentersLookup = "ccenters/lookup";

        /// <summary>
        /// API server route URL for cost center lookups defined in current environment
        /// </summary>
        public const string EnvironmentCostCentersLookupUrl = "ccenters/lookup";

        /// <summary>
        /// API client URL for cost center ledger defined in current environment
        /// </summary>
        public const string EnvironmentCostCentersLedger = "ccenters/ledger";

        /// <summary>
        /// API server route URL for cost center ledger defined in current environment
        /// </summary>
        public const string EnvironmentCostCentersLedgerUrl = "ccenters/ledger";

        /// <summary>
        /// API client URL for a cost center item specified by unique identifier
        /// </summary>
        public const string CostCenter = "ccenters/{0}";

        /// <summary>
        /// API server route URL for a cost center item specified by unique identifier
        /// </summary>
        public const string CostCenterUrl = "ccenters/{ccenterId:min(1)}";

        /// <summary>
        /// API client URL for previous (accessible) cost center relative to a single cost center specified by unique identifier
        /// </summary>
        public const string PreviousEnvironmentCostCenter = "ccenters/{0}/previous";

        /// <summary>
        /// API server route URL for previous (accessible) cost center relative to a single cost center specified by unique identifier
        /// </summary>
        public const string PreviousEnvironmentCostCenterUrl = "ccenters/{ccenterId:min(1)}/previous";

        /// <summary>
        /// API client URL for next (accessible) cost center relative to a single cost center specified by unique identifier
        /// </summary>
        public const string NextEnvironmentCostCenter = "ccenters/{0}/next";

        /// <summary>
        /// API server route URL for next (accessible) cost center relative to a single cost center specified by unique identifier
        /// </summary>
        public const string NextEnvironmentCostCenterUrl = "ccenters/{ccenterId:min(1)}/next";

        /// <summary>
        /// API client URL for all child cost centers under a specific cost center in hierarchy
        /// </summary>
        public const string CostCenterChildren = "ccenters/{0}/children";

        /// <summary>
        /// API server route URL for all child cost centers under a specific cost center in hierarchy
        /// </summary>
        public const string CostCenterChildrenUrl = "ccenters/{ccenterId:min(1)}/children";

        /// <summary>
        /// API client URL for a new child for a parent cost center specified by unique identifier
        /// </summary>
        public const string EnvironmentNewChildCostCenter = "ccenters/{0}/children/new";

        /// <summary>
        /// API server route URL for a new child for a parent cost center specified by unique identifier
        /// </summary>
        public const string EnvironmentNewChildCostCenterUrl = "ccenters/{ccenterId:int}/children/new";

        /// <summary>
        /// API client URL for cost center full code
        /// </summary>
        public const string CostCenterFullCode = "ccenters/fullcode/{0}";

        /// <summary>
        /// API server route URL for cost center full code
        /// </summary>
        public const string CostCenterFullCodeUrl = "ccenters/fullcode/{parentId}";
    }
}
