using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SPPC.Tadbir.Web.Api.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Finance");

            migrationBuilder.EnsureSchema(
                name: "ProductScope");

            migrationBuilder.EnsureSchema(
                name: "Corporate");

            migrationBuilder.EnsureSchema(
                name: "CashFlow");

            migrationBuilder.EnsureSchema(
                name: "Check");

            migrationBuilder.EnsureSchema(
                name: "Metadata");

            migrationBuilder.EnsureSchema(
                name: "Reporting");

            migrationBuilder.EnsureSchema(
                name: "Core");

            migrationBuilder.EnsureSchema(
                name: "Config");

            migrationBuilder.EnsureSchema(
                name: "Auth");

            migrationBuilder.CreateTable(
                name: "AccountCollectionCategory",
                schema: "Finance",
                columns: table => new
                {
                    AccountCollectionCategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountCollectionCategory", x => x.AccountCollectionCategoryId);
                });

            migrationBuilder.CreateTable(
                name: "AccountGroup",
                schema: "Finance",
                columns: table => new
                {
                    AccountGroupId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    InventoryMode = table.Column<short>(type: "smallint", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountGroup", x => x.AccountGroupId);
                });

            migrationBuilder.CreateTable(
                name: "Branch",
                schema: "Corporate",
                columns: table => new
                {
                    BranchId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParentId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    Level = table.Column<int>(type: "int", nullable: false, defaultValueSql: "((0))"),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Branch", x => x.BranchId);
                    table.ForeignKey(
                        name: "FK_Branch_Branch_ParentId",
                        column: x => x.ParentId,
                        principalSchema: "Corporate",
                        principalTable: "Branch",
                        principalColumn: "BranchId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CustomForm",
                schema: "Metadata",
                columns: table => new
                {
                    CustomFormId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomForm", x => x.CustomFormId);
                });

            migrationBuilder.CreateTable(
                name: "Dashboard",
                schema: "Reporting",
                columns: table => new
                {
                    DashboardId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dashboard", x => x.DashboardId);
                });

            migrationBuilder.CreateTable(
                name: "DocumentStatus",
                schema: "Core",
                columns: table => new
                {
                    DocumentStatusId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentStatus", x => x.DocumentStatusId);
                });

            migrationBuilder.CreateTable(
                name: "DocumentType",
                schema: "Core",
                columns: table => new
                {
                    DocumentTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentType", x => x.DocumentTypeId);
                });

            migrationBuilder.CreateTable(
                name: "EntityType",
                schema: "Metadata",
                columns: table => new
                {
                    EntityTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntityType", x => x.EntityTypeId);
                });

            migrationBuilder.CreateTable(
                name: "File",
                schema: "ProductScope",
                columns: table => new
                {
                    FileId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    UniqeName = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false),
                    Type = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Data = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FiscalPeriodId = table.Column<int>(type: "int", nullable: false),
                    BranchId = table.Column<int>(type: "int", nullable: false),
                    BranchScope = table.Column<short>(type: "smallint", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: false),
                    CreatedByName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedById = table.Column<int>(type: "int", nullable: false),
                    ModifiedByName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_File", x => x.FileId);
                });

            migrationBuilder.CreateTable(
                name: "Filter",
                schema: "Core",
                columns: table => new
                {
                    FilterId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ViewId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    IsPublic = table.Column<bool>(type: "bit", nullable: false),
                    Values = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Filter", x => x.FilterId);
                });

            migrationBuilder.CreateTable(
                name: "FiscalPeriod",
                schema: "Finance",
                columns: table => new
                {
                    FiscalPeriodId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InventoryMode = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FiscalPeriod", x => x.FiscalPeriodId);
                });

            migrationBuilder.CreateTable(
                name: "FunctionParameter",
                schema: "Reporting",
                columns: table => new
                {
                    FunctionParameterId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Alias = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Type = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    DefaultValue = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FunctionParameter", x => x.FunctionParameterId);
                });

            migrationBuilder.CreateTable(
                name: "Operation",
                schema: "Metadata",
                columns: table => new
                {
                    OperationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Operation", x => x.OperationId);
                });

            migrationBuilder.CreateTable(
                name: "OperationSource",
                schema: "Metadata",
                columns: table => new
                {
                    OperationSourceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperationSource", x => x.OperationSourceId);
                });

            migrationBuilder.CreateTable(
                name: "OperationSourceList",
                schema: "Metadata",
                columns: table => new
                {
                    OperationSourceListId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperationSourceList", x => x.OperationSourceListId);
                });

            migrationBuilder.CreateTable(
                name: "OperationSourceType",
                schema: "Metadata",
                columns: table => new
                {
                    OperationSourceTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperationSourceType", x => x.OperationSourceTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Province",
                schema: "Metadata",
                columns: table => new
                {
                    ProvinceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: true),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Province", x => x.ProvinceId);
                });

            migrationBuilder.CreateTable(
                name: "Setting",
                schema: "Config",
                columns: table => new
                {
                    SettingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParentId = table.Column<int>(type: "int", nullable: true),
                    Subsystem = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    TitleKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Type = table.Column<short>(type: "smallint", nullable: false),
                    ScopeType = table.Column<short>(type: "smallint", nullable: false),
                    ModelType = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Values = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false),
                    DefaultValues = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false),
                    DescriptionKey = table.Column<string>(type: "nvarchar(1028)", maxLength: 1028, nullable: true),
                    IsStandalone = table.Column<bool>(type: "bit", nullable: false),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Setting", x => x.SettingId);
                    table.ForeignKey(
                        name: "FK_Config_Setting_Config_Parent",
                        column: x => x.ParentId,
                        principalSchema: "Config",
                        principalTable: "Setting",
                        principalColumn: "SettingId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Subsystem",
                schema: "Metadata",
                columns: table => new
                {
                    SubsystemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subsystem", x => x.SubsystemId);
                });

            migrationBuilder.CreateTable(
                name: "TaxCurrency",
                schema: "Finance",
                columns: table => new
                {
                    TaxCurrencyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaxCurrency", x => x.TaxCurrencyId);
                });

            migrationBuilder.CreateTable(
                name: "UserValueCategory",
                schema: "Config",
                columns: table => new
                {
                    UserValueCategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameKey = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserValueCategory", x => x.UserValueCategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Version",
                schema: "Core",
                columns: table => new
                {
                    VersionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: true),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Version", x => x.VersionId);
                });

            migrationBuilder.CreateTable(
                name: "ViewSetting",
                schema: "Config",
                columns: table => new
                {
                    ViewSettingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SettingId = table.Column<int>(type: "int", nullable: false),
                    ViewId = table.Column<int>(type: "int", nullable: false),
                    ModelType = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Values = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DefaultValues = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ViewSetting", x => x.ViewSettingId);
                });

            migrationBuilder.CreateTable(
                name: "VoucherOrigin",
                schema: "Finance",
                columns: table => new
                {
                    VoucherOriginId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VoucherOrigin", x => x.VoucherOriginId);
                });

            migrationBuilder.CreateTable(
                name: "WidgetFunction",
                schema: "Reporting",
                columns: table => new
                {
                    WidgetFunctionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WidgetFunction", x => x.WidgetFunctionId);
                });

            migrationBuilder.CreateTable(
                name: "WidgetType",
                schema: "Reporting",
                columns: table => new
                {
                    WidgetTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WidgetType", x => x.WidgetTypeId);
                });

            migrationBuilder.CreateTable(
                name: "AccountCollection",
                schema: "Finance",
                columns: table => new
                {
                    AccountCollectionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    MultiSelect = table.Column<bool>(type: "bit", nullable: false),
                    TypeLevel = table.Column<short>(type: "smallint", nullable: false),
                    InventoryMode = table.Column<short>(type: "smallint", nullable: false),
                    CategoryID = table.Column<int>(type: "int", nullable: true),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountCollection", x => x.AccountCollectionId);
                    table.ForeignKey(
                        name: "FK_Finance_AccountCollection_Finance_Category",
                        column: x => x.CategoryID,
                        principalSchema: "Finance",
                        principalTable: "AccountCollectionCategory",
                        principalColumn: "AccountCollectionCategoryId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Attribute",
                schema: "ProductScope",
                columns: table => new
                {
                    AttributeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    EnName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    Type = table.Column<short>(type: "smallint", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FiscalPeriodId = table.Column<int>(type: "int", nullable: false),
                    BranchId = table.Column<int>(type: "int", nullable: false),
                    BranchScope = table.Column<short>(type: "smallint", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: false),
                    CreatedByName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedById = table.Column<int>(type: "int", nullable: false),
                    ModifiedByName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attribute", x => x.AttributeId);
                    table.ForeignKey(
                        name: "FK_Attribute_Branch_BranchId",
                        column: x => x.BranchId,
                        principalSchema: "Corporate",
                        principalTable: "Branch",
                        principalColumn: "BranchId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Brand",
                schema: "ProductScope",
                columns: table => new
                {
                    BrandId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    EnName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    SocialLink = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    Website = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    MetaKeyword = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FiscalPeriodId = table.Column<int>(type: "int", nullable: false),
                    BranchId = table.Column<int>(type: "int", nullable: false),
                    BranchScope = table.Column<short>(type: "smallint", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: false),
                    CreatedByName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedById = table.Column<int>(type: "int", nullable: false),
                    ModifiedByName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brand", x => x.BrandId);
                    table.ForeignKey(
                        name: "FK_Brand_Branch_BranchId",
                        column: x => x.BranchId,
                        principalSchema: "Corporate",
                        principalTable: "Branch",
                        principalColumn: "BranchId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Property",
                schema: "ProductScope",
                columns: table => new
                {
                    PropertyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    EnName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    Type = table.Column<short>(type: "smallint", nullable: true),
                    Prefix = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    Suffix = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FiscalPeriodId = table.Column<int>(type: "int", nullable: false),
                    BranchId = table.Column<int>(type: "int", nullable: false),
                    BranchScope = table.Column<short>(type: "smallint", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: false),
                    CreatedByName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedById = table.Column<int>(type: "int", nullable: false),
                    ModifiedByName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Property", x => x.PropertyId);
                    table.ForeignKey(
                        name: "FK_Property_Branch_BranchId",
                        column: x => x.BranchId,
                        principalSchema: "Corporate",
                        principalTable: "Branch",
                        principalColumn: "BranchId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RoleBranch",
                schema: "Auth",
                columns: table => new
                {
                    RoleBranchId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    BranchId = table.Column<int>(type: "int", nullable: false),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleBranch", x => x.RoleBranchId);
                    table.UniqueConstraint("AK_RoleBranch_RoleId_BranchId", x => new { x.RoleId, x.BranchId });
                    table.ForeignKey(
                        name: "FK_RoleBranch_Branch_BranchId",
                        column: x => x.BranchId,
                        principalSchema: "Corporate",
                        principalTable: "Branch",
                        principalColumn: "BranchId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Unit",
                schema: "ProductScope",
                columns: table => new
                {
                    UnitId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    EnName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    Symbol = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    Status = table.Column<short>(type: "smallint", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FiscalPeriodId = table.Column<int>(type: "int", nullable: false),
                    BranchId = table.Column<int>(type: "int", nullable: false),
                    BranchScope = table.Column<short>(type: "smallint", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: false),
                    CreatedByName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedById = table.Column<int>(type: "int", nullable: false),
                    ModifiedByName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Unit", x => x.UnitId);
                    table.ForeignKey(
                        name: "FK_Unit_Branch_BranchId",
                        column: x => x.BranchId,
                        principalSchema: "Corporate",
                        principalTable: "Branch",
                        principalColumn: "BranchId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DashboardTab",
                schema: "Reporting",
                columns: table => new
                {
                    DashboardTabId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DashboardId = table.Column<int>(type: "int", nullable: false),
                    Index = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DashboardTab", x => x.DashboardTabId);
                    table.ForeignKey(
                        name: "FK_Reporting_DashboardTab_Reporting_Dashboard",
                        column: x => x.DashboardId,
                        principalSchema: "Reporting",
                        principalTable: "Dashboard",
                        principalColumn: "DashboardId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Account",
                schema: "Finance",
                columns: table => new
                {
                    AccountId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupId = table.Column<int>(type: "int", nullable: true),
                    IsCurrencyAdjustable = table.Column<bool>(type: "bit", nullable: false),
                    TurnoverMode = table.Column<short>(type: "smallint", nullable: false),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    FiscalPeriodId = table.Column<int>(type: "int", nullable: false),
                    BranchId = table.Column<int>(type: "int", nullable: false),
                    BranchScope = table.Column<short>(type: "smallint", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: false),
                    CreatedByName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedById = table.Column<int>(type: "int", nullable: false),
                    ModifiedByName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    ParentId = table.Column<int>(type: "int", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    FullCode = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    Level = table.Column<short>(type: "smallint", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account", x => x.AccountId);
                    table.ForeignKey(
                        name: "FK_Finance_Account_Corporate_Branch",
                        column: x => x.BranchId,
                        principalSchema: "Corporate",
                        principalTable: "Branch",
                        principalColumn: "BranchId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Finance_Account_Finance_FiscalPeriod",
                        column: x => x.FiscalPeriodId,
                        principalSchema: "Finance",
                        principalTable: "FiscalPeriod",
                        principalColumn: "FiscalPeriodId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Finance_Account_Finance_Group",
                        column: x => x.GroupId,
                        principalSchema: "Finance",
                        principalTable: "AccountGroup",
                        principalColumn: "AccountGroupId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Finance_Account_Finance_Parent",
                        column: x => x.ParentId,
                        principalSchema: "Finance",
                        principalTable: "Account",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CashRegister",
                schema: "CashFlow",
                columns: table => new
                {
                    CashRegisterId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    FiscalPeriodId = table.Column<int>(type: "int", nullable: false),
                    BranchId = table.Column<int>(type: "int", nullable: false),
                    BranchScope = table.Column<short>(type: "smallint", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: false),
                    CreatedByName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedById = table.Column<int>(type: "int", nullable: false),
                    ModifiedByName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CashRegister", x => x.CashRegisterId);
                    table.ForeignKey(
                        name: "FK_CashFlow_CashRegister_Corporate_Branch",
                        column: x => x.BranchId,
                        principalSchema: "Corporate",
                        principalTable: "Branch",
                        principalColumn: "BranchId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CashFlow_CashRegister_Finance_FiscalPeriod",
                        column: x => x.FiscalPeriodId,
                        principalSchema: "Finance",
                        principalTable: "FiscalPeriod",
                        principalColumn: "FiscalPeriodId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CostCenter",
                schema: "Finance",
                columns: table => new
                {
                    CostCenterId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    FiscalPeriodId = table.Column<int>(type: "int", nullable: false),
                    BranchId = table.Column<int>(type: "int", nullable: false),
                    BranchScope = table.Column<short>(type: "smallint", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: false),
                    CreatedByName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedById = table.Column<int>(type: "int", nullable: false),
                    ModifiedByName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    ParentId = table.Column<int>(type: "int", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    FullCode = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Level = table.Column<short>(type: "smallint", nullable: false, defaultValueSql: "((0))"),
                    Description = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CostCenter", x => x.CostCenterId);
                    table.ForeignKey(
                        name: "FK_Finance_CostCenter_Corporate_Branch",
                        column: x => x.BranchId,
                        principalSchema: "Corporate",
                        principalTable: "Branch",
                        principalColumn: "BranchId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Finance_CostCenter_Finance_FiscalPeriod",
                        column: x => x.FiscalPeriodId,
                        principalSchema: "Finance",
                        principalTable: "FiscalPeriod",
                        principalColumn: "FiscalPeriodId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Finance_CostCenter_Finance_Parent",
                        column: x => x.ParentId,
                        principalSchema: "Finance",
                        principalTable: "CostCenter",
                        principalColumn: "CostCenterId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Currency",
                schema: "Finance",
                columns: table => new
                {
                    CurrencyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    TaxCode = table.Column<int>(type: "int", nullable: false),
                    MinorUnit = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    DecimalCount = table.Column<short>(type: "smallint", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    IsDefaultCurrency = table.Column<bool>(type: "bit", nullable: false),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    FiscalPeriodId = table.Column<int>(type: "int", nullable: false),
                    BranchId = table.Column<int>(type: "int", nullable: false),
                    BranchScope = table.Column<short>(type: "smallint", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: false),
                    CreatedByName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedById = table.Column<int>(type: "int", nullable: false),
                    ModifiedByName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currency", x => x.CurrencyId);
                    table.ForeignKey(
                        name: "FK_Currency_FiscalPeriod_FiscalPeriodId",
                        column: x => x.FiscalPeriodId,
                        principalSchema: "Finance",
                        principalTable: "FiscalPeriod",
                        principalColumn: "FiscalPeriodId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Finance_Currency_Corporate_Branch",
                        column: x => x.BranchId,
                        principalSchema: "Corporate",
                        principalTable: "Branch",
                        principalColumn: "BranchId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InactiveEntity",
                schema: "Core",
                columns: table => new
                {
                    InactiveEntityId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EntityId = table.Column<int>(type: "int", nullable: false),
                    EntityName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    FiscalPeriodId = table.Column<int>(type: "int", nullable: false),
                    BranchId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InactiveEntity", x => x.InactiveEntityId);
                    table.ForeignKey(
                        name: "FK_Core_InactiveEntity_Corporate_Branch",
                        column: x => x.BranchId,
                        principalSchema: "Corporate",
                        principalTable: "Branch",
                        principalColumn: "BranchId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Core_InactiveEntity_Finance_FiscalPeriod",
                        column: x => x.FiscalPeriodId,
                        principalSchema: "Finance",
                        principalTable: "FiscalPeriod",
                        principalColumn: "FiscalPeriodId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Project",
                schema: "Finance",
                columns: table => new
                {
                    ProjectId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    FiscalPeriodId = table.Column<int>(type: "int", nullable: false),
                    BranchId = table.Column<int>(type: "int", nullable: false),
                    BranchScope = table.Column<short>(type: "smallint", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: false),
                    CreatedByName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedById = table.Column<int>(type: "int", nullable: false),
                    ModifiedByName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    ParentId = table.Column<int>(type: "int", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    FullCode = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Level = table.Column<short>(type: "smallint", nullable: false, defaultValueSql: "((0))"),
                    Description = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Project", x => x.ProjectId);
                    table.ForeignKey(
                        name: "FK_Finance_Project_Corporate_Branch",
                        column: x => x.BranchId,
                        principalSchema: "Corporate",
                        principalTable: "Branch",
                        principalColumn: "BranchId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Finance_Project_Finance_FiscalPeriod",
                        column: x => x.FiscalPeriodId,
                        principalSchema: "Finance",
                        principalTable: "FiscalPeriod",
                        principalColumn: "FiscalPeriodId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Finance_Project_Finance_Parent",
                        column: x => x.ParentId,
                        principalSchema: "Finance",
                        principalTable: "Project",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RoleFiscalPeriod",
                schema: "Auth",
                columns: table => new
                {
                    RoleFiscalPeriodId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    FiscalPeriodId = table.Column<int>(type: "int", nullable: false),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleFiscalPeriod", x => x.RoleFiscalPeriodId);
                    table.UniqueConstraint("AK_RoleFiscalPeriod_RoleId_FiscalPeriodId", x => new { x.RoleId, x.FiscalPeriodId });
                    table.ForeignKey(
                        name: "FK_RoleFiscalPeriod_FiscalPeriod_FiscalPeriodId",
                        column: x => x.FiscalPeriodId,
                        principalSchema: "Finance",
                        principalTable: "FiscalPeriod",
                        principalColumn: "FiscalPeriodId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SourceApp",
                schema: "CashFlow",
                columns: table => new
                {
                    SourceAppId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    Type = table.Column<short>(type: "smallint", nullable: false),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    FiscalPeriodId = table.Column<int>(type: "int", nullable: false),
                    BranchId = table.Column<int>(type: "int", nullable: false),
                    BranchScope = table.Column<short>(type: "smallint", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: false),
                    CreatedByName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedById = table.Column<int>(type: "int", nullable: false),
                    ModifiedByName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SourceApp", x => x.SourceAppId);
                    table.ForeignKey(
                        name: "FK_CashFlow_SourceApp_Corporate_Branch",
                        column: x => x.BranchId,
                        principalSchema: "Corporate",
                        principalTable: "Branch",
                        principalColumn: "BranchId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CashFlow_SourceApp_Finance_FiscalPeriod",
                        column: x => x.FiscalPeriodId,
                        principalSchema: "Finance",
                        principalTable: "FiscalPeriod",
                        principalColumn: "FiscalPeriodId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OperationLog",
                schema: "Core",
                columns: table => new
                {
                    OperationLogId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OperationId = table.Column<int>(type: "int", nullable: false),
                    EntityTypeId = table.Column<int>(type: "int", nullable: true),
                    SourceId = table.Column<int>(type: "int", nullable: true),
                    SourceListId = table.Column<int>(type: "int", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Time = table.Column<TimeSpan>(type: "time", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    EntityId = table.Column<int>(type: "int", nullable: true),
                    EntityCode = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EntityName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EntityDescription = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    EntityNo = table.Column<int>(type: "int", nullable: true),
                    EntityDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EntityReference = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    EntityAssociation = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    FiscalPeriodId = table.Column<int>(type: "int", nullable: false),
                    BranchId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperationLog", x => x.OperationLogId);
                    table.ForeignKey(
                        name: "FK_Core_OperationLog_Corporate_Branch",
                        column: x => x.BranchId,
                        principalSchema: "Corporate",
                        principalTable: "Branch",
                        principalColumn: "BranchId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Core_OperationLog_Finance_FiscalPeriod",
                        column: x => x.FiscalPeriodId,
                        principalSchema: "Finance",
                        principalTable: "FiscalPeriod",
                        principalColumn: "FiscalPeriodId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Core_OperationLog_Metadata_EntityType",
                        column: x => x.EntityTypeId,
                        principalSchema: "Metadata",
                        principalTable: "EntityType",
                        principalColumn: "EntityTypeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Core_OperationLog_Metadata_Operation",
                        column: x => x.OperationId,
                        principalSchema: "Metadata",
                        principalTable: "Operation",
                        principalColumn: "OperationId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Core_OperationLog_Metadata_Source",
                        column: x => x.SourceId,
                        principalSchema: "Metadata",
                        principalTable: "OperationSource",
                        principalColumn: "OperationSourceId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Core_OperationLog_Metadata_SourceList",
                        column: x => x.SourceListId,
                        principalSchema: "Metadata",
                        principalTable: "OperationSourceList",
                        principalColumn: "OperationSourceListId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OperationLogArchive",
                schema: "Core",
                columns: table => new
                {
                    OperationLogArchiveId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OperationId = table.Column<int>(type: "int", nullable: false),
                    EntityTypeId = table.Column<int>(type: "int", nullable: true),
                    SourceId = table.Column<int>(type: "int", nullable: true),
                    SourceListId = table.Column<int>(type: "int", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Time = table.Column<TimeSpan>(type: "time", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    EntityId = table.Column<int>(type: "int", nullable: true),
                    EntityCode = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EntityName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EntityDescription = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    EntityNo = table.Column<int>(type: "int", nullable: true),
                    EntityDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EntityReference = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    EntityAssociation = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    FiscalPeriodId = table.Column<int>(type: "int", nullable: false),
                    BranchId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperationLogArchive", x => x.OperationLogArchiveId);
                    table.ForeignKey(
                        name: "FK_Core_OperationLogArchive_Corporate_Branch",
                        column: x => x.BranchId,
                        principalSchema: "Corporate",
                        principalTable: "Branch",
                        principalColumn: "BranchId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Core_OperationLogArchive_Finance_FiscalPeriod",
                        column: x => x.FiscalPeriodId,
                        principalSchema: "Finance",
                        principalTable: "FiscalPeriod",
                        principalColumn: "FiscalPeriodId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Core_OperationLogArchive_Metadata_EntityType",
                        column: x => x.EntityTypeId,
                        principalSchema: "Metadata",
                        principalTable: "EntityType",
                        principalColumn: "EntityTypeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Core_OperationLogArchive_Metadata_Operation",
                        column: x => x.OperationId,
                        principalSchema: "Metadata",
                        principalTable: "Operation",
                        principalColumn: "OperationId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Core_OperationLogArchive_Metadata_Source",
                        column: x => x.SourceId,
                        principalSchema: "Metadata",
                        principalTable: "OperationSource",
                        principalColumn: "OperationSourceId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Core_OperationLogArchive_Metadata_SourceList",
                        column: x => x.SourceListId,
                        principalSchema: "Metadata",
                        principalTable: "OperationSourceList",
                        principalColumn: "OperationSourceListId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "City",
                schema: "Metadata",
                columns: table => new
                {
                    CityId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProvinceId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: true),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_City", x => x.CityId);
                    table.ForeignKey(
                        name: "FK_Metadata_City_Metadata_Province",
                        column: x => x.ProvinceId,
                        principalSchema: "Metadata",
                        principalTable: "Province",
                        principalColumn: "ProvinceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LabelSetting",
                schema: "Config",
                columns: table => new
                {
                    LabelSettingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ModelType = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Values = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DefaultValues = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LocaleId = table.Column<int>(type: "int", nullable: false),
                    SettingID = table.Column<int>(type: "int", nullable: true),
                    CustomFormID = table.Column<int>(type: "int", nullable: true),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LabelSetting", x => x.LabelSettingId);
                    table.ForeignKey(
                        name: "FK_Config_LabelSetting_Config_Setting",
                        column: x => x.SettingID,
                        principalSchema: "Config",
                        principalTable: "Setting",
                        principalColumn: "SettingId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Config_LabelSetting_Metadata_CustomForm",
                        column: x => x.CustomFormID,
                        principalSchema: "Metadata",
                        principalTable: "CustomForm",
                        principalColumn: "CustomFormId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserSetting",
                schema: "Config",
                columns: table => new
                {
                    UserSettingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    RoleId = table.Column<int>(type: "int", nullable: true),
                    SettingId = table.Column<int>(type: "int", nullable: false),
                    ViewId = table.Column<int>(type: "int", nullable: true),
                    ModelType = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Values = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSetting", x => x.UserSettingId);
                    table.ForeignKey(
                        name: "FK_Config_UserSetting_Config_Setting",
                        column: x => x.SettingId,
                        principalSchema: "Config",
                        principalTable: "Setting",
                        principalColumn: "SettingId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LogSetting",
                schema: "Config",
                columns: table => new
                {
                    LogSettingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubsystemId = table.Column<int>(type: "int", nullable: false),
                    SourceTypeId = table.Column<int>(type: "int", nullable: false),
                    SourceId = table.Column<int>(type: "int", nullable: true),
                    EntityTypeId = table.Column<int>(type: "int", nullable: true),
                    OperationId = table.Column<int>(type: "int", nullable: false),
                    IsEnabled = table.Column<bool>(type: "bit", nullable: false),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogSetting", x => x.LogSettingId);
                    table.ForeignKey(
                        name: "FK_Config_LogSetting_Metadata_EntityType",
                        column: x => x.EntityTypeId,
                        principalSchema: "Metadata",
                        principalTable: "EntityType",
                        principalColumn: "EntityTypeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Config_LogSetting_Metadata_Operation",
                        column: x => x.OperationId,
                        principalSchema: "Metadata",
                        principalTable: "Operation",
                        principalColumn: "OperationId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Config_LogSetting_Metadata_Source",
                        column: x => x.SourceId,
                        principalSchema: "Metadata",
                        principalTable: "OperationSource",
                        principalColumn: "OperationSourceId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Config_LogSetting_Metadata_SourceType",
                        column: x => x.SourceTypeId,
                        principalSchema: "Metadata",
                        principalTable: "OperationSourceType",
                        principalColumn: "OperationSourceTypeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Config_LogSetting_Metadata_Subsystem",
                        column: x => x.SubsystemId,
                        principalSchema: "Metadata",
                        principalTable: "Subsystem",
                        principalColumn: "SubsystemId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserValue",
                schema: "Config",
                columns: table => new
                {
                    UserValueId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserValue", x => x.UserValueId);
                    table.ForeignKey(
                        name: "FK_Config_UserValue_Config_Category",
                        column: x => x.CategoryId,
                        principalSchema: "Config",
                        principalTable: "UserValueCategory",
                        principalColumn: "UserValueCategoryId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Voucher",
                schema: "Finance",
                columns: table => new
                {
                    VoucherId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    OriginId = table.Column<int>(type: "int", nullable: false),
                    No = table.Column<int>(type: "int", nullable: false),
                    DailyNo = table.Column<int>(type: "int", nullable: false),
                    Association = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    IsBalanced = table.Column<bool>(type: "bit", nullable: false),
                    Type = table.Column<short>(type: "smallint", nullable: false),
                    SubjectType = table.Column<short>(type: "smallint", nullable: false),
                    SaveCount = table.Column<int>(type: "int", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: false),
                    ModifiedById = table.Column<int>(type: "int", nullable: false),
                    ConfirmedById = table.Column<int>(type: "int", nullable: true),
                    ApprovedById = table.Column<int>(type: "int", nullable: true),
                    IssuerName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    ModifierName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    ConfirmerName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    ApproverName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    FiscalPeriodId = table.Column<int>(type: "int", nullable: false),
                    BranchId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime", nullable: false),
                    Reference = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Voucher", x => x.VoucherId);
                    table.ForeignKey(
                        name: "FK_Finance_Voucher_Core_Status",
                        column: x => x.StatusId,
                        principalSchema: "Core",
                        principalTable: "DocumentStatus",
                        principalColumn: "DocumentStatusId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Finance_Voucher_Corporate_Branch",
                        column: x => x.BranchId,
                        principalSchema: "Corporate",
                        principalTable: "Branch",
                        principalColumn: "BranchId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Finance_Voucher_Finance_FiscalPeriod",
                        column: x => x.FiscalPeriodId,
                        principalSchema: "Finance",
                        principalTable: "FiscalPeriod",
                        principalColumn: "FiscalPeriodId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Finance_Voucher_Finance_VoucherOrigin",
                        column: x => x.OriginId,
                        principalSchema: "Finance",
                        principalTable: "VoucherOrigin",
                        principalColumn: "VoucherOriginId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UsedParameter",
                schema: "Reporting",
                columns: table => new
                {
                    UsedParameterId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FunctionId = table.Column<int>(type: "int", nullable: false),
                    ParameterId = table.Column<int>(type: "int", nullable: false),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsedParameter", x => x.UsedParameterId);
                    table.ForeignKey(
                        name: "FK_Reporting_UsedParameter_Reporting_Function",
                        column: x => x.FunctionId,
                        principalSchema: "Reporting",
                        principalTable: "WidgetFunction",
                        principalColumn: "WidgetFunctionId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reporting_UsedParameter_Reporting_Parameter",
                        column: x => x.ParameterId,
                        principalSchema: "Reporting",
                        principalTable: "FunctionParameter",
                        principalColumn: "FunctionParameterId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Widget",
                schema: "Reporting",
                columns: table => new
                {
                    WidgetId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FunctionId = table.Column<int>(type: "int", nullable: false),
                    TypeId = table.Column<int>(type: "int", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    DefaultSettings = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Widget", x => x.WidgetId);
                    table.ForeignKey(
                        name: "FK_Reporting_Widget_Reporting_Function",
                        column: x => x.FunctionId,
                        principalSchema: "Reporting",
                        principalTable: "WidgetFunction",
                        principalColumn: "WidgetFunctionId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reporting_Widget_Reporting_Type",
                        column: x => x.TypeId,
                        principalSchema: "Reporting",
                        principalTable: "WidgetType",
                        principalColumn: "WidgetTypeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AccountCollectionAccount",
                schema: "Finance",
                columns: table => new
                {
                    AccountCollectionAccountId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountId = table.Column<int>(type: "int", nullable: false),
                    CollectionId = table.Column<int>(type: "int", nullable: false),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    FiscalPeriodId = table.Column<int>(type: "int", nullable: false),
                    BranchId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountCollectionAccount", x => x.AccountCollectionAccountId);
                    table.ForeignKey(
                        name: "FK_Finance_AccountCollectionAccount_Finance_Account",
                        column: x => x.AccountId,
                        principalSchema: "Finance",
                        principalTable: "Account",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Finance_AccountCollectionAccount_Finance_Branch",
                        column: x => x.BranchId,
                        principalSchema: "Corporate",
                        principalTable: "Branch",
                        principalColumn: "BranchId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Finance_AccountCollectionAccount_Finance_Collection",
                        column: x => x.CollectionId,
                        principalSchema: "Finance",
                        principalTable: "AccountCollection",
                        principalColumn: "AccountCollectionId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Finance_AccountCollectionAccount_Finance_FiscalPeriod",
                        column: x => x.FiscalPeriodId,
                        principalSchema: "Finance",
                        principalTable: "FiscalPeriod",
                        principalColumn: "FiscalPeriodId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AccountOwner",
                schema: "Finance",
                columns: table => new
                {
                    AccountOwnerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountId = table.Column<int>(type: "int", nullable: false),
                    BankName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    AccountType = table.Column<int>(type: "int", nullable: false),
                    BankBranchName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    BranchIndex = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    AccountNumber = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    CardNumber = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    ShabaNumber = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountOwner", x => x.AccountOwnerId);
                    table.ForeignKey(
                        name: "FK_Finance_AccountOwner_Finance_Account",
                        column: x => x.AccountId,
                        principalSchema: "Finance",
                        principalTable: "Account",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CustomerTaxInfo",
                schema: "Finance",
                columns: table => new
                {
                    CustomerTaxInfoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountId = table.Column<int>(type: "int", nullable: false),
                    CustomerFirstName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    CustomerName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    PersonType = table.Column<int>(type: "int", nullable: false),
                    BuyerType = table.Column<int>(type: "int", nullable: false),
                    EconomicCode = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    NationalCode = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    PerCityCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    PhoneNo = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    MobileNo = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    PostalCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    ProvinceCode = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: false),
                    CityCode = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerTaxInfo", x => x.CustomerTaxInfoId);
                    table.ForeignKey(
                        name: "FK_Finance_CustomerTaxInfo_Finance_Account",
                        column: x => x.AccountId,
                        principalSchema: "Finance",
                        principalTable: "Account",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserCashRegister",
                schema: "CashFlow",
                columns: table => new
                {
                    UserCashRegisterId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CashRegisterId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCashRegister", x => x.UserCashRegisterId);
                    table.ForeignKey(
                        name: "FK_CashFlow_UserCashRegister_CashFlow_CashRegister",
                        column: x => x.CashRegisterId,
                        principalSchema: "CashFlow",
                        principalTable: "CashRegister",
                        principalColumn: "CashRegisterId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AccountCostCenter",
                schema: "Finance",
                columns: table => new
                {
                    AccountCostCenterId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountId = table.Column<int>(type: "int", nullable: false),
                    CostCenterId = table.Column<int>(type: "int", nullable: false),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountCostCenter", x => x.AccountCostCenterId);
                    table.UniqueConstraint("AK_AccountCostCenter_AccountId_CostCenterId", x => new { x.AccountId, x.CostCenterId });
                    table.ForeignKey(
                        name: "FK_AccountCostCenter_Account_AccountId",
                        column: x => x.AccountId,
                        principalSchema: "Finance",
                        principalTable: "Account",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccountCostCenter_CostCenter_CostCenterId",
                        column: x => x.CostCenterId,
                        principalSchema: "Finance",
                        principalTable: "CostCenter",
                        principalColumn: "CostCenterId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AccountCurrency",
                schema: "Finance",
                columns: table => new
                {
                    AccountCurrencyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountId = table.Column<int>(type: "int", nullable: false),
                    CurrencyId = table.Column<int>(type: "int", nullable: false),
                    BranchId = table.Column<int>(type: "int", nullable: false),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountCurrency", x => x.AccountCurrencyId);
                    table.ForeignKey(
                        name: "FK_AccountCurrency_Account_AccountId",
                        column: x => x.AccountId,
                        principalSchema: "Finance",
                        principalTable: "Account",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccountCurrency_Branch_BranchId",
                        column: x => x.BranchId,
                        principalSchema: "Corporate",
                        principalTable: "Branch",
                        principalColumn: "BranchId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccountCurrency_Currency_CurrencyId",
                        column: x => x.CurrencyId,
                        principalSchema: "Finance",
                        principalTable: "Currency",
                        principalColumn: "CurrencyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CurrencyRate",
                schema: "Finance",
                columns: table => new
                {
                    CurrencyRateId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CurrencyId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Time = table.Column<TimeSpan>(type: "time", nullable: false),
                    Multiplier = table.Column<double>(type: "float", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    FiscalPeriodId = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    BranchId = table.Column<int>(type: "int", nullable: false),
                    BranchScope = table.Column<short>(type: "smallint", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: false),
                    CreatedByName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedById = table.Column<int>(type: "int", nullable: false),
                    ModifiedByName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurrencyRate", x => x.CurrencyRateId);
                    table.ForeignKey(
                        name: "FK_CurrencyRate_Currency_CurrencyId",
                        column: x => x.CurrencyId,
                        principalSchema: "Finance",
                        principalTable: "Currency",
                        principalColumn: "CurrencyId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CurrencyRate_FiscalPeriod_FiscalPeriodId",
                        column: x => x.FiscalPeriodId,
                        principalSchema: "Finance",
                        principalTable: "FiscalPeriod",
                        principalColumn: "FiscalPeriodId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Finance_CurrencyRate_Corporate_Branch",
                        column: x => x.BranchId,
                        principalSchema: "Corporate",
                        principalTable: "Branch",
                        principalColumn: "BranchId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DetailAccount",
                schema: "Finance",
                columns: table => new
                {
                    DetailAccountId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CurrencyId = table.Column<int>(type: "int", nullable: true),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    FiscalPeriodId = table.Column<int>(type: "int", nullable: false),
                    BranchId = table.Column<int>(type: "int", nullable: false),
                    BranchScope = table.Column<short>(type: "smallint", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: false),
                    CreatedByName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedById = table.Column<int>(type: "int", nullable: false),
                    ModifiedByName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    ParentId = table.Column<int>(type: "int", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    FullCode = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Level = table.Column<short>(type: "smallint", nullable: false, defaultValueSql: "((0))"),
                    Description = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetailAccount", x => x.DetailAccountId);
                    table.ForeignKey(
                        name: "FK_Finance_DetailAccount_Corporate_Branch",
                        column: x => x.BranchId,
                        principalSchema: "Corporate",
                        principalTable: "Branch",
                        principalColumn: "BranchId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Finance_DetailAccount_Finance_Currency",
                        column: x => x.CurrencyId,
                        principalSchema: "Finance",
                        principalTable: "Currency",
                        principalColumn: "CurrencyId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Finance_DetailAccount_Finance_FiscalPeriod",
                        column: x => x.FiscalPeriodId,
                        principalSchema: "Finance",
                        principalTable: "FiscalPeriod",
                        principalColumn: "FiscalPeriodId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Finance_DetailAccount_Finance_Parent",
                        column: x => x.ParentId,
                        principalSchema: "Finance",
                        principalTable: "DetailAccount",
                        principalColumn: "DetailAccountId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PayReceive",
                schema: "CashFlow",
                columns: table => new
                {
                    PayReceiveId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CurrencyId = table.Column<int>(type: "int", nullable: true),
                    IssuedById = table.Column<int>(type: "int", nullable: false),
                    ModifiedById = table.Column<int>(type: "int", nullable: false),
                    ConfirmedById = table.Column<int>(type: "int", nullable: true),
                    ApprovedById = table.Column<int>(type: "int", nullable: true),
                    Type = table.Column<short>(type: "smallint", nullable: false),
                    CurrencyRate = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IssuedByName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    ModifiedByName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    ConfirmedByName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    ApprovedByName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    FiscalPeriodId = table.Column<int>(type: "int", nullable: false),
                    BranchId = table.Column<int>(type: "int", nullable: false),
                    TextNo = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Reference = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PayReceive", x => x.PayReceiveId);
                    table.ForeignKey(
                        name: "FK_CashFlow_PayReceive_Corporate_Branch",
                        column: x => x.BranchId,
                        principalSchema: "Corporate",
                        principalTable: "Branch",
                        principalColumn: "BranchId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CashFlow_PayReceive_Finance_Currency",
                        column: x => x.CurrencyId,
                        principalSchema: "Finance",
                        principalTable: "Currency",
                        principalColumn: "CurrencyId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CashFlow_PayReceive_Finance_FiscalPeriod",
                        column: x => x.FiscalPeriodId,
                        principalSchema: "Finance",
                        principalTable: "FiscalPeriod",
                        principalColumn: "FiscalPeriodId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AccountProject",
                schema: "Finance",
                columns: table => new
                {
                    AccountProjectId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountId = table.Column<int>(type: "int", nullable: false),
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountProject", x => x.AccountProjectId);
                    table.UniqueConstraint("AK_AccountProject_AccountId_ProjectId", x => new { x.AccountId, x.ProjectId });
                    table.ForeignKey(
                        name: "FK_AccountProject_Account_AccountId",
                        column: x => x.AccountId,
                        principalSchema: "Finance",
                        principalTable: "Account",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccountProject_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalSchema: "Finance",
                        principalTable: "Project",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoleWidget",
                schema: "Auth",
                columns: table => new
                {
                    RoleWidgetId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WidgetId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleWidget", x => x.RoleWidgetId);
                    table.ForeignKey(
                        name: "FK_Auth_RoleWidget_Reporting_Widget",
                        column: x => x.WidgetId,
                        principalSchema: "Reporting",
                        principalTable: "Widget",
                        principalColumn: "WidgetId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TabWidget",
                schema: "Reporting",
                columns: table => new
                {
                    TabWidgetId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TabId = table.Column<int>(type: "int", nullable: false),
                    WidgetId = table.Column<int>(type: "int", nullable: false),
                    Settings = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false),
                    DefaultSettings = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TabWidget", x => x.TabWidgetId);
                    table.ForeignKey(
                        name: "FK_Reporting_TabWidget_Reporting_DashboardTab",
                        column: x => x.TabId,
                        principalSchema: "Reporting",
                        principalTable: "DashboardTab",
                        principalColumn: "DashboardTabId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reporting_TabWidget_Reporting_Widget",
                        column: x => x.WidgetId,
                        principalSchema: "Reporting",
                        principalTable: "Widget",
                        principalColumn: "WidgetId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AccountHolder",
                schema: "Finance",
                columns: table => new
                {
                    AccountHolderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountOwnerId = table.Column<int>(type: "int", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    HasSignature = table.Column<bool>(type: "bit", nullable: false),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountHolder", x => x.AccountHolderId);
                    table.ForeignKey(
                        name: "FK_AccountHolder_AccountOwner_AccountOwnerId",
                        column: x => x.AccountOwnerId,
                        principalSchema: "Finance",
                        principalTable: "AccountOwner",
                        principalColumn: "AccountOwnerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AccountDetailAccount",
                schema: "Finance",
                columns: table => new
                {
                    AccountDetailAccountId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountId = table.Column<int>(type: "int", nullable: false),
                    DetailAccountId = table.Column<int>(type: "int", nullable: false),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountDetailAccount", x => x.AccountDetailAccountId);
                    table.UniqueConstraint("AK_AccountDetailAccount_AccountId_DetailAccountId", x => new { x.AccountId, x.DetailAccountId });
                    table.ForeignKey(
                        name: "FK_AccountDetailAccount_Account_AccountId",
                        column: x => x.AccountId,
                        principalSchema: "Finance",
                        principalTable: "Account",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccountDetailAccount_DetailAccount_DetailAccountId",
                        column: x => x.DetailAccountId,
                        principalSchema: "Finance",
                        principalTable: "DetailAccount",
                        principalColumn: "DetailAccountId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CheckBook",
                schema: "Check",
                columns: table => new
                {
                    CheckBookId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountId = table.Column<int>(type: "int", nullable: false),
                    DetailAccountId = table.Column<int>(type: "int", nullable: true),
                    CostCenterId = table.Column<int>(type: "int", nullable: true),
                    ProjectId = table.Column<int>(type: "int", nullable: true),
                    SeriesNo = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    SayyadStartNo = table.Column<string>(type: "nchar(16)", fixedLength: true, maxLength: 16, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    IssueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartNo = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    EndNo = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    BankName = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    IsArchived = table.Column<bool>(type: "bit", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: false),
                    ModifiedById = table.Column<int>(type: "int", nullable: false),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    FiscalPeriodId = table.Column<int>(type: "int", nullable: false),
                    BranchId = table.Column<int>(type: "int", nullable: false),
                    TextNo = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CheckBook", x => x.CheckBookId);
                    table.ForeignKey(
                        name: "FK_Check_CheckBook_Corporate_Branch",
                        column: x => x.BranchId,
                        principalSchema: "Corporate",
                        principalTable: "Branch",
                        principalColumn: "BranchId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Check_CheckBook_Finance_Account",
                        column: x => x.AccountId,
                        principalSchema: "Finance",
                        principalTable: "Account",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Check_CheckBook_Finance_CostCenter",
                        column: x => x.CostCenterId,
                        principalSchema: "Finance",
                        principalTable: "CostCenter",
                        principalColumn: "CostCenterId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Check_CheckBook_Finance_DetailAccount",
                        column: x => x.DetailAccountId,
                        principalSchema: "Finance",
                        principalTable: "DetailAccount",
                        principalColumn: "DetailAccountId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Check_CheckBook_Finance_Project",
                        column: x => x.ProjectId,
                        principalSchema: "Finance",
                        principalTable: "Project",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CheckBook_FiscalPeriod_FiscalPeriodId",
                        column: x => x.FiscalPeriodId,
                        principalSchema: "Finance",
                        principalTable: "FiscalPeriod",
                        principalColumn: "FiscalPeriodId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VoucherLine",
                schema: "Finance",
                columns: table => new
                {
                    VoucherLineId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VoucherId = table.Column<int>(type: "int", nullable: false),
                    AccountId = table.Column<int>(type: "int", nullable: false),
                    DetailAccountId = table.Column<int>(type: "int", nullable: true),
                    CostCenterId = table.Column<int>(type: "int", nullable: true),
                    ProjectId = table.Column<int>(type: "int", nullable: true),
                    CurrencyId = table.Column<int>(type: "int", nullable: true),
                    CreatedById = table.Column<int>(type: "int", nullable: false),
                    SourceAppId = table.Column<int>(type: "int", nullable: true),
                    RowNo = table.Column<int>(type: "int", nullable: false),
                    Debit = table.Column<decimal>(type: "money", nullable: false),
                    Credit = table.Column<decimal>(type: "money", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    FollowupNo = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    CurrencyValue = table.Column<decimal>(type: "money", nullable: true),
                    Mark = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    TypeId = table.Column<short>(type: "smallint", nullable: false),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    FiscalPeriodId = table.Column<int>(type: "int", nullable: false),
                    BranchId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VoucherLine", x => x.VoucherLineId);
                    table.ForeignKey(
                        name: "FK_Finance_VoucherLine_CashFlow_SourceApp",
                        column: x => x.SourceAppId,
                        principalSchema: "CashFlow",
                        principalTable: "SourceApp",
                        principalColumn: "SourceAppId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Finance_VoucherLine_Corporate_Branch",
                        column: x => x.BranchId,
                        principalSchema: "Corporate",
                        principalTable: "Branch",
                        principalColumn: "BranchId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Finance_VoucherLine_Finance_Account",
                        column: x => x.AccountId,
                        principalSchema: "Finance",
                        principalTable: "Account",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Finance_VoucherLine_Finance_CostCenter",
                        column: x => x.CostCenterId,
                        principalSchema: "Finance",
                        principalTable: "CostCenter",
                        principalColumn: "CostCenterId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Finance_VoucherLine_Finance_Currency",
                        column: x => x.CurrencyId,
                        principalSchema: "Finance",
                        principalTable: "Currency",
                        principalColumn: "CurrencyId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Finance_VoucherLine_Finance_DetailAccount",
                        column: x => x.DetailAccountId,
                        principalSchema: "Finance",
                        principalTable: "DetailAccount",
                        principalColumn: "DetailAccountId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Finance_VoucherLine_Finance_FiscalPeriod",
                        column: x => x.FiscalPeriodId,
                        principalSchema: "Finance",
                        principalTable: "FiscalPeriod",
                        principalColumn: "FiscalPeriodId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Finance_VoucherLine_Finance_Project",
                        column: x => x.ProjectId,
                        principalSchema: "Finance",
                        principalTable: "Project",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Finance_VoucherLine_Finance_Voucher",
                        column: x => x.VoucherId,
                        principalSchema: "Finance",
                        principalTable: "Voucher",
                        principalColumn: "VoucherId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WidgetAccount",
                schema: "Reporting",
                columns: table => new
                {
                    WidgetAccountId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WidgetId = table.Column<int>(type: "int", nullable: false),
                    AccountId = table.Column<int>(type: "int", nullable: true),
                    DetailAccountId = table.Column<int>(type: "int", nullable: true),
                    CostCenterId = table.Column<int>(type: "int", nullable: true),
                    ProjectId = table.Column<int>(type: "int", nullable: true),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WidgetAccount", x => x.WidgetAccountId);
                    table.ForeignKey(
                        name: "FK_Reporting_WidgetAccount_Finance_Account",
                        column: x => x.AccountId,
                        principalSchema: "Finance",
                        principalTable: "Account",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reporting_WidgetAccount_Finance_CostCenter",
                        column: x => x.CostCenterId,
                        principalSchema: "Finance",
                        principalTable: "CostCenter",
                        principalColumn: "CostCenterId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reporting_WidgetAccount_Finance_DetailAccount",
                        column: x => x.DetailAccountId,
                        principalSchema: "Finance",
                        principalTable: "DetailAccount",
                        principalColumn: "DetailAccountId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reporting_WidgetAccount_Finance_Project",
                        column: x => x.ProjectId,
                        principalSchema: "Finance",
                        principalTable: "Project",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reporting_WidgetAccount_Reporting_Widget",
                        column: x => x.WidgetId,
                        principalSchema: "Reporting",
                        principalTable: "Widget",
                        principalColumn: "WidgetId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PayReceiveAccount",
                schema: "CashFlow",
                columns: table => new
                {
                    PayReceiveAccountId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PayReceiveId = table.Column<int>(type: "int", nullable: false),
                    AccountId = table.Column<int>(type: "int", nullable: true),
                    DetailAccountId = table.Column<int>(type: "int", nullable: true),
                    CostCenterId = table.Column<int>(type: "int", nullable: true),
                    ProjectId = table.Column<int>(type: "int", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PayReceiveAccount", x => x.PayReceiveAccountId);
                    table.ForeignKey(
                        name: "FK_CashFlow_PayReceiveAccount_CashFlow_PayReceive",
                        column: x => x.PayReceiveId,
                        principalSchema: "CashFlow",
                        principalTable: "PayReceive",
                        principalColumn: "PayReceiveId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CashFlow_PayReceiveAccount_Finance_Account",
                        column: x => x.AccountId,
                        principalSchema: "Finance",
                        principalTable: "Account",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CashFlow_PayReceiveAccount_Finance_CostCenter",
                        column: x => x.CostCenterId,
                        principalSchema: "Finance",
                        principalTable: "CostCenter",
                        principalColumn: "CostCenterId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CashFlow_PayReceiveAccount_Finance_DetailAccount",
                        column: x => x.DetailAccountId,
                        principalSchema: "Finance",
                        principalTable: "DetailAccount",
                        principalColumn: "DetailAccountId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CashFlow_PayReceiveAccount_Finance_Project",
                        column: x => x.ProjectId,
                        principalSchema: "Finance",
                        principalTable: "Project",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PayReceiveCashAccount",
                schema: "CashFlow",
                columns: table => new
                {
                    PayReceiveCashAccountId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PayReceiveId = table.Column<int>(type: "int", nullable: false),
                    AccountId = table.Column<int>(type: "int", nullable: true),
                    DetailAccountId = table.Column<int>(type: "int", nullable: true),
                    CostCenterId = table.Column<int>(type: "int", nullable: true),
                    ProjectId = table.Column<int>(type: "int", nullable: true),
                    SourceAppId = table.Column<int>(type: "int", nullable: true),
                    IsBank = table.Column<bool>(type: "bit", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BankOrderNo = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PayReceiveCashAccount", x => x.PayReceiveCashAccountId);
                    table.ForeignKey(
                        name: "FK_CashFlow_PayReceiveCashAccount_CashFlow_PayReceive",
                        column: x => x.PayReceiveId,
                        principalSchema: "CashFlow",
                        principalTable: "PayReceive",
                        principalColumn: "PayReceiveId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CashFlow_PayReceiveCashAccount_CashFlow_SourceApp",
                        column: x => x.SourceAppId,
                        principalSchema: "CashFlow",
                        principalTable: "SourceApp",
                        principalColumn: "SourceAppId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CashFlow_PayReceiveCashAccount_Finance_Account",
                        column: x => x.AccountId,
                        principalSchema: "Finance",
                        principalTable: "Account",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CashFlow_PayReceiveCashAccount_Finance_CostCenter",
                        column: x => x.CostCenterId,
                        principalSchema: "Finance",
                        principalTable: "CostCenter",
                        principalColumn: "CostCenterId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CashFlow_PayReceiveCashAccount_Finance_DetailAccount",
                        column: x => x.DetailAccountId,
                        principalSchema: "Finance",
                        principalTable: "DetailAccount",
                        principalColumn: "DetailAccountId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CashFlow_PayReceiveCashAccount_Finance_Project",
                        column: x => x.ProjectId,
                        principalSchema: "Finance",
                        principalTable: "Project",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CheckBookPage",
                schema: "Check",
                columns: table => new
                {
                    CheckBookPageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CheckBookId = table.Column<int>(type: "int", nullable: false),
                    SerialNo = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    SayyadNo = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    Status = table.Column<short>(type: "smallint", nullable: true),
                    CheckId = table.Column<int>(type: "int", nullable: true),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CheckBookPage", x => x.CheckBookPageId);
                    table.ForeignKey(
                        name: "FK_Check_CheckBookPage_Check_CheckBook",
                        column: x => x.CheckBookId,
                        principalSchema: "Check",
                        principalTable: "CheckBook",
                        principalColumn: "CheckBookId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PayReceiveVoucherLine",
                schema: "CashFlow",
                columns: table => new
                {
                    PayReceiveVoucherLineId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PayReceiveId = table.Column<int>(type: "int", nullable: false),
                    VoucherLineId = table.Column<int>(type: "int", nullable: false),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PayReceiveVoucherLine", x => x.PayReceiveVoucherLineId);
                    table.UniqueConstraint("AK_PayReceiveVoucherLine_PayReceiveId_VoucherLineId", x => new { x.PayReceiveId, x.VoucherLineId });
                    table.ForeignKey(
                        name: "FK_PayReceiveVoucherLine_PayReceive_PayReceiveId",
                        column: x => x.PayReceiveId,
                        principalSchema: "CashFlow",
                        principalTable: "PayReceive",
                        principalColumn: "PayReceiveId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PayReceiveVoucherLine_VoucherLine_VoucherLineId",
                        column: x => x.VoucherLineId,
                        principalSchema: "Finance",
                        principalTable: "VoucherLine",
                        principalColumn: "VoucherLineId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "Config",
                table: "Setting",
                columns: new[] { "SettingId", "DefaultValues", "DescriptionKey", "IsStandalone", "ModelType", "ParentId", "ScopeType", "Subsystem", "TitleKey", "Type", "Values" },
                values: new object[,]
                {
                    { 1, "{\"useLeafDetails\": true, \"useLeafCostCenters\": true,\"useLeafProjects\": true}", "AccountRelationsSettingsDescription", true, "RelationsConfig", null, (short)1, null, "AccountRelationsSettings", (short)2, "{\"useLeafDetails\": true, \"useLeafCostCenters\": true,\"useLeafProjects\": true}" },
                    { 2, "{\"defaultDateRange\": \"FiscalStartToFiscalEnd\"}", "QuickReportSettingsDescription", true, "DateRangeConfig", null, (short)0, null, "DateRangeFilterSettings", (short)2, "{\"defaultDateRange\": \"FiscalStartToFiscalEnd\"}" },
                    { 3, "{\"useSeparator\": true, \"separatorMode\": \"UseCustom\", \"separatorSymbol\": \", \", \"decimalPrecision\": 0, \"maxPrecision\": 8}", "NumberCurrencySettingsDescription", true, "NumberDisplayConfig", null, (short)0, null, "NumberCurrencySettings", (short)2, "{\"useSeparator\": true, \"separatorMode\": \"UseCustom\", \"separatorSymbol\": \",\" , \"decimalPrecision\": 0, \"maxPrecision\": 8}" },
                    { 5, "{}", "ViewTreeSettingsDescription", false, "ViewTreeConfig", null, (short)2, null, "ViewTreeSettings", (short)2, "{}" },
                    { 6, "{}", "QuickSearchSettingsDescription", false, "QuickSearchConfig", null, (short)2, null, "QuickSearchSettings", (short)3, "{}" },
                    { 8, "{\"defaultCurrencyNameKey\":\"CUnit_IranianRial\",\"defaultDecimalCount\":0,\"defaultCalendar\":0,\"defaultCalendars\": [{\"language\":\"fa\", \"calendar\":0}, {\"language\":\"en\", \"calendar\":1}],\"usesDefaultCoding\":true,\"inventoryMode\": 1}", "SystemConfigurationDescription", true, "SystemConfig", null, (short)1, null, "SystemConfigurationSettings", (short)2, "{\"defaultCurrencyNameKey\":\"CUnit_IranianRial\",\"defaultDecimalCount\":0,\"defaultCalendar\":0,\"defaultCalendars\": [{\"language\":\"fa\", \"calendar\":0}, {\"language\":\"en\", \"calendar\":1}],\"usesDefaultCoding\":true,\"inventoryMode\": 1}" },
                    { 9, "{\"openingAsFirstVoucher\":false,\"startTurnoverAsInitBalance\":false}", "FinanceReportSettingsDescription", true, "FinanceReportConfig", null, (short)1, null, "FinanceReportSettings", (short)2, "{\"openingAsFirstVoucher\":false,\"startTurnoverAsInitBalance\":false}" },
                    { 10, "{}", null, false, "FormLabelConfig", null, (short)3, null, "FormLabelSettings", (short)2, "{}" },
                    { 11, "{}", "UserProfileSettingsDescription", false, "UserProfileConfig", null, (short)1, null, "UserProfileSettings", (short)3, "{}" },
                    { 12, "{\"registerFlowConfig\":{\"confirmAfterSave\":true, \"approveAfterConfirm\": true, \"registerAfterApprove\": true},\"registerConfig\":{\"registerOnLastValidVoucher\": true, \"registerOnCreatedVoucher\": false, \"checkedVoucher\": false}}", "ReceiptSettingsDescription", true, "ReceiptConfig", null, (short)1, null, "ReceiptSettings", (short)2, "{\"registerFlowConfig\":{\"confirmAfterSave\":true, \"approveAfterConfirm\": true, \"registerAfterApprove\": true},\"registerConfig\":{\"registerWithLastValidVoucher\": true, \"registerWithNewCreatedVoucher\": false, \"checkedVoucher\": false}}" },
                    { 13, "{\"registerFlowConfig\":{\"confirmAfterSave\":true, \"approveAfterConfirm\": true, \"registerAfterApprove\": true},\"registerConfig\":{\"registerOnLastValidVoucher\": true, \"registerOnCreatedVoucher\": false, \"checkedVoucher\": false}}", "PaymentSettingsDescription", true, "PaymentSettings", null, (short)1, null, "PaymentSettings", (short)2, "{\"registerFlowConfig\":{\"confirmAfterSave\":true, \"approveAfterConfirm\": true, \"registerAfterApprove\": true},\"registerConfig\":{\"registerWithLastValidVoucher\": true, \"registerWithNewCreatedVoucher\": false, \"checkedVoucher\": false}}" }
                });

            migrationBuilder.InsertData(
                schema: "Config",
                table: "ViewSetting",
                columns: new[] { "ViewSettingId", "DefaultValues", "ModelType", "SettingId", "Values", "ViewId" },
                values: new object[,]
                {
                    { 1, "{\"viewId\":1,\"maxDepth\":3,\"levels\":[{\"no\":1,\"name\":\"LevelGeneral\",\"codeLength\":3,\"isEnabled\": true,\"isUsed\":true},{\"no\":2,\"name\":\"LevelAuxiliary\",\"codeLength\":3,\"isEnabled\": true,\"isUsed\":true},{\"no\":3,\"name\":\"LevelDetail\",\"codeLength\":4,\"isEnabled\": true,\"isUsed\":true},{\"no\":4,\"name\":\"LevelX\",\"codeLength\":4,\"isEnabled\":false,\"isUsed\":false},{\"no\":5,\"name\":\"LevelX\",\"codeLength\":4,\"isEnabled\":false,\"isUsed\":false},{\"no\":6,\"name\":\"LevelX\",\"codeLength\":4,\"isEnabled\":false,\"isUsed\":false},{\"no\":7,\"name\":\"LevelX\",\"codeLength\":4,\"isEnabled\":false,\"isUsed\":false},{\"no\":8,\"name\":\"LevelX\",\"codeLength\":4,\"isEnabled\":false,\"isUsed\":false},{\"no\":9,\"name\":\"LevelX\",\"codeLength\":4,\"isEnabled\":false,\"isUsed\":false},{\"no\":10,\"name\":\"LevelX\",\"codeLength\":4,\"isEnabled\":false,\"isUsed\":false},{\"no\":11,\"name\":\"LevelX\",\"codeLength\":4,\"isEnabled\":false,\"isUsed\":false},{\"no\":12,\"name\":\"LevelX\",\"codeLength\":4,\"isEnabled\":false,\"isUsed\":false},{\"no\":13,\"name\":\"LevelX\",\"codeLength\":4,\"isEnabled\":false,\"isUsed\":false},{\"no\":14,\"name\":\"LevelX\",\"codeLength\":4,\"isEnabled\":false,\"isUsed\":false},{\"no\":15,\"name\":\"LevelX\",\"codeLength\":4,\"isEnabled\":false,\"isUsed\":false},{\"no\":16,\"name\":\"LevelX\",\"codeLength\":4,\"isEnabled\":false,\"isUsed\":false}]}", "ViewTreeConfig", 5, "{\"viewId\":1,\"maxDepth\":3,\"levels\":[{\"no\":1,\"name\":\"LevelGeneral\",\"codeLength\":3,\"isEnabled\": true,\"isUsed\":true},{\"no\":2,\"name\":\"LevelAuxiliary\",\"codeLength\":3,\"isEnabled\": true,\"isUsed\":true},{\"no\":3,\"name\":\"LevelDetail\",\"codeLength\":4,\"isEnabled\": true,\"isUsed\":true},{\"no\":4,\"name\":\"LevelX\",\"codeLength\":4,\"isEnabled\":false,\"isUsed\":false},{\"no\":5,\"name\":\"LevelX\",\"codeLength\":4,\"isEnabled\":false,\"isUsed\":false},{\"no\":6,\"name\":\"LevelX\",\"codeLength\":4,\"isEnabled\":false,\"isUsed\":false},{\"no\":7,\"name\":\"LevelX\",\"codeLength\":4,\"isEnabled\":false,\"isUsed\":false},{\"no\":8,\"name\":\"LevelX\",\"codeLength\":4,\"isEnabled\":false,\"isUsed\":false},{\"no\":9,\"name\":\"LevelX\",\"codeLength\":4,\"isEnabled\":false,\"isUsed\":false},{\"no\":10,\"name\":\"LevelX\",\"codeLength\":4,\"isEnabled\":false,\"isUsed\":false},{\"no\":11,\"name\":\"LevelX\",\"codeLength\":4,\"isEnabled\":false,\"isUsed\":false},{\"no\":12,\"name\":\"LevelX\",\"codeLength\":4,\"isEnabled\":false,\"isUsed\":false},{\"no\":13,\"name\":\"LevelX\",\"codeLength\":4,\"isEnabled\":false,\"isUsed\":false},{\"no\":14,\"name\":\"LevelX\",\"codeLength\":4,\"isEnabled\":false,\"isUsed\":false},{\"no\":15,\"name\":\"LevelX\",\"codeLength\":4,\"isEnabled\":false,\"isUsed\":false},{\"no\":16,\"name\":\"LevelX\",\"codeLength\":4,\"isEnabled\":false,\"isUsed\":false}]}", 1 },
                    { 2, "{\"viewId\":6,\"maxDepth\":4,\"levels\":[{\"no\":1,\"name\":\"LevelX\",\"codeLength\":4,\"isEnabled\":true,\"isUsed\":false},{\"no\":2,\"name\":\"LevelX\",\"codeLength\":4,\"isEnabled\":true,\"isUsed\":false},{\"no\":3,\"name\":\"LevelX\",\"codeLength\":4,\"isEnabled\":true,\"isUsed\":false},{\"no\":4,\"name\":\"LevelX\",\"codeLength\":4,\"isEnabled\":true,\"isUsed\":false},{\"no\":5,\"name\":\"LevelX\",\"codeLength\":4,\"isEnabled\":false,\"isUsed\":false},{\"no\":6,\"name\":\"LevelX\",\"codeLength\":4,\"isEnabled\":false,\"isUsed\":false},{\"no\":7,\"name\":\"LevelX\",\"codeLength\":4,\"isEnabled\":false,\"isUsed\":false},{\"no\":8,\"name\":\"LevelX\",\"codeLength\":4,\"isEnabled\":false,\"isUsed\":false},{\"no\":9,\"name\":\"LevelX\",\"codeLength\":4,\"isEnabled\":false,\"isUsed\":false},{\"no\":10,\"name\":\"LevelX\",\"codeLength\":4,\"isEnabled\":false,\"isUsed\":false},{\"no\":11,\"name\":\"LevelX\",\"codeLength\":4,\"isEnabled\":false,\"isUsed\":false},{\"no\":12,\"name\":\"LevelX\",\"codeLength\":4,\"isEnabled\":false,\"isUsed\":false},{\"no\":13,\"name\":\"LevelX\",\"codeLength\":4,\"isEnabled\":false,\"isUsed\":false},{\"no\":14,\"name\":\"LevelX\",\"codeLength\":4,\"isEnabled\":false,\"isUsed\":false},{\"no\":15,\"name\":\"LevelX\",\"codeLength\":4,\"isEnabled\":false,\"isUsed\":false},{\"no\":16,\"name\":\"LevelX\",\"codeLength\":4,\"isEnabled\":false,\"isUsed\":false}]}", "ViewTreeConfig", 5, "{\"viewId\":6,\"maxDepth\":4,\"levels\":[{\"no\":1,\"name\":\"LevelX\",\"codeLength\":4,\"isEnabled\":true,\"isUsed\":false},{\"no\":2,\"name\":\"LevelX\",\"codeLength\":4,\"isEnabled\":true,\"isUsed\":false},{\"no\":3,\"name\":\"LevelX\",\"codeLength\":4,\"isEnabled\":true,\"isUsed\":false},{\"no\":4,\"name\":\"LevelX\",\"codeLength\":4,\"isEnabled\":true,\"isUsed\":false},{\"no\":5,\"name\":\"LevelX\",\"codeLength\":4,\"isEnabled\":false,\"isUsed\":false},{\"no\":6,\"name\":\"LevelX\",\"codeLength\":4,\"isEnabled\":false,\"isUsed\":false},{\"no\":7,\"name\":\"LevelX\",\"codeLength\":4,\"isEnabled\":false,\"isUsed\":false},{\"no\":8,\"name\":\"LevelX\",\"codeLength\":4,\"isEnabled\":false,\"isUsed\":false},{\"no\":9,\"name\":\"LevelX\",\"codeLength\":4,\"isEnabled\":false,\"isUsed\":false},{\"no\":10,\"name\":\"LevelX\",\"codeLength\":4,\"isEnabled\":false,\"isUsed\":false},{\"no\":11,\"name\":\"LevelX\",\"codeLength\":4,\"isEnabled\":false,\"isUsed\":false},{\"no\":12,\"name\":\"LevelX\",\"codeLength\":4,\"isEnabled\":false,\"isUsed\":false},{\"no\":13,\"name\":\"LevelX\",\"codeLength\":4,\"isEnabled\":false,\"isUsed\":false},{\"no\":14,\"name\":\"LevelX\",\"codeLength\":4,\"isEnabled\":false,\"isUsed\":false},{\"no\":15,\"name\":\"LevelX\",\"codeLength\":4,\"isEnabled\":false,\"isUsed\":false},{\"no\":16,\"name\":\"LevelX\",\"codeLength\":4,\"isEnabled\":false,\"isUsed\":false}]}", 6 },
                    { 3, "{\"viewId\":7,\"maxDepth\":4,\"levels\":[{\"no\":1,\"name\":\"LevelX\",\"codeLength\":4,\"isEnabled\":true,\"isUsed\":false},{\"no\":2,\"name\":\"LevelX\",\"codeLength\":4,\"isEnabled\":true,\"isUsed\":false},{\"no\":3,\"name\":\"LevelX\",\"codeLength\":4,\"isEnabled\":true,\"isUsed\":false},{\"no\":4,\"name\":\"LevelX\",\"codeLength\":4,\"isEnabled\":true,\"isUsed\":false},{\"no\":5,\"name\":\"LevelX\",\"codeLength\":4,\"isEnabled\":false,\"isUsed\":false},{\"no\":6,\"name\":\"LevelX\",\"codeLength\":4,\"isEnabled\":false,\"isUsed\":false},{\"no\":7,\"name\":\"LevelX\",\"codeLength\":4,\"isEnabled\":false,\"isUsed\":false},{\"no\":8,\"name\":\"LevelX\",\"codeLength\":4,\"isEnabled\":false,\"isUsed\":false},{\"no\":9,\"name\":\"LevelX\",\"codeLength\":4,\"isEnabled\":false,\"isUsed\":false},{\"no\":10,\"name\":\"LevelX\",\"codeLength\":4,\"isEnabled\":false,\"isUsed\":false},{\"no\":11,\"name\":\"LevelX\",\"codeLength\":4,\"isEnabled\":false,\"isUsed\":false},{\"no\":12,\"name\":\"LevelX\",\"codeLength\":4,\"isEnabled\":false,\"isUsed\":false},{\"no\":13,\"name\":\"LevelX\",\"codeLength\":4,\"isEnabled\":false,\"isUsed\":false},{\"no\":14,\"name\":\"LevelX\",\"codeLength\":4,\"isEnabled\":false,\"isUsed\":false},{\"no\":15,\"name\":\"LevelX\",\"codeLength\":4,\"isEnabled\":false,\"isUsed\":false},{\"no\":16,\"name\":\"LevelX\",\"codeLength\":4,\"isEnabled\":false,\"isUsed\":false}]}", "ViewTreeConfig", 5, "{\"viewId\":7,\"maxDepth\":4,\"levels\":[{\"no\":1,\"name\":\"LevelX\",\"codeLength\":4,\"isEnabled\":true,\"isUsed\":false},{\"no\":2,\"name\":\"LevelX\",\"codeLength\":4,\"isEnabled\":true,\"isUsed\":false},{\"no\":3,\"name\":\"LevelX\",\"codeLength\":4,\"isEnabled\":true,\"isUsed\":false},{\"no\":4,\"name\":\"LevelX\",\"codeLength\":4,\"isEnabled\":true,\"isUsed\":false},{\"no\":5,\"name\":\"LevelX\",\"codeLength\":4,\"isEnabled\":false,\"isUsed\":false},{\"no\":6,\"name\":\"LevelX\",\"codeLength\":4,\"isEnabled\":false,\"isUsed\":false},{\"no\":7,\"name\":\"LevelX\",\"codeLength\":4,\"isEnabled\":false,\"isUsed\":false},{\"no\":8,\"name\":\"LevelX\",\"codeLength\":4,\"isEnabled\":false,\"isUsed\":false},{\"no\":9,\"name\":\"LevelX\",\"codeLength\":4,\"isEnabled\":false,\"isUsed\":false},{\"no\":10,\"name\":\"LevelX\",\"codeLength\":4,\"isEnabled\":false,\"isUsed\":false},{\"no\":11,\"name\":\"LevelX\",\"codeLength\":4,\"isEnabled\":false,\"isUsed\":false},{\"no\":12,\"name\":\"LevelX\",\"codeLength\":4,\"isEnabled\":false,\"isUsed\":false},{\"no\":13,\"name\":\"LevelX\",\"codeLength\":4,\"isEnabled\":false,\"isUsed\":false},{\"no\":14,\"name\":\"LevelX\",\"codeLength\":4,\"isEnabled\":false,\"isUsed\":false},{\"no\":15,\"name\":\"LevelX\",\"codeLength\":4,\"isEnabled\":false,\"isUsed\":false},{\"no\":16,\"name\":\"LevelX\",\"codeLength\":4,\"isEnabled\":false,\"isUsed\":false}]}", 7 },
                    { 4, "{\"viewId\":8,\"maxDepth\":4,\"levels\":[{\"no\":1,\"name\":\"LevelX\",\"codeLength\":4,\"isEnabled\":true,\"isUsed\":false},{\"no\":2,\"name\":\"LevelX\",\"codeLength\":4,\"isEnabled\":true,\"isUsed\":false},{\"no\":3,\"name\":\"LevelX\",\"codeLength\":4,\"isEnabled\":true,\"isUsed\":false},{\"no\":4,\"name\":\"LevelX\",\"codeLength\":4,\"isEnabled\":true,\"isUsed\":false},{\"no\":5,\"name\":\"LevelX\",\"codeLength\":4,\"isEnabled\":false,\"isUsed\":false},{\"no\":6,\"name\":\"LevelX\",\"codeLength\":4,\"isEnabled\":false,\"isUsed\":false},{\"no\":7,\"name\":\"LevelX\",\"codeLength\":4,\"isEnabled\":false,\"isUsed\":false},{\"no\":8,\"name\":\"LevelX\",\"codeLength\":4,\"isEnabled\":false,\"isUsed\":false},{\"no\":9,\"name\":\"LevelX\",\"codeLength\":4,\"isEnabled\":false,\"isUsed\":false},{\"no\":10,\"name\":\"LevelX\",\"codeLength\":4,\"isEnabled\":false,\"isUsed\":false},{\"no\":11,\"name\":\"LevelX\",\"codeLength\":4,\"isEnabled\":false,\"isUsed\":false},{\"no\":12,\"name\":\"LevelX\",\"codeLength\":4,\"isEnabled\":false,\"isUsed\":false},{\"no\":13,\"name\":\"LevelX\",\"codeLength\":4,\"isEnabled\":false,\"isUsed\":false},{\"no\":14,\"name\":\"LevelX\",\"codeLength\":4,\"isEnabled\":false,\"isUsed\":false},{\"no\":15,\"name\":\"LevelX\",\"codeLength\":4,\"isEnabled\":false,\"isUsed\":false},{\"no\":16,\"name\":\"LevelX\",\"codeLength\":4,\"isEnabled\":false,\"isUsed\":false}]}", "ViewTreeConfig", 5, "{\"viewId\":8,\"maxDepth\":4,\"levels\":[{\"no\":1,\"name\":\"LevelX\",\"codeLength\":4,\"isEnabled\":true,\"isUsed\":false},{\"no\":2,\"name\":\"LevelX\",\"codeLength\":4,\"isEnabled\":true,\"isUsed\":false},{\"no\":3,\"name\":\"LevelX\",\"codeLength\":4,\"isEnabled\":true,\"isUsed\":false},{\"no\":4,\"name\":\"LevelX\",\"codeLength\":4,\"isEnabled\":true,\"isUsed\":false},{\"no\":5,\"name\":\"LevelX\",\"codeLength\":4,\"isEnabled\":false,\"isUsed\":false},{\"no\":6,\"name\":\"LevelX\",\"codeLength\":4,\"isEnabled\":false,\"isUsed\":false},{\"no\":7,\"name\":\"LevelX\",\"codeLength\":4,\"isEnabled\":false,\"isUsed\":false},{\"no\":8,\"name\":\"LevelX\",\"codeLength\":4,\"isEnabled\":false,\"isUsed\":false},{\"no\":9,\"name\":\"LevelX\",\"codeLength\":4,\"isEnabled\":false,\"isUsed\":false},{\"no\":10,\"name\":\"LevelX\",\"codeLength\":4,\"isEnabled\":false,\"isUsed\":false},{\"no\":11,\"name\":\"LevelX\",\"codeLength\":4,\"isEnabled\":false,\"isUsed\":false},{\"no\":12,\"name\":\"LevelX\",\"codeLength\":4,\"isEnabled\":false,\"isUsed\":false},{\"no\":13,\"name\":\"LevelX\",\"codeLength\":4,\"isEnabled\":false,\"isUsed\":false},{\"no\":14,\"name\":\"LevelX\",\"codeLength\":4,\"isEnabled\":false,\"isUsed\":false},{\"no\":15,\"name\":\"LevelX\",\"codeLength\":4,\"isEnabled\":false,\"isUsed\":false},{\"no\":16,\"name\":\"LevelX\",\"codeLength\":4,\"isEnabled\":false,\"isUsed\":false}]}", 8 }
                });

            migrationBuilder.InsertData(
                schema: "Core",
                table: "Version",
                columns: new[] { "VersionId", "ModifiedDate", "Number", "rowguid" },
                values: new object[] { 1, new DateTime(2022, 8, 27, 13, 56, 52, 150, DateTimeKind.Unspecified), "2.2.0", new Guid("26452115-8352-42fe-a7b8-4bd3d32f50f6") });

            migrationBuilder.InsertData(
                schema: "Corporate",
                table: "Branch",
                columns: new[] { "BranchId", "Description", "Name", "ParentId" },
                values: new object[] { 1, "", "دفتر مرکزی", null });

            migrationBuilder.InsertData(
                schema: "Metadata",
                table: "EntityType",
                columns: new[] { "EntityTypeId", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "", "Account" },
                    { 100004, "", "Attribute" },
                    { 100003, "", "Property" },
                    { 100001, "", "Brand" },
                    { 25, "", "Payment" },
                    { 24, "", "Receipt" },
                    { 23, "", "SourceApp" },
                    { 22, "", "CashRegister" },
                    { 21, "", "CheckBook" },
                    { 20, "", "Widget" },
                    { 19, "", "DashboardTab" },
                    { 100002, "", "Unit" },
                    { 17, "", "Voucher" },
                    { 18, "", "DraftVoucher" },
                    { 2, "", "AccountCollectionAccount" },
                    { 4, "", "AccountGroup" },
                    { 6, "", "CostCenter" },
                    { 7, "", "Currency" },
                    { 5, "", "Branch" },
                    { 10, "", "FiscalPeriod" },
                    { 11, "", "OperationLog" },
                    { 12, "", "Project" },
                    { 15, "", "Setting" },
                    { 9, "", "DetailAccount" }
                });

            migrationBuilder.InsertData(
                schema: "Metadata",
                table: "Operation",
                columns: new[] { "OperationId", "Description", "Name" },
                values: new object[] { 71, "", "CreateAccountLine" });

            migrationBuilder.InsertData(
                schema: "Metadata",
                table: "Operation",
                columns: new[] { "OperationId", "Description", "Name" },
                values: new object[,]
                {
                    { 65, "", "DisconnectFromCheck" },
                    { 70, "", "AggregateAccountLines" },
                    { 69, "", "RemoveInvalidAccountLines" },
                    { 68, "", "Register" },
                    { 67, "", "UndoArchive" },
                    { 66, "", "AssignCashRegisterUser" },
                    { 64, "", "ConnectToCheck" },
                    { 56, "", "FilterRates" },
                    { 62, "", "CancelPage" },
                    { 61, "", "DeletePages" },
                    { 60, "", "CreatePages" },
                    { 58, "", "PrintPreview" },
                    { 55, "", "ExportRates" },
                    { 54, "", "Export" },
                    { 53, "", "GroupNormalize" },
                    { 72, "", "EditAccountLine" },
                    { 63, "", "UndoCancelPage" },
                    { 73, "", "DeleteAccountLine" },
                    { 85, "", "FilterCashAccountLines" },
                    { 75, "", "PrintAccountLines" },
                    { 93, "", "UndoRegister" },
                    { 92, "", "PrintPreviewForm" },
                    { 91, "", "PrintForm" },
                    { 90, "", "Reactivate" },
                    { 89, "", "Deactivate" },
                    { 88, "", "AggregateCashAccountLines" },
                    { 87, "", "RemoveInvalidCashAccountLines" },
                    { 86, "", "ExportCashAccountLines" },
                    { 74, "", "GroupDeleteAccountLines" },
                    { 52, "", "Normalize" },
                    { 83, "", "PrintCashAccountLines" },
                    { 82, "", "GroupDeleteCashAccountLines" },
                    { 81, "", "DeleteCashAccountLine" },
                    { 80, "", "EditCashAccountLine" },
                    { 79, "", "CreateCashAccountLine" },
                    { 78, "", "ExportAccountLines" },
                    { 77, "", "FilterAccountLines" },
                    { 76, "", "PrintPreviewAccountLines" },
                    { 84, "", "PrintPreviewCashAccountLines" },
                    { 51, "", "GroupUndoConfirm" },
                    { 48, "", "GroupFinalize" },
                    { 49, "", "GroupUndoFinalize" }
                });

            migrationBuilder.InsertData(
                schema: "Metadata",
                table: "Operation",
                columns: new[] { "OperationId", "Description", "Name" },
                values: new object[,]
                {
                    { 17, "", "Finalize" },
                    { 16, "", "UndoApprove" },
                    { 15, "", "Approve" },
                    { 14, "", "UndoConfirm" },
                    { 13, "", "Confirm" },
                    { 12, "", "UndoCheck" },
                    { 50, "", "GroupConfirm" },
                    { 10, "", "Design" },
                    { 9, "", "SetDefault" },
                    { 8, "", "Archive" },
                    { 7, "", "Save" },
                    { 6, "", "Print" },
                    { 5, "", "Filter" },
                    { 4, "", "Delete" },
                    { 3, "", "Edit" },
                    { 2, "", "Create" },
                    { 1, "", "View" },
                    { 18, "", "UndoFinalize" },
                    { 19, "", "Mark" },
                    { 11, "", "Check" },
                    { 21, "", "GroupDelete" },
                    { 20, "", "QuickReportDesign" },
                    { 47, "", "GroupUndoCheck" },
                    { 46, "", "GroupCheck" },
                    { 45, "", "ViewRates" },
                    { 44, "", "GroupDeleteRates" },
                    { 43, "", "PrintRates" },
                    { 42, "", "DeleteRate" },
                    { 41, "", "EditRate" },
                    { 39, "", "GroupDeleteLines" },
                    { 40, "", "CreateRate" },
                    { 37, "", "EditLine" },
                    { 36, "", "CreateLine" },
                    { 35, "", "RoleAccess" },
                    { 34, "", "DefaultCodingChange" },
                    { 33, "", "DecimalCountChange" },
                    { 32, "", "CurrencyChange" },
                    { 31, "", "CalendarChange" },
                    { 30, "", "ViewArchive" },
                    { 38, "", "DeleteLine" }
                });

            migrationBuilder.InsertData(
                schema: "Metadata",
                table: "OperationSource",
                columns: new[] { "OperationSourceId", "Description", "Name" },
                values: new object[,]
                {
                    { 6, "", "BalanceByAccount" },
                    { 15, "", "CheckBookReport" }
                });

            migrationBuilder.InsertData(
                schema: "Metadata",
                table: "OperationSource",
                columns: new[] { "OperationSourceId", "Description", "Name" },
                values: new object[,]
                {
                    { 13, "", "SystemIssue" },
                    { 12, "", "BalanceSheet" },
                    { 11, "", "AccountRelations" },
                    { 10, "", "ProfitLoss" },
                    { 1, "", "Journal" },
                    { 5, "", "ItemBalance" },
                    { 4, "", "TestBalance" },
                    { 3, "", "CurrencyBook" },
                    { 2, "", "AccountBook" },
                    { 9, "", "EnvironmentParams" }
                });

            migrationBuilder.InsertData(
                schema: "Metadata",
                table: "OperationSourceType",
                columns: new[] { "OperationSourceTypeId", "Name" },
                values: new object[,]
                {
                    { 1, "BaseData" },
                    { 2, "OperationalForms" },
                    { 3, "Reports" }
                });

            migrationBuilder.InsertData(
                schema: "Metadata",
                table: "Subsystem",
                columns: new[] { "SubsystemId", "Name" },
                values: new object[,]
                {
                    { 1, "Administration" },
                    { 2, "Accounting" },
                    { 3, "Treasury" },
                    { 100000, "ProductScope" }
                });

            migrationBuilder.InsertData(
                schema: "Config",
                table: "LogSetting",
                columns: new[] { "LogSettingId", "EntityTypeId", "IsEnabled", "OperationId", "SourceId", "SourceTypeId", "SubsystemId" },
                values: new object[,]
                {
                    { 167, null, true, 5, 4, 3, 2 },
                    { 207, 21, true, 58, null, 2, 3 },
                    { 206, 21, true, 6, null, 2, 3 },
                    { 205, 21, true, 5, null, 2, 3 },
                    { 204, 21, true, 4, null, 2, 3 },
                    { 203, 21, true, 3, null, 2, 3 },
                    { 202, 21, true, 2, null, 2, 3 },
                    { 201, 21, true, 1, null, 2, 3 },
                    { 326, 11, true, 21, null, 2, 2 },
                    { 323, 18, true, 92, null, 2, 2 },
                    { 322, 18, true, 91, null, 2, 2 },
                    { 321, 18, true, 54, null, 2, 2 },
                    { 320, 18, true, 5, null, 2, 2 },
                    { 319, 17, true, 92, null, 2, 2 },
                    { 318, 17, true, 91, null, 2, 2 },
                    { 317, 17, true, 54, null, 2, 2 },
                    { 316, 17, true, 5, null, 2, 2 },
                    { 311, 12, true, 90, null, 1, 2 },
                    { 208, 21, true, 60, null, 2, 3 },
                    { 310, 12, true, 89, null, 1, 2 },
                    { 209, 21, true, 61, null, 2, 3 },
                    { 211, 21, true, 63, null, 2, 3 },
                    { 230, null, true, 67, 15, 3, 3 },
                    { 229, null, true, 58, 15, 3, 3 },
                    { 228, null, true, 54, 15, 3, 3 },
                    { 227, null, true, 8, 15, 3, 3 },
                    { 226, null, true, 6, 15, 3, 3 },
                    { 225, null, true, 5, 15, 3, 3 },
                    { 224, null, true, 1, 15, 3, 3 },
                    { 223, 22, true, 66, null, 1, 3 },
                    { 222, 22, true, 58, null, 1, 3 },
                    { 221, 22, true, 54, null, 1, 3 },
                    { 220, 22, true, 21, null, 1, 3 },
                    { 219, 22, true, 6, null, 1, 3 },
                    { 218, 22, true, 5, null, 1, 3 },
                    { 217, 22, true, 4, null, 1, 3 },
                    { 216, 22, true, 3, null, 1, 3 },
                    { 213, 21, true, 65, null, 2, 3 },
                    { 212, 21, true, 64, null, 2, 3 },
                    { 210, 21, true, 62, null, 2, 3 },
                    { 231, 23, true, 1, null, 1, 3 },
                    { 309, 9, true, 90, null, 1, 2 }
                });

            migrationBuilder.InsertData(
                schema: "Config",
                table: "LogSetting",
                columns: new[] { "LogSettingId", "EntityTypeId", "IsEnabled", "OperationId", "SourceId", "SourceTypeId", "SubsystemId" },
                values: new object[,]
                {
                    { 307, 7, true, 90, null, 1, 2 },
                    { 186, null, true, 6, 10, 3, 2 },
                    { 185, null, true, 5, 10, 3, 2 },
                    { 184, null, true, 1, 10, 3, 2 },
                    { 183, null, true, 34, 9, 1, 2 },
                    { 182, null, true, 33, 9, 1, 2 },
                    { 181, null, true, 32, 9, 1, 2 },
                    { 180, null, true, 58, 6, 3, 2 },
                    { 179, null, true, 54, 6, 3, 2 },
                    { 178, null, true, 6, 6, 3, 2 },
                    { 177, null, true, 5, 6, 3, 2 },
                    { 176, null, true, 1, 6, 3, 2 },
                    { 175, null, true, 58, 5, 3, 2 },
                    { 174, null, true, 54, 5, 3, 2 },
                    { 173, null, true, 6, 5, 3, 2 },
                    { 172, null, true, 5, 5, 3, 2 },
                    { 171, null, true, 1, 5, 3, 2 },
                    { 170, null, true, 58, 4, 3, 2 },
                    { 187, null, true, 54, 10, 3, 2 },
                    { 308, 9, true, 89, null, 1, 2 },
                    { 188, null, true, 58, 10, 3, 2 },
                    { 190, null, true, 7, 11, 1, 2 },
                    { 306, 7, true, 89, null, 1, 2 },
                    { 305, 6, true, 90, null, 1, 2 },
                    { 304, 6, true, 89, null, 1, 2 },
                    { 303, 1, true, 90, null, 1, 2 },
                    { 302, 1, true, 89, null, 1, 2 },
                    { 215, 22, true, 2, null, 1, 2 },
                    { 214, 22, true, 1, null, 1, 2 },
                    { 200, null, true, 58, 13, 3, 2 },
                    { 199, null, true, 54, 13, 3, 2 },
                    { 198, null, true, 6, 13, 3, 2 },
                    { 197, null, true, 5, 13, 3, 2 },
                    { 196, null, true, 1, 13, 3, 2 },
                    { 195, null, true, 58, 12, 3, 2 },
                    { 194, null, true, 54, 12, 3, 2 },
                    { 193, null, true, 6, 12, 3, 2 },
                    { 192, null, true, 5, 12, 3, 2 },
                    { 191, null, true, 1, 12, 3, 2 },
                    { 189, null, true, 1, 11, 1, 2 },
                    { 169, null, true, 54, 4, 3, 2 },
                    { 232, 23, true, 2, null, 1, 3 }
                });

            migrationBuilder.InsertData(
                schema: "Config",
                table: "LogSetting",
                columns: new[] { "LogSettingId", "EntityTypeId", "IsEnabled", "OperationId", "SourceId", "SourceTypeId", "SubsystemId" },
                values: new object[,]
                {
                    { 234, 23, true, 4, null, 1, 3 },
                    { 293, 25, false, 82, null, 2, 3 },
                    { 292, 25, false, 81, null, 2, 3 },
                    { 291, 25, false, 80, null, 2, 3 },
                    { 290, 25, false, 79, null, 2, 3 },
                    { 289, 24, false, 86, null, 2, 3 },
                    { 288, 24, false, 85, null, 2, 3 },
                    { 287, 24, false, 84, null, 2, 3 },
                    { 286, 24, false, 83, null, 2, 3 },
                    { 285, 24, false, 82, null, 2, 3 },
                    { 284, 24, false, 81, null, 2, 3 },
                    { 283, 24, false, 80, null, 2, 3 },
                    { 282, 24, false, 79, null, 2, 3 },
                    { 281, 25, false, 78, null, 2, 3 },
                    { 280, 25, false, 77, null, 2, 3 },
                    { 279, 25, false, 76, null, 2, 3 },
                    { 278, 25, false, 75, null, 2, 3 },
                    { 277, 24, false, 78, null, 2, 3 },
                    { 294, 25, false, 83, null, 2, 3 },
                    { 276, 24, false, 77, null, 2, 3 },
                    { 295, 25, false, 84, null, 2, 3 },
                    { 297, 25, false, 86, null, 2, 3 },
                    { 100006, 100001, true, 21, null, 1, 100000 },
                    { 100005, 100001, true, 6, null, 1, 100000 },
                    { 100004, 100001, true, 5, null, 1, 100000 },
                    { 100003, 100001, true, 4, null, 1, 100000 },
                    { 100002, 100001, true, 3, null, 1, 100000 },
                    { 100001, 100001, true, 2, null, 1, 100000 },
                    { 100000, 100001, true, 1, null, 1, 100000 },
                    { 325, 25, true, 93, null, 2, 3 },
                    { 324, 24, true, 93, null, 2, 3 },
                    { 315, 23, true, 90, null, 1, 3 },
                    { 314, 23, true, 89, null, 1, 3 },
                    { 313, 22, true, 90, null, 1, 3 },
                    { 312, 22, true, 89, null, 1, 3 },
                    { 301, 25, false, 88, null, 2, 3 },
                    { 300, 25, false, 87, null, 2, 3 },
                    { 299, 24, false, 88, null, 2, 3 },
                    { 298, 24, false, 87, null, 2, 3 },
                    { 296, 25, false, 85, null, 2, 3 },
                    { 233, 23, true, 3, null, 1, 3 },
                    { 275, 24, false, 76, null, 2, 3 }
                });

            migrationBuilder.InsertData(
                schema: "Config",
                table: "LogSetting",
                columns: new[] { "LogSettingId", "EntityTypeId", "IsEnabled", "OperationId", "SourceId", "SourceTypeId", "SubsystemId" },
                values: new object[,]
                {
                    { 273, 24, false, 74, null, 2, 3 },
                    { 251, 25, true, 1, null, 2, 3 },
                    { 250, 24, true, 68, null, 2, 3 },
                    { 249, 24, true, 58, null, 2, 3 },
                    { 248, 24, true, 16, null, 2, 3 },
                    { 247, 24, true, 15, null, 2, 3 },
                    { 246, 24, true, 14, null, 2, 3 },
                    { 245, 24, true, 13, null, 2, 3 },
                    { 244, 24, true, 6, null, 2, 3 },
                    { 243, 24, true, 4, null, 2, 3 },
                    { 242, 24, true, 3, null, 2, 3 },
                    { 241, 24, true, 2, null, 2, 3 },
                    { 240, 24, true, 1, null, 2, 3 },
                    { 239, 23, true, 58, null, 1, 3 },
                    { 238, 23, true, 54, null, 1, 3 },
                    { 237, 23, true, 21, null, 1, 3 },
                    { 236, 23, true, 6, null, 1, 3 },
                    { 235, 23, true, 5, null, 1, 3 },
                    { 252, 25, true, 2, null, 2, 3 },
                    { 274, 24, false, 75, null, 2, 3 },
                    { 253, 25, true, 3, null, 2, 3 },
                    { 255, 25, true, 6, null, 2, 3 },
                    { 272, 24, false, 73, null, 2, 3 },
                    { 271, 24, false, 72, null, 2, 3 },
                    { 270, 24, false, 71, null, 2, 3 },
                    { 269, 24, false, 70, null, 2, 3 },
                    { 268, 24, false, 69, null, 2, 3 },
                    { 267, 25, false, 74, null, 2, 3 },
                    { 266, 25, false, 73, null, 2, 3 },
                    { 265, 25, false, 72, null, 2, 3 },
                    { 264, 25, false, 71, null, 2, 3 },
                    { 263, 25, false, 70, null, 2, 3 },
                    { 262, 25, false, 69, null, 2, 3 },
                    { 261, 25, true, 68, null, 2, 3 },
                    { 260, 25, true, 58, null, 2, 3 },
                    { 259, 25, true, 16, null, 2, 3 },
                    { 258, 25, true, 15, null, 2, 3 },
                    { 257, 25, true, 14, null, 2, 3 },
                    { 256, 25, true, 13, null, 2, 3 },
                    { 254, 25, true, 4, null, 2, 3 },
                    { 168, null, true, 6, 4, 3, 2 },
                    { 100008, 100001, true, 58, null, 1, 100000 }
                });

            migrationBuilder.InsertData(
                schema: "Config",
                table: "LogSetting",
                columns: new[] { "LogSettingId", "EntityTypeId", "IsEnabled", "OperationId", "SourceId", "SourceTypeId", "SubsystemId" },
                values: new object[,]
                {
                    { 166, null, true, 1, 4, 3, 2 },
                    { 59, 9, true, 3, null, 1, 2 },
                    { 58, 9, true, 2, null, 1, 2 },
                    { 57, 9, true, 1, null, 1, 2 },
                    { 56, 7, true, 58, null, 1, 2 },
                    { 55, 7, true, 56, null, 1, 2 },
                    { 54, 7, true, 55, null, 1, 2 },
                    { 53, 7, true, 54, null, 1, 2 },
                    { 52, 7, true, 45, null, 1, 2 },
                    { 51, 7, true, 44, null, 1, 2 },
                    { 50, 7, true, 43, null, 1, 2 },
                    { 49, 7, true, 42, null, 1, 2 },
                    { 48, 7, true, 41, null, 1, 2 },
                    { 47, 7, true, 40, null, 1, 2 },
                    { 46, 7, true, 21, null, 1, 2 },
                    { 45, 7, true, 6, null, 1, 2 },
                    { 44, 7, true, 5, null, 1, 2 },
                    { 43, 7, true, 4, null, 1, 2 },
                    { 60, 9, true, 4, null, 1, 2 },
                    { 42, 7, true, 3, null, 1, 2 },
                    { 61, 9, true, 5, null, 1, 2 },
                    { 63, 9, true, 21, null, 1, 2 },
                    { 80, 11, true, 8, null, 2, 2 },
                    { 79, 11, true, 6, null, 2, 2 },
                    { 78, 11, true, 5, null, 2, 2 },
                    { 77, 11, true, 4, null, 2, 2 },
                    { 76, 11, true, 1, null, 2, 2 },
                    { 75, 10, true, 58, null, 1, 2 },
                    { 74, 10, true, 54, null, 1, 2 },
                    { 73, 10, true, 35, null, 1, 2 },
                    { 72, 10, true, 21, null, 1, 2 },
                    { 71, 10, true, 6, null, 1, 2 },
                    { 70, 10, true, 5, null, 1, 2 },
                    { 69, 10, true, 4, null, 1, 2 },
                    { 68, 10, true, 3, null, 1, 2 },
                    { 67, 10, true, 2, null, 1, 2 },
                    { 66, 10, true, 1, null, 1, 2 },
                    { 65, 9, true, 58, null, 1, 2 },
                    { 64, 9, true, 54, null, 1, 2 },
                    { 62, 9, true, 6, null, 1, 2 },
                    { 81, 11, true, 30, null, 2, 2 },
                    { 41, 7, true, 2, null, 1, 2 }
                });

            migrationBuilder.InsertData(
                schema: "Config",
                table: "LogSetting",
                columns: new[] { "LogSettingId", "EntityTypeId", "IsEnabled", "OperationId", "SourceId", "SourceTypeId", "SubsystemId" },
                values: new object[,]
                {
                    { 39, 6, true, 58, null, 1, 2 },
                    { 17, 4, true, 6, null, 1, 2 },
                    { 16, 4, true, 5, null, 1, 2 },
                    { 15, 4, true, 4, null, 1, 2 },
                    { 14, 4, true, 3, null, 1, 2 },
                    { 13, 4, true, 2, null, 1, 2 },
                    { 12, 4, true, 1, null, 1, 2 },
                    { 11, 2, true, 7, null, 1, 2 },
                    { 10, 2, true, 1, null, 1, 2 },
                    { 9, 1, true, 58, null, 1, 2 },
                    { 8, 1, true, 54, null, 1, 2 },
                    { 7, 1, true, 21, null, 1, 2 },
                    { 6, 1, true, 6, null, 1, 2 },
                    { 5, 1, true, 5, null, 1, 2 },
                    { 4, 1, true, 4, null, 1, 2 },
                    { 3, 1, true, 3, null, 1, 2 },
                    { 2, 1, true, 2, null, 1, 2 },
                    { 1, 1, true, 1, null, 1, 2 },
                    { 18, 4, true, 21, null, 1, 2 },
                    { 40, 7, true, 1, null, 1, 2 },
                    { 19, 4, true, 54, null, 1, 2 },
                    { 21, 5, true, 1, null, 1, 2 },
                    { 38, 6, true, 54, null, 1, 2 },
                    { 37, 6, true, 21, null, 1, 2 },
                    { 36, 6, true, 6, null, 1, 2 },
                    { 35, 6, true, 5, null, 1, 2 },
                    { 34, 6, true, 4, null, 1, 2 },
                    { 33, 6, true, 3, null, 1, 2 },
                    { 32, 6, true, 2, null, 1, 2 },
                    { 31, 6, true, 1, null, 1, 2 },
                    { 30, 5, true, 58, null, 1, 2 },
                    { 29, 5, true, 54, null, 1, 2 },
                    { 28, 5, true, 35, null, 1, 2 },
                    { 27, 5, true, 21, null, 1, 2 },
                    { 26, 5, true, 6, null, 1, 2 },
                    { 25, 5, true, 5, null, 1, 2 },
                    { 24, 5, true, 4, null, 1, 2 },
                    { 23, 5, true, 3, null, 1, 2 },
                    { 22, 5, true, 2, null, 1, 2 },
                    { 20, 4, true, 58, null, 1, 2 },
                    { 82, 11, true, 54, null, 2, 2 },
                    { 83, 11, true, 58, null, 2, 2 }
                });

            migrationBuilder.InsertData(
                schema: "Config",
                table: "LogSetting",
                columns: new[] { "LogSettingId", "EntityTypeId", "IsEnabled", "OperationId", "SourceId", "SourceTypeId", "SubsystemId" },
                values: new object[,]
                {
                    { 84, 12, true, 1, null, 1, 2 },
                    { 144, 20, true, 2, null, 3, 2 },
                    { 143, 20, true, 1, null, 3, 2 },
                    { 142, 19, true, 54, null, 3, 2 },
                    { 141, 19, true, 6, null, 3, 2 },
                    { 140, 19, true, 4, null, 3, 2 },
                    { 139, 19, true, 3, null, 3, 2 },
                    { 138, 19, true, 2, null, 3, 2 },
                    { 137, 18, true, 58, null, 2, 2 },
                    { 136, 18, true, 53, null, 2, 2 },
                    { 135, 18, true, 52, null, 2, 2 },
                    { 134, 18, true, 47, null, 2, 2 },
                    { 133, 18, true, 46, null, 2, 2 },
                    { 132, 18, true, 39, null, 2, 2 },
                    { 131, 18, true, 38, null, 2, 2 },
                    { 130, 18, true, 37, null, 2, 2 },
                    { 129, 18, true, 36, null, 2, 2 },
                    { 128, 18, true, 21, null, 2, 2 },
                    { 145, 20, true, 3, null, 3, 2 },
                    { 127, 18, true, 12, null, 2, 2 },
                    { 146, 20, true, 4, null, 3, 2 },
                    { 148, 20, true, 6, null, 3, 2 },
                    { 165, null, true, 58, 3, 3, 2 },
                    { 164, null, true, 54, 3, 3, 2 },
                    { 163, null, true, 6, 3, 3, 2 },
                    { 162, null, true, 5, 3, 3, 2 },
                    { 161, null, true, 1, 3, 3, 2 },
                    { 160, null, true, 58, 2, 3, 2 },
                    { 159, null, true, 54, 2, 3, 2 },
                    { 158, null, true, 6, 2, 3, 2 },
                    { 157, null, true, 5, 2, 3, 2 },
                    { 156, null, true, 1, 2, 3, 2 },
                    { 155, null, true, 58, 1, 3, 2 },
                    { 154, null, true, 54, 1, 3, 2 },
                    { 153, null, true, 6, 1, 3, 2 },
                    { 152, null, true, 5, 1, 3, 2 },
                    { 151, null, true, 1, 1, 3, 2 },
                    { 150, 20, true, 58, null, 3, 2 },
                    { 149, 20, true, 54, null, 3, 2 },
                    { 147, 20, true, 5, null, 3, 2 },
                    { 126, 18, true, 11, null, 2, 2 },
                    { 125, 18, true, 6, null, 2, 2 }
                });

            migrationBuilder.InsertData(
                schema: "Config",
                table: "LogSetting",
                columns: new[] { "LogSettingId", "EntityTypeId", "IsEnabled", "OperationId", "SourceId", "SourceTypeId", "SubsystemId" },
                values: new object[,]
                {
                    { 124, 18, true, 4, null, 2, 2 },
                    { 101, 17, true, 11, null, 2, 2 },
                    { 100, 17, true, 6, null, 2, 2 },
                    { 99, 17, true, 4, null, 2, 2 },
                    { 98, 17, true, 3, null, 2, 2 },
                    { 97, 17, true, 2, null, 2, 2 },
                    { 96, 17, true, 1, null, 2, 2 },
                    { 95, 15, true, 31, null, 1, 2 },
                    { 94, 15, true, 7, null, 1, 2 },
                    { 93, 15, true, 1, null, 1, 2 },
                    { 92, 12, true, 58, null, 1, 2 },
                    { 91, 12, true, 54, null, 1, 2 },
                    { 90, 12, true, 21, null, 1, 2 },
                    { 89, 12, true, 6, null, 1, 2 },
                    { 88, 12, true, 5, null, 1, 2 },
                    { 87, 12, true, 4, null, 1, 2 },
                    { 86, 12, true, 3, null, 1, 2 },
                    { 85, 12, true, 2, null, 1, 2 },
                    { 102, 17, true, 12, null, 2, 2 },
                    { 103, 17, true, 13, null, 2, 2 },
                    { 104, 17, true, 14, null, 2, 2 },
                    { 105, 17, true, 15, null, 2, 2 },
                    { 123, 18, true, 3, null, 2, 2 },
                    { 122, 18, true, 2, null, 2, 2 },
                    { 121, 18, true, 1, null, 2, 2 },
                    { 120, 17, true, 58, null, 2, 2 },
                    { 119, 17, true, 51, null, 2, 2 },
                    { 118, 17, true, 50, null, 2, 2 },
                    { 117, 17, true, 49, null, 2, 2 },
                    { 116, 17, true, 48, null, 2, 2 },
                    { 100007, 100001, true, 54, null, 1, 100000 },
                    { 115, 17, true, 47, null, 2, 2 },
                    { 113, 17, true, 39, null, 2, 2 },
                    { 112, 17, true, 38, null, 2, 2 },
                    { 111, 17, true, 37, null, 2, 2 },
                    { 110, 17, true, 36, null, 2, 2 },
                    { 109, 17, true, 21, null, 2, 2 },
                    { 108, 17, true, 18, null, 2, 2 },
                    { 107, 17, true, 17, null, 2, 2 },
                    { 106, 17, true, 16, null, 2, 2 },
                    { 114, 17, true, 46, null, 2, 2 }
                });

            migrationBuilder.InsertData(
                schema: "Corporate",
                table: "Branch",
                columns: new[] { "BranchId", "Description", "Level", "Name", "ParentId" },
                values: new object[] { 2, "", 1, "نمایشگاه شمال تهران", 1 });

            migrationBuilder.CreateIndex(
                name: "IX_Account_BranchId",
                schema: "Finance",
                table: "Account",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Account_FiscalPeriodId",
                schema: "Finance",
                table: "Account",
                column: "FiscalPeriodId");

            migrationBuilder.CreateIndex(
                name: "IX_Account_GroupId",
                schema: "Finance",
                table: "Account",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Account_ParentId",
                schema: "Finance",
                table: "Account",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountCollection_CategoryID",
                schema: "Finance",
                table: "AccountCollection",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_AccountCollectionAccount_AccountId",
                schema: "Finance",
                table: "AccountCollectionAccount",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountCollectionAccount_BranchId",
                schema: "Finance",
                table: "AccountCollectionAccount",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountCollectionAccount_CollectionId",
                schema: "Finance",
                table: "AccountCollectionAccount",
                column: "CollectionId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountCollectionAccount_FiscalPeriodId",
                schema: "Finance",
                table: "AccountCollectionAccount",
                column: "FiscalPeriodId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountCostCenter_CostCenterId",
                schema: "Finance",
                table: "AccountCostCenter",
                column: "CostCenterId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountCurrency_AccountId",
                schema: "Finance",
                table: "AccountCurrency",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountCurrency_BranchId",
                schema: "Finance",
                table: "AccountCurrency",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountCurrency_CurrencyId",
                schema: "Finance",
                table: "AccountCurrency",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountDetailAccount_DetailAccountId",
                schema: "Finance",
                table: "AccountDetailAccount",
                column: "DetailAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountHolder_AccountOwnerId",
                schema: "Finance",
                table: "AccountHolder",
                column: "AccountOwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountOwner_AccountId",
                schema: "Finance",
                table: "AccountOwner",
                column: "AccountId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AccountProject_ProjectId",
                schema: "Finance",
                table: "AccountProject",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Attribute_BranchId",
                schema: "ProductScope",
                table: "Attribute",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Branch_ParentId",
                schema: "Corporate",
                table: "Branch",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Brand_BranchId",
                schema: "ProductScope",
                table: "Brand",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_CashRegister_BranchId",
                schema: "CashFlow",
                table: "CashRegister",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_CashRegister_FiscalPeriodId",
                schema: "CashFlow",
                table: "CashRegister",
                column: "FiscalPeriodId");

            migrationBuilder.CreateIndex(
                name: "IX_CheckBook_AccountId",
                schema: "Check",
                table: "CheckBook",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_CheckBook_BranchId",
                schema: "Check",
                table: "CheckBook",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_CheckBook_CostCenterId",
                schema: "Check",
                table: "CheckBook",
                column: "CostCenterId");

            migrationBuilder.CreateIndex(
                name: "IX_CheckBook_DetailAccountId",
                schema: "Check",
                table: "CheckBook",
                column: "DetailAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_CheckBook_FiscalPeriodId",
                schema: "Check",
                table: "CheckBook",
                column: "FiscalPeriodId");

            migrationBuilder.CreateIndex(
                name: "IX_CheckBook_ProjectId",
                schema: "Check",
                table: "CheckBook",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_CheckBookPage_CheckBookId",
                schema: "Check",
                table: "CheckBookPage",
                column: "CheckBookId");

            migrationBuilder.CreateIndex(
                name: "IX_City_ProvinceId",
                schema: "Metadata",
                table: "City",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_CostCenter_BranchId",
                schema: "Finance",
                table: "CostCenter",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_CostCenter_FiscalPeriodId",
                schema: "Finance",
                table: "CostCenter",
                column: "FiscalPeriodId");

            migrationBuilder.CreateIndex(
                name: "IX_CostCenter_ParentId",
                schema: "Finance",
                table: "CostCenter",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Currency_BranchId",
                schema: "Finance",
                table: "Currency",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Currency_FiscalPeriodId",
                schema: "Finance",
                table: "Currency",
                column: "FiscalPeriodId");

            migrationBuilder.CreateIndex(
                name: "IX_CurrencyRate_BranchId",
                schema: "Finance",
                table: "CurrencyRate",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_CurrencyRate_CurrencyId",
                schema: "Finance",
                table: "CurrencyRate",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_CurrencyRate_FiscalPeriodId",
                schema: "Finance",
                table: "CurrencyRate",
                column: "FiscalPeriodId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerTaxInfo_AccountId",
                schema: "Finance",
                table: "CustomerTaxInfo",
                column: "AccountId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DashboardTab_DashboardId",
                schema: "Reporting",
                table: "DashboardTab",
                column: "DashboardId");

            migrationBuilder.CreateIndex(
                name: "IX_DetailAccount_BranchId",
                schema: "Finance",
                table: "DetailAccount",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_DetailAccount_CurrencyId",
                schema: "Finance",
                table: "DetailAccount",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_DetailAccount_FiscalPeriodId",
                schema: "Finance",
                table: "DetailAccount",
                column: "FiscalPeriodId");

            migrationBuilder.CreateIndex(
                name: "IX_DetailAccount_ParentId",
                schema: "Finance",
                table: "DetailAccount",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_InactiveEntity_BranchId",
                schema: "Core",
                table: "InactiveEntity",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_InactiveEntity_FiscalPeriodId",
                schema: "Core",
                table: "InactiveEntity",
                column: "FiscalPeriodId");

            migrationBuilder.CreateIndex(
                name: "IX_LabelSetting_CustomFormID",
                schema: "Config",
                table: "LabelSetting",
                column: "CustomFormID");

            migrationBuilder.CreateIndex(
                name: "IX_LabelSetting_SettingID",
                schema: "Config",
                table: "LabelSetting",
                column: "SettingID");

            migrationBuilder.CreateIndex(
                name: "IX_LogSetting_EntityTypeId",
                schema: "Config",
                table: "LogSetting",
                column: "EntityTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_LogSetting_OperationId",
                schema: "Config",
                table: "LogSetting",
                column: "OperationId");

            migrationBuilder.CreateIndex(
                name: "IX_LogSetting_SourceId",
                schema: "Config",
                table: "LogSetting",
                column: "SourceId");

            migrationBuilder.CreateIndex(
                name: "IX_LogSetting_SourceTypeId",
                schema: "Config",
                table: "LogSetting",
                column: "SourceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_LogSetting_SubsystemId",
                schema: "Config",
                table: "LogSetting",
                column: "SubsystemId");

            migrationBuilder.CreateIndex(
                name: "IX_OperationLog_BranchId",
                schema: "Core",
                table: "OperationLog",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_OperationLog_EntityTypeId",
                schema: "Core",
                table: "OperationLog",
                column: "EntityTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_OperationLog_FiscalPeriodId",
                schema: "Core",
                table: "OperationLog",
                column: "FiscalPeriodId");

            migrationBuilder.CreateIndex(
                name: "IX_OperationLog_OperationId",
                schema: "Core",
                table: "OperationLog",
                column: "OperationId");

            migrationBuilder.CreateIndex(
                name: "IX_OperationLog_SourceId",
                schema: "Core",
                table: "OperationLog",
                column: "SourceId");

            migrationBuilder.CreateIndex(
                name: "IX_OperationLog_SourceListId",
                schema: "Core",
                table: "OperationLog",
                column: "SourceListId");

            migrationBuilder.CreateIndex(
                name: "IX_OperationLogArchive_BranchId",
                schema: "Core",
                table: "OperationLogArchive",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_OperationLogArchive_EntityTypeId",
                schema: "Core",
                table: "OperationLogArchive",
                column: "EntityTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_OperationLogArchive_FiscalPeriodId",
                schema: "Core",
                table: "OperationLogArchive",
                column: "FiscalPeriodId");

            migrationBuilder.CreateIndex(
                name: "IX_OperationLogArchive_OperationId",
                schema: "Core",
                table: "OperationLogArchive",
                column: "OperationId");

            migrationBuilder.CreateIndex(
                name: "IX_OperationLogArchive_SourceId",
                schema: "Core",
                table: "OperationLogArchive",
                column: "SourceId");

            migrationBuilder.CreateIndex(
                name: "IX_OperationLogArchive_SourceListId",
                schema: "Core",
                table: "OperationLogArchive",
                column: "SourceListId");

            migrationBuilder.CreateIndex(
                name: "IX_PayReceive_BranchId",
                schema: "CashFlow",
                table: "PayReceive",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_PayReceive_CurrencyId",
                schema: "CashFlow",
                table: "PayReceive",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_PayReceive_FiscalPeriodId",
                schema: "CashFlow",
                table: "PayReceive",
                column: "FiscalPeriodId");

            migrationBuilder.CreateIndex(
                name: "IX_PayReceiveAccount_AccountId",
                schema: "CashFlow",
                table: "PayReceiveAccount",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_PayReceiveAccount_CostCenterId",
                schema: "CashFlow",
                table: "PayReceiveAccount",
                column: "CostCenterId");

            migrationBuilder.CreateIndex(
                name: "IX_PayReceiveAccount_DetailAccountId",
                schema: "CashFlow",
                table: "PayReceiveAccount",
                column: "DetailAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_PayReceiveAccount_PayReceiveId",
                schema: "CashFlow",
                table: "PayReceiveAccount",
                column: "PayReceiveId");

            migrationBuilder.CreateIndex(
                name: "IX_PayReceiveAccount_ProjectId",
                schema: "CashFlow",
                table: "PayReceiveAccount",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_PayReceiveCashAccount_AccountId",
                schema: "CashFlow",
                table: "PayReceiveCashAccount",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_PayReceiveCashAccount_CostCenterId",
                schema: "CashFlow",
                table: "PayReceiveCashAccount",
                column: "CostCenterId");

            migrationBuilder.CreateIndex(
                name: "IX_PayReceiveCashAccount_DetailAccountId",
                schema: "CashFlow",
                table: "PayReceiveCashAccount",
                column: "DetailAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_PayReceiveCashAccount_PayReceiveId",
                schema: "CashFlow",
                table: "PayReceiveCashAccount",
                column: "PayReceiveId");

            migrationBuilder.CreateIndex(
                name: "IX_PayReceiveCashAccount_ProjectId",
                schema: "CashFlow",
                table: "PayReceiveCashAccount",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_PayReceiveCashAccount_SourceAppId",
                schema: "CashFlow",
                table: "PayReceiveCashAccount",
                column: "SourceAppId");

            migrationBuilder.CreateIndex(
                name: "IX_PayReceiveVoucherLine_VoucherLineId",
                schema: "CashFlow",
                table: "PayReceiveVoucherLine",
                column: "VoucherLineId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Project_BranchId",
                schema: "Finance",
                table: "Project",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Project_FiscalPeriodId",
                schema: "Finance",
                table: "Project",
                column: "FiscalPeriodId");

            migrationBuilder.CreateIndex(
                name: "IX_Project_ParentId",
                schema: "Finance",
                table: "Project",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Property_BranchId",
                schema: "ProductScope",
                table: "Property",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleBranch_BranchId",
                schema: "Auth",
                table: "RoleBranch",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleFiscalPeriod_FiscalPeriodId",
                schema: "Auth",
                table: "RoleFiscalPeriod",
                column: "FiscalPeriodId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleWidget_WidgetId",
                schema: "Auth",
                table: "RoleWidget",
                column: "WidgetId");

            migrationBuilder.CreateIndex(
                name: "IX_Setting_ParentId",
                schema: "Config",
                table: "Setting",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_SourceApp_BranchId",
                schema: "CashFlow",
                table: "SourceApp",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_SourceApp_FiscalPeriodId",
                schema: "CashFlow",
                table: "SourceApp",
                column: "FiscalPeriodId");

            migrationBuilder.CreateIndex(
                name: "IX_TabWidget_TabId",
                schema: "Reporting",
                table: "TabWidget",
                column: "TabId");

            migrationBuilder.CreateIndex(
                name: "IX_TabWidget_WidgetId",
                schema: "Reporting",
                table: "TabWidget",
                column: "WidgetId");

            migrationBuilder.CreateIndex(
                name: "IX_Unit_BranchId",
                schema: "ProductScope",
                table: "Unit",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_UsedParameter_FunctionId",
                schema: "Reporting",
                table: "UsedParameter",
                column: "FunctionId");

            migrationBuilder.CreateIndex(
                name: "IX_UsedParameter_ParameterId",
                schema: "Reporting",
                table: "UsedParameter",
                column: "ParameterId");

            migrationBuilder.CreateIndex(
                name: "IX_UserCashRegister_CashRegisterId",
                schema: "CashFlow",
                table: "UserCashRegister",
                column: "CashRegisterId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSetting_SettingId",
                schema: "Config",
                table: "UserSetting",
                column: "SettingId");

            migrationBuilder.CreateIndex(
                name: "IX_UserValue_CategoryId",
                schema: "Config",
                table: "UserValue",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Voucher_BranchId",
                schema: "Finance",
                table: "Voucher",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Voucher_FiscalPeriodId",
                schema: "Finance",
                table: "Voucher",
                column: "FiscalPeriodId");

            migrationBuilder.CreateIndex(
                name: "IX_Voucher_OriginId",
                schema: "Finance",
                table: "Voucher",
                column: "OriginId");

            migrationBuilder.CreateIndex(
                name: "IX_Voucher_StatusId",
                schema: "Finance",
                table: "Voucher",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_VoucherLine_AccountId",
                schema: "Finance",
                table: "VoucherLine",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_VoucherLine_BranchId",
                schema: "Finance",
                table: "VoucherLine",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_VoucherLine_CostCenterId",
                schema: "Finance",
                table: "VoucherLine",
                column: "CostCenterId");

            migrationBuilder.CreateIndex(
                name: "IX_VoucherLine_CurrencyId",
                schema: "Finance",
                table: "VoucherLine",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_VoucherLine_DetailAccountId",
                schema: "Finance",
                table: "VoucherLine",
                column: "DetailAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_VoucherLine_FiscalPeriodId",
                schema: "Finance",
                table: "VoucherLine",
                column: "FiscalPeriodId");

            migrationBuilder.CreateIndex(
                name: "IX_VoucherLine_ProjectId",
                schema: "Finance",
                table: "VoucherLine",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_VoucherLine_SourceAppId",
                schema: "Finance",
                table: "VoucherLine",
                column: "SourceAppId");

            migrationBuilder.CreateIndex(
                name: "IX_VoucherLine_VoucherId",
                schema: "Finance",
                table: "VoucherLine",
                column: "VoucherId");

            migrationBuilder.CreateIndex(
                name: "IX_Widget_FunctionId",
                schema: "Reporting",
                table: "Widget",
                column: "FunctionId");

            migrationBuilder.CreateIndex(
                name: "IX_Widget_TypeId",
                schema: "Reporting",
                table: "Widget",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_WidgetAccount_AccountId",
                schema: "Reporting",
                table: "WidgetAccount",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_WidgetAccount_CostCenterId",
                schema: "Reporting",
                table: "WidgetAccount",
                column: "CostCenterId");

            migrationBuilder.CreateIndex(
                name: "IX_WidgetAccount_DetailAccountId",
                schema: "Reporting",
                table: "WidgetAccount",
                column: "DetailAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_WidgetAccount_ProjectId",
                schema: "Reporting",
                table: "WidgetAccount",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_WidgetAccount_WidgetId",
                schema: "Reporting",
                table: "WidgetAccount",
                column: "WidgetId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountCollectionAccount",
                schema: "Finance");

            migrationBuilder.DropTable(
                name: "AccountCostCenter",
                schema: "Finance");

            migrationBuilder.DropTable(
                name: "AccountCurrency",
                schema: "Finance");

            migrationBuilder.DropTable(
                name: "AccountDetailAccount",
                schema: "Finance");

            migrationBuilder.DropTable(
                name: "AccountHolder",
                schema: "Finance");

            migrationBuilder.DropTable(
                name: "AccountProject",
                schema: "Finance");

            migrationBuilder.DropTable(
                name: "Attribute",
                schema: "ProductScope");

            migrationBuilder.DropTable(
                name: "Brand",
                schema: "ProductScope");

            migrationBuilder.DropTable(
                name: "CheckBookPage",
                schema: "Check");

            migrationBuilder.DropTable(
                name: "City",
                schema: "Metadata");

            migrationBuilder.DropTable(
                name: "CurrencyRate",
                schema: "Finance");

            migrationBuilder.DropTable(
                name: "CustomerTaxInfo",
                schema: "Finance");

            migrationBuilder.DropTable(
                name: "DocumentType",
                schema: "Core");

            migrationBuilder.DropTable(
                name: "File",
                schema: "ProductScope");

            migrationBuilder.DropTable(
                name: "Filter",
                schema: "Core");

            migrationBuilder.DropTable(
                name: "InactiveEntity",
                schema: "Core");

            migrationBuilder.DropTable(
                name: "LabelSetting",
                schema: "Config");

            migrationBuilder.DropTable(
                name: "LogSetting",
                schema: "Config");

            migrationBuilder.DropTable(
                name: "OperationLog",
                schema: "Core");

            migrationBuilder.DropTable(
                name: "OperationLogArchive",
                schema: "Core");

            migrationBuilder.DropTable(
                name: "PayReceiveAccount",
                schema: "CashFlow");

            migrationBuilder.DropTable(
                name: "PayReceiveCashAccount",
                schema: "CashFlow");

            migrationBuilder.DropTable(
                name: "PayReceiveVoucherLine",
                schema: "CashFlow");

            migrationBuilder.DropTable(
                name: "Property",
                schema: "ProductScope");

            migrationBuilder.DropTable(
                name: "RoleBranch",
                schema: "Auth");

            migrationBuilder.DropTable(
                name: "RoleFiscalPeriod",
                schema: "Auth");

            migrationBuilder.DropTable(
                name: "RoleWidget",
                schema: "Auth");

            migrationBuilder.DropTable(
                name: "TabWidget",
                schema: "Reporting");

            migrationBuilder.DropTable(
                name: "TaxCurrency",
                schema: "Finance");

            migrationBuilder.DropTable(
                name: "Unit",
                schema: "ProductScope");

            migrationBuilder.DropTable(
                name: "UsedParameter",
                schema: "Reporting");

            migrationBuilder.DropTable(
                name: "UserCashRegister",
                schema: "CashFlow");

            migrationBuilder.DropTable(
                name: "UserSetting",
                schema: "Config");

            migrationBuilder.DropTable(
                name: "UserValue",
                schema: "Config");

            migrationBuilder.DropTable(
                name: "Version",
                schema: "Core");

            migrationBuilder.DropTable(
                name: "ViewSetting",
                schema: "Config");

            migrationBuilder.DropTable(
                name: "WidgetAccount",
                schema: "Reporting");

            migrationBuilder.DropTable(
                name: "AccountCollection",
                schema: "Finance");

            migrationBuilder.DropTable(
                name: "AccountOwner",
                schema: "Finance");

            migrationBuilder.DropTable(
                name: "CheckBook",
                schema: "Check");

            migrationBuilder.DropTable(
                name: "Province",
                schema: "Metadata");

            migrationBuilder.DropTable(
                name: "CustomForm",
                schema: "Metadata");

            migrationBuilder.DropTable(
                name: "OperationSourceType",
                schema: "Metadata");

            migrationBuilder.DropTable(
                name: "Subsystem",
                schema: "Metadata");

            migrationBuilder.DropTable(
                name: "EntityType",
                schema: "Metadata");

            migrationBuilder.DropTable(
                name: "Operation",
                schema: "Metadata");

            migrationBuilder.DropTable(
                name: "OperationSource",
                schema: "Metadata");

            migrationBuilder.DropTable(
                name: "OperationSourceList",
                schema: "Metadata");

            migrationBuilder.DropTable(
                name: "PayReceive",
                schema: "CashFlow");

            migrationBuilder.DropTable(
                name: "VoucherLine",
                schema: "Finance");

            migrationBuilder.DropTable(
                name: "DashboardTab",
                schema: "Reporting");

            migrationBuilder.DropTable(
                name: "FunctionParameter",
                schema: "Reporting");

            migrationBuilder.DropTable(
                name: "CashRegister",
                schema: "CashFlow");

            migrationBuilder.DropTable(
                name: "Setting",
                schema: "Config");

            migrationBuilder.DropTable(
                name: "UserValueCategory",
                schema: "Config");

            migrationBuilder.DropTable(
                name: "Widget",
                schema: "Reporting");

            migrationBuilder.DropTable(
                name: "AccountCollectionCategory",
                schema: "Finance");

            migrationBuilder.DropTable(
                name: "SourceApp",
                schema: "CashFlow");

            migrationBuilder.DropTable(
                name: "Account",
                schema: "Finance");

            migrationBuilder.DropTable(
                name: "CostCenter",
                schema: "Finance");

            migrationBuilder.DropTable(
                name: "DetailAccount",
                schema: "Finance");

            migrationBuilder.DropTable(
                name: "Project",
                schema: "Finance");

            migrationBuilder.DropTable(
                name: "Voucher",
                schema: "Finance");

            migrationBuilder.DropTable(
                name: "Dashboard",
                schema: "Reporting");

            migrationBuilder.DropTable(
                name: "WidgetFunction",
                schema: "Reporting");

            migrationBuilder.DropTable(
                name: "WidgetType",
                schema: "Reporting");

            migrationBuilder.DropTable(
                name: "AccountGroup",
                schema: "Finance");

            migrationBuilder.DropTable(
                name: "Currency",
                schema: "Finance");

            migrationBuilder.DropTable(
                name: "DocumentStatus",
                schema: "Core");

            migrationBuilder.DropTable(
                name: "VoucherOrigin",
                schema: "Finance");

            migrationBuilder.DropTable(
                name: "FiscalPeriod",
                schema: "Finance");

            migrationBuilder.DropTable(
                name: "Branch",
                schema: "Corporate");
        }
    }
}
