using System.Linq;
using SPPC.Framework.Common;
using SPPC.Tadbir.ViewModel.Auth;

namespace SPPC.Tadbir.Security
{
    /// <summary>
    /// Provides members required for authorizing actions of current application user.
    /// </summary>
    public class SecurityContext : ISecurityContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SecurityContext"/> class.
        /// </summary>
        public SecurityContext()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SecurityContext"/> class.
        /// </summary>
        /// <param name="userContext">An object containing context information for current application user</param>
        public SecurityContext(UserContextViewModel userContext)
        {
            Verify.ArgumentNotNull(userContext, "userContext");
            User = userContext;
        }

        /// <summary>
        /// Gets or sets the context for current application user.
        /// </summary>
        public UserContextViewModel User { get; set; }

        /// <summary>
        /// Returns a value that indicates if the role specified by identifier is assigned to this user.
        /// </summary>
        /// <param name="roleId">Unique identifier of an existing organization role</param>
        /// <returns>True if current user is in specified role; otherwise, returns false.</returns>
        public bool IsInRole(int roleId)
        {
            return (User.Roles.Contains(roleId));
        }

        /// <summary>
        /// Returns a value that indicates if permission to perform one or more operations is granted to
        /// current application user.
        /// </summary>
        /// <param name="permissions">Variable array of one or more permission to check</param>
        /// <returns>True if current user has all specified permissions. If at least one of the specified permissions
        /// is denied from current application user, returns false.</returns>
        public bool HasPermissions(params PermissionBriefViewModel[] permissions)
        {
            if (permissions.Length == 0)
            {
                return false;
            }

            bool hasPermissions = true;
            foreach (var permission in permissions)
            {
                var enabled = User.Permissions
                    .Where(perm => perm.EntityName == permission.EntityName)
                    .FirstOrDefault();
                if (hasPermissions && enabled != null)
                {
                    hasPermissions = hasPermissions && HasFlags(enabled.Flags, permission.Flags);
                }
                else
                {
                    hasPermissions = false;
                    break;
                }
            }

            return hasPermissions;
        }

        private static bool HasFlags(int value, int flags)
        {
            return ((value & flags) == flags);
        }
    }
}
