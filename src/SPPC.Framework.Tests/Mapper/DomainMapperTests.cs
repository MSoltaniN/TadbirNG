using System;
using System.Activities.Tracking;
using System.Linq;
using AutoMapper;
using NUnit.Framework;
using SPPC.Framework.Model.Workflow.Tracking;

namespace SPPC.Framework.Mapper.Tests
{
    [TestFixture]
    [Category("FrameworkMapping")]
    public class DomainMapperTests
    {
        [OneTimeSetUp]
        public void FixtureSetup()
        {
            _domainMapper = new DomainMapper();
        }

        #region Type Mapping Tests

       [Test]
        public void ContainsMappingFromWorkflowInstanceRecordToWorkflowInstanceEvent()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMappingIsDefined<WorkflowInstanceRecord, WorkflowInstanceEvent>();
        }

        [Test]
        public void ContainsMappingFromWorkflowInstanceUnhandledExceptionRecordToWorkflowInstanceEvent()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMappingIsDefined<WorkflowInstanceUnhandledExceptionRecord, WorkflowInstanceEvent>();
        }

        [Test]
        public void ContainsMappingFromWorkflowInstanceTerminatedRecordToWorkflowInstanceEvent()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMappingIsDefined<WorkflowInstanceTerminatedRecord, WorkflowInstanceEvent>();
        }

        [Test]
        public void ContainsMappingFromWorkflowInstanceAbortedRecordToWorkflowInstanceEvent()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMappingIsDefined<WorkflowInstanceAbortedRecord, WorkflowInstanceEvent>();
        }

        [Test]
        public void ContainsMappingFromWorkflowInstanceSuspendedRecordToWorkflowInstanceEvent()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMappingIsDefined<WorkflowInstanceSuspendedRecord, WorkflowInstanceEvent>();
        }

        [Test]
        public void ContainsMappingFromActivityStateRecordToActivityInstanceEvent()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMappingIsDefined<ActivityStateRecord, ActivityInstanceEvent>();
        }

        [Test]
        public void ContainsMappingFromActivityScheduledRecordToExtendedActivityEvent()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMappingIsDefined<ActivityScheduledRecord, ExtendedActivityEvent>();
        }

        [Test]
        public void ContainsMappingFromCancelRequestedRecordToExtendedActivityEvent()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMappingIsDefined<CancelRequestedRecord, ExtendedActivityEvent>();
        }

        [Test]
        public void ContainsMappingFromFaultPropagationRecordToExtendedActivityEvent()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMappingIsDefined<FaultPropagationRecord, ExtendedActivityEvent>();
        }

        [Test]
        public void ContainsMappingFromBookmarkResumptionRecordToBookmarkResumptionEvent()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMappingIsDefined<BookmarkResumptionRecord, BookmarkResumptionEvent>();
        }

        [Test]
        public void ContainsMappingFromCustomTrackingRecordToCustomTrackingEvent()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMappingIsDefined<CustomTrackingRecord, CustomTrackingEvent>();
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

        ////private void AssertMapperCanConvertFromSourceToDestination<TSource, TDestination>()
        ////    where TSource : class, new()
        ////    where TDestination : class, new()
        ////{
        ////    var source = new TSource();
        ////    _domainMapper.Map<TDestination>(source);
        ////}

        private IDomainMapper _domainMapper;
    }
}
