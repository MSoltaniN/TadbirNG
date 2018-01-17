using System;
using SPPC.Tadbir.ViewModel.Auth;

namespace SPPC.Tadbir.Security
{
    /// <summary>
    /// Exposes all permissions currently applicable to managing a project. This is a singleton class.
    /// </summary>
    public sealed class ProjectPermission
    {
        private ProjectPermission()
        {
            View = new PermissionBriefViewModel()
            {
                EntityName = SecureEntity.Project,
                Flags = (int)ProjectPermissions.View
            };
            Create = new PermissionBriefViewModel()
            {
                EntityName = SecureEntity.Project,
                Flags = (int)ProjectPermissions.Create
            };
            Edit = new PermissionBriefViewModel()
            {
                EntityName = SecureEntity.Project,
                Flags = (int)ProjectPermissions.Edit
            };
            Delete = new PermissionBriefViewModel()
            {
                EntityName = SecureEntity.Project,
                Flags = (int)ProjectPermissions.Delete
            };
            All = new PermissionBriefViewModel()
            {
                EntityName = SecureEntity.Project,
                Flags = (int)ProjectPermissions.All
            };
        }

        /// <summary>
        /// Gets the one and only instance of the <see cref="ProjectPermission"/> class.
        /// </summary>
        public static ProjectPermission Instance
        {
            get { return _instance; }
        }

        /// <summary>
        /// Gets an object that indicates permission to view project list or details of a project.
        /// </summary>
        public PermissionBriefViewModel View { get; private set; }

        /// <summary>
        /// Gets an object that indicates permission to create a new project.
        /// </summary>
        public PermissionBriefViewModel Create { get; private set; }

        /// <summary>
        /// Gets an object that indicates permission to edit an existing project.
        /// </summary>
        public PermissionBriefViewModel Edit { get; private set; }

        /// <summary>
        /// Gets an object that indicates permission to delete an existing project.
        /// </summary>
        public PermissionBriefViewModel Delete { get; private set; }

        /// <summary>
        /// Gets an object that indicates all permissions for managing a project.
        /// </summary>
        public PermissionBriefViewModel All { get; private set; }

        private static ProjectPermission _instance = new ProjectPermission();
    }
}
