using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Api
{
    /// <summary>
    /// Defines API server routes and API client operation URLs for working with system facilities.
    /// </summary>
    public sealed class OperationLogApi
    {
        private OperationLogApi()
        {
        }

        /// <summary>
        /// API client URL for all operation logs
        /// </summary>
        public const string OperationLogs = "system/oplog";

        /// <summary>
        /// API server route URL for all operation logs
        /// </summary>
        public const string OperationLogsUrl = "system/oplog";

        /// <summary>
        /// API client URL for all system operation logs
        /// </summary>
        public const string SysOperationLogs = "system/sys-oplog";

        /// <summary>
        /// API server route URL for all system operation logs
        /// </summary>
        public const string SysOperationLogsUrl = "system/sys-oplog";

        /// <summary>
        /// API client URL for merged view of operation logs and log archive
        /// </summary>
        public const string AllOperationLogs = "system/oplog/all";

        /// <summary>
        /// API server route URL for merged view of operation logs and log archive
        /// </summary>
        public const string AllOperationLogsUrl = "system/oplog/all";

        /// <summary>
        /// API client URL for merged view of system operation logs and system log archive
        /// </summary>
        public const string AllSysOperationLogs = "system/sys-oplog/all";

        /// <summary>
        /// API server route URL for merged view of system operation logs and system log archive
        /// </summary>
        public const string AllSysOperationLogsUrl = "system/sys-oplog/all";

        /// <summary>
        /// API client URL for all operation logs
        /// </summary>
        public const string OperationLogsArchive = "system/oplog/archive";

        /// <summary>
        /// API server route URL for all operation logs
        /// </summary>
        public const string OperationLogsArchiveUrl = "system/oplog/archive";

        /// <summary>
        /// API client URL for all system operation logs
        /// </summary>
        public const string SysOperationLogsArchive = "system/sys-oplog/archive";

        /// <summary>
        /// API server route URL for all system operation logs
        /// </summary>
        public const string SysOperationLogsArchiveUrl = "system/sys-oplog/archive";

        /// <summary>
        /// API client URL for operation log metadata
        /// </summary>
        public const string OperationLogMetadata = "system/oplog/metadata";

        /// <summary>
        /// API server route URL for all operation log metadata
        /// </summary>
        public const string OperationLogMetadataUrl = "system/oplog/metadata";
    }
}
