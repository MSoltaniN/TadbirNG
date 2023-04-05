using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Api
{
    /// <summary>
    /// Defines API server routes and API client operation URLs for working with check books report.
    /// </summary>
    public sealed class CheckBookReportApi
    {
        /// <summary>
        /// API client URL for all check book report items
        /// </summary>
        public const string CheckBooksReport = "check-book-report";

        /// <summary>
        /// API server route URL for all check book report items
        /// </summary>
        public const string CheckBooksReportUrl = "check-book-report";

        /// <summary>
        /// API client URL for archiving a check book
        /// </summary>
        public const string ArchiveCheckBooks = "check-books/archive";

        /// <summary>
        /// API server route URL for archiving a check book
        /// </summary>
        public const string ArchiveCheckBooksUrl = "check-books/archive";

        /// <summary>
        /// API server route URL for undoing archive operation on a check book
        /// </summary>
        public const string UndoArchiveCheckBooks = "check-books/archive/undo";

        /// <summary>
        /// API server route URL for undoing archive operation on a check book
        /// </summary>
        public const string UndoArchiveCheckBooksUrl = "check-books/archive/undo";
    }
}
