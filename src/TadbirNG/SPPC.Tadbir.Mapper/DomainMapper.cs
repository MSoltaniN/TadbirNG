using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using AutoMapper;
using SPPC.Framework.Common;
using SPPC.Framework.Cryptography;
using SPPC.Framework.Extensions;
using SPPC.Framework.Helpers;
using SPPC.Framework.Mapper;
using SPPC.Tadbir.Configuration;
using SPPC.Tadbir.Configuration.Models;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Mapper.ModelHelpers;
using SPPC.Tadbir.Model;
using SPPC.Tadbir.Model.Auth;
using SPPC.Tadbir.Model.Check;
using SPPC.Tadbir.Model.CashFlow;
using SPPC.Tadbir.Model.Config;
using SPPC.Tadbir.Model.Core;
using SPPC.Tadbir.Model.Corporate;
using SPPC.Tadbir.Model.Finance;
using SPPC.Tadbir.Model.Metadata;
using SPPC.Tadbir.Model.Reporting;
using SPPC.Tadbir.ViewModel;
using SPPC.Tadbir.ViewModel.Auth;
using SPPC.Tadbir.ViewModel.Check;
using SPPC.Tadbir.ViewModel.CashFlow;
using SPPC.Tadbir.ViewModel.Config;
using SPPC.Tadbir.ViewModel.Core;
using SPPC.Tadbir.ViewModel.Corporate;
using SPPC.Tadbir.ViewModel.Finance;
using SPPC.Tadbir.ViewModel.Metadata;
using SPPC.Tadbir.ViewModel.Reporting;
using SPPC.Tadbir.Resources;

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
            MapConfigTypes(mapperConfig);
            MapCoreTypes(mapperConfig);
            MapMetadataTypes(mapperConfig);
            MapReportingTypes(mapperConfig);
            MapCashFlowTypes(mapperConfig);
            MapCheckTypes(mapperConfig);
        }

        private static void MapCashFlowTypes(IMapperConfigurationExpression mapperConfig)
        {
            mapperConfig.CreateMap<CashRegister, CashRegisterViewModel>()
                .ForMember(dest => dest.Description, opts => opts.NullSubstitute(String.Empty));
            mapperConfig.CreateMap<CashRegisterViewModel, CashRegister>();
            mapperConfig.CreateMap<UserCashRegister, UserCashRegisterViewModel>();
            mapperConfig.CreateMap<UserCashRegisterViewModel, UserCashRegister>();
        }

        private static void MapCheckTypes(IMapperConfigurationExpression mapperConfig)
        {
            mapperConfig.CreateMap<CheckBook, CheckBookViewModel>()
                .ForMember(dest => dest.BankName, opts => opts.NullSubstitute(String.Empty))
                .ForMember(
                    dest => dest.FullAccount,
                    opts => opts.MapFrom(
                        src => BuildFullAccount(src.Account, src.DetailAccount, src.CostCenter, src.Project)));
            mapperConfig.CreateMap<CheckBookViewModel, CheckBook>()
                .AfterMap((viewModel, model) => model.AccountId = viewModel.FullAccount.Account.Id)
                .AfterMap((viewModel, model) => model.DetailAccountId = GetNullableId(viewModel.FullAccount.DetailAccount))
                .AfterMap((viewModel, model) => model.CostCenterId = GetNullableId(viewModel.FullAccount.CostCenter))
                .AfterMap((viewModel, model) => model.ProjectId = GetNullableId(viewModel.FullAccount.Project));
            mapperConfig.CreateMap<CheckBookPage, CheckBookPageViewModel>();
            mapperConfig.CreateMap<CheckBookPageViewModel, CheckBookPage>();
            mapperConfig.CreateMap<CheckBook, CheckBookReportViewModel>()
                .ForMember(dest => dest.IsArchivedName, opts => opts.MapFrom(src =>
                    src.IsArchived.HasValue && src.IsArchived.Value
                        ? AppStrings.BooleanYes
                        : AppStrings.BooleanNo));
        }

        private static void MapSecurityTypes(IMapperConfigurationExpression mapperConfig)
        {
            mapperConfig.CreateMap<User, UserViewModel>()
                .ForMember(dest => dest.Password, opts => opts.MapFrom(src => src.PasswordHash));
            mapperConfig.CreateMap<UserViewModel, User>()
                .ForMember(dest => dest.LastLoginDate, opts => opts.Ignore())
                .ForMember(dest => dest.PasswordHash,
                    opts => opts.MapFrom(
                        src => !String.IsNullOrEmpty(src.Password)
                            ? _crypto.CreateHash(src.Password).ToLower()
                            : String.Empty));
            mapperConfig.CreateMap<User, UserContextViewModel>()
                .ForMember(dest => dest.Roles, opts => opts.Ignore());
            mapperConfig.CreateMap<User, UserBriefViewModel>()
                .ForMember(dest => dest.HasRole, opts => opts.MapFrom(src => true));
            mapperConfig.CreateMap<UserBriefViewModel, User>();
            mapperConfig.CreateMap<User, RelatedItemsViewModel>()
                .ForMember(dest => dest.RelatedItems, opts => opts.Ignore());
            mapperConfig.CreateMap<User, RelatedItemViewModel>()
                .ForMember(
                    dest => dest.Name,
                    opts => opts.MapFrom(
                        src => String.Format("{0} {1}", src.Person.FirstName, src.Person.LastName)));

            mapperConfig.CreateMap<Role, RoleViewModel>()
                .ForMember(dest => dest.Description, opts => opts.NullSubstitute(String.Empty));
            mapperConfig.CreateMap<RoleViewModel, Role>()
                .ForMember(dest => dest.RolePermissions, opts => opts.Ignore());
            mapperConfig.CreateMap<Role, RelatedItemsViewModel>()
                .ForMember(dest => dest.RelatedItems, opts => opts.Ignore());
            mapperConfig.CreateMap<Role, RelatedItemViewModel>();
            mapperConfig.CreateMap<Role, KeyValue>()
                .ForMember(dest => dest.Key, opts => opts.MapFrom(src => src.Id.ToString()))
                .ForMember(dest => dest.Value, opts => opts.MapFrom(src => src.Name));

            mapperConfig.CreateMap<PermissionGroup, PermissionGroupViewModel>();
            mapperConfig.CreateMap<Permission, PermissionViewModel>()
                .ForMember(dest => dest.IsEnabled, opts => opts.MapFrom(src => true));
            mapperConfig.CreateMap<PermissionViewModel, Permission>();
            mapperConfig.CreateMap<Permission, PermissionBriefViewModel>()
                .ForMember(dest => dest.EntityName, opts => opts.MapFrom(src => src.Group.EntityName))
                .ForMember(dest => dest.Flags, opts => opts.MapFrom(src => src.Flag));

            mapperConfig.CreateMap<Session, SessionViewModel>();
            mapperConfig.CreateMap<SessionViewModel, Session>();

            mapperConfig.CreateMap<ViewRowPermission, ViewRowPermissionViewModel>()
                .ForMember(
                    dest => dest.Items,
                    opts => opts.MapFrom(src => !String.IsNullOrEmpty(src.Items)
                        ? src.Items
                            .Split(',', StringSplitOptions.None)
                            .Select(item => Int32.Parse(item.Trim()))
                            .ToList()
                        : new List<int>()));
            mapperConfig.CreateMap<ViewRowPermissionViewModel, ViewRowPermission>()
                .ForMember(
                    dest => dest.Items,
                    opts => opts.MapFrom(src => src.Items.Count > 0
                        ? String.Join(",", src.Items)
                        : null));
        }

        private static void MapFinanceTypes(IMapperConfigurationExpression mapperConfig)
        {
            mapperConfig.CreateMap<AccountCollectionCategory, AccountCollectionCategoryViewModel>();

            mapperConfig.CreateMap<AccountCollection, AccountCollectionViewModel>();

            mapperConfig.CreateMap<AccountCollectionAccount, AccountCollectionAccountViewModel>();
            mapperConfig.CreateMap<AccountCollectionAccountViewModel, AccountCollectionAccount>();
            mapperConfig.CreateMap<AccountCollectionAccount, AccountViewModel>()
                .ForMember(dest => dest.ChildCount, opts => opts.MapFrom(src => src.Account.Children.Count))
                .AfterMap((model, viewModel) => viewModel.Id = model.Account.Id)
                .AfterMap((model, viewModel) => viewModel.Name = model.Account.Name)
                .AfterMap((model, viewModel) => viewModel.FullCode = model.Account.FullCode)
                .AfterMap((model, viewModel) => viewModel.Level = model.Account.Level)
                .AfterMap((model, viewModel) => viewModel.Description = model.Account.Description);

            mapperConfig.CreateMap<AccountGroup, AccountGroupViewModel>()
                .ForMember(dest => dest.Description, opts => opts.NullSubstitute(String.Empty));
            mapperConfig.CreateMap<AccountGroupViewModel, AccountGroup>();
            mapperConfig.CreateMap<AccountGroup, KeyValue>()
                .ForMember(dest => dest.Key, opts => opts.MapFrom(src => src.Id.ToString()))
                .ForMember(dest => dest.Value, opts => opts.MapFrom(src => src.Name));
            mapperConfig.CreateMap<AccountGroup, AccountItemBriefViewModel>();

            mapperConfig.CreateMap<Account, AccountViewModel>()
                .ForMember(dest => dest.TurnoverMode, opts => opts.MapFrom(src => ((TurnoverMode)src.TurnoverMode).ToString()))
                .ForMember(dest => dest.Description, opts => opts.NullSubstitute(String.Empty))
                .ForMember(dest => dest.ChildCount, opts => opts.MapFrom(src => src.Children.Count));
            mapperConfig.CreateMap<Account, AccountItemBriefViewModel>()
                .ForMember(dest => dest.ChildCount, opts => opts.MapFrom(src => src.Children.Count))
                .ForMember(dest => dest.ParentId, opts => opts.MapFrom(src => src.ParentId));
            mapperConfig.CreateMap<AccountViewModel, Account>()
                .ForMember(dest => dest.TurnoverMode, opts => opts.MapFrom(src => (short)Enum.Parse(typeof(TurnoverMode), src.TurnoverMode)));
            mapperConfig.CreateMap<Account, KeyValue>()
                .ForMember(dest => dest.Key, opts => opts.MapFrom(src => src.Id.ToString()))
                .ForMember(dest => dest.Value, opts => opts.MapFrom(src => String.Format("{0} ({1})", src.Name, src.FullCode)));

            mapperConfig.CreateMap<DefaultAccountViewModel, Account>();

            mapperConfig.CreateMap<DetailAccount, DetailAccountViewModel>()
                .ForMember(dest => dest.Description, opts => opts.NullSubstitute(String.Empty))
                .ForMember(dest => dest.ChildCount, opts => opts.MapFrom(src => src.Children.Count));
            mapperConfig.CreateMap<DetailAccount, AccountItemBriefViewModel>()
                .ForMember(dest => dest.ChildCount, opts => opts.MapFrom(src => src.Children.Count));
            mapperConfig.CreateMap<DetailAccountViewModel, DetailAccount>();
            mapperConfig.CreateMap<DetailAccount, KeyValue>()
                .ForMember(dest => dest.Key, opts => opts.MapFrom(src => src.Id.ToString()))
                .ForMember(dest => dest.Value, opts => opts.MapFrom(src => String.Format("{0} ({1})", src.Name, src.FullCode)));

            mapperConfig.CreateMap<CostCenter, CostCenterViewModel>()
                .ForMember(dest => dest.Description, opts => opts.NullSubstitute(String.Empty))
                .ForMember(dest => dest.ChildCount, opts => opts.MapFrom(src => src.Children.Count));
            mapperConfig.CreateMap<CostCenter, AccountItemBriefViewModel>()
                .ForMember(dest => dest.ChildCount, opts => opts.MapFrom(src => src.Children.Count));
            mapperConfig.CreateMap<CostCenterViewModel, CostCenter>();
            mapperConfig.CreateMap<CostCenter, KeyValue>()
                .ForMember(dest => dest.Key, opts => opts.MapFrom(src => src.Id.ToString()))
                .ForMember(dest => dest.Value, opts => opts.MapFrom(src => String.Format("{0} ({1})", src.Name, src.FullCode)));

            mapperConfig.CreateMap<Project, ProjectViewModel>()
                .ForMember(dest => dest.Description, opts => opts.NullSubstitute(String.Empty))
                .ForMember(dest => dest.ChildCount, opts => opts.MapFrom(src => src.Children.Count));
            mapperConfig.CreateMap<Project, AccountItemBriefViewModel>()
                .ForMember(dest => dest.ChildCount, opts => opts.MapFrom(src => src.Children.Count));
            mapperConfig.CreateMap<ProjectViewModel, Project>();
            mapperConfig.CreateMap<Project, KeyValue>()
                .ForMember(dest => dest.Key, opts => opts.MapFrom(src => src.Id.ToString()))
                .ForMember(dest => dest.Value, opts => opts.MapFrom(src => String.Format("{0} ({1})", src.Name, src.FullCode)));

            mapperConfig.CreateMap<AccountItemBriefViewModel, Account>();
            mapperConfig.CreateMap<AccountItemBriefViewModel, DetailAccount>();
            mapperConfig.CreateMap<AccountItemBriefViewModel, CostCenter>();
            mapperConfig.CreateMap<AccountItemBriefViewModel, Project>();

            mapperConfig.CreateMap<Voucher, VoucherViewModel>()
                .ForMember(dest => dest.Description, opts => opts.NullSubstitute(String.Empty))
                .ForMember(dest => dest.Reference, opts => opts.NullSubstitute(String.Empty))
                .ForMember(dest => dest.Association, opts => opts.NullSubstitute(String.Empty))
                .ForMember(dest => dest.ConfirmerName, opts => opts.NullSubstitute(String.Empty))
                .ForMember(dest => dest.ApproverName, opts => opts.NullSubstitute(String.Empty))
                .ForMember(dest => dest.DebitSum, opts => opts.MapFrom(src => VoucherHelper.GetDebitSum(src)))
                .ForMember(dest => dest.CreditSum, opts => opts.MapFrom(src => VoucherHelper.GetCreditSum(src)))
                .ForMember(dest => dest.IsApproved, opts => opts.MapFrom(src => src.ApprovedById != null))
                .ForMember(dest => dest.IsConfirmed, opts => opts.MapFrom(src => src.ConfirmedById != null))
                .ForMember(dest => dest.TypeName, opts => opts.MapFrom(src => VoucherHelper.GetTypeName(src)));
            mapperConfig.CreateMap<Voucher, VoucherInfoViewModel>()
                .ForMember(dest => dest.IsApproved, opts => opts.MapFrom(src => src.ApprovedById != null))
                .ForMember(dest => dest.IsConfirmed, opts => opts.MapFrom(src => src.ConfirmedById != null));
            mapperConfig.CreateMap<Voucher, GroupActionResultViewModel>();
            mapperConfig.CreateMap<VoucherViewModel, Voucher>();
            mapperConfig.CreateMap<Voucher, KeyValue>()
                .ForMember(dest => dest.Key, opts => opts.MapFrom(src => src.Id.ToString()))
                .ForMember(
                    dest => dest.Value,
                    opts => opts.MapFrom(
                        src => String.Join(",", new[] { "VoucherDisplay", src.No.ToString(), src.Date.ToShortDateString() })));

            mapperConfig.CreateMap<VoucherLine, VoucherLineViewModel>()
                .ForMember(dest => dest.Description, opts => opts.NullSubstitute(String.Empty))
                .ForMember(dest => dest.CurrencyName, opts => opts.NullSubstitute(String.Empty))
                .ForMember(dest => dest.CurrencyValue, opts => opts.NullSubstitute(0.0M))
                .ForMember(
                    dest => dest.LineTypeId,
                    opts => opts.MapFrom(src => src.TypeId))
                .ForMember(
                    dest => dest.FullAccount,
                    opts => opts.MapFrom(
                        src => BuildFullAccount(src.Account, src.DetailAccount, src.CostCenter, src.Project)));
            mapperConfig.CreateMap<VoucherLineViewModel, VoucherLine>()
                .ForMember(
                    dest => dest.TypeId,
                    opts => opts.MapFrom(src => src.LineTypeId))
                .AfterMap((viewModel, model) => model.AccountId = viewModel.FullAccount.Account.Id)
                .AfterMap((viewModel, model) => model.DetailId = GetNullableId(viewModel.FullAccount.DetailAccount))
                .AfterMap((viewModel, model) => model.CostCenterId = GetNullableId(viewModel.FullAccount.CostCenter))
                .AfterMap((viewModel, model) => model.ProjectId = GetNullableId(viewModel.FullAccount.Project));
            mapperConfig.CreateMap<VoucherLine, KeyValue>()
                .ForMember(dest => dest.Key, opts => opts.MapFrom(src => src.Id.ToString()))
                .ForMember(
                    dest => dest.Value,
                    opts => opts.MapFrom(
                        src => String.Join("|",
                            new[] { "VoucherLineDisplay", src.Debit.ToString("C0"), src.Credit.ToString("C0"), src.Description })));
            mapperConfig.CreateMap<VoucherLine, VoucherLineAmountsViewModel>();

            mapperConfig.CreateMap<Currency, CurrencyViewModel>()
                .ForMember(dest => dest.Description, opts => opts.NullSubstitute(String.Empty))
                .ForMember(dest => dest.MinorUnitKey, opts => opts.MapFrom(src => src.MinorUnit));
            mapperConfig.CreateMap<CurrencyInfo, CurrencyViewModel>()
                .ForMember(dest => dest.Code, opts => opts.MapFrom(src => src.Currency.Code))
                .ForMember(dest => dest.DecimalCount, opts => opts.MapFrom(src => src.Currency.DecimalCount))
                .ForMember(dest => dest.MinorUnit, opts => opts.MapFrom(src => src.Currency.MinorUnitKey))
                .ForMember(dest => dest.MinorUnitKey, opts => opts.MapFrom(src => src.Currency.MinorUnitKey))
                .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.Currency.NameKey));
            mapperConfig.CreateMap<CurrencyViewModel, Currency>();
            mapperConfig.CreateMap<Currency, KeyValue>()
                .ForMember(dest => dest.Key, opts => opts.MapFrom(src => src.Id.ToString()))
                .ForMember(dest => dest.Value, opts => opts.MapFrom(src => src.Name));
            mapperConfig.CreateMap<Currency, CurrencyInfoViewModel>()
                .ForMember(dest => dest.LastRate, opts => opts.Ignore());

            mapperConfig.CreateMap<CurrencyRate, CurrencyRateViewModel>()
                .ForMember(dest => dest.Description, opts => opts.NullSubstitute(String.Empty));
            mapperConfig.CreateMap<CurrencyRateViewModel, CurrencyRate>();

            mapperConfig.CreateMap<FiscalPeriod, FiscalPeriodViewModel>()
                .ForMember(dest => dest.Description, opts => opts.NullSubstitute(String.Empty));
            mapperConfig.CreateMap<FiscalPeriodViewModel, FiscalPeriod>();
            mapperConfig.CreateMap<FiscalPeriod, KeyValue>()
                .ForMember(dest => dest.Key, opts => opts.MapFrom(src => src.Id.ToString()))
                .ForMember(dest => dest.Value, opts => opts.MapFrom(src => src.Name));
            mapperConfig.CreateMap<FiscalPeriod, RelatedItemsViewModel>()
                .ForMember(dest => dest.RelatedItems, opts => opts.Ignore());
            mapperConfig.CreateMap<FiscalPeriod, RelatedItemViewModel>();

            mapperConfig.CreateMap<TreeEntity, AccountItemBriefViewModel>();
            mapperConfig.CreateMap<AccountItemBriefViewModel, KeyValue>()
                .ForMember(dest => dest.Key, opts => opts.MapFrom(src => src.Id.ToString()))
                .ForMember(dest => dest.Value, opts => opts.MapFrom(src => String.Format("{0} ({1})", src.Name, src.FullCode)));

            mapperConfig.CreateMap<TaxCurrency, TaxCurrencyViewModel>();
            mapperConfig.CreateMap<TaxCurrencyViewModel, TaxCurrency>();
            mapperConfig.CreateMap<DbDataReader, TaxCurrencyViewModel>()
                .ForMember(dest => dest.Code, opts => opts.MapFrom(src => src["Code"]))
                .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src["Name"]));

            mapperConfig.CreateMap<CustomerTaxInfo, CustomerTaxInfoViewModel>();
            mapperConfig.CreateMap<CustomerTaxInfoViewModel, CustomerTaxInfo>();

            mapperConfig.CreateMap<AccountOwner, AccountOwnerViewModel>();
            mapperConfig.CreateMap<AccountOwnerViewModel, AccountOwner>();

            mapperConfig.CreateMap<AccountHolder, AccountHolderViewModel>();
            mapperConfig.CreateMap<AccountHolderViewModel, AccountHolder>();

            mapperConfig.CreateMap<Account, AccountFullDataViewModel>()
                .ForMember(dest => dest.Account, opt => opt.MapFrom(src => _autoMapper.Map<AccountViewModel>(src)))
                .ForMember(dest => dest.CustomerTaxInfo,
                    opt => opt.MapFrom(src => src.CustomerTaxInfo ?? new CustomerTaxInfo()))
                .ForMember(dest => dest.AccountOwner,
                    opt => opt.MapFrom(src => src.AccountOwner ?? new AccountOwner()));

            mapperConfig.CreateMap<BalanceByAccountItemViewModel, FullAccountCodeBranch>();
        }

        private static void MapCorporateTypes(IMapperConfigurationExpression mapperConfig)
        {
            mapperConfig.CreateMap<Branch, BranchViewModel>()
                .ForMember(dest => dest.Description, opts => opts.NullSubstitute(String.Empty));
            mapperConfig.CreateMap<BranchViewModel, Branch>();
            mapperConfig.CreateMap<Branch, KeyValue>()
                .ForMember(dest => dest.Key, opts => opts.MapFrom(src => src.Id.ToString()))
                .ForMember(dest => dest.Value, opts => opts.MapFrom(src => src.Name));
            mapperConfig.CreateMap<Branch, RelatedItemsViewModel>()
                .ForMember(dest => dest.RelatedItems, opts => opts.Ignore());
            mapperConfig.CreateMap<Branch, RelatedItemViewModel>();
            mapperConfig.CreateMap<Branch, AccountItemBriefViewModel>()
                .ForMember(dest => dest.ChildCount, opts => opts.MapFrom(src => src.Children.Count));
        }

        private static void MapCoreTypes(IMapperConfigurationExpression mapperConfig)
        {
            mapperConfig.CreateMap<OperationLog, OperationLogViewModel>()
                .ForMember(dest => dest.EntityName, opts => opts.NullSubstitute(String.Empty))
                .ForMember(dest => dest.EntityCode, opts => opts.NullSubstitute(String.Empty))
                .ForMember(dest => dest.EntityDescription, opts => opts.NullSubstitute(String.Empty))
                .ForMember(dest => dest.EntityTypeName, opts => opts.NullSubstitute(String.Empty))
                .ForMember(dest => dest.SourceName, opts => opts.NullSubstitute(String.Empty))
                .ForMember(dest => dest.SourceListName, opts => opts.NullSubstitute(String.Empty))
                .ForMember(dest => dest.Description, opts => opts.NullSubstitute(String.Empty));
            mapperConfig.CreateMap<OperationLogViewModel, OperationLog>();
            mapperConfig.CreateMap<SysOperationLog, OperationLogViewModel>()
                .ForMember(dest => dest.EntityName, opts => opts.NullSubstitute(String.Empty))
                .ForMember(dest => dest.EntityCode, opts => opts.NullSubstitute(String.Empty))
                .ForMember(dest => dest.EntityDescription, opts => opts.NullSubstitute(String.Empty))
                .ForMember(dest => dest.EntityTypeName, opts => opts.NullSubstitute(String.Empty))
                .ForMember(dest => dest.SourceName, opts => opts.NullSubstitute(String.Empty))
                .ForMember(dest => dest.SourceListName, opts => opts.NullSubstitute(String.Empty))
                .ForMember(dest => dest.Description, opts => opts.NullSubstitute(String.Empty))
                .ForMember(
                    dest => dest.UserName,
                    opts => opts.MapFrom(src => (src.User != null) ? src.User.UserName : String.Empty))
                .ForMember(
                    dest => dest.CompanyName,
                    opts => opts.MapFrom(src => (src.Company != null) ? src.Company.Name : String.Empty));
            mapperConfig.CreateMap<OperationLogViewModel, SysOperationLog>();
            mapperConfig.CreateMap<OperationLog, OperationLogArchive>();
            mapperConfig.CreateMap<OperationLogArchive, OperationLogViewModel>()
                .ForMember(dest => dest.EntityName, opts => opts.NullSubstitute(String.Empty))
                .ForMember(dest => dest.EntityCode, opts => opts.NullSubstitute(String.Empty))
                .ForMember(dest => dest.EntityDescription, opts => opts.NullSubstitute(String.Empty))
                .ForMember(dest => dest.EntityTypeName, opts => opts.NullSubstitute(String.Empty))
                .ForMember(dest => dest.SourceName, opts => opts.NullSubstitute(String.Empty))
                .ForMember(dest => dest.SourceListName, opts => opts.NullSubstitute(String.Empty))
                .ForMember(dest => dest.Description, opts => opts.NullSubstitute(String.Empty));
            mapperConfig.CreateMap<OperationLogArchive, OperationLog>();
            mapperConfig.CreateMap<SysOperationLog, SysOperationLogArchive>();
            mapperConfig.CreateMap<SysOperationLogArchive, SysOperationLog>();
            mapperConfig.CreateMap<SysOperationLogArchive, OperationLogViewModel>()
                .ForMember(dest => dest.EntityName, opts => opts.NullSubstitute(String.Empty))
                .ForMember(dest => dest.EntityCode, opts => opts.NullSubstitute(String.Empty))
                .ForMember(dest => dest.EntityDescription, opts => opts.NullSubstitute(String.Empty))
                .ForMember(dest => dest.EntityTypeName, opts => opts.NullSubstitute(String.Empty))
                .ForMember(dest => dest.SourceName, opts => opts.NullSubstitute(String.Empty))
                .ForMember(dest => dest.SourceListName, opts => opts.NullSubstitute(String.Empty))
                .ForMember(dest => dest.Description, opts => opts.NullSubstitute(String.Empty))
                .ForMember(
                    dest => dest.UserName,
                    opts => opts.MapFrom(src => (src.User != null) ? src.User.UserName : String.Empty))
                .ForMember(
                    dest => dest.CompanyName,
                    opts => opts.MapFrom(src => (src.Company != null) ? src.Company.Name : String.Empty));
            mapperConfig.CreateMap<Filter, FilterViewModel>();
            mapperConfig.CreateMap<FilterViewModel, Filter>();
            mapperConfig.CreateMap<SystemErrorViewModel, SystemError>();
        }

        private static void MapConfigTypes(IMapperConfigurationExpression mapperConfig)
        {
            mapperConfig.CreateMap<Setting, SettingBriefViewModel>()
                .ForMember(
                    dest => dest.Values,
                    opts => opts.MapFrom(
                        src => ConfigFactory.CreateFromJson(src.Values, src.ModelType)))
                .ForMember(
                    dest => dest.DefaultValues,
                    opts => opts.MapFrom(
                        src => ConfigFactory.CreateFromJson(src.DefaultValues, src.ModelType)))
                .ForMember(dest => dest.Title, opts => opts.MapFrom(src => src.TitleKey))
                .ForMember(dest => dest.Description, opts => opts.MapFrom(src => src.DescriptionKey));
            mapperConfig.CreateMap<Setting, RelationsConfig>()
                .ConvertUsing(src => MapConfigType<RelationsConfig>(src));
            mapperConfig.CreateMap<Setting, DateRangeConfig>()
                .ConvertUsing(src => MapConfigType<DateRangeConfig>(src));
            mapperConfig.CreateMap<Setting, NumberDisplayConfig>()
                .ConvertUsing(src => MapConfigType<NumberDisplayConfig>(src));
            mapperConfig.CreateMap<Setting, ListFormViewConfig>()
                .ConvertUsing(src => MapConfigType<ListFormViewConfig>(src));
            mapperConfig.CreateMap<Setting, EntityRowAccessConfig>()
                .ConvertUsing(src => MapConfigType<EntityRowAccessConfig>(src));
            mapperConfig.CreateMap<Setting, SystemConfig>()
                .ConvertUsing(src => MapConfigType<SystemConfig>(src));
            mapperConfig.CreateMap<Setting, FinanceReportConfig>()
                .ConvertUsing(src => MapConfigType<FinanceReportConfig>(src));
            mapperConfig.CreateMap<Column, ColumnViewConfig>()
                .ConvertUsing(prop => GetDynamicColumnSettings(prop));
            mapperConfig.CreateMap<UserSetting, ListFormViewConfig>()
                .ConvertUsing(cfg => JsonHelper.To<ListFormViewConfig>(cfg.Values));
            mapperConfig.CreateMap<ViewSetting, ViewTreeFullConfig>()
                .ForMember(
                    dest => dest.Current,
                    opts => opts.MapFrom(
                        src => JsonHelper.To<ViewTreeConfig>(src.Values)))
                .ForMember(
                    dest => dest.Default,
                    opts => opts.MapFrom(
                        src => JsonHelper.To<ViewTreeConfig>(src.DefaultValues)));
            mapperConfig.CreateMap<ViewTreeFullConfig, ViewSetting>()
                .ForMember(dest => dest.ViewId, opts => opts.MapFrom(src => src.Default.ViewId))
                .ForMember(dest => dest.ModelType, opts => opts.MapFrom(src => typeof(ViewTreeConfig).Name))
                .ForMember(dest => dest.SettingId, opts => opts.MapFrom(src => (int)SettingId.ViewTree))
                .ForMember(
                    dest => dest.Values,
                    opts => opts.MapFrom(
                        src => JsonHelper.From(src.Current, false, null, true)))
                .ForMember(
                    dest => dest.DefaultValues,
                    opts => opts.MapFrom(
                        src => JsonHelper.From(src.Default, false, null, true)));
            mapperConfig.CreateMap<LabelSetting, FormLabelFullConfig>()
                .ForMember(
                    dest => dest.Current,
                    opts => opts.MapFrom(
                        src => JsonHelper.To<FormLabelConfig>(src.Values)))
                .ForMember(
                    dest => dest.Default,
                    opts => opts.MapFrom(
                        src => JsonHelper.To<FormLabelConfig>(src.DefaultValues)));
            mapperConfig.CreateMap<Column, QuickSearchColumnConfig>()
                .ForMember(
                    dest => dest.Title,
                    opts => opts.MapFrom(src => src.Name))
                .ForMember(dest => dest.IsDisplayed, opts => opts.MapFrom(src => true))
                .ForMember(dest => dest.IsSearched, opts => opts.MapFrom(
                    src => src.Name == "FullCode" || src.Name == "Name"));
            mapperConfig.CreateMap<UserSetting, QuickSearchConfig>()
                .ConvertUsing(cfg => JsonHelper.To<QuickSearchConfig>(cfg.Values));
            mapperConfig.CreateMap<UserSetting, QuickReportConfig>()
                .ConvertUsing(cfg => JsonHelper.To<QuickReportConfig>(cfg.Values));

            mapperConfig.CreateMap<CompanyDb, CompanyDbViewModel>()
                .ForMember(dest => dest.UserName, opts => opts.NullSubstitute(String.Empty))
                .ForMember(dest => dest.Password, opts => opts.NullSubstitute(String.Empty))
                .ForMember(dest => dest.Description, opts => opts.NullSubstitute(String.Empty));
            mapperConfig.CreateMap<CompanyDbViewModel, CompanyDb>();
            mapperConfig.CreateMap<CompanyDb, KeyValue>()
                .ForMember(dest => dest.Key, opts => opts.MapFrom(src => src.Id.ToString()))
                .ForMember(dest => dest.Value, opts => opts.MapFrom(src => src.Name));
            mapperConfig.CreateMap<CompanyDb, RelatedItemsViewModel>()
                .ForMember(dest => dest.RelatedItems, opts => opts.Ignore());
            mapperConfig.CreateMap<CompanyDb, RelatedItemViewModel>();

            mapperConfig.CreateMap<LogSetting, LogSettingViewModel>();
            mapperConfig.CreateMap<LogSetting, LogSettingItemViewModel>();
            mapperConfig.CreateMap<SysLogSetting, LogSettingViewModel>();
            mapperConfig.CreateMap<SysLogSetting, LogSettingItemViewModel>();

            mapperConfig.CreateMap<UserValueCategory, KeyValue>()
                .ForMember(dest => dest.Key, opts => opts.MapFrom(src => src.Id.ToString()))
                .ForMember(dest => dest.Value, opts => opts.MapFrom(src => src.Name));
            mapperConfig.CreateMap<UserValueCategoryViewModel, UserValueCategory>();
            mapperConfig.CreateMap<UserValue, UserValueViewModel>();
            mapperConfig.CreateMap<UserValueViewModel, UserValue>();
        }

        private static void MapMetadataTypes(IMapperConfigurationExpression mapperConfig)
        {
            mapperConfig.CreateMap<View, ViewViewModel>();
            mapperConfig.CreateMap<View, ViewSummaryViewModel>();
            mapperConfig.CreateMap<Column, ColumnViewModel>();
            mapperConfig.CreateMap<Command, CommandViewModel>()
                .ForMember(dest => dest.Title, opts => opts.MapFrom(src => src.TitleKey));
            mapperConfig.CreateMap<View, KeyValue>()
                .ForMember(dest => dest.Key, opts => opts.MapFrom(src => src.Id.ToString()))
                .ForMember(dest => dest.Value, opts => opts.MapFrom(src => src.Name));
            mapperConfig.CreateMap<Province, KeyValue>()
                .ForMember(dest => dest.Key, opts => opts.MapFrom(src => src.Code))
                .ForMember(dest => dest.Value, opts => opts.MapFrom(src => src.Name));
            mapperConfig.CreateMap<City, KeyValue>()
               .ForMember(dest => dest.Key, opts => opts.MapFrom(src => src.Code))
               .ForMember(dest => dest.Value, opts => opts.MapFrom(src => src.Name));

            mapperConfig.CreateMap<ProvinceViewModel, Province>();
            mapperConfig.CreateMap<CityViewModel, City>();

            mapperConfig.CreateMap<ShortcutCommand, ShortcutCommandViewModel>();
        }

        private static void MapReportingTypes(IMapperConfigurationExpression mapperConfig)
        {
            mapperConfig.CreateMap<Report, ReportViewModel>()
                .ForMember(dest => dest.ResourceMap, opts => opts.Ignore());
            mapperConfig.CreateMap<LocalReport, LocalReportViewModel>();
            mapperConfig.CreateMap<Report, TreeItemViewModel>()
                .ForMember(dest => dest.ParentId, opts => opts.MapFrom(
                    src => src.Parent != null ? src.Parent.Id : (int?)null));
            mapperConfig.CreateMap<Report, PrintInfoViewModel>()
                .ForMember(dest => dest.Template, opts => opts.Ignore());
            mapperConfig.CreateMap<Report, ReportSummaryViewModel>();
            mapperConfig.CreateMap<Parameter, ParameterViewModel>();
            mapperConfig.CreateMap<Parameter, Parameter>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(src => 0))
                .ForMember(dest => dest.Report, opts => opts.MapFrom(src => (Report)null));

            mapperConfig.CreateMap<Voucher, VoucherSummaryViewModel>()
                .ForMember(dest => dest.Date, opts => opts.MapFrom(src => src.Date.Date.ToShortDateString(false)))
                .ForMember(dest => dest.DebitSum, opts => opts.MapFrom(src => VoucherHelper.GetDebitSum(src)))
                .ForMember(dest => dest.CreditSum, opts => opts.MapFrom(src => VoucherHelper.GetCreditSum(src)))
                .ForMember(
                    dest => dest.Difference,
                    opts => opts.MapFrom(
                        src => Math.Abs(VoucherHelper.GetDebitSum(src) - VoucherHelper.GetCreditSum(src))))
                .ForMember(
                    dest => dest.BalanceStatus,
                    opts => opts.MapFrom(src => VoucherHelper.GetBalanceStatus(src)));
            mapperConfig.CreateMap<Voucher, StandardVoucherViewModel>()
                .ForMember(dest => dest.Date, opts => opts.MapFrom(src => src.Date.Date.ToShortDateString(false)))
                .ForMember(dest => dest.Lines, opts => opts.Ignore());
            mapperConfig.CreateMap<VoucherLine, StandardVoucherLineViewModel>();
            mapperConfig.CreateMap<VoucherLine, VoucherLineDetailViewModel>();

            mapperConfig.CreateMap<SystemIssue, SystemIssueViewModel>()
                .ForMember(dest => dest.Title, opts => opts.MapFrom(src => src.TitleKey));

            mapperConfig.CreateMap<VoucherLineDetailViewModel, TestBalanceItemViewModel>()
                .ForMember(dest => dest.TurnoverDebit, opts => opts.MapFrom(src => src.Debit))
                .ForMember(dest => dest.TurnoverCredit, opts => opts.MapFrom(src => src.Credit))
                .ForMember(dest => dest.StartBalanceDebit, opts => opts.Ignore())
                .ForMember(dest => dest.StartBalanceCredit, opts => opts.Ignore())
                .ForMember(dest => dest.OperationSumDebit, opts => opts.Ignore())
                .ForMember(dest => dest.OperationSumCredit, opts => opts.Ignore())
                .ForMember(dest => dest.CorrectionsDebit, opts => opts.Ignore())
                .ForMember(dest => dest.CorrectionsCredit, opts => opts.Ignore())
                .ForMember(dest => dest.EndBalanceDebit, opts => opts.Ignore())
                .ForMember(dest => dest.EndBalanceCredit, opts => opts.Ignore());

            mapperConfig.CreateMap<ProfitLossItemViewModel, ProfitLossByItemsViewModel>()
                .ForMember(dest => dest.StartBalanceItem1, opts => opts.MapFrom(src => src.StartBalance))
                .ForMember(dest => dest.PeriodTurnoverItem1, opts => opts.MapFrom(src => src.PeriodTurnover))
                .ForMember(dest => dest.EndBalanceItem1, opts => opts.MapFrom(src => src.EndBalance))
                .ForMember(dest => dest.BalanceItem1, opts => opts.MapFrom(src => src.Balance));

            mapperConfig.CreateMap<DashboardTab, DashboardTabViewModel>()
                .ForMember(dest => dest.Widgets, opts => opts.Ignore());
            mapperConfig.CreateMap<DashboardTabViewModel, DashboardTab>()
                .ForMember(dest => dest.Widgets, opts => opts.Ignore());
            mapperConfig.CreateMap<WidgetFunction, WidgetFunctionViewModel>();
            mapperConfig.CreateMap<WidgetType, WidgetTypeViewModel>();
            mapperConfig.CreateMap<Widget, WidgetViewModel>();
            mapperConfig.CreateMap<WidgetViewModel, Widget>();
            mapperConfig.CreateMap<FullAccountViewModel, WidgetAccount>()
                .ForMember(dest => dest.AccountId, opts => opts.MapFrom(src => GetNullableId(src.Account)))
                .ForMember(dest => dest.DetailAccountId, opts => opts.MapFrom(src => GetNullableId(src.DetailAccount)))
                .ForMember(dest => dest.CostCenterId, opts => opts.MapFrom(src => GetNullableId(src.CostCenter)))
                .ForMember(dest => dest.ProjectId, opts => opts.MapFrom(src => GetNullableId(src.Project)))
                .ForMember(dest => dest.Account, opts => opts.Ignore())
                .ForMember(dest => dest.DetailAccount, opts => opts.Ignore())
                .ForMember(dest => dest.CostCenter, opts => opts.Ignore())
                .ForMember(dest => dest.Project, opts => opts.Ignore());
            mapperConfig.CreateMap<WidgetAccount, FullAccountViewModel>();
            mapperConfig.CreateMap<TabWidget, TabWidgetViewModel>()
                .ForMember(dest => dest.WidgetAccounts, opts => opts.Ignore())
                .ForMember(dest => dest.WidgetParameters, opts => opts.Ignore());
            mapperConfig.CreateMap<TabWidgetViewModel, TabWidget>();
        }

        private static TConfig MapConfigType<TConfig>(Setting setting)
        {
            Verify.ArgumentNotNull(setting, "setting");
            return JsonHelper.To<TConfig>(setting.Values);
        }

        private static int? GetNullableId(AccountItemBriefViewModel item)
        {
            return (item != null && item.Id > 0)
                ? item.Id
                : null;
        }

        private static FullAccountViewModel BuildFullAccount(
            Account account, DetailAccount detailAccount, CostCenter costCenter, Project project)
        {
            var fullAccount = new FullAccountViewModel();
            if (account != null)
            {
                fullAccount.Account = _autoMapper.Map<AccountItemBriefViewModel>(account);
            }

            if (detailAccount != null)
            {
                fullAccount.DetailAccount = _autoMapper.Map<AccountItemBriefViewModel>(detailAccount);
            }

            if (costCenter != null)
            {
                fullAccount.CostCenter = _autoMapper.Map<AccountItemBriefViewModel>(costCenter);
            }

            if (project != null)
            {
                fullAccount.Project = _autoMapper.Map<AccountItemBriefViewModel>(project);
            }

            return fullAccount;
        }

        private static ColumnViewConfig GetDynamicColumnSettings(Column column)
        {
            var columnConfig = new ColumnViewConfig(column.Name);
            var deviceConfig = new ColumnViewDeviceConfig()
            {
                Title = column.Name,
                Visibility = column.Visibility ?? ColumnVisibility.Visible,
                Width = 100,
                Index = column.DisplayIndex,
                DesignIndex = column.DisplayIndex
            };
            columnConfig.ExtraLarge = (ColumnViewDeviceConfig)deviceConfig.Clone();
            columnConfig.ExtraSmall = (ColumnViewDeviceConfig)deviceConfig.Clone();
            columnConfig.Large = (ColumnViewDeviceConfig)deviceConfig.Clone();
            columnConfig.Medium = (ColumnViewDeviceConfig)deviceConfig.Clone();
            columnConfig.Small = (ColumnViewDeviceConfig)deviceConfig.Clone();
            return columnConfig;
        }

        private static ICryptoService _crypto;
        private static readonly MapperConfiguration _configuration;
        private static readonly IMapper _autoMapper;
    }
}
