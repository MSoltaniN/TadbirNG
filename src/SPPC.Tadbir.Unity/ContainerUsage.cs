using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Unity
{
    /// <summary>
    /// Defines different clients that can use the type container.
    /// </summary>
    public enum ContainerUsage
    {
        /// <summary>
        /// Indicates that type container is being used in a Web application.
        /// </summary>
        Application = 0,

        /// <summary>
        /// Indicates that type container is being used in a Web Service.
        /// </summary>
        Service = 1,

        /// <summary>
        /// Indicates that type container is being used in an automated test assembly.
        /// </summary>
        Test = 2
    }
}
