using AutoMapper;
using SPPC.Framework.Mapper;

namespace SPPC.Tools.TadbirDbConverter.Mapper
{
    public class TadbirMapper : IDomainMapper
    {
        static TadbirMapper()
        {
            _configuration = new MapperConfiguration(config => RegisterMappings(config));
            _autoMapper = _configuration.CreateMapper();
        }

        public object Configuration
        {
            get { return _configuration; }
        }

        public T Map<T>(object source)
        {
            return _autoMapper.Map<T>(source);
        }

        private static void RegisterMappings(IMapperConfigurationExpression mapperConfig)
        {
        }

        private static readonly MapperConfiguration _configuration;
        private static readonly IMapper _autoMapper;
    }
}
