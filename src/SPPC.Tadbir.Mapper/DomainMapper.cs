using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BabakSoft.Platform.Common;
using SPPC.Framework.Helpers;
using SPPC.Framework.Mapper;
using SPPC.Framework.Service.Security;
using SPPC.Tadbir.Configuration;
using SPPC.Tadbir.Model.Auth;
using SPPC.Tadbir.Model.Contact;
using SPPC.Tadbir.Model.Core;
using SPPC.Tadbir.Model.Corporate;
using SPPC.Tadbir.Model.Finance;
using SPPC.Tadbir.Model.Inventory;
using SPPC.Tadbir.Model.Procurement;
using SPPC.Tadbir.Model.Workflow;
using SPPC.Tadbir.Values;
using SPPC.Tadbir.ViewModel.Auth;
using SPPC.Tadbir.ViewModel.Core;
using SPPC.Tadbir.ViewModel.Corporate;
using SPPC.Tadbir.ViewModel.Finance;
using SPPC.Tadbir.ViewModel.Inventory;
using SPPC.Tadbir.ViewModel.Procurement;
using SPPC.Tadbir.ViewModel.Settings;
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
            MapSettingsTypes(mapperConfig);
            MapProcurementTypes(mapperConfig);
            MapInventoryTypes(mapperConfig);
            MapContactTypes(mapperConfig);
            MapCoreTypes(mapperConfig);
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
            mapperConfig.CreateMap<User, UserContextViewModel>()
                .ForMember(dest => dest.Roles, opts => opts.Ignore());

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
            mapperConfig.CreateMap<DetailAccount, KeyValue>()
                .ForMember(dest => dest.Key, opts => opts.MapFrom(src => src.Id.ToString()))
                .ForMember(dest => dest.Value, opts => opts.MapFrom(src => String.Format("{0} ({1})", src.Name, src.FullCode)));
            mapperConfig.CreateMap<CostCenter, KeyValue>()
                .ForMember(dest => dest.Key, opts => opts.MapFrom(src => src.Id.ToString()))
                .ForMember(dest => dest.Value, opts => opts.MapFrom(src => String.Format("{0} ({1})", src.Name, src.FullCode)));
            mapperConfig.CreateMap<Project, KeyValue>()
                .ForMember(dest => dest.Key, opts => opts.MapFrom(src => src.Id.ToString()))
                .ForMember(dest => dest.Value, opts => opts.MapFrom(src => String.Format("{0} ({1})", src.Name, src.FullCode)));
            mapperConfig.CreateMap<FullAccount, FullAccountViewModel>();
            mapperConfig.CreateMap<FullAccountViewModel, FullAccount>()
                .AfterMap((viewModel, model) => model.Account.Id = viewModel.AccountId)
                .AfterMap((viewModel, model) => model.Detail.Id = viewModel.DetailId)
                .AfterMap((viewModel, model) => model.CostCenter.Id = viewModel.CostCenterId)
                .AfterMap((viewModel, model) => model.Project.Id = viewModel.ProjectId);
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
            mapperConfig.CreateMap<Transaction, TransactionSummaryViewModel>()
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

            mapperConfig.CreateMap<TransactionViewModel, Transaction>()
                .ForMember(
                    dest => dest.Date,
                    opts => opts.MapFrom(
                        src => JalaliDateTime.Parse(src.Date).ToGregorian()))
                .AfterMap((viewModel, model) => model.FiscalPeriod.Id = viewModel.FiscalPeriodId)
                .AfterMap((viewModel, model) => model.Branch.Id = viewModel.BranchId)
                .AfterMap((viewModel, model) => model.Document.Id = viewModel.Document.Id);
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
            mapperConfig.CreateMap<BusinessUnit, KeyValue>()
                .ForMember(dest => dest.Key, opts => opts.MapFrom(src => src.Id.ToString()))
                .ForMember(dest => dest.Value, opts => opts.MapFrom(src => src.Name));
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
                    dest => dest.Date,
                    opts => opts.MapFrom(
                        src => JalaliDateTime.FromDateTime(src.Date).ToShortDateString()))
                .ForMember(
                    dest => dest.UserFullName,
                    opts => opts.MapFrom(
                        src => String.Format("{0} {1}", src.User.Person.FirstName, src.User.Person.LastName)))
                .ForMember(
                    dest => dest.Status,
                    opts => opts.MapFrom(
                        src => TransactionStatus.ToLocalValue(src.Document.Status.Name)))
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

        private static void MapSettingsTypes(IMapperConfigurationExpression mapperConfig)
        {
            mapperConfig.CreateMap<WorkflowSettingsElement, WorkflowSettingsViewModel>();
            mapperConfig.CreateMap<WorkflowElement, WorkflowViewModel>()
                .ForMember(dest => dest.DefaultEdition, opts => opts.MapFrom(src => src.Editions.DefaultEdition));
            mapperConfig.CreateMap<WorkflowEditionElement, WorkflowEditionViewModel>();
        }

        private static void MapProcurementTypes(IMapperConfigurationExpression mapperConfig)
        {
            mapperConfig.CreateMap<RequisitionVoucherType, KeyValue>()
                .ForMember(dest => dest.Key, opts => opts.MapFrom(src => src.Id.ToString()))
                .ForMember(dest => dest.Value, opts => opts.MapFrom(src => src.Name));
            mapperConfig.CreateMap<RequisitionVoucher, VoucherSummaryViewModel>();
            mapperConfig.CreateMap<RequisitionVoucher, RequisitionFullViewModel>()
                .ForMember(
                    dest => dest.Voucher,
                    opts => opts.MapFrom(src => _autoMapper.Map<RequisitionVoucherViewModel>(src)))
                .ForMember(dest => dest.Lines, opts => opts.Ignore())
                .AfterMap((model, viewModel) => Array.ForEach(
                    model.Lines.ToArray(),
                    line => viewModel.Lines.Add(_autoMapper.Map<VoucherLineSummaryViewModel>(line))));
            mapperConfig.CreateMap<RequisitionVoucher, RequisitionVoucherViewModel>()
                .ForMember(
                    dest => dest.OrderedDate,
                    opts => opts.MapFrom(
                        src => JalaliDateTime.FromDateTime(src.OrderedDate).ToShortDateString()))
                .ForMember(
                    dest => dest.RequiredDate,
                    opts => opts.MapFrom(
                        src => src.RequiredDate.HasValue
                            ? JalaliDateTime.FromDateTime(src.RequiredDate.Value).ToShortDateString()
                            : String.Empty))
                .ForMember(
                    dest => dest.PromisedDate,
                    opts => opts.MapFrom(
                        src => src.PromisedDate.HasValue
                            ? JalaliDateTime.FromDateTime(src.PromisedDate.Value).ToShortDateString()
                            : String.Empty));
            mapperConfig.CreateMap<RequisitionVoucherViewModel, RequisitionVoucher>()
                .ForMember(
                    dest => dest.OrderedDate,
                    opts => opts.MapFrom(
                        src => JalaliDateTime.Parse(src.OrderedDate).ToGregorian()))
                .ForMember(
                    dest => dest.RequiredDate,
                    opts => opts.MapFrom(
                        src => !String.IsNullOrWhiteSpace(src.RequiredDate)
                            ? JalaliDateTime.Parse(src.RequiredDate).ToGregorian()
                            : (DateTime?)null))
                .ForMember(
                    dest => dest.PromisedDate,
                    opts => opts.MapFrom(
                        src => !String.IsNullOrWhiteSpace(src.PromisedDate)
                            ? JalaliDateTime.Parse(src.PromisedDate).ToGregorian()
                            : (DateTime?)null))
                .AfterMap((viewModel, model) => model.Type.Id = viewModel.TypeId)
                .AfterMap((viewModel, model) => model.Branch.Id = viewModel.BranchId)
                .AfterMap((viewModel, model) => model.FiscalPeriod.Id = viewModel.FiscalPeriodId)
                .AfterMap((viewModel, model) => model.Requester.Id = viewModel.RequesterId)
                .AfterMap((viewModel, model) => model.Receiver.Id = viewModel.ReceiverId)
                .AfterMap((viewModel, model) => model.RequesterUnit.Id = viewModel.RequesterUnitId)
                .AfterMap((viewModel, model) => model.ReceiverUnit.Id = viewModel.ReceiverUnitId)
                .AfterMap((viewModel, model) => model.Warehouse.Id = viewModel.WarehouseId);

            mapperConfig.CreateMap<RequisitionVoucherLine, VoucherLineSummaryViewModel>()
                .ForMember(
                    dest => dest.RequiredDate,
                    opts => opts.MapFrom(
                        src => JalaliDateTime.FromDateTime(src.RequiredDate).ToShortDateString()));
            mapperConfig.CreateMap<RequisitionVoucherLine, RequisitionVoucherLineViewModel>()
                .ForMember(
                    dest => dest.DocumentAction,
                    opts => opts.MapFrom(src => src.Action))
                .ForMember(
                    dest => dest.RequiredDate,
                    opts => opts.MapFrom(
                        src => JalaliDateTime.FromDateTime(src.RequiredDate).ToShortDateString()))
                .ForMember(
                    dest => dest.PromisedDate,
                    opts => opts.MapFrom(
                        src => src.PromisedDate.HasValue
                            ? JalaliDateTime.FromDateTime(src.PromisedDate.Value).ToShortDateString()
                            : String.Empty))
                .ForMember(
                    dest => dest.DeliveredDate,
                    opts => opts.MapFrom(
                        src => src.DeliveredDate.HasValue
                            ? JalaliDateTime.FromDateTime(src.DeliveredDate.Value).ToShortDateString()
                            : String.Empty))
                .ForMember(
                    dest => dest.LastOrderedDate,
                    opts => opts.MapFrom(
                        src => src.LastOrderedDate.HasValue
                            ? JalaliDateTime.FromDateTime(src.LastOrderedDate.Value).ToShortDateString()
                            : String.Empty));
            mapperConfig.CreateMap<RequisitionVoucherLineViewModel, RequisitionVoucherLine>()
                .ForMember(
                    dest => dest.Action,
                    opts => opts.MapFrom(src => src.DocumentAction))
                .ForMember(
                    dest => dest.RequiredDate,
                    opts => opts.MapFrom(
                        src => JalaliDateTime.Parse(src.RequiredDate).ToGregorian()))
                .ForMember(
                    dest => dest.PromisedDate,
                    opts => opts.MapFrom(
                        src => !String.IsNullOrWhiteSpace(src.PromisedDate)
                            ? JalaliDateTime.Parse(src.PromisedDate).ToGregorian()
                            : (DateTime?)null))
                .ForMember(
                    dest => dest.DeliveredDate,
                    opts => opts.MapFrom(
                        src => !String.IsNullOrWhiteSpace(src.DeliveredDate)
                            ? JalaliDateTime.Parse(src.DeliveredDate).ToGregorian()
                            : (DateTime?)null))
                .ForMember(
                    dest => dest.LastOrderedDate,
                    opts => opts.MapFrom(
                        src => !String.IsNullOrWhiteSpace(src.LastOrderedDate)
                            ? JalaliDateTime.Parse(src.LastOrderedDate).ToGregorian()
                            : (DateTime?)null))
                .AfterMap((viewModel, model) => model.Voucher.Id = viewModel.VoucherId)
                .AfterMap((viewModel, model) => model.Branch.Id = viewModel.BranchId)
                .AfterMap((viewModel, model) => model.FiscalPeriod.Id = viewModel.FiscalPeriodId)
                .AfterMap((viewModel, model) => model.Uom.Id = viewModel.UomId)
                .AfterMap((viewModel, model) => model.Product.Id = viewModel.ProductId)
                .AfterMap((viewModel, model) => model.Warehouse.Id = viewModel.WarehouseId);
        }

        private static void MapInventoryTypes(IMapperConfigurationExpression mapperConfig)
        {
            mapperConfig.CreateMap<Warehouse, KeyValue>()
                .ForMember(dest => dest.Key, opts => opts.MapFrom(src => src.Id.ToString()))
                .ForMember(dest => dest.Value, opts => opts.MapFrom(src => src.Name));
            mapperConfig.CreateMap<Product, KeyValue>()
                .ForMember(dest => dest.Key, opts => opts.MapFrom(src => src.Id.ToString()))
                .ForMember(dest => dest.Value, opts => opts.MapFrom(src => src.Name));
            mapperConfig.CreateMap<UnitOfMeasurement, KeyValue>()
                .ForMember(dest => dest.Key, opts => opts.MapFrom(src => src.Id.ToString()))
                .ForMember(dest => dest.Value, opts => opts.MapFrom(src => src.Name));
            mapperConfig.CreateMap<ProductInventory, ProductInventoryViewModel>();
            mapperConfig.CreateMap<ProductInventoryViewModel, ProductInventory>()
                .AfterMap((viewModel, model) => model.Product.Id = viewModel.ProductId)
                .AfterMap((viewModel, model) => model.Uom.Id = viewModel.UomId)
                .AfterMap((viewModel, model) => model.Warehouse.Id = viewModel.WarehouseId)
                .AfterMap((viewModel, model) => model.FiscalPeriod.Id = viewModel.FiscalPeriodId)
                .AfterMap((viewModel, model) => model.Branch.Id = viewModel.BranchId);
        }

        private static void MapCoreTypes(IMapperConfigurationExpression mapperConfig)
        {
            mapperConfig.CreateMap<Model.Core.DocumentAction, DocumentActionViewModel>()
                .ForMember(
                    dest => dest.LineId,
                    opts => opts.MapFrom(
                        src => (src.LineId.HasValue) ? src.LineId.Value : 0))
                .ForMember(
                    dest => dest.CreatedDate,
                    opts => opts.MapFrom(
                        src => JalaliDateTime.FromDateTime(src.CreatedDate).ToShortDateString()))
                .ForMember(
                    dest => dest.ModifiedDate,
                    opts => opts.MapFrom(
                        src => JalaliDateTime.FromDateTime(src.ModifiedDate).ToShortDateString()))
                .ForMember(
                    dest => dest.ConfirmedDate,
                    opts => opts.MapFrom(
                        src => src.ConfirmedDate.HasValue
                            ? JalaliDateTime.FromDateTime(src.ConfirmedDate.Value).ToShortDateString()
                            : String.Empty))
                .ForMember(
                    dest => dest.ApprovedDate,
                    opts => opts.MapFrom(
                        src => src.ApprovedDate.HasValue
                            ? JalaliDateTime.FromDateTime(src.ApprovedDate.Value).ToShortDateString()
                            : String.Empty));
            mapperConfig.CreateMap<DocumentActionViewModel, Model.Core.DocumentAction>()
                .ForMember(
                    dest => dest.LineId,
                    opts => opts.MapFrom(
                        src => (src.LineId > 0) ? (int?)src.LineId : null))
                .ForMember(
                    dest => dest.CreatedDate,
                    opts => opts.MapFrom(
                        src => JalaliDateTime.Parse(src.CreatedDate).ToGregorian()))
                .ForMember(
                    dest => dest.ModifiedDate,
                    opts => opts.MapFrom(
                        src => JalaliDateTime.Parse(src.ModifiedDate).ToGregorian()))
                .ForMember(
                    dest => dest.ConfirmedDate,
                    opts => opts.MapFrom(
                        src => !String.IsNullOrWhiteSpace(src.ConfirmedDate)
                            ? JalaliDateTime.Parse(src.ConfirmedDate).ToGregorian()
                            : (DateTime?)null))
                .ForMember(
                    dest => dest.ApprovedDate,
                    opts => opts.MapFrom(
                        src => !String.IsNullOrWhiteSpace(src.ApprovedDate)
                            ? JalaliDateTime.Parse(src.ApprovedDate).ToGregorian()
                            : (DateTime?)null))
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

        private static void MapContactTypes(IMapperConfigurationExpression mapperConfig)
        {
            mapperConfig.CreateMap<BusinessPartner, KeyValue>()
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

        private static ICryptoService _crypto;
        private static MapperConfiguration _configuration;
        private static IMapper _autoMapper;
    }
}
