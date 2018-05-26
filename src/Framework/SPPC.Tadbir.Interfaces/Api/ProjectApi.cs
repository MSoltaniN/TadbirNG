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
        /// API client URL for projects defined in a fiscal period and a corporate branch
        /// </summary>
        public const string FiscalPeriodBranchProjects = "projects/fp/{0}/branch/{0}";

        /// <summary>
        /// API server route URL for projects defined in a fiscal period and a corporate branch
        /// </summary>
        public const string FiscalPeriodBranchProjectsUrl = "projects/fp/{fpId:min(1)}/branch/{branchId:min(1)}";

        /// <summary>
        /// API client URL for all project items
        /// </summary>
        public const string Projects = "projects";

        /// <summary>
        /// API server route URL for all project items
        /// </summary>
        public const string ProjectsUrl = "projects";

        /// <summary>
        /// API client URL for a project item specified by unique identifier
        /// </summary>
        public const string Project = "projects/{0}";

        /// <summary>
        /// API server route URL for a project item specified by unique identifier
        /// </summary>
        public const string ProjectUrl = "projects/{projectId:min(1)}";

        /// <summary>
        /// API client URL for project metadata
        /// </summary>
        public const string ProjectMetadata = "projects/metadata";

        /// <summary>
        /// API server route URL for project metadata
        /// </summary>
        public const string ProjectMetadataUrl = "projects/metadata";
    }
}