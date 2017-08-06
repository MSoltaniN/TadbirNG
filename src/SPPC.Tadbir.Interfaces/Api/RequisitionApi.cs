using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Api
{
    /// <summary>
    /// Defines API server routes and API client operation URLs for working with procurement requisitions and articles.
    /// </summary>
    public sealed class RequisitionApi
    {
        private RequisitionApi()
        {
        }

        /// <summary>
        /// API client URL for all requisitions defined in a fiscal period and a corporate branch
        /// </summary>
        public const string FiscalPeriodBranchRequisitions = "requisitions/fp/{0}/branch/{1}";

        /// <summary>
        /// API server route URL for all requisitions defined in a fiscal period and a corporate branch
        /// </summary>
        public const string FiscalPeriodBranchRequisitionsUrl = "requisitions/fp/{fpId:int}/branch/{branchId:int}";
    }
}
