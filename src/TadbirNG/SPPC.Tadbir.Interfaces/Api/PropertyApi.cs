using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Api
{
    /// <summary>
    /// Defines API server routes and API client operation URLs for working with properties.
    /// </summary>
    public sealed class PropertyApi
    {
        /// <summary>
        /// API client URL for all property items
        /// </summary>
        public const string Properties = "properties";

        /// <summary>
        /// API server route URL for all property items
        /// </summary>
        public const string PropertiesUrl = "properties";

        /// <summary>
        /// API client URL for a property item specified by unique identifier
        /// </summary>
        public const string Property = "properties/{0}";

        /// <summary>
        /// API server route URL for a property item specified by unique identifier
        /// </summary>
        public const string PropertyUrl = "properties/{propertyId:min(1)}";
    }
}
