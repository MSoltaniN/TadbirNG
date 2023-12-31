USE master
GO

CREATE DATABASE @SysDbName
GO

ALTER DATABASE [@SysDbName] SET COMPATIBILITY_LEVEL = 130
GO

ALTER DATABASE SCOPED CONFIGURATION SET IDENTITY_CACHE=OFF
GO

ALTER AUTHORIZATION ON DATABASE::@SysDbName TO @LoginName;
GO

USE [@SysDbName]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE SCHEMA [Core]
GO

CREATE SCHEMA [Metadata]
GO

CREATE SCHEMA [Auth]
GO

CREATE SCHEMA [Reporting]
GO

CREATE SCHEMA [Config]
GO

CREATE SCHEMA [Contact]
GO

CREATE TABLE [Core].[Version] (
    [VersionID]      INT              NOT NULL,
    [Number]         VARCHAR(16)      NOT NULL,
    [rowguid]        UNIQUEIDENTIFIER CONSTRAINT [DF_Core_Version_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]   DATETIME         CONSTRAINT [DF_Core_Version_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Core_Version] PRIMARY KEY CLUSTERED ([VersionID] ASC)
)
GO

CREATE TABLE [Metadata].[Subsystem] (
    [SubsystemID]    INT              IDENTITY (1, 1) NOT NULL,
    [Name]           NVARCHAR(64)     NOT NULL,
    [rowguid]        UNIQUEIDENTIFIER CONSTRAINT [DF_Metadata_Subsystem_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]   DATETIME         CONSTRAINT [DF_Metadata_Subsystem_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Metadata_Subsystem] PRIMARY KEY CLUSTERED ([SubsystemID] ASC)
)
GO

CREATE TABLE [Metadata].[EntityType] (
    [EntityTypeID]   INT              IDENTITY (1, 1) NOT NULL,
    [Name]           NVARCHAR(128)    NOT NULL,
    [Description]    NVARCHAR(512)    NULL,
    [rowguid]        UNIQUEIDENTIFIER CONSTRAINT [DF_Metadata_EntityType_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]   DATETIME         CONSTRAINT [DF_Metadata_EntityType_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Metadata_EntityType] PRIMARY KEY CLUSTERED ([EntityTypeID] ASC)
)
GO

CREATE TABLE [Metadata].[Operation] (
    [OperationID]    INT              IDENTITY (1, 1) NOT NULL,
    [Name]           NVARCHAR(128)    NOT NULL,
    [Description]    NVARCHAR(512)    NULL,
    [rowguid]        UNIQUEIDENTIFIER CONSTRAINT [DF_Metadata_Operation_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]   DATETIME         CONSTRAINT [DF_Metadata_Operation_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Metadata_Operation] PRIMARY KEY CLUSTERED ([OperationID] ASC)
)
GO

CREATE TABLE [Metadata].[OperationSource] (
    [OperationSourceID]   INT              IDENTITY (1, 1) NOT NULL,
    [Name]                NVARCHAR(128)    NOT NULL,
    [Description]         NVARCHAR(512)    NULL,
    [rowguid]             UNIQUEIDENTIFIER CONSTRAINT [DF_Metadata_OperationSource_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]        DATETIME         CONSTRAINT [DF_Metadata_OperationSource_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Metadata_OperationSource] PRIMARY KEY CLUSTERED ([OperationSourceID] ASC)
)
GO

CREATE TABLE [Metadata].[OperationSourceType] (
    [OperationSourceTypeID]   INT              IDENTITY (1, 1) NOT NULL,
    [Name]                    NVARCHAR(64)     NOT NULL,
    [rowguid]                 UNIQUEIDENTIFIER CONSTRAINT [DF_Metadata_OperationSourceType_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]            DATETIME         CONSTRAINT [DF_Metadata_OperationSourceType_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Metadata_OperationSourceType] PRIMARY KEY CLUSTERED ([OperationSourceTypeID] ASC)
)
GO

CREATE TABLE [Metadata].[View] (
    [ViewID]                 INT              IDENTITY (1, 1) NOT NULL,
    [Name]                   VARCHAR(64)      NOT NULL,
    [EntityName]             NVARCHAR(64)     NOT NULL,
    [EntityType]             NVARCHAR(32)     NULL,
    [IsHierarchy]            BIT              NOT NULL,
    [IsCartableIntegrated]   BIT              NOT NULL,
    [FetchUrl]               NVARCHAR(512)    NULL,
    [SearchUrl]              NVARCHAR(512)    NULL,
    [rowguid]                UNIQUEIDENTIFIER CONSTRAINT [DF_Metadata_View_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]           DATETIME         CONSTRAINT [DF_Metadata_View_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Metadata_View] PRIMARY KEY CLUSTERED ([ViewID] ASC)
)
GO

CREATE TABLE [Metadata].[Column] (
    [ColumnID]         INT              IDENTITY (1, 1) NOT NULL,
    [ViewID]           INT              NOT NULL,
    [Name]             VARCHAR(64)      NOT NULL,
    [GroupName]        VARCHAR(64)      NULL,
    [Type]             VARCHAR(32)      NULL,
    [DotNetType]       VARCHAR(64)      NOT NULL,
    [StorageType]      VARCHAR(32)      NOT NULL,
    [ScriptType]       VARCHAR(32)      NOT NULL,
    [Length]           INT              NOT NULL,
    [MinLength]        INT              NOT NULL,
    [IsDynamic]        BIT              CONSTRAINT [DF_Metadata_Column_IsDynamic] DEFAULT (0) NOT NULL,
    [IsFixedLength]    BIT              NOT NULL,
    [IsNullable]       BIT              NOT NULL,
    [AllowSorting]     BIT              NOT NULL,
    [AllowFiltering]   BIT              NOT NULL,
	[Visibility]       NVARCHAR(32)     NULL,
	[DisplayIndex]     SMALLINT         NOT NULL,
    [Expression]       VARCHAR(64)      NULL,
    [rowguid]          UNIQUEIDENTIFIER CONSTRAINT [DF_Metadata_Column_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]     DATETIME         CONSTRAINT [DF_Metadata_Column_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Metadata_Column] PRIMARY KEY CLUSTERED ([ColumnID] ASC)
    , CONSTRAINT [FK_Metadata_Column_Metadata_View] FOREIGN KEY ([ViewID]) REFERENCES [Metadata].[View]([ViewID])
)
GO

CREATE TABLE [Metadata].[Locale] (
    [LocaleID]       INT              IDENTITY (1, 1) NOT NULL,
    [Name]           VARCHAR(64)      NOT NULL,
    [LocalName]      NVARCHAR(64)     NOT NULL,
    [Code]           VARCHAR(16)      NOT NULL,
    [rowguid]        UNIQUEIDENTIFIER CONSTRAINT [DF_Metadata_Locale_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]   DATETIME         CONSTRAINT [DF_Metadata_Locale_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Metadata_Locale] PRIMARY KEY CLUSTERED ([LocaleID] ASC)
)
GO

CREATE TABLE [Config].[SysLogSetting] (
    [SysLogSettingID]   INT              IDENTITY (1, 1) NOT NULL,
    [SourceID]          INT              NULL,
    [EntityTypeID]      INT              NULL,
    [OperationID]       INT              NOT NULL,
    [IsEnabled]         BIT              NOT NULL,
    [rowguid]           UNIQUEIDENTIFIER CONSTRAINT [DF_Config_SysLogSetting_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]      DATETIME         CONSTRAINT [DF_Config_SysLogSetting_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Config_SysLogSetting] PRIMARY KEY CLUSTERED ([SysLogSettingID] ASC)
    , CONSTRAINT [FK_Config_SysLogSetting_Metadata_Source] FOREIGN KEY ([SourceID]) REFERENCES [Metadata].[OperationSource]([OperationSourceID])
    , CONSTRAINT [FK_Config_SysLogSetting_Metadata_EntityType] FOREIGN KEY ([EntityTypeID]) REFERENCES [Metadata].[EntityType]([EntityTypeID])
    , CONSTRAINT [FK_Config_SysLogSetting_Metadata_Operation] FOREIGN KEY ([OperationID]) REFERENCES [Metadata].[Operation]([OperationID])
)
GO

CREATE TABLE [Auth].[User] (
    [UserID]         INT              IDENTITY (1, 1) NOT NULL,
    [UserName]       NVARCHAR(64)     NOT NULL,
    [PasswordHash]   VARCHAR(256)     NOT NULL,
    [LastLoginDate]  DATETIME         NULL,
    [IsEnabled]      BIT              NOT NULL,
    [rowguid]        UNIQUEIDENTIFIER CONSTRAINT [DF_Auth_User_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]   DATETIME         CONSTRAINT [DF_Auth_User_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Auth_User] PRIMARY KEY CLUSTERED ([UserID] ASC)
)
GO

CREATE TABLE [Contact].[Person] (
    [PersonID]       INT              IDENTITY (1, 1) NOT NULL,
	[UserID]         INT              NOT NULL,
    [FirstName]      NVARCHAR(64)     NOT NULL,
    [LastName]       NVARCHAR(64)     NOT NULL,
    [rowguid]        UNIQUEIDENTIFIER CONSTRAINT [DF_Contact_Person_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]   DATETIME         CONSTRAINT [DF_Contact_Person_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Contact_Person] PRIMARY KEY CLUSTERED ([PersonID] ASC)
    , CONSTRAINT [FK_Contact_Person_Auth_User] FOREIGN KEY ([UserID]) REFERENCES [Auth].[User] ([UserID])
)
GO

CREATE TABLE [Auth].[Role] (
    [RoleID]         INT              IDENTITY (1, 1) NOT NULL,
    [Name]           NVARCHAR(64)     NOT NULL,
    [Description]    NVARCHAR(512)    NULL,
    [rowguid]        UNIQUEIDENTIFIER CONSTRAINT [DF_Auth_Role_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]   DATETIME         CONSTRAINT [DF_Auth_Role_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Auth_Role] PRIMARY KEY CLUSTERED ([RoleID] ASC)
)
GO

CREATE TABLE [Auth].[UserRole] (
    [UserRoleID]     INT              IDENTITY (1, 1) NOT NULL,
    [UserID]         INT              NOT NULL,
    [RoleID]         INT              NOT NULL,
    [rowguid]        UNIQUEIDENTIFIER CONSTRAINT [DF_Auth_UserRole_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]   DATETIME         CONSTRAINT [DF_Auth_UserRole_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Auth_UserRole] PRIMARY KEY CLUSTERED ([UserRoleID] ASC)
    , CONSTRAINT [FK_Auth_UserRole_Auth_User] FOREIGN KEY ([UserID]) REFERENCES [Auth].[User] ([UserID])
    , CONSTRAINT [FK_Auth_UserRole_Auth_Role] FOREIGN KEY ([RoleID]) REFERENCES [Auth].[Role] ([RoleID])
)
GO

CREATE TABLE [Auth].[PermissionGroup] (
    [PermissionGroupID]   INT              IDENTITY (1, 1) NOT NULL,
    [SubsystemID]         INT              NOT NULL,
    [SourceTypeID]        INT              NOT NULL,
    [Name]                NVARCHAR(64)     NOT NULL,
    [EntityName]          NVARCHAR(64)     NULL,
    [Description]         NVARCHAR(512)    NULL,
    [rowguid]             UNIQUEIDENTIFIER CONSTRAINT [DF_Auth_PermissionGroup_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]        DATETIME         CONSTRAINT [DF_Auth_PermissionGroup_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Auth_PermissionGroup] PRIMARY KEY CLUSTERED ([PermissionGroupID] ASC)
    , CONSTRAINT [FK_Auth_PermissionGroup_Metadata_Subsystem] FOREIGN KEY ([SubsystemID]) REFERENCES [Metadata].[Subsystem]([SubsystemID])
    , CONSTRAINT [FK_Auth_PermissionGroup_Metadata_SourceType] FOREIGN KEY ([SourceTypeID]) REFERENCES [Metadata].[OperationSourceType]([OperationSourceTypeID])
)
GO

CREATE TABLE [Auth].[Permission] (
    [PermissionID]   INT              IDENTITY (1, 1) NOT NULL,
	[GroupID]        INT              NOT NULL,
    [Name]           NVARCHAR(128)    NOT NULL,
    [Flag]           INT              CONSTRAINT [DF_Auth_Permission_Flag] DEFAULT (0) NOT NULL,
    [Description]    NVARCHAR(512)    NULL,
    [rowguid]        UNIQUEIDENTIFIER CONSTRAINT [DF_Auth_Permission_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]   DATETIME         CONSTRAINT [DF_Auth_Permission_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Auth_Permission] PRIMARY KEY CLUSTERED ([PermissionID] ASC)
    , CONSTRAINT [FK_Auth_Permission_Auth_PermissionGroup] FOREIGN KEY ([GroupID]) REFERENCES [Auth].[PermissionGroup] ([PermissionGroupID])
)
GO

CREATE TABLE [Auth].[RolePermission] (
    [RolePermissionID]   INT              IDENTITY (1, 1) NOT NULL,
    [RoleID]             INT              NOT NULL,
    [PermissionID]       INT              NOT NULL,
    [rowguid]            UNIQUEIDENTIFIER CONSTRAINT [DF_Auth_RolePermission_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]       DATETIME         CONSTRAINT [DF_Auth_RolePermission_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Auth_RolePermission] PRIMARY KEY CLUSTERED ([RolePermissionID] ASC)
    , CONSTRAINT [FK_Auth_RolePermission_Auth_Role] FOREIGN KEY ([RoleID]) REFERENCES [Auth].[Role] ([RoleID])
    , CONSTRAINT [FK_Auth_RolePermission_Auth_Permission] FOREIGN KEY ([PermissionID]) REFERENCES [Auth].[Permission] ([PermissionID])
    , CONSTRAINT [UK_RolePermission] UNIQUE NONCLUSTERED ([RoleID] ASC, [PermissionID] ASC)
)
GO

CREATE TABLE [Auth].[ViewRowPermission] (
    [RowPermissionID]   INT              IDENTITY (1, 1) NOT NULL,
    [RoleID]            INT              NOT NULL,
    [ViewID]            INT              NOT NULL,
    [AccessMode]        NVARCHAR(64)     NOT NULL,
    [Value]             FLOAT            CONSTRAINT [DF_Auth_ViewRowPermission_Value] DEFAULT (0.0) NOT NULL,
    [Value2]            FLOAT            CONSTRAINT [DF_Auth_ViewRowPermission_Value2] DEFAULT (0.0) NOT NULL,
    [TextValue]         NVARCHAR(64)     NULL,
    [Items]             VARCHAR(2048)    NULL,
    [rowguid]           UNIQUEIDENTIFIER CONSTRAINT [DF_Auth_ViewRowPermission_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]      DATETIME         CONSTRAINT [DF_Auth_ViewRowPermission_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Auth_ViewRowPermission] PRIMARY KEY CLUSTERED ([RowPermissionID] ASC)
    , CONSTRAINT [FK_Auth_ViewRowPermission_Auth_Role] FOREIGN KEY ([RoleID]) REFERENCES [Auth].[Role]([RoleID])
    , CONSTRAINT [FK_Auth_ViewRowPermission_Metadata_View] FOREIGN KEY ([ViewID]) REFERENCES [Metadata].[View]([ViewID])
)
GO

CREATE TABLE [Metadata].[ValidRowPermission] (
    [RowPermissionID]        INT              IDENTITY (1, 1) NOT NULL,
    [ViewID]                 INT              NOT NULL,
    [AccessMode]             NVARCHAR(64)     NOT NULL,
    [rowguid]                UNIQUEIDENTIFIER CONSTRAINT [DF_Metadata_ValidRowPermission_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]           DATETIME         CONSTRAINT [DF_Metadata_ValidRowPermission_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Metadata_ValidRowPermission] PRIMARY KEY CLUSTERED ([RowPermissionID] ASC)
    , CONSTRAINT [FK_Metadata_ValidRowPermission_Metadata_View] FOREIGN KEY ([ViewID]) REFERENCES [Metadata].[View]([ViewID])
)
GO

CREATE TABLE [Auth].[Session] (
    [SessionID]       INT              IDENTITY (1, 1) NOT NULL,
    [UserID]          INT              NOT NULL,
    [Device]          NVARCHAR(64)     NOT NULL,
    [Browser]         NVARCHAR(64)     NOT NULL,
    [Fingerprint]     NVARCHAR(128)    NOT NULL,
    [IPAddress]       NVARCHAR(32)     NULL,
    [SinceUtc]        DATETIME         NOT NULL,
    [LastActivityUtc] DATETIME         NOT NULL,
    [TimeZone]        NVARCHAR(32)     NULL,
    [rowguid]         UNIQUEIDENTIFIER CONSTRAINT [DF_Auth_Session_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]    DATETIME         CONSTRAINT [DF_Auth_Session_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Auth_Session] PRIMARY KEY CLUSTERED ([SessionID] ASC)
    , CONSTRAINT [FK_Auth_Session_Auth_User] FOREIGN KEY ([UserID]) REFERENCES [Auth].[User]([UserID])
)
GO

CREATE TABLE [Reporting].[Report] (
    [ReportID]       INT              IDENTITY (1, 1) NOT NULL,
    [ParentID]       INT              NULL,
    [CreatedByID]    INT              NOT NULL,
    [ViewID]         INT              NULL,
    [SubsystemID]    INT              NOT NULL,
    [Code]           NVARCHAR(128)    NOT NULL,
    [ServiceUrl]     NVARCHAR(256)    NULL,
    [IsGroup]        BIT              NOT NULL,
    [IsSystem]       BIT              NOT NULL,
    [IsDefault]      BIT              NOT NULL,
    [IsDynamic]      BIT              CONSTRAINT [DF_Reporting_Report_IsDynamic] DEFAULT (0) NOT NULL,
    [ResourceKeys]   VARCHAR(MAX)     NULL,
    [rowguid]        UNIQUEIDENTIFIER CONSTRAINT [DF_Reporting_Report_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]   DATETIME         CONSTRAINT [DF_Reporting_Report_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Reporting_Report] PRIMARY KEY CLUSTERED ([ReportID] ASC)
    , CONSTRAINT [FK_Reporting_Report_Reporting_Parent] FOREIGN KEY ([ParentID]) REFERENCES [Reporting].[Report]([ReportID])
    , CONSTRAINT [FK_Reporting_Report_Auth_CreatedBy] FOREIGN KEY ([CreatedByID]) REFERENCES [Auth].[User]([UserID])
    , CONSTRAINT [FK_Reporting_Report_Metadata_View] FOREIGN KEY ([ViewID]) REFERENCES [Metadata].[View]([ViewID])
    , CONSTRAINT [FK_Reporting_Report_Metadata_Subsystem] FOREIGN KEY ([SubsystemID]) REFERENCES [Metadata].[Subsystem]([SubsystemID])
)
GO

CREATE TABLE [Reporting].[Parameter] (
    [ParamID]          INT              IDENTITY (1, 1) NOT NULL,
    [ReportID]         INT              NOT NULL,
    [Name]             NVARCHAR(64)     NOT NULL,
    [FieldName]        NVARCHAR(64)     NOT NULL,
    [Operator]         NVARCHAR(16)     NOT NULL,
    [DataType]         NVARCHAR(64)     NOT NULL,
    [ControlType]      NVARCHAR(64)     NOT NULL,
    [Source]           NVARCHAR(64)     NULL,
    [CaptionKey]       NVARCHAR(64)     NOT NULL,
    [DefaultValue]     NVARCHAR(64)     NULL,
    [MinValue]         NVARCHAR(64)     NULL,
    [MaxValue]         NVARCHAR(64)     NULL,
    [DescriptionKey]   NVARCHAR(64)     NULL,
    [rowguid]          UNIQUEIDENTIFIER CONSTRAINT [DF_Reporting_Parameter_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]     DATETIME         CONSTRAINT [DF_Reporting_Parameter_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Reporting_Parameter] PRIMARY KEY CLUSTERED ([ParamID] ASC)
    , CONSTRAINT [FK_Reporting_Parameter_Reporting_Report] FOREIGN KEY ([ReportID]) REFERENCES [Reporting].[Report]([ReportID])
)
GO

CREATE TABLE [Reporting].[LocalReport] (
    [LocalReportID]   INT              IDENTITY (1, 1) NOT NULL,
    [LocaleID]        INT              NOT NULL,
    [ReportID]        INT              NOT NULL,
    [Caption]         NVARCHAR(128)    NOT NULL,
    [Template]        NVARCHAR(MAX)    NULL,
    [rowguid]         UNIQUEIDENTIFIER CONSTRAINT [DF_Reporting_LocalReport_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]    DATETIME         CONSTRAINT [DF_Reporting_LocalReport_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Reporting_LocalReport] PRIMARY KEY CLUSTERED ([LocalReportID] ASC)
    , CONSTRAINT [FK_Reporting_LocalReport_Metadata_Locale] FOREIGN KEY ([LocaleID]) REFERENCES [Metadata].[Locale]([LocaleID])
    , CONSTRAINT [FK_Reporting_LocalReport_Reporting_Report] FOREIGN KEY ([ReportID]) REFERENCES [Reporting].[Report]([ReportID])
)
GO

CREATE TABLE [Config].[Setting] (
    [SettingID]      INT              IDENTITY (1, 1) NOT NULL,
    [ParentID]       INT              NULL,
    [Subsystem]      VARCHAR(32)      NULL,
    [TitleKey]       VARCHAR(128)     NOT NULL,
    [Type]           SMALLINT         NOT NULL,
    [ScopeType]      SMALLINT         NOT NULL,
    [ModelType]      VARCHAR(128)     NOT NULL,
	[IsStandalone]   BIT CONSTRAINT [DF_Config_Setting_IsStandalone] DEFAULT (1) NOT NULL,
    [Values]         NVARCHAR(2048)   NOT NULL,
    [DefaultValues]  NVARCHAR(2048)   NOT NULL,
    [DescriptionKey] VARCHAR(1028)    NULL,
    [rowguid]        UNIQUEIDENTIFIER CONSTRAINT [DF_Config_Setting_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]   DATETIME         CONSTRAINT [DF_Config_Setting_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Config_Setting] PRIMARY KEY CLUSTERED ([SettingID] ASC)
    , CONSTRAINT [FK_Config_Setting_Config_Parent] FOREIGN KEY ([ParentID]) REFERENCES [Config].[Setting]([SettingID])
)
GO

CREATE TABLE [Config].[UserSetting] (
    [UserSettingID]  INT              IDENTITY (1, 1) NOT NULL,
    [SettingID]      INT              NOT NULL,
    [ViewID]         INT              NULL,
    [UserID]         INT              NULL,
    [RoleID]         INT              NULL,
    [ModelType]      VARCHAR(128)     NOT NULL,
    [Values]         NVARCHAR(MAX)            NOT NULL,
    [rowguid]        UNIQUEIDENTIFIER CONSTRAINT [DF_Config_UserSetting_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]   DATETIME         CONSTRAINT [DF_Config_UserSetting_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Config_UserSetting] PRIMARY KEY CLUSTERED ([UserSettingID] ASC)
    , CONSTRAINT [FK_Config_UserSetting_Config_Setting] FOREIGN KEY ([SettingID]) REFERENCES [Config].[Setting]([SettingID])
    , CONSTRAINT [FK_Config_UserSetting_Metadata_View] FOREIGN KEY ([ViewID]) REFERENCES [Metadata].[View]([ViewID])
    , CONSTRAINT [FK_Config_UserSetting_Auth_User] FOREIGN KEY ([UserID]) REFERENCES [Auth].[User]([UserID])
    , CONSTRAINT [FK_Config_UserSetting_Auth_Role] FOREIGN KEY ([RoleID]) REFERENCES [Auth].[Role]([RoleID])
)
GO

CREATE TABLE [Metadata].[Command] (
    [CommandID]      INT              IDENTITY (1, 1) NOT NULL,
    [ParentID]       INT              NULL,
    [PermissionID]   INT              NULL,
    [TitleKey]       NVARCHAR(64)     NOT NULL,
    [RouteUrl]       NVARCHAR(256)    NULL,
    [IconName]       VARCHAR(64)      NULL,
    [HotKey]         VARCHAR(32)      NULL,
	[Index]          INT              NULL,
    [rowguid]        UNIQUEIDENTIFIER CONSTRAINT [DF_Metadata_Command_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]   DATETIME         CONSTRAINT [DF_Metadata_Command_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Metadata_Command] PRIMARY KEY CLUSTERED ([CommandID] ASC)
    , CONSTRAINT [FK_Metadata_Command_Metadata_Parent] FOREIGN KEY ([ParentID]) REFERENCES [Metadata].[Command]([CommandID])
    , CONSTRAINT [FK_Metadata_Command_Auth_Permission] FOREIGN KEY ([PermissionID]) REFERENCES [Auth].[Permission]([PermissionID])
)
GO

CREATE TABLE [Metadata].[ShortcutCommand] (
    [ShortcutCommandID]   INT              IDENTITY (1, 1) NOT NULL,
    [PermissionID]        INT              NULL,
    [Name]                VARCHAR(128)     NOT NULL,
    [Scope]               VARCHAR(1024)      NULL,
    [HotKey]              VARCHAR(32)      NOT NULL,
    [Method]              VARCHAR(128)     NOT NULL,
    [rowguid]             UNIQUEIDENTIFIER CONSTRAINT [DF_Metadata_ShortcutCommand_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]        DATETIME         CONSTRAINT [DF_Metadata_ShortcutCommand_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Metadata_ShortcutCommand] PRIMARY KEY CLUSTERED ([ShortcutCommandID] ASC)
    , CONSTRAINT [FK_Metadata_ShortcutCommand_Auth_Permission] FOREIGN KEY ([PermissionID]) REFERENCES [Auth].[Permission]([PermissionID])
)
GO

CREATE TABLE [Config].[CompanyDb] (
    [CompanyID]      INT              IDENTITY (1, 1) NOT NULL,
    [Name]           NVARCHAR(128)    NOT NULL,
    [DbName]         NVARCHAR(128)    NOT NULL,
    [Server]         NVARCHAR(64)     NOT NULL,
    [UserName]       NVARCHAR(32)     NULL,
    [Password]       NVARCHAR(32)     NULL,
	[IsActive]       BIT              CONSTRAINT [DF_Config_CompanyDb_IsActive] DEFAULT (1) NOT NULL,
    [Description]    NVARCHAR(512)    NULL,
    [rowguid]        UNIQUEIDENTIFIER CONSTRAINT [DF_Config_CompanyDb_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]   DATETIME         CONSTRAINT [DF_Config_CompanyDb_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Config_CompanyDb] PRIMARY KEY CLUSTERED ([CompanyID] ASC)
)
GO

CREATE TABLE [Auth].[RoleCompany] (
    [RoleCompanyID] INT              IDENTITY (1, 1) NOT NULL,
    [RoleID]        INT              NOT NULL,
    [CompanyID]     INT              NOT NULL,
    [rowguid]       UNIQUEIDENTIFIER CONSTRAINT [DF_Auth_RoleCompany_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]  DATETIME         CONSTRAINT [DF_Auth_RoleCompany_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Auth_RoleCompany] PRIMARY KEY CLUSTERED ([RoleCompanyID] ASC)
    , CONSTRAINT [FK_Auth_RoleCompany_Auth_Role] FOREIGN KEY ([RoleID]) REFERENCES [Auth].[Role] ([RoleID])
    , CONSTRAINT [FK_Auth_RoleCompany_Config_CompanyDb] FOREIGN KEY ([CompanyID]) REFERENCES [Config].[CompanyDb] ([CompanyID])
)
GO

CREATE TABLE [Core].[SystemError](
	[SystemErrorID]  INT           IDENTITY (1, 1) NOT NULL,
	[CompanyID]      INT           NULL,
	[FiscalPeriodID] INT           NULL,
	[BranchID]       INT           NULL,
	[TimestampUtc]   VARCHAR(32)   NOT NULL,
	[Code]           INT           NOT NULL,
	[Message]        VARCHAR(2048) NOT NULL,
	[FaultingMethod] VARCHAR(64)   NOT NULL,
	[FaultType]      VARCHAR(64)   NOT NULL,
	[StackTrace]     VARCHAR(MAX)          NULL,
	[Version]        VARCHAR(16)  CONSTRAINT [DF_Core_SystemError_Version] DEFAULT ('1.0') NOT NULL
    , CONSTRAINT [PK_Core_SystemError] PRIMARY KEY CLUSTERED  ([SystemErrorID] ASC)
    , CONSTRAINT [FK_Core_SystemError_Config_CompanyDb] FOREIGN KEY ([CompanyID]) REFERENCES [Config].[CompanyDb] ([CompanyID])
)
GO

CREATE TABLE [Core].[SysOperationLog] (
    [SysOperationLogID]   INT              IDENTITY (1, 1) NOT NULL,
    [OperationID]         INT              NOT NULL,
    [SourceID]            INT              NULL,
    [EntityTypeID]        INT              NULL,
    [SourceListID]        INT              NULL,
    [UserID]              INT              NULL,
    [CompanyID]           INT              NULL,
    [Date]                DATETIME         NOT NULL,
    [Time]                TIME(7)          NOT NULL,
    [EntityId]            INT              NULL,
    [EntityCode]          NVARCHAR(256)    NULL,
    [EntityName]          NVARCHAR(256)    NULL,
    [EntityDescription]   NVARCHAR(1024)   NULL,
    [EntityNo]            INT              NULL,
    [EntityDate]          DATETIME         NULL,
    [Description]         NVARCHAR(MAX)    NULL,
    [rowguid]             UNIQUEIDENTIFIER CONSTRAINT [DF_Core_SysOperationLog_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]        DATETIME         CONSTRAINT [DF_Core_SysOperationLog_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Core_SysOperationLog] PRIMARY KEY CLUSTERED ([SysOperationLogID] ASC)
    , CONSTRAINT [FK_Core_SysOperationLog_Metadata_Operation] FOREIGN KEY ([OperationID]) REFERENCES [Metadata].[Operation]([OperationID])
    , CONSTRAINT [FK_Core_SysOperationLog_Metadata_Source] FOREIGN KEY ([SourceID]) REFERENCES [Metadata].[OperationSource]([OperationSourceID])
    , CONSTRAINT [FK_Core_SysOperationLog_Metadata_SourceList] FOREIGN KEY ([SourceListID]) REFERENCES [Metadata].[View]([ViewID])
    , CONSTRAINT [FK_Core_SysOperationLog_Metadata_EntityType] FOREIGN KEY ([EntityTypeID]) REFERENCES [Metadata].[EntityType]([EntityTypeID])
    , CONSTRAINT [FK_Core_SysOperationLog_Config_CompanyDb] FOREIGN KEY ([CompanyID]) REFERENCES [Config].[CompanyDb]([CompanyID])
    , CONSTRAINT [FK_Core_SysOperationLog_Auth_User] FOREIGN KEY ([UserID]) REFERENCES [Auth].[User]([UserID])
)
GO

CREATE TABLE [Core].[SysOperationLogArchive] (
    [SysOperationLogArchiveID]  INT              NOT NULL,
    [OperationID]               INT              NOT NULL,
    [SourceID]                  INT              NULL,
    [EntityTypeID]              INT              NULL,
    [SourceListID]              INT              NULL,
    [UserID]                    INT              NULL,
    [CompanyID]                 INT              NULL,
    [Date]                      DATETIME         NOT NULL,
    [Time]                      TIME(7)          NOT NULL,
    [EntityId]                  INT              NULL,
    [EntityCode]                NVARCHAR(256)    NULL,
    [EntityName]                NVARCHAR(256)    NULL,
    [EntityDescription]         NVARCHAR(1024)   NULL,
    [EntityNo]                  INT              NULL,
    [EntityDate]                DATETIME         NULL,
    [Description]               NVARCHAR(MAX)    NULL,
    [rowguid]                   UNIQUEIDENTIFIER CONSTRAINT [DF_Core_SysOperationLogArchive_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]              DATETIME         CONSTRAINT [DF_Core_SysOperationLogArchive_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Core_SysOperationLogArchive] PRIMARY KEY CLUSTERED ([SysOperationLogArchiveID] ASC)
    , CONSTRAINT [FK_Core_SysOperationLogArchive_Metadata_Operation] FOREIGN KEY ([OperationID]) REFERENCES [Metadata].[Operation]([OperationID])
    , CONSTRAINT [FK_Core_SysOperationLogArchive_Metadata_Source] FOREIGN KEY ([SourceID]) REFERENCES [Metadata].[OperationSource]([OperationSourceID])
    , CONSTRAINT [FK_Core_SysOperationLogArchive_Metadata_SourceList] FOREIGN KEY ([SourceListID]) REFERENCES [Metadata].[View]([ViewID])
    , CONSTRAINT [FK_Core_SysOperationLogArchive_Metadata_EntityType] FOREIGN KEY ([EntityTypeID]) REFERENCES [Metadata].[EntityType]([EntityTypeID])
    , CONSTRAINT [FK_Core_SysOperationLogArchive_Config_CompanyDb] FOREIGN KEY ([CompanyID]) REFERENCES [Config].[CompanyDb]([CompanyID])
    , CONSTRAINT [FK_Core_SysOperationLogArchive_Auth_User] FOREIGN KEY ([UserID]) REFERENCES [Auth].[User]([UserID])
)
GO

CREATE TABLE [Reporting].[SystemIssue] (
    [SystemIssueID]   INT              IDENTITY (1, 1) NOT NULL,
    [ParentID]        INT              NULL,
    [PermissionID]    INT              NULL,
    [ViewID]          INT              NULL,
    [TitleKey]        NVARCHAR(64)     NOT NULL,
    [ApiUrl]          NVARCHAR(128)    NULL,	
	[BranchScope]     BIT              NOT NULL,
	[DeleteApiUrl]    NVARCHAR(128)    NULL,
    [rowguid]         UNIQUEIDENTIFIER CONSTRAINT [DF_Reporting_SystemIssue_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]    DATETIME         CONSTRAINT [DF_Reporting_SystemIssue_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Reporting_SystemIssue] PRIMARY KEY CLUSTERED ([SystemIssueID] ASC)
    , CONSTRAINT [FK_Reporting_SystemIssue_Reporting_Parent] FOREIGN KEY ([ParentID]) REFERENCES [Reporting].[SystemIssue]([SystemIssueID])
    , CONSTRAINT [FK_Reporting_SystemIssue_Auth_Permission] FOREIGN KEY ([PermissionID]) REFERENCES [Auth].[Permission]([PermissionID])
    , CONSTRAINT [FK_Reporting_SystemIssue_Metadata_View] FOREIGN KEY ([ViewID]) REFERENCES [Metadata].[View]([ViewID])
)
GO

-- Create system metadata records

SET IDENTITY_INSERT [Metadata].[Locale] ON
INSERT INTO [Metadata].[Locale] (LocaleID, Name, LocalName, Code) VALUES (1, 'English', N'English', 'en')
INSERT INTO [Metadata].[Locale] (LocaleID, Name, LocalName, Code) VALUES (2, 'Persian', N'فارسی', 'fa')
INSERT INTO [Metadata].[Locale] (LocaleID, Name, LocalName, Code) VALUES (3, 'Arabic', N'العربیه', 'ar')
INSERT INTO [Metadata].[Locale] (LocaleID, Name, LocalName, Code) VALUES (4, 'French', N'Français', 'fr')
SET IDENTITY_INSERT [Metadata].[Locale] OFF

SET IDENTITY_INSERT [Metadata].[Subsystem] ON
INSERT INTO [Metadata].[Subsystem] ([SubsystemID], [Name]) VALUES (1, N'Administration')
INSERT INTO [Metadata].[Subsystem] ([SubsystemID], [Name]) VALUES (2, N'Accounting')
INSERT INTO [Metadata].[Subsystem] ([SubsystemID], [Name]) VALUES (3, N'Treasury')
INSERT INTO [Metadata].[Subsystem] ([SubsystemID], [Name]) VALUES (100000, N'ProductScope')
SET IDENTITY_INSERT [Metadata].[Subsystem] OFF

SET IDENTITY_INSERT [Metadata].[OperationSourceType] ON
INSERT INTO [Metadata].[OperationSourceType] ([OperationSourceTypeID], [Name]) VALUES (1, N'BaseData')
INSERT INTO [Metadata].[OperationSourceType] ([OperationSourceTypeID], [Name]) VALUES (2, N'OperationalForms')
INSERT INTO [Metadata].[OperationSourceType] ([OperationSourceTypeID], [Name]) VALUES (3, N'Reports')
SET IDENTITY_INSERT [Metadata].[OperationSourceType] OFF

SET IDENTITY_INSERT [Metadata].[EntityType] ON
INSERT INTO [Metadata].[EntityType] ([EntityTypeID],[Name]) VALUES (1, N'CompanyDb')
INSERT INTO [Metadata].[EntityType] ([EntityTypeID],[Name]) VALUES (2, N'Role')
INSERT INTO [Metadata].[EntityType] ([EntityTypeID],[Name]) VALUES (4, N'Setting')
INSERT INTO [Metadata].[EntityType] ([EntityTypeID],[Name]) VALUES (5, N'SysOperationLog')
INSERT INTO [Metadata].[EntityType] ([EntityTypeID],[Name]) VALUES (6, N'User')
INSERT INTO [Metadata].[EntityType] ([EntityTypeID],[Name]) VALUES (8, N'ViewRowPermission')
INSERT INTO [Metadata].[EntityType] ([EntityTypeID],[Name]) VALUES (9, N'UserReport')
SET IDENTITY_INSERT [Metadata].[EntityType] OFF

