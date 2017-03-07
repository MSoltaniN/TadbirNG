using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.Unity;
using NUnit.Framework;
using SPPC.Framework.Mapper;
using SPPC.Framework.Service;
using SPPC.Tadbir.NHibernate;
using SPPC.Tadbir.Service;
using SwForAll.Platform.Persistence;
using SwForAll.Platform.Persistence.NHibernate;

namespace SPPC.Tadbir.Unity.Tests
{
    [TestFixture]
    [Category("Unity")]
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
        public void ContainsAccountRepositoryRegistration()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertIsRegisteredWithConcreteType<IAccountRepository>();
        }

        [Test]
        public void ContainsTransactionRepositoryRegistration()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertIsRegisteredWithConcreteType<ITransactionRepository>();
        }

        [Test]
        public void ContainsLookupRepositoryRegistration()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertIsRegisteredWithConcreteType<ILookupRepository>();
        }

        [Test]
        public void ContainsSecurityRepositoryRegistration()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertIsRegisteredWithConcreteType<ISecurityRepository>();
        }

        #endregion

        #region Service Type Registrations

        [Test]
        public void ContainsApiClientRegistration()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertIsRegisteredWithConcreteType<IApiClient>();
        }

        [Test]
        public void ContainsAccountServiceRegistration()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertIsRegisteredWithConcreteType<IAccountService>();
        }

        [Test]
        public void ContainsTransactionServiceRegistration()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertIsRegisteredWithConcreteType<ITransactionService>();
        }

        [Test]
        public void ContainsLookupServiceRegistration()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertIsRegisteredWithConcreteType<ILookupService>();
        }

        [Test]
        public void ContainsSecurityServiceRegistration()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertIsRegisteredWithConcreteType<ISecurityService>();
        }

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

        private ContainerRegistration GetRegistration<TInterface>()
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
            Assert.That(registration.LifetimeManagerType, Is.EqualTo(typeof(ContainerControlledLifetimeManager)));
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
