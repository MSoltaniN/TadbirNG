﻿using System;
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
    }
}