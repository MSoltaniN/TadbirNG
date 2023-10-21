using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SPPC.Tadbir.Web.Api.Migrations.System
{
    public partial class initial : Migration
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
                columns: new[] { "UserID", "IsEnabled", "LastLoginDate", "PasswordHash", "UserName" },
                values: new object[] { 1, true, new DateTime(2023, 10, 16, 8, 57, 46, 3, DateTimeKind.Unspecified), "b22f213ec710f0b0e86297d10279d69171f50f01a04edf40f472a563e7ad8576", "admin" });

            migrationBuilder.InsertData(
                schema: "Config",
                table: "CompanyDb",
                columns: new[] { "CompanyID", "DbName", "Description", "IsActive", "ModifiedDate", "Name", "Password", "Server", "UserName" },
                values: new object[] { 1, "NGTadbirMG2", "", true, new DateTime(2023, 10, 21, 17, 7, 28, 969, DateTimeKind.Local).AddTicks(4967), "شرکت نمونه", "$$$%%%", "130.185.75.230,49878", "TadbirUser" });

            migrationBuilder.InsertData(
                schema: "Core",
                table: "Version",
                columns: new[] { "VersionId", "ModifiedDate", "Number", "rowguid" },
                values: new object[] { 1, new DateTime(2022, 8, 27, 13, 56, 52, 150, DateTimeKind.Unspecified), "2.2.0", new Guid("26452115-8352-42fe-a7b8-4bd3d32f50f6") });

            migrationBuilder.InsertData(
                schema: "Metadata",
                table: "Column",
                columns: new[] { "ColumnID", "AllowFiltering", "AllowSorting", "DisplayIndex", "DotNetType", "Expression", "GroupName", "IsDynamic", "IsFixedLength", "IsNullable", "Length", "MinLength", "Name", "ScriptType", "StorageType", "Type", "ViewID", "Visibility" },
                values: new object[,]
                {
                    { 2, true, false, (short)-1, "System.Int16", "", "", false, false, false, 0, 0, "BranchScope", "number", "smallint", "", null, "AlwaysHidden" },
                    { 604, true, true, (short)9, "System.DateTime", "", "", false, false, false, 0, 0, "EntityDate", "Date", "datetime", "Default", null, "Visible" },
                    { 605, true, true, (short)10, "System.String", "", "", false, false, false, 64, 0, "SourceName", "string", "nvarchar", "", null, "Visible" },
                    { 606, true, true, (short)11, "System.String", "", "", false, false, false, 64, 0, "SourceListName", "string", "nvarchar", "", null, "Visible" },
                    { 607, true, true, (short)12, "System.String", "", "", false, false, false, 64, 0, "OperationName", "string", "nvarchar", "", null, "Visible" },
                    { 608, true, true, (short)13, "System.String", "", "", false, false, false, 64, 0, "Description", "string", "nvarchar", "", null, "Visible" },
                    { 609, true, true, (short)14, "System.String", "", "", false, false, false, 64, 0, "CompanyName", "string", "nvarchar", "", null, "Visible" },
                    { 610, true, true, (short)-1, "System.Int32", "", "", false, false, true, 0, 0, "Id", "number", "int", "", null, "AlwaysHidden" },
                    { 611, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, "RowNo", "number", "int", "", null, "AlwaysVisible" },
                    { 612, true, true, (short)1, "System.String", "", "", false, false, false, 64, 0, "UserName", "string", "nvarchar", "", null, "Visible" },
                    { 613, true, true, (short)2, "System.Date", "", "", false, false, false, 0, 0, "Date", "Date", "datetime", "Default", null, "AlwaysVisible" },
                    { 614, true, true, (short)3, "System.TimeSpan", "", "", false, false, false, 7, 0, "Time", "Date", "time", "", null, "AlwaysVisible" },
                    { 603, true, true, (short)8, "System.Int32", "", "", false, false, false, 0, 0, "EntityNo", "number", "int", "", null, "Visible" },
                    { 615, true, true, (short)4, "System.String", "", "", false, false, false, 64, 0, "EntityTypeName", "string", "nvarchar", "", null, "Visible" },
                    { 617, true, true, (short)6, "System.String", "", "", false, false, false, 64, 0, "EntityName", "string", "nvarchar", "", null, "Visible" },
                    { 618, true, true, (short)7, "System.String", "", "", false, false, false, 64, 0, "EntityDescription", "string", "nvarchar", "", null, "Visible" },
                    { 619, true, true, (short)8, "System.Int32", "", "", false, false, false, 0, 0, "EntityNo", "number", "int", "", null, "Visible" },
                    { 620, true, true, (short)9, "System.DateTime", "", "", false, false, false, 0, 0, "EntityDate", "Date", "datetime", "Default", null, "Visible" },
                    { 621, true, true, (short)10, "System.String", "", "", false, false, false, 64, 0, "SourceName", "string", "nvarchar", "", null, "Visible" },
                    { 622, true, true, (short)11, "System.String", "", "", false, false, false, 64, 0, "SourceListName", "string", "nvarchar", "", null, "Visible" },
                    { 623, true, true, (short)12, "System.String", "", "", false, false, false, 64, 0, "OperationName", "string", "nvarchar", "", null, "Visible" },
                    { 624, true, true, (short)13, "System.String", "", "", false, false, false, 64, 0, "Description", "string", "nvarchar", "", null, "Visible" },
                    { 625, true, true, (short)14, "System.String", "", "", false, false, false, 64, 0, "CompanyName", "string", "nvarchar", "", null, "Visible" },
                    { 626, true, true, (short)-1, "System.Int32", "", "", false, false, true, 0, 0, "Id", "number", "int", "", null, "AlwaysHidden" },
                    { 627, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, "RowNo", "number", "int", "", null, "AlwaysVisible" },
                    { 616, true, true, (short)5, "System.String", "", "", false, false, false, 64, 0, "EntityCode", "string", "nvarchar", "", null, "Visible" },
                    { 628, true, true, (short)1, "System.String", "", "", false, false, false, 64, 0, "UserName", "string", "nvarchar", "", null, "Visible" },
                    { 602, true, true, (short)7, "System.String", "", "", false, false, false, 64, 0, "EntityDescription", "string", "nvarchar", "", null, "Visible" },
                    { 600, true, true, (short)5, "System.String", "", "", false, false, false, 64, 0, "EntityCode", "string", "nvarchar", "", null, "Visible" },
                    { 576, true, true, (short)4, "System.String", "", "", false, false, false, 512, 0, "DetailAccountName", "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 577, true, true, (short)5, "System.String", "", "", false, false, false, 512, 0, "CostCenterFullCode", "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 578, true, true, (short)6, "System.String", "", "", false, false, false, 512, 0, "CostCenterName", "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 579, true, true, (short)7, "System.String", "", "", false, false, false, 512, 0, "ProjectFullCode", "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 580, true, true, (short)8, "System.String", "", "", false, false, false, 512, 0, "ProjectName", "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 581, true, true, (short)9, "System.String", "", "", false, false, false, 512, 0, "AccountDescription", "string", "nvarchar", "", null, "Visible" },
                    { 582, true, true, (short)10, "System.Decimal", "", "", false, false, false, 0, 0, "StartBalance", "number", "money", "Money", null, "Visible" },
                    { 583, true, true, (short)11, "System.Decimal", "", "", false, false, false, 0, 0, "Debit", "number", "money", "Money", null, "Visible" },
                    { 584, true, true, (short)12, "System.Decimal", "", "", false, false, false, 0, 0, "Credit", "number", "money", "Money", null, "Visible" },
                    { 585, true, true, (short)13, "System.Decimal", "", "", false, false, false, 0, 0, "EndBalance", "number", "money", "Money", null, "Visible" }
                });

            migrationBuilder.InsertData(
                schema: "Metadata",
                table: "Column",
                columns: new[] { "ColumnID", "AllowFiltering", "AllowSorting", "DisplayIndex", "DotNetType", "Expression", "GroupName", "IsDynamic", "IsFixedLength", "IsNullable", "Length", "MinLength", "Name", "ScriptType", "StorageType", "Type", "ViewID", "Visibility" },
                values: new object[,]
                {
                    { 586, true, true, (short)14, "System.String", "", "", false, false, false, 128, 0, "BranchName", "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 601, true, true, (short)6, "System.String", "", "", false, false, false, 64, 0, "EntityName", "string", "nvarchar", "", null, "Visible" },
                    { 587, true, true, (short)12, "System.String", "", "", false, false, false, 64, 0, "EntityReference", "string", "nvarchar", "", null, "Visible" },
                    { 589, true, true, (short)14, "System.String", "", "", false, false, false, 64, 0, "SourceName", "string", "nvarchar", "", null, "Visible" },
                    { 590, true, true, (short)15, "System.String", "", "", false, false, false, 64, 0, "SourceListName", "string", "nvarchar", "", null, "Visible" },
                    { 591, true, true, (short)16, "System.String", "", "", false, false, false, 64, 0, "OperationName", "string", "nvarchar", "", null, "Visible" },
                    { 592, true, true, (short)17, "System.String", "", "", false, false, false, 64, 0, "Description", "string", "nvarchar", "", null, "Visible" },
                    { 593, true, true, (short)18, "System.String", "", "", false, false, false, 64, 0, "CompanyName", "string", "nvarchar", "", null, "Visible" },
                    { 594, true, true, (short)-1, "System.Int32", "", "", false, false, true, 0, 0, "Id", "number", "int", "", null, "AlwaysHidden" },
                    { 595, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, "RowNo", "number", "int", "", null, "AlwaysVisible" },
                    { 596, true, true, (short)1, "System.String", "", "", false, false, false, 64, 0, "UserName", "string", "nvarchar", "", null, "Visible" },
                    { 597, true, true, (short)2, "System.Date", "", "", false, false, false, 0, 0, "Date", "Date", "datetime", "Default", null, "AlwaysVisible" },
                    { 598, true, true, (short)3, "System.TimeSpan", "", "", false, false, false, 7, 0, "Time", "Date", "time", "", null, "AlwaysVisible" },
                    { 599, true, true, (short)4, "System.String", "", "", false, false, false, 64, 0, "EntityTypeName", "string", "nvarchar", "", null, "Visible" },
                    { 588, true, true, (short)13, "System.String", "", "", false, false, false, 64, 0, "EntityAssociation", "string", "nvarchar", "", null, "Visible" },
                    { 629, true, true, (short)2, "System.String", "", "", false, false, false, 64, 0, "BranchName", "string", "nvarchar", "", null, "Visible" },
                    { 630, true, true, (short)3, "System.String", "", "", false, false, false, 64, 0, "FiscalPeriodName", "string", "nvarchar", "", null, "Visible" },
                    { 631, true, true, (short)4, "System.Date", "", "", false, false, false, 0, 0, "Date", "Date", "datetime", "Default", null, "AlwaysVisible" },
                    { 661, true, true, (short)0, "System.Int32", "", "", false, false, false, 0, 0, "No", "number", "int", "", null, "AlwaysVisible" },
                    { 662, true, true, (short)1, "System.DateTime", "", "", false, false, false, 0, 0, "Date", "Date", "datetime", "Default", null, "AlwaysVisible" },
                    { 663, true, true, (short)2, "System.String", "", "", false, false, false, 128, 0, "Name", "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 664, true, true, (short)3, "System.String", "", "", false, false, false, 128, 0, "FullCode", "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 665, true, true, (short)4, "System.String", "", "", false, false, false, 128, 0, "ErrorMessage", "string", "nvarchar", "", null, "Visible" },
                    { 666, false, false, (short)0, "System.String", "", "", false, false, true, 64, 0, "Group", "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 667, false, false, (short)1, "System.String", "", "", false, false, true, 64, 0, "Account", "string", "nvarchar", "", null, "Visible" },
                    { 668, false, false, (short)2, "System.Decimal", "", "", false, false, true, 0, 0, "Balance", "number", "money", "Money", null, "Visible" },
                    { 669, true, true, (short)15, "System.String", "", "", false, false, true, 64, 0, "OriginName", "string", "nvarchar", "", null, "Visible" },
                    { 670, false, false, (short)0, "System.String", "", "", false, false, false, 0, 0, "Group", "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 671, false, false, (short)1, "System.String", "", "", false, false, false, 0, 0, "Account", "string", "nvarchar", "", null, "Visible" },
                    { 660, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, "Id", "number", "int", "", null, "AlwaysHidden" },
                    { 672, false, false, (short)2, "System.Decimal", "", "", true, false, false, 0, 0, "StartBalanceItem", "number", "money", "Money", null, "Visible" },
                    { 674, false, false, (short)4, "System.Decimal", "", "", true, false, false, 0, 0, "EndBalanceItem", "number", "money", "Money", null, "Visible" },
                    { 675, false, false, (short)0, "System.String", "", "", false, false, false, 0, 0, "Group", "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 676, false, false, (short)1, "System.String", "", "", false, false, false, 0, 0, "Account", "string", "nvarchar", "", null, "Visible" },
                    { 677, false, false, (short)2, "System.Decimal", "", "", true, false, false, 0, 0, "BalanceItem", "number", "money", "Money", null, "Visible" },
                    { 678, false, false, (short)16, "System.String", "", "", false, false, true, 0, 0, "TypeName", "string", "nvarchar", "", null, "Visible" },
                    { 679, false, false, (short)0, "System.String", "", "", false, false, true, 0, 0, "Assets", "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 680, false, false, (short)1, "System.Decimal", "", "", false, false, true, 0, 0, "AssetsBalance", "number", "money", "Money", null, "Visible" },
                    { 681, false, false, (short)2, "System.Decimal", "", "", false, false, true, 0, 0, "AssetsPreviousBalance", "number", "money", "Money", null, "Visible" },
                    { 682, false, false, (short)3, "System.String", "", "", false, false, true, 0, 0, "Liabilities", "string", "nvarchar", "", null, "Visible" },
                    { 683, false, false, (short)4, "System.Decimal", "", "", false, false, true, 0, 0, "LiabilitiesBalance", "number", "money", "Money", null, "Visible" },
                    { 684, false, false, (short)5, "System.Decimal", "", "", false, false, true, 0, 0, "LiabilitiesPreviousBalance", "number", "money", "Money", null, "Visible" }
                });

            migrationBuilder.InsertData(
                schema: "Metadata",
                table: "Column",
                columns: new[] { "ColumnID", "AllowFiltering", "AllowSorting", "DisplayIndex", "DotNetType", "Expression", "GroupName", "IsDynamic", "IsFixedLength", "IsNullable", "Length", "MinLength", "Name", "ScriptType", "StorageType", "Type", "ViewID", "Visibility" },
                values: new object[,]
                {
                    { 673, false, false, (short)3, "System.Decimal", "", "", true, false, false, 0, 0, "PeriodTurnoverItem", "number", "money", "Money", null, "Visible" },
                    { 659, true, true, (short)14, "System.String", "", "", false, false, true, 120, 0, "IssuerName", "string", "nvarchar", "", null, "Visible" },
                    { 658, true, true, (short)13, "System.String", "", "", false, false, true, 120, 0, "BranchName", "string", "nvarchar", "", null, "Visible" },
                    { 657, true, true, (short)12, "System.Boolean", "", "", false, false, true, 0, 0, "IsApproved", "boolean", "bit", "", null, "Visible" },
                    { 632, true, true, (short)5, "System.TimeSpan", "", "", false, false, false, 7, 0, "Time", "Date", "time", "", null, "AlwaysVisible" },
                    { 633, true, true, (short)6, "System.String", "", "", false, false, false, 64, 0, "EntityTypeName", "string", "nvarchar", "", null, "Visible" },
                    { 634, true, true, (short)7, "System.String", "", "", false, false, false, 64, 0, "EntityCode", "string", "nvarchar", "", null, "Visible" },
                    { 635, true, true, (short)8, "System.String", "", "", false, false, false, 64, 0, "EntityName", "string", "nvarchar", "", null, "Visible" },
                    { 636, true, true, (short)9, "System.String", "", "", false, false, false, 64, 0, "EntityDescription", "string", "nvarchar", "", null, "Visible" },
                    { 637, true, true, (short)10, "System.Int32", "", "", false, false, false, 0, 0, "EntityNo", "number", "int", "", null, "Visible" },
                    { 638, true, true, (short)11, "System.DateTime", "", "", false, false, false, 0, 0, "EntityDate", "Date", "datetime", "Default", null, "Visible" },
                    { 639, true, true, (short)12, "System.String", "", "", false, false, false, 64, 0, "EntityReference", "string", "nvarchar", "", null, "Visible" },
                    { 640, true, true, (short)13, "System.String", "", "", false, false, false, 64, 0, "EntityAssociation", "string", "nvarchar", "", null, "Visible" },
                    { 641, true, true, (short)14, "System.String", "", "", false, false, false, 64, 0, "SourceName", "string", "nvarchar", "", null, "Visible" },
                    { 642, true, true, (short)15, "System.String", "", "", false, false, false, 64, 0, "SourceListName", "string", "nvarchar", "", null, "Visible" },
                    { 643, true, true, (short)16, "System.String", "", "", false, false, false, 64, 0, "OperationName", "string", "nvarchar", "", null, "Visible" },
                    { 644, true, true, (short)17, "System.String", "", "", false, false, false, 64, 0, "Description", "string", "nvarchar", "", null, "Visible" },
                    { 645, true, true, (short)18, "System.String", "", "", false, false, false, 64, 0, "CompanyName", "string", "nvarchar", "", null, "Visible" },
                    { 646, true, true, (short)-1, "System.String", "", "", false, false, false, 16, 0, "Password", "string", "nvarchar", "", null, "AlwaysHidden" },
                    { 647, true, true, (short)8, "System.Boolean", "", "", false, false, true, 0, 0, "IsBalanced", "boolean", "bit", "", null, "Visible" },
                    { 648, true, true, (short)9, "System.String", "", "", false, false, true, 120, 0, "ConfirmerName", "string", "nvarchar", "", null, "Visible" },
                    { 649, true, true, (short)10, "System.String", "", "", false, false, true, 120, 0, "ApproverName", "string", "nvarchar", "", null, "Visible" },
                    { 650, false, false, (short)0, "System.String", "", "", false, false, true, 0, 0, "Group", "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 651, false, false, (short)1, "System.String", "", "", false, false, true, 0, 0, "Account", "string", "nvarchar", "", null, "Visible" },
                    { 652, false, false, (short)2, "System.Decimal", "", "", false, false, true, 0, 0, "StartBalance", "number", "money", "Money", null, "Visible" },
                    { 653, false, false, (short)3, "System.Decimal", "", "", false, false, true, 0, 0, "PeriodTurnover", "number", "money", "Money", null, "Visible" },
                    { 654, false, false, (short)4, "System.Decimal", "", "", false, false, true, 0, 0, "EndBalance", "number", "money", "Money", null, "Visible" },
                    { 655, false, false, (short)5, "System.Decimal", "", "", false, false, true, 0, 0, "Balance", "number", "money", "Money", null, "Hidden" },
                    { 656, true, true, (short)11, "System.Boolean", "", "", false, false, true, 0, 0, "IsConfirmed", "boolean", "bit", "", null, "Visible" },
                    { 575, true, true, (short)3, "System.String", "", "", false, false, false, 512, 0, "DetailAccountFullCode", "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 574, true, true, (short)2, "System.String", "", "", false, false, false, 512, 0, "AccountName", "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 573, true, true, (short)1, "System.String", "", "", false, false, false, 512, 0, "AccountFullCode", "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 572, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, "RowNo", "number", "int", "", null, "AlwaysVisible" },
                    { 490, true, true, (short)1, "System.String", "", "", false, false, false, 512, 0, "CostCenterFullCode", "string", "nvarchar", "", null, "Visible" },
                    { 491, true, true, (short)2, "System.String", "", "", false, false, false, 512, 0, "CostCenterName", "string", "nvarchar", "", null, "Visible" },
                    { 492, true, true, (short)3, "System.Decimal", "", "StartBalance", false, false, false, 0, 0, "StartBalanceDebit", "number", "money", "Money", null, "Visible" },
                    { 493, true, true, (short)4, "System.Decimal", "", "StartBalance", false, false, false, 0, 0, "StartBalanceCredit", "number", "money", "Money", null, "Visible" },
                    { 494, true, true, (short)5, "System.Decimal", "", "Turnover", false, false, false, 0, 0, "TurnoverDebit", "number", "money", "Money", null, "Visible" },
                    { 495, true, true, (short)6, "System.Decimal", "", "Turnover", false, false, false, 0, 0, "TurnoverCredit", "number", "money", "Money", null, "Visible" },
                    { 496, true, true, (short)7, "System.Decimal", "", "OperationSum", false, false, false, 0, 0, "OperationSumDebit", "number", "money", "Money", null, "Visible" },
                    { 497, true, true, (short)8, "System.Decimal", "", "OperationSum", false, false, false, 0, 0, "OperationSumCredit", "number", "money", "Money", null, "Visible" },
                    { 498, true, true, (short)9, "System.Decimal", "", "EndBalance", false, false, false, 0, 0, "EndBalanceDebit", "number", "money", "Money", null, "Visible" }
                });

            migrationBuilder.InsertData(
                schema: "Metadata",
                table: "Column",
                columns: new[] { "ColumnID", "AllowFiltering", "AllowSorting", "DisplayIndex", "DotNetType", "Expression", "GroupName", "IsDynamic", "IsFixedLength", "IsNullable", "Length", "MinLength", "Name", "ScriptType", "StorageType", "Type", "ViewID", "Visibility" },
                values: new object[,]
                {
                    { 499, true, true, (short)10, "System.Decimal", "", "EndBalance", false, false, false, 0, 0, "EndBalanceCredit", "number", "money", "Money", null, "Visible" },
                    { 500, true, true, (short)11, "System.String", "", "", false, false, false, 128, 0, "BranchName", "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 489, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, "RowNo", "number", "int", "", null, "AlwaysVisible" },
                    { 501, true, true, (short)-1, "System.String", "", "", false, false, true, 64, 0, "VoucherReference", "string", "nvarchar", "", null, "AlwaysHidden" },
                    { 503, true, true, (short)1, "System.String", "", "", false, false, false, 512, 0, "CostCenterFullCode", "string", "nvarchar", "", null, "Visible" },
                    { 504, true, true, (short)2, "System.String", "", "", false, false, false, 512, 0, "CostCenterName", "string", "nvarchar", "", null, "Visible" },
                    { 505, true, true, (short)3, "System.Decimal", "", "StartBalance", false, false, false, 0, 0, "StartBalanceDebit", "number", "money", "Money", null, "Visible" },
                    { 506, true, true, (short)4, "System.Decimal", "", "StartBalance", false, false, false, 0, 0, "StartBalanceCredit", "number", "money", "Money", null, "Visible" },
                    { 507, true, true, (short)5, "System.Decimal", "", "Turnover", false, false, false, 0, 0, "TurnoverDebit", "number", "money", "Money", null, "Visible" },
                    { 508, true, true, (short)6, "System.Decimal", "", "Turnover", false, false, false, 0, 0, "TurnoverCredit", "number", "money", "Money", null, "Visible" },
                    { 509, true, true, (short)7, "System.Decimal", "", "OperationSum", false, false, false, 0, 0, "OperationSumDebit", "number", "money", "Money", null, "Visible" },
                    { 510, true, true, (short)8, "System.Decimal", "", "OperationSum", false, false, false, 0, 0, "OperationSumCredit", "number", "money", "Money", null, "Visible" },
                    { 511, true, true, (short)9, "System.Decimal", "", "Corrections", false, false, false, 0, 0, "CorrectionsDebit", "number", "money", "Money", null, "Visible" },
                    { 512, true, true, (short)10, "System.Decimal", "", "Corrections", false, false, false, 0, 0, "CorrectionsCredit", "number", "money", "Money", null, "Visible" },
                    { 513, true, true, (short)11, "System.Decimal", "", "EndBalance", false, false, false, 0, 0, "EndBalanceDebit", "number", "money", "Money", null, "Visible" },
                    { 502, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, "RowNo", "number", "int", "", null, "AlwaysVisible" },
                    { 488, true, true, (short)-1, "System.String", "", "", false, false, true, 64, 0, "VoucherReference", "string", "nvarchar", "", null, "AlwaysHidden" },
                    { 487, true, true, (short)9, "System.String", "", "", false, false, false, 128, 0, "BranchName", "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 486, true, true, (short)8, "System.Decimal", "", "EndBalance", false, false, false, 0, 0, "EndBalanceCredit", "number", "money", "Money", null, "Visible" },
                    { 461, true, true, (short)-1, "System.String", "", "", false, false, true, 64, 0, "VoucherReference", "string", "nvarchar", "", null, "AlwaysHidden" },
                    { 462, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, "RowNo", "number", "int", "", null, "AlwaysVisible" },
                    { 463, true, true, (short)1, "System.String", "", "", false, false, false, 512, 0, "CostCenterFullCode", "string", "nvarchar", "", null, "Visible" },
                    { 464, true, true, (short)2, "System.String", "", "", false, false, false, 512, 0, "CostCenterName", "string", "nvarchar", "", null, "Visible" },
                    { 465, true, true, (short)3, "System.Decimal", "", "EndBalance", false, false, false, 0, 0, "EndBalanceDebit", "number", "money", "Money", null, "Visible" },
                    { 466, true, true, (short)4, "System.Decimal", "", "EndBalance", false, false, false, 0, 0, "EndBalanceCredit", "number", "money", "Money", null, "Visible" },
                    { 467, true, true, (short)5, "System.String", "", "", false, false, false, 128, 0, "BranchName", "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 468, true, true, (short)-1, "System.String", "", "", false, false, true, 64, 0, "VoucherReference", "string", "nvarchar", "", null, "AlwaysHidden" },
                    { 469, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, "RowNo", "number", "int", "", null, "AlwaysVisible" },
                    { 470, true, true, (short)1, "System.String", "", "", false, false, false, 512, 0, "CostCenterFullCode", "string", "nvarchar", "", null, "Visible" },
                    { 471, true, true, (short)2, "System.String", "", "", false, false, false, 512, 0, "CostCenterName", "string", "nvarchar", "", null, "Visible" },
                    { 472, true, true, (short)3, "System.Decimal", "", "Turnover", false, false, false, 0, 0, "TurnoverDebit", "number", "money", "Money", null, "Visible" },
                    { 473, true, true, (short)4, "System.Decimal", "", "Turnover", false, false, false, 0, 0, "TurnoverCredit", "number", "money", "Money", null, "Visible" },
                    { 474, true, true, (short)5, "System.Decimal", "", "EndBalance", false, false, false, 0, 0, "EndBalanceDebit", "number", "money", "Money", null, "Visible" },
                    { 475, true, true, (short)6, "System.Decimal", "", "EndBalance", false, false, false, 0, 0, "EndBalanceCredit", "number", "money", "Money", null, "Visible" },
                    { 476, true, true, (short)7, "System.String", "", "", false, false, false, 128, 0, "BranchName", "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 477, true, true, (short)-1, "System.String", "", "", false, false, true, 64, 0, "VoucherReference", "string", "nvarchar", "", null, "AlwaysHidden" },
                    { 478, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, "RowNo", "number", "int", "", null, "AlwaysVisible" },
                    { 479, true, true, (short)1, "System.String", "", "", false, false, false, 512, 0, "CostCenterFullCode", "string", "nvarchar", "", null, "Visible" },
                    { 480, true, true, (short)2, "System.String", "", "", false, false, false, 512, 0, "CostCenterName", "string", "nvarchar", "", null, "Visible" },
                    { 481, true, true, (short)3, "System.Decimal", "", "StartBalance", false, false, false, 0, 0, "StartBalanceDebit", "number", "money", "Money", null, "Visible" },
                    { 482, true, true, (short)4, "System.Decimal", "", "StartBalance", false, false, false, 0, 0, "StartBalanceCredit", "number", "money", "Money", null, "Visible" },
                    { 483, true, true, (short)5, "System.Decimal", "", "Turnover", false, false, false, 0, 0, "TurnoverDebit", "number", "money", "Money", null, "Visible" }
                });

            migrationBuilder.InsertData(
                schema: "Metadata",
                table: "Column",
                columns: new[] { "ColumnID", "AllowFiltering", "AllowSorting", "DisplayIndex", "DotNetType", "Expression", "GroupName", "IsDynamic", "IsFixedLength", "IsNullable", "Length", "MinLength", "Name", "ScriptType", "StorageType", "Type", "ViewID", "Visibility" },
                values: new object[,]
                {
                    { 484, true, true, (short)6, "System.Decimal", "", "Turnover", false, false, false, 0, 0, "TurnoverCredit", "number", "money", "Money", null, "Visible" },
                    { 485, true, true, (short)7, "System.Decimal", "", "EndBalance", false, false, false, 0, 0, "EndBalanceDebit", "number", "money", "Money", null, "Visible" },
                    { 514, true, true, (short)12, "System.Decimal", "", "EndBalance", false, false, false, 0, 0, "EndBalanceCredit", "number", "money", "Money", null, "Visible" },
                    { 685, true, true, (short)14, "System.String", "", "", false, false, false, 64, 0, "BranchName", "string", "nvarchar", "", null, "Hidden" },
                    { 515, true, true, (short)13, "System.String", "", "", false, false, false, 128, 0, "BranchName", "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 517, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, "RowNo", "number", "int", "", null, "AlwaysVisible" },
                    { 548, true, true, (short)4, "System.Decimal", "", "StartBalance", false, false, false, 0, 0, "StartBalanceCredit", "number", "money", "Money", null, "Visible" },
                    { 549, true, true, (short)5, "System.Decimal", "", "Turnover", false, false, false, 0, 0, "TurnoverDebit", "number", "money", "Money", null, "Visible" },
                    { 550, true, true, (short)6, "System.Decimal", "", "Turnover", false, false, false, 0, 0, "TurnoverCredit", "number", "money", "Money", null, "Visible" },
                    { 551, true, true, (short)7, "System.Decimal", "", "OperationSum", false, false, false, 0, 0, "OperationSumDebit", "number", "money", "Money", null, "Visible" },
                    { 552, true, true, (short)8, "System.Decimal", "", "OperationSum", false, false, false, 0, 0, "OperationSumCredit", "number", "money", "Money", null, "Visible" },
                    { 553, true, true, (short)9, "System.Decimal", "", "EndBalance", false, false, false, 0, 0, "EndBalanceDebit", "number", "money", "Money", null, "Visible" },
                    { 554, true, true, (short)10, "System.Decimal", "", "EndBalance", false, false, false, 0, 0, "EndBalanceCredit", "number", "money", "Money", null, "Visible" },
                    { 555, true, true, (short)11, "System.String", "", "", false, false, false, 128, 0, "BranchName", "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 556, true, true, (short)-1, "System.String", "", "", false, false, true, 64, 0, "VoucherReference", "string", "nvarchar", "", null, "AlwaysHidden" },
                    { 557, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, "RowNo", "number", "int", "", null, "AlwaysVisible" },
                    { 558, true, true, (short)1, "System.String", "", "", false, false, false, 512, 0, "ProjectFullCode", "string", "nvarchar", "", null, "Visible" },
                    { 547, true, true, (short)3, "System.Decimal", "", "StartBalance", false, false, false, 0, 0, "StartBalanceDebit", "number", "money", "Money", null, "Visible" },
                    { 559, true, true, (short)2, "System.String", "", "", false, false, false, 512, 0, "ProjectName", "string", "nvarchar", "", null, "Visible" },
                    { 561, true, true, (short)4, "System.Decimal", "", "StartBalance", false, false, false, 0, 0, "StartBalanceCredit", "number", "money", "Money", null, "Visible" },
                    { 562, true, true, (short)5, "System.Decimal", "", "Turnover", false, false, false, 0, 0, "TurnoverDebit", "number", "money", "Money", null, "Visible" },
                    { 563, true, true, (short)6, "System.Decimal", "", "Turnover", false, false, false, 0, 0, "TurnoverCredit", "number", "money", "Money", null, "Visible" },
                    { 564, true, true, (short)7, "System.Decimal", "", "OperationSum", false, false, false, 0, 0, "OperationSumDebit", "number", "money", "Money", null, "Visible" },
                    { 565, true, true, (short)8, "System.Decimal", "", "OperationSum", false, false, false, 0, 0, "OperationSumCredit", "number", "money", "Money", null, "Visible" },
                    { 566, true, true, (short)9, "System.Decimal", "", "Corrections", false, false, false, 0, 0, "CorrectionsDebit", "number", "money", "Money", null, "Visible" },
                    { 567, true, true, (short)10, "System.Decimal", "", "Corrections", false, false, false, 0, 0, "CorrectionsCredit", "number", "money", "Money", null, "Visible" },
                    { 568, true, true, (short)11, "System.Decimal", "", "EndBalance", false, false, false, 0, 0, "EndBalanceDebit", "number", "money", "Money", null, "Visible" },
                    { 569, true, true, (short)12, "System.Decimal", "", "EndBalance", false, false, false, 0, 0, "EndBalanceCredit", "number", "money", "Money", null, "Visible" },
                    { 570, true, true, (short)13, "System.String", "", "", false, false, false, 128, 0, "BranchName", "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 571, true, true, (short)-1, "System.String", "", "", false, false, true, 64, 0, "VoucherReference", "string", "nvarchar", "", null, "AlwaysHidden" },
                    { 560, true, true, (short)3, "System.Decimal", "", "StartBalance", false, false, false, 0, 0, "StartBalanceDebit", "number", "money", "Money", null, "Visible" },
                    { 546, true, true, (short)2, "System.String", "", "", false, false, false, 512, 0, "ProjectName", "string", "nvarchar", "", null, "Visible" },
                    { 545, true, true, (short)1, "System.String", "", "", false, false, false, 512, 0, "ProjectFullCode", "string", "nvarchar", "", null, "Visible" },
                    { 544, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, "RowNo", "number", "int", "", null, "AlwaysVisible" },
                    { 518, true, true, (short)1, "System.String", "", "", false, false, false, 512, 0, "ProjectFullCode", "string", "nvarchar", "", null, "Visible" },
                    { 519, true, true, (short)2, "System.String", "", "", false, false, false, 512, 0, "ProjectName", "string", "nvarchar", "", null, "Visible" },
                    { 520, true, true, (short)3, "System.Decimal", "", "EndBalance", false, false, false, 0, 0, "EndBalanceDebit", "number", "money", "Money", null, "Visible" },
                    { 521, true, true, (short)4, "System.Decimal", "", "EndBalance", false, false, false, 0, 0, "EndBalanceCredit", "number", "money", "Money", null, "Visible" },
                    { 522, true, true, (short)5, "System.String", "", "", false, false, false, 128, 0, "BranchName", "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 524, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, "RowNo", "number", "int", "", null, "AlwaysVisible" },
                    { 1, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, "Id", "number", "int", "", null, "AlwaysHidden" },
                    { 526, true, true, (short)2, "System.String", "", "", false, false, false, 512, 0, "ProjectName", "string", "nvarchar", "", null, "Visible" }
                });

            migrationBuilder.InsertData(
                schema: "Metadata",
                table: "Column",
                columns: new[] { "ColumnID", "AllowFiltering", "AllowSorting", "DisplayIndex", "DotNetType", "Expression", "GroupName", "IsDynamic", "IsFixedLength", "IsNullable", "Length", "MinLength", "Name", "ScriptType", "StorageType", "Type", "ViewID", "Visibility" },
                values: new object[,]
                {
                    { 527, true, true, (short)3, "System.Decimal", "", "Turnover", false, false, false, 0, 0, "TurnoverDebit", "number", "money", "Money", null, "Visible" },
                    { 528, true, true, (short)4, "System.Decimal", "", "Turnover", false, false, false, 0, 0, "TurnoverCredit", "number", "money", "Money", null, "Visible" },
                    { 529, true, true, (short)5, "System.Decimal", "", "EndBalance", false, false, false, 0, 0, "EndBalanceDebit", "number", "money", "Money", null, "Visible" },
                    { 530, true, true, (short)6, "System.Decimal", "", "EndBalance", false, false, false, 0, 0, "EndBalanceCredit", "number", "money", "Money", null, "Visible" },
                    { 531, true, true, (short)7, "System.String", "", "", false, false, false, 128, 0, "BranchName", "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 532, true, true, (short)-1, "System.String", "", "", false, false, true, 64, 0, "VoucherReference", "string", "nvarchar", "", null, "AlwaysHidden" },
                    { 533, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, "RowNo", "number", "int", "", null, "AlwaysVisible" },
                    { 534, true, true, (short)1, "System.String", "", "", false, false, false, 512, 0, "ProjectFullCode", "string", "nvarchar", "", null, "Visible" },
                    { 535, true, true, (short)2, "System.String", "", "", false, false, false, 512, 0, "ProjectName", "string", "nvarchar", "", null, "Visible" },
                    { 536, true, true, (short)3, "System.Decimal", "", "StartBalance", false, false, false, 0, 0, "StartBalanceDebit", "number", "money", "Money", null, "Visible" },
                    { 537, true, true, (short)4, "System.Decimal", "", "StartBalance", false, false, false, 0, 0, "StartBalanceCredit", "number", "money", "Money", null, "Visible" },
                    { 538, true, true, (short)5, "System.Decimal", "", "Turnover", false, false, false, 0, 0, "TurnoverDebit", "number", "money", "Money", null, "Visible" },
                    { 539, true, true, (short)6, "System.Decimal", "", "Turnover", false, false, false, 0, 0, "TurnoverCredit", "number", "money", "Money", null, "Visible" },
                    { 540, true, true, (short)7, "System.Decimal", "", "EndBalance", false, false, false, 0, 0, "EndBalanceDebit", "number", "money", "Money", null, "Visible" },
                    { 541, true, true, (short)8, "System.Decimal", "", "EndBalance", false, false, false, 0, 0, "EndBalanceCredit", "number", "money", "Money", null, "Visible" },
                    { 542, true, true, (short)9, "System.String", "", "", false, false, false, 128, 0, "BranchName", "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 543, true, true, (short)-1, "System.String", "", "", false, false, true, 64, 0, "VoucherReference", "string", "nvarchar", "", null, "AlwaysHidden" },
                    { 516, true, true, (short)-1, "System.String", "", "", false, false, true, 64, 0, "VoucherReference", "string", "nvarchar", "", null, "AlwaysHidden" },
                    { 686, true, true, (short)5, "System.String", "", "", false, false, false, 128, 0, "BranchName", "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 687, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, "Id", "number", "int", "", null, "AlwaysHidden" },
                    { 688, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, "TypeId", "number", "int", "", null, "AlwaysHidden" },
                    { 833, true, true, (short)11, "System.String", "", "", false, false, true, 64, 0, "ModifiedByName", "string", "nvarchar", "", null, "Hidden" },
                    { 834, true, true, (short)12, "System.DateTime", "", "", false, false, true, 0, 0, "ModifiedDate", "Date", "datetime", "Default", null, "Hidden" },
                    { 835, true, true, (short)8, "System.String", "", "", false, false, true, 64, 0, "CreatedByName", "string", "nvarchar", "", null, "Hidden" },
                    { 836, true, true, (short)9, "System.DateTime", "", "", false, false, true, 0, 0, "CreatedDate", "Date", "datetime", "Default", null, "Hidden" },
                    { 837, true, true, (short)10, "System.String", "", "", false, false, true, 64, 0, "ModifiedByName", "string", "nvarchar", "", null, "Hidden" },
                    { 838, true, true, (short)11, "System.DateTime", "", "", false, false, true, 0, 0, "ModifiedDate", "Date", "datetime", "Default", null, "Hidden" },
                    { 839, true, true, (short)9, "System.String", "", "", false, false, true, 64, 0, "CreatedByName", "string", "nvarchar", "", null, "Hidden" },
                    { 840, true, true, (short)10, "System.DateTime", "", "", false, false, true, 0, 0, "CreatedDate", "Date", "datetime", "Default", null, "Hidden" },
                    { 841, true, true, (short)11, "System.String", "", "", false, false, true, 64, 0, "ModifiedByName", "string", "nvarchar", "", null, "Hidden" },
                    { 842, true, true, (short)12, "System.DateTime", "", "", false, false, true, 0, 0, "ModifiedDate", "Date", "datetime", "Default", null, "Hidden" },
                    { 843, true, true, (short)5, "System.String", "", "", false, false, true, 64, 0, "CreatedByName", "string", "nvarchar", "", null, "Hidden" },
                    { 832, true, true, (short)10, "System.DateTime", "", "", false, false, true, 0, 0, "CreatedDate", "Date", "datetime", "Default", null, "Hidden" },
                    { 844, true, true, (short)6, "System.DateTime", "", "", false, false, true, 0, 0, "CreatedDate", "Date", "datetime", "Default", null, "Hidden" },
                    { 846, true, true, (short)8, "System.DateTime", "", "", false, false, true, 0, 0, "ModifiedDate", "Date", "datetime", "Default", null, "Hidden" },
                    { 847, true, true, (short)5, "System.String", "", "", false, false, true, 64, 0, "CreatedByName", "string", "nvarchar", "", null, "Hidden" },
                    { 848, true, true, (short)6, "System.DateTime", "", "", false, false, true, 0, 0, "CreatedDate", "Date", "datetime", "Default", null, "Hidden" },
                    { 849, true, true, (short)7, "System.String", "", "", false, false, true, 64, 0, "ModifiedByName", "string", "nvarchar", "", null, "Hidden" },
                    { 850, true, true, (short)8, "System.DateTime", "", "", false, false, true, 0, 0, "ModifiedDate", "Date", "datetime", "Default", null, "Hidden" },
                    { 851, true, true, (short)5, "System.String", "", "", false, false, true, 64, 0, "CreatedByName", "string", "nvarchar", "", null, "Hidden" },
                    { 852, true, true, (short)6, "System.DateTime", "", "", false, false, true, 0, 0, "CreatedDate", "Date", "datetime", "Default", null, "Hidden" },
                    { 853, true, true, (short)7, "System.String", "", "", false, false, true, 64, 0, "ModifiedByName", "string", "nvarchar", "", null, "Hidden" }
                });

            migrationBuilder.InsertData(
                schema: "Metadata",
                table: "Column",
                columns: new[] { "ColumnID", "AllowFiltering", "AllowSorting", "DisplayIndex", "DotNetType", "Expression", "GroupName", "IsDynamic", "IsFixedLength", "IsNullable", "Length", "MinLength", "Name", "ScriptType", "StorageType", "Type", "ViewID", "Visibility" },
                values: new object[,]
                {
                    { 854, true, true, (short)8, "System.DateTime", "", "", false, false, true, 0, 0, "ModifiedDate", "Date", "datetime", "Default", null, "Hidden" },
                    { 855, true, true, (short)9, "System.String", "", "", false, false, false, 128, 0, "SourceAppName", "string", "nvarchar", "", null, "Hidden" },
                    { 856, true, true, (short)-1, "System.Int32", "", "", false, false, true, 0, 0, "SourceAppId", "number", "int", "", null, "AlwaysHidden" },
                    { 845, true, true, (short)7, "System.String", "", "", false, false, true, 64, 0, "ModifiedByName", "string", "nvarchar", "", null, "Hidden" },
                    { 831, true, true, (short)9, "System.String", "", "", false, false, true, 64, 0, "CreatedByName", "string", "nvarchar", "", null, "Hidden" },
                    { 830, true, true, (short)8, "System.DateTime", "", "", false, false, true, 0, 0, "ModifiedDate", "Date", "datetime", "Default", null, "Hidden" },
                    { 829, true, true, (short)7, "System.String", "", "", false, false, true, 64, 0, "ModifiedByName", "string", "nvarchar", "", null, "Hidden" },
                    { 804, true, true, (short)-1, "System.Int32", "", "", false, false, true, 0, 0, "DetailAccountId", "number", "int", "", null, "AlwaysHidden" },
                    { 805, true, true, (short)-1, "System.Int32", "", "", false, false, true, 0, 0, "CostCenterId", "number", "int", "", null, "AlwaysHidden" },
                    { 806, true, true, (short)-1, "System.Int32", "", "", false, false, true, 0, 0, "ProjectId", "number", "int", "", null, "AlwaysHidden" },
                    { 807, true, true, (short)1, "System.String", "", "", false, false, false, 256, 0, "FullAccount.Account.FullCode", "string", "nvarchar", "", null, "Visible" },
                    { 808, true, true, (short)2, "System.String", "", "", false, false, false, 256, 0, "FullAccount.Account.Name", "string", "nvarchar", "", null, "Visible" },
                    { 809, true, true, (short)3, "System.String", "", "", false, false, false, 256, 0, "FullAccount.DetailAccount.FullCode", "string", "nvarchar", "", null, "Hidden" },
                    { 810, true, true, (short)4, "System.String", "", "", false, false, false, 256, 0, "FullAccount.DetailAccount.Name", "string", "nvarchar", "", null, "Hidden" },
                    { 811, true, true, (short)5, "System.String", "", "", false, false, false, 256, 0, "FullAccount.CostCenter.FullCode", "string", "nvarchar", "", null, "Hidden" },
                    { 812, true, true, (short)6, "System.String", "", "", false, false, false, 256, 0, "FullAccount.CostCenter.Name", "string", "nvarchar", "", null, "Hidden" },
                    { 813, true, true, (short)7, "System.String", "", "", false, false, false, 256, 0, "FullAccount.Project.FullCode", "string", "nvarchar", "", null, "Hidden" },
                    { 814, true, true, (short)8, "System.String", "", "", false, false, false, 256, 0, "FullAccount.Project.Name", "string", "nvarchar", "", null, "Hidden" },
                    { 815, true, true, (short)9, "System.Decimal", "", "", false, false, false, 0, 0, "Amount", "number", "money", "Money", null, "Visible" },
                    { 816, true, true, (short)12, "System.String", "", "", false, false, true, 512, 0, "Remarks", "string", "nvarchar", "", null, "Visible" },
                    { 817, true, true, (short)10, "System.String", "", "", false, false, true, 128, 0, "SourceAppName", "string", "nvarchar", "", null, "Visible" },
                    { 818, true, true, (short)5, "System.String", "", "", false, false, true, 32, 0, "State", "string", "nvarchar", "", null, "Visible" },
                    { 819, true, true, (short)5, "System.String", "", "", false, false, true, 32, 0, "State", "string", "nvarchar", "", null, "Visible" },
                    { 820, true, true, (short)5, "System.String", "", "", false, false, true, 32, 0, "State", "string", "nvarchar", "", null, "Visible" },
                    { 821, true, true, (short)3, "System.String", "", "", false, false, true, 32, 0, "State", "string", "nvarchar", "", null, "Visible" },
                    { 822, true, true, (short)5, "System.String", "", "", false, false, true, 32, 0, "State", "string", "nvarchar", "", null, "Visible" },
                    { 823, true, true, (short)3, "System.String", "", "", false, false, true, 64, 0, "CreatedByName", "string", "nvarchar", "", null, "Hidden" },
                    { 824, true, true, (short)4, "System.DateTime", "", "", false, false, true, 0, 0, "CreatedDate", "Date", "datetime", "Default", null, "Hidden" },
                    { 825, true, true, (short)5, "System.String", "", "", false, false, true, 64, 0, "ModifiedByName", "string", "nvarchar", "", null, "Hidden" },
                    { 826, true, true, (short)6, "System.DateTime", "", "", false, false, true, 0, 0, "ModifiedDate", "Date", "datetime", "Default", null, "Hidden" },
                    { 827, true, true, (short)5, "System.String", "", "", false, false, true, 64, 0, "CreatedByName", "string", "nvarchar", "", null, "Hidden" },
                    { 828, true, true, (short)6, "System.DateTime", "", "", false, false, true, 0, 0, "CreatedDate", "Date", "datetime", "Default", null, "Hidden" },
                    { 857, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, "RowNo", "number", "int", "", null, "AlwaysVisible" },
                    { 803, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, "AccountId", "number", "int", "", null, "AlwaysHidden" },
                    { 858, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, "Id", "number", "int", "", null, "AlwaysHidden" },
                    { 860, true, true, (short)2, "System.DateTime", "", "", false, false, true, 0, 0, "Date", "Date", "datetime", "Default", null, "Visible" },
                    { 100029, true, true, (short)5, "System.String", "", "", false, false, false, 64, 0, "Prefix", "string", "nvarchar", "", null, "Visible" },
                    { 100030, true, true, (short)6, "System.String", "", "", false, false, false, 64, 0, "Suffix", "string", "nvarchar", "", null, "Visible" },
                    { 100031, true, true, (short)7, "System.Boolean", "", "", false, false, false, 0, 0, "IsActive", "boolean", "bit", "", null, "Visible" },
                    { 100032, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, "BranchId", "number", "int", "", null, "AlwaysHidden" },
                    { 100033, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, "Id", "number", "int", "", null, "AlwaysHidden" },
                    { 100034, false, false, (short)0, "System.Int32", "", "", false, false, false, 0, 0, "RowNo", "number", "int", "", null, "AlwaysVisible" }
                });

            migrationBuilder.InsertData(
                schema: "Metadata",
                table: "Column",
                columns: new[] { "ColumnID", "AllowFiltering", "AllowSorting", "DisplayIndex", "DotNetType", "Expression", "GroupName", "IsDynamic", "IsFixedLength", "IsNullable", "Length", "MinLength", "Name", "ScriptType", "StorageType", "Type", "ViewID", "Visibility" },
                values: new object[,]
                {
                    { 100035, true, true, (short)1, "System.String", "", "", false, false, false, 64, 0, "Name", "string", "nvarchar", "", null, "Visible" },
                    { 100036, true, true, (short)2, "System.String", "", "", false, false, false, 64, 0, "EnName", "string", "nvarchar", "", null, "Visible" },
                    { 100037, true, true, (short)3, "System.String", "", "", false, false, false, 1024, 0, "Description", "string", "nvarchar", "", null, "Visible" },
                    { 100038, true, true, (short)4, "System.Int16", "", "", false, false, false, 0, 0, "Type", "number", "smallint", "", null, "Visible" },
                    { 100039, true, true, (short)5, "System.Boolean", "", "", false, false, false, 0, 0, "IsActive", "boolean", "bit", "", null, "Visible" },
                    { 100028, true, true, (short)4, "System.Int16", "", "", false, false, false, 0, 0, "Type", "number", "smallint", "", null, "Visible" },
                    { 100040, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, "BranchId", "number", "int", "", null, "AlwaysHidden" },
                    { 100042, true, true, (short)7, "System.String", "", "", false, false, false, 0, 0, "RowGuid", "string", "nvarchar", "", null, "AlwaysHidden" },
                    { 100043, true, true, (short)8, "System.DateTime", "", "", false, false, false, 0, 0, "ModifiedDate", "Date", "datetime", "Default", null, "Hidden" },
                    { 100044, false, false, (short)0, "System.Int32", "", "", false, false, false, 0, 0, "RowNo", "number", "int", "", null, "AlwaysVisible" },
                    { 100045, true, true, (short)1, "System.String", "", "", false, false, false, 2048, 0, "Name", "string", "nvarchar", "", null, "Visible" },
                    { 100046, true, true, (short)2, "System.String", "", "", false, false, false, 2048, 0, "UniqeName", "string", "nvarchar", "", null, "Visible" },
                    { 100047, true, true, (short)3, "System.String", "", "", false, false, false, 2048, 0, "Description", "string", "nvarchar", "", null, "Visible" },
                    { 100048, true, true, (short)4, "System.Boolean", "", "", false, false, false, 0, 0, "IsActive", "boolean", "bit", "", null, "Visible" },
                    { 100049, true, true, (short)5, "Microsoft.AspNetCore.Http.IFormFile", "", "", false, false, false, 0, 0, "FormFile", "", "", "", null, "Visible" },
                    { 100050, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, "Id", "number", "int", "", null, "AlwaysHidden" },
                    { 100051, true, true, (short)6, "System.Guid", "", "", false, false, false, 0, 0, "RowGuid", "", "", "", null, "AlwaysHidden" },
                    { 100052, true, true, (short)7, "System.DateTime", "", "", false, false, false, 0, 0, "ModifiedDate", "Date", "datetime", "Default", null, "Hidden" },
                    { 100041, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, "Id", "number", "int", "", null, "AlwaysHidden" },
                    { 100027, true, true, (short)3, "System.String", "", "", false, false, false, 1024, 0, "Description", "string", "nvarchar", "", null, "Visible" },
                    { 100026, true, true, (short)2, "System.String", "", "", false, false, false, 64, 0, "EnName", "string", "nvarchar", "", null, "Visible" },
                    { 100025, true, true, (short)1, "System.String", "", "", false, false, false, 64, 0, "Name", "string", "nvarchar", "", null, "Visible" },
                    { 861, true, true, (short)3, "System.String", "", "", false, false, true, 512, 0, "Description", "string", "nvarchar", "", null, "Visible" },
                    { 100001, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, "RowNo", "number", "int", "", null, "AlwaysVisible" },
                    { 100002, true, true, (short)1, "System.String", "", "", false, false, false, 64, 0, "Name", "string", "nvarchar", "", null, "Visible" },
                    { 100003, true, true, (short)2, "System.String", "", "", false, false, false, 64, 0, "EnName", "string", "nvarchar", "", null, "Visible" },
                    { 100004, true, true, (short)3, "System.String", "", "", false, false, false, 1024, 0, "Description", "string", "nvarchar", "", null, "Visible" },
                    { 100005, true, true, (short)4, "System.String", "", "", false, false, false, 64, 0, "SocialLink", "string", "nvarchar", "", null, "Visible" },
                    { 100006, true, true, (short)5, "System.String", "", "", false, false, false, 64, 0, "Website", "string", "nvarchar", "", null, "Visible" },
                    { 100007, true, true, (short)6, "System.String", "", "", false, false, false, 64, 0, "MetaKeyword", "string", "nvarchar", "", null, "Visible" },
                    { 100008, true, true, (short)7, "System.Boolean", "", "", false, false, false, 0, 0, "IsActive", "boolean", "bit", "", null, "Visible" },
                    { 100009, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, "BranchId", "number", "int", "", null, "AlwaysHidden" },
                    { 100010, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, "Id", "number", "int", "", null, "AlwaysHidden" },
                    { 100011, true, true, (short)9, "System.Guid", "", "", false, false, false, 0, 0, "RowGuid", "", "", "", null, "AlwaysHidden" },
                    { 100012, true, true, (short)10, "System.DateTime", "", "", false, false, false, 0, 0, "ModifiedDate", "Date", "datetime", "Default", null, "Hidden" },
                    { 100013, false, false, (short)0, "System.Int32", "", "", false, false, false, 0, 0, "RowNo", "number", "int", "", null, "AlwaysVisible" },
                    { 100014, true, true, (short)1, "System.String", "", "", false, false, false, 64, 0, "Name", "string", "nvarchar", "", null, "Visible" },
                    { 100015, true, true, (short)2, "System.String", "", "", false, false, false, 64, 0, "EnName", "string", "nvarchar", "", null, "Visible" },
                    { 100016, true, true, (short)3, "System.String", "", "", false, false, false, 64, 0, "Description", "string", "nvarchar", "", null, "Visible" },
                    { 100017, true, true, (short)4, "System.String", "", "", false, false, false, 64, 0, "Symbol", "string", "nvarchar", "", null, "Visible" },
                    { 100018, true, true, (short)5, "System.Int16", "", "", false, false, false, 0, 0, "Status", "number", "smallint", "", null, "Visible" },
                    { 100019, true, true, (short)6, "System.Boolean", "", "", false, false, false, 0, 0, "IsActive", "boolean", "bit", "", null, "Visible" }
                });

            migrationBuilder.InsertData(
                schema: "Metadata",
                table: "Column",
                columns: new[] { "ColumnID", "AllowFiltering", "AllowSorting", "DisplayIndex", "DotNetType", "Expression", "GroupName", "IsDynamic", "IsFixedLength", "IsNullable", "Length", "MinLength", "Name", "ScriptType", "StorageType", "Type", "ViewID", "Visibility" },
                values: new object[,]
                {
                    { 100020, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, "BranchId", "number", "int", "", null, "AlwaysHidden" },
                    { 100021, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, "Id", "number", "int", "", null, "AlwaysHidden" },
                    { 100022, true, true, (short)8, "System.Guid", "", "", false, false, false, 0, 0, "RowGuid", "Date", "datetime", "", null, "AlwaysHidden" },
                    { 100023, true, true, (short)9, "System.DateTime", "", "", false, false, false, 0, 0, "ModifiedDate", "Date", "datetime", "Default", null, "Hidden" },
                    { 100024, false, false, (short)0, "System.Int32", "", "", false, false, false, 0, 0, "RowNo", "number", "int", "", null, "AlwaysVisible" },
                    { 859, true, true, (short)1, "System.Int32", "", "", false, false, true, 0, 0, "No", "number", "int", "", null, "Visible" },
                    { 460, true, true, (short)13, "System.String", "", "", false, false, false, 128, 0, "BranchName", "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 802, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, "Id", "number", "int", "", null, "AlwaysHidden" },
                    { 800, true, true, (short)-1, "System.Int32", "", "", false, false, true, 0, 0, "SourceAppId", "number", "int", "", null, "AlwaysHidden" },
                    { 718, true, true, (short)7, "System.Boolean", "", "", false, false, true, 0, 0, "IsArchived", "boolean", "bit", "", null, "Visible" },
                    { 719, true, true, (short)8, "System.Int32", "", "", false, false, false, 0, 0, "PageCount", "number", "int", "", null, "Hidden" },
                    { 720, true, true, (short)-1, "System.Object", "", "", false, false, false, 0, 0, "FullAccount", "object", "(n/a)", "", null, "AlwaysHidden" },
                    { 721, true, true, (short)9, "System.String", "", "", false, false, false, 16, 0, "SayyadNo", "string", "nvarchar", "", null, "Visible" },
                    { 723, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, "BranchId", "number", "int", "", null, "AlwaysHidden" },
                    { 724, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, "Id", "number", "int", "", null, "AlwaysHidden" },
                    { 725, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, "RowNo", "number", "int", "", null, "AlwaysVisible" },
                    { 726, true, true, (short)1, "System.Int64", "", "", false, false, false, 0, 0, "TextNo", "number", "bigint", "", null, "Visible" },
                    { 727, true, true, (short)6, "System.String", "", "", false, false, false, 64, 0, "Name", "string", "nvarchar", "", null, "Visible" },
                    { 728, true, true, (short)7, "System.String", "", "", false, false, false, 32, 0, "BankName", "string", "nvarchar", "", null, "Visible" },
                    { 729, true, true, (short)8, "System.DateTime", "", "", false, false, false, 0, 0, "IssueDate", "Date", "datetime", "Default", null, "Visible" },
                    { 717, true, true, (short)6, "System.String", "", "", false, false, true, 32, 0, "EndNo", "string", "nvarchar", "", null, "Visible" },
                    { 730, true, true, (short)9, "System.String", "", "", false, false, false, 32, 0, "StartNo", "string", "nvarchar", "", null, "Visible" },
                    { 732, true, true, (short)11, "System.String", "", "", false, false, false, 0, 0, "AccountCode", "string", "nvarchar", "", null, "Visible" },
                    { 733, true, true, (short)12, "System.String", "", "", false, false, false, 0, 0, "AccountName", "string", "nvarchar", "", null, "Visible" },
                    { 734, true, true, (short)13, "System.String", "", "", false, false, false, 0, 0, "DetailAccountName", "string", "nvarchar", "", null, "Visible" },
                    { 735, true, true, (short)14, "System.String", "", "", false, false, false, 0, 0, "CostCenterName", "string", "nvarchar", "", null, "Visible" },
                    { 736, true, true, (short)15, "System.String", "", "", false, false, false, 0, 0, "ProjectName", "string", "nvarchar", "", null, "Visible" },
                    { 737, true, true, (short)16, "System.String", "", "", false, false, true, 0, 0, "IsArchivedName", "string", "nvarchar", "", null, "Visible" },
                    { 738, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, "Id", "number", "int", "", null, "AlwaysHidden" },
                    { 739, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, "RowNo", "number", "int", "", null, "AlwaysVisible" },
                    { 740, true, true, (short)1, "System.String", "", "", false, false, false, 64, 0, "Code", "string", "nvarchar", "", null, "Visible" },
                    { 741, true, true, (short)2, "System.String", "", "", false, false, false, 256, 0, "Name", "string", "nvarchar", "", null, "Visible" },
                    { 742, true, true, (short)-1, "System.Int16", "", "", false, false, false, 0, 0, "Type", "number", "smallint", "", null, "AlwaysHidden" },
                    { 731, true, true, (short)10, "System.String", "", "", false, false, false, 32, 0, "EndNo", "string", "nvarchar", "", null, "Visible" },
                    { 716, true, true, (short)5, "System.String", "", "", false, false, false, 32, 0, "StartNo", "string", "nvarchar", "", null, "Visible" },
                    { 715, true, true, (short)4, "System.DateTime", "", "", false, false, false, 0, 0, "IssueDate", "Date", "datetime", "Default", null, "Visible" },
                    { 714, true, true, (short)3, "System.String", "", "", false, false, true, 32, 0, "BankName", "string", "nvarchar", "", null, "Visible" },
                    { 689, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, "FunctionId", "number", "int", "", null, "AlwaysHidden" },
                    { 690, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, "CreatedById", "number", "int", "", null, "AlwaysHidden" },
                    { 691, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, "RowNo", "number", "int", "", null, "AlwaysVisible" },
                    { 692, true, true, (short)1, "System.String", "", "", false, false, false, 128, 0, "Title", "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 693, true, true, (short)2, "System.String", "", "", false, false, false, 64, 0, "TypeName", "string", "nvarchar", "", null, "Visible" }
                });

            migrationBuilder.InsertData(
                schema: "Metadata",
                table: "Column",
                columns: new[] { "ColumnID", "AllowFiltering", "AllowSorting", "DisplayIndex", "DotNetType", "Expression", "GroupName", "IsDynamic", "IsFixedLength", "IsNullable", "Length", "MinLength", "Name", "ScriptType", "StorageType", "Type", "ViewID", "Visibility" },
                values: new object[,]
                {
                    { 694, true, true, (short)3, "System.String", "", "", false, false, false, 64, 0, "FunctionName", "string", "nvarchar", "", null, "Visible" },
                    { 695, true, true, (short)4, "System.String", "", "", false, false, false, 128, 0, "CreatedByFullName", "string", "nvarchar", "", null, "Visible" },
                    { 696, true, true, (short)5, "System.String", "", "", false, false, true, 512, 0, "Description", "string", "nvarchar", "", null, "Visible" },
                    { 697, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, "RowNo", "number", "int", "", null, "AlwaysVisible" },
                    { 698, true, true, (short)1, "System.String", "", "", false, false, false, 64, 0, "SerialNo", "string", "nvarchar", "", null, "Visible" },
                    { 699, true, true, (short)2, "System.String", "", "", false, false, false, 32, 0, "StatusName", "string", "nvarchar", "", null, "Visible" },
                    { 700, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, "Id", "number", "int", "", null, "AlwaysHidden" },
                    { 701, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, "CheckBookPageID", "number", "int", "", null, "AlwaysHidden" },
                    { 702, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, "CheckBookID", "number", "int", "", null, "AlwaysHidden" },
                    { 703, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, "CheckID", "number", "int", "", null, "AlwaysHidden" },
                    { 704, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, "RowNo", "number", "int", "", null, "AlwaysVisible" },
                    { 705, true, true, (short)1, "System.String", "", "", false, false, false, 256, 0, "Name", "string", "nvarchar", "", null, "Visible" },
                    { 706, true, true, (short)2, "System.String", "", "", false, false, true, 256, 0, "Description", "string", "nvarchar", "", null, "Visible" },
                    { 707, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, "FiscalPeriodId", "number", "int", "", null, "AlwaysHidden" },
                    { 708, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, "BranchId", "number", "int", "", null, "AlwaysHidden" },
                    { 709, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, "Id", "number", "int", "", null, "AlwaysHidden" },
                    { 710, true, true, (short)-1, "System.Int16", "", "", false, false, false, 0, 0, "BranchScope", "number", "smallint", "", null, "AlwaysHidden" },
                    { 711, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, "RowNo", "number", "int", "", null, "AlwaysVisible" },
                    { 712, true, true, (short)1, "System.String", "", "", false, false, false, 64, 0, "Name", "string", "nvarchar", "", null, "Visible" },
                    { 713, true, true, (short)2, "System.Int64", "", "", false, false, false, 0, 0, "TextNo", "number", "bigint", "", null, "Visible" },
                    { 743, true, true, (short)4, "System.String", "", "", false, false, true, 512, 0, "Description", "string", "nvarchar", "", null, "Visible" },
                    { 801, true, true, (short)11, "System.String", "", "", false, false, true, 64, 0, "BankOrderNo", "string", "nvarchar", "", null, "Visible" },
                    { 744, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, "FiscalPeriodId", "number", "int", "", null, "AlwaysHidden" },
                    { 746, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, "Id", "number", "int", "", null, "AlwaysHidden" },
                    { 776, true, true, (short)-1, "System.Boolean", "", "", false, false, true, 0, 0, "IsConfirmed", "Boolean", "bit", "", null, "Visible" },
                    { 777, true, true, (short)-1, "System.Boolean", "", "", false, false, true, 0, 0, "IsApproved", "Boolean", "bit", "", null, "Visible" },
                    { 778, true, true, (short)1, "System.Int64", "", "", false, false, false, 0, 0, "TextNo", "number", "bigint", "", null, "Visible" },
                    { 779, true, true, (short)2, "System.String", "", "", false, false, true, 64, 0, "Reference", "string", "nvarchar", "", null, "Visible" },
                    { 780, false, false, (short)0, "System.Int32", "", "", false, false, false, 0, 0, "RowNo", "number", "int", "", null, "AlwaysVisible" },
                    { 781, true, true, (short)-1, "System.Object", "", "", false, false, false, 0, 0, "FullAccount", "object", "(n/a)", "", null, "AlwaysHidden" },
                    { 782, true, true, (short)1, "System.String", "", "", false, false, false, 256, 0, "FullAccount.Account.FullCode", "string", "nvarchar", "", null, "Visible" },
                    { 783, true, true, (short)2, "System.String", "", "", false, false, false, 256, 0, "FullAccount.Account.Name", "string", "nvarchar", "", null, "Visible" },
                    { 784, true, true, (short)3, "System.String", "", "", false, false, false, 256, 0, "FullAccount.DetailAccount.FullCode", "string", "nvarchar", "", null, "Hidden" },
                    { 785, true, true, (short)4, "System.String", "", "", false, false, false, 256, 0, "FullAccount.DetailAccount.Name", "string", "nvarchar", "", null, "Hidden" },
                    { 786, true, true, (short)5, "System.String", "", "", false, false, false, 256, 0, "FullAccount.CostCenter.FullCode", "string", "nvarchar", "", null, "Hidden" },
                    { 775, true, true, (short)-1, "System.Int32", "", "", false, false, true, 0, 0, "CurrencyId", "number", "int", "", null, "Visible" },
                    { 787, true, true, (short)6, "System.String", "", "", false, false, false, 256, 0, "FullAccount.CostCenter.Name", "string", "nvarchar", "", null, "Hidden" },
                    { 789, true, true, (short)8, "System.String", "", "", false, false, false, 256, 0, "FullAccount.Project.Name", "string", "nvarchar", "", null, "Hidden" },
                    { 790, true, true, (short)2, "System.String", "", "", false, false, false, 16, 0, "SayyadStartNo", "string", "nvarchar", "", null, "Visible" },
                    { 791, true, true, (short)3, "System.String", "", "", false, false, false, 32, 0, "SeriesNo", "string", "nvarchar", "", null, "Visible" },
                    { 792, true, true, (short)9, "System.Decimal", "", "", false, false, false, 0, 0, "Amount", "number", "money", "Money", null, "Visible" },
                    { 793, true, true, (short)10, "System.String", "", "", false, false, true, 512, 0, "Remarks", "string", "nvarchar", "", null, "Visible" }
                });

            migrationBuilder.InsertData(
                schema: "Metadata",
                table: "Column",
                columns: new[] { "ColumnID", "AllowFiltering", "AllowSorting", "DisplayIndex", "DotNetType", "Expression", "GroupName", "IsDynamic", "IsFixedLength", "IsNullable", "Length", "MinLength", "Name", "ScriptType", "StorageType", "Type", "ViewID", "Visibility" },
                values: new object[,]
                {
                    { 794, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, "PayReceiveId", "number", "int", "", null, "AlwaysHidden" },
                    { 795, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, "Id", "number", "int", "", null, "AlwaysHidden" },
                    { 796, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, "RowNo", "number", "int", "", null, "AlwaysVisible" },
                    { 797, true, true, (short)-1, "System.Object", "", "", false, false, false, 0, 0, "FullAccount", "Object", "int", "", null, "AlwaysHidden" },
                    { 798, true, true, (short)-1, "System.Boolean", "", "", false, false, false, 0, 0, "IsBank", "boolean", "bit", "", null, "AlwaysHidden" },
                    { 799, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, "PayReceiveId", "number", "int", "", null, "AlwaysHidden" },
                    { 788, true, true, (short)7, "System.String", "", "", false, false, false, 256, 0, "FullAccount.Project.FullCode", "string", "nvarchar", "", null, "Hidden" },
                    { 774, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, "Id", "number", "int", "", null, "AlwaysHidden" },
                    { 773, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, "BranchId", "number", "int", "", null, "AlwaysHidden" },
                    { 772, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, "FiscalPeriodId", "number", "int", "", null, "AlwaysHidden" },
                    { 747, true, true, (short)9, "System.String", "", "", false, false, false, 32, 0, "SeriesNo", "string", "nvarchar", "", null, "Visible" },
                    { 748, true, true, (short)10, "System.String", "", "", false, false, false, 16, 0, "SayyadStartNo", "string", "nvarchar", "", null, "Visible" },
                    { 749, true, true, (short)3, "System.String", "", "", false, false, false, 16, 0, "SayyadNo", "string", "nvarchar", "", null, "Visible" },
                    { 750, true, true, (short)3, "System.String", "", "", false, false, true, 64, 0, "TypeName", "string", "nvarchar", "", null, "Visible" },
                    { 751, true, true, (short)-1, "System.Int16", "", "", false, false, false, 0, 0, "BranchScope", "number", "smallint", "", null, "AlwaysHidden" },
                    { 752, true, true, (short)3, "System.DateTime", "", "", false, false, false, 0, 0, "Date", "Date", "datetime", "Default", null, "Visible" },
                    { 753, true, true, (short)4, "System.String", "", "", false, false, false, 64, 0, "IssuedByName", "string", "nvarchar", "", null, "Visible" },
                    { 754, true, true, (short)5, "System.String", "", "", false, false, true, 64, 0, "ConfirmedByName", "string", "nvarchar", "", null, "Visible" },
                    { 755, true, true, (short)6, "System.String", "", "", false, false, true, 64, 0, "ApprovedByName", "string", "nvarchar", "", null, "Visible" },
                    { 756, true, true, (short)7, "System.Decimal", "", "", false, false, true, 0, 0, "CurrencyRate", "number", "money", "Currency", null, "Visible" },
                    { 757, true, true, (short)8, "System.String", "", "", false, false, true, 1024, 0, "Description", "string", "nvarchar", "", null, "Visible" },
                    { 758, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, "FiscalPeriodId", "number", "int", "", null, "AlwaysHidden" },
                    { 759, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, "BranchId", "number", "int", "", null, "AlwaysHidden" },
                    { 760, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, "Id", "number", "int", "", null, "AlwaysHidden" },
                    { 761, true, true, (short)-1, "System.Int32", "", "", false, false, true, 0, 0, "CurrencyId", "number", "int", "", null, "Visible" },
                    { 762, true, true, (short)-1, "System.Boolean", "", "", false, false, true, 0, 0, "IsConfirmed", "Boolean", "bit", "", null, "Visible" },
                    { 763, true, true, (short)-1, "System.Boolean", "", "", false, false, true, 0, 0, "IsApproved", "Boolean", "bit", "", null, "Visible" },
                    { 764, true, true, (short)1, "System.Int64", "", "", false, false, false, 0, 0, "TextNo", "number", "bigint", "", null, "Visible" },
                    { 765, true, true, (short)2, "System.String", "", "", false, false, true, 64, 0, "Reference", "string", "nvarchar", "", null, "Visible" },
                    { 766, true, true, (short)3, "System.DateTime", "", "", false, false, false, 0, 0, "Date", "Date", "datetime", "Default", null, "Visible" },
                    { 767, true, true, (short)4, "System.String", "", "", false, false, false, 64, 0, "IssuedByName", "string", "nvarchar", "", null, "Visible" },
                    { 768, true, true, (short)5, "System.String", "", "", false, false, true, 64, 0, "ConfirmedByName", "string", "nvarchar", "", null, "Visible" },
                    { 769, true, true, (short)6, "System.String", "", "", false, false, true, 64, 0, "ApprovedByName", "string", "nvarchar", "", null, "Visible" },
                    { 770, true, true, (short)7, "System.Decimal", "", "", false, false, true, 0, 0, "CurrencyRate", "number", "money", "Currency", null, "Visible" },
                    { 771, true, true, (short)8, "System.String", "", "", false, false, true, 1024, 0, "Description", "string", "nvarchar", "", null, "Visible" },
                    { 745, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, "BranchId", "number", "int", "", null, "AlwaysHidden" },
                    { 459, true, true, (short)12, "System.Decimal", "", "EndBalance", false, false, false, 0, 0, "EndBalanceCredit", "number", "money", "Money", null, "Visible" },
                    { 525, true, true, (short)1, "System.String", "", "", false, false, false, 512, 0, "ProjectFullCode", "string", "nvarchar", "", null, "Visible" },
                    { 457, true, true, (short)10, "System.Decimal", "", "Corrections", false, false, false, 0, 0, "CorrectionsCredit", "number", "money", "Money", null, "Visible" },
                    { 147, true, true, (short)14, "System.String", "", "", false, false, false, 128, 0, "BranchName", "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 148, true, true, (short)15, "System.String", "", "", false, false, true, 128, 0, "Mark", "string", "nvarchar", "", null, "Visible" },
                    { 149, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, "RowNo", "number", "int", "", null, "AlwaysVisible" }
                });

            migrationBuilder.InsertData(
                schema: "Metadata",
                table: "Column",
                columns: new[] { "ColumnID", "AllowFiltering", "AllowSorting", "DisplayIndex", "DotNetType", "Expression", "GroupName", "IsDynamic", "IsFixedLength", "IsNullable", "Length", "MinLength", "Name", "ScriptType", "StorageType", "Type", "ViewID", "Visibility" },
                values: new object[,]
                {
                    { 150, true, true, (short)1, "System.Date", "", "", false, false, false, 0, 0, "VoucherDate", "Date", "datetime", "Default", null, "Visible" },
                    { 151, true, true, (short)2, "System.Int32", "", "", false, false, false, 0, 0, "VoucherNo", "number", "int", "", null, "Visible" },
                    { 152, true, true, (short)3, "System.String", "", "", false, false, false, 512, 0, "AccountFullCode", "string", "nvarchar", "", null, "Visible" },
                    { 153, true, true, (short)4, "System.String", "", "", false, false, false, 512, 0, "AccountName", "string", "nvarchar", "", null, "Visible" },
                    { 154, true, true, (short)5, "System.String", "", "", false, false, false, 512, 0, "Description", "string", "nvarchar", "", null, "Visible" },
                    { 155, true, true, (short)6, "System.Decimal", "", "", false, false, false, 0, 0, "Debit", "number", "money", "Money", null, "Visible" },
                    { 156, true, true, (short)7, "System.Decimal", "", "", false, false, false, 0, 0, "Credit", "number", "money", "Money", null, "Visible" },
                    { 157, true, true, (short)8, "System.String", "", "", false, false, false, 128, 0, "BranchName", "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 146, true, true, (short)13, "System.Decimal", "", "", false, false, false, 0, 0, "Credit", "number", "money", "Money", null, "Visible" },
                    { 158, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, "RowNo", "number", "int", "", null, "AlwaysVisible" },
                    { 160, true, true, (short)2, "System.Int32", "", "", false, false, false, 0, 0, "VoucherNo", "number", "int", "", null, "Visible" },
                    { 161, true, true, (short)3, "System.String", "", "", false, false, false, 512, 0, "AccountFullCode", "string", "nvarchar", "", null, "Visible" },
                    { 162, true, true, (short)4, "System.String", "", "", false, false, false, 512, 0, "AccountName", "string", "nvarchar", "", null, "Visible" },
                    { 163, true, true, (short)5, "System.String", "", "", false, false, false, 512, 0, "Description", "string", "nvarchar", "", null, "Visible" },
                    { 458, true, true, (short)11, "System.Decimal", "", "EndBalance", false, false, false, 0, 0, "EndBalanceDebit", "number", "money", "Money", null, "Visible" },
                    { 165, true, true, (short)7, "System.Decimal", "", "", false, false, false, 0, 0, "Credit", "number", "money", "Money", null, "Visible" },
                    { 166, true, true, (short)8, "System.String", "", "", false, false, false, 128, 0, "BranchName", "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 167, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, "RowNo", "number", "int", "", null, "AlwaysVisible" },
                    { 168, true, true, (short)1, "System.String", "", "", false, false, false, 512, 0, "AccountFullCode", "string", "nvarchar", "", null, "Visible" },
                    { 169, true, true, (short)2, "System.String", "", "", false, false, false, 512, 0, "AccountName", "string", "nvarchar", "", null, "Visible" },
                    { 170, true, true, (short)3, "System.String", "", "", false, false, false, 512, 0, "Description", "string", "nvarchar", "", null, "Visible" },
                    { 159, true, true, (short)1, "System.Date", "", "", false, false, false, 0, 0, "VoucherDate", "Date", "datetime", "Default", null, "Visible" },
                    { 145, true, true, (short)12, "System.Decimal", "", "", false, false, false, 0, 0, "Debit", "number", "money", "Money", null, "Visible" },
                    { 144, true, true, (short)11, "System.String", "", "", false, false, false, 512, 0, "Description", "string", "nvarchar", "", null, "Visible" },
                    { 143, true, true, (short)10, "System.String", "", "", false, false, false, 512, 0, "ProjectName", "string", "nvarchar", "", null, "Visible" },
                    { 118, true, true, (short)11, "System.DateTime", "", "", false, false, false, 0, 0, "EntityDate", "Date", "datetime", "Default", null, "Visible" },
                    { 119, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, "Id", "number", "int", "", null, "AlwaysHidden" },
                    { 120, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, "RowNo", "number", "int", "", null, "AlwaysVisible" },
                    { 121, true, true, (short)1, "System.String", "", "", false, false, false, 512, 0, "Name", "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 122, true, true, (short)2, "System.String", "", "", false, false, false, 256, 0, "FullCode", "string", "nvarchar", "", null, "Visible" },
                    { 123, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, "RowNo", "number", "int", "", null, "AlwaysVisible" },
                    { 124, true, true, (short)1, "System.Date", "", "", false, false, false, 0, 0, "VoucherDate", "Date", "datetime", "Default", null, "Visible" },
                    { 125, true, true, (short)2, "System.Int32", "", "", false, false, false, 0, 0, "VoucherNo", "number", "int", "", null, "Visible" },
                    { 126, true, true, (short)3, "System.String", "", "", false, false, false, 512, 0, "AccountFullCode", "string", "nvarchar", "", null, "Visible" },
                    { 127, true, true, (short)4, "System.String", "", "", false, false, false, 512, 0, "AccountName", "string", "nvarchar", "", null, "Visible" },
                    { 128, true, true, (short)5, "System.String", "", "", false, false, false, 512, 0, "Description", "string", "nvarchar", "", null, "Visible" },
                    { 129, true, true, (short)6, "System.Decimal", "", "", false, false, false, 0, 0, "Debit", "number", "money", "Money", null, "Visible" },
                    { 130, true, true, (short)7, "System.Decimal", "", "", false, false, false, 0, 0, "Credit", "number", "money", "Money", null, "Visible" },
                    { 131, true, true, (short)8, "System.String", "", "", false, false, false, 128, 0, "BranchName", "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 132, true, true, (short)9, "System.String", "", "", false, false, true, 128, 0, "Mark", "string", "nvarchar", "", null, "Visible" },
                    { 133, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, "RowNo", "number", "int", "", null, "AlwaysVisible" },
                    { 134, true, true, (short)1, "System.Date", "", "", false, false, false, 0, 0, "VoucherDate", "Date", "datetime", "Default", null, "Visible" }
                });

            migrationBuilder.InsertData(
                schema: "Metadata",
                table: "Column",
                columns: new[] { "ColumnID", "AllowFiltering", "AllowSorting", "DisplayIndex", "DotNetType", "Expression", "GroupName", "IsDynamic", "IsFixedLength", "IsNullable", "Length", "MinLength", "Name", "ScriptType", "StorageType", "Type", "ViewID", "Visibility" },
                values: new object[,]
                {
                    { 135, true, true, (short)2, "System.Int32", "", "", false, false, false, 0, 0, "VoucherNo", "number", "int", "", null, "Visible" },
                    { 136, true, true, (short)3, "System.String", "", "", false, false, false, 512, 0, "AccountFullCode", "string", "nvarchar", "", null, "Visible" },
                    { 137, true, true, (short)4, "System.String", "", "", false, false, false, 512, 0, "AccountName", "string", "nvarchar", "", null, "Visible" },
                    { 138, true, true, (short)5, "System.String", "", "", false, false, false, 512, 0, "DetailAccountFullCode", "string", "nvarchar", "", null, "Visible" },
                    { 139, true, true, (short)6, "System.String", "", "", false, false, false, 512, 0, "DetailAccountName", "string", "nvarchar", "", null, "Visible" },
                    { 140, true, true, (short)7, "System.String", "", "", false, false, false, 512, 0, "CostCenterFullCode", "string", "nvarchar", "", null, "Visible" },
                    { 141, true, true, (short)8, "System.String", "", "", false, false, false, 512, 0, "CostCenterName", "string", "nvarchar", "", null, "Visible" },
                    { 142, true, true, (short)9, "System.String", "", "", false, false, false, 512, 0, "ProjectFullCode", "string", "nvarchar", "", null, "Visible" },
                    { 171, true, true, (short)4, "System.Decimal", "", "", false, false, false, 0, 0, "Debit", "number", "money", "Money", null, "Visible" },
                    { 117, true, true, (short)10, "System.Int32", "", "", false, false, false, 0, 0, "EntityNo", "number", "int", "", null, "Visible" },
                    { 172, true, true, (short)5, "System.Decimal", "", "", false, false, false, 0, 0, "Credit", "number", "money", "Money", null, "Visible" },
                    { 174, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, "RowNo", "number", "int", "", null, "AlwaysVisible" },
                    { 204, true, true, (short)4, "System.String", "", "", false, false, false, 512, 0, "AccountName", "string", "nvarchar", "", null, "Visible" },
                    { 205, true, true, (short)5, "System.String", "", "", false, false, false, 512, 0, "DetailAccountFullCode", "string", "nvarchar", "", null, "Visible" },
                    { 206, true, true, (short)6, "System.String", "", "", false, false, false, 512, 0, "DetailAccountName", "string", "nvarchar", "", null, "Visible" },
                    { 207, true, true, (short)7, "System.String", "", "", false, false, false, 512, 0, "CostCenterFullCode", "string", "nvarchar", "", null, "Visible" },
                    { 208, true, true, (short)8, "System.String", "", "", false, false, false, 512, 0, "CostCenterName", "string", "nvarchar", "", null, "Visible" },
                    { 209, true, true, (short)9, "System.String", "", "", false, false, false, 512, 0, "ProjectFullCode", "string", "nvarchar", "", null, "Visible" },
                    { 210, true, true, (short)10, "System.String", "", "", false, false, false, 512, 0, "ProjectName", "string", "nvarchar", "", null, "Visible" },
                    { 211, true, true, (short)11, "System.String", "", "", false, false, false, 512, 0, "Description", "string", "nvarchar", "", null, "Visible" },
                    { 212, true, true, (short)12, "System.Decimal", "", "", false, false, false, 0, 0, "Debit", "number", "money", "Money", null, "Visible" },
                    { 213, true, true, (short)13, "System.Decimal", "", "", false, false, false, 0, 0, "Credit", "number", "money", "Money", null, "Visible" },
                    { 214, true, true, (short)14, "System.String", "", "", false, false, false, 128, 0, "BranchName", "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 203, true, true, (short)3, "System.String", "", "", false, false, false, 512, 0, "AccountFullCode", "string", "nvarchar", "", null, "Visible" },
                    { 215, true, true, (short)15, "System.String", "", "", false, false, true, 128, 0, "Mark", "string", "nvarchar", "", null, "Visible" },
                    { 217, true, true, (short)1, "System.Date", "", "", false, false, false, 0, 0, "VoucherDate", "Date", "datetime", "Default", null, "Visible" },
                    { 218, true, true, (short)2, "System.Int32", "", "", false, false, false, 0, 0, "VoucherNo", "number", "int", "", null, "Visible" },
                    { 219, true, true, (short)3, "System.String", "", "", false, false, false, 512, 0, "AccountFullCode", "string", "nvarchar", "", null, "Visible" },
                    { 220, true, true, (short)4, "System.String", "", "", false, false, false, 512, 0, "AccountName", "string", "nvarchar", "", null, "Visible" },
                    { 221, true, true, (short)5, "System.String", "", "", false, false, false, 512, 0, "Description", "string", "nvarchar", "", null, "Visible" },
                    { 222, true, true, (short)6, "System.Decimal", "", "", false, false, false, 0, 0, "Debit", "number", "money", "Money", null, "Visible" },
                    { 223, true, true, (short)7, "System.Decimal", "", "", false, false, false, 0, 0, "Credit", "number", "money", "Money", null, "Visible" },
                    { 224, true, true, (short)8, "System.String", "", "", false, false, false, 128, 0, "BranchName", "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 225, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, "RowNo", "number", "int", "", null, "AlwaysVisible" },
                    { 226, true, true, (short)1, "System.Date", "", "", false, false, false, 0, 0, "VoucherDate", "Date", "datetime", "Default", null, "Visible" },
                    { 227, true, true, (short)2, "System.Int32", "", "", false, false, false, 0, 0, "VoucherNo", "number", "int", "", null, "Visible" },
                    { 216, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, "RowNo", "number", "int", "", null, "AlwaysVisible" },
                    { 202, true, true, (short)2, "System.Int32", "", "", false, false, false, 0, 0, "VoucherNo", "number", "int", "", null, "Visible" },
                    { 201, true, true, (short)1, "System.Date", "", "", false, false, false, 0, 0, "VoucherDate", "Date", "datetime", "Default", null, "Visible" },
                    { 200, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, "RowNo", "number", "int", "", null, "AlwaysVisible" },
                    { 175, true, true, (short)1, "System.Date", "", "", false, false, false, 0, 0, "VoucherDate", "Date", "datetime", "Default", null, "Visible" },
                    { 176, true, true, (short)2, "System.String", "", "", false, false, false, 512, 0, "AccountFullCode", "string", "nvarchar", "", null, "Visible" }
                });

            migrationBuilder.InsertData(
                schema: "Metadata",
                table: "Column",
                columns: new[] { "ColumnID", "AllowFiltering", "AllowSorting", "DisplayIndex", "DotNetType", "Expression", "GroupName", "IsDynamic", "IsFixedLength", "IsNullable", "Length", "MinLength", "Name", "ScriptType", "StorageType", "Type", "ViewID", "Visibility" },
                values: new object[,]
                {
                    { 177, true, true, (short)3, "System.String", "", "", false, false, false, 512, 0, "AccountName", "string", "nvarchar", "", null, "Visible" },
                    { 178, true, true, (short)4, "System.String", "", "", false, false, false, 512, 0, "Description", "string", "nvarchar", "", null, "Visible" },
                    { 179, true, true, (short)5, "System.Decimal", "", "", false, false, false, 0, 0, "Debit", "number", "money", "Money", null, "Visible" },
                    { 180, true, true, (short)6, "System.Decimal", "", "", false, false, false, 0, 0, "Credit", "number", "money", "Money", null, "Visible" },
                    { 181, true, true, (short)7, "System.String", "", "", false, false, false, 128, 0, "BranchName", "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 182, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, "RowNo", "number", "int", "", null, "AlwaysVisible" },
                    { 183, true, true, (short)1, "System.Date", "", "", false, false, false, 0, 0, "VoucherDate", "Date", "datetime", "Default", null, "Visible" },
                    { 184, true, true, (short)2, "System.String", "", "", false, false, false, 512, 0, "AccountFullCode", "string", "nvarchar", "", null, "Visible" },
                    { 185, true, true, (short)3, "System.String", "", "", false, false, false, 512, 0, "AccountName", "string", "nvarchar", "", null, "Visible" },
                    { 186, true, true, (short)4, "System.String", "", "", false, false, false, 512, 0, "Description", "string", "nvarchar", "", null, "Visible" },
                    { 187, true, true, (short)5, "System.Decimal", "", "", false, false, false, 0, 0, "Debit", "number", "money", "Money", null, "Visible" },
                    { 188, true, true, (short)6, "System.Decimal", "", "", false, false, false, 0, 0, "Credit", "number", "money", "Money", null, "Visible" },
                    { 189, true, true, (short)7, "System.String", "", "", false, false, false, 128, 0, "BranchName", "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 190, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, "RowNo", "number", "int", "", null, "AlwaysVisible" },
                    { 191, true, true, (short)1, "System.Date", "", "", false, false, false, 0, 0, "VoucherDate", "Date", "datetime", "Default", null, "Visible" },
                    { 192, true, true, (short)2, "System.Int32", "", "", false, false, false, 0, 0, "VoucherNo", "number", "int", "", null, "Visible" },
                    { 193, true, true, (short)3, "System.String", "", "", false, false, false, 512, 0, "AccountFullCode", "string", "nvarchar", "", null, "Visible" },
                    { 194, true, true, (short)4, "System.String", "", "", false, false, false, 512, 0, "AccountName", "string", "nvarchar", "", null, "Visible" },
                    { 195, true, true, (short)5, "System.String", "", "", false, false, false, 512, 0, "Description", "string", "nvarchar", "", null, "Visible" },
                    { 196, true, true, (short)6, "System.Decimal", "", "", false, false, false, 0, 0, "Debit", "number", "money", "Money", null, "Visible" },
                    { 197, true, true, (short)7, "System.Decimal", "", "", false, false, false, 0, 0, "Credit", "number", "money", "Money", null, "Visible" },
                    { 198, true, true, (short)8, "System.String", "", "", false, false, false, 128, 0, "BranchName", "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 199, true, true, (short)9, "System.String", "", "", false, false, true, 128, 0, "Mark", "string", "nvarchar", "", null, "Visible" },
                    { 173, true, true, (short)6, "System.String", "", "", false, false, false, 128, 0, "BranchName", "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 228, true, true, (short)3, "System.String", "", "", false, false, false, 512, 0, "AccountFullCode", "string", "nvarchar", "", null, "Visible" },
                    { 116, true, true, (short)9, "System.String", "", "", false, false, false, 64, 0, "EntityDescription", "string", "nvarchar", "", null, "Visible" },
                    { 114, true, true, (short)7, "System.String", "", "", false, false, false, 64, 0, "EntityCode", "string", "nvarchar", "", null, "Visible" },
                    { 32, true, true, (short)-1, "System.Decimal", "", "", false, false, true, 0, 0, "CurrencyRate", "number", "money", "Money", null, "AlwaysHidden" },
                    { 33, true, true, (short)-1, "System.Int16", "", "", false, false, false, 0, 0, "TypeId", "number", "smallint", "", null, "AlwaysHidden" },
                    { 34, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, "RowNo", "number", "int", "", null, "AlwaysVisible" },
                    { 35, true, true, (short)1, "System.String", "", "", false, false, false, 256, 0, "FullAccount.Account.FullCode", "string", "nvarchar", "", null, "Visible" },
                    { 36, true, true, (short)2, "System.String", "", "", false, false, false, 128, 0, "FullAccount.Account.Name", "string", "nvarchar", "", null, "Visible" },
                    { 37, true, true, (short)3, "System.String", "", "", false, false, false, 128, 0, "FullAccount.DetailAccount.FullCode", "string", "nvarchar", "", null, "Hidden" },
                    { 38, true, true, (short)4, "System.String", "", "", false, false, false, 128, 0, "FullAccount.DetailAccount.Name", "string", "nvarchar", "", null, "Hidden" },
                    { 39, true, true, (short)5, "System.String", "", "", false, false, false, 128, 0, "FullAccount.CostCenter.FullCode", "string", "nvarchar", "", null, "Hidden" },
                    { 40, true, true, (short)6, "System.String", "", "", false, false, false, 128, 0, "FullAccount.CostCenter.Name", "string", "nvarchar", "", null, "Hidden" },
                    { 41, true, true, (short)7, "System.String", "", "", false, false, false, 128, 0, "FullAccount.Project.FullCode", "string", "nvarchar", "", null, "Hidden" },
                    { 42, true, true, (short)8, "System.String", "", "", false, false, false, 128, 0, "FullAccount.Project.Name", "string", "nvarchar", "", null, "Hidden" },
                    { 31, true, true, (short)-1, "System.Int32", "", "", false, false, true, 0, 0, "FullAccount.Project.Id", "number", "int", "", null, "AlwaysHidden" },
                    { 43, true, true, (short)11, "System.String", "", "", false, false, true, 512, 0, "Description", "string", "nvarchar", "", null, "Visible" },
                    { 45, true, true, (short)13, "System.Decimal", "", "", false, false, false, 0, 0, "Credit", "number", "money", "Money", null, "Visible" },
                    { 46, true, true, (short)14, "System.String", "", "", false, false, false, 64, 0, "CurrencyName", "string", "nvarchar", "", null, "AlwaysVisible" }
                });

            migrationBuilder.InsertData(
                schema: "Metadata",
                table: "Column",
                columns: new[] { "ColumnID", "AllowFiltering", "AllowSorting", "DisplayIndex", "DotNetType", "Expression", "GroupName", "IsDynamic", "IsFixedLength", "IsNullable", "Length", "MinLength", "Name", "ScriptType", "StorageType", "Type", "ViewID", "Visibility" },
                values: new object[,]
                {
                    { 47, true, true, (short)15, "System.Decimal", "", "", false, false, true, 0, 0, "CurrencyValue", "number", "money", "Money", null, "AlwaysVisible" },
                    { 48, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, "Id", "number", "int", "", null, "AlwaysHidden" },
                    { 49, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, "RowNo", "number", "int", "", null, "AlwaysVisible" },
                    { 50, true, true, (short)1, "System.String", "", "", false, false, false, 64, 0, "UserName", "string", "nvarchar(64)", "", null, "AlwaysVisible" },
                    { 51, true, true, (short)2, "System.DateTime", "", "", false, false, true, 0, 0, "LastLoginDate", "DateTime", "datetime", "Default", null, "Visible" },
                    { 52, true, true, (short)3, "System.Boolean", "", "", false, false, false, 0, 0, "IsEnabled", "boolean", "bit", "", null, "Visible" },
                    { 53, true, true, (short)4, "System.String", "Person.FullName", "", false, false, false, 64, 0, "PersonFullName", "string", "nvarchar(64)", "", null, "Visible" },
                    { 55, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, "RowNo", "number", "int", "", null, "AlwaysVisible" },
                    { 56, true, true, (short)1, "System.String", "", "", false, false, false, 64, 0, "Name", "string", "nvarchar(64)", "", null, "AlwaysVisible" },
                    { 44, true, true, (short)12, "System.Decimal", "", "", false, false, false, 0, 0, "Debit", "number", "money", "Money", null, "Visible" },
                    { 30, true, true, (short)-1, "System.Int32", "", "", false, false, true, 0, 0, "FullAccount.CostCenter.Id", "number", "int", "", null, "AlwaysHidden" },
                    { 29, true, true, (short)-1, "System.Int32", "", "", false, false, true, 0, 0, "FullAccount.DetailAccount.Id", "number", "int", "", null, "AlwaysHidden" },
                    { 28, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, "FullAccount.Account.Id", "number", "int", "", null, "AlwaysHidden" },
                    { 3, true, true, (short)-1, "System.Int32", "", "", false, false, true, 0, 0, "GroupId", "number", "int", "", null, "AlwaysHidden" },
                    { 4, true, true, (short)-1, "System.Int32", "", "", false, false, true, 0, 0, "CurrencyId", "number", "int", "", null, "AlwaysHidden" },
                    { 5, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, "RowNo", "number", "int", "", null, "AlwaysVisible" },
                    { 6, true, true, (short)1, "System.String", "", "", false, false, false, 16, 0, "Code", "string", "nvarchar", "", null, "Visible" },
                    { 7, true, true, (short)2, "System.String", "", "", false, false, false, 256, 0, "FullCode", "string", "nvarchar", "", null, "Visible" },
                    { 8, true, true, (short)3, "System.String", "", "", false, false, false, 512, 0, "Name", "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 9, true, true, (short)-1, "System.Int16", "", "", false, false, false, 0, 0, "Level", "number", "smallint", "", null, "AlwaysHidden" },
                    { 10, true, true, (short)4, "System.String", "", "", false, false, true, 512, 0, "Description", "string", "nvarchar", "", null, "Visible" },
                    { 11, true, true, (short)5, "System.String", "", "", false, false, true, 32, 0, "State", "string", "nvarchar", "", null, "Visible" },
                    { 12, true, true, (short)6, "System.Boolean", "", "", false, false, true, 0, 0, "IsCurrencyAdjustable", "boolean", "bit", "", null, "Hidden" },
                    { 13, true, true, (short)7, "System.String", "", "", false, false, true, 32, 0, "TurnoverMode", "string", "nvarchar", "", null, "Hidden" },
                    { 14, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, "Id", "number", "int", "", null, "AlwaysHidden" },
                    { 15, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, "StatusId", "number", "int", "", null, "AlwaysHidden" },
                    { 16, true, true, (short)-1, "System.Int16", "", "", false, false, false, 0, 0, "Type", "number", "smallint", "", null, "AlwaysHidden" },
                    { 17, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, "RowNo", "number", "int", "", null, "AlwaysVisible" },
                    { 18, true, true, (short)1, "System.Int32", "", "", false, false, false, 0, 0, "No", "number", "int", "", null, "AlwaysVisible" },
                    { 19, true, true, (short)2, "System.Date", "", "", false, false, false, 0, 0, "Date", "Date", "datetime", "Default", null, "Visible" },
                    { 20, true, true, (short)3, "System.String", "", "", false, false, true, 512, 0, "Description", "string", "nvarchar", "", null, "Visible" },
                    { 21, true, true, (short)4, "System.String", "", "", false, false, true, 64, 0, "StatusName", "string", "nvarchar", "", null, "Visible" },
                    { 22, true, true, (short)5, "System.String", "", "", false, false, true, 64, 0, "Reference", "string", "nvarchar", "", null, "Visible" },
                    { 23, true, true, (short)6, "System.String", "", "", false, false, true, 64, 0, "Association", "string", "nvarchar", "", null, "Visible" },
                    { 24, true, true, (short)7, "System.Int32", "", "", false, false, false, 0, 0, "DailyNo", "number", "int", "", null, "Visible" },
                    { 25, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, "Id", "number", "int", "", null, "AlwaysHidden" },
                    { 26, true, true, (short)-1, "System.Int32", "", "", false, false, true, 0, 0, "CurrencyId", "number", "int", "", null, "AlwaysHidden" },
                    { 27, true, true, (short)-1, "System.Object", "", "", false, false, false, 0, 0, "FullAccount", "object", "(n/a)", "", null, "AlwaysHidden" },
                    { 57, true, true, (short)2, "System.String", "", "", false, false, true, 512, 0, "Description", "string", "nvarchar(512)", "", null, "Visible" },
                    { 115, true, true, (short)8, "System.String", "", "", false, false, false, 64, 0, "EntityName", "string", "nvarchar", "", null, "Visible" },
                    { 58, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, "Id", "number", "int", "", null, "AlwaysHidden" },
                    { 60, true, false, (short)-1, "System.Int16", "", "", false, false, false, 0, 0, "BranchScope", "number", "smallint", "", null, "AlwaysHidden" }
                });

            migrationBuilder.InsertData(
                schema: "Metadata",
                table: "Column",
                columns: new[] { "ColumnID", "AllowFiltering", "AllowSorting", "DisplayIndex", "DotNetType", "Expression", "GroupName", "IsDynamic", "IsFixedLength", "IsNullable", "Length", "MinLength", "Name", "ScriptType", "StorageType", "Type", "ViewID", "Visibility" },
                values: new object[,]
                {
                    { 90, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, "RowNo", "number", "int", "", null, "AlwaysVisible" },
                    { 91, true, true, (short)1, "System.String", "", "", false, false, false, 128, 0, "Name", "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 92, true, true, (short)2, "System.String", "", "", false, false, true, 512, 0, "Description", "string", "nvarchar", "", null, "Visible" },
                    { 93, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, "Id", "number", "int", "", null, "AlwaysHidden" },
                    { 94, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, "RowNo", "number", "int", "", null, "AlwaysVisible" },
                    { 95, true, true, (short)1, "System.String", "", "", false, false, false, 128, 0, "Name", "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 96, true, true, (short)2, "System.String", "", "", false, false, false, 128, 0, "DbName", "string", "nvarchar", "", null, "Visible" },
                    { 97, true, true, (short)5, "System.String", "", "", false, false, true, 512, 0, "Description", "string", "nvarchar", "", null, "Visible" },
                    { 98, true, true, (short)3, "System.String", "", "", false, false, true, 64, 0, "Server", "string", "nvarchar", "", null, "Visible" },
                    { 99, true, true, (short)4, "System.String", "", "", false, false, true, 32, 0, "UserName", "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 100, true, true, (short)-1, "System.String", "", "", false, false, true, 32, 0, "Password", "string", "nvarchar", "", null, "AlwaysHidden" },
                    { 89, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, "Id", "number", "int", "", null, "AlwaysHidden" },
                    { 101, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, "Id", "number", "int", "", null, "AlwaysHidden" },
                    { 103, true, true, (short)1, "System.String", "", "", false, false, false, 64, 0, "Name", "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 104, true, true, (short)2, "System.String", "", "", false, false, false, 64, 0, "Category", "string", "nvarchar", "", null, "Visible" },
                    { 105, true, true, (short)3, "System.String", "", "", false, false, true, 256, 0, "Description", "string", "nvarchar", "", null, "Visible" },
                    { 106, true, true, (short)-1, "System.Int32", "", "", false, false, true, 0, 0, "Id", "number", "int", "", null, "AlwaysHidden" },
                    { 107, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, "RowNo", "number", "int", "", null, "AlwaysVisible" },
                    { 108, true, true, (short)1, "System.String", "", "", false, false, false, 64, 0, "UserName", "string", "nvarchar", "", null, "Visible" },
                    { 109, true, true, (short)2, "System.String", "", "", false, false, false, 64, 0, "BranchName", "string", "nvarchar", "", null, "Visible" },
                    { 110, true, true, (short)3, "System.String", "", "", false, false, false, 64, 0, "FiscalPeriodName", "string", "nvarchar", "", null, "Visible" },
                    { 111, true, true, (short)4, "System.Date", "", "", false, false, false, 0, 0, "Date", "Date", "datetime", "Default", null, "AlwaysVisible" },
                    { 112, true, true, (short)5, "System.TimeSpan", "", "", false, false, false, 7, 0, "Time", "Date", "time", "", null, "AlwaysVisible" },
                    { 113, true, true, (short)6, "System.String", "", "", false, false, false, 64, 0, "EntityTypeName", "string", "nvarchar", "", null, "Visible" },
                    { 102, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, "RowNo", "number", "int", "", null, "AlwaysVisible" },
                    { 88, true, true, (short)4, "System.String", "", "", false, false, true, 512, 0, "Description", "string", "nvarchar", "", null, "Visible" },
                    { 87, true, true, (short)3, "System.Date", "", "", false, false, false, 0, 0, "EndDate", "Date", "datetime", "Default", null, "Visible" },
                    { 86, true, true, (short)2, "System.Date", "", "", false, false, false, 0, 0, "StartDate", "Date", "datetime", "Default", null, "Visible" },
                    { 61, true, true, (short)-1, "System.Int32", "", "", false, false, true, 0, 0, "CurrencyId", "number", "int", "", null, "AlwaysHidden" },
                    { 62, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, "RowNo", "number", "int", "", null, "AlwaysVisible" },
                    { 63, true, true, (short)1, "System.String", "", "", false, false, false, 16, 0, "Code", "string", "nvarchar", "", null, "Visible" },
                    { 64, true, true, (short)2, "System.String", "", "", false, false, false, 256, 0, "FullCode", "string", "nvarchar", "", null, "Visible" },
                    { 65, true, true, (short)3, "System.String", "", "", false, false, false, 512, 0, "Name", "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 66, true, true, (short)4, "System.String", "", "", false, false, true, 512, 0, "Description", "string", "nvarchar", "", null, "Visible" },
                    { 67, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, "Id", "number", "int", "", null, "AlwaysHidden" },
                    { 68, true, true, (short)-1, "System.Int16", "", "", false, false, false, 0, 0, "Level", "", "smallint", "", null, "AlwaysHidden" },
                    { 69, true, false, (short)-1, "System.Int16", "", "", false, false, false, 0, 0, "BranchScope", "number", "smallint", "", null, "AlwaysHidden" },
                    { 70, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, "RowNo", "number", "int", "", null, "AlwaysVisible" },
                    { 71, true, true, (short)1, "System.String", "", "", false, false, false, 16, 0, "Code", "string", "nvarchar", "", null, "Visible" },
                    { 72, true, true, (short)2, "System.String", "", "", false, false, false, 256, 0, "FullCode", "string", "nvarchar", "", null, "Visible" },
                    { 73, true, true, (short)3, "System.String", "", "", false, false, false, 512, 0, "Name", "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 74, true, true, (short)4, "System.String", "", "", false, false, true, 512, 0, "Description", "string", "nvarchar", "", null, "Visible" }
                });

            migrationBuilder.InsertData(
                schema: "Metadata",
                table: "Column",
                columns: new[] { "ColumnID", "AllowFiltering", "AllowSorting", "DisplayIndex", "DotNetType", "Expression", "GroupName", "IsDynamic", "IsFixedLength", "IsNullable", "Length", "MinLength", "Name", "ScriptType", "StorageType", "Type", "ViewID", "Visibility" },
                values: new object[,]
                {
                    { 75, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, "Id", "number", "int", "", null, "AlwaysHidden" },
                    { 76, true, true, (short)-1, "System.Int16", "", "", false, false, false, 0, 0, "Level", "", "smallint", "", null, "AlwaysHidden" },
                    { 77, true, false, (short)-1, "System.Int16", "", "", false, false, false, 0, 0, "BranchScope", "number", "smallint", "", null, "AlwaysHidden" },
                    { 78, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, "RowNo", "number", "int", "", null, "AlwaysVisible" },
                    { 79, true, true, (short)1, "System.String", "", "", false, false, false, 16, 0, "Code", "string", "nvarchar", "", null, "Visible" },
                    { 80, true, true, (short)2, "System.String", "", "", false, false, false, 256, 0, "FullCode", "string", "nvarchar", "", null, "Visible" },
                    { 81, true, true, (short)3, "System.String", "", "", false, false, false, 512, 0, "Name", "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 82, true, true, (short)4, "System.String", "", "", false, false, true, 512, 0, "Description", "string", "nvarchar", "", null, "Visible" },
                    { 83, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, "Id", "number", "int", "", null, "AlwaysHidden" },
                    { 84, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, "RowNo", "number", "int", "", null, "AlwaysVisible" },
                    { 85, true, true, (short)1, "System.String", "", "", false, false, false, 64, 0, "Name", "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 59, true, true, (short)-1, "System.Int16", "", "", false, false, false, 0, 0, "Level", "", "smallint", "", null, "AlwaysHidden" },
                    { 229, true, true, (short)4, "System.String", "", "", false, false, false, 512, 0, "AccountName", "string", "nvarchar", "", null, "Visible" },
                    { 164, true, true, (short)6, "System.Decimal", "", "", false, false, false, 0, 0, "Debit", "number", "money", "Money", null, "Visible" },
                    { 231, true, true, (short)6, "System.Decimal", "", "", false, false, false, 0, 0, "Debit", "number", "money", "Money", null, "Visible" },
                    { 376, true, true, (short)1, "System.Int32", "", "", false, false, false, 0, 0, "LineCount", "number", "int", "", null, "Visible" },
                    { 377, true, true, (short)2, "System.Date", "", "", false, false, false, 0, 0, "VoucherDate", "Date", "datetime", "Default", null, "Visible" },
                    { 378, true, true, (short)3, "System.String", "", "", false, false, false, 512, 0, "Description", "string", "nvarchar", "", null, "Visible" },
                    { 379, true, true, (short)4, "System.Decimal", "", "", false, false, false, 0, 0, "BaseCurrencyDebit", "number", "money", "Money", null, "Visible" },
                    { 380, true, true, (short)5, "System.Decimal", "", "", false, false, false, 0, 0, "BaseCurrencyCredit", "number", "money", "Money", null, "Visible" },
                    { 381, true, true, (short)6, "System.Decimal", "", "", false, false, false, 0, 0, "BaseCurrencyBalance", "number", "money", "Money", null, "Visible" },
                    { 382, true, true, (short)7, "System.Decimal", "", "", false, false, false, 0, 0, "Debit", "number", "money", "Money", null, "Visible" },
                    { 383, true, true, (short)8, "System.Decimal", "", "", false, false, false, 0, 0, "Credit", "number", "money", "Money", null, "Visible" },
                    { 384, true, true, (short)9, "System.Decimal", "", "", false, false, false, 0, 0, "Balance", "number", "money", "Money", null, "Visible" },
                    { 385, true, true, (short)10, "System.String", "", "", false, false, false, 128, 0, "BranchName", "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 386, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, "RowNo", "number", "int", "", null, "AlwaysVisible" },
                    { 375, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, "RowNo", "number", "int", "", null, "AlwaysVisible" },
                    { 387, true, true, (short)1, "System.Int32", "", "", false, false, false, 0, 0, "Number", "number", "int", "", null, "AlwaysVisible" },
                    { 389, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, "BranchId", "number", "int", "", null, "AlwaysHidden" },
                    { 390, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, "RowNo", "number", "int", "", null, "AlwaysVisible" },
                    { 391, true, true, (short)1, "System.String", "", "", false, false, false, 256, 0, "AccountFullCode", "string", "nvarchar", "", null, "Visible" },
                    { 392, true, true, (short)2, "System.String", "", "", false, false, false, 128, 0, "AccountName", "string", "nvarchar", "", null, "Visible" },
                    { 393, true, true, (short)3, "System.Date", "", "", false, false, false, 0, 0, "VoucherDate", "Date", "datetime", "Default", null, "Visible" },
                    { 394, true, true, (short)4, "System.Int32", "", "", false, false, false, 0, 0, "VoucherNo", "number", "int", "", null, "AlwaysVisible" },
                    { 395, true, true, (short)5, "System.String", "", "", false, false, false, 64, 0, "VoucherReference", "string", "nvarchar", "", null, "Visible" },
                    { 396, true, true, (short)6, "System.Decimal", "", "", false, false, false, 0, 0, "Debit", "number", "money", "Money", null, "Visible" },
                    { 397, true, true, (short)7, "System.Decimal", "", "", false, false, false, 0, 0, "Credit", "number", "money", "Money", null, "Visible" },
                    { 398, true, true, (short)8, "System.String", "", "", false, false, true, 512, 0, "Description", "string", "nvarchar", "", null, "Visible" },
                    { 399, true, true, (short)9, "System.String", "", "", false, false, false, 128, 0, "DetailAccountFullCode", "string", "nvarchar", "", null, "Hidden" },
                    { 388, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, "Id", "number", "int", "", null, "AlwaysHidden" },
                    { 374, true, true, (short)11, "System.String", "", "", false, false, false, 128, 0, "BranchName", "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 373, true, true, (short)10, "System.Decimal", "", "", false, false, false, 0, 0, "Balance", "number", "money", "Money", null, "Visible" }
                });

            migrationBuilder.InsertData(
                schema: "Metadata",
                table: "Column",
                columns: new[] { "ColumnID", "AllowFiltering", "AllowSorting", "DisplayIndex", "DotNetType", "Expression", "GroupName", "IsDynamic", "IsFixedLength", "IsNullable", "Length", "MinLength", "Name", "ScriptType", "StorageType", "Type", "ViewID", "Visibility" },
                values: new object[,]
                {
                    { 372, true, true, (short)9, "System.Decimal", "", "", false, false, false, 0, 0, "Credit", "number", "money", "Money", null, "Visible" },
                    { 347, true, true, (short)3, "System.Decimal", "", "", false, false, false, 0, 0, "Credit", "number", "money", "Money", null, "Visible" },
                    { 348, true, true, (short)4, "System.Decimal", "", "", false, false, false, 0, 0, "Balance", "number", "money", "Money", null, "Visible" },
                    { 349, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, "RowNo", "number", "int", "", null, "AlwaysVisible" },
                    { 350, true, true, (short)1, "System.Date", "", "", false, false, false, 0, 0, "VoucherDate", "Date", "datetime", "Default", null, "Visible" },
                    { 351, true, true, (short)2, "System.Int32", "", "", false, false, false, 0, 0, "VoucherNo", "number", "int", "", null, "Visible" },
                    { 352, true, true, (short)3, "System.String", "", "", false, false, false, 512, 0, "Description", "string", "nvarchar", "", null, "Visible" },
                    { 353, true, true, (short)4, "System.String", "", "", false, false, false, 64, 0, "Reference", "string", "nvarchar", "", null, "Visible" },
                    { 354, true, true, (short)5, "System.Decimal", "", "", false, false, false, 0, 0, "BaseCurrencyDebit", "number", "money", "Money", null, "Visible" },
                    { 355, true, true, (short)6, "System.Decimal", "", "", false, false, false, 0, 0, "BaseCurrencyCredit", "number", "money", "Money", null, "Visible" },
                    { 356, true, true, (short)7, "System.Decimal", "", "", false, false, false, 0, 0, "BaseCurrencyBalance", "number", "money", "Money", null, "Visible" },
                    { 357, true, true, (short)8, "System.Decimal", "", "", false, false, false, 0, 0, "Debit", "number", "money", "Money", null, "Visible" },
                    { 358, true, true, (short)9, "System.Decimal", "", "", false, false, false, 0, 0, "Credit", "number", "money", "Money", null, "Visible" },
                    { 359, true, true, (short)10, "System.Decimal", "", "", false, false, false, 0, 0, "Balance", "number", "money", "Money", null, "Visible" },
                    { 360, true, true, (short)11, "System.Decimal", "", "", false, false, false, 0, 0, "CurrencyRate", "number", "money", "Money", null, "Visible" },
                    { 361, true, true, (short)12, "System.String", "", "", false, false, false, 128, 0, "Mark", "string", "nvarchar", "", null, "Visible" },
                    { 362, true, true, (short)13, "System.String", "", "", false, false, false, 128, 0, "BranchName", "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 363, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, "RowNo", "number", "int", "", null, "AlwaysVisible" },
                    { 364, true, true, (short)1, "System.Date", "", "", false, false, false, 0, 0, "VoucherDate", "Date", "datetime", "Default", null, "Visible" },
                    { 365, true, true, (short)2, "System.Int32", "", "", false, false, false, 0, 0, "VoucherNo", "number", "int", "", null, "Visible" },
                    { 366, true, true, (short)3, "System.String", "", "", false, false, false, 512, 0, "Description", "string", "nvarchar", "", null, "Visible" },
                    { 367, true, true, (short)4, "System.String", "", "", false, false, false, 64, 0, "Reference", "string", "nvarchar", "", null, "Visible" },
                    { 368, true, true, (short)5, "System.Decimal", "", "", false, false, false, 0, 0, "BaseCurrencyDebit", "number", "money", "Money", null, "Visible" },
                    { 369, true, true, (short)6, "System.Decimal", "", "", false, false, false, 0, 0, "BaseCurrencyCredit", "number", "money", "Money", null, "Visible" },
                    { 370, true, true, (short)7, "System.Decimal", "", "", false, false, false, 0, 0, "BaseCurrencyBalance", "number", "money", "Money", null, "Visible" },
                    { 371, true, true, (short)8, "System.Decimal", "", "", false, false, false, 0, 0, "Debit", "number", "money", "Money", null, "Visible" },
                    { 400, true, true, (short)10, "System.String", "", "", false, false, false, 128, 0, "DetailAccountName", "string", "nvarchar", "", null, "Hidden" },
                    { 346, true, true, (short)2, "System.Decimal", "", "", false, false, false, 0, 0, "Debit", "number", "money", "Money", null, "Visible" },
                    { 401, true, true, (short)11, "System.String", "", "", false, false, false, 128, 0, "CostCenterFullCode", "string", "nvarchar", "", null, "Hidden" },
                    { 403, true, true, (short)13, "System.String", "", "", false, false, false, 128, 0, "ProjectFullCode", "string", "nvarchar", "", null, "Hidden" },
                    { 433, true, true, (short)-1, "System.String", "", "", false, false, true, 64, 0, "VoucherReference", "string", "nvarchar", "", null, "AlwaysHidden" },
                    { 434, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, "RowNo", "number", "int", "", null, "AlwaysVisible" },
                    { 435, true, true, (short)1, "System.String", "", "", false, false, false, 512, 0, "DetailAccountFullCode", "string", "nvarchar", "", null, "Visible" },
                    { 436, true, true, (short)2, "System.String", "", "", false, false, false, 512, 0, "DetailAccountName", "string", "nvarchar", "", null, "Visible" },
                    { 437, true, true, (short)3, "System.Decimal", "", "StartBalance", false, false, false, 0, 0, "StartBalanceDebit", "number", "money", "Money", null, "Visible" },
                    { 438, true, true, (short)4, "System.Decimal", "", "StartBalance", false, false, false, 0, 0, "StartBalanceCredit", "number", "money", "Money", null, "Visible" },
                    { 439, true, true, (short)5, "System.Decimal", "", "Turnover", false, false, false, 0, 0, "TurnoverDebit", "number", "money", "Money", null, "Visible" },
                    { 440, true, true, (short)6, "System.Decimal", "", "Turnover", false, false, false, 0, 0, "TurnoverCredit", "number", "money", "Money", null, "Visible" },
                    { 441, true, true, (short)7, "System.Decimal", "", "OperationSum", false, false, false, 0, 0, "OperationSumDebit", "number", "money", "Money", null, "Visible" },
                    { 442, true, true, (short)8, "System.Decimal", "", "OperationSum", false, false, false, 0, 0, "OperationSumCredit", "number", "money", "Money", null, "Visible" },
                    { 443, true, true, (short)9, "System.Decimal", "", "EndBalance", false, false, false, 0, 0, "EndBalanceDebit", "number", "money", "Money", null, "Visible" },
                    { 432, true, true, (short)9, "System.String", "", "", false, false, false, 128, 0, "BranchName", "string", "nvarchar", "", null, "AlwaysVisible" }
                });

            migrationBuilder.InsertData(
                schema: "Metadata",
                table: "Column",
                columns: new[] { "ColumnID", "AllowFiltering", "AllowSorting", "DisplayIndex", "DotNetType", "Expression", "GroupName", "IsDynamic", "IsFixedLength", "IsNullable", "Length", "MinLength", "Name", "ScriptType", "StorageType", "Type", "ViewID", "Visibility" },
                values: new object[,]
                {
                    { 444, true, true, (short)10, "System.Decimal", "", "EndBalance", false, false, false, 0, 0, "EndBalanceCredit", "number", "money", "Money", null, "Visible" },
                    { 446, true, true, (short)-1, "System.String", "", "", false, false, true, 64, 0, "VoucherReference", "string", "nvarchar", "", null, "AlwaysHidden" },
                    { 447, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, "RowNo", "number", "int", "", null, "AlwaysVisible" },
                    { 448, true, true, (short)1, "System.String", "", "", false, false, false, 512, 0, "DetailAccountFullCode", "string", "nvarchar", "", null, "Visible" },
                    { 449, true, true, (short)2, "System.String", "", "", false, false, false, 512, 0, "DetailAccountName", "string", "nvarchar", "", null, "Visible" },
                    { 450, true, true, (short)3, "System.Decimal", "", "StartBalance", false, false, false, 0, 0, "StartBalanceDebit", "number", "money", "Money", null, "Visible" },
                    { 451, true, true, (short)4, "System.Decimal", "", "StartBalance", false, false, false, 0, 0, "StartBalanceCredit", "number", "money", "Money", null, "Visible" },
                    { 452, true, true, (short)5, "System.Decimal", "", "Turnover", false, false, false, 0, 0, "TurnoverDebit", "number", "money", "Money", null, "Visible" },
                    { 453, true, true, (short)6, "System.Decimal", "", "Turnover", false, false, false, 0, 0, "TurnoverCredit", "number", "money", "Money", null, "Visible" },
                    { 454, true, true, (short)7, "System.Decimal", "", "OperationSum", false, false, false, 0, 0, "OperationSumDebit", "number", "money", "Money", null, "Visible" },
                    { 455, true, true, (short)8, "System.Decimal", "", "OperationSum", false, false, false, 0, 0, "OperationSumCredit", "number", "money", "Money", null, "Visible" },
                    { 456, true, true, (short)9, "System.Decimal", "", "Corrections", false, false, false, 0, 0, "CorrectionsDebit", "number", "money", "Money", null, "Visible" },
                    { 445, true, true, (short)11, "System.String", "", "", false, false, false, 128, 0, "BranchName", "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 431, true, true, (short)8, "System.Decimal", "", "EndBalance", false, false, false, 0, 0, "EndBalanceCredit", "number", "money", "Money", null, "Visible" },
                    { 430, true, true, (short)7, "System.Decimal", "", "EndBalance", false, false, false, 0, 0, "EndBalanceDebit", "number", "money", "Money", null, "Visible" },
                    { 429, true, true, (short)6, "System.Decimal", "", "Turnover", false, false, false, 0, 0, "TurnoverCredit", "number", "money", "Money", null, "Visible" },
                    { 404, true, true, (short)14, "System.String", "", "", false, false, false, 128, 0, "ProjectName", "string", "nvarchar", "", null, "Hidden" },
                    { 405, true, true, (short)15, "System.String", "", "", false, false, false, 64, 0, "CurrencyName", "string", "nvarchar", "", null, "Hidden" },
                    { 406, true, true, (short)16, "System.Decimal", "", "", false, false, true, 0, 0, "CurrencyValue", "number", "money", "Money", null, "Hidden" },
                    { 407, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, "RowNo", "number", "int", "", null, "AlwaysVisible" },
                    { 408, true, true, (short)1, "System.String", "", "", false, false, false, 512, 0, "DetailAccountFullCode", "string", "nvarchar", "", null, "Visible" },
                    { 409, true, true, (short)2, "System.String", "", "", false, false, false, 512, 0, "DetailAccountName", "string", "nvarchar", "", null, "Visible" },
                    { 230, true, true, (short)5, "System.String", "", "", false, false, false, 512, 0, "Description", "string", "nvarchar", "", null, "Visible" },
                    { 411, true, true, (short)4, "System.Decimal", "", "EndBalance", false, false, false, 0, 0, "EndBalanceCredit", "number", "money", "Money", null, "Visible" },
                    { 412, true, true, (short)5, "System.String", "", "", false, false, false, 128, 0, "BranchName", "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 413, true, true, (short)-1, "System.String", "", "", false, false, true, 64, 0, "VoucherReference", "string", "nvarchar", "", null, "AlwaysHidden" },
                    { 414, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, "RowNo", "number", "int", "", null, "AlwaysVisible" },
                    { 415, true, true, (short)1, "System.String", "", "", false, false, false, 512, 0, "DetailAccountFullCode", "string", "nvarchar", "", null, "Visible" },
                    { 416, true, true, (short)2, "System.String", "", "", false, false, false, 512, 0, "DetailAccountName", "string", "nvarchar", "", null, "Visible" },
                    { 417, true, true, (short)3, "System.Decimal", "", "Turnover", false, false, false, 0, 0, "TurnoverDebit", "number", "money", "Money", null, "Visible" },
                    { 418, true, true, (short)4, "System.Decimal", "", "Turnover", false, false, false, 0, 0, "TurnoverCredit", "number", "money", "Money", null, "Visible" },
                    { 419, true, true, (short)5, "System.Decimal", "", "EndBalance", false, false, false, 0, 0, "EndBalanceDebit", "number", "money", "Money", null, "Visible" },
                    { 420, true, true, (short)6, "System.Decimal", "", "EndBalance", false, false, false, 0, 0, "EndBalanceCredit", "number", "money", "Money", null, "Visible" },
                    { 421, true, true, (short)7, "System.String", "", "", false, false, false, 128, 0, "BranchName", "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 422, true, true, (short)-1, "System.String", "", "", false, false, true, 64, 0, "VoucherReference", "string", "nvarchar", "", null, "AlwaysHidden" },
                    { 423, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, "RowNo", "number", "int", "", null, "AlwaysVisible" },
                    { 424, true, true, (short)1, "System.String", "", "", false, false, false, 512, 0, "DetailAccountFullCode", "string", "nvarchar", "", null, "Visible" },
                    { 425, true, true, (short)2, "System.String", "", "", false, false, false, 512, 0, "DetailAccountName", "string", "nvarchar", "", null, "Visible" },
                    { 426, true, true, (short)3, "System.Decimal", "", "StartBalance", false, false, false, 0, 0, "StartBalanceDebit", "number", "money", "Money", null, "Visible" },
                    { 427, true, true, (short)4, "System.Decimal", "", "StartBalance", false, false, false, 0, 0, "StartBalanceCredit", "number", "money", "Money", null, "Visible" },
                    { 428, true, true, (short)5, "System.Decimal", "", "Turnover", false, false, false, 0, 0, "TurnoverDebit", "number", "money", "Money", null, "Visible" },
                    { 402, true, true, (short)12, "System.String", "", "", false, false, false, 128, 0, "CostCenterName", "string", "nvarchar", "", null, "Hidden" }
                });

            migrationBuilder.InsertData(
                schema: "Metadata",
                table: "Column",
                columns: new[] { "ColumnID", "AllowFiltering", "AllowSorting", "DisplayIndex", "DotNetType", "Expression", "GroupName", "IsDynamic", "IsFixedLength", "IsNullable", "Length", "MinLength", "Name", "ScriptType", "StorageType", "Type", "ViewID", "Visibility" },
                values: new object[,]
                {
                    { 345, true, true, (short)1, "System.String", "", "", false, false, false, 128, 0, "CurrencyName", "string", "nvarchar", "", null, "Visible" },
                    { 410, true, true, (short)3, "System.Decimal", "", "EndBalance", false, false, false, 0, 0, "EndBalanceDebit", "number", "money", "Money", null, "Visible" },
                    { 343, true, true, (short)-1, "System.String", "", "", false, false, true, 64, 0, "VoucherReference", "string", "nvarchar", "", null, "AlwaysHidden" },
                    { 260, true, true, (short)2, "System.Int32", "", "", false, false, false, 0, 0, "LineCount", "number", "int", "", null, "Visible" },
                    { 261, true, true, (short)3, "System.String", "", "", false, false, false, 512, 0, "Description", "string", "nvarchar", "", null, "Visible" },
                    { 262, true, true, (short)4, "System.Decimal", "", "", false, false, false, 0, 0, "Debit", "number", "money", "Money", null, "Visible" },
                    { 263, true, true, (short)5, "System.Decimal", "", "", false, false, false, 0, 0, "Credit", "number", "money", "Money", null, "Visible" },
                    { 264, false, false, (short)6, "System.Decimal", "", "", false, false, false, 0, 0, "Balance", "number", "money", "Money", null, "Visible" },
                    { 265, true, true, (short)7, "System.String", "", "", false, false, false, 128, 0, "BranchName", "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 266, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, "Id", "number", "int", "", null, "AlwaysHidden" },
                    { 267, true, true, (short)-1, "System.Int16", "", "", false, false, false, 0, 0, "BranchScope", "number", "smallint", "", null, "AlwaysHidden" },
                    { 268, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, "BranchId", "number", "int", "", null, "AlwaysHidden" },
                    { 269, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, "TaxCode", "number", "int", "", null, "AlwaysHidden" },
                    { 270, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, "RowNo", "number", "int", "", null, "AlwaysVisible" },
                    { 271, true, true, (short)1, "System.String", "", "", false, false, false, 64, 0, "Name", "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 272, true, true, (short)2, "System.String", "", "", false, false, false, 8, 0, "Code", "string", "nvarchar", "", null, "Visible" },
                    { 274, true, true, (short)2, "System.String", "", "", false, false, false, 16, 0, "MinorUnit", "string", "nvarchar", "", null, "Visible" },
                    { 275, true, true, (short)3, "System.String", "", "", false, false, true, 512, 0, "Description", "string", "nvarchar", "", null, "Visible" },
                    { 276, true, true, (short)4, "System.String", "", "", false, false, false, 128, 0, "BranchName", "string", "nvarchar", "", null, "Visible" },
                    { 277, true, true, (short)5, "System.Int16", "", "", false, false, false, 0, 0, "DecimalCount", "number", "smallint", "", null, "Hidden" },
                    { 278, true, true, (short)7, "System.String", "", "", false, false, true, 32, 0, "State", "string", "nvarchar", "", null, "Visible" },
                    { 279, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, "Id", "number", "int", "", null, "AlwaysHidden" },
                    { 280, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, "CurrencyId", "number", "int", "", null, "AlwaysHidden" },
                    { 281, true, true, (short)-1, "System.Int32", "", "", false, false, false, 0, 0, "BranchId", "number", "int", "", null, "AlwaysHidden" },
                    { 282, true, true, (short)-1, "System.Int16", "", "", false, false, false, 0, 0, "BranchScope", "number", "smallint", "", null, "AlwaysHidden" },
                    { 283, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, "RowNo", "number", "int", "", null, "AlwaysVisible" },
                    { 284, true, true, (short)1, "System.Date", "", "", false, false, false, 0, 0, "Date", "Date", "datetime", "Default", null, "Visible" },
                    { 285, true, true, (short)2, "System.TimeSpan", "", "", false, false, false, 0, 0, "Time", "number", "time", "", null, "Visible" },
                    { 259, true, true, (short)1, "System.Date", "", "", false, false, false, 0, 0, "VoucherDate", "Date", "datetime", "Default", null, "Visible" },
                    { 286, true, true, (short)3, "System.decimal", "", "", false, false, false, 0, 0, "Multiplier", "number", "money", "Money", null, "Visible" },
                    { 258, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, "RowNo", "number", "int", "", null, "AlwaysVisible" },
                    { 256, false, false, (short)6, "System.Decimal", "", "", false, false, false, 0, 0, "Balance", "number", "money", "Money", null, "Visible" },
                    { 344, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, "RowNo", "number", "int", "", null, "AlwaysVisible" },
                    { 232, true, true, (short)7, "System.Decimal", "", "", false, false, false, 0, 0, "Credit", "number", "money", "Money", null, "Visible" },
                    { 233, true, true, (short)8, "System.String", "", "", false, false, false, 128, 0, "BranchName", "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 234, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, "RowNo", "number", "int", "", null, "AlwaysVisible" },
                    { 235, true, true, (short)1, "System.String", "", "", false, false, false, 512, 0, "AccountFullCode", "string", "nvarchar", "", null, "Visible" },
                    { 236, true, true, (short)2, "System.String", "", "", false, false, false, 512, 0, "AccountName", "string", "nvarchar", "", null, "Visible" },
                    { 237, true, true, (short)3, "System.String", "", "", false, false, false, 512, 0, "Description", "string", "nvarchar", "", null, "Visible" },
                    { 238, true, true, (short)4, "System.Decimal", "", "", false, false, false, 0, 0, "Debit", "number", "money", "Money", null, "Visible" },
                    { 239, true, true, (short)5, "System.Decimal", "", "", false, false, false, 0, 0, "Credit", "number", "money", "Money", null, "Visible" },
                    { 240, true, true, (short)6, "System.String", "", "", false, false, false, 128, 0, "BranchName", "string", "nvarchar", "", null, "AlwaysVisible" }
                });

            migrationBuilder.InsertData(
                schema: "Metadata",
                table: "Column",
                columns: new[] { "ColumnID", "AllowFiltering", "AllowSorting", "DisplayIndex", "DotNetType", "Expression", "GroupName", "IsDynamic", "IsFixedLength", "IsNullable", "Length", "MinLength", "Name", "ScriptType", "StorageType", "Type", "ViewID", "Visibility" },
                values: new object[,]
                {
                    { 241, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, "RowNo", "number", "int", "", null, "AlwaysVisible" },
                    { 242, true, true, (short)1, "System.Date", "", "", false, false, false, 0, 0, "VoucherDate", "Date", "datetime", "Default", null, "Visible" },
                    { 243, true, true, (short)2, "System.Int32", "", "", false, false, false, 0, 0, "VoucherNo", "number", "int", "", null, "Visible" },
                    { 244, true, true, (short)3, "System.String", "", "", false, false, false, 512, 0, "Description", "string", "nvarchar", "", null, "Visible" },
                    { 245, true, true, (short)4, "System.Decimal", "", "", false, false, false, 0, 0, "Debit", "number", "money", "Money", null, "Visible" },
                    { 246, true, true, (short)5, "System.Decimal", "", "", false, false, false, 0, 0, "Credit", "number", "money", "Money", null, "Visible" },
                    { 247, false, false, (short)6, "System.Decimal", "", "", false, false, false, 0, 0, "Balance", "number", "money", "Money", null, "Visible" },
                    { 248, true, true, (short)7, "System.String", "", "", false, false, false, 128, 0, "Mark", "string", "nvarchar", "", null, "Visible" },
                    { 249, true, true, (short)8, "System.String", "", "", false, false, false, 128, 0, "BranchName", "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 250, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, "RowNo", "number", "int", "", null, "AlwaysVisible" },
                    { 251, true, true, (short)1, "System.Date", "", "", false, false, false, 0, 0, "VoucherDate", "Date", "datetime", "Default", null, "Visible" },
                    { 252, true, true, (short)2, "System.Int32", "", "", false, false, false, 0, 0, "VoucherNo", "number", "int", "", null, "Visible" },
                    { 253, true, true, (short)3, "System.String", "", "", false, false, false, 512, 0, "Description", "string", "nvarchar", "", null, "Visible" },
                    { 254, true, true, (short)4, "System.Decimal", "", "", false, false, false, 0, 0, "Debit", "number", "money", "Money", null, "Visible" },
                    { 255, true, true, (short)5, "System.Decimal", "", "", false, false, false, 0, 0, "Credit", "number", "money", "Money", null, "Visible" },
                    { 257, true, true, (short)7, "System.String", "", "", false, false, false, 128, 0, "BranchName", "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 287, true, true, (short)4, "System.String", "", "", false, false, false, 128, 0, "BranchName", "string", "nvarchar", "", null, "Visible" },
                    { 523, true, true, (short)-1, "System.String", "", "", false, false, true, 64, 0, "VoucherReference", "string", "nvarchar", "", null, "AlwaysHidden" },
                    { 289, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, "RowNo", "number", "int", "", null, "AlwaysVisible" },
                    { 317, true, true, (short)1, "System.String", "", "", false, false, false, 512, 0, "AccountFullCode", "string", "nvarchar", "", null, "Visible" },
                    { 318, true, true, (short)2, "System.String", "", "", false, false, false, 512, 0, "AccountName", "string", "nvarchar", "", null, "Visible" },
                    { 320, true, true, (short)4, "System.Decimal", "", "StartBalance", false, false, false, 0, 0, "StartBalanceCredit", "number", "money", "Money", null, "Visible" },
                    { 321, true, true, (short)5, "System.Decimal", "", "Turnover", false, false, false, 0, 0, "TurnoverDebit", "number", "money", "Money", null, "Visible" },
                    { 322, true, true, (short)6, "System.Decimal", "", "Turnover", false, false, false, 0, 0, "TurnoverCredit", "number", "money", "Money", null, "Visible" },
                    { 323, true, true, (short)7, "System.Decimal", "", "OperationSum", false, false, false, 0, 0, "OperationSumDebit", "number", "money", "Money", null, "Visible" },
                    { 324, true, true, (short)8, "System.Decimal", "", "OperationSum", false, false, false, 0, 0, "OperationSumCredit", "number", "money", "Money", null, "Visible" },
                    { 325, true, true, (short)9, "System.Decimal", "", "EndBalance", false, false, false, 0, 0, "EndBalanceDebit", "number", "money", "Money", null, "Visible" },
                    { 326, true, true, (short)10, "System.Decimal", "", "EndBalance", false, false, false, 0, 0, "EndBalanceCredit", "number", "money", "Money", null, "Visible" },
                    { 327, true, true, (short)11, "System.String", "", "", false, false, false, 128, 0, "BranchName", "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 328, true, true, (short)-1, "System.String", "", "", false, false, true, 64, 0, "VoucherReference", "string", "nvarchar", "", null, "AlwaysHidden" },
                    { 316, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, "RowNo", "number", "int", "", null, "AlwaysVisible" },
                    { 329, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, "RowNo", "number", "int", "", null, "AlwaysVisible" },
                    { 331, true, true, (short)2, "System.String", "", "", false, false, false, 512, 0, "AccountName", "string", "nvarchar", "", null, "Visible" },
                    { 332, true, true, (short)3, "System.Decimal", "", "StartBalance", false, false, false, 0, 0, "StartBalanceDebit", "number", "money", "Money", null, "Visible" },
                    { 333, true, true, (short)4, "System.Decimal", "", "StartBalance", false, false, false, 0, 0, "StartBalanceCredit", "number", "money", "Money", null, "Visible" },
                    { 334, true, true, (short)5, "System.Decimal", "", "Turnover", false, false, false, 0, 0, "TurnoverDebit", "number", "money", "Money", null, "Visible" },
                    { 335, true, true, (short)6, "System.Decimal", "", "Turnover", false, false, false, 0, 0, "TurnoverCredit", "number", "money", "Money", null, "Visible" },
                    { 336, true, true, (short)7, "System.Decimal", "", "OperationSum", false, false, false, 0, 0, "OperationSumDebit", "number", "money", "Money", null, "Visible" },
                    { 337, true, true, (short)8, "System.Decimal", "", "OperationSum", false, false, false, 0, 0, "OperationSumCredit", "number", "money", "Money", null, "Visible" },
                    { 338, true, true, (short)9, "System.Decimal", "", "Corrections", false, false, false, 0, 0, "CorrectionsDebit", "number", "money", "Money", null, "Visible" },
                    { 339, true, true, (short)10, "System.Decimal", "", "Corrections", false, false, false, 0, 0, "CorrectionsCredit", "number", "money", "Money", null, "Visible" },
                    { 340, true, true, (short)11, "System.Decimal", "", "EndBalance", false, false, false, 0, 0, "EndBalanceDebit", "number", "money", "Money", null, "Visible" }
                });

            migrationBuilder.InsertData(
                schema: "Metadata",
                table: "Column",
                columns: new[] { "ColumnID", "AllowFiltering", "AllowSorting", "DisplayIndex", "DotNetType", "Expression", "GroupName", "IsDynamic", "IsFixedLength", "IsNullable", "Length", "MinLength", "Name", "ScriptType", "StorageType", "Type", "ViewID", "Visibility" },
                values: new object[,]
                {
                    { 341, true, true, (short)12, "System.Decimal", "", "EndBalance", false, false, false, 0, 0, "EndBalanceCredit", "number", "money", "Money", null, "Visible" },
                    { 330, true, true, (short)1, "System.String", "", "", false, false, false, 512, 0, "AccountFullCode", "string", "nvarchar", "", null, "Visible" },
                    { 315, true, true, (short)-1, "System.String", "", "", false, false, true, 64, 0, "VoucherReference", "string", "nvarchar", "", null, "AlwaysHidden" },
                    { 319, true, true, (short)3, "System.Decimal", "", "StartBalance", false, false, false, 0, 0, "StartBalanceDebit", "number", "money", "Money", null, "Visible" },
                    { 313, true, true, (short)8, "System.Decimal", "", "EndBalance", false, false, false, 0, 0, "EndBalanceCredit", "number", "money", "Money", null, "Visible" },
                    { 290, true, true, (short)1, "System.String", "", "", false, false, false, 512, 0, "AccountFullCode", "string", "nvarchar", "", null, "Visible" },
                    { 291, true, true, (short)2, "System.String", "", "", false, false, false, 512, 0, "AccountName", "string", "nvarchar", "", null, "Visible" },
                    { 292, true, true, (short)3, "System.Decimal", "", "EndBalance", false, false, false, 0, 0, "EndBalanceDebit", "number", "money", "Money", null, "Visible" },
                    { 293, true, true, (short)4, "System.Decimal", "", "EndBalance", false, false, false, 0, 0, "EndBalanceCredit", "number", "money", "Money", null, "Visible" },
                    { 294, true, true, (short)5, "System.String", "", "", false, false, false, 128, 0, "BranchName", "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 295, true, true, (short)-1, "System.String", "", "", false, false, true, 64, 0, "VoucherReference", "string", "nvarchar", "", null, "AlwaysHidden" },
                    { 288, true, true, (short)5, "System.String", "", "", false, false, true, 512, 0, "Description", "string", "nvarchar", "", null, "Visible" },
                    { 296, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, "RowNo", "number", "int", "", null, "AlwaysVisible" },
                    { 314, true, true, (short)9, "System.String", "", "", false, false, false, 128, 0, "BranchName", "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 298, true, true, (short)2, "System.String", "", "", false, false, false, 512, 0, "AccountName", "string", "nvarchar", "", null, "Visible" },
                    { 299, true, true, (short)3, "System.Decimal", "", "Turnover", false, false, false, 0, 0, "TurnoverDebit", "number", "money", "Money", null, "Visible" },
                    { 300, true, true, (short)4, "System.Decimal", "", "Turnover", false, false, false, 0, 0, "TurnoverCredit", "number", "money", "Money", null, "Visible" },
                    { 297, true, true, (short)1, "System.String", "", "", false, false, false, 512, 0, "AccountFullCode", "string", "nvarchar", "", null, "Visible" },
                    { 302, true, true, (short)6, "System.Decimal", "", "EndBalance", false, false, false, 0, 0, "EndBalanceCredit", "number", "money", "Money", null, "Visible" },
                    { 312, true, true, (short)7, "System.Decimal", "", "EndBalance", false, false, false, 0, 0, "EndBalanceDebit", "number", "money", "Money", null, "Visible" },
                    { 301, true, true, (short)5, "System.Decimal", "", "EndBalance", false, false, false, 0, 0, "EndBalanceDebit", "number", "money", "Money", null, "Visible" },
                    { 310, true, true, (short)5, "System.Decimal", "", "Turnover", false, false, false, 0, 0, "TurnoverDebit", "number", "money", "Money", null, "Visible" },
                    { 309, true, true, (short)4, "System.Decimal", "", "StartBalance", false, false, false, 0, 0, "StartBalanceCredit", "number", "money", "Money", null, "Visible" },
                    { 308, true, true, (short)3, "System.Decimal", "", "StartBalance", false, false, false, 0, 0, "StartBalanceDebit", "number", "money", "Money", null, "Visible" },
                    { 311, true, true, (short)6, "System.Decimal", "", "Turnover", false, false, false, 0, 0, "TurnoverCredit", "number", "money", "Money", null, "Visible" },
                    { 306, true, true, (short)1, "System.String", "", "", false, false, false, 512, 0, "AccountFullCode", "string", "nvarchar", "", null, "Visible" },
                    { 305, false, false, (short)0, "System.Int32", "", "", false, false, true, 0, 0, "RowNo", "number", "int", "", null, "AlwaysVisible" },
                    { 304, true, true, (short)-1, "System.String", "", "", false, false, true, 64, 0, "VoucherReference", "string", "nvarchar", "", null, "AlwaysHidden" },
                    { 303, true, true, (short)7, "System.String", "", "", false, false, false, 128, 0, "BranchName", "string", "nvarchar", "", null, "AlwaysVisible" },
                    { 307, true, true, (short)2, "System.String", "", "", false, false, false, 512, 0, "AccountName", "string", "nvarchar", "", null, "Visible" },
                    { 342, true, true, (short)13, "System.String", "", "", false, false, false, 128, 0, "BranchName", "string", "nvarchar", "", null, "AlwaysVisible" }
                });

            migrationBuilder.InsertData(
                schema: "Metadata",
                table: "Command",
                columns: new[] { "CommandID", "HotKey", "IconName", "Index", "ParentId", "PermissionId", "RouteUrl", "TitleKey" },
                values: new object[,]
                {
                    { 1, "", "folder-close", null, null, null, "", "Accounting" },
                    { 23, "", "folder-close", null, null, null, "", "Organization" },
                    { 27, "", "folder-close", null, null, null, "", "Administration" },
                    { 37, "", "folder-close", null, null, null, "", "Tools" },
                    { 52, "", "folder-close", null, null, null, "", "Treasury" },
                    { 100000, "", "folder-close", null, null, null, "", "ProductScope" }
                });

            migrationBuilder.InsertData(
                schema: "Metadata",
                table: "EntityType",
                columns: new[] { "EntityTypeID", "Description", "Name" },
                values: new object[,]
                {
                    { 8, "", "ViewRowPermission" },
                    { 9, "", "UserReport" },
                    { 6, "", "User" },
                    { 4, "", "Setting" },
                    { 2, "", "Role" }
                });

            migrationBuilder.InsertData(
                schema: "Metadata",
                table: "EntityType",
                columns: new[] { "EntityTypeID", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "", "CompanyDb" },
                    { 5, "", "SysOperationLog" }
                });

            migrationBuilder.InsertData(
                schema: "Metadata",
                table: "Locale",
                columns: new[] { "LocaleID", "Code", "LocalName", "Name" },
                values: new object[,]
                {
                    { 1, "en", "English", "English" },
                    { 2, "fa", "فارسی", "Persian" },
                    { 3, "ar", "العربیه", "Arabic" },
                    { 4, "fr", "Français", "French" }
                });

            migrationBuilder.InsertData(
                schema: "Metadata",
                table: "Operation",
                columns: new[] { "OperationID", "Description", "Name" },
                values: new object[,]
                {
                    { 26, "", "AssignRole" },
                    { 27, "", "AssignUser" },
                    { 28, "", "BranchAccess" },
                    { 29, "", "FiscalPeriodAccess" },
                    { 57, "", "CompanyAccess" },
                    { 35, "", "RoleAccess" },
                    { 54, "", "Export" },
                    { 25, "", "SwitchBranch" },
                    { 30, "", "ViewArchive" },
                    { 24, "", "SwitchFiscalPeriod" },
                    { 58, "", "PrintPreview" },
                    { 22, "", "FailedLogin" },
                    { 1, "", "View" },
                    { 23, "", "CompanyLogin" },
                    { 3, "", "Edit" },
                    { 4, "", "Delete" },
                    { 5, "", "Filter" },
                    { 2, "", "Create" },
                    { 7, "", "Save" },
                    { 8, "", "Archive" },
                    { 10, "", "Design" },
                    { 21, "", "GroupDelete" },
                    { 6, "", "Print" }
                });

            migrationBuilder.InsertData(
                schema: "Metadata",
                table: "OperationSource",
                columns: new[] { "OperationSourceID", "Description", "Name" },
                values: new object[,]
                {
                    { 7, "", "AppLogin" },
                    { 8, "", "AppEnvironment" },
                    { 14, "", "SystemSettings" }
                });

            migrationBuilder.InsertData(
                schema: "Metadata",
                table: "OperationSourceType",
                columns: new[] { "OperationSourceTypeID", "Name" },
                values: new object[,]
                {
                    { 1, "BaseData" },
                    { 2, "OperationalForms" },
                    { 3, "Reports" }
                });

            migrationBuilder.InsertData(
                schema: "Metadata",
                table: "Subsystem",
                columns: new[] { "SubsystemID", "Name" },
                values: new object[,]
                {
                    { 100000, "ProductScope" },
                    { 3, "Treasury" },
                    { 2, "Accounting" },
                    { 1, "Administration" }
                });

            migrationBuilder.InsertData(
                schema: "Metadata",
                table: "View",
                columns: new[] { "ViewID", "EntityName", "EntityType", "FetchUrl", "IsCartableIntegrated", "IsHierarchy", "Name", "SearchUrl" },
                values: new object[,]
                {
                    { 45, "ItemBalance", "", "", false, false, "DetailAccountBalance6Column", "" },
                    { 60, "SysOperationLogArchive", "", "", false, false, "SysOperationLogArchive", "" },
                    { 59, "SysOperationLog", "", "", false, false, "SysOperationLog", "" }
                });

            migrationBuilder.InsertData(
                schema: "Metadata",
                table: "View",
                columns: new[] { "ViewID", "EntityName", "EntityType", "FetchUrl", "IsCartableIntegrated", "IsHierarchy", "Name", "SearchUrl" },
                values: new object[,]
                {
                    { 58, "BalanceByAccount", "", "", false, false, "BalanceByAccount", "" },
                    { 57, "ItemBalance", "", "", false, false, "ProjectBalance10Column", "" },
                    { 56, "ItemBalance", "", "", false, false, "ProjectBalance8Column", "" },
                    { 55, "ItemBalance", "", "", false, false, "ProjectBalance6Column", "" },
                    { 54, "ItemBalance", "", "", false, false, "ProjectBalance4Column", "" },
                    { 53, "ItemBalance", "", "", false, false, "ProjectBalance2Column", "" },
                    { 52, "ItemBalance", "", "", false, false, "CostCenterBalance10Column", "" },
                    { 51, "ItemBalance", "", "", false, false, "CostCenterBalance8Column", "" },
                    { 50, "ItemBalance", "", "", false, false, "CostCenterBalance6Column", "" },
                    { 49, "ItemBalance", "", "", false, false, "CostCenterBalance4Column", "" },
                    { 48, "ItemBalance", "", "", false, false, "CostCenterBalance2Column", "" },
                    { 47, "ItemBalance", "", "", false, false, "DetailAccountBalance10Column", "" },
                    { 46, "ItemBalance", "", "", false, false, "DetailAccountBalance8Column", "" },
                    { 61, "OperationLogArchive", "", "", false, false, "OperationLogArchive", "" },
                    { 62, "ProfitLoss", "", "", false, false, "ProfitLoss", "" },
                    { 72, "CheckBookReport", "", "", false, false, "CheckBookReport", "" },
                    { 64, "ProfitLoss", "", "", false, false, "ProfitLossSimple", "" },
                    { 100004, "Attribute", "Base", "", false, false, "Attribute", "" },
                    { 100003, "Property", "Base", "", false, false, "Property", "" },
                    { 100002, "Unit", "Base", "", false, false, "Unit", "" },
                    { 100001, "Brand", "Base", "", false, false, "Brand", "" },
                    { 78, "VouchersByDate", "Operational", "", false, false, "VouchersByDate", "" },
                    { 77, "PayReceiveCashAccount", "Core", "", false, false, "PayReceiveCashAccount", "" },
                    { 76, "PayReceiveAccount", "Core", "", false, false, "PayReceiveAccount", "" },
                    { 75, "Receipt", "Operational", "", false, false, "Receipt", "" },
                    { 63, "GroupActionResult", "", "", false, false, "GroupActionResult", "" },
                    { 74, "Payment", "Operational", "", false, false, "Payment", "" },
                    { 44, "ItemBalance", "", "", false, false, "DetailAccountBalance4Column", "" },
                    { 71, "CheckBook", "Operational", "", false, false, "CheckBook", "" },
                    { 70, "CashRegister", "Base", "", false, false, "CashRegister", "" },
                    { 69, "CheckBookPage", "Core", "", false, false, "CheckBookPage", "" },
                    { 68, "Dashboard", "Core", "", false, false, "Widget", "" },
                    { 67, "BalanceSheet", "", "", false, false, "BalanceSheet", "" },
                    { 66, "ProfitLoss", "", "", false, false, "ComparativeProfitLossSimple", "" },
                    { 65, "ProfitLoss", "", "", false, false, "ComparativeProfitLoss", "" },
                    { 73, "SourceApp", "Base", "", false, false, "SourceApp", "" },
                    { 43, "ItemBalance", "", "", false, false, "DetailAccountBalance2Column", "" },
                    { 32, "TestBalance", "", "", false, false, "TestBalance2Column", "" },
                    { 41, "NumberList", "", "", false, false, "NumberList", "" },
                    { 16, "Journal", "", "", false, false, "JournalByDateByRowDetail", "" },
                    { 15, "Journal", "", "", false, false, "JournalByDateByRow", "" },
                    { 14, "AccountCollection", "Core", "", false, false, "AccountCollectionAccount", "" }
                });

            migrationBuilder.InsertData(
                schema: "Metadata",
                table: "View",
                columns: new[] { "ViewID", "EntityName", "EntityType", "FetchUrl", "IsCartableIntegrated", "IsHierarchy", "Name", "SearchUrl" },
                values: new object[,]
                {
                    { 13, "OperationLog", "Core", "", false, false, "OperationLog", "" },
                    { 12, "AccountGroup", "Core", "", false, false, "AccountGroup", "" },
                    { 11, "Company", "Core", "", false, false, "CompanyDb", "" },
                    { 10, "Branch", "Core", "", false, false, "Branch", "/branches" },
                    { 9, "FiscalPeriod", "Core", "", true, false, "FiscalPeriod", "/fperiods" },
                    { 8, "Project", "Base", "/lookup/projects", true, true, "Project", "/projects" },
                    { 7, "CostCenter", "Base", "/lookup/ccenters", true, true, "CostCenter", "/ccenters" },
                    { 6, "DetailAccount", "Base", "/lookup/faccounts", true, true, "DetailAccount", "/faccounts" },
                    { 5, "Role", "Core", "", false, false, "Role", "" },
                    { 4, "User", "Core", "", false, false, "User", "" },
                    { 3, "VoucherLine", "Operational", "/lookup/vouchers/lines", true, false, "VoucherLine", "" },
                    { 2, "Voucher", "Operational", "/lookup/vouchers", true, false, "Voucher", "" },
                    { 1, "Account", "Base", "/lookup/accounts", true, true, "Account", "/accounts/lookup" },
                    { 100005, "File", "Base", "", false, false, "File", "" },
                    { 17, "Journal", "", "", false, false, "JournalByDateByLedger", "" },
                    { 18, "Journal", "", "", false, false, "JournalByDateBySubsidiary", "" },
                    { 19, "Journal", "", "", false, false, "JournalByDateSummary", "" },
                    { 20, "Journal", "", "", false, false, "JournalByDateSummaryByDate", "" },
                    { 40, "CurrencyBook", "", "", false, false, "CurrencyBookSummary", "" },
                    { 39, "CurrencyBook", "", "", false, false, "CurrencyBookSingleSummary", "" },
                    { 38, "CurrencyBook", "", "", false, false, "CurrencyBookSingle", "" },
                    { 37, "CurrencyBook", "", "", false, false, "CurrencyBook", "" },
                    { 36, "TestBalance", "", "", false, false, "TestBalance10Column", "" },
                    { 35, "TestBalance", "", "", false, false, "TestBalance8Column", "" },
                    { 34, "TestBalance", "", "", false, false, "TestBalance6Column", "" },
                    { 33, "TestBalance", "", "", false, false, "TestBalance4Column", "" },
                    { 42, "VoucherLineDetail", "", "", false, false, "VoucherLineDetail", "" },
                    { 31, "CurrencyRate", "", "", false, false, "CurrencyRate", "" },
                    { 29, "AccountBook", "", "", false, false, "AccountBookSummary", "" },
                    { 28, "AccountBook", "", "", false, false, "AccountBookSingleSummary", "" },
                    { 27, "AccountBook", "", "", false, false, "AccountBookSingle", "" },
                    { 26, "Journal", "", "", false, false, "JournalByNoSummary", "" },
                    { 25, "Journal", "", "", false, false, "JournalByNoBySubsidiary", "" },
                    { 24, "Journal", "", "", false, false, "JournalByNoByLedger", "" },
                    { 23, "Journal", "", "", false, false, "JournalByNoByRowDetail", "" },
                    { 21, "Journal", "", "", false, false, "JournalByDateSummaryByMonth", "" },
                    { 30, "Currency", "", "", false, false, "Currency", "" },
                    { 22, "Journal", "", "", false, false, "JournalByNoByRow", "" }
                });

            migrationBuilder.InsertData(
                schema: "Auth",
                table: "PermissionGroup",
                columns: new[] { "PermissionGroupID", "Description", "EntityName", "Name", "SourceTypeId", "SubsystemId" },
                values: new object[,]
                {
                    { 100005, "", "file", "ManageEntities,files", 1, 100000 },
                    { 13, "", "AccountGroup", "ManageEntities,AccountGroups", 1, 2 },
                    { 8, "", "Vouchers", "ManageEntities,Vouchers", 2, 2 },
                    { 7, "", "Voucher", "Vouchers", 2, 2 },
                    { 5, "", "FiscalPeriod", "ManageEntities,FiscalPeriods", 1, 2 },
                    { 4, "", "Project", "ManageEntities,Projects", 1, 2 },
                    { 3, "", "CostCenter", "ManageEntities,CostCenters", 1, 2 },
                    { 2, "", "DetailAccount", "ManageEntities,DetailAccounts", 1, 2 },
                    { 1, "", "Account", "ManageEntities,Accounts", 1, 2 },
                    { 30, "", "SystemIssue", "SystemIssueReport", 3, 1 },
                    { 14, "", "AccountCollection", "ManageEntities,AccountCollections", 1, 2 },
                    { 22, "", "RowAccess", "RowAccessSettings", 1, 1 },
                    { 20, "", "Setting", "Settings", 1, 1 },
                    { 18, "", "UserReport", "ManageEntities,UserReports", 3, 1 },
                    { 17, "", "Report", "ManageEntities,Reports", 3, 1 },
                    { 16, "", "SysOperationLog", "ManageEntities,SysOperationLogs", 3, 1 },
                    { 15, "", "OperationLog", "ManageEntities,OperationLogs", 3, 1 },
                    { 12, "", "Role", "ManageEntities,Roles", 1, 1 },
                    { 11, "", "User", "ManageEntities,Users", 1, 1 },
                    { 10, "", "Company", "ManageEntities,Companies", 1, 1 },
                    { 9, "", "Branch", "ManageEntities,Branches", 1, 1 },
                    { 21, "", "LogSetting", "LogSetting", 1, 1 },
                    { 19, "", "AccountRelations", "AccountRelations", 1, 2 },
                    { 6, "", "Currency", "ManageEntities,Currencies", 1, 2 },
                    { 24, "", "Journal", "JournalReport", 3, 2 },
                    { 100003, "", "Property", "ManageEntities,Properties", 1, 100000 },
                    { 100002, "", "Unit", "ManageEntities,Units", 1, 100000 },
                    { 100001, "", "Brand", "ManageEntities,Brands", 1, 100000 },
                    { 42, "", "Receipt", "Receipts", 2, 3 },
                    { 41, "", "Payment", "Payments", 2, 3 },
                    { 23, "", "CurrencyRate", "CurrencyRate", 1, 2 },
                    { 39, "", "CheckBookReport", "CheckBookReport", 3, 3 },
                    { 38, "", "CashRegister", "ManageEntities,CashRegisters", 1, 3 },
                    { 37, "", "CheckBook", "ManageEntities,CheckBooks", 2, 3 },
                    { 36, "", "Dashboard", "Dashboard", 3, 2 },
                    { 40, "", "SourceApp", "ManageEntities,SourceApps", 1, 3 },
                    { 34, "", "BalanceSheet", "BalanceSheetReport", 3, 2 },
                    { 35, "", "SpecialVoucher", "SpecialVoucherOps", 2, 2 },
                    { 25, "", "AccountBook", "AccountBookReport", 3, 2 },
                    { 26, "", "TestBalance", "TestBalanceReport", 3, 2 },
                    { 27, "", "CurrencyBook", "CurrencyBookReport", 3, 2 },
                    { 100004, "", "Attribute", "ManageEntities,Attributes", 1, 100000 }
                });

            migrationBuilder.InsertData(
                schema: "Auth",
                table: "PermissionGroup",
                columns: new[] { "PermissionGroupID", "Description", "EntityName", "Name", "SourceTypeId", "SubsystemId" },
                values: new object[,]
                {
                    { 29, "", "BalanceByAccount", "BalanceByAccountReport", 3, 2 },
                    { 31, "", "ProfitLoss", "ProfitLossReport", 3, 2 },
                    { 32, "", "DraftVoucher", "DraftVouchers", 2, 2 },
                    { 33, "", "DraftVouchers", "ManageEntities,DraftVouchers", 2, 2 },
                    { 28, "", "ItemBalance", "ItemBalanceReport", 3, 2 }
                });

            migrationBuilder.InsertData(
                schema: "Config",
                table: "SysLogSetting",
                columns: new[] { "SysLogSettingID", "EntityTypeId", "IsEnabled", "ModifiedDate", "OperationId", "SourceId" },
                values: new object[,]
                {
                    { 40, 5, true, new DateTime(2023, 10, 21, 17, 7, 29, 19, DateTimeKind.Local).AddTicks(1150), 54, null },
                    { 47, null, true, new DateTime(2023, 10, 21, 17, 7, 29, 19, DateTimeKind.Local).AddTicks(1174), 24, 8 },
                    { 46, null, true, new DateTime(2023, 10, 21, 17, 7, 29, 19, DateTimeKind.Local).AddTicks(1171), 23, 8 },
                    { 45, null, true, new DateTime(2023, 10, 21, 17, 7, 29, 19, DateTimeKind.Local).AddTicks(1167), 22, 7 },
                    { 38, 5, true, new DateTime(2023, 10, 21, 17, 7, 29, 19, DateTimeKind.Local).AddTicks(1143), 58, null },
                    { 25, 2, true, new DateTime(2023, 10, 21, 17, 7, 29, 19, DateTimeKind.Local).AddTicks(1097), 58, null },
                    { 15, 6, true, new DateTime(2023, 10, 21, 17, 7, 29, 19, DateTimeKind.Local).AddTicks(1003), 58, null },
                    { 7, 1, true, new DateTime(2023, 10, 21, 17, 7, 29, 19, DateTimeKind.Local).AddTicks(973), 58, null },
                    { 31, 2, true, new DateTime(2023, 10, 21, 17, 7, 29, 19, DateTimeKind.Local).AddTicks(1116), 57, null },
                    { 48, null, true, new DateTime(2023, 10, 21, 17, 7, 29, 19, DateTimeKind.Local).AddTicks(1177), 25, 8 },
                    { 27, 2, true, new DateTime(2023, 10, 21, 17, 7, 29, 19, DateTimeKind.Local).AddTicks(1104), 54, null },
                    { 9, 1, true, new DateTime(2023, 10, 21, 17, 7, 29, 19, DateTimeKind.Local).AddTicks(980), 54, null },
                    { 6, 1, true, new DateTime(2023, 10, 21, 17, 7, 29, 19, DateTimeKind.Local).AddTicks(969), 5, null },
                    { 35, 5, true, new DateTime(2023, 10, 21, 17, 7, 29, 19, DateTimeKind.Local).AddTicks(1132), 4, null },
                    { 22, 2, true, new DateTime(2023, 10, 21, 17, 7, 29, 19, DateTimeKind.Local).AddTicks(1027), 4, null },
                    { 4, 1, true, new DateTime(2023, 10, 21, 17, 7, 29, 19, DateTimeKind.Local).AddTicks(953), 4, null },
                    { 21, 2, true, new DateTime(2023, 10, 21, 17, 7, 29, 19, DateTimeKind.Local).AddTicks(1024), 3, null },
                    { 13, 6, true, new DateTime(2023, 10, 21, 17, 7, 29, 19, DateTimeKind.Local).AddTicks(996), 3, null },
                    { 3, 1, true, new DateTime(2023, 10, 21, 17, 7, 29, 19, DateTimeKind.Local).AddTicks(949), 3, null },
                    { 14, 6, true, new DateTime(2023, 10, 21, 17, 7, 29, 19, DateTimeKind.Local).AddTicks(1000), 5, null },
                    { 17, 6, true, new DateTime(2023, 10, 21, 17, 7, 29, 19, DateTimeKind.Local).AddTicks(1010), 54, null },
                    { 2, 1, true, new DateTime(2023, 10, 21, 17, 7, 29, 19, DateTimeKind.Local).AddTicks(936), 2, null },
                    { 43, 8, true, new DateTime(2023, 10, 21, 17, 7, 29, 19, DateTimeKind.Local).AddTicks(1160), 1, null },
                    { 34, 5, true, new DateTime(2023, 10, 21, 17, 7, 29, 19, DateTimeKind.Local).AddTicks(1128), 1, null },
                    { 32, 4, true, new DateTime(2023, 10, 21, 17, 7, 29, 19, DateTimeKind.Local).AddTicks(1119), 1, null },
                    { 19, 2, true, new DateTime(2023, 10, 21, 17, 7, 29, 19, DateTimeKind.Local).AddTicks(1018), 1, null },
                    { 11, 6, true, new DateTime(2023, 10, 21, 17, 7, 29, 19, DateTimeKind.Local).AddTicks(989), 1, null },
                    { 1, 1, true, new DateTime(2023, 10, 21, 17, 7, 29, 18, DateTimeKind.Local).AddTicks(9145), 1, null },
                    { 12, 6, true, new DateTime(2023, 10, 21, 17, 7, 29, 19, DateTimeKind.Local).AddTicks(993), 2, null },
                    { 24, 2, true, new DateTime(2023, 10, 21, 17, 7, 29, 19, DateTimeKind.Local).AddTicks(1093), 5, null },
                    { 20, 2, true, new DateTime(2023, 10, 21, 17, 7, 29, 19, DateTimeKind.Local).AddTicks(1021), 2, null },
                    { 8, 1, true, new DateTime(2023, 10, 21, 17, 7, 29, 19, DateTimeKind.Local).AddTicks(977), 6, null },
                    { 42, 5, true, new DateTime(2023, 10, 21, 17, 7, 29, 19, DateTimeKind.Local).AddTicks(1157), 30, null },
                    { 30, 2, true, new DateTime(2023, 10, 21, 17, 7, 29, 19, DateTimeKind.Local).AddTicks(1113), 29, null },
                    { 29, 2, true, new DateTime(2023, 10, 21, 17, 7, 29, 19, DateTimeKind.Local).AddTicks(1110), 28, null },
                    { 28, 2, true, new DateTime(2023, 10, 21, 17, 7, 29, 19, DateTimeKind.Local).AddTicks(1107), 27, null },
                    { 18, 6, true, new DateTime(2023, 10, 21, 17, 7, 29, 19, DateTimeKind.Local).AddTicks(1014), 26, null }
                });

            migrationBuilder.InsertData(
                schema: "Config",
                table: "SysLogSetting",
                columns: new[] { "SysLogSettingID", "EntityTypeId", "IsEnabled", "ModifiedDate", "OperationId", "SourceId" },
                values: new object[,]
                {
                    { 36, 5, true, new DateTime(2023, 10, 21, 17, 7, 29, 19, DateTimeKind.Local).AddTicks(1136), 21, null },
                    { 23, 2, true, new DateTime(2023, 10, 21, 17, 7, 29, 19, DateTimeKind.Local).AddTicks(1089), 21, null },
                    { 5, 1, true, new DateTime(2023, 10, 21, 17, 7, 29, 19, DateTimeKind.Local).AddTicks(957), 21, null },
                    { 49, 9, true, new DateTime(2023, 10, 21, 17, 7, 29, 19, DateTimeKind.Local).AddTicks(1180), 10, null },
                    { 41, 5, true, new DateTime(2023, 10, 21, 17, 7, 29, 19, DateTimeKind.Local).AddTicks(1153), 8, null },
                    { 44, 8, true, new DateTime(2023, 10, 21, 17, 7, 29, 19, DateTimeKind.Local).AddTicks(1164), 7, null },
                    { 33, 4, true, new DateTime(2023, 10, 21, 17, 7, 29, 19, DateTimeKind.Local).AddTicks(1123), 7, null },
                    { 39, 5, true, new DateTime(2023, 10, 21, 17, 7, 29, 19, DateTimeKind.Local).AddTicks(1146), 6, null },
                    { 26, 2, true, new DateTime(2023, 10, 21, 17, 7, 29, 19, DateTimeKind.Local).AddTicks(1100), 6, null },
                    { 16, 6, true, new DateTime(2023, 10, 21, 17, 7, 29, 19, DateTimeKind.Local).AddTicks(1006), 6, null },
                    { 10, 1, true, new DateTime(2023, 10, 21, 17, 7, 29, 19, DateTimeKind.Local).AddTicks(985), 35, null },
                    { 37, 5, true, new DateTime(2023, 10, 21, 17, 7, 29, 19, DateTimeKind.Local).AddTicks(1139), 5, null }
                });

            migrationBuilder.InsertData(
                schema: "Contact",
                table: "Person",
                columns: new[] { "PersonID", "FullName", "UserID" },
                values: new object[] { 1, "admin", 1 });

            migrationBuilder.InsertData(
                schema: "Metadata",
                table: "Command",
                columns: new[] { "CommandID", "HotKey", "IconName", "Index", "ParentId", "PermissionId", "RouteUrl", "TitleKey" },
                values: new object[,]
                {
                    { 100001, "", "folder-close", null, 100000, null, "", "BaseData" },
                    { 2, "", "folder-close", null, 1, null, "", "BaseData" },
                    { 11, "", "folder-close", null, 1, null, "", "VoucherOps" },
                    { 100003, "", "folder-close", null, 100000, null, "", "ProductScopeReports" },
                    { 20, "", "folder-close", null, 1, null, "", "AccountingLedgers" },
                    { 39, "", "folder-close", null, 1, null, "", "FinancialReports" },
                    { 16, "", "folder-close", null, 1, null, "", "SpecialOps" },
                    { 54, "", "folder-close", null, 52, null, "", "CheckOperations" },
                    { 55, "", "folder-close", null, 52, null, "", "CheckReports" },
                    { 62, "", "folder-close", null, 52, null, "", "PaymentOperations" },
                    { 63, "", "folder-close", null, 52, null, "", "ReceiptOperations" },
                    { 53, "", "folder-close", null, 52, null, "", "BaseData" },
                    { 100002, "", "folder-close", null, 100000, null, "", "ProductScopeOperations" }
                });

            migrationBuilder.InsertData(
                schema: "Reporting",
                table: "Report",
                columns: new[] { "ReportID", "Code", "CreatedById", "IsDefault", "IsDynamic", "IsGroup", "IsSystem", "ParentId", "ReportViewId", "ResourceKeys", "ServiceUrl", "SubsystemId", "ViewId" },
                values: new object[,]
                {
                    { 100000, "ProductScope", 1, false, false, true, true, null, null, "", "", 100000, null },
                    { 93, "Treasury", 1, false, false, true, true, null, null, "", "", 3, null },
                    { 42, "Report-QReport-Manage", 1, false, false, true, true, null, null, "", "", 1, null },
                    { 13, "Accounting", 1, false, false, true, true, null, null, "", "", 2, null },
                    { 1, "Administration", 1, false, false, true, true, null, null, "", "", 1, null }
                });

            migrationBuilder.InsertData(
                schema: "Auth",
                table: "Permission",
                columns: new[] { "PermissionID", "Description", "Flag", "GroupId", "Name" },
                values: new object[,]
                {
                    { 127, "", 2, 19, "Save" },
                    { 192, "", 4, 33, "Print" },
                    { 191, "", 2, 33, "Filter" },
                    { 190, "", 1, 33, "View" },
                    { 196, "", 2048, 32, "Normalize" },
                    { 189, "", 1024, 32, "NavigateEntities,DraftVouchers" },
                    { 188, "", 512, 32, "UndoCheck" },
                    { 187, "", 256, 32, "Check" },
                    { 186, "", 128, 32, "DeleteLine" },
                    { 185, "", 64, 32, "EditLine" },
                    { 184, "", 32, 32, "CreateLine" },
                    { 183, "", 16, 32, "Print" },
                    { 182, "", 8, 32, "Delete" },
                    { 181, "", 4, 32, "Edit" },
                    { 180, "", 2, 32, "Create" },
                    { 179, "", 1, 32, "View" },
                    { 200, "", 16, 31, "FilterByRef" },
                    { 178, "", 8, 31, "Export" },
                    { 193, "", 8, 33, "Export" },
                    { 194, "", 16, 33, "GroupCheck" },
                    { 195, "", 32, 33, "GroupUndoCheck" },
                    { 201, "", 1, 34, "View" },
                    { 219, "", 128, 37, "CreatePages" },
                    { 218, "", 64, 37, "NavigateEntities,CheckBooks" },
                    { 217, "", 32, 37, "Delete" },
                    { 216, "", 16, 37, "Edit" },
                    { 215, "", 8, 37, "Create" },
                    { 214, "", 4, 37, "Print" },
                    { 213, "", 2, 37, "Filter" },
                    { 212, "", 1, 37, "View" },
                    { 177, "", 4, 31, "Print" },
                    { 211, "", 2, 36, "ManageWidgets" },
                    { 209, "", 8, 35, "UncheckClosingVoucher" },
                    { 208, "", 4, 35, "IssueClosingVoucher" },
                    { 207, "", 2, 35, "IssueClosingTempAccountsVoucher" },
                    { 206, "", 1, 35, "IssueOpeningVoucher" },
                    { 205, "", 16, 34, "FilterByRef" },
                    { 204, "", 8, 34, "Export" },
                    { 203, "", 4, 34, "Print" },
                    { 202, "", 2, 34, "Filter" },
                    { 210, "", 1, 36, "ManageDashboard" },
                    { 176, "", 2, 31, "Filter" }
                });

            migrationBuilder.InsertData(
                schema: "Auth",
                table: "Permission",
                columns: new[] { "PermissionID", "Description", "Flag", "GroupId", "Name" },
                values: new object[,]
                {
                    { 175, "", 1, 31, "View" },
                    { 199, "", 32, 29, "FilterByRef" },
                    { 153, "", 1, 26, "View" },
                    { 152, "", 32, 25, "ViewByBranch" },
                    { 151, "", 16, 25, "Mark" },
                    { 150, "", 8, 25, "Export" },
                    { 149, "", 4, 25, "Print" },
                    { 148, "", 2, 25, "Filter" },
                    { 147, "", 1, 25, "View" },
                    { 146, "", 32, 24, "ViewByBranch" },
                    { 154, "", 2, 26, "Filter" },
                    { 145, "", 16, 24, "Mark" },
                    { 143, "", 4, 24, "Print" },
                    { 142, "", 2, 24, "Filter" },
                    { 141, "", 1, 24, "View" },
                    { 140, "", 64, 23, "Delete" },
                    { 139, "", 32, 23, "Edit" },
                    { 138, "", 16, 23, "Create" },
                    { 137, "", 8, 23, "Export" },
                    { 136, "", 4, 23, "Print" },
                    { 144, "", 8, 24, "Export" },
                    { 220, "", 256, 37, "DeletePages" },
                    { 155, "", 4, 26, "Print" },
                    { 157, "", 16, 26, "ViewByBranch" },
                    { 173, "", 16, 29, "ViewByBranch" },
                    { 172, "", 8, 29, "Export" },
                    { 171, "", 4, 29, "Print" },
                    { 170, "", 2, 29, "Filter" },
                    { 169, "", 1, 29, "View" },
                    { 198, "", 32, 28, "FilterByRef" },
                    { 168, "", 16, 28, "ViewByBranch" },
                    { 167, "", 8, 28, "Export" },
                    { 156, "", 8, 26, "Export" },
                    { 166, "", 4, 28, "Print" },
                    { 164, "", 1, 28, "View" },
                    { 163, "", 32, 27, "ViewByBranch" },
                    { 162, "", 16, 27, "Mark" },
                    { 161, "", 8, 27, "Export" },
                    { 160, "", 4, 27, "Print" },
                    { 159, "", 2, 27, "Filter" },
                    { 158, "", 1, 27, "View" },
                    { 197, "", 32, 26, "FilterByRef" }
                });

            migrationBuilder.InsertData(
                schema: "Auth",
                table: "Permission",
                columns: new[] { "PermissionID", "Description", "Flag", "GroupId", "Name" },
                values: new object[,]
                {
                    { 165, "", 2, 28, "Filter" },
                    { 135, "", 2, 23, "Filter" },
                    { 221, "", 512, 37, "CancelPage" },
                    { 223, "", 2048, 37, "ConnectToCheck" },
                    { 100011, "", 8, 100002, "Export" },
                    { 100010, "", 4, 100002, "Print" },
                    { 100009, "", 2, 100002, "Filter" },
                    { 100008, "", 1, 100002, "View" },
                    { 100007, "", 64, 100001, "Delete" },
                    { 100006, "", 32, 100001, "Edit" },
                    { 100005, "", 16, 100001, "Create" },
                    { 100004, "", 8, 100001, "Export" },
                    { 100003, "", 4, 100001, "Print" },
                    { 100002, "", 2, 100001, "Filter" },
                    { 100001, "", 1, 100001, "View" },
                    { 283, "", 2048, 42, "UndoRegister" },
                    { 267, "", 1024, 42, "UndoApprove" },
                    { 266, "", 512, 42, "Approve" },
                    { 265, "", 256, 42, "UndoConfirm" },
                    { 264, "", 128, 42, "Confirm" },
                    { 263, "", 64, 42, "Register" },
                    { 100012, "", 16, 100002, "Create" },
                    { 100013, "", 32, 100002, "Edit" },
                    { 100014, "", 64, 100002, "Delete" },
                    { 100015, "", 1, 100003, "View" },
                    { 100033, "", 16, 100005, "Create" },
                    { 100032, "", 8, 100005, "Export" },
                    { 100031, "", 4, 100005, "Print" },
                    { 100030, "", 2, 100005, "Filter" },
                    { 100029, "", 1, 100005, "View" },
                    { 100028, "", 64, 100004, "Delete" },
                    { 100027, "", 32, 100004, "Edit" },
                    { 100026, "", 16, 100004, "Create" },
                    { 262, "", 32, 42, "NavigateEntities,Receipts" },
                    { 100025, "", 8, 100004, "Export" },
                    { 100023, "", 2, 100004, "Filter" },
                    { 100022, "", 1, 100004, "View" },
                    { 100021, "", 64, 100003, "Delete" },
                    { 100020, "", 32, 100003, "Edit" },
                    { 100019, "", 16, 100003, "Create" },
                    { 100018, "", 8, 100003, "Export" },
                    { 100017, "", 4, 100003, "Print" }
                });

            migrationBuilder.InsertData(
                schema: "Auth",
                table: "Permission",
                columns: new[] { "PermissionID", "Description", "Flag", "GroupId", "Name" },
                values: new object[,]
                {
                    { 100016, "", 2, 100003, "Filter" },
                    { 100024, "", 4, 100004, "Print" },
                    { 261, "", 16, 42, "Delete" },
                    { 260, "", 8, 42, "Edit" },
                    { 259, "", 4, 42, "Create" },
                    { 239, "", 1, 40, "View" },
                    { 238, "", 32, 39, "UndoArchive" },
                    { 237, "", 16, 39, "Archive" },
                    { 236, "", 8, 39, "Export" },
                    { 235, "", 4, 39, "Print" },
                    { 234, "", 2, 39, "Filter" },
                    { 233, "", 1, 39, "View" },
                    { 279, "Mark an inactive cash register as active", 512, 38, "Reactivate" },
                    { 240, "", 2, 40, "Filter" },
                    { 278, "Mark an active cash register as inactive", 256, 38, "Deactivate" },
                    { 231, "", 64, 38, "Delete" },
                    { 230, "", 32, 38, "Edit" },
                    { 229, "", 16, 38, "Create" },
                    { 228, "", 8, 38, "Export" },
                    { 227, "", 4, 38, "Print" },
                    { 226, "", 2, 38, "Filter" },
                    { 225, "", 1, 38, "View" },
                    { 224, "", 4096, 37, "DisconnectFromCheck" },
                    { 232, "", 128, 38, "AssignCashRegisterUser" },
                    { 222, "", 1024, 37, "UndoCancelPage" },
                    { 241, "", 4, 40, "Print" },
                    { 243, "", 16, 40, "Create" },
                    { 258, "", 2, 42, "Print" },
                    { 257, "", 1, 42, "View" },
                    { 282, "", 2048, 41, "UndoRegister" },
                    { 256, "", 1024, 41, "UndoApprove" },
                    { 255, "", 512, 41, "Approve" },
                    { 254, "", 256, 41, "UndoConfirm" },
                    { 253, "", 128, 41, "Confirm" },
                    { 252, "", 64, 41, "Register" },
                    { 242, "", 8, 40, "Export" },
                    { 251, "", 32, 41, "NavigateEntities,Payments" },
                    { 249, "", 8, 41, "Edit" },
                    { 248, "", 4, 41, "Create" },
                    { 247, "", 2, 41, "Print" },
                    { 246, "", 1, 41, "View" },
                    { 281, "Mark an inactive source/application as active", 256, 40, "Reactivate" }
                });

            migrationBuilder.InsertData(
                schema: "Auth",
                table: "Permission",
                columns: new[] { "PermissionID", "Description", "Flag", "GroupId", "Name" },
                values: new object[,]
                {
                    { 280, "Mark an active source/application as inactive", 128, 40, "Deactivate" },
                    { 245, "", 64, 40, "Delete" },
                    { 244, "", 32, 40, "Edit" },
                    { 250, "", 16, 41, "Delete" },
                    { 134, "", 1, 23, "View" },
                    { 100035, "", 64, 100005, "Delete" },
                    { 126, "", 1, 19, "View" },
                    { 131, "", 2, 21, "Save" },
                    { 130, "", 1, 21, "View" },
                    { 129, "", 2, 20, "Save" },
                    { 128, "", 1, 20, "View" },
                    { 125, "", 4, 18, "SetDefault" },
                    { 124, "", 2, 18, "Delete" },
                    { 132, "", 1, 22, "View" },
                    { 123, "", 1, 18, "Save" },
                    { 121, "", 2, 17, "Design" },
                    { 120, "", 1, 17, "View" },
                    { 119, "", 32, 16, "ViewArchive" },
                    { 118, "", 16, 16, "Archive" },
                    { 117, "", 8, 16, "Export" },
                    { 116, "", 4, 16, "Print" },
                    { 122, "", 4, 17, "QuickReportDesign" },
                    { 133, "", 2, 22, "Save" },
                    { 174, "", 1, 30, "View" },
                    { 284, "", 2, 30, "Filter" },
                    { 11, "", 8, 2, "Export" },
                    { 10, "", 4, 2, "Print" },
                    { 9, "", 2, 2, "Filter" },
                    { 8, "", 1, 2, "View" },
                    { 269, "Mark an inactive account as active", 256, 1, "Reactivate" },
                    { 268, "Mark an active account as inactive", 128, 1, "Deactivate" },
                    { 7, "", 64, 1, "Delete" },
                    { 6, "", 32, 1, "Edit" },
                    { 5, "", 16, 1, "Create" },
                    { 4, "", 8, 1, "Export" },
                    { 100034, "", 32, 100005, "Edit" },
                    { 2, "", 2, 1, "Filter" },
                    { 1, "", 1, 1, "View" },
                    { 286, "", 8, 30, "Export" },
                    { 285, "", 4, 30, "Print" },
                    { 115, "", 2, 16, "Filter" },
                    { 12, "", 16, 2, "Create" }
                });

            migrationBuilder.InsertData(
                schema: "Auth",
                table: "Permission",
                columns: new[] { "PermissionID", "Description", "Flag", "GroupId", "Name" },
                values: new object[,]
                {
                    { 114, "", 1, 16, "View" },
                    { 112, "", 16, 15, "Archive" },
                    { 83, "", 2, 11, "Filter" },
                    { 82, "", 1, 11, "View" },
                    { 81, "", 8, 10, "Export" },
                    { 80, "", 4, 10, "Print" },
                    { 79, "", 2, 10, "Filter" },
                    { 78, "", 1, 10, "View" },
                    { 84, "", 4, 11, "Print" },
                    { 77, "", 128, 9, "AssignRolesToEntity,Branch" },
                    { 75, "", 32, 9, "Edit" },
                    { 74, "", 16, 9, "Create" },
                    { 73, "", 8, 9, "Export" },
                    { 72, "", 4, 9, "Print" },
                    { 71, "", 2, 9, "Filter" },
                    { 70, "", 1, 9, "View" },
                    { 76, "", 64, 9, "Delete" },
                    { 85, "", 8, 11, "Export" },
                    { 86, "", 16, 11, "Create" },
                    { 87, "", 32, 11, "Edit" },
                    { 111, "", 8, 15, "Export" },
                    { 110, "", 4, 15, "Print" },
                    { 109, "", 2, 15, "Filter" },
                    { 108, "", 1, 15, "View" },
                    { 98, "", 512, 12, "AssignEntityToRole,FiscalPeriod" },
                    { 97, "", 256, 12, "AssignEntityToRole,Branch" },
                    { 96, "", 128, 12, "AssignEntityToRole,User" },
                    { 95, "", 64, 12, "Delete" },
                    { 94, "", 32, 12, "Edit" },
                    { 93, "", 16, 12, "Create" },
                    { 92, "", 8, 12, "Export" },
                    { 91, "", 4, 12, "Print" },
                    { 90, "", 2, 12, "Filter" },
                    { 89, "", 1, 12, "View" },
                    { 88, "", 64, 11, "AssignRolesToEntity,User" },
                    { 113, "", 32, 15, "ViewArchive" },
                    { 13, "", 32, 2, "Edit" },
                    { 3, "", 4, 1, "Print" },
                    { 270, "Mark an active detail account as inactive", 128, 2, "Deactivate" },
                    { 59, "", 16384, 7, "Finalize" },
                    { 58, "", 8192, 7, "UndoApprove" },
                    { 57, "", 4096, 7, "Approve" }
                });

            migrationBuilder.InsertData(
                schema: "Auth",
                table: "Permission",
                columns: new[] { "PermissionID", "Description", "Flag", "GroupId", "Name" },
                values: new object[,]
                {
                    { 14, "", 64, 2, "Delete" },
                    { 55, "", 1024, 7, "Confirm" },
                    { 54, "", 512, 7, "UndoCheck" },
                    { 60, "", 32768, 7, "NavigateEntities,Vouchers" },
                    { 53, "", 256, 7, "Check" },
                    { 51, "", 64, 7, "EditLine" },
                    { 50, "", 32, 7, "CreateLine" },
                    { 49, "", 16, 7, "Print" },
                    { 48, "", 8, 7, "Delete" },
                    { 47, "", 4, 7, "Edit" },
                    { 46, "", 2, 7, "Create" },
                    { 52, "", 128, 7, "DeleteLine" },
                    { 61, "", 1, 8, "View" },
                    { 62, "", 2, 8, "Filter" },
                    { 63, "", 4, 8, "Print" },
                    { 107, "", 2, 14, "Save" },
                    { 106, "", 1, 14, "View" },
                    { 105, "", 64, 13, "Delete" },
                    { 104, "", 32, 13, "Edit" },
                    { 103, "", 16, 13, "Create" },
                    { 102, "", 8, 13, "Export" },
                    { 101, "", 4, 13, "Print" },
                    { 100, "", 2, 13, "Filter" },
                    { 99, "", 1, 13, "View" },
                    { 69, "", 256, 8, "GroupFinalize" },
                    { 68, "", 128, 8, "GroupUndoConfirm" },
                    { 67, "", 64, 8, "GroupConfirm" },
                    { 66, "", 32, 8, "GroupUndoCheck" },
                    { 65, "", 16, 8, "GroupCheck" },
                    { 64, "", 8, 8, "Export" },
                    { 45, "", 1, 7, "View" },
                    { 277, "Mark an inactive currency as active", 256, 6, "Reactivate" },
                    { 56, "", 2048, 7, "UndoConfirm" },
                    { 44, "", 128, 6, "ChangeStatus" },
                    { 26, "", 16, 4, "Create" },
                    { 25, "", 8, 4, "Export" },
                    { 24, "", 4, 4, "Print" },
                    { 23, "", 2, 4, "Filter" },
                    { 22, "", 1, 4, "View" },
                    { 273, "Mark an inactive cost center as active", 256, 3, "Reactivate" },
                    { 272, "Mark an active cost center as inactive", 128, 3, "Deactivate" },
                    { 21, "", 64, 3, "Delete" }
                });

            migrationBuilder.InsertData(
                schema: "Auth",
                table: "Permission",
                columns: new[] { "PermissionID", "Description", "Flag", "GroupId", "Name" },
                values: new object[,]
                {
                    { 20, "", 32, 3, "Edit" },
                    { 19, "", 16, 3, "Create" },
                    { 18, "", 8, 3, "Export" },
                    { 276, "Mark an active currency as inactive", 128, 6, "Deactivate" },
                    { 16, "", 2, 3, "Filter" },
                    { 15, "", 1, 3, "View" },
                    { 271, "Mark an inactive detail account as active", 256, 2, "Reactivate" },
                    { 27, "", 32, 4, "Edit" },
                    { 28, "", 64, 4, "Delete" },
                    { 17, "", 4, 3, "Print" },
                    { 275, "Mark an inactive project as active", 256, 4, "Reactivate" },
                    { 274, "Mark an active project as inactive", 128, 4, "Deactivate" },
                    { 43, "", 64, 6, "Delete" },
                    { 42, "", 32, 6, "Edit" },
                    { 41, "", 16, 6, "Create" },
                    { 39, "", 4, 6, "Print" },
                    { 38, "", 2, 6, "Filter" },
                    { 37, "", 1, 6, "View" },
                    { 40, "", 8, 6, "Export" },
                    { 35, "", 64, 5, "Delete" },
                    { 34, "", 32, 5, "Edit" },
                    { 33, "", 16, 5, "Create" },
                    { 32, "", 8, 5, "Export" },
                    { 31, "", 4, 5, "Print" },
                    { 30, "", 2, 5, "Filter" },
                    { 36, "", 128, 5, "AssignRolesToEntity,FiscalPeriod" },
                    { 29, "", 1, 5, "View" }
                });

            migrationBuilder.InsertData(
                schema: "Reporting",
                table: "LocalReport",
                columns: new[] { "LocalReportID", "Caption", "LocaleId", "ReportId", "Template" },
                values: new object[,]
                {
                    { 52, "Accounting", 4, 13, "" },
                    { 51, "Accounting", 3, 13, "" },
                    { 50, "حسابداری", 2, 13, "" },
                    { 3, "Administration", 3, 1, "" },
                    { 4, "Administration", 4, 1, "" },
                    { 2, "راهبری", 2, 1, "" },
                    { 165, "Manage quick reports", 1, 42, "" },
                    { 49, "Accounting", 1, 13, "" },
                    { 166, "مدیریت گزارشات فوری", 2, 42, "" },
                    { 1, "Administration", 1, 1, "" },
                    { 168, "Manage quick reports", 4, 42, "" },
                    { 167, "Manage quick reports", 3, 42, "" },
                    { 100000, "ProductScope", 1, 100000, "" },
                    { 100001, "محصولات", 2, 100000, "" }
                });

            migrationBuilder.InsertData(
                schema: "Reporting",
                table: "Report",
                columns: new[] { "ReportID", "Code", "CreatedById", "IsDefault", "IsDynamic", "IsGroup", "IsSystem", "ParentId", "ReportViewId", "ResourceKeys", "ServiceUrl", "SubsystemId", "ViewId" },
                values: new object[] { 14, "Accnt-Base", 1, false, false, true, true, 13, null, "", "", 2, null });

            migrationBuilder.InsertData(
                schema: "Reporting",
                table: "Report",
                columns: new[] { "ReportID", "Code", "CreatedById", "IsDefault", "IsDynamic", "IsGroup", "IsSystem", "ParentId", "ReportViewId", "ResourceKeys", "ServiceUrl", "SubsystemId", "ViewId" },
                values: new object[,]
                {
                    { 94, "Treasury-Base", 1, false, false, true, true, 93, null, "", "", 3, null },
                    { 95, "Treasury-Operation", 1, false, false, true, true, 93, null, "", "", 3, null },
                    { 16, "Accnt-Report", 1, false, false, true, true, 13, null, "", "", 2, null },
                    { 43, "QReport-Design-Template", 1, true, false, false, false, 42, null, "", "", 1, null },
                    { 4, "Admin-Report", 1, false, false, true, true, 1, null, "", "", 1, null },
                    { 3, "Admin-Operation", 1, false, false, true, true, 1, null, "", "", 1, null },
                    { 2, "Admin-Base", 1, false, false, true, true, 1, null, "", "", 1, null },
                    { 100001, "ProductScope-Base", 1, false, false, true, true, 100000, null, "", "", 100000, null },
                    { 100002, "ProductScope-Operation", 1, false, false, true, true, 100000, null, "", "", 100000, null },
                    { 100003, "ProductScope-Report", 1, false, false, true, true, 100000, null, "", "", 100000, null },
                    { 15, "Accnt-Operation", 1, false, false, true, true, 13, null, "", "", 2, null },
                    { 96, "Treasury-Report", 1, false, false, true, true, 93, null, "", "", 3, null }
                });

            migrationBuilder.InsertData(
                schema: "Metadata",
                table: "Command",
                columns: new[] { "CommandID", "HotKey", "IconName", "Index", "ParentId", "PermissionId", "RouteUrl", "TitleKey" },
                values: new object[,]
                {
                    { 100008, "", "list", null, 100001, 100029, "/product-scope/files", "Files" },
                    { 3, "Ctrl+Shift+G", "th-large", null, 2, 99, "/finance/account-groups", "AccountGroup" },
                    { 15, "Ctrl+Shift+V", "list", null, 11, 61, "/finance/voucher", "Vouchers" },
                    { 14, "Ctrl+L", "list", null, 11, 60, "/finance/vouchers/last", "LastVoucher" },
                    { 12, "Ctrl+Alt+N", "plus", null, 11, 46, "/finance/vouchers/new", "NewVoucher" },
                    { 19, "Ctrl+Alt+I", "list", null, 16, 45, "/finance/vouchers/close-temp-accounts", "ClosingTempAccounts" },
                    { 18, "Ctrl+Alt+U", "list", null, 16, 45, "/finance/vouchers/closing-voucher", "IssueClosingVoucher" },
                    { 17, "Ctrl+Alt+Y", "list", null, 16, 45, "/finance/vouchers/opening-voucher", "IssueOpeningVoucher" },
                    { 13, "Ctrl+S", "search", null, 11, 45, "/finance/vouchers/by-no", "VoucherByNo" },
                    { 10, "Ctrl+Shift+U", "usd", null, 2, 37, "/finance/currency", "Currency" },
                    { 26, "Ctrl+Shift+F", "list", null, 23, 29, "/organization/fiscalperiod", "FiscalPeriods" },
                    { 7, "Ctrl+Shift+P", "file", null, 2, 22, "/finance/projects", "Project" },
                    { 9, "Ctrl+Shift+H", "list", null, 2, 106, "/finance/account-collection", "AccountCollections" },
                    { 6, "Ctrl+Shift+C", "tower", null, 2, 15, "/finance/costCenter", "CostCenter" },
                    { 4, "Ctrl+Shift+A", "th-list", null, 2, 1, "/finance/account", "Account" },
                    { 42, "Ctrl+Shift+S", "tasks", null, 27, 174, "/finance/system-issue", "SystemIssue" },
                    { 30, "Ctrl+Alt+W", "lock", null, 27, 132, "/admin/viewRowPermission", "RowAccessSettings" },
                    { 45, "", "list", null, 27, 130, "/admin/log-settings", "LogSettings" },
                    { 31, "Ctrl+K", "wrench", null, 27, 128, "/config/settings", "Settings" },
                    { 38, "", "list", null, 37, 120, "/tadbir/reports", "ReportManagement" },
                    { 32, "Ctrl+Alt+L", "list", null, 27, 108, "/admin/operation-log", "OperationLogs" },
                    { 29, "Ctrl+Alt+H", "list", null, 27, 89, "/admin/roles", "Roles" },
                    { 28, "Ctrl+Alt+K", "user", null, 27, 82, "/admin/users", "Users" },
                    { 24, "Ctrl+Alt+C", "list", null, 23, 78, "/organization/companies", "Companies" },
                    { 25, "Ctrl+Alt+E", "list", null, 23, 70, "/organization/branches", "Branches" },
                    { 5, "Ctrl+Shift+D", "th", null, 2, 8, "/finance/detailAccount", "DetailAccount" },
                    { 21, "Ctrl+Alt+Z", "list", null, 20, 141, "/finance/journal", "JournalLedger" },
                    { 8, "Ctrl+Shift+R", "transfer", null, 2, 126, "/finance/accountrelations", "AccountRelations" },
                    { 59, "", "list", null, 53, 225, "/treasury/cash-register", "CashRegisters" },
                    { 100006, "", "list", null, 100001, 100015, "/product-scope/properties", "ProductProperty" },
                    { 100005, "", "list", null, 100001, 100008, "/product-scope/units", "Units" },
                    { 100004, "", "list", null, 100001, 100001, "/product-scope/brands", "Brands" },
                    { 67, "", "list", null, 63, 262, "/treasury/receipts/last", "LastReceiptForm" },
                    { 65, "", "plus", null, 63, 259, "/treasury/receipts/new", "NewReceiptForm" },
                    { 69, "", "search", null, 63, 257, "/treasury/receipts/by-no", "ReceiptFormbyNo" },
                    { 66, "", "list", null, 62, 251, "/treasury/payments/last", "LastPaymentForm" },
                    { 64, "", "plus", null, 62, 248, "/treasury/payments/new", "NewPaymentForm" },
                    { 68, "", "search", null, 62, 246, "/treasury/payments/by-no", "PaymentFormbyNo" },
                    { 61, "", "list", null, 53, 239, "/treasury/source-apps", "SourceApps" },
                    { 60, "", "", null, 55, 233, "/treasury/check-book-report", "CheckBookReport" },
                    { 22, "Ctrl+Alt+B", "list", null, 20, 147, "/finance/account-book", "AccountBook" },
                    { 100007, "", "list", null, 100001, 100022, "/product-scope/attributes", "ProductAttribute" }
                });

            migrationBuilder.InsertData(
                schema: "Metadata",
                table: "Command",
                columns: new[] { "CommandID", "HotKey", "IconName", "Index", "ParentId", "PermissionId", "RouteUrl", "TitleKey" },
                values: new object[,]
                {
                    { 56, "", "", null, 54, 215, "/treasury/check-books/new", "NewCheckBook" },
                    { 58, "", "", null, 54, 212, "/treasury/check-books/by-name", "CheckBookByName" },
                    { 51, "", "list", null, 37, 210, "/tadbir/dashboard", "ManageDashboard" },
                    { 50, "Ctrl+Shift+K", "list", null, 39, 201, "/finance/bal-sheet", "BalanceSheet" },
                    { 49, "Ctrl+Alt+Q", "list", null, 11, 189, "/finance/vouchers/last/draft", "LastDraftVoucher" },
                    { 47, "Ctrl+Alt+V", "list", null, 11, 180, "/finance/vouchers/new/draft", "NewDraftVoucher" },
                    { 48, "Ctrl+Alt+D", "list", null, 11, 179, "/finance/vouchers/by-no/draft", "DraftVoucherByNo" },
                    { 46, "Ctrl+Alt+R", "list", null, 39, 175, "/finance/profit-loss", "ProfitLoss" },
                    { 44, "Ctrl+Shift+B", "list", null, 39, 169, "/finance/balance-by-account", "BalanceByAccount" },
                    { 43, "Ctrl+Shift+I", "list", null, 39, 164, "/finance/itembalance", "ItemBalance" },
                    { 41, "Ctrl+Alt+J", "list", null, 20, 158, "/finance/currency-book", "CurrencyBook" },
                    { 40, "Ctrl+Alt+T", "list", null, 39, 153, "/finance/balance", "TestBalance" },
                    { 57, "", "", null, 54, 218, "/treasury/check-books/last", "LastCheckBook" }
                });

            migrationBuilder.InsertData(
                schema: "Reporting",
                table: "LocalReport",
                columns: new[] { "LocalReportID", "Caption", "LocaleId", "ReportId", "Template" },
                values: new object[,]
                {
                    { 100007, "گزارشات", 2, 100003, "" },
                    { 100006, "Reports", 1, 100003, "" },
                    { 100005, "اطلاعات عملیاتی", 2, 100002, "" },
                    { 100004, "Operational Data", 1, 100002, "" },
                    { 64, "Reports", 4, 16, "" },
                    { 61, "Reports", 1, 16, "" },
                    { 60, "Operational data", 4, 15, "" },
                    { 59, "Operational data", 3, 15, "" },
                    { 58, "اطلاعات عملیاتی", 2, 15, "" },
                    { 57, "Operational data", 1, 15, "" },
                    { 56, "Base data", 4, 14, "" },
                    { 55, "Base data", 3, 14, "" },
                    { 54, "اطلاعات پایه", 2, 14, "" },
                    { 53, "Base data", 1, 14, "" },
                    { 62, "گزارشات", 2, 16, "" },
                    { 16, "Reports", 4, 4, "" },
                    { 14, "گزارشات", 2, 4, "" },
                    { 13, "Reports", 1, 4, "" },
                    { 12, "Operational data", 4, 3, "" },
                    { 11, "Operational data", 3, 3, "" },
                    { 10, "اطلاعات عملیاتی", 2, 3, "" },
                    { 9, "Operational data", 1, 3, "" },
                    { 8, "Base data", 4, 2, "" },
                    { 7, "Base data", 3, 2, "" },
                    { 6, "اطلاعات پایه", 2, 2, "" },
                    { 15, "Reports", 3, 4, "" },
                    { 63, "Reports", 3, 16, "" },
                    { 5, "Base data", 1, 2, "" },
                    { 282, "اطلاعات عملیاتی", 2, 95, "" }
                });

            migrationBuilder.InsertData(
                schema: "Reporting",
                table: "LocalReport",
                columns: new[] { "LocalReportID", "Caption", "LocaleId", "ReportId", "Template" },
                values: new object[,]
                {
                    { 281, "Operational Data", 1, 95, "" },
                    { 100002, "Base Data", 1, 100001, "" },
                    { 280, "اطلاعات پایه", 2, 94, "" },
                    { 279, "Base Data", 1, 94, "" },
                    { 172, "Design template", 4, 43, "" },
                    { 100003, "اطلاعات پایه", 2, 100001, "" },
                    { 283, "Reports", 1, 96, "" },
                    { 171, "Design template", 3, 43, "" },
                    { 284, "گزارشات", 2, 96, "" },
                    { 169, "Design template", 1, 43, "" },
                    { 170, "طراحی قالب", 2, 43, "" }
                });

            migrationBuilder.InsertData(
                schema: "Reporting",
                table: "Report",
                columns: new[] { "ReportID", "Code", "CreatedById", "IsDefault", "IsDynamic", "IsGroup", "IsSystem", "ParentId", "ReportViewId", "ResourceKeys", "ServiceUrl", "SubsystemId", "ViewId" },
                values: new object[,]
                {
                    { 100006, "ProductScope-Report-QReport", 1, false, false, true, true, 100003, null, "", "", 100000, null },
                    { 100007, "", 1, true, true, false, true, 100001, null, "", "brands", 100000, 100001 },
                    { 5, "Admin-Base-QReport", 1, false, false, true, true, 2, null, "", "", 1, null },
                    { 100008, "", 1, true, true, false, true, 100001, null, "", "units", 3, 100002 },
                    { 6, "Admin-Operation-QReport", 1, false, false, true, true, 3, null, "", "", 1, null },
                    { 100005, "ProductScope-Operation-QReport", 1, false, false, true, true, 100002, null, "", "", 100000, null },
                    { 100009, "", 1, true, true, false, true, 100001, null, "", "", 3, 100003 },
                    { 100, "", 1, true, true, false, true, 96, null, "", "check-books", 3, 69 },
                    { 99, "Treasury-Report-QReport", 1, false, false, true, true, 96, null, "", "", 3, null },
                    { 100004, "ProductScope-Base-QReport", 1, false, false, true, true, 100001, null, "", "", 100000, null },
                    { 89, "Voucher-Summary-By-Date", 1, false, false, false, true, 16, null, "", "reports/finance/vouchers/sum-by-date", 2, 2 },
                    { 98, "Treasury-Operation-QReport", 1, false, false, true, true, 95, null, "", "", 3, null },
                    { 90, "Voucher-Summary-By-No", 1, false, false, false, true, 16, null, "", "reports/finance/vouchers/sum-by-no", 2, 2 },
                    { 17, "Accnt-Base-QReport", 1, false, false, true, true, 14, null, "", "", 2, null },
                    { 97, "Treasury-Base-QReport", 1, false, false, true, true, 94, null, "", "", 3, null },
                    { 18, "Accnt-Operation-QReport", 1, false, false, true, true, 15, null, "", "", 2, null },
                    { 20, "Voucher-Printing", 1, false, false, true, true, 15, null, "", "", 2, null },
                    { 100011, "", 1, true, true, false, true, 100001, null, "", "Files", 3, 100005 },
                    { 19, "Accnt-Report-QReport", 1, false, false, true, true, 16, null, "", "", 2, null },
                    { 85, "Journal-ByDate-ByLedger", 1, false, true, false, true, 16, null, "", "reports/journal/by-date/by-ledger", 2, 17 },
                    { 86, "Journal-ByDate-BySubsidiary", 1, false, true, false, true, 16, null, "", "reports/journal/by-date/by-subsid", 2, 18 },
                    { 87, "Journal-ByNo-ByLedger", 1, false, true, false, true, 16, null, "", "reports/journal/by-no/by-ledger", 2, 24 },
                    { 88, "Journal-ByNo-BySubsidiary", 1, false, true, false, true, 16, null, "", "reports/journal/by-no/by-subsid", 2, 25 },
                    { 7, "Admin-Report-QReport", 1, false, false, true, true, 4, null, "", "", 1, null },
                    { 100010, "", 1, true, true, false, true, 100001, null, "", "attributes", 3, 100004 }
                });

            migrationBuilder.InsertData(
                schema: "Reporting",
                table: "LocalReport",
                columns: new[] { "LocalReportID", "Caption", "LocaleId", "ReportId", "Template" },
                values: new object[,]
                {
                    { 17, "Quick Report", 1, 5, "" },
                    { 285, "Quick Report", 1, 97, "" },
                    { 272, "خلاصه اسناد حسابداری - بر اساس شماره سند", 2, 90, "" },
                    { 271, "Accounting Voucher Summary - By Voucher No", 1, 90, "" },
                    { 270, "خلاصه اسناد حسابداری - بر اساس تاریخ", 2, 89, "" },
                    { 269, "Accounting Voucher Summary - By Date", 1, 89, "" },
                    { 268, "دفتر روزنامه در سطح معین - بر اساس شماره سند", 2, 88, "" },
                    { 267, "Journal in Subsidiary Level - By Number", 1, 88, "" },
                    { 266, "دفتر روزنامه در سطح کل - بر اساس شماره سند", 2, 87, "" },
                    { 265, "Journal in Ledger Level - By Number", 1, 87, "" },
                    { 264, "دفتر روزنامه در سطح معین - بر اساس تاریخ", 2, 86, "" },
                    { 263, "Journal in Subsidiary Level - By Date", 1, 86, "" },
                    { 262, "دفتر روزنامه در سطح کل - بر اساس تاریخ", 2, 85, "" },
                    { 261, "Journal in Ledger Level - By Date", 1, 85, "" },
                    { 100012, "Quick Report", 1, 100006, "" },
                    { 76, "Quick Report", 4, 19, "" },
                    { 75, "Quick Report", 3, 19, "" },
                    { 74, "گزارش فوری", 2, 19, "" },
                    { 286, "گزارش فوری", 2, 97, "" },
                    { 73, "Quick Report", 1, 19, "" },
                    { 287, "Quick Report", 1, 98, "" },
                    { 289, "Quick Report", 1, 99, "" },
                    { 100011, "گزارش فوری", 2, 100005, "" },
                    { 100010, "Quick Report", 1, 100005, "" },
                    { 100023, "فهرست فایل ها", 2, 100011, "" },
                    { 100022, "File List", 1, 100011, "" },
                    { 100021, "لیست خصوصیت ها", 2, 100010, "" },
                    { 100020, "Attribute List", 1, 100010, "" },
                    { 100019, "لیست ویژگی ها", 2, 100009, "" },
                    { 100018, "Properties List", 1, 100009, "" },
                    { 100017, "لیست واحدها", 2, 100008, "" },
                    { 100016, "Unit list", 1, 100008, "" },
                    { 100015, "فهرست برندها", 2, 100007, "" },
                    { 100014, "Brnads List", 1, 100007, "" },
                    { 100009, "گزارش فوری", 2, 100004, "" },
                    { 100008, "Quick Report", 1, 100004, "" },
                    { 292, "دسته چک", 2, 100, "" },
                    { 291, "Check Book", 1, 100, "" },
                    { 290, "گزارش فوری", 2, 99, "" },
                    { 288, "گزارش فوری", 2, 98, "" },
                    { 80, "Voucher Printing", 4, 20, "" },
                    { 100013, "گزارش فوری", 2, 100006, "" }
                });

            migrationBuilder.InsertData(
                schema: "Reporting",
                table: "LocalReport",
                columns: new[] { "LocalReportID", "Caption", "LocaleId", "ReportId", "Template" },
                values: new object[,]
                {
                    { 72, "Quick Report", 4, 18, "" },
                    { 25, "Quick Report", 1, 7, "" },
                    { 71, "Quick Report", 3, 18, "" },
                    { 67, "Quick Report", 3, 17, "" },
                    { 28, "Quick Report", 4, 7, "" },
                    { 65, "Quick Report", 1, 17, "" },
                    { 24, "Quick Report", 4, 6, "" },
                    { 23, "Quick Report", 3, 6, "" },
                    { 22, "گزارش فوری", 2, 6, "" },
                    { 27, "Quick Report", 3, 7, "" },
                    { 21, "Quick Report", 1, 6, "" },
                    { 77, "Voucher Printing", 1, 20, "" },
                    { 78, "چاپ سند", 2, 20, "" },
                    { 69, "Quick Report", 1, 18, "" },
                    { 70, "گزارش فوری", 2, 18, "" },
                    { 66, "گزارش فوری", 2, 17, "" },
                    { 79, "Voucher Printing", 3, 20, "" },
                    { 20, "Quick Report", 4, 5, "" },
                    { 19, "Quick Report", 3, 5, "" },
                    { 18, "گزارش فوری", 2, 5, "" },
                    { 68, "Quick Report", 4, 17, "" },
                    { 26, "گزارش فوری", 2, 7, "" }
                });

            migrationBuilder.InsertData(
                schema: "Reporting",
                table: "Report",
                columns: new[] { "ReportID", "Code", "CreatedById", "IsDefault", "IsDynamic", "IsGroup", "IsSystem", "ParentId", "ReportViewId", "ResourceKeys", "ServiceUrl", "SubsystemId", "ViewId" },
                values: new object[,]
                {
                    { 40, "Voucher-Std-Form", 1, true, false, false, true, 20, null, "", "reports/finance/voucher-by-no/{0}/std-form", 2, 2 },
                    { 21, "Fiscal-Periods", 1, true, true, false, true, 17, null, "", "fperiods", 2, 9 },
                    { 74, "", 1, true, true, false, true, 17, null, "", "currencies", 2, 30 },
                    { 23, "Detail-Accounts", 1, true, true, false, true, 17, null, "", "faccounts", 2, 6 },
                    { 24, "Cost-Centers", 1, true, true, false, true, 17, null, "", "ccenters", 2, 7 },
                    { 25, "Projects", 1, true, true, false, true, 17, null, "", "projects", 2, 8 },
                    { 26, "Account-Groups", 1, true, true, false, true, 17, null, "", "accgroups", 2, 12 },
                    { 80, "BalanceSheet", 1, true, true, false, true, 19, null, "", "bal-sheet", 2, 67 },
                    { 22, "Accounts", 1, true, true, false, true, 17, null, "", "accounts", 2, 1 },
                    { 92, "", 1, false, true, false, true, 7, null, "", "dashboard/widgets/all", 1, 68 },
                    { 106, "", 1, true, true, false, true, 98, null, "", "payments/{0}/cash/articles", 3, 74 },
                    { 103, "", 1, true, true, false, true, 97, null, "", "source-apps", 3, 73 },
                    { 8, "Companies", 1, true, true, false, true, 5, null, "", "companies", 1, 11 },
                    { 9, "Branches", 1, true, true, false, true, 5, null, "", "branches", 1, 10 },
                    { 10, "Users", 1, true, true, false, true, 5, null, "", "users", 1, 4 },
                    { 11, "Roles", 1, true, true, false, true, 5, null, "", "roles", 1, 5 },
                    { 70, "", 1, true, true, false, true, 6, null, "", "oplog/archive", 1, 61 },
                    { 71, "", 1, true, true, false, true, 6, null, "", "sys-oplog", 1, 59 },
                    { 101, "", 1, true, true, false, true, 97, null, "", "cash-registers", 3, 70 },
                    { 72, "", 1, true, true, false, true, 6, null, "", "sys-oplog/archive", 1, 60 }
                });

            migrationBuilder.InsertData(
                schema: "Reporting",
                table: "Report",
                columns: new[] { "ReportID", "Code", "CreatedById", "IsDefault", "IsDynamic", "IsGroup", "IsSystem", "ParentId", "ReportViewId", "ResourceKeys", "ServiceUrl", "SubsystemId", "ViewId" },
                values: new object[,]
                {
                    { 102, "", 1, true, true, false, true, 99, null, "", "check-book-report", 3, 72 },
                    { 107, "", 1, true, true, false, true, 98, null, "", "receipts/{0}/cash/articles", 3, 75 },
                    { 79, "", 1, true, true, false, true, 19, null, "", "", 2, 66 },
                    { 105, "", 1, true, true, false, true, 98, null, "", "receipts/{0}/payer/articles", 3, 75 },
                    { 104, "", 1, true, true, false, true, 98, null, "", "payments/{0}/receiver/articles", 3, 74 },
                    { 91, "", 1, false, true, false, true, 7, null, "", "dashboard/widgets", 1, 68 },
                    { 73, "", 1, true, true, false, true, 6, null, "", "oplog", 1, 13 },
                    { 78, "", 1, true, true, false, true, 19, null, "", "", 2, 65 },
                    { 69, "", 1, true, true, false, true, 19, null, "", "", 2, 58 },
                    { 76, "", 1, true, true, false, true, 19, null, "", "", 2, 62 },
                    { 37, "Journal-ByNo-BySubsidiary", 1, true, true, false, true, 19, null, "", "reports/journal/by-no/by-subsid", 2, 25 },
                    { 36, "Journal-ByNo-ByLedger", 1, true, true, false, true, 19, null, "", "reports/journal/by-no/by-ledger", 2, 24 },
                    { 35, "Journal-ByNo-ByRow-Detail", 1, true, true, false, true, 19, null, "", "reports/journal/by-no/by-row-detail", 2, 23 },
                    { 34, "Journal-ByNo-ByRow", 1, true, true, false, true, 19, null, "", "reports/journal/by-no/by-row", 2, 22 },
                    { 33, "Journal-ByDate-LedgerSummary-ByMonth", 1, true, true, false, true, 19, null, "", "reports/journal/by-date/sum-by-month", 2, 21 },
                    { 32, "Journal-ByDate-LedgerSummary-ByDate", 1, true, true, false, true, 19, null, "", "reports/journal/by-date/sum-by-date", 2, 20 },
                    { 31, "Journal-ByDate-LedgerSummary", 1, true, true, false, true, 19, null, "", "reports/journal/by-date/summary", 2, 19 },
                    { 30, "Journal-ByDate-BySubsidiary", 1, true, true, false, true, 19, null, "", "reports/journal/by-date/by-subsid", 2, 18 },
                    { 29, "Journal-ByDate-ByLedger", 1, true, true, false, true, 19, null, "", "reports/journal/by-date/by-ledger", 2, 17 },
                    { 28, "Journal-ByDate-ByRow-Detail", 1, true, true, false, true, 19, null, "", "reports/journal/by-date/by-row-detail", 2, 16 },
                    { 27, "Journal-ByDate-ByRow", 1, true, true, false, true, 19, null, "", "reports/journal/by-date/by-row", 2, 15 },
                    { 75, "", 1, true, true, false, true, 18, null, "", "", 2, 31 },
                    { 109, "", 1, true, true, false, true, 20, null, "", "", 2, 41 },
                    { 108, "", 1, true, true, false, true, 20, null, "", "", 2, 42 },
                    { 84, "", 1, false, false, false, true, 20, null, "", "reports/finance/voucher-by-no/{0}/by-subsid", 2, 2 },
                    { 83, "", 1, false, false, false, true, 20, null, "", "reports/finance/voucher-by-no/{0}/by-ledger", 2, 2 },
                    { 82, "", 1, false, false, false, true, 20, null, "", "reports/finance/voucher-by-no/{0}/by-detail", 2, 2 },
                    { 81, "Vouchers", 1, true, true, false, true, 20, null, "", "", 2, 2 },
                    { 41, "Voucher-Std-Form-Detail", 1, false, false, false, true, 20, null, "", "reports/finance/voucher-by-no/{0}/std-form-detail", 2, 2 },
                    { 38, "Journal-ByNo-LedgerSummary", 1, true, true, false, true, 19, null, "", "reports/journal/by-no/summary", 2, 26 },
                    { 77, "", 1, true, true, false, true, 19, null, "", "", 2, 64 },
                    { 46, "TestBalance2Column", 1, true, true, false, true, 19, null, "", "", 2, 32 },
                    { 49, "TestBalance8Column", 1, true, true, false, true, 19, null, "", "", 2, 35 },
                    { 68, "", 1, true, true, false, true, 19, null, "", "", 2, 40 },
                    { 67, "", 1, true, true, false, true, 19, null, "", "", 2, 39 },
                    { 66, "", 1, true, true, false, true, 19, null, "", "", 2, 38 },
                    { 65, "", 1, true, true, false, true, 19, null, "", "", 2, 37 },
                    { 64, "", 1, true, true, false, true, 19, null, "", "", 2, 29 },
                    { 63, "", 1, true, true, false, true, 19, null, "", "", 2, 28 },
                    { 62, "", 1, true, true, false, true, 19, null, "", "", 2, 27 },
                    { 61, "", 1, true, true, false, true, 19, null, "", "", 2, 56 },
                    { 60, "", 1, true, true, false, true, 19, null, "", "", 2, 55 }
                });

            migrationBuilder.InsertData(
                schema: "Reporting",
                table: "Report",
                columns: new[] { "ReportID", "Code", "CreatedById", "IsDefault", "IsDynamic", "IsGroup", "IsSystem", "ParentId", "ReportViewId", "ResourceKeys", "ServiceUrl", "SubsystemId", "ViewId" },
                values: new object[,]
                {
                    { 59, "", 1, true, true, false, true, 19, null, "", "", 2, 54 },
                    { 58, "", 1, true, true, false, true, 19, null, "", "", 2, 53 },
                    { 57, "", 1, true, true, false, true, 19, null, "", "", 2, 51 },
                    { 56, "", 1, true, true, false, true, 19, null, "", "", 2, 50 },
                    { 55, "", 1, true, true, false, true, 19, null, "", "", 2, 49 },
                    { 54, "", 1, true, true, false, true, 19, null, "", "", 2, 48 },
                    { 53, "", 1, true, true, false, true, 19, null, "", "", 2, 46 },
                    { 52, "", 1, true, true, false, true, 19, null, "", "", 2, 45 },
                    { 51, "", 1, true, true, false, true, 19, null, "", "", 2, 44 },
                    { 50, "", 1, true, true, false, true, 19, null, "", "", 2, 43 },
                    { 47, "TestBalance4Column", 1, true, true, false, true, 19, null, "", "", 2, 33 },
                    { 48, "TestBalance6Column", 1, true, true, false, true, 19, null, "", "", 2, 34 }
                });

            migrationBuilder.InsertData(
                schema: "Reporting",
                table: "LocalReport",
                columns: new[] { "LocalReportID", "Caption", "LocaleId", "ReportId", "Template" },
                values: new object[,]
                {
                    { 29, "Companies", 1, 8, "" },
                    { 176, "Test balance 2 columns", 4, 46, "" },
                    { 177, "Test balance 4 columns", 1, 47, "" },
                    { 178, "تراز آزمایشی ۴ ستونی", 2, 47, "" },
                    { 179, "Test balance 4 columns", 3, 47, "" },
                    { 180, "Test balance 4 columns", 4, 47, "" },
                    { 181, "Test balance 6 columns", 1, 48, "" },
                    { 182, "تراز آزمایشی ۶ ستونی", 2, 48, "" },
                    { 183, "Test balance 6 columns", 3, 48, "" },
                    { 184, "Test balance 6 columns", 4, 48, "" },
                    { 185, "Test balance 8 columns", 1, 49, "" },
                    { 186, "تراز آزمایشی ۸ ستونی", 2, 49, "" },
                    { 187, "Test balance 8 columns", 3, 49, "" },
                    { 188, "Test balance 8 columns", 4, 49, "" },
                    { 189, "Detail account turnover/balance - 2 column", 1, 50, "" },
                    { 190, "گردش و مانده تفصیلی شناور 2 ستونی", 2, 50, "" },
                    { 191, "Detail account turnover/balance - 4 column", 1, 51, "" },
                    { 192, "گردش و مانده تفصیلی شناور 4 ستونی", 2, 51, "" },
                    { 193, "Detail account turnover/balance - 6 column", 1, 52, "" },
                    { 194, "گردش و مانده تفصیلی شناور 6 ستونی", 2, 52, "" },
                    { 195, "Detail account turnover/balance - 8 column", 1, 53, "" },
                    { 196, "گردش و مانده تفصیلی شناور 8 ستونی", 2, 53, "" },
                    { 175, "Test balance 2 columns", 3, 46, "" },
                    { 197, "Cost center turnover/balance - 2 column", 1, 54, "" },
                    { 174, "تراز آزمایشی ۲ ستونی", 2, 46, "" },
                    { 152, "Journal, by number, ledger summary", 4, 38, "" },
                    { 131, "Journal, by date, summary by month", 3, 33, "" },
                    { 132, "Journal, by date, summary by month", 4, 33, "" },
                    { 133, "Journal, by number, by row", 1, 34, "" },
                    { 134, "دفتر روزنامه، بر حسب شماره سند، مطابق ردیف های سند", 2, 34, "" },
                    { 135, "Journal, by number, by row", 3, 34, "" },
                    { 136, "Journal, by number, by row", 4, 34, "" },
                    { 137, "Journal, by number, by row with details", 1, 35, "" },
                    { 138, "دفتر روزنامه، بر حسب شماره سند، مطابق ردیف های سند با سطوح شناور", 2, 35, "" },
                    { 139, "Journal, by number, by row with details", 3, 35, "" },
                    { 140, "Journal, by number, by row with details", 4, 35, "" },
                    { 141, "Journal, by number, by ledger", 1, 36, "" },
                    { 142, "دفتر روزنامه، بر حسب شماره سند، در سطح کل", 2, 36, "" },
                    { 143, "Journal, by number, by ledger", 3, 36, "" },
                    { 144, "Journal, by number, by ledger", 4, 36, "" },
                    { 145, "Journal, by number, by subsidiary", 1, 37, "" },
                    { 146, "دفتر روزنامه، بر حسب شماره سند، در سطح معین", 2, 37, "" }
                });

            migrationBuilder.InsertData(
                schema: "Reporting",
                table: "LocalReport",
                columns: new[] { "LocalReportID", "Caption", "LocaleId", "ReportId", "Template" },
                values: new object[,]
                {
                    { 147, "Journal, by number, by subsidiary", 3, 37, "" },
                    { 148, "Journal, by number, by subsidiary", 4, 37, "" },
                    { 149, "Journal, by number, ledger summary", 1, 38, "" },
                    { 150, "دفتر روزنامه، بر حسب شماره سند، سند خلاصه", 2, 38, "" },
                    { 151, "Journal, by number, ledger summary", 3, 38, "" },
                    { 173, "Test balance 2 columns", 1, 46, "" },
                    { 130, "دفتر روزنامه، بر حسب تاریخ، سند خلاصه به تفکیک ماه", 2, 33, "" },
                    { 198, "گردش و مانده مرکز هزینه 2 ستونی", 2, 54, "" },
                    { 200, "گردش و مانده مرکز هزینه 4 ستونی", 2, 55, "" },
                    { 226, "دفتر عملیات ارزی", 2, 68, "" },
                    { 227, "Balance by account", 1, 69, "" },
                    { 228, "مانده به تفکیک حساب", 2, 69, "" },
                    { 241, "Profit-Loss", 1, 76, "" },
                    { 242, "سود و زیان", 2, 76, "" },
                    { 243, "Profit-Loss", 1, 77, "" },
                    { 244, "سود و زیان", 2, 77, "" },
                    { 249, "BalanceSheet", 1, 80, "" },
                    { 250, "ترازنامه", 2, 80, "" },
                    { 293, "Cash Register List", 1, 101, "" },
                    { 294, "فهرست صندوق‌های اسناد", 2, 101, "" },
                    { 297, "Source and Application List", 1, 103, "" },
                    { 298, "فهرست منابع و مصارف", 2, 103, "" },
                    { 299, "Recipients List", 1, 104, "" },
                    { 300, "لیست دریافت کنندگان", 2, 104, "" },
                    { 301, "Payers List", 1, 105, "" },
                    { 302, "لیست پرداخت کنندگان", 2, 105, "" },
                    { 303, "Cash Accounts List", 1, 106, "" },
                    { 304, "لیست حساب‌های نقدی", 2, 106, "" },
                    { 305, "Cash Accounts List", 1, 107, "" },
                    { 306, "لیست حساب‌های نقدی", 2, 107, "" },
                    { 225, "Currency Book", 1, 68, "" },
                    { 199, "Cost center turnover/balance - 4 column", 1, 55, "" },
                    { 224, "دفتر عملیات ارزی", 2, 67, "" },
                    { 222, "دفتر عملیات ارزی", 2, 66, "" },
                    { 201, "Cost center turnover/balance - 6 column", 1, 56, "" },
                    { 202, "گردش و مانده مرکز هزینه 6 ستونی", 2, 56, "" },
                    { 203, "Cost center turnover/balance - 8 column", 1, 57, "" },
                    { 204, "گردش و مانده مرکز هزینه 8 ستونی", 2, 57, "" },
                    { 205, "Project turnover/balance - 2 column", 1, 58, "" },
                    { 206, "گردش و مانده پروژه 2 ستونی", 2, 58, "" },
                    { 207, "Project turnover/balance - 4 column", 1, 59, "" },
                    { 208, "گردش و مانده پروژه 4 ستونی", 2, 59, "" }
                });

            migrationBuilder.InsertData(
                schema: "Reporting",
                table: "LocalReport",
                columns: new[] { "LocalReportID", "Caption", "LocaleId", "ReportId", "Template" },
                values: new object[,]
                {
                    { 209, "Project turnover/balance - 6 column", 1, 60, "" },
                    { 210, "گردش و مانده پروژه 6 ستونی", 2, 60, "" },
                    { 211, "Project turnover/balance - 8 column", 1, 61, "" },
                    { 212, "گردش و مانده پروژه 8 ستونی", 2, 61, "" },
                    { 213, "Account Book", 1, 62, "" },
                    { 214, "دفتر حساب", 2, 62, "" },
                    { 215, "Account Book", 1, 63, "" },
                    { 216, "دفتر حساب", 2, 63, "" },
                    { 217, "Account Book", 1, 64, "" },
                    { 218, "ذفتر حساب", 2, 64, "" },
                    { 219, "Currency Book", 1, 65, "" },
                    { 220, "دفتر عملیات ارزی", 2, 65, "" },
                    { 221, "Currency Book", 1, 66, "" },
                    { 223, "Currency Book", 1, 67, "" },
                    { 129, "Journal, by date, summary by month", 1, 33, "" },
                    { 128, "Journal, by date, summary by date", 4, 32, "" },
                    { 127, "Journal, by date, summary by date", 3, 32, "" },
                    { 275, "All Widgets", 1, 92, "" },
                    { 276, "همه ویجت ها", 2, 92, "" },
                    { 81, "Fiscal periods", 1, 21, "" },
                    { 82, "دوره های مالی", 2, 21, "" },
                    { 83, "Fiscal periods", 3, 21, "" },
                    { 84, "Fiscal periods", 4, 21, "" },
                    { 85, "Accounts", 1, 22, "" },
                    { 86, "سرفصل های حسابداری", 2, 22, "" },
                    { 87, "Accounts", 3, 22, "" },
                    { 88, "Accounts", 4, 22, "" },
                    { 89, "Detail accounts", 1, 23, "" },
                    { 90, "تفصیلی های شناور", 2, 23, "" },
                    { 91, "Detail accounts", 3, 23, "" },
                    { 92, "Detail accounts", 4, 23, "" },
                    { 93, "Cost centers", 1, 24, "" },
                    { 94, "مراکز هزینه", 2, 24, "" },
                    { 95, "Cost centers", 3, 24, "" },
                    { 96, "Cost centers", 4, 24, "" },
                    { 97, "Projects", 1, 25, "" },
                    { 98, "پروژه ها", 2, 25, "" },
                    { 99, "Projects", 3, 25, "" },
                    { 274, "ویجت های من", 2, 91, "" },
                    { 100, "Projects", 4, 25, "" },
                    { 273, "My Widgets", 1, 91, "" },
                    { 235, "Active operation logs", 1, 73, "" }
                });

            migrationBuilder.InsertData(
                schema: "Reporting",
                table: "LocalReport",
                columns: new[] { "LocalReportID", "Caption", "LocaleId", "ReportId", "Template" },
                values: new object[,]
                {
                    { 30, "شرکت ها", 2, 8, "" },
                    { 31, "Companies", 3, 8, "" },
                    { 32, "Companies", 4, 8, "" },
                    { 33, "Branches", 1, 9, "" },
                    { 34, "شعب سازمانی", 2, 9, "" },
                    { 35, "Branches", 3, 9, "" },
                    { 36, "Branches", 4, 9, "" },
                    { 37, "Users", 1, 10, "" },
                    { 38, "کاربران", 2, 10, "" },
                    { 39, "Users", 3, 10, "" },
                    { 40, "Users", 4, 10, "" },
                    { 41, "Roles", 1, 11, "" },
                    { 42, "نقش ها", 2, 11, "" },
                    { 43, "Roles", 3, 11, "" },
                    { 44, "Roles", 4, 11, "" },
                    { 229, "Archived operation logs", 1, 70, "" },
                    { 230, "رویدادهای عملیاتی بایگانی شده", 2, 70, "" },
                    { 231, "Active system logs", 1, 71, "" },
                    { 232, "رویدادهای سیستمی فعال", 2, 71, "" },
                    { 233, "Archived system logs", 1, 72, "" },
                    { 234, "رویدادهای سیستمی بایگانی شده", 2, 72, "" },
                    { 236, "رویدادهای عملیاتی فعال", 2, 73, "" },
                    { 101, "Account groups", 1, 26, "" },
                    { 102, "گروه های حساب", 2, 26, "" },
                    { 103, "Account groups", 3, 26, "" },
                    { 106, "دفتر روزنامه، بر حسب تاریخ، مطابق ردیف های سند", 2, 27, "" },
                    { 107, "Journal, by date, by row", 3, 27, "" },
                    { 108, "Journal, by date, by row", 4, 27, "" },
                    { 109, "Journal, by date, by row with details", 1, 28, "" },
                    { 110, "دفتر روزنامه، بر حسب تاریخ، مطابق ردیف های سند با سطوح شناور", 2, 28, "" },
                    { 111, "Journal, by date, by row with details", 3, 28, "" },
                    { 112, "Journal, by date, by row with details", 4, 28, "" },
                    { 113, "Journal, by date, by ledger", 1, 29, "" },
                    { 114, "دفتر روزنامه، بر حسب تاریخ، در سطح کل", 2, 29, "" },
                    { 115, "Journal, by date, by ledger", 3, 29, "" },
                    { 116, "Journal, by date, by ledger", 4, 29, "" },
                    { 117, "Journal, by date, by subsidiary", 1, 30, "" },
                    { 118, "دفتر روزنامه، بر حسب تاریخ، در سطح معین", 2, 30, "" },
                    { 119, "Journal, by date, by subsidiary", 3, 30, "" },
                    { 120, "Journal, by date, by subsidiary", 4, 30, "" },
                    { 121, "Journal, by date, ledger summary", 1, 31, "" },
                    { 122, "دفتر روزنامه، بر حسب تاریخ، سند خلاصه", 2, 31, "" }
                });

            migrationBuilder.InsertData(
                schema: "Reporting",
                table: "LocalReport",
                columns: new[] { "LocalReportID", "Caption", "LocaleId", "ReportId", "Template" },
                values: new object[,]
                {
                    { 123, "Journal, by date, ledger summary", 3, 31, "" },
                    { 124, "Journal, by date, ledger summary", 4, 31, "" },
                    { 125, "Journal, by date, summary by date", 1, 32, "" },
                    { 126, "دفتر روزنامه، بر حسب تاریخ، سند خلاصه به تفکیک تاریخ", 2, 32, "" },
                    { 105, "Journal, by date, by row", 1, 27, "" },
                    { 310, "شماره سندهای مفقود", 2, 109, "" },
                    { 309, "Missing Voucher Numbers", 1, 109, "" },
                    { 308, "آرتیکل‌های سند", 2, 108, "" },
                    { 104, "Account groups", 4, 26, "" },
                    { 237, "Currencies", 1, 74, "" },
                    { 238, "ارزها", 2, 74, "" },
                    { 239, "Currency rates", 1, 75, "" },
                    { 240, "نرخ های ارز", 2, 75, "" },
                    { 157, "Voucher, standard format", 1, 40, "" },
                    { 158, "فرم مرسوم سند", 2, 40, "" },
                    { 159, "Voucher, standard format", 3, 40, "" },
                    { 160, "Voucher, standard format", 4, 40, "" },
                    { 161, "Voucher, standard format, with detail", 1, 41, "" },
                    { 295, "Check Book Report", 1, 102, "" },
                    { 162, "فرم مرسوم سند - با سطوح شناور", 2, 41, "" },
                    { 164, "Voucher, standard format, with detail", 4, 41, "" },
                    { 251, "Vouchers", 1, 81, "" },
                    { 252, "اسناد مالی", 2, 81, "" },
                    { 255, "Simple - by detail level", 1, 82, "" },
                    { 256, "ساده - در سطح تفصیلی", 2, 82, "" },
                    { 257, "Aggregate - by ledger level", 1, 83, "" },
                    { 258, "مرکب - در سطح کل", 2, 83, "" },
                    { 259, "Aggregate - by subsidiary level", 1, 84, "" },
                    { 260, "مرکب - در سطح معین", 2, 84, "" },
                    { 307, "Voucher Lines", 1, 108, "" },
                    { 163, "Voucher, standard format, with detail", 3, 41, "" },
                    { 296, "دفتر دسته‌چک", 2, 102, "" }
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
                name: "Version",
                schema: "Core");

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
