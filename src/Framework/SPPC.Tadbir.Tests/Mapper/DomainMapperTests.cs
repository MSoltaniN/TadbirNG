using System;
using System.Collections.Generic;
using NUnit.Framework;
using SPPC.Tadbir.Model.Core;
using SPPC.Tadbir.Model.Workflow;
using SPPC.Tadbir.ViewModel.Core;
using SPPC.Tadbir.ViewModel.Workflow;

namespace SPPC.Tadbir.Mapper.Tests
{
    [TestFixture]
    [Category("DomainMapping")]
    public class DomainMapperTests : DomainMapperTestBase
    {
        [OneTimeSetUp]
        public void FixtureSetup()
        {
            SetUp();
        }

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
    }
}
