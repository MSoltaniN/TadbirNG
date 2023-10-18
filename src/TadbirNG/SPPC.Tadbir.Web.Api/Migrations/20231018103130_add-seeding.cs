using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SPPC.Tadbir.Web.Api.Migrations
{
    public partial class addseeding : Migration
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
                    CategoryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountCollectionCategory", x => x.CategoryID);
                });

            migrationBuilder.CreateTable(
                name: "AccountGroup",
                schema: "Finance",
                columns: table => new
                {
                    GroupID = table.Column<int>(type: "int", nullable: false)
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
                    table.PrimaryKey("PK_AccountGroup", x => x.GroupID);
                });

            migrationBuilder.CreateTable(
                name: "Branch",
                schema: "Corporate",
                columns: table => new
                {
                    BranchID = table.Column<int>(type: "int", nullable: false)
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
                    table.PrimaryKey("PK_Branch", x => x.BranchID);
                    table.ForeignKey(
                        name: "FK_Branch_Branch_ParentId",
                        column: x => x.ParentId,
                        principalSchema: "Corporate",
                        principalTable: "Branch",
                        principalColumn: "BranchID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CustomForm",
                schema: "Metadata",
                columns: table => new
                {
                    CustomFormID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomForm", x => x.CustomFormID);
                });

            migrationBuilder.CreateTable(
                name: "Dashboard",
                schema: "Reporting",
                columns: table => new
                {
                    DashboardID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dashboard", x => x.DashboardID);
                });

            migrationBuilder.CreateTable(
                name: "DocumentStatus",
                schema: "Core",
                columns: table => new
                {
                    StatusID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentStatus", x => x.StatusID);
                });

            migrationBuilder.CreateTable(
                name: "DocumentType",
                schema: "Core",
                columns: table => new
                {
                    TypeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentType", x => x.TypeID);
                });

            migrationBuilder.CreateTable(
                name: "EntityType",
                schema: "Metadata",
                columns: table => new
                {
                    EntityTypeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntityType", x => x.EntityTypeID);
                });

            migrationBuilder.CreateTable(
                name: "File",
                schema: "ProductScope",
                columns: table => new
                {
                    FileID = table.Column<int>(type: "int", nullable: false)
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
                    table.PrimaryKey("PK_File", x => x.FileID);
                });

            migrationBuilder.CreateTable(
                name: "Filter",
                schema: "Core",
                columns: table => new
                {
                    FilterID = table.Column<int>(type: "int", nullable: false)
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
                    table.PrimaryKey("PK_Filter", x => x.FilterID);
                });

            migrationBuilder.CreateTable(
                name: "FiscalPeriod",
                schema: "Finance",
                columns: table => new
                {
                    FiscalPeriodID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FiscalPeriod", x => x.FiscalPeriodID);
                });

            migrationBuilder.CreateTable(
                name: "FunctionParameter",
                schema: "Reporting",
                columns: table => new
                {
                    FunctionParameterID = table.Column<int>(type: "int", nullable: false)
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
                    table.PrimaryKey("PK_FunctionParameter", x => x.FunctionParameterID);
                });

            migrationBuilder.CreateTable(
                name: "Operation",
                schema: "Metadata",
                columns: table => new
                {
                    OperationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Operation", x => x.OperationID);
                });

            migrationBuilder.CreateTable(
                name: "OperationSource",
                schema: "Metadata",
                columns: table => new
                {
                    OperationSourceID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperationSource", x => x.OperationSourceID);
                });

            migrationBuilder.CreateTable(
                name: "OperationSourceList",
                schema: "Metadata",
                columns: table => new
                {
                    OperationSourceListID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperationSourceList", x => x.OperationSourceListID);
                });

            migrationBuilder.CreateTable(
                name: "OperationSourceType",
                schema: "Metadata",
                columns: table => new
                {
                    OperationSourceTypeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperationSourceType", x => x.OperationSourceTypeID);
                });

            migrationBuilder.CreateTable(
                name: "Province",
                schema: "Metadata",
                columns: table => new
                {
                    ProvinceID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: true),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Province", x => x.ProvinceID);
                });

            migrationBuilder.CreateTable(
                name: "Setting",
                schema: "Config",
                columns: table => new
                {
                    SettingID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Subsystem = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    TitleKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Type = table.Column<short>(type: "smallint", nullable: false),
                    ScopeType = table.Column<short>(type: "smallint", nullable: false),
                    ModelType = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Values = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false),
                    DefaultValues = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false),
                    DescriptionKey = table.Column<string>(type: "nvarchar(1028)", maxLength: 1028, nullable: true),
                    IsStandalone = table.Column<bool>(type: "bit", nullable: false),
                    ParentID = table.Column<int>(type: "int", nullable: true),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Setting", x => x.SettingID);
                    table.ForeignKey(
                        name: "FK_Config_Setting_Config_Parent",
                        column: x => x.ParentID,
                        principalSchema: "Config",
                        principalTable: "Setting",
                        principalColumn: "SettingID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Subsystem",
                schema: "Metadata",
                columns: table => new
                {
                    SubsystemID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subsystem", x => x.SubsystemID);
                });

            migrationBuilder.CreateTable(
                name: "TaxCurrency",
                schema: "Finance",
                columns: table => new
                {
                    TaxCurrencyID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaxCurrency", x => x.TaxCurrencyID);
                });

            migrationBuilder.CreateTable(
                name: "UserValueCategory",
                schema: "Config",
                columns: table => new
                {
                    CategoryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameKey = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserValueCategory", x => x.CategoryID);
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
                    ViewSettingID = table.Column<int>(type: "int", nullable: false)
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
                    table.PrimaryKey("PK_ViewSetting", x => x.ViewSettingID);
                });

            migrationBuilder.CreateTable(
                name: "VoucherOrigin",
                schema: "Finance",
                columns: table => new
                {
                    OriginID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VoucherOrigin", x => x.OriginID);
                });

            migrationBuilder.CreateTable(
                name: "WidgetFunction",
                schema: "Reporting",
                columns: table => new
                {
                    WidgetFunctionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WidgetFunction", x => x.WidgetFunctionID);
                });

            migrationBuilder.CreateTable(
                name: "WidgetType",
                schema: "Reporting",
                columns: table => new
                {
                    WidgetTypeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WidgetType", x => x.WidgetTypeID);
                });

            migrationBuilder.CreateTable(
                name: "AccountCollection",
                schema: "Finance",
                columns: table => new
                {
                    CollectionID = table.Column<int>(type: "int", nullable: false)
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
                    table.PrimaryKey("PK_AccountCollection", x => x.CollectionID);
                    table.ForeignKey(
                        name: "FK_Finance_AccountCollection_Finance_Category",
                        column: x => x.CategoryID,
                        principalSchema: "Finance",
                        principalTable: "AccountCollectionCategory",
                        principalColumn: "CategoryID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Attribute",
                schema: "ProductScope",
                columns: table => new
                {
                    AttributeID = table.Column<int>(type: "int", nullable: false)
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
                    table.PrimaryKey("PK_Attribute", x => x.AttributeID);
                    table.ForeignKey(
                        name: "FK_Attribute_Branch_BranchId",
                        column: x => x.BranchId,
                        principalSchema: "Corporate",
                        principalTable: "Branch",
                        principalColumn: "BranchID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Brand",
                schema: "ProductScope",
                columns: table => new
                {
                    BrandID = table.Column<int>(type: "int", nullable: false)
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
                    table.PrimaryKey("PK_Brand", x => x.BrandID);
                    table.ForeignKey(
                        name: "FK_Brand_Branch_BranchId",
                        column: x => x.BranchId,
                        principalSchema: "Corporate",
                        principalTable: "Branch",
                        principalColumn: "BranchID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Property",
                schema: "ProductScope",
                columns: table => new
                {
                    PropertyID = table.Column<int>(type: "int", nullable: false)
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
                    table.PrimaryKey("PK_Property", x => x.PropertyID);
                    table.ForeignKey(
                        name: "FK_Property_Branch_BranchId",
                        column: x => x.BranchId,
                        principalSchema: "Corporate",
                        principalTable: "Branch",
                        principalColumn: "BranchID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RoleBranch",
                schema: "Auth",
                columns: table => new
                {
                    RoleBranchID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    BranchId = table.Column<int>(type: "int", nullable: false),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleBranch", x => x.RoleBranchID);
                    table.UniqueConstraint("AK_RoleBranch_RoleId_BranchId", x => new { x.RoleId, x.BranchId });
                    table.ForeignKey(
                        name: "FK_RoleBranch_Branch_BranchId",
                        column: x => x.BranchId,
                        principalSchema: "Corporate",
                        principalTable: "Branch",
                        principalColumn: "BranchID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Unit",
                schema: "ProductScope",
                columns: table => new
                {
                    UnitID = table.Column<int>(type: "int", nullable: false)
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
                    table.PrimaryKey("PK_Unit", x => x.UnitID);
                    table.ForeignKey(
                        name: "FK_Unit_Branch_BranchId",
                        column: x => x.BranchId,
                        principalSchema: "Corporate",
                        principalTable: "Branch",
                        principalColumn: "BranchID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DashboardTab",
                schema: "Reporting",
                columns: table => new
                {
                    DashboardTabID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DashboardId = table.Column<int>(type: "int", nullable: false),
                    Index = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DashboardTab", x => x.DashboardTabID);
                    table.ForeignKey(
                        name: "FK_Reporting_DashboardTab_Reporting_Dashboard",
                        column: x => x.DashboardId,
                        principalSchema: "Reporting",
                        principalTable: "Dashboard",
                        principalColumn: "DashboardID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Account",
                schema: "Finance",
                columns: table => new
                {
                    AccountID = table.Column<int>(type: "int", nullable: false)
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
                    table.PrimaryKey("PK_Account", x => x.AccountID);
                    table.ForeignKey(
                        name: "FK_Finance_Account_Corporate_Branch",
                        column: x => x.BranchId,
                        principalSchema: "Corporate",
                        principalTable: "Branch",
                        principalColumn: "BranchID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Finance_Account_Finance_FiscalPeriod",
                        column: x => x.FiscalPeriodId,
                        principalSchema: "Finance",
                        principalTable: "FiscalPeriod",
                        principalColumn: "FiscalPeriodID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Finance_Account_Finance_Group",
                        column: x => x.GroupId,
                        principalSchema: "Finance",
                        principalTable: "AccountGroup",
                        principalColumn: "GroupID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Finance_Account_Finance_Parent",
                        column: x => x.ParentId,
                        principalSchema: "Finance",
                        principalTable: "Account",
                        principalColumn: "AccountID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CashRegister",
                schema: "CashFlow",
                columns: table => new
                {
                    CashRegisterID = table.Column<int>(type: "int", nullable: false)
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
                    table.PrimaryKey("PK_CashRegister", x => x.CashRegisterID);
                    table.ForeignKey(
                        name: "FK_CashFlow_CashRegister_Corporate_Branch",
                        column: x => x.BranchId,
                        principalSchema: "Corporate",
                        principalTable: "Branch",
                        principalColumn: "BranchID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CashFlow_CashRegister_Finance_FiscalPeriod",
                        column: x => x.FiscalPeriodId,
                        principalSchema: "Finance",
                        principalTable: "FiscalPeriod",
                        principalColumn: "FiscalPeriodID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CostCenter",
                schema: "Finance",
                columns: table => new
                {
                    CostCenterID = table.Column<int>(type: "int", nullable: false)
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
                    table.PrimaryKey("PK_CostCenter", x => x.CostCenterID);
                    table.ForeignKey(
                        name: "FK_Finance_CostCenter_Corporate_Branch",
                        column: x => x.BranchId,
                        principalSchema: "Corporate",
                        principalTable: "Branch",
                        principalColumn: "BranchID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Finance_CostCenter_Finance_FiscalPeriod",
                        column: x => x.FiscalPeriodId,
                        principalSchema: "Finance",
                        principalTable: "FiscalPeriod",
                        principalColumn: "FiscalPeriodID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Finance_CostCenter_Finance_Parent",
                        column: x => x.ParentId,
                        principalSchema: "Finance",
                        principalTable: "CostCenter",
                        principalColumn: "CostCenterID",
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
                        principalColumn: "FiscalPeriodID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Finance_Currency_Corporate_Branch",
                        column: x => x.BranchId,
                        principalSchema: "Corporate",
                        principalTable: "Branch",
                        principalColumn: "BranchID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InactiveEntity",
                schema: "Core",
                columns: table => new
                {
                    InactiveEntityID = table.Column<int>(type: "int", nullable: false)
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
                    table.PrimaryKey("PK_InactiveEntity", x => x.InactiveEntityID);
                    table.ForeignKey(
                        name: "FK_Core_InactiveEntity_Corporate_Branch",
                        column: x => x.BranchId,
                        principalSchema: "Corporate",
                        principalTable: "Branch",
                        principalColumn: "BranchID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Core_InactiveEntity_Finance_FiscalPeriod",
                        column: x => x.FiscalPeriodId,
                        principalSchema: "Finance",
                        principalTable: "FiscalPeriod",
                        principalColumn: "FiscalPeriodID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Project",
                schema: "Finance",
                columns: table => new
                {
                    ProjectID = table.Column<int>(type: "int", nullable: false)
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
                    table.PrimaryKey("PK_Project", x => x.ProjectID);
                    table.ForeignKey(
                        name: "FK_Finance_Project_Corporate_Branch",
                        column: x => x.BranchId,
                        principalSchema: "Corporate",
                        principalTable: "Branch",
                        principalColumn: "BranchID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Finance_Project_Finance_FiscalPeriod",
                        column: x => x.FiscalPeriodId,
                        principalSchema: "Finance",
                        principalTable: "FiscalPeriod",
                        principalColumn: "FiscalPeriodID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Finance_Project_Finance_Parent",
                        column: x => x.ParentId,
                        principalSchema: "Finance",
                        principalTable: "Project",
                        principalColumn: "ProjectID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RoleFiscalPeriod",
                schema: "Auth",
                columns: table => new
                {
                    RoleFiscalPeriodID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    FiscalPeriodId = table.Column<int>(type: "int", nullable: false),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleFiscalPeriod", x => x.RoleFiscalPeriodID);
                    table.UniqueConstraint("AK_RoleFiscalPeriod_RoleId_FiscalPeriodId", x => new { x.RoleId, x.FiscalPeriodId });
                    table.ForeignKey(
                        name: "FK_RoleFiscalPeriod_FiscalPeriod_FiscalPeriodId",
                        column: x => x.FiscalPeriodId,
                        principalSchema: "Finance",
                        principalTable: "FiscalPeriod",
                        principalColumn: "FiscalPeriodID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SourceApp",
                schema: "CashFlow",
                columns: table => new
                {
                    SourceAppID = table.Column<int>(type: "int", nullable: false)
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
                    table.PrimaryKey("PK_SourceApp", x => x.SourceAppID);
                    table.ForeignKey(
                        name: "FK_CashFlow_SourceApp_Corporate_Branch",
                        column: x => x.BranchId,
                        principalSchema: "Corporate",
                        principalTable: "Branch",
                        principalColumn: "BranchID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CashFlow_SourceApp_Finance_FiscalPeriod",
                        column: x => x.FiscalPeriodId,
                        principalSchema: "Finance",
                        principalTable: "FiscalPeriod",
                        principalColumn: "FiscalPeriodID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OperationLog",
                schema: "Core",
                columns: table => new
                {
                    OperationLogID = table.Column<int>(type: "int", nullable: false)
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
                    table.PrimaryKey("PK_OperationLog", x => x.OperationLogID);
                    table.ForeignKey(
                        name: "FK_Core_OperationLog_Corporate_Branch",
                        column: x => x.BranchId,
                        principalSchema: "Corporate",
                        principalTable: "Branch",
                        principalColumn: "BranchID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Core_OperationLog_Finance_FiscalPeriod",
                        column: x => x.FiscalPeriodId,
                        principalSchema: "Finance",
                        principalTable: "FiscalPeriod",
                        principalColumn: "FiscalPeriodID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Core_OperationLog_Metadata_EntityType",
                        column: x => x.EntityTypeId,
                        principalSchema: "Metadata",
                        principalTable: "EntityType",
                        principalColumn: "EntityTypeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Core_OperationLog_Metadata_Operation",
                        column: x => x.OperationId,
                        principalSchema: "Metadata",
                        principalTable: "Operation",
                        principalColumn: "OperationID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Core_OperationLog_Metadata_Source",
                        column: x => x.SourceId,
                        principalSchema: "Metadata",
                        principalTable: "OperationSource",
                        principalColumn: "OperationSourceID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Core_OperationLog_Metadata_SourceList",
                        column: x => x.SourceListId,
                        principalSchema: "Metadata",
                        principalTable: "OperationSourceList",
                        principalColumn: "OperationSourceListID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OperationLogArchive",
                schema: "Core",
                columns: table => new
                {
                    OperationLogArchiveID = table.Column<int>(type: "int", nullable: false)
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
                    table.PrimaryKey("PK_OperationLogArchive", x => x.OperationLogArchiveID);
                    table.ForeignKey(
                        name: "FK_Core_OperationLogArchive_Corporate_Branch",
                        column: x => x.BranchId,
                        principalSchema: "Corporate",
                        principalTable: "Branch",
                        principalColumn: "BranchID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Core_OperationLogArchive_Finance_FiscalPeriod",
                        column: x => x.FiscalPeriodId,
                        principalSchema: "Finance",
                        principalTable: "FiscalPeriod",
                        principalColumn: "FiscalPeriodID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Core_OperationLogArchive_Metadata_EntityType",
                        column: x => x.EntityTypeId,
                        principalSchema: "Metadata",
                        principalTable: "EntityType",
                        principalColumn: "EntityTypeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Core_OperationLogArchive_Metadata_Operation",
                        column: x => x.OperationId,
                        principalSchema: "Metadata",
                        principalTable: "Operation",
                        principalColumn: "OperationID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Core_OperationLogArchive_Metadata_Source",
                        column: x => x.SourceId,
                        principalSchema: "Metadata",
                        principalTable: "OperationSource",
                        principalColumn: "OperationSourceID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Core_OperationLogArchive_Metadata_SourceList",
                        column: x => x.SourceListId,
                        principalSchema: "Metadata",
                        principalTable: "OperationSourceList",
                        principalColumn: "OperationSourceListID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "City",
                schema: "Metadata",
                columns: table => new
                {
                    CityID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProvinceId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: true),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_City", x => x.CityID);
                    table.ForeignKey(
                        name: "FK_Metadata_City_Metadata_Province",
                        column: x => x.ProvinceId,
                        principalSchema: "Metadata",
                        principalTable: "Province",
                        principalColumn: "ProvinceID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LabelSetting",
                schema: "Config",
                columns: table => new
                {
                    LabelSettingID = table.Column<int>(type: "int", nullable: false)
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
                    table.PrimaryKey("PK_LabelSetting", x => x.LabelSettingID);
                    table.ForeignKey(
                        name: "FK_Config_LabelSetting_Config_Setting",
                        column: x => x.SettingID,
                        principalSchema: "Config",
                        principalTable: "Setting",
                        principalColumn: "SettingID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Config_LabelSetting_Metadata_CustomForm",
                        column: x => x.CustomFormID,
                        principalSchema: "Metadata",
                        principalTable: "CustomForm",
                        principalColumn: "CustomFormID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserSetting",
                schema: "Config",
                columns: table => new
                {
                    UserSettingID = table.Column<int>(type: "int", nullable: false)
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
                    table.PrimaryKey("PK_UserSetting", x => x.UserSettingID);
                    table.ForeignKey(
                        name: "FK_Config_UserSetting_Config_Setting",
                        column: x => x.SettingId,
                        principalSchema: "Config",
                        principalTable: "Setting",
                        principalColumn: "SettingID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LogSetting",
                schema: "Config",
                columns: table => new
                {
                    LogSettingID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubsystemID = table.Column<int>(type: "int", nullable: false),
                    SourceTypeID = table.Column<int>(type: "int", nullable: false),
                    SourceID = table.Column<int>(type: "int", nullable: true),
                    EntityTypeID = table.Column<int>(type: "int", nullable: true),
                    OperationID = table.Column<int>(type: "int", nullable: false),
                    IsEnabled = table.Column<bool>(type: "bit", nullable: false),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogSetting", x => x.LogSettingID);
                    table.ForeignKey(
                        name: "FK_Config_LogSetting_Metadata_EntityType",
                        column: x => x.EntityTypeID,
                        principalSchema: "Metadata",
                        principalTable: "EntityType",
                        principalColumn: "EntityTypeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Config_LogSetting_Metadata_Operation",
                        column: x => x.OperationID,
                        principalSchema: "Metadata",
                        principalTable: "Operation",
                        principalColumn: "OperationID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Config_LogSetting_Metadata_Source",
                        column: x => x.SourceID,
                        principalSchema: "Metadata",
                        principalTable: "OperationSource",
                        principalColumn: "OperationSourceID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Config_LogSetting_Metadata_SourceType",
                        column: x => x.SourceTypeID,
                        principalSchema: "Metadata",
                        principalTable: "OperationSourceType",
                        principalColumn: "OperationSourceTypeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Config_LogSetting_Metadata_Subsystem",
                        column: x => x.SubsystemID,
                        principalSchema: "Metadata",
                        principalTable: "Subsystem",
                        principalColumn: "SubsystemID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserValue",
                schema: "Config",
                columns: table => new
                {
                    ValueID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserValue", x => x.ValueID);
                    table.ForeignKey(
                        name: "FK_Config_UserValue_Config_Category",
                        column: x => x.CategoryId,
                        principalSchema: "Config",
                        principalTable: "UserValueCategory",
                        principalColumn: "CategoryID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Voucher",
                schema: "Finance",
                columns: table => new
                {
                    VoucherID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StatusID = table.Column<int>(type: "int", nullable: false),
                    OriginId = table.Column<int>(type: "int", nullable: false),
                    No = table.Column<int>(type: "int", nullable: false),
                    DailyNo = table.Column<int>(type: "int", nullable: false),
                    Association = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    IsBalanced = table.Column<bool>(type: "bit", nullable: false),
                    Type = table.Column<short>(type: "smallint", nullable: false),
                    SubjectType = table.Column<short>(type: "smallint", nullable: false),
                    SaveCount = table.Column<int>(type: "int", nullable: false),
                    IssuedByID = table.Column<int>(type: "int", nullable: false),
                    ModifiedByID = table.Column<int>(type: "int", nullable: false),
                    ConfirmedByID = table.Column<int>(type: "int", nullable: true),
                    ApprovedByID = table.Column<int>(type: "int", nullable: true),
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
                    table.PrimaryKey("PK_Voucher", x => x.VoucherID);
                    table.ForeignKey(
                        name: "FK_Finance_Voucher_Core_Status",
                        column: x => x.StatusID,
                        principalSchema: "Core",
                        principalTable: "DocumentStatus",
                        principalColumn: "StatusID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Finance_Voucher_Corporate_Branch",
                        column: x => x.BranchId,
                        principalSchema: "Corporate",
                        principalTable: "Branch",
                        principalColumn: "BranchID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Finance_Voucher_Finance_FiscalPeriod",
                        column: x => x.FiscalPeriodId,
                        principalSchema: "Finance",
                        principalTable: "FiscalPeriod",
                        principalColumn: "FiscalPeriodID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Finance_Voucher_Finance_VoucherOrigin",
                        column: x => x.OriginId,
                        principalSchema: "Finance",
                        principalTable: "VoucherOrigin",
                        principalColumn: "OriginID",
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
                        principalColumn: "WidgetFunctionID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reporting_UsedParameter_Reporting_Parameter",
                        column: x => x.ParameterId,
                        principalSchema: "Reporting",
                        principalTable: "FunctionParameter",
                        principalColumn: "FunctionParameterID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Widget",
                schema: "Reporting",
                columns: table => new
                {
                    WidgetID = table.Column<int>(type: "int", nullable: false)
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
                    table.PrimaryKey("PK_Widget", x => x.WidgetID);
                    table.ForeignKey(
                        name: "FK_Reporting_Widget_Reporting_Function",
                        column: x => x.FunctionId,
                        principalSchema: "Reporting",
                        principalTable: "WidgetFunction",
                        principalColumn: "WidgetFunctionID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reporting_Widget_Reporting_Type",
                        column: x => x.TypeId,
                        principalSchema: "Reporting",
                        principalTable: "WidgetType",
                        principalColumn: "WidgetTypeID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AccountCollectionAccount",
                schema: "Finance",
                columns: table => new
                {
                    AccountCollectionAccountID = table.Column<int>(type: "int", nullable: false)
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
                    table.PrimaryKey("PK_AccountCollectionAccount", x => x.AccountCollectionAccountID);
                    table.ForeignKey(
                        name: "FK_Finance_AccountCollectionAccount_Finance_Account",
                        column: x => x.AccountId,
                        principalSchema: "Finance",
                        principalTable: "Account",
                        principalColumn: "AccountID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Finance_AccountCollectionAccount_Finance_Branch",
                        column: x => x.BranchId,
                        principalSchema: "Corporate",
                        principalTable: "Branch",
                        principalColumn: "BranchID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Finance_AccountCollectionAccount_Finance_Collection",
                        column: x => x.CollectionId,
                        principalSchema: "Finance",
                        principalTable: "AccountCollection",
                        principalColumn: "CollectionID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Finance_AccountCollectionAccount_Finance_FiscalPeriod",
                        column: x => x.FiscalPeriodId,
                        principalSchema: "Finance",
                        principalTable: "FiscalPeriod",
                        principalColumn: "FiscalPeriodID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AccountOwner",
                schema: "Finance",
                columns: table => new
                {
                    AccountOwnerID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountID = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_AccountOwner", x => x.AccountOwnerID);
                    table.ForeignKey(
                        name: "FK_Finance_AccountOwner_Finance_Account",
                        column: x => x.AccountID,
                        principalSchema: "Finance",
                        principalTable: "Account",
                        principalColumn: "AccountID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CustomerTaxInfo",
                schema: "Finance",
                columns: table => new
                {
                    CustomerTaxInfoID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountID = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_CustomerTaxInfo", x => x.CustomerTaxInfoID);
                    table.ForeignKey(
                        name: "FK_Finance_CustomerTaxInfo_Finance_Account",
                        column: x => x.AccountID,
                        principalSchema: "Finance",
                        principalTable: "Account",
                        principalColumn: "AccountID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserCashRegister",
                schema: "CashFlow",
                columns: table => new
                {
                    UserCashRegisterID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CashRegisterId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCashRegister", x => x.UserCashRegisterID);
                    table.ForeignKey(
                        name: "FK_CashFlow_UserCashRegister_CashFlow_CashRegister",
                        column: x => x.CashRegisterId,
                        principalSchema: "CashFlow",
                        principalTable: "CashRegister",
                        principalColumn: "CashRegisterID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AccountCostCenter",
                schema: "Finance",
                columns: table => new
                {
                    AccountCostCenterID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountId = table.Column<int>(type: "int", nullable: false),
                    CostCenterId = table.Column<int>(type: "int", nullable: false),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountCostCenter", x => x.AccountCostCenterID);
                    table.UniqueConstraint("AK_AccountCostCenter_AccountId_CostCenterId", x => new { x.AccountId, x.CostCenterId });
                    table.ForeignKey(
                        name: "FK_AccountCostCenter_Account_AccountId",
                        column: x => x.AccountId,
                        principalSchema: "Finance",
                        principalTable: "Account",
                        principalColumn: "AccountID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccountCostCenter_CostCenter_CostCenterId",
                        column: x => x.CostCenterId,
                        principalSchema: "Finance",
                        principalTable: "CostCenter",
                        principalColumn: "CostCenterID",
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
                        principalColumn: "AccountID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccountCurrency_Branch_BranchId",
                        column: x => x.BranchId,
                        principalSchema: "Corporate",
                        principalTable: "Branch",
                        principalColumn: "BranchID",
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
                    CurrencyRateID = table.Column<int>(type: "int", nullable: false)
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
                    table.PrimaryKey("PK_CurrencyRate", x => x.CurrencyRateID);
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
                        principalColumn: "FiscalPeriodID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Finance_CurrencyRate_Corporate_Branch",
                        column: x => x.BranchId,
                        principalSchema: "Corporate",
                        principalTable: "Branch",
                        principalColumn: "BranchID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DetailAccount",
                schema: "Finance",
                columns: table => new
                {
                    DetailAccountID = table.Column<int>(type: "int", nullable: false)
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
                    table.PrimaryKey("PK_DetailAccount", x => x.DetailAccountID);
                    table.ForeignKey(
                        name: "FK_Finance_DetailAccount_Corporate_Branch",
                        column: x => x.BranchId,
                        principalSchema: "Corporate",
                        principalTable: "Branch",
                        principalColumn: "BranchID",
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
                        principalColumn: "FiscalPeriodID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Finance_DetailAccount_Finance_Parent",
                        column: x => x.ParentId,
                        principalSchema: "Finance",
                        principalTable: "DetailAccount",
                        principalColumn: "DetailAccountID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PayReceive",
                schema: "CashFlow",
                columns: table => new
                {
                    PayReceiveID = table.Column<int>(type: "int", nullable: false)
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
                    table.PrimaryKey("PK_PayReceive", x => x.PayReceiveID);
                    table.ForeignKey(
                        name: "FK_CashFlow_PayReceive_Corporate_Branch",
                        column: x => x.BranchId,
                        principalSchema: "Corporate",
                        principalTable: "Branch",
                        principalColumn: "BranchID",
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
                        principalColumn: "FiscalPeriodID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AccountProject",
                schema: "Finance",
                columns: table => new
                {
                    AccountProjectID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountId = table.Column<int>(type: "int", nullable: false),
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountProject", x => x.AccountProjectID);
                    table.UniqueConstraint("AK_AccountProject_AccountId_ProjectId", x => new { x.AccountId, x.ProjectId });
                    table.ForeignKey(
                        name: "FK_AccountProject_Account_AccountId",
                        column: x => x.AccountId,
                        principalSchema: "Finance",
                        principalTable: "Account",
                        principalColumn: "AccountID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccountProject_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalSchema: "Finance",
                        principalTable: "Project",
                        principalColumn: "ProjectID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoleWidget",
                schema: "Auth",
                columns: table => new
                {
                    RoleWidgetID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WidgetId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleWidget", x => x.RoleWidgetID);
                    table.ForeignKey(
                        name: "FK_Auth_RoleWidget_Reporting_Widget",
                        column: x => x.WidgetId,
                        principalSchema: "Reporting",
                        principalTable: "Widget",
                        principalColumn: "WidgetID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TabWidget",
                schema: "Reporting",
                columns: table => new
                {
                    TabWidgetID = table.Column<int>(type: "int", nullable: false)
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
                    table.PrimaryKey("PK_TabWidget", x => x.TabWidgetID);
                    table.ForeignKey(
                        name: "FK_Reporting_TabWidget_Reporting_DashboardTab",
                        column: x => x.TabId,
                        principalSchema: "Reporting",
                        principalTable: "DashboardTab",
                        principalColumn: "DashboardTabID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reporting_TabWidget_Reporting_Widget",
                        column: x => x.WidgetId,
                        principalSchema: "Reporting",
                        principalTable: "Widget",
                        principalColumn: "WidgetID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AccountHolder",
                schema: "Finance",
                columns: table => new
                {
                    AccountHolderID = table.Column<int>(type: "int", nullable: false)
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
                    table.PrimaryKey("PK_AccountHolder", x => x.AccountHolderID);
                    table.ForeignKey(
                        name: "FK_AccountHolder_AccountOwner_AccountOwnerId",
                        column: x => x.AccountOwnerId,
                        principalSchema: "Finance",
                        principalTable: "AccountOwner",
                        principalColumn: "AccountOwnerID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AccountDetailAccount",
                schema: "Finance",
                columns: table => new
                {
                    AccountDetailAccountID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountId = table.Column<int>(type: "int", nullable: false),
                    DetailAccountId = table.Column<int>(type: "int", nullable: false),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountDetailAccount", x => x.AccountDetailAccountID);
                    table.UniqueConstraint("AK_AccountDetailAccount_AccountId_DetailAccountId", x => new { x.AccountId, x.DetailAccountId });
                    table.ForeignKey(
                        name: "FK_AccountDetailAccount_Account_AccountId",
                        column: x => x.AccountId,
                        principalSchema: "Finance",
                        principalTable: "Account",
                        principalColumn: "AccountID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccountDetailAccount_DetailAccount_DetailAccountId",
                        column: x => x.DetailAccountId,
                        principalSchema: "Finance",
                        principalTable: "DetailAccount",
                        principalColumn: "DetailAccountID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CheckBook",
                schema: "Check",
                columns: table => new
                {
                    CheckBookID = table.Column<int>(type: "int", nullable: false)
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
                    table.PrimaryKey("PK_CheckBook", x => x.CheckBookID);
                    table.ForeignKey(
                        name: "FK_Check_CheckBook_Corporate_Branch",
                        column: x => x.BranchId,
                        principalSchema: "Corporate",
                        principalTable: "Branch",
                        principalColumn: "BranchID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Check_CheckBook_Finance_Account",
                        column: x => x.AccountId,
                        principalSchema: "Finance",
                        principalTable: "Account",
                        principalColumn: "AccountID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Check_CheckBook_Finance_CostCenter",
                        column: x => x.CostCenterId,
                        principalSchema: "Finance",
                        principalTable: "CostCenter",
                        principalColumn: "CostCenterID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Check_CheckBook_Finance_DetailAccount",
                        column: x => x.DetailAccountId,
                        principalSchema: "Finance",
                        principalTable: "DetailAccount",
                        principalColumn: "DetailAccountID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Check_CheckBook_Finance_Project",
                        column: x => x.ProjectId,
                        principalSchema: "Finance",
                        principalTable: "Project",
                        principalColumn: "ProjectID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CheckBook_FiscalPeriod_FiscalPeriodId",
                        column: x => x.FiscalPeriodId,
                        principalSchema: "Finance",
                        principalTable: "FiscalPeriod",
                        principalColumn: "FiscalPeriodID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VoucherLine",
                schema: "Finance",
                columns: table => new
                {
                    VoucherLineID = table.Column<int>(type: "int", nullable: false)
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
                    table.PrimaryKey("PK_VoucherLine", x => x.VoucherLineID);
                    table.ForeignKey(
                        name: "FK_Finance_VoucherLine_CashFlow_SourceApp",
                        column: x => x.SourceAppId,
                        principalSchema: "CashFlow",
                        principalTable: "SourceApp",
                        principalColumn: "SourceAppID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Finance_VoucherLine_Corporate_Branch",
                        column: x => x.BranchId,
                        principalSchema: "Corporate",
                        principalTable: "Branch",
                        principalColumn: "BranchID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Finance_VoucherLine_Finance_Account",
                        column: x => x.AccountId,
                        principalSchema: "Finance",
                        principalTable: "Account",
                        principalColumn: "AccountID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Finance_VoucherLine_Finance_CostCenter",
                        column: x => x.CostCenterId,
                        principalSchema: "Finance",
                        principalTable: "CostCenter",
                        principalColumn: "CostCenterID",
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
                        principalColumn: "DetailAccountID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Finance_VoucherLine_Finance_FiscalPeriod",
                        column: x => x.FiscalPeriodId,
                        principalSchema: "Finance",
                        principalTable: "FiscalPeriod",
                        principalColumn: "FiscalPeriodID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Finance_VoucherLine_Finance_Project",
                        column: x => x.ProjectId,
                        principalSchema: "Finance",
                        principalTable: "Project",
                        principalColumn: "ProjectID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Finance_VoucherLine_Finance_Voucher",
                        column: x => x.VoucherId,
                        principalSchema: "Finance",
                        principalTable: "Voucher",
                        principalColumn: "VoucherID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WidgetAccount",
                schema: "Reporting",
                columns: table => new
                {
                    WidgetAccountID = table.Column<int>(type: "int", nullable: false)
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
                    table.PrimaryKey("PK_WidgetAccount", x => x.WidgetAccountID);
                    table.ForeignKey(
                        name: "FK_Reporting_WidgetAccount_Finance_Account",
                        column: x => x.AccountId,
                        principalSchema: "Finance",
                        principalTable: "Account",
                        principalColumn: "AccountID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reporting_WidgetAccount_Finance_CostCenter",
                        column: x => x.CostCenterId,
                        principalSchema: "Finance",
                        principalTable: "CostCenter",
                        principalColumn: "CostCenterID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reporting_WidgetAccount_Finance_DetailAccount",
                        column: x => x.DetailAccountId,
                        principalSchema: "Finance",
                        principalTable: "DetailAccount",
                        principalColumn: "DetailAccountID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reporting_WidgetAccount_Finance_Project",
                        column: x => x.ProjectId,
                        principalSchema: "Finance",
                        principalTable: "Project",
                        principalColumn: "ProjectID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reporting_WidgetAccount_Reporting_Widget",
                        column: x => x.WidgetId,
                        principalSchema: "Reporting",
                        principalTable: "Widget",
                        principalColumn: "WidgetID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PayReceiveAccount",
                schema: "CashFlow",
                columns: table => new
                {
                    PayReceiveAccountID = table.Column<int>(type: "int", nullable: false)
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
                    table.PrimaryKey("PK_PayReceiveAccount", x => x.PayReceiveAccountID);
                    table.ForeignKey(
                        name: "FK_CashFlow_PayReceiveAccount_CashFlow_PayReceive",
                        column: x => x.PayReceiveId,
                        principalSchema: "CashFlow",
                        principalTable: "PayReceive",
                        principalColumn: "PayReceiveID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CashFlow_PayReceiveAccount_Finance_Account",
                        column: x => x.AccountId,
                        principalSchema: "Finance",
                        principalTable: "Account",
                        principalColumn: "AccountID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CashFlow_PayReceiveAccount_Finance_CostCenter",
                        column: x => x.CostCenterId,
                        principalSchema: "Finance",
                        principalTable: "CostCenter",
                        principalColumn: "CostCenterID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CashFlow_PayReceiveAccount_Finance_DetailAccount",
                        column: x => x.DetailAccountId,
                        principalSchema: "Finance",
                        principalTable: "DetailAccount",
                        principalColumn: "DetailAccountID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CashFlow_PayReceiveAccount_Finance_Project",
                        column: x => x.ProjectId,
                        principalSchema: "Finance",
                        principalTable: "Project",
                        principalColumn: "ProjectID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PayReceiveCashAccount",
                schema: "CashFlow",
                columns: table => new
                {
                    PayReceiveCashAccountID = table.Column<int>(type: "int", nullable: false)
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
                    table.PrimaryKey("PK_PayReceiveCashAccount", x => x.PayReceiveCashAccountID);
                    table.ForeignKey(
                        name: "FK_CashFlow_PayReceiveCashAccount_CashFlow_PayReceive",
                        column: x => x.PayReceiveId,
                        principalSchema: "CashFlow",
                        principalTable: "PayReceive",
                        principalColumn: "PayReceiveID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CashFlow_PayReceiveCashAccount_CashFlow_SourceApp",
                        column: x => x.SourceAppId,
                        principalSchema: "CashFlow",
                        principalTable: "SourceApp",
                        principalColumn: "SourceAppID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CashFlow_PayReceiveCashAccount_Finance_Account",
                        column: x => x.AccountId,
                        principalSchema: "Finance",
                        principalTable: "Account",
                        principalColumn: "AccountID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CashFlow_PayReceiveCashAccount_Finance_CostCenter",
                        column: x => x.CostCenterId,
                        principalSchema: "Finance",
                        principalTable: "CostCenter",
                        principalColumn: "CostCenterID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CashFlow_PayReceiveCashAccount_Finance_DetailAccount",
                        column: x => x.DetailAccountId,
                        principalSchema: "Finance",
                        principalTable: "DetailAccount",
                        principalColumn: "DetailAccountID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CashFlow_PayReceiveCashAccount_Finance_Project",
                        column: x => x.ProjectId,
                        principalSchema: "Finance",
                        principalTable: "Project",
                        principalColumn: "ProjectID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CheckBookPage",
                schema: "Check",
                columns: table => new
                {
                    CheckBookPageID = table.Column<int>(type: "int", nullable: false)
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
                    table.PrimaryKey("PK_CheckBookPage", x => x.CheckBookPageID);
                    table.ForeignKey(
                        name: "FK_Check_CheckBookPage_Check_CheckBook",
                        column: x => x.CheckBookId,
                        principalSchema: "Check",
                        principalTable: "CheckBook",
                        principalColumn: "CheckBookID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PayReceiveVoucherLine",
                schema: "CashFlow",
                columns: table => new
                {
                    PayReceiveVoucherLineID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PayReceiveId = table.Column<int>(type: "int", nullable: false),
                    VoucherLineId = table.Column<int>(type: "int", nullable: false),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PayReceiveVoucherLine", x => x.PayReceiveVoucherLineID);
                    table.UniqueConstraint("AK_PayReceiveVoucherLine_PayReceiveId_VoucherLineId", x => new { x.PayReceiveId, x.VoucherLineId });
                    table.ForeignKey(
                        name: "FK_PayReceiveVoucherLine_PayReceive_PayReceiveId",
                        column: x => x.PayReceiveId,
                        principalSchema: "CashFlow",
                        principalTable: "PayReceive",
                        principalColumn: "PayReceiveID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PayReceiveVoucherLine_VoucherLine_VoucherLineId",
                        column: x => x.VoucherLineId,
                        principalSchema: "Finance",
                        principalTable: "VoucherLine",
                        principalColumn: "VoucherLineID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "Core",
                table: "Version",
                columns: new[] { "VersionId", "ModifiedDate", "Number", "rowguid" },
                values: new object[] { 1, new DateTime(2022, 8, 27, 13, 56, 52, 150, DateTimeKind.Unspecified), "2.2.0", new Guid("26452115-8352-42fe-a7b8-4bd3d32f50f6") });

            migrationBuilder.InsertData(
                schema: "Metadata",
                table: "EntityType",
                columns: new[] { "EntityTypeID", "Description", "ModifiedDate", "Name" },
                values: new object[,]
                {
                    { 100004, "", new DateTime(2023, 10, 18, 14, 1, 28, 151, DateTimeKind.Local).AddTicks(2198), "Attribute" },
                    { 100003, "", new DateTime(2023, 10, 18, 14, 1, 28, 151, DateTimeKind.Local).AddTicks(2185), "Property" },
                    { 100002, "", new DateTime(2023, 10, 18, 14, 1, 28, 151, DateTimeKind.Local).AddTicks(2177), "Unit" },
                    { 100001, "", new DateTime(2023, 10, 18, 14, 1, 28, 151, DateTimeKind.Local).AddTicks(2167), "Brand" },
                    { 25, "", new DateTime(2023, 10, 18, 14, 1, 28, 151, DateTimeKind.Local).AddTicks(2158), "Payment" },
                    { 24, "", new DateTime(2023, 10, 18, 14, 1, 28, 151, DateTimeKind.Local).AddTicks(2149), "Receipt" },
                    { 23, "", new DateTime(2023, 10, 18, 14, 1, 28, 151, DateTimeKind.Local).AddTicks(2139), "SourceApp" },
                    { 22, "", new DateTime(2023, 10, 18, 14, 1, 28, 151, DateTimeKind.Local).AddTicks(2128), "CashRegister" },
                    { 21, "", new DateTime(2023, 10, 18, 14, 1, 28, 151, DateTimeKind.Local).AddTicks(2119), "CheckBook" },
                    { 20, "", new DateTime(2023, 10, 18, 14, 1, 28, 151, DateTimeKind.Local).AddTicks(2111), "Widget" },
                    { 19, "", new DateTime(2023, 10, 18, 14, 1, 28, 151, DateTimeKind.Local).AddTicks(2102), "DashboardTab" },
                    { 1, "", new DateTime(2023, 10, 18, 14, 1, 28, 151, DateTimeKind.Local).AddTicks(1010), "Account" },
                    { 17, "", new DateTime(2023, 10, 18, 14, 1, 28, 151, DateTimeKind.Local).AddTicks(2083), "Voucher" },
                    { 18, "", new DateTime(2023, 10, 18, 14, 1, 28, 151, DateTimeKind.Local).AddTicks(2095), "DraftVoucher" },
                    { 2, "", new DateTime(2023, 10, 18, 14, 1, 28, 151, DateTimeKind.Local).AddTicks(1719), "AccountCollectionAccount" },
                    { 5, "", new DateTime(2023, 10, 18, 14, 1, 28, 151, DateTimeKind.Local).AddTicks(1773), "Branch" },
                    { 6, "", new DateTime(2023, 10, 18, 14, 1, 28, 151, DateTimeKind.Local).AddTicks(1792), "CostCenter" },
                    { 7, "", new DateTime(2023, 10, 18, 14, 1, 28, 151, DateTimeKind.Local).AddTicks(1954), "Currency" },
                    { 4, "", new DateTime(2023, 10, 18, 14, 1, 28, 151, DateTimeKind.Local).AddTicks(1752), "AccountGroup" },
                    { 10, "", new DateTime(2023, 10, 18, 14, 1, 28, 151, DateTimeKind.Local).AddTicks(1998), "FiscalPeriod" },
                    { 11, "", new DateTime(2023, 10, 18, 14, 1, 28, 151, DateTimeKind.Local).AddTicks(2018), "OperationLog" },
                    { 12, "", new DateTime(2023, 10, 18, 14, 1, 28, 151, DateTimeKind.Local).AddTicks(2043), "Project" },
                    { 15, "", new DateTime(2023, 10, 18, 14, 1, 28, 151, DateTimeKind.Local).AddTicks(2062), "Setting" },
                    { 9, "", new DateTime(2023, 10, 18, 14, 1, 28, 151, DateTimeKind.Local).AddTicks(1977), "DetailAccount" }
                });

            migrationBuilder.InsertData(
                schema: "Metadata",
                table: "Operation",
                columns: new[] { "OperationID", "Description", "ModifiedDate", "Name" },
                values: new object[,]
                {
                    { 65, "", new DateTime(2023, 10, 18, 14, 1, 28, 166, DateTimeKind.Local).AddTicks(1364), "DisconnectFromCheck" },
                    { 71, "", new DateTime(2023, 10, 18, 14, 1, 28, 166, DateTimeKind.Local).AddTicks(1478), "CreateAccountLine" },
                    { 70, "", new DateTime(2023, 10, 18, 14, 1, 28, 166, DateTimeKind.Local).AddTicks(1459), "AggregateAccountLines" },
                    { 69, "", new DateTime(2023, 10, 18, 14, 1, 28, 166, DateTimeKind.Local).AddTicks(1440), "RemoveInvalidAccountLines" },
                    { 68, "", new DateTime(2023, 10, 18, 14, 1, 28, 166, DateTimeKind.Local).AddTicks(1420), "Register" },
                    { 67, "", new DateTime(2023, 10, 18, 14, 1, 28, 166, DateTimeKind.Local).AddTicks(1401), "UndoArchive" },
                    { 66, "", new DateTime(2023, 10, 18, 14, 1, 28, 166, DateTimeKind.Local).AddTicks(1382), "AssignCashRegisterUser" },
                    { 64, "", new DateTime(2023, 10, 18, 14, 1, 28, 166, DateTimeKind.Local).AddTicks(1345), "ConnectToCheck" },
                    { 56, "", new DateTime(2023, 10, 18, 14, 1, 28, 166, DateTimeKind.Local).AddTicks(1114), "FilterRates" },
                    { 62, "", new DateTime(2023, 10, 18, 14, 1, 28, 166, DateTimeKind.Local).AddTicks(1305), "CancelPage" },
                    { 61, "", new DateTime(2023, 10, 18, 14, 1, 28, 166, DateTimeKind.Local).AddTicks(1281), "DeletePages" },
                    { 60, "", new DateTime(2023, 10, 18, 14, 1, 28, 166, DateTimeKind.Local).AddTicks(1152), "CreatePages" },
                    { 58, "", new DateTime(2023, 10, 18, 14, 1, 28, 166, DateTimeKind.Local).AddTicks(1133), "PrintPreview" },
                    { 72, "", new DateTime(2023, 10, 18, 14, 1, 28, 166, DateTimeKind.Local).AddTicks(1500), "EditAccountLine" },
                    { 55, "", new DateTime(2023, 10, 18, 14, 1, 28, 166, DateTimeKind.Local).AddTicks(1095), "ExportRates" },
                    { 53, "", new DateTime(2023, 10, 18, 14, 1, 28, 166, DateTimeKind.Local).AddTicks(1056), "GroupNormalize" },
                    { 63, "", new DateTime(2023, 10, 18, 14, 1, 28, 166, DateTimeKind.Local).AddTicks(1326), "UndoCancelPage" }
                });

            migrationBuilder.InsertData(
                schema: "Metadata",
                table: "Operation",
                columns: new[] { "OperationID", "Description", "ModifiedDate", "Name" },
                values: new object[,]
                {
                    { 73, "", new DateTime(2023, 10, 18, 14, 1, 28, 166, DateTimeKind.Local).AddTicks(1587), "DeleteAccountLine" },
                    { 79, "", new DateTime(2023, 10, 18, 14, 1, 28, 166, DateTimeKind.Local).AddTicks(1728), "CreateCashAccountLine" },
                    { 75, "", new DateTime(2023, 10, 18, 14, 1, 28, 166, DateTimeKind.Local).AddTicks(1629), "PrintAccountLines" },
                    { 93, "", new DateTime(2023, 10, 18, 14, 1, 28, 166, DateTimeKind.Local).AddTicks(1961), "UndoRegister" },
                    { 92, "", new DateTime(2023, 10, 18, 14, 1, 28, 166, DateTimeKind.Local).AddTicks(1942), "PrintPreviewForm" },
                    { 91, "", new DateTime(2023, 10, 18, 14, 1, 28, 166, DateTimeKind.Local).AddTicks(1923), "PrintForm" },
                    { 90, "", new DateTime(2023, 10, 18, 14, 1, 28, 166, DateTimeKind.Local).AddTicks(1903), "Reactivate" },
                    { 89, "", new DateTime(2023, 10, 18, 14, 1, 28, 166, DateTimeKind.Local).AddTicks(1883), "Deactivate" },
                    { 88, "", new DateTime(2023, 10, 18, 14, 1, 28, 166, DateTimeKind.Local).AddTicks(1863), "AggregateCashAccountLines" },
                    { 87, "", new DateTime(2023, 10, 18, 14, 1, 28, 166, DateTimeKind.Local).AddTicks(1844), "RemoveInvalidCashAccountLines" },
                    { 86, "", new DateTime(2023, 10, 18, 14, 1, 28, 166, DateTimeKind.Local).AddTicks(1825), "ExportCashAccountLines" },
                    { 74, "", new DateTime(2023, 10, 18, 14, 1, 28, 166, DateTimeKind.Local).AddTicks(1609), "GroupDeleteAccountLines" },
                    { 85, "", new DateTime(2023, 10, 18, 14, 1, 28, 166, DateTimeKind.Local).AddTicks(1806), "FilterCashAccountLines" },
                    { 83, "", new DateTime(2023, 10, 18, 14, 1, 28, 166, DateTimeKind.Local).AddTicks(1767), "PrintCashAccountLines" },
                    { 82, "", new DateTime(2023, 10, 18, 14, 1, 28, 166, DateTimeKind.Local).AddTicks(1752), "GroupDeleteCashAccountLines" },
                    { 81, "", new DateTime(2023, 10, 18, 14, 1, 28, 166, DateTimeKind.Local).AddTicks(1744), "DeleteCashAccountLine" },
                    { 80, "", new DateTime(2023, 10, 18, 14, 1, 28, 166, DateTimeKind.Local).AddTicks(1736), "EditCashAccountLine" },
                    { 52, "", new DateTime(2023, 10, 18, 14, 1, 28, 166, DateTimeKind.Local).AddTicks(1035), "Normalize" },
                    { 78, "", new DateTime(2023, 10, 18, 14, 1, 28, 166, DateTimeKind.Local).AddTicks(1711), "ExportAccountLines" },
                    { 77, "", new DateTime(2023, 10, 18, 14, 1, 28, 166, DateTimeKind.Local).AddTicks(1691), "FilterAccountLines" },
                    { 76, "", new DateTime(2023, 10, 18, 14, 1, 28, 166, DateTimeKind.Local).AddTicks(1670), "PrintPreviewAccountLines" },
                    { 84, "", new DateTime(2023, 10, 18, 14, 1, 28, 166, DateTimeKind.Local).AddTicks(1786), "PrintPreviewCashAccountLines" },
                    { 51, "", new DateTime(2023, 10, 18, 14, 1, 28, 166, DateTimeKind.Local).AddTicks(1016), "GroupUndoConfirm" },
                    { 54, "", new DateTime(2023, 10, 18, 14, 1, 28, 166, DateTimeKind.Local).AddTicks(1076), "Export" },
                    { 49, "", new DateTime(2023, 10, 18, 14, 1, 28, 166, DateTimeKind.Local).AddTicks(978), "GroupUndoFinalize" },
                    { 18, "", new DateTime(2023, 10, 18, 14, 1, 28, 166, DateTimeKind.Local).AddTicks(556), "UndoFinalize" },
                    { 17, "", new DateTime(2023, 10, 18, 14, 1, 28, 166, DateTimeKind.Local).AddTicks(527), "Finalize" },
                    { 16, "", new DateTime(2023, 10, 18, 14, 1, 28, 166, DateTimeKind.Local).AddTicks(508), "UndoApprove" },
                    { 15, "", new DateTime(2023, 10, 18, 14, 1, 28, 166, DateTimeKind.Local).AddTicks(489), "Approve" },
                    { 14, "", new DateTime(2023, 10, 18, 14, 1, 28, 166, DateTimeKind.Local).AddTicks(470), "UndoConfirm" },
                    { 13, "", new DateTime(2023, 10, 18, 14, 1, 28, 166, DateTimeKind.Local).AddTicks(451), "Confirm" },
                    { 12, "", new DateTime(2023, 10, 18, 14, 1, 28, 166, DateTimeKind.Local).AddTicks(432), "UndoCheck" },
                    { 50, "", new DateTime(2023, 10, 18, 14, 1, 28, 166, DateTimeKind.Local).AddTicks(997), "GroupConfirm" },
                    { 19, "", new DateTime(2023, 10, 18, 14, 1, 28, 166, DateTimeKind.Local).AddTicks(575), "Mark" },
                    { 10, "", new DateTime(2023, 10, 18, 14, 1, 28, 166, DateTimeKind.Local).AddTicks(387), "Design" },
                    { 8, "", new DateTime(2023, 10, 18, 14, 1, 28, 166, DateTimeKind.Local).AddTicks(188), "Archive" },
                    { 7, "", new DateTime(2023, 10, 18, 14, 1, 28, 166, DateTimeKind.Local).AddTicks(179), "Save" },
                    { 6, "", new DateTime(2023, 10, 18, 14, 1, 28, 166, DateTimeKind.Local).AddTicks(169), "Print" },
                    { 5, "", new DateTime(2023, 10, 18, 14, 1, 28, 166, DateTimeKind.Local).AddTicks(147), "Filter" },
                    { 4, "", new DateTime(2023, 10, 18, 14, 1, 28, 166, DateTimeKind.Local).AddTicks(138), "Delete" },
                    { 3, "", new DateTime(2023, 10, 18, 14, 1, 28, 166, DateTimeKind.Local).AddTicks(127), "Edit" },
                    { 2, "", new DateTime(2023, 10, 18, 14, 1, 28, 166, DateTimeKind.Local).AddTicks(87), "Create" }
                });

            migrationBuilder.InsertData(
                schema: "Metadata",
                table: "Operation",
                columns: new[] { "OperationID", "Description", "ModifiedDate", "Name" },
                values: new object[,]
                {
                    { 1, "", new DateTime(2023, 10, 18, 14, 1, 28, 165, DateTimeKind.Local).AddTicks(9456), "View" },
                    { 9, "", new DateTime(2023, 10, 18, 14, 1, 28, 166, DateTimeKind.Local).AddTicks(197), "SetDefault" },
                    { 20, "", new DateTime(2023, 10, 18, 14, 1, 28, 166, DateTimeKind.Local).AddTicks(594), "QuickReportDesign" },
                    { 11, "", new DateTime(2023, 10, 18, 14, 1, 28, 166, DateTimeKind.Local).AddTicks(412), "Check" },
                    { 30, "", new DateTime(2023, 10, 18, 14, 1, 28, 166, DateTimeKind.Local).AddTicks(632), "ViewArchive" },
                    { 21, "", new DateTime(2023, 10, 18, 14, 1, 28, 166, DateTimeKind.Local).AddTicks(613), "GroupDelete" },
                    { 47, "", new DateTime(2023, 10, 18, 14, 1, 28, 166, DateTimeKind.Local).AddTicks(940), "GroupUndoCheck" },
                    { 46, "", new DateTime(2023, 10, 18, 14, 1, 28, 166, DateTimeKind.Local).AddTicks(921), "GroupCheck" },
                    { 45, "", new DateTime(2023, 10, 18, 14, 1, 28, 166, DateTimeKind.Local).AddTicks(901), "ViewRates" },
                    { 44, "", new DateTime(2023, 10, 18, 14, 1, 28, 166, DateTimeKind.Local).AddTicks(881), "GroupDeleteRates" },
                    { 43, "", new DateTime(2023, 10, 18, 14, 1, 28, 166, DateTimeKind.Local).AddTicks(862), "PrintRates" },
                    { 42, "", new DateTime(2023, 10, 18, 14, 1, 28, 166, DateTimeKind.Local).AddTicks(843), "DeleteRate" },
                    { 41, "", new DateTime(2023, 10, 18, 14, 1, 28, 166, DateTimeKind.Local).AddTicks(818), "EditRate" },
                    { 40, "", new DateTime(2023, 10, 18, 14, 1, 28, 166, DateTimeKind.Local).AddTicks(811), "CreateRate" },
                    { 48, "", new DateTime(2023, 10, 18, 14, 1, 28, 166, DateTimeKind.Local).AddTicks(959), "GroupFinalize" },
                    { 38, "", new DateTime(2023, 10, 18, 14, 1, 28, 166, DateTimeKind.Local).AddTicks(786), "DeleteLine" },
                    { 37, "", new DateTime(2023, 10, 18, 14, 1, 28, 166, DateTimeKind.Local).AddTicks(767), "EditLine" },
                    { 36, "", new DateTime(2023, 10, 18, 14, 1, 28, 166, DateTimeKind.Local).AddTicks(748), "CreateLine" },
                    { 35, "", new DateTime(2023, 10, 18, 14, 1, 28, 166, DateTimeKind.Local).AddTicks(727), "RoleAccess" },
                    { 34, "", new DateTime(2023, 10, 18, 14, 1, 28, 166, DateTimeKind.Local).AddTicks(708), "DefaultCodingChange" },
                    { 33, "", new DateTime(2023, 10, 18, 14, 1, 28, 166, DateTimeKind.Local).AddTicks(688), "DecimalCountChange" },
                    { 32, "", new DateTime(2023, 10, 18, 14, 1, 28, 166, DateTimeKind.Local).AddTicks(670), "CurrencyChange" },
                    { 31, "", new DateTime(2023, 10, 18, 14, 1, 28, 166, DateTimeKind.Local).AddTicks(651), "CalendarChange" },
                    { 39, "", new DateTime(2023, 10, 18, 14, 1, 28, 166, DateTimeKind.Local).AddTicks(803), "GroupDeleteLines" }
                });

            migrationBuilder.InsertData(
                schema: "Metadata",
                table: "OperationSource",
                columns: new[] { "OperationSourceID", "Description", "ModifiedDate", "Name" },
                values: new object[,]
                {
                    { 15, "", new DateTime(2023, 10, 18, 14, 1, 28, 142, DateTimeKind.Local).AddTicks(6848), "CheckBookReport" },
                    { 13, "", new DateTime(2023, 10, 18, 14, 1, 28, 142, DateTimeKind.Local).AddTicks(6839), "SystemIssue" },
                    { 12, "", new DateTime(2023, 10, 18, 14, 1, 28, 142, DateTimeKind.Local).AddTicks(6829), "BalanceSheet" },
                    { 11, "", new DateTime(2023, 10, 18, 14, 1, 28, 142, DateTimeKind.Local).AddTicks(6818), "AccountRelations" },
                    { 10, "", new DateTime(2023, 10, 18, 14, 1, 28, 142, DateTimeKind.Local).AddTicks(6808), "ProfitLoss" },
                    { 6, "", new DateTime(2023, 10, 18, 14, 1, 28, 142, DateTimeKind.Local).AddTicks(6789), "BalanceByAccount" },
                    { 5, "", new DateTime(2023, 10, 18, 14, 1, 28, 142, DateTimeKind.Local).AddTicks(6772), "ItemBalance" },
                    { 4, "", new DateTime(2023, 10, 18, 14, 1, 28, 142, DateTimeKind.Local).AddTicks(6762), "TestBalance" },
                    { 3, "", new DateTime(2023, 10, 18, 14, 1, 28, 142, DateTimeKind.Local).AddTicks(6751), "CurrencyBook" },
                    { 2, "", new DateTime(2023, 10, 18, 14, 1, 28, 142, DateTimeKind.Local).AddTicks(6730), "AccountBook" },
                    { 9, "", new DateTime(2023, 10, 18, 14, 1, 28, 142, DateTimeKind.Local).AddTicks(6799), "EnvironmentParams" },
                    { 1, "", new DateTime(2023, 10, 18, 14, 1, 28, 142, DateTimeKind.Local).AddTicks(6296), "Journal" }
                });

            migrationBuilder.InsertData(
                schema: "Metadata",
                table: "OperationSourceType",
                columns: new[] { "OperationSourceTypeID", "ModifiedDate", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 10, 18, 14, 1, 28, 137, DateTimeKind.Local).AddTicks(9730), "BaseData" },
                    { 2, new DateTime(2023, 10, 18, 14, 1, 28, 138, DateTimeKind.Local).AddTicks(96), "OperationalForms" },
                    { 3, new DateTime(2023, 10, 18, 14, 1, 28, 138, DateTimeKind.Local).AddTicks(119), "Reports" }
                });

            migrationBuilder.InsertData(
                schema: "Metadata",
                table: "Subsystem",
                columns: new[] { "SubsystemID", "ModifiedDate", "Name" },
                values: new object[,]
                {
                    { 100000, new DateTime(2023, 10, 18, 14, 1, 28, 132, DateTimeKind.Local).AddTicks(5467), "ProductScope" },
                    { 1, new DateTime(2023, 10, 18, 14, 1, 28, 128, DateTimeKind.Local).AddTicks(546), "Administration" },
                    { 2, new DateTime(2023, 10, 18, 14, 1, 28, 132, DateTimeKind.Local).AddTicks(5228), "Accounting" }
                });

            migrationBuilder.InsertData(
                schema: "Metadata",
                table: "Subsystem",
                columns: new[] { "SubsystemID", "ModifiedDate", "Name" },
                values: new object[] { 3, new DateTime(2023, 10, 18, 14, 1, 28, 132, DateTimeKind.Local).AddTicks(5455), "Treasury" });

            migrationBuilder.InsertData(
                schema: "Config",
                table: "LogSetting",
                columns: new[] { "LogSettingID", "EntityTypeID", "IsEnabled", "ModifiedDate", "OperationID", "rowguid", "SourceID", "SourceTypeID", "SubsystemID" },
                values: new object[,]
                {
                    { 1, 1, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(3045), 1, new Guid("9930a73c-b6b3-46bc-b9fa-8e815101518d"), null, 1, 2 },
                    { 207, 21, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(8709), 58, new Guid("19c2813a-a0a2-45ba-a4f3-3928f71d5df3"), null, 2, 3 },
                    { 206, 21, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(8699), 6, new Guid("4e773cc2-3e6f-46fe-a68c-3eb9a613d80b"), null, 2, 3 },
                    { 205, 21, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(8692), 5, new Guid("190db794-bd90-4136-8de3-8f75f6504b66"), null, 2, 3 },
                    { 204, 21, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(8685), 4, new Guid("c46a7779-dcdd-4605-88c5-9ca28b9e1787"), null, 2, 3 },
                    { 203, 21, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(8678), 3, new Guid("2f1e51c4-9f96-4554-980a-bc4e1eeacc79"), null, 2, 3 },
                    { 202, 21, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(8671), 2, new Guid("b5a35753-5055-4948-b3f5-4212f8b76d5e"), null, 2, 3 },
                    { 201, 21, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(8664), 1, new Guid("9ef9d850-586b-4342-a60c-fec0ee31d847"), null, 2, 3 },
                    { 326, 11, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(9855), 21, new Guid("c63c0094-6ccd-441e-bc45-bd1f59e88f75"), null, 2, 2 },
                    { 323, 18, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(9832), 92, new Guid("0756eb2d-85c3-4392-a54f-6e609a5698f1"), null, 2, 2 },
                    { 322, 18, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(9824), 91, new Guid("97c34c8b-300c-4d26-875d-3d04b93b5e42"), null, 2, 2 },
                    { 321, 18, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(9816), 54, new Guid("ef8dadd9-4c36-4470-bbfa-d8f19d5a17e9"), null, 2, 2 },
                    { 320, 18, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(9809), 5, new Guid("1ef2e9ec-0cb6-4831-a4ce-4af7b506662f"), null, 2, 2 },
                    { 319, 17, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(9802), 92, new Guid("b77083b0-3121-4e6d-ba4a-901d7ae61d96"), null, 2, 2 },
                    { 318, 17, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(9791), 91, new Guid("c8efca76-efd3-4560-94df-28aeaa900021"), null, 2, 2 },
                    { 317, 17, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(9783), 54, new Guid("efd2a9c0-d43c-4824-8c20-2a04c1a927cb"), null, 2, 2 },
                    { 316, 17, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(9776), 5, new Guid("90be47bd-8cd6-4f9e-9d37-02c72d81765c"), null, 2, 2 },
                    { 311, 12, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(9737), 90, new Guid("b5432105-9244-476d-a9fe-ef0a31e3209b"), null, 1, 2 },
                    { 208, 21, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(8718), 60, new Guid("137aa1a1-6ede-4eb8-8a45-ac608ea31c4d"), null, 2, 3 },
                    { 310, 12, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(9726), 89, new Guid("c43c5786-f383-4098-9dcb-104a2a3c170b"), null, 1, 2 },
                    { 209, 21, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(8725), 61, new Guid("3cf0a290-ddbc-4e78-bc71-cdce8b2b57cb"), null, 2, 3 },
                    { 211, 21, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(8739), 63, new Guid("d2e7d1ce-a87a-45ca-ab70-073bd06a7549"), null, 2, 3 },
                    { 230, null, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(8932), 67, new Guid("0a8614cb-5900-4012-b12b-7409942a76a9"), 15, 3, 3 },
                    { 229, null, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(8925), 58, new Guid("ff141496-9515-4d99-bb8f-6b49209de3cb"), 15, 3, 3 },
                    { 228, null, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(8918), 54, new Guid("7669c4dc-bfea-4979-a30e-cb9dbd5f320d"), 15, 3, 3 },
                    { 227, null, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(8911), 8, new Guid("e4cb5a2f-020c-4bd5-80f0-e872a5800ed8"), 15, 3, 3 },
                    { 226, null, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(8904), 6, new Guid("d12eb46a-a533-4a2a-9456-bacbcfd7f4eb"), 15, 3, 3 },
                    { 225, null, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(8897), 5, new Guid("2ab0b256-07ff-4283-b7c0-9030eb7efb56"), 15, 3, 3 },
                    { 224, null, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(8890), 1, new Guid("85f84981-7fb3-4c4d-8a19-d399a9ddfa86"), 15, 3, 3 },
                    { 223, 22, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(8883), 66, new Guid("7f190b3e-ff55-45e5-a54a-a63ef65f97d8"), null, 1, 3 },
                    { 222, 22, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(8872), 58, new Guid("2ee2eb21-7290-475f-8758-a1a9c14c0288"), null, 1, 3 },
                    { 221, 22, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(8865), 54, new Guid("6f2bd086-c644-4c2f-b4bc-032934b0b214"), null, 1, 3 },
                    { 220, 22, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(8858), 21, new Guid("5f22cd2e-adba-47e9-8e9f-ab3e7be3f1b2"), null, 1, 3 },
                    { 219, 22, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(8799), 6, new Guid("e2d64ced-9ab1-4f70-9542-7d7a997841ee"), null, 1, 3 },
                    { 218, 22, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(8791), 5, new Guid("2fd7d6f2-ea4d-4cf6-b69e-3f8a35316cac"), null, 1, 3 },
                    { 217, 22, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(8784), 4, new Guid("1f8a1656-3acc-4fba-bafe-3f7000eb5e31"), null, 1, 3 },
                    { 216, 22, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(8777), 3, new Guid("2c1994d5-3248-4b4a-9b1b-30059ca191f4"), null, 1, 3 },
                    { 213, 21, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(8753), 65, new Guid("5aa395b0-be33-415c-a3b6-f649f4cb51d8"), null, 2, 3 },
                    { 212, 21, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(8746), 64, new Guid("dc3d2a86-0484-4c0c-82f9-fb1dee4a402c"), null, 2, 3 },
                    { 210, 21, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(8732), 62, new Guid("a966df51-4142-4778-8b5c-48d8aa511d0d"), null, 2, 3 },
                    { 231, 23, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(8943), 1, new Guid("eb8d1370-8e52-4cd4-a0b1-6f8440b972e5"), null, 1, 3 },
                    { 309, 9, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(9718), 90, new Guid("dbc620ac-4594-4ecd-8429-5303b98f17c6"), null, 1, 2 }
                });

            migrationBuilder.InsertData(
                schema: "Config",
                table: "LogSetting",
                columns: new[] { "LogSettingID", "EntityTypeID", "IsEnabled", "ModifiedDate", "OperationID", "rowguid", "SourceID", "SourceTypeID", "SubsystemID" },
                values: new object[,]
                {
                    { 307, 7, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(9653), 90, new Guid("9f86e6f8-0369-4dc1-9af9-bced67555da6"), null, 1, 2 },
                    { 186, null, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(8543), 6, new Guid("1689f1b3-23cb-4c50-915f-671d40a508a9"), 10, 3, 2 },
                    { 185, null, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(8536), 5, new Guid("af68c532-2e92-488b-85b6-ebf59e5824e8"), 10, 3, 2 },
                    { 184, null, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(8529), 1, new Guid("bd6dbe8c-19a5-4fc6-8bb0-c48c0bac624d"), 10, 3, 2 },
                    { 183, null, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(8470), 34, new Guid("78b967ac-2378-4134-a43d-1a920e5aa746"), 9, 1, 2 },
                    { 182, null, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(8460), 33, new Guid("a2996d10-5e07-41ac-a0ad-fe57bb6ad4b3"), 9, 1, 2 },
                    { 181, null, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(8453), 32, new Guid("83e259f4-fa42-4d27-963d-d95d96661f0c"), 9, 1, 2 },
                    { 180, null, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(8446), 58, new Guid("f9bd65e4-584f-4f07-a699-d56c9dcfb22c"), 6, 3, 2 },
                    { 179, null, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(8439), 54, new Guid("b72a08af-af68-4f36-88d3-fcad7f4bc377"), 6, 3, 2 },
                    { 178, null, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(8432), 6, new Guid("93c48756-274b-4fc6-a865-83b401688c4a"), 6, 3, 2 },
                    { 177, null, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(8424), 5, new Guid("f55afc4b-559c-407a-aeae-0260cbdb0973"), 6, 3, 2 },
                    { 176, null, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(8417), 1, new Guid("787212ef-65ff-432e-823d-c21aa6902dff"), 6, 3, 2 },
                    { 175, null, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(8410), 58, new Guid("8206a360-fa1f-48ff-adf6-912871c9e7e3"), 5, 3, 2 },
                    { 174, null, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(8400), 54, new Guid("8ae90875-af66-4ee5-8959-d825af9f3a88"), 5, 3, 2 },
                    { 173, null, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(8393), 6, new Guid("ca2facf3-0740-4838-8efc-b12d464c6648"), 5, 3, 2 },
                    { 172, null, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(8386), 5, new Guid("b9ca7229-f0cb-40b4-b74b-381694e368ad"), 5, 3, 2 },
                    { 171, null, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(8379), 1, new Guid("b7e3c543-5f2e-4a22-aa05-fd72199404cb"), 5, 3, 2 },
                    { 170, null, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(8372), 58, new Guid("a8db5564-e7e8-4a86-b7df-c479528499d8"), 4, 3, 2 },
                    { 187, null, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(8550), 54, new Guid("b829dc5e-7976-435a-b9c2-48ea913c74aa"), 10, 3, 2 },
                    { 308, 9, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(9711), 89, new Guid("72b3748f-0ce8-458d-9538-3f94a910b2e4"), null, 1, 2 },
                    { 188, null, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(8558), 58, new Guid("d1c12587-e5e9-4686-9e52-2b78b860c8b8"), 10, 3, 2 },
                    { 190, null, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(8575), 7, new Guid("c43db7c5-829a-4a4c-81ca-9bd10b7f9fc6"), 11, 1, 2 },
                    { 306, 7, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(9645), 89, new Guid("08464626-8811-437f-b23e-2b0c0268b1e4"), null, 1, 2 },
                    { 305, 6, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(9638), 90, new Guid("193b7d78-4a3f-400f-ab39-96b56a59cce1"), null, 1, 2 },
                    { 304, 6, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(9630), 89, new Guid("9b1e7f95-fd3c-424f-8c8e-1029990f9eb7"), null, 1, 2 },
                    { 303, 1, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(9623), 90, new Guid("ef7946e1-4ee7-485c-aa69-26daae794d7b"), null, 1, 2 },
                    { 302, 1, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(9612), 89, new Guid("7a183be5-5c09-46c7-a923-d6ea6ebacf8f"), null, 1, 2 },
                    { 215, 22, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(8770), 2, new Guid("bf877622-ae92-4751-bb36-f5aebbd115d8"), null, 1, 2 },
                    { 214, 22, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(8760), 1, new Guid("1fb3d5c6-b2a6-4c91-8406-ece9af39f520"), null, 1, 2 },
                    { 200, null, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(8658), 58, new Guid("e931681c-4355-47cf-8fda-5c37abb141d1"), 13, 3, 2 },
                    { 199, null, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(8650), 54, new Guid("a73e8da0-9dda-4d1b-8199-b16fc658994b"), 13, 3, 2 },
                    { 198, null, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(8639), 6, new Guid("cb22b31f-4994-4bfb-a6eb-35fe42931585"), 13, 3, 2 },
                    { 197, null, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(8631), 5, new Guid("c59cf025-cb88-4845-afaf-bedf3e59c433"), 13, 3, 2 },
                    { 196, null, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(8624), 1, new Guid("b275c74f-45df-45d5-9daf-8317aae065c4"), 13, 3, 2 },
                    { 195, null, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(8616), 58, new Guid("b26f663e-119d-48ab-b179-f7073e39234e"), 12, 3, 2 },
                    { 194, null, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(8609), 54, new Guid("95980844-5b62-45fd-a96d-c2affd2049f8"), 12, 3, 2 },
                    { 193, null, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(8602), 6, new Guid("08038f63-e211-4843-b6fc-281e641478d7"), 12, 3, 2 },
                    { 192, null, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(8594), 5, new Guid("b05e7a74-02c8-4d14-be64-92b5319fe3ca"), 12, 3, 2 },
                    { 191, null, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(8587), 1, new Guid("ab970ddd-b3cc-4299-8521-4f8f20061089"), 12, 3, 2 },
                    { 189, null, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(8568), 1, new Guid("7c0c389e-9fa1-46f0-b6db-3c946533730f"), 11, 1, 2 },
                    { 169, null, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(8365), 54, new Guid("c667ec3c-7646-4253-8533-5fa5ea83a261"), 4, 3, 2 },
                    { 232, 23, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(8951), 2, new Guid("68a2ab7e-ad29-433a-9841-cfd5d8c12722"), null, 1, 3 }
                });

            migrationBuilder.InsertData(
                schema: "Config",
                table: "LogSetting",
                columns: new[] { "LogSettingID", "EntityTypeID", "IsEnabled", "ModifiedDate", "OperationID", "rowguid", "SourceID", "SourceTypeID", "SubsystemID" },
                values: new object[,]
                {
                    { 234, 23, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(8965), 4, new Guid("d5781dfe-0c96-4b33-a89e-a136978004fc"), null, 1, 3 },
                    { 293, 25, false, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(9541), 82, new Guid("567eac87-02c9-405f-a594-c31223324a92"), null, 2, 3 },
                    { 292, 25, false, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(9534), 81, new Guid("884c45d8-93e7-447e-986c-f1be3c09c861"), null, 2, 3 },
                    { 291, 25, false, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(9528), 80, new Guid("ca75e715-5437-4862-b919-8482776f7c72"), null, 2, 3 },
                    { 290, 25, false, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(9520), 79, new Guid("4bcfb9ff-2a75-4f3c-b2a5-191c9ea790a8"), null, 2, 3 },
                    { 289, 24, false, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(9514), 86, new Guid("cd70b89c-491c-409e-a459-aa137b5636da"), null, 2, 3 },
                    { 288, 24, false, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(9506), 85, new Guid("2f67aabe-fed3-49f1-8a37-c6ab1c8c5900"), null, 2, 3 },
                    { 287, 24, false, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(9499), 84, new Guid("3ec07ba6-0aa8-4c46-8baf-2c6946d0ccef"), null, 2, 3 },
                    { 286, 24, false, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(9488), 83, new Guid("5e53c1e5-d9ad-46df-ab44-e76842c7ef96"), null, 2, 3 },
                    { 285, 24, false, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(9481), 82, new Guid("aaa376ad-31c8-4cf4-9fa2-11bbac77dfa4"), null, 2, 3 },
                    { 284, 24, false, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(9471), 81, new Guid("99c86aa5-c014-4386-ba84-71908f204117"), null, 2, 3 },
                    { 283, 24, false, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(9465), 80, new Guid("2e4533fd-8ccf-4f64-bbc3-cdb776d23b89"), null, 2, 3 },
                    { 282, 24, false, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(9457), 79, new Guid("839bb059-a2c6-4856-950b-e1d7aceb3f2f"), null, 2, 3 },
                    { 281, 25, false, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(9450), 78, new Guid("891816d0-ab7d-475b-8e38-580173fec8de"), null, 2, 3 },
                    { 280, 25, false, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(9443), 77, new Guid("8182ae83-dc14-4d10-aed8-bbdc9c495c93"), null, 2, 3 },
                    { 279, 25, false, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(9436), 76, new Guid("6c08991a-3402-4787-92b1-c759988480a2"), null, 2, 3 },
                    { 278, 25, false, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(9426), 75, new Guid("dd11b7b6-e74b-482d-b4cb-112f96b43925"), null, 2, 3 },
                    { 277, 24, false, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(9418), 78, new Guid("4d74f728-f453-415d-80da-67ce20fc166d"), null, 2, 3 },
                    { 294, 25, false, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(9549), 83, new Guid("9a4e3971-8d2f-423c-9c42-7fac2ab98dcc"), null, 2, 3 },
                    { 276, 24, false, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(9411), 77, new Guid("54028563-7f53-400d-b107-3827fa26c21b"), null, 2, 3 },
                    { 295, 25, false, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(9559), 84, new Guid("a4f8e7e7-5981-4571-8865-b384b2ed16ea"), null, 2, 3 },
                    { 297, 25, false, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(9575), 86, new Guid("38e3ee72-f09f-4d14-b4ee-f64fedbf1a27"), null, 2, 3 },
                    { 100006, 100001, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(9916), 21, new Guid("5e9524e7-ce29-4def-8093-193729be4e8e"), null, 1, 100000 },
                    { 100005, 100001, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(9909), 6, new Guid("2b62e702-562d-444f-85e2-ac39f0d1529d"), null, 1, 100000 },
                    { 100004, 100001, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(9895), 5, new Guid("696db735-742e-4adb-a3f8-cd8c786ade7f"), null, 1, 100000 },
                    { 100003, 100001, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(9888), 4, new Guid("f0ffc34d-9801-46c9-ad8d-af8036777abc"), null, 1, 100000 },
                    { 100002, 100001, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(9881), 3, new Guid("ebf59526-994b-4886-b612-2ba2ce9aca54"), null, 1, 100000 },
                    { 100001, 100001, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(9873), 2, new Guid("ee11b015-a1ae-4165-acca-339ee31bce04"), null, 1, 100000 },
                    { 100000, 100001, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(9867), 1, new Guid("139890de-4b45-4ce4-afca-3a6c142b2175"), null, 1, 100000 },
                    { 325, 25, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(9847), 93, new Guid("04fd004f-712c-4498-bf41-4f19945ad99e"), null, 2, 3 },
                    { 324, 24, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(9839), 93, new Guid("b7de44c1-62ba-4f5b-abce-f2e85e049c63"), null, 2, 3 },
                    { 315, 23, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(9768), 90, new Guid("d5f536b2-e0ea-42a4-8823-9165f644922f"), null, 1, 3 },
                    { 314, 23, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(9761), 89, new Guid("ec9001ae-b18c-4f9a-ab0b-b7010b9a2b98"), null, 1, 3 },
                    { 313, 22, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(9753), 90, new Guid("7839d29a-a160-466d-8bca-aa0f8e95633a"), null, 1, 3 },
                    { 312, 22, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(9745), 89, new Guid("83b76f12-78e1-4b67-81b2-f377daf09ea1"), null, 1, 3 },
                    { 301, 25, false, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(9604), 88, new Guid("c53ba91c-f1dc-40c7-9222-87da7691e49a"), null, 2, 3 },
                    { 300, 25, false, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(9597), 87, new Guid("88f377bb-3769-4a96-be09-1b25df082da4"), null, 2, 3 },
                    { 299, 24, false, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(9589), 88, new Guid("6a7976b4-9db5-42ec-a360-a6ca2e2cb433"), null, 2, 3 },
                    { 298, 24, false, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(9582), 87, new Guid("17afc17b-2af5-4e1c-ad26-c0c3908fb525"), null, 2, 3 },
                    { 296, 25, false, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(9567), 85, new Guid("349a68c6-2bbf-4079-aea4-beb319e02dc6"), null, 2, 3 },
                    { 233, 23, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(8958), 3, new Guid("ce69d259-d2a3-4057-80a3-33d165d0809d"), null, 1, 3 },
                    { 275, 24, false, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(9403), 76, new Guid("b98395e4-c102-4248-b07d-082bd0773f28"), null, 2, 3 }
                });

            migrationBuilder.InsertData(
                schema: "Config",
                table: "LogSetting",
                columns: new[] { "LogSettingID", "EntityTypeID", "IsEnabled", "ModifiedDate", "OperationID", "rowguid", "SourceID", "SourceTypeID", "SubsystemID" },
                values: new object[,]
                {
                    { 273, 24, false, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(9388), 74, new Guid("e9be8520-135c-44fa-a764-013471982b2d"), null, 2, 3 },
                    { 251, 25, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(9092), 1, new Guid("e7a5168c-7325-49d5-a4e1-14aa4f4450ab"), null, 2, 3 },
                    { 250, 24, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(9085), 68, new Guid("d0c0803b-2748-4901-8c80-f6c1fa5315d6"), null, 2, 3 },
                    { 249, 24, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(9079), 58, new Guid("66b53704-ca60-4cef-8dde-95c2d913002e"), null, 2, 3 },
                    { 248, 24, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(9071), 16, new Guid("e4238aac-bb20-42d8-bf23-e8014ce2e74b"), null, 2, 3 },
                    { 247, 24, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(9064), 15, new Guid("bfaa6e45-0caf-4ee1-b41f-7f6e534cb433"), null, 2, 3 },
                    { 246, 24, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(9054), 14, new Guid("330280b3-90de-4fcb-be57-c040b37a0cf3"), null, 2, 3 },
                    { 245, 24, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(9047), 13, new Guid("08f3daf4-d4e3-4ec2-a1e3-6a989f2de1a3"), null, 2, 3 },
                    { 244, 24, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(9040), 6, new Guid("dce6b431-8736-4eeb-9be8-cbe7c150e2a8"), null, 2, 3 },
                    { 243, 24, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(9033), 4, new Guid("590d3d3c-8a89-4c8a-97c3-4e00bc4b84a2"), null, 2, 3 },
                    { 242, 24, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(9026), 3, new Guid("82718356-3fd1-4647-a2af-a1cb2646b09c"), null, 2, 3 },
                    { 241, 24, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(9017), 2, new Guid("671dedad-463d-43bc-b775-80f521c9ef41"), null, 2, 3 },
                    { 240, 24, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(9010), 1, new Guid("93b7659d-0efe-48f5-9a4a-5bc626651705"), null, 2, 3 },
                    { 239, 23, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(9003), 58, new Guid("0b1409c7-8fb8-4869-98c7-c4f68ca793b8"), null, 1, 3 },
                    { 238, 23, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(8993), 54, new Guid("a7770dbb-4465-41f4-b928-b39e60e22f6a"), null, 1, 3 },
                    { 237, 23, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(8986), 21, new Guid("b236d900-affd-4c92-9305-62e4b25c7e3c"), null, 1, 3 },
                    { 236, 23, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(8979), 6, new Guid("46538703-5f76-4793-a851-8fb341142292"), null, 1, 3 },
                    { 235, 23, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(8972), 5, new Guid("db110fd8-7045-42c2-98e0-72f6a81ab74f"), null, 1, 3 },
                    { 252, 25, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(9099), 2, new Guid("2019379a-2964-4b89-a93d-316b42f45333"), null, 2, 3 },
                    { 274, 24, false, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(9394), 75, new Guid("664d764b-ff8f-4222-98e0-4d0674ae0f4b"), null, 2, 3 },
                    { 253, 25, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(9107), 3, new Guid("cea89b2d-e7f3-4fea-9b7d-2115cc390a0e"), null, 2, 3 },
                    { 255, 25, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(9175), 6, new Guid("07dd82b4-a7c4-4006-b3dd-f83cfca23ce3"), null, 2, 3 },
                    { 272, 24, false, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(9380), 73, new Guid("c5dfe3e8-9846-4249-9fde-70c3eccb3ad7"), null, 2, 3 },
                    { 271, 24, false, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(9301), 72, new Guid("c412666f-5666-4fe1-aec8-7bd657db159e"), null, 2, 3 },
                    { 270, 24, false, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(9291), 71, new Guid("1b05d474-1a5d-4179-bd39-6bd1dcd2cff9"), null, 2, 3 },
                    { 269, 24, false, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(9284), 70, new Guid("2fedecea-0b26-432a-865e-4f51981d26e6"), null, 2, 3 },
                    { 268, 24, false, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(9277), 69, new Guid("81dbec6c-8c70-4753-92c4-d617b48cddbc"), null, 2, 3 },
                    { 267, 25, false, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(9270), 74, new Guid("7f97b047-46a1-4f1e-9c96-212670be9282"), null, 2, 3 },
                    { 266, 25, false, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(9263), 73, new Guid("971dbbbe-eb7f-4d4e-a34b-b7f00e110581"), null, 2, 3 },
                    { 265, 25, false, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(9256), 72, new Guid("32ebc235-55d7-4fc5-a4eb-b1503ca6a52c"), null, 2, 3 },
                    { 264, 25, false, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(9249), 71, new Guid("d29b9d20-1b3a-4376-b7a7-874a89fa960e"), null, 2, 3 },
                    { 263, 25, false, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(9241), 70, new Guid("421f53be-51fd-4837-8131-aa2325f2c730"), null, 2, 3 },
                    { 262, 25, false, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(9230), 69, new Guid("9d20586c-860d-4cca-b101-2bfa3224edf0"), null, 2, 3 },
                    { 261, 25, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(9223), 68, new Guid("0b6e5f6e-0dae-4966-b179-758e9b73dd6a"), null, 2, 3 },
                    { 260, 25, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(9217), 58, new Guid("36e383f1-4860-41ae-9eba-dd2a1ed2b3a4"), null, 2, 3 },
                    { 259, 25, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(9209), 16, new Guid("124afc3f-0c71-423f-85e2-8ac9ec088587"), null, 2, 3 },
                    { 258, 25, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(9202), 15, new Guid("29746dbf-e559-4b72-a9cf-4a78d51b42b5"), null, 2, 3 },
                    { 257, 25, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(9190), 14, new Guid("c313c701-c97a-4e95-9dec-8c5911a6ab25"), null, 2, 3 },
                    { 256, 25, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(9183), 13, new Guid("574bf941-35bf-4b29-a4ab-5afb5c9e380b"), null, 2, 3 },
                    { 254, 25, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(9114), 4, new Guid("bea84a01-9810-467a-8ac7-6462fae674dc"), null, 2, 3 },
                    { 100007, 100001, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(9923), 54, new Guid("51b40a80-5a2b-48b4-b8e4-64f1b642d45c"), null, 1, 100000 },
                    { 168, null, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(8357), 6, new Guid("ecc15608-1e70-4588-9ade-175da47d9118"), 4, 3, 2 }
                });

            migrationBuilder.InsertData(
                schema: "Config",
                table: "LogSetting",
                columns: new[] { "LogSettingID", "EntityTypeID", "IsEnabled", "ModifiedDate", "OperationID", "rowguid", "SourceID", "SourceTypeID", "SubsystemID" },
                values: new object[,]
                {
                    { 166, null, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(8337), 1, new Guid("7d2e66d9-bfa9-466b-986a-ab5cb5d798ce"), 4, 3, 2 },
                    { 60, 9, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(7238), 4, new Guid("a6d739fa-dd87-43a3-a77c-e5bb01b3bd60"), null, 1, 2 },
                    { 59, 9, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(7230), 3, new Guid("06fc3b92-56cf-42ab-b882-70d55902741e"), null, 1, 2 },
                    { 58, 9, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(7223), 2, new Guid("382b8588-b3e3-463f-abd5-cf99be8788b9"), null, 1, 2 },
                    { 57, 9, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(7214), 1, new Guid("4b4bb550-08a9-4075-955c-75121eb2b25f"), null, 1, 2 },
                    { 56, 7, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(7112), 58, new Guid("e037c37f-c08c-4b1f-aeac-544b4712daba"), null, 1, 2 },
                    { 55, 7, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(7104), 56, new Guid("8b50f96c-0830-4a6f-b210-28af31e7d501"), null, 1, 2 },
                    { 54, 7, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(7092), 55, new Guid("8095043f-ebfe-492c-98e2-8fcab73dad81"), null, 1, 2 },
                    { 53, 7, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(7084), 54, new Guid("b6ceb00c-ab7c-428d-a1ed-e43c9a321285"), null, 1, 2 },
                    { 52, 7, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(7076), 45, new Guid("f35e983a-5bd2-4c90-80f4-9d639ef49b3a"), null, 1, 2 },
                    { 51, 7, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(7068), 44, new Guid("00ee46f2-effa-4f39-8278-1deab37d6d10"), null, 1, 2 },
                    { 50, 7, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(7058), 43, new Guid("ec77ba96-546b-420f-b293-209a55deddd8"), null, 1, 2 },
                    { 49, 7, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(7050), 42, new Guid("0e4cda8a-ad43-4f0d-a024-29a3e9fd9db8"), null, 1, 2 },
                    { 48, 7, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(7040), 41, new Guid("272acf73-9683-407b-824b-77a611bbd0cc"), null, 1, 2 },
                    { 47, 7, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(7033), 40, new Guid("3dd086a4-6d6a-4a6d-8d1f-6314716a444f"), null, 1, 2 },
                    { 46, 7, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(7023), 21, new Guid("5b09ef38-f9df-45b5-8587-9cc35b375f5e"), null, 1, 2 },
                    { 45, 7, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(7016), 6, new Guid("365e5f19-5f9c-41ef-ba6a-4dd4947b4e07"), null, 1, 2 },
                    { 44, 7, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(7009), 5, new Guid("e3f7691b-1de7-4564-837e-cea78020c744"), null, 1, 2 },
                    { 61, 9, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(7245), 5, new Guid("44a48805-0552-452f-ad20-6e9ddad8a898"), null, 1, 2 },
                    { 43, 7, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(7002), 4, new Guid("4a695363-2d24-4549-bc9d-e2bd09848743"), null, 1, 2 },
                    { 62, 9, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(7253), 6, new Guid("20d67a42-d8bb-4761-9e67-d301c02046f0"), null, 1, 2 },
                    { 64, 9, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(7274), 54, new Guid("90ff1bf1-82f6-40b9-80ca-4173cb485aaf"), null, 1, 2 },
                    { 81, 11, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(7419), 30, new Guid("89f8113d-f304-457f-9236-3c2a531e9d23"), null, 2, 2 },
                    { 80, 11, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(7410), 8, new Guid("8ae4a0f0-5340-4d02-9b2d-6d89ed479ba0"), null, 2, 2 },
                    { 79, 11, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(7402), 6, new Guid("6f52e454-3203-47d0-a77d-0a3078162620"), null, 2, 2 },
                    { 78, 11, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(7390), 5, new Guid("81678c6c-01e2-4aed-9315-bce3c78b9a34"), null, 2, 2 },
                    { 77, 11, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(7383), 4, new Guid("c2c52bcb-203c-4a9f-8aaa-e20458825bca"), null, 2, 2 },
                    { 76, 11, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(7375), 1, new Guid("9bd1bdc0-f774-4aba-9728-952340c502c1"), null, 2, 2 },
                    { 75, 10, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(7368), 58, new Guid("cb0e04ca-8847-4317-9d65-ee9c8f2c5a88"), null, 1, 2 },
                    { 74, 10, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(7360), 54, new Guid("e28ebc94-dc53-4c19-a3c5-5099d83f9a94"), null, 1, 2 },
                    { 73, 10, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(7352), 35, new Guid("7f961052-d300-4d05-8e45-fff8906d9b49"), null, 1, 2 },
                    { 72, 10, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(7344), 21, new Guid("360f2620-6004-41b0-8c49-e71a117864a6"), null, 1, 2 },
                    { 71, 10, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(7337), 6, new Guid("e5e5e080-1d01-4c4f-9992-3b99d4e04414"), null, 1, 2 },
                    { 70, 10, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(7326), 5, new Guid("334c37bc-1461-4422-bc8c-d1a4c5ebf160"), null, 1, 2 },
                    { 69, 10, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(7319), 4, new Guid("8a817398-de96-437a-9ece-f76ec9fb3c65"), null, 1, 2 },
                    { 68, 10, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(7311), 3, new Guid("7cc74cfc-fcd2-495f-8421-fd9f028ed291"), null, 1, 2 },
                    { 67, 10, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(7302), 2, new Guid("0a9ad3e3-6b21-40b0-bb85-a15a85b16bd6"), null, 1, 2 },
                    { 66, 10, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(7294), 1, new Guid("b97ea36f-3ba6-4d0d-846b-8fd36af64315"), null, 1, 2 },
                    { 65, 9, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(7285), 58, new Guid("cca1b22a-2e11-4e4c-b18a-1c52a38c012e"), null, 1, 2 },
                    { 63, 9, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(7264), 21, new Guid("5be43b82-0669-4f80-9daf-36ace329aff0"), null, 1, 2 },
                    { 82, 11, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(7426), 54, new Guid("40e365a4-0284-4e67-9c24-e3be1ecef8d0"), null, 2, 2 },
                    { 42, 7, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(6993), 3, new Guid("8d5f48c6-48bf-4b5f-a153-a6644f0f6c6e"), null, 1, 2 }
                });

            migrationBuilder.InsertData(
                schema: "Config",
                table: "LogSetting",
                columns: new[] { "LogSettingID", "EntityTypeID", "IsEnabled", "ModifiedDate", "OperationID", "rowguid", "SourceID", "SourceTypeID", "SubsystemID" },
                values: new object[,]
                {
                    { 40, 7, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(6978), 1, new Guid("ed4aeeed-2c4e-4ccc-8aeb-07bedc052d89"), null, 1, 2 },
                    { 18, 4, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(6576), 21, new Guid("5d1eb9f6-33b1-4a71-9269-be9146b5a89d"), null, 1, 2 },
                    { 17, 4, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(6567), 6, new Guid("fad1d956-7235-44ee-9404-7396b01fd171"), null, 1, 2 },
                    { 16, 4, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(6556), 5, new Guid("9158e629-7421-45ce-92fa-69f2515437cd"), null, 1, 2 },
                    { 15, 4, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(6548), 4, new Guid("14862627-0d03-4319-8173-84528f0bd7da"), null, 1, 2 },
                    { 14, 4, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(6535), 3, new Guid("fe4f96b7-453f-41ed-8796-71c0e17449cb"), null, 1, 2 },
                    { 13, 4, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(6528), 2, new Guid("1e07586d-a095-4efd-84b8-863db08c1fc0"), null, 1, 2 },
                    { 12, 4, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(6521), 1, new Guid("e07e3ac8-4edb-43f5-8e4b-bf2cfa470fae"), null, 1, 2 },
                    { 11, 2, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(6514), 7, new Guid("1cbfc750-8ab6-4771-b0b5-d3d8c4570110"), null, 1, 2 },
                    { 10, 2, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(6507), 1, new Guid("cb4fa16c-e29d-479f-a61c-7b54cd6357de"), null, 1, 2 },
                    { 9, 1, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(6496), 58, new Guid("5eb3a11b-ba74-488b-adc3-31c57a148a85"), null, 1, 2 },
                    { 8, 1, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(6487), 54, new Guid("750e41d5-821c-4f5b-9610-c332acc919f9"), null, 1, 2 },
                    { 7, 1, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(6480), 21, new Guid("7b9b9695-5cb5-47d3-ab17-34ccbd523da2"), null, 1, 2 },
                    { 6, 1, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(6456), 6, new Guid("c0b9c85b-2cbf-4993-843e-4ffe225ace4e"), null, 1, 2 },
                    { 5, 1, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(6435), 5, new Guid("d4c63b4f-62fd-4880-aab4-cc93ae83e04f"), null, 1, 2 },
                    { 4, 1, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(6425), 4, new Guid("e1c3402d-5087-4932-8a4a-1728fba89254"), null, 1, 2 },
                    { 3, 1, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(6417), 3, new Guid("16781576-cb20-424e-9cab-1e3eb66b3b78"), null, 1, 2 },
                    { 2, 1, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(6403), 2, new Guid("2cc92599-6a85-4ebc-86f4-d9184f3cbc8e"), null, 1, 2 },
                    { 19, 4, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(6583), 54, new Guid("b00f779c-1ab8-482c-b7fb-0a1211aa124d"), null, 1, 2 },
                    { 41, 7, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(6986), 2, new Guid("7d942914-998e-428a-8b95-bbb8478a564f"), null, 1, 2 },
                    { 20, 4, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(6591), 58, new Guid("629f90c1-a615-42df-a214-fe2210f46c55"), null, 1, 2 },
                    { 22, 5, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(6606), 2, new Guid("1f3f7b3c-9bbc-4380-b273-b6c1c195f5e6"), null, 1, 2 },
                    { 39, 6, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(6969), 58, new Guid("fcbbfd77-ecf8-49b8-a6ef-313e86a38874"), null, 1, 2 },
                    { 38, 6, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(6959), 54, new Guid("cdd702c3-945b-423e-8ec4-2792494f9310"), null, 1, 2 },
                    { 37, 6, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(6952), 21, new Guid("fb89cedd-580c-45b1-8f2a-3a24ef64808c"), null, 1, 2 },
                    { 36, 6, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(6944), 6, new Guid("834ebd27-38a2-4a72-8e4a-d6d49a3ef051"), null, 1, 2 },
                    { 35, 6, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(6937), 5, new Guid("845b9676-1be6-43d9-aa27-70e040266895"), null, 1, 2 },
                    { 34, 6, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(6928), 4, new Guid("19b00b4f-06d4-47c7-8da6-2864a55e0985"), null, 1, 2 },
                    { 33, 6, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(6916), 3, new Guid("d4ad54b4-587e-4cc4-ac04-9268c6445577"), null, 1, 2 },
                    { 32, 6, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(6908), 2, new Guid("1b4505ad-53b0-4466-9fea-9ebc6f0d1d1e"), null, 1, 2 },
                    { 31, 6, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(6900), 1, new Guid("0e46d95b-e896-4a11-b73d-18843fad8777"), null, 1, 2 },
                    { 30, 5, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(6884), 58, new Guid("c7e0a019-3076-47ae-ad51-22e325d50438"), null, 1, 2 },
                    { 29, 5, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(6754), 54, new Guid("95330660-b8f0-4250-8209-9f0cb13e6878"), null, 1, 2 },
                    { 28, 5, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(6747), 35, new Guid("0a58f754-d428-49cf-9983-c9335631da63"), null, 1, 2 },
                    { 27, 5, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(6737), 21, new Guid("1a5480df-d530-46e5-b41b-9a52f2e049f6"), null, 1, 2 },
                    { 26, 5, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(6727), 6, new Guid("4b06b13f-f011-4403-a04c-4ffc135fc597"), null, 1, 2 },
                    { 25, 5, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(6717), 5, new Guid("532cb20b-1887-4ab8-a476-dc4c77f5079f"), null, 1, 2 },
                    { 24, 5, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(6706), 4, new Guid("29956dfe-6b37-4440-bee3-d562d06a0815"), null, 1, 2 },
                    { 23, 5, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(6617), 3, new Guid("5c6266e9-804f-4a63-b9d7-183615576072"), null, 1, 2 },
                    { 21, 5, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(6598), 1, new Guid("f2f643e5-4009-4403-b980-8257b0703d4c"), null, 1, 2 },
                    { 167, null, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(8350), 5, new Guid("265d8109-2336-4f7a-8392-8fbfcc44a11f"), 4, 3, 2 },
                    { 83, 11, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(7434), 58, new Guid("ebc7c43c-8880-4a48-9ad8-dc5fa8de370a"), null, 2, 2 }
                });

            migrationBuilder.InsertData(
                schema: "Config",
                table: "LogSetting",
                columns: new[] { "LogSettingID", "EntityTypeID", "IsEnabled", "ModifiedDate", "OperationID", "rowguid", "SourceID", "SourceTypeID", "SubsystemID" },
                values: new object[,]
                {
                    { 85, 12, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(7448), 2, new Guid("eb030d61-6a2e-4e1a-8f20-1e3ecdb9ab53"), null, 1, 2 },
                    { 144, 20, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(8079), 2, new Guid("d6aa772a-9237-4b34-b966-04f8ecb96354"), null, 3, 2 },
                    { 143, 20, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(8072), 1, new Guid("5010d814-0c00-421d-a28e-eb7f7544731d"), null, 3, 2 },
                    { 142, 19, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(8060), 54, new Guid("e600792b-28b4-454a-a9b9-ec42a7b7e5aa"), null, 3, 2 },
                    { 141, 19, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(8052), 6, new Guid("43b18d84-d0ea-4207-88fa-c1cecfb6dd7b"), null, 3, 2 },
                    { 140, 19, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(8046), 4, new Guid("874b3804-7243-484f-ac8a-40854574f123"), null, 3, 2 },
                    { 139, 19, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(8039), 3, new Guid("dc3e6a0b-269b-440c-bdd5-628eecbc1c7a"), null, 3, 2 },
                    { 138, 19, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(8032), 2, new Guid("b7c63f6e-d147-4a3b-8388-6e78b54ce4e3"), null, 3, 2 },
                    { 137, 18, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(8025), 58, new Guid("aaf4d185-212f-4d2e-bba1-fe6b1f3b4384"), null, 2, 2 },
                    { 136, 18, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(8018), 53, new Guid("5db90b27-bbd2-49db-8c20-6770aa1e380a"), null, 2, 2 },
                    { 135, 18, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(8011), 52, new Guid("c8c4bed7-61b1-4964-a91d-11e74dacb8a0"), null, 2, 2 },
                    { 134, 18, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(7999), 47, new Guid("48ecbf21-952b-46b4-9d35-fd29eebe503f"), null, 2, 2 },
                    { 133, 18, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(7992), 46, new Guid("f7510f4f-88e0-4226-b676-db4b7196131d"), null, 2, 2 },
                    { 132, 18, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(7985), 39, new Guid("1f11d513-79de-4d9c-a421-9fa0611916e1"), null, 2, 2 },
                    { 131, 18, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(7978), 38, new Guid("1cc77848-d7d5-4db9-b539-42da7fb92d90"), null, 2, 2 },
                    { 130, 18, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(7970), 37, new Guid("e32d1bb5-8fc5-4de3-8dbe-4daa841e032c"), null, 2, 2 },
                    { 129, 18, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(7960), 36, new Guid("09128132-4b95-45f2-ae95-d216bf0195a1"), null, 2, 2 },
                    { 128, 18, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(7953), 21, new Guid("03c04259-8175-449b-a39f-fe1ca83fe9fc"), null, 2, 2 },
                    { 145, 20, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(8086), 3, new Guid("8507aac0-5e93-4634-8300-07ff54abbc9f"), null, 3, 2 },
                    { 127, 18, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(7946), 12, new Guid("ac9f46d0-e6f7-4252-9ca6-fe99cc6497fc"), null, 2, 2 },
                    { 146, 20, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(8093), 4, new Guid("a39e4a9a-2602-41af-84b3-9ec86facd3ad"), null, 3, 2 },
                    { 148, 20, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(8107), 6, new Guid("5c9c0fa2-f9b6-497a-b38f-5d7be4378254"), null, 3, 2 },
                    { 165, null, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(8330), 58, new Guid("599069ac-bb0d-444c-b306-166a822b24eb"), 3, 3, 2 },
                    { 164, null, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(8323), 54, new Guid("975cad66-eadd-40dd-9f52-3657ecd6cfeb"), 3, 3, 2 },
                    { 163, null, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(8316), 6, new Guid("2691a4b8-9ddb-45e2-9293-0110fde98120"), 3, 3, 2 },
                    { 162, null, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(8309), 5, new Guid("eced75e8-2d0e-4466-8cb5-0227218e5780"), 3, 3, 2 },
                    { 161, null, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(8302), 1, new Guid("a40988b6-e644-4dc7-93bd-2257715e5f44"), 3, 3, 2 },
                    { 160, null, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(8295), 58, new Guid("7ca600bc-bffa-49d2-bb49-7b7a8bc9b4bc"), 2, 3, 2 },
                    { 159, null, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(8289), 54, new Guid("c5b4233e-6d97-4712-a9e3-0b5385f3e275"), 2, 3, 2 },
                    { 158, null, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(8276), 6, new Guid("80abb5dc-b9bb-4118-86a6-88136503ebdf"), 2, 3, 2 },
                    { 157, null, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(8269), 5, new Guid("f96ff27d-fbb8-413b-bf2e-a32bf24cf5ca"), 2, 3, 2 },
                    { 156, null, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(8263), 1, new Guid("eb038d2d-64da-4342-b4ce-38ba1163d76b"), 2, 3, 2 },
                    { 155, null, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(8254), 58, new Guid("1c1ddde0-23e3-4fdc-bd8a-5969311256bf"), 1, 3, 2 },
                    { 154, null, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(8247), 54, new Guid("e3d0a0c3-ea8a-492c-b2f1-2938f9f0b86c"), 1, 3, 2 },
                    { 153, null, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(8240), 6, new Guid("0f4aa43e-78c4-475c-8a85-4edf715087c4"), 1, 3, 2 },
                    { 152, null, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(8233), 5, new Guid("3682d9f8-0719-4e3f-bdd6-c23aeeda52bd"), 1, 3, 2 },
                    { 151, null, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(8226), 1, new Guid("73eb9316-3118-4b9d-9745-b168b7eeaa7a"), 1, 3, 2 },
                    { 150, 20, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(8216), 58, new Guid("985e57ac-0cf7-43f3-b3f2-d4cb8dc1b3ae"), null, 3, 2 },
                    { 149, 20, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(8208), 54, new Guid("097b57ed-0d4f-4d02-b4a4-2cbd1bc120f5"), null, 3, 2 },
                    { 147, 20, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(8100), 5, new Guid("ecf5352e-5b00-4eee-a29d-fd7c4de1cfd4"), null, 3, 2 },
                    { 84, 12, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(7441), 1, new Guid("5b3e83dc-1b40-4518-87ac-b8b20a06a0e4"), null, 1, 2 },
                    { 126, 18, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(7935), 11, new Guid("0e061137-d3d2-4cd1-8f32-122561986883"), null, 2, 2 }
                });

            migrationBuilder.InsertData(
                schema: "Config",
                table: "LogSetting",
                columns: new[] { "LogSettingID", "EntityTypeID", "IsEnabled", "ModifiedDate", "OperationID", "rowguid", "SourceID", "SourceTypeID", "SubsystemID" },
                values: new object[,]
                {
                    { 124, 18, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(7921), 4, new Guid("c974081d-e6a7-4fdf-9c98-2a415ff52985"), null, 2, 2 },
                    { 102, 17, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(7641), 12, new Guid("39802c5b-abe1-4512-b8fd-06c304068e25"), null, 2, 2 },
                    { 101, 17, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(7633), 11, new Guid("7d3eb1af-26e1-428d-9a22-575774c7f6a6"), null, 2, 2 },
                    { 100, 17, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(7625), 6, new Guid("fed9f648-10d8-4b12-9b00-5865925d6e94"), null, 2, 2 },
                    { 99, 17, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(7618), 4, new Guid("80711c3c-218f-4c58-92bb-0cbb867b8337"), null, 2, 2 },
                    { 98, 17, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(7610), 3, new Guid("bc93db35-d9ed-4bd5-8fb8-9f63b4b8c060"), null, 2, 2 },
                    { 97, 17, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(7603), 2, new Guid("d74b7037-9c9d-4261-9a19-11fa16be6f36"), null, 2, 2 },
                    { 96, 17, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(7596), 1, new Guid("0500dd5e-3825-41a8-9f5c-7f4643106dda"), null, 2, 2 },
                    { 95, 15, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(7587), 31, new Guid("38927b56-8d9a-4ce0-973a-93a448dc4d4e"), null, 1, 2 },
                    { 94, 15, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(7574), 7, new Guid("83bca09a-13bb-4d4e-a46c-0a5c2322e761"), null, 1, 2 },
                    { 93, 15, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(7566), 1, new Guid("eab4ed9f-828a-455c-b8cf-8be5cf463abd"), null, 1, 2 },
                    { 92, 12, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(7558), 58, new Guid("4afd4e62-a791-4fbc-98d8-04a839a1ebf5"), null, 1, 2 },
                    { 91, 12, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(7549), 54, new Guid("49b431b5-c439-49b7-bfe6-017d55c12611"), null, 1, 2 },
                    { 90, 12, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(7542), 21, new Guid("d172afaf-517f-4a31-9f1e-037b96de5d9d"), null, 1, 2 },
                    { 89, 12, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(7535), 6, new Guid("f3da7070-f12b-4cf0-9335-709b7becdcb0"), null, 1, 2 },
                    { 88, 12, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(7527), 5, new Guid("df9d7d66-e56f-4108-8133-bfef43d69fe0"), null, 1, 2 },
                    { 87, 12, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(7466), 4, new Guid("5469291f-cb6f-4789-8f4f-39d0d654e851"), null, 1, 2 },
                    { 86, 12, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(7455), 3, new Guid("6d9005f8-f4ce-488d-b6b4-575270e09e07"), null, 1, 2 },
                    { 103, 17, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(7654), 13, new Guid("942443bc-db1f-44d5-bc13-e9ff4e510dc0"), null, 2, 2 },
                    { 125, 18, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(7928), 6, new Guid("cbf1f187-d0f9-4f09-85ff-eebfd3fb6834"), null, 2, 2 },
                    { 104, 17, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(7661), 14, new Guid("34893df9-c8ae-43fb-87ae-9e47bffe6cfd"), null, 2, 2 },
                    { 106, 17, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(7675), 16, new Guid("f9a4c4ae-2c63-401d-9796-af6b118987db"), null, 2, 2 },
                    { 123, 18, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(7911), 3, new Guid("bf3ff883-3654-43e5-b409-a060041715da"), null, 2, 2 },
                    { 122, 18, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(7800), 2, new Guid("21d1b9c3-afab-4f69-8b38-19a02bd5f878"), null, 2, 2 },
                    { 121, 18, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(7793), 1, new Guid("6d45a4e5-c017-4688-9589-0ca2c1c0cfa0"), null, 2, 2 },
                    { 120, 17, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(7787), 58, new Guid("9a47476e-bbeb-4de7-862c-2ce5716fd329"), null, 2, 2 },
                    { 119, 17, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(7779), 51, new Guid("d7434e66-ae9d-4113-98c5-c2918fd14c48"), null, 2, 2 },
                    { 118, 17, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(7769), 50, new Guid("18b0acd9-ffa4-4285-80a6-21d9aac8cb7a"), null, 2, 2 },
                    { 117, 17, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(7762), 49, new Guid("98fe4e34-19b7-4c37-b8f8-2e461e67191d"), null, 2, 2 },
                    { 116, 17, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(7754), 48, new Guid("653c11de-9bef-4525-b8c3-1088378a0aee"), null, 2, 2 },
                    { 115, 17, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(7747), 47, new Guid("fabb0fe4-4476-4c7f-86a7-1fc1d4fb2b2b"), null, 2, 2 },
                    { 114, 17, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(7740), 46, new Guid("04636182-58e8-4693-af36-c5c5d8bf189e"), null, 2, 2 },
                    { 113, 17, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(7733), 39, new Guid("0ffa96eb-b6f0-4c4f-9a6d-af3d04be68bd"), null, 2, 2 },
                    { 112, 17, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(7725), 38, new Guid("f194d0fe-2870-4524-a84f-af5f68c11dc2"), null, 2, 2 },
                    { 111, 17, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(7717), 37, new Guid("cd22127d-92a2-42b0-8fef-d01431e5f211"), null, 2, 2 },
                    { 110, 17, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(7706), 36, new Guid("db5cd5df-0f28-421b-a4f9-76316a2e603e"), null, 2, 2 },
                    { 109, 17, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(7699), 21, new Guid("45b89d85-98e8-4342-8822-d44b376caef1"), null, 2, 2 },
                    { 108, 17, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(7690), 18, new Guid("8696511d-56e1-48df-96ad-a9aadfdc03b6"), null, 2, 2 },
                    { 107, 17, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(7683), 17, new Guid("f439be38-3b64-4672-bdc6-c483789eba08"), null, 2, 2 },
                    { 105, 17, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(7668), 15, new Guid("49f4f6a2-ef9f-47ba-836f-7ca3c402ec8d"), null, 2, 2 },
                    { 100008, 100001, true, new DateTime(2023, 10, 18, 14, 1, 28, 461, DateTimeKind.Local).AddTicks(9934), 58, new Guid("cb1f7b89-3836-44a7-90cd-5bef20d6356d"), null, 1, 100000 }
                });

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
                name: "IX_AccountOwner_AccountID",
                schema: "Finance",
                table: "AccountOwner",
                column: "AccountID",
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
                name: "IX_CustomerTaxInfo_AccountID",
                schema: "Finance",
                table: "CustomerTaxInfo",
                column: "AccountID",
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
                name: "IX_LogSetting_EntityTypeID",
                schema: "Config",
                table: "LogSetting",
                column: "EntityTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_LogSetting_OperationID",
                schema: "Config",
                table: "LogSetting",
                column: "OperationID");

            migrationBuilder.CreateIndex(
                name: "IX_LogSetting_SourceID",
                schema: "Config",
                table: "LogSetting",
                column: "SourceID");

            migrationBuilder.CreateIndex(
                name: "IX_LogSetting_SourceTypeID",
                schema: "Config",
                table: "LogSetting",
                column: "SourceTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_LogSetting_SubsystemID",
                schema: "Config",
                table: "LogSetting",
                column: "SubsystemID");

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
                name: "IX_Setting_ParentID",
                schema: "Config",
                table: "Setting",
                column: "ParentID");

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
                name: "IX_Voucher_StatusID",
                schema: "Finance",
                table: "Voucher",
                column: "StatusID");

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
