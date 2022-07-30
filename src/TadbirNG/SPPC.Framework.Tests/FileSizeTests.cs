using NUnit.Framework;
using SPPC.Framework.Helpers;

namespace SPPC.Framework.Tests
{
    [TestFixture]
    public class FileSizeTests
    {
        [Test]
        public void FromKiloBytes_GivenRoundValue_ReturnsExpectedValue()
        {
            // Arrange & Act
            var bytes = FileSize.FromKiloBytes(RoundValue);

            // Assert
            Assert.That(bytes, Is.EqualTo(4096L));
        }

        [Test]
        public void FromKiloBytes_GivenFractionalValue_ReturnsExpectedValue()
        {
            // Arrange & Act
            var bytes = FileSize.FromKiloBytes(FractionalValue);

            // Assert
            Assert.That(bytes, Is.EqualTo(4342L));
        }

        [Test]
        public void FromMegaBytes_GivenRoundValue_ReturnsExpectedValue()
        {
            // Arrange & Act
            var bytes = FileSize.FromMegaBytes(RoundValue);

            // Assert
            Assert.That(bytes, Is.EqualTo(4194304L));
        }

        [Test]
        public void FromMegaBytes_GivenFractionalValue_ReturnsExpectedValue()
        {
            // Arrange & Act
            var bytes = FileSize.FromMegaBytes(FractionalValue);

            // Assert
            Assert.That(bytes, Is.EqualTo(4445962L));
        }

        [Test]
        public void FromGigaBytes_GivenRoundValue_ReturnsExpectedValue()
        {
            // Arrange & Act
            var bytes = FileSize.FromGigaBytes(RoundValue);

            // Assert
            Assert.That(bytes, Is.EqualTo(4294967296L));
        }

        [Test]
        public void FromGigaBytes_GivenFractionalValue_ReturnsExpectedValue()
        {
            // Arrange & Act
            var bytes = FileSize.FromGigaBytes(FractionalValue);

            // Assert
            Assert.That(bytes, Is.EqualTo(4552665334L));
        }

        [Test]
        public void ToKiloBytes_GivenSmallValue_ReturnsZero()
        {
            // Arrange & Act
            var kiloBytes = FileSize.ToKiloBytes(50, 1);

            // Assert
            Assert.That(kiloBytes, Is.EqualTo(0.0));
        }

        [Test]
        public void ToKiloBytes_GivenNormalValue_ReturnsExpectedValue()
        {
            // Arrange & Act
            var kiloBytes = FileSize.ToKiloBytes(5000, 3);

            // Assert
            Assert.That(kiloBytes, Is.EqualTo(4.883));
        }

        [Test]
        public void ToMegaBytes_GivenSmallValue_ReturnsZero()
        {
            // Arrange & Act
            var megaBytes = FileSize.ToMegaBytes(5000, 2);

            // Assert
            Assert.That(megaBytes, Is.EqualTo(0.0));
        }

        [Test]
        public void ToMegaBytes_GivenNormalValue_ReturnsExpectedValue()
        {
            // Arrange & Act
            var megaBytes = FileSize.ToMegaBytes(123456789, 3);

            // Assert
            Assert.That(megaBytes, Is.EqualTo(117.738));
        }

        [Test]
        public void ToGigaBytes_GivenSmallValue_ReturnsZero()
        {
            // Arrange & Act
            var gigaBytes = FileSize.ToGigaBytes(500000, 3);

            // Assert
            Assert.That(gigaBytes, Is.EqualTo(0.0));
        }

        [Test]
        public void ToGigaBytes_GivenNormalValue_ReturnsExpectedValue()
        {
            // Arrange & Act
            var gigaBytes = FileSize.ToGigaBytes(1234567890, 3);

            // Assert
            Assert.That(gigaBytes, Is.EqualTo(1.15));
        }

        private const double RoundValue = 4.0;
        private const double FractionalValue = 4.24;
    }
}
