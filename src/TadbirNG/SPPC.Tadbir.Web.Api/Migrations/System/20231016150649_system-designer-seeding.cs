using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SPPC.Tadbir.Web.Api.Migrations.System
{
    public partial class systemdesignerseeding : Migration
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
                    UserID = table.Column<int>(type: "int", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    UserID1 = table.Column<int>(type: "int", nullable: false),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Person", x => x.PersonID);
                    table.ForeignKey(
                        name: "FK_Contact_Person_Auth_User",
                        column: x => x.UserID1,
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

            migrationBuilder.InsertData(
                schema: "Auth",
                table: "User",
                columns: new[] { "UserID", "IsEnabled", "LastLoginDate", "ModifiedDate", "PasswordHash", "UserName" },
                values: new object[] { 1, true, new DateTime(2023, 10, 16, 8, 57, 46, 3, DateTimeKind.Unspecified), new DateTime(2023, 10, 16, 18, 36, 47, 637, DateTimeKind.Local).AddTicks(7610), "b22f213ec710f0b0e86297d10279d69171f50f01a04edf40f472a563e7ad8576", "admin" });

            migrationBuilder.InsertData(
                schema: "Metadata",
                table: "Column",
                columns: new[] { "ColumnID", "AllowFiltering", "AllowSorting", "DisplayIndex", "DotNetType", "Expression", "GroupName", "IsDynamic", "IsFixedLength", "IsNullable", "Length", "MinLength", "ModifiedDate", "Name", "rowguid", "ScriptType", "StorageType", "Type", "ViewID", "Visibility" },
                values: new object[,]
                {
                    { 604, true, true, (short)9, "System.DateTime", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "EntityDate", new Guid("d3af6a24-7407-41f4-a2f8-74da056c55c5"), "Date", "datetime", "Default", null, "Visible" },
                    { 605, true, true, (short)10, "System.String", "", "", false, false, false, 64, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "SourceName", new Guid("42670f08-cdc0-441b-85bf-4480518dca83"), "string", "nvarchar", "", null, "Visible" },
                    { 606, true, true, (short)11, "System.String", "", "", false, false, false, 64, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "SourceListName", new Guid("33d10f41-0bc7-464d-8ff4-f155e4b3dba5"), "string", "nvarchar", "", null, "Visible" },
                    { 607, true, true, (short)12, "System.String", "", "", false, false, false, 64, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "OperationName", new Guid("1c0080d7-ecce-40f8-9ce7-97b347b7f703"), "string", "nvarchar", "", null, "Visible" },
                    { 608, true, true, (short)13, "System.String", "", "", false, false, false, 64, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "Description", new Guid("0b26f9b9-01ee-49ed-a5f2-e92a57909597"), "string", "nvarchar", "", null, "Visible" },
                    { 609, true, true, (short)14, "System.String", "", "", false, false, false, 64, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "CompanyName", new Guid("6fac729f-cd32-47f2-bd84-06c90f84b6de"), "string", "nvarchar", "", null, "Visible" },
                    { 610, true, true, (short)-1, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "Id", new Guid("6be1103c-9c8e-45c4-9793-cb9927dc1600"), "number", "int", "", null, "AlwaysHidden" },
                    { 611, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "RowNo", new Guid("9115e91d-1434-4986-8915-9ea368f84fd6"), "number", "int", "", null, "AlwaysVisible" },
                    { 612, true, true, (short)1, "System.String", "", "", false, false, false, 64, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "UserName", new Guid("134df451-20ac-4f33-a208-7b5ec77b4bdd"), "string", "nvarchar", "", null, "Visible" },
                    { 613, true, true, (short)2, "System.Date", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "Date", new Guid("7a102610-d1d1-4cd0-9806-144f4825e415"), "Date", "datetime", "Default", null, "AlwaysVisible" },
                    { 614, true, true, (short)3, "System.TimeSpan", "", "", false, false, false, 7, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "Time", new Guid("82fca8bf-3499-4e08-9b9f-878c9fa51664"), "Date", "time", "", null, "AlwaysVisible" },
                    { 603, true, true, (short)8, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "EntityNo", new Guid("8f5855fa-0fb5-4fce-a301-ff803e545db3"), "number", "int", "", null, "Visible" },
                    { 615, true, true, (short)4, "System.String", "", "", false, false, false, 64, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "EntityTypeName", new Guid("a1f9227a-e37f-4b99-ad38-52cc8da4a7b1"), "string", "nvarchar", "", null, "Visible" },
                    { 617, true, true, (short)6, "System.String", "", "", false, false, false, 64, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "EntityName", new Guid("8f04fede-01e1-4988-a45d-d8c8ce0d041b"), "string", "nvarchar", "", null, "Visible" },
                    { 618, true, true, (short)7, "System.String", "", "", false, false, false, 64, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "EntityDescription", new Guid("5297ead1-5863-435c-817e-961be0f5ecb7"), "string", "nvarchar", "", null, "Visible" },
                    { 619, true, true, (short)8, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "EntityNo", new Guid("7a3b038f-913a-40ad-a467-a1040314c2c1"), "number", "int", "", null, "Visible" },
                    { 620, true, true, (short)9, "System.DateTime", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "EntityDate", new Guid("61fc3236-7d33-482b-ab1d-b42c366d1591"), "Date", "datetime", "Default", null, "Visible" },
                    { 621, true, true, (short)10, "System.String", "", "", false, false, false, 64, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "SourceName", new Guid("14b4294f-db5c-4794-b162-07ecbd1828a2"), "string", "nvarchar", "", null, "Visible" },
                    { 622, true, true, (short)11, "System.String", "", "", false, false, false, 64, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "SourceListName", new Guid("1c905c52-5679-4f00-bd56-fccd77ad3205"), "string", "nvarchar", "", null, "Visible" },
                    { 623, true, true, (short)12, "System.String", "", "", false, false, false, 64, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "OperationName", new Guid("585554ae-c7ac-4333-ba5a-c02b60b99884"), "string", "nvarchar", "", null, "Visible" },
                    { 624, true, true, (short)13, "System.String", "", "", false, false, false, 64, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "Description", new Guid("9a06f2c8-c5e2-409a-9661-102c8905bdbc"), "string", "nvarchar", "", null, "Visible" },
                    { 625, true, true, (short)14, "System.String", "", "", false, false, false, 64, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "CompanyName", new Guid("3335108f-6b08-46c7-84e2-2a756f61ff7a"), "string", "nvarchar", "", null, "Visible" },
                    { 626, true, true, (short)-1, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "Id", new Guid("1cfe3019-6e67-4277-b5f9-0ce6e9755881"), "number", "int", "", null, "AlwaysHidden" },
                    { 627, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "RowNo", new Guid("2997bdcc-362e-472f-ad1a-973b6ceba237"), "number", "int", "", null, "AlwaysVisible" },
                    { 616, true, true, (short)5, "System.String", "", "", false, false, false, 64, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "EntityCode", new Guid("664ac4ef-3448-4081-aa7c-58bd85d79d69"), "string", "nvarchar", "", null, "Visible" },
                    { 602, true, true, (short)7, "System.String", "", "", false, false, false, 64, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "EntityDescription", new Guid("e6c88232-3715-4942-af0f-0428e455e888"), "string", "nvarchar", "", null, "Visible" },
                    { 601, true, true, (short)6, "System.String", "", "", false, false, false, 64, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "EntityName", new Guid("8032ac2d-264c-4c8a-878e-fe2cabab0294"), "string", "nvarchar", "", null, "Visible" },
                    { 600, true, true, (short)5, "System.String", "", "", false, false, false, 64, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "EntityCode", new Guid("5d60d563-f669-4b23-a33f-68b065f59f4d"), "string", "nvarchar", "", null, "Visible" },
                    { 575, true, true, (short)3, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "DetailAccountFullCode", new Guid("f602cce4-968d-40d5-a023-c82a889f1ea6"), "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 576, true, true, (short)4, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "DetailAccountName", new Guid("1a46abd5-d966-4689-a21e-2c9e4fe51d7d"), "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 577, true, true, (short)5, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "CostCenterFullCode", new Guid("a47ec18a-56fa-40ed-bcca-2059427c6437"), "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 578, true, true, (short)6, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "CostCenterName", new Guid("89ea7784-37a3-4715-adb5-47e496b721e0"), "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 579, true, true, (short)7, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "ProjectFullCode", new Guid("ce199803-9a1e-4fdf-a936-e40a8edf277c"), "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 580, true, true, (short)8, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "ProjectName", new Guid("f479b983-540f-459a-bad1-5ad8c9ec4108"), "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 581, true, true, (short)9, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "AccountDescription", new Guid("3d017f84-1740-4443-82dc-7bcd806f5a75"), "string", "nvarchar", "", null, "Visible" },
                    { 582, true, true, (short)10, "System.Decimal", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "StartBalance", new Guid("9ca04081-8632-47da-baea-8a6b35bb2987"), "number", "money", "Money", null, "Visible" },
                    { 583, true, true, (short)11, "System.Decimal", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "Debit", new Guid("7d1e2817-9b63-47b8-bd57-e1b338288c26"), "number", "money", "Money", null, "Visible" },
                    { 584, true, true, (short)12, "System.Decimal", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "Credit", new Guid("096d6d9b-acfc-4b86-abbe-2ee44c10637e"), "number", "money", "Money", null, "Visible" },
                    { 585, true, true, (short)13, "System.Decimal", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "EndBalance", new Guid("c45c3220-d95f-4444-a44c-159c3239dab3"), "number", "money", "Money", null, "Visible" },
                    { 586, true, true, (short)14, "System.String", "", "", false, false, false, 128, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "BranchName", new Guid("88fc147b-b69f-4ad9-804a-92b690cc4924"), "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 587, true, true, (short)12, "System.String", "", "", false, false, false, 64, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "EntityReference", new Guid("db72e9d2-df5d-4f4d-a63b-c1ba10538783"), "string", "nvarchar", "", null, "Visible" }
                });

            migrationBuilder.InsertData(
                schema: "Metadata",
                table: "Column",
                columns: new[] { "ColumnID", "AllowFiltering", "AllowSorting", "DisplayIndex", "DotNetType", "Expression", "GroupName", "IsDynamic", "IsFixedLength", "IsNullable", "Length", "MinLength", "ModifiedDate", "Name", "rowguid", "ScriptType", "StorageType", "Type", "ViewID", "Visibility" },
                values: new object[,]
                {
                    { 588, true, true, (short)13, "System.String", "", "", false, false, false, 64, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "EntityAssociation", new Guid("31bea6c4-cfa4-4f6c-932c-07c79040a580"), "string", "nvarchar", "", null, "Visible" },
                    { 589, true, true, (short)14, "System.String", "", "", false, false, false, 64, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "SourceName", new Guid("f14d6db9-1238-4f6d-a677-ef8deb339935"), "string", "nvarchar", "", null, "Visible" },
                    { 590, true, true, (short)15, "System.String", "", "", false, false, false, 64, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "SourceListName", new Guid("e2098117-d4d1-48fd-ad9d-c04d62ae65d3"), "string", "nvarchar", "", null, "Visible" },
                    { 591, true, true, (short)16, "System.String", "", "", false, false, false, 64, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "OperationName", new Guid("29ca0abe-3c62-40b9-b892-714619fb8223"), "string", "nvarchar", "", null, "Visible" },
                    { 592, true, true, (short)17, "System.String", "", "", false, false, false, 64, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "Description", new Guid("0914db9e-72ff-4d8b-9b08-9caa26568062"), "string", "nvarchar", "", null, "Visible" },
                    { 593, true, true, (short)18, "System.String", "", "", false, false, false, 64, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "CompanyName", new Guid("9232d9cc-df93-438d-9791-ab84fbc2cbff"), "string", "nvarchar", "", null, "Visible" },
                    { 594, true, true, (short)-1, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "Id", new Guid("67eaf793-54c9-4d2a-b4b4-df1260c5f67d"), "number", "int", "", null, "AlwaysHidden" },
                    { 595, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "RowNo", new Guid("70ce50e8-000d-42ca-bf35-817543345f2b"), "number", "int", "", null, "AlwaysVisible" },
                    { 596, true, true, (short)1, "System.String", "", "", false, false, false, 64, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "UserName", new Guid("0cb4497c-7496-4005-ac59-370cfb35fc3b"), "string", "nvarchar", "", null, "Visible" },
                    { 597, true, true, (short)2, "System.Date", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "Date", new Guid("f8508e79-e382-4fb0-8460-31bb54e952ce"), "Date", "datetime", "Default", null, "AlwaysVisible" },
                    { 598, true, true, (short)3, "System.TimeSpan", "", "", false, false, false, 7, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "Time", new Guid("5a38d93d-71d2-427d-9328-482cafd84b0b"), "Date", "time", "", null, "AlwaysVisible" },
                    { 599, true, true, (short)4, "System.String", "", "", false, false, false, 64, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "EntityTypeName", new Guid("bda6f4d5-08fa-4c41-930f-b15f6ceef6ed"), "string", "nvarchar", "", null, "Visible" },
                    { 628, true, true, (short)1, "System.String", "", "", false, false, false, 64, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "UserName", new Guid("f5002804-97fd-469b-ac7c-0a6919675726"), "string", "nvarchar", "", null, "Visible" },
                    { 574, true, true, (short)2, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "AccountName", new Guid("db69fedc-5b0b-4c76-9cd7-33a71aea293e"), "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 629, true, true, (short)2, "System.String", "", "", false, false, false, 64, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "BranchName", new Guid("0274e90a-9317-43bc-96be-a318900eed6a"), "string", "nvarchar", "", null, "Visible" },
                    { 631, true, true, (short)4, "System.Date", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "Date", new Guid("73ccd1b3-cfea-40be-913e-27fe1caa6f3e"), "Date", "datetime", "Default", null, "AlwaysVisible" },
                    { 661, true, true, (short)0, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "No", new Guid("2085363e-c12f-4ae1-8939-edc8afb88858"), "number", "int", "", null, "AlwaysVisible" },
                    { 662, true, true, (short)1, "System.DateTime", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "Date", new Guid("f1e58eff-d5e1-4b32-bedb-e3c545b9090f"), "Date", "datetime", "Default", null, "AlwaysVisible" },
                    { 663, true, true, (short)2, "System.String", "", "", false, false, false, 128, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "Name", new Guid("c44f5465-9580-43e9-9339-863d980d0aeb"), "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 664, true, true, (short)3, "System.String", "", "", false, false, false, 128, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "FullCode", new Guid("82b8b4fa-90de-4124-ac39-d08451f50cae"), "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 665, true, true, (short)4, "System.String", "", "", false, false, false, 128, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "ErrorMessage", new Guid("91fec094-05cc-4d8a-b690-99541530e8a1"), "string", "nvarchar", "", null, "Visible" },
                    { 666, false, false, (short)0, "System.String", "", "", false, false, true, 64, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "Group", new Guid("f2aee14c-f6b9-4963-a7f5-b02c8e819818"), "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 667, false, false, (short)1, "System.String", "", "", false, false, true, 64, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "Account", new Guid("263dc79f-93a4-40a7-b6a4-e9a51cb5ff6e"), "string", "nvarchar", "", null, "Visible" },
                    { 668, false, false, (short)2, "System.Decimal", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "Balance", new Guid("9cd06c32-b68b-49d6-8a5f-374b8bed55a6"), "number", "money", "Money", null, "Visible" },
                    { 669, true, true, (short)15, "System.String", "", "", false, false, true, 64, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "OriginName", new Guid("71411d74-54a6-4913-a106-c06103cc95d2"), "string", "nvarchar", "", null, "Visible" },
                    { 670, false, false, (short)0, "System.String", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "Group", new Guid("5870eaed-56a2-44bf-96b3-c1b1d3875c21"), "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 671, false, false, (short)1, "System.String", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "Account", new Guid("25024f05-8102-4079-b515-81752a8d8689"), "string", "nvarchar", "", null, "Visible" },
                    { 660, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "Id", new Guid("4bfa130b-5145-449a-98f9-69929aff21c3"), "number", "int", "", null, "AlwaysHidden" },
                    { 672, false, false, (short)2, "System.Decimal", "", "", true, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "StartBalanceItem", new Guid("cf8b1d6e-9f54-42af-96f0-d70034f6e95d"), "number", "money", "Money", null, "Visible" },
                    { 674, false, false, (short)4, "System.Decimal", "", "", true, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "EndBalanceItem", new Guid("2257d7dd-3a81-4a29-93f5-8c0be8e5e7b6"), "number", "money", "Money", null, "Visible" },
                    { 675, false, false, (short)0, "System.String", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "Group", new Guid("fee74f37-6a21-4387-b763-4993104dcd6f"), "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 676, false, false, (short)1, "System.String", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "Account", new Guid("7483c179-d085-4e46-ab8f-957a75330f05"), "string", "nvarchar", "", null, "Visible" },
                    { 677, false, false, (short)2, "System.Decimal", "", "", true, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "BalanceItem", new Guid("becf0ee1-4741-4aae-8842-e51041480c12"), "number", "money", "Money", null, "Visible" },
                    { 678, false, false, (short)16, "System.String", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "TypeName", new Guid("649991da-2992-4f68-8a88-de8325cc9d9a"), "string", "nvarchar", "", null, "Visible" },
                    { 679, false, false, (short)0, "System.String", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "Assets", new Guid("b35eb415-589f-408f-9e62-705d57c6a384"), "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 680, false, false, (short)1, "System.Decimal", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "AssetsBalance", new Guid("8627abb3-d342-4a76-b2f1-a9093ed687b1"), "number", "money", "Money", null, "Visible" },
                    { 681, false, false, (short)2, "System.Decimal", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "AssetsPreviousBalance", new Guid("db0300f1-c652-4c0e-a78d-04534f7b2aa8"), "number", "money", "Money", null, "Visible" },
                    { 682, false, false, (short)3, "System.String", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "Liabilities", new Guid("3c59305a-92b3-4e0c-9247-4fe61029ba9e"), "string", "nvarchar", "", null, "Visible" },
                    { 683, false, false, (short)4, "System.Decimal", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "LiabilitiesBalance", new Guid("5454e220-151e-435e-9143-673abdfacad9"), "number", "money", "Money", null, "Visible" },
                    { 684, false, false, (short)5, "System.Decimal", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "LiabilitiesPreviousBalance", new Guid("a2a70021-3c17-4f06-8904-e9f88c0aec7b"), "number", "money", "Money", null, "Visible" },
                    { 673, false, false, (short)3, "System.Decimal", "", "", true, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "PeriodTurnoverItem", new Guid("1b3b5a16-99d4-4f16-a116-91759bcd18ef"), "number", "money", "Money", null, "Visible" },
                    { 659, true, true, (short)14, "System.String", "", "", false, false, true, 120, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "IssuerName", new Guid("5b5be959-c8d1-4cf6-8081-4f5cf5e27b7f"), "string", "nvarchar", "", null, "Visible" }
                });

            migrationBuilder.InsertData(
                schema: "Metadata",
                table: "Column",
                columns: new[] { "ColumnID", "AllowFiltering", "AllowSorting", "DisplayIndex", "DotNetType", "Expression", "GroupName", "IsDynamic", "IsFixedLength", "IsNullable", "Length", "MinLength", "ModifiedDate", "Name", "rowguid", "ScriptType", "StorageType", "Type", "ViewID", "Visibility" },
                values: new object[,]
                {
                    { 658, true, true, (short)13, "System.String", "", "", false, false, true, 120, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "BranchName", new Guid("84505d38-bc22-46a7-b84e-ea92519babf7"), "string", "nvarchar", "", null, "Visible" },
                    { 657, true, true, (short)12, "System.Boolean", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "IsApproved", new Guid("694ee892-4202-4eab-9324-7a14f8115d4b"), "boolean", "bit", "", null, "Visible" },
                    { 632, true, true, (short)5, "System.TimeSpan", "", "", false, false, false, 7, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "Time", new Guid("edd0c4c2-7cf3-4c58-b0f9-7469bf19e072"), "Date", "time", "", null, "AlwaysVisible" },
                    { 633, true, true, (short)6, "System.String", "", "", false, false, false, 64, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "EntityTypeName", new Guid("1255ace4-b660-4e2e-b04f-1a32a49f3dd0"), "string", "nvarchar", "", null, "Visible" },
                    { 634, true, true, (short)7, "System.String", "", "", false, false, false, 64, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "EntityCode", new Guid("b9a1c654-fd81-46d1-858c-7b61f1c56277"), "string", "nvarchar", "", null, "Visible" },
                    { 635, true, true, (short)8, "System.String", "", "", false, false, false, 64, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "EntityName", new Guid("400ae64e-b920-494b-9e70-bdb06cb9e0fe"), "string", "nvarchar", "", null, "Visible" },
                    { 636, true, true, (short)9, "System.String", "", "", false, false, false, 64, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "EntityDescription", new Guid("238de827-169f-461c-9191-e153de902119"), "string", "nvarchar", "", null, "Visible" },
                    { 637, true, true, (short)10, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "EntityNo", new Guid("5567ba0a-8774-4f83-87d5-3770cac0d9ca"), "number", "int", "", null, "Visible" },
                    { 638, true, true, (short)11, "System.DateTime", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "EntityDate", new Guid("d8c9b19d-789e-44c3-a8d2-7a76813c065d"), "Date", "datetime", "Default", null, "Visible" },
                    { 639, true, true, (short)12, "System.String", "", "", false, false, false, 64, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "EntityReference", new Guid("143d21e5-5ca3-4796-b321-1b60819f5de2"), "string", "nvarchar", "", null, "Visible" },
                    { 640, true, true, (short)13, "System.String", "", "", false, false, false, 64, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "EntityAssociation", new Guid("9d7e95c1-278d-45c9-a798-481dfb3b9968"), "string", "nvarchar", "", null, "Visible" },
                    { 641, true, true, (short)14, "System.String", "", "", false, false, false, 64, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "SourceName", new Guid("99f812a4-7d97-4384-bf68-0eb9af179fc8"), "string", "nvarchar", "", null, "Visible" },
                    { 642, true, true, (short)15, "System.String", "", "", false, false, false, 64, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "SourceListName", new Guid("2d6a99e8-07fc-40c3-997f-4a27af2e4fb2"), "string", "nvarchar", "", null, "Visible" },
                    { 643, true, true, (short)16, "System.String", "", "", false, false, false, 64, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "OperationName", new Guid("8208d270-5720-4de3-899c-72619891d119"), "string", "nvarchar", "", null, "Visible" },
                    { 644, true, true, (short)17, "System.String", "", "", false, false, false, 64, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "Description", new Guid("865ad1e0-b0d1-469f-bac8-a42442c3d6bb"), "string", "nvarchar", "", null, "Visible" },
                    { 645, true, true, (short)18, "System.String", "", "", false, false, false, 64, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "CompanyName", new Guid("b26a639f-699d-40d8-9015-50b71ea0631e"), "string", "nvarchar", "", null, "Visible" },
                    { 646, true, true, (short)-1, "System.String", "", "", false, false, false, 16, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "Password", new Guid("283ea924-06ee-4b21-bbbd-422a2ad00d73"), "string", "nvarchar", "", null, "AlwaysHidden" },
                    { 647, true, true, (short)8, "System.Boolean", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "IsBalanced", new Guid("33699804-80a4-4b75-83b2-59c1a1b66835"), "boolean", "bit", "", null, "Visible" },
                    { 648, true, true, (short)9, "System.String", "", "", false, false, true, 120, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "ConfirmerName", new Guid("c9d98311-249e-4a50-8375-b6ca9525e9e5"), "string", "nvarchar", "", null, "Visible" },
                    { 649, true, true, (short)10, "System.String", "", "", false, false, true, 120, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "ApproverName", new Guid("a0a16ef3-3fa0-4892-90f0-5af5dadb5c49"), "string", "nvarchar", "", null, "Visible" },
                    { 650, false, false, (short)0, "System.String", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "Group", new Guid("c234b134-d369-403a-861a-465a411ac419"), "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 651, false, false, (short)1, "System.String", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "Account", new Guid("01a733bc-b570-47cd-90a6-cf35364dc77d"), "string", "nvarchar", "", null, "Visible" },
                    { 652, false, false, (short)2, "System.Decimal", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "StartBalance", new Guid("7f17e561-a36b-4a9a-ab5e-c409e088f129"), "number", "money", "Money", null, "Visible" },
                    { 653, false, false, (short)3, "System.Decimal", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "PeriodTurnover", new Guid("3461678c-fd0a-4050-9c52-d6a4f24b40d4"), "number", "money", "Money", null, "Visible" },
                    { 654, false, false, (short)4, "System.Decimal", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "EndBalance", new Guid("be92326c-c0b9-4ba1-8f34-5c455c826062"), "number", "money", "Money", null, "Visible" },
                    { 655, false, false, (short)5, "System.Decimal", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "Balance", new Guid("0ae51ac5-30ad-4569-90c6-97aee4c00473"), "number", "money", "Money", null, "Hidden" },
                    { 656, true, true, (short)11, "System.Boolean", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "IsConfirmed", new Guid("1724c8cb-ff7c-4a21-9056-b859ae62151a"), "boolean", "bit", "", null, "Visible" },
                    { 630, true, true, (short)3, "System.String", "", "", false, false, false, 64, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "FiscalPeriodName", new Guid("7640827b-2e89-400d-87e4-0cd5d4f77c0e"), "string", "nvarchar", "", null, "Visible" },
                    { 685, true, true, (short)14, "System.String", "", "", false, false, false, 64, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "BranchName", new Guid("916adca4-ef5a-4f49-9c00-933cafc417ee"), "string", "nvarchar", "", null, "Hidden" },
                    { 573, true, true, (short)1, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "AccountFullCode", new Guid("af88d236-af9b-4fb8-88d5-1748318bb772"), "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 571, true, true, (short)-1, "System.String", "", "", false, false, true, 64, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "VoucherReference", new Guid("ea9a78c2-f3ff-473d-9367-7fc3a649e3e1"), "string", "nvarchar", "", null, "AlwaysHidden" },
                    { 489, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "RowNo", new Guid("96e49eaf-732b-4906-bcb4-2a66848f02fe"), "number", "int", "", null, "AlwaysVisible" },
                    { 490, true, true, (short)1, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "CostCenterFullCode", new Guid("3f8cea1f-8b45-4434-acc8-bee997f2e801"), "string", "nvarchar", "", null, "Visible" },
                    { 491, true, true, (short)2, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "CostCenterName", new Guid("106fd3d0-18ac-4740-a8b4-f7f363f23040"), "string", "nvarchar", "", null, "Visible" },
                    { 492, true, true, (short)3, "System.Decimal", "", "StartBalance", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "StartBalanceDebit", new Guid("c90bdb80-73dd-41bc-afa5-ceee33a0eb7c"), "number", "money", "Money", null, "Visible" },
                    { 493, true, true, (short)4, "System.Decimal", "", "StartBalance", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "StartBalanceCredit", new Guid("a0d43e18-ac95-43a9-828e-d2a2d0d09c50"), "number", "money", "Money", null, "Visible" },
                    { 494, true, true, (short)5, "System.Decimal", "", "Turnover", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "TurnoverDebit", new Guid("c03daab2-a344-4c50-a17d-55fbb0090c83"), "number", "money", "Money", null, "Visible" },
                    { 495, true, true, (short)6, "System.Decimal", "", "Turnover", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "TurnoverCredit", new Guid("95754dc4-9fac-47cd-9290-36527fc5b890"), "number", "money", "Money", null, "Visible" },
                    { 496, true, true, (short)7, "System.Decimal", "", "OperationSum", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "OperationSumDebit", new Guid("3ea19806-6d32-41b7-b462-f53732b2f7d4"), "number", "money", "Money", null, "Visible" },
                    { 497, true, true, (short)8, "System.Decimal", "", "OperationSum", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "OperationSumCredit", new Guid("a60e8f4b-9575-432b-affc-c1f18a61f269"), "number", "money", "Money", null, "Visible" },
                    { 498, true, true, (short)9, "System.Decimal", "", "EndBalance", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "EndBalanceDebit", new Guid("9fa849f7-e2f3-4487-abee-bfaa9dac7f1f"), "number", "money", "Money", null, "Visible" },
                    { 499, true, true, (short)10, "System.Decimal", "", "EndBalance", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "EndBalanceCredit", new Guid("7761e3c3-641e-4182-9c4e-3959829c5243"), "number", "money", "Money", null, "Visible" }
                });

            migrationBuilder.InsertData(
                schema: "Metadata",
                table: "Column",
                columns: new[] { "ColumnID", "AllowFiltering", "AllowSorting", "DisplayIndex", "DotNetType", "Expression", "GroupName", "IsDynamic", "IsFixedLength", "IsNullable", "Length", "MinLength", "ModifiedDate", "Name", "rowguid", "ScriptType", "StorageType", "Type", "ViewID", "Visibility" },
                values: new object[,]
                {
                    { 488, true, true, (short)-1, "System.String", "", "", false, false, true, 64, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "VoucherReference", new Guid("ec117be1-e851-4d8e-a2b4-473bc489c6ba"), "string", "nvarchar", "", null, "AlwaysHidden" },
                    { 500, true, true, (short)11, "System.String", "", "", false, false, false, 128, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "BranchName", new Guid("2fb1624e-2435-4185-832d-cedf298023eb"), "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 502, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "RowNo", new Guid("550d5697-082e-47f7-b8c7-48b9ef3705f4"), "number", "int", "", null, "AlwaysVisible" },
                    { 503, true, true, (short)1, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "CostCenterFullCode", new Guid("42ef3cab-a256-4448-8e0b-9fd54319fc8a"), "string", "nvarchar", "", null, "Visible" },
                    { 504, true, true, (short)2, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "CostCenterName", new Guid("57772ac5-ab59-45b1-b97a-f95454a30094"), "string", "nvarchar", "", null, "Visible" },
                    { 505, true, true, (short)3, "System.Decimal", "", "StartBalance", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "StartBalanceDebit", new Guid("18e193bb-684e-453b-9200-9df4dc312cf8"), "number", "money", "Money", null, "Visible" },
                    { 506, true, true, (short)4, "System.Decimal", "", "StartBalance", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "StartBalanceCredit", new Guid("e1beb20a-6d14-4f57-b649-dfd5ad17140f"), "number", "money", "Money", null, "Visible" },
                    { 507, true, true, (short)5, "System.Decimal", "", "Turnover", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "TurnoverDebit", new Guid("a07de6c2-07ce-4744-a44d-7a0780b58d18"), "number", "money", "Money", null, "Visible" },
                    { 508, true, true, (short)6, "System.Decimal", "", "Turnover", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "TurnoverCredit", new Guid("6cdad63a-1f90-4ae2-8352-7d2c3894a500"), "number", "money", "Money", null, "Visible" },
                    { 509, true, true, (short)7, "System.Decimal", "", "OperationSum", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "OperationSumDebit", new Guid("2740995c-8069-44ae-89a3-449e47842d82"), "number", "money", "Money", null, "Visible" },
                    { 510, true, true, (short)8, "System.Decimal", "", "OperationSum", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "OperationSumCredit", new Guid("df634c6e-c2eb-41e2-997a-ff5fbb85dec8"), "number", "money", "Money", null, "Visible" },
                    { 511, true, true, (short)9, "System.Decimal", "", "Corrections", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "CorrectionsDebit", new Guid("a6f87691-56da-4883-b03f-76db38026727"), "number", "money", "Money", null, "Visible" },
                    { 512, true, true, (short)10, "System.Decimal", "", "Corrections", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "CorrectionsCredit", new Guid("3fb23a81-f894-4024-b9b5-c34b7e578ad8"), "number", "money", "Money", null, "Visible" },
                    { 501, true, true, (short)-1, "System.String", "", "", false, false, true, 64, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "VoucherReference", new Guid("117e7989-b4cc-43e6-96cd-5dfe77aa257b"), "string", "nvarchar", "", null, "AlwaysHidden" },
                    { 487, true, true, (short)9, "System.String", "", "", false, false, false, 128, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "BranchName", new Guid("ca7e5a78-77cf-4a11-b621-db8beb1ea036"), "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 486, true, true, (short)8, "System.Decimal", "", "EndBalance", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "EndBalanceCredit", new Guid("bdc11698-beec-4938-a43a-8d17386626ae"), "number", "money", "Money", null, "Visible" },
                    { 485, true, true, (short)7, "System.Decimal", "", "EndBalance", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "EndBalanceDebit", new Guid("f2b84610-3c23-401f-bd5c-0ab43a787b0c"), "number", "money", "Money", null, "Visible" },
                    { 460, true, true, (short)13, "System.String", "", "", false, false, false, 128, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "BranchName", new Guid("20ab7d80-7b18-43ae-a72d-2fe98c2317a3"), "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 461, true, true, (short)-1, "System.String", "", "", false, false, true, 64, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "VoucherReference", new Guid("6f8af5d4-d64d-46cb-b133-3eea804d493e"), "string", "nvarchar", "", null, "AlwaysHidden" },
                    { 462, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "RowNo", new Guid("2e65bf36-ea57-4852-9323-bc1013410390"), "number", "int", "", null, "AlwaysVisible" },
                    { 463, true, true, (short)1, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "CostCenterFullCode", new Guid("d03a36ca-e863-4fe8-8d19-b7986b6d938a"), "string", "nvarchar", "", null, "Visible" },
                    { 464, true, true, (short)2, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "CostCenterName", new Guid("395dd4fd-2e1f-42b8-8573-c72dc1e50de6"), "string", "nvarchar", "", null, "Visible" },
                    { 465, true, true, (short)3, "System.Decimal", "", "EndBalance", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "EndBalanceDebit", new Guid("aeebc808-2dca-4018-83a7-d56b5a67e75a"), "number", "money", "Money", null, "Visible" },
                    { 466, true, true, (short)4, "System.Decimal", "", "EndBalance", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "EndBalanceCredit", new Guid("7a0902e6-2ce0-4613-bb0e-9617ae7fe90e"), "number", "money", "Money", null, "Visible" },
                    { 467, true, true, (short)5, "System.String", "", "", false, false, false, 128, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "BranchName", new Guid("751fa02e-00af-472d-88bf-7b26d2b84ba1"), "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 468, true, true, (short)-1, "System.String", "", "", false, false, true, 64, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "VoucherReference", new Guid("6dc2f3ff-92ff-47e8-928b-05ba0c890f3d"), "string", "nvarchar", "", null, "AlwaysHidden" },
                    { 469, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "RowNo", new Guid("2de44b84-0de3-4672-8b34-f5afed071955"), "number", "int", "", null, "AlwaysVisible" },
                    { 470, true, true, (short)1, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "CostCenterFullCode", new Guid("f669b153-ebe4-4ee2-a472-26b2d4e5aae2"), "string", "nvarchar", "", null, "Visible" },
                    { 471, true, true, (short)2, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "CostCenterName", new Guid("6dc9f0d6-ed32-4dbb-be5d-58a406478746"), "string", "nvarchar", "", null, "Visible" },
                    { 472, true, true, (short)3, "System.Decimal", "", "Turnover", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "TurnoverDebit", new Guid("f0918fcd-0c44-4204-8ed2-654a1f881c50"), "number", "money", "Money", null, "Visible" },
                    { 473, true, true, (short)4, "System.Decimal", "", "Turnover", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "TurnoverCredit", new Guid("de3d3588-36ec-48a0-890a-a197e1209e4c"), "number", "money", "Money", null, "Visible" },
                    { 474, true, true, (short)5, "System.Decimal", "", "EndBalance", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "EndBalanceDebit", new Guid("1795be4f-ccac-4aa2-944e-5807bd9a6660"), "number", "money", "Money", null, "Visible" },
                    { 475, true, true, (short)6, "System.Decimal", "", "EndBalance", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "EndBalanceCredit", new Guid("76f551cc-98c2-4e8a-96c5-43cd39bd0881"), "number", "money", "Money", null, "Visible" },
                    { 476, true, true, (short)7, "System.String", "", "", false, false, false, 128, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "BranchName", new Guid("3f27148c-638c-41cc-903b-374fd83890cb"), "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 477, true, true, (short)-1, "System.String", "", "", false, false, true, 64, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "VoucherReference", new Guid("292197f2-86ed-400e-8f4f-72f705ee9e21"), "string", "nvarchar", "", null, "AlwaysHidden" },
                    { 478, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "RowNo", new Guid("a6d5e289-ebec-4515-b3e9-5e76d92f863d"), "number", "int", "", null, "AlwaysVisible" },
                    { 479, true, true, (short)1, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "CostCenterFullCode", new Guid("a78d73b1-8d8d-4a0a-8764-6382fb35645f"), "string", "nvarchar", "", null, "Visible" },
                    { 480, true, true, (short)2, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "CostCenterName", new Guid("67d0953e-7d13-4e35-ac6b-52bb40cf56bf"), "string", "nvarchar", "", null, "Visible" },
                    { 481, true, true, (short)3, "System.Decimal", "", "StartBalance", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "StartBalanceDebit", new Guid("17d42719-2c03-4770-9ce4-8cce38629328"), "number", "money", "Money", null, "Visible" },
                    { 482, true, true, (short)4, "System.Decimal", "", "StartBalance", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "StartBalanceCredit", new Guid("59d86c45-eb8a-46dd-84e0-818c3dc44bec"), "number", "money", "Money", null, "Visible" },
                    { 483, true, true, (short)5, "System.Decimal", "", "Turnover", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "TurnoverDebit", new Guid("34a3dd82-b57e-4711-8982-1edca48a9e8e"), "number", "money", "Money", null, "Visible" },
                    { 484, true, true, (short)6, "System.Decimal", "", "Turnover", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "TurnoverCredit", new Guid("3e97209b-6b0a-42f9-b39e-89a554ab5f33"), "number", "money", "Money", null, "Visible" }
                });

            migrationBuilder.InsertData(
                schema: "Metadata",
                table: "Column",
                columns: new[] { "ColumnID", "AllowFiltering", "AllowSorting", "DisplayIndex", "DotNetType", "Expression", "GroupName", "IsDynamic", "IsFixedLength", "IsNullable", "Length", "MinLength", "ModifiedDate", "Name", "rowguid", "ScriptType", "StorageType", "Type", "ViewID", "Visibility" },
                values: new object[,]
                {
                    { 513, true, true, (short)11, "System.Decimal", "", "EndBalance", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "EndBalanceDebit", new Guid("aaaf5bf7-6edd-4a73-9ebf-5b0561281f60"), "number", "money", "Money", null, "Visible" },
                    { 572, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "RowNo", new Guid("2cfaa85c-26da-4bd7-8f9c-3d45c011fb9f"), "number", "int", "", null, "AlwaysVisible" },
                    { 514, true, true, (short)12, "System.Decimal", "", "EndBalance", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "EndBalanceCredit", new Guid("b4676aa8-ea48-4bd9-ba07-74a73c57d9c1"), "number", "money", "Money", null, "Visible" },
                    { 516, true, true, (short)-1, "System.String", "", "", false, false, true, 64, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "VoucherReference", new Guid("266e2174-820d-41e3-b1e5-c50900fd790c"), "string", "nvarchar", "", null, "AlwaysHidden" },
                    { 547, true, true, (short)3, "System.Decimal", "", "StartBalance", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "StartBalanceDebit", new Guid("33ee1f0d-3054-4dca-8051-74abc2797f79"), "number", "money", "Money", null, "Visible" },
                    { 548, true, true, (short)4, "System.Decimal", "", "StartBalance", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "StartBalanceCredit", new Guid("77dfacaa-df77-400f-a8da-8803392abdcd"), "number", "money", "Money", null, "Visible" },
                    { 549, true, true, (short)5, "System.Decimal", "", "Turnover", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "TurnoverDebit", new Guid("d141ede5-614f-4673-9c23-b0788e38f4b7"), "number", "money", "Money", null, "Visible" },
                    { 550, true, true, (short)6, "System.Decimal", "", "Turnover", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "TurnoverCredit", new Guid("053e3e5c-a753-4672-a6c8-b24fa08189fa"), "number", "money", "Money", null, "Visible" },
                    { 551, true, true, (short)7, "System.Decimal", "", "OperationSum", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "OperationSumDebit", new Guid("f542fec4-d92c-4860-80b0-a561824dbf8b"), "number", "money", "Money", null, "Visible" },
                    { 552, true, true, (short)8, "System.Decimal", "", "OperationSum", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "OperationSumCredit", new Guid("7115dfbb-c34e-41c1-b95a-b3856c9de2aa"), "number", "money", "Money", null, "Visible" },
                    { 553, true, true, (short)9, "System.Decimal", "", "EndBalance", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "EndBalanceDebit", new Guid("76db4c60-2ecf-44b4-a050-e2edca5113ca"), "number", "money", "Money", null, "Visible" },
                    { 554, true, true, (short)10, "System.Decimal", "", "EndBalance", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "EndBalanceCredit", new Guid("be892f15-e867-45fd-904f-8291c4435e15"), "number", "money", "Money", null, "Visible" },
                    { 555, true, true, (short)11, "System.String", "", "", false, false, false, 128, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "BranchName", new Guid("06217d35-7995-47e3-a825-b98336d483dc"), "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 556, true, true, (short)-1, "System.String", "", "", false, false, true, 64, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "VoucherReference", new Guid("e7943f3b-ae66-4579-9dbf-f8bc9083d5e6"), "string", "nvarchar", "", null, "AlwaysHidden" },
                    { 557, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "RowNo", new Guid("a3be0b47-43b6-4631-a7f8-134f7ed360a7"), "number", "int", "", null, "AlwaysVisible" },
                    { 546, true, true, (short)2, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "ProjectName", new Guid("3accd0c0-7f79-41d6-bfd5-dd1eda85b29e"), "string", "nvarchar", "", null, "Visible" },
                    { 558, true, true, (short)1, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "ProjectFullCode", new Guid("a639e57c-3c58-492f-8492-b83c7b3eecc6"), "string", "nvarchar", "", null, "Visible" },
                    { 560, true, true, (short)3, "System.Decimal", "", "StartBalance", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "StartBalanceDebit", new Guid("679f8425-01a7-4870-bcd2-74b0e245e7eb"), "number", "money", "Money", null, "Visible" },
                    { 561, true, true, (short)4, "System.Decimal", "", "StartBalance", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "StartBalanceCredit", new Guid("c121c853-7233-46b5-9fe6-3aa610ffa85f"), "number", "money", "Money", null, "Visible" },
                    { 562, true, true, (short)5, "System.Decimal", "", "Turnover", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "TurnoverDebit", new Guid("801c0bdb-d2f7-4570-b129-f811591884b2"), "number", "money", "Money", null, "Visible" },
                    { 563, true, true, (short)6, "System.Decimal", "", "Turnover", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "TurnoverCredit", new Guid("33fca627-6382-4432-8237-b5751149e52d"), "number", "money", "Money", null, "Visible" },
                    { 564, true, true, (short)7, "System.Decimal", "", "OperationSum", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "OperationSumDebit", new Guid("261e345c-30e3-4616-afa3-03cf71b78be0"), "number", "money", "Money", null, "Visible" },
                    { 565, true, true, (short)8, "System.Decimal", "", "OperationSum", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "OperationSumCredit", new Guid("430ba2da-34cc-4e50-81b6-4500369b1eaf"), "number", "money", "Money", null, "Visible" },
                    { 566, true, true, (short)9, "System.Decimal", "", "Corrections", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "CorrectionsDebit", new Guid("c1facc0d-b333-4066-a7b5-0373a3e8c3e0"), "number", "money", "Money", null, "Visible" },
                    { 567, true, true, (short)10, "System.Decimal", "", "Corrections", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "CorrectionsCredit", new Guid("6370da9b-ceb3-4e82-88e6-13a0f5021600"), "number", "money", "Money", null, "Visible" },
                    { 568, true, true, (short)11, "System.Decimal", "", "EndBalance", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "EndBalanceDebit", new Guid("89b0ba24-feec-4640-b09c-addc9dfe2cc3"), "number", "money", "Money", null, "Visible" },
                    { 569, true, true, (short)12, "System.Decimal", "", "EndBalance", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "EndBalanceCredit", new Guid("61c7ba1c-384e-4252-bb12-460e9be88e46"), "number", "money", "Money", null, "Visible" },
                    { 570, true, true, (short)13, "System.String", "", "", false, false, false, 128, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "BranchName", new Guid("2c7990da-3ff6-4e20-a5dd-b6008a76051f"), "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 559, true, true, (short)2, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "ProjectName", new Guid("833a5c60-0786-43b7-b092-a3f01f501e4a"), "string", "nvarchar", "", null, "Visible" },
                    { 545, true, true, (short)1, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "ProjectFullCode", new Guid("e1cf5604-34de-4747-8123-1903caddac08"), "string", "nvarchar", "", null, "Visible" },
                    { 544, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "RowNo", new Guid("83eb16c3-66a7-4ff1-ac33-b106ff1295d8"), "number", "int", "", null, "AlwaysVisible" },
                    { 543, true, true, (short)-1, "System.String", "", "", false, false, true, 64, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "VoucherReference", new Guid("c4326b48-0c40-48f3-ac58-a181c690cf2a"), "string", "nvarchar", "", null, "AlwaysHidden" },
                    { 517, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "RowNo", new Guid("875d29ac-949e-4c2b-b885-7372a30ce8ff"), "number", "int", "", null, "AlwaysVisible" },
                    { 518, true, true, (short)1, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "ProjectFullCode", new Guid("b2432848-0b64-4ac8-8665-6c59f431058d"), "string", "nvarchar", "", null, "Visible" },
                    { 519, true, true, (short)2, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "ProjectName", new Guid("cde58f4b-04a4-4c61-8e55-6cb7d7197afb"), "string", "nvarchar", "", null, "Visible" },
                    { 520, true, true, (short)3, "System.Decimal", "", "EndBalance", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "EndBalanceDebit", new Guid("c52526bc-01aa-4c6b-a539-e0beccdf5198"), "number", "money", "Money", null, "Visible" },
                    { 521, true, true, (short)4, "System.Decimal", "", "EndBalance", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "EndBalanceCredit", new Guid("d87ecf4c-64ab-4ba3-b541-807ae99c4334"), "number", "money", "Money", null, "Visible" },
                    { 522, true, true, (short)5, "System.String", "", "", false, false, false, 128, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "BranchName", new Guid("2d9b8e7e-cd18-43b4-b2f9-81c8cb532a5b"), "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 524, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "RowNo", new Guid("dc2b24ff-7477-4ecc-890d-398aa0aa7e8e"), "number", "int", "", null, "AlwaysVisible" },
                    { 525, true, true, (short)1, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "ProjectFullCode", new Guid("cfbd6a0c-2aca-4081-83f7-1179217b0760"), "string", "nvarchar", "", null, "Visible" },
                    { 526, true, true, (short)2, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "ProjectName", new Guid("91002243-d4b3-49d4-927e-3f4fb5a3d258"), "string", "nvarchar", "", null, "Visible" },
                    { 527, true, true, (short)3, "System.Decimal", "", "Turnover", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "TurnoverDebit", new Guid("777af041-1aff-49b1-aa49-2f8581b6ccee"), "number", "money", "Money", null, "Visible" }
                });

            migrationBuilder.InsertData(
                schema: "Metadata",
                table: "Column",
                columns: new[] { "ColumnID", "AllowFiltering", "AllowSorting", "DisplayIndex", "DotNetType", "Expression", "GroupName", "IsDynamic", "IsFixedLength", "IsNullable", "Length", "MinLength", "ModifiedDate", "Name", "rowguid", "ScriptType", "StorageType", "Type", "ViewID", "Visibility" },
                values: new object[,]
                {
                    { 528, true, true, (short)4, "System.Decimal", "", "Turnover", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "TurnoverCredit", new Guid("9387750c-a2ca-4da5-aabd-dd3d6ac5e3a9"), "number", "money", "Money", null, "Visible" },
                    { 529, true, true, (short)5, "System.Decimal", "", "EndBalance", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "EndBalanceDebit", new Guid("219d1335-46ff-47af-b1d6-e02c83563bc4"), "number", "money", "Money", null, "Visible" },
                    { 530, true, true, (short)6, "System.Decimal", "", "EndBalance", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "EndBalanceCredit", new Guid("1a749a93-01df-499d-8a9f-d39ccf5dc2d9"), "number", "money", "Money", null, "Visible" },
                    { 531, true, true, (short)7, "System.String", "", "", false, false, false, 128, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "BranchName", new Guid("75d3e729-55da-4143-aaa7-9c74bcc06d34"), "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 532, true, true, (short)-1, "System.String", "", "", false, false, true, 64, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "VoucherReference", new Guid("e7178c36-16ce-4282-ab47-8e7b03f5b36e"), "string", "nvarchar", "", null, "AlwaysHidden" },
                    { 533, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "RowNo", new Guid("411691e8-2bd9-4512-856a-bacf320d89ba"), "number", "int", "", null, "AlwaysVisible" },
                    { 534, true, true, (short)1, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "ProjectFullCode", new Guid("4ced86a8-68e2-4880-b5d5-15805eb2a035"), "string", "nvarchar", "", null, "Visible" },
                    { 535, true, true, (short)2, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "ProjectName", new Guid("e63338a5-2077-4ec2-a407-6c492e0fb633"), "string", "nvarchar", "", null, "Visible" },
                    { 536, true, true, (short)3, "System.Decimal", "", "StartBalance", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "StartBalanceDebit", new Guid("8365d94a-5a1d-4c20-b09d-f2fec4550ad6"), "number", "money", "Money", null, "Visible" },
                    { 537, true, true, (short)4, "System.Decimal", "", "StartBalance", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "StartBalanceCredit", new Guid("7ab02f28-9808-493b-a250-d9232c0f519e"), "number", "money", "Money", null, "Visible" },
                    { 538, true, true, (short)5, "System.Decimal", "", "Turnover", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "TurnoverDebit", new Guid("71fdffb2-4bee-406c-9d52-72e93ccb30a9"), "number", "money", "Money", null, "Visible" },
                    { 539, true, true, (short)6, "System.Decimal", "", "Turnover", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "TurnoverCredit", new Guid("223c242b-6708-4d54-bf4c-70f303aa98fa"), "number", "money", "Money", null, "Visible" },
                    { 540, true, true, (short)7, "System.Decimal", "", "EndBalance", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "EndBalanceDebit", new Guid("3ab7c8f4-3e7d-4be7-977f-2bb6492905d9"), "number", "money", "Money", null, "Visible" },
                    { 541, true, true, (short)8, "System.Decimal", "", "EndBalance", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "EndBalanceCredit", new Guid("ea9c6a05-f184-4936-986c-da7123418285"), "number", "money", "Money", null, "Visible" },
                    { 542, true, true, (short)9, "System.String", "", "", false, false, false, 128, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "BranchName", new Guid("59d8ebba-ae93-40f5-acc7-44d7cb452e34"), "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 515, true, true, (short)13, "System.String", "", "", false, false, false, 128, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "BranchName", new Guid("b0cb4fe9-34a7-4541-8325-04e3c4ad0302"), "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 459, true, true, (short)12, "System.Decimal", "", "EndBalance", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "EndBalanceCredit", new Guid("2a8db45f-564a-4191-bc97-e4527fdecaec"), "number", "money", "Money", null, "Visible" },
                    { 686, true, true, (short)5, "System.String", "", "", false, false, false, 128, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "BranchName", new Guid("cc46e6d6-cd21-46d6-9f1b-6b80d89c7086"), "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 688, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "TypeId", new Guid("1377c08f-5ddb-4fa8-b4f8-dcf265da029b"), "number", "int", "", null, "AlwaysHidden" },
                    { 833, true, true, (short)11, "System.String", "", "", false, false, true, 64, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "ModifiedByName", new Guid("a8d97382-a023-4c68-9ae5-910e20b8f4d8"), "string", "nvarchar", "", null, "Hidden" },
                    { 834, true, true, (short)12, "System.DateTime", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "ModifiedDate", new Guid("e2e1fd36-faf9-4a46-8cfb-08a5bfe3ed1c"), "Date", "datetime", "Default", null, "Hidden" },
                    { 835, true, true, (short)8, "System.String", "", "", false, false, true, 64, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "CreatedByName", new Guid("57f52f0a-6275-4322-a73f-137aa2fc04ec"), "string", "nvarchar", "", null, "Hidden" },
                    { 836, true, true, (short)9, "System.DateTime", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "CreatedDate", new Guid("76123cec-13c7-49e9-911d-9d8a080dae8d"), "Date", "datetime", "Default", null, "Hidden" },
                    { 837, true, true, (short)10, "System.String", "", "", false, false, true, 64, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "ModifiedByName", new Guid("be0a8329-ae2f-4525-8c31-c9eb50c8ac9f"), "string", "nvarchar", "", null, "Hidden" },
                    { 838, true, true, (short)11, "System.DateTime", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "ModifiedDate", new Guid("7d3b41c7-e915-49a1-b297-31aa11a19e5b"), "Date", "datetime", "Default", null, "Hidden" },
                    { 839, true, true, (short)9, "System.String", "", "", false, false, true, 64, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "CreatedByName", new Guid("5b03c0a4-645e-4d63-b1ae-1fb72a41a444"), "string", "nvarchar", "", null, "Hidden" },
                    { 840, true, true, (short)10, "System.DateTime", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "CreatedDate", new Guid("530e1041-90a0-4159-b6c9-b98c60380ae6"), "Date", "datetime", "Default", null, "Hidden" },
                    { 841, true, true, (short)11, "System.String", "", "", false, false, true, 64, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "ModifiedByName", new Guid("3b3e6ac1-4b2e-4190-9687-54198e004ead"), "string", "nvarchar", "", null, "Hidden" },
                    { 842, true, true, (short)12, "System.DateTime", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "ModifiedDate", new Guid("4630a32e-e301-4007-9d0d-5efdb6b185d5"), "Date", "datetime", "Default", null, "Hidden" },
                    { 843, true, true, (short)5, "System.String", "", "", false, false, true, 64, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "CreatedByName", new Guid("28f2892b-0bb2-4fc4-82d1-8a9dbde1e857"), "string", "nvarchar", "", null, "Hidden" },
                    { 832, true, true, (short)10, "System.DateTime", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "CreatedDate", new Guid("bc231a20-5d8d-470c-a58b-b5ca165ec13c"), "Date", "datetime", "Default", null, "Hidden" },
                    { 844, true, true, (short)6, "System.DateTime", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "CreatedDate", new Guid("8d0c11da-4a14-4436-9e99-20b12272dd5c"), "Date", "datetime", "Default", null, "Hidden" },
                    { 846, true, true, (short)8, "System.DateTime", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "ModifiedDate", new Guid("c251ad78-d33a-451d-abd9-0d311f93b17f"), "Date", "datetime", "Default", null, "Hidden" },
                    { 847, true, true, (short)5, "System.String", "", "", false, false, true, 64, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "CreatedByName", new Guid("78052f2b-c991-46fe-8d3b-65d8a8a60740"), "string", "nvarchar", "", null, "Hidden" },
                    { 848, true, true, (short)6, "System.DateTime", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "CreatedDate", new Guid("ceac73c1-a20e-4bed-8206-37ffbf0218d8"), "Date", "datetime", "Default", null, "Hidden" },
                    { 849, true, true, (short)7, "System.String", "", "", false, false, true, 64, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "ModifiedByName", new Guid("b9633dea-d05f-4923-999d-c2cefc31fefe"), "string", "nvarchar", "", null, "Hidden" },
                    { 850, true, true, (short)8, "System.DateTime", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "ModifiedDate", new Guid("111d3571-0afd-4d2f-be34-0aa4f4ea3788"), "Date", "datetime", "Default", null, "Hidden" },
                    { 851, true, true, (short)5, "System.String", "", "", false, false, true, 64, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "CreatedByName", new Guid("4b691ce8-eaeb-4462-bb73-f9dc93dc808e"), "string", "nvarchar", "", null, "Hidden" },
                    { 852, true, true, (short)6, "System.DateTime", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "CreatedDate", new Guid("818bf4cb-5559-4c83-b8ef-cd4eeaf03b57"), "Date", "datetime", "Default", null, "Hidden" },
                    { 853, true, true, (short)7, "System.String", "", "", false, false, true, 64, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "ModifiedByName", new Guid("8b38c1c9-0ddf-4af5-9419-b1360375e833"), "string", "nvarchar", "", null, "Hidden" },
                    { 854, true, true, (short)8, "System.DateTime", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "ModifiedDate", new Guid("7362de1c-69a4-4128-a1ed-ec3c19b58384"), "Date", "datetime", "Default", null, "Hidden" },
                    { 855, true, true, (short)9, "System.String", "", "", false, false, false, 128, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "SourceAppName", new Guid("b8a8c74b-7071-49b3-9674-200f52d64fcb"), "string", "nvarchar", "", null, "Hidden" }
                });

            migrationBuilder.InsertData(
                schema: "Metadata",
                table: "Column",
                columns: new[] { "ColumnID", "AllowFiltering", "AllowSorting", "DisplayIndex", "DotNetType", "Expression", "GroupName", "IsDynamic", "IsFixedLength", "IsNullable", "Length", "MinLength", "ModifiedDate", "Name", "rowguid", "ScriptType", "StorageType", "Type", "ViewID", "Visibility" },
                values: new object[,]
                {
                    { 856, true, true, (short)-1, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "SourceAppId", new Guid("e4c01dd1-9e82-4c16-bb37-94b447c585d2"), "number", "int", "", null, "AlwaysHidden" },
                    { 845, true, true, (short)7, "System.String", "", "", false, false, true, 64, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "ModifiedByName", new Guid("84ebe156-7f53-4f35-8534-3a9ce9a06d1a"), "string", "nvarchar", "", null, "Hidden" },
                    { 831, true, true, (short)9, "System.String", "", "", false, false, true, 64, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "CreatedByName", new Guid("306fc26f-f15f-4b22-a836-27089b969794"), "string", "nvarchar", "", null, "Hidden" },
                    { 830, true, true, (short)8, "System.DateTime", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "ModifiedDate", new Guid("062f6b0b-1b38-4b97-977b-0f3cfed34b6c"), "Date", "datetime", "Default", null, "Hidden" },
                    { 829, true, true, (short)7, "System.String", "", "", false, false, true, 64, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "ModifiedByName", new Guid("fb7ff80a-06d3-4081-b8cf-932fdb28efad"), "string", "nvarchar", "", null, "Hidden" },
                    { 804, true, true, (short)-1, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "DetailAccountId", new Guid("aee4f603-fcb8-4ed2-bf1e-59dd17c431ee"), "number", "int", "", null, "AlwaysHidden" },
                    { 805, true, true, (short)-1, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "CostCenterId", new Guid("af0e93f4-c88f-41ba-93fe-2bf639915fa0"), "number", "int", "", null, "AlwaysHidden" },
                    { 806, true, true, (short)-1, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "ProjectId", new Guid("ee7d6938-734f-41c1-b84b-ff3c1fb20065"), "number", "int", "", null, "AlwaysHidden" },
                    { 807, true, true, (short)1, "System.String", "", "", false, false, false, 256, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "FullAccount.Account.FullCode", new Guid("d74090cd-91e8-4515-8c1c-b432e125143e"), "string", "nvarchar", "", null, "Visible" },
                    { 808, true, true, (short)2, "System.String", "", "", false, false, false, 256, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "FullAccount.Account.Name", new Guid("b02ac9a1-f69e-4dd6-bb7e-defb198f41dc"), "string", "nvarchar", "", null, "Visible" },
                    { 809, true, true, (short)3, "System.String", "", "", false, false, false, 256, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "FullAccount.DetailAccount.FullCode", new Guid("3483784e-44c9-4a2a-899f-d595a3b8722b"), "string", "nvarchar", "", null, "Hidden" },
                    { 810, true, true, (short)4, "System.String", "", "", false, false, false, 256, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "FullAccount.DetailAccount.Name", new Guid("be41fd65-43df-4185-b961-8efcb25f674f"), "string", "nvarchar", "", null, "Hidden" },
                    { 811, true, true, (short)5, "System.String", "", "", false, false, false, 256, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "FullAccount.CostCenter.FullCode", new Guid("90114395-46b5-4d5a-a858-74f72e2f6199"), "string", "nvarchar", "", null, "Hidden" },
                    { 812, true, true, (short)6, "System.String", "", "", false, false, false, 256, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "FullAccount.CostCenter.Name", new Guid("24692c89-5cbb-4d14-a1de-d09b5ff69f1b"), "string", "nvarchar", "", null, "Hidden" },
                    { 813, true, true, (short)7, "System.String", "", "", false, false, false, 256, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "FullAccount.Project.FullCode", new Guid("3149d680-2927-4349-a695-e490d05f62c7"), "string", "nvarchar", "", null, "Hidden" },
                    { 814, true, true, (short)8, "System.String", "", "", false, false, false, 256, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "FullAccount.Project.Name", new Guid("51ff5b55-263d-4325-9966-129f5333936b"), "string", "nvarchar", "", null, "Hidden" },
                    { 815, true, true, (short)9, "System.Decimal", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "Amount", new Guid("38d66e71-1881-4835-9e50-167f984c58f1"), "number", "money", "Money", null, "Visible" },
                    { 816, true, true, (short)12, "System.String", "", "", false, false, true, 512, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "Remarks", new Guid("9d6a1ab0-92b8-424c-bc1f-a5896fa31958"), "string", "nvarchar", "", null, "Visible" },
                    { 817, true, true, (short)10, "System.String", "", "", false, false, true, 128, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "SourceAppName", new Guid("8ff997bc-9036-4506-ad3b-9b3d331e226c"), "string", "nvarchar", "", null, "Visible" },
                    { 818, true, true, (short)5, "System.String", "", "", false, false, true, 32, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "State", new Guid("7278496a-81b5-41d4-aad5-19993565da86"), "string", "nvarchar", "", null, "Visible" },
                    { 819, true, true, (short)5, "System.String", "", "", false, false, true, 32, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "State", new Guid("0ccd990d-0174-462c-84f5-783b81abb627"), "string", "nvarchar", "", null, "Visible" },
                    { 820, true, true, (short)5, "System.String", "", "", false, false, true, 32, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "State", new Guid("41435e35-3dc6-4181-b87d-75723e749b4a"), "string", "nvarchar", "", null, "Visible" },
                    { 821, true, true, (short)3, "System.String", "", "", false, false, true, 32, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "State", new Guid("d08f3118-bd6c-4614-9168-0e06c502de9b"), "string", "nvarchar", "", null, "Visible" },
                    { 822, true, true, (short)5, "System.String", "", "", false, false, true, 32, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "State", new Guid("1b86873c-177f-405d-83d9-19e9e16bf8be"), "string", "nvarchar", "", null, "Visible" },
                    { 823, true, true, (short)3, "System.String", "", "", false, false, true, 64, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "CreatedByName", new Guid("bc2877b2-18a1-4cab-9772-ea5de868467f"), "string", "nvarchar", "", null, "Hidden" },
                    { 824, true, true, (short)4, "System.DateTime", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "CreatedDate", new Guid("3f90f4c7-ba6f-46c0-a27a-eccf158e0cd3"), "Date", "datetime", "Default", null, "Hidden" },
                    { 825, true, true, (short)5, "System.String", "", "", false, false, true, 64, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "ModifiedByName", new Guid("fe894635-dd78-4e0e-b83f-6b961d48f92c"), "string", "nvarchar", "", null, "Hidden" },
                    { 826, true, true, (short)6, "System.DateTime", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "ModifiedDate", new Guid("f44e54f5-129a-4330-927b-6c859c3ff99e"), "Date", "datetime", "Default", null, "Hidden" },
                    { 827, true, true, (short)5, "System.String", "", "", false, false, true, 64, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "CreatedByName", new Guid("602982f7-3d3e-4a9f-b2fd-0a2a9a95d7d2"), "string", "nvarchar", "", null, "Hidden" },
                    { 828, true, true, (short)6, "System.DateTime", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "CreatedDate", new Guid("8d079002-1b34-4a90-a93a-daf90cf39ae6"), "Date", "datetime", "Default", null, "Hidden" },
                    { 857, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "RowNo", new Guid("316bc1fd-49e2-4900-be30-8cab19e8764b"), "number", "int", "", null, "AlwaysVisible" },
                    { 803, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "AccountId", new Guid("e6cc5b9c-4155-4941-8e9f-1fc66de55ce5"), "number", "int", "", null, "AlwaysHidden" },
                    { 858, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "Id", new Guid("3ef588b9-5c09-4950-8c2f-ea2f54b0f78a"), "number", "int", "", null, "AlwaysHidden" },
                    { 860, true, true, (short)2, "System.DateTime", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "Date", new Guid("461b5d8a-e419-49c8-befe-f9711e2e3623"), "Date", "datetime", "Default", null, "Visible" },
                    { 100029, true, true, (short)5, "System.String", "", "", false, false, false, 64, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "Prefix", new Guid("2013e7f6-fe4d-49be-b6c1-7dc5b6ce9b37"), "string", "nvarchar", "", null, "Visible" },
                    { 100030, true, true, (short)6, "System.String", "", "", false, false, false, 64, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "Suffix", new Guid("16821970-2262-49b9-ab69-0ea29bbaf05c"), "string", "nvarchar", "", null, "Visible" },
                    { 100031, true, true, (short)7, "System.Boolean", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "IsActive", new Guid("31241c0a-2c05-4015-af20-660f6c4ca66e"), "boolean", "bit", "", null, "Visible" },
                    { 100032, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "BranchId", new Guid("460db50b-d54f-4bf7-9e05-5e9e0dcf377f"), "number", "int", "", null, "AlwaysHidden" },
                    { 100033, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "Id", new Guid("57d9f7bd-168a-4aaa-8fd8-4911a73a880e"), "number", "int", "", null, "AlwaysHidden" },
                    { 100034, false, false, (short)0, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "RowNo", new Guid("f5412d0c-e739-48ff-9d1d-4ac2cd99a3a8"), "number", "int", "", null, "AlwaysVisible" },
                    { 100035, true, true, (short)1, "System.String", "", "", false, false, false, 64, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "Name", new Guid("561ae745-4873-47a4-90d5-a7fa9c183aa4"), "string", "nvarchar", "", null, "Visible" },
                    { 100036, true, true, (short)2, "System.String", "", "", false, false, false, 64, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "EnName", new Guid("32f146da-0727-4b21-b69d-352ecdb4b56f"), "string", "nvarchar", "", null, "Visible" }
                });

            migrationBuilder.InsertData(
                schema: "Metadata",
                table: "Column",
                columns: new[] { "ColumnID", "AllowFiltering", "AllowSorting", "DisplayIndex", "DotNetType", "Expression", "GroupName", "IsDynamic", "IsFixedLength", "IsNullable", "Length", "MinLength", "ModifiedDate", "Name", "rowguid", "ScriptType", "StorageType", "Type", "ViewID", "Visibility" },
                values: new object[,]
                {
                    { 100037, true, true, (short)3, "System.String", "", "", false, false, false, 1024, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "Description", new Guid("3f87bc22-d37d-40bc-92e1-512cbd3cdaac"), "string", "nvarchar", "", null, "Visible" },
                    { 100038, true, true, (short)4, "System.Int16", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "Type", new Guid("dc70bee3-e940-4aaa-83b6-4b38142a5331"), "number", "smallint", "", null, "Visible" },
                    { 100039, true, true, (short)5, "System.Boolean", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "IsActive", new Guid("5c7e216d-5b16-4149-9628-6df447e335f0"), "boolean", "bit", "", null, "Visible" },
                    { 100028, true, true, (short)4, "System.Int16", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "Type", new Guid("a132c7cd-2e87-4570-afbc-91603b99f02c"), "number", "smallint", "", null, "Visible" },
                    { 100040, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "BranchId", new Guid("2fad07f7-e32e-4c86-ac67-07731394a40d"), "number", "int", "", null, "AlwaysHidden" },
                    { 100042, true, true, (short)7, "System.String", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "RowGuid", new Guid("ea692baf-e5e7-4b7d-9488-15e600f8e4d0"), "string", "nvarchar", "", null, "AlwaysHidden" },
                    { 100043, true, true, (short)8, "System.DateTime", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "ModifiedDate", new Guid("984cf5fc-73f9-4eba-b178-ca444acb4149"), "Date", "datetime", "Default", null, "Hidden" },
                    { 100044, false, false, (short)0, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "RowNo", new Guid("1403df63-67e3-40c4-a5c2-29aac4bff7d9"), "number", "int", "", null, "AlwaysVisible" },
                    { 100045, true, true, (short)1, "System.String", "", "", false, false, false, 2048, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "Name", new Guid("2973222a-c86b-444b-809f-c79e9e842de7"), "string", "nvarchar", "", null, "Visible" },
                    { 100046, true, true, (short)2, "System.String", "", "", false, false, false, 2048, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "UniqeName", new Guid("67241499-5adb-49e8-8ee7-18db04a9703b"), "string", "nvarchar", "", null, "Visible" },
                    { 100047, true, true, (short)3, "System.String", "", "", false, false, false, 2048, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "Description", new Guid("ab8fa3ab-3222-4e13-adc7-f97bd316eae4"), "string", "nvarchar", "", null, "Visible" },
                    { 100048, true, true, (short)4, "System.Boolean", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "IsActive", new Guid("c179f42c-0658-4c0c-9aca-4cb06ab944ab"), "boolean", "bit", "", null, "Visible" },
                    { 100049, true, true, (short)5, "Microsoft.AspNetCore.Http.IFormFile", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "FormFile", new Guid("84ceaf8d-d7d7-46fc-bf1f-e68a25b48f7a"), "", "", "", null, "Visible" },
                    { 100050, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "Id", new Guid("75efb957-9eca-45e9-815c-cbcd9e40226e"), "number", "int", "", null, "AlwaysHidden" },
                    { 100051, true, true, (short)6, "System.Guid", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "RowGuid", new Guid("29b43ea5-70c6-44f6-8875-c9b2656c1451"), "", "", "", null, "AlwaysHidden" },
                    { 100052, true, true, (short)7, "System.DateTime", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "ModifiedDate", new Guid("cd445e53-953d-4dc6-a925-398a129bfb15"), "Date", "datetime", "Default", null, "Hidden" },
                    { 100041, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "Id", new Guid("5d700f1a-57b0-47da-99e3-b51d006f9562"), "number", "int", "", null, "AlwaysHidden" },
                    { 100027, true, true, (short)3, "System.String", "", "", false, false, false, 1024, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "Description", new Guid("723c9ee2-d49d-4e88-ba35-f84e43613c46"), "string", "nvarchar", "", null, "Visible" },
                    { 100026, true, true, (short)2, "System.String", "", "", false, false, false, 64, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "EnName", new Guid("c3dc47a2-7e63-47ee-b01c-4bda7f461d87"), "string", "nvarchar", "", null, "Visible" },
                    { 100025, true, true, (short)1, "System.String", "", "", false, false, false, 64, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "Name", new Guid("12088387-97d5-4997-8adb-5d1f0830948b"), "string", "nvarchar", "", null, "Visible" },
                    { 861, true, true, (short)3, "System.String", "", "", false, false, true, 512, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "Description", new Guid("56b40ece-696a-46aa-b9c1-9acf7b7e5796"), "string", "nvarchar", "", null, "Visible" },
                    { 100001, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "RowNo", new Guid("b867799e-f676-4a50-bf16-10d045658578"), "number", "int", "", null, "AlwaysVisible" },
                    { 100002, true, true, (short)1, "System.String", "", "", false, false, false, 64, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "Name", new Guid("335ca36d-7ca0-411a-9cac-28d8fb6c79fa"), "string", "nvarchar", "", null, "Visible" },
                    { 100003, true, true, (short)2, "System.String", "", "", false, false, false, 64, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "EnName", new Guid("c3129805-59a7-434f-8a24-1a8579309898"), "string", "nvarchar", "", null, "Visible" },
                    { 100004, true, true, (short)3, "System.String", "", "", false, false, false, 1024, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "Description", new Guid("91765531-6a7c-4d46-989c-bcba18799c20"), "string", "nvarchar", "", null, "Visible" },
                    { 100005, true, true, (short)4, "System.String", "", "", false, false, false, 64, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "SocialLink", new Guid("4087e38f-595a-46aa-9adb-802c862a43a5"), "string", "nvarchar", "", null, "Visible" },
                    { 100006, true, true, (short)5, "System.String", "", "", false, false, false, 64, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "Website", new Guid("06b29e70-a78e-4a56-94a2-db4965c95940"), "string", "nvarchar", "", null, "Visible" },
                    { 100007, true, true, (short)6, "System.String", "", "", false, false, false, 64, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "MetaKeyword", new Guid("05044450-ec27-4f11-8271-5fe4e4166b27"), "string", "nvarchar", "", null, "Visible" },
                    { 100008, true, true, (short)7, "System.Boolean", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "IsActive", new Guid("cc928ac1-32e1-4d9e-a538-4da693ccf248"), "boolean", "bit", "", null, "Visible" },
                    { 100009, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "BranchId", new Guid("6856d36c-dd56-42b9-b75d-9af88b094560"), "number", "int", "", null, "AlwaysHidden" },
                    { 100010, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "Id", new Guid("3553401e-6650-43b6-a93b-0bd6089a8ad8"), "number", "int", "", null, "AlwaysHidden" },
                    { 100011, true, true, (short)9, "System.Guid", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "RowGuid", new Guid("85e2e17e-8259-49d2-a3ca-df1f9f77fb1d"), "", "", "", null, "AlwaysHidden" },
                    { 100012, true, true, (short)10, "System.DateTime", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "ModifiedDate", new Guid("eef3d706-8f6e-407c-bc46-6e3a42e1cc4e"), "Date", "datetime", "Default", null, "Hidden" },
                    { 100013, false, false, (short)0, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "RowNo", new Guid("13421989-761e-49d5-beb5-15c5daa1b5bf"), "number", "int", "", null, "AlwaysVisible" },
                    { 100014, true, true, (short)1, "System.String", "", "", false, false, false, 64, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "Name", new Guid("4635679e-6535-46fd-9b8f-c7ac5cd24d82"), "string", "nvarchar", "", null, "Visible" },
                    { 100015, true, true, (short)2, "System.String", "", "", false, false, false, 64, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "EnName", new Guid("ceb84c2c-4b78-4b3d-bfec-908fd48a089b"), "string", "nvarchar", "", null, "Visible" },
                    { 100016, true, true, (short)3, "System.String", "", "", false, false, false, 64, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "Description", new Guid("e5060715-42bb-411b-8021-ebb192e16566"), "string", "nvarchar", "", null, "Visible" },
                    { 100017, true, true, (short)4, "System.String", "", "", false, false, false, 64, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "Symbol", new Guid("9dff2874-5389-4171-ba8b-462cb20b9548"), "string", "nvarchar", "", null, "Visible" },
                    { 100018, true, true, (short)5, "System.Int16", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "Status", new Guid("39360a70-3897-488c-8217-ea76f6e26b30"), "number", "smallint", "", null, "Visible" },
                    { 100019, true, true, (short)6, "System.Boolean", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "IsActive", new Guid("98577d60-3620-48d1-9402-698c9e81a00e"), "boolean", "bit", "", null, "Visible" },
                    { 100020, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "BranchId", new Guid("b3557e7b-0180-4e0b-861f-285f35a5b74c"), "number", "int", "", null, "AlwaysHidden" },
                    { 100021, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "Id", new Guid("5e6d8a29-224b-4329-a0ff-0eb1a833f84f"), "number", "int", "", null, "AlwaysHidden" }
                });

            migrationBuilder.InsertData(
                schema: "Metadata",
                table: "Column",
                columns: new[] { "ColumnID", "AllowFiltering", "AllowSorting", "DisplayIndex", "DotNetType", "Expression", "GroupName", "IsDynamic", "IsFixedLength", "IsNullable", "Length", "MinLength", "ModifiedDate", "Name", "rowguid", "ScriptType", "StorageType", "Type", "ViewID", "Visibility" },
                values: new object[,]
                {
                    { 100022, true, true, (short)8, "System.Guid", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "RowGuid", new Guid("157d6e7a-e83b-430b-8d26-12fa2cfcd11c"), "Date", "datetime", "", null, "AlwaysHidden" },
                    { 100023, true, true, (short)9, "System.DateTime", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "ModifiedDate", new Guid("4b3a9dd7-8b84-4d07-8d4a-fad53f39cd87"), "Date", "datetime", "Default", null, "Hidden" },
                    { 100024, false, false, (short)0, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "RowNo", new Guid("58567874-0d40-46a7-8594-2e378417781d"), "number", "int", "", null, "AlwaysVisible" },
                    { 859, true, true, (short)1, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "No", new Guid("fe1724fa-a0aa-4cb8-906e-dcf8286e2d94"), "number", "int", "", null, "Visible" },
                    { 687, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "Id", new Guid("66f23129-d54e-4e97-bd21-e68d825ee1ce"), "number", "int", "", null, "AlwaysHidden" },
                    { 802, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "Id", new Guid("ba013f1f-06a2-4b98-b78d-bd8572ae8066"), "number", "int", "", null, "AlwaysHidden" },
                    { 800, true, true, (short)-1, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "SourceAppId", new Guid("7a4c1b71-2954-41d2-86e3-00afe3349a56"), "number", "int", "", null, "AlwaysHidden" },
                    { 718, true, true, (short)7, "System.Boolean", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "IsArchived", new Guid("92a8b725-8f55-4f40-86cd-ff5b56ec4735"), "boolean", "bit", "", null, "Visible" },
                    { 719, true, true, (short)8, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "PageCount", new Guid("7948b663-ef67-4e59-a0b2-aa349ad0e9d1"), "number", "int", "", null, "Hidden" },
                    { 720, true, true, (short)-1, "System.Object", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "FullAccount", new Guid("08a37c05-7fa7-4b6d-a918-47df750b9631"), "object", "(n/a)", "", null, "AlwaysHidden" },
                    { 721, true, true, (short)9, "System.String", "", "", false, false, false, 16, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "SayyadNo", new Guid("59267006-08b4-4ff0-99a0-3b3ee8c6516a"), "string", "nvarchar", "", null, "Visible" },
                    { 723, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "BranchId", new Guid("4f584376-663c-4594-a93a-0ab0faf53822"), "number", "int", "", null, "AlwaysHidden" },
                    { 724, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "Id", new Guid("33ff6cf4-53e1-427e-9647-883f590447bb"), "number", "int", "", null, "AlwaysHidden" },
                    { 725, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "RowNo", new Guid("e6c9748b-bfb2-4946-9d58-36ca80c70dfd"), "number", "int", "", null, "AlwaysVisible" },
                    { 726, true, true, (short)1, "System.Int64", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "TextNo", new Guid("eacee7e1-5aa0-4e28-905b-297cfe5ed6c7"), "number", "bigint", "", null, "Visible" },
                    { 727, true, true, (short)6, "System.String", "", "", false, false, false, 64, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "Name", new Guid("4a7069ac-580a-4025-9bc3-ae573f698df6"), "string", "nvarchar", "", null, "Visible" },
                    { 728, true, true, (short)7, "System.String", "", "", false, false, false, 32, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "BankName", new Guid("0a90ec3d-7b24-41c7-a405-be82025699c4"), "string", "nvarchar", "", null, "Visible" },
                    { 729, true, true, (short)8, "System.DateTime", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "IssueDate", new Guid("244e6129-1426-42fa-862d-4d0d74d994e4"), "Date", "datetime", "Default", null, "Visible" },
                    { 717, true, true, (short)6, "System.String", "", "", false, false, true, 32, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "EndNo", new Guid("de09c0a7-9d68-4104-85ea-94e8c372411a"), "string", "nvarchar", "", null, "Visible" },
                    { 730, true, true, (short)9, "System.String", "", "", false, false, false, 32, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "StartNo", new Guid("81555258-d76f-4f3d-9ed6-6bebbd3d494c"), "string", "nvarchar", "", null, "Visible" },
                    { 732, true, true, (short)11, "System.String", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "AccountCode", new Guid("c550f827-4c30-43ff-b173-aac1035340d4"), "string", "nvarchar", "", null, "Visible" },
                    { 733, true, true, (short)12, "System.String", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "AccountName", new Guid("416b6769-f5d0-495b-ad6f-a69dce932eff"), "string", "nvarchar", "", null, "Visible" },
                    { 734, true, true, (short)13, "System.String", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "DetailAccountName", new Guid("dccbfe6f-de4e-4420-aa4f-cbdabd2be33f"), "string", "nvarchar", "", null, "Visible" },
                    { 735, true, true, (short)14, "System.String", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "CostCenterName", new Guid("6008a475-d659-4d20-95b2-38c080649a33"), "string", "nvarchar", "", null, "Visible" },
                    { 736, true, true, (short)15, "System.String", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "ProjectName", new Guid("d00c1b35-b764-46da-9a41-c182547914ac"), "string", "nvarchar", "", null, "Visible" },
                    { 737, true, true, (short)16, "System.String", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "IsArchivedName", new Guid("102575d3-0180-45f5-b768-091790f5d7c5"), "string", "nvarchar", "", null, "Visible" },
                    { 738, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "Id", new Guid("97f7fda5-80f9-405c-94a6-1f2e75e54711"), "number", "int", "", null, "AlwaysHidden" },
                    { 739, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "RowNo", new Guid("bfa772e6-d5cd-4ee6-a2ff-a3efc48376b3"), "number", "int", "", null, "AlwaysVisible" },
                    { 740, true, true, (short)1, "System.String", "", "", false, false, false, 64, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "Code", new Guid("1dbcf095-0115-403b-95eb-f2918c5a6f79"), "string", "nvarchar", "", null, "Visible" },
                    { 741, true, true, (short)2, "System.String", "", "", false, false, false, 256, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "Name", new Guid("2c8efcbf-5b1b-497c-8f33-cf57f6e09715"), "string", "nvarchar", "", null, "Visible" },
                    { 742, true, true, (short)-1, "System.Int16", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "Type", new Guid("798aacf0-6808-4ca8-8683-ba3e66a3c90a"), "number", "smallint", "", null, "AlwaysHidden" },
                    { 731, true, true, (short)10, "System.String", "", "", false, false, false, 32, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "EndNo", new Guid("026ff169-56e6-4632-8fbc-1d738e175778"), "string", "nvarchar", "", null, "Visible" },
                    { 716, true, true, (short)5, "System.String", "", "", false, false, false, 32, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "StartNo", new Guid("4e8e08f8-5288-4c04-9538-0a7df70d401b"), "string", "nvarchar", "", null, "Visible" },
                    { 715, true, true, (short)4, "System.DateTime", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "IssueDate", new Guid("1d89dee8-8f60-4abf-bb86-6855b9d74543"), "Date", "datetime", "Default", null, "Visible" },
                    { 714, true, true, (short)3, "System.String", "", "", false, false, true, 32, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "BankName", new Guid("9a3176ae-515f-4fb2-8563-156ae41df639"), "string", "nvarchar", "", null, "Visible" },
                    { 689, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "FunctionId", new Guid("b7b860a5-472a-481e-9343-a6ddc678f91f"), "number", "int", "", null, "AlwaysHidden" },
                    { 690, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "CreatedById", new Guid("6036b10f-e7ba-4c0e-9cba-a4527077afd5"), "number", "int", "", null, "AlwaysHidden" },
                    { 691, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "RowNo", new Guid("04c0e92e-5311-4b67-9481-001c014d69bb"), "number", "int", "", null, "AlwaysVisible" },
                    { 692, true, true, (short)1, "System.String", "", "", false, false, false, 128, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "Title", new Guid("80120fb2-66c6-4f52-a946-452486469272"), "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 693, true, true, (short)2, "System.String", "", "", false, false, false, 64, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "TypeName", new Guid("257ca3c7-6900-405c-a1f5-8a719cda19e7"), "string", "nvarchar", "", null, "Visible" },
                    { 694, true, true, (short)3, "System.String", "", "", false, false, false, 64, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "FunctionName", new Guid("840502e2-a5c9-4210-ac62-a888b839289a"), "string", "nvarchar", "", null, "Visible" },
                    { 695, true, true, (short)4, "System.String", "", "", false, false, false, 128, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "CreatedByFullName", new Guid("165839ff-cad2-48cf-a92a-05749ab0923d"), "string", "nvarchar", "", null, "Visible" }
                });

            migrationBuilder.InsertData(
                schema: "Metadata",
                table: "Column",
                columns: new[] { "ColumnID", "AllowFiltering", "AllowSorting", "DisplayIndex", "DotNetType", "Expression", "GroupName", "IsDynamic", "IsFixedLength", "IsNullable", "Length", "MinLength", "ModifiedDate", "Name", "rowguid", "ScriptType", "StorageType", "Type", "ViewID", "Visibility" },
                values: new object[,]
                {
                    { 696, true, true, (short)5, "System.String", "", "", false, false, true, 512, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "Description", new Guid("00d3569e-ccc3-4542-bb5e-dea883c527a2"), "string", "nvarchar", "", null, "Visible" },
                    { 697, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "RowNo", new Guid("04f9c4cd-afdf-4cd8-973d-8a7581009ac7"), "number", "int", "", null, "AlwaysVisible" },
                    { 698, true, true, (short)1, "System.String", "", "", false, false, false, 64, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "SerialNo", new Guid("78366c12-cb9f-43bd-8793-54df0f272c3f"), "string", "nvarchar", "", null, "Visible" },
                    { 699, true, true, (short)2, "System.String", "", "", false, false, false, 32, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "StatusName", new Guid("73d467c2-cb7d-4160-94fc-7f640e13156a"), "string", "nvarchar", "", null, "Visible" },
                    { 700, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "Id", new Guid("377a9fca-34f1-4fdf-b36b-62237267f1bc"), "number", "int", "", null, "AlwaysHidden" },
                    { 701, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "CheckBookPageID", new Guid("922dad91-5c89-47a5-a731-a7fec15d4ad0"), "number", "int", "", null, "AlwaysHidden" },
                    { 702, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "CheckBookID", new Guid("98895a32-dae5-4bac-a305-4453b5021897"), "number", "int", "", null, "AlwaysHidden" },
                    { 703, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "CheckID", new Guid("409c8cd7-557a-44c3-9e7a-a136c57bde01"), "number", "int", "", null, "AlwaysHidden" },
                    { 704, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "RowNo", new Guid("66911251-8722-4207-83b9-bf6179b9758b"), "number", "int", "", null, "AlwaysVisible" },
                    { 705, true, true, (short)1, "System.String", "", "", false, false, false, 256, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "Name", new Guid("3dca2c38-23f1-47de-a2da-1f37466516d9"), "string", "nvarchar", "", null, "Visible" },
                    { 706, true, true, (short)2, "System.String", "", "", false, false, true, 256, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "Description", new Guid("1390ec28-e565-4f5a-8f29-5b3be3c64d4c"), "string", "nvarchar", "", null, "Visible" },
                    { 707, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "FiscalPeriodId", new Guid("ee1dcf55-6327-47a8-9b80-b0ba697dbcab"), "number", "int", "", null, "AlwaysHidden" },
                    { 708, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "BranchId", new Guid("f26d8aa5-22bd-4688-b18d-328f7f2551fc"), "number", "int", "", null, "AlwaysHidden" },
                    { 709, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "Id", new Guid("3e3ead17-a960-4b93-8061-59de3dc0152b"), "number", "int", "", null, "AlwaysHidden" },
                    { 710, true, true, (short)-1, "System.Int16", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "BranchScope", new Guid("b472a6d6-232d-45b5-bba2-978461b515c4"), "number", "smallint", "", null, "AlwaysHidden" },
                    { 711, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "RowNo", new Guid("72419e46-8c85-40ce-9046-9243b70489b9"), "number", "int", "", null, "AlwaysVisible" },
                    { 712, true, true, (short)1, "System.String", "", "", false, false, false, 64, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "Name", new Guid("4930ac7d-5b8e-410f-af65-f99f616dc449"), "string", "nvarchar", "", null, "Visible" },
                    { 713, true, true, (short)2, "System.Int64", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "TextNo", new Guid("cd17fbcc-e6d3-4d90-b1fe-3ef998a5906d"), "number", "bigint", "", null, "Visible" },
                    { 743, true, true, (short)4, "System.String", "", "", false, false, true, 512, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "Description", new Guid("45660992-beed-4941-91c1-f179b66fc2dc"), "string", "nvarchar", "", null, "Visible" },
                    { 801, true, true, (short)11, "System.String", "", "", false, false, true, 64, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "BankOrderNo", new Guid("5b54ebf6-4adb-48dd-b0bb-cb973b55e0b9"), "string", "nvarchar", "", null, "Visible" },
                    { 744, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "FiscalPeriodId", new Guid("3fd25e33-2f0b-480d-84e1-805ffe10a1c7"), "number", "int", "", null, "AlwaysHidden" },
                    { 746, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "Id", new Guid("eafecaa5-59f7-41fe-b2c3-1f84edc17041"), "number", "int", "", null, "AlwaysHidden" },
                    { 776, true, true, (short)-1, "System.Boolean", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "IsConfirmed", new Guid("f2f72175-542f-4651-b152-4cf4e12e1fd1"), "Boolean", "bit", "", null, "Visible" },
                    { 777, true, true, (short)-1, "System.Boolean", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "IsApproved", new Guid("ac50a97e-a239-4474-bc8e-947d19805fdb"), "Boolean", "bit", "", null, "Visible" },
                    { 778, true, true, (short)1, "System.Int64", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "TextNo", new Guid("7c5397ca-6a09-4528-80b4-97a1cc8af3b0"), "number", "bigint", "", null, "Visible" },
                    { 779, true, true, (short)2, "System.String", "", "", false, false, true, 64, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "Reference", new Guid("a4df9bd3-b870-4a9d-b765-e25fce3da3d9"), "string", "nvarchar", "", null, "Visible" },
                    { 780, false, false, (short)0, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "RowNo", new Guid("caf523fa-3237-4a5a-bc30-0b998f95f51e"), "number", "int", "", null, "AlwaysVisible" },
                    { 781, true, true, (short)-1, "System.Object", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "FullAccount", new Guid("c50354c8-b238-4e98-859b-8fc1c7423b8e"), "object", "(n/a)", "", null, "AlwaysHidden" },
                    { 782, true, true, (short)1, "System.String", "", "", false, false, false, 256, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "FullAccount.Account.FullCode", new Guid("85bcb38d-5960-4b6a-84dd-619cac42e118"), "string", "nvarchar", "", null, "Visible" },
                    { 783, true, true, (short)2, "System.String", "", "", false, false, false, 256, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "FullAccount.Account.Name", new Guid("87722237-6cfc-4a97-b763-c108363c2db5"), "string", "nvarchar", "", null, "Visible" },
                    { 784, true, true, (short)3, "System.String", "", "", false, false, false, 256, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "FullAccount.DetailAccount.FullCode", new Guid("a2ce04c6-ca59-46a0-8fef-2a700f84becc"), "string", "nvarchar", "", null, "Hidden" },
                    { 785, true, true, (short)4, "System.String", "", "", false, false, false, 256, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "FullAccount.DetailAccount.Name", new Guid("e73c81bb-a32d-4961-b55b-1d0a4a14d25a"), "string", "nvarchar", "", null, "Hidden" },
                    { 786, true, true, (short)5, "System.String", "", "", false, false, false, 256, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "FullAccount.CostCenter.FullCode", new Guid("2068b7c0-41c3-4211-b1d3-1fec9ed7f278"), "string", "nvarchar", "", null, "Hidden" },
                    { 775, true, true, (short)-1, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "CurrencyId", new Guid("fc0a5c72-0ade-4d34-b831-b79e1b5317c9"), "number", "int", "", null, "Visible" },
                    { 787, true, true, (short)6, "System.String", "", "", false, false, false, 256, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "FullAccount.CostCenter.Name", new Guid("4fa5daa7-ef3b-432c-b4a5-8513f1395fb2"), "string", "nvarchar", "", null, "Hidden" },
                    { 789, true, true, (short)8, "System.String", "", "", false, false, false, 256, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "FullAccount.Project.Name", new Guid("7ce5bb85-a53d-4133-b140-13a8cd6f0f37"), "string", "nvarchar", "", null, "Hidden" },
                    { 790, true, true, (short)2, "System.String", "", "", false, false, false, 16, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "SayyadStartNo", new Guid("89434c4a-8ed8-446c-9e69-786d37da85ea"), "string", "nvarchar", "", null, "Visible" },
                    { 791, true, true, (short)3, "System.String", "", "", false, false, false, 32, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "SeriesNo", new Guid("9cee00cb-54d8-431c-8838-20023c81f29a"), "string", "nvarchar", "", null, "Visible" },
                    { 792, true, true, (short)9, "System.Decimal", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "Amount", new Guid("add5481e-b7fe-49e4-ab88-3975914868d4"), "number", "money", "Money", null, "Visible" },
                    { 793, true, true, (short)10, "System.String", "", "", false, false, true, 512, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "Remarks", new Guid("e859599a-fa6f-4b6f-8bc9-31e6a9bac33b"), "string", "nvarchar", "", null, "Visible" },
                    { 794, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "PayReceiveId", new Guid("f1810003-eb14-4ccf-ac73-4413bd723756"), "number", "int", "", null, "AlwaysHidden" },
                    { 795, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "Id", new Guid("f6b21c21-a42d-4806-a36d-beaac91bbdab"), "number", "int", "", null, "AlwaysHidden" }
                });

            migrationBuilder.InsertData(
                schema: "Metadata",
                table: "Column",
                columns: new[] { "ColumnID", "AllowFiltering", "AllowSorting", "DisplayIndex", "DotNetType", "Expression", "GroupName", "IsDynamic", "IsFixedLength", "IsNullable", "Length", "MinLength", "ModifiedDate", "Name", "rowguid", "ScriptType", "StorageType", "Type", "ViewID", "Visibility" },
                values: new object[,]
                {
                    { 796, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "RowNo", new Guid("e834c261-6478-4b4b-8da8-51b6d11da9a3"), "number", "int", "", null, "AlwaysVisible" },
                    { 797, true, true, (short)-1, "System.Object", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "FullAccount", new Guid("e554e93f-1b2e-4bc4-b1d6-c75ffad3c40f"), "Object", "int", "", null, "AlwaysHidden" },
                    { 798, true, true, (short)-1, "System.Boolean", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "IsBank", new Guid("d9523868-0bf5-4a63-921e-12b3df444e7f"), "boolean", "bit", "", null, "AlwaysHidden" },
                    { 799, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "PayReceiveId", new Guid("6ed1ab57-5810-471e-a47b-dbea5ffa9097"), "number", "int", "", null, "AlwaysHidden" },
                    { 788, true, true, (short)7, "System.String", "", "", false, false, false, 256, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "FullAccount.Project.FullCode", new Guid("4dfec190-81c6-4ba1-a6cc-6192803355bb"), "string", "nvarchar", "", null, "Hidden" },
                    { 774, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "Id", new Guid("9a3cea9d-8e74-4e0b-a588-9c8a98e722a1"), "number", "int", "", null, "AlwaysHidden" },
                    { 773, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "BranchId", new Guid("d1c30229-662d-4d06-8e31-0f7bef84deba"), "number", "int", "", null, "AlwaysHidden" },
                    { 772, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "FiscalPeriodId", new Guid("0e3608e1-574b-4217-98f7-4a16baa934ff"), "number", "int", "", null, "AlwaysHidden" },
                    { 747, true, true, (short)9, "System.String", "", "", false, false, false, 32, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "SeriesNo", new Guid("4094bca6-6ff6-47de-80ad-12e8d67aeee5"), "string", "nvarchar", "", null, "Visible" },
                    { 748, true, true, (short)10, "System.String", "", "", false, false, false, 16, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "SayyadStartNo", new Guid("b4ede395-273f-4bd7-9722-b1f56527d656"), "string", "nvarchar", "", null, "Visible" },
                    { 749, true, true, (short)3, "System.String", "", "", false, false, false, 16, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "SayyadNo", new Guid("ef2c36b1-baf8-4a06-b2a5-9aa44a3533e7"), "string", "nvarchar", "", null, "Visible" },
                    { 750, true, true, (short)3, "System.String", "", "", false, false, true, 64, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "TypeName", new Guid("63ee6e8f-315b-4e28-b222-02a01c497de5"), "string", "nvarchar", "", null, "Visible" },
                    { 751, true, true, (short)-1, "System.Int16", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "BranchScope", new Guid("a4bc6fb4-12fb-4b62-b044-e5b44733d2eb"), "number", "smallint", "", null, "AlwaysHidden" },
                    { 752, true, true, (short)3, "System.DateTime", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "Date", new Guid("44fe4f68-5857-42a0-9315-1922cb007786"), "Date", "datetime", "Default", null, "Visible" },
                    { 753, true, true, (short)4, "System.String", "", "", false, false, false, 64, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "IssuedByName", new Guid("73df925f-b66e-462e-a0df-dddc9d2913c0"), "string", "nvarchar", "", null, "Visible" },
                    { 754, true, true, (short)5, "System.String", "", "", false, false, true, 64, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "ConfirmedByName", new Guid("a4caf925-b356-49fc-b35c-e1d625353f11"), "string", "nvarchar", "", null, "Visible" },
                    { 755, true, true, (short)6, "System.String", "", "", false, false, true, 64, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "ApprovedByName", new Guid("00f03033-7f9c-4c44-8077-cc713a868e8e"), "string", "nvarchar", "", null, "Visible" },
                    { 756, true, true, (short)7, "System.Decimal", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "CurrencyRate", new Guid("25e127ec-9f28-46ef-8a1d-074eecc3938e"), "number", "money", "Currency", null, "Visible" },
                    { 757, true, true, (short)8, "System.String", "", "", false, false, true, 1024, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "Description", new Guid("a9516cfb-526e-49c5-b0d9-2314715362da"), "string", "nvarchar", "", null, "Visible" },
                    { 758, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "FiscalPeriodId", new Guid("18bdda12-730d-4db0-98ac-00da21352218"), "number", "int", "", null, "AlwaysHidden" },
                    { 759, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "BranchId", new Guid("53299134-c7d7-49ef-bea8-5d4e9755bbe2"), "number", "int", "", null, "AlwaysHidden" },
                    { 760, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "Id", new Guid("6d388792-f2db-455a-b55f-4b3dd3719944"), "number", "int", "", null, "AlwaysHidden" },
                    { 761, true, true, (short)-1, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "CurrencyId", new Guid("322e0dcd-4742-40fa-8791-b3a6f253a66a"), "number", "int", "", null, "Visible" },
                    { 762, true, true, (short)-1, "System.Boolean", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "IsConfirmed", new Guid("62ae0853-b412-4ee8-93b7-65f72e3d3922"), "Boolean", "bit", "", null, "Visible" },
                    { 763, true, true, (short)-1, "System.Boolean", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "IsApproved", new Guid("545db15a-67fe-44cb-9c97-aabc677dd655"), "Boolean", "bit", "", null, "Visible" },
                    { 764, true, true, (short)1, "System.Int64", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "TextNo", new Guid("501f9bd3-5761-42dc-8ae4-cb35d5a0893d"), "number", "bigint", "", null, "Visible" },
                    { 765, true, true, (short)2, "System.String", "", "", false, false, true, 64, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "Reference", new Guid("105a8aa0-16b9-470d-8021-116117acf57b"), "string", "nvarchar", "", null, "Visible" },
                    { 766, true, true, (short)3, "System.DateTime", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "Date", new Guid("8292cbf0-d2fe-44b0-aa18-a33807b03f10"), "Date", "datetime", "Default", null, "Visible" },
                    { 767, true, true, (short)4, "System.String", "", "", false, false, false, 64, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "IssuedByName", new Guid("9776a778-86c3-4d8f-8d24-ac5489457dc0"), "string", "nvarchar", "", null, "Visible" },
                    { 768, true, true, (short)5, "System.String", "", "", false, false, true, 64, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "ConfirmedByName", new Guid("c26a87dc-86de-474c-ad9f-3a5c92778991"), "string", "nvarchar", "", null, "Visible" },
                    { 769, true, true, (short)6, "System.String", "", "", false, false, true, 64, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "ApprovedByName", new Guid("d9199057-e65e-4fb1-98fa-3f7d93178ee7"), "string", "nvarchar", "", null, "Visible" },
                    { 770, true, true, (short)7, "System.Decimal", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "CurrencyRate", new Guid("56f6390c-9e5d-416c-ac4b-1f85b720c31b"), "number", "money", "Currency", null, "Visible" },
                    { 771, true, true, (short)8, "System.String", "", "", false, false, true, 1024, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "Description", new Guid("75bad728-89de-45a9-8817-320fe57c117e"), "string", "nvarchar", "", null, "Visible" },
                    { 745, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "BranchId", new Guid("7807cbf0-5511-461f-a55d-a015c73641f4"), "number", "int", "", null, "AlwaysHidden" },
                    { 458, true, true, (short)11, "System.Decimal", "", "EndBalance", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "EndBalanceDebit", new Guid("067e0de1-637d-482e-8404-c9cbdb631be3"), "number", "money", "Money", null, "Visible" },
                    { 523, true, true, (short)-1, "System.String", "", "", false, false, true, 64, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "VoucherReference", new Guid("451e6b06-8f46-485f-931f-78b00ff7de49"), "string", "nvarchar", "", null, "AlwaysHidden" },
                    { 456, true, true, (short)9, "System.Decimal", "", "Corrections", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "CorrectionsDebit", new Guid("45d0ea51-cd1d-42a6-870b-80a05a99e20c"), "number", "money", "Money", null, "Visible" },
                    { 146, true, true, (short)13, "System.Decimal", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "Credit", new Guid("85260731-9ddd-4cca-9eba-524ca495b2e3"), "number", "money", "Money", null, "Visible" },
                    { 147, true, true, (short)14, "System.String", "", "", false, false, false, 128, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "BranchName", new Guid("ec3483cd-5ee6-480e-8353-f4d05d20988d"), "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 148, true, true, (short)15, "System.String", "", "", false, false, true, 128, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "Mark", new Guid("22968e5a-6c2e-442c-a65b-779e82f8b433"), "string", "nvarchar", "", null, "Visible" },
                    { 149, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "RowNo", new Guid("44f16bb0-7221-4b31-babd-3f52a76ff0c2"), "number", "int", "", null, "AlwaysVisible" },
                    { 150, true, true, (short)1, "System.Date", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "VoucherDate", new Guid("d1216109-26ff-4557-868f-5b86eee14ea0"), "Date", "datetime", "Default", null, "Visible" }
                });

            migrationBuilder.InsertData(
                schema: "Metadata",
                table: "Column",
                columns: new[] { "ColumnID", "AllowFiltering", "AllowSorting", "DisplayIndex", "DotNetType", "Expression", "GroupName", "IsDynamic", "IsFixedLength", "IsNullable", "Length", "MinLength", "ModifiedDate", "Name", "rowguid", "ScriptType", "StorageType", "Type", "ViewID", "Visibility" },
                values: new object[,]
                {
                    { 151, true, true, (short)2, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "VoucherNo", new Guid("eabecfb6-3af8-4f13-8a45-9275f4558670"), "number", "int", "", null, "Visible" },
                    { 152, true, true, (short)3, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "AccountFullCode", new Guid("f967d1c2-31a6-4007-85fb-f43786f858dc"), "string", "nvarchar", "", null, "Visible" },
                    { 153, true, true, (short)4, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "AccountName", new Guid("d55d2e88-4068-4a5f-97b3-c66d38cfe4bc"), "string", "nvarchar", "", null, "Visible" },
                    { 154, true, true, (short)5, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "Description", new Guid("83e1f293-e65b-4ec3-8969-27b7d9d9542a"), "string", "nvarchar", "", null, "Visible" },
                    { 155, true, true, (short)6, "System.Decimal", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "Debit", new Guid("4de4dda7-fc49-458c-9dd8-bc6beb6e983c"), "number", "money", "Money", null, "Visible" },
                    { 156, true, true, (short)7, "System.Decimal", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "Credit", new Guid("97939d01-d3e1-4c23-90f8-9df164aec654"), "number", "money", "Money", null, "Visible" },
                    { 145, true, true, (short)12, "System.Decimal", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "Debit", new Guid("c6576cfa-446d-4ad7-a4c6-36710bf957d9"), "number", "money", "Money", null, "Visible" },
                    { 157, true, true, (short)8, "System.String", "", "", false, false, false, 128, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "BranchName", new Guid("671a8482-1cdc-44b2-bd27-fc0ebe6c6d8a"), "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 159, true, true, (short)1, "System.Date", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "VoucherDate", new Guid("6127a913-e924-484a-ac7d-7296fca7d79e"), "Date", "datetime", "Default", null, "Visible" },
                    { 160, true, true, (short)2, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "VoucherNo", new Guid("ec3c94d5-eb91-4d5b-b338-22d7544cad28"), "number", "int", "", null, "Visible" },
                    { 161, true, true, (short)3, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "AccountFullCode", new Guid("db86dbf3-5e76-4fe9-a8c6-fae62fb734ac"), "string", "nvarchar", "", null, "Visible" },
                    { 162, true, true, (short)4, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "AccountName", new Guid("45e8a420-9c71-4404-8f58-b0472b5fca6f"), "string", "nvarchar", "", null, "Visible" },
                    { 457, true, true, (short)10, "System.Decimal", "", "Corrections", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "CorrectionsCredit", new Guid("bc9a2e0c-8a35-4b69-8b3f-be24b0665d2a"), "number", "money", "Money", null, "Visible" },
                    { 164, true, true, (short)6, "System.Decimal", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "Debit", new Guid("b07f08cc-a76a-4a78-b765-b8496f07250f"), "number", "money", "Money", null, "Visible" },
                    { 165, true, true, (short)7, "System.Decimal", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "Credit", new Guid("d4524831-a533-4207-9f7e-21fde346212a"), "number", "money", "Money", null, "Visible" },
                    { 166, true, true, (short)8, "System.String", "", "", false, false, false, 128, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "BranchName", new Guid("cf592b32-995c-4a9c-bf9e-3778347ec8aa"), "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 167, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "RowNo", new Guid("3c8a4c75-4f25-403f-a064-fe87ca2278f3"), "number", "int", "", null, "AlwaysVisible" },
                    { 168, true, true, (short)1, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "AccountFullCode", new Guid("e67af598-5ae0-44f3-869c-8b39b768c01f"), "string", "nvarchar", "", null, "Visible" },
                    { 169, true, true, (short)2, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "AccountName", new Guid("4feed739-7376-4028-971d-aaf3115d730c"), "string", "nvarchar", "", null, "Visible" },
                    { 158, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "RowNo", new Guid("2c37cd24-a61b-4abd-8054-aeec1124b84a"), "number", "int", "", null, "AlwaysVisible" },
                    { 144, true, true, (short)11, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "Description", new Guid("1f17cdd4-f8db-4fae-aa86-eef61a9b366e"), "string", "nvarchar", "", null, "Visible" },
                    { 143, true, true, (short)10, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "ProjectName", new Guid("d215116f-2a9f-431f-b878-e27409df5ec5"), "string", "nvarchar", "", null, "Visible" },
                    { 142, true, true, (short)9, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "ProjectFullCode", new Guid("5297a762-d893-4708-aaed-916eaa52771b"), "string", "nvarchar", "", null, "Visible" },
                    { 117, true, true, (short)10, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "EntityNo", new Guid("25617cb3-d4b8-45bd-b238-4f8c737ab1e1"), "number", "int", "", null, "Visible" },
                    { 118, true, true, (short)11, "System.DateTime", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "EntityDate", new Guid("b698fc49-5b44-454c-ba5f-046765da8bae"), "Date", "datetime", "Default", null, "Visible" },
                    { 119, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "Id", new Guid("33b99455-2501-4cde-8b7f-277a5df07e1a"), "number", "int", "", null, "AlwaysHidden" },
                    { 120, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "RowNo", new Guid("8d9392b3-2171-489f-b4f7-8e867ad426de"), "number", "int", "", null, "AlwaysVisible" },
                    { 121, true, true, (short)1, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "Name", new Guid("808f2790-c6a4-4c6b-9815-1faf0cfc56c3"), "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 122, true, true, (short)2, "System.String", "", "", false, false, false, 256, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "FullCode", new Guid("4b17227a-2f6d-4dda-b6c2-f3d0e28f2e44"), "string", "nvarchar", "", null, "Visible" },
                    { 123, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "RowNo", new Guid("7c6cb0bd-f04e-4e31-8ded-bc94d899f50a"), "number", "int", "", null, "AlwaysVisible" },
                    { 124, true, true, (short)1, "System.Date", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "VoucherDate", new Guid("b039bff2-eb36-4ac6-8908-887fc1891a35"), "Date", "datetime", "Default", null, "Visible" },
                    { 125, true, true, (short)2, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "VoucherNo", new Guid("012a2b69-ae05-465e-a54c-f07829b28a76"), "number", "int", "", null, "Visible" },
                    { 126, true, true, (short)3, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "AccountFullCode", new Guid("36d619f4-b983-4184-8fdb-4b510a5db5df"), "string", "nvarchar", "", null, "Visible" },
                    { 127, true, true, (short)4, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "AccountName", new Guid("879b3759-8824-4bba-a6c5-3fad9722b7bd"), "string", "nvarchar", "", null, "Visible" },
                    { 128, true, true, (short)5, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "Description", new Guid("de030586-f973-480e-8bf5-a630efe1a74a"), "string", "nvarchar", "", null, "Visible" },
                    { 129, true, true, (short)6, "System.Decimal", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "Debit", new Guid("fb1e68fa-ca53-4ab3-a2b8-b9c16801ca44"), "number", "money", "Money", null, "Visible" },
                    { 130, true, true, (short)7, "System.Decimal", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "Credit", new Guid("ead118ec-cf7b-4633-9167-348dc1dce4ec"), "number", "money", "Money", null, "Visible" },
                    { 131, true, true, (short)8, "System.String", "", "", false, false, false, 128, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "BranchName", new Guid("e53127a1-a025-455c-97cb-b8d7cc029112"), "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 132, true, true, (short)9, "System.String", "", "", false, false, true, 128, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "Mark", new Guid("d65e0dc5-050f-42a6-a2ee-c75d30cc8179"), "string", "nvarchar", "", null, "Visible" },
                    { 133, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "RowNo", new Guid("3b034b44-e86c-402b-8642-ad57cfb93d3a"), "number", "int", "", null, "AlwaysVisible" },
                    { 134, true, true, (short)1, "System.Date", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "VoucherDate", new Guid("d3f3cd09-4f7d-4cbe-94c1-a0c165e46ec2"), "Date", "datetime", "Default", null, "Visible" },
                    { 135, true, true, (short)2, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "VoucherNo", new Guid("ee31d320-f165-4ca4-970e-d34cff71769f"), "number", "int", "", null, "Visible" }
                });

            migrationBuilder.InsertData(
                schema: "Metadata",
                table: "Column",
                columns: new[] { "ColumnID", "AllowFiltering", "AllowSorting", "DisplayIndex", "DotNetType", "Expression", "GroupName", "IsDynamic", "IsFixedLength", "IsNullable", "Length", "MinLength", "ModifiedDate", "Name", "rowguid", "ScriptType", "StorageType", "Type", "ViewID", "Visibility" },
                values: new object[,]
                {
                    { 136, true, true, (short)3, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "AccountFullCode", new Guid("0b738ba8-1816-400a-abe8-cec1b92f2b2e"), "string", "nvarchar", "", null, "Visible" },
                    { 137, true, true, (short)4, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "AccountName", new Guid("fed957ee-c63b-4b5a-991e-4962121c1641"), "string", "nvarchar", "", null, "Visible" },
                    { 138, true, true, (short)5, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "DetailAccountFullCode", new Guid("476db247-317f-4ed9-bb24-e01d45c03bff"), "string", "nvarchar", "", null, "Visible" },
                    { 139, true, true, (short)6, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "DetailAccountName", new Guid("3c0bdab1-622f-4534-925c-a61a6b1779c6"), "string", "nvarchar", "", null, "Visible" },
                    { 140, true, true, (short)7, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "CostCenterFullCode", new Guid("4ac34d52-04f0-4d12-93ca-2f4675fbf6e9"), "string", "nvarchar", "", null, "Visible" },
                    { 141, true, true, (short)8, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "CostCenterName", new Guid("a996f8bb-6595-4922-8323-e17134351195"), "string", "nvarchar", "", null, "Visible" },
                    { 170, true, true, (short)3, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "Description", new Guid("a3d8a8e8-11e7-4111-9cc3-ec82edd1279b"), "string", "nvarchar", "", null, "Visible" },
                    { 116, true, true, (short)9, "System.String", "", "", false, false, false, 64, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "EntityDescription", new Guid("2d3a9eca-48ad-41bf-9b17-ee9c0a8a41b9"), "string", "nvarchar", "", null, "Visible" },
                    { 171, true, true, (short)4, "System.Decimal", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "Debit", new Guid("e16c7343-ffc8-4023-be2b-822bdcae6555"), "number", "money", "Money", null, "Visible" },
                    { 173, true, true, (short)6, "System.String", "", "", false, false, false, 128, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "BranchName", new Guid("e8ae582e-60a9-45cb-8243-c16a8f5ca117"), "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 203, true, true, (short)3, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "AccountFullCode", new Guid("6381b3f8-4e47-4d95-a7e0-47c3d9c10f43"), "string", "nvarchar", "", null, "Visible" },
                    { 204, true, true, (short)4, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "AccountName", new Guid("730054b5-b371-40ae-8370-3211f6504cb7"), "string", "nvarchar", "", null, "Visible" },
                    { 205, true, true, (short)5, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "DetailAccountFullCode", new Guid("bbaac9c4-9e31-42d3-8d01-17637cd31cc9"), "string", "nvarchar", "", null, "Visible" },
                    { 206, true, true, (short)6, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "DetailAccountName", new Guid("b9ed99bb-94c0-4ed7-830c-906611c75a73"), "string", "nvarchar", "", null, "Visible" },
                    { 207, true, true, (short)7, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "CostCenterFullCode", new Guid("eec36ac7-379e-4b1e-8a2d-2e852a5b01eb"), "string", "nvarchar", "", null, "Visible" },
                    { 208, true, true, (short)8, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "CostCenterName", new Guid("ffcd1a09-ce50-4f7a-bf6a-1d5405786cc0"), "string", "nvarchar", "", null, "Visible" },
                    { 209, true, true, (short)9, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "ProjectFullCode", new Guid("d5b10fb4-3ae0-464d-9437-5d38c5324511"), "string", "nvarchar", "", null, "Visible" },
                    { 210, true, true, (short)10, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "ProjectName", new Guid("5c9e4cfb-c3a4-4d20-9c3c-7b830527fac4"), "string", "nvarchar", "", null, "Visible" },
                    { 211, true, true, (short)11, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "Description", new Guid("c2685f58-0525-493b-9036-b3720984465d"), "string", "nvarchar", "", null, "Visible" },
                    { 212, true, true, (short)12, "System.Decimal", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "Debit", new Guid("593cc46e-3529-4ba8-905f-567d6fbdb7f1"), "number", "money", "Money", null, "Visible" },
                    { 213, true, true, (short)13, "System.Decimal", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "Credit", new Guid("050b7fb5-9dea-4018-b8fd-47671278ca4a"), "number", "money", "Money", null, "Visible" },
                    { 202, true, true, (short)2, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "VoucherNo", new Guid("792f54af-e469-430f-9a12-651bc8fa9811"), "number", "int", "", null, "Visible" },
                    { 214, true, true, (short)14, "System.String", "", "", false, false, false, 128, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "BranchName", new Guid("ad162ef7-1b78-43c3-b3da-07894035c9a7"), "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 216, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "RowNo", new Guid("57751281-b750-4a8f-8417-7c5fd5d802fa"), "number", "int", "", null, "AlwaysVisible" },
                    { 217, true, true, (short)1, "System.Date", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "VoucherDate", new Guid("5b1c358a-5e19-45aa-8d17-5bf40aedbd1c"), "Date", "datetime", "Default", null, "Visible" },
                    { 218, true, true, (short)2, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "VoucherNo", new Guid("0656ff21-0df7-4c4b-9d24-8c5418239304"), "number", "int", "", null, "Visible" },
                    { 219, true, true, (short)3, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "AccountFullCode", new Guid("018f51fe-950b-4298-a159-82fe6e6f6765"), "string", "nvarchar", "", null, "Visible" },
                    { 220, true, true, (short)4, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "AccountName", new Guid("f6d3ffda-569a-4e7b-aaa8-6b2829f1e32e"), "string", "nvarchar", "", null, "Visible" },
                    { 221, true, true, (short)5, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "Description", new Guid("caac9970-3cab-42cb-9229-42512ba9e15f"), "string", "nvarchar", "", null, "Visible" },
                    { 222, true, true, (short)6, "System.Decimal", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "Debit", new Guid("b6073891-8e48-4776-b98d-3cb2fb64f9b2"), "number", "money", "Money", null, "Visible" },
                    { 223, true, true, (short)7, "System.Decimal", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "Credit", new Guid("59bada62-31aa-4564-8e5d-323524eac35c"), "number", "money", "Money", null, "Visible" },
                    { 224, true, true, (short)8, "System.String", "", "", false, false, false, 128, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "BranchName", new Guid("328399c9-bc33-47c8-a1aa-a0b8f9b7978a"), "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 225, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "RowNo", new Guid("f504e062-78e5-4195-b3ac-18da84f9c511"), "number", "int", "", null, "AlwaysVisible" },
                    { 226, true, true, (short)1, "System.Date", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "VoucherDate", new Guid("683ab492-d302-4b19-9c9a-8aa50144e1e1"), "Date", "datetime", "Default", null, "Visible" },
                    { 215, true, true, (short)15, "System.String", "", "", false, false, true, 128, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "Mark", new Guid("9cf21ad3-f52d-4bc5-8320-48e55b4dc78c"), "string", "nvarchar", "", null, "Visible" },
                    { 201, true, true, (short)1, "System.Date", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "VoucherDate", new Guid("c79e7046-57f3-4d76-b7ea-8aa3449ad63b"), "Date", "datetime", "Default", null, "Visible" },
                    { 200, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "RowNo", new Guid("bcad6147-21b9-4b04-9178-0028afbe55c7"), "number", "int", "", null, "AlwaysVisible" },
                    { 199, true, true, (short)9, "System.String", "", "", false, false, true, 128, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "Mark", new Guid("f93f68bb-1933-4c1f-a50c-07dfa36e6610"), "string", "nvarchar", "", null, "Visible" },
                    { 174, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "RowNo", new Guid("f553d398-91b8-4715-894d-dc47640bdd3c"), "number", "int", "", null, "AlwaysVisible" },
                    { 175, true, true, (short)1, "System.Date", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "VoucherDate", new Guid("08a8d517-3dac-4d98-988f-d8fc93ac485d"), "Date", "datetime", "Default", null, "Visible" },
                    { 176, true, true, (short)2, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "AccountFullCode", new Guid("dd1bdcfe-1a19-498c-8c6f-d73f63d8b7b0"), "string", "nvarchar", "", null, "Visible" },
                    { 177, true, true, (short)3, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "AccountName", new Guid("77c6ebc2-14bb-460d-8bcf-60cd5b794940"), "string", "nvarchar", "", null, "Visible" }
                });

            migrationBuilder.InsertData(
                schema: "Metadata",
                table: "Column",
                columns: new[] { "ColumnID", "AllowFiltering", "AllowSorting", "DisplayIndex", "DotNetType", "Expression", "GroupName", "IsDynamic", "IsFixedLength", "IsNullable", "Length", "MinLength", "ModifiedDate", "Name", "rowguid", "ScriptType", "StorageType", "Type", "ViewID", "Visibility" },
                values: new object[,]
                {
                    { 178, true, true, (short)4, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "Description", new Guid("18190144-b390-4ecf-9aee-0d893d3a6e81"), "string", "nvarchar", "", null, "Visible" },
                    { 179, true, true, (short)5, "System.Decimal", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "Debit", new Guid("6d12d506-7494-4a1b-86ac-a27d8cf09ace"), "number", "money", "Money", null, "Visible" },
                    { 180, true, true, (short)6, "System.Decimal", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "Credit", new Guid("7b19c550-d977-4a91-bef8-3c89283ccc1a"), "number", "money", "Money", null, "Visible" },
                    { 181, true, true, (short)7, "System.String", "", "", false, false, false, 128, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "BranchName", new Guid("efc27fe0-f2fb-458c-b13d-5d23cf09dd1c"), "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 182, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "RowNo", new Guid("8aeee6c2-c6ca-47d5-8cf6-509b061f38a8"), "number", "int", "", null, "AlwaysVisible" },
                    { 183, true, true, (short)1, "System.Date", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "VoucherDate", new Guid("e90e889a-420e-41f4-893c-4b252537fc95"), "Date", "datetime", "Default", null, "Visible" },
                    { 184, true, true, (short)2, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "AccountFullCode", new Guid("4e70816a-b1b8-48e8-874a-994b85f41dd8"), "string", "nvarchar", "", null, "Visible" },
                    { 185, true, true, (short)3, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "AccountName", new Guid("b70dc0c7-d378-45aa-bc56-0b9514ea9778"), "string", "nvarchar", "", null, "Visible" },
                    { 186, true, true, (short)4, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "Description", new Guid("84c7f3c3-f2b4-4d9f-8492-edcbb3c4c67f"), "string", "nvarchar", "", null, "Visible" },
                    { 187, true, true, (short)5, "System.Decimal", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "Debit", new Guid("062fc6eb-8292-4e70-b8e7-03bdd175202d"), "number", "money", "Money", null, "Visible" },
                    { 188, true, true, (short)6, "System.Decimal", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "Credit", new Guid("695232cf-959d-4f5f-9a1d-29ef0dcc20b6"), "number", "money", "Money", null, "Visible" },
                    { 189, true, true, (short)7, "System.String", "", "", false, false, false, 128, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "BranchName", new Guid("18abc4ea-ff97-4162-aec4-d17b1e37f526"), "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 190, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "RowNo", new Guid("d11d6ca7-4b55-47a1-a651-56615f36821d"), "number", "int", "", null, "AlwaysVisible" },
                    { 191, true, true, (short)1, "System.Date", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "VoucherDate", new Guid("21e5b444-dd3a-4f82-8309-5a33ab309daf"), "Date", "datetime", "Default", null, "Visible" },
                    { 192, true, true, (short)2, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "VoucherNo", new Guid("1499b8fd-f6a9-4968-8607-d13206b78367"), "number", "int", "", null, "Visible" },
                    { 193, true, true, (short)3, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "AccountFullCode", new Guid("37d896ae-d779-4d23-a6ca-41626732f7a6"), "string", "nvarchar", "", null, "Visible" },
                    { 194, true, true, (short)4, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "AccountName", new Guid("f97b5f5b-cfb7-4da3-b5bc-7742e8d9b00a"), "string", "nvarchar", "", null, "Visible" },
                    { 195, true, true, (short)5, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "Description", new Guid("446b413d-9de6-4324-9a0f-ff65be66ca9f"), "string", "nvarchar", "", null, "Visible" },
                    { 196, true, true, (short)6, "System.Decimal", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "Debit", new Guid("f9b843c5-7773-43a2-b8ed-507fc359002f"), "number", "money", "Money", null, "Visible" },
                    { 197, true, true, (short)7, "System.Decimal", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "Credit", new Guid("2ffbc80d-c3bb-463b-8cc1-47d5bde3531f"), "number", "money", "Money", null, "Visible" },
                    { 198, true, true, (short)8, "System.String", "", "", false, false, false, 128, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "BranchName", new Guid("d8d30a27-d3a5-4743-8731-553acb3b966a"), "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 172, true, true, (short)5, "System.Decimal", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "Credit", new Guid("834d8397-65ac-4bd0-a6cb-8fb524034957"), "number", "money", "Money", null, "Visible" },
                    { 115, true, true, (short)8, "System.String", "", "", false, false, false, 64, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "EntityName", new Guid("dee64b91-cbd5-4068-83db-44d8689ac598"), "string", "nvarchar", "", null, "Visible" },
                    { 114, true, true, (short)7, "System.String", "", "", false, false, false, 64, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "EntityCode", new Guid("c3d66762-54df-4301-ad06-0cb7bebfe0fe"), "string", "nvarchar", "", null, "Visible" },
                    { 113, true, true, (short)6, "System.String", "", "", false, false, false, 64, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "EntityTypeName", new Guid("7327d76e-b058-468e-8c87-257514cff6ee"), "string", "nvarchar", "", null, "Visible" },
                    { 30, true, true, (short)-1, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "FullAccount.CostCenter.Id", new Guid("719d5523-94fe-4323-81fa-b84dbb1832a5"), "number", "int", "", null, "AlwaysHidden" },
                    { 31, true, true, (short)-1, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "FullAccount.Project.Id", new Guid("55aa4d9c-52c7-4b8b-96e2-ca5c1c503f3b"), "number", "int", "", null, "AlwaysHidden" },
                    { 32, true, true, (short)-1, "System.Decimal", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "CurrencyRate", new Guid("9722b816-24e5-4eeb-acc4-12b993ce1e2a"), "number", "money", "Money", null, "AlwaysHidden" },
                    { 33, true, true, (short)-1, "System.Int16", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "TypeId", new Guid("f00b4e36-5160-4bdc-b80f-aacbb9a96fcb"), "number", "smallint", "", null, "AlwaysHidden" },
                    { 34, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "RowNo", new Guid("b4eaf91a-2e07-40ee-b364-d2e1b5710246"), "number", "int", "", null, "AlwaysVisible" },
                    { 35, true, true, (short)1, "System.String", "", "", false, false, false, 256, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "FullAccount.Account.FullCode", new Guid("b48b8d8c-c293-491a-afc2-edfcbd015beb"), "string", "nvarchar", "", null, "Visible" },
                    { 36, true, true, (short)2, "System.String", "", "", false, false, false, 128, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "FullAccount.Account.Name", new Guid("ac20889b-bff6-4a11-8f2c-5ab9241607de"), "string", "nvarchar", "", null, "Visible" },
                    { 37, true, true, (short)3, "System.String", "", "", false, false, false, 128, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "FullAccount.DetailAccount.FullCode", new Guid("a9abdf8d-d305-4ed3-a319-fd9e8b1c55d7"), "string", "nvarchar", "", null, "Hidden" },
                    { 38, true, true, (short)4, "System.String", "", "", false, false, false, 128, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "FullAccount.DetailAccount.Name", new Guid("5a69ecb4-3dd5-45ec-bfa2-7f49ed7d33cd"), "string", "nvarchar", "", null, "Hidden" },
                    { 39, true, true, (short)5, "System.String", "", "", false, false, false, 128, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "FullAccount.CostCenter.FullCode", new Guid("986605d9-72fe-41fc-9607-0248918db5aa"), "string", "nvarchar", "", null, "Hidden" },
                    { 40, true, true, (short)6, "System.String", "", "", false, false, false, 128, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "FullAccount.CostCenter.Name", new Guid("3243aa7d-7b3d-445e-a56f-6e9739a46d36"), "string", "nvarchar", "", null, "Hidden" },
                    { 29, true, true, (short)-1, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "FullAccount.DetailAccount.Id", new Guid("481d5ef9-e560-49ec-92d5-3bc413c627dc"), "number", "int", "", null, "AlwaysHidden" },
                    { 41, true, true, (short)7, "System.String", "", "", false, false, false, 128, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "FullAccount.Project.FullCode", new Guid("eff62bf0-d35b-44f3-977e-e385572503ca"), "string", "nvarchar", "", null, "Hidden" },
                    { 43, true, true, (short)11, "System.String", "", "", false, false, true, 512, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "Description", new Guid("411b2ec1-a25a-4548-b29a-63dac35afac0"), "string", "nvarchar", "", null, "Visible" },
                    { 44, true, true, (short)12, "System.Decimal", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "Debit", new Guid("8819c22f-4ad0-4715-be0c-f86a616f0cc1"), "number", "money", "Money", null, "Visible" },
                    { 45, true, true, (short)13, "System.Decimal", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "Credit", new Guid("73a27c4b-a089-4765-bbac-c99a31ebd133"), "number", "money", "Money", null, "Visible" },
                    { 46, true, true, (short)14, "System.String", "", "", false, false, false, 64, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "CurrencyName", new Guid("48f88d6c-86a0-4b77-adab-3d3873b7fb69"), "string", "nvarchar", "", null, "AlwaysVisible" }
                });

            migrationBuilder.InsertData(
                schema: "Metadata",
                table: "Column",
                columns: new[] { "ColumnID", "AllowFiltering", "AllowSorting", "DisplayIndex", "DotNetType", "Expression", "GroupName", "IsDynamic", "IsFixedLength", "IsNullable", "Length", "MinLength", "ModifiedDate", "Name", "rowguid", "ScriptType", "StorageType", "Type", "ViewID", "Visibility" },
                values: new object[,]
                {
                    { 47, true, true, (short)15, "System.Decimal", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "CurrencyValue", new Guid("dea8d20a-36c4-483c-af60-de9e89fcfd5b"), "number", "money", "Money", null, "AlwaysVisible" },
                    { 48, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "Id", new Guid("2d25335a-9a42-410f-b738-6e6d8fbfa73f"), "number", "int", "", null, "AlwaysHidden" },
                    { 49, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "RowNo", new Guid("08a54151-58e8-4c90-a785-5884024fd216"), "number", "int", "", null, "AlwaysVisible" },
                    { 50, true, true, (short)1, "System.String", "", "", false, false, false, 64, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "UserName", new Guid("7cbede71-80a9-426b-bfee-7a22f8904b42"), "string", "nvarchar(64)", "", null, "AlwaysVisible" },
                    { 51, true, true, (short)2, "System.DateTime", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "LastLoginDate", new Guid("3fbda2b9-caae-4cfc-9877-20766fe46cfe"), "DateTime", "datetime", "Default", null, "Visible" },
                    { 52, true, true, (short)3, "System.Boolean", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "IsEnabled", new Guid("434c68ba-473d-4fce-a836-0270c466ac55"), "boolean", "bit", "", null, "Visible" },
                    { 53, true, true, (short)4, "System.String", "Person.FullName", "", false, false, false, 64, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "PersonFullName", new Guid("12dff1d0-7bcb-4a9a-90ca-d60a1e774c48"), "string", "nvarchar(64)", "", null, "Visible" },
                    { 42, true, true, (short)8, "System.String", "", "", false, false, false, 128, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "FullAccount.Project.Name", new Guid("b4181f15-e5a6-4075-afd2-d005f522e9d5"), "string", "nvarchar", "", null, "Hidden" },
                    { 28, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "FullAccount.Account.Id", new Guid("f97d94db-d63b-4218-86da-5eeb0690e253"), "number", "int", "", null, "AlwaysHidden" },
                    { 27, true, true, (short)-1, "System.Object", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "FullAccount", new Guid("8a97d9fc-9974-4e51-ba8f-60582d20310d"), "object", "(n/a)", "", null, "AlwaysHidden" },
                    { 26, true, true, (short)-1, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "CurrencyId", new Guid("bdc0d572-4a6f-453f-b87b-8c52f19fe1c3"), "number", "int", "", null, "AlwaysHidden" },
                    { 1, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "Id", new Guid("16061986-c04a-4942-a333-c662833d5884"), "number", "int", "", null, "AlwaysHidden" },
                    { 2, true, false, (short)-1, "System.Int16", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "BranchScope", new Guid("e395733b-d346-479d-87e3-6f0e955632d1"), "number", "smallint", "", null, "AlwaysHidden" },
                    { 3, true, true, (short)-1, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "GroupId", new Guid("6c3f12fc-d1d3-4c0b-b579-4abe710ec97f"), "number", "int", "", null, "AlwaysHidden" },
                    { 4, true, true, (short)-1, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "CurrencyId", new Guid("7cec5457-2c94-45a0-8bc2-6cb149457df0"), "number", "int", "", null, "AlwaysHidden" },
                    { 5, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "RowNo", new Guid("7a28459e-8b02-4ead-a72c-90e0ca4970b7"), "number", "int", "", null, "AlwaysVisible" },
                    { 6, true, true, (short)1, "System.String", "", "", false, false, false, 16, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "Code", new Guid("534128f6-42d7-4b0a-b334-9b883306d9b6"), "string", "nvarchar", "", null, "Visible" },
                    { 7, true, true, (short)2, "System.String", "", "", false, false, false, 256, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "FullCode", new Guid("0576fd15-3fdc-4cd6-9540-dc956b636dd9"), "string", "nvarchar", "", null, "Visible" },
                    { 8, true, true, (short)3, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "Name", new Guid("da320275-e6a7-430b-a015-64790f66cb1c"), "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 9, true, true, (short)-1, "System.Int16", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "Level", new Guid("eb53bc01-5239-4386-ab74-e4a5958df150"), "number", "smallint", "", null, "AlwaysHidden" },
                    { 10, true, true, (short)4, "System.String", "", "", false, false, true, 512, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "Description", new Guid("74d175e6-be35-4eaf-9979-1d9ab4a53a4f"), "string", "nvarchar", "", null, "Visible" },
                    { 11, true, true, (short)5, "System.String", "", "", false, false, true, 32, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "State", new Guid("2db0be15-0747-4a3b-9ae6-7b4fd7201d02"), "string", "nvarchar", "", null, "Visible" },
                    { 12, true, true, (short)6, "System.Boolean", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "IsCurrencyAdjustable", new Guid("9be2dc8e-43c7-44a0-9925-f7fe7a70b49f"), "boolean", "bit", "", null, "Hidden" },
                    { 13, true, true, (short)7, "System.String", "", "", false, false, true, 32, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "TurnoverMode", new Guid("e067905a-a7d2-47c2-8ced-de0f992e6a74"), "string", "nvarchar", "", null, "Hidden" },
                    { 14, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "Id", new Guid("5ee0d1e5-4a67-4476-97db-20c37e46e459"), "number", "int", "", null, "AlwaysHidden" },
                    { 15, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "StatusId", new Guid("e5624a09-653d-4425-ac12-1c8e28b1cc2b"), "number", "int", "", null, "AlwaysHidden" },
                    { 16, true, true, (short)-1, "System.Int16", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "Type", new Guid("2c345ffe-f7dc-4adf-ba80-723268bd6609"), "number", "smallint", "", null, "AlwaysHidden" },
                    { 17, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "RowNo", new Guid("a3989200-f943-4889-ac96-35f50da86058"), "number", "int", "", null, "AlwaysVisible" },
                    { 18, true, true, (short)1, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "No", new Guid("61f0ef3e-56c3-472a-80ca-a74353a39250"), "number", "int", "", null, "AlwaysVisible" },
                    { 19, true, true, (short)2, "System.Date", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "Date", new Guid("81115627-c841-4f4e-b10e-356f8b0f6fc0"), "Date", "datetime", "Default", null, "Visible" },
                    { 20, true, true, (short)3, "System.String", "", "", false, false, true, 512, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "Description", new Guid("82daeb8e-c055-470b-8d04-56583ab80105"), "string", "nvarchar", "", null, "Visible" },
                    { 21, true, true, (short)4, "System.String", "", "", false, false, true, 64, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "StatusName", new Guid("c69ffb5e-2630-4117-8928-b9512abf02e3"), "string", "nvarchar", "", null, "Visible" },
                    { 22, true, true, (short)5, "System.String", "", "", false, false, true, 64, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "Reference", new Guid("3765a263-edf9-40ee-a9c9-735df2d94ba3"), "string", "nvarchar", "", null, "Visible" },
                    { 23, true, true, (short)6, "System.String", "", "", false, false, true, 64, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "Association", new Guid("7c2e7b91-2156-4668-9abd-6ae9b48bdabc"), "string", "nvarchar", "", null, "Visible" },
                    { 24, true, true, (short)7, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "DailyNo", new Guid("50709b5b-e0f1-4979-bb87-5ecddafb8435"), "number", "int", "", null, "Visible" },
                    { 25, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "Id", new Guid("9e6fbfba-349d-4840-a3d3-92feb9897dac"), "number", "int", "", null, "AlwaysHidden" },
                    { 55, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "RowNo", new Guid("46b2e8b5-7b8a-4448-8d19-b4d157619637"), "number", "int", "", null, "AlwaysVisible" },
                    { 56, true, true, (short)1, "System.String", "", "", false, false, false, 64, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "Name", new Guid("88aec8f7-57fd-4b58-a81e-5f7d77c1807c"), "string", "nvarchar(64)", "", null, "AlwaysVisible" },
                    { 57, true, true, (short)2, "System.String", "", "", false, false, true, 512, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "Description", new Guid("5163fbe8-7843-4e1e-b0b4-9b5b8162b604"), "string", "nvarchar(512)", "", null, "Visible" },
                    { 58, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "Id", new Guid("3e3de172-b420-4c20-8580-f0808cb864da"), "number", "int", "", null, "AlwaysHidden" },
                    { 88, true, true, (short)4, "System.String", "", "", false, false, true, 512, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "Description", new Guid("d358bd03-b242-4ec8-aefb-dcfa24f30363"), "string", "nvarchar", "", null, "Visible" },
                    { 89, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "Id", new Guid("7251f006-6643-4bfe-b60e-951f3b3e3a66"), "number", "int", "", null, "AlwaysHidden" }
                });

            migrationBuilder.InsertData(
                schema: "Metadata",
                table: "Column",
                columns: new[] { "ColumnID", "AllowFiltering", "AllowSorting", "DisplayIndex", "DotNetType", "Expression", "GroupName", "IsDynamic", "IsFixedLength", "IsNullable", "Length", "MinLength", "ModifiedDate", "Name", "rowguid", "ScriptType", "StorageType", "Type", "ViewID", "Visibility" },
                values: new object[,]
                {
                    { 90, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "RowNo", new Guid("f9b8d202-06f1-43a3-9c68-3990c0dbcb4d"), "number", "int", "", null, "AlwaysVisible" },
                    { 91, true, true, (short)1, "System.String", "", "", false, false, false, 128, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "Name", new Guid("f0558578-a2e5-4cd7-bd0e-c535c74b7ab3"), "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 92, true, true, (short)2, "System.String", "", "", false, false, true, 512, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "Description", new Guid("21665726-9e59-42f0-b960-a0344e3c7f05"), "string", "nvarchar", "", null, "Visible" },
                    { 93, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "Id", new Guid("4773607f-d564-4279-a7f5-00522beb1990"), "number", "int", "", null, "AlwaysHidden" },
                    { 94, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "RowNo", new Guid("db4a9dec-23e7-450e-bd2e-ae1908a7fa6d"), "number", "int", "", null, "AlwaysVisible" },
                    { 95, true, true, (short)1, "System.String", "", "", false, false, false, 128, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "Name", new Guid("12d19bfd-1b33-40a8-9a4d-e400bf7c1c18"), "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 96, true, true, (short)2, "System.String", "", "", false, false, false, 128, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "DbName", new Guid("40134279-6964-44b3-a296-ef2c3c5bc177"), "string", "nvarchar", "", null, "Visible" },
                    { 97, true, true, (short)5, "System.String", "", "", false, false, true, 512, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "Description", new Guid("d753f999-40bd-46c1-b7a5-baf9b7a5ce2f"), "string", "nvarchar", "", null, "Visible" },
                    { 98, true, true, (short)3, "System.String", "", "", false, false, true, 64, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "Server", new Guid("d2f72601-5f7b-4bab-a189-cdcbe6ba0b94"), "string", "nvarchar", "", null, "Visible" },
                    { 99, true, true, (short)4, "System.String", "", "", false, false, true, 32, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "UserName", new Guid("a4d97575-34d9-4d13-b248-0d965c57b781"), "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 100, true, true, (short)-1, "System.String", "", "", false, false, true, 32, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "Password", new Guid("21cc30ef-9ad6-4fe1-a93a-69b7cadbd06e"), "string", "nvarchar", "", null, "AlwaysHidden" },
                    { 101, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "Id", new Guid("15933336-d87c-4827-93ad-05eef1e0d3ae"), "number", "int", "", null, "AlwaysHidden" },
                    { 102, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "RowNo", new Guid("42a404ed-77e1-496c-9a29-9ec0c4ef6198"), "number", "int", "", null, "AlwaysVisible" },
                    { 103, true, true, (short)1, "System.String", "", "", false, false, false, 64, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "Name", new Guid("84445af6-57f4-46a5-a2a8-705b317627b7"), "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 104, true, true, (short)2, "System.String", "", "", false, false, false, 64, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "Category", new Guid("ea9628dd-14b8-413f-9da4-be6726aaec3b"), "string", "nvarchar", "", null, "Visible" },
                    { 105, true, true, (short)3, "System.String", "", "", false, false, true, 256, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "Description", new Guid("bbf22c19-7288-480b-926d-cb6bf92b2e01"), "string", "nvarchar", "", null, "Visible" },
                    { 106, true, true, (short)-1, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "Id", new Guid("65811998-7df3-4614-8e29-46cd11ceee3c"), "number", "int", "", null, "AlwaysHidden" },
                    { 107, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "RowNo", new Guid("81550e5f-4fc4-4bd9-93b3-66d5144dc169"), "number", "int", "", null, "AlwaysVisible" },
                    { 108, true, true, (short)1, "System.String", "", "", false, false, false, 64, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "UserName", new Guid("8f1825ae-a0ba-4da8-ac24-20e07aae0761"), "string", "nvarchar", "", null, "Visible" },
                    { 109, true, true, (short)2, "System.String", "", "", false, false, false, 64, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "BranchName", new Guid("3e8bcb2c-6a9a-42d4-959d-8f79ef365ef3"), "string", "nvarchar", "", null, "Visible" },
                    { 110, true, true, (short)3, "System.String", "", "", false, false, false, 64, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "FiscalPeriodName", new Guid("651902a7-5098-4c31-b6a1-0b87a31b5e82"), "string", "nvarchar", "", null, "Visible" },
                    { 111, true, true, (short)4, "System.Date", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "Date", new Guid("936640e9-70f7-43b6-b596-2668070275e9"), "Date", "datetime", "Default", null, "AlwaysVisible" },
                    { 112, true, true, (short)5, "System.TimeSpan", "", "", false, false, false, 7, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "Time", new Guid("0d399897-7a8d-4d5a-9b34-b243026d899a"), "Date", "time", "", null, "AlwaysVisible" },
                    { 87, true, true, (short)3, "System.Date", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "EndDate", new Guid("39a88fd7-9d47-4720-9097-48585c035b32"), "Date", "datetime", "Default", null, "Visible" },
                    { 227, true, true, (short)2, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "VoucherNo", new Guid("c3f8c200-f7b8-448f-90bf-3e8e2dfd457e"), "number", "int", "", null, "Visible" },
                    { 86, true, true, (short)2, "System.Date", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "StartDate", new Guid("48889969-594d-4675-b3cb-ffbe2d197693"), "Date", "datetime", "Default", null, "Visible" },
                    { 84, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "RowNo", new Guid("e2966285-dd28-415c-9bad-76884cf99b5b"), "number", "int", "", null, "AlwaysVisible" },
                    { 59, true, true, (short)-1, "System.Int16", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "Level", new Guid("8a362cfb-e8b6-42cf-a642-5a3f38a9034e"), "", "smallint", "", null, "AlwaysHidden" },
                    { 60, true, false, (short)-1, "System.Int16", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "BranchScope", new Guid("d6620e61-dfcb-4f22-ad9b-4177f19963a0"), "number", "smallint", "", null, "AlwaysHidden" },
                    { 61, true, true, (short)-1, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "CurrencyId", new Guid("997638b2-176c-4586-9a29-c1e0c54bd960"), "number", "int", "", null, "AlwaysHidden" },
                    { 62, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "RowNo", new Guid("168174a4-a54d-4f52-8c8b-7e1968add356"), "number", "int", "", null, "AlwaysVisible" },
                    { 63, true, true, (short)1, "System.String", "", "", false, false, false, 16, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "Code", new Guid("a9903b6d-805c-4a39-9d2e-b368752fc183"), "string", "nvarchar", "", null, "Visible" },
                    { 64, true, true, (short)2, "System.String", "", "", false, false, false, 256, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "FullCode", new Guid("c8234ee3-7a56-49b4-ac92-f6c0d2b50703"), "string", "nvarchar", "", null, "Visible" },
                    { 65, true, true, (short)3, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "Name", new Guid("dd45c747-f644-4406-a80b-bd915bb5d038"), "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 66, true, true, (short)4, "System.String", "", "", false, false, true, 512, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "Description", new Guid("efb34417-94c1-4336-a8f3-0ba16be9f1f3"), "string", "nvarchar", "", null, "Visible" },
                    { 67, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "Id", new Guid("33170318-a5ff-4633-b660-b9b9f6cf21e9"), "number", "int", "", null, "AlwaysHidden" },
                    { 68, true, true, (short)-1, "System.Int16", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "Level", new Guid("ec9ba0e9-e445-4075-a017-9fdfc82b3b83"), "", "smallint", "", null, "AlwaysHidden" },
                    { 69, true, false, (short)-1, "System.Int16", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "BranchScope", new Guid("69f522de-29aa-4524-86b7-b949b752fb80"), "number", "smallint", "", null, "AlwaysHidden" },
                    { 70, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "RowNo", new Guid("105e0e71-bb42-4025-94d6-678564313d8f"), "number", "int", "", null, "AlwaysVisible" },
                    { 71, true, true, (short)1, "System.String", "", "", false, false, false, 16, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "Code", new Guid("c6294d45-4487-4510-8c50-11a34189d162"), "string", "nvarchar", "", null, "Visible" },
                    { 72, true, true, (short)2, "System.String", "", "", false, false, false, 256, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "FullCode", new Guid("4497b4c1-d985-4584-8b2a-a85684e2bd6f"), "string", "nvarchar", "", null, "Visible" },
                    { 73, true, true, (short)3, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "Name", new Guid("4f7bc4fd-61c3-48cd-a54a-23b92a403fbc"), "string", "nvarchar", "", null, "AlwaysVisible" }
                });

            migrationBuilder.InsertData(
                schema: "Metadata",
                table: "Column",
                columns: new[] { "ColumnID", "AllowFiltering", "AllowSorting", "DisplayIndex", "DotNetType", "Expression", "GroupName", "IsDynamic", "IsFixedLength", "IsNullable", "Length", "MinLength", "ModifiedDate", "Name", "rowguid", "ScriptType", "StorageType", "Type", "ViewID", "Visibility" },
                values: new object[,]
                {
                    { 74, true, true, (short)4, "System.String", "", "", false, false, true, 512, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "Description", new Guid("f49ad3f4-3a08-4cc9-a1b0-330edd8c0a23"), "string", "nvarchar", "", null, "Visible" },
                    { 75, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "Id", new Guid("3162c9d7-03d9-43ee-a2e6-72dfab6e34a1"), "number", "int", "", null, "AlwaysHidden" },
                    { 76, true, true, (short)-1, "System.Int16", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "Level", new Guid("96265c7e-0c32-4907-815e-9b0d9f8486b5"), "", "smallint", "", null, "AlwaysHidden" },
                    { 77, true, false, (short)-1, "System.Int16", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "BranchScope", new Guid("2e339494-c1fd-4229-95c9-807e65532449"), "number", "smallint", "", null, "AlwaysHidden" },
                    { 78, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "RowNo", new Guid("94f74aae-ac8c-44b8-8ced-f72314913295"), "number", "int", "", null, "AlwaysVisible" },
                    { 79, true, true, (short)1, "System.String", "", "", false, false, false, 16, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "Code", new Guid("f28dfbb1-53e3-41af-8758-5f1205dcdc4c"), "string", "nvarchar", "", null, "Visible" },
                    { 80, true, true, (short)2, "System.String", "", "", false, false, false, 256, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "FullCode", new Guid("b48e3e6d-73fd-4d93-9110-3def0ced6790"), "string", "nvarchar", "", null, "Visible" },
                    { 81, true, true, (short)3, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "Name", new Guid("3a8702d6-fb01-4102-aa52-515d54dc54a1"), "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 82, true, true, (short)4, "System.String", "", "", false, false, true, 512, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "Description", new Guid("86a4e738-77bd-4763-aaff-d739f1b8d71d"), "string", "nvarchar", "", null, "Visible" },
                    { 83, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "Id", new Guid("7ce04e18-2d12-4c43-a85e-65391d050d4c"), "number", "int", "", null, "AlwaysHidden" },
                    { 85, true, true, (short)1, "System.String", "", "", false, false, false, 64, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "Name", new Guid("ccd95542-6f5e-4a8d-8c25-1e804989cdaf"), "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 228, true, true, (short)3, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "AccountFullCode", new Guid("1728de88-582c-4f5a-a775-4ee7f1bad0e2"), "string", "nvarchar", "", null, "Visible" },
                    { 163, true, true, (short)5, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "Description", new Guid("1adf742a-cf8f-48f0-99ac-54c1c7fe1059"), "string", "nvarchar", "", null, "Visible" },
                    { 230, true, true, (short)5, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "Description", new Guid("f3548c64-66dc-4606-970e-f39e377fc3f0"), "string", "nvarchar", "", null, "Visible" },
                    { 375, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "RowNo", new Guid("399c3501-36ad-4b96-8039-103b6fb8e29e"), "number", "int", "", null, "AlwaysVisible" },
                    { 376, true, true, (short)1, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "LineCount", new Guid("d2227e63-476f-4e64-84bb-a8e79009caa3"), "number", "int", "", null, "Visible" },
                    { 377, true, true, (short)2, "System.Date", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "VoucherDate", new Guid("a19f46d3-c653-4ef4-9e1c-574eb8a355cf"), "Date", "datetime", "Default", null, "Visible" },
                    { 378, true, true, (short)3, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "Description", new Guid("1bfaaf4c-2a08-4ea2-add0-8dc7ad2b3289"), "string", "nvarchar", "", null, "Visible" },
                    { 379, true, true, (short)4, "System.Decimal", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "BaseCurrencyDebit", new Guid("eef88456-9dfd-4f0d-87ba-5fae8dcd609c"), "number", "money", "Money", null, "Visible" },
                    { 380, true, true, (short)5, "System.Decimal", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "BaseCurrencyCredit", new Guid("e16797a7-7ffc-4a37-acc0-5356542268f5"), "number", "money", "Money", null, "Visible" },
                    { 381, true, true, (short)6, "System.Decimal", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "BaseCurrencyBalance", new Guid("b586207c-0d67-485f-9a60-a0ad12ede50d"), "number", "money", "Money", null, "Visible" },
                    { 382, true, true, (short)7, "System.Decimal", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "Debit", new Guid("455aa606-c627-46f3-9b59-03bc17eca18a"), "number", "money", "Money", null, "Visible" },
                    { 383, true, true, (short)8, "System.Decimal", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "Credit", new Guid("72b869ea-afc6-4808-bd13-e55c34026b54"), "number", "money", "Money", null, "Visible" },
                    { 384, true, true, (short)9, "System.Decimal", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "Balance", new Guid("c4003fff-f4fa-4a79-aae0-d30a96fe6f98"), "number", "money", "Money", null, "Visible" },
                    { 385, true, true, (short)10, "System.String", "", "", false, false, false, 128, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "BranchName", new Guid("35ccd3b2-38da-4160-a1a5-598408dab1c4"), "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 374, true, true, (short)11, "System.String", "", "", false, false, false, 128, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "BranchName", new Guid("9bbf9df9-4b43-49f3-973e-77720e40a317"), "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 386, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "RowNo", new Guid("a87a6471-1037-4af7-b725-52bc13b55665"), "number", "int", "", null, "AlwaysVisible" },
                    { 388, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "Id", new Guid("6c7e515e-dee3-4331-980d-33e2806d0007"), "number", "int", "", null, "AlwaysHidden" },
                    { 389, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "BranchId", new Guid("57436f4e-29dc-490a-802d-44795308ebfd"), "number", "int", "", null, "AlwaysHidden" },
                    { 390, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "RowNo", new Guid("e759962f-78f8-4d24-85e0-ec8154c4d903"), "number", "int", "", null, "AlwaysVisible" },
                    { 391, true, true, (short)1, "System.String", "", "", false, false, false, 256, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "AccountFullCode", new Guid("364875a1-4080-4f68-a118-c23ee7ac6741"), "string", "nvarchar", "", null, "Visible" },
                    { 392, true, true, (short)2, "System.String", "", "", false, false, false, 128, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "AccountName", new Guid("7d3cd592-fa62-4d24-82ba-8e93fe975eb3"), "string", "nvarchar", "", null, "Visible" },
                    { 393, true, true, (short)3, "System.Date", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "VoucherDate", new Guid("3c9570db-8617-49a4-824d-f65fe36a02e7"), "Date", "datetime", "Default", null, "Visible" },
                    { 394, true, true, (short)4, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "VoucherNo", new Guid("82475e78-8833-47e6-88b3-80804267ead7"), "number", "int", "", null, "AlwaysVisible" },
                    { 395, true, true, (short)5, "System.String", "", "", false, false, false, 64, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "VoucherReference", new Guid("ed96ded8-70bd-4e17-a0a2-00efbb3f419c"), "string", "nvarchar", "", null, "Visible" },
                    { 396, true, true, (short)6, "System.Decimal", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "Debit", new Guid("b44309fa-287a-4faf-b4fc-76fb761aca98"), "number", "money", "Money", null, "Visible" },
                    { 397, true, true, (short)7, "System.Decimal", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "Credit", new Guid("c73c7154-2105-4464-a6b2-568559aeb97c"), "number", "money", "Money", null, "Visible" },
                    { 398, true, true, (short)8, "System.String", "", "", false, false, true, 512, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "Description", new Guid("753750aa-eec0-4328-abe4-d25e03e94a2c"), "string", "nvarchar", "", null, "Visible" },
                    { 387, true, true, (short)1, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "Number", new Guid("30f03ef6-151e-4409-b0f8-e9b2e801e910"), "number", "int", "", null, "AlwaysVisible" },
                    { 373, true, true, (short)10, "System.Decimal", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "Balance", new Guid("d8820b3a-12b1-4304-80f1-5b9229e24b23"), "number", "money", "Money", null, "Visible" },
                    { 372, true, true, (short)9, "System.Decimal", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "Credit", new Guid("a63b03d5-bbea-419c-bb37-68dffced39df"), "number", "money", "Money", null, "Visible" },
                    { 371, true, true, (short)8, "System.Decimal", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "Debit", new Guid("b823b088-35b3-4916-82e8-e5cee2531b07"), "number", "money", "Money", null, "Visible" }
                });

            migrationBuilder.InsertData(
                schema: "Metadata",
                table: "Column",
                columns: new[] { "ColumnID", "AllowFiltering", "AllowSorting", "DisplayIndex", "DotNetType", "Expression", "GroupName", "IsDynamic", "IsFixedLength", "IsNullable", "Length", "MinLength", "ModifiedDate", "Name", "rowguid", "ScriptType", "StorageType", "Type", "ViewID", "Visibility" },
                values: new object[,]
                {
                    { 346, true, true, (short)2, "System.Decimal", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "Debit", new Guid("dfd5377a-8433-4260-bc4c-baa0435ddb88"), "number", "money", "Money", null, "Visible" },
                    { 347, true, true, (short)3, "System.Decimal", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "Credit", new Guid("e46b3a16-7735-4e1d-8e40-8ea0e09ada26"), "number", "money", "Money", null, "Visible" },
                    { 348, true, true, (short)4, "System.Decimal", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "Balance", new Guid("a04f7aa2-68e2-405d-b53a-17cf7a758ac7"), "number", "money", "Money", null, "Visible" },
                    { 349, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "RowNo", new Guid("9d608096-e18c-46a9-a404-e33c8bef1a3a"), "number", "int", "", null, "AlwaysVisible" },
                    { 350, true, true, (short)1, "System.Date", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "VoucherDate", new Guid("d8943610-f2c6-484d-b324-b2e918345d45"), "Date", "datetime", "Default", null, "Visible" },
                    { 351, true, true, (short)2, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "VoucherNo", new Guid("318c60f6-3d18-4463-9618-88c4ce04cd23"), "number", "int", "", null, "Visible" },
                    { 352, true, true, (short)3, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "Description", new Guid("adf6ae15-75ad-4419-803b-ebf1a9432ca6"), "string", "nvarchar", "", null, "Visible" },
                    { 353, true, true, (short)4, "System.String", "", "", false, false, false, 64, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "Reference", new Guid("74506ef0-21bc-4357-ab06-f79814d980b6"), "string", "nvarchar", "", null, "Visible" },
                    { 354, true, true, (short)5, "System.Decimal", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "BaseCurrencyDebit", new Guid("8be52c70-0bfe-45b3-be99-7fa690529677"), "number", "money", "Money", null, "Visible" },
                    { 355, true, true, (short)6, "System.Decimal", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "BaseCurrencyCredit", new Guid("b1356d9d-2da9-488d-ad44-b5c8a0032ee2"), "number", "money", "Money", null, "Visible" },
                    { 356, true, true, (short)7, "System.Decimal", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "BaseCurrencyBalance", new Guid("4cb45a41-7430-46cc-8423-935fa1185b1a"), "number", "money", "Money", null, "Visible" },
                    { 357, true, true, (short)8, "System.Decimal", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "Debit", new Guid("735296c1-73da-401b-9dba-4aa1d101122a"), "number", "money", "Money", null, "Visible" },
                    { 358, true, true, (short)9, "System.Decimal", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "Credit", new Guid("a5f2ea0c-e2a1-4c40-a22c-4cfb2c2ff88a"), "number", "money", "Money", null, "Visible" },
                    { 359, true, true, (short)10, "System.Decimal", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "Balance", new Guid("b31e0b02-63ed-44a7-b56b-60d04aedbffb"), "number", "money", "Money", null, "Visible" },
                    { 360, true, true, (short)11, "System.Decimal", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "CurrencyRate", new Guid("1433390d-fe15-4834-9bd2-009fa016ff1f"), "number", "money", "Money", null, "Visible" },
                    { 361, true, true, (short)12, "System.String", "", "", false, false, false, 128, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "Mark", new Guid("479e9cda-90ed-41e0-a99a-249c3179029e"), "string", "nvarchar", "", null, "Visible" },
                    { 362, true, true, (short)13, "System.String", "", "", false, false, false, 128, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "BranchName", new Guid("02bba795-f7fd-4082-b97a-946ad595654c"), "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 363, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "RowNo", new Guid("33a512c1-7b51-4e47-bff5-545cd01b3308"), "number", "int", "", null, "AlwaysVisible" },
                    { 364, true, true, (short)1, "System.Date", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "VoucherDate", new Guid("081f0910-6152-4201-a597-2c2f2cffef76"), "Date", "datetime", "Default", null, "Visible" },
                    { 365, true, true, (short)2, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "VoucherNo", new Guid("a523ae86-276a-4f8f-86f8-f8578b616707"), "number", "int", "", null, "Visible" },
                    { 366, true, true, (short)3, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "Description", new Guid("2e5702ba-8c7b-4696-b733-dcf1ef67f842"), "string", "nvarchar", "", null, "Visible" },
                    { 367, true, true, (short)4, "System.String", "", "", false, false, false, 64, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "Reference", new Guid("5b3cb9db-afcf-4707-9069-2552c83c4e9f"), "string", "nvarchar", "", null, "Visible" },
                    { 368, true, true, (short)5, "System.Decimal", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "BaseCurrencyDebit", new Guid("73ac6889-4424-471e-9893-7c0a42287086"), "number", "money", "Money", null, "Visible" },
                    { 369, true, true, (short)6, "System.Decimal", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "BaseCurrencyCredit", new Guid("596a0899-9c18-47e4-a6f3-fff3e97a6f8e"), "number", "money", "Money", null, "Visible" },
                    { 370, true, true, (short)7, "System.Decimal", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "BaseCurrencyBalance", new Guid("88455368-f053-4192-89e3-bb2c75c4b639"), "number", "money", "Money", null, "Visible" },
                    { 399, true, true, (short)9, "System.String", "", "", false, false, false, 128, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "DetailAccountFullCode", new Guid("37023636-1c82-4c63-a9e7-1f3279f127ca"), "string", "nvarchar", "", null, "Hidden" },
                    { 345, true, true, (short)1, "System.String", "", "", false, false, false, 128, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "CurrencyName", new Guid("f9868a93-d5b3-4c6d-b81f-f5565997654c"), "string", "nvarchar", "", null, "Visible" },
                    { 400, true, true, (short)10, "System.String", "", "", false, false, false, 128, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "DetailAccountName", new Guid("92e6fea5-d571-4aff-a889-1610564c1e2f"), "string", "nvarchar", "", null, "Hidden" },
                    { 402, true, true, (short)12, "System.String", "", "", false, false, false, 128, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "CostCenterName", new Guid("e8ee51f4-6da0-465e-a13f-e901c5bc333d"), "string", "nvarchar", "", null, "Hidden" },
                    { 432, true, true, (short)9, "System.String", "", "", false, false, false, 128, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "BranchName", new Guid("70beded6-0610-4039-bb9d-7dbcf2be028b"), "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 433, true, true, (short)-1, "System.String", "", "", false, false, true, 64, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "VoucherReference", new Guid("4124b74c-5835-46ac-ac5b-da63f2fbf2e0"), "string", "nvarchar", "", null, "AlwaysHidden" },
                    { 434, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "RowNo", new Guid("442b83e4-5610-4255-ab41-f7583721f09a"), "number", "int", "", null, "AlwaysVisible" },
                    { 435, true, true, (short)1, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "DetailAccountFullCode", new Guid("28e1ce96-142c-4199-a269-93ba95aa9332"), "string", "nvarchar", "", null, "Visible" },
                    { 436, true, true, (short)2, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "DetailAccountName", new Guid("bd6505a0-b6e3-4424-8b15-b9a21b03a83f"), "string", "nvarchar", "", null, "Visible" },
                    { 437, true, true, (short)3, "System.Decimal", "", "StartBalance", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "StartBalanceDebit", new Guid("6fdf5362-444e-498e-ab4b-4c0a168647e0"), "number", "money", "Money", null, "Visible" },
                    { 438, true, true, (short)4, "System.Decimal", "", "StartBalance", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "StartBalanceCredit", new Guid("d0825a84-3871-4d0c-a035-8c41230c1c7f"), "number", "money", "Money", null, "Visible" },
                    { 439, true, true, (short)5, "System.Decimal", "", "Turnover", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "TurnoverDebit", new Guid("395a3a7f-c845-46f1-8249-e9cd89023ec3"), "number", "money", "Money", null, "Visible" },
                    { 440, true, true, (short)6, "System.Decimal", "", "Turnover", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "TurnoverCredit", new Guid("9b3f7fc1-d631-4121-8401-3a71fa67a5b2"), "number", "money", "Money", null, "Visible" },
                    { 441, true, true, (short)7, "System.Decimal", "", "OperationSum", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "OperationSumDebit", new Guid("e5c4f5cd-f052-42c8-8ae3-be4e8bf272cd"), "number", "money", "Money", null, "Visible" },
                    { 442, true, true, (short)8, "System.Decimal", "", "OperationSum", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "OperationSumCredit", new Guid("caa7877f-0b10-4853-be86-4777ae937248"), "number", "money", "Money", null, "Visible" },
                    { 431, true, true, (short)8, "System.Decimal", "", "EndBalance", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "EndBalanceCredit", new Guid("9aa495c9-2e3e-4273-85d6-f5b2228e04c7"), "number", "money", "Money", null, "Visible" },
                    { 443, true, true, (short)9, "System.Decimal", "", "EndBalance", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "EndBalanceDebit", new Guid("e8ba0623-1473-463f-a0dc-a74eb0fe96e6"), "number", "money", "Money", null, "Visible" }
                });

            migrationBuilder.InsertData(
                schema: "Metadata",
                table: "Column",
                columns: new[] { "ColumnID", "AllowFiltering", "AllowSorting", "DisplayIndex", "DotNetType", "Expression", "GroupName", "IsDynamic", "IsFixedLength", "IsNullable", "Length", "MinLength", "ModifiedDate", "Name", "rowguid", "ScriptType", "StorageType", "Type", "ViewID", "Visibility" },
                values: new object[,]
                {
                    { 445, true, true, (short)11, "System.String", "", "", false, false, false, 128, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "BranchName", new Guid("7ea0a6fe-d8f2-41a5-a51f-25a35b588c33"), "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 446, true, true, (short)-1, "System.String", "", "", false, false, true, 64, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "VoucherReference", new Guid("5aaaae85-e459-4e9c-b32e-f5ca3b61ca2a"), "string", "nvarchar", "", null, "AlwaysHidden" },
                    { 447, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "RowNo", new Guid("c6786fc2-9636-4d33-9bff-d5cd908c18d0"), "number", "int", "", null, "AlwaysVisible" },
                    { 448, true, true, (short)1, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "DetailAccountFullCode", new Guid("bc6cb141-52f7-4d87-ac4a-aa37b7cca964"), "string", "nvarchar", "", null, "Visible" },
                    { 449, true, true, (short)2, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "DetailAccountName", new Guid("07769009-019b-4987-8e14-dcd2a707f4a3"), "string", "nvarchar", "", null, "Visible" },
                    { 450, true, true, (short)3, "System.Decimal", "", "StartBalance", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "StartBalanceDebit", new Guid("e0999f73-0279-4a89-bb90-d8a663e0d6dc"), "number", "money", "Money", null, "Visible" },
                    { 451, true, true, (short)4, "System.Decimal", "", "StartBalance", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "StartBalanceCredit", new Guid("313d02a2-6700-4471-8061-8f2928efc117"), "number", "money", "Money", null, "Visible" },
                    { 452, true, true, (short)5, "System.Decimal", "", "Turnover", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "TurnoverDebit", new Guid("dd17070c-640a-42d3-995a-a79713145bfb"), "number", "money", "Money", null, "Visible" },
                    { 453, true, true, (short)6, "System.Decimal", "", "Turnover", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "TurnoverCredit", new Guid("5f382dd3-711c-4ea4-b71d-8085ab04dcd7"), "number", "money", "Money", null, "Visible" },
                    { 454, true, true, (short)7, "System.Decimal", "", "OperationSum", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "OperationSumDebit", new Guid("ca140900-f3f0-4e41-95a7-029e652707a0"), "number", "money", "Money", null, "Visible" },
                    { 455, true, true, (short)8, "System.Decimal", "", "OperationSum", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "OperationSumCredit", new Guid("b039b181-1155-484a-ab81-841df3f743fb"), "number", "money", "Money", null, "Visible" },
                    { 444, true, true, (short)10, "System.Decimal", "", "EndBalance", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "EndBalanceCredit", new Guid("2e8a1ac3-5f68-455a-bd19-bd44d30527a4"), "number", "money", "Money", null, "Visible" },
                    { 430, true, true, (short)7, "System.Decimal", "", "EndBalance", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "EndBalanceDebit", new Guid("bac3653a-7d2d-412c-8740-786d31c9cbe1"), "number", "money", "Money", null, "Visible" },
                    { 429, true, true, (short)6, "System.Decimal", "", "Turnover", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "TurnoverCredit", new Guid("87fb0d0a-f78c-4cf6-a3b5-87337fe92fa6"), "number", "money", "Money", null, "Visible" },
                    { 428, true, true, (short)5, "System.Decimal", "", "Turnover", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "TurnoverDebit", new Guid("527fa354-0e2e-4dc9-bbd6-3aa3bd8b2435"), "number", "money", "Money", null, "Visible" },
                    { 403, true, true, (short)13, "System.String", "", "", false, false, false, 128, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "ProjectFullCode", new Guid("9913d980-b9fa-4aa9-b29c-0cc2df5360fb"), "string", "nvarchar", "", null, "Hidden" },
                    { 404, true, true, (short)14, "System.String", "", "", false, false, false, 128, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "ProjectName", new Guid("9c20b5ad-5d0d-41ff-a935-38b8bb68a697"), "string", "nvarchar", "", null, "Hidden" },
                    { 405, true, true, (short)15, "System.String", "", "", false, false, false, 64, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "CurrencyName", new Guid("ec4d18b9-5065-4ddf-824f-9ebe56786950"), "string", "nvarchar", "", null, "Hidden" },
                    { 406, true, true, (short)16, "System.Decimal", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "CurrencyValue", new Guid("8828de39-1836-4981-8a09-ce6f3c568f6f"), "number", "money", "Money", null, "Hidden" },
                    { 407, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "RowNo", new Guid("adbea225-93a1-4610-8e5e-5d4c0fa24396"), "number", "int", "", null, "AlwaysVisible" },
                    { 408, true, true, (short)1, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "DetailAccountFullCode", new Guid("5363fd33-98db-4546-8cd2-b92da42282c6"), "string", "nvarchar", "", null, "Visible" },
                    { 229, true, true, (short)4, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "AccountName", new Guid("e58819ea-852e-4a41-8a7f-719382de47b7"), "string", "nvarchar", "", null, "Visible" },
                    { 410, true, true, (short)3, "System.Decimal", "", "EndBalance", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "EndBalanceDebit", new Guid("d75132dc-6a3f-4f09-b906-3f4674b94a09"), "number", "money", "Money", null, "Visible" },
                    { 411, true, true, (short)4, "System.Decimal", "", "EndBalance", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "EndBalanceCredit", new Guid("6c73ea91-4868-43ea-91dc-b7f4a2124e4d"), "number", "money", "Money", null, "Visible" },
                    { 412, true, true, (short)5, "System.String", "", "", false, false, false, 128, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "BranchName", new Guid("1a5f0263-3857-4723-bc44-8db4c9dcfe59"), "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 413, true, true, (short)-1, "System.String", "", "", false, false, true, 64, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "VoucherReference", new Guid("119dce70-6df4-4f8b-a986-3481fc59f811"), "string", "nvarchar", "", null, "AlwaysHidden" },
                    { 414, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "RowNo", new Guid("52552570-9827-440b-a927-e7086f033141"), "number", "int", "", null, "AlwaysVisible" },
                    { 415, true, true, (short)1, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "DetailAccountFullCode", new Guid("d0f9de85-750f-4633-aa9a-0ff9e536068d"), "string", "nvarchar", "", null, "Visible" },
                    { 416, true, true, (short)2, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "DetailAccountName", new Guid("f021fcea-2e02-46a2-b8ee-431d82b2bfed"), "string", "nvarchar", "", null, "Visible" },
                    { 417, true, true, (short)3, "System.Decimal", "", "Turnover", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "TurnoverDebit", new Guid("b2431211-c425-426f-af2e-3e73dd42269c"), "number", "money", "Money", null, "Visible" },
                    { 418, true, true, (short)4, "System.Decimal", "", "Turnover", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "TurnoverCredit", new Guid("36c02126-a1b8-4352-a89f-02a4450dddaa"), "number", "money", "Money", null, "Visible" },
                    { 419, true, true, (short)5, "System.Decimal", "", "EndBalance", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "EndBalanceDebit", new Guid("edbf3532-8549-4255-9d2f-e24fa6599a10"), "number", "money", "Money", null, "Visible" },
                    { 420, true, true, (short)6, "System.Decimal", "", "EndBalance", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "EndBalanceCredit", new Guid("82d33bb6-c058-496e-b171-402c28b7dc20"), "number", "money", "Money", null, "Visible" },
                    { 421, true, true, (short)7, "System.String", "", "", false, false, false, 128, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "BranchName", new Guid("5c367551-7bc1-480a-906e-a73b4dac7340"), "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 422, true, true, (short)-1, "System.String", "", "", false, false, true, 64, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "VoucherReference", new Guid("fb2c74c0-129b-44b5-bdb3-a765abd4da1d"), "string", "nvarchar", "", null, "AlwaysHidden" },
                    { 423, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "RowNo", new Guid("7d9619eb-eea2-41df-9243-529c7076c94d"), "number", "int", "", null, "AlwaysVisible" },
                    { 424, true, true, (short)1, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "DetailAccountFullCode", new Guid("64e13eed-6bae-40aa-8320-2601c9c6d90a"), "string", "nvarchar", "", null, "Visible" },
                    { 425, true, true, (short)2, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "DetailAccountName", new Guid("84ffc04e-a770-4354-9247-72410aa2d56a"), "string", "nvarchar", "", null, "Visible" },
                    { 426, true, true, (short)3, "System.Decimal", "", "StartBalance", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "StartBalanceDebit", new Guid("e1c1c629-85d1-420c-b92f-9536bb219e8a"), "number", "money", "Money", null, "Visible" },
                    { 427, true, true, (short)4, "System.Decimal", "", "StartBalance", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "StartBalanceCredit", new Guid("223ab53b-89a7-4275-82c0-e418a6b96927"), "number", "money", "Money", null, "Visible" },
                    { 401, true, true, (short)11, "System.String", "", "", false, false, false, 128, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "CostCenterFullCode", new Guid("43a02bb7-d216-4216-9255-337707ffa8cd"), "string", "nvarchar", "", null, "Hidden" },
                    { 344, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "RowNo", new Guid("f7be179e-bacf-477d-ba34-ac52674d8c65"), "number", "int", "", null, "AlwaysVisible" }
                });

            migrationBuilder.InsertData(
                schema: "Metadata",
                table: "Column",
                columns: new[] { "ColumnID", "AllowFiltering", "AllowSorting", "DisplayIndex", "DotNetType", "Expression", "GroupName", "IsDynamic", "IsFixedLength", "IsNullable", "Length", "MinLength", "ModifiedDate", "Name", "rowguid", "ScriptType", "StorageType", "Type", "ViewID", "Visibility" },
                values: new object[,]
                {
                    { 409, true, true, (short)2, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 16, 15, 5, 55, 0, DateTimeKind.Unspecified), "DetailAccountName", new Guid("754d98ee-5f55-4579-bbb3-e570df03216d"), "string", "nvarchar", "", null, "Visible" },
                    { 342, true, true, (short)13, "System.String", "", "", false, false, false, 128, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "BranchName", new Guid("6a2d8d6e-3085-48ac-96af-43ee54c59810"), "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 260, true, true, (short)2, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "LineCount", new Guid("d428aa1f-dbef-463a-be37-09f6aad023c3"), "number", "int", "", null, "Visible" },
                    { 261, true, true, (short)3, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "Description", new Guid("fb237efb-e391-4ffe-9326-3e057141cda3"), "string", "nvarchar", "", null, "Visible" },
                    { 262, true, true, (short)4, "System.Decimal", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "Debit", new Guid("b69b1f21-5ad0-49b5-b7e2-b55c68e680fd"), "number", "money", "Money", null, "Visible" },
                    { 263, true, true, (short)5, "System.Decimal", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "Credit", new Guid("c1d54d8c-bdbf-4615-a798-b0fbf7d49bbf"), "number", "money", "Money", null, "Visible" },
                    { 264, false, false, (short)6, "System.Decimal", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "Balance", new Guid("633261fe-d26d-4a43-a902-07506ee3ef28"), "number", "money", "Money", null, "Visible" },
                    { 265, true, true, (short)7, "System.String", "", "", false, false, false, 128, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "BranchName", new Guid("01210ced-3bcc-4759-9a86-437ac909a37a"), "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 266, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "Id", new Guid("397f3702-ee92-4f63-8074-cd0b22064e51"), "number", "int", "", null, "AlwaysHidden" },
                    { 267, true, true, (short)-1, "System.Int16", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "BranchScope", new Guid("ee822695-3eef-4c14-8da6-f5041698cbfd"), "number", "smallint", "", null, "AlwaysHidden" },
                    { 268, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "BranchId", new Guid("56f60506-79f8-4ccb-b749-04d47c5c22a4"), "number", "int", "", null, "AlwaysHidden" },
                    { 269, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "TaxCode", new Guid("5e1d433e-9c27-41b2-905a-3de760b36311"), "number", "int", "", null, "AlwaysHidden" },
                    { 270, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "RowNo", new Guid("ffa331b4-ab6a-4ba9-be84-18786138f4df"), "number", "int", "", null, "AlwaysVisible" },
                    { 259, true, true, (short)1, "System.Date", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "VoucherDate", new Guid("99dc4c60-7634-4722-8ddd-e3a021b36596"), "Date", "datetime", "Default", null, "Visible" },
                    { 271, true, true, (short)1, "System.String", "", "", false, false, false, 64, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "Name", new Guid("a65f30b1-bd50-489e-86cc-f251762cacdb"), "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 274, true, true, (short)2, "System.String", "", "", false, false, false, 16, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "MinorUnit", new Guid("0dba0ae5-0968-418a-9ecd-9f04936d9ff1"), "string", "nvarchar", "", null, "Visible" },
                    { 275, true, true, (short)3, "System.String", "", "", false, false, true, 512, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "Description", new Guid("1f4eeef8-2fd7-4509-b0fd-395bdec81227"), "string", "nvarchar", "", null, "Visible" },
                    { 276, true, true, (short)4, "System.String", "", "", false, false, false, 128, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "BranchName", new Guid("f9cfb4b1-1549-4c4d-bbfc-112aebed9f66"), "string", "nvarchar", "", null, "Visible" },
                    { 277, true, true, (short)5, "System.Int16", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "DecimalCount", new Guid("d28ed6fb-9457-4f59-8603-0319673e246f"), "number", "smallint", "", null, "Hidden" },
                    { 278, true, true, (short)7, "System.String", "", "", false, false, true, 32, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "State", new Guid("a89bbc64-010e-4f34-b870-3f5827790839"), "string", "nvarchar", "", null, "Visible" },
                    { 279, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "Id", new Guid("d8fe77c4-46a0-4ad9-b5a6-8f5d1612c1d5"), "number", "int", "", null, "AlwaysHidden" },
                    { 280, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "CurrencyId", new Guid("38d18e1b-e3e3-40ec-8c46-d6e5e90ad358"), "number", "int", "", null, "AlwaysHidden" },
                    { 281, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "BranchId", new Guid("2bff806d-4d54-4ddf-bd7b-f355e3aae0a9"), "number", "int", "", null, "AlwaysHidden" },
                    { 282, true, true, (short)-1, "System.Int16", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "BranchScope", new Guid("3c7aada6-078f-4f73-8345-bbd113f39d2b"), "number", "smallint", "", null, "AlwaysHidden" },
                    { 343, true, true, (short)-1, "System.String", "", "", false, false, true, 64, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "VoucherReference", new Guid("b32e0bad-70ed-45e5-b805-8a670faff4d7"), "string", "nvarchar", "", null, "AlwaysHidden" },
                    { 284, true, true, (short)1, "System.Date", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "Date", new Guid("ecc8d105-92b1-4c01-bcc9-d7d8ae6c7d95"), "Date", "datetime", "Default", null, "Visible" },
                    { 272, true, true, (short)2, "System.String", "", "", false, false, false, 8, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "Code", new Guid("c45fc1ca-231f-4222-86d4-5c1267920273"), "string", "nvarchar", "", null, "Visible" },
                    { 258, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "RowNo", new Guid("ca31b63c-b289-42d4-bb8e-8bb6fbad3a33"), "number", "int", "", null, "AlwaysVisible" },
                    { 257, true, true, (short)7, "System.String", "", "", false, false, false, 128, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "BranchName", new Guid("9daaa08f-d9c2-4027-b48a-73f9febf6ba8"), "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 256, false, false, (short)6, "System.Decimal", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "Balance", new Guid("50e259f0-4c9e-4eda-ad9e-b061561a408e"), "number", "money", "Money", null, "Visible" },
                    { 231, true, true, (short)6, "System.Decimal", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "Debit", new Guid("f3fb4a3e-d007-4b31-9c2a-e6e59de1d54f"), "number", "money", "Money", null, "Visible" },
                    { 232, true, true, (short)7, "System.Decimal", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "Credit", new Guid("bc952180-4df6-4636-a884-6a2c6eb516dc"), "number", "money", "Money", null, "Visible" },
                    { 233, true, true, (short)8, "System.String", "", "", false, false, false, 128, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "BranchName", new Guid("68543589-078a-4950-9cc5-992e20e01a02"), "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 234, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "RowNo", new Guid("aea4c3bc-1766-4f82-9214-93d3768b71fa"), "number", "int", "", null, "AlwaysVisible" },
                    { 235, true, true, (short)1, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "AccountFullCode", new Guid("cea7eadd-3b75-4e71-85eb-5bfa1c671445"), "string", "nvarchar", "", null, "Visible" },
                    { 236, true, true, (short)2, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "AccountName", new Guid("d8e29cf1-643e-4c78-aa10-c5d0676b9291"), "string", "nvarchar", "", null, "Visible" },
                    { 237, true, true, (short)3, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "Description", new Guid("5328526c-857d-47ed-81cd-c8d982e6d04f"), "string", "nvarchar", "", null, "Visible" },
                    { 238, true, true, (short)4, "System.Decimal", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "Debit", new Guid("1c94777d-e8c3-4638-9eb1-cb7af5622509"), "number", "money", "Money", null, "Visible" },
                    { 239, true, true, (short)5, "System.Decimal", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "Credit", new Guid("884e95ba-3632-444d-8cd4-4c74aa58880c"), "number", "money", "Money", null, "Visible" },
                    { 240, true, true, (short)6, "System.String", "", "", false, false, false, 128, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "BranchName", new Guid("91adaee0-f167-44d0-8b37-0fcf0325f004"), "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 241, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "RowNo", new Guid("f824f52d-75fb-4cc5-bb1b-1d3bdcc70eba"), "number", "int", "", null, "AlwaysVisible" },
                    { 242, true, true, (short)1, "System.Date", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "VoucherDate", new Guid("9bc3b239-aa19-469f-a266-94a3e88c2eb7"), "Date", "datetime", "Default", null, "Visible" }
                });

            migrationBuilder.InsertData(
                schema: "Metadata",
                table: "Column",
                columns: new[] { "ColumnID", "AllowFiltering", "AllowSorting", "DisplayIndex", "DotNetType", "Expression", "GroupName", "IsDynamic", "IsFixedLength", "IsNullable", "Length", "MinLength", "ModifiedDate", "Name", "rowguid", "ScriptType", "StorageType", "Type", "ViewID", "Visibility" },
                values: new object[,]
                {
                    { 243, true, true, (short)2, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "VoucherNo", new Guid("af1d0576-62be-408c-a498-d00fd2af7fd8"), "number", "int", "", null, "Visible" },
                    { 244, true, true, (short)3, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "Description", new Guid("45e1de8e-89b3-4e58-b3c1-bf29eeef2cb5"), "string", "nvarchar", "", null, "Visible" },
                    { 245, true, true, (short)4, "System.Decimal", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "Debit", new Guid("733870c9-1181-4188-9218-04d041f6fb74"), "number", "money", "Money", null, "Visible" },
                    { 246, true, true, (short)5, "System.Decimal", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "Credit", new Guid("5a6852bd-73ce-4036-9df6-377577169b55"), "number", "money", "Money", null, "Visible" },
                    { 247, false, false, (short)6, "System.Decimal", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "Balance", new Guid("901fd58c-6994-48ee-80d5-1516660de87b"), "number", "money", "Money", null, "Visible" },
                    { 248, true, true, (short)7, "System.String", "", "", false, false, false, 128, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "Mark", new Guid("1cf95175-ceb2-46d1-adc4-726047c35f33"), "string", "nvarchar", "", null, "Visible" },
                    { 249, true, true, (short)8, "System.String", "", "", false, false, false, 128, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "BranchName", new Guid("3c0ea0ad-d6b0-4f30-a1d9-81a8fcf1a4f3"), "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 250, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "RowNo", new Guid("56b45522-40da-4f30-88f2-a27654351d21"), "number", "int", "", null, "AlwaysVisible" },
                    { 251, true, true, (short)1, "System.Date", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "VoucherDate", new Guid("949bd4c4-6b77-4cd2-81a8-b0a918c8f091"), "Date", "datetime", "Default", null, "Visible" },
                    { 252, true, true, (short)2, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "VoucherNo", new Guid("8b11695c-0cf7-4a29-b29b-062cd7598646"), "number", "int", "", null, "Visible" },
                    { 253, true, true, (short)3, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "Description", new Guid("d376c712-759d-4cfd-8739-15764d142f78"), "string", "nvarchar", "", null, "Visible" },
                    { 254, true, true, (short)4, "System.Decimal", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "Debit", new Guid("8ed97dc1-46d1-46cc-869c-a396cc8cfb8a"), "number", "money", "Money", null, "Visible" },
                    { 255, true, true, (short)5, "System.Decimal", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "Credit", new Guid("ff94d8fe-5da1-4e3b-9a7b-230a28481d17"), "number", "money", "Money", null, "Visible" },
                    { 285, true, true, (short)2, "System.TimeSpan", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "Time", new Guid("5b4abf4d-df03-43e4-b263-68a391f724e4"), "number", "time", "", null, "Visible" },
                    { 286, true, true, (short)3, "System.decimal", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "Multiplier", new Guid("3486ba0b-b55d-4226-b9ee-82bf8d8d2914"), "number", "money", "Money", null, "Visible" },
                    { 283, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "RowNo", new Guid("15aef42c-dc63-40a3-a2e2-8e46884e036b"), "number", "int", "", null, "AlwaysVisible" },
                    { 288, true, true, (short)5, "System.String", "", "", false, false, true, 512, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "Description", new Guid("28477406-43a9-4042-82f3-6c2b41af84ed"), "string", "nvarchar", "", null, "Visible" },
                    { 317, true, true, (short)1, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "AccountFullCode", new Guid("15ad6b56-5f01-42cc-92c8-32121f87495a"), "string", "nvarchar", "", null, "Visible" },
                    { 287, true, true, (short)4, "System.String", "", "", false, false, false, 128, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "BranchName", new Guid("63b49c1b-2715-4618-9481-bb2bbd20ab0c"), "string", "nvarchar", "", null, "Visible" },
                    { 319, true, true, (short)3, "System.Decimal", "", "StartBalance", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "StartBalanceDebit", new Guid("dc2ba280-63f7-4e18-aacd-8f27361d469a"), "number", "money", "Money", null, "Visible" },
                    { 320, true, true, (short)4, "System.Decimal", "", "StartBalance", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "StartBalanceCredit", new Guid("2caf7b89-ad0c-47e0-b02f-c974bd02edc6"), "number", "money", "Money", null, "Visible" },
                    { 321, true, true, (short)5, "System.Decimal", "", "Turnover", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "TurnoverDebit", new Guid("9b9cb684-ec11-4eb1-933c-487438e93715"), "number", "money", "Money", null, "Visible" },
                    { 322, true, true, (short)6, "System.Decimal", "", "Turnover", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "TurnoverCredit", new Guid("83465246-305b-4828-8c1a-3fb8ea209a73"), "number", "money", "Money", null, "Visible" },
                    { 323, true, true, (short)7, "System.Decimal", "", "OperationSum", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "OperationSumDebit", new Guid("1ec05e55-bdb9-46c8-ad8b-3b71bdcd7936"), "number", "money", "Money", null, "Visible" },
                    { 324, true, true, (short)8, "System.Decimal", "", "OperationSum", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "OperationSumCredit", new Guid("bdbddbf3-8bac-4440-9964-f4cebaac4b3c"), "number", "money", "Money", null, "Visible" },
                    { 325, true, true, (short)9, "System.Decimal", "", "EndBalance", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "EndBalanceDebit", new Guid("7f2954d0-cdbb-483f-8dc0-7a68c918ac20"), "number", "money", "Money", null, "Visible" },
                    { 326, true, true, (short)10, "System.Decimal", "", "EndBalance", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "EndBalanceCredit", new Guid("b81e460f-782a-4ac7-a509-1ab619d10e05"), "number", "money", "Money", null, "Visible" },
                    { 327, true, true, (short)11, "System.String", "", "", false, false, false, 128, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "BranchName", new Guid("ec957bb3-25bc-4b30-87c0-030b7b5f9a4c"), "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 328, true, true, (short)-1, "System.String", "", "", false, false, true, 64, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "VoucherReference", new Guid("38ed0f39-bc1a-4afa-85b9-2a123090bdff"), "string", "nvarchar", "", null, "AlwaysHidden" },
                    { 329, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "RowNo", new Guid("183003b8-18e7-4ebd-aff0-1ad722c19d40"), "number", "int", "", null, "AlwaysVisible" },
                    { 330, true, true, (short)1, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "AccountFullCode", new Guid("9c767745-ae7b-4e41-ba06-7ec817a241b0"), "string", "nvarchar", "", null, "Visible" },
                    { 331, true, true, (short)2, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "AccountName", new Guid("8e1bb1c6-2c9a-4e0f-bc1e-be7029128ef8"), "string", "nvarchar", "", null, "Visible" },
                    { 332, true, true, (short)3, "System.Decimal", "", "StartBalance", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "StartBalanceDebit", new Guid("d13735af-6f95-44e7-8868-e57b13bf2172"), "number", "money", "Money", null, "Visible" },
                    { 333, true, true, (short)4, "System.Decimal", "", "StartBalance", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "StartBalanceCredit", new Guid("d7823a15-eba9-44eb-972d-7e07f2c8b821"), "number", "money", "Money", null, "Visible" },
                    { 334, true, true, (short)5, "System.Decimal", "", "Turnover", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "TurnoverDebit", new Guid("27fab610-477f-44c9-876c-5d5a46a1383d"), "number", "money", "Money", null, "Visible" },
                    { 335, true, true, (short)6, "System.Decimal", "", "Turnover", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "TurnoverCredit", new Guid("fd47dfab-901e-4791-91ba-6fe32ccdd4f1"), "number", "money", "Money", null, "Visible" },
                    { 336, true, true, (short)7, "System.Decimal", "", "OperationSum", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "OperationSumDebit", new Guid("f73bb2c7-844e-479b-a126-db61fdc143dc"), "number", "money", "Money", null, "Visible" },
                    { 337, true, true, (short)8, "System.Decimal", "", "OperationSum", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "OperationSumCredit", new Guid("bcb9e6a1-eb48-4b10-943d-896a4b76975f"), "number", "money", "Money", null, "Visible" },
                    { 338, true, true, (short)9, "System.Decimal", "", "Corrections", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "CorrectionsDebit", new Guid("7521a0ae-6f11-44af-b94c-3c7efa8513a2"), "number", "money", "Money", null, "Visible" },
                    { 339, true, true, (short)10, "System.Decimal", "", "Corrections", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "CorrectionsCredit", new Guid("93618583-350d-4b21-9c2c-dd521fd03187"), "number", "money", "Money", null, "Visible" },
                    { 340, true, true, (short)11, "System.Decimal", "", "EndBalance", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "EndBalanceDebit", new Guid("f03083ef-d53f-4130-83ff-65f59c1cfed5"), "number", "money", "Money", null, "Visible" },
                    { 341, true, true, (short)12, "System.Decimal", "", "EndBalance", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "EndBalanceCredit", new Guid("51e9c447-54d4-4fb7-8de5-dbfc5e4eb647"), "number", "money", "Money", null, "Visible" }
                });

            migrationBuilder.InsertData(
                schema: "Metadata",
                table: "Column",
                columns: new[] { "ColumnID", "AllowFiltering", "AllowSorting", "DisplayIndex", "DotNetType", "Expression", "GroupName", "IsDynamic", "IsFixedLength", "IsNullable", "Length", "MinLength", "ModifiedDate", "Name", "rowguid", "ScriptType", "StorageType", "Type", "ViewID", "Visibility" },
                values: new object[,]
                {
                    { 316, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "RowNo", new Guid("d93609f5-cb24-4f6f-96ed-842b23e6a030"), "number", "int", "", null, "AlwaysVisible" },
                    { 315, true, true, (short)-1, "System.String", "", "", false, false, true, 64, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "VoucherReference", new Guid("92763c99-b896-495c-9002-a5596a15cc53"), "string", "nvarchar", "", null, "AlwaysHidden" },
                    { 318, true, true, (short)2, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "AccountName", new Guid("a9ff84ed-aee1-438e-8b7e-b73eb537fa6a"), "string", "nvarchar", "", null, "Visible" },
                    { 313, true, true, (short)8, "System.Decimal", "", "EndBalance", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "EndBalanceCredit", new Guid("f318f297-c42c-44e8-8cf7-52f23c6a3987"), "number", "money", "Money", null, "Visible" },
                    { 289, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "RowNo", new Guid("fee705b0-cb2a-4af6-953b-81288fcdc6b9"), "number", "int", "", null, "AlwaysVisible" },
                    { 290, true, true, (short)1, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "AccountFullCode", new Guid("2c46133b-70de-4fc8-8fc5-7d0d4b5d6fa0"), "string", "nvarchar", "", null, "Visible" },
                    { 291, true, true, (short)2, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "AccountName", new Guid("d4220f5e-4b78-41a2-8716-f03dc1e0232d"), "string", "nvarchar", "", null, "Visible" },
                    { 292, true, true, (short)3, "System.Decimal", "", "EndBalance", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "EndBalanceDebit", new Guid("e14f5df4-2ca8-4b2a-a840-c166852caace"), "number", "money", "Money", null, "Visible" },
                    { 293, true, true, (short)4, "System.Decimal", "", "EndBalance", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "EndBalanceCredit", new Guid("51bf554b-c233-43f4-aeee-b3ba09c50352"), "number", "money", "Money", null, "Visible" },
                    { 294, true, true, (short)5, "System.String", "", "", false, false, false, 128, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "BranchName", new Guid("f5655912-473d-4490-8836-c7f9f882c718"), "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 295, true, true, (short)-1, "System.String", "", "", false, false, true, 64, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "VoucherReference", new Guid("69c43e10-bd51-4bac-8ca1-2777492794e6"), "string", "nvarchar", "", null, "AlwaysHidden" },
                    { 314, true, true, (short)9, "System.String", "", "", false, false, false, 128, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "BranchName", new Guid("bd9caec7-3e08-4348-9729-df6c1c87c963"), "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 297, true, true, (short)1, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "AccountFullCode", new Guid("e2e77a35-fe94-4dc1-b152-0c9961024d3e"), "string", "nvarchar", "", null, "Visible" },
                    { 298, true, true, (short)2, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "AccountName", new Guid("2b95bfd6-382f-4770-9c24-a0fde27133e2"), "string", "nvarchar", "", null, "Visible" },
                    { 299, true, true, (short)3, "System.Decimal", "", "Turnover", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "TurnoverDebit", new Guid("67b2de6b-eef9-4b05-9f3d-2c92191efc53"), "number", "money", "Money", null, "Visible" },
                    { 300, true, true, (short)4, "System.Decimal", "", "Turnover", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "TurnoverCredit", new Guid("0749e494-7ffb-4def-86d7-501347f1c919"), "number", "money", "Money", null, "Visible" },
                    { 296, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "RowNo", new Guid("64c35243-eb81-4efc-b3e8-5167ec949f35"), "number", "int", "", null, "AlwaysVisible" },
                    { 302, true, true, (short)6, "System.Decimal", "", "EndBalance", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "EndBalanceCredit", new Guid("69e60abd-d45a-415f-830d-2bce4f5f5c79"), "number", "money", "Money", null, "Visible" },
                    { 312, true, true, (short)7, "System.Decimal", "", "EndBalance", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "EndBalanceDebit", new Guid("00145f85-ac42-4f8a-8617-f1c6e3d95497"), "number", "money", "Money", null, "Visible" },
                    { 301, true, true, (short)5, "System.Decimal", "", "EndBalance", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "EndBalanceDebit", new Guid("7a00d843-7016-49f5-8c53-4a2a2ac90ddb"), "number", "money", "Money", null, "Visible" },
                    { 310, true, true, (short)5, "System.Decimal", "", "Turnover", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "TurnoverDebit", new Guid("0447ec72-1a9a-4e1c-9907-59057affe1a4"), "number", "money", "Money", null, "Visible" },
                    { 309, true, true, (short)4, "System.Decimal", "", "StartBalance", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "StartBalanceCredit", new Guid("d4c42568-fa61-4b5f-ab93-8f4abb28f0ec"), "number", "money", "Money", null, "Visible" },
                    { 308, true, true, (short)3, "System.Decimal", "", "StartBalance", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "StartBalanceDebit", new Guid("d97f4662-2f69-4b0f-88cf-10ee1adbc376"), "number", "money", "Money", null, "Visible" },
                    { 311, true, true, (short)6, "System.Decimal", "", "Turnover", false, false, false, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "TurnoverCredit", new Guid("e7e23eea-a5d9-41b2-acd6-b371e4805ea9"), "number", "money", "Money", null, "Visible" },
                    { 306, true, true, (short)1, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "AccountFullCode", new Guid("24986c4a-9540-47d3-9ad0-a13f1dae23fb"), "string", "nvarchar", "", null, "Visible" },
                    { 305, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "RowNo", new Guid("d5147887-6c91-418e-a6b5-a6bdaaf3d4c0"), "number", "int", "", null, "AlwaysVisible" },
                    { 304, true, true, (short)-1, "System.String", "", "", false, false, true, 64, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "VoucherReference", new Guid("6df573d0-4337-4923-979c-7ca1c73360e2"), "string", "nvarchar", "", null, "AlwaysHidden" },
                    { 303, true, true, (short)7, "System.String", "", "", false, false, false, 128, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "BranchName", new Guid("9e3768e2-8e7f-4467-968c-6ff9f3f134aa"), "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 307, true, true, (short)2, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 16, 15, 5, 54, 0, DateTimeKind.Unspecified), "AccountName", new Guid("e976672c-5dcc-46f5-92ee-9d2c9206e1c8"), "string", "nvarchar", "", null, "Visible" }
                });

            migrationBuilder.InsertData(
                schema: "Metadata",
                table: "Command",
                columns: new[] { "CommandID", "HotKey", "IconName", "Index", "ModifiedDate", "ParentId", "PermissionId", "RouteUrl", "rowguid", "TitleKey" },
                values: new object[,]
                {
                    { 1, "", "folder-close", null, new DateTime(2023, 10, 16, 15, 56, 33, 0, DateTimeKind.Unspecified), null, null, "", new Guid("f1d27659-0ebe-4c13-8f3d-eedb0cb489ca"), "Accounting" },
                    { 23, "", "folder-close", null, new DateTime(2023, 10, 16, 15, 56, 33, 0, DateTimeKind.Unspecified), null, null, "", new Guid("a55b9700-4119-489e-98e7-be01bd18eb55"), "Organization" },
                    { 27, "", "folder-close", null, new DateTime(2023, 10, 16, 15, 56, 33, 0, DateTimeKind.Unspecified), null, null, "", new Guid("fdeebb09-3258-4c2e-88c0-0ee7997eef8f"), "Administration" },
                    { 37, "", "folder-close", null, new DateTime(2023, 10, 16, 15, 56, 33, 0, DateTimeKind.Unspecified), null, null, "", new Guid("8a53f38d-15c6-4f1b-bfe7-42f4d8678579"), "Tools" },
                    { 52, "", "folder-close", null, new DateTime(2023, 10, 16, 15, 56, 33, 0, DateTimeKind.Unspecified), null, null, "", new Guid("c55e41ea-2593-4e22-8cad-6a57b452836f"), "Treasury" },
                    { 100000, "", "folder-close", null, new DateTime(2023, 10, 16, 15, 56, 33, 0, DateTimeKind.Unspecified), null, null, "", new Guid("3945f2ca-6ba1-4c90-ac6d-096071469c6d"), "ProductScope" }
                });

            migrationBuilder.InsertData(
                schema: "Metadata",
                table: "EntityType",
                columns: new[] { "EntityTypeID", "Description", "Name", "rowguid" },
                values: new object[,]
                {
                    { 9, "", "UserReport", new Guid("09d85b66-d12d-4015-bf31-4a8143104598") },
                    { 8, "", "ViewRowPermission", new Guid("09d85b66-d12d-4015-bf31-4a8143104598") },
                    { 5, "", "SysOperationLog", new Guid("09d85b66-d12d-4015-bf31-4a8143104598") },
                    { 6, "", "User", new Guid("09d85b66-d12d-4015-bf31-4a8143104598") },
                    { 2, "", "Role", new Guid("eb27fb68-0d6c-4e8b-ba88-5f25f518c579") },
                    { 1, "", "CompanyDb", new Guid("7a078968-4470-49a7-aaa5-396ae400e02c") },
                    { 4, "", "Setting", new Guid("09d85b66-d12d-4015-bf31-4a8143104598") }
                });

            migrationBuilder.InsertData(
                schema: "Metadata",
                table: "Locale",
                columns: new[] { "LocaleID", "Code", "LocalName", "ModifiedDate", "Name" },
                values: new object[,]
                {
                    { 1, "en", "English", new DateTime(2023, 10, 16, 18, 36, 47, 637, DateTimeKind.Local).AddTicks(4276), "English" },
                    { 2, "fa", "فارسی", new DateTime(2023, 10, 16, 18, 36, 47, 637, DateTimeKind.Local).AddTicks(4393), "Persian" },
                    { 3, "ar", "العربیه", new DateTime(2023, 10, 16, 18, 36, 47, 637, DateTimeKind.Local).AddTicks(4399), "Arabic" },
                    { 4, "fr", "Français", new DateTime(2023, 10, 16, 18, 36, 47, 637, DateTimeKind.Local).AddTicks(4403), "French" }
                });

            migrationBuilder.InsertData(
                schema: "Metadata",
                table: "Operation",
                columns: new[] { "OperationID", "Description", "ModifiedDate", "Name" },
                values: new object[,]
                {
                    { 26, "", new DateTime(2023, 10, 16, 18, 36, 47, 629, DateTimeKind.Local).AddTicks(4079), "AssignRole" },
                    { 27, "", new DateTime(2023, 10, 16, 18, 36, 47, 629, DateTimeKind.Local).AddTicks(4082), "AssignUser" },
                    { 28, "", new DateTime(2023, 10, 16, 18, 36, 47, 629, DateTimeKind.Local).AddTicks(4086), "BranchAccess" },
                    { 29, "", new DateTime(2023, 10, 16, 18, 36, 47, 629, DateTimeKind.Local).AddTicks(4090), "FiscalPeriodAccess" },
                    { 57, "", new DateTime(2023, 10, 16, 18, 36, 47, 629, DateTimeKind.Local).AddTicks(4104), "CompanyAccess" },
                    { 35, "", new DateTime(2023, 10, 16, 18, 36, 47, 629, DateTimeKind.Local).AddTicks(4097), "RoleAccess" },
                    { 54, "", new DateTime(2023, 10, 16, 18, 36, 47, 629, DateTimeKind.Local).AddTicks(4100), "Export" },
                    { 25, "", new DateTime(2023, 10, 16, 18, 36, 47, 629, DateTimeKind.Local).AddTicks(4075), "SwitchBranch" },
                    { 58, "", new DateTime(2023, 10, 16, 18, 36, 47, 629, DateTimeKind.Local).AddTicks(4107), "PrintPreview" },
                    { 30, "", new DateTime(2023, 10, 16, 18, 36, 47, 629, DateTimeKind.Local).AddTicks(4094), "ViewArchive" },
                    { 24, "", new DateTime(2023, 10, 16, 18, 36, 47, 629, DateTimeKind.Local).AddTicks(4021), "SwitchFiscalPeriod" },
                    { 23, "", new DateTime(2023, 10, 16, 18, 36, 47, 629, DateTimeKind.Local).AddTicks(4018), "CompanyLogin" },
                    { 22, "", new DateTime(2023, 10, 16, 18, 36, 47, 629, DateTimeKind.Local).AddTicks(4015), "FailedLogin" },
                    { 1, "", new DateTime(2023, 10, 16, 18, 36, 47, 629, DateTimeKind.Local).AddTicks(3852), "View" },
                    { 2, "", new DateTime(2023, 10, 16, 18, 36, 47, 629, DateTimeKind.Local).AddTicks(3977), "Create" },
                    { 4, "", new DateTime(2023, 10, 16, 18, 36, 47, 629, DateTimeKind.Local).AddTicks(3986), "Delete" },
                    { 5, "", new DateTime(2023, 10, 16, 18, 36, 47, 629, DateTimeKind.Local).AddTicks(3991), "Filter" },
                    { 3, "", new DateTime(2023, 10, 16, 18, 36, 47, 629, DateTimeKind.Local).AddTicks(3983), "Edit" },
                    { 7, "", new DateTime(2023, 10, 16, 18, 36, 47, 629, DateTimeKind.Local).AddTicks(4000), "Save" },
                    { 8, "", new DateTime(2023, 10, 16, 18, 36, 47, 629, DateTimeKind.Local).AddTicks(4004), "Archive" },
                    { 10, "", new DateTime(2023, 10, 16, 18, 36, 47, 629, DateTimeKind.Local).AddTicks(4007), "Design" },
                    { 21, "", new DateTime(2023, 10, 16, 18, 36, 47, 629, DateTimeKind.Local).AddTicks(4011), "GroupDelete" },
                    { 6, "", new DateTime(2023, 10, 16, 18, 36, 47, 629, DateTimeKind.Local).AddTicks(3997), "Print" }
                });

            migrationBuilder.InsertData(
                schema: "Metadata",
                table: "OperationSource",
                columns: new[] { "OperationSourceID", "Description", "Name", "rowguid" },
                values: new object[,]
                {
                    { 7, "", "AppLogin", new Guid("7a078968-4470-49a7-aaa5-396ae400e02c") },
                    { 8, "", "AppEnvironment", new Guid("eb27fb68-0d6c-4e8b-ba88-5f25f518c579") },
                    { 14, "", "SystemSettings", new Guid("09d85b66-d12d-4015-bf31-4a8143104598") }
                });

            migrationBuilder.InsertData(
                schema: "Metadata",
                table: "OperationSourceType",
                columns: new[] { "OperationSourceTypeID", "Name", "rowguid" },
                values: new object[,]
                {
                    { 3, "Reports", new Guid("09d85b66-d12d-4015-bf31-4a8143104598") },
                    { 1, "BaseData", new Guid("7a078968-4470-49a7-aaa5-396ae400e02c") },
                    { 2, "OperationalForms", new Guid("eb27fb68-0d6c-4e8b-ba88-5f25f518c579") }
                });

            migrationBuilder.InsertData(
                schema: "Metadata",
                table: "Subsystem",
                columns: new[] { "SubsystemID", "ModifiedDate", "Name" },
                values: new object[,]
                {
                    { 3, new DateTime(2023, 10, 16, 18, 36, 47, 423, DateTimeKind.Local).AddTicks(5796), "Treasury" },
                    { 100000, new DateTime(2023, 10, 16, 18, 36, 47, 423, DateTimeKind.Local).AddTicks(5802), "ProductScope" },
                    { 1, new DateTime(2023, 10, 16, 18, 36, 47, 421, DateTimeKind.Local).AddTicks(6522), "Administration" },
                    { 2, new DateTime(2023, 10, 16, 18, 36, 47, 423, DateTimeKind.Local).AddTicks(5711), "Accounting" }
                });

            migrationBuilder.InsertData(
                schema: "Metadata",
                table: "View",
                columns: new[] { "ViewID", "EntityName", "EntityType", "FetchUrl", "IsCartableIntegrated", "IsHierarchy", "Name", "rowguid", "SearchUrl" },
                values: new object[,]
                {
                    { 45, "ItemBalance", "", "", false, false, "DetailAccountBalance6Column", new Guid("f56fee44-23f7-4dd9-8dfb-73447e6bbd27"), "" },
                    { 60, "SysOperationLogArchive", "", "", false, false, "SysOperationLogArchive", new Guid("9b605225-f68c-4ffd-bd72-36818fa2feef"), "" },
                    { 100003, "Property", "Base", "", false, false, "Property", new Guid("b35008e5-e0dd-4538-8036-65ef4d946070"), "" },
                    { 59, "SysOperationLog", "", "", false, false, "SysOperationLog", new Guid("e1c9cfb2-3b2a-414e-a349-a59f67ced8b8"), "" },
                    { 58, "BalanceByAccount", "", "", false, false, "BalanceByAccount", new Guid("c43dd5ed-8b2a-4f3b-b620-a25fb2b340b6"), "" }
                });

            migrationBuilder.InsertData(
                schema: "Metadata",
                table: "View",
                columns: new[] { "ViewID", "EntityName", "EntityType", "FetchUrl", "IsCartableIntegrated", "IsHierarchy", "Name", "rowguid", "SearchUrl" },
                values: new object[,]
                {
                    { 57, "ItemBalance", "", "", false, false, "ProjectBalance10Column", new Guid("3f4e4a35-7e84-4ddb-85ac-530d74495c44"), "" },
                    { 56, "ItemBalance", "", "", false, false, "ProjectBalance8Column", new Guid("8bf7c64d-f3fd-44d5-aa0f-0f9bad86c890"), "" },
                    { 55, "ItemBalance", "", "", false, false, "ProjectBalance6Column", new Guid("6e65a1d6-c707-40dc-ada8-7c87ef6239d7"), "" },
                    { 44, "ItemBalance", "", "", false, false, "DetailAccountBalance4Column", new Guid("b529ebc9-2431-444d-940e-8b50968948cb"), "" },
                    { 54, "ItemBalance", "", "", false, false, "ProjectBalance4Column", new Guid("8bb84062-5533-4e3f-9b04-1a5ff81941c5"), "" },
                    { 52, "ItemBalance", "", "", false, false, "CostCenterBalance10Column", new Guid("72dcc270-7e31-41b7-af11-252aa7f385df"), "" },
                    { 51, "ItemBalance", "", "", false, false, "CostCenterBalance8Column", new Guid("d80d6a4e-c997-4f02-bacf-18dc6253db64"), "" },
                    { 50, "ItemBalance", "", "", false, false, "CostCenterBalance6Column", new Guid("b7510be2-689d-4a7d-ac99-c8917af9d4de"), "" },
                    { 49, "ItemBalance", "", "", false, false, "CostCenterBalance4Column", new Guid("0924a77b-0ef1-4086-ba60-1dcfa46a4bc3"), "" },
                    { 48, "ItemBalance", "", "", false, false, "CostCenterBalance2Column", new Guid("64180b64-902c-451f-8f79-e585c3eface5"), "" },
                    { 47, "ItemBalance", "", "", false, false, "DetailAccountBalance10Column", new Guid("6634dd78-c5cf-4fb8-9e23-0045536f06a0"), "" },
                    { 46, "ItemBalance", "", "", false, false, "DetailAccountBalance8Column", new Guid("d4d5fa7d-ec1f-46b1-a70e-714cb042b38a"), "" },
                    { 61, "OperationLogArchive", "", "", false, false, "OperationLogArchive", new Guid("76365d2e-d462-4bf4-9a70-7433e6c53379"), "" },
                    { 53, "ItemBalance", "", "", false, false, "ProjectBalance2Column", new Guid("0fa89131-ef40-4805-a94a-9dd0cec874bc"), "" },
                    { 62, "ProfitLoss", "", "", false, false, "ProfitLoss", new Guid("1aec9060-f4bc-42ab-9280-cd49c621d956"), "" },
                    { 64, "ProfitLoss", "", "", false, false, "ProfitLossSimple", new Guid("944f6b25-5d9c-4645-8025-b0caf5cbb9c9"), "" },
                    { 43, "ItemBalance", "", "", false, false, "DetailAccountBalance2Column", new Guid("d9c56f3a-3515-49c2-b270-500893553ddb"), "" },
                    { 100002, "Unit", "Base", "", false, false, "Unit", new Guid("12985525-64fb-4923-8581-e46fce61580c"), "" },
                    { 100001, "Brand", "Base", "", false, false, "Brand", new Guid("d2332768-cbff-4410-8cde-fa197c130f63"), "" },
                    { 78, "VouchersByDate", "Operational", "", false, false, "VouchersByDate", new Guid("ba9163d7-419f-4cfe-84c8-d0fa6e47a3d9"), "" },
                    { 77, "PayReceiveCashAccount", "Core", "", false, false, "PayReceiveCashAccount", new Guid("0b574e80-9a0b-4351-95d2-7660e673ed64"), "" },
                    { 76, "PayReceiveAccount", "Core", "", false, false, "PayReceiveAccount", new Guid("f13e2ea4-2aa6-4e27-b06e-117afccc4f1f"), "" },
                    { 75, "Receipt", "Operational", "", false, false, "Receipt", new Guid("68ea9b43-f889-43d0-a6c4-ad071c3613bf"), "" },
                    { 74, "Payment", "Operational", "", false, false, "Payment", new Guid("a28319e9-2420-48d5-a668-6972c5f653e2"), "" },
                    { 73, "SourceApp", "Base", "", false, false, "SourceApp", new Guid("9a4a2e73-5858-47fe-bea1-d4cd9638b3f1"), "" },
                    { 72, "CheckBookReport", "", "", false, false, "CheckBookReport", new Guid("24d95289-f000-4d15-84b0-0ff065a96384"), "" },
                    { 71, "CheckBook", "Operational", "", false, false, "CheckBook", new Guid("559d405d-385b-4a70-a278-999aeeb9c0b1"), "" },
                    { 70, "CashRegister", "Base", "", false, false, "CashRegister", new Guid("3dd8d60a-6282-43ba-a592-c7f084c87901"), "" },
                    { 69, "CheckBookPage", "Core", "", false, false, "CheckBookPage", new Guid("551ba51a-1cb4-4f1b-bcc9-2e68eb0e77ca"), "" },
                    { 68, "Dashboard", "Core", "", false, false, "Widget", new Guid("a2e7d0ec-a73e-405e-b3cb-0b1ce91efbf8"), "" },
                    { 67, "BalanceSheet", "", "", false, false, "BalanceSheet", new Guid("310b8d86-d6a3-4ab3-b962-069a4d8ca0db"), "" },
                    { 66, "ProfitLoss", "", "", false, false, "ComparativeProfitLossSimple", new Guid("0ebec321-dfc7-41a1-99b0-d42f24d9eea7"), "" },
                    { 65, "ProfitLoss", "", "", false, false, "ComparativeProfitLoss", new Guid("72443f21-835c-476e-aca6-d1e92712dc9c"), "" },
                    { 63, "GroupActionResult", "", "", false, false, "GroupActionResult", new Guid("94a0b5fe-15f0-4558-b891-790cf54e8b55"), "" },
                    { 42, "VoucherLineDetail", "", "", false, false, "VoucherLineDetail", new Guid("9bd13254-84b2-4d51-b611-8444ce00b209"), "" },
                    { 22, "Journal", "", "", false, false, "JournalByNoByRow", new Guid("71bd2ef1-a4aa-404d-bb70-aa67f12bb95c"), "" },
                    { 40, "CurrencyBook", "", "", false, false, "CurrencyBookSummary", new Guid("3b376137-055c-4431-9b0d-e696b1eeeb4b"), "" },
                    { 16, "Journal", "", "", false, false, "JournalByDateByRowDetail", new Guid("4a18b6db-e9ed-4ad8-b509-b9fcddb26be3"), "" },
                    { 15, "Journal", "", "", false, false, "JournalByDateByRow", new Guid("37405791-6669-418f-82ce-822f69ad9fcf"), "" },
                    { 14, "AccountCollection", "Core", "", false, false, "AccountCollectionAccount", new Guid("cd31bbf7-e434-4c3b-b1ad-999cd97a20ef"), "" },
                    { 13, "OperationLog", "Core", "", false, false, "OperationLog", new Guid("c6f22e55-e167-42d5-90dc-62ef925c04b6"), "" },
                    { 12, "AccountGroup", "Core", "", false, false, "AccountGroup", new Guid("fb9c8df4-d670-4b03-a302-89cbd1316388"), "" }
                });

            migrationBuilder.InsertData(
                schema: "Metadata",
                table: "View",
                columns: new[] { "ViewID", "EntityName", "EntityType", "FetchUrl", "IsCartableIntegrated", "IsHierarchy", "Name", "rowguid", "SearchUrl" },
                values: new object[,]
                {
                    { 11, "Company", "Core", "", false, false, "CompanyDb", new Guid("5bbe8693-f738-4567-9cb6-a98fb84f427c"), "" },
                    { 10, "Branch", "Core", "", false, false, "Branch", new Guid("17320031-06b8-45ad-9794-6b8f81cda55a"), "/branches" },
                    { 9, "FiscalPeriod", "Core", "", true, false, "FiscalPeriod", new Guid("c73f0f80-dbcd-4be4-a07a-931f8ad9729c"), "/fperiods" },
                    { 8, "Project", "Base", "/lookup/projects", true, true, "Project", new Guid("4ff7cf88-8424-4304-bc05-affbe1a2fbb5"), "/projects" },
                    { 7, "CostCenter", "Base", "/lookup/ccenters", true, true, "CostCenter", new Guid("a2033c86-4050-4b8c-983d-40871002fe81"), "/ccenters" },
                    { 6, "DetailAccount", "Base", "/lookup/faccounts", true, true, "DetailAccount", new Guid("9ebbb3c1-1aee-4d9b-b6c4-9e3ee8ca1543"), "/faccounts" },
                    { 5, "Role", "Core", "", false, false, "Role", new Guid("751241cc-38dc-40dd-9ae3-c45963e3febb"), "" },
                    { 4, "User", "Core", "", false, false, "User", new Guid("e4dd6995-561b-4eba-a401-bdd4cb24cdb4"), "" },
                    { 3, "VoucherLine", "Operational", "/lookup/vouchers/lines", true, false, "VoucherLine", new Guid("09d85b66-d12d-4015-bf31-4a8143104598"), "" },
                    { 2, "Voucher", "Operational", "/lookup/vouchers", true, false, "Voucher", new Guid("eb27fb68-0d6c-4e8b-ba88-5f25f518c579"), "" },
                    { 1, "Account", "Base", "/lookup/accounts", true, true, "Account", new Guid("7a078968-4470-49a7-aaa5-396ae400e02c"), "/accounts/lookup" },
                    { 100004, "Attribute", "Base", "", false, false, "Attribute", new Guid("92456aa5-7eb6-4fab-aba5-7d2d56aa8427"), "" },
                    { 17, "Journal", "", "", false, false, "JournalByDateByLedger", new Guid("5a577867-eb1f-41cd-be8e-f136f4efad88"), "" },
                    { 18, "Journal", "", "", false, false, "JournalByDateBySubsidiary", new Guid("ba5b80c1-d88f-46c0-9f90-b568de95c9d1"), "" },
                    { 19, "Journal", "", "", false, false, "JournalByDateSummary", new Guid("c1df61e9-3b7d-4118-b6f9-f72f0421c781"), "" },
                    { 20, "Journal", "", "", false, false, "JournalByDateSummaryByDate", new Guid("037ab456-bb78-4697-99ba-cf358a54a987"), "" },
                    { 39, "CurrencyBook", "", "", false, false, "CurrencyBookSingleSummary", new Guid("09c53547-6cde-463e-8d9d-4073101dd9ef"), "" },
                    { 38, "CurrencyBook", "", "", false, false, "CurrencyBookSingle", new Guid("1b28520f-db0f-4813-b8c8-3939fd52a9be"), "" },
                    { 37, "CurrencyBook", "", "", false, false, "CurrencyBook", new Guid("eae9d124-3843-431e-83b3-3d11e5d49fbb"), "" },
                    { 36, "TestBalance", "", "", false, false, "TestBalance10Column", new Guid("55babf36-4cd3-46d9-9944-1f7fe7322a56"), "" },
                    { 35, "TestBalance", "", "", false, false, "TestBalance8Column", new Guid("f96feaea-094f-4729-b0d7-90694275b63b"), "" },
                    { 34, "TestBalance", "", "", false, false, "TestBalance6Column", new Guid("474173af-8745-485a-84fa-5d1e28b62535"), "" },
                    { 33, "TestBalance", "", "", false, false, "TestBalance4Column", new Guid("1b26b836-6d3c-4256-8e08-abe81865ba4d"), "" },
                    { 32, "TestBalance", "", "", false, false, "TestBalance2Column", new Guid("0ccb697c-3d0b-4d40-8a53-8ebe7082faf5"), "" },
                    { 41, "NumberList", "", "", false, false, "NumberList", new Guid("f35eea7b-fc0c-4405-b689-0f67b061ab19"), "" },
                    { 31, "CurrencyRate", "", "", false, false, "CurrencyRate", new Guid("8148071a-0111-4d57-8854-827f813b7988"), "" },
                    { 29, "AccountBook", "", "", false, false, "AccountBookSummary", new Guid("88a50f22-e094-4c62-822f-36a2eb0e9795"), "" },
                    { 28, "AccountBook", "", "", false, false, "AccountBookSingleSummary", new Guid("576bcbde-d08d-4a54-a79c-9bb461f226a4"), "" },
                    { 27, "AccountBook", "", "", false, false, "AccountBookSingle", new Guid("f7ce7722-f431-4aac-82b1-7fe29395571c"), "" },
                    { 26, "Journal", "", "", false, false, "JournalByNoSummary", new Guid("483c5922-7739-413c-b148-8a8f339e1ddd"), "" },
                    { 25, "Journal", "", "", false, false, "JournalByNoBySubsidiary", new Guid("5d9b8d81-48a1-478a-80de-e6eb8a12825e"), "" },
                    { 24, "Journal", "", "", false, false, "JournalByNoByLedger", new Guid("389bacbd-9dad-4320-9b0e-01814e2c528d"), "" },
                    { 23, "Journal", "", "", false, false, "JournalByNoByRowDetail", new Guid("4f92cac5-1194-47fd-89c7-b26d6e0c2289"), "" },
                    { 21, "Journal", "", "", false, false, "JournalByDateSummaryByMonth", new Guid("db0f1633-df62-4aed-a201-e37d40da0892"), "" },
                    { 30, "Currency", "", "", false, false, "Currency", new Guid("bc0dd97e-3b25-453c-b942-4eddcf89ebfc"), "" },
                    { 100005, "File", "Base", "", false, false, "File", new Guid("d125ce08-9733-467f-b46a-0c8530420131"), "" }
                });

            migrationBuilder.InsertData(
                schema: "Auth",
                table: "PermissionGroup",
                columns: new[] { "PermissionGroupID", "Description", "EntityName", "ModifiedDate", "Name", "rowguid", "SourceTypeId", "SubsystemId" },
                values: new object[,]
                {
                    { 100005, "", "file", new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "ManageEntities,files", new Guid("efef2dff-118e-4dd1-b949-f240b8570389"), 1, 100000 },
                    { 13, "", "AccountGroup", new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "ManageEntities,AccountGroups", new Guid("e34b16e3-2130-43e5-98a5-dd6b19f89afb"), 1, 2 },
                    { 8, "", "Vouchers", new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "ManageEntities,Vouchers", new Guid("5d50bf73-1485-481f-a6ed-93fe09a89f39"), 2, 2 },
                    { 7, "", "Voucher", new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Vouchers", new Guid("30ba43d9-ea26-4d5f-bdef-505bfa2e6a01"), 2, 2 },
                    { 5, "", "FiscalPeriod", new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "ManageEntities,FiscalPeriods", new Guid("47100fd6-ae85-4b2b-82c3-7cde4c553fa7"), 1, 2 },
                    { 4, "", "Project", new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "ManageEntities,Projects", new Guid("5f6fb4c9-ec5d-48a1-be64-2f3fa19cce85"), 1, 2 },
                    { 3, "", "CostCenter", new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "ManageEntities,CostCenters", new Guid("c4e7d6bd-2bb2-4f7a-94fb-a0cc9db541c4"), 1, 2 },
                    { 2, "", "DetailAccount", new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "ManageEntities,DetailAccounts", new Guid("55e29a84-e2fb-40aa-8689-03a50d2ee6ff"), 1, 2 },
                    { 1, "", "Account", new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "ManageEntities,Accounts", new Guid("c91136fa-e30e-42b4-b947-d6a05fcbf293"), 1, 2 },
                    { 30, "", "SystemIssue", new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "SystemIssueReport", new Guid("962398dc-84f9-4b64-960f-7d03b61c2b0c"), 3, 1 },
                    { 14, "", "AccountCollection", new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "ManageEntities,AccountCollections", new Guid("e32acffc-9f89-47dd-8ac0-b66f53ebcc6a"), 1, 2 },
                    { 22, "", "RowAccess", new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "RowAccessSettings", new Guid("bfd73d2a-a59f-49af-936e-8e2d31bc8026"), 1, 1 },
                    { 20, "", "Setting", new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Settings", new Guid("bacc9e07-3a60-471b-af3a-814c9b66483b"), 1, 1 },
                    { 18, "", "UserReport", new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "ManageEntities,UserReports", new Guid("af6741eb-e30d-4b50-ab4a-25af92c3c183"), 3, 1 },
                    { 17, "", "Report", new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "ManageEntities,Reports", new Guid("00c0f9c0-9bf7-491a-ac5b-3b334e4a2791"), 3, 1 },
                    { 16, "", "SysOperationLog", new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "ManageEntities,SysOperationLogs", new Guid("d695c0d5-ff33-4636-9ce8-a81cce4d6f52"), 3, 1 },
                    { 15, "", "OperationLog", new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "ManageEntities,OperationLogs", new Guid("f8362800-a3a7-4fa2-a6dc-2e5da3453e6d"), 3, 1 },
                    { 12, "", "Role", new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "ManageEntities,Roles", new Guid("27e2bf48-5f1a-4d70-a8f2-858c9ca8bdc4"), 1, 1 },
                    { 11, "", "User", new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "ManageEntities,Users", new Guid("1f3ec3cf-faba-4719-90c4-a2ca61f6fb21"), 1, 1 },
                    { 10, "", "Company", new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "ManageEntities,Companies", new Guid("af236c0b-5819-49c5-aea1-cab79e1206d1"), 1, 1 },
                    { 9, "", "Branch", new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "ManageEntities,Branches", new Guid("0351db19-5b4d-441e-94cf-7091d256a915"), 1, 1 },
                    { 21, "", "LogSetting", new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "LogSetting", new Guid("e5196d70-ef47-4fae-86c0-4d18b9602fab"), 1, 1 },
                    { 19, "", "AccountRelations", new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "AccountRelations", new Guid("5cb522f9-b56e-4949-adcd-bcdf2f262f3b"), 1, 2 },
                    { 6, "", "Currency", new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "ManageEntities,Currencies", new Guid("c10bdff1-3e86-495b-89a1-e34e70369a25"), 1, 2 },
                    { 24, "", "Journal", new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "JournalReport", new Guid("c757f8e4-37e4-4afe-81f9-ef6ca80b727a"), 3, 2 },
                    { 100003, "", "Property", new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "ManageEntities,Properties", new Guid("773b39bb-7d73-450c-bdc5-dacc6fbb65e3"), 1, 100000 },
                    { 100002, "", "Unit", new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "ManageEntities,Units", new Guid("3885b65b-39ac-476e-8b28-7e112fb2ce03"), 1, 100000 },
                    { 100001, "", "Brand", new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "ManageEntities,Brands", new Guid("c8ff0422-00f0-4502-bed7-4aa58db037e3"), 1, 100000 },
                    { 42, "", "Receipt", new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Receipts", new Guid("041a01c4-5c7f-4e30-ab05-951210277724"), 2, 3 },
                    { 41, "", "Payment", new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Payments", new Guid("099566ba-0745-42d0-93a5-673e84ef802a"), 2, 3 },
                    { 23, "", "CurrencyRate", new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "CurrencyRate", new Guid("cddd5da1-70fc-4982-a8de-820559c24d1e"), 1, 2 },
                    { 39, "", "CheckBookReport", new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "CheckBookReport", new Guid("0c7aa5a5-8eff-45a0-ac32-b37aee7e1b5b"), 3, 3 },
                    { 38, "", "CashRegister", new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "ManageEntities,CashRegisters", new Guid("7a1cc574-063b-4589-b29f-0a83d0aa4b89"), 1, 3 },
                    { 37, "", "CheckBook", new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "ManageEntities,CheckBooks", new Guid("9a9fd8cb-bb94-472a-ad85-0b9b78af1001"), 2, 3 },
                    { 36, "", "Dashboard", new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Dashboard", new Guid("e45d0815-e591-4b4c-9149-e731722635c1"), 3, 2 },
                    { 40, "", "SourceApp", new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "ManageEntities,SourceApps", new Guid("595dbe1b-52cc-44c9-bfc5-9a06fba66132"), 1, 3 },
                    { 34, "", "BalanceSheet", new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "BalanceSheetReport", new Guid("56b59825-19fc-49c1-9361-390a601808f5"), 3, 2 },
                    { 35, "", "SpecialVoucher", new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "SpecialVoucherOps", new Guid("b9e10292-bd88-4ed6-b40d-f17142a021e5"), 2, 2 },
                    { 25, "", "AccountBook", new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "AccountBookReport", new Guid("58f38b5c-c7cb-46bd-8bc1-aefa2f2aaf54"), 3, 2 },
                    { 26, "", "TestBalance", new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "TestBalanceReport", new Guid("2b44cc63-181a-4197-bef9-29dfb08c1e82"), 3, 2 },
                    { 27, "", "CurrencyBook", new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "CurrencyBookReport", new Guid("6e2784c1-a2bb-48d2-9366-5753321d93be"), 3, 2 },
                    { 100004, "", "Attribute", new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "ManageEntities,Attributes", new Guid("297e0d98-7c65-42f1-a523-cef08e834a7c"), 1, 100000 }
                });

            migrationBuilder.InsertData(
                schema: "Auth",
                table: "PermissionGroup",
                columns: new[] { "PermissionGroupID", "Description", "EntityName", "ModifiedDate", "Name", "rowguid", "SourceTypeId", "SubsystemId" },
                values: new object[,]
                {
                    { 29, "", "BalanceByAccount", new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "BalanceByAccountReport", new Guid("0c57fe2a-dc9a-4cd7-9c73-22bcc27380e3"), 3, 2 },
                    { 31, "", "ProfitLoss", new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "ProfitLossReport", new Guid("99fd4238-36a4-4791-95ed-b4e916b0f492"), 3, 2 },
                    { 32, "", "DraftVoucher", new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "DraftVouchers", new Guid("28819e8c-0f19-4fe8-a2a5-e11c3e9c4f23"), 2, 2 },
                    { 33, "", "DraftVouchers", new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "ManageEntities,DraftVouchers", new Guid("88c30324-0ad1-4617-a360-762ee5063c47"), 2, 2 },
                    { 28, "", "ItemBalance", new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "ItemBalanceReport", new Guid("3fbaa2a7-5611-4216-88bb-244c3ba81cc2"), 3, 2 }
                });

            migrationBuilder.InsertData(
                schema: "Config",
                table: "SysLogSetting",
                columns: new[] { "SysLogSettingID", "EntityTypeId", "IsEnabled", "ModifiedDate", "OperationId", "rowguid", "SourceId" },
                values: new object[,]
                {
                    { 40, 5, true, new DateTime(2023, 10, 16, 14, 50, 15, 0, DateTimeKind.Unspecified), 54, new Guid("c35f0d13-ee67-494a-b422-43d9298d1a6a"), null },
                    { 47, null, true, new DateTime(2023, 10, 16, 14, 50, 15, 0, DateTimeKind.Unspecified), 24, new Guid("922a9d9d-115e-4c7e-a3d1-4849a5f9fa35"), 8 },
                    { 46, null, true, new DateTime(2023, 10, 16, 14, 50, 15, 0, DateTimeKind.Unspecified), 23, new Guid("3a9cb95a-6e74-4b87-b430-35b63f397ed5"), 8 },
                    { 45, null, true, new DateTime(2023, 10, 16, 14, 50, 15, 0, DateTimeKind.Unspecified), 22, new Guid("e4d43830-86bb-425a-bb27-47035772a9c4"), 7 },
                    { 38, 5, true, new DateTime(2023, 10, 16, 14, 50, 15, 0, DateTimeKind.Unspecified), 58, new Guid("8dbdff7d-0831-4d25-9fb0-37679a68e36b"), null },
                    { 25, 2, true, new DateTime(2023, 10, 16, 14, 50, 15, 0, DateTimeKind.Unspecified), 58, new Guid("7b7dce71-16d8-476f-ba5d-995e7b06d3dc"), null },
                    { 15, 6, true, new DateTime(2023, 10, 16, 14, 50, 15, 0, DateTimeKind.Unspecified), 58, new Guid("4a147a2c-0cb1-42e8-9ab8-8ecec4a6379d"), null },
                    { 7, 1, true, new DateTime(2023, 10, 16, 14, 50, 15, 0, DateTimeKind.Unspecified), 58, new Guid("b632cedb-2b01-41d3-86cd-e385688b34b9"), null },
                    { 31, 2, true, new DateTime(2023, 10, 16, 14, 50, 15, 0, DateTimeKind.Unspecified), 57, new Guid("22377188-b1c0-4c0b-9f90-85e4ea1d8a50"), null },
                    { 48, null, true, new DateTime(2023, 10, 16, 14, 50, 15, 0, DateTimeKind.Unspecified), 25, new Guid("b556bf70-918b-43e4-9a0a-418e39a73d2b"), 8 },
                    { 27, 2, true, new DateTime(2023, 10, 16, 14, 50, 15, 0, DateTimeKind.Unspecified), 54, new Guid("0310b528-6412-4be3-9d08-9d123d03aaca"), null },
                    { 9, 1, true, new DateTime(2023, 10, 16, 14, 50, 15, 0, DateTimeKind.Unspecified), 54, new Guid("9bd2225e-810d-474b-86d2-3b255063723f"), null },
                    { 6, 1, true, new DateTime(2023, 10, 16, 14, 50, 15, 0, DateTimeKind.Unspecified), 5, new Guid("250cc31b-34f0-40f0-9348-3f981202467e"), null },
                    { 35, 5, true, new DateTime(2023, 10, 16, 14, 50, 15, 0, DateTimeKind.Unspecified), 4, new Guid("9ee77205-cfde-465a-b209-70b2f9307250"), null },
                    { 22, 2, true, new DateTime(2023, 10, 16, 14, 50, 15, 0, DateTimeKind.Unspecified), 4, new Guid("7c0bc7ba-a580-4aff-b36d-a752d6b81ec5"), null },
                    { 4, 1, true, new DateTime(2023, 10, 16, 14, 50, 15, 0, DateTimeKind.Unspecified), 4, new Guid("45a1ea29-acd4-4eba-88e1-0e32f2f604d5"), null },
                    { 21, 2, true, new DateTime(2023, 10, 16, 14, 50, 15, 0, DateTimeKind.Unspecified), 3, new Guid("6b213e37-b6ef-42e5-8657-168fb2638cfc"), null },
                    { 13, 6, true, new DateTime(2023, 10, 16, 14, 50, 15, 0, DateTimeKind.Unspecified), 3, new Guid("cd0d8c1a-64ea-426e-b213-fd6b905de572"), null },
                    { 17, 6, true, new DateTime(2023, 10, 16, 14, 50, 15, 0, DateTimeKind.Unspecified), 54, new Guid("e1048fb4-aa05-4466-9a75-1270fb75dfe0"), null },
                    { 14, 6, true, new DateTime(2023, 10, 16, 14, 50, 15, 0, DateTimeKind.Unspecified), 5, new Guid("d45fb39e-dd07-4e46-bbc0-fd8a52d40389"), null },
                    { 20, 2, true, new DateTime(2023, 10, 16, 14, 50, 15, 0, DateTimeKind.Unspecified), 2, new Guid("c72fa8b8-72d5-42af-9117-7100a4db8bcd"), null },
                    { 2, 1, true, new DateTime(2023, 10, 16, 14, 50, 15, 0, DateTimeKind.Unspecified), 2, new Guid("4504ecba-abc3-4b2d-af01-2f125e26e680"), null },
                    { 43, 8, true, new DateTime(2023, 10, 16, 14, 50, 15, 0, DateTimeKind.Unspecified), 1, new Guid("f38bbf6e-7ad4-43d0-b214-338fef9651d6"), null },
                    { 34, 5, true, new DateTime(2023, 10, 16, 14, 50, 15, 0, DateTimeKind.Unspecified), 1, new Guid("1907d3ec-8124-4aa0-a358-3df2eee56e70"), null },
                    { 32, 4, true, new DateTime(2023, 10, 16, 14, 50, 15, 0, DateTimeKind.Unspecified), 1, new Guid("8095689a-aed4-410c-a5b7-83242d767550"), null },
                    { 19, 2, true, new DateTime(2023, 10, 16, 14, 50, 15, 0, DateTimeKind.Unspecified), 1, new Guid("9c45105c-ce6a-4c64-aa81-be44ba1a9029"), null },
                    { 11, 6, true, new DateTime(2023, 10, 16, 14, 50, 15, 0, DateTimeKind.Unspecified), 1, new Guid("dfed72b1-329d-4461-b676-a5a4d0861988"), null },
                    { 1, 1, true, new DateTime(2023, 10, 16, 14, 50, 15, 0, DateTimeKind.Unspecified), 1, new Guid("7141517b-75fc-4676-80dd-e3be8ff753c1"), null },
                    { 12, 6, true, new DateTime(2023, 10, 16, 14, 50, 15, 0, DateTimeKind.Unspecified), 2, new Guid("235b1d09-7a16-4730-a27e-ef283f1feb82"), null },
                    { 24, 2, true, new DateTime(2023, 10, 16, 14, 50, 15, 0, DateTimeKind.Unspecified), 5, new Guid("5c914aab-0553-473d-b071-c8895c762cad"), null },
                    { 3, 1, true, new DateTime(2023, 10, 16, 14, 50, 15, 0, DateTimeKind.Unspecified), 3, new Guid("3c87833f-abdf-4f45-a266-ae7483e228ac"), null },
                    { 8, 1, true, new DateTime(2023, 10, 16, 14, 50, 15, 0, DateTimeKind.Unspecified), 6, new Guid("67a813d8-5df6-44cb-be57-bb8831a1b087"), null },
                    { 37, 5, true, new DateTime(2023, 10, 16, 14, 50, 15, 0, DateTimeKind.Unspecified), 5, new Guid("830ee56d-8e92-4f1b-92b8-ef112025631e"), null },
                    { 10, 1, true, new DateTime(2023, 10, 16, 14, 50, 15, 0, DateTimeKind.Unspecified), 35, new Guid("8dd21585-c7a3-41cf-b298-db5e7db5da22"), null },
                    { 42, 5, true, new DateTime(2023, 10, 16, 14, 50, 15, 0, DateTimeKind.Unspecified), 30, new Guid("5b6d5e64-9ef1-4864-992f-a4da806173eb"), null },
                    { 30, 2, true, new DateTime(2023, 10, 16, 14, 50, 15, 0, DateTimeKind.Unspecified), 29, new Guid("cc684944-61db-4ad5-8aed-24e49a142318"), null },
                    { 29, 2, true, new DateTime(2023, 10, 16, 14, 50, 15, 0, DateTimeKind.Unspecified), 28, new Guid("16eccaf6-d546-4c87-9a5b-eeb2a7630abf"), null }
                });

            migrationBuilder.InsertData(
                schema: "Config",
                table: "SysLogSetting",
                columns: new[] { "SysLogSettingID", "EntityTypeId", "IsEnabled", "ModifiedDate", "OperationId", "rowguid", "SourceId" },
                values: new object[,]
                {
                    { 28, 2, true, new DateTime(2023, 10, 16, 14, 50, 15, 0, DateTimeKind.Unspecified), 27, new Guid("03779a40-2591-4631-a8ac-f715c577aecc"), null },
                    { 36, 5, true, new DateTime(2023, 10, 16, 14, 50, 15, 0, DateTimeKind.Unspecified), 21, new Guid("30dcb839-619a-4b05-a4cd-414093b65bf2"), null },
                    { 23, 2, true, new DateTime(2023, 10, 16, 14, 50, 15, 0, DateTimeKind.Unspecified), 21, new Guid("6e5804a3-2ed5-406d-aba1-6997a5e213c3"), null },
                    { 18, 6, true, new DateTime(2023, 10, 16, 14, 50, 15, 0, DateTimeKind.Unspecified), 26, new Guid("d0482435-05cd-40aa-ae00-5e062d7e011c"), null },
                    { 49, 9, true, new DateTime(2023, 10, 16, 14, 50, 15, 0, DateTimeKind.Unspecified), 10, new Guid("6c4544f1-fe36-4a89-8987-206118ec30cb"), null },
                    { 41, 5, true, new DateTime(2023, 10, 16, 14, 50, 15, 0, DateTimeKind.Unspecified), 8, new Guid("669e6ab5-41ec-4ecb-9c50-6e3f1223e879"), null },
                    { 44, 8, true, new DateTime(2023, 10, 16, 14, 50, 15, 0, DateTimeKind.Unspecified), 7, new Guid("10747d8c-db32-497d-9ef3-a2cda1cf5dd9"), null },
                    { 33, 4, true, new DateTime(2023, 10, 16, 14, 50, 15, 0, DateTimeKind.Unspecified), 7, new Guid("c76b2aae-29e3-4720-b6a0-931c91a893b1"), null },
                    { 39, 5, true, new DateTime(2023, 10, 16, 14, 50, 15, 0, DateTimeKind.Unspecified), 6, new Guid("8a888350-2723-41a6-bb45-9f56acdc895c"), null },
                    { 26, 2, true, new DateTime(2023, 10, 16, 14, 50, 15, 0, DateTimeKind.Unspecified), 6, new Guid("fae4c491-9b75-4f44-b0e7-6c6e337dfd75"), null },
                    { 16, 6, true, new DateTime(2023, 10, 16, 14, 50, 15, 0, DateTimeKind.Unspecified), 6, new Guid("17ccc5f7-bedc-4f74-8567-4e36fcfadcbe"), null },
                    { 5, 1, true, new DateTime(2023, 10, 16, 14, 50, 15, 0, DateTimeKind.Unspecified), 21, new Guid("efbd3f68-5132-427b-bd6c-7b91b760b7bd"), null }
                });

            migrationBuilder.InsertData(
                schema: "Metadata",
                table: "Command",
                columns: new[] { "CommandID", "HotKey", "IconName", "Index", "ModifiedDate", "ParentId", "PermissionId", "RouteUrl", "rowguid", "TitleKey" },
                values: new object[,]
                {
                    { 39, "", "folder-close", null, new DateTime(2023, 10, 16, 15, 56, 33, 0, DateTimeKind.Unspecified), 1, null, "", new Guid("80bbd832-3ce7-4dee-8bd6-b2ac7b060cc0"), "FinancialReports" },
                    { 2, "", "folder-close", null, new DateTime(2023, 10, 16, 15, 56, 33, 0, DateTimeKind.Unspecified), 1, null, "", new Guid("009397bc-97b9-4cc6-865b-7e1bbec3a9c2"), "BaseData" },
                    { 11, "", "folder-close", null, new DateTime(2023, 10, 16, 15, 56, 33, 0, DateTimeKind.Unspecified), 1, null, "", new Guid("c8d4d7f6-a078-4951-97cc-5c3f9418d979"), "VoucherOps" },
                    { 16, "", "folder-close", null, new DateTime(2023, 10, 16, 15, 56, 33, 0, DateTimeKind.Unspecified), 1, null, "", new Guid("c5b6c7a8-6a85-4db5-80aa-7bfa3f7779d6"), "SpecialOps" },
                    { 20, "", "folder-close", null, new DateTime(2023, 10, 16, 15, 56, 33, 0, DateTimeKind.Unspecified), 1, null, "", new Guid("eb247e6d-1f19-4992-9c06-651155d47b60"), "AccountingLedgers" },
                    { 53, "", "folder-close", null, new DateTime(2023, 10, 16, 15, 56, 33, 0, DateTimeKind.Unspecified), 52, null, "", new Guid("ed09c1a5-3777-4fb4-a8e5-172b31e229eb"), "BaseData" },
                    { 100003, "", "folder-close", null, new DateTime(2023, 10, 16, 15, 56, 33, 0, DateTimeKind.Unspecified), 100000, null, "", new Guid("1da20bb5-9f06-4bb5-acad-e0d836ed9bfe"), "ProductScopeReports" },
                    { 55, "", "folder-close", null, new DateTime(2023, 10, 16, 15, 56, 33, 0, DateTimeKind.Unspecified), 52, null, "", new Guid("0107b9b1-cd38-4386-bd78-ce97a35d030d"), "CheckReports" },
                    { 62, "", "folder-close", null, new DateTime(2023, 10, 16, 15, 56, 33, 0, DateTimeKind.Unspecified), 52, null, "", new Guid("b0c92c48-608d-478f-b13b-94128a1fdba8"), "PaymentOperations" },
                    { 63, "", "folder-close", null, new DateTime(2023, 10, 16, 15, 56, 33, 0, DateTimeKind.Unspecified), 52, null, "", new Guid("2b7b3182-02eb-4c81-9192-23e77a52427e"), "ReceiptOperations" },
                    { 100001, "", "folder-close", null, new DateTime(2023, 10, 16, 15, 56, 33, 0, DateTimeKind.Unspecified), 100000, null, "", new Guid("8a433ca7-e03f-4bcf-94db-03dc04349fa5"), "BaseData" },
                    { 100002, "", "folder-close", null, new DateTime(2023, 10, 16, 15, 56, 33, 0, DateTimeKind.Unspecified), 100000, null, "", new Guid("b7767c97-77d8-4826-835a-bade05dfde44"), "ProductScopeOperations" },
                    { 54, "", "folder-close", null, new DateTime(2023, 10, 16, 15, 56, 33, 0, DateTimeKind.Unspecified), 52, null, "", new Guid("0e40a05f-2dc5-43d2-b44d-7f61d7fc1ffa"), "CheckOperations" }
                });

            migrationBuilder.InsertData(
                schema: "Reporting",
                table: "Report",
                columns: new[] { "ReportID", "Code", "CreatedById", "IsDefault", "IsDynamic", "IsGroup", "IsSystem", "ModifiedDate", "ParentId", "ResourceKeys", "ServiceUrl", "SubsystemId", "ViewId" },
                values: new object[,]
                {
                    { 100000, "ProductScope", 1, false, false, true, true, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), null, "", "", 100000, null },
                    { 93, "Treasury", 1, false, false, true, true, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), null, "", "", 3, null },
                    { 42, "Report-QReport-Manage", 1, false, false, true, true, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), null, "", "", 1, null },
                    { 13, "Accounting", 1, false, false, true, true, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), null, "", "", 2, null },
                    { 1, "Administration", 1, false, false, true, true, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), null, "", "", 1, null }
                });

            migrationBuilder.InsertData(
                schema: "Auth",
                table: "Permission",
                columns: new[] { "PermissionID", "Description", "Flag", "GroupId", "ModifiedDate", "Name", "rowguid" },
                values: new object[,]
                {
                    { 127, "", 2, 19, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Save", new Guid("63ec90d3-5d8b-4d17-9742-3543eac44f5d") },
                    { 192, "", 4, 33, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Print", new Guid("f6fb9c16-f8e7-4a6d-adc5-025d96863a9e") },
                    { 191, "", 2, 33, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Filter", new Guid("05551436-71ec-4fd1-b1d8-7af5f8f2f817") },
                    { 190, "", 1, 33, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "View", new Guid("709e2e5e-e174-42f9-b6ab-f6970f5b967e") },
                    { 196, "", 2048, 32, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Normalize", new Guid("7ce107e5-0600-4cb4-9341-3a099138e583") },
                    { 189, "", 1024, 32, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "NavigateEntities,DraftVouchers", new Guid("55102dc4-e956-4420-b088-9282f679eb85") },
                    { 188, "", 512, 32, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "UndoCheck", new Guid("f396c5a8-6023-460b-8a5b-cd6bc1ff84f2") },
                    { 187, "", 256, 32, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Check", new Guid("fbae21cd-a38d-4bdb-aec9-678ea0e22f37") },
                    { 186, "", 128, 32, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "DeleteLine", new Guid("cc237768-c99c-42eb-ad03-a757e47f367d") },
                    { 185, "", 64, 32, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "EditLine", new Guid("feab2ac7-6cff-467e-9aa7-f536acbcd6d9") },
                    { 184, "", 32, 32, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "CreateLine", new Guid("df2bd3af-648f-4a4b-bf5c-1ac54986d7c0") },
                    { 183, "", 16, 32, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Print", new Guid("f0c471ee-a4cc-49b5-934d-eec08ee7d02b") },
                    { 182, "", 8, 32, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Delete", new Guid("a65cfae8-3481-4101-bb30-4783df018851") },
                    { 181, "", 4, 32, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Edit", new Guid("88259c9f-7287-4787-a101-3d50f76ca8a8") },
                    { 180, "", 2, 32, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Create", new Guid("bf0ae39d-ec06-47b4-a55b-a0fe26586510") },
                    { 179, "", 1, 32, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "View", new Guid("94f3a5a6-30e3-42e3-9ee5-32004731d734") },
                    { 200, "", 16, 31, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "FilterByRef", new Guid("9b7f7d50-6d28-4204-8868-a52cc9705007") },
                    { 178, "", 8, 31, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Export", new Guid("20cb49f0-1b2e-4966-98ef-800c9cb30172") },
                    { 193, "", 8, 33, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Export", new Guid("262062bc-eba3-410c-98e0-83e10df0ec49") },
                    { 194, "", 16, 33, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "GroupCheck", new Guid("b1dc7905-96df-4d83-98bf-265e5162dda9") },
                    { 195, "", 32, 33, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "GroupUndoCheck", new Guid("d8fe802a-3bbd-45d9-b5cf-fc8d6b72dcef") },
                    { 201, "", 1, 34, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "View", new Guid("b9f85f99-226a-4835-bf69-f975e3294b9c") },
                    { 219, "", 128, 37, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "CreatePages", new Guid("06108c26-938f-4f35-946d-a449fbc16974") },
                    { 218, "", 64, 37, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "NavigateEntities,CheckBooks", new Guid("3c688576-db55-4d49-83c3-b52c91e992a1") },
                    { 217, "", 32, 37, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Delete", new Guid("8e7d4ecf-f555-4753-b97f-c4fce2af987e") },
                    { 216, "", 16, 37, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Edit", new Guid("685896e9-fa83-4a11-8d85-ed3d25a2441b") },
                    { 215, "", 8, 37, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Create", new Guid("a88f7d1f-c7f6-4309-bcaa-287cf5d3db05") },
                    { 214, "", 4, 37, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Print", new Guid("8b324362-4311-41dd-a8f8-00f7f48a6abe") },
                    { 213, "", 2, 37, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Filter", new Guid("c0678975-5f52-48ac-a710-5851cb237309") },
                    { 212, "", 1, 37, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "View", new Guid("c867d2c6-1f39-4691-b1ee-34631289a7ab") },
                    { 177, "", 4, 31, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Print", new Guid("3d4b3d9a-c4ca-4d7a-8344-1f0989c04c03") },
                    { 211, "", 2, 36, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "ManageWidgets", new Guid("db166320-767a-40db-8a26-2afd1b082d5b") },
                    { 209, "", 8, 35, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "UncheckClosingVoucher", new Guid("0d511c42-fc7d-487a-8a8a-0c06337a9173") },
                    { 208, "", 4, 35, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "IssueClosingVoucher", new Guid("1fafb49b-d8c3-48ca-91f9-b004e624e36d") },
                    { 207, "", 2, 35, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "IssueClosingTempAccountsVoucher", new Guid("c467e17c-9cf0-4592-ac44-41de4906eaa5") },
                    { 206, "", 1, 35, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "IssueOpeningVoucher", new Guid("ada5e3f5-aa27-42f8-b926-1056ac288983") },
                    { 205, "", 16, 34, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "FilterByRef", new Guid("34723e1b-9ce6-4496-b6c7-130405281831") },
                    { 204, "", 8, 34, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Export", new Guid("8af814f2-e8bd-4218-90c0-01fba0cb78ae") },
                    { 203, "", 4, 34, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Print", new Guid("81be7ed7-9898-4dcd-894c-15a05d441faf") },
                    { 202, "", 2, 34, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Filter", new Guid("8f10d0c8-3521-4b9e-812e-33c0f7adaa13") },
                    { 210, "", 1, 36, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "ManageDashboard", new Guid("5fdcbf86-8b21-4b3b-86d7-5c33be0a8122") },
                    { 176, "", 2, 31, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Filter", new Guid("2265e4de-5d07-4543-8f2e-1b61f99a9305") }
                });

            migrationBuilder.InsertData(
                schema: "Auth",
                table: "Permission",
                columns: new[] { "PermissionID", "Description", "Flag", "GroupId", "ModifiedDate", "Name", "rowguid" },
                values: new object[,]
                {
                    { 175, "", 1, 31, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "View", new Guid("d1ed0204-22dd-4921-b6a2-4e9c352a654c") },
                    { 199, "", 32, 29, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "FilterByRef", new Guid("af9b5362-9c33-4e35-97aa-5386f53f1c3e") },
                    { 153, "", 1, 26, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "View", new Guid("b36f916c-69b5-431b-9821-1b2d0c17020c") },
                    { 152, "", 32, 25, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "ViewByBranch", new Guid("5ad30aaf-2483-45d0-8146-2ad791838631") },
                    { 151, "", 16, 25, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Mark", new Guid("84a1a291-7f53-4bed-82bc-3fa85ddb5acd") },
                    { 150, "", 8, 25, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Export", new Guid("61ee747c-bccf-49fe-a4e5-afbd4ae0e38d") },
                    { 149, "", 4, 25, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Print", new Guid("391368f9-a6e6-4425-9198-547f0500ea8f") },
                    { 148, "", 2, 25, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Filter", new Guid("e25efe61-2701-4c9e-843b-014635758f48") },
                    { 147, "", 1, 25, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "View", new Guid("ce9bde49-54f9-4071-95ac-34672483ea4d") },
                    { 146, "", 32, 24, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "ViewByBranch", new Guid("ba99fd50-31c9-4b69-93ca-854e13ed66b3") },
                    { 154, "", 2, 26, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Filter", new Guid("11397789-2986-4594-87b4-6e7d8abb0310") },
                    { 145, "", 16, 24, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Mark", new Guid("b25f936a-0133-479c-8dac-5aa528978279") },
                    { 143, "", 4, 24, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Print", new Guid("b28fa05f-79de-4965-9ee1-ea2a55764857") },
                    { 142, "", 2, 24, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Filter", new Guid("39d7f8c6-af5c-4a15-8b03-a12a16fe85bb") },
                    { 141, "", 1, 24, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "View", new Guid("44794135-85c6-4beb-8590-7a8a0ce03de7") },
                    { 140, "", 64, 23, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Delete", new Guid("57d7d1ba-7af6-4b83-a849-81a5e043ddac") },
                    { 139, "", 32, 23, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Edit", new Guid("9f9981bf-393f-4a2b-8090-478b2115305e") },
                    { 138, "", 16, 23, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Create", new Guid("2a9eabf5-5c19-4455-8792-fa56255a4296") },
                    { 137, "", 8, 23, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Export", new Guid("a2c3ca30-ab91-458c-a97b-bd8c230c4444") },
                    { 136, "", 4, 23, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Print", new Guid("dce353a1-31b5-43ba-9f83-5b173bc7c64e") },
                    { 144, "", 8, 24, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Export", new Guid("475fcf28-a631-4aa8-81e0-6fee7c095b99") },
                    { 220, "", 256, 37, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "DeletePages", new Guid("fda1064b-895d-4eb5-97fd-b2852b6e0311") },
                    { 155, "", 4, 26, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Print", new Guid("2c30b5da-e2da-4c4a-b97c-9453c3d669ad") },
                    { 157, "", 16, 26, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "ViewByBranch", new Guid("2e929a5f-7e0f-4753-a6f0-4e5518e3f1c7") },
                    { 173, "", 16, 29, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "ViewByBranch", new Guid("d74c5017-80e5-4b1b-8148-0104b7c11d22") },
                    { 172, "", 8, 29, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Export", new Guid("ca390bf8-83b7-4e34-8075-e55143f95710") },
                    { 171, "", 4, 29, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Print", new Guid("7d977475-658c-4584-ac67-3a919cff67a9") },
                    { 170, "", 2, 29, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Filter", new Guid("fc07b739-6bea-4206-adef-9bbe22fb66e6") },
                    { 169, "", 1, 29, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "View", new Guid("dcd9af1a-62d9-448f-9a4a-d76fbabfc00c") },
                    { 198, "", 32, 28, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "FilterByRef", new Guid("5a2b1b2d-9a33-44a4-8056-54074ea6e326") },
                    { 168, "", 16, 28, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "ViewByBranch", new Guid("6f426555-8a15-4f5a-9b95-598c5b6a8bb6") },
                    { 167, "", 8, 28, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Export", new Guid("6a89464b-d3aa-427f-a169-1a84259ed8fd") },
                    { 156, "", 8, 26, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Export", new Guid("23c6347e-739c-4d67-91f0-e285202b25a0") },
                    { 166, "", 4, 28, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Print", new Guid("e2df51c8-1324-48cd-b42b-cbc774b59e04") },
                    { 164, "", 1, 28, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "View", new Guid("841eb4a8-0841-4cfe-ad0b-b4e8311c8153") },
                    { 163, "", 32, 27, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "ViewByBranch", new Guid("5f8dedb7-3d4c-40c8-ac81-c12fea4735ae") },
                    { 162, "", 16, 27, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Mark", new Guid("66c69ce5-c5e1-478a-8116-b959e1b5b0ce") },
                    { 161, "", 8, 27, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Export", new Guid("61e719ed-4a24-4a29-abe8-73d1b56b457a") },
                    { 160, "", 4, 27, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Print", new Guid("a9c5da55-47e4-47ae-82ec-f3d5c1bd54bd") },
                    { 159, "", 2, 27, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Filter", new Guid("2aaec074-17f9-4a89-9f4d-de81d90e7256") },
                    { 158, "", 1, 27, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "View", new Guid("a1105696-28bf-46af-a03e-96c7658cd450") },
                    { 197, "", 32, 26, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "FilterByRef", new Guid("b38392dc-a462-4df6-9b61-51c8a6d2740f") }
                });

            migrationBuilder.InsertData(
                schema: "Auth",
                table: "Permission",
                columns: new[] { "PermissionID", "Description", "Flag", "GroupId", "ModifiedDate", "Name", "rowguid" },
                values: new object[,]
                {
                    { 165, "", 2, 28, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Filter", new Guid("5d5f875d-a4ce-4c39-916a-3036369258b4") },
                    { 135, "", 2, 23, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Filter", new Guid("d318326d-0f05-40da-825d-5a062a7397fe") },
                    { 221, "", 512, 37, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "CancelPage", new Guid("52df59e8-1b76-4e3b-b096-bb77caa8ad60") },
                    { 223, "", 2048, 37, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "ConnectToCheck", new Guid("893471e2-c939-4991-94c4-355fe9598ca7") },
                    { 100011, "", 8, 100002, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Export", new Guid("107916bf-201e-4b1e-b42d-886c33312fc9") },
                    { 100010, "", 4, 100002, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Print", new Guid("d174aca9-ffbd-422f-a38f-cb9661ff4758") },
                    { 100009, "", 2, 100002, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Filter", new Guid("7af5388f-0e02-4c63-a581-57f800c18748") },
                    { 100008, "", 1, 100002, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "View", new Guid("d9e43490-807b-4b9c-9312-3f64cec0c357") },
                    { 100007, "", 64, 100001, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Delete", new Guid("ee84bb81-877d-42b0-bf7c-a8ae90fb6eb3") },
                    { 100006, "", 32, 100001, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Edit", new Guid("92a57e14-d4c7-4abd-ba45-19075d187ecd") },
                    { 100005, "", 16, 100001, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Create", new Guid("b4b7e4a2-b05a-4ff5-9923-d0317c54108c") },
                    { 100004, "", 8, 100001, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Export", new Guid("dabc211f-4bca-499e-992c-51d8711bccad") },
                    { 100003, "", 4, 100001, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Print", new Guid("57dcf42d-fadd-490a-b8da-d1294a55d331") },
                    { 100002, "", 2, 100001, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Filter", new Guid("acd1316d-e964-412b-b7bb-a01b1c723102") },
                    { 100001, "", 1, 100001, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "View", new Guid("6157196e-2bde-41cb-8238-1641e4758fea") },
                    { 283, "", 2048, 42, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "UndoRegister", new Guid("cf346202-eff2-42ab-bc7d-a887abf30c6f") },
                    { 267, "", 1024, 42, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "UndoApprove", new Guid("3f5ee0b6-eea9-4ea3-b2b1-d360da73f008") },
                    { 266, "", 512, 42, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Approve", new Guid("f291841c-d3e7-4a64-9622-87e82e6a92af") },
                    { 265, "", 256, 42, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "UndoConfirm", new Guid("256137ae-13dd-4d14-84ad-979af86e85b1") },
                    { 264, "", 128, 42, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Confirm", new Guid("16e99da0-fcb7-4d24-b63f-84e4d841ef17") },
                    { 263, "", 64, 42, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Register", new Guid("8bcdaf5e-29c4-4dbf-88f7-7d325e65e0a0") },
                    { 100012, "", 16, 100002, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Create", new Guid("a56e253a-39cf-4bbe-a211-e31d47c7db59") },
                    { 100013, "", 32, 100002, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Edit", new Guid("50151068-734a-46a9-84d8-26ec21b49d23") },
                    { 100014, "", 64, 100002, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Delete", new Guid("06c77041-637a-4b3e-9066-959d74121fe5") },
                    { 100015, "", 1, 100003, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "View", new Guid("b39a2357-0e70-4d73-9086-9b8f677be221") },
                    { 100033, "", 16, 100005, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Create", new Guid("d5587b33-8c64-44af-a4ef-fecd5df299de") },
                    { 100032, "", 8, 100005, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Export", new Guid("6cf767df-637e-4d48-b4c9-e0a3830ef0b5") },
                    { 100031, "", 4, 100005, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Print", new Guid("3d6cc315-76c2-4103-8005-46e73d283dd4") },
                    { 100030, "", 2, 100005, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Filter", new Guid("87f2f9cf-0952-4f4d-b542-68fe1db885e7") },
                    { 100029, "", 1, 100005, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "View", new Guid("f0bab472-d0f5-419c-9a17-e9a60f867457") },
                    { 100028, "", 64, 100004, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Delete", new Guid("0f26f83e-8cb3-438a-92b2-33eb7f071f59") },
                    { 100027, "", 32, 100004, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Edit", new Guid("49008c18-43f8-4ce4-b4d7-197d7785a154") },
                    { 100026, "", 16, 100004, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Create", new Guid("4220e179-a5a2-4ce8-b2fa-59a83df56573") },
                    { 262, "", 32, 42, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "NavigateEntities,Receipts", new Guid("31a57784-ece9-429e-96b2-cf7b930f19eb") },
                    { 100025, "", 8, 100004, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Export", new Guid("a1defbe3-e0cd-484f-86c4-1a8eb36dfdfc") },
                    { 100023, "", 2, 100004, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Filter", new Guid("094f5d70-3289-47c1-99d1-5220a449c133") },
                    { 100022, "", 1, 100004, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "View", new Guid("bc486934-7e5d-4bf6-bf43-edeef46918ba") },
                    { 100021, "", 64, 100003, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Delete", new Guid("d5d1c754-0d78-4f70-bc5c-224d230dadc1") },
                    { 100020, "", 32, 100003, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Edit", new Guid("8c1353cd-d1ea-491d-bf6b-7571fe0917b8") },
                    { 100019, "", 16, 100003, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Create", new Guid("b7731293-f7fe-4d8a-8a1c-98afdd562501") },
                    { 100018, "", 8, 100003, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Export", new Guid("d8117f7c-61e0-4d0f-801f-e3afb549dff7") },
                    { 100017, "", 4, 100003, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Print", new Guid("c1ae9705-8a5d-4592-9c23-627e47ecb7bf") }
                });

            migrationBuilder.InsertData(
                schema: "Auth",
                table: "Permission",
                columns: new[] { "PermissionID", "Description", "Flag", "GroupId", "ModifiedDate", "Name", "rowguid" },
                values: new object[,]
                {
                    { 100016, "", 2, 100003, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Filter", new Guid("1466baa0-3eb5-4a3d-a139-c3421ab20d81") },
                    { 100024, "", 4, 100004, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Print", new Guid("b5e061b7-f55d-4349-a23e-20d85b63a89f") },
                    { 261, "", 16, 42, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Delete", new Guid("d4f92a97-bb81-4310-9f5e-e62dd9f6c794") },
                    { 260, "", 8, 42, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Edit", new Guid("156a46cc-f48a-4941-bbe6-584544f73467") },
                    { 259, "", 4, 42, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Create", new Guid("f87721cd-bf69-4a08-bdbf-0f2893ffdfae") },
                    { 239, "", 1, 40, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "View", new Guid("accd022e-aada-4b38-9780-65d94a291fce") },
                    { 238, "", 32, 39, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "UndoArchive", new Guid("215656bf-5d89-4c20-92c6-2bef59583c11") },
                    { 237, "", 16, 39, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Archive", new Guid("5157b25a-4348-4313-b131-1abe9f88cc48") },
                    { 236, "", 8, 39, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Export", new Guid("56d2d5ed-7335-46f8-bfb8-1c0e85640430") },
                    { 235, "", 4, 39, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Print", new Guid("8b4934ab-fbef-454f-8c55-5b2c941c070b") },
                    { 234, "", 2, 39, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Filter", new Guid("173abc00-545d-4bbe-9de8-dfce367b8b4f") },
                    { 233, "", 1, 39, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "View", new Guid("4ed8e34a-19c1-4671-97cc-69834390dede") },
                    { 279, "Mark an inactive cash register as active", 512, 38, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Reactivate", new Guid("adb8bc62-c8f4-4562-8be9-60a63f15efd2") },
                    { 240, "", 2, 40, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Filter", new Guid("d5616002-b35b-47ba-a071-75aec861f005") },
                    { 278, "Mark an active cash register as inactive", 256, 38, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Deactivate", new Guid("d68b0c7e-b68e-40db-95b3-10d946ff0017") },
                    { 231, "", 64, 38, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Delete", new Guid("8f33780e-f0dc-48b6-886b-c43f46c018d8") },
                    { 230, "", 32, 38, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Edit", new Guid("965cc295-eadb-40e5-b51d-a36653df33cb") },
                    { 229, "", 16, 38, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Create", new Guid("bfa4e632-1bb8-4cbe-b04d-6056243181b8") },
                    { 228, "", 8, 38, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Export", new Guid("d19f983f-c6d4-4c97-a5e2-ea94061b2a40") },
                    { 227, "", 4, 38, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Print", new Guid("801d9319-ec80-4bfc-a0bd-a2319bd7ed17") },
                    { 226, "", 2, 38, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Filter", new Guid("d0120114-1a8b-4919-99ac-afd64d1c592a") },
                    { 225, "", 1, 38, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "View", new Guid("80761dbe-af55-4b1e-bf80-bdec8a9ffe2e") },
                    { 224, "", 4096, 37, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "DisconnectFromCheck", new Guid("cbd36a22-8724-4fe8-bf5b-5f5ec4c41e2f") },
                    { 232, "", 128, 38, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "AssignCashRegisterUser", new Guid("cc531090-f436-46d6-a095-26b31434108c") },
                    { 222, "", 1024, 37, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "UndoCancelPage", new Guid("e7e97e60-b86e-46f4-9c61-d850735835b8") },
                    { 241, "", 4, 40, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Print", new Guid("b7396ac3-8df7-40c6-beb5-2da7dc727573") },
                    { 243, "", 16, 40, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Create", new Guid("f2597db5-e1ba-4c8e-ab1c-721be0955e3e") },
                    { 258, "", 2, 42, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Print", new Guid("afdd2186-4ced-421c-93ca-2bfcdf1ffc6d") },
                    { 257, "", 1, 42, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "View", new Guid("5b66d3bf-c898-4956-a637-f01bed9ee3da") },
                    { 282, "", 2048, 41, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "UndoRegister", new Guid("b411992e-6c23-4e7b-811a-c523fd359a3e") },
                    { 256, "", 1024, 41, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "UndoApprove", new Guid("553e77be-bfdf-4d5c-b9d1-762ec52b2174") },
                    { 255, "", 512, 41, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Approve", new Guid("fcc27c26-24ef-4c7f-926b-c040349be3d6") },
                    { 254, "", 256, 41, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "UndoConfirm", new Guid("418fa4d5-4d80-4b85-a981-d276ff9f0dd9") },
                    { 253, "", 128, 41, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Confirm", new Guid("6223e956-5d09-4a12-8bb8-03f583466a40") },
                    { 252, "", 64, 41, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Register", new Guid("b9f5e141-0e53-4d02-a037-58099607202d") },
                    { 242, "", 8, 40, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Export", new Guid("aea6e660-2adc-4176-a91f-8539b98b8bae") },
                    { 251, "", 32, 41, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "NavigateEntities,Payments", new Guid("0061d6f8-8c58-4d8a-9a2a-0013bcca4322") },
                    { 249, "", 8, 41, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Edit", new Guid("439081ab-d156-476b-94e1-a981a964b652") },
                    { 248, "", 4, 41, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Create", new Guid("fb379a91-65d3-47fa-85d8-255935589703") },
                    { 247, "", 2, 41, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Print", new Guid("d3ae7030-36d5-4ba7-b3eb-06b4c13cac97") },
                    { 246, "", 1, 41, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "View", new Guid("6d0fa4f6-1b5a-4fd8-9125-352534553791") },
                    { 281, "Mark an inactive source/application as active", 256, 40, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Reactivate", new Guid("13aebd29-3e0d-4d5d-973d-d554c74e8377") }
                });

            migrationBuilder.InsertData(
                schema: "Auth",
                table: "Permission",
                columns: new[] { "PermissionID", "Description", "Flag", "GroupId", "ModifiedDate", "Name", "rowguid" },
                values: new object[,]
                {
                    { 280, "Mark an active source/application as inactive", 128, 40, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Deactivate", new Guid("a5242251-29c6-4fca-a47b-41a1b2fb45d9") },
                    { 245, "", 64, 40, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Delete", new Guid("326235d2-69eb-4655-a413-3732fe2204d9") },
                    { 244, "", 32, 40, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Edit", new Guid("ce05aaba-c76e-48d0-9e5b-696eda7d09aa") },
                    { 250, "", 16, 41, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Delete", new Guid("05a0e9f1-fd91-49dc-ab13-5c8b85dbf95b") },
                    { 134, "", 1, 23, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "View", new Guid("915d7c08-8a50-405f-afa6-4230740799b0") },
                    { 100035, "", 64, 100005, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Delete", new Guid("8e59d77a-a3d2-4b61-899b-bf15d58221a8") },
                    { 126, "", 1, 19, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "View", new Guid("a34af723-2927-4b85-94eb-198c43e1281b") },
                    { 131, "", 2, 21, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Save", new Guid("e5630c48-b7d9-4a4c-b863-72b86fa4d727") },
                    { 130, "", 1, 21, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "View", new Guid("9a81e949-07b7-4346-b0f0-839c1832bce4") },
                    { 129, "", 2, 20, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Save", new Guid("609a8757-041c-476a-b115-286443a3bb3a") },
                    { 128, "", 1, 20, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "View", new Guid("707dc42b-40dc-4239-9f9d-b05cf654b156") },
                    { 125, "", 4, 18, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "SetDefault", new Guid("c14291c8-3648-4adc-a8d3-e6a1bd2a78aa") },
                    { 124, "", 2, 18, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Delete", new Guid("1304efa0-59d1-4106-8ac5-40bc2ffcae8c") },
                    { 132, "", 1, 22, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "View", new Guid("964ce0d3-f35f-46e0-8c2c-df61e30862b4") },
                    { 123, "", 1, 18, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Save", new Guid("c48c85e5-d05e-4754-a135-986d3f1d56d9") },
                    { 121, "", 2, 17, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Design", new Guid("b5851f43-0937-42ed-9cee-c38a906a80c4") },
                    { 120, "", 1, 17, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "View", new Guid("f9c3a03b-d56a-4f5d-b59f-34c17998a2b7") },
                    { 119, "", 32, 16, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "ViewArchive", new Guid("50a81b3d-2e63-4eb6-bb74-55ec7e2117cd") },
                    { 118, "", 16, 16, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Archive", new Guid("8d7bf8b8-ba29-4a6b-aa06-1e064a00d6fe") },
                    { 117, "", 8, 16, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Export", new Guid("292e94a8-f63d-4623-8ec0-53b1ec09ad0f") },
                    { 116, "", 4, 16, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Print", new Guid("90c58605-bd58-405e-9bc6-5995e109378d") },
                    { 122, "", 4, 17, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "QuickReportDesign", new Guid("06a983e2-35b4-4fd6-8510-f90a1c4af614") },
                    { 133, "", 2, 22, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Save", new Guid("9b16743c-3157-41ce-99f9-b92236d7009f") },
                    { 174, "", 1, 30, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "View", new Guid("c6d4c5d2-4a90-43db-9e80-28eacf56b91a") },
                    { 284, "", 2, 30, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Filter", new Guid("78f7a8c6-9f49-4c76-b5a6-835adb815f70") },
                    { 11, "", 8, 2, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Export", new Guid("1ca45249-8aae-46fe-98a1-661dc9313961") },
                    { 10, "", 4, 2, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Print", new Guid("2feef406-a1ef-427f-9f1b-68f59158b618") },
                    { 9, "", 2, 2, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Filter", new Guid("cfec39fa-d87a-4e6e-9f67-043d051de975") },
                    { 8, "", 1, 2, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "View", new Guid("d5cfc463-1602-4760-b231-28d5c5d4c2cb") },
                    { 269, "Mark an inactive account as active", 256, 1, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Reactivate", new Guid("8f3a7b2d-2d30-49ab-b96a-4477e6bff813") },
                    { 268, "Mark an active account as inactive", 128, 1, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Deactivate", new Guid("17585374-8a06-437e-a92e-4e90688d7812") },
                    { 7, "", 64, 1, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Delete", new Guid("e6624567-a9b1-4794-ad39-04d123117e54") },
                    { 6, "", 32, 1, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Edit", new Guid("9472f632-dbd6-456f-bd61-799034d9a75b") },
                    { 5, "", 16, 1, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Create", new Guid("cbec48ad-4665-4ec6-a466-1dbaea3dc9f8") },
                    { 4, "", 8, 1, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Export", new Guid("3e3b33c1-c653-4653-8ae3-18438ef0b52c") },
                    { 100034, "", 32, 100005, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Edit", new Guid("c71901fd-5868-4520-841c-2c2719710100") },
                    { 2, "", 2, 1, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Filter", new Guid("6b190ef3-0f38-4589-8542-e9e6f4f47250") },
                    { 1, "", 1, 1, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "View", new Guid("38578e8c-2373-48ab-886b-b40c6e87a1c5") },
                    { 286, "", 8, 30, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Export", new Guid("ac8820e8-bbe6-4819-9d52-e99bc13c1197") },
                    { 285, "", 4, 30, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Print", new Guid("646ccfab-54a3-4e2f-80c0-3164ddccfa79") },
                    { 115, "", 2, 16, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Filter", new Guid("06763c35-8ad9-4dee-a50b-0731fd544282") },
                    { 12, "", 16, 2, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Create", new Guid("ac77aac4-820f-4191-afdb-f5cef6d114a2") }
                });

            migrationBuilder.InsertData(
                schema: "Auth",
                table: "Permission",
                columns: new[] { "PermissionID", "Description", "Flag", "GroupId", "ModifiedDate", "Name", "rowguid" },
                values: new object[,]
                {
                    { 114, "", 1, 16, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "View", new Guid("2d3419a9-7b12-4522-afb6-005659737b5d") },
                    { 112, "", 16, 15, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Archive", new Guid("d2351bbd-7740-4c8c-bdf4-852cf051494b") },
                    { 83, "", 2, 11, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Filter", new Guid("88172f82-c2fc-44ac-9963-dc4088f23d3d") },
                    { 82, "", 1, 11, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "View", new Guid("64407319-6468-4929-ae2a-95952db9d96e") },
                    { 81, "", 8, 10, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Export", new Guid("62572e63-62e7-4513-b932-8cd50c292dff") },
                    { 80, "", 4, 10, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Print", new Guid("aa1df4d0-7a9b-4fe5-9d6d-d7514f41f2cc") },
                    { 79, "", 2, 10, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Filter", new Guid("7fd57f44-1aec-4317-b2bd-ec6cc7e4b67c") },
                    { 78, "", 1, 10, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "View", new Guid("0b8c2b9c-be0a-455c-830b-56b60e6d9a11") },
                    { 84, "", 4, 11, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Print", new Guid("3325d86d-399f-415c-9eb3-60656dbfc098") },
                    { 77, "", 128, 9, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "AssignRolesToEntity,Branch", new Guid("4d640dbe-eca8-44e3-8627-602f9965914f") },
                    { 75, "", 32, 9, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Edit", new Guid("5571334a-0390-47b2-880c-dde58a3ffcea") },
                    { 74, "", 16, 9, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Create", new Guid("554c255d-c15e-4ff7-a899-dbf47d0e523f") },
                    { 73, "", 8, 9, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Export", new Guid("a48b9978-8685-475e-a3f6-0788ea0d86ee") },
                    { 72, "", 4, 9, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Print", new Guid("349928c5-bec1-4f05-a9bd-a5fcea6c155d") },
                    { 71, "", 2, 9, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Filter", new Guid("b7e93364-bf49-4778-abcc-5063278d5368") },
                    { 70, "", 1, 9, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "View", new Guid("13f7a5bb-9ba0-43b3-8cfe-265125b4741d") },
                    { 76, "", 64, 9, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Delete", new Guid("3e1be9b1-d652-4d45-bfe7-39d863ac09f4") },
                    { 85, "", 8, 11, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Export", new Guid("8230018b-9f53-4988-b709-aa4c0713ea15") },
                    { 86, "", 16, 11, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Create", new Guid("f8235860-0ef8-43bb-89f9-10c1016141cd") },
                    { 87, "", 32, 11, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Edit", new Guid("0826a4f9-1a2b-40a1-b680-1eb2de95b927") },
                    { 111, "", 8, 15, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Export", new Guid("5600d87b-c254-4151-aa28-0c2acb272ec7") },
                    { 110, "", 4, 15, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Print", new Guid("e46955ae-7ea1-4f7e-9966-7bd6a00cd2ba") },
                    { 109, "", 2, 15, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Filter", new Guid("b138b16a-ed8b-436b-abeb-ac8eb87bb9ee") },
                    { 108, "", 1, 15, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "View", new Guid("a9a00716-3eed-4c53-aaf7-a4466ea8eb06") },
                    { 98, "", 512, 12, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "AssignEntityToRole,FiscalPeriod", new Guid("fb76d6d1-38c1-49c3-83d0-791a8ed00a82") },
                    { 97, "", 256, 12, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "AssignEntityToRole,Branch", new Guid("972a5148-4fed-4e8c-92d2-4f0a066d0f27") },
                    { 96, "", 128, 12, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "AssignEntityToRole,User", new Guid("ebc04a98-41c8-4e62-b2a0-99274935317b") },
                    { 95, "", 64, 12, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Delete", new Guid("814eb634-bd2e-4106-b0dc-5c495a54b408") },
                    { 94, "", 32, 12, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Edit", new Guid("fd485a6a-addd-45e3-ac8e-86b90230ee9c") },
                    { 93, "", 16, 12, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Create", new Guid("d1d1fe40-9ff4-4037-b9fa-3f59b287fc5d") },
                    { 92, "", 8, 12, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Export", new Guid("3b1efd51-16f6-4438-b16e-412e1b7ede27") },
                    { 91, "", 4, 12, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Print", new Guid("813f21b1-e9e8-4f72-b1fe-976b7ecccc1c") },
                    { 90, "", 2, 12, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Filter", new Guid("4207dc3d-7465-4a0d-8ab9-c91cdb467ad6") },
                    { 89, "", 1, 12, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "View", new Guid("3a652c4e-dbdf-475b-bc98-dca013fe6459") },
                    { 88, "", 64, 11, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "AssignRolesToEntity,User", new Guid("6e51255d-129a-4673-906a-e977cc6e8385") },
                    { 113, "", 32, 15, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "ViewArchive", new Guid("5a2498a2-c757-48de-a84f-e28f5fcadacf") },
                    { 13, "", 32, 2, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Edit", new Guid("cb4743a5-33b6-4283-b74b-63da36849ccc") },
                    { 3, "", 4, 1, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Print", new Guid("4a4e5f22-0557-464b-80db-fefe9b352e0c") },
                    { 270, "Mark an active detail account as inactive", 128, 2, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Deactivate", new Guid("22949dc5-80b7-46d2-b6b8-382d2109e428") },
                    { 59, "", 16384, 7, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Finalize", new Guid("396c42ef-d6a3-4977-b7f2-0faecc8ec068") },
                    { 58, "", 8192, 7, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "UndoApprove", new Guid("913e435e-35e7-4d93-9df8-3f96e04c22e4") },
                    { 57, "", 4096, 7, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Approve", new Guid("0fee6e23-d786-4312-8bc0-def2db3e1d65") }
                });

            migrationBuilder.InsertData(
                schema: "Auth",
                table: "Permission",
                columns: new[] { "PermissionID", "Description", "Flag", "GroupId", "ModifiedDate", "Name", "rowguid" },
                values: new object[,]
                {
                    { 14, "", 64, 2, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Delete", new Guid("35fb0168-d6de-4d66-ab10-2e571d1a88e4") },
                    { 55, "", 1024, 7, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Confirm", new Guid("4d4ad8aa-0da4-4103-ba7e-c5dc86dfc3fe") },
                    { 54, "", 512, 7, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "UndoCheck", new Guid("d8b203e9-d7a1-45a7-92ed-ea08d6711744") },
                    { 60, "", 32768, 7, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "NavigateEntities,Vouchers", new Guid("9a201b37-dcc1-405f-b9da-8cb830aed932") },
                    { 53, "", 256, 7, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Check", new Guid("056eed7b-176f-443a-a84a-f41ef412cf2e") },
                    { 51, "", 64, 7, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "EditLine", new Guid("90e85f61-57a6-45e9-903b-a538a7fb4d73") },
                    { 50, "", 32, 7, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "CreateLine", new Guid("7bfe952d-8b89-4961-b59e-11d911ee8b09") },
                    { 49, "", 16, 7, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Print", new Guid("b7eececd-713c-4d71-8157-c8bdb24670f6") },
                    { 48, "", 8, 7, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Delete", new Guid("2429e68a-230e-44b9-ad26-ffb81c1d9743") },
                    { 47, "", 4, 7, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Edit", new Guid("b3cfce9a-328f-40ba-8351-4416e2f63077") },
                    { 46, "", 2, 7, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Create", new Guid("e012d9cf-0a75-4e36-b202-e3c6f8fa9921") },
                    { 52, "", 128, 7, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "DeleteLine", new Guid("1e61d76c-29e4-4e9b-af63-7d78093efbbb") },
                    { 61, "", 1, 8, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "View", new Guid("0177fbc9-9927-4aac-a58c-55d288be2188") },
                    { 62, "", 2, 8, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Filter", new Guid("58062eba-c47f-4418-b025-6bf78278bad2") },
                    { 63, "", 4, 8, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Print", new Guid("785c25cb-d78c-4387-a025-e2f3c0479160") },
                    { 107, "", 2, 14, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Save", new Guid("47b6d59b-7c41-4089-89b5-2af302175329") },
                    { 106, "", 1, 14, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "View", new Guid("86d9141b-d060-4405-a182-f028155205f1") },
                    { 105, "", 64, 13, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Delete", new Guid("758de475-4d0a-41fd-a45b-e1061da7c8fe") },
                    { 104, "", 32, 13, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Edit", new Guid("f28aa706-d8a9-429f-8836-ec673adcc6c0") },
                    { 103, "", 16, 13, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Create", new Guid("08f0edd8-542a-4e77-9f31-ca57cc066f23") },
                    { 102, "", 8, 13, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Export", new Guid("68a5a08c-5178-4ee1-9de9-3dd0123cca43") },
                    { 101, "", 4, 13, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Print", new Guid("471ca4fa-158a-40eb-bd63-2a5d8b6b1011") },
                    { 100, "", 2, 13, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Filter", new Guid("5f818bf0-b944-43d2-ae5c-8c45b4348aaa") },
                    { 99, "", 1, 13, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "View", new Guid("15fda3f1-42d5-4274-8360-d878db3b7426") },
                    { 69, "", 256, 8, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "GroupFinalize", new Guid("b23fc837-f23a-4589-ab23-34d4a76b67a3") },
                    { 68, "", 128, 8, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "GroupUndoConfirm", new Guid("319d5a94-f817-4aea-abe4-c1eb0715f618") },
                    { 67, "", 64, 8, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "GroupConfirm", new Guid("24d35a7c-2594-420d-8670-f143c7225b0a") },
                    { 66, "", 32, 8, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "GroupUndoCheck", new Guid("93af852f-e7e2-4670-b151-e36b6105d0cd") },
                    { 65, "", 16, 8, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "GroupCheck", new Guid("cdb30c61-c245-4ce3-ba20-023c0bdb6220") },
                    { 64, "", 8, 8, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Export", new Guid("9dd6be99-755d-48c5-b446-d62b968d4395") },
                    { 45, "", 1, 7, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "View", new Guid("6a2bc063-8f48-4667-91c8-5e0a700300d6") },
                    { 277, "Mark an inactive currency as active", 256, 6, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Reactivate", new Guid("3ef5040b-0213-42f4-bcf5-41a9737005cd") },
                    { 56, "", 2048, 7, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "UndoConfirm", new Guid("9c59a657-4c4b-42ee-9384-2810db499fb9") },
                    { 44, "", 128, 6, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "ChangeStatus", new Guid("747f5693-797f-4b20-be26-fe7b481af688") },
                    { 26, "", 16, 4, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Create", new Guid("b96e71f2-a677-4889-9077-20c13b9ee990") },
                    { 25, "", 8, 4, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Export", new Guid("c951df75-b658-4890-97ea-7882748a0d32") },
                    { 24, "", 4, 4, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Print", new Guid("c0ff296c-611e-4545-9fb9-28e9a26c7dae") },
                    { 23, "", 2, 4, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Filter", new Guid("26376ccc-6b87-4e82-aa66-4e606db3c3d6") },
                    { 22, "", 1, 4, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "View", new Guid("cd4ba701-2086-4c31-b245-ea299a225c88") },
                    { 273, "Mark an inactive cost center as active", 256, 3, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Reactivate", new Guid("7fe2b7de-16c6-4f8c-88e9-f327648ce818") },
                    { 272, "Mark an active cost center as inactive", 128, 3, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Deactivate", new Guid("cbcf4ed3-db93-4f80-af70-d135769524d7") },
                    { 21, "", 64, 3, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Delete", new Guid("3a9519fa-5259-4f82-b715-1ddc609662c3") }
                });

            migrationBuilder.InsertData(
                schema: "Auth",
                table: "Permission",
                columns: new[] { "PermissionID", "Description", "Flag", "GroupId", "ModifiedDate", "Name", "rowguid" },
                values: new object[,]
                {
                    { 20, "", 32, 3, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Edit", new Guid("63eba9ad-b19e-4ac7-9f6c-2d2f1915650e") },
                    { 19, "", 16, 3, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Create", new Guid("82ccc58e-a88f-4a8b-8eef-b15de1d9b3d2") },
                    { 18, "", 8, 3, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Export", new Guid("e2bc8e3c-5163-4d02-8a88-1ed33174c4d3") },
                    { 276, "Mark an active currency as inactive", 128, 6, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Deactivate", new Guid("6da40e96-4a3d-42f4-b15c-fe84348e97b0") },
                    { 16, "", 2, 3, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Filter", new Guid("2ebae83a-7c37-495f-9a30-554774574afe") },
                    { 15, "", 1, 3, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "View", new Guid("58c2026b-f099-49be-9ddf-de5535858159") },
                    { 271, "Mark an inactive detail account as active", 256, 2, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Reactivate", new Guid("368ee22f-c9a0-4557-90d7-835ada2a7112") },
                    { 27, "", 32, 4, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Edit", new Guid("e9b682be-8ea2-41f8-a026-8847c66c5e9f") },
                    { 28, "", 64, 4, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Delete", new Guid("389aa8a1-093c-4f3a-9e7d-ba184f429e2f") },
                    { 17, "", 4, 3, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Print", new Guid("144979a2-c196-4d3c-96fa-5516ce6cfd05") },
                    { 275, "Mark an inactive project as active", 256, 4, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Reactivate", new Guid("6a978075-ead0-4911-bde6-452a8de26365") },
                    { 274, "Mark an active project as inactive", 128, 4, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Deactivate", new Guid("9752e2df-c05f-4ee4-91bd-98fb58ab1ee5") },
                    { 43, "", 64, 6, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Delete", new Guid("0ee8a5f8-ce80-4b1b-ae4c-c21bee0d0ab5") },
                    { 42, "", 32, 6, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Edit", new Guid("12292ac4-09a5-42b0-a6f5-b5493d90b9f1") },
                    { 41, "", 16, 6, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Create", new Guid("2f5e7ba4-650c-40d4-8fe5-7bed84202a6b") },
                    { 39, "", 4, 6, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Print", new Guid("f0b2aa86-ee7b-443a-b333-b6810a7e7561") },
                    { 38, "", 2, 6, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Filter", new Guid("f6eefdb9-cb32-482a-916b-f93ec07357db") },
                    { 37, "", 1, 6, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "View", new Guid("411b3ebc-1435-47bb-88eb-4aee117ce61b") },
                    { 40, "", 8, 6, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Export", new Guid("6eac9f70-1ac1-4e67-bc0b-0a4da5d70e5c") },
                    { 35, "", 64, 5, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Delete", new Guid("372c6094-0590-44d1-a09e-a988e4dc23f7") },
                    { 34, "", 32, 5, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Edit", new Guid("3ea8c950-d4f8-4331-b700-bbe977f028f0") },
                    { 33, "", 16, 5, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Create", new Guid("ff62f582-9cd7-41f7-9ed5-36950bab94ae") },
                    { 32, "", 8, 5, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Export", new Guid("87d92780-5c5d-4197-afed-47c76d8e6b9e") },
                    { 31, "", 4, 5, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Print", new Guid("1b0d951a-edf8-4310-b7db-66a977eddc6f") },
                    { 30, "", 2, 5, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "Filter", new Guid("9d138f13-ba43-43d2-b37b-d84de1dc8b8c") },
                    { 36, "", 128, 5, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "AssignRolesToEntity,FiscalPeriod", new Guid("52c2c978-b08b-4695-a504-606903d91567") },
                    { 29, "", 1, 5, new DateTime(2023, 10, 16, 18, 35, 14, 0, DateTimeKind.Unspecified), "View", new Guid("1f9dc87b-94c0-47a4-af8c-1d2a93f414a4") }
                });

            migrationBuilder.InsertData(
                schema: "Reporting",
                table: "LocalReport",
                columns: new[] { "LocalReportID", "Caption", "LocaleId", "ModifiedDate", "ReportId", "rowguid", "Template" },
                values: new object[,]
                {
                    { 52, "Accounting", 4, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 13, new Guid("7053aba4-2cd6-46d2-bc36-f8ad29564d34"), "" },
                    { 51, "Accounting", 3, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 13, new Guid("01d87ec3-c6fe-4433-a26f-f3a337592757"), "" },
                    { 50, "حسابداری", 2, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 13, new Guid("f0215a21-d4dd-4993-948b-063de61eb4b7"), "" },
                    { 3, "Administration", 3, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 1, new Guid("9b5a7ecb-a2b3-4cee-869a-8806450b776a"), "" },
                    { 4, "Administration", 4, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 1, new Guid("b65a4943-2fed-4e21-a8ab-cf961d6e2949"), "" },
                    { 2, "راهبری", 2, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 1, new Guid("f3e4c0b2-b8e3-406b-90a2-06e0ce9c4de8"), "" },
                    { 165, "Manage quick reports", 1, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 42, new Guid("c524824b-cce2-42e2-8cc1-e9e3f25825a4"), "" },
                    { 49, "Accounting", 1, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 13, new Guid("251826ac-a337-4ee4-a541-ac7b48c6cb79"), "" },
                    { 166, "مدیریت گزارشات فوری", 2, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 42, new Guid("6f9304b6-2f6e-45ec-98f7-fe37b57b263d"), "" },
                    { 1, "Administration", 1, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 1, new Guid("9f254e76-723d-4f5b-8366-b9f1ec49d2b4"), "" },
                    { 168, "Manage quick reports", 4, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 42, new Guid("2a19d9c8-0da6-4c03-b682-e3b1ff132d4e"), "" },
                    { 167, "Manage quick reports", 3, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 42, new Guid("8758b476-5b17-415a-adb2-0a59b1ea205d"), "" },
                    { 100000, "ProductScope", 1, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 100000, new Guid("8b5bbdaa-061a-46dd-b364-1f829fe7847a"), "" },
                    { 100001, "محصولات", 2, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 100000, new Guid("bf380b49-c9f0-4241-90a4-cb81d13c5cd7"), "" }
                });

            migrationBuilder.InsertData(
                schema: "Reporting",
                table: "Report",
                columns: new[] { "ReportID", "Code", "CreatedById", "IsDefault", "IsDynamic", "IsGroup", "IsSystem", "ModifiedDate", "ParentId", "ResourceKeys", "ServiceUrl", "SubsystemId", "ViewId" },
                values: new object[] { 14, "Accnt-Base", 1, false, false, true, true, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 13, "", "", 2, null });

            migrationBuilder.InsertData(
                schema: "Reporting",
                table: "Report",
                columns: new[] { "ReportID", "Code", "CreatedById", "IsDefault", "IsDynamic", "IsGroup", "IsSystem", "ModifiedDate", "ParentId", "ResourceKeys", "ServiceUrl", "SubsystemId", "ViewId" },
                values: new object[,]
                {
                    { 94, "Treasury-Base", 1, false, false, true, true, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 93, "", "", 3, null },
                    { 95, "Treasury-Operation", 1, false, false, true, true, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 93, "", "", 3, null },
                    { 16, "Accnt-Report", 1, false, false, true, true, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 13, "", "", 2, null },
                    { 43, "QReport-Design-Template", 1, true, false, false, false, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 42, "", "", 1, null },
                    { 4, "Admin-Report", 1, false, false, true, true, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 1, "", "", 1, null },
                    { 3, "Admin-Operation", 1, false, false, true, true, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 1, "", "", 1, null },
                    { 2, "Admin-Base", 1, false, false, true, true, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 1, "", "", 1, null },
                    { 100001, "ProductScope-Base", 1, false, false, true, true, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 100000, "", "", 100000, null },
                    { 100002, "ProductScope-Operation", 1, false, false, true, true, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 100000, "", "", 100000, null },
                    { 100003, "ProductScope-Report", 1, false, false, true, true, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 100000, "", "", 100000, null },
                    { 15, "Accnt-Operation", 1, false, false, true, true, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 13, "", "", 2, null },
                    { 96, "Treasury-Report", 1, false, false, true, true, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 93, "", "", 3, null }
                });

            migrationBuilder.InsertData(
                schema: "Metadata",
                table: "Command",
                columns: new[] { "CommandID", "HotKey", "IconName", "Index", "ModifiedDate", "ParentId", "PermissionId", "RouteUrl", "rowguid", "TitleKey" },
                values: new object[,]
                {
                    { 100008, "", "list", null, new DateTime(2023, 10, 16, 15, 56, 33, 0, DateTimeKind.Unspecified), 100001, 100029, "/product-scope/files", new Guid("5a4d08af-e7ee-44a7-a52a-6be039c623eb"), "Files" },
                    { 3, "Ctrl+Shift+G", "th-large", null, new DateTime(2023, 10, 16, 15, 56, 33, 0, DateTimeKind.Unspecified), 2, 99, "/finance/account-groups", new Guid("6f57dd47-8462-40b7-af10-1e6b61581a85"), "AccountGroup" },
                    { 15, "Ctrl+Shift+V", "list", null, new DateTime(2023, 10, 16, 15, 56, 33, 0, DateTimeKind.Unspecified), 11, 61, "/finance/voucher", new Guid("3d3d8c1b-86fd-4060-8d27-5e701bb919e5"), "Vouchers" },
                    { 14, "Ctrl+L", "list", null, new DateTime(2023, 10, 16, 15, 56, 33, 0, DateTimeKind.Unspecified), 11, 60, "/finance/vouchers/last", new Guid("4034bfcf-998a-46d9-a236-d2adb77afc8b"), "LastVoucher" },
                    { 12, "Ctrl+Alt+N", "plus", null, new DateTime(2023, 10, 16, 15, 56, 33, 0, DateTimeKind.Unspecified), 11, 46, "/finance/vouchers/new", new Guid("a3a93b79-381f-484f-a45d-49bf466e7aef"), "NewVoucher" },
                    { 19, "Ctrl+Alt+I", "list", null, new DateTime(2023, 10, 16, 15, 56, 33, 0, DateTimeKind.Unspecified), 16, 45, "/finance/vouchers/close-temp-accounts", new Guid("aa9f5921-6e05-4c2a-a2d0-7e886d133f42"), "ClosingTempAccounts" },
                    { 18, "Ctrl+Alt+U", "list", null, new DateTime(2023, 10, 16, 15, 56, 33, 0, DateTimeKind.Unspecified), 16, 45, "/finance/vouchers/closing-voucher", new Guid("e06b470f-aa0c-4a32-ae9d-2fb7c119eff6"), "IssueClosingVoucher" },
                    { 17, "Ctrl+Alt+Y", "list", null, new DateTime(2023, 10, 16, 15, 56, 33, 0, DateTimeKind.Unspecified), 16, 45, "/finance/vouchers/opening-voucher", new Guid("c09de6d3-c74b-46ea-b068-77d8568ce9f1"), "IssueOpeningVoucher" },
                    { 13, "Ctrl+S", "search", null, new DateTime(2023, 10, 16, 15, 56, 33, 0, DateTimeKind.Unspecified), 11, 45, "/finance/vouchers/by-no", new Guid("1646bb1f-71a5-4c6d-be2c-4188bf5e1551"), "VoucherByNo" },
                    { 10, "Ctrl+Shift+U", "usd", null, new DateTime(2023, 10, 16, 15, 56, 33, 0, DateTimeKind.Unspecified), 2, 37, "/finance/currency", new Guid("4fbbd932-bede-412a-be6f-6920b61d1795"), "Currency" },
                    { 26, "Ctrl+Shift+F", "list", null, new DateTime(2023, 10, 16, 15, 56, 33, 0, DateTimeKind.Unspecified), 23, 29, "/organization/fiscalperiod", new Guid("83b88989-aaa8-4354-bdd0-456898202fc4"), "FiscalPeriods" },
                    { 7, "Ctrl+Shift+P", "file", null, new DateTime(2023, 10, 16, 15, 56, 33, 0, DateTimeKind.Unspecified), 2, 22, "/finance/projects", new Guid("c8926ea9-f1c4-4eb0-adcb-988b7d46d6f9"), "Project" },
                    { 9, "Ctrl+Shift+H", "list", null, new DateTime(2023, 10, 16, 15, 56, 33, 0, DateTimeKind.Unspecified), 2, 106, "/finance/account-collection", new Guid("762c16d2-2169-4cce-b38e-106209049e9b"), "AccountCollections" },
                    { 6, "Ctrl+Shift+C", "tower", null, new DateTime(2023, 10, 16, 15, 56, 33, 0, DateTimeKind.Unspecified), 2, 15, "/finance/costCenter", new Guid("2e81c3e7-3774-40b7-b20f-aa40ceca9aa5"), "CostCenter" },
                    { 4, "Ctrl+Shift+A", "th-list", null, new DateTime(2023, 10, 16, 15, 56, 33, 0, DateTimeKind.Unspecified), 2, 1, "/finance/account", new Guid("5594f223-578c-43ce-9c57-a8f2e0ecca8a"), "Account" },
                    { 42, "Ctrl+Shift+S", "tasks", null, new DateTime(2023, 10, 16, 15, 56, 33, 0, DateTimeKind.Unspecified), 27, 174, "/finance/system-issue", new Guid("1b5cc17d-fe7c-4c73-81b4-1f1f3d97a74b"), "SystemIssue" },
                    { 30, "Ctrl+Alt+W", "lock", null, new DateTime(2023, 10, 16, 15, 56, 33, 0, DateTimeKind.Unspecified), 27, 132, "/admin/viewRowPermission", new Guid("3e411e70-d90e-42f3-9480-d83a8792b606"), "RowAccessSettings" },
                    { 45, "", "list", null, new DateTime(2023, 10, 16, 15, 56, 33, 0, DateTimeKind.Unspecified), 27, 130, "/admin/log-settings", new Guid("93befc53-8726-41be-af11-3cbde7ba8551"), "LogSettings" },
                    { 31, "Ctrl+K", "wrench", null, new DateTime(2023, 10, 16, 15, 56, 33, 0, DateTimeKind.Unspecified), 27, 128, "/config/settings", new Guid("ae96a077-9888-44ac-b02e-b1451dd2aacb"), "Settings" },
                    { 38, "", "list", null, new DateTime(2023, 10, 16, 15, 56, 33, 0, DateTimeKind.Unspecified), 37, 120, "/tadbir/reports", new Guid("10529157-c858-4432-8625-effb9cdef13a"), "ReportManagement" },
                    { 32, "Ctrl+Alt+L", "list", null, new DateTime(2023, 10, 16, 15, 56, 33, 0, DateTimeKind.Unspecified), 27, 108, "/admin/operation-log", new Guid("9edcc721-efec-4150-8e76-7d059cdcd8a1"), "OperationLogs" },
                    { 29, "Ctrl+Alt+H", "list", null, new DateTime(2023, 10, 16, 15, 56, 33, 0, DateTimeKind.Unspecified), 27, 89, "/admin/roles", new Guid("58c9161b-0be5-4146-bf9c-d6b27b5a1956"), "Roles" },
                    { 28, "Ctrl+Alt+K", "user", null, new DateTime(2023, 10, 16, 15, 56, 33, 0, DateTimeKind.Unspecified), 27, 82, "/admin/users", new Guid("7c1a6b18-5e93-4d12-ae2f-176a193c459a"), "Users" },
                    { 24, "Ctrl+Alt+C", "list", null, new DateTime(2023, 10, 16, 15, 56, 33, 0, DateTimeKind.Unspecified), 23, 78, "/organization/companies", new Guid("e6256e81-273b-43fd-9a5c-5fe4f32764b0"), "Companies" },
                    { 25, "Ctrl+Alt+E", "list", null, new DateTime(2023, 10, 16, 15, 56, 33, 0, DateTimeKind.Unspecified), 23, 70, "/organization/branches", new Guid("39f0b33e-d0f8-4e82-b16f-5cc984b7fef0"), "Branches" },
                    { 5, "Ctrl+Shift+D", "th", null, new DateTime(2023, 10, 16, 15, 56, 33, 0, DateTimeKind.Unspecified), 2, 8, "/finance/detailAccount", new Guid("4ddef6f2-1a40-4e32-a974-7f5d9d8bf99e"), "DetailAccount" },
                    { 21, "Ctrl+Alt+Z", "list", null, new DateTime(2023, 10, 16, 15, 56, 33, 0, DateTimeKind.Unspecified), 20, 141, "/finance/journal", new Guid("732c9112-4443-4c07-ba8b-1dd22f69eb8a"), "JournalLedger" },
                    { 8, "Ctrl+Shift+R", "transfer", null, new DateTime(2023, 10, 16, 15, 56, 33, 0, DateTimeKind.Unspecified), 2, 126, "/finance/accountrelations", new Guid("4832a7e8-9593-43e2-909c-0fe704aa926f"), "AccountRelations" },
                    { 59, "", "list", null, new DateTime(2023, 10, 16, 15, 56, 33, 0, DateTimeKind.Unspecified), 53, 225, "/treasury/cash-register", new Guid("7da4cbc4-9827-42d7-9898-1ff0e8b7fc4a"), "CashRegisters" },
                    { 100006, "", "list", null, new DateTime(2023, 10, 16, 15, 56, 33, 0, DateTimeKind.Unspecified), 100001, 100015, "/product-scope/properties", new Guid("c4b159d8-2d76-49a5-90e3-fc3c62c0885b"), "ProductProperty" },
                    { 100005, "", "list", null, new DateTime(2023, 10, 16, 15, 56, 33, 0, DateTimeKind.Unspecified), 100001, 100008, "/product-scope/units", new Guid("cbf2ea0c-5eed-411c-896c-f57a8c99d028"), "Units" },
                    { 100004, "", "list", null, new DateTime(2023, 10, 16, 15, 56, 33, 0, DateTimeKind.Unspecified), 100001, 100001, "/product-scope/brands", new Guid("9bd1b3c8-c1ee-49aa-8434-367a88cca7db"), "Brands" },
                    { 67, "", "list", null, new DateTime(2023, 10, 16, 15, 56, 33, 0, DateTimeKind.Unspecified), 63, 262, "/treasury/receipts/last", new Guid("8d56005b-8ee4-4f8c-afd1-651239780b53"), "LastReceiptForm" },
                    { 65, "", "plus", null, new DateTime(2023, 10, 16, 15, 56, 33, 0, DateTimeKind.Unspecified), 63, 259, "/treasury/receipts/new", new Guid("551f0290-1427-46f0-962f-d1302a4531ce"), "NewReceiptForm" },
                    { 69, "", "search", null, new DateTime(2023, 10, 16, 15, 56, 33, 0, DateTimeKind.Unspecified), 63, 257, "/treasury/receipts/by-no", new Guid("c2e75a30-41af-4dd6-b38a-35a41620710d"), "ReceiptFormbyNo" },
                    { 66, "", "list", null, new DateTime(2023, 10, 16, 15, 56, 33, 0, DateTimeKind.Unspecified), 62, 251, "/treasury/payments/last", new Guid("d75a840b-c136-4b22-9c27-bf5f9c4c4c29"), "LastPaymentForm" },
                    { 64, "", "plus", null, new DateTime(2023, 10, 16, 15, 56, 33, 0, DateTimeKind.Unspecified), 62, 248, "/treasury/payments/new", new Guid("f15f2a23-3444-4d79-98b3-71bdab1f32bf"), "NewPaymentForm" },
                    { 68, "", "search", null, new DateTime(2023, 10, 16, 15, 56, 33, 0, DateTimeKind.Unspecified), 62, 246, "/treasury/payments/by-no", new Guid("6a57790f-50b4-4cfa-b02a-77a70cab9536"), "PaymentFormbyNo" },
                    { 61, "", "list", null, new DateTime(2023, 10, 16, 15, 56, 33, 0, DateTimeKind.Unspecified), 53, 239, "/treasury/source-apps", new Guid("007a557b-b9a7-428f-afa9-f8e02c46e64d"), "SourceApps" },
                    { 60, "", "", null, new DateTime(2023, 10, 16, 15, 56, 33, 0, DateTimeKind.Unspecified), 55, 233, "/treasury/check-book-report", new Guid("04dc309f-9967-4764-b870-eb15579dd551"), "CheckBookReport" },
                    { 22, "Ctrl+Alt+B", "list", null, new DateTime(2023, 10, 16, 15, 56, 33, 0, DateTimeKind.Unspecified), 20, 147, "/finance/account-book", new Guid("dadee934-c6e2-4843-b456-7e0bdd3ae93d"), "AccountBook" },
                    { 100007, "", "list", null, new DateTime(2023, 10, 16, 15, 56, 33, 0, DateTimeKind.Unspecified), 100001, 100022, "/product-scope/attributes", new Guid("6db03c92-88e6-4830-b230-8ab1948770e1"), "ProductAttribute" }
                });

            migrationBuilder.InsertData(
                schema: "Metadata",
                table: "Command",
                columns: new[] { "CommandID", "HotKey", "IconName", "Index", "ModifiedDate", "ParentId", "PermissionId", "RouteUrl", "rowguid", "TitleKey" },
                values: new object[,]
                {
                    { 56, "", "", null, new DateTime(2023, 10, 16, 15, 56, 33, 0, DateTimeKind.Unspecified), 54, 215, "/treasury/check-books/new", new Guid("cf7ecfca-94bb-41e8-bc2c-630b346b015a"), "NewCheckBook" },
                    { 58, "", "", null, new DateTime(2023, 10, 16, 15, 56, 33, 0, DateTimeKind.Unspecified), 54, 212, "/treasury/check-books/by-name", new Guid("22fe6b4d-05a5-4818-9f9a-c80da61fcaef"), "CheckBookByName" },
                    { 51, "", "list", null, new DateTime(2023, 10, 16, 15, 56, 33, 0, DateTimeKind.Unspecified), 37, 210, "/tadbir/dashboard", new Guid("34ac446b-11c0-4668-8800-c11dd02e9c3d"), "ManageDashboard" },
                    { 50, "Ctrl+Shift+K", "list", null, new DateTime(2023, 10, 16, 15, 56, 33, 0, DateTimeKind.Unspecified), 39, 201, "/finance/bal-sheet", new Guid("22b72878-4051-4807-90e0-0d8e5badeae1"), "BalanceSheet" },
                    { 49, "Ctrl+Alt+Q", "list", null, new DateTime(2023, 10, 16, 15, 56, 33, 0, DateTimeKind.Unspecified), 11, 189, "/finance/vouchers/last/draft", new Guid("6844d73d-9fcb-41e3-9916-ecbe9bb44aaa"), "LastDraftVoucher" },
                    { 47, "Ctrl+Alt+V", "list", null, new DateTime(2023, 10, 16, 15, 56, 33, 0, DateTimeKind.Unspecified), 11, 180, "/finance/vouchers/new/draft", new Guid("35610066-98e3-45ca-a857-4886c1592746"), "NewDraftVoucher" },
                    { 48, "Ctrl+Alt+D", "list", null, new DateTime(2023, 10, 16, 15, 56, 33, 0, DateTimeKind.Unspecified), 11, 179, "/finance/vouchers/by-no/draft", new Guid("7bddb252-150d-4dd8-b5d3-82459f465f63"), "DraftVoucherByNo" },
                    { 46, "Ctrl+Alt+R", "list", null, new DateTime(2023, 10, 16, 15, 56, 33, 0, DateTimeKind.Unspecified), 39, 175, "/finance/profit-loss", new Guid("681e3a11-b30a-478b-92f8-0ff9029418bd"), "ProfitLoss" },
                    { 44, "Ctrl+Shift+B", "list", null, new DateTime(2023, 10, 16, 15, 56, 33, 0, DateTimeKind.Unspecified), 39, 169, "/finance/balance-by-account", new Guid("f1e7687c-c183-45d9-bab5-78a7cccb2ff0"), "BalanceByAccount" },
                    { 43, "Ctrl+Shift+I", "list", null, new DateTime(2023, 10, 16, 15, 56, 33, 0, DateTimeKind.Unspecified), 39, 164, "/finance/itembalance", new Guid("3c1987b5-3cb3-482d-bfa4-04d9ffc4e1b1"), "ItemBalance" },
                    { 41, "Ctrl+Alt+J", "list", null, new DateTime(2023, 10, 16, 15, 56, 33, 0, DateTimeKind.Unspecified), 20, 158, "/finance/currency-book", new Guid("5dc85a8b-c064-4ba0-bd7d-84d54f455753"), "CurrencyBook" },
                    { 40, "Ctrl+Alt+T", "list", null, new DateTime(2023, 10, 16, 15, 56, 33, 0, DateTimeKind.Unspecified), 39, 153, "/finance/balance", new Guid("8f375cc3-2711-4960-abd6-9acba078619a"), "TestBalance" },
                    { 57, "", "", null, new DateTime(2023, 10, 16, 15, 56, 33, 0, DateTimeKind.Unspecified), 54, 218, "/treasury/check-books/last", new Guid("dd74499d-37a4-4b82-9985-1953d1b13dfc"), "LastCheckBook" }
                });

            migrationBuilder.InsertData(
                schema: "Reporting",
                table: "LocalReport",
                columns: new[] { "LocalReportID", "Caption", "LocaleId", "ModifiedDate", "ReportId", "rowguid", "Template" },
                values: new object[,]
                {
                    { 100007, "گزارشات", 2, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 100003, new Guid("80fd1f24-c20e-428b-b432-fdba7ccb3aba"), "" },
                    { 100006, "Reports", 1, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 100003, new Guid("96bc141d-9cc0-492e-955e-773395e8e083"), "" },
                    { 100005, "اطلاعات عملیاتی", 2, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 100002, new Guid("106febcd-4d2e-4d1f-ae3b-dda4a22b4d77"), "" },
                    { 100004, "Operational Data", 1, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 100002, new Guid("3c8a411e-7398-4358-bb65-f3d4acc2b12b"), "" },
                    { 64, "Reports", 4, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 16, new Guid("7787828b-5276-4ab9-90cf-d8f29afccb75"), "" },
                    { 61, "Reports", 1, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 16, new Guid("7f30daac-6a1a-4427-8fea-07ee75b557ae"), "" },
                    { 60, "Operational data", 4, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 15, new Guid("313f7e67-ad71-4012-9f60-2c85b7a06c16"), "" },
                    { 59, "Operational data", 3, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 15, new Guid("58f79c37-0fec-407f-b2a5-095135666656"), "" },
                    { 58, "اطلاعات عملیاتی", 2, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 15, new Guid("56a471c7-3fb6-4c11-ae67-2df53e93e75b"), "" },
                    { 57, "Operational data", 1, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 15, new Guid("d34563e3-d852-436f-b8a2-893f2bdf6b37"), "" },
                    { 56, "Base data", 4, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 14, new Guid("fd64a7a7-bc36-4bdc-af39-57c650a33342"), "" },
                    { 55, "Base data", 3, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 14, new Guid("f471160c-b8c9-4f13-a643-64ebcab8fff6"), "" },
                    { 54, "اطلاعات پایه", 2, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 14, new Guid("5d5512e4-65cc-4835-8d38-4b497aefa9c1"), "" },
                    { 53, "Base data", 1, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 14, new Guid("0e6c7f31-afb5-4b19-a72d-b41d99554958"), "" },
                    { 62, "گزارشات", 2, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 16, new Guid("30d4af82-13ed-4697-8ec4-bb004bf4a0bd"), "" },
                    { 16, "Reports", 4, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 4, new Guid("1ce46326-811c-496b-9fbc-673be86c7e5b"), "" },
                    { 14, "گزارشات", 2, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 4, new Guid("990726b5-80c7-4944-8cb5-280ba1d3549b"), "" },
                    { 13, "Reports", 1, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 4, new Guid("9e2173da-cd31-4674-a3bf-fe24c5301b04"), "" },
                    { 12, "Operational data", 4, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 3, new Guid("4beb0eff-249b-403a-b5fd-2b3d2dc449ad"), "" },
                    { 11, "Operational data", 3, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 3, new Guid("0a529999-dcf3-45d8-a12a-663935b20f02"), "" },
                    { 10, "اطلاعات عملیاتی", 2, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 3, new Guid("53d59c5d-50a1-4689-895c-5d3aa80c9e4e"), "" },
                    { 9, "Operational data", 1, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 3, new Guid("103c729a-c6cb-4f5c-89c4-5057e67c2900"), "" },
                    { 8, "Base data", 4, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 2, new Guid("0f76faf0-469e-4794-945f-6dadc6cf8d6d"), "" },
                    { 7, "Base data", 3, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 2, new Guid("5f318056-3882-4ceb-8917-32b594f6c7e4"), "" },
                    { 6, "اطلاعات پایه", 2, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 2, new Guid("9b806374-28dc-4e11-ae45-8505870ce8d9"), "" },
                    { 15, "Reports", 3, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 4, new Guid("c5949140-00b1-4fb7-8e45-f2b3e1ad57c7"), "" },
                    { 63, "Reports", 3, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 16, new Guid("fd8d7b56-c274-493c-8841-9a669c41d784"), "" },
                    { 5, "Base data", 1, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 2, new Guid("f2e43965-9ee9-4bda-9f1a-4d90372017d1"), "" },
                    { 282, "اطلاعات عملیاتی", 2, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 95, new Guid("04cd6bd2-f5a5-4007-840d-1da1d7b310f4"), "" }
                });

            migrationBuilder.InsertData(
                schema: "Reporting",
                table: "LocalReport",
                columns: new[] { "LocalReportID", "Caption", "LocaleId", "ModifiedDate", "ReportId", "rowguid", "Template" },
                values: new object[,]
                {
                    { 281, "Operational Data", 1, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 95, new Guid("b062e766-7b4a-45b8-a228-a5aa5f78a3a3"), "" },
                    { 100002, "Base Data", 1, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 100001, new Guid("a5f3732c-e612-4a83-b7ca-087f575b69e9"), "" },
                    { 280, "اطلاعات پایه", 2, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 94, new Guid("7616f931-afcf-4cd1-908f-21b3e3077ec0"), "" },
                    { 279, "Base Data", 1, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 94, new Guid("f0fd4e22-d455-4a49-adb7-7ab2b2a499d3"), "" },
                    { 172, "Design template", 4, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 43, new Guid("1612ed5c-b62d-4906-928b-1889f28d3790"), "" },
                    { 100003, "اطلاعات پایه", 2, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 100001, new Guid("b08fb8d0-b26c-4e39-baac-2edd30361deb"), "" },
                    { 283, "Reports", 1, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 96, new Guid("aa492b5e-46b8-4735-9efd-0d7dcc762344"), "" },
                    { 171, "Design template", 3, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 43, new Guid("352a1b78-7567-4143-9000-32aba93f9088"), "" },
                    { 284, "گزارشات", 2, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 96, new Guid("3914340d-fca2-474a-a52a-5107cb0eb685"), "" },
                    { 169, "Design template", 1, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 43, new Guid("11cb2415-bbb4-4376-aaee-ca4842acdf70"), "" },
                    { 170, "طراحی قالب", 2, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 43, new Guid("ae604cfc-d4fe-4ce1-8dda-1da169ddca08"), "" }
                });

            migrationBuilder.InsertData(
                schema: "Reporting",
                table: "Report",
                columns: new[] { "ReportID", "Code", "CreatedById", "IsDefault", "IsDynamic", "IsGroup", "IsSystem", "ModifiedDate", "ParentId", "ResourceKeys", "ServiceUrl", "SubsystemId", "ViewId" },
                values: new object[,]
                {
                    { 100006, "ProductScope-Report-QReport", 1, false, false, true, true, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 100003, "", "", 100000, null },
                    { 100007, "", 1, true, true, false, true, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 100001, "", "brands", 100000, 100001 },
                    { 5, "Admin-Base-QReport", 1, false, false, true, true, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 2, "", "", 1, null },
                    { 100008, "", 1, true, true, false, true, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 100001, "", "units", 3, 100002 },
                    { 6, "Admin-Operation-QReport", 1, false, false, true, true, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 3, "", "", 1, null },
                    { 100005, "ProductScope-Operation-QReport", 1, false, false, true, true, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 100002, "", "", 100000, null },
                    { 100009, "", 1, true, true, false, true, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 100001, "", "", 3, 100003 },
                    { 100, "", 1, true, true, false, true, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 96, "", "check-books", 3, 69 },
                    { 99, "Treasury-Report-QReport", 1, false, false, true, true, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 96, "", "", 3, null },
                    { 100004, "ProductScope-Base-QReport", 1, false, false, true, true, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 100001, "", "", 100000, null },
                    { 89, "Voucher-Summary-By-Date", 1, false, false, false, true, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 16, "", "reports/finance/vouchers/sum-by-date", 2, 2 },
                    { 98, "Treasury-Operation-QReport", 1, false, false, true, true, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 95, "", "", 3, null },
                    { 90, "Voucher-Summary-By-No", 1, false, false, false, true, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 16, "", "reports/finance/vouchers/sum-by-no", 2, 2 },
                    { 17, "Accnt-Base-QReport", 1, false, false, true, true, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 14, "", "", 2, null },
                    { 97, "Treasury-Base-QReport", 1, false, false, true, true, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 94, "", "", 3, null },
                    { 18, "Accnt-Operation-QReport", 1, false, false, true, true, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 15, "", "", 2, null },
                    { 20, "Voucher-Printing", 1, false, false, true, true, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 15, "", "", 2, null },
                    { 100011, "", 1, true, true, false, true, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 100001, "", "Files", 3, 100005 },
                    { 19, "Accnt-Report-QReport", 1, false, false, true, true, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 16, "", "", 2, null },
                    { 85, "Journal-ByDate-ByLedger", 1, false, true, false, true, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 16, "", "reports/journal/by-date/by-ledger", 2, 17 },
                    { 86, "Journal-ByDate-BySubsidiary", 1, false, true, false, true, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 16, "", "reports/journal/by-date/by-subsid", 2, 18 },
                    { 87, "Journal-ByNo-ByLedger", 1, false, true, false, true, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 16, "", "reports/journal/by-no/by-ledger", 2, 24 },
                    { 88, "Journal-ByNo-BySubsidiary", 1, false, true, false, true, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 16, "", "reports/journal/by-no/by-subsid", 2, 25 },
                    { 7, "Admin-Report-QReport", 1, false, false, true, true, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 4, "", "", 1, null },
                    { 100010, "", 1, true, true, false, true, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 100001, "", "attributes", 3, 100004 }
                });

            migrationBuilder.InsertData(
                schema: "Reporting",
                table: "LocalReport",
                columns: new[] { "LocalReportID", "Caption", "LocaleId", "ModifiedDate", "ReportId", "rowguid", "Template" },
                values: new object[,]
                {
                    { 17, "Quick Report", 1, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 5, new Guid("cffb20f5-87c7-4001-8e12-60c22afbcc1e"), "" },
                    { 285, "Quick Report", 1, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 97, new Guid("906ac896-7267-46c5-b584-276097fab6b8"), "" },
                    { 272, "خلاصه اسناد حسابداری - بر اساس شماره سند", 2, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 90, new Guid("f9aacbb0-9ebe-44c0-b169-0b9115df9ed2"), "" },
                    { 271, "Accounting Voucher Summary - By Voucher No", 1, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 90, new Guid("886f6004-4edd-4692-822f-dda22fbb5001"), "" },
                    { 270, "خلاصه اسناد حسابداری - بر اساس تاریخ", 2, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 89, new Guid("e1d11e03-bbd7-4145-81af-ae94f304b1b5"), "" },
                    { 269, "Accounting Voucher Summary - By Date", 1, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 89, new Guid("26c1d993-4d8e-4f00-94f6-646ceef448fd"), "" },
                    { 268, "دفتر روزنامه در سطح معین - بر اساس شماره سند", 2, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 88, new Guid("521313e0-815a-41d5-b8ec-1d33811bdade"), "" },
                    { 267, "Journal in Subsidiary Level - By Number", 1, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 88, new Guid("5eff51fc-413e-4cff-b4ae-1e0540d56c8a"), "" },
                    { 266, "دفتر روزنامه در سطح کل - بر اساس شماره سند", 2, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 87, new Guid("2a5e71ff-40be-4ad2-979a-450baf62e4a1"), "" },
                    { 265, "Journal in Ledger Level - By Number", 1, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 87, new Guid("fe434c61-4838-4377-a25b-11a222aa7207"), "" },
                    { 264, "دفتر روزنامه در سطح معین - بر اساس تاریخ", 2, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 86, new Guid("b567053a-cdcc-49ae-979a-89c4b59622ac"), "" },
                    { 263, "Journal in Subsidiary Level - By Date", 1, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 86, new Guid("819f4d8e-7fcb-4f86-b9fa-c69edf58b568"), "" },
                    { 262, "دفتر روزنامه در سطح کل - بر اساس تاریخ", 2, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 85, new Guid("b2f9fade-de0a-4fa0-80f4-38d7d81c9c40"), "" },
                    { 261, "Journal in Ledger Level - By Date", 1, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 85, new Guid("e3f72bcc-e40b-457c-ae17-ae3bc88f75cb"), "" },
                    { 100012, "Quick Report", 1, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 100006, new Guid("d42857ab-4525-4717-9563-6d8e8f3c5827"), "" },
                    { 76, "Quick Report", 4, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 19, new Guid("a6844a3f-7a27-4000-b806-d459f5eccf89"), "" },
                    { 75, "Quick Report", 3, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 19, new Guid("6fdc2f55-b0be-4c93-a0df-c18d71e27b02"), "" },
                    { 74, "گزارش فوری", 2, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 19, new Guid("4dfa0541-3628-4b04-bfea-b1d30a43778f"), "" },
                    { 286, "گزارش فوری", 2, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 97, new Guid("faf660df-4133-404b-abd5-181b9bbef254"), "" },
                    { 73, "Quick Report", 1, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 19, new Guid("df1adaa6-9d31-4ace-8275-f768df83a589"), "" },
                    { 287, "Quick Report", 1, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 98, new Guid("91e96792-dc62-4f49-a5ee-87a56f96faa6"), "" },
                    { 289, "Quick Report", 1, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 99, new Guid("add701b3-5e62-4be5-b77c-af78474ebf07"), "" },
                    { 100011, "گزارش فوری", 2, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 100005, new Guid("316b86c5-1c8c-485d-9881-ccab159a6ec5"), "" },
                    { 100010, "Quick Report", 1, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 100005, new Guid("1be96c27-1b50-459a-97ae-6b30837f2f8d"), "" },
                    { 100023, "فهرست فایل ها", 2, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 100011, new Guid("2b798a51-33cb-4ffb-ae56-ef5325b02126"), "" },
                    { 100022, "File List", 1, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 100011, new Guid("70f05757-7a7f-4d9a-a0b8-e089e0a7b26f"), "" },
                    { 100021, "لیست خصوصیت ها", 2, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 100010, new Guid("54cd6b72-d172-4579-a5bd-f691e7badaeb"), "" },
                    { 100020, "Attribute List", 1, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 100010, new Guid("7830eb6b-bde0-4908-9638-2456c17c0208"), "" },
                    { 100019, "لیست ویژگی ها", 2, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 100009, new Guid("78ca3e19-16ed-4b4e-ba36-596a5f064e6d"), "" },
                    { 100018, "Properties List", 1, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 100009, new Guid("5ed13983-a4d1-441e-a8ec-acbef9d13c93"), "" },
                    { 100017, "لیست واحدها", 2, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 100008, new Guid("e62337de-55c3-49f6-bee1-337bafbe50ae"), "" },
                    { 100016, "Unit list", 1, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 100008, new Guid("cc17cd2c-5369-4071-9863-858b423ea667"), "" },
                    { 100015, "فهرست برندها", 2, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 100007, new Guid("80d4aa50-96dc-4bd4-a210-d678688409d9"), "" },
                    { 100014, "Brnads List", 1, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 100007, new Guid("7f51552c-2ce5-441e-a6b0-de0dc43ec8f0"), "" },
                    { 100009, "گزارش فوری", 2, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 100004, new Guid("cd24218d-72a9-446f-af44-6b0de5a86181"), "" },
                    { 100008, "Quick Report", 1, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 100004, new Guid("10e8003e-a8ac-4434-be19-6f0402509cc0"), "" },
                    { 292, "دسته چک", 2, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 100, new Guid("f5e62020-b90b-41fe-9c1d-774a417ea787"), "" },
                    { 291, "Check Book", 1, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 100, new Guid("3df44c0d-f3d4-4b4a-9135-60366d6460c4"), "" },
                    { 290, "گزارش فوری", 2, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 99, new Guid("d39c36f5-6b31-42a8-9eb7-8d637ec93ba7"), "" },
                    { 288, "گزارش فوری", 2, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 98, new Guid("15c0a334-0d77-46bc-b206-a4db1928d09c"), "" },
                    { 80, "Voucher Printing", 4, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 20, new Guid("5b6090e7-d40c-4ca4-99b4-ff56eabc4672"), "" },
                    { 100013, "گزارش فوری", 2, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 100006, new Guid("2f5700de-b3da-4a36-86cb-5dc23f44e709"), "" }
                });

            migrationBuilder.InsertData(
                schema: "Reporting",
                table: "LocalReport",
                columns: new[] { "LocalReportID", "Caption", "LocaleId", "ModifiedDate", "ReportId", "rowguid", "Template" },
                values: new object[,]
                {
                    { 72, "Quick Report", 4, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 18, new Guid("93fd0476-2a33-4da4-9193-774b41738ac9"), "" },
                    { 25, "Quick Report", 1, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 7, new Guid("390617a4-f970-4443-a934-651f16ff32f8"), "" },
                    { 71, "Quick Report", 3, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 18, new Guid("46a6f5d8-8d70-4ceb-adac-fa12d9e83c04"), "" },
                    { 67, "Quick Report", 3, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 17, new Guid("44e7770e-6838-4d0f-be96-f89ee19943d0"), "" },
                    { 28, "Quick Report", 4, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 7, new Guid("c69c7663-5539-4b81-8bae-15c1e7f49c17"), "" },
                    { 65, "Quick Report", 1, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 17, new Guid("082e12f1-4ad9-42be-be93-0162dc42cb28"), "" },
                    { 24, "Quick Report", 4, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 6, new Guid("7ab4896f-9f2d-4423-9967-57cea0528597"), "" },
                    { 23, "Quick Report", 3, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 6, new Guid("9af9d2bc-ea8f-490c-a526-0807265dfd5c"), "" },
                    { 22, "گزارش فوری", 2, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 6, new Guid("e7d687c5-a3e6-40f3-88f6-414c8fd40bf0"), "" },
                    { 27, "Quick Report", 3, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 7, new Guid("32e6096e-967c-4efb-b56a-d3879f69c9cf"), "" },
                    { 21, "Quick Report", 1, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 6, new Guid("e7d9c840-2f14-4485-b9ee-46ecd92a042f"), "" },
                    { 77, "Voucher Printing", 1, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 20, new Guid("75d71469-2f57-4f7a-bbd8-438f416fb3ce"), "" },
                    { 78, "چاپ سند", 2, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 20, new Guid("3228e245-5960-40f4-88c2-422f1b769a1e"), "" },
                    { 69, "Quick Report", 1, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 18, new Guid("e4625144-7521-4b8a-ba26-de6f1f2ba87c"), "" },
                    { 70, "گزارش فوری", 2, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 18, new Guid("7d83d2b7-8139-4d4e-a91e-95ddfd958cce"), "" },
                    { 66, "گزارش فوری", 2, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 17, new Guid("d2970f20-d35c-4006-acb6-d770effa75f4"), "" },
                    { 79, "Voucher Printing", 3, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 20, new Guid("a2cffd5c-19c8-402c-9f60-12143ab247ee"), "" },
                    { 20, "Quick Report", 4, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 5, new Guid("e9144e19-fe27-43e3-b1ac-309365d37eb4"), "" },
                    { 19, "Quick Report", 3, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 5, new Guid("c94f72ae-0378-47d9-bd65-89e240b2fc4a"), "" },
                    { 18, "گزارش فوری", 2, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 5, new Guid("28125b11-4dae-4de3-9a49-8fb58b4a39b8"), "" },
                    { 68, "Quick Report", 4, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 17, new Guid("b5bbe349-1b83-4835-b1e5-51a956f94f7e"), "" },
                    { 26, "گزارش فوری", 2, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 7, new Guid("d413d97c-d04d-49d5-bb99-ad55c1b2b6b2"), "" }
                });

            migrationBuilder.InsertData(
                schema: "Reporting",
                table: "Report",
                columns: new[] { "ReportID", "Code", "CreatedById", "IsDefault", "IsDynamic", "IsGroup", "IsSystem", "ModifiedDate", "ParentId", "ResourceKeys", "ServiceUrl", "SubsystemId", "ViewId" },
                values: new object[,]
                {
                    { 40, "Voucher-Std-Form", 1, true, false, false, true, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 20, "", "reports/finance/voucher-by-no/{0}/std-form", 2, 2 },
                    { 21, "Fiscal-Periods", 1, true, true, false, true, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 17, "", "fperiods", 2, 9 },
                    { 74, "", 1, true, true, false, true, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 17, "", "currencies", 2, 30 },
                    { 23, "Detail-Accounts", 1, true, true, false, true, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 17, "", "faccounts", 2, 6 },
                    { 24, "Cost-Centers", 1, true, true, false, true, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 17, "", "ccenters", 2, 7 },
                    { 25, "Projects", 1, true, true, false, true, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 17, "", "projects", 2, 8 },
                    { 26, "Account-Groups", 1, true, true, false, true, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 17, "", "accgroups", 2, 12 },
                    { 80, "BalanceSheet", 1, true, true, false, true, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 19, "", "bal-sheet", 2, 67 },
                    { 22, "Accounts", 1, true, true, false, true, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 17, "", "accounts", 2, 1 },
                    { 92, "", 1, false, true, false, true, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 7, "", "dashboard/widgets/all", 1, 68 },
                    { 106, "", 1, true, true, false, true, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 98, "", "payments/{0}/cash/articles", 3, 74 },
                    { 103, "", 1, true, true, false, true, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 97, "", "source-apps", 3, 73 },
                    { 8, "Companies", 1, true, true, false, true, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 5, "", "companies", 1, 11 },
                    { 9, "Branches", 1, true, true, false, true, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 5, "", "branches", 1, 10 },
                    { 10, "Users", 1, true, true, false, true, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 5, "", "users", 1, 4 },
                    { 11, "Roles", 1, true, true, false, true, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 5, "", "roles", 1, 5 },
                    { 70, "", 1, true, true, false, true, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 6, "", "oplog/archive", 1, 61 },
                    { 71, "", 1, true, true, false, true, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 6, "", "sys-oplog", 1, 59 },
                    { 101, "", 1, true, true, false, true, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 97, "", "cash-registers", 3, 70 },
                    { 72, "", 1, true, true, false, true, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 6, "", "sys-oplog/archive", 1, 60 }
                });

            migrationBuilder.InsertData(
                schema: "Reporting",
                table: "Report",
                columns: new[] { "ReportID", "Code", "CreatedById", "IsDefault", "IsDynamic", "IsGroup", "IsSystem", "ModifiedDate", "ParentId", "ResourceKeys", "ServiceUrl", "SubsystemId", "ViewId" },
                values: new object[,]
                {
                    { 102, "", 1, true, true, false, true, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 99, "", "check-book-report", 3, 72 },
                    { 107, "", 1, true, true, false, true, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 98, "", "receipts/{0}/cash/articles", 3, 75 },
                    { 79, "", 1, true, true, false, true, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 19, "", "", 2, 66 },
                    { 105, "", 1, true, true, false, true, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 98, "", "receipts/{0}/payer/articles", 3, 75 },
                    { 104, "", 1, true, true, false, true, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 98, "", "payments/{0}/receiver/articles", 3, 74 },
                    { 91, "", 1, false, true, false, true, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 7, "", "dashboard/widgets", 1, 68 },
                    { 73, "", 1, true, true, false, true, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 6, "", "oplog", 1, 13 },
                    { 78, "", 1, true, true, false, true, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 19, "", "", 2, 65 },
                    { 69, "", 1, true, true, false, true, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 19, "", "", 2, 58 },
                    { 76, "", 1, true, true, false, true, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 19, "", "", 2, 62 },
                    { 37, "Journal-ByNo-BySubsidiary", 1, true, true, false, true, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 19, "", "reports/journal/by-no/by-subsid", 2, 25 },
                    { 36, "Journal-ByNo-ByLedger", 1, true, true, false, true, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 19, "", "reports/journal/by-no/by-ledger", 2, 24 },
                    { 35, "Journal-ByNo-ByRow-Detail", 1, true, true, false, true, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 19, "", "reports/journal/by-no/by-row-detail", 2, 23 },
                    { 34, "Journal-ByNo-ByRow", 1, true, true, false, true, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 19, "", "reports/journal/by-no/by-row", 2, 22 },
                    { 33, "Journal-ByDate-LedgerSummary-ByMonth", 1, true, true, false, true, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 19, "", "reports/journal/by-date/sum-by-month", 2, 21 },
                    { 32, "Journal-ByDate-LedgerSummary-ByDate", 1, true, true, false, true, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 19, "", "reports/journal/by-date/sum-by-date", 2, 20 },
                    { 31, "Journal-ByDate-LedgerSummary", 1, true, true, false, true, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 19, "", "reports/journal/by-date/summary", 2, 19 },
                    { 30, "Journal-ByDate-BySubsidiary", 1, true, true, false, true, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 19, "", "reports/journal/by-date/by-subsid", 2, 18 },
                    { 29, "Journal-ByDate-ByLedger", 1, true, true, false, true, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 19, "", "reports/journal/by-date/by-ledger", 2, 17 },
                    { 28, "Journal-ByDate-ByRow-Detail", 1, true, true, false, true, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 19, "", "reports/journal/by-date/by-row-detail", 2, 16 },
                    { 27, "Journal-ByDate-ByRow", 1, true, true, false, true, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 19, "", "reports/journal/by-date/by-row", 2, 15 },
                    { 75, "", 1, true, true, false, true, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 18, "", "", 2, 31 },
                    { 109, "", 1, true, true, false, true, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 20, "", "", 2, 41 },
                    { 108, "", 1, true, true, false, true, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 20, "", "", 2, 42 },
                    { 84, "", 1, false, false, false, true, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 20, "", "reports/finance/voucher-by-no/{0}/by-subsid", 2, 2 },
                    { 83, "", 1, false, false, false, true, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 20, "", "reports/finance/voucher-by-no/{0}/by-ledger", 2, 2 },
                    { 82, "", 1, false, false, false, true, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 20, "", "reports/finance/voucher-by-no/{0}/by-detail", 2, 2 },
                    { 81, "Vouchers", 1, true, true, false, true, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 20, "", "", 2, 2 },
                    { 41, "Voucher-Std-Form-Detail", 1, false, false, false, true, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 20, "", "reports/finance/voucher-by-no/{0}/std-form-detail", 2, 2 },
                    { 38, "Journal-ByNo-LedgerSummary", 1, true, true, false, true, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 19, "", "reports/journal/by-no/summary", 2, 26 },
                    { 77, "", 1, true, true, false, true, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 19, "", "", 2, 64 },
                    { 46, "TestBalance2Column", 1, true, true, false, true, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 19, "", "", 2, 32 },
                    { 49, "TestBalance8Column", 1, true, true, false, true, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 19, "", "", 2, 35 },
                    { 68, "", 1, true, true, false, true, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 19, "", "", 2, 40 },
                    { 67, "", 1, true, true, false, true, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 19, "", "", 2, 39 },
                    { 66, "", 1, true, true, false, true, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 19, "", "", 2, 38 },
                    { 65, "", 1, true, true, false, true, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 19, "", "", 2, 37 },
                    { 64, "", 1, true, true, false, true, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 19, "", "", 2, 29 },
                    { 63, "", 1, true, true, false, true, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 19, "", "", 2, 28 },
                    { 62, "", 1, true, true, false, true, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 19, "", "", 2, 27 },
                    { 61, "", 1, true, true, false, true, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 19, "", "", 2, 56 },
                    { 60, "", 1, true, true, false, true, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 19, "", "", 2, 55 }
                });

            migrationBuilder.InsertData(
                schema: "Reporting",
                table: "Report",
                columns: new[] { "ReportID", "Code", "CreatedById", "IsDefault", "IsDynamic", "IsGroup", "IsSystem", "ModifiedDate", "ParentId", "ResourceKeys", "ServiceUrl", "SubsystemId", "ViewId" },
                values: new object[,]
                {
                    { 59, "", 1, true, true, false, true, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 19, "", "", 2, 54 },
                    { 58, "", 1, true, true, false, true, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 19, "", "", 2, 53 },
                    { 57, "", 1, true, true, false, true, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 19, "", "", 2, 51 },
                    { 56, "", 1, true, true, false, true, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 19, "", "", 2, 50 },
                    { 55, "", 1, true, true, false, true, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 19, "", "", 2, 49 },
                    { 54, "", 1, true, true, false, true, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 19, "", "", 2, 48 },
                    { 53, "", 1, true, true, false, true, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 19, "", "", 2, 46 },
                    { 52, "", 1, true, true, false, true, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 19, "", "", 2, 45 },
                    { 51, "", 1, true, true, false, true, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 19, "", "", 2, 44 },
                    { 50, "", 1, true, true, false, true, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 19, "", "", 2, 43 },
                    { 47, "TestBalance4Column", 1, true, true, false, true, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 19, "", "", 2, 33 },
                    { 48, "TestBalance6Column", 1, true, true, false, true, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 19, "", "", 2, 34 }
                });

            migrationBuilder.InsertData(
                schema: "Reporting",
                table: "LocalReport",
                columns: new[] { "LocalReportID", "Caption", "LocaleId", "ModifiedDate", "ReportId", "rowguid", "Template" },
                values: new object[,]
                {
                    { 29, "Companies", 1, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 8, new Guid("46da822d-bfea-4bc0-9011-42ad8cf7aef3"), "" },
                    { 176, "Test balance 2 columns", 4, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 46, new Guid("fc231e36-76b5-482a-a4ce-31a38495dd77"), "" },
                    { 177, "Test balance 4 columns", 1, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 47, new Guid("4a0ac42e-faf9-4c68-afa6-5d8a3fc7510d"), "" },
                    { 178, "تراز آزمایشی ۴ ستونی", 2, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 47, new Guid("496dba2c-e58e-4f11-899c-becba6c428a9"), "" },
                    { 179, "Test balance 4 columns", 3, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 47, new Guid("7115864e-454a-44f4-b526-b614d14afb5c"), "" },
                    { 180, "Test balance 4 columns", 4, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 47, new Guid("4b314598-e3bc-4dee-a6cf-23000900ad33"), "" },
                    { 181, "Test balance 6 columns", 1, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 48, new Guid("9eaee9f9-b226-4066-8dd3-347524fd1b9e"), "" },
                    { 182, "تراز آزمایشی ۶ ستونی", 2, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 48, new Guid("dd1041ca-b6a5-436c-89f1-3fa332328fbc"), "" },
                    { 183, "Test balance 6 columns", 3, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 48, new Guid("7e32f5fa-abf3-4c14-9759-5d0fe8bc3218"), "" },
                    { 184, "Test balance 6 columns", 4, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 48, new Guid("0e43b7a5-65c1-403a-bddf-80b87e233b5e"), "" },
                    { 185, "Test balance 8 columns", 1, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 49, new Guid("1a077e01-a6ee-4319-b02c-efcc938f54e6"), "" },
                    { 186, "تراز آزمایشی ۸ ستونی", 2, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 49, new Guid("6e72f05f-f0d5-45a7-a685-a8625da970f4"), "" },
                    { 187, "Test balance 8 columns", 3, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 49, new Guid("89dec43f-76d1-445a-8870-f9ef56c6a9ae"), "" },
                    { 188, "Test balance 8 columns", 4, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 49, new Guid("c70064c0-354d-4e88-a35c-e7d2b4f0d0f3"), "" },
                    { 189, "Detail account turnover/balance - 2 column", 1, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 50, new Guid("e7bcf5af-ccab-47d1-ae3b-9e0c70f520a4"), "" },
                    { 190, "گردش و مانده تفصیلی شناور 2 ستونی", 2, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 50, new Guid("4d02541d-a18c-419d-af03-7fcc01530f06"), "" },
                    { 191, "Detail account turnover/balance - 4 column", 1, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 51, new Guid("b2c53c63-7df6-4506-869a-5b0dd8890c79"), "" },
                    { 192, "گردش و مانده تفصیلی شناور 4 ستونی", 2, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 51, new Guid("8be1a65f-b280-4142-b11a-2124ee823610"), "" },
                    { 193, "Detail account turnover/balance - 6 column", 1, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 52, new Guid("859309a5-c70a-4d4f-ae8b-9b3abdd7dd56"), "" },
                    { 194, "گردش و مانده تفصیلی شناور 6 ستونی", 2, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 52, new Guid("2c29800c-d3a4-4282-92e5-a44e2d6a4949"), "" },
                    { 195, "Detail account turnover/balance - 8 column", 1, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 53, new Guid("8dc95a96-0753-419d-9d09-051210475a5c"), "" },
                    { 196, "گردش و مانده تفصیلی شناور 8 ستونی", 2, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 53, new Guid("4d74c7c1-0a0a-4b92-91e2-8ffb8d935406"), "" },
                    { 175, "Test balance 2 columns", 3, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 46, new Guid("3da7d123-38f3-4ce1-a782-8e6ece05ade7"), "" },
                    { 197, "Cost center turnover/balance - 2 column", 1, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 54, new Guid("065eae7a-5dd7-4a43-805a-a25c6000ee97"), "" },
                    { 174, "تراز آزمایشی ۲ ستونی", 2, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 46, new Guid("895d3bb8-9e86-454e-9c96-b408e727e502"), "" },
                    { 152, "Journal, by number, ledger summary", 4, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 38, new Guid("f4d4e2a7-8144-40e4-ade0-b8f53c1f7b58"), "" },
                    { 131, "Journal, by date, summary by month", 3, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 33, new Guid("ecd1f4cb-23e2-4604-b411-1ab2036ba3ea"), "" },
                    { 132, "Journal, by date, summary by month", 4, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 33, new Guid("ce93ca35-6ad0-4733-b817-d943cad7b30b"), "" },
                    { 133, "Journal, by number, by row", 1, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 34, new Guid("49c02f84-8078-4061-9174-bb97aa37ec7b"), "" },
                    { 134, "دفتر روزنامه، بر حسب شماره سند، مطابق ردیف های سند", 2, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 34, new Guid("59445ba5-a679-4b70-84d7-cd595883097c"), "" },
                    { 135, "Journal, by number, by row", 3, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 34, new Guid("3098672f-d078-4142-bef6-a7db26a985de"), "" },
                    { 136, "Journal, by number, by row", 4, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 34, new Guid("5085efbb-7ff4-4346-85ea-fc0ddadd24af"), "" },
                    { 137, "Journal, by number, by row with details", 1, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 35, new Guid("ffd2cef9-53ad-4b64-a196-5abff7bd4c82"), "" },
                    { 138, "دفتر روزنامه، بر حسب شماره سند، مطابق ردیف های سند با سطوح شناور", 2, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 35, new Guid("ef282f6d-d55b-44d6-ae16-4641429e0ad5"), "" },
                    { 139, "Journal, by number, by row with details", 3, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 35, new Guid("dbfa2982-48ee-4179-ba12-14a360865484"), "" },
                    { 140, "Journal, by number, by row with details", 4, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 35, new Guid("e77d5f74-d468-4f26-9bf4-0cfa841f3bda"), "" },
                    { 141, "Journal, by number, by ledger", 1, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 36, new Guid("51cb152e-858d-4a5c-b1f3-1f66bc718ffa"), "" },
                    { 142, "دفتر روزنامه، بر حسب شماره سند، در سطح کل", 2, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 36, new Guid("58e7bd2b-70b5-40f1-855d-2811b110d389"), "" },
                    { 143, "Journal, by number, by ledger", 3, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 36, new Guid("31cdc4f7-e033-4236-bda6-1969bb27c85d"), "" },
                    { 144, "Journal, by number, by ledger", 4, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 36, new Guid("5289c6e2-14cd-44fa-aeea-643ece0ea7bf"), "" },
                    { 145, "Journal, by number, by subsidiary", 1, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 37, new Guid("dcd3018b-4dac-4ed5-ab79-b35bfdcc4caf"), "" },
                    { 146, "دفتر روزنامه، بر حسب شماره سند، در سطح معین", 2, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 37, new Guid("b579d0db-680c-48d9-b0a4-06f586dcdd0d"), "" }
                });

            migrationBuilder.InsertData(
                schema: "Reporting",
                table: "LocalReport",
                columns: new[] { "LocalReportID", "Caption", "LocaleId", "ModifiedDate", "ReportId", "rowguid", "Template" },
                values: new object[,]
                {
                    { 147, "Journal, by number, by subsidiary", 3, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 37, new Guid("db927678-917c-4a58-85eb-8737e7bdb3e9"), "" },
                    { 148, "Journal, by number, by subsidiary", 4, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 37, new Guid("5b4877af-d5d6-4c00-b9bf-2f782b35f5c4"), "" },
                    { 149, "Journal, by number, ledger summary", 1, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 38, new Guid("999e9e03-6dfb-4d23-adbb-c12ac04cf6b0"), "" },
                    { 150, "دفتر روزنامه، بر حسب شماره سند، سند خلاصه", 2, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 38, new Guid("566086aa-667e-4821-858b-211c15dfd048"), "" },
                    { 151, "Journal, by number, ledger summary", 3, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 38, new Guid("a12f7964-36f8-432e-8c42-301a3ea9ca0f"), "" },
                    { 173, "Test balance 2 columns", 1, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 46, new Guid("55378a66-d74f-4723-bc92-ed858552fb6b"), "" },
                    { 130, "دفتر روزنامه، بر حسب تاریخ، سند خلاصه به تفکیک ماه", 2, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 33, new Guid("093100cd-faeb-4905-9304-7b63bb598b34"), "" },
                    { 198, "گردش و مانده مرکز هزینه 2 ستونی", 2, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 54, new Guid("e1451af8-bf12-4422-9220-542900a4d728"), "" },
                    { 200, "گردش و مانده مرکز هزینه 4 ستونی", 2, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 55, new Guid("2bbaa2e0-bff5-47ba-972a-17f2ae579be3"), "" },
                    { 226, "دفتر عملیات ارزی", 2, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 68, new Guid("83919371-d0f9-42fe-ad80-8298be57d12d"), "" },
                    { 227, "Balance by account", 1, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 69, new Guid("9b79ef57-1b55-4426-ab73-880621b85e18"), "" },
                    { 228, "مانده به تفکیک حساب", 2, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 69, new Guid("dc5ef37f-aceb-42eb-8861-2ba48567e888"), "" },
                    { 241, "Profit-Loss", 1, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 76, new Guid("69983116-c065-485d-9e2a-c4da3dc0056c"), "" },
                    { 242, "سود و زیان", 2, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 76, new Guid("850ff1fb-48a7-4746-a9a1-6a9d9f1090b2"), "" },
                    { 243, "Profit-Loss", 1, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 77, new Guid("0bc08f4b-2ccf-4f2e-a49b-48dbd4b17198"), "" },
                    { 244, "سود و زیان", 2, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 77, new Guid("0441788d-a19a-4e92-b4a8-78ecbf1844d8"), "" },
                    { 249, "BalanceSheet", 1, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 80, new Guid("4a87e900-9438-4ce3-9d08-c0d025cba9f8"), "" },
                    { 250, "ترازنامه", 2, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 80, new Guid("34181ea6-7b4c-40be-acad-7a9d45257138"), "" },
                    { 293, "Cash Register List", 1, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 101, new Guid("1482f50c-27b5-4cd5-bbb4-7e246305fd3e"), "" },
                    { 294, "فهرست صندوق‌های اسناد", 2, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 101, new Guid("01eaf9f2-ea4f-467d-93fe-0472a7e31bdb"), "" },
                    { 297, "Source and Application List", 1, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 103, new Guid("2c263415-0324-4860-b679-4fc481234853"), "" },
                    { 298, "فهرست منابع و مصارف", 2, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 103, new Guid("18957918-c0ea-4081-8e64-80c91e187f2b"), "" },
                    { 299, "Recipients List", 1, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 104, new Guid("bc372659-9638-45ab-880f-d3818fa96999"), "" },
                    { 300, "لیست دریافت کنندگان", 2, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 104, new Guid("152d9ec4-8259-4c81-ac42-645ca4b59faf"), "" },
                    { 301, "Payers List", 1, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 105, new Guid("81c4f83e-4a60-403f-a49f-5da9ba84bce8"), "" },
                    { 302, "لیست پرداخت کنندگان", 2, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 105, new Guid("50e78489-ae72-44e5-b8e7-cab78062fd88"), "" },
                    { 303, "Cash Accounts List", 1, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 106, new Guid("1e86bf75-fc1f-461f-8d1f-c5eb03169098"), "" },
                    { 304, "لیست حساب‌های نقدی", 2, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 106, new Guid("cc54682b-2dc3-4dc7-aecb-e0a2957d4e8f"), "" },
                    { 305, "Cash Accounts List", 1, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 107, new Guid("1498a1a3-a5a5-44a0-a81b-e22577ab56a0"), "" },
                    { 306, "لیست حساب‌های نقدی", 2, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 107, new Guid("90b0ac12-f690-4c42-87a7-b8c964c87562"), "" },
                    { 225, "Currency Book", 1, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 68, new Guid("3135c186-44fa-459b-ab27-273afb75b686"), "" },
                    { 199, "Cost center turnover/balance - 4 column", 1, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 55, new Guid("53f10688-c2f5-4719-bde3-1c14c430e504"), "" },
                    { 224, "دفتر عملیات ارزی", 2, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 67, new Guid("b38f0d13-6b39-4a87-bd71-0cf15792967c"), "" },
                    { 222, "دفتر عملیات ارزی", 2, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 66, new Guid("868d6d1a-2f9e-4d70-94f6-9268133f271c"), "" },
                    { 201, "Cost center turnover/balance - 6 column", 1, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 56, new Guid("f46c0d05-eb52-41df-adf0-039fc31914a9"), "" },
                    { 202, "گردش و مانده مرکز هزینه 6 ستونی", 2, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 56, new Guid("b6448122-28d9-4f58-9a5b-3bb46e63b979"), "" },
                    { 203, "Cost center turnover/balance - 8 column", 1, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 57, new Guid("4a1038ee-efa4-4f15-8730-d8ef2a83951e"), "" },
                    { 204, "گردش و مانده مرکز هزینه 8 ستونی", 2, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 57, new Guid("94c65a8a-478c-4285-ac62-bf4c3dfa2356"), "" },
                    { 205, "Project turnover/balance - 2 column", 1, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 58, new Guid("a937cbb1-f026-4f4f-973b-51d71bf23835"), "" },
                    { 206, "گردش و مانده پروژه 2 ستونی", 2, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 58, new Guid("d705d0f5-a81f-43a7-bf24-eba2aa0d0c7b"), "" },
                    { 207, "Project turnover/balance - 4 column", 1, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 59, new Guid("53e7a579-9a99-4bac-8df8-ba365c73b78e"), "" },
                    { 208, "گردش و مانده پروژه 4 ستونی", 2, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 59, new Guid("dc8fbc21-f173-4fb6-9e41-dea5e7decddd"), "" }
                });

            migrationBuilder.InsertData(
                schema: "Reporting",
                table: "LocalReport",
                columns: new[] { "LocalReportID", "Caption", "LocaleId", "ModifiedDate", "ReportId", "rowguid", "Template" },
                values: new object[,]
                {
                    { 209, "Project turnover/balance - 6 column", 1, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 60, new Guid("6c2e649f-8883-4da2-834f-aa987836cac2"), "" },
                    { 210, "گردش و مانده پروژه 6 ستونی", 2, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 60, new Guid("d8cb0640-353d-4f43-b2f6-14e37c7d9935"), "" },
                    { 211, "Project turnover/balance - 8 column", 1, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 61, new Guid("ea50f233-38ef-4e73-816c-dc738e48a4a6"), "" },
                    { 212, "گردش و مانده پروژه 8 ستونی", 2, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 61, new Guid("ee4dcbda-e512-4fe2-a6b7-6c649cce30c1"), "" },
                    { 213, "Account Book", 1, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 62, new Guid("00f0bc4c-917e-461d-9bc9-168f9dfd1304"), "" },
                    { 214, "دفتر حساب", 2, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 62, new Guid("1886a834-fcfe-46d9-8a41-9438f32545b5"), "" },
                    { 215, "Account Book", 1, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 63, new Guid("73e01b39-6946-4503-9428-b6854b626760"), "" },
                    { 216, "دفتر حساب", 2, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 63, new Guid("7945ca3b-db77-4505-adae-19fbe8687dc9"), "" },
                    { 217, "Account Book", 1, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 64, new Guid("f1c38aee-c3eb-466b-86ca-dac44b7d402a"), "" },
                    { 218, "ذفتر حساب", 2, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 64, new Guid("c30c7e6b-dc43-4b47-a9e2-5a2bc98d29fb"), "" },
                    { 219, "Currency Book", 1, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 65, new Guid("0acfbdcf-15ce-4025-b912-97e912b0652f"), "" },
                    { 220, "دفتر عملیات ارزی", 2, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 65, new Guid("55c2b9f1-79e7-4235-ac8f-3e319b8f67c0"), "" },
                    { 221, "Currency Book", 1, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 66, new Guid("b6e75213-a806-4000-b42c-74ff7a676753"), "" },
                    { 223, "Currency Book", 1, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 67, new Guid("420129a4-7077-4621-af3a-eb6e0a95002e"), "" },
                    { 129, "Journal, by date, summary by month", 1, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 33, new Guid("beabb2e9-60ef-4355-9188-23c206abca63"), "" },
                    { 128, "Journal, by date, summary by date", 4, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 32, new Guid("ba2787b4-77c8-4c38-b9bf-ffbd9cc57122"), "" },
                    { 127, "Journal, by date, summary by date", 3, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 32, new Guid("40bfffcb-8d8f-4c82-b00f-a14f2e6c509a"), "" },
                    { 275, "All Widgets", 1, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 92, new Guid("8ede66c9-3fd7-4891-b6c1-dfc7b140d85a"), "" },
                    { 276, "همه ویجت ها", 2, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 92, new Guid("d50dc95e-8252-4906-ac51-61a6925565cc"), "" },
                    { 81, "Fiscal periods", 1, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 21, new Guid("9bb5681a-23f9-48e5-981f-76bf1e5d30b9"), "" },
                    { 82, "دوره های مالی", 2, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 21, new Guid("5051db38-8ae1-47e9-8103-c8376bc58b40"), "" },
                    { 83, "Fiscal periods", 3, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 21, new Guid("33e85e84-4197-4aa5-8eee-985555fa87d1"), "" },
                    { 84, "Fiscal periods", 4, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 21, new Guid("3739c30e-2964-4183-82c6-142c9c0461b1"), "" },
                    { 85, "Accounts", 1, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 22, new Guid("19f9ae4c-955a-48b1-879e-9bb6a9886307"), "" },
                    { 86, "سرفصل های حسابداری", 2, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 22, new Guid("79b2a43e-9a48-4c9c-a158-b66b0f0804f3"), "" },
                    { 87, "Accounts", 3, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 22, new Guid("8a7fd943-ef0e-4379-a8ef-3c6d071c0264"), "" },
                    { 88, "Accounts", 4, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 22, new Guid("f6b5fab8-1e9c-4b1e-850f-9cf6921e2983"), "" },
                    { 89, "Detail accounts", 1, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 23, new Guid("d73934c4-4c0f-48ac-b373-352cb9680e26"), "" },
                    { 90, "تفصیلی های شناور", 2, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 23, new Guid("9c30ee48-c23d-44b4-bb10-f15642783d4b"), "" },
                    { 91, "Detail accounts", 3, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 23, new Guid("1babd472-5bf0-4d28-bef0-6ecaabd89cae"), "" },
                    { 92, "Detail accounts", 4, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 23, new Guid("ee496ac4-e8d9-4064-b746-6d1f7be68399"), "" },
                    { 93, "Cost centers", 1, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 24, new Guid("c8de51be-3ab4-4424-8400-8a1aaaf58f2f"), "" },
                    { 94, "مراکز هزینه", 2, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 24, new Guid("6b5465dd-7488-4f29-8808-6f833a7f13d1"), "" },
                    { 95, "Cost centers", 3, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 24, new Guid("b9b7d00e-c1ed-49e2-bee0-6fc4d5ec40ea"), "" },
                    { 96, "Cost centers", 4, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 24, new Guid("f4042032-36e2-45af-97aa-f0ac5ce03a91"), "" },
                    { 97, "Projects", 1, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 25, new Guid("81b71662-211d-47aa-ab60-9417510d93bf"), "" },
                    { 98, "پروژه ها", 2, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 25, new Guid("36553d63-d7db-4f8e-962f-cdcabf7826c3"), "" },
                    { 99, "Projects", 3, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 25, new Guid("a991517c-62ed-4f7f-b7eb-ce41bd0dfe87"), "" },
                    { 274, "ویجت های من", 2, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 91, new Guid("68312df2-7f09-4a6e-adcd-6bcb0351db39"), "" },
                    { 100, "Projects", 4, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 25, new Guid("c9ddf181-adbe-43ee-99f0-9c695cc721ec"), "" },
                    { 273, "My Widgets", 1, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 91, new Guid("267e85dd-dab8-474a-8421-94cb06fec0a9"), "" },
                    { 235, "Active operation logs", 1, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 73, new Guid("675c24e7-a94c-43fe-bbff-fdfabe36f206"), "" }
                });

            migrationBuilder.InsertData(
                schema: "Reporting",
                table: "LocalReport",
                columns: new[] { "LocalReportID", "Caption", "LocaleId", "ModifiedDate", "ReportId", "rowguid", "Template" },
                values: new object[,]
                {
                    { 30, "شرکت ها", 2, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 8, new Guid("537f06d9-7dc9-451e-a586-e98a424a6b2b"), "" },
                    { 31, "Companies", 3, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 8, new Guid("1dea5427-3ea5-4b37-85f8-a79ab80068c8"), "" },
                    { 32, "Companies", 4, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 8, new Guid("686581af-087f-433a-8255-62e227816ced"), "" },
                    { 33, "Branches", 1, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 9, new Guid("c31725c6-093b-4b82-9453-d5437f76e96b"), "" },
                    { 34, "شعب سازمانی", 2, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 9, new Guid("d6e49919-8e34-4a6e-af82-2f5b493c5662"), "" },
                    { 35, "Branches", 3, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 9, new Guid("5a49885c-aee5-4648-8f52-c12cb6ff3c56"), "" },
                    { 36, "Branches", 4, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 9, new Guid("ba9a64f9-6f24-45f9-8fa0-a9146c32cc1b"), "" },
                    { 37, "Users", 1, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 10, new Guid("db05d0c9-d30f-462a-b547-3182401da19d"), "" },
                    { 38, "کاربران", 2, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 10, new Guid("600073f9-def4-4c4f-8a02-7806f66b3d25"), "" },
                    { 39, "Users", 3, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 10, new Guid("8cbe50b1-70df-477e-9d2c-f85d54811569"), "" },
                    { 40, "Users", 4, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 10, new Guid("ca951dd7-2e0f-474e-a29f-580b3909a548"), "" },
                    { 41, "Roles", 1, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 11, new Guid("73bfef8e-9325-472c-91cb-9178f17da3ee"), "" },
                    { 42, "نقش ها", 2, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 11, new Guid("40d35bbf-6f6a-4cfb-9449-45e89d64271b"), "" },
                    { 43, "Roles", 3, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 11, new Guid("2a26c9c5-12ce-4ae4-be29-1c065f07b514"), "" },
                    { 44, "Roles", 4, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 11, new Guid("29d2c012-3dbd-4a2d-96db-79a42a98d1c0"), "" },
                    { 229, "Archived operation logs", 1, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 70, new Guid("b1eee803-14f4-4db0-a0d4-c0954dc40df2"), "" },
                    { 230, "رویدادهای عملیاتی بایگانی شده", 2, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 70, new Guid("03d11c51-b0db-4d5b-8ab7-410849542fb8"), "" },
                    { 231, "Active system logs", 1, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 71, new Guid("00dd1fec-3e10-4193-ad93-0aea24e59e9f"), "" },
                    { 232, "رویدادهای سیستمی فعال", 2, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 71, new Guid("6f34eae1-b807-4907-a2f4-6138c542a0f6"), "" },
                    { 233, "Archived system logs", 1, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 72, new Guid("f5b5a52b-fecc-416a-987f-e5213e9eec7e"), "" },
                    { 234, "رویدادهای سیستمی بایگانی شده", 2, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 72, new Guid("3b6a1320-f310-434e-9562-94e2464cba73"), "" },
                    { 236, "رویدادهای عملیاتی فعال", 2, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 73, new Guid("b81e17d0-99f2-4aaf-ae66-2427546c5af6"), "" },
                    { 101, "Account groups", 1, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 26, new Guid("e4ca2608-92a1-495d-a6c4-ce37f3349556"), "" },
                    { 102, "گروه های حساب", 2, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 26, new Guid("2414eb2e-9abc-4ff0-87ac-48736cfb851c"), "" },
                    { 103, "Account groups", 3, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 26, new Guid("8f10e860-2435-4ec0-8200-1eb2f0aa6b9f"), "" },
                    { 106, "دفتر روزنامه، بر حسب تاریخ، مطابق ردیف های سند", 2, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 27, new Guid("c0742b30-4555-4867-9e09-baa3b04654bf"), "" },
                    { 107, "Journal, by date, by row", 3, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 27, new Guid("0560a627-9369-4a77-83f9-1f2049167e9a"), "" },
                    { 108, "Journal, by date, by row", 4, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 27, new Guid("c9fed515-ade3-4996-9e74-a50da507e83b"), "" },
                    { 109, "Journal, by date, by row with details", 1, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 28, new Guid("2de5e5a8-ec81-45e7-9066-6bd220c9126c"), "" },
                    { 110, "دفتر روزنامه، بر حسب تاریخ، مطابق ردیف های سند با سطوح شناور", 2, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 28, new Guid("863bfbd6-2b0a-4730-839a-4bb700549a02"), "" },
                    { 111, "Journal, by date, by row with details", 3, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 28, new Guid("724d61f7-e763-43fb-afd6-a22283ea9561"), "" },
                    { 112, "Journal, by date, by row with details", 4, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 28, new Guid("0cc2e90b-aa21-4b41-b366-089d19015f9a"), "" },
                    { 113, "Journal, by date, by ledger", 1, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 29, new Guid("9f78f2ee-c226-4907-9977-8be8b1b936d9"), "" },
                    { 114, "دفتر روزنامه، بر حسب تاریخ، در سطح کل", 2, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 29, new Guid("50929aee-a6cf-4f0b-8bed-4bf62810678c"), "" },
                    { 115, "Journal, by date, by ledger", 3, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 29, new Guid("c3645266-9925-41d4-97fd-341f3ddf426b"), "" },
                    { 116, "Journal, by date, by ledger", 4, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 29, new Guid("808bdd16-7267-459f-b82b-4d90635d2301"), "" },
                    { 117, "Journal, by date, by subsidiary", 1, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 30, new Guid("8f889a24-6d47-45c0-bc21-1ca489e07a66"), "" },
                    { 118, "دفتر روزنامه، بر حسب تاریخ، در سطح معین", 2, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 30, new Guid("6cf1f1b0-f374-4003-9f4b-2dd324a4b6d3"), "" },
                    { 119, "Journal, by date, by subsidiary", 3, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 30, new Guid("7a2184e1-0030-4ef2-a47d-61156415ad89"), "" },
                    { 120, "Journal, by date, by subsidiary", 4, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 30, new Guid("14fb41d8-094c-4647-8a46-c2c2504d391a"), "" },
                    { 121, "Journal, by date, ledger summary", 1, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 31, new Guid("323c8e67-46d7-425d-b63f-9e0786bfd197"), "" },
                    { 122, "دفتر روزنامه، بر حسب تاریخ، سند خلاصه", 2, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 31, new Guid("573aa7b4-7c05-45df-b03e-e02a88351839"), "" }
                });

            migrationBuilder.InsertData(
                schema: "Reporting",
                table: "LocalReport",
                columns: new[] { "LocalReportID", "Caption", "LocaleId", "ModifiedDate", "ReportId", "rowguid", "Template" },
                values: new object[,]
                {
                    { 123, "Journal, by date, ledger summary", 3, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 31, new Guid("92dd5e95-7f6c-441e-b9ea-20e3f68b427f"), "" },
                    { 124, "Journal, by date, ledger summary", 4, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 31, new Guid("2bffcc12-86fe-493e-b5e2-6c466b69ba0b"), "" },
                    { 125, "Journal, by date, summary by date", 1, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 32, new Guid("7d70ee97-ca04-414a-99d7-0b7a314f9b1e"), "" },
                    { 126, "دفتر روزنامه، بر حسب تاریخ، سند خلاصه به تفکیک تاریخ", 2, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 32, new Guid("0aa52996-cbd6-491f-9823-68ebf64d9b34"), "" },
                    { 105, "Journal, by date, by row", 1, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 27, new Guid("5ab57f2c-abdf-4c53-973c-38a8edfa9fac"), "" },
                    { 310, "شماره سندهای مفقود", 2, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 109, new Guid("cdc688eb-a273-41bc-ba60-55b40af97a33"), "" },
                    { 309, "Missing Voucher Numbers", 1, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 109, new Guid("92e138d2-f103-40fc-a68d-49f973587cb6"), "" },
                    { 308, "آرتیکل‌های سند", 2, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 108, new Guid("7f485deb-a37d-46ac-ac71-e84490a2b102"), "" },
                    { 104, "Account groups", 4, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 26, new Guid("40a666bb-9b7f-4439-aa6d-e651b294cc30"), "" },
                    { 237, "Currencies", 1, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 74, new Guid("c7786301-0e57-4d50-9793-070694174441"), "" },
                    { 238, "ارزها", 2, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 74, new Guid("94b6ae1c-f1a4-47f0-a017-89520d95929b"), "" },
                    { 239, "Currency rates", 1, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 75, new Guid("c77cc58d-08a2-46fc-a029-88a0dc5200c3"), "" },
                    { 240, "نرخ های ارز", 2, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 75, new Guid("29167f52-16ff-4763-b673-48b1ab7aba72"), "" },
                    { 157, "Voucher, standard format", 1, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 40, new Guid("b91e289f-49aa-43ef-8357-6d34f80e5f33"), "" },
                    { 158, "فرم مرسوم سند", 2, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 40, new Guid("520649d4-1195-45df-957f-e1209debb7e8"), "" },
                    { 159, "Voucher, standard format", 3, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 40, new Guid("90c364db-afce-45af-bc17-ead790503eb3"), "" },
                    { 160, "Voucher, standard format", 4, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 40, new Guid("52a7afb1-332e-4a88-8cf8-0c9ce56fb5b1"), "" },
                    { 161, "Voucher, standard format, with detail", 1, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 41, new Guid("3b066e06-15d5-472b-bf55-1f984a7e65a5"), "" },
                    { 295, "Check Book Report", 1, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 102, new Guid("ed05e3b3-1345-423c-a5b0-d6ecbe0abb90"), "" },
                    { 162, "فرم مرسوم سند - با سطوح شناور", 2, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 41, new Guid("c7cc7e2b-6982-45aa-8624-99dc1987eb78"), "" },
                    { 164, "Voucher, standard format, with detail", 4, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 41, new Guid("567390a9-4180-46a2-899a-e7958db6bfe5"), "" },
                    { 251, "Vouchers", 1, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 81, new Guid("b8938639-d305-4075-a18e-383137289102"), "" },
                    { 252, "اسناد مالی", 2, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 81, new Guid("e0973881-21ca-44bb-9c4a-30d6a5146640"), "" },
                    { 255, "Simple - by detail level", 1, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 82, new Guid("de220410-860d-4f2c-9a7d-f1d99bab2aee"), "" },
                    { 256, "ساده - در سطح تفصیلی", 2, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 82, new Guid("e28ecf93-964f-4b04-89cc-82c30627f6c2"), "" },
                    { 257, "Aggregate - by ledger level", 1, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 83, new Guid("f8c68fbc-ee9b-468c-9329-9dd905f08c07"), "" },
                    { 258, "مرکب - در سطح کل", 2, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 83, new Guid("04ea6019-1c53-43fe-8cc5-958ccebafaf8"), "" },
                    { 259, "Aggregate - by subsidiary level", 1, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 84, new Guid("9f24aed2-958c-445b-bac3-f9703e2a7fec"), "" },
                    { 260, "مرکب - در سطح معین", 2, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 84, new Guid("6b7d5964-f23f-4a8e-bdcd-d15130b870bc"), "" },
                    { 307, "Voucher Lines", 1, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 108, new Guid("d9ff8d95-211e-4855-ad76-79fb28cbf5e6"), "" },
                    { 163, "Voucher, standard format, with detail", 3, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 41, new Guid("adea5600-8b47-44ee-ae69-ef476b41ae13"), "" },
                    { 296, "دفتر دسته‌چک", 2, new DateTime(2023, 10, 16, 14, 57, 37, 0, DateTimeKind.Unspecified), 102, new Guid("fd525933-e053-4a55-80b8-b263d3ecde51"), "" }
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
                name: "IX_Person_UserID1",
                schema: "Contact",
                table: "Person",
                column: "UserID1",
                unique: true);

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
