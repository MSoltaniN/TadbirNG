// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.0.0.0
//     Template Version: 1.0
//     Generation Date: 2017-02-27 2:27:54 PM
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http.Results;
using SPPC.Tadbir.NHibernate;
using SPPC.Tadbir.ViewModel.Finance;
using Moq;
using NUnit.Framework;
using SPPC.Tadbir.Api;

namespace SPPC.Tadbir.Web.Api.Controllers.Tests
{
    [TestFixture]
    [Category("WebApi")]
    public partial class TransactionsControllerTests : ApiControllerTestBase<TransactionsController>
    {
        [OneTimeSetUp]
        public void FixtureSetup()
        {
            _mockRepository = new Mock<ITransactionRepository>();
        }

        [SetUp]
        public void Setup()
        {
            _controller = new TransactionsController(_mockRepository.Object);
            _existingTransaction = new TransactionViewModel() { Id = _existingTransactionId };
            _existingArticle = new TransactionLineViewModel()
            {
                Id = _existingArticleId,
                TransactionId = _existingTransactionId
            };
        }

        #region GetTransactions (GET: transactions/fp/{fpId:int}) tests

        [Test]
        public void GetTransactions_SpecifiesCorrectRoute()
        {
            // Arrange

            // Act & Assert
            AssertActionRouteEquals("GetTransactions", TransactionApi.FiscalPeriodTransactionsUrl);
        }

