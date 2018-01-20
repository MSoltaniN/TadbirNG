using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Api
{
    public partial class LookupApi
    {
        #region Procurement Subsystem Resource

        /// <summary>
        /// API client URL for lookup collections of all requisition voucher types
        /// </summary>
        public const string RequisitionVoucherTypes = "lookup/rvtypes";

        /// <summary>
        /// API server route URL for lookup collections of all requisition voucher types
        /// </summary>
        public const string RequisitionVoucherTypesUrl = "lookup/rvtypes";

        /// <summary>
        /// API client URL for lookup collections of all dependencies required by a requisition voucher
        /// </summary>
        public const string RequisitionVoucherDepends = "lookup/rvdepends";

        /// <summary>
        /// API server route URL for lookup collections of all dependencies required by a requisition voucher
        /// </summary>
        public const string RequisitionVoucherDependsUrl = "lookup/rvdepends";

        /// <summary>
        /// API client URL for lookup collections of all dependencies required by a requisition voucher line
        /// </summary>
        public const string RequisitionVoucherLineDepends = "lookup/rvldepends";

        /// <summary>
        /// API server route URL for lookup collections of all dependencies required by a requisition voucher line
        /// </summary>
        public const string RequisitionVoucherLineDependsUrl = "lookup/rvldepends";

        #endregion
    }
}
