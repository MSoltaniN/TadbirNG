using NUnit.Framework;

namespace SPPC.Tadbir.Configuration
{
    [TestFixture]
    public class DbVersionsTest
    {
        [Test]
        public void SystemDbVersion_ReturnsCorrectValue()
        {
            // Arrange & Act
            var systemDbVersion = DbVersions.SystemDbVersion;

            // Assert
            Assert.That(systemDbVersion, Is.EqualTo(DbVersionValues.SystemDbVersion));
        }

        [Test]
        public void CompanyDbVersion_ReturnsCorrectValue()
        {
            // Arrange & Act
            var companyDbVersion = DbVersions.CompanyDbVersion;

            // Assert
            Assert.That(companyDbVersion, Is.EqualTo(DbVersionValues.CompanyDbVersion));
        }
    }
}
