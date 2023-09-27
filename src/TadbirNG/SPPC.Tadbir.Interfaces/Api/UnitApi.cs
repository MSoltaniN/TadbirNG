using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Api
{
    /// <summary>
    /// Defines API server routes and API client operation URLs for working with units.
    /// </summary>
    public sealed class UnitApi
    {
        /// <summary>
        /// API client URL for all unit items
        /// </summary>
        public const string Units = "units";

        /// <summary>
        /// API server route URL for all unit items
        /// </summary>
        public const string UnitsUrl = "units";

        /// <summary>
        /// API client URL for a unit item specified by unique identifier
        /// </summary>
        public const string Unit = "units/{0}";

        /// <summary>
        /// API server route URL for a unit item specified by unique identifier
        /// </summary>
        public const string UnitUrl = "units/{unitId:min(1)}";
    }
}
