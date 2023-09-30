using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SPPC.Tadbir.Web.Api.Migrations.System
{
    public partial class sysinitialmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Metadata");

            migrationBuilder.EnsureSchema(
                name: "Config");

            migrationBuilder.EnsureSchema(
                name: "Reporting");

            migrationBuilder.EnsureSchema(
                name: "Auth");

            migrationBuilder.EnsureSchema(
                name: "Contact");

            migrationBuilder.EnsureSchema(
                name: "Core");

            migrationBuilder.CreateTable(
                name: "CompanyDb",
                schema: "Config",
                columns: table => new
                {
                    CompanyID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    DbName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Server = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    Password = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyDb", x => x.CompanyID);
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
                name: "Locale",
                schema: "Metadata",
                columns: table => new
                {
                    LocaleID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    LocalName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locale", x => x.LocaleID);
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
                name: "ReportView",
                schema: "Metadata",
                columns: table => new
                {
                    ViewID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportView", x => x.ViewID);
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
                name: "View",
                schema: "Metadata",
                columns: table => new
                {
                    ViewID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    EntityName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    EntityType = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    IsHierarchy = table.Column<bool>(type: "bit", nullable: false),
                    IsCartableIntegrated = table.Column<bool>(type: "bit", nullable: false),
                    FetchUrl = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    SearchUrl = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_View", x => x.ViewID);
                });

            migrationBuilder.CreateTable(
                name: "SystemError",
                schema: "Core",
                columns: table => new
                {
                    SystemErrorID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyId = table.Column<int>(type: "int", nullable: true),
                    FiscalPeriodId = table.Column<int>(type: "int", nullable: true),
                    BranchId = table.Column<int>(type: "int", nullable: true),
                    TimestampUtc = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Code = table.Column<int>(type: "int", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    FaultingMethod = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    FaultType = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    StackTrace = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Version = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    RowGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemError", x => x.SystemErrorID);
                    table.ForeignKey(
                        name: "FK_Core_SystemError_Config_Company",
                        column: x => x.CompanyId,
                        principalSchema: "Config",
                        principalTable: "CompanyDb",
                        principalColumn: "CompanyID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SysLogSetting",
                schema: "Config",
                columns: table => new
                {
                    SysLogSettingID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SourceId = table.Column<int>(type: "int", nullable: true),
                    EntityTypeId = table.Column<int>(type: "int", nullable: true),
                    OperationId = table.Column<int>(type: "int", nullable: false),
                    IsEnabled = table.Column<bool>(type: "bit", nullable: false),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysLogSetting", x => x.SysLogSettingID);
                    table.ForeignKey(
                        name: "FK_Config_LogSetting_Metadata_EntityType",
                        column: x => x.EntityTypeId,
                        principalSchema: "Metadata",
                        principalTable: "EntityType",
                        principalColumn: "EntityTypeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Config_LogSetting_Metadata_Operation",
                        column: x => x.OperationId,
                        principalSchema: "Metadata",
                        principalTable: "Operation",
                        principalColumn: "OperationID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Config_LogSetting_Metadata_Source",
                        column: x => x.SourceId,
                        principalSchema: "Metadata",
                        principalTable: "OperationSource",
                        principalColumn: "OperationSourceID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RoleCompany",
                schema: "Auth",
                columns: table => new
                {
                    RoleCompanyID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleCompany", x => x.RoleCompanyID);
                    table.ForeignKey(
                        name: "FK_RoleCompany_CompanyDb_CompanyId",
                        column: x => x.CompanyId,
                        principalSchema: "Config",
                        principalTable: "CompanyDb",
                        principalColumn: "CompanyID",
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
                name: "Report",
                schema: "Reporting",
                columns: table => new
                {
                    ReportID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedById = table.Column<int>(type: "int", nullable: false),
                    ParentId = table.Column<int>(type: "int", nullable: true),
                    ViewId = table.Column<int>(type: "int", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ServiceUrl = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    IsGroup = table.Column<bool>(type: "bit", nullable: false),
                    IsSystem = table.Column<bool>(type: "bit", nullable: false),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false),
                    IsDynamic = table.Column<bool>(type: "bit", nullable: false),
                    ResourceKeys = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubsystemId = table.Column<int>(type: "int", nullable: false),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Report", x => x.ReportID);
                    table.ForeignKey(
                        name: "FK_Reporting_Report_Auth_CreatedBy",
                        column: x => x.CreatedById,
                        principalSchema: "Auth",
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reporting_Report_Metadata_ReportView",
                        column: x => x.ViewId,
                        principalSchema: "Metadata",
                        principalTable: "ReportView",
                        principalColumn: "ViewID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reporting_Report_Reporting_Parent",
                        column: x => x.ParentId,
                        principalSchema: "Reporting",
                        principalTable: "Report",
                        principalColumn: "ReportID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Session",
                schema: "Auth",
                columns: table => new
                {
                    SessionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Device = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Browser = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Fingerprint = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    IPAddress = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: true),
                    SinceUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastActivityUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeZone = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Session", x => x.SessionID);
                    table.ForeignKey(
                        name: "FK_Auth_Session_Auth_User",
                        column: x => x.UserId,
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
                name: "Column",
                schema: "Metadata",
                columns: table => new
                {
                    ColumnID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    GroupName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    Type = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    DotNetType = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    StorageType = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    ScriptType = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Length = table.Column<int>(type: "int", nullable: false),
                    MinLength = table.Column<int>(type: "int", nullable: false),
                    IsFixedLength = table.Column<bool>(type: "bit", nullable: false),
                    IsDynamic = table.Column<bool>(type: "bit", nullable: false),
                    IsNullable = table.Column<bool>(type: "bit", nullable: false),
                    AllowSorting = table.Column<bool>(type: "bit", nullable: false),
                    AllowFiltering = table.Column<bool>(type: "bit", nullable: false),
                    Visibility = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    DisplayIndex = table.Column<short>(type: "smallint", nullable: false),
                    Expression = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    ViewID = table.Column<int>(type: "int", nullable: true),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Column", x => x.ColumnID);
                    table.ForeignKey(
                        name: "FK_Metadata_Column_Metadata_View",
                        column: x => x.ViewID,
                        principalSchema: "Metadata",
                        principalTable: "View",
                        principalColumn: "ViewID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SysOperationLog",
                schema: "Core",
                columns: table => new
                {
                    SysOperationLogID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OperationId = table.Column<int>(type: "int", nullable: false),
                    EntityTypeId = table.Column<int>(type: "int", nullable: true),
                    SourceId = table.Column<int>(type: "int", nullable: true),
                    SourceListId = table.Column<int>(type: "int", nullable: true),
                    CompanyId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Time = table.Column<TimeSpan>(type: "time", nullable: false),
                    EntityId = table.Column<int>(type: "int", nullable: true),
                    EntityCode = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EntityName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EntityDescription = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    EntityNo = table.Column<int>(type: "int", nullable: true),
                    EntityDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysOperationLog", x => x.SysOperationLogID);
                    table.ForeignKey(
                        name: "FK_Core_SysOperationLog_Auth_User",
                        column: x => x.UserId,
                        principalSchema: "Auth",
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Core_SysOperationLog_Config_Company",
                        column: x => x.CompanyId,
                        principalSchema: "Config",
                        principalTable: "CompanyDb",
                        principalColumn: "CompanyID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Core_SysOperationLog_Metadata_EntityType",
                        column: x => x.EntityTypeId,
                        principalSchema: "Metadata",
                        principalTable: "EntityType",
                        principalColumn: "EntityTypeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Core_SysOperationLog_Metadata_Operation",
                        column: x => x.OperationId,
                        principalSchema: "Metadata",
                        principalTable: "Operation",
                        principalColumn: "OperationID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Core_SysOperationLog_Metadata_Source",
                        column: x => x.SourceId,
                        principalSchema: "Metadata",
                        principalTable: "OperationSource",
                        principalColumn: "OperationSourceID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Core_SysOperationLog_Metadata_SourceList",
                        column: x => x.SourceListId,
                        principalSchema: "Metadata",
                        principalTable: "View",
                        principalColumn: "ViewID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SysOperationLogArchive",
                schema: "Core",
                columns: table => new
                {
                    SysOperationLogArchiveID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OperationId = table.Column<int>(type: "int", nullable: false),
                    EntityTypeId = table.Column<int>(type: "int", nullable: true),
                    SourceId = table.Column<int>(type: "int", nullable: true),
                    SourceListId = table.Column<int>(type: "int", nullable: true),
                    CompanyId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Time = table.Column<TimeSpan>(type: "time", nullable: false),
                    EntityId = table.Column<int>(type: "int", nullable: true),
                    EntityCode = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EntityName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EntityDescription = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    EntityNo = table.Column<int>(type: "int", nullable: true),
                    EntityDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysOperationLogArchive", x => x.SysOperationLogArchiveID);
                    table.ForeignKey(
                        name: "FK_Core_SysOperationLogArchive_Auth_User",
                        column: x => x.UserId,
                        principalSchema: "Auth",
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Core_SysOperationLogArchive_Config_Company",
                        column: x => x.CompanyId,
                        principalSchema: "Config",
                        principalTable: "CompanyDb",
                        principalColumn: "CompanyID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Core_SysOperationLogArchive_Metadata_EntityType",
                        column: x => x.EntityTypeId,
                        principalSchema: "Metadata",
                        principalTable: "EntityType",
                        principalColumn: "EntityTypeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Core_SysOperationLogArchive_Metadata_Operation",
                        column: x => x.OperationId,
                        principalSchema: "Metadata",
                        principalTable: "Operation",
                        principalColumn: "OperationID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Core_SysOperationLogArchive_Metadata_Source",
                        column: x => x.SourceId,
                        principalSchema: "Metadata",
                        principalTable: "OperationSource",
                        principalColumn: "OperationSourceID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Core_SysOperationLogArchive_Metadata_SourceList",
                        column: x => x.SourceListId,
                        principalSchema: "Metadata",
                        principalTable: "View",
                        principalColumn: "ViewID",
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
                        principalSchema: "Metadata",
                        principalTable: "View",
                        principalColumn: "ViewID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ValidRowPermission",
                schema: "Metadata",
                columns: table => new
                {
                    ValidRowPermissionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccessMode = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    ViewID = table.Column<int>(type: "int", nullable: true),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ValidRowPermission", x => x.ValidRowPermissionID);
                    table.ForeignKey(
                        name: "FK_Metadata_ValidRowPermission_Metadata_View",
                        column: x => x.ViewID,
                        principalSchema: "Metadata",
                        principalTable: "View",
                        principalColumn: "ViewID",
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
                        principalSchema: "Metadata",
                        principalTable: "View",
                        principalColumn: "ViewID",
                        onDelete: ReferentialAction.Restrict);
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
                name: "LocalReport",
                schema: "Reporting",
                columns: table => new
                {
                    LocalReportID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LocaleId = table.Column<int>(type: "int", nullable: false),
                    ReportId = table.Column<int>(type: "int", nullable: false),
                    Caption = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Template = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocalReport", x => x.LocalReportID);
                    table.ForeignKey(
                        name: "FK_Reporting_LocalReport_Reporting_Locale",
                        column: x => x.LocaleId,
                        principalSchema: "Metadata",
                        principalTable: "Locale",
                        principalColumn: "LocaleID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reporting_LocalReport_Reporting_Report",
                        column: x => x.ReportId,
                        principalSchema: "Reporting",
                        principalTable: "Report",
                        principalColumn: "ReportID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Parameter",
                schema: "Reporting",
                columns: table => new
                {
                    ParamID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReportId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    FieldName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Operator = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    DataType = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    ControlType = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Source = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    CaptionKey = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    DefaultValue = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    MinValue = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    MaxValue = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    DescriptionKey = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parameter", x => x.ParamID);
                    table.ForeignKey(
                        name: "FK_Reporting_Parameter_Reporting_Report",
                        column: x => x.ReportId,
                        principalSchema: "Reporting",
                        principalTable: "Report",
                        principalColumn: "ReportID",
                        onDelete: ReferentialAction.Cascade);
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
                name: "ShortcutCommand",
                schema: "Metadata",
                columns: table => new
                {
                    ShortcutCommandID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PermissionId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Scope = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    HotKey = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Method = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShortcutCommand", x => x.ShortcutCommandID);
                    table.ForeignKey(
                        name: "FK_Config_ShortcutCommand_Auth_Permission",
                        column: x => x.PermissionId,
                        principalSchema: "Auth",
                        principalTable: "Permission",
                        principalColumn: "PermissionID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SystemIssue",
                schema: "Reporting",
                columns: table => new
                {
                    SystemIssueID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParentId = table.Column<int>(type: "int", nullable: true),
                    PermissionID = table.Column<int>(type: "int", nullable: true),
                    ViewId = table.Column<int>(type: "int", nullable: true),
                    TitleKey = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    ApiUrl = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DeleteApiUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BranchScope = table.Column<bool>(type: "bit", nullable: false),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemIssue", x => x.SystemIssueID);
                    table.ForeignKey(
                        name: "FK_Reporting_SystemIssue_Auth_Permission",
                        column: x => x.PermissionID,
                        principalSchema: "Auth",
                        principalTable: "Permission",
                        principalColumn: "PermissionID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reporting_SystemIssue_Metadata_View",
                        column: x => x.ViewId,
                        principalSchema: "Metadata",
                        principalTable: "View",
                        principalColumn: "ViewID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reporting_SystemIssue_Reporting_Parent",
                        column: x => x.ParentId,
                        principalSchema: "Reporting",
                        principalTable: "SystemIssue",
                        principalColumn: "SystemIssueID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Column_ViewID",
                schema: "Metadata",
                table: "Column",
                column: "ViewID");

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
                name: "IX_LocalReport_LocaleId",
                schema: "Reporting",
                table: "LocalReport",
                column: "LocaleId");

            migrationBuilder.CreateIndex(
                name: "IX_LocalReport_ReportId",
                schema: "Reporting",
                table: "LocalReport",
                column: "ReportId");

            migrationBuilder.CreateIndex(
                name: "IX_Parameter_ReportId",
                schema: "Reporting",
                table: "Parameter",
                column: "ReportId");

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
                name: "IX_Report_CreatedById",
                schema: "Reporting",
                table: "Report",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Report_ParentId",
                schema: "Reporting",
                table: "Report",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Report_ViewId",
                schema: "Reporting",
                table: "Report",
                column: "ViewId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleCompany_CompanyId",
                schema: "Auth",
                table: "RoleCompany",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleCompany_RoleId",
                schema: "Auth",
                table: "RoleCompany",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermission_PermissionId",
                schema: "Auth",
                table: "RolePermission",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_Session_UserId",
                schema: "Auth",
                table: "Session",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Setting_ParentID",
                schema: "Config",
                table: "Setting",
                column: "ParentID");

            migrationBuilder.CreateIndex(
                name: "IX_ShortcutCommand_PermissionId",
                schema: "Metadata",
                table: "ShortcutCommand",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_SysLogSetting_EntityTypeId",
                schema: "Config",
                table: "SysLogSetting",
                column: "EntityTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_SysLogSetting_OperationId",
                schema: "Config",
                table: "SysLogSetting",
                column: "OperationId");

            migrationBuilder.CreateIndex(
                name: "IX_SysLogSetting_SourceId",
                schema: "Config",
                table: "SysLogSetting",
                column: "SourceId");

            migrationBuilder.CreateIndex(
                name: "IX_SysOperationLog_CompanyId",
                schema: "Core",
                table: "SysOperationLog",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_SysOperationLog_EntityTypeId",
                schema: "Core",
                table: "SysOperationLog",
                column: "EntityTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_SysOperationLog_OperationId",
                schema: "Core",
                table: "SysOperationLog",
                column: "OperationId");

            migrationBuilder.CreateIndex(
                name: "IX_SysOperationLog_SourceId",
                schema: "Core",
                table: "SysOperationLog",
                column: "SourceId");

            migrationBuilder.CreateIndex(
                name: "IX_SysOperationLog_SourceListId",
                schema: "Core",
                table: "SysOperationLog",
                column: "SourceListId");

            migrationBuilder.CreateIndex(
                name: "IX_SysOperationLog_UserId",
                schema: "Core",
                table: "SysOperationLog",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SysOperationLogArchive_CompanyId",
                schema: "Core",
                table: "SysOperationLogArchive",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_SysOperationLogArchive_EntityTypeId",
                schema: "Core",
                table: "SysOperationLogArchive",
                column: "EntityTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_SysOperationLogArchive_OperationId",
                schema: "Core",
                table: "SysOperationLogArchive",
                column: "OperationId");

            migrationBuilder.CreateIndex(
                name: "IX_SysOperationLogArchive_SourceId",
                schema: "Core",
                table: "SysOperationLogArchive",
                column: "SourceId");

            migrationBuilder.CreateIndex(
                name: "IX_SysOperationLogArchive_SourceListId",
                schema: "Core",
                table: "SysOperationLogArchive",
                column: "SourceListId");

            migrationBuilder.CreateIndex(
                name: "IX_SysOperationLogArchive_UserId",
                schema: "Core",
                table: "SysOperationLogArchive",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SystemError_CompanyId",
                schema: "Core",
                table: "SystemError",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_SystemIssue_ParentId",
                schema: "Reporting",
                table: "SystemIssue",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_SystemIssue_PermissionID",
                schema: "Reporting",
                table: "SystemIssue",
                column: "PermissionID");

            migrationBuilder.CreateIndex(
                name: "IX_SystemIssue_ViewId",
                schema: "Reporting",
                table: "SystemIssue",
                column: "ViewId");

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
                name: "IX_ValidRowPermission_ViewID",
                schema: "Metadata",
                table: "ValidRowPermission",
                column: "ViewID");

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Column",
                schema: "Metadata");

            migrationBuilder.DropTable(
                name: "Command",
                schema: "Metadata");

            migrationBuilder.DropTable(
                name: "LocalReport",
                schema: "Reporting");

            migrationBuilder.DropTable(
                name: "Parameter",
                schema: "Reporting");

            migrationBuilder.DropTable(
                name: "Person",
                schema: "Contact");

            migrationBuilder.DropTable(
                name: "RoleCompany",
                schema: "Auth");

            migrationBuilder.DropTable(
                name: "RolePermission",
                schema: "Auth");

            migrationBuilder.DropTable(
                name: "Session",
                schema: "Auth");

            migrationBuilder.DropTable(
                name: "ShortcutCommand",
                schema: "Metadata");

            migrationBuilder.DropTable(
                name: "SysLogSetting",
                schema: "Config");

            migrationBuilder.DropTable(
                name: "SysOperationLog",
                schema: "Core");

            migrationBuilder.DropTable(
                name: "SysOperationLogArchive",
                schema: "Core");

            migrationBuilder.DropTable(
                name: "SystemError",
                schema: "Core");

            migrationBuilder.DropTable(
                name: "SystemIssue",
                schema: "Reporting");

            migrationBuilder.DropTable(
                name: "UserRole",
                schema: "Auth");

            migrationBuilder.DropTable(
                name: "UserSetting",
                schema: "Config");

            migrationBuilder.DropTable(
                name: "ValidRowPermission",
                schema: "Metadata");

            migrationBuilder.DropTable(
                name: "ViewRowPermission",
                schema: "Auth");

            migrationBuilder.DropTable(
                name: "Locale",
                schema: "Metadata");

            migrationBuilder.DropTable(
                name: "Report",
                schema: "Reporting");

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
                name: "CompanyDb",
                schema: "Config");

            migrationBuilder.DropTable(
                name: "Permission",
                schema: "Auth");

            migrationBuilder.DropTable(
                name: "Setting",
                schema: "Config");

            migrationBuilder.DropTable(
                name: "Role",
                schema: "Auth");

            migrationBuilder.DropTable(
                name: "View",
                schema: "Metadata");

            migrationBuilder.DropTable(
                name: "User",
                schema: "Auth");

            migrationBuilder.DropTable(
                name: "ReportView",
                schema: "Metadata");

            migrationBuilder.DropTable(
                name: "PermissionGroup",
                schema: "Auth");

            migrationBuilder.DropTable(
                name: "OperationSourceType",
                schema: "Metadata");

            migrationBuilder.DropTable(
                name: "Subsystem",
                schema: "Metadata");
        }
    }
}
