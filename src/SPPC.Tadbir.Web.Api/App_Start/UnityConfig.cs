using System;
using SPPC.Tadbir.NHibernate;
using SPPC.Tadbir.Repository;
using SPPC.Tadbir.Service;
using SPPC.Tadbir.Unity;
using SPPC.Tadbir.Web.Api.Controllers;
using SPPC.Tadbir.Workflow;
using Unity;
using Unity.Configuration;
using Unity.Injection;

namespace SPPC.Tadbir.Web.Api.AppStart
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
            // NOTE: The following Unity configurations cannot be done inside the main Unity configuration assembly
            // (SPPC.Tadbir.Unity) because it requires adding a reference to this assembly (SPPC.Tadbir.Web.Api) and
            // this assembly already depends on Unity configuration assembly (circular dependencies are not allowed).
            container.RegisterType<TransactionsController>(
                new InjectionConstructor(
                    new ResolvedParameter<ITransactionRepository>(),
                    new ResolvedParameter<IDocumentWorkflow>(),
                    new ResolvedParameter<ISecurityContextManager>("API")));
            container.RegisterType<RequisitionsController>(
                new InjectionConstructor(
                    new ResolvedParameter<IRequisitionRepository>(),
                    new ResolvedParameter<IDocumentWorkflow>(),
                    new ResolvedParameter<ISecurityContextManager>("API")));

            _unityWrapper = new TypeContainer(container);
            _unityWrapper.RegisterAll();
        }

        private static TypeContainer _unityWrapper;
    }
}