        [Test]
        public void GetTransactions_ReturnsNotNullResult()
        {
            // Arrange

            // Act
            var result = _controller.GetTransactions(_fpId);

            // Assert
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void GetTransactions_CallsRepositoryWithFiscalPeriodId()
        {
            // Arrange

            // Act
            _controller.GetTransactions(_fpId);

            // Assert
            _mockRepository.Verify(repo => repo.GetTransactions(_fpId));
        }

        [Test]
        public void GetTransactions_ReturnsJsonWithCorrectContentType()
        {
            // Arrange

            // Act
            var result = _controller.GetTransactions(_fpId) as JsonResult<IList<TransactionViewModel>>;

            // Assert
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void GetTransactions_GivenInvalidFiscalPeriodId_ReturnsNotFound()
        {
            // Arrange
            int invalidFpId = -2;

            // Act
            var result = _controller.GetTransactions(invalidFpId) as NotFoundResult;

            // Assert
            Assert.That(result, Is.Not.Null);
        }

        #endregion

        #region PostNewTransaction (POST: transactions) tests

        [Test]
        public void PostNewTransaction_SpecifiesCorrectRoute()
        {
            // Arrange (Done in setup methods)

            // Act & Assert
            AssertActionRouteEquals("PostNewTransaction", TransactionApi.TransactionsUrl);
        }

        [Test]
        public void PostNewTransaction_ReturnsNonNullResult()
        {
            // Arrange (Done in Setup method)

            // Act
            var result = _controller.PostNewTransaction(new TransactionViewModel());

            // Assert
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void PostNewTransaction_GivenValidModel_CallsRepositoryWithModel()
        {
            // Arrange
            var newTransaction = new TransactionViewModel();
            _mockRepository.Setup(repo => repo.IsValidTransaction(newTransaction))
                .Returns(true);

            // Act
            _controller.PostNewTransaction(newTransaction);

            // Assert
            _mockRepository.Verify(repo => repo.SaveTransaction(newTransaction));
        }

        [Test]
        public void PostNewTransaction_GivenValidModel_ReturnsCreatedStatusCodeResult()
        {
            // Arrange
            var newTransaction = new TransactionViewModel();
            _mockRepository.Setup(repo => repo.IsValidTransaction(newTransaction))
                .Returns(true);

            // Act
            var result = _controller.PostNewTransaction(newTransaction) as StatusCodeResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.Created));
        }

        [Test]
        public void PostNewTransaction_GivenNoContent_ReturnsBadRequestWithMessage()
        {
            // Arrange (Done in setup methods)

            // Act
            var result = _controller.PostNewTransaction(null) as BadRequestErrorMessageResult;

            // Assert
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void PostNewTransaction_GivenInvalidModel_ReturnsInvalidModelStateResultWithModelState()
        {
            // Arrange
            var invalidModel = new TransactionViewModel();

            // Act
            _controller.ModelState.AddModelError(String.Empty, "Invalid");
            var result = _controller.PostNewTransaction(invalidModel) as InvalidModelStateResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.ModelState, Is.SameAs(_controller.ModelState));
        }

        #endregion

        #region PutModifiedTransaction (PUT: transactions/{transactionId}) tests

        [Test]
        public void PutModifiedTransaction_SpecifiesCorrectRoute()
        {
            // Arrange (Done in setup methods)

            // Act & Assert
            AssertActionRouteEquals("PutModifiedTransaction", TransactionApi.TransactionUrl);
        }

        [Test]
        public void PutModifiedTransaction_ReturnsNonNullResult()
        {
            // Arrange (Done in setup methods)

            // Act
            var result = _controller.PutModifiedTransaction(_existingTransactionId, _existingTransaction);

            // Assert
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void PutModifiedTransaction_GivenInvalidId_ReturnsBadRequestWithMessage()
        {
            // Arrange (Done in setup methods)

            // Act
            var result = _controller.PutModifiedTransaction(0, _existingTransaction) as BadRequestErrorMessageResult;

            // Assert
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void PutModifiedTransaction_GivenInvalidModelId_ReturnsBadRequestWithMessage()
        {
            // Arrange
            var transaction = new TransactionViewModel() { Id = 0 };

            // Act
            var result = _controller.PutModifiedTransaction(1, transaction) as BadRequestErrorMessageResult;

            // Assert
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void PutModifiedTransaction_GivenConflictingIdAndModelId_ReturnsBadRequestWithMessage()
        {
            // Arrange
			int conflictingTransactionId = 4;

            // Act
            var result = _controller.PutModifiedTransaction(conflictingTransactionId, _existingTransaction)
                as BadRequestErrorMessageResult;

            // Assert
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void PutModifiedTransaction_GivenNoContent_ReturnsBadRequestWithMessage()
        {
            // Arrange (Done in setup methods)
            int validId = 1;

            // Act
            var result = _controller.PutModifiedTransaction(validId, null) as BadRequestErrorMessageResult;

            // Assert
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void PutModifiedTransaction_GivenInvalidModel_ReturnsInvalidModelStateResultWithModelState()
        {
            // Arrange
            var invalidModel = new TransactionViewModel() { Id = _existingTransactionId };

            // Act
            _controller.ModelState.AddModelError(String.Empty, "Invalid");
            var result = _controller.PutModifiedTransaction(_existingTransactionId, invalidModel)
                as InvalidModelStateResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.ModelState, Is.SameAs(_controller.ModelState));
        }

        [Test]
        public void PutModifiedTransaction_GivenValidModel_CallsRepositoryWithModel()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.IsValidTransaction(_existingTransaction))
                .Returns(true);

            // Act
            _controller.PutModifiedTransaction(_existingTransactionId, _existingTransaction);

            // Assert
            _mockRepository.Verify(repo => repo.SaveTransaction(_existingTransaction));
        }

        [Test]
        public void PutModifiedTransaction_GivenValidIdAndModel_ReturnsOk()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.IsValidTransaction(_existingTransaction))
                .Returns(true);

            // Act
            var result = _controller.PutModifiedTransaction(_existingTransactionId, _existingTransaction) as OkResult;

            // Assert
            Assert.That(result, Is.Not.Null);
        }

        #endregion

        #region GetArticle (GET: transactions/articles/{articleId:int}) tests

        [Test]
        public void GetArticle_SpecifiesCorrectRoute()
        {
            // Arrange (Done in setup methods)

            // Act & Assert
            AssertActionRouteEquals("GetArticle", TransactionApi.TransactionArticleUrl);
        }

        [Test]
        public void GetArticle_ReturnsNonNullResult()
        {
            // Arrange (Done in setup methods)

            // Act
            var result = _controller.GetArticle(1);

            // Assert
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void GetArticle_CallsRepositoryWithArticleId()
        {
            // Arrange
            int articleId = 1;

            // Act
            _controller.GetArticle(articleId);

            // Assert
            _mockRepository.Verify(repo => repo.GetArticle(articleId));
        }

        [Test]
        public void GetArticle_GivenExistingId_ReturnsJsonWithCorrectContentType()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.GetArticle(_existingArticleId))
                .Returns(new TransactionLineViewModel());

            // Act
            var result = _controller.GetArticle(_existingArticleId)
                as JsonResult<TransactionLineViewModel>;

            // Assert
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void GetArticle_GivenNonExistingId_ReturnsNotFound()
        {
            // Arrange
            int nonExistingId = 2;
            _mockRepository.Setup(repo => repo.GetArticle(nonExistingId))
                .Returns((TransactionLineViewModel)null);

            // Act
            var result = _controller.GetArticle(nonExistingId) as NotFoundResult;

            // Assert
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void GetArticle_GivenInvalidArticleId_ReturnsNotFound()
        {
            // Arrange
            int invalidId = 0;
            _mockRepository.Setup(repo => repo.GetArticle(It.Is<int>(val => val <= 0)))
                .Returns((TransactionLineViewModel)null);

            // Act
            var result = _controller.GetArticle(invalidId) as NotFoundResult;

            // Assert
            Assert.That(result, Is.Not.Null);
        }

        #endregion

        #region PostNewArticle (POST: transactions/{transactionId:int}/articles) tests

        [Test]
        public void PostNewArticle_SpecifiesCorrectRoute()
        {
            // Arrange (Done in setup methos)

            // Act & Assert
            AssertActionRouteEquals("PostNewArticle", TransactionApi.TransactionArticlesUrl);
        }

        [Test]
        public void PostNewArticle_ReturnsNonNullResult()
        {
            // Arrange (Done in setup methods)

            // Act
            var result = _controller.PostNewArticle(1, new TransactionLineViewModel());

            // Assert
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void PostNewArticle_GivenValidModel_CallsRepositoryWithModel()
        {
            // Arrange
            var validModel = new TransactionLineViewModel() { TransactionId = _existingTransactionId };

            // Act
            _controller.PostNewArticle(_existingTransactionId, validModel);

            // Assert
            _mockRepository.Verify(repo => repo.SaveArticle(validModel));
        }

        [Test]
        public void PostNewArticle_GivenValidModel_ReturnsCreatedStatusCodeResult()
        {
            // Arrange
            var validModel = new TransactionLineViewModel() { TransactionId = _existingTransactionId };

            // Act
            var result = _controller.PostNewArticle(_existingTransactionId, validModel) as StatusCodeResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.Created));
        }

        [Test]
        public void PostNewArticle_GivenNoContent_ReturnsBadRequestWithMessage()
        {
            // Arrange (Done in setup methods)

            // Act
            var result = _controller.PostNewArticle(_existingTransactionId, null) as BadRequestErrorMessageResult;

            // Assert
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void PostNewArticle_GivenInvalidTransactionId_ReturnsBadRequestWithMessage()
        {
            // Arrange
            int invalidId = 0;

            // Act
            var result = _controller.PostNewArticle(invalidId, new TransactionLineViewModel()) as BadRequestErrorMessageResult;

            // Assert
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void PostNewArticle_GivenModelWithInvalidTransactionId_ReturnsBadRequestWithMessage()
        {
            // Arrange (Done in setup methods)

            // Act
            var result = _controller.PostNewArticle(_existingTransactionId, new TransactionLineViewModel())
                as BadRequestErrorMessageResult;

            // Assert
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void PostNewArticle_GivenInvalidModel_ReturnsInvalidModelStateResultWithModelState()
        {
            // Arrange
            var invalidModel = new TransactionLineViewModel() { TransactionId = _existingTransactionId };

            // Act
            _controller.ModelState.AddModelError(String.Empty, "Invalid");
            var result = _controller.PostNewArticle(_existingTransactionId, invalidModel) as InvalidModelStateResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.ModelState, Is.SameAs(_controller.ModelState));
        }

        #endregion

        #region PutModifiedArticle (PUT: transactions/articles/{articleId:int}) tests

        [Test]
        public void PutModifiedArticle_SpecifiesCorrectRoute()
        {
            // Arrange (Done in Setup method)

            // Act & Assert
            AssertActionRouteEquals("PutModifiedArticle", TransactionApi.TransactionArticleUrl);
        }

        [Test]
        public void PutModifiedArticle_ReturnsNonNullResult()
        {
            // Arrange (Done in setup methods)

            // Act
            var result = _controller.PutModifiedArticle(1, new TransactionLineViewModel());

            // Assert
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void PutModifiedArticle_GivenInvalidArticleId_ReturnsBadRequestWithMessage()
        {
            // Arrange (Done in setup methods)

            // Act
            var result = _controller.PutModifiedArticle(0, _existingArticle)
                as BadRequestErrorMessageResult;

            // Assert
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void PutModifiedArticle_GivenNoContent_ReturnsBadRequestWithMessage()
        {
            // Arrange (Done in Setup method)
            int validId = 1;

            // Act
            var result = _controller.PutModifiedArticle(validId, null) as BadRequestErrorMessageResult;

            // Assert
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void PutModifiedArticle_GivenInvalidModel_ReturnsInvalidModelStateResultWithModelState()
        {
            // Arrange
            var invalidModel = new TransactionLineViewModel() { Id = 1 };

            // Act
            _controller.ModelState.AddModelError(String.Empty, "Invalid");
            var result = _controller.PutModifiedArticle(1, invalidModel) as InvalidModelStateResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.ModelState, Is.SameAs(_controller.ModelState));
        }

        [Test]
        public void PutModifiedArticle_CallsRepositoryWithModel()
        {
            // Arrange (Done in setup methods)

            // Act
            _controller.PutModifiedArticle(_existingArticleId, _existingArticle);

            // Assert
            _mockRepository.Verify(repo => repo.SaveArticle(_existingArticle));
        }

        [Test]
        public void PutModifiedArticle_GivenValidIdsAndModel_ReturnsOk()
        {
            // Arrange (Done in setup methods)

            // Act
            var result = _controller.PutModifiedArticle(_existingArticleId, _existingArticle)
                as OkResult;

            // Assert
            Assert.That(result, Is.Not.Null);
        }

        #endregion

        private Mock<ITransactionRepository> _mockRepository;
        private TransactionViewModel _existingTransaction;
        private int _existingTransactionId = 1;
        private int _existingArticleId = 1;
        private int _fpId = 1;
        private TransactionLineViewModel _existingArticle;
    }
}
