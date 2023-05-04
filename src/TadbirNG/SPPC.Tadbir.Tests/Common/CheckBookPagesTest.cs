using NUnit.Framework;
using SPPC.Tadbir.Persistence;

namespace SPPC.Tadbir.Common
{
    [TestFixture]
    public class CheckBookPagesTest
    {
        [Test]
        public void CountCtor_GivenValidFirstPageAndCheckSeriesNoAndSayyadStartNo_SetsCorrectSerials()
        {
            // Arrange & Act
            var pages = new CheckBookPages("ABC1001", "XYZ", "1234567890123456", 4);
            var expectedSerials = new string[] { "XYZ/ABC1001", "XYZ/ABC1002", "XYZ/ABC1003", "XYZ/ABC1004" };

            // Assert
            CollectionAssert.AreEqual(expectedSerials, pages.Serials);
        }

        [Test]
        public void CountCtor_GivenValidFirstPageAndCheckSeriesNoAndSayyadStartNo_SetsCorrectSayyadSerials()
        {
            // Arrange & Act
            var pages = new CheckBookPages("ABC1001", "XYZ", "1234567890123456", 4);
            var expectedSerials = new string[]
            {
                "1234567890123456", "1234567890123457", "1234567890123458", "1234567890123459"
            };

            // Assert
            CollectionAssert.AreEqual(expectedSerials, pages.SayyadSerials);
        }

        [Test]
        public void CountCtor_GivenValidFirstPageWithZerosAndCheckSeriesNoAndSayyadStartNo_SetsCorrectSerials()
        {
            // Arrange & Act
            var pages = new CheckBookPages("ABC001001", "XYZ", "1234567890123456", 4);
            var expectedSerials = new string[]
            {
                "XYZ/ABC001001", "XYZ/ABC001002", "XYZ/ABC001003", "XYZ/ABC001004"
            };

            // Assert
            CollectionAssert.AreEqual(expectedSerials, pages.Serials);
        }

        [Test]
        public void LastPageCtor_GivenValidPagesAndCheckSeriesNoAndSayyadStartNo_SetsCorrectSerialsAndCount()
        {
            // Arrange & Act
            var pages = new CheckBookPages("ABC1001", "ABC1004", "XYZ", "1234567890123456");
            var expectedSerials = new string[] { "XYZ/ABC1001", "XYZ/ABC1002", "XYZ/ABC1003", "XYZ/ABC1004" };

            // Assert
            CollectionAssert.AreEqual(expectedSerials, pages.Serials);
            Assert.That(pages.Count, Is.EqualTo(4));
        }

        [Test]
        public void LastPageCtor_GivenValidPagesAndCheckSeriesNoAndSayyadStartNo_SetsCorrectSayyadSerials()
        {
            // Arrange & Act
            var pages = new CheckBookPages("ABC1001", "ABC1004", "XYZ", "1234567890123456");
            var expectedSerials = new string[]
            {
                "1234567890123456", "1234567890123457", "1234567890123458", "1234567890123459"
            };

            // Assert
            CollectionAssert.AreEqual(expectedSerials, pages.SayyadSerials);
        }

        [Test]
        public void LastPageCtor_GivenValidPagesWithZerosAndCheckSeriesNoAndSayyadStartNo_SetsCorrectSerialsAndCount()
        {
            // Arrange & Act
            var pages = new CheckBookPages("ABC001001", "ABC001004", "XYZ", "1234567890123456");
            var expectedSerials = new string[]
            {
                "XYZ/ABC001001", "XYZ/ABC001002", "XYZ/ABC001003", "XYZ/ABC001004"
            };

            // Assert
            CollectionAssert.AreEqual(expectedSerials, pages.Serials);
            Assert.That(pages.Count, Is.EqualTo(4));
        }
    }
}
