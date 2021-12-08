using System;
using System.IO;
using Moq;
using NUnit.Framework;
using SPPC.Framework.Helpers;
using SPPC.Tadbir.Configuration.Models;
using SPPC.Tadbir.Persistence;
using SPPC.Tadbir.Tests;
using SPPC.Tadbir.ViewModel.Config;

namespace SPPC.Tadbir.Licensing.Tests
{
    [TestFixture]
    public class CompanyValidatorTests : TestBase
    {
        [OneTimeSetUp]
        public void FixtureSetup()
        {
            ReadEditionsConfig();
        }

        [Test]
        public void Validate_WhenModelIsNull_ThrowsArgumentNullException()
        {
            // Arrange
            var validator = GetValidator(_editions.Standard.MaxCompanies, true);

            // Act & Assert
            Assert.Throws<ArgumentNullException>(
                () => validator.Validate(null, _editions.Standard));
        }

        [Test]
        public void Validate_WhenModelIsUnexpected_ThrowsArgumentException()
        {
            // Arrange
            var validator = GetValidator(_editions.Standard.MaxCompanies, true);

            // Act & Assert
            Assert.Throws<ArgumentException>(
                () => validator.Validate(String.Empty, _editions.Standard));
        }

        [Test]
        public void Validate_InStandardEdition_WhenMaxCompaniesExceeded_ReturnsError()
        {
            // Arrange
            var validator = GetValidator(_editions.Standard.MaxCompanies, false);

            // Act
            var message = validator.Validate(new CompanyDbViewModel(), _editions.Standard);

            // Assert
            Assert.That(message, Is.Not.Empty);
        }

        [Test]
        public void Validate_InStandardEdition_WhenMaxCompaniesNotExceeded_ReturnsEmpty()
        {
            // Arrange
            var validator = GetValidator(_editions.Standard.MaxCompanies, true);

            // Act
            var message = validator.Validate(new CompanyDbViewModel(), _editions.Standard);

            // Assert
            Assert.That(message, Is.Empty);
        }

        [Test]
        public void Validate_InProfessionalEdition_WhenMaxCompaniesExceeded_ReturnsError()
        {
            // Arrange
            var validator = GetValidator(_editions.Professional.MaxCompanies, false);

            // Act
            var message = validator.Validate(new CompanyDbViewModel(), _editions.Professional);

            // Assert
            Assert.That(message, Is.Not.Empty);
        }

        [Test]
        public void Validate_InProfessionalEdition_WhenMaxCompaniesNotExceeded_ReturnsEmpty()
        {
            // Arrange
            var validator = GetValidator(_editions.Professional.MaxCompanies, true);

            // Act
            var message = validator.Validate(new CompanyDbViewModel(), _editions.Professional);

            // Assert
            Assert.That(message, Is.Empty);
        }

        private static void ReadEditionsConfig()
        {
            _editions = JsonHelper.To<EditionsConfig>(File.ReadAllText(_configPath));
        }

        private CompanyValidator GetValidator(int maxCompanies, bool canCreate)
        {
            _mockRepository = new Mock<IEditionRepository>();
            _mockRepository
                .Setup(repo => repo.CanCreteCompanyAsync(maxCompanies))
                .ReturnsAsync(canCreate);
            return new CompanyValidator(_mockRepository.Object, GetStringLocalizer());
        }

        private Mock<IEditionRepository> _mockRepository;
        private static EditionsConfig _editions;
    }
}
