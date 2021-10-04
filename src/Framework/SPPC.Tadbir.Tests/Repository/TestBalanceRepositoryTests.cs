using System;
using System.Threading.Tasks;
using NUnit.Framework;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Persistence;
using SPPC.Tadbir.Persistence.Utility;
using SPPC.Tadbir.ViewModel.Reporting;

namespace SPPC.Tadbir.Tests.Repository
{
    [TestFixture]
    [Category("RepositorySmokeTest")]
    public class TestBalanceRepositoryTests : RepositoryTestBase
    {
        [OneTimeSetUp]
        public void FixtureSetup()
        {
            SetUp();
        }

        #region Level Balance By Date Tests

        #region CheckedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort

        [Test]
        public async Task Balance_2Column_CheckedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_4Column_CheckedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_6Column_CheckedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_8Column_CheckedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region FinalizedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort

        [Test]
        public async Task Balance_2Column_FinalizedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_4Column_FinalizedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_6Column_FinalizedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_8Column_FinalizedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region ConfirmedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort

        [Test]
        public async Task Balance_2Column_ConfirmedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_4Column_ConfirmedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_6Column_ConfirmedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_8Column_ConfirmedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region ApprovedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort

        [Test]
        public async Task Balance_2Column_ApprovedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_4Column_ApprovedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_6Column_ApprovedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_8Column_ApprovedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region AllVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort

        [Test]
        public async Task Balance_2Column_AllVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_4Column_AllVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_6Column_AllVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_8Column_AllVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region CheckedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort

        [Test]
        public async Task Balance_2Column_CheckedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_4Column_CheckedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_6Column_CheckedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_8Column_CheckedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region FinalizedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort

        [Test]
        public async Task Balance_2Column_FinalizedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_4Column_FinalizedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_6Column_FinalizedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_8Column_FinalizedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region ConfirmedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort

        [Test]
        public async Task Balance_2Column_ConfirmedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_4Column_ConfirmedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_6Column_ConfirmedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_8Column_ConfirmedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region ApprovedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort

        [Test]
        public async Task Balance_2Column_ApprovedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_4Column_ApprovedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_6Column_ApprovedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_8Column_ApprovedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region AllVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort

        [Test]
        public async Task Balance_2Column_AllVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_4Column_AllVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_6Column_AllVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_8Column_AllVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region CheckedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort

        [Test]
        public async Task Balance_2Column_CheckedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_4Column_CheckedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_6Column_CheckedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_8Column_CheckedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region FinalizedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort

        [Test]
        public async Task Balance_2Column_FinalizedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_4Column_FinalizedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_6Column_FinalizedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_8Column_FinalizedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region ConfirmedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort

        [Test]
        public async Task Balance_2Column_ConfirmedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_4Column_ConfirmedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_6Column_ConfirmedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_8Column_ConfirmedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region ApprovedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort

        [Test]
        public async Task Balance_2Column_ApprovedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_4Column_ApprovedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_6Column_ApprovedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_8Column_ApprovedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region AllVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort

        [Test]
        public async Task Balance_2Column_AllVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_4Column_AllVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_6Column_AllVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_8Column_AllVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region CheckedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort

        [Test]
        public async Task Balance_2Column_CheckedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_4Column_CheckedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_6Column_CheckedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_8Column_CheckedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region FinalizedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort

        [Test]
        public async Task Balance_2Column_FinalizedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_4Column_FinalizedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_6Column_FinalizedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_8Column_FinalizedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region ConfirmedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort

        [Test]
        public async Task Balance_2Column_ConfirmedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_4Column_ConfirmedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_6Column_ConfirmedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_8Column_ConfirmedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region ApprovedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort

        [Test]
        public async Task Balance_2Column_ApprovedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_4Column_ApprovedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_6Column_ApprovedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_8Column_ApprovedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region AllVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort

        [Test]
        public async Task Balance_2Column_AllVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_4Column_AllVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_6Column_AllVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_8Column_AllVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region CheckedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort

        [Test]
        public async Task Balance_2Column_CheckedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_4Column_CheckedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_6Column_CheckedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_8Column_CheckedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region FinalizedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort

        [Test]
        public async Task Balance_2Column_FinalizedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_4Column_FinalizedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_6Column_FinalizedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_8Column_FinalizedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region ConfirmedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort

        [Test]
        public async Task Balance_2Column_ConfirmedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_4Column_ConfirmedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_6Column_ConfirmedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_8Column_ConfirmedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region ApprovedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort

        [Test]
        public async Task Balance_2Column_ApprovedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_4Column_ApprovedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_6Column_ApprovedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_8Column_ApprovedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region AllVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort

        [Test]
        public async Task Balance_2Column_AllVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_4Column_AllVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_6Column_AllVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_8Column_AllVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region CheckedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort

        [Test]
        public async Task Balance_2Column_CheckedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_4Column_CheckedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_6Column_CheckedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_8Column_CheckedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region FinalizedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort

        [Test]
        public async Task Balance_2Column_FinalizedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_4Column_FinalizedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_6Column_FinalizedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_8Column_FinalizedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region ConfirmedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort

        [Test]
        public async Task Balance_2Column_ConfirmedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_4Column_ConfirmedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_6Column_ConfirmedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_8Column_ConfirmedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region ApprovedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort

        [Test]
        public async Task Balance_2Column_ApprovedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_4Column_ApprovedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_6Column_ApprovedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_8Column_ApprovedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region AllVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort

        [Test]
        public async Task Balance_2Column_AllVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_4Column_AllVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_6Column_AllVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_8Column_AllVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region CheckedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort

        [Test]
        public async Task Balance_2Column_CheckedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_4Column_CheckedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_6Column_CheckedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_8Column_CheckedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region FinalizedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort

        [Test]
        public async Task Balance_2Column_FinalizedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_4Column_FinalizedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_6Column_FinalizedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_8Column_FinalizedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region ConfirmedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort

        [Test]
        public async Task Balance_2Column_ConfirmedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_4Column_ConfirmedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_6Column_ConfirmedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_8Column_ConfirmedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region ApprovedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort

        [Test]
        public async Task Balance_2Column_ApprovedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_4Column_ApprovedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_6Column_ApprovedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_8Column_ApprovedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region AllVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort

        [Test]
        public async Task Balance_2Column_AllVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_4Column_AllVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_6Column_AllVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_8Column_AllVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region CheckedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort

        [Test]
        public async Task Balance_2Column_CheckedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_4Column_CheckedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_6Column_CheckedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_8Column_CheckedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region FinalizedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort

        [Test]
        public async Task Balance_2Column_FinalizedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_4Column_FinalizedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_6Column_FinalizedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_8Column_FinalizedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region ConfirmedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort

        [Test]
        public async Task Balance_2Column_ConfirmedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_4Column_ConfirmedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_6Column_ConfirmedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_8Column_ConfirmedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region ApprovedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort

        [Test]
        public async Task Balance_2Column_ApprovedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_4Column_ApprovedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_6Column_ApprovedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_8Column_ApprovedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region AllVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort

        [Test]
        public async Task Balance_2Column_AllVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_4Column_AllVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_6Column_AllVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_8Column_AllVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region CheckedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort

        [Test]
        public async Task Balance_2Column_CheckedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_4Column_CheckedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_6Column_CheckedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_8Column_CheckedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region FinalizedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort

        [Test]
        public async Task Balance_2Column_FinalizedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_4Column_FinalizedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_6Column_FinalizedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_8Column_FinalizedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region ConfirmedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort

        [Test]
        public async Task Balance_2Column_ConfirmedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_4Column_ConfirmedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_6Column_ConfirmedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_8Column_ConfirmedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region ApprovedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort

        [Test]
        public async Task Balance_2Column_ApprovedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_4Column_ApprovedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_6Column_ApprovedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_8Column_ApprovedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region AllVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort

        [Test]
        public async Task Balance_2Column_AllVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_4Column_AllVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_6Column_AllVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_8Column_AllVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region CheckedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort

        [Test]
        public async Task Balance_2Column_CheckedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_4Column_CheckedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_6Column_CheckedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_8Column_CheckedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region FinalizedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort

        [Test]
        public async Task Balance_2Column_FinalizedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_4Column_FinalizedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_6Column_FinalizedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_8Column_FinalizedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region ConfirmedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort

        [Test]
        public async Task Balance_2Column_ConfirmedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_4Column_ConfirmedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_6Column_ConfirmedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_8Column_ConfirmedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region ApprovedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort

        [Test]
        public async Task Balance_2Column_ApprovedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_4Column_ApprovedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_6Column_ApprovedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_8Column_ApprovedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region AllVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort

        [Test]
        public async Task Balance_2Column_AllVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_4Column_AllVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_6Column_AllVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_8Column_AllVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region CheckedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort

        [Test]
        public async Task Balance_2Column_CheckedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_4Column_CheckedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_6Column_CheckedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_8Column_CheckedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region FinalizedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort

        [Test]
        public async Task Balance_2Column_FinalizedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_4Column_FinalizedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_6Column_FinalizedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_8Column_FinalizedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region ConfirmedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort

        [Test]
        public async Task Balance_2Column_ConfirmedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_4Column_ConfirmedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_6Column_ConfirmedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_8Column_ConfirmedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region ApprovedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort

        [Test]
        public async Task Balance_2Column_ApprovedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_4Column_ApprovedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_6Column_ApprovedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_8Column_ApprovedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region AllVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort

        [Test]
        public async Task Balance_2Column_AllVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_4Column_AllVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_6Column_AllVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_8Column_AllVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region CheckedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort

        [Test]
        public async Task Balance_2Column_CheckedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_4Column_CheckedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_6Column_CheckedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_8Column_CheckedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region FinalizedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort

        [Test]
        public async Task Balance_2Column_FinalizedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_4Column_FinalizedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_6Column_FinalizedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_8Column_FinalizedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region ConfirmedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort

        [Test]
        public async Task Balance_2Column_ConfirmedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_4Column_ConfirmedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_6Column_ConfirmedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_8Column_ConfirmedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region ApprovedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort

        [Test]
        public async Task Balance_2Column_ApprovedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_4Column_ApprovedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_6Column_ApprovedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_8Column_ApprovedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region AllVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort

        [Test]
        public async Task Balance_2Column_AllVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_4Column_AllVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_6Column_AllVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_8Column_AllVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region CheckedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort

        [Test]
        public async Task Balance_2Column_CheckedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_4Column_CheckedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_6Column_CheckedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_8Column_CheckedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region FinalizedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort

        [Test]
        public async Task Balance_2Column_FinalizedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_4Column_FinalizedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_6Column_FinalizedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_8Column_FinalizedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region ConfirmedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort

        [Test]
        public async Task Balance_2Column_ConfirmedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_4Column_ConfirmedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_6Column_ConfirmedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_8Column_ConfirmedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region ApprovedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort

        [Test]
        public async Task Balance_2Column_ApprovedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_4Column_ApprovedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_6Column_ApprovedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_8Column_ApprovedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region AllVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort

        [Test]
        public async Task Balance_2Column_AllVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_4Column_AllVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_6Column_AllVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_8Column_AllVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region CheckedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort

        [Test]
        public async Task Balance_2Column_CheckedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_4Column_CheckedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_6Column_CheckedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_8Column_CheckedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region FinalizedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort

        [Test]
        public async Task Balance_2Column_FinalizedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_4Column_FinalizedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_6Column_FinalizedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_8Column_FinalizedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region ConfirmedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort

        [Test]
        public async Task Balance_2Column_ConfirmedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_4Column_ConfirmedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_6Column_ConfirmedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_8Column_ConfirmedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region ApprovedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort

        [Test]
        public async Task Balance_2Column_ApprovedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_4Column_ApprovedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_6Column_ApprovedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_8Column_ApprovedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region AllVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort

        [Test]
        public async Task Balance_2Column_AllVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_4Column_AllVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_6Column_AllVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_8Column_AllVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region CheckedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort

        [Test]
        public async Task Balance_2Column_CheckedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_4Column_CheckedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_6Column_CheckedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_8Column_CheckedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region FinalizedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort

        [Test]
        public async Task Balance_2Column_FinalizedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_4Column_FinalizedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_6Column_FinalizedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_8Column_FinalizedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region ConfirmedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort

        [Test]
        public async Task Balance_2Column_ConfirmedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_4Column_ConfirmedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_6Column_ConfirmedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_8Column_ConfirmedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region ApprovedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort

        [Test]
        public async Task Balance_2Column_ApprovedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_4Column_ApprovedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_6Column_ApprovedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_8Column_ApprovedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region AllVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort

        [Test]
        public async Task Balance_2Column_AllVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_4Column_AllVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_6Column_AllVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_8Column_AllVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region CheckedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort

        [Test]
        public async Task Balance_2Column_CheckedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_4Column_CheckedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_6Column_CheckedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_8Column_CheckedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region FinalizedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort

        [Test]
        public async Task Balance_2Column_FinalizedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_4Column_FinalizedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_6Column_FinalizedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_8Column_FinalizedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region ConfirmedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort

        [Test]
        public async Task Balance_2Column_ConfirmedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_4Column_ConfirmedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_6Column_ConfirmedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_8Column_ConfirmedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region ApprovedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort

        [Test]
        public async Task Balance_2Column_ApprovedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_4Column_ApprovedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_6Column_ApprovedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_8Column_ApprovedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region AllVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort

        [Test]
        public async Task Balance_2Column_AllVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_4Column_AllVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_6Column_AllVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_8Column_AllVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region CheckedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort

        [Test]
        public async Task Balance_2Column_CheckedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_4Column_CheckedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_6Column_CheckedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_8Column_CheckedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region FinalizedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort

        [Test]
        public async Task Balance_2Column_FinalizedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_4Column_FinalizedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_6Column_FinalizedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_8Column_FinalizedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region ConfirmedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort

        [Test]
        public async Task Balance_2Column_ConfirmedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_4Column_ConfirmedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_6Column_ConfirmedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_8Column_ConfirmedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region ApprovedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort

        [Test]
        public async Task Balance_2Column_ApprovedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_4Column_ApprovedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_6Column_ApprovedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_8Column_ApprovedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region AllVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort

        [Test]
        public async Task Balance_2Column_AllVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_4Column_AllVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_6Column_AllVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_8Column_AllVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region CheckedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort

        [Test]
        public async Task Balance_2Column_CheckedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_4Column_CheckedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_6Column_CheckedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_8Column_CheckedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region FinalizedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort

        [Test]
        public async Task Balance_2Column_FinalizedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_4Column_FinalizedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_6Column_FinalizedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_8Column_FinalizedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region ConfirmedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort

        [Test]
        public async Task Balance_2Column_ConfirmedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_4Column_ConfirmedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_6Column_ConfirmedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_8Column_ConfirmedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region ApprovedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort

        [Test]
        public async Task Balance_2Column_ApprovedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_4Column_ApprovedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_6Column_ApprovedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_8Column_ApprovedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region AllVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort

        [Test]
        public async Task Balance_2Column_AllVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_4Column_AllVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_6Column_AllVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_8Column_AllVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region CheckedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort

        [Test]
        public async Task Balance_2Column_CheckedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_4Column_CheckedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_6Column_CheckedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_8Column_CheckedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region FinalizedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort

        [Test]
        public async Task Balance_2Column_FinalizedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_4Column_FinalizedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_6Column_FinalizedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_8Column_FinalizedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region ConfirmedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort

        [Test]
        public async Task Balance_2Column_ConfirmedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_4Column_ConfirmedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_6Column_ConfirmedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_8Column_ConfirmedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region ApprovedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort

