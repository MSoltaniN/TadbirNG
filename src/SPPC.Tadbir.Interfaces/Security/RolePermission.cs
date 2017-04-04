using System;
using System.Collections.Generic;
using SPPC.Tadbir.ViewModel.Auth;

namespace SPPC.Tadbir.Security
{
    public sealed class RolePermission
    {
        private RolePermission()
        {
            View = new PermissionBriefViewModel()
            {
                EntityName = SecureEntity.Role,
                Flags = (int)RolePermissions.View
            };

            Create = new PermissionBriefViewModel()
            {
                EntityName = SecureEntity.Role,
                Flags = (int)RolePermissions.Create
            };

            Edit = new PermissionBriefViewModel()
            {
                EntityName = SecureEntity.Role,
                Flags = (int)RolePermissions.Edit
            };

            Delete = new PermissionBriefViewModel()
            {
                EntityName = SecureEntity.Role,
                Flags = (int)RolePermissions.Delete
            };

            AssignUsers = new PermissionBriefViewModel()
            {
                EntityName = SecureEntity.Role,
                Flags = (int)RolePermissions.AssignUsers
            };

            AssignBranches = new PermissionBriefViewModel()
            {
                EntityName = SecureEntity.Role,
                Flags = (int)RolePermissions.AssignBranches
            };

            All = new PermissionBriefViewModel()
            {
                EntityName = SecureEntity.Role,
                Flags = (int)RolePermissions.All
            };
        }

        public static RolePermission Instance
        {
            get { return _instance; }
        }

        public PermissionBriefViewModel View { get; private set; }

        public PermissionBriefViewModel Create { get; private set; }

        public PermissionBriefViewModel Edit { get; private set; }

        public PermissionBriefViewModel Delete { get; private set; }

        public PermissionBriefViewModel AssignUsers { get; private set; }

        public PermissionBriefViewModel AssignBranches { get; private set; }

        public PermissionBriefViewModel All { get; private set; }

        private static RolePermission _instance = new RolePermission();
    }
}
