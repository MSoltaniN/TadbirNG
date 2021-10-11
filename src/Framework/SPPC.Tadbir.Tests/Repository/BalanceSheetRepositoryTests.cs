using System;
using System.Threading.Tasks;
using NUnit.Framework;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Persistence.Utility;
using SPPC.Tadbir.ViewModel.Reporting;

namespace SPPC.Tadbir.Persistence.Tests
{
    [TestFixture]
    [Category("RepositorySmokeTest")]
    public class BalanceSheetRepositoryTests : RepositoryTestBase
    {
        [OneTimeSetUp]
        public void FixtureSetup()
        {
            SetUp();
        }

        #region CheckedVouchers_CurrentBranch_NoPrevious_NoOptions_NoFilterSort

        [Test]
        public async Task BalanceSheet_CheckedVouchers_CurrentBranch_NoPrevious_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balanceSheet = await GetBalSheet_CheckedVouchers_CurrentBranch_NoPrevious_NoOptions_NoFilterSort();

            // Assert
            Assert.That(balanceSheet, Is.Not.Null);
        }

        [Test]
        public async Task BalanceSheet_CheckedVouchers_CurrentBranch_ByCenter_NoPrevious_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balanceSheet = await GetBalSheet_CheckedVouchers_CurrentBranch_NoPrevious_NoOptions_NoFilterSort(
                TestCenterId);

