using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Moq;
using NUnit.Framework;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Api;
using SPPC.Tadbir.Configuration.Models;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Persistence;
using SPPC.Tadbir.Security;
using SPPC.Tadbir.ViewModel.Finance;
using SPPC.Tadbir.Web.Api.Resources.Types;

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
            _mockLocalizer = new Mock<IStringLocalizer<AppStrings>>();
            _mockConfig = new Mock<IConfigRepository>();
        }

        [SetUp]
        public void Setup()
        {
            _mockLocalizer.Setup(loc => loc[It.IsAny<string>()])
                .Returns(new LocalizedString("Name", "Value"));
            _mockConfig.Setup(cfg => cfg.GetViewTreeConfigByViewAsync(ViewName.Account))
                .ReturnsAsync(new ViewTreeFullConfig());
            _controller = new AccountsController(
                _mockRepository.Object, _mockConfig.Object, _mockLocalizer.Object)
            {
                ControllerContext = TestContext
            };
            _existingAccount = new AccountViewModel() { Id = _existingAccountId };
        }

        #region GetEnvironmentAccountsAsync (GET: accounts) tests

        [Test]
        public void GetEnvironmentAccounts_HasAuthorizeRequestAttribute()
        {
            // Arrange

            // Act & Assert
            AssertActionIsSecured("GetEnvironmentAccountsAsync", SecureEntity.Account, (int)AccountPermissions.View);
        }

        [Test]
        public void GetEnvironmentAccounts_SpecifiesCorrectRoute()
        {
            // Arrange

            // Act & Assert
            AssertActionRouteEquals("GetEnvironmentAccountsAsync", AccountApi.EnvironmentAccountsUrl);
        }

        [Test]
        public async Task GetEnvironmentAccounts_ReturnsNonNullResult()
        {
            // Arrange

            // Act
            var result = await _controller.GetEnvironmentAccountsAsync();

            // Assert
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public async Task GetEnvironmentAccounts_CallsRepository()
        {
            // Arrange

            // Act
            await _controller.GetEnvironmentAccountsAsync();

            // Assert
            _mockRepository.Verify(repo => repo.GetAccountsAsync(It.IsAny<GridOptions>()));
        }

        [Test]
        public async Task GetEnvironmentAccounts_ReturnsJsonWithCorrectContentType()
        {
            // Arrange
            _mockRepository
                .Setup(repo => repo.GetAccountsAsync(It.IsAny<GridOptions>()))
                .ReturnsAsync(new List<AccountViewModel>());

            // Act
            var result = await _controller.GetEnvironmentAccountsAsync() as JsonResult;

            // Assert
            Assert.That(result.Value, Is.InstanceOf<IList<AccountViewModel>>());
        }

        #endregion

        #region GetAccountAsync (GET: accounts/{accountId}) tests

        [Test]
        public void GetAccountAsync_HasAuthorizeRequestAttribute()
        {
            // Arrange

            // Act & Assert
            AssertActionIsSecured("GetAccountAsync", SecureEntity.Account, (int)AccountPermissions.View);
        }

        [Test]
        public void GetAccountAsync_SpecifiesCorrectRoute()
        {
            // Arrange (Done in setup methods)

            // Act & Assert
            AssertActionRouteEquals("GetAccountAsync", AccountApi.AccountUrl);
        }

        [Test]
        public async Task GetAccountAsync_ReturnsNotNullResult()
        {
            // Arrange (Done in setup methods)

            // Act
            var result = await _controller.GetAccountAsync(1);

            // Assert
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public async Task GetAccountAsync_CallsRepositoryWithAccountId()
        {
            // Arrange
            int accountId = 1;

            // Act
            await _controller.GetAccountAsync(accountId);

            // Assert
            _mockRepository.Verify(repo => repo.GetAccountAsync(accountId));
        }

        [Test]
        public async Task GetAccountAsync_GivenExistingId_ReturnsJsonWithCorrectContentType()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.GetAccountAsync(_existingAccountId))
                .ReturnsAsync(new AccountViewModel());

            // Act
            var result = await _controller.GetAccountAsync(_existingAccountId) as JsonResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Value, Is.InstanceOf<AccountViewModel>());
        }

        [Test]
        public async Task GetAccountAsync_GivenNonExistingId_ReturnsNotFound()
        {
            // Arrange
            int nonExistingId = 2;
            _mockRepository.Setup(repo => repo.GetAccountAsync(nonExistingId))
                .ReturnsAsync((AccountViewModel)null);

            // Act
            var result = await _controller.GetAccountAsync(nonExistingId) as NotFoundResult;

            // Assert
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public async Task GetAccountAsync_GivenInvalidId_ReturnsNotFound()
        {
            // Arrange
            int invalidId = 0;
            _mockRepository.Setup(repo => repo.GetAccountAsync(It.Is<int>(val => val <= 0)))
                .ReturnsAsync((AccountViewModel)null);

            // Act
            var result = await _controller.GetAccountAsync(invalidId) as NotFoundResult;

            // Assert
            Assert.That(result, Is.Not.Null);
        }

        #endregion

        #region PostNewAccountAsync (POST: accounts) tests

        [Test]
        public void PostNewAccountAsync_HasAuthorizeRequestAttribute()
        {
            // Arrange

            // Act & Assert
            AssertActionIsSecured("PostNewAccountAsync", SecureEntity.Account, (int)AccountPermissions.Create);
        }

        [Test]
        public void PostNewAccountAsync_SpecifiesCorrectRoute()
        {
            // Arrange (Done in setup methods)

            // Act & Assert
            AssertActionRouteEquals("PostNewAccountAsync", AccountApi.EnvironmentAccountsUrl);
        }

        [Test]
        public void PostNewAccountAsync_SpecifiesCorrectHttpVerb()
        {
            // Arrange

            // Act & Assert
            AssertActionHasVerbAttribute<HttpPostAttribute>("PostNewAccountAsync");
        }

        [Test]
        public async Task PostNewAccountAsync_ReturnsNonNullResult()
        {
            // Arrange (Done in Setup method)

            // Act
            var result = await _controller.PostNewAccountAsync(new AccountViewModel());

            // Assert
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public async Task PostNewAccountAsync_GivenValidModel_CallsRepositoryWithModel()
        {
            // Arrange
            var newAccount = new AccountViewModel() { BranchId = 1 };

            // Act
            await _controller.PostNewAccountAsync(newAccount);

            // Assert
            _mockRepository.Verify(repo => repo.SaveAccountAsync(newAccount));
        }

        [Test]
        public async Task PostNewAccountAsync_GivenValidModel_ReturnsObjectResultWithCreatedStatusCode()
        {
            // Arrange
            var newAccount = new AccountViewModel() { BranchId = 1 };

            // Act
            var result = await _controller.PostNewAccountAsync(newAccount) as ObjectResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.StatusCode, Is.EqualTo(StatusCodes.Status201Created));
        }

        [Test]
        public async Task PostNewAccountAsync_GivenNoContent_ReturnsBadRequestWithMessage()
        {
            // Arrange (Done in setup methods)

            // Act
            var result = await _controller.PostNewAccountAsync(null) as BadRequestObjectResult;

            // Assert
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public async Task PostNewAccountAsync_GivenInvalidModel_ReturnsBadRequestObjectResultWithCorrectValue()
        {
            // Arrange
            var invalidModel = new AccountViewModel();

            // Act
            _controller.ModelState.AddModelError(String.Empty, "Invalid");
            var result = await _controller.PostNewAccountAsync(invalidModel) as BadRequestObjectResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Value, Is.InstanceOf<SerializableError>());
            Assert.That((result.Value as SerializableError).Count, Is.EqualTo(1));
        }

        [Test]
        public async Task PostNewAccountAsync_GivenDuplicateAccount_ReturnsBadRequestWithMessage()
        {
            // Arrange (Done in setup methods)
            var duplicate = new AccountViewModel() { FullCode = "1234" };
            _mockRepository.Setup(repo => repo.IsDuplicateAccountAsync(duplicate))
                .ReturnsAsync(true);

            // Act
            var result = await _controller.PostNewAccountAsync(duplicate) as BadRequestObjectResult;

            // Assert
            Assert.That(result, Is.Not.Null);
        }

        #endregion

        ////#region PutModifiedAccount (PUT: accounts/{accountId}) tests

        ////[Test]
        ////public void PutModifiedAccount_HasAuthorizeRequestAttribute()
        ////{
        ////    // Arrange

        ////    // Act & Assert
        ////    AssertActionIsSecured("PutModifiedAccount", SecureEntity.Account, (int)AccountPermissions.Edit);
        ////}

        ////[Test]
        ////public void PutModifiedAccount_SpecifiesCorrectRoute()
        ////{
        ////    // Arrange (Done in setup methods)

        ////    // Act & Assert
        ////    AssertActionRouteEquals("PutModifiedAccount", AccountApi.AccountSyncUrl);
        ////}

        ////[Test]
        ////public void PutModifiedAccount_SpecifiesCorrectHttpVerb()
        ////{
        ////    // Arrange

        ////    // Act & Assert
        ////    AssertActionHasVerbAttribute<HttpPutAttribute>("PutModifiedAccount");
        ////}

        ////[Test]
        ////public void PutModifiedAccount_ReturnsNonNullResult()
        ////{
        ////    // Arrange (Done in setup methods)

        ////    // Act
        ////    var result = _controller.PutModifiedAccount(_existingAccountId, _existingAccount);

        ////    // Assert
        ////    Assert.That(result, Is.Not.Null);
        ////}

        ////[Test]
        ////public void PutModifiedAccount_GivenInvalidId_ReturnsBadRequestWithMessage()
        ////{
        ////    // Arrange (Done in setup methods)

        ////    // Act
        ////    var result = _controller.PutModifiedAccount(0, _existingAccount) as BadRequestObjectResult;

        ////    // Assert
        ////    Assert.That(result, Is.Not.Null);
        ////}

        ////[Test]
        ////public void PutModifiedAccount_GivenInvalidModelId_ReturnsBadRequestWithMessage()
        ////{
        ////    // Arrange
        ////    var account = new AccountViewModel() { Id = 0 };

        ////    // Act
        ////    var result = _controller.PutModifiedAccount(1, account) as BadRequestObjectResult;

        ////    // Assert
        ////    Assert.That(result, Is.Not.Null);
        ////}

        ////[Test]
        ////public void PutModifiedAccount_GivenConflictingIdAndModelId_ReturnsBadRequestWithMessage()
        ////{
        ////    // Arrange
        ////    int conflictingAccountId = 4;

        ////    // Act
        ////    var result = _controller.PutModifiedAccount(conflictingAccountId, _existingAccount) as BadRequestObjectResult;

        ////    // Assert
        ////    Assert.That(result, Is.Not.Null);
        ////}

        ////[Test]
        ////public void PutModifiedAccount_GivenNoContent_ReturnsBadRequestWithMessage()
        ////{
        ////    // Arrange (Done in setup methods)
        ////    int validId = 1;

        ////    // Act
        ////    var result = _controller.PutModifiedAccount(validId, null) as BadRequestObjectResult;

        ////    // Assert
        ////    Assert.That(result, Is.Not.Null);
        ////}

        ////[Test]
        ////public void PutModifiedAccount_GivenInvalidModel_ReturnsBadRequestObjectResultWithCorrectValue()
        ////{
        ////    // Arrange
        ////    var invalidModel = new AccountViewModel() { Id = _existingAccountId };

        ////    // Act
        ////    _controller.ModelState.AddModelError(String.Empty, "Invalid");
        ////    var result = _controller.PutModifiedAccount(_existingAccountId, invalidModel) as BadRequestObjectResult;

        ////    // Assert
        ////    Assert.That(result, Is.Not.Null);
        ////    Assert.That(result.Value, Is.InstanceOf<SerializableError>());
        ////    Assert.That((result.Value as SerializableError).Count, Is.EqualTo(1));
        ////}

        ////[Test]
        ////public void PutModifiedAccount_CallsRepositoryWithModel()
        ////{
        ////    // Arrange (Done in setup methods)

        ////    // Act
        ////    _controller.PutModifiedAccount(_existingAccountId, _existingAccount);

        ////    // Assert
        ////    _mockRepository.Verify(repo => repo.SaveAccount(_existingAccount));
        ////}

        ////[Test]
        ////public void PutModifiedAccount_GivenValidIdAndModel_ReturnsOk()
        ////{
        ////    // Arrange (Done in setup methods)

        ////    // Act
        ////    var result = _controller.PutModifiedAccount(_existingAccountId, _existingAccount) as OkResult;

        ////    // Assert
        ////    Assert.That(result, Is.Not.Null);
        ////}

        ////[Test]
        ////public void PutModifiedAccount_GivenDuplicateAccount_ReturnsBadRequestWithMessage()
        ////{
        ////    // Arrange (Done in setup methods)
        ////    var duplicate = new AccountViewModel() { Id = _existingAccountId };
        ////    _mockRepository.Setup(repo => repo.IsDuplicateAccount(duplicate))
        ////        .Returns(true);

        ////    // Act
        ////    var result = _controller.PutModifiedAccount(_existingAccountId, duplicate) as BadRequestObjectResult;

        ////    // Assert
        ////    Assert.That(result, Is.Not.Null);
        ////}

        ////#endregion

        ////#region GetAccountDetail (GET: accounts/{accountId}/details) tests

        ////[Test]
        ////public void GetAccountDetail_HasAuthorizeRequestAttribute()
        ////{
        ////    // Arrange

        ////    // Act & Assert
        ////    AssertActionIsSecured("GetAccountDetail", SecureEntity.Account, (int)AccountPermissions.View);
        ////}

        ////[Test]
        ////public void GetAccountDetail_SpecifiesCorrectRoute()
        ////{
        ////    // Arrange (Done in setup methods)

        ////    // Act & Assert
        ////    AssertActionRouteEquals("GetAccountDetail", AccountApi.AccountDetailsSyncUrl);
        ////}

        ////[Test]
        ////public void GetAccountDetail_ReturnsNotNullResult()
        ////{
        ////    // Arrange (Done in setup methods)

        ////    // Act
        ////    var result = _controller.GetAccountDetail(1);

        ////    // Assert
        ////    Assert.That(result, Is.Not.Null);
        ////}

        ////[Test]
        ////public void GetAccountDetail_CallsRepositoryWithAccountId()
        ////{
        ////    // Arrange
        ////    int accountId = 1;

        ////    // Act
        ////    _controller.GetAccountDetail(accountId);

        ////    // Assert
        ////    _mockRepository.Verify(repo => repo.GetAccountDetail(accountId));
        ////}

        ////[Test]
        ////public void GetAccountDetail_GivenExistingId_ReturnsJsonWithCorrectContentType()
        ////{
        ////    // Arrange
        ////    _mockRepository.Setup(repo => repo.GetAccountDetail(_existingAccountId))
        ////        .Returns(new AccountFullViewModel());

        ////    // Act
        ////    var result = _controller.GetAccountDetail(_existingAccountId) as JsonResult;

        ////    // Assert
        ////    Assert.That(result, Is.Not.Null);
        ////    Assert.That(result.Value, Is.InstanceOf<AccountFullViewModel>());
        ////}

        ////[Test]
        ////public void GetAccountDetail_GivenNonExistingId_ReturnsNotFound()
        ////{
        ////    // Arrange
        ////    int nonExistingId = 2;
        ////    _mockRepository.Setup(repo => repo.GetAccountDetail(nonExistingId))
        ////        .Returns((AccountFullViewModel)null);

        ////    // Act
        ////    var result = _controller.GetAccountDetail(nonExistingId) as NotFoundResult;

        ////    // Assert
        ////    Assert.That(result, Is.Not.Null);
        ////}

        ////[Test]
        ////public void GetAccountDetail_GivenInvalidId_ReturnsNotFound()
        ////{
        ////    // Arrange
        ////    int invalidId = 0;
        ////    _mockRepository.Setup(repo => repo.GetAccountDetail(It.Is<int>(val => val <= 0)))
        ////        .Returns((AccountFullViewModel)null);

        ////    // Act
        ////    var result = _controller.GetAccountDetail(invalidId) as NotFoundResult;

        ////    // Assert
        ////    Assert.That(result, Is.Not.Null);
        ////}

        ////#endregion

        private Mock<IAccountRepository> _mockRepository;
        private Mock<IStringLocalizer<AppStrings>> _mockLocalizer;
        private Mock<IConfigRepository> _mockConfig;
        private AccountViewModel _existingAccount;
        private int _existingAccountId = 1;
    }
}
