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
    public class AccountBookRepositoryTests : RepositoryTestBase
    {
        [OneTimeSetUp]
        public void FixtureSetup()
        {
            SetUp();
        }

        #region Account Book Tests

        #region CheckedVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort

        [Test]
        public async Task Book_ByRows_CheckedVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_CheckedVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort(
                AccountBookMode.ByRows);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_VoucherSum_CheckedVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_CheckedVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort(
                AccountBookMode.VoucherSum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_DailySum_CheckedVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_CheckedVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort(
                AccountBookMode.DailySum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_MonthlySum_CheckedVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_CheckedVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort(
                AccountBookMode.MonthlySum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        #endregion

        #region FinalizedVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort

        [Test]
        public async Task Book_ByRows_FinalizedVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_FinalizedVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort(
                AccountBookMode.ByRows);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_VoucherSum_FinalizedVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_FinalizedVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort(
                AccountBookMode.VoucherSum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_DailySum_FinalizedVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_FinalizedVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort(
                AccountBookMode.DailySum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_MonthlySum_FinalizedVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_FinalizedVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort(
                AccountBookMode.MonthlySum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        #endregion

        #region ConfirmedVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort

        [Test]
        public async Task Book_ByRows_ConfirmedVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_ConfirmedVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort(
                AccountBookMode.ByRows);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_VoucherSum_ConfirmedVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_ConfirmedVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort(
                AccountBookMode.VoucherSum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_DailySum_ConfirmedVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_ConfirmedVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort(
                AccountBookMode.DailySum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_MonthlySum_ConfirmedVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_ConfirmedVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort(
                AccountBookMode.MonthlySum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        #endregion

        #region ApprovedVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort

        [Test]
        public async Task Book_ByRows_ApprovedVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_ApprovedVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort(
                AccountBookMode.ByRows);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_VoucherSum_ApprovedVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_ApprovedVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort(
                AccountBookMode.VoucherSum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_DailySum_ApprovedVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_ApprovedVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort(
                AccountBookMode.DailySum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_MonthlySum_ApprovedVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_ApprovedVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort(
                AccountBookMode.MonthlySum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        #endregion

        #region AllVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort

        [Test]
        public async Task Book_ByRows_AllVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_AllVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort(
                AccountBookMode.ByRows);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_VoucherSum_AllVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_AllVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort(
                AccountBookMode.VoucherSum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_DailySum_AllVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_AllVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort(
                AccountBookMode.DailySum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_MonthlySum_AllVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_AllVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort(
                AccountBookMode.MonthlySum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        #endregion

        #region CheckedVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort

        [Test]
        public async Task Book_ByRows_CheckedVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_CheckedVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
                AccountBookMode.ByRows);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_VoucherSum_CheckedVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_CheckedVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
                AccountBookMode.VoucherSum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_DailySum_CheckedVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_CheckedVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
                AccountBookMode.DailySum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_MonthlySum_CheckedVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_CheckedVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
                AccountBookMode.MonthlySum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        #endregion

        #region FinalizedVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort

        [Test]
        public async Task Book_ByRows_FinalizedVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_FinalizedVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
                AccountBookMode.ByRows);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_VoucherSum_FinalizedVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_FinalizedVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
                AccountBookMode.VoucherSum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_DailySum_FinalizedVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_FinalizedVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
                AccountBookMode.DailySum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_MonthlySum_FinalizedVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_FinalizedVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
                AccountBookMode.MonthlySum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        #endregion

        #region ConfirmedVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort

        [Test]
        public async Task Book_ByRows_ConfirmedVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_ConfirmedVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
                AccountBookMode.ByRows);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_VoucherSum_ConfirmedVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_ConfirmedVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
                AccountBookMode.VoucherSum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_DailySum_ConfirmedVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_ConfirmedVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
                AccountBookMode.DailySum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_MonthlySum_ConfirmedVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_ConfirmedVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
                AccountBookMode.MonthlySum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        #endregion

        #region ApprovedVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort

        [Test]
        public async Task Book_ByRows_ApprovedVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_ApprovedVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
                AccountBookMode.ByRows);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_VoucherSum_ApprovedVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_ApprovedVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
                AccountBookMode.VoucherSum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_DailySum_ApprovedVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_ApprovedVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
                AccountBookMode.DailySum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_MonthlySum_ApprovedVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_ApprovedVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
                AccountBookMode.MonthlySum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        #endregion

        #region AllVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort

        [Test]
        public async Task Book_ByRows_AllVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_AllVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
                AccountBookMode.ByRows);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_VoucherSum_AllVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_AllVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
                AccountBookMode.VoucherSum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_DailySum_AllVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_AllVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
                AccountBookMode.DailySum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_MonthlySum_AllVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_AllVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
                AccountBookMode.MonthlySum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        #endregion

        #region CheckedVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort

        [Test]
        public async Task Book_ByRows_CheckedVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_CheckedVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
                AccountBookMode.ByRows);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_VoucherSum_CheckedVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_CheckedVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
                AccountBookMode.VoucherSum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_DailySum_CheckedVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_CheckedVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
                AccountBookMode.DailySum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_MonthlySum_CheckedVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_CheckedVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
                AccountBookMode.MonthlySum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        #endregion

        #region FinalizedVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort

        [Test]
        public async Task Book_ByRows_FinalizedVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_FinalizedVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
                AccountBookMode.ByRows);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_VoucherSum_FinalizedVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_FinalizedVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
                AccountBookMode.VoucherSum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_DailySum_FinalizedVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_FinalizedVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
                AccountBookMode.DailySum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_MonthlySum_FinalizedVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_FinalizedVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
                AccountBookMode.MonthlySum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        #endregion

        #region ConfirmedVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort

        [Test]
        public async Task Book_ByRows_ConfirmedVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_ConfirmedVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
                AccountBookMode.ByRows);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_VoucherSum_ConfirmedVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_ConfirmedVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
                AccountBookMode.VoucherSum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_DailySum_ConfirmedVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_ConfirmedVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
                AccountBookMode.DailySum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_MonthlySum_ConfirmedVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_ConfirmedVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
                AccountBookMode.MonthlySum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        #endregion

        #region ApprovedVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort

        [Test]
        public async Task Book_ByRows_ApprovedVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_ApprovedVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
                AccountBookMode.ByRows);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_VoucherSum_ApprovedVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_ApprovedVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
                AccountBookMode.VoucherSum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_DailySum_ApprovedVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_ApprovedVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
                AccountBookMode.DailySum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_MonthlySum_ApprovedVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_ApprovedVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
                AccountBookMode.MonthlySum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        #endregion

        #region AllVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort

        [Test]
        public async Task Book_ByRows_AllVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_AllVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
                AccountBookMode.ByRows);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_VoucherSum_AllVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_AllVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
                AccountBookMode.VoucherSum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_DailySum_AllVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_AllVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
                AccountBookMode.DailySum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_MonthlySum_AllVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_AllVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
                AccountBookMode.MonthlySum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        #endregion

        #region CheckedVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort

        [Test]
        public async Task Book_ByRows_CheckedVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_CheckedVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                AccountBookMode.ByRows);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_VoucherSum_CheckedVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_CheckedVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                AccountBookMode.VoucherSum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_DailySum_CheckedVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_CheckedVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                AccountBookMode.DailySum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_MonthlySum_CheckedVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_CheckedVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                AccountBookMode.MonthlySum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        #endregion

        #region FinalizedVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort

        [Test]
        public async Task Book_ByRows_FinalizedVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_FinalizedVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                AccountBookMode.ByRows);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_VoucherSum_FinalizedVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_FinalizedVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                AccountBookMode.VoucherSum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_DailySum_FinalizedVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_FinalizedVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                AccountBookMode.DailySum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_MonthlySum_FinalizedVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_FinalizedVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                AccountBookMode.MonthlySum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        #endregion

        #region ConfirmedVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort

        [Test]
        public async Task Book_ByRows_ConfirmedVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_ConfirmedVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                AccountBookMode.ByRows);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_VoucherSum_ConfirmedVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_ConfirmedVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                AccountBookMode.VoucherSum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_DailySum_ConfirmedVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_ConfirmedVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                AccountBookMode.DailySum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_MonthlySum_ConfirmedVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_ConfirmedVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                AccountBookMode.MonthlySum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        #endregion

        #region ApprovedVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort

        [Test]
        public async Task Book_ByRows_ApprovedVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_ApprovedVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                AccountBookMode.ByRows);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_VoucherSum_ApprovedVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_ApprovedVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                AccountBookMode.VoucherSum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_DailySum_ApprovedVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_ApprovedVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                AccountBookMode.DailySum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_MonthlySum_ApprovedVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_ApprovedVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                AccountBookMode.MonthlySum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        #endregion

        #region AllVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort

        [Test]
        public async Task Book_ByRows_AllVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_AllVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                AccountBookMode.ByRows);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_VoucherSum_AllVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_AllVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                AccountBookMode.VoucherSum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_DailySum_AllVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_AllVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                AccountBookMode.DailySum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_MonthlySum_AllVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_AllVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                AccountBookMode.MonthlySum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        #endregion

        #region CheckedVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort

        [Test]
        public async Task Book_ByRows_CheckedVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_CheckedVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                AccountBookMode.ByRows);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_VoucherSum_CheckedVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_CheckedVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                AccountBookMode.VoucherSum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_DailySum_CheckedVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_CheckedVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                AccountBookMode.DailySum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_MonthlySum_CheckedVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_CheckedVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                AccountBookMode.MonthlySum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        #endregion

        #region FinalizedVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort

        [Test]
        public async Task Book_ByRows_FinalizedVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_FinalizedVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                AccountBookMode.ByRows);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_VoucherSum_FinalizedVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_FinalizedVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                AccountBookMode.VoucherSum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_DailySum_FinalizedVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_FinalizedVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                AccountBookMode.DailySum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_MonthlySum_FinalizedVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_FinalizedVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                AccountBookMode.MonthlySum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        #endregion

        #region ConfirmedVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort

        [Test]
        public async Task Book_ByRows_ConfirmedVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_ConfirmedVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                AccountBookMode.ByRows);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_VoucherSum_ConfirmedVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_ConfirmedVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                AccountBookMode.VoucherSum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_DailySum_ConfirmedVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_ConfirmedVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                AccountBookMode.DailySum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_MonthlySum_ConfirmedVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_ConfirmedVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                AccountBookMode.MonthlySum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        #endregion

        #region ApprovedVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort

        [Test]
        public async Task Book_ByRows_ApprovedVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_ApprovedVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                AccountBookMode.ByRows);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_VoucherSum_ApprovedVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_ApprovedVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                AccountBookMode.VoucherSum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_DailySum_ApprovedVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_ApprovedVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                AccountBookMode.DailySum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_MonthlySum_ApprovedVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_ApprovedVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                AccountBookMode.MonthlySum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        #endregion

        #region AllVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort

        [Test]
        public async Task Book_ByRows_AllVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_AllVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                AccountBookMode.ByRows);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_VoucherSum_AllVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_AllVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                AccountBookMode.VoucherSum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_DailySum_AllVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_AllVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                AccountBookMode.DailySum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_MonthlySum_AllVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_AllVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                AccountBookMode.MonthlySum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        #endregion

        #region CheckedVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort

        [Test]
        public async Task Book_ByRows_CheckedVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_CheckedVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                AccountBookMode.ByRows);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_VoucherSum_CheckedVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_CheckedVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                AccountBookMode.VoucherSum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_DailySum_CheckedVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_CheckedVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                AccountBookMode.DailySum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_MonthlySum_CheckedVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_CheckedVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                AccountBookMode.MonthlySum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        #endregion

        #region FinalizedVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort

        [Test]
        public async Task Book_ByRows_FinalizedVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_FinalizedVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                AccountBookMode.ByRows);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_VoucherSum_FinalizedVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_FinalizedVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                AccountBookMode.VoucherSum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_DailySum_FinalizedVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_FinalizedVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                AccountBookMode.DailySum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_MonthlySum_FinalizedVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_FinalizedVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                AccountBookMode.MonthlySum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        #endregion

        #region ConfirmedVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort

        [Test]
        public async Task Book_ByRows_ConfirmedVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_ConfirmedVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                AccountBookMode.ByRows);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_VoucherSum_ConfirmedVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_ConfirmedVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                AccountBookMode.VoucherSum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_DailySum_ConfirmedVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_ConfirmedVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                AccountBookMode.DailySum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_MonthlySum_ConfirmedVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_ConfirmedVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                AccountBookMode.MonthlySum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        #endregion

        #region ApprovedVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort

        [Test]
        public async Task Book_ByRows_ApprovedVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_ApprovedVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                AccountBookMode.ByRows);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_VoucherSum_ApprovedVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_ApprovedVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                AccountBookMode.VoucherSum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_DailySum_ApprovedVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_ApprovedVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                AccountBookMode.DailySum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_MonthlySum_ApprovedVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_ApprovedVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                AccountBookMode.MonthlySum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        #endregion

        #region AllVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort

        [Test]
        public async Task Book_ByRows_AllVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_AllVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                AccountBookMode.ByRows);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_VoucherSum_AllVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_AllVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                AccountBookMode.VoucherSum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_DailySum_AllVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_AllVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                AccountBookMode.DailySum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_MonthlySum_AllVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_AllVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
                AccountBookMode.MonthlySum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        #endregion

        #endregion

        #region Account Book By Branch Tests

        #region CheckedVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort

        [Test]
        public async Task Book_ByRows_CheckedVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_CheckedVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort(
                AccountBookMode.ByRows);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_VoucherSum_CheckedVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_CheckedVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort(
                AccountBookMode.VoucherSum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_DailySum_CheckedVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_CheckedVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort(
                AccountBookMode.DailySum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_MonthlySum_CheckedVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_CheckedVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort(
                AccountBookMode.MonthlySum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        #endregion

        #region FinalizedVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort

        [Test]
        public async Task Book_ByRows_FinalizedVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_FinalizedVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort(
                AccountBookMode.ByRows);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_VoucherSum_FinalizedVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_FinalizedVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort(
                AccountBookMode.VoucherSum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_DailySum_FinalizedVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_FinalizedVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort(
                AccountBookMode.DailySum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_MonthlySum_FinalizedVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_FinalizedVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort(
                AccountBookMode.MonthlySum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        #endregion

        #region ConfirmedVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort

        [Test]
        public async Task Book_ByRows_ConfirmedVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_ConfirmedVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort(
                AccountBookMode.ByRows);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_VoucherSum_ConfirmedVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_ConfirmedVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort(
                AccountBookMode.VoucherSum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_DailySum_ConfirmedVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_ConfirmedVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort(
                AccountBookMode.DailySum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_MonthlySum_ConfirmedVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_ConfirmedVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort(
                AccountBookMode.MonthlySum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        #endregion

        #region ApprovedVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort

        [Test]
        public async Task Book_ByRows_ApprovedVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_ApprovedVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort(
                AccountBookMode.ByRows);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_VoucherSum_ApprovedVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_ApprovedVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort(
                AccountBookMode.VoucherSum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_DailySum_ApprovedVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_ApprovedVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort(
                AccountBookMode.DailySum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_MonthlySum_ApprovedVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_ApprovedVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort(
                AccountBookMode.MonthlySum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        #endregion

        #region AllVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort

        [Test]
        public async Task Book_ByRows_AllVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_AllVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort(
                AccountBookMode.ByRows);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_VoucherSum_AllVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_AllVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort(
                AccountBookMode.VoucherSum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_DailySum_AllVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_AllVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort(
                AccountBookMode.DailySum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_MonthlySum_AllVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_AllVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort(
                AccountBookMode.MonthlySum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        #endregion

        #region CheckedVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort

        [Test]
        public async Task Book_ByRows_CheckedVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_CheckedVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
                AccountBookMode.ByRows);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_VoucherSum_CheckedVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_CheckedVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
                AccountBookMode.VoucherSum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_DailySum_CheckedVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_CheckedVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
                AccountBookMode.DailySum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_MonthlySum_CheckedVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_CheckedVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
                AccountBookMode.MonthlySum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        #endregion

        #region FinalizedVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort

        [Test]
        public async Task Book_ByRows_FinalizedVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_FinalizedVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
                AccountBookMode.ByRows);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_VoucherSum_FinalizedVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_FinalizedVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
                AccountBookMode.VoucherSum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_DailySum_FinalizedVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_FinalizedVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
                AccountBookMode.DailySum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_MonthlySum_FinalizedVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_FinalizedVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
                AccountBookMode.MonthlySum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        #endregion

        #region ConfirmedVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort

        [Test]
        public async Task Book_ByRows_ConfirmedVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_ConfirmedVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
                AccountBookMode.ByRows);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_VoucherSum_ConfirmedVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_ConfirmedVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
                AccountBookMode.VoucherSum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_DailySum_ConfirmedVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_ConfirmedVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
                AccountBookMode.DailySum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_MonthlySum_ConfirmedVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_ConfirmedVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
                AccountBookMode.MonthlySum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        #endregion

        #region ApprovedVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort

        [Test]
        public async Task Book_ByRows_ApprovedVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_ApprovedVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
                AccountBookMode.ByRows);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_VoucherSum_ApprovedVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_ApprovedVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
                AccountBookMode.VoucherSum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_DailySum_ApprovedVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_ApprovedVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
                AccountBookMode.DailySum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_MonthlySum_ApprovedVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_ApprovedVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
                AccountBookMode.MonthlySum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        #endregion

        #region AllVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort

        [Test]
        public async Task Book_ByRows_AllVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_AllVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
                AccountBookMode.ByRows);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_VoucherSum_AllVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_AllVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
                AccountBookMode.VoucherSum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_DailySum_AllVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_AllVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
                AccountBookMode.DailySum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_MonthlySum_AllVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_AllVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
                AccountBookMode.MonthlySum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        #endregion

        #region CheckedVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort

        [Test]
        public async Task Book_ByRows_CheckedVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_CheckedVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
                AccountBookMode.ByRows);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_VoucherSum_CheckedVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_CheckedVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
                AccountBookMode.VoucherSum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_DailySum_CheckedVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_CheckedVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
                AccountBookMode.DailySum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_MonthlySum_CheckedVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_CheckedVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
                AccountBookMode.MonthlySum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        #endregion

        #region FinalizedVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort

        [Test]
        public async Task Book_ByRows_FinalizedVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_FinalizedVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
                AccountBookMode.ByRows);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_VoucherSum_FinalizedVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_FinalizedVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
                AccountBookMode.VoucherSum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_DailySum_FinalizedVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_FinalizedVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
                AccountBookMode.DailySum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_MonthlySum_FinalizedVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_FinalizedVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
                AccountBookMode.MonthlySum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        #endregion

        #region ConfirmedVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort

        [Test]
        public async Task Book_ByRows_ConfirmedVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_ConfirmedVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
                AccountBookMode.ByRows);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_VoucherSum_ConfirmedVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_ConfirmedVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
                AccountBookMode.VoucherSum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_DailySum_ConfirmedVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_ConfirmedVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
                AccountBookMode.DailySum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_MonthlySum_ConfirmedVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_ConfirmedVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
                AccountBookMode.MonthlySum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        #endregion

        #region ApprovedVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort

        [Test]
        public async Task Book_ByRows_ApprovedVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_ApprovedVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
                AccountBookMode.ByRows);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_VoucherSum_ApprovedVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_ApprovedVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
                AccountBookMode.VoucherSum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_DailySum_ApprovedVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_ApprovedVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
                AccountBookMode.DailySum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_MonthlySum_ApprovedVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_ApprovedVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
                AccountBookMode.MonthlySum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        #endregion

        #region AllVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort

        [Test]
        public async Task Book_ByRows_AllVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_AllVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
                AccountBookMode.ByRows);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_VoucherSum_AllVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_AllVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
                AccountBookMode.VoucherSum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_DailySum_AllVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_AllVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
                AccountBookMode.DailySum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_MonthlySum_AllVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_AllVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
                AccountBookMode.MonthlySum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        #endregion

        #region CheckedVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort

        [Test]
        public async Task Book_ByRows_CheckedVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_CheckedVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                AccountBookMode.ByRows);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_VoucherSum_CheckedVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_CheckedVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                AccountBookMode.VoucherSum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_DailySum_CheckedVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_CheckedVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                AccountBookMode.DailySum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_MonthlySum_CheckedVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_CheckedVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                AccountBookMode.MonthlySum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        #endregion

        #region FinalizedVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort

        [Test]
        public async Task Book_ByRows_FinalizedVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_FinalizedVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                AccountBookMode.ByRows);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_VoucherSum_FinalizedVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_FinalizedVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                AccountBookMode.VoucherSum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_DailySum_FinalizedVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_FinalizedVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                AccountBookMode.DailySum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_MonthlySum_FinalizedVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_FinalizedVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                AccountBookMode.MonthlySum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        #endregion

        #region ConfirmedVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort

        [Test]
        public async Task Book_ByRows_ConfirmedVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_ConfirmedVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                AccountBookMode.ByRows);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_VoucherSum_ConfirmedVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_ConfirmedVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                AccountBookMode.VoucherSum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_DailySum_ConfirmedVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_ConfirmedVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                AccountBookMode.DailySum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_MonthlySum_ConfirmedVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_ConfirmedVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                AccountBookMode.MonthlySum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        #endregion

        #region ApprovedVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort

        [Test]
        public async Task Book_ByRows_ApprovedVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_ApprovedVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                AccountBookMode.ByRows);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_VoucherSum_ApprovedVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_ApprovedVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                AccountBookMode.VoucherSum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_DailySum_ApprovedVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_ApprovedVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                AccountBookMode.DailySum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_MonthlySum_ApprovedVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_ApprovedVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                AccountBookMode.MonthlySum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        #endregion

        #region AllVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort

        [Test]
        public async Task Book_ByRows_AllVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_AllVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                AccountBookMode.ByRows);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_VoucherSum_AllVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_AllVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                AccountBookMode.VoucherSum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_DailySum_AllVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_AllVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                AccountBookMode.DailySum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_MonthlySum_AllVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_AllVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                AccountBookMode.MonthlySum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        #endregion

        #region CheckedVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort

        [Test]
        public async Task Book_ByRows_CheckedVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_CheckedVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                AccountBookMode.ByRows);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_VoucherSum_CheckedVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_CheckedVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                AccountBookMode.VoucherSum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_DailySum_CheckedVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_CheckedVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                AccountBookMode.DailySum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_MonthlySum_CheckedVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_CheckedVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                AccountBookMode.MonthlySum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        #endregion

        #region FinalizedVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort

        [Test]
        public async Task Book_ByRows_FinalizedVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_FinalizedVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                AccountBookMode.ByRows);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_VoucherSum_FinalizedVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_FinalizedVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                AccountBookMode.VoucherSum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_DailySum_FinalizedVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_FinalizedVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                AccountBookMode.DailySum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_MonthlySum_FinalizedVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_FinalizedVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                AccountBookMode.MonthlySum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        #endregion

        #region ConfirmedVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort

        [Test]
        public async Task Book_ByRows_ConfirmedVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_ConfirmedVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                AccountBookMode.ByRows);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_VoucherSum_ConfirmedVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_ConfirmedVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                AccountBookMode.VoucherSum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_DailySum_ConfirmedVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_ConfirmedVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                AccountBookMode.DailySum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_MonthlySum_ConfirmedVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_ConfirmedVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                AccountBookMode.MonthlySum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        #endregion

        #region ApprovedVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort

        [Test]
        public async Task Book_ByRows_ApprovedVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_ApprovedVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                AccountBookMode.ByRows);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_VoucherSum_ApprovedVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_ApprovedVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                AccountBookMode.VoucherSum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_DailySum_ApprovedVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_ApprovedVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                AccountBookMode.DailySum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_MonthlySum_ApprovedVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_ApprovedVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                AccountBookMode.MonthlySum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        #endregion

        #region AllVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort

        [Test]
        public async Task Book_ByRows_AllVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_AllVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                AccountBookMode.ByRows);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_VoucherSum_AllVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_AllVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                AccountBookMode.VoucherSum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_DailySum_AllVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_AllVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                AccountBookMode.DailySum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_MonthlySum_AllVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_AllVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                AccountBookMode.MonthlySum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        #endregion

        #region CheckedVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort

        [Test]
        public async Task Book_ByRows_CheckedVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_CheckedVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                AccountBookMode.ByRows);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_VoucherSum_CheckedVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_CheckedVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                AccountBookMode.VoucherSum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_DailySum_CheckedVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_CheckedVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                AccountBookMode.DailySum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_MonthlySum_CheckedVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_CheckedVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                AccountBookMode.MonthlySum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        #endregion

        #region FinalizedVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort

        [Test]
        public async Task Book_ByRows_FinalizedVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_FinalizedVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                AccountBookMode.ByRows);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_VoucherSum_FinalizedVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_FinalizedVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                AccountBookMode.VoucherSum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_DailySum_FinalizedVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_FinalizedVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                AccountBookMode.DailySum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_MonthlySum_FinalizedVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_FinalizedVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                AccountBookMode.MonthlySum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        #endregion

        #region ConfirmedVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort

        [Test]
        public async Task Book_ByRows_ConfirmedVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_ConfirmedVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                AccountBookMode.ByRows);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_VoucherSum_ConfirmedVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_ConfirmedVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                AccountBookMode.VoucherSum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_DailySum_ConfirmedVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_ConfirmedVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                AccountBookMode.DailySum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_MonthlySum_ConfirmedVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_ConfirmedVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                AccountBookMode.MonthlySum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        #endregion

        #region ApprovedVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort

        [Test]
        public async Task Book_ByRows_ApprovedVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_ApprovedVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                AccountBookMode.ByRows);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_VoucherSum_ApprovedVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_ApprovedVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                AccountBookMode.VoucherSum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_DailySum_ApprovedVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_ApprovedVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                AccountBookMode.DailySum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_MonthlySum_ApprovedVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_ApprovedVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                AccountBookMode.MonthlySum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        #endregion

        #region AllVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort

        [Test]
        public async Task Book_ByRows_AllVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_AllVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                AccountBookMode.ByRows);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_VoucherSum_AllVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_AllVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                AccountBookMode.VoucherSum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_DailySum_AllVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_AllVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                AccountBookMode.DailySum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        [Test]
        public async Task Book_MonthlySum_AllVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort()
        {
            // Arrange & Act
            var book = await GetBook_AllVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
                AccountBookMode.MonthlySum);

            // Assert
            Assert.That(book, Is.Not.Null);
        }

        #endregion

        #endregion

        private void SetUp()
        {
            var repoContext = GetRepositoryContext();
            var sysRepo = GetSystemRepository(repoContext);
            var utility = new ReportDirectUtility(repoContext, sysRepo);
            _repository = new AccountBookRepositoryDirect(repoContext, sysRepo, utility);
        }

        #region GetBook_AllArticles_CurrentBranch_NoGrouping_NoFilterSort

        private async Task<AccountBookViewModel> GetBook_CheckedVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort(
            AccountBookMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseCheckedVouchers()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions);

            return await _repository.GetAccountBookAsync(parameters);
        }

        private async Task<AccountBookViewModel> GetBook_FinalizedVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort(
            AccountBookMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseFinalizedVouchers()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions);

            return await _repository.GetAccountBookAsync(parameters);
        }

        private async Task<AccountBookViewModel> GetBook_ConfirmedVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort(
            AccountBookMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseConfirmedVouchers()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions);

            return await _repository.GetAccountBookAsync(parameters);
        }

        private async Task<AccountBookViewModel> GetBook_ApprovedVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort(
            AccountBookMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseApprovedVouchers()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions);

            return await _repository.GetAccountBookAsync(parameters);
        }

        private async Task<AccountBookViewModel> GetBook_AllVouchers_AllArticles_CurrentBranch_NoGrouping_NoFilterSort(
            AccountBookMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .Build();
            var parameters = GetTestParameters(mode, gridOptions);

            return await _repository.GetAccountBookAsync(parameters);
        }

        #endregion

        #region GetBook_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort

        private async Task<AccountBookViewModel> GetBook_CheckedVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
            AccountBookMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .UseCheckedVouchers()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions);

            return await _repository.GetAccountBookAsync(parameters);
        }

        private async Task<AccountBookViewModel> GetBook_FinalizedVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
            AccountBookMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .UseFinalizedVouchers()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions);

            return await _repository.GetAccountBookAsync(parameters);
        }

        private async Task<AccountBookViewModel> GetBook_ConfirmedVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
            AccountBookMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .UseConfirmedVouchers()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions);

            return await _repository.GetAccountBookAsync(parameters);
        }

        private async Task<AccountBookViewModel> GetBook_ApprovedVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
            AccountBookMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .UseApprovedVouchers()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions);

            return await _repository.GetAccountBookAsync(parameters);
        }

        private async Task<AccountBookViewModel> GetBook_AllVouchers_AllArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
            AccountBookMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions);

            return await _repository.GetAccountBookAsync(parameters);
        }

        #endregion

        #region GetBook_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort

        private async Task<AccountBookViewModel> GetBook_CheckedVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
            AccountBookMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseCheckedVouchers()
                .UseMarkedLines()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions);

            return await _repository.GetAccountBookAsync(parameters);
        }

        private async Task<AccountBookViewModel> GetBook_FinalizedVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
            AccountBookMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseFinalizedVouchers()
                .UseMarkedLines()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions);

            return await _repository.GetAccountBookAsync(parameters);
        }

        private async Task<AccountBookViewModel> GetBook_ConfirmedVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
            AccountBookMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseConfirmedVouchers()
                .UseMarkedLines()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions);

            return await _repository.GetAccountBookAsync(parameters);
        }

        private async Task<AccountBookViewModel> GetBook_ApprovedVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
            AccountBookMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseApprovedVouchers()
                .UseMarkedLines()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions);

            return await _repository.GetAccountBookAsync(parameters);
        }

        private async Task<AccountBookViewModel> GetBook_AllVouchers_MarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
            AccountBookMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseMarkedLines()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions);

            return await _repository.GetAccountBookAsync(parameters);
        }

        #endregion

        #region GetBook_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort

        private async Task<AccountBookViewModel> GetBook_CheckedVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
            AccountBookMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .UseCheckedVouchers()
                .UseMarkedLines()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions);

            return await _repository.GetAccountBookAsync(parameters);
        }

        private async Task<AccountBookViewModel> GetBook_FinalizedVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
            AccountBookMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .UseFinalizedVouchers()
                .UseMarkedLines()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions);

            return await _repository.GetAccountBookAsync(parameters);
        }

        private async Task<AccountBookViewModel> GetBook_ConfirmedVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
            AccountBookMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .UseConfirmedVouchers()
                .UseMarkedLines()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions);

            return await _repository.GetAccountBookAsync(parameters);
        }

        private async Task<AccountBookViewModel> GetBook_ApprovedVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
            AccountBookMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .UseApprovedVouchers()
                .UseMarkedLines()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions);

            return await _repository.GetAccountBookAsync(parameters);
        }

        private async Task<AccountBookViewModel> GetBook_AllVouchers_MarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
            AccountBookMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .UseMarkedLines()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions);

            return await _repository.GetAccountBookAsync(parameters);
        }

        #endregion

        #region GetBook_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort

        private async Task<AccountBookViewModel> GetBook_CheckedVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
            AccountBookMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseCheckedVouchers()
                .UseUnmarkedLines()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions);

            return await _repository.GetAccountBookAsync(parameters);
        }

        private async Task<AccountBookViewModel> GetBook_FinalizedVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
            AccountBookMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseFinalizedVouchers()
                .UseUnmarkedLines()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions);

            return await _repository.GetAccountBookAsync(parameters);
        }

        private async Task<AccountBookViewModel> GetBook_ConfirmedVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
            AccountBookMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseConfirmedVouchers()
                .UseUnmarkedLines()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions);

            return await _repository.GetAccountBookAsync(parameters);
        }

        private async Task<AccountBookViewModel> GetBook_ApprovedVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
            AccountBookMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseApprovedVouchers()
                .UseUnmarkedLines()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions);

            return await _repository.GetAccountBookAsync(parameters);
        }

        private async Task<AccountBookViewModel> GetBook_AllVouchers_UnmarkedArticles_CurrentBranch_NoGrouping_NoFilterSort(
            AccountBookMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseUnmarkedLines()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions);

            return await _repository.GetAccountBookAsync(parameters);
        }

        #endregion

        #region GetBook_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort

        private async Task<AccountBookViewModel> GetBook_CheckedVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
            AccountBookMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .UseCheckedVouchers()
                .UseUnmarkedLines()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions);

            return await _repository.GetAccountBookAsync(parameters);
        }

        private async Task<AccountBookViewModel> GetBook_FinalizedVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
            AccountBookMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .UseFinalizedVouchers()
                .UseUnmarkedLines()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions);

            return await _repository.GetAccountBookAsync(parameters);
        }

        private async Task<AccountBookViewModel> GetBook_ConfirmedVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
            AccountBookMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .UseConfirmedVouchers()
                .UseUnmarkedLines()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions);

            return await _repository.GetAccountBookAsync(parameters);
        }

        private async Task<AccountBookViewModel> GetBook_ApprovedVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
            AccountBookMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .UseApprovedVouchers()
                .UseUnmarkedLines()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions);

            return await _repository.GetAccountBookAsync(parameters);
        }

        private async Task<AccountBookViewModel> GetBook_AllVouchers_UnmarkedArticles_CurrentAndChildren_NoGrouping_NoFilterSort(
            AccountBookMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .UseUnmarkedLines()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions);

            return await _repository.GetAccountBookAsync(parameters);
        }

        #endregion

        #region GetBook_AllArticles_CurrentBranch_ByBranch_NoFilterSort

        private async Task<AccountBookViewModel> GetBook_CheckedVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort(
            AccountBookMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseCheckedVouchers()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions, true);

            return await _repository.GetAccountBookAsync(parameters);
        }

        private async Task<AccountBookViewModel> GetBook_FinalizedVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort(
            AccountBookMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseFinalizedVouchers()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions, true);

            return await _repository.GetAccountBookAsync(parameters);
        }

        private async Task<AccountBookViewModel> GetBook_ConfirmedVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort(
            AccountBookMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseConfirmedVouchers()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions, true);

            return await _repository.GetAccountBookAsync(parameters);
        }

        private async Task<AccountBookViewModel> GetBook_ApprovedVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort(
            AccountBookMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseApprovedVouchers()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions, true);

            return await _repository.GetAccountBookAsync(parameters);
        }

        private async Task<AccountBookViewModel> GetBook_AllVouchers_AllArticles_CurrentBranch_ByBranch_NoFilterSort(
            AccountBookMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .Build();
            var parameters = GetTestParameters(mode, gridOptions, true);

            return await _repository.GetAccountBookAsync(parameters);
        }

        #endregion

        #region GetBook_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort

        private async Task<AccountBookViewModel> GetBook_CheckedVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort(
            AccountBookMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .UseCheckedVouchers()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions, true);

            return await _repository.GetAccountBookAsync(parameters);
        }

        private async Task<AccountBookViewModel> GetBook_FinalizedVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort(
            AccountBookMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .UseFinalizedVouchers()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions, true);

            return await _repository.GetAccountBookAsync(parameters);
        }

        private async Task<AccountBookViewModel> GetBook_ConfirmedVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort(
            AccountBookMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .UseConfirmedVouchers()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions, true);

            return await _repository.GetAccountBookAsync(parameters);
        }

        private async Task<AccountBookViewModel> GetBook_ApprovedVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort(
            AccountBookMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .UseApprovedVouchers()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions, true);

            return await _repository.GetAccountBookAsync(parameters);
        }

        private async Task<AccountBookViewModel> GetBook_AllVouchers_AllArticles_CurrentAndChildren_ByBranch_NoFilterSort(
            AccountBookMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions, true);

            return await _repository.GetAccountBookAsync(parameters);
        }

        #endregion

        #region GetBook_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort

        private async Task<AccountBookViewModel> GetBook_CheckedVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
            AccountBookMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseCheckedVouchers()
                .UseMarkedLines()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions, true);

            return await _repository.GetAccountBookAsync(parameters);
        }

        private async Task<AccountBookViewModel> GetBook_FinalizedVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
            AccountBookMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseFinalizedVouchers()
                .UseMarkedLines()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions, true);

            return await _repository.GetAccountBookAsync(parameters);
        }

        private async Task<AccountBookViewModel> GetBook_ConfirmedVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
            AccountBookMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseConfirmedVouchers()
                .UseMarkedLines()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions, true);

            return await _repository.GetAccountBookAsync(parameters);
        }

        private async Task<AccountBookViewModel> GetBook_ApprovedVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
            AccountBookMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseApprovedVouchers()
                .UseMarkedLines()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions, true);

            return await _repository.GetAccountBookAsync(parameters);
        }

        private async Task<AccountBookViewModel> GetBook_AllVouchers_MarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
            AccountBookMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseMarkedLines()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions, true);

            return await _repository.GetAccountBookAsync(parameters);
        }

        #endregion

        #region GetBook_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort

        private async Task<AccountBookViewModel> GetBook_CheckedVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
            AccountBookMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .UseCheckedVouchers()
                .UseMarkedLines()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions, true);

            return await _repository.GetAccountBookAsync(parameters);
        }

        private async Task<AccountBookViewModel> GetBook_FinalizedVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
            AccountBookMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .UseFinalizedVouchers()
                .UseMarkedLines()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions, true);

            return await _repository.GetAccountBookAsync(parameters);
        }

        private async Task<AccountBookViewModel> GetBook_ConfirmedVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
            AccountBookMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .UseConfirmedVouchers()
                .UseMarkedLines()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions, true);

            return await _repository.GetAccountBookAsync(parameters);
        }

        private async Task<AccountBookViewModel> GetBook_ApprovedVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
            AccountBookMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .UseApprovedVouchers()
                .UseMarkedLines()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions, true);

            return await _repository.GetAccountBookAsync(parameters);
        }

        private async Task<AccountBookViewModel> GetBook_AllVouchers_MarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
            AccountBookMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .UseMarkedLines()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions, true);

            return await _repository.GetAccountBookAsync(parameters);
        }

        #endregion

        #region GetBook_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort

        private async Task<AccountBookViewModel> GetBook_CheckedVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
            AccountBookMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseCheckedVouchers()
                .UseUnmarkedLines()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions, true);

            return await _repository.GetAccountBookAsync(parameters);
        }

        private async Task<AccountBookViewModel> GetBook_FinalizedVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
            AccountBookMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseFinalizedVouchers()
                .UseUnmarkedLines()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions, true);

            return await _repository.GetAccountBookAsync(parameters);
        }

        private async Task<AccountBookViewModel> GetBook_ConfirmedVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
            AccountBookMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseConfirmedVouchers()
                .UseUnmarkedLines()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions, true);

            return await _repository.GetAccountBookAsync(parameters);
        }

        private async Task<AccountBookViewModel> GetBook_ApprovedVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
            AccountBookMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseApprovedVouchers()
                .UseUnmarkedLines()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions, true);

            return await _repository.GetAccountBookAsync(parameters);
        }

        private async Task<AccountBookViewModel> GetBook_AllVouchers_UnmarkedArticles_CurrentBranch_ByBranch_NoFilterSort(
            AccountBookMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .WithBranchFilter(1)
                .UseUnmarkedLines()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions, true);

            return await _repository.GetAccountBookAsync(parameters);
        }

        #endregion

        #region GetBook_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort

        private async Task<AccountBookViewModel> GetBook_CheckedVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
            AccountBookMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .UseCheckedVouchers()
                .UseUnmarkedLines()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions, true);

            return await _repository.GetAccountBookAsync(parameters);
        }

        private async Task<AccountBookViewModel> GetBook_FinalizedVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
            AccountBookMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .UseFinalizedVouchers()
                .UseUnmarkedLines()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions, true);

            return await _repository.GetAccountBookAsync(parameters);
        }

        private async Task<AccountBookViewModel> GetBook_ConfirmedVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
            AccountBookMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .UseConfirmedVouchers()
                .UseUnmarkedLines()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions, true);

            return await _repository.GetAccountBookAsync(parameters);
        }

        private async Task<AccountBookViewModel> GetBook_ApprovedVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
            AccountBookMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .UseApprovedVouchers()
                .UseUnmarkedLines()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions, true);

            return await _repository.GetAccountBookAsync(parameters);
        }

        private async Task<AccountBookViewModel> GetBook_AllVouchers_UnmarkedArticles_CurrentAndChildren_ByBranch_NoFilterSort(
            AccountBookMode mode)
        {
            var gridOptions = new OptionsBuilder()
                .UseUnmarkedLines()
                .Build();
            var parameters = GetTestParameters(mode, gridOptions, true);

            return await _repository.GetAccountBookAsync(parameters);
        }

        #endregion

        private AccountBookParameters GetTestParameters(
            AccountBookMode mode, GridOptions gridOptions, bool byBranch = false)
        {
            return new AccountBookParameters()
            {
                FromDate = DateTime.Parse("2018-06-21"),
                ToDate = DateTime.Parse("2019-03-20"),
                Mode = mode,
                IsByBranch = byBranch,
                ViewId = ViewId.Account,
                ItemId = 101,
                GridOptions = gridOptions
            };
        }

        private IAccountBookRepository _repository;
    }
}
