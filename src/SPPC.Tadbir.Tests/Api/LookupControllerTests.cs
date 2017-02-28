using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Results;
using Moq;
using NUnit.Framework;
using SPPC.Framework.Helpers;
using SPPC.Tadbir.Api;
using SPPC.Tadbir.NHibernate;

namespace SPPC.Tadbir.Web.Api.Controllers.Tests
{
    [TestFixture]
    [Category("WebApi")]
    public class LookupControllerTests : ApiControllerTestBase<LookupController>
    {
        [OneTimeSetUp]
        public void FixtureSetup()
        {
            _mockRepository = new Mock<ILookupRepository>();
        }

        [SetUp]
        public void Setup()
        {
            _controller = new LookupController(_mockRepository.Object);
        }

        #region GetAccountsLookup (GET: lookup/accounts/fp/{fpId:int}) tests

        [Test]
        public void GetAccountsLookup_SpecifiesCorrectRoute()
        {
            // Arrange

            // Act & Assert
            AssertActionRouteEquals("GetAccountsLookup", LookupApi.FiscalPeriodAccountsUrl);
        }

        [Test]
        public void GetAccountsLookup_ReturnsNotNullResult()
        {
            // Arrange

            // Act
            var result = _controller.GetAccountsLookup(_fpId);

            // Assert
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void GetAccountsLookup_CallsRepositoryWithFiscalPeriodId()
        {
            // Arrange

            // Act
            _controller.GetAccountsLookup(_fpId);

            // Assert
            _mockRepository.Verify(repo => repo.GetAccounts(_fpId));
        }

        [Test]
        public void GetAccountsLookup_ReturnsJsonWithCorrectContentType()
        {
            // Arrange

            // Act
            var result = _controller.GetAccountsLookup(_fpId) as JsonResult<IEnumerable<KeyValue>>;

            // Assert
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void GetAccountsLookup_GivenInvalidFiscalPeriodId_ReturnsNotFound()
        {
            // Arrange
            int invalidFpId = -2;

            // Act
            var result = _controller.GetAccountsLookup(invalidFpId) as NotFoundResult;

            // Assert
            Assert.That(result, Is.Not.Null);
        }

        #endregion

        #region GetCurrenciesLookup (GET: lookup/currencies) tests

        [Test]
        public void GetCurrenciesLookup_SpecifiesCorrectRoute()
        {
            // Arrange

            // Act & Assert
            AssertActionRouteEquals("GetCurrenciesLookup", LookupApi.CurrenciesUrl);
        }

        [Test]
        public void GetCurrenciesLookup_ReturnsNotNullResult()
        {
            // Arrange

            // Act
            var result = _controller.GetCurrenciesLookup();

            // Assert
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void GetCurrenciesLookup_CallsRepository()
        {
            // Arrange

            // Act
            _controller.GetCurrenciesLookup();

            // Assert
            _mockRepository.Verify(repo => repo.GetCurrencies());
        }

        [Test]
        public void GetCurrenciesLookup_ReturnsJsonWithCorrectContentType()
        {
            // Arrange

            // Act
            var result = _controller.GetCurrenciesLookup() as JsonResult<IEnumerable<KeyValue>>;

            // Assert
            Assert.That(result, Is.Not.Null);
        }

        #endregion

        private Mock<ILookupRepository> _mockRepository;
        private int _fpId = 1;
    }
}
