using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Api
{
    /// <summary>
    /// Defines API server routes and API client operation URLs for working with check books
    /// </summary>
    public sealed class CheckBookApi
    {
        /// <summary>
        /// API client URL for all check book items
        /// </summary>
        public const string CheckBooks = "check-books";

        /// <summary>
        /// API server route URL for all check book items
        /// </summary>
        public const string CheckBooksUrl = "check-books";

        /// <summary>
        /// API client URL for a check book item specified by unique identifier
        /// </summary>
        public const string CheckBook = "check-books/{0}";

        /// <summary>
        /// API server route URL for a check book item specified by unique identifier
        /// </summary>
        public const string CheckBookUrl = "check-books/{checkBookId:min(1)}";

        /// <summary>
        /// API client URL for a single check book specified by number
        /// </summary>
        public const string CheckBookByNo = "check-books/by-no/{0}";

        /// <summary>
        /// API server route URL for a single check book specified by number
        /// </summary>
        public const string CheckBookByNoUrl = "check-books/by-no/{checkBookNo:min(1)}";

        /// <summary>
        /// API client URL for all pages in a single check book specified by identifier
        /// </summary>
        public const string CheckBookPages = "check-books/{0}/pages";

        /// <summary>
        /// API server route URL for all pages in a single check book specified by identifier
        /// </summary>
        public const string CheckBookPagesUrl = "check-books/{checkBookId:min(1)}/pages";

        /// <summary>
        /// API server route URL for cancelling a single page specified by identifier
        /// </summary>
        public const string CancelPage = "check-books/pages/{0}/cancel";

        /// <summary>
        /// API server route URL for cancelling a single page specified by identifier
        /// </summary>
        public const string CancelPageUrl = "check-books/pages/{pageId:min(1)}/cancel";

        /// <summary>
        /// API server route URL for undoing a cancelled page specified by identifier
        /// </summary>
        public const string UndoCancelPage = "check-books/pages/{0}/cancel/undo";

        /// <summary>
        /// API server route URL for undoing a cancelled page specified by identifier
        /// </summary>
        public const string UndoCancelPageUrl = "check-books/pages/{pageId:min(1)}/cancel/undo";

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

        /// <summary>
        /// API client URL for the first check book in current environment
        /// </summary>
        public const string FirstCheckBook = "check-books/first";

        /// <summary>
        /// API server route URL for the first check book in current environment
        /// </summary>
        public const string FirstCheckBookUrl = "check-books/first";

        /// <summary>
        /// API client URL for previous check book in current environment
        /// </summary>
        public const string PreviousCheckBook = "check-books/{0}/previous";

        /// <summary>
        /// API server route URL for previous check book in current environment
        /// </summary>
        public const string PreviousCheckBookUrl = "check-books/{issueDate:DateTime}/previous";

        /// <summary>
        /// API client URL for next check book in current environment
        /// </summary>
        public const string NextCheckBook = "check-books/{0}/next";

        /// <summary>
        /// API server route URL for next check book in current environment
        /// </summary>
        public const string NextCheckBookUrl = "check-books/{issueDate:DateTime}/next";

        /// <summary>
        /// API client URL for the last check book in current environment
        /// </summary>
        public const string LastCheckBook = "check-books/last";

        /// <summary>
        /// API server route URL for the last check book in current environment
        /// </summary>
        public const string LastCheckBookUrl = "check-books/last";

        /// <summary>
        /// API client URL for a newly initialized check book
        /// </summary>
        public const string NewCheckBook = "check-books/new";

        /// <summary>
        /// API server route URL for a newly initialized check book
        /// </summary>
        public const string NewCheckBookUrl = "check-books/new";
    }
}
