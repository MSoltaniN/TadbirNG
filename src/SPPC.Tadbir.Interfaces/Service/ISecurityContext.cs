using System;
using System.Collections.Generic;
using SPPC.Tadbir.ViewModel.Auth;

namespace SPPC.Tadbir.Service
{
    /// <summary>
    /// Defines members required for authorizing actions of current application user.
    /// </summary>
    public interface ISecurityContext
    {
        /// <summary>
        /// Gets or sets the context for current application user.
        /// </summary>
        UserContextViewModel User { get; }

        /// <summary>
        /// Returns a value that indicates if current user can access data for a branch specified by identifier.
        /// </summary>
        /// <param name="branchId">Unique identifier of an existing corporate branch</param>
        /// <returns>True if current user has access to specified branch; otherwise, returns false.</returns>
        bool CanAccessBranch(int branchId);

        /// <summary>
        /// Returns a value that indicates if the role specified by identifier is assigned to this user.
        /// </summary>
        /// <param name="roleId">Unique identifier of an existing organization role</param>
        /// <returns>True if current user is in specified role; otherwise, returns false.</returns>
        bool IsInRole(int roleId);

        /// <summary>
        /// Returns a value that indicates if permission to perform one or more operations is granted to
        /// current application user.
        /// </summary>
        /// <param name="permissions">Variable array of one or more permission to check</param>
        /// <returns>True if current user has all specified permissions. If at least one of the specified permissions
        /// is denied from current application user, returns false.</returns>
        bool HasPermissions(params PermissionBriefViewModel[] permissions);
    }
}
