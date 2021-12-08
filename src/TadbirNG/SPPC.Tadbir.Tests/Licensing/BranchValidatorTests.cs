using System;
using System.IO;
using Moq;
using NUnit.Framework;
using SPPC.Framework.Helpers;
using SPPC.Tadbir.Configuration.Models;
using SPPC.Tadbir.Persistence;
using SPPC.Tadbir.Tests;
using SPPC.Tadbir.ViewModel.Corporate;

namespace SPPC.Tadbir.Licensing.Tests
{
    [TestFixture]
    public class BranchValidatorTests : TestBase
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
            var validator = GetValidator(_editions.Standard.MaxBranches, true);

            // Act & Assert
            Assert.Throws<ArgumentNullException>(
                () => validator.Validate(null, _editions.Standard));
        }

        [Test]
        public void Validate_WhenModelIsUnexpected_ThrowsArgumentException()
        {
            // Arrange
            var validator = GetValidator(_editions.Standard.MaxBranches, true);

            // Act & Assert
            Assert.Throws<ArgumentException>(
                () => validator.Validate(String.Empty, _editions.Standard));
        }

        [Test]
        public void Validate_InStandardEdition_WhenMaxBranchesExceeded_ReturnsError()
        {
            // Arrange
            var validator = GetValidator(_editions.Standard.MaxBranches, false);

            // Act
            var message = validator.Validate(new BranchViewModel(), _editions.Standard);

            // Assert
            Assert.That(message, Is.Not.Empty);
        }

        [Test]
        public void Validate_InStandardEdition_WhenMaxBranchesNotExceeded_ReturnsEmpty()
        {
            // Arrange
            var validator = GetValidator(_editions.Standard.MaxBranches, true);

            // Act
            var message = validator.Validate(new BranchViewModel(), _editions.Standard);

            // Assert
            Assert.That(message, Is.Empty);
        }

        [Test]
        public void Validate_InProfessionalEdition_WhenMaxBranchesExceeded_ReturnsError()
        {
            // Arrange
            var validator = GetValidator(_editions.Professional.MaxBranches, false);

            // Act
            var message = validator.Validate(new BranchViewModel(), _editions.Professional);

            // Assert
            Assert.That(message, Is.Not.Empty);
        }

        [Test]
        public void Validate_InProfessionalEdition_WhenMaxBranchesNotExceeded_ReturnsEmpty()
        {
            // Arrange
            var validator = GetValidator(_editions.Professional.MaxBranches, true);

            // Act
            var message = validator.Validate(new BranchViewModel(), _editions.Professional);

            // Assert
            Assert.That(message, Is.Empty);
        }

        [Test]
        public void Validate_InStandardEdition_WhenLevelIsTooDeep_ReturnsError()
        {
            // Arrange
            var validator = GetValidator(_editions.Standard.MaxBranches, true);
            var model = new BranchViewModel() { Level = (short)_editions.Standard.MaxBranchDepth};

            // Act
            var message = validator.Validate(model, _editions.Standard);

            // Assert
            Assert.That(message, Is.Not.Empty);
        }

        [Test]
        public void Validate_InStandardEdition_WhenLevelIsOK_ReturnsEmpty()
        {
            // Arrange
            var validator = GetValidator(_editions.Standard.MaxBranches, true);
            var model = new BranchViewModel() { Level = (short)_editions.Standard.MaxBranchDepth - 1 };

            // Act
            var message = validator.Validate(model, _editions.Standard);

            // Assert
            Assert.That(message, Is.Empty);
        }

        [Test]
        public void Validate_InProfessionalEdition_WhenLevelIsTooDeep_ReturnsError()
        {
            // Arrange
            var validator = GetValidator(_editions.Professional.MaxBranches, true);
            var model = new BranchViewModel() { Level = (short)_editions.Professional.MaxBranchDepth };

            // Act
            var message = validator.Validate(model, _editions.Professional);

            // Assert
            Assert.That(message, Is.Not.Empty);
        }

        [Test]
        public void Validate_InProfessionalEdition_WhenLevelIsOK_ReturnsEmpty()
        {
            // Arrange
            var validator = GetValidator(_editions.Professional.MaxBranches, true);
            var model = new BranchViewModel() { Level = (short)_editions.Professional.MaxBranchDepth - 1 };

            // Act
            var message = validator.Validate(model, _editions.Professional);

            // Assert
            Assert.That(message, Is.Empty);
        }

        private static void ReadEditionsConfig()
        {
            _editions = JsonHelper.To<EditionsConfig>(File.ReadAllText(_configPath));
        }

        private BranchValidator GetValidator(int maxBranches, bool canCreate)
        {
            _mockRepository = new Mock<IEditionRepository>();
            _mockRepository
                .Setup(repo => repo.CanCreteBranchAsync(maxBranches))
                .ReturnsAsync(canCreate);
            return new BranchValidator(_mockRepository.Object, GetStringLocalizer());
        }

        private Mock<IEditionRepository> _mockRepository;
        private static EditionsConfig _editions;
    }
}
