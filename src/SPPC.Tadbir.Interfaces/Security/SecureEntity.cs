using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Security
{
    /// <summary>
    /// Provides strongly-types values for all entities for which one or more permissions are defined.
    /// </summary>
    public sealed class SecureEntity
    {
        private SecureEntity()
        {
        }

        /// <summary>
        /// Represents the entity name for a financial account.
        /// </summary>
        public const string Account = "Account";

        /// <summary>
        /// Represents the entity name for a financial transaction.
        /// </summary>
        public const string Transaction = "Transaction";

        /// <summary>
        /// Represents the entity name for an application user.
        /// </summary>
        public const string User = "User";

        /// <summary>
        /// Represents the entity name for an application role.
        /// </summary>
        public const string Role = "Role";
    }
}
