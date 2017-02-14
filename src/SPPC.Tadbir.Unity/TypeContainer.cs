using System;
using System.Collections.Generic;
using System.Web;
using Microsoft.Practices.Unity;
using SPPC.Framework.Mapper;
using SPPC.Tadbir.Mapper;
using SwForAll.Platform.Persistence;
using SwForAll.Platform.Persistence.NHibernate;

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
            var nhibernate = new WebHibernateConfigurator();
            _container.RegisterInstance<IORMapper>(nhibernate);
            _container.RegisterInstance<IHibernateWrapper>(nhibernate);
            _container.RegisterType<IUnitOfWork, UnitOfWork>();

            // TODO: Associate each new repository class with its contract, similar to the following code...
            // _container.RegisterType<IRepository, Repository>();
        }

        /// <summary>
        /// Performs type registrations for service types.
        /// </summary>
        public void RegisterServiceTypes()
        {
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
