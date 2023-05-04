using System;

namespace SPPC.Tadbir.Api
{
    /// <summary>
    /// Defines API server routes and API client operation URLs for working with user values
    /// </summary>
    public sealed class UserValueApi
    {
        /// <summary>
        /// API client URL for all categories of user values
        /// </summary>
        public const string Categories = "user-values/categories";

        /// <summary>
        /// API server route URL for all categories of user values
        /// </summary>
        public const string CategoriesUrl = "user-values/categories";

        /// <summary>
        /// API client URL for all user values in a category specified by unique identifier
        /// </summary>
        public const string CategoryValues = "user-values/categories/{0}/values";

        /// <summary>
        /// API server route URL for all user values in a category specified by unique identifier
        /// </summary>
        public const string CategoryValuesUrl = "user-values/categories/{categoryId:min(1)}/values";

        /// <summary>
        /// API client URL for all user values
        /// </summary>
        public const string UserValues = "user-values/values";

        /// <summary>
        /// API server route URL for all user values
        /// </summary>
        public const string UserValuesUrl = "user-values/values";

        /// <summary>
        /// API client URL for a single user value specified by unique identifier
        /// </summary>
        public const string UserValue = "user-values/values/{0}";

        /// <summary>
        /// API server route URL for a single user value specified by unique identifier
        /// </summary>
        public const string UserValueUrl = "user-values/values/{valueId:min(1)}";
    }
}
