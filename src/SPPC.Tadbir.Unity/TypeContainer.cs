using System;
using System.Web;
using BabakSoft.Platform.Persistence;
using BabakSoft.Platform.Persistence.NHibernate;
using SPPC.Framework.Mapper;
using SPPC.Framework.NHibernate;
using SPPC.Framework.Service;
using SPPC.Framework.Service.Security;
using SPPC.Tadbir.Mapper;
using SPPC.Tadbir.NHibernate;
using SPPC.Tadbir.Repository;
using SPPC.Tadbir.Service;
using SPPC.Tadbir.Values;
using SPPC.Tadbir.Workflow;
using Unity;
using Unity.Injection;

namespace SPPC.Tadbir.Unity
{
    /// <summary>
    /// Provides support for mapping abstract types to concrete implementations in Tadbir using the Microsoft Unity
    /// IoC (Inversion of Control) container.
    /// </summary>
    public class TypeContainer : IDisposable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TypeContainer"/> class that wraps a given Unity container.
        /// </summary>
        /// <param name="container">The Unity container that is wrapped by this instance.</param>
        public TypeContainer(IUnityContainer container = null)
        {
            _container = container ?? new UnityContainer();
        }

        /// <summary>
        /// Gets the Unity container wrapped by this instance.
        /// </summary>
        public IUnityContainer Unity
        {
            get { return _container; }
        }

        /// <summary>
        /// Instantiates and returns a concrete instance of the specified type, using existing type registration.
        /// </summary>
        /// <typeparam name="T">Type of abstract instance to instantiate</typeparam>
        /// <returns>A concrete implementation of the given type</returns>
        public T Get<T>()
        {
            return Unity.Resolve<T>();
        }

        /// <summary>
        /// Performs all type registrations in a single call
        /// </summary>
        public void RegisterAll()
        {
            RegisterCrossCuttingTypes();
            RegisterPersistenceTypes();
            RegisterServiceTypes();
            RegisterWebDependentTypes();
        }

        /// <summary>
        /// Performs all type registrations for the types that are used in multiple logical layers.
        /// </summary>
        public void RegisterCrossCuttingTypes()
        {
            // ======== Dependencies shared by several layers =======
            _container.RegisterType<IDomainMapper, DomainMapper>();
        }

        /// <summary>
        /// Performs all type registrations for the types that are used for database persistence operations.
        /// </summary>
        public void RegisterPersistenceTypes()
        {
            // =========== NHibernate Persistence Layer dependencies ===========
            var apiHibernate = new WebHibernateConfigurator();
            var wfHibernate = new HibernateConfigurator();
            _container.RegisterInstance<IORMapper>(apiHibernate);
            _container.RegisterInstance<IHibernateWrapper>(apiHibernate);
            _container.RegisterInstance<IORMapper>("WF", wfHibernate);
            _container.RegisterInstance<IHibernateWrapper>("WF", wfHibernate);
            _container.RegisterType<IUnitOfWork, UnitOfWork>();
            _container.RegisterType<IUnitOfWork, UnitOfWork>(
                "WF", new InjectionConstructor(new ResolvedParameter<IHibernateWrapper>("WF")));

            _container.RegisterType<IAccountRepository, AccountRepository>();
            _container.RegisterType<ITransactionRepository, TransactionRepository>();
            _container.RegisterType<ITransactionRepository, TransactionRepository>(
                "WF", new InjectionConstructor(
                    new ResolvedParameter<IUnitOfWork>("WF"), new ResolvedParameter<IDomainMapper>()));
            _container.RegisterType<ILookupRepository, LookupRepository>();
            _container.RegisterType<ISecurityRepository, SecurityRepository>();
            _container.RegisterType<IWorkItemRepository, WorkItemRepository>();
            _container.RegisterType<IWorkItemRepository, WorkItemRepository>(
                "WF", new InjectionConstructor(
                    new ResolvedParameter<IUnitOfWork>("WF"), new ResolvedParameter<IDomainMapper>()));

            _container.RegisterType<ISettingsRepository, ConfigSettingsRepository>();
            _container.RegisterType<ITrackingRepository, TrackingRepository>();
            _container.RegisterType<IWorkflowRepository, WorkflowRepository>();
            _container.RegisterType<IRequisitionRepository, RequisitionRepository>();
            _container.RegisterType<IInventoryRepository, InventoryRepository>();
            _container.RegisterType<IMetadataRepository, JsonMetadataRepository>();
        }

        /// <summary>
        /// Performs type registrations for service types.
        /// </summary>
        public void RegisterServiceTypes()
        {
            _container.RegisterType<IApiClient, ApiClient>();
            _container.RegisterType<IAccountService, AccountService>();
            _container.RegisterType<ITransactionService, TransactionService>();
            _container.RegisterType<ILookupService, LookupService>();
            _container.RegisterType<ISecurityService, SecurityService>();
            _container.RegisterType<ICryptoService, CryptoService>();
            _container.RegisterType<ICartableService, CartableService>();
            _container.RegisterType<ISecurityContextManager, SecurityContextManager>();
            _container.RegisterType<ISecurityContextManager, ServiceContextManager>("API");
            _container.RegisterType<ITextEncoder<SecurityContext>, Base64Encoder<SecurityContext>>();
            _container.RegisterType<IDocumentWorkflow, DocumentWorkflow>();
            _container.RegisterType<ITransactionWorkflow, TransactionWorkflow>(WorkflowEdition.StateMachine);
            _container.RegisterType<ITransactionWorkflow, TransactionDecisionWorkflow>(WorkflowEdition.Flowchart);
            _container.RegisterType<ITransactionWorkflow, TransactionTimeoutWorkflow>(WorkflowEdition.Timeout);
            _container.RegisterType<ITransactionWorkflow, TransactionBasicWorkflow>(WorkflowEdition.Sequential);
            _container.RegisterType<ISettingsService, ConfigSettingsService>();
            _container.RegisterType<IWorkflowService, WorkflowService>();
            _container.RegisterType<IWorkflowTracker, WorkflowTracker>();
            _container.RegisterType<IRequisitionService, RequisitionService>();
            _container.RegisterType<IInventoryService, InventoryService>();
        }

        /// <summary>
        /// Performs type registrations for the types that are strictly dependent to the existence of
        /// a current HttpContext.
        /// </summary>
        public void RegisterWebDependentTypes()
        {
            _container.RegisterType<HttpContextBase>(new InjectionFactory(_ =>
                new HttpContextWrapper(HttpContext.Current)));
            _container.RegisterType<HttpRequestBase>(new InjectionFactory(_ =>
                new HttpRequestWrapper(HttpContext.Current.Request)));
            _container.RegisterType<HttpResponseBase>(new InjectionFactory(_ =>
                new HttpResponseWrapper(HttpContext.Current.Response)));
        }

        #region IDisposable Support

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) below.
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Supports correct implementation of the Disposable pattern for this class.
        /// </summary>
        /// <param name="disposing">Indicates if this instance is currently being disposed</param>
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                _container.Dispose();
                _disposed = true;
            }
        }

        #endregion

        private readonly IUnityContainer _container;
        private bool _disposed = false;
    }
}