        [Test]
        public async Task Balance_2Column_ApprovedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_4Column_ApprovedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_6Column_ApprovedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_8Column_ApprovedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region AllVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort

        [Test]
        public async Task Balance_2Column_AllVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_4Column_AllVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_6Column_AllVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_8Column_AllVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region CheckedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort

        [Test]
        public async Task Balance_2Column_CheckedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_4Column_CheckedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_6Column_CheckedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_8Column_CheckedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region FinalizedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort

        [Test]
        public async Task Balance_2Column_FinalizedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_4Column_FinalizedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_6Column_FinalizedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_8Column_FinalizedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region ConfirmedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort

        [Test]
        public async Task Balance_2Column_ConfirmedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_4Column_ConfirmedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_6Column_ConfirmedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_8Column_ConfirmedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region ApprovedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort

        [Test]
        public async Task Balance_2Column_ApprovedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_4Column_ApprovedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_6Column_ApprovedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_8Column_ApprovedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region AllVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort

        [Test]
        public async Task Balance_2Column_AllVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_4Column_AllVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_6Column_AllVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_8Column_AllVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region CheckedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort

        [Test]
        public async Task Balance_2Column_CheckedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_4Column_CheckedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_6Column_CheckedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_8Column_CheckedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region FinalizedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort

        [Test]
        public async Task Balance_2Column_FinalizedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_4Column_FinalizedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_6Column_FinalizedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_8Column_FinalizedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region ConfirmedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort

        [Test]
        public async Task Balance_2Column_ConfirmedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_4Column_ConfirmedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_6Column_ConfirmedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_8Column_ConfirmedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region ApprovedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort

        [Test]
        public async Task Balance_2Column_ApprovedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_4Column_ApprovedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_6Column_ApprovedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_8Column_ApprovedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region AllVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort

        [Test]
        public async Task Balance_2Column_AllVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_4Column_AllVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_6Column_AllVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_8Column_AllVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region CheckedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort

        [Test]
        public async Task Balance_2Column_CheckedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_4Column_CheckedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_6Column_CheckedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_8Column_CheckedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region FinalizedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort

        [Test]
        public async Task Balance_2Column_FinalizedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_4Column_FinalizedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_6Column_FinalizedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_8Column_FinalizedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region ConfirmedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort

        [Test]
        public async Task Balance_2Column_ConfirmedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_4Column_ConfirmedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_6Column_ConfirmedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_8Column_ConfirmedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region ApprovedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort

        [Test]
        public async Task Balance_2Column_ApprovedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_4Column_ApprovedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_6Column_ApprovedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_8Column_ApprovedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region AllVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort

        [Test]
        public async Task Balance_2Column_AllVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_4Column_AllVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_6Column_AllVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_8Column_AllVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region CheckedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort

        [Test]
        public async Task Balance_2Column_CheckedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_4Column_CheckedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_6Column_CheckedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_8Column_CheckedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region FinalizedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort

        [Test]
        public async Task Balance_2Column_FinalizedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_4Column_FinalizedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_6Column_FinalizedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_8Column_FinalizedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region ConfirmedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort

        [Test]
        public async Task Balance_2Column_ConfirmedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_4Column_ConfirmedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_6Column_ConfirmedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_8Column_ConfirmedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region ApprovedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort

        [Test]
        public async Task Balance_2Column_ApprovedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_4Column_ApprovedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_6Column_ApprovedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_8Column_ApprovedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region AllVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort

        [Test]
        public async Task Balance_2Column_AllVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_4Column_AllVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_6Column_AllVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_8Column_AllVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region CheckedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort

        [Test]
        public async Task Balance_2Column_CheckedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_4Column_CheckedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_6Column_CheckedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_8Column_CheckedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region FinalizedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort

        [Test]
        public async Task Balance_2Column_FinalizedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_4Column_FinalizedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_6Column_FinalizedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_8Column_FinalizedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region ConfirmedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort

        [Test]
        public async Task Balance_2Column_ConfirmedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_4Column_ConfirmedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_6Column_ConfirmedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_8Column_ConfirmedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region ApprovedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort

        [Test]
        public async Task Balance_2Column_ApprovedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_4Column_ApprovedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_6Column_ApprovedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_8Column_ApprovedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region AllVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort

        [Test]
        public async Task Balance_2Column_AllVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_4Column_AllVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_6Column_AllVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_8Column_AllVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region CheckedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort

        [Test]
        public async Task Balance_2Column_CheckedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_4Column_CheckedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_6Column_CheckedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_8Column_CheckedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region FinalizedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort

        [Test]
        public async Task Balance_2Column_FinalizedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_4Column_FinalizedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_6Column_FinalizedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_8Column_FinalizedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region ConfirmedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort

        [Test]
        public async Task Balance_2Column_ConfirmedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_4Column_ConfirmedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_6Column_ConfirmedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_8Column_ConfirmedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region ApprovedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort

        [Test]
        public async Task Balance_2Column_ApprovedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_4Column_ApprovedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_6Column_ApprovedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_8Column_ApprovedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region AllVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort

        [Test]
        public async Task Balance_2Column_AllVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_4Column_AllVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_6Column_AllVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_8Column_AllVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region CheckedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort

        [Test]
        public async Task Balance_2Column_CheckedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_4Column_CheckedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_6Column_CheckedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_8Column_CheckedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region FinalizedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort

        [Test]
        public async Task Balance_2Column_FinalizedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_4Column_FinalizedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_6Column_FinalizedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_8Column_FinalizedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region ConfirmedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort

        [Test]
        public async Task Balance_2Column_ConfirmedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_4Column_ConfirmedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_6Column_ConfirmedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_8Column_ConfirmedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region ApprovedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort

        [Test]
        public async Task Balance_2Column_ApprovedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_4Column_ApprovedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_6Column_ApprovedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_8Column_ApprovedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region AllVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort

        [Test]
        public async Task Balance_2Column_AllVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_4Column_AllVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_6Column_AllVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_8Column_AllVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region CheckedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort

        [Test]
        public async Task Balance_2Column_CheckedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_4Column_CheckedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_6Column_CheckedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_8Column_CheckedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region FinalizedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort

        [Test]
        public async Task Balance_2Column_FinalizedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_4Column_FinalizedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_6Column_FinalizedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_8Column_FinalizedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region ConfirmedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort

        [Test]
        public async Task Balance_2Column_ConfirmedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_4Column_ConfirmedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_6Column_ConfirmedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_8Column_ConfirmedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region ApprovedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort

        [Test]
        public async Task Balance_2Column_ApprovedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_4Column_ApprovedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_6Column_ApprovedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_8Column_ApprovedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region AllVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort

        [Test]
        public async Task Balance_2Column_AllVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_4Column_AllVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_6Column_AllVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_8Column_AllVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region CheckedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort

        [Test]
        public async Task Balance_2Column_CheckedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_4Column_CheckedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_6Column_CheckedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_8Column_CheckedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region FinalizedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort

        [Test]
        public async Task Balance_2Column_FinalizedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_4Column_FinalizedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_6Column_FinalizedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_8Column_FinalizedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region ConfirmedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort

        [Test]
        public async Task Balance_2Column_ConfirmedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_4Column_ConfirmedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_6Column_ConfirmedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_8Column_ConfirmedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region ApprovedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort

        [Test]
        public async Task Balance_2Column_ApprovedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_4Column_ApprovedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_6Column_ApprovedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_8Column_ApprovedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region AllVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort

        [Test]
        public async Task Balance_2Column_AllVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_4Column_AllVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_6Column_AllVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_8Column_AllVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region CheckedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort

        [Test]
        public async Task Balance_2Column_CheckedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_4Column_CheckedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_6Column_CheckedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_8Column_CheckedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region FinalizedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort

        [Test]
        public async Task Balance_2Column_FinalizedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_4Column_FinalizedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_6Column_FinalizedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_8Column_FinalizedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region ConfirmedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort

        [Test]
        public async Task Balance_2Column_ConfirmedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_4Column_ConfirmedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_6Column_ConfirmedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_8Column_ConfirmedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region ApprovedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort

        [Test]
        public async Task Balance_2Column_ApprovedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_4Column_ApprovedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_6Column_ApprovedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_8Column_ApprovedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region AllVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort

        [Test]
        public async Task Balance_2Column_AllVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_4Column_AllVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_6Column_AllVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_8Column_AllVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region CheckedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort

        [Test]
        public async Task Balance_2Column_CheckedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_4Column_CheckedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_6Column_CheckedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_8Column_CheckedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region FinalizedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort

        [Test]
        public async Task Balance_2Column_FinalizedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_4Column_FinalizedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_6Column_FinalizedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_8Column_FinalizedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region ConfirmedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort

        [Test]
        public async Task Balance_2Column_ConfirmedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_4Column_ConfirmedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_6Column_ConfirmedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_8Column_ConfirmedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region ApprovedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort

        [Test]
        public async Task Balance_2Column_ApprovedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_4Column_ApprovedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_6Column_ApprovedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_8Column_ApprovedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region AllVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort

        [Test]
        public async Task Balance_2Column_AllVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_4Column_AllVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_6Column_AllVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_8Column_AllVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region CheckedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort

        [Test]
        public async Task Balance_2Column_CheckedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_4Column_CheckedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_6Column_CheckedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_8Column_CheckedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region FinalizedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort

        [Test]
        public async Task Balance_2Column_FinalizedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_4Column_FinalizedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_6Column_FinalizedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_8Column_FinalizedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region ConfirmedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort

        [Test]
        public async Task Balance_2Column_ConfirmedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_4Column_ConfirmedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_6Column_ConfirmedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_8Column_ConfirmedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region ApprovedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort

        [Test]
        public async Task Balance_2Column_ApprovedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_4Column_ApprovedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_6Column_ApprovedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_8Column_ApprovedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region AllVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort

        [Test]
        public async Task Balance_2Column_AllVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_4Column_AllVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_6Column_AllVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_8Column_AllVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region CheckedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort

        [Test]
        public async Task Balance_2Column_CheckedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_4Column_CheckedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_6Column_CheckedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_8Column_CheckedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region FinalizedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort

        [Test]
        public async Task Balance_2Column_FinalizedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_4Column_FinalizedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_6Column_FinalizedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_8Column_FinalizedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region ConfirmedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort

        [Test]
        public async Task Balance_2Column_ConfirmedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_4Column_ConfirmedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_6Column_ConfirmedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_8Column_ConfirmedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region ApprovedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort

        [Test]
        public async Task Balance_2Column_ApprovedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_4Column_ApprovedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_6Column_ApprovedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_8Column_ApprovedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region AllVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort

        [Test]
        public async Task Balance_2Column_AllVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_4Column_AllVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_6Column_AllVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task Balance_8Column_AllVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #endregion

        #region Level Balance By No Tests

        #region CheckedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort

        [Test]
        public async Task BalanceByNo_2Column_CheckedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_4Column_CheckedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_6Column_CheckedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_8Column_CheckedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region FinalizedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort

        [Test]
        public async Task BalanceByNo_2Column_FinalizedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_4Column_FinalizedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_6Column_FinalizedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_8Column_FinalizedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region ConfirmedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort

        [Test]
        public async Task BalanceByNo_2Column_ConfirmedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_4Column_ConfirmedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_6Column_ConfirmedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_8Column_ConfirmedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region ApprovedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort

        [Test]
        public async Task BalanceByNo_2Column_ApprovedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_4Column_ApprovedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_6Column_ApprovedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_8Column_ApprovedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region AllVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort

        [Test]
        public async Task BalanceByNo_2Column_AllVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_4Column_AllVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_6Column_AllVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_8Column_AllVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region CheckedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort

        [Test]
        public async Task BalanceByNo_2Column_CheckedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_4Column_CheckedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_6Column_CheckedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_8Column_CheckedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region FinalizedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort

        [Test]
        public async Task BalanceByNo_2Column_FinalizedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_4Column_FinalizedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_6Column_FinalizedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_8Column_FinalizedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region ConfirmedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort

        [Test]
        public async Task BalanceByNo_2Column_ConfirmedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_4Column_ConfirmedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_6Column_ConfirmedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_8Column_ConfirmedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region ApprovedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort

        [Test]
        public async Task BalanceByNo_2Column_ApprovedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_4Column_ApprovedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_6Column_ApprovedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_8Column_ApprovedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region AllVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort

        [Test]
        public async Task BalanceByNo_2Column_AllVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_4Column_AllVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_6Column_AllVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_8Column_AllVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region CheckedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort

        [Test]
        public async Task BalanceByNo_2Column_CheckedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_4Column_CheckedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_6Column_CheckedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_8Column_CheckedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region FinalizedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort

        [Test]
        public async Task BalanceByNo_2Column_FinalizedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_4Column_FinalizedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_6Column_FinalizedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_8Column_FinalizedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region ConfirmedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort

        [Test]
        public async Task BalanceByNo_2Column_ConfirmedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_4Column_ConfirmedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_6Column_ConfirmedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_8Column_ConfirmedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region ApprovedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort

        [Test]
        public async Task BalanceByNo_2Column_ApprovedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_4Column_ApprovedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_6Column_ApprovedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_8Column_ApprovedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region AllVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort

        [Test]
        public async Task BalanceByNo_2Column_AllVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_4Column_AllVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_6Column_AllVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_8Column_AllVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region CheckedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort

        [Test]
        public async Task BalanceByNo_2Column_CheckedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_4Column_CheckedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_6Column_CheckedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_8Column_CheckedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region FinalizedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort

        [Test]
        public async Task BalanceByNo_2Column_FinalizedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_4Column_FinalizedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_6Column_FinalizedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_8Column_FinalizedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region ConfirmedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort

        [Test]
        public async Task BalanceByNo_2Column_ConfirmedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_4Column_ConfirmedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_6Column_ConfirmedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_8Column_ConfirmedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region ApprovedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort

        [Test]
        public async Task BalanceByNo_2Column_ApprovedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_4Column_ApprovedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_6Column_ApprovedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_8Column_ApprovedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region AllVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort

        [Test]
        public async Task BalanceByNo_2Column_AllVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_4Column_AllVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_6Column_AllVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_8Column_AllVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region CheckedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort

        [Test]
        public async Task BalanceByNo_2Column_CheckedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_4Column_CheckedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_6Column_CheckedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_8Column_CheckedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region FinalizedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort

        [Test]
        public async Task BalanceByNo_2Column_FinalizedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_4Column_FinalizedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_6Column_FinalizedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_8Column_FinalizedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region ConfirmedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort

        [Test]
        public async Task BalanceByNo_2Column_ConfirmedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_4Column_ConfirmedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_6Column_ConfirmedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_8Column_ConfirmedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region ApprovedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort

        [Test]
        public async Task BalanceByNo_2Column_ApprovedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_4Column_ApprovedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_6Column_ApprovedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_8Column_ApprovedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region AllVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort

        [Test]
        public async Task BalanceByNo_2Column_AllVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_4Column_AllVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_6Column_AllVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_8Column_AllVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region CheckedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort

        [Test]
        public async Task BalanceByNo_2Column_CheckedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_4Column_CheckedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_6Column_CheckedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_8Column_CheckedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region FinalizedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort

        [Test]
        public async Task BalanceByNo_2Column_FinalizedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_4Column_FinalizedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_6Column_FinalizedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_8Column_FinalizedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region ConfirmedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort

        [Test]
        public async Task BalanceByNo_2Column_ConfirmedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_4Column_ConfirmedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_6Column_ConfirmedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_8Column_ConfirmedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region ApprovedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort

        [Test]
        public async Task BalanceByNo_2Column_ApprovedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_4Column_ApprovedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_6Column_ApprovedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_8Column_ApprovedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region AllVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort

        [Test]
        public async Task BalanceByNo_2Column_AllVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_4Column_AllVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_6Column_AllVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_8Column_AllVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region CheckedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort

        [Test]
        public async Task BalanceByNo_2Column_CheckedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_4Column_CheckedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_6Column_CheckedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_8Column_CheckedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region FinalizedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort

        [Test]
        public async Task BalanceByNo_2Column_FinalizedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_4Column_FinalizedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_6Column_FinalizedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_8Column_FinalizedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region ConfirmedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort

        [Test]
        public async Task BalanceByNo_2Column_ConfirmedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_4Column_ConfirmedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_6Column_ConfirmedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_8Column_ConfirmedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region ApprovedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort

        [Test]
        public async Task BalanceByNo_2Column_ApprovedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_4Column_ApprovedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_6Column_ApprovedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_8Column_ApprovedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region AllVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort

        [Test]
        public async Task BalanceByNo_2Column_AllVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_4Column_AllVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_6Column_AllVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_8Column_AllVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region CheckedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort

        [Test]
        public async Task BalanceByNo_2Column_CheckedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_4Column_CheckedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_6Column_CheckedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_8Column_CheckedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region FinalizedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort

        [Test]
        public async Task BalanceByNo_2Column_FinalizedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_4Column_FinalizedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_6Column_FinalizedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_8Column_FinalizedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region ConfirmedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort

        [Test]
        public async Task BalanceByNo_2Column_ConfirmedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_4Column_ConfirmedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_6Column_ConfirmedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_8Column_ConfirmedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region ApprovedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort

        [Test]
        public async Task BalanceByNo_2Column_ApprovedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_4Column_ApprovedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_6Column_ApprovedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_8Column_ApprovedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region AllVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort

        [Test]
        public async Task BalanceByNo_2Column_AllVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_4Column_AllVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_6Column_AllVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_8Column_AllVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region CheckedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_2Column_CheckedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_4Column_CheckedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_6Column_CheckedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_8Column_CheckedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region FinalizedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_2Column_FinalizedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_4Column_FinalizedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_6Column_FinalizedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_8Column_FinalizedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region ConfirmedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_2Column_ConfirmedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_4Column_ConfirmedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_6Column_ConfirmedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_8Column_ConfirmedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region ApprovedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_2Column_ApprovedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_4Column_ApprovedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_6Column_ApprovedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_8Column_ApprovedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region AllVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_2Column_AllVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_4Column_AllVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_6Column_AllVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_8Column_AllVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region CheckedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_2Column_CheckedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_4Column_CheckedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_6Column_CheckedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_8Column_CheckedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region FinalizedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_2Column_FinalizedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_4Column_FinalizedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_6Column_FinalizedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_8Column_FinalizedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region ConfirmedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_2Column_ConfirmedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_4Column_ConfirmedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_6Column_ConfirmedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_8Column_ConfirmedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region ApprovedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_2Column_ApprovedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_4Column_ApprovedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_6Column_ApprovedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_8Column_ApprovedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region AllVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_2Column_AllVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_4Column_AllVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_6Column_AllVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_8Column_AllVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region CheckedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_2Column_CheckedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_4Column_CheckedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_6Column_CheckedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_8Column_CheckedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region FinalizedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_2Column_FinalizedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_4Column_FinalizedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_6Column_FinalizedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_8Column_FinalizedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region ConfirmedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_2Column_ConfirmedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_4Column_ConfirmedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_6Column_ConfirmedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_8Column_ConfirmedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region ApprovedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_2Column_ApprovedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_4Column_ApprovedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_6Column_ApprovedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_8Column_ApprovedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region AllVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_2Column_AllVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_4Column_AllVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_6Column_AllVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_8Column_AllVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region CheckedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_2Column_CheckedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_4Column_CheckedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_6Column_CheckedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_8Column_CheckedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region FinalizedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_2Column_FinalizedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_4Column_FinalizedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_6Column_FinalizedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_8Column_FinalizedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region ConfirmedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_2Column_ConfirmedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_4Column_ConfirmedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_6Column_ConfirmedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_8Column_ConfirmedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region ApprovedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_2Column_ApprovedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_4Column_ApprovedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_6Column_ApprovedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_8Column_ApprovedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region AllVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_2Column_AllVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_4Column_AllVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_6Column_AllVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_8Column_AllVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region CheckedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_2Column_CheckedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_4Column_CheckedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_6Column_CheckedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_8Column_CheckedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region FinalizedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_2Column_FinalizedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_4Column_FinalizedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_6Column_FinalizedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_8Column_FinalizedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region ConfirmedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_2Column_ConfirmedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_4Column_ConfirmedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_6Column_ConfirmedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_8Column_ConfirmedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region ApprovedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_2Column_ApprovedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_4Column_ApprovedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_6Column_ApprovedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_8Column_ApprovedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region AllVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_2Column_AllVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_4Column_AllVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_6Column_AllVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_8Column_AllVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region CheckedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_2Column_CheckedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_4Column_CheckedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_6Column_CheckedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_8Column_CheckedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region FinalizedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_2Column_FinalizedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_4Column_FinalizedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_6Column_FinalizedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_8Column_FinalizedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region ConfirmedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_2Column_ConfirmedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_4Column_ConfirmedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_6Column_ConfirmedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_8Column_ConfirmedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region ApprovedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_2Column_ApprovedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_4Column_ApprovedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_6Column_ApprovedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_8Column_ApprovedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region AllVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_2Column_AllVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_4Column_AllVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_6Column_AllVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_8Column_AllVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region CheckedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_2Column_CheckedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_4Column_CheckedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_6Column_CheckedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_8Column_CheckedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region FinalizedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_2Column_FinalizedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_4Column_FinalizedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_6Column_FinalizedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_8Column_FinalizedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region ConfirmedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_2Column_ConfirmedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_4Column_ConfirmedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_6Column_ConfirmedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_8Column_ConfirmedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region ApprovedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_2Column_ApprovedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_4Column_ApprovedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_6Column_ApprovedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_8Column_ApprovedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region AllVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_2Column_AllVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_4Column_AllVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_6Column_AllVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_8Column_AllVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region CheckedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_2Column_CheckedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_4Column_CheckedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_6Column_CheckedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_8Column_CheckedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region FinalizedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_2Column_FinalizedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_4Column_FinalizedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_6Column_FinalizedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_8Column_FinalizedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region ConfirmedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_2Column_ConfirmedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_4Column_ConfirmedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_6Column_ConfirmedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_8Column_ConfirmedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region ApprovedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_2Column_ApprovedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_4Column_ApprovedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_6Column_ApprovedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_8Column_ApprovedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region AllVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_2Column_AllVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_4Column_AllVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_6Column_AllVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_8Column_AllVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region CheckedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_2Column_CheckedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_4Column_CheckedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_6Column_CheckedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_8Column_CheckedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region FinalizedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_2Column_FinalizedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_4Column_FinalizedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_6Column_FinalizedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_8Column_FinalizedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region ConfirmedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_2Column_ConfirmedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_4Column_ConfirmedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_6Column_ConfirmedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_8Column_ConfirmedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region ApprovedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_2Column_ApprovedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_4Column_ApprovedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_6Column_ApprovedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_8Column_ApprovedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region AllVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_2Column_AllVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_4Column_AllVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_6Column_AllVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_8Column_AllVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region CheckedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_2Column_CheckedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_4Column_CheckedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_6Column_CheckedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_8Column_CheckedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region FinalizedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_2Column_FinalizedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_4Column_FinalizedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_6Column_FinalizedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_8Column_FinalizedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region ConfirmedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_2Column_ConfirmedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_4Column_ConfirmedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_6Column_ConfirmedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_8Column_ConfirmedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region ApprovedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_2Column_ApprovedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_4Column_ApprovedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_6Column_ApprovedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_8Column_ApprovedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region AllVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_2Column_AllVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_4Column_AllVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_6Column_AllVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_8Column_AllVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region CheckedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_2Column_CheckedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_4Column_CheckedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_6Column_CheckedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_8Column_CheckedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region FinalizedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_2Column_FinalizedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_4Column_FinalizedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_6Column_FinalizedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_8Column_FinalizedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region ConfirmedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_2Column_ConfirmedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_4Column_ConfirmedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_6Column_ConfirmedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_8Column_ConfirmedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region ApprovedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_2Column_ApprovedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_4Column_ApprovedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_6Column_ApprovedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_8Column_ApprovedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region AllVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_2Column_AllVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_4Column_AllVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_6Column_AllVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_8Column_AllVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region CheckedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_2Column_CheckedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_4Column_CheckedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_6Column_CheckedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_8Column_CheckedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region FinalizedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_2Column_FinalizedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_4Column_FinalizedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_6Column_FinalizedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_8Column_FinalizedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region ConfirmedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_2Column_ConfirmedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_4Column_ConfirmedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_6Column_ConfirmedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_8Column_ConfirmedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region ApprovedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_2Column_ApprovedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_4Column_ApprovedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_6Column_ApprovedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_8Column_ApprovedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region AllVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_2Column_AllVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_4Column_AllVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_6Column_AllVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_8Column_AllVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region CheckedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_2Column_CheckedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_4Column_CheckedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_6Column_CheckedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_8Column_CheckedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region FinalizedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_2Column_FinalizedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_4Column_FinalizedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_6Column_FinalizedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_8Column_FinalizedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region ConfirmedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_2Column_ConfirmedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_4Column_ConfirmedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_6Column_ConfirmedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_8Column_ConfirmedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region ApprovedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_2Column_ApprovedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_4Column_ApprovedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_6Column_ApprovedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_8Column_ApprovedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region AllVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_2Column_AllVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_4Column_AllVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_6Column_AllVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_8Column_AllVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region CheckedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_2Column_CheckedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_4Column_CheckedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_6Column_CheckedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_8Column_CheckedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region FinalizedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_2Column_FinalizedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_4Column_FinalizedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_6Column_FinalizedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_8Column_FinalizedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region ConfirmedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_2Column_ConfirmedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_4Column_ConfirmedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_6Column_ConfirmedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_8Column_ConfirmedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region ApprovedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_2Column_ApprovedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_4Column_ApprovedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_6Column_ApprovedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_8Column_ApprovedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region AllVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_2Column_AllVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_4Column_AllVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_6Column_AllVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_8Column_AllVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region CheckedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_2Column_CheckedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_4Column_CheckedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_6Column_CheckedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_8Column_CheckedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region FinalizedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_2Column_FinalizedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_4Column_FinalizedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_6Column_FinalizedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_8Column_FinalizedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region ConfirmedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_2Column_ConfirmedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_4Column_ConfirmedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_6Column_ConfirmedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_8Column_ConfirmedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region ApprovedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_2Column_ApprovedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_4Column_ApprovedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_6Column_ApprovedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_8Column_ApprovedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region AllVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_2Column_AllVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_4Column_AllVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_6Column_AllVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_8Column_AllVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region CheckedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_2Column_CheckedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_4Column_CheckedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_6Column_CheckedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_8Column_CheckedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region FinalizedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_2Column_FinalizedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_4Column_FinalizedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_6Column_FinalizedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_8Column_FinalizedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region ConfirmedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_2Column_ConfirmedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_4Column_ConfirmedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_6Column_ConfirmedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_8Column_ConfirmedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region ApprovedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_2Column_ApprovedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_4Column_ApprovedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_6Column_ApprovedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_8Column_ApprovedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region AllVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_2Column_AllVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_4Column_AllVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_6Column_AllVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_8Column_AllVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region CheckedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_2Column_CheckedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_4Column_CheckedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_6Column_CheckedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_8Column_CheckedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region FinalizedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_2Column_FinalizedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_4Column_FinalizedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_6Column_FinalizedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_8Column_FinalizedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region ConfirmedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_2Column_ConfirmedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_4Column_ConfirmedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_6Column_ConfirmedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_8Column_ConfirmedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region ApprovedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_2Column_ApprovedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_4Column_ApprovedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_6Column_ApprovedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_8Column_ApprovedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region AllVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_2Column_AllVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_4Column_AllVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_6Column_AllVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_8Column_AllVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region CheckedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_2Column_CheckedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_4Column_CheckedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_6Column_CheckedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_8Column_CheckedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region FinalizedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_2Column_FinalizedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_4Column_FinalizedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_6Column_FinalizedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_8Column_FinalizedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region ConfirmedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_2Column_ConfirmedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_4Column_ConfirmedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_6Column_ConfirmedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_8Column_ConfirmedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region ApprovedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_2Column_ApprovedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_4Column_ApprovedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_6Column_ApprovedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_8Column_ApprovedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region AllVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_2Column_AllVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_4Column_AllVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_6Column_AllVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_8Column_AllVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region CheckedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_2Column_CheckedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_4Column_CheckedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_6Column_CheckedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_8Column_CheckedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region FinalizedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_2Column_FinalizedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_4Column_FinalizedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_6Column_FinalizedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_8Column_FinalizedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region ConfirmedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_2Column_ConfirmedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_4Column_ConfirmedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_6Column_ConfirmedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_8Column_ConfirmedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region ApprovedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_2Column_ApprovedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_4Column_ApprovedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_6Column_ApprovedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_8Column_ApprovedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region AllVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_2Column_AllVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_4Column_AllVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_6Column_AllVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_8Column_AllVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region CheckedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_2Column_CheckedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_4Column_CheckedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_6Column_CheckedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_8Column_CheckedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region FinalizedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_2Column_FinalizedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_4Column_FinalizedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_6Column_FinalizedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_8Column_FinalizedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region ConfirmedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_2Column_ConfirmedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_4Column_ConfirmedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_6Column_ConfirmedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_8Column_ConfirmedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region ApprovedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_2Column_ApprovedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_4Column_ApprovedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_6Column_ApprovedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_8Column_ApprovedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region AllVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort

        [Test]
        public async Task BalanceByNo_2Column_AllVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_4Column_AllVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_6Column_AllVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_8Column_AllVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region CheckedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort

        [Test]
        public async Task BalanceByNo_2Column_CheckedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_4Column_CheckedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_6Column_CheckedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_8Column_CheckedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region FinalizedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort

        [Test]
        public async Task BalanceByNo_2Column_FinalizedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_4Column_FinalizedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_6Column_FinalizedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_8Column_FinalizedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region ConfirmedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort

        [Test]
        public async Task BalanceByNo_2Column_ConfirmedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_4Column_ConfirmedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_6Column_ConfirmedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_8Column_ConfirmedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region ApprovedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort

        [Test]
        public async Task BalanceByNo_2Column_ApprovedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_4Column_ApprovedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_6Column_ApprovedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_8Column_ApprovedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region AllVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort

        [Test]
        public async Task BalanceByNo_2Column_AllVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_4Column_AllVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_6Column_AllVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_8Column_AllVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region CheckedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort

        [Test]
        public async Task BalanceByNo_2Column_CheckedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_4Column_CheckedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_6Column_CheckedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_8Column_CheckedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region FinalizedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort

        [Test]
        public async Task BalanceByNo_2Column_FinalizedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_4Column_FinalizedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_6Column_FinalizedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_8Column_FinalizedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region ConfirmedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort

        [Test]
        public async Task BalanceByNo_2Column_ConfirmedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_4Column_ConfirmedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_6Column_ConfirmedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_8Column_ConfirmedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region ApprovedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort

        [Test]
        public async Task BalanceByNo_2Column_ApprovedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_4Column_ApprovedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_6Column_ApprovedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_8Column_ApprovedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region AllVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort

        [Test]
        public async Task BalanceByNo_2Column_AllVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_4Column_AllVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_6Column_AllVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_8Column_AllVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region CheckedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort

        [Test]
        public async Task BalanceByNo_2Column_CheckedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_4Column_CheckedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_6Column_CheckedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_8Column_CheckedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region FinalizedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort

        [Test]
        public async Task BalanceByNo_2Column_FinalizedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_4Column_FinalizedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_6Column_FinalizedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_8Column_FinalizedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region ConfirmedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort

        [Test]
        public async Task BalanceByNo_2Column_ConfirmedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_4Column_ConfirmedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_6Column_ConfirmedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_8Column_ConfirmedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region ApprovedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort

