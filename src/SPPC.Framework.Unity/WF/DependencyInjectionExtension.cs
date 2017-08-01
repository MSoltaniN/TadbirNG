using System;
using System.Collections.Generic;
using Microsoft.Practices.Unity;
using BabakSoft.Platform.Common;

namespace SPPC.Framework.Unity.WF
{
    /// <summary>
    /// Provides a simple wrapper class for the dependency injection framework currently used in a client application.
    /// </summary>
    /// <remarks>
    /// This class is designed to be used for enabling dependency injection in Workflow Foundation (WF) activities.
    /// Current implementation depends on Unity container for dependency resolution.
    /// </remarks>
    public sealed class DependencyInjectionExtension
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DependencyInjectionExtension"/> class.
        /// </summary>
        /// <param name="container">Type container to be used for dependency resolution</param>
        public DependencyInjectionExtension(object container)
        {
            _container = GetUnityContainer(container);
        }

        /// <summary>
        /// Creates and returns a dependency having a specific type.
        /// </summary>
        /// <typeparam name="T">Type of dependency that is required</typeparam>
        /// <returns>Dependency that is required</returns>
        public T GetDependency<T>(string name = null)
        {
            return _container.Resolve<T>(name);
        }

        private static IUnityContainer GetUnityContainer(object container)
        {
            Verify.ArgumentNotNull(container, "container");
            var unityContainer = container as IUnityContainer;
            if (unityContainer == null)
            {
                throw ExceptionBuilder.NewInvalidOperationException(
                    "Constructor argument 'container' must be set to a configured IUnityContainer instance.");
            }

            return unityContainer;
        }

        private readonly IUnityContainer _container;
    }
}
