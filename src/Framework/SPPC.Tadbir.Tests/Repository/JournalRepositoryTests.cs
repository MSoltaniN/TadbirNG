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
    public class JournalRepositoryTests : RepositoryTestBase
    {
        [OneTimeSetUp]
        public void FixtureSetup()
        {
            SetUp();
        }

        #region Journal By Date Tests

        #region ByDate_CheckedVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort

        [Test]
        public async Task Journal_ByDate_ByRows_CheckedVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_CheckedVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.ByRows);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_ByRowsWithDetail_CheckedVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_CheckedVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.ByRowsWithDetail);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_ByLedger_CheckedVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_CheckedVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.ByLedger);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_BySubsidiary_CheckedVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_CheckedVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.BySubsidiary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_LedgerSummary_CheckedVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_CheckedVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.LedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_LedgerSummaryByDate_CheckedVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_CheckedVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.LedgerSummaryByDate);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_MonthlyLedgerSummary_CheckedVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_CheckedVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.MonthlyLedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        #endregion

        #region ByDate_FinalizedVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort

        [Test]
        public async Task Journal_ByDate_ByRows_FinalizedVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_FinalizedVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.ByRows);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_ByRowsWithDetail_FinalizedVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_FinalizedVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.ByRowsWithDetail);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_ByLedger_FinalizedVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_FinalizedVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.ByLedger);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_BySubsidiary_FinalizedVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_FinalizedVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.BySubsidiary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_LedgerSummary_FinalizedVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_FinalizedVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.LedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_LedgerSummaryByDate_FinalizedVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_FinalizedVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.LedgerSummaryByDate);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_MonthlyLedgerSummary_FinalizedVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_FinalizedVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.MonthlyLedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        #endregion

        #region ByDate_ConfirmedVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort

        [Test]
        public async Task Journal_ByDate_ByRows_ConfirmedVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_ConfirmedVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.ByRows);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_ByRowsWithDetail_ConfirmedVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_ConfirmedVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.ByRowsWithDetail);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_ByLedger_ConfirmedVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_ConfirmedVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.ByLedger);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_BySubsidiary_ConfirmedVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_ConfirmedVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.BySubsidiary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_LedgerSummary_ConfirmedVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_ConfirmedVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.LedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_LedgerSummaryByDate_ConfirmedVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_ConfirmedVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.LedgerSummaryByDate);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_MonthlyLedgerSummary_ConfirmedVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_ConfirmedVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.MonthlyLedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        #endregion

        #region ByDate_ApprovedVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort

        [Test]
        public async Task Journal_ByDate_ByRows_ApprovedVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_ApprovedVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.ByRows);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_ByRowsWithDetail_ApprovedVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_ApprovedVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.ByRowsWithDetail);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_ByLedger_ApprovedVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_ApprovedVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.ByLedger);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_BySubsidiary_ApprovedVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_ApprovedVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.BySubsidiary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_LedgerSummary_ApprovedVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_ApprovedVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.LedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_LedgerSummaryByDate_ApprovedVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_ApprovedVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.LedgerSummaryByDate);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_MonthlyLedgerSummary_ApprovedVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_ApprovedVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.MonthlyLedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        #endregion

        #region ByDate_AllVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort

        [Test]
        public async Task Journal_ByDate_ByRows_AllVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_AllVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.ByRows);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_ByRowsWithDetail_AllVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_AllVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.ByRowsWithDetail);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_ByLedger_AllVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_AllVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.ByLedger);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_BySubsidiary_AllVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_AllVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.BySubsidiary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_LedgerSummary_AllVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_AllVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.LedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_LedgerSummaryByDate_AllVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_AllVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.LedgerSummaryByDate);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_MonthlyLedgerSummary_AllVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_AllVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.MonthlyLedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        #endregion

        #region ByDate_CheckedVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort

        [Test]
        public async Task Journal_ByDate_ByRows_CheckedVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_CheckedVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.ByRows);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_ByRowsWithDetail_CheckedVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_CheckedVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.ByRowsWithDetail);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_ByLedger_CheckedVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_CheckedVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.ByLedger);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_BySubsidiary_CheckedVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_CheckedVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.BySubsidiary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_LedgerSummary_CheckedVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_CheckedVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.LedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_LedgerSummaryByDate_CheckedVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_CheckedVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.LedgerSummaryByDate);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_MonthlyLedgerSummary_CheckedVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_CheckedVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.MonthlyLedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        #endregion

        #region ByDate_FinalizedVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort

        [Test]
        public async Task Journal_ByDate_ByRows_FinalizedVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_FinalizedVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.ByRows);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_ByRowsWithDetail_FinalizedVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_FinalizedVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.ByRowsWithDetail);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_ByLedger_FinalizedVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_FinalizedVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.ByLedger);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_BySubsidiary_FinalizedVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_FinalizedVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.BySubsidiary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_LedgerSummary_FinalizedVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_FinalizedVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.LedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_LedgerSummaryByDate_FinalizedVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_FinalizedVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.LedgerSummaryByDate);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_MonthlyLedgerSummary_FinalizedVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_FinalizedVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.MonthlyLedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        #endregion

        #region ByDate_ConfirmedVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort

        [Test]
        public async Task Journal_ByDate_ByRows_ConfirmedVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_ConfirmedVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.ByRows);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_ByRowsWithDetail_ConfirmedVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_ConfirmedVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.ByRowsWithDetail);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_ByLedger_ConfirmedVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_ConfirmedVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.ByLedger);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_BySubsidiary_ConfirmedVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_ConfirmedVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.BySubsidiary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_LedgerSummary_ConfirmedVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_ConfirmedVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.LedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_LedgerSummaryByDate_ConfirmedVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_ConfirmedVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.LedgerSummaryByDate);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_MonthlyLedgerSummary_ConfirmedVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_ConfirmedVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.MonthlyLedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        #endregion

        #region ByDate_ApprovedVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort

        [Test]
        public async Task Journal_ByDate_ByRows_ApprovedVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_ApprovedVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.ByRows);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_ByRowsWithDetail_ApprovedVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_ApprovedVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.ByRowsWithDetail);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_ByLedger_ApprovedVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_ApprovedVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.ByLedger);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_BySubsidiary_ApprovedVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_ApprovedVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.BySubsidiary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_LedgerSummary_ApprovedVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_ApprovedVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.LedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_LedgerSummaryByDate_ApprovedVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_ApprovedVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.LedgerSummaryByDate);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_MonthlyLedgerSummary_ApprovedVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_ApprovedVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.MonthlyLedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        #endregion

        #region ByDate_AllVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort

        [Test]
        public async Task Journal_ByDate_ByRows_AllVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_AllVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.ByRows);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_ByRowsWithDetail_AllVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_AllVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.ByRowsWithDetail);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_ByLedger_AllVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_AllVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.ByLedger);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_BySubsidiary_AllVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_AllVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.BySubsidiary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_LedgerSummary_AllVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_AllVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.LedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_LedgerSummaryByDate_AllVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_AllVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.LedgerSummaryByDate);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_MonthlyLedgerSummary_AllVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_AllVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.MonthlyLedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        #endregion

        #region ByDate_CheckedVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort

        [Test]
        public async Task Journal_ByDate_ByRows_CheckedVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_CheckedVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.ByRows);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_ByRowsWithDetail_CheckedVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_CheckedVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.ByRowsWithDetail);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_ByLedger_CheckedVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_CheckedVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.ByLedger);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_BySubsidiary_CheckedVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_CheckedVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.BySubsidiary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_LedgerSummary_CheckedVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_CheckedVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.LedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_LedgerSummaryByDate_CheckedVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_CheckedVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.LedgerSummaryByDate);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_MonthlyLedgerSummary_CheckedVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_CheckedVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.MonthlyLedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        #endregion

        #region ByDate_FinalizedVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort

        [Test]
        public async Task Journal_ByDate_ByRows_FinalizedVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_FinalizedVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.ByRows);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_ByRowsWithDetail_FinalizedVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_FinalizedVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.ByRowsWithDetail);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_ByLedger_FinalizedVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_FinalizedVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.ByLedger);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_BySubsidiary_FinalizedVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_FinalizedVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.BySubsidiary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_LedgerSummary_FinalizedVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_FinalizedVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.LedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_LedgerSummaryByDate_FinalizedVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_FinalizedVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.LedgerSummaryByDate);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_MonthlyLedgerSummary_FinalizedVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_FinalizedVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.MonthlyLedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        #endregion

        #region ByDate_ConfirmedVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort

        [Test]
        public async Task Journal_ByDate_ByRows_ConfirmedVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_ConfirmedVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.ByRows);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_ByRowsWithDetail_ConfirmedVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_ConfirmedVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.ByRowsWithDetail);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_ByLedger_ConfirmedVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_ConfirmedVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.ByLedger);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_BySubsidiary_ConfirmedVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_ConfirmedVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.BySubsidiary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_LedgerSummary_ConfirmedVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_ConfirmedVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.LedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_LedgerSummaryByDate_ConfirmedVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_ConfirmedVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.LedgerSummaryByDate);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_MonthlyLedgerSummary_ConfirmedVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_ConfirmedVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.MonthlyLedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        #endregion

        #region ByDate_ApprovedVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort

        [Test]
        public async Task Journal_ByDate_ByRows_ApprovedVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_ApprovedVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.ByRows);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_ByRowsWithDetail_ApprovedVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_ApprovedVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.ByRowsWithDetail);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_ByLedger_ApprovedVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_ApprovedVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.ByLedger);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_BySubsidiary_ApprovedVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_ApprovedVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.BySubsidiary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_LedgerSummary_ApprovedVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_ApprovedVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.LedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_LedgerSummaryByDate_ApprovedVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_ApprovedVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.LedgerSummaryByDate);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_MonthlyLedgerSummary_ApprovedVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_ApprovedVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.MonthlyLedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        #endregion

        #region ByDate_AllVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort

        [Test]
        public async Task Journal_ByDate_ByRows_AllVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_AllVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.ByRows);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_ByRowsWithDetail_AllVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_AllVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.ByRowsWithDetail);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_ByLedger_AllVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_AllVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.ByLedger);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_BySubsidiary_AllVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_AllVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.BySubsidiary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_LedgerSummary_AllVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_AllVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.LedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_LedgerSummaryByDate_AllVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_AllVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.LedgerSummaryByDate);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_MonthlyLedgerSummary_AllVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_AllVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.MonthlyLedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        #endregion

        #region ByDate_CheckedVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort

        [Test]
        public async Task Journal_ByDate_ByRows_CheckedVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_CheckedVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.ByRows);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_ByRowsWithDetail_CheckedVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_CheckedVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.ByRowsWithDetail);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_ByLedger_CheckedVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_CheckedVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.ByLedger);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_BySubsidiary_CheckedVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_CheckedVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.BySubsidiary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_LedgerSummary_CheckedVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_CheckedVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.LedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_LedgerSummaryByDate_CheckedVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_CheckedVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.LedgerSummaryByDate);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_MonthlyLedgerSummary_CheckedVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_CheckedVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.MonthlyLedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        #endregion

        #region ByDate_FinalizedVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort

        [Test]
        public async Task Journal_ByDate_ByRows_FinalizedVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_FinalizedVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.ByRows);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_ByRowsWithDetail_FinalizedVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_FinalizedVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.ByRowsWithDetail);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_ByLedger_FinalizedVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_FinalizedVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.ByLedger);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_BySubsidiary_FinalizedVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_FinalizedVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.BySubsidiary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_LedgerSummary_FinalizedVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_FinalizedVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.LedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_LedgerSummaryByDate_FinalizedVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_FinalizedVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.LedgerSummaryByDate);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_MonthlyLedgerSummary_FinalizedVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_FinalizedVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.MonthlyLedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        #endregion

        #region ByDate_ConfirmedVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort

        [Test]
        public async Task Journal_ByDate_ByRows_ConfirmedVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_ConfirmedVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.ByRows);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_ByRowsWithDetail_ConfirmedVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_ConfirmedVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.ByRowsWithDetail);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_ByLedger_ConfirmedVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_ConfirmedVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.ByLedger);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_BySubsidiary_ConfirmedVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_ConfirmedVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.BySubsidiary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_LedgerSummary_ConfirmedVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_ConfirmedVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.LedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_LedgerSummaryByDate_ConfirmedVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_ConfirmedVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.LedgerSummaryByDate);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_MonthlyLedgerSummary_ConfirmedVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_ConfirmedVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.MonthlyLedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        #endregion

        #region ByDate_ApprovedVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort

        [Test]
        public async Task Journal_ByDate_ByRows_ApprovedVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_ApprovedVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.ByRows);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_ByRowsWithDetail_ApprovedVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_ApprovedVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.ByRowsWithDetail);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_ByLedger_ApprovedVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_ApprovedVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.ByLedger);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_BySubsidiary_ApprovedVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_ApprovedVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.BySubsidiary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_LedgerSummary_ApprovedVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_ApprovedVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.LedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_LedgerSummaryByDate_ApprovedVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_ApprovedVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.LedgerSummaryByDate);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_MonthlyLedgerSummary_ApprovedVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_ApprovedVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.MonthlyLedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        #endregion

        #region ByDate_AllVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort

        [Test]
        public async Task Journal_ByDate_ByRows_AllVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_AllVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.ByRows);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_ByRowsWithDetail_AllVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_AllVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.ByRowsWithDetail);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_ByLedger_AllVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_AllVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.ByLedger);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_BySubsidiary_AllVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_AllVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.BySubsidiary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_LedgerSummary_AllVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_AllVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.LedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_LedgerSummaryByDate_AllVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_AllVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.LedgerSummaryByDate);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_MonthlyLedgerSummary_AllVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_AllVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.MonthlyLedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        #endregion

        #region ByDate_CheckedVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort

        [Test]
        public async Task Journal_ByDate_ByRows_CheckedVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_CheckedVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.ByRows);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_ByRowsWithDetail_CheckedVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_CheckedVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.ByRowsWithDetail);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_ByLedger_CheckedVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_CheckedVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.ByLedger);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_BySubsidiary_CheckedVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_CheckedVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.BySubsidiary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_LedgerSummary_CheckedVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_CheckedVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.LedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_LedgerSummaryByDate_CheckedVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_CheckedVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.LedgerSummaryByDate);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_MonthlyLedgerSummary_CheckedVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_CheckedVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.MonthlyLedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        #endregion

        #region ByDate_FinalizedVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort

        [Test]
        public async Task Journal_ByDate_ByRows_FinalizedVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_FinalizedVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.ByRows);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_ByRowsWithDetail_FinalizedVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_FinalizedVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.ByRowsWithDetail);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_ByLedger_FinalizedVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_FinalizedVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.ByLedger);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_BySubsidiary_FinalizedVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_FinalizedVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.BySubsidiary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_LedgerSummary_FinalizedVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_FinalizedVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.LedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_LedgerSummaryByDate_FinalizedVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_FinalizedVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.LedgerSummaryByDate);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_MonthlyLedgerSummary_FinalizedVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_FinalizedVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.MonthlyLedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        #endregion

        #region ByDate_ConfirmedVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort

        [Test]
        public async Task Journal_ByDate_ByRows_ConfirmedVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_ConfirmedVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.ByRows);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_ByRowsWithDetail_ConfirmedVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_ConfirmedVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.ByRowsWithDetail);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_ByLedger_ConfirmedVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_ConfirmedVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.ByLedger);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_BySubsidiary_ConfirmedVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_ConfirmedVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.BySubsidiary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_LedgerSummary_ConfirmedVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_ConfirmedVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.LedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_LedgerSummaryByDate_ConfirmedVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_ConfirmedVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.LedgerSummaryByDate);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_MonthlyLedgerSummary_ConfirmedVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_ConfirmedVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.MonthlyLedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        #endregion

        #region ByDate_ApprovedVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort

        [Test]
        public async Task Journal_ByDate_ByRows_ApprovedVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_ApprovedVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.ByRows);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_ByRowsWithDetail_ApprovedVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_ApprovedVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.ByRowsWithDetail);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_ByLedger_ApprovedVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_ApprovedVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.ByLedger);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_BySubsidiary_ApprovedVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_ApprovedVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.BySubsidiary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_LedgerSummary_ApprovedVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_ApprovedVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.LedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_LedgerSummaryByDate_ApprovedVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_ApprovedVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.LedgerSummaryByDate);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_MonthlyLedgerSummary_ApprovedVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_ApprovedVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.MonthlyLedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        #endregion

        #region ByDate_AllVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort

        [Test]
        public async Task Journal_ByDate_ByRows_AllVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_AllVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.ByRows);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_ByRowsWithDetail_AllVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_AllVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.ByRowsWithDetail);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_ByLedger_AllVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_AllVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.ByLedger);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_BySubsidiary_AllVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_AllVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.BySubsidiary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_LedgerSummary_AllVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_AllVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.LedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_LedgerSummaryByDate_AllVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_AllVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.LedgerSummaryByDate);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_MonthlyLedgerSummary_AllVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_AllVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.MonthlyLedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        #endregion

        #region ByDate_CheckedVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort

        [Test]
        public async Task Journal_ByDate_ByRows_CheckedVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_CheckedVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.ByRows);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_ByRowsWithDetail_CheckedVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_CheckedVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.ByRowsWithDetail);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_ByLedger_CheckedVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_CheckedVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.ByLedger);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_BySubsidiary_CheckedVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_CheckedVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.BySubsidiary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_LedgerSummary_CheckedVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_CheckedVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.LedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_LedgerSummaryByDate_CheckedVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_CheckedVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.LedgerSummaryByDate);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_MonthlyLedgerSummary_CheckedVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_CheckedVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.MonthlyLedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        #endregion

        #region ByDate_FinalizedVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort

        [Test]
        public async Task Journal_ByDate_ByRows_FinalizedVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_FinalizedVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.ByRows);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_ByRowsWithDetail_FinalizedVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_FinalizedVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.ByRowsWithDetail);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_ByLedger_FinalizedVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_FinalizedVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.ByLedger);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_BySubsidiary_FinalizedVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_FinalizedVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.BySubsidiary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_LedgerSummary_FinalizedVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_FinalizedVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.LedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_LedgerSummaryByDate_FinalizedVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_FinalizedVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.LedgerSummaryByDate);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_MonthlyLedgerSummary_FinalizedVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_FinalizedVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.MonthlyLedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        #endregion

        #region ByDate_ConfirmedVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort

        [Test]
        public async Task Journal_ByDate_ByRows_ConfirmedVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_ConfirmedVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.ByRows);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_ByRowsWithDetail_ConfirmedVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_ConfirmedVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.ByRowsWithDetail);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_ByLedger_ConfirmedVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_ConfirmedVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.ByLedger);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_BySubsidiary_ConfirmedVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_ConfirmedVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.BySubsidiary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_LedgerSummary_ConfirmedVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_ConfirmedVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.LedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_LedgerSummaryByDate_ConfirmedVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_ConfirmedVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.LedgerSummaryByDate);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_MonthlyLedgerSummary_ConfirmedVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_ConfirmedVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.MonthlyLedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        #endregion

        #region ByDate_ApprovedVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort

        [Test]
        public async Task Journal_ByDate_ByRows_ApprovedVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_ApprovedVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.ByRows);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_ByRowsWithDetail_ApprovedVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_ApprovedVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.ByRowsWithDetail);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_ByLedger_ApprovedVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_ApprovedVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.ByLedger);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_BySubsidiary_ApprovedVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_ApprovedVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.BySubsidiary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_LedgerSummary_ApprovedVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_ApprovedVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.LedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_LedgerSummaryByDate_ApprovedVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_ApprovedVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.LedgerSummaryByDate);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_MonthlyLedgerSummary_ApprovedVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_ApprovedVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.MonthlyLedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        #endregion

        #region ByDate_AllVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort

        [Test]
        public async Task Journal_ByDate_ByRows_AllVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_AllVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.ByRows);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_ByRowsWithDetail_AllVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_AllVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.ByRowsWithDetail);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_ByLedger_AllVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_AllVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.ByLedger);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_BySubsidiary_AllVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_AllVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.BySubsidiary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_LedgerSummary_AllVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_AllVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.LedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_LedgerSummaryByDate_AllVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_AllVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.LedgerSummaryByDate);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_MonthlyLedgerSummary_AllVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_AllVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.MonthlyLedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        #endregion

        #endregion

        #region Journal By Date By Branch Tests

        #region ByDate_CheckedVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort

        [Test]
        public async Task Journal_ByDate_ByRows_CheckedVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_CheckedVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.ByRows);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_ByRowsWithDetail_CheckedVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_CheckedVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.ByRowsWithDetail);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_ByLedger_CheckedVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_CheckedVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.ByLedger);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_BySubsidiary_CheckedVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_CheckedVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.BySubsidiary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_LedgerSummary_CheckedVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_CheckedVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.LedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_LedgerSummaryByDate_CheckedVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_CheckedVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.LedgerSummaryByDate);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_MonthlyLedgerSummary_CheckedVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_CheckedVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.MonthlyLedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        #endregion

        #region ByDate_FinalizedVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort

        [Test]
        public async Task Journal_ByDate_ByRows_FinalizedVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_FinalizedVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.ByRows);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_ByRowsWithDetail_FinalizedVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_FinalizedVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.ByRowsWithDetail);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_ByLedger_FinalizedVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_FinalizedVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.ByLedger);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_BySubsidiary_FinalizedVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_FinalizedVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.BySubsidiary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_LedgerSummary_FinalizedVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_FinalizedVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.LedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_LedgerSummaryByDate_FinalizedVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_FinalizedVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.LedgerSummaryByDate);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_MonthlyLedgerSummary_FinalizedVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_FinalizedVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.MonthlyLedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        #endregion

        #region ByDate_ConfirmedVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort

        [Test]
        public async Task Journal_ByDate_ByRows_ConfirmedVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_ConfirmedVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.ByRows);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_ByRowsWithDetail_ConfirmedVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_ConfirmedVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.ByRowsWithDetail);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_ByLedger_ConfirmedVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_ConfirmedVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.ByLedger);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_BySubsidiary_ConfirmedVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_ConfirmedVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.BySubsidiary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_LedgerSummary_ConfirmedVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_ConfirmedVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.LedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_LedgerSummaryByDate_ConfirmedVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_ConfirmedVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.LedgerSummaryByDate);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_MonthlyLedgerSummary_ConfirmedVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_ConfirmedVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.MonthlyLedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        #endregion

        #region ByDate_ApprovedVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort

        [Test]
        public async Task Journal_ByDate_ByRows_ApprovedVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_ApprovedVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.ByRows);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_ByRowsWithDetail_ApprovedVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_ApprovedVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.ByRowsWithDetail);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_ByLedger_ApprovedVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_ApprovedVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.ByLedger);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_BySubsidiary_ApprovedVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_ApprovedVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.BySubsidiary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_LedgerSummary_ApprovedVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_ApprovedVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.LedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_LedgerSummaryByDate_ApprovedVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_ApprovedVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.LedgerSummaryByDate);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_MonthlyLedgerSummary_ApprovedVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_ApprovedVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.MonthlyLedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        #endregion

        #region ByDate_AllVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort

        [Test]
        public async Task Journal_ByDate_ByRows_AllVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_AllVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.ByRows);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_ByRowsWithDetail_AllVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_AllVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.ByRowsWithDetail);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_ByLedger_AllVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_AllVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.ByLedger);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_BySubsidiary_AllVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_AllVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.BySubsidiary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_LedgerSummary_AllVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_AllVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.LedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_LedgerSummaryByDate_AllVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_AllVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.LedgerSummaryByDate);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_MonthlyLedgerSummary_AllVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_AllVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.MonthlyLedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        #endregion

        #region ByDate_CheckedVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort

        [Test]
        public async Task Journal_ByDate_ByRows_CheckedVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_CheckedVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.ByRows);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_ByRowsWithDetail_CheckedVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_CheckedVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.ByRowsWithDetail);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_ByLedger_CheckedVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_CheckedVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.ByLedger);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_BySubsidiary_CheckedVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_CheckedVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.BySubsidiary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_LedgerSummary_CheckedVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_CheckedVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.LedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_LedgerSummaryByDate_CheckedVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_CheckedVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.LedgerSummaryByDate);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_MonthlyLedgerSummary_CheckedVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_CheckedVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.MonthlyLedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        #endregion

        #region ByDate_FinalizedVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort

        [Test]
        public async Task Journal_ByDate_ByRows_FinalizedVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_FinalizedVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.ByRows);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_ByRowsWithDetail_FinalizedVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_FinalizedVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.ByRowsWithDetail);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_ByLedger_FinalizedVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_FinalizedVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.ByLedger);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_BySubsidiary_FinalizedVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_FinalizedVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.BySubsidiary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_LedgerSummary_FinalizedVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_FinalizedVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.LedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_LedgerSummaryByDate_FinalizedVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_FinalizedVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.LedgerSummaryByDate);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_MonthlyLedgerSummary_FinalizedVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_FinalizedVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.MonthlyLedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        #endregion

        #region ByDate_ConfirmedVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort

        [Test]
        public async Task Journal_ByDate_ByRows_ConfirmedVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_ConfirmedVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.ByRows);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_ByRowsWithDetail_ConfirmedVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_ConfirmedVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.ByRowsWithDetail);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_ByLedger_ConfirmedVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_ConfirmedVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.ByLedger);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_BySubsidiary_ConfirmedVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_ConfirmedVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.BySubsidiary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_LedgerSummary_ConfirmedVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_ConfirmedVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.LedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_LedgerSummaryByDate_ConfirmedVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_ConfirmedVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.LedgerSummaryByDate);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_MonthlyLedgerSummary_ConfirmedVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_ConfirmedVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.MonthlyLedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        #endregion

        #region ByDate_ApprovedVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort

        [Test]
        public async Task Journal_ByDate_ByRows_ApprovedVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_ApprovedVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.ByRows);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_ByRowsWithDetail_ApprovedVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_ApprovedVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.ByRowsWithDetail);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_ByLedger_ApprovedVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_ApprovedVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.ByLedger);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_BySubsidiary_ApprovedVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_ApprovedVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.BySubsidiary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_LedgerSummary_ApprovedVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_ApprovedVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.LedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_LedgerSummaryByDate_ApprovedVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_ApprovedVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.LedgerSummaryByDate);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_MonthlyLedgerSummary_ApprovedVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_ApprovedVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.MonthlyLedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        #endregion

        #region ByDate_AllVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort

        [Test]
        public async Task Journal_ByDate_ByRows_AllVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_AllVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.ByRows);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_ByRowsWithDetail_AllVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_AllVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.ByRowsWithDetail);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_ByLedger_AllVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_AllVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.ByLedger);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_BySubsidiary_AllVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_AllVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.BySubsidiary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_LedgerSummary_AllVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_AllVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.LedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_LedgerSummaryByDate_AllVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_AllVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.LedgerSummaryByDate);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_MonthlyLedgerSummary_AllVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_AllVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.MonthlyLedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        #endregion

        #region ByDate_CheckedVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort

        [Test]
        public async Task Journal_ByDate_ByRows_CheckedVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_CheckedVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.ByRows);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_ByRowsWithDetail_CheckedVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_CheckedVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.ByRowsWithDetail);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_ByLedger_CheckedVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_CheckedVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.ByLedger);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_BySubsidiary_CheckedVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_CheckedVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.BySubsidiary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_LedgerSummary_CheckedVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_CheckedVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.LedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_LedgerSummaryByDate_CheckedVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_CheckedVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.LedgerSummaryByDate);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_MonthlyLedgerSummary_CheckedVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_CheckedVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.MonthlyLedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        #endregion

        #region ByDate_FinalizedVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort

        [Test]
        public async Task Journal_ByDate_ByRows_FinalizedVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_FinalizedVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.ByRows);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_ByRowsWithDetail_FinalizedVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_FinalizedVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.ByRowsWithDetail);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_ByLedger_FinalizedVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_FinalizedVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.ByLedger);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_BySubsidiary_FinalizedVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_FinalizedVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.BySubsidiary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_LedgerSummary_FinalizedVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_FinalizedVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.LedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_LedgerSummaryByDate_FinalizedVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_FinalizedVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.LedgerSummaryByDate);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_MonthlyLedgerSummary_FinalizedVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_FinalizedVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.MonthlyLedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        #endregion

        #region ByDate_ConfirmedVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort

        [Test]
        public async Task Journal_ByDate_ByRows_ConfirmedVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_ConfirmedVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.ByRows);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_ByRowsWithDetail_ConfirmedVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_ConfirmedVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.ByRowsWithDetail);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_ByLedger_ConfirmedVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_ConfirmedVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.ByLedger);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_BySubsidiary_ConfirmedVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_ConfirmedVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.BySubsidiary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_LedgerSummary_ConfirmedVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_ConfirmedVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.LedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_LedgerSummaryByDate_ConfirmedVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_ConfirmedVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.LedgerSummaryByDate);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_MonthlyLedgerSummary_ConfirmedVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_ConfirmedVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.MonthlyLedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        #endregion

        #region ByDate_ApprovedVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort

        [Test]
        public async Task Journal_ByDate_ByRows_ApprovedVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_ApprovedVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.ByRows);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_ByRowsWithDetail_ApprovedVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_ApprovedVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.ByRowsWithDetail);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_ByLedger_ApprovedVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_ApprovedVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.ByLedger);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_BySubsidiary_ApprovedVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_ApprovedVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.BySubsidiary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_LedgerSummary_ApprovedVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_ApprovedVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.LedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_LedgerSummaryByDate_ApprovedVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_ApprovedVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.LedgerSummaryByDate);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_MonthlyLedgerSummary_ApprovedVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_ApprovedVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.MonthlyLedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        #endregion

        #region ByDate_AllVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort

        [Test]
        public async Task Journal_ByDate_ByRows_AllVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_AllVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.ByRows);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_ByRowsWithDetail_AllVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_AllVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.ByRowsWithDetail);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_ByLedger_AllVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_AllVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.ByLedger);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_BySubsidiary_AllVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_AllVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.BySubsidiary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_LedgerSummary_AllVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_AllVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.LedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_LedgerSummaryByDate_AllVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_AllVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.LedgerSummaryByDate);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_MonthlyLedgerSummary_AllVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_AllVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.MonthlyLedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        #endregion

        #region ByDate_CheckedVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort

        [Test]
        public async Task Journal_ByDate_ByRows_CheckedVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_CheckedVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.ByRows);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_ByRowsWithDetail_CheckedVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_CheckedVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.ByRowsWithDetail);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_ByLedger_CheckedVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_CheckedVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.ByLedger);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_BySubsidiary_CheckedVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_CheckedVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.BySubsidiary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_LedgerSummary_CheckedVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_CheckedVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.LedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_LedgerSummaryByDate_CheckedVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_CheckedVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.LedgerSummaryByDate);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_MonthlyLedgerSummary_CheckedVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_CheckedVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.MonthlyLedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        #endregion

        #region ByDate_FinalizedVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort

        [Test]
        public async Task Journal_ByDate_ByRows_FinalizedVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_FinalizedVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.ByRows);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_ByRowsWithDetail_FinalizedVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_FinalizedVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.ByRowsWithDetail);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_ByLedger_FinalizedVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_FinalizedVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.ByLedger);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_BySubsidiary_FinalizedVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_FinalizedVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.BySubsidiary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_LedgerSummary_FinalizedVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_FinalizedVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.LedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_LedgerSummaryByDate_FinalizedVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_FinalizedVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.LedgerSummaryByDate);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_MonthlyLedgerSummary_FinalizedVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_FinalizedVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.MonthlyLedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        #endregion

        #region ByDate_ConfirmedVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort

        [Test]
        public async Task Journal_ByDate_ByRows_ConfirmedVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_ConfirmedVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.ByRows);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_ByRowsWithDetail_ConfirmedVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_ConfirmedVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.ByRowsWithDetail);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_ByLedger_ConfirmedVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_ConfirmedVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.ByLedger);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_BySubsidiary_ConfirmedVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_ConfirmedVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.BySubsidiary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_LedgerSummary_ConfirmedVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_ConfirmedVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.LedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_LedgerSummaryByDate_ConfirmedVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_ConfirmedVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.LedgerSummaryByDate);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_MonthlyLedgerSummary_ConfirmedVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_ConfirmedVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.MonthlyLedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        #endregion

        #region ByDate_ApprovedVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort

        [Test]
        public async Task Journal_ByDate_ByRows_ApprovedVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_ApprovedVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.ByRows);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_ByRowsWithDetail_ApprovedVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_ApprovedVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.ByRowsWithDetail);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_ByLedger_ApprovedVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_ApprovedVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.ByLedger);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_BySubsidiary_ApprovedVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_ApprovedVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.BySubsidiary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_LedgerSummary_ApprovedVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_ApprovedVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.LedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_LedgerSummaryByDate_ApprovedVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_ApprovedVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.LedgerSummaryByDate);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_MonthlyLedgerSummary_ApprovedVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_ApprovedVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.MonthlyLedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        #endregion

        #region ByDate_AllVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort

        [Test]
        public async Task Journal_ByDate_ByRows_AllVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_AllVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.ByRows);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_ByRowsWithDetail_AllVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_AllVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.ByRowsWithDetail);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_ByLedger_AllVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_AllVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.ByLedger);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_BySubsidiary_AllVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_AllVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.BySubsidiary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_LedgerSummary_AllVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_AllVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.LedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_LedgerSummaryByDate_AllVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_AllVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.LedgerSummaryByDate);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_MonthlyLedgerSummary_AllVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_AllVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.MonthlyLedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        #endregion

        #region ByDate_CheckedVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort

        [Test]
        public async Task Journal_ByDate_ByRows_CheckedVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_CheckedVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.ByRows);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_ByRowsWithDetail_CheckedVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_CheckedVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.ByRowsWithDetail);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_ByLedger_CheckedVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_CheckedVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.ByLedger);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_BySubsidiary_CheckedVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_CheckedVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.BySubsidiary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_LedgerSummary_CheckedVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_CheckedVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.LedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_LedgerSummaryByDate_CheckedVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_CheckedVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.LedgerSummaryByDate);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_MonthlyLedgerSummary_CheckedVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_CheckedVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.MonthlyLedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        #endregion

        #region ByDate_FinalizedVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort

        [Test]
        public async Task Journal_ByDate_ByRows_FinalizedVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_FinalizedVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.ByRows);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_ByRowsWithDetail_FinalizedVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_FinalizedVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.ByRowsWithDetail);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_ByLedger_FinalizedVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_FinalizedVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.ByLedger);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_BySubsidiary_FinalizedVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_FinalizedVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.BySubsidiary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_LedgerSummary_FinalizedVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_FinalizedVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.LedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_LedgerSummaryByDate_FinalizedVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_FinalizedVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.LedgerSummaryByDate);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_MonthlyLedgerSummary_FinalizedVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_FinalizedVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.MonthlyLedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        #endregion

        #region ByDate_ConfirmedVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort

        [Test]
        public async Task Journal_ByDate_ByRows_ConfirmedVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_ConfirmedVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.ByRows);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_ByRowsWithDetail_ConfirmedVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_ConfirmedVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.ByRowsWithDetail);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_ByLedger_ConfirmedVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_ConfirmedVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.ByLedger);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_BySubsidiary_ConfirmedVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_ConfirmedVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.BySubsidiary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_LedgerSummary_ConfirmedVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_ConfirmedVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.LedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_LedgerSummaryByDate_ConfirmedVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_ConfirmedVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.LedgerSummaryByDate);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_MonthlyLedgerSummary_ConfirmedVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_ConfirmedVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.MonthlyLedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        #endregion

        #region ByDate_ApprovedVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort

        [Test]
        public async Task Journal_ByDate_ByRows_ApprovedVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_ApprovedVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.ByRows);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_ByRowsWithDetail_ApprovedVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_ApprovedVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.ByRowsWithDetail);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_ByLedger_ApprovedVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_ApprovedVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.ByLedger);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_BySubsidiary_ApprovedVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_ApprovedVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.BySubsidiary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_LedgerSummary_ApprovedVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_ApprovedVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.LedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_LedgerSummaryByDate_ApprovedVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_ApprovedVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.LedgerSummaryByDate);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_MonthlyLedgerSummary_ApprovedVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_ApprovedVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.MonthlyLedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        #endregion

        #region ByDate_AllVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort

        [Test]
        public async Task Journal_ByDate_ByRows_AllVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_AllVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.ByRows);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_ByRowsWithDetail_AllVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_AllVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.ByRowsWithDetail);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_ByLedger_AllVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_AllVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.ByLedger);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_BySubsidiary_AllVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_AllVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.BySubsidiary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_LedgerSummary_AllVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_AllVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.LedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_LedgerSummaryByDate_AllVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_AllVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.LedgerSummaryByDate);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_MonthlyLedgerSummary_AllVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_AllVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.MonthlyLedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        #endregion

        #region ByDate_CheckedVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort

        [Test]
        public async Task Journal_ByDate_ByRows_CheckedVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_CheckedVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.ByRows);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_ByRowsWithDetail_CheckedVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_CheckedVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.ByRowsWithDetail);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_ByLedger_CheckedVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_CheckedVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.ByLedger);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_BySubsidiary_CheckedVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_CheckedVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.BySubsidiary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_LedgerSummary_CheckedVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_CheckedVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.LedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_LedgerSummaryByDate_CheckedVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_CheckedVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.LedgerSummaryByDate);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_MonthlyLedgerSummary_CheckedVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_CheckedVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.MonthlyLedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        #endregion

        #region ByDate_FinalizedVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort

        [Test]
        public async Task Journal_ByDate_ByRows_FinalizedVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_FinalizedVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.ByRows);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_ByRowsWithDetail_FinalizedVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_FinalizedVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.ByRowsWithDetail);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_ByLedger_FinalizedVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_FinalizedVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.ByLedger);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_BySubsidiary_FinalizedVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_FinalizedVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.BySubsidiary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_LedgerSummary_FinalizedVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_FinalizedVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.LedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_LedgerSummaryByDate_FinalizedVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_FinalizedVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.LedgerSummaryByDate);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_MonthlyLedgerSummary_FinalizedVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_FinalizedVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.MonthlyLedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        #endregion

        #region ByDate_ConfirmedVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort

        [Test]
        public async Task Journal_ByDate_ByRows_ConfirmedVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_ConfirmedVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.ByRows);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_ByRowsWithDetail_ConfirmedVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_ConfirmedVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.ByRowsWithDetail);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_ByLedger_ConfirmedVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_ConfirmedVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.ByLedger);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_BySubsidiary_ConfirmedVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_ConfirmedVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.BySubsidiary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_LedgerSummary_ConfirmedVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_ConfirmedVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.LedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_LedgerSummaryByDate_ConfirmedVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_ConfirmedVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.LedgerSummaryByDate);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_MonthlyLedgerSummary_ConfirmedVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_ConfirmedVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.MonthlyLedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        #endregion

        #region ByDate_ApprovedVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort

        [Test]
        public async Task Journal_ByDate_ByRows_ApprovedVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_ApprovedVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.ByRows);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_ByRowsWithDetail_ApprovedVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_ApprovedVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.ByRowsWithDetail);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_ByLedger_ApprovedVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_ApprovedVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.ByLedger);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_BySubsidiary_ApprovedVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_ApprovedVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.BySubsidiary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_LedgerSummary_ApprovedVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_ApprovedVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.LedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_LedgerSummaryByDate_ApprovedVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_ApprovedVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.LedgerSummaryByDate);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_MonthlyLedgerSummary_ApprovedVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_ApprovedVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.MonthlyLedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        #endregion

        #region ByDate_AllVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort

        [Test]
        public async Task Journal_ByDate_ByRows_AllVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_AllVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.ByRows);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_ByRowsWithDetail_AllVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_AllVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.ByRowsWithDetail);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_ByLedger_AllVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_AllVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.ByLedger);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_BySubsidiary_AllVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_AllVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.BySubsidiary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_LedgerSummary_AllVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_AllVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.LedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_LedgerSummaryByDate_AllVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_AllVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.LedgerSummaryByDate);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByDate_MonthlyLedgerSummary_AllVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByDate_AllVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.MonthlyLedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        #endregion

        #endregion

        #region Journal By No Tests

        #region ByNo_CheckedVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort

        [Test]
        public async Task Journal_ByNo_ByRows_CheckedVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_CheckedVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.ByRows);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_ByRowsWithDetail_CheckedVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_CheckedVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.ByRowsWithDetail);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_ByLedger_CheckedVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_CheckedVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.ByLedger);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_BySubsidiary_CheckedVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_CheckedVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.BySubsidiary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_LedgerSummary_CheckedVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_CheckedVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.LedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        #endregion

        #region ByNo_FinalizedVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort

        [Test]
        public async Task Journal_ByNo_ByRows_FinalizedVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_FinalizedVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.ByRows);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_ByRowsWithDetail_FinalizedVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_FinalizedVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.ByRowsWithDetail);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_ByLedger_FinalizedVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_FinalizedVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.ByLedger);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_BySubsidiary_FinalizedVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_FinalizedVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.BySubsidiary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_LedgerSummary_FinalizedVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_FinalizedVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.LedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        #endregion

        #region ByNo_ConfirmedVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort

        [Test]
        public async Task Journal_ByNo_ByRows_ConfirmedVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_ConfirmedVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.ByRows);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_ByRowsWithDetail_ConfirmedVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_ConfirmedVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.ByRowsWithDetail);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_ByLedger_ConfirmedVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_ConfirmedVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.ByLedger);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_BySubsidiary_ConfirmedVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_ConfirmedVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.BySubsidiary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_LedgerSummary_ConfirmedVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_ConfirmedVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.LedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        #endregion

        #region ByNo_ApprovedVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort

        [Test]
        public async Task Journal_ByNo_ByRows_ApprovedVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_ApprovedVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.ByRows);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_ByRowsWithDetail_ApprovedVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_ApprovedVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.ByRowsWithDetail);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_ByLedger_ApprovedVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_ApprovedVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.ByLedger);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_BySubsidiary_ApprovedVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_ApprovedVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.BySubsidiary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_LedgerSummary_ApprovedVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_ApprovedVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.LedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        #endregion

        #region ByNo_AllVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort

        [Test]
        public async Task Journal_ByNo_ByRows_AllVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_AllVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.ByRows);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_ByRowsWithDetail_AllVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_AllVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.ByRowsWithDetail);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_ByLedger_AllVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_AllVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.ByLedger);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_BySubsidiary_AllVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_AllVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.BySubsidiary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_LedgerSummary_AllVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_AllVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.LedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        #endregion

        #region ByNo_CheckedVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort

        [Test]
        public async Task Journal_ByNo_ByRows_CheckedVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_CheckedVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.ByRows);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_ByRowsWithDetail_CheckedVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_CheckedVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.ByRowsWithDetail);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_ByLedger_CheckedVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_CheckedVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.ByLedger);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_BySubsidiary_CheckedVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_CheckedVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.BySubsidiary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_LedgerSummary_CheckedVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_CheckedVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.LedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        #endregion

        #region ByNo_FinalizedVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort

        [Test]
        public async Task Journal_ByNo_ByRows_FinalizedVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_FinalizedVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.ByRows);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_ByRowsWithDetail_FinalizedVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_FinalizedVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.ByRowsWithDetail);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_ByLedger_FinalizedVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_FinalizedVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.ByLedger);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_BySubsidiary_FinalizedVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_FinalizedVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.BySubsidiary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_LedgerSummary_FinalizedVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_FinalizedVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.LedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        #endregion

        #region ByNo_ConfirmedVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort

        [Test]
        public async Task Journal_ByNo_ByRows_ConfirmedVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_ConfirmedVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.ByRows);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_ByRowsWithDetail_ConfirmedVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_ConfirmedVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.ByRowsWithDetail);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_ByLedger_ConfirmedVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_ConfirmedVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.ByLedger);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_BySubsidiary_ConfirmedVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_ConfirmedVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.BySubsidiary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_LedgerSummary_ConfirmedVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_ConfirmedVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.LedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        #endregion

        #region ByNo_ApprovedVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort

        [Test]
        public async Task Journal_ByNo_ByRows_ApprovedVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_ApprovedVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.ByRows);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_ByRowsWithDetail_ApprovedVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_ApprovedVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.ByRowsWithDetail);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_ByLedger_ApprovedVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_ApprovedVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.ByLedger);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_BySubsidiary_ApprovedVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_ApprovedVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.BySubsidiary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_LedgerSummary_ApprovedVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_ApprovedVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.LedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        #endregion

        #region ByNo_AllVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort

        [Test]
        public async Task Journal_ByNo_ByRows_AllVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_AllVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.ByRows);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_ByRowsWithDetail_AllVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_AllVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.ByRowsWithDetail);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_ByLedger_AllVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_AllVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.ByLedger);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_BySubsidiary_AllVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_AllVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.BySubsidiary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_LedgerSummary_AllVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_AllVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.LedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        #endregion

        #region ByNo_CheckedVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort

        [Test]
        public async Task Journal_ByNo_ByRows_CheckedVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_CheckedVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.ByRows);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_ByRowsWithDetail_CheckedVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_CheckedVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.ByRowsWithDetail);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_ByLedger_CheckedVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_CheckedVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.ByLedger);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_BySubsidiary_CheckedVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_CheckedVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.BySubsidiary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_LedgerSummary_CheckedVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_CheckedVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.LedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        #endregion

        #region ByNo_FinalizedVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort

        [Test]
        public async Task Journal_ByNo_ByRows_FinalizedVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_FinalizedVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.ByRows);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_ByRowsWithDetail_FinalizedVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_FinalizedVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.ByRowsWithDetail);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_ByLedger_FinalizedVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_FinalizedVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.ByLedger);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_BySubsidiary_FinalizedVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_FinalizedVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.BySubsidiary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_LedgerSummary_FinalizedVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_FinalizedVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.LedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        #endregion

        #region ByNo_ConfirmedVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort

        [Test]
        public async Task Journal_ByNo_ByRows_ConfirmedVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_ConfirmedVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.ByRows);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_ByRowsWithDetail_ConfirmedVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_ConfirmedVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.ByRowsWithDetail);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_ByLedger_ConfirmedVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_ConfirmedVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.ByLedger);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_BySubsidiary_ConfirmedVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_ConfirmedVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.BySubsidiary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_LedgerSummary_ConfirmedVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_ConfirmedVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.LedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        #endregion

        #region ByNo_ApprovedVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort

        [Test]
        public async Task Journal_ByNo_ByRows_ApprovedVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_ApprovedVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.ByRows);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_ByRowsWithDetail_ApprovedVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_ApprovedVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.ByRowsWithDetail);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_ByLedger_ApprovedVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_ApprovedVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.ByLedger);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_BySubsidiary_ApprovedVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_ApprovedVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.BySubsidiary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_LedgerSummary_ApprovedVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_ApprovedVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.LedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        #endregion

        #region ByNo_AllVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort

        [Test]
        public async Task Journal_ByNo_ByRows_AllVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_AllVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.ByRows);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_ByRowsWithDetail_AllVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_AllVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.ByRowsWithDetail);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_ByLedger_AllVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_AllVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.ByLedger);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_BySubsidiary_AllVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_AllVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.BySubsidiary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_LedgerSummary_AllVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_AllVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.LedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        #endregion

        #region ByNo_CheckedVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort

        [Test]
        public async Task Journal_ByNo_ByRows_CheckedVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_CheckedVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.ByRows);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_ByRowsWithDetail_CheckedVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_CheckedVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.ByRowsWithDetail);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_ByLedger_CheckedVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_CheckedVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.ByLedger);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_BySubsidiary_CheckedVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_CheckedVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.BySubsidiary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_LedgerSummary_CheckedVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_CheckedVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.LedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        #endregion

        #region ByNo_FinalizedVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort

        [Test]
        public async Task Journal_ByNo_ByRows_FinalizedVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_FinalizedVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.ByRows);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_ByRowsWithDetail_FinalizedVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_FinalizedVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.ByRowsWithDetail);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_ByLedger_FinalizedVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_FinalizedVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.ByLedger);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_BySubsidiary_FinalizedVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_FinalizedVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.BySubsidiary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_LedgerSummary_FinalizedVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_FinalizedVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.LedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        #endregion

        #region ByNo_ConfirmedVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort

        [Test]
        public async Task Journal_ByNo_ByRows_ConfirmedVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_ConfirmedVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.ByRows);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_ByRowsWithDetail_ConfirmedVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_ConfirmedVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.ByRowsWithDetail);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_ByLedger_ConfirmedVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_ConfirmedVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.ByLedger);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_BySubsidiary_ConfirmedVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_ConfirmedVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.BySubsidiary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_LedgerSummary_ConfirmedVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_ConfirmedVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.LedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        #endregion

        #region ByNo_ApprovedVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort

        [Test]
        public async Task Journal_ByNo_ByRows_ApprovedVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_ApprovedVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.ByRows);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_ByRowsWithDetail_ApprovedVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_ApprovedVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.ByRowsWithDetail);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_ByLedger_ApprovedVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_ApprovedVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.ByLedger);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_BySubsidiary_ApprovedVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_ApprovedVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.BySubsidiary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_LedgerSummary_ApprovedVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_ApprovedVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.LedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        #endregion

        #region ByNo_AllVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort

        [Test]
        public async Task Journal_ByNo_ByRows_AllVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_AllVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.ByRows);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_ByRowsWithDetail_AllVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_AllVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.ByRowsWithDetail);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_ByLedger_AllVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_AllVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.ByLedger);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_BySubsidiary_AllVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_AllVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.BySubsidiary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_LedgerSummary_AllVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_AllVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.LedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        #endregion

        #region ByNo_CheckedVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort

        [Test]
        public async Task Journal_ByNo_ByRows_CheckedVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_CheckedVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.ByRows);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_ByRowsWithDetail_CheckedVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_CheckedVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.ByRowsWithDetail);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_ByLedger_CheckedVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_CheckedVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.ByLedger);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_BySubsidiary_CheckedVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_CheckedVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.BySubsidiary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_LedgerSummary_CheckedVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_CheckedVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.LedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        #endregion

        #region ByNo_FinalizedVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort

        [Test]
        public async Task Journal_ByNo_ByRows_FinalizedVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_FinalizedVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.ByRows);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_ByRowsWithDetail_FinalizedVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_FinalizedVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.ByRowsWithDetail);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_ByLedger_FinalizedVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_FinalizedVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.ByLedger);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_BySubsidiary_FinalizedVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_FinalizedVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.BySubsidiary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_LedgerSummary_FinalizedVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_FinalizedVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.LedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        #endregion

        #region ByNo_ConfirmedVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort

        [Test]
        public async Task Journal_ByNo_ByRows_ConfirmedVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_ConfirmedVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.ByRows);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_ByRowsWithDetail_ConfirmedVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_ConfirmedVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.ByRowsWithDetail);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_ByLedger_ConfirmedVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_ConfirmedVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.ByLedger);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_BySubsidiary_ConfirmedVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_ConfirmedVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.BySubsidiary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_LedgerSummary_ConfirmedVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_ConfirmedVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.LedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        #endregion

        #region ByNo_ApprovedVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort

        [Test]
        public async Task Journal_ByNo_ByRows_ApprovedVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_ApprovedVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.ByRows);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_ByRowsWithDetail_ApprovedVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_ApprovedVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.ByRowsWithDetail);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_ByLedger_ApprovedVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_ApprovedVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.ByLedger);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_BySubsidiary_ApprovedVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_ApprovedVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.BySubsidiary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_LedgerSummary_ApprovedVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_ApprovedVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.LedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        #endregion

        #region ByNo_AllVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort

        [Test]
        public async Task Journal_ByNo_ByRows_AllVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_AllVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.ByRows);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_ByRowsWithDetail_AllVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_AllVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.ByRowsWithDetail);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_ByLedger_AllVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_AllVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.ByLedger);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_BySubsidiary_AllVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_AllVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.BySubsidiary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_LedgerSummary_AllVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_AllVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
                JournalMode.LedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        #endregion

        #region ByNo_CheckedVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort

        [Test]
        public async Task Journal_ByNo_ByRows_CheckedVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_CheckedVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.ByRows);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_ByRowsWithDetail_CheckedVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_CheckedVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.ByRowsWithDetail);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_ByLedger_CheckedVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_CheckedVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.ByLedger);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_BySubsidiary_CheckedVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_CheckedVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.BySubsidiary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_LedgerSummary_CheckedVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_CheckedVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.LedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        #endregion

        #region ByNo_FinalizedVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort

        [Test]
        public async Task Journal_ByNo_ByRows_FinalizedVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_FinalizedVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.ByRows);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_ByRowsWithDetail_FinalizedVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_FinalizedVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.ByRowsWithDetail);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_ByLedger_FinalizedVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_FinalizedVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.ByLedger);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_BySubsidiary_FinalizedVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_FinalizedVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.BySubsidiary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_LedgerSummary_FinalizedVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_FinalizedVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.LedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        #endregion

        #region ByNo_ConfirmedVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort

        [Test]
        public async Task Journal_ByNo_ByRows_ConfirmedVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_ConfirmedVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.ByRows);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_ByRowsWithDetail_ConfirmedVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_ConfirmedVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.ByRowsWithDetail);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_ByLedger_ConfirmedVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_ConfirmedVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.ByLedger);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_BySubsidiary_ConfirmedVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_ConfirmedVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.BySubsidiary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_LedgerSummary_ConfirmedVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_ConfirmedVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.LedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        #endregion

        #region ByNo_ApprovedVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort

        [Test]
        public async Task Journal_ByNo_ByRows_ApprovedVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_ApprovedVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.ByRows);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_ByRowsWithDetail_ApprovedVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_ApprovedVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.ByRowsWithDetail);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_ByLedger_ApprovedVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_ApprovedVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.ByLedger);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_BySubsidiary_ApprovedVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_ApprovedVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.BySubsidiary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_LedgerSummary_ApprovedVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_ApprovedVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.LedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        #endregion

        #region ByNo_AllVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort

        [Test]
        public async Task Journal_ByNo_ByRows_AllVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_AllVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.ByRows);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_ByRowsWithDetail_AllVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_AllVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.ByRowsWithDetail);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_ByLedger_AllVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_AllVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.ByLedger);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_BySubsidiary_AllVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_AllVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.BySubsidiary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_LedgerSummary_AllVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_AllVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                JournalMode.LedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        #endregion

        #endregion

        #region Journal By No By Branch Tests

        #region ByNo_CheckedVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort

        [Test]
        public async Task Journal_ByNo_ByRows_CheckedVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_CheckedVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.ByRows);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_ByRowsWithDetail_CheckedVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_CheckedVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.ByRowsWithDetail);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_ByLedger_CheckedVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_CheckedVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.ByLedger);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_BySubsidiary_CheckedVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_CheckedVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.BySubsidiary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_LedgerSummary_CheckedVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_CheckedVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.LedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        #endregion

        #region ByNo_FinalizedVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort

        [Test]
        public async Task Journal_ByNo_ByRows_FinalizedVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_FinalizedVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.ByRows);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_ByRowsWithDetail_FinalizedVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_FinalizedVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.ByRowsWithDetail);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_ByLedger_FinalizedVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_FinalizedVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.ByLedger);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_BySubsidiary_FinalizedVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_FinalizedVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.BySubsidiary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_LedgerSummary_FinalizedVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_FinalizedVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.LedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        #endregion

        #region ByNo_ConfirmedVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort

        [Test]
        public async Task Journal_ByNo_ByRows_ConfirmedVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_ConfirmedVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.ByRows);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_ByRowsWithDetail_ConfirmedVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_ConfirmedVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.ByRowsWithDetail);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_ByLedger_ConfirmedVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_ConfirmedVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.ByLedger);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_BySubsidiary_ConfirmedVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_ConfirmedVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.BySubsidiary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_LedgerSummary_ConfirmedVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_ConfirmedVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.LedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        #endregion

        #region ByNo_ApprovedVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort

        [Test]
        public async Task Journal_ByNo_ByRows_ApprovedVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_ApprovedVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.ByRows);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_ByRowsWithDetail_ApprovedVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_ApprovedVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.ByRowsWithDetail);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_ByLedger_ApprovedVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_ApprovedVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.ByLedger);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_BySubsidiary_ApprovedVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_ApprovedVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.BySubsidiary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_LedgerSummary_ApprovedVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_ApprovedVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.LedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        #endregion

        #region ByNo_AllVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort

        [Test]
        public async Task Journal_ByNo_ByRows_AllVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_AllVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.ByRows);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_ByRowsWithDetail_AllVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_AllVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.ByRowsWithDetail);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_ByLedger_AllVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_AllVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.ByLedger);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_BySubsidiary_AllVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_AllVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.BySubsidiary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_LedgerSummary_AllVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_AllVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.LedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        #endregion

        #region ByNo_CheckedVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort

        [Test]
        public async Task Journal_ByNo_ByRows_CheckedVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_CheckedVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.ByRows);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_ByRowsWithDetail_CheckedVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_CheckedVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.ByRowsWithDetail);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_ByLedger_CheckedVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_CheckedVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.ByLedger);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_BySubsidiary_CheckedVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_CheckedVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.BySubsidiary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_LedgerSummary_CheckedVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_CheckedVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.LedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        #endregion

        #region ByNo_FinalizedVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort

        [Test]
        public async Task Journal_ByNo_ByRows_FinalizedVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_FinalizedVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.ByRows);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_ByRowsWithDetail_FinalizedVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_FinalizedVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.ByRowsWithDetail);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_ByLedger_FinalizedVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_FinalizedVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.ByLedger);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_BySubsidiary_FinalizedVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_FinalizedVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.BySubsidiary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_LedgerSummary_FinalizedVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_FinalizedVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.LedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        #endregion

        #region ByNo_ConfirmedVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort

        [Test]
        public async Task Journal_ByNo_ByRows_ConfirmedVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_ConfirmedVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.ByRows);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_ByRowsWithDetail_ConfirmedVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_ConfirmedVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.ByRowsWithDetail);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_ByLedger_ConfirmedVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_ConfirmedVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.ByLedger);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_BySubsidiary_ConfirmedVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_ConfirmedVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.BySubsidiary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_LedgerSummary_ConfirmedVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_ConfirmedVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.LedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        #endregion

        #region ByNo_ApprovedVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort

        [Test]
        public async Task Journal_ByNo_ByRows_ApprovedVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_ApprovedVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.ByRows);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_ByRowsWithDetail_ApprovedVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_ApprovedVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.ByRowsWithDetail);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_ByLedger_ApprovedVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_ApprovedVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.ByLedger);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_BySubsidiary_ApprovedVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_ApprovedVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.BySubsidiary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_LedgerSummary_ApprovedVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_ApprovedVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.LedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        #endregion

        #region ByNo_AllVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort

        [Test]
        public async Task Journal_ByNo_ByRows_AllVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_AllVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.ByRows);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_ByRowsWithDetail_AllVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_AllVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.ByRowsWithDetail);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_ByLedger_AllVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_AllVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.ByLedger);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_BySubsidiary_AllVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_AllVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.BySubsidiary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_LedgerSummary_AllVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_AllVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.LedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        #endregion

        #region ByNo_CheckedVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort

        [Test]
        public async Task Journal_ByNo_ByRows_CheckedVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_CheckedVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.ByRows);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_ByRowsWithDetail_CheckedVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_CheckedVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.ByRowsWithDetail);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_ByLedger_CheckedVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_CheckedVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.ByLedger);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_BySubsidiary_CheckedVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_CheckedVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.BySubsidiary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_LedgerSummary_CheckedVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_CheckedVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.LedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        #endregion

        #region ByNo_FinalizedVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort

        [Test]
        public async Task Journal_ByNo_ByRows_FinalizedVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_FinalizedVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.ByRows);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_ByRowsWithDetail_FinalizedVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_FinalizedVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.ByRowsWithDetail);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_ByLedger_FinalizedVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_FinalizedVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.ByLedger);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_BySubsidiary_FinalizedVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_FinalizedVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.BySubsidiary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_LedgerSummary_FinalizedVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_FinalizedVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.LedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        #endregion

        #region ByNo_ConfirmedVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort

        [Test]
        public async Task Journal_ByNo_ByRows_ConfirmedVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_ConfirmedVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.ByRows);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_ByRowsWithDetail_ConfirmedVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_ConfirmedVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.ByRowsWithDetail);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_ByLedger_ConfirmedVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_ConfirmedVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.ByLedger);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_BySubsidiary_ConfirmedVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_ConfirmedVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.BySubsidiary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_LedgerSummary_ConfirmedVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_ConfirmedVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.LedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        #endregion

        #region ByNo_ApprovedVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort

        [Test]
        public async Task Journal_ByNo_ByRows_ApprovedVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_ApprovedVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.ByRows);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_ByRowsWithDetail_ApprovedVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_ApprovedVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.ByRowsWithDetail);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_ByLedger_ApprovedVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_ApprovedVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.ByLedger);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_BySubsidiary_ApprovedVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_ApprovedVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.BySubsidiary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_LedgerSummary_ApprovedVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_ApprovedVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.LedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        #endregion

        #region ByNo_AllVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort

        [Test]
        public async Task Journal_ByNo_ByRows_AllVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_AllVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.ByRows);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_ByRowsWithDetail_AllVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_AllVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.ByRowsWithDetail);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_ByLedger_AllVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_AllVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.ByLedger);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_BySubsidiary_AllVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_AllVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.BySubsidiary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_LedgerSummary_AllVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_AllVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.LedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        #endregion

        #region ByNo_CheckedVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort

        [Test]
        public async Task Journal_ByNo_ByRows_CheckedVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_CheckedVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.ByRows);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_ByRowsWithDetail_CheckedVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_CheckedVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.ByRowsWithDetail);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_ByLedger_CheckedVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_CheckedVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.ByLedger);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_BySubsidiary_CheckedVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_CheckedVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.BySubsidiary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_LedgerSummary_CheckedVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_CheckedVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.LedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        #endregion

        #region ByNo_FinalizedVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort

        [Test]
        public async Task Journal_ByNo_ByRows_FinalizedVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_FinalizedVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.ByRows);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_ByRowsWithDetail_FinalizedVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_FinalizedVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.ByRowsWithDetail);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_ByLedger_FinalizedVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_FinalizedVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.ByLedger);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_BySubsidiary_FinalizedVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_FinalizedVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.BySubsidiary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_LedgerSummary_FinalizedVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_FinalizedVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.LedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        #endregion

        #region ByNo_ConfirmedVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort

        [Test]
        public async Task Journal_ByNo_ByRows_ConfirmedVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_ConfirmedVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.ByRows);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_ByRowsWithDetail_ConfirmedVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_ConfirmedVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.ByRowsWithDetail);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_ByLedger_ConfirmedVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_ConfirmedVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.ByLedger);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_BySubsidiary_ConfirmedVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_ConfirmedVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.BySubsidiary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_LedgerSummary_ConfirmedVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_ConfirmedVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.LedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        #endregion

        #region ByNo_ApprovedVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort

        [Test]
        public async Task Journal_ByNo_ByRows_ApprovedVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_ApprovedVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.ByRows);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_ByRowsWithDetail_ApprovedVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_ApprovedVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.ByRowsWithDetail);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_ByLedger_ApprovedVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_ApprovedVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.ByLedger);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_BySubsidiary_ApprovedVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_ApprovedVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.BySubsidiary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_LedgerSummary_ApprovedVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_ApprovedVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.LedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        #endregion

        #region ByNo_AllVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort

        [Test]
        public async Task Journal_ByNo_ByRows_AllVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_AllVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.ByRows);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_ByRowsWithDetail_AllVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_AllVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.ByRowsWithDetail);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_ByLedger_AllVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_AllVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.ByLedger);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_BySubsidiary_AllVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_AllVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.BySubsidiary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_LedgerSummary_AllVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_AllVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.LedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        #endregion

        #region ByNo_CheckedVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort

        [Test]
        public async Task Journal_ByNo_ByRows_CheckedVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_CheckedVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.ByRows);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_ByRowsWithDetail_CheckedVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_CheckedVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.ByRowsWithDetail);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_ByLedger_CheckedVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_CheckedVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.ByLedger);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_BySubsidiary_CheckedVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_CheckedVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.BySubsidiary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_LedgerSummary_CheckedVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_CheckedVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.LedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        #endregion

        #region ByNo_FinalizedVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort

        [Test]
        public async Task Journal_ByNo_ByRows_FinalizedVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_FinalizedVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.ByRows);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_ByRowsWithDetail_FinalizedVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_FinalizedVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.ByRowsWithDetail);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_ByLedger_FinalizedVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_FinalizedVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.ByLedger);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_BySubsidiary_FinalizedVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_FinalizedVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.BySubsidiary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_LedgerSummary_FinalizedVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_FinalizedVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.LedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        #endregion

        #region ByNo_ConfirmedVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort

        [Test]
        public async Task Journal_ByNo_ByRows_ConfirmedVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_ConfirmedVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.ByRows);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_ByRowsWithDetail_ConfirmedVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_ConfirmedVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.ByRowsWithDetail);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_ByLedger_ConfirmedVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_ConfirmedVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.ByLedger);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_BySubsidiary_ConfirmedVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_ConfirmedVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.BySubsidiary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_LedgerSummary_ConfirmedVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_ConfirmedVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.LedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        #endregion

        #region ByNo_ApprovedVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort

        [Test]
        public async Task Journal_ByNo_ByRows_ApprovedVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_ApprovedVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.ByRows);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_ByRowsWithDetail_ApprovedVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_ApprovedVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.ByRowsWithDetail);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_ByLedger_ApprovedVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_ApprovedVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.ByLedger);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_BySubsidiary_ApprovedVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_ApprovedVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.BySubsidiary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_LedgerSummary_ApprovedVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_ApprovedVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.LedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        #endregion

        #region ByNo_AllVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort

        [Test]
        public async Task Journal_ByNo_ByRows_AllVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_AllVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.ByRows);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_ByRowsWithDetail_AllVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_AllVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.ByRowsWithDetail);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_ByLedger_AllVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_AllVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.ByLedger);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_BySubsidiary_AllVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_AllVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.BySubsidiary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_LedgerSummary_AllVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_AllVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
                JournalMode.LedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        #endregion

        #region ByNo_CheckedVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort

        [Test]
        public async Task Journal_ByNo_ByRows_CheckedVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_CheckedVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.ByRows);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_ByRowsWithDetail_CheckedVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_CheckedVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.ByRowsWithDetail);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_ByLedger_CheckedVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_CheckedVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.ByLedger);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_BySubsidiary_CheckedVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_CheckedVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.BySubsidiary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_LedgerSummary_CheckedVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_CheckedVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.LedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        #endregion

        #region ByNo_FinalizedVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort

        [Test]
        public async Task Journal_ByNo_ByRows_FinalizedVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_FinalizedVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.ByRows);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_ByRowsWithDetail_FinalizedVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_FinalizedVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.ByRowsWithDetail);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_ByLedger_FinalizedVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_FinalizedVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.ByLedger);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_BySubsidiary_FinalizedVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_FinalizedVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.BySubsidiary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_LedgerSummary_FinalizedVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_FinalizedVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.LedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        #endregion

        #region ByNo_ConfirmedVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort

        [Test]
        public async Task Journal_ByNo_ByRows_ConfirmedVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_ConfirmedVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.ByRows);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_ByRowsWithDetail_ConfirmedVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_ConfirmedVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.ByRowsWithDetail);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_ByLedger_ConfirmedVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_ConfirmedVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.ByLedger);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_BySubsidiary_ConfirmedVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_ConfirmedVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.BySubsidiary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_LedgerSummary_ConfirmedVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_ConfirmedVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.LedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        #endregion

        #region ByNo_ApprovedVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort

        [Test]
        public async Task Journal_ByNo_ByRows_ApprovedVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_ApprovedVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.ByRows);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_ByRowsWithDetail_ApprovedVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_ApprovedVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.ByRowsWithDetail);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_ByLedger_ApprovedVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_ApprovedVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.ByLedger);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_BySubsidiary_ApprovedVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_ApprovedVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.BySubsidiary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_LedgerSummary_ApprovedVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_ApprovedVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.LedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        #endregion

        #region ByNo_AllVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort

        [Test]
        public async Task Journal_ByNo_ByRows_AllVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_AllVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.ByRows);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_ByRowsWithDetail_AllVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_AllVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.ByRowsWithDetail);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_ByLedger_AllVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_AllVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.ByLedger);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_BySubsidiary_AllVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_AllVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.BySubsidiary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        [Test]
        public async Task Journal_ByNo_LedgerSummary_AllVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var journal = await GetJournalByNo_AllVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                JournalMode.LedgerSummary);

            // Assert
            Assert.That(journal, Is.Not.Null);
        }

        #endregion

        #endregion

        private void SetUp()
        {
            var repoContext = GetRepositoryContext();
            var sysRepo = GetSystemRepository(repoContext);
            var utility = new ReportDirectUtility(repoContext);
            _repository = new JournalRepositoryDirect(repoContext, sysRepo, utility);
        }

        #region GetJournalByDate_AllArticles_CurrentBranch_NoGrouping_NoFilterSort

        private async Task<JournalViewModel> GetJournalByDate_CheckedVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort(
            JournalMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseCheckedVouchers()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions);

            return await _repository.GetJournalByDateAsync(parameters);
        }

        private async Task<JournalViewModel> GetJournalByDate_FinalizedVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort(
            JournalMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseFinalizedVouchers()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions);

            return await _repository.GetJournalByDateAsync(parameters);
        }

        private async Task<JournalViewModel> GetJournalByDate_ConfirmedVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort(
            JournalMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseConfirmedVouchers()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions);

            return await _repository.GetJournalByDateAsync(parameters);
        }

        private async Task<JournalViewModel> GetJournalByDate_ApprovedVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort(
            JournalMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseApprovedVouchers()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions);

            return await _repository.GetJournalByDateAsync(parameters);
        }

        private async Task<JournalViewModel> GetJournalByDate_AllVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort(
            JournalMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .Build();
            var parameters = GetTestParameters(mode, gridOptions);

            return await _repository.GetJournalByDateAsync(parameters);
        }

        #endregion

        #region GetJournalByDate_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort

        private async Task<JournalViewModel> GetJournalByDate_CheckedVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
            JournalMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .UseCheckedVouchers()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions);

            return await _repository.GetJournalByDateAsync(parameters);
        }

        private async Task<JournalViewModel> GetJournalByDate_FinalizedVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
            JournalMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .UseFinalizedVouchers()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions);

            return await _repository.GetJournalByDateAsync(parameters);
        }

        private async Task<JournalViewModel> GetJournalByDate_ConfirmedVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
            JournalMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .UseConfirmedVouchers()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions);

            return await _repository.GetJournalByDateAsync(parameters);
        }

        private async Task<JournalViewModel> GetJournalByDate_ApprovedVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
            JournalMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .UseApprovedVouchers()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions);

            return await _repository.GetJournalByDateAsync(parameters);
        }

        private async Task<JournalViewModel> GetJournalByDate_AllVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
            JournalMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions);

            return await _repository.GetJournalByDateAsync(parameters);
        }

        #endregion

        #region GetJournalByDate_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort

        private async Task<JournalViewModel> GetJournalByDate_CheckedVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
            JournalMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseCheckedVouchers()
                .UseMarkedLines()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions);

            return await _repository.GetJournalByDateAsync(parameters);
        }

        private async Task<JournalViewModel> GetJournalByDate_FinalizedVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
            JournalMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseFinalizedVouchers()
                .UseMarkedLines()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions);

            return await _repository.GetJournalByDateAsync(parameters);
        }

        private async Task<JournalViewModel> GetJournalByDate_ConfirmedVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
            JournalMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseConfirmedVouchers()
                .UseMarkedLines()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions);

            return await _repository.GetJournalByDateAsync(parameters);
        }

        private async Task<JournalViewModel> GetJournalByDate_ApprovedVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
            JournalMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseApprovedVouchers()
                .UseMarkedLines()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions);

            return await _repository.GetJournalByDateAsync(parameters);
        }

        private async Task<JournalViewModel> GetJournalByDate_AllVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
            JournalMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseMarkedLines()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions);

            return await _repository.GetJournalByDateAsync(parameters);
        }

        #endregion

        #region GetJournalByDate_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort

        private async Task<JournalViewModel> GetJournalByDate_CheckedVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
            JournalMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .UseCheckedVouchers()
                .UseMarkedLines()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions);

            return await _repository.GetJournalByDateAsync(parameters);
        }

        private async Task<JournalViewModel> GetJournalByDate_FinalizedVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
            JournalMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .UseFinalizedVouchers()
                .UseMarkedLines()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions);

            return await _repository.GetJournalByDateAsync(parameters);
        }

        private async Task<JournalViewModel> GetJournalByDate_ConfirmedVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
            JournalMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .UseConfirmedVouchers()
                .UseMarkedLines()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions);

            return await _repository.GetJournalByDateAsync(parameters);
        }

        private async Task<JournalViewModel> GetJournalByDate_ApprovedVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
            JournalMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .UseApprovedVouchers()
                .UseMarkedLines()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions);

            return await _repository.GetJournalByDateAsync(parameters);
        }

        private async Task<JournalViewModel> GetJournalByDate_AllVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
            JournalMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .UseMarkedLines()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions);

            return await _repository.GetJournalByDateAsync(parameters);
        }

        #endregion

        #region GetJournalByDate_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort

        private async Task<JournalViewModel> GetJournalByDate_CheckedVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
            JournalMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseCheckedVouchers()
                .UseUnmarkedLines()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions);

            return await _repository.GetJournalByDateAsync(parameters);
        }

        private async Task<JournalViewModel> GetJournalByDate_FinalizedVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
            JournalMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseFinalizedVouchers()
                .UseUnmarkedLines()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions);

            return await _repository.GetJournalByDateAsync(parameters);
        }

        private async Task<JournalViewModel> GetJournalByDate_ConfirmedVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
            JournalMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseConfirmedVouchers()
                .UseUnmarkedLines()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions);

            return await _repository.GetJournalByDateAsync(parameters);
        }

        private async Task<JournalViewModel> GetJournalByDate_ApprovedVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
            JournalMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseApprovedVouchers()
                .UseUnmarkedLines()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions);

            return await _repository.GetJournalByDateAsync(parameters);
        }

        private async Task<JournalViewModel> GetJournalByDate_AllVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
            JournalMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseUnmarkedLines()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions);

            return await _repository.GetJournalByDateAsync(parameters);
        }

        #endregion

        #region GetJournalByDate_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort

        private async Task<JournalViewModel> GetJournalByDate_CheckedVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
            JournalMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .UseCheckedVouchers()
                .UseUnmarkedLines()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions);

            return await _repository.GetJournalByDateAsync(parameters);
        }

        private async Task<JournalViewModel> GetJournalByDate_FinalizedVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
            JournalMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .UseFinalizedVouchers()
                .UseUnmarkedLines()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions);

            return await _repository.GetJournalByDateAsync(parameters);
        }

        private async Task<JournalViewModel> GetJournalByDate_ConfirmedVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
            JournalMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .UseConfirmedVouchers()
                .UseUnmarkedLines()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions);

            return await _repository.GetJournalByDateAsync(parameters);
        }

        private async Task<JournalViewModel> GetJournalByDate_ApprovedVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
            JournalMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .UseApprovedVouchers()
                .UseUnmarkedLines()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions);

            return await _repository.GetJournalByDateAsync(parameters);
        }

        private async Task<JournalViewModel> GetJournalByDate_AllVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
            JournalMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .UseUnmarkedLines()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions);

            return await _repository.GetJournalByDateAsync(parameters);
        }

        #endregion

        #region GetJournalByDate_AllArticles_CurrentBranch_ByBranch_NoFilterSort

        private async Task<JournalViewModel> GetJournalByDate_CheckedVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort(
            JournalMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseCheckedVouchers()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions);

            return await _repository.GetJournalByDateByBranchAsync(parameters);
        }

        private async Task<JournalViewModel> GetJournalByDate_FinalizedVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort(
            JournalMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseFinalizedVouchers()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions);

            return await _repository.GetJournalByDateByBranchAsync(parameters);
        }

        private async Task<JournalViewModel> GetJournalByDate_ConfirmedVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort(
            JournalMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseConfirmedVouchers()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions);

            return await _repository.GetJournalByDateByBranchAsync(parameters);
        }

        private async Task<JournalViewModel> GetJournalByDate_ApprovedVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort(
            JournalMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseApprovedVouchers()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions);

            return await _repository.GetJournalByDateByBranchAsync(parameters);
        }

        private async Task<JournalViewModel> GetJournalByDate_AllVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort(
            JournalMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .Build();
            var parameters = GetTestParameters(mode, gridOptions);

            return await _repository.GetJournalByDateByBranchAsync(parameters);
        }

        #endregion

        #region GetJournalByDate_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort

        private async Task<JournalViewModel> GetJournalByDate_CheckedVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort(
            JournalMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .UseCheckedVouchers()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions);

            return await _repository.GetJournalByDateByBranchAsync(parameters);
        }

        private async Task<JournalViewModel> GetJournalByDate_FinalizedVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort(
            JournalMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .UseFinalizedVouchers()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions);

            return await _repository.GetJournalByDateByBranchAsync(parameters);
        }

        private async Task<JournalViewModel> GetJournalByDate_ConfirmedVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort(
            JournalMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .UseConfirmedVouchers()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions);

            return await _repository.GetJournalByDateByBranchAsync(parameters);
        }

        private async Task<JournalViewModel> GetJournalByDate_ApprovedVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort(
            JournalMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .UseApprovedVouchers()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions);

            return await _repository.GetJournalByDateByBranchAsync(parameters);
        }

        private async Task<JournalViewModel> GetJournalByDate_AllVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort(
            JournalMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions);

            return await _repository.GetJournalByDateByBranchAsync(parameters);
        }

        #endregion

        #region GetJournalByDate_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort

        private async Task<JournalViewModel> GetJournalByDate_CheckedVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
            JournalMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseCheckedVouchers()
                .UseMarkedLines()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions);

            return await _repository.GetJournalByDateByBranchAsync(parameters);
        }

        private async Task<JournalViewModel> GetJournalByDate_FinalizedVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
            JournalMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseFinalizedVouchers()
                .UseMarkedLines()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions);

            return await _repository.GetJournalByDateByBranchAsync(parameters);
        }

        private async Task<JournalViewModel> GetJournalByDate_ConfirmedVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
            JournalMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseConfirmedVouchers()
                .UseMarkedLines()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions);

            return await _repository.GetJournalByDateByBranchAsync(parameters);
        }

        private async Task<JournalViewModel> GetJournalByDate_ApprovedVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
            JournalMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseApprovedVouchers()
                .UseMarkedLines()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions);

            return await _repository.GetJournalByDateByBranchAsync(parameters);
        }

        private async Task<JournalViewModel> GetJournalByDate_AllVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
            JournalMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseMarkedLines()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions);

            return await _repository.GetJournalByDateByBranchAsync(parameters);
        }

        #endregion

        #region GetJournalByDate_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort

        private async Task<JournalViewModel> GetJournalByDate_CheckedVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
            JournalMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .UseCheckedVouchers()
                .UseMarkedLines()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions);

            return await _repository.GetJournalByDateByBranchAsync(parameters);
        }

        private async Task<JournalViewModel> GetJournalByDate_FinalizedVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
            JournalMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .UseFinalizedVouchers()
                .UseMarkedLines()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions);

            return await _repository.GetJournalByDateByBranchAsync(parameters);
        }

        private async Task<JournalViewModel> GetJournalByDate_ConfirmedVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
            JournalMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .UseConfirmedVouchers()
                .UseMarkedLines()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions);

            return await _repository.GetJournalByDateByBranchAsync(parameters);
        }

        private async Task<JournalViewModel> GetJournalByDate_ApprovedVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
            JournalMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .UseApprovedVouchers()
                .UseMarkedLines()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions);

            return await _repository.GetJournalByDateByBranchAsync(parameters);
        }

        private async Task<JournalViewModel> GetJournalByDate_AllVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
            JournalMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .UseMarkedLines()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions);

            return await _repository.GetJournalByDateByBranchAsync(parameters);
        }

        #endregion

        #region GetJournalByDate_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort

        private async Task<JournalViewModel> GetJournalByDate_CheckedVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
            JournalMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseCheckedVouchers()
                .UseUnmarkedLines()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions);

            return await _repository.GetJournalByDateByBranchAsync(parameters);
        }

        private async Task<JournalViewModel> GetJournalByDate_FinalizedVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
            JournalMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseFinalizedVouchers()
                .UseUnmarkedLines()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions);

            return await _repository.GetJournalByDateByBranchAsync(parameters);
        }

        private async Task<JournalViewModel> GetJournalByDate_ConfirmedVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
            JournalMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseConfirmedVouchers()
                .UseUnmarkedLines()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions);

            return await _repository.GetJournalByDateByBranchAsync(parameters);
        }

        private async Task<JournalViewModel> GetJournalByDate_ApprovedVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
            JournalMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseApprovedVouchers()
                .UseUnmarkedLines()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions);

            return await _repository.GetJournalByDateByBranchAsync(parameters);
        }

        private async Task<JournalViewModel> GetJournalByDate_AllVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
            JournalMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseUnmarkedLines()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions);

            return await _repository.GetJournalByDateByBranchAsync(parameters);
        }

        #endregion

        #region GetJournalByDate_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort

        private async Task<JournalViewModel> GetJournalByDate_CheckedVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
            JournalMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .UseCheckedVouchers()
                .UseUnmarkedLines()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions);

            return await _repository.GetJournalByDateByBranchAsync(parameters);
        }

        private async Task<JournalViewModel> GetJournalByDate_FinalizedVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
            JournalMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .UseFinalizedVouchers()
                .UseUnmarkedLines()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions);

            return await _repository.GetJournalByDateByBranchAsync(parameters);
        }

        private async Task<JournalViewModel> GetJournalByDate_ConfirmedVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
            JournalMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .UseConfirmedVouchers()
                .UseUnmarkedLines()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions);

            return await _repository.GetJournalByDateByBranchAsync(parameters);
        }

        private async Task<JournalViewModel> GetJournalByDate_ApprovedVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
            JournalMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .UseApprovedVouchers()
                .UseUnmarkedLines()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions);

            return await _repository.GetJournalByDateByBranchAsync(parameters);
        }

        private async Task<JournalViewModel> GetJournalByDate_AllVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
            JournalMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .UseUnmarkedLines()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions);

            return await _repository.GetJournalByDateByBranchAsync(parameters);
        }

        #endregion

        #region GetJournalByNo_AllArticles_CurrentBranch_NoGrouping_NoFilterSort

        private async Task<JournalViewModel> GetJournalByNo_CheckedVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort(
            JournalMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseCheckedVouchers()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions);

            return await _repository.GetJournalByNoAsync(parameters);
        }

        private async Task<JournalViewModel> GetJournalByNo_FinalizedVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort(
            JournalMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseFinalizedVouchers()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions);

            return await _repository.GetJournalByNoAsync(parameters);
        }

        private async Task<JournalViewModel> GetJournalByNo_ConfirmedVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort(
            JournalMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseConfirmedVouchers()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions);

            return await _repository.GetJournalByNoAsync(parameters);
        }

        private async Task<JournalViewModel> GetJournalByNo_ApprovedVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort(
            JournalMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseApprovedVouchers()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions);

            return await _repository.GetJournalByNoAsync(parameters);
        }

        private async Task<JournalViewModel> GetJournalByNo_AllVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort(
            JournalMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .Build();
            var parameters = GetTestParameters(mode, gridOptions);

            return await _repository.GetJournalByNoAsync(parameters);
        }

        #endregion

        #region GetJournalByNo_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort

        private async Task<JournalViewModel> GetJournalByNo_CheckedVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
            JournalMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .UseCheckedVouchers()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions);

            return await _repository.GetJournalByNoAsync(parameters);
        }

        private async Task<JournalViewModel> GetJournalByNo_FinalizedVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
            JournalMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .UseFinalizedVouchers()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions);

            return await _repository.GetJournalByNoAsync(parameters);
        }

        private async Task<JournalViewModel> GetJournalByNo_ConfirmedVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
            JournalMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .UseConfirmedVouchers()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions);

            return await _repository.GetJournalByNoAsync(parameters);
        }

        private async Task<JournalViewModel> GetJournalByNo_ApprovedVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
            JournalMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .UseApprovedVouchers()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions);

            return await _repository.GetJournalByNoAsync(parameters);
        }

        private async Task<JournalViewModel> GetJournalByNo_AllVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
            JournalMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions);

            return await _repository.GetJournalByNoAsync(parameters);
        }

        #endregion

        #region GetJournalByNo_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort

        private async Task<JournalViewModel> GetJournalByNo_CheckedVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
            JournalMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseCheckedVouchers()
                .UseMarkedLines()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions);

            return await _repository.GetJournalByNoAsync(parameters);
        }

        private async Task<JournalViewModel> GetJournalByNo_FinalizedVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
            JournalMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseFinalizedVouchers()
                .UseMarkedLines()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions);

            return await _repository.GetJournalByNoAsync(parameters);
        }

        private async Task<JournalViewModel> GetJournalByNo_ConfirmedVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
            JournalMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseConfirmedVouchers()
                .UseMarkedLines()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions);

            return await _repository.GetJournalByNoAsync(parameters);
        }

        private async Task<JournalViewModel> GetJournalByNo_ApprovedVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
            JournalMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseApprovedVouchers()
                .UseMarkedLines()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions);

            return await _repository.GetJournalByNoAsync(parameters);
        }

        private async Task<JournalViewModel> GetJournalByNo_AllVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
            JournalMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseMarkedLines()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions);

            return await _repository.GetJournalByNoAsync(parameters);
        }

        #endregion

        #region GetJournalByNo_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort

        private async Task<JournalViewModel> GetJournalByNo_CheckedVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
            JournalMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .UseCheckedVouchers()
                .UseMarkedLines()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions);

            return await _repository.GetJournalByNoAsync(parameters);
        }

        private async Task<JournalViewModel> GetJournalByNo_FinalizedVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
            JournalMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .UseFinalizedVouchers()
                .UseMarkedLines()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions);

            return await _repository.GetJournalByNoAsync(parameters);
        }

        private async Task<JournalViewModel> GetJournalByNo_ConfirmedVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
            JournalMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .UseConfirmedVouchers()
                .UseMarkedLines()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions);

            return await _repository.GetJournalByNoAsync(parameters);
        }

        private async Task<JournalViewModel> GetJournalByNo_ApprovedVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
            JournalMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .UseApprovedVouchers()
                .UseMarkedLines()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions);

            return await _repository.GetJournalByNoAsync(parameters);
        }

        private async Task<JournalViewModel> GetJournalByNo_AllVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
            JournalMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .UseMarkedLines()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions);

            return await _repository.GetJournalByNoAsync(parameters);
        }

        #endregion

        #region GetJournalByNo_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort

        private async Task<JournalViewModel> GetJournalByNo_CheckedVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
            JournalMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseCheckedVouchers()
                .UseUnmarkedLines()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions);

            return await _repository.GetJournalByNoAsync(parameters);
        }

        private async Task<JournalViewModel> GetJournalByNo_FinalizedVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
            JournalMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseFinalizedVouchers()
                .UseUnmarkedLines()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions);

            return await _repository.GetJournalByNoAsync(parameters);
        }

        private async Task<JournalViewModel> GetJournalByNo_ConfirmedVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
            JournalMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseConfirmedVouchers()
                .UseUnmarkedLines()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions);

            return await _repository.GetJournalByNoAsync(parameters);
        }

        private async Task<JournalViewModel> GetJournalByNo_ApprovedVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
            JournalMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseApprovedVouchers()
                .UseUnmarkedLines()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions);

            return await _repository.GetJournalByNoAsync(parameters);
        }

        private async Task<JournalViewModel> GetJournalByNo_AllVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
            JournalMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseUnmarkedLines()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions);

            return await _repository.GetJournalByNoAsync(parameters);
        }

        #endregion

        #region GetJournalByNo_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort

        private async Task<JournalViewModel> GetJournalByNo_CheckedVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
            JournalMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .UseCheckedVouchers()
                .UseUnmarkedLines()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions);

            return await _repository.GetJournalByNoAsync(parameters);
        }

        private async Task<JournalViewModel> GetJournalByNo_FinalizedVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
            JournalMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .UseFinalizedVouchers()
                .UseUnmarkedLines()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions);

            return await _repository.GetJournalByNoAsync(parameters);
        }

        private async Task<JournalViewModel> GetJournalByNo_ConfirmedVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
            JournalMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .UseConfirmedVouchers()
                .UseUnmarkedLines()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions);

            return await _repository.GetJournalByNoAsync(parameters);
        }

        private async Task<JournalViewModel> GetJournalByNo_ApprovedVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
            JournalMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .UseApprovedVouchers()
                .UseUnmarkedLines()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions);

            return await _repository.GetJournalByNoAsync(parameters);
        }

        private async Task<JournalViewModel> GetJournalByNo_AllVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
            JournalMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .UseUnmarkedLines()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions);

            return await _repository.GetJournalByNoAsync(parameters);
        }

        #endregion

        #region GetJournalByNo_AllArticles_CurrentBranch_ByBranch_NoFilterSort

        private async Task<JournalViewModel> GetJournalByNo_CheckedVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort(
            JournalMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseCheckedVouchers()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions);

            return await _repository.GetJournalByNoByBranchAsync(parameters);
        }

        private async Task<JournalViewModel> GetJournalByNo_FinalizedVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort(
            JournalMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseFinalizedVouchers()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions);

            return await _repository.GetJournalByNoByBranchAsync(parameters);
        }

        private async Task<JournalViewModel> GetJournalByNo_ConfirmedVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort(
            JournalMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseConfirmedVouchers()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions);

            return await _repository.GetJournalByNoByBranchAsync(parameters);
        }

        private async Task<JournalViewModel> GetJournalByNo_ApprovedVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort(
            JournalMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseApprovedVouchers()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions);

            return await _repository.GetJournalByNoByBranchAsync(parameters);
        }

        private async Task<JournalViewModel> GetJournalByNo_AllVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort(
            JournalMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .Build();
            var parameters = GetTestParameters(mode, gridOptions);

            return await _repository.GetJournalByNoByBranchAsync(parameters);
        }

        #endregion

        #region GetJournalByNo_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort

        private async Task<JournalViewModel> GetJournalByNo_CheckedVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort(
            JournalMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .UseCheckedVouchers()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions);

            return await _repository.GetJournalByNoByBranchAsync(parameters);
        }

        private async Task<JournalViewModel> GetJournalByNo_FinalizedVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort(
            JournalMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .UseFinalizedVouchers()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions);

            return await _repository.GetJournalByNoByBranchAsync(parameters);
        }

        private async Task<JournalViewModel> GetJournalByNo_ConfirmedVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort(
            JournalMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .UseConfirmedVouchers()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions);

            return await _repository.GetJournalByNoByBranchAsync(parameters);
        }

        private async Task<JournalViewModel> GetJournalByNo_ApprovedVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort(
            JournalMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .UseApprovedVouchers()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions);

            return await _repository.GetJournalByNoByBranchAsync(parameters);
        }

        private async Task<JournalViewModel> GetJournalByNo_AllVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort(
            JournalMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions);

            return await _repository.GetJournalByNoByBranchAsync(parameters);
        }

        #endregion

        #region GetJournalByNo_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort

        private async Task<JournalViewModel> GetJournalByNo_CheckedVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
            JournalMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseCheckedVouchers()
                .UseMarkedLines()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions);

            return await _repository.GetJournalByNoByBranchAsync(parameters);
        }

        private async Task<JournalViewModel> GetJournalByNo_FinalizedVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
            JournalMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseFinalizedVouchers()
                .UseMarkedLines()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions);

            return await _repository.GetJournalByNoByBranchAsync(parameters);
        }

        private async Task<JournalViewModel> GetJournalByNo_ConfirmedVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
            JournalMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseConfirmedVouchers()
                .UseMarkedLines()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions);

            return await _repository.GetJournalByNoByBranchAsync(parameters);
        }

        private async Task<JournalViewModel> GetJournalByNo_ApprovedVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
            JournalMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseApprovedVouchers()
                .UseMarkedLines()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions);

            return await _repository.GetJournalByNoByBranchAsync(parameters);
        }

        private async Task<JournalViewModel> GetJournalByNo_AllVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
            JournalMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseMarkedLines()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions);

            return await _repository.GetJournalByNoByBranchAsync(parameters);
        }

        #endregion

        #region GetJournalByNo_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort

        private async Task<JournalViewModel> GetJournalByNo_CheckedVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
            JournalMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .UseCheckedVouchers()
                .UseMarkedLines()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions);

            return await _repository.GetJournalByNoByBranchAsync(parameters);
        }

        private async Task<JournalViewModel> GetJournalByNo_FinalizedVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
            JournalMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .UseFinalizedVouchers()
                .UseMarkedLines()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions);

            return await _repository.GetJournalByNoByBranchAsync(parameters);
        }

        private async Task<JournalViewModel> GetJournalByNo_ConfirmedVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
            JournalMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .UseConfirmedVouchers()
                .UseMarkedLines()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions);

            return await _repository.GetJournalByNoByBranchAsync(parameters);
        }

        private async Task<JournalViewModel> GetJournalByNo_ApprovedVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
            JournalMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .UseApprovedVouchers()
                .UseMarkedLines()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions);

            return await _repository.GetJournalByNoByBranchAsync(parameters);
        }

        private async Task<JournalViewModel> GetJournalByNo_AllVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
            JournalMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .UseMarkedLines()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions);

            return await _repository.GetJournalByNoByBranchAsync(parameters);
        }

        #endregion

        #region GetJournalByNo_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort

        private async Task<JournalViewModel> GetJournalByNo_CheckedVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
            JournalMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseCheckedVouchers()
                .UseUnmarkedLines()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions);

            return await _repository.GetJournalByNoByBranchAsync(parameters);
        }

        private async Task<JournalViewModel> GetJournalByNo_FinalizedVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
            JournalMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseFinalizedVouchers()
                .UseUnmarkedLines()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions);

            return await _repository.GetJournalByNoByBranchAsync(parameters);
        }

        private async Task<JournalViewModel> GetJournalByNo_ConfirmedVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
            JournalMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseConfirmedVouchers()
                .UseUnmarkedLines()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions);

            return await _repository.GetJournalByNoByBranchAsync(parameters);
        }

        private async Task<JournalViewModel> GetJournalByNo_ApprovedVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
            JournalMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseApprovedVouchers()
                .UseUnmarkedLines()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions);

            return await _repository.GetJournalByNoByBranchAsync(parameters);
        }

        private async Task<JournalViewModel> GetJournalByNo_AllVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
            JournalMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseUnmarkedLines()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions);

            return await _repository.GetJournalByNoByBranchAsync(parameters);
        }

        #endregion

        #region GetJournalByNo_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort

        private async Task<JournalViewModel> GetJournalByNo_CheckedVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
            JournalMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .UseCheckedVouchers()
                .UseUnmarkedLines()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions);

            return await _repository.GetJournalByNoByBranchAsync(parameters);
        }

        private async Task<JournalViewModel> GetJournalByNo_FinalizedVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
            JournalMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .UseFinalizedVouchers()
                .UseUnmarkedLines()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions);

            return await _repository.GetJournalByNoByBranchAsync(parameters);
        }

        private async Task<JournalViewModel> GetJournalByNo_ConfirmedVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
            JournalMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .UseConfirmedVouchers()
                .UseUnmarkedLines()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions);

            return await _repository.GetJournalByNoByBranchAsync(parameters);
        }

        private async Task<JournalViewModel> GetJournalByNo_ApprovedVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
            JournalMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .UseApprovedVouchers()
                .UseUnmarkedLines()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions);

            return await _repository.GetJournalByNoByBranchAsync(parameters);
        }

        private async Task<JournalViewModel> GetJournalByNo_AllVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
            JournalMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .UseUnmarkedLines()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions);

            return await _repository.GetJournalByNoByBranchAsync(parameters);
        }

        #endregion

        private JournalParameters GetTestParameters(JournalMode mode, GridOptions gridOptions)
        {
            return new JournalParameters()
            {
                FromDate = DateTime.Parse("2018-03-21"),
                ToDate = DateTime.Parse("2019-03-20"),
                FromNo = 1,
                ToNo = 1000,
                Mode = mode,
                GridOptions = gridOptions
            };
        }

        private IJournalRepository _repository;
    }
}
