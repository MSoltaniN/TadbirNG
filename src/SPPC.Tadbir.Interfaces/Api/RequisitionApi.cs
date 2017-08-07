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

        /// <summary>
        /// API client URL for all requisitions
        /// </summary>
        public const string Requisitions = "requisitions";

        /// <summary>
        /// API server route URL for all requisitions
        /// </summary>
        public const string RequisitionsUrl = "requisitions";

        /// <summary>
        /// API client URL for a single requisition voucher specified by identifier
        /// </summary>
        public const string Requisition = "requisitions/{0}";

        /// <summary>
        /// API server route URL for a single requisition voucher specified by identifier
        /// </summary>
        public const string RequisitionUrl = "requisitions/{voucherId:int}";

        /// <summary>
        /// API client URL for details of a single requisition voucher specified by identifier
        /// </summary>
        public const string RequisitionDetails = "requisitions/{0}/details";

        /// <summary>
        /// API server route URL for details of a single requisition voucher specified by identifier
        /// </summary>
        public const string RequisitionDetailsUrl = "requisitions/{voucherId:int}/details";
    }
}
