using System;
using System.Collections.Generic;
using SPPC.Tadbir.ViewModel.Auth;

namespace SPPC.Tadbir.Security
{
    public sealed class UserPermission
    {
        private UserPermission()
        {
            View = new PermissionBriefViewModel()
            {
                EntityName = SecureEntity.User,
                Flags = (int)UserPermissions.View
            };
            Create = new PermissionBriefViewModel()
            {
                EntityName = SecureEntity.User,
                Flags = (int)UserPermissions.Create
            };
            Edit = new PermissionBriefViewModel()
            {
                EntityName = SecureEntity.User,
                Flags = (int)UserPermissions.Edit
            };
            All = new PermissionBriefViewModel()
            {
                EntityName = SecureEntity.User,
                Flags = (int)UserPermissions.All
            };
        }

        public static UserPermission Instance
        {
            get { return _instance; }
        }

        public PermissionBriefViewModel View { get; private set; }

        public PermissionBriefViewModel Create { get; private set; }

        public PermissionBriefViewModel Edit { get; private set; }

        public PermissionBriefViewModel All { get; private set; }

        private static UserPermission _instance = new UserPermission();
    }
}
