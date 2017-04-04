using System;
using System.Collections.Generic;
using SPPC.Tadbir.ViewModel.Auth;

namespace SPPC.Tadbir.Security
{
    public sealed class AccountPermission
    {
        private AccountPermission()
        {
            View = new PermissionBriefViewModel()
            {
                EntityName = SecureEntity.Account,
                Flags = (int)AccountPermissions.View
            };
            Create = new PermissionBriefViewModel()
            {
                EntityName = SecureEntity.Account,
                Flags = (int)AccountPermissions.Create
            };
            Edit = new PermissionBriefViewModel()
            {
                EntityName = SecureEntity.Account,
                Flags = (int)AccountPermissions.Edit
            };
            Delete = new PermissionBriefViewModel()
            {
                EntityName = SecureEntity.Account,
                Flags = (int)AccountPermissions.Delete
            };
            All = new PermissionBriefViewModel()
            {
                EntityName = SecureEntity.Account,
                Flags = (int)AccountPermissions.All
            };
        }

        public static AccountPermission Instance
        {
            get { return _instance; }
        }

        public PermissionBriefViewModel View { get; private set; }

        public PermissionBriefViewModel Create { get; private set; }

        public PermissionBriefViewModel Edit { get; private set; }

        public PermissionBriefViewModel Delete { get; private set; }

        public PermissionBriefViewModel All { get; private set; }

        private static AccountPermission _instance = new AccountPermission();
    }
}
