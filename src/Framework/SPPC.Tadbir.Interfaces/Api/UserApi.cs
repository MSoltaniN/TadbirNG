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
        /// API client URL for a user specified by unique identifier.
        /// </summary>
        public const string User = "users/{0}";

        /// <summary>
        /// API server route URL for a user specified by unique identifier.
        /// </summary>
        public const string UserUrl = "users/{userId:min(1)}";

        /// <summary>
        /// API client URL for a user's last login statistics.
        /// </summary>
        public const string UserLastLogin = "users/{0}/login";

        /// <summary>
        /// API server route URL for a user's last login statistics.
        /// </summary>
        public const string UserLastLoginUrl = "users/{userId:min(1)}/login";

        /// <summary>
        /// API client URL for a user specified by user name.
        /// </summary>
        public const string UserByName = "users/name/{0}";

        /// <summary>
        /// API server route URL for a user specified by user name.
        /// </summary>
        public const string UserByNameUrl = "users/name/{userName}";

        /// <summary>
        /// API client URL for the password of a user specified by user name.
        /// </summary>
        public const string UserPassword = "users/{0}/password";

        /// <summary>
        /// API server route URL for the password of a user specified by user name.
        /// </summary>
        public const string UserPasswordUrl = "users/{userName}/password";

        /// <summary>
        /// API client URL for all roles assigned to a user specified by identifier.
        /// </summary>
        public const string UserRoles = "users/{0}/roles";

        /// <summary>
        /// API server route URL for all roles assigned to a user specified by identifier.
        /// </summary>
        public const string UserRolesUrl = "users/{userId:min(1)}/roles";

        /// <summary>
        /// API client URL for application context related to a user specified by identifier.
        /// </summary>
        public const string UserContext = "users/{0}/context";

        /// <summary>
        /// API server route URL for application context related to a user specified by identifier.
        /// </summary>
        public const string UserContextUrl = "users/{userId:min(1)}/context";

        /// <summary>
        /// API client URL for login status of all users.
        /// </summary>
        public const string UserLoginStatus = "users/login";

        /// <summary>
        /// API server route URL for login status of all users.
        /// </summary>
        public const string UserLoginStatusUrl = "users/login";

        /// <summary>
        /// API client URL for status of login to a company for all users.
        /// </summary>
        public const string UserCompanyLoginStatus = "users/login/company";

        /// <summary>
        /// API server route URL for status of login to a company for all users.
        /// </summary>
        public const string UserCompanyLoginStatusUrl = "users/login/company";

        /// <summary>
        /// API client URL for commands accessible to current application user.
        /// </summary>
        public const string CurrentUserCommands = "users/current/commands";

        /// <summary>
        /// API server route URL for commands accessible to current application user.
        /// </summary>
        public const string CurrentUserCommandsUrl = "users/current/commands";

        /// <summary>
        /// API client URL for application environment set by current user.
        /// </summary>
        public const string CurrentUserEnvironment = "users/current/env";

        /// <summary>
        /// API server route URL for application environment set by current user.
        /// </summary>
        public const string CurrentUserEnvironmentUrl = "users/current/env";

        /// <summary>
        /// API client URL for commands accessible to all users.
        /// </summary>
        public const string UserDefaultCommands = "users/default/commands";

        /// <summary>
        /// API server route URL for commands accessible to all users.
        /// </summary>
        public const string UserDefaultCommandsUrl = "users/default/commands";
    }
}