            // Assert
            Assert.That(balanceSheet, Is.Not.Null);
        }

        [Test]
        public async Task BalanceSheet_CheckedVouchers_CurrentBranch_ByProject_NoPrevious_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balanceSheet = await GetBalSheet_CheckedVouchers_CurrentBranch_NoPrevious_NoOptions_NoFilterSort(
                null, TestProjectId);

            // Assert
            Assert.That(balanceSheet, Is.Not.Null);
        }

        [Test]
        public async Task BalanceSheet_CheckedVouchers_CurrentBranch_ByCenterProject_NoPrevious_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balanceSheet = await GetBalSheet_CheckedVouchers_CurrentBranch_NoPrevious_NoOptions_NoFilterSort(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(balanceSheet, Is.Not.Null);
        }

        #endregion

        #region FinalizedVouchers_CurrentBranch_NoPrevious_NoOptions_NoFilterSort

        [Test]
        public async Task BalanceSheet_FinalizedVouchers_CurrentBranch_NoPrevious_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balanceSheet = await GetBalSheet_FinalizedVouchers_CurrentBranch_NoPrevious_NoOptions_NoFilterSort();

            // Assert
            Assert.That(balanceSheet, Is.Not.Null);
        }

        [Test]
        public async Task BalanceSheet_FinalizedVouchers_CurrentBranch_ByCenter_NoPrevious_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balanceSheet = await GetBalSheet_FinalizedVouchers_CurrentBranch_NoPrevious_NoOptions_NoFilterSort(
                TestCenterId);

            // Assert
            Assert.That(balanceSheet, Is.Not.Null);
        }

        [Test]
        public async Task BalanceSheet_FinalizedVouchers_CurrentBranch_ByProject_NoPrevious_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balanceSheet = await GetBalSheet_FinalizedVouchers_CurrentBranch_NoPrevious_NoOptions_NoFilterSort(
                null, TestProjectId);

            // Assert
            Assert.That(balanceSheet, Is.Not.Null);
        }

        [Test]
        public async Task BalanceSheet_FinalizedVouchers_CurrentBranch_ByCenterProject_NoPrevious_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balanceSheet = await GetBalSheet_FinalizedVouchers_CurrentBranch_NoPrevious_NoOptions_NoFilterSort(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(balanceSheet, Is.Not.Null);
        }

        #endregion

        #region ConfirmedVouchers_CurrentBranch_NoPrevious_NoOptions_NoFilterSort

        [Test]
        public async Task BalanceSheet_ConfirmedVouchers_CurrentBranch_NoPrevious_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balanceSheet = await GetBalSheet_ConfirmedVouchers_CurrentBranch_NoPrevious_NoOptions_NoFilterSort();

            // Assert
            Assert.That(balanceSheet, Is.Not.Null);
        }

        [Test]
        public async Task BalanceSheet_ConfirmedVouchers_CurrentBranch_ByCenter_NoPrevious_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balanceSheet = await GetBalSheet_ConfirmedVouchers_CurrentBranch_NoPrevious_NoOptions_NoFilterSort(
                TestCenterId);

            // Assert
            Assert.That(balanceSheet, Is.Not.Null);
        }

        [Test]
        public async Task BalanceSheet_ConfirmedVouchers_CurrentBranch_ByProject_NoPrevious_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balanceSheet = await GetBalSheet_ConfirmedVouchers_CurrentBranch_NoPrevious_NoOptions_NoFilterSort(
                null, TestProjectId);

            // Assert
            Assert.That(balanceSheet, Is.Not.Null);
        }

        [Test]
        public async Task BalanceSheet_ConfirmedVouchers_CurrentBranch_ByCenterProject_NoPrevious_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balanceSheet = await GetBalSheet_ConfirmedVouchers_CurrentBranch_NoPrevious_NoOptions_NoFilterSort(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(balanceSheet, Is.Not.Null);
        }

        #endregion

        #region ApprovedVouchers_CurrentBranch_NoPrevious_NoOptions_NoFilterSort

        [Test]
        public async Task BalanceSheet_ApprovedVouchers_CurrentBranch_NoPrevious_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balanceSheet = await GetBalSheet_ApprovedVouchers_CurrentBranch_NoPrevious_NoOptions_NoFilterSort();

            // Assert
            Assert.That(balanceSheet, Is.Not.Null);
        }

        [Test]
        public async Task BalanceSheet_ApprovedVouchers_CurrentBranch_ByCenter_NoPrevious_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balanceSheet = await GetBalSheet_ApprovedVouchers_CurrentBranch_NoPrevious_NoOptions_NoFilterSort(
                TestCenterId);

            // Assert
            Assert.That(balanceSheet, Is.Not.Null);
        }

        [Test]
        public async Task BalanceSheet_ApprovedVouchers_CurrentBranch_ByProject_NoPrevious_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balanceSheet = await GetBalSheet_ApprovedVouchers_CurrentBranch_NoPrevious_NoOptions_NoFilterSort(
                null, TestProjectId);

            // Assert
            Assert.That(balanceSheet, Is.Not.Null);
        }

        [Test]
        public async Task BalanceSheet_ApprovedVouchers_CurrentBranch_ByCenterProject_NoPrevious_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balanceSheet = await GetBalSheet_ApprovedVouchers_CurrentBranch_NoPrevious_NoOptions_NoFilterSort(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(balanceSheet, Is.Not.Null);
        }

        #endregion

        #region AllVouchers_CurrentBranch_NoPrevious_NoOptions_NoFilterSort

        [Test]
        public async Task BalanceSheet_AllVouchers_CurrentBranch_NoPrevious_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balanceSheet = await GetBalSheet_AllVouchers_CurrentBranch_NoPrevious_NoOptions_NoFilterSort();

            // Assert
            Assert.That(balanceSheet, Is.Not.Null);
        }

        [Test]
        public async Task BalanceSheet_AllVouchers_CurrentBranch_ByCenter_NoPrevious_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balanceSheet = await GetBalSheet_AllVouchers_CurrentBranch_NoPrevious_NoOptions_NoFilterSort(
                TestCenterId);

            // Assert
            Assert.That(balanceSheet, Is.Not.Null);
        }

        [Test]
        public async Task BalanceSheet_AllVouchers_CurrentBranch_ByProject_NoPrevious_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balanceSheet = await GetBalSheet_AllVouchers_CurrentBranch_NoPrevious_NoOptions_NoFilterSort(
                null, TestProjectId);

            // Assert
            Assert.That(balanceSheet, Is.Not.Null);
        }

        [Test]
        public async Task BalanceSheet_AllVouchers_CurrentBranch_ByCenterProject_NoPrevious_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balanceSheet = await GetBalSheet_AllVouchers_CurrentBranch_NoPrevious_NoOptions_NoFilterSort(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(balanceSheet, Is.Not.Null);
        }

        #endregion

        #region CheckedVouchers_CurrentAndChildren_NoPrevious_NoOptions_NoFilterSort

        [Test]
        public async Task BalanceSheet_CheckedVouchers_CurrentAndChildren_NoPrevious_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balanceSheet = await GetBalSheet_CheckedVouchers_CurrentAndChildren_NoPrevious_NoOptions_NoFilterSort();

            // Assert
            Assert.That(balanceSheet, Is.Not.Null);
        }

        [Test]
        public async Task BalanceSheet_CheckedVouchers_CurrentAndChildren_ByCenter_NoPrevious_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balanceSheet = await GetBalSheet_CheckedVouchers_CurrentAndChildren_NoPrevious_NoOptions_NoFilterSort(
                TestCenterId);

            // Assert
            Assert.That(balanceSheet, Is.Not.Null);
        }

        [Test]
        public async Task BalanceSheet_CheckedVouchers_CurrentAndChildren_ByProject_NoPrevious_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balanceSheet = await GetBalSheet_CheckedVouchers_CurrentAndChildren_NoPrevious_NoOptions_NoFilterSort(
                null, TestProjectId);

            // Assert
            Assert.That(balanceSheet, Is.Not.Null);
        }

        [Test]
        public async Task BalanceSheet_CheckedVouchers_CurrentAndChildren_ByCenterProject_NoPrevious_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balanceSheet = await GetBalSheet_CheckedVouchers_CurrentAndChildren_NoPrevious_NoOptions_NoFilterSort(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(balanceSheet, Is.Not.Null);
        }

        #endregion

        #region FinalizedVouchers_CurrentAndChildren_NoPrevious_NoOptions_NoFilterSort

        [Test]
        public async Task BalanceSheet_FinalizedVouchers_CurrentAndChildren_NoPrevious_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balanceSheet = await GetBalSheet_FinalizedVouchers_CurrentAndChildren_NoPrevious_NoOptions_NoFilterSort();

            // Assert
            Assert.That(balanceSheet, Is.Not.Null);
        }

        [Test]
        public async Task BalanceSheet_FinalizedVouchers_CurrentAndChildren_ByCenter_NoPrevious_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balanceSheet = await GetBalSheet_FinalizedVouchers_CurrentAndChildren_NoPrevious_NoOptions_NoFilterSort(
                TestCenterId);

            // Assert
            Assert.That(balanceSheet, Is.Not.Null);
        }

        [Test]
        public async Task BalanceSheet_FinalizedVouchers_CurrentAndChildren_ByProject_NoPrevious_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balanceSheet = await GetBalSheet_FinalizedVouchers_CurrentAndChildren_NoPrevious_NoOptions_NoFilterSort(
                null, TestProjectId);

            // Assert
            Assert.That(balanceSheet, Is.Not.Null);
        }

        [Test]
        public async Task BalanceSheet_FinalizedVouchers_CurrentAndChildren_ByCenterProject_NoPrevious_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balanceSheet = await GetBalSheet_FinalizedVouchers_CurrentAndChildren_NoPrevious_NoOptions_NoFilterSort(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(balanceSheet, Is.Not.Null);
        }

        #endregion

        #region ConfirmedVouchers_CurrentAndChildren_NoPrevious_NoOptions_NoFilterSort

        [Test]
        public async Task BalanceSheet_ConfirmedVouchers_CurrentAndChildren_NoPrevious_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balanceSheet = await GetBalSheet_ConfirmedVouchers_CurrentAndChildren_NoPrevious_NoOptions_NoFilterSort();

            // Assert
            Assert.That(balanceSheet, Is.Not.Null);
        }

        [Test]
        public async Task BalanceSheet_ConfirmedVouchers_CurrentAndChildren_ByCenter_NoPrevious_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balanceSheet = await GetBalSheet_ConfirmedVouchers_CurrentAndChildren_NoPrevious_NoOptions_NoFilterSort(
                TestCenterId);

            // Assert
            Assert.That(balanceSheet, Is.Not.Null);
        }

        [Test]
        public async Task BalanceSheet_ConfirmedVouchers_CurrentAndChildren_ByProject_NoPrevious_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balanceSheet = await GetBalSheet_ConfirmedVouchers_CurrentAndChildren_NoPrevious_NoOptions_NoFilterSort(
                null, TestProjectId);

            // Assert
            Assert.That(balanceSheet, Is.Not.Null);
        }

        [Test]
        public async Task BalanceSheet_ConfirmedVouchers_CurrentAndChildren_ByCenterProject_NoPrevious_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balanceSheet = await GetBalSheet_ConfirmedVouchers_CurrentAndChildren_NoPrevious_NoOptions_NoFilterSort(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(balanceSheet, Is.Not.Null);
        }

        #endregion

        #region ApprovedVouchers_CurrentAndChildren_NoPrevious_NoOptions_NoFilterSort

        [Test]
        public async Task BalanceSheet_ApprovedVouchers_CurrentAndChildren_NoPrevious_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balanceSheet = await GetBalSheet_ApprovedVouchers_CurrentAndChildren_NoPrevious_NoOptions_NoFilterSort();

            // Assert
            Assert.That(balanceSheet, Is.Not.Null);
        }

        [Test]
        public async Task BalanceSheet_ApprovedVouchers_CurrentAndChildren_ByCenter_NoPrevious_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balanceSheet = await GetBalSheet_ApprovedVouchers_CurrentAndChildren_NoPrevious_NoOptions_NoFilterSort(
                TestCenterId);

            // Assert
            Assert.That(balanceSheet, Is.Not.Null);
        }

        [Test]
        public async Task BalanceSheet_ApprovedVouchers_CurrentAndChildren_ByProject_NoPrevious_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balanceSheet = await GetBalSheet_ApprovedVouchers_CurrentAndChildren_NoPrevious_NoOptions_NoFilterSort(
                null, TestProjectId);

            // Assert
            Assert.That(balanceSheet, Is.Not.Null);
        }

        [Test]
        public async Task BalanceSheet_ApprovedVouchers_CurrentAndChildren_ByCenterProject_NoPrevious_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balanceSheet = await GetBalSheet_ApprovedVouchers_CurrentAndChildren_NoPrevious_NoOptions_NoFilterSort(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(balanceSheet, Is.Not.Null);
        }

        #endregion

        #region AllVouchers_CurrentAndChildren_NoPrevious_NoOptions_NoFilterSort

        [Test]
        public async Task BalanceSheet_AllVouchers_CurrentAndChildren_NoPrevious_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balanceSheet = await GetBalSheet_AllVouchers_CurrentAndChildren_NoPrevious_NoOptions_NoFilterSort();

            // Assert
            Assert.That(balanceSheet, Is.Not.Null);
        }

        [Test]
        public async Task BalanceSheet_AllVouchers_CurrentAndChildren_ByCenter_NoPrevious_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balanceSheet = await GetBalSheet_AllVouchers_CurrentAndChildren_NoPrevious_NoOptions_NoFilterSort(
                TestCenterId);

            // Assert
            Assert.That(balanceSheet, Is.Not.Null);
        }

        [Test]
        public async Task BalanceSheet_AllVouchers_CurrentAndChildren_ByProject_NoPrevious_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balanceSheet = await GetBalSheet_AllVouchers_CurrentAndChildren_NoPrevious_NoOptions_NoFilterSort(
                null, TestProjectId);

            // Assert
            Assert.That(balanceSheet, Is.Not.Null);
        }

        [Test]
        public async Task BalanceSheet_AllVouchers_CurrentAndChildren_ByCenterProject_NoPrevious_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balanceSheet = await GetBalSheet_AllVouchers_CurrentAndChildren_NoPrevious_NoOptions_NoFilterSort(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(balanceSheet, Is.Not.Null);
        }

        #endregion

        #region CheckedVouchers_CurrentBranch_NoPrevious_ClosingOption_NoFilterSort

        [Test]
        public async Task BalanceSheet_CheckedVouchers_CurrentBranch_NoPrevious_ClosingOption_NoFilterSort()
        {
            // Arrange & Act
            var balanceSheet = await GetBalSheet_CheckedVouchers_CurrentBranch_NoPrevious_ClosingOption_NoFilterSort();

            // Assert
            Assert.That(balanceSheet, Is.Not.Null);
        }

        [Test]
        public async Task BalanceSheet_CheckedVouchers_CurrentBranch_ByCenter_NoPrevious_ClosingOption_NoFilterSort()
        {
            // Arrange & Act
            var balanceSheet = await GetBalSheet_CheckedVouchers_CurrentBranch_NoPrevious_ClosingOption_NoFilterSort(
                TestCenterId);

            // Assert
            Assert.That(balanceSheet, Is.Not.Null);
        }

        [Test]
        public async Task BalanceSheet_CheckedVouchers_CurrentBranch_ByProject_NoPrevious_ClosingOption_NoFilterSort()
        {
            // Arrange & Act
            var balanceSheet = await GetBalSheet_CheckedVouchers_CurrentBranch_NoPrevious_ClosingOption_NoFilterSort(
                null, TestProjectId);

            // Assert
            Assert.That(balanceSheet, Is.Not.Null);
        }

        [Test]
        public async Task BalanceSheet_CheckedVouchers_CurrentBranch_ByCenterProject_NoPrevious_ClosingOption_NoFilterSort()
        {
            // Arrange & Act
            var balanceSheet = await GetBalSheet_CheckedVouchers_CurrentBranch_NoPrevious_ClosingOption_NoFilterSort(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(balanceSheet, Is.Not.Null);
        }

        #endregion

        #region FinalizedVouchers_CurrentBranch_NoPrevious_ClosingOption_NoFilterSort

        [Test]
        public async Task BalanceSheet_FinalizedVouchers_CurrentBranch_NoPrevious_ClosingOption_NoFilterSort()
        {
            // Arrange & Act
            var balanceSheet = await GetBalSheet_FinalizedVouchers_CurrentBranch_NoPrevious_ClosingOption_NoFilterSort();

            // Assert
            Assert.That(balanceSheet, Is.Not.Null);
        }

        [Test]
        public async Task BalanceSheet_FinalizedVouchers_CurrentBranch_ByCenter_NoPrevious_ClosingOption_NoFilterSort()
        {
            // Arrange & Act
            var balanceSheet = await GetBalSheet_FinalizedVouchers_CurrentBranch_NoPrevious_ClosingOption_NoFilterSort(
                TestCenterId);

            // Assert
            Assert.That(balanceSheet, Is.Not.Null);
        }

        [Test]
        public async Task BalanceSheet_FinalizedVouchers_CurrentBranch_ByProject_NoPrevious_ClosingOption_NoFilterSort()
        {
            // Arrange & Act
            var balanceSheet = await GetBalSheet_FinalizedVouchers_CurrentBranch_NoPrevious_ClosingOption_NoFilterSort(
                null, TestProjectId);

            // Assert
            Assert.That(balanceSheet, Is.Not.Null);
        }

        [Test]
        public async Task BalanceSheet_FinalizedVouchers_CurrentBranch_ByCenterProject_NoPrevious_ClosingOption_NoFilterSort()
        {
            // Arrange & Act
            var balanceSheet = await GetBalSheet_FinalizedVouchers_CurrentBranch_NoPrevious_ClosingOption_NoFilterSort(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(balanceSheet, Is.Not.Null);
        }

        #endregion

        #region ConfirmedVouchers_CurrentBranch_NoPrevious_ClosingOption_NoFilterSort

        [Test]
        public async Task BalanceSheet_ConfirmedVouchers_CurrentBranch_NoPrevious_ClosingOption_NoFilterSort()
        {
            // Arrange & Act
            var balanceSheet = await GetBalSheet_ConfirmedVouchers_CurrentBranch_NoPrevious_ClosingOption_NoFilterSort();

            // Assert
            Assert.That(balanceSheet, Is.Not.Null);
        }

        [Test]
        public async Task BalanceSheet_ConfirmedVouchers_CurrentBranch_ByCenter_NoPrevious_ClosingOption_NoFilterSort()
        {
            // Arrange & Act
            var balanceSheet = await GetBalSheet_ConfirmedVouchers_CurrentBranch_NoPrevious_ClosingOption_NoFilterSort(
                TestCenterId);

            // Assert
            Assert.That(balanceSheet, Is.Not.Null);
        }

        [Test]
        public async Task BalanceSheet_ConfirmedVouchers_CurrentBranch_ByProject_NoPrevious_ClosingOption_NoFilterSort()
        {
            // Arrange & Act
            var balanceSheet = await GetBalSheet_ConfirmedVouchers_CurrentBranch_NoPrevious_ClosingOption_NoFilterSort(
                null, TestProjectId);

            // Assert
            Assert.That(balanceSheet, Is.Not.Null);
        }

        [Test]
        public async Task BalanceSheet_ConfirmedVouchers_CurrentBranch_ByCenterProject_NoPrevious_ClosingOption_NoFilterSort()
        {
            // Arrange & Act
            var balanceSheet = await GetBalSheet_ConfirmedVouchers_CurrentBranch_NoPrevious_ClosingOption_NoFilterSort(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(balanceSheet, Is.Not.Null);
        }

        #endregion

        #region ApprovedVouchers_CurrentBranch_NoPrevious_ClosingOption_NoFilterSort

        [Test]
        public async Task BalanceSheet_ApprovedVouchers_CurrentBranch_NoPrevious_ClosingOption_NoFilterSort()
        {
            // Arrange & Act
            var balanceSheet = await GetBalSheet_ApprovedVouchers_CurrentBranch_NoPrevious_ClosingOption_NoFilterSort();

            // Assert
            Assert.That(balanceSheet, Is.Not.Null);
        }

        [Test]
        public async Task BalanceSheet_ApprovedVouchers_CurrentBranch_ByCenter_NoPrevious_ClosingOption_NoFilterSort()
        {
            // Arrange & Act
            var balanceSheet = await GetBalSheet_ApprovedVouchers_CurrentBranch_NoPrevious_ClosingOption_NoFilterSort(
                TestCenterId);

            // Assert
            Assert.That(balanceSheet, Is.Not.Null);
        }

        [Test]
        public async Task BalanceSheet_ApprovedVouchers_CurrentBranch_ByProject_NoPrevious_ClosingOption_NoFilterSort()
        {
            // Arrange & Act
            var balanceSheet = await GetBalSheet_ApprovedVouchers_CurrentBranch_NoPrevious_ClosingOption_NoFilterSort(
                null, TestProjectId);

            // Assert
            Assert.That(balanceSheet, Is.Not.Null);
        }

        [Test]
        public async Task BalanceSheet_ApprovedVouchers_CurrentBranch_ByCenterProject_NoPrevious_ClosingOption_NoFilterSort()
        {
            // Arrange & Act
            var balanceSheet = await GetBalSheet_ApprovedVouchers_CurrentBranch_NoPrevious_ClosingOption_NoFilterSort(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(balanceSheet, Is.Not.Null);
        }

        #endregion

        #region AllVouchers_CurrentBranch_NoPrevious_ClosingOption_NoFilterSort

        [Test]
        public async Task BalanceSheet_AllVouchers_CurrentBranch_NoPrevious_ClosingOption_NoFilterSort()
        {
            // Arrange & Act
            var balanceSheet = await GetBalSheet_AllVouchers_CurrentBranch_NoPrevious_ClosingOption_NoFilterSort();

            // Assert
            Assert.That(balanceSheet, Is.Not.Null);
        }

        [Test]
        public async Task BalanceSheet_AllVouchers_CurrentBranch_ByCenter_NoPrevious_ClosingOption_NoFilterSort()
        {
            // Arrange & Act
            var balanceSheet = await GetBalSheet_AllVouchers_CurrentBranch_NoPrevious_ClosingOption_NoFilterSort(
                TestCenterId);

            // Assert
            Assert.That(balanceSheet, Is.Not.Null);
        }

        [Test]
        public async Task BalanceSheet_AllVouchers_CurrentBranch_ByProject_NoPrevious_ClosingOption_NoFilterSort()
        {
            // Arrange & Act
            var balanceSheet = await GetBalSheet_AllVouchers_CurrentBranch_NoPrevious_ClosingOption_NoFilterSort(
                null, TestProjectId);

            // Assert
            Assert.That(balanceSheet, Is.Not.Null);
        }

        [Test]
        public async Task BalanceSheet_AllVouchers_CurrentBranch_ByCenterProject_NoPrevious_ClosingOption_NoFilterSort()
        {
            // Arrange & Act
            var balanceSheet = await GetBalSheet_AllVouchers_CurrentBranch_NoPrevious_ClosingOption_NoFilterSort(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(balanceSheet, Is.Not.Null);
        }

        #endregion

        #region CheckedVouchers_CurrentAndChildren_NoPrevious_ClosingOption_NoFilterSort

        [Test]
        public async Task BalanceSheet_CheckedVouchers_CurrentAndChildren_NoPrevious_ClosingOption_NoFilterSort()
        {
            // Arrange & Act
            var balanceSheet = await GetBalSheet_CheckedVouchers_CurrentAndChildren_NoPrevious_ClosingOption_NoFilterSort();

            // Assert
            Assert.That(balanceSheet, Is.Not.Null);
        }

        [Test]
        public async Task BalanceSheet_CheckedVouchers_CurrentAndChildren_ByCenter_NoPrevious_ClosingOption_NoFilterSort()
        {
            // Arrange & Act
            var balanceSheet = await GetBalSheet_CheckedVouchers_CurrentAndChildren_NoPrevious_ClosingOption_NoFilterSort(
                TestCenterId);

            // Assert
            Assert.That(balanceSheet, Is.Not.Null);
        }

        [Test]
        public async Task BalanceSheet_CheckedVouchers_CurrentAndChildren_ByProject_NoPrevious_ClosingOption_NoFilterSort()
        {
            // Arrange & Act
            var balanceSheet = await GetBalSheet_CheckedVouchers_CurrentAndChildren_NoPrevious_ClosingOption_NoFilterSort(
                null, TestProjectId);

            // Assert
            Assert.That(balanceSheet, Is.Not.Null);
        }

        [Test]
        public async Task BalanceSheet_CheckedVouchers_CurrentAndChildren_ByCenterProject_NoPrevious_ClosingOption_NoFilterSort()
        {
            // Arrange & Act
            var balanceSheet = await GetBalSheet_CheckedVouchers_CurrentAndChildren_NoPrevious_ClosingOption_NoFilterSort(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(balanceSheet, Is.Not.Null);
        }

        #endregion

        #region FinalizedVouchers_CurrentAndChildren_NoPrevious_ClosingOption_NoFilterSort

        [Test]
        public async Task BalanceSheet_FinalizedVouchers_CurrentAndChildren_NoPrevious_ClosingOption_NoFilterSort()
        {
            // Arrange & Act
            var balanceSheet = await GetBalSheet_FinalizedVouchers_CurrentAndChildren_NoPrevious_ClosingOption_NoFilterSort();

            // Assert
            Assert.That(balanceSheet, Is.Not.Null);
        }

        [Test]
        public async Task BalanceSheet_FinalizedVouchers_CurrentAndChildren_ByCenter_NoPrevious_ClosingOption_NoFilterSort()
        {
            // Arrange & Act
            var balanceSheet = await GetBalSheet_FinalizedVouchers_CurrentAndChildren_NoPrevious_ClosingOption_NoFilterSort(
                TestCenterId);

            // Assert
            Assert.That(balanceSheet, Is.Not.Null);
        }

        [Test]
        public async Task BalanceSheet_FinalizedVouchers_CurrentAndChildren_ByProject_NoPrevious_ClosingOption_NoFilterSort()
        {
            // Arrange & Act
            var balanceSheet = await GetBalSheet_FinalizedVouchers_CurrentAndChildren_NoPrevious_ClosingOption_NoFilterSort(
                null, TestProjectId);

            // Assert
            Assert.That(balanceSheet, Is.Not.Null);
        }

        [Test]
        public async Task BalanceSheet_FinalizedVouchers_CurrentAndChildren_ByCenterProject_NoPrevious_ClosingOption_NoFilterSort()
        {
            // Arrange & Act
            var balanceSheet = await GetBalSheet_FinalizedVouchers_CurrentAndChildren_NoPrevious_ClosingOption_NoFilterSort(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(balanceSheet, Is.Not.Null);
        }

        #endregion

        #region ConfirmedVouchers_CurrentAndChildren_NoPrevious_ClosingOption_NoFilterSort

        [Test]
        public async Task BalanceSheet_ConfirmedVouchers_CurrentAndChildren_NoPrevious_ClosingOption_NoFilterSort()
        {
            // Arrange & Act
            var balanceSheet = await GetBalSheet_ConfirmedVouchers_CurrentAndChildren_NoPrevious_ClosingOption_NoFilterSort();

            // Assert
            Assert.That(balanceSheet, Is.Not.Null);
        }

        [Test]
        public async Task BalanceSheet_ConfirmedVouchers_CurrentAndChildren_ByCenter_NoPrevious_ClosingOption_NoFilterSort()
        {
            // Arrange & Act
            var balanceSheet = await GetBalSheet_ConfirmedVouchers_CurrentAndChildren_NoPrevious_ClosingOption_NoFilterSort(
                TestCenterId);

            // Assert
            Assert.That(balanceSheet, Is.Not.Null);
        }

        [Test]
        public async Task BalanceSheet_ConfirmedVouchers_CurrentAndChildren_ByProject_NoPrevious_ClosingOption_NoFilterSort()
        {
            // Arrange & Act
            var balanceSheet = await GetBalSheet_ConfirmedVouchers_CurrentAndChildren_NoPrevious_ClosingOption_NoFilterSort(
                null, TestProjectId);

            // Assert
            Assert.That(balanceSheet, Is.Not.Null);
        }

        [Test]
        public async Task BalanceSheet_ConfirmedVouchers_CurrentAndChildren_ByCenterProject_NoPrevious_ClosingOption_NoFilterSort()
        {
            // Arrange & Act
            var balanceSheet = await GetBalSheet_ConfirmedVouchers_CurrentAndChildren_NoPrevious_ClosingOption_NoFilterSort(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(balanceSheet, Is.Not.Null);
        }

        #endregion

        #region ApprovedVouchers_CurrentAndChildren_NoPrevious_ClosingOption_NoFilterSort

        [Test]
        public async Task BalanceSheet_ApprovedVouchers_CurrentAndChildren_NoPrevious_ClosingOption_NoFilterSort()
        {
            // Arrange & Act
            var balanceSheet = await GetBalSheet_ApprovedVouchers_CurrentAndChildren_NoPrevious_ClosingOption_NoFilterSort();

            // Assert
            Assert.That(balanceSheet, Is.Not.Null);
        }

        [Test]
        public async Task BalanceSheet_ApprovedVouchers_CurrentAndChildren_ByCenter_NoPrevious_ClosingOption_NoFilterSort()
        {
            // Arrange & Act
            var balanceSheet = await GetBalSheet_ApprovedVouchers_CurrentAndChildren_NoPrevious_ClosingOption_NoFilterSort(
                TestCenterId);

            // Assert
            Assert.That(balanceSheet, Is.Not.Null);
        }

        [Test]
        public async Task BalanceSheet_ApprovedVouchers_CurrentAndChildren_ByProject_NoPrevious_ClosingOption_NoFilterSort()
        {
            // Arrange & Act
            var balanceSheet = await GetBalSheet_ApprovedVouchers_CurrentAndChildren_NoPrevious_ClosingOption_NoFilterSort(
                null, TestProjectId);

            // Assert
            Assert.That(balanceSheet, Is.Not.Null);
        }

        [Test]
        public async Task BalanceSheet_ApprovedVouchers_CurrentAndChildren_ByCenterProject_NoPrevious_ClosingOption_NoFilterSort()
        {
            // Arrange & Act
            var balanceSheet = await GetBalSheet_ApprovedVouchers_CurrentAndChildren_NoPrevious_ClosingOption_NoFilterSort(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(balanceSheet, Is.Not.Null);
        }

        #endregion

        #region AllVouchers_CurrentAndChildren_NoPrevious_ClosingOption_NoFilterSort

        [Test]
        public async Task BalanceSheet_AllVouchers_CurrentAndChildren_NoPrevious_ClosingOption_NoFilterSort()
        {
            // Arrange & Act
            var balanceSheet = await GetBalSheet_AllVouchers_CurrentAndChildren_NoPrevious_ClosingOption_NoFilterSort();

            // Assert
            Assert.That(balanceSheet, Is.Not.Null);
        }

        [Test]
        public async Task BalanceSheet_AllVouchers_CurrentAndChildren_ByCenter_NoPrevious_ClosingOption_NoFilterSort()
        {
            // Arrange & Act
            var balanceSheet = await GetBalSheet_AllVouchers_CurrentAndChildren_NoPrevious_ClosingOption_NoFilterSort(
                TestCenterId);

            // Assert
            Assert.That(balanceSheet, Is.Not.Null);
        }

        [Test]
        public async Task BalanceSheet_AllVouchers_CurrentAndChildren_ByProject_NoPrevious_ClosingOption_NoFilterSort()
        {
            // Arrange & Act
            var balanceSheet = await GetBalSheet_AllVouchers_CurrentAndChildren_NoPrevious_ClosingOption_NoFilterSort(
                null, TestProjectId);

            // Assert
            Assert.That(balanceSheet, Is.Not.Null);
        }

        [Test]
        public async Task BalanceSheet_AllVouchers_CurrentAndChildren_ByCenterProject_NoPrevious_ClosingOption_NoFilterSort()
        {
            // Arrange & Act
            var balanceSheet = await GetBalSheet_AllVouchers_CurrentAndChildren_NoPrevious_ClosingOption_NoFilterSort(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(balanceSheet, Is.Not.Null);
        }

        #endregion

        #region CheckedVouchers_CurrentBranch_WithPrevious_NoOptions_NoFilterSort

        [Test]
        public async Task BalanceSheet_CheckedVouchers_CurrentBranch_WithPrevious_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balanceSheet = await GetBalSheet_CheckedVouchers_CurrentBranch_WithPrevious_NoOptions_NoFilterSort();

            // Assert
            Assert.That(balanceSheet, Is.Not.Null);
        }

        [Test]
        public async Task BalanceSheet_CheckedVouchers_CurrentBranch_ByCenter_WithPrevious_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balanceSheet = await GetBalSheet_CheckedVouchers_CurrentBranch_WithPrevious_NoOptions_NoFilterSort(
                TestCenterId);

            // Assert
            Assert.That(balanceSheet, Is.Not.Null);
        }

        [Test]
        public async Task BalanceSheet_CheckedVouchers_CurrentBranch_ByProject_WithPrevious_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balanceSheet = await GetBalSheet_CheckedVouchers_CurrentBranch_WithPrevious_NoOptions_NoFilterSort(
                null, TestProjectId);

            // Assert
            Assert.That(balanceSheet, Is.Not.Null);
        }

        [Test]
        public async Task BalanceSheet_CheckedVouchers_CurrentBranch_ByCenterProject_WithPrevious_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balanceSheet = await GetBalSheet_CheckedVouchers_CurrentBranch_WithPrevious_NoOptions_NoFilterSort(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(balanceSheet, Is.Not.Null);
        }

        #endregion

        #region FinalizedVouchers_CurrentBranch_WithPrevious_NoOptions_NoFilterSort

        [Test]
        public async Task BalanceSheet_FinalizedVouchers_CurrentBranch_WithPrevious_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balanceSheet = await GetBalSheet_FinalizedVouchers_CurrentBranch_WithPrevious_NoOptions_NoFilterSort();

            // Assert
            Assert.That(balanceSheet, Is.Not.Null);
        }

        [Test]
        public async Task BalanceSheet_FinalizedVouchers_CurrentBranch_ByCenter_WithPrevious_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balanceSheet = await GetBalSheet_FinalizedVouchers_CurrentBranch_WithPrevious_NoOptions_NoFilterSort(
                TestCenterId);

            // Assert
            Assert.That(balanceSheet, Is.Not.Null);
        }

        [Test]
        public async Task BalanceSheet_FinalizedVouchers_CurrentBranch_ByProject_WithPrevious_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balanceSheet = await GetBalSheet_FinalizedVouchers_CurrentBranch_WithPrevious_NoOptions_NoFilterSort(
                null, TestProjectId);

            // Assert
            Assert.That(balanceSheet, Is.Not.Null);
        }

        [Test]
        public async Task BalanceSheet_FinalizedVouchers_CurrentBranch_ByCenterProject_WithPrevious_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balanceSheet = await GetBalSheet_FinalizedVouchers_CurrentBranch_WithPrevious_NoOptions_NoFilterSort(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(balanceSheet, Is.Not.Null);
        }

        #endregion

        #region ConfirmedVouchers_CurrentBranch_WithPrevious_NoOptions_NoFilterSort

        [Test]
        public async Task BalanceSheet_ConfirmedVouchers_CurrentBranch_WithPrevious_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balanceSheet = await GetBalSheet_ConfirmedVouchers_CurrentBranch_WithPrevious_NoOptions_NoFilterSort();

            // Assert
            Assert.That(balanceSheet, Is.Not.Null);
        }

        [Test]
        public async Task BalanceSheet_ConfirmedVouchers_CurrentBranch_ByCenter_WithPrevious_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balanceSheet = await GetBalSheet_ConfirmedVouchers_CurrentBranch_WithPrevious_NoOptions_NoFilterSort(
                TestCenterId);

            // Assert
            Assert.That(balanceSheet, Is.Not.Null);
        }

        [Test]
        public async Task BalanceSheet_ConfirmedVouchers_CurrentBranch_ByProject_WithPrevious_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balanceSheet = await GetBalSheet_ConfirmedVouchers_CurrentBranch_WithPrevious_NoOptions_NoFilterSort(
                null, TestProjectId);

            // Assert
            Assert.That(balanceSheet, Is.Not.Null);
        }

        [Test]
        public async Task BalanceSheet_ConfirmedVouchers_CurrentBranch_ByCenterProject_WithPrevious_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balanceSheet = await GetBalSheet_ConfirmedVouchers_CurrentBranch_WithPrevious_NoOptions_NoFilterSort(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(balanceSheet, Is.Not.Null);
        }

        #endregion

        #region ApprovedVouchers_CurrentBranch_WithPrevious_NoOptions_NoFilterSort

        [Test]
        public async Task BalanceSheet_ApprovedVouchers_CurrentBranch_WithPrevious_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balanceSheet = await GetBalSheet_ApprovedVouchers_CurrentBranch_WithPrevious_NoOptions_NoFilterSort();

            // Assert
            Assert.That(balanceSheet, Is.Not.Null);
        }

        [Test]
        public async Task BalanceSheet_ApprovedVouchers_CurrentBranch_ByCenter_WithPrevious_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balanceSheet = await GetBalSheet_ApprovedVouchers_CurrentBranch_WithPrevious_NoOptions_NoFilterSort(
                TestCenterId);

            // Assert
            Assert.That(balanceSheet, Is.Not.Null);
        }

        [Test]
        public async Task BalanceSheet_ApprovedVouchers_CurrentBranch_ByProject_WithPrevious_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balanceSheet = await GetBalSheet_ApprovedVouchers_CurrentBranch_WithPrevious_NoOptions_NoFilterSort(
                null, TestProjectId);

            // Assert
            Assert.That(balanceSheet, Is.Not.Null);
        }

        [Test]
        public async Task BalanceSheet_ApprovedVouchers_CurrentBranch_ByCenterProject_WithPrevious_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balanceSheet = await GetBalSheet_ApprovedVouchers_CurrentBranch_WithPrevious_NoOptions_NoFilterSort(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(balanceSheet, Is.Not.Null);
        }

        #endregion

        #region AllVouchers_CurrentBranch_WithPrevious_NoOptions_NoFilterSort

        [Test]
        public async Task BalanceSheet_AllVouchers_CurrentBranch_WithPrevious_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balanceSheet = await GetBalSheet_AllVouchers_CurrentBranch_WithPrevious_NoOptions_NoFilterSort();

            // Assert
            Assert.That(balanceSheet, Is.Not.Null);
        }

        [Test]
        public async Task BalanceSheet_AllVouchers_CurrentBranch_ByCenter_WithPrevious_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balanceSheet = await GetBalSheet_AllVouchers_CurrentBranch_WithPrevious_NoOptions_NoFilterSort(
                TestCenterId);

            // Assert
            Assert.That(balanceSheet, Is.Not.Null);
        }

        [Test]
        public async Task BalanceSheet_AllVouchers_CurrentBranch_ByProject_WithPrevious_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balanceSheet = await GetBalSheet_AllVouchers_CurrentBranch_WithPrevious_NoOptions_NoFilterSort(
                null, TestProjectId);

            // Assert
            Assert.That(balanceSheet, Is.Not.Null);
        }

        [Test]
        public async Task BalanceSheet_AllVouchers_CurrentBranch_ByCenterProject_WithPrevious_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balanceSheet = await GetBalSheet_AllVouchers_CurrentBranch_WithPrevious_NoOptions_NoFilterSort(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(balanceSheet, Is.Not.Null);
        }

        #endregion

        #region CheckedVouchers_CurrentAndChildren_WithPrevious_NoOptions_NoFilterSort

        [Test]
        public async Task BalanceSheet_CheckedVouchers_CurrentAndChildren_WithPrevious_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balanceSheet = await GetBalSheet_CheckedVouchers_CurrentAndChildren_WithPrevious_NoOptions_NoFilterSort();

            // Assert
            Assert.That(balanceSheet, Is.Not.Null);
        }

        [Test]
        public async Task BalanceSheet_CheckedVouchers_CurrentAndChildren_ByCenter_WithPrevious_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balanceSheet = await GetBalSheet_CheckedVouchers_CurrentAndChildren_WithPrevious_NoOptions_NoFilterSort(
                TestCenterId);

            // Assert
            Assert.That(balanceSheet, Is.Not.Null);
        }

        [Test]
        public async Task BalanceSheet_CheckedVouchers_CurrentAndChildren_ByProject_WithPrevious_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balanceSheet = await GetBalSheet_CheckedVouchers_CurrentAndChildren_WithPrevious_NoOptions_NoFilterSort(
                null, TestProjectId);

            // Assert
            Assert.That(balanceSheet, Is.Not.Null);
        }

        [Test]
        public async Task BalanceSheet_CheckedVouchers_CurrentAndChildren_ByCenterProject_WithPrevious_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balanceSheet = await GetBalSheet_CheckedVouchers_CurrentAndChildren_WithPrevious_NoOptions_NoFilterSort(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(balanceSheet, Is.Not.Null);
        }

        #endregion

        #region FinalizedVouchers_CurrentAndChildren_WithPrevious_NoOptions_NoFilterSort

        [Test]
        public async Task BalanceSheet_FinalizedVouchers_CurrentAndChildren_WithPrevious_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balanceSheet = await GetBalSheet_FinalizedVouchers_CurrentAndChildren_WithPrevious_NoOptions_NoFilterSort();

            // Assert
            Assert.That(balanceSheet, Is.Not.Null);
        }

        [Test]
        public async Task BalanceSheet_FinalizedVouchers_CurrentAndChildren_ByCenter_WithPrevious_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balanceSheet = await GetBalSheet_FinalizedVouchers_CurrentAndChildren_WithPrevious_NoOptions_NoFilterSort(
                TestCenterId);

            // Assert
            Assert.That(balanceSheet, Is.Not.Null);
        }

        [Test]
        public async Task BalanceSheet_FinalizedVouchers_CurrentAndChildren_ByProject_WithPrevious_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balanceSheet = await GetBalSheet_FinalizedVouchers_CurrentAndChildren_WithPrevious_NoOptions_NoFilterSort(
                null, TestProjectId);

            // Assert
            Assert.That(balanceSheet, Is.Not.Null);
        }

        [Test]
        public async Task BalanceSheet_FinalizedVouchers_CurrentAndChildren_ByCenterProject_WithPrevious_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balanceSheet = await GetBalSheet_FinalizedVouchers_CurrentAndChildren_WithPrevious_NoOptions_NoFilterSort(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(balanceSheet, Is.Not.Null);
        }

        #endregion

        #region ConfirmedVouchers_CurrentAndChildren_WithPrevious_NoOptions_NoFilterSort

        [Test]
        public async Task BalanceSheet_ConfirmedVouchers_CurrentAndChildren_WithPrevious_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balanceSheet = await GetBalSheet_ConfirmedVouchers_CurrentAndChildren_WithPrevious_NoOptions_NoFilterSort();

            // Assert
            Assert.That(balanceSheet, Is.Not.Null);
        }

        [Test]
        public async Task BalanceSheet_ConfirmedVouchers_CurrentAndChildren_ByCenter_WithPrevious_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balanceSheet = await GetBalSheet_ConfirmedVouchers_CurrentAndChildren_WithPrevious_NoOptions_NoFilterSort(
                TestCenterId);

            // Assert
            Assert.That(balanceSheet, Is.Not.Null);
        }

        [Test]
        public async Task BalanceSheet_ConfirmedVouchers_CurrentAndChildren_ByProject_WithPrevious_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balanceSheet = await GetBalSheet_ConfirmedVouchers_CurrentAndChildren_WithPrevious_NoOptions_NoFilterSort(
                null, TestProjectId);

            // Assert
            Assert.That(balanceSheet, Is.Not.Null);
        }

        [Test]
        public async Task BalanceSheet_ConfirmedVouchers_CurrentAndChildren_ByCenterProject_WithPrevious_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balanceSheet = await GetBalSheet_ConfirmedVouchers_CurrentAndChildren_WithPrevious_NoOptions_NoFilterSort(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(balanceSheet, Is.Not.Null);
        }

        #endregion

        #region ApprovedVouchers_CurrentAndChildren_WithPrevious_NoOptions_NoFilterSort

        [Test]
        public async Task BalanceSheet_ApprovedVouchers_CurrentAndChildren_WithPrevious_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balanceSheet = await GetBalSheet_ApprovedVouchers_CurrentAndChildren_WithPrevious_NoOptions_NoFilterSort();

            // Assert
            Assert.That(balanceSheet, Is.Not.Null);
        }

        [Test]
        public async Task BalanceSheet_ApprovedVouchers_CurrentAndChildren_ByCenter_WithPrevious_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balanceSheet = await GetBalSheet_ApprovedVouchers_CurrentAndChildren_WithPrevious_NoOptions_NoFilterSort(
                TestCenterId);

            // Assert
            Assert.That(balanceSheet, Is.Not.Null);
        }

        [Test]
        public async Task BalanceSheet_ApprovedVouchers_CurrentAndChildren_ByProject_WithPrevious_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balanceSheet = await GetBalSheet_ApprovedVouchers_CurrentAndChildren_WithPrevious_NoOptions_NoFilterSort(
                null, TestProjectId);

            // Assert
            Assert.That(balanceSheet, Is.Not.Null);
        }

        [Test]
        public async Task BalanceSheet_ApprovedVouchers_CurrentAndChildren_ByCenterProject_WithPrevious_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balanceSheet = await GetBalSheet_ApprovedVouchers_CurrentAndChildren_WithPrevious_NoOptions_NoFilterSort(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(balanceSheet, Is.Not.Null);
        }

        #endregion

        #region AllVouchers_CurrentAndChildren_WithPrevious_NoOptions_NoFilterSort

        [Test]
        public async Task BalanceSheet_AllVouchers_CurrentAndChildren_WithPrevious_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balanceSheet = await GetBalSheet_AllVouchers_CurrentAndChildren_WithPrevious_NoOptions_NoFilterSort();

            // Assert
            Assert.That(balanceSheet, Is.Not.Null);
        }

        [Test]
        public async Task BalanceSheet_AllVouchers_CurrentAndChildren_ByCenter_WithPrevious_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balanceSheet = await GetBalSheet_AllVouchers_CurrentAndChildren_WithPrevious_NoOptions_NoFilterSort(
                TestCenterId);

            // Assert
            Assert.That(balanceSheet, Is.Not.Null);
        }

        [Test]
        public async Task BalanceSheet_AllVouchers_CurrentAndChildren_ByProject_WithPrevious_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balanceSheet = await GetBalSheet_AllVouchers_CurrentAndChildren_WithPrevious_NoOptions_NoFilterSort(
                null, TestProjectId);

            // Assert
            Assert.That(balanceSheet, Is.Not.Null);
        }

        [Test]
        public async Task BalanceSheet_AllVouchers_CurrentAndChildren_ByCenterProject_WithPrevious_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balanceSheet = await GetBalSheet_AllVouchers_CurrentAndChildren_WithPrevious_NoOptions_NoFilterSort(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(balanceSheet, Is.Not.Null);
        }

        #endregion

        #region CheckedVouchers_CurrentBranch_WithPrevious_ClosingOption_NoFilterSort

        [Test]
        public async Task BalanceSheet_CheckedVouchers_CurrentBranch_WithPrevious_ClosingOption_NoFilterSort()
        {
            // Arrange & Act
            var balanceSheet = await GetBalSheet_CheckedVouchers_CurrentBranch_WithPrevious_ClosingOption_NoFilterSort();

            // Assert
            Assert.That(balanceSheet, Is.Not.Null);
        }

        [Test]
        public async Task BalanceSheet_CheckedVouchers_CurrentBranch_ByCenter_WithPrevious_ClosingOption_NoFilterSort()
        {
            // Arrange & Act
            var balanceSheet = await GetBalSheet_CheckedVouchers_CurrentBranch_WithPrevious_ClosingOption_NoFilterSort(
                TestCenterId);

            // Assert
            Assert.That(balanceSheet, Is.Not.Null);
        }

        [Test]
        public async Task BalanceSheet_CheckedVouchers_CurrentBranch_ByProject_WithPrevious_ClosingOption_NoFilterSort()
        {
            // Arrange & Act
            var balanceSheet = await GetBalSheet_CheckedVouchers_CurrentBranch_WithPrevious_ClosingOption_NoFilterSort(
                null, TestProjectId);

            // Assert
            Assert.That(balanceSheet, Is.Not.Null);
        }

        [Test]
        public async Task BalanceSheet_CheckedVouchers_CurrentBranch_ByCenterProject_WithPrevious_ClosingOption_NoFilterSort()
        {
            // Arrange & Act
            var balanceSheet = await GetBalSheet_CheckedVouchers_CurrentBranch_WithPrevious_ClosingOption_NoFilterSort(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(balanceSheet, Is.Not.Null);
        }

        #endregion

        #region FinalizedVouchers_CurrentBranch_WithPrevious_ClosingOption_NoFilterSort

        [Test]
        public async Task BalanceSheet_FinalizedVouchers_CurrentBranch_WithPrevious_ClosingOption_NoFilterSort()
        {
            // Arrange & Act
            var balanceSheet = await GetBalSheet_FinalizedVouchers_CurrentBranch_WithPrevious_ClosingOption_NoFilterSort();

            // Assert
            Assert.That(balanceSheet, Is.Not.Null);
        }

        [Test]
        public async Task BalanceSheet_FinalizedVouchers_CurrentBranch_ByCenter_WithPrevious_ClosingOption_NoFilterSort()
        {
            // Arrange & Act
            var balanceSheet = await GetBalSheet_FinalizedVouchers_CurrentBranch_WithPrevious_ClosingOption_NoFilterSort(
                TestCenterId);

            // Assert
            Assert.That(balanceSheet, Is.Not.Null);
        }

        [Test]
        public async Task BalanceSheet_FinalizedVouchers_CurrentBranch_ByProject_WithPrevious_ClosingOption_NoFilterSort()
        {
            // Arrange & Act
            var balanceSheet = await GetBalSheet_FinalizedVouchers_CurrentBranch_WithPrevious_ClosingOption_NoFilterSort(
                null, TestProjectId);

            // Assert
            Assert.That(balanceSheet, Is.Not.Null);
        }

        [Test]
        public async Task BalanceSheet_FinalizedVouchers_CurrentBranch_ByCenterProject_WithPrevious_ClosingOption_NoFilterSort()
        {
            // Arrange & Act
            var balanceSheet = await GetBalSheet_FinalizedVouchers_CurrentBranch_WithPrevious_ClosingOption_NoFilterSort(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(balanceSheet, Is.Not.Null);
        }

        #endregion

        #region ConfirmedVouchers_CurrentBranch_WithPrevious_ClosingOption_NoFilterSort

        [Test]
        public async Task BalanceSheet_ConfirmedVouchers_CurrentBranch_WithPrevious_ClosingOption_NoFilterSort()
        {
            // Arrange & Act
            var balanceSheet = await GetBalSheet_ConfirmedVouchers_CurrentBranch_WithPrevious_ClosingOption_NoFilterSort();

            // Assert
            Assert.That(balanceSheet, Is.Not.Null);
        }

        [Test]
        public async Task BalanceSheet_ConfirmedVouchers_CurrentBranch_ByCenter_WithPrevious_ClosingOption_NoFilterSort()
        {
            // Arrange & Act
            var balanceSheet = await GetBalSheet_ConfirmedVouchers_CurrentBranch_WithPrevious_ClosingOption_NoFilterSort(
                TestCenterId);

            // Assert
            Assert.That(balanceSheet, Is.Not.Null);
        }

        [Test]
        public async Task BalanceSheet_ConfirmedVouchers_CurrentBranch_ByProject_WithPrevious_ClosingOption_NoFilterSort()
        {
            // Arrange & Act
            var balanceSheet = await GetBalSheet_ConfirmedVouchers_CurrentBranch_WithPrevious_ClosingOption_NoFilterSort(
                null, TestProjectId);

            // Assert
            Assert.That(balanceSheet, Is.Not.Null);
        }

        [Test]
        public async Task BalanceSheet_ConfirmedVouchers_CurrentBranch_ByCenterProject_WithPrevious_ClosingOption_NoFilterSort()
        {
            // Arrange & Act
            var balanceSheet = await GetBalSheet_ConfirmedVouchers_CurrentBranch_WithPrevious_ClosingOption_NoFilterSort(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(balanceSheet, Is.Not.Null);
        }

        #endregion

        #region ApprovedVouchers_CurrentBranch_WithPrevious_ClosingOption_NoFilterSort

        [Test]
        public async Task BalanceSheet_ApprovedVouchers_CurrentBranch_WithPrevious_ClosingOption_NoFilterSort()
        {
            // Arrange & Act
            var balanceSheet = await GetBalSheet_ApprovedVouchers_CurrentBranch_WithPrevious_ClosingOption_NoFilterSort();

            // Assert
            Assert.That(balanceSheet, Is.Not.Null);
        }

        [Test]
        public async Task BalanceSheet_ApprovedVouchers_CurrentBranch_ByCenter_WithPrevious_ClosingOption_NoFilterSort()
        {
            // Arrange & Act
            var balanceSheet = await GetBalSheet_ApprovedVouchers_CurrentBranch_WithPrevious_ClosingOption_NoFilterSort(
                TestCenterId);

            // Assert
            Assert.That(balanceSheet, Is.Not.Null);
        }

        [Test]
        public async Task BalanceSheet_ApprovedVouchers_CurrentBranch_ByProject_WithPrevious_ClosingOption_NoFilterSort()
        {
            // Arrange & Act
            var balanceSheet = await GetBalSheet_ApprovedVouchers_CurrentBranch_WithPrevious_ClosingOption_NoFilterSort(
                null, TestProjectId);

            // Assert
            Assert.That(balanceSheet, Is.Not.Null);
        }

        [Test]
        public async Task BalanceSheet_ApprovedVouchers_CurrentBranch_ByCenterProject_WithPrevious_ClosingOption_NoFilterSort()
        {
            // Arrange & Act
            var balanceSheet = await GetBalSheet_ApprovedVouchers_CurrentBranch_WithPrevious_ClosingOption_NoFilterSort(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(balanceSheet, Is.Not.Null);
        }

        #endregion

        #region AllVouchers_CurrentBranch_WithPrevious_ClosingOption_NoFilterSort

        [Test]
        public async Task BalanceSheet_AllVouchers_CurrentBranch_WithPrevious_ClosingOption_NoFilterSort()
        {
            // Arrange & Act
            var balanceSheet = await GetBalSheet_AllVouchers_CurrentBranch_WithPrevious_ClosingOption_NoFilterSort();

            // Assert
            Assert.That(balanceSheet, Is.Not.Null);
        }

        [Test]
        public async Task BalanceSheet_AllVouchers_CurrentBranch_ByCenter_WithPrevious_ClosingOption_NoFilterSort()
        {
            // Arrange & Act
            var balanceSheet = await GetBalSheet_AllVouchers_CurrentBranch_WithPrevious_ClosingOption_NoFilterSort(
                TestCenterId);

            // Assert
            Assert.That(balanceSheet, Is.Not.Null);
        }

        [Test]
        public async Task BalanceSheet_AllVouchers_CurrentBranch_ByProject_WithPrevious_ClosingOption_NoFilterSort()
        {
            // Arrange & Act
            var balanceSheet = await GetBalSheet_AllVouchers_CurrentBranch_WithPrevious_ClosingOption_NoFilterSort(
                null, TestProjectId);

            // Assert
            Assert.That(balanceSheet, Is.Not.Null);
        }

        [Test]
        public async Task BalanceSheet_AllVouchers_CurrentBranch_ByCenterProject_WithPrevious_ClosingOption_NoFilterSort()
        {
            // Arrange & Act
            var balanceSheet = await GetBalSheet_AllVouchers_CurrentBranch_WithPrevious_ClosingOption_NoFilterSort(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(balanceSheet, Is.Not.Null);
        }

        #endregion

        #region CheckedVouchers_CurrentAndChildren_WithPrevious_ClosingOption_NoFilterSort

        [Test]
        public async Task BalanceSheet_CheckedVouchers_CurrentAndChildren_WithPrevious_ClosingOption_NoFilterSort()
        {
            // Arrange & Act
            var balanceSheet = await GetBalSheet_CheckedVouchers_CurrentAndChildren_WithPrevious_ClosingOption_NoFilterSort();

            // Assert
            Assert.That(balanceSheet, Is.Not.Null);
        }

        [Test]
        public async Task BalanceSheet_CheckedVouchers_CurrentAndChildren_ByCenter_WithPrevious_ClosingOption_NoFilterSort()
        {
            // Arrange & Act
            var balanceSheet = await GetBalSheet_CheckedVouchers_CurrentAndChildren_WithPrevious_ClosingOption_NoFilterSort(
                TestCenterId);

            // Assert
            Assert.That(balanceSheet, Is.Not.Null);
        }

        [Test]
        public async Task BalanceSheet_CheckedVouchers_CurrentAndChildren_ByProject_WithPrevious_ClosingOption_NoFilterSort()
        {
            // Arrange & Act
            var balanceSheet = await GetBalSheet_CheckedVouchers_CurrentAndChildren_WithPrevious_ClosingOption_NoFilterSort(
                null, TestProjectId);

            // Assert
            Assert.That(balanceSheet, Is.Not.Null);
        }

        [Test]
        public async Task BalanceSheet_CheckedVouchers_CurrentAndChildren_ByCenterProject_WithPrevious_ClosingOption_NoFilterSort()
        {
            // Arrange & Act
            var balanceSheet = await GetBalSheet_CheckedVouchers_CurrentAndChildren_WithPrevious_ClosingOption_NoFilterSort(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(balanceSheet, Is.Not.Null);
        }

        #endregion

        #region FinalizedVouchers_CurrentAndChildren_WithPrevious_ClosingOption_NoFilterSort

        [Test]
        public async Task BalanceSheet_FinalizedVouchers_CurrentAndChildren_WithPrevious_ClosingOption_NoFilterSort()
        {
            // Arrange & Act
            var balanceSheet = await GetBalSheet_FinalizedVouchers_CurrentAndChildren_WithPrevious_ClosingOption_NoFilterSort();

            // Assert
            Assert.That(balanceSheet, Is.Not.Null);
        }

        [Test]
        public async Task BalanceSheet_FinalizedVouchers_CurrentAndChildren_ByCenter_WithPrevious_ClosingOption_NoFilterSort()
        {
            // Arrange & Act
            var balanceSheet = await GetBalSheet_FinalizedVouchers_CurrentAndChildren_WithPrevious_ClosingOption_NoFilterSort(
                TestCenterId);

            // Assert
            Assert.That(balanceSheet, Is.Not.Null);
        }

        [Test]
        public async Task BalanceSheet_FinalizedVouchers_CurrentAndChildren_ByProject_WithPrevious_ClosingOption_NoFilterSort()
        {
            // Arrange & Act
            var balanceSheet = await GetBalSheet_FinalizedVouchers_CurrentAndChildren_WithPrevious_ClosingOption_NoFilterSort(
                null, TestProjectId);

            // Assert
            Assert.That(balanceSheet, Is.Not.Null);
        }

        [Test]
        public async Task BalanceSheet_FinalizedVouchers_CurrentAndChildren_ByCenterProject_WithPrevious_ClosingOption_NoFilterSort()
        {
            // Arrange & Act
            var balanceSheet = await GetBalSheet_FinalizedVouchers_CurrentAndChildren_WithPrevious_ClosingOption_NoFilterSort(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(balanceSheet, Is.Not.Null);
        }

        #endregion

        #region ConfirmedVouchers_CurrentAndChildren_WithPrevious_ClosingOption_NoFilterSort

        [Test]
        public async Task BalanceSheet_ConfirmedVouchers_CurrentAndChildren_WithPrevious_ClosingOption_NoFilterSort()
        {
            // Arrange & Act
            var balanceSheet = await GetBalSheet_ConfirmedVouchers_CurrentAndChildren_WithPrevious_ClosingOption_NoFilterSort();

            // Assert
            Assert.That(balanceSheet, Is.Not.Null);
        }

        [Test]
        public async Task BalanceSheet_ConfirmedVouchers_CurrentAndChildren_ByCenter_WithPrevious_ClosingOption_NoFilterSort()
        {
            // Arrange & Act
            var balanceSheet = await GetBalSheet_ConfirmedVouchers_CurrentAndChildren_WithPrevious_ClosingOption_NoFilterSort(
                TestCenterId);

            // Assert
            Assert.That(balanceSheet, Is.Not.Null);
        }

        [Test]
        public async Task BalanceSheet_ConfirmedVouchers_CurrentAndChildren_ByProject_WithPrevious_ClosingOption_NoFilterSort()
        {
            // Arrange & Act
            var balanceSheet = await GetBalSheet_ConfirmedVouchers_CurrentAndChildren_WithPrevious_ClosingOption_NoFilterSort(
                null, TestProjectId);

            // Assert
            Assert.That(balanceSheet, Is.Not.Null);
        }

        [Test]
        public async Task BalanceSheet_ConfirmedVouchers_CurrentAndChildren_ByCenterProject_WithPrevious_ClosingOption_NoFilterSort()
        {
            // Arrange & Act
            var balanceSheet = await GetBalSheet_ConfirmedVouchers_CurrentAndChildren_WithPrevious_ClosingOption_NoFilterSort(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(balanceSheet, Is.Not.Null);
        }

        #endregion

        #region ApprovedVouchers_CurrentAndChildren_WithPrevious_ClosingOption_NoFilterSort

        [Test]
        public async Task BalanceSheet_ApprovedVouchers_CurrentAndChildren_WithPrevious_ClosingOption_NoFilterSort()
        {
            // Arrange & Act
            var balanceSheet = await GetBalSheet_ApprovedVouchers_CurrentAndChildren_WithPrevious_ClosingOption_NoFilterSort();

            // Assert
            Assert.That(balanceSheet, Is.Not.Null);
        }

        [Test]
        public async Task BalanceSheet_ApprovedVouchers_CurrentAndChildren_ByCenter_WithPrevious_ClosingOption_NoFilterSort()
        {
            // Arrange & Act
            var balanceSheet = await GetBalSheet_ApprovedVouchers_CurrentAndChildren_WithPrevious_ClosingOption_NoFilterSort(
                TestCenterId);

            // Assert
            Assert.That(balanceSheet, Is.Not.Null);
        }

        [Test]
        public async Task BalanceSheet_ApprovedVouchers_CurrentAndChildren_ByProject_WithPrevious_ClosingOption_NoFilterSort()
        {
            // Arrange & Act
            var balanceSheet = await GetBalSheet_ApprovedVouchers_CurrentAndChildren_WithPrevious_ClosingOption_NoFilterSort(
                null, TestProjectId);

            // Assert
            Assert.That(balanceSheet, Is.Not.Null);
        }

        [Test]
        public async Task BalanceSheet_ApprovedVouchers_CurrentAndChildren_ByCenterProject_WithPrevious_ClosingOption_NoFilterSort()
        {
            // Arrange & Act
            var balanceSheet = await GetBalSheet_ApprovedVouchers_CurrentAndChildren_WithPrevious_ClosingOption_NoFilterSort(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(balanceSheet, Is.Not.Null);
        }

        #endregion

        #region AllVouchers_CurrentAndChildren_WithPrevious_ClosingOption_NoFilterSort

        [Test]
        public async Task BalanceSheet_AllVouchers_CurrentAndChildren_WithPrevious_ClosingOption_NoFilterSort()
        {
            // Arrange & Act
            var balanceSheet = await GetBalSheet_AllVouchers_CurrentAndChildren_WithPrevious_ClosingOption_NoFilterSort();

            // Assert
            Assert.That(balanceSheet, Is.Not.Null);
        }

        [Test]
        public async Task BalanceSheet_AllVouchers_CurrentAndChildren_ByCenter_WithPrevious_ClosingOption_NoFilterSort()
        {
            // Arrange & Act
            var balanceSheet = await GetBalSheet_AllVouchers_CurrentAndChildren_WithPrevious_ClosingOption_NoFilterSort(
                TestCenterId);

            // Assert
            Assert.That(balanceSheet, Is.Not.Null);
        }

        [Test]
        public async Task BalanceSheet_AllVouchers_CurrentAndChildren_ByProject_WithPrevious_ClosingOption_NoFilterSort()
        {
            // Arrange & Act
            var balanceSheet = await GetBalSheet_AllVouchers_CurrentAndChildren_WithPrevious_ClosingOption_NoFilterSort(
                null, TestProjectId);

            // Assert
            Assert.That(balanceSheet, Is.Not.Null);
        }

        [Test]
        public async Task BalanceSheet_AllVouchers_CurrentAndChildren_ByCenterProject_WithPrevious_ClosingOption_NoFilterSort()
        {
            // Arrange & Act
            var balanceSheet = await GetBalSheet_AllVouchers_CurrentAndChildren_WithPrevious_ClosingOption_NoFilterSort(
                TestCenterId, TestProjectId);

            // Assert
            Assert.That(balanceSheet, Is.Not.Null);
        }

        #endregion

        private void SetUp()
        {
            var repoContext = GetRepositoryContext();
            var sysRepo = GetSystemRepository(repoContext);
            var utility = new ReportDirectUtility(repoContext, sysRepo);
            _repository = new BalanceSheetRepositoryDirect(repoContext, sysRepo, utility);
        }

        #region GetBalSheet_CurrentBranch_NoPrevious_NoOptions_NoFilterSort

        private async Task<BalanceSheetViewModel> GetBalSheet_CheckedVouchers_CurrentBranch_NoPrevious_NoOptions_NoFilterSort(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseCheckedVouchers()
                .Build();
            var parameters = GetTestParameters(gridOptions, false, null, ccenterId, projectId);

            return await _repository.GetBalanceSheetAsync(parameters);
        }

        private async Task<BalanceSheetViewModel> GetBalSheet_FinalizedVouchers_CurrentBranch_NoPrevious_NoOptions_NoFilterSort(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseFinalizedVouchers()
                .Build();
            var parameters = GetTestParameters(gridOptions, false, null, ccenterId, projectId);

            return await _repository.GetBalanceSheetAsync(parameters);
        }

        private async Task<BalanceSheetViewModel> GetBalSheet_ConfirmedVouchers_CurrentBranch_NoPrevious_NoOptions_NoFilterSort(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseConfirmedVouchers()
                .Build();
            var parameters = GetTestParameters(gridOptions, false, null, ccenterId, projectId);

            return await _repository.GetBalanceSheetAsync(parameters);
        }

        private async Task<BalanceSheetViewModel> GetBalSheet_ApprovedVouchers_CurrentBranch_NoPrevious_NoOptions_NoFilterSort(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseApprovedVouchers()
                .Build();
            var parameters = GetTestParameters(gridOptions, false, null, ccenterId, projectId);

            return await _repository.GetBalanceSheetAsync(parameters);
        }

        private async Task<BalanceSheetViewModel> GetBalSheet_AllVouchers_CurrentBranch_NoPrevious_NoOptions_NoFilterSort(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .Build();
            var parameters = GetTestParameters(gridOptions, false, null, ccenterId, projectId);

            return await _repository.GetBalanceSheetAsync(parameters);
        }

        #endregion

        #region GetBalSheet_CurrentAndChildren_NoPrevious_NoOptions_NoFilterSort

        private async Task<BalanceSheetViewModel> GetBalSheet_CheckedVouchers_CurrentAndChildren_NoPrevious_NoOptions_NoFilterSort(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseCheckedVouchers()
                .Build();
            var parameters = GetTestParameters(gridOptions, false, null, ccenterId, projectId);

            return await _repository.GetBalanceSheetAsync(parameters);
        }

        private async Task<BalanceSheetViewModel> GetBalSheet_FinalizedVouchers_CurrentAndChildren_NoPrevious_NoOptions_NoFilterSort(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseFinalizedVouchers()
                .Build();
            var parameters = GetTestParameters(gridOptions, false, null, ccenterId, projectId);

            return await _repository.GetBalanceSheetAsync(parameters);
        }

        private async Task<BalanceSheetViewModel> GetBalSheet_ConfirmedVouchers_CurrentAndChildren_NoPrevious_NoOptions_NoFilterSort(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseConfirmedVouchers()
                .Build();
            var parameters = GetTestParameters(gridOptions, false, null, ccenterId, projectId);

            return await _repository.GetBalanceSheetAsync(parameters);
        }

        private async Task<BalanceSheetViewModel> GetBalSheet_ApprovedVouchers_CurrentAndChildren_NoPrevious_NoOptions_NoFilterSort(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseApprovedVouchers()
                .Build();
            var parameters = GetTestParameters(gridOptions, false, null, ccenterId, projectId);

            return await _repository.GetBalanceSheetAsync(parameters);
        }

        private async Task<BalanceSheetViewModel> GetBalSheet_AllVouchers_CurrentAndChildren_NoPrevious_NoOptions_NoFilterSort(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .Build();
            var parameters = GetTestParameters(gridOptions, false, null, ccenterId, projectId);

            return await _repository.GetBalanceSheetAsync(parameters);
        }

        #endregion

        #region GetBalSheet_CurrentBranch_NoPrevious_ClosingOption_NoFilterSort

        private async Task<BalanceSheetViewModel> GetBalSheet_CheckedVouchers_CurrentBranch_NoPrevious_ClosingOption_NoFilterSort(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseCheckedVouchers()
                .Build();
            var parameters = GetTestParameters(gridOptions, true, null, ccenterId, projectId);

            return await _repository.GetBalanceSheetAsync(parameters);
        }

        private async Task<BalanceSheetViewModel> GetBalSheet_FinalizedVouchers_CurrentBranch_NoPrevious_ClosingOption_NoFilterSort(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseFinalizedVouchers()
                .Build();
            var parameters = GetTestParameters(gridOptions, true, null, ccenterId, projectId);

            return await _repository.GetBalanceSheetAsync(parameters);
        }

        private async Task<BalanceSheetViewModel> GetBalSheet_ConfirmedVouchers_CurrentBranch_NoPrevious_ClosingOption_NoFilterSort(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseConfirmedVouchers()
                .Build();
            var parameters = GetTestParameters(gridOptions, true, null, ccenterId, projectId);

            return await _repository.GetBalanceSheetAsync(parameters);
        }

        private async Task<BalanceSheetViewModel> GetBalSheet_ApprovedVouchers_CurrentBranch_NoPrevious_ClosingOption_NoFilterSort(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseApprovedVouchers()
                .Build();
            var parameters = GetTestParameters(gridOptions, true, null, ccenterId, projectId);

            return await _repository.GetBalanceSheetAsync(parameters);
        }

        private async Task<BalanceSheetViewModel> GetBalSheet_AllVouchers_CurrentBranch_NoPrevious_ClosingOption_NoFilterSort(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .Build();
            var parameters = GetTestParameters(gridOptions, true, null, ccenterId, projectId);

            return await _repository.GetBalanceSheetAsync(parameters);
        }

        #endregion

        #region GetBalSheet_CurrentAndChildren_NoPrevious_ClosingOption_NoFilterSort

        private async Task<BalanceSheetViewModel> GetBalSheet_CheckedVouchers_CurrentAndChildren_NoPrevious_ClosingOption_NoFilterSort(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseCheckedVouchers()
                .Build();
            var parameters = GetTestParameters(gridOptions, true, null, ccenterId, projectId);

            return await _repository.GetBalanceSheetAsync(parameters);
        }

        private async Task<BalanceSheetViewModel> GetBalSheet_FinalizedVouchers_CurrentAndChildren_NoPrevious_ClosingOption_NoFilterSort(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseFinalizedVouchers()
                .Build();
            var parameters = GetTestParameters(gridOptions, true, null, ccenterId, projectId);

            return await _repository.GetBalanceSheetAsync(parameters);
        }

        private async Task<BalanceSheetViewModel> GetBalSheet_ConfirmedVouchers_CurrentAndChildren_NoPrevious_ClosingOption_NoFilterSort(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseConfirmedVouchers()
                .Build();
            var parameters = GetTestParameters(gridOptions, true, null, ccenterId, projectId);

            return await _repository.GetBalanceSheetAsync(parameters);
        }

        private async Task<BalanceSheetViewModel> GetBalSheet_ApprovedVouchers_CurrentAndChildren_NoPrevious_ClosingOption_NoFilterSort(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseApprovedVouchers()
                .Build();
            var parameters = GetTestParameters(gridOptions, true, null, ccenterId, projectId);

            return await _repository.GetBalanceSheetAsync(parameters);
        }

        private async Task<BalanceSheetViewModel> GetBalSheet_AllVouchers_CurrentAndChildren_NoPrevious_ClosingOption_NoFilterSort(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .Build();
            var parameters = GetTestParameters(gridOptions, true, null, ccenterId, projectId);

            return await _repository.GetBalanceSheetAsync(parameters);
        }

        #endregion

        #region GetBalSheet_CurrentBranch_WithPrevious_NoOptions_NoFilterSort

        private async Task<BalanceSheetViewModel> GetBalSheet_CheckedVouchers_CurrentBranch_WithPrevious_NoOptions_NoFilterSort(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseCheckedVouchers()
                .Build();
            var parameters = GetTestParameters(gridOptions, false, TestPreviousFpId, ccenterId, projectId);

            return await _repository.GetBalanceSheetAsync(parameters);
        }

        private async Task<BalanceSheetViewModel> GetBalSheet_FinalizedVouchers_CurrentBranch_WithPrevious_NoOptions_NoFilterSort(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseFinalizedVouchers()
                .Build();
            var parameters = GetTestParameters(gridOptions, false, TestPreviousFpId, ccenterId, projectId);

            return await _repository.GetBalanceSheetAsync(parameters);
        }

        private async Task<BalanceSheetViewModel> GetBalSheet_ConfirmedVouchers_CurrentBranch_WithPrevious_NoOptions_NoFilterSort(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseConfirmedVouchers()
                .Build();
            var parameters = GetTestParameters(gridOptions, false, TestPreviousFpId, ccenterId, projectId);

            return await _repository.GetBalanceSheetAsync(parameters);
        }

        private async Task<BalanceSheetViewModel> GetBalSheet_ApprovedVouchers_CurrentBranch_WithPrevious_NoOptions_NoFilterSort(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseApprovedVouchers()
                .Build();
            var parameters = GetTestParameters(gridOptions, false, TestPreviousFpId, ccenterId, projectId);

            return await _repository.GetBalanceSheetAsync(parameters);
        }

        private async Task<BalanceSheetViewModel> GetBalSheet_AllVouchers_CurrentBranch_WithPrevious_NoOptions_NoFilterSort(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .Build();
            var parameters = GetTestParameters(gridOptions, false, TestPreviousFpId, ccenterId, projectId);

            return await _repository.GetBalanceSheetAsync(parameters);
        }

        #endregion

        #region GetBalSheet_CurrentAndChildren_WithPrevious_NoOptions_NoFilterSort

        private async Task<BalanceSheetViewModel> GetBalSheet_CheckedVouchers_CurrentAndChildren_WithPrevious_NoOptions_NoFilterSort(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseCheckedVouchers()
                .Build();
            var parameters = GetTestParameters(gridOptions, false, TestPreviousFpId, ccenterId, projectId);

            return await _repository.GetBalanceSheetAsync(parameters);
        }

        private async Task<BalanceSheetViewModel> GetBalSheet_FinalizedVouchers_CurrentAndChildren_WithPrevious_NoOptions_NoFilterSort(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseFinalizedVouchers()
                .Build();
            var parameters = GetTestParameters(gridOptions, false, TestPreviousFpId, ccenterId, projectId);

            return await _repository.GetBalanceSheetAsync(parameters);
        }

        private async Task<BalanceSheetViewModel> GetBalSheet_ConfirmedVouchers_CurrentAndChildren_WithPrevious_NoOptions_NoFilterSort(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseConfirmedVouchers()
                .Build();
            var parameters = GetTestParameters(gridOptions, false, TestPreviousFpId, ccenterId, projectId);

            return await _repository.GetBalanceSheetAsync(parameters);
        }

        private async Task<BalanceSheetViewModel> GetBalSheet_ApprovedVouchers_CurrentAndChildren_WithPrevious_NoOptions_NoFilterSort(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseApprovedVouchers()
                .Build();
            var parameters = GetTestParameters(gridOptions, false, TestPreviousFpId, ccenterId, projectId);

            return await _repository.GetBalanceSheetAsync(parameters);
        }

        private async Task<BalanceSheetViewModel> GetBalSheet_AllVouchers_CurrentAndChildren_WithPrevious_NoOptions_NoFilterSort(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .Build();
            var parameters = GetTestParameters(gridOptions, false, TestPreviousFpId, ccenterId, projectId);

            return await _repository.GetBalanceSheetAsync(parameters);
        }

        #endregion

        #region GetBalSheet_CurrentBranch_WithPrevious_ClosingOption_NoFilterSort

        private async Task<BalanceSheetViewModel> GetBalSheet_CheckedVouchers_CurrentBranch_WithPrevious_ClosingOption_NoFilterSort(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseCheckedVouchers()
                .Build();
            var parameters = GetTestParameters(gridOptions, true, TestPreviousFpId, ccenterId, projectId);

            return await _repository.GetBalanceSheetAsync(parameters);
        }

        private async Task<BalanceSheetViewModel> GetBalSheet_FinalizedVouchers_CurrentBranch_WithPrevious_ClosingOption_NoFilterSort(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseFinalizedVouchers()
                .Build();
            var parameters = GetTestParameters(gridOptions, true, TestPreviousFpId, ccenterId, projectId);

            return await _repository.GetBalanceSheetAsync(parameters);
        }

        private async Task<BalanceSheetViewModel> GetBalSheet_ConfirmedVouchers_CurrentBranch_WithPrevious_ClosingOption_NoFilterSort(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseConfirmedVouchers()
                .Build();
            var parameters = GetTestParameters(gridOptions, true, TestPreviousFpId, ccenterId, projectId);

            return await _repository.GetBalanceSheetAsync(parameters);
        }

        private async Task<BalanceSheetViewModel> GetBalSheet_ApprovedVouchers_CurrentBranch_WithPrevious_ClosingOption_NoFilterSort(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseApprovedVouchers()
                .Build();
            var parameters = GetTestParameters(gridOptions, true, TestPreviousFpId, ccenterId, projectId);

            return await _repository.GetBalanceSheetAsync(parameters);
        }

        private async Task<BalanceSheetViewModel> GetBalSheet_AllVouchers_CurrentBranch_WithPrevious_ClosingOption_NoFilterSort(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .Build();
            var parameters = GetTestParameters(gridOptions, true, TestPreviousFpId, ccenterId, projectId);

            return await _repository.GetBalanceSheetAsync(parameters);
        }

        #endregion

        #region GetBalSheet_CurrentAndChildren_WithPrevious_ClosingOption_NoFilterSort

        private async Task<BalanceSheetViewModel> GetBalSheet_CheckedVouchers_CurrentAndChildren_WithPrevious_ClosingOption_NoFilterSort(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseCheckedVouchers()
                .Build();
            var parameters = GetTestParameters(gridOptions, true, TestPreviousFpId, ccenterId, projectId);

            return await _repository.GetBalanceSheetAsync(parameters);
        }

        private async Task<BalanceSheetViewModel> GetBalSheet_FinalizedVouchers_CurrentAndChildren_WithPrevious_ClosingOption_NoFilterSort(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseFinalizedVouchers()
                .Build();
            var parameters = GetTestParameters(gridOptions, true, TestPreviousFpId, ccenterId, projectId);

            return await _repository.GetBalanceSheetAsync(parameters);
        }

        private async Task<BalanceSheetViewModel> GetBalSheet_ConfirmedVouchers_CurrentAndChildren_WithPrevious_ClosingOption_NoFilterSort(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseConfirmedVouchers()
                .Build();
            var parameters = GetTestParameters(gridOptions, true, TestPreviousFpId, ccenterId, projectId);

            return await _repository.GetBalanceSheetAsync(parameters);
        }

        private async Task<BalanceSheetViewModel> GetBalSheet_ApprovedVouchers_CurrentAndChildren_WithPrevious_ClosingOption_NoFilterSort(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .UseApprovedVouchers()
                .Build();
            var parameters = GetTestParameters(gridOptions, true, TestPreviousFpId, ccenterId, projectId);

            return await _repository.GetBalanceSheetAsync(parameters);
        }

        private async Task<BalanceSheetViewModel> GetBalSheet_AllVouchers_CurrentAndChildren_WithPrevious_ClosingOption_NoFilterSort(
            int? ccenterId = null, int? projectId = null)
        {
            var gridOptions = new OptionsBuilder()
                .Build();
            var parameters = GetTestParameters(gridOptions, true, TestPreviousFpId, ccenterId, projectId);

            return await _repository.GetBalanceSheetAsync(parameters);
        }

        #endregion

        private BalanceSheetParameters GetTestParameters(
            GridOptions gridOptions, bool useClosing,
            int? fpId = null, int? ccenterId = null, int? projectId = null)
        {
            return new BalanceSheetParameters()
            {
                GridOptions = gridOptions,
                Date = DateTime.Parse("2019-01-01"),
                UseClosingVoucher = useClosing,
                FiscalPeriodId = fpId,
                CostCenterId = ccenterId,
                ProjectId = projectId
            };
        }

        private const int TestCenterId = 14;
        private const int TestProjectId = 4;
        private const int TestPreviousFpId = 1;
        private IBalanceSheetRepository _repository;
    }
}
