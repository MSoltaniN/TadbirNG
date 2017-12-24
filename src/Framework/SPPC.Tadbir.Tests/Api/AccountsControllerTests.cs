using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using SPPC.Tadbir.Api;
using SPPC.Tadbir.Persistence;
using SPPC.Tadbir.ViewModel.Finance;

namespace SPPC.Tadbir.Web.Api.Controllers.Tests
{
    [TestFixture]
    [Category("WebApi")]
    public partial class AccountsControllerTests : ApiControllerTestBase<AccountsController>
    {
        [OneTimeSetUp]
        public void FixtureSetup()
        {
            _mockRepository = new Mock<IAccountRepository>();
        }

        [SetUp]
        public void Setup()
        {
            _controller = new AccountsController(_mockRepository.Object);
            _existingAccount = new AccountViewModel() { Id = _existingAccountId };
        }

        #region GetAccounts (GET: accounts/fp/{fpId:int}) tests

        [Test]
        public void GetAccounts_SpecifiesCorrectRoute()
        {
            // Arrange

            // Act & Assert
            AssertActionRouteEquals("GetAccounts", AccountApi.FiscalPeriodBranchAccountsUrl);
        }

        [Test]
        public void GetAccounts_ReturnsNonNullResult()
        {
            // Arrange

            // Act
            var result = _controller.GetAccounts(_fpId, _branchId);

            // Assert
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void GetAccounts_CallsRepositoryWithFiscalPeriodId()
        {
            // Arrange

            // Act
            _controller.GetAccounts(_fpId, _branchId);

            // Assert
            _mockRepository.Verify(repo => repo.GetAccounts(_fpId, _branchId, null));
        }

        [Test]
        public void GetAccounts_ReturnsJsonWithCorrectContentType()
        {
            // Arrange

            // Act
            var result = _controller.GetAccounts(_fpId, _branchId) as JsonResult;

            // Assert
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void GetAccounts_GivenInvalidFiscalPeriodId_ReturnsNotFound()
        {
            // Arrange
            int invalidFpId = -2;

            // Act
            var result = _controller.GetAccounts(invalidFpId, _branchId) as NotFoundResult;

            // Assert
            Assert.That(result, Is.Not.Null);
        }

        #endregion

        #region GetAccount (GET: accounts/{accountId:int}) tests

        [Test]
        public void GetAccount_SpecifiesCorrectRoute()
        {
            // Arrange (Done in setup methods)

            // Act & Assert
            AssertActionRouteEquals("GetAccount", AccountApi.AccountUrl);
        }

        [Test]
        public void GetAccount_ReturnsNotNullResult()
        {
            // Arrange (Done in setup methods)

            // Act
            var result = _controller.GetAccount(1);

            // Assert
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void GetAccount_CallsRepositoryWithAccountId()
        {
            // Arrange
            int accountId = 1;

            // Act
            _controller.GetAccount(accountId);

            // Assert
            _mockRepository.Verify(repo => repo.GetAccount(accountId));
        }

        [Test]
        public void GetAccount_GivenExistingId_ReturnsJsonWithCorrectContentType()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.GetAccount(_existingAccountId))
                .Returns(new AccountViewModel());

            // Act
            var result = _controller.GetAccount(_existingAccountId) as JsonResult;

            // Assert
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void GetAccount_GivenNonExistingId_ReturnsNotFound()
        {
            // Arrange
            int nonExistingId = 2;
            _mockRepository.Setup(repo => repo.GetAccount(nonExistingId))
                .Returns((AccountViewModel)null);

            // Act
            var result = _controller.GetAccount(nonExistingId) as NotFoundResult;

            // Assert
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void GetAccount_GivenInvalidId_ReturnsNotFound()
        {
            // Arrange
            int invalidId = 0;
            _mockRepository.Setup(repo => repo.GetAccount(It.Is<int>(val => val <= 0)))
                .Returns((AccountViewModel)null);

            // Act
            var result = _controller.GetAccount(invalidId) as NotFoundResult;

            // Assert
            Assert.That(result, Is.Not.Null);
        }

        #endregion

        #region PostNewAccount (POST: accounts) tests

        [Test]
        public void PostNewAccount_SpecifiesCorrectRoute()
        {
            // Arrange (Done in setup methods)

            // Act & Assert
            AssertActionRouteEquals("PostNewAccount", AccountApi.AccountsUrl);
        }

        [Test]
        public void PostNewAccount_ReturnsNonNullResult()
        {
            // Arrange (Done in Setup method)

            // Act
            var result = _controller.PostNewAccount(new AccountViewModel());

            // Assert
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void PostNewAccount_GivenValidModel_CallsRepositoryWithModel()
        {
            // Arrange
            var newAccount = new AccountViewModel();

            // Act
            _controller.PostNewAccount(newAccount);

            // Assert
            _mockRepository.Verify(repo => repo.SaveAccount(newAccount));
        }

        [Test]
        public void PostNewAccount_GivenValidModel_ReturnsCreatedStatusCodeResult()
        {
            // Arrange
            var newAccount = new AccountViewModel();

            // Act
            var result = _controller.PostNewAccount(newAccount) as StatusCodeResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.Created));
        }

        [Test]
        public void PostNewAccount_GivenNoContent_ReturnsBadRequestWithMessage()
        {
            // Arrange (Done in setup methods)

            // Act
            var result = _controller.PostNewAccount(null) as BadRequestObjectResult;

            // Assert
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void PostNewAccount_GivenInvalidModel_ReturnsInvalidModelStateResultWithModelState()
        {
            // Arrange
            var invalidModel = new AccountViewModel();

            // Act
            _controller.ModelState.AddModelError(String.Empty, "Invalid");
            var result = _controller.PostNewAccount(invalidModel) as BadRequestObjectResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Value, Is.SameAs(_controller.ModelState));
        }

        [Test]
        public void PostNewAccount_GivenDuplicateAccount_ReturnsBadRequestWithMessage()
        {
            // Arrange (Done in setup methods)
            var duplicate = new AccountViewModel();
            _mockRepository.Setup(repo => repo.IsDuplicateAccount(duplicate))
                .Returns(true);

            // Act
            var result = _controller.PostNewAccount(duplicate) as BadRequestObjectResult;

            // Assert
            Assert.That(result, Is.Not.Null);
        }

        #endregion

        #region PutModifiedAccount (PUT: accounts/{accountId:int}) tests

        [Test]
        public void PutModifiedAccount_SpecifiesCorrectRoute()
        {
            // Arrange (Done in setup methods)

            // Act & Assert
            AssertActionRouteEquals("PutModifiedAccount", AccountApi.AccountUrl);
        }

        [Test]
        public void PutModifiedAccount_ReturnsNonNullResult()
        {
            // Arrange (Done in setup methods)

            // Act
            var result = _controller.PutModifiedAccount(_existingAccountId, _existingAccount);

            // Assert
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void PutModifiedAccount_GivenInvalidId_ReturnsBadRequestWithMessage()
        {
            // Arrange (Done in setup methods)

            // Act
            var result = _controller.PutModifiedAccount(0, _existingAccount) as BadRequestObjectResult;

            // Assert
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void PutModifiedAccount_GivenInvalidModelId_ReturnsBadRequestWithMessage()
        {
            // Arrange
            var account = new AccountViewModel() { Id = 0 };

            // Act
            var result = _controller.PutModifiedAccount(1, account) as BadRequestObjectResult;

            // Assert
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void PutModifiedAccount_GivenConflictingIdAndModelId_ReturnsBadRequestWithMessage()
        {
            // Arrange
            int conflictingAccountId = 4;

            // Act
            var result = _controller.PutModifiedAccount(conflictingAccountId, _existingAccount) as BadRequestObjectResult;

            // Assert
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void PutModifiedAccount_GivenNoContent_ReturnsBadRequestWithMessage()
        {
            // Arrange (Done in setup methods)
            int validId = 1;

            // Act
            var result = _controller.PutModifiedAccount(validId, null) as BadRequestObjectResult;

            // Assert
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void PutModifiedAccount_GivenInvalidModel_ReturnsInvalidModelStateResultWithModelState()
        {
            // Arrange
            var invalidModel = new AccountViewModel() { Id = _existingAccountId };

            // Act
            _controller.ModelState.AddModelError(String.Empty, "Invalid");
            var result = _controller.PutModifiedAccount(_existingAccountId, invalidModel) as BadRequestObjectResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Value, Is.SameAs(_controller.ModelState));
        }

        [Test]
        public void PutModifiedAccount_CallsRepositoryWithModel()
        {
            // Arrange (Done in setup methods)

            // Act
            _controller.PutModifiedAccount(_existingAccountId, _existingAccount);

            // Assert
            _mockRepository.Verify(repo => repo.SaveAccount(_existingAccount));
        }

        [Test]
        public void PutModifiedAccount_GivenValidIdAndModel_ReturnsOk()
        {
            // Arrange (Done in setup methods)

            // Act
            var result = _controller.PutModifiedAccount(_existingAccountId, _existingAccount) as OkResult;

            // Assert
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void PutModifiedAccount_GivenDuplicateAccount_ReturnsBadRequestWithMessage()
        {
            // Arrange (Done in setup methods)
            var duplicate = new AccountViewModel() { Id = _existingAccountId };
            _mockRepository.Setup(repo => repo.IsDuplicateAccount(duplicate))
                .Returns(true);

            // Act
            var result = _controller.PutModifiedAccount(_existingAccountId, duplicate) as BadRequestObjectResult;

            // Assert
            Assert.That(result, Is.Not.Null);
        }

        #endregion

        #region GetAccountDetail (GET: accounts/{accountId:int}/details) tests

        [Test]
        public void GetAccountDetail_SpecifiesCorrectRoute()
        {
            // Arrange (Done in setup methods)

            // Act & Assert
            AssertActionRouteEquals("GetAccountDetail", AccountApi.AccountDetailsUrl);
        }

        [Test]
        public void GetAccountDetail_ReturnsNotNullResult()
        {
            // Arrange (Done in setup methods)

            // Act
            var result = _controller.GetAccountDetail(1);

            // Assert
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void GetAccountDetail_CallsRepositoryWithAccountId()
        {
            // Arrange
            int accountId = 1;

            // Act
            _controller.GetAccountDetail(accountId);

            // Assert
            _mockRepository.Verify(repo => repo.GetAccountDetail(accountId));
        }

        [Test]
        public void GetAccountDetail_GivenExistingId_ReturnsJsonWithCorrectContentType()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.GetAccountDetail(_existingAccountId))
                .Returns(new AccountFullViewModel());

            // Act
            var result = _controller.GetAccountDetail(_existingAccountId) as JsonResult;

            // Assert
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void GetAccountDetail_GivenNonExistingId_ReturnsNotFound()
        {
            // Arrange
            int nonExistingId = 2;
            _mockRepository.Setup(repo => repo.GetAccountDetail(nonExistingId))
                .Returns((AccountFullViewModel)null);

            // Act
            var result = _controller.GetAccountDetail(nonExistingId) as NotFoundResult;

            // Assert
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void GetAccountDetail_GivenInvalidId_ReturnsNotFound()
        {
            // Arrange
            int invalidId = 0;
            _mockRepository.Setup(repo => repo.GetAccountDetail(It.Is<int>(val => val <= 0)))
                .Returns((AccountFullViewModel)null);

            // Act
            var result = _controller.GetAccountDetail(invalidId) as NotFoundResult;

            // Assert
            Assert.That(result, Is.Not.Null);
        }

        #endregion

        private Mock<IAccountRepository> _mockRepository;
        private AccountViewModel _existingAccount;
        private int _existingAccountId = 1;
        private int _fpId = 1;
        private int _branchId = 1;
    }
}
