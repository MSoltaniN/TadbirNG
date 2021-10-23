using NUnit.Framework;

namespace SPPC.Framework.Helpers.Tests
{
    [TestFixture]
    [Category("Presentation")]
    public class ResXResourceReaderTests
    {
        [Test]
        public void ReadStringResources_GivenValidPath_ReadsFirstEntry()
        {
            // Arrange
            var reader = new ResXResourceReader(_resPath);

            // Act
            var resources = reader.StringResources;

            // Assert
            Assert.That(resources.Count, Is.GreaterThan(1));
        }

        private const string _resPath = @"..\..\..\src\TadbirNG\SPPC.Tadbir.Resources\AppStrings.resx";
    }
}