SET IDENTITY_INSERT [Metadata].[Operation] ON
INSERT INTO [Metadata].[Operation] ([OperationID], [Name]) VALUES (1, N'View')
INSERT INTO [Metadata].[Operation] ([OperationID], [Name]) VALUES (2, N'Create')
INSERT INTO [Metadata].[Operation] ([OperationID], [Name]) VALUES (3, N'Edit')
INSERT INTO [Metadata].[Operation] ([OperationID], [Name]) VALUES (4, N'Delete')
INSERT INTO [Metadata].[Operation] ([OperationID], [Name]) VALUES (5, N'Filter')
INSERT INTO [Metadata].[Operation] ([OperationID], [Name]) VALUES (6, N'Print')
INSERT INTO [Metadata].[Operation] ([OperationID], [Name]) VALUES (7, N'Save')
INSERT INTO [Metadata].[Operation] ([OperationID], [Name]) VALUES (8, N'Archive')
INSERT INTO [Metadata].[Operation] ([OperationID], [Name]) VALUES (10, N'Design')
INSERT INTO [Metadata].[Operation] ([OperationID], [Name]) VALUES (21, N'GroupDelete')
INSERT INTO [Metadata].[Operation] ([OperationID], [Name]) VALUES (22, N'FailedLogin')
INSERT INTO [Metadata].[Operation] ([OperationID], [Name]) VALUES (23, N'CompanyLogin')
INSERT INTO [Metadata].[Operation] ([OperationID], [Name]) VALUES (24, N'SwitchFiscalPeriod')
INSERT INTO [Metadata].[Operation] ([OperationID], [Name]) VALUES (25, N'SwitchBranch')
INSERT INTO [Metadata].[Operation] ([OperationID], [Name]) VALUES (26, N'AssignRole')
INSERT INTO [Metadata].[Operation] ([OperationID], [Name]) VALUES (27, N'AssignUser')
INSERT INTO [Metadata].[Operation] ([OperationID], [Name]) VALUES (28, N'BranchAccess')
INSERT INTO [Metadata].[Operation] ([OperationID], [Name]) VALUES (29, N'FiscalPeriodAccess')
INSERT INTO [Metadata].[Operation] ([OperationID], [Name]) VALUES (30, N'ViewArchive')
INSERT INTO [Metadata].[Operation] ([OperationID], [Name]) VALUES (35, N'RoleAccess')
INSERT INTO [Metadata].[Operation] ([OperationID], [Name]) VALUES (54, N'Export')
INSERT INTO [Metadata].[Operation] ([OperationID], [Name]) VALUES (57, N'CompanyAccess')
INSERT INTO [Metadata].[Operation] ([OperationID], [Name]) VALUES (58, N'PrintPreview')
SET IDENTITY_INSERT [Metadata].[Operation] OFF

SET IDENTITY_INSERT [Metadata].[OperationSource] ON
INSERT INTO [Metadata].[OperationSource] ([OperationSourceID],[Name]) VALUES (7, N'AppLogin')
INSERT INTO [Metadata].[OperationSource] ([OperationSourceID],[Name]) VALUES (8, N'AppEnvironment')
INSERT INTO [Metadata].[OperationSource] ([OperationSourceID],[Name]) VALUES (14, N'SystemSettings')
SET IDENTITY_INSERT [Metadata].[OperationSource] OFF

