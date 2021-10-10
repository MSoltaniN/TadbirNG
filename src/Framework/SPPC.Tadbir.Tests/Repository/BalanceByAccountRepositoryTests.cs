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
    public class BalanceByAccountRepositoryTests : RepositoryTestBase
    {
        [OneTimeSetUp]
        public void FixtureSetup()
        {
            SetUp();
        }

        #region Balance By Date Tests

        #region Balance_CheckedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort

        [Test]
        public async Task Balance_AllAccounts_CheckedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort(false, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsAndDetails_CheckedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort(false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsOneDetail_CheckedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort(false, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccount_CheckedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort(true, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountAllDetails_CheckedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort(true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountOneDetail_CheckedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort(true, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_FinalizedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort

        [Test]
        public async Task Balance_AllAccounts_FinalizedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort(false, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsAndDetails_FinalizedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort(false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsOneDetail_FinalizedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort(false, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccount_FinalizedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort(true, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountAllDetails_FinalizedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort(true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountOneDetail_FinalizedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort(true, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_ConfirmedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort

        [Test]
        public async Task Balance_AllAccounts_ConfirmedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort(false, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsAndDetails_ConfirmedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort(false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsOneDetail_ConfirmedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort(false, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccount_ConfirmedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort(true, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountAllDetails_ConfirmedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort(true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountOneDetail_ConfirmedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort(true, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_ApprovedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort

        [Test]
        public async Task Balance_AllAccounts_ApprovedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort(false, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsAndDetails_ApprovedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort(false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsOneDetail_ApprovedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort(false, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccount_ApprovedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort(true, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountAllDetails_ApprovedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort(true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountOneDetail_ApprovedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort(true, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_AllVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort

        [Test]
        public async Task Balance_AllAccounts_AllVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort(false, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsAndDetails_AllVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort(false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsOneDetail_AllVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort(false, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccount_AllVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort(true, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountAllDetails_AllVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort(true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountOneDetail_AllVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort(true, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_CheckedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort

        [Test]
        public async Task Balance_AllAccounts_CheckedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort(false, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsAndDetails_CheckedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort(false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsOneDetail_CheckedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort(false, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccount_CheckedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort(true, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountAllDetails_CheckedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort(true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountOneDetail_CheckedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort(true, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_FinalizedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort

        [Test]
        public async Task Balance_AllAccounts_FinalizedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort(false, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsAndDetails_FinalizedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort(false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsOneDetail_FinalizedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort(false, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccount_FinalizedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort(true, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountAllDetails_FinalizedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort(true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountOneDetail_FinalizedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort(true, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort

        [Test]
        public async Task Balance_AllAccounts_ConfirmedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort(false, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsAndDetails_ConfirmedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort(false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsOneDetail_ConfirmedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort(false, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccount_ConfirmedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort(true, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountAllDetails_ConfirmedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort(true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountOneDetail_ConfirmedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort(true, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_ApprovedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort

        [Test]
        public async Task Balance_AllAccounts_ApprovedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort(false, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsAndDetails_ApprovedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort(false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsOneDetail_ApprovedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort(false, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccount_ApprovedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort(true, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountAllDetails_ApprovedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort(true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountOneDetail_ApprovedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort(true, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_AllVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort

        [Test]
        public async Task Balance_AllAccounts_AllVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort(false, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsAndDetails_AllVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort(false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsOneDetail_AllVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort(false, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccount_AllVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort(true, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountAllDetails_AllVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort(true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountOneDetail_AllVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort(true, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_CheckedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort

        [Test]
        public async Task Balance_AllAccounts_CheckedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort(false, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsAndDetails_CheckedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort(false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsOneDetail_CheckedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort(false, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccount_CheckedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort(true, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountAllDetails_CheckedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort(true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountOneDetail_CheckedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort(true, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_FinalizedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort

        [Test]
        public async Task Balance_AllAccounts_FinalizedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort(false, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsAndDetails_FinalizedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort(false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsOneDetail_FinalizedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort(false, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccount_FinalizedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort(true, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountAllDetails_FinalizedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort(true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountOneDetail_FinalizedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort(true, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_ConfirmedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort

        [Test]
        public async Task Balance_AllAccounts_ConfirmedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort(false, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsAndDetails_ConfirmedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort(false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsOneDetail_ConfirmedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort(false, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccount_ConfirmedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort(true, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountAllDetails_ConfirmedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort(true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountOneDetail_ConfirmedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort(true, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_ApprovedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort

        [Test]
        public async Task Balance_AllAccounts_ApprovedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort(false, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsAndDetails_ApprovedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort(false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsOneDetail_ApprovedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort(false, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccount_ApprovedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort(true, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountAllDetails_ApprovedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort(true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountOneDetail_ApprovedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort(true, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_AllVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort

        [Test]
        public async Task Balance_AllAccounts_AllVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort(false, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsAndDetails_AllVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort(false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsOneDetail_AllVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort(false, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccount_AllVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort(true, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountAllDetails_AllVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort(true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountOneDetail_AllVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort(true, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_CheckedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort

        [Test]
        public async Task Balance_AllAccounts_CheckedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort(false, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsAndDetails_CheckedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort(false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsOneDetail_CheckedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort(false, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccount_CheckedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort(true, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountAllDetails_CheckedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort(true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountOneDetail_CheckedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort(true, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_FinalizedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort

        [Test]
        public async Task Balance_AllAccounts_FinalizedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort(false, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsAndDetails_FinalizedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort(false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsOneDetail_FinalizedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort(false, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccount_FinalizedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort(true, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountAllDetails_FinalizedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort(true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountOneDetail_FinalizedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort(true, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_ConfirmedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort

        [Test]
        public async Task Balance_AllAccounts_ConfirmedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort(false, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsAndDetails_ConfirmedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort(false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsOneDetail_ConfirmedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort(false, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccount_ConfirmedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort(true, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountAllDetails_ConfirmedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort(true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountOneDetail_ConfirmedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort(true, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_ApprovedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort

        [Test]
        public async Task Balance_AllAccounts_ApprovedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort(false, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsAndDetails_ApprovedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort(false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsOneDetail_ApprovedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort(false, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccount_ApprovedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort(true, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountAllDetails_ApprovedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort(true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountOneDetail_ApprovedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort(true, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_AllVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort

        [Test]
        public async Task Balance_AllAccounts_AllVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort(false, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsAndDetails_AllVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort(false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsOneDetail_AllVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort(false, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccount_AllVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort(true, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountAllDetails_AllVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort(true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountOneDetail_AllVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort(true, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_CheckedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort

        [Test]
        public async Task Balance_AllAccounts_CheckedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort(false, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsAndDetails_CheckedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort(false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsOneDetail_CheckedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort(false, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccount_CheckedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort(true, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountAllDetails_CheckedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort(true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountOneDetail_CheckedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort(true, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_FinalizedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort

        [Test]
        public async Task Balance_AllAccounts_FinalizedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort(false, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsAndDetails_FinalizedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort(false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsOneDetail_FinalizedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort(false, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccount_FinalizedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort(true, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountAllDetails_FinalizedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort(true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountOneDetail_FinalizedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort(true, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_ConfirmedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort

        [Test]
        public async Task Balance_AllAccounts_ConfirmedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort(false, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsAndDetails_ConfirmedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort(false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsOneDetail_ConfirmedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort(false, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccount_ConfirmedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort(true, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountAllDetails_ConfirmedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort(true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountOneDetail_ConfirmedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort(true, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_ApprovedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort

        [Test]
        public async Task Balance_AllAccounts_ApprovedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort(false, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsAndDetails_ApprovedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort(false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsOneDetail_ApprovedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort(false, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccount_ApprovedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort(true, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountAllDetails_ApprovedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort(true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountOneDetail_ApprovedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort(true, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_AllVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort

        [Test]
        public async Task Balance_AllAccounts_AllVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort(false, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsAndDetails_AllVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort(false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsOneDetail_AllVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort(false, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccount_AllVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort(true, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountAllDetails_AllVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort(true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountOneDetail_AllVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort(true, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_CheckedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort

        [Test]
        public async Task Balance_AllAccounts_CheckedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort(false, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsAndDetails_CheckedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort(false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsOneDetail_CheckedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort(false, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccount_CheckedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort(true, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountAllDetails_CheckedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort(true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountOneDetail_CheckedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort(true, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_FinalizedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort

        [Test]
        public async Task Balance_AllAccounts_FinalizedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort(false, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsAndDetails_FinalizedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort(false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsOneDetail_FinalizedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort(false, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccount_FinalizedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort(true, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountAllDetails_FinalizedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort(true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountOneDetail_FinalizedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort(true, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort

        [Test]
        public async Task Balance_AllAccounts_ConfirmedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort(false, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsAndDetails_ConfirmedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort(false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsOneDetail_ConfirmedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort(false, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccount_ConfirmedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort(true, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountAllDetails_ConfirmedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort(true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountOneDetail_ConfirmedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort(true, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_ApprovedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort

        [Test]
        public async Task Balance_AllAccounts_ApprovedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort(false, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsAndDetails_ApprovedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort(false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsOneDetail_ApprovedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort(false, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccount_ApprovedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort(true, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountAllDetails_ApprovedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort(true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountOneDetail_ApprovedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort(true, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_AllVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort

        [Test]
        public async Task Balance_AllAccounts_AllVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort(false, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsAndDetails_AllVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort(false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsOneDetail_AllVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort(false, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccount_AllVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort(true, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountAllDetails_AllVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort(true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountOneDetail_AllVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort(true, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_CheckedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort

        [Test]
        public async Task Balance_AllAccounts_CheckedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort(false, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsAndDetails_CheckedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort(false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsOneDetail_CheckedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort(false, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccount_CheckedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort(true, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountAllDetails_CheckedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort(true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountOneDetail_CheckedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort(true, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_FinalizedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort

        [Test]
        public async Task Balance_AllAccounts_FinalizedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort(false, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsAndDetails_FinalizedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort(false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsOneDetail_FinalizedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort(false, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccount_FinalizedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort(true, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountAllDetails_FinalizedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort(true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountOneDetail_FinalizedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort(true, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_ConfirmedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort

        [Test]
        public async Task Balance_AllAccounts_ConfirmedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort(false, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsAndDetails_ConfirmedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort(false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsOneDetail_ConfirmedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort(false, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccount_ConfirmedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort(true, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountAllDetails_ConfirmedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort(true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountOneDetail_ConfirmedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort(true, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_ApprovedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort

        [Test]
        public async Task Balance_AllAccounts_ApprovedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort(false, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsAndDetails_ApprovedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort(false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsOneDetail_ApprovedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort(false, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccount_ApprovedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort(true, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountAllDetails_ApprovedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort(true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountOneDetail_ApprovedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort(true, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_AllVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort

        [Test]
        public async Task Balance_AllAccounts_AllVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort(false, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsAndDetails_AllVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort(false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsOneDetail_AllVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort(false, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccount_AllVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort(true, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountAllDetails_AllVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort(true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountOneDetail_AllVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort(true, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_CheckedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort

        [Test]
        public async Task Balance_AllAccounts_CheckedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort(false, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsAndDetails_CheckedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort(false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsOneDetail_CheckedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort(false, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccount_CheckedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort(true, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountAllDetails_CheckedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort(true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountOneDetail_CheckedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort(true, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_FinalizedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort

        [Test]
        public async Task Balance_AllAccounts_FinalizedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort(false, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsAndDetails_FinalizedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort(false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsOneDetail_FinalizedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort(false, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccount_FinalizedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort(true, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountAllDetails_FinalizedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort(true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountOneDetail_FinalizedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort(true, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_ConfirmedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort

        [Test]
        public async Task Balance_AllAccounts_ConfirmedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort(false, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsAndDetails_ConfirmedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort(false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsOneDetail_ConfirmedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort(false, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccount_ConfirmedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort(true, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountAllDetails_ConfirmedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort(true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountOneDetail_ConfirmedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort(true, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_ApprovedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort

        [Test]
        public async Task Balance_AllAccounts_ApprovedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort(false, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsAndDetails_ApprovedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort(false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsOneDetail_ApprovedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort(false, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccount_ApprovedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort(true, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountAllDetails_ApprovedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort(true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountOneDetail_ApprovedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort(true, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_AllVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort

        [Test]
        public async Task Balance_AllAccounts_AllVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort(false, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsAndDetails_AllVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort(false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsOneDetail_AllVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort(false, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccount_AllVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort(true, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountAllDetails_AllVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort(true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountOneDetail_AllVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort(true, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_CheckedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort

        [Test]
        public async Task Balance_AllAccounts_CheckedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort(false, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsAndDetails_CheckedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort(false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsOneDetail_CheckedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort(false, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccount_CheckedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort(true, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountAllDetails_CheckedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort(true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountOneDetail_CheckedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort(true, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_FinalizedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort

        [Test]
        public async Task Balance_AllAccounts_FinalizedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort(false, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsAndDetails_FinalizedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort(false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsOneDetail_FinalizedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort(false, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccount_FinalizedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort(true, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountAllDetails_FinalizedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort(true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountOneDetail_FinalizedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort(true, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_ConfirmedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort

        [Test]
        public async Task Balance_AllAccounts_ConfirmedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort(false, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsAndDetails_ConfirmedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort(false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsOneDetail_ConfirmedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort(false, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccount_ConfirmedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort(true, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountAllDetails_ConfirmedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort(true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountOneDetail_ConfirmedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort(true, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_ApprovedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort

        [Test]
        public async Task Balance_AllAccounts_ApprovedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort(false, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsAndDetails_ApprovedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort(false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsOneDetail_ApprovedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort(false, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccount_ApprovedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort(true, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountAllDetails_ApprovedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort(true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountOneDetail_ApprovedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort(true, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_AllVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort

        [Test]
        public async Task Balance_AllAccounts_AllVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort(false, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsAndDetails_AllVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort(false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsOneDetail_AllVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort(false, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccount_AllVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort(true, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountAllDetails_AllVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort(true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountOneDetail_AllVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort(true, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_CheckedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort

        [Test]
        public async Task Balance_AllAccounts_CheckedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort(false, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsAndDetails_CheckedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort(false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsOneDetail_CheckedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort(false, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccount_CheckedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort(true, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountAllDetails_CheckedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort(true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountOneDetail_CheckedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort(true, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_FinalizedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort

        [Test]
        public async Task Balance_AllAccounts_FinalizedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort(false, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsAndDetails_FinalizedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort(false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsOneDetail_FinalizedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort(false, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccount_FinalizedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort(true, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountAllDetails_FinalizedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort(true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountOneDetail_FinalizedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort(true, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort

        [Test]
        public async Task Balance_AllAccounts_ConfirmedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort(false, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsAndDetails_ConfirmedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort(false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsOneDetail_ConfirmedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort(false, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccount_ConfirmedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort(true, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountAllDetails_ConfirmedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort(true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountOneDetail_ConfirmedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort(true, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_ApprovedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort

        [Test]
        public async Task Balance_AllAccounts_ApprovedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort(false, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsAndDetails_ApprovedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort(false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsOneDetail_ApprovedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort(false, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccount_ApprovedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort(true, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountAllDetails_ApprovedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort(true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountOneDetail_ApprovedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort(true, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_AllVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort

        [Test]
        public async Task Balance_AllAccounts_AllVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort(false, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsAndDetails_AllVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort(false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsOneDetail_AllVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort(false, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccount_AllVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort(true, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountAllDetails_AllVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort(true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountOneDetail_AllVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort(true, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_CheckedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort

        [Test]
        public async Task Balance_AllAccounts_CheckedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort(false, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsAndDetails_CheckedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort(false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsOneDetail_CheckedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort(false, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccount_CheckedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort(true, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountAllDetails_CheckedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort(true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountOneDetail_CheckedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort(true, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_FinalizedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort

        [Test]
        public async Task Balance_AllAccounts_FinalizedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort(false, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsAndDetails_FinalizedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort(false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsOneDetail_FinalizedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort(false, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccount_FinalizedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort(true, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountAllDetails_FinalizedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort(true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountOneDetail_FinalizedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort(true, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_ConfirmedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort

        [Test]
        public async Task Balance_AllAccounts_ConfirmedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort(false, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsAndDetails_ConfirmedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort(false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsOneDetail_ConfirmedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort(false, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccount_ConfirmedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort(true, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountAllDetails_ConfirmedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort(true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountOneDetail_ConfirmedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort(true, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_ApprovedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort

        [Test]
        public async Task Balance_AllAccounts_ApprovedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort(false, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsAndDetails_ApprovedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort(false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsOneDetail_ApprovedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort(false, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccount_ApprovedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort(true, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountAllDetails_ApprovedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort(true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountOneDetail_ApprovedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort(true, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_AllVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort

        [Test]
        public async Task Balance_AllAccounts_AllVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort(false, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsAndDetails_AllVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort(false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsOneDetail_AllVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort(false, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccount_AllVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort(true, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountAllDetails_AllVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort(true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountOneDetail_AllVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort(true, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_CheckedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort

        [Test]
        public async Task Balance_AllAccounts_CheckedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort(false, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsAndDetails_CheckedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort(false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsOneDetail_CheckedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort(false, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccount_CheckedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort(true, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountAllDetails_CheckedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort(true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountOneDetail_CheckedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort(true, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_FinalizedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort

        [Test]
        public async Task Balance_AllAccounts_FinalizedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort(false, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsAndDetails_FinalizedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort(false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsOneDetail_FinalizedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort(false, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccount_FinalizedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort(true, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountAllDetails_FinalizedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort(true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountOneDetail_FinalizedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort(true, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_ConfirmedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort

        [Test]
        public async Task Balance_AllAccounts_ConfirmedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort(false, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsAndDetails_ConfirmedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort(false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsOneDetail_ConfirmedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort(false, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccount_ConfirmedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort(true, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountAllDetails_ConfirmedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort(true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountOneDetail_ConfirmedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort(true, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_ApprovedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort

        [Test]
        public async Task Balance_AllAccounts_ApprovedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort(false, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsAndDetails_ApprovedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort(false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsOneDetail_ApprovedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort(false, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccount_ApprovedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort(true, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountAllDetails_ApprovedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort(true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountOneDetail_ApprovedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort(true, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_AllVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort

        [Test]
        public async Task Balance_AllAccounts_AllVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort(false, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsAndDetails_AllVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort(false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsOneDetail_AllVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort(false, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccount_AllVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort(true, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountAllDetails_AllVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort(true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountOneDetail_AllVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort(true, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_CheckedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort

        [Test]
        public async Task Balance_AllAccounts_CheckedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort(false, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsAndDetails_CheckedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort(false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsOneDetail_CheckedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort(false, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccount_CheckedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort(true, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountAllDetails_CheckedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort(true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountOneDetail_CheckedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort(true, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_FinalizedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort

        [Test]
        public async Task Balance_AllAccounts_FinalizedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort(false, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsAndDetails_FinalizedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort(false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsOneDetail_FinalizedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort(false, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccount_FinalizedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort(true, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountAllDetails_FinalizedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort(true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountOneDetail_FinalizedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort(true, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_ConfirmedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort

        [Test]
        public async Task Balance_AllAccounts_ConfirmedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort(false, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsAndDetails_ConfirmedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort(false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsOneDetail_ConfirmedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort(false, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccount_ConfirmedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort(true, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountAllDetails_ConfirmedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort(true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountOneDetail_ConfirmedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort(true, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_ApprovedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort

        [Test]
        public async Task Balance_AllAccounts_ApprovedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort(false, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsAndDetails_ApprovedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort(false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsOneDetail_ApprovedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort(false, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccount_ApprovedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort(true, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountAllDetails_ApprovedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort(true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountOneDetail_ApprovedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort(true, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_AllVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort

        [Test]
        public async Task Balance_AllAccounts_AllVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort(false, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsAndDetails_AllVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort(false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsOneDetail_AllVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort(false, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccount_AllVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort(true, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountAllDetails_AllVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort(true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountOneDetail_AllVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort(true, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_CheckedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort

        [Test]
        public async Task Balance_AllAccounts_CheckedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort(false, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsAndDetails_CheckedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort(false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsOneDetail_CheckedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort(false, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccount_CheckedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort(true, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountAllDetails_CheckedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort(true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountOneDetail_CheckedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort(true, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_FinalizedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort

        [Test]
        public async Task Balance_AllAccounts_FinalizedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort(false, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsAndDetails_FinalizedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort(false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsOneDetail_FinalizedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort(false, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccount_FinalizedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort(true, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountAllDetails_FinalizedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort(true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountOneDetail_FinalizedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort(true, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort

        [Test]
        public async Task Balance_AllAccounts_ConfirmedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort(false, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsAndDetails_ConfirmedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort(false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsOneDetail_ConfirmedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort(false, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccount_ConfirmedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort(true, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountAllDetails_ConfirmedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort(true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountOneDetail_ConfirmedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort(true, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_ApprovedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort

        [Test]
        public async Task Balance_AllAccounts_ApprovedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort(false, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsAndDetails_ApprovedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort(false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsOneDetail_ApprovedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort(false, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccount_ApprovedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort(true, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountAllDetails_ApprovedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort(true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountOneDetail_ApprovedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort(true, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_AllVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort

        [Test]
        public async Task Balance_AllAccounts_AllVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort(false, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsAndDetails_AllVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort(false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsOneDetail_AllVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort(false, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccount_AllVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort(true, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountAllDetails_AllVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort(true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountOneDetail_AllVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort(true, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_CheckedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort

        [Test]
        public async Task Balance_AllAccounts_CheckedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort(false, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsAndDetails_CheckedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort(false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsOneDetail_CheckedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort(false, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccount_CheckedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort(true, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountAllDetails_CheckedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort(true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountOneDetail_CheckedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort(true, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_FinalizedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort

        [Test]
        public async Task Balance_AllAccounts_FinalizedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort(false, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsAndDetails_FinalizedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort(false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsOneDetail_FinalizedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort(false, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccount_FinalizedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort(true, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountAllDetails_FinalizedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort(true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountOneDetail_FinalizedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort(true, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_ConfirmedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort

        [Test]
        public async Task Balance_AllAccounts_ConfirmedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort(false, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsAndDetails_ConfirmedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort(false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsOneDetail_ConfirmedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort(false, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccount_ConfirmedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort(true, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountAllDetails_ConfirmedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort(true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountOneDetail_ConfirmedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort(true, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_ApprovedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort

        [Test]
        public async Task Balance_AllAccounts_ApprovedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort(false, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsAndDetails_ApprovedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort(false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsOneDetail_ApprovedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort(false, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccount_ApprovedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort(true, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountAllDetails_ApprovedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort(true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountOneDetail_ApprovedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort(true, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_AllVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort

        [Test]
        public async Task Balance_AllAccounts_AllVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort(false, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsAndDetails_AllVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort(false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsOneDetail_AllVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort(false, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccount_AllVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort(true, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountAllDetails_AllVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort(true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountOneDetail_AllVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort(true, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_CheckedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort

        [Test]
        public async Task Balance_AllAccounts_CheckedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort(false, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsAndDetails_CheckedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort(false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsOneDetail_CheckedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort(false, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccount_CheckedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort(true, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountAllDetails_CheckedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort(true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountOneDetail_CheckedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort(true, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_FinalizedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort

        [Test]
        public async Task Balance_AllAccounts_FinalizedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort(false, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsAndDetails_FinalizedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort(false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsOneDetail_FinalizedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort(false, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccount_FinalizedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort(true, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountAllDetails_FinalizedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort(true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountOneDetail_FinalizedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort(true, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_ConfirmedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort

        [Test]
        public async Task Balance_AllAccounts_ConfirmedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort(false, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsAndDetails_ConfirmedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort(false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsOneDetail_ConfirmedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort(false, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccount_ConfirmedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort(true, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountAllDetails_ConfirmedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort(true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountOneDetail_ConfirmedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort(true, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_ApprovedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort

        [Test]
        public async Task Balance_AllAccounts_ApprovedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort(false, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsAndDetails_ApprovedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort(false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsOneDetail_ApprovedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort(false, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccount_ApprovedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort(true, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountAllDetails_ApprovedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort(true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountOneDetail_ApprovedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort(true, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_AllVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort

        [Test]
        public async Task Balance_AllAccounts_AllVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort(false, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsAndDetails_AllVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort(false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsOneDetail_AllVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort(false, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccount_AllVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort(true, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountAllDetails_AllVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort(true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountOneDetail_AllVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort(true, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_CheckedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort

        [Test]
        public async Task Balance_AllAccounts_CheckedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort(false, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsAndDetails_CheckedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort(false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsOneDetail_CheckedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort(false, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccount_CheckedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort(true, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountAllDetails_CheckedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort(true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountOneDetail_CheckedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort(true, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_FinalizedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort

        [Test]
        public async Task Balance_AllAccounts_FinalizedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort(false, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsAndDetails_FinalizedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort(false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsOneDetail_FinalizedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort(false, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccount_FinalizedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort(true, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountAllDetails_FinalizedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort(true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountOneDetail_FinalizedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort(true, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_ConfirmedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort

        [Test]
        public async Task Balance_AllAccounts_ConfirmedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort(false, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsAndDetails_ConfirmedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort(false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsOneDetail_ConfirmedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort(false, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccount_ConfirmedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort(true, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountAllDetails_ConfirmedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort(true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountOneDetail_ConfirmedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort(true, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_ApprovedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort

        [Test]
        public async Task Balance_AllAccounts_ApprovedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort(false, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsAndDetails_ApprovedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort(false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsOneDetail_ApprovedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort(false, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccount_ApprovedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort(true, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountAllDetails_ApprovedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort(true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountOneDetail_ApprovedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort(true, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_AllVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort

        [Test]
        public async Task Balance_AllAccounts_AllVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort(false, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsAndDetails_AllVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort(false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsOneDetail_AllVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort(false, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccount_AllVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort(true, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountAllDetails_AllVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort(true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountOneDetail_AllVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort(true, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_CheckedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort

        [Test]
        public async Task Balance_AllAccounts_CheckedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort(false, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsAndDetails_CheckedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort(false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsOneDetail_CheckedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort(false, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccount_CheckedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort(true, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountAllDetails_CheckedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort(true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountOneDetail_CheckedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort(true, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_FinalizedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort

        [Test]
        public async Task Balance_AllAccounts_FinalizedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort(false, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsAndDetails_FinalizedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort(false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsOneDetail_FinalizedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort(false, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccount_FinalizedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort(true, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountAllDetails_FinalizedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort(true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountOneDetail_FinalizedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort(true, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort

        [Test]
        public async Task Balance_AllAccounts_ConfirmedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort(false, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsAndDetails_ConfirmedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort(false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsOneDetail_ConfirmedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort(false, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccount_ConfirmedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort(true, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountAllDetails_ConfirmedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort(true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountOneDetail_ConfirmedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort(true, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_ApprovedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort

        [Test]
        public async Task Balance_AllAccounts_ApprovedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort(false, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsAndDetails_ApprovedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort(false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsOneDetail_ApprovedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort(false, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccount_ApprovedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort(true, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountAllDetails_ApprovedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort(true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountOneDetail_ApprovedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort(true, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_AllVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort

        [Test]
        public async Task Balance_AllAccounts_AllVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort(false, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsAndDetails_AllVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort(false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsOneDetail_AllVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort(false, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccount_AllVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort(true, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountAllDetails_AllVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort(true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountOneDetail_AllVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort(true, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_CheckedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort

        [Test]
        public async Task Balance_AllAccounts_CheckedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort(false, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsAndDetails_CheckedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort(false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsOneDetail_CheckedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort(false, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccount_CheckedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort(true, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountAllDetails_CheckedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort(true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountOneDetail_CheckedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort(true, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_FinalizedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort

        [Test]
        public async Task Balance_AllAccounts_FinalizedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort(false, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsAndDetails_FinalizedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort(false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsOneDetail_FinalizedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort(false, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccount_FinalizedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort(true, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountAllDetails_FinalizedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort(true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountOneDetail_FinalizedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort(true, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_ConfirmedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort

        [Test]
        public async Task Balance_AllAccounts_ConfirmedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort(false, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsAndDetails_ConfirmedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort(false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsOneDetail_ConfirmedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort(false, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccount_ConfirmedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort(true, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountAllDetails_ConfirmedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort(true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountOneDetail_ConfirmedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort(true, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_ApprovedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort

        [Test]
        public async Task Balance_AllAccounts_ApprovedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort(false, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsAndDetails_ApprovedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort(false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsOneDetail_ApprovedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort(false, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccount_ApprovedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort(true, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountAllDetails_ApprovedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort(true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountOneDetail_ApprovedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort(true, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_AllVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort

        [Test]
        public async Task Balance_AllAccounts_AllVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort(false, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsAndDetails_AllVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort(false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsOneDetail_AllVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort(false, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccount_AllVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort(true, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountAllDetails_AllVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort(true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountOneDetail_AllVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort(true, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_CheckedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort

        [Test]
        public async Task Balance_AllAccounts_CheckedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort(false, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsAndDetails_CheckedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort(false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsOneDetail_CheckedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort(false, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccount_CheckedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort(true, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountAllDetails_CheckedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort(true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountOneDetail_CheckedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort(true, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_FinalizedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort

        [Test]
        public async Task Balance_AllAccounts_FinalizedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort(false, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsAndDetails_FinalizedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort(false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsOneDetail_FinalizedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort(false, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccount_FinalizedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort(true, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountAllDetails_FinalizedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort(true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountOneDetail_FinalizedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort(true, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_ConfirmedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort

        [Test]
        public async Task Balance_AllAccounts_ConfirmedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort(false, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsAndDetails_ConfirmedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort(false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsOneDetail_ConfirmedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort(false, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccount_ConfirmedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort(true, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountAllDetails_ConfirmedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort(true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountOneDetail_ConfirmedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort(true, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_ApprovedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort

        [Test]
        public async Task Balance_AllAccounts_ApprovedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort(false, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsAndDetails_ApprovedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort(false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsOneDetail_ApprovedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort(false, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccount_ApprovedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort(true, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountAllDetails_ApprovedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort(true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountOneDetail_ApprovedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort(true, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_AllVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort

        [Test]
        public async Task Balance_AllAccounts_AllVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort(false, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsAndDetails_AllVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort(false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsOneDetail_AllVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort(false, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccount_AllVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort(true, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountAllDetails_AllVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort(true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountOneDetail_AllVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort(true, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_CheckedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort

        [Test]
        public async Task Balance_AllAccounts_CheckedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort(false, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsAndDetails_CheckedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort(false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsOneDetail_CheckedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort(false, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccount_CheckedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort(true, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountAllDetails_CheckedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort(true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountOneDetail_CheckedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort(true, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_FinalizedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort

        [Test]
        public async Task Balance_AllAccounts_FinalizedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort(false, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsAndDetails_FinalizedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort(false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsOneDetail_FinalizedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort(false, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccount_FinalizedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort(true, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountAllDetails_FinalizedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort(true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountOneDetail_FinalizedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort(true, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_ConfirmedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort

        [Test]
        public async Task Balance_AllAccounts_ConfirmedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort(false, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsAndDetails_ConfirmedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort(false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsOneDetail_ConfirmedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort(false, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccount_ConfirmedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort(true, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountAllDetails_ConfirmedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort(true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountOneDetail_ConfirmedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort(true, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_ApprovedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort

        [Test]
        public async Task Balance_AllAccounts_ApprovedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort(false, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsAndDetails_ApprovedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort(false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsOneDetail_ApprovedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort(false, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccount_ApprovedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort(true, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountAllDetails_ApprovedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort(true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountOneDetail_ApprovedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort(true, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_AllVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort

        [Test]
        public async Task Balance_AllAccounts_AllVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort(false, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsAndDetails_AllVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort(false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsOneDetail_AllVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort(false, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccount_AllVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort(true, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountAllDetails_AllVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort(true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountOneDetail_AllVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort(true, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_CheckedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort

        [Test]
        public async Task Balance_AllAccounts_CheckedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort(false, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsAndDetails_CheckedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort(false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsOneDetail_CheckedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort(false, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccount_CheckedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort(true, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountAllDetails_CheckedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort(true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountOneDetail_CheckedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort(true, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_FinalizedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort

        [Test]
        public async Task Balance_AllAccounts_FinalizedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort(false, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsAndDetails_FinalizedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort(false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsOneDetail_FinalizedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort(false, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccount_FinalizedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort(true, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountAllDetails_FinalizedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort(true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountOneDetail_FinalizedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort(true, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort

        [Test]
        public async Task Balance_AllAccounts_ConfirmedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort(false, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsAndDetails_ConfirmedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort(false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsOneDetail_ConfirmedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort(false, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccount_ConfirmedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort(true, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountAllDetails_ConfirmedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort(true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountOneDetail_ConfirmedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort(true, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_ApprovedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort

        [Test]
        public async Task Balance_AllAccounts_ApprovedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort(false, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsAndDetails_ApprovedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort(false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsOneDetail_ApprovedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort(false, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccount_ApprovedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort(true, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountAllDetails_ApprovedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort(true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountOneDetail_ApprovedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort(true, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_AllVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort

        [Test]
        public async Task Balance_AllAccounts_AllVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort(false, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsAndDetails_AllVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort(false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsOneDetail_AllVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort(false, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccount_AllVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort(true, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountAllDetails_AllVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort(true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountOneDetail_AllVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort(true, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_CheckedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort

        [Test]
        public async Task Balance_AllAccounts_CheckedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort(false, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsAndDetails_CheckedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort(false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsOneDetail_CheckedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort(false, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccount_CheckedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort(true, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountAllDetails_CheckedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort(true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountOneDetail_CheckedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort(true, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_FinalizedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort

        [Test]
        public async Task Balance_AllAccounts_FinalizedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort(false, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsAndDetails_FinalizedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort(false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsOneDetail_FinalizedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort(false, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccount_FinalizedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort(true, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountAllDetails_FinalizedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort(true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountOneDetail_FinalizedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort(true, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_ConfirmedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort

        [Test]
        public async Task Balance_AllAccounts_ConfirmedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort(false, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsAndDetails_ConfirmedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort(false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsOneDetail_ConfirmedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort(false, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccount_ConfirmedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort(true, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountAllDetails_ConfirmedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort(true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountOneDetail_ConfirmedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort(true, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_ApprovedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort

        [Test]
        public async Task Balance_AllAccounts_ApprovedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort(false, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsAndDetails_ApprovedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort(false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsOneDetail_ApprovedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort(false, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccount_ApprovedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort(true, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountAllDetails_ApprovedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort(true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountOneDetail_ApprovedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort(true, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_AllVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort

        [Test]
        public async Task Balance_AllAccounts_AllVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort(false, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsAndDetails_AllVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort(false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsOneDetail_AllVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort(false, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccount_AllVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort(true, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountAllDetails_AllVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort(true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountOneDetail_AllVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort(true, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_CheckedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort

        [Test]
        public async Task Balance_AllAccounts_CheckedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort(false, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsAndDetails_CheckedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort(false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsOneDetail_CheckedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort(false, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccount_CheckedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort(true, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountAllDetails_CheckedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort(true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountOneDetail_CheckedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort(true, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_FinalizedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort

        [Test]
        public async Task Balance_AllAccounts_FinalizedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort(false, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsAndDetails_FinalizedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort(false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsOneDetail_FinalizedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort(false, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccount_FinalizedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort(true, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountAllDetails_FinalizedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort(true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountOneDetail_FinalizedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort(true, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_ConfirmedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort

        [Test]
        public async Task Balance_AllAccounts_ConfirmedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort(false, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsAndDetails_ConfirmedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort(false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsOneDetail_ConfirmedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort(false, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccount_ConfirmedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort(true, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountAllDetails_ConfirmedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort(true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountOneDetail_ConfirmedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort(true, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_ApprovedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort

        [Test]
        public async Task Balance_AllAccounts_ApprovedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort(false, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsAndDetails_ApprovedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort(false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsOneDetail_ApprovedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort(false, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccount_ApprovedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort(true, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountAllDetails_ApprovedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort(true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountOneDetail_ApprovedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort(true, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_AllVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort

        [Test]
        public async Task Balance_AllAccounts_AllVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort(false, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsAndDetails_AllVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort(false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsOneDetail_AllVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort(false, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccount_AllVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort(true, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountAllDetails_AllVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort(true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountOneDetail_AllVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort(true, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_CheckedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort

        [Test]
        public async Task Balance_AllAccounts_CheckedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(false, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsAndDetails_CheckedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsOneDetail_CheckedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(false, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccount_CheckedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(true, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountAllDetails_CheckedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountOneDetail_CheckedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(true, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_FinalizedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort

        [Test]
        public async Task Balance_AllAccounts_FinalizedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(false, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsAndDetails_FinalizedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsOneDetail_FinalizedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(false, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccount_FinalizedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(true, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountAllDetails_FinalizedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountOneDetail_FinalizedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(true, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_ConfirmedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort

        [Test]
        public async Task Balance_AllAccounts_ConfirmedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(false, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsAndDetails_ConfirmedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsOneDetail_ConfirmedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(false, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccount_ConfirmedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(true, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountAllDetails_ConfirmedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountOneDetail_ConfirmedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(true, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_ApprovedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort

        [Test]
        public async Task Balance_AllAccounts_ApprovedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(false, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsAndDetails_ApprovedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsOneDetail_ApprovedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(false, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccount_ApprovedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(true, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountAllDetails_ApprovedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountOneDetail_ApprovedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(true, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_AllVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort

        [Test]
        public async Task Balance_AllAccounts_AllVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(false, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsAndDetails_AllVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsOneDetail_AllVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(false, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccount_AllVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(true, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountAllDetails_AllVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountOneDetail_AllVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(true, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_CheckedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort

        [Test]
        public async Task Balance_AllAccounts_CheckedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(false, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsAndDetails_CheckedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsOneDetail_CheckedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(false, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccount_CheckedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(true, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountAllDetails_CheckedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountOneDetail_CheckedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(true, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_FinalizedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort

        [Test]
        public async Task Balance_AllAccounts_FinalizedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(false, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsAndDetails_FinalizedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsOneDetail_FinalizedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(false, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccount_FinalizedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(true, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountAllDetails_FinalizedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountOneDetail_FinalizedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(true, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort

        [Test]
        public async Task Balance_AllAccounts_ConfirmedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(false, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsAndDetails_ConfirmedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsOneDetail_ConfirmedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(false, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccount_ConfirmedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(true, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountAllDetails_ConfirmedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountOneDetail_ConfirmedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(true, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_ApprovedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort

        [Test]
        public async Task Balance_AllAccounts_ApprovedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(false, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsAndDetails_ApprovedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsOneDetail_ApprovedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(false, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccount_ApprovedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(true, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountAllDetails_ApprovedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountOneDetail_ApprovedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(true, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_AllVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort

        [Test]
        public async Task Balance_AllAccounts_AllVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(false, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsAndDetails_AllVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsOneDetail_AllVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(false, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccount_AllVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(true, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountAllDetails_AllVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountOneDetail_AllVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(true, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_CheckedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort

        [Test]
        public async Task Balance_AllAccounts_CheckedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort(false, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsAndDetails_CheckedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort(false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsOneDetail_CheckedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort(false, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccount_CheckedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort(true, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountAllDetails_CheckedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort(true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountOneDetail_CheckedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort(true, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_FinalizedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort

        [Test]
        public async Task Balance_AllAccounts_FinalizedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort(false, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsAndDetails_FinalizedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort(false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsOneDetail_FinalizedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort(false, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccount_FinalizedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort(true, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountAllDetails_FinalizedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort(true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountOneDetail_FinalizedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort(true, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_ConfirmedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort

        [Test]
        public async Task Balance_AllAccounts_ConfirmedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort(false, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsAndDetails_ConfirmedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort(false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsOneDetail_ConfirmedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort(false, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccount_ConfirmedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort(true, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountAllDetails_ConfirmedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort(true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountOneDetail_ConfirmedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort(true, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_ApprovedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort

        [Test]
        public async Task Balance_AllAccounts_ApprovedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort(false, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsAndDetails_ApprovedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort(false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsOneDetail_ApprovedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort(false, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccount_ApprovedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort(true, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountAllDetails_ApprovedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort(true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountOneDetail_ApprovedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort(true, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_AllVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort

        [Test]
        public async Task Balance_AllAccounts_AllVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort(false, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsAndDetails_AllVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort(false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsOneDetail_AllVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort(false, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccount_AllVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort(true, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountAllDetails_AllVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort(true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountOneDetail_AllVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort(true, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_CheckedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort

        [Test]
        public async Task Balance_AllAccounts_CheckedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort(false, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsAndDetails_CheckedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort(false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsOneDetail_CheckedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort(false, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccount_CheckedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort(true, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountAllDetails_CheckedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort(true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountOneDetail_CheckedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort(true, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_FinalizedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort

        [Test]
        public async Task Balance_AllAccounts_FinalizedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort(false, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsAndDetails_FinalizedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort(false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsOneDetail_FinalizedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort(false, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccount_FinalizedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort(true, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountAllDetails_FinalizedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort(true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountOneDetail_FinalizedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort(true, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_ConfirmedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort

        [Test]
        public async Task Balance_AllAccounts_ConfirmedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort(false, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsAndDetails_ConfirmedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort(false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsOneDetail_ConfirmedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort(false, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccount_ConfirmedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort(true, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountAllDetails_ConfirmedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort(true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountOneDetail_ConfirmedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort(true, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_ApprovedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort

        [Test]
        public async Task Balance_AllAccounts_ApprovedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort(false, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsAndDetails_ApprovedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort(false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsOneDetail_ApprovedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort(false, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccount_ApprovedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort(true, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountAllDetails_ApprovedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort(true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountOneDetail_ApprovedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort(true, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_AllVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort

        [Test]
        public async Task Balance_AllAccounts_AllVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort(false, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsAndDetails_AllVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort(false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsOneDetail_AllVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort(false, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccount_AllVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort(true, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountAllDetails_AllVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort(true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountOneDetail_AllVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort(true, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_CheckedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort

        [Test]
        public async Task Balance_AllAccounts_CheckedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort(false, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsAndDetails_CheckedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort(false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsOneDetail_CheckedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort(false, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccount_CheckedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort(true, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountAllDetails_CheckedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort(true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountOneDetail_CheckedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort(true, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_FinalizedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort

        [Test]
        public async Task Balance_AllAccounts_FinalizedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort(false, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsAndDetails_FinalizedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort(false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsOneDetail_FinalizedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort(false, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccount_FinalizedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort(true, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountAllDetails_FinalizedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort(true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountOneDetail_FinalizedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort(true, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_ConfirmedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort

        [Test]
        public async Task Balance_AllAccounts_ConfirmedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort(false, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsAndDetails_ConfirmedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort(false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsOneDetail_ConfirmedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort(false, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccount_ConfirmedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort(true, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountAllDetails_ConfirmedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort(true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountOneDetail_ConfirmedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort(true, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_ApprovedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort

        [Test]
        public async Task Balance_AllAccounts_ApprovedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort(false, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsAndDetails_ApprovedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort(false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsOneDetail_ApprovedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort(false, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccount_ApprovedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort(true, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountAllDetails_ApprovedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort(true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountOneDetail_ApprovedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort(true, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_AllVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort

        [Test]
        public async Task Balance_AllAccounts_AllVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort(false, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsAndDetails_AllVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort(false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsOneDetail_AllVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort(false, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccount_AllVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort(true, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountAllDetails_AllVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort(true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountOneDetail_AllVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort(true, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_CheckedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort

        [Test]
        public async Task Balance_AllAccounts_CheckedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort(false, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsAndDetails_CheckedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort(false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsOneDetail_CheckedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort(false, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccount_CheckedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort(true, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountAllDetails_CheckedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort(true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountOneDetail_CheckedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort(true, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_FinalizedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort

        [Test]
        public async Task Balance_AllAccounts_FinalizedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort(false, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsAndDetails_FinalizedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort(false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsOneDetail_FinalizedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort(false, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccount_FinalizedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort(true, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountAllDetails_FinalizedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort(true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountOneDetail_FinalizedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort(true, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort

        [Test]
        public async Task Balance_AllAccounts_ConfirmedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort(false, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsAndDetails_ConfirmedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort(false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsOneDetail_ConfirmedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort(false, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccount_ConfirmedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort(true, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountAllDetails_ConfirmedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort(true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountOneDetail_ConfirmedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort(true, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_ApprovedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort

        [Test]
        public async Task Balance_AllAccounts_ApprovedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort(false, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsAndDetails_ApprovedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort(false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsOneDetail_ApprovedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort(false, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccount_ApprovedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort(true, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountAllDetails_ApprovedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort(true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountOneDetail_ApprovedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort(true, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_AllVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort

        [Test]
        public async Task Balance_AllAccounts_AllVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort(false, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsAndDetails_AllVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort(false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsOneDetail_AllVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort(false, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccount_AllVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort(true, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountAllDetails_AllVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort(true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountOneDetail_AllVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort(true, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_CheckedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort

        [Test]
        public async Task Balance_AllAccounts_CheckedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort(false, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsAndDetails_CheckedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort(false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsOneDetail_CheckedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort(false, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccount_CheckedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort(true, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountAllDetails_CheckedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort(true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountOneDetail_CheckedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort(true, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_FinalizedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort

        [Test]
        public async Task Balance_AllAccounts_FinalizedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort(false, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsAndDetails_FinalizedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort(false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsOneDetail_FinalizedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort(false, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccount_FinalizedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort(true, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountAllDetails_FinalizedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort(true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountOneDetail_FinalizedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort(true, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_ConfirmedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort

        [Test]
        public async Task Balance_AllAccounts_ConfirmedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort(false, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsAndDetails_ConfirmedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort(false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsOneDetail_ConfirmedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort(false, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccount_ConfirmedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort(true, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountAllDetails_ConfirmedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort(true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountOneDetail_ConfirmedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort(true, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_ApprovedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort

        [Test]
        public async Task Balance_AllAccounts_ApprovedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort(false, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsAndDetails_ApprovedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort(false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsOneDetail_ApprovedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort(false, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccount_ApprovedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort(true, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountAllDetails_ApprovedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort(true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountOneDetail_ApprovedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort(true, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_AllVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort

        [Test]
        public async Task Balance_AllAccounts_AllVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort(false, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsAndDetails_AllVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort(false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsOneDetail_AllVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort(false, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccount_AllVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort(true, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountAllDetails_AllVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort(true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountOneDetail_AllVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort(true, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_CheckedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort

        [Test]
        public async Task Balance_AllAccounts_CheckedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort(false, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsAndDetails_CheckedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort(false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsOneDetail_CheckedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort(false, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccount_CheckedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort(true, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountAllDetails_CheckedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort(true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountOneDetail_CheckedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort(true, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_FinalizedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort

        [Test]
        public async Task Balance_AllAccounts_FinalizedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort(false, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsAndDetails_FinalizedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort(false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsOneDetail_FinalizedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort(false, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccount_FinalizedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort(true, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountAllDetails_FinalizedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort(true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountOneDetail_FinalizedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort(true, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_ConfirmedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort

        [Test]
        public async Task Balance_AllAccounts_ConfirmedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort(false, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsAndDetails_ConfirmedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort(false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsOneDetail_ConfirmedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort(false, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccount_ConfirmedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort(true, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountAllDetails_ConfirmedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort(true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountOneDetail_ConfirmedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort(true, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_ApprovedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort

        [Test]
        public async Task Balance_AllAccounts_ApprovedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort(false, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsAndDetails_ApprovedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort(false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsOneDetail_ApprovedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort(false, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccount_ApprovedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort(true, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountAllDetails_ApprovedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort(true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountOneDetail_ApprovedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort(true, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_AllVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort

        [Test]
        public async Task Balance_AllAccounts_AllVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort(false, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsAndDetails_AllVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort(false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_AllAccountsOneDetail_AllVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort(false, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccount_AllVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort(true, null);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountAllDetails_AllVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort(true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_OneAccountOneDetail_AllVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort(true, true);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #endregion

        #region Balance By No Tests

        #region Balance_CheckedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort

        [Test]
        public async Task BalanceByNo_AllAccounts_CheckedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort(false, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsAndDetails_CheckedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort(false, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsOneDetail_CheckedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort(false, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccount_CheckedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort(true, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountAllDetails_CheckedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort(true, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountOneDetail_CheckedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort(true, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_FinalizedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort

        [Test]
        public async Task BalanceByNo_AllAccounts_FinalizedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort(false, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsAndDetails_FinalizedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort(false, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsOneDetail_FinalizedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort(false, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccount_FinalizedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort(true, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountAllDetails_FinalizedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort(true, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountOneDetail_FinalizedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort(true, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_ConfirmedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort

        [Test]
        public async Task BalanceByNo_AllAccounts_ConfirmedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort(false, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsAndDetails_ConfirmedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort(false, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsOneDetail_ConfirmedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort(false, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccount_ConfirmedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort(true, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountAllDetails_ConfirmedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort(true, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountOneDetail_ConfirmedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort(true, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_ApprovedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort

        [Test]
        public async Task BalanceByNo_AllAccounts_ApprovedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort(false, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsAndDetails_ApprovedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort(false, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsOneDetail_ApprovedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort(false, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccount_ApprovedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort(true, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountAllDetails_ApprovedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort(true, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountOneDetail_ApprovedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort(true, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_AllVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort

        [Test]
        public async Task BalanceByNo_AllAccounts_AllVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort(false, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsAndDetails_AllVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort(false, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsOneDetail_AllVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort(false, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccount_AllVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort(true, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountAllDetails_AllVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort(true, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountOneDetail_AllVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort(true, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_CheckedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort

        [Test]
        public async Task BalanceByNo_AllAccounts_CheckedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort(false, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsAndDetails_CheckedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort(false, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsOneDetail_CheckedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort(false, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccount_CheckedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort(true, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountAllDetails_CheckedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort(true, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountOneDetail_CheckedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort(true, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_FinalizedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort

        [Test]
        public async Task BalanceByNo_AllAccounts_FinalizedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort(false, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsAndDetails_FinalizedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort(false, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsOneDetail_FinalizedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort(false, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccount_FinalizedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort(true, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountAllDetails_FinalizedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort(true, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountOneDetail_FinalizedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort(true, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort

        [Test]
        public async Task BalanceByNo_AllAccounts_ConfirmedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort(false, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsAndDetails_ConfirmedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort(false, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsOneDetail_ConfirmedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort(false, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccount_ConfirmedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort(true, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountAllDetails_ConfirmedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort(true, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountOneDetail_ConfirmedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort(true, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_ApprovedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort

        [Test]
        public async Task BalanceByNo_AllAccounts_ApprovedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort(false, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsAndDetails_ApprovedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort(false, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsOneDetail_ApprovedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort(false, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccount_ApprovedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort(true, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountAllDetails_ApprovedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort(true, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountOneDetail_ApprovedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort(true, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_AllVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort

        [Test]
        public async Task BalanceByNo_AllAccounts_AllVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort(false, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsAndDetails_AllVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort(false, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsOneDetail_AllVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort(false, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccount_AllVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort(true, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountAllDetails_AllVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort(true, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountOneDetail_AllVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort(true, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_CheckedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort

        [Test]
        public async Task BalanceByNo_AllAccounts_CheckedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort(false, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsAndDetails_CheckedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort(false, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsOneDetail_CheckedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort(false, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccount_CheckedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort(true, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountAllDetails_CheckedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort(true, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountOneDetail_CheckedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort(true, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_FinalizedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort

        [Test]
        public async Task BalanceByNo_AllAccounts_FinalizedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort(false, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsAndDetails_FinalizedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort(false, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsOneDetail_FinalizedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort(false, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccount_FinalizedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort(true, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountAllDetails_FinalizedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort(true, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountOneDetail_FinalizedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort(true, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_ConfirmedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort

        [Test]
        public async Task BalanceByNo_AllAccounts_ConfirmedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort(false, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsAndDetails_ConfirmedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort(false, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsOneDetail_ConfirmedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort(false, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccount_ConfirmedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort(true, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountAllDetails_ConfirmedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort(true, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountOneDetail_ConfirmedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort(true, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_ApprovedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort

        [Test]
        public async Task BalanceByNo_AllAccounts_ApprovedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort(false, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsAndDetails_ApprovedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort(false, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsOneDetail_ApprovedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort(false, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccount_ApprovedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort(true, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountAllDetails_ApprovedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort(true, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountOneDetail_ApprovedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort(true, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_AllVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort

        [Test]
        public async Task BalanceByNo_AllAccounts_AllVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort(false, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsAndDetails_AllVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort(false, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsOneDetail_AllVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort(false, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccount_AllVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort(true, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountAllDetails_AllVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort(true, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountOneDetail_AllVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort(true, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_CheckedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort

        [Test]
        public async Task BalanceByNo_AllAccounts_CheckedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort(false, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsAndDetails_CheckedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort(false, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsOneDetail_CheckedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort(false, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccount_CheckedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort(true, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountAllDetails_CheckedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort(true, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountOneDetail_CheckedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort(true, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_FinalizedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort

        [Test]
        public async Task BalanceByNo_AllAccounts_FinalizedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort(false, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsAndDetails_FinalizedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort(false, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsOneDetail_FinalizedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort(false, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccount_FinalizedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort(true, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountAllDetails_FinalizedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort(true, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountOneDetail_FinalizedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort(true, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_ConfirmedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort

        [Test]
        public async Task BalanceByNo_AllAccounts_ConfirmedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort(false, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsAndDetails_ConfirmedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort(false, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsOneDetail_ConfirmedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort(false, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccount_ConfirmedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort(true, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountAllDetails_ConfirmedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort(true, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountOneDetail_ConfirmedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort(true, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_ApprovedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort

        [Test]
        public async Task BalanceByNo_AllAccounts_ApprovedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort(false, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsAndDetails_ApprovedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort(false, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsOneDetail_ApprovedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort(false, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccount_ApprovedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort(true, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountAllDetails_ApprovedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort(true, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountOneDetail_ApprovedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort(true, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_AllVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort

        [Test]
        public async Task BalanceByNo_AllAccounts_AllVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort(false, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsAndDetails_AllVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort(false, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsOneDetail_AllVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort(false, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccount_AllVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort(true, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountAllDetails_AllVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort(true, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountOneDetail_AllVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort(true, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_CheckedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort

        [Test]
        public async Task BalanceByNo_AllAccounts_CheckedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort(false, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsAndDetails_CheckedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort(false, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsOneDetail_CheckedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort(false, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccount_CheckedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort(true, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountAllDetails_CheckedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort(true, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountOneDetail_CheckedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort(true, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_FinalizedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort

        [Test]
        public async Task BalanceByNo_AllAccounts_FinalizedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort(false, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsAndDetails_FinalizedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort(false, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsOneDetail_FinalizedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort(false, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccount_FinalizedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort(true, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountAllDetails_FinalizedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort(true, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountOneDetail_FinalizedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort(true, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_ConfirmedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort

        [Test]
        public async Task BalanceByNo_AllAccounts_ConfirmedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort(false, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsAndDetails_ConfirmedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort(false, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsOneDetail_ConfirmedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort(false, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccount_ConfirmedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort(true, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountAllDetails_ConfirmedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort(true, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountOneDetail_ConfirmedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort(true, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_ApprovedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort

        [Test]
        public async Task BalanceByNo_AllAccounts_ApprovedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort(false, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsAndDetails_ApprovedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort(false, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsOneDetail_ApprovedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort(false, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccount_ApprovedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort(true, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountAllDetails_ApprovedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort(true, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountOneDetail_ApprovedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort(true, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_AllVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort

        [Test]
        public async Task BalanceByNo_AllAccounts_AllVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort(false, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsAndDetails_AllVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort(false, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsOneDetail_AllVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort(false, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccount_AllVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort(true, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountAllDetails_AllVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort(true, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountOneDetail_AllVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort(true, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_CheckedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort

        [Test]
        public async Task BalanceByNo_AllAccounts_CheckedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort(false, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsAndDetails_CheckedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort(false, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsOneDetail_CheckedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort(false, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccount_CheckedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort(true, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountAllDetails_CheckedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort(true, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountOneDetail_CheckedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort(true, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_FinalizedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort

        [Test]
        public async Task BalanceByNo_AllAccounts_FinalizedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort(false, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsAndDetails_FinalizedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort(false, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsOneDetail_FinalizedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort(false, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccount_FinalizedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort(true, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountAllDetails_FinalizedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort(true, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountOneDetail_FinalizedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort(true, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort

        [Test]
        public async Task BalanceByNo_AllAccounts_ConfirmedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort(false, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsAndDetails_ConfirmedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort(false, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsOneDetail_ConfirmedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort(false, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccount_ConfirmedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort(true, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountAllDetails_ConfirmedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort(true, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountOneDetail_ConfirmedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort(true, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_ApprovedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort

        [Test]
        public async Task BalanceByNo_AllAccounts_ApprovedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort(false, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsAndDetails_ApprovedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort(false, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsOneDetail_ApprovedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort(false, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccount_ApprovedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort(true, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountAllDetails_ApprovedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort(true, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountOneDetail_ApprovedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort(true, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_AllVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort

        [Test]
        public async Task BalanceByNo_AllAccounts_AllVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort(false, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsAndDetails_AllVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort(false, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsOneDetail_AllVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort(false, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccount_AllVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort(true, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountAllDetails_AllVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort(true, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountOneDetail_AllVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort(true, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_CheckedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort

        [Test]
        public async Task BalanceByNo_AllAccounts_CheckedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort(false, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsAndDetails_CheckedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort(false, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsOneDetail_CheckedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort(false, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccount_CheckedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort(true, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountAllDetails_CheckedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort(true, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountOneDetail_CheckedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort(true, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_FinalizedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort

        [Test]
        public async Task BalanceByNo_AllAccounts_FinalizedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort(false, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsAndDetails_FinalizedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort(false, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsOneDetail_FinalizedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort(false, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccount_FinalizedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort(true, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountAllDetails_FinalizedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort(true, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountOneDetail_FinalizedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort(true, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_ConfirmedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort

        [Test]
        public async Task BalanceByNo_AllAccounts_ConfirmedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort(false, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsAndDetails_ConfirmedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort(false, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsOneDetail_ConfirmedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort(false, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccount_ConfirmedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort(true, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountAllDetails_ConfirmedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort(true, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountOneDetail_ConfirmedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort(true, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_ApprovedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort

        [Test]
        public async Task BalanceByNo_AllAccounts_ApprovedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort(false, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsAndDetails_ApprovedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort(false, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsOneDetail_ApprovedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort(false, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccount_ApprovedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort(true, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountAllDetails_ApprovedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort(true, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountOneDetail_ApprovedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort(true, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_AllVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort

        [Test]
        public async Task BalanceByNo_AllAccounts_AllVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort(false, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsAndDetails_AllVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort(false, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsOneDetail_AllVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort(false, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccount_AllVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort(true, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountAllDetails_AllVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort(true, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountOneDetail_AllVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort(true, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_CheckedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort

        [Test]
        public async Task BalanceByNo_AllAccounts_CheckedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort(false, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsAndDetails_CheckedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort(false, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsOneDetail_CheckedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort(false, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccount_CheckedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort(true, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountAllDetails_CheckedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort(true, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountOneDetail_CheckedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort(true, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_FinalizedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort

        [Test]
        public async Task BalanceByNo_AllAccounts_FinalizedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort(false, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsAndDetails_FinalizedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort(false, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsOneDetail_FinalizedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort(false, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccount_FinalizedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort(true, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountAllDetails_FinalizedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort(true, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountOneDetail_FinalizedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort(true, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_ConfirmedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort

        [Test]
        public async Task BalanceByNo_AllAccounts_ConfirmedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort(false, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsAndDetails_ConfirmedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort(false, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsOneDetail_ConfirmedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort(false, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccount_ConfirmedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort(true, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountAllDetails_ConfirmedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort(true, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountOneDetail_ConfirmedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort(true, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_ApprovedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort

        [Test]
        public async Task BalanceByNo_AllAccounts_ApprovedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort(false, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsAndDetails_ApprovedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort(false, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsOneDetail_ApprovedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort(false, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccount_ApprovedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort(true, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountAllDetails_ApprovedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort(true, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountOneDetail_ApprovedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort(true, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_AllVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort

        [Test]
        public async Task BalanceByNo_AllAccounts_AllVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort(false, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsAndDetails_AllVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort(false, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsOneDetail_AllVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort(false, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccount_AllVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort(true, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountAllDetails_AllVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort(true, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountOneDetail_AllVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort(true, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_CheckedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_AllAccounts_CheckedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort(false, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsAndDetails_CheckedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort(false, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsOneDetail_CheckedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort(false, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccount_CheckedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort(true, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountAllDetails_CheckedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort(true, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountOneDetail_CheckedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort(true, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_FinalizedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_AllAccounts_FinalizedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort(false, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsAndDetails_FinalizedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort(false, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsOneDetail_FinalizedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort(false, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccount_FinalizedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort(true, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountAllDetails_FinalizedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort(true, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountOneDetail_FinalizedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort(true, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_ConfirmedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_AllAccounts_ConfirmedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort(false, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsAndDetails_ConfirmedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort(false, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsOneDetail_ConfirmedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort(false, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccount_ConfirmedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort(true, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountAllDetails_ConfirmedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort(true, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountOneDetail_ConfirmedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort(true, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_ApprovedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_AllAccounts_ApprovedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort(false, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsAndDetails_ApprovedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort(false, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsOneDetail_ApprovedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort(false, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccount_ApprovedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort(true, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountAllDetails_ApprovedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort(true, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountOneDetail_ApprovedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort(true, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_AllVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_AllAccounts_AllVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort(false, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsAndDetails_AllVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort(false, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsOneDetail_AllVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort(false, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccount_AllVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort(true, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountAllDetails_AllVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort(true, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountOneDetail_AllVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort(true, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_CheckedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_AllAccounts_CheckedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort(false, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsAndDetails_CheckedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort(false, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsOneDetail_CheckedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort(false, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccount_CheckedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort(true, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountAllDetails_CheckedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort(true, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountOneDetail_CheckedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort(true, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_FinalizedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_AllAccounts_FinalizedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort(false, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsAndDetails_FinalizedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort(false, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsOneDetail_FinalizedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort(false, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccount_FinalizedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort(true, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountAllDetails_FinalizedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort(true, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountOneDetail_FinalizedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort(true, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_AllAccounts_ConfirmedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort(false, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsAndDetails_ConfirmedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort(false, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsOneDetail_ConfirmedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort(false, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccount_ConfirmedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort(true, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountAllDetails_ConfirmedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort(true, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountOneDetail_ConfirmedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort(true, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_ApprovedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_AllAccounts_ApprovedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort(false, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsAndDetails_ApprovedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort(false, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsOneDetail_ApprovedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort(false, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccount_ApprovedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort(true, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountAllDetails_ApprovedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort(true, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountOneDetail_ApprovedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort(true, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_AllVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_AllAccounts_AllVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort(false, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsAndDetails_AllVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort(false, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsOneDetail_AllVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort(false, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccount_AllVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort(true, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountAllDetails_AllVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort(true, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountOneDetail_AllVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort(true, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_CheckedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_AllAccounts_CheckedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort(false, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsAndDetails_CheckedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort(false, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsOneDetail_CheckedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort(false, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccount_CheckedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort(true, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountAllDetails_CheckedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort(true, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountOneDetail_CheckedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort(true, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_FinalizedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_AllAccounts_FinalizedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort(false, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsAndDetails_FinalizedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort(false, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsOneDetail_FinalizedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort(false, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccount_FinalizedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort(true, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountAllDetails_FinalizedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort(true, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountOneDetail_FinalizedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort(true, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_ConfirmedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_AllAccounts_ConfirmedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort(false, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsAndDetails_ConfirmedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort(false, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsOneDetail_ConfirmedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort(false, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccount_ConfirmedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort(true, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountAllDetails_ConfirmedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort(true, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountOneDetail_ConfirmedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort(true, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_ApprovedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_AllAccounts_ApprovedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort(false, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsAndDetails_ApprovedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort(false, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsOneDetail_ApprovedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort(false, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccount_ApprovedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort(true, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountAllDetails_ApprovedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort(true, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountOneDetail_ApprovedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort(true, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_AllVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_AllAccounts_AllVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort(false, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsAndDetails_AllVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort(false, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsOneDetail_AllVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort(false, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccount_AllVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort(true, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountAllDetails_AllVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort(true, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountOneDetail_AllVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort(true, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_CheckedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_AllAccounts_CheckedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort(false, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsAndDetails_CheckedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort(false, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsOneDetail_CheckedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort(false, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccount_CheckedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort(true, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountAllDetails_CheckedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort(true, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountOneDetail_CheckedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort(true, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_FinalizedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_AllAccounts_FinalizedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort(false, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsAndDetails_FinalizedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort(false, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsOneDetail_FinalizedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort(false, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccount_FinalizedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort(true, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountAllDetails_FinalizedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort(true, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountOneDetail_FinalizedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort(true, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_ConfirmedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_AllAccounts_ConfirmedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort(false, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsAndDetails_ConfirmedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort(false, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsOneDetail_ConfirmedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort(false, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccount_ConfirmedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort(true, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountAllDetails_ConfirmedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort(true, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountOneDetail_ConfirmedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort(true, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_ApprovedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_AllAccounts_ApprovedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort(false, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsAndDetails_ApprovedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort(false, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsOneDetail_ApprovedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort(false, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccount_ApprovedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort(true, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountAllDetails_ApprovedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort(true, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountOneDetail_ApprovedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort(true, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_AllVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_AllAccounts_AllVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort(false, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsAndDetails_AllVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort(false, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsOneDetail_AllVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort(false, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccount_AllVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort(true, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountAllDetails_AllVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort(true, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountOneDetail_AllVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort(true, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_CheckedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_AllAccounts_CheckedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort(false, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsAndDetails_CheckedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort(false, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsOneDetail_CheckedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort(false, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccount_CheckedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort(true, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountAllDetails_CheckedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort(true, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountOneDetail_CheckedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort(true, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_FinalizedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_AllAccounts_FinalizedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort(false, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsAndDetails_FinalizedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort(false, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsOneDetail_FinalizedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort(false, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccount_FinalizedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort(true, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountAllDetails_FinalizedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort(true, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountOneDetail_FinalizedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort(true, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_ConfirmedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_AllAccounts_ConfirmedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort(false, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsAndDetails_ConfirmedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort(false, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsOneDetail_ConfirmedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort(false, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccount_ConfirmedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort(true, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountAllDetails_ConfirmedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort(true, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountOneDetail_ConfirmedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort(true, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_ApprovedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_AllAccounts_ApprovedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort(false, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsAndDetails_ApprovedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort(false, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsOneDetail_ApprovedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort(false, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccount_ApprovedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort(true, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountAllDetails_ApprovedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort(true, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountOneDetail_ApprovedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort(true, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_AllVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_AllAccounts_AllVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort(false, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsAndDetails_AllVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort(false, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsOneDetail_AllVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort(false, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccount_AllVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort(true, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountAllDetails_AllVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort(true, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountOneDetail_AllVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort(true, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_CheckedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_AllAccounts_CheckedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort(false, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsAndDetails_CheckedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort(false, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsOneDetail_CheckedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort(false, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccount_CheckedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort(true, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountAllDetails_CheckedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort(true, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountOneDetail_CheckedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort(true, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_FinalizedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_AllAccounts_FinalizedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort(false, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsAndDetails_FinalizedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort(false, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsOneDetail_FinalizedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort(false, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccount_FinalizedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort(true, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountAllDetails_FinalizedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort(true, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountOneDetail_FinalizedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort(true, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_AllAccounts_ConfirmedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort(false, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsAndDetails_ConfirmedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort(false, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsOneDetail_ConfirmedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort(false, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccount_ConfirmedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort(true, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountAllDetails_ConfirmedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort(true, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountOneDetail_ConfirmedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort(true, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_ApprovedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_AllAccounts_ApprovedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort(false, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsAndDetails_ApprovedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort(false, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsOneDetail_ApprovedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort(false, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccount_ApprovedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort(true, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountAllDetails_ApprovedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort(true, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountOneDetail_ApprovedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort(true, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_AllVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_AllAccounts_AllVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort(false, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsAndDetails_AllVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort(false, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsOneDetail_AllVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort(false, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccount_AllVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort(true, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountAllDetails_AllVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort(true, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountOneDetail_AllVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort(true, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_CheckedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_AllAccounts_CheckedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort(false, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsAndDetails_CheckedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort(false, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsOneDetail_CheckedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort(false, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccount_CheckedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort(true, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountAllDetails_CheckedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort(true, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountOneDetail_CheckedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort(true, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_FinalizedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_AllAccounts_FinalizedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort(false, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsAndDetails_FinalizedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort(false, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsOneDetail_FinalizedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort(false, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccount_FinalizedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort(true, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountAllDetails_FinalizedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort(true, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountOneDetail_FinalizedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort(true, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_ConfirmedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_AllAccounts_ConfirmedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort(false, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsAndDetails_ConfirmedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort(false, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsOneDetail_ConfirmedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort(false, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccount_ConfirmedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort(true, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountAllDetails_ConfirmedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort(true, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountOneDetail_ConfirmedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort(true, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_ApprovedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_AllAccounts_ApprovedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort(false, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsAndDetails_ApprovedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort(false, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsOneDetail_ApprovedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort(false, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccount_ApprovedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort(true, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountAllDetails_ApprovedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort(true, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountOneDetail_ApprovedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort(true, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_AllVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_AllAccounts_AllVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort(false, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsAndDetails_AllVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort(false, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsOneDetail_AllVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort(false, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccount_AllVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort(true, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountAllDetails_AllVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort(true, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountOneDetail_AllVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort(true, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_CheckedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_AllAccounts_CheckedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort(false, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsAndDetails_CheckedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort(false, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsOneDetail_CheckedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort(false, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccount_CheckedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort(true, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountAllDetails_CheckedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort(true, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountOneDetail_CheckedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort(true, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_FinalizedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_AllAccounts_FinalizedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort(false, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsAndDetails_FinalizedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort(false, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsOneDetail_FinalizedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort(false, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccount_FinalizedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort(true, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountAllDetails_FinalizedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort(true, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountOneDetail_FinalizedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort(true, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_ConfirmedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_AllAccounts_ConfirmedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort(false, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsAndDetails_ConfirmedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort(false, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsOneDetail_ConfirmedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort(false, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccount_ConfirmedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort(true, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountAllDetails_ConfirmedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort(true, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountOneDetail_ConfirmedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort(true, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_ApprovedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_AllAccounts_ApprovedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort(false, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsAndDetails_ApprovedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort(false, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsOneDetail_ApprovedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort(false, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccount_ApprovedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort(true, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountAllDetails_ApprovedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort(true, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountOneDetail_ApprovedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort(true, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_AllVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_AllAccounts_AllVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort(false, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsAndDetails_AllVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort(false, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsOneDetail_AllVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort(false, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccount_AllVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort(true, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountAllDetails_AllVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort(true, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountOneDetail_AllVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort(true, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_CheckedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_AllAccounts_CheckedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort(false, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsAndDetails_CheckedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort(false, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsOneDetail_CheckedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort(false, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccount_CheckedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort(true, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountAllDetails_CheckedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort(true, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountOneDetail_CheckedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort(true, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_FinalizedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_AllAccounts_FinalizedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort(false, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsAndDetails_FinalizedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort(false, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsOneDetail_FinalizedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort(false, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccount_FinalizedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort(true, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountAllDetails_FinalizedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort(true, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountOneDetail_FinalizedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort(true, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_ConfirmedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_AllAccounts_ConfirmedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort(false, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsAndDetails_ConfirmedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort(false, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsOneDetail_ConfirmedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort(false, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccount_ConfirmedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort(true, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountAllDetails_ConfirmedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort(true, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountOneDetail_ConfirmedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort(true, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_ApprovedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_AllAccounts_ApprovedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort(false, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsAndDetails_ApprovedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort(false, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsOneDetail_ApprovedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort(false, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccount_ApprovedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort(true, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountAllDetails_ApprovedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort(true, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountOneDetail_ApprovedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort(true, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_AllVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_AllAccounts_AllVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort(false, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsAndDetails_AllVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort(false, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsOneDetail_AllVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort(false, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccount_AllVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort(true, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountAllDetails_AllVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort(true, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountOneDetail_AllVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort(true, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_CheckedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_AllAccounts_CheckedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort(false, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsAndDetails_CheckedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort(false, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsOneDetail_CheckedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort(false, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccount_CheckedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort(true, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountAllDetails_CheckedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort(true, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountOneDetail_CheckedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort(true, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_FinalizedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_AllAccounts_FinalizedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort(false, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsAndDetails_FinalizedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort(false, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsOneDetail_FinalizedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort(false, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccount_FinalizedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort(true, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountAllDetails_FinalizedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort(true, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountOneDetail_FinalizedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort(true, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_AllAccounts_ConfirmedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort(false, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsAndDetails_ConfirmedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort(false, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsOneDetail_ConfirmedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort(false, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccount_ConfirmedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort(true, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountAllDetails_ConfirmedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort(true, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountOneDetail_ConfirmedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort(true, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_ApprovedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_AllAccounts_ApprovedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort(false, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsAndDetails_ApprovedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort(false, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsOneDetail_ApprovedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort(false, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccount_ApprovedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort(true, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountAllDetails_ApprovedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort(true, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountOneDetail_ApprovedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort(true, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_AllVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_AllAccounts_AllVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort(false, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsAndDetails_AllVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort(false, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsOneDetail_AllVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort(false, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccount_AllVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort(true, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountAllDetails_AllVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort(true, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountOneDetail_AllVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort(true, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_CheckedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_AllAccounts_CheckedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort(false, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsAndDetails_CheckedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort(false, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsOneDetail_CheckedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort(false, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccount_CheckedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort(true, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountAllDetails_CheckedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort(true, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountOneDetail_CheckedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort(true, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_FinalizedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_AllAccounts_FinalizedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort(false, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsAndDetails_FinalizedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort(false, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsOneDetail_FinalizedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort(false, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccount_FinalizedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort(true, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountAllDetails_FinalizedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort(true, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountOneDetail_FinalizedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort(true, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_ConfirmedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_AllAccounts_ConfirmedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort(false, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsAndDetails_ConfirmedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort(false, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsOneDetail_ConfirmedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort(false, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccount_ConfirmedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort(true, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountAllDetails_ConfirmedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort(true, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountOneDetail_ConfirmedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort(true, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_ApprovedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_AllAccounts_ApprovedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort(false, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsAndDetails_ApprovedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort(false, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsOneDetail_ApprovedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort(false, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccount_ApprovedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort(true, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountAllDetails_ApprovedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort(true, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountOneDetail_ApprovedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort(true, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_AllVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_AllAccounts_AllVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort(false, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsAndDetails_AllVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort(false, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsOneDetail_AllVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort(false, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccount_AllVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort(true, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountAllDetails_AllVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort(true, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountOneDetail_AllVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort(true, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_CheckedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_AllAccounts_CheckedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort(false, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsAndDetails_CheckedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort(false, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsOneDetail_CheckedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort(false, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccount_CheckedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort(true, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountAllDetails_CheckedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort(true, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountOneDetail_CheckedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort(true, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_FinalizedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_AllAccounts_FinalizedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort(false, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsAndDetails_FinalizedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort(false, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsOneDetail_FinalizedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort(false, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccount_FinalizedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort(true, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountAllDetails_FinalizedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort(true, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountOneDetail_FinalizedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort(true, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_ConfirmedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_AllAccounts_ConfirmedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort(false, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsAndDetails_ConfirmedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort(false, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsOneDetail_ConfirmedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort(false, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccount_ConfirmedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort(true, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountAllDetails_ConfirmedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort(true, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountOneDetail_ConfirmedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort(true, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_ApprovedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_AllAccounts_ApprovedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort(false, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsAndDetails_ApprovedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort(false, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsOneDetail_ApprovedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort(false, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccount_ApprovedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort(true, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountAllDetails_ApprovedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort(true, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountOneDetail_ApprovedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort(true, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_AllVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_AllAccounts_AllVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort(false, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsAndDetails_AllVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort(false, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsOneDetail_AllVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort(false, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccount_AllVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort(true, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountAllDetails_AllVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort(true, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountOneDetail_AllVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort(true, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_CheckedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_AllAccounts_CheckedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort(false, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsAndDetails_CheckedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort(false, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsOneDetail_CheckedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort(false, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccount_CheckedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort(true, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountAllDetails_CheckedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort(true, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountOneDetail_CheckedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort(true, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_FinalizedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_AllAccounts_FinalizedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort(false, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsAndDetails_FinalizedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort(false, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsOneDetail_FinalizedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort(false, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccount_FinalizedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort(true, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountAllDetails_FinalizedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort(true, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountOneDetail_FinalizedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort(true, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_ConfirmedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_AllAccounts_ConfirmedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort(false, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsAndDetails_ConfirmedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort(false, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsOneDetail_ConfirmedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort(false, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccount_ConfirmedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort(true, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountAllDetails_ConfirmedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort(true, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountOneDetail_ConfirmedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort(true, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_ApprovedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_AllAccounts_ApprovedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort(false, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsAndDetails_ApprovedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort(false, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsOneDetail_ApprovedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort(false, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccount_ApprovedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort(true, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountAllDetails_ApprovedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort(true, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountOneDetail_ApprovedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort(true, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_AllVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_AllAccounts_AllVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort(false, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsAndDetails_AllVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort(false, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsOneDetail_AllVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort(false, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccount_AllVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort(true, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountAllDetails_AllVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort(true, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountOneDetail_AllVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort(true, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_CheckedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_AllAccounts_CheckedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort(false, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsAndDetails_CheckedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort(false, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsOneDetail_CheckedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort(false, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccount_CheckedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort(true, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountAllDetails_CheckedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort(true, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountOneDetail_CheckedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort(true, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_FinalizedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_AllAccounts_FinalizedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort(false, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsAndDetails_FinalizedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort(false, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsOneDetail_FinalizedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort(false, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccount_FinalizedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort(true, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountAllDetails_FinalizedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort(true, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountOneDetail_FinalizedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort(true, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_AllAccounts_ConfirmedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort(false, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsAndDetails_ConfirmedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort(false, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsOneDetail_ConfirmedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort(false, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccount_ConfirmedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort(true, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountAllDetails_ConfirmedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort(true, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountOneDetail_ConfirmedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort(true, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_ApprovedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_AllAccounts_ApprovedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort(false, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsAndDetails_ApprovedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort(false, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsOneDetail_ApprovedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort(false, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccount_ApprovedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort(true, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountAllDetails_ApprovedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort(true, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountOneDetail_ApprovedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort(true, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_AllVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_AllAccounts_AllVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort(false, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsAndDetails_AllVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort(false, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsOneDetail_AllVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort(false, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccount_AllVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort(true, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountAllDetails_AllVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort(true, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountOneDetail_AllVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort(true, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_CheckedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_AllAccounts_CheckedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort(false, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsAndDetails_CheckedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort(false, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsOneDetail_CheckedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort(false, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccount_CheckedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort(true, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountAllDetails_CheckedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort(true, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountOneDetail_CheckedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort(true, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_FinalizedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_AllAccounts_FinalizedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort(false, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsAndDetails_FinalizedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort(false, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsOneDetail_FinalizedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort(false, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccount_FinalizedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort(true, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountAllDetails_FinalizedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort(true, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountOneDetail_FinalizedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort(true, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_ConfirmedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_AllAccounts_ConfirmedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort(false, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsAndDetails_ConfirmedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort(false, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsOneDetail_ConfirmedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort(false, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccount_ConfirmedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort(true, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountAllDetails_ConfirmedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort(true, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountOneDetail_ConfirmedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort(true, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_ApprovedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_AllAccounts_ApprovedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort(false, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsAndDetails_ApprovedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort(false, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsOneDetail_ApprovedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort(false, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccount_ApprovedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort(true, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountAllDetails_ApprovedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort(true, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountOneDetail_ApprovedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort(true, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_AllVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_AllAccounts_AllVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort(false, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsAndDetails_AllVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort(false, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsOneDetail_AllVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort(false, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccount_AllVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort(true, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountAllDetails_AllVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort(true, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountOneDetail_AllVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort(true, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_CheckedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_AllAccounts_CheckedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort(false, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsAndDetails_CheckedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort(false, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsOneDetail_CheckedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort(false, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccount_CheckedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort(true, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountAllDetails_CheckedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort(true, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountOneDetail_CheckedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort(true, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_FinalizedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_AllAccounts_FinalizedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort(false, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsAndDetails_FinalizedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort(false, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsOneDetail_FinalizedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort(false, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccount_FinalizedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort(true, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountAllDetails_FinalizedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort(true, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountOneDetail_FinalizedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort(true, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_ConfirmedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_AllAccounts_ConfirmedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort(false, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsAndDetails_ConfirmedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort(false, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsOneDetail_ConfirmedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort(false, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccount_ConfirmedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort(true, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountAllDetails_ConfirmedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort(true, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountOneDetail_ConfirmedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort(true, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_ApprovedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_AllAccounts_ApprovedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort(false, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsAndDetails_ApprovedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort(false, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsOneDetail_ApprovedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort(false, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccount_ApprovedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort(true, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountAllDetails_ApprovedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort(true, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountOneDetail_ApprovedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort(true, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_AllVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_AllAccounts_AllVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort(false, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsAndDetails_AllVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort(false, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsOneDetail_AllVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort(false, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccount_AllVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort(true, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountAllDetails_AllVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort(true, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountOneDetail_AllVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort(true, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_CheckedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_AllAccounts_CheckedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(false, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsAndDetails_CheckedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(false, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsOneDetail_CheckedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(false, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccount_CheckedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(true, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountAllDetails_CheckedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(true, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountOneDetail_CheckedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(true, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_FinalizedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_AllAccounts_FinalizedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(false, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsAndDetails_FinalizedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(false, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsOneDetail_FinalizedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(false, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccount_FinalizedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(true, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountAllDetails_FinalizedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(true, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountOneDetail_FinalizedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(true, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_ConfirmedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_AllAccounts_ConfirmedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(false, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsAndDetails_ConfirmedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(false, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsOneDetail_ConfirmedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(false, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccount_ConfirmedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(true, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountAllDetails_ConfirmedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(true, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountOneDetail_ConfirmedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(true, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_ApprovedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_AllAccounts_ApprovedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(false, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsAndDetails_ApprovedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(false, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsOneDetail_ApprovedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(false, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccount_ApprovedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(true, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountAllDetails_ApprovedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(true, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountOneDetail_ApprovedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(true, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_AllVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_AllAccounts_AllVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(false, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsAndDetails_AllVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(false, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsOneDetail_AllVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(false, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccount_AllVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(true, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountAllDetails_AllVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(true, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountOneDetail_AllVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(true, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_CheckedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_AllAccounts_CheckedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(false, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsAndDetails_CheckedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(false, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsOneDetail_CheckedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(false, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccount_CheckedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(true, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountAllDetails_CheckedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(true, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountOneDetail_CheckedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(true, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_FinalizedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_AllAccounts_FinalizedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(false, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsAndDetails_FinalizedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(false, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsOneDetail_FinalizedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(false, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccount_FinalizedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(true, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountAllDetails_FinalizedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(true, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountOneDetail_FinalizedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(true, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_AllAccounts_ConfirmedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(false, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsAndDetails_ConfirmedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(false, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsOneDetail_ConfirmedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(false, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccount_ConfirmedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(true, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountAllDetails_ConfirmedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(true, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountOneDetail_ConfirmedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(true, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_ApprovedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_AllAccounts_ApprovedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(false, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsAndDetails_ApprovedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(false, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsOneDetail_ApprovedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(false, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccount_ApprovedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(true, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountAllDetails_ApprovedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(true, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountOneDetail_ApprovedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(true, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_AllVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_AllAccounts_AllVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(false, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsAndDetails_AllVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(false, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsOneDetail_AllVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(false, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccount_AllVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(true, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountAllDetails_AllVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(true, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountOneDetail_AllVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(true, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_CheckedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_AllAccounts_CheckedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort(false, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsAndDetails_CheckedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort(false, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsOneDetail_CheckedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort(false, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccount_CheckedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort(true, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountAllDetails_CheckedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort(true, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountOneDetail_CheckedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort(true, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_FinalizedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_AllAccounts_FinalizedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort(false, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsAndDetails_FinalizedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort(false, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsOneDetail_FinalizedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort(false, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccount_FinalizedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort(true, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountAllDetails_FinalizedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort(true, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountOneDetail_FinalizedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort(true, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_ConfirmedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_AllAccounts_ConfirmedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort(false, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsAndDetails_ConfirmedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort(false, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsOneDetail_ConfirmedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort(false, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccount_ConfirmedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort(true, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountAllDetails_ConfirmedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort(true, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountOneDetail_ConfirmedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort(true, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_ApprovedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_AllAccounts_ApprovedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort(false, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsAndDetails_ApprovedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort(false, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsOneDetail_ApprovedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort(false, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccount_ApprovedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort(true, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountAllDetails_ApprovedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort(true, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountOneDetail_ApprovedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort(true, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_AllVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_AllAccounts_AllVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort(false, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsAndDetails_AllVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort(false, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsOneDetail_AllVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort(false, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccount_AllVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort(true, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountAllDetails_AllVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort(true, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountOneDetail_AllVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort(true, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_CheckedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_AllAccounts_CheckedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort(false, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsAndDetails_CheckedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort(false, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsOneDetail_CheckedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort(false, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccount_CheckedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort(true, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountAllDetails_CheckedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort(true, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountOneDetail_CheckedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort(true, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_FinalizedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_AllAccounts_FinalizedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort(false, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsAndDetails_FinalizedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort(false, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsOneDetail_FinalizedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort(false, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccount_FinalizedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort(true, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountAllDetails_FinalizedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort(true, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountOneDetail_FinalizedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort(true, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_ConfirmedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_AllAccounts_ConfirmedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort(false, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsAndDetails_ConfirmedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort(false, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsOneDetail_ConfirmedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort(false, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccount_ConfirmedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort(true, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountAllDetails_ConfirmedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort(true, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountOneDetail_ConfirmedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort(true, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_ApprovedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_AllAccounts_ApprovedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort(false, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsAndDetails_ApprovedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort(false, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsOneDetail_ApprovedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort(false, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccount_ApprovedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort(true, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountAllDetails_ApprovedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort(true, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountOneDetail_ApprovedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort(true, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_AllVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_AllAccounts_AllVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort(false, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsAndDetails_AllVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort(false, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsOneDetail_AllVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort(false, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccount_AllVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort(true, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountAllDetails_AllVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort(true, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountOneDetail_AllVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort(true, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_CheckedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort

        [Test]
        public async Task BalanceByNo_AllAccounts_CheckedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort(false, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsAndDetails_CheckedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort(false, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsOneDetail_CheckedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort(false, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccount_CheckedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort(true, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountAllDetails_CheckedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort(true, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountOneDetail_CheckedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort(true, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_FinalizedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort

        [Test]
        public async Task BalanceByNo_AllAccounts_FinalizedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort(false, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsAndDetails_FinalizedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort(false, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsOneDetail_FinalizedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort(false, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccount_FinalizedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort(true, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountAllDetails_FinalizedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort(true, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountOneDetail_FinalizedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort(true, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_ConfirmedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort

        [Test]
        public async Task BalanceByNo_AllAccounts_ConfirmedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort(false, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsAndDetails_ConfirmedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort(false, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsOneDetail_ConfirmedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort(false, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccount_ConfirmedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort(true, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountAllDetails_ConfirmedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort(true, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountOneDetail_ConfirmedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort(true, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_ApprovedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort

        [Test]
        public async Task BalanceByNo_AllAccounts_ApprovedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort(false, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsAndDetails_ApprovedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort(false, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsOneDetail_ApprovedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort(false, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccount_ApprovedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort(true, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountAllDetails_ApprovedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort(true, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountOneDetail_ApprovedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort(true, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_AllVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort

        [Test]
        public async Task BalanceByNo_AllAccounts_AllVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort(false, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsAndDetails_AllVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort(false, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsOneDetail_AllVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort(false, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccount_AllVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort(true, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountAllDetails_AllVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort(true, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountOneDetail_AllVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort(true, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_CheckedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort

        [Test]
        public async Task BalanceByNo_AllAccounts_CheckedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort(false, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsAndDetails_CheckedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort(false, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsOneDetail_CheckedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort(false, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccount_CheckedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort(true, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountAllDetails_CheckedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort(true, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountOneDetail_CheckedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort(true, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_FinalizedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort

        [Test]
        public async Task BalanceByNo_AllAccounts_FinalizedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort(false, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsAndDetails_FinalizedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort(false, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsOneDetail_FinalizedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort(false, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccount_FinalizedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort(true, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountAllDetails_FinalizedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort(true, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountOneDetail_FinalizedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort(true, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort

        [Test]
        public async Task BalanceByNo_AllAccounts_ConfirmedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort(false, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsAndDetails_ConfirmedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort(false, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsOneDetail_ConfirmedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort(false, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccount_ConfirmedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort(true, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountAllDetails_ConfirmedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort(true, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountOneDetail_ConfirmedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort(true, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_ApprovedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort

        [Test]
        public async Task BalanceByNo_AllAccounts_ApprovedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort(false, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsAndDetails_ApprovedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort(false, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsOneDetail_ApprovedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort(false, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccount_ApprovedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort(true, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountAllDetails_ApprovedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort(true, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountOneDetail_ApprovedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort(true, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_AllVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort

        [Test]
        public async Task BalanceByNo_AllAccounts_AllVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort(false, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsAndDetails_AllVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort(false, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsOneDetail_AllVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort(false, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccount_AllVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort(true, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountAllDetails_AllVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort(true, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountOneDetail_AllVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort(true, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_CheckedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort

        [Test]
        public async Task BalanceByNo_AllAccounts_CheckedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort(false, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsAndDetails_CheckedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort(false, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsOneDetail_CheckedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort(false, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccount_CheckedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort(true, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountAllDetails_CheckedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort(true, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountOneDetail_CheckedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort(true, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_FinalizedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort

        [Test]
        public async Task BalanceByNo_AllAccounts_FinalizedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort(false, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsAndDetails_FinalizedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort(false, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsOneDetail_FinalizedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort(false, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccount_FinalizedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort(true, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountAllDetails_FinalizedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort(true, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountOneDetail_FinalizedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort(true, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_ConfirmedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort

        [Test]
        public async Task BalanceByNo_AllAccounts_ConfirmedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort(false, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsAndDetails_ConfirmedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort(false, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsOneDetail_ConfirmedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort(false, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccount_ConfirmedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort(true, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountAllDetails_ConfirmedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort(true, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountOneDetail_ConfirmedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort(true, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_ApprovedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort

        [Test]
        public async Task BalanceByNo_AllAccounts_ApprovedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort(false, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsAndDetails_ApprovedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort(false, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsOneDetail_ApprovedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort(false, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccount_ApprovedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort(true, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountAllDetails_ApprovedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort(true, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountOneDetail_ApprovedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort(true, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_AllVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort

        [Test]
        public async Task BalanceByNo_AllAccounts_AllVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort(false, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsAndDetails_AllVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort(false, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsOneDetail_AllVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort(false, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccount_AllVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort(true, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountAllDetails_AllVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort(true, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountOneDetail_AllVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort(true, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_CheckedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort

        [Test]
        public async Task BalanceByNo_AllAccounts_CheckedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort(false, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsAndDetails_CheckedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort(false, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsOneDetail_CheckedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort(false, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccount_CheckedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort(true, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountAllDetails_CheckedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort(true, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountOneDetail_CheckedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_CheckedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort(true, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_FinalizedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort

        [Test]
        public async Task BalanceByNo_AllAccounts_FinalizedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort(false, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsAndDetails_FinalizedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort(false, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsOneDetail_FinalizedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort(false, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccount_FinalizedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort(true, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountAllDetails_FinalizedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort(true, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountOneDetail_FinalizedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort(true, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_ConfirmedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort

        [Test]
        public async Task BalanceByNo_AllAccounts_ConfirmedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort(false, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsAndDetails_ConfirmedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort(false, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsOneDetail_ConfirmedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort(false, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccount_ConfirmedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort(true, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountAllDetails_ConfirmedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort(true, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountOneDetail_ConfirmedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort(true, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_ApprovedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort

        [Test]
        public async Task BalanceByNo_AllAccounts_ApprovedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort(false, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsAndDetails_ApprovedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort(false, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsOneDetail_ApprovedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort(false, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccount_ApprovedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort(true, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountAllDetails_ApprovedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort(true, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountOneDetail_ApprovedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort(true, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region Balance_AllVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort

        [Test]
        public async Task BalanceByNo_AllAccounts_AllVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort(false, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsAndDetails_AllVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort(false, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_AllAccountsOneDetail_AllVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort(false, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccount_AllVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort(true, null, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountAllDetails_AllVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort(true, false, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_OneAccountOneDetail_AllVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetBalance_AllVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort(true, true, false);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #endregion

        private static FinanceReportOptions GetFormOptions()
        {
            return FinanceReportOptions.UseClosingVoucher
                | FinanceReportOptions.UseClosingTempVoucher;
        }

        private static FinanceReportOptions GetFormAndOpeningOptions()
        {
            return FinanceReportOptions.UseClosingVoucher
                | FinanceReportOptions.UseClosingTempVoucher
                | FinanceReportOptions.OpeningAsFirstVoucher;
        }

        private static FinanceReportOptions GetFormAndTurnoverOptions()
        {
            return FinanceReportOptions.UseClosingVoucher
                | FinanceReportOptions.UseClosingTempVoucher
                | FinanceReportOptions.StartTurnoverAsInitBalance;
        }

        private static FinanceReportOptions GetOpeningAndTurnoverOptions()
        {
            return FinanceReportOptions.OpeningAsFirstVoucher
                | FinanceReportOptions.StartTurnoverAsInitBalance;
        }

        private void SetUp()
        {
            var repoContext = GetRepositoryContext();
            var sysRepo = GetSystemRepository(repoContext);
            var utility = new ReportDirectUtility(repoContext, sysRepo);
            _repository = new BalanceByAccountRepositoryDirect(repoContext, sysRepo, utility);
        }

        #region GetBalance_CurrentBranch_NoGrouping_NoOptions_NoFilterSort

        private async Task<BalanceByAccountViewModel> GetBalance_CheckedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort(
            bool byOneAccount, bool? byOneDetail, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseCheckedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, FinanceReportOptions.None, byOneAccount, byOneDetail)
                : GetByNoTestParameters(gridOptions, FinanceReportOptions.None, byOneAccount, byOneDetail);

            return await _repository.GetBalanceByAccountAsync(parameters);
        }

        private async Task<BalanceByAccountViewModel> GetBalance_FinalizedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort(
            bool byOneAccount, bool? byOneDetail, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseFinalizedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, FinanceReportOptions.None, byOneAccount, byOneDetail)
                : GetByNoTestParameters(gridOptions, FinanceReportOptions.None, byOneAccount, byOneDetail);

            return await _repository.GetBalanceByAccountAsync(parameters);
        }

        private async Task<BalanceByAccountViewModel> GetBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort(
            bool byOneAccount, bool? byOneDetail, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseConfirmedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, FinanceReportOptions.None, byOneAccount, byOneDetail)
                : GetByNoTestParameters(gridOptions, FinanceReportOptions.None, byOneAccount, byOneDetail);

            return await _repository.GetBalanceByAccountAsync(parameters);
        }

        private async Task<BalanceByAccountViewModel> GetBalance_ApprovedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort(
            bool byOneAccount, bool? byOneDetail, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseApprovedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, FinanceReportOptions.None, byOneAccount, byOneDetail)
                : GetByNoTestParameters(gridOptions, FinanceReportOptions.None, byOneAccount, byOneDetail);

            return await _repository.GetBalanceByAccountAsync(parameters);
        }

        private async Task<BalanceByAccountViewModel> GetBalance_AllVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort(
            bool byOneAccount, bool? byOneDetail, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, FinanceReportOptions.None, byOneAccount, byOneDetail)
                : GetByNoTestParameters(gridOptions, FinanceReportOptions.None, byOneAccount, byOneDetail);

            return await _repository.GetBalanceByAccountAsync(parameters);
        }

        #endregion

        #region GetBalance_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort

        private async Task<BalanceByAccountViewModel> GetBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort(
            bool byOneAccount, bool? byOneDetail, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .UseCheckedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, FinanceReportOptions.None, byOneAccount, byOneDetail)
                : GetByNoTestParameters(gridOptions, FinanceReportOptions.None, byOneAccount, byOneDetail);

            return await _repository.GetBalanceByAccountAsync(parameters);
        }

        private async Task<BalanceByAccountViewModel> GetBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort(
            bool byOneAccount, bool? byOneDetail, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .UseFinalizedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, FinanceReportOptions.None, byOneAccount, byOneDetail)
                : GetByNoTestParameters(gridOptions, FinanceReportOptions.None, byOneAccount, byOneDetail);

            return await _repository.GetBalanceByAccountAsync(parameters);
        }

        private async Task<BalanceByAccountViewModel> GetBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort(
            bool byOneAccount, bool? byOneDetail, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .UseConfirmedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, FinanceReportOptions.None, byOneAccount, byOneDetail)
                : GetByNoTestParameters(gridOptions, FinanceReportOptions.None, byOneAccount, byOneDetail);

            return await _repository.GetBalanceByAccountAsync(parameters);
        }

        private async Task<BalanceByAccountViewModel> GetBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort(
            bool byOneAccount, bool? byOneDetail, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .UseApprovedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, FinanceReportOptions.None, byOneAccount, byOneDetail)
                : GetByNoTestParameters(gridOptions, FinanceReportOptions.None, byOneAccount, byOneDetail);

            return await _repository.GetBalanceByAccountAsync(parameters);
        }

        private async Task<BalanceByAccountViewModel> GetBalance_AllVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort(
            bool byOneAccount, bool? byOneDetail, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, FinanceReportOptions.None, byOneAccount, byOneDetail)
                : GetByNoTestParameters(gridOptions, FinanceReportOptions.None, byOneAccount, byOneDetail);

            return await _repository.GetBalanceByAccountAsync(parameters);
        }

        #endregion

        #region GetBalance_CurrentBranch_ByBranch_NoOptions_NoFilterSort

        private async Task<BalanceByAccountViewModel> GetBalance_CheckedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort(
            bool byOneAccount, bool? byOneDetail, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseCheckedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, FinanceReportOptions.None, byOneAccount, byOneDetail, true)
                : GetByNoTestParameters(gridOptions, FinanceReportOptions.None, byOneAccount, byOneDetail, true);

            return await _repository.GetBalanceByAccountAsync(parameters);
        }

        private async Task<BalanceByAccountViewModel> GetBalance_FinalizedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort(
            bool byOneAccount, bool? byOneDetail, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseFinalizedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, FinanceReportOptions.None, byOneAccount, byOneDetail, true)
                : GetByNoTestParameters(gridOptions, FinanceReportOptions.None, byOneAccount, byOneDetail, true);

            return await _repository.GetBalanceByAccountAsync(parameters);
        }

        private async Task<BalanceByAccountViewModel> GetBalance_ConfirmedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort(
            bool byOneAccount, bool? byOneDetail, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseConfirmedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, FinanceReportOptions.None, byOneAccount, byOneDetail, true)
                : GetByNoTestParameters(gridOptions, FinanceReportOptions.None, byOneAccount, byOneDetail, true);

            return await _repository.GetBalanceByAccountAsync(parameters);
        }

        private async Task<BalanceByAccountViewModel> GetBalance_ApprovedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort(
            bool byOneAccount, bool? byOneDetail, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseApprovedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, FinanceReportOptions.None, byOneAccount, byOneDetail, true)
                : GetByNoTestParameters(gridOptions, FinanceReportOptions.None, byOneAccount, byOneDetail, true);

            return await _repository.GetBalanceByAccountAsync(parameters);
        }

        private async Task<BalanceByAccountViewModel> GetBalance_AllVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort(
            bool byOneAccount, bool? byOneDetail, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, FinanceReportOptions.None, byOneAccount, byOneDetail, true)
                : GetByNoTestParameters(gridOptions, FinanceReportOptions.None, byOneAccount, byOneDetail, true);

            return await _repository.GetBalanceByAccountAsync(parameters);
        }

        #endregion

        #region GetBalance_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort

        private async Task<BalanceByAccountViewModel> GetBalance_CheckedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort(
            bool byOneAccount, bool? byOneDetail, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .UseCheckedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, FinanceReportOptions.None, byOneAccount, byOneDetail, true)
                : GetByNoTestParameters(gridOptions, FinanceReportOptions.None, byOneAccount, byOneDetail, true);

            return await _repository.GetBalanceByAccountAsync(parameters);
        }

        private async Task<BalanceByAccountViewModel> GetBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort(
            bool byOneAccount, bool? byOneDetail, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .UseFinalizedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, FinanceReportOptions.None, byOneAccount, byOneDetail, true)
                : GetByNoTestParameters(gridOptions, FinanceReportOptions.None, byOneAccount, byOneDetail, true);

            return await _repository.GetBalanceByAccountAsync(parameters);
        }

        private async Task<BalanceByAccountViewModel> GetBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort(
            bool byOneAccount, bool? byOneDetail, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .UseConfirmedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, FinanceReportOptions.None, byOneAccount, byOneDetail, true)
                : GetByNoTestParameters(gridOptions, FinanceReportOptions.None, byOneAccount, byOneDetail, true);

            return await _repository.GetBalanceByAccountAsync(parameters);
        }

        private async Task<BalanceByAccountViewModel> GetBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort(
            bool byOneAccount, bool? byOneDetail, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .UseApprovedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, FinanceReportOptions.None, byOneAccount, byOneDetail, true)
                : GetByNoTestParameters(gridOptions, FinanceReportOptions.None, byOneAccount, byOneDetail, true);

            return await _repository.GetBalanceByAccountAsync(parameters);
        }

        private async Task<BalanceByAccountViewModel> GetBalance_AllVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort(
            bool byOneAccount, bool? byOneDetail, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, FinanceReportOptions.None, byOneAccount, byOneDetail, true)
                : GetByNoTestParameters(gridOptions, FinanceReportOptions.None, byOneAccount, byOneDetail, true);

            return await _repository.GetBalanceByAccountAsync(parameters);
        }

        #endregion

        #region GetBalance_CurrentBranch_NoGrouping_FormOptions_NoFilterSort

        private async Task<BalanceByAccountViewModel> GetBalance_CheckedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort(
            bool byOneAccount, bool? byOneDetail, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseCheckedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, GetFormOptions(), byOneAccount, byOneDetail)
                : GetByNoTestParameters(gridOptions, GetFormOptions(), byOneAccount, byOneDetail);

            return await _repository.GetBalanceByAccountAsync(parameters);
        }

        private async Task<BalanceByAccountViewModel> GetBalance_FinalizedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort(
            bool byOneAccount, bool? byOneDetail, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseFinalizedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, GetFormOptions(), byOneAccount, byOneDetail)
                : GetByNoTestParameters(gridOptions, GetFormOptions(), byOneAccount, byOneDetail);

            return await _repository.GetBalanceByAccountAsync(parameters);
        }

        private async Task<BalanceByAccountViewModel> GetBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort(
            bool byOneAccount, bool? byOneDetail, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseConfirmedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, GetFormOptions(), byOneAccount, byOneDetail)
                : GetByNoTestParameters(gridOptions, GetFormOptions(), byOneAccount, byOneDetail);

            return await _repository.GetBalanceByAccountAsync(parameters);
        }

        private async Task<BalanceByAccountViewModel> GetBalance_ApprovedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort(
            bool byOneAccount, bool? byOneDetail, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseApprovedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, GetFormOptions(), byOneAccount, byOneDetail)
                : GetByNoTestParameters(gridOptions, GetFormOptions(), byOneAccount, byOneDetail);

            return await _repository.GetBalanceByAccountAsync(parameters);
        }

        private async Task<BalanceByAccountViewModel> GetBalance_AllVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort(
            bool byOneAccount, bool? byOneDetail, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, GetFormOptions(), byOneAccount, byOneDetail)
                : GetByNoTestParameters(gridOptions, GetFormOptions(), byOneAccount, byOneDetail);

            return await _repository.GetBalanceByAccountAsync(parameters);
        }

        #endregion

        #region GetBalance_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort

        private async Task<BalanceByAccountViewModel> GetBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort(
            bool byOneAccount, bool? byOneDetail, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .UseCheckedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, GetFormOptions(), byOneAccount, byOneDetail)
                : GetByNoTestParameters(gridOptions, GetFormOptions(), byOneAccount, byOneDetail);

            return await _repository.GetBalanceByAccountAsync(parameters);
        }

        private async Task<BalanceByAccountViewModel> GetBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort(
            bool byOneAccount, bool? byOneDetail, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .UseFinalizedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, GetFormOptions(), byOneAccount, byOneDetail)
                : GetByNoTestParameters(gridOptions, GetFormOptions(), byOneAccount, byOneDetail);

            return await _repository.GetBalanceByAccountAsync(parameters);
        }

        private async Task<BalanceByAccountViewModel> GetBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort(
            bool byOneAccount, bool? byOneDetail, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .UseConfirmedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, GetFormOptions(), byOneAccount, byOneDetail)
                : GetByNoTestParameters(gridOptions, GetFormOptions(), byOneAccount, byOneDetail);

            return await _repository.GetBalanceByAccountAsync(parameters);
        }

        private async Task<BalanceByAccountViewModel> GetBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort(
            bool byOneAccount, bool? byOneDetail, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .UseApprovedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, GetFormOptions(), byOneAccount, byOneDetail)
                : GetByNoTestParameters(gridOptions, GetFormOptions(), byOneAccount, byOneDetail);

            return await _repository.GetBalanceByAccountAsync(parameters);
        }

        private async Task<BalanceByAccountViewModel> GetBalance_AllVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort(
            bool byOneAccount, bool? byOneDetail, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, GetFormOptions(), byOneAccount, byOneDetail)
                : GetByNoTestParameters(gridOptions, GetFormOptions(), byOneAccount, byOneDetail);

            return await _repository.GetBalanceByAccountAsync(parameters);
        }

        #endregion

        #region GetBalance_CurrentBranch_ByBranch_FormOptions_NoFilterSort

        private async Task<BalanceByAccountViewModel> GetBalance_CheckedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort(
            bool byOneAccount, bool? byOneDetail, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseCheckedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, GetFormOptions(), byOneAccount, byOneDetail, true)
                : GetByNoTestParameters(gridOptions, GetFormOptions(), byOneAccount, byOneDetail, true);

            return await _repository.GetBalanceByAccountAsync(parameters);
        }

        private async Task<BalanceByAccountViewModel> GetBalance_FinalizedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort(
            bool byOneAccount, bool? byOneDetail, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseFinalizedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, GetFormOptions(), byOneAccount, byOneDetail, true)
                : GetByNoTestParameters(gridOptions, GetFormOptions(), byOneAccount, byOneDetail, true);

            return await _repository.GetBalanceByAccountAsync(parameters);
        }

        private async Task<BalanceByAccountViewModel> GetBalance_ConfirmedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort(
            bool byOneAccount, bool? byOneDetail, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseConfirmedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, GetFormOptions(), byOneAccount, byOneDetail, true)
                : GetByNoTestParameters(gridOptions, GetFormOptions(), byOneAccount, byOneDetail, true);

            return await _repository.GetBalanceByAccountAsync(parameters);
        }

        private async Task<BalanceByAccountViewModel> GetBalance_ApprovedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort(
            bool byOneAccount, bool? byOneDetail, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseApprovedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, GetFormOptions(), byOneAccount, byOneDetail, true)
                : GetByNoTestParameters(gridOptions, GetFormOptions(), byOneAccount, byOneDetail, true);

            return await _repository.GetBalanceByAccountAsync(parameters);
        }

        private async Task<BalanceByAccountViewModel> GetBalance_AllVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort(
            bool byOneAccount, bool? byOneDetail, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, GetFormOptions(), byOneAccount, byOneDetail, true)
                : GetByNoTestParameters(gridOptions, GetFormOptions(), byOneAccount, byOneDetail, true);

            return await _repository.GetBalanceByAccountAsync(parameters);
        }

        #endregion

        #region GetBalance_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort

        private async Task<BalanceByAccountViewModel> GetBalance_CheckedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort(
            bool byOneAccount, bool? byOneDetail, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .UseCheckedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, GetFormOptions(), byOneAccount, byOneDetail, true)
                : GetByNoTestParameters(gridOptions, GetFormOptions(), byOneAccount, byOneDetail, true);

            return await _repository.GetBalanceByAccountAsync(parameters);
        }

        private async Task<BalanceByAccountViewModel> GetBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort(
            bool byOneAccount, bool? byOneDetail, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .UseFinalizedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, GetFormOptions(), byOneAccount, byOneDetail, true)
                : GetByNoTestParameters(gridOptions, GetFormOptions(), byOneAccount, byOneDetail, true);

            return await _repository.GetBalanceByAccountAsync(parameters);
        }

        private async Task<BalanceByAccountViewModel> GetBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort(
            bool byOneAccount, bool? byOneDetail, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .UseConfirmedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, GetFormOptions(), byOneAccount, byOneDetail, true)
                : GetByNoTestParameters(gridOptions, GetFormOptions(), byOneAccount, byOneDetail, true);

            return await _repository.GetBalanceByAccountAsync(parameters);
        }

        private async Task<BalanceByAccountViewModel> GetBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort(
            bool byOneAccount, bool? byOneDetail, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .UseApprovedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, GetFormOptions(), byOneAccount, byOneDetail, true)
                : GetByNoTestParameters(gridOptions, GetFormOptions(), byOneAccount, byOneDetail, true);

            return await _repository.GetBalanceByAccountAsync(parameters);
        }

        private async Task<BalanceByAccountViewModel> GetBalance_AllVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort(
            bool byOneAccount, bool? byOneDetail, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, GetFormOptions(), byOneAccount, byOneDetail, true)
                : GetByNoTestParameters(gridOptions, GetFormOptions(), byOneAccount, byOneDetail, true);

            return await _repository.GetBalanceByAccountAsync(parameters);
        }

        #endregion

        #region GetBalance_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort

        private async Task<BalanceByAccountViewModel> GetBalance_CheckedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort(
            bool byOneAccount, bool? byOneDetail, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseCheckedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, FinanceReportOptions.OpeningAsFirstVoucher, byOneAccount, byOneDetail)
                : GetByNoTestParameters(gridOptions, FinanceReportOptions.OpeningAsFirstVoucher, byOneAccount, byOneDetail);

            return await _repository.GetBalanceByAccountAsync(parameters);
        }

        private async Task<BalanceByAccountViewModel> GetBalance_FinalizedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort(
            bool byOneAccount, bool? byOneDetail, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseFinalizedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, FinanceReportOptions.OpeningAsFirstVoucher, byOneAccount, byOneDetail)
                : GetByNoTestParameters(gridOptions, FinanceReportOptions.OpeningAsFirstVoucher, byOneAccount, byOneDetail);

            return await _repository.GetBalanceByAccountAsync(parameters);
        }

        private async Task<BalanceByAccountViewModel> GetBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort(
            bool byOneAccount, bool? byOneDetail, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseConfirmedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, FinanceReportOptions.OpeningAsFirstVoucher, byOneAccount, byOneDetail)
                : GetByNoTestParameters(gridOptions, FinanceReportOptions.OpeningAsFirstVoucher, byOneAccount, byOneDetail);

            return await _repository.GetBalanceByAccountAsync(parameters);
        }

        private async Task<BalanceByAccountViewModel> GetBalance_ApprovedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort(
            bool byOneAccount, bool? byOneDetail, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseApprovedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, FinanceReportOptions.OpeningAsFirstVoucher, byOneAccount, byOneDetail)
                : GetByNoTestParameters(gridOptions, FinanceReportOptions.OpeningAsFirstVoucher, byOneAccount, byOneDetail);

            return await _repository.GetBalanceByAccountAsync(parameters);
        }

        private async Task<BalanceByAccountViewModel> GetBalance_AllVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort(
            bool byOneAccount, bool? byOneDetail, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, FinanceReportOptions.OpeningAsFirstVoucher, byOneAccount, byOneDetail)
                : GetByNoTestParameters(gridOptions, FinanceReportOptions.OpeningAsFirstVoucher, byOneAccount, byOneDetail);

            return await _repository.GetBalanceByAccountAsync(parameters);
        }

        #endregion

        #region GetBalance_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort

        private async Task<BalanceByAccountViewModel> GetBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort(
            bool byOneAccount, bool? byOneDetail, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .UseCheckedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, FinanceReportOptions.OpeningAsFirstVoucher, byOneAccount, byOneDetail)
                : GetByNoTestParameters(gridOptions, FinanceReportOptions.OpeningAsFirstVoucher, byOneAccount, byOneDetail);

            return await _repository.GetBalanceByAccountAsync(parameters);
        }

        private async Task<BalanceByAccountViewModel> GetBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort(
            bool byOneAccount, bool? byOneDetail, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .UseFinalizedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, FinanceReportOptions.OpeningAsFirstVoucher, byOneAccount, byOneDetail)
                : GetByNoTestParameters(gridOptions, FinanceReportOptions.OpeningAsFirstVoucher, byOneAccount, byOneDetail);

            return await _repository.GetBalanceByAccountAsync(parameters);
        }

        private async Task<BalanceByAccountViewModel> GetBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort(
            bool byOneAccount, bool? byOneDetail, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .UseConfirmedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, FinanceReportOptions.OpeningAsFirstVoucher, byOneAccount, byOneDetail)
                : GetByNoTestParameters(gridOptions, FinanceReportOptions.OpeningAsFirstVoucher, byOneAccount, byOneDetail);

            return await _repository.GetBalanceByAccountAsync(parameters);
        }

        private async Task<BalanceByAccountViewModel> GetBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort(
            bool byOneAccount, bool? byOneDetail, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .UseApprovedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, FinanceReportOptions.OpeningAsFirstVoucher, byOneAccount, byOneDetail)
                : GetByNoTestParameters(gridOptions, FinanceReportOptions.OpeningAsFirstVoucher, byOneAccount, byOneDetail);

            return await _repository.GetBalanceByAccountAsync(parameters);
        }

        private async Task<BalanceByAccountViewModel> GetBalance_AllVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort(
            bool byOneAccount, bool? byOneDetail, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, FinanceReportOptions.OpeningAsFirstVoucher, byOneAccount, byOneDetail)
                : GetByNoTestParameters(gridOptions, FinanceReportOptions.OpeningAsFirstVoucher, byOneAccount, byOneDetail);

            return await _repository.GetBalanceByAccountAsync(parameters);
        }

        #endregion

        #region GetBalance_CurrentBranch_ByBranch_OpeningOption_NoFilterSort

        private async Task<BalanceByAccountViewModel> GetBalance_CheckedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort(
            bool byOneAccount, bool? byOneDetail, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseCheckedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, FinanceReportOptions.OpeningAsFirstVoucher, byOneAccount, byOneDetail, true)
                : GetByNoTestParameters(gridOptions, FinanceReportOptions.OpeningAsFirstVoucher, byOneAccount, byOneDetail, true);

            return await _repository.GetBalanceByAccountAsync(parameters);
        }

        private async Task<BalanceByAccountViewModel> GetBalance_FinalizedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort(
            bool byOneAccount, bool? byOneDetail, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseFinalizedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, FinanceReportOptions.OpeningAsFirstVoucher, byOneAccount, byOneDetail, true)
                : GetByNoTestParameters(gridOptions, FinanceReportOptions.OpeningAsFirstVoucher, byOneAccount, byOneDetail, true);

            return await _repository.GetBalanceByAccountAsync(parameters);
        }

        private async Task<BalanceByAccountViewModel> GetBalance_ConfirmedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort(
            bool byOneAccount, bool? byOneDetail, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseConfirmedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, FinanceReportOptions.OpeningAsFirstVoucher, byOneAccount, byOneDetail, true)
                : GetByNoTestParameters(gridOptions, FinanceReportOptions.OpeningAsFirstVoucher, byOneAccount, byOneDetail, true);

            return await _repository.GetBalanceByAccountAsync(parameters);
        }

        private async Task<BalanceByAccountViewModel> GetBalance_ApprovedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort(
            bool byOneAccount, bool? byOneDetail, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseApprovedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, FinanceReportOptions.OpeningAsFirstVoucher, byOneAccount, byOneDetail, true)
                : GetByNoTestParameters(gridOptions, FinanceReportOptions.OpeningAsFirstVoucher, byOneAccount, byOneDetail, true);

            return await _repository.GetBalanceByAccountAsync(parameters);
        }

        private async Task<BalanceByAccountViewModel> GetBalance_AllVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort(
            bool byOneAccount, bool? byOneDetail, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, FinanceReportOptions.OpeningAsFirstVoucher, byOneAccount, byOneDetail, true)
                : GetByNoTestParameters(gridOptions, FinanceReportOptions.OpeningAsFirstVoucher, byOneAccount, byOneDetail, true);

            return await _repository.GetBalanceByAccountAsync(parameters);
        }

        #endregion

        #region GetBalance_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort

        private async Task<BalanceByAccountViewModel> GetBalance_CheckedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort(
            bool byOneAccount, bool? byOneDetail, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .UseCheckedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, FinanceReportOptions.OpeningAsFirstVoucher, byOneAccount, byOneDetail, true)
                : GetByNoTestParameters(gridOptions, FinanceReportOptions.OpeningAsFirstVoucher, byOneAccount, byOneDetail, true);

            return await _repository.GetBalanceByAccountAsync(parameters);
        }

        private async Task<BalanceByAccountViewModel> GetBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort(
            bool byOneAccount, bool? byOneDetail, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .UseFinalizedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, FinanceReportOptions.OpeningAsFirstVoucher, byOneAccount, byOneDetail, true)
                : GetByNoTestParameters(gridOptions, FinanceReportOptions.OpeningAsFirstVoucher, byOneAccount, byOneDetail, true);

            return await _repository.GetBalanceByAccountAsync(parameters);
        }

        private async Task<BalanceByAccountViewModel> GetBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort(
            bool byOneAccount, bool? byOneDetail, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .UseConfirmedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, FinanceReportOptions.OpeningAsFirstVoucher, byOneAccount, byOneDetail, true)
                : GetByNoTestParameters(gridOptions, FinanceReportOptions.OpeningAsFirstVoucher, byOneAccount, byOneDetail, true);

            return await _repository.GetBalanceByAccountAsync(parameters);
        }

        private async Task<BalanceByAccountViewModel> GetBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort(
            bool byOneAccount, bool? byOneDetail, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .UseApprovedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, FinanceReportOptions.OpeningAsFirstVoucher, byOneAccount, byOneDetail, true)
                : GetByNoTestParameters(gridOptions, FinanceReportOptions.OpeningAsFirstVoucher, byOneAccount, byOneDetail, true);

            return await _repository.GetBalanceByAccountAsync(parameters);
        }

        private async Task<BalanceByAccountViewModel> GetBalance_AllVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort(
            bool byOneAccount, bool? byOneDetail, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, FinanceReportOptions.OpeningAsFirstVoucher, byOneAccount, byOneDetail, true)
                : GetByNoTestParameters(gridOptions, FinanceReportOptions.OpeningAsFirstVoucher, byOneAccount, byOneDetail, true);

            return await _repository.GetBalanceByAccountAsync(parameters);
        }

        #endregion

        #region GetBalance_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort

        private async Task<BalanceByAccountViewModel> GetBalance_CheckedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort(
            bool byOneAccount, bool? byOneDetail, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseCheckedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, GetFormAndOpeningOptions(), byOneAccount, byOneDetail)
                : GetByNoTestParameters(gridOptions, GetFormAndOpeningOptions(), byOneAccount, byOneDetail);

            return await _repository.GetBalanceByAccountAsync(parameters);
        }

        private async Task<BalanceByAccountViewModel> GetBalance_FinalizedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort(
            bool byOneAccount, bool? byOneDetail, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseFinalizedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, GetFormAndOpeningOptions(), byOneAccount, byOneDetail)
                : GetByNoTestParameters(gridOptions, GetFormAndOpeningOptions(), byOneAccount, byOneDetail);

            return await _repository.GetBalanceByAccountAsync(parameters);
        }

        private async Task<BalanceByAccountViewModel> GetBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort(
            bool byOneAccount, bool? byOneDetail, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseConfirmedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, GetFormAndOpeningOptions(), byOneAccount, byOneDetail)
                : GetByNoTestParameters(gridOptions, GetFormAndOpeningOptions(), byOneAccount, byOneDetail);

            return await _repository.GetBalanceByAccountAsync(parameters);
        }

        private async Task<BalanceByAccountViewModel> GetBalance_ApprovedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort(
            bool byOneAccount, bool? byOneDetail, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseApprovedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, GetFormAndOpeningOptions(), byOneAccount, byOneDetail)
                : GetByNoTestParameters(gridOptions, GetFormAndOpeningOptions(), byOneAccount, byOneDetail);

            return await _repository.GetBalanceByAccountAsync(parameters);
        }

        private async Task<BalanceByAccountViewModel> GetBalance_AllVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort(
            bool byOneAccount, bool? byOneDetail, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, GetFormAndOpeningOptions(), byOneAccount, byOneDetail)
                : GetByNoTestParameters(gridOptions, GetFormAndOpeningOptions(), byOneAccount, byOneDetail);

            return await _repository.GetBalanceByAccountAsync(parameters);
        }

        #endregion

        #region GetBalance_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort

        private async Task<BalanceByAccountViewModel> GetBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort(
            bool byOneAccount, bool? byOneDetail, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .UseCheckedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, GetFormAndOpeningOptions(), byOneAccount, byOneDetail)
                : GetByNoTestParameters(gridOptions, GetFormAndOpeningOptions(), byOneAccount, byOneDetail);

            return await _repository.GetBalanceByAccountAsync(parameters);
        }

        private async Task<BalanceByAccountViewModel> GetBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort(
            bool byOneAccount, bool? byOneDetail, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .UseFinalizedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, GetFormAndOpeningOptions(), byOneAccount, byOneDetail)
                : GetByNoTestParameters(gridOptions, GetFormAndOpeningOptions(), byOneAccount, byOneDetail);

            return await _repository.GetBalanceByAccountAsync(parameters);
        }

        private async Task<BalanceByAccountViewModel> GetBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort(
            bool byOneAccount, bool? byOneDetail, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .UseConfirmedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, GetFormAndOpeningOptions(), byOneAccount, byOneDetail)
                : GetByNoTestParameters(gridOptions, GetFormAndOpeningOptions(), byOneAccount, byOneDetail);

            return await _repository.GetBalanceByAccountAsync(parameters);
        }

        private async Task<BalanceByAccountViewModel> GetBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort(
            bool byOneAccount, bool? byOneDetail, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .UseApprovedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, GetFormAndOpeningOptions(), byOneAccount, byOneDetail)
                : GetByNoTestParameters(gridOptions, GetFormAndOpeningOptions(), byOneAccount, byOneDetail);

            return await _repository.GetBalanceByAccountAsync(parameters);
        }

        private async Task<BalanceByAccountViewModel> GetBalance_AllVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort(
            bool byOneAccount, bool? byOneDetail, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, GetFormAndOpeningOptions(), byOneAccount, byOneDetail)
                : GetByNoTestParameters(gridOptions, GetFormAndOpeningOptions(), byOneAccount, byOneDetail);

            return await _repository.GetBalanceByAccountAsync(parameters);
        }

        #endregion

        #region GetBalance_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort

        private async Task<BalanceByAccountViewModel> GetBalance_CheckedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort(
            bool byOneAccount, bool? byOneDetail, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseCheckedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, GetFormAndOpeningOptions(), byOneAccount, byOneDetail, true)
                : GetByNoTestParameters(gridOptions, GetFormAndOpeningOptions(), byOneAccount, byOneDetail, true);

            return await _repository.GetBalanceByAccountAsync(parameters);
        }

        private async Task<BalanceByAccountViewModel> GetBalance_FinalizedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort(
            bool byOneAccount, bool? byOneDetail, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseFinalizedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, GetFormAndOpeningOptions(), byOneAccount, byOneDetail, true)
                : GetByNoTestParameters(gridOptions, GetFormAndOpeningOptions(), byOneAccount, byOneDetail, true);

            return await _repository.GetBalanceByAccountAsync(parameters);
        }

        private async Task<BalanceByAccountViewModel> GetBalance_ConfirmedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort(
            bool byOneAccount, bool? byOneDetail, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseConfirmedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, GetFormAndOpeningOptions(), byOneAccount, byOneDetail, true)
                : GetByNoTestParameters(gridOptions, GetFormAndOpeningOptions(), byOneAccount, byOneDetail, true);

            return await _repository.GetBalanceByAccountAsync(parameters);
        }

        private async Task<BalanceByAccountViewModel> GetBalance_ApprovedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort(
            bool byOneAccount, bool? byOneDetail, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseApprovedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, GetFormAndOpeningOptions(), byOneAccount, byOneDetail, true)
                : GetByNoTestParameters(gridOptions, GetFormAndOpeningOptions(), byOneAccount, byOneDetail, true);

            return await _repository.GetBalanceByAccountAsync(parameters);
        }

        private async Task<BalanceByAccountViewModel> GetBalance_AllVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort(
            bool byOneAccount, bool? byOneDetail, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, GetFormAndOpeningOptions(), byOneAccount, byOneDetail, true)
                : GetByNoTestParameters(gridOptions, GetFormAndOpeningOptions(), byOneAccount, byOneDetail, true);

            return await _repository.GetBalanceByAccountAsync(parameters);
        }

        #endregion

        #region GetBalance_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort

        private async Task<BalanceByAccountViewModel> GetBalance_CheckedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort(
            bool byOneAccount, bool? byOneDetail, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .UseCheckedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, GetFormAndOpeningOptions(), byOneAccount, byOneDetail, true)
                : GetByNoTestParameters(gridOptions, GetFormAndOpeningOptions(), byOneAccount, byOneDetail, true);

            return await _repository.GetBalanceByAccountAsync(parameters);
        }

        private async Task<BalanceByAccountViewModel> GetBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort(
            bool byOneAccount, bool? byOneDetail, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .UseFinalizedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, GetFormAndOpeningOptions(), byOneAccount, byOneDetail, true)
                : GetByNoTestParameters(gridOptions, GetFormAndOpeningOptions(), byOneAccount, byOneDetail, true);

            return await _repository.GetBalanceByAccountAsync(parameters);
        }

        private async Task<BalanceByAccountViewModel> GetBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort(
            bool byOneAccount, bool? byOneDetail, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .UseConfirmedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, GetFormAndOpeningOptions(), byOneAccount, byOneDetail, true)
                : GetByNoTestParameters(gridOptions, GetFormAndOpeningOptions(), byOneAccount, byOneDetail, true);

            return await _repository.GetBalanceByAccountAsync(parameters);
        }

        private async Task<BalanceByAccountViewModel> GetBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort(
            bool byOneAccount, bool? byOneDetail, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .UseApprovedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, GetFormAndOpeningOptions(), byOneAccount, byOneDetail, true)
                : GetByNoTestParameters(gridOptions, GetFormAndOpeningOptions(), byOneAccount, byOneDetail, true);

            return await _repository.GetBalanceByAccountAsync(parameters);
        }

        private async Task<BalanceByAccountViewModel> GetBalance_AllVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort(
            bool byOneAccount, bool? byOneDetail, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, GetFormAndOpeningOptions(), byOneAccount, byOneDetail, true)
                : GetByNoTestParameters(gridOptions, GetFormAndOpeningOptions(), byOneAccount, byOneDetail, true);

            return await _repository.GetBalanceByAccountAsync(parameters);
        }

        #endregion

        #region GetBalance_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort

        private async Task<BalanceByAccountViewModel> GetBalance_CheckedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort(
            bool byOneAccount, bool? byOneDetail, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseCheckedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, FinanceReportOptions.StartTurnoverAsInitBalance, byOneAccount, byOneDetail)
                : GetByNoTestParameters(gridOptions, FinanceReportOptions.StartTurnoverAsInitBalance, byOneAccount, byOneDetail);

            return await _repository.GetBalanceByAccountAsync(parameters);
        }

        private async Task<BalanceByAccountViewModel> GetBalance_FinalizedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort(
            bool byOneAccount, bool? byOneDetail, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseFinalizedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, FinanceReportOptions.StartTurnoverAsInitBalance, byOneAccount, byOneDetail)
                : GetByNoTestParameters(gridOptions, FinanceReportOptions.StartTurnoverAsInitBalance, byOneAccount, byOneDetail);

            return await _repository.GetBalanceByAccountAsync(parameters);
        }

        private async Task<BalanceByAccountViewModel> GetBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort(
            bool byOneAccount, bool? byOneDetail, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseConfirmedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, FinanceReportOptions.StartTurnoverAsInitBalance, byOneAccount, byOneDetail)
                : GetByNoTestParameters(gridOptions, FinanceReportOptions.StartTurnoverAsInitBalance, byOneAccount, byOneDetail);

            return await _repository.GetBalanceByAccountAsync(parameters);
        }

        private async Task<BalanceByAccountViewModel> GetBalance_ApprovedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort(
            bool byOneAccount, bool? byOneDetail, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseApprovedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, FinanceReportOptions.StartTurnoverAsInitBalance, byOneAccount, byOneDetail)
                : GetByNoTestParameters(gridOptions, FinanceReportOptions.StartTurnoverAsInitBalance, byOneAccount, byOneDetail);

            return await _repository.GetBalanceByAccountAsync(parameters);
        }

        private async Task<BalanceByAccountViewModel> GetBalance_AllVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort(
            bool byOneAccount, bool? byOneDetail, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, FinanceReportOptions.StartTurnoverAsInitBalance, byOneAccount, byOneDetail)
                : GetByNoTestParameters(gridOptions, FinanceReportOptions.StartTurnoverAsInitBalance, byOneAccount, byOneDetail);

            return await _repository.GetBalanceByAccountAsync(parameters);
        }

        #endregion

        #region GetBalance_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort

        private async Task<BalanceByAccountViewModel> GetBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort(
            bool byOneAccount, bool? byOneDetail, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .UseCheckedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, FinanceReportOptions.StartTurnoverAsInitBalance, byOneAccount, byOneDetail)
                : GetByNoTestParameters(gridOptions, FinanceReportOptions.StartTurnoverAsInitBalance, byOneAccount, byOneDetail);

            return await _repository.GetBalanceByAccountAsync(parameters);
        }

        private async Task<BalanceByAccountViewModel> GetBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort(
            bool byOneAccount, bool? byOneDetail, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .UseFinalizedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, FinanceReportOptions.StartTurnoverAsInitBalance, byOneAccount, byOneDetail)
                : GetByNoTestParameters(gridOptions, FinanceReportOptions.StartTurnoverAsInitBalance, byOneAccount, byOneDetail);

            return await _repository.GetBalanceByAccountAsync(parameters);
        }

        private async Task<BalanceByAccountViewModel> GetBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort(
            bool byOneAccount, bool? byOneDetail, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .UseConfirmedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, FinanceReportOptions.StartTurnoverAsInitBalance, byOneAccount, byOneDetail)
                : GetByNoTestParameters(gridOptions, FinanceReportOptions.StartTurnoverAsInitBalance, byOneAccount, byOneDetail);

            return await _repository.GetBalanceByAccountAsync(parameters);
        }

        private async Task<BalanceByAccountViewModel> GetBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort(
            bool byOneAccount, bool? byOneDetail, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .UseApprovedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, FinanceReportOptions.StartTurnoverAsInitBalance, byOneAccount, byOneDetail)
                : GetByNoTestParameters(gridOptions, FinanceReportOptions.StartTurnoverAsInitBalance, byOneAccount, byOneDetail);

            return await _repository.GetBalanceByAccountAsync(parameters);
        }

        private async Task<BalanceByAccountViewModel> GetBalance_AllVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort(
            bool byOneAccount, bool? byOneDetail, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, FinanceReportOptions.StartTurnoverAsInitBalance, byOneAccount, byOneDetail)
                : GetByNoTestParameters(gridOptions, FinanceReportOptions.StartTurnoverAsInitBalance, byOneAccount, byOneDetail);

            return await _repository.GetBalanceByAccountAsync(parameters);
        }

        #endregion

        #region GetBalance_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort

        private async Task<BalanceByAccountViewModel> GetBalance_CheckedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort(
            bool byOneAccount, bool? byOneDetail, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseCheckedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, FinanceReportOptions.StartTurnoverAsInitBalance, byOneAccount, byOneDetail, true)
                : GetByNoTestParameters(gridOptions, FinanceReportOptions.StartTurnoverAsInitBalance, byOneAccount, byOneDetail, true);

            return await _repository.GetBalanceByAccountAsync(parameters);
        }

        private async Task<BalanceByAccountViewModel> GetBalance_FinalizedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort(
            bool byOneAccount, bool? byOneDetail, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseFinalizedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, FinanceReportOptions.StartTurnoverAsInitBalance, byOneAccount, byOneDetail, true)
                : GetByNoTestParameters(gridOptions, FinanceReportOptions.StartTurnoverAsInitBalance, byOneAccount, byOneDetail, true);

            return await _repository.GetBalanceByAccountAsync(parameters);
        }

        private async Task<BalanceByAccountViewModel> GetBalance_ConfirmedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort(
            bool byOneAccount, bool? byOneDetail, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseConfirmedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, FinanceReportOptions.StartTurnoverAsInitBalance, byOneAccount, byOneDetail, true)
                : GetByNoTestParameters(gridOptions, FinanceReportOptions.StartTurnoverAsInitBalance, byOneAccount, byOneDetail, true);

            return await _repository.GetBalanceByAccountAsync(parameters);
        }

        private async Task<BalanceByAccountViewModel> GetBalance_ApprovedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort(
            bool byOneAccount, bool? byOneDetail, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseApprovedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, FinanceReportOptions.StartTurnoverAsInitBalance, byOneAccount, byOneDetail, true)
                : GetByNoTestParameters(gridOptions, FinanceReportOptions.StartTurnoverAsInitBalance, byOneAccount, byOneDetail, true);

            return await _repository.GetBalanceByAccountAsync(parameters);
        }

        private async Task<BalanceByAccountViewModel> GetBalance_AllVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort(
            bool byOneAccount, bool? byOneDetail, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, FinanceReportOptions.StartTurnoverAsInitBalance, byOneAccount, byOneDetail, true)
                : GetByNoTestParameters(gridOptions, FinanceReportOptions.StartTurnoverAsInitBalance, byOneAccount, byOneDetail, true);

            return await _repository.GetBalanceByAccountAsync(parameters);
        }

        #endregion

        #region GetBalance_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort

        private async Task<BalanceByAccountViewModel> GetBalance_CheckedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort(
            bool byOneAccount, bool? byOneDetail, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .UseCheckedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, FinanceReportOptions.StartTurnoverAsInitBalance, byOneAccount, byOneDetail, true)
                : GetByNoTestParameters(gridOptions, FinanceReportOptions.StartTurnoverAsInitBalance, byOneAccount, byOneDetail, true);

            return await _repository.GetBalanceByAccountAsync(parameters);
        }

        private async Task<BalanceByAccountViewModel> GetBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort(
            bool byOneAccount, bool? byOneDetail, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .UseFinalizedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, FinanceReportOptions.StartTurnoverAsInitBalance, byOneAccount, byOneDetail, true)
                : GetByNoTestParameters(gridOptions, FinanceReportOptions.StartTurnoverAsInitBalance, byOneAccount, byOneDetail, true);

            return await _repository.GetBalanceByAccountAsync(parameters);
        }

        private async Task<BalanceByAccountViewModel> GetBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort(
            bool byOneAccount, bool? byOneDetail, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .UseConfirmedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, FinanceReportOptions.StartTurnoverAsInitBalance, byOneAccount, byOneDetail, true)
                : GetByNoTestParameters(gridOptions, FinanceReportOptions.StartTurnoverAsInitBalance, byOneAccount, byOneDetail, true);

            return await _repository.GetBalanceByAccountAsync(parameters);
        }

        private async Task<BalanceByAccountViewModel> GetBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort(
            bool byOneAccount, bool? byOneDetail, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .UseApprovedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, FinanceReportOptions.StartTurnoverAsInitBalance, byOneAccount, byOneDetail, true)
                : GetByNoTestParameters(gridOptions, FinanceReportOptions.StartTurnoverAsInitBalance, byOneAccount, byOneDetail, true);

            return await _repository.GetBalanceByAccountAsync(parameters);
        }

        private async Task<BalanceByAccountViewModel> GetBalance_AllVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort(
            bool byOneAccount, bool? byOneDetail, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, FinanceReportOptions.StartTurnoverAsInitBalance, byOneAccount, byOneDetail, true)
                : GetByNoTestParameters(gridOptions, FinanceReportOptions.StartTurnoverAsInitBalance, byOneAccount, byOneDetail, true);

            return await _repository.GetBalanceByAccountAsync(parameters);
        }

        #endregion

        #region GetBalance_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort

        private async Task<BalanceByAccountViewModel> GetBalance_CheckedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort(
            bool byOneAccount, bool? byOneDetail, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseCheckedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, GetFormAndTurnoverOptions(), byOneAccount, byOneDetail)
                : GetByNoTestParameters(gridOptions, GetFormAndTurnoverOptions(), byOneAccount, byOneDetail);

            return await _repository.GetBalanceByAccountAsync(parameters);
        }

        private async Task<BalanceByAccountViewModel> GetBalance_FinalizedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort(
            bool byOneAccount, bool? byOneDetail, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseFinalizedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, GetFormAndTurnoverOptions(), byOneAccount, byOneDetail)
                : GetByNoTestParameters(gridOptions, GetFormAndTurnoverOptions(), byOneAccount, byOneDetail);

            return await _repository.GetBalanceByAccountAsync(parameters);
        }

        private async Task<BalanceByAccountViewModel> GetBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort(
            bool byOneAccount, bool? byOneDetail, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseConfirmedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, GetFormAndTurnoverOptions(), byOneAccount, byOneDetail)
                : GetByNoTestParameters(gridOptions, GetFormAndTurnoverOptions(), byOneAccount, byOneDetail);

            return await _repository.GetBalanceByAccountAsync(parameters);
        }

        private async Task<BalanceByAccountViewModel> GetBalance_ApprovedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort(
            bool byOneAccount, bool? byOneDetail, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseApprovedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, GetFormAndTurnoverOptions(), byOneAccount, byOneDetail)
                : GetByNoTestParameters(gridOptions, GetFormAndTurnoverOptions(), byOneAccount, byOneDetail);

            return await _repository.GetBalanceByAccountAsync(parameters);
        }

        private async Task<BalanceByAccountViewModel> GetBalance_AllVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort(
            bool byOneAccount, bool? byOneDetail, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, GetFormAndTurnoverOptions(), byOneAccount, byOneDetail)
                : GetByNoTestParameters(gridOptions, GetFormAndTurnoverOptions(), byOneAccount, byOneDetail);

            return await _repository.GetBalanceByAccountAsync(parameters);
        }

        #endregion

        #region GetBalance_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort

        private async Task<BalanceByAccountViewModel> GetBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort(
            bool byOneAccount, bool? byOneDetail, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .UseCheckedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, GetFormAndTurnoverOptions(), byOneAccount, byOneDetail)
                : GetByNoTestParameters(gridOptions, GetFormAndTurnoverOptions(), byOneAccount, byOneDetail);

            return await _repository.GetBalanceByAccountAsync(parameters);
        }

        private async Task<BalanceByAccountViewModel> GetBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort(
            bool byOneAccount, bool? byOneDetail, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .UseFinalizedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, GetFormAndTurnoverOptions(), byOneAccount, byOneDetail)
                : GetByNoTestParameters(gridOptions, GetFormAndTurnoverOptions(), byOneAccount, byOneDetail);

            return await _repository.GetBalanceByAccountAsync(parameters);
        }

        private async Task<BalanceByAccountViewModel> GetBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort(
            bool byOneAccount, bool? byOneDetail, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .UseConfirmedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, GetFormAndTurnoverOptions(), byOneAccount, byOneDetail)
                : GetByNoTestParameters(gridOptions, GetFormAndTurnoverOptions(), byOneAccount, byOneDetail);

            return await _repository.GetBalanceByAccountAsync(parameters);
        }

        private async Task<BalanceByAccountViewModel> GetBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort(
            bool byOneAccount, bool? byOneDetail, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .UseApprovedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, GetFormAndTurnoverOptions(), byOneAccount, byOneDetail)
                : GetByNoTestParameters(gridOptions, GetFormAndTurnoverOptions(), byOneAccount, byOneDetail);

            return await _repository.GetBalanceByAccountAsync(parameters);
        }

        private async Task<BalanceByAccountViewModel> GetBalance_AllVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort(
            bool byOneAccount, bool? byOneDetail, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, GetFormAndTurnoverOptions(), byOneAccount, byOneDetail)
                : GetByNoTestParameters(gridOptions, GetFormAndTurnoverOptions(), byOneAccount, byOneDetail);

            return await _repository.GetBalanceByAccountAsync(parameters);
        }

        #endregion

        #region GetBalance_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort

        private async Task<BalanceByAccountViewModel> GetBalance_CheckedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort(
            bool byOneAccount, bool? byOneDetail, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseCheckedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, GetFormAndTurnoverOptions(), byOneAccount, byOneDetail, true)
                : GetByNoTestParameters(gridOptions, GetFormAndTurnoverOptions(), byOneAccount, byOneDetail, true);

            return await _repository.GetBalanceByAccountAsync(parameters);
        }

        private async Task<BalanceByAccountViewModel> GetBalance_FinalizedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort(
            bool byOneAccount, bool? byOneDetail, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseFinalizedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, GetFormAndTurnoverOptions(), byOneAccount, byOneDetail, true)
                : GetByNoTestParameters(gridOptions, GetFormAndTurnoverOptions(), byOneAccount, byOneDetail, true);

            return await _repository.GetBalanceByAccountAsync(parameters);
        }

        private async Task<BalanceByAccountViewModel> GetBalance_ConfirmedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort(
            bool byOneAccount, bool? byOneDetail, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseConfirmedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, GetFormAndTurnoverOptions(), byOneAccount, byOneDetail, true)
                : GetByNoTestParameters(gridOptions, GetFormAndTurnoverOptions(), byOneAccount, byOneDetail, true);

            return await _repository.GetBalanceByAccountAsync(parameters);
        }

        private async Task<BalanceByAccountViewModel> GetBalance_ApprovedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort(
            bool byOneAccount, bool? byOneDetail, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseApprovedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, GetFormAndTurnoverOptions(), byOneAccount, byOneDetail, true)
                : GetByNoTestParameters(gridOptions, GetFormAndTurnoverOptions(), byOneAccount, byOneDetail, true);

            return await _repository.GetBalanceByAccountAsync(parameters);
        }

        private async Task<BalanceByAccountViewModel> GetBalance_AllVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort(
            bool byOneAccount, bool? byOneDetail, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, GetFormAndTurnoverOptions(), byOneAccount, byOneDetail, true)
                : GetByNoTestParameters(gridOptions, GetFormAndTurnoverOptions(), byOneAccount, byOneDetail, true);

            return await _repository.GetBalanceByAccountAsync(parameters);
        }

        #endregion

        #region GetBalance_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort

        private async Task<BalanceByAccountViewModel> GetBalance_CheckedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort(
            bool byOneAccount, bool? byOneDetail, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .UseCheckedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, GetFormAndTurnoverOptions(), byOneAccount, byOneDetail, true)
                : GetByNoTestParameters(gridOptions, GetFormAndTurnoverOptions(), byOneAccount, byOneDetail, true);

            return await _repository.GetBalanceByAccountAsync(parameters);
        }

        private async Task<BalanceByAccountViewModel> GetBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort(
            bool byOneAccount, bool? byOneDetail, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .UseFinalizedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, GetFormAndTurnoverOptions(), byOneAccount, byOneDetail, true)
                : GetByNoTestParameters(gridOptions, GetFormAndTurnoverOptions(), byOneAccount, byOneDetail, true);

            return await _repository.GetBalanceByAccountAsync(parameters);
        }

        private async Task<BalanceByAccountViewModel> GetBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort(
            bool byOneAccount, bool? byOneDetail, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .UseConfirmedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, GetFormAndTurnoverOptions(), byOneAccount, byOneDetail, true)
                : GetByNoTestParameters(gridOptions, GetFormAndTurnoverOptions(), byOneAccount, byOneDetail, true);

            return await _repository.GetBalanceByAccountAsync(parameters);
        }

        private async Task<BalanceByAccountViewModel> GetBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort(
            bool byOneAccount, bool? byOneDetail, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .UseApprovedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, GetFormAndTurnoverOptions(), byOneAccount, byOneDetail, true)
                : GetByNoTestParameters(gridOptions, GetFormAndTurnoverOptions(), byOneAccount, byOneDetail, true);

            return await _repository.GetBalanceByAccountAsync(parameters);
        }

        private async Task<BalanceByAccountViewModel> GetBalance_AllVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort(
            bool byOneAccount, bool? byOneDetail, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, GetFormAndTurnoverOptions(), byOneAccount, byOneDetail, true)
                : GetByNoTestParameters(gridOptions, GetFormAndTurnoverOptions(), byOneAccount, byOneDetail, true);

            return await _repository.GetBalanceByAccountAsync(parameters);
        }

        #endregion

        #region GetBalance_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort

        private async Task<BalanceByAccountViewModel> GetBalance_CheckedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(
            bool byOneAccount, bool? byOneDetail, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseCheckedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, GetOpeningAndTurnoverOptions(), byOneAccount, byOneDetail)
                : GetByNoTestParameters(gridOptions, GetOpeningAndTurnoverOptions(), byOneAccount, byOneDetail);

            return await _repository.GetBalanceByAccountAsync(parameters);
        }

        private async Task<BalanceByAccountViewModel> GetBalance_FinalizedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(
            bool byOneAccount, bool? byOneDetail, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseFinalizedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, GetOpeningAndTurnoverOptions(), byOneAccount, byOneDetail)
                : GetByNoTestParameters(gridOptions, GetOpeningAndTurnoverOptions(), byOneAccount, byOneDetail);

            return await _repository.GetBalanceByAccountAsync(parameters);
        }

        private async Task<BalanceByAccountViewModel> GetBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(
            bool byOneAccount, bool? byOneDetail, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseConfirmedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, GetOpeningAndTurnoverOptions(), byOneAccount, byOneDetail)
                : GetByNoTestParameters(gridOptions, GetOpeningAndTurnoverOptions(), byOneAccount, byOneDetail);

            return await _repository.GetBalanceByAccountAsync(parameters);
        }

        private async Task<BalanceByAccountViewModel> GetBalance_ApprovedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(
            bool byOneAccount, bool? byOneDetail, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseApprovedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, GetOpeningAndTurnoverOptions(), byOneAccount, byOneDetail)
                : GetByNoTestParameters(gridOptions, GetOpeningAndTurnoverOptions(), byOneAccount, byOneDetail);

            return await _repository.GetBalanceByAccountAsync(parameters);
        }

        private async Task<BalanceByAccountViewModel> GetBalance_AllVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(
            bool byOneAccount, bool? byOneDetail, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, GetOpeningAndTurnoverOptions(), byOneAccount, byOneDetail)
                : GetByNoTestParameters(gridOptions, GetOpeningAndTurnoverOptions(), byOneAccount, byOneDetail);

            return await _repository.GetBalanceByAccountAsync(parameters);
        }

        #endregion

        #region GetBalance_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort

        private async Task<BalanceByAccountViewModel> GetBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(
            bool byOneAccount, bool? byOneDetail, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .UseCheckedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, GetOpeningAndTurnoverOptions(), byOneAccount, byOneDetail)
                : GetByNoTestParameters(gridOptions, GetOpeningAndTurnoverOptions(), byOneAccount, byOneDetail);

            return await _repository.GetBalanceByAccountAsync(parameters);
        }

        private async Task<BalanceByAccountViewModel> GetBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(
            bool byOneAccount, bool? byOneDetail, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .UseFinalizedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, GetOpeningAndTurnoverOptions(), byOneAccount, byOneDetail)
                : GetByNoTestParameters(gridOptions, GetOpeningAndTurnoverOptions(), byOneAccount, byOneDetail);

            return await _repository.GetBalanceByAccountAsync(parameters);
        }

        private async Task<BalanceByAccountViewModel> GetBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(
            bool byOneAccount, bool? byOneDetail, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .UseConfirmedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, GetOpeningAndTurnoverOptions(), byOneAccount, byOneDetail)
                : GetByNoTestParameters(gridOptions, GetOpeningAndTurnoverOptions(), byOneAccount, byOneDetail);

            return await _repository.GetBalanceByAccountAsync(parameters);
        }

        private async Task<BalanceByAccountViewModel> GetBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(
            bool byOneAccount, bool? byOneDetail, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .UseApprovedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, GetOpeningAndTurnoverOptions(), byOneAccount, byOneDetail)
                : GetByNoTestParameters(gridOptions, GetOpeningAndTurnoverOptions(), byOneAccount, byOneDetail);

            return await _repository.GetBalanceByAccountAsync(parameters);
        }

        private async Task<BalanceByAccountViewModel> GetBalance_AllVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(
            bool byOneAccount, bool? byOneDetail, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, GetOpeningAndTurnoverOptions(), byOneAccount, byOneDetail)
                : GetByNoTestParameters(gridOptions, GetOpeningAndTurnoverOptions(), byOneAccount, byOneDetail);

            return await _repository.GetBalanceByAccountAsync(parameters);
        }

        #endregion

        #region GetBalance_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort

        private async Task<BalanceByAccountViewModel> GetBalance_CheckedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort(
            bool byOneAccount, bool? byOneDetail, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseCheckedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, GetOpeningAndTurnoverOptions(), byOneAccount, byOneDetail, true)
                : GetByNoTestParameters(gridOptions, GetOpeningAndTurnoverOptions(), byOneAccount, byOneDetail, true);

            return await _repository.GetBalanceByAccountAsync(parameters);
        }

        private async Task<BalanceByAccountViewModel> GetBalance_FinalizedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort(
            bool byOneAccount, bool? byOneDetail, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseFinalizedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, GetOpeningAndTurnoverOptions(), byOneAccount, byOneDetail, true)
                : GetByNoTestParameters(gridOptions, GetOpeningAndTurnoverOptions(), byOneAccount, byOneDetail, true);

            return await _repository.GetBalanceByAccountAsync(parameters);
        }

        private async Task<BalanceByAccountViewModel> GetBalance_ConfirmedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort(
            bool byOneAccount, bool? byOneDetail, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseConfirmedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, GetOpeningAndTurnoverOptions(), byOneAccount, byOneDetail, true)
                : GetByNoTestParameters(gridOptions, GetOpeningAndTurnoverOptions(), byOneAccount, byOneDetail, true);

            return await _repository.GetBalanceByAccountAsync(parameters);
        }

        private async Task<BalanceByAccountViewModel> GetBalance_ApprovedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort(
            bool byOneAccount, bool? byOneDetail, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseApprovedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, GetOpeningAndTurnoverOptions(), byOneAccount, byOneDetail, true)
                : GetByNoTestParameters(gridOptions, GetOpeningAndTurnoverOptions(), byOneAccount, byOneDetail, true);

            return await _repository.GetBalanceByAccountAsync(parameters);
        }

        private async Task<BalanceByAccountViewModel> GetBalance_AllVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort(
            bool byOneAccount, bool? byOneDetail, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, GetOpeningAndTurnoverOptions(), byOneAccount, byOneDetail, true)
                : GetByNoTestParameters(gridOptions, GetOpeningAndTurnoverOptions(), byOneAccount, byOneDetail, true);

            return await _repository.GetBalanceByAccountAsync(parameters);
        }

        #endregion

        #region GetBalance_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort

        private async Task<BalanceByAccountViewModel> GetBalance_CheckedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort(
            bool byOneAccount, bool? byOneDetail, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .UseCheckedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, GetOpeningAndTurnoverOptions(), byOneAccount, byOneDetail, true)
                : GetByNoTestParameters(gridOptions, GetOpeningAndTurnoverOptions(), byOneAccount, byOneDetail, true);

            return await _repository.GetBalanceByAccountAsync(parameters);
        }

        private async Task<BalanceByAccountViewModel> GetBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort(
            bool byOneAccount, bool? byOneDetail, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .UseFinalizedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, GetOpeningAndTurnoverOptions(), byOneAccount, byOneDetail, true)
                : GetByNoTestParameters(gridOptions, GetOpeningAndTurnoverOptions(), byOneAccount, byOneDetail, true);

            return await _repository.GetBalanceByAccountAsync(parameters);
        }

        private async Task<BalanceByAccountViewModel> GetBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort(
            bool byOneAccount, bool? byOneDetail, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .UseConfirmedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, GetOpeningAndTurnoverOptions(), byOneAccount, byOneDetail, true)
                : GetByNoTestParameters(gridOptions, GetOpeningAndTurnoverOptions(), byOneAccount, byOneDetail, true);

            return await _repository.GetBalanceByAccountAsync(parameters);
        }

        private async Task<BalanceByAccountViewModel> GetBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort(
            bool byOneAccount, bool? byOneDetail, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .UseApprovedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, GetOpeningAndTurnoverOptions(), byOneAccount, byOneDetail, true)
                : GetByNoTestParameters(gridOptions, GetOpeningAndTurnoverOptions(), byOneAccount, byOneDetail, true);

            return await _repository.GetBalanceByAccountAsync(parameters);
        }

        private async Task<BalanceByAccountViewModel> GetBalance_AllVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort(
            bool byOneAccount, bool? byOneDetail, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, GetOpeningAndTurnoverOptions(), byOneAccount, byOneDetail, true)
                : GetByNoTestParameters(gridOptions, GetOpeningAndTurnoverOptions(), byOneAccount, byOneDetail, true);

            return await _repository.GetBalanceByAccountAsync(parameters);
        }

        #endregion

        #region GetBalance_CurrentBranch_NoGrouping_AllOptions_NoFilterSort

        private async Task<BalanceByAccountViewModel> GetBalance_CheckedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort(
            bool byOneAccount, bool? byOneDetail, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseCheckedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, FinanceReportOptions.All, byOneAccount, byOneDetail)
                : GetByNoTestParameters(gridOptions, FinanceReportOptions.All, byOneAccount, byOneDetail);

            return await _repository.GetBalanceByAccountAsync(parameters);
        }

        private async Task<BalanceByAccountViewModel> GetBalance_FinalizedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort(
            bool byOneAccount, bool? byOneDetail, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseFinalizedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, FinanceReportOptions.All, byOneAccount, byOneDetail)
                : GetByNoTestParameters(gridOptions, FinanceReportOptions.All, byOneAccount, byOneDetail);

            return await _repository.GetBalanceByAccountAsync(parameters);
        }

        private async Task<BalanceByAccountViewModel> GetBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort(
            bool byOneAccount, bool? byOneDetail, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseConfirmedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, FinanceReportOptions.All, byOneAccount, byOneDetail)
                : GetByNoTestParameters(gridOptions, FinanceReportOptions.All, byOneAccount, byOneDetail);

            return await _repository.GetBalanceByAccountAsync(parameters);
        }

        private async Task<BalanceByAccountViewModel> GetBalance_ApprovedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort(
            bool byOneAccount, bool? byOneDetail, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseApprovedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, FinanceReportOptions.All, byOneAccount, byOneDetail)
                : GetByNoTestParameters(gridOptions, FinanceReportOptions.All, byOneAccount, byOneDetail);

            return await _repository.GetBalanceByAccountAsync(parameters);
        }

        private async Task<BalanceByAccountViewModel> GetBalance_AllVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort(
            bool byOneAccount, bool? byOneDetail, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, FinanceReportOptions.All, byOneAccount, byOneDetail)
                : GetByNoTestParameters(gridOptions, FinanceReportOptions.All, byOneAccount, byOneDetail);

            return await _repository.GetBalanceByAccountAsync(parameters);
        }

        #endregion

        #region GetBalance_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort

        private async Task<BalanceByAccountViewModel> GetBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort(
            bool byOneAccount, bool? byOneDetail, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .UseCheckedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, FinanceReportOptions.All, byOneAccount, byOneDetail)
                : GetByNoTestParameters(gridOptions, FinanceReportOptions.All, byOneAccount, byOneDetail);

            return await _repository.GetBalanceByAccountAsync(parameters);
        }

        private async Task<BalanceByAccountViewModel> GetBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort(
            bool byOneAccount, bool? byOneDetail, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .UseFinalizedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, FinanceReportOptions.All, byOneAccount, byOneDetail)
                : GetByNoTestParameters(gridOptions, FinanceReportOptions.All, byOneAccount, byOneDetail);

            return await _repository.GetBalanceByAccountAsync(parameters);
        }

        private async Task<BalanceByAccountViewModel> GetBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort(
            bool byOneAccount, bool? byOneDetail, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .UseConfirmedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, FinanceReportOptions.All, byOneAccount, byOneDetail)
                : GetByNoTestParameters(gridOptions, FinanceReportOptions.All, byOneAccount, byOneDetail);

            return await _repository.GetBalanceByAccountAsync(parameters);
        }

        private async Task<BalanceByAccountViewModel> GetBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort(
            bool byOneAccount, bool? byOneDetail, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .UseApprovedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, FinanceReportOptions.All, byOneAccount, byOneDetail)
                : GetByNoTestParameters(gridOptions, FinanceReportOptions.All, byOneAccount, byOneDetail);

            return await _repository.GetBalanceByAccountAsync(parameters);
        }

        private async Task<BalanceByAccountViewModel> GetBalance_AllVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort(
            bool byOneAccount, bool? byOneDetail, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, FinanceReportOptions.All, byOneAccount, byOneDetail)
                : GetByNoTestParameters(gridOptions, FinanceReportOptions.All, byOneAccount, byOneDetail);

            return await _repository.GetBalanceByAccountAsync(parameters);
        }

        #endregion

        #region GetBalance_CurrentBranch_ByBranch_AllOptions_NoFilterSort

        private async Task<BalanceByAccountViewModel> GetBalance_CheckedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort(
            bool byOneAccount, bool? byOneDetail, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseCheckedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, FinanceReportOptions.All, byOneAccount, byOneDetail, true)
                : GetByNoTestParameters(gridOptions, FinanceReportOptions.All, byOneAccount, byOneDetail, true);

            return await _repository.GetBalanceByAccountAsync(parameters);
        }

        private async Task<BalanceByAccountViewModel> GetBalance_FinalizedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort(
            bool byOneAccount, bool? byOneDetail, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseFinalizedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, FinanceReportOptions.All, byOneAccount, byOneDetail, true)
                : GetByNoTestParameters(gridOptions, FinanceReportOptions.All, byOneAccount, byOneDetail, true);

            return await _repository.GetBalanceByAccountAsync(parameters);
        }

        private async Task<BalanceByAccountViewModel> GetBalance_ConfirmedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort(
            bool byOneAccount, bool? byOneDetail, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseConfirmedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, FinanceReportOptions.All, byOneAccount, byOneDetail, true)
                : GetByNoTestParameters(gridOptions, FinanceReportOptions.All, byOneAccount, byOneDetail, true);

            return await _repository.GetBalanceByAccountAsync(parameters);
        }

        private async Task<BalanceByAccountViewModel> GetBalance_ApprovedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort(
            bool byOneAccount, bool? byOneDetail, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseApprovedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, FinanceReportOptions.All, byOneAccount, byOneDetail, true)
                : GetByNoTestParameters(gridOptions, FinanceReportOptions.All, byOneAccount, byOneDetail, true);

            return await _repository.GetBalanceByAccountAsync(parameters);
        }

        private async Task<BalanceByAccountViewModel> GetBalance_AllVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort(
            bool byOneAccount, bool? byOneDetail, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, FinanceReportOptions.All, byOneAccount, byOneDetail, true)
                : GetByNoTestParameters(gridOptions, FinanceReportOptions.All, byOneAccount, byOneDetail, true);

            return await _repository.GetBalanceByAccountAsync(parameters);
        }

        #endregion

        #region GetBalance_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort

        private async Task<BalanceByAccountViewModel> GetBalance_CheckedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort(
            bool byOneAccount, bool? byOneDetail, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .UseCheckedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, FinanceReportOptions.All, byOneAccount, byOneDetail, true)
                : GetByNoTestParameters(gridOptions, FinanceReportOptions.All, byOneAccount, byOneDetail, true);

            return await _repository.GetBalanceByAccountAsync(parameters);
        }

        private async Task<BalanceByAccountViewModel> GetBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort(
            bool byOneAccount, bool? byOneDetail, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .UseFinalizedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, FinanceReportOptions.All, byOneAccount, byOneDetail, true)
                : GetByNoTestParameters(gridOptions, FinanceReportOptions.All, byOneAccount, byOneDetail, true);

            return await _repository.GetBalanceByAccountAsync(parameters);
        }

        private async Task<BalanceByAccountViewModel> GetBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort(
            bool byOneAccount, bool? byOneDetail, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .UseConfirmedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, FinanceReportOptions.All, byOneAccount, byOneDetail, true)
                : GetByNoTestParameters(gridOptions, FinanceReportOptions.All, byOneAccount, byOneDetail, true);

            return await _repository.GetBalanceByAccountAsync(parameters);
        }

        private async Task<BalanceByAccountViewModel> GetBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort(
            bool byOneAccount, bool? byOneDetail, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .UseApprovedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, FinanceReportOptions.All, byOneAccount, byOneDetail, true)
                : GetByNoTestParameters(gridOptions, FinanceReportOptions.All, byOneAccount, byOneDetail, true);

            return await _repository.GetBalanceByAccountAsync(parameters);
        }

        private async Task<BalanceByAccountViewModel> GetBalance_AllVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort(
            bool byOneAccount, bool? byOneDetail, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, FinanceReportOptions.All, byOneAccount, byOneDetail, true)
                : GetByNoTestParameters(gridOptions, FinanceReportOptions.All, byOneAccount, byOneDetail, true);

            return await _repository.GetBalanceByAccountAsync(parameters);
        }

        #endregion

        private BalanceByAccountParameters GetByDateTestParameters(
            GridOptions gridOptions, FinanceReportOptions options,
            bool byOneAccount = false, bool? byOneDetail = null, bool byBranch = false)
        {
            return new BalanceByAccountParameters()
            {
                FromDate = DateTime.Parse("2019-04-01"),
                ToDate = DateTime.Parse("2020-03-20"),
                IsByBranch = byBranch,
                ViewId = ViewId.Account,
                IsSelectedAccount = true,
                AccountLevel = 1,
                AccountId = byOneAccount ? TestAccountId : (int?)null,
                IsSelectedDetailAccount = byOneDetail.HasValue,
                DetailAccountLevel = byOneDetail.HasValue ? 2 : (int?)null,
                DetailAccountId = (byOneDetail.HasValue && byOneDetail.Value) ? TestDetailId : (int?)null,
                GridOptions = gridOptions,
                Options = (int)options
            };
        }

        private BalanceByAccountParameters GetByNoTestParameters(
            GridOptions gridOptions, FinanceReportOptions options,
            bool byOneAccount = false, bool? byOneDetail = null, bool byBranch = false)
        {
            var parameters = GetByDateTestParameters(gridOptions, options, byOneAccount, byOneDetail, byBranch);
            parameters.FromDate = parameters.ToDate = null;
            parameters.FromNo = 10;
            parameters.ToNo = 100;
            return parameters;
        }

        private const int TestAccountId = 174;
        private const int TestDetailId = 15;
        private IBalanceByAccountRepository _repository;
    }
}
