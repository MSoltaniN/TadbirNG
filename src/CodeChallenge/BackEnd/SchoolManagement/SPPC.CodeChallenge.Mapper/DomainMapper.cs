using AutoMapper;
using SPPC.CodeChallenge.Common;
using SPPC.CodeChallenge.Model.Core;
using SPPC.CodeChallenge.Model.Metadata;
using SPPC.CodeChallenge.ViewModel.Core;
using SPPC.CodeChallenge.ViewModel.Metadata;

namespace SPPC.CodeChallenge.Mapper
{
    /// <summary>
    /// Provides support for mappings between Tadbir model and view model classes using the AutoMapper
    /// object mapping library.
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
            MapMetadataTypes(mapperConfig);
        }

        private static void MapCoreTypes(IMapperConfigurationExpression mapperConfig)
        {
            mapperConfig.CreateMap<School, SchoolViewModel>();
            mapperConfig.CreateMap<SchoolViewModel, School>();
        }

        private static void MapMetadataTypes(IMapperConfigurationExpression mapperConfig)
        {
            mapperConfig.CreateMap<Province, KeyValue>()
                .ForMember(dest => dest.Key, opts => opts.MapFrom(src => src.Id.ToString()))
                .ForMember(dest => dest.Value, opts => opts.MapFrom(src => src.Name));
            mapperConfig.CreateMap<City, KeyValue>()
               .ForMember(dest => dest.Key, opts => opts.MapFrom(src => src.Id.ToString()))
               .ForMember(dest => dest.Value, opts => opts.MapFrom(src => src.Name));

            mapperConfig.CreateMap<ProvinceViewModel, Province>();
            mapperConfig.CreateMap<CityViewModel, City>();
        }

        private static readonly MapperConfiguration _configuration;
        private static readonly IMapper _autoMapper;
    }
}
