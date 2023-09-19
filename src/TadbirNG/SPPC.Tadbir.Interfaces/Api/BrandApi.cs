using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Api
{
    /// <summary>
    /// Defines API server routes and API client operation URLs for working with brands.
    /// </summary>
    public sealed class BrandApi
    {
        /// <summary>
        /// API client URL for all brand items
        /// </summary>
        public const string Brands = "brands";

        /// <summary>
        /// API server route URL for all brand items
        /// </summary>
        public const string BrandsUrl = "brands";

        /// <summary>
        /// API client URL for a brand item specified by unique identifier
        /// </summary>
        public const string Brand = "brands/{0}";

        /// <summary>
        /// API server route URL for a brand item specified by unique identifier
        /// </summary>
        public const string BrandUrl = "brands/{brandId:min(1)}";
    }
}
