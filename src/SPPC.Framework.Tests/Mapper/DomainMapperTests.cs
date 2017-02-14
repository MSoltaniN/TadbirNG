using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using NUnit.Framework;

namespace SPPC.Framework.Mapper.Tests
{
    [TestFixture]
    public class DomainMapperTests
    {
        [OneTimeSetUp]
        public void FixtureSetup()
        {
            _domainMapper = new DomainMapper();
        }

        #region Type Mapping Tests

        //// TODO: Verify domain mappings similar to the following example test methods...
        ////[Test]
        ////public void ContainsMappingFromModelToViewModel()
        ////{
        ////    // Arrange (Done in FixtureSetup)

        ////    // Act & Assert
        ////    AssertMappingIsDefined<Model, ViewModel>();
        ////}

        ////[Test]
        ////public void CanMapFromModelToViewModel()
        ////{
        ////    // Arrange (Done in FixtureSetup)

        ////    // Act & Assert
        ////    AssertMapperCanConvertFromSourceToDestination<Model, ViewModel>();
        ////}

        ////[Test]
        ////public void ContainsMappingFromViewModelToModel()
        ////{
        ////    // Arrange (Done in FixtureSetup)

        ////    // Act & Assert
        ////    AssertMappingIsDefined<ViewModel, Model>();
        ////}

        ////[Test]
        ////public void CanMapFromViewModelToModel()
        ////{
        ////    // Arrange (Done in FixtureSetup)

        ////    // Act & Assert
        ////    AssertMapperCanConvertFromSourceToDestination<ViewModel, Model>();
        ////}

        #endregion

        private void AssertMappingIsDefined<TSource, TDestination>()
        {
            var configurtion = _domainMapper.Configuration as MapperConfiguration;
            var mapping = configurtion.GetAllTypeMaps()
                .Where(map => map.SourceType == typeof(TSource)
                    && map.DestinationType == typeof(TDestination))
                .FirstOrDefault();

            // Assert
            Assert.That(mapping, Is.Not.Null);
        }

        private void AssertMapperCanConvertFromSourceToDestination<TSource, TDestination>()
            where TSource : class, new()
            where TDestination : class, new()
        {
            var source = new TSource();
            _domainMapper.Map<TDestination>(source);
        }

        private IDomainMapper _domainMapper;
    }
}
