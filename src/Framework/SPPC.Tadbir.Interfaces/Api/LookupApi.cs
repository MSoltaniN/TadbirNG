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

        /// <summary>
        /// API client URL for lookup collection of all security roles
        /// </summary>
        public const string Roles = "lookup/roles";

        /// <summary>
        /// API server route URL for lookup collection of all security roles
        /// </summary>
        public const string RolesUrl = "lookup/roles";

        /// <summary>
        /// API client URL for lookup collection of all entity views
        /// </summary>
        public const string EntityViews = "lookup/views";

        /// <summary>
        /// API server route URL for lookup collection of all entity views
        /// </summary>
        public const string EntityViewsUrl = "lookup/views";

        /// <summary>
        /// API client URL for lookup collection of all base entity views
        /// </summary>
        public const string BaseEntityViews = "lookup/views/base";

        /// <summary>
        /// API server route URL for lookup collection of all base entity views
        /// </summary>
        public const string BaseEntityViewsUrl = "lookup/views/base";

        /// <summary>
        /// API client URL for lookup collection of all hierarchy views
        /// </summary>
        public const string TreeViews = "lookup/views/tree";

        /// <summary>
        /// API server route URL for lookup collection of all hierarchy views
        /// </summary>
        public const string TreeViewsUrl = "lookup/views/tree";

        /// <summary>
        /// API client URL for all application entity types
        /// </summary>
        public const string EntityTypes = "lookup/entities";

        /// <summary>
        /// API server route URL for all application entity types
        /// </summary>
        public const string EntityTypesUrl = "lookup/entities";

        /// <summary>
        /// API client URL for all system entity types
        /// </summary>
        public const string SystemEntityTypes = "lookup/sys/entities";

        /// <summary>
        /// API server route URL for all system entity types
        /// </summary>
        public const string SystemEntityTypesUrl = "lookup/sys/entities";

        /// <summary>
        /// API client URL for all application users
        /// </summary>
        public const string Users = "lookup/users";

        /// <summary>
        /// API server route URL for all application users
        /// </summary>
        public const string UsersUrl = "lookup/users";
    }
}
