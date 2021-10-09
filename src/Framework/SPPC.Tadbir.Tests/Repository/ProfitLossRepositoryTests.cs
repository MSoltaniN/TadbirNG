using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NUnit.Framework;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Persistence;
using SPPC.Tadbir.Persistence.Utility;
using SPPC.Tadbir.ViewModel.Finance;
using SPPC.Tadbir.ViewModel.Reporting;

namespace SPPC.Tadbir.Tests.Repository
{
    [TestFixture]
    [Category("RepositorySmokeTest")]
    public class ProfitLossRepositoryTests : RepositoryTestBase
    {
        [OneTimeSetUp]
        public void FixtureSetup()
        {
            SetUp();
        }

        #region Perpetual Fiscal Period Tests

        #region Profit-Loss Tests

        #region CheckedVouchers_CurrentBranch_NoOptions

        [Test]
        public async Task ProfitLoss_CheckedVouchers_CurrentBranch_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CheckedVouchers_CurrentBranch_NoOptions();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CheckedVouchers_CurrentBranch_ByCenter_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CheckedVouchers_CurrentBranch_NoOptions(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CheckedVouchers_CurrentBranch_ByProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CheckedVouchers_CurrentBranch_NoOptions(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CheckedVouchers_CurrentBranch_ByCenterProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CheckedVouchers_CurrentBranch_NoOptions(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region FinalizedVouchers_CurrentBranch_NoOptions

        [Test]
        public async Task ProfitLoss_FinalizedVouchers_CurrentBranch_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_FinalizedVouchers_CurrentBranch_NoOptions();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_FinalizedVouchers_CurrentBranch_ByCenter_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_FinalizedVouchers_CurrentBranch_NoOptions(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_FinalizedVouchers_CurrentBranch_ByProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_FinalizedVouchers_CurrentBranch_NoOptions(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_FinalizedVouchers_CurrentBranch_ByCenterProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_FinalizedVouchers_CurrentBranch_NoOptions(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region ConfirmedVouchers_CurrentBranch_NoOptions

        [Test]
        public async Task ProfitLoss_ConfirmedVouchers_CurrentBranch_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_ConfirmedVouchers_CurrentBranch_NoOptions();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_ConfirmedVouchers_CurrentBranch_ByCenter_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_ConfirmedVouchers_CurrentBranch_NoOptions(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_ConfirmedVouchers_CurrentBranch_ByProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_ConfirmedVouchers_CurrentBranch_NoOptions(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_ConfirmedVouchers_CurrentBranch_ByCenterProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_ConfirmedVouchers_CurrentBranch_NoOptions(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region ApprovedVouchers_CurrentBranch_NoOptions

        [Test]
        public async Task ProfitLoss_ApprovedVouchers_CurrentBranch_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_ApprovedVouchers_CurrentBranch_NoOptions();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_ApprovedVouchers_CurrentBranch_ByCenter_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_ApprovedVouchers_CurrentBranch_NoOptions(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_ApprovedVouchers_CurrentBranch_ByProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_ApprovedVouchers_CurrentBranch_NoOptions(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_ApprovedVouchers_CurrentBranch_ByCenterProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_ApprovedVouchers_CurrentBranch_NoOptions(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region AllVouchers_CurrentBranch_NoOptions

        [Test]
        public async Task ProfitLoss_AllVouchers_CurrentBranch_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_AllVouchers_CurrentBranch_NoOptions();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_AllVouchers_CurrentBranch_ByCenter_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_AllVouchers_CurrentBranch_NoOptions(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_AllVouchers_CurrentBranch_ByProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_AllVouchers_CurrentBranch_NoOptions(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_AllVouchers_CurrentBranch_ByCenterProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_AllVouchers_CurrentBranch_NoOptions(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region CheckedVouchers_CurrentAndChildren_NoOptions

        [Test]
        public async Task ProfitLoss_CheckedVouchers_CurrentAndChildren_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CheckedVouchers_CurrentAndChildren_NoOptions();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CheckedVouchers_CurrentAndChildren_ByCenter_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CheckedVouchers_CurrentAndChildren_NoOptions(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CheckedVouchers_CurrentAndChildren_ByProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CheckedVouchers_CurrentAndChildren_NoOptions(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CheckedVouchers_CurrentAndChildren_ByCenterProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CheckedVouchers_CurrentAndChildren_NoOptions(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region FinalizedVouchers_CurrentAndChildren_NoOptions

        [Test]
        public async Task ProfitLoss_FinalizedVouchers_CurrentAndChildren_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_FinalizedVouchers_CurrentAndChildren_NoOptions();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_FinalizedVouchers_CurrentAndChildren_ByCenter_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_FinalizedVouchers_CurrentAndChildren_NoOptions(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_FinalizedVouchers_CurrentAndChildren_ByProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_FinalizedVouchers_CurrentAndChildren_NoOptions(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_FinalizedVouchers_CurrentAndChildren_ByCenterProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_FinalizedVouchers_CurrentAndChildren_NoOptions(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region ConfirmedVouchers_CurrentAndChildren_NoOptions

        [Test]
        public async Task ProfitLoss_ConfirmedVouchers_CurrentAndChildren_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_ConfirmedVouchers_CurrentAndChildren_NoOptions();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_ConfirmedVouchers_CurrentAndChildren_ByCenter_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_ConfirmedVouchers_CurrentAndChildren_NoOptions(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_ConfirmedVouchers_CurrentAndChildren_ByProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_ConfirmedVouchers_CurrentAndChildren_NoOptions(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_ConfirmedVouchers_CurrentAndChildren_ByCenterProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_ConfirmedVouchers_CurrentAndChildren_NoOptions(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region ApprovedVouchers_CurrentAndChildren_NoOptions

        [Test]
        public async Task ProfitLoss_ApprovedVouchers_CurrentAndChildren_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_ApprovedVouchers_CurrentAndChildren_NoOptions();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_ApprovedVouchers_CurrentAndChildren_ByCenter_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_ApprovedVouchers_CurrentAndChildren_NoOptions(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_ApprovedVouchers_CurrentAndChildren_ByProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_ApprovedVouchers_CurrentAndChildren_NoOptions(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_ApprovedVouchers_CurrentAndChildren_ByCenterProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_ApprovedVouchers_CurrentAndChildren_NoOptions(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region AllVouchers_CurrentAndChildren_NoOptions

        [Test]
        public async Task ProfitLoss_AllVouchers_CurrentAndChildren_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_AllVouchers_CurrentAndChildren_NoOptions();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_AllVouchers_CurrentAndChildren_ByCenter_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_AllVouchers_CurrentAndChildren_NoOptions(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_AllVouchers_CurrentAndChildren_ByProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_AllVouchers_CurrentAndChildren_NoOptions(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_AllVouchers_CurrentAndChildren_ByCenterProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_AllVouchers_CurrentAndChildren_NoOptions(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region CheckedVouchers_CurrentBranch_ClosingOption

        [Test]
        public async Task ProfitLoss_CheckedVouchers_CurrentBranch_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CheckedVouchers_CurrentBranch_ClosingOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CheckedVouchers_CurrentBranch_ByCenter_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CheckedVouchers_CurrentBranch_ClosingOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CheckedVouchers_CurrentBranch_ByProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CheckedVouchers_CurrentBranch_ClosingOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CheckedVouchers_CurrentBranch_ByCenterProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CheckedVouchers_CurrentBranch_ClosingOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region FinalizedVouchers_CurrentBranch_ClosingOption

        [Test]
        public async Task ProfitLoss_FinalizedVouchers_CurrentBranch_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_FinalizedVouchers_CurrentBranch_ClosingOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_FinalizedVouchers_CurrentBranch_ByCenter_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_FinalizedVouchers_CurrentBranch_ClosingOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_FinalizedVouchers_CurrentBranch_ByProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_FinalizedVouchers_CurrentBranch_ClosingOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_FinalizedVouchers_CurrentBranch_ByCenterProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_FinalizedVouchers_CurrentBranch_ClosingOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region ConfirmedVouchers_CurrentBranch_ClosingOption

        [Test]
        public async Task ProfitLoss_ConfirmedVouchers_CurrentBranch_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_ConfirmedVouchers_CurrentBranch_ClosingOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_ConfirmedVouchers_CurrentBranch_ByCenter_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_ConfirmedVouchers_CurrentBranch_ClosingOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_ConfirmedVouchers_CurrentBranch_ByProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_ConfirmedVouchers_CurrentBranch_ClosingOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_ConfirmedVouchers_CurrentBranch_ByCenterProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_ConfirmedVouchers_CurrentBranch_ClosingOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region ApprovedVouchers_CurrentBranch_ClosingOption

        [Test]
        public async Task ProfitLoss_ApprovedVouchers_CurrentBranch_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_ApprovedVouchers_CurrentBranch_ClosingOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_ApprovedVouchers_CurrentBranch_ByCenter_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_ApprovedVouchers_CurrentBranch_ClosingOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_ApprovedVouchers_CurrentBranch_ByProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_ApprovedVouchers_CurrentBranch_ClosingOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_ApprovedVouchers_CurrentBranch_ByCenterProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_ApprovedVouchers_CurrentBranch_ClosingOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region AllVouchers_CurrentBranch_ClosingOption

        [Test]
        public async Task ProfitLoss_AllVouchers_CurrentBranch_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_AllVouchers_CurrentBranch_ClosingOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_AllVouchers_CurrentBranch_ByCenter_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_AllVouchers_CurrentBranch_ClosingOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_AllVouchers_CurrentBranch_ByProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_AllVouchers_CurrentBranch_ClosingOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_AllVouchers_CurrentBranch_ByCenterProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_AllVouchers_CurrentBranch_ClosingOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region CheckedVouchers_CurrentAndChildren_ClosingOption

        [Test]
        public async Task ProfitLoss_CheckedVouchers_CurrentAndChildren_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CheckedVouchers_CurrentAndChildren_ClosingOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CheckedVouchers_CurrentAndChildren_ByCenter_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CheckedVouchers_CurrentAndChildren_ClosingOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CheckedVouchers_CurrentAndChildren_ByProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CheckedVouchers_CurrentAndChildren_ClosingOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CheckedVouchers_CurrentAndChildren_ByCenterProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CheckedVouchers_CurrentAndChildren_ClosingOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region FinalizedVouchers_CurrentAndChildren_ClosingOption

        [Test]
        public async Task ProfitLoss_FinalizedVouchers_CurrentAndChildren_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_FinalizedVouchers_CurrentAndChildren_ClosingOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_FinalizedVouchers_CurrentAndChildren_ByCenter_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_FinalizedVouchers_CurrentAndChildren_ClosingOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_FinalizedVouchers_CurrentAndChildren_ByProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_FinalizedVouchers_CurrentAndChildren_ClosingOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_FinalizedVouchers_CurrentAndChildren_ByCenterProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_FinalizedVouchers_CurrentAndChildren_ClosingOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region ConfirmedVouchers_CurrentAndChildren_ClosingOption

        [Test]
        public async Task ProfitLoss_ConfirmedVouchers_CurrentAndChildren_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_ConfirmedVouchers_CurrentAndChildren_ClosingOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_ConfirmedVouchers_CurrentAndChildren_ByCenter_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_ConfirmedVouchers_CurrentAndChildren_ClosingOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_ConfirmedVouchers_CurrentAndChildren_ByProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_ConfirmedVouchers_CurrentAndChildren_ClosingOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_ConfirmedVouchers_CurrentAndChildren_ByCenterProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_ConfirmedVouchers_CurrentAndChildren_ClosingOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region ApprovedVouchers_CurrentAndChildren_ClosingOption

        [Test]
        public async Task ProfitLoss_ApprovedVouchers_CurrentAndChildren_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_ApprovedVouchers_CurrentAndChildren_ClosingOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_ApprovedVouchers_CurrentAndChildren_ByCenter_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_ApprovedVouchers_CurrentAndChildren_ClosingOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_ApprovedVouchers_CurrentAndChildren_ByProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_ApprovedVouchers_CurrentAndChildren_ClosingOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_ApprovedVouchers_CurrentAndChildren_ByCenterProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_ApprovedVouchers_CurrentAndChildren_ClosingOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region AllVouchers_CurrentAndChildren_ClosingOption

        [Test]
        public async Task ProfitLoss_AllVouchers_CurrentAndChildren_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_AllVouchers_CurrentAndChildren_ClosingOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_AllVouchers_CurrentAndChildren_ByCenter_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_AllVouchers_CurrentAndChildren_ClosingOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_AllVouchers_CurrentAndChildren_ByProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_AllVouchers_CurrentAndChildren_ClosingOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_AllVouchers_CurrentAndChildren_ByCenterProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_AllVouchers_CurrentAndChildren_ClosingOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region CheckedVouchers_CurrentBranch_TurnoverOption

        [Test]
        public async Task ProfitLoss_CheckedVouchers_CurrentBranch_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CheckedVouchers_CurrentBranch_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CheckedVouchers_CurrentBranch_ByCenter_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CheckedVouchers_CurrentBranch_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CheckedVouchers_CurrentBranch_ByProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CheckedVouchers_CurrentBranch_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CheckedVouchers_CurrentBranch_ByCenterProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CheckedVouchers_CurrentBranch_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region FinalizedVouchers_CurrentBranch_TurnoverOption

        [Test]
        public async Task ProfitLoss_FinalizedVouchers_CurrentBranch_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_FinalizedVouchers_CurrentBranch_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_FinalizedVouchers_CurrentBranch_ByCenter_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_FinalizedVouchers_CurrentBranch_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_FinalizedVouchers_CurrentBranch_ByProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_FinalizedVouchers_CurrentBranch_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_FinalizedVouchers_CurrentBranch_ByCenterProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_FinalizedVouchers_CurrentBranch_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region ConfirmedVouchers_CurrentBranch_TurnoverOption

        [Test]
        public async Task ProfitLoss_ConfirmedVouchers_CurrentBranch_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_ConfirmedVouchers_CurrentBranch_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_ConfirmedVouchers_CurrentBranch_ByCenter_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_ConfirmedVouchers_CurrentBranch_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_ConfirmedVouchers_CurrentBranch_ByProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_ConfirmedVouchers_CurrentBranch_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_ConfirmedVouchers_CurrentBranch_ByCenterProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_ConfirmedVouchers_CurrentBranch_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region ApprovedVouchers_CurrentBranch_TurnoverOption

        [Test]
        public async Task ProfitLoss_ApprovedVouchers_CurrentBranch_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_ApprovedVouchers_CurrentBranch_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_ApprovedVouchers_CurrentBranch_ByCenter_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_ApprovedVouchers_CurrentBranch_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_ApprovedVouchers_CurrentBranch_ByProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_ApprovedVouchers_CurrentBranch_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_ApprovedVouchers_CurrentBranch_ByCenterProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_ApprovedVouchers_CurrentBranch_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region AllVouchers_CurrentBranch_TurnoverOption

        [Test]
        public async Task ProfitLoss_AllVouchers_CurrentBranch_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_AllVouchers_CurrentBranch_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_AllVouchers_CurrentBranch_ByCenter_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_AllVouchers_CurrentBranch_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_AllVouchers_CurrentBranch_ByProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_AllVouchers_CurrentBranch_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_AllVouchers_CurrentBranch_ByCenterProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_AllVouchers_CurrentBranch_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region CheckedVouchers_CurrentAndChildren_TurnoverOption

        [Test]
        public async Task ProfitLoss_CheckedVouchers_CurrentAndChildren_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CheckedVouchers_CurrentAndChildren_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CheckedVouchers_CurrentAndChildren_ByCenter_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CheckedVouchers_CurrentAndChildren_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CheckedVouchers_CurrentAndChildren_ByProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CheckedVouchers_CurrentAndChildren_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CheckedVouchers_CurrentAndChildren_ByCenterProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CheckedVouchers_CurrentAndChildren_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region FinalizedVouchers_CurrentAndChildren_TurnoverOption

        [Test]
        public async Task ProfitLoss_FinalizedVouchers_CurrentAndChildren_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_FinalizedVouchers_CurrentAndChildren_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_FinalizedVouchers_CurrentAndChildren_ByCenter_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_FinalizedVouchers_CurrentAndChildren_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_FinalizedVouchers_CurrentAndChildren_ByProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_FinalizedVouchers_CurrentAndChildren_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_FinalizedVouchers_CurrentAndChildren_ByCenterProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_FinalizedVouchers_CurrentAndChildren_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region ConfirmedVouchers_CurrentAndChildren_TurnoverOption

        [Test]
        public async Task ProfitLoss_ConfirmedVouchers_CurrentAndChildren_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_ConfirmedVouchers_CurrentAndChildren_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_ConfirmedVouchers_CurrentAndChildren_ByCenter_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_ConfirmedVouchers_CurrentAndChildren_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_ConfirmedVouchers_CurrentAndChildren_ByProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_ConfirmedVouchers_CurrentAndChildren_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_ConfirmedVouchers_CurrentAndChildren_ByCenterProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_ConfirmedVouchers_CurrentAndChildren_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region ApprovedVouchers_CurrentAndChildren_TurnoverOption

        [Test]
        public async Task ProfitLoss_ApprovedVouchers_CurrentAndChildren_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_ApprovedVouchers_CurrentAndChildren_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_ApprovedVouchers_CurrentAndChildren_ByCenter_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_ApprovedVouchers_CurrentAndChildren_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_ApprovedVouchers_CurrentAndChildren_ByProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_ApprovedVouchers_CurrentAndChildren_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_ApprovedVouchers_CurrentAndChildren_ByCenterProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_ApprovedVouchers_CurrentAndChildren_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region AllVouchers_CurrentAndChildren_TurnoverOption

        [Test]
        public async Task ProfitLoss_AllVouchers_CurrentAndChildren_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_AllVouchers_CurrentAndChildren_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_AllVouchers_CurrentAndChildren_ByCenter_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_AllVouchers_CurrentAndChildren_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_AllVouchers_CurrentAndChildren_ByProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_AllVouchers_CurrentAndChildren_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_AllVouchers_CurrentAndChildren_ByCenterProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_AllVouchers_CurrentAndChildren_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region CheckedVouchers_CurrentBranch_ClosingOption_TurnoverOption

        [Test]
        public async Task ProfitLoss_CheckedVouchers_CurrentBranch_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CheckedVouchers_CurrentBranch_ClosingOption_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CheckedVouchers_CurrentBranch_ByCenter_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CheckedVouchers_CurrentBranch_ClosingOption_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CheckedVouchers_CurrentBranch_ByProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CheckedVouchers_CurrentBranch_ClosingOption_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CheckedVouchers_CurrentBranch_ByCenterProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CheckedVouchers_CurrentBranch_ClosingOption_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region FinalizedVouchers_CurrentBranch_ClosingOption_TurnoverOption

        [Test]
        public async Task ProfitLoss_FinalizedVouchers_CurrentBranch_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_FinalizedVouchers_CurrentBranch_ClosingOption_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_FinalizedVouchers_CurrentBranch_ByCenter_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_FinalizedVouchers_CurrentBranch_ClosingOption_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_FinalizedVouchers_CurrentBranch_ByProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_FinalizedVouchers_CurrentBranch_ClosingOption_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_FinalizedVouchers_CurrentBranch_ByCenterProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_FinalizedVouchers_CurrentBranch_ClosingOption_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region ConfirmedVouchers_CurrentBranch_ClosingOption_TurnoverOption

        [Test]
        public async Task ProfitLoss_ConfirmedVouchers_CurrentBranch_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_ConfirmedVouchers_CurrentBranch_ClosingOption_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_ConfirmedVouchers_CurrentBranch_ByCenter_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_ConfirmedVouchers_CurrentBranch_ClosingOption_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_ConfirmedVouchers_CurrentBranch_ByProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_ConfirmedVouchers_CurrentBranch_ClosingOption_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_ConfirmedVouchers_CurrentBranch_ByCenterProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_ConfirmedVouchers_CurrentBranch_ClosingOption_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region ApprovedVouchers_CurrentBranch_ClosingOption_TurnoverOption

        [Test]
        public async Task ProfitLoss_ApprovedVouchers_CurrentBranch_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_ApprovedVouchers_CurrentBranch_ClosingOption_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_ApprovedVouchers_CurrentBranch_ByCenter_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_ApprovedVouchers_CurrentBranch_ClosingOption_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_ApprovedVouchers_CurrentBranch_ByProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_ApprovedVouchers_CurrentBranch_ClosingOption_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_ApprovedVouchers_CurrentBranch_ByCenterProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_ApprovedVouchers_CurrentBranch_ClosingOption_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region AllVouchers_CurrentBranch_ClosingOption_TurnoverOption

        [Test]
        public async Task ProfitLoss_AllVouchers_CurrentBranch_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_AllVouchers_CurrentBranch_ClosingOption_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_AllVouchers_CurrentBranch_ByCenter_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_AllVouchers_CurrentBranch_ClosingOption_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_AllVouchers_CurrentBranch_ByProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_AllVouchers_CurrentBranch_ClosingOption_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_AllVouchers_CurrentBranch_ByCenterProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_AllVouchers_CurrentBranch_ClosingOption_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region CheckedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption

        [Test]
        public async Task ProfitLoss_CheckedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CheckedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CheckedVouchers_CurrentAndChildren_ByCenter_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CheckedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CheckedVouchers_CurrentAndChildren_ByProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CheckedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CheckedVouchers_CurrentAndChildren_ByCenterProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CheckedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region FinalizedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption

        [Test]
        public async Task ProfitLoss_FinalizedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_FinalizedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_FinalizedVouchers_CurrentAndChildren_ByCenter_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_FinalizedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_FinalizedVouchers_CurrentAndChildren_ByProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_FinalizedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_FinalizedVouchers_CurrentAndChildren_ByCenterProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_FinalizedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region ConfirmedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption

        [Test]
        public async Task ProfitLoss_ConfirmedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_ConfirmedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_ConfirmedVouchers_CurrentAndChildren_ByCenter_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_ConfirmedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_ConfirmedVouchers_CurrentAndChildren_ByProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_ConfirmedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_ConfirmedVouchers_CurrentAndChildren_ByCenterProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_ConfirmedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region ApprovedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption

        [Test]
        public async Task ProfitLoss_ApprovedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_ApprovedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_ApprovedVouchers_CurrentAndChildren_ByCenter_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_ApprovedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_ApprovedVouchers_CurrentAndChildren_ByProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_ApprovedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_ApprovedVouchers_CurrentAndChildren_ByCenterProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_ApprovedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region AllVouchers_CurrentAndChildren_ClosingOption_TurnoverOption

        [Test]
        public async Task ProfitLoss_AllVouchers_CurrentAndChildren_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_AllVouchers_CurrentAndChildren_ClosingOption_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_AllVouchers_CurrentAndChildren_ByCenter_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_AllVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_AllVouchers_CurrentAndChildren_ByProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_AllVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_AllVouchers_CurrentAndChildren_ByCenterProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_AllVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #endregion

        #region Profit-Loss By Branches Tests

        #region CheckedVouchers_CurrentBranch_NoOptions

        [Test]
        public async Task ProfitLoss_CompareBranches_CheckedVouchers_CurrentBranch_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareBranches_CheckedVouchers_CurrentBranch_NoOptions();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareBranches_CheckedVouchers_CurrentBranch_ByCenter_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareBranches_CheckedVouchers_CurrentBranch_NoOptions(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareBranches_CheckedVouchers_CurrentBranch_ByProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareBranches_CheckedVouchers_CurrentBranch_NoOptions(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareBranches_CheckedVouchers_CurrentBranch_ByCenterProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareBranches_CheckedVouchers_CurrentBranch_NoOptions(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region FinalizedVouchers_CurrentBranch_NoOptions

        [Test]
        public async Task ProfitLoss_CompareBranches_FinalizedVouchers_CurrentBranch_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareBranches_FinalizedVouchers_CurrentBranch_NoOptions();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareBranches_FinalizedVouchers_CurrentBranch_ByCenter_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareBranches_FinalizedVouchers_CurrentBranch_NoOptions(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareBranches_FinalizedVouchers_CurrentBranch_ByProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareBranches_FinalizedVouchers_CurrentBranch_NoOptions(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareBranches_FinalizedVouchers_CurrentBranch_ByCenterProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareBranches_FinalizedVouchers_CurrentBranch_NoOptions(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region ConfirmedVouchers_CurrentBranch_NoOptions

        [Test]
        public async Task ProfitLoss_CompareBranches_ConfirmedVouchers_CurrentBranch_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareBranches_ConfirmedVouchers_CurrentBranch_NoOptions();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareBranches_ConfirmedVouchers_CurrentBranch_ByCenter_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareBranches_ConfirmedVouchers_CurrentBranch_NoOptions(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareBranches_ConfirmedVouchers_CurrentBranch_ByProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareBranches_ConfirmedVouchers_CurrentBranch_NoOptions(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareBranches_ConfirmedVouchers_CurrentBranch_ByCenterProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareBranches_ConfirmedVouchers_CurrentBranch_NoOptions(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region ApprovedVouchers_CurrentBranch_NoOptions

        [Test]
        public async Task ProfitLoss_CompareBranches_ApprovedVouchers_CurrentBranch_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareBranches_ApprovedVouchers_CurrentBranch_NoOptions();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareBranches_ApprovedVouchers_CurrentBranch_ByCenter_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareBranches_ApprovedVouchers_CurrentBranch_NoOptions(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareBranches_ApprovedVouchers_CurrentBranch_ByProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareBranches_ApprovedVouchers_CurrentBranch_NoOptions(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareBranches_ApprovedVouchers_CurrentBranch_ByCenterProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareBranches_ApprovedVouchers_CurrentBranch_NoOptions(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region AllVouchers_CurrentBranch_NoOptions

        [Test]
        public async Task ProfitLoss_CompareBranches_AllVouchers_CurrentBranch_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareBranches_AllVouchers_CurrentBranch_NoOptions();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareBranches_AllVouchers_CurrentBranch_ByCenter_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareBranches_AllVouchers_CurrentBranch_NoOptions(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareBranches_AllVouchers_CurrentBranch_ByProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareBranches_AllVouchers_CurrentBranch_NoOptions(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareBranches_AllVouchers_CurrentBranch_ByCenterProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareBranches_AllVouchers_CurrentBranch_NoOptions(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region CheckedVouchers_CurrentAndChildren_NoOptions

        [Test]
        public async Task ProfitLoss_CompareBranches_CheckedVouchers_CurrentAndChildren_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareBranches_CheckedVouchers_CurrentAndChildren_NoOptions();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareBranches_CheckedVouchers_CurrentAndChildren_ByCenter_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareBranches_CheckedVouchers_CurrentAndChildren_NoOptions(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareBranches_CheckedVouchers_CurrentAndChildren_ByProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareBranches_CheckedVouchers_CurrentAndChildren_NoOptions(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareBranches_CheckedVouchers_CurrentAndChildren_ByCenterProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareBranches_CheckedVouchers_CurrentAndChildren_NoOptions(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region FinalizedVouchers_CurrentAndChildren_NoOptions

        [Test]
        public async Task ProfitLoss_CompareBranches_FinalizedVouchers_CurrentAndChildren_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareBranches_FinalizedVouchers_CurrentAndChildren_NoOptions();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareBranches_FinalizedVouchers_CurrentAndChildren_ByCenter_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareBranches_FinalizedVouchers_CurrentAndChildren_NoOptions(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareBranches_FinalizedVouchers_CurrentAndChildren_ByProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareBranches_FinalizedVouchers_CurrentAndChildren_NoOptions(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareBranches_FinalizedVouchers_CurrentAndChildren_ByCenterProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareBranches_FinalizedVouchers_CurrentAndChildren_NoOptions(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region ConfirmedVouchers_CurrentAndChildren_NoOptions

        [Test]
        public async Task ProfitLoss_CompareBranches_ConfirmedVouchers_CurrentAndChildren_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareBranches_ConfirmedVouchers_CurrentAndChildren_NoOptions();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareBranches_ConfirmedVouchers_CurrentAndChildren_ByCenter_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareBranches_ConfirmedVouchers_CurrentAndChildren_NoOptions(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareBranches_ConfirmedVouchers_CurrentAndChildren_ByProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareBranches_ConfirmedVouchers_CurrentAndChildren_NoOptions(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareBranches_ConfirmedVouchers_CurrentAndChildren_ByCenterProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareBranches_ConfirmedVouchers_CurrentAndChildren_NoOptions(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region ApprovedVouchers_CurrentAndChildren_NoOptions

        [Test]
        public async Task ProfitLoss_CompareBranches_ApprovedVouchers_CurrentAndChildren_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareBranches_ApprovedVouchers_CurrentAndChildren_NoOptions();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareBranches_ApprovedVouchers_CurrentAndChildren_ByCenter_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareBranches_ApprovedVouchers_CurrentAndChildren_NoOptions(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareBranches_ApprovedVouchers_CurrentAndChildren_ByProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareBranches_ApprovedVouchers_CurrentAndChildren_NoOptions(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareBranches_ApprovedVouchers_CurrentAndChildren_ByCenterProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareBranches_ApprovedVouchers_CurrentAndChildren_NoOptions(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region AllVouchers_CurrentAndChildren_NoOptions

        [Test]
        public async Task ProfitLoss_CompareBranches_AllVouchers_CurrentAndChildren_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareBranches_AllVouchers_CurrentAndChildren_NoOptions();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareBranches_AllVouchers_CurrentAndChildren_ByCenter_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareBranches_AllVouchers_CurrentAndChildren_NoOptions(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareBranches_AllVouchers_CurrentAndChildren_ByProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareBranches_AllVouchers_CurrentAndChildren_NoOptions(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareBranches_AllVouchers_CurrentAndChildren_ByCenterProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareBranches_AllVouchers_CurrentAndChildren_NoOptions(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region CheckedVouchers_CurrentBranch_ClosingOption

        [Test]
        public async Task ProfitLoss_CompareBranches_CheckedVouchers_CurrentBranch_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareBranches_CheckedVouchers_CurrentBranch_ClosingOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareBranches_CheckedVouchers_CurrentBranch_ByCenter_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareBranches_CheckedVouchers_CurrentBranch_ClosingOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareBranches_CheckedVouchers_CurrentBranch_ByProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareBranches_CheckedVouchers_CurrentBranch_ClosingOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareBranches_CheckedVouchers_CurrentBranch_ByCenterProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareBranches_CheckedVouchers_CurrentBranch_ClosingOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region FinalizedVouchers_CurrentBranch_ClosingOption

        [Test]
        public async Task ProfitLoss_CompareBranches_FinalizedVouchers_CurrentBranch_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareBranches_FinalizedVouchers_CurrentBranch_ClosingOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareBranches_FinalizedVouchers_CurrentBranch_ByCenter_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareBranches_FinalizedVouchers_CurrentBranch_ClosingOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareBranches_FinalizedVouchers_CurrentBranch_ByProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareBranches_FinalizedVouchers_CurrentBranch_ClosingOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareBranches_FinalizedVouchers_CurrentBranch_ByCenterProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareBranches_FinalizedVouchers_CurrentBranch_ClosingOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region ConfirmedVouchers_CurrentBranch_ClosingOption

        [Test]
        public async Task ProfitLoss_CompareBranches_ConfirmedVouchers_CurrentBranch_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareBranches_ConfirmedVouchers_CurrentBranch_ClosingOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareBranches_ConfirmedVouchers_CurrentBranch_ByCenter_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareBranches_ConfirmedVouchers_CurrentBranch_ClosingOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareBranches_ConfirmedVouchers_CurrentBranch_ByProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareBranches_ConfirmedVouchers_CurrentBranch_ClosingOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareBranches_ConfirmedVouchers_CurrentBranch_ByCenterProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareBranches_ConfirmedVouchers_CurrentBranch_ClosingOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region ApprovedVouchers_CurrentBranch_ClosingOption

        [Test]
        public async Task ProfitLoss_CompareBranches_ApprovedVouchers_CurrentBranch_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareBranches_ApprovedVouchers_CurrentBranch_ClosingOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareBranches_ApprovedVouchers_CurrentBranch_ByCenter_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareBranches_ApprovedVouchers_CurrentBranch_ClosingOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareBranches_ApprovedVouchers_CurrentBranch_ByProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareBranches_ApprovedVouchers_CurrentBranch_ClosingOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareBranches_ApprovedVouchers_CurrentBranch_ByCenterProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareBranches_ApprovedVouchers_CurrentBranch_ClosingOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region AllVouchers_CurrentBranch_ClosingOption

        [Test]
        public async Task ProfitLoss_CompareBranches_AllVouchers_CurrentBranch_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareBranches_AllVouchers_CurrentBranch_ClosingOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareBranches_AllVouchers_CurrentBranch_ByCenter_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareBranches_AllVouchers_CurrentBranch_ClosingOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareBranches_AllVouchers_CurrentBranch_ByProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareBranches_AllVouchers_CurrentBranch_ClosingOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareBranches_AllVouchers_CurrentBranch_ByCenterProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareBranches_AllVouchers_CurrentBranch_ClosingOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region CheckedVouchers_CurrentAndChildren_ClosingOption

        [Test]
        public async Task ProfitLoss_CompareBranches_CheckedVouchers_CurrentAndChildren_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareBranches_CheckedVouchers_CurrentAndChildren_ClosingOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareBranches_CheckedVouchers_CurrentAndChildren_ByCenter_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareBranches_CheckedVouchers_CurrentAndChildren_ClosingOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareBranches_CheckedVouchers_CurrentAndChildren_ByProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareBranches_CheckedVouchers_CurrentAndChildren_ClosingOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareBranches_CheckedVouchers_CurrentAndChildren_ByCenterProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareBranches_CheckedVouchers_CurrentAndChildren_ClosingOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region FinalizedVouchers_CurrentAndChildren_ClosingOption

        [Test]
        public async Task ProfitLoss_CompareBranches_FinalizedVouchers_CurrentAndChildren_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareBranches_FinalizedVouchers_CurrentAndChildren_ClosingOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareBranches_FinalizedVouchers_CurrentAndChildren_ByCenter_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareBranches_FinalizedVouchers_CurrentAndChildren_ClosingOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareBranches_FinalizedVouchers_CurrentAndChildren_ByProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareBranches_FinalizedVouchers_CurrentAndChildren_ClosingOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareBranches_FinalizedVouchers_CurrentAndChildren_ByCenterProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareBranches_FinalizedVouchers_CurrentAndChildren_ClosingOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region ConfirmedVouchers_CurrentAndChildren_ClosingOption

        [Test]
        public async Task ProfitLoss_CompareBranches_ConfirmedVouchers_CurrentAndChildren_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareBranches_ConfirmedVouchers_CurrentAndChildren_ClosingOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareBranches_ConfirmedVouchers_CurrentAndChildren_ByCenter_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareBranches_ConfirmedVouchers_CurrentAndChildren_ClosingOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareBranches_ConfirmedVouchers_CurrentAndChildren_ByProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareBranches_ConfirmedVouchers_CurrentAndChildren_ClosingOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareBranches_ConfirmedVouchers_CurrentAndChildren_ByCenterProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareBranches_ConfirmedVouchers_CurrentAndChildren_ClosingOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region ApprovedVouchers_CurrentAndChildren_ClosingOption

        [Test]
        public async Task ProfitLoss_CompareBranches_ApprovedVouchers_CurrentAndChildren_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareBranches_ApprovedVouchers_CurrentAndChildren_ClosingOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareBranches_ApprovedVouchers_CurrentAndChildren_ByCenter_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareBranches_ApprovedVouchers_CurrentAndChildren_ClosingOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareBranches_ApprovedVouchers_CurrentAndChildren_ByProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareBranches_ApprovedVouchers_CurrentAndChildren_ClosingOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareBranches_ApprovedVouchers_CurrentAndChildren_ByCenterProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareBranches_ApprovedVouchers_CurrentAndChildren_ClosingOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region AllVouchers_CurrentAndChildren_ClosingOption

        [Test]
        public async Task ProfitLoss_CompareBranches_AllVouchers_CurrentAndChildren_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareBranches_AllVouchers_CurrentAndChildren_ClosingOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareBranches_AllVouchers_CurrentAndChildren_ByCenter_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareBranches_AllVouchers_CurrentAndChildren_ClosingOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareBranches_AllVouchers_CurrentAndChildren_ByProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareBranches_AllVouchers_CurrentAndChildren_ClosingOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareBranches_AllVouchers_CurrentAndChildren_ByCenterProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareBranches_AllVouchers_CurrentAndChildren_ClosingOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region CheckedVouchers_CurrentBranch_TurnoverOption

        [Test]
        public async Task ProfitLoss_CompareBranches_CheckedVouchers_CurrentBranch_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareBranches_CheckedVouchers_CurrentBranch_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareBranches_CheckedVouchers_CurrentBranch_ByCenter_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareBranches_CheckedVouchers_CurrentBranch_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareBranches_CheckedVouchers_CurrentBranch_ByProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareBranches_CheckedVouchers_CurrentBranch_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareBranches_CheckedVouchers_CurrentBranch_ByCenterProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareBranches_CheckedVouchers_CurrentBranch_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region FinalizedVouchers_CurrentBranch_TurnoverOption

        [Test]
        public async Task ProfitLoss_CompareBranches_FinalizedVouchers_CurrentBranch_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareBranches_FinalizedVouchers_CurrentBranch_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareBranches_FinalizedVouchers_CurrentBranch_ByCenter_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareBranches_FinalizedVouchers_CurrentBranch_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareBranches_FinalizedVouchers_CurrentBranch_ByProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareBranches_FinalizedVouchers_CurrentBranch_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareBranches_FinalizedVouchers_CurrentBranch_ByCenterProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareBranches_FinalizedVouchers_CurrentBranch_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region ConfirmedVouchers_CurrentBranch_TurnoverOption

        [Test]
        public async Task ProfitLoss_CompareBranches_ConfirmedVouchers_CurrentBranch_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareBranches_ConfirmedVouchers_CurrentBranch_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareBranches_ConfirmedVouchers_CurrentBranch_ByCenter_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareBranches_ConfirmedVouchers_CurrentBranch_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareBranches_ConfirmedVouchers_CurrentBranch_ByProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareBranches_ConfirmedVouchers_CurrentBranch_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareBranches_ConfirmedVouchers_CurrentBranch_ByCenterProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareBranches_ConfirmedVouchers_CurrentBranch_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region ApprovedVouchers_CurrentBranch_TurnoverOption

        [Test]
        public async Task ProfitLoss_CompareBranches_ApprovedVouchers_CurrentBranch_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareBranches_ApprovedVouchers_CurrentBranch_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareBranches_ApprovedVouchers_CurrentBranch_ByCenter_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareBranches_ApprovedVouchers_CurrentBranch_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareBranches_ApprovedVouchers_CurrentBranch_ByProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareBranches_ApprovedVouchers_CurrentBranch_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareBranches_ApprovedVouchers_CurrentBranch_ByCenterProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareBranches_ApprovedVouchers_CurrentBranch_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region AllVouchers_CurrentBranch_TurnoverOption

        [Test]
        public async Task ProfitLoss_CompareBranches_AllVouchers_CurrentBranch_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareBranches_AllVouchers_CurrentBranch_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareBranches_AllVouchers_CurrentBranch_ByCenter_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareBranches_AllVouchers_CurrentBranch_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareBranches_AllVouchers_CurrentBranch_ByProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareBranches_AllVouchers_CurrentBranch_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareBranches_AllVouchers_CurrentBranch_ByCenterProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareBranches_AllVouchers_CurrentBranch_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region CheckedVouchers_CurrentAndChildren_TurnoverOption

        [Test]
        public async Task ProfitLoss_CompareBranches_CheckedVouchers_CurrentAndChildren_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareBranches_CheckedVouchers_CurrentAndChildren_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareBranches_CheckedVouchers_CurrentAndChildren_ByCenter_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareBranches_CheckedVouchers_CurrentAndChildren_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareBranches_CheckedVouchers_CurrentAndChildren_ByProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareBranches_CheckedVouchers_CurrentAndChildren_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareBranches_CheckedVouchers_CurrentAndChildren_ByCenterProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareBranches_CheckedVouchers_CurrentAndChildren_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region FinalizedVouchers_CurrentAndChildren_TurnoverOption

        [Test]
        public async Task ProfitLoss_CompareBranches_FinalizedVouchers_CurrentAndChildren_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareBranches_FinalizedVouchers_CurrentAndChildren_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareBranches_FinalizedVouchers_CurrentAndChildren_ByCenter_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareBranches_FinalizedVouchers_CurrentAndChildren_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareBranches_FinalizedVouchers_CurrentAndChildren_ByProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareBranches_FinalizedVouchers_CurrentAndChildren_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareBranches_FinalizedVouchers_CurrentAndChildren_ByCenterProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareBranches_FinalizedVouchers_CurrentAndChildren_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region ConfirmedVouchers_CurrentAndChildren_TurnoverOption

        [Test]
        public async Task ProfitLoss_CompareBranches_ConfirmedVouchers_CurrentAndChildren_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareBranches_ConfirmedVouchers_CurrentAndChildren_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareBranches_ConfirmedVouchers_CurrentAndChildren_ByCenter_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareBranches_ConfirmedVouchers_CurrentAndChildren_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareBranches_ConfirmedVouchers_CurrentAndChildren_ByProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareBranches_ConfirmedVouchers_CurrentAndChildren_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareBranches_ConfirmedVouchers_CurrentAndChildren_ByCenterProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareBranches_ConfirmedVouchers_CurrentAndChildren_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region ApprovedVouchers_CurrentAndChildren_TurnoverOption

        [Test]
        public async Task ProfitLoss_CompareBranches_ApprovedVouchers_CurrentAndChildren_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareBranches_ApprovedVouchers_CurrentAndChildren_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareBranches_ApprovedVouchers_CurrentAndChildren_ByCenter_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareBranches_ApprovedVouchers_CurrentAndChildren_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareBranches_ApprovedVouchers_CurrentAndChildren_ByProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareBranches_ApprovedVouchers_CurrentAndChildren_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareBranches_ApprovedVouchers_CurrentAndChildren_ByCenterProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareBranches_ApprovedVouchers_CurrentAndChildren_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region AllVouchers_CurrentAndChildren_TurnoverOption

        [Test]
        public async Task ProfitLoss_CompareBranches_AllVouchers_CurrentAndChildren_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareBranches_AllVouchers_CurrentAndChildren_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareBranches_AllVouchers_CurrentAndChildren_ByCenter_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareBranches_AllVouchers_CurrentAndChildren_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareBranches_AllVouchers_CurrentAndChildren_ByProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareBranches_AllVouchers_CurrentAndChildren_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareBranches_AllVouchers_CurrentAndChildren_ByCenterProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareBranches_AllVouchers_CurrentAndChildren_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region CheckedVouchers_CurrentBranch_ClosingOption_TurnoverOption

        [Test]
        public async Task ProfitLoss_CompareBranches_CheckedVouchers_CurrentBranch_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareBranches_CheckedVouchers_CurrentBranch_ClosingOption_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareBranches_CheckedVouchers_CurrentBranch_ByCenter_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareBranches_CheckedVouchers_CurrentBranch_ClosingOption_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareBranches_CheckedVouchers_CurrentBranch_ByProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareBranches_CheckedVouchers_CurrentBranch_ClosingOption_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareBranches_CheckedVouchers_CurrentBranch_ByCenterProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareBranches_CheckedVouchers_CurrentBranch_ClosingOption_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region FinalizedVouchers_CurrentBranch_ClosingOption_TurnoverOption

        [Test]
        public async Task ProfitLoss_CompareBranches_FinalizedVouchers_CurrentBranch_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareBranches_FinalizedVouchers_CurrentBranch_ClosingOption_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareBranches_FinalizedVouchers_CurrentBranch_ByCenter_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareBranches_FinalizedVouchers_CurrentBranch_ClosingOption_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareBranches_FinalizedVouchers_CurrentBranch_ByProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareBranches_FinalizedVouchers_CurrentBranch_ClosingOption_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareBranches_FinalizedVouchers_CurrentBranch_ByCenterProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareBranches_FinalizedVouchers_CurrentBranch_ClosingOption_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region ConfirmedVouchers_CurrentBranch_ClosingOption_TurnoverOption

        [Test]
        public async Task ProfitLoss_CompareBranches_ConfirmedVouchers_CurrentBranch_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareBranches_ConfirmedVouchers_CurrentBranch_ClosingOption_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareBranches_ConfirmedVouchers_CurrentBranch_ByCenter_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareBranches_ConfirmedVouchers_CurrentBranch_ClosingOption_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareBranches_ConfirmedVouchers_CurrentBranch_ByProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareBranches_ConfirmedVouchers_CurrentBranch_ClosingOption_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareBranches_ConfirmedVouchers_CurrentBranch_ByCenterProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareBranches_ConfirmedVouchers_CurrentBranch_ClosingOption_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region ApprovedVouchers_CurrentBranch_ClosingOption_TurnoverOption

        [Test]
        public async Task ProfitLoss_CompareBranches_ApprovedVouchers_CurrentBranch_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareBranches_ApprovedVouchers_CurrentBranch_ClosingOption_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareBranches_ApprovedVouchers_CurrentBranch_ByCenter_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareBranches_ApprovedVouchers_CurrentBranch_ClosingOption_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareBranches_ApprovedVouchers_CurrentBranch_ByProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareBranches_ApprovedVouchers_CurrentBranch_ClosingOption_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareBranches_ApprovedVouchers_CurrentBranch_ByCenterProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareBranches_ApprovedVouchers_CurrentBranch_ClosingOption_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region AllVouchers_CurrentBranch_ClosingOption_TurnoverOption

        [Test]
        public async Task ProfitLoss_CompareBranches_AllVouchers_CurrentBranch_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareBranches_AllVouchers_CurrentBranch_ClosingOption_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareBranches_AllVouchers_CurrentBranch_ByCenter_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareBranches_AllVouchers_CurrentBranch_ClosingOption_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareBranches_AllVouchers_CurrentBranch_ByProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareBranches_AllVouchers_CurrentBranch_ClosingOption_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareBranches_AllVouchers_CurrentBranch_ByCenterProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareBranches_AllVouchers_CurrentBranch_ClosingOption_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region CheckedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption

        [Test]
        public async Task ProfitLoss_CompareBranches_CheckedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareBranches_CheckedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareBranches_CheckedVouchers_CurrentAndChildren_ByCenter_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareBranches_CheckedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareBranches_CheckedVouchers_CurrentAndChildren_ByProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareBranches_CheckedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareBranches_CheckedVouchers_CurrentAndChildren_ByCenterProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareBranches_CheckedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region FinalizedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption

        [Test]
        public async Task ProfitLoss_CompareBranches_FinalizedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareBranches_FinalizedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareBranches_FinalizedVouchers_CurrentAndChildren_ByCenter_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareBranches_FinalizedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareBranches_FinalizedVouchers_CurrentAndChildren_ByProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareBranches_FinalizedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareBranches_FinalizedVouchers_CurrentAndChildren_ByCenterProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareBranches_FinalizedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region ConfirmedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption

        [Test]
        public async Task ProfitLoss_CompareBranches_ConfirmedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareBranches_ConfirmedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareBranches_ConfirmedVouchers_CurrentAndChildren_ByCenter_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareBranches_ConfirmedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareBranches_ConfirmedVouchers_CurrentAndChildren_ByProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareBranches_ConfirmedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareBranches_ConfirmedVouchers_CurrentAndChildren_ByCenterProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareBranches_ConfirmedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region ApprovedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption

        [Test]
        public async Task ProfitLoss_CompareBranches_ApprovedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareBranches_ApprovedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareBranches_ApprovedVouchers_CurrentAndChildren_ByCenter_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareBranches_ApprovedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareBranches_ApprovedVouchers_CurrentAndChildren_ByProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareBranches_ApprovedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareBranches_ApprovedVouchers_CurrentAndChildren_ByCenterProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareBranches_ApprovedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region AllVouchers_CurrentAndChildren_ClosingOption_TurnoverOption

        [Test]
        public async Task ProfitLoss_CompareBranches_AllVouchers_CurrentAndChildren_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareBranches_AllVouchers_CurrentAndChildren_ClosingOption_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareBranches_AllVouchers_CurrentAndChildren_ByCenter_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareBranches_AllVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareBranches_AllVouchers_CurrentAndChildren_ByProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareBranches_AllVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareBranches_AllVouchers_CurrentAndChildren_ByCenterProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareBranches_AllVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #endregion

        #region Profit-Loss By Projects Tests

        #region CheckedVouchers_CurrentBranch_NoOptions

        [Test]
        public async Task ProfitLoss_CompareProjects_CheckedVouchers_CurrentBranch_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareProjects_CheckedVouchers_CurrentBranch_NoOptions();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareProjects_CheckedVouchers_CurrentBranch_ByCenter_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareProjects_CheckedVouchers_CurrentBranch_NoOptions(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareProjects_CheckedVouchers_CurrentBranch_ByProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareProjects_CheckedVouchers_CurrentBranch_NoOptions(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareProjects_CheckedVouchers_CurrentBranch_ByCenterProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareProjects_CheckedVouchers_CurrentBranch_NoOptions(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region FinalizedVouchers_CurrentBranch_NoOptions

        [Test]
        public async Task ProfitLoss_CompareProjects_FinalizedVouchers_CurrentBranch_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareProjects_FinalizedVouchers_CurrentBranch_NoOptions();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareProjects_FinalizedVouchers_CurrentBranch_ByCenter_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareProjects_FinalizedVouchers_CurrentBranch_NoOptions(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareProjects_FinalizedVouchers_CurrentBranch_ByProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareProjects_FinalizedVouchers_CurrentBranch_NoOptions(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareProjects_FinalizedVouchers_CurrentBranch_ByCenterProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareProjects_FinalizedVouchers_CurrentBranch_NoOptions(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region ConfirmedVouchers_CurrentBranch_NoOptions

        [Test]
        public async Task ProfitLoss_CompareProjects_ConfirmedVouchers_CurrentBranch_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareProjects_ConfirmedVouchers_CurrentBranch_NoOptions();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareProjects_ConfirmedVouchers_CurrentBranch_ByCenter_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareProjects_ConfirmedVouchers_CurrentBranch_NoOptions(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareProjects_ConfirmedVouchers_CurrentBranch_ByProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareProjects_ConfirmedVouchers_CurrentBranch_NoOptions(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareProjects_ConfirmedVouchers_CurrentBranch_ByCenterProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareProjects_ConfirmedVouchers_CurrentBranch_NoOptions(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region ApprovedVouchers_CurrentBranch_NoOptions

        [Test]
        public async Task ProfitLoss_CompareProjects_ApprovedVouchers_CurrentBranch_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareProjects_ApprovedVouchers_CurrentBranch_NoOptions();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareProjects_ApprovedVouchers_CurrentBranch_ByCenter_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareProjects_ApprovedVouchers_CurrentBranch_NoOptions(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareProjects_ApprovedVouchers_CurrentBranch_ByProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareProjects_ApprovedVouchers_CurrentBranch_NoOptions(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareProjects_ApprovedVouchers_CurrentBranch_ByCenterProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareProjects_ApprovedVouchers_CurrentBranch_NoOptions(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region AllVouchers_CurrentBranch_NoOptions

        [Test]
        public async Task ProfitLoss_CompareProjects_AllVouchers_CurrentBranch_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareProjects_AllVouchers_CurrentBranch_NoOptions();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareProjects_AllVouchers_CurrentBranch_ByCenter_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareProjects_AllVouchers_CurrentBranch_NoOptions(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareProjects_AllVouchers_CurrentBranch_ByProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareProjects_AllVouchers_CurrentBranch_NoOptions(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareProjects_AllVouchers_CurrentBranch_ByCenterProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareProjects_AllVouchers_CurrentBranch_NoOptions(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region CheckedVouchers_CurrentAndChildren_NoOptions

        [Test]
        public async Task ProfitLoss_CompareProjects_CheckedVouchers_CurrentAndChildren_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareProjects_CheckedVouchers_CurrentAndChildren_NoOptions();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareProjects_CheckedVouchers_CurrentAndChildren_ByCenter_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareProjects_CheckedVouchers_CurrentAndChildren_NoOptions(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareProjects_CheckedVouchers_CurrentAndChildren_ByProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareProjects_CheckedVouchers_CurrentAndChildren_NoOptions(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareProjects_CheckedVouchers_CurrentAndChildren_ByCenterProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareProjects_CheckedVouchers_CurrentAndChildren_NoOptions(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region FinalizedVouchers_CurrentAndChildren_NoOptions

        [Test]
        public async Task ProfitLoss_CompareProjects_FinalizedVouchers_CurrentAndChildren_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareProjects_FinalizedVouchers_CurrentAndChildren_NoOptions();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareProjects_FinalizedVouchers_CurrentAndChildren_ByCenter_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareProjects_FinalizedVouchers_CurrentAndChildren_NoOptions(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareProjects_FinalizedVouchers_CurrentAndChildren_ByProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareProjects_FinalizedVouchers_CurrentAndChildren_NoOptions(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareProjects_FinalizedVouchers_CurrentAndChildren_ByCenterProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareProjects_FinalizedVouchers_CurrentAndChildren_NoOptions(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region ConfirmedVouchers_CurrentAndChildren_NoOptions

        [Test]
        public async Task ProfitLoss_CompareProjects_ConfirmedVouchers_CurrentAndChildren_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareProjects_ConfirmedVouchers_CurrentAndChildren_NoOptions();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareProjects_ConfirmedVouchers_CurrentAndChildren_ByCenter_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareProjects_ConfirmedVouchers_CurrentAndChildren_NoOptions(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareProjects_ConfirmedVouchers_CurrentAndChildren_ByProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareProjects_ConfirmedVouchers_CurrentAndChildren_NoOptions(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareProjects_ConfirmedVouchers_CurrentAndChildren_ByCenterProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareProjects_ConfirmedVouchers_CurrentAndChildren_NoOptions(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region ApprovedVouchers_CurrentAndChildren_NoOptions

        [Test]
        public async Task ProfitLoss_CompareProjects_ApprovedVouchers_CurrentAndChildren_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareProjects_ApprovedVouchers_CurrentAndChildren_NoOptions();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareProjects_ApprovedVouchers_CurrentAndChildren_ByCenter_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareProjects_ApprovedVouchers_CurrentAndChildren_NoOptions(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareProjects_ApprovedVouchers_CurrentAndChildren_ByProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareProjects_ApprovedVouchers_CurrentAndChildren_NoOptions(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareProjects_ApprovedVouchers_CurrentAndChildren_ByCenterProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareProjects_ApprovedVouchers_CurrentAndChildren_NoOptions(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region AllVouchers_CurrentAndChildren_NoOptions

        [Test]
        public async Task ProfitLoss_CompareProjects_AllVouchers_CurrentAndChildren_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareProjects_AllVouchers_CurrentAndChildren_NoOptions();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareProjects_AllVouchers_CurrentAndChildren_ByCenter_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareProjects_AllVouchers_CurrentAndChildren_NoOptions(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareProjects_AllVouchers_CurrentAndChildren_ByProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareProjects_AllVouchers_CurrentAndChildren_NoOptions(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareProjects_AllVouchers_CurrentAndChildren_ByCenterProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareProjects_AllVouchers_CurrentAndChildren_NoOptions(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region CheckedVouchers_CurrentBranch_ClosingOption

        [Test]
        public async Task ProfitLoss_CompareProjects_CheckedVouchers_CurrentBranch_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareProjects_CheckedVouchers_CurrentBranch_ClosingOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareProjects_CheckedVouchers_CurrentBranch_ByCenter_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareProjects_CheckedVouchers_CurrentBranch_ClosingOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareProjects_CheckedVouchers_CurrentBranch_ByProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareProjects_CheckedVouchers_CurrentBranch_ClosingOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareProjects_CheckedVouchers_CurrentBranch_ByCenterProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareProjects_CheckedVouchers_CurrentBranch_ClosingOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region FinalizedVouchers_CurrentBranch_ClosingOption

        [Test]
        public async Task ProfitLoss_CompareProjects_FinalizedVouchers_CurrentBranch_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareProjects_FinalizedVouchers_CurrentBranch_ClosingOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareProjects_FinalizedVouchers_CurrentBranch_ByCenter_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareProjects_FinalizedVouchers_CurrentBranch_ClosingOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareProjects_FinalizedVouchers_CurrentBranch_ByProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareProjects_FinalizedVouchers_CurrentBranch_ClosingOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareProjects_FinalizedVouchers_CurrentBranch_ByCenterProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareProjects_FinalizedVouchers_CurrentBranch_ClosingOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region ConfirmedVouchers_CurrentBranch_ClosingOption

        [Test]
        public async Task ProfitLoss_CompareProjects_ConfirmedVouchers_CurrentBranch_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareProjects_ConfirmedVouchers_CurrentBranch_ClosingOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareProjects_ConfirmedVouchers_CurrentBranch_ByCenter_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareProjects_ConfirmedVouchers_CurrentBranch_ClosingOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareProjects_ConfirmedVouchers_CurrentBranch_ByProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareProjects_ConfirmedVouchers_CurrentBranch_ClosingOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareProjects_ConfirmedVouchers_CurrentBranch_ByCenterProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareProjects_ConfirmedVouchers_CurrentBranch_ClosingOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region ApprovedVouchers_CurrentBranch_ClosingOption

        [Test]
        public async Task ProfitLoss_CompareProjects_ApprovedVouchers_CurrentBranch_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareProjects_ApprovedVouchers_CurrentBranch_ClosingOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareProjects_ApprovedVouchers_CurrentBranch_ByCenter_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareProjects_ApprovedVouchers_CurrentBranch_ClosingOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareProjects_ApprovedVouchers_CurrentBranch_ByProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareProjects_ApprovedVouchers_CurrentBranch_ClosingOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareProjects_ApprovedVouchers_CurrentBranch_ByCenterProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareProjects_ApprovedVouchers_CurrentBranch_ClosingOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region AllVouchers_CurrentBranch_ClosingOption

        [Test]
        public async Task ProfitLoss_CompareProjects_AllVouchers_CurrentBranch_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareProjects_AllVouchers_CurrentBranch_ClosingOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareProjects_AllVouchers_CurrentBranch_ByCenter_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareProjects_AllVouchers_CurrentBranch_ClosingOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareProjects_AllVouchers_CurrentBranch_ByProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareProjects_AllVouchers_CurrentBranch_ClosingOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareProjects_AllVouchers_CurrentBranch_ByCenterProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareProjects_AllVouchers_CurrentBranch_ClosingOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region CheckedVouchers_CurrentAndChildren_ClosingOption

        [Test]
        public async Task ProfitLoss_CompareProjects_CheckedVouchers_CurrentAndChildren_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareProjects_CheckedVouchers_CurrentAndChildren_ClosingOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareProjects_CheckedVouchers_CurrentAndChildren_ByCenter_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareProjects_CheckedVouchers_CurrentAndChildren_ClosingOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareProjects_CheckedVouchers_CurrentAndChildren_ByProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareProjects_CheckedVouchers_CurrentAndChildren_ClosingOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareProjects_CheckedVouchers_CurrentAndChildren_ByCenterProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareProjects_CheckedVouchers_CurrentAndChildren_ClosingOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region FinalizedVouchers_CurrentAndChildren_ClosingOption

        [Test]
        public async Task ProfitLoss_CompareProjects_FinalizedVouchers_CurrentAndChildren_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareProjects_FinalizedVouchers_CurrentAndChildren_ClosingOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareProjects_FinalizedVouchers_CurrentAndChildren_ByCenter_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareProjects_FinalizedVouchers_CurrentAndChildren_ClosingOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareProjects_FinalizedVouchers_CurrentAndChildren_ByProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareProjects_FinalizedVouchers_CurrentAndChildren_ClosingOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareProjects_FinalizedVouchers_CurrentAndChildren_ByCenterProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareProjects_FinalizedVouchers_CurrentAndChildren_ClosingOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region ConfirmedVouchers_CurrentAndChildren_ClosingOption

        [Test]
        public async Task ProfitLoss_CompareProjects_ConfirmedVouchers_CurrentAndChildren_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareProjects_ConfirmedVouchers_CurrentAndChildren_ClosingOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareProjects_ConfirmedVouchers_CurrentAndChildren_ByCenter_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareProjects_ConfirmedVouchers_CurrentAndChildren_ClosingOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareProjects_ConfirmedVouchers_CurrentAndChildren_ByProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareProjects_ConfirmedVouchers_CurrentAndChildren_ClosingOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareProjects_ConfirmedVouchers_CurrentAndChildren_ByCenterProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareProjects_ConfirmedVouchers_CurrentAndChildren_ClosingOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region ApprovedVouchers_CurrentAndChildren_ClosingOption

        [Test]
        public async Task ProfitLoss_CompareProjects_ApprovedVouchers_CurrentAndChildren_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareProjects_ApprovedVouchers_CurrentAndChildren_ClosingOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareProjects_ApprovedVouchers_CurrentAndChildren_ByCenter_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareProjects_ApprovedVouchers_CurrentAndChildren_ClosingOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareProjects_ApprovedVouchers_CurrentAndChildren_ByProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareProjects_ApprovedVouchers_CurrentAndChildren_ClosingOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareProjects_ApprovedVouchers_CurrentAndChildren_ByCenterProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareProjects_ApprovedVouchers_CurrentAndChildren_ClosingOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region AllVouchers_CurrentAndChildren_ClosingOption

        [Test]
        public async Task ProfitLoss_CompareProjects_AllVouchers_CurrentAndChildren_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareProjects_AllVouchers_CurrentAndChildren_ClosingOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareProjects_AllVouchers_CurrentAndChildren_ByCenter_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareProjects_AllVouchers_CurrentAndChildren_ClosingOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareProjects_AllVouchers_CurrentAndChildren_ByProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareProjects_AllVouchers_CurrentAndChildren_ClosingOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareProjects_AllVouchers_CurrentAndChildren_ByCenterProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareProjects_AllVouchers_CurrentAndChildren_ClosingOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region CheckedVouchers_CurrentBranch_TurnoverOption

        [Test]
        public async Task ProfitLoss_CompareProjects_CheckedVouchers_CurrentBranch_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareProjects_CheckedVouchers_CurrentBranch_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareProjects_CheckedVouchers_CurrentBranch_ByCenter_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareProjects_CheckedVouchers_CurrentBranch_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareProjects_CheckedVouchers_CurrentBranch_ByProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareProjects_CheckedVouchers_CurrentBranch_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareProjects_CheckedVouchers_CurrentBranch_ByCenterProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareProjects_CheckedVouchers_CurrentBranch_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region FinalizedVouchers_CurrentBranch_TurnoverOption

        [Test]
        public async Task ProfitLoss_CompareProjects_FinalizedVouchers_CurrentBranch_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareProjects_FinalizedVouchers_CurrentBranch_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareProjects_FinalizedVouchers_CurrentBranch_ByCenter_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareProjects_FinalizedVouchers_CurrentBranch_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareProjects_FinalizedVouchers_CurrentBranch_ByProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareProjects_FinalizedVouchers_CurrentBranch_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareProjects_FinalizedVouchers_CurrentBranch_ByCenterProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareProjects_FinalizedVouchers_CurrentBranch_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region ConfirmedVouchers_CurrentBranch_TurnoverOption

        [Test]
        public async Task ProfitLoss_CompareProjects_ConfirmedVouchers_CurrentBranch_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareProjects_ConfirmedVouchers_CurrentBranch_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareProjects_ConfirmedVouchers_CurrentBranch_ByCenter_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareProjects_ConfirmedVouchers_CurrentBranch_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareProjects_ConfirmedVouchers_CurrentBranch_ByProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareProjects_ConfirmedVouchers_CurrentBranch_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareProjects_ConfirmedVouchers_CurrentBranch_ByCenterProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareProjects_ConfirmedVouchers_CurrentBranch_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region ApprovedVouchers_CurrentBranch_TurnoverOption

        [Test]
        public async Task ProfitLoss_CompareProjects_ApprovedVouchers_CurrentBranch_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareProjects_ApprovedVouchers_CurrentBranch_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareProjects_ApprovedVouchers_CurrentBranch_ByCenter_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareProjects_ApprovedVouchers_CurrentBranch_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareProjects_ApprovedVouchers_CurrentBranch_ByProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareProjects_ApprovedVouchers_CurrentBranch_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareProjects_ApprovedVouchers_CurrentBranch_ByCenterProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareProjects_ApprovedVouchers_CurrentBranch_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region AllVouchers_CurrentBranch_TurnoverOption

        [Test]
        public async Task ProfitLoss_CompareProjects_AllVouchers_CurrentBranch_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareProjects_AllVouchers_CurrentBranch_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareProjects_AllVouchers_CurrentBranch_ByCenter_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareProjects_AllVouchers_CurrentBranch_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareProjects_AllVouchers_CurrentBranch_ByProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareProjects_AllVouchers_CurrentBranch_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareProjects_AllVouchers_CurrentBranch_ByCenterProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareProjects_AllVouchers_CurrentBranch_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region CheckedVouchers_CurrentAndChildren_TurnoverOption

        [Test]
        public async Task ProfitLoss_CompareProjects_CheckedVouchers_CurrentAndChildren_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareProjects_CheckedVouchers_CurrentAndChildren_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareProjects_CheckedVouchers_CurrentAndChildren_ByCenter_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareProjects_CheckedVouchers_CurrentAndChildren_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareProjects_CheckedVouchers_CurrentAndChildren_ByProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareProjects_CheckedVouchers_CurrentAndChildren_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareProjects_CheckedVouchers_CurrentAndChildren_ByCenterProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareProjects_CheckedVouchers_CurrentAndChildren_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region FinalizedVouchers_CurrentAndChildren_TurnoverOption

        [Test]
        public async Task ProfitLoss_CompareProjects_FinalizedVouchers_CurrentAndChildren_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareProjects_FinalizedVouchers_CurrentAndChildren_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareProjects_FinalizedVouchers_CurrentAndChildren_ByCenter_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareProjects_FinalizedVouchers_CurrentAndChildren_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareProjects_FinalizedVouchers_CurrentAndChildren_ByProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareProjects_FinalizedVouchers_CurrentAndChildren_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareProjects_FinalizedVouchers_CurrentAndChildren_ByCenterProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareProjects_FinalizedVouchers_CurrentAndChildren_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region ConfirmedVouchers_CurrentAndChildren_TurnoverOption

        [Test]
        public async Task ProfitLoss_CompareProjects_ConfirmedVouchers_CurrentAndChildren_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareProjects_ConfirmedVouchers_CurrentAndChildren_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareProjects_ConfirmedVouchers_CurrentAndChildren_ByCenter_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareProjects_ConfirmedVouchers_CurrentAndChildren_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareProjects_ConfirmedVouchers_CurrentAndChildren_ByProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareProjects_ConfirmedVouchers_CurrentAndChildren_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareProjects_ConfirmedVouchers_CurrentAndChildren_ByCenterProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareProjects_ConfirmedVouchers_CurrentAndChildren_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region ApprovedVouchers_CurrentAndChildren_TurnoverOption

        [Test]
        public async Task ProfitLoss_CompareProjects_ApprovedVouchers_CurrentAndChildren_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareProjects_ApprovedVouchers_CurrentAndChildren_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareProjects_ApprovedVouchers_CurrentAndChildren_ByCenter_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareProjects_ApprovedVouchers_CurrentAndChildren_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareProjects_ApprovedVouchers_CurrentAndChildren_ByProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareProjects_ApprovedVouchers_CurrentAndChildren_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareProjects_ApprovedVouchers_CurrentAndChildren_ByCenterProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareProjects_ApprovedVouchers_CurrentAndChildren_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region AllVouchers_CurrentAndChildren_TurnoverOption

        [Test]
        public async Task ProfitLoss_CompareProjects_AllVouchers_CurrentAndChildren_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareProjects_AllVouchers_CurrentAndChildren_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareProjects_AllVouchers_CurrentAndChildren_ByCenter_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareProjects_AllVouchers_CurrentAndChildren_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareProjects_AllVouchers_CurrentAndChildren_ByProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareProjects_AllVouchers_CurrentAndChildren_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareProjects_AllVouchers_CurrentAndChildren_ByCenterProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareProjects_AllVouchers_CurrentAndChildren_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region CheckedVouchers_CurrentBranch_ClosingOption_TurnoverOption

        [Test]
        public async Task ProfitLoss_CompareProjects_CheckedVouchers_CurrentBranch_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareProjects_CheckedVouchers_CurrentBranch_ClosingOption_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareProjects_CheckedVouchers_CurrentBranch_ByCenter_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareProjects_CheckedVouchers_CurrentBranch_ClosingOption_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareProjects_CheckedVouchers_CurrentBranch_ByProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareProjects_CheckedVouchers_CurrentBranch_ClosingOption_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareProjects_CheckedVouchers_CurrentBranch_ByCenterProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareProjects_CheckedVouchers_CurrentBranch_ClosingOption_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region FinalizedVouchers_CurrentBranch_ClosingOption_TurnoverOption

        [Test]
        public async Task ProfitLoss_CompareProjects_FinalizedVouchers_CurrentBranch_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareProjects_FinalizedVouchers_CurrentBranch_ClosingOption_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareProjects_FinalizedVouchers_CurrentBranch_ByCenter_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareProjects_FinalizedVouchers_CurrentBranch_ClosingOption_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareProjects_FinalizedVouchers_CurrentBranch_ByProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareProjects_FinalizedVouchers_CurrentBranch_ClosingOption_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareProjects_FinalizedVouchers_CurrentBranch_ByCenterProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareProjects_FinalizedVouchers_CurrentBranch_ClosingOption_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region ConfirmedVouchers_CurrentBranch_ClosingOption_TurnoverOption

        [Test]
        public async Task ProfitLoss_CompareProjects_ConfirmedVouchers_CurrentBranch_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareProjects_ConfirmedVouchers_CurrentBranch_ClosingOption_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareProjects_ConfirmedVouchers_CurrentBranch_ByCenter_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareProjects_ConfirmedVouchers_CurrentBranch_ClosingOption_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareProjects_ConfirmedVouchers_CurrentBranch_ByProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareProjects_ConfirmedVouchers_CurrentBranch_ClosingOption_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareProjects_ConfirmedVouchers_CurrentBranch_ByCenterProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareProjects_ConfirmedVouchers_CurrentBranch_ClosingOption_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region ApprovedVouchers_CurrentBranch_ClosingOption_TurnoverOption

        [Test]
        public async Task ProfitLoss_CompareProjects_ApprovedVouchers_CurrentBranch_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareProjects_ApprovedVouchers_CurrentBranch_ClosingOption_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareProjects_ApprovedVouchers_CurrentBranch_ByCenter_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareProjects_ApprovedVouchers_CurrentBranch_ClosingOption_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareProjects_ApprovedVouchers_CurrentBranch_ByProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareProjects_ApprovedVouchers_CurrentBranch_ClosingOption_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareProjects_ApprovedVouchers_CurrentBranch_ByCenterProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareProjects_ApprovedVouchers_CurrentBranch_ClosingOption_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region AllVouchers_CurrentBranch_ClosingOption_TurnoverOption

        [Test]
        public async Task ProfitLoss_CompareProjects_AllVouchers_CurrentBranch_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareProjects_AllVouchers_CurrentBranch_ClosingOption_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareProjects_AllVouchers_CurrentBranch_ByCenter_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareProjects_AllVouchers_CurrentBranch_ClosingOption_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareProjects_AllVouchers_CurrentBranch_ByProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareProjects_AllVouchers_CurrentBranch_ClosingOption_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareProjects_AllVouchers_CurrentBranch_ByCenterProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareProjects_AllVouchers_CurrentBranch_ClosingOption_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region CheckedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption

        [Test]
        public async Task ProfitLoss_CompareProjects_CheckedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareProjects_CheckedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareProjects_CheckedVouchers_CurrentAndChildren_ByCenter_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareProjects_CheckedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareProjects_CheckedVouchers_CurrentAndChildren_ByProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareProjects_CheckedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareProjects_CheckedVouchers_CurrentAndChildren_ByCenterProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareProjects_CheckedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region FinalizedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption

        [Test]
        public async Task ProfitLoss_CompareProjects_FinalizedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareProjects_FinalizedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareProjects_FinalizedVouchers_CurrentAndChildren_ByCenter_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareProjects_FinalizedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareProjects_FinalizedVouchers_CurrentAndChildren_ByProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareProjects_FinalizedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareProjects_FinalizedVouchers_CurrentAndChildren_ByCenterProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareProjects_FinalizedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region ConfirmedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption

        [Test]
        public async Task ProfitLoss_CompareProjects_ConfirmedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareProjects_ConfirmedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareProjects_ConfirmedVouchers_CurrentAndChildren_ByCenter_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareProjects_ConfirmedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareProjects_ConfirmedVouchers_CurrentAndChildren_ByProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareProjects_ConfirmedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareProjects_ConfirmedVouchers_CurrentAndChildren_ByCenterProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareProjects_ConfirmedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region ApprovedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption

        [Test]
        public async Task ProfitLoss_CompareProjects_ApprovedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareProjects_ApprovedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareProjects_ApprovedVouchers_CurrentAndChildren_ByCenter_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareProjects_ApprovedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareProjects_ApprovedVouchers_CurrentAndChildren_ByProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareProjects_ApprovedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareProjects_ApprovedVouchers_CurrentAndChildren_ByCenterProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareProjects_ApprovedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region AllVouchers_CurrentAndChildren_ClosingOption_TurnoverOption

        [Test]
        public async Task ProfitLoss_CompareProjects_AllVouchers_CurrentAndChildren_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareProjects_AllVouchers_CurrentAndChildren_ClosingOption_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareProjects_AllVouchers_CurrentAndChildren_ByCenter_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareProjects_AllVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareProjects_AllVouchers_CurrentAndChildren_ByProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareProjects_AllVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareProjects_AllVouchers_CurrentAndChildren_ByCenterProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareProjects_AllVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #endregion

        #region Profit-Loss By Cost Centers Tests

        #region CheckedVouchers_CurrentBranch_NoOptions

        [Test]
        public async Task ProfitLoss_CompareCostCenters_CheckedVouchers_CurrentBranch_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareCostCenters_CheckedVouchers_CurrentBranch_NoOptions();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareCostCenters_CheckedVouchers_CurrentBranch_ByCenter_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareCostCenters_CheckedVouchers_CurrentBranch_NoOptions(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareCostCenters_CheckedVouchers_CurrentBranch_ByProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareCostCenters_CheckedVouchers_CurrentBranch_NoOptions(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareCostCenters_CheckedVouchers_CurrentBranch_ByCenterProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareCostCenters_CheckedVouchers_CurrentBranch_NoOptions(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region FinalizedVouchers_CurrentBranch_NoOptions

        [Test]
        public async Task ProfitLoss_CompareCostCenters_FinalizedVouchers_CurrentBranch_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareCostCenters_FinalizedVouchers_CurrentBranch_NoOptions();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareCostCenters_FinalizedVouchers_CurrentBranch_ByCenter_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareCostCenters_FinalizedVouchers_CurrentBranch_NoOptions(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareCostCenters_FinalizedVouchers_CurrentBranch_ByProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareCostCenters_FinalizedVouchers_CurrentBranch_NoOptions(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareCostCenters_FinalizedVouchers_CurrentBranch_ByCenterProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareCostCenters_FinalizedVouchers_CurrentBranch_NoOptions(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region ConfirmedVouchers_CurrentBranch_NoOptions

        [Test]
        public async Task ProfitLoss_CompareCostCenters_ConfirmedVouchers_CurrentBranch_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareCostCenters_ConfirmedVouchers_CurrentBranch_NoOptions();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareCostCenters_ConfirmedVouchers_CurrentBranch_ByCenter_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareCostCenters_ConfirmedVouchers_CurrentBranch_NoOptions(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareCostCenters_ConfirmedVouchers_CurrentBranch_ByProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareCostCenters_ConfirmedVouchers_CurrentBranch_NoOptions(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareCostCenters_ConfirmedVouchers_CurrentBranch_ByCenterProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareCostCenters_ConfirmedVouchers_CurrentBranch_NoOptions(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region ApprovedVouchers_CurrentBranch_NoOptions

        [Test]
        public async Task ProfitLoss_CompareCostCenters_ApprovedVouchers_CurrentBranch_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareCostCenters_ApprovedVouchers_CurrentBranch_NoOptions();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareCostCenters_ApprovedVouchers_CurrentBranch_ByCenter_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareCostCenters_ApprovedVouchers_CurrentBranch_NoOptions(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareCostCenters_ApprovedVouchers_CurrentBranch_ByProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareCostCenters_ApprovedVouchers_CurrentBranch_NoOptions(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareCostCenters_ApprovedVouchers_CurrentBranch_ByCenterProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareCostCenters_ApprovedVouchers_CurrentBranch_NoOptions(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region AllVouchers_CurrentBranch_NoOptions

        [Test]
        public async Task ProfitLoss_CompareCostCenters_AllVouchers_CurrentBranch_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareCostCenters_AllVouchers_CurrentBranch_NoOptions();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareCostCenters_AllVouchers_CurrentBranch_ByCenter_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareCostCenters_AllVouchers_CurrentBranch_NoOptions(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareCostCenters_AllVouchers_CurrentBranch_ByProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareCostCenters_AllVouchers_CurrentBranch_NoOptions(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareCostCenters_AllVouchers_CurrentBranch_ByCenterProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareCostCenters_AllVouchers_CurrentBranch_NoOptions(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region CheckedVouchers_CurrentAndChildren_NoOptions

        [Test]
        public async Task ProfitLoss_CompareCostCenters_CheckedVouchers_CurrentAndChildren_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareCostCenters_CheckedVouchers_CurrentAndChildren_NoOptions();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareCostCenters_CheckedVouchers_CurrentAndChildren_ByCenter_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareCostCenters_CheckedVouchers_CurrentAndChildren_NoOptions(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareCostCenters_CheckedVouchers_CurrentAndChildren_ByProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareCostCenters_CheckedVouchers_CurrentAndChildren_NoOptions(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareCostCenters_CheckedVouchers_CurrentAndChildren_ByCenterProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareCostCenters_CheckedVouchers_CurrentAndChildren_NoOptions(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region FinalizedVouchers_CurrentAndChildren_NoOptions

        [Test]
        public async Task ProfitLoss_CompareCostCenters_FinalizedVouchers_CurrentAndChildren_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareCostCenters_FinalizedVouchers_CurrentAndChildren_NoOptions();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareCostCenters_FinalizedVouchers_CurrentAndChildren_ByCenter_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareCostCenters_FinalizedVouchers_CurrentAndChildren_NoOptions(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareCostCenters_FinalizedVouchers_CurrentAndChildren_ByProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareCostCenters_FinalizedVouchers_CurrentAndChildren_NoOptions(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareCostCenters_FinalizedVouchers_CurrentAndChildren_ByCenterProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareCostCenters_FinalizedVouchers_CurrentAndChildren_NoOptions(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region ConfirmedVouchers_CurrentAndChildren_NoOptions

        [Test]
        public async Task ProfitLoss_CompareCostCenters_ConfirmedVouchers_CurrentAndChildren_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareCostCenters_ConfirmedVouchers_CurrentAndChildren_NoOptions();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareCostCenters_ConfirmedVouchers_CurrentAndChildren_ByCenter_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareCostCenters_ConfirmedVouchers_CurrentAndChildren_NoOptions(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareCostCenters_ConfirmedVouchers_CurrentAndChildren_ByProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareCostCenters_ConfirmedVouchers_CurrentAndChildren_NoOptions(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareCostCenters_ConfirmedVouchers_CurrentAndChildren_ByCenterProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareCostCenters_ConfirmedVouchers_CurrentAndChildren_NoOptions(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region ApprovedVouchers_CurrentAndChildren_NoOptions

        [Test]
        public async Task ProfitLoss_CompareCostCenters_ApprovedVouchers_CurrentAndChildren_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareCostCenters_ApprovedVouchers_CurrentAndChildren_NoOptions();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareCostCenters_ApprovedVouchers_CurrentAndChildren_ByCenter_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareCostCenters_ApprovedVouchers_CurrentAndChildren_NoOptions(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareCostCenters_ApprovedVouchers_CurrentAndChildren_ByProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareCostCenters_ApprovedVouchers_CurrentAndChildren_NoOptions(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareCostCenters_ApprovedVouchers_CurrentAndChildren_ByCenterProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareCostCenters_ApprovedVouchers_CurrentAndChildren_NoOptions(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region AllVouchers_CurrentAndChildren_NoOptions

        [Test]
        public async Task ProfitLoss_CompareCostCenters_AllVouchers_CurrentAndChildren_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareCostCenters_AllVouchers_CurrentAndChildren_NoOptions();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareCostCenters_AllVouchers_CurrentAndChildren_ByCenter_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareCostCenters_AllVouchers_CurrentAndChildren_NoOptions(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareCostCenters_AllVouchers_CurrentAndChildren_ByProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareCostCenters_AllVouchers_CurrentAndChildren_NoOptions(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareCostCenters_AllVouchers_CurrentAndChildren_ByCenterProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareCostCenters_AllVouchers_CurrentAndChildren_NoOptions(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region CheckedVouchers_CurrentBranch_ClosingOption

        [Test]
        public async Task ProfitLoss_CompareCostCenters_CheckedVouchers_CurrentBranch_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareCostCenters_CheckedVouchers_CurrentBranch_ClosingOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareCostCenters_CheckedVouchers_CurrentBranch_ByCenter_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareCostCenters_CheckedVouchers_CurrentBranch_ClosingOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareCostCenters_CheckedVouchers_CurrentBranch_ByProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareCostCenters_CheckedVouchers_CurrentBranch_ClosingOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareCostCenters_CheckedVouchers_CurrentBranch_ByCenterProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareCostCenters_CheckedVouchers_CurrentBranch_ClosingOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region FinalizedVouchers_CurrentBranch_ClosingOption

        [Test]
        public async Task ProfitLoss_CompareCostCenters_FinalizedVouchers_CurrentBranch_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareCostCenters_FinalizedVouchers_CurrentBranch_ClosingOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareCostCenters_FinalizedVouchers_CurrentBranch_ByCenter_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareCostCenters_FinalizedVouchers_CurrentBranch_ClosingOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareCostCenters_FinalizedVouchers_CurrentBranch_ByProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareCostCenters_FinalizedVouchers_CurrentBranch_ClosingOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareCostCenters_FinalizedVouchers_CurrentBranch_ByCenterProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareCostCenters_FinalizedVouchers_CurrentBranch_ClosingOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region ConfirmedVouchers_CurrentBranch_ClosingOption

        [Test]
        public async Task ProfitLoss_CompareCostCenters_ConfirmedVouchers_CurrentBranch_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareCostCenters_ConfirmedVouchers_CurrentBranch_ClosingOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareCostCenters_ConfirmedVouchers_CurrentBranch_ByCenter_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareCostCenters_ConfirmedVouchers_CurrentBranch_ClosingOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareCostCenters_ConfirmedVouchers_CurrentBranch_ByProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareCostCenters_ConfirmedVouchers_CurrentBranch_ClosingOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareCostCenters_ConfirmedVouchers_CurrentBranch_ByCenterProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareCostCenters_ConfirmedVouchers_CurrentBranch_ClosingOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region ApprovedVouchers_CurrentBranch_ClosingOption

        [Test]
        public async Task ProfitLoss_CompareCostCenters_ApprovedVouchers_CurrentBranch_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareCostCenters_ApprovedVouchers_CurrentBranch_ClosingOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareCostCenters_ApprovedVouchers_CurrentBranch_ByCenter_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareCostCenters_ApprovedVouchers_CurrentBranch_ClosingOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareCostCenters_ApprovedVouchers_CurrentBranch_ByProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareCostCenters_ApprovedVouchers_CurrentBranch_ClosingOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareCostCenters_ApprovedVouchers_CurrentBranch_ByCenterProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareCostCenters_ApprovedVouchers_CurrentBranch_ClosingOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region AllVouchers_CurrentBranch_ClosingOption

        [Test]
        public async Task ProfitLoss_CompareCostCenters_AllVouchers_CurrentBranch_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareCostCenters_AllVouchers_CurrentBranch_ClosingOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareCostCenters_AllVouchers_CurrentBranch_ByCenter_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareCostCenters_AllVouchers_CurrentBranch_ClosingOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareCostCenters_AllVouchers_CurrentBranch_ByProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareCostCenters_AllVouchers_CurrentBranch_ClosingOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareCostCenters_AllVouchers_CurrentBranch_ByCenterProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareCostCenters_AllVouchers_CurrentBranch_ClosingOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region CheckedVouchers_CurrentAndChildren_ClosingOption

        [Test]
        public async Task ProfitLoss_CompareCostCenters_CheckedVouchers_CurrentAndChildren_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareCostCenters_CheckedVouchers_CurrentAndChildren_ClosingOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareCostCenters_CheckedVouchers_CurrentAndChildren_ByCenter_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareCostCenters_CheckedVouchers_CurrentAndChildren_ClosingOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareCostCenters_CheckedVouchers_CurrentAndChildren_ByProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareCostCenters_CheckedVouchers_CurrentAndChildren_ClosingOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareCostCenters_CheckedVouchers_CurrentAndChildren_ByCenterProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareCostCenters_CheckedVouchers_CurrentAndChildren_ClosingOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region FinalizedVouchers_CurrentAndChildren_ClosingOption

        [Test]
        public async Task ProfitLoss_CompareCostCenters_FinalizedVouchers_CurrentAndChildren_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareCostCenters_FinalizedVouchers_CurrentAndChildren_ClosingOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareCostCenters_FinalizedVouchers_CurrentAndChildren_ByCenter_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareCostCenters_FinalizedVouchers_CurrentAndChildren_ClosingOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareCostCenters_FinalizedVouchers_CurrentAndChildren_ByProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareCostCenters_FinalizedVouchers_CurrentAndChildren_ClosingOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareCostCenters_FinalizedVouchers_CurrentAndChildren_ByCenterProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareCostCenters_FinalizedVouchers_CurrentAndChildren_ClosingOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region ConfirmedVouchers_CurrentAndChildren_ClosingOption

        [Test]
        public async Task ProfitLoss_CompareCostCenters_ConfirmedVouchers_CurrentAndChildren_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareCostCenters_ConfirmedVouchers_CurrentAndChildren_ClosingOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareCostCenters_ConfirmedVouchers_CurrentAndChildren_ByCenter_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareCostCenters_ConfirmedVouchers_CurrentAndChildren_ClosingOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareCostCenters_ConfirmedVouchers_CurrentAndChildren_ByProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareCostCenters_ConfirmedVouchers_CurrentAndChildren_ClosingOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareCostCenters_ConfirmedVouchers_CurrentAndChildren_ByCenterProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareCostCenters_ConfirmedVouchers_CurrentAndChildren_ClosingOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region ApprovedVouchers_CurrentAndChildren_ClosingOption

        [Test]
        public async Task ProfitLoss_CompareCostCenters_ApprovedVouchers_CurrentAndChildren_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareCostCenters_ApprovedVouchers_CurrentAndChildren_ClosingOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareCostCenters_ApprovedVouchers_CurrentAndChildren_ByCenter_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareCostCenters_ApprovedVouchers_CurrentAndChildren_ClosingOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareCostCenters_ApprovedVouchers_CurrentAndChildren_ByProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareCostCenters_ApprovedVouchers_CurrentAndChildren_ClosingOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareCostCenters_ApprovedVouchers_CurrentAndChildren_ByCenterProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareCostCenters_ApprovedVouchers_CurrentAndChildren_ClosingOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region AllVouchers_CurrentAndChildren_ClosingOption

        [Test]
        public async Task ProfitLoss_CompareCostCenters_AllVouchers_CurrentAndChildren_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareCostCenters_AllVouchers_CurrentAndChildren_ClosingOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareCostCenters_AllVouchers_CurrentAndChildren_ByCenter_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareCostCenters_AllVouchers_CurrentAndChildren_ClosingOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareCostCenters_AllVouchers_CurrentAndChildren_ByProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareCostCenters_AllVouchers_CurrentAndChildren_ClosingOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareCostCenters_AllVouchers_CurrentAndChildren_ByCenterProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareCostCenters_AllVouchers_CurrentAndChildren_ClosingOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region CheckedVouchers_CurrentBranch_TurnoverOption

        [Test]
        public async Task ProfitLoss_CompareCostCenters_CheckedVouchers_CurrentBranch_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareCostCenters_CheckedVouchers_CurrentBranch_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareCostCenters_CheckedVouchers_CurrentBranch_ByCenter_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareCostCenters_CheckedVouchers_CurrentBranch_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareCostCenters_CheckedVouchers_CurrentBranch_ByProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareCostCenters_CheckedVouchers_CurrentBranch_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareCostCenters_CheckedVouchers_CurrentBranch_ByCenterProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareCostCenters_CheckedVouchers_CurrentBranch_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region FinalizedVouchers_CurrentBranch_TurnoverOption

        [Test]
        public async Task ProfitLoss_CompareCostCenters_FinalizedVouchers_CurrentBranch_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareCostCenters_FinalizedVouchers_CurrentBranch_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareCostCenters_FinalizedVouchers_CurrentBranch_ByCenter_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareCostCenters_FinalizedVouchers_CurrentBranch_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareCostCenters_FinalizedVouchers_CurrentBranch_ByProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareCostCenters_FinalizedVouchers_CurrentBranch_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareCostCenters_FinalizedVouchers_CurrentBranch_ByCenterProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareCostCenters_FinalizedVouchers_CurrentBranch_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region ConfirmedVouchers_CurrentBranch_TurnoverOption

        [Test]
        public async Task ProfitLoss_CompareCostCenters_ConfirmedVouchers_CurrentBranch_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareCostCenters_ConfirmedVouchers_CurrentBranch_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareCostCenters_ConfirmedVouchers_CurrentBranch_ByCenter_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareCostCenters_ConfirmedVouchers_CurrentBranch_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareCostCenters_ConfirmedVouchers_CurrentBranch_ByProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareCostCenters_ConfirmedVouchers_CurrentBranch_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareCostCenters_ConfirmedVouchers_CurrentBranch_ByCenterProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareCostCenters_ConfirmedVouchers_CurrentBranch_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region ApprovedVouchers_CurrentBranch_TurnoverOption

        [Test]
        public async Task ProfitLoss_CompareCostCenters_ApprovedVouchers_CurrentBranch_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareCostCenters_ApprovedVouchers_CurrentBranch_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareCostCenters_ApprovedVouchers_CurrentBranch_ByCenter_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareCostCenters_ApprovedVouchers_CurrentBranch_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareCostCenters_ApprovedVouchers_CurrentBranch_ByProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareCostCenters_ApprovedVouchers_CurrentBranch_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareCostCenters_ApprovedVouchers_CurrentBranch_ByCenterProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareCostCenters_ApprovedVouchers_CurrentBranch_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region AllVouchers_CurrentBranch_TurnoverOption

        [Test]
        public async Task ProfitLoss_CompareCostCenters_AllVouchers_CurrentBranch_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareCostCenters_AllVouchers_CurrentBranch_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareCostCenters_AllVouchers_CurrentBranch_ByCenter_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareCostCenters_AllVouchers_CurrentBranch_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareCostCenters_AllVouchers_CurrentBranch_ByProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareCostCenters_AllVouchers_CurrentBranch_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareCostCenters_AllVouchers_CurrentBranch_ByCenterProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareCostCenters_AllVouchers_CurrentBranch_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region CheckedVouchers_CurrentAndChildren_TurnoverOption

        [Test]
        public async Task ProfitLoss_CompareCostCenters_CheckedVouchers_CurrentAndChildren_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareCostCenters_CheckedVouchers_CurrentAndChildren_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareCostCenters_CheckedVouchers_CurrentAndChildren_ByCenter_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareCostCenters_CheckedVouchers_CurrentAndChildren_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareCostCenters_CheckedVouchers_CurrentAndChildren_ByProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareCostCenters_CheckedVouchers_CurrentAndChildren_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareCostCenters_CheckedVouchers_CurrentAndChildren_ByCenterProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareCostCenters_CheckedVouchers_CurrentAndChildren_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region FinalizedVouchers_CurrentAndChildren_TurnoverOption

        [Test]
        public async Task ProfitLoss_CompareCostCenters_FinalizedVouchers_CurrentAndChildren_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareCostCenters_FinalizedVouchers_CurrentAndChildren_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareCostCenters_FinalizedVouchers_CurrentAndChildren_ByCenter_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareCostCenters_FinalizedVouchers_CurrentAndChildren_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareCostCenters_FinalizedVouchers_CurrentAndChildren_ByProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareCostCenters_FinalizedVouchers_CurrentAndChildren_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareCostCenters_FinalizedVouchers_CurrentAndChildren_ByCenterProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareCostCenters_FinalizedVouchers_CurrentAndChildren_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region ConfirmedVouchers_CurrentAndChildren_TurnoverOption

        [Test]
        public async Task ProfitLoss_CompareCostCenters_ConfirmedVouchers_CurrentAndChildren_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareCostCenters_ConfirmedVouchers_CurrentAndChildren_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareCostCenters_ConfirmedVouchers_CurrentAndChildren_ByCenter_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareCostCenters_ConfirmedVouchers_CurrentAndChildren_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareCostCenters_ConfirmedVouchers_CurrentAndChildren_ByProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareCostCenters_ConfirmedVouchers_CurrentAndChildren_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareCostCenters_ConfirmedVouchers_CurrentAndChildren_ByCenterProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareCostCenters_ConfirmedVouchers_CurrentAndChildren_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region ApprovedVouchers_CurrentAndChildren_TurnoverOption

        [Test]
        public async Task ProfitLoss_CompareCostCenters_ApprovedVouchers_CurrentAndChildren_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareCostCenters_ApprovedVouchers_CurrentAndChildren_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareCostCenters_ApprovedVouchers_CurrentAndChildren_ByCenter_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareCostCenters_ApprovedVouchers_CurrentAndChildren_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareCostCenters_ApprovedVouchers_CurrentAndChildren_ByProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareCostCenters_ApprovedVouchers_CurrentAndChildren_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareCostCenters_ApprovedVouchers_CurrentAndChildren_ByCenterProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareCostCenters_ApprovedVouchers_CurrentAndChildren_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region AllVouchers_CurrentAndChildren_TurnoverOption

        [Test]
        public async Task ProfitLoss_CompareCostCenters_AllVouchers_CurrentAndChildren_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareCostCenters_AllVouchers_CurrentAndChildren_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareCostCenters_AllVouchers_CurrentAndChildren_ByCenter_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareCostCenters_AllVouchers_CurrentAndChildren_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareCostCenters_AllVouchers_CurrentAndChildren_ByProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareCostCenters_AllVouchers_CurrentAndChildren_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareCostCenters_AllVouchers_CurrentAndChildren_ByCenterProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareCostCenters_AllVouchers_CurrentAndChildren_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region CheckedVouchers_CurrentBranch_ClosingOption_TurnoverOption

        [Test]
        public async Task ProfitLoss_CompareCostCenters_CheckedVouchers_CurrentBranch_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareCostCenters_CheckedVouchers_CurrentBranch_ClosingOption_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareCostCenters_CheckedVouchers_CurrentBranch_ByCenter_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareCostCenters_CheckedVouchers_CurrentBranch_ClosingOption_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareCostCenters_CheckedVouchers_CurrentBranch_ByProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareCostCenters_CheckedVouchers_CurrentBranch_ClosingOption_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareCostCenters_CheckedVouchers_CurrentBranch_ByCenterProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareCostCenters_CheckedVouchers_CurrentBranch_ClosingOption_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region FinalizedVouchers_CurrentBranch_ClosingOption_TurnoverOption

        [Test]
        public async Task ProfitLoss_CompareCostCenters_FinalizedVouchers_CurrentBranch_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareCostCenters_FinalizedVouchers_CurrentBranch_ClosingOption_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareCostCenters_FinalizedVouchers_CurrentBranch_ByCenter_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareCostCenters_FinalizedVouchers_CurrentBranch_ClosingOption_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareCostCenters_FinalizedVouchers_CurrentBranch_ByProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareCostCenters_FinalizedVouchers_CurrentBranch_ClosingOption_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareCostCenters_FinalizedVouchers_CurrentBranch_ByCenterProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareCostCenters_FinalizedVouchers_CurrentBranch_ClosingOption_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region ConfirmedVouchers_CurrentBranch_ClosingOption_TurnoverOption

        [Test]
        public async Task ProfitLoss_CompareCostCenters_ConfirmedVouchers_CurrentBranch_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareCostCenters_ConfirmedVouchers_CurrentBranch_ClosingOption_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareCostCenters_ConfirmedVouchers_CurrentBranch_ByCenter_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareCostCenters_ConfirmedVouchers_CurrentBranch_ClosingOption_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareCostCenters_ConfirmedVouchers_CurrentBranch_ByProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareCostCenters_ConfirmedVouchers_CurrentBranch_ClosingOption_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareCostCenters_ConfirmedVouchers_CurrentBranch_ByCenterProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareCostCenters_ConfirmedVouchers_CurrentBranch_ClosingOption_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region ApprovedVouchers_CurrentBranch_ClosingOption_TurnoverOption

        [Test]
        public async Task ProfitLoss_CompareCostCenters_ApprovedVouchers_CurrentBranch_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareCostCenters_ApprovedVouchers_CurrentBranch_ClosingOption_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareCostCenters_ApprovedVouchers_CurrentBranch_ByCenter_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareCostCenters_ApprovedVouchers_CurrentBranch_ClosingOption_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareCostCenters_ApprovedVouchers_CurrentBranch_ByProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareCostCenters_ApprovedVouchers_CurrentBranch_ClosingOption_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareCostCenters_ApprovedVouchers_CurrentBranch_ByCenterProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareCostCenters_ApprovedVouchers_CurrentBranch_ClosingOption_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region AllVouchers_CurrentBranch_ClosingOption_TurnoverOption

        [Test]
        public async Task ProfitLoss_CompareCostCenters_AllVouchers_CurrentBranch_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareCostCenters_AllVouchers_CurrentBranch_ClosingOption_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareCostCenters_AllVouchers_CurrentBranch_ByCenter_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareCostCenters_AllVouchers_CurrentBranch_ClosingOption_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareCostCenters_AllVouchers_CurrentBranch_ByProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareCostCenters_AllVouchers_CurrentBranch_ClosingOption_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareCostCenters_AllVouchers_CurrentBranch_ByCenterProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareCostCenters_AllVouchers_CurrentBranch_ClosingOption_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region CheckedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption

        [Test]
        public async Task ProfitLoss_CompareCostCenters_CheckedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareCostCenters_CheckedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareCostCenters_CheckedVouchers_CurrentAndChildren_ByCenter_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareCostCenters_CheckedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareCostCenters_CheckedVouchers_CurrentAndChildren_ByProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareCostCenters_CheckedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareCostCenters_CheckedVouchers_CurrentAndChildren_ByCenterProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareCostCenters_CheckedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region FinalizedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption

        [Test]
        public async Task ProfitLoss_CompareCostCenters_FinalizedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareCostCenters_FinalizedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareCostCenters_FinalizedVouchers_CurrentAndChildren_ByCenter_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareCostCenters_FinalizedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareCostCenters_FinalizedVouchers_CurrentAndChildren_ByProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareCostCenters_FinalizedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareCostCenters_FinalizedVouchers_CurrentAndChildren_ByCenterProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareCostCenters_FinalizedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region ConfirmedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption

        [Test]
        public async Task ProfitLoss_CompareCostCenters_ConfirmedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareCostCenters_ConfirmedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareCostCenters_ConfirmedVouchers_CurrentAndChildren_ByCenter_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareCostCenters_ConfirmedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareCostCenters_ConfirmedVouchers_CurrentAndChildren_ByProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareCostCenters_ConfirmedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareCostCenters_ConfirmedVouchers_CurrentAndChildren_ByCenterProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareCostCenters_ConfirmedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region ApprovedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption

        [Test]
        public async Task ProfitLoss_CompareCostCenters_ApprovedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareCostCenters_ApprovedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareCostCenters_ApprovedVouchers_CurrentAndChildren_ByCenter_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareCostCenters_ApprovedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareCostCenters_ApprovedVouchers_CurrentAndChildren_ByProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareCostCenters_ApprovedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareCostCenters_ApprovedVouchers_CurrentAndChildren_ByCenterProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareCostCenters_ApprovedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region AllVouchers_CurrentAndChildren_ClosingOption_TurnoverOption

        [Test]
        public async Task ProfitLoss_CompareCostCenters_AllVouchers_CurrentAndChildren_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareCostCenters_AllVouchers_CurrentAndChildren_ClosingOption_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareCostCenters_AllVouchers_CurrentAndChildren_ByCenter_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareCostCenters_AllVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareCostCenters_AllVouchers_CurrentAndChildren_ByProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareCostCenters_AllVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareCostCenters_AllVouchers_CurrentAndChildren_ByCenterProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareCostCenters_AllVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #endregion

        #region Profit-Loss By Fiscal Periods Tests

        #region CheckedVouchers_CurrentBranch_NoOptions

        [Test]
        public async Task ProfitLoss_CompareFiscalPeriods_CheckedVouchers_CurrentBranch_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareFiscalPeriods_CheckedVouchers_CurrentBranch_NoOptions();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareFiscalPeriods_CheckedVouchers_CurrentBranch_ByCenter_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareFiscalPeriods_CheckedVouchers_CurrentBranch_NoOptions(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareFiscalPeriods_CheckedVouchers_CurrentBranch_ByProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareFiscalPeriods_CheckedVouchers_CurrentBranch_NoOptions(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareFiscalPeriods_CheckedVouchers_CurrentBranch_ByCenterProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareFiscalPeriods_CheckedVouchers_CurrentBranch_NoOptions(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region FinalizedVouchers_CurrentBranch_NoOptions

        [Test]
        public async Task ProfitLoss_CompareFiscalPeriods_FinalizedVouchers_CurrentBranch_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareFiscalPeriods_FinalizedVouchers_CurrentBranch_NoOptions();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareFiscalPeriods_FinalizedVouchers_CurrentBranch_ByCenter_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareFiscalPeriods_FinalizedVouchers_CurrentBranch_NoOptions(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareFiscalPeriods_FinalizedVouchers_CurrentBranch_ByProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareFiscalPeriods_FinalizedVouchers_CurrentBranch_NoOptions(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareFiscalPeriods_FinalizedVouchers_CurrentBranch_ByCenterProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareFiscalPeriods_FinalizedVouchers_CurrentBranch_NoOptions(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region ConfirmedVouchers_CurrentBranch_NoOptions

        [Test]
        public async Task ProfitLoss_CompareFiscalPeriods_ConfirmedVouchers_CurrentBranch_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareFiscalPeriods_ConfirmedVouchers_CurrentBranch_NoOptions();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareFiscalPeriods_ConfirmedVouchers_CurrentBranch_ByCenter_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareFiscalPeriods_ConfirmedVouchers_CurrentBranch_NoOptions(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareFiscalPeriods_ConfirmedVouchers_CurrentBranch_ByProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareFiscalPeriods_ConfirmedVouchers_CurrentBranch_NoOptions(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareFiscalPeriods_ConfirmedVouchers_CurrentBranch_ByCenterProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareFiscalPeriods_ConfirmedVouchers_CurrentBranch_NoOptions(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region ApprovedVouchers_CurrentBranch_NoOptions

        [Test]
        public async Task ProfitLoss_CompareFiscalPeriods_ApprovedVouchers_CurrentBranch_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareFiscalPeriods_ApprovedVouchers_CurrentBranch_NoOptions();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareFiscalPeriods_ApprovedVouchers_CurrentBranch_ByCenter_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareFiscalPeriods_ApprovedVouchers_CurrentBranch_NoOptions(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareFiscalPeriods_ApprovedVouchers_CurrentBranch_ByProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareFiscalPeriods_ApprovedVouchers_CurrentBranch_NoOptions(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareFiscalPeriods_ApprovedVouchers_CurrentBranch_ByCenterProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareFiscalPeriods_ApprovedVouchers_CurrentBranch_NoOptions(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region AllVouchers_CurrentBranch_NoOptions

        [Test]
        public async Task ProfitLoss_CompareFiscalPeriods_AllVouchers_CurrentBranch_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareFiscalPeriods_AllVouchers_CurrentBranch_NoOptions();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareFiscalPeriods_AllVouchers_CurrentBranch_ByCenter_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareFiscalPeriods_AllVouchers_CurrentBranch_NoOptions(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareFiscalPeriods_AllVouchers_CurrentBranch_ByProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareFiscalPeriods_AllVouchers_CurrentBranch_NoOptions(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareFiscalPeriods_AllVouchers_CurrentBranch_ByCenterProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareFiscalPeriods_AllVouchers_CurrentBranch_NoOptions(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region CheckedVouchers_CurrentAndChildren_NoOptions

        [Test]
        public async Task ProfitLoss_CompareFiscalPeriods_CheckedVouchers_CurrentAndChildren_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareFiscalPeriods_CheckedVouchers_CurrentAndChildren_NoOptions();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareFiscalPeriods_CheckedVouchers_CurrentAndChildren_ByCenter_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareFiscalPeriods_CheckedVouchers_CurrentAndChildren_NoOptions(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareFiscalPeriods_CheckedVouchers_CurrentAndChildren_ByProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareFiscalPeriods_CheckedVouchers_CurrentAndChildren_NoOptions(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareFiscalPeriods_CheckedVouchers_CurrentAndChildren_ByCenterProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareFiscalPeriods_CheckedVouchers_CurrentAndChildren_NoOptions(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region FinalizedVouchers_CurrentAndChildren_NoOptions

        [Test]
        public async Task ProfitLoss_CompareFiscalPeriods_FinalizedVouchers_CurrentAndChildren_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareFiscalPeriods_FinalizedVouchers_CurrentAndChildren_NoOptions();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareFiscalPeriods_FinalizedVouchers_CurrentAndChildren_ByCenter_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareFiscalPeriods_FinalizedVouchers_CurrentAndChildren_NoOptions(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareFiscalPeriods_FinalizedVouchers_CurrentAndChildren_ByProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareFiscalPeriods_FinalizedVouchers_CurrentAndChildren_NoOptions(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareFiscalPeriods_FinalizedVouchers_CurrentAndChildren_ByCenterProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareFiscalPeriods_FinalizedVouchers_CurrentAndChildren_NoOptions(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region ConfirmedVouchers_CurrentAndChildren_NoOptions

        [Test]
        public async Task ProfitLoss_CompareFiscalPeriods_ConfirmedVouchers_CurrentAndChildren_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareFiscalPeriods_ConfirmedVouchers_CurrentAndChildren_NoOptions();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareFiscalPeriods_ConfirmedVouchers_CurrentAndChildren_ByCenter_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareFiscalPeriods_ConfirmedVouchers_CurrentAndChildren_NoOptions(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareFiscalPeriods_ConfirmedVouchers_CurrentAndChildren_ByProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareFiscalPeriods_ConfirmedVouchers_CurrentAndChildren_NoOptions(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareFiscalPeriods_ConfirmedVouchers_CurrentAndChildren_ByCenterProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareFiscalPeriods_ConfirmedVouchers_CurrentAndChildren_NoOptions(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region ApprovedVouchers_CurrentAndChildren_NoOptions

        [Test]
        public async Task ProfitLoss_CompareFiscalPeriods_ApprovedVouchers_CurrentAndChildren_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareFiscalPeriods_ApprovedVouchers_CurrentAndChildren_NoOptions();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareFiscalPeriods_ApprovedVouchers_CurrentAndChildren_ByCenter_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareFiscalPeriods_ApprovedVouchers_CurrentAndChildren_NoOptions(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareFiscalPeriods_ApprovedVouchers_CurrentAndChildren_ByProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareFiscalPeriods_ApprovedVouchers_CurrentAndChildren_NoOptions(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareFiscalPeriods_ApprovedVouchers_CurrentAndChildren_ByCenterProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareFiscalPeriods_ApprovedVouchers_CurrentAndChildren_NoOptions(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region AllVouchers_CurrentAndChildren_NoOptions

        [Test]
        public async Task ProfitLoss_CompareFiscalPeriods_AllVouchers_CurrentAndChildren_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareFiscalPeriods_AllVouchers_CurrentAndChildren_NoOptions();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareFiscalPeriods_AllVouchers_CurrentAndChildren_ByCenter_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareFiscalPeriods_AllVouchers_CurrentAndChildren_NoOptions(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareFiscalPeriods_AllVouchers_CurrentAndChildren_ByProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareFiscalPeriods_AllVouchers_CurrentAndChildren_NoOptions(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareFiscalPeriods_AllVouchers_CurrentAndChildren_ByCenterProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareFiscalPeriods_AllVouchers_CurrentAndChildren_NoOptions(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region CheckedVouchers_CurrentBranch_ClosingOption

        [Test]
        public async Task ProfitLoss_CompareFiscalPeriods_CheckedVouchers_CurrentBranch_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareFiscalPeriods_CheckedVouchers_CurrentBranch_ClosingOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareFiscalPeriods_CheckedVouchers_CurrentBranch_ByCenter_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareFiscalPeriods_CheckedVouchers_CurrentBranch_ClosingOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareFiscalPeriods_CheckedVouchers_CurrentBranch_ByProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareFiscalPeriods_CheckedVouchers_CurrentBranch_ClosingOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareFiscalPeriods_CheckedVouchers_CurrentBranch_ByCenterProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareFiscalPeriods_CheckedVouchers_CurrentBranch_ClosingOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region FinalizedVouchers_CurrentBranch_ClosingOption

        [Test]
        public async Task ProfitLoss_CompareFiscalPeriods_FinalizedVouchers_CurrentBranch_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareFiscalPeriods_FinalizedVouchers_CurrentBranch_ClosingOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareFiscalPeriods_FinalizedVouchers_CurrentBranch_ByCenter_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareFiscalPeriods_FinalizedVouchers_CurrentBranch_ClosingOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareFiscalPeriods_FinalizedVouchers_CurrentBranch_ByProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareFiscalPeriods_FinalizedVouchers_CurrentBranch_ClosingOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareFiscalPeriods_FinalizedVouchers_CurrentBranch_ByCenterProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareFiscalPeriods_FinalizedVouchers_CurrentBranch_ClosingOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region ConfirmedVouchers_CurrentBranch_ClosingOption

        [Test]
        public async Task ProfitLoss_CompareFiscalPeriods_ConfirmedVouchers_CurrentBranch_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareFiscalPeriods_ConfirmedVouchers_CurrentBranch_ClosingOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareFiscalPeriods_ConfirmedVouchers_CurrentBranch_ByCenter_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareFiscalPeriods_ConfirmedVouchers_CurrentBranch_ClosingOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareFiscalPeriods_ConfirmedVouchers_CurrentBranch_ByProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareFiscalPeriods_ConfirmedVouchers_CurrentBranch_ClosingOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareFiscalPeriods_ConfirmedVouchers_CurrentBranch_ByCenterProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareFiscalPeriods_ConfirmedVouchers_CurrentBranch_ClosingOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region ApprovedVouchers_CurrentBranch_ClosingOption

        [Test]
        public async Task ProfitLoss_CompareFiscalPeriods_ApprovedVouchers_CurrentBranch_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareFiscalPeriods_ApprovedVouchers_CurrentBranch_ClosingOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareFiscalPeriods_ApprovedVouchers_CurrentBranch_ByCenter_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareFiscalPeriods_ApprovedVouchers_CurrentBranch_ClosingOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareFiscalPeriods_ApprovedVouchers_CurrentBranch_ByProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareFiscalPeriods_ApprovedVouchers_CurrentBranch_ClosingOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareFiscalPeriods_ApprovedVouchers_CurrentBranch_ByCenterProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareFiscalPeriods_ApprovedVouchers_CurrentBranch_ClosingOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region AllVouchers_CurrentBranch_ClosingOption

        [Test]
        public async Task ProfitLoss_CompareFiscalPeriods_AllVouchers_CurrentBranch_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareFiscalPeriods_AllVouchers_CurrentBranch_ClosingOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareFiscalPeriods_AllVouchers_CurrentBranch_ByCenter_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareFiscalPeriods_AllVouchers_CurrentBranch_ClosingOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareFiscalPeriods_AllVouchers_CurrentBranch_ByProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareFiscalPeriods_AllVouchers_CurrentBranch_ClosingOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareFiscalPeriods_AllVouchers_CurrentBranch_ByCenterProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareFiscalPeriods_AllVouchers_CurrentBranch_ClosingOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region CheckedVouchers_CurrentAndChildren_ClosingOption

        [Test]
        public async Task ProfitLoss_CompareFiscalPeriods_CheckedVouchers_CurrentAndChildren_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareFiscalPeriods_CheckedVouchers_CurrentAndChildren_ClosingOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareFiscalPeriods_CheckedVouchers_CurrentAndChildren_ByCenter_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareFiscalPeriods_CheckedVouchers_CurrentAndChildren_ClosingOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareFiscalPeriods_CheckedVouchers_CurrentAndChildren_ByProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareFiscalPeriods_CheckedVouchers_CurrentAndChildren_ClosingOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareFiscalPeriods_CheckedVouchers_CurrentAndChildren_ByCenterProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareFiscalPeriods_CheckedVouchers_CurrentAndChildren_ClosingOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region FinalizedVouchers_CurrentAndChildren_ClosingOption

        [Test]
        public async Task ProfitLoss_CompareFiscalPeriods_FinalizedVouchers_CurrentAndChildren_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareFiscalPeriods_FinalizedVouchers_CurrentAndChildren_ClosingOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareFiscalPeriods_FinalizedVouchers_CurrentAndChildren_ByCenter_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareFiscalPeriods_FinalizedVouchers_CurrentAndChildren_ClosingOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareFiscalPeriods_FinalizedVouchers_CurrentAndChildren_ByProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareFiscalPeriods_FinalizedVouchers_CurrentAndChildren_ClosingOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareFiscalPeriods_FinalizedVouchers_CurrentAndChildren_ByCenterProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareFiscalPeriods_FinalizedVouchers_CurrentAndChildren_ClosingOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region ConfirmedVouchers_CurrentAndChildren_ClosingOption

        [Test]
        public async Task ProfitLoss_CompareFiscalPeriods_ConfirmedVouchers_CurrentAndChildren_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareFiscalPeriods_ConfirmedVouchers_CurrentAndChildren_ClosingOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareFiscalPeriods_ConfirmedVouchers_CurrentAndChildren_ByCenter_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareFiscalPeriods_ConfirmedVouchers_CurrentAndChildren_ClosingOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareFiscalPeriods_ConfirmedVouchers_CurrentAndChildren_ByProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareFiscalPeriods_ConfirmedVouchers_CurrentAndChildren_ClosingOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareFiscalPeriods_ConfirmedVouchers_CurrentAndChildren_ByCenterProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareFiscalPeriods_ConfirmedVouchers_CurrentAndChildren_ClosingOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region ApprovedVouchers_CurrentAndChildren_ClosingOption

        [Test]
        public async Task ProfitLoss_CompareFiscalPeriods_ApprovedVouchers_CurrentAndChildren_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareFiscalPeriods_ApprovedVouchers_CurrentAndChildren_ClosingOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareFiscalPeriods_ApprovedVouchers_CurrentAndChildren_ByCenter_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareFiscalPeriods_ApprovedVouchers_CurrentAndChildren_ClosingOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareFiscalPeriods_ApprovedVouchers_CurrentAndChildren_ByProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareFiscalPeriods_ApprovedVouchers_CurrentAndChildren_ClosingOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareFiscalPeriods_ApprovedVouchers_CurrentAndChildren_ByCenterProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareFiscalPeriods_ApprovedVouchers_CurrentAndChildren_ClosingOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region AllVouchers_CurrentAndChildren_ClosingOption

        [Test]
        public async Task ProfitLoss_CompareFiscalPeriods_AllVouchers_CurrentAndChildren_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareFiscalPeriods_AllVouchers_CurrentAndChildren_ClosingOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareFiscalPeriods_AllVouchers_CurrentAndChildren_ByCenter_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareFiscalPeriods_AllVouchers_CurrentAndChildren_ClosingOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareFiscalPeriods_AllVouchers_CurrentAndChildren_ByProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareFiscalPeriods_AllVouchers_CurrentAndChildren_ClosingOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareFiscalPeriods_AllVouchers_CurrentAndChildren_ByCenterProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareFiscalPeriods_AllVouchers_CurrentAndChildren_ClosingOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region CheckedVouchers_CurrentBranch_TurnoverOption

        [Test]
        public async Task ProfitLoss_CompareFiscalPeriods_CheckedVouchers_CurrentBranch_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareFiscalPeriods_CheckedVouchers_CurrentBranch_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareFiscalPeriods_CheckedVouchers_CurrentBranch_ByCenter_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareFiscalPeriods_CheckedVouchers_CurrentBranch_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareFiscalPeriods_CheckedVouchers_CurrentBranch_ByProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareFiscalPeriods_CheckedVouchers_CurrentBranch_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareFiscalPeriods_CheckedVouchers_CurrentBranch_ByCenterProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareFiscalPeriods_CheckedVouchers_CurrentBranch_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region FinalizedVouchers_CurrentBranch_TurnoverOption

        [Test]
        public async Task ProfitLoss_CompareFiscalPeriods_FinalizedVouchers_CurrentBranch_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareFiscalPeriods_FinalizedVouchers_CurrentBranch_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareFiscalPeriods_FinalizedVouchers_CurrentBranch_ByCenter_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareFiscalPeriods_FinalizedVouchers_CurrentBranch_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareFiscalPeriods_FinalizedVouchers_CurrentBranch_ByProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareFiscalPeriods_FinalizedVouchers_CurrentBranch_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareFiscalPeriods_FinalizedVouchers_CurrentBranch_ByCenterProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareFiscalPeriods_FinalizedVouchers_CurrentBranch_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region ConfirmedVouchers_CurrentBranch_TurnoverOption

        [Test]
        public async Task ProfitLoss_CompareFiscalPeriods_ConfirmedVouchers_CurrentBranch_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareFiscalPeriods_ConfirmedVouchers_CurrentBranch_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareFiscalPeriods_ConfirmedVouchers_CurrentBranch_ByCenter_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareFiscalPeriods_ConfirmedVouchers_CurrentBranch_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareFiscalPeriods_ConfirmedVouchers_CurrentBranch_ByProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareFiscalPeriods_ConfirmedVouchers_CurrentBranch_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareFiscalPeriods_ConfirmedVouchers_CurrentBranch_ByCenterProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareFiscalPeriods_ConfirmedVouchers_CurrentBranch_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region ApprovedVouchers_CurrentBranch_TurnoverOption

        [Test]
        public async Task ProfitLoss_CompareFiscalPeriods_ApprovedVouchers_CurrentBranch_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareFiscalPeriods_ApprovedVouchers_CurrentBranch_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareFiscalPeriods_ApprovedVouchers_CurrentBranch_ByCenter_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareFiscalPeriods_ApprovedVouchers_CurrentBranch_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareFiscalPeriods_ApprovedVouchers_CurrentBranch_ByProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareFiscalPeriods_ApprovedVouchers_CurrentBranch_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareFiscalPeriods_ApprovedVouchers_CurrentBranch_ByCenterProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareFiscalPeriods_ApprovedVouchers_CurrentBranch_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region AllVouchers_CurrentBranch_TurnoverOption

        [Test]
        public async Task ProfitLoss_CompareFiscalPeriods_AllVouchers_CurrentBranch_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareFiscalPeriods_AllVouchers_CurrentBranch_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareFiscalPeriods_AllVouchers_CurrentBranch_ByCenter_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareFiscalPeriods_AllVouchers_CurrentBranch_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareFiscalPeriods_AllVouchers_CurrentBranch_ByProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareFiscalPeriods_AllVouchers_CurrentBranch_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareFiscalPeriods_AllVouchers_CurrentBranch_ByCenterProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareFiscalPeriods_AllVouchers_CurrentBranch_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region CheckedVouchers_CurrentAndChildren_TurnoverOption

        [Test]
        public async Task ProfitLoss_CompareFiscalPeriods_CheckedVouchers_CurrentAndChildren_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareFiscalPeriods_CheckedVouchers_CurrentAndChildren_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareFiscalPeriods_CheckedVouchers_CurrentAndChildren_ByCenter_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareFiscalPeriods_CheckedVouchers_CurrentAndChildren_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareFiscalPeriods_CheckedVouchers_CurrentAndChildren_ByProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareFiscalPeriods_CheckedVouchers_CurrentAndChildren_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareFiscalPeriods_CheckedVouchers_CurrentAndChildren_ByCenterProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareFiscalPeriods_CheckedVouchers_CurrentAndChildren_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region FinalizedVouchers_CurrentAndChildren_TurnoverOption

        [Test]
        public async Task ProfitLoss_CompareFiscalPeriods_FinalizedVouchers_CurrentAndChildren_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareFiscalPeriods_FinalizedVouchers_CurrentAndChildren_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareFiscalPeriods_FinalizedVouchers_CurrentAndChildren_ByCenter_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareFiscalPeriods_FinalizedVouchers_CurrentAndChildren_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareFiscalPeriods_FinalizedVouchers_CurrentAndChildren_ByProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareFiscalPeriods_FinalizedVouchers_CurrentAndChildren_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareFiscalPeriods_FinalizedVouchers_CurrentAndChildren_ByCenterProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareFiscalPeriods_FinalizedVouchers_CurrentAndChildren_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region ConfirmedVouchers_CurrentAndChildren_TurnoverOption

        [Test]
        public async Task ProfitLoss_CompareFiscalPeriods_ConfirmedVouchers_CurrentAndChildren_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareFiscalPeriods_ConfirmedVouchers_CurrentAndChildren_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareFiscalPeriods_ConfirmedVouchers_CurrentAndChildren_ByCenter_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareFiscalPeriods_ConfirmedVouchers_CurrentAndChildren_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareFiscalPeriods_ConfirmedVouchers_CurrentAndChildren_ByProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareFiscalPeriods_ConfirmedVouchers_CurrentAndChildren_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareFiscalPeriods_ConfirmedVouchers_CurrentAndChildren_ByCenterProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareFiscalPeriods_ConfirmedVouchers_CurrentAndChildren_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region ApprovedVouchers_CurrentAndChildren_TurnoverOption

        [Test]
        public async Task ProfitLoss_CompareFiscalPeriods_ApprovedVouchers_CurrentAndChildren_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareFiscalPeriods_ApprovedVouchers_CurrentAndChildren_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareFiscalPeriods_ApprovedVouchers_CurrentAndChildren_ByCenter_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareFiscalPeriods_ApprovedVouchers_CurrentAndChildren_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareFiscalPeriods_ApprovedVouchers_CurrentAndChildren_ByProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareFiscalPeriods_ApprovedVouchers_CurrentAndChildren_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareFiscalPeriods_ApprovedVouchers_CurrentAndChildren_ByCenterProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareFiscalPeriods_ApprovedVouchers_CurrentAndChildren_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region AllVouchers_CurrentAndChildren_TurnoverOption

        [Test]
        public async Task ProfitLoss_CompareFiscalPeriods_AllVouchers_CurrentAndChildren_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareFiscalPeriods_AllVouchers_CurrentAndChildren_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareFiscalPeriods_AllVouchers_CurrentAndChildren_ByCenter_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareFiscalPeriods_AllVouchers_CurrentAndChildren_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareFiscalPeriods_AllVouchers_CurrentAndChildren_ByProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareFiscalPeriods_AllVouchers_CurrentAndChildren_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareFiscalPeriods_AllVouchers_CurrentAndChildren_ByCenterProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareFiscalPeriods_AllVouchers_CurrentAndChildren_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region CheckedVouchers_CurrentBranch_ClosingOption_TurnoverOption

        [Test]
        public async Task ProfitLoss_CompareFiscalPeriods_CheckedVouchers_CurrentBranch_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareFiscalPeriods_CheckedVouchers_CurrentBranch_ClosingOption_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareFiscalPeriods_CheckedVouchers_CurrentBranch_ByCenter_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareFiscalPeriods_CheckedVouchers_CurrentBranch_ClosingOption_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareFiscalPeriods_CheckedVouchers_CurrentBranch_ByProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareFiscalPeriods_CheckedVouchers_CurrentBranch_ClosingOption_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareFiscalPeriods_CheckedVouchers_CurrentBranch_ByCenterProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareFiscalPeriods_CheckedVouchers_CurrentBranch_ClosingOption_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region FinalizedVouchers_CurrentBranch_ClosingOption_TurnoverOption

        [Test]
        public async Task ProfitLoss_CompareFiscalPeriods_FinalizedVouchers_CurrentBranch_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareFiscalPeriods_FinalizedVouchers_CurrentBranch_ClosingOption_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareFiscalPeriods_FinalizedVouchers_CurrentBranch_ByCenter_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareFiscalPeriods_FinalizedVouchers_CurrentBranch_ClosingOption_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareFiscalPeriods_FinalizedVouchers_CurrentBranch_ByProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareFiscalPeriods_FinalizedVouchers_CurrentBranch_ClosingOption_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareFiscalPeriods_FinalizedVouchers_CurrentBranch_ByCenterProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareFiscalPeriods_FinalizedVouchers_CurrentBranch_ClosingOption_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region ConfirmedVouchers_CurrentBranch_ClosingOption_TurnoverOption

        [Test]
        public async Task ProfitLoss_CompareFiscalPeriods_ConfirmedVouchers_CurrentBranch_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareFiscalPeriods_ConfirmedVouchers_CurrentBranch_ClosingOption_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareFiscalPeriods_ConfirmedVouchers_CurrentBranch_ByCenter_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareFiscalPeriods_ConfirmedVouchers_CurrentBranch_ClosingOption_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareFiscalPeriods_ConfirmedVouchers_CurrentBranch_ByProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareFiscalPeriods_ConfirmedVouchers_CurrentBranch_ClosingOption_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareFiscalPeriods_ConfirmedVouchers_CurrentBranch_ByCenterProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareFiscalPeriods_ConfirmedVouchers_CurrentBranch_ClosingOption_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region ApprovedVouchers_CurrentBranch_ClosingOption_TurnoverOption

        [Test]
        public async Task ProfitLoss_CompareFiscalPeriods_ApprovedVouchers_CurrentBranch_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareFiscalPeriods_ApprovedVouchers_CurrentBranch_ClosingOption_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareFiscalPeriods_ApprovedVouchers_CurrentBranch_ByCenter_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareFiscalPeriods_ApprovedVouchers_CurrentBranch_ClosingOption_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareFiscalPeriods_ApprovedVouchers_CurrentBranch_ByProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareFiscalPeriods_ApprovedVouchers_CurrentBranch_ClosingOption_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareFiscalPeriods_ApprovedVouchers_CurrentBranch_ByCenterProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareFiscalPeriods_ApprovedVouchers_CurrentBranch_ClosingOption_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region AllVouchers_CurrentBranch_ClosingOption_TurnoverOption

        [Test]
        public async Task ProfitLoss_CompareFiscalPeriods_AllVouchers_CurrentBranch_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareFiscalPeriods_AllVouchers_CurrentBranch_ClosingOption_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareFiscalPeriods_AllVouchers_CurrentBranch_ByCenter_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareFiscalPeriods_AllVouchers_CurrentBranch_ClosingOption_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareFiscalPeriods_AllVouchers_CurrentBranch_ByProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareFiscalPeriods_AllVouchers_CurrentBranch_ClosingOption_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareFiscalPeriods_AllVouchers_CurrentBranch_ByCenterProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareFiscalPeriods_AllVouchers_CurrentBranch_ClosingOption_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region CheckedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption

        [Test]
        public async Task ProfitLoss_CompareFiscalPeriods_CheckedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareFiscalPeriods_CheckedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareFiscalPeriods_CheckedVouchers_CurrentAndChildren_ByCenter_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareFiscalPeriods_CheckedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareFiscalPeriods_CheckedVouchers_CurrentAndChildren_ByProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareFiscalPeriods_CheckedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareFiscalPeriods_CheckedVouchers_CurrentAndChildren_ByCenterProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareFiscalPeriods_CheckedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region FinalizedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption

        [Test]
        public async Task ProfitLoss_CompareFiscalPeriods_FinalizedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareFiscalPeriods_FinalizedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareFiscalPeriods_FinalizedVouchers_CurrentAndChildren_ByCenter_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareFiscalPeriods_FinalizedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareFiscalPeriods_FinalizedVouchers_CurrentAndChildren_ByProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareFiscalPeriods_FinalizedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareFiscalPeriods_FinalizedVouchers_CurrentAndChildren_ByCenterProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareFiscalPeriods_FinalizedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region ConfirmedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption

        [Test]
        public async Task ProfitLoss_CompareFiscalPeriods_ConfirmedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareFiscalPeriods_ConfirmedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareFiscalPeriods_ConfirmedVouchers_CurrentAndChildren_ByCenter_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareFiscalPeriods_ConfirmedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareFiscalPeriods_ConfirmedVouchers_CurrentAndChildren_ByProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareFiscalPeriods_ConfirmedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareFiscalPeriods_ConfirmedVouchers_CurrentAndChildren_ByCenterProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareFiscalPeriods_ConfirmedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region ApprovedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption

        [Test]
        public async Task ProfitLoss_CompareFiscalPeriods_ApprovedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareFiscalPeriods_ApprovedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareFiscalPeriods_ApprovedVouchers_CurrentAndChildren_ByCenter_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareFiscalPeriods_ApprovedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareFiscalPeriods_ApprovedVouchers_CurrentAndChildren_ByProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareFiscalPeriods_ApprovedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareFiscalPeriods_ApprovedVouchers_CurrentAndChildren_ByCenterProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareFiscalPeriods_ApprovedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region AllVouchers_CurrentAndChildren_ClosingOption_TurnoverOption

        [Test]
        public async Task ProfitLoss_CompareFiscalPeriods_AllVouchers_CurrentAndChildren_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareFiscalPeriods_AllVouchers_CurrentAndChildren_ClosingOption_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareFiscalPeriods_AllVouchers_CurrentAndChildren_ByCenter_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareFiscalPeriods_AllVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareFiscalPeriods_AllVouchers_CurrentAndChildren_ByProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareFiscalPeriods_AllVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_CompareFiscalPeriods_AllVouchers_CurrentAndChildren_ByCenterProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_CompareFiscalPeriods_AllVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #endregion

        #endregion

        #region Periodic Fiscal Period Tests

        #region Profit-Loss Tests

        #region CheckedVouchers_CurrentBranch_NoOptions

        [Test]
        public async Task ProfitLoss_Periodic_CheckedVouchers_CurrentBranch_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CheckedVouchers_CurrentBranch_NoOptions();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CheckedVouchers_CurrentBranch_ByCenter_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CheckedVouchers_CurrentBranch_NoOptions(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CheckedVouchers_CurrentBranch_ByProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CheckedVouchers_CurrentBranch_NoOptions(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CheckedVouchers_CurrentBranch_ByCenterProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CheckedVouchers_CurrentBranch_NoOptions(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region FinalizedVouchers_CurrentBranch_NoOptions

        [Test]
        public async Task ProfitLoss_Periodic_FinalizedVouchers_CurrentBranch_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_FinalizedVouchers_CurrentBranch_NoOptions();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_FinalizedVouchers_CurrentBranch_ByCenter_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_FinalizedVouchers_CurrentBranch_NoOptions(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_FinalizedVouchers_CurrentBranch_ByProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_FinalizedVouchers_CurrentBranch_NoOptions(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_FinalizedVouchers_CurrentBranch_ByCenterProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_FinalizedVouchers_CurrentBranch_NoOptions(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region ConfirmedVouchers_CurrentBranch_NoOptions

        [Test]
        public async Task ProfitLoss_Periodic_ConfirmedVouchers_CurrentBranch_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_ConfirmedVouchers_CurrentBranch_NoOptions();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_ConfirmedVouchers_CurrentBranch_ByCenter_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_ConfirmedVouchers_CurrentBranch_NoOptions(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_ConfirmedVouchers_CurrentBranch_ByProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_ConfirmedVouchers_CurrentBranch_NoOptions(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_ConfirmedVouchers_CurrentBranch_ByCenterProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_ConfirmedVouchers_CurrentBranch_NoOptions(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region ApprovedVouchers_CurrentBranch_NoOptions

        [Test]
        public async Task ProfitLoss_Periodic_ApprovedVouchers_CurrentBranch_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_ApprovedVouchers_CurrentBranch_NoOptions();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_ApprovedVouchers_CurrentBranch_ByCenter_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_ApprovedVouchers_CurrentBranch_NoOptions(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_ApprovedVouchers_CurrentBranch_ByProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_ApprovedVouchers_CurrentBranch_NoOptions(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_ApprovedVouchers_CurrentBranch_ByCenterProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_ApprovedVouchers_CurrentBranch_NoOptions(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region AllVouchers_CurrentBranch_NoOptions

        [Test]
        public async Task ProfitLoss_Periodic_AllVouchers_CurrentBranch_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_AllVouchers_CurrentBranch_NoOptions();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_AllVouchers_CurrentBranch_ByCenter_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_AllVouchers_CurrentBranch_NoOptions(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_AllVouchers_CurrentBranch_ByProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_AllVouchers_CurrentBranch_NoOptions(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_AllVouchers_CurrentBranch_ByCenterProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_AllVouchers_CurrentBranch_NoOptions(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region CheckedVouchers_CurrentAndChildren_NoOptions

        [Test]
        public async Task ProfitLoss_Periodic_CheckedVouchers_CurrentAndChildren_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CheckedVouchers_CurrentAndChildren_NoOptions();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CheckedVouchers_CurrentAndChildren_ByCenter_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CheckedVouchers_CurrentAndChildren_NoOptions(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CheckedVouchers_CurrentAndChildren_ByProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CheckedVouchers_CurrentAndChildren_NoOptions(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CheckedVouchers_CurrentAndChildren_ByCenterProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CheckedVouchers_CurrentAndChildren_NoOptions(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region FinalizedVouchers_CurrentAndChildren_NoOptions

        [Test]
        public async Task ProfitLoss_Periodic_FinalizedVouchers_CurrentAndChildren_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_FinalizedVouchers_CurrentAndChildren_NoOptions();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_FinalizedVouchers_CurrentAndChildren_ByCenter_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_FinalizedVouchers_CurrentAndChildren_NoOptions(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_FinalizedVouchers_CurrentAndChildren_ByProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_FinalizedVouchers_CurrentAndChildren_NoOptions(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_FinalizedVouchers_CurrentAndChildren_ByCenterProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_FinalizedVouchers_CurrentAndChildren_NoOptions(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region ConfirmedVouchers_CurrentAndChildren_NoOptions

        [Test]
        public async Task ProfitLoss_Periodic_ConfirmedVouchers_CurrentAndChildren_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_ConfirmedVouchers_CurrentAndChildren_NoOptions();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_ConfirmedVouchers_CurrentAndChildren_ByCenter_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_ConfirmedVouchers_CurrentAndChildren_NoOptions(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_ConfirmedVouchers_CurrentAndChildren_ByProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_ConfirmedVouchers_CurrentAndChildren_NoOptions(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_ConfirmedVouchers_CurrentAndChildren_ByCenterProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_ConfirmedVouchers_CurrentAndChildren_NoOptions(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region ApprovedVouchers_CurrentAndChildren_NoOptions

        [Test]
        public async Task ProfitLoss_Periodic_ApprovedVouchers_CurrentAndChildren_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_ApprovedVouchers_CurrentAndChildren_NoOptions();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_ApprovedVouchers_CurrentAndChildren_ByCenter_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_ApprovedVouchers_CurrentAndChildren_NoOptions(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_ApprovedVouchers_CurrentAndChildren_ByProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_ApprovedVouchers_CurrentAndChildren_NoOptions(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_ApprovedVouchers_CurrentAndChildren_ByCenterProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_ApprovedVouchers_CurrentAndChildren_NoOptions(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region AllVouchers_CurrentAndChildren_NoOptions

        [Test]
        public async Task ProfitLoss_Periodic_AllVouchers_CurrentAndChildren_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_AllVouchers_CurrentAndChildren_NoOptions();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_AllVouchers_CurrentAndChildren_ByCenter_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_AllVouchers_CurrentAndChildren_NoOptions(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_AllVouchers_CurrentAndChildren_ByProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_AllVouchers_CurrentAndChildren_NoOptions(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_AllVouchers_CurrentAndChildren_ByCenterProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_AllVouchers_CurrentAndChildren_NoOptions(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region CheckedVouchers_CurrentBranch_ClosingOption

        [Test]
        public async Task ProfitLoss_Periodic_CheckedVouchers_CurrentBranch_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CheckedVouchers_CurrentBranch_ClosingOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CheckedVouchers_CurrentBranch_ByCenter_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CheckedVouchers_CurrentBranch_ClosingOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CheckedVouchers_CurrentBranch_ByProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CheckedVouchers_CurrentBranch_ClosingOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CheckedVouchers_CurrentBranch_ByCenterProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CheckedVouchers_CurrentBranch_ClosingOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region FinalizedVouchers_CurrentBranch_ClosingOption

        [Test]
        public async Task ProfitLoss_Periodic_FinalizedVouchers_CurrentBranch_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_FinalizedVouchers_CurrentBranch_ClosingOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_FinalizedVouchers_CurrentBranch_ByCenter_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_FinalizedVouchers_CurrentBranch_ClosingOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_FinalizedVouchers_CurrentBranch_ByProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_FinalizedVouchers_CurrentBranch_ClosingOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_FinalizedVouchers_CurrentBranch_ByCenterProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_FinalizedVouchers_CurrentBranch_ClosingOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region ConfirmedVouchers_CurrentBranch_ClosingOption

        [Test]
        public async Task ProfitLoss_Periodic_ConfirmedVouchers_CurrentBranch_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_ConfirmedVouchers_CurrentBranch_ClosingOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_ConfirmedVouchers_CurrentBranch_ByCenter_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_ConfirmedVouchers_CurrentBranch_ClosingOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_ConfirmedVouchers_CurrentBranch_ByProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_ConfirmedVouchers_CurrentBranch_ClosingOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_ConfirmedVouchers_CurrentBranch_ByCenterProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_ConfirmedVouchers_CurrentBranch_ClosingOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region ApprovedVouchers_CurrentBranch_ClosingOption

        [Test]
        public async Task ProfitLoss_Periodic_ApprovedVouchers_CurrentBranch_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_ApprovedVouchers_CurrentBranch_ClosingOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_ApprovedVouchers_CurrentBranch_ByCenter_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_ApprovedVouchers_CurrentBranch_ClosingOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_ApprovedVouchers_CurrentBranch_ByProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_ApprovedVouchers_CurrentBranch_ClosingOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_ApprovedVouchers_CurrentBranch_ByCenterProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_ApprovedVouchers_CurrentBranch_ClosingOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region AllVouchers_CurrentBranch_ClosingOption

        [Test]
        public async Task ProfitLoss_Periodic_AllVouchers_CurrentBranch_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_AllVouchers_CurrentBranch_ClosingOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_AllVouchers_CurrentBranch_ByCenter_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_AllVouchers_CurrentBranch_ClosingOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_AllVouchers_CurrentBranch_ByProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_AllVouchers_CurrentBranch_ClosingOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_AllVouchers_CurrentBranch_ByCenterProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_AllVouchers_CurrentBranch_ClosingOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region CheckedVouchers_CurrentAndChildren_ClosingOption

        [Test]
        public async Task ProfitLoss_Periodic_CheckedVouchers_CurrentAndChildren_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CheckedVouchers_CurrentAndChildren_ClosingOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CheckedVouchers_CurrentAndChildren_ByCenter_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CheckedVouchers_CurrentAndChildren_ClosingOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CheckedVouchers_CurrentAndChildren_ByProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CheckedVouchers_CurrentAndChildren_ClosingOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CheckedVouchers_CurrentAndChildren_ByCenterProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CheckedVouchers_CurrentAndChildren_ClosingOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region FinalizedVouchers_CurrentAndChildren_ClosingOption

        [Test]
        public async Task ProfitLoss_Periodic_FinalizedVouchers_CurrentAndChildren_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_FinalizedVouchers_CurrentAndChildren_ClosingOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_FinalizedVouchers_CurrentAndChildren_ByCenter_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_FinalizedVouchers_CurrentAndChildren_ClosingOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_FinalizedVouchers_CurrentAndChildren_ByProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_FinalizedVouchers_CurrentAndChildren_ClosingOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_FinalizedVouchers_CurrentAndChildren_ByCenterProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_FinalizedVouchers_CurrentAndChildren_ClosingOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region ConfirmedVouchers_CurrentAndChildren_ClosingOption

        [Test]
        public async Task ProfitLoss_Periodic_ConfirmedVouchers_CurrentAndChildren_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_ConfirmedVouchers_CurrentAndChildren_ClosingOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_ConfirmedVouchers_CurrentAndChildren_ByCenter_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_ConfirmedVouchers_CurrentAndChildren_ClosingOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_ConfirmedVouchers_CurrentAndChildren_ByProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_ConfirmedVouchers_CurrentAndChildren_ClosingOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_ConfirmedVouchers_CurrentAndChildren_ByCenterProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_ConfirmedVouchers_CurrentAndChildren_ClosingOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region ApprovedVouchers_CurrentAndChildren_ClosingOption

        [Test]
        public async Task ProfitLoss_Periodic_ApprovedVouchers_CurrentAndChildren_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_ApprovedVouchers_CurrentAndChildren_ClosingOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_ApprovedVouchers_CurrentAndChildren_ByCenter_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_ApprovedVouchers_CurrentAndChildren_ClosingOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_ApprovedVouchers_CurrentAndChildren_ByProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_ApprovedVouchers_CurrentAndChildren_ClosingOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_ApprovedVouchers_CurrentAndChildren_ByCenterProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_ApprovedVouchers_CurrentAndChildren_ClosingOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region AllVouchers_CurrentAndChildren_ClosingOption

        [Test]
        public async Task ProfitLoss_Periodic_AllVouchers_CurrentAndChildren_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_AllVouchers_CurrentAndChildren_ClosingOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_AllVouchers_CurrentAndChildren_ByCenter_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_AllVouchers_CurrentAndChildren_ClosingOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_AllVouchers_CurrentAndChildren_ByProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_AllVouchers_CurrentAndChildren_ClosingOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_AllVouchers_CurrentAndChildren_ByCenterProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_AllVouchers_CurrentAndChildren_ClosingOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region CheckedVouchers_CurrentBranch_TurnoverOption

        [Test]
        public async Task ProfitLoss_Periodic_CheckedVouchers_CurrentBranch_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CheckedVouchers_CurrentBranch_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CheckedVouchers_CurrentBranch_ByCenter_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CheckedVouchers_CurrentBranch_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CheckedVouchers_CurrentBranch_ByProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CheckedVouchers_CurrentBranch_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CheckedVouchers_CurrentBranch_ByCenterProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CheckedVouchers_CurrentBranch_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region FinalizedVouchers_CurrentBranch_TurnoverOption

        [Test]
        public async Task ProfitLoss_Periodic_FinalizedVouchers_CurrentBranch_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_FinalizedVouchers_CurrentBranch_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_FinalizedVouchers_CurrentBranch_ByCenter_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_FinalizedVouchers_CurrentBranch_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_FinalizedVouchers_CurrentBranch_ByProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_FinalizedVouchers_CurrentBranch_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_FinalizedVouchers_CurrentBranch_ByCenterProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_FinalizedVouchers_CurrentBranch_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region ConfirmedVouchers_CurrentBranch_TurnoverOption

        [Test]
        public async Task ProfitLoss_Periodic_ConfirmedVouchers_CurrentBranch_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_ConfirmedVouchers_CurrentBranch_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_ConfirmedVouchers_CurrentBranch_ByCenter_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_ConfirmedVouchers_CurrentBranch_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_ConfirmedVouchers_CurrentBranch_ByProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_ConfirmedVouchers_CurrentBranch_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_ConfirmedVouchers_CurrentBranch_ByCenterProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_ConfirmedVouchers_CurrentBranch_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region ApprovedVouchers_CurrentBranch_TurnoverOption

        [Test]
        public async Task ProfitLoss_Periodic_ApprovedVouchers_CurrentBranch_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_ApprovedVouchers_CurrentBranch_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_ApprovedVouchers_CurrentBranch_ByCenter_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_ApprovedVouchers_CurrentBranch_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_ApprovedVouchers_CurrentBranch_ByProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_ApprovedVouchers_CurrentBranch_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_ApprovedVouchers_CurrentBranch_ByCenterProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_ApprovedVouchers_CurrentBranch_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region AllVouchers_CurrentBranch_TurnoverOption

        [Test]
        public async Task ProfitLoss_Periodic_AllVouchers_CurrentBranch_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_AllVouchers_CurrentBranch_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_AllVouchers_CurrentBranch_ByCenter_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_AllVouchers_CurrentBranch_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_AllVouchers_CurrentBranch_ByProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_AllVouchers_CurrentBranch_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_AllVouchers_CurrentBranch_ByCenterProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_AllVouchers_CurrentBranch_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region CheckedVouchers_CurrentAndChildren_TurnoverOption

        [Test]
        public async Task ProfitLoss_Periodic_CheckedVouchers_CurrentAndChildren_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CheckedVouchers_CurrentAndChildren_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CheckedVouchers_CurrentAndChildren_ByCenter_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CheckedVouchers_CurrentAndChildren_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CheckedVouchers_CurrentAndChildren_ByProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CheckedVouchers_CurrentAndChildren_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CheckedVouchers_CurrentAndChildren_ByCenterProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CheckedVouchers_CurrentAndChildren_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region FinalizedVouchers_CurrentAndChildren_TurnoverOption

        [Test]
        public async Task ProfitLoss_Periodic_FinalizedVouchers_CurrentAndChildren_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_FinalizedVouchers_CurrentAndChildren_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_FinalizedVouchers_CurrentAndChildren_ByCenter_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_FinalizedVouchers_CurrentAndChildren_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_FinalizedVouchers_CurrentAndChildren_ByProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_FinalizedVouchers_CurrentAndChildren_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_FinalizedVouchers_CurrentAndChildren_ByCenterProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_FinalizedVouchers_CurrentAndChildren_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region ConfirmedVouchers_CurrentAndChildren_TurnoverOption

        [Test]
        public async Task ProfitLoss_Periodic_ConfirmedVouchers_CurrentAndChildren_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_ConfirmedVouchers_CurrentAndChildren_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_ConfirmedVouchers_CurrentAndChildren_ByCenter_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_ConfirmedVouchers_CurrentAndChildren_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_ConfirmedVouchers_CurrentAndChildren_ByProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_ConfirmedVouchers_CurrentAndChildren_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_ConfirmedVouchers_CurrentAndChildren_ByCenterProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_ConfirmedVouchers_CurrentAndChildren_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region ApprovedVouchers_CurrentAndChildren_TurnoverOption

        [Test]
        public async Task ProfitLoss_Periodic_ApprovedVouchers_CurrentAndChildren_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_ApprovedVouchers_CurrentAndChildren_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_ApprovedVouchers_CurrentAndChildren_ByCenter_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_ApprovedVouchers_CurrentAndChildren_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_ApprovedVouchers_CurrentAndChildren_ByProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_ApprovedVouchers_CurrentAndChildren_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_ApprovedVouchers_CurrentAndChildren_ByCenterProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_ApprovedVouchers_CurrentAndChildren_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region AllVouchers_CurrentAndChildren_TurnoverOption

        [Test]
        public async Task ProfitLoss_Periodic_AllVouchers_CurrentAndChildren_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_AllVouchers_CurrentAndChildren_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_AllVouchers_CurrentAndChildren_ByCenter_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_AllVouchers_CurrentAndChildren_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_AllVouchers_CurrentAndChildren_ByProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_AllVouchers_CurrentAndChildren_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_AllVouchers_CurrentAndChildren_ByCenterProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_AllVouchers_CurrentAndChildren_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region CheckedVouchers_CurrentBranch_ClosingOption_TurnoverOption

        [Test]
        public async Task ProfitLoss_Periodic_CheckedVouchers_CurrentBranch_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CheckedVouchers_CurrentBranch_ClosingOption_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CheckedVouchers_CurrentBranch_ByCenter_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CheckedVouchers_CurrentBranch_ClosingOption_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CheckedVouchers_CurrentBranch_ByProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CheckedVouchers_CurrentBranch_ClosingOption_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CheckedVouchers_CurrentBranch_ByCenterProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CheckedVouchers_CurrentBranch_ClosingOption_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region FinalizedVouchers_CurrentBranch_ClosingOption_TurnoverOption

        [Test]
        public async Task ProfitLoss_Periodic_FinalizedVouchers_CurrentBranch_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_FinalizedVouchers_CurrentBranch_ClosingOption_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_FinalizedVouchers_CurrentBranch_ByCenter_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_FinalizedVouchers_CurrentBranch_ClosingOption_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_FinalizedVouchers_CurrentBranch_ByProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_FinalizedVouchers_CurrentBranch_ClosingOption_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_FinalizedVouchers_CurrentBranch_ByCenterProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_FinalizedVouchers_CurrentBranch_ClosingOption_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region ConfirmedVouchers_CurrentBranch_ClosingOption_TurnoverOption

        [Test]
        public async Task ProfitLoss_Periodic_ConfirmedVouchers_CurrentBranch_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_ConfirmedVouchers_CurrentBranch_ClosingOption_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_ConfirmedVouchers_CurrentBranch_ByCenter_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_ConfirmedVouchers_CurrentBranch_ClosingOption_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_ConfirmedVouchers_CurrentBranch_ByProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_ConfirmedVouchers_CurrentBranch_ClosingOption_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_ConfirmedVouchers_CurrentBranch_ByCenterProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_ConfirmedVouchers_CurrentBranch_ClosingOption_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region ApprovedVouchers_CurrentBranch_ClosingOption_TurnoverOption

        [Test]
        public async Task ProfitLoss_Periodic_ApprovedVouchers_CurrentBranch_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_ApprovedVouchers_CurrentBranch_ClosingOption_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_ApprovedVouchers_CurrentBranch_ByCenter_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_ApprovedVouchers_CurrentBranch_ClosingOption_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_ApprovedVouchers_CurrentBranch_ByProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_ApprovedVouchers_CurrentBranch_ClosingOption_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_ApprovedVouchers_CurrentBranch_ByCenterProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_ApprovedVouchers_CurrentBranch_ClosingOption_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region AllVouchers_CurrentBranch_ClosingOption_TurnoverOption

        [Test]
        public async Task ProfitLoss_Periodic_AllVouchers_CurrentBranch_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_AllVouchers_CurrentBranch_ClosingOption_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_AllVouchers_CurrentBranch_ByCenter_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_AllVouchers_CurrentBranch_ClosingOption_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_AllVouchers_CurrentBranch_ByProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_AllVouchers_CurrentBranch_ClosingOption_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_AllVouchers_CurrentBranch_ByCenterProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_AllVouchers_CurrentBranch_ClosingOption_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region CheckedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption

        [Test]
        public async Task ProfitLoss_Periodic_CheckedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CheckedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CheckedVouchers_CurrentAndChildren_ByCenter_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CheckedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CheckedVouchers_CurrentAndChildren_ByProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CheckedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CheckedVouchers_CurrentAndChildren_ByCenterProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CheckedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region FinalizedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption

        [Test]
        public async Task ProfitLoss_Periodic_FinalizedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_FinalizedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_FinalizedVouchers_CurrentAndChildren_ByCenter_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_FinalizedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_FinalizedVouchers_CurrentAndChildren_ByProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_FinalizedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_FinalizedVouchers_CurrentAndChildren_ByCenterProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_FinalizedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region ConfirmedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption

        [Test]
        public async Task ProfitLoss_Periodic_ConfirmedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_ConfirmedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_ConfirmedVouchers_CurrentAndChildren_ByCenter_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_ConfirmedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_ConfirmedVouchers_CurrentAndChildren_ByProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_ConfirmedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_ConfirmedVouchers_CurrentAndChildren_ByCenterProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_ConfirmedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region ApprovedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption

        [Test]
        public async Task ProfitLoss_Periodic_ApprovedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_ApprovedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_ApprovedVouchers_CurrentAndChildren_ByCenter_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_ApprovedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_ApprovedVouchers_CurrentAndChildren_ByProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_ApprovedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_ApprovedVouchers_CurrentAndChildren_ByCenterProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_ApprovedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region AllVouchers_CurrentAndChildren_ClosingOption_TurnoverOption

        [Test]
        public async Task ProfitLoss_Periodic_AllVouchers_CurrentAndChildren_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_AllVouchers_CurrentAndChildren_ClosingOption_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_AllVouchers_CurrentAndChildren_ByCenter_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_AllVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_AllVouchers_CurrentAndChildren_ByProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_AllVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_AllVouchers_CurrentAndChildren_ByCenterProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_AllVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #endregion

        #region Profit-Loss By Branches Tests

        #region CheckedVouchers_CurrentBranch_NoOptions

        [Test]
        public async Task ProfitLoss_Periodic_CompareBranches_CheckedVouchers_CurrentBranch_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareBranches_CheckedVouchers_CurrentBranch_NoOptions();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareBranches_CheckedVouchers_CurrentBranch_ByCenter_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareBranches_CheckedVouchers_CurrentBranch_NoOptions(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareBranches_CheckedVouchers_CurrentBranch_ByProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareBranches_CheckedVouchers_CurrentBranch_NoOptions(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareBranches_CheckedVouchers_CurrentBranch_ByCenterProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareBranches_CheckedVouchers_CurrentBranch_NoOptions(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region FinalizedVouchers_CurrentBranch_NoOptions

        [Test]
        public async Task ProfitLoss_Periodic_CompareBranches_FinalizedVouchers_CurrentBranch_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareBranches_FinalizedVouchers_CurrentBranch_NoOptions();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareBranches_FinalizedVouchers_CurrentBranch_ByCenter_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareBranches_FinalizedVouchers_CurrentBranch_NoOptions(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareBranches_FinalizedVouchers_CurrentBranch_ByProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareBranches_FinalizedVouchers_CurrentBranch_NoOptions(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareBranches_FinalizedVouchers_CurrentBranch_ByCenterProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareBranches_FinalizedVouchers_CurrentBranch_NoOptions(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region ConfirmedVouchers_CurrentBranch_NoOptions

        [Test]
        public async Task ProfitLoss_Periodic_CompareBranches_ConfirmedVouchers_CurrentBranch_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareBranches_ConfirmedVouchers_CurrentBranch_NoOptions();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareBranches_ConfirmedVouchers_CurrentBranch_ByCenter_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareBranches_ConfirmedVouchers_CurrentBranch_NoOptions(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareBranches_ConfirmedVouchers_CurrentBranch_ByProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareBranches_ConfirmedVouchers_CurrentBranch_NoOptions(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareBranches_ConfirmedVouchers_CurrentBranch_ByCenterProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareBranches_ConfirmedVouchers_CurrentBranch_NoOptions(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region ApprovedVouchers_CurrentBranch_NoOptions

        [Test]
        public async Task ProfitLoss_Periodic_CompareBranches_ApprovedVouchers_CurrentBranch_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareBranches_ApprovedVouchers_CurrentBranch_NoOptions();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareBranches_ApprovedVouchers_CurrentBranch_ByCenter_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareBranches_ApprovedVouchers_CurrentBranch_NoOptions(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareBranches_ApprovedVouchers_CurrentBranch_ByProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareBranches_ApprovedVouchers_CurrentBranch_NoOptions(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareBranches_ApprovedVouchers_CurrentBranch_ByCenterProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareBranches_ApprovedVouchers_CurrentBranch_NoOptions(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region AllVouchers_CurrentBranch_NoOptions

        [Test]
        public async Task ProfitLoss_Periodic_CompareBranches_AllVouchers_CurrentBranch_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareBranches_AllVouchers_CurrentBranch_NoOptions();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareBranches_AllVouchers_CurrentBranch_ByCenter_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareBranches_AllVouchers_CurrentBranch_NoOptions(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareBranches_AllVouchers_CurrentBranch_ByProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareBranches_AllVouchers_CurrentBranch_NoOptions(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareBranches_AllVouchers_CurrentBranch_ByCenterProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareBranches_AllVouchers_CurrentBranch_NoOptions(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region CheckedVouchers_CurrentAndChildren_NoOptions

        [Test]
        public async Task ProfitLoss_Periodic_CompareBranches_CheckedVouchers_CurrentAndChildren_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareBranches_CheckedVouchers_CurrentAndChildren_NoOptions();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareBranches_CheckedVouchers_CurrentAndChildren_ByCenter_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareBranches_CheckedVouchers_CurrentAndChildren_NoOptions(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareBranches_CheckedVouchers_CurrentAndChildren_ByProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareBranches_CheckedVouchers_CurrentAndChildren_NoOptions(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareBranches_CheckedVouchers_CurrentAndChildren_ByCenterProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareBranches_CheckedVouchers_CurrentAndChildren_NoOptions(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region FinalizedVouchers_CurrentAndChildren_NoOptions

        [Test]
        public async Task ProfitLoss_Periodic_CompareBranches_FinalizedVouchers_CurrentAndChildren_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareBranches_FinalizedVouchers_CurrentAndChildren_NoOptions();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareBranches_FinalizedVouchers_CurrentAndChildren_ByCenter_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareBranches_FinalizedVouchers_CurrentAndChildren_NoOptions(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareBranches_FinalizedVouchers_CurrentAndChildren_ByProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareBranches_FinalizedVouchers_CurrentAndChildren_NoOptions(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareBranches_FinalizedVouchers_CurrentAndChildren_ByCenterProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareBranches_FinalizedVouchers_CurrentAndChildren_NoOptions(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region ConfirmedVouchers_CurrentAndChildren_NoOptions

        [Test]
        public async Task ProfitLoss_Periodic_CompareBranches_ConfirmedVouchers_CurrentAndChildren_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareBranches_ConfirmedVouchers_CurrentAndChildren_NoOptions();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareBranches_ConfirmedVouchers_CurrentAndChildren_ByCenter_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareBranches_ConfirmedVouchers_CurrentAndChildren_NoOptions(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareBranches_ConfirmedVouchers_CurrentAndChildren_ByProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareBranches_ConfirmedVouchers_CurrentAndChildren_NoOptions(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareBranches_ConfirmedVouchers_CurrentAndChildren_ByCenterProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareBranches_ConfirmedVouchers_CurrentAndChildren_NoOptions(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region ApprovedVouchers_CurrentAndChildren_NoOptions

        [Test]
        public async Task ProfitLoss_Periodic_CompareBranches_ApprovedVouchers_CurrentAndChildren_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareBranches_ApprovedVouchers_CurrentAndChildren_NoOptions();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareBranches_ApprovedVouchers_CurrentAndChildren_ByCenter_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareBranches_ApprovedVouchers_CurrentAndChildren_NoOptions(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareBranches_ApprovedVouchers_CurrentAndChildren_ByProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareBranches_ApprovedVouchers_CurrentAndChildren_NoOptions(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareBranches_ApprovedVouchers_CurrentAndChildren_ByCenterProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareBranches_ApprovedVouchers_CurrentAndChildren_NoOptions(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region AllVouchers_CurrentAndChildren_NoOptions

        [Test]
        public async Task ProfitLoss_Periodic_CompareBranches_AllVouchers_CurrentAndChildren_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareBranches_AllVouchers_CurrentAndChildren_NoOptions();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareBranches_AllVouchers_CurrentAndChildren_ByCenter_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareBranches_AllVouchers_CurrentAndChildren_NoOptions(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareBranches_AllVouchers_CurrentAndChildren_ByProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareBranches_AllVouchers_CurrentAndChildren_NoOptions(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareBranches_AllVouchers_CurrentAndChildren_ByCenterProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareBranches_AllVouchers_CurrentAndChildren_NoOptions(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region CheckedVouchers_CurrentBranch_ClosingOption

        [Test]
        public async Task ProfitLoss_Periodic_CompareBranches_CheckedVouchers_CurrentBranch_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareBranches_CheckedVouchers_CurrentBranch_ClosingOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareBranches_CheckedVouchers_CurrentBranch_ByCenter_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareBranches_CheckedVouchers_CurrentBranch_ClosingOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareBranches_CheckedVouchers_CurrentBranch_ByProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareBranches_CheckedVouchers_CurrentBranch_ClosingOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareBranches_CheckedVouchers_CurrentBranch_ByCenterProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareBranches_CheckedVouchers_CurrentBranch_ClosingOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region FinalizedVouchers_CurrentBranch_ClosingOption

        [Test]
        public async Task ProfitLoss_Periodic_CompareBranches_FinalizedVouchers_CurrentBranch_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareBranches_FinalizedVouchers_CurrentBranch_ClosingOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareBranches_FinalizedVouchers_CurrentBranch_ByCenter_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareBranches_FinalizedVouchers_CurrentBranch_ClosingOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareBranches_FinalizedVouchers_CurrentBranch_ByProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareBranches_FinalizedVouchers_CurrentBranch_ClosingOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareBranches_FinalizedVouchers_CurrentBranch_ByCenterProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareBranches_FinalizedVouchers_CurrentBranch_ClosingOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region ConfirmedVouchers_CurrentBranch_ClosingOption

        [Test]
        public async Task ProfitLoss_Periodic_CompareBranches_ConfirmedVouchers_CurrentBranch_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareBranches_ConfirmedVouchers_CurrentBranch_ClosingOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareBranches_ConfirmedVouchers_CurrentBranch_ByCenter_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareBranches_ConfirmedVouchers_CurrentBranch_ClosingOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareBranches_ConfirmedVouchers_CurrentBranch_ByProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareBranches_ConfirmedVouchers_CurrentBranch_ClosingOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareBranches_ConfirmedVouchers_CurrentBranch_ByCenterProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareBranches_ConfirmedVouchers_CurrentBranch_ClosingOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region ApprovedVouchers_CurrentBranch_ClosingOption

        [Test]
        public async Task ProfitLoss_Periodic_CompareBranches_ApprovedVouchers_CurrentBranch_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareBranches_ApprovedVouchers_CurrentBranch_ClosingOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareBranches_ApprovedVouchers_CurrentBranch_ByCenter_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareBranches_ApprovedVouchers_CurrentBranch_ClosingOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareBranches_ApprovedVouchers_CurrentBranch_ByProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareBranches_ApprovedVouchers_CurrentBranch_ClosingOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareBranches_ApprovedVouchers_CurrentBranch_ByCenterProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareBranches_ApprovedVouchers_CurrentBranch_ClosingOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region AllVouchers_CurrentBranch_ClosingOption

        [Test]
        public async Task ProfitLoss_Periodic_CompareBranches_AllVouchers_CurrentBranch_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareBranches_AllVouchers_CurrentBranch_ClosingOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareBranches_AllVouchers_CurrentBranch_ByCenter_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareBranches_AllVouchers_CurrentBranch_ClosingOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareBranches_AllVouchers_CurrentBranch_ByProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareBranches_AllVouchers_CurrentBranch_ClosingOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareBranches_AllVouchers_CurrentBranch_ByCenterProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareBranches_AllVouchers_CurrentBranch_ClosingOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region CheckedVouchers_CurrentAndChildren_ClosingOption

        [Test]
        public async Task ProfitLoss_Periodic_CompareBranches_CheckedVouchers_CurrentAndChildren_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareBranches_CheckedVouchers_CurrentAndChildren_ClosingOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareBranches_CheckedVouchers_CurrentAndChildren_ByCenter_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareBranches_CheckedVouchers_CurrentAndChildren_ClosingOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareBranches_CheckedVouchers_CurrentAndChildren_ByProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareBranches_CheckedVouchers_CurrentAndChildren_ClosingOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareBranches_CheckedVouchers_CurrentAndChildren_ByCenterProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareBranches_CheckedVouchers_CurrentAndChildren_ClosingOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region FinalizedVouchers_CurrentAndChildren_ClosingOption

        [Test]
        public async Task ProfitLoss_Periodic_CompareBranches_FinalizedVouchers_CurrentAndChildren_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareBranches_FinalizedVouchers_CurrentAndChildren_ClosingOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareBranches_FinalizedVouchers_CurrentAndChildren_ByCenter_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareBranches_FinalizedVouchers_CurrentAndChildren_ClosingOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareBranches_FinalizedVouchers_CurrentAndChildren_ByProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareBranches_FinalizedVouchers_CurrentAndChildren_ClosingOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareBranches_FinalizedVouchers_CurrentAndChildren_ByCenterProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareBranches_FinalizedVouchers_CurrentAndChildren_ClosingOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region ConfirmedVouchers_CurrentAndChildren_ClosingOption

        [Test]
        public async Task ProfitLoss_Periodic_CompareBranches_ConfirmedVouchers_CurrentAndChildren_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareBranches_ConfirmedVouchers_CurrentAndChildren_ClosingOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareBranches_ConfirmedVouchers_CurrentAndChildren_ByCenter_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareBranches_ConfirmedVouchers_CurrentAndChildren_ClosingOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareBranches_ConfirmedVouchers_CurrentAndChildren_ByProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareBranches_ConfirmedVouchers_CurrentAndChildren_ClosingOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareBranches_ConfirmedVouchers_CurrentAndChildren_ByCenterProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareBranches_ConfirmedVouchers_CurrentAndChildren_ClosingOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region ApprovedVouchers_CurrentAndChildren_ClosingOption

        [Test]
        public async Task ProfitLoss_Periodic_CompareBranches_ApprovedVouchers_CurrentAndChildren_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareBranches_ApprovedVouchers_CurrentAndChildren_ClosingOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareBranches_ApprovedVouchers_CurrentAndChildren_ByCenter_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareBranches_ApprovedVouchers_CurrentAndChildren_ClosingOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareBranches_ApprovedVouchers_CurrentAndChildren_ByProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareBranches_ApprovedVouchers_CurrentAndChildren_ClosingOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareBranches_ApprovedVouchers_CurrentAndChildren_ByCenterProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareBranches_ApprovedVouchers_CurrentAndChildren_ClosingOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region AllVouchers_CurrentAndChildren_ClosingOption

        [Test]
        public async Task ProfitLoss_Periodic_CompareBranches_AllVouchers_CurrentAndChildren_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareBranches_AllVouchers_CurrentAndChildren_ClosingOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareBranches_AllVouchers_CurrentAndChildren_ByCenter_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareBranches_AllVouchers_CurrentAndChildren_ClosingOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareBranches_AllVouchers_CurrentAndChildren_ByProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareBranches_AllVouchers_CurrentAndChildren_ClosingOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareBranches_AllVouchers_CurrentAndChildren_ByCenterProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareBranches_AllVouchers_CurrentAndChildren_ClosingOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region CheckedVouchers_CurrentBranch_TurnoverOption

        [Test]
        public async Task ProfitLoss_Periodic_CompareBranches_CheckedVouchers_CurrentBranch_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareBranches_CheckedVouchers_CurrentBranch_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareBranches_CheckedVouchers_CurrentBranch_ByCenter_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareBranches_CheckedVouchers_CurrentBranch_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareBranches_CheckedVouchers_CurrentBranch_ByProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareBranches_CheckedVouchers_CurrentBranch_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareBranches_CheckedVouchers_CurrentBranch_ByCenterProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareBranches_CheckedVouchers_CurrentBranch_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region FinalizedVouchers_CurrentBranch_TurnoverOption

        [Test]
        public async Task ProfitLoss_Periodic_CompareBranches_FinalizedVouchers_CurrentBranch_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareBranches_FinalizedVouchers_CurrentBranch_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareBranches_FinalizedVouchers_CurrentBranch_ByCenter_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareBranches_FinalizedVouchers_CurrentBranch_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareBranches_FinalizedVouchers_CurrentBranch_ByProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareBranches_FinalizedVouchers_CurrentBranch_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareBranches_FinalizedVouchers_CurrentBranch_ByCenterProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareBranches_FinalizedVouchers_CurrentBranch_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region ConfirmedVouchers_CurrentBranch_TurnoverOption

        [Test]
        public async Task ProfitLoss_Periodic_CompareBranches_ConfirmedVouchers_CurrentBranch_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareBranches_ConfirmedVouchers_CurrentBranch_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareBranches_ConfirmedVouchers_CurrentBranch_ByCenter_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareBranches_ConfirmedVouchers_CurrentBranch_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareBranches_ConfirmedVouchers_CurrentBranch_ByProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareBranches_ConfirmedVouchers_CurrentBranch_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareBranches_ConfirmedVouchers_CurrentBranch_ByCenterProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareBranches_ConfirmedVouchers_CurrentBranch_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region ApprovedVouchers_CurrentBranch_TurnoverOption

        [Test]
        public async Task ProfitLoss_Periodic_CompareBranches_ApprovedVouchers_CurrentBranch_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareBranches_ApprovedVouchers_CurrentBranch_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareBranches_ApprovedVouchers_CurrentBranch_ByCenter_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareBranches_ApprovedVouchers_CurrentBranch_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareBranches_ApprovedVouchers_CurrentBranch_ByProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareBranches_ApprovedVouchers_CurrentBranch_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareBranches_ApprovedVouchers_CurrentBranch_ByCenterProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareBranches_ApprovedVouchers_CurrentBranch_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region AllVouchers_CurrentBranch_TurnoverOption

        [Test]
        public async Task ProfitLoss_Periodic_CompareBranches_AllVouchers_CurrentBranch_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareBranches_AllVouchers_CurrentBranch_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareBranches_AllVouchers_CurrentBranch_ByCenter_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareBranches_AllVouchers_CurrentBranch_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareBranches_AllVouchers_CurrentBranch_ByProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareBranches_AllVouchers_CurrentBranch_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareBranches_AllVouchers_CurrentBranch_ByCenterProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareBranches_AllVouchers_CurrentBranch_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region CheckedVouchers_CurrentAndChildren_TurnoverOption

        [Test]
        public async Task ProfitLoss_Periodic_CompareBranches_CheckedVouchers_CurrentAndChildren_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareBranches_CheckedVouchers_CurrentAndChildren_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareBranches_CheckedVouchers_CurrentAndChildren_ByCenter_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareBranches_CheckedVouchers_CurrentAndChildren_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareBranches_CheckedVouchers_CurrentAndChildren_ByProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareBranches_CheckedVouchers_CurrentAndChildren_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareBranches_CheckedVouchers_CurrentAndChildren_ByCenterProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareBranches_CheckedVouchers_CurrentAndChildren_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region FinalizedVouchers_CurrentAndChildren_TurnoverOption

        [Test]
        public async Task ProfitLoss_Periodic_CompareBranches_FinalizedVouchers_CurrentAndChildren_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareBranches_FinalizedVouchers_CurrentAndChildren_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareBranches_FinalizedVouchers_CurrentAndChildren_ByCenter_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareBranches_FinalizedVouchers_CurrentAndChildren_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareBranches_FinalizedVouchers_CurrentAndChildren_ByProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareBranches_FinalizedVouchers_CurrentAndChildren_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareBranches_FinalizedVouchers_CurrentAndChildren_ByCenterProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareBranches_FinalizedVouchers_CurrentAndChildren_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region ConfirmedVouchers_CurrentAndChildren_TurnoverOption

        [Test]
        public async Task ProfitLoss_Periodic_CompareBranches_ConfirmedVouchers_CurrentAndChildren_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareBranches_ConfirmedVouchers_CurrentAndChildren_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareBranches_ConfirmedVouchers_CurrentAndChildren_ByCenter_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareBranches_ConfirmedVouchers_CurrentAndChildren_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareBranches_ConfirmedVouchers_CurrentAndChildren_ByProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareBranches_ConfirmedVouchers_CurrentAndChildren_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareBranches_ConfirmedVouchers_CurrentAndChildren_ByCenterProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareBranches_ConfirmedVouchers_CurrentAndChildren_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region ApprovedVouchers_CurrentAndChildren_TurnoverOption

        [Test]
        public async Task ProfitLoss_Periodic_CompareBranches_ApprovedVouchers_CurrentAndChildren_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareBranches_ApprovedVouchers_CurrentAndChildren_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareBranches_ApprovedVouchers_CurrentAndChildren_ByCenter_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareBranches_ApprovedVouchers_CurrentAndChildren_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareBranches_ApprovedVouchers_CurrentAndChildren_ByProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareBranches_ApprovedVouchers_CurrentAndChildren_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareBranches_ApprovedVouchers_CurrentAndChildren_ByCenterProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareBranches_ApprovedVouchers_CurrentAndChildren_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region AllVouchers_CurrentAndChildren_TurnoverOption

        [Test]
        public async Task ProfitLoss_Periodic_CompareBranches_AllVouchers_CurrentAndChildren_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareBranches_AllVouchers_CurrentAndChildren_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareBranches_AllVouchers_CurrentAndChildren_ByCenter_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareBranches_AllVouchers_CurrentAndChildren_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareBranches_AllVouchers_CurrentAndChildren_ByProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareBranches_AllVouchers_CurrentAndChildren_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareBranches_AllVouchers_CurrentAndChildren_ByCenterProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareBranches_AllVouchers_CurrentAndChildren_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region CheckedVouchers_CurrentBranch_ClosingOption_TurnoverOption

        [Test]
        public async Task ProfitLoss_Periodic_CompareBranches_CheckedVouchers_CurrentBranch_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareBranches_CheckedVouchers_CurrentBranch_ClosingOption_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareBranches_CheckedVouchers_CurrentBranch_ByCenter_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareBranches_CheckedVouchers_CurrentBranch_ClosingOption_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareBranches_CheckedVouchers_CurrentBranch_ByProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareBranches_CheckedVouchers_CurrentBranch_ClosingOption_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareBranches_CheckedVouchers_CurrentBranch_ByCenterProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareBranches_CheckedVouchers_CurrentBranch_ClosingOption_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region FinalizedVouchers_CurrentBranch_ClosingOption_TurnoverOption

        [Test]
        public async Task ProfitLoss_Periodic_CompareBranches_FinalizedVouchers_CurrentBranch_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareBranches_FinalizedVouchers_CurrentBranch_ClosingOption_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareBranches_FinalizedVouchers_CurrentBranch_ByCenter_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareBranches_FinalizedVouchers_CurrentBranch_ClosingOption_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareBranches_FinalizedVouchers_CurrentBranch_ByProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareBranches_FinalizedVouchers_CurrentBranch_ClosingOption_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareBranches_FinalizedVouchers_CurrentBranch_ByCenterProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareBranches_FinalizedVouchers_CurrentBranch_ClosingOption_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region ConfirmedVouchers_CurrentBranch_ClosingOption_TurnoverOption

        [Test]
        public async Task ProfitLoss_Periodic_CompareBranches_ConfirmedVouchers_CurrentBranch_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareBranches_ConfirmedVouchers_CurrentBranch_ClosingOption_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareBranches_ConfirmedVouchers_CurrentBranch_ByCenter_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareBranches_ConfirmedVouchers_CurrentBranch_ClosingOption_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareBranches_ConfirmedVouchers_CurrentBranch_ByProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareBranches_ConfirmedVouchers_CurrentBranch_ClosingOption_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareBranches_ConfirmedVouchers_CurrentBranch_ByCenterProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareBranches_ConfirmedVouchers_CurrentBranch_ClosingOption_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region ApprovedVouchers_CurrentBranch_ClosingOption_TurnoverOption

        [Test]
        public async Task ProfitLoss_Periodic_CompareBranches_ApprovedVouchers_CurrentBranch_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareBranches_ApprovedVouchers_CurrentBranch_ClosingOption_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareBranches_ApprovedVouchers_CurrentBranch_ByCenter_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareBranches_ApprovedVouchers_CurrentBranch_ClosingOption_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareBranches_ApprovedVouchers_CurrentBranch_ByProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareBranches_ApprovedVouchers_CurrentBranch_ClosingOption_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareBranches_ApprovedVouchers_CurrentBranch_ByCenterProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareBranches_ApprovedVouchers_CurrentBranch_ClosingOption_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region AllVouchers_CurrentBranch_ClosingOption_TurnoverOption

        [Test]
        public async Task ProfitLoss_Periodic_CompareBranches_AllVouchers_CurrentBranch_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareBranches_AllVouchers_CurrentBranch_ClosingOption_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareBranches_AllVouchers_CurrentBranch_ByCenter_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareBranches_AllVouchers_CurrentBranch_ClosingOption_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareBranches_AllVouchers_CurrentBranch_ByProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareBranches_AllVouchers_CurrentBranch_ClosingOption_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareBranches_AllVouchers_CurrentBranch_ByCenterProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareBranches_AllVouchers_CurrentBranch_ClosingOption_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region CheckedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption

        [Test]
        public async Task ProfitLoss_Periodic_CompareBranches_CheckedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareBranches_CheckedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareBranches_CheckedVouchers_CurrentAndChildren_ByCenter_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareBranches_CheckedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareBranches_CheckedVouchers_CurrentAndChildren_ByProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareBranches_CheckedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareBranches_CheckedVouchers_CurrentAndChildren_ByCenterProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareBranches_CheckedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region FinalizedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption

        [Test]
        public async Task ProfitLoss_Periodic_CompareBranches_FinalizedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareBranches_FinalizedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareBranches_FinalizedVouchers_CurrentAndChildren_ByCenter_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareBranches_FinalizedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareBranches_FinalizedVouchers_CurrentAndChildren_ByProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareBranches_FinalizedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareBranches_FinalizedVouchers_CurrentAndChildren_ByCenterProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareBranches_FinalizedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region ConfirmedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption

        [Test]
        public async Task ProfitLoss_Periodic_CompareBranches_ConfirmedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareBranches_ConfirmedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareBranches_ConfirmedVouchers_CurrentAndChildren_ByCenter_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareBranches_ConfirmedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareBranches_ConfirmedVouchers_CurrentAndChildren_ByProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareBranches_ConfirmedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareBranches_ConfirmedVouchers_CurrentAndChildren_ByCenterProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareBranches_ConfirmedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region ApprovedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption

        [Test]
        public async Task ProfitLoss_Periodic_CompareBranches_ApprovedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareBranches_ApprovedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareBranches_ApprovedVouchers_CurrentAndChildren_ByCenter_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareBranches_ApprovedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareBranches_ApprovedVouchers_CurrentAndChildren_ByProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareBranches_ApprovedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareBranches_ApprovedVouchers_CurrentAndChildren_ByCenterProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareBranches_ApprovedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region AllVouchers_CurrentAndChildren_ClosingOption_TurnoverOption

        [Test]
        public async Task ProfitLoss_Periodic_CompareBranches_AllVouchers_CurrentAndChildren_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareBranches_AllVouchers_CurrentAndChildren_ClosingOption_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareBranches_AllVouchers_CurrentAndChildren_ByCenter_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareBranches_AllVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareBranches_AllVouchers_CurrentAndChildren_ByProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareBranches_AllVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareBranches_AllVouchers_CurrentAndChildren_ByCenterProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareBranches_AllVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #endregion

        #region Profit-Loss By Projects Tests

        #region CheckedVouchers_CurrentBranch_NoOptions

        [Test]
        public async Task ProfitLoss_Periodic_CompareProjects_CheckedVouchers_CurrentBranch_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareProjects_CheckedVouchers_CurrentBranch_NoOptions();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareProjects_CheckedVouchers_CurrentBranch_ByCenter_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareProjects_CheckedVouchers_CurrentBranch_NoOptions(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareProjects_CheckedVouchers_CurrentBranch_ByProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareProjects_CheckedVouchers_CurrentBranch_NoOptions(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareProjects_CheckedVouchers_CurrentBranch_ByCenterProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareProjects_CheckedVouchers_CurrentBranch_NoOptions(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region FinalizedVouchers_CurrentBranch_NoOptions

        [Test]
        public async Task ProfitLoss_Periodic_CompareProjects_FinalizedVouchers_CurrentBranch_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareProjects_FinalizedVouchers_CurrentBranch_NoOptions();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareProjects_FinalizedVouchers_CurrentBranch_ByCenter_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareProjects_FinalizedVouchers_CurrentBranch_NoOptions(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareProjects_FinalizedVouchers_CurrentBranch_ByProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareProjects_FinalizedVouchers_CurrentBranch_NoOptions(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareProjects_FinalizedVouchers_CurrentBranch_ByCenterProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareProjects_FinalizedVouchers_CurrentBranch_NoOptions(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region ConfirmedVouchers_CurrentBranch_NoOptions

        [Test]
        public async Task ProfitLoss_Periodic_CompareProjects_ConfirmedVouchers_CurrentBranch_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareProjects_ConfirmedVouchers_CurrentBranch_NoOptions();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareProjects_ConfirmedVouchers_CurrentBranch_ByCenter_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareProjects_ConfirmedVouchers_CurrentBranch_NoOptions(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareProjects_ConfirmedVouchers_CurrentBranch_ByProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareProjects_ConfirmedVouchers_CurrentBranch_NoOptions(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareProjects_ConfirmedVouchers_CurrentBranch_ByCenterProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareProjects_ConfirmedVouchers_CurrentBranch_NoOptions(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region ApprovedVouchers_CurrentBranch_NoOptions

        [Test]
        public async Task ProfitLoss_Periodic_CompareProjects_ApprovedVouchers_CurrentBranch_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareProjects_ApprovedVouchers_CurrentBranch_NoOptions();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareProjects_ApprovedVouchers_CurrentBranch_ByCenter_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareProjects_ApprovedVouchers_CurrentBranch_NoOptions(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareProjects_ApprovedVouchers_CurrentBranch_ByProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareProjects_ApprovedVouchers_CurrentBranch_NoOptions(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareProjects_ApprovedVouchers_CurrentBranch_ByCenterProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareProjects_ApprovedVouchers_CurrentBranch_NoOptions(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region AllVouchers_CurrentBranch_NoOptions

        [Test]
        public async Task ProfitLoss_Periodic_CompareProjects_AllVouchers_CurrentBranch_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareProjects_AllVouchers_CurrentBranch_NoOptions();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareProjects_AllVouchers_CurrentBranch_ByCenter_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareProjects_AllVouchers_CurrentBranch_NoOptions(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareProjects_AllVouchers_CurrentBranch_ByProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareProjects_AllVouchers_CurrentBranch_NoOptions(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareProjects_AllVouchers_CurrentBranch_ByCenterProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareProjects_AllVouchers_CurrentBranch_NoOptions(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region CheckedVouchers_CurrentAndChildren_NoOptions

        [Test]
        public async Task ProfitLoss_Periodic_CompareProjects_CheckedVouchers_CurrentAndChildren_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareProjects_CheckedVouchers_CurrentAndChildren_NoOptions();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareProjects_CheckedVouchers_CurrentAndChildren_ByCenter_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareProjects_CheckedVouchers_CurrentAndChildren_NoOptions(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareProjects_CheckedVouchers_CurrentAndChildren_ByProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareProjects_CheckedVouchers_CurrentAndChildren_NoOptions(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareProjects_CheckedVouchers_CurrentAndChildren_ByCenterProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareProjects_CheckedVouchers_CurrentAndChildren_NoOptions(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region FinalizedVouchers_CurrentAndChildren_NoOptions

        [Test]
        public async Task ProfitLoss_Periodic_CompareProjects_FinalizedVouchers_CurrentAndChildren_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareProjects_FinalizedVouchers_CurrentAndChildren_NoOptions();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareProjects_FinalizedVouchers_CurrentAndChildren_ByCenter_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareProjects_FinalizedVouchers_CurrentAndChildren_NoOptions(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareProjects_FinalizedVouchers_CurrentAndChildren_ByProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareProjects_FinalizedVouchers_CurrentAndChildren_NoOptions(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareProjects_FinalizedVouchers_CurrentAndChildren_ByCenterProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareProjects_FinalizedVouchers_CurrentAndChildren_NoOptions(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region ConfirmedVouchers_CurrentAndChildren_NoOptions

        [Test]
        public async Task ProfitLoss_Periodic_CompareProjects_ConfirmedVouchers_CurrentAndChildren_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareProjects_ConfirmedVouchers_CurrentAndChildren_NoOptions();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareProjects_ConfirmedVouchers_CurrentAndChildren_ByCenter_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareProjects_ConfirmedVouchers_CurrentAndChildren_NoOptions(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareProjects_ConfirmedVouchers_CurrentAndChildren_ByProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareProjects_ConfirmedVouchers_CurrentAndChildren_NoOptions(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareProjects_ConfirmedVouchers_CurrentAndChildren_ByCenterProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareProjects_ConfirmedVouchers_CurrentAndChildren_NoOptions(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region ApprovedVouchers_CurrentAndChildren_NoOptions

        [Test]
        public async Task ProfitLoss_Periodic_CompareProjects_ApprovedVouchers_CurrentAndChildren_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareProjects_ApprovedVouchers_CurrentAndChildren_NoOptions();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareProjects_ApprovedVouchers_CurrentAndChildren_ByCenter_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareProjects_ApprovedVouchers_CurrentAndChildren_NoOptions(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareProjects_ApprovedVouchers_CurrentAndChildren_ByProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareProjects_ApprovedVouchers_CurrentAndChildren_NoOptions(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareProjects_ApprovedVouchers_CurrentAndChildren_ByCenterProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareProjects_ApprovedVouchers_CurrentAndChildren_NoOptions(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region AllVouchers_CurrentAndChildren_NoOptions

        [Test]
        public async Task ProfitLoss_Periodic_CompareProjects_AllVouchers_CurrentAndChildren_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareProjects_AllVouchers_CurrentAndChildren_NoOptions();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareProjects_AllVouchers_CurrentAndChildren_ByCenter_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareProjects_AllVouchers_CurrentAndChildren_NoOptions(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareProjects_AllVouchers_CurrentAndChildren_ByProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareProjects_AllVouchers_CurrentAndChildren_NoOptions(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareProjects_AllVouchers_CurrentAndChildren_ByCenterProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareProjects_AllVouchers_CurrentAndChildren_NoOptions(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region CheckedVouchers_CurrentBranch_ClosingOption

        [Test]
        public async Task ProfitLoss_Periodic_CompareProjects_CheckedVouchers_CurrentBranch_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareProjects_CheckedVouchers_CurrentBranch_ClosingOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareProjects_CheckedVouchers_CurrentBranch_ByCenter_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareProjects_CheckedVouchers_CurrentBranch_ClosingOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareProjects_CheckedVouchers_CurrentBranch_ByProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareProjects_CheckedVouchers_CurrentBranch_ClosingOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareProjects_CheckedVouchers_CurrentBranch_ByCenterProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareProjects_CheckedVouchers_CurrentBranch_ClosingOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region FinalizedVouchers_CurrentBranch_ClosingOption

        [Test]
        public async Task ProfitLoss_Periodic_CompareProjects_FinalizedVouchers_CurrentBranch_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareProjects_FinalizedVouchers_CurrentBranch_ClosingOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareProjects_FinalizedVouchers_CurrentBranch_ByCenter_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareProjects_FinalizedVouchers_CurrentBranch_ClosingOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareProjects_FinalizedVouchers_CurrentBranch_ByProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareProjects_FinalizedVouchers_CurrentBranch_ClosingOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareProjects_FinalizedVouchers_CurrentBranch_ByCenterProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareProjects_FinalizedVouchers_CurrentBranch_ClosingOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region ConfirmedVouchers_CurrentBranch_ClosingOption

        [Test]
        public async Task ProfitLoss_Periodic_CompareProjects_ConfirmedVouchers_CurrentBranch_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareProjects_ConfirmedVouchers_CurrentBranch_ClosingOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareProjects_ConfirmedVouchers_CurrentBranch_ByCenter_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareProjects_ConfirmedVouchers_CurrentBranch_ClosingOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareProjects_ConfirmedVouchers_CurrentBranch_ByProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareProjects_ConfirmedVouchers_CurrentBranch_ClosingOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareProjects_ConfirmedVouchers_CurrentBranch_ByCenterProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareProjects_ConfirmedVouchers_CurrentBranch_ClosingOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region ApprovedVouchers_CurrentBranch_ClosingOption

        [Test]
        public async Task ProfitLoss_Periodic_CompareProjects_ApprovedVouchers_CurrentBranch_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareProjects_ApprovedVouchers_CurrentBranch_ClosingOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareProjects_ApprovedVouchers_CurrentBranch_ByCenter_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareProjects_ApprovedVouchers_CurrentBranch_ClosingOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareProjects_ApprovedVouchers_CurrentBranch_ByProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareProjects_ApprovedVouchers_CurrentBranch_ClosingOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareProjects_ApprovedVouchers_CurrentBranch_ByCenterProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareProjects_ApprovedVouchers_CurrentBranch_ClosingOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region AllVouchers_CurrentBranch_ClosingOption

        [Test]
        public async Task ProfitLoss_Periodic_CompareProjects_AllVouchers_CurrentBranch_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareProjects_AllVouchers_CurrentBranch_ClosingOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareProjects_AllVouchers_CurrentBranch_ByCenter_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareProjects_AllVouchers_CurrentBranch_ClosingOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareProjects_AllVouchers_CurrentBranch_ByProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareProjects_AllVouchers_CurrentBranch_ClosingOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareProjects_AllVouchers_CurrentBranch_ByCenterProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareProjects_AllVouchers_CurrentBranch_ClosingOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region CheckedVouchers_CurrentAndChildren_ClosingOption

        [Test]
        public async Task ProfitLoss_Periodic_CompareProjects_CheckedVouchers_CurrentAndChildren_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareProjects_CheckedVouchers_CurrentAndChildren_ClosingOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareProjects_CheckedVouchers_CurrentAndChildren_ByCenter_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareProjects_CheckedVouchers_CurrentAndChildren_ClosingOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareProjects_CheckedVouchers_CurrentAndChildren_ByProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareProjects_CheckedVouchers_CurrentAndChildren_ClosingOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareProjects_CheckedVouchers_CurrentAndChildren_ByCenterProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareProjects_CheckedVouchers_CurrentAndChildren_ClosingOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region FinalizedVouchers_CurrentAndChildren_ClosingOption

        [Test]
        public async Task ProfitLoss_Periodic_CompareProjects_FinalizedVouchers_CurrentAndChildren_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareProjects_FinalizedVouchers_CurrentAndChildren_ClosingOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareProjects_FinalizedVouchers_CurrentAndChildren_ByCenter_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareProjects_FinalizedVouchers_CurrentAndChildren_ClosingOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareProjects_FinalizedVouchers_CurrentAndChildren_ByProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareProjects_FinalizedVouchers_CurrentAndChildren_ClosingOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareProjects_FinalizedVouchers_CurrentAndChildren_ByCenterProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareProjects_FinalizedVouchers_CurrentAndChildren_ClosingOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region ConfirmedVouchers_CurrentAndChildren_ClosingOption

        [Test]
        public async Task ProfitLoss_Periodic_CompareProjects_ConfirmedVouchers_CurrentAndChildren_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareProjects_ConfirmedVouchers_CurrentAndChildren_ClosingOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareProjects_ConfirmedVouchers_CurrentAndChildren_ByCenter_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareProjects_ConfirmedVouchers_CurrentAndChildren_ClosingOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareProjects_ConfirmedVouchers_CurrentAndChildren_ByProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareProjects_ConfirmedVouchers_CurrentAndChildren_ClosingOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareProjects_ConfirmedVouchers_CurrentAndChildren_ByCenterProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareProjects_ConfirmedVouchers_CurrentAndChildren_ClosingOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region ApprovedVouchers_CurrentAndChildren_ClosingOption

        [Test]
        public async Task ProfitLoss_Periodic_CompareProjects_ApprovedVouchers_CurrentAndChildren_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareProjects_ApprovedVouchers_CurrentAndChildren_ClosingOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareProjects_ApprovedVouchers_CurrentAndChildren_ByCenter_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareProjects_ApprovedVouchers_CurrentAndChildren_ClosingOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareProjects_ApprovedVouchers_CurrentAndChildren_ByProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareProjects_ApprovedVouchers_CurrentAndChildren_ClosingOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareProjects_ApprovedVouchers_CurrentAndChildren_ByCenterProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareProjects_ApprovedVouchers_CurrentAndChildren_ClosingOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region AllVouchers_CurrentAndChildren_ClosingOption

        [Test]
        public async Task ProfitLoss_Periodic_CompareProjects_AllVouchers_CurrentAndChildren_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareProjects_AllVouchers_CurrentAndChildren_ClosingOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareProjects_AllVouchers_CurrentAndChildren_ByCenter_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareProjects_AllVouchers_CurrentAndChildren_ClosingOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareProjects_AllVouchers_CurrentAndChildren_ByProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareProjects_AllVouchers_CurrentAndChildren_ClosingOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareProjects_AllVouchers_CurrentAndChildren_ByCenterProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareProjects_AllVouchers_CurrentAndChildren_ClosingOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region CheckedVouchers_CurrentBranch_TurnoverOption

        [Test]
        public async Task ProfitLoss_Periodic_CompareProjects_CheckedVouchers_CurrentBranch_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareProjects_CheckedVouchers_CurrentBranch_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareProjects_CheckedVouchers_CurrentBranch_ByCenter_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareProjects_CheckedVouchers_CurrentBranch_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareProjects_CheckedVouchers_CurrentBranch_ByProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareProjects_CheckedVouchers_CurrentBranch_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareProjects_CheckedVouchers_CurrentBranch_ByCenterProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareProjects_CheckedVouchers_CurrentBranch_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region FinalizedVouchers_CurrentBranch_TurnoverOption

        [Test]
        public async Task ProfitLoss_Periodic_CompareProjects_FinalizedVouchers_CurrentBranch_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareProjects_FinalizedVouchers_CurrentBranch_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareProjects_FinalizedVouchers_CurrentBranch_ByCenter_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareProjects_FinalizedVouchers_CurrentBranch_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareProjects_FinalizedVouchers_CurrentBranch_ByProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareProjects_FinalizedVouchers_CurrentBranch_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareProjects_FinalizedVouchers_CurrentBranch_ByCenterProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareProjects_FinalizedVouchers_CurrentBranch_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region ConfirmedVouchers_CurrentBranch_TurnoverOption

        [Test]
        public async Task ProfitLoss_Periodic_CompareProjects_ConfirmedVouchers_CurrentBranch_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareProjects_ConfirmedVouchers_CurrentBranch_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareProjects_ConfirmedVouchers_CurrentBranch_ByCenter_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareProjects_ConfirmedVouchers_CurrentBranch_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareProjects_ConfirmedVouchers_CurrentBranch_ByProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareProjects_ConfirmedVouchers_CurrentBranch_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareProjects_ConfirmedVouchers_CurrentBranch_ByCenterProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareProjects_ConfirmedVouchers_CurrentBranch_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region ApprovedVouchers_CurrentBranch_TurnoverOption

        [Test]
        public async Task ProfitLoss_Periodic_CompareProjects_ApprovedVouchers_CurrentBranch_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareProjects_ApprovedVouchers_CurrentBranch_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareProjects_ApprovedVouchers_CurrentBranch_ByCenter_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareProjects_ApprovedVouchers_CurrentBranch_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareProjects_ApprovedVouchers_CurrentBranch_ByProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareProjects_ApprovedVouchers_CurrentBranch_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareProjects_ApprovedVouchers_CurrentBranch_ByCenterProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareProjects_ApprovedVouchers_CurrentBranch_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region AllVouchers_CurrentBranch_TurnoverOption

        [Test]
        public async Task ProfitLoss_Periodic_CompareProjects_AllVouchers_CurrentBranch_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareProjects_AllVouchers_CurrentBranch_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareProjects_AllVouchers_CurrentBranch_ByCenter_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareProjects_AllVouchers_CurrentBranch_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareProjects_AllVouchers_CurrentBranch_ByProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareProjects_AllVouchers_CurrentBranch_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareProjects_AllVouchers_CurrentBranch_ByCenterProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareProjects_AllVouchers_CurrentBranch_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region CheckedVouchers_CurrentAndChildren_TurnoverOption

        [Test]
        public async Task ProfitLoss_Periodic_CompareProjects_CheckedVouchers_CurrentAndChildren_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareProjects_CheckedVouchers_CurrentAndChildren_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareProjects_CheckedVouchers_CurrentAndChildren_ByCenter_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareProjects_CheckedVouchers_CurrentAndChildren_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareProjects_CheckedVouchers_CurrentAndChildren_ByProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareProjects_CheckedVouchers_CurrentAndChildren_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareProjects_CheckedVouchers_CurrentAndChildren_ByCenterProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareProjects_CheckedVouchers_CurrentAndChildren_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region FinalizedVouchers_CurrentAndChildren_TurnoverOption

        [Test]
        public async Task ProfitLoss_Periodic_CompareProjects_FinalizedVouchers_CurrentAndChildren_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareProjects_FinalizedVouchers_CurrentAndChildren_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareProjects_FinalizedVouchers_CurrentAndChildren_ByCenter_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareProjects_FinalizedVouchers_CurrentAndChildren_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareProjects_FinalizedVouchers_CurrentAndChildren_ByProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareProjects_FinalizedVouchers_CurrentAndChildren_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareProjects_FinalizedVouchers_CurrentAndChildren_ByCenterProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareProjects_FinalizedVouchers_CurrentAndChildren_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region ConfirmedVouchers_CurrentAndChildren_TurnoverOption

        [Test]
        public async Task ProfitLoss_Periodic_CompareProjects_ConfirmedVouchers_CurrentAndChildren_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareProjects_ConfirmedVouchers_CurrentAndChildren_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareProjects_ConfirmedVouchers_CurrentAndChildren_ByCenter_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareProjects_ConfirmedVouchers_CurrentAndChildren_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareProjects_ConfirmedVouchers_CurrentAndChildren_ByProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareProjects_ConfirmedVouchers_CurrentAndChildren_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareProjects_ConfirmedVouchers_CurrentAndChildren_ByCenterProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareProjects_ConfirmedVouchers_CurrentAndChildren_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region ApprovedVouchers_CurrentAndChildren_TurnoverOption

        [Test]
        public async Task ProfitLoss_Periodic_CompareProjects_ApprovedVouchers_CurrentAndChildren_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareProjects_ApprovedVouchers_CurrentAndChildren_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareProjects_ApprovedVouchers_CurrentAndChildren_ByCenter_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareProjects_ApprovedVouchers_CurrentAndChildren_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareProjects_ApprovedVouchers_CurrentAndChildren_ByProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareProjects_ApprovedVouchers_CurrentAndChildren_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareProjects_ApprovedVouchers_CurrentAndChildren_ByCenterProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareProjects_ApprovedVouchers_CurrentAndChildren_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region AllVouchers_CurrentAndChildren_TurnoverOption

        [Test]
        public async Task ProfitLoss_Periodic_CompareProjects_AllVouchers_CurrentAndChildren_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareProjects_AllVouchers_CurrentAndChildren_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareProjects_AllVouchers_CurrentAndChildren_ByCenter_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareProjects_AllVouchers_CurrentAndChildren_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareProjects_AllVouchers_CurrentAndChildren_ByProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareProjects_AllVouchers_CurrentAndChildren_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareProjects_AllVouchers_CurrentAndChildren_ByCenterProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareProjects_AllVouchers_CurrentAndChildren_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region CheckedVouchers_CurrentBranch_ClosingOption_TurnoverOption

        [Test]
        public async Task ProfitLoss_Periodic_CompareProjects_CheckedVouchers_CurrentBranch_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareProjects_CheckedVouchers_CurrentBranch_ClosingOption_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareProjects_CheckedVouchers_CurrentBranch_ByCenter_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareProjects_CheckedVouchers_CurrentBranch_ClosingOption_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareProjects_CheckedVouchers_CurrentBranch_ByProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareProjects_CheckedVouchers_CurrentBranch_ClosingOption_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareProjects_CheckedVouchers_CurrentBranch_ByCenterProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareProjects_CheckedVouchers_CurrentBranch_ClosingOption_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region FinalizedVouchers_CurrentBranch_ClosingOption_TurnoverOption

        [Test]
        public async Task ProfitLoss_Periodic_CompareProjects_FinalizedVouchers_CurrentBranch_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareProjects_FinalizedVouchers_CurrentBranch_ClosingOption_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareProjects_FinalizedVouchers_CurrentBranch_ByCenter_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareProjects_FinalizedVouchers_CurrentBranch_ClosingOption_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareProjects_FinalizedVouchers_CurrentBranch_ByProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareProjects_FinalizedVouchers_CurrentBranch_ClosingOption_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareProjects_FinalizedVouchers_CurrentBranch_ByCenterProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareProjects_FinalizedVouchers_CurrentBranch_ClosingOption_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region ConfirmedVouchers_CurrentBranch_ClosingOption_TurnoverOption

        [Test]
        public async Task ProfitLoss_Periodic_CompareProjects_ConfirmedVouchers_CurrentBranch_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareProjects_ConfirmedVouchers_CurrentBranch_ClosingOption_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareProjects_ConfirmedVouchers_CurrentBranch_ByCenter_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareProjects_ConfirmedVouchers_CurrentBranch_ClosingOption_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareProjects_ConfirmedVouchers_CurrentBranch_ByProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareProjects_ConfirmedVouchers_CurrentBranch_ClosingOption_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareProjects_ConfirmedVouchers_CurrentBranch_ByCenterProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareProjects_ConfirmedVouchers_CurrentBranch_ClosingOption_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region ApprovedVouchers_CurrentBranch_ClosingOption_TurnoverOption

        [Test]
        public async Task ProfitLoss_Periodic_CompareProjects_ApprovedVouchers_CurrentBranch_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareProjects_ApprovedVouchers_CurrentBranch_ClosingOption_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareProjects_ApprovedVouchers_CurrentBranch_ByCenter_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareProjects_ApprovedVouchers_CurrentBranch_ClosingOption_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareProjects_ApprovedVouchers_CurrentBranch_ByProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareProjects_ApprovedVouchers_CurrentBranch_ClosingOption_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareProjects_ApprovedVouchers_CurrentBranch_ByCenterProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareProjects_ApprovedVouchers_CurrentBranch_ClosingOption_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region AllVouchers_CurrentBranch_ClosingOption_TurnoverOption

        [Test]
        public async Task ProfitLoss_Periodic_CompareProjects_AllVouchers_CurrentBranch_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareProjects_AllVouchers_CurrentBranch_ClosingOption_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareProjects_AllVouchers_CurrentBranch_ByCenter_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareProjects_AllVouchers_CurrentBranch_ClosingOption_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareProjects_AllVouchers_CurrentBranch_ByProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareProjects_AllVouchers_CurrentBranch_ClosingOption_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareProjects_AllVouchers_CurrentBranch_ByCenterProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareProjects_AllVouchers_CurrentBranch_ClosingOption_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region CheckedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption

        [Test]
        public async Task ProfitLoss_Periodic_CompareProjects_CheckedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareProjects_CheckedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareProjects_CheckedVouchers_CurrentAndChildren_ByCenter_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareProjects_CheckedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareProjects_CheckedVouchers_CurrentAndChildren_ByProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareProjects_CheckedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareProjects_CheckedVouchers_CurrentAndChildren_ByCenterProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareProjects_CheckedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region FinalizedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption

        [Test]
        public async Task ProfitLoss_Periodic_CompareProjects_FinalizedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareProjects_FinalizedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareProjects_FinalizedVouchers_CurrentAndChildren_ByCenter_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareProjects_FinalizedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareProjects_FinalizedVouchers_CurrentAndChildren_ByProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareProjects_FinalizedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareProjects_FinalizedVouchers_CurrentAndChildren_ByCenterProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareProjects_FinalizedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region ConfirmedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption

        [Test]
        public async Task ProfitLoss_Periodic_CompareProjects_ConfirmedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareProjects_ConfirmedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareProjects_ConfirmedVouchers_CurrentAndChildren_ByCenter_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareProjects_ConfirmedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareProjects_ConfirmedVouchers_CurrentAndChildren_ByProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareProjects_ConfirmedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareProjects_ConfirmedVouchers_CurrentAndChildren_ByCenterProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareProjects_ConfirmedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region ApprovedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption

        [Test]
        public async Task ProfitLoss_Periodic_CompareProjects_ApprovedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareProjects_ApprovedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareProjects_ApprovedVouchers_CurrentAndChildren_ByCenter_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareProjects_ApprovedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareProjects_ApprovedVouchers_CurrentAndChildren_ByProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareProjects_ApprovedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareProjects_ApprovedVouchers_CurrentAndChildren_ByCenterProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareProjects_ApprovedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region AllVouchers_CurrentAndChildren_ClosingOption_TurnoverOption

        [Test]
        public async Task ProfitLoss_Periodic_CompareProjects_AllVouchers_CurrentAndChildren_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareProjects_AllVouchers_CurrentAndChildren_ClosingOption_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareProjects_AllVouchers_CurrentAndChildren_ByCenter_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareProjects_AllVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareProjects_AllVouchers_CurrentAndChildren_ByProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareProjects_AllVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareProjects_AllVouchers_CurrentAndChildren_ByCenterProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareProjects_AllVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #endregion

        #region Profit-Loss By Cost Centers Tests

        #region CheckedVouchers_CurrentBranch_NoOptions

        [Test]
        public async Task ProfitLoss_Periodic_CompareCostCenters_CheckedVouchers_CurrentBranch_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareCostCenters_CheckedVouchers_CurrentBranch_NoOptions();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareCostCenters_CheckedVouchers_CurrentBranch_ByCenter_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareCostCenters_CheckedVouchers_CurrentBranch_NoOptions(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareCostCenters_CheckedVouchers_CurrentBranch_ByProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareCostCenters_CheckedVouchers_CurrentBranch_NoOptions(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareCostCenters_CheckedVouchers_CurrentBranch_ByCenterProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareCostCenters_CheckedVouchers_CurrentBranch_NoOptions(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region FinalizedVouchers_CurrentBranch_NoOptions

        [Test]
        public async Task ProfitLoss_Periodic_CompareCostCenters_FinalizedVouchers_CurrentBranch_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareCostCenters_FinalizedVouchers_CurrentBranch_NoOptions();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareCostCenters_FinalizedVouchers_CurrentBranch_ByCenter_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareCostCenters_FinalizedVouchers_CurrentBranch_NoOptions(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareCostCenters_FinalizedVouchers_CurrentBranch_ByProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareCostCenters_FinalizedVouchers_CurrentBranch_NoOptions(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareCostCenters_FinalizedVouchers_CurrentBranch_ByCenterProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareCostCenters_FinalizedVouchers_CurrentBranch_NoOptions(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region ConfirmedVouchers_CurrentBranch_NoOptions

        [Test]
        public async Task ProfitLoss_Periodic_CompareCostCenters_ConfirmedVouchers_CurrentBranch_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareCostCenters_ConfirmedVouchers_CurrentBranch_NoOptions();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareCostCenters_ConfirmedVouchers_CurrentBranch_ByCenter_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareCostCenters_ConfirmedVouchers_CurrentBranch_NoOptions(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareCostCenters_ConfirmedVouchers_CurrentBranch_ByProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareCostCenters_ConfirmedVouchers_CurrentBranch_NoOptions(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareCostCenters_ConfirmedVouchers_CurrentBranch_ByCenterProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareCostCenters_ConfirmedVouchers_CurrentBranch_NoOptions(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region ApprovedVouchers_CurrentBranch_NoOptions

        [Test]
        public async Task ProfitLoss_Periodic_CompareCostCenters_ApprovedVouchers_CurrentBranch_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareCostCenters_ApprovedVouchers_CurrentBranch_NoOptions();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareCostCenters_ApprovedVouchers_CurrentBranch_ByCenter_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareCostCenters_ApprovedVouchers_CurrentBranch_NoOptions(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareCostCenters_ApprovedVouchers_CurrentBranch_ByProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareCostCenters_ApprovedVouchers_CurrentBranch_NoOptions(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareCostCenters_ApprovedVouchers_CurrentBranch_ByCenterProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareCostCenters_ApprovedVouchers_CurrentBranch_NoOptions(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region AllVouchers_CurrentBranch_NoOptions

        [Test]
        public async Task ProfitLoss_Periodic_CompareCostCenters_AllVouchers_CurrentBranch_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareCostCenters_AllVouchers_CurrentBranch_NoOptions();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareCostCenters_AllVouchers_CurrentBranch_ByCenter_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareCostCenters_AllVouchers_CurrentBranch_NoOptions(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareCostCenters_AllVouchers_CurrentBranch_ByProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareCostCenters_AllVouchers_CurrentBranch_NoOptions(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareCostCenters_AllVouchers_CurrentBranch_ByCenterProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareCostCenters_AllVouchers_CurrentBranch_NoOptions(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region CheckedVouchers_CurrentAndChildren_NoOptions

        [Test]
        public async Task ProfitLoss_Periodic_CompareCostCenters_CheckedVouchers_CurrentAndChildren_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareCostCenters_CheckedVouchers_CurrentAndChildren_NoOptions();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareCostCenters_CheckedVouchers_CurrentAndChildren_ByCenter_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareCostCenters_CheckedVouchers_CurrentAndChildren_NoOptions(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareCostCenters_CheckedVouchers_CurrentAndChildren_ByProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareCostCenters_CheckedVouchers_CurrentAndChildren_NoOptions(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareCostCenters_CheckedVouchers_CurrentAndChildren_ByCenterProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareCostCenters_CheckedVouchers_CurrentAndChildren_NoOptions(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region FinalizedVouchers_CurrentAndChildren_NoOptions

        [Test]
        public async Task ProfitLoss_Periodic_CompareCostCenters_FinalizedVouchers_CurrentAndChildren_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareCostCenters_FinalizedVouchers_CurrentAndChildren_NoOptions();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareCostCenters_FinalizedVouchers_CurrentAndChildren_ByCenter_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareCostCenters_FinalizedVouchers_CurrentAndChildren_NoOptions(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareCostCenters_FinalizedVouchers_CurrentAndChildren_ByProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareCostCenters_FinalizedVouchers_CurrentAndChildren_NoOptions(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareCostCenters_FinalizedVouchers_CurrentAndChildren_ByCenterProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareCostCenters_FinalizedVouchers_CurrentAndChildren_NoOptions(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region ConfirmedVouchers_CurrentAndChildren_NoOptions

        [Test]
        public async Task ProfitLoss_Periodic_CompareCostCenters_ConfirmedVouchers_CurrentAndChildren_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareCostCenters_ConfirmedVouchers_CurrentAndChildren_NoOptions();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareCostCenters_ConfirmedVouchers_CurrentAndChildren_ByCenter_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareCostCenters_ConfirmedVouchers_CurrentAndChildren_NoOptions(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareCostCenters_ConfirmedVouchers_CurrentAndChildren_ByProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareCostCenters_ConfirmedVouchers_CurrentAndChildren_NoOptions(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareCostCenters_ConfirmedVouchers_CurrentAndChildren_ByCenterProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareCostCenters_ConfirmedVouchers_CurrentAndChildren_NoOptions(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region ApprovedVouchers_CurrentAndChildren_NoOptions

        [Test]
        public async Task ProfitLoss_Periodic_CompareCostCenters_ApprovedVouchers_CurrentAndChildren_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareCostCenters_ApprovedVouchers_CurrentAndChildren_NoOptions();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareCostCenters_ApprovedVouchers_CurrentAndChildren_ByCenter_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareCostCenters_ApprovedVouchers_CurrentAndChildren_NoOptions(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareCostCenters_ApprovedVouchers_CurrentAndChildren_ByProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareCostCenters_ApprovedVouchers_CurrentAndChildren_NoOptions(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareCostCenters_ApprovedVouchers_CurrentAndChildren_ByCenterProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareCostCenters_ApprovedVouchers_CurrentAndChildren_NoOptions(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region AllVouchers_CurrentAndChildren_NoOptions

        [Test]
        public async Task ProfitLoss_Periodic_CompareCostCenters_AllVouchers_CurrentAndChildren_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareCostCenters_AllVouchers_CurrentAndChildren_NoOptions();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareCostCenters_AllVouchers_CurrentAndChildren_ByCenter_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareCostCenters_AllVouchers_CurrentAndChildren_NoOptions(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareCostCenters_AllVouchers_CurrentAndChildren_ByProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareCostCenters_AllVouchers_CurrentAndChildren_NoOptions(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareCostCenters_AllVouchers_CurrentAndChildren_ByCenterProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareCostCenters_AllVouchers_CurrentAndChildren_NoOptions(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region CheckedVouchers_CurrentBranch_ClosingOption

        [Test]
        public async Task ProfitLoss_Periodic_CompareCostCenters_CheckedVouchers_CurrentBranch_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareCostCenters_CheckedVouchers_CurrentBranch_ClosingOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareCostCenters_CheckedVouchers_CurrentBranch_ByCenter_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareCostCenters_CheckedVouchers_CurrentBranch_ClosingOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareCostCenters_CheckedVouchers_CurrentBranch_ByProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareCostCenters_CheckedVouchers_CurrentBranch_ClosingOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareCostCenters_CheckedVouchers_CurrentBranch_ByCenterProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareCostCenters_CheckedVouchers_CurrentBranch_ClosingOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region FinalizedVouchers_CurrentBranch_ClosingOption

        [Test]
        public async Task ProfitLoss_Periodic_CompareCostCenters_FinalizedVouchers_CurrentBranch_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareCostCenters_FinalizedVouchers_CurrentBranch_ClosingOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareCostCenters_FinalizedVouchers_CurrentBranch_ByCenter_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareCostCenters_FinalizedVouchers_CurrentBranch_ClosingOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareCostCenters_FinalizedVouchers_CurrentBranch_ByProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareCostCenters_FinalizedVouchers_CurrentBranch_ClosingOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareCostCenters_FinalizedVouchers_CurrentBranch_ByCenterProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareCostCenters_FinalizedVouchers_CurrentBranch_ClosingOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region ConfirmedVouchers_CurrentBranch_ClosingOption

        [Test]
        public async Task ProfitLoss_Periodic_CompareCostCenters_ConfirmedVouchers_CurrentBranch_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareCostCenters_ConfirmedVouchers_CurrentBranch_ClosingOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareCostCenters_ConfirmedVouchers_CurrentBranch_ByCenter_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareCostCenters_ConfirmedVouchers_CurrentBranch_ClosingOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareCostCenters_ConfirmedVouchers_CurrentBranch_ByProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareCostCenters_ConfirmedVouchers_CurrentBranch_ClosingOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareCostCenters_ConfirmedVouchers_CurrentBranch_ByCenterProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareCostCenters_ConfirmedVouchers_CurrentBranch_ClosingOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region ApprovedVouchers_CurrentBranch_ClosingOption

        [Test]
        public async Task ProfitLoss_Periodic_CompareCostCenters_ApprovedVouchers_CurrentBranch_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareCostCenters_ApprovedVouchers_CurrentBranch_ClosingOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareCostCenters_ApprovedVouchers_CurrentBranch_ByCenter_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareCostCenters_ApprovedVouchers_CurrentBranch_ClosingOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareCostCenters_ApprovedVouchers_CurrentBranch_ByProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareCostCenters_ApprovedVouchers_CurrentBranch_ClosingOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareCostCenters_ApprovedVouchers_CurrentBranch_ByCenterProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareCostCenters_ApprovedVouchers_CurrentBranch_ClosingOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region AllVouchers_CurrentBranch_ClosingOption

        [Test]
        public async Task ProfitLoss_Periodic_CompareCostCenters_AllVouchers_CurrentBranch_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareCostCenters_AllVouchers_CurrentBranch_ClosingOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareCostCenters_AllVouchers_CurrentBranch_ByCenter_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareCostCenters_AllVouchers_CurrentBranch_ClosingOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareCostCenters_AllVouchers_CurrentBranch_ByProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareCostCenters_AllVouchers_CurrentBranch_ClosingOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareCostCenters_AllVouchers_CurrentBranch_ByCenterProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareCostCenters_AllVouchers_CurrentBranch_ClosingOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region CheckedVouchers_CurrentAndChildren_ClosingOption

        [Test]
        public async Task ProfitLoss_Periodic_CompareCostCenters_CheckedVouchers_CurrentAndChildren_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareCostCenters_CheckedVouchers_CurrentAndChildren_ClosingOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareCostCenters_CheckedVouchers_CurrentAndChildren_ByCenter_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareCostCenters_CheckedVouchers_CurrentAndChildren_ClosingOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareCostCenters_CheckedVouchers_CurrentAndChildren_ByProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareCostCenters_CheckedVouchers_CurrentAndChildren_ClosingOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareCostCenters_CheckedVouchers_CurrentAndChildren_ByCenterProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareCostCenters_CheckedVouchers_CurrentAndChildren_ClosingOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region FinalizedVouchers_CurrentAndChildren_ClosingOption

        [Test]
        public async Task ProfitLoss_Periodic_CompareCostCenters_FinalizedVouchers_CurrentAndChildren_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareCostCenters_FinalizedVouchers_CurrentAndChildren_ClosingOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareCostCenters_FinalizedVouchers_CurrentAndChildren_ByCenter_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareCostCenters_FinalizedVouchers_CurrentAndChildren_ClosingOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareCostCenters_FinalizedVouchers_CurrentAndChildren_ByProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareCostCenters_FinalizedVouchers_CurrentAndChildren_ClosingOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareCostCenters_FinalizedVouchers_CurrentAndChildren_ByCenterProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareCostCenters_FinalizedVouchers_CurrentAndChildren_ClosingOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region ConfirmedVouchers_CurrentAndChildren_ClosingOption

        [Test]
        public async Task ProfitLoss_Periodic_CompareCostCenters_ConfirmedVouchers_CurrentAndChildren_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareCostCenters_ConfirmedVouchers_CurrentAndChildren_ClosingOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareCostCenters_ConfirmedVouchers_CurrentAndChildren_ByCenter_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareCostCenters_ConfirmedVouchers_CurrentAndChildren_ClosingOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareCostCenters_ConfirmedVouchers_CurrentAndChildren_ByProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareCostCenters_ConfirmedVouchers_CurrentAndChildren_ClosingOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareCostCenters_ConfirmedVouchers_CurrentAndChildren_ByCenterProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareCostCenters_ConfirmedVouchers_CurrentAndChildren_ClosingOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region ApprovedVouchers_CurrentAndChildren_ClosingOption

        [Test]
        public async Task ProfitLoss_Periodic_CompareCostCenters_ApprovedVouchers_CurrentAndChildren_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareCostCenters_ApprovedVouchers_CurrentAndChildren_ClosingOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareCostCenters_ApprovedVouchers_CurrentAndChildren_ByCenter_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareCostCenters_ApprovedVouchers_CurrentAndChildren_ClosingOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareCostCenters_ApprovedVouchers_CurrentAndChildren_ByProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareCostCenters_ApprovedVouchers_CurrentAndChildren_ClosingOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareCostCenters_ApprovedVouchers_CurrentAndChildren_ByCenterProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareCostCenters_ApprovedVouchers_CurrentAndChildren_ClosingOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region AllVouchers_CurrentAndChildren_ClosingOption

        [Test]
        public async Task ProfitLoss_Periodic_CompareCostCenters_AllVouchers_CurrentAndChildren_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareCostCenters_AllVouchers_CurrentAndChildren_ClosingOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareCostCenters_AllVouchers_CurrentAndChildren_ByCenter_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareCostCenters_AllVouchers_CurrentAndChildren_ClosingOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareCostCenters_AllVouchers_CurrentAndChildren_ByProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareCostCenters_AllVouchers_CurrentAndChildren_ClosingOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareCostCenters_AllVouchers_CurrentAndChildren_ByCenterProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareCostCenters_AllVouchers_CurrentAndChildren_ClosingOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region CheckedVouchers_CurrentBranch_TurnoverOption

        [Test]
        public async Task ProfitLoss_Periodic_CompareCostCenters_CheckedVouchers_CurrentBranch_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareCostCenters_CheckedVouchers_CurrentBranch_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareCostCenters_CheckedVouchers_CurrentBranch_ByCenter_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareCostCenters_CheckedVouchers_CurrentBranch_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareCostCenters_CheckedVouchers_CurrentBranch_ByProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareCostCenters_CheckedVouchers_CurrentBranch_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareCostCenters_CheckedVouchers_CurrentBranch_ByCenterProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareCostCenters_CheckedVouchers_CurrentBranch_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region FinalizedVouchers_CurrentBranch_TurnoverOption

        [Test]
        public async Task ProfitLoss_Periodic_CompareCostCenters_FinalizedVouchers_CurrentBranch_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareCostCenters_FinalizedVouchers_CurrentBranch_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareCostCenters_FinalizedVouchers_CurrentBranch_ByCenter_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareCostCenters_FinalizedVouchers_CurrentBranch_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareCostCenters_FinalizedVouchers_CurrentBranch_ByProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareCostCenters_FinalizedVouchers_CurrentBranch_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareCostCenters_FinalizedVouchers_CurrentBranch_ByCenterProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareCostCenters_FinalizedVouchers_CurrentBranch_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region ConfirmedVouchers_CurrentBranch_TurnoverOption

        [Test]
        public async Task ProfitLoss_Periodic_CompareCostCenters_ConfirmedVouchers_CurrentBranch_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareCostCenters_ConfirmedVouchers_CurrentBranch_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareCostCenters_ConfirmedVouchers_CurrentBranch_ByCenter_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareCostCenters_ConfirmedVouchers_CurrentBranch_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareCostCenters_ConfirmedVouchers_CurrentBranch_ByProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareCostCenters_ConfirmedVouchers_CurrentBranch_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareCostCenters_ConfirmedVouchers_CurrentBranch_ByCenterProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareCostCenters_ConfirmedVouchers_CurrentBranch_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region ApprovedVouchers_CurrentBranch_TurnoverOption

        [Test]
        public async Task ProfitLoss_Periodic_CompareCostCenters_ApprovedVouchers_CurrentBranch_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareCostCenters_ApprovedVouchers_CurrentBranch_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareCostCenters_ApprovedVouchers_CurrentBranch_ByCenter_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareCostCenters_ApprovedVouchers_CurrentBranch_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareCostCenters_ApprovedVouchers_CurrentBranch_ByProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareCostCenters_ApprovedVouchers_CurrentBranch_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareCostCenters_ApprovedVouchers_CurrentBranch_ByCenterProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareCostCenters_ApprovedVouchers_CurrentBranch_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region AllVouchers_CurrentBranch_TurnoverOption

        [Test]
        public async Task ProfitLoss_Periodic_CompareCostCenters_AllVouchers_CurrentBranch_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareCostCenters_AllVouchers_CurrentBranch_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareCostCenters_AllVouchers_CurrentBranch_ByCenter_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareCostCenters_AllVouchers_CurrentBranch_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareCostCenters_AllVouchers_CurrentBranch_ByProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareCostCenters_AllVouchers_CurrentBranch_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareCostCenters_AllVouchers_CurrentBranch_ByCenterProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareCostCenters_AllVouchers_CurrentBranch_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region CheckedVouchers_CurrentAndChildren_TurnoverOption

        [Test]
        public async Task ProfitLoss_Periodic_CompareCostCenters_CheckedVouchers_CurrentAndChildren_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareCostCenters_CheckedVouchers_CurrentAndChildren_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareCostCenters_CheckedVouchers_CurrentAndChildren_ByCenter_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareCostCenters_CheckedVouchers_CurrentAndChildren_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareCostCenters_CheckedVouchers_CurrentAndChildren_ByProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareCostCenters_CheckedVouchers_CurrentAndChildren_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareCostCenters_CheckedVouchers_CurrentAndChildren_ByCenterProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareCostCenters_CheckedVouchers_CurrentAndChildren_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region FinalizedVouchers_CurrentAndChildren_TurnoverOption

        [Test]
        public async Task ProfitLoss_Periodic_CompareCostCenters_FinalizedVouchers_CurrentAndChildren_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareCostCenters_FinalizedVouchers_CurrentAndChildren_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareCostCenters_FinalizedVouchers_CurrentAndChildren_ByCenter_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareCostCenters_FinalizedVouchers_CurrentAndChildren_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareCostCenters_FinalizedVouchers_CurrentAndChildren_ByProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareCostCenters_FinalizedVouchers_CurrentAndChildren_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareCostCenters_FinalizedVouchers_CurrentAndChildren_ByCenterProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareCostCenters_FinalizedVouchers_CurrentAndChildren_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region ConfirmedVouchers_CurrentAndChildren_TurnoverOption

        [Test]
        public async Task ProfitLoss_Periodic_CompareCostCenters_ConfirmedVouchers_CurrentAndChildren_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareCostCenters_ConfirmedVouchers_CurrentAndChildren_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareCostCenters_ConfirmedVouchers_CurrentAndChildren_ByCenter_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareCostCenters_ConfirmedVouchers_CurrentAndChildren_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareCostCenters_ConfirmedVouchers_CurrentAndChildren_ByProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareCostCenters_ConfirmedVouchers_CurrentAndChildren_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareCostCenters_ConfirmedVouchers_CurrentAndChildren_ByCenterProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareCostCenters_ConfirmedVouchers_CurrentAndChildren_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region ApprovedVouchers_CurrentAndChildren_TurnoverOption

        [Test]
        public async Task ProfitLoss_Periodic_CompareCostCenters_ApprovedVouchers_CurrentAndChildren_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareCostCenters_ApprovedVouchers_CurrentAndChildren_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareCostCenters_ApprovedVouchers_CurrentAndChildren_ByCenter_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareCostCenters_ApprovedVouchers_CurrentAndChildren_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareCostCenters_ApprovedVouchers_CurrentAndChildren_ByProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareCostCenters_ApprovedVouchers_CurrentAndChildren_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareCostCenters_ApprovedVouchers_CurrentAndChildren_ByCenterProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareCostCenters_ApprovedVouchers_CurrentAndChildren_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region AllVouchers_CurrentAndChildren_TurnoverOption

        [Test]
        public async Task ProfitLoss_Periodic_CompareCostCenters_AllVouchers_CurrentAndChildren_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareCostCenters_AllVouchers_CurrentAndChildren_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareCostCenters_AllVouchers_CurrentAndChildren_ByCenter_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareCostCenters_AllVouchers_CurrentAndChildren_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareCostCenters_AllVouchers_CurrentAndChildren_ByProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareCostCenters_AllVouchers_CurrentAndChildren_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareCostCenters_AllVouchers_CurrentAndChildren_ByCenterProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareCostCenters_AllVouchers_CurrentAndChildren_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region CheckedVouchers_CurrentBranch_ClosingOption_TurnoverOption

        [Test]
        public async Task ProfitLoss_Periodic_CompareCostCenters_CheckedVouchers_CurrentBranch_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareCostCenters_CheckedVouchers_CurrentBranch_ClosingOption_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareCostCenters_CheckedVouchers_CurrentBranch_ByCenter_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareCostCenters_CheckedVouchers_CurrentBranch_ClosingOption_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareCostCenters_CheckedVouchers_CurrentBranch_ByProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareCostCenters_CheckedVouchers_CurrentBranch_ClosingOption_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareCostCenters_CheckedVouchers_CurrentBranch_ByCenterProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareCostCenters_CheckedVouchers_CurrentBranch_ClosingOption_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region FinalizedVouchers_CurrentBranch_ClosingOption_TurnoverOption

        [Test]
        public async Task ProfitLoss_Periodic_CompareCostCenters_FinalizedVouchers_CurrentBranch_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareCostCenters_FinalizedVouchers_CurrentBranch_ClosingOption_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareCostCenters_FinalizedVouchers_CurrentBranch_ByCenter_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareCostCenters_FinalizedVouchers_CurrentBranch_ClosingOption_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareCostCenters_FinalizedVouchers_CurrentBranch_ByProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareCostCenters_FinalizedVouchers_CurrentBranch_ClosingOption_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareCostCenters_FinalizedVouchers_CurrentBranch_ByCenterProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareCostCenters_FinalizedVouchers_CurrentBranch_ClosingOption_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region ConfirmedVouchers_CurrentBranch_ClosingOption_TurnoverOption

        [Test]
        public async Task ProfitLoss_Periodic_CompareCostCenters_ConfirmedVouchers_CurrentBranch_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareCostCenters_ConfirmedVouchers_CurrentBranch_ClosingOption_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareCostCenters_ConfirmedVouchers_CurrentBranch_ByCenter_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareCostCenters_ConfirmedVouchers_CurrentBranch_ClosingOption_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareCostCenters_ConfirmedVouchers_CurrentBranch_ByProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareCostCenters_ConfirmedVouchers_CurrentBranch_ClosingOption_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareCostCenters_ConfirmedVouchers_CurrentBranch_ByCenterProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareCostCenters_ConfirmedVouchers_CurrentBranch_ClosingOption_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region ApprovedVouchers_CurrentBranch_ClosingOption_TurnoverOption

        [Test]
        public async Task ProfitLoss_Periodic_CompareCostCenters_ApprovedVouchers_CurrentBranch_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareCostCenters_ApprovedVouchers_CurrentBranch_ClosingOption_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareCostCenters_ApprovedVouchers_CurrentBranch_ByCenter_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareCostCenters_ApprovedVouchers_CurrentBranch_ClosingOption_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareCostCenters_ApprovedVouchers_CurrentBranch_ByProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareCostCenters_ApprovedVouchers_CurrentBranch_ClosingOption_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareCostCenters_ApprovedVouchers_CurrentBranch_ByCenterProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareCostCenters_ApprovedVouchers_CurrentBranch_ClosingOption_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region AllVouchers_CurrentBranch_ClosingOption_TurnoverOption

        [Test]
        public async Task ProfitLoss_Periodic_CompareCostCenters_AllVouchers_CurrentBranch_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareCostCenters_AllVouchers_CurrentBranch_ClosingOption_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareCostCenters_AllVouchers_CurrentBranch_ByCenter_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareCostCenters_AllVouchers_CurrentBranch_ClosingOption_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareCostCenters_AllVouchers_CurrentBranch_ByProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareCostCenters_AllVouchers_CurrentBranch_ClosingOption_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareCostCenters_AllVouchers_CurrentBranch_ByCenterProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareCostCenters_AllVouchers_CurrentBranch_ClosingOption_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region CheckedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption

        [Test]
        public async Task ProfitLoss_Periodic_CompareCostCenters_CheckedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareCostCenters_CheckedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareCostCenters_CheckedVouchers_CurrentAndChildren_ByCenter_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareCostCenters_CheckedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareCostCenters_CheckedVouchers_CurrentAndChildren_ByProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareCostCenters_CheckedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareCostCenters_CheckedVouchers_CurrentAndChildren_ByCenterProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareCostCenters_CheckedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region FinalizedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption

        [Test]
        public async Task ProfitLoss_Periodic_CompareCostCenters_FinalizedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareCostCenters_FinalizedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareCostCenters_FinalizedVouchers_CurrentAndChildren_ByCenter_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareCostCenters_FinalizedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareCostCenters_FinalizedVouchers_CurrentAndChildren_ByProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareCostCenters_FinalizedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareCostCenters_FinalizedVouchers_CurrentAndChildren_ByCenterProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareCostCenters_FinalizedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region ConfirmedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption

        [Test]
        public async Task ProfitLoss_Periodic_CompareCostCenters_ConfirmedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareCostCenters_ConfirmedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareCostCenters_ConfirmedVouchers_CurrentAndChildren_ByCenter_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareCostCenters_ConfirmedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareCostCenters_ConfirmedVouchers_CurrentAndChildren_ByProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareCostCenters_ConfirmedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareCostCenters_ConfirmedVouchers_CurrentAndChildren_ByCenterProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareCostCenters_ConfirmedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region ApprovedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption

        [Test]
        public async Task ProfitLoss_Periodic_CompareCostCenters_ApprovedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareCostCenters_ApprovedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareCostCenters_ApprovedVouchers_CurrentAndChildren_ByCenter_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareCostCenters_ApprovedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareCostCenters_ApprovedVouchers_CurrentAndChildren_ByProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareCostCenters_ApprovedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareCostCenters_ApprovedVouchers_CurrentAndChildren_ByCenterProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareCostCenters_ApprovedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region AllVouchers_CurrentAndChildren_ClosingOption_TurnoverOption

        [Test]
        public async Task ProfitLoss_Periodic_CompareCostCenters_AllVouchers_CurrentAndChildren_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareCostCenters_AllVouchers_CurrentAndChildren_ClosingOption_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareCostCenters_AllVouchers_CurrentAndChildren_ByCenter_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareCostCenters_AllVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareCostCenters_AllVouchers_CurrentAndChildren_ByProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareCostCenters_AllVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareCostCenters_AllVouchers_CurrentAndChildren_ByCenterProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareCostCenters_AllVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #endregion

        #region Profit-Loss By Fiscal Periods Tests

        #region CheckedVouchers_CurrentBranch_NoOptions

        [Test]
        public async Task ProfitLoss_Periodic_CompareFiscalPeriods_CheckedVouchers_CurrentBranch_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareFiscalPeriods_CheckedVouchers_CurrentBranch_NoOptions();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareFiscalPeriods_CheckedVouchers_CurrentBranch_ByCenter_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareFiscalPeriods_CheckedVouchers_CurrentBranch_NoOptions(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareFiscalPeriods_CheckedVouchers_CurrentBranch_ByProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareFiscalPeriods_CheckedVouchers_CurrentBranch_NoOptions(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareFiscalPeriods_CheckedVouchers_CurrentBranch_ByCenterProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareFiscalPeriods_CheckedVouchers_CurrentBranch_NoOptions(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region FinalizedVouchers_CurrentBranch_NoOptions

        [Test]
        public async Task ProfitLoss_Periodic_CompareFiscalPeriods_FinalizedVouchers_CurrentBranch_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareFiscalPeriods_FinalizedVouchers_CurrentBranch_NoOptions();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareFiscalPeriods_FinalizedVouchers_CurrentBranch_ByCenter_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareFiscalPeriods_FinalizedVouchers_CurrentBranch_NoOptions(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareFiscalPeriods_FinalizedVouchers_CurrentBranch_ByProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareFiscalPeriods_FinalizedVouchers_CurrentBranch_NoOptions(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareFiscalPeriods_FinalizedVouchers_CurrentBranch_ByCenterProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareFiscalPeriods_FinalizedVouchers_CurrentBranch_NoOptions(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region ConfirmedVouchers_CurrentBranch_NoOptions

        [Test]
        public async Task ProfitLoss_Periodic_CompareFiscalPeriods_ConfirmedVouchers_CurrentBranch_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareFiscalPeriods_ConfirmedVouchers_CurrentBranch_NoOptions();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareFiscalPeriods_ConfirmedVouchers_CurrentBranch_ByCenter_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareFiscalPeriods_ConfirmedVouchers_CurrentBranch_NoOptions(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareFiscalPeriods_ConfirmedVouchers_CurrentBranch_ByProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareFiscalPeriods_ConfirmedVouchers_CurrentBranch_NoOptions(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareFiscalPeriods_ConfirmedVouchers_CurrentBranch_ByCenterProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareFiscalPeriods_ConfirmedVouchers_CurrentBranch_NoOptions(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region ApprovedVouchers_CurrentBranch_NoOptions

        [Test]
        public async Task ProfitLoss_Periodic_CompareFiscalPeriods_ApprovedVouchers_CurrentBranch_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareFiscalPeriods_ApprovedVouchers_CurrentBranch_NoOptions();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareFiscalPeriods_ApprovedVouchers_CurrentBranch_ByCenter_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareFiscalPeriods_ApprovedVouchers_CurrentBranch_NoOptions(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareFiscalPeriods_ApprovedVouchers_CurrentBranch_ByProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareFiscalPeriods_ApprovedVouchers_CurrentBranch_NoOptions(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareFiscalPeriods_ApprovedVouchers_CurrentBranch_ByCenterProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareFiscalPeriods_ApprovedVouchers_CurrentBranch_NoOptions(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region AllVouchers_CurrentBranch_NoOptions

        [Test]
        public async Task ProfitLoss_Periodic_CompareFiscalPeriods_AllVouchers_CurrentBranch_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareFiscalPeriods_AllVouchers_CurrentBranch_NoOptions();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareFiscalPeriods_AllVouchers_CurrentBranch_ByCenter_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareFiscalPeriods_AllVouchers_CurrentBranch_NoOptions(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareFiscalPeriods_AllVouchers_CurrentBranch_ByProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareFiscalPeriods_AllVouchers_CurrentBranch_NoOptions(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareFiscalPeriods_AllVouchers_CurrentBranch_ByCenterProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareFiscalPeriods_AllVouchers_CurrentBranch_NoOptions(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region CheckedVouchers_CurrentAndChildren_NoOptions

        [Test]
        public async Task ProfitLoss_Periodic_CompareFiscalPeriods_CheckedVouchers_CurrentAndChildren_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareFiscalPeriods_CheckedVouchers_CurrentAndChildren_NoOptions();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareFiscalPeriods_CheckedVouchers_CurrentAndChildren_ByCenter_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareFiscalPeriods_CheckedVouchers_CurrentAndChildren_NoOptions(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareFiscalPeriods_CheckedVouchers_CurrentAndChildren_ByProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareFiscalPeriods_CheckedVouchers_CurrentAndChildren_NoOptions(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareFiscalPeriods_CheckedVouchers_CurrentAndChildren_ByCenterProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareFiscalPeriods_CheckedVouchers_CurrentAndChildren_NoOptions(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region FinalizedVouchers_CurrentAndChildren_NoOptions

        [Test]
        public async Task ProfitLoss_Periodic_CompareFiscalPeriods_FinalizedVouchers_CurrentAndChildren_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareFiscalPeriods_FinalizedVouchers_CurrentAndChildren_NoOptions();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareFiscalPeriods_FinalizedVouchers_CurrentAndChildren_ByCenter_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareFiscalPeriods_FinalizedVouchers_CurrentAndChildren_NoOptions(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareFiscalPeriods_FinalizedVouchers_CurrentAndChildren_ByProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareFiscalPeriods_FinalizedVouchers_CurrentAndChildren_NoOptions(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareFiscalPeriods_FinalizedVouchers_CurrentAndChildren_ByCenterProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareFiscalPeriods_FinalizedVouchers_CurrentAndChildren_NoOptions(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region ConfirmedVouchers_CurrentAndChildren_NoOptions

        [Test]
        public async Task ProfitLoss_Periodic_CompareFiscalPeriods_ConfirmedVouchers_CurrentAndChildren_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareFiscalPeriods_ConfirmedVouchers_CurrentAndChildren_NoOptions();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareFiscalPeriods_ConfirmedVouchers_CurrentAndChildren_ByCenter_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareFiscalPeriods_ConfirmedVouchers_CurrentAndChildren_NoOptions(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareFiscalPeriods_ConfirmedVouchers_CurrentAndChildren_ByProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareFiscalPeriods_ConfirmedVouchers_CurrentAndChildren_NoOptions(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareFiscalPeriods_ConfirmedVouchers_CurrentAndChildren_ByCenterProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareFiscalPeriods_ConfirmedVouchers_CurrentAndChildren_NoOptions(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region ApprovedVouchers_CurrentAndChildren_NoOptions

        [Test]
        public async Task ProfitLoss_Periodic_CompareFiscalPeriods_ApprovedVouchers_CurrentAndChildren_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareFiscalPeriods_ApprovedVouchers_CurrentAndChildren_NoOptions();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareFiscalPeriods_ApprovedVouchers_CurrentAndChildren_ByCenter_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareFiscalPeriods_ApprovedVouchers_CurrentAndChildren_NoOptions(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareFiscalPeriods_ApprovedVouchers_CurrentAndChildren_ByProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareFiscalPeriods_ApprovedVouchers_CurrentAndChildren_NoOptions(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareFiscalPeriods_ApprovedVouchers_CurrentAndChildren_ByCenterProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareFiscalPeriods_ApprovedVouchers_CurrentAndChildren_NoOptions(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region AllVouchers_CurrentAndChildren_NoOptions

        [Test]
        public async Task ProfitLoss_Periodic_CompareFiscalPeriods_AllVouchers_CurrentAndChildren_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareFiscalPeriods_AllVouchers_CurrentAndChildren_NoOptions();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareFiscalPeriods_AllVouchers_CurrentAndChildren_ByCenter_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareFiscalPeriods_AllVouchers_CurrentAndChildren_NoOptions(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareFiscalPeriods_AllVouchers_CurrentAndChildren_ByProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareFiscalPeriods_AllVouchers_CurrentAndChildren_NoOptions(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareFiscalPeriods_AllVouchers_CurrentAndChildren_ByCenterProject_NoOptions()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareFiscalPeriods_AllVouchers_CurrentAndChildren_NoOptions(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region CheckedVouchers_CurrentBranch_ClosingOption

        [Test]
        public async Task ProfitLoss_Periodic_CompareFiscalPeriods_CheckedVouchers_CurrentBranch_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareFiscalPeriods_CheckedVouchers_CurrentBranch_ClosingOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareFiscalPeriods_CheckedVouchers_CurrentBranch_ByCenter_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareFiscalPeriods_CheckedVouchers_CurrentBranch_ClosingOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareFiscalPeriods_CheckedVouchers_CurrentBranch_ByProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareFiscalPeriods_CheckedVouchers_CurrentBranch_ClosingOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareFiscalPeriods_CheckedVouchers_CurrentBranch_ByCenterProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareFiscalPeriods_CheckedVouchers_CurrentBranch_ClosingOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region FinalizedVouchers_CurrentBranch_ClosingOption

        [Test]
        public async Task ProfitLoss_Periodic_CompareFiscalPeriods_FinalizedVouchers_CurrentBranch_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareFiscalPeriods_FinalizedVouchers_CurrentBranch_ClosingOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareFiscalPeriods_FinalizedVouchers_CurrentBranch_ByCenter_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareFiscalPeriods_FinalizedVouchers_CurrentBranch_ClosingOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareFiscalPeriods_FinalizedVouchers_CurrentBranch_ByProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareFiscalPeriods_FinalizedVouchers_CurrentBranch_ClosingOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareFiscalPeriods_FinalizedVouchers_CurrentBranch_ByCenterProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareFiscalPeriods_FinalizedVouchers_CurrentBranch_ClosingOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region ConfirmedVouchers_CurrentBranch_ClosingOption

        [Test]
        public async Task ProfitLoss_Periodic_CompareFiscalPeriods_ConfirmedVouchers_CurrentBranch_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareFiscalPeriods_ConfirmedVouchers_CurrentBranch_ClosingOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareFiscalPeriods_ConfirmedVouchers_CurrentBranch_ByCenter_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareFiscalPeriods_ConfirmedVouchers_CurrentBranch_ClosingOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareFiscalPeriods_ConfirmedVouchers_CurrentBranch_ByProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareFiscalPeriods_ConfirmedVouchers_CurrentBranch_ClosingOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareFiscalPeriods_ConfirmedVouchers_CurrentBranch_ByCenterProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareFiscalPeriods_ConfirmedVouchers_CurrentBranch_ClosingOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region ApprovedVouchers_CurrentBranch_ClosingOption

        [Test]
        public async Task ProfitLoss_Periodic_CompareFiscalPeriods_ApprovedVouchers_CurrentBranch_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareFiscalPeriods_ApprovedVouchers_CurrentBranch_ClosingOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareFiscalPeriods_ApprovedVouchers_CurrentBranch_ByCenter_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareFiscalPeriods_ApprovedVouchers_CurrentBranch_ClosingOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareFiscalPeriods_ApprovedVouchers_CurrentBranch_ByProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareFiscalPeriods_ApprovedVouchers_CurrentBranch_ClosingOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareFiscalPeriods_ApprovedVouchers_CurrentBranch_ByCenterProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareFiscalPeriods_ApprovedVouchers_CurrentBranch_ClosingOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region AllVouchers_CurrentBranch_ClosingOption

        [Test]
        public async Task ProfitLoss_Periodic_CompareFiscalPeriods_AllVouchers_CurrentBranch_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareFiscalPeriods_AllVouchers_CurrentBranch_ClosingOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareFiscalPeriods_AllVouchers_CurrentBranch_ByCenter_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareFiscalPeriods_AllVouchers_CurrentBranch_ClosingOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareFiscalPeriods_AllVouchers_CurrentBranch_ByProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareFiscalPeriods_AllVouchers_CurrentBranch_ClosingOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareFiscalPeriods_AllVouchers_CurrentBranch_ByCenterProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareFiscalPeriods_AllVouchers_CurrentBranch_ClosingOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region CheckedVouchers_CurrentAndChildren_ClosingOption

        [Test]
        public async Task ProfitLoss_Periodic_CompareFiscalPeriods_CheckedVouchers_CurrentAndChildren_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareFiscalPeriods_CheckedVouchers_CurrentAndChildren_ClosingOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareFiscalPeriods_CheckedVouchers_CurrentAndChildren_ByCenter_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareFiscalPeriods_CheckedVouchers_CurrentAndChildren_ClosingOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareFiscalPeriods_CheckedVouchers_CurrentAndChildren_ByProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareFiscalPeriods_CheckedVouchers_CurrentAndChildren_ClosingOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareFiscalPeriods_CheckedVouchers_CurrentAndChildren_ByCenterProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareFiscalPeriods_CheckedVouchers_CurrentAndChildren_ClosingOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region FinalizedVouchers_CurrentAndChildren_ClosingOption

        [Test]
        public async Task ProfitLoss_Periodic_CompareFiscalPeriods_FinalizedVouchers_CurrentAndChildren_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareFiscalPeriods_FinalizedVouchers_CurrentAndChildren_ClosingOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareFiscalPeriods_FinalizedVouchers_CurrentAndChildren_ByCenter_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareFiscalPeriods_FinalizedVouchers_CurrentAndChildren_ClosingOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareFiscalPeriods_FinalizedVouchers_CurrentAndChildren_ByProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareFiscalPeriods_FinalizedVouchers_CurrentAndChildren_ClosingOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareFiscalPeriods_FinalizedVouchers_CurrentAndChildren_ByCenterProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareFiscalPeriods_FinalizedVouchers_CurrentAndChildren_ClosingOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region ConfirmedVouchers_CurrentAndChildren_ClosingOption

        [Test]
        public async Task ProfitLoss_Periodic_CompareFiscalPeriods_ConfirmedVouchers_CurrentAndChildren_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareFiscalPeriods_ConfirmedVouchers_CurrentAndChildren_ClosingOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareFiscalPeriods_ConfirmedVouchers_CurrentAndChildren_ByCenter_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareFiscalPeriods_ConfirmedVouchers_CurrentAndChildren_ClosingOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareFiscalPeriods_ConfirmedVouchers_CurrentAndChildren_ByProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareFiscalPeriods_ConfirmedVouchers_CurrentAndChildren_ClosingOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareFiscalPeriods_ConfirmedVouchers_CurrentAndChildren_ByCenterProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareFiscalPeriods_ConfirmedVouchers_CurrentAndChildren_ClosingOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region ApprovedVouchers_CurrentAndChildren_ClosingOption

        [Test]
        public async Task ProfitLoss_Periodic_CompareFiscalPeriods_ApprovedVouchers_CurrentAndChildren_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareFiscalPeriods_ApprovedVouchers_CurrentAndChildren_ClosingOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareFiscalPeriods_ApprovedVouchers_CurrentAndChildren_ByCenter_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareFiscalPeriods_ApprovedVouchers_CurrentAndChildren_ClosingOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareFiscalPeriods_ApprovedVouchers_CurrentAndChildren_ByProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareFiscalPeriods_ApprovedVouchers_CurrentAndChildren_ClosingOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareFiscalPeriods_ApprovedVouchers_CurrentAndChildren_ByCenterProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareFiscalPeriods_ApprovedVouchers_CurrentAndChildren_ClosingOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region AllVouchers_CurrentAndChildren_ClosingOption

        [Test]
        public async Task ProfitLoss_Periodic_CompareFiscalPeriods_AllVouchers_CurrentAndChildren_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareFiscalPeriods_AllVouchers_CurrentAndChildren_ClosingOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareFiscalPeriods_AllVouchers_CurrentAndChildren_ByCenter_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareFiscalPeriods_AllVouchers_CurrentAndChildren_ClosingOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareFiscalPeriods_AllVouchers_CurrentAndChildren_ByProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareFiscalPeriods_AllVouchers_CurrentAndChildren_ClosingOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareFiscalPeriods_AllVouchers_CurrentAndChildren_ByCenterProject_ClosingOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareFiscalPeriods_AllVouchers_CurrentAndChildren_ClosingOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region CheckedVouchers_CurrentBranch_TurnoverOption

        [Test]
        public async Task ProfitLoss_Periodic_CompareFiscalPeriods_CheckedVouchers_CurrentBranch_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareFiscalPeriods_CheckedVouchers_CurrentBranch_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareFiscalPeriods_CheckedVouchers_CurrentBranch_ByCenter_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareFiscalPeriods_CheckedVouchers_CurrentBranch_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareFiscalPeriods_CheckedVouchers_CurrentBranch_ByProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareFiscalPeriods_CheckedVouchers_CurrentBranch_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareFiscalPeriods_CheckedVouchers_CurrentBranch_ByCenterProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareFiscalPeriods_CheckedVouchers_CurrentBranch_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region FinalizedVouchers_CurrentBranch_TurnoverOption

        [Test]
        public async Task ProfitLoss_Periodic_CompareFiscalPeriods_FinalizedVouchers_CurrentBranch_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareFiscalPeriods_FinalizedVouchers_CurrentBranch_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareFiscalPeriods_FinalizedVouchers_CurrentBranch_ByCenter_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareFiscalPeriods_FinalizedVouchers_CurrentBranch_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareFiscalPeriods_FinalizedVouchers_CurrentBranch_ByProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareFiscalPeriods_FinalizedVouchers_CurrentBranch_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareFiscalPeriods_FinalizedVouchers_CurrentBranch_ByCenterProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareFiscalPeriods_FinalizedVouchers_CurrentBranch_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region ConfirmedVouchers_CurrentBranch_TurnoverOption

        [Test]
        public async Task ProfitLoss_Periodic_CompareFiscalPeriods_ConfirmedVouchers_CurrentBranch_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareFiscalPeriods_ConfirmedVouchers_CurrentBranch_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareFiscalPeriods_ConfirmedVouchers_CurrentBranch_ByCenter_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareFiscalPeriods_ConfirmedVouchers_CurrentBranch_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareFiscalPeriods_ConfirmedVouchers_CurrentBranch_ByProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareFiscalPeriods_ConfirmedVouchers_CurrentBranch_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareFiscalPeriods_ConfirmedVouchers_CurrentBranch_ByCenterProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareFiscalPeriods_ConfirmedVouchers_CurrentBranch_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region ApprovedVouchers_CurrentBranch_TurnoverOption

        [Test]
        public async Task ProfitLoss_Periodic_CompareFiscalPeriods_ApprovedVouchers_CurrentBranch_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareFiscalPeriods_ApprovedVouchers_CurrentBranch_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareFiscalPeriods_ApprovedVouchers_CurrentBranch_ByCenter_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareFiscalPeriods_ApprovedVouchers_CurrentBranch_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareFiscalPeriods_ApprovedVouchers_CurrentBranch_ByProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareFiscalPeriods_ApprovedVouchers_CurrentBranch_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareFiscalPeriods_ApprovedVouchers_CurrentBranch_ByCenterProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareFiscalPeriods_ApprovedVouchers_CurrentBranch_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region AllVouchers_CurrentBranch_TurnoverOption

        [Test]
        public async Task ProfitLoss_Periodic_CompareFiscalPeriods_AllVouchers_CurrentBranch_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareFiscalPeriods_AllVouchers_CurrentBranch_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareFiscalPeriods_AllVouchers_CurrentBranch_ByCenter_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareFiscalPeriods_AllVouchers_CurrentBranch_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareFiscalPeriods_AllVouchers_CurrentBranch_ByProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareFiscalPeriods_AllVouchers_CurrentBranch_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareFiscalPeriods_AllVouchers_CurrentBranch_ByCenterProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareFiscalPeriods_AllVouchers_CurrentBranch_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region CheckedVouchers_CurrentAndChildren_TurnoverOption

        [Test]
        public async Task ProfitLoss_Periodic_CompareFiscalPeriods_CheckedVouchers_CurrentAndChildren_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareFiscalPeriods_CheckedVouchers_CurrentAndChildren_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareFiscalPeriods_CheckedVouchers_CurrentAndChildren_ByCenter_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareFiscalPeriods_CheckedVouchers_CurrentAndChildren_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareFiscalPeriods_CheckedVouchers_CurrentAndChildren_ByProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareFiscalPeriods_CheckedVouchers_CurrentAndChildren_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareFiscalPeriods_CheckedVouchers_CurrentAndChildren_ByCenterProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareFiscalPeriods_CheckedVouchers_CurrentAndChildren_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region FinalizedVouchers_CurrentAndChildren_TurnoverOption

        [Test]
        public async Task ProfitLoss_Periodic_CompareFiscalPeriods_FinalizedVouchers_CurrentAndChildren_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareFiscalPeriods_FinalizedVouchers_CurrentAndChildren_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareFiscalPeriods_FinalizedVouchers_CurrentAndChildren_ByCenter_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareFiscalPeriods_FinalizedVouchers_CurrentAndChildren_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareFiscalPeriods_FinalizedVouchers_CurrentAndChildren_ByProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareFiscalPeriods_FinalizedVouchers_CurrentAndChildren_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareFiscalPeriods_FinalizedVouchers_CurrentAndChildren_ByCenterProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareFiscalPeriods_FinalizedVouchers_CurrentAndChildren_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region ConfirmedVouchers_CurrentAndChildren_TurnoverOption

        [Test]
        public async Task ProfitLoss_Periodic_CompareFiscalPeriods_ConfirmedVouchers_CurrentAndChildren_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareFiscalPeriods_ConfirmedVouchers_CurrentAndChildren_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareFiscalPeriods_ConfirmedVouchers_CurrentAndChildren_ByCenter_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareFiscalPeriods_ConfirmedVouchers_CurrentAndChildren_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareFiscalPeriods_ConfirmedVouchers_CurrentAndChildren_ByProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareFiscalPeriods_ConfirmedVouchers_CurrentAndChildren_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareFiscalPeriods_ConfirmedVouchers_CurrentAndChildren_ByCenterProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareFiscalPeriods_ConfirmedVouchers_CurrentAndChildren_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region ApprovedVouchers_CurrentAndChildren_TurnoverOption

        [Test]
        public async Task ProfitLoss_Periodic_CompareFiscalPeriods_ApprovedVouchers_CurrentAndChildren_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareFiscalPeriods_ApprovedVouchers_CurrentAndChildren_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareFiscalPeriods_ApprovedVouchers_CurrentAndChildren_ByCenter_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareFiscalPeriods_ApprovedVouchers_CurrentAndChildren_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareFiscalPeriods_ApprovedVouchers_CurrentAndChildren_ByProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareFiscalPeriods_ApprovedVouchers_CurrentAndChildren_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareFiscalPeriods_ApprovedVouchers_CurrentAndChildren_ByCenterProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareFiscalPeriods_ApprovedVouchers_CurrentAndChildren_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region AllVouchers_CurrentAndChildren_TurnoverOption

        [Test]
        public async Task ProfitLoss_Periodic_CompareFiscalPeriods_AllVouchers_CurrentAndChildren_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareFiscalPeriods_AllVouchers_CurrentAndChildren_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareFiscalPeriods_AllVouchers_CurrentAndChildren_ByCenter_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareFiscalPeriods_AllVouchers_CurrentAndChildren_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareFiscalPeriods_AllVouchers_CurrentAndChildren_ByProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareFiscalPeriods_AllVouchers_CurrentAndChildren_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareFiscalPeriods_AllVouchers_CurrentAndChildren_ByCenterProject_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareFiscalPeriods_AllVouchers_CurrentAndChildren_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region CheckedVouchers_CurrentBranch_ClosingOption_TurnoverOption

        [Test]
        public async Task ProfitLoss_Periodic_CompareFiscalPeriods_CheckedVouchers_CurrentBranch_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareFiscalPeriods_CheckedVouchers_CurrentBranch_ClosingOption_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareFiscalPeriods_CheckedVouchers_CurrentBranch_ByCenter_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareFiscalPeriods_CheckedVouchers_CurrentBranch_ClosingOption_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareFiscalPeriods_CheckedVouchers_CurrentBranch_ByProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareFiscalPeriods_CheckedVouchers_CurrentBranch_ClosingOption_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareFiscalPeriods_CheckedVouchers_CurrentBranch_ByCenterProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareFiscalPeriods_CheckedVouchers_CurrentBranch_ClosingOption_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region FinalizedVouchers_CurrentBranch_ClosingOption_TurnoverOption

        [Test]
        public async Task ProfitLoss_Periodic_CompareFiscalPeriods_FinalizedVouchers_CurrentBranch_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareFiscalPeriods_FinalizedVouchers_CurrentBranch_ClosingOption_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareFiscalPeriods_FinalizedVouchers_CurrentBranch_ByCenter_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareFiscalPeriods_FinalizedVouchers_CurrentBranch_ClosingOption_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareFiscalPeriods_FinalizedVouchers_CurrentBranch_ByProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareFiscalPeriods_FinalizedVouchers_CurrentBranch_ClosingOption_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareFiscalPeriods_FinalizedVouchers_CurrentBranch_ByCenterProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareFiscalPeriods_FinalizedVouchers_CurrentBranch_ClosingOption_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region ConfirmedVouchers_CurrentBranch_ClosingOption_TurnoverOption

        [Test]
        public async Task ProfitLoss_Periodic_CompareFiscalPeriods_ConfirmedVouchers_CurrentBranch_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareFiscalPeriods_ConfirmedVouchers_CurrentBranch_ClosingOption_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareFiscalPeriods_ConfirmedVouchers_CurrentBranch_ByCenter_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareFiscalPeriods_ConfirmedVouchers_CurrentBranch_ClosingOption_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareFiscalPeriods_ConfirmedVouchers_CurrentBranch_ByProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareFiscalPeriods_ConfirmedVouchers_CurrentBranch_ClosingOption_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareFiscalPeriods_ConfirmedVouchers_CurrentBranch_ByCenterProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareFiscalPeriods_ConfirmedVouchers_CurrentBranch_ClosingOption_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region ApprovedVouchers_CurrentBranch_ClosingOption_TurnoverOption

        [Test]
        public async Task ProfitLoss_Periodic_CompareFiscalPeriods_ApprovedVouchers_CurrentBranch_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareFiscalPeriods_ApprovedVouchers_CurrentBranch_ClosingOption_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareFiscalPeriods_ApprovedVouchers_CurrentBranch_ByCenter_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareFiscalPeriods_ApprovedVouchers_CurrentBranch_ClosingOption_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareFiscalPeriods_ApprovedVouchers_CurrentBranch_ByProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareFiscalPeriods_ApprovedVouchers_CurrentBranch_ClosingOption_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareFiscalPeriods_ApprovedVouchers_CurrentBranch_ByCenterProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareFiscalPeriods_ApprovedVouchers_CurrentBranch_ClosingOption_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region AllVouchers_CurrentBranch_ClosingOption_TurnoverOption

        [Test]
        public async Task ProfitLoss_Periodic_CompareFiscalPeriods_AllVouchers_CurrentBranch_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareFiscalPeriods_AllVouchers_CurrentBranch_ClosingOption_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareFiscalPeriods_AllVouchers_CurrentBranch_ByCenter_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareFiscalPeriods_AllVouchers_CurrentBranch_ClosingOption_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareFiscalPeriods_AllVouchers_CurrentBranch_ByProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareFiscalPeriods_AllVouchers_CurrentBranch_ClosingOption_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareFiscalPeriods_AllVouchers_CurrentBranch_ByCenterProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareFiscalPeriods_AllVouchers_CurrentBranch_ClosingOption_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region CheckedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption

        [Test]
        public async Task ProfitLoss_Periodic_CompareFiscalPeriods_CheckedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareFiscalPeriods_CheckedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareFiscalPeriods_CheckedVouchers_CurrentAndChildren_ByCenter_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareFiscalPeriods_CheckedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareFiscalPeriods_CheckedVouchers_CurrentAndChildren_ByProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareFiscalPeriods_CheckedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareFiscalPeriods_CheckedVouchers_CurrentAndChildren_ByCenterProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareFiscalPeriods_CheckedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region FinalizedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption

        [Test]
        public async Task ProfitLoss_Periodic_CompareFiscalPeriods_FinalizedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareFiscalPeriods_FinalizedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareFiscalPeriods_FinalizedVouchers_CurrentAndChildren_ByCenter_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareFiscalPeriods_FinalizedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareFiscalPeriods_FinalizedVouchers_CurrentAndChildren_ByProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareFiscalPeriods_FinalizedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareFiscalPeriods_FinalizedVouchers_CurrentAndChildren_ByCenterProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareFiscalPeriods_FinalizedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region ConfirmedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption

        [Test]
        public async Task ProfitLoss_Periodic_CompareFiscalPeriods_ConfirmedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareFiscalPeriods_ConfirmedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareFiscalPeriods_ConfirmedVouchers_CurrentAndChildren_ByCenter_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareFiscalPeriods_ConfirmedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareFiscalPeriods_ConfirmedVouchers_CurrentAndChildren_ByProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareFiscalPeriods_ConfirmedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareFiscalPeriods_ConfirmedVouchers_CurrentAndChildren_ByCenterProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareFiscalPeriods_ConfirmedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region ApprovedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption

        [Test]
        public async Task ProfitLoss_Periodic_CompareFiscalPeriods_ApprovedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareFiscalPeriods_ApprovedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareFiscalPeriods_ApprovedVouchers_CurrentAndChildren_ByCenter_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareFiscalPeriods_ApprovedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareFiscalPeriods_ApprovedVouchers_CurrentAndChildren_ByProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareFiscalPeriods_ApprovedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareFiscalPeriods_ApprovedVouchers_CurrentAndChildren_ByCenterProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareFiscalPeriods_ApprovedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #region AllVouchers_CurrentAndChildren_ClosingOption_TurnoverOption

        [Test]
        public async Task ProfitLoss_Periodic_CompareFiscalPeriods_AllVouchers_CurrentAndChildren_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareFiscalPeriods_AllVouchers_CurrentAndChildren_ClosingOption_TurnoverOption();

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareFiscalPeriods_AllVouchers_CurrentAndChildren_ByCenter_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareFiscalPeriods_AllVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
                TestCenterId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareFiscalPeriods_AllVouchers_CurrentAndChildren_ByProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareFiscalPeriods_AllVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
                null, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        [Test]
        public async Task ProfitLoss_Periodic_CompareFiscalPeriods_AllVouchers_CurrentAndChildren_ByCenterProject_ClosingOption_TurnoverOption()
        {
            // Arrange & Act
            var profitLoss = await GetProfitLoss_Periodic_CompareFiscalPeriods_AllVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(profitLoss, Is.Not.Null);
        }

        #endregion

        #endregion

        #endregion

        private void SetUp()
        {
            var repoContext = GetRepositoryContext();
            var sysRepo = GetSystemRepository(repoContext);
            var utility = new ReportDirectUtility(repoContext, sysRepo);
            _repository = new ProfitLossRepositoryDirect(repoContext, sysRepo, utility);
        }

        #region GetProfitLoss_CurrentBranch_NoOptions

        private async Task<ProfitLossViewModel> GetProfitLoss_CheckedVouchers_CurrentBranch_NoOptions(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseCheckedVouchers()
                .Build();
            var parameters = GetTestParameters(gridOptions, false, false, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_FinalizedVouchers_CurrentBranch_NoOptions(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseFinalizedVouchers()
                .Build();
            var parameters = GetTestParameters(gridOptions, false, false, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_ConfirmedVouchers_CurrentBranch_NoOptions(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseConfirmedVouchers()
                .Build();
            var parameters = GetTestParameters(gridOptions, false, false, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_ApprovedVouchers_CurrentBranch_NoOptions(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseApprovedVouchers()
                .Build();
            var parameters = GetTestParameters(gridOptions, false, false, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_AllVouchers_CurrentBranch_NoOptions(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .Build();
            var parameters = GetTestParameters(gridOptions, false, false, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossAsync(parameters, balanceItems);
        }

        #endregion

        #region GetProfitLoss_CurrentAndChildren_NoOptions

        private async Task<ProfitLossViewModel> GetProfitLoss_CheckedVouchers_CurrentAndChildren_NoOptions(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseCheckedVouchers()
                .Build();
            var parameters = GetTestParameters(gridOptions, false, false, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_FinalizedVouchers_CurrentAndChildren_NoOptions(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseFinalizedVouchers()
                .Build();
            var parameters = GetTestParameters(gridOptions, false, false, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_ConfirmedVouchers_CurrentAndChildren_NoOptions(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseConfirmedVouchers()
                .Build();
            var parameters = GetTestParameters(gridOptions, false, false, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_ApprovedVouchers_CurrentAndChildren_NoOptions(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseApprovedVouchers()
                .Build();
            var parameters = GetTestParameters(gridOptions, false, false, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_AllVouchers_CurrentAndChildren_NoOptions(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .Build();
            var parameters = GetTestParameters(gridOptions, false, false, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossAsync(parameters, balanceItems);
        }

        #endregion

        #region GetProfitLoss_CurrentBranch_ClosingOption

        private async Task<ProfitLossViewModel> GetProfitLoss_CheckedVouchers_CurrentBranch_ClosingOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseCheckedVouchers()
                .Build();
            var parameters = GetTestParameters(gridOptions, false, true, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_FinalizedVouchers_CurrentBranch_ClosingOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseFinalizedVouchers()
                .Build();
            var parameters = GetTestParameters(gridOptions, false, true, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_ConfirmedVouchers_CurrentBranch_ClosingOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseConfirmedVouchers()
                .Build();
            var parameters = GetTestParameters(gridOptions, false, true, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_ApprovedVouchers_CurrentBranch_ClosingOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseApprovedVouchers()
                .Build();
            var parameters = GetTestParameters(gridOptions, false, true, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_AllVouchers_CurrentBranch_ClosingOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .Build();
            var parameters = GetTestParameters(gridOptions, false, true, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossAsync(parameters, balanceItems);
        }

        #endregion

        #region GetProfitLoss_CurrentAndChildren_ClosingOption

        private async Task<ProfitLossViewModel> GetProfitLoss_CheckedVouchers_CurrentAndChildren_ClosingOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseCheckedVouchers()
                .Build();
            var parameters = GetTestParameters(gridOptions, false, true, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_FinalizedVouchers_CurrentAndChildren_ClosingOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseFinalizedVouchers()
                .Build();
            var parameters = GetTestParameters(gridOptions, false, true, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_ConfirmedVouchers_CurrentAndChildren_ClosingOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseConfirmedVouchers()
                .Build();
            var parameters = GetTestParameters(gridOptions, false, true, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_ApprovedVouchers_CurrentAndChildren_ClosingOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseApprovedVouchers()
                .Build();
            var parameters = GetTestParameters(gridOptions, false, true, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_AllVouchers_CurrentAndChildren_ClosingOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .Build();
            var parameters = GetTestParameters(gridOptions, false, true, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossAsync(parameters, balanceItems);
        }

        #endregion

        #region GetProfitLoss_CurrentBranch_TurnoverOption

        private async Task<ProfitLossViewModel> GetProfitLoss_CheckedVouchers_CurrentBranch_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseCheckedVouchers()
                .Build();
            var parameters = GetTestParameters(gridOptions, true, false, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_FinalizedVouchers_CurrentBranch_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseFinalizedVouchers()
                .Build();
            var parameters = GetTestParameters(gridOptions, true, false, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_ConfirmedVouchers_CurrentBranch_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseConfirmedVouchers()
                .Build();
            var parameters = GetTestParameters(gridOptions, true, false, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_ApprovedVouchers_CurrentBranch_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseApprovedVouchers()
                .Build();
            var parameters = GetTestParameters(gridOptions, true, false, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_AllVouchers_CurrentBranch_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .Build();
            var parameters = GetTestParameters(gridOptions, true, false, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossAsync(parameters, balanceItems);
        }

        #endregion

        #region GetProfitLoss_CurrentAndChildren_TurnoverOption

        private async Task<ProfitLossViewModel> GetProfitLoss_CheckedVouchers_CurrentAndChildren_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseCheckedVouchers()
                .Build();
            var parameters = GetTestParameters(gridOptions, true, false, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_FinalizedVouchers_CurrentAndChildren_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseFinalizedVouchers()
                .Build();
            var parameters = GetTestParameters(gridOptions, true, false, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_ConfirmedVouchers_CurrentAndChildren_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseConfirmedVouchers()
                .Build();
            var parameters = GetTestParameters(gridOptions, true, false, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_ApprovedVouchers_CurrentAndChildren_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseApprovedVouchers()
                .Build();
            var parameters = GetTestParameters(gridOptions, true, false, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_AllVouchers_CurrentAndChildren_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .Build();
            var parameters = GetTestParameters(gridOptions, true, false, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossAsync(parameters, balanceItems);
        }

        #endregion

        #region GetProfitLoss_CurrentBranch_ClosingOption_TurnoverOption

        private async Task<ProfitLossViewModel> GetProfitLoss_CheckedVouchers_CurrentBranch_ClosingOption_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseCheckedVouchers()
                .Build();
            var parameters = GetTestParameters(gridOptions, true, true, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_FinalizedVouchers_CurrentBranch_ClosingOption_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseFinalizedVouchers()
                .Build();
            var parameters = GetTestParameters(gridOptions, true, true, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_ConfirmedVouchers_CurrentBranch_ClosingOption_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseConfirmedVouchers()
                .Build();
            var parameters = GetTestParameters(gridOptions, true, true, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_ApprovedVouchers_CurrentBranch_ClosingOption_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseApprovedVouchers()
                .Build();
            var parameters = GetTestParameters(gridOptions, true, true, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_AllVouchers_CurrentBranch_ClosingOption_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .Build();
            var parameters = GetTestParameters(gridOptions, true, true, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossAsync(parameters, balanceItems);
        }

        #endregion

        #region GetProfitLoss_CurrentAndChildren_ClosingOption_TurnoverOption

        private async Task<ProfitLossViewModel> GetProfitLoss_CheckedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseCheckedVouchers()
                .Build();
            var parameters = GetTestParameters(gridOptions, true, true, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_FinalizedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseFinalizedVouchers()
                .Build();
            var parameters = GetTestParameters(gridOptions, true, true, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_ConfirmedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseConfirmedVouchers()
                .Build();
            var parameters = GetTestParameters(gridOptions, true, true, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_ApprovedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseApprovedVouchers()
                .Build();
            var parameters = GetTestParameters(gridOptions, true, true, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_AllVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .Build();
            var parameters = GetTestParameters(gridOptions, true, true, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossAsync(parameters, balanceItems);
        }

        #endregion

        #region GetProfitLoss_CompareBranches_CurrentBranch_NoOptions

        private async Task<ProfitLossViewModel> GetProfitLoss_CompareBranches_CheckedVouchers_CurrentBranch_NoOptions(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseCheckedVouchers()
                .Build();
            var parameters = GetByBranchesTestParameters(gridOptions, false, false, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossByBranchesAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_CompareBranches_FinalizedVouchers_CurrentBranch_NoOptions(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseFinalizedVouchers()
                .Build();
            var parameters = GetByBranchesTestParameters(gridOptions, false, false, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossByBranchesAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_CompareBranches_ConfirmedVouchers_CurrentBranch_NoOptions(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseConfirmedVouchers()
                .Build();
            var parameters = GetByBranchesTestParameters(gridOptions, false, false, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossByBranchesAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_CompareBranches_ApprovedVouchers_CurrentBranch_NoOptions(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseApprovedVouchers()
                .Build();
            var parameters = GetByBranchesTestParameters(gridOptions, false, false, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossByBranchesAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_CompareBranches_AllVouchers_CurrentBranch_NoOptions(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .Build();
            var parameters = GetByBranchesTestParameters(gridOptions, false, false, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossByBranchesAsync(parameters, balanceItems);
        }

        #endregion

        #region GetProfitLoss_CompareBranches_CurrentAndChildren_NoOptions

        private async Task<ProfitLossViewModel> GetProfitLoss_CompareBranches_CheckedVouchers_CurrentAndChildren_NoOptions(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseCheckedVouchers()
                .Build();
            var parameters = GetByBranchesTestParameters(gridOptions, false, false, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossByBranchesAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_CompareBranches_FinalizedVouchers_CurrentAndChildren_NoOptions(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseFinalizedVouchers()
                .Build();
            var parameters = GetByBranchesTestParameters(gridOptions, false, false, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossByBranchesAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_CompareBranches_ConfirmedVouchers_CurrentAndChildren_NoOptions(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseConfirmedVouchers()
                .Build();
            var parameters = GetByBranchesTestParameters(gridOptions, false, false, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossByBranchesAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_CompareBranches_ApprovedVouchers_CurrentAndChildren_NoOptions(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseApprovedVouchers()
                .Build();
            var parameters = GetByBranchesTestParameters(gridOptions, false, false, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossByBranchesAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_CompareBranches_AllVouchers_CurrentAndChildren_NoOptions(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .Build();
            var parameters = GetByBranchesTestParameters(gridOptions, false, false, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossByBranchesAsync(parameters, balanceItems);
        }

        #endregion

        #region GetProfitLoss_CompareBranches_CurrentBranch_ClosingOption

        private async Task<ProfitLossViewModel> GetProfitLoss_CompareBranches_CheckedVouchers_CurrentBranch_ClosingOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseCheckedVouchers()
                .Build();
            var parameters = GetByBranchesTestParameters(gridOptions, false, true, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossByBranchesAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_CompareBranches_FinalizedVouchers_CurrentBranch_ClosingOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseFinalizedVouchers()
                .Build();
            var parameters = GetByBranchesTestParameters(gridOptions, false, true, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossByBranchesAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_CompareBranches_ConfirmedVouchers_CurrentBranch_ClosingOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseConfirmedVouchers()
                .Build();
            var parameters = GetByBranchesTestParameters(gridOptions, false, true, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossByBranchesAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_CompareBranches_ApprovedVouchers_CurrentBranch_ClosingOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseApprovedVouchers()
                .Build();
            var parameters = GetByBranchesTestParameters(gridOptions, false, true, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossByBranchesAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_CompareBranches_AllVouchers_CurrentBranch_ClosingOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .Build();
            var parameters = GetByBranchesTestParameters(gridOptions, false, true, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossByBranchesAsync(parameters, balanceItems);
        }

        #endregion

        #region GetProfitLoss_CompareBranches_CurrentAndChildren_ClosingOption

        private async Task<ProfitLossViewModel> GetProfitLoss_CompareBranches_CheckedVouchers_CurrentAndChildren_ClosingOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseCheckedVouchers()
                .Build();
            var parameters = GetByBranchesTestParameters(gridOptions, false, true, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossByBranchesAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_CompareBranches_FinalizedVouchers_CurrentAndChildren_ClosingOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseFinalizedVouchers()
                .Build();
            var parameters = GetByBranchesTestParameters(gridOptions, false, true, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossByBranchesAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_CompareBranches_ConfirmedVouchers_CurrentAndChildren_ClosingOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseConfirmedVouchers()
                .Build();
            var parameters = GetByBranchesTestParameters(gridOptions, false, true, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossByBranchesAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_CompareBranches_ApprovedVouchers_CurrentAndChildren_ClosingOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseApprovedVouchers()
                .Build();
            var parameters = GetByBranchesTestParameters(gridOptions, false, true, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossByBranchesAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_CompareBranches_AllVouchers_CurrentAndChildren_ClosingOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .Build();
            var parameters = GetByBranchesTestParameters(gridOptions, false, true, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossByBranchesAsync(parameters, balanceItems);
        }

        #endregion

        #region GetProfitLoss_CompareBranches_CurrentBranch_TurnoverOption

        private async Task<ProfitLossViewModel> GetProfitLoss_CompareBranches_CheckedVouchers_CurrentBranch_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseCheckedVouchers()
                .Build();
            var parameters = GetByBranchesTestParameters(gridOptions, true, false, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossByBranchesAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_CompareBranches_FinalizedVouchers_CurrentBranch_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseFinalizedVouchers()
                .Build();
            var parameters = GetByBranchesTestParameters(gridOptions, true, false, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossByBranchesAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_CompareBranches_ConfirmedVouchers_CurrentBranch_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseConfirmedVouchers()
                .Build();
            var parameters = GetByBranchesTestParameters(gridOptions, true, false, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossByBranchesAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_CompareBranches_ApprovedVouchers_CurrentBranch_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseApprovedVouchers()
                .Build();
            var parameters = GetByBranchesTestParameters(gridOptions, true, false, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossByBranchesAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_CompareBranches_AllVouchers_CurrentBranch_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .Build();
            var parameters = GetByBranchesTestParameters(gridOptions, true, false, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossByBranchesAsync(parameters, balanceItems);
        }

        #endregion

        #region GetProfitLoss_CompareBranches_CurrentAndChildren_TurnoverOption

        private async Task<ProfitLossViewModel> GetProfitLoss_CompareBranches_CheckedVouchers_CurrentAndChildren_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseCheckedVouchers()
                .Build();
            var parameters = GetByBranchesTestParameters(gridOptions, true, false, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossByBranchesAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_CompareBranches_FinalizedVouchers_CurrentAndChildren_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseFinalizedVouchers()
                .Build();
            var parameters = GetByBranchesTestParameters(gridOptions, true, false, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossByBranchesAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_CompareBranches_ConfirmedVouchers_CurrentAndChildren_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseConfirmedVouchers()
                .Build();
            var parameters = GetByBranchesTestParameters(gridOptions, true, false, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossByBranchesAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_CompareBranches_ApprovedVouchers_CurrentAndChildren_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseApprovedVouchers()
                .Build();
            var parameters = GetByBranchesTestParameters(gridOptions, true, false, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossByBranchesAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_CompareBranches_AllVouchers_CurrentAndChildren_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .Build();
            var parameters = GetByBranchesTestParameters(gridOptions, true, false, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossByBranchesAsync(parameters, balanceItems);
        }

        #endregion

        #region GetProfitLoss_CompareBranches_CurrentBranch_ClosingOption_TurnoverOption

        private async Task<ProfitLossViewModel> GetProfitLoss_CompareBranches_CheckedVouchers_CurrentBranch_ClosingOption_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseCheckedVouchers()
                .Build();
            var parameters = GetByBranchesTestParameters(gridOptions, true, true, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossByBranchesAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_CompareBranches_FinalizedVouchers_CurrentBranch_ClosingOption_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseFinalizedVouchers()
                .Build();
            var parameters = GetByBranchesTestParameters(gridOptions, true, true, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossByBranchesAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_CompareBranches_ConfirmedVouchers_CurrentBranch_ClosingOption_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseConfirmedVouchers()
                .Build();
            var parameters = GetByBranchesTestParameters(gridOptions, true, true, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossByBranchesAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_CompareBranches_ApprovedVouchers_CurrentBranch_ClosingOption_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseApprovedVouchers()
                .Build();
            var parameters = GetByBranchesTestParameters(gridOptions, true, true, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossByBranchesAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_CompareBranches_AllVouchers_CurrentBranch_ClosingOption_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .Build();
            var parameters = GetByBranchesTestParameters(gridOptions, true, true, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossByBranchesAsync(parameters, balanceItems);
        }

        #endregion

        #region GetProfitLoss_CompareBranches_CurrentAndChildren_ClosingOption_TurnoverOption

        private async Task<ProfitLossViewModel> GetProfitLoss_CompareBranches_CheckedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseCheckedVouchers()
                .Build();
            var parameters = GetByBranchesTestParameters(gridOptions, true, true, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossByBranchesAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_CompareBranches_FinalizedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseFinalizedVouchers()
                .Build();
            var parameters = GetByBranchesTestParameters(gridOptions, true, true, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossByBranchesAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_CompareBranches_ConfirmedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseConfirmedVouchers()
                .Build();
            var parameters = GetByBranchesTestParameters(gridOptions, true, true, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossByBranchesAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_CompareBranches_ApprovedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseApprovedVouchers()
                .Build();
            var parameters = GetByBranchesTestParameters(gridOptions, true, true, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossByBranchesAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_CompareBranches_AllVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .Build();
            var parameters = GetByBranchesTestParameters(gridOptions, true, true, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossByBranchesAsync(parameters, balanceItems);
        }

        #endregion

        #region GetProfitLoss_CompareProjects_CurrentBranch_NoOptions

        private async Task<ProfitLossViewModel> GetProfitLoss_CompareProjects_CheckedVouchers_CurrentBranch_NoOptions(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseCheckedVouchers()
                .Build();
            var parameters = GetByProjectsTestParameters(gridOptions, false, false, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossByProjectsAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_CompareProjects_FinalizedVouchers_CurrentBranch_NoOptions(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseFinalizedVouchers()
                .Build();
            var parameters = GetByProjectsTestParameters(gridOptions, false, false, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossByProjectsAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_CompareProjects_ConfirmedVouchers_CurrentBranch_NoOptions(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseConfirmedVouchers()
                .Build();
            var parameters = GetByProjectsTestParameters(gridOptions, false, false, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossByProjectsAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_CompareProjects_ApprovedVouchers_CurrentBranch_NoOptions(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseApprovedVouchers()
                .Build();
            var parameters = GetByProjectsTestParameters(gridOptions, false, false, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossByProjectsAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_CompareProjects_AllVouchers_CurrentBranch_NoOptions(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .Build();
            var parameters = GetByProjectsTestParameters(gridOptions, false, false, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossByProjectsAsync(parameters, balanceItems);
        }

        #endregion

        #region GetProfitLoss_CompareProjects_CurrentAndChildren_NoOptions

        private async Task<ProfitLossViewModel> GetProfitLoss_CompareProjects_CheckedVouchers_CurrentAndChildren_NoOptions(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseCheckedVouchers()
                .Build();
            var parameters = GetByProjectsTestParameters(gridOptions, false, false, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossByProjectsAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_CompareProjects_FinalizedVouchers_CurrentAndChildren_NoOptions(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseFinalizedVouchers()
                .Build();
            var parameters = GetByProjectsTestParameters(gridOptions, false, false, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossByProjectsAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_CompareProjects_ConfirmedVouchers_CurrentAndChildren_NoOptions(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseConfirmedVouchers()
                .Build();
            var parameters = GetByProjectsTestParameters(gridOptions, false, false, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossByProjectsAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_CompareProjects_ApprovedVouchers_CurrentAndChildren_NoOptions(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseApprovedVouchers()
                .Build();
            var parameters = GetByProjectsTestParameters(gridOptions, false, false, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossByProjectsAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_CompareProjects_AllVouchers_CurrentAndChildren_NoOptions(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .Build();
            var parameters = GetByProjectsTestParameters(gridOptions, false, false, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossByProjectsAsync(parameters, balanceItems);
        }

        #endregion

        #region GetProfitLoss_CompareProjects_CurrentBranch_ClosingOption

        private async Task<ProfitLossViewModel> GetProfitLoss_CompareProjects_CheckedVouchers_CurrentBranch_ClosingOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseCheckedVouchers()
                .Build();
            var parameters = GetByProjectsTestParameters(gridOptions, false, true, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossByProjectsAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_CompareProjects_FinalizedVouchers_CurrentBranch_ClosingOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseFinalizedVouchers()
                .Build();
            var parameters = GetByProjectsTestParameters(gridOptions, false, true, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossByProjectsAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_CompareProjects_ConfirmedVouchers_CurrentBranch_ClosingOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseConfirmedVouchers()
                .Build();
            var parameters = GetByProjectsTestParameters(gridOptions, false, true, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossByProjectsAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_CompareProjects_ApprovedVouchers_CurrentBranch_ClosingOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseApprovedVouchers()
                .Build();
            var parameters = GetByProjectsTestParameters(gridOptions, false, true, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossByProjectsAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_CompareProjects_AllVouchers_CurrentBranch_ClosingOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .Build();
            var parameters = GetByProjectsTestParameters(gridOptions, false, true, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossByProjectsAsync(parameters, balanceItems);
        }

        #endregion

        #region GetProfitLoss_CompareProjects_CurrentAndChildren_ClosingOption

        private async Task<ProfitLossViewModel> GetProfitLoss_CompareProjects_CheckedVouchers_CurrentAndChildren_ClosingOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseCheckedVouchers()
                .Build();
            var parameters = GetByProjectsTestParameters(gridOptions, false, true, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossByProjectsAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_CompareProjects_FinalizedVouchers_CurrentAndChildren_ClosingOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseFinalizedVouchers()
                .Build();
            var parameters = GetByProjectsTestParameters(gridOptions, false, true, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossByProjectsAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_CompareProjects_ConfirmedVouchers_CurrentAndChildren_ClosingOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseConfirmedVouchers()
                .Build();
            var parameters = GetByProjectsTestParameters(gridOptions, false, true, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossByProjectsAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_CompareProjects_ApprovedVouchers_CurrentAndChildren_ClosingOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseApprovedVouchers()
                .Build();
            var parameters = GetByProjectsTestParameters(gridOptions, false, true, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossByProjectsAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_CompareProjects_AllVouchers_CurrentAndChildren_ClosingOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .Build();
            var parameters = GetByProjectsTestParameters(gridOptions, false, true, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossByProjectsAsync(parameters, balanceItems);
        }

        #endregion

        #region GetProfitLoss_CompareProjects_CurrentBranch_TurnoverOption

        private async Task<ProfitLossViewModel> GetProfitLoss_CompareProjects_CheckedVouchers_CurrentBranch_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseCheckedVouchers()
                .Build();
            var parameters = GetByProjectsTestParameters(gridOptions, true, false, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossByProjectsAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_CompareProjects_FinalizedVouchers_CurrentBranch_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseFinalizedVouchers()
                .Build();
            var parameters = GetByProjectsTestParameters(gridOptions, true, false, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossByProjectsAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_CompareProjects_ConfirmedVouchers_CurrentBranch_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseConfirmedVouchers()
                .Build();
            var parameters = GetByProjectsTestParameters(gridOptions, true, false, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossByProjectsAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_CompareProjects_ApprovedVouchers_CurrentBranch_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseApprovedVouchers()
                .Build();
            var parameters = GetByProjectsTestParameters(gridOptions, true, false, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossByProjectsAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_CompareProjects_AllVouchers_CurrentBranch_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .Build();
            var parameters = GetByProjectsTestParameters(gridOptions, true, false, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossByProjectsAsync(parameters, balanceItems);
        }

        #endregion

        #region GetProfitLoss_CompareProjects_CurrentAndChildren_TurnoverOption

        private async Task<ProfitLossViewModel> GetProfitLoss_CompareProjects_CheckedVouchers_CurrentAndChildren_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseCheckedVouchers()
                .Build();
            var parameters = GetByProjectsTestParameters(gridOptions, true, false, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossByProjectsAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_CompareProjects_FinalizedVouchers_CurrentAndChildren_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseFinalizedVouchers()
                .Build();
            var parameters = GetByProjectsTestParameters(gridOptions, true, false, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossByProjectsAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_CompareProjects_ConfirmedVouchers_CurrentAndChildren_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseConfirmedVouchers()
                .Build();
            var parameters = GetByProjectsTestParameters(gridOptions, true, false, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossByProjectsAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_CompareProjects_ApprovedVouchers_CurrentAndChildren_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseApprovedVouchers()
                .Build();
            var parameters = GetByProjectsTestParameters(gridOptions, true, false, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossByProjectsAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_CompareProjects_AllVouchers_CurrentAndChildren_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .Build();
            var parameters = GetByProjectsTestParameters(gridOptions, true, false, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossByProjectsAsync(parameters, balanceItems);
        }

        #endregion

        #region GetProfitLoss_CompareProjects_CurrentBranch_ClosingOption_TurnoverOption

        private async Task<ProfitLossViewModel> GetProfitLoss_CompareProjects_CheckedVouchers_CurrentBranch_ClosingOption_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseCheckedVouchers()
                .Build();
            var parameters = GetByProjectsTestParameters(gridOptions, true, true, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossByProjectsAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_CompareProjects_FinalizedVouchers_CurrentBranch_ClosingOption_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseFinalizedVouchers()
                .Build();
            var parameters = GetByProjectsTestParameters(gridOptions, true, true, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossByProjectsAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_CompareProjects_ConfirmedVouchers_CurrentBranch_ClosingOption_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseConfirmedVouchers()
                .Build();
            var parameters = GetByProjectsTestParameters(gridOptions, true, true, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossByProjectsAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_CompareProjects_ApprovedVouchers_CurrentBranch_ClosingOption_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseApprovedVouchers()
                .Build();
            var parameters = GetByProjectsTestParameters(gridOptions, true, true, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossByProjectsAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_CompareProjects_AllVouchers_CurrentBranch_ClosingOption_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .Build();
            var parameters = GetByProjectsTestParameters(gridOptions, true, true, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossByProjectsAsync(parameters, balanceItems);
        }

        #endregion

        #region GetProfitLoss_CompareProjects_CurrentAndChildren_ClosingOption_TurnoverOption

        private async Task<ProfitLossViewModel> GetProfitLoss_CompareProjects_CheckedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseCheckedVouchers()
                .Build();
            var parameters = GetByProjectsTestParameters(gridOptions, true, true, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossByProjectsAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_CompareProjects_FinalizedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseFinalizedVouchers()
                .Build();
            var parameters = GetByProjectsTestParameters(gridOptions, true, true, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossByProjectsAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_CompareProjects_ConfirmedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseConfirmedVouchers()
                .Build();
            var parameters = GetByProjectsTestParameters(gridOptions, true, true, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossByProjectsAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_CompareProjects_ApprovedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseApprovedVouchers()
                .Build();
            var parameters = GetByProjectsTestParameters(gridOptions, true, true, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossByProjectsAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_CompareProjects_AllVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .Build();
            var parameters = GetByProjectsTestParameters(gridOptions, true, true, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossByProjectsAsync(parameters, balanceItems);
        }

        #endregion

        #region GetProfitLoss_CompareCostCenters_CurrentBranch_NoOptions

        private async Task<ProfitLossViewModel> GetProfitLoss_CompareCostCenters_CheckedVouchers_CurrentBranch_NoOptions(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseCheckedVouchers()
                .Build();
            var parameters = GetByCostCentersTestParameters(gridOptions, false, false, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossByCostCentersAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_CompareCostCenters_FinalizedVouchers_CurrentBranch_NoOptions(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseFinalizedVouchers()
                .Build();
            var parameters = GetByCostCentersTestParameters(gridOptions, false, false, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossByCostCentersAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_CompareCostCenters_ConfirmedVouchers_CurrentBranch_NoOptions(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseConfirmedVouchers()
                .Build();
            var parameters = GetByCostCentersTestParameters(gridOptions, false, false, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossByCostCentersAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_CompareCostCenters_ApprovedVouchers_CurrentBranch_NoOptions(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseApprovedVouchers()
                .Build();
            var parameters = GetByCostCentersTestParameters(gridOptions, false, false, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossByCostCentersAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_CompareCostCenters_AllVouchers_CurrentBranch_NoOptions(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .Build();
            var parameters = GetByCostCentersTestParameters(gridOptions, false, false, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossByCostCentersAsync(parameters, balanceItems);
        }

        #endregion

        #region GetProfitLoss_CompareCostCenters_CurrentAndChildren_NoOptions

        private async Task<ProfitLossViewModel> GetProfitLoss_CompareCostCenters_CheckedVouchers_CurrentAndChildren_NoOptions(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseCheckedVouchers()
                .Build();
            var parameters = GetByCostCentersTestParameters(gridOptions, false, false, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossByCostCentersAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_CompareCostCenters_FinalizedVouchers_CurrentAndChildren_NoOptions(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseFinalizedVouchers()
                .Build();
            var parameters = GetByCostCentersTestParameters(gridOptions, false, false, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossByCostCentersAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_CompareCostCenters_ConfirmedVouchers_CurrentAndChildren_NoOptions(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseConfirmedVouchers()
                .Build();
            var parameters = GetByCostCentersTestParameters(gridOptions, false, false, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossByCostCentersAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_CompareCostCenters_ApprovedVouchers_CurrentAndChildren_NoOptions(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseApprovedVouchers()
                .Build();
            var parameters = GetByCostCentersTestParameters(gridOptions, false, false, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossByCostCentersAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_CompareCostCenters_AllVouchers_CurrentAndChildren_NoOptions(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .Build();
            var parameters = GetByCostCentersTestParameters(gridOptions, false, false, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossByCostCentersAsync(parameters, balanceItems);
        }

        #endregion

        #region GetProfitLoss_CompareCostCenters_CurrentBranch_ClosingOption

        private async Task<ProfitLossViewModel> GetProfitLoss_CompareCostCenters_CheckedVouchers_CurrentBranch_ClosingOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseCheckedVouchers()
                .Build();
            var parameters = GetByCostCentersTestParameters(gridOptions, false, true, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossByCostCentersAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_CompareCostCenters_FinalizedVouchers_CurrentBranch_ClosingOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseFinalizedVouchers()
                .Build();
            var parameters = GetByCostCentersTestParameters(gridOptions, false, true, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossByCostCentersAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_CompareCostCenters_ConfirmedVouchers_CurrentBranch_ClosingOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseConfirmedVouchers()
                .Build();
            var parameters = GetByCostCentersTestParameters(gridOptions, false, true, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossByCostCentersAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_CompareCostCenters_ApprovedVouchers_CurrentBranch_ClosingOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseApprovedVouchers()
                .Build();
            var parameters = GetByCostCentersTestParameters(gridOptions, false, true, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossByCostCentersAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_CompareCostCenters_AllVouchers_CurrentBranch_ClosingOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .Build();
            var parameters = GetByCostCentersTestParameters(gridOptions, false, true, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossByCostCentersAsync(parameters, balanceItems);
        }

        #endregion

        #region GetProfitLoss_CompareCostCenters_CurrentAndChildren_ClosingOption

        private async Task<ProfitLossViewModel> GetProfitLoss_CompareCostCenters_CheckedVouchers_CurrentAndChildren_ClosingOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseCheckedVouchers()
                .Build();
            var parameters = GetByCostCentersTestParameters(gridOptions, false, true, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossByCostCentersAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_CompareCostCenters_FinalizedVouchers_CurrentAndChildren_ClosingOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseFinalizedVouchers()
                .Build();
            var parameters = GetByCostCentersTestParameters(gridOptions, false, true, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossByCostCentersAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_CompareCostCenters_ConfirmedVouchers_CurrentAndChildren_ClosingOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseConfirmedVouchers()
                .Build();
            var parameters = GetByCostCentersTestParameters(gridOptions, false, true, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossByCostCentersAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_CompareCostCenters_ApprovedVouchers_CurrentAndChildren_ClosingOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseApprovedVouchers()
                .Build();
            var parameters = GetByCostCentersTestParameters(gridOptions, false, true, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossByCostCentersAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_CompareCostCenters_AllVouchers_CurrentAndChildren_ClosingOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .Build();
            var parameters = GetByCostCentersTestParameters(gridOptions, false, true, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossByCostCentersAsync(parameters, balanceItems);
        }

        #endregion

        #region GetProfitLoss_CompareCostCenters_CurrentBranch_TurnoverOption

        private async Task<ProfitLossViewModel> GetProfitLoss_CompareCostCenters_CheckedVouchers_CurrentBranch_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseCheckedVouchers()
                .Build();
            var parameters = GetByCostCentersTestParameters(gridOptions, true, false, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossByCostCentersAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_CompareCostCenters_FinalizedVouchers_CurrentBranch_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseFinalizedVouchers()
                .Build();
            var parameters = GetByCostCentersTestParameters(gridOptions, true, false, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossByCostCentersAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_CompareCostCenters_ConfirmedVouchers_CurrentBranch_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseConfirmedVouchers()
                .Build();
            var parameters = GetByCostCentersTestParameters(gridOptions, true, false, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossByCostCentersAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_CompareCostCenters_ApprovedVouchers_CurrentBranch_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseApprovedVouchers()
                .Build();
            var parameters = GetByCostCentersTestParameters(gridOptions, true, false, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossByCostCentersAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_CompareCostCenters_AllVouchers_CurrentBranch_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .Build();
            var parameters = GetByCostCentersTestParameters(gridOptions, true, false, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossByCostCentersAsync(parameters, balanceItems);
        }

        #endregion

        #region GetProfitLoss_CompareCostCenters_CurrentAndChildren_TurnoverOption

        private async Task<ProfitLossViewModel> GetProfitLoss_CompareCostCenters_CheckedVouchers_CurrentAndChildren_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseCheckedVouchers()
                .Build();
            var parameters = GetByCostCentersTestParameters(gridOptions, true, false, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossByCostCentersAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_CompareCostCenters_FinalizedVouchers_CurrentAndChildren_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseFinalizedVouchers()
                .Build();
            var parameters = GetByCostCentersTestParameters(gridOptions, true, false, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossByCostCentersAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_CompareCostCenters_ConfirmedVouchers_CurrentAndChildren_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseConfirmedVouchers()
                .Build();
            var parameters = GetByCostCentersTestParameters(gridOptions, true, false, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossByCostCentersAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_CompareCostCenters_ApprovedVouchers_CurrentAndChildren_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseApprovedVouchers()
                .Build();
            var parameters = GetByCostCentersTestParameters(gridOptions, true, false, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossByCostCentersAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_CompareCostCenters_AllVouchers_CurrentAndChildren_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .Build();
            var parameters = GetByCostCentersTestParameters(gridOptions, true, false, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossByCostCentersAsync(parameters, balanceItems);
        }

        #endregion

        #region GetProfitLoss_CompareCostCenters_CurrentBranch_ClosingOption_TurnoverOption

        private async Task<ProfitLossViewModel> GetProfitLoss_CompareCostCenters_CheckedVouchers_CurrentBranch_ClosingOption_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseCheckedVouchers()
                .Build();
            var parameters = GetByCostCentersTestParameters(gridOptions, true, true, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossByCostCentersAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_CompareCostCenters_FinalizedVouchers_CurrentBranch_ClosingOption_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseFinalizedVouchers()
                .Build();
            var parameters = GetByCostCentersTestParameters(gridOptions, true, true, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossByCostCentersAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_CompareCostCenters_ConfirmedVouchers_CurrentBranch_ClosingOption_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseConfirmedVouchers()
                .Build();
            var parameters = GetByCostCentersTestParameters(gridOptions, true, true, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossByCostCentersAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_CompareCostCenters_ApprovedVouchers_CurrentBranch_ClosingOption_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseApprovedVouchers()
                .Build();
            var parameters = GetByCostCentersTestParameters(gridOptions, true, true, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossByCostCentersAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_CompareCostCenters_AllVouchers_CurrentBranch_ClosingOption_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .Build();
            var parameters = GetByCostCentersTestParameters(gridOptions, true, true, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossByCostCentersAsync(parameters, balanceItems);
        }

        #endregion

        #region GetProfitLoss_CompareCostCenters_CurrentAndChildren_ClosingOption_TurnoverOption

        private async Task<ProfitLossViewModel> GetProfitLoss_CompareCostCenters_CheckedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseCheckedVouchers()
                .Build();
            var parameters = GetByCostCentersTestParameters(gridOptions, true, true, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossByCostCentersAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_CompareCostCenters_FinalizedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseFinalizedVouchers()
                .Build();
            var parameters = GetByCostCentersTestParameters(gridOptions, true, true, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossByCostCentersAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_CompareCostCenters_ConfirmedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseConfirmedVouchers()
                .Build();
            var parameters = GetByCostCentersTestParameters(gridOptions, true, true, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossByCostCentersAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_CompareCostCenters_ApprovedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseApprovedVouchers()
                .Build();
            var parameters = GetByCostCentersTestParameters(gridOptions, true, true, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossByCostCentersAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_CompareCostCenters_AllVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .Build();
            var parameters = GetByCostCentersTestParameters(gridOptions, true, true, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossByCostCentersAsync(parameters, balanceItems);
        }

        #endregion

        #region GetProfitLoss_CompareFiscalPeriods_CurrentBranch_NoOptions

        private async Task<ProfitLossViewModel> GetProfitLoss_CompareFiscalPeriods_CheckedVouchers_CurrentBranch_NoOptions(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseCheckedVouchers()
                .Build();
            var parameters = GetByFiscalPeriodsTestParameters(gridOptions, false, false, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossByFiscalPeriodsAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_CompareFiscalPeriods_FinalizedVouchers_CurrentBranch_NoOptions(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseFinalizedVouchers()
                .Build();
            var parameters = GetByFiscalPeriodsTestParameters(gridOptions, false, false, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossByFiscalPeriodsAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_CompareFiscalPeriods_ConfirmedVouchers_CurrentBranch_NoOptions(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseConfirmedVouchers()
                .Build();
            var parameters = GetByFiscalPeriodsTestParameters(gridOptions, false, false, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossByFiscalPeriodsAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_CompareFiscalPeriods_ApprovedVouchers_CurrentBranch_NoOptions(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseApprovedVouchers()
                .Build();
            var parameters = GetByFiscalPeriodsTestParameters(gridOptions, false, false, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossByFiscalPeriodsAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_CompareFiscalPeriods_AllVouchers_CurrentBranch_NoOptions(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .Build();
            var parameters = GetByFiscalPeriodsTestParameters(gridOptions, false, false, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossByFiscalPeriodsAsync(parameters, balanceItems);
        }

        #endregion

        #region GetProfitLoss_CompareFiscalPeriods_CurrentAndChildren_NoOptions

        private async Task<ProfitLossViewModel> GetProfitLoss_CompareFiscalPeriods_CheckedVouchers_CurrentAndChildren_NoOptions(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseCheckedVouchers()
                .Build();
            var parameters = GetByFiscalPeriodsTestParameters(gridOptions, false, false, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossByFiscalPeriodsAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_CompareFiscalPeriods_FinalizedVouchers_CurrentAndChildren_NoOptions(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseFinalizedVouchers()
                .Build();
            var parameters = GetByFiscalPeriodsTestParameters(gridOptions, false, false, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossByFiscalPeriodsAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_CompareFiscalPeriods_ConfirmedVouchers_CurrentAndChildren_NoOptions(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseConfirmedVouchers()
                .Build();
            var parameters = GetByFiscalPeriodsTestParameters(gridOptions, false, false, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossByFiscalPeriodsAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_CompareFiscalPeriods_ApprovedVouchers_CurrentAndChildren_NoOptions(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseApprovedVouchers()
                .Build();
            var parameters = GetByFiscalPeriodsTestParameters(gridOptions, false, false, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossByFiscalPeriodsAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_CompareFiscalPeriods_AllVouchers_CurrentAndChildren_NoOptions(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .Build();
            var parameters = GetByFiscalPeriodsTestParameters(gridOptions, false, false, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossByFiscalPeriodsAsync(parameters, balanceItems);
        }

        #endregion

        #region GetProfitLoss_CompareFiscalPeriods_CurrentBranch_ClosingOption

        private async Task<ProfitLossViewModel> GetProfitLoss_CompareFiscalPeriods_CheckedVouchers_CurrentBranch_ClosingOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseCheckedVouchers()
                .Build();
            var parameters = GetByFiscalPeriodsTestParameters(gridOptions, false, true, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossByFiscalPeriodsAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_CompareFiscalPeriods_FinalizedVouchers_CurrentBranch_ClosingOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseFinalizedVouchers()
                .Build();
            var parameters = GetByFiscalPeriodsTestParameters(gridOptions, false, true, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossByFiscalPeriodsAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_CompareFiscalPeriods_ConfirmedVouchers_CurrentBranch_ClosingOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseConfirmedVouchers()
                .Build();
            var parameters = GetByFiscalPeriodsTestParameters(gridOptions, false, true, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossByFiscalPeriodsAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_CompareFiscalPeriods_ApprovedVouchers_CurrentBranch_ClosingOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseApprovedVouchers()
                .Build();
            var parameters = GetByFiscalPeriodsTestParameters(gridOptions, false, true, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossByFiscalPeriodsAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_CompareFiscalPeriods_AllVouchers_CurrentBranch_ClosingOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .Build();
            var parameters = GetByFiscalPeriodsTestParameters(gridOptions, false, true, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossByFiscalPeriodsAsync(parameters, balanceItems);
        }

        #endregion

        #region GetProfitLoss_CompareFiscalPeriods_CurrentAndChildren_ClosingOption

        private async Task<ProfitLossViewModel> GetProfitLoss_CompareFiscalPeriods_CheckedVouchers_CurrentAndChildren_ClosingOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseCheckedVouchers()
                .Build();
            var parameters = GetByFiscalPeriodsTestParameters(gridOptions, false, true, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossByFiscalPeriodsAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_CompareFiscalPeriods_FinalizedVouchers_CurrentAndChildren_ClosingOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseFinalizedVouchers()
                .Build();
            var parameters = GetByFiscalPeriodsTestParameters(gridOptions, false, true, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossByFiscalPeriodsAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_CompareFiscalPeriods_ConfirmedVouchers_CurrentAndChildren_ClosingOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseConfirmedVouchers()
                .Build();
            var parameters = GetByFiscalPeriodsTestParameters(gridOptions, false, true, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossByFiscalPeriodsAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_CompareFiscalPeriods_ApprovedVouchers_CurrentAndChildren_ClosingOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseApprovedVouchers()
                .Build();
            var parameters = GetByFiscalPeriodsTestParameters(gridOptions, false, true, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossByFiscalPeriodsAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_CompareFiscalPeriods_AllVouchers_CurrentAndChildren_ClosingOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .Build();
            var parameters = GetByFiscalPeriodsTestParameters(gridOptions, false, true, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossByFiscalPeriodsAsync(parameters, balanceItems);
        }

        #endregion

        #region GetProfitLoss_CompareFiscalPeriods_CurrentBranch_TurnoverOption

        private async Task<ProfitLossViewModel> GetProfitLoss_CompareFiscalPeriods_CheckedVouchers_CurrentBranch_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseCheckedVouchers()
                .Build();
            var parameters = GetByFiscalPeriodsTestParameters(gridOptions, true, false, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossByFiscalPeriodsAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_CompareFiscalPeriods_FinalizedVouchers_CurrentBranch_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseFinalizedVouchers()
                .Build();
            var parameters = GetByFiscalPeriodsTestParameters(gridOptions, true, false, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossByFiscalPeriodsAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_CompareFiscalPeriods_ConfirmedVouchers_CurrentBranch_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseConfirmedVouchers()
                .Build();
            var parameters = GetByFiscalPeriodsTestParameters(gridOptions, true, false, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossByFiscalPeriodsAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_CompareFiscalPeriods_ApprovedVouchers_CurrentBranch_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseApprovedVouchers()
                .Build();
            var parameters = GetByFiscalPeriodsTestParameters(gridOptions, true, false, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossByFiscalPeriodsAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_CompareFiscalPeriods_AllVouchers_CurrentBranch_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .Build();
            var parameters = GetByFiscalPeriodsTestParameters(gridOptions, true, false, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossByFiscalPeriodsAsync(parameters, balanceItems);
        }

        #endregion

        #region GetProfitLoss_CompareFiscalPeriods_CurrentAndChildren_TurnoverOption

        private async Task<ProfitLossViewModel> GetProfitLoss_CompareFiscalPeriods_CheckedVouchers_CurrentAndChildren_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseCheckedVouchers()
                .Build();
            var parameters = GetByFiscalPeriodsTestParameters(gridOptions, true, false, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossByFiscalPeriodsAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_CompareFiscalPeriods_FinalizedVouchers_CurrentAndChildren_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseFinalizedVouchers()
                .Build();
            var parameters = GetByFiscalPeriodsTestParameters(gridOptions, true, false, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossByFiscalPeriodsAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_CompareFiscalPeriods_ConfirmedVouchers_CurrentAndChildren_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseConfirmedVouchers()
                .Build();
            var parameters = GetByFiscalPeriodsTestParameters(gridOptions, true, false, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossByFiscalPeriodsAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_CompareFiscalPeriods_ApprovedVouchers_CurrentAndChildren_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseApprovedVouchers()
                .Build();
            var parameters = GetByFiscalPeriodsTestParameters(gridOptions, true, false, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossByFiscalPeriodsAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_CompareFiscalPeriods_AllVouchers_CurrentAndChildren_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .Build();
            var parameters = GetByFiscalPeriodsTestParameters(gridOptions, true, false, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossByFiscalPeriodsAsync(parameters, balanceItems);
        }

        #endregion

        #region GetProfitLoss_CompareFiscalPeriods_CurrentBranch_ClosingOption_TurnoverOption

        private async Task<ProfitLossViewModel> GetProfitLoss_CompareFiscalPeriods_CheckedVouchers_CurrentBranch_ClosingOption_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseCheckedVouchers()
                .Build();
            var parameters = GetByFiscalPeriodsTestParameters(gridOptions, true, true, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossByFiscalPeriodsAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_CompareFiscalPeriods_FinalizedVouchers_CurrentBranch_ClosingOption_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseFinalizedVouchers()
                .Build();
            var parameters = GetByFiscalPeriodsTestParameters(gridOptions, true, true, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossByFiscalPeriodsAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_CompareFiscalPeriods_ConfirmedVouchers_CurrentBranch_ClosingOption_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseConfirmedVouchers()
                .Build();
            var parameters = GetByFiscalPeriodsTestParameters(gridOptions, true, true, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossByFiscalPeriodsAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_CompareFiscalPeriods_ApprovedVouchers_CurrentBranch_ClosingOption_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseApprovedVouchers()
                .Build();
            var parameters = GetByFiscalPeriodsTestParameters(gridOptions, true, true, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossByFiscalPeriodsAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_CompareFiscalPeriods_AllVouchers_CurrentBranch_ClosingOption_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .Build();
            var parameters = GetByFiscalPeriodsTestParameters(gridOptions, true, true, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossByFiscalPeriodsAsync(parameters, balanceItems);
        }

        #endregion

        #region GetProfitLoss_CompareFiscalPeriods_CurrentAndChildren_ClosingOption_TurnoverOption

        private async Task<ProfitLossViewModel> GetProfitLoss_CompareFiscalPeriods_CheckedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseCheckedVouchers()
                .Build();
            var parameters = GetByFiscalPeriodsTestParameters(gridOptions, true, true, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossByFiscalPeriodsAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_CompareFiscalPeriods_FinalizedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseFinalizedVouchers()
                .Build();
            var parameters = GetByFiscalPeriodsTestParameters(gridOptions, true, true, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossByFiscalPeriodsAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_CompareFiscalPeriods_ConfirmedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseConfirmedVouchers()
                .Build();
            var parameters = GetByFiscalPeriodsTestParameters(gridOptions, true, true, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossByFiscalPeriodsAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_CompareFiscalPeriods_ApprovedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseApprovedVouchers()
                .Build();
            var parameters = GetByFiscalPeriodsTestParameters(gridOptions, true, true, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossByFiscalPeriodsAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_CompareFiscalPeriods_AllVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .Build();
            var parameters = GetByFiscalPeriodsTestParameters(gridOptions, true, true, ccenterId, projectId);
            var balanceItems = new List<StartEndBalanceViewModel>();
            return await _repository.GetProfitLossByFiscalPeriodsAsync(parameters, balanceItems);
        }

        #endregion

        #region GetProfitLoss_Periodic_CurrentBranch_NoOptions

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_CheckedVouchers_CurrentBranch_NoOptions(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseCheckedVouchers()
                .Build();
            var parameters = GetTestParameters(gridOptions, false, false, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_FinalizedVouchers_CurrentBranch_NoOptions(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseFinalizedVouchers()
                .Build();
            var parameters = GetTestParameters(gridOptions, false, false, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_ConfirmedVouchers_CurrentBranch_NoOptions(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseConfirmedVouchers()
                .Build();
            var parameters = GetTestParameters(gridOptions, false, false, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_ApprovedVouchers_CurrentBranch_NoOptions(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseApprovedVouchers()
                .Build();
            var parameters = GetTestParameters(gridOptions, false, false, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_AllVouchers_CurrentBranch_NoOptions(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .Build();
            var parameters = GetTestParameters(gridOptions, false, false, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossAsync(parameters, balanceItems);
        }

        #endregion

        #region GetProfitLoss_Periodic_CurrentAndChildren_NoOptions

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_CheckedVouchers_CurrentAndChildren_NoOptions(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseCheckedVouchers()
                .Build();
            var parameters = GetTestParameters(gridOptions, false, false, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_FinalizedVouchers_CurrentAndChildren_NoOptions(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseFinalizedVouchers()
                .Build();
            var parameters = GetTestParameters(gridOptions, false, false, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_ConfirmedVouchers_CurrentAndChildren_NoOptions(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseConfirmedVouchers()
                .Build();
            var parameters = GetTestParameters(gridOptions, false, false, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_ApprovedVouchers_CurrentAndChildren_NoOptions(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseApprovedVouchers()
                .Build();
            var parameters = GetTestParameters(gridOptions, false, false, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_AllVouchers_CurrentAndChildren_NoOptions(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .Build();
            var parameters = GetTestParameters(gridOptions, false, false, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossAsync(parameters, balanceItems);
        }

        #endregion

        #region GetProfitLoss_Periodic_CurrentBranch_ClosingOption

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_CheckedVouchers_CurrentBranch_ClosingOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseCheckedVouchers()
                .Build();
            var parameters = GetTestParameters(gridOptions, false, true, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_FinalizedVouchers_CurrentBranch_ClosingOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseFinalizedVouchers()
                .Build();
            var parameters = GetTestParameters(gridOptions, false, true, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_ConfirmedVouchers_CurrentBranch_ClosingOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseConfirmedVouchers()
                .Build();
            var parameters = GetTestParameters(gridOptions, false, true, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_ApprovedVouchers_CurrentBranch_ClosingOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseApprovedVouchers()
                .Build();
            var parameters = GetTestParameters(gridOptions, false, true, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_AllVouchers_CurrentBranch_ClosingOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .Build();
            var parameters = GetTestParameters(gridOptions, false, true, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossAsync(parameters, balanceItems);
        }

        #endregion

        #region GetProfitLoss_Periodic_CurrentAndChildren_ClosingOption

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_CheckedVouchers_CurrentAndChildren_ClosingOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseCheckedVouchers()
                .Build();
            var parameters = GetTestParameters(gridOptions, false, true, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_FinalizedVouchers_CurrentAndChildren_ClosingOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseFinalizedVouchers()
                .Build();
            var parameters = GetTestParameters(gridOptions, false, true, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_ConfirmedVouchers_CurrentAndChildren_ClosingOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseConfirmedVouchers()
                .Build();
            var parameters = GetTestParameters(gridOptions, false, true, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_ApprovedVouchers_CurrentAndChildren_ClosingOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseApprovedVouchers()
                .Build();
            var parameters = GetTestParameters(gridOptions, false, true, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_AllVouchers_CurrentAndChildren_ClosingOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .Build();
            var parameters = GetTestParameters(gridOptions, false, true, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossAsync(parameters, balanceItems);
        }

        #endregion

        #region GetProfitLoss_Periodic_CurrentBranch_TurnoverOption

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_CheckedVouchers_CurrentBranch_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseCheckedVouchers()
                .Build();
            var parameters = GetTestParameters(gridOptions, true, false, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_FinalizedVouchers_CurrentBranch_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseFinalizedVouchers()
                .Build();
            var parameters = GetTestParameters(gridOptions, true, false, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_ConfirmedVouchers_CurrentBranch_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseConfirmedVouchers()
                .Build();
            var parameters = GetTestParameters(gridOptions, true, false, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_ApprovedVouchers_CurrentBranch_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseApprovedVouchers()
                .Build();
            var parameters = GetTestParameters(gridOptions, true, false, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_AllVouchers_CurrentBranch_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .Build();
            var parameters = GetTestParameters(gridOptions, true, false, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossAsync(parameters, balanceItems);
        }

        #endregion

        #region GetProfitLoss_Periodic_CurrentAndChildren_TurnoverOption

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_CheckedVouchers_CurrentAndChildren_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseCheckedVouchers()
                .Build();
            var parameters = GetTestParameters(gridOptions, true, false, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_FinalizedVouchers_CurrentAndChildren_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseFinalizedVouchers()
                .Build();
            var parameters = GetTestParameters(gridOptions, true, false, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_ConfirmedVouchers_CurrentAndChildren_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseConfirmedVouchers()
                .Build();
            var parameters = GetTestParameters(gridOptions, true, false, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_ApprovedVouchers_CurrentAndChildren_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseApprovedVouchers()
                .Build();
            var parameters = GetTestParameters(gridOptions, true, false, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_AllVouchers_CurrentAndChildren_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .Build();
            var parameters = GetTestParameters(gridOptions, true, false, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossAsync(parameters, balanceItems);
        }

        #endregion

        #region GetProfitLoss_Periodic_CurrentBranch_ClosingOption_TurnoverOption

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_CheckedVouchers_CurrentBranch_ClosingOption_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseCheckedVouchers()
                .Build();
            var parameters = GetTestParameters(gridOptions, true, true, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_FinalizedVouchers_CurrentBranch_ClosingOption_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseFinalizedVouchers()
                .Build();
            var parameters = GetTestParameters(gridOptions, true, true, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_ConfirmedVouchers_CurrentBranch_ClosingOption_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseConfirmedVouchers()
                .Build();
            var parameters = GetTestParameters(gridOptions, true, true, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_ApprovedVouchers_CurrentBranch_ClosingOption_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseApprovedVouchers()
                .Build();
            var parameters = GetTestParameters(gridOptions, true, true, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_AllVouchers_CurrentBranch_ClosingOption_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .Build();
            var parameters = GetTestParameters(gridOptions, true, true, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossAsync(parameters, balanceItems);
        }

        #endregion

        #region GetProfitLoss_Periodic_CurrentAndChildren_ClosingOption_TurnoverOption

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_CheckedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseCheckedVouchers()
                .Build();
            var parameters = GetTestParameters(gridOptions, true, true, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_FinalizedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseFinalizedVouchers()
                .Build();
            var parameters = GetTestParameters(gridOptions, true, true, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_ConfirmedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseConfirmedVouchers()
                .Build();
            var parameters = GetTestParameters(gridOptions, true, true, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_ApprovedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseApprovedVouchers()
                .Build();
            var parameters = GetTestParameters(gridOptions, true, true, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_AllVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .Build();
            var parameters = GetTestParameters(gridOptions, true, true, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossAsync(parameters, balanceItems);
        }

        #endregion

        #region GetProfitLoss_Periodic_CompareBranches_CurrentBranch_NoOptions

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_CompareBranches_CheckedVouchers_CurrentBranch_NoOptions(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseCheckedVouchers()
                .Build();
            var parameters = GetByBranchesTestParameters(gridOptions, false, false, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossByBranchesAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_CompareBranches_FinalizedVouchers_CurrentBranch_NoOptions(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseFinalizedVouchers()
                .Build();
            var parameters = GetByBranchesTestParameters(gridOptions, false, false, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossByBranchesAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_CompareBranches_ConfirmedVouchers_CurrentBranch_NoOptions(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseConfirmedVouchers()
                .Build();
            var parameters = GetByBranchesTestParameters(gridOptions, false, false, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossByBranchesAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_CompareBranches_ApprovedVouchers_CurrentBranch_NoOptions(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseApprovedVouchers()
                .Build();
            var parameters = GetByBranchesTestParameters(gridOptions, false, false, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossByBranchesAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_CompareBranches_AllVouchers_CurrentBranch_NoOptions(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .Build();
            var parameters = GetByBranchesTestParameters(gridOptions, false, false, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossByBranchesAsync(parameters, balanceItems);
        }

        #endregion

        #region GetProfitLoss_Periodic_CompareBranches_CurrentAndChildren_NoOptions

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_CompareBranches_CheckedVouchers_CurrentAndChildren_NoOptions(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseCheckedVouchers()
                .Build();
            var parameters = GetByBranchesTestParameters(gridOptions, false, false, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossByBranchesAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_CompareBranches_FinalizedVouchers_CurrentAndChildren_NoOptions(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseFinalizedVouchers()
                .Build();
            var parameters = GetByBranchesTestParameters(gridOptions, false, false, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossByBranchesAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_CompareBranches_ConfirmedVouchers_CurrentAndChildren_NoOptions(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseConfirmedVouchers()
                .Build();
            var parameters = GetByBranchesTestParameters(gridOptions, false, false, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossByBranchesAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_CompareBranches_ApprovedVouchers_CurrentAndChildren_NoOptions(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseApprovedVouchers()
                .Build();
            var parameters = GetByBranchesTestParameters(gridOptions, false, false, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossByBranchesAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_CompareBranches_AllVouchers_CurrentAndChildren_NoOptions(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .Build();
            var parameters = GetByBranchesTestParameters(gridOptions, false, false, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossByBranchesAsync(parameters, balanceItems);
        }

        #endregion

        #region GetProfitLoss_Periodic_CompareBranches_CurrentBranch_ClosingOption

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_CompareBranches_CheckedVouchers_CurrentBranch_ClosingOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseCheckedVouchers()
                .Build();
            var parameters = GetByBranchesTestParameters(gridOptions, false, true, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossByBranchesAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_CompareBranches_FinalizedVouchers_CurrentBranch_ClosingOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseFinalizedVouchers()
                .Build();
            var parameters = GetByBranchesTestParameters(gridOptions, false, true, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossByBranchesAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_CompareBranches_ConfirmedVouchers_CurrentBranch_ClosingOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseConfirmedVouchers()
                .Build();
            var parameters = GetByBranchesTestParameters(gridOptions, false, true, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossByBranchesAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_CompareBranches_ApprovedVouchers_CurrentBranch_ClosingOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseApprovedVouchers()
                .Build();
            var parameters = GetByBranchesTestParameters(gridOptions, false, true, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossByBranchesAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_CompareBranches_AllVouchers_CurrentBranch_ClosingOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .Build();
            var parameters = GetByBranchesTestParameters(gridOptions, false, true, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossByBranchesAsync(parameters, balanceItems);
        }

        #endregion

        #region GetProfitLoss_Periodic_CompareBranches_CurrentAndChildren_ClosingOption

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_CompareBranches_CheckedVouchers_CurrentAndChildren_ClosingOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseCheckedVouchers()
                .Build();
            var parameters = GetByBranchesTestParameters(gridOptions, false, true, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossByBranchesAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_CompareBranches_FinalizedVouchers_CurrentAndChildren_ClosingOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseFinalizedVouchers()
                .Build();
            var parameters = GetByBranchesTestParameters(gridOptions, false, true, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossByBranchesAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_CompareBranches_ConfirmedVouchers_CurrentAndChildren_ClosingOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseConfirmedVouchers()
                .Build();
            var parameters = GetByBranchesTestParameters(gridOptions, false, true, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossByBranchesAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_CompareBranches_ApprovedVouchers_CurrentAndChildren_ClosingOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseApprovedVouchers()
                .Build();
            var parameters = GetByBranchesTestParameters(gridOptions, false, true, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossByBranchesAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_CompareBranches_AllVouchers_CurrentAndChildren_ClosingOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .Build();
            var parameters = GetByBranchesTestParameters(gridOptions, false, true, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossByBranchesAsync(parameters, balanceItems);
        }

        #endregion

        #region GetProfitLoss_Periodic_CompareBranches_CurrentBranch_TurnoverOption

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_CompareBranches_CheckedVouchers_CurrentBranch_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseCheckedVouchers()
                .Build();
            var parameters = GetByBranchesTestParameters(gridOptions, true, false, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossByBranchesAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_CompareBranches_FinalizedVouchers_CurrentBranch_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseFinalizedVouchers()
                .Build();
            var parameters = GetByBranchesTestParameters(gridOptions, true, false, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossByBranchesAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_CompareBranches_ConfirmedVouchers_CurrentBranch_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseConfirmedVouchers()
                .Build();
            var parameters = GetByBranchesTestParameters(gridOptions, true, false, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossByBranchesAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_CompareBranches_ApprovedVouchers_CurrentBranch_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseApprovedVouchers()
                .Build();
            var parameters = GetByBranchesTestParameters(gridOptions, true, false, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossByBranchesAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_CompareBranches_AllVouchers_CurrentBranch_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .Build();
            var parameters = GetByBranchesTestParameters(gridOptions, true, false, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossByBranchesAsync(parameters, balanceItems);
        }

        #endregion

        #region GetProfitLoss_Periodic_CompareBranches_CurrentAndChildren_TurnoverOption

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_CompareBranches_CheckedVouchers_CurrentAndChildren_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseCheckedVouchers()
                .Build();
            var parameters = GetByBranchesTestParameters(gridOptions, true, false, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossByBranchesAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_CompareBranches_FinalizedVouchers_CurrentAndChildren_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseFinalizedVouchers()
                .Build();
            var parameters = GetByBranchesTestParameters(gridOptions, true, false, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossByBranchesAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_CompareBranches_ConfirmedVouchers_CurrentAndChildren_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseConfirmedVouchers()
                .Build();
            var parameters = GetByBranchesTestParameters(gridOptions, true, false, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossByBranchesAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_CompareBranches_ApprovedVouchers_CurrentAndChildren_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseApprovedVouchers()
                .Build();
            var parameters = GetByBranchesTestParameters(gridOptions, true, false, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossByBranchesAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_CompareBranches_AllVouchers_CurrentAndChildren_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .Build();
            var parameters = GetByBranchesTestParameters(gridOptions, true, false, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossByBranchesAsync(parameters, balanceItems);
        }

        #endregion

        #region GetProfitLoss_Periodic_CompareBranches_CurrentBranch_ClosingOption_TurnoverOption

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_CompareBranches_CheckedVouchers_CurrentBranch_ClosingOption_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseCheckedVouchers()
                .Build();
            var parameters = GetByBranchesTestParameters(gridOptions, true, true, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossByBranchesAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_CompareBranches_FinalizedVouchers_CurrentBranch_ClosingOption_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseFinalizedVouchers()
                .Build();
            var parameters = GetByBranchesTestParameters(gridOptions, true, true, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossByBranchesAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_CompareBranches_ConfirmedVouchers_CurrentBranch_ClosingOption_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseConfirmedVouchers()
                .Build();
            var parameters = GetByBranchesTestParameters(gridOptions, true, true, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossByBranchesAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_CompareBranches_ApprovedVouchers_CurrentBranch_ClosingOption_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseApprovedVouchers()
                .Build();
            var parameters = GetByBranchesTestParameters(gridOptions, true, true, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossByBranchesAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_CompareBranches_AllVouchers_CurrentBranch_ClosingOption_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .Build();
            var parameters = GetByBranchesTestParameters(gridOptions, true, true, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossByBranchesAsync(parameters, balanceItems);
        }

        #endregion

        #region GetProfitLoss_Periodic_CompareBranches_CurrentAndChildren_ClosingOption_TurnoverOption

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_CompareBranches_CheckedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseCheckedVouchers()
                .Build();
            var parameters = GetByBranchesTestParameters(gridOptions, true, true, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossByBranchesAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_CompareBranches_FinalizedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseFinalizedVouchers()
                .Build();
            var parameters = GetByBranchesTestParameters(gridOptions, true, true, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossByBranchesAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_CompareBranches_ConfirmedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseConfirmedVouchers()
                .Build();
            var parameters = GetByBranchesTestParameters(gridOptions, true, true, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossByBranchesAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_CompareBranches_ApprovedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseApprovedVouchers()
                .Build();
            var parameters = GetByBranchesTestParameters(gridOptions, true, true, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossByBranchesAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_CompareBranches_AllVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .Build();
            var parameters = GetByBranchesTestParameters(gridOptions, true, true, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossByBranchesAsync(parameters, balanceItems);
        }

        #endregion

        #region GetProfitLoss_Periodic_CompareProjects_CurrentBranch_NoOptions

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_CompareProjects_CheckedVouchers_CurrentBranch_NoOptions(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseCheckedVouchers()
                .Build();
            var parameters = GetByProjectsTestParameters(gridOptions, false, false, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossByProjectsAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_CompareProjects_FinalizedVouchers_CurrentBranch_NoOptions(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseFinalizedVouchers()
                .Build();
            var parameters = GetByProjectsTestParameters(gridOptions, false, false, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossByProjectsAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_CompareProjects_ConfirmedVouchers_CurrentBranch_NoOptions(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseConfirmedVouchers()
                .Build();
            var parameters = GetByProjectsTestParameters(gridOptions, false, false, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossByProjectsAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_CompareProjects_ApprovedVouchers_CurrentBranch_NoOptions(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseApprovedVouchers()
                .Build();
            var parameters = GetByProjectsTestParameters(gridOptions, false, false, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossByProjectsAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_CompareProjects_AllVouchers_CurrentBranch_NoOptions(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .Build();
            var parameters = GetByProjectsTestParameters(gridOptions, false, false, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossByProjectsAsync(parameters, balanceItems);
        }

        #endregion

        #region GetProfitLoss_Periodic_CompareProjects_CurrentAndChildren_NoOptions

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_CompareProjects_CheckedVouchers_CurrentAndChildren_NoOptions(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseCheckedVouchers()
                .Build();
            var parameters = GetByProjectsTestParameters(gridOptions, false, false, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossByProjectsAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_CompareProjects_FinalizedVouchers_CurrentAndChildren_NoOptions(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseFinalizedVouchers()
                .Build();
            var parameters = GetByProjectsTestParameters(gridOptions, false, false, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossByProjectsAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_CompareProjects_ConfirmedVouchers_CurrentAndChildren_NoOptions(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseConfirmedVouchers()
                .Build();
            var parameters = GetByProjectsTestParameters(gridOptions, false, false, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossByProjectsAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_CompareProjects_ApprovedVouchers_CurrentAndChildren_NoOptions(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseApprovedVouchers()
                .Build();
            var parameters = GetByProjectsTestParameters(gridOptions, false, false, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossByProjectsAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_CompareProjects_AllVouchers_CurrentAndChildren_NoOptions(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .Build();
            var parameters = GetByProjectsTestParameters(gridOptions, false, false, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossByProjectsAsync(parameters, balanceItems);
        }

        #endregion

        #region GetProfitLoss_Periodic_CompareProjects_CurrentBranch_ClosingOption

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_CompareProjects_CheckedVouchers_CurrentBranch_ClosingOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseCheckedVouchers()
                .Build();
            var parameters = GetByProjectsTestParameters(gridOptions, false, true, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossByProjectsAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_CompareProjects_FinalizedVouchers_CurrentBranch_ClosingOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseFinalizedVouchers()
                .Build();
            var parameters = GetByProjectsTestParameters(gridOptions, false, true, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossByProjectsAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_CompareProjects_ConfirmedVouchers_CurrentBranch_ClosingOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseConfirmedVouchers()
                .Build();
            var parameters = GetByProjectsTestParameters(gridOptions, false, true, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossByProjectsAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_CompareProjects_ApprovedVouchers_CurrentBranch_ClosingOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseApprovedVouchers()
                .Build();
            var parameters = GetByProjectsTestParameters(gridOptions, false, true, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossByProjectsAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_CompareProjects_AllVouchers_CurrentBranch_ClosingOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .Build();
            var parameters = GetByProjectsTestParameters(gridOptions, false, true, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossByProjectsAsync(parameters, balanceItems);
        }

        #endregion

        #region GetProfitLoss_Periodic_CompareProjects_CurrentAndChildren_ClosingOption

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_CompareProjects_CheckedVouchers_CurrentAndChildren_ClosingOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseCheckedVouchers()
                .Build();
            var parameters = GetByProjectsTestParameters(gridOptions, false, true, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossByProjectsAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_CompareProjects_FinalizedVouchers_CurrentAndChildren_ClosingOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseFinalizedVouchers()
                .Build();
            var parameters = GetByProjectsTestParameters(gridOptions, false, true, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossByProjectsAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_CompareProjects_ConfirmedVouchers_CurrentAndChildren_ClosingOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseConfirmedVouchers()
                .Build();
            var parameters = GetByProjectsTestParameters(gridOptions, false, true, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossByProjectsAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_CompareProjects_ApprovedVouchers_CurrentAndChildren_ClosingOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseApprovedVouchers()
                .Build();
            var parameters = GetByProjectsTestParameters(gridOptions, false, true, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossByProjectsAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_CompareProjects_AllVouchers_CurrentAndChildren_ClosingOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .Build();
            var parameters = GetByProjectsTestParameters(gridOptions, false, true, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossByProjectsAsync(parameters, balanceItems);
        }

        #endregion

        #region GetProfitLoss_Periodic_CompareProjects_CurrentBranch_TurnoverOption

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_CompareProjects_CheckedVouchers_CurrentBranch_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseCheckedVouchers()
                .Build();
            var parameters = GetByProjectsTestParameters(gridOptions, true, false, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossByProjectsAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_CompareProjects_FinalizedVouchers_CurrentBranch_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseFinalizedVouchers()
                .Build();
            var parameters = GetByProjectsTestParameters(gridOptions, true, false, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossByProjectsAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_CompareProjects_ConfirmedVouchers_CurrentBranch_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseConfirmedVouchers()
                .Build();
            var parameters = GetByProjectsTestParameters(gridOptions, true, false, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossByProjectsAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_CompareProjects_ApprovedVouchers_CurrentBranch_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseApprovedVouchers()
                .Build();
            var parameters = GetByProjectsTestParameters(gridOptions, true, false, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossByProjectsAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_CompareProjects_AllVouchers_CurrentBranch_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .Build();
            var parameters = GetByProjectsTestParameters(gridOptions, true, false, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossByProjectsAsync(parameters, balanceItems);
        }

        #endregion

        #region GetProfitLoss_Periodic_CompareProjects_CurrentAndChildren_TurnoverOption

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_CompareProjects_CheckedVouchers_CurrentAndChildren_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseCheckedVouchers()
                .Build();
            var parameters = GetByProjectsTestParameters(gridOptions, true, false, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossByProjectsAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_CompareProjects_FinalizedVouchers_CurrentAndChildren_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseFinalizedVouchers()
                .Build();
            var parameters = GetByProjectsTestParameters(gridOptions, true, false, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossByProjectsAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_CompareProjects_ConfirmedVouchers_CurrentAndChildren_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseConfirmedVouchers()
                .Build();
            var parameters = GetByProjectsTestParameters(gridOptions, true, false, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossByProjectsAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_CompareProjects_ApprovedVouchers_CurrentAndChildren_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseApprovedVouchers()
                .Build();
            var parameters = GetByProjectsTestParameters(gridOptions, true, false, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossByProjectsAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_CompareProjects_AllVouchers_CurrentAndChildren_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .Build();
            var parameters = GetByProjectsTestParameters(gridOptions, true, false, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossByProjectsAsync(parameters, balanceItems);
        }

        #endregion

        #region GetProfitLoss_Periodic_CompareProjects_CurrentBranch_ClosingOption_TurnoverOption

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_CompareProjects_CheckedVouchers_CurrentBranch_ClosingOption_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseCheckedVouchers()
                .Build();
            var parameters = GetByProjectsTestParameters(gridOptions, true, true, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossByProjectsAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_CompareProjects_FinalizedVouchers_CurrentBranch_ClosingOption_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseFinalizedVouchers()
                .Build();
            var parameters = GetByProjectsTestParameters(gridOptions, true, true, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossByProjectsAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_CompareProjects_ConfirmedVouchers_CurrentBranch_ClosingOption_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseConfirmedVouchers()
                .Build();
            var parameters = GetByProjectsTestParameters(gridOptions, true, true, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossByProjectsAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_CompareProjects_ApprovedVouchers_CurrentBranch_ClosingOption_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseApprovedVouchers()
                .Build();
            var parameters = GetByProjectsTestParameters(gridOptions, true, true, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossByProjectsAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_CompareProjects_AllVouchers_CurrentBranch_ClosingOption_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .Build();
            var parameters = GetByProjectsTestParameters(gridOptions, true, true, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossByProjectsAsync(parameters, balanceItems);
        }

        #endregion

        #region GetProfitLoss_Periodic_CompareProjects_CurrentAndChildren_ClosingOption_TurnoverOption

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_CompareProjects_CheckedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseCheckedVouchers()
                .Build();
            var parameters = GetByProjectsTestParameters(gridOptions, true, true, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossByProjectsAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_CompareProjects_FinalizedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseFinalizedVouchers()
                .Build();
            var parameters = GetByProjectsTestParameters(gridOptions, true, true, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossByProjectsAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_CompareProjects_ConfirmedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseConfirmedVouchers()
                .Build();
            var parameters = GetByProjectsTestParameters(gridOptions, true, true, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossByProjectsAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_CompareProjects_ApprovedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseApprovedVouchers()
                .Build();
            var parameters = GetByProjectsTestParameters(gridOptions, true, true, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossByProjectsAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_CompareProjects_AllVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .Build();
            var parameters = GetByProjectsTestParameters(gridOptions, true, true, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossByProjectsAsync(parameters, balanceItems);
        }

        #endregion

        #region GetProfitLoss_Periodic_CompareCostCenters_CurrentBranch_NoOptions

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_CompareCostCenters_CheckedVouchers_CurrentBranch_NoOptions(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseCheckedVouchers()
                .Build();
            var parameters = GetByCostCentersTestParameters(gridOptions, false, false, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossByCostCentersAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_CompareCostCenters_FinalizedVouchers_CurrentBranch_NoOptions(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseFinalizedVouchers()
                .Build();
            var parameters = GetByCostCentersTestParameters(gridOptions, false, false, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossByCostCentersAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_CompareCostCenters_ConfirmedVouchers_CurrentBranch_NoOptions(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseConfirmedVouchers()
                .Build();
            var parameters = GetByCostCentersTestParameters(gridOptions, false, false, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossByCostCentersAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_CompareCostCenters_ApprovedVouchers_CurrentBranch_NoOptions(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseApprovedVouchers()
                .Build();
            var parameters = GetByCostCentersTestParameters(gridOptions, false, false, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossByCostCentersAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_CompareCostCenters_AllVouchers_CurrentBranch_NoOptions(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .Build();
            var parameters = GetByCostCentersTestParameters(gridOptions, false, false, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossByCostCentersAsync(parameters, balanceItems);
        }

        #endregion

        #region GetProfitLoss_Periodic_CompareCostCenters_CurrentAndChildren_NoOptions

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_CompareCostCenters_CheckedVouchers_CurrentAndChildren_NoOptions(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseCheckedVouchers()
                .Build();
            var parameters = GetByCostCentersTestParameters(gridOptions, false, false, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossByCostCentersAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_CompareCostCenters_FinalizedVouchers_CurrentAndChildren_NoOptions(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseFinalizedVouchers()
                .Build();
            var parameters = GetByCostCentersTestParameters(gridOptions, false, false, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossByCostCentersAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_CompareCostCenters_ConfirmedVouchers_CurrentAndChildren_NoOptions(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseConfirmedVouchers()
                .Build();
            var parameters = GetByCostCentersTestParameters(gridOptions, false, false, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossByCostCentersAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_CompareCostCenters_ApprovedVouchers_CurrentAndChildren_NoOptions(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseApprovedVouchers()
                .Build();
            var parameters = GetByCostCentersTestParameters(gridOptions, false, false, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossByCostCentersAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_CompareCostCenters_AllVouchers_CurrentAndChildren_NoOptions(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .Build();
            var parameters = GetByCostCentersTestParameters(gridOptions, false, false, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossByCostCentersAsync(parameters, balanceItems);
        }

        #endregion

        #region GetProfitLoss_Periodic_CompareCostCenters_CurrentBranch_ClosingOption

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_CompareCostCenters_CheckedVouchers_CurrentBranch_ClosingOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseCheckedVouchers()
                .Build();
            var parameters = GetByCostCentersTestParameters(gridOptions, false, true, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossByCostCentersAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_CompareCostCenters_FinalizedVouchers_CurrentBranch_ClosingOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseFinalizedVouchers()
                .Build();
            var parameters = GetByCostCentersTestParameters(gridOptions, false, true, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossByCostCentersAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_CompareCostCenters_ConfirmedVouchers_CurrentBranch_ClosingOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseConfirmedVouchers()
                .Build();
            var parameters = GetByCostCentersTestParameters(gridOptions, false, true, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossByCostCentersAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_CompareCostCenters_ApprovedVouchers_CurrentBranch_ClosingOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseApprovedVouchers()
                .Build();
            var parameters = GetByCostCentersTestParameters(gridOptions, false, true, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossByCostCentersAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_CompareCostCenters_AllVouchers_CurrentBranch_ClosingOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .Build();
            var parameters = GetByCostCentersTestParameters(gridOptions, false, true, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossByCostCentersAsync(parameters, balanceItems);
        }

        #endregion

        #region GetProfitLoss_Periodic_CompareCostCenters_CurrentAndChildren_ClosingOption

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_CompareCostCenters_CheckedVouchers_CurrentAndChildren_ClosingOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseCheckedVouchers()
                .Build();
            var parameters = GetByCostCentersTestParameters(gridOptions, false, true, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossByCostCentersAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_CompareCostCenters_FinalizedVouchers_CurrentAndChildren_ClosingOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseFinalizedVouchers()
                .Build();
            var parameters = GetByCostCentersTestParameters(gridOptions, false, true, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossByCostCentersAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_CompareCostCenters_ConfirmedVouchers_CurrentAndChildren_ClosingOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseConfirmedVouchers()
                .Build();
            var parameters = GetByCostCentersTestParameters(gridOptions, false, true, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossByCostCentersAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_CompareCostCenters_ApprovedVouchers_CurrentAndChildren_ClosingOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseApprovedVouchers()
                .Build();
            var parameters = GetByCostCentersTestParameters(gridOptions, false, true, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossByCostCentersAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_CompareCostCenters_AllVouchers_CurrentAndChildren_ClosingOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .Build();
            var parameters = GetByCostCentersTestParameters(gridOptions, false, true, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossByCostCentersAsync(parameters, balanceItems);
        }

        #endregion

        #region GetProfitLoss_Periodic_CompareCostCenters_CurrentBranch_TurnoverOption

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_CompareCostCenters_CheckedVouchers_CurrentBranch_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseCheckedVouchers()
                .Build();
            var parameters = GetByCostCentersTestParameters(gridOptions, true, false, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossByCostCentersAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_CompareCostCenters_FinalizedVouchers_CurrentBranch_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseFinalizedVouchers()
                .Build();
            var parameters = GetByCostCentersTestParameters(gridOptions, true, false, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossByCostCentersAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_CompareCostCenters_ConfirmedVouchers_CurrentBranch_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseConfirmedVouchers()
                .Build();
            var parameters = GetByCostCentersTestParameters(gridOptions, true, false, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossByCostCentersAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_CompareCostCenters_ApprovedVouchers_CurrentBranch_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseApprovedVouchers()
                .Build();
            var parameters = GetByCostCentersTestParameters(gridOptions, true, false, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossByCostCentersAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_CompareCostCenters_AllVouchers_CurrentBranch_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .Build();
            var parameters = GetByCostCentersTestParameters(gridOptions, true, false, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossByCostCentersAsync(parameters, balanceItems);
        }

        #endregion

        #region GetProfitLoss_Periodic_CompareCostCenters_CurrentAndChildren_TurnoverOption

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_CompareCostCenters_CheckedVouchers_CurrentAndChildren_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseCheckedVouchers()
                .Build();
            var parameters = GetByCostCentersTestParameters(gridOptions, true, false, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossByCostCentersAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_CompareCostCenters_FinalizedVouchers_CurrentAndChildren_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseFinalizedVouchers()
                .Build();
            var parameters = GetByCostCentersTestParameters(gridOptions, true, false, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossByCostCentersAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_CompareCostCenters_ConfirmedVouchers_CurrentAndChildren_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseConfirmedVouchers()
                .Build();
            var parameters = GetByCostCentersTestParameters(gridOptions, true, false, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossByCostCentersAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_CompareCostCenters_ApprovedVouchers_CurrentAndChildren_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseApprovedVouchers()
                .Build();
            var parameters = GetByCostCentersTestParameters(gridOptions, true, false, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossByCostCentersAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_CompareCostCenters_AllVouchers_CurrentAndChildren_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .Build();
            var parameters = GetByCostCentersTestParameters(gridOptions, true, false, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossByCostCentersAsync(parameters, balanceItems);
        }

        #endregion

        #region GetProfitLoss_Periodic_CompareCostCenters_CurrentBranch_ClosingOption_TurnoverOption

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_CompareCostCenters_CheckedVouchers_CurrentBranch_ClosingOption_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseCheckedVouchers()
                .Build();
            var parameters = GetByCostCentersTestParameters(gridOptions, true, true, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossByCostCentersAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_CompareCostCenters_FinalizedVouchers_CurrentBranch_ClosingOption_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseFinalizedVouchers()
                .Build();
            var parameters = GetByCostCentersTestParameters(gridOptions, true, true, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossByCostCentersAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_CompareCostCenters_ConfirmedVouchers_CurrentBranch_ClosingOption_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseConfirmedVouchers()
                .Build();
            var parameters = GetByCostCentersTestParameters(gridOptions, true, true, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossByCostCentersAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_CompareCostCenters_ApprovedVouchers_CurrentBranch_ClosingOption_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseApprovedVouchers()
                .Build();
            var parameters = GetByCostCentersTestParameters(gridOptions, true, true, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossByCostCentersAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_CompareCostCenters_AllVouchers_CurrentBranch_ClosingOption_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .Build();
            var parameters = GetByCostCentersTestParameters(gridOptions, true, true, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossByCostCentersAsync(parameters, balanceItems);
        }

        #endregion

        #region GetProfitLoss_Periodic_CompareCostCenters_CurrentAndChildren_ClosingOption_TurnoverOption

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_CompareCostCenters_CheckedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseCheckedVouchers()
                .Build();
            var parameters = GetByCostCentersTestParameters(gridOptions, true, true, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossByCostCentersAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_CompareCostCenters_FinalizedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseFinalizedVouchers()
                .Build();
            var parameters = GetByCostCentersTestParameters(gridOptions, true, true, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossByCostCentersAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_CompareCostCenters_ConfirmedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseConfirmedVouchers()
                .Build();
            var parameters = GetByCostCentersTestParameters(gridOptions, true, true, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossByCostCentersAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_CompareCostCenters_ApprovedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseApprovedVouchers()
                .Build();
            var parameters = GetByCostCentersTestParameters(gridOptions, true, true, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossByCostCentersAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_CompareCostCenters_AllVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .Build();
            var parameters = GetByCostCentersTestParameters(gridOptions, true, true, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossByCostCentersAsync(parameters, balanceItems);
        }

        #endregion

        #region GetProfitLoss_Periodic_CompareFiscalPeriods_CurrentBranch_NoOptions

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_CompareFiscalPeriods_CheckedVouchers_CurrentBranch_NoOptions(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseCheckedVouchers()
                .Build();
            var parameters = GetByFiscalPeriodsTestParameters(gridOptions, false, false, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossByFiscalPeriodsAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_CompareFiscalPeriods_FinalizedVouchers_CurrentBranch_NoOptions(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseFinalizedVouchers()
                .Build();
            var parameters = GetByFiscalPeriodsTestParameters(gridOptions, false, false, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossByFiscalPeriodsAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_CompareFiscalPeriods_ConfirmedVouchers_CurrentBranch_NoOptions(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseConfirmedVouchers()
                .Build();
            var parameters = GetByFiscalPeriodsTestParameters(gridOptions, false, false, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossByFiscalPeriodsAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_CompareFiscalPeriods_ApprovedVouchers_CurrentBranch_NoOptions(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseApprovedVouchers()
                .Build();
            var parameters = GetByFiscalPeriodsTestParameters(gridOptions, false, false, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossByFiscalPeriodsAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_CompareFiscalPeriods_AllVouchers_CurrentBranch_NoOptions(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .Build();
            var parameters = GetByFiscalPeriodsTestParameters(gridOptions, false, false, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossByFiscalPeriodsAsync(parameters, balanceItems);
        }

        #endregion

        #region GetProfitLoss_Periodic_CompareFiscalPeriods_CurrentAndChildren_NoOptions

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_CompareFiscalPeriods_CheckedVouchers_CurrentAndChildren_NoOptions(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseCheckedVouchers()
                .Build();
            var parameters = GetByFiscalPeriodsTestParameters(gridOptions, false, false, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossByFiscalPeriodsAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_CompareFiscalPeriods_FinalizedVouchers_CurrentAndChildren_NoOptions(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseFinalizedVouchers()
                .Build();
            var parameters = GetByFiscalPeriodsTestParameters(gridOptions, false, false, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossByFiscalPeriodsAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_CompareFiscalPeriods_ConfirmedVouchers_CurrentAndChildren_NoOptions(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseConfirmedVouchers()
                .Build();
            var parameters = GetByFiscalPeriodsTestParameters(gridOptions, false, false, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossByFiscalPeriodsAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_CompareFiscalPeriods_ApprovedVouchers_CurrentAndChildren_NoOptions(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseApprovedVouchers()
                .Build();
            var parameters = GetByFiscalPeriodsTestParameters(gridOptions, false, false, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossByFiscalPeriodsAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_CompareFiscalPeriods_AllVouchers_CurrentAndChildren_NoOptions(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .Build();
            var parameters = GetByFiscalPeriodsTestParameters(gridOptions, false, false, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossByFiscalPeriodsAsync(parameters, balanceItems);
        }

        #endregion

        #region GetProfitLoss_Periodic_CompareFiscalPeriods_CurrentBranch_ClosingOption

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_CompareFiscalPeriods_CheckedVouchers_CurrentBranch_ClosingOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseCheckedVouchers()
                .Build();
            var parameters = GetByFiscalPeriodsTestParameters(gridOptions, false, true, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossByFiscalPeriodsAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_CompareFiscalPeriods_FinalizedVouchers_CurrentBranch_ClosingOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseFinalizedVouchers()
                .Build();
            var parameters = GetByFiscalPeriodsTestParameters(gridOptions, false, true, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossByFiscalPeriodsAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_CompareFiscalPeriods_ConfirmedVouchers_CurrentBranch_ClosingOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseConfirmedVouchers()
                .Build();
            var parameters = GetByFiscalPeriodsTestParameters(gridOptions, false, true, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossByFiscalPeriodsAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_CompareFiscalPeriods_ApprovedVouchers_CurrentBranch_ClosingOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseApprovedVouchers()
                .Build();
            var parameters = GetByFiscalPeriodsTestParameters(gridOptions, false, true, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossByFiscalPeriodsAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_CompareFiscalPeriods_AllVouchers_CurrentBranch_ClosingOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .Build();
            var parameters = GetByFiscalPeriodsTestParameters(gridOptions, false, true, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossByFiscalPeriodsAsync(parameters, balanceItems);
        }

        #endregion

        #region GetProfitLoss_Periodic_CompareFiscalPeriods_CurrentAndChildren_ClosingOption

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_CompareFiscalPeriods_CheckedVouchers_CurrentAndChildren_ClosingOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseCheckedVouchers()
                .Build();
            var parameters = GetByFiscalPeriodsTestParameters(gridOptions, false, true, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossByFiscalPeriodsAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_CompareFiscalPeriods_FinalizedVouchers_CurrentAndChildren_ClosingOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseFinalizedVouchers()
                .Build();
            var parameters = GetByFiscalPeriodsTestParameters(gridOptions, false, true, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossByFiscalPeriodsAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_CompareFiscalPeriods_ConfirmedVouchers_CurrentAndChildren_ClosingOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseConfirmedVouchers()
                .Build();
            var parameters = GetByFiscalPeriodsTestParameters(gridOptions, false, true, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossByFiscalPeriodsAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_CompareFiscalPeriods_ApprovedVouchers_CurrentAndChildren_ClosingOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseApprovedVouchers()
                .Build();
            var parameters = GetByFiscalPeriodsTestParameters(gridOptions, false, true, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossByFiscalPeriodsAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_CompareFiscalPeriods_AllVouchers_CurrentAndChildren_ClosingOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .Build();
            var parameters = GetByFiscalPeriodsTestParameters(gridOptions, false, true, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossByFiscalPeriodsAsync(parameters, balanceItems);
        }

        #endregion

        #region GetProfitLoss_Periodic_CompareFiscalPeriods_CurrentBranch_TurnoverOption

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_CompareFiscalPeriods_CheckedVouchers_CurrentBranch_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseCheckedVouchers()
                .Build();
            var parameters = GetByFiscalPeriodsTestParameters(gridOptions, true, false, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossByFiscalPeriodsAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_CompareFiscalPeriods_FinalizedVouchers_CurrentBranch_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseFinalizedVouchers()
                .Build();
            var parameters = GetByFiscalPeriodsTestParameters(gridOptions, true, false, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossByFiscalPeriodsAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_CompareFiscalPeriods_ConfirmedVouchers_CurrentBranch_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseConfirmedVouchers()
                .Build();
            var parameters = GetByFiscalPeriodsTestParameters(gridOptions, true, false, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossByFiscalPeriodsAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_CompareFiscalPeriods_ApprovedVouchers_CurrentBranch_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseApprovedVouchers()
                .Build();
            var parameters = GetByFiscalPeriodsTestParameters(gridOptions, true, false, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossByFiscalPeriodsAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_CompareFiscalPeriods_AllVouchers_CurrentBranch_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .Build();
            var parameters = GetByFiscalPeriodsTestParameters(gridOptions, true, false, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossByFiscalPeriodsAsync(parameters, balanceItems);
        }

        #endregion

        #region GetProfitLoss_Periodic_CompareFiscalPeriods_CurrentAndChildren_TurnoverOption

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_CompareFiscalPeriods_CheckedVouchers_CurrentAndChildren_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseCheckedVouchers()
                .Build();
            var parameters = GetByFiscalPeriodsTestParameters(gridOptions, true, false, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossByFiscalPeriodsAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_CompareFiscalPeriods_FinalizedVouchers_CurrentAndChildren_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseFinalizedVouchers()
                .Build();
            var parameters = GetByFiscalPeriodsTestParameters(gridOptions, true, false, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossByFiscalPeriodsAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_CompareFiscalPeriods_ConfirmedVouchers_CurrentAndChildren_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseConfirmedVouchers()
                .Build();
            var parameters = GetByFiscalPeriodsTestParameters(gridOptions, true, false, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossByFiscalPeriodsAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_CompareFiscalPeriods_ApprovedVouchers_CurrentAndChildren_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseApprovedVouchers()
                .Build();
            var parameters = GetByFiscalPeriodsTestParameters(gridOptions, true, false, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossByFiscalPeriodsAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_CompareFiscalPeriods_AllVouchers_CurrentAndChildren_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .Build();
            var parameters = GetByFiscalPeriodsTestParameters(gridOptions, true, false, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossByFiscalPeriodsAsync(parameters, balanceItems);
        }

        #endregion

        #region GetProfitLoss_Periodic_CompareFiscalPeriods_CurrentBranch_ClosingOption_TurnoverOption

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_CompareFiscalPeriods_CheckedVouchers_CurrentBranch_ClosingOption_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseCheckedVouchers()
                .Build();
            var parameters = GetByFiscalPeriodsTestParameters(gridOptions, true, true, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossByFiscalPeriodsAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_CompareFiscalPeriods_FinalizedVouchers_CurrentBranch_ClosingOption_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseFinalizedVouchers()
                .Build();
            var parameters = GetByFiscalPeriodsTestParameters(gridOptions, true, true, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossByFiscalPeriodsAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_CompareFiscalPeriods_ConfirmedVouchers_CurrentBranch_ClosingOption_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseConfirmedVouchers()
                .Build();
            var parameters = GetByFiscalPeriodsTestParameters(gridOptions, true, true, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossByFiscalPeriodsAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_CompareFiscalPeriods_ApprovedVouchers_CurrentBranch_ClosingOption_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseApprovedVouchers()
                .Build();
            var parameters = GetByFiscalPeriodsTestParameters(gridOptions, true, true, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossByFiscalPeriodsAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_CompareFiscalPeriods_AllVouchers_CurrentBranch_ClosingOption_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .Build();
            var parameters = GetByFiscalPeriodsTestParameters(gridOptions, true, true, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossByFiscalPeriodsAsync(parameters, balanceItems);
        }

        #endregion

        #region GetProfitLoss_Periodic_CompareFiscalPeriods_CurrentAndChildren_ClosingOption_TurnoverOption

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_CompareFiscalPeriods_CheckedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseCheckedVouchers()
                .Build();
            var parameters = GetByFiscalPeriodsTestParameters(gridOptions, true, true, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossByFiscalPeriodsAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_CompareFiscalPeriods_FinalizedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseFinalizedVouchers()
                .Build();
            var parameters = GetByFiscalPeriodsTestParameters(gridOptions, true, true, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossByFiscalPeriodsAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_CompareFiscalPeriods_ConfirmedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseConfirmedVouchers()
                .Build();
            var parameters = GetByFiscalPeriodsTestParameters(gridOptions, true, true, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossByFiscalPeriodsAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_CompareFiscalPeriods_ApprovedVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseApprovedVouchers()
                .Build();
            var parameters = GetByFiscalPeriodsTestParameters(gridOptions, true, true, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossByFiscalPeriodsAsync(parameters, balanceItems);
        }

        private async Task<ProfitLossViewModel> GetProfitLoss_Periodic_CompareFiscalPeriods_AllVouchers_CurrentAndChildren_ClosingOption_TurnoverOption(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .Build();
            var parameters = GetByFiscalPeriodsTestParameters(gridOptions, true, true, ccenterId, projectId, InventoryMode.Periodic);
            var balanceItems = GetTestPeriodicBalanceItems();
            return await _repository.GetProfitLossByFiscalPeriodsAsync(parameters, balanceItems);
        }

        #endregion

        private ProfitLossParameters GetTestParameters(
            GridOptions gridOptions, bool startAsInit, bool useClosing,
            int? ccenterId = null, int? projectId = null, InventoryMode mode = InventoryMode.Perpetual)
        {
            _repository.UserContext.InventoryMode = (int)mode;
            return new ProfitLossParameters()
            {
                FromDate = DateTime.Parse("2018-06-21"),
                ToDate = DateTime.Parse("2019-03-20"),
                GridOptions = gridOptions,
                CostCenterId = ccenterId,
                ProjectId = projectId,
                TaxAmount = 10000,
                StartTurnoverAsInitBalance = startAsInit,
                UseClosingTempVoucher = useClosing
            };
        }

        private ProfitLossParameters GetByBranchesTestParameters(
            GridOptions gridOptions, bool startAsInit, bool useClosing,
            int? ccenterId = null, int? projectId = null, InventoryMode mode = InventoryMode.Perpetual)
        {
            var parameters = GetTestParameters(gridOptions, startAsInit, useClosing, ccenterId, projectId, mode);
            parameters.CompareItems.AddRange(new List<int> { 1, 3 });
            return parameters;
        }

        private ProfitLossParameters GetByProjectsTestParameters(
            GridOptions gridOptions, bool startAsInit, bool useClosing,
            int? ccenterId = null, int? projectId = null, InventoryMode mode = InventoryMode.Perpetual)
        {
            var parameters = GetTestParameters(gridOptions, startAsInit, useClosing, ccenterId, projectId, mode);
            parameters.CompareItems.AddRange(new List<int> { 11, 12 });
            return parameters;
        }

        private ProfitLossParameters GetByCostCentersTestParameters(
            GridOptions gridOptions, bool startAsInit, bool useClosing,
            int? ccenterId = null, int? projectId = null, InventoryMode mode = InventoryMode.Perpetual)
        {
            var parameters = GetTestParameters(gridOptions, startAsInit, useClosing, ccenterId, projectId, mode);
            parameters.CompareItems.AddRange(new List<int> { 10, 14 });
            return parameters;
        }

        private ProfitLossParameters GetByFiscalPeriodsTestParameters(
            GridOptions gridOptions, bool startAsInit, bool useClosing,
            int? ccenterId = null, int? projectId = null, InventoryMode mode = InventoryMode.Perpetual)
        {
            var parameters = GetTestParameters(gridOptions, startAsInit, useClosing, ccenterId, projectId, mode);
            parameters.CompareItems.AddRange(new List<int> { 1, 2 });
            return parameters;
        }

        private IEnumerable<StartEndBalanceViewModel> GetTestPeriodicBalanceItems()
        {
            return new List<StartEndBalanceViewModel>
            {
                new StartEndBalanceViewModel()
                {
                    StartBalanceDebit = 10000M,
                    EndBalanceDebit = 200000M
                },
                new StartEndBalanceViewModel()
                {
                    StartBalanceDebit = 50000M,
                    EndBalanceCredit = 20000M
                },
                new StartEndBalanceViewModel()
                {
                    StartBalanceCredit = 15000M,
                    EndBalanceDebit = 28000M
                },
                new StartEndBalanceViewModel()
                {
                    StartBalanceCredit = 180000M,
                    EndBalanceCredit = 120000M
                }
            };
        }

        private const int TestCenterId = 14;
        private const int TestProjectId = 4;
        private IProfitLossRepository _repository;
    }
}
