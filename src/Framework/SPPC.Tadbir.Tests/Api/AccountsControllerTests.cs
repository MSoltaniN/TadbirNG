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
using SPPC.Tadbir.Resources;
using SPPC.Tadbir.Security;
using SPPC.Tadbir.Utility;
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
            _mockLocalizer = new Mock<IStringLocalizer<AppStrings>>();
            _mockConfig = new Mock<IConfigRepository>();
        }

        [SetUp]
        public void Setup()
        {
            _mockLocalizer.Setup(loc => loc[It.IsAny<string>()])
                .Returns(new LocalizedString("Name", "Value"));
            _mockConfig.Setup(cfg => cfg.GetViewTreeConfigByViewAsync(ViewId.Account))
                .ReturnsAsync(new ViewTreeFullConfig());
            _controller = new AccountsController(
                _mockRepository.Object, _mockConfig.Object, _mockLocalizer.Object, null)
            {
                ControllerContext = TestControllerContext
            };
            _existingAccount = new AccountFullDataViewModel()
            {
                Account = new AccountViewModel() { Id = _existingAccountId }
            };
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
                .ReturnsAsync(new PagedList<AccountViewModel>(new List<AccountViewModel>()));

            // Act
            var result = await _controller.GetEnvironmentAccountsAsync() as JsonResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Value, Is.InstanceOf<IList<AccountViewModel>>());
        }

        [Test]
        public async Task GetEnvironmentAccounts_SetsTotalCountInHttpResponseHeader()
        {
            // Arrange

            // Act
            await _controller.GetEnvironmentAccountsAsync();

            // Assert
            var totalCountValue = _controller.HttpContext.Response.Headers[AppConstants.TotalCountHeaderName];
            Assert.That(String.IsNullOrEmpty(totalCountValue), Is.False);
            int totalCount = Int32.Parse(totalCountValue);
            Assert.That(totalCount, Is.EqualTo(0));
        }

        #endregion

        #region GetAccountAsync (GET: accounts/{accountId:min(1)}) tests

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

        #endregion

        #region GetAccountChildrenAsync (GET: accounts/{accountId:min(1)}/children) tests

        [Test]
        public void GetAccountChildrenAsync_HasAuthorizeRequestAttribute()
        {
            // Arrange

            // Act & Assert
            AssertActionIsSecured("GetAccountChildrenAsync", SecureEntity.Account, (int)AccountPermissions.View);
        }

        [Test]
        public void GetAccountChildrenAsync_SpecifiesCorrectRoute()
        {
            // Arrange

            // Act & Assert
            AssertActionRouteEquals("GetAccountChildrenAsync", AccountApi.AccountChildrenUrl);
        }

        [Test]
        public async Task GetAccountChildrenAsync_ReturnsNonNullResult()
        {
            // Arrange

            // Act
            var result = await _controller.GetAccountChildrenAsync(_existingAccountId);

            // Assert
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public async Task GetAccountChildrenAsync_CallsRepository()
        {
            // Arrange

            // Act
            await _controller.GetAccountChildrenAsync(_existingAccountId);

            // Assert
            _mockRepository.Verify(repo => repo.GetAccountChildrenAsync(_existingAccountId));
        }

        [Test]
        public async Task GetAccountChildrenAsync_ReturnsJsonWithCorrectContentType()
        {
            // Arrange
            _mockRepository
                .Setup(repo => repo.GetAccountChildrenAsync(_existingAccountId))
                .ReturnsAsync(new List<AccountItemBriefViewModel>());

            // Act
            var result = await _controller.GetAccountChildrenAsync(_existingAccountId) as JsonResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Value, Is.InstanceOf<IList<AccountItemBriefViewModel>>());
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
            var result = await _controller.PostNewAccountAsync(new AccountFullDataViewModel());

            // Assert
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public async Task PostNewAccountAsync_GivenValidModel_CallsRepositoryWithModel()
        {
            // Arrange
            var newAccount = new AccountFullDataViewModel() { Account = new AccountViewModel() { BranchId = 1 } };

            // Act
            await _controller.PostNewAccountAsync(newAccount);

            // Assert
            _mockRepository.Verify(repo => repo.SaveAccountAsync(newAccount));
        }

        [Test]
        public async Task PostNewAccountAsync_GivenValidModel_ReturnsObjectResultWithCreatedStatusCode()
        {
            // Arrange
            var newAccount = new AccountFullDataViewModel() { Account = new AccountViewModel() { BranchId = 1 } };

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
            var invalidModel = new AccountFullDataViewModel();

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
            var duplicate = new AccountFullDataViewModel() { Account = new AccountViewModel() { FullCode = "1234" } };
            _mockRepository.Setup(repo => repo.IsDuplicateFullCodeAsync(duplicate.Account))
                .ReturnsAsync(true);

            // Act
            var result = await _controller.PostNewAccountAsync(duplicate) as BadRequestObjectResult;

            // Assert
            Assert.That(result, Is.Not.Null);
        }

        #endregion

        #region PutModifiedAccount (PUT: accounts/{accountId}) tests

        [Test]
        public void PutModifiedAccount_HasAuthorizeRequestAttribute()
        {
            // Arrange

            // Act & Assert
            AssertActionIsSecured("PutModifiedAccountAsync", SecureEntity.Account, (int)AccountPermissions.Edit);
        }

        [Test]
        public void PutModifiedAccount_SpecifiesCorrectRoute()
        {
            // Arrange (Done in setup methods)

            // Act & Assert
            AssertActionRouteEquals("PutModifiedAccountAsync", AccountApi.AccountUrl);
        }

        [Test]
        public void PutModifiedAccount_SpecifiesCorrectHttpVerb()
        {
            // Arrange

            // Act & Assert
            AssertActionHasVerbAttribute<HttpPutAttribute>("PutModifiedAccountAsync");
        }

        [Test]
        public async Task PutModifiedAccount_ReturnsNonNullResult()
        {
            // Arrange (Done in setup methods)

            // Act
            var result = await _controller.PutModifiedAccountAsync(_existingAccountId, _existingAccount);

            // Assert
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public async Task PutModifiedAccount_GivenInvalidId_ReturnsBadRequestWithMessage()
        {
            // Arrange (Done in setup methods)

            // Act
            var result = await _controller.PutModifiedAccountAsync(0, _existingAccount) as BadRequestObjectResult;

            // Assert
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public async Task PutModifiedAccount_GivenConflictingIdAndModelId_ReturnsBadRequestWithMessage()
        {
            // Arrange
            int conflictingAccountId = 4;

            // Act
            var result = await _controller.PutModifiedAccountAsync(conflictingAccountId, _existingAccount) as BadRequestObjectResult;

            // Assert
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public async Task PutModifiedAccount_GivenNoContent_ReturnsBadRequestWithMessage()
        {
            // Arrange (Done in setup methods)
            int validId = 1;

            // Act
            var result = await _controller.PutModifiedAccountAsync(validId, null) as BadRequestObjectResult;

            // Assert
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public async Task PutModifiedAccount_GivenInvalidModel_ReturnsBadRequestObjectResultWithCorrectValue()
        {
            // Arrange
            var invalidModel = new AccountFullDataViewModel()
            {
                Account = new AccountViewModel() { Id = _existingAccountId }
            };

            // Act
            _controller.ModelState.AddModelError(String.Empty, "Invalid");
            var result = await _controller.PutModifiedAccountAsync(_existingAccountId, invalidModel) as BadRequestObjectResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Value, Is.InstanceOf<SerializableError>());
            Assert.That((result.Value as SerializableError).Count, Is.EqualTo(1));
        }

        [Test]
        public async Task PutModifiedAccount_CallsRepositoryWithModel()
        {
            // Arrange (Done in setup methods)

            // Act
            await _controller.PutModifiedAccountAsync(_existingAccountId, _existingAccount);

            // Assert
            _mockRepository.Verify(repo => repo.SaveAccountAsync(_existingAccount));
        }

        [Test]
        public async Task PutModifiedAccount_GivenValidIdAndModel_ReturnsOk()
        {
            // Arrange (Done in setup methods)

            // Act
            var result = await _controller.PutModifiedAccountAsync(_existingAccountId, _existingAccount) as OkResult;

            // Assert
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public async Task PutModifiedAccount_GivenDuplicateAccount_ReturnsBadRequestWithMessage()
        {
            // Arrange (Done in setup methods)
            var duplicate = new AccountFullDataViewModel()
            {
                Account = new AccountViewModel() { Id = _existingAccountId }
            };
            _mockRepository.Setup(repo => repo.IsDuplicateFullCodeAsync(duplicate.Account))
                .ReturnsAsync(true);

            // Act
            var result = await _controller.PutModifiedAccountAsync(_existingAccountId, duplicate) as BadRequestObjectResult;

            // Assert
            Assert.That(result, Is.Not.Null);
        }

        #endregion

        private Mock<IAccountRepository> _mockRepository;
        private Mock<IStringLocalizer<AppStrings>> _mockLocalizer;
        private Mock<IConfigRepository> _mockConfig;
        private AccountFullDataViewModel _existingAccount;
        private int _existingAccountId = 1;
    }
}
