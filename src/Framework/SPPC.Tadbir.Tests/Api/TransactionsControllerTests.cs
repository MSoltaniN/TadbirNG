using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using SPPC.Tadbir.Api;
using SPPC.Tadbir.Persistence;
using SPPC.Tadbir.Security;
using SPPC.Tadbir.Service;
using SPPC.Tadbir.ViewModel.Auth;
using SPPC.Tadbir.ViewModel.Core;
using SPPC.Tadbir.ViewModel.Finance;

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
            var mockUserContext = new Mock<ISecurityContext>();
            mockUserContext
                .Setup(ctx => ctx.User)
                .Returns(new UserContextViewModel() { Id = 1 });
            _mockContext = new Mock<ISecurityContextManager>();
            _mockContext
                .Setup(ctx => ctx.CurrentContext)
                .Returns(mockUserContext.Object);
        }

        [SetUp]
        public void Setup()
        {
            _controller = new TransactionsController(_mockRepository.Object, _mockContext.Object);
            _existingTransaction = new TransactionViewModel() { Id = _existingTransactionId };
            _existingTransaction.Document.Actions.Add(new DocumentActionViewModel());
            _existingArticle = new TransactionLineViewModel()
            {
                Id = _existingArticleId,
                TransactionId = _existingTransactionId
            };
        }

        #region GetTransactions (GET: transactions/fp/{fpId}/branch/{branchId}) tests

        [Test]
        public void GetTransactions_HasAuthorizeRequestAttribute()
        {
            // Arrange

            // Act & Assert
            AssertActionIsSecured("GetTransactions", SecureEntity.Transaction, (int)AccountPermissions.View);
        }

        [Test]
        public void GetTransactions_SpecifiesCorrectRoute()
        {
            // Arrange

            // Act & Assert
            AssertActionRouteEquals("GetTransactions", TransactionApi.FiscalPeriodBranchTransactionsSyncUrl);
        }

        [Test]
        public void GetTransactions_ReturnsNotNullResult()
        {
            // Arrange

            // Act
            var result = _controller.GetTransactions(_fpId, _branchId);

            // Assert
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void GetTransactions_CallsRepositoryWithFiscalPeriodId()
        {
            // Arrange

            // Act
            _controller.GetTransactions(_fpId, _branchId);

            // Assert
            _mockRepository.Verify(repo => repo.GetTransactions(_fpId, _branchId));
        }

        [Test]
        public void GetTransactions_ReturnsJsonWithCorrectContentType()
        {
            // Arrange

            // Act
            var result = _controller.GetTransactions(_fpId, _branchId) as JsonResult;

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
            AssertActionRouteEquals("PostNewTransaction", TransactionApi.TransactionsSyncUrl);
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
            var newTransaction = GetNewTransaction();
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
            var newTransaction = GetNewTransaction();
            _mockRepository.Setup(repo => repo.IsValidTransaction(newTransaction))
                .Returns(true);

            // Act
            var result = _controller.PostNewTransaction(newTransaction) as StatusCodeResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.StatusCode, Is.EqualTo(StatusCodes.Status201Created));
        }

        [Test]
        public void PostNewTransaction_GivenNoContent_ReturnsBadRequestWithMessage()
        {
            // Arrange (Done in setup methods)

            // Act
            var result = _controller.PostNewTransaction(null) as BadRequestObjectResult;

            // Assert
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void PostNewTransaction_GivenInvalidModel_ReturnsBadRequestObjectResultWithCorrectValue()
        {
            // Arrange
            var invalidModel = new TransactionViewModel();

            // Act
            _controller.ModelState.AddModelError(String.Empty, "Invalid");
            var result = _controller.PostNewTransaction(invalidModel) as BadRequestObjectResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Value, Is.InstanceOf<SerializableError>());
            Assert.That((result.Value as SerializableError).Count, Is.EqualTo(1));
        }

        [Test]
        public void PostNewTransaction_GivenInvalidTransaction_ReturnsBadRequestWithMessage()
        {
            // Arrange
            var invalid = new TransactionViewModel() { Id = 1 };
            _mockRepository.Setup(repo => repo.IsValidTransaction(invalid))
                .Returns(false);

            // Act
            var result = _controller.PostNewTransaction(invalid) as BadRequestObjectResult;

            // Assert
            Assert.That(result, Is.Not.Null);
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
            var result = _controller.PutModifiedTransaction(0, _existingTransaction) as BadRequestObjectResult;

            // Assert
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void PutModifiedTransaction_GivenInvalidModelId_ReturnsBadRequestWithMessage()
        {
            // Arrange
            var transaction = new TransactionViewModel() { Id = 0 };

            // Act
            var result = _controller.PutModifiedTransaction(1, transaction) as BadRequestObjectResult;

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
                as BadRequestObjectResult;

            // Assert
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void PutModifiedTransaction_GivenNoContent_ReturnsBadRequestWithMessage()
        {
            // Arrange (Done in setup methods)
            int validId = 1;

            // Act
            var result = _controller.PutModifiedTransaction(validId, null) as BadRequestObjectResult;

            // Assert
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void PutModifiedTransaction_GivenInvalidModel_ReturnsBadRequestObjectResultWithCorrectValue()
        {
            // Arrange
            var invalidModel = new TransactionViewModel() { Id = _existingTransactionId };

            // Act
            _controller.ModelState.AddModelError(String.Empty, "Invalid");
            var result = _controller.PutModifiedTransaction(_existingTransactionId, invalidModel)
                as BadRequestObjectResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Value, Is.InstanceOf<SerializableError>());
            Assert.That((result.Value as SerializableError).Count, Is.EqualTo(1));
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

        [Test]
        public void PutModifiedTransaction_GivenInvalidTransaction_ReturnsBadRequestWithMessage()
        {
            // Arrange
            var invalid = new TransactionViewModel() { Id = 1 };
            _mockRepository.Setup(repo => repo.IsValidTransaction(invalid))
                .Returns(false);

            // Act
            var result = _controller.PutModifiedTransaction(1, invalid) as BadRequestObjectResult;

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
                as JsonResult;

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
            Assert.That(result.StatusCode, Is.EqualTo(StatusCodes.Status201Created));
        }

        [Test]
        public void PostNewArticle_GivenNoContent_ReturnsBadRequestWithMessage()
        {
            // Arrange (Done in setup methods)

            // Act
            var result = _controller.PostNewArticle(_existingTransactionId, null) as BadRequestObjectResult;

            // Assert
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void PostNewArticle_GivenModelWithInvalidTransactionId_ReturnsBadRequestWithMessage()
        {
            // Arrange (Done in setup methods)

            // Act
            var result = _controller.PostNewArticle(_existingTransactionId, new TransactionLineViewModel())
                as BadRequestObjectResult;

            // Assert
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void PostNewArticle_GivenInvalidModel_ReturnsBadRequestObjectResultWithCorrectValue()
        {
            // Arrange
            var invalidModel = new TransactionLineViewModel() { TransactionId = _existingTransactionId };

            // Act
            _controller.ModelState.AddModelError(String.Empty, "Invalid");
            var result = _controller.PostNewArticle(_existingTransactionId, invalidModel) as BadRequestObjectResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Value, Is.InstanceOf<SerializableError>());
            Assert.That((result.Value as SerializableError).Count, Is.EqualTo(1));
        }

        [Test]
        public void PostNewArticle_GivenArticleWithDebitAndCredit_ReturnsBadRequestWithMessage()
        {
            // Arrange
            var invalid = GetInvalidArticle();

            // Act
            var result = _controller.PostNewArticle(invalid.TransactionId, invalid) as BadRequestObjectResult;

            // Assert
            Assert.That(result, Is.Not.Null);
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
                as BadRequestObjectResult;

            // Assert
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void PutModifiedArticle_GivenNoContent_ReturnsBadRequestWithMessage()
        {
            // Arrange (Done in Setup method)
            int validId = 1;

            // Act
            var result = _controller.PutModifiedArticle(validId, null) as BadRequestObjectResult;

            // Assert
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void PutModifiedArticle_GivenInvalidModel_ReturnsBadRequestObjectResultWithCorrectValue()
        {
            // Arrange
            var invalidModel = new TransactionLineViewModel() { Id = 1 };

            // Act
            _controller.ModelState.AddModelError(String.Empty, "Invalid");
            var result = _controller.PutModifiedArticle(1, invalidModel) as BadRequestObjectResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Value, Is.InstanceOf<SerializableError>());
            Assert.That((result.Value as SerializableError).Count, Is.EqualTo(1));
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

        [Test]
        public void PutModifiedArticle_GivenArticleWithDebitAndCredit_ReturnsBadRequestWithMessage()
        {
            // Arrange
            var invalid = GetInvalidArticle();

            // Act
            var result = _controller.PutModifiedArticle(invalid.Id, invalid) as BadRequestObjectResult;

            // Assert
            Assert.That(result, Is.Not.Null);
        }

        #endregion

        #region GetTransactionDetail (GET: transactions/{transactionId:int}/details) tests

        [Test]
        public void GetTransactionDetail_SpecifiesCorrectRoute()
        {
            // Arrange (Done in setup methods)

            // Act & Assert
            AssertActionRouteEquals("GetTransactionDetail", TransactionApi.TransactionDetailsSyncUrl);
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
            var result = _controller.GetTransactionDetail(_existingTransactionId) as JsonResult;

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
                as JsonResult;

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

        private TransactionViewModel GetNewTransaction()
        {
            var newTransaction = new TransactionViewModel();
            newTransaction.Document.Actions.Add(new DocumentActionViewModel());
            return newTransaction;
        }

        private Mock<ITransactionRepository> _mockRepository;
        private Mock<ISecurityContextManager> _mockContext;
        private TransactionViewModel _existingTransaction;
        private int _existingTransactionId = 1;
        private int _existingArticleId = 1;
        private int _fpId = 1;
        private int _branchId = 1;
        private TransactionLineViewModel _existingArticle;
    }
}
