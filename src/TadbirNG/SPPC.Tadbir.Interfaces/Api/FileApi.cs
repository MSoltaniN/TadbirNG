using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Api
{
    /// <summary>
    /// Defines API server routes and API client operation URLs for working with files.
    /// </summary>
    public sealed class FileApi
    {
        /// <summary>
        /// API client URL for all file items
        /// </summary>
        public const string Files = "files";

        /// <summary>
        /// API server route URL for all file items
        /// </summary>
        public const string FilesUrl = "files";

        /// <summary>
        /// API client URL for a file item specified by unique identifier
        /// </summary>
        public const string File = "files/{0}";

        /// <summary>
        /// API server route URL for a file item specified by unique identifier
        /// </summary>
        public const string FileUrl = "files/{fileId:min(1)}";
    }
}
