using System;
using NUnit.Framework;
using SPPC.Framework.Helpers;
using SPPC.Tadbir.Model.Corporate;
using SPPC.Tadbir.ViewModel;
using SPPC.Tadbir.ViewModel.Corporate;

namespace SPPC.Tadbir.Mapper.Tests
{
    [TestFixture]
    [Category("DomainMapping")]
    public class DomainMapperCorporateTests : DomainMapperTestBase
    {
        [OneTimeSetUp]
        public void FixtureSetup()
        {
            SetUp();
        }

        #region Branch Mapping Tests

        [Test]
        public void ContainsMappingFromBranchToBranchViewModel()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMappingIsDefined<Branch, BranchViewModel>();
        }

        [Test]
        public void CanMapFromBranchToBranchViewModel()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMapperCanConvertFromSourceToDestination<Branch, BranchViewModel>();
        }

        [Test]
        public void ContainsMappingFromBranchViewModelToBranch()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMappingIsDefined<BranchViewModel, Branch>();
        }

        [Test]
        public void CanMapFromBranchViewModelToBranch()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMapperCanConvertFromSourceToDestination<BranchViewModel, Branch>();
        }

        [Test]
        public void ContainsMappingFromBranchToKeyValue()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMappingIsDefined<Branch, KeyValue>();
        }

        [Test]
        public void CanMapFromBranchToKeyValue()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMapperCanConvertFromSourceToDestination<Branch, KeyValue>();
        }

        [Test]
        public void ContainsMappingFromBranchToRelatedItemsViewModel()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMappingIsDefined<Branch, RelatedItemsViewModel>();
        }

        [Test]
        public void CanMapFromBranchToRelatedItemsViewModel()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMapperCanConvertFromSourceToDestination<Branch, RelatedItemsViewModel>();
        }

        [Test]
        public void ContainsMappingFromBranchToRelatedItemViewModel()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMappingIsDefined<Branch, RelatedItemViewModel>();
        }

        [Test]
        public void CanMapFromBranchToRelatedItemViewModel()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMapperCanConvertFromSourceToDestination<Branch, RelatedItemViewModel>();
        }

        #endregion // Branch Mapping Tests
    }
}
