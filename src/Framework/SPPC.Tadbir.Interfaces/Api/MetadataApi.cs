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
        /// API client URL for entity metadata
        /// </summary>
        public const string EntityMetadata = "metadata/entity/{0}";

        /// <summary>
        /// API server route URL for entity metadata
        /// </summary>
        public const string EntityMetadataUrl = "metadata/entity/{entityName}";

        /// <summary>
        /// API client URL for entity metadata
        /// </summary>
        public const string EntityMetadataById = "metadata/entity/{0}";

        /// <summary>
        /// API server route URL for entity metadata
        /// </summary>
        public const string EntityMetadataByIdUrl = "metadata/entity/{entityId:min(1)}";
    }
}
