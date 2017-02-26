using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using SPPC.Framework.Helpers;
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
            mapperConfig.CreateMap<AccountViewModel, Account>()
                .AfterMap((viewModel, model) => model.FiscalPeriod.Id = viewModel.FiscalPeriodId);
            mapperConfig.CreateMap<Account, AccountFullViewModel>();
            mapperConfig.CreateMap<Account, KeyValue>()
                .ForMember(dest => dest.Key, opts => opts.MapFrom(src => src.Id.ToString()))
                .ForMember(dest => dest.Value, opts => opts.MapFrom(src => String.Format("{0} ({1})", src.Name, src.Code)));
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
                        src => JalaliDateTime.Parse(src.Date).ToGregorian()))
                .AfterMap((viewModel, model) => model.FiscalPeriod.Id = viewModel.FiscalPeriodId)
                .AfterMap((viewModel, model) => model.Creator.Id = viewModel.CreatorId)
                .AfterMap((viewModel, model) => model.LastModifier.Id = viewModel.LastModifierId);
            mapperConfig.CreateMap<TransactionLine, TransactionLineViewModel>();
            mapperConfig.CreateMap<TransactionLine, TransactionLineFullViewModel>()
                .ForMember(
                    dest => dest.Article,
                    opts => opts.MapFrom(
                        src => _autoMapper.Map<TransactionLineViewModel>(src)));
            mapperConfig.CreateMap<TransactionLineViewModel, TransactionLine>()
                .AfterMap((viewModel, model) => model.Transaction.Id = viewModel.TransactionId)
                .AfterMap((viewModel, model) => model.Account.Id = viewModel.AccountId)
                .AfterMap((viewModel, model) => model.Currency.Id = viewModel.CurrencyId);

            mapperConfig.CreateMap<Currency, KeyValue>()
                .ForMember(dest => dest.Key, opts => opts.MapFrom(src => src.Id.ToString()))
                .ForMember(dest => dest.Value, opts => opts.MapFrom(src => src.Name));
        }

        private static MapperConfiguration _configuration;
        private static IMapper _autoMapper;
    }
}
