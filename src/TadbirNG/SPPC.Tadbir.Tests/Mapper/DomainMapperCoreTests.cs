using System;
using NUnit.Framework;
using SPPC.Tadbir.Model.Core;
using SPPC.Tadbir.ViewModel.Core;

namespace SPPC.Tadbir.Mapper.Tests
{
    [TestFixture]
    [Category("DomainMapping")]
    public class DomainMapperCoreTests : DomainMapperTestBase
    {
        [OneTimeSetUp]
        public void FixtureSetup()
        {
            SetUp();
        }

        #region OperationLog Mapping Tests

        [Test]
        public void ContainsMappingFromOperationLogToOperationLogViewModel()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMappingIsDefined<OperationLog, OperationLogViewModel>();
        }

        [Test]
        public void CanMapFromOperationLogToOperationLogViewModel()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMapperCanConvertFromSourceToDestination<OperationLog, OperationLogViewModel>();
        }

        [Test]
        public void ContainsMappingFromOperationLogViewModelToOperationLog()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMappingIsDefined<OperationLogViewModel, OperationLog>();
        }

        [Test]
        public void CanMapFromOperationLogViewModelToOperationLog()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMapperCanConvertFromSourceToDestination<OperationLogViewModel, OperationLog>();
        }

        #endregion // OperationLog Mapping Tests

        #region Document Mapping Tests

        [Test]
        public void ContainsMappingFromDocumentToDocumentViewModel()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMappingIsDefined<Document, DocumentViewModel>();
        }

        [Test]
        public void CanMapFromDocumentToDocumentViewModel()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMapperCanConvertFromSourceToDestination<Document, DocumentViewModel>();
        }

        [Test]
        public void ContainsMappingFromDocumentViewModelToDocument()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMappingIsDefined<DocumentViewModel, Document>();
        }

        [Test]
        public void CanMapFromDocumentViewModelToDocument()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMapperCanConvertFromSourceToDestination<DocumentViewModel, Document>();
        }

        #endregion // Document Mapping Tests

        #region DocumentAction Mapping Tests

        [Test]
        public void ContainsMappingFromDocumentActionToDocumentActionViewModel()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMappingIsDefined<DocumentAction, DocumentActionViewModel>();
        }

        [Test]
        public void CanMapFromDocumentActionToDocumentActionViewModel()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMapperCanConvertFromSourceToDestination<DocumentAction, DocumentActionViewModel>();
        }

        [Test]
        public void ContainsMappingFromDocumentActionViewModelToDocumentAction()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMappingIsDefined<DocumentActionViewModel, DocumentAction>();
        }

        [Test]
        public void CanMapFromDocumentActionViewModelToDocumentAction()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMapperCanConvertFromSourceToDestination<DocumentActionViewModel, DocumentAction>();
        }

        #endregion // DocumentAction Mapping Tests
    }
}
