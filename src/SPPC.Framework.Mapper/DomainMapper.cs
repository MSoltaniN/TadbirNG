using System;
using System.Collections.Generic;
using AutoMapper;

namespace SPPC.Framework.Mapper
{
    /// <summary>
    /// Provides support for mappings between model and view model classes.
    /// </summary>
    public partial class DomainMapper : IDomainMapper
    {
        static DomainMapper()
        {
            _configuration = new MapperConfiguration(config => RegisterMappings(config));
            _autoMapper = _configuration.CreateMapper();
        }

        /// <summary>
        /// Gets an object used for mapping configuration.
        /// </summary>
        public object Configuration
        {
            get { return _configuration; }
        }

        /// <summary>
        /// Maps source object to another object having a different type.
        /// </summary>
        /// <typeparam name="T">Type of target object</typeparam>
        /// <param name="source">Source object that should be mapped</param>
        /// <returns>The target object mapped from the source object</returns>
        public T Map<T>(object source)
        {
            return _autoMapper.Map<T>(source);
        }

        private static void RegisterMappings(IMapperConfigurationExpression mapperConfig)
        {
            MapCoreTypes(mapperConfig);
        }

        private static void MapCoreTypes(IMapperConfigurationExpression mapperConfig)
        {
            // TODO: Add domain mapping configuration statements similar to the following commented lines...
            //mapperConfig.CreateMap<Model, ViewModel>();
            //mapperConfig.CreateMap<ViewModel, Model>();
        }

        private static MapperConfiguration _configuration;
        private static IMapper _autoMapper;
    }
}
