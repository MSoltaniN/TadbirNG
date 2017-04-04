using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Security
{
    public sealed class Permissions
    {
        private Permissions()
        {
        }

        public static AccountPermission Account = AccountPermission.Instance;
        public static TransactionPermission Transaction = TransactionPermission.Instance;
        public static UserPermission User = UserPermission.Instance;
        public static RolePermission Role = RolePermission.Instance;
    }
}
