using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Values
{
    /// <summary>
    /// Provides definitions for special-purpose constant values.
    /// </summary>
    public sealed class Constants
    {
        private Constants()
        {
        }

        /// <summary>
        /// Special identifier for System Administrator user (admin) in security system
        /// </summary>
        public const int AdminUserId = 1;

        /// <summary>
        /// Special-purpose name of the system administrator user
        /// </summary>
        public const string AdminUserName = "admin";

        /// <summary>
        /// Dummy text to display in password boxes
        /// </summary>
        public const string DummyPassword = "************";

        /// <summary>
        /// Special identifier for System Administrator role in security system
        /// </summary>
        public const int AdminRoleId = 1;

        /// <summary>
        /// Name of security context cookie used for authorization in application level
        /// </summary>
        public const string ContextCookieName = "TadbirContext";

        /// <summary>
        /// Name of authorization context HTTP header used for authorization in Web service level
        /// </summary>
        public const string ContextHeaderName = "X-Tadbir-AuthTicket";

        /// <summary>
        /// اندازه پیش فرض صفحه در فهرست های اطلاعاتی
        /// </summary>
        public const int DefaultPageSize = 10;
    }
}
