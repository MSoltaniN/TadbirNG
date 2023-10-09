using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Api
{
    /// <summary>
    /// Defines API server routes and API client operation URLs for working with attributes.
    /// </summary>
    public sealed class AttributeApi
    {
        /// <summary>
        /// API client URL for all attribute items
        /// </summary>
        public const string Attributes = "attributes";

        /// <summary>
        /// API server route URL for all attribute items
        /// </summary>
        public const string AttributesUrl = "attributes";

        /// <summary>
        /// API client URL for a attribute item specified by unique identifier
        /// </summary>
        public const string Attribute = "attributes/{0}";

        /// <summary>
        /// API server route URL for a attribute item specified by unique identifier
        /// </summary>
        public const string AttributeUrl = "attributes/{attributeId:min(1)}";
    }
}
