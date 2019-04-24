using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Api
{
    /// <summary>
    /// Defines API server routes and API client operation URLs for working with projects.
    /// </summary>
    public sealed class ProjectApi
    {
        private ProjectApi()
        {
        }

        /// <summary>
        /// API client URL for projects defined in current environment
        /// </summary>
        public const string EnvironmentProjects = "projects";

        /// <summary>
        /// API server route URL for projects defined in current environment
        /// </summary>
        public const string EnvironmentProjectsUrl = "projects";

        /// <summary>
        /// API client URL for project lookups defined in current environment
        /// </summary>
        public const string EnvironmentProjectsLookup = "projects/lookup";

        /// <summary>
        /// API server route URL for project lookups defined in current environment
        /// </summary>
        public const string EnvironmentProjectsLookupUrl = "projects/lookup";

        /// <summary>
        /// API client URL for projects ledger defined in current environment
        /// </summary>
        public const string EnvironmentProjectsLedger = "projects/ledger";

        /// <summary>
        /// API server route URL for projects ledger defined in current environment
        /// </summary>
        public const string EnvironmentProjectsLedgerUrl = "projects/ledger";

        /// <summary>
        /// API client URL for a project item specified by unique identifier
        /// </summary>
        public const string Project = "projects/{0}";

        /// <summary>
        /// API server route URL for a project item specified by unique identifier
        /// </summary>
        public const string ProjectUrl = "projects/{projectId:min(1)}";

        /// <summary>
        /// API client URL for all child projects under a specific project in hierarchy
        /// </summary>
        public const string ProjectChildren = "projects/{0}/children";

        /// <summary>
        /// API server route URL for all child projects under a specific project in hierarchy
        /// </summary>
        public const string ProjectChildrenUrl = "projects/{projectId:min(1)}/children";

        /// <summary>
        /// API client URL for a new child for a parent project specified by unique identifier
        /// </summary>
        public const string EnvironmentNewChildProject = "projects/{0}/children/new";

        /// <summary>
        /// API server route URL for a new child for a parent project specified by unique identifier
        /// </summary>
        public const string EnvironmentNewChildProjectUrl = "projects/{projectId:int}/children/new";

        /// <summary>
        /// API client URL for project metadata
        /// </summary>
        public const string ProjectMetadata = "projects/metadata";

        /// <summary>
        /// API server route URL for project metadata
        /// </summary>
        public const string ProjectMetadataUrl = "projects/metadata";

        /// <summary>
        /// API client URL for project full code
        /// </summary>
        public const string ProjectFullCode = "projects/fullcode/{0}";

        /// <summary>
        /// API server route URL for project full code
        /// </summary>
        public const string ProjectFullCodeUrl = "projects/fullcode/{parentId}";
    }
}
