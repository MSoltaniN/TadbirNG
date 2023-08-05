using System;

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
        /// API client URL for root cost centers defined in current environment
        /// </summary>
        public const string RootCostCenters = "ccenters/root";

        /// <summary>
        /// API server route URL for root cost centers defined in current environment
        /// </summary>
        public const string RootCostCentersUrl = "ccenters/root";

        /// <summary>
        /// API client URL for a cost center item specified by unique identifier
        /// </summary>
        public const string CostCenter = "ccenters/{0}";

        /// <summary>
        /// API server route URL for a cost center item specified by unique identifier
        /// </summary>
        public const string CostCenterUrl = "ccenters/{ccenterId:min(1)}";

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
        public const string NewChildCostCenter = "ccenters/{0}/children/new";

        /// <summary>
        /// API server route URL for a new child for a parent cost center specified by unique identifier
        /// </summary>
        public const string NewChildCostCenterUrl = "ccenters/{ccenterId:int}/children/new";

        /// <summary>
        /// API client URL for cost center full code
        /// </summary>
        public const string CostCenterFullCode = "ccenters/{0}/fullcode";

        /// <summary>
        /// API server route URL for cost center full code
        /// </summary>
        public const string CostCenterFullCodeUrl = "ccenters/{ccenterId:int}/fullcode";

        /// <summary>
        /// API client URL for marking an active cost center as inactive
        /// </summary>
        public const string DeactivateCostCenter = "ccenters/{0}/deactivate";

        /// <summary>
        /// API server route URL for marking an active cost center as inactive
        /// </summary>
        public const string DeactivateCostCenterUrl = "ccenters/{ccenterId:min(1)}/deactivate";

        /// <summary>
        /// API client URL for marking an inactive cost center as active
        /// </summary>
        public const string ReactivateCostCenter = "ccenters/{0}/reactivate";

        /// <summary>
        /// API server route URL for marking an inactive cost center as active
        /// </summary>
        public const string ReactivateCostCenterUrl = "ccenters/{ccenterId:min(1)}/reactivate";
    }
}
