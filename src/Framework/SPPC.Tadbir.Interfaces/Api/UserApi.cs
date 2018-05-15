using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Api
{
    /// <summary>
    /// Defines API server routes and API client operation URLs for working with application users.
    /// </summary>
    public sealed class UserApi
    {
        private UserApi()
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
        /// API server route URL for all users
        /// </summary>
        public const string UsersSyncUrl = "users/sync";

        /// <summary>
        /// API client URL for a user specified by unique identifier.
        /// </summary>
        public const string User = "users/{0}";

        /// <summary>
        /// API server route URL for a user specified by unique identifier.
        /// </summary>
        public const string UserUrl = "users/{userId:min(1)}";

        /// <summary>
        /// API server route URL for a user specified by unique identifier.
        /// </summary>
        public const string UserSyncUrl = "users/{userId:min(1)}/sync";

        /// <summary>
        /// API client URL for a user's last login statistics.
        /// </summary>
        public const string UserLastLogin = "users/{0}/login";

        /// <summary>
        /// API server route URL for a user's last login statistics.
        /// </summary>
        public const string UserLastLoginUrl = "users/{userId:min(1)}/login";

        /// <summary>
        /// API server route URL for a user's last login statistics.
        /// </summary>
        public const string UserLastLoginSyncUrl = "users/{userId:min(1)}/login/sync";

        /// <summary>
        /// API client URL for a user specified by user name.
        /// </summary>
        public const string UserByName = "users/name/{0}";

        /// <summary>
        /// API server route URL for a user specified by user name.
        /// </summary>
        public const string UserByNameUrl = "users/name/{userName}";

        /// <summary>
        /// API server route URL for a user specified by user name.
        /// </summary>
        public const string UserByNameSyncUrl = "users/name/{userName}/sync";

        /// <summary>
        /// API client URL for the password of a user specified by user name.
        /// </summary>
        public const string UserPassword = "users/{0}/password";

        /// <summary>
        /// API server route URL for the password of a user specified by user name.
        /// </summary>
        public const string UserPasswordUrl = "users/{userName}/password";

        /// <summary>
        /// API server route URL for the password of a user specified by user name.
        /// </summary>
        public const string UserPasswordSyncUrl = "users/{userName}/password/sync";

        /// <summary>
        /// API client URL for application context related to a user specified by identifier.
        /// </summary>
        public const string UserContext = "users/{0}/context";

        /// <summary>
        /// API server route URL for application context related to a user specified by identifier.
        /// </summary>
        public const string UserContextUrl = "users/{userId:min(1)}/context";

        /// <summary>
        /// API server route URL for application context related to a user specified by identifier.
        /// </summary>
        public const string UserContextSyncUrl = "users/{userId:min(1)}/context/sync";

        /// <summary>
        /// API client URL for login status of all users.
        /// </summary>
        public const string UsersLoginStatus = "users/login";

        /// <summary>
        /// API server route URL for login status of all users.
        /// </summary>
        public const string UsersLoginStatusUrl = "users/login";

        /// <summary>
        /// API client URL for user metadata.
        /// </summary>
        public const string UserMetadata = "users/metadata";

        /// <summary>
        /// API server route URL for user metadata.
        /// </summary>
        public const string UserMetadataUrl = "users/metadata";

        /// <summary>
        /// API client URL for commands accessible to a user specified by unique identifier.
        /// </summary>
        public const string CurrentUserCommands = "users/current/commands";

        /// <summary>
        /// API server route URL for commands accessible to a user specified by unique identifier.
        /// </summary>
        public const string CurrentUserCommandsUrl = "users/current/commands";
    }
}
