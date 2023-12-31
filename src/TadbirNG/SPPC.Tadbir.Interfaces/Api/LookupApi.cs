﻿using System;

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
        /// API server route URL for lookup a entity views
        /// </summary>
        public const string EntityViewUrl = "lookup/view/{viewId}";

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

        /// <summary>
        /// API client URL for all provinces
        /// </summary>
        public const string Provinces = "lookup/provinces";

        /// <summary>
        /// API server route URL for all provinces
        /// </summary>
        public const string ProvincesUrl = "lookup/provinces";

        /// <summary>
        /// API client URL for all cities of province
        /// </summary>
        public const string Cities = "lookup/cities/{provinceCode}";

        /// <summary>
        /// API server route URL for cities of province
        /// </summary>
        public const string CitiesUrl = "lookup/cities/{provinceCode}";

        /// <summary>
        /// API client URL for all row premissions applicable to a view specified by unique identifier
        /// </summary>
        public const string ValidRowPermissions = "lookup/rowaccess/views/{0}";

        /// <summary>
        /// API server route URL for all row premissions applicable to a view specified by unique identifier
        /// </summary>
        public const string ValidRowPermissionsUrl = "lookup/rowaccess/views/{viewId:min(1)}";

        /// <summary>
        /// API client URL for lookup collection of all source/application based on type
        /// </summary>
        public const string SourceApps = "lookup/source-apps/types/{0}";

        /// <summary>
        /// API server route URL for lookup collection of all source/application based on type
        /// </summary>
        public const string SourceAppsUrl = "lookup/source-apps/types/{sourceAppType}";
    }
}
