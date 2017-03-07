using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Moq;
using NUnit.Framework;
using SPPC.Framework.Helpers;
using SPPC.Framework.Mapper;
using SPPC.Framework.Service.Security;
using SPPC.Tadbir.Model.Auth;
using SPPC.Tadbir.Model.Finance;
using SPPC.Tadbir.ViewModel.Auth;
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
            _mockCrypto = new Mock<ICryptoService>();
            _mockCrypto.Setup(crypto => crypto.CreateHash(It.IsAny<byte[]>()))
                .Returns(new byte[] { 0x12, 0x34, 0x56, 0x78, 0x90, 0xab, 0xcd, 0xef });
            _domainMapper = new DomainMapper(_mockCrypto.Object);
        }

        #region Security Type Mapping Tests

        [Test]
        public void ContainsMappingFromUserToUserViewModel()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMappingIsDefined<User, UserViewModel>();
        }

        [Test]
        public void CanMapFromUserToUserViewModel()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMapperCanConvertFromSourceToDestination<User, UserViewModel>();
        }

        [Test]
        public void ContainsMappingFromUserViewModelToUser()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMappingIsDefined<UserViewModel, User>();
        }

        [Test]
        public void CanMapFromUserViewModelToUser()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMapperCanConvertFromSourceToDestination<UserViewModel, User>();
        }

        #endregion

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
        public void ContainsMappingFromAccountToAccountFullViewModel()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMappingIsDefined<Account, AccountFullViewModel>();
        }

        [Test]
        public void CanMapFromAccountToAccountFullViewModel()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMapperCanConvertFromSourceToDestination<Account, AccountFullViewModel>();
        }

        [Test]
        public void ContainsMappingFromAccountToKeyValue()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMappingIsDefined<Account, KeyValue>();
        }

        [Test]
        public void CanMapFromAccountToKeyValue()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMapperCanConvertFromSourceToDestination<Account, KeyValue>();
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
        public void ContainsMappingFromTransactionLineToTransactionLineFullViewModel()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMappingIsDefined<TransactionLine, TransactionLineFullViewModel>();
        }

        [Test]
        public void CanMapFromTransactionLineToTransactionLineFullViewModel()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMapperCanConvertFromSourceToDestination<TransactionLine, TransactionLineFullViewModel>();
        }

        [Test]
        public void ContainsMappingFromTransactionLineViewModelToTransactionLine()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMappingIsDefined<TransactionLineViewModel, TransactionLine>();
        }

        [Test]
        public void CanMapFromTransactionLineViewModelToTransactionLine()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMapperCanConvertFromSourceToDestination<TransactionLineViewModel, TransactionLine>();
        }

        [Test]
        public void ContainsMappingFromCurrencyToKeyValue()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMappingIsDefined<Currency, KeyValue>();
        }

        [Test]
        public void CanMapFromCurrencyToKeyValue()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMapperCanConvertFromSourceToDestination<Currency, KeyValue>();
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
        private Mock<ICryptoService> _mockCrypto;
    }
}
