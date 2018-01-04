using System;
using SPPC.Framework.Persistence;
using SPPC.Workflow.Unity;
using Unity;

namespace SPPC.Tadbir.WorkflowService
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public sealed class UnityConfig
    {
        private UnityConfig()
        {
        }

        #region Unity Container

        private static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
        {
            UnityContainer temp = null;
            UnityContainer container = null;
            try
            {
                temp = new UnityContainer();
                RegisterTypes(temp);
                ////var nhibernate = temp.Resolve<IORMapper>("WF");
                ////nhibernate.Initialize();
                container = temp;
                temp = null;
            }
            finally
            {
                if (temp != null)
                {
                    temp.Dispose();
                }
            }

            return container;
        });

        /// <summary>
        /// Gets the configured Unity container.
        /// </summary>
        public static IUnityContainer GetConfiguredContainer()
        {
            return container.Value;
        }

        #endregion

        /// <summary>Registers the type mappings with the Unity container.</summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>There is no need to register concrete types such as controllers or API controllers (unless you want to 
        /// change the defaults), as Unity allows resolving a concrete type even if it was not previously registered.</remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            _unityWrapper = new TypeContainer(container);
            _unityWrapper.RegisterAll();
        }

        private static TypeContainer _unityWrapper;
    }
}
