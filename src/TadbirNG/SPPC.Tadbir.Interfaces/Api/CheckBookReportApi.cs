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
        public const string CheckBooksReport = "check-books-report";

        /// <summary>
        /// API server route URL for all check book report items
        /// </summary>
        public const string CheckBooksReportUrl = "check-books-report";

        /// <summary>
        /// API server route URL for archive check book
        /// </summary>
        public const string ArchiveCheckBooksURL = "check-books/archive";

        /// <summary>
        /// API server route URL for undo archive check book
        /// </summary>
        public const string UndoArchiveCheckBooksURL = "check-books/undo-archive";
    }
}
