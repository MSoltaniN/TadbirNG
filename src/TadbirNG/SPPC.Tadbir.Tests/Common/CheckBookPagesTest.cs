using NUnit.Framework;
using SPPC.Tadbir.Persistence;

namespace SPPC.Tadbir.Common
{
    [TestFixture]
    public class CheckBookPagesTest
    {
        [Test]
        public void CountCtor_GivenValidFirstPage_SetsCorrectSerials()
        {
            // Arrange & Act
            var pages = new CheckBookPages("1001","1234567891234561", 4);
            var expectedSerials = new string[] { "1001", "1002", "1003", "1004" };

            // Assert
            CollectionAssert.AreEqual(expectedSerials, pages.Serials);
        }

        [Test]
        public void CountCtor_GivenValidFirstPageWithZeros_SetsCorrectSerials()
        {
            // Arrange & Act
            var pages = new CheckBookPages("001001", "1234567891234561", 4);
            var expectedSerials = new string[] { "001001", "001002", "001003", "001004" };

            // Assert
            CollectionAssert.AreEqual(expectedSerials, pages.Serials);
        }

        [Test]
        public void LastPageCtor_GivenValidPages_SetsCorrectSerialsAndCount()
        {
            // Arrange & Act
            var pages = new CheckBookPages("1001", "1004", "1234567891234561");
            var expectedSerials = new string[] { "1001", "1002", "1003", "1004" };

            // Assert
            CollectionAssert.AreEqual(expectedSerials, pages.Serials);
            Assert.That(pages.Count, Is.EqualTo(4));
        }

        [Test]
        public void LastPageCtor_GivenValidPagesWithZeros_SetsCorrectSerialsAndCount()
        {
            // Arrange & Act
            var pages = new CheckBookPages("001001", "001004", "1234567891234561");
            var expectedSerials = new string[] { "001001", "001002", "001003", "001004" };

            // Assert
            CollectionAssert.AreEqual(expectedSerials, pages.Serials);
            Assert.That(pages.Count, Is.EqualTo(4));
        }
    }
}
