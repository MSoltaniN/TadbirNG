using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Api
{
    /// <summary>
    /// Defines API server routes and API client operation URLs for working with checkbooks.
    /// </summary>
    public sealed class CheckBookApi
    {
        /// <summary>
        /// API client URL for all checkbook items
        /// </summary>
        public const string CheckBooks = "checkBooks";

        /// <summary>
        /// API server route URL for all checkbook items
        /// </summary>
        public const string CheckBooksUrl = "checkbooks";

        /// <summary>
        /// API client URL for a checkbook item specified by unique identifier
        /// </summary>
        public const string CheckBook = "checkbooks/{0}";

        /// <summary>
        /// API server route URL for a checkbook item specified by unique identifier
        /// </summary>
        public const string CheckBookUrl = "checkBooks/{checkBookId:min(1)}";
    }
}
