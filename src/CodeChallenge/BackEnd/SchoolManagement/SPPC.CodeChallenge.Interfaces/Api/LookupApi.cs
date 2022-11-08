using System;

namespace SPPC.CodeChallenge.Api
{
    /// <summary>
    /// Defines API server routes and API client operation URLs for working with entity lookup collections.
    /// </summary>
    public sealed partial class LookupApi
    {
        private LookupApi()
        {
        }

        /// <summary>
        /// API client URL for all provinces
        /// </summary>
        public const string Provinces = "lookup/provinces";

        /// <summary>
        /// API server route URL for all provinces
        /// </summary>
        public const string ProvincesUrl = "lookup/provinces";

        /// <summary>
        /// API client URL for all cities of a province
        /// </summary>
        public const string Cities = "lookup/cities/{0}";

        /// <summary>
        /// API server route URL for cities of a province
        /// </summary>
        public const string CitiesUrl = "lookup/cities/{provinceId:min(1)}";
    }
}
