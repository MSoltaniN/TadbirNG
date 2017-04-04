using System;
using System.Collections.Generic;
using SPPC.Tadbir.ViewModel.Auth;

namespace SPPC.Tadbir.Service
{
    /// <summary>
    /// Defines operations for managing required contextual information related to current application user.
    /// </summary>
    public interface ISecurityContextManager
    {
        /// <summary>
        /// Gets current security context, usually set after a successful login.
        /// </summary>
        ISecurityContext CurrentContext { get; }

        /// <summary>
        /// Saves security context of current application user in a suitable place, so that it can be
        /// easily retrieved on demand for performing authorization.
        /// </summary>
        /// <param name="userContext">Object containing context information related to current application user</param>
        void SetUserContext(UserContextViewModel userContext);
    }
}
