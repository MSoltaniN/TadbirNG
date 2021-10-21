using System;
using System.Linq;
using AutoMapper;
using Moq;
using NUnit.Framework;
using SPPC.Framework.Cryptography;

namespace SPPC.Tadbir.Mapper.Tests
{
    public class DomainMapperTestBase
    {
        protected void SetUp()
        {
            _mockCrypto = new Mock<ICryptoService>();
            _mockCrypto.Setup(crypto => crypto.CreateHash(It.IsAny<string>()))
                .Returns("1234567890abcdef");
            Mapper = new DomainMapper(_mockCrypto.Object);
        }

        protected DomainMapper Mapper { get; private set; }

        protected void AssertMappingIsDefined<TSource, TDestination>()
        {
            var configurtion = Mapper.Configuration as MapperConfiguration;
            var mapping = configurtion.GetAllTypeMaps()
                .Where(map => map.SourceType == typeof(TSource)
                    && map.DestinationType == typeof(TDestination))
                .FirstOrDefault();

            // Assert
            Assert.That(mapping, Is.Not.Null);
        }

        protected void AssertMapperCanConvertFromSourceToDestination<TSource, TDestination>()
            where TSource : class, new()
            where TDestination : class, new()
        {
            var source = new TSource();
            Mapper.Map<TDestination>(source);
        }

        private Mock<ICryptoService> _mockCrypto;
    }
}
