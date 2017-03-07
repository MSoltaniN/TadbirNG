﻿using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Api
{
    /// <summary>
    /// Defines API server routes and API client operation URLs for working with application users.
    /// </summary>
    public sealed class SecurityApi
    {
        private SecurityApi()
        {
        }

        /// <summary>
        /// API client URL for all users
        /// </summary>
        public const string Users = "users";

        /// <summary>
        /// API server route URL for all users
        /// </summary>
        public const string UsersUrl = "users";

        /// <summary>
        /// API client URL for a user specified by user name.
        /// </summary>
        public const string UserByName = "users/{0}";

        /// <summary>
        /// API server route URL for a user specified by user name.
        /// </summary>
        public const string UserByNameUrl = "users/{userName}";
    }
}
