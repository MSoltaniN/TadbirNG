using System;
using NUnit.Framework;
using SPPC.Framework.Helpers;
using SPPC.Tadbir.Model.Metadata;
using SPPC.Tadbir.ViewModel.Metadata;

namespace SPPC.Tadbir.Mapper.Tests
{
    [TestFixture]
    [Category("DomainMapping")]
    public class DomainMapperMetadataTests : DomainMapperTestBase
    {
        [OneTimeSetUp]
        public void FixtureSetup()
        {
            SetUp();
        }

        #region View Mapping Tests

        [Test]
        public void ContainsMappingFromViewToViewViewModel()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMappingIsDefined<View, ViewViewModel>();
        }

        [Test]
        public void CanMapFromViewToViewViewModel()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMapperCanConvertFromSourceToDestination<View, ViewViewModel>();
        }

        [Test]
        public void ContainsMappingFromViewToKeyValue()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMappingIsDefined<View, KeyValue>();
        }

        [Test]
        public void CanMapFromViewToKeyValue()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMapperCanConvertFromSourceToDestination<View, KeyValue>();
        }

        #endregion // View Mapping Tests

        #region Column Mapping Tests

        [Test]
        public void ContainsMappingFromColumnToColumnViewModel()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMappingIsDefined<Column, ColumnViewModel>();
        }

        [Test]
        public void CanMapFromColumnToColumnViewModel()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMapperCanConvertFromSourceToDestination<Column, ColumnViewModel>();
        }

        #endregion // Column Mapping Tests

        #region Command Mapping Tests

        [Test]
        public void ContainsMappingFromCommandToCommandViewModel()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMappingIsDefined<Command, CommandViewModel>();
        }

        [Test]
        public void CanMapFromCommandToCommandViewModel()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMapperCanConvertFromSourceToDestination<Command, CommandViewModel>();
        }

        #endregion // Command Mapping Tests
    }
}
