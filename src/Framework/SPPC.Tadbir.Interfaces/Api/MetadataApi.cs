using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Api
{
    /// <summary>
    /// Defines API server routes and API client operation URLs for working with metadata.
    /// </summary>
    public sealed class MetadataApi
    {
        private MetadataApi()
        {
        }

        /// <summary>
        /// API client URL for metadata of a view specified by name
        /// </summary>
        public const string ViewMetadata = "metadata/view/{0}";

        /// <summary>
        /// API server route URL for metadata of a view specified by name
        /// </summary>
        public const string ViewMetadataUrl = "metadata/view/{viewName}";

        /// <summary>
        /// API client URL for metadata of a view specified by unique identifier
        /// </summary>
        public const string ViewMetadataById = "metadata/view/{0}";

        /// <summary>
        /// API server route URL for metadata of a view specified by unique identifier
        /// </summary>
        public const string ViewMetadataByIdUrl = "metadata/view/{viewId:min(1)}";

        /// <summary>
        /// API client URL for permission metadata in security system
        /// </summary>
        public const string PermissionMetadata = "metadata/permissions";

        /// <summary>
        /// API client URL for permission metadata in security system
        /// </summary>
        public const string PermissionMetadataUrl = "metadata/permissions";

        /// <summary>
        /// API client URL for metadata of all views
        /// </summary>
        public const string ViewsMetadata = "metadata/views";

        /// <summary>
        /// API server route URL for metadata of all views
        /// </summary>
        public const string ViewsMetadataUrl = "metadata/views";
    }
}
