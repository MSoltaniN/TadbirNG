using System;
using NUnit.Framework;
using SPPC.Framework.Helpers;
using SPPC.Tadbir.Configuration.Models;
using SPPC.Tadbir.Model.Config;
using SPPC.Tadbir.Model.Metadata;
using SPPC.Tadbir.ViewModel.Config;

namespace SPPC.Tadbir.Mapper.Tests
{
    [TestFixture]
    [Category("DomainMapping")]
    public class DomainMapperConfigTests : DomainMapperTestBase
    {
        [OneTimeSetUp]
        public void FixtureSetup()
        {
            SetUp();
        }

        #region Setting Mapping Tests

        [Test]
        public void ContainsMappingFromSettingToSettingBriefViewModel()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMappingIsDefined<Setting, SettingBriefViewModel>();
        }

        [Test]
        public void CanMapFromSettingToSettingBriefViewModel()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            var setting = new Setting() { Values = "{}", DefaultValues = "{}", ModelType = "TestConfig" };
            Mapper.Map<SettingBriefViewModel>(setting);
        }

        [Test]
        public void ContainsMappingFromSettingToRelationsConfig()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMappingIsDefined<Setting, RelationsConfig>();
        }

        [Test]
        public void CanMapFromSettingToRelationsConfig()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            var setting = new Setting() { Values = "{}" };
            Mapper.Map<RelationsConfig>(setting);
        }

        [Test]
        public void ContainsMappingFromSettingToDateRangeConfig()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMappingIsDefined<Setting, DateRangeConfig>();
        }

        [Test]
        public void CanMapFromSettingToDateRangeConfig()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            var setting = new Setting() { Values = "{}" };
            Mapper.Map<DateRangeConfig>(setting);
        }

        [Test]
        public void ContainsMappingFromSettingToNumberDisplayConfig()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMappingIsDefined<Setting, NumberDisplayConfig>();
        }

        [Test]
        public void CanMapFromSettingToNumberDisplayConfig()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            var setting = new Setting() { Values = "{}" };
            Mapper.Map<NumberDisplayConfig>(setting);
        }

        [Test]
        public void ContainsMappingFromSettingToListFormViewConfig()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMappingIsDefined<Setting, ListFormViewConfig>();
        }

        [Test]
        public void CanMapFromSettingToListFormViewConfig()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            var setting = new Setting() { Values = "{}" };
            Mapper.Map<ListFormViewConfig>(setting);
        }

        [Test]
        public void ContainsMappingFromSettingToEntityRowAccessConfig()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMappingIsDefined<Setting, EntityRowAccessConfig>();
        }

        [Test]
        public void CanMapFromSettingToEntityRowAccessConfig()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            var setting = new Setting() { Values = "{}" };
            Mapper.Map<EntityRowAccessConfig>(setting);
        }

        #endregion

        #region ViewSetting Mapping Tests

        [Test]
        public void ContainsMappingFromViewSettingToViewTreeFullConfig()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMappingIsDefined<ViewSetting, ViewTreeFullConfig>();
        }

        [Test]
        public void CanMapFromViewSettingToViewTreeFullConfig()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            var setting = new ViewSetting() { Values = "{}", DefaultValues = "{}" };
            Mapper.Map<ViewTreeFullConfig>(setting);
        }

        [Test]
        public void ContainsMappingFromViewTreeFullConfigToViewSetting()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMappingIsDefined<ViewSetting, ViewTreeFullConfig>();
        }

        [Test]
        public void CanMapFromViewTreeFullConfigToViewSetting()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMapperCanConvertFromSourceToDestination<ViewTreeFullConfig, ViewSetting>();
        }

        #endregion // ViewSetting Mapping Tests

        #region Other Mapping Tests

        [Test]
        public void ContainsMappingFromColumnToColumnViewConfig()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMappingIsDefined<Column, ColumnViewConfig>();
        }

        [Test]
        public void CanMapFromColumnToColumnViewConfig()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMapperCanConvertFromSourceToDestination<Column, ColumnViewConfig>();
        }

        [Test]
        public void ContainsMappingFromUserSettingToListFormViewConfig()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMappingIsDefined<UserSetting, ListFormViewConfig>();
        }

        [Test]
        public void CanMapFromUserSettingToListFormViewConfig()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            var setting = new UserSetting() { Values = "{}" };
            Mapper.Map<ListFormViewConfig>(setting);
        }

        #endregion // Other Mapping Tests

        #region CompanyDb Mapping Tests

        [Test]
        public void ContainsMappingFromCompanyDbToCompanyDbViewModel()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMappingIsDefined<CompanyDb, CompanyDbViewModel>();
        }

        [Test]
        public void CanMapFromCompanyDbToCompanyDbViewModel()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMapperCanConvertFromSourceToDestination<CompanyDb, CompanyDbViewModel>();
        }

        [Test]
        public void ContainsMappingFromCompanyDbViewModelToCompanyDb()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMappingIsDefined<CompanyDbViewModel, CompanyDb>();
        }

        [Test]
        public void CanMapFromCompanyDbViewModelToCompanyDb()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMapperCanConvertFromSourceToDestination<CompanyDbViewModel, CompanyDb>();
        }

        [Test]
        public void ContainsMappingFromCompanyDbToKeyValue()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMappingIsDefined<CompanyDb, KeyValue>();
        }

        [Test]
        public void CanMapFromCompanyDbToKeyValue()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMapperCanConvertFromSourceToDestination<CompanyDb, KeyValue>();
        }

        #endregion
    }
}
