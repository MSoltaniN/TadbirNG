using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Security
{
    public sealed class SecureEntity
    {
        private SecureEntity()
        {
        }

        public const string Account = "Account";
        public const string Transaction = "Transaction";
        public const string User = "User";
        public const string Role = "Role";
    }
}
