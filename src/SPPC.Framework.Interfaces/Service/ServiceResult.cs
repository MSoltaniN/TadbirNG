using System;
using System.Collections.Generic;

namespace SPPC.Framework.Service
{
    /// <summary>
    /// Defines possible results from a service operation.
    /// </summary>
    public enum ServiceResult
    {
        /// <summary>
        /// Indicates that the service operation was successful
        /// </summary>
        Success = 0,

        /// <summary>
        /// Indicates that the service operation did not succeed because of a validation error
        /// </summary>
        ValidationFailed = 1,

        /// <summary>
        /// Indicates that the service operation did not succeed because of a server-side error
        /// </summary>
        ServerError = 2
    }
}
