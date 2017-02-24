using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using SPPC.Framework.Mapper;
using SPPC.Tadbir.Model.Finance;
using SPPC.Tadbir.ViewModel.Finance;
using SwForAll.Platform.Common;

namespace SPPC.Tadbir.Mapper
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
            MapFinanceTypes(mapperConfig);
        }

        private static void MapFinanceTypes(IMapperConfigurationExpression mapperConfig)
        {
            mapperConfig.CreateMap<Account, AccountViewModel>();
            mapperConfig.CreateMap<AccountViewModel, Account>();
            mapperConfig.CreateMap<Account, AccountFullViewModel>();
            mapperConfig.CreateMap<Transaction, TransactionFullViewModel>()
                .ForMember(
                    dest => dest.Transaction,
                    opts => opts.MapFrom(
                        src => _autoMapper.Map<TransactionViewModel>(src)));
            mapperConfig.CreateMap<Transaction, TransactionViewModel>()
                .ForMember(
                    dest => dest.DebitSum,
                    opts => opts.MapFrom(
                        src => src.Lines
                            .Select(line => line.Debit)
                            .Sum()))
                .ForMember(
                    dest => dest.CreditSum,
                    opts => opts.MapFrom(
                        src => src.Lines
                            .Select(line => line.Credit)
                            .Sum()))
                .ForMember(
                    dest => dest.Date,
                    opts => opts.MapFrom(
                        src => JalaliDateTime.FromDateTime(src.Date).ToShortDateString()));
            mapperConfig.CreateMap<TransactionViewModel, Transaction>()
                .ForMember(
                    dest => dest.Date,
                    opts => opts.MapFrom(
                        src => JalaliDateTime.Parse(src.Date).ToGregorian()));
            mapperConfig.CreateMap<TransactionLine, TransactionLineViewModel>();
        }

        private static MapperConfiguration _configuration;
        private static IMapper _autoMapper;
    }
}
