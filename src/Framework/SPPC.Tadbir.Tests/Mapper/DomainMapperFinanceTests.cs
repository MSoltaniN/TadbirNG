using System;
using NUnit.Framework;
using SPPC.Framework.Helpers;
using SPPC.Tadbir.Model.Finance;
using SPPC.Tadbir.ViewModel;
using SPPC.Tadbir.ViewModel.Finance;

namespace SPPC.Tadbir.Mapper.Tests
{
    [TestFixture]
    [Category("DomainMapping")]
    public class DomainMapperFinanceTests : DomainMapperTestBase
    {
        [OneTimeSetUp]
        public void FixtureSetup()
        {
            SetUp();
        }

        #region Account Mapping Tests

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
        public void ContainsMappingFromAccountToAccountItemBriefViewModel()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMappingIsDefined<Account, AccountItemBriefViewModel>();
        }

        [Test]
        public void CanMapFromAccountToAccountItemBriefViewModel()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMapperCanConvertFromSourceToDestination<Account, AccountItemBriefViewModel>();
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

        #endregion // Account Mapping Tests

        #region DetailAccount Mapping Tests

        [Test]
        public void ContainsMappingFromDetailAccountToDetailAccountViewModel()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMappingIsDefined<DetailAccount, DetailAccountViewModel>();
        }

        [Test]
        public void CanMapFromDetailAccountToDetailAccountViewModel()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMapperCanConvertFromSourceToDestination<DetailAccount, DetailAccountViewModel>();
        }

        [Test]
        public void ContainsMappingFromDetailAccountToAccountItemBriefViewModel()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMappingIsDefined<DetailAccount, AccountItemBriefViewModel>();
        }

        [Test]
        public void CanMapFromDetailAccountToAccountItemBriefViewModel()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMapperCanConvertFromSourceToDestination<DetailAccount, AccountItemBriefViewModel>();
        }

        [Test]
        public void ContainsMappingFromDetailAccountViewModelToDetailAccount()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMappingIsDefined<DetailAccountViewModel, DetailAccount>();
        }

        [Test]
        public void CanMapFromDetailAccountViewModelToDetailAccount()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMapperCanConvertFromSourceToDestination<DetailAccountViewModel, DetailAccount>();
        }

        [Test]
        public void ContainsMappingFromDetailAccountToKeyValue()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMappingIsDefined<DetailAccount, KeyValue>();
        }

        [Test]
        public void CanMapFromDetailAccountToKeyValue()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMapperCanConvertFromSourceToDestination<DetailAccount, KeyValue>();
        }

        #endregion // DetailAccount Mapping Tests

        #region CostCenter Mapping Tests

        [Test]
        public void ContainsMappingFromCostCenterToCostCenterViewModel()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMappingIsDefined<CostCenter, CostCenterViewModel>();
        }

        [Test]
        public void CanMapFromCostCenterToCostCenterViewModel()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMapperCanConvertFromSourceToDestination<CostCenter, CostCenterViewModel>();
        }

        [Test]
        public void ContainsMappingFromCostCenterToAccountItemBriefViewModel()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMappingIsDefined<CostCenter, AccountItemBriefViewModel>();
        }

        [Test]
        public void CanMapFromCostCenterToAccountItemBriefViewModel()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMapperCanConvertFromSourceToDestination<CostCenter, AccountItemBriefViewModel>();
        }

        [Test]
        public void ContainsMappingFromCostCenterViewModelToCostCenter()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMappingIsDefined<CostCenterViewModel, CostCenter>();
        }

        [Test]
        public void CanMapFromCostCenterViewModelToCostCenter()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMapperCanConvertFromSourceToDestination<CostCenterViewModel, CostCenter>();
        }

        [Test]
        public void ContainsMappingFromCostCenterToKeyValue()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMappingIsDefined<CostCenter, KeyValue>();
        }

        [Test]
        public void CanMapFromCostCenterToKeyValue()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMapperCanConvertFromSourceToDestination<CostCenter, KeyValue>();
        }

        #endregion // CostCenter Mapping Tests

        #region Project Mapping Tests

        [Test]
        public void ContainsMappingFromProjectToProjectViewModel()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMappingIsDefined<Project, ProjectViewModel>();
        }

        [Test]
        public void CanMapFromProjectToProjectViewModel()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMapperCanConvertFromSourceToDestination<Project, ProjectViewModel>();
        }

        [Test]
        public void ContainsMappingFromProjectToAccountItemBriefViewModel()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMappingIsDefined<Project, AccountItemBriefViewModel>();
        }

        [Test]
        public void CanMapFromProjectToAccountItemBriefViewModel()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMapperCanConvertFromSourceToDestination<Project, AccountItemBriefViewModel>();
        }

        [Test]
        public void ContainsMappingFromProjectViewModelToProject()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMappingIsDefined<ProjectViewModel, Project>();
        }

        [Test]
        public void CanMapFromProjectViewModelToProject()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMapperCanConvertFromSourceToDestination<ProjectViewModel, Project>();
        }

        [Test]
        public void ContainsMappingFromProjectToKeyValue()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMappingIsDefined<Project, KeyValue>();
        }

        [Test]
        public void CanMapFromProjectToKeyValue()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMapperCanConvertFromSourceToDestination<Project, KeyValue>();
        }

        #endregion // Project Mapping Tests

        #region Voucher Mapping Tests

        [Test]
        public void ContainsMappingFromVoucherToVoucherViewModel()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMappingIsDefined<Voucher, VoucherViewModel>();
        }

        [Test]
        public void CanMapFromVoucherToVoucherViewModel()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMapperCanConvertFromSourceToDestination<Voucher, VoucherViewModel>();
        }

        [Test]
        public void ContainsMappingFromVoucherViewModelToVoucher()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMappingIsDefined<VoucherViewModel, Voucher>();
        }

        [Test]
        public void CanMapFromVoucherViewModelToVoucher()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMapperCanConvertFromSourceToDestination<VoucherViewModel, Voucher>();
        }

        [Test]
        public void ContainsMappingFromVoucherToKeyValue()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMappingIsDefined<Voucher, KeyValue>();
        }

        [Test]
        public void CanMapFromVoucherToKeyValue()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMapperCanConvertFromSourceToDestination<Voucher, KeyValue>();
        }

        #endregion // Voucher Mapping Tests

        #region VoucherLine Mapping Tests

        [Test]
        public void ContainsMappingFromVoucherLineToVoucherLineViewModel()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMappingIsDefined<VoucherLine, VoucherLineViewModel>();
        }

        [Test]
        public void CanMapFromVoucherLineToVoucherLineViewModel()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMapperCanConvertFromSourceToDestination<VoucherLine, VoucherLineViewModel>();
        }

        [Test]
        public void ContainsMappingFromVoucherLineViewModelToVoucherLine()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMappingIsDefined<VoucherLineViewModel, VoucherLine>();
        }

        [Test]
        public void CanMapFromVoucherLineViewModelToVoucherLine()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMapperCanConvertFromSourceToDestination<VoucherLineViewModel, VoucherLine>();
        }

        [Test]
        public void ContainsMappingFromVoucherLineToKeyValue()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMappingIsDefined<VoucherLine, KeyValue>();
        }

        [Test]
        public void CanMapFromVoucherLineToKeyValue()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMapperCanConvertFromSourceToDestination<VoucherLine, KeyValue>();
        }

        #endregion // VoucherLine Mapping Tests

        #region Currency Mapping Tests

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

        #endregion // Currency Mapping Tests

        #region FiscalPeriod Mapping Tests

        [Test]
        public void ContainsMappingFromFiscalPeriodToFiscalPeriodViewModel()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMappingIsDefined<FiscalPeriod, FiscalPeriodViewModel>();
        }

        [Test]
        public void CanMapFromFiscalPeriodToFiscalPeriodViewModel()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMapperCanConvertFromSourceToDestination<FiscalPeriod, FiscalPeriodViewModel>();
        }

        [Test]
        public void ContainsMappingFromFiscalPeriodViewModelToFiscalPeriod()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMappingIsDefined<FiscalPeriodViewModel, FiscalPeriod>();
        }

        [Test]
        public void CanMapFromFiscalPeriodViewModelToFiscalPeriod()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMapperCanConvertFromSourceToDestination<FiscalPeriodViewModel, FiscalPeriod>();
        }

        [Test]
        public void ContainsMappingFromFiscalPeriodToKeyValue()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMappingIsDefined<FiscalPeriod, KeyValue>();
        }

        [Test]
        public void CanMapFromFiscalPeriodToKeyValue()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMapperCanConvertFromSourceToDestination<FiscalPeriod, KeyValue>();
        }

        [Test]
        public void ContainsMappingFromFiscalPeriodToRelatedItemsViewModel()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMappingIsDefined<FiscalPeriod, RelatedItemsViewModel>();
        }

        [Test]
        public void CanMapFromFiscalPeriodToRelatedItemsViewModel()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMapperCanConvertFromSourceToDestination<FiscalPeriod, RelatedItemsViewModel>();
        }

        [Test]
        public void ContainsMappingFromFiscalPeriodToRelatedItemViewModel()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMappingIsDefined<FiscalPeriod, RelatedItemViewModel>();
        }

        [Test]
        public void CanMapFromFiscalPeriodToRelatedItemViewModel()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMapperCanConvertFromSourceToDestination<FiscalPeriod, RelatedItemViewModel>();
        }

        #endregion // FiscalPeriod Mapping Tests

        #region Other Mapping Tests

        [Test]
        public void ContainsMappingFromAccountItemBriefViewModelToKeyValue()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMappingIsDefined<AccountItemBriefViewModel, KeyValue>();
        }

        [Test]
        public void CanMapFromAccountItemBriefViewModelToKeyValue()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMapperCanConvertFromSourceToDestination<AccountItemBriefViewModel, KeyValue>();
        }

        #endregion
    }
}
