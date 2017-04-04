using System;
using System.Collections.Generic;
using SPPC.Tadbir.ViewModel.Auth;

namespace SPPC.Tadbir.Security
{
    public sealed class TransactionPermission
    {
        private TransactionPermission()
        {
            View = new PermissionBriefViewModel()
            {
                EntityName = SecureEntity.Transaction,
                Flags = (int)TransactionPermissions.View
            };

            Create = new PermissionBriefViewModel()
            {
                EntityName = SecureEntity.Transaction,
                Flags = (int)TransactionPermissions.Create
            };

            Edit = new PermissionBriefViewModel()
            {
                EntityName = SecureEntity.Transaction,
                Flags = (int)TransactionPermissions.Edit
            };

            Delete = new PermissionBriefViewModel()
            {
                EntityName = SecureEntity.Transaction,
                Flags = (int)TransactionPermissions.Delete
            };

            All = new PermissionBriefViewModel()
            {
                EntityName = SecureEntity.Transaction,
                Flags = (int)TransactionPermissions.All
            };
        }

        public static TransactionPermission Instance
        {
            get { return _instance; }
        }

        public PermissionBriefViewModel View { get; private set; }

        public PermissionBriefViewModel Create { get; private set; }

        public PermissionBriefViewModel Edit { get; private set; }

        public PermissionBriefViewModel Delete { get; private set; }

        public PermissionBriefViewModel All { get; private set; }

        private static TransactionPermission _instance = new TransactionPermission();
    }
}
