using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SPPC.Tadbir.Web.Api.Migrations
{
    public partial class initialmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Finance");

            migrationBuilder.EnsureSchema(
                name: "Corporate");

            migrationBuilder.EnsureSchema(
                name: "ProductScope");

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

            migrationBuilder.EnsureSchema(
                name: "Contact");

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
                name: "CompanyDb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DbName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Server = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RowGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyDb", x => x.Id);
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
                name: "Role",
                schema: "Auth",
                columns: table => new
                {
                    RoleID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.RoleID);
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
                    DescriptionKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
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
                name: "User",
                schema: "Auth",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    PasswordHash = table.Column<string>(type: "varchar(256)", unicode: false, maxLength: 256, nullable: false),
                    LastLoginDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    IsEnabled = table.Column<bool>(type: "bit", nullable: false),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserID);
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
                name: "View",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EntityName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EntityType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsHierarchy = table.Column<bool>(type: "bit", nullable: false),
                    IsCartableIntegrated = table.Column<bool>(type: "bit", nullable: false),
                    FetchUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SearchUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RowGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_View", x => x.Id);
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
                    table.ForeignKey(
                        name: "FK_Brand_FiscalPeriod_FiscalPeriodId",
                        column: x => x.FiscalPeriodId,
                        principalSchema: "Finance",
                        principalTable: "FiscalPeriod",
                        principalColumn: "FiscalPeriodID",
                        onDelete: ReferentialAction.Cascade);
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
                    table.ForeignKey(
                        name: "FK_Unit_FiscalPeriod_FiscalPeriodId",
                        column: x => x.FiscalPeriodId,
                        principalSchema: "Finance",
                        principalTable: "FiscalPeriod",
                        principalColumn: "FiscalPeriodID",
                        onDelete: ReferentialAction.Cascade);
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
                name: "RoleCompany",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    RowGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleCompany", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleCompany_CompanyDb_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "CompanyDb",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoleCompany_Role_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "Auth",
                        principalTable: "Role",
                        principalColumn: "RoleID",
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
                name: "LogSetting",
                schema: "Config",
                columns: table => new
                {
                    LogSettingID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsEnabled = table.Column<bool>(type: "bit", nullable: false),
                    SubsystemID = table.Column<int>(type: "int", nullable: false),
                    SourceTypeID = table.Column<int>(type: "int", nullable: false),
                    SourceID = table.Column<int>(type: "int", nullable: true),
                    EntityTypeID = table.Column<int>(type: "int", nullable: true),
                    OperationID = table.Column<int>(type: "int", nullable: false),
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
                name: "PermissionGroup",
                schema: "Auth",
                columns: table => new
                {
                    PermissionGroupID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubsystemId = table.Column<int>(type: "int", nullable: false),
                    SourceTypeId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    EntityName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermissionGroup", x => x.PermissionGroupID);
                    table.ForeignKey(
                        name: "FK_Auth_PermissionGroup_Metadata_SourceType",
                        column: x => x.SourceTypeId,
                        principalSchema: "Metadata",
                        principalTable: "OperationSourceType",
                        principalColumn: "OperationSourceTypeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Auth_PermissionGroup_Metadata_Subsystem",
                        column: x => x.SubsystemId,
                        principalSchema: "Metadata",
                        principalTable: "Subsystem",
                        principalColumn: "SubsystemID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Person",
                schema: "Contact",
                columns: table => new
                {
                    PersonID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: true),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Person", x => x.PersonID);
                    table.ForeignKey(
                        name: "FK_Contact_Person_Auth_User",
                        column: x => x.UserID,
                        principalSchema: "Auth",
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserRole",
                schema: "Auth",
                columns: table => new
                {
                    UserRoleID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRole", x => x.UserRoleID);
                    table.UniqueConstraint("AK_UserRole_UserId_RoleId", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRole_Role_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "Auth",
                        principalTable: "Role",
                        principalColumn: "RoleID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRole_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "Auth",
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
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
                name: "Column",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GroupName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DotNetType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StorageType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ScriptType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Length = table.Column<int>(type: "int", nullable: false),
                    MinLength = table.Column<int>(type: "int", nullable: false),
                    IsFixedLength = table.Column<bool>(type: "bit", nullable: false),
                    IsDynamic = table.Column<bool>(type: "bit", nullable: false),
                    IsNullable = table.Column<bool>(type: "bit", nullable: false),
                    AllowSorting = table.Column<bool>(type: "bit", nullable: false),
                    AllowFiltering = table.Column<bool>(type: "bit", nullable: false),
                    Visibility = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DisplayIndex = table.Column<short>(type: "smallint", nullable: false),
                    Expression = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ViewId = table.Column<int>(type: "int", nullable: true),
                    RowGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Column", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Column_View_ViewId",
                        column: x => x.ViewId,
                        principalTable: "View",
                        principalColumn: "Id",
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
                        name: "FK_Config_UserSetting_Auth_Role",
                        column: x => x.RoleId,
                        principalSchema: "Auth",
                        principalTable: "Role",
                        principalColumn: "RoleID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Config_UserSetting_Auth_User",
                        column: x => x.UserId,
                        principalSchema: "Auth",
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Config_UserSetting_Config_Setting",
                        column: x => x.SettingId,
                        principalSchema: "Config",
                        principalTable: "Setting",
                        principalColumn: "SettingID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Config_UserSetting_Metadata_EntityView",
                        column: x => x.ViewId,
                        principalTable: "View",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ViewRowPermission",
                schema: "Auth",
                columns: table => new
                {
                    RowPermissionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    ViewID = table.Column<int>(type: "int", nullable: false),
                    AccessMode = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Value = table.Column<double>(type: "float", nullable: false),
                    Value2 = table.Column<double>(type: "float", nullable: false),
                    TextValue = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    Items = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: true),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ViewRowPermission", x => x.RowPermissionID);
                    table.ForeignKey(
                        name: "FK_Auth_ViewRowPermission_Auth_Role",
                        column: x => x.RoleId,
                        principalSchema: "Auth",
                        principalTable: "Role",
                        principalColumn: "RoleID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Auth_ViewRowPermission_Metadata_View",
                        column: x => x.ViewID,
                        principalTable: "View",
                        principalColumn: "Id",
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
                name: "Permission",
                schema: "Auth",
                columns: table => new
                {
                    PermissionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Flag = table.Column<int>(type: "int", nullable: false, defaultValueSql: "((0))"),
                    Description = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permission", x => x.PermissionID);
                    table.ForeignKey(
                        name: "FK_Auth_Permission_Auth_PermissionGroup",
                        column: x => x.GroupId,
                        principalSchema: "Auth",
                        principalTable: "PermissionGroup",
                        principalColumn: "PermissionGroupID",
                        onDelete: ReferentialAction.Restrict);
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
                name: "Command",
                schema: "Metadata",
                columns: table => new
                {
                    CommandID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParentId = table.Column<int>(type: "int", nullable: true),
                    PermissionId = table.Column<int>(type: "int", nullable: true),
                    TitleKey = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    RouteUrl = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    IconName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    HotKey = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    Index = table.Column<int>(type: "int", nullable: true),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Command", x => x.CommandID);
                    table.ForeignKey(
                        name: "FK_Metadata_Command_Auth_Permission",
                        column: x => x.PermissionId,
                        principalSchema: "Auth",
                        principalTable: "Permission",
                        principalColumn: "PermissionID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Metadata_Command_Metadata_Parent",
                        column: x => x.ParentId,
                        principalSchema: "Metadata",
                        principalTable: "Command",
                        principalColumn: "CommandID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RolePermission",
                schema: "Auth",
                columns: table => new
                {
                    RolePermissionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    PermissionId = table.Column<int>(type: "int", nullable: false),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolePermission", x => x.RolePermissionID);
                    table.UniqueConstraint("AK_RolePermission_RoleId_PermissionId", x => new { x.RoleId, x.PermissionId });
                    table.ForeignKey(
                        name: "FK_RolePermission_Permission_PermissionId",
                        column: x => x.PermissionId,
                        principalSchema: "Auth",
                        principalTable: "Permission",
                        principalColumn: "PermissionID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RolePermission_Role_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "Auth",
                        principalTable: "Role",
                        principalColumn: "RoleID",
                        onDelete: ReferentialAction.Cascade);
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
                name: "IX_Brand_FiscalPeriodId",
                schema: "ProductScope",
                table: "Brand",
                column: "FiscalPeriodId");

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
                name: "IX_Column_ViewId",
                table: "Column",
                column: "ViewId");

            migrationBuilder.CreateIndex(
                name: "IX_Command_ParentId",
                schema: "Metadata",
                table: "Command",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Command_PermissionId",
                schema: "Metadata",
                table: "Command",
                column: "PermissionId");

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
                name: "IX_Permission_GroupId",
                schema: "Auth",
                table: "Permission",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_PermissionGroup_SourceTypeId",
                schema: "Auth",
                table: "PermissionGroup",
                column: "SourceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PermissionGroup_SubsystemId",
                schema: "Auth",
                table: "PermissionGroup",
                column: "SubsystemId");

            migrationBuilder.CreateIndex(
                name: "IX_Person_UserID",
                schema: "Contact",
                table: "Person",
                column: "UserID",
                unique: true,
                filter: "[UserID] IS NOT NULL");

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
                name: "IX_RoleBranch_BranchId",
                schema: "Auth",
                table: "RoleBranch",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleCompany_CompanyId",
                table: "RoleCompany",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleCompany_RoleId",
                table: "RoleCompany",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleFiscalPeriod_FiscalPeriodId",
                schema: "Auth",
                table: "RoleFiscalPeriod",
                column: "FiscalPeriodId");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermission_PermissionId",
                schema: "Auth",
                table: "RolePermission",
                column: "PermissionId");

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
                name: "IX_Unit_FiscalPeriodId",
                schema: "ProductScope",
                table: "Unit",
                column: "FiscalPeriodId");

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
                name: "IX_UserRole_RoleId",
                schema: "Auth",
                table: "UserRole",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSetting_RoleId",
                schema: "Config",
                table: "UserSetting",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSetting_SettingId",
                schema: "Config",
                table: "UserSetting",
                column: "SettingId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSetting_UserId",
                schema: "Config",
                table: "UserSetting",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSetting_ViewId",
                schema: "Config",
                table: "UserSetting",
                column: "ViewId");

            migrationBuilder.CreateIndex(
                name: "IX_UserValue_CategoryId",
                schema: "Config",
                table: "UserValue",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ViewRowPermission_RoleId",
                schema: "Auth",
                table: "ViewRowPermission",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_ViewRowPermission_ViewID",
                schema: "Auth",
                table: "ViewRowPermission",
                column: "ViewID");

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
                name: "Brand",
                schema: "ProductScope");

            migrationBuilder.DropTable(
                name: "CheckBookPage",
                schema: "Check");

            migrationBuilder.DropTable(
                name: "City",
                schema: "Metadata");

            migrationBuilder.DropTable(
                name: "Column");

            migrationBuilder.DropTable(
                name: "Command",
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
                name: "Person",
                schema: "Contact");

            migrationBuilder.DropTable(
                name: "RoleBranch",
                schema: "Auth");

            migrationBuilder.DropTable(
                name: "RoleCompany");

            migrationBuilder.DropTable(
                name: "RoleFiscalPeriod",
                schema: "Auth");

            migrationBuilder.DropTable(
                name: "RolePermission",
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
                name: "UserRole",
                schema: "Auth");

            migrationBuilder.DropTable(
                name: "UserSetting",
                schema: "Config");

            migrationBuilder.DropTable(
                name: "UserValue",
                schema: "Config");

            migrationBuilder.DropTable(
                name: "ViewRowPermission",
                schema: "Auth");

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
                name: "CompanyDb");

            migrationBuilder.DropTable(
                name: "Permission",
                schema: "Auth");

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
                name: "User",
                schema: "Auth");

            migrationBuilder.DropTable(
                name: "Setting",
                schema: "Config");

            migrationBuilder.DropTable(
                name: "UserValueCategory",
                schema: "Config");

            migrationBuilder.DropTable(
                name: "Role",
                schema: "Auth");

            migrationBuilder.DropTable(
                name: "View");

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
                name: "PermissionGroup",
                schema: "Auth");

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
                name: "OperationSourceType",
                schema: "Metadata");

            migrationBuilder.DropTable(
                name: "Subsystem",
                schema: "Metadata");

            migrationBuilder.DropTable(
                name: "FiscalPeriod",
                schema: "Finance");

            migrationBuilder.DropTable(
                name: "Branch",
                schema: "Corporate");
        }
    }
}