        [Test]
        public async Task BalanceByNo_2Column_ApprovedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_4Column_ApprovedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_6Column_ApprovedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_8Column_ApprovedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region AllVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort

        [Test]
        public async Task BalanceByNo_2Column_AllVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_4Column_AllVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_6Column_AllVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_8Column_AllVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region CheckedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort

        [Test]
        public async Task BalanceByNo_2Column_CheckedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_4Column_CheckedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_6Column_CheckedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_8Column_CheckedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_CheckedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region FinalizedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort

        [Test]
        public async Task BalanceByNo_2Column_FinalizedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_4Column_FinalizedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_6Column_FinalizedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_8Column_FinalizedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region ConfirmedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort

        [Test]
        public async Task BalanceByNo_2Column_ConfirmedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_4Column_ConfirmedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_6Column_ConfirmedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_8Column_ConfirmedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region ApprovedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort

        [Test]
        public async Task BalanceByNo_2Column_ApprovedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_4Column_ApprovedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_6Column_ApprovedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_8Column_ApprovedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #region AllVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort

        [Test]
        public async Task BalanceByNo_2Column_AllVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort(
                TestBalanceFormat.TwoColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_4Column_AllVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort(
                TestBalanceFormat.FourColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_6Column_AllVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort(
                TestBalanceFormat.SixColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        [Test]
        public async Task BalanceByNo_8Column_AllVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort()
        {
            // Arrange & Act
            var balance = await GetLevelBalance_AllVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort(
                TestBalanceFormat.EightColumn);

            // Assert
            Assert.That(balance, Is.Not.Null);
        }

        #endregion

        #endregion

        private static FinanceReportOptions GetFormOptions()
        {
            return FinanceReportOptions.UseClosingVoucher
                | FinanceReportOptions.UseClosingTempVoucher
                | FinanceReportOptions.ShowZeroBalanceItems;
        }

        private static FinanceReportOptions GetFormAndOpeningOptions()
        {
            return FinanceReportOptions.UseClosingVoucher
                | FinanceReportOptions.UseClosingTempVoucher
                | FinanceReportOptions.ShowZeroBalanceItems
                | FinanceReportOptions.OpeningAsFirstVoucher;
        }

        private static FinanceReportOptions GetFormAndTurnoverOptions()
        {
            return FinanceReportOptions.UseClosingVoucher
                | FinanceReportOptions.UseClosingTempVoucher
                | FinanceReportOptions.ShowZeroBalanceItems
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
            _repository = new TestBalanceRepositoryDirect(repoContext, sysRepo, utility);
        }

        #region Level Balance Implementation

        #region GetLevelBalance_CurrentBranch_NoGrouping_NoOptions_NoFilterSort

        private async Task<TestBalanceViewModel> GetLevelBalance_CheckedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort(
            TestBalanceFormat format, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseCheckedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, format, TestBalanceMode.Level, FinanceReportOptions.None)
                : GetByNoTestParameters(gridOptions, format, TestBalanceMode.Level, FinanceReportOptions.None);

            return await _repository.GetLevelBalanceAsync(0, parameters);
        }

        private async Task<TestBalanceViewModel> GetLevelBalance_FinalizedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort(
            TestBalanceFormat format, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseFinalizedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, format, TestBalanceMode.Level, FinanceReportOptions.None)
                : GetByNoTestParameters(gridOptions, format, TestBalanceMode.Level, FinanceReportOptions.None);

            return await _repository.GetLevelBalanceAsync(0, parameters);
        }

        private async Task<TestBalanceViewModel> GetLevelBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort(
            TestBalanceFormat format, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseConfirmedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, format, TestBalanceMode.Level, FinanceReportOptions.None)
                : GetByNoTestParameters(gridOptions, format, TestBalanceMode.Level, FinanceReportOptions.None);

            return await _repository.GetLevelBalanceAsync(0, parameters);
        }

        private async Task<TestBalanceViewModel> GetLevelBalance_ApprovedVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort(
            TestBalanceFormat format, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseApprovedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, format, TestBalanceMode.Level, FinanceReportOptions.None)
                : GetByNoTestParameters(gridOptions, format, TestBalanceMode.Level, FinanceReportOptions.None);

            return await _repository.GetLevelBalanceAsync(0, parameters);
        }

        private async Task<TestBalanceViewModel> GetLevelBalance_AllVouchers_CurrentBranch_NoGrouping_NoOptions_NoFilterSort(
            TestBalanceFormat format, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, format, TestBalanceMode.Level, FinanceReportOptions.None)
                : GetByNoTestParameters(gridOptions, format, TestBalanceMode.Level, FinanceReportOptions.None);

