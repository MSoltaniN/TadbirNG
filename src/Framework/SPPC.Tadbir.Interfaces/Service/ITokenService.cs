using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Service
{
    /// <summary>
    ///
    /// </summary>
    public interface ITokenService
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        string Generate(ISecurityContext context);

        /// <summary>
        ///
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        bool Validate(string token);

        /// <summary>
        ///
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        ISecurityContext GetSecurityContext(string token);
    }
}
