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
    public partial class AccountsControllerTests
    {
        [Test]
        public void PostNewAccount_GivenDuplicateAccount_ReturnsBadRequestWithMessage()
        {
            // Arrange (Done in setup methods)
            var duplicate = new AccountViewModel();
            _mockRepository.Setup(repo => repo.IsDuplicateAccount(duplicate))
                .Returns(true);

            // Act
            var result = _controller.PostNewAccount(duplicate) as BadRequestErrorMessageResult;

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
            var result = _controller.PutModifiedAccount(_existingAccountId, duplicate) as BadRequestErrorMessageResult;

            // Assert
            Assert.That(result, Is.Not.Null);
        }

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
            var result = _controller.GetAccountDetail(_existingAccountId) as JsonResult<AccountFullViewModel>;

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
    }
}
