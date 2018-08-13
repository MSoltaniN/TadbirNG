using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using SPPC.Framework.Common;
using SPPC.Framework.Helpers;
using SPPC.Framework.Mapper;
using SPPC.Framework.Service.Security;
using SPPC.Tadbir.Configuration;
using SPPC.Tadbir.Configuration.Models;
using SPPC.Tadbir.Model.Auth;
using SPPC.Tadbir.Model.Config;
using SPPC.Tadbir.Model.Core;
using SPPC.Tadbir.Model.Corporate;
using SPPC.Tadbir.Model.Finance;
using SPPC.Tadbir.Model.Metadata;
using SPPC.Tadbir.Model.Workflow;
using SPPC.Tadbir.Values;
using SPPC.Tadbir.ViewModel;
using SPPC.Tadbir.ViewModel.Auth;
using SPPC.Tadbir.ViewModel.Config;
using SPPC.Tadbir.ViewModel.Core;
using SPPC.Tadbir.ViewModel.Corporate;
using SPPC.Tadbir.ViewModel.Finance;
using SPPC.Tadbir.ViewModel.Metadata;
using SPPC.Tadbir.ViewModel.Workflow;

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
            MapConfigTypes(mapperConfig);
            MapCoreTypes(mapperConfig);
            MapMetadataTypes(mapperConfig);
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

            mapperConfig.CreateMap<User, UserBriefViewModel>()
                .ForMember(dest => dest.HasRole, opts => opts.UseValue(true));
            mapperConfig.CreateMap<User, RelatedItemsViewModel>()
                .ForMember(dest => dest.RelatedItems, opts => opts.Ignore());
            mapperConfig.CreateMap<User, RelatedItemViewModel>()
                .ForMember(
                    dest => dest.Name,
                    opts => opts.MapFrom(
                        src => String.Format("{0} {1}", src.Person.FirstName, src.Person.LastName)));
            mapperConfig.CreateMap<UserBriefViewModel, User>();
            mapperConfig.CreateMap<User, UserContextViewModel>()
                .ForMember(dest => dest.Roles, opts => opts.Ignore());

            mapperConfig.CreateMap<Role, RoleViewModel>()
                .ForMember(
                    dest => dest.Permissions,
                    opts => opts.MapFrom(
                        src => src.RolePermissions.Select(rp => rp.Permission).Select(perm => perm.Name).ToList()));
            mapperConfig.CreateMap<RoleViewModel, Role>()
                .ForMember(dest => dest.RolePermissions, opts => opts.Ignore());
            mapperConfig.CreateMap<Role, RelatedItemsViewModel>()
                .ForMember(dest => dest.RelatedItems, opts => opts.Ignore());
            mapperConfig.CreateMap<Role, RelatedItemViewModel>();
            mapperConfig.CreateMap<Role, KeyValue>()
                .ForMember(dest => dest.Key, opts => opts.MapFrom(src => src.Id.ToString()))
                .ForMember(dest => dest.Value, opts => opts.MapFrom(src => src.Name));

            mapperConfig.CreateMap<Permission, PermissionViewModel>()
                .ForMember(dest => dest.IsEnabled, opts => opts.UseValue(true));
            mapperConfig.CreateMap<PermissionViewModel, Permission>()
                .AfterMap((viewModel, model) => model.Group.Id = viewModel.GroupId);
            mapperConfig.CreateMap<Permission, PermissionBriefViewModel>()
                .ForMember(dest => dest.EntityName, opts => opts.MapFrom(src => src.Group.EntityName))
                .ForMember(dest => dest.Flags, opts => opts.MapFrom(src => src.Flag));

            mapperConfig.CreateMap<ViewRowPermission, ViewRowPermissionViewModel>()
                .ForMember(
                    dest => dest.Items,
                    opts => opts.MapFrom(src => !String.IsNullOrEmpty(src.Items)
                        ? src.Items
                            .Split(',')
                            .Select(item => Int32.Parse(item.Trim()))
                            .ToList()
                        : new List<int>()));
            mapperConfig.CreateMap<ViewRowPermissionViewModel, ViewRowPermission>()
                .ForMember(
                    dest => dest.Items,
                    opts => opts.MapFrom(src => src.Items.Count > 0
                        ? String.Join(",", src.Items)
                        : null))
                .AfterMap((viewModel, model) => model.Role.Id = viewModel.RoleId)
                .AfterMap((viewModel, model) => model.View.Id = viewModel.ViewId);
        }

        private static void MapFinanceTypes(IMapperConfigurationExpression mapperConfig)
        {
            mapperConfig.CreateMap<Account, AccountViewModel>()
                .ForMember(dest => dest.ChildCount, opts => opts.MapFrom(src => src.Children.Count));
            mapperConfig.CreateMap<Account, AccountItemBriefViewModel>()
                .ForMember(dest => dest.ChildCount, opts => opts.MapFrom(src => src.Children.Count));
            mapperConfig.CreateMap<AccountViewModel, Account>()
                .AfterMap((viewModel, model) => model.ParentId = viewModel.ParentId)
                .AfterMap((viewModel, model) => model.FiscalPeriod.Id = viewModel.FiscalPeriodId)
                .AfterMap((viewModel, model) => model.Branch.Id = viewModel.BranchId);
            mapperConfig.CreateMap<DetailAccount, DetailAccountViewModel>()
                .ForMember(dest => dest.ChildCount, opts => opts.MapFrom(src => src.Children.Count));
            mapperConfig.CreateMap<DetailAccount, AccountItemBriefViewModel>()
                .ForMember(dest => dest.ChildCount, opts => opts.MapFrom(src => src.Children.Count));
            mapperConfig.CreateMap<DetailAccountViewModel, DetailAccount>()
                .AfterMap((viewModel, model) => model.ParentId = viewModel.ParentId)
                .AfterMap((viewModel, model) => model.FiscalPeriod.Id = viewModel.FiscalPeriodId)
                .AfterMap((viewModel, model) => model.Branch.Id = viewModel.BranchId);
            mapperConfig.CreateMap<CostCenter, CostCenterViewModel>()
                .ForMember(dest => dest.ChildCount, opts => opts.MapFrom(src => src.Children.Count));
            mapperConfig.CreateMap<CostCenter, AccountItemBriefViewModel>()
                .ForMember(dest => dest.ChildCount, opts => opts.MapFrom(src => src.Children.Count));
            mapperConfig.CreateMap<CostCenterViewModel, CostCenter>()
                .AfterMap((viewModel, model) => model.ParentId = viewModel.ParentId)
                .AfterMap((viewModel, model) => model.FiscalPeriod.Id = viewModel.FiscalPeriodId)
                .AfterMap((viewModel, model) => model.Branch.Id = viewModel.BranchId);
            mapperConfig.CreateMap<Project, ProjectViewModel>()
                .ForMember(dest => dest.ChildCount, opts => opts.MapFrom(src => src.Children.Count));
            mapperConfig.CreateMap<Project, AccountItemBriefViewModel>()
                .ForMember(dest => dest.ChildCount, opts => opts.MapFrom(src => src.Children.Count));
            mapperConfig.CreateMap<ProjectViewModel, Project>()
                .AfterMap((viewModel, model) => model.ParentId = viewModel.ParentId)
                .AfterMap((viewModel, model) => model.FiscalPeriod.Id = viewModel.FiscalPeriodId)
                .AfterMap((viewModel, model) => model.Branch.Id = viewModel.BranchId);
            mapperConfig.CreateMap<Account, AccountFullViewModel>();
            mapperConfig.CreateMap<Account, KeyValue>()
                .ForMember(dest => dest.Key, opts => opts.MapFrom(src => src.Id.ToString()))
                .ForMember(dest => dest.Value, opts => opts.MapFrom(src => String.Format("{0} ({1})", src.Name, src.FullCode)));
            mapperConfig.CreateMap<DetailAccount, KeyValue>()
                .ForMember(dest => dest.Key, opts => opts.MapFrom(src => src.Id.ToString()))
                .ForMember(dest => dest.Value, opts => opts.MapFrom(src => String.Format("{0} ({1})", src.Name, src.FullCode)));
            mapperConfig.CreateMap<CostCenter, KeyValue>()
                .ForMember(dest => dest.Key, opts => opts.MapFrom(src => src.Id.ToString()))
                .ForMember(dest => dest.Value, opts => opts.MapFrom(src => String.Format("{0} ({1})", src.Name, src.FullCode)));
            mapperConfig.CreateMap<Project, KeyValue>()
                .ForMember(dest => dest.Key, opts => opts.MapFrom(src => src.Id.ToString()))
                .ForMember(dest => dest.Value, opts => opts.MapFrom(src => String.Format("{0} ({1})", src.Name, src.FullCode)));
            mapperConfig.CreateMap<Voucher, VoucherViewModel>()
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
                            .Sum()));

            mapperConfig.CreateMap<VoucherViewModel, Voucher>()
                .AfterMap((viewModel, model) => model.FiscalPeriod.Id = viewModel.FiscalPeriodId)
                .AfterMap((viewModel, model) => model.Branch.Id = viewModel.BranchId);
            mapperConfig.CreateMap<VoucherLine, VoucherLineViewModel>()
                .AfterMap((model, viewModel) => viewModel.FullAccount.AccountId = model.AccountId)
                .AfterMap((model, viewModel) => viewModel.FullAccount.DetailId = model.DetailId)
                .AfterMap((model, viewModel) => viewModel.FullAccount.CostCenterId = model.CostCenterId)
                .AfterMap((model, viewModel) => viewModel.FullAccount.ProjectId = model.ProjectId);
            mapperConfig.CreateMap<VoucherLineViewModel, VoucherLine>()
                .AfterMap((viewModel, model) => model.Voucher.Id = viewModel.VoucherId)
                .AfterMap((viewModel, model) => model.FiscalPeriod.Id = viewModel.FiscalPeriodId)
                .AfterMap((viewModel, model) => model.Branch.Id = viewModel.BranchId)
                .AfterMap((viewModel, model) => model.AccountId = viewModel.FullAccount.AccountId ?? 0)
                .AfterMap((viewModel, model) => model.DetailId = viewModel.FullAccount.DetailId)
                .AfterMap((viewModel, model) => model.CostCenterId = viewModel.FullAccount.CostCenterId)
                .AfterMap((viewModel, model) => model.ProjectId = viewModel.FullAccount.ProjectId)
                .AfterMap((viewModel, model) => model.CurrencyId = viewModel.CurrencyId ?? 0);

            mapperConfig.CreateMap<Voucher, KeyValue>()
                .ForMember(dest => dest.Key, opts => opts.MapFrom(src => src.Id.ToString()))
                .ForMember(
                    dest => dest.Value,
                    opts => opts.MapFrom(
                        src => String.Join(",", new[] { "VoucherDisplay", src.No, src.Date.ToShortDateString() })));

            mapperConfig.CreateMap<VoucherLine, KeyValue>()
                .ForMember(dest => dest.Key, opts => opts.MapFrom(src => src.Id.ToString()))
                .ForMember(
                    dest => dest.Value,
                    opts => opts.MapFrom(
                        src => String.Join("|",
                            new[] { "VoucherLineDisplay", src.Debit.ToString("C0"), src.Credit.ToString("C0"), src.Description })));

            mapperConfig.CreateMap<Currency, KeyValue>()
                .ForMember(dest => dest.Key, opts => opts.MapFrom(src => src.Id.ToString()))
                .ForMember(dest => dest.Value, opts => opts.MapFrom(src => src.Name));

            mapperConfig.CreateMap<FiscalPeriod, FiscalPeriodViewModel>();
            mapperConfig.CreateMap<FiscalPeriod, KeyValue>()
                .ForMember(dest => dest.Key, opts => opts.MapFrom(src => src.Id.ToString()))
                .ForMember(dest => dest.Value, opts => opts.MapFrom(src => src.Name));
            mapperConfig.CreateMap<FiscalPeriodViewModel, FiscalPeriod>()
               .AfterMap((viewModel, model) => model.Company.Id = viewModel.CompanyId);
            mapperConfig.CreateMap<FiscalPeriod, RelatedItemsViewModel>()
                .ForMember(dest => dest.RelatedItems, opts => opts.Ignore());
            mapperConfig.CreateMap<FiscalPeriod, RelatedItemViewModel>();
        }

        private static void MapCorporateTypes(IMapperConfigurationExpression mapperConfig)
        {
            mapperConfig.CreateMap<Company, KeyValue>()
                .ForMember(dest => dest.Key, opts => opts.MapFrom(src => src.Id.ToString()))
                .ForMember(dest => dest.Value, opts => opts.MapFrom(src => src.Name));
            mapperConfig.CreateMap<Branch, KeyValue>()
                .ForMember(dest => dest.Key, opts => opts.MapFrom(src => src.Id.ToString()))
                .ForMember(dest => dest.Value, opts => opts.MapFrom(src => src.Name));
            mapperConfig.CreateMap<Branch, BranchViewModel>()
                .ForMember(dest => dest.IsAccessible, opts => opts.UseValue(true))
                .ForMember(dest => dest.ChildCount, opts => opts.MapFrom(src => src.Children.Count));
            mapperConfig.CreateMap<BranchViewModel, Branch>()
                .AfterMap((viewModel, model) => model.Company.Id = viewModel.CompanyId)
                .AfterMap((viewModel, model) => model.ParentId = viewModel.ParentId);
            mapperConfig.CreateMap<Branch, RelatedItemsViewModel>()
                .ForMember(dest => dest.RelatedItems, opts => opts.Ignore());
            mapperConfig.CreateMap<Branch, RelatedItemViewModel>();
            mapperConfig.CreateMap<Company, CompanyViewModel>()
                .ForMember(dest => dest.ChildCount, opts => opts.MapFrom(src => src.Children.Count));
            mapperConfig.CreateMap<CompanyViewModel, Company>()
                .AfterMap((viewModel, model) => model.ParentId = viewModel.ParentId);
        }

        private static void MapWorkflowTypes(IMapperConfigurationExpression mapperConfig)
        {
            mapperConfig.CreateMap<WorkItem, WorkItemViewModel>();
            mapperConfig.CreateMap<WorkItem, InboxItemViewModel>()
                .ForMember(dest => dest.EntityNo, opts => opts.Ignore())
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
                        src => (src.Documents.Count > 0) ? src.Documents[0].Document.Id : 0))
                .ForMember(
                    dest => dest.EntityId,
                    opts => opts.MapFrom(
                        src => (src.Documents.Count > 0) ? src.Documents[0].EntityId : 0))
                .ForMember(
                    dest => dest.Date,
                    opts => opts.MapFrom(
                        src => JalaliDateTime.FromDateTime(src.Date).ToShortDateString()));
            mapperConfig.CreateMap<WorkItemViewModel, WorkItem>()
                .AfterMap((viewModel, model) => model.CreatedBy.Id = viewModel.CreatedById)
                .AfterMap((viewModel, model) => model.Target.Id = viewModel.TargetId);
            mapperConfig.CreateMap<WorkItemDocumentViewModel, WorkItemDocument>()
                .AfterMap((viewModel, model) => model.WorkItem.Id = viewModel.WorkItemId)
                .AfterMap((viewModel, model) => model.Document.Id = viewModel.DocumentId);
            mapperConfig.CreateMap<WorkItemViewModel, WorkItemHistory>()
                .ForMember(dest => dest.Id, opts => opts.Ignore())
                .ForMember(dest => dest.Action, opts => opts.MapFrom(src => src.PreviousAction))
                .AfterMap((viewModel, model) => model.Document.Id = viewModel.DocumentId)
                .AfterMap((viewModel, model) => model.User.Id = viewModel.CreatedById)
                .AfterMap((viewModel, model) =>
                    model.Role = (viewModel.TargetId > 0)
                        ? new Role()
                            {
                                Id = viewModel.TargetId
                            }
                        : null);
            mapperConfig.CreateMap<WorkItemHistory, HistoryItemViewModel>()
                .ForMember(
                    dest => dest.UserFullName,
                    opts => opts.MapFrom(
                        src => String.Format("{0} {1}", src.User.Person.FirstName, src.User.Person.LastName)))
                .ForMember(
                    dest => dest.Status,
                    opts => opts.MapFrom(
                        src => VoucherStatus.ToLocalValue(src.Document.Status.Name)))
                .ForMember(
                    dest => dest.OperationalStatus,
                    opts => opts.MapFrom(
                        src => DocumentStatusName.ToLocalValue(src.Document.OperationalStatus)));
            mapperConfig.CreateMap<WorkItemHistory, OutboxItemViewModel>()
                .ForMember(dest => dest.EntityNo, opts => opts.Ignore())
                .ForMember(
                    dest => dest.Date,
                    opts => opts.MapFrom(
                        src => JalaliDateTime.FromDateTime(src.Date).ToShortDateString()))
                .ForMember(
                    dest => dest.DocumentType,
                    opts => opts.MapFrom(
                        src => DocumentTypeName.ToLocalValue(src.Document.Type.Name)))
                .ForMember(
                    dest => dest.Action,
                    opts => opts.MapFrom(
                        src => DocumentActionName.ToLocalValue(src.Action)));

            mapperConfig.CreateMap<Dictionary<string, object>, WorkflowInstanceViewModel>()
                .ForMember(dest => dest.InstanceId, opts => opts.MapFrom(src => ValueOrDefault<string>(src, "InstanceId")))
                .ForMember(dest => dest.DocumentType, opts => opts.MapFrom(src => ValueOrDefault<string>(src, "DocumentType")))
                .ForMember(dest => dest.DocumentId, opts => opts.MapFrom(src => ValueOrDefault<int>(src, "DocumentId")))
                .ForMember(dest => dest.WorkflowName, opts => opts.MapFrom(src => ValueOrDefault<string>(src, "WorkflowName")))
                .ForMember(dest => dest.EditionName, opts => opts.MapFrom(src => ValueOrDefault<string>(src, "EditionName")))
                .ForMember(dest => dest.State, opts => opts.MapFrom(src => ValueOrDefault<string>(src, "State")))
                .ForMember(dest => dest.LastActor, opts => opts.MapFrom(src => ValueOrDefault<string>(src, "LastActor")))
                .ForMember(
                    dest => dest.LastActionDate,
                    opts => opts.MapFrom(src =>
                        ValueOrDefault<DateTime>(src, "LastActionDate") == default(DateTime)
                            ? String.Empty
                            : JalaliDateTime.FromDateTime(ValueOrDefault<DateTime>(src, "LastActionDate"))
                                .ToString()));
        }

        private static void MapCoreTypes(IMapperConfigurationExpression mapperConfig)
        {
            mapperConfig.CreateMap<OperationLog, OperationLogViewModel>()
                .ForMember(
                    dest => dest.UserFullName,
                    opts => opts.MapFrom(src => String.Format("{0} {1}", src.User.Person.FirstName, src.User.Person.LastName)));
            mapperConfig.CreateMap<OperationLogViewModel, OperationLog>();
            mapperConfig.CreateMap<DocumentAction, DocumentActionViewModel>()
                .ForMember(
                    dest => dest.LineId,
                    opts => opts.MapFrom(src => src.LineId ?? 0));
            mapperConfig.CreateMap<DocumentActionViewModel, Model.Core.DocumentAction>()
                .ForMember(
                    dest => dest.LineId,
                    opts => opts.MapFrom(
                        src => (src.LineId > 0) ? (int?)src.LineId : null))
                .AfterMap((viewModel, model) => model.CreatedBy.Id = viewModel.CreatedById)
                .AfterMap((viewModel, model) => model.ModifiedBy.Id = viewModel.ModifiedById)
                .AfterMap((viewModel, model) =>
                    model.ConfirmedBy = (viewModel.ConfirmedById > 0)
                        ? new User()
                            {
                                Id = viewModel.ConfirmedById
                            }
                        : null)
                .AfterMap((viewModel, model) =>
                    model.ApprovedBy = (viewModel.ApprovedById > 0)
                        ? new User()
                            {
                                Id = viewModel.ApprovedById
                            }
                        : null);
            mapperConfig.CreateMap<Document, DocumentViewModel>();
            mapperConfig.CreateMap<DocumentViewModel, Document>()
                .ForMember(
                    dest => dest.Actions,
                    opts => opts.Ignore())
                .AfterMap((viewModel, model) => Array.ForEach(
                    viewModel.Actions.ToArray(),
                    act => model.Actions.Add(_autoMapper.Map<Model.Core.DocumentAction>(act))))
                .AfterMap((viewModel, model) => model.Type.Id = viewModel.TypeId)
                .AfterMap((viewModel, model) => model.Status.Id = viewModel.StatusId);
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
                .ConvertUsing(MapConfigType<RelationsConfig>);
            mapperConfig.CreateMap<Setting, DateRangeConfig>()
                .ConvertUsing(MapConfigType<DateRangeConfig>);
            mapperConfig.CreateMap<Setting, NumberDisplayConfig>()
                .ConvertUsing(MapConfigType<NumberDisplayConfig>);
            mapperConfig.CreateMap<Setting, ListFormViewConfig>()
                .ConvertUsing(MapConfigType<ListFormViewConfig>);
            mapperConfig.CreateMap<Setting, EntityRowAccessConfig>()
                .ConvertUsing(MapConfigType<EntityRowAccessConfig>);
            mapperConfig.CreateMap<Property, ColumnViewConfig>()
                .ConvertUsing(prop => !String.IsNullOrEmpty(prop.Settings)
                    ? JsonHelper.To<ColumnViewConfig>(prop.Settings)
                    : new ColumnViewConfig(prop.Name));
            mapperConfig.CreateMap<UserSetting, ListFormViewConfig>()
                .ConvertUsing(cfg => JsonHelper.To<ListFormViewConfig>(cfg.Values));
        }

        private static void MapMetadataTypes(IMapperConfigurationExpression mapperConfig)
        {
            mapperConfig.CreateMap<Entity, EntityViewModel>();
            mapperConfig.CreateMap<Property, PropertyViewModel>();
            mapperConfig.CreateMap<Command, CommandViewModel>()
                .ForMember(dest => dest.Title, opts => opts.MapFrom(src => src.TitleKey));
            mapperConfig.CreateMap<Entity, KeyValue>()
                .ForMember(dest => dest.Key, opts => opts.MapFrom(src => src.Id.ToString()))
                .ForMember(dest => dest.Value, opts => opts.MapFrom(src => src.Name));
        }

        private static TValue ValueOrDefault<TValue>(IDictionary<string, object> dictionary, string key)
        {
            var value = (dictionary.ContainsKey(key))
                ? (TValue)dictionary[key]
                : default(TValue);
            return value;
        }

        private static TConfig MapConfigType<TConfig>(Setting setting)
        {
            Verify.ArgumentNotNull(setting, "setting");
            return JsonHelper.To<TConfig>(setting.Values);
        }

        private static ICryptoService _crypto;
        private static MapperConfiguration _configuration;
        private static IMapper _autoMapper;
    }
}
