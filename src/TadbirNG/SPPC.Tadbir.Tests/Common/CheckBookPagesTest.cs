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
            var pages = new CheckBookPages("ABC1001", 4);
            var expectedSerials = new string[] { "ABC1001", "ABC1002", "ABC1003", "ABC1004" };

            // Assert
            CollectionAssert.AreEqual(expectedSerials, pages.Serials);
        }

        [Test]
        public void CountCtor_GivenValidFirstPageWithZeros_SetsCorrectSerials()
        {
            // Arrange & Act
            var pages = new CheckBookPages("ABC001001", 4);
            var expectedSerials = new string[] { "ABC001001", "ABC001002", "ABC001003", "ABC001004" };

            // Assert
            CollectionAssert.AreEqual(expectedSerials, pages.Serials);
        }

        [Test]
        public void LastPageCtor_GivenValidPages_SetsCorrectSerialsAndCount()
        {
            // Arrange & Act
            var pages = new CheckBookPages("ABC1001", "ABC1004");
            var expectedSerials = new string[] { "ABC1001", "ABC1002", "ABC1003", "ABC1004" };

            // Assert
            CollectionAssert.AreEqual(expectedSerials, pages.Serials);
            Assert.That(pages.Count, Is.EqualTo(4));
        }

        [Test]
        public void LastPageCtor_GivenValidPagesWithZeros_SetsCorrectSerialsAndCount()
        {
            // Arrange & Act
            var pages = new CheckBookPages("ABC001001", "ABC001004");
            var expectedSerials = new string[] { "ABC001001", "ABC001002", "ABC001003", "ABC001004" };

            // Assert
            CollectionAssert.AreEqual(expectedSerials, pages.Serials);
            Assert.That(pages.Count, Is.EqualTo(4));
        }
    }
}