SET IDENTITY_INSERT [Config].[SysLogSetting] ON
INSERT INTO [Config].[SysLogSetting] ([SysLogSettingID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (1, NULL, 1, 1, 1)
INSERT INTO [Config].[SysLogSetting] ([SysLogSettingID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (2, NULL, 1, 2, 1)
INSERT INTO [Config].[SysLogSetting] ([SysLogSettingID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (3, NULL, 1, 3, 1)
INSERT INTO [Config].[SysLogSetting] ([SysLogSettingID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (4, NULL, 1, 4, 1)
INSERT INTO [Config].[SysLogSetting] ([SysLogSettingID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (5, NULL, 1, 21, 1)
INSERT INTO [Config].[SysLogSetting] ([SysLogSettingID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (6, NULL, 1, 5, 1)
INSERT INTO [Config].[SysLogSetting] ([SysLogSettingID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (7, NULL, 1, 58, 1)
INSERT INTO [Config].[SysLogSetting] ([SysLogSettingID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (8, NULL, 1, 6, 1)
INSERT INTO [Config].[SysLogSetting] ([SysLogSettingID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (9, NULL, 1, 54, 1)
INSERT INTO [Config].[SysLogSetting] ([SysLogSettingID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (10, NULL, 1, 35, 1)
INSERT INTO [Config].[SysLogSetting] ([SysLogSettingID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (11, NULL, 6, 1, 1)
INSERT INTO [Config].[SysLogSetting] ([SysLogSettingID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (12, NULL, 6, 2, 1)
INSERT INTO [Config].[SysLogSetting] ([SysLogSettingID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (13, NULL, 6, 3, 1)
INSERT INTO [Config].[SysLogSetting] ([SysLogSettingID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (14, NULL, 6, 5, 1)
INSERT INTO [Config].[SysLogSetting] ([SysLogSettingID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (15, NULL, 6, 58, 1)
INSERT INTO [Config].[SysLogSetting] ([SysLogSettingID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (16, NULL, 6, 6, 1)
INSERT INTO [Config].[SysLogSetting] ([SysLogSettingID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (17, NULL, 6, 54, 1)
INSERT INTO [Config].[SysLogSetting] ([SysLogSettingID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (18, NULL, 6, 26, 1)
INSERT INTO [Config].[SysLogSetting] ([SysLogSettingID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (19, NULL, 2, 1, 1)
INSERT INTO [Config].[SysLogSetting] ([SysLogSettingID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (20, NULL, 2, 2, 1)
INSERT INTO [Config].[SysLogSetting] ([SysLogSettingID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (21, NULL, 2, 3, 1)
INSERT INTO [Config].[SysLogSetting] ([SysLogSettingID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (22, NULL, 2, 4, 1)
INSERT INTO [Config].[SysLogSetting] ([SysLogSettingID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (23, NULL, 2, 21, 1)
INSERT INTO [Config].[SysLogSetting] ([SysLogSettingID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (24, NULL, 2, 5, 1)
INSERT INTO [Config].[SysLogSetting] ([SysLogSettingID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (25, NULL, 2, 58, 1)
INSERT INTO [Config].[SysLogSetting] ([SysLogSettingID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (26, NULL, 2, 6, 1)
INSERT INTO [Config].[SysLogSetting] ([SysLogSettingID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (27, NULL, 2, 54, 1)
INSERT INTO [Config].[SysLogSetting] ([SysLogSettingID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (28, NULL, 2, 27, 1)
INSERT INTO [Config].[SysLogSetting] ([SysLogSettingID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (29, NULL, 2, 28, 1)
INSERT INTO [Config].[SysLogSetting] ([SysLogSettingID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (30, NULL, 2, 29, 1)
INSERT INTO [Config].[SysLogSetting] ([SysLogSettingID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (31, NULL, 2, 57, 1)
INSERT INTO [Config].[SysLogSetting] ([SysLogSettingID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (32, NULL, 4, 1, 1)
INSERT INTO [Config].[SysLogSetting] ([SysLogSettingID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (33, NULL, 4, 7, 1)
INSERT INTO [Config].[SysLogSetting] ([SysLogSettingID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (34, NULL, 5, 1, 1)
INSERT INTO [Config].[SysLogSetting] ([SysLogSettingID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (35, NULL, 5, 4, 1)
INSERT INTO [Config].[SysLogSetting] ([SysLogSettingID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (36, NULL, 5, 21, 1)
INSERT INTO [Config].[SysLogSetting] ([SysLogSettingID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (37, NULL, 5, 5, 1)
INSERT INTO [Config].[SysLogSetting] ([SysLogSettingID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (38, NULL, 5, 58, 1)
INSERT INTO [Config].[SysLogSetting] ([SysLogSettingID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (39, NULL, 5, 6, 1)
INSERT INTO [Config].[SysLogSetting] ([SysLogSettingID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (40, NULL, 5, 54, 1)
INSERT INTO [Config].[SysLogSetting] ([SysLogSettingID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (41, NULL, 5, 8, 1)
INSERT INTO [Config].[SysLogSetting] ([SysLogSettingID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (42, NULL, 5, 30, 1)
INSERT INTO [Config].[SysLogSetting] ([SysLogSettingID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (43, NULL, 8, 1, 1)
INSERT INTO [Config].[SysLogSetting] ([SysLogSettingID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (44, NULL, 8, 7, 1)
INSERT INTO [Config].[SysLogSetting] ([SysLogSettingID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (45, 7, NULL, 22, 1)
INSERT INTO [Config].[SysLogSetting] ([SysLogSettingID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (46, 8, NULL, 23, 1)
INSERT INTO [Config].[SysLogSetting] ([SysLogSettingID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (47, 8, NULL, 24, 1)
INSERT INTO [Config].[SysLogSetting] ([SysLogSettingID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (48, 8, NULL, 25, 1)
INSERT INTO [Config].[SysLogSetting] ([SysLogSettingID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (49, NULL, 9, 10, 1)
SET IDENTITY_INSERT [Config].[SysLogSetting] OFF


SET IDENTITY_INSERT [Metadata].[View] ON
INSERT INTO [Metadata].[View] (ViewID, Name, EntityName, EntityType, IsHierarchy, IsCartableIntegrated, FetchUrl, SearchUrl) VALUES (1, 'Account', 'Account', 'Base', 1, 1, N'/lookup/accounts', N'/accounts/lookup')
INSERT INTO [Metadata].[View] (ViewID, Name, EntityName, EntityType, IsHierarchy, IsCartableIntegrated, FetchUrl) VALUES (2, 'Voucher', 'Voucher', 'Operational', 0, 1, N'/lookup/vouchers')
INSERT INTO [Metadata].[View] (ViewID, Name, EntityName, EntityType, IsHierarchy, IsCartableIntegrated, FetchUrl) VALUES (3, 'VoucherLine', 'VoucherLine', 'Operational', 0, 1, N'/lookup/vouchers/lines')
INSERT INTO [Metadata].[View] (ViewID, Name, EntityName, EntityType, IsHierarchy, IsCartableIntegrated, FetchUrl) VALUES (4, 'User', 'User', 'Core', 0, 0, N'')
INSERT INTO [Metadata].[View] (ViewID, Name, EntityName, EntityType, IsHierarchy, IsCartableIntegrated, FetchUrl) VALUES (5, 'Role', 'Role', 'Core', 0, 0, N'')
INSERT INTO [Metadata].[View] (ViewID, Name, EntityName, EntityType, IsHierarchy, IsCartableIntegrated, FetchUrl, SearchUrl) VALUES (6, 'DetailAccount', 'DetailAccount', 'Base', 1, 1, N'/lookup/faccounts', N'/faccounts')
INSERT INTO [Metadata].[View] (ViewID, Name, EntityName, EntityType, IsHierarchy, IsCartableIntegrated, FetchUrl, SearchUrl) VALUES (7, 'CostCenter', 'CostCenter', 'Base', 1, 1, N'/lookup/ccenters', N'/ccenters')
INSERT INTO [Metadata].[View] (ViewID, Name, EntityName, EntityType, IsHierarchy, IsCartableIntegrated, FetchUrl, SearchUrl) VALUES (8, 'Project', 'Project', 'Base', 1, 1, N'/lookup/projects', N'/projects')
INSERT INTO [Metadata].[View] (ViewID, Name, EntityName, EntityType, IsHierarchy, IsCartableIntegrated, FetchUrl, SearchUrl) VALUES (9, 'FiscalPeriod', 'FiscalPeriod', 'Core', 0, 1, NULL, N'/fperiods')
INSERT INTO [Metadata].[View] (ViewID, Name, EntityName, EntityType, IsHierarchy, IsCartableIntegrated, FetchUrl, SearchUrl) VALUES (10, 'Branch', 'Branch', 'Core', 0, 0, NULL, N'/branches')
INSERT INTO [Metadata].[View] (ViewID, Name, EntityName, EntityType, IsHierarchy, IsCartableIntegrated, FetchUrl) VALUES (11, 'CompanyDb', 'Company', 'Core', 0, 0, N'')
INSERT INTO [Metadata].[View] (ViewID, Name, EntityName, EntityType, IsHierarchy, IsCartableIntegrated, FetchUrl) VALUES (12, 'AccountGroup', 'AccountGroup', 'Core', 0, 0, N'')
INSERT INTO [Metadata].[View] (ViewID, Name, EntityName, EntityType, IsHierarchy, IsCartableIntegrated, FetchUrl) VALUES (13, 'OperationLog', 'OperationLog', 'Core', 0, 0, N'')
INSERT INTO [Metadata].[View] (ViewID, Name, EntityName, EntityType, IsHierarchy, IsCartableIntegrated, FetchUrl) VALUES (14, 'AccountCollectionAccount', 'AccountCollection', 'Core', 0, 0, N'')
INSERT INTO [Metadata].[View] (ViewID, Name, EntityName, IsHierarchy, IsCartableIntegrated, FetchUrl) VALUES (15, 'JournalByDateByRow', 'Journal', 0, 0, N'')
INSERT INTO [Metadata].[View] (ViewID, Name, EntityName, IsHierarchy, IsCartableIntegrated, FetchUrl) VALUES (16, 'JournalByDateByRowDetail', 'Journal', 0, 0, N'')
INSERT INTO [Metadata].[View] (ViewID, Name, EntityName, IsHierarchy, IsCartableIntegrated, FetchUrl) VALUES (17, 'JournalByDateByLedger', 'Journal', 0, 0, N'')
INSERT INTO [Metadata].[View] (ViewID, Name, EntityName, IsHierarchy, IsCartableIntegrated, FetchUrl) VALUES (18, 'JournalByDateBySubsidiary', 'Journal', 0, 0, N'')
INSERT INTO [Metadata].[View] (ViewID, Name, EntityName, IsHierarchy, IsCartableIntegrated, FetchUrl) VALUES (19, 'JournalByDateSummary', 'Journal', 0, 0, N'')
INSERT INTO [Metadata].[View] (ViewID, Name, EntityName, IsHierarchy, IsCartableIntegrated, FetchUrl) VALUES (20, 'JournalByDateSummaryByDate', 'Journal', 0, 0, N'')
INSERT INTO [Metadata].[View] (ViewID, Name, EntityName, IsHierarchy, IsCartableIntegrated, FetchUrl) VALUES (21, 'JournalByDateSummaryByMonth', 'Journal', 0, 0, N'')
INSERT INTO [Metadata].[View] (ViewID, Name, EntityName, IsHierarchy, IsCartableIntegrated, FetchUrl) VALUES (22, 'JournalByNoByRow', 'Journal', 0, 0, N'')
INSERT INTO [Metadata].[View] (ViewID, Name, EntityName, IsHierarchy, IsCartableIntegrated, FetchUrl) VALUES (23, 'JournalByNoByRowDetail', 'Journal', 0, 0, N'')
INSERT INTO [Metadata].[View] (ViewID, Name, EntityName, IsHierarchy, IsCartableIntegrated, FetchUrl) VALUES (24, 'JournalByNoByLedger', 'Journal', 0, 0, N'')
INSERT INTO [Metadata].[View] (ViewID, Name, EntityName, IsHierarchy, IsCartableIntegrated, FetchUrl) VALUES (25, 'JournalByNoBySubsidiary', 'Journal', 0, 0, N'')
INSERT INTO [Metadata].[View] (ViewID, Name, EntityName, IsHierarchy, IsCartableIntegrated, FetchUrl) VALUES (26, 'JournalByNoSummary', 'Journal', 0, 0, N'')
INSERT INTO [Metadata].[View] (ViewID, Name, EntityName, IsHierarchy, IsCartableIntegrated, FetchUrl) VALUES (27, 'AccountBookSingle', 'AccountBook', 0, 0, N'')
INSERT INTO [Metadata].[View] (ViewID, Name, EntityName, IsHierarchy, IsCartableIntegrated, FetchUrl) VALUES (28, 'AccountBookSingleSummary', 'AccountBook', 0, 0, N'')
INSERT INTO [Metadata].[View] (ViewID, Name, EntityName, IsHierarchy, IsCartableIntegrated, FetchUrl) VALUES (29, 'AccountBookSummary', 'AccountBook', 0, 0, N'')
INSERT INTO [Metadata].[View] (ViewID, Name, EntityName, IsHierarchy, IsCartableIntegrated, FetchUrl) VALUES (30, 'Currency', 'Currency', 0, 0, N'')
INSERT INTO [Metadata].[View] (ViewID, Name, EntityName, IsHierarchy, IsCartableIntegrated, FetchUrl) VALUES (31, 'CurrencyRate', 'CurrencyRate', 0, 0, N'')
INSERT INTO [Metadata].[View] (ViewID, Name, EntityName, IsHierarchy, IsCartableIntegrated, FetchUrl) VALUES (32, 'TestBalance2Column', 'TestBalance', 0, 0, N'')
INSERT INTO [Metadata].[View] (ViewID, Name, EntityName, IsHierarchy, IsCartableIntegrated, FetchUrl) VALUES (33, 'TestBalance4Column', 'TestBalance', 0, 0, N'')
INSERT INTO [Metadata].[View] (ViewID, Name, EntityName, IsHierarchy, IsCartableIntegrated, FetchUrl) VALUES (34, 'TestBalance6Column', 'TestBalance', 0, 0, N'')
INSERT INTO [Metadata].[View] (ViewID, Name, EntityName, IsHierarchy, IsCartableIntegrated, FetchUrl) VALUES (35, 'TestBalance8Column', 'TestBalance', 0, 0, N'')
INSERT INTO [Metadata].[View] (ViewID, Name, EntityName, IsHierarchy, IsCartableIntegrated, FetchUrl) VALUES (36, 'TestBalance10Column', 'TestBalance', 0, 0, N'')
INSERT INTO [Metadata].[View] (ViewID, Name, EntityName, IsHierarchy, IsCartableIntegrated, FetchUrl) VALUES (37, 'CurrencyBook', 'CurrencyBook', 0, 0, N'')
INSERT INTO [Metadata].[View] (ViewID, Name, EntityName, IsHierarchy, IsCartableIntegrated, FetchUrl) VALUES (38, 'CurrencyBookSingle', 'CurrencyBook', 0, 0, N'')
INSERT INTO [Metadata].[View] (ViewID, Name, EntityName, IsHierarchy, IsCartableIntegrated, FetchUrl) VALUES (39, 'CurrencyBookSingleSummary', 'CurrencyBook', 0, 0, N'')
INSERT INTO [Metadata].[View] (ViewID, Name, EntityName, IsHierarchy, IsCartableIntegrated, FetchUrl) VALUES (40, 'CurrencyBookSummary', 'CurrencyBook', 0, 0, N'')
INSERT INTO [Metadata].[View] (ViewID, Name, EntityName, IsHierarchy, IsCartableIntegrated, FetchUrl) VALUES (41, 'NumberList', 'NumberList', 0, 0, N'')
INSERT INTO [Metadata].[View] (ViewID, Name, EntityName, IsHierarchy, IsCartableIntegrated, FetchUrl) VALUES (42, 'VoucherLineDetail', 'VoucherLineDetail', 0, 0, N'')
INSERT INTO [Metadata].[View] (ViewID, Name, EntityName, IsHierarchy, IsCartableIntegrated, FetchUrl) VALUES (43, 'DetailAccountBalance2Column', 'ItemBalance', 0, 0, N'')
INSERT INTO [Metadata].[View] (ViewID, Name, EntityName, IsHierarchy, IsCartableIntegrated, FetchUrl) VALUES (44, 'DetailAccountBalance4Column', 'ItemBalance', 0, 0, N'')
INSERT INTO [Metadata].[View] (ViewID, Name, EntityName, IsHierarchy, IsCartableIntegrated, FetchUrl) VALUES (45, 'DetailAccountBalance6Column', 'ItemBalance', 0, 0, N'')
INSERT INTO [Metadata].[View] (ViewID, Name, EntityName, IsHierarchy, IsCartableIntegrated, FetchUrl) VALUES (46, 'DetailAccountBalance8Column', 'ItemBalance', 0, 0, N'')
INSERT INTO [Metadata].[View] (ViewID, Name, EntityName, IsHierarchy, IsCartableIntegrated, FetchUrl) VALUES (47, 'DetailAccountBalance10Column', 'ItemBalance', 0, 0, N'')
INSERT INTO [Metadata].[View] (ViewID, Name, EntityName, IsHierarchy, IsCartableIntegrated, FetchUrl) VALUES (48, 'CostCenterBalance2Column', 'ItemBalance', 0, 0, N'')
INSERT INTO [Metadata].[View] (ViewID, Name, EntityName, IsHierarchy, IsCartableIntegrated, FetchUrl) VALUES (49, 'CostCenterBalance4Column', 'ItemBalance', 0, 0, N'')
INSERT INTO [Metadata].[View] (ViewID, Name, EntityName, IsHierarchy, IsCartableIntegrated, FetchUrl) VALUES (50, 'CostCenterBalance6Column', 'ItemBalance', 0, 0, N'')
INSERT INTO [Metadata].[View] (ViewID, Name, EntityName, IsHierarchy, IsCartableIntegrated, FetchUrl) VALUES (51, 'CostCenterBalance8Column', 'ItemBalance', 0, 0, N'')
INSERT INTO [Metadata].[View] (ViewID, Name, EntityName, IsHierarchy, IsCartableIntegrated, FetchUrl) VALUES (52, 'CostCenterBalance10Column', 'ItemBalance', 0, 0, N'')
INSERT INTO [Metadata].[View] (ViewID, Name, EntityName, IsHierarchy, IsCartableIntegrated, FetchUrl) VALUES (53, 'ProjectBalance2Column', 'ItemBalance', 0, 0, N'')
INSERT INTO [Metadata].[View] (ViewID, Name, EntityName, IsHierarchy, IsCartableIntegrated, FetchUrl) VALUES (54, 'ProjectBalance4Column', 'ItemBalance', 0, 0, N'')
INSERT INTO [Metadata].[View] (ViewID, Name, EntityName, IsHierarchy, IsCartableIntegrated, FetchUrl) VALUES (55, 'ProjectBalance6Column', 'ItemBalance', 0, 0, N'')
INSERT INTO [Metadata].[View] (ViewID, Name, EntityName, IsHierarchy, IsCartableIntegrated, FetchUrl) VALUES (56, 'ProjectBalance8Column', 'ItemBalance', 0, 0, N'')
INSERT INTO [Metadata].[View] (ViewID, Name, EntityName, IsHierarchy, IsCartableIntegrated, FetchUrl) VALUES (57, 'ProjectBalance10Column', 'ItemBalance', 0, 0, N'')
INSERT INTO [Metadata].[View] (ViewID, Name, EntityName, IsHierarchy, IsCartableIntegrated, FetchUrl) VALUES (58, 'BalanceByAccount', 'BalanceByAccount', 0, 0, N'')
INSERT INTO [Metadata].[View] (ViewID, Name, EntityName, IsHierarchy, IsCartableIntegrated, FetchUrl) VALUES (59, 'SysOperationLog', 'SysOperationLog', 0, 0, N'')
INSERT INTO [Metadata].[View] (ViewID, Name, EntityName, IsHierarchy, IsCartableIntegrated, FetchUrl) VALUES (60, 'SysOperationLogArchive', 'SysOperationLogArchive', 0, 0, N'')
INSERT INTO [Metadata].[View] (ViewID, Name, EntityName, IsHierarchy, IsCartableIntegrated, FetchUrl) VALUES (61, 'OperationLogArchive', 'OperationLogArchive', 0, 0, N'')
INSERT INTO [Metadata].[View] ([ViewID], [Name], [EntityName], [IsHierarchy], [IsCartableIntegrated], [EntityType], [FetchUrl], [SearchUrl])
    VALUES (62, 'ProfitLoss', 'ProfitLoss', 0, 0, NULL, '', NULL)
INSERT INTO [Metadata].[View] ([ViewID], [Name], [EntityName], [IsHierarchy], [IsCartableIntegrated], [EntityType], [FetchUrl], [SearchUrl])
    VALUES (63, 'GroupActionResult', 'GroupActionResult', 0, 0, NULL, '', NULL)
INSERT INTO [Metadata].[View] ([ViewID], [Name], [EntityName], [IsHierarchy], [IsCartableIntegrated], [EntityType], [FetchUrl], [SearchUrl])
    VALUES (64, 'ProfitLossSimple', 'ProfitLoss', 0, 0, NULL, '', NULL)
INSERT INTO [Metadata].[View] ([ViewID], [Name], [EntityName], [IsHierarchy], [IsCartableIntegrated], [EntityType], [FetchUrl], [SearchUrl])
    VALUES (65, 'ComparativeProfitLoss', 'ProfitLoss', 0, 0, NULL, '', NULL)
INSERT INTO [Metadata].[View] ([ViewID], [Name], [EntityName], [IsHierarchy], [IsCartableIntegrated], [EntityType], [FetchUrl], [SearchUrl])
    VALUES (66, 'ComparativeProfitLossSimple', 'ProfitLoss', 0, 0, NULL, '', NULL)
INSERT INTO [Metadata].[View] ([ViewID], [Name], [EntityName], [IsHierarchy], [IsCartableIntegrated], [EntityType], [FetchUrl], [SearchUrl])
    VALUES (67, 'BalanceSheet', 'BalanceSheet', 0, 0, '', '', NULL)
INSERT INTO [Metadata].[View] ([ViewID], [Name], [EntityName], [IsHierarchy], [IsCartableIntegrated], [EntityType], [FetchUrl], [SearchUrl])
    VALUES (68, 'Widget', 'Dashboard', 0, 0, 'Core', NULL, NULL)
INSERT INTO [Metadata].[View] ([ViewID], [Name], [EntityName], [EntityType], [FetchUrl], [SearchUrl], [IsHierarchy], [IsCartableIntegrated])
    VALUES (69, 'CheckBookPage', N'CheckBookPage', N'Core', NULL, NULL, 0, 0)
INSERT INTO [Metadata].[View] ([ViewID], [Name], [EntityName], [EntityType], [FetchUrl], [SearchUrl], [IsHierarchy], [IsCartableIntegrated])
    VALUES (70, 'CashRegister', N'CashRegister', N'Base', NULL, NULL, 0, 0)
INSERT INTO [Metadata].[View] ([ViewID], [Name], [EntityName], [EntityType], [FetchUrl], [SearchUrl], [IsHierarchy], [IsCartableIntegrated])
    VALUES (71, 'CheckBook', N'CheckBook', N'Operational', NULL, NULL, 0, 0)
INSERT INTO [Metadata].[View] ([ViewID], [Name], [EntityName], [EntityType], [FetchUrl], [SearchUrl], [IsHierarchy], [IsCartableIntegrated])
    VALUES (72, 'CheckBookReport', N'CheckBookReport', NULL, NULL, NULL, 0, 0)
INSERT INTO [Metadata].[View] ([ViewID], [Name], [EntityName], [EntityType], [FetchUrl], [SearchUrl], [IsHierarchy], [IsCartableIntegrated])
    VALUES (73, 'SourceApp', N'SourceApp', N'Base', NULL, NULL, 0, 0)
INSERT INTO [Metadata].[View] ([ViewID], [Name], [EntityName], [EntityType], [FetchUrl], [SearchUrl], [IsHierarchy], [IsCartableIntegrated])
    VALUES (74, 'Payment', N'Payment', N'Operational', NULL, NULL, 0, 0)
INSERT INTO [Metadata].[View] ([ViewID], [Name], [EntityName], [EntityType], [FetchUrl], [SearchUrl], [IsHierarchy], [IsCartableIntegrated])
    VALUES (75, 'Receipt', N'Receipt', N'Operational', NULL, NULL, 0, 0)
INSERT INTO [Metadata].[View] ([ViewID], [Name], [EntityName], [EntityType], [FetchUrl], [SearchUrl], [IsHierarchy], [IsCartableIntegrated])
    VALUES (76, 'PayReceiveAccount', N'PayReceiveAccount', N'Core', NULL, NULL, 0, 0)
INSERT INTO [Metadata].[View] ([ViewID], [Name], [EntityName], [EntityType], [FetchUrl], [SearchUrl], [IsHierarchy], [IsCartableIntegrated])
    VALUES (77, 'PayReceiveCashAccount', N'PayReceiveCashAccount', N'Core', NULL, NULL, 0, 0)
INSERT INTO [Metadata].[View] ([ViewID], [Name], [EntityName], [EntityType], [FetchUrl], [SearchUrl], [IsHierarchy], [IsCartableIntegrated])
    VALUES (78, 'VouchersByDate', N'VouchersByDate', N'Operational', NULL, NULL, 0, 0)









































































INSERT INTO [Metadata].[View] ([ViewID], [Name], [EntityName], [EntityType], [FetchUrl], [SearchUrl], [IsHierarchy], [IsCartableIntegrated])
    VALUES (100001, 'Brand', N'Brand', N'Base', NULL, NULL, 0, 0)
INSERT INTO [Metadata].[View] ([ViewID], [Name], [EntityName], [EntityType], [FetchUrl], [SearchUrl], [IsHierarchy], [IsCartableIntegrated])
    VALUES (100002, 'Unit', N'Unit', N'Base', NULL, NULL, 0, 0)
INSERT INTO [Metadata].[View] ([ViewID], [Name], [EntityName], [EntityType], [FetchUrl], [SearchUrl], [IsHierarchy], [IsCartableIntegrated])
    VALUES (100003, 'Property', N'Property', N'Base', NULL, NULL, 0, 0)
SET IDENTITY_INSERT [Metadata].[View] OFF

SET IDENTITY_INSERT [Metadata].[Column] ON
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (1, 1, 'Id', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (2, 1, 'BranchScope', NULL, NULL, 'System.Int16', 'smallint', 'number', 0, 0, 0, 0, 0, 1, N'AlwaysHidden', -1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (3, 1, 'GroupId', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 1, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (4, 1, 'CurrencyId', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 1, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (5, 1, 'RowNo', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 1, 0, 0, N'AlwaysVisible', 0, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (6, 1, 'Code', NULL, NULL, 'System.String', 'nvarchar', 'string', 16, 0, 0, 0, 1, 1, NULL, 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (7, 1, 'FullCode', NULL, NULL, 'System.String', 'nvarchar', 'string', 256, 0, 0, 0, 1, 1, NULL, 2, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (8, 1, 'Name', NULL, NULL, 'System.String', 'nvarchar', 'string', 512, 0, 0, 0, 1, 1, N'AlwaysVisible', 3, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (9, 1, 'Level', NULL, NULL, 'System.Int16', 'smallint', 'number', 0, 0, 0, 0, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (10, 1, 'Description', NULL, NULL, 'System.String', 'nvarchar', 'string', 512, 0, 0, 1, 1, 1, NULL, 4, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (11, 1, 'State', NULL, NULL, 'System.String', 'nvarchar', 'string', 32, 0, 0, 1, 1, 1, NULL, 5, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (12, 1, 'IsCurrencyAdjustable', NULL, NULL, 'System.Boolean', 'bit', 'boolean', 0, 0, 0, 1, 1, 1, N'Hidden', 6, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (13, 1, 'TurnoverMode', NULL, NULL, 'System.String', 'nvarchar', 'string', 32, 0, 0, 1, 1, 1, N'Hidden', 7, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (14, 2, 'Id', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (15, 2, 'StatusId', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (16, 2, 'Type', NULL, NULL, 'System.Int16', 'smallint', 'number', 0, 0, 0, 0, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (17, 2, 'RowNo', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 1, 0, 0, N'AlwaysVisible', 0, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (18, 2, 'No', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 1, 1, N'AlwaysVisible', 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (19, 2, 'Date', NULL, NULL, 'System.Date', 'datetime', 'Date', 0, 0, 0, 0, 1, 1, NULL, 2, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (20, 2, 'Description', NULL, NULL, 'System.String', 'nvarchar', 'string', 512, 0, 0, 1, 1, 1, NULL, 3, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (21, 2, 'StatusName', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 1, 1, 1, NULL, 4, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (22, 2, 'Reference', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 1, 1, 1, NULL, 5, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (23, 2, 'Association', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 1, 1, 1, NULL, 6, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (24, 2, 'DailyNo', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 1, 1, NULL, 7, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (25, 3, 'Id', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (26, 3, 'CurrencyId', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 1, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (27, 3, 'FullAccount', NULL, NULL, 'System.Object', '(n/a)', 'object', 0, 0, 0, 0, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (28, 3, 'FullAccount.Account.Id', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (29, 3, 'FullAccount.DetailAccount.Id', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 1, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (30, 3, 'FullAccount.CostCenter.Id', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 1, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (31, 3, 'FullAccount.Project.Id', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 1, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (32, 3, 'CurrencyRate', NULL, 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 1, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (33, 3, 'TypeId', NULL, NULL, 'System.Int16', 'smallint', 'number', 0, 0, 0, 0, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (34, 3, 'RowNo', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 1, 0, 0, N'AlwaysVisible', 0, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (35, 3, 'FullAccount.Account.FullCode', NULL, NULL, 'System.String', 'nvarchar', 'string', 256, 0, 0, 0, 1, 1, NULL, 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (36, 3, 'FullAccount.Account.Name', NULL, NULL, 'System.String', 'nvarchar', 'string', 128, 0, 0, 0, 1, 1, NULL, 2, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (37, 3, 'FullAccount.DetailAccount.FullCode', NULL, NULL, 'System.String', 'nvarchar', 'string', 128, 0, 0, 0, 1, 1, N'Hidden', 3, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (38, 3, 'FullAccount.DetailAccount.Name', NULL, NULL, 'System.String', 'nvarchar', 'string', 128, 0, 0, 0, 1, 1, N'Hidden', 4, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (39, 3, 'FullAccount.CostCenter.FullCode', NULL, NULL, 'System.String', 'nvarchar', 'string', 128, 0, 0, 0, 1, 1, N'Hidden', 5, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (40, 3, 'FullAccount.CostCenter.Name', NULL, NULL, 'System.String', 'nvarchar', 'string', 128, 0, 0, 0, 1, 1, N'Hidden', 6, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (41, 3, 'FullAccount.Project.FullCode', NULL, NULL, 'System.String', 'nvarchar', 'string', 128, 0, 0, 0, 1, 1, N'Hidden', 7, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (42, 3, 'FullAccount.Project.Name', NULL, NULL, 'System.String', 'nvarchar', 'string', 128, 0, 0, 0, 1, 1, N'Hidden', 8, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (43, 3, 'Description', NULL, NULL, 'System.String', 'nvarchar', 'string', 512, 0, 0, 1, 1, 1, NULL, 10, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (44, 3, 'Debit', NULL, 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 11, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (45, 3, 'Credit', NULL, 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 12, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (46, 3, 'CurrencyName', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 0, 1, 1, N'AlwaysVisible', 13, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (47, 3, 'CurrencyValue', NULL, 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 1, 1, 1, N'AlwaysVisible', 14, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (48, 4, 'Id', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (49, 4, 'RowNo', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 1, 0, 0, N'AlwaysVisible', 0, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (50, 4, 'UserName', NULL, NULL, 'System.String', 'nvarchar(64)', 'string', 64, 0, 0, 0, 1, 1, N'AlwaysVisible', 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (51, 4, 'LastLoginDate', NULL, NULL, 'System.DateTime', 'datetime', 'DateTime', 0, 0, 0, 1, 1, 1, NULL, 2, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (52, 4, 'IsEnabled', NULL, NULL, 'System.Boolean', 'bit', 'boolean', 0, 0, 0, 0, 1, 1, NULL, 3, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (53, 4, 'PersonFullName', NULL, NULL, 'System.String', 'nvarchar(64)', 'string', 64, 0, 0, 0, 1, 1, NULL, 4, 'Person.FullName')
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (55, 5, 'RowNo', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 1, 0, 0, N'AlwaysVisible', 0, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (56, 5, 'Name', NULL, NULL, 'System.String', 'nvarchar(64)', 'string', 64, 0, 0, 0, 1, 1, N'AlwaysVisible', 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (57, 5, 'Description', NULL, NULL, 'System.String', 'nvarchar(512)', 'string', 512, 0, 0, 1, 1, 1, NULL, 2, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (58, 6, 'Id', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (59, 6, 'Level', NULL, NULL, 'System.Int16', 'smallint', '', 0, 0, 0, 0, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (60, 6, 'BranchScope', NULL, NULL, 'System.Int16', 'smallint', 'number', 0, 0, 0, 0, 0, 1, N'AlwaysHidden', -1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (61, 6, 'CurrencyId', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 1, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (62, 6, 'RowNo', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 1, 0, 0, N'AlwaysVisible', 0, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (63, 6, 'Code', NULL, NULL, 'System.String', 'nvarchar', 'string', 16, 0, 0, 0, 1, 1, NULL, 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (64, 6, 'FullCode', NULL, NULL, 'System.String', 'nvarchar', 'string', 256, 0, 0, 0, 1, 1, NULL, 2, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (65, 6, 'Name', NULL, NULL, 'System.String', 'nvarchar', 'string', 512, 0, 0, 0, 1, 1, N'AlwaysVisible', 3, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (66, 6, 'Description', NULL, NULL, 'System.String', 'nvarchar', 'string', 512, 0, 0, 1, 1, 1, NULL, 4, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (67, 7, 'Id', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (68, 7, 'Level', NULL, NULL, 'System.Int16', 'smallint', '', 0, 0, 0, 0, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (69, 7, 'BranchScope', NULL, NULL, 'System.Int16', 'smallint', 'number', 0, 0, 0, 0, 0, 1, N'AlwaysHidden', -1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (70, 7, 'RowNo', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 1, 0, 0, N'AlwaysVisible', 0, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (71, 7, 'Code', NULL, NULL, 'System.String', 'nvarchar', 'string', 16, 0, 0, 0, 1, 1, NULL, 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (72, 7, 'FullCode', NULL, NULL, 'System.String', 'nvarchar', 'string', 256, 0, 0, 0, 1, 1, NULL, 2, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (73, 7, 'Name', NULL, NULL, 'System.String', 'nvarchar', 'string', 512, 0, 0, 0, 1, 1, N'AlwaysVisible', 3, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (74, 7, 'Description', NULL, NULL, 'System.String', 'nvarchar', 'string', 512, 0, 0, 1, 1, 1, NULL, 4, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (75, 8, 'Id', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (76, 8, 'Level', NULL, NULL, 'System.Int16', 'smallint', '', 0, 0, 0, 0, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (77, 8, 'BranchScope', NULL, NULL, 'System.Int16', 'smallint', 'number', 0, 0, 0, 0, 0, 1, N'AlwaysHidden', -1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (78, 8, 'RowNo', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 1, 0, 0, N'AlwaysVisible', 0, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (79, 8, 'Code', NULL, NULL, 'System.String', 'nvarchar', 'string', 16, 0, 0, 0, 1, 1, NULL, 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (80, 8, 'FullCode', NULL, NULL, 'System.String', 'nvarchar', 'string', 256, 0, 0, 0, 1, 1, NULL, 2, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (81, 8, 'Name', NULL, NULL, 'System.String', 'nvarchar', 'string', 512, 0, 0, 0, 1, 1, N'AlwaysVisible', 3, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (82, 8, 'Description', NULL, NULL, 'System.String', 'nvarchar', 'string', 512, 0, 0, 1, 1, 1, NULL, 4, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (83, 9, 'Id', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (84, 9, 'RowNo', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 1, 0, 0, N'AlwaysVisible', 0, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (85, 9, 'Name', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 0, 1, 1, N'AlwaysVisible', 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (86, 9, 'StartDate', NULL, NULL, 'System.Date', 'datetime', 'Date', 0, 0, 0, 0, 1, 1, NULL, 2, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (87, 9, 'EndDate', NULL, NULL, 'System.Date', 'datetime', 'Date', 0, 0, 0, 0, 1, 1, NULL, 3, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (88, 9, 'Description', NULL, NULL, 'System.String', 'nvarchar', 'string', 512, 0, 0, 1, 1, 1, NULL, 4, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (89, 10, 'Id', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (90, 10, 'RowNo', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 1, 0, 0, N'AlwaysVisible', 0, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (91, 10, 'Name', NULL, NULL, 'System.String', 'nvarchar', 'string', 128, 0, 0, 0, 1, 1, N'AlwaysVisible', 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (92, 10, 'Description', NULL, NULL, 'System.String', 'nvarchar', 'string', 512, 0, 0, 1, 1, 1, NULL, 2, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (93, 11, 'Id', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (94, 11, 'RowNo', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 1, 0, 0, N'AlwaysVisible', 0, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (95, 11, 'Name', NULL, NULL, 'System.String', 'nvarchar', 'string', 128, 0, 0, 0, 1, 1, N'AlwaysVisible', 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (96, 11, 'DbName', NULL, NULL, 'System.String', 'nvarchar', 'string', 128, 0, 0, 0, 1, 1, NULL, 2, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (97, 11, 'Description', NULL, NULL, 'System.String', 'nvarchar', 'string', 512, 0, 0, 1, 1, 1, NULL, 5, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (98, 11, 'Server', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 1, 1, 1, NULL, 3, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (99, 11, 'UserName', NULL, NULL, 'System.String', 'nvarchar', 'string', 32, 0, 0, 1, 1, 1, N'AlwaysVisible', 4, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (100, 11, 'Password', NULL, NULL, 'System.String', 'nvarchar', 'string', 32, 0, 0, 1, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (101, 12, 'Id', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (102, 12, 'RowNo', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 1, 0, 0, N'AlwaysVisible', 0, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (103, 12, 'Name', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 0, 1, 1, N'AlwaysVisible', 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (104, 12, 'Category', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 0, 1, 1, NULL, 2, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (105, 12, 'Description', NULL, NULL, 'System.String', 'nvarchar', 'string', 256, 0, 0, 1, 1, 1, NULL, 3, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (106, 13, 'Id', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 1, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (107, 13, 'RowNo', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 0, 0, N'AlwaysVisible', 0, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (108, 13, 'UserName', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 0, 1, 1, NULL, 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (109, 13, 'BranchName', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 0, 1, 1, NULL, 2, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (110, 13, 'FiscalPeriodName', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 0, 1, 1, NULL, 3, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (111, 13, 'Date', NULL, NULL, 'System.Date', 'datetime', 'Date', 0, 0, 0, 0, 1, 1, N'AlwaysVisible', 4, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (112, 13, 'Time', NULL, NULL, 'System.TimeSpan', 'time', 'Date', 7, 0, 0, 0, 1, 1, N'AlwaysVisible', 5, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (113, 13, 'EntityTypeName', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 0, 1, 1, NULL, 6, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (114, 13, 'EntityCode', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 0, 1, 1, NULL, 7, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (115, 13, 'EntityName', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 0, 1, 1, NULL, 8, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (116, 13, 'EntityDescription', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 0, 1, 1, NULL, 9, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (117, 13, 'EntityNo', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 1, 1, NULL, 10, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (118, 13, 'EntityDate', NULL, NULL, 'System.DateTime', 'datetime', 'Date', 0, 0, 0, 0, 1, 1, NULL, 11, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (119, 14, 'Id', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (120, 14, 'RowNo', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 1, 0, 0, N'AlwaysVisible', 0, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (121, 14, 'Name', NULL, NULL, 'System.String', 'nvarchar', 'string', 512, 0, 0, 0, 1, 1, N'AlwaysVisible', 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (122, 14, 'FullCode', NULL, NULL, 'System.String', 'nvarchar', 'string', 256, 0, 0, 0, 1, 1, NULL, 2, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (123, 15, 'RowNo', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 1, 0, 0, N'AlwaysVisible', 0, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (124, 15, 'VoucherDate', NULL, NULL, 'System.Date', 'datetime', 'Date', 0, 0, 0, 0, 1, 1, NULL, 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (125, 15, 'VoucherNo', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 1, 1, NULL, 2, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (126, 15, 'AccountFullCode', NULL, NULL, 'System.String', 'nvarchar', 'string', 512, 0, 0, 0, 1, 1, NULL, 3, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (127, 15, 'AccountName', NULL, NULL, 'System.String', 'nvarchar', 'string', 512, 0, 0, 0, 1, 1, NULL, 4, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (128, 15, 'Description', NULL, NULL, 'System.String', 'nvarchar', 'string', 512, 0, 0, 0, 1, 1, NULL, 5, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (129, 15, 'Debit', NULL, 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 6, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (130, 15, 'Credit', NULL, 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 7, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (131, 15, 'BranchName', NULL, NULL, 'System.String', 'nvarchar', 'string', 128, 0, 0, 0, 1, 1, N'AlwaysVisible', 8, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (132, 15, 'Mark', NULL, NULL, 'System.String', 'nvarchar', 'string', 128, 0, 0, 1, 1, 1, N'Visible', 9, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (133, 16, 'RowNo', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 1, 0, 0, N'AlwaysVisible', 0, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (134, 16, 'VoucherDate', NULL, NULL, 'System.Date', 'datetime', 'Date', 0, 0, 0, 0, 1, 1, NULL, 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (135, 16, 'VoucherNo', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 1, 1, NULL, 2, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (136, 16, 'AccountFullCode', NULL, NULL, 'System.String', 'nvarchar', 'string', 512, 0, 0, 0, 1, 1, NULL, 3, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (137, 16, 'AccountName', NULL, NULL, 'System.String', 'nvarchar', 'string', 512, 0, 0, 0, 1, 1, NULL, 4, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (138, 16, 'DetailAccountFullCode', NULL, NULL, 'System.String', 'nvarchar', 'string', 512, 0, 0, 0, 1, 1, NULL, 5, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (139, 16, 'DetailAccountName', NULL, NULL, 'System.String', 'nvarchar', 'string', 512, 0, 0, 0, 1, 1, NULL, 6, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (140, 16, 'CostCenterFullCode', NULL, NULL, 'System.String', 'nvarchar', 'string', 512, 0, 0, 0, 1, 1, NULL, 7, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (141, 16, 'CostCenterName', NULL, NULL, 'System.String', 'nvarchar', 'string', 512, 0, 0, 0, 1, 1, NULL, 8, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (142, 16, 'ProjectFullCode', NULL, NULL, 'System.String', 'nvarchar', 'string', 512, 0, 0, 0, 1, 1, NULL, 9, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (143, 16, 'ProjectName', NULL, NULL, 'System.String', 'nvarchar', 'string', 512, 0, 0, 0, 1, 1, NULL, 10, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (144, 16, 'Description', NULL, NULL, 'System.String', 'nvarchar', 'string', 512, 0, 0, 0, 1, 1, NULL, 11, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (145, 16, 'Debit', NULL, 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 12, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (146, 16, 'Credit', NULL, 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 13, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (147, 16, 'BranchName', NULL, NULL, 'System.String', 'nvarchar', 'string', 128, 0, 0, 0, 1, 1, N'AlwaysVisible', 14, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (148, 16, 'Mark', NULL, NULL, 'System.String', 'nvarchar', 'string', 128, 0, 0, 1, 1, 1, N'Visible', 15, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (149, 17, 'RowNo', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 1, 0, 0, N'AlwaysVisible', 0, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (150, 17, 'VoucherDate', NULL, NULL, 'System.Date', 'datetime', 'Date', 0, 0, 0, 0, 1, 1, NULL, 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (151, 17, 'VoucherNo', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 1, 1, NULL, 2, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (152, 17, 'AccountFullCode', NULL, NULL, 'System.String', 'nvarchar', 'string', 512, 0, 0, 0, 1, 1, NULL, 3, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (153, 17, 'AccountName', NULL, NULL, 'System.String', 'nvarchar', 'string', 512, 0, 0, 0, 1, 1, NULL, 4, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (154, 17, 'Description', NULL, NULL, 'System.String', 'nvarchar', 'string', 512, 0, 0, 0, 1, 1, NULL, 5, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (155, 17, 'Debit', NULL, 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 6, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (156, 17, 'Credit', NULL, 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 7, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (157, 17, 'BranchName', NULL, NULL, 'System.String', 'nvarchar', 'string', 128, 0, 0, 0, 1, 1, N'AlwaysVisible', 8, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (158, 18, 'RowNo', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 1, 0, 0, N'AlwaysVisible', 0, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (159, 18, 'VoucherDate', NULL, NULL, 'System.Date', 'datetime', 'Date', 0, 0, 0, 0, 1, 1, NULL, 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (160, 18, 'VoucherNo', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 1, 1, NULL, 2, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (161, 18, 'AccountFullCode', NULL, NULL, 'System.String', 'nvarchar', 'string', 512, 0, 0, 0, 1, 1, NULL, 3, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (162, 18, 'AccountName', NULL, NULL, 'System.String', 'nvarchar', 'string', 512, 0, 0, 0, 1, 1, NULL, 4, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (163, 18, 'Description', NULL, NULL, 'System.String', 'nvarchar', 'string', 512, 0, 0, 0, 1, 1, NULL, 5, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (164, 18, 'Debit', NULL, 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 6, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (165, 18, 'Credit', NULL, 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 7, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (166, 18, 'BranchName', NULL, NULL, 'System.String', 'nvarchar', 'string', 128, 0, 0, 0, 1, 1, N'AlwaysVisible', 8, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (167, 19, 'RowNo', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 1, 0, 0, N'AlwaysVisible', 0, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (168, 19, 'AccountFullCode', NULL, NULL, 'System.String', 'nvarchar', 'string', 512, 0, 0, 0, 1, 1, NULL, 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (169, 19, 'AccountName', NULL, NULL, 'System.String', 'nvarchar', 'string', 512, 0, 0, 0, 1, 1, NULL, 2, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (170, 19, 'Description', NULL, NULL, 'System.String', 'nvarchar', 'string', 512, 0, 0, 0, 1, 1, NULL, 3, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (171, 19, 'Debit', NULL, 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 4, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (172, 19, 'Credit', NULL, 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 5, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (173, 19, 'BranchName', NULL, NULL, 'System.String', 'nvarchar', 'string', 128, 0, 0, 0, 1, 1, N'AlwaysVisible', 6, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (174, 20, 'RowNo', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 1, 0, 0, N'AlwaysVisible', 0, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (175, 20, 'VoucherDate', NULL, NULL, 'System.Date', 'datetime', 'Date', 0, 0, 0, 0, 1, 1, NULL, 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (176, 20, 'AccountFullCode', NULL, NULL, 'System.String', 'nvarchar', 'string', 512, 0, 0, 0, 1, 1, NULL, 2, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (177, 20, 'AccountName', NULL, NULL, 'System.String', 'nvarchar', 'string', 512, 0, 0, 0, 1, 1, NULL, 3, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (178, 20, 'Description', NULL, NULL, 'System.String', 'nvarchar', 'string', 512, 0, 0, 0, 1, 1, NULL, 4, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (179, 20, 'Debit', NULL, 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 5, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (180, 20, 'Credit', NULL, 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 6, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (181, 20, 'BranchName', NULL, NULL, 'System.String', 'nvarchar', 'string', 128, 0, 0, 0, 1, 1, N'AlwaysVisible', 7, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (182, 21, 'RowNo', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 1, 0, 0, N'AlwaysVisible', 0, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (183, 21, 'VoucherDate', NULL, NULL, 'System.Date', 'datetime', 'Date', 0, 0, 0, 0, 1, 1, NULL, 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (184, 21, 'AccountFullCode', NULL, NULL, 'System.String', 'nvarchar', 'string', 512, 0, 0, 0, 1, 1, NULL, 2, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (185, 21, 'AccountName', NULL, NULL, 'System.String', 'nvarchar', 'string', 512, 0, 0, 0, 1, 1, NULL, 3, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (186, 21, 'Description', NULL, NULL, 'System.String', 'nvarchar', 'string', 512, 0, 0, 0, 1, 1, NULL, 4, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (187, 21, 'Debit', NULL, 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 5, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (188, 21, 'Credit', NULL, 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 6, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (189, 21, 'BranchName', NULL, NULL, 'System.String', 'nvarchar', 'string', 128, 0, 0, 0, 1, 1, N'AlwaysVisible', 7, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (190, 22, 'RowNo', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 1, 0, 0, N'AlwaysVisible', 0, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (191, 22, 'VoucherDate', NULL, NULL, 'System.Date', 'datetime', 'Date', 0, 0, 0, 0, 1, 1, NULL, 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (192, 22, 'VoucherNo', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 1, 1, NULL, 2, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (193, 22, 'AccountFullCode', NULL, NULL, 'System.String', 'nvarchar', 'string', 512, 0, 0, 0, 1, 1, NULL, 3, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (194, 22, 'AccountName', NULL, NULL, 'System.String', 'nvarchar', 'string', 512, 0, 0, 0, 1, 1, NULL, 4, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (195, 22, 'Description', NULL, NULL, 'System.String', 'nvarchar', 'string', 512, 0, 0, 0, 1, 1, NULL, 5, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (196, 22, 'Debit', NULL, 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 6, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (197, 22, 'Credit', NULL, 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 7, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (198, 22, 'BranchName', NULL, NULL, 'System.String', 'nvarchar', 'string', 128, 0, 0, 0, 1, 1, N'AlwaysVisible', 8, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (199, 22, 'Mark', NULL, NULL, 'System.String', 'nvarchar', 'string', 128, 0, 0, 1, 1, 1, N'Visible', 9, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (200, 23, 'RowNo', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 1, 0, 0, N'AlwaysVisible', 0, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (201, 23, 'VoucherDate', NULL, NULL, 'System.Date', 'datetime', 'Date', 0, 0, 0, 0, 1, 1, NULL, 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (202, 23, 'VoucherNo', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 1, 1, NULL, 2, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (203, 23, 'AccountFullCode', NULL, NULL, 'System.String', 'nvarchar', 'string', 512, 0, 0, 0, 1, 1, NULL, 3, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (204, 23, 'AccountName', NULL, NULL, 'System.String', 'nvarchar', 'string', 512, 0, 0, 0, 1, 1, NULL, 4, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (205, 23, 'DetailAccountFullCode', NULL, NULL, 'System.String', 'nvarchar', 'string', 512, 0, 0, 0, 1, 1, NULL, 5, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (206, 23, 'DetailAccountName', NULL, NULL, 'System.String', 'nvarchar', 'string', 512, 0, 0, 0, 1, 1, NULL, 6, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (207, 23, 'CostCenterFullCode', NULL, NULL, 'System.String', 'nvarchar', 'string', 512, 0, 0, 0, 1, 1, NULL, 7, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (208, 23, 'CostCenterName', NULL, NULL, 'System.String', 'nvarchar', 'string', 512, 0, 0, 0, 1, 1, NULL, 8, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (209, 23, 'ProjectFullCode', NULL, NULL, 'System.String', 'nvarchar', 'string', 512, 0, 0, 0, 1, 1, NULL, 9, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (210, 23, 'ProjectName', NULL, NULL, 'System.String', 'nvarchar', 'string', 512, 0, 0, 0, 1, 1, NULL, 10, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (211, 23, 'Description', NULL, NULL, 'System.String', 'nvarchar', 'string', 512, 0, 0, 0, 1, 1, NULL, 11, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (212, 23, 'Debit', NULL, 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 12, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (213, 23, 'Credit', NULL, 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 13, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (214, 23, 'BranchName', NULL, NULL, 'System.String', 'nvarchar', 'string', 128, 0, 0, 0, 1, 1, N'AlwaysVisible', 14, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (215, 23, 'Mark', NULL, NULL, 'System.String', 'nvarchar', 'string', 128, 0, 0, 1, 1, 1, N'Visible', 15, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (216, 24, 'RowNo', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 1, 0, 0, N'AlwaysVisible', 0, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (217, 24, 'VoucherDate', NULL, NULL, 'System.Date', 'datetime', 'Date', 0, 0, 0, 0, 1, 1, NULL, 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (218, 24, 'VoucherNo', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 1, 1, NULL, 2, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (219, 24, 'AccountFullCode', NULL, NULL, 'System.String', 'nvarchar', 'string', 512, 0, 0, 0, 1, 1, NULL, 3, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (220, 24, 'AccountName', NULL, NULL, 'System.String', 'nvarchar', 'string', 512, 0, 0, 0, 1, 1, NULL, 4, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (221, 24, 'Description', NULL, NULL, 'System.String', 'nvarchar', 'string', 512, 0, 0, 0, 1, 1, NULL, 5, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (222, 24, 'Debit', NULL, 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 6, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (223, 24, 'Credit', NULL, 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 7, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (224, 24, 'BranchName', NULL, NULL, 'System.String', 'nvarchar', 'string', 128, 0, 0, 0, 1, 1, N'AlwaysVisible', 8, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (225, 25, 'RowNo', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 1, 0, 0, N'AlwaysVisible', 0, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (226, 25, 'VoucherDate', NULL, NULL, 'System.Date', 'datetime', 'Date', 0, 0, 0, 0, 1, 1, NULL, 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (227, 25, 'VoucherNo', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 1, 1, NULL, 2, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (228, 25, 'AccountFullCode', NULL, NULL, 'System.String', 'nvarchar', 'string', 512, 0, 0, 0, 1, 1, NULL, 3, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (229, 25, 'AccountName', NULL, NULL, 'System.String', 'nvarchar', 'string', 512, 0, 0, 0, 1, 1, NULL, 4, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (230, 25, 'Description', NULL, NULL, 'System.String', 'nvarchar', 'string', 512, 0, 0, 0, 1, 1, NULL, 5, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (231, 25, 'Debit', NULL, 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 6, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (232, 25, 'Credit', NULL, 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 7, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (233, 25, 'BranchName', NULL, NULL, 'System.String', 'nvarchar', 'string', 128, 0, 0, 0, 1, 1, N'AlwaysVisible', 8, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (234, 26, 'RowNo', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 1, 0, 0, N'AlwaysVisible', 0, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (235, 26, 'AccountFullCode', NULL, NULL, 'System.String', 'nvarchar', 'string', 512, 0, 0, 0, 1, 1, NULL, 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (236, 26, 'AccountName', NULL, NULL, 'System.String', 'nvarchar', 'string', 512, 0, 0, 0, 1, 1, NULL, 2, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (237, 26, 'Description', NULL, NULL, 'System.String', 'nvarchar', 'string', 512, 0, 0, 0, 1, 1, NULL, 3, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (238, 26, 'Debit', NULL, 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 4, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (239, 26, 'Credit', NULL, 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 5, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (240, 26, 'BranchName', NULL, NULL, 'System.String', 'nvarchar', 'string', 128, 0, 0, 0, 1, 1, N'AlwaysVisible', 6, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (241, 27, 'RowNo', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 1, 0, 0, N'AlwaysVisible', 0, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (242, 27, 'VoucherDate', NULL, NULL, 'System.Date', 'datetime', 'Date', 0, 0, 0, 0, 1, 1, NULL, 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (243, 27, 'VoucherNo', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 1, 1, NULL, 2, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (244, 27, 'Description', NULL, NULL, 'System.String', 'nvarchar', 'string', 512, 0, 0, 0, 1, 1, NULL, 3, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (245, 27, 'Debit', NULL, 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 4, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (246, 27, 'Credit', NULL, 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 5, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (247, 27, 'Balance', NULL, 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 0, 0, NULL, 6, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (248, 27, 'Mark', NULL, NULL, 'System.String', 'nvarchar', 'string', 128, 0, 0, 0, 1, 1, NULL, 7, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (249, 27, 'BranchName', NULL, NULL, 'System.String', 'nvarchar', 'string', 128, 0, 0, 0, 1, 1, N'AlwaysVisible', 8, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (250, 28, 'RowNo', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 1, 0, 0, N'AlwaysVisible', 0, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (251, 28, 'VoucherDate', NULL, NULL, 'System.Date', 'datetime', 'Date', 0, 0, 0, 0, 1, 1, NULL, 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (252, 28, 'VoucherNo', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 1, 1, NULL, 2, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (253, 28, 'Description', NULL, NULL, 'System.String', 'nvarchar', 'string', 512, 0, 0, 0, 1, 1, NULL, 3, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (254, 28, 'Debit', NULL, 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 4, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (255, 28, 'Credit', NULL, 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 5, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (256, 28, 'Balance', NULL, 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 0, 0, NULL, 6, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (257, 28, 'BranchName', NULL, NULL, 'System.String', 'nvarchar', 'string', 128, 0, 0, 0, 1, 1, N'AlwaysVisible', 7, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (258, 29, 'RowNo', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 1, 0, 0, N'AlwaysVisible', 0, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (259, 29, 'VoucherDate', NULL, NULL, 'System.Date', 'datetime', 'Date', 0, 0, 0, 0, 1, 1, NULL, 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (260, 29, 'LineCount', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 1, 1, NULL, 2, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (261, 29, 'Description', NULL, NULL, 'System.String', 'nvarchar', 'string', 512, 0, 0, 0, 1, 1, NULL, 3, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (262, 29, 'Debit', NULL, 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 4, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (263, 29, 'Credit', NULL, 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 5, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (264, 29, 'Balance', NULL, 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 0, 0, NULL, 6, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (265, 29, 'BranchName', NULL, NULL, 'System.String', 'nvarchar', 'string', 128, 0, 0, 0, 1, 1, N'AlwaysVisible', 7, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (266, 30, 'Id', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (267, 30, 'BranchScope', NULL, NULL, 'System.Int16', 'smallint', 'number', 0, 0, 0, 0, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (268, 30, 'BranchId', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (269, 30, 'TaxCode', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (270, 30, 'RowNo', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 1, 0, 0, N'AlwaysVisible', 0, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (271, 30, 'Name', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 0, 1, 1, N'AlwaysVisible', 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (272, 30, 'Code', NULL, NULL, 'System.String', 'nvarchar', 'string', 8, 0, 0, 0, 1, 1, NULL, 2, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (274, 30, 'MinorUnit', NULL, NULL, 'System.String', 'nvarchar', 'string', 16, 0, 0, 0, 1, 1, NULL, 3, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (275, 30, 'Description', NULL, NULL, 'System.String', 'nvarchar', 'string', 512, 0, 0, 1, 1, 1, NULL, 4, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (276, 30, 'BranchName', NULL, NULL, 'System.String', 'nvarchar', 'string', 128, 0, 0, 0, 1, 1, NULL, 5, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (277, 30, 'DecimalCount', NULL, NULL, 'System.Int16', 'smallint', 'number', 0, 0, 0, 0, 1, 1, N'Hidden', 6, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (278, 30, 'State', NULL, NULL, 'System.String', 'nvarchar', 'string', 32, 0, 0, 1, 1, 1, NULL, 7, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (279, 31, 'Id', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (280, 31, 'CurrencyId', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (281, 31, 'BranchId', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (282, 31, 'BranchScope', NULL, NULL, 'System.Int16', 'smallint', 'number', 0, 0, 0, 0, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (283, 31, 'RowNo', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 1, 0, 0, N'AlwaysVisible', 0, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (284, 31, 'Date', NULL, NULL, 'System.Date', 'datetime', 'Date', 0, 0, 0, 0, 1, 1, NULL, 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (285, 31, 'Time', NULL, NULL, 'System.TimeSpan', 'time', 'number', 0, 0, 0, 0, 1, 1, NULL, 2, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (286, 31, 'Multiplier', NULL, 'Money', 'System.decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 3, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (287, 31, 'BranchName', NULL, NULL, 'System.String', 'nvarchar', 'string', 128, 0, 0, 0, 1, 1, NULL, 4, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (288, 31, 'Description', NULL, NULL, 'System.String', 'nvarchar', 'string', 512, 0, 0, 1, 1, 1, NULL, 5, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (289, 32, 'RowNo', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 1, 0, 0, N'AlwaysVisible', 0, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (290, 32, 'AccountFullCode', NULL, NULL, 'System.String', 'nvarchar', 'string', 512, 0, 0, 0, 1, 1, NULL, 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (291, 32, 'AccountName', NULL, NULL, 'System.String', 'nvarchar', 'string', 512, 0, 0, 0, 1, 1, NULL, 2, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (292, 32, 'EndBalanceDebit', N'EndBalance', 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 3, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (293, 32, 'EndBalanceCredit', N'EndBalance', 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 4, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (294, 32, 'BranchName', NULL, NULL, 'System.String', 'nvarchar', 'string', 128, 0, 0, 0, 1, 1, N'AlwaysVisible', 5, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (295, 32, 'VoucherReference', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 1, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (296, 33, 'RowNo', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 1, 0, 0, N'AlwaysVisible', 0, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (297, 33, 'AccountFullCode', NULL, NULL, 'System.String', 'nvarchar', 'string', 512, 0, 0, 0, 1, 1, NULL, 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (298, 33, 'AccountName', NULL, NULL, 'System.String', 'nvarchar', 'string', 512, 0, 0, 0, 1, 1, NULL, 2, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (299, 33, 'TurnoverDebit', N'Turnover', 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 3, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (300, 33, 'TurnoverCredit', N'Turnover', 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 4, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (301, 33, 'EndBalanceDebit', N'EndBalance', 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 5, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (302, 33, 'EndBalanceCredit', N'EndBalance', 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 6, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (303, 33, 'BranchName', NULL, NULL, 'System.String', 'nvarchar', 'string', 128, 0, 0, 0, 1, 1, N'AlwaysVisible', 7, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (304, 33, 'VoucherReference', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 1, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (305, 34, 'RowNo', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 1, 0, 0, N'AlwaysVisible', 0, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (306, 34, 'AccountFullCode', NULL, NULL, 'System.String', 'nvarchar', 'string', 512, 0, 0, 0, 1, 1, NULL, 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (307, 34, 'AccountName', NULL, NULL, 'System.String', 'nvarchar', 'string', 512, 0, 0, 0, 1, 1, NULL, 2, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (308, 34, 'StartBalanceDebit', N'StartBalance', 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 3, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (309, 34, 'StartBalanceCredit', N'StartBalance', 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 4, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (310, 34, 'TurnoverDebit', N'Turnover', 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 5, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (311, 34, 'TurnoverCredit', N'Turnover', 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 6, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (312, 34, 'EndBalanceDebit', N'EndBalance', 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 7, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (313, 34, 'EndBalanceCredit', N'EndBalance', 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 8, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (314, 34, 'BranchName', NULL, NULL, 'System.String', 'nvarchar', 'string', 128, 0, 0, 0, 1, 1, N'AlwaysVisible', 9, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (315, 34, 'VoucherReference', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 1, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (316, 35, 'RowNo', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 1, 0, 0, N'AlwaysVisible', 0, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (317, 35, 'AccountFullCode', NULL, NULL, 'System.String', 'nvarchar', 'string', 512, 0, 0, 0, 1, 1, NULL, 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (318, 35, 'AccountName', NULL, NULL, 'System.String', 'nvarchar', 'string', 512, 0, 0, 0, 1, 1, NULL, 2, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (319, 35, 'StartBalanceDebit', N'StartBalance', 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 3, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (320, 35, 'StartBalanceCredit', N'StartBalance', 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 4, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (321, 35, 'TurnoverDebit', N'Turnover', 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 5, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (322, 35, 'TurnoverCredit', N'Turnover', 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 6, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (323, 35, 'OperationSumDebit', N'OperationSum', 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 7, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (324, 35, 'OperationSumCredit', N'OperationSum', 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 8, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (325, 35, 'EndBalanceDebit', N'EndBalance', 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 9, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (326, 35, 'EndBalanceCredit', N'EndBalance', 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 10, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (327, 35, 'BranchName', NULL, NULL, 'System.String', 'nvarchar', 'string', 128, 0, 0, 0, 1, 1, N'AlwaysVisible', 11, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (328, 35, 'VoucherReference', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 1, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (329, 36, 'RowNo', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 1, 0, 0, N'AlwaysVisible', 0, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (330, 36, 'AccountFullCode', NULL, NULL, 'System.String', 'nvarchar', 'string', 512, 0, 0, 0, 1, 1, NULL, 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (331, 36, 'AccountName', NULL, NULL, 'System.String', 'nvarchar', 'string', 512, 0, 0, 0, 1, 1, NULL, 2, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (332, 36, 'StartBalanceDebit', N'StartBalance', 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 3, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (333, 36, 'StartBalanceCredit', N'StartBalance', 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 4, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (334, 36, 'TurnoverDebit', N'Turnover', 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 5, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (335, 36, 'TurnoverCredit', N'Turnover', 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 6, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (336, 36, 'OperationSumDebit', N'OperationSum', 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 7, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (337, 36, 'OperationSumCredit', N'OperationSum', 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 8, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (338, 36, 'CorrectionsDebit', N'Corrections', 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 9, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (339, 36, 'CorrectionsCredit', N'Corrections', 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 10, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (340, 36, 'EndBalanceDebit', N'EndBalance', 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 11, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (341, 36, 'EndBalanceCredit', N'EndBalance', 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 12, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (342, 36, 'BranchName', NULL, NULL, 'System.String', 'nvarchar', 'string', 128, 0, 0, 0, 1, 1, N'AlwaysVisible', 13, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (343, 36, 'VoucherReference', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 1, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (344, 37, 'RowNo', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 1, 0, 0, N'AlwaysVisible', 0, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (345, 37, 'CurrencyName', NULL, NULL, 'System.String', 'nvarchar', 'string', 128, 0, 0, 0, 1, 1, NULL, 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (346, 37, 'Debit', NULL, 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 2, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (347, 37, 'Credit', NULL, 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 3, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (348, 37, 'Balance', NULL, 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 4, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (349, 38, 'RowNo', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 1, 0, 0, N'AlwaysVisible', 0, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (350, 38, 'VoucherDate', NULL, NULL, 'System.Date', 'datetime', 'Date', 0, 0, 0, 0, 1, 1, NULL, 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (351, 38, 'VoucherNo', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 1, 1, NULL, 2, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (352, 38, 'Description', NULL, NULL, 'System.String', 'nvarchar', 'string', 512, 0, 0, 0, 1, 1, NULL, 3, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (353, 38, 'Reference', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 0, 1, 1, NULL, 4, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (354, 38, 'BaseCurrencyDebit', NULL, 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 5, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (355, 38, 'BaseCurrencyCredit', NULL, 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 6, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (356, 38, 'BaseCurrencyBalance', NULL, 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 7, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (357, 38, 'Debit', NULL, 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 8, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (358, 38, 'Credit', NULL, 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 9, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (359, 38, 'Balance', NULL, 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 10, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (360, 38, 'CurrencyRate', NULL, 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 11, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (361, 38, 'Mark', NULL, NULL, 'System.String', 'nvarchar', 'string', 128, 0, 0, 0, 1, 1, NULL, 12, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (362, 38, 'BranchName', NULL, NULL, 'System.String', 'nvarchar', 'string', 128, 0, 0, 0, 1, 1, N'AlwaysVisible', 13, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (363, 39, 'RowNo', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 1, 0, 0, N'AlwaysVisible', 0, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (364, 39, 'VoucherDate', NULL, NULL, 'System.Date', 'datetime', 'Date', 0, 0, 0, 0, 1, 1, NULL, 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (365, 39, 'VoucherNo', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 1, 1, NULL, 2, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (366, 39, 'Description', NULL, NULL, 'System.String', 'nvarchar', 'string', 512, 0, 0, 0, 1, 1, NULL, 3, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (367, 39, 'Reference', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 0, 1, 1, NULL, 4, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (368, 39, 'BaseCurrencyDebit', NULL, 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 5, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (369, 39, 'BaseCurrencyCredit', NULL, 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 6, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (370, 39, 'BaseCurrencyBalance', NULL, 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 7, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (371, 39, 'Debit', NULL, 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 8, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (372, 39, 'Credit', NULL, 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 9, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (373, 39, 'Balance', NULL, 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 10, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (374, 39, 'BranchName', NULL, NULL, 'System.String', 'nvarchar', 'string', 128, 0, 0, 0, 1, 1, N'AlwaysVisible', 11, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (375, 40, 'RowNo', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 1, 0, 0, N'AlwaysVisible', 0, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (376, 40, 'LineCount', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 1, 1, NULL, 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (377, 40, 'VoucherDate', NULL, NULL, 'System.Date', 'datetime', 'Date', 0, 0, 0, 0, 1, 1, NULL, 2, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (378, 40, 'Description', NULL, NULL, 'System.String', 'nvarchar', 'string', 512, 0, 0, 0, 1, 1, NULL, 3, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (379, 40, 'BaseCurrencyDebit', NULL, 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 4, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (380, 40, 'BaseCurrencyCredit', NULL, 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 5, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (381, 40, 'BaseCurrencyBalance', NULL, 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 6, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (382, 40, 'Debit', NULL, 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 7, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (383, 40, 'Credit', NULL, 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 8, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (384, 40, 'Balance', NULL, 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 9, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (385, 40, 'BranchName', NULL, NULL, 'System.String', 'nvarchar', 'string', 128, 0, 0, 0, 1, 1, N'AlwaysVisible', 10, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (386, 41, 'RowNo', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 1, 0, 0, N'AlwaysVisible', 0, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (387, 41, 'Number', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 1, 1, N'AlwaysVisible', 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (388, 42, 'Id', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (389, 42, 'BranchId', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (390, 42, 'RowNo', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 1, 0, 0, N'AlwaysVisible', 0, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (391, 42, 'AccountFullCode', NULL, NULL, 'System.String', 'nvarchar', 'string', 256, 0, 0, 0, 1, 1, NULL, 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (392, 42, 'AccountName', NULL, NULL, 'System.String', 'nvarchar', 'string', 128, 0, 0, 0, 1, 1, NULL, 2, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (393, 42, 'VoucherDate', NULL, NULL, 'System.Date', 'datetime', 'Date', 0, 0, 0, 0, 1, 1, NULL, 3, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (394, 42, 'VoucherNo', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 1, 1, N'AlwaysVisible', 4, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (395, 42, 'VoucherReference', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 0, 1, 1, NULL, 5, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (396, 42, 'Debit', NULL, 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 6, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (397, 42, 'Credit', NULL, 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 7, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (398, 42, 'Description', NULL, NULL, 'System.String', 'nvarchar', 'string', 512, 0, 0, 1, 1, 1, NULL, 8, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (399, 42, 'DetailAccountFullCode', NULL, NULL, 'System.String', 'nvarchar', 'string', 128, 0, 0, 0, 1, 1, N'Hidden', 9, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (400, 42, 'DetailAccountName', NULL, NULL, 'System.String', 'nvarchar', 'string', 128, 0, 0, 0, 1, 1, N'Hidden', 10, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (401, 42, 'CostCenterFullCode', NULL, NULL, 'System.String', 'nvarchar', 'string', 128, 0, 0, 0, 1, 1, N'Hidden', 11, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (402, 42, 'CostCenterName', NULL, NULL, 'System.String', 'nvarchar', 'string', 128, 0, 0, 0, 1, 1, N'Hidden', 12, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (403, 42, 'ProjectFullCode', NULL, NULL, 'System.String', 'nvarchar', 'string', 128, 0, 0, 0, 1, 1, N'Hidden', 13, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (404, 42, 'ProjectName', NULL, NULL, 'System.String', 'nvarchar', 'string', 128, 0, 0, 0, 1, 1, N'Hidden', 14, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (405, 42, 'CurrencyName', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 0, 1, 1, N'Hidden', 15, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (406, 42, 'CurrencyValue', NULL, 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 1, 1, 1, N'Hidden', 16, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (407, 43, 'RowNo', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 1, 0, 0, N'AlwaysVisible', 0, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (408, 43, 'DetailAccountFullCode', NULL, NULL, 'System.String', 'nvarchar', 'string', 512, 0, 0, 0, 1, 1, NULL, 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (409, 43, 'DetailAccountName', NULL, NULL, 'System.String', 'nvarchar', 'string', 512, 0, 0, 0, 1, 1, NULL, 2, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (410, 43, 'EndBalanceDebit', N'EndBalance', 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 3, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (411, 43, 'EndBalanceCredit', N'EndBalance', 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 4, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (412, 43, 'BranchName', NULL, NULL, 'System.String', 'nvarchar', 'string', 128, 0, 0, 0, 1, 1, N'AlwaysVisible', 5, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (413, 43, 'VoucherReference', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 1, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (414, 44, 'RowNo', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 1, 0, 0, N'AlwaysVisible', 0, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (415, 44, 'DetailAccountFullCode', NULL, NULL, 'System.String', 'nvarchar', 'string', 512, 0, 0, 0, 1, 1, NULL, 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (416, 44, 'DetailAccountName', NULL, NULL, 'System.String', 'nvarchar', 'string', 512, 0, 0, 0, 1, 1, NULL, 2, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (417, 44, 'TurnoverDebit', N'Turnover', 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 3, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (418, 44, 'TurnoverCredit', N'Turnover', 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 4, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (419, 44, 'EndBalanceDebit', N'EndBalance', 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 5, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (420, 44, 'EndBalanceCredit', N'EndBalance', 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 6, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (421, 44, 'BranchName', NULL, NULL, 'System.String', 'nvarchar', 'string', 128, 0, 0, 0, 1, 1, N'AlwaysVisible', 7, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (422, 44, 'VoucherReference', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 1, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (423, 45, 'RowNo', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 1, 0, 0, N'AlwaysVisible', 0, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (424, 45, 'DetailAccountFullCode', NULL, NULL, 'System.String', 'nvarchar', 'string', 512, 0, 0, 0, 1, 1, NULL, 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (425, 45, 'DetailAccountName', NULL, NULL, 'System.String', 'nvarchar', 'string', 512, 0, 0, 0, 1, 1, NULL, 2, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (426, 45, 'StartBalanceDebit', N'StartBalance', 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 3, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (427, 45, 'StartBalanceCredit', N'StartBalance', 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 4, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (428, 45, 'TurnoverDebit', N'Turnover', 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 5, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (429, 45, 'TurnoverCredit', N'Turnover', 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 6, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (430, 45, 'EndBalanceDebit', N'EndBalance', 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 7, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (431, 45, 'EndBalanceCredit', N'EndBalance', 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 8, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (432, 45, 'BranchName', NULL, NULL, 'System.String', 'nvarchar', 'string', 128, 0, 0, 0, 1, 1, N'AlwaysVisible', 9, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (433, 45, 'VoucherReference', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 1, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (434, 46, 'RowNo', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 1, 0, 0, N'AlwaysVisible', 0, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (435, 46, 'DetailAccountFullCode', NULL, NULL, 'System.String', 'nvarchar', 'string', 512, 0, 0, 0, 1, 1, NULL, 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (436, 46, 'DetailAccountName', NULL, NULL, 'System.String', 'nvarchar', 'string', 512, 0, 0, 0, 1, 1, NULL, 2, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (437, 46, 'StartBalanceDebit', N'StartBalance', 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 3, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (438, 46, 'StartBalanceCredit', N'StartBalance', 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 4, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (439, 46, 'TurnoverDebit', N'Turnover', 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 5, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (440, 46, 'TurnoverCredit', N'Turnover', 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 6, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (441, 46, 'OperationSumDebit', N'OperationSum', 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 7, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (442, 46, 'OperationSumCredit', N'OperationSum', 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 8, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (443, 46, 'EndBalanceDebit', N'EndBalance', 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 9, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (444, 46, 'EndBalanceCredit', N'EndBalance', 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 10, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (445, 46, 'BranchName', NULL, NULL, 'System.String', 'nvarchar', 'string', 128, 0, 0, 0, 1, 1, N'AlwaysVisible', 11, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (446, 46, 'VoucherReference', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 1, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (447, 47, 'RowNo', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 1, 0, 0, N'AlwaysVisible', 0, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (448, 47, 'DetailAccountFullCode', NULL, NULL, 'System.String', 'nvarchar', 'string', 512, 0, 0, 0, 1, 1, NULL, 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (449, 47, 'DetailAccountName', NULL, NULL, 'System.String', 'nvarchar', 'string', 512, 0, 0, 0, 1, 1, NULL, 2, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (450, 47, 'StartBalanceDebit', N'StartBalance', 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 3, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (451, 47, 'StartBalanceCredit', N'StartBalance', 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 4, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (452, 47, 'TurnoverDebit', N'Turnover', 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 5, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (453, 47, 'TurnoverCredit', N'Turnover', 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 6, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (454, 47, 'OperationSumDebit', N'OperationSum', 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 7, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (455, 47, 'OperationSumCredit', N'OperationSum', 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 8, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (456, 47, 'CorrectionsDebit', N'Corrections', 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 9, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (457, 47, 'CorrectionsCredit', N'Corrections', 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 10, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (458, 47, 'EndBalanceDebit', N'EndBalance', 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 11, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (459, 47, 'EndBalanceCredit', N'EndBalance', 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 12, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (460, 47, 'BranchName', NULL, NULL, 'System.String', 'nvarchar', 'string', 128, 0, 0, 0, 1, 1, N'AlwaysVisible', 13, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (461, 47, 'VoucherReference', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 1, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (462, 48, 'RowNo', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 1, 0, 0, N'AlwaysVisible', 0, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (463, 48, 'CostCenterFullCode', NULL, NULL, 'System.String', 'nvarchar', 'string', 512, 0, 0, 0, 1, 1, NULL, 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (464, 48, 'CostCenterName', NULL, NULL, 'System.String', 'nvarchar', 'string', 512, 0, 0, 0, 1, 1, NULL, 2, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (465, 48, 'EndBalanceDebit', N'EndBalance', 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 3, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (466, 48, 'EndBalanceCredit', N'EndBalance', 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 4, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (467, 48, 'BranchName', NULL, NULL, 'System.String', 'nvarchar', 'string', 128, 0, 0, 0, 1, 1, N'AlwaysVisible', 5, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (468, 48, 'VoucherReference', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 1, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (469, 49, 'RowNo', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 1, 0, 0, N'AlwaysVisible', 0, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (470, 49, 'CostCenterFullCode', NULL, NULL, 'System.String', 'nvarchar', 'string', 512, 0, 0, 0, 1, 1, NULL, 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (471, 49, 'CostCenterName', NULL, NULL, 'System.String', 'nvarchar', 'string', 512, 0, 0, 0, 1, 1, NULL, 2, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (472, 49, 'TurnoverDebit', N'Turnover', 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 3, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (473, 49, 'TurnoverCredit', N'Turnover', 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 4, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (474, 49, 'EndBalanceDebit', N'EndBalance', 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 5, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (475, 49, 'EndBalanceCredit', N'EndBalance', 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 6, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (476, 49, 'BranchName', NULL, NULL, 'System.String', 'nvarchar', 'string', 128, 0, 0, 0, 1, 1, N'AlwaysVisible', 7, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (477, 49, 'VoucherReference', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 1, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (478, 50, 'RowNo', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 1, 0, 0, N'AlwaysVisible', 0, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (479, 50, 'CostCenterFullCode', NULL, NULL, 'System.String', 'nvarchar', 'string', 512, 0, 0, 0, 1, 1, NULL, 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (480, 50, 'CostCenterName', NULL, NULL, 'System.String', 'nvarchar', 'string', 512, 0, 0, 0, 1, 1, NULL, 2, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (481, 50, 'StartBalanceDebit', N'StartBalance', 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 3, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (482, 50, 'StartBalanceCredit', N'StartBalance', 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 4, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (483, 50, 'TurnoverDebit', N'Turnover', 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 5, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (484, 50, 'TurnoverCredit', N'Turnover', 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 6, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (485, 50, 'EndBalanceDebit', N'EndBalance', 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 7, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (486, 50, 'EndBalanceCredit', N'EndBalance', 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 8, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (487, 50, 'BranchName', NULL, NULL, 'System.String', 'nvarchar', 'string', 128, 0, 0, 0, 1, 1, N'AlwaysVisible', 9, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (488, 50, 'VoucherReference', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 1, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (489, 51, 'RowNo', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 1, 0, 0, N'AlwaysVisible', 0, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (490, 51, 'CostCenterFullCode', NULL, NULL, 'System.String', 'nvarchar', 'string', 512, 0, 0, 0, 1, 1, NULL, 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (491, 51, 'CostCenterName', NULL, NULL, 'System.String', 'nvarchar', 'string', 512, 0, 0, 0, 1, 1, NULL, 2, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (492, 51, 'StartBalanceDebit', N'StartBalance', 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 3, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (493, 51, 'StartBalanceCredit', N'StartBalance', 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 4, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (494, 51, 'TurnoverDebit', N'Turnover', 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 5, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (495, 51, 'TurnoverCredit', N'Turnover', 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 6, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (496, 51, 'OperationSumDebit', N'OperationSum', 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 7, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (497, 51, 'OperationSumCredit', N'OperationSum', 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 8, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (498, 51, 'EndBalanceDebit', N'EndBalance', 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 9, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (499, 51, 'EndBalanceCredit', N'EndBalance', 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 10, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (500, 51, 'BranchName', NULL, NULL, 'System.String', 'nvarchar', 'string', 128, 0, 0, 0, 1, 1, N'AlwaysVisible', 11, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (501, 51, 'VoucherReference', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 1, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (502, 52, 'RowNo', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 1, 0, 0, N'AlwaysVisible', 0, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (503, 52, 'CostCenterFullCode', NULL, NULL, 'System.String', 'nvarchar', 'string', 512, 0, 0, 0, 1, 1, NULL, 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (504, 52, 'CostCenterName', NULL, NULL, 'System.String', 'nvarchar', 'string', 512, 0, 0, 0, 1, 1, NULL, 2, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (505, 52, 'StartBalanceDebit', N'StartBalance', 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 3, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (506, 52, 'StartBalanceCredit', N'StartBalance', 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 4, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (507, 52, 'TurnoverDebit', N'Turnover', 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 5, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (508, 52, 'TurnoverCredit', N'Turnover', 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 6, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (509, 52, 'OperationSumDebit', N'OperationSum', 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 7, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (510, 52, 'OperationSumCredit', N'OperationSum', 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 8, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (511, 52, 'CorrectionsDebit', N'Corrections', 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 9, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (512, 52, 'CorrectionsCredit', N'Corrections', 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 10, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (513, 52, 'EndBalanceDebit', N'EndBalance', 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 11, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (514, 52, 'EndBalanceCredit', N'EndBalance', 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 12, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (515, 52, 'BranchName', NULL, NULL, 'System.String', 'nvarchar', 'string', 128, 0, 0, 0, 1, 1, N'AlwaysVisible', 13, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (516, 52, 'VoucherReference', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 1, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (517, 53, 'RowNo', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 1, 0, 0, N'AlwaysVisible', 0, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (518, 53, 'ProjectFullCode', NULL, NULL, 'System.String', 'nvarchar', 'string', 512, 0, 0, 0, 1, 1, NULL, 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (519, 53, 'ProjectName', NULL, NULL, 'System.String', 'nvarchar', 'string', 512, 0, 0, 0, 1, 1, NULL, 2, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (520, 53, 'EndBalanceDebit', N'EndBalance', 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 3, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (521, 53, 'EndBalanceCredit', N'EndBalance', 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 4, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (522, 53, 'BranchName', NULL, NULL, 'System.String', 'nvarchar', 'string', 128, 0, 0, 0, 1, 1, N'AlwaysVisible', 5, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (523, 53, 'VoucherReference', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 1, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (524, 54, 'RowNo', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 1, 0, 0, N'AlwaysVisible', 0, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (525, 54, 'ProjectFullCode', NULL, NULL, 'System.String', 'nvarchar', 'string', 512, 0, 0, 0, 1, 1, NULL, 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (526, 54, 'ProjectName', NULL, NULL, 'System.String', 'nvarchar', 'string', 512, 0, 0, 0, 1, 1, NULL, 2, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (527, 54, 'TurnoverDebit', N'Turnover', 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 3, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (528, 54, 'TurnoverCredit', N'Turnover', 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 4, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (529, 54, 'EndBalanceDebit', N'EndBalance', 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 5, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (530, 54, 'EndBalanceCredit', N'EndBalance', 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 6, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (531, 54, 'BranchName', NULL, NULL, 'System.String', 'nvarchar', 'string', 128, 0, 0, 0, 1, 1, N'AlwaysVisible', 7, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (532, 54, 'VoucherReference', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 1, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (533, 55, 'RowNo', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 1, 0, 0, N'AlwaysVisible', 0, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (534, 55, 'ProjectFullCode', NULL, NULL, 'System.String', 'nvarchar', 'string', 512, 0, 0, 0, 1, 1, NULL, 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (535, 55, 'ProjectName', NULL, NULL, 'System.String', 'nvarchar', 'string', 512, 0, 0, 0, 1, 1, NULL, 2, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (536, 55, 'StartBalanceDebit', N'StartBalance', 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 3, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (537, 55, 'StartBalanceCredit', N'StartBalance', 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 4, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (538, 55, 'TurnoverDebit', N'Turnover', 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 5, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (539, 55, 'TurnoverCredit', N'Turnover', 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 6, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (540, 55, 'EndBalanceDebit', N'EndBalance', 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 7, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (541, 55, 'EndBalanceCredit', N'EndBalance', 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 8, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (542, 55, 'BranchName', NULL, NULL, 'System.String', 'nvarchar', 'string', 128, 0, 0, 0, 1, 1, N'AlwaysVisible', 9, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (543, 55, 'VoucherReference', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 1, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (544, 56, 'RowNo', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 1, 0, 0, N'AlwaysVisible', 0, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (545, 56, 'ProjectFullCode', NULL, NULL, 'System.String', 'nvarchar', 'string', 512, 0, 0, 0, 1, 1, NULL, 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (546, 56, 'ProjectName', NULL, NULL, 'System.String', 'nvarchar', 'string', 512, 0, 0, 0, 1, 1, NULL, 2, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (547, 56, 'StartBalanceDebit', N'StartBalance', 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 3, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (548, 56, 'StartBalanceCredit', N'StartBalance', 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 4, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (549, 56, 'TurnoverDebit', N'Turnover', 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 5, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (550, 56, 'TurnoverCredit', N'Turnover', 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 6, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (551, 56, 'OperationSumDebit', N'OperationSum', 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 7, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (552, 56, 'OperationSumCredit', N'OperationSum', 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 8, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (553, 56, 'EndBalanceDebit', N'EndBalance', 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 9, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (554, 56, 'EndBalanceCredit', N'EndBalance', 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 10, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (555, 56, 'BranchName', NULL, NULL, 'System.String', 'nvarchar', 'string', 128, 0, 0, 0, 1, 1, N'AlwaysVisible', 11, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (556, 56, 'VoucherReference', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 1, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (557, 57, 'RowNo', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 1, 0, 0, N'AlwaysVisible', 0, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (558, 57, 'ProjectFullCode', NULL, NULL, 'System.String', 'nvarchar', 'string', 512, 0, 0, 0, 1, 1, NULL, 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (559, 57, 'ProjectName', NULL, NULL, 'System.String', 'nvarchar', 'string', 512, 0, 0, 0, 1, 1, NULL, 2, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (560, 57, 'StartBalanceDebit', N'StartBalance', 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 3, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (561, 57, 'StartBalanceCredit', N'StartBalance', 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 4, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (562, 57, 'TurnoverDebit', N'Turnover', 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 5, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (563, 57, 'TurnoverCredit', N'Turnover', 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 6, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (564, 57, 'OperationSumDebit', N'OperationSum', 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 7, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (565, 57, 'OperationSumCredit', N'OperationSum', 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 8, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (566, 57, 'CorrectionsDebit', N'Corrections', 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 9, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (567, 57, 'CorrectionsCredit', N'Corrections', 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 10, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (568, 57, 'EndBalanceDebit', N'EndBalance', 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 11, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (569, 57, 'EndBalanceCredit', N'EndBalance', 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 12, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (570, 57, 'BranchName', NULL, NULL, 'System.String', 'nvarchar', 'string', 128, 0, 0, 0, 1, 1, N'AlwaysVisible', 13, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (571, 57, 'VoucherReference', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 1, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (572, 58, 'RowNo', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 1, 0, 0, N'AlwaysVisible', 0, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (573, 58, 'AccountFullCode', NULL, NULL, 'System.String', 'nvarchar', 'string', 512, 0, 0, 0, 1, 1, N'AlwaysVisible', 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (574, 58, 'AccountName', NULL, NULL, 'System.String', 'nvarchar', 'string', 512, 0, 0, 0, 1, 1, N'AlwaysVisible', 2, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (575, 58, 'DetailAccountFullCode', NULL, NULL, 'System.String', 'nvarchar', 'string', 512, 0, 0, 0, 1, 1, N'AlwaysVisible', 3, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (576, 58, 'DetailAccountName', NULL, NULL, 'System.String', 'nvarchar', 'string', 512, 0, 0, 0, 1, 1, N'AlwaysVisible', 4, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (577, 58, 'CostCenterFullCode', NULL, NULL, 'System.String', 'nvarchar', 'string', 512, 0, 0, 0, 1, 1, N'AlwaysVisible', 5, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (578, 58, 'CostCenterName', NULL, NULL, 'System.String', 'nvarchar', 'string', 512, 0, 0, 0, 1, 1, N'AlwaysVisible', 6, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (579, 58, 'ProjectFullCode', NULL, NULL, 'System.String', 'nvarchar', 'string', 512, 0, 0, 0, 1, 1, N'AlwaysVisible', 7, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (580, 58, 'ProjectName', NULL, NULL, 'System.String', 'nvarchar', 'string', 512, 0, 0, 0, 1, 1, N'AlwaysVisible', 8, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (581, 58, 'AccountDescription', NULL, NULL, 'System.String', 'nvarchar', 'string', 512, 0, 0, 0, 1, 1, NULL, 9, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (582, 58, 'StartBalance', NULL, 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 10, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (583, 58, 'Debit', NULL, 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 11, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (584, 58, 'Credit', NULL, 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 12, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (585, 58, 'EndBalance', NULL, 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 13, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (586, 58, 'BranchName', NULL, NULL, 'System.String', 'nvarchar', 'string', 128, 0, 0, 0, 1, 1, N'AlwaysVisible', 14, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (587, 13, 'EntityReference', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 0, 1, 1, NULL, 12, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (588, 13, 'EntityAssociation', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 0, 1, 1, NULL, 13, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (589, 13, 'SourceName', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 0, 1, 1, NULL, 14, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (590, 13, 'SourceListName', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 0, 1, 1, NULL, 15, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (591, 13, 'OperationName', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 0, 1, 1, NULL, 16, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (592, 13, 'Description', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 0, 1, 1, NULL, 17, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (593, 13, 'CompanyName', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 0, 1, 1, NULL, 18, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (594, 59, 'Id', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 1, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (595, 59, 'RowNo', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 0, 0, N'AlwaysVisible', 0, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (596, 59, 'UserName', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 0, 1, 1, NULL, 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (597, 59, 'Date', NULL, NULL, 'System.Date', 'datetime', 'Date', 0, 0, 0, 0, 1, 1, N'AlwaysVisible', 2, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (598, 59, 'Time', NULL, NULL, 'System.TimeSpan', 'time', 'Date', 7, 0, 0, 0, 1, 1, N'AlwaysVisible', 3, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (599, 59, 'EntityTypeName', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 0, 1, 1, NULL, 4, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (600, 59, 'EntityCode', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 0, 1, 1, NULL, 5, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (601, 59, 'EntityName', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 0, 1, 1, NULL, 6, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (602, 59, 'EntityDescription', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 0, 1, 1, NULL, 7, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (603, 59, 'EntityNo', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 1, 1, NULL, 8, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (604, 59, 'EntityDate', NULL, NULL, 'System.DateTime', 'datetime', 'Date', 0, 0, 0, 0, 1, 1, NULL, 9, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (605, 59, 'SourceName', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 0, 1, 1, NULL, 10, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (606, 59, 'SourceListName', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 0, 1, 1, NULL, 11, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (607, 59, 'OperationName', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 0, 1, 1, NULL, 12, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (608, 59, 'Description', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 0, 1, 1, NULL, 13, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (609, 59, 'CompanyName', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 0, 1, 1, NULL, 14, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (610, 60, 'Id', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 1, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (611, 60, 'RowNo', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 0, 0, N'AlwaysVisible', 0, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (612, 60, 'UserName', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 0, 1, 1, NULL, 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (613, 60, 'Date', NULL, NULL, 'System.Date', 'datetime', 'Date', 0, 0, 0, 0, 1, 1, N'AlwaysVisible', 2, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (614, 60, 'Time', NULL, NULL, 'System.TimeSpan', 'time', 'Date', 7, 0, 0, 0, 1, 1, N'AlwaysVisible', 3, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (615, 60, 'EntityTypeName', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 0, 1, 1, NULL, 4, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (616, 60, 'EntityCode', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 0, 1, 1, NULL, 5, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (617, 60, 'EntityName', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 0, 1, 1, NULL, 6, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (618, 60, 'EntityDescription', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 0, 1, 1, NULL, 7, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (619, 60, 'EntityNo', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 1, 1, NULL, 8, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (620, 60, 'EntityDate', NULL, NULL, 'System.DateTime', 'datetime', 'Date', 0, 0, 0, 0, 1, 1, NULL, 9, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (621, 60, 'SourceName', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 0, 1, 1, NULL, 10, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (622, 60, 'SourceListName', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 0, 1, 1, NULL, 11, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (623, 60, 'OperationName', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 0, 1, 1, NULL, 12, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (624, 60, 'Description', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 0, 1, 1, NULL, 13, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (625, 60, 'CompanyName', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 0, 1, 1, NULL, 14, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (626, 61, 'Id', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 1, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (627, 61, 'RowNo', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 0, 0, N'AlwaysVisible', 0, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (628, 61, 'UserName', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 0, 1, 1, NULL, 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (629, 61, 'BranchName', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 0, 1, 1, NULL, 2, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (630, 61, 'FiscalPeriodName', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 0, 1, 1, NULL, 3, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (631, 61, 'Date', NULL, NULL, 'System.Date', 'datetime', 'Date', 0, 0, 0, 0, 1, 1, N'AlwaysVisible', 4, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (632, 61, 'Time', NULL, NULL, 'System.TimeSpan', 'time', 'Date', 7, 0, 0, 0, 1, 1, N'AlwaysVisible', 5, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (633, 61, 'EntityTypeName', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 0, 1, 1, NULL, 6, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (634, 61, 'EntityCode', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 0, 1, 1, NULL, 7, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (635, 61, 'EntityName', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 0, 1, 1, NULL, 8, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (636, 61, 'EntityDescription', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 0, 1, 1, NULL, 9, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (637, 61, 'EntityNo', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 1, 1, NULL, 10, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (638, 61, 'EntityDate', NULL, NULL, 'System.DateTime', 'datetime', 'Date', 0, 0, 0, 0, 1, 1, NULL, 11, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (639, 61, 'EntityReference', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 0, 1, 1, NULL, 12, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (640, 61, 'EntityAssociation', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 0, 1, 1, NULL, 13, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (641, 61, 'SourceName', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 0, 1, 1, NULL, 14, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (642, 61, 'SourceListName', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 0, 1, 1, NULL, 15, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (643, 61, 'OperationName', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 0, 1, 1, NULL, 16, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (644, 61, 'Description', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 0, 1, 1, NULL, 17, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (645, 61, 'CompanyName', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 0, 1, 1, NULL, 18, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (646, 4, 'Password', NULL, NULL, 'System.String', 'nvarchar', 'string', 16, 0, 0, 0, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (647, 2, 'IsBalanced', NULL, NULL, 'System.Boolean', 'bit', 'boolean', 0, 0, 0, 1, 1, 1, NULL, 8, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (648, 2, 'ConfirmerName', NULL, NULL, 'System.String', 'nvarchar', 'string', 120, 0, 0, 1, 1, 1, NULL, 9, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (649, 2, 'ApproverName', NULL, NULL, 'System.String', 'nvarchar', 'string', 120, 0, 0, 1, 1, 1, NULL, 10, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (650, 62, 'Group', NULL, NULL, 'System.String', 'nvarchar', 'string', 0, 0, 0, 1, 0, 0, 'AlwaysVisible', 0, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (651, 62, 'Account', NULL, NULL, 'System.String', 'nvarchar', 'string', 0, 0, 0, 1, 0, 0, NULL, 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (652, 62, 'StartBalance', NULL, 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 1, 0, 0, NULL, 2, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (653, 62, 'PeriodTurnover', NULL, 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 1, 0, 0, NULL, 3, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (654, 62, 'EndBalance', NULL, 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 1, 0, 0, NULL, 4, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (655, 62, 'Balance', NULL, 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 1, 0, 0, 'Hidden', 5, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (656, 2, 'IsConfirmed', NULL, NULL, 'System.Boolean', 'bit', 'boolean', 0, 0, 0, 1, 1, 1, NULL, 11, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (657, 2, 'IsApproved', NULL, NULL, 'System.Boolean', 'bit', 'boolean', 0, 0, 0, 1, 1, 1, NULL, 12, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (658, 2, 'BranchName', NULL, NULL, 'System.String', 'nvarchar', 'string', 120, 0, 0, 1, 1, 1, NULL, 13, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (659, 2, 'IssuerName', NULL, NULL, 'System.String', 'nvarchar', 'string', 120, 0, 0, 1, 1, 1, NULL, 14, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (660, 63, 'Id', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 1, 1, 'AlwaysHidden', -1, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (661, 63, 'No', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 1, 1, 'AlwaysVisible', 0, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (662, 63, 'Date', NULL, NULL, 'System.DateTime', 'datetime', 'Date', 0, 0, 0, 0, 1, 1, 'AlwaysVisible', 1, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (663, 63, 'Name', NULL, NULL, 'System.String', 'nvarchar', 'string', 128, 0, 0, 0, 1, 1, 'AlwaysVisible', 2, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (664, 63, 'FullCode', NULL, NULL, 'System.String', 'nvarchar', 'string', 128, 0, 0, 0, 1, 1, 'AlwaysVisible', 3, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (665, 63, 'ErrorMessage', NULL, NULL, 'System.String', 'nvarchar', 'string', 128, 0, 0, 0, 1, 1, NULL, 4, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (666, 64, 'Group', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 1, 0, 0, 'AlwaysVisible', 0, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (667, 64, 'Account', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 1, 0, 0, NULL, 1, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (668, 64, 'Balance', NULL, 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 1, 0, 0, NULL, 2, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (669, 2, 'OriginName', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 1, 1, 1, NULL, 15, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [IsDynamic], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (670, 65, 'Group', 0, NULL, NULL, 'System.String', 'nvarchar', 'string', 0, 0, 0, 0, 0, 0, 'AlwaysVisible', 0, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [IsDynamic], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (671, 65, 'Account', 0, NULL, NULL, 'System.String', 'nvarchar', 'string', 0, 0, 0, 0, 0, 0, NULL, 1, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [IsDynamic], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (672, 65, 'StartBalanceItem', 1, NULL, 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 0, 0, NULL, 2, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [IsDynamic], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (673, 65, 'PeriodTurnoverItem', 1, NULL, 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 0, 0, NULL, 3, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [IsDynamic], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (674, 65, 'EndBalanceItem', 1, NULL, 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 0, 0, NULL, 4, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [IsDynamic], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (675, 66, 'Group', 0, NULL, NULL, 'System.String', 'nvarchar', 'string', 0, 0, 0, 0, 0, 0, 'AlwaysVisible', 0, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [IsDynamic], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (676, 66, 'Account', 0, NULL, NULL, 'System.String', 'nvarchar', 'string', 0, 0, 0, 0, 0, 0, NULL, 1, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [IsDynamic], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (677, 66, 'BalanceItem', 1, NULL, 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 0, 0, NULL, 2, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [IsDynamic], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (678, 2, 'TypeName', 0, NULL, NULL, 'System.String', 'nvarchar', 'string', 0, 0, 0, 1, 0, 0, NULL, 16, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (679, 67, 'Assets', NULL, NULL, 'System.String', 'nvarchar', 'string', 0, 0, 0, 1, 0, 0, 'AlwaysVisible', 0, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (680, 67, 'AssetsBalance', NULL, 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 1, 0, 0, NULL, 1, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (681, 67, 'AssetsPreviousBalance', NULL, 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 1, 0, 0, NULL, 2, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (682, 67, 'Liabilities', NULL, NULL, 'System.String', 'nvarchar', 'string', 0, 0, 0, 1, 0, 0, NULL, 3, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (683, 67, 'LiabilitiesBalance', NULL, 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 1, 0, 0, NULL, 4, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (684, 67, 'LiabilitiesPreviousBalance', NULL, 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 1, 0, 0, NULL, 5, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (685, 3, 'BranchName', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 0, 1, 1, 'Hidden', 14, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (686, 37, 'BranchName', NULL, NULL, 'System.String', 'nvarchar', 'string', 128, 0, 0, 0, 1, 1, N'AlwaysVisible', 5, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex])
    VALUES (687, 68, 'Id', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 1, 1, N'AlwaysHidden', -1)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex])
    VALUES (688, 68, 'TypeId', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 1, 1, N'AlwaysHidden', -1)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex])
    VALUES (689, 68, 'FunctionId', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 1, 1, N'AlwaysHidden', -1)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex])
    VALUES (690, 68, 'CreatedById', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 1, 1, N'AlwaysHidden', -1)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex])
    VALUES (691, 68, 'RowNo', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 0, 0, N'AlwaysVisible', 0)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex])
    VALUES (692, 68, 'Title', NULL, NULL, 'System.String', 'nvarchar', 'string', 128, 0, 0, 0, 1, 1, N'AlwaysVisible', 1)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex])
    VALUES (693, 68, 'TypeName', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 0, 1, 1, NULL, 2)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex])
    VALUES (694, 68, 'FunctionName', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 0, 1, 1, NULL, 3)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex])
    VALUES (695, 68, 'CreatedByFullName', NULL, NULL, 'System.String', 'nvarchar', 'string', 128, 0, 0, 0, 1, 1, NULL, 4)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex])
    VALUES (696, 68, 'Description', NULL, NULL, 'System.String', 'nvarchar', 'string', 512, 0, 0, 1, 1, 1, NULL, 5)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (697, 69, 'RowNo', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 0, 0, 0, N'AlwaysVisible', 0, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (698, 69, 'SerialNo', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 0, 0, 1, 1, NULL, 1, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (699, 69, 'StatusName', NULL, NULL, 'System.String', 'nvarchar', 'string', 32, 0, 0, 0, 0, 1, 1, NULL, 2, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (700, 69, 'Id', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 0, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (701, 69, 'CheckBookPageID', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 0, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (702, 69, 'CheckBookID', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 0, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (703, 69, 'CheckID', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 0, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (704, 70, 'RowNo', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 0, 0, 0, N'AlwaysVisible', 0, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (705, 70, 'Name', NULL, NULL, 'System.String', 'nvarchar', 'string', 256, 0, 0, 0, 0, 1, 1, NULL, 1, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (706, 70, 'Description', NULL, NULL, 'System.String', 'nvarchar', 'string', 256, 0, 0, 0, 1, 1, 1, NULL, 2, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (707, 70, 'FiscalPeriodId', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 0, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (708, 70, 'BranchId', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 0, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (709, 70, 'Id', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 0, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (710, 70, 'BranchScope', NULL, NULL, 'System.Int16', 'smallint', 'number', 0, 0, 0, 0, 0, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (711, 71, 'RowNo', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 0, 0, 0, N'AlwaysVisible', 0, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (712, 71, 'Name', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 0, 0, 1, 1, NULL, 1, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (713, 71, 'TextNo', NULL, NULL, 'System.Int64', 'bigint', 'number', 0, 0, 0, 0, 0, 1, 1, NULL, 2, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (714, 71, 'BankName', NULL, NULL, 'System.String', 'nvarchar', 'string', 32, 0, 0, 0, 1, 1, 1, NULL, 3, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (715, 71, 'IssueDate', NULL, 'Default', 'System.DateTime', 'datetime', 'Date', 0, 0, 0, 0, 0, 1, 1, NULL, 4, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (716, 71, 'StartNo', NULL, NULL, 'System.String', 'nvarchar', 'string', 32, 0, 0, 0, 0, 1, 1, NULL, 5, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (717, 71, 'EndNo', NULL, NULL, 'System.String', 'nvarchar', 'string', 32, 0, 0, 0, 1, 1, 1, NULL, 6, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (718, 71, 'IsArchived', NULL, NULL, 'System.Boolean', 'bit', 'boolean', 0, 0, 0, 0, 1, 1, 1, NULL, 7, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (719, 71, 'PageCount', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 0, 1, 1, N'Hidden', 8, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (720, 71, 'FullAccount', NULL, NULL, 'System.Object', '(n/a)', 'object', 0, 0, 0, 0, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (721, 69, 'SayyadNo', NULL, NULL, 'System.String', 'nvarchar', 'string', 16, 0, 0, 0, 1, 1, NULL, 9, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (723, 71, 'BranchId', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 0, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (724, 71, 'Id', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 0, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (725, 72, 'RowNo', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 0, 0, 0, N'AlwaysVisible', 0, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (726, 72, 'TextNo', NULL, NULL, 'System.Int64', 'bigint', 'number', 0, 0, 0, 0, 0, 1, 1, NULL, 1, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (727, 72, 'Name', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 0, 0, 1, 1, NULL, 4, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (728, 72, 'BankName', NULL, NULL, 'System.String', 'nvarchar', 'string', 32, 0, 0, 0, 0, 1, 1, NULL, 5, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (729, 72, 'IssueDate', NULL, 'Default', 'System.DateTime', 'datetime', 'Date', 0, 0, 0, 0, 0, 1, 1, NULL, 6, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (730, 72, 'StartNo', NULL, NULL, 'System.String', 'nvarchar', 'string', 32, 0, 0, 0, 0, 1, 1, NULL, 7, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (731, 72, 'EndNo', NULL, NULL, 'System.String', 'nvarchar', 'string', 32, 0, 0, 0, 0, 1, 1, NULL, 8, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (732, 72, 'AccountCode', NULL, NULL, 'System.String', 'nvarchar', 'string', 0, 0, 0, 0, 0, 1, 1, NULL, 9, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (733, 72, 'AccountName', NULL, NULL, 'System.String', 'nvarchar', 'string', 0, 0, 0, 0, 0, 1, 1, NULL, 10, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (734, 72, 'DetailAccountName', NULL, NULL, 'System.String', 'nvarchar', 'string', 0, 0, 0, 0, 0, 1, 1, NULL, 11, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (735, 72, 'CostCenterName', NULL, NULL, 'System.String', 'nvarchar', 'string', 0, 0, 0, 0, 0, 1, 1, NULL, 12, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (736, 72, 'ProjectName', NULL, NULL, 'System.String', 'nvarchar', 'string', 0, 0, 0, 0, 0, 1, 1, NULL, 13, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (737, 72, 'IsArchivedName', NULL, NULL, 'System.String', 'nvarchar', 'string', 0, 0, 0, 0, 1, 1, 1, NULL, 14, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (738, 72, 'Id', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 0, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (739, 73, 'RowNo', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 0, 0, 0, N'AlwaysVisible', 0, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (740, 73, 'Code', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 0, 0, 1, 1, NULL, 1, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (741, 73, 'Name', NULL, NULL, 'System.String', 'nvarchar', 'string', 256, 0, 0, 0, 0, 1, 1, NULL, 2, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (742, 73, 'Type', NULL, NULL, 'System.Int16', 'smallint', 'number', 0, 0, 0, 0, 0, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (743, 73, 'Description', NULL, NULL, 'System.String', 'nvarchar', 'string', 512, 0, 0, 0, 1, 1, 1, NULL, 4, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (744, 73, 'FiscalPeriodId', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 0, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (745, 73, 'BranchId', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 0, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (746, 73, 'Id', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 0, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (747, 71, 'SeriesNo', NULL, NULL, 'System.String', 'nvarchar', 'string', 32, 0, 0, 0, 0, 1, 1, NULL, 9, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (748, 71, 'SayyadStartNo', NULL, NULL, 'System.String', 'nvarchar', 'string', 16, 0, 0, 0, 0, 1, 1, NULL, 10, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (749, 69, 'SayyadNo', NULL, NULL, 'System.String', 'nvarchar', 'string', 16, 0, 0, 0, 0, 1, 1, NULL, 3, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (750, 73, 'TypeName', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 0, 1, 1, 1, NULL, 3, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (751, 73, 'BranchScope', NULL, NULL, 'System.Int16', 'smallint', 'number', 0, 0, 0, 0, 0, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (752, 74, 'Date', NULL, 'Default', 'System.DateTime', 'datetime', 'Date', 0, 0, 0, 0, 0, 1, 1, NULL, 3, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (753, 74, 'IssuedByName', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 0, 0, 1, 1, NULL, 4, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (754, 74, 'ConfirmedByName', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 0, 1, 1, 1, NULL, 5, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (755, 74, 'ApprovedByName', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 0, 1, 1, 1, NULL, 6, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (756, 74, 'CurrencyRate', NULL, 'Currency', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, 1, NULL, 7, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (757, 74, 'Description', NULL, NULL, 'System.String', 'nvarchar', 'string', 1024, 0, 0, 0, 1, 1, 1, NULL, 8, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (758, 74, 'FiscalPeriodId', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 0, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (759, 74, 'BranchId', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 0, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (760, 74, 'Id', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 0, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (761, 74, 'CurrencyId', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 1, 1, 1, NULL, -1, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (762, 74, 'IsConfirmed', NULL, NULL, 'System.Boolean', 'bit', 'Boolean', 0, 0, 0, 0, 1, 1, 1, NULL, -1, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (763, 74, 'IsApproved', NULL, NULL, 'System.Boolean', 'bit', 'Boolean', 0, 0, 0, 0, 1, 1, 1, NULL, -1, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (764, 75, 'TextNo', NULL, NULL, 'System.Int64', 'bigint', 'number', 0, 0, 0, 0, 0, 1, 1, NULL, 1, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (765, 75, 'Reference', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 0, 1, 1, 1, NULL, 2, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (766, 75, 'Date', NULL, 'Default', 'System.DateTime', 'datetime', 'Date', 0, 0, 0, 0, 0, 1, 1, NULL, 3, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (767, 75, 'IssuedByName', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 0, 0, 1, 1, NULL, 4, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (768, 75, 'ConfirmedByName', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 0, 1, 1, 1, NULL, 5, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (769, 75, 'ApprovedByName', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 0, 1, 1, 1, NULL, 6, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (770, 75, 'CurrencyRate', NULL, 'Currency', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, 1, NULL, 7, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (771, 75, 'Description', NULL, NULL, 'System.String', 'nvarchar', 'string', 1024, 0, 0, 0, 1, 1, 1, NULL, 8, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (772, 75, 'FiscalPeriodId', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 0, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (773, 75, 'BranchId', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 0, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (774, 75, 'Id', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 0, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (775, 75, 'CurrencyId', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 1, 1, 1, NULL, -1, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (776, 75, 'IsConfirmed', NULL, NULL, 'System.Boolean', 'bit', 'Boolean', 0, 0, 0, 0, 1, 1, 1, NULL, -1, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (777, 75, 'IsApproved', NULL, NULL, 'System.Boolean', 'bit', 'Boolean', 0, 0, 0, 0, 1, 1, 1, NULL, -1, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (778, 74, 'TextNo', NULL, NULL, 'System.Int64', 'bigint', 'number', 0, 0, 0, 0, 0, 1, 1, NULL, 1, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (779, 74, 'Reference', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 0, 1, 1, 1, NULL, 2, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (780, 76, 'RowNo', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 0, 0, 0, N'AlwaysVisible', 0, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (781, 76, 'FullAccount', NULL, NULL, 'System.Object', '(n/a)', 'object', 0, 0, 0, 0, 0, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (782, 76, 'FullAccount.Account.FullCode', NULL, NULL, 'System.String', 'nvarchar', 'string', 256, 0, 0, 0, 0, 1, 1, NULL, 1, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (783, 76, 'FullAccount.Account.Name', NULL, NULL, 'System.String', 'nvarchar', 'string', 256, 0, 0, 0, 0, 1, 1, NULL, 2, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (784, 76, 'FullAccount.DetailAccount.FullCode', NULL, NULL, 'System.String', 'nvarchar', 'string', 256, 0, 0, 0, 0, 1, 1, N'Hidden', 3, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (785, 76, 'FullAccount.DetailAccount.Name', NULL, NULL, 'System.String', 'nvarchar', 'string', 256, 0, 0, 0, 0, 1, 1, N'Hidden', 4, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (786, 76, 'FullAccount.CostCenter.FullCode', NULL, NULL, 'System.String', 'nvarchar', 'string', 256, 0, 0, 0, 0, 1, 1, N'Hidden', 5, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (787, 76, 'FullAccount.CostCenter.Name', NULL, NULL, 'System.String', 'nvarchar', 'string', 256, 0, 0, 0, 0, 1, 1, N'Hidden', 6, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (788, 76, 'FullAccount.Project.FullCode', NULL, NULL, 'System.String', 'nvarchar', 'string', 256, 0, 0, 0, 0, 1, 1, N'Hidden', 7, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (789, 76, 'FullAccount.Project.Name', NULL, NULL, 'System.String', 'nvarchar', 'string', 256, 0, 0, 0, 0, 1, 1, N'Hidden', 8, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (792, 76, 'Amount', NULL, 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 0, 1, 1, NULL, 9, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (793, 76, 'Remarks', NULL, NULL, 'System.String', 'nvarchar', 'string', 512, 0, 0, 0, 1, 1, 1, NULL, 10, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (794, 76, 'PayReceiveId', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 0, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (795, 76, 'Id', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 0, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (796, 77, 'RowNo', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 0, 0, 0, N'AlwaysVisible', 0, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (797, 77, 'FullAccount', NULL, NULL, 'System.Object', 'int', 'Object', 0, 0, 0, 0, 0, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (798, 77, 'IsBank', NULL, NULL, 'System.Boolean', 'bit', 'boolean', 0, 0, 0, 0, 0, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (799, 77, 'PayReceiveId', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 0, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (800, 77, 'SourceAppId', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 1, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (801, 77, 'BankOrderNo', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 0, 1, 1, 1, NULL, 11, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (802, 77, 'Id', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 0, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (803, 77, 'AccountId', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (804, 77, 'DetailAccountId', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 1, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (805, 77, 'CostCenterId', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 1, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (806, 77, 'ProjectId', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 1, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (807, 77, 'FullAccount.Account.FullCode', NULL, NULL, 'System.String', 'nvarchar', 'string', 256, 0, 0, 0, 1, 1, NULL, 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (808, 77, 'FullAccount.Account.Name', NULL, NULL, 'System.String', 'nvarchar', 'string', 256, 0, 0, 0, 1, 1, NULL, 2, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (809, 77, 'FullAccount.DetailAccount.FullCode', NULL, NULL, 'System.String', 'nvarchar', 'string', 256, 0, 0, 0, 1, 1, N'Hidden', 3, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (810, 77, 'FullAccount.DetailAccount.Name', NULL, NULL, 'System.String', 'nvarchar', 'string', 256, 0, 0, 0, 1, 1, N'Hidden', 4, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (811, 77, 'FullAccount.CostCenter.FullCode', NULL, NULL, 'System.String', 'nvarchar', 'string', 256, 0, 0, 0, 1, 1, N'Hidden', 5, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (812, 77, 'FullAccount.CostCenter.Name', NULL, NULL, 'System.String', 'nvarchar', 'string', 256, 0, 0, 0, 1, 1, N'Hidden', 6, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (813, 77, 'FullAccount.Project.FullCode', NULL, NULL, 'System.String', 'nvarchar', 'string', 256, 0, 0, 0, 1, 1, N'Hidden', 7, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (814, 77, 'FullAccount.Project.Name', NULL, NULL, 'System.String', 'nvarchar', 'string', 256, 0, 0, 0, 1, 1, N'Hidden', 8, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (815, 77, 'Amount', NULL, 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 0, 1, 1, NULL, 9, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (816, 77, 'Remarks', NULL, NULL, 'System.String', 'nvarchar', 'string', 512, 0, 0, 0, 1, 1, 1, NULL, 12, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (817, 77, 'SourceAppName', NULL, NULL, 'System.String', 'nvarchar', 'string', 128, 0, 0, 0, 1, 1, 1, NULL, 10, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (818, 6, 'State', NULL, NULL, 'System.String', 'nvarchar', 'string', 32, 0, 0, 0, 1, 1, 1, NULL, 5, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (819, 7, 'State', NULL, NULL, 'System.String', 'nvarchar', 'string', 32, 0, 0, 0, 1, 1, 1, NULL, 5, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (820, 8, 'State', NULL, NULL, 'System.String', 'nvarchar', 'string', 32, 0, 0, 0, 1, 1, 1, NULL, 5, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (821, 70, 'State', NULL, NULL, 'System.String', 'nvarchar', 'string', 32, 0, 0, 0, 1, 1, 1, NULL, 3, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (822, 73, 'State', NULL, NULL, 'System.String', 'nvarchar', 'string', 32, 0, 0, 0, 1, 1, 1, NULL, 5, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (823, 70, 'CreatedByName', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 1, 1, 1, N'Hidden', 3, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (824, 70, 'CreatedDate', NULL, 'Default', 'System.DateTime', 'datetime', 'Date', 0, 0, 0, 0, 1, 1, 1, N'Hidden', 4, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (825, 70, 'ModifiedByName', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 1, 1, 1, N'Hidden', 5, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (826, 70, 'ModifiedDate', NULL, 'Default', 'System.DateTime', 'datetime', 'Date', 0, 0, 0, 0, 1, 1, 1, N'Hidden', 6, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (827, 73, 'CreatedByName', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 1, 1, 1, N'Hidden', 5, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (828, 73, 'CreatedDate', NULL, 'Default', 'System.DateTime', 'datetime', 'Date', 0, 0, 0, 0, 1, 1, 1, N'Hidden', 6, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (829, 73, 'ModifiedByName', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 1, 1, 1, N'Hidden', 7, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (830, 73, 'ModifiedDate', NULL, 'Default', 'System.DateTime', 'datetime', 'Date', 0, 0, 0, 0, 1, 1, 1, N'Hidden', 8, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (831, 30, 'CreatedByName', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 1, 1, 1, N'Hidden', 9, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (832, 30, 'CreatedDate', NULL, 'Default', 'System.DateTime', 'datetime', 'Date', 0, 0, 0, 0, 1, 1, 1, N'Hidden', 10, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (833, 30, 'ModifiedByName', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 1, 1, 1, N'Hidden', 11, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (834, 30, 'ModifiedDate', NULL, 'Default', 'System.DateTime', 'datetime', 'Date', 0, 0, 0, 0, 1, 1, 1, N'Hidden', 12, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (835, 31, 'CreatedByName', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 1, 1, 1, N'Hidden', 8, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (836, 31, 'CreatedDate', NULL, 'Default', 'System.DateTime', 'datetime', 'Date', 0, 0, 0, 0, 1, 1, 1, N'Hidden', 9, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (837, 31, 'ModifiedByName', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 1, 1, 1, N'Hidden', 10, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (838, 31, 'ModifiedDate', NULL, 'Default', 'System.DateTime', 'datetime', 'Date', 0, 0, 0, 0, 1, 1, 1, N'Hidden', 11, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (839, 1, 'CreatedByName', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 1, 1, 1, N'Hidden', 9, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (840, 1, 'CreatedDate', NULL, 'Default', 'System.DateTime', 'datetime', 'Date', 0, 0, 0, 0, 1, 1, 1, N'Hidden', 10, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (841, 1, 'ModifiedByName', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 1, 1, 1, N'Hidden', 11, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (842, 1, 'ModifiedDate', NULL, 'Default', 'System.DateTime', 'datetime', 'Date', 0, 0, 0, 0, 1, 1, 1, N'Hidden', 12, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (843, 7, 'CreatedByName', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 1, 1, 1, N'Hidden', 5, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (844, 7, 'CreatedDate', NULL, 'Default', 'System.DateTime', 'datetime', 'Date', 0, 0, 0, 0, 1, 1, 1, N'Hidden', 6, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (845, 7, 'ModifiedByName', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 1, 1, 1, N'Hidden', 7, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (846, 7, 'ModifiedDate', NULL, 'Default', 'System.DateTime', 'datetime', 'Date', 0, 0, 0, 0, 1, 1, 1, N'Hidden', 8, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (847, 6, 'CreatedByName', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 1, 1, 1, N'Hidden', 5, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (848, 6, 'CreatedDate', NULL, 'Default', 'System.DateTime', 'datetime', 'Date', 0, 0, 0, 0, 1, 1, 1, N'Hidden', 6, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (849, 6, 'ModifiedByName', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 1, 1, 1, N'Hidden', 7, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (850, 6, 'ModifiedDate', NULL, 'Default', 'System.DateTime', 'datetime', 'Date', 0, 0, 0, 0, 1, 1, 1, N'Hidden', 8, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (851, 8, 'CreatedByName', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 1, 1, 1, N'Hidden', 5, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (852, 8, 'CreatedDate', NULL, 'Default', 'System.DateTime', 'datetime', 'Date', 0, 0, 0, 0, 1, 1, 1, N'Hidden', 6, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (853, 8, 'ModifiedByName', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 1, 1, 1, N'Hidden', 7, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (854, 8, 'ModifiedDate', NULL, 'Default', 'System.DateTime', 'datetime', 'Date', 0, 0, 0, 0, 1, 1, 1, N'Hidden', 8, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (855, 3, 'SourceAppName', NULL, NULL, 'System.String', 'nvarchar', 'string', 128, 0, 0, 0, 1, 1, N'Hidden', 9, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (856, 3, 'SourceAppId', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 1, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (857, 78, 'RowNo', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 0, 0, 0, N'AlwaysVisible', 0, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (858, 78, 'Id', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 0, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (859, 78, 'No', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 1, 1, 1, NULL, 1, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (860, 78, 'Date', NULL, 'Default', 'System.DateTime', 'datetime', 'Date', 0, 0, 0, 0, 1, 1, 1, NULL, 2, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (861, 78, 'Description', NULL, NULL, 'System.String', 'nvarchar', 'string', 512, 0, 0, 1, 1, 1, NULL, 3, NULL)














































































































































































































































































































INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (100001, 100001, 'RowNo', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 0, 0, 0, N'AlwaysVisible', 0, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (100002, 100001, 'Name', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 0, 0, 1, 1, NULL, 1, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (100003, 100001, 'EnName', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 0, 0, 1, 1, NULL, 2, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (100004, 100001, 'Description', NULL, NULL, 'System.String', 'nvarchar', 'string', 1024, 0, 0, 0, 0, 1, 1, NULL, 3, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (100005, 100001, 'SocialLink', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 0, 0, 1, 1, NULL, 4, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (100006, 100001, 'Website', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 0, 0, 1, 1, NULL, 5, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (100007, 100001, 'MetaKeyword', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 0, 0, 1, 1, NULL, 6, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (100008, 100001, 'IsActive', NULL, NULL, 'System.Boolean', 'bit', 'boolean', 0, 0, 0, 0, 0, 1, 1, NULL, 7, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (100009, 100001, 'BranchId', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 0, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (100010, 100001, 'Id', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 0, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (100011, 100001, 'RowGuid', NULL, NULL, 'System.Guid', '', '', 0, 0, 0, 0, 0, 1, 1, NULL, 9, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (100012, 100001, 'ModifiedDate', NULL, 'Default', 'System.DateTime', 'datetime', 'Date', 0, 0, 0, 0, 0, 1, 1, NULL, 10, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (100013, 100002, 'RowNo', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 0, 0, 0, N'AlwaysVisible', 0, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (100014, 100002, 'Name', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 0, 0, 1, 1, NULL, 1, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (100015, 100002, 'EnName', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 0, 0, 1, 1, NULL, 2, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (100016, 100002, 'Description', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 0, 0, 1, 1, NULL, 3, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (100017, 100002, 'Symbol', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 0, 0, 1, 1, NULL, 4, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (100018, 100002, 'Status', NULL, NULL, 'System.Int16', 'smallint', 'number', 0, 0, 0, 0, 0, 1, 1, NULL, 5, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (100019, 100002, 'IsActive', NULL, NULL, 'System.Boolean', 'bit', 'boolean', 0, 0, 0, 0, 0, 1, 1, NULL, 6, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (100020, 100002, 'BranchId', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 0, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (100021, 100002, 'Id', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 0, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (100022, 100002, 'RowGuid', NULL, NULL, 'System.DateTime', 'datetime', 'Date', 0, 0, 0, 0, 0, 1, 1, NULL, 8, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (100023, 100002, 'ModifiedDate', NULL, 'Default', 'System.DateTime', 'datetime', 'Date', 0, 0, 0, 0, 0, 1, 1, NULL, 9, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (100024, 100003, 'RowNo', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 0, 0, 0, N'AlwaysVisible', 0, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (100025, 100003, 'Name', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 0, 0, 1, 1, NULL, 1, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (100026, 100003, 'EnName', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 0, 0, 1, 1, NULL, 2, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (100027, 100003, 'Description', NULL, NULL, 'System.String', 'nvarchar', 'string', 1024, 0, 0, 0, 0, 1, 1, NULL, 3, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (100028, 100003, 'Type', NULL, NULL, 'System.Int16', 'smallint', 'number', 0, 0, 0, 0, 0, 1, 1, NULL, 4, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (100029, 100003, 'Prefix', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 0, 0, 1, 1, NULL, 5, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (100030, 100003, 'Suffix', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 0, 0, 1, 1, NULL, 6, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (100031, 100003, 'IsActive', NULL, NULL, 'System.Boolean', 'bit', 'boolean', 0, 0, 0, 0, 0, 1, 1, NULL, 7, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (100032, 100003, 'BranchId', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 0, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (100033, 100003, 'Id', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 0, 1, 1, N'AlwaysHidden', -1, NULL)
SET IDENTITY_INSERT [Metadata].[Column] OFF

UPDATE [Metadata].[View]
SET [FetchUrl] = NULL
WHERE [FetchUrl] = ''
GO

UPDATE [Metadata].[View]
SET [EntityType] = NULL
WHERE [EntityType] = ''
GO

UPDATE [Metadata].[Column]
SET [Type] = 'Default'
WHERE DotNetType LIKE 'System.Date%'

UPDATE [Metadata].[Column]
SET AllowSorting = 0, AllowFiltering = 0, IsNullable = 1
WHERE [Name] = 'RowNo'

SET IDENTITY_INSERT [Metadata].[ValidRowPermission] ON

INSERT INTO [Metadata].[ValidRowPermission] ([RowPermissionID], [ViewID], [AccessMode]) VALUES (1, 1, 'Access_Default')
INSERT INTO [Metadata].[ValidRowPermission] ([RowPermissionID], [ViewID], [AccessMode]) VALUES (2, 1, 'Access_SpecificRecords')
INSERT INTO [Metadata].[ValidRowPermission] ([RowPermissionID], [ViewID], [AccessMode]) VALUES (3, 1, 'Access_AllExceptSpecificRecords')
INSERT INTO [Metadata].[ValidRowPermission] ([RowPermissionID], [ViewID], [AccessMode]) VALUES (4, 2, 'Access_Default')
INSERT INTO [Metadata].[ValidRowPermission] ([RowPermissionID], [ViewID], [AccessMode]) VALUES (5, 2, 'Access_AllRecordsCreatedByUser')
INSERT INTO [Metadata].[ValidRowPermission] ([RowPermissionID], [ViewID], [AccessMode]) VALUES (6, 2, 'Access_SpecificRecords')
INSERT INTO [Metadata].[ValidRowPermission] ([RowPermissionID], [ViewID], [AccessMode]) VALUES (7, 2, 'Access_AllExceptSpecificRecords')
INSERT INTO [Metadata].[ValidRowPermission] ([RowPermissionID], [ViewID], [AccessMode]) VALUES (8, 2, 'Access_SpecificReferences')
INSERT INTO [Metadata].[ValidRowPermission] ([RowPermissionID], [ViewID], [AccessMode]) VALUES (9, 2, 'Access_AllExceptSpecificReferences')
INSERT INTO [Metadata].[ValidRowPermission] ([RowPermissionID], [ViewID], [AccessMode]) VALUES (10, 3, 'Access_Default')
INSERT INTO [Metadata].[ValidRowPermission] ([RowPermissionID], [ViewID], [AccessMode]) VALUES (11, 3, 'Access_SpecificRecords')
INSERT INTO [Metadata].[ValidRowPermission] ([RowPermissionID], [ViewID], [AccessMode]) VALUES (12, 3, 'Access_AllExceptSpecificRecords')
INSERT INTO [Metadata].[ValidRowPermission] ([RowPermissionID], [ViewID], [AccessMode]) VALUES (13, 3, 'Access_MaxMoneyValue')
INSERT INTO [Metadata].[ValidRowPermission] ([RowPermissionID], [ViewID], [AccessMode]) VALUES (14, 6, 'Access_Default')
INSERT INTO [Metadata].[ValidRowPermission] ([RowPermissionID], [ViewID], [AccessMode]) VALUES (15, 6, 'Access_SpecificRecords')
INSERT INTO [Metadata].[ValidRowPermission] ([RowPermissionID], [ViewID], [AccessMode]) VALUES (16, 6, 'Access_AllExceptSpecificRecords')
INSERT INTO [Metadata].[ValidRowPermission] ([RowPermissionID], [ViewID], [AccessMode]) VALUES (17, 7, 'Access_Default')
INSERT INTO [Metadata].[ValidRowPermission] ([RowPermissionID], [ViewID], [AccessMode]) VALUES (18, 7, 'Access_SpecificRecords')
INSERT INTO [Metadata].[ValidRowPermission] ([RowPermissionID], [ViewID], [AccessMode]) VALUES (19, 7, 'Access_AllExceptSpecificRecords')
INSERT INTO [Metadata].[ValidRowPermission] ([RowPermissionID], [ViewID], [AccessMode]) VALUES (20, 8, 'Access_Default')
INSERT INTO [Metadata].[ValidRowPermission] ([RowPermissionID], [ViewID], [AccessMode]) VALUES (21, 8, 'Access_SpecificRecords')
INSERT INTO [Metadata].[ValidRowPermission] ([RowPermissionID], [ViewID], [AccessMode]) VALUES (22, 8, 'Access_AllExceptSpecificRecords')

SET IDENTITY_INSERT [Metadata].[ValidRowPermission] OFF

-- Create configuration records...
SET IDENTITY_INSERT [Config].[Setting] ON
INSERT INTO [Config].[Setting] (SettingID, TitleKey, [Type], ScopeType, ModelType, [Values], DefaultValues, DescriptionKey, IsStandalone)
    VALUES (4, 'ListFormViewSettings', 3, 2, 'ListFormViewConfig', N'{"pageSize": 10, "columnViews": []}', N'{"pageSize": 10, "columnViews": []}', 'ListFormViewSettingsDescription', 1)
INSERT INTO [Config].[Setting] (SettingID, TitleKey, [Type], ScopeType, ModelType, [Values], DefaultValues, DescriptionKey, IsStandalone)
    VALUES (7, 'QuickReportSettings', 3, 2, 'QuickReportConfig', N'{}', N'{}', 'QuickReportSettingsDescription', 1)
SET IDENTITY_INSERT [Config].[Setting] OFF


-- Create system records for security

-- @AdminUserName user is added with password '@Password' (case-sensitive)
SET IDENTITY_INSERT [Auth].[User] ON
INSERT INTO [Auth].[User] (UserID, UserName, PasswordHash, IsEnabled) VALUES (1, N'@AdminUserName', '@AdminPasswordHash', 1)
SET IDENTITY_INSERT [Auth].[User] OFF

SET IDENTITY_INSERT [Contact].[Person] ON
INSERT INTO [Contact].[Person] (PersonID, UserID, FirstName, LastName) VALUES (1, 1, N'@AdminFirstName', N'@AdminLastName')
SET IDENTITY_INSERT [Contact].[Person] OFF

SET IDENTITY_INSERT [Auth].[Role] ON
INSERT [Auth].[Role] ([RoleID], [Name], [Description]) VALUES (1, N'Role_SysAdmin', N'Role_SysAdminDesc')
SET IDENTITY_INSERT [Auth].[Role] OFF

SET IDENTITY_INSERT [Auth].[UserRole] ON
INSERT INTO [Auth].[UserRole] (UserRoleID, UserID, RoleID) VALUES (1, 1, 1)
SET IDENTITY_INSERT [Auth].[UserRole] OFF

SET IDENTITY_INSERT [Auth].[PermissionGroup] ON
INSERT INTO [Auth].[PermissionGroup] ([PermissionGroupID], [SubsystemID], [SourceTypeID], [Name], [EntityName], [Description])
    VALUES (1, 2, 1, N'ManageEntities,Accounts', N'Account', NULL)
INSERT INTO [Auth].[PermissionGroup] ([PermissionGroupID], [SubsystemID], [SourceTypeID], [Name], [EntityName], [Description])
    VALUES (2, 2, 1, N'ManageEntities,DetailAccounts', N'DetailAccount', NULL)
INSERT INTO [Auth].[PermissionGroup] ([PermissionGroupID], [SubsystemID], [SourceTypeID], [Name], [EntityName], [Description])
    VALUES (3, 2, 1, N'ManageEntities,CostCenters', N'CostCenter', NULL)
INSERT INTO [Auth].[PermissionGroup] ([PermissionGroupID], [SubsystemID], [SourceTypeID], [Name], [EntityName], [Description])
    VALUES (4, 2, 1, N'ManageEntities,Projects', N'Project', NULL)
INSERT INTO [Auth].[PermissionGroup] ([PermissionGroupID], [SubsystemID], [SourceTypeID], [Name], [EntityName], [Description])
    VALUES (5, 2, 1, N'ManageEntities,FiscalPeriods', N'FiscalPeriod', NULL)
INSERT INTO [Auth].[PermissionGroup] ([PermissionGroupID], [SubsystemID], [SourceTypeID], [Name], [EntityName], [Description])
    VALUES (6, 2, 1, N'ManageEntities,Currencies', N'Currency', NULL)
INSERT INTO [Auth].[PermissionGroup] ([PermissionGroupID], [SubsystemID], [SourceTypeID], [Name], [EntityName], [Description])
    VALUES (7, 2, 2, N'Vouchers', N'Voucher', NULL)
INSERT INTO [Auth].[PermissionGroup] ([PermissionGroupID], [SubsystemID], [SourceTypeID], [Name], [EntityName], [Description])
    VALUES (8, 2, 2, N'ManageEntities,Vouchers', N'Vouchers', NULL)
INSERT INTO [Auth].[PermissionGroup] ([PermissionGroupID], [SubsystemID], [SourceTypeID], [Name], [EntityName], [Description])
    VALUES (9, 1, 1, N'ManageEntities,Branches', N'Branch', NULL)
INSERT INTO [Auth].[PermissionGroup] ([PermissionGroupID], [SubsystemID], [SourceTypeID], [Name], [EntityName], [Description])
    VALUES (10, 1, 1, N'ManageEntities,Companies', N'Company', NULL)
INSERT INTO [Auth].[PermissionGroup] ([PermissionGroupID], [SubsystemID], [SourceTypeID], [Name], [EntityName], [Description])
    VALUES (11, 1, 1, N'ManageEntities,Users', N'User', NULL)
INSERT INTO [Auth].[PermissionGroup] ([PermissionGroupID], [SubsystemID], [SourceTypeID], [Name], [EntityName], [Description])
    VALUES (12, 1, 1, N'ManageEntities,Roles', N'Role', NULL)
INSERT INTO [Auth].[PermissionGroup] ([PermissionGroupID], [SubsystemID], [SourceTypeID], [Name], [EntityName], [Description])
    VALUES (13, 2, 1, N'ManageEntities,AccountGroups', N'AccountGroup', NULL)
INSERT INTO [Auth].[PermissionGroup] ([PermissionGroupID], [SubsystemID], [SourceTypeID], [Name], [EntityName], [Description])
    VALUES (14, 2, 1, N'ManageEntities,AccountCollections', N'AccountCollection', NULL)
INSERT INTO [Auth].[PermissionGroup] ([PermissionGroupID], [SubsystemID], [SourceTypeID], [Name], [EntityName], [Description])
    VALUES (15, 1, 3, N'ManageEntities,OperationLogs', N'OperationLog', NULL)
INSERT INTO [Auth].[PermissionGroup] ([PermissionGroupID], [SubsystemID], [SourceTypeID], [Name], [EntityName], [Description])
    VALUES (16, 1, 3, N'ManageEntities,SysOperationLogs', N'SysOperationLog', NULL)
INSERT INTO [Auth].[PermissionGroup] ([PermissionGroupID], [SubsystemID], [SourceTypeID], [Name], [EntityName], [Description])
    VALUES (17, 1, 3, N'ManageEntities,Reports', N'Report', NULL)
INSERT INTO [Auth].[PermissionGroup] ([PermissionGroupID], [SubsystemID], [SourceTypeID], [Name], [EntityName], [Description])
    VALUES (18, 1, 3, N'ManageEntities,UserReports', N'UserReport', NULL)
INSERT INTO [Auth].[PermissionGroup] ([PermissionGroupID], [SubsystemID], [SourceTypeID], [Name], [EntityName], [Description])
    VALUES (19, 2, 1, N'AccountRelations', N'AccountRelations', NULL)
INSERT INTO [Auth].[PermissionGroup] ([PermissionGroupID], [SubsystemID], [SourceTypeID], [Name], [EntityName], [Description])
    VALUES (20, 1, 1, N'Settings', N'Setting', NULL)
INSERT INTO [Auth].[PermissionGroup] ([PermissionGroupID], [SubsystemID], [SourceTypeID], [Name], [EntityName], [Description])
    VALUES (21, 1, 1, N'LogSetting', N'LogSetting', NULL)
INSERT INTO [Auth].[PermissionGroup] ([PermissionGroupID], [SubsystemID], [SourceTypeID], [Name], [EntityName], [Description])
    VALUES (22, 1, 1, N'RowAccessSettings', N'RowAccess', NULL)
INSERT INTO [Auth].[PermissionGroup] ([PermissionGroupID], [SubsystemID], [SourceTypeID], [Name], [EntityName], [Description])
    VALUES (23, 2, 1, N'CurrencyRate', N'CurrencyRate', NULL)
INSERT INTO [Auth].[PermissionGroup] ([PermissionGroupID], [SubsystemID], [SourceTypeID], [Name], [EntityName], [Description])
    VALUES (24, 2, 3, N'JournalReport', N'Journal', NULL)
INSERT INTO [Auth].[PermissionGroup] ([PermissionGroupID], [SubsystemID], [SourceTypeID], [Name], [EntityName], [Description])
    VALUES (25, 2, 3, N'AccountBookReport', N'AccountBook', NULL)
INSERT INTO [Auth].[PermissionGroup] ([PermissionGroupID], [SubsystemID], [SourceTypeID], [Name], [EntityName], [Description])
    VALUES (26, 2, 3, N'TestBalanceReport', N'TestBalance', NULL)
INSERT INTO [Auth].[PermissionGroup] ([PermissionGroupID], [SubsystemID], [SourceTypeID], [Name], [EntityName], [Description])
    VALUES (27, 2, 3, N'CurrencyBookReport', N'CurrencyBook', NULL)
INSERT INTO [Auth].[PermissionGroup] ([PermissionGroupID], [SubsystemID], [SourceTypeID], [Name], [EntityName], [Description])
    VALUES (28, 2, 3, N'ItemBalanceReport', N'ItemBalance', NULL)
INSERT INTO [Auth].[PermissionGroup] ([PermissionGroupID], [SubsystemID], [SourceTypeID], [Name], [EntityName], [Description])
    VALUES (29, 2, 3, N'BalanceByAccountReport', N'BalanceByAccount', NULL)
INSERT INTO [Auth].[PermissionGroup] ([PermissionGroupID], [SubsystemID], [SourceTypeID], [Name], [EntityName], [Description])
    VALUES (30, 1, 3, N'SystemIssueReport', N'SystemIssue', NULL)
INSERT INTO [Auth].[PermissionGroup] ([PermissionGroupID], [SubsystemID], [SourceTypeID], [Name], [EntityName], [Description])
    VALUES (31, 2, 3, N'ProfitLossReport', N'ProfitLoss', NULL)
INSERT INTO [Auth].[PermissionGroup] ([PermissionGroupID], [SubsystemID], [SourceTypeID], [Name], [EntityName], [Description])
    VALUES (32, 2, 2, N'DraftVouchers', N'DraftVoucher', NULL)
INSERT INTO [Auth].[PermissionGroup] ([PermissionGroupID], [SubsystemID], [SourceTypeID], [Name], [EntityName], [Description])
    VALUES (33, 2, 2, N'ManageEntities,DraftVouchers', N'DraftVouchers', NULL)
INSERT INTO [Auth].[PermissionGroup] ([PermissionGroupID], [SubsystemID], [SourceTypeID], [Name], [EntityName], [Description])
    VALUES (34, 2, 3, N'BalanceSheetReport', N'BalanceSheet', NULL)
INSERT INTO [Auth].[PermissionGroup] ([PermissionGroupID], [SubsystemID], [SourceTypeID], [Name], [EntityName], [Description])
    VALUES (35, 2, 2, N'SpecialVoucherOps', N'SpecialVoucher', NULL)
INSERT INTO [Auth].[PermissionGroup] ([PermissionGroupID], [SubsystemID], [SourceTypeID], [Name], [EntityName], [Description])
    VALUES (36, 2, 3, N'Dashboard', N'Dashboard', NULL)
INSERT INTO [Auth].[PermissionGroup] ([PermissionGroupID], [SubsystemID], [SourceTypeID], [Name], [EntityName], [Description])
    VALUES (37, 3, 2, N'ManageEntities,CheckBooks', N'CheckBook', NULL)
INSERT INTO [Auth].[PermissionGroup] ([PermissionGroupID], [SubsystemID], [SourceTypeID], [Name], [EntityName], [Description])
    VALUES (38, 3, 1, N'ManageEntities,CashRegisters', N'CashRegister', NULL)
INSERT INTO [Auth].[PermissionGroup] ([PermissionGroupID], [SubsystemID], [SourceTypeID], [Name], [EntityName], [Description])
    VALUES (39, 3, 3, N'CheckBookReport', N'CheckBookReport', NULL)
INSERT INTO [Auth].[PermissionGroup] ([PermissionGroupID], [SubsystemID], [SourceTypeID], [Name], [EntityName], [Description])
    VALUES (40, 3, 1, N'ManageEntities,SourceApps', N'SourceApp', NULL)
INSERT INTO [Auth].[PermissionGroup] ([PermissionGroupID], [SubsystemID], [SourceTypeID], [Name], [EntityName], [Description])
    VALUES (41, 3, 2, N'Receipts', N'Receipt', NULL)
INSERT INTO [Auth].[PermissionGroup] ([PermissionGroupID], [SubsystemID], [SourceTypeID], [Name], [EntityName], [Description])
    VALUES (42, 3, 2, N'Payments', N'Payment', NULL)







































































































INSERT INTO [Auth].[PermissionGroup] ([PermissionGroupID], [SubsystemID], [SourceTypeID], [Name], [EntityName], [Description])
    VALUES (100001, 100000, 1, N'ManageEntities,Brands', N'Brand', NULL)
INSERT INTO [Auth].[PermissionGroup] ([PermissionGroupID], [SubsystemID], [SourceTypeID], [Name], [EntityName], [Description])
    VALUES (100002, 100000, 1, N'ManageEntities,Units', N'Unit', NULL)
INSERT INTO [Auth].[PermissionGroup] ([PermissionGroupID], [SubsystemID], [SourceTypeID], [Name], [EntityName], [Description])
    VALUES (100003, 100000, 1, N'ManageEntities,Properties', N'Property', NULL)
SET IDENTITY_INSERT [Auth].[PermissionGroup] OFF

SET IDENTITY_INSERT [Auth].[Permission] ON
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (1, 1, N'View', 1, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (2, 1, N'Filter', 2, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (3, 1, N'Print', 4, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (4, 1, N'Export', 8, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (5, 1, N'Create', 16, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (6, 1, N'Edit', 32, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (7, 1, N'Delete', 64, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (8, 2, N'View', 1, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (9, 2, N'Filter', 2, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (10, 2, N'Print', 4, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (11, 2, N'Export', 8, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (12, 2, N'Create', 16, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (13, 2, N'Edit', 32, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (14, 2, N'Delete', 64, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (15, 3, N'View', 1, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (16, 3, N'Filter', 2, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (17, 3, N'Print', 4, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (18, 3, N'Export', 8, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (19, 3, N'Create', 16, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (20, 3, N'Edit', 32, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (21, 3, N'Delete', 64, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (22, 4, N'View', 1, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (23, 4, N'Filter', 2, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (24, 4, N'Print', 4, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (25, 4, N'Export', 8, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (26, 4, N'Create', 16, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (27, 4, N'Edit', 32, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (28, 4, N'Delete', 64, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (29, 5, N'View', 1, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (30, 5, N'Filter', 2, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (31, 5, N'Print', 4, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (32, 5, N'Export', 8, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (33, 5, N'Create', 16, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (34, 5, N'Edit', 32, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (35, 5, N'Delete', 64, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (36, 5, N'AssignRolesToEntity,FiscalPeriod', 128, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (37, 6, N'View', 1, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (38, 6, N'Filter', 2, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (39, 6, N'Print', 4, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (40, 6, N'Export', 8, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (41, 6, N'Create', 16, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (42, 6, N'Edit', 32, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (43, 6, N'Delete', 64, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (44, 6, N'ChangeStatus', 128, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (45, 7, N'View', 1, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (46, 7, N'Create', 2, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (47, 7, N'Edit', 4, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (48, 7, N'Delete', 8, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (49, 7, N'Print', 16, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (50, 7, N'CreateLine', 32, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (51, 7, N'EditLine', 64, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (52, 7, N'DeleteLine', 128, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (53, 7, N'Check', 256, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (54, 7, N'UndoCheck', 512, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (55, 7, N'Confirm', 1024, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (56, 7, N'UndoConfirm', 2048, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (57, 7, N'Approve', 4096, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (58, 7, N'UndoApprove', 8192, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (59, 7, N'Finalize', 16384, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (60, 7, N'NavigateEntities,Vouchers', 32768, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (61, 8, N'View', 1, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (62, 8, N'Filter', 2, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (63, 8, N'Print', 4, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (64, 8, N'Export', 8, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (65, 8, N'GroupCheck', 16, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (66, 8, N'GroupUndoCheck', 32, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (67, 8, N'GroupConfirm', 64, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (68, 8, N'GroupUndoConfirm', 128, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (69, 8, N'GroupFinalize', 256, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (70, 9, N'View', 1, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (71, 9, N'Filter', 2, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (72, 9, N'Print', 4, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (73, 9, N'Export', 8, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (74, 9, N'Create', 16, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (75, 9, N'Edit', 32, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (76, 9, N'Delete', 64, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (77, 9, N'AssignRolesToEntity,Branch', 128, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (78, 10, N'View', 1, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (79, 10, N'Filter', 2, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (80, 10, N'Print', 4, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (81, 10, N'Export', 8, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (82, 11, N'View', 1, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (83, 11, N'Filter', 2, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (84, 11, N'Print', 4, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (85, 11, N'Export', 8, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (86, 11, N'Create', 16, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (87, 11, N'Edit', 32, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (88, 11, N'AssignRolesToEntity,User', 64, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (89, 12, N'View', 1, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (90, 12, N'Filter', 2, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (91, 12, N'Print', 4, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (92, 12, N'Export', 8, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (93, 12, N'Create', 16, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (94, 12, N'Edit', 32, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (95, 12, N'Delete', 64, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (96, 12, N'AssignEntityToRole,User', 128, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (97, 12, N'AssignEntityToRole,Branch', 256, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (98, 12, N'AssignEntityToRole,FiscalPeriod', 512, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (99, 13, N'View', 1, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (100, 13, N'Filter', 2, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (101, 13, N'Print', 4, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (102, 13, N'Export', 8, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (103, 13, N'Create', 16, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (104, 13, N'Edit', 32, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (105, 13, N'Delete', 64, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (106, 14, N'View', 1, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (107, 14, N'Save', 2, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (108, 15, N'View', 1, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (109, 15, N'Filter', 2, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (110, 15, N'Print', 4, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (111, 15, N'Export', 8, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (112, 15, N'Archive', 16, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (113, 15, N'ViewArchive', 32, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (114, 16, N'View', 1, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (115, 16, N'Filter', 2, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (116, 16, N'Print', 4, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (117, 16, N'Export', 8, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (118, 16, N'Archive', 16, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (119, 16, N'ViewArchive', 32, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (120, 17, N'View', 1, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (121, 17, N'Design', 2, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (122, 17, N'QuickReportDesign', 4, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (123, 18, N'Save', 1, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (124, 18, N'Delete', 2, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (125, 18, N'SetDefault', 4, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (126, 19, N'View', 1, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (127, 19, N'Save', 2, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (128, 20, N'View', 1, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (129, 20, N'Save', 2, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (130, 21, N'View', 1, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (131, 21, N'Save', 2, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (132, 22, N'View', 1, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (133, 22, N'Save', 2, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (134, 23, N'View', 1, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (135, 23, N'Filter', 2, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (136, 23, N'Print', 4, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (137, 23, N'Export', 8, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (138, 23, N'Create', 16, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (139, 23, N'Edit', 32, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (140, 23, N'Delete', 64, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (141, 24, N'View', 1, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (142, 24, N'Filter', 2, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (143, 24, N'Print', 4, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (144, 24, N'Export', 8, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (145, 24, N'Mark', 16, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (146, 24, N'ViewByBranch', 32, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (147, 25, N'View', 1, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (148, 25, N'Filter', 2, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (149, 25, N'Print', 4, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (150, 25, N'Export', 8, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (151, 25, N'Mark', 16, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (152, 25, N'ViewByBranch', 32, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (153, 26, N'View', 1, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (154, 26, N'Filter', 2, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (155, 26, N'Print', 4, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (156, 26, N'Export', 8, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (157, 26, N'ViewByBranch', 16, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (197, 26, N'FilterByRef', 32, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (158, 27, N'View', 1, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (159, 27, N'Filter', 2, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (160, 27, N'Print', 4, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (161, 27, N'Export', 8, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (162, 27, N'Mark', 16, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (163, 27, N'ViewByBranch', 32, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (164, 28, N'View', 1, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (165, 28, N'Filter', 2, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (166, 28, N'Print', 4, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (167, 28, N'Export', 8, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (168, 28, N'ViewByBranch', 16, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (198, 28, N'FilterByRef', 32, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (169, 29, N'View', 1, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (170, 29, N'Filter', 2, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (171, 29, N'Print', 4, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (172, 29, N'Export', 8, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (173, 29, N'ViewByBranch', 16, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (199, 29, N'FilterByRef', 32, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (174, 30, N'View', 1, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (175, 31, N'View', 1, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (176, 31, N'Filter', 2, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (177, 31, N'Print', 4, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (178, 31, N'Export', 8, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (200, 31, N'FilterByRef', 16, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (179, 32, N'View', 1, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (180, 32, N'Create', 2, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (181, 32, N'Edit', 4, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (182, 32, N'Delete', 8, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (183, 32, N'Print', 16, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (184, 32, N'CreateLine', 32, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (185, 32, N'EditLine', 64, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (186, 32, N'DeleteLine', 128, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (187, 32, N'Check', 256, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (188, 32, N'UndoCheck', 512, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (189, 32, N'NavigateEntities,DraftVouchers', 1024, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (196, 32, N'Normalize', 2048, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (190, 33, N'View', 1, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (191, 33, N'Filter', 2, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (192, 33, N'Print', 4, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (193, 33, N'Export', 8, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (194, 33, N'GroupCheck', 16, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (195, 33, N'GroupUndoCheck', 32, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (201, 34, N'View', 1, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (202, 34, N'Filter', 2, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (203, 34, N'Print', 4, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (204, 34, N'Export', 8, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (205, 34, N'FilterByRef', 16, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (206, 35, N'IssueOpeningVoucher', 1, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (207, 35, N'IssueClosingTempAccountsVoucher', 2, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (208, 35, N'IssueClosingVoucher', 4, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (209, 35, N'UncheckClosingVoucher', 8, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (210, 36, N'ManageDashboard', 1, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (211, 36, N'ManageWidgets', 2, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (212, 37, N'View', 1, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (213, 37, N'Filter', 2, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (214, 37, N'Print', 4, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (215, 37, N'Create', 8, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (216, 37, N'Edit', 16, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (217, 37, N'Delete', 32, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (218, 37, N'NavigateEntities,CheckBooks', 64, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (219, 37, N'CreatePages', 128, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (220, 37, N'DeletePages', 256, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (221, 37, N'CancelPage', 512, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (222, 37, N'UndoCancelPage', 1024, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (223, 37, N'ConnectToCheck', 2048, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (224, 37, N'DisconnectFromCheck', 4096, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (225, 38, N'View', 1, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (226, 38, N'Filter', 2, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (227, 38, N'Print', 4, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (228, 38, N'Export', 8, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (229, 38, N'Create', 16, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (230, 38, N'Edit', 32, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (231, 38, N'Delete', 64, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (232, 38, N'AssignCashRegisterUser', 128, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (233, 39, N'View', 1, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (234, 39, N'Filter', 2, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (235, 39, N'Print', 4, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (236, 39, N'Export', 8, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (237, 39, N'Archive', 16, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (238, 39, N'UndoArchive', 32, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (239, 40, N'View', 1, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (240, 40, N'Filter', 2, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (241, 40, N'Print', 4, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (242, 40, N'Export', 8, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (243, 40, N'Create', 16, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (244, 40, N'Edit', 32, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (245, 40, N'Delete', 64, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (246, 41, N'View', 1, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (247, 41, N'Print', 2, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (248, 41, N'Create', 4, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (249, 41, N'Edit', 8, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (250, 41, N'Delete', 16, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (251, 41, N'NavigateEntities,Receipts', 32, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (252, 41, N'Register', 64, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (253, 41, N'Confirm', 128, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (254, 41, N'UndoConfirm', 256, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (255, 41, N'Approve', 512, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (256, 41, N'UndoApprove', 1024, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (257, 42, N'View', 1, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (258, 42, N'Print', 2, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (259, 42, N'Create', 4, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (260, 42, N'Edit', 8, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (261, 42, N'Delete', 16, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
     VALUES (262, 42, N'NavigateEntities,Payments', 32, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (263, 42, N'Register', 64, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (264, 42, N'Confirm', 128, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (265, 42, N'UndoConfirm', 256, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (266, 42, N'Approve', 512, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (267, 42, N'UndoApprove', 1024, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (268, 1, N'Deactivate', 128, N'Mark an active account as inactive')
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (269, 1, N'Reactivate', 256, N'Mark an inactive account as active')
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (270, 2, N'Deactivate', 128, N'Mark an active detail account as inactive')
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (271, 2, N'Reactivate', 256, N'Mark an inactive detail account as active')
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (272, 3, N'Deactivate', 128, N'Mark an active cost center as inactive')
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (273, 3, N'Reactivate', 256, N'Mark an inactive cost center as active')
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (274, 4, N'Deactivate', 128, N'Mark an active project as inactive')
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (275, 4, N'Reactivate', 256, N'Mark an inactive project as active')
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (276, 6, N'Deactivate', 128, N'Mark an active currency as inactive')
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (277, 6, N'Reactivate', 256, N'Mark an inactive currency as active')
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (278, 38, N'Deactivate', 256, N'Mark an active cash register as inactive')
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (279, 38, N'Reactivate', 512, N'Mark an inactive cash register as active')
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (280, 40, N'Deactivate', 128, N'Mark an active source/application as inactive')
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (281, 40, N'Reactivate', 256, N'Mark an inactive source/application as active')
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (282, 41, N'UndoRegister', 2048, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (283, 42, N'UndoRegister', 2048, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (284, 30, N'Filter', 2, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (285, 30, N'Print', 4, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (286, 30, N'Export', 8, NULL)




























































































































































































INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (100001, 100001, N'View', 1, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (100002, 100001, N'Filter', 2, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (100003, 100001, N'Print', 4, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (100004, 100001, N'Export', 8, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (100005, 100001, N'Create', 16, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (100006, 100001, N'Edit', 32, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (100007, 100001, N'Delete', 64, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (100008, 100002, N'View', 1, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (100009, 100002, N'Filter', 2, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (100010, 100002, N'Print', 4, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (100011, 100002, N'Export', 8, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (100012, 100002, N'Create', 16, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (100013, 100002, N'Edit', 32, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (100014, 100002, N'Delete', 64, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (100015, 100003, N'View', 1, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (100016, 100003, N'Filter', 2, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (100017, 100003, N'Print', 4, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (100018, 100003, N'Export', 8, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (100019, 100003, N'Create', 16, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (100020, 100003, N'Edit', 32, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (100021, 100003, N'Delete', 64, NULL)

SET IDENTITY_INSERT [Auth].[Permission] OFF

SET IDENTITY_INSERT [Metadata].[ShortcutCommand] ON 

INSERT [Metadata].[ShortcutCommand] ([ShortcutCommandID], [PermissionID], [Name], [Scope], [HotKey], [Method]) VALUES (1, NULL, N'NewVoucherLine', N'VoucherLineComponent', N'Ctrl+O', N'sh_addVoucherLine')

INSERT [Metadata].[ShortcutCommand] ([ShortcutCommandID], [PermissionID], [Name], [Scope], [HotKey], [Method]) VALUES (2, NULL, N'ExportToExcel', N'AutoGeneratedGridComponent,AutoGridExplorerComponent', N'Alt+E', N'sh_exportToExcel')

INSERT [Metadata].[ShortcutCommand] ([ShortcutCommandID], [PermissionID], [Name], [Scope], [HotKey], [Method]) VALUES (3, NULL, N'Print', N'AutoGeneratedGridComponent,AutoGridExplorerComponent,DetailComponent', N'Alt+P', N'sh_print')

INSERT [Metadata].[ShortcutCommand] ([ShortcutCommandID], [PermissionID], [Name], [Scope], [HotKey], [Method]) VALUES (4, NULL, N'ReportSetting', N'AutoGeneratedGridComponent,AutoGridExplorerComponent', N'Alt+S', N'sh_openReportSetting')

INSERT [Metadata].[ShortcutCommand] ([ShortcutCommandID], [PermissionID], [Name], [Scope], [HotKey], [Method]) VALUES (5, NULL, N'AdvanceFilter', N'AutoGeneratedGridComponent,AutoGridExplorerComponent', N'Alt+L', N'sh_openAdvanceFilter')

INSERT [Metadata].[ShortcutCommand] ([ShortcutCommandID], [PermissionID], [Name], [Scope], [HotKey], [Method]) VALUES (6, NULL, N'NewRecord', N'AutoGeneratedGridComponent,AutoGridExplorerComponent', N'Ctrl+Alt+N', N'sh_openNewDialog')

INSERT [Metadata].[ShortcutCommand] ([ShortcutCommandID], [PermissionID], [Name], [Scope], [HotKey], [Method]) VALUES (7, NULL, N'EditRecord', N'AutoGeneratedGridComponent,AutoGridExplorerComponent', N'Ctrl+Alt+M', N'sh_openEditDialog')

INSERT [Metadata].[ShortcutCommand] ([ShortcutCommandID], [PermissionID], [Name], [Scope], [HotKey], [Method]) VALUES (8, NULL, N'DeleteRecord', N'AutoGeneratedGridComponent,AutoGridExplorerComponent', N'Ctrl+Shift+R', N'sh_delete')

INSERT [Metadata].[ShortcutCommand] ([ShortcutCommandID], [PermissionID], [Name], [Scope], [HotKey], [Method]) VALUES (9, NULL, N'ExecuteFilter', N'AutoGeneratedGridComponent,AutoGridExplorerComponent', N'Ctrl+Alt+L', N'sh_executeFilter')

INSERT [Metadata].[ShortcutCommand] ([ShortcutCommandID], [PermissionID], [Name], [Scope], [HotKey], [Method]) VALUES (10, NULL, N'NewVoucher', N'VoucherEditorComponent', N'Alt+Insert', N'sh_newVoucher')

INSERT [Metadata].[ShortcutCommand] ([ShortcutCommandID], [PermissionID], [Name], [Scope], [HotKey], [Method]) VALUES (11, NULL, N'RemoveVoucher', N'VoucherEditorComponent', N'Alt+Delete', N'sh_removeVoucher')

INSERT [Metadata].[ShortcutCommand] ([ShortcutCommandID], [PermissionID], [Name], [Scope], [HotKey], [Method]) VALUES (12, NULL, N'CheckVoucher', N'VoucherEditorComponent', N'Alt+C', N'sh_checkVoucher')

INSERT [Metadata].[ShortcutCommand] ([ShortcutCommandID], [PermissionID], [Name], [Scope], [HotKey], [Method]) VALUES (13, NULL, N'UnCheckVoucher', N'VoucherEditorComponent', N'Alt+K', N'sh_unCheckVoucher')

INSERT [Metadata].[ShortcutCommand] ([ShortcutCommandID], [PermissionID], [Name], [Scope], [HotKey], [Method]) VALUES (14, NULL, N'ReportManagement', N'NavMenuComponent,AutoGeneratedGridComponent,AutoGridExplorerComponent', N'Alt+Q', N'sh_openReportManager')

SET IDENTITY_INSERT [Metadata].[ShortcutCommand] OFF

SET IDENTITY_INSERT [Reporting].[Report] ON
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (1, NULL, 1, NULL, 1, N'Administration', NULL, 1, 1, 0, 0, NULL)
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (2, 1, 1, NULL, 1, N'Admin-Base', NULL, 1, 1, 0, 0, NULL)
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (3, 1, 1, NULL, 1, N'Admin-Operation', NULL, 1, 1, 0, 0, NULL)
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (4, 1, 1, NULL, 1, N'Admin-Report', NULL, 1, 1, 0, 0, NULL)
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (5, 2, 1, NULL, 1, N'Admin-Base-QReport', NULL, 1, 1, 0, 0, NULL)
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (6, 3, 1, NULL, 1, N'Admin-Operation-QReport', NULL, 1, 1, 0, 0, NULL)
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (7, 4, 1, NULL, 1, N'Admin-Report-QReport', NULL, 1, 1, 0, 0, NULL)
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (8, 5, 1, 11, 1, N'Companies', N'companies', 0, 1, 1, 1, NULL)
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (9, 5, 1, 10, 1, N'Branches', N'branches', 0, 1, 1, 1, NULL)
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (10, 5, 1, 4, 1, N'Users', N'users', 0, 1, 1, 1, NULL)
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (11, 5, 1, 5, 1, N'Roles', N'roles', 0, 1, 1, 1, NULL)
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (13, NULL, 1, NULL, 2, N'Accounting', NULL, 1, 1, 0, 0, NULL)
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (14, 13, 1, NULL, 2, N'Accnt-Base', NULL, 1, 1, 0, 0, NULL)
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (15, 13, 1, NULL, 2, N'Accnt-Operation', NULL, 1, 1, 0, 0, NULL)
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (16, 13, 1, NULL, 2, N'Accnt-Report', NULL, 1, 1, 0, 0, NULL)
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (17, 14, 1, NULL, 2, N'Accnt-Base-QReport', NULL, 1, 1, 0, 0, NULL)
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (18, 15, 1, NULL, 2, N'Accnt-Operation-QReport', NULL, 1, 1, 0, 0, NULL)
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (19, 16, 1, NULL, 2, N'Accnt-Report-QReport', NULL, 1, 1, 0, 0, NULL)
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (20, 15, 1, NULL, 2, N'Voucher-Printing', NULL, 1, 1, 0, 0, NULL)
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (21, 17, 1, 9, 2, N'Fiscal-Periods', N'fperiods', 0, 1, 1, 1, NULL)
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (22, 17, 1, 1, 2, N'Accounts', N'accounts', 0, 1, 1, 1, NULL)
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (23, 17, 1, 6, 2, N'Detail-Accounts', N'faccounts', 0, 1, 1, 1, NULL)
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (24, 17, 1, 7, 2, N'Cost-Centers', N'ccenters', 0, 1, 1, 1, NULL)
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (25, 17, 1, 8, 2, N'Projects', N'projects', 0, 1, 1, 1, NULL)
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (26, 17, 1, 12, 2, N'Account-Groups', N'accgroups', 0, 1, 1, 1, NULL)
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (27, 19, 1, 15, 2, N'Journal-ByDate-ByRow', N'reports/journal/by-date/by-row', 0, 1, 1, 1, NULL)
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (28, 19, 1, 16, 2, N'Journal-ByDate-ByRow-Detail', N'reports/journal/by-date/by-row-detail', 0, 1, 1, 1, NULL)
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (29, 19, 1, 17, 2, N'Journal-ByDate-ByLedger', N'reports/journal/by-date/by-ledger', 0, 1, 1, 1, NULL)
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (30, 19, 1, 18, 2, N'Journal-ByDate-BySubsidiary', N'reports/journal/by-date/by-subsid', 0, 1, 1, 1, NULL)
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (31, 19, 1, 19, 2, N'Journal-ByDate-LedgerSummary', N'reports/journal/by-date/summary', 0, 1, 1, 1, NULL)
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (32, 19, 1, 20, 2, N'Journal-ByDate-LedgerSummary-ByDate', N'reports/journal/by-date/sum-by-date', 0, 1, 1, 1, NULL)
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (33, 19, 1, 21, 2, N'Journal-ByDate-LedgerSummary-ByMonth', N'reports/journal/by-date/sum-by-month', 0, 1, 1, 1, NULL)
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (34, 19, 1, 22, 2, N'Journal-ByNo-ByRow', N'reports/journal/by-no/by-row', 0, 1, 1, 1, NULL)
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (35, 19, 1, 23, 2, N'Journal-ByNo-ByRow-Detail', N'reports/journal/by-no/by-row-detail', 0, 1, 1, 1, NULL)
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (36, 19, 1, 24, 2, N'Journal-ByNo-ByLedger', N'reports/journal/by-no/by-ledger', 0, 1, 1, 1, NULL)
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (37, 19, 1, 25, 2, N'Journal-ByNo-BySubsidiary', N'reports/journal/by-no/by-subsid', 0, 1, 1, 1, NULL)
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (38, 19, 1, 26, 2, N'Journal-ByNo-LedgerSummary', N'reports/journal/by-no/summary', 0, 1, 1, 1, NULL)
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (40, 20, 1, 2, 2, N'Voucher-Std-Form', N'reports/finance/voucher-by-no/{0}/std-form', 0, 1, 1, 0, NULL)
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (41, 20, 1, 2, 2, N'Voucher-Std-Form-Detail', N'reports/finance/voucher-by-no/{0}/std-form-detail', 0, 1, 0, 0, NULL)
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (42, NULL, 1, NULL, 1, N'Report-QReport-Manage', NULL, 1, 1, 0, 0, NULL)
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (43, 42, 1, NULL, 1, N'QReport-Design-Template', NULL, 0, 0, 1, 0, NULL)
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (46, 19, 1, 32, 2, N'TestBalance2Column', NULL, 0, 1, 1, 1, NULL)
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (47, 19, 1, 33, 2, N'TestBalance4Column', NULL, 0, 1, 1, 1, NULL)
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (48, 19, 1, 34, 2, N'TestBalance6Column', NULL, 0, 1, 1, 1, NULL)
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (49, 19, 1, 35, 2, N'TestBalance8Column', NULL, 0, 1, 1, 1, NULL)
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (50, 19, 1, 43, 2, N'', NULL, 0, 1, 1, 1, NULL)
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (51, 19, 1, 44, 2, N'', NULL, 0, 1, 1, 1, NULL)
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (52, 19, 1, 45, 2, N'', NULL, 0, 1, 1, 1, NULL)
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (53, 19, 1, 46, 2, N'', NULL, 0, 1, 1, 1, NULL)
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (54, 19, 1, 48, 2, N'', NULL, 0, 1, 1, 1, NULL)
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (55, 19, 1, 49, 2, N'', NULL, 0, 1, 1, 1, NULL)
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (56, 19, 1, 50, 2, N'', NULL, 0, 1, 1, 1, NULL)
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (57, 19, 1, 51, 2, N'', NULL, 0, 1, 1, 1, NULL)
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (58, 19, 1, 53, 2, N'', NULL, 0, 1, 1, 1, NULL)
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (59, 19, 1, 54, 2, N'', NULL, 0, 1, 1, 1, NULL)
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (60, 19, 1, 55, 2, N'', NULL, 0, 1, 1, 1, NULL)
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (61, 19, 1, 56, 2, N'', NULL, 0, 1, 1, 1, NULL)
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (62, 19, 1, 27, 2, N'', NULL, 0, 1, 1, 1, NULL)
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (63, 19, 1, 28, 2, N'', NULL, 0, 1, 1, 1, NULL)
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (64, 19, 1, 29, 2, N'', NULL, 0, 1, 1, 1, NULL)
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (65, 19, 1, 37, 2, N'', NULL, 0, 1, 1, 1, NULL)
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (66, 19, 1, 38, 2, N'', NULL, 0, 1, 1, 1, NULL)
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (67, 19, 1, 39, 2, N'', NULL, 0, 1, 1, 1, NULL)
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (68, 19, 1, 40, 2, N'', NULL, 0, 1, 1, 1, NULL)
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (69, 19, 1, 58, 2, N'', NULL, 0, 1, 1, 1, NULL)
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (70, 6, 1, 61, 1, N'', N'oplog/archive', 0, 1, 1, 1, NULL)
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (71, 6, 1, 59, 1, N'', N'sys-oplog', 0, 1, 1, 1, NULL)
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (72, 6, 1, 60, 1, N'', N'sys-oplog/archive', 0, 1, 1, 1, NULL)
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (73, 6, 1, 13, 1, N'', N'oplog', 0, 1, 1, 1, NULL)
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (74, 17, 1, 30, 2, N'', N'currencies', 0, 1, 1, 1, NULL)
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (75, 18, 1, 31, 2, N'', NULL, 0, 1, 1, 1, NULL)
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (76, 19, 1, 62, 2, N'', NULL, 0, 1, 1, 1, NULL)
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (77, 19, 1, 64, 2, N'', NULL, 0, 1, 1, 1, NULL)
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (78, 19, 1, 65, 2, N'', NULL, 0, 1, 1, 1, NULL)
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (79, 19, 1, 66, 2, N'', NULL, 0, 1, 1, 1, NULL)
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (80, 19, 1, 67, 2, N'BalanceSheet', N'bal-sheet', 0, 1, 1, 1, NULL)
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (81, 20, 1, 2, 2, N'Vouchers', NULL, 0, 1, 1, 1, NULL)
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (82, 20, 1, 2, 2, N'', N'reports/finance/voucher-by-no/{0}/by-detail', 0, 1, 0, 0, NULL)
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (83, 20, 1, 2, 2, N'', N'reports/finance/voucher-by-no/{0}/by-ledger', 0, 1, 0, 0, NULL)
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (84, 20, 1, 2, 2, N'', N'reports/finance/voucher-by-no/{0}/by-subsid', 0, 1, 0, 0, NULL)
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (85, 16, 1, 17, 2, N'Journal-ByDate-ByLedger', N'reports/journal/by-date/by-ledger', 0, 1, 0, 1, NULL)
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (86, 16, 1, 18, 2, N'Journal-ByDate-BySubsidiary', N'reports/journal/by-date/by-subsid', 0, 1, 0, 1, NULL)
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (87, 16, 1, 24, 2, N'Journal-ByNo-ByLedger', N'reports/journal/by-no/by-ledger', 0, 1, 0, 1, NULL)
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (88, 16, 1, 25, 2, N'Journal-ByNo-BySubsidiary', N'reports/journal/by-no/by-subsid', 0, 1, 0, 1, NULL)
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (89, 16, 1, 2, 2, N'Voucher-Summary-By-Date', N'reports/finance/vouchers/sum-by-date', 0, 1, 0, 0, NULL)
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (90, 16, 1, 2, 2, N'Voucher-Summary-By-No', N'reports/finance/vouchers/sum-by-no', 0, 1, 0, 0, NULL)
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (91, 7, 1, 68, 1, N'', N'dashboard/widgets', 0, 1, 0, 1, NULL)
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (92, 7, 1, 68, 1, N'', N'dashboard/widgets/all', 0, 1, 0, 1, NULL)
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (93, NULL, 1, NULL, 3, N'Treasury', NULL, 1, 1, 0, 0, NULL)
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (94, 93, 1, NULL, 3, N'Treasury-Base', NULL, 1, 1, 0, 0, NULL)
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (95, 93, 1, NULL, 3, N'Treasury-Operation', NULL, 1, 1, 0, 0, NULL)
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (96, 93, 1, NULL, 3, N'Treasury-Report', NULL, 1, 1, 0, 0, NULL)
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (97, 94, 1, NULL, 3, N'Treasury-Base-QReport', NULL, 1, 1, 0, 0, NULL)
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (98, 95, 1, NULL, 3, N'Treasury-Operation-QReport', NULL, 1, 1, 0, 0, NULL)
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (99, 96, 1, NULL, 3, N'Treasury-Report-QReport', NULL, 1, 1, 0, 0, NULL)
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (100, 96, 1, 69, 3, N'', N'check-books', 0, 1, 1, 1, NULL)
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (101, 97, 1, 70, 3, N'', N'cash-registers', 0, 1, 1, 1, NULL)
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (102, 99, 1, 72, 3, N'', N'check-book-report', 0, 1, 1, 1, NULL)
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (103, 97, 1, 73, 3, N'', N'source-apps', 0, 1, 1, 1, NULL)
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (104, 98, 1, 75, 3, N'', N'receipts/{0}/payer/articles', 0, 1, 1, 1, NULL)
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (105, 98, 1, 75, 3, N'', N'receipts/{0}/cash/articles', 0, 1, 1, 1, NULL)
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (106, 98, 1, 74, 3, N'', N'payments/{0}/receiver/articles', 0, 1, 1, 1, NULL)
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (107, 98, 1, 74, 3, N'', N'payments/{0}/cash/articles', 0, 1, 1, 1, NULL)
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (108, 20, 1, 42, 2, N'', NULL, 0, 1, 1, 1, NULL)
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (109, 20, 1, 41, 2, N'', NULL, 0, 1, 1, 1, NULL)















































































































































INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (100000, NULL, 1, NULL, 100000, N'ProductScope', NULL, 1, 1, 0, 0, NULL)
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (100001, 100000, 1, NULL, 100000, N'ProductScope-Base', NULL, 1, 1, 0, 0, NULL)
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (100002, 100000, 1, NULL, 100000, N'ProductScope-Operation', NULL, 1, 1, 0, 0, NULL)
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (100003, 100000, 1, NULL, 100000, N'ProductScope-Report', NULL, 1, 1, 0, 0, NULL)
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (100004, 100001, 1, NULL, 100000, N'ProductScope-Base-QReport', NULL, 1, 1, 0, 0, NULL)
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (100005, 100002, 1, NULL, 100000, N'ProductScope-Operation-QReport', NULL, 1, 1, 0, 0, NULL)
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (100006, 100003, 1, NULL, 100000, N'ProductScope-Report-QReport', NULL, 1, 1, 0, 0, NULL)
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (100007, 100001, 1, 100001, 100000, N'', N'brands', 0, 1, 1, 1, NULL)
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (100008, 100001, 1, 100002, 100000, N'', N'units', 0, 1, 1, 1, NULL)
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (100009, 100001, 1, 100003, 100000, N'', N'properties', 0, 1, 1, 1, NULL)
SET IDENTITY_INSERT [Reporting].[Report] OFF

SET IDENTITY_INSERT [Reporting].[LocalReport] ON
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (1, 1, 1, N'Administration')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (2, 2, 1, N'راهبری')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (3, 3, 1, N'Administration')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (4, 4, 1, N'Administration')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (5, 1, 2, N'Base data')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (6, 2, 2, N'اطلاعات پایه')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (7, 3, 2, N'Base data')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (8, 4, 2, N'Base data')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (9, 1, 3, N'Operational data')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (10, 2, 3, N'اطلاعات عملیاتی')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (11, 3, 3, N'Operational data')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (12, 4, 3, N'Operational data')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (13, 1, 4, N'Reports')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (14, 2, 4, N'گزارشات')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (15, 3, 4, N'Reports')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (16, 4, 4, N'Reports')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (17, 1, 5, N'Quick Report')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (18, 2, 5, N'گزارش فوری')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (19, 3, 5, N'Quick Report')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (20, 4, 5, N'Quick Report')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (21, 1, 6, N'Quick Report')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (22, 2, 6, N'گزارش فوری')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (23, 3, 6, N'Quick Report')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (24, 4, 6, N'Quick Report')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (25, 1, 7, N'Quick Report')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (26, 2, 7, N'گزارش فوری')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (27, 3, 7, N'Quick Report')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (28, 4, 7, N'Quick Report')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (29, 1, 8, N'Companies')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (30, 2, 8, N'شرکت ها')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (31, 3, 8, N'Companies')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (32, 4, 8, N'Companies')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (33, 1, 9, N'Branches')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (34, 2, 9, N'شعب سازمانی')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (35, 3, 9, N'Branches')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (36, 4, 9, N'Branches')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (37, 1, 10, N'Users')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (38, 2, 10, N'کاربران')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (39, 3, 10, N'Users')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (40, 4, 10, N'Users')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (41, 1, 11, N'Roles')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (42, 2, 11, N'نقش ها')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (43, 3, 11, N'Roles')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (44, 4, 11, N'Roles')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (49, 1, 13, N'Accounting')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (50, 2, 13, N'حسابداری')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (51, 3, 13, N'Accounting')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (52, 4, 13, N'Accounting')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (53, 1, 14, N'Base data')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (54, 2, 14, N'اطلاعات پایه')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (55, 3, 14, N'Base data')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (56, 4, 14, N'Base data')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (57, 1, 15, N'Operational data')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (58, 2, 15, N'اطلاعات عملیاتی')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (59, 3, 15, N'Operational data')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (60, 4, 15, N'Operational data')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (61, 1, 16, N'Reports')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (62, 2, 16, N'گزارشات')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (63, 3, 16, N'Reports')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (64, 4, 16, N'Reports')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (65, 1, 17, N'Quick Report')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (66, 2, 17, N'گزارش فوری')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (67, 3, 17, N'Quick Report')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (68, 4, 17, N'Quick Report')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (69, 1, 18, N'Quick Report')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (70, 2, 18, N'گزارش فوری')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (71, 3, 18, N'Quick Report')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (72, 4, 18, N'Quick Report')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (73, 1, 19, N'Quick Report')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (74, 2, 19, N'گزارش فوری')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (75, 3, 19, N'Quick Report')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (76, 4, 19, N'Quick Report')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (77, 1, 20, N'Voucher Printing')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (78, 2, 20, N'چاپ سند')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (79, 3, 20, N'Voucher Printing')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (80, 4, 20, N'Voucher Printing')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (81, 1, 21, N'Fiscal periods')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (82, 2, 21, N'دوره های مالی')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (83, 3, 21, N'Fiscal periods')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (84, 4, 21, N'Fiscal periods')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (85, 1, 22, N'Accounts')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (86, 2, 22, N'سرفصل های حسابداری')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (87, 3, 22, N'Accounts')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (88, 4, 22, N'Accounts')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (89, 1, 23, N'Detail accounts')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (90, 2, 23, N'تفصیلی های شناور')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (91, 3, 23, N'Detail accounts')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (92, 4, 23, N'Detail accounts')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (93, 1, 24, N'Cost centers')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (94, 2, 24, N'مراکز هزینه')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (95, 3, 24, N'Cost centers')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (96, 4, 24, N'Cost centers')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (97, 1, 25, N'Projects')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (98, 2, 25, N'پروژه ها')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (99, 3, 25, N'Projects')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (100, 4, 25, N'Projects')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (101, 1, 26, N'Account groups')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (102, 2, 26, N'گروه های حساب')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (103, 3, 26, N'Account groups')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (104, 4, 26, N'Account groups')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (105, 1, 27, N'Journal, by date, by row')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (106, 2, 27, N'دفتر روزنامه، بر حسب تاریخ، مطابق ردیف های سند')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (107, 3, 27, N'Journal, by date, by row')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (108, 4, 27, N'Journal, by date, by row')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (109, 1, 28, N'Journal, by date, by row with details')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (110, 2, 28, N'دفتر روزنامه، بر حسب تاریخ، مطابق ردیف های سند با سطوح شناور')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (111, 3, 28, N'Journal, by date, by row with details')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (112, 4, 28, N'Journal, by date, by row with details')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (113, 1, 29, N'Journal, by date, by ledger')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (114, 2, 29, N'دفتر روزنامه، بر حسب تاریخ، در سطح کل')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (115, 3, 29, N'Journal, by date, by ledger')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (116, 4, 29, N'Journal, by date, by ledger')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (117, 1, 30, N'Journal, by date, by subsidiary')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (118, 2, 30, N'دفتر روزنامه، بر حسب تاریخ، در سطح معین')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (119, 3, 30, N'Journal, by date, by subsidiary')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (120, 4, 30, N'Journal, by date, by subsidiary')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (121, 1, 31, N'Journal, by date, ledger summary')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (122, 2, 31, N'دفتر روزنامه، بر حسب تاریخ، سند خلاصه')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (123, 3, 31, N'Journal, by date, ledger summary')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (124, 4, 31, N'Journal, by date, ledger summary')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (125, 1, 32, N'Journal, by date, summary by date')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (126, 2, 32, N'دفتر روزنامه، بر حسب تاریخ، سند خلاصه به تفکیک تاریخ')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (127, 3, 32, N'Journal, by date, summary by date')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (128, 4, 32, N'Journal, by date, summary by date')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (129, 1, 33, N'Journal, by date, summary by month')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (130, 2, 33, N'دفتر روزنامه، بر حسب تاریخ، سند خلاصه به تفکیک ماه')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (131, 3, 33, N'Journal, by date, summary by month')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (132, 4, 33, N'Journal, by date, summary by month')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (133, 1, 34, N'Journal, by number, by row')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (134, 2, 34, N'دفتر روزنامه، بر حسب شماره سند، مطابق ردیف های سند')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (135, 3, 34, N'Journal, by number, by row')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (136, 4, 34, N'Journal, by number, by row')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (137, 1, 35, N'Journal, by number, by row with details')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (138, 2, 35, N'دفتر روزنامه، بر حسب شماره سند، مطابق ردیف های سند با سطوح شناور')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (139, 3, 35, N'Journal, by number, by row with details')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (140, 4, 35, N'Journal, by number, by row with details')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (141, 1, 36, N'Journal, by number, by ledger')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (142, 2, 36, N'دفتر روزنامه، بر حسب شماره سند، در سطح کل')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (143, 3, 36, N'Journal, by number, by ledger')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (144, 4, 36, N'Journal, by number, by ledger')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (145, 1, 37, N'Journal, by number, by subsidiary')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (146, 2, 37, N'دفتر روزنامه، بر حسب شماره سند، در سطح معین')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (147, 3, 37, N'Journal, by number, by subsidiary')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (148, 4, 37, N'Journal, by number, by subsidiary')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (149, 1, 38, N'Journal, by number, ledger summary')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (150, 2, 38, N'دفتر روزنامه، بر حسب شماره سند، سند خلاصه')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (151, 3, 38, N'Journal, by number, ledger summary')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (152, 4, 38, N'Journal, by number, ledger summary')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (157, 1, 40, N'Voucher, standard format')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (158, 2, 40, N'فرم مرسوم سند')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (159, 3, 40, N'Voucher, standard format')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (160, 4, 40, N'Voucher, standard format')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (161, 1, 41, N'Voucher, standard format, with detail')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (162, 2, 41, N'فرم مرسوم سند - با سطوح شناور')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (163, 3, 41, N'Voucher, standard format, with detail')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (164, 4, 41, N'Voucher, standard format, with detail')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (165, 1, 42, N'Manage quick reports')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (166, 2, 42, N'مدیریت گزارشات فوری')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (167, 3, 42, N'Manage quick reports')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (168, 4, 42, N'Manage quick reports')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (169, 1, 43, N'Design template')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (170, 2, 43, N'طراحی قالب')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (171, 3, 43, N'Design template')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (172, 4, 43, N'Design template')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (173, 1, 46, N'Test balance 2 columns')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (174, 2, 46, N'تراز آزمایشی ۲ ستونی')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (175, 3, 46, N'Test balance 2 columns')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (176, 4, 46, N'Test balance 2 columns')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (177, 1, 47, N'Test balance 4 columns')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (178, 2, 47, N'تراز آزمایشی ۴ ستونی')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (179, 3, 47, N'Test balance 4 columns')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (180, 4, 47, N'Test balance 4 columns')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (181, 1, 48, N'Test balance 6 columns')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (182, 2, 48, N'تراز آزمایشی ۶ ستونی')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (183, 3, 48, N'Test balance 6 columns')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (184, 4, 48, N'Test balance 6 columns')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (185, 1, 49, N'Test balance 8 columns')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (186, 2, 49, N'تراز آزمایشی ۸ ستونی')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (187, 3, 49, N'Test balance 8 columns')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (188, 4, 49, N'Test balance 8 columns')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (189, 1, 50, N'Detail account turnover/balance - 2 column')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (190, 2, 50, N'گردش و مانده تفصیلی شناور 2 ستونی')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (191, 1, 51, N'Detail account turnover/balance - 4 column')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (192, 2, 51, N'گردش و مانده تفصیلی شناور 4 ستونی')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (193, 1, 52, N'Detail account turnover/balance - 6 column')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (194, 2, 52, N'گردش و مانده تفصیلی شناور 6 ستونی')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (195, 1, 53, N'Detail account turnover/balance - 8 column')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (196, 2, 53, N'گردش و مانده تفصیلی شناور 8 ستونی')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (197, 1, 54, N'Cost center turnover/balance - 2 column')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (198, 2, 54, N'گردش و مانده مرکز هزینه 2 ستونی')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (199, 1, 55, N'Cost center turnover/balance - 4 column')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (200, 2, 55, N'گردش و مانده مرکز هزینه 4 ستونی')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (201, 1, 56, N'Cost center turnover/balance - 6 column')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (202, 2, 56, N'گردش و مانده مرکز هزینه 6 ستونی')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (203, 1, 57, N'Cost center turnover/balance - 8 column')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (204, 2, 57, N'گردش و مانده مرکز هزینه 8 ستونی')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (205, 1, 58, N'Project turnover/balance - 2 column')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (206, 2, 58, N'گردش و مانده پروژه 2 ستونی')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (207, 1, 59, N'Project turnover/balance - 4 column')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (208, 2, 59, N'گردش و مانده پروژه 4 ستونی')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (209, 1, 60, N'Project turnover/balance - 6 column')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (210, 2, 60, N'گردش و مانده پروژه 6 ستونی')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (211, 1, 61, N'Project turnover/balance - 8 column')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (212, 2, 61, N'گردش و مانده پروژه 8 ستونی')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (213, 1, 62, N'Account Book')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (214, 2, 62, N'دفتر حساب')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (215, 1, 63, N'Account Book')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (216, 2, 63, N'دفتر حساب')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (217, 1, 64, N'Account Book')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (218, 2, 64, N'ذفتر حساب')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (219, 1, 65, N'Currency Book')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (220, 2, 65, N'دفتر عملیات ارزی')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (221, 1, 66, N'Currency Book')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (222, 2, 66, N'دفتر عملیات ارزی')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (223, 1, 67, N'Currency Book')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (224, 2, 67, N'دفتر عملیات ارزی')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (225, 1, 68, N'Currency Book')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (226, 2, 68, N'دفتر عملیات ارزی')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (227, 1, 69, N'Balance by account')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (228, 2, 69, N'مانده به تفکیک حساب')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (229, 1, 70, N'Archived operation logs')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (230, 2, 70, N'رویدادهای عملیاتی بایگانی شده')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (231, 1, 71, N'Active system logs')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (232, 2, 71, N'رویدادهای سیستمی فعال')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (233, 1, 72, N'Archived system logs')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (234, 2, 72, N'رویدادهای سیستمی بایگانی شده')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (235, 1, 73, N'Active operation logs')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (236, 2, 73, N'رویدادهای عملیاتی فعال')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (237, 1, 74, N'Currencies')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (238, 2, 74, N'ارزها')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (239, 1, 75, N'Currency rates')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (240, 2, 75, N'نرخ های ارز')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (241, 1, 76, N'Profit-Loss')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (242, 2, 76, N'سود و زیان')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (243, 1, 77, N'Profit-Loss')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (244, 2, 77, N'سود و زیان')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (249, 1, 80, N'BalanceSheet')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (250, 2, 80, N'ترازنامه')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (251, 1, 81, N'Vouchers')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (252, 2, 81, N'اسناد مالی')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (255, 1, 82, N'Simple - by detail level')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (256, 2, 82, N'ساده - در سطح تفصیلی')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (257, 1, 83, N'Aggregate - by ledger level')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (258, 2, 83, N'مرکب - در سطح کل')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (259, 1, 84, N'Aggregate - by subsidiary level')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (260, 2, 84, N'مرکب - در سطح معین')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (261, 1, 85, N'Journal in Ledger Level - By Date')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (262, 2, 85, N'دفتر روزنامه در سطح کل - بر اساس تاریخ')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (263, 1, 86, N'Journal in Subsidiary Level - By Date')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (264, 2, 86, N'دفتر روزنامه در سطح معین - بر اساس تاریخ')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (265, 1, 87, N'Journal in Ledger Level - By Number')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (266, 2, 87, N'دفتر روزنامه در سطح کل - بر اساس شماره سند')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (267, 1, 88, N'Journal in Subsidiary Level - By Number')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (268, 2, 88, N'دفتر روزنامه در سطح معین - بر اساس شماره سند')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (269, 1, 89, N'Accounting Voucher Summary - By Date')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (270, 2, 89, N'خلاصه اسناد حسابداری - بر اساس تاریخ')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (271, 1, 90, N'Accounting Voucher Summary - By Voucher No')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (272, 2, 90, N'خلاصه اسناد حسابداری - بر اساس شماره سند')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (273, 1, 91, N'My Widgets')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (274, 2, 91, N'ویجت های من')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (275, 1, 92, N'All Widgets')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (276, 2, 92, N'همه ویجت ها')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (279, 1, 94, N'Base Data')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (280, 2, 94, N'اطلاعات پایه')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (281, 1, 95, N'Operational Data')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (282, 2, 95, N'اطلاعات عملیاتی')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (283, 1, 96, N'Reports')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (284, 2, 96, N'گزارشات')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (285, 1, 97, N'Quick Report')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (286, 2, 97, N'گزارش فوری')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (287, 1, 98, N'Quick Report')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (288, 2, 98, N'گزارش فوری')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (289, 1, 99, N'Quick Report')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (290, 2, 99, N'گزارش فوری')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (291, 1, 100, N'Check Book')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (292, 2, 100, N'دسته چک')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (293, 1, 101, N'Cash Register List')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (294, 2, 101, N'فهرست صندوق‌های اسناد')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (295, 1, 102, N'Check Book Report')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (296, 2, 102, N'دفتر دسته‌چک')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (297, 1, 103, N'Source and Application List')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (298, 2, 103, N'فهرست منابع و مصارف')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (299, 1, 104, N'Payers List')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (300, 2, 104, N'لیست پرداخت کنندگان')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (301, 1, 105, N'Cash Accounts List')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (302, 2, 105, N'لیست حساب‌های نقدی')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (303, 1, 106, N'Recipients List')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (304, 2, 106, N'لیست دریافت کنندگان')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (305, 1, 107, N'Cash Accounts List')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (306, 2, 107, N'لیست حساب‌های نقدی')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (307, 1, 108, N'Voucher Lines')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (308, 2, 108, N'آرتیکل‌های سند')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (309, 1, 109, N'Missing Voucher Numbers')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (310, 2, 109, N'شماره سندهای مفقود')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (100000, 1, 100000, N'ProductScope')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (100001, 2, 100000, N'محصولات')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (100002, 1, 100001, N'Base Data')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (100003, 2, 100001, N'اطلاعات پایه')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (100004, 1, 100002, N'Operational Data')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (100005, 2, 100002, N'اطلاعات عملیاتی')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (100006, 1, 100003, N'Reports')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (100007, 2, 100003, N'گزارشات')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (100008, 1, 100004, N'Quick Report')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (100009, 2, 100004, N'گزارش فوری')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (100010, 1, 100005, N'Quick Report')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (100011, 2, 100005, N'گزارش فوری')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (100012, 1, 100006, N'Quick Report')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (100013, 2, 100006, N'گزارش فوری')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (100014, 1, 100007, N'Brnads List')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (100015, 2, 100007, N'فهرست برندها')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (100016, 1, 100008, N'Unit list')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (100017, 2, 100008, N'لیست واحدها')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (100018, 1, 100009, N'Properties List')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (100019, 2, 100009, N'لیست ویژگی ها')
SET IDENTITY_INSERT [Reporting].[LocalReport] OFF

SET IDENTITY_INSERT [Reporting].[Parameter] ON 

GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (1, 27, N'fromDate', N'from', N'EQ', N'System.DateTime', N'DatePicker', N'FromDate', NULL, NULL, NULL, N'FromDate', N'84732780-f776-403e-8ed3-e31e13610dd3', CAST(N'2020-10-25T15:42:02.040' AS DateTime), N'QueryString')
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (2, 27, N'toDate', N'to', N'EQ', N'System.DateTime', N'DatePicker', N'ToDate', NULL, NULL, NULL, N'ToDate', N'fe82f62e-e911-4235-8e6a-faba397fd027', CAST(N'2020-10-25T15:42:02.040' AS DateTime), N'QueryString')
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (3, 28, N'fromDate', N'from', N'EQ', N'System.DateTime', N'DatePicker', N'FromDate', NULL, NULL, NULL, N'FromDate', N'f6874908-874c-49ae-b4de-16cdd227582d', CAST(N'2020-10-25T15:42:02.040' AS DateTime), N'QueryString')
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (4, 28, N'toDate', N'to', N'EQ', N'System.DateTime', N'DatePicker', N'ToDate', NULL, NULL, NULL, N'ToDate', N'c321285b-1c15-44f1-94f9-dfc29418062e', CAST(N'2020-10-25T15:42:02.053' AS DateTime), N'QueryString')
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (5, 29, N'fromDate', N'from', N'EQ', N'System.DateTime', N'DatePicker', N'FromDate', NULL, NULL, NULL, N'FromDate', N'72c50d03-59cc-4540-be4b-da6531318ca6', CAST(N'2020-10-25T15:42:02.057' AS DateTime), N'QueryString')
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (6, 29, N'toDate', N'to', N'EQ', N'System.DateTime', N'DatePicker', N'ToDate', NULL, NULL, NULL, N'ToDate', N'898c7ace-e2dc-45ac-8984-4b2ff9bcda7a', CAST(N'2020-10-25T15:42:02.057' AS DateTime), N'QueryString')
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (7, 30, N'fromDate', N'from', N'EQ', N'System.DateTime', N'DatePicker', N'FromDate', NULL, NULL, NULL, N'FromDate', N'ef60f87e-2740-4c0f-b36a-a487cfb5a3a3', CAST(N'2020-10-25T15:42:02.057' AS DateTime), N'QueryString')
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (8, 30, N'toDate', N'to', N'EQ', N'System.DateTime', N'DatePicker', N'ToDate', NULL, NULL, NULL, N'ToDate', N'eb69507d-b6d2-4327-81e3-7d711cf675be', CAST(N'2020-10-25T15:42:02.057' AS DateTime), N'QueryString')
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (9, 31, N'fromDate', N'from', N'EQ', N'System.DateTime', N'DatePicker', N'FromDate', NULL, NULL, NULL, N'FromDate', N'ccc37b76-145e-4057-8cd5-c0e1484b3a10', CAST(N'2020-10-25T15:42:02.060' AS DateTime), N'QueryString')
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (10, 31, N'toDate', N'to', N'EQ', N'System.DateTime', N'DatePicker', N'ToDate', NULL, NULL, NULL, N'ToDate', N'18b1a8ac-09f2-48b4-9190-7d6d748c4018', CAST(N'2020-10-25T15:42:02.060' AS DateTime), N'QueryString')
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (11, 32, N'fromDate', N'from', N'EQ', N'System.DateTime', N'DatePicker', N'FromDate', NULL, NULL, NULL, N'FromDate', N'888350e5-01d5-4fc0-a22a-758adada5ea3', CAST(N'2020-10-25T15:42:02.060' AS DateTime), N'QueryString')
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (12, 32, N'toDate', N'to', N'EQ', N'System.DateTime', N'DatePicker', N'ToDate', NULL, NULL, NULL, N'ToDate', N'aa0a98e4-ed63-4c1d-839c-3db749ce7ec4', CAST(N'2020-10-25T15:42:02.060' AS DateTime), N'QueryString')
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (13, 33, N'fromNo', N'from', N'EQ', N'System.Int32', N'NumberBox', N'FromNo', NULL, NULL, NULL, N'FromNo', N'ec00fcfa-5bf6-4f12-9f3a-68ee5439d73d', CAST(N'2020-10-25T15:42:02.060' AS DateTime), N'QueryString')
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (14, 33, N'toNo', N'to', N'EQ', N'System.Int32', N'NumberBox', N'ToNo', NULL, NULL, NULL, N'ToNo', N'952696f4-49cd-4637-bec3-37eae53e0c78', CAST(N'2020-10-25T15:42:02.060' AS DateTime), N'QueryString')
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (15, 34, N'fromNo', N'from', N'EQ', N'System.Int32', N'NumberBox', N'FromNo', NULL, NULL, NULL, N'FromNo', N'efaa89c3-d6b7-4a5b-870d-10bed60212b1', CAST(N'2020-10-25T15:42:02.063' AS DateTime), N'QueryString')
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (16, 34, N'toNo', N'to', N'EQ', N'System.Int32', N'NumberBox', N'ToNo', NULL, NULL, NULL, N'ToNo', N'00216b6f-560a-4fa7-9f4b-8f4990f19ea8', CAST(N'2020-10-25T15:42:02.063' AS DateTime), N'QueryString')
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (17, 35, N'fromNo', N'from', N'EQ', N'System.Int32', N'NumberBox', N'FromNo', NULL, NULL, NULL, N'FromNo', N'b9ecb187-06e0-4785-b224-6f3103fb3898', CAST(N'2020-10-25T15:42:02.063' AS DateTime), N'QueryString')
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (18, 35, N'toNo', N'to', N'EQ', N'System.Int32', N'NumberBox', N'ToNo', NULL, NULL, NULL, N'ToNo', N'5e5eb692-c608-467f-bf0e-4810b29cff7c', CAST(N'2020-10-25T15:42:02.063' AS DateTime), N'QueryString')
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (19, 36, N'fromNo', N'from', N'EQ', N'System.Int32', N'NumberBox', N'FromNo', NULL, NULL, NULL, N'FromNo', N'93f0375e-2c16-404e-b0e7-e443b01bcb29', CAST(N'2020-10-25T15:42:02.067' AS DateTime), N'QueryString')
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (20, 36, N'toNo', N'to', N'EQ', N'System.Int32', N'NumberBox', N'ToNo', NULL, NULL, NULL, N'ToNo', N'55e07ff1-be8e-4f82-930c-938ca0060924', CAST(N'2020-10-25T15:42:02.067' AS DateTime), N'QueryString')
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (21, 37, N'fromNo', N'from', N'EQ', N'System.Int32', N'NumberBox', N'FromNo', NULL, NULL, NULL, N'FromNo', N'f66e70ac-0adf-4bb9-890c-5d7792b5823d', CAST(N'2020-10-25T15:42:02.067' AS DateTime), N'QueryString')
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (22, 37, N'toNo', N'to', N'EQ', N'System.Int32', N'NumberBox', N'ToNo', NULL, NULL, NULL, N'ToNo', N'797e64e5-7a8c-4f35-837f-b4b5fd89f868', CAST(N'2020-10-25T15:42:02.067' AS DateTime), N'QueryString')
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (23, 38, N'fromDate', N'from', N'EQ', N'System.DateTime', N'DatePicker', N'FromDate', NULL, NULL, NULL, N'FromDate', N'50b85b6f-6df4-4367-b0aa-5743a65e8372', CAST(N'2020-10-25T15:42:02.067' AS DateTime), N'QueryString')
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (24, 38, N'toDate', N'to', N'EQ', N'System.DateTime', N'DatePicker', N'ToDate', NULL, NULL, NULL, N'ToDate', N'5b22b731-a0c2-4f8c-aff7-5b6ea83b3ded', CAST(N'2020-10-25T15:42:02.070' AS DateTime), N'QueryString')
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (42, 40, N'no', N'no', N'EQ', N'System.Int32', N'NumberBox', N'VoucherNo', NULL, NULL, NULL, N'VoucherNo', N'bd82ec0d-9c10-4e32-ab9c-46027788ef92', CAST(N'2020-10-25T15:42:02.090' AS DateTime), N'Route')
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (43, 46, N'fromDate', N'from', N'EQ', N'System.DateTime', N'TextBox', N'FromDate', NULL, NULL, NULL, N'FromDate', N'5512defe-4a18-4ce9-938d-70c0d5947627', CAST(N'2020-10-25T15:42:02.090' AS DateTime), N'QueryString')
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (44, 46, N'toDate', N'to', N'EQ', N'System.DateTime', N'TextBox', N'ToDate', NULL, NULL, NULL, N'ToDate', N'11fc43dc-35cb-4ca3-883c-b851e7a96369', CAST(N'2020-10-25T15:42:02.090' AS DateTime), N'QueryString')
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (45, 46, N'fromNo', N'from', N'EQ', N'System.Int32', N'TextBox', N'FromNo', NULL, NULL, NULL, N'FromNo', N'7e2132f2-c1af-48bf-a527-a0cadc06a7a4', CAST(N'2020-10-25T15:42:02.090' AS DateTime), N'QueryString')
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (46, 46, N'toNo', N'to', N'EQ', N'System.Int32', N'TextBox', N'ToNo', NULL, NULL, NULL, N'ToNo', N'a199be92-9258-43c1-88a9-282c11afcf83', CAST(N'2020-10-25T15:42:02.090' AS DateTime), N'QueryString')
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (48, 47, N'fromDate', N'from', N'EQ', N'System.DateTime', N'TextBox', N'FromDate', NULL, NULL, NULL, N'FromDate', N'3df0a65d-d678-4990-8ecd-ee671202d211', CAST(N'2020-10-25T15:42:02.117' AS DateTime), N'QueryString')
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (49, 47, N'toDate', N'to', N'EQ', N'System.DateTime', N'TextBox', N'ToDate', NULL, NULL, NULL, N'ToDate', N'1eb88cbe-6d53-4e60-9e1f-0eb661a3fcf6', CAST(N'2020-10-25T15:42:02.117' AS DateTime), N'QueryString')
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (50, 47, N'fromNo', N'from', N'EQ', N'System.Int32', N'TextBox', N'FromNo', NULL, NULL, NULL, N'FromNo', N'7799a459-7178-496f-b87f-1056ed78cdad', CAST(N'2020-10-25T15:42:02.117' AS DateTime), N'QueryString')
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (51, 47, N'toNo', N'to', N'EQ', N'System.Int32', N'TextBox', N'ToNo', NULL, NULL, NULL, N'ToNo', N'57ffe553-bdaa-45b0-9a90-cb01f33d2f69', CAST(N'2020-10-25T15:42:02.117' AS DateTime), N'QueryString')
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (53, 48, N'fromDate', N'from', N'EQ', N'System.DateTime', N'TextBox', N'FromDate', NULL, NULL, NULL, N'FromDate', N'f2c6a59f-e869-4837-a90c-8864e22bf574', CAST(N'2020-10-25T15:42:02.120' AS DateTime), N'QueryString')
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (54, 48, N'toDate', N'to', N'EQ', N'System.DateTime', N'TextBox', N'ToDate', NULL, NULL, NULL, N'ToDate', N'bf2aa88a-112a-4e7b-b6bb-5e208ffa156c', CAST(N'2020-10-25T15:42:02.120' AS DateTime), N'QueryString')
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (55, 48, N'fromNo', N'from', N'EQ', N'System.Int32', N'TextBox', N'FromNo', NULL, NULL, NULL, N'FromNo', N'a83cee27-1b2f-4a31-a097-0299ad828e35', CAST(N'2020-10-25T15:42:02.120' AS DateTime), N'QueryString')
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (56, 48, N'toNo', N'to', N'EQ', N'System.Int32', N'TextBox', N'ToNo', NULL, NULL, NULL, N'ToNo', N'21b5b77b-34a4-4bfc-b414-1f2ff1c1d008', CAST(N'2020-10-25T15:42:02.120' AS DateTime), N'QueryString')
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (58, 49, N'fromDate', N'from', N'EQ', N'System.DateTime', N'TextBox', N'FromDate', NULL, NULL, NULL, N'FromDate', N'36c7abbb-909c-482b-ba9c-04f3a2865386', CAST(N'2020-10-25T15:42:02.120' AS DateTime), N'QueryString')
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (59, 49, N'toDate', N'to', N'EQ', N'System.DateTime', N'TextBox', N'ToDate', NULL, NULL, NULL, N'ToDate', N'22ae306d-eeb8-4b5c-916c-3dc893411cd9', CAST(N'2020-10-25T15:42:02.123' AS DateTime), N'QueryString')
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (60, 49, N'fromNo', N'from', N'EQ', N'System.Int32', N'TextBox', N'FromNo', NULL, NULL, NULL, N'FromNo', N'27517b43-b9ef-4d45-a978-4ce5e98e4474', CAST(N'2020-10-25T15:42:02.123' AS DateTime), N'QueryString')
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (61, 49, N'toNo', N'to', N'EQ', N'System.Int32', N'TextBox', N'ToNo', NULL, NULL, NULL, N'ToNo', N'17e9538a-f44a-4727-863f-4cf1efda5c5a', CAST(N'2020-10-25T15:42:02.123' AS DateTime), N'QueryString')
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (63, 50, N'fromDate', N'from', N'EQ', N'System.DateTime', N'TextBox', N'FromDate', NULL, NULL, NULL, N'FromDate', N'9923e064-7905-40ba-93e9-55d3eacd4912', CAST(N'2020-10-25T15:45:55.800' AS DateTime), NULL)
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (64, 50, N'toDate', N'to', N'EQ', N'System.DateTime', N'TextBox', N'ToDate', NULL, NULL, NULL, N'ToDate', N'f7275c15-a72e-4f58-9b80-f8f3fcf02773', CAST(N'2020-10-25T15:45:55.800' AS DateTime), NULL)
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (65, 50, N'fromNo', N'from', N'EQ', N'System.Int32', N'TextBox', N'FromNo', NULL, NULL, NULL, N'FromNo', N'cf037b90-4e08-468d-ba45-dbe2ad63cd20', CAST(N'2020-10-25T15:45:55.803' AS DateTime), NULL)
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (66, 50, N'toNo', N'to', N'EQ', N'System.Int32', N'TextBox', N'ToNo', NULL, NULL, NULL, N'ToNo', N'997f82d4-8c43-40d6-bab1-6f09a6b720dd', CAST(N'2020-10-25T15:45:55.807' AS DateTime), NULL)
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (68, 51, N'fromDate', N'from', N'EQ', N'System.DateTime', N'TextBox', N'FromDate', NULL, NULL, NULL, N'FromDate', N'1111d526-70bb-479c-805b-3414a937c741', CAST(N'2020-10-25T15:45:55.810' AS DateTime), NULL)
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (69, 51, N'toDate', N'to', N'EQ', N'System.DateTime', N'TextBox', N'ToDate', NULL, NULL, NULL, N'ToDate', N'6258bdbb-e6ee-4905-a946-3b4ad91ae395', CAST(N'2020-10-25T15:45:55.810' AS DateTime), NULL)
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (70, 51, N'fromNo', N'from', N'EQ', N'System.Int32', N'TextBox', N'FromNo', NULL, NULL, NULL, N'FromNo', N'7581d672-a482-4be4-b6a8-314ef0432f27', CAST(N'2020-10-25T15:45:55.813' AS DateTime), NULL)
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (71, 51, N'toNo', N'to', N'EQ', N'System.Int32', N'TextBox', N'ToNo', NULL, NULL, NULL, N'ToNo', N'ce613fa4-22dd-4657-9c36-f7e3aa92d419', CAST(N'2020-10-25T15:45:55.813' AS DateTime), NULL)
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (73, 52, N'fromDate', N'from', N'EQ', N'System.DateTime', N'TextBox', N'FromDate', NULL, NULL, NULL, N'FromDate', N'd4f519e3-ae51-47f8-88df-086f0e01a826', CAST(N'2020-10-25T15:45:55.817' AS DateTime), NULL)
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (74, 52, N'toDate', N'to', N'EQ', N'System.DateTime', N'TextBox', N'ToDate', NULL, NULL, NULL, N'ToDate', N'f93969ab-00ac-4a96-8312-9eff293c88b7', CAST(N'2020-10-25T15:45:55.820' AS DateTime), NULL)
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (75, 52, N'fromNo', N'from', N'EQ', N'System.Int32', N'TextBox', N'FromNo', NULL, NULL, NULL, N'FromNo', N'c3b01215-3890-496f-b42f-7a65d899e3e9', CAST(N'2020-10-25T15:45:55.820' AS DateTime), NULL)
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (76, 52, N'toNo', N'to', N'EQ', N'System.Int32', N'TextBox', N'ToNo', NULL, NULL, NULL, N'ToNo', N'8724a10f-6237-492c-80a7-baf0e8f04347', CAST(N'2020-10-25T15:45:55.820' AS DateTime), NULL)
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (78, 53, N'fromDate', N'from', N'EQ', N'System.DateTime', N'TextBox', N'FromDate', NULL, NULL, NULL, N'FromDate', N'2a59b010-3922-417c-a056-735005ffcf46', CAST(N'2020-10-25T15:45:55.823' AS DateTime), NULL)
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (79, 53, N'toDate', N'to', N'EQ', N'System.DateTime', N'TextBox', N'ToDate', NULL, NULL, NULL, N'ToDate', N'd20f07be-f1d8-475c-adb4-6f2b4254d654', CAST(N'2020-10-25T15:45:55.827' AS DateTime), NULL)
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (80, 53, N'fromNo', N'from', N'EQ', N'System.Int32', N'TextBox', N'FromNo', NULL, NULL, NULL, N'FromNo', N'cf2b9df4-f0bc-4c34-8edc-f53e87e39dbb', CAST(N'2020-10-25T15:45:55.827' AS DateTime), NULL)
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (81, 53, N'toNo', N'to', N'EQ', N'System.Int32', N'TextBox', N'ToNo', NULL, NULL, NULL, N'ToNo', N'9ee7ad78-83d8-4a4a-8fbe-1bb89f3c2596', CAST(N'2020-10-25T15:45:55.830' AS DateTime), NULL)
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (83, 54, N'fromDate', N'from', N'EQ', N'System.DateTime', N'TextBox', N'FromDate', NULL, NULL, NULL, N'FromDate', N'e206c2e5-cb0f-4450-a51e-176be8506add', CAST(N'2020-10-25T15:45:55.830' AS DateTime), NULL)
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (84, 54, N'toDate', N'to', N'EQ', N'System.DateTime', N'TextBox', N'ToDate', NULL, NULL, NULL, N'ToDate', N'12da3b9a-d05b-4901-87fd-66813cadb1fa', CAST(N'2020-10-25T15:45:55.833' AS DateTime), NULL)
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (85, 54, N'fromNo', N'from', N'EQ', N'System.Int32', N'TextBox', N'FromNo', NULL, NULL, NULL, N'FromNo', N'7982f7e1-f3a6-4f86-aa5a-f0fc9e02e95d', CAST(N'2020-10-25T15:45:55.833' AS DateTime), NULL)
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (86, 54, N'toNo', N'to', N'EQ', N'System.Int32', N'TextBox', N'ToNo', NULL, NULL, NULL, N'ToNo', N'fa52667e-0e87-4f59-9dee-f35568c22cd0', CAST(N'2020-10-25T15:45:55.837' AS DateTime), NULL)
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (88, 55, N'fromDate', N'from', N'EQ', N'System.DateTime', N'TextBox', N'FromDate', NULL, NULL, NULL, N'FromDate', N'125ed154-b199-492f-916b-c2664d1b5e3d', CAST(N'2020-10-25T15:45:55.840' AS DateTime), NULL)
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (89, 55, N'toDate', N'to', N'EQ', N'System.DateTime', N'TextBox', N'ToDate', NULL, NULL, NULL, N'ToDate', N'67ff4255-574d-46fd-9429-4ee14f698805', CAST(N'2020-10-25T15:45:55.840' AS DateTime), NULL)
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (90, 55, N'fromNo', N'from', N'EQ', N'System.Int32', N'TextBox', N'FromNo', NULL, NULL, NULL, N'FromNo', N'd99a5611-538e-4ce5-9980-ebdd5de827c2', CAST(N'2020-10-25T15:45:55.840' AS DateTime), NULL)
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (91, 55, N'toNo', N'to', N'EQ', N'System.Int32', N'TextBox', N'ToNo', NULL, NULL, NULL, N'ToNo', N'0c582466-67e0-4100-9b7e-062e4de690ed', CAST(N'2020-10-25T15:45:55.843' AS DateTime), NULL)
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (93, 56, N'fromDate', N'from', N'EQ', N'System.DateTime', N'TextBox', N'FromDate', NULL, NULL, NULL, N'FromDate', N'97c111d3-4dc5-4896-963c-6c1ea0d10fc4', CAST(N'2020-10-25T15:45:55.847' AS DateTime), NULL)
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (94, 56, N'toDate', N'to', N'EQ', N'System.DateTime', N'TextBox', N'ToDate', NULL, NULL, NULL, N'ToDate', N'df384db3-3c42-4891-94b9-d1560a7e1747', CAST(N'2020-10-25T15:45:55.847' AS DateTime), NULL)
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (95, 56, N'fromNo', N'from', N'EQ', N'System.Int32', N'TextBox', N'FromNo', NULL, NULL, NULL, N'FromNo', N'0ad1e482-57e7-4f6c-a53f-4b1c15ae80ce', CAST(N'2020-10-25T15:45:55.847' AS DateTime), NULL)
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (96, 56, N'toNo', N'to', N'EQ', N'System.Int32', N'TextBox', N'ToNo', NULL, NULL, NULL, N'ToNo', N'3643b73f-570b-4ae5-997d-2f626471afe1', CAST(N'2020-10-25T15:45:55.850' AS DateTime), NULL)
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (98, 57, N'fromDate', N'from', N'EQ', N'System.DateTime', N'TextBox', N'FromDate', NULL, NULL, NULL, N'FromDate', N'42856328-8f76-4cf0-be84-b7652e11c443', CAST(N'2020-10-25T15:45:55.850' AS DateTime), NULL)
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (99, 57, N'toDate', N'to', N'EQ', N'System.DateTime', N'TextBox', N'ToDate', NULL, NULL, NULL, N'ToDate', N'561001e3-e645-4943-aa44-660549053df6', CAST(N'2020-10-25T15:45:55.853' AS DateTime), NULL)
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (100, 57, N'fromNo', N'from', N'EQ', N'System.Int32', N'TextBox', N'FromNo', NULL, NULL, NULL, N'FromNo', N'ff420147-b3ce-4b78-b659-fad13b2558db', CAST(N'2020-10-25T15:45:55.853' AS DateTime), NULL)
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (101, 57, N'toNo', N'to', N'EQ', N'System.Int32', N'TextBox', N'ToNo', NULL, NULL, NULL, N'ToNo', N'29679a94-323f-4002-9430-60b501683fb6', CAST(N'2020-10-25T15:45:55.857' AS DateTime), NULL)
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (103, 58, N'fromDate', N'from', N'EQ', N'System.DateTime', N'TextBox', N'FromDate', NULL, NULL, NULL, N'FromDate', N'8715b9f5-f3b6-48d1-88a9-a20379012ec5', CAST(N'2020-10-25T15:45:55.857' AS DateTime), NULL)
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (104, 58, N'toDate', N'to', N'EQ', N'System.DateTime', N'TextBox', N'ToDate', NULL, NULL, NULL, N'ToDate', N'f90505ac-b7c2-4419-8ff3-0004bad4b758', CAST(N'2020-10-25T15:45:55.860' AS DateTime), NULL)
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (105, 58, N'fromNo', N'from', N'EQ', N'System.Int32', N'TextBox', N'FromNo', NULL, NULL, NULL, N'FromNo', N'e6cda994-564c-4440-b677-543964ffe793', CAST(N'2020-10-25T15:45:55.860' AS DateTime), NULL)
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (106, 58, N'toNo', N'to', N'EQ', N'System.Int32', N'TextBox', N'ToNo', NULL, NULL, NULL, N'ToNo', N'2985882d-4613-4dc1-a818-46c4e74a4a5a', CAST(N'2020-10-25T15:45:55.860' AS DateTime), NULL)
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (108, 59, N'fromDate', N'from', N'EQ', N'System.DateTime', N'TextBox', N'FromDate', NULL, NULL, NULL, N'FromDate', N'9f883d6e-be2d-487c-b7d7-56b4f252bc55', CAST(N'2020-10-25T15:45:55.863' AS DateTime), NULL)
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (109, 59, N'toDate', N'to', N'EQ', N'System.DateTime', N'TextBox', N'ToDate', NULL, NULL, NULL, N'ToDate', N'1e5b3e5f-596b-4299-a20a-8bec1e71ff4e', CAST(N'2020-10-25T15:45:55.863' AS DateTime), NULL)
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (110, 59, N'fromNo', N'from', N'EQ', N'System.Int32', N'TextBox', N'FromNo', NULL, NULL, NULL, N'FromNo', N'a2eda7d9-9894-49be-845a-85eceed626ba', CAST(N'2020-10-25T15:45:55.867' AS DateTime), NULL)
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (111, 59, N'toNo', N'to', N'EQ', N'System.Int32', N'TextBox', N'ToNo', NULL, NULL, NULL, N'ToNo', N'7e811526-91a5-4473-9087-fa97e654d25a', CAST(N'2020-10-25T15:45:55.867' AS DateTime), NULL)
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (113, 60, N'fromDate', N'from', N'EQ', N'System.DateTime', N'TextBox', N'FromDate', NULL, NULL, NULL, N'FromDate', N'e31ad3b2-8865-427a-a5e2-b7e895419731', CAST(N'2020-10-25T15:45:55.870' AS DateTime), NULL)
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (114, 60, N'toDate', N'to', N'EQ', N'System.DateTime', N'TextBox', N'ToDate', NULL, NULL, NULL, N'ToDate', N'c8606466-1d25-43d1-97a3-be2b153ef51a', CAST(N'2020-10-25T15:45:55.873' AS DateTime), NULL)
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (115, 60, N'fromNo', N'from', N'EQ', N'System.Int32', N'TextBox', N'FromNo', NULL, NULL, NULL, N'FromNo', N'd61e11b4-3d2b-4de0-b3ca-46cde7dc4edf', CAST(N'2020-10-25T15:45:55.877' AS DateTime), NULL)
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (116, 60, N'toNo', N'to', N'EQ', N'System.Int32', N'TextBox', N'ToNo', NULL, NULL, NULL, N'ToNo', N'87c087d0-a488-4c61-b994-536e98252a93', CAST(N'2020-10-25T15:45:55.877' AS DateTime), NULL)
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (118, 61, N'fromDate', N'from', N'EQ', N'System.DateTime', N'TextBox', N'FromDate', NULL, NULL, NULL, N'FromDate', N'ed57b093-d280-468d-a696-b487cf0fd157', CAST(N'2020-10-25T15:45:55.880' AS DateTime), NULL)
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (119, 61, N'toDate', N'to', N'EQ', N'System.DateTime', N'TextBox', N'ToDate', NULL, NULL, NULL, N'ToDate', N'30ea1b47-3af8-46ff-8129-419954bdc7c6', CAST(N'2020-10-25T15:45:55.880' AS DateTime), NULL)
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (120, 61, N'fromNo', N'from', N'EQ', N'System.Int32', N'TextBox', N'FromNo', NULL, NULL, NULL, N'FromNo', N'7f01ae23-c02b-475f-b01c-fd80c03a05da', CAST(N'2020-10-25T15:45:55.883' AS DateTime), NULL)
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (121, 61, N'toNo', N'to', N'EQ', N'System.Int32', N'TextBox', N'ToNo', NULL, NULL, NULL, N'ToNo', N'6783385a-2212-4f5c-8384-b5f85656a383', CAST(N'2020-10-25T15:45:55.887' AS DateTime), NULL)
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (123, 62, N'fromDate', N'from', N'EQ', N'System.DateTime', N'TextBox', N'FromDate', NULL, NULL, NULL, N'FromDate', N'43808c19-895e-4043-9260-141d65e41470', CAST(N'2020-11-16T16:33:28.113' AS DateTime), N'QueryString')
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (124, 62, N'toDate', N'to', N'EQ', N'System.DateTime', N'TextBox', N'ToDate', NULL, NULL, NULL, N'ToDate', N'0ea78a3e-a1e0-45c4-8341-f67e7184a40b', CAST(N'2020-11-16T16:33:28.113' AS DateTime), N'QueryString')
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (126, 63, N'fromDate', N'from', N'EQ', N'System.DateTime', N'TextBox', N'FromDate', NULL, NULL, NULL, N'FromDate', N'003cb6c3-b74d-423b-ba7b-4276acfbf763', CAST(N'2020-11-16T16:33:28.117' AS DateTime), N'QueryString')
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (127, 63, N'toDate', N'to', N'EQ', N'System.DateTime', N'TextBox', N'ToDate', NULL, NULL, NULL, N'ToDate', N'8a53e7c9-5189-4488-9d04-f4ee330e86b7', CAST(N'2020-11-16T16:33:28.117' AS DateTime), N'QueryString')
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (129, 64, N'fromDate', N'from', N'EQ', N'System.DateTime', N'TextBox', N'FromDate', NULL, NULL, NULL, N'FromDate', N'0ef97b82-c0a7-4b6c-af31-793c8067506a', CAST(N'2020-11-16T16:33:28.120' AS DateTime), N'QueryString')
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (130, 64, N'toDate', N'to', N'EQ', N'System.DateTime', N'TextBox', N'ToDate', NULL, NULL, NULL, N'ToDate', N'4ebc103f-fda0-40df-8cbb-e857bc53f571', CAST(N'2020-11-16T16:33:28.120' AS DateTime), N'QueryString')
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (132, 65, N'fromDate', N'from', N'EQ', N'System.DateTime', N'TextBox', N'FromDate', NULL, NULL, NULL, N'FromDate', N'd816ef3b-0f2f-4013-b58d-3133977296b6', CAST(N'2020-11-16T16:33:28.120' AS DateTime), N'QueryString')
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (133, 65, N'toDate', N'to', N'EQ', N'System.DateTime', N'TextBox', N'ToDate', NULL, NULL, NULL, N'ToDate', N'01489499-56e4-482d-8eb7-95501671096a', CAST(N'2020-11-16T16:33:28.120' AS DateTime), N'QueryString')
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (135, 66, N'fromDate', N'from', N'EQ', N'System.DateTime', N'TextBox', N'FromDate', NULL, NULL, NULL, N'FromDate', N'530bdd56-2bc7-4fc3-95a1-6e193f761039', CAST(N'2020-11-16T16:33:28.123' AS DateTime), N'QueryString')
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (136, 66, N'toDate', N'to', N'EQ', N'System.DateTime', N'TextBox', N'ToDate', NULL, NULL, NULL, N'ToDate', N'8f870c99-f391-4a08-aefa-aa5210322bb3', CAST(N'2020-11-16T16:33:28.123' AS DateTime), N'QueryString')
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (138, 67, N'fromDate', N'from', N'EQ', N'System.DateTime', N'TextBox', N'FromDate', NULL, NULL, NULL, N'FromDate', N'867728b1-54da-4808-bcee-fbeccd4a0fd9', CAST(N'2020-11-16T16:33:28.123' AS DateTime), N'QueryString')
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (139, 67, N'toDate', N'to', N'EQ', N'System.DateTime', N'TextBox', N'ToDate', NULL, NULL, NULL, N'ToDate', N'174303f5-9b9d-4a0b-ac86-50e1d6a4bab2', CAST(N'2020-11-16T16:33:28.127' AS DateTime), N'QueryString')
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (141, 68, N'fromDate', N'from', N'EQ', N'System.DateTime', N'TextBox', N'FromDate', NULL, NULL, NULL, N'FromDate', N'00a575a6-5aed-4588-89c7-e424dfc0a6c9', CAST(N'2020-11-16T16:33:28.127' AS DateTime), N'QueryString')
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (142, 68, N'toDate', N'to', N'EQ', N'System.DateTime', N'TextBox', N'ToDate', NULL, NULL, NULL, N'ToDate', N'2a07a9bf-5b5d-4bfe-bf88-bf0df0b8f488', CAST(N'2020-11-16T16:33:28.127' AS DateTime), N'QueryString')
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (144, 69, N'fromDate', N'from', N'EQ', N'System.DateTime', N'TextBox', N'FromDate', NULL, NULL, NULL, N'FromDate', N'ee4f1225-2534-4284-8656-ca291efdd87d', CAST(N'2020-11-16T16:33:28.130' AS DateTime), N'QueryString')
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (145, 69, N'toDate', N'to', N'EQ', N'System.DateTime', N'TextBox', N'ToDate', NULL, NULL, NULL, N'ToDate', N'831d75cc-4468-49d6-b12c-75c712f1376b', CAST(N'2020-11-16T16:33:28.130' AS DateTime), N'QueryString')
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (146, 69, N'fromNo', N'from', N'EQ', N'System.Int32', N'TextBox', N'FromNo', NULL, NULL, NULL, N'FromNo', N'0d250786-4a36-4663-9a8c-51430f0c2715', CAST(N'2020-11-16T16:33:28.130' AS DateTime), N'QueryString')
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (147, 69, N'toNo', N'to', N'EQ', N'System.Int32', N'TextBox', N'ToNo', NULL, NULL, NULL, N'ToNo', N'b03fd7c8-30c3-4171-bc53-50e4590f8840', CAST(N'2020-11-16T16:33:28.130' AS DateTime), N'QueryString')
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (149, 70, N'fromDate', N'Date', N'GTE', N'System.DateTime', N'TextBox', N'FromDate', NULL, NULL, NULL, N'FromDate', N'8385212d-2096-4b29-9832-df3844384bef', CAST(N'2020-11-16T16:33:28.130' AS DateTime), N'GridOptions')
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (150, 70, N'toDate', N'Date', N'LTE', N'System.DateTime', N'TextBox', N'ToDate', NULL, NULL, NULL, N'ToDate', N'350f3c09-7198-418a-9540-d57c2bda8559', CAST(N'2020-11-16T16:33:28.133' AS DateTime), N'GridOptions')
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (151, 71, N'fromDate', N'Date', N'GTE', N'System.DateTime', N'TextBox', N'FromDate', NULL, NULL, NULL, N'FromDate', N'abe24fa4-74b9-4731-a086-fca4599f181c', CAST(N'2020-11-16T16:33:28.133' AS DateTime), N'GridOptions')
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (152, 71, N'toDate', N'Date', N'LTE', N'System.DateTime', N'TextBox', N'ToDate', NULL, NULL, NULL, N'ToDate', N'ec1dd965-5ea4-4b97-9c36-f0f1daea59b5', CAST(N'2020-11-16T16:33:28.137' AS DateTime), N'GridOptions')
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (153, 72, N'fromDate', N'Date', N'GTE', N'System.DateTime', N'TextBox', N'FromDate', NULL, NULL, NULL, N'FromDate', N'3d34114f-5a5d-47e3-8942-f3bfb6f7ec9c', CAST(N'2020-11-16T16:33:28.137' AS DateTime), N'GridOptions')
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (154, 72, N'toDate', N'Date', N'LTE', N'System.DateTime', N'TextBox', N'ToDate', NULL, NULL, NULL, N'ToDate', N'9bdab2ae-1342-4f82-ae52-aa93fe870ef2', CAST(N'2020-11-16T16:33:28.137' AS DateTime), N'GridOptions')
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (155, 73, N'fromDate', N'Date', N'GTE', N'System.DateTime', N'TextBox', N'FromDate', NULL, NULL, NULL, N'FromDate', N'd5d764db-f763-49d1-8906-6c08a3a3f313', CAST(N'2020-11-16T16:33:28.137' AS DateTime), N'GridOptions')
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (156, 73, N'toDate', N'Date', N'LTE', N'System.DateTime', N'TextBox', N'ToDate', NULL, NULL, NULL, N'ToDate', N'7c2356ce-ff9f-4e56-af0d-ace93101ebfc', CAST(N'2020-11-16T16:33:28.140' AS DateTime), N'GridOptions')
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (158, 76, N'fromDate', N'from', N'EQ', N'System.DateTime', N'TextBox', N'FromDate', NULL, NULL, NULL, N'FromDate', N'725dc31d-ca0e-4684-9be2-51dad9f407dc', CAST(N'2020-11-16T16:33:28.140' AS DateTime), N'QueryString')
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (159, 76, N'toDate', N'to', N'EQ', N'System.DateTime', N'TextBox', N'ToDate', NULL, NULL, NULL, N'ToDate', N'e0526a31-a2a7-4c29-a23f-38b05a131b75', CAST(N'2020-11-16T16:33:28.140' AS DateTime), N'QueryString')
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (161, 77, N'fromDate', N'from', N'EQ', N'System.DateTime', N'TextBox', N'FromDate', NULL, NULL, NULL, N'FromDate', N'12acf28a-3fe3-4343-a0db-2e5eee95f3e5', CAST(N'2020-11-16T16:33:28.143' AS DateTime), N'QueryString')
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (162, 77, N'toDate', N'to', N'EQ', N'System.DateTime', N'TextBox', N'ToDate', NULL, NULL, NULL, N'ToDate', N'b620feff-6706-4695-b70e-3b707722635a', CAST(N'2020-11-16T16:33:28.143' AS DateTime), N'QueryString')
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (164, 82, N'no', N'no', N'EQ', N'System.Int32', N'NumberBox', N'VoucherNo', NULL, NULL, NULL, N'VoucherNo', N'9547da60-1477-4dd9-ba38-848d5d091290', CAST(N'2021-11-05T17:11:56.697' AS DateTime), N'Route')
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (165, 83, N'no', N'no', N'EQ', N'System.Int32', N'NumberBox', N'VoucherNo', NULL, NULL, NULL, N'VoucherNo', N'578093cb-f49d-458b-ab97-ba9495e362f3', CAST(N'2021-11-05T17:11:56.703' AS DateTime), N'Route')
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (166, 84, N'no', N'no', N'EQ', N'System.Int32', N'NumberBox', N'VoucherNo', NULL, NULL, NULL, N'VoucherNo', N'9f4b49d1-eb3e-4de7-b199-2f68b37bfb59', CAST(N'2021-11-05T17:11:56.703' AS DateTime), N'Route')
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (167, 41, N'no', N'no', N'EQ', N'System.Int32', N'NumberBox', N'VoucherNo', NULL, NULL, NULL, N'VoucherNo', N'131b617b-8e0c-4f6c-9b1b-7e5f73b68f31', CAST(N'2021-11-15T11:20:12.253' AS DateTime), N'Route')
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (168, 85, N'fromDate', N'from', N'EQ', N'System.DateTime', N'DatePicker', N'FromDate', NULL, NULL, NULL, N'FromDate', N'6e9fe990-7fd5-4b98-9666-d4d07c39df73', CAST(N'2021-11-13T14:52:37.147' AS DateTime), N'DatePicker')
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (169, 85, N'toDate', N'to', N'EQ', N'System.DateTime', N'DatePicker', N'ToDate', NULL, NULL, NULL, N'ToDate', N'2a5d45b0-ab27-4c8c-bbd6-ebcaf1bb3c35', CAST(N'2021-11-13T14:52:37.147' AS DateTime), N'DatePicker')
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (170, 86, N'fromDate', N'from', N'EQ', N'System.DateTime', N'DatePicker', N'FromDate', NULL, NULL, NULL, N'FromDate', N'728995ab-f331-4c20-a2f8-3d406c2316e5', CAST(N'2021-11-13T14:52:37.147' AS DateTime), N'DatePicker')
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (171, 86, N'toDate', N'to', N'EQ', N'System.DateTime', N'DatePicker', N'ToDate', NULL, NULL, NULL, N'ToDate', N'20b6f2c5-353b-4e76-a714-f974519cc9a1', CAST(N'2021-11-13T14:52:37.147' AS DateTime), N'DatePicker')
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (172, 87, N'fromNo', N'from', N'EQ', N'System.Int32', N'NumberBox', N'FromNo', NULL, NULL, NULL, N'FromNo', N'b34899da-b1ac-4775-9a8c-627c18defe97', CAST(N'2021-11-13T14:52:37.147' AS DateTime), N'NumberBox')
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (173, 87, N'toNo', N'to', N'EQ', N'System.Int32', N'NumberBox', N'ToNo', NULL, NULL, NULL, N'ToNo', N'f45065b4-14c1-4afb-93d7-8d07eb2511b0', CAST(N'2021-11-13T14:52:37.147' AS DateTime), N'NumberBox')
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (174, 88, N'fromNo', N'from', N'EQ', N'System.Int32', N'NumberBox', N'FromNo', NULL, NULL, NULL, N'FromNo', N'ae4a6e07-01a6-4103-a3fc-f6835247ca85', CAST(N'2021-11-13T14:52:37.150' AS DateTime), N'NumberBox')
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (175, 88, N'toNo', N'to', N'EQ', N'System.Int32', N'NumberBox', N'ToNo', NULL, NULL, NULL, N'ToNo', N'c27542f8-d876-4224-97d9-4363589d345d', CAST(N'2021-11-13T14:52:37.150' AS DateTime), N'NumberBox')
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (176, 21, N'companyId', N'companyId', N'EQ', N'System.Int32', N'NumberBox', N'CompanyId', NULL, NULL, NULL, N'CompanyId', N'17754561-aa81-41a2-afe6-dd423886f5c4', CAST(N'2021-11-15T11:49:21.773' AS DateTime), N'Route')
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (177, 80, N'date', N'date', N'EQ', N'System.DateTime', N'DatePicker', N'Date', NULL, NULL, NULL, N'Date', N'7c984db0-8248-4bf9-908e-557a7677ddf3', CAST(N'2021-11-15T13:51:11.493' AS DateTime), N'QueryString')
GO
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DescriptionKey])
    VALUES (178, 89, 'fromDate', 'Date', 'GTE', 'System.DateTime', 'TextBox', 'FromDate', 'FromDate')
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DescriptionKey])
    VALUES (179, 89, 'toDate', 'Date', 'LTE', 'System.DateTime', 'TextBox', 'ToDate', 'ToDate')
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DescriptionKey])
    VALUES (180, 90, 'fromNo', 'No', 'GTE', 'System.Int32', 'TextBox', 'FromNo', 'FromNo')
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DescriptionKey])
    VALUES (181, 90, 'toNo', 'No', 'LTE', 'System.Int32', 'TextBox', 'ToNo', 'ToNo')
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [Source])
    VALUES (182, 104, N'receiptId', N'receiptId', N'EQ', 'System.Int32', N'NumberBox', N'ReceiptId', NULL, 1, NULL, N'ReceiptId', N'Route')
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [Source])
    VALUES (183, 105, N'receiptId', N'receiptId', N'EQ', 'System.Int32', N'NumberBox', N'ReceiptId', NULL, 1, NULL, N'ReceiptId', N'Route')
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [Source])
    VALUES (184, 106, N'paymentId', N'paymentId', N'EQ', 'System.Int32', N'NumberBox', N'PaymentId', NULL, 1, NULL, N'PaymentId', N'Route')
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [Source])
    VALUES (185, 107, N'paymentId', N'paymentId', N'EQ', 'System.Int32', N'NumberBox', N'PaymentId', NULL, 1, NULL, N'PaymentId', N'Route')
SET IDENTITY_INSERT [Reporting].[Parameter] OFF
GO

SET IDENTITY_INSERT [Metadata].[Command] ON
INSERT INTO [Metadata].[Command] ([CommandID], [ParentID], [PermissionID], [TitleKey], [RouteUrl], [IconName], [HotKey], [Index])
    VALUES (1, NULL, NULL, N'Accounting', NULL, 'folder-close', NULL, 1)
INSERT INTO [Metadata].[Command] ([CommandID], [ParentID], [PermissionID], [TitleKey], [RouteUrl], [IconName], [HotKey])
    VALUES (2, 1, NULL, N'BaseData', NULL, 'folder-close', NULL)
INSERT INTO [Metadata].[Command] ([CommandID], [ParentID], [PermissionID], [TitleKey], [RouteUrl], [IconName], [HotKey])
    VALUES (3, 2, 99, N'AccountGroup', N'/finance/account-groups', 'th-large', 'Ctrl+Shift+G')
INSERT INTO [Metadata].[Command] ([CommandID], [ParentID], [PermissionID], [TitleKey], [RouteUrl], [IconName], [HotKey])
    VALUES (4, 2, 1, N'Account', N'/finance/account', 'th-list', 'Ctrl+Shift+A')
INSERT INTO [Metadata].[Command] ([CommandID], [ParentID], [PermissionID], [TitleKey], [RouteUrl], [IconName], [HotKey])
    VALUES (5, 2, 8, N'DetailAccount', N'/finance/detailAccount', 'th', 'Ctrl+Shift+D')
INSERT INTO [Metadata].[Command] ([CommandID], [ParentID], [PermissionID], [TitleKey], [RouteUrl], [IconName], [HotKey])
    VALUES (6, 2, 15, N'CostCenter', N'/finance/costCenter', 'tower', 'Ctrl+Shift+C')
INSERT INTO [Metadata].[Command] ([CommandID], [ParentID], [PermissionID], [TitleKey], [RouteUrl], [IconName], [HotKey])
    VALUES (7, 2, 22, N'Project', N'/finance/projects', 'file', 'Ctrl+Shift+P')
INSERT INTO [Metadata].[Command] ([CommandID], [ParentID], [PermissionID], [TitleKey], [RouteUrl], [IconName], [HotKey])
    VALUES (8, 2, 126, N'AccountRelations', N'/finance/accountrelations', 'transfer', 'Ctrl+Shift+R')
INSERT INTO [Metadata].[Command] ([CommandID], [ParentID], [PermissionID], [TitleKey], [RouteUrl], [IconName], [HotKey])
    VALUES (9, 2, 106, N'AccountCollections', N'/finance/account-collection', 'list', 'Ctrl+Shift+H')
INSERT INTO [Metadata].[Command] ([CommandID], [ParentID], [PermissionID], [TitleKey], [RouteUrl], [IconName], [HotKey])
    VALUES (10, 2, 37, N'Currency', N'/finance/currency', 'usd', 'Ctrl+Shift+U')
INSERT INTO [Metadata].[Command] ([CommandID], [ParentID], [PermissionID], [TitleKey], [RouteUrl], [IconName], [HotKey])
    VALUES (11, 1, NULL, N'VoucherOps', NULL, 'folder-close', NULL)
INSERT INTO [Metadata].[Command] ([CommandID], [ParentID], [PermissionID], [TitleKey], [RouteUrl], [IconName], [HotKey])
    VALUES (12, 11, 46, N'NewVoucher', N'/finance/vouchers/new', 'plus', 'Ctrl+Alt+N')
INSERT INTO [Metadata].[Command] ([CommandID], [ParentID], [PermissionID], [TitleKey], [RouteUrl], [IconName], [HotKey])
    VALUES (13, 11, 45, N'VoucherByNo', N'/finance/vouchers/by-no', 'search', 'Ctrl+S')
INSERT INTO [Metadata].[Command] ([CommandID], [ParentID], [PermissionID], [TitleKey], [RouteUrl], [IconName], [HotKey])
    VALUES (14, 11, 60, N'LastVoucher', N'/finance/vouchers/last', 'list', 'Ctrl+L')
INSERT INTO [Metadata].[Command] ([CommandID], [ParentID], [PermissionID], [TitleKey], [RouteUrl], [IconName], [HotKey])
    VALUES (15, 11, 61, N'Vouchers', N'/finance/voucher', 'list', 'Ctrl+Shift+V')
INSERT INTO [Metadata].[Command] ([CommandID], [ParentID], [PermissionID], [TitleKey], [RouteUrl], [IconName], [HotKey])
    VALUES (16, 1, NULL, N'SpecialOps', NULL, 'folder-close', NULL)
INSERT INTO [Metadata].[Command] ([CommandID], [ParentID], [PermissionID], [TitleKey], [RouteUrl], [IconName], [HotKey])
    VALUES (17, 16, 45, N'IssueOpeningVoucher', N'/finance/vouchers/opening-voucher', 'list', 'Ctrl+Alt+Y')
INSERT INTO [Metadata].[Command] ([CommandID], [ParentID], [PermissionID], [TitleKey], [RouteUrl], [IconName], [HotKey])
    VALUES (18, 16, 45, N'IssueClosingVoucher', N'/finance/vouchers/closing-voucher', 'list', 'Ctrl+Alt+U')
INSERT INTO [Metadata].[Command] ([CommandID], [ParentID], [PermissionID], [TitleKey], [RouteUrl], [IconName], [HotKey])
    VALUES (19, 16, 45, N'ClosingTempAccounts', N'/finance/vouchers/close-temp-accounts', 'list', 'Ctrl+Alt+I')
INSERT INTO [Metadata].[Command] ([CommandID], [ParentID], [PermissionID], [TitleKey], [RouteUrl], [IconName], [HotKey])
    VALUES (20, 1, NULL, N'AccountingLedgers', NULL, 'folder-close', NULL)
INSERT INTO [Metadata].[Command] ([CommandID], [ParentID], [PermissionID], [TitleKey], [RouteUrl], [IconName], [HotKey])
    VALUES (21, 20, 141, N'JournalLedger', N'/finance/journal', 'list', 'Ctrl+Alt+Z')
INSERT INTO [Metadata].[Command] ([CommandID], [ParentID], [PermissionID], [TitleKey], [RouteUrl], [IconName], [HotKey])
    VALUES (22, 20, 147, N'AccountBook', N'/finance/account-book', 'list', 'Ctrl+Alt+B')
INSERT INTO [Metadata].[Command] ([CommandID], [ParentID], [PermissionID], [TitleKey], [RouteUrl], [IconName], [HotKey], [Index])
    VALUES (23, NULL, NULL, N'Organization', NULL, 'folder-close', NULL, 3)
INSERT INTO [Metadata].[Command] ([CommandID], [ParentID], [PermissionID], [TitleKey], [RouteUrl], [IconName], [HotKey])
    VALUES (24, 23, 78, N'Companies', N'/organization/companies', 'list', 'Ctrl+Alt+C')
INSERT INTO [Metadata].[Command] ([CommandID], [ParentID], [PermissionID], [TitleKey], [RouteUrl], [IconName], [HotKey])
    VALUES (25, 23, 70, N'Branches', N'/organization/branches', 'list', 'Ctrl+Shift+B')
INSERT INTO [Metadata].[Command] ([CommandID], [ParentID], [PermissionID], [TitleKey], [RouteUrl], [IconName], [HotKey])
    VALUES (26, 23, 29, N'FiscalPeriods', N'/organization/fiscalperiod', 'list', 'Ctrl+Shift+F')
INSERT INTO [Metadata].[Command] ([CommandID], [ParentID], [PermissionID], [TitleKey], [RouteUrl], [IconName], [HotKey], [Index])
    VALUES (27, NULL, NULL, N'Administration', NULL, 'folder-close', NULL, 4)
INSERT INTO [Metadata].[Command] ([CommandID], [ParentID], [PermissionID], [TitleKey], [RouteUrl], [IconName], [HotKey])
    VALUES (28, 27, 82, N'Users', N'/admin/users', 'user', 'Ctrl+Shift+U')
INSERT INTO [Metadata].[Command] ([CommandID], [ParentID], [PermissionID], [TitleKey], [RouteUrl], [IconName], [HotKey])
    VALUES (29, 27, 89, N'Roles', N'/admin/roles', 'list', 'Ctrl+Alt+H')
INSERT INTO [Metadata].[Command] ([CommandID], [ParentID], [PermissionID], [TitleKey], [RouteUrl], [IconName], [HotKey])
    VALUES (30, 27, 132, N'RowAccessSettings', N'/admin/viewRowPermission', 'lock', 'Ctrl+Alt+W')
INSERT INTO [Metadata].[Command] ([CommandID], [ParentID], [PermissionID], [TitleKey], [RouteUrl], [IconName], [HotKey])
    VALUES (31, 27, 128, N'Settings', N'/config/settings', 'wrench', 'Ctrl+K')
INSERT INTO [Metadata].[Command] ([CommandID], [ParentID], [PermissionID], [TitleKey], [RouteUrl], [IconName], [HotKey])
    VALUES (32, 27, 108, N'OperationLogs', N'/admin/operation-log', 'list', 'Ctrl+Shift+O')
INSERT INTO [Metadata].[Command] ([CommandID], [ParentID], [PermissionID], [TitleKey], [RouteUrl], [IconName], [HotKey], [Index])
    VALUES (33, NULL, NULL, N'Profile', NULL, 'folder-close', NULL, 0)
INSERT INTO [Metadata].[Command] ([CommandID], [ParentID], [PermissionID], [TitleKey], [RouteUrl], [IconName], [HotKey])
    VALUES (34, 33, NULL, N'ChangePassword', N'/admin/changePassword', 'eye-open', NULL)
INSERT INTO [Metadata].[Command] ([CommandID], [ParentID], [PermissionID], [TitleKey], [RouteUrl], [IconName], [HotKey])
    VALUES (35, 33, NULL, N'LogOut', N'/logout', 'log-out', 'Ctrl+Shift+X')
INSERT INTO [Metadata].[Command] ([CommandID], [ParentID], [PermissionID], [TitleKey], [RouteUrl], [IconName], [HotKey])
    VALUES (36, 33, NULL, N'ChangeCompany', N'/login', 'tasks', NULL)
INSERT INTO [Metadata].[Command] ([CommandID], [ParentID], [PermissionID], [TitleKey], [RouteUrl], [IconName], [HotKey], [Index])
    VALUES (37, NULL, NULL, N'Tools', NULL, 'folder-close', NULL, 5)
INSERT INTO [Metadata].[Command] ([CommandID], [ParentID], [PermissionID], [TitleKey], [RouteUrl], [IconName], [HotKey])
    VALUES (38, 37, 120, N'ReportManagement', N'/tadbir/reports', 'list', NULL)
INSERT INTO [Metadata].[Command] ([CommandID], [ParentID], [PermissionID], [TitleKey], [RouteUrl], [IconName], [HotKey])
    VALUES (39, 1, NULL, N'FinancialReports', NULL, 'folder-close', NULL)
INSERT INTO [Metadata].[Command] ([CommandID], [ParentID], [PermissionID], [TitleKey], [RouteUrl], [IconName], [HotKey])
    VALUES (40, 39, 153, N'TestBalance', N'/finance/balance', 'list', 'Ctrl+Shift+T')
INSERT INTO [Metadata].[Command] ([CommandID], [ParentID], [PermissionID], [TitleKey], [RouteUrl], [IconName], [HotKey])
    VALUES (41, 20, 158, N'CurrencyBook', N'/finance/currency-book', 'list', 'Ctrl+Alt+J')
INSERT INTO [Metadata].[Command] ([CommandID], [ParentID], [PermissionID], [TitleKey], [RouteUrl], [IconName], [HotKey])
    VALUES (42, 27, 174, N'SystemIssue', N'/finance/system-issue', 'tasks', 'Ctrl+Shift+S')
INSERT INTO [Metadata].[Command] ([CommandID], [ParentID], [PermissionID], [TitleKey], [RouteUrl], [IconName], [HotKey])
    VALUES (43, 39, 164, N'ItemBalance', N'/finance/itembalance', 'list', 'Ctrl+Shift+I')
INSERT INTO [Metadata].[Command] ([CommandID], [ParentID], [PermissionID], [TitleKey], [RouteUrl], [IconName], [HotKey])
    VALUES (44, 39, 169, N'BalanceByAccount', N'/finance/balance-by-account', 'list', 'Ctrl+Shift+B')
INSERT INTO [Metadata].[Command] ([CommandID], [ParentID], [PermissionID], [TitleKey], [RouteUrl], [IconName], [HotKey])
    VALUES (45, 27, 130, N'LogSettings', N'/admin/log-settings', 'list', NULL)
INSERT INTO [Metadata].[Command] ([CommandID], [ParentID], [PermissionID], [TitleKey], [RouteUrl], [IconName], [HotKey])
    VALUES (46, 39, 175, N'ProfitLoss', N'/finance/profit-loss', 'list', 'Ctrl+Shift+R')
INSERT INTO [Metadata].[Command] ([CommandID], [ParentID], [PermissionID], [TitleKey], [RouteUrl], [IconName], [HotKey])
    VALUES (47, 11, 180, N'NewDraftVoucher', N'/finance/vouchers/new/draft', 'list', 'Ctrl+Alt+V')
INSERT INTO [Metadata].[Command] ([CommandID], [ParentID], [PermissionID], [TitleKey], [RouteUrl], [IconName], [HotKey])
    VALUES (48, 11, 179, N'DraftVoucherByNo', N'/finance/vouchers/by-no/draft', 'list', 'Ctrl+Alt+D')
INSERT INTO [Metadata].[Command] ([CommandID], [ParentID], [PermissionID], [TitleKey], [RouteUrl], [IconName], [HotKey])
    VALUES (49, 11, 189, N'LastDraftVoucher', N'/finance/vouchers/last/draft', 'list', 'Ctrl+Alt+Q')
INSERT INTO [Metadata].[Command] ([CommandID], [ParentID], [PermissionID], [TitleKey], [RouteUrl], [IconName], [HotKey])
    VALUES (50, 39, 201, N'BalanceSheet', N'/finance/bal-sheet', 'list', 'Ctrl+Shift+K')
INSERT INTO [Metadata].[Command] ([CommandID], [ParentID], [PermissionID], [TitleKey], [RouteUrl], [IconName], [HotKey])
    VALUES (51, 37, 210, N'ManageDashboard', N'/tadbir/dashboard', 'list', NULL)
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey, [Index])
    VALUES (52, NULL, NULL, N'Treasury', NULL, N'folder-close', NULL, 2)
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey)
    VALUES (53, 52, NULL, N'BaseData', NULL, N'folder-close', NULL)
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey)
    VALUES (54, 52, NULL, N'CheckOperations', NULL, N'folder-close', NULL)
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey)
    VALUES (55, 52, NULL, N'CheckReports', NULL, N'folder-close', NULL)
INSERT INTO [Metadata].[Command] ([CommandID], [ParentID], [PermissionID], [TitleKey], [RouteUrl], [IconName], [HotKey])
    VALUES (56, 54, 215, N'NewCheckBook', N'/treasury/check-books/new', NULL, NULL)
INSERT INTO [Metadata].[Command] ([CommandID], [ParentID], [PermissionID], [TitleKey], [RouteUrl], [IconName], [HotKey])
    VALUES (57, 54, 218, N'LastCheckBook', N'/treasury/check-books/last', NULL, NULL)
INSERT INTO [Metadata].[Command] ([CommandID], [ParentID], [PermissionID], [TitleKey], [RouteUrl], [IconName], [HotKey])
    VALUES (58, 54, 212, N'CheckBookByName', N'/treasury/check-books/by-name', NULL, NULL)
INSERT INTO [Metadata].[Command] ([CommandID], [ParentID], [PermissionID], [TitleKey], [RouteUrl], [IconName], [HotKey])
    VALUES (59, 53, 225, N'CashRegisters', N'/treasury/cash-register', 'list', NULL)
INSERT INTO [Metadata].[Command] ([CommandID], [ParentID], [PermissionID], [TitleKey], [RouteUrl], [IconName], [HotKey])
    VALUES (60, 55, 233, N'CheckBookReport', N'/treasury/check-book-report', NULL, NULL)
INSERT INTO [Metadata].[Command] ([CommandID], [ParentID], [PermissionID], [TitleKey], [RouteUrl], [IconName], [HotKey])
    VALUES (61, 53, 239, N'SourceApps', '/treasury/source-apps', 'list', NULL)
INSERT INTO [Metadata].[Command] ([CommandID], [ParentID], [PermissionID], [TitleKey], [RouteUrl], [IconName], [HotKey])
    VALUES (62, 52, NULL, N'ReceiptOperations', NULL, 'folder-close', NULL)
INSERT INTO [Metadata].[Command] ([CommandID], [ParentID], [PermissionID], [TitleKey], [RouteUrl], [IconName], [HotKey])
    VALUES (63, 52, NULL, N'PaymentOperations', NULL, 'folder-close', NULL)
INSERT INTO [Metadata].[Command] ([CommandID], [ParentID], [PermissionID], [TitleKey], [RouteUrl], [IconName], [HotKey])
    VALUES (64, 62, 248, N'NewReceiptForm', N'/treasury/receipts/new', 'plus', NULL)
INSERT INTO [Metadata].[Command] ([CommandID], [ParentID], [PermissionID], [TitleKey], [RouteUrl], [IconName], [HotKey])
    VALUES (65, 62, 251, N'LastReceiptForm', N'/treasury/receipts/last', 'list', NULL)
INSERT INTO [Metadata].[Command] ([CommandID], [ParentID], [PermissionID], [TitleKey], [RouteUrl], [IconName], [HotKey])
    VALUES (66, 62, 246, N'ReceiptFormbyNo', N'/treasury/receipts/by-no', 'search', NULL)
INSERT INTO [Metadata].[Command] ([CommandID], [ParentID], [PermissionID], [TitleKey], [RouteUrl], [IconName], [HotKey])
    VALUES (67, 63, 259, N'NewPaymentForm', N'/treasury/payments/new', 'plus', NULL)
INSERT INTO [Metadata].[Command] ([CommandID], [ParentID], [PermissionID], [TitleKey], [RouteUrl], [IconName], [HotKey])
    VALUES (68, 63, 262, N'LastPaymentForm', N'/treasury/payments/last', 'list', NULL)
INSERT INTO [Metadata].[Command] ([CommandID], [ParentID], [PermissionID], [TitleKey], [RouteUrl], [IconName], [HotKey])
    VALUES (69, 63, 257, N'PaymentFormbyNo', N'/treasury/payments/by-no', 'search', NULL)
























































































































































































































































































































INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey)
    VALUES (100000, NULL, NULL, N'ProductScope', NULL, N'folder-close', NULL)
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey)
    VALUES (100001, 100000, NULL, N'BaseData', NULL, N'folder-close', NULL)
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey)
    VALUES (100002, 100000, NULL, N'ProductScopeOperations', NULL, N'folder-close', NULL)
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey)
    VALUES (100003, 100000, NULL, N'ProductScopeReports', NULL, N'folder-close', NULL)
INSERT INTO [Metadata].[Command] ([CommandID], [ParentID], [PermissionID], [TitleKey], [RouteUrl], [IconName], [HotKey])
    VALUES (100004, 100001, 100001, N'Brand', N'product-scope/brands', NULL, NULL)
INSERT INTO [Metadata].[Command] ([CommandID], [ParentID], [PermissionID], [TitleKey], [RouteUrl], [IconName], [HotKey])
    VALUES (100005, 100001, 100008, N'Units', N'/product-scope/products', NULL, NULL)
INSERT INTO [Metadata].[Command] ([CommandID], [ParentID], [PermissionID], [TitleKey], [RouteUrl], [IconName], [HotKey])
    VALUES (100006, 100001, 100015, N'Property', N'/product-scope/prperties', 'list', NULL)
SET IDENTITY_INSERT [Metadata].[Command] OFF

SET IDENTITY_INSERT [Reporting].[SystemIssue] ON 
INSERT [Reporting].[SystemIssue] ([SystemIssueID], [ParentID], [PermissionID], [ViewID], [TitleKey], [ApiUrl], [DeleteApiUrl], [BranchScope]) 
    VALUES (1, NULL, NULL, NULL, N'Accounting', NULL, NULL, 0)
INSERT [Reporting].[SystemIssue] ([SystemIssueID], [ParentID], [PermissionID], [ViewID], [TitleKey], [ApiUrl], [DeleteApiUrl], [BranchScope]) 
    VALUES (2, 1, NULL, NULL, N'VoucherIssues', NULL, NULL, 0)
INSERT [Reporting].[SystemIssue] ([SystemIssueID], [ParentID], [PermissionID], [ViewID], [TitleKey], [ApiUrl], [DeleteApiUrl], [BranchScope]) 
    VALUES (3, 1, NULL, NULL, N'AccountIssues', NULL, NULL, 0)
INSERT [Reporting].[SystemIssue] ([SystemIssueID], [ParentID], [PermissionID], [ViewID], [TitleKey], [ApiUrl], [DeleteApiUrl], [BranchScope]) 
    VALUES (4, 2, 45, 2, N'UnbalancedVouchers', N'/sys-issues/vouchers/unbalanced', N'/vouchers', 1)
INSERT [Reporting].[SystemIssue] ([SystemIssueID], [ParentID], [PermissionID], [ViewID], [TitleKey], [ApiUrl], [DeleteApiUrl], [BranchScope]) 
    VALUES (5, 2, 45, 2, N'VouchersWithNoArticle', N'/sys-issues/vouchers/no-article', N'/vouchers', 1)
INSERT [Reporting].[SystemIssue] ([SystemIssueID], [ParentID], [PermissionID], [ViewID], [TitleKey], [ApiUrl], [DeleteApiUrl], [BranchScope]) 
    VALUES (6, 2, 45, 42, N'ArticlesHavingZeroAmount', N'/sys-issues/articles/zero-amount', N'/vouchers/articles', 1)
INSERT [Reporting].[SystemIssue] ([SystemIssueID], [ParentID], [PermissionID], [ViewID], [TitleKey], [ApiUrl], [DeleteApiUrl], [BranchScope]) 
    VALUES (7, 2, 45, 42, N'ArticlesWithMissingAccount', N'/sys-issues/articles/miss-acc', N'/vouchers/articles', 1)
INSERT [Reporting].[SystemIssue] ([SystemIssueID], [ParentID], [PermissionID], [ViewID], [TitleKey], [ApiUrl], [DeleteApiUrl], [BranchScope]) 
    VALUES (8, 2, 45, 42, N'ArticlesWithInvalidAccountItems', N'/sys-issues/articles/invalid-acc', N'/vouchers/articles', 1)
INSERT [Reporting].[SystemIssue] ([SystemIssueID], [ParentID], [PermissionID], [ViewID], [TitleKey], [ApiUrl], [DeleteApiUrl], [BranchScope]) 
    VALUES (9, 2, NULL, 41, N'MissingVoucherNumbers', N'/sys-issues/vouchers/miss-number', NULL, 0)
INSERT [Reporting].[SystemIssue] ([SystemIssueID], [ParentID], [PermissionID], [ViewID], [TitleKey], [ApiUrl], [DeleteApiUrl], [BranchScope]) 
    VALUES (10, 3, 45, 42, N'AccountsWithInvalidBalance', N'/sys-issues/articles/invalid-balance', NULL, 0)
INSERT [Reporting].[SystemIssue] ([SystemIssueID], [ParentID], [PermissionID], [ViewID], [TitleKey], [ApiUrl], [DeleteApiUrl], [BranchScope]) 
    VALUES (11, 3, 45, 42, N'AccountsWithInvalidPeriodTurnover', N'/sys-issues/articles/invalid-turnover', NULL, 0)
SET IDENTITY_INSERT [Reporting].[SystemIssue] OFF


UPDATE Metadata.Command SET HotKey = 'Ctrl+Shift+G' WHERE TitleKey = 'AccountGroup'
UPDATE Metadata.Command SET HotKey = 'Ctrl+Shift+U' WHERE TitleKey = 'Currency'
UPDATE Metadata.Command SET HotKey = 'Ctrl+Shift+H' WHERE TitleKey = 'AccountCollections'
UPDATE Metadata.Command SET HotKey = 'Ctrl+Alt+V'   WHERE TitleKey = 'NewDraftVoucher'
UPDATE Metadata.Command SET HotKey = 'Ctrl+Alt+D'   WHERE TitleKey = 'DraftVoucherByNo'
UPDATE Metadata.Command SET HotKey = 'Ctrl+Alt+Q'   WHERE TitleKey = 'LastDraftVoucher'
UPDATE Metadata.Command SET HotKey = 'Ctrl+Alt+Y'   WHERE TitleKey = 'IssueOpeningVoucher'
UPDATE Metadata.Command SET HotKey = 'Ctrl+Alt+U'   WHERE TitleKey = 'IssueClosingVoucher'
UPDATE Metadata.Command SET HotKey = 'Ctrl+Alt+I'   WHERE TitleKey = 'ClosingTempAccounts'
UPDATE Metadata.Command SET HotKey = 'Ctrl+Alt+Z'   WHERE TitleKey = 'JournalLedger'
UPDATE Metadata.Command SET HotKey = 'Ctrl+Alt+B'   WHERE TitleKey = 'AccountBook'
UPDATE Metadata.Command SET HotKey = 'Ctrl+Alt+J'   WHERE TitleKey = 'CurrencyBook'
UPDATE Metadata.Command SET HotKey = 'Ctrl+Shift+I' WHERE TitleKey = 'ItemBalance'
UPDATE Metadata.Command SET HotKey = 'Ctrl+Shift+B' WHERE TitleKey = 'BalanceByAccount'
UPDATE Metadata.Command SET HotKey = 'Ctrl+Shift+K' WHERE TitleKey = 'BalanceSheet'
UPDATE Metadata.Command SET HotKey = 'Ctrl+Alt+C'   WHERE TitleKey = 'Companies'
UPDATE Metadata.Command SET HotKey = 'Ctrl+Alt+H'   WHERE TitleKey = 'Roles'
UPDATE Metadata.Command SET HotKey = 'Ctrl+Alt+W'   WHERE TitleKey = 'RowAccessSettings'
UPDATE Metadata.Command SET HotKey = 'Ctrl+K'       WHERE TitleKey = 'Settings'
UPDATE Metadata.Command SET HotKey = 'Ctrl+Shift+S' WHERE TitleKey = 'SystemIssue'
UPDATE Metadata.Command SET HotKey = 'Ctrl+Alt+N'   WHERE TitleKey = 'NewVoucher'
UPDATE Metadata.Command set HotKey = 'Ctrl+Alt+T' Where TitleKey = 'TestBalance'
UPDATE Metadata.Command set HotKey = 'Ctrl+Alt+R' Where TitleKey = 'ProfitLoss'
UPDATE Metadata.Command set HotKey = 'Ctrl+Alt+K' Where TitleKey = 'Users'
UPDATE Metadata.Command set HotKey = 'Ctrl+Alt+L' Where TitleKey = 'OperationLogs'
UPDATE [Metadata].[ShortcutCommand] Set HotKey = 'Ctrl+Shift+Insert' where [Name] = 'NewRecord'
UPDATE Metadata.Command set HotKey = 'Ctrl+Alt+E' Where TitleKey = 'Branches'
UPDATE Metadata.Command set HotKey = NULL Where TitleKey = 'ReportManagement'
UPDATE [Metadata].[ShortcutCommand] Set HotKey = 'Ctrl+Shift+Delete' where [Name] = 'DeleteRecord'