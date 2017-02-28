using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http.Results;
using Moq;
using NUnit.Framework;
using SPPC.Tadbir.Api;
using SPPC.Tadbir.ViewModel.Finance;

namespace SPPC.Tadbir.Web.Api.Controllers.Tests
{
    public partial class TransactionsControllerTests
    {
        [Test]
        public void PostNewTransaction_GivenInvalidTransaction_ReturnsBadRequestWithMessage()
        {
            // Arrange
            var invalid = new TransactionViewModel() { Id = 1 };
            _mockRepository.Setup(repo => repo.IsValidTransaction(invalid))
                .Returns(false);

            // Act
            var result = _controller.PostNewTransaction(invalid) as BadRequestErrorMessageResult;

            // Assert
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void PutModifiedTransaction_GivenInvalidTransaction_ReturnsBadRequestWithMessage()
        {
            // Arrange
            var invalid = new TransactionViewModel() { Id = 1 };
            _mockRepository.Setup(repo => repo.IsValidTransaction(invalid))
                .Returns(false);

            // Act
            var result = _controller.PutModifiedTransaction(1, invalid) as BadRequestErrorMessageResult;

            // Assert
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void PostNewArticle_GivenArticleWithDebitAndCredit_ReturnsBadRequestWithMessage()
        {
            // Arrange
            var invalid = GetInvalidArticle();

            // Act
            var result = _controller.PostNewArticle(invalid.TransactionId, invalid) as BadRequestErrorMessageResult;

            // Assert
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void PutModifiedArticle_GivenArticleWithDebitAndCredit_ReturnsBadRequestWithMessage()
        {
            // Arrange
            var invalid = GetInvalidArticle();

            // Act
            var result = _controller.PutModifiedArticle(invalid.Id, invalid) as BadRequestErrorMessageResult;

            // Assert
            Assert.That(result, Is.Not.Null);
        }

        #region GetTransactionDetail (GET: transactions/{transactionId:int}/details) tests

        [Test]
        public void GetTransactionDetail_SpecifiesCorrectRoute()
        {
            // Arrange (Done in setup methods)

            // Act & Assert
            AssertActionRouteEquals("GetTransactionDetail", TransactionApi.TransactionDetailsUrl);
        }

        [Test]
        public void GetTransactionDetail_ReturnsNonNullResult()
        {
            // Arrange (Done in setup methods)

            // Act
            var result = _controller.GetTransactionDetail(1);

            // Assert
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void GetTransactionDetail_CallsRepositoryWithTransactionId()
        {
            // Arrange
            int transactionId = 1;

            // Act
            _controller.GetTransactionDetail(transactionId);

            // Assert
            _mockRepository.Verify(repo => repo.GetTransactionDetail(transactionId));
        }

        [Test]
        public void GetTransactionDetail_GivenExistingId_ReturnsJsonWithCorrectContentType()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.GetTransactionDetail(_existingTransactionId))
                .Returns(new TransactionFullViewModel());

            // Act
            var result = _controller.GetTransactionDetail(_existingTransactionId) as JsonResult<TransactionFullViewModel>;

            // Assert
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void GetTransactionDetail_GivenNonExistingId_ReturnsNotFound()
        {
            // Arrange
            int nonExistingId = 2;
            _mockRepository.Setup(repo => repo.GetTransactionDetail(nonExistingId))
                .Returns((TransactionFullViewModel)null);

            // Act
            var result = _controller.GetTransactionDetail(nonExistingId) as NotFoundResult;

            // Assert
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void GetTransactionDetail_GivenInvalidId_ReturnsNotFound()
        {
            // Arrange
            int invalidId = 0;
            _mockRepository.Setup(repo => repo.GetTransactionDetail(It.Is<int>(val => val <= 0)))
                .Returns((TransactionFullViewModel)null);

            // Act
            var result = _controller.GetTransactionDetail(invalidId) as NotFoundResult;

            // Assert
            Assert.That(result, Is.Not.Null);
        }

        #endregion

        #region GetArticleDetails (GET: transactions/articles/{articleId:int}/details) tests

        [Test]
        public void GetArticleDetails_SpecifiesCorrectRoute()
        {
            // Arrange (Done in setup methods)

            // Act & Assert
            AssertActionRouteEquals("GetArticleDetails", TransactionApi.TransactionArticleDetailsUrl);
        }

        [Test]
        public void GetArticleDetails_ReturnsNotNullResult()
        {
            // Arrange (Done in setup methods)

            // Act
            var result = _controller.GetArticleDetails(1);

            // Assert
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void GetArticleDetails_CallsRepositoryWithArticleId()
        {
            // Arrange
            int articleId = 1;

            // Act
            _controller.GetArticleDetails(articleId);

            // Assert
            _mockRepository.Verify(repo => repo.GetArticle(articleId));
        }

        [Test]
        public void GetArticleDetails_GivenExistingId_ReturnsJsonWithCorrectContentType()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.GetArticleDetails(_existingArticleId))
                .Returns(new TransactionLineFullViewModel());

            // Act
            var result = _controller.GetArticleDetails(_existingArticleId)
                as JsonResult<TransactionLineFullViewModel>;

            // Assert
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void GetArticleDetails_GivenNonExistingId_ReturnsNotFound()
        {
            // Arrange
            int nonExistingId = 2;
            _mockRepository.Setup(repo => repo.GetArticleDetails(nonExistingId))
                .Returns((TransactionLineFullViewModel)null);

            // Act
            var result = _controller.GetArticleDetails(nonExistingId) as NotFoundResult;

            // Assert
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void GetArticleDetails_GivenInvalidArticleId_ReturnsNotFound()
        {
            // Arrange
            int invalidId = 0;
            _mockRepository.Setup(repo => repo.GetArticleDetails(It.Is<int>(val => val <= 0)))
                .Returns((TransactionLineFullViewModel)null);

            // Act
            var result = _controller.GetArticleDetails(invalidId) as NotFoundResult;

            // Assert
            Assert.That(result, Is.Not.Null);
        }

        #endregion

        private TransactionLineViewModel GetInvalidArticle()
        {
            var invalid = new TransactionLineViewModel()
            {
                Id = 1,
                TransactionId = 1,
                Debit = 100,
                Credit = 200
            };

            return invalid;
        }
    }
}
