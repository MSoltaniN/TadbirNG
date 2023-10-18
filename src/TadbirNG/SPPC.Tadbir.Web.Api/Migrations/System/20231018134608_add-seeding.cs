using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SPPC.Tadbir.Web.Api.Migrations.System
{
    public partial class addseeding : Migration
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
                    ReportViewId = table.Column<int>(type: "int", nullable: true),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Report", x => x.ReportID);
                    table.ForeignKey(
                        name: "FK_Report_ReportView_ReportViewId",
                        column: x => x.ReportViewId,
                        principalSchema: "Metadata",
                        principalTable: "ReportView",
                        principalColumn: "ViewID",
                        onDelete: ReferentialAction.Restrict);
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
                        principalTable: "View",
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
                values: new object[] { 1, true, new DateTime(2023, 10, 16, 8, 57, 46, 3, DateTimeKind.Unspecified), new DateTime(2023, 10, 18, 17, 16, 5, 402, DateTimeKind.Local).AddTicks(6874), "b22f213ec710f0b0e86297d10279d69171f50f01a04edf40f472a563e7ad8576", "admin" });

            migrationBuilder.InsertData(
                schema: "Metadata",
                table: "Column",
                columns: new[] { "ColumnID", "AllowFiltering", "AllowSorting", "DisplayIndex", "DotNetType", "Expression", "GroupName", "IsDynamic", "IsFixedLength", "IsNullable", "Length", "MinLength", "ModifiedDate", "Name", "rowguid", "ScriptType", "StorageType", "Type", "ViewID", "Visibility" },
                values: new object[,]
                {
                    { 604, true, true, (short)9, "System.DateTime", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(6601), "EntityDate", new Guid("9dae27de-f96f-46aa-becf-39014382601a"), "Date", "datetime", "Default", null, "Visible" },
                    { 605, true, true, (short)10, "System.String", "", "", false, false, false, 64, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(6617), "SourceName", new Guid("eae00fa1-ff9f-41c5-807e-2c931f44c314"), "string", "nvarchar", "", null, "Visible" },
                    { 606, true, true, (short)11, "System.String", "", "", false, false, false, 64, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(6635), "SourceListName", new Guid("cf90102e-2108-469a-add5-cfb413809171"), "string", "nvarchar", "", null, "Visible" },
                    { 607, true, true, (short)12, "System.String", "", "", false, false, false, 64, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(6651), "OperationName", new Guid("9047777e-3a2a-44f7-86ab-b8464f51c977"), "string", "nvarchar", "", null, "Visible" },
                    { 608, true, true, (short)13, "System.String", "", "", false, false, false, 64, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(6668), "Description", new Guid("74028b81-6c5a-4736-adb1-7cf09a67cef0"), "string", "nvarchar", "", null, "Visible" },
                    { 609, true, true, (short)14, "System.String", "", "", false, false, false, 64, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(6687), "CompanyName", new Guid("427c570f-8fea-4e6f-b3c8-cc16c20b2fe9"), "string", "nvarchar", "", null, "Visible" },
                    { 610, true, true, (short)-1, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(6709), "Id", new Guid("19568767-bb1b-4c08-91dd-f25ee961f14d"), "number", "int", "", null, "AlwaysHidden" },
                    { 611, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(6726), "RowNo", new Guid("d92b3fa6-9706-4c39-bddf-9aa66f2bb318"), "number", "int", "", null, "AlwaysVisible" },
                    { 612, true, true, (short)1, "System.String", "", "", false, false, false, 64, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(6742), "UserName", new Guid("42d2a2d2-17a0-44a1-b4ab-67d5c2126cca"), "string", "nvarchar", "", null, "Visible" },
                    { 613, true, true, (short)2, "System.Date", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(6758), "Date", new Guid("22369686-ea30-4639-a6c9-5b5386fb4ab9"), "Date", "datetime", "Default", null, "AlwaysVisible" },
                    { 614, true, true, (short)3, "System.TimeSpan", "", "", false, false, false, 7, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(6774), "Time", new Guid("1175b71d-ada8-4b21-ab97-e92896da140d"), "Date", "time", "", null, "AlwaysVisible" },
                    { 603, true, true, (short)8, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(6585), "EntityNo", new Guid("53b3deff-7472-4328-b1bb-0768ca72b026"), "number", "int", "", null, "Visible" },
                    { 615, true, true, (short)4, "System.String", "", "", false, false, false, 64, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(6790), "EntityTypeName", new Guid("a63785f4-f88d-462d-b6f9-9ab30246a4f4"), "string", "nvarchar", "", null, "Visible" },
                    { 617, true, true, (short)6, "System.String", "", "", false, false, false, 64, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(6822), "EntityName", new Guid("57e1c888-7d1c-4f3d-8043-10f90af593f8"), "string", "nvarchar", "", null, "Visible" },
                    { 618, true, true, (short)7, "System.String", "", "", false, false, false, 64, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(6843), "EntityDescription", new Guid("b5ae73c0-64ae-454a-b21b-18cbade402cf"), "string", "nvarchar", "", null, "Visible" },
                    { 619, true, true, (short)8, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(6860), "EntityNo", new Guid("e5bb5cce-bbf6-429e-ba23-0409dbfa932c"), "number", "int", "", null, "Visible" },
                    { 620, true, true, (short)9, "System.DateTime", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(6876), "EntityDate", new Guid("1c88fae4-0e6e-4487-8944-782f99f2e4ba"), "Date", "datetime", "Default", null, "Visible" },
                    { 621, true, true, (short)10, "System.String", "", "", false, false, false, 64, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(6987), "SourceName", new Guid("0ed21718-1436-4807-8e27-4916258b3ffe"), "string", "nvarchar", "", null, "Visible" },
                    { 622, true, true, (short)11, "System.String", "", "", false, false, false, 64, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(7006), "SourceListName", new Guid("55959cd6-1646-47d1-b00d-e75095aa345d"), "string", "nvarchar", "", null, "Visible" },
                    { 623, true, true, (short)12, "System.String", "", "", false, false, false, 64, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(7022), "OperationName", new Guid("f0bee936-d91e-40d1-b1da-31f3d467dcc9"), "string", "nvarchar", "", null, "Visible" },
                    { 624, true, true, (short)13, "System.String", "", "", false, false, false, 64, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(7038), "Description", new Guid("164838dc-c66f-4610-bc08-561a90fd85bc"), "string", "nvarchar", "", null, "Visible" },
                    { 625, true, true, (short)14, "System.String", "", "", false, false, false, 64, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(7053), "CompanyName", new Guid("afdcaf59-0932-4b93-a873-a92fb5222d96"), "string", "nvarchar", "", null, "Visible" },
                    { 626, true, true, (short)-1, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(7075), "Id", new Guid("7e4df576-b148-4f85-ac6f-32aedc0ab9ca"), "number", "int", "", null, "AlwaysHidden" },
                    { 627, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(7093), "RowNo", new Guid("458b7995-de1a-465a-ba1a-41e0bca934bb"), "number", "int", "", null, "AlwaysVisible" },
                    { 616, true, true, (short)5, "System.String", "", "", false, false, false, 64, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(6806), "EntityCode", new Guid("6b34d60f-acf4-4daa-94f6-c8ed64528298"), "string", "nvarchar", "", null, "Visible" },
                    { 602, true, true, (short)7, "System.String", "", "", false, false, false, 64, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(6569), "EntityDescription", new Guid("15b09465-fdbc-4ea2-ac95-3e51f9eb0fb0"), "string", "nvarchar", "", null, "Visible" },
                    { 601, true, true, (short)6, "System.String", "", "", false, false, false, 64, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(6547), "EntityName", new Guid("9a0726dd-a7bb-42f2-b400-fb24802167a9"), "string", "nvarchar", "", null, "Visible" },
                    { 600, true, true, (short)5, "System.String", "", "", false, false, false, 64, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(6531), "EntityCode", new Guid("acedf08f-0997-40f3-98f2-e1f278bb1610"), "string", "nvarchar", "", null, "Visible" },
                    { 575, true, true, (short)3, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(6012), "DetailAccountFullCode", new Guid("1df997d3-c48e-4fd5-86cf-2a3069204f68"), "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 576, true, true, (short)4, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(6028), "DetailAccountName", new Guid("9913f594-f3a8-4369-89b5-05ce12cea7c9"), "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 577, true, true, (short)5, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(6043), "CostCenterFullCode", new Guid("9eda519e-6178-4854-9b65-d8c7e6094d04"), "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 578, true, true, (short)6, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(6065), "CostCenterName", new Guid("b5a9077a-4630-4e67-af5a-8e3e2c56deff"), "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 579, true, true, (short)7, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(6080), "ProjectFullCode", new Guid("39851f7d-7036-466d-89af-5d26c6747438"), "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 580, true, true, (short)8, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(6098), "ProjectName", new Guid("8b98881e-e1ce-4bdf-a14b-3646e7b50d31"), "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 581, true, true, (short)9, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(6114), "AccountDescription", new Guid("55d9db14-1602-4dad-9051-9c6c690a2e9e"), "string", "nvarchar", "", null, "Visible" },
                    { 582, true, true, (short)10, "System.Decimal", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(6130), "StartBalance", new Guid("f67ff10a-814c-451f-ad63-4cf4d010d58a"), "number", "money", "Money", null, "Visible" },
                    { 583, true, true, (short)11, "System.Decimal", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(6145), "Debit", new Guid("5d2a3ea5-508d-4bf4-b34e-b9f97d573b6d"), "number", "money", "Money", null, "Visible" },
                    { 584, true, true, (short)12, "System.Decimal", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(6161), "Credit", new Guid("54e92cfb-42f8-4df3-9470-6ea213a01cca"), "number", "money", "Money", null, "Visible" },
                    { 585, true, true, (short)13, "System.Decimal", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(6177), "EndBalance", new Guid("6a829b37-5638-4264-9e52-d8005b2c577a"), "number", "money", "Money", null, "Visible" },
                    { 586, true, true, (short)14, "System.String", "", "", false, false, false, 128, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(6198), "BranchName", new Guid("c62abd07-1729-43c0-9b3e-9529e6a79b69"), "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 587, true, true, (short)12, "System.String", "", "", false, false, false, 64, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(6216), "EntityReference", new Guid("37175b82-86c8-4921-978d-006e8e2e557d"), "string", "nvarchar", "", null, "Visible" }
                });

            migrationBuilder.InsertData(
                schema: "Metadata",
                table: "Column",
                columns: new[] { "ColumnID", "AllowFiltering", "AllowSorting", "DisplayIndex", "DotNetType", "Expression", "GroupName", "IsDynamic", "IsFixedLength", "IsNullable", "Length", "MinLength", "ModifiedDate", "Name", "rowguid", "ScriptType", "StorageType", "Type", "ViewID", "Visibility" },
                values: new object[,]
                {
                    { 588, true, true, (short)13, "System.String", "", "", false, false, false, 64, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(6231), "EntityAssociation", new Guid("139ca932-c9ff-47d2-a296-d3d884137b5d"), "string", "nvarchar", "", null, "Visible" },
                    { 589, true, true, (short)14, "System.String", "", "", false, false, false, 64, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(6345), "SourceName", new Guid("f9dfcd71-d2de-4437-9871-1cca030cd061"), "string", "nvarchar", "", null, "Visible" },
                    { 590, true, true, (short)15, "System.String", "", "", false, false, false, 64, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(6362), "SourceListName", new Guid("a664e7d8-2e32-4edd-a99c-bb93d501beb4"), "string", "nvarchar", "", null, "Visible" },
                    { 591, true, true, (short)16, "System.String", "", "", false, false, false, 64, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(6378), "OperationName", new Guid("0f78bfc3-c47b-4661-be68-496202df8f71"), "string", "nvarchar", "", null, "Visible" },
                    { 592, true, true, (short)17, "System.String", "", "", false, false, false, 64, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(6394), "Description", new Guid("543cd471-0add-46a2-be87-fbf4096775d8"), "string", "nvarchar", "", null, "Visible" },
                    { 593, true, true, (short)18, "System.String", "", "", false, false, false, 64, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(6410), "CompanyName", new Guid("bacb2d55-03ee-424f-a752-8601c0b26f58"), "string", "nvarchar", "", null, "Visible" },
                    { 594, true, true, (short)-1, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(6433), "Id", new Guid("10828517-1740-4cf3-b1b2-bc8e3b227b30"), "number", "int", "", null, "AlwaysHidden" },
                    { 595, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(6449), "RowNo", new Guid("eb834737-ffbd-400b-bb88-03c98199a0c0"), "number", "int", "", null, "AlwaysVisible" },
                    { 596, true, true, (short)1, "System.String", "", "", false, false, false, 64, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(6465), "UserName", new Guid("3a46ba6e-c987-4326-a7ae-2eefb0d50d88"), "string", "nvarchar", "", null, "Visible" },
                    { 597, true, true, (short)2, "System.Date", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(6481), "Date", new Guid("8b0e2e3d-df63-4ffb-8a0d-2ca2cff7071f"), "Date", "datetime", "Default", null, "AlwaysVisible" },
                    { 598, true, true, (short)3, "System.TimeSpan", "", "", false, false, false, 7, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(6497), "Time", new Guid("5d9a0f4c-2ec9-4bac-89f2-907c346e0957"), "Date", "time", "", null, "AlwaysVisible" },
                    { 599, true, true, (short)4, "System.String", "", "", false, false, false, 64, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(6513), "EntityTypeName", new Guid("39a0fa22-593c-4bd2-a6b7-2338804d3a53"), "string", "nvarchar", "", null, "Visible" },
                    { 628, true, true, (short)1, "System.String", "", "", false, false, false, 64, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(7109), "UserName", new Guid("d6e42e83-9248-4330-8cf4-a6f43a5aeb7c"), "string", "nvarchar", "", null, "Visible" },
                    { 574, true, true, (short)2, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(5996), "AccountName", new Guid("a81f18d5-95c3-4d77-9f18-92f9c333fa6b"), "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 629, true, true, (short)2, "System.String", "", "", false, false, false, 64, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(7125), "BranchName", new Guid("c6a75130-bee5-4e90-9f94-126fc06b6f2e"), "string", "nvarchar", "", null, "Visible" },
                    { 631, true, true, (short)4, "System.Date", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(7156), "Date", new Guid("5d01d038-9ff7-43e3-97a4-37337333ba91"), "Date", "datetime", "Default", null, "AlwaysVisible" },
                    { 661, true, true, (short)0, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(7767), "No", new Guid("b503b4a9-fc3b-460f-89c5-96b4a0192022"), "number", "int", "", null, "AlwaysVisible" },
                    { 662, true, true, (short)1, "System.DateTime", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(7783), "Date", new Guid("2f5cd615-d448-48ec-bf34-22bf145ba42d"), "Date", "datetime", "Default", null, "AlwaysVisible" },
                    { 663, true, true, (short)2, "System.String", "", "", false, false, false, 128, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(7799), "Name", new Guid("6fffdcf7-729b-4a76-9bb9-6e33525a01ba"), "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 664, true, true, (short)3, "System.String", "", "", false, false, false, 128, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(7814), "FullCode", new Guid("7647bab9-fec1-4054-b2eb-e46de7362821"), "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 665, true, true, (short)4, "System.String", "", "", false, false, false, 128, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(7830), "ErrorMessage", new Guid("9e70e2ad-a575-4208-808d-adee72f040f1"), "string", "nvarchar", "", null, "Visible" },
                    { 666, false, false, (short)0, "System.String", "", "", false, false, true, 64, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(7853), "Group", new Guid("95c47a8a-85ea-48a9-ba78-686085ca8108"), "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 667, false, false, (short)1, "System.String", "", "", false, false, true, 64, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(7869), "Account", new Guid("361c79ab-20c8-4a58-8909-c7aa42118528"), "string", "nvarchar", "", null, "Visible" },
                    { 668, false, false, (short)2, "System.Decimal", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(7888), "Balance", new Guid("cb28ffed-d364-40c5-a80c-0fce55572d8f"), "number", "money", "Money", null, "Visible" },
                    { 669, true, true, (short)15, "System.String", "", "", false, false, true, 64, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(7904), "OriginName", new Guid("d6853602-9ecb-4aaa-bf0e-7d0eaa9611b5"), "string", "nvarchar", "", null, "Visible" },
                    { 670, false, false, (short)0, "System.String", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(7922), "Group", new Guid("df983a82-727e-4d24-be56-d63f46e07966"), "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 671, false, false, (short)1, "System.String", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(7937), "Account", new Guid("8071cd93-51e7-4a1a-9f74-e38a810ccf9a"), "string", "nvarchar", "", null, "Visible" },
                    { 660, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(7749), "Id", new Guid("4b1b66cd-c0da-4628-85bd-ca645bb8364c"), "number", "int", "", null, "AlwaysHidden" },
                    { 672, false, false, (short)2, "System.Decimal", "", "", true, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(7953), "StartBalanceItem", new Guid("8c3f3c6e-e263-4652-a123-f86789f67e5b"), "number", "money", "Money", null, "Visible" },
                    { 674, false, false, (short)4, "System.Decimal", "", "", true, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(7992), "EndBalanceItem", new Guid("ef1514b2-6c44-489b-bf02-05d048ad5938"), "number", "money", "Money", null, "Visible" },
                    { 675, false, false, (short)0, "System.String", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(8010), "Group", new Guid("54a99d63-0223-43cc-9c85-50a6965bbaf0"), "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 676, false, false, (short)1, "System.String", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(8026), "Account", new Guid("e4197933-004f-4378-a860-1b36432ba767"), "string", "nvarchar", "", null, "Visible" },
                    { 677, false, false, (short)2, "System.Decimal", "", "", true, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(8043), "BalanceItem", new Guid("b1b39187-5382-417b-a256-4e954860c9a4"), "number", "money", "Money", null, "Visible" },
                    { 678, false, false, (short)16, "System.String", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(8059), "TypeName", new Guid("4dab93fd-96df-4ac2-b3f9-af11382b36d9"), "string", "nvarchar", "", null, "Visible" },
                    { 679, false, false, (short)0, "System.String", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(8074), "Assets", new Guid("e75d9b65-5a0e-4be7-b214-7a2efe053835"), "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 680, false, false, (short)1, "System.Decimal", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(8092), "AssetsBalance", new Guid("8aa8dc51-8da1-448a-8e04-f205fe1931df"), "number", "money", "Money", null, "Visible" },
                    { 681, false, false, (short)2, "System.Decimal", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(8110), "AssetsPreviousBalance", new Guid("144c6e80-f2b2-4aa4-9164-ccc73ecc1bc4"), "number", "money", "Money", null, "Visible" },
                    { 682, false, false, (short)3, "System.String", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(8131), "Liabilities", new Guid("553d6542-da32-4677-8d3d-36734b5f9f73"), "string", "nvarchar", "", null, "Visible" },
                    { 683, false, false, (short)4, "System.Decimal", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(8147), "LiabilitiesBalance", new Guid("f66ff308-694b-4f81-80be-917e6ea104fe"), "number", "money", "Money", null, "Visible" },
                    { 684, false, false, (short)5, "System.Decimal", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(8162), "LiabilitiesPreviousBalance", new Guid("1fd6918a-ba65-4fb0-935f-a209e4898199"), "number", "money", "Money", null, "Visible" },
                    { 673, false, false, (short)3, "System.Decimal", "", "", true, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(7970), "PeriodTurnoverItem", new Guid("eb6d8d01-c7d9-426c-aa37-26cfb5f9d9f0"), "number", "money", "Money", null, "Visible" },
                    { 659, true, true, (short)14, "System.String", "", "", false, false, true, 120, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(7732), "IssuerName", new Guid("7b0b4a0e-0dbc-4cda-bd08-f28ee700e46a"), "string", "nvarchar", "", null, "Visible" }
                });

            migrationBuilder.InsertData(
                schema: "Metadata",
                table: "Column",
                columns: new[] { "ColumnID", "AllowFiltering", "AllowSorting", "DisplayIndex", "DotNetType", "Expression", "GroupName", "IsDynamic", "IsFixedLength", "IsNullable", "Length", "MinLength", "ModifiedDate", "Name", "rowguid", "ScriptType", "StorageType", "Type", "ViewID", "Visibility" },
                values: new object[,]
                {
                    { 658, true, true, (short)13, "System.String", "", "", false, false, true, 120, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(7717), "BranchName", new Guid("7317bcd3-10e1-4005-af43-1cdaabb56aa6"), "string", "nvarchar", "", null, "Visible" },
                    { 657, true, true, (short)12, "System.Boolean", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(7695), "IsApproved", new Guid("ea417f4a-3f7e-4b4f-ba53-079c18016e7f"), "boolean", "bit", "", null, "Visible" },
                    { 632, true, true, (short)5, "System.TimeSpan", "", "", false, false, false, 7, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(7172), "Time", new Guid("494dae4d-bc83-45e8-aa62-d06fe09be0b3"), "Date", "time", "", null, "AlwaysVisible" },
                    { 633, true, true, (short)6, "System.String", "", "", false, false, false, 64, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(7189), "EntityTypeName", new Guid("38597898-fd44-4248-9fc6-0544796c5fcc"), "string", "nvarchar", "", null, "Visible" },
                    { 634, true, true, (short)7, "System.String", "", "", false, false, false, 64, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(7210), "EntityCode", new Guid("1f2662bf-b504-4a4f-ab56-d38f823f2824"), "string", "nvarchar", "", null, "Visible" },
                    { 635, true, true, (short)8, "System.String", "", "", false, false, false, 64, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(7226), "EntityName", new Guid("7683949b-a160-44b8-8b34-df6b7f8a7acd"), "string", "nvarchar", "", null, "Visible" },
                    { 636, true, true, (short)9, "System.String", "", "", false, false, false, 64, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(7243), "EntityDescription", new Guid("62f272dc-82ac-4c3d-a6fb-a43725b196d2"), "string", "nvarchar", "", null, "Visible" },
                    { 637, true, true, (short)10, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(7259), "EntityNo", new Guid("a367c91b-b9c3-4ae4-91a2-6cc0d31feaa1"), "number", "int", "", null, "Visible" },
                    { 638, true, true, (short)11, "System.DateTime", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(7275), "EntityDate", new Guid("914aa64e-ec9a-47b0-ab7b-34928d27209f"), "Date", "datetime", "Default", null, "Visible" },
                    { 639, true, true, (short)12, "System.String", "", "", false, false, false, 64, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(7291), "EntityReference", new Guid("f9a39a42-4521-4588-8300-d5fe7f023852"), "string", "nvarchar", "", null, "Visible" },
                    { 640, true, true, (short)13, "System.String", "", "", false, false, false, 64, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(7309), "EntityAssociation", new Guid("eb6c74c1-5c35-4920-976a-686ca8dbc547"), "string", "nvarchar", "", null, "Visible" },
                    { 641, true, true, (short)14, "System.String", "", "", false, false, false, 64, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(7325), "SourceName", new Guid("134c86ca-8328-45f5-9799-5be8f37bb5ec"), "string", "nvarchar", "", null, "Visible" },
                    { 642, true, true, (short)15, "System.String", "", "", false, false, false, 64, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(7346), "SourceListName", new Guid("2921fbdd-ee59-481a-af64-078c88019e98"), "string", "nvarchar", "", null, "Visible" },
                    { 643, true, true, (short)16, "System.String", "", "", false, false, false, 64, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(7362), "OperationName", new Guid("78d04f05-ecdf-4312-a38e-e2658eae8b1e"), "string", "nvarchar", "", null, "Visible" },
                    { 644, true, true, (short)17, "System.String", "", "", false, false, false, 64, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(7379), "Description", new Guid("35eb6445-e4a4-4be5-94a7-ddeedfa40728"), "string", "nvarchar", "", null, "Visible" },
                    { 645, true, true, (short)18, "System.String", "", "", false, false, false, 64, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(7394), "CompanyName", new Guid("5ce780cc-24f8-474d-806c-596f497a6f66"), "string", "nvarchar", "", null, "Visible" },
                    { 646, true, true, (short)-1, "System.String", "", "", false, false, false, 16, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(7410), "Password", new Guid("60f76bc4-7ad1-41d9-a407-e8da2024037f"), "string", "nvarchar", "", null, "AlwaysHidden" },
                    { 647, true, true, (short)8, "System.Boolean", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(7426), "IsBalanced", new Guid("fe94393e-cebc-4822-bf1c-6d3ee453666b"), "boolean", "bit", "", null, "Visible" },
                    { 648, true, true, (short)9, "System.String", "", "", false, false, true, 120, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(7445), "ConfirmerName", new Guid("a164e859-cc66-41ad-9b11-6759dae691b6"), "string", "nvarchar", "", null, "Visible" },
                    { 649, true, true, (short)10, "System.String", "", "", false, false, true, 120, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(7461), "ApproverName", new Guid("7f342668-54d6-4985-8f41-8a7481a4507e"), "string", "nvarchar", "", null, "Visible" },
                    { 650, false, false, (short)0, "System.String", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(7483), "Group", new Guid("3e3e4364-e6ed-4c81-85ac-73e2b899f008"), "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 651, false, false, (short)1, "System.String", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(7499), "Account", new Guid("c2a58cd7-f399-427f-919f-f4ddc8e38d71"), "string", "nvarchar", "", null, "Visible" },
                    { 652, false, false, (short)2, "System.Decimal", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(7516), "StartBalance", new Guid("7fbff6e4-8f73-4279-8694-3b78b5ce14a3"), "number", "money", "Money", null, "Visible" },
                    { 653, false, false, (short)3, "System.Decimal", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(7532), "PeriodTurnover", new Guid("b454a63c-f48f-4298-896e-87e65f12e4b8"), "number", "money", "Money", null, "Visible" },
                    { 654, false, false, (short)4, "System.Decimal", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(7644), "EndBalance", new Guid("2297b346-628e-45a1-9ed5-ad6c639e79d3"), "number", "money", "Money", null, "Visible" },
                    { 655, false, false, (short)5, "System.Decimal", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(7662), "Balance", new Guid("12e5ef6d-90d3-45c0-b04b-9e2ef7f01a6f"), "number", "money", "Money", null, "Hidden" },
                    { 656, true, true, (short)11, "System.Boolean", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(7678), "IsConfirmed", new Guid("5574f2a6-4243-4913-b1d6-5e7bd0f80feb"), "boolean", "bit", "", null, "Visible" },
                    { 630, true, true, (short)3, "System.String", "", "", false, false, false, 64, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(7141), "FiscalPeriodName", new Guid("4a274c97-dd99-4dcf-b99b-640d35a4fa8b"), "string", "nvarchar", "", null, "Visible" },
                    { 685, true, true, (short)14, "System.String", "", "", false, false, false, 64, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(8178), "BranchName", new Guid("0592b12c-64ce-4897-acf4-4978ae8b5cf1"), "string", "nvarchar", "", null, "Hidden" },
                    { 573, true, true, (short)1, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(5980), "AccountFullCode", new Guid("42478b9c-d2c1-4696-ac97-78e9f3517613"), "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 571, true, true, (short)-1, "System.String", "", "", false, false, true, 64, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(5944), "VoucherReference", new Guid("83b98dda-301f-40fb-91f1-e2401e8a7e30"), "string", "nvarchar", "", null, "AlwaysHidden" },
                    { 489, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(4044), "RowNo", new Guid("7543269e-39ce-4b75-bf0c-ef27f6042c82"), "number", "int", "", null, "AlwaysVisible" },
                    { 490, true, true, (short)1, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(4164), "CostCenterFullCode", new Guid("8ce567db-5572-4f80-8789-d791f55e9de2"), "string", "nvarchar", "", null, "Visible" },
                    { 491, true, true, (short)2, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(4183), "CostCenterName", new Guid("d7fe81d0-9bed-4365-963f-6fb3754cd940"), "string", "nvarchar", "", null, "Visible" },
                    { 492, true, true, (short)3, "System.Decimal", "", "StartBalance", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(4201), "StartBalanceDebit", new Guid("9aa38bf5-0fac-4d90-a55f-86fd53b1d657"), "number", "money", "Money", null, "Visible" },
                    { 493, true, true, (short)4, "System.Decimal", "", "StartBalance", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(4218), "StartBalanceCredit", new Guid("677482ad-b53c-4925-bd57-bfc9458f54d2"), "number", "money", "Money", null, "Visible" },
                    { 494, true, true, (short)5, "System.Decimal", "", "Turnover", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(4234), "TurnoverDebit", new Guid("bf7c9766-7fc9-4a66-b9b8-1fefe7137484"), "number", "money", "Money", null, "Visible" },
                    { 495, true, true, (short)6, "System.Decimal", "", "Turnover", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(4250), "TurnoverCredit", new Guid("a4a94510-71f7-4884-9063-58d981f8867a"), "number", "money", "Money", null, "Visible" },
                    { 496, true, true, (short)7, "System.Decimal", "", "OperationSum", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(4265), "OperationSumDebit", new Guid("c1d92b6a-b0e2-4764-8fe3-ad8e0edf5ce2"), "number", "money", "Money", null, "Visible" },
                    { 497, true, true, (short)8, "System.Decimal", "", "OperationSum", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(4281), "OperationSumCredit", new Guid("bcd9252e-1b49-4b4c-b033-3813950eee2e"), "number", "money", "Money", null, "Visible" },
                    { 498, true, true, (short)9, "System.Decimal", "", "EndBalance", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(4302), "EndBalanceDebit", new Guid("0d990f86-cf8e-42af-b6d2-918df3eeb08f"), "number", "money", "Money", null, "Visible" },
                    { 499, true, true, (short)10, "System.Decimal", "", "EndBalance", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(4320), "EndBalanceCredit", new Guid("c8ce68c6-31f9-47d8-9394-3b928681fdb3"), "number", "money", "Money", null, "Visible" }
                });

            migrationBuilder.InsertData(
                schema: "Metadata",
                table: "Column",
                columns: new[] { "ColumnID", "AllowFiltering", "AllowSorting", "DisplayIndex", "DotNetType", "Expression", "GroupName", "IsDynamic", "IsFixedLength", "IsNullable", "Length", "MinLength", "ModifiedDate", "Name", "rowguid", "ScriptType", "StorageType", "Type", "ViewID", "Visibility" },
                values: new object[,]
                {
                    { 488, true, true, (short)-1, "System.String", "", "", false, false, true, 64, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(4029), "VoucherReference", new Guid("63c8cc29-6578-4902-965c-ba265721e3ad"), "string", "nvarchar", "", null, "AlwaysHidden" },
                    { 500, true, true, (short)11, "System.String", "", "", false, false, false, 128, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(4336), "BranchName", new Guid("a8beff4a-67fc-4b23-9635-dbdeb4651d34"), "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 502, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(4367), "RowNo", new Guid("cb90cb27-f7f7-4ae1-b964-bd116d0395c7"), "number", "int", "", null, "AlwaysVisible" },
                    { 503, true, true, (short)1, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(4383), "CostCenterFullCode", new Guid("ea5fa07c-3601-41ea-906b-b0955df46896"), "string", "nvarchar", "", null, "Visible" },
                    { 504, true, true, (short)2, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(4399), "CostCenterName", new Guid("f1efdfc6-bf8a-48a4-8da2-7dd1d080409c"), "string", "nvarchar", "", null, "Visible" },
                    { 505, true, true, (short)3, "System.Decimal", "", "StartBalance", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(4414), "StartBalanceDebit", new Guid("3691c30b-f18d-410b-ba15-7ad916bd32d2"), "number", "money", "Money", null, "Visible" },
                    { 506, true, true, (short)4, "System.Decimal", "", "StartBalance", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(4437), "StartBalanceCredit", new Guid("c3aca553-1c26-473a-8d32-2b25fcd30b7e"), "number", "money", "Money", null, "Visible" },
                    { 507, true, true, (short)5, "System.Decimal", "", "Turnover", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(4453), "TurnoverDebit", new Guid("34c71352-f50b-466f-9784-e554c3ab531a"), "number", "money", "Money", null, "Visible" },
                    { 508, true, true, (short)6, "System.Decimal", "", "Turnover", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(4469), "TurnoverCredit", new Guid("94a6fc08-518d-453c-8e69-26f0b1ba8127"), "number", "money", "Money", null, "Visible" },
                    { 509, true, true, (short)7, "System.Decimal", "", "OperationSum", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(4485), "OperationSumDebit", new Guid("17c21d05-6168-4377-bdb4-985e7139c9e7"), "number", "money", "Money", null, "Visible" },
                    { 510, true, true, (short)8, "System.Decimal", "", "OperationSum", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(4503), "OperationSumCredit", new Guid("4ac18685-08f1-42b8-a655-1baadc5b0c3f"), "number", "money", "Money", null, "Visible" },
                    { 511, true, true, (short)9, "System.Decimal", "", "Corrections", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(4519), "CorrectionsDebit", new Guid("2ccb9800-dc69-4e66-ad88-53f5339997f6"), "number", "money", "Money", null, "Visible" },
                    { 512, true, true, (short)10, "System.Decimal", "", "Corrections", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(4534), "CorrectionsCredit", new Guid("0c3e2069-9d3a-480d-b07e-b752c1fbcf79"), "number", "money", "Money", null, "Visible" },
                    { 501, true, true, (short)-1, "System.String", "", "", false, false, true, 64, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(4351), "VoucherReference", new Guid("87974470-9132-411f-ac49-541b8000e792"), "string", "nvarchar", "", null, "AlwaysHidden" },
                    { 487, true, true, (short)9, "System.String", "", "", false, false, false, 128, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(4013), "BranchName", new Guid("061ae7f2-502c-4dc2-9894-f549c1e91325"), "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 486, true, true, (short)8, "System.Decimal", "", "EndBalance", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(3997), "EndBalanceCredit", new Guid("ee1b2ae3-75f0-42b9-8c36-e86eb24e0a4c"), "number", "money", "Money", null, "Visible" },
                    { 485, true, true, (short)7, "System.Decimal", "", "EndBalance", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(3980), "EndBalanceDebit", new Guid("ee19d3d5-0efd-4853-8a07-72b39d8198f5"), "number", "money", "Money", null, "Visible" },
                    { 460, true, true, (short)13, "System.String", "", "", false, false, false, 128, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(3557), "BranchName", new Guid("7ff0b96c-089a-42ea-8d81-665105892fd1"), "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 461, true, true, (short)-1, "System.String", "", "", false, false, true, 64, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(3572), "VoucherReference", new Guid("dfdb1036-5fe9-4a7c-81f6-8b2d10f27338"), "string", "nvarchar", "", null, "AlwaysHidden" },
                    { 462, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(3593), "RowNo", new Guid("647f58fb-935c-4742-9172-8a90bf6f3c49"), "number", "int", "", null, "AlwaysVisible" },
                    { 463, true, true, (short)1, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(3609), "CostCenterFullCode", new Guid("6034452f-75ac-4622-9af6-0bed10d6b209"), "string", "nvarchar", "", null, "Visible" },
                    { 464, true, true, (short)2, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(3624), "CostCenterName", new Guid("82efccd5-a9a8-4221-80a0-17e193c3148d"), "string", "nvarchar", "", null, "Visible" },
                    { 465, true, true, (short)3, "System.Decimal", "", "EndBalance", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(3642), "EndBalanceDebit", new Guid("4090885a-c3d8-497f-a70d-d16b1f7dd395"), "number", "money", "Money", null, "Visible" },
                    { 466, true, true, (short)4, "System.Decimal", "", "EndBalance", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(3665), "EndBalanceCredit", new Guid("3f8169f3-b043-4783-a052-5737edd228d6"), "number", "money", "Money", null, "Visible" },
                    { 467, true, true, (short)5, "System.String", "", "", false, false, false, 128, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(3681), "BranchName", new Guid("16cfaf30-4f08-4382-a3eb-e49812b61f4a"), "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 468, true, true, (short)-1, "System.String", "", "", false, false, true, 64, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(3697), "VoucherReference", new Guid("de38f9ce-04be-4443-8dfd-96b515d43151"), "string", "nvarchar", "", null, "AlwaysHidden" },
                    { 469, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(3713), "RowNo", new Guid("d9e56399-41a4-42e0-a0de-b0810a6602e5"), "number", "int", "", null, "AlwaysVisible" },
                    { 470, true, true, (short)1, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(3729), "CostCenterFullCode", new Guid("44eb7d7e-db9c-4f3e-ad81-0a2e48f58448"), "string", "nvarchar", "", null, "Visible" },
                    { 471, true, true, (short)2, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(3745), "CostCenterName", new Guid("efc23672-07e8-45db-a52d-00091788b32e"), "string", "nvarchar", "", null, "Visible" },
                    { 472, true, true, (short)3, "System.Decimal", "", "Turnover", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(3762), "TurnoverDebit", new Guid("3fe775c9-d9b0-4159-8318-e696858c85a1"), "number", "money", "Money", null, "Visible" },
                    { 473, true, true, (short)4, "System.Decimal", "", "Turnover", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(3778), "TurnoverCredit", new Guid("89455ca2-ea0c-4e10-9f2f-aec6220d3537"), "number", "money", "Money", null, "Visible" },
                    { 474, true, true, (short)5, "System.Decimal", "", "EndBalance", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(3800), "EndBalanceDebit", new Guid("e40604ad-b192-4383-87e0-be51d2f9545f"), "number", "money", "Money", null, "Visible" },
                    { 475, true, true, (short)6, "System.Decimal", "", "EndBalance", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(3816), "EndBalanceCredit", new Guid("cd38ffa2-f3bb-43ae-862f-3cfafb6d872d"), "number", "money", "Money", null, "Visible" },
                    { 476, true, true, (short)7, "System.String", "", "", false, false, false, 128, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(3831), "BranchName", new Guid("49899948-787e-47ec-b7d5-cd5ac845f585"), "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 477, true, true, (short)-1, "System.String", "", "", false, false, true, 64, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(3848), "VoucherReference", new Guid("06f3da43-3e01-4c39-8779-b97ffe3af68b"), "string", "nvarchar", "", null, "AlwaysHidden" },
                    { 478, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(3864), "RowNo", new Guid("c73f7b40-8187-4e3d-b5c7-2e7d57d441a9"), "number", "int", "", null, "AlwaysVisible" },
                    { 479, true, true, (short)1, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(3880), "CostCenterFullCode", new Guid("bf07ba82-cc1c-46ac-a4b6-ecb3b6f4b079"), "string", "nvarchar", "", null, "Visible" },
                    { 480, true, true, (short)2, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(3896), "CostCenterName", new Guid("1029a5be-155b-4804-942e-b225bcddd75d"), "string", "nvarchar", "", null, "Visible" },
                    { 481, true, true, (short)3, "System.Decimal", "", "StartBalance", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(3911), "StartBalanceDebit", new Guid("b27f87d2-9c82-48fa-acaa-63d65c8046cd"), "number", "money", "Money", null, "Visible" },
                    { 482, true, true, (short)4, "System.Decimal", "", "StartBalance", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(3933), "StartBalanceCredit", new Guid("e341e581-9c2f-4547-a381-b59bb16cb14e"), "number", "money", "Money", null, "Visible" },
                    { 483, true, true, (short)5, "System.Decimal", "", "Turnover", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(3949), "TurnoverDebit", new Guid("87ba19db-79d7-4433-be4b-c159978f2f4f"), "number", "money", "Money", null, "Visible" },
                    { 484, true, true, (short)6, "System.Decimal", "", "Turnover", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(3965), "TurnoverCredit", new Guid("a0c9e482-0190-4827-b5d3-cf20dd31b099"), "number", "money", "Money", null, "Visible" }
                });

            migrationBuilder.InsertData(
                schema: "Metadata",
                table: "Column",
                columns: new[] { "ColumnID", "AllowFiltering", "AllowSorting", "DisplayIndex", "DotNetType", "Expression", "GroupName", "IsDynamic", "IsFixedLength", "IsNullable", "Length", "MinLength", "ModifiedDate", "Name", "rowguid", "ScriptType", "StorageType", "Type", "ViewID", "Visibility" },
                values: new object[,]
                {
                    { 513, true, true, (short)11, "System.Decimal", "", "EndBalance", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(4552), "EndBalanceDebit", new Guid("1ec048d8-8147-4cbd-ae12-ef3af7fb6e99"), "number", "money", "Money", null, "Visible" },
                    { 572, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(5960), "RowNo", new Guid("13ccef41-4962-4dcf-884a-4a6383b652c2"), "number", "int", "", null, "AlwaysVisible" },
                    { 514, true, true, (short)12, "System.Decimal", "", "EndBalance", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(4574), "EndBalanceCredit", new Guid("90147316-52ec-4a27-83df-2c6b09ced6d2"), "number", "money", "Money", null, "Visible" },
                    { 516, true, true, (short)-1, "System.String", "", "", false, false, true, 64, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(4811), "VoucherReference", new Guid("3339cf3e-de2d-4ef6-854b-b95f38da5a89"), "string", "nvarchar", "", null, "AlwaysHidden" },
                    { 547, true, true, (short)3, "System.Decimal", "", "StartBalance", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(5439), "StartBalanceDebit", new Guid("7b018bf8-f19b-4c97-bfb2-ce0a64ee2d9f"), "number", "money", "Money", null, "Visible" },
                    { 548, true, true, (short)4, "System.Decimal", "", "StartBalance", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(5455), "StartBalanceCredit", new Guid("cf578efe-3309-4082-ae46-ded6cd12edd9"), "number", "money", "Money", null, "Visible" },
                    { 549, true, true, (short)5, "System.Decimal", "", "Turnover", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(5471), "TurnoverDebit", new Guid("c2b96b22-60b3-45d8-b833-bac08012229e"), "number", "money", "Money", null, "Visible" },
                    { 550, true, true, (short)6, "System.Decimal", "", "Turnover", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(5488), "TurnoverCredit", new Guid("e85ed0a2-603e-4cf5-8cc6-3e2ae6ba4c0a"), "number", "money", "Money", null, "Visible" },
                    { 551, true, true, (short)7, "System.Decimal", "", "OperationSum", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(5504), "OperationSumDebit", new Guid("10d6d138-18d9-4fae-81fd-2d3ba1bfb718"), "number", "money", "Money", null, "Visible" },
                    { 552, true, true, (short)8, "System.Decimal", "", "OperationSum", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(5520), "OperationSumCredit", new Guid("88354e19-5515-4d22-b98a-3da893bbcef8"), "number", "money", "Money", null, "Visible" },
                    { 553, true, true, (short)9, "System.Decimal", "", "EndBalance", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(5537), "EndBalanceDebit", new Guid("9053bf9c-f4d1-4ef1-bcb7-b0bb01739d75"), "number", "money", "Money", null, "Visible" },
                    { 554, true, true, (short)10, "System.Decimal", "", "EndBalance", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(5558), "EndBalanceCredit", new Guid("29b461b9-2721-4f64-82fd-1ac4c12ad12a"), "number", "money", "Money", null, "Visible" },
                    { 555, true, true, (short)11, "System.String", "", "", false, false, false, 128, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(5574), "BranchName", new Guid("a0ba0f28-97c4-49a4-b56f-96ba5a727728"), "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 556, true, true, (short)-1, "System.String", "", "", false, false, true, 64, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(5686), "VoucherReference", new Guid("2e8bb23f-f3fd-4319-9422-0d50808794e1"), "string", "nvarchar", "", null, "AlwaysHidden" },
                    { 557, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(5704), "RowNo", new Guid("4d5b0f33-6709-49c8-934b-0f4ec734e92e"), "number", "int", "", null, "AlwaysVisible" },
                    { 546, true, true, (short)2, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(5422), "ProjectName", new Guid("4113ea79-eafe-4b71-86c1-332e1e0289e5"), "string", "nvarchar", "", null, "Visible" },
                    { 558, true, true, (short)1, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(5721), "ProjectFullCode", new Guid("63ba5d3f-c6c5-4ba7-97e7-f1381684127f"), "string", "nvarchar", "", null, "Visible" },
                    { 560, true, true, (short)3, "System.Decimal", "", "StartBalance", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(5755), "StartBalanceDebit", new Guid("8a8e5a74-e96b-4a69-b4a9-00cdfa11dc10"), "number", "money", "Money", null, "Visible" },
                    { 561, true, true, (short)4, "System.Decimal", "", "StartBalance", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(5770), "StartBalanceCredit", new Guid("8835dde3-28bb-4dde-b425-e320c2468aa6"), "number", "money", "Money", null, "Visible" },
                    { 562, true, true, (short)5, "System.Decimal", "", "Turnover", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(5795), "TurnoverDebit", new Guid("fbfb6cd3-dcf7-44f9-952a-62056a2e4a02"), "number", "money", "Money", null, "Visible" },
                    { 563, true, true, (short)6, "System.Decimal", "", "Turnover", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(5812), "TurnoverCredit", new Guid("bec296ec-d4dc-4e90-b5da-4ca245ad9401"), "number", "money", "Money", null, "Visible" },
                    { 564, true, true, (short)7, "System.Decimal", "", "OperationSum", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(5827), "OperationSumDebit", new Guid("f07e5b16-7a44-4b90-af82-8faef5a82a1e"), "number", "money", "Money", null, "Visible" },
                    { 565, true, true, (short)8, "System.Decimal", "", "OperationSum", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(5843), "OperationSumCredit", new Guid("7ec726a2-e609-4db0-87bd-d9563fb76404"), "number", "money", "Money", null, "Visible" },
                    { 566, true, true, (short)9, "System.Decimal", "", "Corrections", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(5859), "CorrectionsDebit", new Guid("40daca43-b86e-4b10-be5f-9d7796064b62"), "number", "money", "Money", null, "Visible" },
                    { 567, true, true, (short)10, "System.Decimal", "", "Corrections", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(5876), "CorrectionsCredit", new Guid("1e61a229-182d-4b2b-b41d-6bbecc009ea4"), "number", "money", "Money", null, "Visible" },
                    { 568, true, true, (short)11, "System.Decimal", "", "EndBalance", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(5892), "EndBalanceDebit", new Guid("8969a990-8bd1-40f0-87c2-2efbfbcbecaa"), "number", "money", "Money", null, "Visible" },
                    { 569, true, true, (short)12, "System.Decimal", "", "EndBalance", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(5907), "EndBalanceCredit", new Guid("f2927829-11b4-49f5-97ce-7a696369b350"), "number", "money", "Money", null, "Visible" },
                    { 570, true, true, (short)13, "System.String", "", "", false, false, false, 128, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(5928), "BranchName", new Guid("b9acd3df-b57b-4676-93a6-5347771be275"), "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 559, true, true, (short)2, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(5737), "ProjectName", new Guid("20a373db-d8d4-4436-80a7-f68b5f5a0bb8"), "string", "nvarchar", "", null, "Visible" },
                    { 545, true, true, (short)1, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(5399), "ProjectFullCode", new Guid("a48266f3-8da2-43a5-99ba-8ba9ee6a5b97"), "string", "nvarchar", "", null, "Visible" },
                    { 544, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(5383), "RowNo", new Guid("ae89387c-9702-4e87-9ad3-9e838d3a1cc2"), "number", "int", "", null, "AlwaysVisible" },
                    { 543, true, true, (short)-1, "System.String", "", "", false, false, true, 64, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(5367), "VoucherReference", new Guid("71432be7-64ae-4002-bb08-31fb7316061b"), "string", "nvarchar", "", null, "AlwaysHidden" },
                    { 517, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(4830), "RowNo", new Guid("01960c25-315f-4f14-9073-669a70cfa627"), "number", "int", "", null, "AlwaysVisible" },
                    { 518, true, true, (short)1, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(4845), "ProjectFullCode", new Guid("921439a3-a92c-4997-8c04-fc8fc9e3c366"), "string", "nvarchar", "", null, "Visible" },
                    { 519, true, true, (short)2, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(4863), "ProjectName", new Guid("777905a9-ab6d-4ba8-b0f9-ce8e28307b7e"), "string", "nvarchar", "", null, "Visible" },
                    { 520, true, true, (short)3, "System.Decimal", "", "EndBalance", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(4879), "EndBalanceDebit", new Guid("5dc10bd8-042a-45f3-9051-6999b690706c"), "number", "money", "Money", null, "Visible" },
                    { 521, true, true, (short)4, "System.Decimal", "", "EndBalance", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(4894), "EndBalanceCredit", new Guid("34c64cb1-0ccd-48dd-812e-a17c6846b31f"), "number", "money", "Money", null, "Visible" },
                    { 522, true, true, (short)5, "System.String", "", "", false, false, false, 128, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(4917), "BranchName", new Guid("01a0af4e-ba9b-4b96-8741-ac9bc6d3b432"), "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 524, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(5048), "RowNo", new Guid("0c1db231-5ccb-4a9f-99de-9c1dbb2d97c2"), "number", "int", "", null, "AlwaysVisible" },
                    { 525, true, true, (short)1, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(5063), "ProjectFullCode", new Guid("70190791-f20a-4297-b95f-84a9e5ca7183"), "string", "nvarchar", "", null, "Visible" },
                    { 526, true, true, (short)2, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(5081), "ProjectName", new Guid("9f3cf5e6-2513-4f20-b36d-0142c288215a"), "string", "nvarchar", "", null, "Visible" },
                    { 527, true, true, (short)3, "System.Decimal", "", "Turnover", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(5097), "TurnoverDebit", new Guid("fdbf9ee6-b2c5-4afd-91cd-4b4bf7d6239a"), "number", "money", "Money", null, "Visible" }
                });

            migrationBuilder.InsertData(
                schema: "Metadata",
                table: "Column",
                columns: new[] { "ColumnID", "AllowFiltering", "AllowSorting", "DisplayIndex", "DotNetType", "Expression", "GroupName", "IsDynamic", "IsFixedLength", "IsNullable", "Length", "MinLength", "ModifiedDate", "Name", "rowguid", "ScriptType", "StorageType", "Type", "ViewID", "Visibility" },
                values: new object[,]
                {
                    { 528, true, true, (short)4, "System.Decimal", "", "Turnover", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(5113), "TurnoverCredit", new Guid("72f23b63-094a-4c13-823c-222e0d03acc8"), "number", "money", "Money", null, "Visible" },
                    { 529, true, true, (short)5, "System.Decimal", "", "EndBalance", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(5129), "EndBalanceDebit", new Guid("54fde847-a5a9-4acc-ad74-e11656e20358"), "number", "money", "Money", null, "Visible" },
                    { 530, true, true, (short)6, "System.Decimal", "", "EndBalance", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(5151), "EndBalanceCredit", new Guid("79ddafe5-77c3-4054-a939-b347e11629a4"), "number", "money", "Money", null, "Visible" },
                    { 531, true, true, (short)7, "System.String", "", "", false, false, false, 128, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(5167), "BranchName", new Guid("5490eb45-9838-479b-95b2-8c28cd4baeb0"), "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 532, true, true, (short)-1, "System.String", "", "", false, false, true, 64, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(5183), "VoucherReference", new Guid("ad545442-b13b-414b-ab9e-0b3ab3147660"), "string", "nvarchar", "", null, "AlwaysHidden" },
                    { 533, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(5200), "RowNo", new Guid("d4ee6b39-60c6-4374-9597-c6e045c822d4"), "number", "int", "", null, "AlwaysVisible" },
                    { 534, true, true, (short)1, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(5216), "ProjectFullCode", new Guid("f6c3b262-88fb-46ab-9bd6-5ec592c652ce"), "string", "nvarchar", "", null, "Visible" },
                    { 535, true, true, (short)2, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(5232), "ProjectName", new Guid("fe3af238-d4f7-469e-a43f-e60e9ae61ee9"), "string", "nvarchar", "", null, "Visible" },
                    { 536, true, true, (short)3, "System.Decimal", "", "StartBalance", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(5247), "StartBalanceDebit", new Guid("8afa62fc-d155-4f87-b3cc-b87885d330b2"), "number", "money", "Money", null, "Visible" },
                    { 537, true, true, (short)4, "System.Decimal", "", "StartBalance", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(5263), "StartBalanceCredit", new Guid("e1445d48-a193-4edb-8a57-62ee9bf654c0"), "number", "money", "Money", null, "Visible" },
                    { 538, true, true, (short)5, "System.Decimal", "", "Turnover", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(5285), "TurnoverDebit", new Guid("17c6bbd9-dac1-4162-ae14-8c9d37215f49"), "number", "money", "Money", null, "Visible" },
                    { 539, true, true, (short)6, "System.Decimal", "", "Turnover", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(5301), "TurnoverCredit", new Guid("7daa72f4-d253-4928-8383-0f81b59c0c24"), "number", "money", "Money", null, "Visible" },
                    { 540, true, true, (short)7, "System.Decimal", "", "EndBalance", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(5319), "EndBalanceDebit", new Guid("ae4401ad-cce9-40cf-863f-3e7b3d6dc4a3"), "number", "money", "Money", null, "Visible" },
                    { 541, true, true, (short)8, "System.Decimal", "", "EndBalance", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(5335), "EndBalanceCredit", new Guid("a55ccb0a-93fa-45ef-8b68-d6929176574d"), "number", "money", "Money", null, "Visible" },
                    { 542, true, true, (short)9, "System.String", "", "", false, false, false, 128, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(5351), "BranchName", new Guid("885f3eb8-f384-481f-9fea-7a2f3839b9df"), "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 515, true, true, (short)13, "System.String", "", "", false, false, false, 128, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(4590), "BranchName", new Guid("ffa8e20d-9483-4f2a-beaf-21487e09b0ad"), "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 459, true, true, (short)12, "System.Decimal", "", "EndBalance", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(3541), "EndBalanceCredit", new Guid("e0aa00d7-0163-4d75-852d-156ba4887650"), "number", "money", "Money", null, "Visible" },
                    { 686, true, true, (short)5, "System.String", "", "", false, false, false, 128, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(8194), "BranchName", new Guid("da04ba0b-087a-4b64-b865-e2800d9da91e"), "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 688, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(8326), "TypeId", new Guid("ed8ed8b4-cb4b-464a-bc4d-6042a191e6c5"), "number", "int", "", null, "AlwaysHidden" },
                    { 833, true, true, (short)11, "System.String", "", "", false, false, true, 64, 0, new DateTime(2023, 10, 18, 17, 16, 5, 829, DateTimeKind.Local).AddTicks(1357), "ModifiedByName", new Guid("4c770b70-f637-4a93-9e1f-c0aab8f2c1b2"), "string", "nvarchar", "", null, "Hidden" },
                    { 834, true, true, (short)12, "System.DateTime", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 829, DateTimeKind.Local).AddTicks(1374), "ModifiedDate", new Guid("3db55c91-8cdf-4e15-860d-c14da7c59518"), "Date", "datetime", "Default", null, "Hidden" },
                    { 835, true, true, (short)8, "System.String", "", "", false, false, true, 64, 0, new DateTime(2023, 10, 18, 17, 16, 5, 829, DateTimeKind.Local).AddTicks(1395), "CreatedByName", new Guid("2b7e3fce-9664-41c5-955a-43f7a57cc539"), "string", "nvarchar", "", null, "Hidden" },
                    { 836, true, true, (short)9, "System.DateTime", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 829, DateTimeKind.Local).AddTicks(1411), "CreatedDate", new Guid("131c1ca1-f538-4ba3-9034-090168255776"), "Date", "datetime", "Default", null, "Hidden" },
                    { 837, true, true, (short)10, "System.String", "", "", false, false, true, 64, 0, new DateTime(2023, 10, 18, 17, 16, 5, 829, DateTimeKind.Local).AddTicks(1429), "ModifiedByName", new Guid("9662e408-4ac9-468c-857f-ea8691845d40"), "string", "nvarchar", "", null, "Hidden" },
                    { 838, true, true, (short)11, "System.DateTime", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 829, DateTimeKind.Local).AddTicks(1445), "ModifiedDate", new Guid("94f0fb67-ade2-4ea3-870e-b302bd36b1ac"), "Date", "datetime", "Default", null, "Hidden" },
                    { 839, true, true, (short)9, "System.String", "", "", false, false, true, 64, 0, new DateTime(2023, 10, 18, 17, 16, 5, 829, DateTimeKind.Local).AddTicks(1461), "CreatedByName", new Guid("de31cc6f-017c-49a7-b5a0-6ecc968a21e3"), "string", "nvarchar", "", null, "Hidden" },
                    { 840, true, true, (short)10, "System.DateTime", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 829, DateTimeKind.Local).AddTicks(1478), "CreatedDate", new Guid("44132e82-fcad-4d9a-9784-5f5dc75ae0b0"), "Date", "datetime", "Default", null, "Hidden" },
                    { 841, true, true, (short)11, "System.String", "", "", false, false, true, 64, 0, new DateTime(2023, 10, 18, 17, 16, 5, 829, DateTimeKind.Local).AddTicks(1493), "ModifiedByName", new Guid("cfd52ad8-5db9-4089-8015-418f1b4f16a7"), "string", "nvarchar", "", null, "Hidden" },
                    { 842, true, true, (short)12, "System.DateTime", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 829, DateTimeKind.Local).AddTicks(1510), "ModifiedDate", new Guid("d45302f4-cd28-4df5-90f1-70a10ef53b2c"), "Date", "datetime", "Default", null, "Hidden" },
                    { 843, true, true, (short)5, "System.String", "", "", false, false, true, 64, 0, new DateTime(2023, 10, 18, 17, 16, 5, 829, DateTimeKind.Local).AddTicks(1530), "CreatedByName", new Guid("3a9843db-57ca-4bd9-b7fe-655840c6eb2e"), "string", "nvarchar", "", null, "Hidden" },
                    { 832, true, true, (short)10, "System.DateTime", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 829, DateTimeKind.Local).AddTicks(1341), "CreatedDate", new Guid("6a056c55-ddfc-4833-9010-b2a88a2b7a02"), "Date", "datetime", "Default", null, "Hidden" },
                    { 844, true, true, (short)6, "System.DateTime", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 829, DateTimeKind.Local).AddTicks(1547), "CreatedDate", new Guid("b915daff-d8ad-4a54-9892-85d1fd2e9fed"), "Date", "datetime", "Default", null, "Hidden" },
                    { 846, true, true, (short)8, "System.DateTime", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 829, DateTimeKind.Local).AddTicks(1579), "ModifiedDate", new Guid("fcf0fee0-2622-4e48-a3e1-ba47b4a24329"), "Date", "datetime", "Default", null, "Hidden" },
                    { 847, true, true, (short)5, "System.String", "", "", false, false, true, 64, 0, new DateTime(2023, 10, 18, 17, 16, 5, 829, DateTimeKind.Local).AddTicks(1594), "CreatedByName", new Guid("cb01f695-f429-4e2a-9935-95c7745a6f80"), "string", "nvarchar", "", null, "Hidden" },
                    { 848, true, true, (short)6, "System.DateTime", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 829, DateTimeKind.Local).AddTicks(1611), "CreatedDate", new Guid("0c0e401f-7501-42d1-881e-3d138b4b159c"), "Date", "datetime", "Default", null, "Hidden" },
                    { 849, true, true, (short)7, "System.String", "", "", false, false, true, 64, 0, new DateTime(2023, 10, 18, 17, 16, 5, 829, DateTimeKind.Local).AddTicks(1627), "ModifiedByName", new Guid("a882ead8-78f9-4b92-afc7-a02adcd3d301"), "string", "nvarchar", "", null, "Hidden" },
                    { 850, true, true, (short)8, "System.DateTime", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 829, DateTimeKind.Local).AddTicks(1643), "ModifiedDate", new Guid("b5d2775b-374a-4b61-a9f4-18fbafd9af89"), "Date", "datetime", "Default", null, "Hidden" },
                    { 851, true, true, (short)5, "System.String", "", "", false, false, true, 64, 0, new DateTime(2023, 10, 18, 17, 16, 5, 829, DateTimeKind.Local).AddTicks(1666), "CreatedByName", new Guid("64d63ca3-3321-4e9b-8bf1-5589d6686a30"), "string", "nvarchar", "", null, "Hidden" },
                    { 852, true, true, (short)6, "System.DateTime", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 829, DateTimeKind.Local).AddTicks(1682), "CreatedDate", new Guid("bad96f67-24de-459a-9ce8-e4c440165c4a"), "Date", "datetime", "Default", null, "Hidden" },
                    { 853, true, true, (short)7, "System.String", "", "", false, false, true, 64, 0, new DateTime(2023, 10, 18, 17, 16, 5, 829, DateTimeKind.Local).AddTicks(1795), "ModifiedByName", new Guid("5e8b8f88-840d-485a-8a65-8b9339ee6756"), "string", "nvarchar", "", null, "Hidden" },
                    { 854, true, true, (short)8, "System.DateTime", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 829, DateTimeKind.Local).AddTicks(1813), "ModifiedDate", new Guid("07e7cc6d-1c0e-4b45-bddf-fd3eaa615b2e"), "Date", "datetime", "Default", null, "Hidden" },
                    { 855, true, true, (short)9, "System.String", "", "", false, false, false, 128, 0, new DateTime(2023, 10, 18, 17, 16, 5, 829, DateTimeKind.Local).AddTicks(1829), "SourceAppName", new Guid("88312e82-68a0-4993-b25e-0c5adea8779e"), "string", "nvarchar", "", null, "Hidden" }
                });

            migrationBuilder.InsertData(
                schema: "Metadata",
                table: "Column",
                columns: new[] { "ColumnID", "AllowFiltering", "AllowSorting", "DisplayIndex", "DotNetType", "Expression", "GroupName", "IsDynamic", "IsFixedLength", "IsNullable", "Length", "MinLength", "ModifiedDate", "Name", "rowguid", "ScriptType", "StorageType", "Type", "ViewID", "Visibility" },
                values: new object[,]
                {
                    { 856, true, true, (short)-1, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 829, DateTimeKind.Local).AddTicks(1845), "SourceAppId", new Guid("c56bc2df-2cbc-48eb-aa77-98ad2cb40c94"), "number", "int", "", null, "AlwaysHidden" },
                    { 845, true, true, (short)7, "System.String", "", "", false, false, true, 64, 0, new DateTime(2023, 10, 18, 17, 16, 5, 829, DateTimeKind.Local).AddTicks(1563), "ModifiedByName", new Guid("2609cb6c-e7c8-48be-aa56-7e3324ac2eba"), "string", "nvarchar", "", null, "Hidden" },
                    { 831, true, true, (short)9, "System.String", "", "", false, false, true, 64, 0, new DateTime(2023, 10, 18, 17, 16, 5, 829, DateTimeKind.Local).AddTicks(1326), "CreatedByName", new Guid("7d25049a-7f86-4ba0-b7a0-2ab923c163a2"), "string", "nvarchar", "", null, "Hidden" },
                    { 830, true, true, (short)8, "System.DateTime", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 829, DateTimeKind.Local).AddTicks(1309), "ModifiedDate", new Guid("e1f7bb95-d5d8-4d3f-8bb2-02f72439f8ab"), "Date", "datetime", "Default", null, "Hidden" },
                    { 829, true, true, (short)7, "System.String", "", "", false, false, true, 64, 0, new DateTime(2023, 10, 18, 17, 16, 5, 829, DateTimeKind.Local).AddTicks(1293), "ModifiedByName", new Guid("97440f5f-3b60-4525-981f-37ee96dc03be"), "string", "nvarchar", "", null, "Hidden" },
                    { 804, true, true, (short)-1, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 829, DateTimeKind.Local).AddTicks(773), "DetailAccountId", new Guid("fff47dce-ef52-43c8-b083-73b71d8f884b"), "number", "int", "", null, "AlwaysHidden" },
                    { 805, true, true, (short)-1, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 829, DateTimeKind.Local).AddTicks(790), "CostCenterId", new Guid("7ffa294b-90fc-4645-97c4-fbc51f6ffec9"), "number", "int", "", null, "AlwaysHidden" },
                    { 806, true, true, (short)-1, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 829, DateTimeKind.Local).AddTicks(806), "ProjectId", new Guid("1ac219b4-037d-4498-a1d3-57403553bf72"), "number", "int", "", null, "AlwaysHidden" },
                    { 807, true, true, (short)1, "System.String", "", "", false, false, false, 256, 0, new DateTime(2023, 10, 18, 17, 16, 5, 829, DateTimeKind.Local).AddTicks(822), "FullAccount.Account.FullCode", new Guid("bf7f4f03-3487-4801-82e1-4d30c98188fe"), "string", "nvarchar", "", null, "Visible" },
                    { 808, true, true, (short)2, "System.String", "", "", false, false, false, 256, 0, new DateTime(2023, 10, 18, 17, 16, 5, 829, DateTimeKind.Local).AddTicks(838), "FullAccount.Account.Name", new Guid("6ffc867b-28b6-4940-804e-5c8176edf75b"), "string", "nvarchar", "", null, "Visible" },
                    { 809, true, true, (short)3, "System.String", "", "", false, false, false, 256, 0, new DateTime(2023, 10, 18, 17, 16, 5, 829, DateTimeKind.Local).AddTicks(854), "FullAccount.DetailAccount.FullCode", new Guid("1b959789-f472-461d-8353-9b3456f03c86"), "string", "nvarchar", "", null, "Hidden" },
                    { 810, true, true, (short)4, "System.String", "", "", false, false, false, 256, 0, new DateTime(2023, 10, 18, 17, 16, 5, 829, DateTimeKind.Local).AddTicks(871), "FullAccount.DetailAccount.Name", new Guid("702b5f7d-9d2a-44cc-8811-00d17b97ccc3"), "string", "nvarchar", "", null, "Hidden" },
                    { 811, true, true, (short)5, "System.String", "", "", false, false, false, 256, 0, new DateTime(2023, 10, 18, 17, 16, 5, 829, DateTimeKind.Local).AddTicks(892), "FullAccount.CostCenter.FullCode", new Guid("84a522b7-6f44-43a4-b4e9-81beeee86797"), "string", "nvarchar", "", null, "Hidden" },
                    { 812, true, true, (short)6, "System.String", "", "", false, false, false, 256, 0, new DateTime(2023, 10, 18, 17, 16, 5, 829, DateTimeKind.Local).AddTicks(909), "FullAccount.CostCenter.Name", new Guid("c21e0273-c5fe-4018-a166-e9d8ccfbfd63"), "string", "nvarchar", "", null, "Hidden" },
                    { 813, true, true, (short)7, "System.String", "", "", false, false, false, 256, 0, new DateTime(2023, 10, 18, 17, 16, 5, 829, DateTimeKind.Local).AddTicks(925), "FullAccount.Project.FullCode", new Guid("bfd50b87-4e0d-4909-bacd-1c37de3fb79b"), "string", "nvarchar", "", null, "Hidden" },
                    { 814, true, true, (short)8, "System.String", "", "", false, false, false, 256, 0, new DateTime(2023, 10, 18, 17, 16, 5, 829, DateTimeKind.Local).AddTicks(941), "FullAccount.Project.Name", new Guid("56f8d006-abd6-4516-8b9e-f6c18ceadcb3"), "string", "nvarchar", "", null, "Hidden" },
                    { 815, true, true, (short)9, "System.Decimal", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 829, DateTimeKind.Local).AddTicks(957), "Amount", new Guid("86b4ece7-9194-48b6-9d80-8b86ad10ea5e"), "number", "money", "Money", null, "Visible" },
                    { 816, true, true, (short)12, "System.String", "", "", false, false, true, 512, 0, new DateTime(2023, 10, 18, 17, 16, 5, 829, DateTimeKind.Local).AddTicks(972), "Remarks", new Guid("02bf02d1-baad-4ea8-9d31-6b15485214bc"), "string", "nvarchar", "", null, "Visible" },
                    { 817, true, true, (short)10, "System.String", "", "", false, false, true, 128, 0, new DateTime(2023, 10, 18, 17, 16, 5, 829, DateTimeKind.Local).AddTicks(989), "SourceAppName", new Guid("43da8715-6a2f-47fc-829e-f33c269386ce"), "string", "nvarchar", "", null, "Visible" },
                    { 818, true, true, (short)5, "System.String", "", "", false, false, true, 32, 0, new DateTime(2023, 10, 18, 17, 16, 5, 829, DateTimeKind.Local).AddTicks(1005), "State", new Guid("8ae5e9df-ce1c-453c-8048-42be73a56e62"), "string", "nvarchar", "", null, "Visible" },
                    { 819, true, true, (short)5, "System.String", "", "", false, false, true, 32, 0, new DateTime(2023, 10, 18, 17, 16, 5, 829, DateTimeKind.Local).AddTicks(1026), "State", new Guid("2ac98272-1398-4a28-ac43-431ab8fcbc02"), "string", "nvarchar", "", null, "Visible" },
                    { 820, true, true, (short)5, "System.String", "", "", false, false, true, 32, 0, new DateTime(2023, 10, 18, 17, 16, 5, 829, DateTimeKind.Local).AddTicks(1139), "State", new Guid("1bf1290a-3284-4d26-8538-354e935982ea"), "string", "nvarchar", "", null, "Visible" },
                    { 821, true, true, (short)3, "System.String", "", "", false, false, true, 32, 0, new DateTime(2023, 10, 18, 17, 16, 5, 829, DateTimeKind.Local).AddTicks(1157), "State", new Guid("0d7782cf-2b8b-46cd-980c-c02d594b3671"), "string", "nvarchar", "", null, "Visible" },
                    { 822, true, true, (short)5, "System.String", "", "", false, false, true, 32, 0, new DateTime(2023, 10, 18, 17, 16, 5, 829, DateTimeKind.Local).AddTicks(1172), "State", new Guid("6e903def-265b-489b-b193-ce2dc5fefbd7"), "string", "nvarchar", "", null, "Visible" },
                    { 823, true, true, (short)3, "System.String", "", "", false, false, true, 64, 0, new DateTime(2023, 10, 18, 17, 16, 5, 829, DateTimeKind.Local).AddTicks(1188), "CreatedByName", new Guid("37c624cd-0ffd-4ccf-81ea-c2f8aa783d96"), "string", "nvarchar", "", null, "Hidden" },
                    { 824, true, true, (short)4, "System.DateTime", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 829, DateTimeKind.Local).AddTicks(1206), "CreatedDate", new Guid("011059a5-ab91-4dab-a38f-f9d2403e19c8"), "Date", "datetime", "Default", null, "Hidden" },
                    { 825, true, true, (short)5, "System.String", "", "", false, false, true, 64, 0, new DateTime(2023, 10, 18, 17, 16, 5, 829, DateTimeKind.Local).AddTicks(1222), "ModifiedByName", new Guid("969afbfa-4017-41d2-8b99-39cf53de2180"), "string", "nvarchar", "", null, "Hidden" },
                    { 826, true, true, (short)6, "System.DateTime", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 829, DateTimeKind.Local).AddTicks(1238), "ModifiedDate", new Guid("b16e199c-e3a4-47b7-9c8e-d6b159b729b9"), "Date", "datetime", "Default", null, "Hidden" },
                    { 827, true, true, (short)5, "System.String", "", "", false, false, true, 64, 0, new DateTime(2023, 10, 18, 17, 16, 5, 829, DateTimeKind.Local).AddTicks(1259), "CreatedByName", new Guid("cc529e65-074d-4024-8b81-2ca1ec784fcf"), "string", "nvarchar", "", null, "Hidden" },
                    { 828, true, true, (short)6, "System.DateTime", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 829, DateTimeKind.Local).AddTicks(1275), "CreatedDate", new Guid("98b062c3-b29b-4398-97c3-4f6b5f0533a5"), "Date", "datetime", "Default", null, "Hidden" },
                    { 857, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 829, DateTimeKind.Local).AddTicks(1863), "RowNo", new Guid("90c471fd-9a57-4ef1-9aa0-2eeea3a406ce"), "number", "int", "", null, "AlwaysVisible" },
                    { 803, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 829, DateTimeKind.Local).AddTicks(755), "AccountId", new Guid("b0141c02-e984-4e31-9837-601983333c4a"), "number", "int", "", null, "AlwaysHidden" },
                    { 858, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 829, DateTimeKind.Local).AddTicks(1879), "Id", new Guid("4d8a7862-a751-4101-92bb-71d0acdb347b"), "number", "int", "", null, "AlwaysHidden" },
                    { 860, true, true, (short)2, "System.DateTime", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 829, DateTimeKind.Local).AddTicks(1918), "Date", new Guid("435b8bc4-efa1-4ddc-b6e0-60ab32bf3f6f"), "Date", "datetime", "Default", null, "Visible" },
                    { 100029, true, true, (short)5, "System.String", "", "", false, false, false, 64, 0, new DateTime(2023, 10, 18, 17, 16, 5, 829, DateTimeKind.Local).AddTicks(2518), "Prefix", new Guid("5fcbec51-4bf2-46cc-8809-435a93552db3"), "string", "nvarchar", "", null, "Visible" },
                    { 100030, true, true, (short)6, "System.String", "", "", false, false, false, 64, 0, new DateTime(2023, 10, 18, 17, 16, 5, 829, DateTimeKind.Local).AddTicks(2541), "Suffix", new Guid("44476206-6365-47be-9c27-b8a4a6f4903f"), "string", "nvarchar", "", null, "Visible" },
                    { 100031, true, true, (short)7, "System.Boolean", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 829, DateTimeKind.Local).AddTicks(2557), "IsActive", new Guid("d328d43e-8c9d-46a9-86dd-fad8d60f74aa"), "boolean", "bit", "", null, "Visible" },
                    { 100032, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 829, DateTimeKind.Local).AddTicks(2573), "BranchId", new Guid("0367b5f5-0e2b-4046-89b2-4f5161ad237b"), "number", "int", "", null, "AlwaysHidden" },
                    { 100033, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 829, DateTimeKind.Local).AddTicks(2589), "Id", new Guid("255e7230-4b60-41cc-a135-40c13fc1ba0d"), "number", "int", "", null, "AlwaysHidden" },
                    { 100034, false, false, (short)0, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 829, DateTimeKind.Local).AddTicks(2605), "RowNo", new Guid("0f2dea04-dc3a-45e2-b8f5-49178a83cd32"), "number", "int", "", null, "AlwaysVisible" },
                    { 100035, true, true, (short)1, "System.String", "", "", false, false, false, 64, 0, new DateTime(2023, 10, 18, 17, 16, 5, 829, DateTimeKind.Local).AddTicks(2622), "Name", new Guid("2aff9ea5-ef34-4b5f-b76b-2303953ee670"), "string", "nvarchar", "", null, "Visible" },
                    { 100036, true, true, (short)2, "System.String", "", "", false, false, false, 64, 0, new DateTime(2023, 10, 18, 17, 16, 5, 829, DateTimeKind.Local).AddTicks(2639), "EnName", new Guid("aa8318a3-3535-47bd-a0e0-6f3d225537a5"), "string", "nvarchar", "", null, "Visible" }
                });

            migrationBuilder.InsertData(
                schema: "Metadata",
                table: "Column",
                columns: new[] { "ColumnID", "AllowFiltering", "AllowSorting", "DisplayIndex", "DotNetType", "Expression", "GroupName", "IsDynamic", "IsFixedLength", "IsNullable", "Length", "MinLength", "ModifiedDate", "Name", "rowguid", "ScriptType", "StorageType", "Type", "ViewID", "Visibility" },
                values: new object[,]
                {
                    { 100037, true, true, (short)3, "System.String", "", "", false, false, false, 1024, 0, new DateTime(2023, 10, 18, 17, 16, 5, 829, DateTimeKind.Local).AddTicks(2654), "Description", new Guid("55e8e7a2-073f-4c5c-ab76-5c722933bca1"), "string", "nvarchar", "", null, "Visible" },
                    { 100038, true, true, (short)4, "System.Int16", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 829, DateTimeKind.Local).AddTicks(2675), "Type", new Guid("5c4e7d16-f1a9-45ff-bbdd-78d449f58c24"), "number", "smallint", "", null, "Visible" },
                    { 100039, true, true, (short)5, "System.Boolean", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 829, DateTimeKind.Local).AddTicks(2691), "IsActive", new Guid("fab0d104-9c63-4689-b7e4-adb591e9e06b"), "boolean", "bit", "", null, "Visible" },
                    { 100028, true, true, (short)4, "System.Int16", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 829, DateTimeKind.Local).AddTicks(2503), "Type", new Guid("ba7368e5-2de1-4ef8-987d-7ee40be414b4"), "number", "smallint", "", null, "Visible" },
                    { 100040, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 829, DateTimeKind.Local).AddTicks(2707), "BranchId", new Guid("bd674f8d-dfcf-4e16-92d7-bb13180df87b"), "number", "int", "", null, "AlwaysHidden" },
                    { 100042, true, true, (short)7, "System.String", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 829, DateTimeKind.Local).AddTicks(2739), "RowGuid", new Guid("5f14a11e-ca52-4956-81ce-d34942856f96"), "string", "nvarchar", "", null, "AlwaysHidden" },
                    { 100043, true, true, (short)8, "System.DateTime", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 829, DateTimeKind.Local).AddTicks(2755), "ModifiedDate", new Guid("d2c15507-b6b6-4f45-8212-629a908b4907"), "Date", "datetime", "Default", null, "Hidden" },
                    { 100044, false, false, (short)0, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 829, DateTimeKind.Local).AddTicks(2773), "RowNo", new Guid("b285a197-10b9-422d-bb8a-03d914003abe"), "number", "int", "", null, "AlwaysVisible" },
                    { 100045, true, true, (short)1, "System.String", "", "", false, false, false, 2048, 0, new DateTime(2023, 10, 18, 17, 16, 5, 829, DateTimeKind.Local).AddTicks(2789), "Name", new Guid("a9c4499c-e701-48bd-97cb-59113a1bfdee"), "string", "nvarchar", "", null, "Visible" },
                    { 100046, true, true, (short)2, "System.String", "", "", false, false, false, 2048, 0, new DateTime(2023, 10, 18, 17, 16, 5, 829, DateTimeKind.Local).AddTicks(2810), "UniqeName", new Guid("7b15037e-4652-4c92-b2d8-8f8557e6c5e5"), "string", "nvarchar", "", null, "Visible" },
                    { 100047, true, true, (short)3, "System.String", "", "", false, false, false, 2048, 0, new DateTime(2023, 10, 18, 17, 16, 5, 829, DateTimeKind.Local).AddTicks(2830), "Description", new Guid("b410e16f-9589-44ae-a49c-1009e088a8af"), "string", "nvarchar", "", null, "Visible" },
                    { 100048, true, true, (short)4, "System.Boolean", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 829, DateTimeKind.Local).AddTicks(2847), "IsActive", new Guid("bc8fa215-8eff-430b-a525-5027f41463f2"), "boolean", "bit", "", null, "Visible" },
                    { 100049, true, true, (short)5, "Microsoft.AspNetCore.Http.IFormFile", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 829, DateTimeKind.Local).AddTicks(2862), "FormFile", new Guid("d2761539-5194-4118-af9c-c8705976f9fd"), "", "", "", null, "Visible" },
                    { 100050, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 829, DateTimeKind.Local).AddTicks(2879), "Id", new Guid("974bab47-9ce8-4d84-a4a6-ebc8d666e97d"), "number", "int", "", null, "AlwaysHidden" },
                    { 100051, true, true, (short)6, "System.Guid", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 829, DateTimeKind.Local).AddTicks(2894), "RowGuid", new Guid("65885bae-eef2-4bcb-b9d0-41a1bf4239f8"), "", "", "", null, "AlwaysHidden" },
                    { 100052, true, true, (short)7, "System.DateTime", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 829, DateTimeKind.Local).AddTicks(2911), "ModifiedDate", new Guid("493a7ef9-0987-48ab-8100-d95e4077460b"), "Date", "datetime", "Default", null, "Hidden" },
                    { 100041, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 829, DateTimeKind.Local).AddTicks(2723), "Id", new Guid("fd621130-afa1-4ed8-aa70-faf005780b45"), "number", "int", "", null, "AlwaysHidden" },
                    { 100027, true, true, (short)3, "System.String", "", "", false, false, false, 1024, 0, new DateTime(2023, 10, 18, 17, 16, 5, 829, DateTimeKind.Local).AddTicks(2487), "Description", new Guid("ff0319af-a4d8-411f-9f28-39389de07fa5"), "string", "nvarchar", "", null, "Visible" },
                    { 100026, true, true, (short)2, "System.String", "", "", false, false, false, 64, 0, new DateTime(2023, 10, 18, 17, 16, 5, 829, DateTimeKind.Local).AddTicks(2471), "EnName", new Guid("f36a15b3-1b04-43fe-b829-3d62f963e3a2"), "string", "nvarchar", "", null, "Visible" },
                    { 100025, true, true, (short)1, "System.String", "", "", false, false, false, 64, 0, new DateTime(2023, 10, 18, 17, 16, 5, 829, DateTimeKind.Local).AddTicks(2454), "Name", new Guid("df6d22bf-6a2e-4516-a14a-f3c9eeb541f1"), "string", "nvarchar", "", null, "Visible" },
                    { 861, true, true, (short)3, "System.String", "", "", false, false, true, 512, 0, new DateTime(2023, 10, 18, 17, 16, 5, 829, DateTimeKind.Local).AddTicks(1936), "Description", new Guid("27dddc6b-c297-46c7-a115-16444a2452bd"), "string", "nvarchar", "", null, "Visible" },
                    { 100001, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 829, DateTimeKind.Local).AddTicks(1951), "RowNo", new Guid("9f69d7d2-93f2-4f93-baf8-fb8b86bd8394"), "number", "int", "", null, "AlwaysVisible" },
                    { 100002, true, true, (short)1, "System.String", "", "", false, false, false, 64, 0, new DateTime(2023, 10, 18, 17, 16, 5, 829, DateTimeKind.Local).AddTicks(1967), "Name", new Guid("2a95d16c-aa64-43e2-8923-f4b608f35024"), "string", "nvarchar", "", null, "Visible" },
                    { 100003, true, true, (short)2, "System.String", "", "", false, false, false, 64, 0, new DateTime(2023, 10, 18, 17, 16, 5, 829, DateTimeKind.Local).AddTicks(1985), "EnName", new Guid("0877b02e-29de-4a30-880b-a98b637a3565"), "string", "nvarchar", "", null, "Visible" },
                    { 100004, true, true, (short)3, "System.String", "", "", false, false, false, 1024, 0, new DateTime(2023, 10, 18, 17, 16, 5, 829, DateTimeKind.Local).AddTicks(2002), "Description", new Guid("a61fbfbc-6ad1-41d4-8ef8-577c4466fdbe"), "string", "nvarchar", "", null, "Visible" },
                    { 100005, true, true, (short)4, "System.String", "", "", false, false, false, 64, 0, new DateTime(2023, 10, 18, 17, 16, 5, 829, DateTimeKind.Local).AddTicks(2021), "SocialLink", new Guid("b1a8cd77-0f35-4491-81aa-c338fbfb83c7"), "string", "nvarchar", "", null, "Visible" },
                    { 100006, true, true, (short)5, "System.String", "", "", false, false, false, 64, 0, new DateTime(2023, 10, 18, 17, 16, 5, 829, DateTimeKind.Local).AddTicks(2042), "Website", new Guid("6c3a889f-6273-4a87-97dc-b7872550d30f"), "string", "nvarchar", "", null, "Visible" },
                    { 100007, true, true, (short)6, "System.String", "", "", false, false, false, 64, 0, new DateTime(2023, 10, 18, 17, 16, 5, 829, DateTimeKind.Local).AddTicks(2058), "MetaKeyword", new Guid("f7db0bc2-c006-4b80-860e-7a35c0687b58"), "string", "nvarchar", "", null, "Visible" },
                    { 100008, true, true, (short)7, "System.Boolean", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 829, DateTimeKind.Local).AddTicks(2074), "IsActive", new Guid("d9387b01-fff3-406b-8bee-3e86be57acdf"), "boolean", "bit", "", null, "Visible" },
                    { 100009, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 829, DateTimeKind.Local).AddTicks(2091), "BranchId", new Guid("47ac9284-cccd-469f-bee1-44f186e2e3f6"), "number", "int", "", null, "AlwaysHidden" },
                    { 100010, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 829, DateTimeKind.Local).AddTicks(2108), "Id", new Guid("1f636310-720a-4cc4-9a37-ea7d4077f7f4"), "number", "int", "", null, "AlwaysHidden" },
                    { 100011, true, true, (short)9, "System.Guid", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 829, DateTimeKind.Local).AddTicks(2124), "RowGuid", new Guid("ce6b2647-52c9-44d3-beeb-b04679d09a27"), "", "", "", null, "AlwaysHidden" },
                    { 100012, true, true, (short)10, "System.DateTime", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 829, DateTimeKind.Local).AddTicks(2139), "ModifiedDate", new Guid("49eef268-9fab-430f-bf21-84091f78b384"), "Date", "datetime", "Default", null, "Hidden" },
                    { 100013, false, false, (short)0, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 829, DateTimeKind.Local).AddTicks(2155), "RowNo", new Guid("8d78f676-4b55-4191-9145-2811c136927b"), "number", "int", "", null, "AlwaysVisible" },
                    { 100014, true, true, (short)1, "System.String", "", "", false, false, false, 64, 0, new DateTime(2023, 10, 18, 17, 16, 5, 829, DateTimeKind.Local).AddTicks(2176), "Name", new Guid("191862be-a5ef-45a3-833b-78b4ae35507b"), "string", "nvarchar", "", null, "Visible" },
                    { 100015, true, true, (short)2, "System.String", "", "", false, false, false, 64, 0, new DateTime(2023, 10, 18, 17, 16, 5, 829, DateTimeKind.Local).AddTicks(2192), "EnName", new Guid("359498ae-cc20-4c72-85d6-08843c41b40f"), "string", "nvarchar", "", null, "Visible" },
                    { 100016, true, true, (short)3, "System.String", "", "", false, false, false, 64, 0, new DateTime(2023, 10, 18, 17, 16, 5, 829, DateTimeKind.Local).AddTicks(2208), "Description", new Guid("0255b114-8d9b-4449-aa2e-8e9580b3ba56"), "string", "nvarchar", "", null, "Visible" },
                    { 100017, true, true, (short)4, "System.String", "", "", false, false, false, 64, 0, new DateTime(2023, 10, 18, 17, 16, 5, 829, DateTimeKind.Local).AddTicks(2225), "Symbol", new Guid("307e2e37-7ea6-4ee6-bfe3-0c89a7e18172"), "string", "nvarchar", "", null, "Visible" },
                    { 100018, true, true, (short)5, "System.Int16", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 829, DateTimeKind.Local).AddTicks(2241), "Status", new Guid("3a4aecfb-a700-4cda-a185-b0a661e09f58"), "number", "smallint", "", null, "Visible" },
                    { 100019, true, true, (short)6, "System.Boolean", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 829, DateTimeKind.Local).AddTicks(2257), "IsActive", new Guid("efcc3e0a-f475-4600-8646-14c311c75b70"), "boolean", "bit", "", null, "Visible" },
                    { 100020, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 829, DateTimeKind.Local).AddTicks(2273), "BranchId", new Guid("30ec6e2e-3b2f-465e-bd4e-9284d3807dda"), "number", "int", "", null, "AlwaysHidden" },
                    { 100021, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 829, DateTimeKind.Local).AddTicks(2289), "Id", new Guid("60682036-f891-4e1a-bffc-981363e1a524"), "number", "int", "", null, "AlwaysHidden" }
                });

            migrationBuilder.InsertData(
                schema: "Metadata",
                table: "Column",
                columns: new[] { "ColumnID", "AllowFiltering", "AllowSorting", "DisplayIndex", "DotNetType", "Expression", "GroupName", "IsDynamic", "IsFixedLength", "IsNullable", "Length", "MinLength", "ModifiedDate", "Name", "rowguid", "ScriptType", "StorageType", "Type", "ViewID", "Visibility" },
                values: new object[,]
                {
                    { 100022, true, true, (short)8, "System.Guid", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 829, DateTimeKind.Local).AddTicks(2311), "RowGuid", new Guid("3a70b3d2-1a3c-4d63-b15b-7101d5d5bbb5"), "Date", "datetime", "", null, "AlwaysHidden" },
                    { 100023, true, true, (short)9, "System.DateTime", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 829, DateTimeKind.Local).AddTicks(2326), "ModifiedDate", new Guid("2fad9da9-7716-4bce-9630-291b46e6fa23"), "Date", "datetime", "Default", null, "Hidden" },
                    { 100024, false, false, (short)0, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 829, DateTimeKind.Local).AddTicks(2342), "RowNo", new Guid("34f59737-3f5d-4e5a-ad72-08da9702dcbb"), "number", "int", "", null, "AlwaysVisible" },
                    { 859, true, true, (short)1, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 829, DateTimeKind.Local).AddTicks(1902), "No", new Guid("d8ac81a9-50b2-47d9-a618-845c2d1309f9"), "number", "int", "", null, "Visible" },
                    { 687, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(8305), "Id", new Guid("0cb7225a-b38c-49ca-a98e-e3314350d917"), "number", "int", "", null, "AlwaysHidden" },
                    { 802, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 829, DateTimeKind.Local).AddTicks(734), "Id", new Guid("7175b79e-7e41-4b94-8c4d-87eb6123e248"), "number", "int", "", null, "AlwaysHidden" },
                    { 800, true, true, (short)-1, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 829, DateTimeKind.Local).AddTicks(702), "SourceAppId", new Guid("77780363-e564-42db-8cbe-9f8292a9e49f"), "number", "int", "", null, "AlwaysHidden" },
                    { 718, true, true, (short)7, "System.Boolean", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(8837), "IsArchived", new Guid("8e49e874-0064-4091-a4cb-b70fed806a9e"), "boolean", "bit", "", null, "Visible" },
                    { 719, true, true, (short)8, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(8854), "PageCount", new Guid("e853fa4b-373b-4c2d-8ee1-de1b8c1e913f"), "number", "int", "", null, "Hidden" },
                    { 720, true, true, (short)-1, "System.Object", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(9135), "FullAccount", new Guid("87988399-6023-4675-a9cb-7a9a049ca04d"), "object", "(n/a)", "", null, "AlwaysHidden" },
                    { 721, true, true, (short)9, "System.String", "", "", false, false, false, 16, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(9159), "SayyadNo", new Guid("bec87c0d-a289-4983-ac19-c3549e179c78"), "string", "nvarchar", "", null, "Visible" },
                    { 723, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(9182), "BranchId", new Guid("6f6ee26a-3809-481c-81ba-1f9d964b5a8c"), "number", "int", "", null, "AlwaysHidden" },
                    { 724, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(9198), "Id", new Guid("aa711120-ffa6-4d41-bcfb-2e2c4c77ca02"), "number", "int", "", null, "AlwaysHidden" },
                    { 725, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(9214), "RowNo", new Guid("558750d0-072a-4420-847c-9679bcde387f"), "number", "int", "", null, "AlwaysVisible" },
                    { 726, true, true, (short)1, "System.Int64", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(9230), "TextNo", new Guid("e4be5715-fcbe-4afd-adf3-87ea68bbf48d"), "number", "bigint", "", null, "Visible" },
                    { 727, true, true, (short)6, "System.String", "", "", false, false, false, 64, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(9246), "Name", new Guid("aa27e5da-2729-46e8-ad26-5e805e143c21"), "string", "nvarchar", "", null, "Visible" },
                    { 728, true, true, (short)7, "System.String", "", "", false, false, false, 32, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(9263), "BankName", new Guid("4e015b8f-44ad-496f-9446-5349a220c449"), "string", "nvarchar", "", null, "Visible" },
                    { 729, true, true, (short)8, "System.DateTime", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(9279), "IssueDate", new Guid("aea4b89d-c3e4-4b9e-bd53-941dae060bbd"), "Date", "datetime", "Default", null, "Visible" },
                    { 717, true, true, (short)6, "System.String", "", "", false, false, true, 32, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(8821), "EndNo", new Guid("e084eae9-c508-45ee-b4a8-ee5f0265b612"), "string", "nvarchar", "", null, "Visible" },
                    { 730, true, true, (short)9, "System.String", "", "", false, false, false, 32, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(9296), "StartNo", new Guid("4629e535-26bf-47a2-a25f-85c619599aa7"), "string", "nvarchar", "", null, "Visible" },
                    { 732, true, true, (short)11, "System.String", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(9332), "AccountCode", new Guid("71ea4b58-c312-461b-b0d2-d446ccc0d76b"), "string", "nvarchar", "", null, "Visible" },
                    { 733, true, true, (short)12, "System.String", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(9348), "AccountName", new Guid("237841e0-2a19-47a3-a2a6-17fe3c12cbcd"), "string", "nvarchar", "", null, "Visible" },
                    { 734, true, true, (short)13, "System.String", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(9363), "DetailAccountName", new Guid("66076473-b0e1-44ef-98a9-44d2e9319719"), "string", "nvarchar", "", null, "Visible" },
                    { 735, true, true, (short)14, "System.String", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(9379), "CostCenterName", new Guid("5342d116-dcee-481a-b65e-1afe296bcf80"), "string", "nvarchar", "", null, "Visible" },
                    { 736, true, true, (short)15, "System.String", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(9397), "ProjectName", new Guid("82a97cb2-93ab-4358-94ab-ea741896d4a3"), "string", "nvarchar", "", null, "Visible" },
                    { 737, true, true, (short)16, "System.String", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(9413), "IsArchivedName", new Guid("6db601ef-b344-4470-8dad-69ad67549b5a"), "string", "nvarchar", "", null, "Visible" },
                    { 738, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(9430), "Id", new Guid("a1556b6d-9b50-4df3-8093-d03e21c058ed"), "number", "int", "", null, "AlwaysHidden" },
                    { 739, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(9451), "RowNo", new Guid("dced5b58-fd75-4d01-9249-8a3714b2f222"), "number", "int", "", null, "AlwaysVisible" },
                    { 740, true, true, (short)1, "System.String", "", "", false, false, false, 64, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(9467), "Code", new Guid("8613df51-acb9-45e4-afb8-c71bd9882e51"), "string", "nvarchar", "", null, "Visible" },
                    { 741, true, true, (short)2, "System.String", "", "", false, false, false, 256, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(9484), "Name", new Guid("e94c27e3-f794-4b8b-9b74-b943bddf91f3"), "string", "nvarchar", "", null, "Visible" },
                    { 742, true, true, (short)-1, "System.Int16", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(9500), "Type", new Guid("3155a0e7-986e-46d6-8ca3-ac490a89ff51"), "number", "smallint", "", null, "AlwaysHidden" },
                    { 731, true, true, (short)10, "System.String", "", "", false, false, false, 32, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(9316), "EndNo", new Guid("a9bca81a-93d1-42fa-8b1d-8ec6c0c503be"), "string", "nvarchar", "", null, "Visible" },
                    { 716, true, true, (short)5, "System.String", "", "", false, false, false, 32, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(8805), "StartNo", new Guid("72d588e9-c90c-4653-84f5-15a021471d02"), "string", "nvarchar", "", null, "Visible" },
                    { 715, true, true, (short)4, "System.DateTime", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(8789), "IssueDate", new Guid("5d85761d-f0b9-4bae-a731-a7a1cd79e62d"), "Date", "datetime", "Default", null, "Visible" },
                    { 714, true, true, (short)3, "System.String", "", "", false, false, true, 32, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(8772), "BankName", new Guid("2e18e3a8-5576-49a1-bf79-037131d80f10"), "string", "nvarchar", "", null, "Visible" },
                    { 689, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(8342), "FunctionId", new Guid("c6bce0d1-6453-4a8b-8a26-250b3efee002"), "number", "int", "", null, "AlwaysHidden" },
                    { 690, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(8363), "CreatedById", new Guid("c6f68965-8b85-43ea-bfa3-8d6fd2aa5081"), "number", "int", "", null, "AlwaysHidden" },
                    { 691, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(8379), "RowNo", new Guid("a9b35b14-a071-4254-9fb2-aa879a7e1eb3"), "number", "int", "", null, "AlwaysVisible" },
                    { 692, true, true, (short)1, "System.String", "", "", false, false, false, 128, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(8396), "Title", new Guid("58d4d630-bd2a-456f-a392-c4a644343181"), "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 693, true, true, (short)2, "System.String", "", "", false, false, false, 64, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(8413), "TypeName", new Guid("d82a2123-62f1-48d1-9d5f-60a3709636e5"), "string", "nvarchar", "", null, "Visible" },
                    { 694, true, true, (short)3, "System.String", "", "", false, false, false, 64, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(8429), "FunctionName", new Guid("8d8a4f25-0b20-43f7-be6d-0c24daa34986"), "string", "nvarchar", "", null, "Visible" },
                    { 695, true, true, (short)4, "System.String", "", "", false, false, false, 128, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(8447), "CreatedByFullName", new Guid("5126064a-3448-47ed-918d-b1c1b35fba4c"), "string", "nvarchar", "", null, "Visible" }
                });

            migrationBuilder.InsertData(
                schema: "Metadata",
                table: "Column",
                columns: new[] { "ColumnID", "AllowFiltering", "AllowSorting", "DisplayIndex", "DotNetType", "Expression", "GroupName", "IsDynamic", "IsFixedLength", "IsNullable", "Length", "MinLength", "ModifiedDate", "Name", "rowguid", "ScriptType", "StorageType", "Type", "ViewID", "Visibility" },
                values: new object[,]
                {
                    { 696, true, true, (short)5, "System.String", "", "", false, false, true, 512, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(8463), "Description", new Guid("04823d6b-3270-4b6d-85a2-333bf7000d31"), "string", "nvarchar", "", null, "Visible" },
                    { 697, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(8479), "RowNo", new Guid("d37e2994-32c0-49cc-b0a4-47bc636e7d06"), "number", "int", "", null, "AlwaysVisible" },
                    { 698, true, true, (short)1, "System.String", "", "", false, false, false, 64, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(8501), "SerialNo", new Guid("04692a1e-105e-4629-aae8-c8d90a686fa6"), "string", "nvarchar", "", null, "Visible" },
                    { 699, true, true, (short)2, "System.String", "", "", false, false, false, 32, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(8517), "StatusName", new Guid("dc79558d-3ba0-4f1f-8ab8-4778ccd6681d"), "string", "nvarchar", "", null, "Visible" },
                    { 700, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(8533), "Id", new Guid("6b292bf8-8802-4989-b5b0-fdc393e8e548"), "number", "int", "", null, "AlwaysHidden" },
                    { 701, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(8549), "CheckBookPageID", new Guid("3ad6625d-1db0-4740-bd7a-020cb90a4f7a"), "number", "int", "", null, "AlwaysHidden" },
                    { 702, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(8567), "CheckBookID", new Guid("9cc718af-f682-48e0-9259-255e9f0aafba"), "number", "int", "", null, "AlwaysHidden" },
                    { 703, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(8583), "CheckID", new Guid("3a9ac265-15c5-4b7a-8868-2d37ce868bd1"), "number", "int", "", null, "AlwaysHidden" },
                    { 704, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(8599), "RowNo", new Guid("18e63554-fdd9-4390-9ae0-3b0e7b9a8786"), "number", "int", "", null, "AlwaysVisible" },
                    { 705, true, true, (short)1, "System.String", "", "", false, false, false, 256, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(8615), "Name", new Guid("2af81c3b-e86a-4155-8a95-d0c62cfd788e"), "string", "nvarchar", "", null, "Visible" },
                    { 706, true, true, (short)2, "System.String", "", "", false, false, true, 256, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(8637), "Description", new Guid("d7c977ed-f7b1-4f54-b9cc-dd462df02c69"), "string", "nvarchar", "", null, "Visible" },
                    { 707, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(8652), "FiscalPeriodId", new Guid("3e6a0570-8984-4e47-b02b-c4eae74b838b"), "number", "int", "", null, "AlwaysHidden" },
                    { 708, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(8671), "BranchId", new Guid("2e70defa-e244-444c-a75f-c2c8083c14e0"), "number", "int", "", null, "AlwaysHidden" },
                    { 709, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(8686), "Id", new Guid("d4f5e1da-0b72-457b-8c50-797867a528ac"), "number", "int", "", null, "AlwaysHidden" },
                    { 710, true, true, (short)-1, "System.Int16", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(8702), "BranchScope", new Guid("25d0d901-c5ea-400d-879d-8f0dd9099e2c"), "number", "smallint", "", null, "AlwaysHidden" },
                    { 711, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(8718), "RowNo", new Guid("e1d2270a-9b2c-4cb3-8633-b1dbdd578e49"), "number", "int", "", null, "AlwaysVisible" },
                    { 712, true, true, (short)1, "System.String", "", "", false, false, false, 64, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(8734), "Name", new Guid("d3935a01-d155-41a6-989e-6a9fb8948758"), "string", "nvarchar", "", null, "Visible" },
                    { 713, true, true, (short)2, "System.Int64", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(8750), "TextNo", new Guid("d0fcf507-cf79-4834-8ebe-05997f998ac0"), "number", "bigint", "", null, "Visible" },
                    { 743, true, true, (short)4, "System.String", "", "", false, false, true, 512, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(9519), "Description", new Guid("fab58e92-f844-42da-916e-c7c34e4daa9c"), "string", "nvarchar", "", null, "Visible" },
                    { 801, true, true, (short)11, "System.String", "", "", false, false, true, 64, 0, new DateTime(2023, 10, 18, 17, 16, 5, 829, DateTimeKind.Local).AddTicks(718), "BankOrderNo", new Guid("861832c7-5167-4363-b1ad-5b8b9fb34668"), "string", "nvarchar", "", null, "Visible" },
                    { 744, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(9534), "FiscalPeriodId", new Guid("d53468bf-a317-4d7b-8a37-34f05efeadfc"), "number", "int", "", null, "AlwaysHidden" },
                    { 746, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(9566), "Id", new Guid("917b90c4-4689-4be8-90c7-f10faf3e16d6"), "number", "int", "", null, "AlwaysHidden" },
                    { 776, true, true, (short)-1, "System.Boolean", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 829, DateTimeKind.Local).AddTicks(151), "IsConfirmed", new Guid("366432c5-ba89-4bb1-96ab-b1bded0b6c1c"), "Boolean", "bit", "", null, "Visible" },
                    { 777, true, true, (short)-1, "System.Boolean", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 829, DateTimeKind.Local).AddTicks(172), "IsApproved", new Guid("56a0f25d-2a9f-4b5a-9611-c2f6c4cbe153"), "Boolean", "bit", "", null, "Visible" },
                    { 778, true, true, (short)1, "System.Int64", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 829, DateTimeKind.Local).AddTicks(187), "TextNo", new Guid("7868e543-7ac2-44d4-b078-7465e043ae8a"), "number", "bigint", "", null, "Visible" },
                    { 779, true, true, (short)2, "System.String", "", "", false, false, true, 64, 0, new DateTime(2023, 10, 18, 17, 16, 5, 829, DateTimeKind.Local).AddTicks(209), "Reference", new Guid("8e700470-0b31-4931-9193-5c7cfc2f0955"), "string", "nvarchar", "", null, "Visible" },
                    { 780, false, false, (short)0, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 829, DateTimeKind.Local).AddTicks(225), "RowNo", new Guid("11420822-3eea-4914-892f-6d263457bc37"), "number", "int", "", null, "AlwaysVisible" },
                    { 781, true, true, (short)-1, "System.Object", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 829, DateTimeKind.Local).AddTicks(241), "FullAccount", new Guid("441cfa61-8c5b-4bda-9e23-f0bbbb3eb50a"), "object", "(n/a)", "", null, "AlwaysHidden" },
                    { 782, true, true, (short)1, "System.String", "", "", false, false, false, 256, 0, new DateTime(2023, 10, 18, 17, 16, 5, 829, DateTimeKind.Local).AddTicks(257), "FullAccount.Account.FullCode", new Guid("18255bbf-7361-4981-8de6-d2501d562002"), "string", "nvarchar", "", null, "Visible" },
                    { 783, true, true, (short)2, "System.String", "", "", false, false, false, 256, 0, new DateTime(2023, 10, 18, 17, 16, 5, 829, DateTimeKind.Local).AddTicks(274), "FullAccount.Account.Name", new Guid("44680436-adf4-4004-a1ae-d9718e4768d5"), "string", "nvarchar", "", null, "Visible" },
                    { 784, true, true, (short)3, "System.String", "", "", false, false, false, 256, 0, new DateTime(2023, 10, 18, 17, 16, 5, 829, DateTimeKind.Local).AddTicks(289), "FullAccount.DetailAccount.FullCode", new Guid("53fa7832-0569-4726-b446-b4719bafc35f"), "string", "nvarchar", "", null, "Hidden" },
                    { 785, true, true, (short)4, "System.String", "", "", false, false, false, 256, 0, new DateTime(2023, 10, 18, 17, 16, 5, 829, DateTimeKind.Local).AddTicks(305), "FullAccount.DetailAccount.Name", new Guid("ce6f815e-7c02-441d-b35b-eb3398442bb1"), "string", "nvarchar", "", null, "Hidden" },
                    { 786, true, true, (short)5, "System.String", "", "", false, false, false, 256, 0, new DateTime(2023, 10, 18, 17, 16, 5, 829, DateTimeKind.Local).AddTicks(320), "FullAccount.CostCenter.FullCode", new Guid("aa209536-5c2f-4d04-a973-61b94b953bb4"), "string", "nvarchar", "", null, "Hidden" },
                    { 775, true, true, (short)-1, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 829, DateTimeKind.Local).AddTicks(136), "CurrencyId", new Guid("33b4f091-2f26-4c83-8c81-86c47b68e325"), "number", "int", "", null, "Visible" },
                    { 787, true, true, (short)6, "System.String", "", "", false, false, false, 256, 0, new DateTime(2023, 10, 18, 17, 16, 5, 829, DateTimeKind.Local).AddTicks(483), "FullAccount.CostCenter.Name", new Guid("c8c28e4b-f7a2-4d8b-9aa4-4779f261adb5"), "string", "nvarchar", "", null, "Hidden" },
                    { 789, true, true, (short)8, "System.String", "", "", false, false, false, 256, 0, new DateTime(2023, 10, 18, 17, 16, 5, 829, DateTimeKind.Local).AddTicks(519), "FullAccount.Project.Name", new Guid("99dec710-9d28-451f-b93d-e519e61affef"), "string", "nvarchar", "", null, "Hidden" },
                    { 790, true, true, (short)2, "System.String", "", "", false, false, false, 16, 0, new DateTime(2023, 10, 18, 17, 16, 5, 829, DateTimeKind.Local).AddTicks(537), "SayyadStartNo", new Guid("f4b6785b-b4d3-4a14-bdb7-0ebbffc50cc8"), "string", "nvarchar", "", null, "Visible" },
                    { 791, true, true, (short)3, "System.String", "", "", false, false, false, 32, 0, new DateTime(2023, 10, 18, 17, 16, 5, 829, DateTimeKind.Local).AddTicks(553), "SeriesNo", new Guid("370f9482-b609-4e09-8462-4b9943f773cf"), "string", "nvarchar", "", null, "Visible" },
                    { 792, true, true, (short)9, "System.Decimal", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 829, DateTimeKind.Local).AddTicks(569), "Amount", new Guid("38bca9d5-f86a-48d4-b0a6-968af27978d0"), "number", "money", "Money", null, "Visible" },
                    { 793, true, true, (short)10, "System.String", "", "", false, false, true, 512, 0, new DateTime(2023, 10, 18, 17, 16, 5, 829, DateTimeKind.Local).AddTicks(584), "Remarks", new Guid("9f23ca8f-1097-431f-8e8f-3ec9aae9a03c"), "string", "nvarchar", "", null, "Visible" },
                    { 794, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 829, DateTimeKind.Local).AddTicks(600), "PayReceiveId", new Guid("7d08adf2-d3b5-4a8e-9b92-d12e7b1344e0"), "number", "int", "", null, "AlwaysHidden" },
                    { 795, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 829, DateTimeKind.Local).AddTicks(622), "Id", new Guid("7e65f3c9-d3ef-4a4a-bccf-f9daac48a89f"), "number", "int", "", null, "AlwaysHidden" }
                });

            migrationBuilder.InsertData(
                schema: "Metadata",
                table: "Column",
                columns: new[] { "ColumnID", "AllowFiltering", "AllowSorting", "DisplayIndex", "DotNetType", "Expression", "GroupName", "IsDynamic", "IsFixedLength", "IsNullable", "Length", "MinLength", "ModifiedDate", "Name", "rowguid", "ScriptType", "StorageType", "Type", "ViewID", "Visibility" },
                values: new object[,]
                {
                    { 796, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 829, DateTimeKind.Local).AddTicks(638), "RowNo", new Guid("d410dfaa-42b6-466f-963a-5b650d2ae3e2"), "number", "int", "", null, "AlwaysVisible" },
                    { 797, true, true, (short)-1, "System.Object", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 829, DateTimeKind.Local).AddTicks(655), "FullAccount", new Guid("329378f4-52ad-44f9-9f8d-8ef884d35db3"), "Object", "int", "", null, "AlwaysHidden" },
                    { 798, true, true, (short)-1, "System.Boolean", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 829, DateTimeKind.Local).AddTicks(671), "IsBank", new Guid("0007a224-43c7-4e09-9d91-02c5b5df1b4f"), "boolean", "bit", "", null, "AlwaysHidden" },
                    { 799, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 829, DateTimeKind.Local).AddTicks(686), "PayReceiveId", new Guid("99cfbed8-7cc6-432d-ace0-49bda3e841d7"), "number", "int", "", null, "AlwaysHidden" },
                    { 788, true, true, (short)7, "System.String", "", "", false, false, false, 256, 0, new DateTime(2023, 10, 18, 17, 16, 5, 829, DateTimeKind.Local).AddTicks(503), "FullAccount.Project.FullCode", new Guid("1f33521e-4efa-4f24-bdea-1971320be489"), "string", "nvarchar", "", null, "Hidden" },
                    { 774, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 829, DateTimeKind.Local).AddTicks(120), "Id", new Guid("e5ef2b0d-6008-4b70-826c-8c58ae799314"), "number", "int", "", null, "AlwaysHidden" },
                    { 773, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 829, DateTimeKind.Local).AddTicks(104), "BranchId", new Guid("0c2b0e9a-792a-4adf-b540-050797158c31"), "number", "int", "", null, "AlwaysHidden" },
                    { 772, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 829, DateTimeKind.Local).AddTicks(88), "FiscalPeriodId", new Guid("1479fd3c-955a-4733-ba3d-b5cc18b4c935"), "number", "int", "", null, "AlwaysHidden" },
                    { 747, true, true, (short)9, "System.String", "", "", false, false, false, 32, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(9587), "SeriesNo", new Guid("7453699c-3389-485e-b867-c710e3b9a4b6"), "string", "nvarchar", "", null, "Visible" },
                    { 748, true, true, (short)10, "System.String", "", "", false, false, false, 16, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(9603), "SayyadStartNo", new Guid("47849a83-2a54-4821-a9df-e4d7b444f652"), "string", "nvarchar", "", null, "Visible" },
                    { 749, true, true, (short)3, "System.String", "", "", false, false, false, 16, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(9620), "SayyadNo", new Guid("9366577d-d86e-4d59-8143-8339a911d282"), "string", "nvarchar", "", null, "Visible" },
                    { 750, true, true, (short)3, "System.String", "", "", false, false, true, 64, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(9637), "TypeName", new Guid("27a3b2c6-ed37-4e03-bc21-261e12628456"), "string", "nvarchar", "", null, "Visible" },
                    { 751, true, true, (short)-1, "System.Int16", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(9653), "BranchScope", new Guid("c6dfa806-739a-40e9-9761-308c8d7091e2"), "number", "smallint", "", null, "AlwaysHidden" },
                    { 752, true, true, (short)3, "System.DateTime", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(9669), "Date", new Guid("234b32ea-f7f8-41e0-a00e-66b790d80ec1"), "Date", "datetime", "Default", null, "Visible" },
                    { 753, true, true, (short)4, "System.String", "", "", false, false, false, 64, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(9685), "IssuedByName", new Guid("12f3a2fa-90de-4e6f-b2ef-9df709fe0dec"), "string", "nvarchar", "", null, "Visible" },
                    { 754, true, true, (short)5, "System.String", "", "", false, false, true, 64, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(9765), "ConfirmedByName", new Guid("54ff86c8-6366-492f-b2e7-e06bfe78053d"), "string", "nvarchar", "", null, "Visible" },
                    { 755, true, true, (short)6, "System.String", "", "", false, false, true, 64, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(9789), "ApprovedByName", new Guid("a83813f6-6d24-4d43-ad17-42bdd6498c77"), "string", "nvarchar", "", null, "Visible" },
                    { 756, true, true, (short)7, "System.Decimal", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(9804), "CurrencyRate", new Guid("b56ee081-8af7-41d6-a899-600dc0a3265f"), "number", "money", "Currency", null, "Visible" },
                    { 757, true, true, (short)8, "System.String", "", "", false, false, true, 1024, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(9822), "Description", new Guid("6178e571-aa44-49b1-a6f8-6a24e6a95155"), "string", "nvarchar", "", null, "Visible" },
                    { 758, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(9838), "FiscalPeriodId", new Guid("4e35a1f2-3ab0-4cfc-9719-b5cc43157557"), "number", "int", "", null, "AlwaysHidden" },
                    { 759, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(9854), "BranchId", new Guid("ccf45205-dd35-4454-8dfc-c2994fa9037d"), "number", "int", "", null, "AlwaysHidden" },
                    { 760, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(9870), "Id", new Guid("d16e6489-61d5-4703-9fe4-9ebdc2085912"), "number", "int", "", null, "AlwaysHidden" },
                    { 761, true, true, (short)-1, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(9889), "CurrencyId", new Guid("4019154c-6132-417a-99c0-9bf2829d1ee4"), "number", "int", "", null, "Visible" },
                    { 762, true, true, (short)-1, "System.Boolean", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(9907), "IsConfirmed", new Guid("e14a1b8f-0a61-44fb-8ac8-21c802b1e16c"), "Boolean", "bit", "", null, "Visible" },
                    { 763, true, true, (short)-1, "System.Boolean", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(9932), "IsApproved", new Guid("c6af833a-8834-4e8f-ac63-aa4868811c64"), "Boolean", "bit", "", null, "Visible" },
                    { 764, true, true, (short)1, "System.Int64", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(9949), "TextNo", new Guid("cb0a6463-a392-4915-8103-f84d34e78c35"), "number", "bigint", "", null, "Visible" },
                    { 765, true, true, (short)2, "System.String", "", "", false, false, true, 64, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(9966), "Reference", new Guid("eea00370-e4b8-472a-9bcc-f2702fe08a01"), "string", "nvarchar", "", null, "Visible" },
                    { 766, true, true, (short)3, "System.DateTime", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(9983), "Date", new Guid("f21fe3f8-a0a9-40fe-952f-02d5c6f2617d"), "Date", "datetime", "Default", null, "Visible" },
                    { 767, true, true, (short)4, "System.String", "", "", false, false, false, 64, 0, new DateTime(2023, 10, 18, 17, 16, 5, 829, DateTimeKind.Local), "IssuedByName", new Guid("f61d229e-4126-4724-9e17-c1a1cfed5150"), "string", "nvarchar", "", null, "Visible" },
                    { 768, true, true, (short)5, "System.String", "", "", false, false, true, 64, 0, new DateTime(2023, 10, 18, 17, 16, 5, 829, DateTimeKind.Local).AddTicks(15), "ConfirmedByName", new Guid("81f489a5-4f12-4648-8157-746454894490"), "string", "nvarchar", "", null, "Visible" },
                    { 769, true, true, (short)6, "System.String", "", "", false, false, true, 64, 0, new DateTime(2023, 10, 18, 17, 16, 5, 829, DateTimeKind.Local).AddTicks(31), "ApprovedByName", new Guid("39f11b46-2979-4ba7-9c71-4bc6ceab90d7"), "string", "nvarchar", "", null, "Visible" },
                    { 770, true, true, (short)7, "System.Decimal", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 829, DateTimeKind.Local).AddTicks(49), "CurrencyRate", new Guid("64a38dbc-9e01-4324-8d5a-758f4f35da20"), "number", "money", "Currency", null, "Visible" },
                    { 771, true, true, (short)8, "System.String", "", "", false, false, true, 1024, 0, new DateTime(2023, 10, 18, 17, 16, 5, 829, DateTimeKind.Local).AddTicks(72), "Description", new Guid("8ef0d0a8-28a0-457d-b7ef-9dab1de3ba34"), "string", "nvarchar", "", null, "Visible" },
                    { 745, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(9550), "BranchId", new Guid("fe3e7274-5fbe-4cb4-a5ee-2d76b3e6cf1a"), "number", "int", "", null, "AlwaysHidden" },
                    { 458, true, true, (short)11, "System.Decimal", "", "EndBalance", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(3523), "EndBalanceDebit", new Guid("a4343cb2-c0be-420a-8bb8-d13941b8dc7c"), "number", "money", "Money", null, "Visible" },
                    { 523, true, true, (short)-1, "System.String", "", "", false, false, true, 64, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(5030), "VoucherReference", new Guid("f0bfaa22-a8fa-4c8c-8b58-bd4e63b086fe"), "string", "nvarchar", "", null, "AlwaysHidden" },
                    { 456, true, true, (short)9, "System.Decimal", "", "Corrections", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(3389), "CorrectionsDebit", new Guid("25bbe12a-2d5c-4d9b-b7ba-d5b7880781e0"), "number", "money", "Money", null, "Visible" },
                    { 146, true, true, (short)13, "System.Decimal", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(6904), "Credit", new Guid("d4d56cf6-52d5-495e-bed0-9c40c83ad0d4"), "number", "money", "Money", null, "Visible" },
                    { 147, true, true, (short)14, "System.String", "", "", false, false, false, 128, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(6921), "BranchName", new Guid("7d5f5cc4-7d55-4377-a817-d943902d5cb4"), "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 148, true, true, (short)15, "System.String", "", "", false, false, true, 128, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(6937), "Mark", new Guid("d026bd43-e95c-4f52-a0c2-211a45b37868"), "string", "nvarchar", "", null, "Visible" },
                    { 149, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(6955), "RowNo", new Guid("1e85c120-6033-45e6-ae5c-32a815e8715b"), "number", "int", "", null, "AlwaysVisible" },
                    { 150, true, true, (short)1, "System.Date", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(6971), "VoucherDate", new Guid("1424bf30-bca3-4b2f-9243-ed09eec0e7c6"), "Date", "datetime", "Default", null, "Visible" }
                });

            migrationBuilder.InsertData(
                schema: "Metadata",
                table: "Column",
                columns: new[] { "ColumnID", "AllowFiltering", "AllowSorting", "DisplayIndex", "DotNetType", "Expression", "GroupName", "IsDynamic", "IsFixedLength", "IsNullable", "Length", "MinLength", "ModifiedDate", "Name", "rowguid", "ScriptType", "StorageType", "Type", "ViewID", "Visibility" },
                values: new object[,]
                {
                    { 151, true, true, (short)2, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(6987), "VoucherNo", new Guid("988d3671-fc53-4b44-ba9e-fcd8ca5bc69e"), "number", "int", "", null, "Visible" },
                    { 152, true, true, (short)3, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(7004), "AccountFullCode", new Guid("9e3730cd-1b39-4595-ad82-56943e6c95b3"), "string", "nvarchar", "", null, "Visible" },
                    { 153, true, true, (short)4, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(7025), "AccountName", new Guid("865d7828-e5ef-44fb-b9e2-785c4dfbca32"), "string", "nvarchar", "", null, "Visible" },
                    { 154, true, true, (short)5, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(7043), "Description", new Guid("9f6c6573-98e4-4589-b87e-edd67a4c0b79"), "string", "nvarchar", "", null, "Visible" },
                    { 155, true, true, (short)6, "System.Decimal", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(7058), "Debit", new Guid("c701d9fb-e465-4233-9177-46a3b243342d"), "number", "money", "Money", null, "Visible" },
                    { 156, true, true, (short)7, "System.Decimal", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(7074), "Credit", new Guid("38bcf5fc-aa1b-44e0-95bf-85f714f374f7"), "number", "money", "Money", null, "Visible" },
                    { 145, true, true, (short)12, "System.Decimal", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(6888), "Debit", new Guid("9266b099-6d9b-4514-b37e-569186a4dc58"), "number", "money", "Money", null, "Visible" },
                    { 157, true, true, (short)8, "System.String", "", "", false, false, false, 128, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(7090), "BranchName", new Guid("83f60009-7162-45c1-9422-08f5e79c7385"), "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 159, true, true, (short)1, "System.Date", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(7121), "VoucherDate", new Guid("64a529f8-8093-4b18-8040-25302e0848a3"), "Date", "datetime", "Default", null, "Visible" },
                    { 160, true, true, (short)2, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(7138), "VoucherNo", new Guid("4841aa82-1e60-448d-ad99-3b55bc2a9927"), "number", "int", "", null, "Visible" },
                    { 161, true, true, (short)3, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(7162), "AccountFullCode", new Guid("43e3313e-5b76-4823-9e29-48d0ad396cab"), "string", "nvarchar", "", null, "Visible" },
                    { 162, true, true, (short)4, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(7178), "AccountName", new Guid("2c832ef6-d0f6-4622-b9a1-dfca1f95d036"), "string", "nvarchar", "", null, "Visible" },
                    { 457, true, true, (short)10, "System.Decimal", "", "Corrections", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(3499), "CorrectionsCredit", new Guid("b6b2f2f9-dc85-4de2-84bf-2f873799ec23"), "number", "money", "Money", null, "Visible" },
                    { 164, true, true, (short)6, "System.Decimal", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(7210), "Debit", new Guid("30157d92-21a0-4a0d-b39a-c9d33ee78385"), "number", "money", "Money", null, "Visible" },
                    { 165, true, true, (short)7, "System.Decimal", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(7225), "Credit", new Guid("0606793e-8401-4b12-8245-47454b837385"), "number", "money", "Money", null, "Visible" },
                    { 166, true, true, (short)8, "System.String", "", "", false, false, false, 128, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(7241), "BranchName", new Guid("fff7ec10-7d37-4902-873e-f4ea0ff427a6"), "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 167, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(7258), "RowNo", new Guid("2503f5a1-4a72-45ff-8e55-7b8b52cb9f90"), "number", "int", "", null, "AlwaysVisible" },
                    { 168, true, true, (short)1, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(7275), "AccountFullCode", new Guid("2908f430-1847-43d4-8a5c-d7522c0c2b5a"), "string", "nvarchar", "", null, "Visible" },
                    { 169, true, true, (short)2, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(7295), "AccountName", new Guid("0b771af7-787c-4f1f-a289-72b2a7d615a9"), "string", "nvarchar", "", null, "Visible" },
                    { 158, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(7105), "RowNo", new Guid("6e19aef8-5211-43ab-9183-5ebbe0dd4fc7"), "number", "int", "", null, "AlwaysVisible" },
                    { 144, true, true, (short)11, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(6866), "Description", new Guid("202f7e79-e581-4ed5-abae-c232d66cc35d"), "string", "nvarchar", "", null, "Visible" },
                    { 143, true, true, (short)10, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(6846), "ProjectName", new Guid("bc8e4878-b54a-4662-951b-4c91b5622cdb"), "string", "nvarchar", "", null, "Visible" },
                    { 142, true, true, (short)9, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(6734), "ProjectFullCode", new Guid("6abc3af3-87bb-4bf7-a2ee-a71c9a9491cc"), "string", "nvarchar", "", null, "Visible" },
                    { 117, true, true, (short)10, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(6207), "EntityNo", new Guid("fd229fe0-7853-4550-abbd-fe305cbe5e2c"), "number", "int", "", null, "Visible" },
                    { 118, true, true, (short)11, "System.DateTime", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(6318), "EntityDate", new Guid("e94f156c-5beb-449d-8561-f09f117e0213"), "Date", "datetime", "Default", null, "Visible" },
                    { 119, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(6337), "Id", new Guid("200e0fd7-342f-4bb7-9999-642f7e62aa77"), "number", "int", "", null, "AlwaysHidden" },
                    { 120, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(6354), "RowNo", new Guid("3e24911d-8b5d-4525-9aa2-a8994dbd3e0f"), "number", "int", "", null, "AlwaysVisible" },
                    { 121, true, true, (short)1, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(6375), "Name", new Guid("72fb38dd-c33a-4aa0-a68c-afdc417366c8"), "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 122, true, true, (short)2, "System.String", "", "", false, false, false, 256, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(6393), "FullCode", new Guid("bc45d063-85b2-4c02-86a8-5f833ad81a28"), "string", "nvarchar", "", null, "Visible" },
                    { 123, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(6408), "RowNo", new Guid("ab564cad-84fa-43bf-bade-a736368dd5eb"), "number", "int", "", null, "AlwaysVisible" },
                    { 124, true, true, (short)1, "System.Date", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(6424), "VoucherDate", new Guid("9d1e487d-f0b7-4d4c-b3dd-f2b6e505cce7"), "Date", "datetime", "Default", null, "Visible" },
                    { 125, true, true, (short)2, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(6440), "VoucherNo", new Guid("cb097b5c-6842-4f95-9d61-e7d385237797"), "number", "int", "", null, "Visible" },
                    { 126, true, true, (short)3, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(6456), "AccountFullCode", new Guid("b08c5d8a-b42c-4d45-bd5f-62106908f1d3"), "string", "nvarchar", "", null, "Visible" },
                    { 127, true, true, (short)4, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(6474), "AccountName", new Guid("d59d7b38-b3b3-40ab-8f93-80326a63f703"), "string", "nvarchar", "", null, "Visible" },
                    { 128, true, true, (short)5, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(6489), "Description", new Guid("3fa85f5a-f076-42ff-a55c-774dc60d0f3e"), "string", "nvarchar", "", null, "Visible" },
                    { 129, true, true, (short)6, "System.Decimal", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(6510), "Debit", new Guid("f7fb5c22-6d6d-464e-b447-0b904857683f"), "number", "money", "Money", null, "Visible" },
                    { 130, true, true, (short)7, "System.Decimal", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(6526), "Credit", new Guid("ca18cd25-e8dd-40ae-b70a-5f73ea12f592"), "number", "money", "Money", null, "Visible" },
                    { 131, true, true, (short)8, "System.String", "", "", false, false, false, 128, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(6547), "BranchName", new Guid("acf11464-ba95-42c9-83d6-ee4939817d2d"), "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 132, true, true, (short)9, "System.String", "", "", false, false, true, 128, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(6563), "Mark", new Guid("2e6228c4-f3dc-472e-8831-39059083ae30"), "string", "nvarchar", "", null, "Visible" },
                    { 133, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(6580), "RowNo", new Guid("a193050b-80c4-4613-9dd2-3785080c37b9"), "number", "int", "", null, "AlwaysVisible" },
                    { 134, true, true, (short)1, "System.Date", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(6597), "VoucherDate", new Guid("6d5d65db-6082-47ac-8e44-0d8b17b6eb4c"), "Date", "datetime", "Default", null, "Visible" },
                    { 135, true, true, (short)2, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(6613), "VoucherNo", new Guid("6570a620-b9f7-4e40-a719-5404345cc3ff"), "number", "int", "", null, "Visible" }
                });

            migrationBuilder.InsertData(
                schema: "Metadata",
                table: "Column",
                columns: new[] { "ColumnID", "AllowFiltering", "AllowSorting", "DisplayIndex", "DotNetType", "Expression", "GroupName", "IsDynamic", "IsFixedLength", "IsNullable", "Length", "MinLength", "ModifiedDate", "Name", "rowguid", "ScriptType", "StorageType", "Type", "ViewID", "Visibility" },
                values: new object[,]
                {
                    { 136, true, true, (short)3, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(6629), "AccountFullCode", new Guid("e0a58caa-f6b5-416a-a092-f85c9b068a04"), "string", "nvarchar", "", null, "Visible" },
                    { 137, true, true, (short)4, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(6650), "AccountName", new Guid("abce6093-3e9d-4a5d-97ac-c27e0e6e3c0c"), "string", "nvarchar", "", null, "Visible" },
                    { 138, true, true, (short)5, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(6666), "DetailAccountFullCode", new Guid("be7ba9e6-ed8f-46f2-8411-4befc724b8f7"), "string", "nvarchar", "", null, "Visible" },
                    { 139, true, true, (short)6, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(6681), "DetailAccountName", new Guid("3d1266e8-9955-432c-8f40-6cc998413bd5"), "string", "nvarchar", "", null, "Visible" },
                    { 140, true, true, (short)7, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(6699), "CostCenterFullCode", new Guid("c194aba3-d0fd-4592-9fd6-0624bd9f2006"), "string", "nvarchar", "", null, "Visible" },
                    { 141, true, true, (short)8, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(6718), "CostCenterName", new Guid("998096d2-f9c6-4939-a39f-c8b5798a73c2"), "string", "nvarchar", "", null, "Visible" },
                    { 170, true, true, (short)3, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(7313), "Description", new Guid("214a8ec7-c452-4ed7-8327-05856a46e346"), "string", "nvarchar", "", null, "Visible" },
                    { 116, true, true, (short)9, "System.String", "", "", false, false, false, 64, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(6191), "EntityDescription", new Guid("be06fafc-e154-404c-a7bf-ab1d3a435d88"), "string", "nvarchar", "", null, "Visible" },
                    { 171, true, true, (short)4, "System.Decimal", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(7328), "Debit", new Guid("7a096934-bd19-4eee-a252-796f58ed6917"), "number", "money", "Money", null, "Visible" },
                    { 173, true, true, (short)6, "System.String", "", "", false, false, false, 128, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(7360), "BranchName", new Guid("60c88406-1de3-4833-a84d-d20cd4f17d61"), "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 203, true, true, (short)3, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(7967), "AccountFullCode", new Guid("e2bbd534-46b5-453b-94ba-ffecbba97d7c"), "string", "nvarchar", "", null, "Visible" },
                    { 204, true, true, (short)4, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(7983), "AccountName", new Guid("9df45a65-01d5-4d60-b18d-813d9e50344d"), "string", "nvarchar", "", null, "Visible" },
                    { 205, true, true, (short)5, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(7999), "DetailAccountFullCode", new Guid("cef3753b-e507-462b-a289-b3fe11e89aa2"), "string", "nvarchar", "", null, "Visible" },
                    { 206, true, true, (short)6, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(8014), "DetailAccountName", new Guid("0133da50-6317-4220-84b8-c36446ed2ace"), "string", "nvarchar", "", null, "Visible" },
                    { 207, true, true, (short)7, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(8030), "CostCenterFullCode", new Guid("c34859a7-70c6-41e3-89d8-72bdb8bffa92"), "string", "nvarchar", "", null, "Visible" },
                    { 208, true, true, (short)8, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(8047), "CostCenterName", new Guid("ffe6ac30-e1d7-4602-89b1-b9d9c8a382c8"), "string", "nvarchar", "", null, "Visible" },
                    { 209, true, true, (short)9, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(8164), "ProjectFullCode", new Guid("4edb9cb0-f65c-4e0d-8d7f-a1a1429b1f44"), "string", "nvarchar", "", null, "Visible" },
                    { 210, true, true, (short)10, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(8182), "ProjectName", new Guid("e67e1523-f538-4953-ba5b-9e7b052b3b0d"), "string", "nvarchar", "", null, "Visible" },
                    { 211, true, true, (short)11, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(8198), "Description", new Guid("1a23dc4d-5abb-4b92-aeb0-42fc41c5e922"), "string", "nvarchar", "", null, "Visible" },
                    { 212, true, true, (short)12, "System.Decimal", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(8214), "Debit", new Guid("b80586a1-24b8-4ca3-9d19-0e43411b3f28"), "number", "money", "Money", null, "Visible" },
                    { 213, true, true, (short)13, "System.Decimal", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(8230), "Credit", new Guid("3371090a-c0b6-4d8e-bd40-eb0907ff4b92"), "number", "money", "Money", null, "Visible" },
                    { 202, true, true, (short)2, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(7951), "VoucherNo", new Guid("2d9117c2-c7b4-4b2a-aa31-97b81c33fe82"), "number", "int", "", null, "Visible" },
                    { 214, true, true, (short)14, "System.String", "", "", false, false, false, 128, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(8246), "BranchName", new Guid("80b69143-a1e6-4ee1-b3e7-fe4eb2284dd9"), "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 216, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(8282), "RowNo", new Guid("41548a4f-cb4f-41ea-a534-144cf94b1d89"), "number", "int", "", null, "AlwaysVisible" },
                    { 217, true, true, (short)1, "System.Date", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(8304), "VoucherDate", new Guid("27268230-18c5-4f6c-8f02-8f0a8e4021bf"), "Date", "datetime", "Default", null, "Visible" },
                    { 218, true, true, (short)2, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(8320), "VoucherNo", new Guid("3ac8c219-94d2-45b4-85c3-a1c177c00403"), "number", "int", "", null, "Visible" },
                    { 219, true, true, (short)3, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(8337), "AccountFullCode", new Guid("333b190d-0ae4-4b2a-b6de-e644b31b2d73"), "string", "nvarchar", "", null, "Visible" },
                    { 220, true, true, (short)4, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(8353), "AccountName", new Guid("49523fea-3c5b-46d6-9110-13e927c64a55"), "string", "nvarchar", "", null, "Visible" },
                    { 221, true, true, (short)5, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(8371), "Description", new Guid("680e79ba-66b5-4fae-a491-a69dcf3f1fde"), "string", "nvarchar", "", null, "Visible" },
                    { 222, true, true, (short)6, "System.Decimal", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(8388), "Debit", new Guid("d3b7073e-7b65-4c18-b42b-aa992ec79856"), "number", "money", "Money", null, "Visible" },
                    { 223, true, true, (short)7, "System.Decimal", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(8404), "Credit", new Guid("b4459933-8bed-4214-bd60-b7470925ceb1"), "number", "money", "Money", null, "Visible" },
                    { 224, true, true, (short)8, "System.String", "", "", false, false, false, 128, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(8421), "BranchName", new Guid("f81146b8-9a3b-444f-ae05-f2ef3e8c9bed"), "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 225, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(8444), "RowNo", new Guid("46b8151f-58ce-4723-b7b5-ea79512c1d17"), "number", "int", "", null, "AlwaysVisible" },
                    { 226, true, true, (short)1, "System.Date", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(8464), "VoucherDate", new Guid("0c8c5f67-a48b-4d55-9385-cbeb5c83a82f"), "Date", "datetime", "Default", null, "Visible" },
                    { 215, true, true, (short)15, "System.String", "", "", false, false, true, 128, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(8266), "Mark", new Guid("056e70bf-e314-4bc2-9bc0-664a3bb55eec"), "string", "nvarchar", "", null, "Visible" },
                    { 201, true, true, (short)1, "System.Date", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(7933), "VoucherDate", new Guid("b14b3eff-dfee-485b-a7ec-6b837f0d053b"), "Date", "datetime", "Default", null, "Visible" },
                    { 200, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(7910), "RowNo", new Guid("2979b39a-7ae0-4026-aa6c-17b79738328b"), "number", "int", "", null, "AlwaysVisible" },
                    { 199, true, true, (short)9, "System.String", "", "", false, false, true, 128, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(7894), "Mark", new Guid("1edc7286-393c-480b-9f23-f74b957928c6"), "string", "nvarchar", "", null, "Visible" },
                    { 174, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(7377), "RowNo", new Guid("8fe74c02-abf8-462c-9d35-1f375fb47410"), "number", "int", "", null, "AlwaysVisible" },
                    { 175, true, true, (short)1, "System.Date", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(7393), "VoucherDate", new Guid("aa15dc25-7ddc-453d-80c5-ced3970082a4"), "Date", "datetime", "Default", null, "Visible" },
                    { 176, true, true, (short)2, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(7510), "AccountFullCode", new Guid("54f97e0a-5b28-43f6-8889-8ae1515c5837"), "string", "nvarchar", "", null, "Visible" },
                    { 177, true, true, (short)3, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(7532), "AccountName", new Guid("9f02734d-b746-402b-ab2c-1d6a22038042"), "string", "nvarchar", "", null, "Visible" }
                });

            migrationBuilder.InsertData(
                schema: "Metadata",
                table: "Column",
                columns: new[] { "ColumnID", "AllowFiltering", "AllowSorting", "DisplayIndex", "DotNetType", "Expression", "GroupName", "IsDynamic", "IsFixedLength", "IsNullable", "Length", "MinLength", "ModifiedDate", "Name", "rowguid", "ScriptType", "StorageType", "Type", "ViewID", "Visibility" },
                values: new object[,]
                {
                    { 178, true, true, (short)4, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(7548), "Description", new Guid("3ea6b563-8a67-4b90-bd66-a6508f97901c"), "string", "nvarchar", "", null, "Visible" },
                    { 179, true, true, (short)5, "System.Decimal", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(7563), "Debit", new Guid("1a0c8495-cc6e-412d-9624-6194b30304aa"), "number", "money", "Money", null, "Visible" },
                    { 180, true, true, (short)6, "System.Decimal", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(7579), "Credit", new Guid("da37a0de-6b92-43a4-998a-c7bf2c4818a2"), "number", "money", "Money", null, "Visible" },
                    { 181, true, true, (short)7, "System.String", "", "", false, false, false, 128, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(7597), "BranchName", new Guid("13e4667c-82d6-4576-b964-7170288c4e1b"), "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 182, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(7612), "RowNo", new Guid("454b5ded-cf6f-40b7-92df-b271f82833ea"), "number", "int", "", null, "AlwaysVisible" },
                    { 183, true, true, (short)1, "System.Date", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(7628), "VoucherDate", new Guid("a2b77365-8374-49b9-a648-24d5814cd6d4"), "Date", "datetime", "Default", null, "Visible" },
                    { 184, true, true, (short)2, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(7644), "AccountFullCode", new Guid("360dd15b-16f0-4dab-a89a-a86d139b772d"), "string", "nvarchar", "", null, "Visible" },
                    { 185, true, true, (short)3, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(7666), "AccountName", new Guid("5a3901b6-bc00-4d56-a59c-29d8b1caeb9a"), "string", "nvarchar", "", null, "Visible" },
                    { 186, true, true, (short)4, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(7682), "Description", new Guid("68b57e82-ac9c-4c51-89b4-f55e73e12079"), "string", "nvarchar", "", null, "Visible" },
                    { 187, true, true, (short)5, "System.Decimal", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(7697), "Debit", new Guid("a2dfcba5-413f-440b-afa9-99ee9d202382"), "number", "money", "Money", null, "Visible" },
                    { 188, true, true, (short)6, "System.Decimal", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(7714), "Credit", new Guid("aa06ec87-a3a8-4f08-80b8-591863bff852"), "number", "money", "Money", null, "Visible" },
                    { 189, true, true, (short)7, "System.String", "", "", false, false, false, 128, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(7730), "BranchName", new Guid("b9e8c3b2-f7a3-4f58-9740-404ecb1d8d4a"), "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 190, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(7746), "RowNo", new Guid("2210feac-865d-4a9f-a2a0-b0ff5cf3c10b"), "number", "int", "", null, "AlwaysVisible" },
                    { 191, true, true, (short)1, "System.Date", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(7761), "VoucherDate", new Guid("b7439ac7-ec87-412f-bf89-17bbcba0749d"), "Date", "datetime", "Default", null, "Visible" },
                    { 192, true, true, (short)2, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(7777), "VoucherNo", new Guid("feec4a66-44af-40f7-9e6b-33db92f20f67"), "number", "int", "", null, "Visible" },
                    { 193, true, true, (short)3, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(7798), "AccountFullCode", new Guid("7aeed924-5408-44f9-9a51-a170da217a10"), "string", "nvarchar", "", null, "Visible" },
                    { 194, true, true, (short)4, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(7816), "AccountName", new Guid("4d410902-ef72-4da2-a427-0f6ff3a021b2"), "string", "nvarchar", "", null, "Visible" },
                    { 195, true, true, (short)5, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(7832), "Description", new Guid("65550ba0-e4e6-4142-b959-c68953622d04"), "string", "nvarchar", "", null, "Visible" },
                    { 196, true, true, (short)6, "System.Decimal", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(7847), "Debit", new Guid("dea9d5e5-9d43-46ff-b283-97aff93f60a6"), "number", "money", "Money", null, "Visible" },
                    { 197, true, true, (short)7, "System.Decimal", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(7863), "Credit", new Guid("0c616ded-06f7-4359-94b0-c90ac3a592dd"), "number", "money", "Money", null, "Visible" },
                    { 198, true, true, (short)8, "System.String", "", "", false, false, false, 128, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(7879), "BranchName", new Guid("467d2796-0324-4614-a79b-ab0a698a427d"), "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 172, true, true, (short)5, "System.Decimal", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(7344), "Credit", new Guid("e3fe48c5-186d-4f7e-82e8-6d0dd53f41bb"), "number", "money", "Money", null, "Visible" },
                    { 115, true, true, (short)8, "System.String", "", "", false, false, false, 64, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(6176), "EntityName", new Guid("ecdefa31-3556-4345-909c-d32f29c88306"), "string", "nvarchar", "", null, "Visible" },
                    { 114, true, true, (short)7, "System.String", "", "", false, false, false, 64, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(6160), "EntityCode", new Guid("d83ea6b6-e99b-4810-8676-92347b3febe3"), "string", "nvarchar", "", null, "Visible" },
                    { 113, true, true, (short)6, "System.String", "", "", false, false, false, 64, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(6144), "EntityTypeName", new Guid("32d87579-dae8-4474-a639-ca2f9896ac52"), "string", "nvarchar", "", null, "Visible" },
                    { 30, true, true, (short)-1, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(4244), "FullAccount.CostCenter.Id", new Guid("ac0010be-cda2-4f20-914a-2396cc93e16b"), "number", "int", "", null, "AlwaysHidden" },
                    { 31, true, true, (short)-1, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(4378), "FullAccount.Project.Id", new Guid("6ccdacc0-be3e-4e1d-a8e9-1c3fa10d9832"), "number", "int", "", null, "AlwaysHidden" },
                    { 32, true, true, (short)-1, "System.Decimal", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(4403), "CurrencyRate", new Guid("c055c42c-319f-4a91-bdbc-edfb3448c4c5"), "number", "money", "Money", null, "AlwaysHidden" },
                    { 33, true, true, (short)-1, "System.Int16", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(4420), "TypeId", new Guid("d7868e5f-aeb9-4102-b0b5-01b0275b3c90"), "number", "smallint", "", null, "AlwaysHidden" },
                    { 34, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(4443), "RowNo", new Guid("d69336de-fbf8-4805-9408-b40a1519c764"), "number", "int", "", null, "AlwaysVisible" },
                    { 35, true, true, (short)1, "System.String", "", "", false, false, false, 256, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(4460), "FullAccount.Account.FullCode", new Guid("866612be-41c8-4277-b832-20928ba656bc"), "string", "nvarchar", "", null, "Visible" },
                    { 36, true, true, (short)2, "System.String", "", "", false, false, false, 128, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(4477), "FullAccount.Account.Name", new Guid("f87b137f-3adc-4745-b246-68459cd1f03f"), "string", "nvarchar", "", null, "Visible" },
                    { 37, true, true, (short)3, "System.String", "", "", false, false, false, 128, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(4494), "FullAccount.DetailAccount.FullCode", new Guid("56b60d1e-b52c-4d53-9c88-3feaa193f84b"), "string", "nvarchar", "", null, "Hidden" },
                    { 38, true, true, (short)4, "System.String", "", "", false, false, false, 128, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(4513), "FullAccount.DetailAccount.Name", new Guid("f9e54496-3b0d-439c-95bc-2b7681748c1f"), "string", "nvarchar", "", null, "Hidden" },
                    { 39, true, true, (short)5, "System.String", "", "", false, false, false, 128, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(4530), "FullAccount.CostCenter.FullCode", new Guid("cf7245be-e297-4b81-a4d3-f46e94ac0815"), "string", "nvarchar", "", null, "Hidden" },
                    { 40, true, true, (short)6, "System.String", "", "", false, false, false, 128, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(4552), "FullAccount.CostCenter.Name", new Guid("278f8a13-2705-4695-82a3-fc6066bd1592"), "string", "nvarchar", "", null, "Hidden" },
                    { 29, true, true, (short)-1, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(4218), "FullAccount.DetailAccount.Id", new Guid("e081c5d3-1a11-4241-8101-3a2c91eff462"), "number", "int", "", null, "AlwaysHidden" },
                    { 41, true, true, (short)7, "System.String", "", "", false, false, false, 128, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(4571), "FullAccount.Project.FullCode", new Guid("caaf8e03-a5ed-47c5-82c2-62b9817741c0"), "string", "nvarchar", "", null, "Hidden" },
                    { 43, true, true, (short)11, "System.String", "", "", false, false, true, 512, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(4615), "Description", new Guid("d853a688-b4b6-444b-88dd-146693b274b0"), "string", "nvarchar", "", null, "Visible" },
                    { 44, true, true, (short)12, "System.Decimal", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(4635), "Debit", new Guid("a8260e10-6af6-43c0-b110-07a3cec5037d"), "number", "money", "Money", null, "Visible" },
                    { 45, true, true, (short)13, "System.Decimal", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(4654), "Credit", new Guid("eafcb1b3-f634-455e-9005-75e35aefb64b"), "number", "money", "Money", null, "Visible" },
                    { 46, true, true, (short)14, "System.String", "", "", false, false, false, 64, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(4670), "CurrencyName", new Guid("980e3f54-0a84-47e9-988c-434167e3e350"), "string", "nvarchar", "", null, "AlwaysVisible" }
                });

            migrationBuilder.InsertData(
                schema: "Metadata",
                table: "Column",
                columns: new[] { "ColumnID", "AllowFiltering", "AllowSorting", "DisplayIndex", "DotNetType", "Expression", "GroupName", "IsDynamic", "IsFixedLength", "IsNullable", "Length", "MinLength", "ModifiedDate", "Name", "rowguid", "ScriptType", "StorageType", "Type", "ViewID", "Visibility" },
                values: new object[,]
                {
                    { 47, true, true, (short)15, "System.Decimal", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(4690), "CurrencyValue", new Guid("ad21263f-2b32-47eb-ad2b-efec47b220f7"), "number", "money", "Money", null, "AlwaysVisible" },
                    { 48, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(4712), "Id", new Guid("c90d2ef1-d26e-443a-b396-018bd79b0a6c"), "number", "int", "", null, "AlwaysHidden" },
                    { 49, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(4729), "RowNo", new Guid("9de16a18-5e75-419e-b37e-ed17ba6da93b"), "number", "int", "", null, "AlwaysVisible" },
                    { 50, true, true, (short)1, "System.String", "", "", false, false, false, 64, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(4747), "UserName", new Guid("81ce417e-130b-4213-afde-7ef2f9091430"), "string", "nvarchar(64)", "", null, "AlwaysVisible" },
                    { 51, true, true, (short)2, "System.DateTime", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(4767), "LastLoginDate", new Guid("72cc201c-ef80-45ff-9c12-cec1c7023a57"), "DateTime", "datetime", "Default", null, "Visible" },
                    { 52, true, true, (short)3, "System.Boolean", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(4789), "IsEnabled", new Guid("c566265a-49e1-4e24-9c1e-d5fdf583adf6"), "boolean", "bit", "", null, "Visible" },
                    { 53, true, true, (short)4, "System.String", "Person.FullName", "", false, false, false, 64, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(4805), "PersonFullName", new Guid("7c32608e-54dd-4935-8428-54432b636602"), "string", "nvarchar(64)", "", null, "Visible" },
                    { 42, true, true, (short)8, "System.String", "", "", false, false, false, 128, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(4593), "FullAccount.Project.Name", new Guid("4cc83286-269f-46e7-b915-819d2bde373e"), "string", "nvarchar", "", null, "Hidden" },
                    { 28, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(4200), "FullAccount.Account.Id", new Guid("5793c99b-ff29-4d65-9ddd-3a7526e9d488"), "number", "int", "", null, "AlwaysHidden" },
                    { 27, true, true, (short)-1, "System.Object", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(4184), "FullAccount", new Guid("1815525d-0d24-4dff-a892-95cd8b81d5e0"), "object", "(n/a)", "", null, "AlwaysHidden" },
                    { 26, true, true, (short)-1, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(4166), "CurrencyId", new Guid("0a9cb39c-3876-47db-9553-232764974c74"), "number", "int", "", null, "AlwaysHidden" },
                    { 1, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 826, DateTimeKind.Local).AddTicks(6426), "Id", new Guid("7daa7fea-3e20-4834-b1a3-e447e01afc47"), "number", "int", "", null, "AlwaysHidden" },
                    { 2, true, false, (short)-1, "System.Int16", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(3223), "BranchScope", new Guid("48c5fcbd-ffd5-482f-87af-5e2b8fab8dcc"), "number", "smallint", "", null, "AlwaysHidden" },
                    { 3, true, true, (short)-1, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(3253), "GroupId", new Guid("b3775df7-ea1d-451c-8861-2c2e126bcd9d"), "number", "int", "", null, "AlwaysHidden" },
                    { 4, true, true, (short)-1, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(3273), "CurrencyId", new Guid("10371955-3e02-465a-8c39-a3f9cc6621cb"), "number", "int", "", null, "AlwaysHidden" },
                    { 5, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(3291), "RowNo", new Guid("5ea9f107-3f3c-494a-9288-a65a6a93b26c"), "number", "int", "", null, "AlwaysVisible" },
                    { 6, true, true, (short)1, "System.String", "", "", false, false, false, 16, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(3323), "Code", new Guid("dbfc7f2d-e581-4b61-a388-c0ba493954f9"), "string", "nvarchar", "", null, "Visible" },
                    { 7, true, true, (short)2, "System.String", "", "", false, false, false, 256, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(3340), "FullCode", new Guid("df3543ee-1cba-4e6a-94e9-aa74808ef8ad"), "string", "nvarchar", "", null, "Visible" },
                    { 8, true, true, (short)3, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(3379), "Name", new Guid("077e6169-f42c-4351-825c-ed9679068ade"), "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 9, true, true, (short)-1, "System.Int16", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(3398), "Level", new Guid("a8ded709-c62c-420d-a269-7e51003732ec"), "number", "smallint", "", null, "AlwaysHidden" },
                    { 10, true, true, (short)4, "System.String", "", "", false, false, true, 512, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(3418), "Description", new Guid("94dc8882-bc6a-4b54-a205-cea60ecf8283"), "string", "nvarchar", "", null, "Visible" },
                    { 11, true, true, (short)5, "System.String", "", "", false, false, true, 32, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(3623), "State", new Guid("afa60d0f-f289-454c-a13f-37f168820de2"), "string", "nvarchar", "", null, "Visible" },
                    { 12, true, true, (short)6, "System.Boolean", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(3645), "IsCurrencyAdjustable", new Guid("1dd31f8e-20cc-49a9-bb46-51d35e8c81f6"), "boolean", "bit", "", null, "Hidden" },
                    { 13, true, true, (short)7, "System.String", "", "", false, false, true, 32, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(3664), "TurnoverMode", new Guid("8ef2fca1-5ae6-45d6-af27-2c5c84cf4752"), "string", "nvarchar", "", null, "Hidden" },
                    { 14, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(3681), "Id", new Guid("a9d585de-e06b-4164-8ea5-5e6fbd821cb7"), "number", "int", "", null, "AlwaysHidden" },
                    { 15, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(3736), "StatusId", new Guid("5c018ccb-03e7-465f-9485-6f5f783dbd9c"), "number", "int", "", null, "AlwaysHidden" },
                    { 16, true, true, (short)-1, "System.Int16", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(3828), "Type", new Guid("d56c14c6-a2ef-4ef1-9649-77c32993e112"), "number", "smallint", "", null, "AlwaysHidden" },
                    { 17, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(3847), "RowNo", new Guid("3aff5b0a-c685-49aa-8c69-18a14a6107ca"), "number", "int", "", null, "AlwaysVisible" },
                    { 18, true, true, (short)1, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(3870), "No", new Guid("6c8cf669-703d-45c2-ab3f-fa0a4674fa0a"), "number", "int", "", null, "AlwaysVisible" },
                    { 19, true, true, (short)2, "System.Date", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(3889), "Date", new Guid("e5b78fef-1c28-429a-9124-2953361da529"), "Date", "datetime", "Default", null, "Visible" },
                    { 20, true, true, (short)3, "System.String", "", "", false, false, true, 512, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(3905), "Description", new Guid("e21abdd0-f34c-44c3-8cf0-0b11ef6485fd"), "string", "nvarchar", "", null, "Visible" },
                    { 21, true, true, (short)4, "System.String", "", "", false, false, true, 64, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(3922), "StatusName", new Guid("28698299-91b5-47c5-bc0c-ffe6e07b7b9c"), "string", "nvarchar", "", null, "Visible" },
                    { 22, true, true, (short)5, "System.String", "", "", false, false, true, 64, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(3938), "Reference", new Guid("3bfcb848-6ca7-4a64-935b-dc77044b6d5f"), "string", "nvarchar", "", null, "Visible" },
                    { 23, true, true, (short)6, "System.String", "", "", false, false, true, 64, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(3955), "Association", new Guid("afb260f5-c3b4-419b-af68-23a00dce15be"), "string", "nvarchar", "", null, "Visible" },
                    { 24, true, true, (short)7, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(3977), "DailyNo", new Guid("b36a5189-a340-4871-a2d1-35cba5d7705c"), "number", "int", "", null, "Visible" },
                    { 25, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(4144), "Id", new Guid("65bb7542-7738-427b-92ca-2dca5bdb1f6f"), "number", "int", "", null, "AlwaysHidden" },
                    { 55, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(4823), "RowNo", new Guid("fa9a447d-2c5a-4835-a3c7-20d554d86266"), "number", "int", "", null, "AlwaysVisible" },
                    { 56, true, true, (short)1, "System.String", "", "", false, false, false, 64, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(4840), "Name", new Guid("611d4a83-2ccb-4283-b52f-1748b4c6ffd3"), "string", "nvarchar(64)", "", null, "AlwaysVisible" },
                    { 57, true, true, (short)2, "System.String", "", "", false, false, true, 512, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(5042), "Description", new Guid("b069ea28-d9c0-45a2-912f-ba4e36c1e1c9"), "string", "nvarchar(512)", "", null, "Visible" },
                    { 58, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(5064), "Id", new Guid("ccd2fd7b-9717-47e1-86c4-3db291a9a3f5"), "number", "int", "", null, "AlwaysHidden" },
                    { 88, true, true, (short)4, "System.String", "", "", false, false, true, 512, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(5718), "Description", new Guid("d9d5f409-f360-45d5-8195-56d945303a26"), "string", "nvarchar", "", null, "Visible" },
                    { 89, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(5739), "Id", new Guid("53f7d405-0d30-4fcc-8285-b48133f02eda"), "number", "int", "", null, "AlwaysHidden" }
                });

            migrationBuilder.InsertData(
                schema: "Metadata",
                table: "Column",
                columns: new[] { "ColumnID", "AllowFiltering", "AllowSorting", "DisplayIndex", "DotNetType", "Expression", "GroupName", "IsDynamic", "IsFixedLength", "IsNullable", "Length", "MinLength", "ModifiedDate", "Name", "rowguid", "ScriptType", "StorageType", "Type", "ViewID", "Visibility" },
                values: new object[,]
                {
                    { 90, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(5755), "RowNo", new Guid("7cd5e51d-d3e3-4b9d-810a-075e4d55ab51"), "number", "int", "", null, "AlwaysVisible" },
                    { 91, true, true, (short)1, "System.String", "", "", false, false, false, 128, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(5771), "Name", new Guid("aeb98db8-2692-42b7-a911-d075cea72fe1"), "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 92, true, true, (short)2, "System.String", "", "", false, false, true, 512, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(5787), "Description", new Guid("5a2a8139-037e-4bc1-8899-64f8299f251a"), "string", "nvarchar", "", null, "Visible" },
                    { 93, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(5804), "Id", new Guid("5fcab088-f3c9-4e69-822f-8519bcb4da24"), "number", "int", "", null, "AlwaysHidden" },
                    { 94, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(5820), "RowNo", new Guid("49fadcb8-86b3-49d7-b81c-0e0fad102659"), "number", "int", "", null, "AlwaysVisible" },
                    { 95, true, true, (short)1, "System.String", "", "", false, false, false, 128, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(5836), "Name", new Guid("cc070b62-1217-40c0-afe9-4a1f26f82cc3"), "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 96, true, true, (short)2, "System.String", "", "", false, false, false, 128, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(5852), "DbName", new Guid("47e8bfd4-4a9b-412b-aef0-8545ec908cd8"), "string", "nvarchar", "", null, "Visible" },
                    { 97, true, true, (short)5, "System.String", "", "", false, false, true, 512, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(5873), "Description", new Guid("166ee606-6da1-426d-8f1b-e648b60ffce2"), "string", "nvarchar", "", null, "Visible" },
                    { 98, true, true, (short)3, "System.String", "", "", false, false, true, 64, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(5889), "Server", new Guid("844ba310-3d57-45e4-8d65-0056745927a7"), "string", "nvarchar", "", null, "Visible" },
                    { 99, true, true, (short)4, "System.String", "", "", false, false, true, 32, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(5905), "UserName", new Guid("e27c6b8f-1591-488d-89f4-965ca8e41530"), "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 100, true, true, (short)-1, "System.String", "", "", false, false, true, 32, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(5922), "Password", new Guid("53d043e6-9183-43c2-963c-baf914e3405c"), "string", "nvarchar", "", null, "AlwaysHidden" },
                    { 101, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(5938), "Id", new Guid("901e378c-af62-4cf6-a1e4-fb3ab70d8c03"), "number", "int", "", null, "AlwaysHidden" },
                    { 102, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(5954), "RowNo", new Guid("cf69ef04-bcb0-46f1-9215-9d2e991e0248"), "number", "int", "", null, "AlwaysVisible" },
                    { 103, true, true, (short)1, "System.String", "", "", false, false, false, 64, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(5969), "Name", new Guid("e4b56876-2682-4fc6-8119-4c8526e7534a"), "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 104, true, true, (short)2, "System.String", "", "", false, false, false, 64, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(5985), "Category", new Guid("9fda607a-a527-47dc-8b88-b4be00f685e7"), "string", "nvarchar", "", null, "Visible" },
                    { 105, true, true, (short)3, "System.String", "", "", false, false, true, 256, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(6006), "Description", new Guid("91eb1b42-8c67-4bd5-b0f1-2b37d47bb793"), "string", "nvarchar", "", null, "Visible" },
                    { 106, true, true, (short)-1, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(6022), "Id", new Guid("df2efab0-0821-4a84-80bc-b23c49e042ce"), "number", "int", "", null, "AlwaysHidden" },
                    { 107, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(6040), "RowNo", new Guid("b2d67fce-e6a7-427f-956f-b50c7cb6c165"), "number", "int", "", null, "AlwaysVisible" },
                    { 108, true, true, (short)1, "System.String", "", "", false, false, false, 64, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(6056), "UserName", new Guid("e8d33752-0af0-4601-b6df-73ffb8b92b53"), "string", "nvarchar", "", null, "Visible" },
                    { 109, true, true, (short)2, "System.String", "", "", false, false, false, 64, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(6072), "BranchName", new Guid("cccf8fc0-2e33-4cd3-a489-0137c2aa50dd"), "string", "nvarchar", "", null, "Visible" },
                    { 110, true, true, (short)3, "System.String", "", "", false, false, false, 64, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(6087), "FiscalPeriodName", new Guid("212f2bdb-c1c5-412f-8aec-b1dba6053c14"), "string", "nvarchar", "", null, "Visible" },
                    { 111, true, true, (short)4, "System.Date", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(6104), "Date", new Guid("17f6b9ef-d931-4c5a-a0e0-92a59d0f9f0d"), "Date", "datetime", "Default", null, "AlwaysVisible" },
                    { 112, true, true, (short)5, "System.TimeSpan", "", "", false, false, false, 7, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(6120), "Time", new Guid("a56aec04-8b4e-426f-b6f2-b562bf3d3d5a"), "Date", "time", "", null, "AlwaysVisible" },
                    { 87, true, true, (short)3, "System.Date", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(5702), "EndDate", new Guid("29bccef8-c433-4956-acc6-b54b16dbb1e8"), "Date", "datetime", "Default", null, "Visible" },
                    { 227, true, true, (short)2, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(8485), "VoucherNo", new Guid("66e67275-994a-4d35-bc46-6441ce665452"), "number", "int", "", null, "Visible" },
                    { 86, true, true, (short)2, "System.Date", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(5686), "StartDate", new Guid("d63e4569-3a5a-4d59-b98e-aa4b0318c87e"), "Date", "datetime", "Default", null, "Visible" },
                    { 84, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(5553), "RowNo", new Guid("0b3b8ebd-f4c3-41d8-8ae8-ae208fee8a6f"), "number", "int", "", null, "AlwaysVisible" },
                    { 59, true, true, (short)-1, "System.Int16", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(5083), "Level", new Guid("d2ce5751-03c3-4810-bdbd-770a721899de"), "", "smallint", "", null, "AlwaysHidden" },
                    { 60, true, false, (short)-1, "System.Int16", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(5099), "BranchScope", new Guid("27437ecd-6904-481c-9818-12f22a979365"), "number", "smallint", "", null, "AlwaysHidden" },
                    { 61, true, true, (short)-1, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(5115), "CurrencyId", new Guid("b146460a-0097-41b2-bd20-68f33b29bafc"), "number", "int", "", null, "AlwaysHidden" },
                    { 62, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(5132), "RowNo", new Guid("77cfd630-27e7-4d54-b3e1-28950efa2e67"), "number", "int", "", null, "AlwaysVisible" },
                    { 63, true, true, (short)1, "System.String", "", "", false, false, false, 16, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(5149), "Code", new Guid("7809fd35-6aab-4fee-a39a-ffddf00c488c"), "string", "nvarchar", "", null, "Visible" },
                    { 64, true, true, (short)2, "System.String", "", "", false, false, false, 256, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(5165), "FullCode", new Guid("757ea7d5-789e-499e-a5d6-77a75439a0ff"), "string", "nvarchar", "", null, "Visible" },
                    { 65, true, true, (short)3, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(5187), "Name", new Guid("1239970d-8f3b-4e2e-8dbe-21e7179c134e"), "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 66, true, true, (short)4, "System.String", "", "", false, false, true, 512, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(5206), "Description", new Guid("7971499f-da8b-4968-85cb-f8c16eb5136c"), "string", "nvarchar", "", null, "Visible" },
                    { 67, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(5227), "Id", new Guid("ab0de854-4dfc-4ee4-ad90-9770d991e3bf"), "number", "int", "", null, "AlwaysHidden" },
                    { 68, true, true, (short)-1, "System.Int16", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(5243), "Level", new Guid("3b142d73-8dd3-412e-a14c-cbd7f019e961"), "", "smallint", "", null, "AlwaysHidden" },
                    { 69, true, false, (short)-1, "System.Int16", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(5264), "BranchScope", new Guid("dec09da8-38cc-4087-865a-58a1fdb553cf"), "number", "smallint", "", null, "AlwaysHidden" },
                    { 70, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(5281), "RowNo", new Guid("ef3cccef-a843-45ce-989b-7b136b0833bb"), "number", "int", "", null, "AlwaysVisible" },
                    { 71, true, true, (short)1, "System.String", "", "", false, false, false, 16, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(5298), "Code", new Guid("06b1b3e2-9f6e-4921-a7f8-0a0a2d1adef8"), "string", "nvarchar", "", null, "Visible" },
                    { 72, true, true, (short)2, "System.String", "", "", false, false, false, 256, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(5316), "FullCode", new Guid("61f8363a-6b05-4677-b81d-b939a62f8892"), "string", "nvarchar", "", null, "Visible" },
                    { 73, true, true, (short)3, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(5346), "Name", new Guid("50e7060a-20a8-4703-af2a-71ebd14e135d"), "string", "nvarchar", "", null, "AlwaysVisible" }
                });

            migrationBuilder.InsertData(
                schema: "Metadata",
                table: "Column",
                columns: new[] { "ColumnID", "AllowFiltering", "AllowSorting", "DisplayIndex", "DotNetType", "Expression", "GroupName", "IsDynamic", "IsFixedLength", "IsNullable", "Length", "MinLength", "ModifiedDate", "Name", "rowguid", "ScriptType", "StorageType", "Type", "ViewID", "Visibility" },
                values: new object[,]
                {
                    { 74, true, true, (short)4, "System.String", "", "", false, false, true, 512, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(5364), "Description", new Guid("5c98b1b2-c5c4-4d0f-ba60-11a950d00e3d"), "string", "nvarchar", "", null, "Visible" },
                    { 75, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(5384), "Id", new Guid("acbc02c0-9b8d-4fc7-9a23-efd4779fb8ad"), "number", "int", "", null, "AlwaysHidden" },
                    { 76, true, true, (short)-1, "System.Int16", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(5404), "Level", new Guid("6a87b9af-e2c3-478e-b376-b128431c6240"), "", "smallint", "", null, "AlwaysHidden" },
                    { 77, true, false, (short)-1, "System.Int16", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(5421), "BranchScope", new Guid("7ef63a67-27d0-47b9-8afb-bfb15b5e32c6"), "number", "smallint", "", null, "AlwaysHidden" },
                    { 78, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(5440), "RowNo", new Guid("2efb53f0-fb3f-4cbf-8297-5c8607a485c0"), "number", "int", "", null, "AlwaysVisible" },
                    { 79, true, true, (short)1, "System.String", "", "", false, false, false, 16, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(5459), "Code", new Guid("c01f9503-c887-443b-9a90-02079b1afbe6"), "string", "nvarchar", "", null, "Visible" },
                    { 80, true, true, (short)2, "System.String", "", "", false, false, false, 256, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(5477), "FullCode", new Guid("8b3f0892-90e2-435c-810e-8bc7ee9ec0f5"), "string", "nvarchar", "", null, "Visible" },
                    { 81, true, true, (short)3, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(5500), "Name", new Guid("434669e5-943c-4a5a-91a2-c5d26816919c"), "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 82, true, true, (short)4, "System.String", "", "", false, false, true, 512, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(5519), "Description", new Guid("3e4544e3-7504-4d9b-bc05-a7ed7de41141"), "string", "nvarchar", "", null, "Visible" },
                    { 83, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(5537), "Id", new Guid("3f10e684-04cc-43bc-88b9-296ee5d71581"), "number", "int", "", null, "AlwaysHidden" },
                    { 85, true, true, (short)1, "System.String", "", "", false, false, false, 64, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(5568), "Name", new Guid("bb63684e-5e22-4bab-927f-7db0d5b022cf"), "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 228, true, true, (short)3, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(8503), "AccountFullCode", new Guid("cda6e54e-8184-4894-b616-fbd4cc311692"), "string", "nvarchar", "", null, "Visible" },
                    { 163, true, true, (short)5, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(7194), "Description", new Guid("9f415133-6e7f-4f48-90f4-32f3d22f0f78"), "string", "nvarchar", "", null, "Visible" },
                    { 230, true, true, (short)5, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(8537), "Description", new Guid("a5f2519b-eb42-4938-9b2f-780e7c3c7bfa"), "string", "nvarchar", "", null, "Visible" },
                    { 375, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(1797), "RowNo", new Guid("f1a0aae7-ab19-45a9-a9ab-41402be50160"), "number", "int", "", null, "AlwaysVisible" },
                    { 376, true, true, (short)1, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(1812), "LineCount", new Guid("e030b4a6-4bb6-4a61-861a-d3f11477e52f"), "number", "int", "", null, "Visible" },
                    { 377, true, true, (short)2, "System.Date", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(1828), "VoucherDate", new Guid("78bde5c0-7080-4021-b471-e666ae2845c5"), "Date", "datetime", "Default", null, "Visible" },
                    { 378, true, true, (short)3, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(1852), "Description", new Guid("04e87d7f-45d2-41cb-aebc-3a9aad248e0c"), "string", "nvarchar", "", null, "Visible" },
                    { 379, true, true, (short)4, "System.Decimal", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(1868), "BaseCurrencyDebit", new Guid("24fcffa1-badc-46ed-acb8-17a2fbec08bf"), "number", "money", "Money", null, "Visible" },
                    { 380, true, true, (short)5, "System.Decimal", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(1883), "BaseCurrencyCredit", new Guid("adcadebb-28c5-4e32-af73-4512baa73a5e"), "number", "money", "Money", null, "Visible" },
                    { 381, true, true, (short)6, "System.Decimal", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(1899), "BaseCurrencyBalance", new Guid("628e7a7a-5db7-402d-93f5-fb023bddb844"), "number", "money", "Money", null, "Visible" },
                    { 382, true, true, (short)7, "System.Decimal", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(1915), "Debit", new Guid("d1032b2e-0383-406e-b6ef-997d75794165"), "number", "money", "Money", null, "Visible" },
                    { 383, true, true, (short)8, "System.Decimal", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(1932), "Credit", new Guid("e88ff924-31fa-4450-a0c3-c20968e31a21"), "number", "money", "Money", null, "Visible" },
                    { 384, true, true, (short)9, "System.Decimal", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(1949), "Balance", new Guid("95a10b68-4e90-4fb8-8ad1-8edc1578c60a"), "number", "money", "Money", null, "Visible" },
                    { 385, true, true, (short)10, "System.String", "", "", false, false, false, 128, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(1965), "BranchName", new Guid("363d27bf-bc1a-4bf1-b499-1693e434b95e"), "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 374, true, true, (short)11, "System.String", "", "", false, false, false, 128, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(1781), "BranchName", new Guid("f965fca1-51b5-48c5-88ff-afaef1931cbf"), "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 386, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(1986), "RowNo", new Guid("70e5f896-9d0f-4c6b-8e6a-9fc3d93f702d"), "number", "int", "", null, "AlwaysVisible" },
                    { 388, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(2020), "Id", new Guid("2b2cbad8-3fa5-4f30-b4ed-b0aea74dfc0c"), "number", "int", "", null, "AlwaysHidden" },
                    { 389, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(2036), "BranchId", new Guid("2b2ae061-319e-47e7-b277-2e01df1fbcef"), "number", "int", "", null, "AlwaysHidden" },
                    { 390, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(2053), "RowNo", new Guid("e948f1b1-4fc9-4ee7-8081-dd64a426e1b8"), "number", "int", "", null, "AlwaysVisible" },
                    { 391, true, true, (short)1, "System.String", "", "", false, false, false, 256, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(2170), "AccountFullCode", new Guid("39ef0c47-594a-4887-be17-33a919fe804a"), "string", "nvarchar", "", null, "Visible" },
                    { 392, true, true, (short)2, "System.String", "", "", false, false, false, 128, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(2190), "AccountName", new Guid("d11cf2fe-352d-4f19-95a7-b5b66cae4923"), "string", "nvarchar", "", null, "Visible" },
                    { 393, true, true, (short)3, "System.Date", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(2206), "VoucherDate", new Guid("aa3821d9-6533-4c39-a82d-a6c393254169"), "Date", "datetime", "Default", null, "Visible" },
                    { 394, true, true, (short)4, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(2229), "VoucherNo", new Guid("6b752830-3167-4515-8fac-0c6e76fdf35f"), "number", "int", "", null, "AlwaysVisible" },
                    { 395, true, true, (short)5, "System.String", "", "", false, false, false, 64, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(2245), "VoucherReference", new Guid("c6b8c8c4-175c-40c4-ad02-061a912cb2f3"), "string", "nvarchar", "", null, "Visible" },
                    { 396, true, true, (short)6, "System.Decimal", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(2261), "Debit", new Guid("cd99e44a-d919-47fe-97b5-ed2c0ac6a263"), "number", "money", "Money", null, "Visible" },
                    { 397, true, true, (short)7, "System.Decimal", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(2276), "Credit", new Guid("927e90fa-841e-4d96-8c4f-b7b7a20b4238"), "number", "money", "Money", null, "Visible" },
                    { 398, true, true, (short)8, "System.String", "", "", false, false, true, 512, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(2294), "Description", new Guid("8d01bcc2-19d4-4328-ac81-433f6fce39ef"), "string", "nvarchar", "", null, "Visible" },
                    { 387, true, true, (short)1, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(2004), "Number", new Guid("8aaa1c2b-55a8-4a83-9f2c-ad42e43f7411"), "number", "int", "", null, "AlwaysVisible" },
                    { 373, true, true, (short)10, "System.Decimal", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(1765), "Balance", new Guid("a8382325-b596-4040-b887-d4b280c1fecc"), "number", "money", "Money", null, "Visible" },
                    { 372, true, true, (short)9, "System.Decimal", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(1745), "Credit", new Guid("2040362a-82e7-4937-adc6-cc0ab0743952"), "number", "money", "Money", null, "Visible" },
                    { 371, true, true, (short)8, "System.Decimal", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(1730), "Debit", new Guid("3818bfa6-cbdb-4d49-84e8-aafbb5390e37"), "number", "money", "Money", null, "Visible" }
                });

            migrationBuilder.InsertData(
                schema: "Metadata",
                table: "Column",
                columns: new[] { "ColumnID", "AllowFiltering", "AllowSorting", "DisplayIndex", "DotNetType", "Expression", "GroupName", "IsDynamic", "IsFixedLength", "IsNullable", "Length", "MinLength", "ModifiedDate", "Name", "rowguid", "ScriptType", "StorageType", "Type", "ViewID", "Visibility" },
                values: new object[,]
                {
                    { 346, true, true, (short)2, "System.Decimal", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(1195), "Debit", new Guid("f9e49509-ee68-453d-b803-d0280c761517"), "number", "money", "Money", null, "Visible" },
                    { 347, true, true, (short)3, "System.Decimal", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(1211), "Credit", new Guid("8fb05d52-fa81-42d7-b374-dafe1a617eee"), "number", "money", "Money", null, "Visible" },
                    { 348, true, true, (short)4, "System.Decimal", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(1228), "Balance", new Guid("e1e56eae-64d8-4858-b6b3-49e4bd3eca92"), "number", "money", "Money", null, "Visible" },
                    { 349, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(1246), "RowNo", new Guid("5badee60-7900-44ef-882a-a8a1d2a9b948"), "number", "int", "", null, "AlwaysVisible" },
                    { 350, true, true, (short)1, "System.Date", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(1262), "VoucherDate", new Guid("8aa499a2-a699-4185-bb55-55065fdc73b6"), "Date", "datetime", "Default", null, "Visible" },
                    { 351, true, true, (short)2, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(1278), "VoucherNo", new Guid("9ed482b8-cd0f-43f3-b2a8-2d3747bea1ba"), "number", "int", "", null, "Visible" },
                    { 352, true, true, (short)3, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(1294), "Description", new Guid("5b3c14e0-9762-4ee6-b9e4-95d5e0834e37"), "string", "nvarchar", "", null, "Visible" },
                    { 353, true, true, (short)4, "System.String", "", "", false, false, false, 64, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(1310), "Reference", new Guid("eaa769e8-5c50-47b4-a2fd-74108586e2d7"), "string", "nvarchar", "", null, "Visible" },
                    { 354, true, true, (short)5, "System.Decimal", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(1331), "BaseCurrencyDebit", new Guid("f32c01b1-5756-4270-9ad6-f75d96bbe331"), "number", "money", "Money", null, "Visible" },
                    { 355, true, true, (short)6, "System.Decimal", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(1348), "BaseCurrencyCredit", new Guid("fd34c45b-ca55-4ed9-b3b5-1a7d9c033353"), "number", "money", "Money", null, "Visible" },
                    { 356, true, true, (short)7, "System.Decimal", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(1364), "BaseCurrencyBalance", new Guid("ddbe04b8-ec00-4be1-a795-f41d5134f192"), "number", "money", "Money", null, "Visible" },
                    { 357, true, true, (short)8, "System.Decimal", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(1383), "Debit", new Guid("7ebc95f7-b277-4296-9404-d50d184567e9"), "number", "money", "Money", null, "Visible" },
                    { 358, true, true, (short)9, "System.Decimal", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(1500), "Credit", new Guid("5d55a4b9-c910-4ca7-a6ea-c6117f0cc20e"), "number", "money", "Money", null, "Visible" },
                    { 359, true, true, (short)10, "System.Decimal", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(1520), "Balance", new Guid("36ecabc9-59f6-4fc6-ab06-798f814457d7"), "number", "money", "Money", null, "Visible" },
                    { 360, true, true, (short)11, "System.Decimal", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(1536), "CurrencyRate", new Guid("05dfc76c-1001-4580-9fc9-9ba6285b1707"), "number", "money", "Money", null, "Visible" },
                    { 361, true, true, (short)12, "System.String", "", "", false, false, false, 128, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(1554), "Mark", new Guid("5ffa5ff7-6723-4915-a9f5-8e3a05141d39"), "string", "nvarchar", "", null, "Visible" },
                    { 362, true, true, (short)13, "System.String", "", "", false, false, false, 128, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(1575), "BranchName", new Guid("dc9b137f-7171-4b10-b6c8-ac3963c401c1"), "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 363, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(1592), "RowNo", new Guid("42c9e90a-a5b8-4acb-90bd-f0245b652f70"), "number", "int", "", null, "AlwaysVisible" },
                    { 364, true, true, (short)1, "System.Date", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(1611), "VoucherDate", new Guid("6496cb23-9774-46fa-81bc-5b350a788d48"), "Date", "datetime", "Default", null, "Visible" },
                    { 365, true, true, (short)2, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(1627), "VoucherNo", new Guid("abaf27a3-328b-4c01-8de8-2b21d5d88711"), "number", "int", "", null, "Visible" },
                    { 366, true, true, (short)3, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(1643), "Description", new Guid("690ca55b-f2e2-4a97-b249-6736f00c7f42"), "string", "nvarchar", "", null, "Visible" },
                    { 367, true, true, (short)4, "System.String", "", "", false, false, false, 64, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(1660), "Reference", new Guid("ffda8ef2-38ae-49a6-95b0-c357210efca4"), "string", "nvarchar", "", null, "Visible" },
                    { 368, true, true, (short)5, "System.Decimal", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(1676), "BaseCurrencyDebit", new Guid("ba6473c3-8845-40ef-8a9c-564e3910dc1c"), "number", "money", "Money", null, "Visible" },
                    { 369, true, true, (short)6, "System.Decimal", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(1692), "BaseCurrencyCredit", new Guid("8d0dde95-3351-409c-b426-3d95e51da8e1"), "number", "money", "Money", null, "Visible" },
                    { 370, true, true, (short)7, "System.Decimal", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(1714), "BaseCurrencyBalance", new Guid("3c20b431-7fc3-45c3-95f8-409a7991f31c"), "number", "money", "Money", null, "Visible" },
                    { 399, true, true, (short)9, "System.String", "", "", false, false, false, 128, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(2310), "DetailAccountFullCode", new Guid("9032d53e-9bba-4f71-9a61-3e3e1488ec30"), "string", "nvarchar", "", null, "Hidden" },
                    { 345, true, true, (short)1, "System.String", "", "", false, false, false, 128, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(1170), "CurrencyName", new Guid("3a24bf9a-c086-4041-b22b-34e782e724cb"), "string", "nvarchar", "", null, "Visible" },
                    { 400, true, true, (short)10, "System.String", "", "", false, false, false, 128, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(2327), "DetailAccountName", new Guid("d7cf843b-b896-4047-94fb-c041102d7418"), "string", "nvarchar", "", null, "Hidden" },
                    { 402, true, true, (short)12, "System.String", "", "", false, false, false, 128, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(2367), "CostCenterName", new Guid("7d1fa781-bec4-4201-8f3d-2c8d16ae40e0"), "string", "nvarchar", "", null, "Hidden" },
                    { 432, true, true, (short)9, "System.String", "", "", false, false, false, 128, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(2980), "BranchName", new Guid("1ab886f6-c5f9-48e7-ac1f-194579d15e80"), "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 433, true, true, (short)-1, "System.String", "", "", false, false, true, 64, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(2998), "VoucherReference", new Guid("08e74edf-e6f9-4f8d-982c-98ddb6f6e925"), "string", "nvarchar", "", null, "AlwaysHidden" },
                    { 434, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(3019), "RowNo", new Guid("97ce39f9-a322-476e-b912-15866b1e08c0"), "number", "int", "", null, "AlwaysVisible" },
                    { 435, true, true, (short)1, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(3035), "DetailAccountFullCode", new Guid("eb1321a4-876d-4976-94a7-4512d8eb11c5"), "string", "nvarchar", "", null, "Visible" },
                    { 436, true, true, (short)2, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(3051), "DetailAccountName", new Guid("c19ee4a0-2c47-4b34-8249-34d560eae687"), "string", "nvarchar", "", null, "Visible" },
                    { 437, true, true, (short)3, "System.Decimal", "", "StartBalance", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(3067), "StartBalanceDebit", new Guid("af75fd58-4f85-4128-95a2-a1a003f1faa5"), "number", "money", "Money", null, "Visible" },
                    { 438, true, true, (short)4, "System.Decimal", "", "StartBalance", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(3085), "StartBalanceCredit", new Guid("4b6882f7-af66-49a7-8751-bb9d8c53f894"), "number", "money", "Money", null, "Visible" },
                    { 439, true, true, (short)5, "System.Decimal", "", "Turnover", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(3102), "TurnoverDebit", new Guid("35cd12b2-39c3-4ad3-9eb1-b5f56e30cf8a"), "number", "money", "Money", null, "Visible" },
                    { 440, true, true, (short)6, "System.Decimal", "", "Turnover", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(3118), "TurnoverCredit", new Guid("36ccbb38-e4de-44e0-95fd-a5d4c973c5f9"), "number", "money", "Money", null, "Visible" },
                    { 441, true, true, (short)7, "System.Decimal", "", "OperationSum", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(3134), "OperationSumDebit", new Guid("fd4d3418-8a06-45ca-a33e-a8dfcbfefd0d"), "number", "money", "Money", null, "Visible" },
                    { 442, true, true, (short)8, "System.Decimal", "", "OperationSum", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(3156), "OperationSumCredit", new Guid("a3ba5df1-1a68-4ca4-9982-1e3dc7db92ee"), "number", "money", "Money", null, "Visible" },
                    { 431, true, true, (short)8, "System.Decimal", "", "EndBalance", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(2962), "EndBalanceCredit", new Guid("89fd1d4d-2439-4562-8837-06222d2cb0dd"), "number", "money", "Money", null, "Visible" },
                    { 443, true, true, (short)9, "System.Decimal", "", "EndBalance", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(3172), "EndBalanceDebit", new Guid("b687ef9a-3df1-482e-9f01-02dc174cdd85"), "number", "money", "Money", null, "Visible" }
                });

            migrationBuilder.InsertData(
                schema: "Metadata",
                table: "Column",
                columns: new[] { "ColumnID", "AllowFiltering", "AllowSorting", "DisplayIndex", "DotNetType", "Expression", "GroupName", "IsDynamic", "IsFixedLength", "IsNullable", "Length", "MinLength", "ModifiedDate", "Name", "rowguid", "ScriptType", "StorageType", "Type", "ViewID", "Visibility" },
                values: new object[,]
                {
                    { 445, true, true, (short)11, "System.String", "", "", false, false, false, 128, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(3205), "BranchName", new Guid("3e1f9922-ad61-442e-8072-f103775aa5c3"), "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 446, true, true, (short)-1, "System.String", "", "", false, false, true, 64, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(3220), "VoucherReference", new Guid("2416b38a-0a2d-4a34-826b-eff0901b56c3"), "string", "nvarchar", "", null, "AlwaysHidden" },
                    { 447, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(3236), "RowNo", new Guid("4b59c4e4-1091-49d8-92f7-ce582879c4e3"), "number", "int", "", null, "AlwaysVisible" },
                    { 448, true, true, (short)1, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(3252), "DetailAccountFullCode", new Guid("a1841a34-bdd2-42e7-bc35-2c0a56733c5a"), "string", "nvarchar", "", null, "Visible" },
                    { 449, true, true, (short)2, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(3269), "DetailAccountName", new Guid("431db1d5-9f28-454b-ba59-34f7cd3ec0a2"), "string", "nvarchar", "", null, "Visible" },
                    { 450, true, true, (short)3, "System.Decimal", "", "StartBalance", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(3291), "StartBalanceDebit", new Guid("2ed5d0c6-7909-41f6-97da-06079f01be3e"), "number", "money", "Money", null, "Visible" },
                    { 451, true, true, (short)4, "System.Decimal", "", "StartBalance", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(3307), "StartBalanceCredit", new Guid("9f61008f-50e0-4919-9373-144f2646bb71"), "number", "money", "Money", null, "Visible" },
                    { 452, true, true, (short)5, "System.Decimal", "", "Turnover", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(3326), "TurnoverDebit", new Guid("4aadc9a4-f935-462c-a050-19a8676f5eca"), "number", "money", "Money", null, "Visible" },
                    { 453, true, true, (short)6, "System.Decimal", "", "Turnover", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(3342), "TurnoverCredit", new Guid("6c380b2e-03c2-4e32-9d6a-b96041da277e"), "number", "money", "Money", null, "Visible" },
                    { 454, true, true, (short)7, "System.Decimal", "", "OperationSum", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(3358), "OperationSumDebit", new Guid("0c70e4e3-d34b-472c-9828-4e92168504a9"), "number", "money", "Money", null, "Visible" },
                    { 455, true, true, (short)8, "System.Decimal", "", "OperationSum", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(3373), "OperationSumCredit", new Guid("4c255a8a-f0e9-424d-b5f5-dde33173c4c9"), "number", "money", "Money", null, "Visible" },
                    { 444, true, true, (short)10, "System.Decimal", "", "EndBalance", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(3187), "EndBalanceCredit", new Guid("f5b3feb4-a983-4330-91c3-20c715432c1b"), "number", "money", "Money", null, "Visible" },
                    { 430, true, true, (short)7, "System.Decimal", "", "EndBalance", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(2946), "EndBalanceDebit", new Guid("db8f25ad-2d4c-4c48-b57c-2103d852689c"), "number", "money", "Money", null, "Visible" },
                    { 429, true, true, (short)6, "System.Decimal", "", "Turnover", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(2929), "TurnoverCredit", new Guid("0d30e864-b34c-45fc-b855-68ab86923c07"), "number", "money", "Money", null, "Visible" },
                    { 428, true, true, (short)5, "System.Decimal", "", "Turnover", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(2913), "TurnoverDebit", new Guid("d13fd32b-6006-4214-90a6-d3b86d167c4d"), "number", "money", "Money", null, "Visible" },
                    { 403, true, true, (short)13, "System.String", "", "", false, false, false, 128, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(2383), "ProjectFullCode", new Guid("f9a1126f-e8c8-4626-a676-9a9156174a16"), "string", "nvarchar", "", null, "Hidden" },
                    { 404, true, true, (short)14, "System.String", "", "", false, false, false, 128, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(2399), "ProjectName", new Guid("dc9a5048-08b2-4a6d-8493-e0e8f1f6aee7"), "string", "nvarchar", "", null, "Hidden" },
                    { 405, true, true, (short)15, "System.String", "", "", false, false, false, 64, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(2418), "CurrencyName", new Guid("d0b565cc-7b4d-4bfa-9665-a2494f89ea03"), "string", "nvarchar", "", null, "Hidden" },
                    { 406, true, true, (short)16, "System.Decimal", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(2434), "CurrencyValue", new Guid("6812dce5-d5a1-4d18-ac4b-7148047258bd"), "number", "money", "Money", null, "Hidden" },
                    { 407, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(2449), "RowNo", new Guid("0703adae-f6de-45eb-97a5-59449fbdf157"), "number", "int", "", null, "AlwaysVisible" },
                    { 408, true, true, (short)1, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(2465), "DetailAccountFullCode", new Guid("7bb02ea3-5f12-4601-b1a1-42b8987e2fda"), "string", "nvarchar", "", null, "Visible" },
                    { 229, true, true, (short)4, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(8519), "AccountName", new Guid("c4035388-0720-4f32-a9bf-4dd12bb3f783"), "string", "nvarchar", "", null, "Visible" },
                    { 410, true, true, (short)3, "System.Decimal", "", "EndBalance", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(2504), "EndBalanceDebit", new Guid("8bd89b32-5b00-45ed-a2f1-d897b7ef1837"), "number", "money", "Money", null, "Visible" },
                    { 411, true, true, (short)4, "System.Decimal", "", "EndBalance", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(2520), "EndBalanceCredit", new Guid("7562d7f8-6ec7-48f7-9d8c-4ad78e4c3605"), "number", "money", "Money", null, "Visible" },
                    { 412, true, true, (short)5, "System.String", "", "", false, false, false, 128, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(2536), "BranchName", new Guid("cf5fad37-70fd-4256-93e3-2207cb4a9b7c"), "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 413, true, true, (short)-1, "System.String", "", "", false, false, true, 64, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(2551), "VoucherReference", new Guid("57a3dab1-162d-43b6-a99d-5ddadb85053e"), "string", "nvarchar", "", null, "AlwaysHidden" },
                    { 414, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(2567), "RowNo", new Guid("e6f50659-a038-43cf-aa6f-b2acf5dab1ff"), "number", "int", "", null, "AlwaysVisible" },
                    { 415, true, true, (short)1, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(2584), "DetailAccountFullCode", new Guid("07ac813c-b80d-4d86-b456-b0a5392f8156"), "string", "nvarchar", "", null, "Visible" },
                    { 416, true, true, (short)2, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(2602), "DetailAccountName", new Guid("b65bfde5-6a0c-44b6-80d4-3600e9f1c8dd"), "string", "nvarchar", "", null, "Visible" },
                    { 417, true, true, (short)3, "System.Decimal", "", "Turnover", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(2618), "TurnoverDebit", new Guid("dd448972-6423-4fc2-a5b6-2543885fb7eb"), "number", "money", "Money", null, "Visible" },
                    { 418, true, true, (short)4, "System.Decimal", "", "Turnover", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(2641), "TurnoverCredit", new Guid("e8292736-4c33-4dcc-a018-5556acefea1a"), "number", "money", "Money", null, "Visible" },
                    { 419, true, true, (short)5, "System.Decimal", "", "EndBalance", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(2657), "EndBalanceDebit", new Guid("db19c2f0-5bb1-4614-b42a-acdabc4a1c7f"), "number", "money", "Money", null, "Visible" },
                    { 420, true, true, (short)6, "System.Decimal", "", "EndBalance", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(2674), "EndBalanceCredit", new Guid("2f9e0345-e268-4de7-861d-ded49c85dd62"), "number", "money", "Money", null, "Visible" },
                    { 421, true, true, (short)7, "System.String", "", "", false, false, false, 128, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(2690), "BranchName", new Guid("95df438f-51a2-4b3a-9a20-179e12d8fa64"), "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 422, true, true, (short)-1, "System.String", "", "", false, false, true, 64, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(2708), "VoucherReference", new Guid("cbec57e4-f00e-40a9-97f3-fca02b78f6d6"), "string", "nvarchar", "", null, "AlwaysHidden" },
                    { 423, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(2725), "RowNo", new Guid("a1060da3-f301-4d0f-bd98-31c2471e1a9a"), "number", "int", "", null, "AlwaysVisible" },
                    { 424, true, true, (short)1, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(2835), "DetailAccountFullCode", new Guid("0dcef909-03d0-4ac1-bccc-a9de5b7225f8"), "string", "nvarchar", "", null, "Visible" },
                    { 425, true, true, (short)2, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(2856), "DetailAccountName", new Guid("3f147e40-43c2-4a4d-97d3-87c350465397"), "string", "nvarchar", "", null, "Visible" },
                    { 426, true, true, (short)3, "System.Decimal", "", "StartBalance", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(2880), "StartBalanceDebit", new Guid("c03fd586-3d04-4cf0-b20b-c46765234c38"), "number", "money", "Money", null, "Visible" },
                    { 427, true, true, (short)4, "System.Decimal", "", "StartBalance", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(2897), "StartBalanceCredit", new Guid("e1f47920-5b0d-436b-9f3f-b8b8cc119314"), "number", "money", "Money", null, "Visible" },
                    { 401, true, true, (short)11, "System.String", "", "", false, false, false, 128, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(2343), "CostCenterFullCode", new Guid("84ded832-98e7-4627-8be8-0439f40a5d40"), "string", "nvarchar", "", null, "Hidden" },
                    { 344, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(1155), "RowNo", new Guid("2e39df52-f1cf-4759-a8b3-a4b6ff4c1149"), "number", "int", "", null, "AlwaysVisible" }
                });

            migrationBuilder.InsertData(
                schema: "Metadata",
                table: "Column",
                columns: new[] { "ColumnID", "AllowFiltering", "AllowSorting", "DisplayIndex", "DotNetType", "Expression", "GroupName", "IsDynamic", "IsFixedLength", "IsNullable", "Length", "MinLength", "ModifiedDate", "Name", "rowguid", "ScriptType", "StorageType", "Type", "ViewID", "Visibility" },
                values: new object[,]
                {
                    { 409, true, true, (short)2, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(2481), "DetailAccountName", new Guid("a4e2841a-10b0-413e-8939-ea920f41df2a"), "string", "nvarchar", "", null, "Visible" },
                    { 342, true, true, (short)13, "System.String", "", "", false, false, false, 128, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(1121), "BranchName", new Guid("37094ec6-4c45-4172-96da-e1ab78c4ed71"), "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 260, true, true, (short)2, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(9256), "LineCount", new Guid("05f02047-4f64-4135-b58e-9635132295cf"), "number", "int", "", null, "Visible" },
                    { 261, true, true, (short)3, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(9272), "Description", new Guid("381df1a8-167f-460f-bd28-6691617b6f52"), "string", "nvarchar", "", null, "Visible" },
                    { 262, true, true, (short)4, "System.Decimal", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(9290), "Debit", new Guid("17cf7174-fc42-4779-805b-e6c19253017e"), "number", "money", "Money", null, "Visible" },
                    { 263, true, true, (short)5, "System.Decimal", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(9306), "Credit", new Guid("a13453c8-572a-4623-b92a-cfa40b608e27"), "number", "money", "Money", null, "Visible" },
                    { 264, false, false, (short)6, "System.Decimal", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(9322), "Balance", new Guid("0c17a6f1-905a-4c92-b1cd-4f1dc5da4d41"), "number", "money", "Money", null, "Visible" },
                    { 265, true, true, (short)7, "System.String", "", "", false, false, false, 128, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(9343), "BranchName", new Guid("13ff6606-5c95-435d-8d84-662ded7c75de"), "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 266, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(9359), "Id", new Guid("a3e20fe9-c00c-450e-b385-3126f266698c"), "number", "int", "", null, "AlwaysHidden" },
                    { 267, true, true, (short)-1, "System.Int16", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(9375), "BranchScope", new Guid("a2196a7f-b77c-4dce-bf19-de1f836533f8"), "number", "smallint", "", null, "AlwaysHidden" },
                    { 268, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(9390), "BranchId", new Guid("94763c58-45dc-44a8-aeae-254b8b1a2976"), "number", "int", "", null, "AlwaysHidden" },
                    { 269, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(9408), "TaxCode", new Guid("55d4b3c8-8d2b-4491-b147-5730030e3e26"), "number", "int", "", null, "AlwaysHidden" },
                    { 270, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(9423), "RowNo", new Guid("89779028-518b-42ff-9d1a-1364c266e218"), "number", "int", "", null, "AlwaysVisible" },
                    { 259, true, true, (short)1, "System.Date", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(9238), "VoucherDate", new Guid("4cffb4c3-243f-4b29-87d6-1a308a198dbc"), "Date", "datetime", "Default", null, "Visible" },
                    { 271, true, true, (short)1, "System.String", "", "", false, false, false, 64, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(9440), "Name", new Guid("4d21cfaf-d2c8-4a05-8d07-a02850fdbe1e"), "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 274, true, true, (short)2, "System.String", "", "", false, false, false, 16, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(9477), "MinorUnit", new Guid("476eee74-bee9-40cd-83f8-e281c008122b"), "string", "nvarchar", "", null, "Visible" },
                    { 275, true, true, (short)3, "System.String", "", "", false, false, true, 512, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(9494), "Description", new Guid("a20a262b-cd6d-44c7-8a8a-0864b6a5d0c2"), "string", "nvarchar", "", null, "Visible" },
                    { 276, true, true, (short)4, "System.String", "", "", false, false, false, 128, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(9511), "BranchName", new Guid("cb417f60-7ec3-4cff-9f2b-9b79657a7f51"), "string", "nvarchar", "", null, "Visible" },
                    { 277, true, true, (short)5, "System.Int16", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(9526), "DecimalCount", new Guid("a23469ea-7bda-4471-8b3f-8337ccc7e40a"), "number", "smallint", "", null, "Hidden" },
                    { 278, true, true, (short)7, "System.String", "", "", false, false, true, 32, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(9542), "State", new Guid("f112fa59-2ab3-4c46-b088-82a983626031"), "string", "nvarchar", "", null, "Visible" },
                    { 279, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(9558), "Id", new Guid("5a6f84df-86e8-48d6-b5eb-73505965b5ab"), "number", "int", "", null, "AlwaysHidden" },
                    { 280, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(9573), "CurrencyId", new Guid("2cb2b842-341f-430e-87e7-27a4132d731c"), "number", "int", "", null, "AlwaysHidden" },
                    { 281, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(9589), "BranchId", new Guid("667f335f-7d6f-4aff-bd14-69f02abf7856"), "number", "int", "", null, "AlwaysHidden" },
                    { 282, true, true, (short)-1, "System.Int16", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(9611), "BranchScope", new Guid("c5509367-ddb9-431a-a6e8-0366d8507f34"), "number", "smallint", "", null, "AlwaysHidden" },
                    { 343, true, true, (short)-1, "System.String", "", "", false, false, true, 64, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(1136), "VoucherReference", new Guid("060a79bb-fbd0-4f2b-aaae-22057aacc661"), "string", "nvarchar", "", null, "AlwaysHidden" },
                    { 284, true, true, (short)1, "System.Date", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(9645), "Date", new Guid("4565d4bc-6ecc-427f-8c8f-a4ecdac9389d"), "Date", "datetime", "Default", null, "Visible" },
                    { 272, true, true, (short)2, "System.String", "", "", false, false, false, 8, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(9456), "Code", new Guid("0c919c34-9007-4149-8552-24411a9404a7"), "string", "nvarchar", "", null, "Visible" },
                    { 258, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(9112), "RowNo", new Guid("deefa868-6f56-47f7-a3ff-bc4f7b3a7a5c"), "number", "int", "", null, "AlwaysVisible" },
                    { 257, true, true, (short)7, "System.String", "", "", false, false, false, 128, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(9096), "BranchName", new Guid("90cac42c-4c22-429d-8293-024cd8d1d430"), "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 256, false, false, (short)6, "System.Decimal", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(9075), "Balance", new Guid("608a2334-13f3-4fd4-a76d-1153e1cda69a"), "number", "money", "Money", null, "Visible" },
                    { 231, true, true, (short)6, "System.Decimal", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(8553), "Debit", new Guid("4c4e5bb5-b62e-4de2-abec-47a1ad80ac69"), "number", "money", "Money", null, "Visible" },
                    { 232, true, true, (short)7, "System.Decimal", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(8570), "Credit", new Guid("ed7fe0e3-31bd-49ef-9074-0536d68e45ec"), "number", "money", "Money", null, "Visible" },
                    { 233, true, true, (short)8, "System.String", "", "", false, false, false, 128, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(8594), "BranchName", new Guid("667d50dc-cde7-4fa3-89be-75697c35ff31"), "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 234, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(8612), "RowNo", new Guid("59aa8afa-8cd3-4579-a9a4-dd71fa971475"), "number", "int", "", null, "AlwaysVisible" },
                    { 235, true, true, (short)1, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(8629), "AccountFullCode", new Guid("6f618f29-962b-428e-a6b4-bacde5371b49"), "string", "nvarchar", "", null, "Visible" },
                    { 236, true, true, (short)2, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(8645), "AccountName", new Guid("6a457fbc-eb18-468e-b270-8e3bd1416966"), "string", "nvarchar", "", null, "Visible" },
                    { 237, true, true, (short)3, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(8660), "Description", new Guid("5b662dbb-f1bc-430e-bc4a-97a25786f81d"), "string", "nvarchar", "", null, "Visible" },
                    { 238, true, true, (short)4, "System.Decimal", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(8676), "Debit", new Guid("32df236c-cee4-484d-b5ad-f7e39996c9ab"), "number", "money", "Money", null, "Visible" },
                    { 239, true, true, (short)5, "System.Decimal", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(8692), "Credit", new Guid("38722360-c7b5-4c51-8553-541077ffb06e"), "number", "money", "Money", null, "Visible" },
                    { 240, true, true, (short)6, "System.String", "", "", false, false, false, 128, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(8707), "BranchName", new Guid("6cc8b278-a51a-4221-a816-438d9bad8159"), "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 241, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(8729), "RowNo", new Guid("4fc935c8-55e1-4ba5-af0e-f78e8ff8220a"), "number", "int", "", null, "AlwaysVisible" },
                    { 242, true, true, (short)1, "System.Date", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(8843), "VoucherDate", new Guid("e34a8c09-83d7-4b85-acbc-1a93b87e4471"), "Date", "datetime", "Default", null, "Visible" }
                });

            migrationBuilder.InsertData(
                schema: "Metadata",
                table: "Column",
                columns: new[] { "ColumnID", "AllowFiltering", "AllowSorting", "DisplayIndex", "DotNetType", "Expression", "GroupName", "IsDynamic", "IsFixedLength", "IsNullable", "Length", "MinLength", "ModifiedDate", "Name", "rowguid", "ScriptType", "StorageType", "Type", "ViewID", "Visibility" },
                values: new object[,]
                {
                    { 243, true, true, (short)2, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(8862), "VoucherNo", new Guid("b98a5d65-1008-4290-bf3d-63d9a2dca9b9"), "number", "int", "", null, "Visible" },
                    { 244, true, true, (short)3, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(8877), "Description", new Guid("5dcae14d-5c41-46c4-b4c1-d101a02c3e8b"), "string", "nvarchar", "", null, "Visible" },
                    { 245, true, true, (short)4, "System.Decimal", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(8893), "Debit", new Guid("36ae2cef-de52-43aa-9e95-b1cb4610e765"), "number", "money", "Money", null, "Visible" },
                    { 246, true, true, (short)5, "System.Decimal", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(8909), "Credit", new Guid("3a78875b-ea35-44d2-a293-31a52be482aa"), "number", "money", "Money", null, "Visible" },
                    { 247, false, false, (short)6, "System.Decimal", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(8924), "Balance", new Guid("544b11ab-5f56-4db1-b86f-31204dab4946"), "number", "money", "Money", null, "Visible" },
                    { 248, true, true, (short)7, "System.String", "", "", false, false, false, 128, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(8942), "Mark", new Guid("76a57e7b-253b-4f28-b9bb-53f45bad6bce"), "string", "nvarchar", "", null, "Visible" },
                    { 249, true, true, (short)8, "System.String", "", "", false, false, false, 128, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(8964), "BranchName", new Guid("9afcf996-ee16-473d-aedd-d9bf1c3e5684"), "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 250, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(8980), "RowNo", new Guid("b52bf864-ca21-4a92-8c0a-22e372ea77da"), "number", "int", "", null, "AlwaysVisible" },
                    { 251, true, true, (short)1, "System.Date", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(8995), "VoucherDate", new Guid("0105ca9c-f105-4238-9d61-d0cfe4d223e2"), "Date", "datetime", "Default", null, "Visible" },
                    { 252, true, true, (short)2, "System.Int32", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(9011), "VoucherNo", new Guid("f5d0a03f-93a7-4e39-a705-5826b83e6120"), "number", "int", "", null, "Visible" },
                    { 253, true, true, (short)3, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(9027), "Description", new Guid("8114a3c1-3606-4c93-8135-1ed71cc3e75c"), "string", "nvarchar", "", null, "Visible" },
                    { 254, true, true, (short)4, "System.Decimal", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(9042), "Debit", new Guid("55844db0-36ee-400b-b2a3-96888cb08514"), "number", "money", "Money", null, "Visible" },
                    { 255, true, true, (short)5, "System.Decimal", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(9060), "Credit", new Guid("da3f0d1e-56da-4d56-b2c1-bdb9b280d763"), "number", "money", "Money", null, "Visible" },
                    { 285, true, true, (short)2, "System.TimeSpan", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(9861), "Time", new Guid("4cd866ca-c313-4eab-bd36-0988cba14a9e"), "number", "time", "", null, "Visible" },
                    { 286, true, true, (short)3, "System.decimal", "", "", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(9877), "Multiplier", new Guid("1c4d004a-cd29-4406-a409-5e611bd6b20b"), "number", "money", "Money", null, "Visible" },
                    { 283, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(9628), "RowNo", new Guid("e1d13ab0-f38d-4ff5-8bc6-1b3a2ca429ad"), "number", "int", "", null, "AlwaysVisible" },
                    { 288, true, true, (short)5, "System.String", "", "", false, false, true, 512, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(9909), "Description", new Guid("1a019649-4e70-4533-9a3e-5c99b2f6c491"), "string", "nvarchar", "", null, "Visible" },
                    { 317, true, true, (short)1, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(584), "AccountFullCode", new Guid("cfcf9245-47ba-4046-8c74-a76935f195bb"), "string", "nvarchar", "", null, "Visible" },
                    { 287, true, true, (short)4, "System.String", "", "", false, false, false, 128, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(9893), "BranchName", new Guid("51790535-5127-4ca1-9b29-8a3948b50426"), "string", "nvarchar", "", null, "Visible" },
                    { 319, true, true, (short)3, "System.Decimal", "", "StartBalance", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(616), "StartBalanceDebit", new Guid("6cdf2890-e941-4823-82ac-77da020c772b"), "number", "money", "Money", null, "Visible" },
                    { 320, true, true, (short)4, "System.Decimal", "", "StartBalance", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(631), "StartBalanceCredit", new Guid("e884fab7-58a2-48ad-b32d-e928282bbdb8"), "number", "money", "Money", null, "Visible" },
                    { 321, true, true, (short)5, "System.Decimal", "", "Turnover", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(648), "TurnoverDebit", new Guid("acf33db8-b63f-4cce-8391-75377a63e806"), "number", "money", "Money", null, "Visible" },
                    { 322, true, true, (short)6, "System.Decimal", "", "Turnover", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(671), "TurnoverCredit", new Guid("91416ad8-8d55-47d2-ba4d-ee15a0e5ba4d"), "number", "money", "Money", null, "Visible" },
                    { 323, true, true, (short)7, "System.Decimal", "", "OperationSum", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(687), "OperationSumDebit", new Guid("42f074cf-570b-40cb-975e-6669962e2f1e"), "number", "money", "Money", null, "Visible" },
                    { 324, true, true, (short)8, "System.Decimal", "", "OperationSum", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(707), "OperationSumCredit", new Guid("8f5581cc-6fef-4ea5-8ea4-5765a138a04f"), "number", "money", "Money", null, "Visible" },
                    { 325, true, true, (short)9, "System.Decimal", "", "EndBalance", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(828), "EndBalanceDebit", new Guid("8ebc8ce9-ae50-490f-baf6-c5a6a6343c87"), "number", "money", "Money", null, "Visible" },
                    { 326, true, true, (short)10, "System.Decimal", "", "EndBalance", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(847), "EndBalanceCredit", new Guid("f658e637-2b13-4b3f-a604-adbe34660f07"), "number", "money", "Money", null, "Visible" },
                    { 327, true, true, (short)11, "System.String", "", "", false, false, false, 128, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(863), "BranchName", new Guid("ce3a2c72-0ae1-4ad4-8f1d-76ff846fdeaa"), "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 328, true, true, (short)-1, "System.String", "", "", false, false, true, 64, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(879), "VoucherReference", new Guid("f09cec14-dc8c-4127-95e2-04983beafd81"), "string", "nvarchar", "", null, "AlwaysHidden" },
                    { 329, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(896), "RowNo", new Guid("613be496-ae17-490e-8e61-cf69f0ad6af6"), "number", "int", "", null, "AlwaysVisible" },
                    { 330, true, true, (short)1, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(919), "AccountFullCode", new Guid("6c98bcc3-efb3-411b-b8d1-d25d68f9e90e"), "string", "nvarchar", "", null, "Visible" },
                    { 331, true, true, (short)2, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(937), "AccountName", new Guid("6982d943-707e-4e82-91b4-b9076f760b5b"), "string", "nvarchar", "", null, "Visible" },
                    { 332, true, true, (short)3, "System.Decimal", "", "StartBalance", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(954), "StartBalanceDebit", new Guid("e1049b57-45a5-4037-aedb-182ebdaeb4f1"), "number", "money", "Money", null, "Visible" },
                    { 333, true, true, (short)4, "System.Decimal", "", "StartBalance", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(969), "StartBalanceCredit", new Guid("8af09e19-99a4-43f7-8932-13d89d977c37"), "number", "money", "Money", null, "Visible" },
                    { 334, true, true, (short)5, "System.Decimal", "", "Turnover", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(986), "TurnoverDebit", new Guid("96db58d4-1ab2-4af6-8fb5-c27fe6b0428f"), "number", "money", "Money", null, "Visible" },
                    { 335, true, true, (short)6, "System.Decimal", "", "Turnover", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(1001), "TurnoverCredit", new Guid("0bc227d8-f65b-496f-ae40-937c519ce32f"), "number", "money", "Money", null, "Visible" },
                    { 336, true, true, (short)7, "System.Decimal", "", "OperationSum", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(1018), "OperationSumDebit", new Guid("45677108-6e35-4c28-b7b1-b07ffa5e4ca2"), "number", "money", "Money", null, "Visible" },
                    { 337, true, true, (short)8, "System.Decimal", "", "OperationSum", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(1036), "OperationSumCredit", new Guid("be3cf0c2-85d1-4398-a545-1416b7b4510c"), "number", "money", "Money", null, "Visible" },
                    { 338, true, true, (short)9, "System.Decimal", "", "Corrections", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(1057), "CorrectionsDebit", new Guid("a0bb206f-d69e-4a8a-a1ec-6ebdf4bc2aa1"), "number", "money", "Money", null, "Visible" },
                    { 339, true, true, (short)10, "System.Decimal", "", "Corrections", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(1073), "CorrectionsCredit", new Guid("bb9e71b8-4440-4c58-96fa-ba11d1b7f4be"), "number", "money", "Money", null, "Visible" },
                    { 340, true, true, (short)11, "System.Decimal", "", "EndBalance", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(1088), "EndBalanceDebit", new Guid("11b86342-f460-4f22-a937-f28f8a70625a"), "number", "money", "Money", null, "Visible" },
                    { 341, true, true, (short)12, "System.Decimal", "", "EndBalance", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(1105), "EndBalanceCredit", new Guid("29056e77-a415-4ccd-b3bb-23856245f166"), "number", "money", "Money", null, "Visible" }
                });

            migrationBuilder.InsertData(
                schema: "Metadata",
                table: "Column",
                columns: new[] { "ColumnID", "AllowFiltering", "AllowSorting", "DisplayIndex", "DotNetType", "Expression", "GroupName", "IsDynamic", "IsFixedLength", "IsNullable", "Length", "MinLength", "ModifiedDate", "Name", "rowguid", "ScriptType", "StorageType", "Type", "ViewID", "Visibility" },
                values: new object[,]
                {
                    { 316, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(567), "RowNo", new Guid("7f900242-6fad-416e-b642-8cd63080eb52"), "number", "int", "", null, "AlwaysVisible" },
                    { 315, true, true, (short)-1, "System.String", "", "", false, false, true, 64, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(551), "VoucherReference", new Guid("1821e950-9560-4e59-b0a3-2013565d5dd1"), "string", "nvarchar", "", null, "AlwaysHidden" },
                    { 318, true, true, (short)2, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(600), "AccountName", new Guid("b9f3b302-f6ae-4511-b988-b7b6587508f0"), "string", "nvarchar", "", null, "Visible" },
                    { 313, true, true, (short)8, "System.Decimal", "", "EndBalance", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(514), "EndBalanceCredit", new Guid("8ca7255a-8cd7-4bc7-ac7e-8b770778c328"), "number", "money", "Money", null, "Visible" },
                    { 289, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(9924), "RowNo", new Guid("b403cf9a-652f-466a-a9cc-c617da67ea34"), "number", "int", "", null, "AlwaysVisible" },
                    { 290, true, true, (short)1, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(9951), "AccountFullCode", new Guid("9c765c1e-2931-43e1-bd87-5e23837a725f"), "string", "nvarchar", "", null, "Visible" },
                    { 291, true, true, (short)2, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 18, 17, 16, 5, 827, DateTimeKind.Local).AddTicks(9967), "AccountName", new Guid("bcd51103-d52a-4c62-9774-ea8844c4b1d1"), "string", "nvarchar", "", null, "Visible" },
                    { 292, true, true, (short)3, "System.Decimal", "", "EndBalance", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(140), "EndBalanceDebit", new Guid("3d681efb-33cd-4bf2-ae06-6ad5c7c7d6a5"), "number", "money", "Money", null, "Visible" },
                    { 293, true, true, (short)4, "System.Decimal", "", "EndBalance", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(160), "EndBalanceCredit", new Guid("626b6a3d-ad82-45d5-aee9-b717f068ef1c"), "number", "money", "Money", null, "Visible" },
                    { 294, true, true, (short)5, "System.String", "", "", false, false, false, 128, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(177), "BranchName", new Guid("c7b9d1a9-823f-4fa8-9b25-a67271c33a9f"), "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 295, true, true, (short)-1, "System.String", "", "", false, false, true, 64, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(192), "VoucherReference", new Guid("8074ce66-9abc-45db-b52a-eaf296b4f2ad"), "string", "nvarchar", "", null, "AlwaysHidden" },
                    { 314, true, true, (short)9, "System.String", "", "", false, false, false, 128, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(535), "BranchName", new Guid("2d7b2371-c3fa-4aa8-b670-65c1f342172f"), "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 297, true, true, (short)1, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(227), "AccountFullCode", new Guid("3c2a3e53-c2b5-405f-8b09-e84dbb9c4ef6"), "string", "nvarchar", "", null, "Visible" },
                    { 298, true, true, (short)2, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(251), "AccountName", new Guid("541242e5-6155-42f4-88dd-573cce553d29"), "string", "nvarchar", "", null, "Visible" },
                    { 299, true, true, (short)3, "System.Decimal", "", "Turnover", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(269), "TurnoverDebit", new Guid("770547e5-c0c9-4958-87cf-6cb51dbe57bb"), "number", "money", "Money", null, "Visible" },
                    { 300, true, true, (short)4, "System.Decimal", "", "Turnover", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(284), "TurnoverCredit", new Guid("765122e1-6bbf-47d3-bf07-4f5db38372c1"), "number", "money", "Money", null, "Visible" },
                    { 296, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(209), "RowNo", new Guid("9b834ed5-3132-4a40-b8ca-5052ce2910c9"), "number", "int", "", null, "AlwaysVisible" },
                    { 302, true, true, (short)6, "System.Decimal", "", "EndBalance", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(321), "EndBalanceCredit", new Guid("4c3a155e-b4e2-4f72-9245-4e93645b8f00"), "number", "money", "Money", null, "Visible" },
                    { 312, true, true, (short)7, "System.Decimal", "", "EndBalance", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(498), "EndBalanceDebit", new Guid("cde0beeb-87a1-4059-bfdf-1cdac20d33dd"), "number", "money", "Money", null, "Visible" },
                    { 301, true, true, (short)5, "System.Decimal", "", "EndBalance", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(304), "EndBalanceDebit", new Guid("8d6211bf-67ab-479e-a839-7bfba9f1d287"), "number", "money", "Money", null, "Visible" },
                    { 310, true, true, (short)5, "System.Decimal", "", "Turnover", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(465), "TurnoverDebit", new Guid("d1d63f9c-b422-4360-bbc4-cd72ffb1beda"), "number", "money", "Money", null, "Visible" },
                    { 309, true, true, (short)4, "System.Decimal", "", "StartBalance", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(448), "StartBalanceCredit", new Guid("5d914bf5-0501-490f-bcec-da0c8cfc4f69"), "number", "money", "Money", null, "Visible" },
                    { 308, true, true, (short)3, "System.Decimal", "", "StartBalance", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(430), "StartBalanceDebit", new Guid("da48802f-b03a-4291-aa2c-49f2c2b5f503"), "number", "money", "Money", null, "Visible" },
                    { 311, true, true, (short)6, "System.Decimal", "", "Turnover", false, false, false, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(482), "TurnoverCredit", new Guid("018177e0-6c4b-4a7f-8e09-23469b577995"), "number", "money", "Money", null, "Visible" },
                    { 306, true, true, (short)1, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(395), "AccountFullCode", new Guid("9aa26f2c-191a-47f3-9cff-bf2c81a04c3d"), "string", "nvarchar", "", null, "Visible" },
                    { 305, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(372), "RowNo", new Guid("d2409e87-fb8a-44a5-af48-6fb880e73f4a"), "number", "int", "", null, "AlwaysVisible" },
                    { 304, true, true, (short)-1, "System.String", "", "", false, false, true, 64, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(354), "VoucherReference", new Guid("405eb7f5-724b-4b21-9ef9-f7238ec1cd5c"), "string", "nvarchar", "", null, "AlwaysHidden" },
                    { 303, true, true, (short)7, "System.String", "", "", false, false, false, 128, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(338), "BranchName", new Guid("80dc4d07-2595-45b9-bf08-9dea3198df1f"), "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 307, true, true, (short)2, "System.String", "", "", false, false, false, 512, 0, new DateTime(2023, 10, 18, 17, 16, 5, 828, DateTimeKind.Local).AddTicks(413), "AccountName", new Guid("69879c10-35cd-4bbc-b87b-238720832c73"), "string", "nvarchar", "", null, "Visible" }
                });

            migrationBuilder.InsertData(
                schema: "Metadata",
                table: "Command",
                columns: new[] { "CommandID", "HotKey", "IconName", "Index", "ModifiedDate", "ParentId", "PermissionId", "RouteUrl", "rowguid", "TitleKey" },
                values: new object[,]
                {
                    { 1, "", "folder-close", null, new DateTime(2023, 10, 18, 17, 16, 5, 855, DateTimeKind.Local).AddTicks(1409), null, null, "", new Guid("85970da0-b5b4-4b2d-95b2-e7ebc51330e0"), "Accounting" },
                    { 23, "", "folder-close", null, new DateTime(2023, 10, 18, 17, 16, 5, 855, DateTimeKind.Local).AddTicks(6851), null, null, "", new Guid("e4a6ad20-1fd0-48b3-a5d9-d50c6787d8b3"), "Organization" },
                    { 27, "", "folder-close", null, new DateTime(2023, 10, 18, 17, 16, 5, 855, DateTimeKind.Local).AddTicks(6909), null, null, "", new Guid("938652d1-ebbc-43b5-8c24-570de94ab320"), "Administration" },
                    { 37, "", "folder-close", null, new DateTime(2023, 10, 18, 17, 16, 5, 855, DateTimeKind.Local).AddTicks(6986), null, null, "", new Guid("d95626ff-5166-4cd3-8fb0-74a5342e3154"), "Tools" },
                    { 52, "", "folder-close", null, new DateTime(2023, 10, 18, 17, 16, 5, 855, DateTimeKind.Local).AddTicks(7157), null, null, "", new Guid("9cb85296-e9fd-4d50-9887-d7e1354d4ca1"), "Treasury" },
                    { 100000, "", "folder-close", null, new DateTime(2023, 10, 18, 17, 16, 5, 855, DateTimeKind.Local).AddTicks(7464), null, null, "", new Guid("e3be1829-25f0-4f1b-8736-b35d62a5a272"), "ProductScope" }
                });

            migrationBuilder.InsertData(
                schema: "Metadata",
                table: "EntityType",
                columns: new[] { "EntityTypeID", "Description", "ModifiedDate", "Name" },
                values: new object[,]
                {
                    { 9, "", new DateTime(2023, 10, 18, 17, 16, 5, 382, DateTimeKind.Local).AddTicks(2751), "UserReport" },
                    { 8, "", new DateTime(2023, 10, 18, 17, 16, 5, 382, DateTimeKind.Local).AddTicks(2743), "ViewRowPermission" },
                    { 5, "", new DateTime(2023, 10, 18, 17, 16, 5, 382, DateTimeKind.Local).AddTicks(2723), "SysOperationLog" },
                    { 6, "", new DateTime(2023, 10, 18, 17, 16, 5, 382, DateTimeKind.Local).AddTicks(2730), "User" },
                    { 2, "", new DateTime(2023, 10, 18, 17, 16, 5, 382, DateTimeKind.Local).AddTicks(2698), "Role" },
                    { 1, "", new DateTime(2023, 10, 18, 17, 16, 5, 382, DateTimeKind.Local).AddTicks(2436), "CompanyDb" },
                    { 4, "", new DateTime(2023, 10, 18, 17, 16, 5, 382, DateTimeKind.Local).AddTicks(2714), "Setting" }
                });

            migrationBuilder.InsertData(
                schema: "Metadata",
                table: "Locale",
                columns: new[] { "LocaleID", "Code", "LocalName", "ModifiedDate", "Name" },
                values: new object[,]
                {
                    { 1, "en", "English", new DateTime(2023, 10, 18, 17, 16, 5, 401, DateTimeKind.Local).AddTicks(9880), "English" },
                    { 2, "fa", "فارسی", new DateTime(2023, 10, 18, 17, 16, 5, 402, DateTimeKind.Local).AddTicks(110), "Persian" },
                    { 3, "ar", "العربیه", new DateTime(2023, 10, 18, 17, 16, 5, 402, DateTimeKind.Local).AddTicks(121), "Arabic" },
                    { 4, "fr", "Français", new DateTime(2023, 10, 18, 17, 16, 5, 402, DateTimeKind.Local).AddTicks(128), "French" }
                });

            migrationBuilder.InsertData(
                schema: "Metadata",
                table: "Operation",
                columns: new[] { "OperationID", "Description", "ModifiedDate", "Name" },
                values: new object[,]
                {
                    { 26, "", new DateTime(2023, 10, 18, 17, 16, 5, 385, DateTimeKind.Local).AddTicks(1973), "AssignRole" },
                    { 27, "", new DateTime(2023, 10, 18, 17, 16, 5, 385, DateTimeKind.Local).AddTicks(1978), "AssignUser" },
                    { 28, "", new DateTime(2023, 10, 18, 17, 16, 5, 385, DateTimeKind.Local).AddTicks(1983), "BranchAccess" },
                    { 29, "", new DateTime(2023, 10, 18, 17, 16, 5, 385, DateTimeKind.Local).AddTicks(1989), "FiscalPeriodAccess" },
                    { 57, "", new DateTime(2023, 10, 18, 17, 16, 5, 385, DateTimeKind.Local).AddTicks(2007), "CompanyAccess" },
                    { 35, "", new DateTime(2023, 10, 18, 17, 16, 5, 385, DateTimeKind.Local).AddTicks(1998), "RoleAccess" },
                    { 54, "", new DateTime(2023, 10, 18, 17, 16, 5, 385, DateTimeKind.Local).AddTicks(2003), "Export" },
                    { 25, "", new DateTime(2023, 10, 18, 17, 16, 5, 385, DateTimeKind.Local).AddTicks(1895), "SwitchBranch" },
                    { 58, "", new DateTime(2023, 10, 18, 17, 16, 5, 385, DateTimeKind.Local).AddTicks(2012), "PrintPreview" },
                    { 30, "", new DateTime(2023, 10, 18, 17, 16, 5, 385, DateTimeKind.Local).AddTicks(1994), "ViewArchive" },
                    { 24, "", new DateTime(2023, 10, 18, 17, 16, 5, 385, DateTimeKind.Local).AddTicks(1890), "SwitchFiscalPeriod" },
                    { 23, "", new DateTime(2023, 10, 18, 17, 16, 5, 385, DateTimeKind.Local).AddTicks(1886), "CompanyLogin" },
                    { 22, "", new DateTime(2023, 10, 18, 17, 16, 5, 385, DateTimeKind.Local).AddTicks(1881), "FailedLogin" },
                    { 1, "", new DateTime(2023, 10, 18, 17, 16, 5, 385, DateTimeKind.Local).AddTicks(1622), "View" },
                    { 2, "", new DateTime(2023, 10, 18, 17, 16, 5, 385, DateTimeKind.Local).AddTicks(1826), "Create" },
                    { 4, "", new DateTime(2023, 10, 18, 17, 16, 5, 385, DateTimeKind.Local).AddTicks(1842), "Delete" },
                    { 5, "", new DateTime(2023, 10, 18, 17, 16, 5, 385, DateTimeKind.Local).AddTicks(1848), "Filter" },
                    { 3, "", new DateTime(2023, 10, 18, 17, 16, 5, 385, DateTimeKind.Local).AddTicks(1837), "Edit" },
                    { 7, "", new DateTime(2023, 10, 18, 17, 16, 5, 385, DateTimeKind.Local).AddTicks(1861), "Save" },
                    { 8, "", new DateTime(2023, 10, 18, 17, 16, 5, 385, DateTimeKind.Local).AddTicks(1866), "Archive" },
                    { 10, "", new DateTime(2023, 10, 18, 17, 16, 5, 385, DateTimeKind.Local).AddTicks(1870), "Design" },
                    { 21, "", new DateTime(2023, 10, 18, 17, 16, 5, 385, DateTimeKind.Local).AddTicks(1877), "GroupDelete" },
                    { 6, "", new DateTime(2023, 10, 18, 17, 16, 5, 385, DateTimeKind.Local).AddTicks(1856), "Print" }
                });

            migrationBuilder.InsertData(
                schema: "Metadata",
                table: "OperationSource",
                columns: new[] { "OperationSourceID", "Description", "ModifiedDate", "Name" },
                values: new object[,]
                {
                    { 7, "", new DateTime(2023, 10, 18, 17, 16, 5, 380, DateTimeKind.Local).AddTicks(4003), "AppLogin" },
                    { 8, "", new DateTime(2023, 10, 18, 17, 16, 5, 380, DateTimeKind.Local).AddTicks(4213), "AppEnvironment" },
                    { 14, "", new DateTime(2023, 10, 18, 17, 16, 5, 380, DateTimeKind.Local).AddTicks(4223), "SystemSettings" }
                });

            migrationBuilder.InsertData(
                schema: "Metadata",
                table: "OperationSourceType",
                columns: new[] { "OperationSourceTypeID", "ModifiedDate", "Name" },
                values: new object[,]
                {
                    { 3, new DateTime(2023, 10, 18, 17, 16, 5, 43, DateTimeKind.Local).AddTicks(7780), "Reports" },
                    { 1, new DateTime(2023, 10, 18, 17, 16, 5, 43, DateTimeKind.Local).AddTicks(7516), "BaseData" },
                    { 2, new DateTime(2023, 10, 18, 17, 16, 5, 43, DateTimeKind.Local).AddTicks(7766), "OperationalForms" }
                });

            migrationBuilder.InsertData(
                schema: "Metadata",
                table: "Subsystem",
                columns: new[] { "SubsystemID", "ModifiedDate", "Name" },
                values: new object[,]
                {
                    { 3, new DateTime(2023, 10, 18, 17, 16, 5, 39, DateTimeKind.Local).AddTicks(3941), "Treasury" },
                    { 100000, new DateTime(2023, 10, 18, 17, 16, 5, 39, DateTimeKind.Local).AddTicks(3952), "ProductScope" },
                    { 1, new DateTime(2023, 10, 18, 17, 16, 5, 34, DateTimeKind.Local).AddTicks(2175), "Administration" },
                    { 2, new DateTime(2023, 10, 18, 17, 16, 5, 39, DateTimeKind.Local).AddTicks(3738), "Accounting" }
                });

            migrationBuilder.InsertData(
                schema: "Metadata",
                table: "View",
                columns: new[] { "ViewID", "EntityName", "EntityType", "FetchUrl", "IsCartableIntegrated", "IsHierarchy", "ModifiedDate", "Name", "rowguid", "SearchUrl" },
                values: new object[,]
                {
                    { 45, "ItemBalance", "", "", false, false, new DateTime(2023, 10, 18, 17, 16, 5, 444, DateTimeKind.Local).AddTicks(8433), "DetailAccountBalance6Column", new Guid("af96623f-2027-4ad0-80e0-c895592e2c81"), "" },
                    { 60, "SysOperationLogArchive", "", "", false, false, new DateTime(2023, 10, 18, 17, 16, 5, 444, DateTimeKind.Local).AddTicks(8673), "SysOperationLogArchive", new Guid("0ea75f28-da42-4bf1-b5c8-27de18bfe787"), "" },
                    { 100003, "Property", "Base", "", false, false, new DateTime(2023, 10, 18, 17, 16, 5, 444, DateTimeKind.Local).AddTicks(8914), "Property", new Guid("38e626b0-3e79-43c0-845f-2d2252adfa9b"), "" },
                    { 59, "SysOperationLog", "", "", false, false, new DateTime(2023, 10, 18, 17, 16, 5, 444, DateTimeKind.Local).AddTicks(8663), "SysOperationLog", new Guid("bb3a867e-32d9-424b-89d7-37635037c97f"), "" },
                    { 58, "BalanceByAccount", "", "", false, false, new DateTime(2023, 10, 18, 17, 16, 5, 444, DateTimeKind.Local).AddTicks(8651), "BalanceByAccount", new Guid("163830c6-61de-46d0-a405-46a9cf6cd5ba"), "" }
                });

            migrationBuilder.InsertData(
                schema: "Metadata",
                table: "View",
                columns: new[] { "ViewID", "EntityName", "EntityType", "FetchUrl", "IsCartableIntegrated", "IsHierarchy", "ModifiedDate", "Name", "rowguid", "SearchUrl" },
                values: new object[,]
                {
                    { 57, "ItemBalance", "", "", false, false, new DateTime(2023, 10, 18, 17, 16, 5, 444, DateTimeKind.Local).AddTicks(8640), "ProjectBalance10Column", new Guid("36d35d0c-3936-4bf4-9042-d27c3cbc1bcb"), "" },
                    { 56, "ItemBalance", "", "", false, false, new DateTime(2023, 10, 18, 17, 16, 5, 444, DateTimeKind.Local).AddTicks(8629), "ProjectBalance8Column", new Guid("d76947ef-e7d3-4578-b9cb-c0d055000bb5"), "" },
                    { 55, "ItemBalance", "", "", false, false, new DateTime(2023, 10, 18, 17, 16, 5, 444, DateTimeKind.Local).AddTicks(8618), "ProjectBalance6Column", new Guid("28ceab7b-08b9-4ca4-9538-95412fbc0647"), "" },
                    { 44, "ItemBalance", "", "", false, false, new DateTime(2023, 10, 18, 17, 16, 5, 444, DateTimeKind.Local).AddTicks(8422), "DetailAccountBalance4Column", new Guid("0e26ffda-f6d3-4612-9afd-fd41aa34d2dd"), "" },
                    { 54, "ItemBalance", "", "", false, false, new DateTime(2023, 10, 18, 17, 16, 5, 444, DateTimeKind.Local).AddTicks(8603), "ProjectBalance4Column", new Guid("4223941c-1d8b-43e4-b636-6f2fe2b79159"), "" },
                    { 52, "ItemBalance", "", "", false, false, new DateTime(2023, 10, 18, 17, 16, 5, 444, DateTimeKind.Local).AddTicks(8582), "CostCenterBalance10Column", new Guid("794e1d9a-6743-48e4-bfe4-94840e1973ad"), "" },
                    { 51, "ItemBalance", "", "", false, false, new DateTime(2023, 10, 18, 17, 16, 5, 444, DateTimeKind.Local).AddTicks(8571), "CostCenterBalance8Column", new Guid("4caa7649-8f32-4e1e-a042-041e883020bc"), "" },
                    { 50, "ItemBalance", "", "", false, false, new DateTime(2023, 10, 18, 17, 16, 5, 444, DateTimeKind.Local).AddTicks(8491), "CostCenterBalance6Column", new Guid("d396347b-9c74-47e6-9af0-48fffc40296e"), "" },
                    { 49, "ItemBalance", "", "", false, false, new DateTime(2023, 10, 18, 17, 16, 5, 444, DateTimeKind.Local).AddTicks(8480), "CostCenterBalance4Column", new Guid("15fb9d31-b7be-4251-9f4d-efd61d9cc43a"), "" },
                    { 48, "ItemBalance", "", "", false, false, new DateTime(2023, 10, 18, 17, 16, 5, 444, DateTimeKind.Local).AddTicks(8469), "CostCenterBalance2Column", new Guid("fcfa7bc0-d885-4d99-8866-e16bf36bf946"), "" },
                    { 47, "ItemBalance", "", "", false, false, new DateTime(2023, 10, 18, 17, 16, 5, 444, DateTimeKind.Local).AddTicks(8458), "DetailAccountBalance10Column", new Guid("1913e366-cacb-43a2-8d93-3d81583ea255"), "" },
                    { 46, "ItemBalance", "", "", false, false, new DateTime(2023, 10, 18, 17, 16, 5, 444, DateTimeKind.Local).AddTicks(8443), "DetailAccountBalance8Column", new Guid("076a1097-9d5d-45de-8d14-4e3074a1d9fc"), "" },
                    { 61, "OperationLogArchive", "", "", false, false, new DateTime(2023, 10, 18, 17, 16, 5, 444, DateTimeKind.Local).AddTicks(8684), "OperationLogArchive", new Guid("c83c255f-929e-4575-8c97-e048a0caa2cd"), "" },
                    { 53, "ItemBalance", "", "", false, false, new DateTime(2023, 10, 18, 17, 16, 5, 444, DateTimeKind.Local).AddTicks(8592), "ProjectBalance2Column", new Guid("53302876-420f-4078-b673-3d61b411686f"), "" },
                    { 62, "ProfitLoss", "", "", false, false, new DateTime(2023, 10, 18, 17, 16, 5, 444, DateTimeKind.Local).AddTicks(8695), "ProfitLoss", new Guid("25c2cbf8-2c73-4473-8067-a8efff4e78fb"), "" },
                    { 64, "ProfitLoss", "", "", false, false, new DateTime(2023, 10, 18, 17, 16, 5, 444, DateTimeKind.Local).AddTicks(8721), "ProfitLossSimple", new Guid("d8c1dc13-1879-48b4-9d17-b77a442cb482"), "" },
                    { 43, "ItemBalance", "", "", false, false, new DateTime(2023, 10, 18, 17, 16, 5, 444, DateTimeKind.Local).AddTicks(8411), "DetailAccountBalance2Column", new Guid("47b50bec-dc32-4b93-954c-17ab0341e339"), "" },
                    { 100002, "Unit", "Base", "", false, false, new DateTime(2023, 10, 18, 17, 16, 5, 444, DateTimeKind.Local).AddTicks(8903), "Unit", new Guid("3b331bd7-664e-402d-b140-e8323dc1af21"), "" },
                    { 100001, "Brand", "Base", "", false, false, new DateTime(2023, 10, 18, 17, 16, 5, 444, DateTimeKind.Local).AddTicks(8893), "Brand", new Guid("60de09e5-a140-4a85-b7ff-fa791d14b134"), "" },
                    { 78, "VouchersByDate", "Operational", "", false, false, new DateTime(2023, 10, 18, 17, 16, 5, 444, DateTimeKind.Local).AddTicks(8878), "VouchersByDate", new Guid("43812cd3-30bf-420d-9e52-fa9cd650b3f3"), "" },
                    { 77, "PayReceiveCashAccount", "Core", "", false, false, new DateTime(2023, 10, 18, 17, 16, 5, 444, DateTimeKind.Local).AddTicks(8867), "PayReceiveCashAccount", new Guid("db53c535-11b3-40e8-b545-378d5c552369"), "" },
                    { 76, "PayReceiveAccount", "Core", "", false, false, new DateTime(2023, 10, 18, 17, 16, 5, 444, DateTimeKind.Local).AddTicks(8856), "PayReceiveAccount", new Guid("f0bfa512-1dc3-4b22-b05e-7362972e15e7"), "" },
                    { 75, "Receipt", "Operational", "", false, false, new DateTime(2023, 10, 18, 17, 16, 5, 444, DateTimeKind.Local).AddTicks(8846), "Receipt", new Guid("b1b291dd-0273-4d45-a317-711fe5bfac7a"), "" },
                    { 74, "Payment", "Operational", "", false, false, new DateTime(2023, 10, 18, 17, 16, 5, 444, DateTimeKind.Local).AddTicks(8835), "Payment", new Guid("3983626c-6b71-4013-adaa-686208479bb0"), "" },
                    { 73, "SourceApp", "Base", "", false, false, new DateTime(2023, 10, 18, 17, 16, 5, 444, DateTimeKind.Local).AddTicks(8824), "SourceApp", new Guid("c567abcf-75e8-4a61-86c0-d7532c9888f6"), "" },
                    { 72, "CheckBookReport", "", "", false, false, new DateTime(2023, 10, 18, 17, 16, 5, 444, DateTimeKind.Local).AddTicks(8814), "CheckBookReport", new Guid("e8e5d5f3-1461-4461-8e52-fa243325972c"), "" },
                    { 71, "CheckBook", "Operational", "", false, false, new DateTime(2023, 10, 18, 17, 16, 5, 444, DateTimeKind.Local).AddTicks(8803), "CheckBook", new Guid("f63db63a-76e4-45ac-938b-50d3ef749698"), "" },
                    { 70, "CashRegister", "Base", "", false, false, new DateTime(2023, 10, 18, 17, 16, 5, 444, DateTimeKind.Local).AddTicks(8788), "CashRegister", new Guid("c8a55cd7-cfa0-4fd5-aba8-7539a4136f26"), "" },
                    { 69, "CheckBookPage", "Core", "", false, false, new DateTime(2023, 10, 18, 17, 16, 5, 444, DateTimeKind.Local).AddTicks(8777), "CheckBookPage", new Guid("a853e0fa-cb5e-4127-bd5f-b98c67ddccb5"), "" },
                    { 68, "Dashboard", "Core", "", false, false, new DateTime(2023, 10, 18, 17, 16, 5, 444, DateTimeKind.Local).AddTicks(8766), "Widget", new Guid("43ed482f-9435-4465-b437-c90fe0e680f4"), "" },
                    { 67, "BalanceSheet", "", "", false, false, new DateTime(2023, 10, 18, 17, 16, 5, 444, DateTimeKind.Local).AddTicks(8756), "BalanceSheet", new Guid("94aefdbc-4f12-4f27-a89f-65d42e025693"), "" },
                    { 66, "ProfitLoss", "", "", false, false, new DateTime(2023, 10, 18, 17, 16, 5, 444, DateTimeKind.Local).AddTicks(8745), "ComparativeProfitLossSimple", new Guid("26eebacf-44a0-4cdd-b6f6-69510f5fb451"), "" },
                    { 65, "ProfitLoss", "", "", false, false, new DateTime(2023, 10, 18, 17, 16, 5, 444, DateTimeKind.Local).AddTicks(8731), "ComparativeProfitLoss", new Guid("bc7fe19e-7acf-4406-aaba-2ee9cf7f8983"), "" },
                    { 63, "GroupActionResult", "", "", false, false, new DateTime(2023, 10, 18, 17, 16, 5, 444, DateTimeKind.Local).AddTicks(8710), "GroupActionResult", new Guid("d51f40e0-e021-442a-b483-a3fed1999a32"), "" },
                    { 42, "VoucherLineDetail", "", "", false, false, new DateTime(2023, 10, 18, 17, 16, 5, 444, DateTimeKind.Local).AddTicks(8400), "VoucherLineDetail", new Guid("a02321dc-6437-4495-932d-9cb31a757516"), "" },
                    { 22, "Journal", "", "", false, false, new DateTime(2023, 10, 18, 17, 16, 5, 444, DateTimeKind.Local).AddTicks(8165), "JournalByNoByRow", new Guid("5f9d0743-e581-4c93-b7aa-03aa4997df17"), "" },
                    { 40, "CurrencyBook", "", "", false, false, new DateTime(2023, 10, 18, 17, 16, 5, 444, DateTimeKind.Local).AddTicks(8379), "CurrencyBookSummary", new Guid("fa521d4b-bd3a-4188-8092-e1bd360365a2"), "" },
                    { 16, "Journal", "", "", false, false, new DateTime(2023, 10, 18, 17, 16, 5, 444, DateTimeKind.Local).AddTicks(7979), "JournalByDateByRowDetail", new Guid("f4f848b6-a368-403a-ba03-fbc8417aef73"), "" },
                    { 15, "Journal", "", "", false, false, new DateTime(2023, 10, 18, 17, 16, 5, 444, DateTimeKind.Local).AddTicks(7969), "JournalByDateByRow", new Guid("b57fca2f-e314-472c-97a6-609ccc94266b"), "" },
                    { 14, "AccountCollection", "Core", "", false, false, new DateTime(2023, 10, 18, 17, 16, 5, 444, DateTimeKind.Local).AddTicks(7952), "AccountCollectionAccount", new Guid("54339e06-cc44-490a-83bf-d110978a85ba"), "" },
                    { 13, "OperationLog", "Core", "", false, false, new DateTime(2023, 10, 18, 17, 16, 5, 444, DateTimeKind.Local).AddTicks(7941), "OperationLog", new Guid("568124ca-d912-4702-9e23-eb940ac899a3"), "" },
                    { 12, "AccountGroup", "Core", "", false, false, new DateTime(2023, 10, 18, 17, 16, 5, 444, DateTimeKind.Local).AddTicks(7930), "AccountGroup", new Guid("5c714beb-4b99-4faf-8950-b0ed4608950e"), "" }
                });

            migrationBuilder.InsertData(
                schema: "Metadata",
                table: "View",
                columns: new[] { "ViewID", "EntityName", "EntityType", "FetchUrl", "IsCartableIntegrated", "IsHierarchy", "ModifiedDate", "Name", "rowguid", "SearchUrl" },
                values: new object[,]
                {
                    { 11, "Company", "Core", "", false, false, new DateTime(2023, 10, 18, 17, 16, 5, 444, DateTimeKind.Local).AddTicks(7920), "CompanyDb", new Guid("e9374479-b89d-40b7-a241-372c20af5d05"), "" },
                    { 10, "Branch", "Core", "", false, false, new DateTime(2023, 10, 18, 17, 16, 5, 444, DateTimeKind.Local).AddTicks(7909), "Branch", new Guid("13f5a513-b031-4bf6-a3bf-74f145223692"), "/branches" },
                    { 9, "FiscalPeriod", "Core", "", true, false, new DateTime(2023, 10, 18, 17, 16, 5, 444, DateTimeKind.Local).AddTicks(7894), "FiscalPeriod", new Guid("0624b785-7288-412a-a12f-f03a39361c50"), "/fperiods" },
                    { 8, "Project", "Base", "/lookup/projects", true, true, new DateTime(2023, 10, 18, 17, 16, 5, 444, DateTimeKind.Local).AddTicks(7882), "Project", new Guid("b1fbb674-32d1-4b39-8a95-e860e6b587f4"), "/projects" },
                    { 7, "CostCenter", "Base", "/lookup/ccenters", true, true, new DateTime(2023, 10, 18, 17, 16, 5, 444, DateTimeKind.Local).AddTicks(7870), "CostCenter", new Guid("8cb6e429-3056-450a-b288-fa271412616e"), "/ccenters" },
                    { 6, "DetailAccount", "Base", "/lookup/faccounts", true, true, new DateTime(2023, 10, 18, 17, 16, 5, 444, DateTimeKind.Local).AddTicks(7839), "DetailAccount", new Guid("999c942a-1910-46c6-890a-a77e94d3885e"), "/faccounts" },
                    { 5, "Role", "Core", "", false, false, new DateTime(2023, 10, 18, 17, 16, 5, 444, DateTimeKind.Local).AddTicks(7813), "Role", new Guid("c2a874be-8de5-40e2-8abe-4b7b7bf1e1d8"), "" },
                    { 4, "User", "Core", "", false, false, new DateTime(2023, 10, 18, 17, 16, 5, 444, DateTimeKind.Local).AddTicks(7802), "User", new Guid("81306041-7049-4849-b76f-584d38989dce"), "" },
                    { 3, "VoucherLine", "Operational", "/lookup/vouchers/lines", true, false, new DateTime(2023, 10, 18, 17, 16, 5, 444, DateTimeKind.Local).AddTicks(7789), "VoucherLine", new Guid("620ecd48-ba3b-47bf-8aac-2d69e69a2def"), "" },
                    { 2, "Voucher", "Operational", "/lookup/vouchers", true, false, new DateTime(2023, 10, 18, 17, 16, 5, 444, DateTimeKind.Local).AddTicks(7763), "Voucher", new Guid("12391e8e-84e8-426f-94e5-d3f4c48beecb"), "" },
                    { 1, "Account", "Base", "/lookup/accounts", true, true, new DateTime(2023, 10, 18, 17, 16, 5, 444, DateTimeKind.Local).AddTicks(3114), "Account", new Guid("881cc006-c644-49cb-b7cf-e94f1267a654"), "/accounts/lookup" },
                    { 100004, "Attribute", "Base", "", false, false, new DateTime(2023, 10, 18, 17, 16, 5, 444, DateTimeKind.Local).AddTicks(8926), "Attribute", new Guid("8e0e92a4-fd8b-498f-976f-32e404e9b267"), "" },
                    { 17, "Journal", "", "", false, false, new DateTime(2023, 10, 18, 17, 16, 5, 444, DateTimeKind.Local).AddTicks(8106), "JournalByDateByLedger", new Guid("a39785f0-eae0-4652-85cd-f0f1ac2588f9"), "" },
                    { 18, "Journal", "", "", false, false, new DateTime(2023, 10, 18, 17, 16, 5, 444, DateTimeKind.Local).AddTicks(8121), "JournalByDateBySubsidiary", new Guid("b6f27ce9-fa72-49ef-8747-bfa47a6900df"), "" },
                    { 19, "Journal", "", "", false, false, new DateTime(2023, 10, 18, 17, 16, 5, 444, DateTimeKind.Local).AddTicks(8131), "JournalByDateSummary", new Guid("b39958dd-7d9d-4048-b620-ef049d02a3c2"), "" },
                    { 20, "Journal", "", "", false, false, new DateTime(2023, 10, 18, 17, 16, 5, 444, DateTimeKind.Local).AddTicks(8144), "JournalByDateSummaryByDate", new Guid("da4715f9-ba9b-4735-aec4-1a99d0fd3065"), "" },
                    { 39, "CurrencyBook", "", "", false, false, new DateTime(2023, 10, 18, 17, 16, 5, 444, DateTimeKind.Local).AddTicks(8367), "CurrencyBookSingleSummary", new Guid("ad9c4396-b770-4e59-80bf-5901ba1ce8e8"), "" },
                    { 38, "CurrencyBook", "", "", false, false, new DateTime(2023, 10, 18, 17, 16, 5, 444, DateTimeKind.Local).AddTicks(8352), "CurrencyBookSingle", new Guid("e380ed05-d78f-49e0-a842-8ca3a161c6d3"), "" },
                    { 37, "CurrencyBook", "", "", false, false, new DateTime(2023, 10, 18, 17, 16, 5, 444, DateTimeKind.Local).AddTicks(8341), "CurrencyBook", new Guid("d6484cc7-4d85-4d7d-8966-6cf82e920713"), "" },
                    { 36, "TestBalance", "", "", false, false, new DateTime(2023, 10, 18, 17, 16, 5, 444, DateTimeKind.Local).AddTicks(8330), "TestBalance10Column", new Guid("dd638c6d-72c4-4591-9293-0b10417a81d6"), "" },
                    { 35, "TestBalance", "", "", false, false, new DateTime(2023, 10, 18, 17, 16, 5, 444, DateTimeKind.Local).AddTicks(8319), "TestBalance8Column", new Guid("391001bb-640f-41a1-bbc2-80e4728343d0"), "" },
                    { 34, "TestBalance", "", "", false, false, new DateTime(2023, 10, 18, 17, 16, 5, 444, DateTimeKind.Local).AddTicks(8308), "TestBalance6Column", new Guid("d6d907b0-46bb-435c-85ac-e07d75060131"), "" },
                    { 33, "TestBalance", "", "", false, false, new DateTime(2023, 10, 18, 17, 16, 5, 444, DateTimeKind.Local).AddTicks(8295), "TestBalance4Column", new Guid("f664a2eb-8841-4b01-974c-951a5f8c48e7"), "" },
                    { 32, "TestBalance", "", "", false, false, new DateTime(2023, 10, 18, 17, 16, 5, 444, DateTimeKind.Local).AddTicks(8283), "TestBalance2Column", new Guid("c095ec42-4f48-4474-823b-2b468165f1d2"), "" },
                    { 41, "NumberList", "", "", false, false, new DateTime(2023, 10, 18, 17, 16, 5, 444, DateTimeKind.Local).AddTicks(8389), "NumberList", new Guid("4044ac16-85b2-47e5-90af-4cac17b7f79d"), "" },
                    { 31, "CurrencyRate", "", "", false, false, new DateTime(2023, 10, 18, 17, 16, 5, 444, DateTimeKind.Local).AddTicks(8273), "CurrencyRate", new Guid("729fdb16-2913-4fd6-96e8-8a8a085a57b8"), "" },
                    { 29, "AccountBook", "", "", false, false, new DateTime(2023, 10, 18, 17, 16, 5, 444, DateTimeKind.Local).AddTicks(8246), "AccountBookSummary", new Guid("8dd0d2a5-40f6-41c3-be96-f088c8afd2ca"), "" },
                    { 28, "AccountBook", "", "", false, false, new DateTime(2023, 10, 18, 17, 16, 5, 444, DateTimeKind.Local).AddTicks(8235), "AccountBookSingleSummary", new Guid("1a69fcb0-8334-40ea-872e-2c0589348430"), "" },
                    { 27, "AccountBook", "", "", false, false, new DateTime(2023, 10, 18, 17, 16, 5, 444, DateTimeKind.Local).AddTicks(8224), "AccountBookSingle", new Guid("47b7afd6-bbcd-4751-96b8-31ebfdf655db"), "" },
                    { 26, "Journal", "", "", false, false, new DateTime(2023, 10, 18, 17, 16, 5, 444, DateTimeKind.Local).AddTicks(8213), "JournalByNoSummary", new Guid("0e4caed1-4678-467d-94d3-4dd53f81cc7e"), "" },
                    { 25, "Journal", "", "", false, false, new DateTime(2023, 10, 18, 17, 16, 5, 444, DateTimeKind.Local).AddTicks(8202), "JournalByNoBySubsidiary", new Guid("75a25af1-581b-4d7e-a546-ec351fcb3e9e"), "" },
                    { 24, "Journal", "", "", false, false, new DateTime(2023, 10, 18, 17, 16, 5, 444, DateTimeKind.Local).AddTicks(8192), "JournalByNoByLedger", new Guid("992c3b05-7eb8-47bb-b6ce-20447b2b6ec6"), "" },
                    { 23, "Journal", "", "", false, false, new DateTime(2023, 10, 18, 17, 16, 5, 444, DateTimeKind.Local).AddTicks(8181), "JournalByNoByRowDetail", new Guid("bd3e200a-44de-44e6-934b-36ee3f84457e"), "" },
                    { 21, "Journal", "", "", false, false, new DateTime(2023, 10, 18, 17, 16, 5, 444, DateTimeKind.Local).AddTicks(8154), "JournalByDateSummaryByMonth", new Guid("5644521b-0b3f-4651-a771-fe0eb272d00e"), "" },
                    { 30, "Currency", "", "", false, false, new DateTime(2023, 10, 18, 17, 16, 5, 444, DateTimeKind.Local).AddTicks(8257), "Currency", new Guid("dd16a27b-e25f-4d85-a843-ea7b38cd9588"), "" },
                    { 100005, "File", "Base", "", false, false, new DateTime(2023, 10, 18, 17, 16, 5, 444, DateTimeKind.Local).AddTicks(8937), "File", new Guid("f45ac614-da9c-4010-af65-e0809e9cb640"), "" }
                });

            migrationBuilder.InsertData(
                schema: "Auth",
                table: "PermissionGroup",
                columns: new[] { "PermissionGroupID", "Description", "EntityName", "ModifiedDate", "Name", "rowguid", "SourceTypeId", "SubsystemId" },
                values: new object[,]
                {
                    { 100005, "", "file", new DateTime(2023, 10, 18, 17, 16, 5, 348, DateTimeKind.Local).AddTicks(2393), "ManageEntities,files", new Guid("df593251-8e62-44fe-a958-048f56307456"), 1, 100000 },
                    { 13, "", "AccountGroup", new DateTime(2023, 10, 18, 17, 16, 5, 348, DateTimeKind.Local).AddTicks(2059), "ManageEntities,AccountGroups", new Guid("9d8dd5d0-269e-4585-8c82-7a7e284b1011"), 1, 2 },
                    { 8, "", "Vouchers", new DateTime(2023, 10, 18, 17, 16, 5, 348, DateTimeKind.Local).AddTicks(2018), "ManageEntities,Vouchers", new Guid("1b684e89-85df-4cc7-80b7-802abcba1c16"), 2, 2 },
                    { 7, "", "Voucher", new DateTime(2023, 10, 18, 17, 16, 5, 348, DateTimeKind.Local).AddTicks(1991), "Vouchers", new Guid("bcb1ef49-8086-427c-abbf-377ba36b61fa"), 2, 2 },
                    { 5, "", "FiscalPeriod", new DateTime(2023, 10, 18, 17, 16, 5, 348, DateTimeKind.Local).AddTicks(1876), "ManageEntities,FiscalPeriods", new Guid("f877a200-67fa-4e27-a5ed-5e01ba05e40a"), 1, 2 },
                    { 4, "", "Project", new DateTime(2023, 10, 18, 17, 16, 5, 348, DateTimeKind.Local).AddTicks(1868), "ManageEntities,Projects", new Guid("311660fc-bb06-4101-8bb5-866a268e7b76"), 1, 2 },
                    { 3, "", "CostCenter", new DateTime(2023, 10, 18, 17, 16, 5, 348, DateTimeKind.Local).AddTicks(1861), "ManageEntities,CostCenters", new Guid("5f6595bc-4012-491d-aac0-2e8c64c62981"), 1, 2 },
                    { 2, "", "DetailAccount", new DateTime(2023, 10, 18, 17, 16, 5, 348, DateTimeKind.Local).AddTicks(1847), "ManageEntities,DetailAccounts", new Guid("2f96d944-b3f7-46b1-85ef-2738de3a6263"), 1, 2 },
                    { 1, "", "Account", new DateTime(2023, 10, 18, 17, 16, 5, 347, DateTimeKind.Local).AddTicks(9337), "ManageEntities,Accounts", new Guid("afe89a9a-4d47-4c6b-ba17-0672d03db191"), 1, 2 },
                    { 30, "", "SystemIssue", new DateTime(2023, 10, 18, 17, 16, 5, 348, DateTimeKind.Local).AddTicks(2197), "SystemIssueReport", new Guid("fe6e7fea-1c8c-460c-b6b9-ee43d8fba43c"), 3, 1 },
                    { 14, "", "AccountCollection", new DateTime(2023, 10, 18, 17, 16, 5, 348, DateTimeKind.Local).AddTicks(2067), "ManageEntities,AccountCollections", new Guid("c5b1a188-2f2d-484f-8289-7ae32aeccf4a"), 1, 2 },
                    { 22, "", "RowAccess", new DateTime(2023, 10, 18, 17, 16, 5, 348, DateTimeKind.Local).AddTicks(2133), "RowAccessSettings", new Guid("20110f60-98ba-489f-aa8d-e6640aceb404"), 1, 1 },
                    { 20, "", "Setting", new DateTime(2023, 10, 18, 17, 16, 5, 348, DateTimeKind.Local).AddTicks(2119), "Settings", new Guid("227bef2f-4925-48ce-a4ff-d50c191065a9"), 1, 1 },
                    { 18, "", "UserReport", new DateTime(2023, 10, 18, 17, 16, 5, 348, DateTimeKind.Local).AddTicks(2104), "ManageEntities,UserReports", new Guid("47bec82b-c6a0-4e70-9b96-59b9cc949d5f"), 3, 1 },
                    { 17, "", "Report", new DateTime(2023, 10, 18, 17, 16, 5, 348, DateTimeKind.Local).AddTicks(2094), "ManageEntities,Reports", new Guid("06312a20-a1d0-4c2e-bc04-c82f84855e4d"), 3, 1 },
                    { 16, "", "SysOperationLog", new DateTime(2023, 10, 18, 17, 16, 5, 348, DateTimeKind.Local).AddTicks(2087), "ManageEntities,SysOperationLogs", new Guid("0733831b-612b-43fc-bb5a-3fb168bb7cca"), 3, 1 },
                    { 15, "", "OperationLog", new DateTime(2023, 10, 18, 17, 16, 5, 348, DateTimeKind.Local).AddTicks(2074), "ManageEntities,OperationLogs", new Guid("4f247584-449d-4ba8-aebd-c58e0d6c2edf"), 3, 1 },
                    { 12, "", "Role", new DateTime(2023, 10, 18, 17, 16, 5, 348, DateTimeKind.Local).AddTicks(2052), "ManageEntities,Roles", new Guid("eb1a3c96-f112-4608-b8f1-e7a10dfabb79"), 1, 1 },
                    { 11, "", "User", new DateTime(2023, 10, 18, 17, 16, 5, 348, DateTimeKind.Local).AddTicks(2044), "ManageEntities,Users", new Guid("c6805086-336f-4b6d-a20c-ec30b5d422db"), 1, 1 },
                    { 10, "", "Company", new DateTime(2023, 10, 18, 17, 16, 5, 348, DateTimeKind.Local).AddTicks(2037), "ManageEntities,Companies", new Guid("2f163e77-fd88-4607-b11f-644e927e8e11"), 1, 1 },
                    { 9, "", "Branch", new DateTime(2023, 10, 18, 17, 16, 5, 348, DateTimeKind.Local).AddTicks(2027), "ManageEntities,Branches", new Guid("8cde7ea8-c2f1-4495-94d9-5b12900bce67"), 1, 1 },
                    { 21, "", "LogSetting", new DateTime(2023, 10, 18, 17, 16, 5, 348, DateTimeKind.Local).AddTicks(2126), "LogSetting", new Guid("232c7a0f-e484-4e46-875b-4280b408a055"), 1, 1 },
                    { 19, "", "AccountRelations", new DateTime(2023, 10, 18, 17, 16, 5, 348, DateTimeKind.Local).AddTicks(2111), "AccountRelations", new Guid("3cccc6b0-33de-4d8c-b734-6405ab91281e"), 1, 2 },
                    { 6, "", "Currency", new DateTime(2023, 10, 18, 17, 16, 5, 348, DateTimeKind.Local).AddTicks(1890), "ManageEntities,Currencies", new Guid("94941273-6a04-4509-97c1-2be012913bee"), 1, 2 },
                    { 24, "", "Journal", new DateTime(2023, 10, 18, 17, 16, 5, 348, DateTimeKind.Local).AddTicks(2152), "JournalReport", new Guid("65e9f390-8d09-4f99-91f5-c86af68f115e"), 3, 2 },
                    { 100003, "", "Property", new DateTime(2023, 10, 18, 17, 16, 5, 348, DateTimeKind.Local).AddTicks(2377), "ManageEntities,Properties", new Guid("882bcc78-8042-41c6-8dea-d886d7648dea"), 1, 100000 },
                    { 100002, "", "Unit", new DateTime(2023, 10, 18, 17, 16, 5, 348, DateTimeKind.Local).AddTicks(2370), "ManageEntities,Units", new Guid("40640b02-d189-473d-a52e-2ca72bb66945"), 1, 100000 },
                    { 100001, "", "Brand", new DateTime(2023, 10, 18, 17, 16, 5, 348, DateTimeKind.Local).AddTicks(2362), "ManageEntities,Brands", new Guid("e6aeba6e-8bf4-4039-beae-a51a4afda031"), 1, 100000 },
                    { 42, "", "Receipt", new DateTime(2023, 10, 18, 17, 16, 5, 348, DateTimeKind.Local).AddTicks(2355), "Receipts", new Guid("ca0dc72e-f65f-469a-a8d3-dcfc3a62aeca"), 2, 3 },
                    { 41, "", "Payment", new DateTime(2023, 10, 18, 17, 16, 5, 348, DateTimeKind.Local).AddTicks(2348), "Payments", new Guid("6589f678-a424-40ba-a4d0-639a74434dbe"), 2, 3 },
                    { 23, "", "CurrencyRate", new DateTime(2023, 10, 18, 17, 16, 5, 348, DateTimeKind.Local).AddTicks(2141), "CurrencyRate", new Guid("4e6c434e-fedc-4432-a5c4-ef320ea6be30"), 1, 2 },
                    { 39, "", "CheckBookReport", new DateTime(2023, 10, 18, 17, 16, 5, 348, DateTimeKind.Local).AddTicks(2269), "CheckBookReport", new Guid("743d5eb1-a271-47b8-943e-5d9d978791ad"), 3, 3 },
                    { 38, "", "CashRegister", new DateTime(2023, 10, 18, 17, 16, 5, 348, DateTimeKind.Local).AddTicks(2261), "ManageEntities,CashRegisters", new Guid("3c984e85-963b-4454-946f-13c64639b2c9"), 1, 3 },
                    { 37, "", "CheckBook", new DateTime(2023, 10, 18, 17, 16, 5, 348, DateTimeKind.Local).AddTicks(2254), "ManageEntities,CheckBooks", new Guid("49ddae8c-7fc6-4334-a60b-e42c7c05dfdb"), 2, 3 },
                    { 36, "", "Dashboard", new DateTime(2023, 10, 18, 17, 16, 5, 348, DateTimeKind.Local).AddTicks(2247), "Dashboard", new Guid("d556a231-2a8b-42ad-8861-a07bfabbce9f"), 3, 2 },
                    { 40, "", "SourceApp", new DateTime(2023, 10, 18, 17, 16, 5, 348, DateTimeKind.Local).AddTicks(2339), "ManageEntities,SourceApps", new Guid("05ba7ced-edae-4c73-8d83-3ca7b1366673"), 1, 3 },
                    { 34, "", "BalanceSheet", new DateTime(2023, 10, 18, 17, 16, 5, 348, DateTimeKind.Local).AddTicks(2231), "BalanceSheetReport", new Guid("b19fab0a-d47a-4b98-914b-9f0792c9c8b8"), 3, 2 },
                    { 35, "", "SpecialVoucher", new DateTime(2023, 10, 18, 17, 16, 5, 348, DateTimeKind.Local).AddTicks(2239), "SpecialVoucherOps", new Guid("2ae59741-f6f4-475a-8153-b881dbb13c07"), 2, 2 },
                    { 25, "", "AccountBook", new DateTime(2023, 10, 18, 17, 16, 5, 348, DateTimeKind.Local).AddTicks(2159), "AccountBookReport", new Guid("1e8e61e0-2fa4-45a7-97ff-e5d41e620dc5"), 3, 2 },
                    { 26, "", "TestBalance", new DateTime(2023, 10, 18, 17, 16, 5, 348, DateTimeKind.Local).AddTicks(2167), "TestBalanceReport", new Guid("bb75d5be-661b-4075-9320-d7af44aecfad"), 3, 2 },
                    { 27, "", "CurrencyBook", new DateTime(2023, 10, 18, 17, 16, 5, 348, DateTimeKind.Local).AddTicks(2174), "CurrencyBookReport", new Guid("0add6650-d2ef-4867-bf88-fb84bd8f9101"), 3, 2 },
                    { 100004, "", "Attribute", new DateTime(2023, 10, 18, 17, 16, 5, 348, DateTimeKind.Local).AddTicks(2386), "ManageEntities,Attributes", new Guid("807e0da0-4517-4ef7-a451-226c6b0b0d3f"), 1, 100000 }
                });

            migrationBuilder.InsertData(
                schema: "Auth",
                table: "PermissionGroup",
                columns: new[] { "PermissionGroupID", "Description", "EntityName", "ModifiedDate", "Name", "rowguid", "SourceTypeId", "SubsystemId" },
                values: new object[,]
                {
                    { 29, "", "BalanceByAccount", new DateTime(2023, 10, 18, 17, 16, 5, 348, DateTimeKind.Local).AddTicks(2189), "BalanceByAccountReport", new Guid("506954c2-963c-43cb-a7c0-c87f63a11006"), 3, 2 },
                    { 31, "", "ProfitLoss", new DateTime(2023, 10, 18, 17, 16, 5, 348, DateTimeKind.Local).AddTicks(2204), "ProfitLossReport", new Guid("5811f5f0-04e3-4b94-bb44-af18eae6a347"), 3, 2 },
                    { 32, "", "DraftVoucher", new DateTime(2023, 10, 18, 17, 16, 5, 348, DateTimeKind.Local).AddTicks(2215), "DraftVouchers", new Guid("d3ec2a08-5b5f-4dc0-ba18-f34cbf86f3fb"), 2, 2 },
                    { 33, "", "DraftVouchers", new DateTime(2023, 10, 18, 17, 16, 5, 348, DateTimeKind.Local).AddTicks(2222), "ManageEntities,DraftVouchers", new Guid("2ccd2cf3-8fc9-4fbb-b6d4-7fadcbbba19b"), 2, 2 },
                    { 28, "", "ItemBalance", new DateTime(2023, 10, 18, 17, 16, 5, 348, DateTimeKind.Local).AddTicks(2182), "ItemBalanceReport", new Guid("e9b4ea5d-e698-42d9-b4d0-c055c715fde6"), 3, 2 }
                });

            migrationBuilder.InsertData(
                schema: "Config",
                table: "SysLogSetting",
                columns: new[] { "SysLogSettingID", "EntityTypeId", "IsEnabled", "ModifiedDate", "OperationId", "rowguid", "SourceId" },
                values: new object[,]
                {
                    { 40, 5, true, new DateTime(2023, 10, 18, 17, 16, 5, 399, DateTimeKind.Local).AddTicks(7069), 54, new Guid("cdfcef13-fa23-4ddf-a74d-e68d86876bf7"), null },
                    { 47, null, true, new DateTime(2023, 10, 18, 17, 16, 5, 399, DateTimeKind.Local).AddTicks(7130), 24, new Guid("29fd935b-30dc-4bbe-acf2-6cc273374567"), 8 },
                    { 46, null, true, new DateTime(2023, 10, 18, 17, 16, 5, 399, DateTimeKind.Local).AddTicks(7122), 23, new Guid("401a3d67-4390-4d2d-b42e-ec298d9f158b"), 8 },
                    { 45, null, true, new DateTime(2023, 10, 18, 17, 16, 5, 399, DateTimeKind.Local).AddTicks(7114), 22, new Guid("29ba017f-0bfe-4eed-99c6-58667bb4128b"), 7 },
                    { 38, 5, true, new DateTime(2023, 10, 18, 17, 16, 5, 399, DateTimeKind.Local).AddTicks(7052), 58, new Guid("a9315e47-6ec5-47e2-a29e-eedb2f14a2c2"), null },
                    { 25, 2, true, new DateTime(2023, 10, 18, 17, 16, 5, 399, DateTimeKind.Local).AddTicks(6834), 58, new Guid("84195b57-f800-45ba-b2cf-5353b912870d"), null },
                    { 15, 6, true, new DateTime(2023, 10, 18, 17, 16, 5, 399, DateTimeKind.Local).AddTicks(6741), 58, new Guid("c9f1549a-3baf-42d3-9d31-1a89dc29d106"), null },
                    { 7, 1, true, new DateTime(2023, 10, 18, 17, 16, 5, 399, DateTimeKind.Local).AddTicks(6662), 58, new Guid("05767de4-984c-4b7f-9f8f-b9b778a713f8"), null },
                    { 31, 2, true, new DateTime(2023, 10, 18, 17, 16, 5, 399, DateTimeKind.Local).AddTicks(6987), 57, new Guid("f4f5a035-64a5-4f1b-a975-85f35a3f2e46"), null },
                    { 48, null, true, new DateTime(2023, 10, 18, 17, 16, 5, 399, DateTimeKind.Local).AddTicks(7138), 25, new Guid("7a298349-8252-4993-802d-2b79c4577b27"), 8 },
                    { 27, 2, true, new DateTime(2023, 10, 18, 17, 16, 5, 399, DateTimeKind.Local).AddTicks(6955), 54, new Guid("36cf47d3-ac22-4b7b-a228-23a66ab44d72"), null },
                    { 9, 1, true, new DateTime(2023, 10, 18, 17, 16, 5, 399, DateTimeKind.Local).AddTicks(6688), 54, new Guid("bc26ef8f-c684-44de-b1b2-e4e2a837f21f"), null },
                    { 6, 1, true, new DateTime(2023, 10, 18, 17, 16, 5, 399, DateTimeKind.Local).AddTicks(6653), 5, new Guid("36e7016f-afb5-4553-bde2-4f2b82e283d1"), null },
                    { 35, 5, true, new DateTime(2023, 10, 18, 17, 16, 5, 399, DateTimeKind.Local).AddTicks(7026), 4, new Guid("aab0ce41-de28-42b2-9cc9-1d674e2c67be"), null },
                    { 22, 2, true, new DateTime(2023, 10, 18, 17, 16, 5, 399, DateTimeKind.Local).AddTicks(6806), 4, new Guid("b30358e1-4e8e-498e-ab6f-9cab730f364d"), null },
                    { 4, 1, true, new DateTime(2023, 10, 18, 17, 16, 5, 399, DateTimeKind.Local).AddTicks(6626), 4, new Guid("63ad5523-21dc-44d6-938b-a67cb9e91ffb"), null },
                    { 21, 2, true, new DateTime(2023, 10, 18, 17, 16, 5, 399, DateTimeKind.Local).AddTicks(6797), 3, new Guid("23472146-b693-4cea-aaec-65b9706f4ca4"), null },
                    { 13, 6, true, new DateTime(2023, 10, 18, 17, 16, 5, 399, DateTimeKind.Local).AddTicks(6724), 3, new Guid("05888ba2-b3b6-4fc7-b139-23dee131a859"), null },
                    { 17, 6, true, new DateTime(2023, 10, 18, 17, 16, 5, 399, DateTimeKind.Local).AddTicks(6762), 54, new Guid("ddcccd18-3a98-4f5c-b094-74888649ef50"), null },
                    { 14, 6, true, new DateTime(2023, 10, 18, 17, 16, 5, 399, DateTimeKind.Local).AddTicks(6732), 5, new Guid("552d8c05-96e8-45f9-9472-88007f711890"), null },
                    { 20, 2, true, new DateTime(2023, 10, 18, 17, 16, 5, 399, DateTimeKind.Local).AddTicks(6789), 2, new Guid("6a865669-993e-46b7-a3f1-35ca24fc819d"), null },
                    { 2, 1, true, new DateTime(2023, 10, 18, 17, 16, 5, 399, DateTimeKind.Local).AddTicks(6601), 2, new Guid("ffc360fc-2b32-4f63-9373-bc5dc79da898"), null },
                    { 43, 8, true, new DateTime(2023, 10, 18, 17, 16, 5, 399, DateTimeKind.Local).AddTicks(7097), 1, new Guid("c45645cc-236b-4711-a6b5-3dddadb0ad91"), null },
                    { 34, 5, true, new DateTime(2023, 10, 18, 17, 16, 5, 399, DateTimeKind.Local).AddTicks(7018), 1, new Guid("64fde8d1-521f-4e5b-b209-2accf8240c03"), null },
                    { 32, 4, true, new DateTime(2023, 10, 18, 17, 16, 5, 399, DateTimeKind.Local).AddTicks(6995), 1, new Guid("c0981714-9b94-430d-a5d2-d2cf07689b4d"), null },
                    { 19, 2, true, new DateTime(2023, 10, 18, 17, 16, 5, 399, DateTimeKind.Local).AddTicks(6780), 1, new Guid("69cefb66-ae31-4f6d-94fd-a9a565e2b322"), null },
                    { 11, 6, true, new DateTime(2023, 10, 18, 17, 16, 5, 399, DateTimeKind.Local).AddTicks(6707), 1, new Guid("cb940d84-aa29-4543-8256-ba6071d6981a"), null },
                    { 1, 1, true, new DateTime(2023, 10, 18, 17, 16, 5, 399, DateTimeKind.Local).AddTicks(3134), 1, new Guid("8d514538-f5b0-4aca-abb0-202010996763"), null },
                    { 12, 6, true, new DateTime(2023, 10, 18, 17, 16, 5, 399, DateTimeKind.Local).AddTicks(6715), 2, new Guid("cfcb7e2e-13b1-4066-8c52-8830dbce5108"), null },
                    { 24, 2, true, new DateTime(2023, 10, 18, 17, 16, 5, 399, DateTimeKind.Local).AddTicks(6822), 5, new Guid("24f107cc-68e9-4d1f-b79e-c689f4c962b2"), null },
                    { 3, 1, true, new DateTime(2023, 10, 18, 17, 16, 5, 399, DateTimeKind.Local).AddTicks(6618), 3, new Guid("ccb48768-f95d-4ee3-8587-0661a4632618"), null },
                    { 8, 1, true, new DateTime(2023, 10, 18, 17, 16, 5, 399, DateTimeKind.Local).AddTicks(6670), 6, new Guid("c0de457f-9f53-4483-b15b-f4293edbfcf5"), null },
                    { 37, 5, true, new DateTime(2023, 10, 18, 17, 16, 5, 399, DateTimeKind.Local).AddTicks(7044), 5, new Guid("56bbc9f6-6a0e-43ff-b0e2-d2fd5ce9975d"), null },
                    { 10, 1, true, new DateTime(2023, 10, 18, 17, 16, 5, 399, DateTimeKind.Local).AddTicks(6699), 35, new Guid("fe18df10-3333-42d8-ad8f-92c6fc171174"), null },
                    { 42, 5, true, new DateTime(2023, 10, 18, 17, 16, 5, 399, DateTimeKind.Local).AddTicks(7089), 30, new Guid("ec3ebf62-41fe-4826-a06e-603d854c082b"), null },
                    { 30, 2, true, new DateTime(2023, 10, 18, 17, 16, 5, 399, DateTimeKind.Local).AddTicks(6979), 29, new Guid("9775ea26-48a8-4c15-a694-b72aa6e27caf"), null },
                    { 29, 2, true, new DateTime(2023, 10, 18, 17, 16, 5, 399, DateTimeKind.Local).AddTicks(6971), 28, new Guid("3de2f3fa-f71d-43db-9376-878416e8695a"), null }
                });

            migrationBuilder.InsertData(
                schema: "Config",
                table: "SysLogSetting",
                columns: new[] { "SysLogSettingID", "EntityTypeId", "IsEnabled", "ModifiedDate", "OperationId", "rowguid", "SourceId" },
                values: new object[,]
                {
                    { 28, 2, true, new DateTime(2023, 10, 18, 17, 16, 5, 399, DateTimeKind.Local).AddTicks(6963), 27, new Guid("ea4b5065-9dae-48c7-9890-99a8f8c35033"), null },
                    { 36, 5, true, new DateTime(2023, 10, 18, 17, 16, 5, 399, DateTimeKind.Local).AddTicks(7036), 21, new Guid("48452f7d-a4bc-4bde-b183-4dc95654dfb6"), null },
                    { 23, 2, true, new DateTime(2023, 10, 18, 17, 16, 5, 399, DateTimeKind.Local).AddTicks(6814), 21, new Guid("dddfd4fb-51fa-43f5-ab29-80d11d58f035"), null },
                    { 18, 6, true, new DateTime(2023, 10, 18, 17, 16, 5, 399, DateTimeKind.Local).AddTicks(6772), 26, new Guid("3dd3d1bf-bad0-41f1-8a71-2020e3cd7fb7"), null },
                    { 49, 9, true, new DateTime(2023, 10, 18, 17, 16, 5, 399, DateTimeKind.Local).AddTicks(7150), 10, new Guid("ffc3759e-6862-4111-8e46-60b7d6ad6ca3"), null },
                    { 41, 5, true, new DateTime(2023, 10, 18, 17, 16, 5, 399, DateTimeKind.Local).AddTicks(7081), 8, new Guid("830dcbac-7979-4d4c-acd8-b16e59d03aa6"), null },
                    { 44, 8, true, new DateTime(2023, 10, 18, 17, 16, 5, 399, DateTimeKind.Local).AddTicks(7106), 7, new Guid("ab7d184f-e033-4fd2-a9de-862a4794bec8"), null },
                    { 33, 4, true, new DateTime(2023, 10, 18, 17, 16, 5, 399, DateTimeKind.Local).AddTicks(7008), 7, new Guid("572d0e20-8be1-4a4c-affa-d011244ba193"), null },
                    { 39, 5, true, new DateTime(2023, 10, 18, 17, 16, 5, 399, DateTimeKind.Local).AddTicks(7061), 6, new Guid("12dd5df2-3711-4f75-9377-a0b1afcdd6ed"), null },
                    { 26, 2, true, new DateTime(2023, 10, 18, 17, 16, 5, 399, DateTimeKind.Local).AddTicks(6943), 6, new Guid("058032ff-86b5-4a8d-a1ba-b63e1e8132c6"), null },
                    { 16, 6, true, new DateTime(2023, 10, 18, 17, 16, 5, 399, DateTimeKind.Local).AddTicks(6749), 6, new Guid("e23ac484-1e99-4aa4-9797-2751eab6cf07"), null },
                    { 5, 1, true, new DateTime(2023, 10, 18, 17, 16, 5, 399, DateTimeKind.Local).AddTicks(6635), 21, new Guid("43c17668-82a3-4b3c-b16e-0e4e3ba0c5c5"), null }
                });

            migrationBuilder.InsertData(
                schema: "Metadata",
                table: "Command",
                columns: new[] { "CommandID", "HotKey", "IconName", "Index", "ModifiedDate", "ParentId", "PermissionId", "RouteUrl", "rowguid", "TitleKey" },
                values: new object[,]
                {
                    { 39, "", "folder-close", null, new DateTime(2023, 10, 18, 17, 16, 5, 855, DateTimeKind.Local).AddTicks(7010), 1, null, "", new Guid("7be331c4-b4ad-4a7b-bbf2-90327ac0b7db"), "FinancialReports" },
                    { 2, "", "folder-close", null, new DateTime(2023, 10, 18, 17, 16, 5, 855, DateTimeKind.Local).AddTicks(6404), 1, null, "", new Guid("a59749c0-ddb1-453c-b948-15950bb3b18a"), "BaseData" },
                    { 11, "", "folder-close", null, new DateTime(2023, 10, 18, 17, 16, 5, 855, DateTimeKind.Local).AddTicks(6556), 1, null, "", new Guid("cdb504d3-ddbc-426d-9b9f-e2d1dbb5bf5a"), "VoucherOps" },
                    { 16, "", "folder-close", null, new DateTime(2023, 10, 18, 17, 16, 5, 855, DateTimeKind.Local).AddTicks(6611), 1, null, "", new Guid("054d8e96-c98d-48c4-8b54-769b7dd2c857"), "SpecialOps" },
                    { 20, "", "folder-close", null, new DateTime(2023, 10, 18, 17, 16, 5, 855, DateTimeKind.Local).AddTicks(6818), 1, null, "", new Guid("6936cbfa-288e-4ad8-b8fe-e4014df228f3"), "AccountingLedgers" },
                    { 53, "", "folder-close", null, new DateTime(2023, 10, 18, 17, 16, 5, 855, DateTimeKind.Local).AddTicks(7172), 52, null, "", new Guid("c96a9b19-8060-4e57-b98f-f764480de2ca"), "BaseData" },
                    { 100003, "", "folder-close", null, new DateTime(2023, 10, 18, 17, 16, 5, 855, DateTimeKind.Local).AddTicks(7495), 100000, null, "", new Guid("44342fb2-55b9-4171-9411-6b801142395d"), "ProductScopeReports" },
                    { 55, "", "folder-close", null, new DateTime(2023, 10, 18, 17, 16, 5, 855, DateTimeKind.Local).AddTicks(7292), 52, null, "", new Guid("8e9a07cc-8110-4b4a-8b62-e23c7942fc99"), "CheckReports" },
                    { 62, "", "folder-close", null, new DateTime(2023, 10, 18, 17, 16, 5, 855, DateTimeKind.Local).AddTicks(7373), 52, null, "", new Guid("4a7b9982-6f55-41c2-8762-c41cf63e6d27"), "PaymentOperations" },
                    { 63, "", "folder-close", null, new DateTime(2023, 10, 18, 17, 16, 5, 855, DateTimeKind.Local).AddTicks(7383), 52, null, "", new Guid("3b1dab1e-141e-45ec-8243-6def3604bced"), "ReceiptOperations" },
                    { 100001, "", "folder-close", null, new DateTime(2023, 10, 18, 17, 16, 5, 855, DateTimeKind.Local).AddTicks(7475), 100000, null, "", new Guid("822d8c89-efb6-49d2-9d0c-25c179650f47"), "BaseData" },
                    { 100002, "", "folder-close", null, new DateTime(2023, 10, 18, 17, 16, 5, 855, DateTimeKind.Local).AddTicks(7485), 100000, null, "", new Guid("03dc1de6-861b-4a1f-a217-2831e6a8a3ab"), "ProductScopeOperations" },
                    { 54, "", "folder-close", null, new DateTime(2023, 10, 18, 17, 16, 5, 855, DateTimeKind.Local).AddTicks(7185), 52, null, "", new Guid("66be9f96-84c0-43f1-96ea-c5fb2619000a"), "CheckOperations" }
                });

            migrationBuilder.InsertData(
                schema: "Reporting",
                table: "Report",
                columns: new[] { "ReportID", "Code", "CreatedById", "IsDefault", "IsDynamic", "IsGroup", "IsSystem", "ModifiedDate", "ParentId", "ReportViewId", "ResourceKeys", "ServiceUrl", "SubsystemId", "ViewId" },
                values: new object[,]
                {
                    { 100000, "ProductScope", 1, false, false, true, true, new DateTime(2023, 10, 18, 17, 16, 5, 915, DateTimeKind.Local).AddTicks(6894), null, null, "", "", 100000, null },
                    { 93, "Treasury", 1, false, false, true, true, new DateTime(2023, 10, 18, 17, 16, 5, 915, DateTimeKind.Local).AddTicks(6540), null, null, "", "", 3, null },
                    { 42, "Report-QReport-Manage", 1, false, false, true, true, new DateTime(2023, 10, 18, 17, 16, 5, 915, DateTimeKind.Local).AddTicks(5846), null, null, "", "", 1, null },
                    { 13, "Accounting", 1, false, false, true, true, new DateTime(2023, 10, 18, 17, 16, 5, 915, DateTimeKind.Local).AddTicks(4959), null, null, "", "", 2, null },
                    { 1, "Administration", 1, false, false, true, true, new DateTime(2023, 10, 18, 17, 16, 5, 913, DateTimeKind.Local).AddTicks(9530), null, null, "", "", 1, null }
                });

            migrationBuilder.InsertData(
                schema: "Auth",
                table: "Permission",
                columns: new[] { "PermissionID", "Description", "Flag", "GroupId", "ModifiedDate", "Name", "rowguid" },
                values: new object[,]
                {
                    { 127, "", 2, 19, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(4674), "Save", new Guid("b7cdc96f-b1b2-499d-b8ec-c02c9cd9b303") },
                    { 192, "", 4, 33, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(5418), "Print", new Guid("5f5303cf-36bd-4f42-a36b-6802e06f4418") },
                    { 191, "", 2, 33, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(5352), "Filter", new Guid("17ef94f8-5d88-440d-8ea5-6cb2886e7b2f") },
                    { 190, "", 1, 33, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(5344), "View", new Guid("573e3c92-df80-4b4e-9256-848994a779ae") },
                    { 196, "", 2048, 32, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(5450), "Normalize", new Guid("077cf91c-dcdc-4bfd-ae1e-e3dbb86ee839") },
                    { 189, "", 1024, 32, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(5337), "NavigateEntities,DraftVouchers", new Guid("a25f8f07-6d37-4d0b-9097-5c2179229a05") },
                    { 188, "", 512, 32, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(5330), "UndoCheck", new Guid("c6920147-01ba-4dac-b098-508c7131d879") },
                    { 187, "", 256, 32, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(5323), "Check", new Guid("9597cb95-50cc-476d-af6c-be1ad9ceec82") },
                    { 186, "", 128, 32, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(5315), "DeleteLine", new Guid("8c0fb251-09e1-459e-b7d7-50449eff5b31") },
                    { 185, "", 64, 32, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(5308), "EditLine", new Guid("575a2c2f-47aa-4cde-b423-7df6b8c74491") },
                    { 184, "", 32, 32, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(5297), "CreateLine", new Guid("b2125255-4725-4214-9a4b-5989e6b7f270") },
                    { 183, "", 16, 32, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(5290), "Print", new Guid("2c131465-e56d-4800-89aa-de57bb08595f") },
                    { 182, "", 8, 32, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(5283), "Delete", new Guid("143c94ed-cd98-4d51-93bd-89e95f951602") },
                    { 181, "", 4, 32, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(5276), "Edit", new Guid("5f7a05aa-2b18-4c4a-b1f6-064cabab3fe6") },
                    { 180, "", 2, 32, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(5269), "Create", new Guid("f59c315b-0866-46f3-92cc-35b9ba147e54") },
                    { 179, "", 1, 32, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(5261), "View", new Guid("42fc46de-8ec9-41d7-aa60-5aadd7036964") },
                    { 200, "", 16, 31, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(5480), "FilterByRef", new Guid("afd28ed0-4683-4163-b2c9-29c29c8f429f") },
                    { 178, "", 8, 31, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(5134), "Export", new Guid("ce6a14a7-0d7e-4cc7-8d30-41a2093bc20e") },
                    { 193, "", 8, 33, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(5429), "Export", new Guid("7239a3b2-09bb-4a97-9d84-034f66c6594c") },
                    { 194, "", 16, 33, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(5436), "GroupCheck", new Guid("c45751b9-7a09-446d-80b0-f6f1901c2075") },
                    { 195, "", 32, 33, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(5443), "GroupUndoCheck", new Guid("ca920775-688b-4b80-9eeb-a5925904257a") },
                    { 201, "", 1, 34, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(5490), "View", new Guid("5a8721cf-0798-48ee-8f3a-2940e80c9216") },
                    { 219, "", 128, 37, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(5630), "CreatePages", new Guid("73781bc1-0122-458b-9f82-09adb997628e") },
                    { 218, "", 64, 37, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(5621), "NavigateEntities,CheckBooks", new Guid("2c8b3373-01dc-4cc0-906d-cf518e511fef") },
                    { 217, "", 32, 37, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(5614), "Delete", new Guid("3cf1ce07-e791-4b02-a6ab-52f9c414ae6d") },
                    { 216, "", 16, 37, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(5604), "Edit", new Guid("16d24c42-770a-4b38-860c-96c92e437944") },
                    { 215, "", 8, 37, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(5596), "Create", new Guid("17905a8d-1659-4f1a-aaf8-6158c614077e") },
                    { 214, "", 4, 37, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(5589), "Print", new Guid("b20b2b9c-19cd-4bfd-981a-35fa12115a2b") },
                    { 213, "", 2, 37, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(5582), "Filter", new Guid("771be0a1-118c-4fc1-a93e-eff039d635b0") },
                    { 212, "", 1, 37, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(5575), "View", new Guid("1df47fef-8be4-4058-ac04-93a33074a93b") },
                    { 177, "", 4, 31, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(5127), "Print", new Guid("5881a1e7-87ff-449f-824a-93c8197a4cce") },
                    { 211, "", 2, 36, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(5567), "ManageWidgets", new Guid("3eb6d2a6-87d2-4309-86f0-1adf1339914a") },
                    { 209, "", 8, 35, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(5552), "UncheckClosingVoucher", new Guid("65f0afe2-8f3b-4159-8c98-40c499cb95eb") },
                    { 208, "", 4, 35, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(5542), "IssueClosingVoucher", new Guid("a931e1f0-03f2-4c99-bfba-28596e84e183") },
                    { 207, "", 2, 35, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(5534), "IssueClosingTempAccountsVoucher", new Guid("699652d7-ab20-4497-a24d-aedd8a51b15b") },
                    { 206, "", 1, 35, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(5527), "IssueOpeningVoucher", new Guid("533cff0d-25c8-4d63-8ca9-dd90512b20c1") },
                    { 205, "", 16, 34, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(5520), "FilterByRef", new Guid("2bc80507-fb8b-46e0-8f3d-54166690dad3") },
                    { 204, "", 8, 34, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(5513), "Export", new Guid("71da4c3e-c354-4f53-8faf-0cb761a7e06e") },
                    { 203, "", 4, 34, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(5505), "Print", new Guid("40fd6c44-40e1-436d-bb7d-7c8d66813742") },
                    { 202, "", 2, 34, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(5498), "Filter", new Guid("0e459149-b29d-49ca-9343-c534510fd95d") },
                    { 210, "", 1, 36, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(5560), "ManageDashboard", new Guid("fb5d57e6-1075-4594-ae09-def7b371da67") },
                    { 176, "", 2, 31, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(5116), "Filter", new Guid("4b322f4b-382a-4893-a694-15ed1007c734") }
                });

            migrationBuilder.InsertData(
                schema: "Auth",
                table: "Permission",
                columns: new[] { "PermissionID", "Description", "Flag", "GroupId", "ModifiedDate", "Name", "rowguid" },
                values: new object[,]
                {
                    { 175, "", 1, 31, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(5109), "View", new Guid("6695573f-b50d-4d3d-b6ec-9939bacbc0bd") },
                    { 199, "", 32, 29, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(5472), "FilterByRef", new Guid("851fce25-71fe-446b-96d9-905f7fbb97fe") },
                    { 153, "", 1, 26, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(4943), "View", new Guid("ecce0503-702b-480c-bfb2-3b5c21c2533b") },
                    { 152, "", 32, 25, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(4933), "ViewByBranch", new Guid("8062bac7-abf8-47f9-8db0-d41a1536aa10") },
                    { 151, "", 16, 25, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(4925), "Mark", new Guid("14ece5e3-99f0-473a-9f63-dfdfc52ce50b") },
                    { 150, "", 8, 25, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(4855), "Export", new Guid("382189e6-1f97-4208-a3f3-d4208598c1b5") },
                    { 149, "", 4, 25, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(4848), "Print", new Guid("c5f872a0-f472-43b6-b2f8-af3bd1a4097d") },
                    { 148, "", 2, 25, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(4840), "Filter", new Guid("d955673c-a45f-4224-9b47-75584573d96d") },
                    { 147, "", 1, 25, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(4833), "View", new Guid("a53078ef-7ce1-4fd9-b1fb-c672c9adec60") },
                    { 146, "", 32, 24, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(4826), "ViewByBranch", new Guid("2fb6d84b-a597-417f-9a99-830a9a2dcaf2") },
                    { 154, "", 2, 26, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(4951), "Filter", new Guid("36b6970e-9f17-4b70-936a-fe97f0fe3160") },
                    { 145, "", 16, 24, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(4819), "Mark", new Guid("608860a3-424b-4400-9e1f-1f6d517c9893") },
                    { 143, "", 4, 24, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(4801), "Print", new Guid("91ef4d47-3709-4aa8-930b-d7b331133499") },
                    { 142, "", 2, 24, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(4794), "Filter", new Guid("2c84355d-b93d-4adc-80d7-15ef7c6ed189") },
                    { 141, "", 1, 24, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(4786), "View", new Guid("a20ce458-2ca8-44d8-9307-a43f17ff2462") },
                    { 140, "", 64, 23, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(4779), "Delete", new Guid("2d5626c8-7993-4778-bcc0-e438641f5c84") },
                    { 139, "", 32, 23, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(4772), "Edit", new Guid("061b9fa4-21b3-496a-a899-6f45dd1538a9") },
                    { 138, "", 16, 23, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(4764), "Create", new Guid("71e97cd3-77d7-492e-8061-9a4c4d58274a") },
                    { 137, "", 8, 23, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(4756), "Export", new Guid("1c6adfd5-658e-435e-9921-8af3aad392c2") },
                    { 136, "", 4, 23, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(4745), "Print", new Guid("7db2a2e4-6a62-4943-9ebe-87861622a37d") },
                    { 144, "", 8, 24, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(4808), "Export", new Guid("f35d9fdd-15bb-4fa7-97d3-25006b0f2eb1") },
                    { 220, "", 256, 37, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(5637), "DeletePages", new Guid("5330d649-d104-4390-924d-5e9b6f21dc75") },
                    { 155, "", 4, 26, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(4958), "Print", new Guid("0f81b3a4-afa2-45c7-82be-b302bd677d67") },
                    { 157, "", 16, 26, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(4972), "ViewByBranch", new Guid("8d0519df-ca1b-48cf-a773-f7acb91f2889") },
                    { 173, "", 16, 29, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(5095), "ViewByBranch", new Guid("36b13a22-e4b1-41c6-8b60-2b03376f384b") },
                    { 172, "", 8, 29, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(5088), "Export", new Guid("855e311b-1255-4a19-8720-3d8bc62436f9") },
                    { 171, "", 4, 29, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(5080), "Print", new Guid("f6721332-12b1-4d47-9ff0-e02a75944fc9") },
                    { 170, "", 2, 29, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(5073), "Filter", new Guid("67b23f78-7117-421f-a2df-aff8e1d59675") },
                    { 169, "", 1, 29, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(5066), "View", new Guid("0396848e-839b-4397-a75f-3acd59365868") },
                    { 198, "", 32, 28, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(5465), "FilterByRef", new Guid("df5b6592-ad11-4ddc-a3e1-04e77dc293a3") },
                    { 168, "", 16, 28, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(5055), "ViewByBranch", new Guid("c7875034-d851-45e2-a465-58e7b9eea441") },
                    { 167, "", 8, 28, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(5048), "Export", new Guid("cb128fa6-0c02-4f9e-a07a-e75df73d0e6f") },
                    { 156, "", 8, 26, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(4965), "Export", new Guid("552fc94f-2081-4069-b7c5-6711355b6abc") },
                    { 166, "", 4, 28, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(5041), "Print", new Guid("67ac76ee-fb6b-434b-a5a9-724e02c2444a") },
                    { 164, "", 1, 28, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(5027), "View", new Guid("0a8812bf-2b9c-4cbe-9b16-e527a521b6e0") },
                    { 163, "", 32, 27, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(5019), "ViewByBranch", new Guid("53344a09-76b6-4b78-97de-cf6adfe1c0c6") },
                    { 162, "", 16, 27, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(5012), "Mark", new Guid("e7b04d9b-6a47-4309-9a82-47b084c084d1") },
                    { 161, "", 8, 27, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(5005), "Export", new Guid("a35a9360-a67e-4ee6-a4bf-38ae8a226a46") },
                    { 160, "", 4, 27, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(4994), "Print", new Guid("7cad1ed7-6055-46c2-a559-606403e82348") },
                    { 159, "", 2, 27, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(4987), "Filter", new Guid("474b77f5-feec-4dfb-b285-de9ef18b3eb6") },
                    { 158, "", 1, 27, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(4980), "View", new Guid("a7892523-be6a-41cd-a1cf-2008a63fc763") },
                    { 197, "", 32, 26, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(5458), "FilterByRef", new Guid("21ad378a-6820-4e70-af92-3c8cfd02e604") }
                });

            migrationBuilder.InsertData(
                schema: "Auth",
                table: "Permission",
                columns: new[] { "PermissionID", "Description", "Flag", "GroupId", "ModifiedDate", "Name", "rowguid" },
                values: new object[,]
                {
                    { 165, "", 2, 28, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(5034), "Filter", new Guid("96ccf17a-0d48-406b-878c-53c2dcd2f132") },
                    { 135, "", 2, 23, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(4738), "Filter", new Guid("32494e6a-31cd-4a55-b4bc-61c028504e1d") },
                    { 221, "", 512, 37, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(5645), "CancelPage", new Guid("13f2cc87-1934-4c24-8891-a6c6068191c9") },
                    { 223, "", 2048, 37, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(5659), "ConnectToCheck", new Guid("ddb4a09e-0ef2-4c92-a0b5-fe0622d5df6b") },
                    { 100011, "", 8, 100002, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(6415), "Export", new Guid("ab06bf20-f989-4b46-8297-37c5dac1eb0d") },
                    { 100010, "", 4, 100002, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(6404), "Print", new Guid("98d01cc7-074b-4213-9b01-71cbdbb40fc5") },
                    { 100009, "", 2, 100002, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(6397), "Filter", new Guid("d1abcf3b-fc14-466b-b0e1-4ed3a4529b3d") },
                    { 100008, "", 1, 100002, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(6389), "View", new Guid("09ca21d8-2126-4ef1-ae81-997a00ebf677") },
                    { 100007, "", 64, 100001, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(6323), "Delete", new Guid("9e38bbf8-07de-4437-91e6-c2d9290cf55c") },
                    { 100006, "", 32, 100001, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(6316), "Edit", new Guid("9c5d65c8-9a3d-424d-bc71-3efe822773e2") },
                    { 100005, "", 16, 100001, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(6309), "Create", new Guid("0086f9eb-0987-4811-bc3e-1d2062c6caf4") },
                    { 100004, "", 8, 100001, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(6302), "Export", new Guid("fe0970a1-75c3-42bc-9874-8c2df62d4073") },
                    { 100003, "", 4, 100001, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(6295), "Print", new Guid("74619604-d84b-45e8-97e7-ea72219b8e95") },
                    { 100002, "", 2, 100001, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(6284), "Filter", new Guid("25f70369-8913-45f0-8070-3496ccf46ed5") },
                    { 100001, "", 1, 100001, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(6277), "View", new Guid("835acb3b-c7bb-4527-ae3e-db803c020102") },
                    { 283, "", 2048, 42, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(6247), "UndoRegister", new Guid("48ba4868-8098-4782-af75-13db0fee0481") },
                    { 267, "", 1024, 42, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(6124), "UndoApprove", new Guid("4f746845-9a81-4f0e-9631-36f6332b5e25") },
                    { 266, "", 512, 42, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(6117), "Approve", new Guid("da705f90-6708-4a21-b40e-14b21017072c") },
                    { 265, "", 256, 42, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(6110), "UndoConfirm", new Guid("7a7ccca7-8aaa-4eea-9984-28f20493b8b0") },
                    { 264, "", 128, 42, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(6099), "Confirm", new Guid("16deb954-ede6-4099-a0b3-5af57480ee70") },
                    { 263, "", 64, 42, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(6091), "Register", new Guid("09b2d3be-819a-495e-b495-f3490b3a776a") },
                    { 100012, "", 16, 100002, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(6422), "Create", new Guid("829d80ed-b616-4a56-87ea-0a5b71e81d66") },
                    { 100013, "", 32, 100002, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(6430), "Edit", new Guid("e213e569-e486-4b04-bffb-087acd6b461b") },
                    { 100014, "", 64, 100002, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(6437), "Delete", new Guid("f87eca7d-ec93-4a0e-a5f5-5a656f72038e") },
                    { 100015, "", 1, 100003, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(6445), "View", new Guid("b52689c6-4149-4200-9d64-69809bd87dae") },
                    { 100033, "", 16, 100005, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(6583), "Create", new Guid("052dda39-f74a-4586-8022-53f25ac3df51") },
                    { 100032, "", 8, 100005, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(6576), "Export", new Guid("fd4e54b0-78b0-45e1-ab0c-4f0376fc7d56") },
                    { 100031, "", 4, 100005, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(6568), "Print", new Guid("147ac7cc-ae1e-4464-a596-3bd491fb78e2") },
                    { 100030, "", 2, 100005, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(6561), "Filter", new Guid("a6d92b5a-3bbb-442b-9aa5-4bf8daabc9f9") },
                    { 100029, "", 1, 100005, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(6554), "View", new Guid("7c535718-7fa4-4e4e-a47d-ee175ab150db") },
                    { 100028, "", 64, 100004, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(6547), "Delete", new Guid("841918d6-0e33-49bf-9ed9-315d7316f5fb") },
                    { 100027, "", 32, 100004, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(6539), "Edit", new Guid("26fb86f9-560c-4f1a-b17b-7439f91cf980") },
                    { 100026, "", 16, 100004, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(6529), "Create", new Guid("d34dedbd-61c6-45cd-a810-4c7cf0558ed7") },
                    { 262, "", 32, 42, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(6084), "NavigateEntities,Receipts", new Guid("e9afeb3d-32db-4e5e-ac7a-48179b7b4445") },
                    { 100025, "", 8, 100004, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(6521), "Export", new Guid("68e81ee3-7998-47a8-91bd-fb43c9b36f25") },
                    { 100023, "", 2, 100004, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(6507), "Filter", new Guid("e59bcee6-0290-4487-9c64-1c697093a864") },
                    { 100022, "", 1, 100004, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(6499), "View", new Guid("f965bb0c-14b0-45cf-9269-00618784fd99") },
                    { 100021, "", 64, 100003, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(6492), "Delete", new Guid("d141b8d6-9767-4630-8199-b47ed36c6a2f") },
                    { 100020, "", 32, 100003, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(6484), "Edit", new Guid("cc2c192f-b443-4cb6-ad51-6c045313e3a8") },
                    { 100019, "", 16, 100003, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(6477), "Create", new Guid("cc31de96-f513-4362-86e6-be3fe87aac35") },
                    { 100018, "", 8, 100003, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(6466), "Export", new Guid("3ac4f9b7-0602-47ae-9ce6-fe41e6c70822") },
                    { 100017, "", 4, 100003, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(6459), "Print", new Guid("53967e3f-4466-4b57-be6e-5a6ed5d16041") }
                });

            migrationBuilder.InsertData(
                schema: "Auth",
                table: "Permission",
                columns: new[] { "PermissionID", "Description", "Flag", "GroupId", "ModifiedDate", "Name", "rowguid" },
                values: new object[,]
                {
                    { 100016, "", 2, 100003, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(6452), "Filter", new Guid("f782268f-f5f3-4e92-9e62-820ee3434d41") },
                    { 100024, "", 4, 100004, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(6514), "Print", new Guid("e4c7d304-8309-49bf-9573-1466e39b36e5") },
                    { 261, "", 16, 42, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(6077), "Delete", new Guid("a54eebc3-8782-41cf-be65-47169fcb6dc1") },
                    { 260, "", 8, 42, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(6069), "Edit", new Guid("9360dc06-80ac-4dd5-82b2-5d40a5711528") },
                    { 259, "", 4, 42, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(6062), "Create", new Guid("4f017210-52fb-428f-8871-b02b796b7dd2") },
                    { 239, "", 1, 40, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(5842), "View", new Guid("2caed546-6f5e-404e-b017-eae7797b0e14") },
                    { 238, "", 32, 39, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(5834), "UndoArchive", new Guid("48beb9b1-f59d-461b-97ad-11693160a51e") },
                    { 237, "", 16, 39, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(5827), "Archive", new Guid("88e9bdef-7678-48c2-9042-ac34fd085ae1") },
                    { 236, "", 8, 39, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(5820), "Export", new Guid("2a269b55-70d6-428e-aa10-f2ffb1d50c06") },
                    { 235, "", 4, 39, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(5813), "Print", new Guid("02298f11-cce9-409a-b672-719cc5c93db9") },
                    { 234, "", 2, 39, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(5805), "Filter", new Guid("a29c5019-9835-4683-940b-e5ce26cd9de0") },
                    { 233, "", 1, 39, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(5798), "View", new Guid("49ca0089-5630-4fa6-acb2-436ac161c672") },
                    { 279, "Mark an inactive cash register as active", 512, 38, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(6215), "Reactivate", new Guid("eb4aa12f-3940-4cd3-8bd8-1829b588b99f") },
                    { 240, "", 2, 40, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(5849), "Filter", new Guid("d28d86ee-eb17-430c-b7ee-3081537e9ed9") },
                    { 278, "Mark an active cash register as inactive", 256, 38, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(6208), "Deactivate", new Guid("27b38392-503b-4656-b8dd-c39cc6c730e2") },
                    { 231, "", 64, 38, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(5720), "Delete", new Guid("da4fd10f-c603-4ecc-b786-87e8ae4f547e") },
                    { 230, "", 32, 38, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(5713), "Edit", new Guid("3ba723ca-978c-4dd0-9562-f475f38360c3") },
                    { 229, "", 16, 38, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(5705), "Create", new Guid("5b21a63a-2dda-4188-bbe0-be0287a93688") },
                    { 228, "", 8, 38, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(5698), "Export", new Guid("9f64e311-831f-4dc0-9ab3-4d13e502b0ac") },
                    { 227, "", 4, 38, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(5691), "Print", new Guid("e4b71229-c12b-44ee-92d1-b343688c0262") },
                    { 226, "", 2, 38, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(5684), "Filter", new Guid("79e4fc3a-92b5-4cfd-af5a-724080798b1f") },
                    { 225, "", 1, 38, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(5677), "View", new Guid("02e2d500-3f97-4042-b140-8254593208c5") },
                    { 224, "", 4096, 37, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(5666), "DisconnectFromCheck", new Guid("ee8c97f6-c5a3-4b15-bd62-de0fb3a5a891") },
                    { 232, "", 128, 38, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(5787), "AssignCashRegisterUser", new Guid("3b24044d-5186-4151-84ea-250b7ff102e1") },
                    { 222, "", 1024, 37, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(5652), "UndoCancelPage", new Guid("06891ebe-ab82-42f2-a359-4005dbd64c7c") },
                    { 241, "", 4, 40, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(5859), "Print", new Guid("d1a886fe-17c8-4557-8099-0cd4dc38d26a") },
                    { 243, "", 16, 40, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(5874), "Create", new Guid("d4290e68-fc03-4085-8a19-6f52f5bcddc1") },
                    { 258, "", 2, 42, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(6054), "Print", new Guid("196949bb-30d7-4ab9-a86d-78be73f2a4e2") },
                    { 257, "", 1, 42, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(5982), "View", new Guid("26c8cf65-e4a6-4cf0-a176-5fddc36f8be1") },
                    { 282, "", 2048, 41, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(6240), "UndoRegister", new Guid("0c5f8816-471a-4f91-9f77-b46ed941e93e") },
                    { 256, "", 1024, 41, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(5972), "UndoApprove", new Guid("12ea5bae-4846-49a1-8ae4-767b760fdb3f") },
                    { 255, "", 512, 41, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(5965), "Approve", new Guid("589bb5a2-cb91-4cf2-b8b4-ed2ef112d760") },
                    { 254, "", 256, 41, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(5957), "UndoConfirm", new Guid("d3013c37-c510-40f2-8f03-b1d256813778") },
                    { 253, "", 128, 41, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(5950), "Confirm", new Guid("28ec77b2-6640-4f00-bcc9-db6705631e7d") },
                    { 252, "", 64, 41, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(5943), "Register", new Guid("1f26b456-0d9e-4e26-ae1f-c11045bfe037") },
                    { 242, "", 8, 40, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(5867), "Export", new Guid("b05741bb-265b-4b8b-b291-cef8cbd575fe") },
                    { 251, "", 32, 41, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(5936), "NavigateEntities,Payments", new Guid("fefede27-c676-4419-8ec4-b17106bb811a") },
                    { 249, "", 8, 41, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(5921), "Edit", new Guid("fc342ec4-2123-4921-93e5-baa201bee828") },
                    { 248, "", 4, 41, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(5910), "Create", new Guid("afc72cf0-5bf1-4f46-935d-d0cdb10b88dd") },
                    { 247, "", 2, 41, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(5903), "Print", new Guid("ef6fb9a6-de9e-4dad-9cb1-0f2780124266") },
                    { 246, "", 1, 41, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(5896), "View", new Guid("b23e46a9-a12e-4ba9-9837-c02ab9a09959") },
                    { 281, "Mark an inactive source/application as active", 256, 40, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(6233), "Reactivate", new Guid("ff642c7d-d788-4b9e-a66e-1d3865b525bd") }
                });

            migrationBuilder.InsertData(
                schema: "Auth",
                table: "Permission",
                columns: new[] { "PermissionID", "Description", "Flag", "GroupId", "ModifiedDate", "Name", "rowguid" },
                values: new object[,]
                {
                    { 280, "Mark an active source/application as inactive", 128, 40, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(6222), "Deactivate", new Guid("9e4d2ee1-acbf-416f-a438-56d2d5e77321") },
                    { 245, "", 64, 40, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(5889), "Delete", new Guid("4ba2a947-6d4e-4d22-bc03-03ff0cb1eaa9") },
                    { 244, "", 32, 40, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(5881), "Edit", new Guid("334b166c-ba22-42e0-af8c-60fc1bd826b8") },
                    { 250, "", 16, 41, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(5928), "Delete", new Guid("4aead445-c4eb-4245-8606-25b6ad2a50c2") },
                    { 134, "", 1, 23, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(4731), "View", new Guid("61eb3bd3-5668-4ada-a29e-bc69763c9827") },
                    { 100035, "", 64, 100005, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(6601), "Delete", new Guid("fded4048-b1ee-4152-acb6-665091a8fce1") },
                    { 126, "", 1, 19, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(4667), "View", new Guid("646053ab-39f7-4e59-af89-8e3203be2ab1") },
                    { 131, "", 2, 21, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(4709), "Save", new Guid("a85c0b3e-fdb9-4936-be0c-39960eb99ad6") },
                    { 130, "", 1, 21, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(4701), "View", new Guid("d65da88f-0a9b-4e52-94e9-a5ded85779fa") },
                    { 129, "", 2, 20, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(4692), "Save", new Guid("05f4c91d-f982-4b37-bcc9-f90d9c5bfd02") },
                    { 128, "", 1, 20, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(4681), "View", new Guid("959259bd-0c2d-4293-9fd6-309dbd37bf9d") },
                    { 125, "", 4, 18, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(4660), "SetDefault", new Guid("fdd8fc73-51ac-4731-9cf6-e78e25e86b53") },
                    { 124, "", 2, 18, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(4652), "Delete", new Guid("1c76d616-c278-4437-81c5-a97aec31c449") },
                    { 132, "", 1, 22, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(4716), "View", new Guid("c19c5faf-82f7-44c6-b026-c69b794c7c23") },
                    { 123, "", 1, 18, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(4645), "Save", new Guid("4aa4c3fc-2742-44f1-bfa6-08bfa6238f4d") },
                    { 121, "", 2, 17, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(4630), "Design", new Guid("5f0c3ce8-ff52-4af3-aaa2-96ce1a1c01b6") },
                    { 120, "", 1, 17, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(4619), "View", new Guid("7f9f4630-867e-45b6-8f3c-072e3fb0f9b4") },
                    { 119, "", 32, 16, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(4560), "ViewArchive", new Guid("83c8645d-4fd1-4b36-8031-d882af1cdc40") },
                    { 118, "", 16, 16, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(4553), "Archive", new Guid("a70a5444-8658-4794-a3bd-015909d9d78f") },
                    { 117, "", 8, 16, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(4545), "Export", new Guid("c5fc4539-96de-424c-b849-e8b4ee021cab") },
                    { 116, "", 4, 16, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(4538), "Print", new Guid("335af06a-fd80-455b-87bc-1c4bde2c4600") },
                    { 122, "", 4, 17, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(4637), "QuickReportDesign", new Guid("44d1723f-b313-423f-8496-9e12ba5deab6") },
                    { 133, "", 2, 22, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(4723), "Save", new Guid("a26323ff-991c-4ee4-ad0a-e6ca712b4d01") },
                    { 174, "", 1, 30, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(5102), "View", new Guid("e054a252-0600-44c9-8e3a-7e8c6552764a") },
                    { 284, "", 2, 30, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(6255), "Filter", new Guid("57898cf8-a859-44bb-866d-c682ebbb0da9") },
                    { 11, "", 8, 2, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(3528), "Export", new Guid("4b4bc789-1aaf-492c-8096-99b049a73c75") },
                    { 10, "", 4, 2, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(3520), "Print", new Guid("e287dc94-5be6-4551-906f-2330e05ddccc") },
                    { 9, "", 2, 2, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(3511), "Filter", new Guid("91ccc71c-dd1d-4ccf-bb92-cc94e3d4d834") },
                    { 8, "", 1, 2, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(3494), "View", new Guid("7176aa2e-a621-46e6-83d3-04808a117547") },
                    { 269, "Mark an inactive account as active", 256, 1, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(6139), "Reactivate", new Guid("62dcc638-6732-41a1-a380-56f31f94cc3b") },
                    { 268, "Mark an active account as inactive", 128, 1, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(6132), "Deactivate", new Guid("55e02472-6966-4b96-99e0-7c9565b8eb98") },
                    { 7, "", 64, 1, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(3406), "Delete", new Guid("50a914b4-660f-401a-ac6d-2fba3b9f36ee") },
                    { 6, "", 32, 1, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(3399), "Edit", new Guid("3287a7cb-69ed-493b-a905-f70ff2c3220d") },
                    { 5, "", 16, 1, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(3379), "Create", new Guid("266992dc-695c-463a-931e-5b11c0e799e6") },
                    { 4, "", 8, 1, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(3371), "Export", new Guid("71207e9f-c25f-41a0-91d5-863d9eb4257f") },
                    { 100034, "", 32, 100005, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(6590), "Edit", new Guid("6340376e-cfed-441d-96d8-3e3c69930197") },
                    { 2, "", 2, 1, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(3351), "Filter", new Guid("599b966c-3c9a-4f73-82bd-b95f04cf9567") },
                    { 1, "", 1, 1, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(2018), "View", new Guid("01d66a42-9b4e-4dd9-a004-9de1e7025baa") },
                    { 286, "", 8, 30, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(6270), "Export", new Guid("b8b0c5f5-cacc-4ea1-8dd3-12ea85e0c933") },
                    { 285, "", 4, 30, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(6263), "Print", new Guid("3d432674-e268-466f-bf30-cd96bdd00817") },
                    { 115, "", 2, 16, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(4531), "Filter", new Guid("1cd057e7-7e2c-4123-99d6-cd2dd84ea5ed") },
                    { 12, "", 16, 2, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(3535), "Create", new Guid("e460e379-05b9-4289-8a92-3df7377d5f14") }
                });

            migrationBuilder.InsertData(
                schema: "Auth",
                table: "Permission",
                columns: new[] { "PermissionID", "Description", "Flag", "GroupId", "ModifiedDate", "Name", "rowguid" },
                values: new object[,]
                {
                    { 114, "", 1, 16, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(4524), "View", new Guid("d1a0fbf7-387d-40ad-9914-961892ffbf04") },
                    { 112, "", 16, 15, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(4505), "Archive", new Guid("4f4e612f-fd52-4139-8549-7c8b4f3a5e72") },
                    { 83, "", 2, 11, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(4286), "Filter", new Guid("81cc8946-caca-43cc-8bd4-3511989f2321") },
                    { 82, "", 1, 11, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(4278), "View", new Guid("da9c1194-a829-4482-bc5e-4b7fc547a102") },
                    { 81, "", 8, 10, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(4271), "Export", new Guid("af4e343b-185d-4afd-a39f-a0213941c447") },
                    { 80, "", 4, 10, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(4261), "Print", new Guid("cd2672ce-e9ce-4aac-b2cd-cafb1c4ff7b8") },
                    { 79, "", 2, 10, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(4252), "Filter", new Guid("f390162f-0611-4872-a7c7-30ada8bbdce9") },
                    { 78, "", 1, 10, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(4145), "View", new Guid("95bb651b-30a7-4e34-a716-eb76acb00031") },
                    { 84, "", 4, 11, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(4294), "Print", new Guid("5a13feda-2813-4584-b2e6-994dcdfb0636") },
                    { 77, "", 128, 9, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(4137), "AssignRolesToEntity,Branch", new Guid("78376b1c-4f56-4f10-92a3-52c8f02bb0d0") },
                    { 75, "", 32, 9, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(4123), "Edit", new Guid("8415291f-d0c2-459d-86ea-ff7c816b6d08") },
                    { 74, "", 16, 9, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(4116), "Create", new Guid("d89035e1-1f86-41d7-ad14-46a784583e91") },
                    { 73, "", 8, 9, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(4109), "Export", new Guid("ccb53346-c65f-4403-a243-cfaebad106b8") },
                    { 72, "", 4, 9, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(4098), "Print", new Guid("537c0430-1ffc-4ade-b67b-ef0047ce878e") },
                    { 71, "", 2, 9, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(4091), "Filter", new Guid("aff44519-f3b4-453a-8c7a-691d10e397aa") },
                    { 70, "", 1, 9, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(4084), "View", new Guid("82e561ad-a1d0-43bd-80eb-a8fb0e41b044") },
                    { 76, "", 64, 9, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(4130), "Delete", new Guid("de2e147f-5f6d-4806-807c-c516d3a21f9d") },
                    { 85, "", 8, 11, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(4301), "Export", new Guid("59eb3752-2d24-4a31-88e8-e37e8b6f1228") },
                    { 86, "", 16, 11, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(4308), "Create", new Guid("6fc6208a-edac-452c-87d5-f3affd5c03b2") },
                    { 87, "", 32, 11, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(4315), "Edit", new Guid("d1655c4f-d89c-483f-beee-bbf3c7857144") },
                    { 111, "", 8, 15, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(4498), "Export", new Guid("7fb61775-13ac-4ebd-b035-a43586ef8e05") },
                    { 110, "", 4, 15, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(4491), "Print", new Guid("73fd06ab-f6d2-4ec7-ae0b-a10424f335e2") },
                    { 109, "", 2, 15, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(4483), "Filter", new Guid("5d4af382-c00b-4eea-acc9-48d4c8fb1542") },
                    { 108, "", 1, 15, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(4476), "View", new Guid("6eedb1b7-f19a-457d-a582-20cd288519a5") },
                    { 98, "", 512, 12, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(4401), "AssignEntityToRole,FiscalPeriod", new Guid("b3b70fe8-2273-44b4-8790-2f3fdf648914") },
                    { 97, "", 256, 12, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(4393), "AssignEntityToRole,Branch", new Guid("933e5078-e82f-40f8-ae02-96eb090c8b29") },
                    { 96, "", 128, 12, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(4383), "AssignEntityToRole,User", new Guid("0c8e024e-6c63-488e-a45a-982d8bed34b2") },
                    { 95, "", 64, 12, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(4376), "Delete", new Guid("871f4229-baf4-4f49-a8a3-7ac557cac3c1") },
                    { 94, "", 32, 12, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(4369), "Edit", new Guid("69466680-affb-41a2-96d3-8f9dacc0eb36") },
                    { 93, "", 16, 12, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(4362), "Create", new Guid("61a47915-6e23-4e55-a3b2-e24ab86cceac") },
                    { 92, "", 8, 12, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(4355), "Export", new Guid("03722459-aafe-41c0-a252-7b44d5997eb2") },
                    { 91, "", 4, 12, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(4348), "Print", new Guid("8b57f4f2-ce7c-4551-82c0-335f4639dd2b") },
                    { 90, "", 2, 12, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(4340), "Filter", new Guid("a69f7197-4602-4e6b-93a1-107255ab9d7c") },
                    { 89, "", 1, 12, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(4333), "View", new Guid("f9bcf0b9-aa5e-4fbb-8087-cbd25448ff70") },
                    { 88, "", 64, 11, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(4322), "AssignRolesToEntity,User", new Guid("c133b3c9-1d16-4d65-8a05-8597cb9b7d16") },
                    { 113, "", 32, 15, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(4516), "ViewArchive", new Guid("2b2e2b38-0789-43d3-9ef5-cce116253da6") },
                    { 13, "", 32, 2, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(3542), "Edit", new Guid("0af535f0-ca09-4f97-bcb3-a6ac578a793f") },
                    { 3, "", 4, 1, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(3363), "Print", new Guid("a185ba6b-bec5-4196-88fa-4035e936a671") },
                    { 270, "Mark an active detail account as inactive", 128, 2, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(6146), "Deactivate", new Guid("47b85828-bae7-485a-b0cb-3ebd331d3df1") },
                    { 59, "", 16384, 7, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(3996), "Finalize", new Guid("277a0828-d667-4b70-b8da-48f0232bd410") },
                    { 58, "", 8192, 7, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(3989), "UndoApprove", new Guid("f852a180-96b7-48f5-863f-7cd6411cafa4") },
                    { 57, "", 4096, 7, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(3981), "Approve", new Guid("3ce5aaab-d2db-459e-b290-fa96acdfa6c4") }
                });

            migrationBuilder.InsertData(
                schema: "Auth",
                table: "Permission",
                columns: new[] { "PermissionID", "Description", "Flag", "GroupId", "ModifiedDate", "Name", "rowguid" },
                values: new object[,]
                {
                    { 14, "", 64, 2, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(3550), "Delete", new Guid("9afacb0a-da99-43ac-a500-82ed4d1dfc4b") },
                    { 55, "", 1024, 7, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(3962), "Confirm", new Guid("e8456fe7-dc62-48d5-b11f-73e4444b7497") },
                    { 54, "", 512, 7, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(3955), "UndoCheck", new Guid("4bea90aa-4a5a-4992-92ab-557d3d29df7a") },
                    { 60, "", 32768, 7, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(4004), "NavigateEntities,Vouchers", new Guid("d881ccb4-ad06-4465-994e-cdab9faf054c") },
                    { 53, "", 256, 7, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(3948), "Check", new Guid("1659a95b-5d96-4925-9e64-59aa4da9b838") },
                    { 51, "", 64, 7, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(3909), "EditLine", new Guid("9b4e11b9-3bf2-45c5-9ba9-1824fe8a5f95") },
                    { 50, "", 32, 7, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(3902), "CreateLine", new Guid("5715f8f4-415c-4bdd-a7c0-0685ed70265c") },
                    { 49, "", 16, 7, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(3894), "Print", new Guid("553a9151-1245-4a76-bc7b-5436648948bf") },
                    { 48, "", 8, 7, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(3884), "Delete", new Guid("ded6ad52-0003-443d-a905-715a1b481309") },
                    { 47, "", 4, 7, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(3876), "Edit", new Guid("c6b61540-c169-44d9-b0ff-d22cadad80b5") },
                    { 46, "", 2, 7, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(3869), "Create", new Guid("e8d845e5-5072-423e-8128-0f147f0a5c59") },
                    { 52, "", 128, 7, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(3940), "DeleteLine", new Guid("008503a2-a604-41dd-821d-f17e5c7fa3f6") },
                    { 61, "", 1, 8, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(4011), "View", new Guid("c1c6d346-eb87-4cf7-b1af-95b2ff446afe") },
                    { 62, "", 2, 8, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(4018), "Filter", new Guid("5079cfd8-d8a0-494a-9aaf-3a5fabfb02bd") },
                    { 63, "", 4, 8, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(4025), "Print", new Guid("afb88856-4cea-47a1-92c4-308fc98ff459") },
                    { 107, "", 2, 14, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(4469), "Save", new Guid("b95a546a-abd2-4019-bb02-3f0b1e70380f") },
                    { 106, "", 1, 14, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(4462), "View", new Guid("86621779-4845-47db-8d54-4c7aee25bc85") },
                    { 105, "", 64, 13, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(4454), "Delete", new Guid("dfa67993-7bcd-4549-978f-5612cdf132f2") },
                    { 104, "", 32, 13, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(4443), "Edit", new Guid("0b7a0e61-b1bd-4221-9383-55b789195d6d") },
                    { 103, "", 16, 13, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(4436), "Create", new Guid("a99e81d2-59df-40d8-8cb5-fdb9b5fdf2a5") },
                    { 102, "", 8, 13, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(4429), "Export", new Guid("38deac3a-6320-48c3-8e56-0fd586b3c301") },
                    { 101, "", 4, 13, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(4422), "Print", new Guid("e7cd3abc-850c-48e3-a2ad-c093434bd4de") },
                    { 100, "", 2, 13, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(4415), "Filter", new Guid("5eb26cb9-9b57-45b2-baa4-827af0c9606c") },
                    { 99, "", 1, 13, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(4408), "View", new Guid("5a4ea56c-000a-4bb2-92c0-d152c813dfc6") },
                    { 69, "", 256, 8, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(4076), "GroupFinalize", new Guid("51c55b4e-2ed5-407a-9da5-7e5f0982656a") },
                    { 68, "", 128, 8, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(4068), "GroupUndoConfirm", new Guid("762c560b-6349-437b-8273-83b771a2476c") },
                    { 67, "", 64, 8, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(4061), "GroupConfirm", new Guid("91c8ea0b-a824-4330-835d-a86f2c122a9f") },
                    { 66, "", 32, 8, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(4054), "GroupUndoCheck", new Guid("418de377-c282-494c-8ec3-e5f03df3ef0f") },
                    { 65, "", 16, 8, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(4044), "GroupCheck", new Guid("c3dc9382-2580-4bae-9e2b-053efe8df10d") },
                    { 64, "", 8, 8, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(4032), "Export", new Guid("9004c9f6-1aa5-4c95-bfc2-c95195f3004e") },
                    { 45, "", 1, 7, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(3861), "View", new Guid("775a9718-c295-48e3-9e6d-82f36b93103c") },
                    { 277, "Mark an inactive currency as active", 256, 6, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(6200), "Reactivate", new Guid("7b33d66e-07aa-436a-8be6-68d28929ef0b") },
                    { 56, "", 2048, 7, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(3970), "UndoConfirm", new Guid("08925e9a-d0ef-47dd-ad3a-353791865c76") },
                    { 44, "", 128, 6, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(3853), "ChangeStatus", new Guid("9be8edb2-c68f-4d52-9ff1-104f24437648") },
                    { 26, "", 16, 4, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(3650), "Create", new Guid("0eb609ba-f8a0-4a61-bffc-d6c13c6aec9d") },
                    { 25, "", 8, 4, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(3642), "Export", new Guid("9799f572-5687-486e-ada8-cd5a4a0d5cd4") },
                    { 24, "", 4, 4, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(3631), "Print", new Guid("251281aa-8829-439a-ac48-1ef8a1a27e51") },
                    { 23, "", 2, 4, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(3624), "Filter", new Guid("0b52cada-1204-46ae-8372-1eccf98427f0") },
                    { 22, "", 1, 4, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(3616), "View", new Guid("01c80523-e4c5-4807-b863-7c7d2aaf53db") },
                    { 273, "Mark an inactive cost center as active", 256, 3, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(6171), "Reactivate", new Guid("b8266e4b-c285-40cd-bcc5-3a0f15e961b1") },
                    { 272, "Mark an active cost center as inactive", 128, 3, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(6161), "Deactivate", new Guid("9e2d7c4d-eefe-493e-b9c0-27972e4dc148") },
                    { 21, "", 64, 3, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(3609), "Delete", new Guid("c9077fd4-06f7-4c67-bafc-a69af38fa11c") }
                });

            migrationBuilder.InsertData(
                schema: "Auth",
                table: "Permission",
                columns: new[] { "PermissionID", "Description", "Flag", "GroupId", "ModifiedDate", "Name", "rowguid" },
                values: new object[,]
                {
                    { 20, "", 32, 3, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(3602), "Edit", new Guid("b61ca481-ff56-4ac5-8af2-a8306d0fce0e") },
                    { 19, "", 16, 3, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(3594), "Create", new Guid("a9448dd3-8ad4-4ca4-8d98-24756ae77839") },
                    { 18, "", 8, 3, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(3586), "Export", new Guid("74c9806a-fa9b-4074-bf42-4c7a26987662") },
                    { 276, "Mark an active currency as inactive", 128, 6, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(6193), "Deactivate", new Guid("3091af37-6001-4453-9291-1ad452681e44") },
                    { 16, "", 2, 3, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(3565), "Filter", new Guid("1ae8b580-125b-4f4f-836e-6d732937ab33") },
                    { 15, "", 1, 3, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(3557), "View", new Guid("ae959482-4d6f-40d1-99b2-5c5c7d4c76bb") },
                    { 271, "Mark an inactive detail account as active", 256, 2, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(6153), "Reactivate", new Guid("4b82ad7f-06ae-4e0a-a79b-f1f6452f6566") },
                    { 27, "", 32, 4, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(3657), "Edit", new Guid("51ecbf73-e52e-4074-adda-d4a85cc467d0") },
                    { 28, "", 64, 4, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(3665), "Delete", new Guid("c554e910-7a81-4d2a-b5fe-5efb7a90b79c") },
                    { 17, "", 4, 3, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(3577), "Print", new Guid("b30b0bfd-710c-4d9d-ae58-f1e11930be79") },
                    { 275, "Mark an inactive project as active", 256, 4, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(6186), "Reactivate", new Guid("a64f620d-d0d8-49fd-85e2-64f657a0a34c") },
                    { 274, "Mark an active project as inactive", 128, 4, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(6179), "Deactivate", new Guid("1140b600-f9a8-46e2-a2d9-3f6f2abaaa64") },
                    { 43, "", 64, 6, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(3784), "Delete", new Guid("268dd561-a640-49b7-8963-5dd6c730cc47") },
                    { 42, "", 32, 6, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(3777), "Edit", new Guid("4ef69c50-2f79-4e0c-a1f0-3af6d7d4d8d5") },
                    { 41, "", 16, 6, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(3770), "Create", new Guid("47a016cf-26b1-4287-a627-fb18061e2069") },
                    { 39, "", 4, 6, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(3751), "Print", new Guid("e6b76741-c840-469a-862a-4a5d1caabf9f") },
                    { 38, "", 2, 6, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(3744), "Filter", new Guid("91b54355-88f6-48cb-9095-7ea8cdfb218f") },
                    { 37, "", 1, 6, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(3737), "View", new Guid("1ac2d24b-3d4d-4ae8-9a2a-b73aa4f583b5") },
                    { 40, "", 8, 6, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(3759), "Export", new Guid("a397040d-ea76-46e6-97a0-ab3a07f31345") },
                    { 35, "", 64, 5, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(3722), "Delete", new Guid("fb7496a1-785e-4e32-b7ed-2350d7462244") },
                    { 34, "", 32, 5, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(3715), "Edit", new Guid("db932bb5-9282-4bb0-8e6f-c0cd85395a20") },
                    { 33, "", 16, 5, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(3705), "Create", new Guid("27a0e805-1faa-47f9-84c6-fd5e56c01a9f") },
                    { 32, "", 8, 5, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(3695), "Export", new Guid("fa8da7a9-e2b9-4141-bf17-e4fac6dc43f4") },
                    { 31, "", 4, 5, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(3687), "Print", new Guid("801c9f86-f5fb-40d6-931c-62db2226b523") },
                    { 30, "", 2, 5, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(3680), "Filter", new Guid("04545822-132f-47f5-bc8b-89a4ffc3e9a7") },
                    { 36, "", 128, 5, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(3730), "AssignRolesToEntity,FiscalPeriod", new Guid("5e31e7be-d513-40a6-909e-8b0a3b7327fc") },
                    { 29, "", 1, 5, new DateTime(2023, 10, 18, 17, 16, 5, 378, DateTimeKind.Local).AddTicks(3672), "View", new Guid("c682c791-dcfd-479f-a7eb-df7d53ef7e5c") }
                });

            migrationBuilder.InsertData(
                schema: "Reporting",
                table: "LocalReport",
                columns: new[] { "LocalReportID", "Caption", "LocaleId", "ModifiedDate", "ReportId", "rowguid", "Template" },
                values: new object[,]
                {
                    { 52, "Accounting", 4, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(3211), 13, new Guid("f9054144-b5df-439c-81d7-feef9f6a34ee"), "" },
                    { 51, "Accounting", 3, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(3202), 13, new Guid("61c6761b-fac6-4087-910a-22fa457df5d1"), "" },
                    { 50, "حسابداری", 2, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(3188), 13, new Guid("d299dde9-cad9-42e3-be61-73f43f96f0aa"), "" },
                    { 3, "Administration", 3, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(2570), 1, new Guid("412b1e68-0a4c-4f53-b674-05ad5a428596"), "" },
                    { 4, "Administration", 4, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(2581), 1, new Guid("b967cbcb-9cc4-4970-8def-e84c2a17f766"), "" },
                    { 2, "راهبری", 2, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(2554), 1, new Guid("000cd07d-c627-4e99-91fb-2bb006c976a0"), "" },
                    { 165, "Manage quick reports", 1, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(4815), 42, new Guid("bc9f9e77-6d26-4907-9911-16e42338a5de"), "" },
                    { 49, "Accounting", 1, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(3178), 13, new Guid("fb100b25-0704-420e-89b6-658e0f159e21"), "" },
                    { 166, "مدیریت گزارشات فوری", 2, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(4825), 42, new Guid("934dcc72-e7fc-4bb4-8c81-9d5b5fdd3f00"), "" },
                    { 1, "Administration", 1, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(811), 1, new Guid("a8d1d997-8d8a-4052-90fa-55cdea0498c9"), "" },
                    { 168, "Manage quick reports", 4, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(4848), 42, new Guid("73178c1b-1aec-4368-835f-00304ee70ce4"), "" },
                    { 167, "Manage quick reports", 3, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(4840), 42, new Guid("91fb6251-1c6b-44f3-a487-f84438ce5ad1"), "" },
                    { 100000, "ProductScope", 1, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(6641), 100000, new Guid("7b0020eb-6453-40e7-92c0-da9c8e71b73f"), "" },
                    { 100001, "محصولات", 2, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(6650), 100000, new Guid("dab49b91-2755-41a1-aaed-935450cc434e"), "" }
                });

            migrationBuilder.InsertData(
                schema: "Reporting",
                table: "Report",
                columns: new[] { "ReportID", "Code", "CreatedById", "IsDefault", "IsDynamic", "IsGroup", "IsSystem", "ModifiedDate", "ParentId", "ReportViewId", "ResourceKeys", "ServiceUrl", "SubsystemId", "ViewId" },
                values: new object[] { 14, "Accnt-Base", 1, false, false, true, true, new DateTime(2023, 10, 18, 17, 16, 5, 915, DateTimeKind.Local).AddTicks(4974), 13, null, "", "", 2, null });

            migrationBuilder.InsertData(
                schema: "Reporting",
                table: "Report",
                columns: new[] { "ReportID", "Code", "CreatedById", "IsDefault", "IsDynamic", "IsGroup", "IsSystem", "ModifiedDate", "ParentId", "ReportViewId", "ResourceKeys", "ServiceUrl", "SubsystemId", "ViewId" },
                values: new object[,]
                {
                    { 94, "Treasury-Base", 1, false, false, true, true, new DateTime(2023, 10, 18, 17, 16, 5, 915, DateTimeKind.Local).AddTicks(6549), 93, null, "", "", 3, null },
                    { 95, "Treasury-Operation", 1, false, false, true, true, new DateTime(2023, 10, 18, 17, 16, 5, 915, DateTimeKind.Local).AddTicks(6559), 93, null, "", "", 3, null },
                    { 16, "Accnt-Report", 1, false, false, true, true, new DateTime(2023, 10, 18, 17, 16, 5, 915, DateTimeKind.Local).AddTicks(5010), 13, null, "", "", 2, null },
                    { 43, "QReport-Design-Template", 1, true, false, false, false, new DateTime(2023, 10, 18, 17, 16, 5, 915, DateTimeKind.Local).AddTicks(5856), 42, null, "", "", 1, null },
                    { 4, "Admin-Report", 1, false, false, true, true, new DateTime(2023, 10, 18, 17, 16, 5, 915, DateTimeKind.Local).AddTicks(4810), 1, null, "", "", 1, null },
                    { 3, "Admin-Operation", 1, false, false, true, true, new DateTime(2023, 10, 18, 17, 16, 5, 915, DateTimeKind.Local).AddTicks(4794), 1, null, "", "", 1, null },
                    { 2, "Admin-Base", 1, false, false, true, true, new DateTime(2023, 10, 18, 17, 16, 5, 915, DateTimeKind.Local).AddTicks(4729), 1, null, "", "", 1, null },
                    { 100001, "ProductScope-Base", 1, false, false, true, true, new DateTime(2023, 10, 18, 17, 16, 5, 915, DateTimeKind.Local).AddTicks(6903), 100000, null, "", "", 100000, null },
                    { 100002, "ProductScope-Operation", 1, false, false, true, true, new DateTime(2023, 10, 18, 17, 16, 5, 915, DateTimeKind.Local).AddTicks(6914), 100000, null, "", "", 100000, null },
                    { 100003, "ProductScope-Report", 1, false, false, true, true, new DateTime(2023, 10, 18, 17, 16, 5, 915, DateTimeKind.Local).AddTicks(6923), 100000, null, "", "", 100000, null },
                    { 15, "Accnt-Operation", 1, false, false, true, true, new DateTime(2023, 10, 18, 17, 16, 5, 915, DateTimeKind.Local).AddTicks(4988), 13, null, "", "", 2, null },
                    { 96, "Treasury-Report", 1, false, false, true, true, new DateTime(2023, 10, 18, 17, 16, 5, 915, DateTimeKind.Local).AddTicks(6568), 93, null, "", "", 3, null }
                });

            migrationBuilder.InsertData(
                schema: "Metadata",
                table: "Command",
                columns: new[] { "CommandID", "HotKey", "IconName", "Index", "ModifiedDate", "ParentId", "PermissionId", "RouteUrl", "rowguid", "TitleKey" },
                values: new object[,]
                {
                    { 100008, "", "list", null, new DateTime(2023, 10, 18, 17, 16, 5, 855, DateTimeKind.Local).AddTicks(7551), 100001, 100029, "/product-scope/files", new Guid("ce455388-d1d6-4bc2-ac3a-1c68f6b38b62"), "Files" },
                    { 3, "Ctrl+Shift+G", "th-large", null, new DateTime(2023, 10, 18, 17, 16, 5, 855, DateTimeKind.Local).AddTicks(6431), 2, 99, "/finance/account-groups", new Guid("d0ab03e0-b7aa-4dc5-beb3-c126aa182b87"), "AccountGroup" },
                    { 15, "Ctrl+Shift+V", "list", null, new DateTime(2023, 10, 18, 17, 16, 5, 855, DateTimeKind.Local).AddTicks(6600), 11, 61, "/finance/voucher", new Guid("9c848a5a-4e21-4954-9d7f-e8f1b4ddbe6e"), "Vouchers" },
                    { 14, "Ctrl+L", "list", null, new DateTime(2023, 10, 18, 17, 16, 5, 855, DateTimeKind.Local).AddTicks(6588), 11, 60, "/finance/vouchers/last", new Guid("dabb0798-c9a0-40ba-84ba-b292411ffa0e"), "LastVoucher" },
                    { 12, "Ctrl+Alt+N", "plus", null, new DateTime(2023, 10, 18, 17, 16, 5, 855, DateTimeKind.Local).AddTicks(6567), 11, 46, "/finance/vouchers/new", new Guid("4c67e3c2-fd0c-4679-a866-c2e0b33dfd07"), "NewVoucher" },
                    { 19, "Ctrl+Alt+I", "list", null, new DateTime(2023, 10, 18, 17, 16, 5, 855, DateTimeKind.Local).AddTicks(6804), 16, 45, "/finance/vouchers/close-temp-accounts", new Guid("8e415851-6932-4431-94de-074eafb1df16"), "ClosingTempAccounts" },
                    { 18, "Ctrl+Alt+U", "list", null, new DateTime(2023, 10, 18, 17, 16, 5, 855, DateTimeKind.Local).AddTicks(6659), 16, 45, "/finance/vouchers/closing-voucher", new Guid("5f124058-caed-411b-b7cc-70b231ffc3e8"), "IssueClosingVoucher" },
                    { 17, "Ctrl+Alt+Y", "list", null, new DateTime(2023, 10, 18, 17, 16, 5, 855, DateTimeKind.Local).AddTicks(6644), 16, 45, "/finance/vouchers/opening-voucher", new Guid("75f0ec2c-65bc-4c64-807c-6b7cccdb0c30"), "IssueOpeningVoucher" },
                    { 13, "Ctrl+S", "search", null, new DateTime(2023, 10, 18, 17, 16, 5, 855, DateTimeKind.Local).AddTicks(6577), 11, 45, "/finance/vouchers/by-no", new Guid("72d374ae-562b-4bfe-9c72-7b8c190e9fb9"), "VoucherByNo" },
                    { 10, "Ctrl+Shift+U", "usd", null, new DateTime(2023, 10, 18, 17, 16, 5, 855, DateTimeKind.Local).AddTicks(6545), 2, 37, "/finance/currency", new Guid("e2e0fce6-d438-42c8-ba3e-3e203dd0cba9"), "Currency" },
                    { 26, "Ctrl+Shift+F", "list", null, new DateTime(2023, 10, 18, 17, 16, 5, 855, DateTimeKind.Local).AddTicks(6897), 23, 29, "/organization/fiscalperiod", new Guid("320dcfa2-6daa-4532-843f-89b93ad4526d"), "FiscalPeriods" },
                    { 7, "Ctrl+Shift+P", "file", null, new DateTime(2023, 10, 18, 17, 16, 5, 855, DateTimeKind.Local).AddTicks(6491), 2, 22, "/finance/projects", new Guid("64f391c3-1f5d-459a-a789-272309afe555"), "Project" },
                    { 9, "Ctrl+Shift+H", "list", null, new DateTime(2023, 10, 18, 17, 16, 5, 855, DateTimeKind.Local).AddTicks(6530), 2, 106, "/finance/account-collection", new Guid("a44735db-4347-48fe-a2cf-5b85fca2c2c6"), "AccountCollections" },
                    { 6, "Ctrl+Shift+C", "tower", null, new DateTime(2023, 10, 18, 17, 16, 5, 855, DateTimeKind.Local).AddTicks(6480), 2, 15, "/finance/costCenter", new Guid("ce22c44e-cce8-414c-ae0c-c31a7e1930d0"), "CostCenter" },
                    { 4, "Ctrl+Shift+A", "th-list", null, new DateTime(2023, 10, 18, 17, 16, 5, 855, DateTimeKind.Local).AddTicks(6442), 2, 1, "/finance/account", new Guid("e635ea8e-d23d-4cad-8ad0-bb130fbfdc91"), "Account" },
                    { 42, "Ctrl+Shift+S", "tasks", null, new DateTime(2023, 10, 18, 17, 16, 5, 855, DateTimeKind.Local).AddTicks(7042), 27, 174, "/finance/system-issue", new Guid("9636e6cd-1e4e-40e5-9d96-6c05d2322d81"), "SystemIssue" },
                    { 30, "Ctrl+Alt+W", "lock", null, new DateTime(2023, 10, 18, 17, 16, 5, 855, DateTimeKind.Local).AddTicks(6944), 27, 132, "/admin/viewRowPermission", new Guid("934ad75c-39a2-43c0-b68e-52b367048c72"), "RowAccessSettings" },
                    { 45, "", "list", null, new DateTime(2023, 10, 18, 17, 16, 5, 855, DateTimeKind.Local).AddTicks(7079), 27, 130, "/admin/log-settings", new Guid("65927f69-f98d-4c26-b602-84efa3e69fe7"), "LogSettings" },
                    { 31, "Ctrl+K", "wrench", null, new DateTime(2023, 10, 18, 17, 16, 5, 855, DateTimeKind.Local).AddTicks(6957), 27, 128, "/config/settings", new Guid("5e377ea8-4e52-44b0-86a8-33d54ea63d0e"), "Settings" },
                    { 38, "", "list", null, new DateTime(2023, 10, 18, 17, 16, 5, 855, DateTimeKind.Local).AddTicks(7000), 37, 120, "/tadbir/reports", new Guid("af6e1033-843a-444e-b4bb-ecbb8a08ec4c"), "ReportManagement" },
                    { 32, "Ctrl+Alt+L", "list", null, new DateTime(2023, 10, 18, 17, 16, 5, 855, DateTimeKind.Local).AddTicks(6970), 27, 108, "/admin/operation-log", new Guid("6f523021-dac8-4602-8ff6-dcfe3c143b38"), "OperationLogs" },
                    { 29, "Ctrl+Alt+H", "list", null, new DateTime(2023, 10, 18, 17, 16, 5, 855, DateTimeKind.Local).AddTicks(6933), 27, 89, "/admin/roles", new Guid("e18636bc-6107-4a0f-aeeb-ae421d4ee80a"), "Roles" },
                    { 28, "Ctrl+Alt+K", "user", null, new DateTime(2023, 10, 18, 17, 16, 5, 855, DateTimeKind.Local).AddTicks(6920), 27, 82, "/admin/users", new Guid("74d8d9e2-7ca3-4092-b058-4a406878d230"), "Users" },
                    { 24, "Ctrl+Alt+C", "list", null, new DateTime(2023, 10, 18, 17, 16, 5, 855, DateTimeKind.Local).AddTicks(6862), 23, 78, "/organization/companies", new Guid("2bf79862-d18b-4c66-8981-5aba9a79a4a4"), "Companies" },
                    { 25, "Ctrl+Alt+E", "list", null, new DateTime(2023, 10, 18, 17, 16, 5, 855, DateTimeKind.Local).AddTicks(6883), 23, 70, "/organization/branches", new Guid("faf753e1-bc92-4f61-a855-5fed20e6936e"), "Branches" },
                    { 5, "Ctrl+Shift+D", "th", null, new DateTime(2023, 10, 18, 17, 16, 5, 855, DateTimeKind.Local).AddTicks(6454), 2, 8, "/finance/detailAccount", new Guid("d56c95be-3cac-4f3e-a283-a79a2090c889"), "DetailAccount" },
                    { 21, "Ctrl+Alt+Z", "list", null, new DateTime(2023, 10, 18, 17, 16, 5, 855, DateTimeKind.Local).AddTicks(6828), 20, 141, "/finance/journal", new Guid("1f542cee-6916-4d45-92fa-54bb7d0f06a2"), "JournalLedger" },
                    { 8, "Ctrl+Shift+R", "transfer", null, new DateTime(2023, 10, 18, 17, 16, 5, 855, DateTimeKind.Local).AddTicks(6506), 2, 126, "/finance/accountrelations", new Guid("fdf5267c-7c7c-41e7-99d3-64aa664a1f08"), "AccountRelations" },
                    { 59, "", "list", null, new DateTime(2023, 10, 18, 17, 16, 5, 855, DateTimeKind.Local).AddTicks(7335), 53, 225, "/treasury/cash-register", new Guid("603a5f3c-31d1-4c19-a44b-d034b4b7abfc"), "CashRegisters" },
                    { 100006, "", "list", null, new DateTime(2023, 10, 18, 17, 16, 5, 855, DateTimeKind.Local).AddTicks(7525), 100001, 100015, "/product-scope/properties", new Guid("a451d37a-95cd-438b-a77c-2cf66ff9eba0"), "ProductProperty" },
                    { 100005, "", "list", null, new DateTime(2023, 10, 18, 17, 16, 5, 855, DateTimeKind.Local).AddTicks(7515), 100001, 100008, "/product-scope/units", new Guid("157ec30d-d462-4b8a-a2e2-c872c9497c67"), "Units" },
                    { 100004, "", "list", null, new DateTime(2023, 10, 18, 17, 16, 5, 855, DateTimeKind.Local).AddTicks(7505), 100001, 100001, "/product-scope/brands", new Guid("58a63a50-ed58-4038-a6e4-25727b126b62"), "Brands" },
                    { 67, "", "list", null, new DateTime(2023, 10, 18, 17, 16, 5, 855, DateTimeKind.Local).AddTicks(7424), 63, 262, "/treasury/receipts/last", new Guid("201a803a-9d9f-4e74-9cd4-e726e278391b"), "LastReceiptForm" },
                    { 65, "", "plus", null, new DateTime(2023, 10, 18, 17, 16, 5, 855, DateTimeKind.Local).AddTicks(7404), 63, 259, "/treasury/receipts/new", new Guid("0703f580-12f0-4390-9b7d-3fd211551f58"), "NewReceiptForm" },
                    { 69, "", "search", null, new DateTime(2023, 10, 18, 17, 16, 5, 855, DateTimeKind.Local).AddTicks(7450), 63, 257, "/treasury/receipts/by-no", new Guid("5633820d-9e58-4964-962d-4fdee83accb8"), "ReceiptFormbyNo" },
                    { 66, "", "list", null, new DateTime(2023, 10, 18, 17, 16, 5, 855, DateTimeKind.Local).AddTicks(7414), 62, 251, "/treasury/payments/last", new Guid("d8e42017-8931-486a-9cd0-08f578621209"), "LastPaymentForm" },
                    { 64, "", "plus", null, new DateTime(2023, 10, 18, 17, 16, 5, 855, DateTimeKind.Local).AddTicks(7393), 62, 248, "/treasury/payments/new", new Guid("d3752f7d-2959-4dd5-9397-a11c9eee7eea"), "NewPaymentForm" },
                    { 68, "", "search", null, new DateTime(2023, 10, 18, 17, 16, 5, 855, DateTimeKind.Local).AddTicks(7435), 62, 246, "/treasury/payments/by-no", new Guid("6639a977-815b-4892-a372-b7c99f9f7ab6"), "PaymentFormbyNo" },
                    { 61, "", "list", null, new DateTime(2023, 10, 18, 17, 16, 5, 855, DateTimeKind.Local).AddTicks(7361), 53, 239, "/treasury/source-apps", new Guid("a7eade80-2339-4018-a205-739f40e0b54a"), "SourceApps" },
                    { 60, "", "", null, new DateTime(2023, 10, 18, 17, 16, 5, 855, DateTimeKind.Local).AddTicks(7345), 55, 233, "/treasury/check-book-report", new Guid("a90d2444-aa84-4485-90f5-d5cb5f347f02"), "CheckBookReport" },
                    { 22, "Ctrl+Alt+B", "list", null, new DateTime(2023, 10, 18, 17, 16, 5, 855, DateTimeKind.Local).AddTicks(6839), 20, 147, "/finance/account-book", new Guid("bd99d6c5-bc89-4831-8ca0-a50c27fbfa6d"), "AccountBook" },
                    { 100007, "", "list", null, new DateTime(2023, 10, 18, 17, 16, 5, 855, DateTimeKind.Local).AddTicks(7541), 100001, 100022, "/product-scope/attributes", new Guid("f0d7d4da-c4c6-4d08-943b-b42268e8611e"), "ProductAttribute" }
                });

            migrationBuilder.InsertData(
                schema: "Metadata",
                table: "Command",
                columns: new[] { "CommandID", "HotKey", "IconName", "Index", "ModifiedDate", "ParentId", "PermissionId", "RouteUrl", "rowguid", "TitleKey" },
                values: new object[,]
                {
                    { 56, "", "", null, new DateTime(2023, 10, 18, 17, 16, 5, 855, DateTimeKind.Local).AddTicks(7305), 54, 215, "/treasury/check-books/new", new Guid("20e61906-931b-4699-9d22-0046bd39848e"), "NewCheckBook" },
                    { 58, "", "", null, new DateTime(2023, 10, 18, 17, 16, 5, 855, DateTimeKind.Local).AddTicks(7325), 54, 212, "/treasury/check-books/by-name", new Guid("a883159c-b3df-4364-b4c4-81f4441cbd17"), "CheckBookByName" },
                    { 51, "", "list", null, new DateTime(2023, 10, 18, 17, 16, 5, 855, DateTimeKind.Local).AddTicks(7146), 37, 210, "/tadbir/dashboard", new Guid("288a5167-9397-4577-9c57-bc77c9bffc45"), "ManageDashboard" },
                    { 50, "Ctrl+Shift+K", "list", null, new DateTime(2023, 10, 18, 17, 16, 5, 855, DateTimeKind.Local).AddTicks(7136), 39, 201, "/finance/bal-sheet", new Guid("d3f83245-feda-43ef-a3f1-904be8beefb3"), "BalanceSheet" },
                    { 49, "Ctrl+Alt+Q", "list", null, new DateTime(2023, 10, 18, 17, 16, 5, 855, DateTimeKind.Local).AddTicks(7124), 11, 189, "/finance/vouchers/last/draft", new Guid("c0e7718c-4c95-4bb1-a5e2-245615302f26"), "LastDraftVoucher" },
                    { 47, "Ctrl+Alt+V", "list", null, new DateTime(2023, 10, 18, 17, 16, 5, 855, DateTimeKind.Local).AddTicks(7101), 11, 180, "/finance/vouchers/new/draft", new Guid("dd512678-f355-4e04-b6ad-9afef8168200"), "NewDraftVoucher" },
                    { 48, "Ctrl+Alt+D", "list", null, new DateTime(2023, 10, 18, 17, 16, 5, 855, DateTimeKind.Local).AddTicks(7114), 11, 179, "/finance/vouchers/by-no/draft", new Guid("e3e8cfe7-f624-4e7b-a5fa-fd8f35694f58"), "DraftVoucherByNo" },
                    { 46, "Ctrl+Alt+R", "list", null, new DateTime(2023, 10, 18, 17, 16, 5, 855, DateTimeKind.Local).AddTicks(7089), 39, 175, "/finance/profit-loss", new Guid("ffc94557-eb12-45af-8f5f-ad53313e9da8"), "ProfitLoss" },
                    { 44, "Ctrl+Shift+B", "list", null, new DateTime(2023, 10, 18, 17, 16, 5, 855, DateTimeKind.Local).AddTicks(7064), 39, 169, "/finance/balance-by-account", new Guid("28d799f5-82dc-49e5-96a4-6f8237a7b822"), "BalanceByAccount" },
                    { 43, "Ctrl+Shift+I", "list", null, new DateTime(2023, 10, 18, 17, 16, 5, 855, DateTimeKind.Local).AddTicks(7052), 39, 164, "/finance/itembalance", new Guid("6dca5265-612a-4446-93c8-1cd68c35dfff"), "ItemBalance" },
                    { 41, "Ctrl+Alt+J", "list", null, new DateTime(2023, 10, 18, 17, 16, 5, 855, DateTimeKind.Local).AddTicks(7031), 20, 158, "/finance/currency-book", new Guid("0f459a15-0615-49b8-a7e4-540ddc5e30fa"), "CurrencyBook" },
                    { 40, "Ctrl+Alt+T", "list", null, new DateTime(2023, 10, 18, 17, 16, 5, 855, DateTimeKind.Local).AddTicks(7021), 39, 153, "/finance/balance", new Guid("912c214c-89e2-4442-87a0-bc05db4c7f25"), "TestBalance" },
                    { 57, "", "", null, new DateTime(2023, 10, 18, 17, 16, 5, 855, DateTimeKind.Local).AddTicks(7315), 54, 218, "/treasury/check-books/last", new Guid("03b98659-4c04-4df8-83f1-b48db1e1baeb"), "LastCheckBook" }
                });

            migrationBuilder.InsertData(
                schema: "Reporting",
                table: "LocalReport",
                columns: new[] { "LocalReportID", "Caption", "LocaleId", "ModifiedDate", "ReportId", "rowguid", "Template" },
                values: new object[,]
                {
                    { 100007, "گزارشات", 2, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(6708), 100003, new Guid("a9321e7a-6834-466f-892b-cf5c1a45e432"), "" },
                    { 100006, "Reports", 1, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(6698), 100003, new Guid("bcd8f327-aa95-4d9d-9a09-20ca272a9406"), "" },
                    { 100005, "اطلاعات عملیاتی", 2, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(6689), 100002, new Guid("152f3336-86a8-4076-a00f-ec076cdf72ae"), "" },
                    { 100004, "Operational Data", 1, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(6678), 100002, new Guid("c326bed5-f67e-4301-a37b-ca39bd19e566"), "" },
                    { 64, "Reports", 4, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(3330), 16, new Guid("ef90cd4d-594e-401f-b912-74ba9910496c"), "" },
                    { 61, "Reports", 1, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(3303), 16, new Guid("44d24c8c-0cb6-4ced-b4e4-d4f136eb3643"), "" },
                    { 60, "Operational data", 4, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(3293), 15, new Guid("be76110b-3d0c-4866-8946-662924fcf42c"), "" },
                    { 59, "Operational data", 3, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(3283), 15, new Guid("7c3bbd8a-ecdf-4268-8a89-10f76d17b240"), "" },
                    { 58, "اطلاعات عملیاتی", 2, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(3269), 15, new Guid("cd7816d6-ecd6-4a2f-b789-a9daf96381a1"), "" },
                    { 57, "Operational data", 1, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(3260), 15, new Guid("4c39b07e-3492-4670-9a5a-1a7e9f693a15"), "" },
                    { 56, "Base data", 4, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(3251), 14, new Guid("3abd2811-a73e-4044-b2fa-905ecee67ae8"), "" },
                    { 55, "Base data", 3, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(3240), 14, new Guid("068f1557-46a2-41b2-95f2-d6af286c7532"), "" },
                    { 54, "اطلاعات پایه", 2, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(3231), 14, new Guid("536c4aa1-2883-44c5-8984-e429c292f367"), "" },
                    { 53, "Base data", 1, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(3220), 14, new Guid("262b1ea2-c232-47e6-a070-ea3f645c94e9"), "" },
                    { 62, "گزارشات", 2, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(3313), 16, new Guid("03e172a3-3ce4-4489-a7f5-69e9f7361267"), "" },
                    { 16, "Reports", 4, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(2737), 4, new Guid("47e0efdd-d35e-48f9-9ea7-1c46cbad82f8"), "" },
                    { 14, "گزارشات", 2, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(2712), 4, new Guid("f78e0bee-85c2-436a-bad3-63bc6ac90589"), "" },
                    { 13, "Reports", 1, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(2701), 4, new Guid("3f65a5df-0625-481b-ad93-f8a84a4e8ce4"), "" },
                    { 12, "Operational data", 4, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(2692), 3, new Guid("9e9bb8e6-ff32-457c-b5a3-62649ecb33c0"), "" },
                    { 11, "Operational data", 3, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(2683), 3, new Guid("ea318962-e6e7-4691-ae9b-7bc338c866a5"), "" },
                    { 10, "اطلاعات عملیاتی", 2, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(2674), 3, new Guid("502285ed-1882-4842-9f5c-0ec3699c37d0"), "" },
                    { 9, "Operational data", 1, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(2662), 3, new Guid("fc41c57c-9003-4602-b3a0-44a0fa84299f"), "" },
                    { 8, "Base data", 4, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(2653), 2, new Guid("b2824b6d-b2cd-48a3-b1b4-6101ebb0dc9b"), "" },
                    { 7, "Base data", 3, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(2643), 2, new Guid("72482924-6a28-4ac9-9234-395bf1497a3f"), "" },
                    { 6, "اطلاعات پایه", 2, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(2612), 2, new Guid("c3fa458b-a380-4e5a-9af2-337bd28b7fcd"), "" },
                    { 15, "Reports", 3, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(2728), 4, new Guid("0b332f51-2121-4b8c-b661-708d4e6f9059"), "" },
                    { 63, "Reports", 3, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(3321), 16, new Guid("328da871-dd1a-4082-a145-54e9c8bc8b57"), "" },
                    { 5, "Base data", 1, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(2592), 2, new Guid("accd3677-1405-41f4-9d29-2cca7edb67d5"), "" },
                    { 282, "اطلاعات عملیاتی", 2, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(6262), 95, new Guid("641d4929-ae67-40b4-b614-fa0ce48a57be"), "" }
                });

            migrationBuilder.InsertData(
                schema: "Reporting",
                table: "LocalReport",
                columns: new[] { "LocalReportID", "Caption", "LocaleId", "ModifiedDate", "ReportId", "rowguid", "Template" },
                values: new object[,]
                {
                    { 281, "Operational Data", 1, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(6252), 95, new Guid("5382ebb1-bc08-4585-8924-ca39eb4ac205"), "" },
                    { 100002, "Base Data", 1, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(6661), 100001, new Guid("4de83ca2-112c-4537-b8ab-4868b1d7fa2a"), "" },
                    { 280, "اطلاعات پایه", 2, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(6242), 94, new Guid("00af2f54-00b4-4ae7-9d04-ad7ba5012dd5"), "" },
                    { 279, "Base Data", 1, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(6232), 94, new Guid("6ecbe361-2876-4fc0-9772-80f0d4634add"), "" },
                    { 172, "Design template", 4, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(4886), 43, new Guid("3c512872-aabd-4ff1-8e5a-dd096c8fbdbd"), "" },
                    { 100003, "اطلاعات پایه", 2, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(6670), 100001, new Guid("2d591a29-0437-4fca-9382-612bd6e7b75a"), "" },
                    { 283, "Reports", 1, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(6271), 96, new Guid("df131552-7d94-4898-ad51-2769e166ab53"), "" },
                    { 171, "Design template", 3, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(4876), 43, new Guid("72b809e9-7e39-422b-b944-6193d6a972ac"), "" },
                    { 284, "گزارشات", 2, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(6280), 96, new Guid("d106c4e1-42b1-4695-ae61-588bf86da415"), "" },
                    { 169, "Design template", 1, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(4858), 43, new Guid("bd8dde79-807e-4ef1-bfbe-afebe162fd27"), "" },
                    { 170, "طراحی قالب", 2, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(4867), 43, new Guid("0395fbe5-9629-407c-865e-5b567d2ca046"), "" }
                });

            migrationBuilder.InsertData(
                schema: "Reporting",
                table: "Report",
                columns: new[] { "ReportID", "Code", "CreatedById", "IsDefault", "IsDynamic", "IsGroup", "IsSystem", "ModifiedDate", "ParentId", "ReportViewId", "ResourceKeys", "ServiceUrl", "SubsystemId", "ViewId" },
                values: new object[,]
                {
                    { 100006, "ProductScope-Report-QReport", 1, false, false, true, true, new DateTime(2023, 10, 18, 17, 16, 5, 915, DateTimeKind.Local).AddTicks(6950), 100003, null, "", "", 100000, null },
                    { 100007, "", 1, true, true, false, true, new DateTime(2023, 10, 18, 17, 16, 5, 915, DateTimeKind.Local).AddTicks(6960), 100001, null, "", "brands", 100000, 100001 },
                    { 5, "Admin-Base-QReport", 1, false, false, true, true, new DateTime(2023, 10, 18, 17, 16, 5, 915, DateTimeKind.Local).AddTicks(4823), 2, null, "", "", 1, null },
                    { 100008, "", 1, true, true, false, true, new DateTime(2023, 10, 18, 17, 16, 5, 915, DateTimeKind.Local).AddTicks(6969), 100001, null, "", "units", 3, 100002 },
                    { 6, "Admin-Operation-QReport", 1, false, false, true, true, new DateTime(2023, 10, 18, 17, 16, 5, 915, DateTimeKind.Local).AddTicks(4856), 3, null, "", "", 1, null },
                    { 100005, "ProductScope-Operation-QReport", 1, false, false, true, true, new DateTime(2023, 10, 18, 17, 16, 5, 915, DateTimeKind.Local).AddTicks(6941), 100002, null, "", "", 100000, null },
                    { 100009, "", 1, true, true, false, true, new DateTime(2023, 10, 18, 17, 16, 5, 915, DateTimeKind.Local).AddTicks(6978), 100001, null, "", "", 3, 100003 },
                    { 100, "", 1, true, true, false, true, new DateTime(2023, 10, 18, 17, 16, 5, 915, DateTimeKind.Local).AddTicks(6795), 96, null, "", "check-books", 3, 69 },
                    { 99, "Treasury-Report-QReport", 1, false, false, true, true, new DateTime(2023, 10, 18, 17, 16, 5, 915, DateTimeKind.Local).AddTicks(6785), 96, null, "", "", 3, null },
                    { 100004, "ProductScope-Base-QReport", 1, false, false, true, true, new DateTime(2023, 10, 18, 17, 16, 5, 915, DateTimeKind.Local).AddTicks(6932), 100001, null, "", "", 100000, null },
                    { 89, "Voucher-Summary-By-Date", 1, false, false, false, true, new DateTime(2023, 10, 18, 17, 16, 5, 915, DateTimeKind.Local).AddTicks(6502), 16, null, "", "reports/finance/vouchers/sum-by-date", 2, 2 },
                    { 98, "Treasury-Operation-QReport", 1, false, false, true, true, new DateTime(2023, 10, 18, 17, 16, 5, 915, DateTimeKind.Local).AddTicks(6587), 95, null, "", "", 3, null },
                    { 90, "Voucher-Summary-By-No", 1, false, false, false, true, new DateTime(2023, 10, 18, 17, 16, 5, 915, DateTimeKind.Local).AddTicks(6513), 16, null, "", "reports/finance/vouchers/sum-by-no", 2, 2 },
                    { 17, "Accnt-Base-QReport", 1, false, false, true, true, new DateTime(2023, 10, 18, 17, 16, 5, 915, DateTimeKind.Local).AddTicks(5025), 14, null, "", "", 2, null },
                    { 97, "Treasury-Base-QReport", 1, false, false, true, true, new DateTime(2023, 10, 18, 17, 16, 5, 915, DateTimeKind.Local).AddTicks(6577), 94, null, "", "", 3, null },
                    { 18, "Accnt-Operation-QReport", 1, false, false, true, true, new DateTime(2023, 10, 18, 17, 16, 5, 915, DateTimeKind.Local).AddTicks(5039), 15, null, "", "", 2, null },
                    { 20, "Voucher-Printing", 1, false, false, true, true, new DateTime(2023, 10, 18, 17, 16, 5, 915, DateTimeKind.Local).AddTicks(5079), 15, null, "", "", 2, null },
                    { 100011, "", 1, true, true, false, true, new DateTime(2023, 10, 18, 17, 16, 5, 915, DateTimeKind.Local).AddTicks(6997), 100001, null, "", "Files", 3, 100005 },
                    { 19, "Accnt-Report-QReport", 1, false, false, true, true, new DateTime(2023, 10, 18, 17, 16, 5, 915, DateTimeKind.Local).AddTicks(5062), 16, null, "", "", 2, null },
                    { 85, "Journal-ByDate-ByLedger", 1, false, true, false, true, new DateTime(2023, 10, 18, 17, 16, 5, 915, DateTimeKind.Local).AddTicks(6464), 16, null, "", "reports/journal/by-date/by-ledger", 2, 17 },
                    { 86, "Journal-ByDate-BySubsidiary", 1, false, true, false, true, new DateTime(2023, 10, 18, 17, 16, 5, 915, DateTimeKind.Local).AddTicks(6474), 16, null, "", "reports/journal/by-date/by-subsid", 2, 18 },
                    { 87, "Journal-ByNo-ByLedger", 1, false, true, false, true, new DateTime(2023, 10, 18, 17, 16, 5, 915, DateTimeKind.Local).AddTicks(6484), 16, null, "", "reports/journal/by-no/by-ledger", 2, 24 },
                    { 88, "Journal-ByNo-BySubsidiary", 1, false, true, false, true, new DateTime(2023, 10, 18, 17, 16, 5, 915, DateTimeKind.Local).AddTicks(6493), 16, null, "", "reports/journal/by-no/by-subsid", 2, 25 },
                    { 7, "Admin-Report-QReport", 1, false, false, true, true, new DateTime(2023, 10, 18, 17, 16, 5, 915, DateTimeKind.Local).AddTicks(4875), 4, null, "", "", 1, null },
                    { 100010, "", 1, true, true, false, true, new DateTime(2023, 10, 18, 17, 16, 5, 915, DateTimeKind.Local).AddTicks(6987), 100001, null, "", "attributes", 3, 100004 }
                });

            migrationBuilder.InsertData(
                schema: "Reporting",
                table: "LocalReport",
                columns: new[] { "LocalReportID", "Caption", "LocaleId", "ModifiedDate", "ReportId", "rowguid", "Template" },
                values: new object[,]
                {
                    { 17, "Quick Report", 1, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(2746), 5, new Guid("8c15693a-e6e5-48a2-aed5-5a0918ae5546"), "" },
                    { 285, "Quick Report", 1, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(6290), 97, new Guid("14706a00-3ab2-4d99-96b7-a0fba3d933e5"), "" },
                    { 272, "خلاصه اسناد حسابداری - بر اساس شماره سند", 2, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(6181), 90, new Guid("7981fa4b-0e39-4ee3-b067-5e6c1e9cdd4e"), "" },
                    { 271, "Accounting Voucher Summary - By Voucher No", 1, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(6166), 90, new Guid("885bebb2-e6df-4dde-b07c-ec22fa901a0e"), "" },
                    { 270, "خلاصه اسناد حسابداری - بر اساس تاریخ", 2, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(6158), 89, new Guid("fe12794f-c869-49b3-a7d8-e23142de1d33"), "" },
                    { 269, "Accounting Voucher Summary - By Date", 1, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(6148), 89, new Guid("c656aa19-6856-4eaf-bad6-d862ea5f410b"), "" },
                    { 268, "دفتر روزنامه در سطح معین - بر اساس شماره سند", 2, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(6134), 88, new Guid("b1825e48-782d-4f45-a16a-05a3d565143d"), "" },
                    { 267, "Journal in Subsidiary Level - By Number", 1, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(6124), 88, new Guid("2abf5cb1-aae3-49aa-b0e7-4b554a3a173c"), "" },
                    { 266, "دفتر روزنامه در سطح کل - بر اساس شماره سند", 2, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(6115), 87, new Guid("14c97eb5-8625-4c70-8358-4fecc579a4ca"), "" },
                    { 265, "Journal in Ledger Level - By Number", 1, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(6106), 87, new Guid("9188220a-d38c-4566-8d59-73df496b2563"), "" },
                    { 264, "دفتر روزنامه در سطح معین - بر اساس تاریخ", 2, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(6096), 86, new Guid("30b493b0-6e3f-4658-8cd1-6ffea058f32d"), "" },
                    { 263, "Journal in Subsidiary Level - By Date", 1, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(5992), 86, new Guid("14a777a0-7fe8-4a92-aa5c-4bac90ee5385"), "" },
                    { 262, "دفتر روزنامه در سطح کل - بر اساس تاریخ", 2, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(5983), 85, new Guid("25b15c69-2f70-4fc5-95c3-c37e699225d2"), "" },
                    { 261, "Journal in Ledger Level - By Date", 1, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(5974), 85, new Guid("030fbc88-e184-47d3-897b-1aa5cdd607bf"), "" },
                    { 100012, "Quick Report", 1, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(6759), 100006, new Guid("ac20e75c-1aa4-44b8-8266-ab707ee84ff6"), "" },
                    { 76, "Quick Report", 4, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(3553), 19, new Guid("f10faa22-0c06-453a-81d4-e04d28743f1f"), "" },
                    { 75, "Quick Report", 3, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(3541), 19, new Guid("16f4edc3-1da2-4560-836f-ba5d67e59c3a"), "" },
                    { 74, "گزارش فوری", 2, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(3527), 19, new Guid("09295f4b-d5d9-4b00-9c1d-eab3e36a70b3"), "" },
                    { 286, "گزارش فوری", 2, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(6391), 97, new Guid("fdaba257-2889-487d-84c9-361f6edb1a2e"), "" },
                    { 73, "Quick Report", 1, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(3518), 19, new Guid("b13ef985-ff93-457f-87cc-fa8fab548a10"), "" },
                    { 287, "Quick Report", 1, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(6405), 98, new Guid("59b4ae76-89aa-46ab-86fb-069551dc0241"), "" },
                    { 289, "Quick Report", 1, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(6423), 99, new Guid("6c37ba27-7359-4e9a-88f1-2660954cbe89"), "" },
                    { 100011, "گزارش فوری", 2, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(6750), 100005, new Guid("99dd8aa3-10a9-491f-9745-a9b6288245dd"), "" },
                    { 100010, "Quick Report", 1, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(6741), 100005, new Guid("ab8fbb33-17d6-4f66-96ff-f9e9efef821e"), "" },
                    { 100023, "فهرست فایل ها", 2, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(6962), 100011, new Guid("90c28c20-c591-4abc-ba0d-a0f4841e63d1"), "" },
                    { 100022, "File List", 1, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(6953), 100011, new Guid("5a04d5f2-922b-48d3-838e-b7aed4678c5a"), "" },
                    { 100021, "لیست خصوصیت ها", 2, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(6944), 100010, new Guid("0a2dc833-b3bf-4aaa-9b24-945f6ef5b2f9"), "" },
                    { 100020, "Attribute List", 1, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(6935), 100010, new Guid("59f01587-d5d4-484d-9c1b-12f7a0b18f50"), "" },
                    { 100019, "لیست ویژگی ها", 2, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(6926), 100009, new Guid("3d956233-9c5e-4f96-8239-5fb3d2043838"), "" },
                    { 100018, "Properties List", 1, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(6918), 100009, new Guid("45933822-ec52-4b8f-9542-5bba7e5a2b21"), "" },
                    { 100017, "لیست واحدها", 2, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(6909), 100008, new Guid("1fc28560-8ef2-47dd-a8bf-942e128c8e45"), "" },
                    { 100016, "Unit list", 1, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(6898), 100008, new Guid("b40f78b8-9eae-4fa3-84b2-b57c2ecaa08f"), "" },
                    { 100015, "فهرست برندها", 2, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(6785), 100007, new Guid("b233f3fb-4e28-4300-bcc1-75ee0ad3013e"), "" },
                    { 100014, "Brnads List", 1, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(6776), 100007, new Guid("3a2366e4-2af4-4e45-bfde-a001e0290542"), "" },
                    { 100009, "گزارش فوری", 2, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(6732), 100004, new Guid("0f65545b-24c0-4440-b6f1-c8fbcaa6bd7e"), "" },
                    { 100008, "Quick Report", 1, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(6723), 100004, new Guid("69b5b4ea-f419-489a-9e00-bcfa706e2f3b"), "" },
                    { 292, "دسته چک", 2, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(6453), 100, new Guid("8996d863-5a93-4c4d-bb7b-0795afa08f15"), "" },
                    { 291, "Check Book", 1, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(6443), 100, new Guid("896747ef-86bf-425b-8cf8-1502246c7f34"), "" },
                    { 290, "گزارش فوری", 2, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(6434), 99, new Guid("8771fa9f-3bb3-4445-a1b4-4abec229bfca"), "" },
                    { 288, "گزارش فوری", 2, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(6414), 98, new Guid("9329b067-dae4-4355-8b6b-b5458aa444eb"), "" },
                    { 80, "Voucher Printing", 4, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(3593), 20, new Guid("f4826e15-dfaf-431b-aea4-5079e8927c98"), "" },
                    { 100013, "گزارش فوری", 2, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(6767), 100006, new Guid("0addacf4-852a-4798-a77e-bacc4a490dee"), "" }
                });

            migrationBuilder.InsertData(
                schema: "Reporting",
                table: "LocalReport",
                columns: new[] { "LocalReportID", "Caption", "LocaleId", "ModifiedDate", "ReportId", "rowguid", "Template" },
                values: new object[,]
                {
                    { 72, "Quick Report", 4, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(3509), 18, new Guid("2b63f089-e508-46f6-a7bf-a02d5d48339f"), "" },
                    { 25, "Quick Report", 1, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(2977), 7, new Guid("7a23065f-3d76-468f-9b26-3e15511d38b7"), "" },
                    { 71, "Quick Report", 3, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(3499), 18, new Guid("ba4a5480-7874-444f-836f-b0708553f8f2"), "" },
                    { 67, "Quick Report", 3, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(3461), 17, new Guid("282b1b5d-db89-4a40-85aa-bcb9e7aba9ba"), "" },
                    { 28, "Quick Report", 4, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(3004), 7, new Guid("a0904454-a2a4-497a-86a7-216185e13561"), "" },
                    { 65, "Quick Report", 1, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(3437), 17, new Guid("b5268204-f118-4af4-a566-3a5065809f52"), "" },
                    { 24, "Quick Report", 4, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(2968), 6, new Guid("747c4bd0-e1aa-4edd-b626-cb12b0da5038"), "" },
                    { 23, "Quick Report", 3, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(2958), 6, new Guid("50ca8aad-62d6-4f44-91c5-86bbac4f2a4b"), "" },
                    { 22, "گزارش فوری", 2, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(2794), 6, new Guid("67fcc302-f0c6-466b-8260-ed62fd214172"), "" },
                    { 27, "Quick Report", 3, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(2995), 7, new Guid("1f293fbe-0c4e-47db-8c54-b6b52f3fe1b8"), "" },
                    { 21, "Quick Report", 1, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(2785), 6, new Guid("193d6dcd-a905-4c7a-bba1-fa3bfcec2d51"), "" },
                    { 77, "Voucher Printing", 1, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(3563), 20, new Guid("e3814d69-7bfd-4f05-89fe-1c11766bfadc"), "" },
                    { 78, "چاپ سند", 2, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(3573), 20, new Guid("ca3dafe9-bba5-4a2a-a48c-660bb13fb59e"), "" },
                    { 69, "Quick Report", 1, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(3478), 18, new Guid("faa1aea2-8093-4164-8766-cac301adcd69"), "" },
                    { 70, "گزارش فوری", 2, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(3490), 18, new Guid("573ad28d-22da-42eb-8b9b-5db03b45f162"), "" },
                    { 66, "گزارش فوری", 2, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(3446), 17, new Guid("c1c8ea5c-969b-4e1a-913f-c42080ebb29b"), "" },
                    { 79, "Voucher Printing", 3, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(3583), 20, new Guid("50105243-9d1c-4475-84f8-7967f893398f"), "" },
                    { 20, "Quick Report", 4, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(2776), 5, new Guid("b8defd31-9442-40ef-991b-78ecfdd2185e"), "" },
                    { 19, "Quick Report", 3, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(2767), 5, new Guid("aea99099-02cb-41d7-bc77-a6ef21a03272"), "" },
                    { 18, "گزارش فوری", 2, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(2758), 5, new Guid("a3e2618d-63cc-493f-958e-36c4ab62389c"), "" },
                    { 68, "Quick Report", 4, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(3470), 17, new Guid("1fce5958-24b7-4f69-9f49-123418d3f8b7"), "" },
                    { 26, "گزارش فوری", 2, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(2986), 7, new Guid("2b77c691-195a-48c4-a840-70464d152e42"), "" }
                });

            migrationBuilder.InsertData(
                schema: "Reporting",
                table: "Report",
                columns: new[] { "ReportID", "Code", "CreatedById", "IsDefault", "IsDynamic", "IsGroup", "IsSystem", "ModifiedDate", "ParentId", "ReportViewId", "ResourceKeys", "ServiceUrl", "SubsystemId", "ViewId" },
                values: new object[,]
                {
                    { 40, "Voucher-Std-Form", 1, true, false, false, true, new DateTime(2023, 10, 18, 17, 16, 5, 915, DateTimeKind.Local).AddTicks(5827), 20, null, "", "reports/finance/voucher-by-no/{0}/std-form", 2, 2 },
                    { 21, "Fiscal-Periods", 1, true, true, false, true, new DateTime(2023, 10, 18, 17, 16, 5, 915, DateTimeKind.Local).AddTicks(5094), 17, null, "", "fperiods", 2, 9 },
                    { 74, "", 1, true, true, false, true, new DateTime(2023, 10, 18, 17, 16, 5, 915, DateTimeKind.Local).AddTicks(6355), 17, null, "", "currencies", 2, 30 },
                    { 23, "Detail-Accounts", 1, true, true, false, true, new DateTime(2023, 10, 18, 17, 16, 5, 915, DateTimeKind.Local).AddTicks(5124), 17, null, "", "faccounts", 2, 6 },
                    { 24, "Cost-Centers", 1, true, true, false, true, new DateTime(2023, 10, 18, 17, 16, 5, 915, DateTimeKind.Local).AddTicks(5406), 17, null, "", "ccenters", 2, 7 },
                    { 25, "Projects", 1, true, true, false, true, new DateTime(2023, 10, 18, 17, 16, 5, 915, DateTimeKind.Local).AddTicks(5431), 17, null, "", "projects", 2, 8 },
                    { 26, "Account-Groups", 1, true, true, false, true, new DateTime(2023, 10, 18, 17, 16, 5, 915, DateTimeKind.Local).AddTicks(5451), 17, null, "", "accgroups", 2, 12 },
                    { 80, "BalanceSheet", 1, true, true, false, true, new DateTime(2023, 10, 18, 17, 16, 5, 915, DateTimeKind.Local).AddTicks(6415), 19, null, "", "bal-sheet", 2, 67 },
                    { 22, "Accounts", 1, true, true, false, true, new DateTime(2023, 10, 18, 17, 16, 5, 915, DateTimeKind.Local).AddTicks(5108), 17, null, "", "accounts", 2, 1 },
                    { 92, "", 1, false, true, false, true, new DateTime(2023, 10, 18, 17, 16, 5, 915, DateTimeKind.Local).AddTicks(6531), 7, null, "", "dashboard/widgets/all", 1, 68 },
                    { 106, "", 1, true, true, false, true, new DateTime(2023, 10, 18, 17, 16, 5, 915, DateTimeKind.Local).AddTicks(6855), 98, null, "", "payments/{0}/cash/articles", 3, 74 },
                    { 103, "", 1, true, true, false, true, new DateTime(2023, 10, 18, 17, 16, 5, 915, DateTimeKind.Local).AddTicks(6825), 97, null, "", "source-apps", 3, 73 },
                    { 8, "Companies", 1, true, true, false, true, new DateTime(2023, 10, 18, 17, 16, 5, 915, DateTimeKind.Local).AddTicks(4894), 5, null, "", "companies", 1, 11 },
                    { 9, "Branches", 1, true, true, false, true, new DateTime(2023, 10, 18, 17, 16, 5, 915, DateTimeKind.Local).AddTicks(4913), 5, null, "", "branches", 1, 10 },
                    { 10, "Users", 1, true, true, false, true, new DateTime(2023, 10, 18, 17, 16, 5, 915, DateTimeKind.Local).AddTicks(4930), 5, null, "", "users", 1, 4 },
                    { 11, "Roles", 1, true, true, false, true, new DateTime(2023, 10, 18, 17, 16, 5, 915, DateTimeKind.Local).AddTicks(4944), 5, null, "", "roles", 1, 5 },
                    { 70, "", 1, true, true, false, true, new DateTime(2023, 10, 18, 17, 16, 5, 915, DateTimeKind.Local).AddTicks(6216), 6, null, "", "oplog/archive", 1, 61 },
                    { 71, "", 1, true, true, false, true, new DateTime(2023, 10, 18, 17, 16, 5, 915, DateTimeKind.Local).AddTicks(6226), 6, null, "", "sys-oplog", 1, 59 },
                    { 101, "", 1, true, true, false, true, new DateTime(2023, 10, 18, 17, 16, 5, 915, DateTimeKind.Local).AddTicks(6804), 97, null, "", "cash-registers", 3, 70 },
                    { 72, "", 1, true, true, false, true, new DateTime(2023, 10, 18, 17, 16, 5, 915, DateTimeKind.Local).AddTicks(6236), 6, null, "", "sys-oplog/archive", 1, 60 }
                });

            migrationBuilder.InsertData(
                schema: "Reporting",
                table: "Report",
                columns: new[] { "ReportID", "Code", "CreatedById", "IsDefault", "IsDynamic", "IsGroup", "IsSystem", "ModifiedDate", "ParentId", "ReportViewId", "ResourceKeys", "ServiceUrl", "SubsystemId", "ViewId" },
                values: new object[,]
                {
                    { 102, "", 1, true, true, false, true, new DateTime(2023, 10, 18, 17, 16, 5, 915, DateTimeKind.Local).AddTicks(6814), 99, null, "", "check-book-report", 3, 72 },
                    { 107, "", 1, true, true, false, true, new DateTime(2023, 10, 18, 17, 16, 5, 915, DateTimeKind.Local).AddTicks(6866), 98, null, "", "receipts/{0}/cash/articles", 3, 75 },
                    { 79, "", 1, true, true, false, true, new DateTime(2023, 10, 18, 17, 16, 5, 915, DateTimeKind.Local).AddTicks(6406), 19, null, "", "", 2, 66 },
                    { 105, "", 1, true, true, false, true, new DateTime(2023, 10, 18, 17, 16, 5, 915, DateTimeKind.Local).AddTicks(6845), 98, null, "", "receipts/{0}/payer/articles", 3, 75 },
                    { 104, "", 1, true, true, false, true, new DateTime(2023, 10, 18, 17, 16, 5, 915, DateTimeKind.Local).AddTicks(6834), 98, null, "", "payments/{0}/receiver/articles", 3, 74 },
                    { 91, "", 1, false, true, false, true, new DateTime(2023, 10, 18, 17, 16, 5, 915, DateTimeKind.Local).AddTicks(6522), 7, null, "", "dashboard/widgets", 1, 68 },
                    { 73, "", 1, true, true, false, true, new DateTime(2023, 10, 18, 17, 16, 5, 915, DateTimeKind.Local).AddTicks(6343), 6, null, "", "oplog", 1, 13 },
                    { 78, "", 1, true, true, false, true, new DateTime(2023, 10, 18, 17, 16, 5, 915, DateTimeKind.Local).AddTicks(6397), 19, null, "", "", 2, 65 },
                    { 69, "", 1, true, true, false, true, new DateTime(2023, 10, 18, 17, 16, 5, 915, DateTimeKind.Local).AddTicks(6202), 19, null, "", "", 2, 58 },
                    { 76, "", 1, true, true, false, true, new DateTime(2023, 10, 18, 17, 16, 5, 915, DateTimeKind.Local).AddTicks(6378), 19, null, "", "", 2, 62 },
                    { 37, "Journal-ByNo-BySubsidiary", 1, true, true, false, true, new DateTime(2023, 10, 18, 17, 16, 5, 915, DateTimeKind.Local).AddTicks(5808), 19, null, "", "reports/journal/by-no/by-subsid", 2, 25 },
                    { 36, "Journal-ByNo-ByLedger", 1, true, true, false, true, new DateTime(2023, 10, 18, 17, 16, 5, 915, DateTimeKind.Local).AddTicks(5798), 19, null, "", "reports/journal/by-no/by-ledger", 2, 24 },
                    { 35, "Journal-ByNo-ByRow-Detail", 1, true, true, false, true, new DateTime(2023, 10, 18, 17, 16, 5, 915, DateTimeKind.Local).AddTicks(5787), 19, null, "", "reports/journal/by-no/by-row-detail", 2, 23 },
                    { 34, "Journal-ByNo-ByRow", 1, true, true, false, true, new DateTime(2023, 10, 18, 17, 16, 5, 915, DateTimeKind.Local).AddTicks(5770), 19, null, "", "reports/journal/by-no/by-row", 2, 22 },
                    { 33, "Journal-ByDate-LedgerSummary-ByMonth", 1, true, true, false, true, new DateTime(2023, 10, 18, 17, 16, 5, 915, DateTimeKind.Local).AddTicks(5760), 19, null, "", "reports/journal/by-date/sum-by-month", 2, 21 },
                    { 32, "Journal-ByDate-LedgerSummary-ByDate", 1, true, true, false, true, new DateTime(2023, 10, 18, 17, 16, 5, 915, DateTimeKind.Local).AddTicks(5751), 19, null, "", "reports/journal/by-date/sum-by-date", 2, 20 },
                    { 31, "Journal-ByDate-LedgerSummary", 1, true, true, false, true, new DateTime(2023, 10, 18, 17, 16, 5, 915, DateTimeKind.Local).AddTicks(5741), 19, null, "", "reports/journal/by-date/summary", 2, 19 },
                    { 30, "Journal-ByDate-BySubsidiary", 1, true, true, false, true, new DateTime(2023, 10, 18, 17, 16, 5, 915, DateTimeKind.Local).AddTicks(5732), 19, null, "", "reports/journal/by-date/by-subsid", 2, 18 },
                    { 29, "Journal-ByDate-ByLedger", 1, true, true, false, true, new DateTime(2023, 10, 18, 17, 16, 5, 915, DateTimeKind.Local).AddTicks(5714), 19, null, "", "reports/journal/by-date/by-ledger", 2, 17 },
                    { 28, "Journal-ByDate-ByRow-Detail", 1, true, true, false, true, new DateTime(2023, 10, 18, 17, 16, 5, 915, DateTimeKind.Local).AddTicks(5697), 19, null, "", "reports/journal/by-date/by-row-detail", 2, 16 },
                    { 27, "Journal-ByDate-ByRow", 1, true, true, false, true, new DateTime(2023, 10, 18, 17, 16, 5, 915, DateTimeKind.Local).AddTicks(5466), 19, null, "", "reports/journal/by-date/by-row", 2, 15 },
                    { 75, "", 1, true, true, false, true, new DateTime(2023, 10, 18, 17, 16, 5, 915, DateTimeKind.Local).AddTicks(6366), 18, null, "", "", 2, 31 },
                    { 109, "", 1, true, true, false, true, new DateTime(2023, 10, 18, 17, 16, 5, 915, DateTimeKind.Local).AddTicks(6884), 20, null, "", "", 2, 41 },
                    { 108, "", 1, true, true, false, true, new DateTime(2023, 10, 18, 17, 16, 5, 915, DateTimeKind.Local).AddTicks(6875), 20, null, "", "", 2, 42 },
                    { 84, "", 1, false, false, false, true, new DateTime(2023, 10, 18, 17, 16, 5, 915, DateTimeKind.Local).AddTicks(6455), 20, null, "", "reports/finance/voucher-by-no/{0}/by-subsid", 2, 2 },
                    { 83, "", 1, false, false, false, true, new DateTime(2023, 10, 18, 17, 16, 5, 915, DateTimeKind.Local).AddTicks(6446), 20, null, "", "reports/finance/voucher-by-no/{0}/by-ledger", 2, 2 },
                    { 82, "", 1, false, false, false, true, new DateTime(2023, 10, 18, 17, 16, 5, 915, DateTimeKind.Local).AddTicks(6433), 20, null, "", "reports/finance/voucher-by-no/{0}/by-detail", 2, 2 },
                    { 81, "Vouchers", 1, true, true, false, true, new DateTime(2023, 10, 18, 17, 16, 5, 915, DateTimeKind.Local).AddTicks(6424), 20, null, "", "", 2, 2 },
                    { 41, "Voucher-Std-Form-Detail", 1, false, false, false, true, new DateTime(2023, 10, 18, 17, 16, 5, 915, DateTimeKind.Local).AddTicks(5837), 20, null, "", "reports/finance/voucher-by-no/{0}/std-form-detail", 2, 2 },
                    { 38, "Journal-ByNo-LedgerSummary", 1, true, true, false, true, new DateTime(2023, 10, 18, 17, 16, 5, 915, DateTimeKind.Local).AddTicks(5818), 19, null, "", "reports/journal/by-no/summary", 2, 26 },
                    { 77, "", 1, true, true, false, true, new DateTime(2023, 10, 18, 17, 16, 5, 915, DateTimeKind.Local).AddTicks(6387), 19, null, "", "", 2, 64 },
                    { 46, "TestBalance2Column", 1, true, true, false, true, new DateTime(2023, 10, 18, 17, 16, 5, 915, DateTimeKind.Local).AddTicks(5866), 19, null, "", "", 2, 32 },
                    { 49, "TestBalance8Column", 1, true, true, false, true, new DateTime(2023, 10, 18, 17, 16, 5, 915, DateTimeKind.Local).AddTicks(5895), 19, null, "", "", 2, 35 },
                    { 68, "", 1, true, true, false, true, new DateTime(2023, 10, 18, 17, 16, 5, 915, DateTimeKind.Local).AddTicks(6190), 19, null, "", "", 2, 40 },
                    { 67, "", 1, true, true, false, true, new DateTime(2023, 10, 18, 17, 16, 5, 915, DateTimeKind.Local).AddTicks(6181), 19, null, "", "", 2, 39 },
                    { 66, "", 1, true, true, false, true, new DateTime(2023, 10, 18, 17, 16, 5, 915, DateTimeKind.Local).AddTicks(6172), 19, null, "", "", 2, 38 },
                    { 65, "", 1, true, true, false, true, new DateTime(2023, 10, 18, 17, 16, 5, 915, DateTimeKind.Local).AddTicks(6163), 19, null, "", "", 2, 37 },
                    { 64, "", 1, true, true, false, true, new DateTime(2023, 10, 18, 17, 16, 5, 915, DateTimeKind.Local).AddTicks(6153), 19, null, "", "", 2, 29 },
                    { 63, "", 1, true, true, false, true, new DateTime(2023, 10, 18, 17, 16, 5, 915, DateTimeKind.Local).AddTicks(6143), 19, null, "", "", 2, 28 },
                    { 62, "", 1, true, true, false, true, new DateTime(2023, 10, 18, 17, 16, 5, 915, DateTimeKind.Local).AddTicks(6134), 19, null, "", "", 2, 27 },
                    { 61, "", 1, true, true, false, true, new DateTime(2023, 10, 18, 17, 16, 5, 915, DateTimeKind.Local).AddTicks(6124), 19, null, "", "", 2, 56 },
                    { 60, "", 1, true, true, false, true, new DateTime(2023, 10, 18, 17, 16, 5, 915, DateTimeKind.Local).AddTicks(6114), 19, null, "", "", 2, 55 }
                });

            migrationBuilder.InsertData(
                schema: "Reporting",
                table: "Report",
                columns: new[] { "ReportID", "Code", "CreatedById", "IsDefault", "IsDynamic", "IsGroup", "IsSystem", "ModifiedDate", "ParentId", "ReportViewId", "ResourceKeys", "ServiceUrl", "SubsystemId", "ViewId" },
                values: new object[,]
                {
                    { 59, "", 1, true, true, false, true, new DateTime(2023, 10, 18, 17, 16, 5, 915, DateTimeKind.Local).AddTicks(6105), 19, null, "", "", 2, 54 },
                    { 58, "", 1, true, true, false, true, new DateTime(2023, 10, 18, 17, 16, 5, 915, DateTimeKind.Local).AddTicks(6096), 19, null, "", "", 2, 53 },
                    { 57, "", 1, true, true, false, true, new DateTime(2023, 10, 18, 17, 16, 5, 915, DateTimeKind.Local).AddTicks(6087), 19, null, "", "", 2, 51 },
                    { 56, "", 1, true, true, false, true, new DateTime(2023, 10, 18, 17, 16, 5, 915, DateTimeKind.Local).AddTicks(6077), 19, null, "", "", 2, 50 },
                    { 55, "", 1, true, true, false, true, new DateTime(2023, 10, 18, 17, 16, 5, 915, DateTimeKind.Local).AddTicks(6068), 19, null, "", "", 2, 49 },
                    { 54, "", 1, true, true, false, true, new DateTime(2023, 10, 18, 17, 16, 5, 915, DateTimeKind.Local).AddTicks(6056), 19, null, "", "", 2, 48 },
                    { 53, "", 1, true, true, false, true, new DateTime(2023, 10, 18, 17, 16, 5, 915, DateTimeKind.Local).AddTicks(6047), 19, null, "", "", 2, 46 },
                    { 52, "", 1, true, true, false, true, new DateTime(2023, 10, 18, 17, 16, 5, 915, DateTimeKind.Local).AddTicks(6037), 19, null, "", "", 2, 45 },
                    { 51, "", 1, true, true, false, true, new DateTime(2023, 10, 18, 17, 16, 5, 915, DateTimeKind.Local).AddTicks(6024), 19, null, "", "", 2, 44 },
                    { 50, "", 1, true, true, false, true, new DateTime(2023, 10, 18, 17, 16, 5, 915, DateTimeKind.Local).AddTicks(5905), 19, null, "", "", 2, 43 },
                    { 47, "TestBalance4Column", 1, true, true, false, true, new DateTime(2023, 10, 18, 17, 16, 5, 915, DateTimeKind.Local).AddTicks(5875), 19, null, "", "", 2, 33 },
                    { 48, "TestBalance6Column", 1, true, true, false, true, new DateTime(2023, 10, 18, 17, 16, 5, 915, DateTimeKind.Local).AddTicks(5886), 19, null, "", "", 2, 34 }
                });

            migrationBuilder.InsertData(
                schema: "Reporting",
                table: "LocalReport",
                columns: new[] { "LocalReportID", "Caption", "LocaleId", "ModifiedDate", "ReportId", "rowguid", "Template" },
                values: new object[,]
                {
                    { 29, "Companies", 1, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(3015), 8, new Guid("95740604-b9a2-466f-a1fb-e24aed3acfda"), "" },
                    { 176, "Test balance 2 columns", 4, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(5022), 46, new Guid("2e2b6980-9558-49c8-9921-97121da6eea1"), "" },
                    { 177, "Test balance 4 columns", 1, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(5032), 47, new Guid("a5f37946-d152-431f-8021-d5446e181fd4"), "" },
                    { 178, "تراز آزمایشی ۴ ستونی", 2, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(5040), 47, new Guid("6df29f93-7c79-4c2a-b0c5-4913ec7e830e"), "" },
                    { 179, "Test balance 4 columns", 3, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(5049), 47, new Guid("4b32ec2e-6aff-4e45-9b11-7561afb00151"), "" },
                    { 180, "Test balance 4 columns", 4, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(5058), 47, new Guid("374e6c42-448f-47f5-ba82-12c2672eb2a0"), "" },
                    { 181, "Test balance 6 columns", 1, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(5071), 48, new Guid("fef7d229-6d80-4441-8475-061a87375f70"), "" },
                    { 182, "تراز آزمایشی ۶ ستونی", 2, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(5080), 48, new Guid("5ded35e5-bccb-4fe5-8983-c7486c14cacb"), "" },
                    { 183, "Test balance 6 columns", 3, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(5094), 48, new Guid("8bf86b7f-7016-4408-9d96-b09c03f5de9b"), "" },
                    { 184, "Test balance 6 columns", 4, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(5103), 48, new Guid("22551d90-3480-4c36-8fa6-428ff8cecd3c"), "" },
                    { 185, "Test balance 8 columns", 1, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(5112), 49, new Guid("a9847c15-01df-44d4-86d5-4d5a314689dc"), "" },
                    { 186, "تراز آزمایشی ۸ ستونی", 2, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(5121), 49, new Guid("abb4b541-458a-41f9-bfe0-9ca32480ad4e"), "" },
                    { 187, "Test balance 8 columns", 3, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(5129), 49, new Guid("5671db22-4dcc-4de5-a4ae-dd33945514cb"), "" },
                    { 188, "Test balance 8 columns", 4, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(5138), 49, new Guid("16b8d6ff-5db5-487c-9e08-8b2c82b7c128"), "" },
                    { 189, "Detail account turnover/balance - 2 column", 1, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(5148), 50, new Guid("406a54c6-2d71-4da4-b260-687ec5c74eaa"), "" },
                    { 190, "گردش و مانده تفصیلی شناور 2 ستونی", 2, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(5158), 50, new Guid("c2a3caef-9d8c-4661-ae34-95ae24d4c6a2"), "" },
                    { 191, "Detail account turnover/balance - 4 column", 1, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(5172), 51, new Guid("1f5af9de-0d98-4c87-aa66-d06ca0536eb3"), "" },
                    { 192, "گردش و مانده تفصیلی شناور 4 ستونی", 2, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(5185), 51, new Guid("bc9fcf5c-ecdd-41ff-b820-652d4c3bc2bb"), "" },
                    { 193, "Detail account turnover/balance - 6 column", 1, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(5195), 52, new Guid("436e23cd-805b-4fed-87d4-ebbf683efd9a"), "" },
                    { 194, "گردش و مانده تفصیلی شناور 6 ستونی", 2, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(5203), 52, new Guid("67d04709-2012-4ba0-886a-bcdaa52ffad6"), "" },
                    { 195, "Detail account turnover/balance - 8 column", 1, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(5214), 53, new Guid("2ddcab4a-3a6c-44be-8f95-0b39676259f4"), "" },
                    { 196, "گردش و مانده تفصیلی شناور 8 ستونی", 2, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(5223), 53, new Guid("4c560ca2-e5ad-43fd-827f-d7924e3c8ee1"), "" },
                    { 175, "Test balance 2 columns", 3, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(4918), 46, new Guid("251c6000-8a5d-479e-93eb-bfe220fbda42"), "" },
                    { 197, "Cost center turnover/balance - 2 column", 1, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(5232), 54, new Guid("a991ef07-15e4-4169-ae4e-42d84c154b92"), "" },
                    { 174, "تراز آزمایشی ۲ ستونی", 2, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(4904), 46, new Guid("a1d9bfd3-79ef-465e-b720-149cedfcd371"), "" },
                    { 152, "Journal, by number, ledger summary", 4, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(4724), 38, new Guid("b15bce00-173a-491c-b6fb-5a59ec701239"), "" },
                    { 131, "Journal, by date, summary by month", 3, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(4415), 33, new Guid("5b5b23b0-9add-461e-8aa1-dddd74553dba"), "" },
                    { 132, "Journal, by date, summary by month", 4, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(4424), 33, new Guid("f2392d21-a7cf-4b05-88d8-35186982d493"), "" },
                    { 133, "Journal, by number, by row", 1, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(4433), 34, new Guid("d54703be-d1e1-4cc5-a537-f4ebd7d8aa85"), "" },
                    { 134, "دفتر روزنامه، بر حسب شماره سند، مطابق ردیف های سند", 2, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(4541), 34, new Guid("0ed0a12b-4d93-48ef-b1eb-30ceb7b9db4a"), "" },
                    { 135, "Journal, by number, by row", 3, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(4551), 34, new Guid("9dfdf8c5-1b1a-44ed-9327-f94243fa7caa"), "" },
                    { 136, "Journal, by number, by row", 4, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(4561), 34, new Guid("c2d2c9a5-79a6-4bfd-8ad9-2df971926d0d"), "" },
                    { 137, "Journal, by number, by row with details", 1, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(4570), 35, new Guid("1adae24e-7142-4a91-8790-84f485f18cca"), "" },
                    { 138, "دفتر روزنامه، بر حسب شماره سند، مطابق ردیف های سند با سطوح شناور", 2, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(4579), 35, new Guid("a62e8526-0c89-41a3-87fe-c863fc4e6670"), "" },
                    { 139, "Journal, by number, by row with details", 3, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(4593), 35, new Guid("35c26141-614e-4538-8544-e220b5693d30"), "" },
                    { 140, "Journal, by number, by row with details", 4, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(4602), 35, new Guid("6c8ac84e-7cb3-4d3b-8064-4988054f8d6d"), "" },
                    { 141, "Journal, by number, by ledger", 1, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(4610), 36, new Guid("f56a7278-a5f6-4b4c-bb04-c025ec3bd9be"), "" },
                    { 142, "دفتر روزنامه، بر حسب شماره سند، در سطح کل", 2, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(4620), 36, new Guid("a1f4356f-c711-4e22-85f1-0770470a80c1"), "" },
                    { 143, "Journal, by number, by ledger", 3, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(4630), 36, new Guid("0ded4ae5-7171-4b25-9017-ba1de91d3b72"), "" },
                    { 144, "Journal, by number, by ledger", 4, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(4639), 36, new Guid("641807a2-a6db-4d66-93d1-272d530bdd0b"), "" },
                    { 145, "Journal, by number, by subsidiary", 1, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(4651), 37, new Guid("5afd738b-8702-42df-ab3c-632b22d22124"), "" },
                    { 146, "دفتر روزنامه، بر حسب شماره سند، در سطح معین", 2, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(4660), 37, new Guid("33a4ca3d-e2a5-43a0-bf9a-b28d3db7728d"), "" }
                });

            migrationBuilder.InsertData(
                schema: "Reporting",
                table: "LocalReport",
                columns: new[] { "LocalReportID", "Caption", "LocaleId", "ModifiedDate", "ReportId", "rowguid", "Template" },
                values: new object[,]
                {
                    { 147, "Journal, by number, by subsidiary", 3, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(4676), 37, new Guid("171f50ec-74c9-4946-9058-37297a767b5e"), "" },
                    { 148, "Journal, by number, by subsidiary", 4, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(4685), 37, new Guid("c24ac3bb-d4bf-490d-802b-8a2d93b17462"), "" },
                    { 149, "Journal, by number, ledger summary", 1, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(4694), 38, new Guid("c91932ce-fcb4-4f8e-8cab-1d5301f547e2"), "" },
                    { 150, "دفتر روزنامه، بر حسب شماره سند، سند خلاصه", 2, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(4703), 38, new Guid("725daaea-20ba-4d58-bad9-dec910707d42"), "" },
                    { 151, "Journal, by number, ledger summary", 3, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(4714), 38, new Guid("bce74d8a-8c84-4310-a497-11454b8de7bd"), "" },
                    { 173, "Test balance 2 columns", 1, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(4894), 46, new Guid("df112290-3916-4f97-9863-ae82d0f495b4"), "" },
                    { 130, "دفتر روزنامه، بر حسب تاریخ، سند خلاصه به تفکیک ماه", 2, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(4401), 33, new Guid("642643dd-3476-40c6-83b6-73322acb8b04"), "" },
                    { 198, "گردش و مانده مرکز هزینه 2 ستونی", 2, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(5241), 54, new Guid("444b3d4e-5574-4879-9265-92e45c49533e"), "" },
                    { 200, "گردش و مانده مرکز هزینه 4 ستونی", 2, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(5266), 55, new Guid("4959c69b-718f-4105-bbcd-2b38e3248a97"), "" },
                    { 226, "دفتر عملیات ارزی", 2, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(5690), 68, new Guid("2a1d9410-4e81-4bb0-84b9-7c198792770b"), "" },
                    { 227, "Balance by account", 1, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(5699), 69, new Guid("bbdee4bc-6909-48e6-a8ed-a31b19459105"), "" },
                    { 228, "مانده به تفکیک حساب", 2, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(5708), 69, new Guid("cc4b2cac-c8f9-4a1d-a047-3dceeb88d897"), "" },
                    { 241, "Profit-Loss", 1, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(5835), 76, new Guid("69f34e9f-730b-4d1a-acee-258f3b4ec403"), "" },
                    { 242, "سود و زیان", 2, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(5844), 76, new Guid("acb2002e-1cbf-4b1f-af5a-2188b29e6777"), "" },
                    { 243, "Profit-Loss", 1, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(5852), 77, new Guid("a3f1294c-af0f-452b-8dfa-954f96190c60"), "" },
                    { 244, "سود و زیان", 2, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(5861), 77, new Guid("3edaed6a-56bb-4e00-b724-9abf607e1966"), "" },
                    { 249, "BalanceSheet", 1, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(5870), 80, new Guid("3f75fa40-a581-47a8-ba19-5cee5f9d45ae"), "" },
                    { 250, "ترازنامه", 2, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(5879), 80, new Guid("fc1b875c-7007-4eff-b3de-0e644043dc8d"), "" },
                    { 293, "Cash Register List", 1, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(6462), 101, new Guid("e7cfdadc-4927-4c43-9106-8c84b58be200"), "" },
                    { 294, "فهرست صندوق‌های اسناد", 2, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(6471), 101, new Guid("3a0b6991-4d4a-4886-8f7c-c3000824e800"), "" },
                    { 297, "Source and Application List", 1, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(6503), 103, new Guid("5007e8e0-0c0e-491d-8711-d21ffd30bca6"), "" },
                    { 298, "فهرست منابع و مصارف", 2, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(6512), 103, new Guid("f61836d4-ed79-4857-9723-eb712cd7802f"), "" },
                    { 299, "Recipients List", 1, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(6520), 104, new Guid("a2319ca8-ec09-4679-bf96-8515004a32bb"), "" },
                    { 300, "لیست دریافت کنندگان", 2, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(6529), 104, new Guid("335340c7-f3fc-4d11-80e0-757fa39a97ed"), "" },
                    { 301, "Payers List", 1, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(6539), 105, new Guid("7e289e35-aa9f-421e-8908-d712183f4f73"), "" },
                    { 302, "لیست پرداخت کنندگان", 2, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(6548), 105, new Guid("2d2bbb7f-a799-4b9c-8c0b-a8ca198b656c"), "" },
                    { 303, "Cash Accounts List", 1, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(6563), 106, new Guid("82e1aede-6bfe-4df5-9d44-8694b109c536"), "" },
                    { 304, "لیست حساب‌های نقدی", 2, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(6572), 106, new Guid("10d479fa-ecfd-4c3d-9235-2bb2eac63b43"), "" },
                    { 305, "Cash Accounts List", 1, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(6581), 107, new Guid("959c9e65-0e22-41b4-87a9-1d9e585660a8"), "" },
                    { 306, "لیست حساب‌های نقدی", 2, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(6590), 107, new Guid("0e666b18-7986-4564-b2bf-4cd2ddea2c20"), "" },
                    { 225, "Currency Book", 1, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(5682), 68, new Guid("bb6f3bcd-2580-4aa6-ac44-cc5ef34199b9"), "" },
                    { 199, "Cost center turnover/balance - 4 column", 1, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(5256), 55, new Guid("90b9554f-7020-4492-83a1-ed38829ef678"), "" },
                    { 224, "دفتر عملیات ارزی", 2, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(5673), 67, new Guid("cad7a1d5-85c7-4741-b8be-98fb38f1a60c"), "" },
                    { 222, "دفتر عملیات ارزی", 2, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(5647), 66, new Guid("8bb9a416-b4d4-4ac4-89b8-0e502aed2529"), "" },
                    { 201, "Cost center turnover/balance - 6 column", 1, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(5275), 56, new Guid("bdaad042-4b17-4d40-a2e7-dd1c4392b16a"), "" },
                    { 202, "گردش و مانده مرکز هزینه 6 ستونی", 2, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(5283), 56, new Guid("6636fa1f-9215-4adc-9845-162ad2134f0b"), "" },
                    { 203, "Cost center turnover/balance - 8 column", 1, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(5293), 57, new Guid("c7e1fe16-6450-4a0f-b6ce-da159af58445"), "" },
                    { 204, "گردش و مانده مرکز هزینه 8 ستونی", 2, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(5302), 57, new Guid("fb440f01-9448-4c5b-9142-ed57efe8568e"), "" },
                    { 205, "Project turnover/balance - 2 column", 1, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(5311), 58, new Guid("43de1953-ce03-4e39-991f-1ec64e504a11"), "" },
                    { 206, "گردش و مانده پروژه 2 ستونی", 2, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(5320), 58, new Guid("8f7d6fd1-6085-4c50-b20d-9952ceb7f57f"), "" },
                    { 207, "Project turnover/balance - 4 column", 1, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(5334), 59, new Guid("3919bcf0-658f-4aa2-b7ad-65169d27b303"), "" },
                    { 208, "گردش و مانده پروژه 4 ستونی", 2, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(5344), 59, new Guid("1b562924-a531-4491-a6bd-e51295515e35"), "" }
                });

            migrationBuilder.InsertData(
                schema: "Reporting",
                table: "LocalReport",
                columns: new[] { "LocalReportID", "Caption", "LocaleId", "ModifiedDate", "ReportId", "rowguid", "Template" },
                values: new object[,]
                {
                    { 209, "Project turnover/balance - 6 column", 1, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(5353), 60, new Guid("f5c34894-35f8-4e28-9bfa-a8680f7dbd1e"), "" },
                    { 210, "گردش و مانده پروژه 6 ستونی", 2, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(5362), 60, new Guid("f263df80-b778-4b41-8d74-ab836e0add51"), "" },
                    { 211, "Project turnover/balance - 8 column", 1, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(5371), 61, new Guid("3fb76185-5989-43db-b8c0-3f85f55cf033"), "" },
                    { 212, "گردش و مانده پروژه 8 ستونی", 2, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(5381), 61, new Guid("f86c7c30-d62d-46c5-a767-dcca7a576a6e"), "" },
                    { 213, "Account Book", 1, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(5390), 62, new Guid("b69f037d-d0dc-4158-b3b6-6233c05aaa9a"), "" },
                    { 214, "دفتر حساب", 2, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(5399), 62, new Guid("48a52e1d-28f0-4a82-bf0c-57f44edce36b"), "" },
                    { 215, "Account Book", 1, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(5413), 63, new Guid("f852dd2f-f280-4872-9820-45a3a6d21bb3"), "" },
                    { 216, "دفتر حساب", 2, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(5422), 63, new Guid("9bb5ecc4-aac3-4a5b-ad0f-3b3ee5ce1b87"), "" },
                    { 217, "Account Book", 1, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(5602), 64, new Guid("98920e77-5178-4e95-926e-d1e2a4a1e73f"), "" },
                    { 218, "ذفتر حساب", 2, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(5612), 64, new Guid("ed374986-6d9e-4ede-be5c-8902be78bad5"), "" },
                    { 219, "Currency Book", 1, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(5621), 65, new Guid("745e664e-5bac-4111-8883-5fa78f2c686e"), "" },
                    { 220, "دفتر عملیات ارزی", 2, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(5630), 65, new Guid("d7c0491b-6c84-4802-8db9-2a479c191e9a"), "" },
                    { 221, "Currency Book", 1, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(5638), 66, new Guid("23a3bf93-4134-4417-a170-1f2a8838149b"), "" },
                    { 223, "Currency Book", 1, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(5662), 67, new Guid("a42a41e4-ca5c-4eaa-a775-c34247da085d"), "" },
                    { 129, "Journal, by date, summary by month", 1, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(4392), 33, new Guid("83e51d2d-bda3-4caf-934c-9acde305989e"), "" },
                    { 128, "Journal, by date, summary by date", 4, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(4382), 32, new Guid("14266445-6e29-4778-a9d3-47eb3790615f"), "" },
                    { 127, "Journal, by date, summary by date", 3, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(4373), 32, new Guid("a6ed8bd5-0301-4cfe-b7ed-af27e32e517d"), "" },
                    { 275, "All Widgets", 1, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(6209), 92, new Guid("e273b701-3e16-4b5c-919e-34b80750ab55"), "" },
                    { 276, "همه ویجت ها", 2, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(6218), 92, new Guid("9b53236a-25e1-42bd-958a-04c4711a349f"), "" },
                    { 81, "Fiscal periods", 1, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(3603), 21, new Guid("d05aab67-5cd5-40ec-860f-23f55ea55490"), "" },
                    { 82, "دوره های مالی", 2, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(3613), 21, new Guid("7d3be2e7-5c96-427a-99f7-c732995b75f1"), "" },
                    { 83, "Fiscal periods", 3, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(3627), 21, new Guid("e2a6cdb1-45b6-4bdb-8962-7401682620a5"), "" },
                    { 84, "Fiscal periods", 4, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(3637), 21, new Guid("fc05d44b-1222-498c-8161-eb12e89bf460"), "" },
                    { 85, "Accounts", 1, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(3646), 22, new Guid("29dbc97b-a4a3-4610-85ab-a4125c4f654a"), "" },
                    { 86, "سرفصل های حسابداری", 2, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(3860), 22, new Guid("c13e131b-2ce7-443e-b9ac-fe398252cd71"), "" },
                    { 87, "Accounts", 3, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(3875), 22, new Guid("0f61f7fe-9456-41a9-87ab-5cf763d73aab"), "" },
                    { 88, "Accounts", 4, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(3884), 22, new Guid("262d3658-b8eb-4df6-a4df-24aa7f841e72"), "" },
                    { 89, "Detail accounts", 1, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(3895), 23, new Guid("9d99f410-f099-432a-826c-bb4b5abefe17"), "" },
                    { 90, "تفصیلی های شناور", 2, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(3905), 23, new Guid("03c6e97f-4abe-4a73-a327-cde9cab85378"), "" },
                    { 91, "Detail accounts", 3, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(3922), 23, new Guid("17c77768-ce63-4132-8df1-53f2e227f879"), "" },
                    { 92, "Detail accounts", 4, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(3932), 23, new Guid("458c9444-b09b-4d1f-afa4-8a4abcb8689b"), "" },
                    { 93, "Cost centers", 1, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(3941), 24, new Guid("481d98e9-48cd-42da-8d8b-b4b40b70526c"), "" },
                    { 94, "مراکز هزینه", 2, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(3950), 24, new Guid("9b2fcd95-6f21-408b-8b61-7e9e22689e3d"), "" },
                    { 95, "Cost centers", 3, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(3958), 24, new Guid("3dc095d3-1313-4833-91e6-e5107233524b"), "" },
                    { 96, "Cost centers", 4, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(3967), 24, new Guid("5c27f076-6f17-4c98-afcb-b9c391ac8fda"), "" },
                    { 97, "Projects", 1, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(3976), 25, new Guid("5259f254-b8ef-4e06-af86-d0acbc2dd698"), "" },
                    { 98, "پروژه ها", 2, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(3985), 25, new Guid("bc22cd6f-a039-4df4-95b3-fd9d9d565d1d"), "" },
                    { 99, "Projects", 3, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(4000), 25, new Guid("701dfa48-9b34-40f1-80c3-1291fc844298"), "" },
                    { 274, "ویجت های من", 2, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(6199), 91, new Guid("479599a8-735a-474f-9431-f893bda1d752"), "" },
                    { 100, "Projects", 4, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(4009), 25, new Guid("08bfbadb-fe89-4460-bfbd-ec02d02b30e1"), "" },
                    { 273, "My Widgets", 1, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(6191), 91, new Guid("63c5dfbd-e35e-4e69-93e3-09ba19609fcd"), "" },
                    { 235, "Active operation logs", 1, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(5775), 73, new Guid("fb1f3ab3-1df1-43f4-babd-4bfe73a7000f"), "" }
                });

            migrationBuilder.InsertData(
                schema: "Reporting",
                table: "LocalReport",
                columns: new[] { "LocalReportID", "Caption", "LocaleId", "ModifiedDate", "ReportId", "rowguid", "Template" },
                values: new object[,]
                {
                    { 30, "شرکت ها", 2, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(3025), 8, new Guid("41d79c51-1f29-4903-9d32-32a5ac97de6b"), "" },
                    { 31, "Companies", 3, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(3041), 8, new Guid("5452b5a4-eaac-434d-b78b-60a37b6c8916"), "" },
                    { 32, "Companies", 4, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(3050), 8, new Guid("f98d9ef0-7eca-414b-b102-5f8e4e0dfc24"), "" },
                    { 33, "Branches", 1, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(3060), 9, new Guid("7d297ac7-3f35-4fc9-947f-5c2637dd99c3"), "" },
                    { 34, "شعب سازمانی", 2, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(3072), 9, new Guid("6bd5f1be-3bd7-4741-b33c-440453d28985"), "" },
                    { 35, "Branches", 3, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(3081), 9, new Guid("259e23fc-252e-49cf-b095-0852a84c49ba"), "" },
                    { 36, "Branches", 4, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(3090), 9, new Guid("a0efb5b5-26ec-4b27-8694-9ccd061bdb66"), "" },
                    { 37, "Users", 1, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(3098), 10, new Guid("269978d8-1f62-410e-a169-7951aff92042"), "" },
                    { 38, "کاربران", 2, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(3107), 10, new Guid("7797153c-5b57-4785-8002-2080ab921de7"), "" },
                    { 39, "Users", 3, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(3121), 10, new Guid("cab37bdd-75a1-454e-b96e-b3efdd03031e"), "" },
                    { 40, "Users", 4, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(3130), 10, new Guid("4c299f02-497a-4326-bcb0-ca95af153d4f"), "" },
                    { 41, "Roles", 1, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(3140), 11, new Guid("4d9fcaeb-9355-407d-9be8-349e74ef033a"), "" },
                    { 42, "نقش ها", 2, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(3150), 11, new Guid("99321cbe-169b-400d-b167-7f1bff05f430"), "" },
                    { 43, "Roles", 3, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(3160), 11, new Guid("1e33ebde-4279-4d71-a7f9-ec6a7aed518a"), "" },
                    { 44, "Roles", 4, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(3170), 11, new Guid("d530150d-1081-4976-8aee-9dff83c0e0da"), "" },
                    { 229, "Archived operation logs", 1, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(5717), 70, new Guid("b7acc5fc-aa44-46b2-962a-f0c742460b60"), "" },
                    { 230, "رویدادهای عملیاتی بایگانی شده", 2, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(5726), 70, new Guid("0b544457-3fc0-40dc-ba2a-511c259bbcb0"), "" },
                    { 231, "Active system logs", 1, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(5740), 71, new Guid("bbf67eba-3cbc-434a-9682-d5eeb5cd7a2e"), "" },
                    { 232, "رویدادهای سیستمی فعال", 2, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(5749), 71, new Guid("4f50fc29-7779-42e9-8f10-c8a069bcf45e"), "" },
                    { 233, "Archived system logs", 1, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(5758), 72, new Guid("e6c04b1b-6f32-4dbc-aa6d-8ed39f09dd7f"), "" },
                    { 234, "رویدادهای سیستمی بایگانی شده", 2, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(5767), 72, new Guid("262945c1-3594-4647-8868-5646e798c3de"), "" },
                    { 236, "رویدادهای عملیاتی فعال", 2, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(5785), 73, new Guid("b0e77c98-d4d3-4bd5-898a-287bb325f644"), "" },
                    { 101, "Account groups", 1, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(4117), 26, new Guid("f9506fb9-f6f3-4a01-af94-8a74f4035e9c"), "" },
                    { 102, "گروه های حساب", 2, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(4127), 26, new Guid("bfe24c60-ca7f-41e8-9725-124e6b87bad7"), "" },
                    { 103, "Account groups", 3, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(4136), 26, new Guid("00f54673-b2ca-4123-9ffa-91dbe1b8b9f9"), "" },
                    { 106, "دفتر روزنامه، بر حسب تاریخ، مطابق ردیف های سند", 2, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(4165), 27, new Guid("468482de-a504-4fc8-9445-dca3a20b3a6b"), "" },
                    { 107, "Journal, by date, by row", 3, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(4179), 27, new Guid("e6e97517-2f42-4a21-9172-f608d5623e7b"), "" },
                    { 108, "Journal, by date, by row", 4, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(4188), 27, new Guid("1d4b0ccc-c8a2-4297-9a75-8b9ceb6c68b2"), "" },
                    { 109, "Journal, by date, by row with details", 1, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(4197), 28, new Guid("19ca6135-0f30-4125-adc5-d73ef455c533"), "" },
                    { 110, "دفتر روزنامه، بر حسب تاریخ، مطابق ردیف های سند با سطوح شناور", 2, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(4207), 28, new Guid("2985115c-e780-41f6-964c-aaa3149765e5"), "" },
                    { 111, "Journal, by date, by row with details", 3, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(4217), 28, new Guid("c4dbd0a1-0beb-4175-8fae-596f2fae4a06"), "" },
                    { 112, "Journal, by date, by row with details", 4, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(4227), 28, new Guid("1d761a01-0ecf-4db0-9cf1-de71c84b453e"), "" },
                    { 113, "Journal, by date, by ledger", 1, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(4236), 29, new Guid("9ff16c98-bb93-46d2-bc1b-44b6cfd1558b"), "" },
                    { 114, "دفتر روزنامه، بر حسب تاریخ، در سطح کل", 2, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(4246), 29, new Guid("860f22cd-b4cc-4b6c-9afe-76cacfce5b1f"), "" },
                    { 115, "Journal, by date, by ledger", 3, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(4260), 29, new Guid("5fcb493b-3164-4325-a265-dce0cd43ded3"), "" },
                    { 116, "Journal, by date, by ledger", 4, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(4269), 29, new Guid("a1be376a-309a-40ee-9ed4-e6c898a0fdde"), "" },
                    { 117, "Journal, by date, by subsidiary", 1, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(4277), 30, new Guid("d5bf2f52-00f5-4f38-8e96-3d5229f95f21"), "" },
                    { 118, "دفتر روزنامه، بر حسب تاریخ، در سطح معین", 2, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(4286), 30, new Guid("dc4a6a9f-be61-44ca-9cf4-37c8a2a128ad"), "" },
                    { 119, "Journal, by date, by subsidiary", 3, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(4296), 30, new Guid("b4f6b035-4f37-47df-81d3-978c0a16331d"), "" },
                    { 120, "Journal, by date, by subsidiary", 4, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(4305), 30, new Guid("0ffb3322-76a7-45d3-b409-e6acc880f455"), "" },
                    { 121, "Journal, by date, ledger summary", 1, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(4314), 31, new Guid("431ab649-5f68-49e0-be7a-9e4f4300a31a"), "" },
                    { 122, "دفتر روزنامه، بر حسب تاریخ، سند خلاصه", 2, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(4323), 31, new Guid("2b9cb226-c2bb-420e-bf75-30f1f6da4571"), "" }
                });

            migrationBuilder.InsertData(
                schema: "Reporting",
                table: "LocalReport",
                columns: new[] { "LocalReportID", "Caption", "LocaleId", "ModifiedDate", "ReportId", "rowguid", "Template" },
                values: new object[,]
                {
                    { 123, "Journal, by date, ledger summary", 3, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(4337), 31, new Guid("d17da636-d64f-4758-ba8c-f96b90b1bf2b"), "" },
                    { 124, "Journal, by date, ledger summary", 4, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(4346), 31, new Guid("7302ada6-e1c7-43ba-a37a-0c44108c1cb4"), "" },
                    { 125, "Journal, by date, summary by date", 1, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(4355), 32, new Guid("6d700dcf-ea0b-4535-baea-0c64fb2fd41f"), "" },
                    { 126, "دفتر روزنامه، بر حسب تاریخ، سند خلاصه به تفکیک تاریخ", 2, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(4364), 32, new Guid("5ffaf343-7d92-4ec1-98ea-895fe3b17fa1"), "" },
                    { 105, "Journal, by date, by row", 1, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(4156), 27, new Guid("ed5baacb-df24-4370-b0a8-13fae3463372"), "" },
                    { 310, "شماره سندهای مفقود", 2, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(6627), 109, new Guid("2f7b1050-ce7b-4c10-bedc-8bcd8c4bdcc5"), "" },
                    { 309, "Missing Voucher Numbers", 1, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(6618), 109, new Guid("707433c1-831f-42be-86e2-9a23e7216f8f"), "" },
                    { 308, "آرتیکل‌های سند", 2, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(6609), 108, new Guid("ea796970-fd0e-462a-aea8-95f02cf250d9"), "" },
                    { 104, "Account groups", 4, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(4145), 26, new Guid("84d3423d-de61-4d1a-8a38-40e4b25bd0dc"), "" },
                    { 237, "Currencies", 1, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(5795), 74, new Guid("3ee6b78c-bf72-4f11-9885-d8ad5531f6ca"), "" },
                    { 238, "ارزها", 2, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(5804), 74, new Guid("470e8f63-b435-4b98-ad37-5bdf34d7c95d"), "" },
                    { 239, "Currency rates", 1, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(5817), 75, new Guid("60c24f7a-e540-4054-84e1-5e2f3c661a97"), "" },
                    { 240, "نرخ های ارز", 2, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(5826), 75, new Guid("c5222a3d-0d61-417f-af5c-d119ee478225"), "" },
                    { 157, "Voucher, standard format", 1, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(4733), 40, new Guid("35eb3e5c-a1fc-475b-aaf6-65ca58aeaabe"), "" },
                    { 158, "فرم مرسوم سند", 2, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(4742), 40, new Guid("6f111df9-da27-47f3-a5ac-1ba9c63a52a9"), "" },
                    { 159, "Voucher, standard format", 3, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(4756), 40, new Guid("ad10b1b9-1882-4338-8f19-360497d37e43"), "" },
                    { 160, "Voucher, standard format", 4, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(4765), 40, new Guid("66b8499c-e60b-4c22-bb8a-2bec894d02ca"), "" },
                    { 161, "Voucher, standard format, with detail", 1, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(4775), 41, new Guid("f2da17aa-4541-46b1-822e-8ba9d6105a9e"), "" },
                    { 295, "Check Book Report", 1, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(6485), 102, new Guid("0e888175-f29e-484d-9e0b-ce1e5822b96a"), "" },
                    { 162, "فرم مرسوم سند - با سطوح شناور", 2, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(4785), 41, new Guid("aaa0ac49-2208-4f27-b572-895cab33e307"), "" },
                    { 164, "Voucher, standard format, with detail", 4, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(4805), 41, new Guid("6f20475d-327d-4a39-bd57-e77557b48980"), "" },
                    { 251, "Vouchers", 1, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(5893), 81, new Guid("87130040-3fb5-4f3d-8e0f-fd6ccb81579b"), "" },
                    { 252, "اسناد مالی", 2, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(5902), 81, new Guid("134350c9-d7a7-451e-a80e-877b041dd842"), "" },
                    { 255, "Simple - by detail level", 1, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(5914), 82, new Guid("60ada576-04ea-48de-9766-d1674c7eed33"), "" },
                    { 256, "ساده - در سطح تفصیلی", 2, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(5922), 82, new Guid("20f39ac4-e4d1-46e1-9e43-d9df75cafa79"), "" },
                    { 257, "Aggregate - by ledger level", 1, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(5931), 83, new Guid("3021bf7a-cd25-4496-8329-c651480684d0"), "" },
                    { 258, "مرکب - در سطح کل", 2, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(5940), 83, new Guid("22722773-d101-4b51-aed4-3d96b8b82eac"), "" },
                    { 259, "Aggregate - by subsidiary level", 1, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(5951), 84, new Guid("29e386e5-180f-4bdf-ae3d-881f7a0a894d"), "" },
                    { 260, "مرکب - در سطح معین", 2, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(5960), 84, new Guid("0deeeaea-4b93-433a-9aa4-e3e53c6dd304"), "" },
                    { 307, "Voucher Lines", 1, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(6600), 108, new Guid("ff0e6fe3-48cd-4f0a-b8bb-81813377d942"), "" },
                    { 163, "Voucher, standard format, with detail", 3, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(4796), 41, new Guid("15b223ef-cdb9-47fc-b188-8a2599969a3b"), "" },
                    { 296, "دفتر دسته‌چک", 2, new DateTime(2023, 10, 18, 17, 16, 5, 953, DateTimeKind.Local).AddTicks(6494), 102, new Guid("b7543d37-34e7-43d1-adbe-d2d15d7f2191"), "" }
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
                name: "IX_Report_ReportViewId",
                schema: "Reporting",
                table: "Report",
                column: "ReportViewId");

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
                name: "ReportView",
                schema: "Metadata");

            migrationBuilder.DropTable(
                name: "User",
                schema: "Auth");

            migrationBuilder.DropTable(
                name: "View",
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
