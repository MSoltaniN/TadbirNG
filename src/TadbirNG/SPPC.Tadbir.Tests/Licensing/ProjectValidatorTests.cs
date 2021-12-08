using System;
using System.IO;
using NUnit.Framework;
using SPPC.Framework.Helpers;
using SPPC.Tadbir.Configuration.Models;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Tests;
using SPPC.Tadbir.ViewModel.Finance;

namespace SPPC.Tadbir.Licensing.Tests
{
    [TestFixture]
    public class ProjectValidatorTests : TestBase
    {
        // NOTE: As current version of Moq doesn't support mocking extension methods
        // and custom localization is done using an extension method of IStringLocalizer,
        // we can't mock IStringLocalizer and must use a real implementation. However,
        // we're also doing file I/O in ReadEditionsConfig (to test against the latest edition configuration).
        // So, we're NOT doing pure unit testing here.
        [OneTimeSetUp]
        public void FixtureSetup()
        {
            _validator = new ProjectValidator(GetStringLocalizer());
            ReadEditionsConfig();
        }

        [Test]
        public void Validate_WhenModelIsNull_ThrowsArgumentNullException()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            Assert.Throws<ArgumentNullException>(
                () => _validator.Validate(null, _editions.Standard));
        }

        [Test]
        public void Validate_WhenModelIsUnexpected_ThrowsArgumentException()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            Assert.Throws<ArgumentException>(
                () => _validator.Validate(String.Empty, _editions.Standard));
        }

        [Test]
        public void Validate_InStandardEdition_WhenLevelIsTooDeep_ReturnsError()
        {
            // Arrange
            var model = new ProjectViewModel() { Level = (short)_editions.Standard.MaxProjectDepth };

            // Act
            var message = _validator.Validate(model, _editions.Standard);

            // Assert
            Assert.That(message, Is.Not.Empty);
        }

        [Test]
        public void Validate_InStandardEdition_WhenLevelIsOK_ReturnsEmpty()
        {
            // Arrange
            var model = new ProjectViewModel() { Level = (short)(_editions.Standard.MaxProjectDepth - 1) };

            // Act
            var message = _validator.Validate(model, _editions.Standard);

            // Assert
            Assert.That(message, Is.Empty);
        }

        [Test]
        public void Validate_InProfessionalEdition_WhenLevelIsTooDeep_ReturnsError()
        {
            // Arrange
            var model = new ProjectViewModel() { Level = (short)_editions.Professional.MaxProjectDepth };

            // Act
            var message = _validator.Validate(model, _editions.Professional);

            // Assert
            Assert.That(message, Is.Not.Empty);
        }

        [Test]
        public void Validate_InProfessionalEdition_WhenLevelIsOK_ReturnsEmpty()
        {
            // Arrange
            var model = new ProjectViewModel() { Level = (short)(_editions.Professional.MaxProjectDepth - 1) };

            // Act
            var message = _validator.Validate(model, _editions.Professional);

            // Assert
            Assert.That(message, Is.Empty);
        }

        [Test]
        public void Validate_InEnterpriseEdition_WhenLevelIsInvalid_ReturnsError()
        {
            // Arrange
            var model = new ProjectViewModel() { Level = AppConstants.MaxAccountTreeLevel };

            // Act
            var message = _validator.Validate(model, _editions.Enterprise);

            // Assert
            Assert.That(message, Is.Not.Empty);
        }

        [Test]
        public void Validate_InEnterpriseEdition_WhenLevelIsOK_ReturnsEmpty()
        {
            // Arrange
            var model = new ProjectViewModel() { Level = AppConstants.MaxAccountTreeLevel - 1 };

            // Act
            var message = _validator.Validate(model, _editions.Enterprise);

            // Assert
            Assert.That(message, Is.Empty);
        }

        private static void ReadEditionsConfig()
        {
            _editions = JsonHelper.To<EditionsConfig>(File.ReadAllText(_configPath));
        }

        private ProjectValidator _validator;
        private static EditionsConfig _editions;
    }
}
