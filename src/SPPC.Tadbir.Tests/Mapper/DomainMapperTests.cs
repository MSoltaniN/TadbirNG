using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using NUnit.Framework;
using SPPC.Framework.Mapper;
using SPPC.Tadbir.Model.Finance;
using SPPC.Tadbir.ViewModel.Finance;

namespace SPPC.Tadbir.Mapper.Tests
{
    [TestFixture]
    [Category("DomainMapping")]
    public class DomainMapperTests
    {
        [OneTimeSetUp]
        public void FixtureSetup()
        {
            _domainMapper = new DomainMapper();
        }

        #region Finance Type Mapping Tests

        [Test]
        public void ContainsMappingFromAccountToAccountViewModel()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMappingIsDefined<Account, AccountViewModel>();
        }

        [Test]
        public void CanMapFromAccountToAccountViewModel()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMapperCanConvertFromSourceToDestination<Account, AccountViewModel>();
        }

        [Test]
        public void ContainsMappingFromAccountViewModelToAccount()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMappingIsDefined<AccountViewModel, Account>();
        }

        [Test]
        public void CanMapFromAccountViewModelToAccount()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMapperCanConvertFromSourceToDestination<AccountViewModel, Account>();
        }

        [Test]
        public void ContainsMappingFromTransactionToTransactionViewModel()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMappingIsDefined<Transaction, TransactionViewModel>();
        }

        [Test]
        public void CanMapFromTransactionToTransactionViewModel()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMapperCanConvertFromSourceToDestination<Transaction, TransactionViewModel>();
        }

        [Test]
        public void ContainsMappingFromTransactionViewModelToTransaction()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMappingIsDefined<TransactionViewModel, Transaction>();
        }

        [Test]
        public void CanMapFromTransactionViewModelToTransaction()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMapperCanConvertFromSourceToDestination<TransactionViewModel, Transaction>();
        }

        [Test]
        public void ContainsMappingFromTransactionLineToTransactionLineViewModel()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMappingIsDefined<TransactionLine, TransactionLineViewModel>();
        }

        [Test]
        public void CanMapFromTransactionLineToTransactionLineViewModel()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMapperCanConvertFromSourceToDestination<TransactionLine, TransactionLineViewModel>();
        }

        [Test]
        public void ContainsMappingFromTransactionToTransactionFullViewModel()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMappingIsDefined<Transaction, TransactionFullViewModel>();
        }

        [Test]
        public void CanMapFromTransactionToTransactionFullViewModel()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMapperCanConvertFromSourceToDestination<Transaction, TransactionFullViewModel>();
        }

        #endregion

        private void AssertMappingIsDefined<TSource, TDestination>()
        {
            var configurtion = _domainMapper.Configuration as MapperConfiguration;
            var mapping = configurtion.GetAllTypeMaps()
                .Where(map => map.SourceType == typeof(TSource)
                    && map.DestinationType == typeof(TDestination))
                .FirstOrDefault();

            // Assert
            Assert.That(mapping, Is.Not.Null);
        }

        private void AssertMapperCanConvertFromSourceToDestination<TSource, TDestination>()
            where TSource : class, new()
            where TDestination : class, new()
        {
            var source = new TSource();
            _domainMapper.Map<TDestination>(source);
        }

        private IDomainMapper _domainMapper;
    }
}
