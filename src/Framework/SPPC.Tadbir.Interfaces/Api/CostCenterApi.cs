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
        /// API client URL for cost centers defined in a fiscal period and a corporate branch
        /// </summary>
        public const string FiscalPeriodBranchCostCenters = "ccenters/fp/{0}/branch/{1}";

        /// <summary>
        /// API server route URL for cost centers defined in a fiscal period and a corporate branch
        /// </summary>
        public const string FiscalPeriodBranchCostCentersUrl = "ccenters/fp/{fpId:min(1)}/branch/{branchId:min(1)}";

        /// <summary>
        /// API client URL for all cost center items
        /// </summary>
        public const string CostCenters = "ccenters";

        /// <summary>
        /// API server route URL for all cost center items
        /// </summary>
        public const string CostCentersUrl = "ccenters";

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
        /// API client URL for cost center metadata
        /// </summary>
        public const string CostCenterMetadata = "ccenters/metadata";

        /// <summary>
        /// API server route URL for cost center metadata
        /// </summary>
        public const string CostCenterMetadataUrl = "ccenters/metadata";
    }
}
