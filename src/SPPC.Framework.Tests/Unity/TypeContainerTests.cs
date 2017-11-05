using System;
using System.Linq;
using BabakSoft.Platform.Persistence;
using BabakSoft.Platform.Persistence.NHibernate;
using NUnit.Framework;
using SPPC.Framework.Mapper;
using SPPC.Framework.NHibernate;
using Unity.Container.Registration;
using Unity.Lifetime;
using Unity.Registration;

namespace SPPC.Framework.Unity.Tests
{
    [TestFixture]
    [Category("FrameworkUnity")]
    public class TypeContainerTests : IDisposable
    {
        [OneTimeSetUp]
        public void FixtureSetup()
        {
            _container = new TypeContainer();
            _container.RegisterAll();
        }

        [OneTimeTearDown]
        public void FixtureTearDown()
        {
            _container.Dispose();
        }

        #region Cross-cutting Type Registrations

        [Test]
        public void ContainsDomainMapperRegistration()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertIsRegisteredWithConcreteType<IDomainMapper>();
        }

        #endregion

        #region Persistence Type Registrations

        [Test]
        public void ContainsORMapperRegistrationWithInstance()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertIsRegisteredWithInstance<IORMapper>();
        }

        [Test]
        public void ContainsHibernateWrapperRegistrationWithInstance()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertIsRegisteredWithInstance<IHibernateWrapper>();
        }

        [Test]
        public void ContainsUnitOfWorkRegistration()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertIsRegisteredWithConcreteType<IUnitOfWork>();
        }

        [Test]
        public void ContainsTrackingRepositoryRegistration()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertIsRegisteredWithConcreteType<ITrackingRepository>();
        }

        #endregion

        #region Service Type Registrations
        #endregion

        #region IDisposable Support

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
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

        private IContainerRegistration GetRegistration<TInterface>()
            where TInterface : class
        {
            return _container.Unity.Registrations
                .Where(reg => reg.RegisteredType == typeof(TInterface))
                .FirstOrDefault();
        }

        private void AssertIsRegisteredWithInstance<TInterface>()
            where TInterface : class
        {
            var registration = GetRegistration<TInterface>();

            Assert.That(registration.RegisteredType, Is.EqualTo(registration.MappedToType));
            ////Assert.That(registration.LifetimeManagerType, Is.EqualTo(typeof(ContainerControlledLifetimeManager)));
        }

        private void AssertIsRegisteredWithConcreteType<TInterface>()
            where TInterface : class
        {
            var registration = _container.Unity.Registrations
                .Where(reg => reg.RegisteredType == typeof(TInterface))
                .FirstOrDefault();

            Assert.That(registration, Is.Not.Null);
            Assert.That(registration.RegisteredType, Is.Not.EqualTo(registration.MappedToType));
        }

        private TypeContainer _container;
        private bool _disposed = false;
    }
}