            return await _repository.GetLevelBalanceAsync(0, parameters);
        }

        #endregion

        #region GetLevelBalance_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort

        private async Task<TestBalanceViewModel> GetLevelBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort(
            TestBalanceFormat format, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .UseCheckedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, format, TestBalanceMode.Level, FinanceReportOptions.None)
                : GetByNoTestParameters(gridOptions, format, TestBalanceMode.Level, FinanceReportOptions.None);

            return await _repository.GetLevelBalanceAsync(0, parameters);
        }

        private async Task<TestBalanceViewModel> GetLevelBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort(
            TestBalanceFormat format, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .UseFinalizedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, format, TestBalanceMode.Level, FinanceReportOptions.None)
                : GetByNoTestParameters(gridOptions, format, TestBalanceMode.Level, FinanceReportOptions.None);

            return await _repository.GetLevelBalanceAsync(0, parameters);
        }

        private async Task<TestBalanceViewModel> GetLevelBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort(
            TestBalanceFormat format, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .UseConfirmedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, format, TestBalanceMode.Level, FinanceReportOptions.None)
                : GetByNoTestParameters(gridOptions, format, TestBalanceMode.Level, FinanceReportOptions.None);

            return await _repository.GetLevelBalanceAsync(0, parameters);
        }

        private async Task<TestBalanceViewModel> GetLevelBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort(
            TestBalanceFormat format, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .UseApprovedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, format, TestBalanceMode.Level, FinanceReportOptions.None)
                : GetByNoTestParameters(gridOptions, format, TestBalanceMode.Level, FinanceReportOptions.None);

            return await _repository.GetLevelBalanceAsync(0, parameters);
        }

        private async Task<TestBalanceViewModel> GetLevelBalance_AllVouchers_CurrentAndChildren_NoGrouping_NoOptions_NoFilterSort(
            TestBalanceFormat format, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, format, TestBalanceMode.Level, FinanceReportOptions.None)
                : GetByNoTestParameters(gridOptions, format, TestBalanceMode.Level, FinanceReportOptions.None);

            return await _repository.GetLevelBalanceAsync(0, parameters);
        }

        #endregion

        #region GetLevelBalance_CurrentBranch_ByBranch_NoOptions_NoFilterSort

        private async Task<TestBalanceViewModel> GetLevelBalance_CheckedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort(
            TestBalanceFormat format, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseCheckedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, format, TestBalanceMode.Level, FinanceReportOptions.None, true)
                : GetByNoTestParameters(gridOptions, format, TestBalanceMode.Level, FinanceReportOptions.None, true);

            return await _repository.GetLevelBalanceAsync(0, parameters);
        }

        private async Task<TestBalanceViewModel> GetLevelBalance_FinalizedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort(
            TestBalanceFormat format, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseFinalizedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, format, TestBalanceMode.Level, FinanceReportOptions.None, true)
                : GetByNoTestParameters(gridOptions, format, TestBalanceMode.Level, FinanceReportOptions.None, true);

            return await _repository.GetLevelBalanceAsync(0, parameters);
        }

        private async Task<TestBalanceViewModel> GetLevelBalance_ConfirmedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort(
            TestBalanceFormat format, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseConfirmedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, format, TestBalanceMode.Level, FinanceReportOptions.None, true)
                : GetByNoTestParameters(gridOptions, format, TestBalanceMode.Level, FinanceReportOptions.None, true);

            return await _repository.GetLevelBalanceAsync(0, parameters);
        }

        private async Task<TestBalanceViewModel> GetLevelBalance_ApprovedVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort(
            TestBalanceFormat format, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseApprovedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, format, TestBalanceMode.Level, FinanceReportOptions.None, true)
                : GetByNoTestParameters(gridOptions, format, TestBalanceMode.Level, FinanceReportOptions.None, true);

            return await _repository.GetLevelBalanceAsync(0, parameters);
        }

        private async Task<TestBalanceViewModel> GetLevelBalance_AllVouchers_CurrentBranch_ByBranch_NoOptions_NoFilterSort(
            TestBalanceFormat format, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, format, TestBalanceMode.Level, FinanceReportOptions.None, true)
                : GetByNoTestParameters(gridOptions, format, TestBalanceMode.Level, FinanceReportOptions.None, true);

            return await _repository.GetLevelBalanceAsync(0, parameters);
        }

        #endregion

        #region GetLevelBalance_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort

        private async Task<TestBalanceViewModel> GetLevelBalance_CheckedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort(
            TestBalanceFormat format, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .UseCheckedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, format, TestBalanceMode.Level, FinanceReportOptions.None, true)
                : GetByNoTestParameters(gridOptions, format, TestBalanceMode.Level, FinanceReportOptions.None, true);

            return await _repository.GetLevelBalanceAsync(0, parameters);
        }

        private async Task<TestBalanceViewModel> GetLevelBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort(
            TestBalanceFormat format, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .UseFinalizedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, format, TestBalanceMode.Level, FinanceReportOptions.None, true)
                : GetByNoTestParameters(gridOptions, format, TestBalanceMode.Level, FinanceReportOptions.None, true);

            return await _repository.GetLevelBalanceAsync(0, parameters);
        }

        private async Task<TestBalanceViewModel> GetLevelBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort(
            TestBalanceFormat format, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .UseConfirmedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, format, TestBalanceMode.Level, FinanceReportOptions.None, true)
                : GetByNoTestParameters(gridOptions, format, TestBalanceMode.Level, FinanceReportOptions.None, true);

            return await _repository.GetLevelBalanceAsync(0, parameters);
        }

        private async Task<TestBalanceViewModel> GetLevelBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort(
            TestBalanceFormat format, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .UseApprovedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, format, TestBalanceMode.Level, FinanceReportOptions.None, true)
                : GetByNoTestParameters(gridOptions, format, TestBalanceMode.Level, FinanceReportOptions.None, true);

            return await _repository.GetLevelBalanceAsync(0, parameters);
        }

        private async Task<TestBalanceViewModel> GetLevelBalance_AllVouchers_CurrentAndChildren_ByBranch_NoOptions_NoFilterSort(
            TestBalanceFormat format, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, format, TestBalanceMode.Level, FinanceReportOptions.None, true)
                : GetByNoTestParameters(gridOptions, format, TestBalanceMode.Level, FinanceReportOptions.None, true);

            return await _repository.GetLevelBalanceAsync(0, parameters);
        }

        #endregion

        #region GetLevelBalance_CurrentBranch_NoGrouping_FormOptions_NoFilterSort

        private async Task<TestBalanceViewModel> GetLevelBalance_CheckedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort(
            TestBalanceFormat format, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseCheckedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, format, TestBalanceMode.Level, GetFormOptions())
                : GetByNoTestParameters(gridOptions, format, TestBalanceMode.Level, GetFormOptions());

            return await _repository.GetLevelBalanceAsync(0, parameters);
        }

        private async Task<TestBalanceViewModel> GetLevelBalance_FinalizedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort(
            TestBalanceFormat format, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseFinalizedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, format, TestBalanceMode.Level, GetFormOptions())
                : GetByNoTestParameters(gridOptions, format, TestBalanceMode.Level, GetFormOptions());

            return await _repository.GetLevelBalanceAsync(0, parameters);
        }

        private async Task<TestBalanceViewModel> GetLevelBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort(
            TestBalanceFormat format, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseConfirmedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, format, TestBalanceMode.Level, GetFormOptions())
                : GetByNoTestParameters(gridOptions, format, TestBalanceMode.Level, GetFormOptions());

            return await _repository.GetLevelBalanceAsync(0, parameters);
        }

        private async Task<TestBalanceViewModel> GetLevelBalance_ApprovedVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort(
            TestBalanceFormat format, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseApprovedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, format, TestBalanceMode.Level, GetFormOptions())
                : GetByNoTestParameters(gridOptions, format, TestBalanceMode.Level, GetFormOptions());

            return await _repository.GetLevelBalanceAsync(0, parameters);
        }

        private async Task<TestBalanceViewModel> GetLevelBalance_AllVouchers_CurrentBranch_NoGrouping_FormOptions_NoFilterSort(
            TestBalanceFormat format, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, format, TestBalanceMode.Level, GetFormOptions())
                : GetByNoTestParameters(gridOptions, format, TestBalanceMode.Level, GetFormOptions());

            return await _repository.GetLevelBalanceAsync(0, parameters);
        }

        #endregion

        #region GetLevelBalance_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort

        private async Task<TestBalanceViewModel> GetLevelBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort(
            TestBalanceFormat format, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .UseCheckedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, format, TestBalanceMode.Level, GetFormOptions())
                : GetByNoTestParameters(gridOptions, format, TestBalanceMode.Level, GetFormOptions());

            return await _repository.GetLevelBalanceAsync(0, parameters);
        }

        private async Task<TestBalanceViewModel> GetLevelBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort(
            TestBalanceFormat format, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .UseFinalizedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, format, TestBalanceMode.Level, GetFormOptions())
                : GetByNoTestParameters(gridOptions, format, TestBalanceMode.Level, GetFormOptions());

            return await _repository.GetLevelBalanceAsync(0, parameters);
        }

        private async Task<TestBalanceViewModel> GetLevelBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort(
            TestBalanceFormat format, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .UseConfirmedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, format, TestBalanceMode.Level, GetFormOptions())
                : GetByNoTestParameters(gridOptions, format, TestBalanceMode.Level, GetFormOptions());

            return await _repository.GetLevelBalanceAsync(0, parameters);
        }

        private async Task<TestBalanceViewModel> GetLevelBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort(
            TestBalanceFormat format, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .UseApprovedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, format, TestBalanceMode.Level, GetFormOptions())
                : GetByNoTestParameters(gridOptions, format, TestBalanceMode.Level, GetFormOptions());

            return await _repository.GetLevelBalanceAsync(0, parameters);
        }

        private async Task<TestBalanceViewModel> GetLevelBalance_AllVouchers_CurrentAndChildren_NoGrouping_FormOptions_NoFilterSort(
            TestBalanceFormat format, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, format, TestBalanceMode.Level, GetFormOptions())
                : GetByNoTestParameters(gridOptions, format, TestBalanceMode.Level, GetFormOptions());

            return await _repository.GetLevelBalanceAsync(0, parameters);
        }

        #endregion

        #region GetLevelBalance_CurrentBranch_ByBranch_FormOptions_NoFilterSort

        private async Task<TestBalanceViewModel> GetLevelBalance_CheckedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort(
            TestBalanceFormat format, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseCheckedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, format, TestBalanceMode.Level, GetFormOptions(), true)
                : GetByNoTestParameters(gridOptions, format, TestBalanceMode.Level, GetFormOptions(), true);

            return await _repository.GetLevelBalanceAsync(0, parameters);
        }

        private async Task<TestBalanceViewModel> GetLevelBalance_FinalizedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort(
            TestBalanceFormat format, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseFinalizedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, format, TestBalanceMode.Level, GetFormOptions(), true)
                : GetByNoTestParameters(gridOptions, format, TestBalanceMode.Level, GetFormOptions(), true);

            return await _repository.GetLevelBalanceAsync(0, parameters);
        }

        private async Task<TestBalanceViewModel> GetLevelBalance_ConfirmedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort(
            TestBalanceFormat format, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseConfirmedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, format, TestBalanceMode.Level, GetFormOptions(), true)
                : GetByNoTestParameters(gridOptions, format, TestBalanceMode.Level, GetFormOptions(), true);

            return await _repository.GetLevelBalanceAsync(0, parameters);
        }

        private async Task<TestBalanceViewModel> GetLevelBalance_ApprovedVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort(
            TestBalanceFormat format, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseApprovedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, format, TestBalanceMode.Level, GetFormOptions(), true)
                : GetByNoTestParameters(gridOptions, format, TestBalanceMode.Level, GetFormOptions(), true);

            return await _repository.GetLevelBalanceAsync(0, parameters);
        }

        private async Task<TestBalanceViewModel> GetLevelBalance_AllVouchers_CurrentBranch_ByBranch_FormOptions_NoFilterSort(
            TestBalanceFormat format, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, format, TestBalanceMode.Level, GetFormOptions(), true)
                : GetByNoTestParameters(gridOptions, format, TestBalanceMode.Level, GetFormOptions(), true);

            return await _repository.GetLevelBalanceAsync(0, parameters);
        }

        #endregion

        #region GetLevelBalance_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort

        private async Task<TestBalanceViewModel> GetLevelBalance_CheckedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort(
            TestBalanceFormat format, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .UseCheckedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, format, TestBalanceMode.Level, GetFormOptions(), true)
                : GetByNoTestParameters(gridOptions, format, TestBalanceMode.Level, GetFormOptions(), true);

            return await _repository.GetLevelBalanceAsync(0, parameters);
        }

        private async Task<TestBalanceViewModel> GetLevelBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort(
            TestBalanceFormat format, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .UseFinalizedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, format, TestBalanceMode.Level, GetFormOptions(), true)
                : GetByNoTestParameters(gridOptions, format, TestBalanceMode.Level, GetFormOptions(), true);

            return await _repository.GetLevelBalanceAsync(0, parameters);
        }

        private async Task<TestBalanceViewModel> GetLevelBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort(
            TestBalanceFormat format, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .UseConfirmedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, format, TestBalanceMode.Level, GetFormOptions(), true)
                : GetByNoTestParameters(gridOptions, format, TestBalanceMode.Level, GetFormOptions(), true);

            return await _repository.GetLevelBalanceAsync(0, parameters);
        }

        private async Task<TestBalanceViewModel> GetLevelBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort(
            TestBalanceFormat format, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .UseApprovedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, format, TestBalanceMode.Level, GetFormOptions(), true)
                : GetByNoTestParameters(gridOptions, format, TestBalanceMode.Level, GetFormOptions(), true);

            return await _repository.GetLevelBalanceAsync(0, parameters);
        }

        private async Task<TestBalanceViewModel> GetLevelBalance_AllVouchers_CurrentAndChildren_ByBranch_FormOptions_NoFilterSort(
            TestBalanceFormat format, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, format, TestBalanceMode.Level, GetFormOptions(), true)
                : GetByNoTestParameters(gridOptions, format, TestBalanceMode.Level, GetFormOptions(), true);

            return await _repository.GetLevelBalanceAsync(0, parameters);
        }

        #endregion

        #region GetLevelBalance_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort

        private async Task<TestBalanceViewModel> GetLevelBalance_CheckedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort(
            TestBalanceFormat format, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseCheckedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, format, TestBalanceMode.Level, FinanceReportOptions.OpeningAsFirstVoucher)
                : GetByNoTestParameters(gridOptions, format, TestBalanceMode.Level, FinanceReportOptions.OpeningAsFirstVoucher);

            return await _repository.GetLevelBalanceAsync(0, parameters);
        }

        private async Task<TestBalanceViewModel> GetLevelBalance_FinalizedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort(
            TestBalanceFormat format, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseFinalizedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, format, TestBalanceMode.Level, FinanceReportOptions.OpeningAsFirstVoucher)
                : GetByNoTestParameters(gridOptions, format, TestBalanceMode.Level, FinanceReportOptions.OpeningAsFirstVoucher);

            return await _repository.GetLevelBalanceAsync(0, parameters);
        }

        private async Task<TestBalanceViewModel> GetLevelBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort(
            TestBalanceFormat format, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseConfirmedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, format, TestBalanceMode.Level, FinanceReportOptions.OpeningAsFirstVoucher)
                : GetByNoTestParameters(gridOptions, format, TestBalanceMode.Level, FinanceReportOptions.OpeningAsFirstVoucher);

            return await _repository.GetLevelBalanceAsync(0, parameters);
        }

        private async Task<TestBalanceViewModel> GetLevelBalance_ApprovedVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort(
            TestBalanceFormat format, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseApprovedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, format, TestBalanceMode.Level, FinanceReportOptions.OpeningAsFirstVoucher)
                : GetByNoTestParameters(gridOptions, format, TestBalanceMode.Level, FinanceReportOptions.OpeningAsFirstVoucher);

            return await _repository.GetLevelBalanceAsync(0, parameters);
        }

        private async Task<TestBalanceViewModel> GetLevelBalance_AllVouchers_CurrentBranch_NoGrouping_OpeningOption_NoFilterSort(
            TestBalanceFormat format, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, format, TestBalanceMode.Level, FinanceReportOptions.OpeningAsFirstVoucher)
                : GetByNoTestParameters(gridOptions, format, TestBalanceMode.Level, FinanceReportOptions.OpeningAsFirstVoucher);

            return await _repository.GetLevelBalanceAsync(0, parameters);
        }

        #endregion

        #region GetLevelBalance_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort

        private async Task<TestBalanceViewModel> GetLevelBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort(
            TestBalanceFormat format, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .UseCheckedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, format, TestBalanceMode.Level, FinanceReportOptions.OpeningAsFirstVoucher)
                : GetByNoTestParameters(gridOptions, format, TestBalanceMode.Level, FinanceReportOptions.OpeningAsFirstVoucher);

            return await _repository.GetLevelBalanceAsync(0, parameters);
        }

        private async Task<TestBalanceViewModel> GetLevelBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort(
            TestBalanceFormat format, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .UseFinalizedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, format, TestBalanceMode.Level, FinanceReportOptions.OpeningAsFirstVoucher)
                : GetByNoTestParameters(gridOptions, format, TestBalanceMode.Level, FinanceReportOptions.OpeningAsFirstVoucher);

            return await _repository.GetLevelBalanceAsync(0, parameters);
        }

        private async Task<TestBalanceViewModel> GetLevelBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort(
            TestBalanceFormat format, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .UseConfirmedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, format, TestBalanceMode.Level, FinanceReportOptions.OpeningAsFirstVoucher)
                : GetByNoTestParameters(gridOptions, format, TestBalanceMode.Level, FinanceReportOptions.OpeningAsFirstVoucher);

            return await _repository.GetLevelBalanceAsync(0, parameters);
        }

        private async Task<TestBalanceViewModel> GetLevelBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort(
            TestBalanceFormat format, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .UseApprovedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, format, TestBalanceMode.Level, FinanceReportOptions.OpeningAsFirstVoucher)
                : GetByNoTestParameters(gridOptions, format, TestBalanceMode.Level, FinanceReportOptions.OpeningAsFirstVoucher);

            return await _repository.GetLevelBalanceAsync(0, parameters);
        }

        private async Task<TestBalanceViewModel> GetLevelBalance_AllVouchers_CurrentAndChildren_NoGrouping_OpeningOption_NoFilterSort(
            TestBalanceFormat format, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, format, TestBalanceMode.Level, FinanceReportOptions.OpeningAsFirstVoucher)
                : GetByNoTestParameters(gridOptions, format, TestBalanceMode.Level, FinanceReportOptions.OpeningAsFirstVoucher);

            return await _repository.GetLevelBalanceAsync(0, parameters);
        }

        #endregion

        #region GetLevelBalance_CurrentBranch_ByBranch_OpeningOption_NoFilterSort

        private async Task<TestBalanceViewModel> GetLevelBalance_CheckedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort(
            TestBalanceFormat format, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseCheckedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, format, TestBalanceMode.Level, FinanceReportOptions.OpeningAsFirstVoucher, true)
                : GetByNoTestParameters(gridOptions, format, TestBalanceMode.Level, FinanceReportOptions.OpeningAsFirstVoucher, true);

            return await _repository.GetLevelBalanceAsync(0, parameters);
        }

        private async Task<TestBalanceViewModel> GetLevelBalance_FinalizedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort(
            TestBalanceFormat format, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseFinalizedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, format, TestBalanceMode.Level, FinanceReportOptions.OpeningAsFirstVoucher, true)
                : GetByNoTestParameters(gridOptions, format, TestBalanceMode.Level, FinanceReportOptions.OpeningAsFirstVoucher, true);

            return await _repository.GetLevelBalanceAsync(0, parameters);
        }

        private async Task<TestBalanceViewModel> GetLevelBalance_ConfirmedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort(
            TestBalanceFormat format, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseConfirmedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, format, TestBalanceMode.Level, FinanceReportOptions.OpeningAsFirstVoucher, true)
                : GetByNoTestParameters(gridOptions, format, TestBalanceMode.Level, FinanceReportOptions.OpeningAsFirstVoucher, true);

            return await _repository.GetLevelBalanceAsync(0, parameters);
        }

        private async Task<TestBalanceViewModel> GetLevelBalance_ApprovedVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort(
            TestBalanceFormat format, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseApprovedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, format, TestBalanceMode.Level, FinanceReportOptions.OpeningAsFirstVoucher, true)
                : GetByNoTestParameters(gridOptions, format, TestBalanceMode.Level, FinanceReportOptions.OpeningAsFirstVoucher, true);

            return await _repository.GetLevelBalanceAsync(0, parameters);
        }

        private async Task<TestBalanceViewModel> GetLevelBalance_AllVouchers_CurrentBranch_ByBranch_OpeningOption_NoFilterSort(
            TestBalanceFormat format, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, format, TestBalanceMode.Level, FinanceReportOptions.OpeningAsFirstVoucher, true)
                : GetByNoTestParameters(gridOptions, format, TestBalanceMode.Level, FinanceReportOptions.OpeningAsFirstVoucher, true);

            return await _repository.GetLevelBalanceAsync(0, parameters);
        }

        #endregion

        #region GetLevelBalance_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort

        private async Task<TestBalanceViewModel> GetLevelBalance_CheckedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort(
            TestBalanceFormat format, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .UseCheckedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, format, TestBalanceMode.Level, FinanceReportOptions.OpeningAsFirstVoucher, true)
                : GetByNoTestParameters(gridOptions, format, TestBalanceMode.Level, FinanceReportOptions.OpeningAsFirstVoucher, true);

            return await _repository.GetLevelBalanceAsync(0, parameters);
        }

        private async Task<TestBalanceViewModel> GetLevelBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort(
            TestBalanceFormat format, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .UseFinalizedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, format, TestBalanceMode.Level, FinanceReportOptions.OpeningAsFirstVoucher, true)
                : GetByNoTestParameters(gridOptions, format, TestBalanceMode.Level, FinanceReportOptions.OpeningAsFirstVoucher, true);

            return await _repository.GetLevelBalanceAsync(0, parameters);
        }

        private async Task<TestBalanceViewModel> GetLevelBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort(
            TestBalanceFormat format, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .UseConfirmedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, format, TestBalanceMode.Level, FinanceReportOptions.OpeningAsFirstVoucher, true)
                : GetByNoTestParameters(gridOptions, format, TestBalanceMode.Level, FinanceReportOptions.OpeningAsFirstVoucher, true);

            return await _repository.GetLevelBalanceAsync(0, parameters);
        }

        private async Task<TestBalanceViewModel> GetLevelBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort(
            TestBalanceFormat format, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .UseApprovedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, format, TestBalanceMode.Level, FinanceReportOptions.OpeningAsFirstVoucher, true)
                : GetByNoTestParameters(gridOptions, format, TestBalanceMode.Level, FinanceReportOptions.OpeningAsFirstVoucher, true);

            return await _repository.GetLevelBalanceAsync(0, parameters);
        }

        private async Task<TestBalanceViewModel> GetLevelBalance_AllVouchers_CurrentAndChildren_ByBranch_OpeningOption_NoFilterSort(
            TestBalanceFormat format, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, format, TestBalanceMode.Level, FinanceReportOptions.OpeningAsFirstVoucher, true)
                : GetByNoTestParameters(gridOptions, format, TestBalanceMode.Level, FinanceReportOptions.OpeningAsFirstVoucher, true);

            return await _repository.GetLevelBalanceAsync(0, parameters);
        }

        #endregion

        #region GetLevelBalance_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort

        private async Task<TestBalanceViewModel> GetLevelBalance_CheckedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort(
            TestBalanceFormat format, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseCheckedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, format, TestBalanceMode.Level, GetFormAndOpeningOptions())
                : GetByNoTestParameters(gridOptions, format, TestBalanceMode.Level, GetFormAndOpeningOptions());

            return await _repository.GetLevelBalanceAsync(0, parameters);
        }

        private async Task<TestBalanceViewModel> GetLevelBalance_FinalizedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort(
            TestBalanceFormat format, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseFinalizedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, format, TestBalanceMode.Level, GetFormAndOpeningOptions())
                : GetByNoTestParameters(gridOptions, format, TestBalanceMode.Level, GetFormAndOpeningOptions());

            return await _repository.GetLevelBalanceAsync(0, parameters);
        }

        private async Task<TestBalanceViewModel> GetLevelBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort(
            TestBalanceFormat format, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseConfirmedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, format, TestBalanceMode.Level, GetFormAndOpeningOptions())
                : GetByNoTestParameters(gridOptions, format, TestBalanceMode.Level, GetFormAndOpeningOptions());

            return await _repository.GetLevelBalanceAsync(0, parameters);
        }

        private async Task<TestBalanceViewModel> GetLevelBalance_ApprovedVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort(
            TestBalanceFormat format, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseApprovedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, format, TestBalanceMode.Level, GetFormAndOpeningOptions())
                : GetByNoTestParameters(gridOptions, format, TestBalanceMode.Level, GetFormAndOpeningOptions());

            return await _repository.GetLevelBalanceAsync(0, parameters);
        }

        private async Task<TestBalanceViewModel> GetLevelBalance_AllVouchers_CurrentBranch_NoGrouping_FormAndOpeningOption_NoFilterSort(
            TestBalanceFormat format, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, format, TestBalanceMode.Level, GetFormAndOpeningOptions())
                : GetByNoTestParameters(gridOptions, format, TestBalanceMode.Level, GetFormAndOpeningOptions());

            return await _repository.GetLevelBalanceAsync(0, parameters);
        }

        #endregion

        #region GetLevelBalance_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort

        private async Task<TestBalanceViewModel> GetLevelBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort(
            TestBalanceFormat format, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .UseCheckedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, format, TestBalanceMode.Level, GetFormAndOpeningOptions())
                : GetByNoTestParameters(gridOptions, format, TestBalanceMode.Level, GetFormAndOpeningOptions());

            return await _repository.GetLevelBalanceAsync(0, parameters);
        }

        private async Task<TestBalanceViewModel> GetLevelBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort(
            TestBalanceFormat format, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .UseFinalizedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, format, TestBalanceMode.Level, GetFormAndOpeningOptions())
                : GetByNoTestParameters(gridOptions, format, TestBalanceMode.Level, GetFormAndOpeningOptions());

            return await _repository.GetLevelBalanceAsync(0, parameters);
        }

        private async Task<TestBalanceViewModel> GetLevelBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort(
            TestBalanceFormat format, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .UseConfirmedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, format, TestBalanceMode.Level, GetFormAndOpeningOptions())
                : GetByNoTestParameters(gridOptions, format, TestBalanceMode.Level, GetFormAndOpeningOptions());

            return await _repository.GetLevelBalanceAsync(0, parameters);
        }

        private async Task<TestBalanceViewModel> GetLevelBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort(
            TestBalanceFormat format, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .UseApprovedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, format, TestBalanceMode.Level, GetFormAndOpeningOptions())
                : GetByNoTestParameters(gridOptions, format, TestBalanceMode.Level, GetFormAndOpeningOptions());

            return await _repository.GetLevelBalanceAsync(0, parameters);
        }

        private async Task<TestBalanceViewModel> GetLevelBalance_AllVouchers_CurrentAndChildren_NoGrouping_FormAndOpeningOption_NoFilterSort(
            TestBalanceFormat format, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, format, TestBalanceMode.Level, GetFormAndOpeningOptions())
                : GetByNoTestParameters(gridOptions, format, TestBalanceMode.Level, GetFormAndOpeningOptions());

            return await _repository.GetLevelBalanceAsync(0, parameters);
        }

        #endregion

        #region GetLevelBalance_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort

        private async Task<TestBalanceViewModel> GetLevelBalance_CheckedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort(
            TestBalanceFormat format, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseCheckedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, format, TestBalanceMode.Level, GetFormAndOpeningOptions(), true)
                : GetByNoTestParameters(gridOptions, format, TestBalanceMode.Level, GetFormAndOpeningOptions(), true);

            return await _repository.GetLevelBalanceAsync(0, parameters);
        }

        private async Task<TestBalanceViewModel> GetLevelBalance_FinalizedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort(
            TestBalanceFormat format, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseFinalizedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, format, TestBalanceMode.Level, GetFormAndOpeningOptions(), true)
                : GetByNoTestParameters(gridOptions, format, TestBalanceMode.Level, GetFormAndOpeningOptions(), true);

            return await _repository.GetLevelBalanceAsync(0, parameters);
        }

        private async Task<TestBalanceViewModel> GetLevelBalance_ConfirmedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort(
            TestBalanceFormat format, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseConfirmedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, format, TestBalanceMode.Level, GetFormAndOpeningOptions(), true)
                : GetByNoTestParameters(gridOptions, format, TestBalanceMode.Level, GetFormAndOpeningOptions(), true);

            return await _repository.GetLevelBalanceAsync(0, parameters);
        }

        private async Task<TestBalanceViewModel> GetLevelBalance_ApprovedVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort(
            TestBalanceFormat format, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseApprovedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, format, TestBalanceMode.Level, GetFormAndOpeningOptions(), true)
                : GetByNoTestParameters(gridOptions, format, TestBalanceMode.Level, GetFormAndOpeningOptions(), true);

            return await _repository.GetLevelBalanceAsync(0, parameters);
        }

        private async Task<TestBalanceViewModel> GetLevelBalance_AllVouchers_CurrentBranch_ByBranch_FormAndOpeningOption_NoFilterSort(
            TestBalanceFormat format, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, format, TestBalanceMode.Level, GetFormAndOpeningOptions(), true)
                : GetByNoTestParameters(gridOptions, format, TestBalanceMode.Level, GetFormAndOpeningOptions(), true);

            return await _repository.GetLevelBalanceAsync(0, parameters);
        }

        #endregion

        #region GetLevelBalance_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort

        private async Task<TestBalanceViewModel> GetLevelBalance_CheckedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort(
            TestBalanceFormat format, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .UseCheckedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, format, TestBalanceMode.Level, GetFormAndOpeningOptions(), true)
                : GetByNoTestParameters(gridOptions, format, TestBalanceMode.Level, GetFormAndOpeningOptions(), true);

            return await _repository.GetLevelBalanceAsync(0, parameters);
        }

        private async Task<TestBalanceViewModel> GetLevelBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort(
            TestBalanceFormat format, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .UseFinalizedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, format, TestBalanceMode.Level, GetFormAndOpeningOptions(), true)
                : GetByNoTestParameters(gridOptions, format, TestBalanceMode.Level, GetFormAndOpeningOptions(), true);

            return await _repository.GetLevelBalanceAsync(0, parameters);
        }

        private async Task<TestBalanceViewModel> GetLevelBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort(
            TestBalanceFormat format, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .UseConfirmedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, format, TestBalanceMode.Level, GetFormAndOpeningOptions(), true)
                : GetByNoTestParameters(gridOptions, format, TestBalanceMode.Level, GetFormAndOpeningOptions(), true);

            return await _repository.GetLevelBalanceAsync(0, parameters);
        }

        private async Task<TestBalanceViewModel> GetLevelBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort(
            TestBalanceFormat format, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .UseApprovedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, format, TestBalanceMode.Level, GetFormAndOpeningOptions(), true)
                : GetByNoTestParameters(gridOptions, format, TestBalanceMode.Level, GetFormAndOpeningOptions(), true);

            return await _repository.GetLevelBalanceAsync(0, parameters);
        }

        private async Task<TestBalanceViewModel> GetLevelBalance_AllVouchers_CurrentAndChildren_ByBranch_FormAndOpeningOption_NoFilterSort(
            TestBalanceFormat format, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, format, TestBalanceMode.Level, GetFormAndOpeningOptions(), true)
                : GetByNoTestParameters(gridOptions, format, TestBalanceMode.Level, GetFormAndOpeningOptions(), true);

            return await _repository.GetLevelBalanceAsync(0, parameters);
        }

        #endregion

        #region GetLevelBalance_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort

        private async Task<TestBalanceViewModel> GetLevelBalance_CheckedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort(
            TestBalanceFormat format, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseCheckedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, format, TestBalanceMode.Level, FinanceReportOptions.StartTurnoverAsInitBalance)
                : GetByNoTestParameters(gridOptions, format, TestBalanceMode.Level, FinanceReportOptions.StartTurnoverAsInitBalance);

            return await _repository.GetLevelBalanceAsync(0, parameters);
        }

        private async Task<TestBalanceViewModel> GetLevelBalance_FinalizedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort(
            TestBalanceFormat format, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseFinalizedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, format, TestBalanceMode.Level, FinanceReportOptions.StartTurnoverAsInitBalance)
                : GetByNoTestParameters(gridOptions, format, TestBalanceMode.Level, FinanceReportOptions.StartTurnoverAsInitBalance);

            return await _repository.GetLevelBalanceAsync(0, parameters);
        }

        private async Task<TestBalanceViewModel> GetLevelBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort(
            TestBalanceFormat format, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseConfirmedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, format, TestBalanceMode.Level, FinanceReportOptions.StartTurnoverAsInitBalance)
                : GetByNoTestParameters(gridOptions, format, TestBalanceMode.Level, FinanceReportOptions.StartTurnoverAsInitBalance);

            return await _repository.GetLevelBalanceAsync(0, parameters);
        }

        private async Task<TestBalanceViewModel> GetLevelBalance_ApprovedVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort(
            TestBalanceFormat format, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseApprovedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, format, TestBalanceMode.Level, FinanceReportOptions.StartTurnoverAsInitBalance)
                : GetByNoTestParameters(gridOptions, format, TestBalanceMode.Level, FinanceReportOptions.StartTurnoverAsInitBalance);

            return await _repository.GetLevelBalanceAsync(0, parameters);
        }

        private async Task<TestBalanceViewModel> GetLevelBalance_AllVouchers_CurrentBranch_NoGrouping_TurnoverOption_NoFilterSort(
            TestBalanceFormat format, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, format, TestBalanceMode.Level, FinanceReportOptions.StartTurnoverAsInitBalance)
                : GetByNoTestParameters(gridOptions, format, TestBalanceMode.Level, FinanceReportOptions.StartTurnoverAsInitBalance);

            return await _repository.GetLevelBalanceAsync(0, parameters);
        }

        #endregion

        #region GetLevelBalance_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort

        private async Task<TestBalanceViewModel> GetLevelBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort(
            TestBalanceFormat format, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .UseCheckedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, format, TestBalanceMode.Level, FinanceReportOptions.StartTurnoverAsInitBalance)
                : GetByNoTestParameters(gridOptions, format, TestBalanceMode.Level, FinanceReportOptions.StartTurnoverAsInitBalance);

            return await _repository.GetLevelBalanceAsync(0, parameters);
        }

        private async Task<TestBalanceViewModel> GetLevelBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort(
            TestBalanceFormat format, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .UseFinalizedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, format, TestBalanceMode.Level, FinanceReportOptions.StartTurnoverAsInitBalance)
                : GetByNoTestParameters(gridOptions, format, TestBalanceMode.Level, FinanceReportOptions.StartTurnoverAsInitBalance);

            return await _repository.GetLevelBalanceAsync(0, parameters);
        }

        private async Task<TestBalanceViewModel> GetLevelBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort(
            TestBalanceFormat format, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .UseConfirmedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, format, TestBalanceMode.Level, FinanceReportOptions.StartTurnoverAsInitBalance)
                : GetByNoTestParameters(gridOptions, format, TestBalanceMode.Level, FinanceReportOptions.StartTurnoverAsInitBalance);

            return await _repository.GetLevelBalanceAsync(0, parameters);
        }

        private async Task<TestBalanceViewModel> GetLevelBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort(
            TestBalanceFormat format, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .UseApprovedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, format, TestBalanceMode.Level, FinanceReportOptions.StartTurnoverAsInitBalance)
                : GetByNoTestParameters(gridOptions, format, TestBalanceMode.Level, FinanceReportOptions.StartTurnoverAsInitBalance);

            return await _repository.GetLevelBalanceAsync(0, parameters);
        }

        private async Task<TestBalanceViewModel> GetLevelBalance_AllVouchers_CurrentAndChildren_NoGrouping_TurnoverOption_NoFilterSort(
            TestBalanceFormat format, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, format, TestBalanceMode.Level, FinanceReportOptions.StartTurnoverAsInitBalance)
                : GetByNoTestParameters(gridOptions, format, TestBalanceMode.Level, FinanceReportOptions.StartTurnoverAsInitBalance);

            return await _repository.GetLevelBalanceAsync(0, parameters);
        }

        #endregion

        #region GetLevelBalance_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort

        private async Task<TestBalanceViewModel> GetLevelBalance_CheckedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort(
            TestBalanceFormat format, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseCheckedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, format, TestBalanceMode.Level, FinanceReportOptions.StartTurnoverAsInitBalance, true)
                : GetByNoTestParameters(gridOptions, format, TestBalanceMode.Level, FinanceReportOptions.StartTurnoverAsInitBalance, true);

            return await _repository.GetLevelBalanceAsync(0, parameters);
        }

        private async Task<TestBalanceViewModel> GetLevelBalance_FinalizedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort(
            TestBalanceFormat format, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseFinalizedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, format, TestBalanceMode.Level, FinanceReportOptions.StartTurnoverAsInitBalance, true)
                : GetByNoTestParameters(gridOptions, format, TestBalanceMode.Level, FinanceReportOptions.StartTurnoverAsInitBalance, true);

            return await _repository.GetLevelBalanceAsync(0, parameters);
        }

        private async Task<TestBalanceViewModel> GetLevelBalance_ConfirmedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort(
            TestBalanceFormat format, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseConfirmedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, format, TestBalanceMode.Level, FinanceReportOptions.StartTurnoverAsInitBalance, true)
                : GetByNoTestParameters(gridOptions, format, TestBalanceMode.Level, FinanceReportOptions.StartTurnoverAsInitBalance, true);

            return await _repository.GetLevelBalanceAsync(0, parameters);
        }

        private async Task<TestBalanceViewModel> GetLevelBalance_ApprovedVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort(
            TestBalanceFormat format, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseApprovedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, format, TestBalanceMode.Level, FinanceReportOptions.StartTurnoverAsInitBalance, true)
                : GetByNoTestParameters(gridOptions, format, TestBalanceMode.Level, FinanceReportOptions.StartTurnoverAsInitBalance, true);

            return await _repository.GetLevelBalanceAsync(0, parameters);
        }

        private async Task<TestBalanceViewModel> GetLevelBalance_AllVouchers_CurrentBranch_ByBranch_TurnoverOption_NoFilterSort(
            TestBalanceFormat format, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, format, TestBalanceMode.Level, FinanceReportOptions.StartTurnoverAsInitBalance, true)
                : GetByNoTestParameters(gridOptions, format, TestBalanceMode.Level, FinanceReportOptions.StartTurnoverAsInitBalance, true);

            return await _repository.GetLevelBalanceAsync(0, parameters);
        }

        #endregion

        #region GetLevelBalance_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort

        private async Task<TestBalanceViewModel> GetLevelBalance_CheckedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort(
            TestBalanceFormat format, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .UseCheckedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, format, TestBalanceMode.Level, FinanceReportOptions.StartTurnoverAsInitBalance, true)
                : GetByNoTestParameters(gridOptions, format, TestBalanceMode.Level, FinanceReportOptions.StartTurnoverAsInitBalance, true);

            return await _repository.GetLevelBalanceAsync(0, parameters);
        }

        private async Task<TestBalanceViewModel> GetLevelBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort(
            TestBalanceFormat format, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .UseFinalizedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, format, TestBalanceMode.Level, FinanceReportOptions.StartTurnoverAsInitBalance, true)
                : GetByNoTestParameters(gridOptions, format, TestBalanceMode.Level, FinanceReportOptions.StartTurnoverAsInitBalance, true);

            return await _repository.GetLevelBalanceAsync(0, parameters);
        }

        private async Task<TestBalanceViewModel> GetLevelBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort(
            TestBalanceFormat format, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .UseConfirmedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, format, TestBalanceMode.Level, FinanceReportOptions.StartTurnoverAsInitBalance, true)
                : GetByNoTestParameters(gridOptions, format, TestBalanceMode.Level, FinanceReportOptions.StartTurnoverAsInitBalance, true);

            return await _repository.GetLevelBalanceAsync(0, parameters);
        }

        private async Task<TestBalanceViewModel> GetLevelBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort(
            TestBalanceFormat format, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .UseApprovedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, format, TestBalanceMode.Level, FinanceReportOptions.StartTurnoverAsInitBalance, true)
                : GetByNoTestParameters(gridOptions, format, TestBalanceMode.Level, FinanceReportOptions.StartTurnoverAsInitBalance, true);

            return await _repository.GetLevelBalanceAsync(0, parameters);
        }

        private async Task<TestBalanceViewModel> GetLevelBalance_AllVouchers_CurrentAndChildren_ByBranch_TurnoverOption_NoFilterSort(
            TestBalanceFormat format, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, format, TestBalanceMode.Level, FinanceReportOptions.StartTurnoverAsInitBalance, true)
                : GetByNoTestParameters(gridOptions, format, TestBalanceMode.Level, FinanceReportOptions.StartTurnoverAsInitBalance, true);

            return await _repository.GetLevelBalanceAsync(0, parameters);
        }

        #endregion

        #region GetLevelBalance_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort

        private async Task<TestBalanceViewModel> GetLevelBalance_CheckedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort(
            TestBalanceFormat format, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseCheckedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, format, TestBalanceMode.Level, GetFormAndTurnoverOptions())
                : GetByNoTestParameters(gridOptions, format, TestBalanceMode.Level, GetFormAndTurnoverOptions());

            return await _repository.GetLevelBalanceAsync(0, parameters);
        }

        private async Task<TestBalanceViewModel> GetLevelBalance_FinalizedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort(
            TestBalanceFormat format, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseFinalizedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, format, TestBalanceMode.Level, GetFormAndTurnoverOptions())
                : GetByNoTestParameters(gridOptions, format, TestBalanceMode.Level, GetFormAndTurnoverOptions());

            return await _repository.GetLevelBalanceAsync(0, parameters);
        }

        private async Task<TestBalanceViewModel> GetLevelBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort(
            TestBalanceFormat format, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseConfirmedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, format, TestBalanceMode.Level, GetFormAndTurnoverOptions())
                : GetByNoTestParameters(gridOptions, format, TestBalanceMode.Level, GetFormAndTurnoverOptions());

            return await _repository.GetLevelBalanceAsync(0, parameters);
        }

        private async Task<TestBalanceViewModel> GetLevelBalance_ApprovedVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort(
            TestBalanceFormat format, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseApprovedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, format, TestBalanceMode.Level, GetFormAndTurnoverOptions())
                : GetByNoTestParameters(gridOptions, format, TestBalanceMode.Level, GetFormAndTurnoverOptions());

            return await _repository.GetLevelBalanceAsync(0, parameters);
        }

        private async Task<TestBalanceViewModel> GetLevelBalance_AllVouchers_CurrentBranch_NoGrouping_FormAndTurnoverOption_NoFilterSort(
            TestBalanceFormat format, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, format, TestBalanceMode.Level, GetFormAndTurnoverOptions())
                : GetByNoTestParameters(gridOptions, format, TestBalanceMode.Level, GetFormAndTurnoverOptions());

            return await _repository.GetLevelBalanceAsync(0, parameters);
        }

        #endregion

        #region GetLevelBalance_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort

        private async Task<TestBalanceViewModel> GetLevelBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort(
            TestBalanceFormat format, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .UseCheckedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, format, TestBalanceMode.Level, GetFormAndTurnoverOptions())
                : GetByNoTestParameters(gridOptions, format, TestBalanceMode.Level, GetFormAndTurnoverOptions());

            return await _repository.GetLevelBalanceAsync(0, parameters);
        }

        private async Task<TestBalanceViewModel> GetLevelBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort(
            TestBalanceFormat format, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .UseFinalizedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, format, TestBalanceMode.Level, GetFormAndTurnoverOptions())
                : GetByNoTestParameters(gridOptions, format, TestBalanceMode.Level, GetFormAndTurnoverOptions());

            return await _repository.GetLevelBalanceAsync(0, parameters);
        }

        private async Task<TestBalanceViewModel> GetLevelBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort(
            TestBalanceFormat format, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .UseConfirmedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, format, TestBalanceMode.Level, GetFormAndTurnoverOptions())
                : GetByNoTestParameters(gridOptions, format, TestBalanceMode.Level, GetFormAndTurnoverOptions());

            return await _repository.GetLevelBalanceAsync(0, parameters);
        }

        private async Task<TestBalanceViewModel> GetLevelBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort(
            TestBalanceFormat format, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .UseApprovedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, format, TestBalanceMode.Level, GetFormAndTurnoverOptions())
                : GetByNoTestParameters(gridOptions, format, TestBalanceMode.Level, GetFormAndTurnoverOptions());

            return await _repository.GetLevelBalanceAsync(0, parameters);
        }

        private async Task<TestBalanceViewModel> GetLevelBalance_AllVouchers_CurrentAndChildren_NoGrouping_FormAndTurnoverOption_NoFilterSort(
            TestBalanceFormat format, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, format, TestBalanceMode.Level, GetFormAndTurnoverOptions())
                : GetByNoTestParameters(gridOptions, format, TestBalanceMode.Level, GetFormAndTurnoverOptions());

            return await _repository.GetLevelBalanceAsync(0, parameters);
        }

        #endregion

        #region GetLevelBalance_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort

        private async Task<TestBalanceViewModel> GetLevelBalance_CheckedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort(
            TestBalanceFormat format, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseCheckedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, format, TestBalanceMode.Level, GetFormAndTurnoverOptions(), true)
                : GetByNoTestParameters(gridOptions, format, TestBalanceMode.Level, GetFormAndTurnoverOptions(), true);

            return await _repository.GetLevelBalanceAsync(0, parameters);
        }

        private async Task<TestBalanceViewModel> GetLevelBalance_FinalizedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort(
            TestBalanceFormat format, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseFinalizedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, format, TestBalanceMode.Level, GetFormAndTurnoverOptions(), true)
                : GetByNoTestParameters(gridOptions, format, TestBalanceMode.Level, GetFormAndTurnoverOptions(), true);

            return await _repository.GetLevelBalanceAsync(0, parameters);
        }

        private async Task<TestBalanceViewModel> GetLevelBalance_ConfirmedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort(
            TestBalanceFormat format, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseConfirmedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, format, TestBalanceMode.Level, GetFormAndTurnoverOptions(), true)
                : GetByNoTestParameters(gridOptions, format, TestBalanceMode.Level, GetFormAndTurnoverOptions(), true);

            return await _repository.GetLevelBalanceAsync(0, parameters);
        }

        private async Task<TestBalanceViewModel> GetLevelBalance_ApprovedVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort(
            TestBalanceFormat format, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseApprovedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, format, TestBalanceMode.Level, GetFormAndTurnoverOptions(), true)
                : GetByNoTestParameters(gridOptions, format, TestBalanceMode.Level, GetFormAndTurnoverOptions(), true);

            return await _repository.GetLevelBalanceAsync(0, parameters);
        }

        private async Task<TestBalanceViewModel> GetLevelBalance_AllVouchers_CurrentBranch_ByBranch_FormAndTurnoverOption_NoFilterSort(
            TestBalanceFormat format, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, format, TestBalanceMode.Level, GetFormAndTurnoverOptions(), true)
                : GetByNoTestParameters(gridOptions, format, TestBalanceMode.Level, GetFormAndTurnoverOptions(), true);

            return await _repository.GetLevelBalanceAsync(0, parameters);
        }

        #endregion

        #region GetLevelBalance_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort

        private async Task<TestBalanceViewModel> GetLevelBalance_CheckedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort(
            TestBalanceFormat format, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .UseCheckedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, format, TestBalanceMode.Level, GetFormAndTurnoverOptions(), true)
                : GetByNoTestParameters(gridOptions, format, TestBalanceMode.Level, GetFormAndTurnoverOptions(), true);

            return await _repository.GetLevelBalanceAsync(0, parameters);
        }

        private async Task<TestBalanceViewModel> GetLevelBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort(
            TestBalanceFormat format, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .UseFinalizedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, format, TestBalanceMode.Level, GetFormAndTurnoverOptions(), true)
                : GetByNoTestParameters(gridOptions, format, TestBalanceMode.Level, GetFormAndTurnoverOptions(), true);

            return await _repository.GetLevelBalanceAsync(0, parameters);
        }

        private async Task<TestBalanceViewModel> GetLevelBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort(
            TestBalanceFormat format, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .UseConfirmedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, format, TestBalanceMode.Level, GetFormAndTurnoverOptions(), true)
                : GetByNoTestParameters(gridOptions, format, TestBalanceMode.Level, GetFormAndTurnoverOptions(), true);

            return await _repository.GetLevelBalanceAsync(0, parameters);
        }

        private async Task<TestBalanceViewModel> GetLevelBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort(
            TestBalanceFormat format, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .UseApprovedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, format, TestBalanceMode.Level, GetFormAndTurnoverOptions(), true)
                : GetByNoTestParameters(gridOptions, format, TestBalanceMode.Level, GetFormAndTurnoverOptions(), true);

            return await _repository.GetLevelBalanceAsync(0, parameters);
        }

        private async Task<TestBalanceViewModel> GetLevelBalance_AllVouchers_CurrentAndChildren_ByBranch_FormAndTurnoverOption_NoFilterSort(
            TestBalanceFormat format, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, format, TestBalanceMode.Level, GetFormAndTurnoverOptions(), true)
                : GetByNoTestParameters(gridOptions, format, TestBalanceMode.Level, GetFormAndTurnoverOptions(), true);

            return await _repository.GetLevelBalanceAsync(0, parameters);
        }

        #endregion

        #region GetLevelBalance_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort

        private async Task<TestBalanceViewModel> GetLevelBalance_CheckedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(
            TestBalanceFormat format, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseCheckedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, format, TestBalanceMode.Level, GetOpeningAndTurnoverOptions())
                : GetByNoTestParameters(gridOptions, format, TestBalanceMode.Level, GetOpeningAndTurnoverOptions());

            return await _repository.GetLevelBalanceAsync(0, parameters);
        }

        private async Task<TestBalanceViewModel> GetLevelBalance_FinalizedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(
            TestBalanceFormat format, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseFinalizedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, format, TestBalanceMode.Level, GetOpeningAndTurnoverOptions())
                : GetByNoTestParameters(gridOptions, format, TestBalanceMode.Level, GetOpeningAndTurnoverOptions());

            return await _repository.GetLevelBalanceAsync(0, parameters);
        }

        private async Task<TestBalanceViewModel> GetLevelBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(
            TestBalanceFormat format, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseConfirmedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, format, TestBalanceMode.Level, GetOpeningAndTurnoverOptions())
                : GetByNoTestParameters(gridOptions, format, TestBalanceMode.Level, GetOpeningAndTurnoverOptions());

            return await _repository.GetLevelBalanceAsync(0, parameters);
        }

        private async Task<TestBalanceViewModel> GetLevelBalance_ApprovedVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(
            TestBalanceFormat format, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseApprovedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, format, TestBalanceMode.Level, GetOpeningAndTurnoverOptions())
                : GetByNoTestParameters(gridOptions, format, TestBalanceMode.Level, GetOpeningAndTurnoverOptions());

            return await _repository.GetLevelBalanceAsync(0, parameters);
        }

        private async Task<TestBalanceViewModel> GetLevelBalance_AllVouchers_CurrentBranch_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(
            TestBalanceFormat format, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, format, TestBalanceMode.Level, GetOpeningAndTurnoverOptions())
                : GetByNoTestParameters(gridOptions, format, TestBalanceMode.Level, GetOpeningAndTurnoverOptions());

            return await _repository.GetLevelBalanceAsync(0, parameters);
        }

        #endregion

        #region GetLevelBalance_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort

        private async Task<TestBalanceViewModel> GetLevelBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(
            TestBalanceFormat format, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .UseCheckedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, format, TestBalanceMode.Level, GetOpeningAndTurnoverOptions())
                : GetByNoTestParameters(gridOptions, format, TestBalanceMode.Level, GetOpeningAndTurnoverOptions());

            return await _repository.GetLevelBalanceAsync(0, parameters);
        }

        private async Task<TestBalanceViewModel> GetLevelBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(
            TestBalanceFormat format, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .UseFinalizedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, format, TestBalanceMode.Level, GetOpeningAndTurnoverOptions())
                : GetByNoTestParameters(gridOptions, format, TestBalanceMode.Level, GetOpeningAndTurnoverOptions());

            return await _repository.GetLevelBalanceAsync(0, parameters);
        }

        private async Task<TestBalanceViewModel> GetLevelBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(
            TestBalanceFormat format, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .UseConfirmedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, format, TestBalanceMode.Level, GetOpeningAndTurnoverOptions())
                : GetByNoTestParameters(gridOptions, format, TestBalanceMode.Level, GetOpeningAndTurnoverOptions());

            return await _repository.GetLevelBalanceAsync(0, parameters);
        }

        private async Task<TestBalanceViewModel> GetLevelBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(
            TestBalanceFormat format, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .UseApprovedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, format, TestBalanceMode.Level, GetOpeningAndTurnoverOptions())
                : GetByNoTestParameters(gridOptions, format, TestBalanceMode.Level, GetOpeningAndTurnoverOptions());

            return await _repository.GetLevelBalanceAsync(0, parameters);
        }

        private async Task<TestBalanceViewModel> GetLevelBalance_AllVouchers_CurrentAndChildren_NoGrouping_OpeningAndTurnoverOption_NoFilterSort(
            TestBalanceFormat format, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, format, TestBalanceMode.Level, GetOpeningAndTurnoverOptions())
                : GetByNoTestParameters(gridOptions, format, TestBalanceMode.Level, GetOpeningAndTurnoverOptions());

            return await _repository.GetLevelBalanceAsync(0, parameters);
        }

        #endregion

        #region GetLevelBalance_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort

        private async Task<TestBalanceViewModel> GetLevelBalance_CheckedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort(
            TestBalanceFormat format, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseCheckedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, format, TestBalanceMode.Level, GetOpeningAndTurnoverOptions(), true)
                : GetByNoTestParameters(gridOptions, format, TestBalanceMode.Level, GetOpeningAndTurnoverOptions(), true);

            return await _repository.GetLevelBalanceAsync(0, parameters);
        }

        private async Task<TestBalanceViewModel> GetLevelBalance_FinalizedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort(
            TestBalanceFormat format, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseFinalizedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, format, TestBalanceMode.Level, GetOpeningAndTurnoverOptions(), true)
                : GetByNoTestParameters(gridOptions, format, TestBalanceMode.Level, GetOpeningAndTurnoverOptions(), true);

            return await _repository.GetLevelBalanceAsync(0, parameters);
        }

        private async Task<TestBalanceViewModel> GetLevelBalance_ConfirmedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort(
            TestBalanceFormat format, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseConfirmedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, format, TestBalanceMode.Level, GetOpeningAndTurnoverOptions(), true)
                : GetByNoTestParameters(gridOptions, format, TestBalanceMode.Level, GetOpeningAndTurnoverOptions(), true);

            return await _repository.GetLevelBalanceAsync(0, parameters);
        }

        private async Task<TestBalanceViewModel> GetLevelBalance_ApprovedVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort(
            TestBalanceFormat format, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseApprovedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, format, TestBalanceMode.Level, GetOpeningAndTurnoverOptions(), true)
                : GetByNoTestParameters(gridOptions, format, TestBalanceMode.Level, GetOpeningAndTurnoverOptions(), true);

            return await _repository.GetLevelBalanceAsync(0, parameters);
        }

        private async Task<TestBalanceViewModel> GetLevelBalance_AllVouchers_CurrentBranch_ByBranch_OpeningAndTurnoverOption_NoFilterSort(
            TestBalanceFormat format, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, format, TestBalanceMode.Level, GetOpeningAndTurnoverOptions(), true)
                : GetByNoTestParameters(gridOptions, format, TestBalanceMode.Level, GetOpeningAndTurnoverOptions(), true);

            return await _repository.GetLevelBalanceAsync(0, parameters);
        }

        #endregion

        #region GetLevelBalance_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort

        private async Task<TestBalanceViewModel> GetLevelBalance_CheckedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort(
            TestBalanceFormat format, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .UseCheckedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, format, TestBalanceMode.Level, GetOpeningAndTurnoverOptions(), true)
                : GetByNoTestParameters(gridOptions, format, TestBalanceMode.Level, GetOpeningAndTurnoverOptions(), true);

            return await _repository.GetLevelBalanceAsync(0, parameters);
        }

        private async Task<TestBalanceViewModel> GetLevelBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort(
            TestBalanceFormat format, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .UseFinalizedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, format, TestBalanceMode.Level, GetOpeningAndTurnoverOptions(), true)
                : GetByNoTestParameters(gridOptions, format, TestBalanceMode.Level, GetOpeningAndTurnoverOptions(), true);

            return await _repository.GetLevelBalanceAsync(0, parameters);
        }

        private async Task<TestBalanceViewModel> GetLevelBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort(
            TestBalanceFormat format, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .UseConfirmedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, format, TestBalanceMode.Level, GetOpeningAndTurnoverOptions(), true)
                : GetByNoTestParameters(gridOptions, format, TestBalanceMode.Level, GetOpeningAndTurnoverOptions(), true);

            return await _repository.GetLevelBalanceAsync(0, parameters);
        }

        private async Task<TestBalanceViewModel> GetLevelBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort(
            TestBalanceFormat format, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .UseApprovedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, format, TestBalanceMode.Level, GetOpeningAndTurnoverOptions(), true)
                : GetByNoTestParameters(gridOptions, format, TestBalanceMode.Level, GetOpeningAndTurnoverOptions(), true);

            return await _repository.GetLevelBalanceAsync(0, parameters);
        }

        private async Task<TestBalanceViewModel> GetLevelBalance_AllVouchers_CurrentAndChildren_ByBranch_OpeningAndTurnoverOption_NoFilterSort(
            TestBalanceFormat format, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, format, TestBalanceMode.Level, GetOpeningAndTurnoverOptions(), true)
                : GetByNoTestParameters(gridOptions, format, TestBalanceMode.Level, GetOpeningAndTurnoverOptions(), true);

            return await _repository.GetLevelBalanceAsync(0, parameters);
        }

        #endregion

        #region GetLevelBalance_CurrentBranch_NoGrouping_AllOptions_NoFilterSort

        private async Task<TestBalanceViewModel> GetLevelBalance_CheckedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort(
            TestBalanceFormat format, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseCheckedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, format, TestBalanceMode.Level, FinanceReportOptions.All)
                : GetByNoTestParameters(gridOptions, format, TestBalanceMode.Level, FinanceReportOptions.All);

            return await _repository.GetLevelBalanceAsync(0, parameters);
        }

        private async Task<TestBalanceViewModel> GetLevelBalance_FinalizedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort(
            TestBalanceFormat format, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseFinalizedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, format, TestBalanceMode.Level, FinanceReportOptions.All)
                : GetByNoTestParameters(gridOptions, format, TestBalanceMode.Level, FinanceReportOptions.All);

            return await _repository.GetLevelBalanceAsync(0, parameters);
        }

        private async Task<TestBalanceViewModel> GetLevelBalance_ConfirmedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort(
            TestBalanceFormat format, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseConfirmedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, format, TestBalanceMode.Level, FinanceReportOptions.All)
                : GetByNoTestParameters(gridOptions, format, TestBalanceMode.Level, FinanceReportOptions.All);

            return await _repository.GetLevelBalanceAsync(0, parameters);
        }

        private async Task<TestBalanceViewModel> GetLevelBalance_ApprovedVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort(
            TestBalanceFormat format, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseApprovedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, format, TestBalanceMode.Level, FinanceReportOptions.All)
                : GetByNoTestParameters(gridOptions, format, TestBalanceMode.Level, FinanceReportOptions.All);

            return await _repository.GetLevelBalanceAsync(0, parameters);
        }

        private async Task<TestBalanceViewModel> GetLevelBalance_AllVouchers_CurrentBranch_NoGrouping_AllOptions_NoFilterSort(
            TestBalanceFormat format, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, format, TestBalanceMode.Level, FinanceReportOptions.All)
                : GetByNoTestParameters(gridOptions, format, TestBalanceMode.Level, FinanceReportOptions.All);

            return await _repository.GetLevelBalanceAsync(0, parameters);
        }

        #endregion

        #region GetLevelBalance_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort

        private async Task<TestBalanceViewModel> GetLevelBalance_CheckedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort(
            TestBalanceFormat format, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .UseCheckedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, format, TestBalanceMode.Level, FinanceReportOptions.All)
                : GetByNoTestParameters(gridOptions, format, TestBalanceMode.Level, FinanceReportOptions.All);

            return await _repository.GetLevelBalanceAsync(0, parameters);
        }

        private async Task<TestBalanceViewModel> GetLevelBalance_FinalizedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort(
            TestBalanceFormat format, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .UseFinalizedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, format, TestBalanceMode.Level, FinanceReportOptions.All)
                : GetByNoTestParameters(gridOptions, format, TestBalanceMode.Level, FinanceReportOptions.All);

            return await _repository.GetLevelBalanceAsync(0, parameters);
        }

        private async Task<TestBalanceViewModel> GetLevelBalance_ConfirmedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort(
            TestBalanceFormat format, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .UseConfirmedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, format, TestBalanceMode.Level, FinanceReportOptions.All)
                : GetByNoTestParameters(gridOptions, format, TestBalanceMode.Level, FinanceReportOptions.All);

            return await _repository.GetLevelBalanceAsync(0, parameters);
        }

        private async Task<TestBalanceViewModel> GetLevelBalance_ApprovedVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort(
            TestBalanceFormat format, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .UseApprovedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, format, TestBalanceMode.Level, FinanceReportOptions.All)
                : GetByNoTestParameters(gridOptions, format, TestBalanceMode.Level, FinanceReportOptions.All);

            return await _repository.GetLevelBalanceAsync(0, parameters);
        }

        private async Task<TestBalanceViewModel> GetLevelBalance_AllVouchers_CurrentAndChildren_NoGrouping_AllOptions_NoFilterSort(
            TestBalanceFormat format, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, format, TestBalanceMode.Level, FinanceReportOptions.All)
                : GetByNoTestParameters(gridOptions, format, TestBalanceMode.Level, FinanceReportOptions.All);

            return await _repository.GetLevelBalanceAsync(0, parameters);
        }

        #endregion

        #region GetLevelBalance_CurrentBranch_ByBranch_AllOptions_NoFilterSort

        private async Task<TestBalanceViewModel> GetLevelBalance_CheckedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort(
            TestBalanceFormat format, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseCheckedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, format, TestBalanceMode.Level, FinanceReportOptions.All, true)
                : GetByNoTestParameters(gridOptions, format, TestBalanceMode.Level, FinanceReportOptions.All, true);

            return await _repository.GetLevelBalanceAsync(0, parameters);
        }

        private async Task<TestBalanceViewModel> GetLevelBalance_FinalizedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort(
            TestBalanceFormat format, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseFinalizedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, format, TestBalanceMode.Level, FinanceReportOptions.All, true)
                : GetByNoTestParameters(gridOptions, format, TestBalanceMode.Level, FinanceReportOptions.All, true);

            return await _repository.GetLevelBalanceAsync(0, parameters);
        }

        private async Task<TestBalanceViewModel> GetLevelBalance_ConfirmedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort(
            TestBalanceFormat format, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseConfirmedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, format, TestBalanceMode.Level, FinanceReportOptions.All, true)
                : GetByNoTestParameters(gridOptions, format, TestBalanceMode.Level, FinanceReportOptions.All, true);

            return await _repository.GetLevelBalanceAsync(0, parameters);
        }

        private async Task<TestBalanceViewModel> GetLevelBalance_ApprovedVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort(
            TestBalanceFormat format, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseApprovedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, format, TestBalanceMode.Level, FinanceReportOptions.All, true)
                : GetByNoTestParameters(gridOptions, format, TestBalanceMode.Level, FinanceReportOptions.All, true);

            return await _repository.GetLevelBalanceAsync(0, parameters);
        }

        private async Task<TestBalanceViewModel> GetLevelBalance_AllVouchers_CurrentBranch_ByBranch_AllOptions_NoFilterSort(
            TestBalanceFormat format, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, format, TestBalanceMode.Level, FinanceReportOptions.All, true)
                : GetByNoTestParameters(gridOptions, format, TestBalanceMode.Level, FinanceReportOptions.All, true);

            return await _repository.GetLevelBalanceAsync(0, parameters);
        }

        #endregion

        #region GetLevelBalance_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort

        private async Task<TestBalanceViewModel> GetLevelBalance_CheckedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort(
            TestBalanceFormat format, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .UseCheckedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, format, TestBalanceMode.Level, FinanceReportOptions.All, true)
                : GetByNoTestParameters(gridOptions, format, TestBalanceMode.Level, FinanceReportOptions.All, true);

            return await _repository.GetLevelBalanceAsync(0, parameters);
        }

        private async Task<TestBalanceViewModel> GetLevelBalance_FinalizedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort(
            TestBalanceFormat format, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .UseFinalizedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, format, TestBalanceMode.Level, FinanceReportOptions.All, true)
                : GetByNoTestParameters(gridOptions, format, TestBalanceMode.Level, FinanceReportOptions.All, true);

            return await _repository.GetLevelBalanceAsync(0, parameters);
        }

        private async Task<TestBalanceViewModel> GetLevelBalance_ConfirmedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort(
            TestBalanceFormat format, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .UseConfirmedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, format, TestBalanceMode.Level, FinanceReportOptions.All, true)
                : GetByNoTestParameters(gridOptions, format, TestBalanceMode.Level, FinanceReportOptions.All, true);

            return await _repository.GetLevelBalanceAsync(0, parameters);
        }

        private async Task<TestBalanceViewModel> GetLevelBalance_ApprovedVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort(
            TestBalanceFormat format, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .UseApprovedVouchers()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, format, TestBalanceMode.Level, FinanceReportOptions.All, true)
                : GetByNoTestParameters(gridOptions, format, TestBalanceMode.Level, FinanceReportOptions.All, true);

            return await _repository.GetLevelBalanceAsync(0, parameters);
        }

        private async Task<TestBalanceViewModel> GetLevelBalance_AllVouchers_CurrentAndChildren_ByBranch_AllOptions_NoFilterSort(
            TestBalanceFormat format, bool byDate = true)
        {
            var gridOptions = new OptionsBuilder()
                .Build();
            var parameters = byDate
                ? GetByDateTestParameters(gridOptions, format, TestBalanceMode.Level, FinanceReportOptions.All, true)
                : GetByNoTestParameters(gridOptions, format, TestBalanceMode.Level, FinanceReportOptions.All, true);

            return await _repository.GetLevelBalanceAsync(0, parameters);
        }

        #endregion

        #endregion

        private TestBalanceParameters GetByDateTestParameters(
            GridOptions gridOptions, TestBalanceFormat format, TestBalanceMode mode,
            FinanceReportOptions options, bool byBranch = false)
        {
            return new TestBalanceParameters()
            {
                FromDate = DateTime.Parse("2019-04-01"),
                ToDate = DateTime.Parse("2020-03-20"),
                IsByBranch = byBranch,
                ViewId = ViewId.Account,
                GridOptions = gridOptions,
                Format = format,
                Mode = mode,
                Options = options
            };
        }

        private TestBalanceParameters GetByNoTestParameters(
            GridOptions gridOptions, TestBalanceFormat format, TestBalanceMode mode,
            FinanceReportOptions options, bool byBranch = false)
        {
            return new TestBalanceParameters()
            {
                FromNo = 10,
                ToNo = 100,
                IsByBranch = byBranch,
                ViewId = ViewId.Account,
                GridOptions = gridOptions,
                Format = format,
                Mode = mode,
                Options = options
            };
        }

        private ITestBalanceRepository _repository;
    }
}
