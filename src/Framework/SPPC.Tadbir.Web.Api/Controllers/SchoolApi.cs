using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Api
{
    /// <summary>
    /// Defines API server routes and API client operation URLs for working with schools.
    /// </summary>
    public sealed class SchoolApi
    {
        /// <summary>
        /// API client URL for all school items
        /// </summary>
        public const string Schools = "schools";

        /// <summary>
        /// API server route URL for all school items
        /// </summary>
        public const string SchoolsUrl = "schools";

        /// <summary>
        /// API client URL for a school item specified by unique identifier
        /// </summary>
        public const string School = "schools/{0}";

        /// <summary>
        /// API server route URL for a school item specified by unique identifier
        /// </summary>
        public const string SchoolUrl = "schools/{schoolId:min(1)}";
    }
}
