using System;

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
        /// API client URL for projects ledger defined in current environment
        /// </summary>
        public const string RootProjects = "projects/root";

        /// <summary>
        /// API server route URL for projects ledger defined in current environment
        /// </summary>
        public const string RootProjectsUrl = "projects/root";

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
        public const string NewChildProject = "projects/{0}/children/new";

        /// <summary>
        /// API server route URL for a new child for a parent project specified by unique identifier
        /// </summary>
        public const string NewChildProjectUrl = "projects/{projectId:int}/children/new";

        /// <summary>
        /// API client URL for project full code
        /// </summary>
        public const string ProjectFullCode = "projects/{0}/fullcode";

        /// <summary>
        /// API server route URL for project full code
        /// </summary>
        public const string ProjectFullCodeUrl = "projects/{projectId:int}/fullcode";

        /// <summary>
        /// API client URL for marking an active project as inactive
        /// </summary>
        public const string DeactivateProject = "projects/{0}/deactivate";

        /// <summary>
        /// API server route URL for marking an active project as inactive
        /// </summary>
        public const string DeactivateProjectUrl = "projects/{projectId:min(1)}/deactivate";

        /// <summary>
        /// API client URL for marking an inactive project as active
        /// </summary>
        public const string ReactivateProject = "projects/{0}/reactivate";

        /// <summary>
        /// API server route URL for marking an inactive project as active
        /// </summary>
        public const string ReactivateProjectUrl = "projects/{projectId:min(1)}/reactivate";
    }
}
