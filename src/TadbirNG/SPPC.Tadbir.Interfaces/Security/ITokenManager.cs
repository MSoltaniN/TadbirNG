using System;

namespace SPPC.Tadbir.Security
{
    /// <summary>
    ///
    /// </summary>
    public interface ITokenManager
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
