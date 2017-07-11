using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Moq;
using NUnit.Framework;
using SPPC.Framework.Helpers;
using SPPC.Framework.Mapper;
using SPPC.Framework.Service.Security;
using SPPC.Tadbir.Configuration;
using SPPC.Tadbir.Model.Auth;
using SPPC.Tadbir.Model.Finance;
using SPPC.Tadbir.Model.Workflow;
using SPPC.Tadbir.ViewModel.Auth;
using SPPC.Tadbir.ViewModel.Finance;
using SPPC.Tadbir.ViewModel.Settings;
using SPPC.Tadbir.ViewModel.Workflow;

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
            _mockCrypto.Setup(crypto => crypto.CreateHash(It.IsAny<string>()))
                .Returns("1234567890abcdef");
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

        [Test]
        public void ContainsMappingFromRoleToRoleViewModel()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMappingIsDefined<Role, RoleViewModel>();
        }

        [Test]
        public void CanMapFromRoleToRoleViewModel()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMapperCanConvertFromSourceToDestination<Role, RoleViewModel>();
        }

        [Test]
        public void ContainsMappingFromRoleViewModelToRole()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMappingIsDefined<RoleViewModel, Role>();
        }

        [Test]
        public void CanMapFromRoleViewModelToRole()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMapperCanConvertFromSourceToDestination<RoleViewModel, Role>();
        }

        [Test]
        public void ContainsMappingFromPermissionToPermissionViewModel()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMappingIsDefined<Permission, PermissionViewModel>();
        }

        [Test]
        public void CanMapFromPermissionToPermissionViewModel()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMapperCanConvertFromSourceToDestination<Permission, PermissionViewModel>();
        }

        [Test]
        public void ContainsMappingFromPermissionViewModelToPermission()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMappingIsDefined<PermissionViewModel, Permission>();
        }

        [Test]
        public void CanMapFromPermissionViewModelToPermission()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMapperCanConvertFromSourceToDestination<PermissionViewModel, Permission>();
        }

        [Test]
        public void ContainsMappingFromUserToUserContextViewModel()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMappingIsDefined<User, UserContextViewModel>();
        }

        [Test]
        public void CanMapFromUserToUserContextViewModel()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMapperCanConvertFromSourceToDestination<User, UserContextViewModel>();
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

        #region Workflow Type Mapping Tests

        [Test]
        public void ContainsMappingFromWorkItemToWorkItemViewModel()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMappingIsDefined<WorkItem, WorkItemViewModel>();
        }

        [Test]
        public void CanMapFromWorkItemToWorkItemViewModel()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMapperCanConvertFromSourceToDestination<WorkItem, WorkItemViewModel>();
        }

        [Test]
        public void ContainsMappingFromWorkItemToInboxItemViewModel()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMappingIsDefined<WorkItem, InboxItemViewModel>();
        }

        [Test]
        public void CanMapFromWorkItemToInboxItemViewModel()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMapperCanConvertFromSourceToDestination<WorkItem, InboxItemViewModel>();
        }

        [Test]
        public void ContainsMappingFromWorkItemViewModelToWorkItem()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMappingIsDefined<WorkItemViewModel, WorkItem>();
        }

        [Test]
        public void CanMapFromWorkItemViewModelToWorkItem()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMapperCanConvertFromSourceToDestination<WorkItemViewModel, WorkItem>();
        }

        [Test]
        public void ContainsMappingFromWorkItemDocumentViewModelToWorkItemDocument()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMappingIsDefined<WorkItemDocumentViewModel, WorkItemDocument>();
        }

        [Test]
        public void CanMapFromWorkItemDocumentViewModelToWorkItemDocument()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMapperCanConvertFromSourceToDestination<WorkItemDocumentViewModel, WorkItemDocument>();
        }

        [Test]
        public void ContainsMappingFromWorkItemViewModelToWorkItemHistory()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMappingIsDefined<WorkItemViewModel, WorkItemHistory>();
        }

        [Test]
        public void CanMapFromWorkItemViewModelToWorkItemHistory()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMapperCanConvertFromSourceToDestination<WorkItemViewModel, WorkItemHistory>();
        }

        [Test]
        public void ContainsMappingFromWorkItemHistoryToHistoryItemViewModel()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMappingIsDefined<WorkItemHistory, HistoryItemViewModel>();
        }

        [Test]
        public void CanMapFromWorkItemHistoryToHistoryItemViewModel()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMapperCanConvertFromSourceToDestination<WorkItemHistory, HistoryItemViewModel>();
        }

        [Test]
        public void ContainsMappingFromWorkItemHistoryToOutboxItemViewModel()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMappingIsDefined<WorkItemHistory, OutboxItemViewModel>();
        }

        [Test]
        public void CanMapFromWorkItemHistoryToOutboxItemViewModel()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMapperCanConvertFromSourceToDestination<WorkItemHistory, OutboxItemViewModel>();
        }

        [Test]
        public void ContainsMappingFromStringToAnyDictionaryToWorkflowInstanceViewModel()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMappingIsDefined<Dictionary<string, object>, WorkflowInstanceViewModel>();
        }

        [Test]
        public void CanMapFromStringToAnyDictionaryToWorkflowInstanceViewModel()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMapperCanConvertFromSourceToDestination<Dictionary<string, object>, WorkflowInstanceViewModel>();
        }

        #endregion

        #region Settings Type Mapping Tests

        [Test]
        public void ContainsMappingFromWorkflowSettingsElementToWorkflowSettingsViewModel()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMappingIsDefined<WorkflowSettingsElement, WorkflowSettingsViewModel>();
        }

        [Test]
        public void CanMapFromWorkflowSettingsElementToWorkflowSettingsViewModel()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMapperCanConvertFromSourceToDestination<WorkflowSettingsElement, WorkflowSettingsViewModel>();
        }

        [Test]
        public void ContainsMappingFromWorkflowElementToWorkflowViewModel()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMappingIsDefined<WorkflowElement, WorkflowViewModel>();
        }

        [Test]
        public void CanMapFromWorkflowElementToWorkflowViewModel()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMapperCanConvertFromSourceToDestination<WorkflowElement, WorkflowViewModel>();
        }

        [Test]
        public void ContainsMappingFromWorkflowEditionElementToWorkflowEditionViewModel()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMappingIsDefined<WorkflowEditionElement, WorkflowEditionViewModel>();
        }

        [Test]
        public void CanMapFromWorkflowEditionElementToWorkflowEditionViewModel()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMapperCanConvertFromSourceToDestination<WorkflowEditionElement, WorkflowEditionViewModel>();
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
