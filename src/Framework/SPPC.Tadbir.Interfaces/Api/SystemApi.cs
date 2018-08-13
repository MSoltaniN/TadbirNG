using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Api
{
    /// <summary>
    /// Defines API server routes and API client operation URLs for working with system facilities.
    /// </summary>
    public sealed class SystemApi
    {
        private SystemApi()
        {
        }

        /// <summary>
        /// API client URL for all operation logs created for a company specified by database identifier
        /// </summary>
        public const string CompanyOperationLogs = "system/oplog/company/{0}";

        /// <summary>
        /// API server route URL for all operation logs created for a company specified by database identifier
        /// </summary>
        public const string CompanyOperationLogsUrl = "system/oplog/company/{companyId:int}";

        /// <summary>
        /// API client URL for all operation logs created for a user specified by database identifier
        /// </summary>
        public const string UserOperationLogs = "system/oplog/user/{0}";

        /// <summary>
        /// API server route URL for all operation logs created for a user specified by database identifier
        /// </summary>
        public const string UserOperationLogsUrl = "system/oplog/user/{userId:int}";

        /// <summary>
        /// API client URL for all operation logs created for a specific user while working in a specific company
        /// </summary>
        public const string UserCompanyOperationLogs = "system/oplog/user/{0}/company/{1}";

        /// <summary>
        /// API server route URL for all operation logs created for a specific user while working in a specific company
        /// </summary>
        public const string UserCompanyOperationLogsUrl = "system/oplog/user/{userId:int}/company/{companyId:int}";
    }
}
