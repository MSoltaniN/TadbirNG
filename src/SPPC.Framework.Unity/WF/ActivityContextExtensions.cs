using System;
using System.Activities;
using System.Collections.Generic;
using SwForAll.Platform.Common;

namespace SPPC.Framework.Unity.WF
{
    /// <summary>
    /// Extends the <see cref="ActivityContext"/> class to enable dependency resolution using Unity container.
    /// </summary>
    public static class ActivityContextExtensions
    {
        /// <summary>
        /// Creates and returns a dependency having a specific type, using current activity context.
        /// </summary>
        /// <typeparam name="T">Type of dependency that is required</typeparam>
        /// <param name="context">Current activity context</param>
        /// <returns>Dependency that is required</returns>
        public static T GetDependency<T>(this ActivityContext context)
        {
            Verify.ArgumentNotNull(context, "context");
            var extension = context.GetExtension<DependencyInjectionExtension>();
            return extension.GetDependency<T>();
        }
    }
}
