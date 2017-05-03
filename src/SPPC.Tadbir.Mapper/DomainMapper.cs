using System;
using System.Linq;
using AutoMapper;
using SPPC.Framework.Helpers;
using SPPC.Framework.Mapper;
using SPPC.Framework.Service.Security;
using SPPC.Tadbir.Model.Auth;
using SPPC.Tadbir.Model.Corporate;
using SPPC.Tadbir.Model.Finance;
using SPPC.Tadbir.Model.Workflow;
using SPPC.Tadbir.Values;
using SPPC.Tadbir.ViewModel.Auth;
using SPPC.Tadbir.ViewModel.Corporate;
using SPPC.Tadbir.ViewModel.Finance;
using SPPC.Tadbir.ViewModel.Workflow;
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
        /// Initializes a new instance of the <see cref="DomainMapper"/> class.
        /// </summary>
        /// <param name="crypto">An <see cref="ICryptoService"/> implementation used for performing cryptography
        /// operations during class mapping.</param>
        public DomainMapper(ICryptoService crypto)
        {
            _crypto = crypto;
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
            MapSecurityTypes(mapperConfig);
            MapFinanceTypes(mapperConfig);
            MapCorporateTypes(mapperConfig);
            MapWorkflowTypes(mapperConfig);
        }

        private static void MapSecurityTypes(IMapperConfigurationExpression mapperConfig)
        {
            mapperConfig.CreateMap<User, UserViewModel>()
                .ForMember(dest => dest.LastLoginDate, opts => opts.MapFrom(
                    src => src.LastLoginDate.HasValue
                        ? JalaliDateTime.FromDateTime(src.LastLoginDate.Value).ToString()
                        : String.Empty))
                .ForMember(dest => dest.Password, opts => opts.MapFrom(src => src.PasswordHash));
            mapperConfig.CreateMap<UserViewModel, User>()
                .ForMember(dest => dest.LastLoginDate, opts => opts.Ignore())
                .ForMember(dest => dest.PasswordHash,
                    opts => opts.MapFrom(
                        src => !String.IsNullOrEmpty(src.Password)
                            ? _crypto.CreateHash(src.Password).ToLower()
                            : String.Empty));

            mapperConfig.CreateMap<User, UserBriefViewModel>()
                .ForMember(dest => dest.HasRole, opts => opts.UseValue(true));
            mapperConfig.CreateMap<UserBriefViewModel, User>();
            mapperConfig.CreateMap<User, UserContextViewModel>();

            mapperConfig.CreateMap<Role, RoleViewModel>()
                .ForMember(
                    dest => dest.Permissions,
                    opts => opts.MapFrom(
                        src => src.Permissions.Select(perm => perm.Name)));
            mapperConfig.CreateMap<RoleViewModel, Role>()
                .ForMember(dest => dest.Permissions, opts => opts.Ignore());
            mapperConfig.CreateMap<Role, RoleBranchesViewModel>()
                .ForMember(dest => dest.Branches, opts => opts.Ignore());
            mapperConfig.CreateMap<Role, RoleUsersViewModel>()
                .ForMember(dest => dest.Users, opts => opts.Ignore());

            mapperConfig.CreateMap<Permission, PermissionViewModel>()
                .ForMember(dest => dest.IsEnabled, opts => opts.UseValue(true));
            mapperConfig.CreateMap<PermissionViewModel, Permission>()
                .AfterMap((viewModel, model) => model.Group.Id = viewModel.GroupId);
            mapperConfig.CreateMap<Permission, PermissionBriefViewModel>()
                .ForMember(dest => dest.EntityName, opts => opts.MapFrom(src => src.Group.EntityName))
                .ForMember(dest => dest.Flags, opts => opts.MapFrom(src => src.Flag));
        }

        private static void MapFinanceTypes(IMapperConfigurationExpression mapperConfig)
        {
            mapperConfig.CreateMap<Account, AccountViewModel>();
            mapperConfig.CreateMap<AccountViewModel, Account>()
                .AfterMap((viewModel, model) => model.FiscalPeriod.Id = viewModel.FiscalPeriodId)
                .AfterMap((viewModel, model) => model.Branch.Id = viewModel.BranchId);
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
                .AfterMap((viewModel, model) => model.Branch.Id = viewModel.BranchId)
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
                .AfterMap((viewModel, model) => model.FiscalPeriod.Id = viewModel.FiscalPeriodId)
                .AfterMap((viewModel, model) => model.Branch.Id = viewModel.BranchId)
                .AfterMap((viewModel, model) => model.Account.Id = viewModel.AccountId)
                .AfterMap((viewModel, model) => model.Currency.Id = viewModel.CurrencyId);

            mapperConfig.CreateMap<Currency, KeyValue>()
                .ForMember(dest => dest.Key, opts => opts.MapFrom(src => src.Id.ToString()))
                .ForMember(dest => dest.Value, opts => opts.MapFrom(src => src.Name));
        }

        private static void MapCorporateTypes(IMapperConfigurationExpression mapperConfig)
        {
            mapperConfig.CreateMap<Branch, BranchViewModel>()
                .ForMember(dest => dest.IsAccessible, opts => opts.UseValue(true));
            mapperConfig.CreateMap<BranchViewModel, Branch>()
                .AfterMap((viewModel, model) => model.Company.Id = viewModel.CompanyId);
        }

        private static void MapWorkflowTypes(IMapperConfigurationExpression mapperConfig)
        {
            mapperConfig.CreateMap<WorkItem, WorkItemViewModel>();
            mapperConfig.CreateMap<WorkItem, InboxItemViewModel>()
                .ForMember(
                    dest => dest.CreatedBy,
                    opts => opts.MapFrom(
                        src => String.Format("{0} {1}", src.CreatedBy.Person.FirstName, src.CreatedBy.Person.LastName)))
                .ForMember(
                    dest => dest.Title,
                    opts => opts.MapFrom(
                        src => WorkItemTitle.ToLocalValue(src.Title)))
                .ForMember(
                    dest => dest.DocumentId,
                    opts => opts.MapFrom(
                        src => src.Documents[0].DocumentId))
                .ForMember(
                    dest => dest.DocumentType,
                    opts => opts.MapFrom(
                        src => DocumentType.ToLocalValue(src.DocumentType)))
                .ForMember(
                    dest => dest.Date,
                    opts => opts.MapFrom(
                        src => JalaliDateTime.FromDateTime(src.Date).ToShortDateString()));
            mapperConfig.CreateMap<WorkItemViewModel, WorkItem>()
                .AfterMap((viewModel, model) => model.CreatedBy.Id = viewModel.CreatedById)
                .AfterMap((viewModel, model) => model.Target.Id = viewModel.TargetId);
            mapperConfig.CreateMap<WorkItemDocumentViewModel, WorkItemDocument>()
                .AfterMap((viewModel, model) => model.WorkItem.Id = viewModel.WorkItemId);
            mapperConfig.CreateMap<WorkItemViewModel, WorkItemHistory>()
                .ForMember(dest => dest.Id, opts => opts.Ignore())
                .AfterMap((viewModel, model) => model.User.Id = viewModel.CreatedById);
        }

        private static ICryptoService _crypto;
        private static MapperConfiguration _configuration;
        private static IMapper _autoMapper;
    }
}
