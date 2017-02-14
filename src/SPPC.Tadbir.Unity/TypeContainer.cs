using System;
using System.Collections.Generic;
using System.Web;
using Microsoft.Practices.Unity;
using SPPC.Tadbir.Mapper;
using SwForAll.Platform.Persistence;
using SwForAll.Platform.Persistence.NHibernate;

namespace SPPC.Tadbir.Unity
{
    public class TypeContainer : IDisposable
    {
        public TypeContainer(IUnityContainer container = null)
        {
            _container = container ?? new UnityContainer();
        }

        public IUnityContainer Unity
        {
            get { return _container; }
        }

        public T Get<T>()
        {
            return Unity.Resolve<T>();
        }

        public void RegisterAll()
        {
            RegisterCrossCuttingTypes();
            RegisterPersistenceTypes();
            RegisterServiceTypes();
            RegisterWebDependentTypes();
        }

        public void RegisterCrossCuttingTypes()
        {
            // ======== Dependencies shared by several layers =======
            _container.RegisterType<IDomainMapper, DomainMapper>();
        }

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

        public void RegisterServiceTypes()
        {
        }

        public void RegisterWebDependentTypes()
        {
            // Register all types that are strictly dependent to the existence of a current HttpContext...
            _container.RegisterType<HttpContextBase>(new InjectionFactory(_ =>
                new HttpContextWrapper(HttpContext.Current)));
            _container.RegisterType<HttpRequestBase>(new InjectionFactory(_ =>
                new HttpRequestWrapper(HttpContext.Current.Request)));
            _container.RegisterType<HttpResponseBase>(new InjectionFactory(_ =>
                new HttpResponseWrapper(HttpContext.Current.Response)));
        }

        #region IDisposable Support

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) below.
            Dispose(true);
            GC.SuppressFinalize(this);
        }

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
