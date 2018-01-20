using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Api
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
        /// API client URL for lookup collection of all business partners
        /// </summary>
        public const string Partners = "lookup/partners";

        /// <summary>
        /// API server route URL for lookup collection of all business partners
        /// </summary>
        public const string PartnersUrl = "lookup/partners";

        /// <summary>
        /// API client URL for lookup collection of all business units
        /// </summary>
        public const string Units = "lookup/units";

        /// <summary>
        /// API server route URL for lookup collection of all business units
        /// </summary>
        public const string UnitsUrl = "lookup/units";
    }
}
