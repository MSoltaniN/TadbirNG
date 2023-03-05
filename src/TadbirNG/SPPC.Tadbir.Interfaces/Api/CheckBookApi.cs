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
        /// API client URL for all check Book items
        /// </summary>
        public const string CheckBooks = "check-books";

        /// <summary>
        /// API server route URL for all check Book items
        /// </summary>
        public const string CheckBooksUrl = "check-books";

        /// <summary>
        /// API client URL for a check Book item specified by unique identifier
        /// </summary>
        public const string CheckBook = "check-books/{0}";

        /// <summary>
        /// API server route URL for a check Book item specified by unique identifier
        /// </summary>
        public const string CheckBookUrl = "check-books/{checkbookId:min(1)}";

        /// <summary>
        /// API client URL for a single check Book specified by number
        /// </summary>
        public const string CheckBookByNo = "check-books/by-no/{0}";

        /// <summary>
        /// API server route URL for a single check Book specified by number
        /// </summary>
        public const string CheckBookByNoUrl = "check-books/by-no/{checkbookNo:min(1)}";

        /// <summary>
        /// API client URL for all Pages in a single Check Book specified by identifier
        /// </summary>
        public const string CheckBookPages = "check-book/{0}/pages";

        /// <summary>
        /// API server route URL for all pages in a single check Book specified by identifier
        /// </summary>
        public const string CheckBookPagesUrl = "check-book/{checkbookId:min(1)}/pages";

        /// <summary>
        /// API client URL for a single check Book page specified by identifier
        /// </summary>
        public const string CheckBookPage = "pages/pages/{0}";

        /// <summary>
        /// API server route URL for a single check Book page specified by identifier
        /// </summary>
        public const string CheckBookPageUrl = "pages/pages/{checkbookPageId:min(1)}";
    }
}
