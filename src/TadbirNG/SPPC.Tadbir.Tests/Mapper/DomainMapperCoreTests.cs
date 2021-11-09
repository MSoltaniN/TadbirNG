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
    }
}
