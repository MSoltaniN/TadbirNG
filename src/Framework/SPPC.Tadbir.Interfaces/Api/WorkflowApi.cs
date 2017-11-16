using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Api
{
    /// <summary>
    /// Defines API server routes and API client operation URLs for reading workflow information.
    /// </summary>
    public sealed class WorkflowApi
    {
        private WorkflowApi()
        {
        }

        /// <summary>
        /// API client URL for all workflows that are currently active
        /// </summary>
        public const string RunningWorkflows = "workflows/running";

        /// <summary>
        /// API server route URL for all workflows that are currently active
        /// </summary>
        public const string RunningWorkflowsUrl = "workflows/running";
    }
}
