using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Api
{
    /// <summary>
    /// Defines API server routes and API client operation URLs for working with Advanced Filters.
    /// </summary>
    public sealed class FilterApi
    {
        private FilterApi()
        {
        }

        /// <summary>
        /// API client URL for all advanced filters defined for a view and accessible to current user
        /// </summary>
        public const string FiltersByView = "filters/views/{0}";

        /// <summary>
        /// API server route URL for all advanced filters defined for a view and accessible to current user
        /// </summary>
        public const string FiltersByViewUrl = "filters/views/{viewId:min(1)}";

        /// <summary>
        /// API client URL for all advanced filters
        /// </summary>
        public const string Filters = "filters";

        /// <summary>
        /// API server route URL for all advanced filters
        /// </summary>
        public const string FiltersUrl = "filters";

        /// <summary>
        /// API client URL for a specific advanced filter specified by database identifier
        /// </summary>
        public const string Filter = "filters/{0}";

        /// <summary>
        /// API server route URL for a specific advanced filter specified by database identifier
        /// </summary>
        public const string FilterUrl = "filters/{filterId:min(1)}";
    }
}
